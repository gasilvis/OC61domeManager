Public Class Form1 ' Peripheral Controller Emulator
    Private HA_Pot_ZeroReference As String = "512" ' nominal is 512
    Private DEC_Pot_ZeroReference As String = "512"
    Private HA_Pot_ScaleFactor As String = "0.381" '0.38136 ' about 23" per tic
    Private DEC_Pot_ScaleFactor As String = "0.398" '0.39823

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' load comport load: Show all available COM ports.
        For Each sp As String In My.Computer.Ports.SerialPortNames
            ComboBox1.Items.Add(sp)
        Next
        ComboBox1.SelectedIndex = ComboBox1.FindStringExact("COM20")
        TextBoxHA_Zero.Text = HA_Pot_ZeroReference
        TextBoxHA_Scale.Text = HA_Pot_ScaleFactor
        TextBoxDec_Zero.Text = DEC_Pot_ZeroReference
        TextBoxDec_Scale.Text = DEC_Pot_ScaleFactor

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If SerialPort1.IsOpen Then SerialPort1.Close()
        SerialPort1.PortName = ComboBox1.SelectedItem
        SerialPort1.BaudRate = 9600
        SerialPort1.NewLine = vbCr
        SerialPort1.ReadTimeout = 200
        SerialPort1.ReceivedBytesThreshold = 4
        SerialPort1.Open()
        ' protocol:   send, no prefix, suffix \r
        '           receive  prefix *, suffix \r
        'SerialPort1.Write("Hello world" & vbCr)
    End Sub

    Private Sub sendmsg(Message As String)
        ' send back 
        List1.AppendText("resp: " & Message & vbCrLf)
        If SerialPort1.IsOpen Then SerialPort1.Write(Message & vbCr)
    End Sub

    Dim mcstate As Integer = 0

    Private Sub processCmd(sent As String)
        List1.AppendText("cmd: " & sent & vbCrLf)
        Dim cmd As String = sent
        Dim parm As String = ""
        For i = 1 To Len(sent)
            parm = Mid(sent, i, 1)
            If IsNumeric(parm) Or 0 = StrComp(parm, "+") Or 0 = StrComp(parm, "-") Or 0 = StrComp(parm, ".") Then
                cmd = Mid(sent, 1, i - 1)
                parm = Mid(sent, i)
                Exit For
            End If
        Next
        Select Case cmd
            Case "*SEND_HA_ZERO_REF_VAL"
                TextBoxHA_Zero_Click(Me, EventArgs.Empty)
            Case "*SEND_HA_SCALE_FACTOR"
                TextBoxHA_Scale_Click(Me, EventArgs.Empty)
            Case "*SEND_DEC_ZERO_REF_VAL"
                TextBoxDec_Zero_Click(Me, EventArgs.Empty)
            Case "*SEND_DEC_SCALE_FACTOR"
                TextBoxDec_Scale_Click(Me, EventArgs.Empty)

            Case "*SET_HA_ZERO_REF_VAL"
                TextBoxHA_Zero.Text = parm
                HA_Pot_ZeroReference = parm
            Case "*SET_HA_SCALE_FACTOR"
                TextBoxHA_Scale.Text = parm
                HA_Pot_ScaleFactor = parm
            Case "*SET_DEC_ZERO_REF_VAL"
                TextBoxDec_Zero.Text = parm
                DEC_Pot_ZeroReference = parm
            Case "*SET_DEC_SCALE_FACTOR"
                TextBoxDec_Scale.Text = parm
                DEC_Pot_ScaleFactor = parm

            Case "*SEND_POSITION"
                Label10_Click(Me, EventArgs.Empty)
            Case "*SEND_ADC_VALS"
                Label18_Click(Me, EventArgs.Empty)
            Case "*OPEN_MIRROR_COVER"
                If mcstate <> 10 Then mcstate = 13 ' if not open, start opening
            Case "*CLOSE_MIRROR_COVER"
                If mcstate <> 0 Then mcstate = 3 ' if not shut, start shutting
            Case "*MIRROR_COVER_STATUS"
                Select Case mcstate
                    Case 0
                        Label6_Click(Me, EventArgs.Empty) ' shut
                    Case 10
                        Label7_Click(Me, EventArgs.Empty) ' open
                    Case Else
                        Label8_Click(Me, EventArgs.Empty) ' partly open
                        mcstate -= 1
                End Select

        End Select
    End Sub

    Private Sub SerialPort1_DataReceived(sender As Object, e As IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        Dim indata As String
        Try
            indata = SerialPort1.ReadLine() ' waits here for full line, ending in \r
            Me.Invoke(Sub() processCmd(indata)) ' because we are in a thread
        Catch
        End Try
    End Sub


#Region "button code"
    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        sendmsg("MIRROR COVER FAILED TO SHUT: COVER OPEN")
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        sendmsg("MIRROR COVER FAILED TO SHUT: COVER PARTLY OPEN")
    End Sub

    Private Sub Label1_Click_1(sender As Object, e As EventArgs) Handles Label1.Click
        sendmsg("MIRROR COVER FAILED TO OPEN: COVER SHUT")
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        sendmsg("MIRROR COVER FAILED TO OPEN: COVER PARTLY OPEN")
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        sendmsg("MIRROR COVER SENSOR ERROR")
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        sendmsg("SHUT")
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        sendmsg("OPEN")
    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click
        sendmsg("PARTLY OPEN")
    End Sub

    Private Sub Label16_Click(sender As Object, e As EventArgs) Handles Label16.Click
        sendmsg("HORIZON OK")
    End Sub

    Private Sub Label15_Click(sender As Object, e As EventArgs) Handles Label15.Click
        sendmsg("AT HORIZON LIMIT")
    End Sub

    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.Click
        ' send HA and Dec
        If Not IsNumeric(HAbox.Text) Then HAbox.Text = "600"
        Dim HA_Pot_AvgVal As Integer = CInt(HAbox.Text)
        Dim HA_RelativeVal As Integer = HA_Pot_AvgVal - HA_Pot_ZeroReference
        HA_RelativeVal = HA_Pot_AvgVal - HA_Pot_ZeroReference
        Dim HA_AngleFromPot As Double = HA_RelativeVal * HA_Pot_ScaleFactor
        Dim HA_String As String = String.Format("{0:+000.0;-000.0}", HA_AngleFromPot).Substring(0, 4) ' to allow -000

        If Not IsNumeric(DecBox.Text) Then DecBox.Text = "600"
        Dim DEC_Pot_AvgVal As Integer = CInt(DecBox.Text)
        Dim DEC_RelativeVal As Integer = DEC_Pot_AvgVal - DEC_Pot_ZeroReference
        DEC_RelativeVal = DEC_Pot_AvgVal - DEC_Pot_ZeroReference
        Dim DEC_AngleFromPot As Double = DEC_RelativeVal * DEC_Pot_ScaleFactor
        Dim DEC_String As String = String.Format("{0:+000.0;-000.0}", DEC_AngleFromPot).Substring(0, 4)

        sendmsg("HA" & HA_String & " DEC" & DEC_String)
    End Sub

    Private Sub Label18_Click(sender As Object, e As EventArgs) Handles Label18.Click
        ' send pot values
        If Not IsNumeric(HAbox.Text) Then HAbox.Text = "400"
        Dim HA_Pot_AvgVal As Integer = CInt(HAbox.Text)
        Dim HA_RelativeVal As Integer = HA_Pot_AvgVal - HA_Pot_ZeroReference
        Dim s As String = "HA_ABS: " & CInt(HA_Pot_AvgVal) & " HA_REL: " & CInt(HA_RelativeVal)

        If Not IsNumeric(DecBox.Text) Then DecBox.Text = "600"
        Dim DEC_Pot_AvgVal As Integer = CInt(DecBox.Text)
        Dim DEC_RelativeVal As Integer = DEC_Pot_AvgVal - DEC_Pot_ZeroReference
        s = s & " DEC_ABS: " & CInt(DEC_Pot_AvgVal) & " DEC_REL: " & CInt(DEC_RelativeVal)
        sendmsg(s)
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click
        sendmsg("HA_ZERO")
    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click
        sendmsg("DEC_ZERO")
    End Sub

    Private Sub Label14_Click(sender As Object, e As EventArgs) Handles Label14.Click
        sendmsg("HA_POT_SLIPPAGE_ERROR")
    End Sub

    Private Sub Label17_Click(sender As Object, e As EventArgs) Handles Label17.Click
        sendmsg("DEC_POT_SLIPPAGE_ERROR")
    End Sub

    Private Sub TextBoxHA_Zero_Click(sender As Object, e As EventArgs) Handles TextBoxHA_Zero.Click
        sendmsg("HA ZERO REF VAL: " + TextBoxHA_Zero.Text)
    End Sub

    Private Sub TextBoxHA_Scale_Click(sender As Object, e As EventArgs) Handles TextBoxHA_Scale.Click
        sendmsg("HA SCALE FACTOR VAL: " + TextBoxHA_Scale.Text)
    End Sub

    Private Sub TextBoxDec_Zero_Click(sender As Object, e As EventArgs) Handles TextBoxDec_Zero.Click
        sendmsg("DEC ZERO REF VAL: " + TextBoxDec_Zero.Text)
    End Sub

    Private Sub TextBoxDec_Scale_Click(sender As Object, e As EventArgs) Handles TextBoxDec_Scale.Click
        sendmsg("DEC SCALE FACTOR VAL: " + TextBoxDec_Scale.Text)
    End Sub


#End Region
End Class
