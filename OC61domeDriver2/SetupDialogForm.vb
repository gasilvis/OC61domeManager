Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports ASCOM.Utilities
Imports ASCOM.OC61domeDriver

<ComVisible(False)>
Public Class SetupDialogForm

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click ' OK button event handler
        ' Persist new values of user settings to the ASCOM profile
        Dome.comPort = ComboBoxComPort.SelectedItem ' Update the state variables with results from the dialogue
        Dome.traceState = chkTrace.Checked
        Dome.childDomeId = labelDriverId.Text
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click 'Cancel button event handler
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub ShowAscomWebPage(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.DoubleClick, PictureBox1.Click
        ' Click on ASCOM logo event handler
        Try
            System.Diagnostics.Process.Start("http://ascom-standards.org/")
        Catch noBrowser As System.ComponentModel.Win32Exception
            If noBrowser.ErrorCode = -2147467259 Then
                MessageBox.Show(noBrowser.Message)
            End If
        Catch other As System.Exception
            MessageBox.Show(other.Message)
        End Try
    End Sub

    Private Sub SetupDialogForm_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load ' Form load event handler
        ' Retrieve current values of user settings from the ASCOM Profile
        InitUI()
    End Sub

    Private Sub InitUI()
        chkTrace.Checked = Dome.traceState
        ' set the list of com ports to those that are currently available
        ComboBoxComPort.Items.Clear()
        ComboBoxComPort.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames())       ' use System.IO because it's static
        ' select the current port if possible
        If ComboBoxComPort.Items.Contains(Dome.comPort) Then
            ComboBoxComPort.SelectedItem = Dome.comPort
        End If
        labelDriverId.Text = Dome.childDomeId
    End Sub

    Private Sub buttonChoose_Click(sender As Object, e As EventArgs) Handles buttonChoose.Click
        ' choose childDome
        ASCOM.DriverAccess.Dome.Choose(Dome.childDomeId)
        labelDriverId.Text = Dome.childDomeId
    End Sub

    Private Sub buttonConnect_Click(sender As Object, e As EventArgs) Handles buttonConnect.Click
        ' connect to childDome
        If (Dome.childDome.Connected) Then
            Dome.childDome.Connected = False
        Else
            Dome.childDome = New ASCOM.DriverAccess.Dome(Dome.childDomeId)
            Dome.childDome.Connected = True
        End If

    End Sub
End Class
