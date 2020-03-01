<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.List1 = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.DecBox = New System.Windows.Forms.TextBox()
        Me.HAbox = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.SerialPort1 = New System.IO.Ports.SerialPort(Me.components)
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'List1
        '
        Me.List1.Font = New System.Drawing.Font("Arial Narrow", 7.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.List1.Location = New System.Drawing.Point(12, 42)
        Me.List1.Multiline = True
        Me.List1.Name = "List1"
        Me.List1.Size = New System.Drawing.Size(372, 486)
        Me.List1.TabIndex = 9
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Info
        Me.GroupBox1.Controls.Add(Me.GroupBox4)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Location = New System.Drawing.Point(399, 42)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(462, 486)
        Me.GroupBox1.TabIndex = 10
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "responses"
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.SystemColors.Window
        Me.GroupBox4.Controls.Add(Me.Label18)
        Me.GroupBox4.Controls.Add(Me.Label17)
        Me.GroupBox4.Controls.Add(Me.Label14)
        Me.GroupBox4.Controls.Add(Me.Label13)
        Me.GroupBox4.Controls.Add(Me.Label12)
        Me.GroupBox4.Controls.Add(Me.DecBox)
        Me.GroupBox4.Controls.Add(Me.HAbox)
        Me.GroupBox4.Controls.Add(Me.Label11)
        Me.GroupBox4.Controls.Add(Me.Label9)
        Me.GroupBox4.Controls.Add(Me.Label10)
        Me.GroupBox4.Location = New System.Drawing.Point(18, 305)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(421, 126)
        Me.GroupBox4.TabIndex = 4
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "position"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(188, 61)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(110, 17)
        Me.Label18.TabIndex = 11
        Me.Label18.Text = "Send pot values"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(172, 95)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(90, 17)
        Me.Label17.TabIndex = 10
        Me.Label17.Text = "Dec slippage"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(17, 95)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(84, 17)
        Me.Label14.TabIndex = 9
        Me.Label14.Text = "HA slippage"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(172, 78)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(105, 17)
        Me.Label13.TabIndex = 8
        Me.Label13.Text = "Dec zero found"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(205, 36)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(57, 17)
        Me.Label12.TabIndex = 7
        Me.Label12.Text = "Dec pot"
        '
        'DecBox
        '
        Me.DecBox.Location = New System.Drawing.Point(292, 33)
        Me.DecBox.Name = "DecBox"
        Me.DecBox.Size = New System.Drawing.Size(100, 22)
        Me.DecBox.TabIndex = 6
        Me.DecBox.Text = "512"
        '
        'HAbox
        '
        Me.HAbox.Location = New System.Drawing.Point(73, 30)
        Me.HAbox.Name = "HAbox"
        Me.HAbox.Size = New System.Drawing.Size(100, 22)
        Me.HAbox.TabIndex = 5
        Me.HAbox.Text = "512"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(16, 33)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(51, 17)
        Me.Label11.TabIndex = 4
        Me.Label11.Text = "HA pot"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(17, 78)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(99, 17)
        Me.Label9.TabIndex = 3
        Me.Label9.Text = "HA zero found"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(39, 61)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(98, 17)
        Me.Label10.TabIndex = 2
        Me.Label10.Text = "SendPositions"
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.SystemColors.Window
        Me.GroupBox3.Controls.Add(Me.Label15)
        Me.GroupBox3.Controls.Add(Me.Label16)
        Me.GroupBox3.Location = New System.Drawing.Point(18, 215)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(421, 75)
        Me.GroupBox3.TabIndex = 3
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Horizon sense"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(17, 45)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(132, 17)
        Me.Label15.TabIndex = 3
        Me.Label15.Text = "AT HORIZON LIMIT"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(17, 28)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(96, 17)
        Me.Label16.TabIndex = 2
        Me.Label16.Text = "HORIZON OK"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.SystemColors.Window
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Location = New System.Drawing.Point(18, 34)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(421, 175)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "MirrorCover Status"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(17, 147)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(105, 17)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "PARTLY OPEN"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(17, 130)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(47, 17)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "OPEN"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(17, 113)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(46, 17)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "SHUT"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 96)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(231, 17)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "MIRROR COVER SENSOR ERROR"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(17, 79)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(390, 17)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "MIRROR COVER FAILED TO OPEN: COVER PARTLY OPEN"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 62)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(331, 17)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "MIRROR COVER FAILED TO OPEN: COVER SHUT"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(17, 45)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(389, 17)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "MIRROR COVER FAILED TO SHUT: COVER PARTLY OPEN"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(17, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(331, 17)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "MIRROR COVER FAILED TO SHUT: COVER OPEN"
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(52, 12)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(121, 24)
        Me.ComboBox1.TabIndex = 12
        Me.ComboBox1.Text = "select comport"
        '
        'SerialPort1
        '
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(879, 549)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.List1)
        Me.Name = "Form1"
        Me.Text = "Peripheral Controller Emulator  ver 1"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents List1 As TextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Label15 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents Label12 As Label
    Friend WithEvents DecBox As TextBox
    Friend WithEvents HAbox As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents SerialPort1 As IO.Ports.SerialPort
    Friend WithEvents Label13 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents Label18 As Label
End Class
