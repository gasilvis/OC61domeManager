Public Class Form1 ' Peripheral Controller Application

    Private driver As ASCOM.DriverAccess.Dome

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' load comport load: Show all available COM ports for the serial connect option
        For Each sp As String In My.Computer.Ports.SerialPortNames
            ComboBox1.Items.Add(sp)
        Next
        ComboBox1.SelectedIndex = ComboBox1.FindStringExact("COM7") ' the real one on OC61. COM21 is the PCemul
        ' connect to the upstream link
        SerialPort2.PortName = "COM31" ' this is connected to COM30
        SerialPort2.BaudRate = 9600
        SerialPort2.NewLine = vbCr
        Try
            SerialPort2.Open()
        Catch ex As System.UnauthorizedAccessException
            'TabControl1.SelectedIndex = 1 ' switch to driver mode since we are probably the upstream client
        End Try
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Windows.Forms.DialogResult.No = MessageBox.Show("This app should be left running." + vbCrLf + "Are you sure you want to exit?", "PCapp", MessageBoxButtons.YesNo) Then
            e.Cancel = True
        Else
            If IsConnected Then
                If TabControl1.SelectedIndex = 0 Then ' serial port
                    SerialPort1.Close()
                    SerialPort2.Close()
                Else ' driver
                    driver.Connected = False
                    driver.Dispose()
                    driver = Nothing
                End If
            End If
            ' the settings are saved automatically when this application is closed.
        End If
    End Sub

    ''' This event is where the driver is choosen. The device ID will be saved in the settings.
    Private Sub buttonChoose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonChoose.Click
        My.Settings.DriverId = ASCOM.DriverAccess.Dome.Choose(My.Settings.DriverId)
        SetUIState()
    End Sub

    ''' Connects to the device to be tested.
    Private Sub buttonConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonConnect.Click
        If IsConnected Then
            If Not driver Is Nothing Then
                driver.Connected = False
                driver.Dispose()
                driver = Nothing
            End If
        Else
            driver = New ASCOM.DriverAccess.Dome(My.Settings.DriverId)
            driver.Connected = True
        End If
        SetUIState()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If SerialPort1.IsOpen Then SerialPort1.Close()
        SerialPort1.PortName = ComboBox1.SelectedItem
        SerialPort1.BaudRate = 9600
        SerialPort1.NewLine = vbCr
        Try
            SerialPort1.Open()
        Catch ex As System.UnauthorizedAccessException
            'TabControl1.SelectedIndex = 1 ' switch to driver
            MessageBox.Show("This serial port is not available", "PCapp", MessageBoxButtons.OK)
        End Try

        ' protocol: receive, no prefix, suffix \r
        '           send  prefix *, suffix \r
        'SerialPort1.Write("Hello world" & vbCr)
    End Sub

    ''' Sets the state of the UI depending on the device state
    Private Sub SetUIState() ' only for Driver connection tab
        buttonConnect.Enabled = Not String.IsNullOrEmpty(My.Settings.DriverId)
        buttonChoose.Enabled = Not IsConnected
        buttonConnect.Text = IIf(IsConnected, "Disconnect", "Connect")
    End Sub

    Private Sub TabControl1_Selected(sender As Object, e As TabControlEventArgs) Handles TabControl1.Selected
        If TabControl1.SelectedIndex = 0 Then ' serial port
            If Not driver Is Nothing Then
                driver.Connected = False
                driver.Dispose()
                driver = Nothing
            End If
        Else ' driver
            SerialPort1.Close()
        End If
    End Sub

    ''' Gets a value indicating whether this instance is connected via the driver or directly to the PC
    Private ReadOnly Property IsConnected() As Boolean
        Get
            If TabControl1.SelectedIndex = 0 Then ' serial port
                If SerialPort1.IsOpen Then Return True
                Return False
            Else ' driver
                If Me.driver Is Nothing Then Return False
                Return driver.Connected
            End If
        End Get
    End Property

#Region "Button commands"
    Private Sub cmdOver_ride_HorizonSensor_Click(sender As Object, e As EventArgs) Handles cmdOver_ride_HorizonSensor.Click
        sendPCcmd("*OVER_RIDE_HORZ_LIMIT" & vbCr)
    End Sub

    Private Sub cmdGetHorizonSenseStatus_Click(sender As Object, e As EventArgs) Handles cmdGetHorizonSenseStatus.Click
        sendPCcmd("*HORIZON_STATUS" & vbCr)
    End Sub

    Private Sub cmdOpenMirrorCover_Click(sender As Object, e As EventArgs) Handles cmdOpenMirrorCover.Click
        If TabControl1.SelectedIndex = 0 Then ' serial port
            sendPCcmd("*OPEN_MIRROR_COVER" & vbCr)
        Else
            driver.OpenShutter()
        End If
    End Sub

    Private Sub cmdShutMirrorCover_Click(sender As Object, e As EventArgs) Handles cmdShutMirrorCover.Click
        If TabControl1.SelectedIndex = 0 Then ' serial port
            sendPCcmd("*CLOSE_MIRROR_COVER" & vbCr)
        Else
            driver.CloseShutter()
        End If
    End Sub

    Private Sub cmdGetMirrorCoverStatus_Click(sender As Object, e As EventArgs) Handles cmdGetMirrorCoverStatus.Click
        If TabControl1.SelectedIndex = 0 Then ' serial port
            sendPCcmd("*MIRROR_COVER_STATUS" & vbCr)
        Else
            Dim ret As Integer = driver.ShutterStatus()
            ' TODO this needs to be a function, with a return   
            ShowMsg("shutter status is " & ret & vbCrLf)
        End If
    End Sub

    Private Sub cmdClearList_Click(sender As Object, e As EventArgs) Handles cmdClearList.Click
        List1.Clear()
    End Sub

    Private Sub cmdDriveFlipMirrorIn_Click(sender As Object, e As EventArgs) Handles cmdDriveFlipMirrorIn.Click
        sendPCcmd("*FLIP_MIRROR_IN" & vbCr)
    End Sub

    Private Sub cmdDriveFlipMirrorOut_Click(sender As Object, e As EventArgs) Handles cmdDriveFlipMirrorOut.Click
        sendPCcmd("*FLIP_MIRROR_OUT" & vbCr)
    End Sub

    Private Sub cmdGetFlipMirrorStatus_Click(sender As Object, e As EventArgs) Handles cmdGetFlipMirrorStatus.Click
        sendPCcmd("*FLIP_MIRROR_STATUS" & vbCr)
    End Sub

    Private Sub cmdEnablePositionSend_Click(sender As Object, e As EventArgs) Handles cmdEnablePositionSend.Click
        sendPCcmd("*POSITION_SEND_ON" & vbCr)
    End Sub

    Private Sub cmdDisablePositionSend_Click(sender As Object, e As EventArgs) Handles cmdDisablePositionSend.Click
        sendPCcmd("*POSITION_SEND_OFF" & vbCr)
    End Sub

    Private Sub cmdGetPositions_Click(sender As Object, e As EventArgs) Handles cmdGetPositions.Click
        If TabControl1.SelectedIndex = 0 Then ' serial port
            sendPCcmd("*SEND_POSITION" & vbCr)
        Else 'driver
            ShowMsg("asking for HA&Dec" & vbCrLf)
            Dim ret As String = driver.Action("ScopePosition", "")
            ShowMsg("spcPC: " & ret & vbCrLf)
        End If
    End Sub

    Private Sub cmdGetPot_ADU_Values_Click(sender As Object, e As EventArgs) Handles cmdGetPot_ADU_Values.Click
        If TabControl1.SelectedIndex = 0 Then ' serial port
            sendPCcmd("*SEND_ADC_VALS" & vbCr)
        Else ' driver
            ShowMsg("asking for ADC values" & vbCrLf)
            Dim ret As String = driver.Action("ADCvalues", "")
            ShowMsg("spcPC: " & ret & vbCrLf)
        End If
    End Sub

    Private Sub cmdGetHA_Pot_ZeroRef_Click(sender As Object, e As EventArgs) Handles cmdGetHA_Pot_ZeroRef.Click
        If TabControl1.SelectedIndex = 0 Then ' serial port
            sendPCcmd("*SEND_HA_ZERO_REF_VAL" & vbCr)
        Else 'driver
            ShowMsg("asking for HA Pot zeroref" & vbCrLf)
            Dim ret As String = driver.Action("PotParms", "SEND_HA_ZERO_REF_VAL")
            ShowMsg("spcPC: " & ret & vbCrLf)
            txtHA_Pot_ZeroRef.Text = Mid(ret, 18)

            'ElseIf cmd.Substring(0, 21) = "HA SCALE FACTOR VAL: " Then
            'txtHA_Pot_ScaleFactor.Text = Mid(cmd, 22)
            'ElseIf cmd.Substring(0, 18) = "DEC ZERO REF VAL: " Then
            'txtDEC_Pot_ZeroRef.Text = Mid(cmd, 19)
            'ElseIf cmd.Substring(0, 22) = "DEC SCALE FACTOR VAL: " Then
            'txtDEC_Pot_ScaleFactor.Text = Mid(cmd, 23)
        End If
    End Sub

    Private Sub cmdSetHA_Pot_ZeroRef_Click(sender As Object, e As EventArgs) Handles cmdSetHA_Pot_ZeroRef.Click
        If txtHA_Pot_ZeroRef.Text = "" Then Exit Sub
        If Val(txtHA_Pot_ZeroRef.Text) = 0 Then Exit Sub
        sendPCcmd("*SET_HA_ZERO_REF_VAL" & txtHA_Pot_ZeroRef.Text.Trim() & vbCr)
    End Sub

    Private Sub cmdGetDEC_Pot_ZeroRef_Click(sender As Object, e As EventArgs) Handles cmdGetDEC_Pot_ZeroRef.Click
        sendPCcmd("*SEND_DEC_ZERO_REF_VAL" & vbCr)
    End Sub

    Private Sub cmdSetDEC_Pot_ZeroRef_Click(sender As Object, e As EventArgs) Handles cmdSetDEC_Pot_ZeroRef.Click
        If txtDEC_Pot_ZeroRef.Text = "" Then Exit Sub
        If Val(txtDEC_Pot_ZeroRef.Text) = 0 Then Exit Sub
        sendPCcmd("*SET_DEC_ZERO_REF_VAL" & txtDEC_Pot_ZeroRef.Text.Trim() & vbCr)
    End Sub

    Private Sub cmdGetHA_Pot_ScaleFactor_Click(sender As Object, e As EventArgs) Handles cmdGetHA_Pot_ScaleFactor.Click
        sendPCcmd("*SEND_HA_SCALE_FACTOR" & vbCr)
    End Sub

    Private Sub cmdSetHA_Pot_ScaleFactor_Click(sender As Object, e As EventArgs) Handles cmdSetHA_Pot_ScaleFactor.Click
        If txtHA_Pot_ScaleFactor.Text = "" Then Exit Sub
        If Val(txtHA_Pot_ScaleFactor.Text) = 0 Then Exit Sub
        sendPCcmd("*SET_HA_SCALE_FACTOR" & txtHA_Pot_ScaleFactor.Text.Trim() & vbCr)
    End Sub

    Private Sub cmdGetDEC_Pot_ScaleFactor_Click(sender As Object, e As EventArgs) Handles cmdGetDEC_Pot_ScaleFactor.Click
        sendPCcmd("*SEND_DEC_SCALE_FACTOR" & vbCr)
    End Sub

    Private Sub cmdSetDEC_Pot_ScaleFactor_Click(sender As Object, e As EventArgs) Handles cmdSetDEC_Pot_ScaleFactor.Click
        If txtDEC_Pot_ScaleFactor.Text = "" Then Exit Sub
        If Val(txtDEC_Pot_ScaleFactor.Text) = 0 Then Exit Sub
        sendPCcmd("*SET_DEC_SCALE_FACTOR" & txtDEC_Pot_ScaleFactor.Text.Trim() & vbCr)
    End Sub

    Private Sub cmdSync_HA_Pot_Click(sender As Object, e As EventArgs) Handles cmdSync_HA_Pot.Click
        sendPCcmd("*SAVE_HA_POT_ZERO_REF" & vbCr)
    End Sub

    Private Sub cmdSync_DEC_Pot_Click(sender As Object, e As EventArgs) Handles cmdSync_DEC_Pot.Click
        sendPCcmd("*SAVE_DEC_POT_ZERO_REF" & vbCr)
    End Sub

    Private Sub cmdFind_HA_Opto_Click(sender As Object, e As EventArgs) Handles cmdFind_HA_Opto.Click
        sendPCcmd("*FIND_HA_OPTO" & vbCr)
    End Sub

    Private Sub cmdFind_DEC_Opto_Click(sender As Object, e As EventArgs) Handles cmdFind_DEC_Opto.Click
        sendPCcmd("*FIND_DEC_OPTO" & vbCr)
    End Sub

#End Region
    Private Sub ShowMsg(msg As String)
        List1.AppendText(msg)
        ' append to log file, local time stamp
        If My.Computer.FileSystem.DirectoryExists("C:\Users\oc61\Documents\ACP Astronomy\Logs") Then
            My.Computer.FileSystem.WriteAllText("C:\Users\oc61\Documents\ACP Astronomy\Logs\PClog.txt", DateTime.Now.ToString("s") + " " + msg, True)
        Else
            My.Computer.FileSystem.WriteAllText("C:\Data\PClog.txt", DateTime.Now.ToString("s") + " " + msg, True)
        End If
        ' eg   2020-03-17T11:19:04 fromPC: HA+004 DEC-005
    End Sub

    ' from UI buttons (or..) to PC
    Private Sub sendPCcmd(cmd As String)
        If TabControl1.SelectedIndex = 0 Then ' serial port
            If SerialPort1.IsOpen Then
                ShowMsg("toPC: " & cmd & vbLf) ' show what we are sending to the PC
                SerialPort1.Write(cmd)
            Else
                MessageBox.Show("Select a serial port to the PC first")
            End If
        Else ' driver
            ShowMsg("toPC: " & cmd & vbLf) ' show what we are sending to the PC
            Call driver.CommandBlind(cmd, False) ' send it to the driver blind: don't wait for response
        End If
    End Sub

    Private Const dLAT As Double = -43.9856 '-43:59:08
    Private Const dLONG As Double = 170.465 ' 170:27:54  East   (Mashnee is -70.633 )

    Private Function SiderealTime() As Double
        ' local sidereal time now, here, in degrees
        ' https://www.aa.quae.nl/en/reken/sterrentijd.html#1
        'θ≡L0+L1ΔJ+L2ΔJ2+L3ΔJ3−lw(mod360°)
        Dim L0 As Double = 99.967794687 '°
        Dim L1 As Double = 360.985647366286 '°
        Dim L2 As Double = 0.0000000000002907879 '°
        Dim L3 As Double = -5.302E-22 '°
        Dim lw As Double = -dLONG
        Dim jdts As TimeSpan = DateTime.UtcNow.Subtract(New DateTime(2000, 1, 1))
        Dim jd As Double = jdts.TotalDays
        Dim sid As Double = ((L0 + (L1 + (L2 + L3 * jd) * jd) * jd) - lw) Mod 360
        Return sid

    End Function

    ' Answer back from PC
    Private Sub processPCmsg(cmd As String)
        ' maintain states
        ' augmented too
        ' pass on to com31
        ShowMsg("fromPC: " & cmd & vbCrLf) ' display msg to list
        SerialPort2.Write(cmd & vbCr) ' send upstream 
        ' secondary messages for upstream? Or handling for the gui
        If (cmd.Substring(0, 2) = "HA" And cmd.Length = 14) Then ' eg  'HA+012 DEC-015'
            ' pass on, but send a second, better, signal with Dec corrected and Alt and Az
            Dim dHA As Double = CDbl(cmd.Substring(2, 4))
            Dim dDec As Double = CDbl(cmd.Substring(10, 4))
            ' correct dec to 90 to -90
            If dDec > 90 Then
                dDec = 180 - dDec
            ElseIf dDec < -90 Then
                dDec = (-180) - dDec
            End If
            'Dim toRads As Double = Math.Asin(1) / 90 ' pi / 180
            'Dim DEC As Double = dDec * toRads
            'Dim LAT As Double = dLAT * toRads
            'Dim HA As Double = dHA * toRads
            'Dim ALT = Math.Asin(Math.Sin(DEC) * Math.Sin(LAT) + Math.Cos(DEC) * Math.Cos(LAT) * Math.Cos(HA))
            'Dim AZ = Math.Acos((Math.Sin(DEC) - Math.Sin(ALT) * Math.Sin(LAT)) / (Math.Cos(ALT) * Math.Cos(LAT)))
            ''If Math.Sin(HA) < 0 Then AZ = (360 * toRads) - AZ ' this makes AZ==0 be South
            'Dim dALT As Double = ALT / toRads
            'Dim dAZ As Double = AZ / toRads

            ' https://idlastro.gsfc.nasa.gov/ftp/pro/astro/hadec2altaz.pro
            Dim d2r As Double = Math.Asin(1) / 90 ' pi/180     
            Dim sh As Double = Math.Sin(dHA * d2r)
            Dim ch As Double = Math.Cos(dHA * d2r)
            Dim sd As Double = Math.Sin(dDec * d2r)
            Dim cd As Double = Math.Cos(dDec * d2r)
            Dim sl As Double = Math.Sin(dLAT * d2r)
            Dim cl As Double = Math.Cos(dLAT * d2r)

            Dim x As Double = -ch * cd * sl + sd * cl
            Dim y As Double = -sh * cd
            Dim z As Double = ch * cd * cl + sd * sl
            Dim r As Double = Math.Sqrt(x ^ 2 + y ^ 2)
            'now get Alt, Az
            Dim dAZ As Double = Math.Atan(y / x) / d2r
            Dim dALT As Double = Math.Atan(z / r) / d2r
            ' correct for negative AZ
            If dAZ < 0 Then dAZ += 360
            'Dim dLST = Date.Now.ToUniversalTime().Add(TimeSpan.FromHours(dLAT / 15D)) ' toSiderealTime
            Dim dLST As Double = SiderealTime()
            Dim s As String
            s = String.Format("True HA{0:+000.0;-000.0}", dHA) ' to allow -000
            cmd = s 's.Substring(0, s.Length - 2) ' clip tail
            s = String.Format(" DEC{0:+000.0;-000.0}", dDec)
            cmd &= s 's.Substring(0, s.Length - 2)
            'ShowMsg("fromPC: " & cmd & vbCrLf) ' display msg to list
            'SerialPort2.Write(cmd & vbCr) ' send upstream 
            s = String.Format(" ALT{0:+000.0;-000.0}", dALT)
            cmd &= s 's.Substring(0, s.Length - 2)
            If Double.IsNaN(dAZ) Then dAZ = 0 ' not sure why this happens
            s = String.Format(" AZ{0:+000.0;-000.0}", dAZ)
            cmd &= s 's.Substring(0, s.Length - 2)
            'ShowMsg("fromPC: " & cmd & vbCrLf) ' display msg to list
            'SerialPort2.Write(cmd & vbCr) ' send upstream 
            s = String.Format(" LST{0:+000.0;-000.0}", dLST)
            cmd &= s 's.Substring(0, s.Length - 2)
            s = String.Format(" RA{0:+000.0;-000.0}", dLST - dHA)
            cmd &= s 's.Substring(0, s.Length - 2)
            ShowMsg("fromPC: " & cmd & vbCrLf) ' display msg to list
            SerialPort2.Write(cmd & vbCr) ' send upstream 
        ElseIf cmd.Length > 18 AndAlso cmd.Substring(0, 17) = "HA ZERO REF VAL: " Then
            txtHA_Pot_ZeroRef.Text = Mid(cmd, 18)
        ElseIf cmd.Length > 22 AndAlso cmd.Substring(0, 21) = "HA SCALE FACTOR VAL: " Then
            txtHA_Pot_ScaleFactor.Text = Mid(cmd, 22)
        ElseIf cmd.Length > 19 AndAlso cmd.Substring(0, 18) = "DEC ZERO REF VAL: " Then
            txtDEC_Pot_ZeroRef.Text = Mid(cmd, 19)
        ElseIf cmd.Length > 23 AndAlso cmd.Substring(0, 22) = "DEC SCALE FACTOR VAL: " Then
                txtDEC_Pot_ScaleFactor.Text = Mid(cmd, 23)
        End If
    End Sub

    ' data back from the PC
    Private Sub SerialPort1_DataReceived(sender As Object, e As IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        Dim indata As String
        Try
            indata = SerialPort1.ReadLine() ' waits here for full line, ending in \r
            Me.Invoke(Sub() processPCmsg(indata)) ' because we are in a thread
        Catch
        End Try
    End Sub


    ' from upstream (the driver)
    Private Sub SerialPort2_DataReceived(sender As Object, e As IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort2.DataReceived
        Dim indata As String
        Try
            indata = SerialPort2.ReadLine() ' waits here for full line, ending in \r
            ' pass it on to the PC
            Me.Invoke(Sub() sendPCcmd(indata & vbCr)) ' invoke because we are in a thread
            ' TODO any special processing? Question answering?
        Catch
        End Try
    End Sub


End Class
