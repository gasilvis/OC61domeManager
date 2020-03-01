'tabs=4
' --------------------------------------------------------------------------------
'
' ASCOM Dome driver for OC61domeServer2
'
' Description:	- This driver wraps around a Dome driver, simply passing
'				all commands and requests. And
'               - It provides a serial interface to the OC61 Peripheral Controller
'                     commands supported
'                         Action("ScopePosition")
'                         ShutterStatus queries the PC
'				- It watches for Shutter changes and moves a mirror cover
'				   as appropriate. The mirror cover is accessed via the Peripheral Controll
'               attached to the serial port	we have selected.
'
' Implements:	ASCOM Dome interface version: 1.0
' Author:		(SGEO) George Silvis <George@GASilvis.net>
'
' Edit Log:
'
' Date			Who	Vers	Description
' -----------	---	-----	-------------------------------------------------------
' dd-mmm-yyyy	XXX	1.0.0	Initial edit, from Dome template
' ---------------------------------------------------------------------------------
'
'
' Your driver's ID is ASCOM.OC61domeServer2.Dome
'
' The Guid attribute sets the CLSID for ASCOM.DeviceName.Dome
' The ClassInterface/None addribute prevents an empty interface called
' _Dome from being created and used as the [default] interface
'

' This definition is used to select code that's only applicable for one device type
#Const Device = "Dome"

Imports ASCOM
Imports ASCOM.Astrometry
Imports ASCOM.Astrometry.AstroUtils
Imports ASCOM.DeviceInterface
Imports ASCOM.OC61domeServer2
Imports ASCOM.Utilities

Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Runtime.InteropServices
Imports System.Text

'<Guid("5ff4c119-d85c-49ac-8fc6-00aa50af7c95")>
<Guid("122CDD7A-7F74-4272-8721-BA809ACF34C6")>
<ProgId(“ASCOM.OC61domeServer2.Dome”)>
<ServedClassName(“OC61 Dome Server”)>
<ClassInterface(ClassInterfaceType.None)>
Public Class Dome
    '==================================
    Inherits ReferenceCountedObjectBase
    '==================================

    ' The Guid attribute sets the CLSID for ASCOM.OC61domeServer2.Dome
    ' The ClassInterface/None addribute prevents an empty interface called
    ' _OC61domeServer2 from being created and used as the [default] interface

    ' Replace the "not implemented exceptions" with code to implement the function or
    ' throw the appropriate ASCOM exception.
    '
    Implements IDomeV2

    '
    ' Driver ID and descriptive string that shows in the Chooser
    '
    'Friend Shared driverID As String = "ASCOM.OC61domeServer2.Dome"
    'Private Shared driverDescription As String = "OC61domeServer2 Dome"

    Friend Shared comPortProfileName As String = "COM Port" 'Constants used for Profile persistence
    Friend Shared traceStateProfileName As String = "Trace Level"
    Friend Shared childDomeIdProfileName As String = "Child Dome"
    Friend Shared comPortDefault As String = "COM1"
    Friend Shared traceStateDefault As String = "False"
    Friend Shared childDomeIdDefault As String = "DomeSim.Dome"  ' will be MaxDomII

    Friend Shared comPort As String ' Variables to hold the currrent device configuration
    Friend Shared traceState As Boolean
    Friend Shared childDomeId As String = childDomeIdDefault

    Private connectedState As Boolean ' Private variable to hold the connected state
    Private utilities As Util ' Private variable to hold an ASCOM Utilities object
    Private astroUtilities As AstroUtils ' Private variable to hold an AstroUtils object to provide the Range method
    Private TL As TraceLogger ' Private variable to hold the trace logger object (creates a diagnostic log file with information that you specify)
    Friend Shared childDome As ASCOM.DriverAccess.Dome
    Friend Shared PCStatusUpdateTimer As System.Timers.Timer
    Friend Shared PCStatusUpdateCounter As Integer
    Friend Shared PCStatusUpdateCalled As Integer = False
    '
    ' Constructor - Must be public for COM registration!
    '

    Dim OC61ActionList As ArrayList = New ArrayList()

    Public Sub New()
        DriverID = Marshal.GenerateProgIdForType(Me.GetType())
        ReadProfile() ' Read device configuration from the ASCOM Profile store
        TL = New TraceLogger("", "OC61domeServer2") With {
            .Enabled = traceState
        }
        TL.LogMessage("Dome", "Starting initialisation")

        connectedState = False ' Initialise connected to false
        utilities = New Util() ' Initialise util object
        astroUtilities = New AstroUtils 'Initialise new astro utiliites object
        childDome = New ASCOM.DriverAccess.Dome(childDomeId)

        ' Implement your additional construction here
        PCStatusUpdateTimer = New System.Timers.Timer()
        AddHandler PCStatusUpdateTimer.Elapsed, AddressOf OnPCstatusUpdate
        PCStatusUpdateTimer.Interval = 1000
        PCStatusUpdateTimer.Enabled = False

        OC61ActionList.Add("ScopePosition")
        OC61ActionList.Add("ADCvalues")

        TL.LogMessage("Dome", "Completed initialisation")


    End Sub

    ' not in use
    Public Sub OnPCstatusUpdate(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
        ' runs every second
        TL.LogMessage("PCStatusTimer", "checking shutter status")
        PCStatusUpdateCalled = True ' signal for ShutterStatus that this timer is calling, so do its job
        domeShutterState = ShutterStatus()
        PCStatusUpdateCalled = False
        If domeShutterState <> ShutterState.shutterClosing And domeShutterState <> ShutterState.shutterOpening Then
            PCStatusUpdateTimer.Enabled = False ' we're done with the timer
            TL.LogMessage("PCStatustimer", "finisihed")
        Else
            PCStatusUpdateCounter -= 1
            If PCStatusUpdateCounter <= 0 Then
                PCStatusUpdateTimer.Enabled = False
                domeShutterState = ShutterState.shutterError
                TL.LogMessage("PCStatusTimer", "Timedout error")
            End If
        End If
    End Sub

    '
    ' PUBLIC COM INTERFACE IDomeV2 IMPLEMENTATION
    '

#Region "Common properties and methods"
    ''' <summary>
    ''' Displays the Setup Dialog form.
    ''' If the user clicks the OK button to dismiss the form, then
    ''' the new settings are saved, otherwise the old values are reloaded.
    ''' THIS IS THE ONLY PLACE WHERE SHOWING USER INTERFACE IS ALLOWED!
    ''' </summary>
    Public Sub SetupDialog() Implements IDomeV2.SetupDialog
        ' consider only showing the setup dialog if not connected
        ' or call a different dialog if connected
        If IsConnected And childDome.Connected Then
            System.Windows.Forms.MessageBox.Show("Already connected, just press OK")
        End If

        Using F As SetupDialogForm = New SetupDialogForm()
            Dim result As System.Windows.Forms.DialogResult = F.ShowDialog()
            If result = DialogResult.OK Then
                WriteProfile() ' Persist device configuration values to the ASCOM Profile store
                ' TODO  connect to child
            End If
        End Using
    End Sub

    Public ReadOnly Property SupportedActions() As ArrayList Implements IDomeV2.SupportedActions
        Get
            'TL.LogMessage("SupportedActions Get", "Returning empty arraylist")
            'Return New ArrayList()
            TL.LogMessage("SupportedActions Get", "Asked child dome too")
            Dim al As ArrayList = childDome.SupportedActions()
            For ai As Integer = 0 To OC61ActionList.Count - 1
                al.Add(OC61ActionList(ai).ToString())
            Next
            Return al
        End Get
    End Property

    Public Function Action(ByVal ActionName As String, ByVal ActionParameters As String) As String Implements IDomeV2.Action
        ' Is it local?
        Dim ret As String
        Select Case ActionName.ToLower()
            Case "scopeposition" ' ActionList(0)
                ' ask for HA, look for true and return
                CommandBlind("*SEND_POSITION" & vbCr) ' Get HA, Dec
                ret = Listener({"True"})
                If ret = "" Then ret = "timed out" 'Throw New TimeoutException
                Return ret
            Case "adcvalues"
                ' ask for HA, look for true and return
                CommandBlind("*SEND_ADC_VALS" & vbCr) ' Get HA, Dec
                ret = Listener({"HA_ABS"})
                If ret = "" Then ret = "timed out" 'Throw New TimeoutException
                Return ret
            Case Else
                Return childDome.Action(ActionName, ActionParameters)
        End Select
        'Throw New ActionNotImplementedException("Action " & ActionName & " is not supported by this driver")
    End Function

    Public Function Listener(prefix() As String) As String ' accepts an array of prefixes 
        SharedResources.SharedSerial.ReceiveTimeoutMs = 200 '  200ms
        Dim rdata As String = ""
        Dim cnt As Integer = 100 ' TODO shorten to 10
        While cnt
            Try
                Dim b As Char = Convert.ToChar(SharedResources.SharedSerial.ReceiveByte())
                rdata = b & SharedResources.SharedSerial.ReceiveTerminated(vbCr)
                TL.LogMessage("Listener", " msg seen: " & rdata)
                For Each pre As String In prefix
                    'TL.LogMessage("Listener", " prefix to test: " & pre)
                    If rdata.StartsWith(pre) Then
                        TL.LogMessage("Listener", " msg caught: " & rdata)
                        Return rdata
                    End If
                Next
                'Return "no prefix: " & rdata '  comment out
            Catch
            End Try
            cnt -= 1
        End While
        Return "ran out of time"

    End Function

    ' commands to be sent to the Peripheral Controller
    'Public Sub PCcommandBlind(ByVal Command As String)
    '   CheckConnected("PCCommandBlind")
    '   SharedResources.SendMessage(Command)
    'End Sub

    ' CommandBlind taken over for use with the Peripheral Controller
    Public Sub CommandBlind(ByVal Command As String, Optional ByVal Raw As Boolean = False) Implements IDomeV2.CommandBlind
        CheckConnected("CommandBlind")
        SharedResources.SharedSerial.ClearBuffers() ' flush buffers
        SharedResources.SendMessage(Command) ' to the PC
        ' Call CommandString and return as soon as it finishes
        'Me.CommandString(Command, Raw)
        ' or
        'Throw New MethodNotImplementedException("CommandBlind")
    End Sub

    Public Function CommandBool(ByVal Command As String, Optional ByVal Raw As Boolean = False) As Boolean _
        Implements IDomeV2.CommandBool
        CheckConnected("CommandBool")
        Dim ret As String = CommandString(Command, Raw)
        ' decode the return string and return true or false
        Return "TRUE" = ret.ToUpper()
        ' or
        'Throw New MethodNotImplementedException("CommandBool")
    End Function

    Public Function CommandString(ByVal Command As String, Optional ByVal Raw As Boolean = False) As String _
        Implements IDomeV2.CommandString
        CheckConnected("CommandString")
        ' it's a good idea to put all the low level communication with the device here,
        ' then all communication calls this function
        Dim ret As String = childDome.CommandString(Command, Raw)
        Return ret
        ' you need something to ensure that only one command is in progress at a time
        ' or
        'Throw New MethodNotImplementedException("CommandString")
    End Function

    '// called by driver clients
    Public Property Connected() As Boolean Implements IDomeV2.Connected
        Get
            TL.LogMessage("Connected Get", IsConnected.ToString())
            Return IsConnected
        End Get
        Set(value As Boolean)
            TL.LogMessage("Connected Set", value.ToString())
            If value = IsConnected Then
                Return
            End If

            If value Then
                connectedState = True
                TL.LogMessage("Connected Set", "Connecting to port " + comPort + " and " + childDomeId)
                childDome.Connected = True
                ' TODO throw error on fail
                SharedResources.comPort = comPort ' TODO is comport set?
                SharedResources.Connected = True
                ' TODO throw error on fail
            Else
                connectedState = False
                TL.LogMessage("Connected Set", "Disconnecting from port " + comPort)
                ' TODO disconnect from the device
                childDome.Connected = False
                SharedResources.Connected = False
            End If
        End Set
    End Property

    Public ReadOnly Property Description As String Implements IDomeV2.Description
        Get
            ' this pattern seems to be needed to allow a public property to return a private field
            Dim d As String = "Child dome: " & childDome.Description
            TL.LogMessage("Description Get", d)
            Return d
        End Get
    End Property

    Public ReadOnly Property DriverInfo As String Implements IDomeV2.DriverInfo
        Get
            Dim m_version As Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version
            ' TODO customise this driver description
            'Dim s_driverInfo As String = "Information about the driver itself. Version: " + m_version.Major.ToString() + "." + m_version.Minor.ToString()
            Dim s_driverInfo As String = childDome.DriverInfo
            TL.LogMessage("DriverInfo Get", s_driverInfo)
            Return s_driverInfo
        End Get
    End Property

    Public ReadOnly Property DriverVersion() As String Implements IDomeV2.DriverVersion
        Get
            ' Get our own assembly and report its version number
            TL.LogMessage("DriverVersion Get", Reflection.Assembly.GetExecutingAssembly.GetName.Version.ToString(2))
            'Return Reflection.Assembly.GetExecutingAssembly.GetName.Version.ToString(2)
            Return childDome.DriverVersion
        End Get
    End Property

    Public ReadOnly Property InterfaceVersion() As Short Implements IDomeV2.InterfaceVersion
        Get
            TL.LogMessage("InterfaceVersion Get", "2")
            Return childDome.InterfaceVersion ' 2
        End Get
    End Property

    Public ReadOnly Property Name As String Implements IDomeV2.Name
        Get
            Dim s_name As String = "Short driver name - please customise"
            TL.LogMessage("Name Get", s_name)
            'Return s_name
            Return childDome.Name
        End Get
    End Property

    Public Sub Dispose() Implements IDomeV2.Dispose
        ' Clean up the tracelogger and util objects
        TL.Enabled = False
        TL.Dispose()
        TL = Nothing
        utilities.Dispose()
        utilities = Nothing
        astroUtilities.Dispose()
        astroUtilities = Nothing
        PCStatusUpdateTimer.Dispose()
    End Sub

#End Region

#Region "IDome Implementation"
    Public Enum MirrorCoverStates
        mirrorcoverOpen = 0
        mirrorcoverClosed = 1
        mirrorcoverPartial = 2
        mirrorcoverError = 3
    End Enum

    Dim MirrorCoverStateNames() As String = {"open", "closed", "partial", "error"}
    Dim ShutterStateNames() As String = {"open", "closed", "opening", "closing", "error"}

    Private domeShutterState As Integer = ShutterState.shutterClosed ' Variable to hold the open/closed status of the shutter
    Private mirrorCoverState As Integer = MirrorCoverStates.mirrorcoverClosed
    Private childdomeShutterState As Integer = ShutterState.shutterClosed
    Private DriverID As String

    Public Sub AbortSlew() Implements IDomeV2.AbortSlew
        ' This is a mandatory parameter but we have no action to take in this simple driver
        childDome.AbortSlew()
        TL.LogMessage("AbortSlew", "Ask the child.")
    End Sub

    Public ReadOnly Property Altitude() As Double Implements IDomeV2.Altitude
        Get
            'TL.LogMessage("Altitude Get", "Not implemented")
            'Throw New ASCOM.PropertyNotImplementedException("Altitude", False)
            TL.LogMessage("Altitude Get", "Ask the child.")
            Return childDome.Altitude()
        End Get
    End Property

    Public ReadOnly Property AtHome() As Boolean Implements IDomeV2.AtHome
        Get
            'TL.LogMessage("AtHome", "Not implemented")
            'Throw New ASCOM.PropertyNotImplementedException("AtHome", False)
            TL.LogMessage("AtHome", "Ask the child.")
            Return childDome.AtHome()
        End Get
    End Property

    Public ReadOnly Property AtPark() As Boolean Implements IDomeV2.AtPark
        Get
            'TL.LogMessage("AtPark", "Not implemented")
            'Throw New ASCOM.PropertyNotImplementedException("AtPark", False)
            TL.LogMessage("AtPark", "Ask the child.")
            Return childDome.AtPark()
        End Get
    End Property

    Public ReadOnly Property Azimuth() As Double Implements IDomeV2.Azimuth
        Get
            'TL.LogMessage("Azimuth", "Not implemented")
            'Throw New ASCOM.PropertyNotImplementedException("Azimuth", False)
            TL.LogMessage("Azimuth", "Ask the child.")
            Return childDome.Azimuth()
        End Get
    End Property

    Public ReadOnly Property CanFindHome() As Boolean Implements IDomeV2.CanFindHome
        Get
            'TL.LogMessage("CanFindHome Get", False.ToString())
            'Return False
            TL.LogMessage("CanFindHome GetType", "Ask the child.")
            Return childDome.CanFindHome()
        End Get
    End Property

    Public ReadOnly Property CanPark() As Boolean Implements IDomeV2.CanPark
        Get
            'TL.LogMessage("CanPark Get", False.ToString())
            'Return False
            TL.LogMessage("CanPark GetType", "Ask the child.")
            Return childDome.CanPark()
        End Get
    End Property

    Public ReadOnly Property CanSetAltitude() As Boolean Implements IDomeV2.CanSetAltitude
        Get
            'TL.LogMessage("CanSetAltitude Get", False.ToString())
            'Return False
            TL.LogMessage("CanSetAltitude GetType", "Ask the Child.")
            Return childDome.CanSetAltitude()
        End Get
    End Property

    Public ReadOnly Property CanSetAzimuth() As Boolean Implements IDomeV2.CanSetAzimuth
        Get
            'TL.LogMessage("CanSetAzimuth Get", False.ToString())
            'Return False
            TL.LogMessage("CanSetAzimuth GetType", "Ask the child.")
            Return childDome.CanSetAzimuth()
        End Get
    End Property

    Public ReadOnly Property CanSetPark() As Boolean Implements IDomeV2.CanSetPark
        Get
            'TL.LogMessage("CanSetPark Get", False.ToString())
            'Return False
            TL.LogMessage("CanSetPark GetType", "Ask the child.")
            Return childDome.CanSetPark()
        End Get
    End Property

    Public ReadOnly Property CanSetShutter() As Boolean Implements IDomeV2.CanSetShutter
        Get
            'TL.LogMessage("CanSetShutter Get", True.ToString())
            'Return True
            TL.LogMessage("CanSetShutter GetType", "Ask the child.") ' I assume the MaxDome can do this
            Return childDome.CanSetShutter()
        End Get
    End Property

    Public ReadOnly Property CanSlave() As Boolean Implements IDomeV2.CanSlave
        Get
            'TL.LogMessage("CanSlave Get", False.ToString())
            'Return False
            TL.LogMessage("CanSlave GetType", "Ask the child.")
            Return childDome.CanSlave()
        End Get
    End Property

    Public ReadOnly Property CanSyncAzimuth() As Boolean Implements IDomeV2.CanSyncAzimuth
        Get
            'TL.LogMessage("CanSyncAzimuth Get", False.ToString())
            'Return False
            TL.LogMessage("CanSyncAzimuth Get", "Ask the child.")
            Return childDome.CanSyncAzimuth()
        End Get
    End Property

    Public Sub FindHome() Implements IDomeV2.FindHome
        'TL.LogMessage("FindHome", "Not implemented")
        'Throw New ASCOM.MethodNotImplementedException("FindHome")
        TL.LogMessage("FindHome", "Ask the child.")
        childDome.FindHome()
    End Sub

    Public Sub Park() Implements IDomeV2.Park
        'TL.LogMessage("Park", "Not implemented")
        'Throw New ASCOM.MethodNotImplementedException("Park")
        TL.LogMessage("Park", "Ask the child.")
        childDome.Park()
    End Sub

    Public Sub SetPark() Implements IDomeV2.SetPark
        'TL.LogMessage("SetPark", "Not implemented")
        'Throw New ASCOM.MethodNotImplementedException("SetPark")
        TL.LogMessage("SetPark", "Ask the child.")
        childDome.SetPark()
    End Sub

    Public Sub CloseShutter() Implements IDomeV2.CloseShutter
        TL.LogMessage("CloseShutter", "Shutter close requested: close mirror cover")
        domeShutterState = ShutterState.shutterClosing
        CommandBlind("*CLOSE_MIRROR_COVER" & vbCr)
        '' Poll the PC to move the close process forward
        'PCStatusUpdateCounter = 20
        'PCStatusUpdateTimer.Enabled = True
    End Sub

    Public Sub OpenShutter() Implements IDomeV2.OpenShutter
        TL.LogMessage("OpenShutter", "Shutter open requested; open child shutter")
        domeShutterState = ShutterState.shutterOpening
        childDome.OpenShutter()
        '' Poll the PC to move the open process forward
        'PCStatusUpdateCounter = 20
        'PCStatusUpdateTimer.Enabled = True
    End Sub

    Public Function ShutterStates() As String
        ' use in log messages
        Dim ret As String
        ret = "DS:" & ShutterStateNames(domeShutterState)
        ret &= ", CS:" & ShutterStateNames(childdomeShutterState)
        ret &= ", MS:" & MirrorCoverStateNames(mirrorCoverState)
        Return ret
    End Function

    Public Sub GetShutterStates()
        ' use this when you just want the child and mirrorcover states. A call to ShutterStatus()
        ' invokes active state changes (ie, calls to open and close)
        CommandBlind("*MIRROR_COVER_STATUS" & vbCr) ' Get shutter status
        Dim ret As String = Listener({"MIRROR COVER", "SHUT", "OPEN", "PARTLY"}) ' wait for answer
        TL.LogMessage("ShutterStatus", ShutterStates() & " response from the PC: [" & ret & "]")
        Select Case ret.Substring(0, 1)
            Case "P"
                mirrorCoverState = MirrorCoverStates.mirrorcoverPartial
            Case "O"
                mirrorCoverState = MirrorCoverStates.mirrorcoverOpen
            Case "S"
                mirrorCoverState = MirrorCoverStates.mirrorcoverClosed
            Case Else 'Case "M"
                mirrorCoverState = MirrorCoverStates.mirrorcoverError
        End Select
        childdomeShutterState = childDome.ShutterStatus()
    End Sub

    Public ReadOnly Property ShutterStatus() As ShutterState Implements IDomeV2.ShutterStatus
        Get
            'If PCStatusUpdateTimer.Enabled And Not PCStatusUpdateCalled Then
            '    Return domeShutterState ' used the value caught by the timer calls
            'End If
            ' ask for mirrorcoverState and childdome states
            GetShutterStates()
            ' now figure out where we are, what we have to do
            If childdomeShutterState = ShutterState.shutterError Or mirrorCoverState = MirrorCoverStates.mirrorcoverError Then
                domeShutterState = ShutterState.shutterError ' if there is an error out there, we are in error
                TL.LogMessage("ShutterStatus", ShutterStates() & " error!")
            Else
                If domeShutterState = ShutterState.shutterClosing Then
                    If mirrorCoverState = MirrorCoverStates.mirrorcoverClosed And childdomeShutterState = ShutterState.shutterClosed Then
                        domeShutterState = ShutterState.shutterClosed
                        TL.LogMessage("CloseShutter", "Shutter closed")
                    ElseIf mirrorCoverState = MirrorCoverStates.mirrorcoverClosed And childdomeShutterState <> ShutterState.shutterClosing Then
                        childDome.CloseShutter()
                        TL.LogMessage("CloseShutter", "Close the child shutter")
                    End If

                ElseIf domeShutterState = ShutterState.shutterOpening Then
                    If mirrorCoverState = MirrorCoverStates.mirrorcoverOpen And childdomeShutterState = ShutterState.shutterOpen Then
                        domeShutterState = ShutterState.shutterOpen
                        TL.LogMessage("OpenShutter", "Shutter open")
                    ElseIf childdomeShutterState = ShutterState.shutterOpen And mirrorCoverState <> MirrorCoverStates.mirrorcoverPartial Then
                        CommandBlind("*OPEN_MIRROR_COVER" & vbCr)
                        TL.LogMessage("OpenShutter", "Open the mirror cover")
                    End If
                End If
                '' might be in error due to timeout  not implemented
            End If
            Return domeShutterState
        End Get
    End Property

    Public Property Slaved() As Boolean Implements IDomeV2.Slaved
        Get
            'TL.LogMessage("Slaved Get", False.ToString())
            'Return False
            TL.LogMessage("Slaved Get", "Ask the child.")
            Return childDome.Slaved()
        End Get
        Set(value As Boolean)
            'TL.LogMessage("Slaved Set", "not implemented")
            'Throw New ASCOM.PropertyNotImplementedException("Slaved", True)
            TL.LogMessage("Slaved Set", "Ask the child.")
            childDome.Slaved = value
        End Set
    End Property

    Public Sub SlewToAltitude(Altitude As Double) Implements IDomeV2.SlewToAltitude
        'TL.LogMessage("SlewToAltitude", "Not implemented")
        'Throw New ASCOM.MethodNotImplementedException("SlewToAltitude")
        TL.LogMessage("SlewToAltitude", "Ask the child.")
        childDome.SlewToAltitude(Altitude)
    End Sub

    Public Sub SlewToAzimuth(Azimuth As Double) Implements IDomeV2.SlewToAzimuth
        'TL.LogMessage("SlewToAzimuth", "Not implemented")
        'Throw New ASCOM.MethodNotImplementedException("SlewToAzimuth")
        TL.LogMessage("SlewToAzimuth", "Ask the child.")
        childDome.SlewToAzimuth(Azimuth)
    End Sub

    Public ReadOnly Property Slewing() As Boolean Implements IDomeV2.Slewing
        Get
            'TL.LogMessage("Slewing Get", False.ToString())
            'Return False
            TL.LogMessage("Slewing Get", "Ask the child.")
            Return childDome.Slewing()
        End Get
    End Property

    Public Sub SyncToAzimuth(Azimuth As Double) Implements IDomeV2.SyncToAzimuth
        'TL.LogMessage("SyncToAzimuth", "Not implemented")
        'Throw New ASCOM.MethodNotImplementedException("SyncToAzimuth")
        TL.LogMessage("SyncToAzimuth", "Ask the child.")
        childDome.SyncToAzimuth(Azimuth)
    End Sub

#End Region

#Region "Private properties and methods"
    ' here are some useful properties and methods that can be used as required
    ' to help with


    ''' <summary>
    ''' Returns true if there is a valid connection to the driver hardware
    ''' </summary>
    Private ReadOnly Property IsConnected As Boolean
        Get
            ' Check that the driver hardware connection exists and is connected to the hardware
            ' childDome and the serial port need to be up.
            connectedState = True ' the hope, then check the two interfaces
            ' is the childDome connected?
            If Not childDome.Connected Then
                TL.LogMessage("IsConnected", "childDome not connected.")
                connectedState = False
            End If
            ' is the serial port to the periperal controller connected?
            If Not SharedResources.Connected Then
                TL.LogMessage("IsConnected", "Serial port to Peripheral Controller not collected.")
                connectedState = False
            End If

            Return connectedState
        End Get
    End Property

    ''' <summary>
    ''' Use this function to throw an exception if we aren't connected to the hardware
    ''' </summary>
    ''' <param name="message"></param>
    Private Sub CheckConnected(ByVal message As String)
        If Not IsConnected Then
            Throw New NotConnectedException(message)
        End If
    End Sub

    ''' <summary>
    ''' Read the device configuration from the ASCOM Profile store
    ''' </summary>
    Friend Sub ReadProfile()
        Using driverProfile As New Profile()
            driverProfile.DeviceType = "Dome"
            traceState = Convert.ToBoolean(driverProfile.GetValue(driverID, traceStateProfileName, String.Empty, traceStateDefault))
            comPort = driverProfile.GetValue(driverID, comPortProfileName, String.Empty, comPortDefault)
            childDomeId = driverProfile.GetValue(driverID, childDomeIdProfileName, String.Empty, childDomeIdDefault)
        End Using
    End Sub

    ''' <summary>
    ''' Write the device configuration to the  ASCOM  Profile store
    ''' </summary>
    Friend Sub WriteProfile()
        Using driverProfile As New Profile()
            driverProfile.DeviceType = "Dome"
            driverProfile.WriteValue(driverID, traceStateProfileName, traceState.ToString())
            driverProfile.WriteValue(driverID, comPortProfileName, comPort.ToString())
            driverProfile.WriteValue(driverID, childDomeIdProfileName, childDomeId.ToString())
        End Using

    End Sub

#End Region

End Class
