<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Splash
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Splash))
        Me.tmrSplash = New System.Windows.Forms.Timer(Me.components)
        Me.picBoxSplash = New System.Windows.Forms.PictureBox()
        Me.lblSplashTitle = New System.Windows.Forms.Label()
        Me.lblSplashVersion = New System.Windows.Forms.Label()
        Me.lblSplashCopy = New System.Windows.Forms.Label()
        Me.lblSplashLoad = New System.Windows.Forms.Label()
        CType(Me.picBoxSplash, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tmrSplash
        '
        Me.tmrSplash.Interval = 300
        '
        'picBoxSplash
        '
        Me.picBoxSplash.Image = CType(resources.GetObject("picBoxSplash.Image"), System.Drawing.Image)
        Me.picBoxSplash.Location = New System.Drawing.Point(12, 12)
        Me.picBoxSplash.Name = "picBoxSplash"
        Me.picBoxSplash.Size = New System.Drawing.Size(363, 382)
        Me.picBoxSplash.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picBoxSplash.TabIndex = 27
        Me.picBoxSplash.TabStop = False
        '
        'lblSplashTitle
        '
        Me.lblSplashTitle.AutoSize = True
        Me.lblSplashTitle.Font = New System.Drawing.Font("OCR A Extended", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSplashTitle.Location = New System.Drawing.Point(409, 42)
        Me.lblSplashTitle.Name = "lblSplashTitle"
        Me.lblSplashTitle.Size = New System.Drawing.Size(262, 50)
        Me.lblSplashTitle.TabIndex = 28
        Me.lblSplashTitle.Text = "Halfcoin"
        '
        'lblSplashVersion
        '
        Me.lblSplashVersion.AutoSize = True
        Me.lblSplashVersion.Font = New System.Drawing.Font("OCR A Extended", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSplashVersion.Location = New System.Drawing.Point(525, 92)
        Me.lblSplashVersion.Name = "lblSplashVersion"
        Me.lblSplashVersion.Size = New System.Drawing.Size(130, 20)
        Me.lblSplashVersion.TabIndex = 29
        Me.lblSplashVersion.Text = "Version 1.0"
        '
        'lblSplashCopy
        '
        Me.lblSplashCopy.AutoSize = True
        Me.lblSplashCopy.Font = New System.Drawing.Font("OCR A Extended", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSplashCopy.Location = New System.Drawing.Point(381, 377)
        Me.lblSplashCopy.Name = "lblSplashCopy"
        Me.lblSplashCopy.Size = New System.Drawing.Size(317, 20)
        Me.lblSplashCopy.TabIndex = 30
        Me.lblSplashCopy.Text = "© 2016-2017 Gianluca Cantone"
        '
        'lblSplashLoad
        '
        Me.lblSplashLoad.AutoSize = True
        Me.lblSplashLoad.Font = New System.Drawing.Font("OCR A Extended", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSplashLoad.Location = New System.Drawing.Point(381, 337)
        Me.lblSplashLoad.Name = "lblSplashLoad"
        Me.lblSplashLoad.Size = New System.Drawing.Size(146, 30)
        Me.lblSplashLoad.TabIndex = 31
        Me.lblSplashLoad.Text = "Loading"
        '
        'Splash
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.ClientSize = New System.Drawing.Size(703, 406)
        Me.Controls.Add(Me.lblSplashLoad)
        Me.Controls.Add(Me.lblSplashCopy)
        Me.Controls.Add(Me.lblSplashVersion)
        Me.Controls.Add(Me.lblSplashTitle)
        Me.Controls.Add(Me.picBoxSplash)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Splash"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Splash"
        CType(Me.picBoxSplash, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tmrSplash As System.Windows.Forms.Timer
    Friend WithEvents picBoxSplash As System.Windows.Forms.PictureBox
    Friend WithEvents lblSplashTitle As System.Windows.Forms.Label
    Friend WithEvents lblSplashVersion As System.Windows.Forms.Label
    Friend WithEvents lblSplashCopy As System.Windows.Forms.Label
    Friend WithEvents lblSplashLoad As System.Windows.Forms.Label
End Class
