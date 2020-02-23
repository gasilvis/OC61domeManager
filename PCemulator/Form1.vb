Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' load comport load: Show all available COM ports.
        For Each sp As String In My.Computer.Ports.SerialPortNames
            ComboBox1.Items.Add(sp)
        Next
    End Sub

    Private Sub sendmsg(Message As String)
        ' send back 
        List1.AppendText("resp: " & Message & vbCrLf)
    End Sub

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
End Class
