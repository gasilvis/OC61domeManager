VERSION 5.00
Object = "{648A5603-2C6E-101B-82B6-000000000014}#1.1#0"; "MSCOMM32.OCX"
Begin VB.Form frm_OC_Telescope_Peripheral_Control 
   Caption         =   "OC Telescope Peripheral Controller Setup Program"
   ClientHeight    =   13830
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   17625
   LinkTopic       =   "Form1"
   ScaleHeight     =   13830
   ScaleWidth      =   17625
   StartUpPosition =   3  'Windows Default
   Begin VB.Frame Frame6 
      Caption         =   "Horizon Sensor"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   2535
      Left            =   14400
      TabIndex        =   39
      Top             =   5520
      Width           =   2415
      Begin VB.CommandButton cmdOver_ride_HorizonSensor 
         Caption         =   "Over-ride horizon sensor (5 min)"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   795
         Left            =   120
         TabIndex        =   41
         Top             =   1440
         Width           =   2175
      End
      Begin VB.CommandButton cmdGetHorizonSenseStatus 
         Caption         =   "Get Horizon Sense Status"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   795
         Left            =   120
         TabIndex        =   40
         Top             =   360
         Width           =   2175
      End
   End
   Begin VB.CommandButton cmdClearList 
      Caption         =   "Clear the List"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   5400
      TabIndex        =   38
      Top             =   13200
      Width           =   1335
   End
   Begin VB.Frame Frame5 
      Caption         =   "Find Optos"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   2535
      Left            =   14400
      TabIndex        =   35
      Top             =   2400
      Width           =   2415
      Begin VB.CommandButton cmdFind_HA_Opto 
         Caption         =   "Find HA Opto"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   795
         Left            =   120
         TabIndex        =   37
         Top             =   360
         Width           =   2175
      End
      Begin VB.CommandButton cmdFind_DEC_Opto 
         Caption         =   "Find DEC Opto"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   795
         Left            =   120
         TabIndex        =   36
         Top             =   1440
         Width           =   2175
      End
   End
   Begin VB.Frame Frame4 
      Caption         =   "Position Pot Settings"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   11175
      Left            =   9240
      TabIndex        =   19
      Top             =   2400
      Width           =   4455
      Begin VB.CommandButton cmdSync_DEC_Pot 
         Caption         =   "Sync DEC Pot Zero Ref To Opto DEC Opto"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   795
         Left            =   120
         TabIndex        =   34
         Top             =   10320
         Width           =   2175
      End
      Begin VB.CommandButton cmdSync_HA_Pot 
         Caption         =   "Sync HA Pot Zero Ref To Opto HA Opto"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   795
         Left            =   120
         TabIndex        =   33
         Top             =   9480
         Width           =   2175
      End
      Begin VB.CommandButton cmdGetDEC_Pot_ScaleFactor 
         Caption         =   "Get DEC Pot Scale Factor"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   795
         Left            =   120
         TabIndex        =   32
         Top             =   8400
         Width           =   2175
      End
      Begin VB.CommandButton cmdGetHA_Pot_ScaleFactor 
         Caption         =   "Get HA Pot Scale Factor"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   795
         Left            =   120
         TabIndex        =   31
         Top             =   7560
         Width           =   2175
      End
      Begin VB.TextBox txtDEC_Pot_ScaleFactor 
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   12
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   495
         Left            =   2640
         TabIndex        =   30
         Top             =   6480
         Width           =   1335
      End
      Begin VB.TextBox txtHA_Pot_ScaleFactor 
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   12
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   495
         Left            =   2640
         TabIndex        =   29
         Top             =   5640
         Width           =   1335
      End
      Begin VB.CommandButton cmdSetDEC_Pot_ScaleFactor 
         Caption         =   "Set DEC Pot Scale Factor"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   795
         Left            =   120
         TabIndex        =   28
         Top             =   6360
         Width           =   2175
      End
      Begin VB.CommandButton cmdSetHA_Pot_ScaleFactor 
         Caption         =   "Set HA Pot Scale Factor"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   795
         Left            =   120
         TabIndex        =   27
         Top             =   5520
         Width           =   2175
      End
      Begin VB.CommandButton cmdGetDEC_Pot_ZeroRef 
         Caption         =   "Get DEC Pot Zero Reference Value"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   795
         Left            =   120
         TabIndex        =   26
         Top             =   4320
         Width           =   2175
      End
      Begin VB.CommandButton cmdGetHA_Pot_ZeroRef 
         Caption         =   "Get HA Pot Zero Reference Value"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   795
         Left            =   120
         TabIndex        =   25
         Top             =   3480
         Width           =   2175
      End
      Begin VB.TextBox txtDEC_Pot_ZeroRef 
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   12
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   495
         Left            =   2640
         TabIndex        =   24
         Top             =   2400
         Width           =   1335
      End
      Begin VB.TextBox txtHA_Pot_ZeroRef 
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   12
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   495
         Left            =   2640
         TabIndex        =   23
         Top             =   1560
         Width           =   1335
      End
      Begin VB.CommandButton cmdSetDEC_Pot_ZeroRef 
         Caption         =   "Set DEC Pot Zero Reference Value"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   795
         Left            =   120
         TabIndex        =   22
         Top             =   2280
         Width           =   2175
      End
      Begin VB.CommandButton cmdGetPot_ADU_Values 
         Caption         =   "Get Pot ADU values"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   795
         Left            =   120
         TabIndex        =   21
         Top             =   360
         Width           =   2175
      End
      Begin VB.CommandButton cmdSetHA_Pot_ZeroRef 
         Caption         =   "Set HA Pot Zero Reference Value"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   795
         Left            =   120
         TabIndex        =   20
         Top             =   1440
         Width           =   2175
      End
   End
   Begin VB.Frame Frame1 
      Caption         =   "Position String"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   3015
      Left            =   5280
      TabIndex        =   15
      Top             =   9720
      Width           =   2415
      Begin VB.CommandButton cmdGetPositions 
         Caption         =   "Get HA and DEC positions"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   795
         Left            =   120
         TabIndex        =   18
         Top             =   2040
         Width           =   2175
      End
      Begin VB.CommandButton cmdEnablePositionSend 
         Caption         =   "Enable Position Sending"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   795
         Left            =   120
         TabIndex        =   17
         Top             =   360
         Width           =   2175
      End
      Begin VB.CommandButton cmdDisablePositionSend 
         Caption         =   "Disable Position Sending"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   795
         Left            =   120
         TabIndex        =   16
         Top             =   1200
         Width           =   2175
      End
   End
   Begin VB.CommandButton cmdCheckForHardwarePresent 
      Caption         =   "Check for Controller Connected"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   555
      Left            =   10680
      TabIndex        =   14
      Top             =   1200
      Width           =   3015
   End
   Begin VB.Frame Frame2 
      Caption         =   "Mirror Cover"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   3015
      Left            =   5280
      TabIndex        =   10
      Top             =   6000
      Width           =   2415
      Begin VB.CommandButton cmdShutMirrorCover 
         Caption         =   "Shut Mirror Cover"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   795
         Left            =   120
         TabIndex        =   13
         Top             =   1200
         Width           =   2175
      End
      Begin VB.CommandButton cmdOpenMirrorCover 
         Caption         =   "Open Mirror Cover"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   795
         Left            =   120
         TabIndex        =   12
         Top             =   360
         Width           =   2175
      End
      Begin VB.CommandButton cmdGetMirrorCoverStatus 
         Caption         =   "Get Mirror Cover Status"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   795
         Left            =   120
         TabIndex        =   11
         Top             =   2040
         Width           =   2175
      End
   End
   Begin VB.Frame Frame3 
      Caption         =   "Flip Mirror"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   3015
      Left            =   5280
      TabIndex        =   6
      Top             =   2400
      Width           =   2415
      Begin VB.CommandButton cmdDriveFlipMirrorOut 
         Caption         =   "Drive Flip Mirror OUT"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   795
         Left            =   120
         TabIndex        =   9
         Top             =   1200
         Width           =   2175
      End
      Begin VB.CommandButton cmdDriveFlipMirrorIn 
         Caption         =   "Drive Flip Mirror IN"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   795
         Left            =   120
         TabIndex        =   8
         Top             =   360
         Width           =   2175
      End
      Begin VB.CommandButton cmdGetFlipMirrorStatus 
         Caption         =   "Get Flip Mirror Status"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   795
         Left            =   120
         TabIndex        =   7
         Top             =   2040
         Width           =   2175
      End
   End
   Begin MSCommLib.MSComm MSComm1 
      Left            =   9720
      Top             =   720
      _ExtentX        =   1005
      _ExtentY        =   1005
      _Version        =   393216
      DTREnable       =   -1  'True
      InputLen        =   1
      RThreshold      =   1
      SThreshold      =   1
   End
   Begin VB.Timer timer_HardwareDetect 
      Enabled         =   0   'False
      Interval        =   50
      Left            =   9120
      Top             =   720
   End
   Begin VB.ListBox List1 
      Height          =   11175
      Left            =   120
      TabIndex        =   0
      Top             =   2400
      Width           =   4935
   End
   Begin VB.Label Label4 
      Caption         =   "List of all data sent from the Peripheral Control Box"
      Height          =   375
      Left            =   120
      TabIndex        =   5
      Top             =   2040
      Width           =   4215
   End
   Begin VB.Label Label3 
      BackColor       =   &H80000009&
      Caption         =   $"OC_Telescope_Peripheral_Control_Test_Program.frx":0000
      Height          =   375
      Left            =   240
      TabIndex        =   4
      Top             =   960
      Width           =   7575
   End
   Begin VB.Label Label1 
      BackColor       =   &H80000009&
      Caption         =   "This program is designed to interface with the OC Telescope Peripheral Control Box"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   9.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   240
      TabIndex        =   3
      Top             =   360
      Width           =   7575
   End
   Begin VB.Label Label2 
      BackStyle       =   0  'Transparent
      Caption         =   "Comms connection status"
      Height          =   255
      Left            =   10680
      TabIndex        =   2
      Top             =   360
      Width           =   2415
   End
   Begin VB.Label lblHardwareDetected 
      Alignment       =   2  'Center
      BackColor       =   &H80000009&
      Height          =   375
      Left            =   10680
      TabIndex        =   1
      Top             =   600
      Width           =   3015
   End
End
Attribute VB_Name = "frm_OC_Telescope_Peripheral_Control"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'This is the code for the OC Telescope to test and setup the Peripheral Controller electronics program.
'Written Jan 2013
'Geoff Graham

Option Explicit

'Global variables
Private ResponseString As String
Private CommPortNumber As Integer
Private HardwareAttached As Boolean, HardwareDetected As Boolean




'This is the subroutine that is activated when the button "Get Data from Logger" is clicked.
'When the button "Get Data from Logger" is clicked, a "$" character is sent to the logger
'to tell it to send its data.

Private Sub cmdCheckForHardwarePresent_Click()

 On Error GoTo Failed

 MSComm1.Output = "*" & vbCr    'Send the "*" character to check for a "hardware present" response. This will return the "*" character
 Exit Sub
 
Failed:
 MsgBox "Failed to find the interface box on any comm port"

End Sub




Private Sub cmdClearList_Click()

 List1.Clear

End Sub

Private Sub cmdDriveFlipMirrorIn_Click()

 MSComm1.Output = "*FLIP_MIRROR_IN" & vbCr

End Sub

Private Sub cmdDriveFlipMirrorOut_Click()

 MSComm1.Output = "*FLIP_MIRROR_OUT" & vbCr

End Sub

Private Sub cmdEnablePositionSend_Click()

 MSComm1.Output = "*POSITION_SEND_ON" & vbCr

End Sub

Private Sub cmdDisablePositionSend_Click()

 MSComm1.Output = "*POSITION_SEND_OFF" & vbCr

End Sub

Private Sub cmdFind_HA_Opto_Click()

 MSComm1.Output = "*FIND_HA_OPTO" & vbCr

End Sub

Private Sub cmdFind_DEC_Opto_Click()

 MSComm1.Output = "*FIND_DEC_OPTO" & vbCr

End Sub

Private Sub cmdGetFlipMirrorStatus_Click()

 MSComm1.Output = "*FLIP_MIRROR_STATUS" & vbCr

End Sub

Private Sub cmdGetHA_Pot_ScaleFactor_Click()

 MSComm1.Output = "*SEND_HA_SCALE_FACTOR" & vbCr

End Sub

Private Sub cmdGetDEC_Pot_ScaleFactor_Click()

 MSComm1.Output = "*SEND_DEC_SCALE_FACTOR" & vbCr

End Sub

Private Sub cmdGetHA_Pot_ZeroRef_Click()

 MSComm1.Output = "*SEND_HA_ZERO_REF_VAL" & vbCr

End Sub

Private Sub cmdGetDEC_Pot_ZeroRef_Click()

 MSComm1.Output = "*SEND_DEC_ZERO_REF_VAL" & vbCr

End Sub

Private Sub cmdGetHorizonSenseStatus_Click()

 MSComm1.Output = "*HORIZON_STATUS" & vbCr

End Sub

Private Sub cmdGetMirrorCoverStatus_Click()

 MSComm1.Output = "*MIRROR_COVER_STATUS" & vbCr

End Sub

Private Sub cmdGetPositions_Click()

 MSComm1.Output = "*SEND_POSITION" & vbCr

End Sub

Private Sub cmdGetPot_ADU_Values_Click()

 MSComm1.Output = "*SEND_ADC_VALS" & vbCr

End Sub

Private Sub cmdOpenMirrorCover_Click()

 MSComm1.Output = "*OPEN_MIRROR_COVER" & vbCr

End Sub

Private Sub cmdOver_ride_HorizonSensor_Click()

 MSComm1.Output = "*OVER_RIDE_HORZ_LIMIT" & vbCr

End Sub

Private Sub cmdSetHA_Pot_ScaleFactor_Click()

 If txtHA_Pot_ScaleFactor = "" Then Exit Sub
 
 If Val(txtHA_Pot_ScaleFactor) = 0 Then Exit Sub

 MSComm1.Output = "*SET_HA_SCALE_FACTOR" & txtHA_Pot_ScaleFactor & vbCr

End Sub

Private Sub cmdSetDEC_Pot_ScaleFactor_Click()

 If txtDEC_Pot_ScaleFactor = "" Then Exit Sub
 
 If Val(txtDEC_Pot_ScaleFactor) = 0 Then Exit Sub

 MSComm1.Output = "*SET_DEC_SCALE_FACTOR" & txtDEC_Pot_ScaleFactor & vbCr

End Sub

Private Sub cmdSetHA_Pot_ZeroRef_Click()

 If txtHA_Pot_ZeroRef = "" Then Exit Sub
 
 If Val(txtHA_Pot_ZeroRef) = 0 Then Exit Sub

 MSComm1.Output = "*SET_HA_ZERO_REF_VAL" & txtHA_Pot_ZeroRef & vbCr

End Sub

Private Sub cmdSetDEC_Pot_ZeroRef_Click()

 If txtDEC_Pot_ZeroRef = "" Then Exit Sub
 
 If Val(txtDEC_Pot_ZeroRef) = 0 Then Exit Sub

 MSComm1.Output = "*SET_DEC_ZERO_REF_VAL" & txtDEC_Pot_ZeroRef & vbCr

End Sub

Private Sub cmdShutMirrorCover_Click()

 MSComm1.Output = "*CLOSE_MIRROR_COVER" & vbCr

End Sub

Private Sub cmdSync_HA_Pot_Click()

 MSComm1.Output = "*SAVE_HA_POT_ZERO_REF" & vbCr

End Sub

Private Sub cmdSync_DEC_Pot_Click()

 MSComm1.Output = "*SAVE_DEC_POT_ZERO_REF" & vbCr

End Sub



Private Sub Form_Load()

 'This is the first code run when the progran starts
 'The next two lines position the form at the top left of the screen
 
 frm_OC_Telescope_Peripheral_Control.Top = 0
 frm_OC_Telescope_Peripheral_Control.Left = 0
 
 'Next we find the comport with the logger attached
 OpenAvailableCommPort

End Sub

'This subroutine tests to see which comm ports are available on the PC, then it sends a "*" character to each available comm port
'If it receives a "*" character in response, then it assumes that the logger hardware has been detected so it writes a message indicating
'which comm port it found the logger on. The system is then ready to get the data from the logger. The data will be down loaded from the logger when
'the front panel button "Get Data from Logger" is clicked. When the button "Get Data from Logger" is clicked, a "$" character is sent to the logger
'to tell it to send its data.

Private Sub OpenAvailableCommPort()

 Dim NumberOfPortsFound As Integer, i As Integer
 Dim AvailableComPortsArray(0 To 20) As Integer
 Dim CommPortNumberString As String


 For CommPortNumber = 1 To 20
  On Error Resume Next
  MSComm1.CommPort = CommPortNumber
  
  On Error Resume Next
  MSComm1.PortOpen = True
  On Error Resume Next
  MSComm1.PortOpen = False
  
  If Err.Number = 0 Then
   AvailableComPortsArray(NumberOfPortsFound) = CommPortNumber
   NumberOfPortsFound = NumberOfPortsFound + 1
  End If
 Next CommPortNumber
 
 

 For i = 0 To NumberOfPortsFound - 1
  CommPortNumber = AvailableComPortsArray(i)
  On Error Resume Next
  MSComm1.CommPort = CommPortNumber
  On Error Resume Next
  MSComm1.PortOpen = True
  On Error Resume Next
  MSComm1.PortOpen = True
  
  HardwareDetected = False
  MSComm1.Output = "*" & vbCr              'Send the string to detect the hardware attached to the comm port
  Delay (0.05)
  
  If HardwareDetected Then
   lblHardwareDetected.ForeColor = vbBlue
   lblHardwareDetected = "Periphal Controller detected on Comm:" & CommPortNumber
   Exit Sub
  Else
   lblHardwareDetected.ForeColor = vbRed
   lblHardwareDetected = "Periphal Controller  NOT Detected"
   MSComm1.PortOpen = False
  End If
  
CheckNextCommPort:
 On Error GoTo 0   'This cancels the error trapping
 
 Next i
 
 

End Sub


Private Sub Delay(DelayTime As Single)

 Dim TimeOutVal As Single
 
 TimeOutVal = Timer + DelayTime
 
 Do
  DoEvents
 Loop Until Timer > TimeOutVal
 

End Sub

'This is the Comms routine that receives the characters sent from the logger
'This routine is entered every time a character is received from the comm port

Private Sub MSComm1_OnComm()

 Dim InputChar As String
 
 
 InputChar = MSComm1.Input   'Copy the character from the UART buffer to a local variable
 
 If InputChar = "" Then Exit Sub     'Test the character, if null then exit
 If InputChar = vbLf Then Exit Sub   'Test the character, if Line Feed then exit
 
 If InputChar = "$" Then             'Test the character, if "$" then set "HardwareDetected" true then exit
  HardwareDetected = True
 End If
 
 
 If InputChar = vbCr Then            'Test the character, if Carriage Return then
  List1.AddItem ResponseString       'add the ResponseString to the list
  ResponseString = ""                'Clear the ResponseString
  Exit Sub                           'exit
 Else
  ResponseString = ResponseString & InputChar  'Otherwise the character must be a number charcter to be added to the string
 End If
 
 
End Sub


