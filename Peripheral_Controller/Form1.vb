Public Class Form1

    Private driver As ASCOM.DriverAccess.Dome

    ''' <summary>
    ''' This event is where the driver is choosen. The device ID will be saved in the settings.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub buttonChoose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonChoose.Click
        My.Settings.DriverId = ASCOM.DriverAccess.Dome.Choose(My.Settings.DriverId)
        SetUIState()
    End Sub

    ''' <summary>
    ''' Connects to the device to be tested.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub buttonConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonConnect.Click
        If (IsConnected) Then
            driver.Connected = False
        Else
            driver = New ASCOM.DriverAccess.Dome(My.Settings.DriverId)
            driver.Connected = True
        End If
        SetUIState()
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If IsConnected Then
            driver.Connected = False
        End If
        ' the settings are saved automatically when this application is closed.
    End Sub

    ''' <summary>
    ''' Sets the state of the UI depending on the device state
    ''' </summary>
    Private Sub SetUIState()
        buttonConnect.Enabled = Not String.IsNullOrEmpty(My.Settings.DriverId)
        buttonChoose.Enabled = Not IsConnected
        buttonConnect.Text = IIf(IsConnected, "Disconnect", "Connect")
    End Sub

    ''' <summary>
    ''' Gets a value indicating whether this instance is connected.
    ''' </summary>
    ''' <value>
    ''' 
    ''' <c>true</c> if this instance is connected; otherwise, <c>false</c>.
    ''' 
    ''' </value>
    Private ReadOnly Property IsConnected() As Boolean
        Get
            If Me.driver Is Nothing Then Return False
            Return driver.Connected
        End Get
    End Property

    Private Sub cmdOver_ride_HorizonSensor_Click(sender As Object, e As EventArgs) Handles cmdOver_ride_HorizonSensor.Click
        sendPCcmd("*OVER_RIDE_HORZ_LIMIT" & vbCr)
    End Sub

    Private Sub cmdGetHorizonSenseStatus_Click(sender As Object, e As EventArgs) Handles cmdGetHorizonSenseStatus.Click
        sendPCcmd("*HORIZON_STATUS" & vbCr)
    End Sub

    Private Sub cmdOpenMirrorCover_Click(sender As Object, e As EventArgs) Handles cmdOpenMirrorCover.Click
        sendPCcmd("*OPEN_MIRROR_COVER" & vbCr)
    End Sub

    Private Sub cmdShutMirrorCover_Click(sender As Object, e As EventArgs) Handles cmdShutMirrorCover.Click
        sendPCcmd("*CLOSE_MIRROR_COVER" & vbCr)
    End Sub

    Private Sub cmdGetMirrorCoverStatus_Click(sender As Object, e As EventArgs) Handles cmdGetMirrorCoverStatus.Click
        sendPCcmd("*MIRROR_COVER_STATUS" & vbCr)
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
        sendPCcmd("*SEND_POSITION" & vbCr)
    End Sub

    Private Sub cmdGetPot_ADU_Values_Click(sender As Object, e As EventArgs) Handles cmdGetPot_ADU_Values.Click
        sendPCcmd("*SEND_ADC_VALS" & vbCr)
    End Sub

    Private Sub cmdGetHA_Pot_ZeroRef_Click(sender As Object, e As EventArgs) Handles cmdGetHA_Pot_ZeroRef.Click
        sendPCcmd("*SEND_HA_ZERO_REF_VAL" & vbCr)
    End Sub

    Private Sub cmdSetHA_Pot_ZeroRef_Click(sender As Object, e As EventArgs) Handles cmdSetHA_Pot_ZeroRef.Click
        If txtHA_Pot_ZeroRef.Text = "" Then Exit Sub
        If Val(txtHA_Pot_ZeroRef.Text) = 0 Then Exit Sub
        sendPCcmd("*SET_HA_ZERO_REF_VAL" & txtHA_Pot_ZeroRef.Text & vbCr)
    End Sub

    Private Sub cmdGetDEC_Pot_ZeroRef_Click(sender As Object, e As EventArgs) Handles cmdGetDEC_Pot_ZeroRef.Click
        sendPCcmd("*SEND_DEC_ZERO_REF_VAL" & vbCr)
    End Sub

    Private Sub cmdSetDEC_Pot_ZeroRef_Click(sender As Object, e As EventArgs) Handles cmdSetDEC_Pot_ZeroRef.Click
        If txtDEC_Pot_ZeroRef.Text = "" Then Exit Sub
        If Val(txtDEC_Pot_ZeroRef.Text) = 0 Then Exit Sub
        sendPCcmd("*SET_DEC_ZERO_REF_VAL" & txtDEC_Pot_ZeroRef.Text & vbCr)
    End Sub

    Private Sub cmdGetHA_Pot_ScaleFactor_Click(sender As Object, e As EventArgs) Handles cmdGetHA_Pot_ScaleFactor.Click
        sendPCcmd("*SEND_HA_SCALE_FACTOR" & vbCr)
    End Sub

    Private Sub cmdSetHA_Pot_ScaleFactor_Click(sender As Object, e As EventArgs) Handles cmdSetHA_Pot_ScaleFactor.Click
        If txtHA_Pot_ScaleFactor.Text = "" Then Exit Sub
        If Val(txtHA_Pot_ScaleFactor.Text) = 0 Then Exit Sub
        sendPCcmd("*SET_HA_SCALE_FACTOR" & txtHA_Pot_ScaleFactor.Text & vbCr)
    End Sub

    Private Sub cmdGetDEC_Pot_ScaleFactor_Click(sender As Object, e As EventArgs) Handles cmdGetDEC_Pot_ScaleFactor.Click
        sendPCcmd("*SEND_DEC_SCALE_FACTOR" & vbCr)
    End Sub

    Private Sub cmdSetDEC_Pot_ScaleFactor_Click(sender As Object, e As EventArgs) Handles cmdSetDEC_Pot_ScaleFactor.Click
        If txtDEC_Pot_ScaleFactor.Text = "" Then Exit Sub
        If Val(txtDEC_Pot_ScaleFactor.Text) = 0 Then Exit Sub
        sendPCcmd("*SET_DEC_SCALE_FACTOR" & txtDEC_Pot_ScaleFactor.Text & vbCr)
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

    Private Sub sendPCcmd(cmd As String)
        List1.AppendText("send: " & cmd & vbLf) ' show what we are sending to the PC
        driver.CommandBlind(cmd, False) ' send it to the driver blind: don't wait for response
    End Sub
End Class
