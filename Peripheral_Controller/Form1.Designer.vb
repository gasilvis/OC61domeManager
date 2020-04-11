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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmdGetHorizonSenseStatus = New System.Windows.Forms.Button()
        Me.cmdOver_ride_HorizonSensor = New System.Windows.Forms.Button()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.cmdGetMirrorCoverStatus = New System.Windows.Forms.Button()
        Me.cmdShutMirrorCover = New System.Windows.Forms.Button()
        Me.cmdOpenMirrorCover = New System.Windows.Forms.Button()
        Me.List1 = New System.Windows.Forms.TextBox()
        Me.cmdClearList = New System.Windows.Forms.Button()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.cmdGetFlipMirrorStatus = New System.Windows.Forms.Button()
        Me.cmdDriveFlipMirrorOut = New System.Windows.Forms.Button()
        Me.cmdDriveFlipMirrorIn = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtDEC_Pot_ScaleFactor = New System.Windows.Forms.TextBox()
        Me.cmdSetDEC_Pot_ScaleFactor = New System.Windows.Forms.Button()
        Me.cmdGetDEC_Pot_ScaleFactor = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtHA_Pot_ScaleFactor = New System.Windows.Forms.TextBox()
        Me.cmdSetHA_Pot_ScaleFactor = New System.Windows.Forms.Button()
        Me.cmdGetHA_Pot_ScaleFactor = New System.Windows.Forms.Button()
        Me.cmdGetPot_ADU_Values = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtDEC_Pot_ZeroRef = New System.Windows.Forms.TextBox()
        Me.cmdSetDEC_Pot_ZeroRef = New System.Windows.Forms.Button()
        Me.cmdGetDEC_Pot_ZeroRef = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtHA_Pot_ZeroRef = New System.Windows.Forms.TextBox()
        Me.cmdSetHA_Pot_ZeroRef = New System.Windows.Forms.Button()
        Me.cmdGetHA_Pot_ZeroRef = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.cmdGetPositions = New System.Windows.Forms.Button()
        Me.cmdDisablePositionSend = New System.Windows.Forms.Button()
        Me.cmdEnablePositionSend = New System.Windows.Forms.Button()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.cmdFind_DEC_Opto = New System.Windows.Forms.Button()
        Me.cmdFind_HA_Opto = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.labelDriverId = New System.Windows.Forms.Label()
        Me.buttonConnect = New System.Windows.Forms.Button()
        Me.buttonChoose = New System.Windows.Forms.Button()
        Me.SerialPort1 = New System.IO.Ports.SerialPort(Me.components)
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.cmdSync_DEC_Pot = New System.Windows.Forms.Button()
        Me.cmdSync_HA_Pot = New System.Windows.Forms.Button()
        Me.SerialPort2 = New System.IO.Ports.SerialPort(Me.components)
        Me.GroupBox1.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.GroupBox1.Controls.Add(Me.cmdGetHorizonSenseStatus)
        Me.GroupBox1.Controls.Add(Me.cmdOver_ride_HorizonSensor)
        Me.GroupBox1.Location = New System.Drawing.Point(837, 423)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(184, 151)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Horizon Sensor"
        '
        'cmdGetHorizonSenseStatus
        '
        Me.cmdGetHorizonSenseStatus.Location = New System.Drawing.Point(21, 84)
        Me.cmdGetHorizonSenseStatus.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdGetHorizonSenseStatus.Name = "cmdGetHorizonSenseStatus"
        Me.cmdGetHorizonSenseStatus.Size = New System.Drawing.Size(144, 44)
        Me.cmdGetHorizonSenseStatus.TabIndex = 1
        Me.cmdGetHorizonSenseStatus.Text = "Get  Status"
        Me.cmdGetHorizonSenseStatus.UseVisualStyleBackColor = True
        '
        'cmdOver_ride_HorizonSensor
        '
        Me.cmdOver_ride_HorizonSensor.Location = New System.Drawing.Point(21, 28)
        Me.cmdOver_ride_HorizonSensor.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdOver_ride_HorizonSensor.Name = "cmdOver_ride_HorizonSensor"
        Me.cmdOver_ride_HorizonSensor.Size = New System.Drawing.Size(144, 44)
        Me.cmdOver_ride_HorizonSensor.TabIndex = 0
        Me.cmdOver_ride_HorizonSensor.Text = "5 min override"
        Me.cmdOver_ride_HorizonSensor.UseVisualStyleBackColor = True
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.Frame2.Controls.Add(Me.cmdGetMirrorCoverStatus)
        Me.Frame2.Controls.Add(Me.cmdShutMirrorCover)
        Me.Frame2.Controls.Add(Me.cmdOpenMirrorCover)
        Me.Frame2.Location = New System.Drawing.Point(330, 211)
        Me.Frame2.Margin = New System.Windows.Forms.Padding(4)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(4)
        Me.Frame2.Size = New System.Drawing.Size(159, 181)
        Me.Frame2.TabIndex = 7
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Mirror Cover"
        '
        'cmdGetMirrorCoverStatus
        '
        Me.cmdGetMirrorCoverStatus.Location = New System.Drawing.Point(8, 122)
        Me.cmdGetMirrorCoverStatus.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdGetMirrorCoverStatus.Name = "cmdGetMirrorCoverStatus"
        Me.cmdGetMirrorCoverStatus.Size = New System.Drawing.Size(139, 40)
        Me.cmdGetMirrorCoverStatus.TabIndex = 2
        Me.cmdGetMirrorCoverStatus.Text = "Get Status"
        Me.cmdGetMirrorCoverStatus.UseVisualStyleBackColor = True
        '
        'cmdShutMirrorCover
        '
        Me.cmdShutMirrorCover.Location = New System.Drawing.Point(8, 74)
        Me.cmdShutMirrorCover.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdShutMirrorCover.Name = "cmdShutMirrorCover"
        Me.cmdShutMirrorCover.Size = New System.Drawing.Size(139, 40)
        Me.cmdShutMirrorCover.TabIndex = 1
        Me.cmdShutMirrorCover.Text = "Shut"
        Me.cmdShutMirrorCover.UseVisualStyleBackColor = True
        '
        'cmdOpenMirrorCover
        '
        Me.cmdOpenMirrorCover.Location = New System.Drawing.Point(8, 26)
        Me.cmdOpenMirrorCover.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdOpenMirrorCover.Name = "cmdOpenMirrorCover"
        Me.cmdOpenMirrorCover.Size = New System.Drawing.Size(139, 40)
        Me.cmdOpenMirrorCover.TabIndex = 0
        Me.cmdOpenMirrorCover.Text = "Open"
        Me.cmdOpenMirrorCover.UseVisualStyleBackColor = True
        '
        'List1
        '
        Me.List1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.List1.Location = New System.Drawing.Point(16, 143)
        Me.List1.Multiline = True
        Me.List1.Name = "List1"
        Me.List1.Size = New System.Drawing.Size(307, 411)
        Me.List1.TabIndex = 8
        '
        'cmdClearList
        '
        Me.cmdClearList.Location = New System.Drawing.Point(88, 561)
        Me.cmdClearList.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdClearList.Name = "cmdClearList"
        Me.cmdClearList.Size = New System.Drawing.Size(172, 31)
        Me.cmdClearList.TabIndex = 9
        Me.cmdClearList.Text = "Clear the List"
        Me.cmdClearList.UseVisualStyleBackColor = True
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.Frame3.Controls.Add(Me.cmdGetFlipMirrorStatus)
        Me.Frame3.Controls.Add(Me.cmdDriveFlipMirrorOut)
        Me.Frame3.Controls.Add(Me.cmdDriveFlipMirrorIn)
        Me.Frame3.Location = New System.Drawing.Point(330, 15)
        Me.Frame3.Margin = New System.Windows.Forms.Padding(4)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(4)
        Me.Frame3.Size = New System.Drawing.Size(159, 175)
        Me.Frame3.TabIndex = 10
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Flip Mirror"
        '
        'cmdGetFlipMirrorStatus
        '
        Me.cmdGetFlipMirrorStatus.Location = New System.Drawing.Point(8, 122)
        Me.cmdGetFlipMirrorStatus.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdGetFlipMirrorStatus.Name = "cmdGetFlipMirrorStatus"
        Me.cmdGetFlipMirrorStatus.Size = New System.Drawing.Size(139, 40)
        Me.cmdGetFlipMirrorStatus.TabIndex = 2
        Me.cmdGetFlipMirrorStatus.Text = "Get Status"
        Me.cmdGetFlipMirrorStatus.UseVisualStyleBackColor = True
        '
        'cmdDriveFlipMirrorOut
        '
        Me.cmdDriveFlipMirrorOut.Location = New System.Drawing.Point(8, 77)
        Me.cmdDriveFlipMirrorOut.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdDriveFlipMirrorOut.Name = "cmdDriveFlipMirrorOut"
        Me.cmdDriveFlipMirrorOut.Size = New System.Drawing.Size(139, 40)
        Me.cmdDriveFlipMirrorOut.TabIndex = 1
        Me.cmdDriveFlipMirrorOut.Text = "Drive OUT"
        Me.cmdDriveFlipMirrorOut.UseVisualStyleBackColor = True
        '
        'cmdDriveFlipMirrorIn
        '
        Me.cmdDriveFlipMirrorIn.Location = New System.Drawing.Point(8, 31)
        Me.cmdDriveFlipMirrorIn.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdDriveFlipMirrorIn.Name = "cmdDriveFlipMirrorIn"
        Me.cmdDriveFlipMirrorIn.Size = New System.Drawing.Size(139, 40)
        Me.cmdDriveFlipMirrorIn.TabIndex = 0
        Me.cmdDriveFlipMirrorIn.Text = "Drive IN"
        Me.cmdDriveFlipMirrorIn.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.SystemColors.Info
        Me.GroupBox3.Controls.Add(Me.GroupBox5)
        Me.GroupBox3.Controls.Add(Me.cmdGetPot_ADU_Values)
        Me.GroupBox3.Controls.Add(Me.GroupBox2)
        Me.GroupBox3.Location = New System.Drawing.Point(510, 37)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(309, 465)
        Me.GroupBox3.TabIndex = 12
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Position Pot Settings"
        '
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.GroupBox5.Controls.Add(Me.Label3)
        Me.GroupBox5.Controls.Add(Me.txtDEC_Pot_ScaleFactor)
        Me.GroupBox5.Controls.Add(Me.cmdSetDEC_Pot_ScaleFactor)
        Me.GroupBox5.Controls.Add(Me.cmdGetDEC_Pot_ScaleFactor)
        Me.GroupBox5.Controls.Add(Me.Label4)
        Me.GroupBox5.Controls.Add(Me.txtHA_Pot_ScaleFactor)
        Me.GroupBox5.Controls.Add(Me.cmdSetHA_Pot_ScaleFactor)
        Me.GroupBox5.Controls.Add(Me.cmdGetHA_Pot_ScaleFactor)
        Me.GroupBox5.Location = New System.Drawing.Point(20, 283)
        Me.GroupBox5.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox5.Size = New System.Drawing.Size(276, 157)
        Me.GroupBox5.TabIndex = 14
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Pot Scale Factor"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 106)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(43, 20)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Dec"
        '
        'txtDEC_Pot_ScaleFactor
        '
        Me.txtDEC_Pot_ScaleFactor.Location = New System.Drawing.Point(116, 103)
        Me.txtDEC_Pot_ScaleFactor.Name = "txtDEC_Pot_ScaleFactor"
        Me.txtDEC_Pot_ScaleFactor.Size = New System.Drawing.Size(85, 27)
        Me.txtDEC_Pot_ScaleFactor.TabIndex = 8
        '
        'cmdSetDEC_Pot_ScaleFactor
        '
        Me.cmdSetDEC_Pot_ScaleFactor.Location = New System.Drawing.Point(208, 98)
        Me.cmdSetDEC_Pot_ScaleFactor.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdSetDEC_Pot_ScaleFactor.Name = "cmdSetDEC_Pot_ScaleFactor"
        Me.cmdSetDEC_Pot_ScaleFactor.Size = New System.Drawing.Size(53, 36)
        Me.cmdSetDEC_Pot_ScaleFactor.TabIndex = 7
        Me.cmdSetDEC_Pot_ScaleFactor.Text = "Set"
        Me.cmdSetDEC_Pot_ScaleFactor.UseVisualStyleBackColor = True
        '
        'cmdGetDEC_Pot_ScaleFactor
        '
        Me.cmdGetDEC_Pot_ScaleFactor.Location = New System.Drawing.Point(56, 98)
        Me.cmdGetDEC_Pot_ScaleFactor.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdGetDEC_Pot_ScaleFactor.Name = "cmdGetDEC_Pot_ScaleFactor"
        Me.cmdGetDEC_Pot_ScaleFactor.Size = New System.Drawing.Size(53, 36)
        Me.cmdGetDEC_Pot_ScaleFactor.TabIndex = 6
        Me.cmdGetDEC_Pot_ScaleFactor.Text = "Get"
        Me.cmdGetDEC_Pot_ScaleFactor.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 49)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 20)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "HA"
        '
        'txtHA_Pot_ScaleFactor
        '
        Me.txtHA_Pot_ScaleFactor.Location = New System.Drawing.Point(116, 46)
        Me.txtHA_Pot_ScaleFactor.Name = "txtHA_Pot_ScaleFactor"
        Me.txtHA_Pot_ScaleFactor.Size = New System.Drawing.Size(85, 27)
        Me.txtHA_Pot_ScaleFactor.TabIndex = 4
        '
        'cmdSetHA_Pot_ScaleFactor
        '
        Me.cmdSetHA_Pot_ScaleFactor.Location = New System.Drawing.Point(208, 41)
        Me.cmdSetHA_Pot_ScaleFactor.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdSetHA_Pot_ScaleFactor.Name = "cmdSetHA_Pot_ScaleFactor"
        Me.cmdSetHA_Pot_ScaleFactor.Size = New System.Drawing.Size(53, 36)
        Me.cmdSetHA_Pot_ScaleFactor.TabIndex = 3
        Me.cmdSetHA_Pot_ScaleFactor.Text = "Set"
        Me.cmdSetHA_Pot_ScaleFactor.UseVisualStyleBackColor = True
        '
        'cmdGetHA_Pot_ScaleFactor
        '
        Me.cmdGetHA_Pot_ScaleFactor.Location = New System.Drawing.Point(56, 41)
        Me.cmdGetHA_Pot_ScaleFactor.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdGetHA_Pot_ScaleFactor.Name = "cmdGetHA_Pot_ScaleFactor"
        Me.cmdGetHA_Pot_ScaleFactor.Size = New System.Drawing.Size(53, 36)
        Me.cmdGetHA_Pot_ScaleFactor.TabIndex = 1
        Me.cmdGetHA_Pot_ScaleFactor.Text = "Get"
        Me.cmdGetHA_Pot_ScaleFactor.UseVisualStyleBackColor = True
        '
        'cmdGetPot_ADU_Values
        '
        Me.cmdGetPot_ADU_Values.Location = New System.Drawing.Point(20, 37)
        Me.cmdGetPot_ADU_Values.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.cmdGetPot_ADU_Values.Name = "cmdGetPot_ADU_Values"
        Me.cmdGetPot_ADU_Values.Size = New System.Drawing.Size(225, 35)
        Me.cmdGetPot_ADU_Values.TabIndex = 13
        Me.cmdGetPot_ADU_Values.Text = "Get Pot ADU values"
        Me.cmdGetPot_ADU_Values.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.txtDEC_Pot_ZeroRef)
        Me.GroupBox2.Controls.Add(Me.cmdSetDEC_Pot_ZeroRef)
        Me.GroupBox2.Controls.Add(Me.cmdGetDEC_Pot_ZeroRef)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.txtHA_Pot_ZeroRef)
        Me.GroupBox2.Controls.Add(Me.cmdSetHA_Pot_ZeroRef)
        Me.GroupBox2.Controls.Add(Me.cmdGetHA_Pot_ZeroRef)
        Me.GroupBox2.Location = New System.Drawing.Point(20, 102)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(276, 150)
        Me.GroupBox2.TabIndex = 12
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Pot Zero Reference"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 106)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 20)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Dec"
        '
        'txtDEC_Pot_ZeroRef
        '
        Me.txtDEC_Pot_ZeroRef.Location = New System.Drawing.Point(116, 103)
        Me.txtDEC_Pot_ZeroRef.Name = "txtDEC_Pot_ZeroRef"
        Me.txtDEC_Pot_ZeroRef.Size = New System.Drawing.Size(85, 27)
        Me.txtDEC_Pot_ZeroRef.TabIndex = 8
        '
        'cmdSetDEC_Pot_ZeroRef
        '
        Me.cmdSetDEC_Pot_ZeroRef.Location = New System.Drawing.Point(208, 98)
        Me.cmdSetDEC_Pot_ZeroRef.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdSetDEC_Pot_ZeroRef.Name = "cmdSetDEC_Pot_ZeroRef"
        Me.cmdSetDEC_Pot_ZeroRef.Size = New System.Drawing.Size(53, 36)
        Me.cmdSetDEC_Pot_ZeroRef.TabIndex = 7
        Me.cmdSetDEC_Pot_ZeroRef.Text = "Set"
        Me.cmdSetDEC_Pot_ZeroRef.UseVisualStyleBackColor = True
        '
        'cmdGetDEC_Pot_ZeroRef
        '
        Me.cmdGetDEC_Pot_ZeroRef.Location = New System.Drawing.Point(56, 94)
        Me.cmdGetDEC_Pot_ZeroRef.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdGetDEC_Pot_ZeroRef.Name = "cmdGetDEC_Pot_ZeroRef"
        Me.cmdGetDEC_Pot_ZeroRef.Size = New System.Drawing.Size(53, 36)
        Me.cmdGetDEC_Pot_ZeroRef.TabIndex = 6
        Me.cmdGetDEC_Pot_ZeroRef.Text = "Get"
        Me.cmdGetDEC_Pot_ZeroRef.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 49)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 20)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "HA"
        '
        'txtHA_Pot_ZeroRef
        '
        Me.txtHA_Pot_ZeroRef.Location = New System.Drawing.Point(116, 46)
        Me.txtHA_Pot_ZeroRef.Name = "txtHA_Pot_ZeroRef"
        Me.txtHA_Pot_ZeroRef.Size = New System.Drawing.Size(85, 27)
        Me.txtHA_Pot_ZeroRef.TabIndex = 4
        '
        'cmdSetHA_Pot_ZeroRef
        '
        Me.cmdSetHA_Pot_ZeroRef.Location = New System.Drawing.Point(208, 41)
        Me.cmdSetHA_Pot_ZeroRef.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdSetHA_Pot_ZeroRef.Name = "cmdSetHA_Pot_ZeroRef"
        Me.cmdSetHA_Pot_ZeroRef.Size = New System.Drawing.Size(53, 36)
        Me.cmdSetHA_Pot_ZeroRef.TabIndex = 3
        Me.cmdSetHA_Pot_ZeroRef.Text = "Set"
        Me.cmdSetHA_Pot_ZeroRef.UseVisualStyleBackColor = True
        '
        'cmdGetHA_Pot_ZeroRef
        '
        Me.cmdGetHA_Pot_ZeroRef.Location = New System.Drawing.Point(56, 37)
        Me.cmdGetHA_Pot_ZeroRef.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdGetHA_Pot_ZeroRef.Name = "cmdGetHA_Pot_ZeroRef"
        Me.cmdGetHA_Pot_ZeroRef.Size = New System.Drawing.Size(53, 36)
        Me.cmdGetHA_Pot_ZeroRef.TabIndex = 1
        Me.cmdGetHA_Pot_ZeroRef.Text = "Get"
        Me.cmdGetHA_Pot_ZeroRef.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.GroupBox4.Controls.Add(Me.cmdGetPositions)
        Me.GroupBox4.Controls.Add(Me.cmdDisablePositionSend)
        Me.GroupBox4.Controls.Add(Me.cmdEnablePositionSend)
        Me.GroupBox4.Location = New System.Drawing.Point(330, 412)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox4.Size = New System.Drawing.Size(159, 180)
        Me.GroupBox4.TabIndex = 13
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Position String"
        '
        'cmdGetPositions
        '
        Me.cmdGetPositions.Location = New System.Drawing.Point(8, 126)
        Me.cmdGetPositions.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdGetPositions.Name = "cmdGetPositions"
        Me.cmdGetPositions.Size = New System.Drawing.Size(139, 40)
        Me.cmdGetPositions.TabIndex = 2
        Me.cmdGetPositions.Text = "Get HA, Dec"
        Me.cmdGetPositions.UseVisualStyleBackColor = True
        '
        'cmdDisablePositionSend
        '
        Me.cmdDisablePositionSend.Location = New System.Drawing.Point(8, 78)
        Me.cmdDisablePositionSend.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdDisablePositionSend.Name = "cmdDisablePositionSend"
        Me.cmdDisablePositionSend.Size = New System.Drawing.Size(139, 40)
        Me.cmdDisablePositionSend.TabIndex = 1
        Me.cmdDisablePositionSend.Text = "Disable Send"
        Me.cmdDisablePositionSend.UseVisualStyleBackColor = True
        '
        'cmdEnablePositionSend
        '
        Me.cmdEnablePositionSend.Location = New System.Drawing.Point(8, 30)
        Me.cmdEnablePositionSend.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdEnablePositionSend.Name = "cmdEnablePositionSend"
        Me.cmdEnablePositionSend.Size = New System.Drawing.Size(139, 40)
        Me.cmdEnablePositionSend.TabIndex = 0
        Me.cmdEnablePositionSend.Text = "Enable Send"
        Me.cmdEnablePositionSend.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.GroupBox6.Controls.Add(Me.cmdFind_DEC_Opto)
        Me.GroupBox6.Controls.Add(Me.cmdFind_HA_Opto)
        Me.GroupBox6.Location = New System.Drawing.Point(837, 21)
        Me.GroupBox6.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox6.Size = New System.Drawing.Size(184, 151)
        Me.GroupBox6.TabIndex = 14
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Find Optos"
        '
        'cmdFind_DEC_Opto
        '
        Me.cmdFind_DEC_Opto.Location = New System.Drawing.Point(21, 84)
        Me.cmdFind_DEC_Opto.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdFind_DEC_Opto.Name = "cmdFind_DEC_Opto"
        Me.cmdFind_DEC_Opto.Size = New System.Drawing.Size(144, 44)
        Me.cmdFind_DEC_Opto.TabIndex = 1
        Me.cmdFind_DEC_Opto.Text = "Find Dec Opto"
        Me.cmdFind_DEC_Opto.UseVisualStyleBackColor = True
        '
        'cmdFind_HA_Opto
        '
        Me.cmdFind_HA_Opto.Location = New System.Drawing.Point(21, 28)
        Me.cmdFind_HA_Opto.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdFind_HA_Opto.Name = "cmdFind_HA_Opto"
        Me.cmdFind_HA_Opto.Size = New System.Drawing.Size(144, 44)
        Me.cmdFind_HA_Opto.TabIndex = 0
        Me.cmdFind_HA_Opto.Text = "Find HA Opto"
        Me.cmdFind_HA_Opto.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(12, 8)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(311, 129)
        Me.TabControl1.TabIndex = 15
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.Salmon
        Me.TabPage1.Controls.Add(Me.ComboBox1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 29)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(303, 96)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Serial connection"
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(44, 34)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(170, 28)
        Me.ComboBox1.TabIndex = 13
        Me.ComboBox1.Text = "select comport"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.labelDriverId)
        Me.TabPage2.Controls.Add(Me.buttonConnect)
        Me.TabPage2.Controls.Add(Me.buttonChoose)
        Me.TabPage2.Location = New System.Drawing.Point(4, 29)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(303, 96)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Driver"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'labelDriverId
        '
        Me.labelDriverId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.labelDriverId.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.ASCOM.OC61domeServer2.My.MySettings.Default, "DriverId", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.labelDriverId.Location = New System.Drawing.Point(6, 56)
        Me.labelDriverId.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.labelDriverId.Name = "labelDriverId"
        Me.labelDriverId.Size = New System.Drawing.Size(271, 30)
        Me.labelDriverId.TabIndex = 8
        Me.labelDriverId.Text = Global.ASCOM.OC61domeServer2.My.MySettings.Default.DriverId
        Me.labelDriverId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'buttonConnect
        '
        Me.buttonConnect.Location = New System.Drawing.Point(23, 13)
        Me.buttonConnect.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.buttonConnect.Name = "buttonConnect"
        Me.buttonConnect.Size = New System.Drawing.Size(116, 29)
        Me.buttonConnect.TabIndex = 7
        Me.buttonConnect.Text = "Connect"
        Me.buttonConnect.UseVisualStyleBackColor = True
        '
        'buttonChoose
        '
        Me.buttonChoose.Location = New System.Drawing.Point(151, 13)
        Me.buttonChoose.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.buttonChoose.Name = "buttonChoose"
        Me.buttonChoose.Size = New System.Drawing.Size(120, 29)
        Me.buttonChoose.TabIndex = 6
        Me.buttonChoose.Text = "Choose"
        Me.buttonChoose.UseVisualStyleBackColor = True
        '
        'SerialPort1
        '
        '
        'GroupBox7
        '
        Me.GroupBox7.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.GroupBox7.Controls.Add(Me.cmdSync_DEC_Pot)
        Me.GroupBox7.Controls.Add(Me.cmdSync_HA_Pot)
        Me.GroupBox7.Location = New System.Drawing.Point(837, 191)
        Me.GroupBox7.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox7.Size = New System.Drawing.Size(191, 178)
        Me.GroupBox7.TabIndex = 16
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Sync to Optos"
        '
        'cmdSync_DEC_Pot
        '
        Me.cmdSync_DEC_Pot.Location = New System.Drawing.Point(10, 97)
        Me.cmdSync_DEC_Pot.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.cmdSync_DEC_Pot.Name = "cmdSync_DEC_Pot"
        Me.cmdSync_DEC_Pot.Size = New System.Drawing.Size(171, 61)
        Me.cmdSync_DEC_Pot.TabIndex = 17
        Me.cmdSync_DEC_Pot.Text = "Dec Zero Ref Pot to Dec Opto"
        Me.cmdSync_DEC_Pot.UseVisualStyleBackColor = True
        '
        'cmdSync_HA_Pot
        '
        Me.cmdSync_HA_Pot.Location = New System.Drawing.Point(10, 29)
        Me.cmdSync_HA_Pot.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.cmdSync_HA_Pot.Name = "cmdSync_HA_Pot"
        Me.cmdSync_HA_Pot.Size = New System.Drawing.Size(171, 59)
        Me.cmdSync_HA_Pot.TabIndex = 16
        Me.cmdSync_HA_Pot.Text = "HA Zero Ref Pot to HA Opto"
        Me.cmdSync_HA_Pot.UseVisualStyleBackColor = True
        '
        'SerialPort2
        '
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1036, 605)
        Me.Controls.Add(Me.GroupBox7)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.cmdClearList)
        Me.Controls.Add(Me.List1)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.Name = "Form1"
        Me.Text = "Peripheral Controller Application   ver 5"
        Me.GroupBox1.ResumeLayout(False)
        Me.Frame2.ResumeLayout(False)
        Me.Frame3.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox7.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents cmdOver_ride_HorizonSensor As Button
    Private WithEvents cmdGetHorizonSenseStatus As Button
    Friend WithEvents Frame2 As GroupBox
    Private WithEvents cmdGetMirrorCoverStatus As Button
    Private WithEvents cmdShutMirrorCover As Button
    Friend WithEvents cmdOpenMirrorCover As Button
    Friend WithEvents List1 As TextBox
    Friend WithEvents cmdClearList As Button
    Friend WithEvents Frame3 As GroupBox
    Private WithEvents cmdGetFlipMirrorStatus As Button
    Private WithEvents cmdDriveFlipMirrorOut As Button
    Friend WithEvents cmdDriveFlipMirrorIn As Button
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtDEC_Pot_ZeroRef As TextBox
    Private WithEvents cmdSetDEC_Pot_ZeroRef As Button
    Private WithEvents cmdGetDEC_Pot_ZeroRef As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents txtHA_Pot_ZeroRef As TextBox
    Private WithEvents cmdSetHA_Pot_ZeroRef As Button
    Private WithEvents cmdGetHA_Pot_ZeroRef As Button
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtDEC_Pot_ScaleFactor As TextBox
    Private WithEvents cmdSetDEC_Pot_ScaleFactor As Button
    Private WithEvents cmdGetDEC_Pot_ScaleFactor As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents txtHA_Pot_ScaleFactor As TextBox
    Private WithEvents cmdSetHA_Pot_ScaleFactor As Button
    Private WithEvents cmdGetHA_Pot_ScaleFactor As Button
    Private WithEvents cmdGetPot_ADU_Values As Button
    Friend WithEvents GroupBox4 As GroupBox
    Private WithEvents cmdGetPositions As Button
    Private WithEvents cmdDisablePositionSend As Button
    Friend WithEvents cmdEnablePositionSend As Button
    Friend WithEvents GroupBox6 As GroupBox
    Private WithEvents cmdFind_DEC_Opto As Button
    Friend WithEvents cmdFind_HA_Opto As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Private WithEvents labelDriverId As Label
    Private WithEvents buttonConnect As Button
    Private WithEvents buttonChoose As Button
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents SerialPort1 As IO.Ports.SerialPort
    Friend WithEvents GroupBox7 As GroupBox
    Private WithEvents cmdSync_DEC_Pot As Button
    Private WithEvents cmdSync_HA_Pot As Button
    Friend WithEvents SerialPort2 As IO.Ports.SerialPort
End Class
