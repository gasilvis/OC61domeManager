Public Class Form1 ' Peripheral Controller Emulator

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' load comport load: Show all available COM ports.
        For Each sp As String In My.Computer.Ports.SerialPortNames
            ComboBox1.Items.Add(sp)
        Next
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
        SerialPort1.Write(Message & vbCr)
    End Sub

    Private Sub processCmd(cmd As String)
        List1.AppendText("cmd: " & cmd & vbCrLf)
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
        sendmsg("HA" & HAbox.Text & " Dec" & DecBox.Text)
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

#End Region
End Class
