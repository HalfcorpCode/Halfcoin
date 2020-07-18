<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Halfcoin
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Halfcoin))
        Me.TabControl = New System.Windows.Forms.TabControl()
        Me.Wallet = New System.Windows.Forms.TabPage()
        Me.picboxWarn = New System.Windows.Forms.PictureBox()
        Me.btnCopyRight = New System.Windows.Forms.Button()
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.picboxLogo = New System.Windows.Forms.PictureBox()
        Me.btnConnect = New System.Windows.Forms.Button()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.lblBlocksRem = New System.Windows.Forms.Label()
        Me.prbBlocks = New System.Windows.Forms.ProgressBar()
        Me.lblSync1 = New System.Windows.Forms.Label()
        Me.lblStat1 = New System.Windows.Forms.Label()
        Me.lblSyncStat1 = New System.Windows.Forms.Label()
        Me.lblNetStat1 = New System.Windows.Forms.Label()
        Me.btnLogin = New System.Windows.Forms.Button()
        Me.lblSuffix2 = New System.Windows.Forms.Label()
        Me.lblSuffix = New System.Windows.Forms.Label()
        Me.txtUnBal = New System.Windows.Forms.TextBox()
        Me.txtBal = New System.Windows.Forms.TextBox()
        Me.txtPubKey = New System.Windows.Forms.TextBox()
        Me.txtPriKey = New System.Windows.Forms.TextBox()
        Me.txtLogin = New System.Windows.Forms.TextBox()
        Me.lblBalanceInfo = New System.Windows.Forms.Label()
        Me.lblUnconfirmedBalance = New System.Windows.Forms.Label()
        Me.lblBalance = New System.Windows.Forms.Label()
        Me.lblPublicKey = New System.Windows.Forms.Label()
        Me.lblPrivateKey = New System.Windows.Forms.Label()
        Me.lblLoginWord = New System.Windows.Forms.Label()
        Me.lblAccountInfo = New System.Windows.Forms.Label()
        Me.grbAccInfo = New System.Windows.Forms.GroupBox()
        Me.btnHidLog = New System.Windows.Forms.Button()
        Me.btnHidePri = New System.Windows.Forms.Button()
        Me.grbAccBal = New System.Windows.Forms.GroupBox()
        Me.Send_Money = New System.Windows.Forms.TabPage()
        Me.lblOptional = New System.Windows.Forms.Label()
        Me.btnSend = New System.Windows.Forms.Button()
        Me.lblSuffix4 = New System.Windows.Forms.Label()
        Me.lblSuffix3 = New System.Windows.Forms.Label()
        Me.txtTransFee = New System.Windows.Forms.TextBox()
        Me.txtSendAmount = New System.Windows.Forms.TextBox()
        Me.lblTransFee = New System.Windows.Forms.Label()
        Me.lblSendAmount = New System.Windows.Forms.Label()
        Me.txtRepAdr = New System.Windows.Forms.TextBox()
        Me.lblRAddress = New System.Windows.Forms.Label()
        Me.txtRepPubKey = New System.Windows.Forms.TextBox()
        Me.lblRPublicKey = New System.Windows.Forms.Label()
        Me.btnNewAdr = New System.Windows.Forms.Button()
        Me.txtAdr = New System.Windows.Forms.TextBox()
        Me.lblTransDetails = New System.Windows.Forms.Label()
        Me.lblAddress = New System.Windows.Forms.Label()
        Me.lblAddresses = New System.Windows.Forms.Label()
        Me.grbYourAdr = New System.Windows.Forms.GroupBox()
        Me.grbTransDet = New System.Windows.Forms.GroupBox()
        Me.Address_Book = New System.Windows.Forms.TabPage()
        Me.txtLabel = New System.Windows.Forms.TextBox()
        Me.lblLabel = New System.Windows.Forms.Label()
        Me.btnDel = New System.Windows.Forms.Button()
        Me.btnAddNew = New System.Windows.Forms.Button()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.txtEnterPubKey = New System.Windows.Forms.TextBox()
        Me.lblPublicKey1 = New System.Windows.Forms.Label()
        Me.lstAdr = New System.Windows.Forms.ListBox()
        Me.lblAdrBook = New System.Windows.Forms.Label()
        Me.grpAdrBook = New System.Windows.Forms.GroupBox()
        Me.Mine = New System.Windows.Forms.TabPage()
        Me.txtTime = New System.Windows.Forms.TextBox()
        Me.lblTimeElapsed = New System.Windows.Forms.Label()
        Me.btnStopMine = New System.Windows.Forms.Button()
        Me.txtConsoleMine = New System.Windows.Forms.RichTextBox()
        Me.btnStartMine = New System.Windows.Forms.Button()
        Me.grpMining = New System.Windows.Forms.GroupBox()
        Me.ckbStop = New System.Windows.Forms.CheckBox()
        Me.lblHashUnit = New System.Windows.Forms.Label()
        Me.txtHashRate = New System.Windows.Forms.TextBox()
        Me.lblHashRate = New System.Windows.Forms.Label()
        Me.cmbCores = New System.Windows.Forms.ComboBox()
        Me.lblCores = New System.Windows.Forms.Label()
        Me.txtBlocksMissed = New System.Windows.Forms.TextBox()
        Me.lblBlocksMissed = New System.Windows.Forms.Label()
        Me.txtBlocksMined = New System.Windows.Forms.TextBox()
        Me.txtTotalTime = New System.Windows.Forms.TextBox()
        Me.lblBlocksMined = New System.Windows.Forms.Label()
        Me.lblTotalTimeElapsed = New System.Windows.Forms.Label()
        Me.lblMining = New System.Windows.Forms.Label()
        Me.Settings = New System.Windows.Forms.TabPage()
        Me.lstKnownNodes = New System.Windows.Forms.ListBox()
        Me.lblChainStat = New System.Windows.Forms.Label()
        Me.lblFileSet = New System.Windows.Forms.Label()
        Me.grpChainStat = New System.Windows.Forms.GroupBox()
        Me.txtUTXOSize = New System.Windows.Forms.TextBox()
        Me.txtUTXONum = New System.Windows.Forms.TextBox()
        Me.txtBlockSize = New System.Windows.Forms.TextBox()
        Me.txtLocalHeight = New System.Windows.Forms.TextBox()
        Me.lblUTXOSize = New System.Windows.Forms.Label()
        Me.lblUTXONum = New System.Windows.Forms.Label()
        Me.lblChainSize = New System.Windows.Forms.Label()
        Me.lblLocal = New System.Windows.Forms.Label()
        Me.grpFileSet = New System.Windows.Forms.GroupBox()
        Me.btnHashRebuild = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnBlockReset = New System.Windows.Forms.Button()
        Me.btnUTXORebuild = New System.Windows.Forms.Button()
        Me.lblRebuild = New System.Windows.Forms.Label()
        Me.lblRest = New System.Windows.Forms.Label()
        Me.lblNetSet = New System.Windows.Forms.Label()
        Me.grpNetSet = New System.Windows.Forms.GroupBox()
        Me.txtParentIP = New System.Windows.Forms.TextBox()
        Me.lblParentIP = New System.Windows.Forms.Label()
        Me.cmbUPNP = New System.Windows.Forms.ComboBox()
        Me.lblNetShip = New System.Windows.Forms.Label()
        Me.cmbNetShip = New System.Windows.Forms.ComboBox()
        Me.cmbNetType = New System.Windows.Forms.ComboBox()
        Me.lblNetType = New System.Windows.Forms.Label()
        Me.lblUPNP = New System.Windows.Forms.Label()
        Me.lstActiveNodes = New System.Windows.Forms.ListBox()
        Me.lblKnownNodes = New System.Windows.Forms.Label()
        Me.lblActiveNodes = New System.Windows.Forms.Label()
        Me.lblMemPool1 = New System.Windows.Forms.Label()
        Me.lblNodes = New System.Windows.Forms.Label()
        Me.lblSync2 = New System.Windows.Forms.Label()
        Me.lblStat2 = New System.Windows.Forms.Label()
        Me.lblNodeCon = New System.Windows.Forms.Label()
        Me.lblNetStat = New System.Windows.Forms.Label()
        Me.grbNetStat = New System.Windows.Forms.GroupBox()
        Me.lblNetStat2 = New System.Windows.Forms.Label()
        Me.lblSyncStat2 = New System.Windows.Forms.Label()
        Me.lblMemPool2 = New System.Windows.Forms.Label()
        Me.Console = New System.Windows.Forms.TabPage()
        Me.btnEXE = New System.Windows.Forms.Button()
        Me.txtCom = New System.Windows.Forms.TextBox()
        Me.txtConsoleMain = New System.Windows.Forms.RichTextBox()
        Me.lblConsole = New System.Windows.Forms.Label()
        Me.grpConsole = New System.Windows.Forms.GroupBox()
        Me.Blockchain_Explorer = New System.Windows.Forms.TabPage()
        Me.grpOutExp = New System.Windows.Forms.GroupBox()
        Me.lblXOS = New System.Windows.Forms.Label()
        Me.txtXOS = New System.Windows.Forms.RichTextBox()
        Me.lblOutExp = New System.Windows.Forms.Label()
        Me.txtXOV = New System.Windows.Forms.TextBox()
        Me.lblXOV = New System.Windows.Forms.Label()
        Me.lblInExp = New System.Windows.Forms.Label()
        Me.grpInExp = New System.Windows.Forms.GroupBox()
        Me.lblXIS = New System.Windows.Forms.Label()
        Me.txtXIS = New System.Windows.Forms.RichTextBox()
        Me.txtXII = New System.Windows.Forms.TextBox()
        Me.txtXIT = New System.Windows.Forms.TextBox()
        Me.lblXII = New System.Windows.Forms.Label()
        Me.lblXIT = New System.Windows.Forms.Label()
        Me.lblTransExp = New System.Windows.Forms.Label()
        Me.grpTransExp = New System.Windows.Forms.GroupBox()
        Me.lblXTH = New System.Windows.Forms.Label()
        Me.txtXTH = New System.Windows.Forms.TextBox()
        Me.btnOutGetData = New System.Windows.Forms.Button()
        Me.btnINGetData = New System.Windows.Forms.Button()
        Me.lblXTO = New System.Windows.Forms.Label()
        Me.lstXTIn = New System.Windows.Forms.Label()
        Me.lstXTO = New System.Windows.Forms.ListBox()
        Me.lstXTI = New System.Windows.Forms.ListBox()
        Me.txtXTV = New System.Windows.Forms.TextBox()
        Me.txtXTI = New System.Windows.Forms.TextBox()
        Me.lblXTV = New System.Windows.Forms.Label()
        Me.lblXTI = New System.Windows.Forms.Label()
        Me.lblBlockExp = New System.Windows.Forms.Label()
        Me.grpBlockExp = New System.Windows.Forms.GroupBox()
        Me.lblXBH = New System.Windows.Forms.Label()
        Me.txtXBH = New System.Windows.Forms.TextBox()
        Me.btnDataMinus = New System.Windows.Forms.Button()
        Me.btnDataPlus = New System.Windows.Forms.Button()
        Me.btnTransGetData = New System.Windows.Forms.Button()
        Me.lblXBTs = New System.Windows.Forms.Label()
        Me.btnBlockGetData = New System.Windows.Forms.Button()
        Me.lstXBT = New System.Windows.Forms.ListBox()
        Me.txtXBN = New System.Windows.Forms.TextBox()
        Me.txtXBD = New System.Windows.Forms.TextBox()
        Me.txtXBT = New System.Windows.Forms.TextBox()
        Me.txtXBP = New System.Windows.Forms.TextBox()
        Me.txtXBM = New System.Windows.Forms.TextBox()
        Me.txtXBV = New System.Windows.Forms.TextBox()
        Me.txtXBB = New System.Windows.Forms.TextBox()
        Me.lblXBN = New System.Windows.Forms.Label()
        Me.lblXBD = New System.Windows.Forms.Label()
        Me.lblXBT = New System.Windows.Forms.Label()
        Me.lblXBM = New System.Windows.Forms.Label()
        Me.lblXBP = New System.Windows.Forms.Label()
        Me.lblXBV = New System.Windows.Forms.Label()
        Me.lblXBB = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TCPTimer = New System.Windows.Forms.Timer(Me.components)
        Me.Clock = New System.Windows.Forms.Timer(Me.components)
        Me.TabControl.SuspendLayout()
        Me.Wallet.SuspendLayout()
        CType(Me.picboxWarn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picboxLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grbAccInfo.SuspendLayout()
        Me.Send_Money.SuspendLayout()
        Me.Address_Book.SuspendLayout()
        Me.Mine.SuspendLayout()
        Me.grpMining.SuspendLayout()
        Me.Settings.SuspendLayout()
        Me.grpChainStat.SuspendLayout()
        Me.grpFileSet.SuspendLayout()
        Me.grpNetSet.SuspendLayout()
        Me.grbNetStat.SuspendLayout()
        Me.Console.SuspendLayout()
        Me.Blockchain_Explorer.SuspendLayout()
        Me.grpOutExp.SuspendLayout()
        Me.grpInExp.SuspendLayout()
        Me.grpTransExp.SuspendLayout()
        Me.grpBlockExp.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl
        '
        Me.TabControl.Controls.Add(Me.Wallet)
        Me.TabControl.Controls.Add(Me.Send_Money)
        Me.TabControl.Controls.Add(Me.Address_Book)
        Me.TabControl.Controls.Add(Me.Mine)
        Me.TabControl.Controls.Add(Me.Settings)
        Me.TabControl.Controls.Add(Me.Console)
        Me.TabControl.Controls.Add(Me.Blockchain_Explorer)
        Me.TabControl.Location = New System.Drawing.Point(0, 1)
        Me.TabControl.Name = "TabControl"
        Me.TabControl.SelectedIndex = 0
        Me.TabControl.Size = New System.Drawing.Size(876, 478)
        Me.TabControl.TabIndex = 0
        '
        'Wallet
        '
        Me.Wallet.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Wallet.Controls.Add(Me.picboxWarn)
        Me.Wallet.Controls.Add(Me.btnCopyRight)
        Me.Wallet.Controls.Add(Me.btnHelp)
        Me.Wallet.Controls.Add(Me.picboxLogo)
        Me.Wallet.Controls.Add(Me.btnConnect)
        Me.Wallet.Controls.Add(Me.lblVersion)
        Me.Wallet.Controls.Add(Me.lblBlocksRem)
        Me.Wallet.Controls.Add(Me.prbBlocks)
        Me.Wallet.Controls.Add(Me.lblSync1)
        Me.Wallet.Controls.Add(Me.lblStat1)
        Me.Wallet.Controls.Add(Me.lblSyncStat1)
        Me.Wallet.Controls.Add(Me.lblNetStat1)
        Me.Wallet.Controls.Add(Me.btnLogin)
        Me.Wallet.Controls.Add(Me.lblSuffix2)
        Me.Wallet.Controls.Add(Me.lblSuffix)
        Me.Wallet.Controls.Add(Me.txtUnBal)
        Me.Wallet.Controls.Add(Me.txtBal)
        Me.Wallet.Controls.Add(Me.txtPubKey)
        Me.Wallet.Controls.Add(Me.txtPriKey)
        Me.Wallet.Controls.Add(Me.txtLogin)
        Me.Wallet.Controls.Add(Me.lblBalanceInfo)
        Me.Wallet.Controls.Add(Me.lblUnconfirmedBalance)
        Me.Wallet.Controls.Add(Me.lblBalance)
        Me.Wallet.Controls.Add(Me.lblPublicKey)
        Me.Wallet.Controls.Add(Me.lblPrivateKey)
        Me.Wallet.Controls.Add(Me.lblLoginWord)
        Me.Wallet.Controls.Add(Me.lblAccountInfo)
        Me.Wallet.Controls.Add(Me.grbAccInfo)
        Me.Wallet.Controls.Add(Me.grbAccBal)
        Me.Wallet.Location = New System.Drawing.Point(4, 22)
        Me.Wallet.Name = "Wallet"
        Me.Wallet.Padding = New System.Windows.Forms.Padding(3)
        Me.Wallet.Size = New System.Drawing.Size(868, 452)
        Me.Wallet.TabIndex = 0
        Me.Wallet.Text = "Wallet"
        '
        'picboxWarn
        '
        Me.picboxWarn.Image = CType(resources.GetObject("picboxWarn.Image"), System.Drawing.Image)
        Me.picboxWarn.Location = New System.Drawing.Point(147, 352)
        Me.picboxWarn.Name = "picboxWarn"
        Me.picboxWarn.Size = New System.Drawing.Size(45, 57)
        Me.picboxWarn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picboxWarn.TabIndex = 29
        Me.picboxWarn.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picboxWarn, resources.GetString("picboxWarn.ToolTip"))
        Me.picboxWarn.Visible = False
        '
        'btnCopyRight
        '
        Me.btnCopyRight.Location = New System.Drawing.Point(828, 4)
        Me.btnCopyRight.Name = "btnCopyRight"
        Me.btnCopyRight.Size = New System.Drawing.Size(25, 23)
        Me.btnCopyRight.TabIndex = 28
        Me.btnCopyRight.Text = "©"
        Me.ToolTip1.SetToolTip(Me.btnCopyRight, "Click for copyright information about this program.")
        Me.btnCopyRight.UseVisualStyleBackColor = True
        '
        'btnHelp
        '
        Me.btnHelp.Location = New System.Drawing.Point(797, 4)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(25, 23)
        Me.btnHelp.TabIndex = 27
        Me.btnHelp.Text = "?"
        Me.ToolTip1.SetToolTip(Me.btnHelp, "Click for information to help you use the program.")
        Me.btnHelp.UseVisualStyleBackColor = True
        '
        'picboxLogo
        '
        Me.picboxLogo.Image = CType(resources.GetObject("picboxLogo.Image"), System.Drawing.Image)
        Me.picboxLogo.Location = New System.Drawing.Point(509, 193)
        Me.picboxLogo.Name = "picboxLogo"
        Me.picboxLogo.Size = New System.Drawing.Size(215, 186)
        Me.picboxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picboxLogo.TabIndex = 26
        Me.picboxLogo.TabStop = False
        '
        'btnConnect
        '
        Me.btnConnect.Location = New System.Drawing.Point(20, 370)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(123, 23)
        Me.btnConnect.TabIndex = 23
        Me.btnConnect.Text = "Connect"
        Me.ToolTip1.SetToolTip(Me.btnConnect, "Connect and sync to the network. You must be successfully connected to the networ" & _
                "k" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "before you will be able to send and receive transactions.")
        Me.btnConnect.UseVisualStyleBackColor = True
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVersion.Location = New System.Drawing.Point(792, 370)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(26, 20)
        Me.lblVersion.TabIndex = 22
        Me.lblVersion.Text = "V "
        Me.ToolTip1.SetToolTip(Me.lblVersion, "Current version number of this client.")
        '
        'lblBlocksRem
        '
        Me.lblBlocksRem.AutoSize = True
        Me.lblBlocksRem.BackColor = System.Drawing.Color.Transparent
        Me.lblBlocksRem.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBlocksRem.Location = New System.Drawing.Point(352, 390)
        Me.lblBlocksRem.Name = "lblBlocksRem"
        Me.lblBlocksRem.Size = New System.Drawing.Size(123, 16)
        Me.lblBlocksRem.TabIndex = 21
        Me.lblBlocksRem.Text = "Blocks Remaining: "
        '
        'prbBlocks
        '
        Me.prbBlocks.Location = New System.Drawing.Point(355, 409)
        Me.prbBlocks.Name = "prbBlocks"
        Me.prbBlocks.Size = New System.Drawing.Size(493, 32)
        Me.prbBlocks.TabIndex = 20
        Me.ToolTip1.SetToolTip(Me.prbBlocks, "Indicateds the status of syncing with the network. You will not be able to" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "use m" & _
                "ost of the programs features until your account is in sync.")
        '
        'lblSync1
        '
        Me.lblSync1.AutoSize = True
        Me.lblSync1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSync1.ForeColor = System.Drawing.Color.Red
        Me.lblSync1.Location = New System.Drawing.Point(265, 423)
        Me.lblSync1.Name = "lblSync1"
        Me.lblSync1.Size = New System.Drawing.Size(74, 13)
        Me.lblSync1.TabIndex = 19
        Me.lblSync1.Text = "Out of Sync"
        Me.ToolTip1.SetToolTip(Me.lblSync1, resources.GetString("lblSync1.ToolTip"))
        '
        'lblStat1
        '
        Me.lblStat1.AutoSize = True
        Me.lblStat1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStat1.ForeColor = System.Drawing.Color.Red
        Me.lblStat1.Location = New System.Drawing.Point(102, 422)
        Me.lblStat1.Name = "lblStat1"
        Me.lblStat1.Size = New System.Drawing.Size(44, 13)
        Me.lblStat1.TabIndex = 18
        Me.lblStat1.Text = "Offline"
        Me.ToolTip1.SetToolTip(Me.lblStat1, "You can login to your Halfcoin account and create new addresses while offline." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "T" & _
                "o send and receive funds, mine and check your balance you must be connected" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "to " & _
                "the to the network.")
        '
        'lblSyncStat1
        '
        Me.lblSyncStat1.AutoSize = True
        Me.lblSyncStat1.Location = New System.Drawing.Point(176, 423)
        Me.lblSyncStat1.Name = "lblSyncStat1"
        Me.lblSyncStat1.Size = New System.Drawing.Size(85, 13)
        Me.lblSyncStat1.TabIndex = 17
        Me.lblSyncStat1.Text = "Synchronization:"
        '
        'lblNetStat1
        '
        Me.lblNetStat1.AutoSize = True
        Me.lblNetStat1.Location = New System.Drawing.Point(15, 422)
        Me.lblNetStat1.Name = "lblNetStat1"
        Me.lblNetStat1.Size = New System.Drawing.Size(83, 13)
        Me.lblNetStat1.TabIndex = 16
        Me.lblNetStat1.Text = "Network Status:"
        '
        'btnLogin
        '
        Me.btnLogin.Location = New System.Drawing.Point(725, 56)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(123, 23)
        Me.btnLogin.TabIndex = 14
        Me.btnLogin.Text = "Login"
        Me.ToolTip1.SetToolTip(Me.btnLogin, "Click to login using the password/login word entered in the" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "login word box to th" & _
                "e left.")
        Me.btnLogin.UseVisualStyleBackColor = True
        '
        'lblSuffix2
        '
        Me.lblSuffix2.AutoSize = True
        Me.lblSuffix2.Location = New System.Drawing.Point(216, 256)
        Me.lblSuffix2.Name = "lblSuffix2"
        Me.lblSuffix2.Size = New System.Drawing.Size(29, 13)
        Me.lblSuffix2.TabIndex = 13
        Me.lblSuffix2.Text = "HAC"
        '
        'lblSuffix
        '
        Me.lblSuffix.AutoSize = True
        Me.lblSuffix.Location = New System.Drawing.Point(216, 218)
        Me.lblSuffix.Name = "lblSuffix"
        Me.lblSuffix.Size = New System.Drawing.Size(29, 13)
        Me.lblSuffix.TabIndex = 12
        Me.lblSuffix.Text = "HAC"
        '
        'txtUnBal
        '
        Me.txtUnBal.Location = New System.Drawing.Point(88, 253)
        Me.txtUnBal.Name = "txtUnBal"
        Me.txtUnBal.ReadOnly = True
        Me.txtUnBal.Size = New System.Drawing.Size(122, 20)
        Me.txtUnBal.TabIndex = 11
        Me.txtUnBal.Text = "0"
        Me.ToolTip1.SetToolTip(Me.txtUnBal, resources.GetString("txtUnBal.ToolTip"))
        '
        'txtBal
        '
        Me.txtBal.Location = New System.Drawing.Point(88, 215)
        Me.txtBal.Name = "txtBal"
        Me.txtBal.ReadOnly = True
        Me.txtBal.Size = New System.Drawing.Size(122, 20)
        Me.txtBal.TabIndex = 10
        Me.txtBal.Text = "0"
        Me.ToolTip1.SetToolTip(Me.txtBal, "This is the balance of your account.")
        '
        'txtPubKey
        '
        Me.txtPubKey.Location = New System.Drawing.Point(88, 126)
        Me.txtPubKey.Name = "txtPubKey"
        Me.txtPubKey.ReadOnly = True
        Me.txtPubKey.Size = New System.Drawing.Size(620, 20)
        Me.txtPubKey.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.txtPubKey, "This is your accounts public key, it will not change. This is required by the sen" & _
                "der if you wish to receive" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "funds from another account. You can store other peop" & _
                "les public keys in your address book.")
        '
        'txtPriKey
        '
        Me.txtPriKey.Location = New System.Drawing.Point(88, 92)
        Me.txtPriKey.Name = "txtPriKey"
        Me.txtPriKey.ReadOnly = True
        Me.txtPriKey.Size = New System.Drawing.Size(620, 20)
        Me.txtPriKey.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.txtPriKey, resources.GetString("txtPriKey.ToolTip"))
        '
        'txtLogin
        '
        Me.txtLogin.Location = New System.Drawing.Point(88, 58)
        Me.txtLogin.Name = "txtLogin"
        Me.txtLogin.Size = New System.Drawing.Size(620, 20)
        Me.txtLogin.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.txtLogin, resources.GetString("txtLogin.ToolTip"))
        '
        'lblBalanceInfo
        '
        Me.lblBalanceInfo.AutoSize = True
        Me.lblBalanceInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalanceInfo.Location = New System.Drawing.Point(17, 173)
        Me.lblBalanceInfo.Name = "lblBalanceInfo"
        Me.lblBalanceInfo.Size = New System.Drawing.Size(145, 20)
        Me.lblBalanceInfo.TabIndex = 6
        Me.lblBalanceInfo.Text = "Account Balance"
        Me.ToolTip1.SetToolTip(Me.lblBalanceInfo, "Information about the balance of your account.")
        '
        'lblUnconfirmedBalance
        '
        Me.lblUnconfirmedBalance.AutoSize = True
        Me.lblUnconfirmedBalance.Location = New System.Drawing.Point(15, 250)
        Me.lblUnconfirmedBalance.Name = "lblUnconfirmedBalance"
        Me.lblUnconfirmedBalance.Size = New System.Drawing.Size(71, 26)
        Me.lblUnconfirmedBalance.TabIndex = 5
        Me.lblUnconfirmedBalance.Text = "Unconfirmed" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Transactions:"
        '
        'lblBalance
        '
        Me.lblBalance.AutoSize = True
        Me.lblBalance.Location = New System.Drawing.Point(33, 217)
        Me.lblBalance.Name = "lblBalance"
        Me.lblBalance.Size = New System.Drawing.Size(49, 13)
        Me.lblBalance.TabIndex = 4
        Me.lblBalance.Text = "Balance:"
        '
        'lblPublicKey
        '
        Me.lblPublicKey.AutoSize = True
        Me.lblPublicKey.Location = New System.Drawing.Point(22, 129)
        Me.lblPublicKey.Name = "lblPublicKey"
        Me.lblPublicKey.Size = New System.Drawing.Size(60, 13)
        Me.lblPublicKey.TabIndex = 3
        Me.lblPublicKey.Text = "Public Key:"
        '
        'lblPrivateKey
        '
        Me.lblPrivateKey.AutoSize = True
        Me.lblPrivateKey.Location = New System.Drawing.Point(18, 96)
        Me.lblPrivateKey.Name = "lblPrivateKey"
        Me.lblPrivateKey.Size = New System.Drawing.Size(64, 13)
        Me.lblPrivateKey.TabIndex = 2
        Me.lblPrivateKey.Text = "Private Key:"
        '
        'lblLoginWord
        '
        Me.lblLoginWord.AutoSize = True
        Me.lblLoginWord.Location = New System.Drawing.Point(17, 61)
        Me.lblLoginWord.Name = "lblLoginWord"
        Me.lblLoginWord.Size = New System.Drawing.Size(65, 13)
        Me.lblLoginWord.TabIndex = 1
        Me.lblLoginWord.Text = "Login Word:"
        '
        'lblAccountInfo
        '
        Me.lblAccountInfo.AutoSize = True
        Me.lblAccountInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAccountInfo.Location = New System.Drawing.Point(17, 18)
        Me.lblAccountInfo.Name = "lblAccountInfo"
        Me.lblAccountInfo.Size = New System.Drawing.Size(172, 20)
        Me.lblAccountInfo.TabIndex = 0
        Me.lblAccountInfo.Text = "Account Information"
        Me.ToolTip1.SetToolTip(Me.lblAccountInfo, "Information about your account.")
        '
        'grbAccInfo
        '
        Me.grbAccInfo.Controls.Add(Me.btnHidLog)
        Me.grbAccInfo.Controls.Add(Me.btnHidePri)
        Me.grbAccInfo.Location = New System.Drawing.Point(10, 23)
        Me.grbAccInfo.Name = "grbAccInfo"
        Me.grbAccInfo.Size = New System.Drawing.Size(849, 139)
        Me.grbAccInfo.TabIndex = 24
        Me.grbAccInfo.TabStop = False
        '
        'btnHidLog
        '
        Me.btnHidLog.Location = New System.Drawing.Point(715, 68)
        Me.btnHidLog.Name = "btnHidLog"
        Me.btnHidLog.Size = New System.Drawing.Size(123, 23)
        Me.btnHidLog.TabIndex = 16
        Me.btnHidLog.Text = "Hide Login Word"
        Me.ToolTip1.SetToolTip(Me.btnHidLog, "Click to hide your login word from view. Useful if other" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "people can view your sc" & _
                "reen.")
        Me.btnHidLog.UseVisualStyleBackColor = True
        '
        'btnHidePri
        '
        Me.btnHidePri.Location = New System.Drawing.Point(715, 101)
        Me.btnHidePri.Name = "btnHidePri"
        Me.btnHidePri.Size = New System.Drawing.Size(123, 23)
        Me.btnHidePri.TabIndex = 15
        Me.btnHidePri.Text = "Hide Private Key"
        Me.ToolTip1.SetToolTip(Me.btnHidePri, "Click to hide your private key from view. Useful if other" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "people can view your s" & _
                "creen.")
        Me.btnHidePri.UseVisualStyleBackColor = True
        '
        'grbAccBal
        '
        Me.grbAccBal.Location = New System.Drawing.Point(10, 177)
        Me.grbAccBal.Name = "grbAccBal"
        Me.grbAccBal.Size = New System.Drawing.Size(251, 113)
        Me.grbAccBal.TabIndex = 25
        Me.grbAccBal.TabStop = False
        Me.grbAccBal.Text = "GroupBox1"
        '
        'Send_Money
        '
        Me.Send_Money.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Send_Money.Controls.Add(Me.lblOptional)
        Me.Send_Money.Controls.Add(Me.btnSend)
        Me.Send_Money.Controls.Add(Me.lblSuffix4)
        Me.Send_Money.Controls.Add(Me.lblSuffix3)
        Me.Send_Money.Controls.Add(Me.txtTransFee)
        Me.Send_Money.Controls.Add(Me.txtSendAmount)
        Me.Send_Money.Controls.Add(Me.lblTransFee)
        Me.Send_Money.Controls.Add(Me.lblSendAmount)
        Me.Send_Money.Controls.Add(Me.txtRepAdr)
        Me.Send_Money.Controls.Add(Me.lblRAddress)
        Me.Send_Money.Controls.Add(Me.txtRepPubKey)
        Me.Send_Money.Controls.Add(Me.lblRPublicKey)
        Me.Send_Money.Controls.Add(Me.btnNewAdr)
        Me.Send_Money.Controls.Add(Me.txtAdr)
        Me.Send_Money.Controls.Add(Me.lblTransDetails)
        Me.Send_Money.Controls.Add(Me.lblAddress)
        Me.Send_Money.Controls.Add(Me.lblAddresses)
        Me.Send_Money.Controls.Add(Me.grbYourAdr)
        Me.Send_Money.Controls.Add(Me.grbTransDet)
        Me.Send_Money.Location = New System.Drawing.Point(4, 22)
        Me.Send_Money.Name = "Send_Money"
        Me.Send_Money.Padding = New System.Windows.Forms.Padding(3)
        Me.Send_Money.Size = New System.Drawing.Size(868, 452)
        Me.Send_Money.TabIndex = 1
        Me.Send_Money.Text = "Send Money"
        Me.ToolTip1.SetToolTip(Me.Send_Money, "Create transactions")
        '
        'lblOptional
        '
        Me.lblOptional.AutoSize = True
        Me.lblOptional.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOptional.Location = New System.Drawing.Point(16, 291)
        Me.lblOptional.Name = "lblOptional"
        Me.lblOptional.Size = New System.Drawing.Size(146, 13)
        Me.lblOptional.TabIndex = 33
        Me.lblOptional.Text = "(Minimum of 10 required)"
        Me.ToolTip1.SetToolTip(Me.lblOptional, resources.GetString("lblOptional.ToolTip"))
        '
        'btnSend
        '
        Me.btnSend.Location = New System.Drawing.Point(716, 267)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(123, 23)
        Me.btnSend.TabIndex = 32
        Me.btnSend.Text = "Send"
        Me.ToolTip1.SetToolTip(Me.btnSend, "Confirm the transaction and send the funds to the entered account." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(You will be " & _
                "prompted again before the transaction is final.)")
        Me.btnSend.UseVisualStyleBackColor = True
        '
        'lblSuffix4
        '
        Me.lblSuffix4.AutoSize = True
        Me.lblSuffix4.Location = New System.Drawing.Point(259, 267)
        Me.lblSuffix4.Name = "lblSuffix4"
        Me.lblSuffix4.Size = New System.Drawing.Size(29, 13)
        Me.lblSuffix4.TabIndex = 31
        Me.lblSuffix4.Text = "HAC"
        '
        'lblSuffix3
        '
        Me.lblSuffix3.AutoSize = True
        Me.lblSuffix3.Location = New System.Drawing.Point(259, 229)
        Me.lblSuffix3.Name = "lblSuffix3"
        Me.lblSuffix3.Size = New System.Drawing.Size(29, 13)
        Me.lblSuffix3.TabIndex = 30
        Me.lblSuffix3.Text = "HAC"
        '
        'txtTransFee
        '
        Me.txtTransFee.Location = New System.Drawing.Point(131, 264)
        Me.txtTransFee.Name = "txtTransFee"
        Me.txtTransFee.Size = New System.Drawing.Size(122, 20)
        Me.txtTransFee.TabIndex = 29
        Me.ToolTip1.SetToolTip(Me.txtTransFee, "Enter any amount of Halfcoins as a transaction fees to the miner. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "A minimum of " & _
                "10 is required to prevent spam transactions.")
        '
        'txtSendAmount
        '
        Me.txtSendAmount.Location = New System.Drawing.Point(131, 226)
        Me.txtSendAmount.Name = "txtSendAmount"
        Me.txtSendAmount.Size = New System.Drawing.Size(122, 20)
        Me.txtSendAmount.TabIndex = 28
        Me.ToolTip1.SetToolTip(Me.txtSendAmount, "Enter the amount of Halfcoins you wish to send.")
        '
        'lblTransFee
        '
        Me.lblTransFee.AutoSize = True
        Me.lblTransFee.Location = New System.Drawing.Point(37, 267)
        Me.lblTransFee.Name = "lblTransFee"
        Me.lblTransFee.Size = New System.Drawing.Size(87, 13)
        Me.lblTransFee.TabIndex = 26
        Me.lblTransFee.Text = "Transaction Fee:"
        '
        'lblSendAmount
        '
        Me.lblSendAmount.AutoSize = True
        Me.lblSendAmount.Location = New System.Drawing.Point(51, 229)
        Me.lblSendAmount.Name = "lblSendAmount"
        Me.lblSendAmount.Size = New System.Drawing.Size(74, 13)
        Me.lblSendAmount.TabIndex = 24
        Me.lblSendAmount.Text = "Send Amount:"
        '
        'txtRepAdr
        '
        Me.txtRepAdr.Location = New System.Drawing.Point(131, 185)
        Me.txtRepAdr.Name = "txtRepAdr"
        Me.txtRepAdr.Size = New System.Drawing.Size(568, 20)
        Me.txtRepAdr.TabIndex = 23
        Me.ToolTip1.SetToolTip(Me.txtRepAdr, "Enter an address of the account you wish to send funds to.")
        '
        'lblRAddress
        '
        Me.lblRAddress.AutoSize = True
        Me.lblRAddress.Location = New System.Drawing.Point(29, 188)
        Me.lblRAddress.Name = "lblRAddress"
        Me.lblRAddress.Size = New System.Drawing.Size(96, 13)
        Me.lblRAddress.TabIndex = 22
        Me.lblRAddress.Text = "Recipient Address:"
        '
        'txtRepPubKey
        '
        Me.txtRepPubKey.Location = New System.Drawing.Point(131, 147)
        Me.txtRepPubKey.Name = "txtRepPubKey"
        Me.txtRepPubKey.Size = New System.Drawing.Size(568, 20)
        Me.txtRepPubKey.TabIndex = 21
        Me.ToolTip1.SetToolTip(Me.txtRepPubKey, "Enter the public key of the account you wish to send funds to. Keys saved in the " & _
                "address book can be" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "inputted directly using the ""Select"" button in the address " & _
                "book tab.")
        '
        'lblRPublicKey
        '
        Me.lblRPublicKey.AutoSize = True
        Me.lblRPublicKey.Location = New System.Drawing.Point(17, 150)
        Me.lblRPublicKey.Name = "lblRPublicKey"
        Me.lblRPublicKey.Size = New System.Drawing.Size(108, 13)
        Me.lblRPublicKey.TabIndex = 20
        Me.lblRPublicKey.Text = "Recipient Public Key:"
        '
        'btnNewAdr
        '
        Me.btnNewAdr.Location = New System.Drawing.Point(716, 53)
        Me.btnNewAdr.Name = "btnNewAdr"
        Me.btnNewAdr.Size = New System.Drawing.Size(123, 23)
        Me.btnNewAdr.TabIndex = 19
        Me.btnNewAdr.Text = "New Address"
        Me.ToolTip1.SetToolTip(Me.btnNewAdr, resources.GetString("btnNewAdr.ToolTip"))
        Me.btnNewAdr.UseVisualStyleBackColor = True
        '
        'txtAdr
        '
        Me.txtAdr.Location = New System.Drawing.Point(131, 56)
        Me.txtAdr.Name = "txtAdr"
        Me.txtAdr.ReadOnly = True
        Me.txtAdr.Size = New System.Drawing.Size(568, 20)
        Me.txtAdr.TabIndex = 18
        Me.ToolTip1.SetToolTip(Me.txtAdr, resources.GetString("txtAdr.ToolTip"))
        '
        'lblTransDetails
        '
        Me.lblTransDetails.AutoSize = True
        Me.lblTransDetails.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTransDetails.Location = New System.Drawing.Point(32, 102)
        Me.lblTransDetails.Name = "lblTransDetails"
        Me.lblTransDetails.Size = New System.Drawing.Size(164, 20)
        Me.lblTransDetails.TabIndex = 17
        Me.lblTransDetails.Text = "Transaction Details"
        Me.ToolTip1.SetToolTip(Me.lblTransDetails, "Create and send transaction here.")
        '
        'lblAddress
        '
        Me.lblAddress.AutoSize = True
        Me.lblAddress.Location = New System.Drawing.Point(60, 59)
        Me.lblAddress.Name = "lblAddress"
        Me.lblAddress.Size = New System.Drawing.Size(48, 13)
        Me.lblAddress.TabIndex = 16
        Me.lblAddress.Text = "Address:"
        '
        'lblAddresses
        '
        Me.lblAddresses.AutoSize = True
        Me.lblAddresses.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddresses.Location = New System.Drawing.Point(32, 17)
        Me.lblAddresses.Name = "lblAddresses"
        Me.lblAddresses.Size = New System.Drawing.Size(137, 20)
        Me.lblAddresses.TabIndex = 15
        Me.lblAddresses.Text = "Your Addresses"
        Me.ToolTip1.SetToolTip(Me.lblAddresses, "Addresses for your account.")
        '
        'grbYourAdr
        '
        Me.grbYourAdr.Location = New System.Drawing.Point(8, 19)
        Me.grbYourAdr.Name = "grbYourAdr"
        Me.grbYourAdr.Size = New System.Drawing.Size(849, 78)
        Me.grbYourAdr.TabIndex = 34
        Me.grbYourAdr.TabStop = False
        '
        'grbTransDet
        '
        Me.grbTransDet.Location = New System.Drawing.Point(8, 105)
        Me.grbTransDet.Name = "grbTransDet"
        Me.grbTransDet.Size = New System.Drawing.Size(849, 210)
        Me.grbTransDet.TabIndex = 35
        Me.grbTransDet.TabStop = False
        '
        'Address_Book
        '
        Me.Address_Book.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Address_Book.Controls.Add(Me.txtLabel)
        Me.Address_Book.Controls.Add(Me.lblLabel)
        Me.Address_Book.Controls.Add(Me.btnDel)
        Me.Address_Book.Controls.Add(Me.btnAddNew)
        Me.Address_Book.Controls.Add(Me.btnSelect)
        Me.Address_Book.Controls.Add(Me.txtEnterPubKey)
        Me.Address_Book.Controls.Add(Me.lblPublicKey1)
        Me.Address_Book.Controls.Add(Me.lstAdr)
        Me.Address_Book.Controls.Add(Me.lblAdrBook)
        Me.Address_Book.Controls.Add(Me.grpAdrBook)
        Me.Address_Book.Location = New System.Drawing.Point(4, 22)
        Me.Address_Book.Name = "Address_Book"
        Me.Address_Book.Size = New System.Drawing.Size(868, 452)
        Me.Address_Book.TabIndex = 5
        Me.Address_Book.Text = "Address Book"
        Me.ToolTip1.SetToolTip(Me.Address_Book, "Store peoples public keys along with their name using the address book.")
        '
        'txtLabel
        '
        Me.txtLabel.Location = New System.Drawing.Point(92, 91)
        Me.txtLabel.Name = "txtLabel"
        Me.txtLabel.Size = New System.Drawing.Size(637, 20)
        Me.txtLabel.TabIndex = 24
        Me.ToolTip1.SetToolTip(Me.txtLabel, "Enter a label for this public key. (e.g John Smith)")
        '
        'lblLabel
        '
        Me.lblLabel.AutoSize = True
        Me.lblLabel.Location = New System.Drawing.Point(49, 94)
        Me.lblLabel.Name = "lblLabel"
        Me.lblLabel.Size = New System.Drawing.Size(36, 13)
        Me.lblLabel.TabIndex = 23
        Me.lblLabel.Text = "Label:"
        '
        'btnDel
        '
        Me.btnDel.Location = New System.Drawing.Point(742, 89)
        Me.btnDel.Name = "btnDel"
        Me.btnDel.Size = New System.Drawing.Size(94, 23)
        Me.btnDel.TabIndex = 22
        Me.btnDel.Text = "Delete"
        Me.ToolTip1.SetToolTip(Me.btnDel, "Delete selected address from your address book.")
        Me.btnDel.UseVisualStyleBackColor = True
        '
        'btnAddNew
        '
        Me.btnAddNew.Location = New System.Drawing.Point(742, 60)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(94, 23)
        Me.btnAddNew.TabIndex = 21
        Me.btnAddNew.Text = "Add New"
        Me.ToolTip1.SetToolTip(Me.btnAddNew, "Add a new public key and label to your address book.")
        Me.btnAddNew.UseVisualStyleBackColor = True
        '
        'btnSelect
        '
        Me.btnSelect.Location = New System.Drawing.Point(742, 31)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(94, 23)
        Me.btnSelect.TabIndex = 20
        Me.btnSelect.Text = "Select"
        Me.ToolTip1.SetToolTip(Me.btnSelect, "Select an address from your address book to send funds to.")
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'txtEnterPubKey
        '
        Me.txtEnterPubKey.Location = New System.Drawing.Point(92, 62)
        Me.txtEnterPubKey.Name = "txtEnterPubKey"
        Me.txtEnterPubKey.Size = New System.Drawing.Size(637, 20)
        Me.txtEnterPubKey.TabIndex = 19
        Me.ToolTip1.SetToolTip(Me.txtEnterPubKey, "Enter a public key to add to your address book.")
        '
        'lblPublicKey1
        '
        Me.lblPublicKey1.AutoSize = True
        Me.lblPublicKey1.Location = New System.Drawing.Point(25, 65)
        Me.lblPublicKey1.Name = "lblPublicKey1"
        Me.lblPublicKey1.Size = New System.Drawing.Size(60, 13)
        Me.lblPublicKey1.TabIndex = 18
        Me.lblPublicKey1.Text = "Public Key:"
        '
        'lstAdr
        '
        Me.lstAdr.FormattingEnabled = True
        Me.lstAdr.Location = New System.Drawing.Point(17, 127)
        Me.lstAdr.Name = "lstAdr"
        Me.lstAdr.Size = New System.Drawing.Size(833, 316)
        Me.lstAdr.TabIndex = 17
        '
        'lblAdrBook
        '
        Me.lblAdrBook.AutoSize = True
        Me.lblAdrBook.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAdrBook.Location = New System.Drawing.Point(23, 13)
        Me.lblAdrBook.Name = "lblAdrBook"
        Me.lblAdrBook.Size = New System.Drawing.Size(140, 20)
        Me.lblAdrBook.TabIndex = 16
        Me.lblAdrBook.Text = "Addresses Book"
        Me.ToolTip1.SetToolTip(Me.lblAdrBook, "Add contacts and select contacts to send fund to using this.")
        '
        'grpAdrBook
        '
        Me.grpAdrBook.Location = New System.Drawing.Point(17, 17)
        Me.grpAdrBook.Name = "grpAdrBook"
        Me.grpAdrBook.Size = New System.Drawing.Size(833, 105)
        Me.grpAdrBook.TabIndex = 35
        Me.grpAdrBook.TabStop = False
        '
        'Mine
        '
        Me.Mine.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Mine.Controls.Add(Me.txtTime)
        Me.Mine.Controls.Add(Me.lblTimeElapsed)
        Me.Mine.Controls.Add(Me.btnStopMine)
        Me.Mine.Controls.Add(Me.txtConsoleMine)
        Me.Mine.Controls.Add(Me.btnStartMine)
        Me.Mine.Controls.Add(Me.grpMining)
        Me.Mine.Location = New System.Drawing.Point(4, 22)
        Me.Mine.Name = "Mine"
        Me.Mine.Size = New System.Drawing.Size(868, 452)
        Me.Mine.TabIndex = 2
        Me.Mine.Text = "Mine"
        Me.ToolTip1.SetToolTip(Me.Mine, "Mine for Halfcoins using this tool")
        '
        'txtTime
        '
        Me.txtTime.Location = New System.Drawing.Point(151, 126)
        Me.txtTime.Name = "txtTime"
        Me.txtTime.ReadOnly = True
        Me.txtTime.Size = New System.Drawing.Size(55, 20)
        Me.txtTime.TabIndex = 30
        Me.txtTime.Text = "0"
        Me.ToolTip1.SetToolTip(Me.txtTime, "Total time spent mining the current block in seconds.")
        '
        'lblTimeElapsed
        '
        Me.lblTimeElapsed.AutoSize = True
        Me.lblTimeElapsed.Location = New System.Drawing.Point(29, 129)
        Me.lblTimeElapsed.Name = "lblTimeElapsed"
        Me.lblTimeElapsed.Size = New System.Drawing.Size(74, 13)
        Me.lblTimeElapsed.TabIndex = 29
        Me.lblTimeElapsed.Text = "Time Elapsed:"
        '
        'btnStopMine
        '
        Me.btnStopMine.Location = New System.Drawing.Point(18, 78)
        Me.btnStopMine.Name = "btnStopMine"
        Me.btnStopMine.Size = New System.Drawing.Size(188, 29)
        Me.btnStopMine.TabIndex = 22
        Me.btnStopMine.Text = "Stop Mining"
        Me.ToolTip1.SetToolTip(Me.btnStopMine, "Stop the mining process.")
        Me.btnStopMine.UseVisualStyleBackColor = True
        '
        'txtConsoleMine
        '
        Me.txtConsoleMine.BackColor = System.Drawing.Color.White
        Me.txtConsoleMine.Location = New System.Drawing.Point(223, 14)
        Me.txtConsoleMine.Name = "txtConsoleMine"
        Me.txtConsoleMine.ReadOnly = True
        Me.txtConsoleMine.Size = New System.Drawing.Size(632, 422)
        Me.txtConsoleMine.TabIndex = 21
        Me.txtConsoleMine.Text = ""
        '
        'btnStartMine
        '
        Me.btnStartMine.Location = New System.Drawing.Point(18, 43)
        Me.btnStartMine.Name = "btnStartMine"
        Me.btnStartMine.Size = New System.Drawing.Size(188, 29)
        Me.btnStartMine.TabIndex = 20
        Me.btnStartMine.Text = "Start Mining"
        Me.ToolTip1.SetToolTip(Me.btnStartMine, resources.GetString("btnStartMine.ToolTip"))
        Me.btnStartMine.UseVisualStyleBackColor = True
        '
        'grpMining
        '
        Me.grpMining.Controls.Add(Me.ckbStop)
        Me.grpMining.Controls.Add(Me.lblHashUnit)
        Me.grpMining.Controls.Add(Me.txtHashRate)
        Me.grpMining.Controls.Add(Me.lblHashRate)
        Me.grpMining.Controls.Add(Me.cmbCores)
        Me.grpMining.Controls.Add(Me.lblCores)
        Me.grpMining.Controls.Add(Me.txtBlocksMissed)
        Me.grpMining.Controls.Add(Me.lblBlocksMissed)
        Me.grpMining.Controls.Add(Me.txtBlocksMined)
        Me.grpMining.Controls.Add(Me.txtTotalTime)
        Me.grpMining.Controls.Add(Me.lblBlocksMined)
        Me.grpMining.Controls.Add(Me.lblTotalTimeElapsed)
        Me.grpMining.Controls.Add(Me.lblMining)
        Me.grpMining.Location = New System.Drawing.Point(8, 10)
        Me.grpMining.Name = "grpMining"
        Me.grpMining.Size = New System.Drawing.Size(209, 426)
        Me.grpMining.TabIndex = 42
        Me.grpMining.TabStop = False
        '
        'ckbStop
        '
        Me.ckbStop.AutoSize = True
        Me.ckbStop.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ckbStop.Location = New System.Drawing.Point(20, 263)
        Me.ckbStop.Name = "ckbStop"
        Me.ckbStop.Size = New System.Drawing.Size(93, 30)
        Me.ckbStop.TabIndex = 58
        Me.ckbStop.Text = "Stop On Next " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Mined Block:"
        Me.ToolTip1.SetToolTip(Me.ckbStop, "Check this option to automatically stop mining after one successful block.")
        Me.ckbStop.UseVisualStyleBackColor = True
        '
        'lblHashUnit
        '
        Me.lblHashUnit.AutoSize = True
        Me.lblHashUnit.Location = New System.Drawing.Point(178, 400)
        Me.lblHashUnit.Name = "lblHashUnit"
        Me.lblHashUnit.Size = New System.Drawing.Size(25, 13)
        Me.lblHashUnit.TabIndex = 57
        Me.lblHashUnit.Text = "H/s"
        '
        'txtHashRate
        '
        Me.txtHashRate.Location = New System.Drawing.Point(85, 397)
        Me.txtHashRate.Name = "txtHashRate"
        Me.txtHashRate.ReadOnly = True
        Me.txtHashRate.Size = New System.Drawing.Size(91, 20)
        Me.txtHashRate.TabIndex = 56
        Me.ToolTip1.SetToolTip(Me.txtHashRate, "The speed at which this node is hashing measure in hashes per second." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "This will " & _
                "be calculated and displayed after one mine attempt.")
        '
        'lblHashRate
        '
        Me.lblHashRate.AutoSize = True
        Me.lblHashRate.Location = New System.Drawing.Point(18, 400)
        Me.lblHashRate.Name = "lblHashRate"
        Me.lblHashRate.Size = New System.Drawing.Size(61, 13)
        Me.lblHashRate.TabIndex = 55
        Me.lblHashRate.Text = "Hash Rate:"
        '
        'cmbCores
        '
        Me.cmbCores.FormattingEnabled = True
        Me.cmbCores.Location = New System.Drawing.Point(143, 309)
        Me.cmbCores.Name = "cmbCores"
        Me.cmbCores.Size = New System.Drawing.Size(55, 21)
        Me.cmbCores.TabIndex = 54
        Me.cmbCores.Text = "1"
        Me.ToolTip1.SetToolTip(Me.cmbCores, "Number of CPU cores dedicated to mining." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(Currently not supported).")
        '
        'lblCores
        '
        Me.lblCores.AutoSize = True
        Me.lblCores.Location = New System.Drawing.Point(21, 309)
        Me.lblCores.Name = "lblCores"
        Me.lblCores.Size = New System.Drawing.Size(75, 26)
        Me.lblCores.TabIndex = 49
        Me.lblCores.Text = "Number of " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Cores To Use:"
        '
        'txtBlocksMissed
        '
        Me.txtBlocksMissed.Location = New System.Drawing.Point(143, 228)
        Me.txtBlocksMissed.Name = "txtBlocksMissed"
        Me.txtBlocksMissed.ReadOnly = True
        Me.txtBlocksMissed.Size = New System.Drawing.Size(55, 20)
        Me.txtBlocksMissed.TabIndex = 47
        Me.txtBlocksMissed.Text = "0"
        Me.ToolTip1.SetToolTip(Me.txtBlocksMissed, "Total number of blocks attempted but failed this runtime.")
        '
        'lblBlocksMissed
        '
        Me.lblBlocksMissed.AutoSize = True
        Me.lblBlocksMissed.Location = New System.Drawing.Point(21, 231)
        Me.lblBlocksMissed.Name = "lblBlocksMissed"
        Me.lblBlocksMissed.Size = New System.Drawing.Size(78, 13)
        Me.lblBlocksMissed.TabIndex = 46
        Me.lblBlocksMissed.Text = "Blocks Missed:"
        '
        'txtBlocksMined
        '
        Me.txtBlocksMined.Location = New System.Drawing.Point(143, 193)
        Me.txtBlocksMined.Name = "txtBlocksMined"
        Me.txtBlocksMined.ReadOnly = True
        Me.txtBlocksMined.Size = New System.Drawing.Size(55, 20)
        Me.txtBlocksMined.TabIndex = 45
        Me.txtBlocksMined.Text = "0"
        Me.ToolTip1.SetToolTip(Me.txtBlocksMined, "Total number of blocks mined this runtime.")
        '
        'txtTotalTime
        '
        Me.txtTotalTime.Location = New System.Drawing.Point(143, 153)
        Me.txtTotalTime.Name = "txtTotalTime"
        Me.txtTotalTime.ReadOnly = True
        Me.txtTotalTime.Size = New System.Drawing.Size(55, 20)
        Me.txtTotalTime.TabIndex = 43
        Me.txtTotalTime.Text = "0"
        Me.ToolTip1.SetToolTip(Me.txtTotalTime, "Total time spent mining this runtime in seconds.")
        '
        'lblBlocksMined
        '
        Me.lblBlocksMined.AutoSize = True
        Me.lblBlocksMined.Location = New System.Drawing.Point(21, 196)
        Me.lblBlocksMined.Name = "lblBlocksMined"
        Me.lblBlocksMined.Size = New System.Drawing.Size(74, 13)
        Me.lblBlocksMined.TabIndex = 44
        Me.lblBlocksMined.Text = "Blocks Mined:"
        '
        'lblTotalTimeElapsed
        '
        Me.lblTotalTimeElapsed.AutoSize = True
        Me.lblTotalTimeElapsed.Location = New System.Drawing.Point(21, 157)
        Me.lblTotalTimeElapsed.Name = "lblTotalTimeElapsed"
        Me.lblTotalTimeElapsed.Size = New System.Drawing.Size(101, 13)
        Me.lblTotalTimeElapsed.TabIndex = 43
        Me.lblTotalTimeElapsed.Text = "Total Time Elapsed:"
        '
        'lblMining
        '
        Me.lblMining.AutoSize = True
        Me.lblMining.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMining.Location = New System.Drawing.Point(10, -3)
        Me.lblMining.Name = "lblMining"
        Me.lblMining.Size = New System.Drawing.Size(61, 20)
        Me.lblMining.TabIndex = 41
        Me.lblMining.Text = "Mining"
        Me.ToolTip1.SetToolTip(Me.lblMining, "Start, stop and see the status of block mining.")
        '
        'Settings
        '
        Me.Settings.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Settings.Controls.Add(Me.lstKnownNodes)
        Me.Settings.Controls.Add(Me.lblChainStat)
        Me.Settings.Controls.Add(Me.lblFileSet)
        Me.Settings.Controls.Add(Me.grpChainStat)
        Me.Settings.Controls.Add(Me.grpFileSet)
        Me.Settings.Controls.Add(Me.lblNetSet)
        Me.Settings.Controls.Add(Me.grpNetSet)
        Me.Settings.Controls.Add(Me.lstActiveNodes)
        Me.Settings.Controls.Add(Me.lblKnownNodes)
        Me.Settings.Controls.Add(Me.lblActiveNodes)
        Me.Settings.Controls.Add(Me.lblMemPool1)
        Me.Settings.Controls.Add(Me.lblNodes)
        Me.Settings.Controls.Add(Me.lblSync2)
        Me.Settings.Controls.Add(Me.lblStat2)
        Me.Settings.Controls.Add(Me.lblNodeCon)
        Me.Settings.Controls.Add(Me.lblNetStat)
        Me.Settings.Controls.Add(Me.grbNetStat)
        Me.Settings.Location = New System.Drawing.Point(4, 22)
        Me.Settings.Name = "Settings"
        Me.Settings.Size = New System.Drawing.Size(868, 452)
        Me.Settings.TabIndex = 3
        Me.Settings.Text = "Settings"
        Me.ToolTip1.SetToolTip(Me.Settings, "Change file directories and check connection status")
        '
        'lstKnownNodes
        '
        Me.lstKnownNodes.FormattingEnabled = True
        Me.lstKnownNodes.Location = New System.Drawing.Point(679, 32)
        Me.lstKnownNodes.Name = "lstKnownNodes"
        Me.lstKnownNodes.Size = New System.Drawing.Size(162, 407)
        Me.lstKnownNodes.TabIndex = 57
        '
        'lblChainStat
        '
        Me.lblChainStat.AutoSize = True
        Me.lblChainStat.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChainStat.Location = New System.Drawing.Point(269, 23)
        Me.lblChainStat.Name = "lblChainStat"
        Me.lblChainStat.Size = New System.Drawing.Size(154, 20)
        Me.lblChainStat.TabIndex = 41
        Me.lblChainStat.Text = "Blockchain Status"
        Me.ToolTip1.SetToolTip(Me.lblChainStat, "Information about the Blockchain and UTXO database.")
        '
        'lblFileSet
        '
        Me.lblFileSet.AutoSize = True
        Me.lblFileSet.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFileSet.Location = New System.Drawing.Point(281, 199)
        Me.lblFileSet.Name = "lblFileSet"
        Me.lblFileSet.Size = New System.Drawing.Size(110, 20)
        Me.lblFileSet.TabIndex = 55
        Me.lblFileSet.Text = "File Settings"
        Me.ToolTip1.SetToolTip(Me.lblFileSet, "Change file settings.")
        '
        'grpChainStat
        '
        Me.grpChainStat.Controls.Add(Me.txtUTXOSize)
        Me.grpChainStat.Controls.Add(Me.txtUTXONum)
        Me.grpChainStat.Controls.Add(Me.txtBlockSize)
        Me.grpChainStat.Controls.Add(Me.txtLocalHeight)
        Me.grpChainStat.Controls.Add(Me.lblUTXOSize)
        Me.grpChainStat.Controls.Add(Me.lblUTXONum)
        Me.grpChainStat.Controls.Add(Me.lblChainSize)
        Me.grpChainStat.Controls.Add(Me.lblLocal)
        Me.grpChainStat.Location = New System.Drawing.Point(259, 26)
        Me.grpChainStat.Name = "grpChainStat"
        Me.grpChainStat.Size = New System.Drawing.Size(245, 159)
        Me.grpChainStat.TabIndex = 42
        Me.grpChainStat.TabStop = False
        '
        'txtUTXOSize
        '
        Me.txtUTXOSize.Location = New System.Drawing.Point(126, 128)
        Me.txtUTXOSize.Name = "txtUTXOSize"
        Me.txtUTXOSize.ReadOnly = True
        Me.txtUTXOSize.Size = New System.Drawing.Size(97, 20)
        Me.txtUTXOSize.TabIndex = 64
        Me.ToolTip1.SetToolTip(Me.txtUTXOSize, "The size of the UTXO database in bytes.")
        '
        'txtUTXONum
        '
        Me.txtUTXONum.Location = New System.Drawing.Point(126, 97)
        Me.txtUTXONum.Name = "txtUTXONum"
        Me.txtUTXONum.ReadOnly = True
        Me.txtUTXONum.Size = New System.Drawing.Size(97, 20)
        Me.txtUTXONum.TabIndex = 63
        Me.ToolTip1.SetToolTip(Me.txtUTXONum, "The number of UTXO's stored in the UTXO database.")
        '
        'txtBlockSize
        '
        Me.txtBlockSize.Location = New System.Drawing.Point(126, 66)
        Me.txtBlockSize.Name = "txtBlockSize"
        Me.txtBlockSize.ReadOnly = True
        Me.txtBlockSize.Size = New System.Drawing.Size(97, 20)
        Me.txtBlockSize.TabIndex = 62
        Me.ToolTip1.SetToolTip(Me.txtBlockSize, "The size of this nodes blockchain in bytes.")
        '
        'txtLocalHeight
        '
        Me.txtLocalHeight.Location = New System.Drawing.Point(126, 35)
        Me.txtLocalHeight.Name = "txtLocalHeight"
        Me.txtLocalHeight.ReadOnly = True
        Me.txtLocalHeight.Size = New System.Drawing.Size(97, 20)
        Me.txtLocalHeight.TabIndex = 61
        Me.ToolTip1.SetToolTip(Me.txtLocalHeight, "The current height of this nodes blockchain.")
        '
        'lblUTXOSize
        '
        Me.lblUTXOSize.AutoSize = True
        Me.lblUTXOSize.Location = New System.Drawing.Point(14, 131)
        Me.lblUTXOSize.Name = "lblUTXOSize"
        Me.lblUTXOSize.Size = New System.Drawing.Size(82, 13)
        Me.lblUTXOSize.TabIndex = 60
        Me.lblUTXOSize.Text = "UTXO File Size:"
        '
        'lblUTXONum
        '
        Me.lblUTXONum.AutoSize = True
        Me.lblUTXONum.Location = New System.Drawing.Point(14, 100)
        Me.lblUTXONum.Name = "lblUTXONum"
        Me.lblUTXONum.Size = New System.Drawing.Size(99, 13)
        Me.lblUTXONum.TabIndex = 59
        Me.lblUTXONum.Text = "Number of UTXO's:"
        '
        'lblChainSize
        '
        Me.lblChainSize.AutoSize = True
        Me.lblChainSize.Location = New System.Drawing.Point(14, 69)
        Me.lblChainSize.Name = "lblChainSize"
        Me.lblChainSize.Size = New System.Drawing.Size(86, 13)
        Me.lblChainSize.TabIndex = 58
        Me.lblChainSize.Text = "Blockchain Size:"
        '
        'lblLocal
        '
        Me.lblLocal.AutoSize = True
        Me.lblLocal.Location = New System.Drawing.Point(14, 39)
        Me.lblLocal.Name = "lblLocal"
        Me.lblLocal.Size = New System.Drawing.Size(70, 13)
        Me.lblLocal.TabIndex = 57
        Me.lblLocal.Text = "Local Height:"
        '
        'grpFileSet
        '
        Me.grpFileSet.Controls.Add(Me.btnHashRebuild)
        Me.grpFileSet.Controls.Add(Me.Label3)
        Me.grpFileSet.Controls.Add(Me.btnBlockReset)
        Me.grpFileSet.Controls.Add(Me.btnUTXORebuild)
        Me.grpFileSet.Controls.Add(Me.lblRebuild)
        Me.grpFileSet.Controls.Add(Me.lblRest)
        Me.grpFileSet.Location = New System.Drawing.Point(259, 202)
        Me.grpFileSet.Name = "grpFileSet"
        Me.grpFileSet.Size = New System.Drawing.Size(246, 202)
        Me.grpFileSet.TabIndex = 56
        Me.grpFileSet.TabStop = False
        '
        'btnHashRebuild
        '
        Me.btnHashRebuild.Location = New System.Drawing.Point(148, 63)
        Me.btnHashRebuild.Name = "btnHashRebuild"
        Me.btnHashRebuild.Size = New System.Drawing.Size(75, 23)
        Me.btnHashRebuild.TabIndex = 55
        Me.btnHashRebuild.Text = "Rebuild"
        Me.ToolTip1.SetToolTip(Me.btnHashRebuild, "Resets the blochchain. This process will take some time.")
        Me.btnHashRebuild.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(14, 68)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(123, 13)
        Me.Label3.TabIndex = 54
        Me.Label3.Text = "Rebuild Hash Database:"
        Me.ToolTip1.SetToolTip(Me.Label3, resources.GetString("Label3.ToolTip"))
        '
        'btnBlockReset
        '
        Me.btnBlockReset.Location = New System.Drawing.Point(148, 98)
        Me.btnBlockReset.Name = "btnBlockReset"
        Me.btnBlockReset.Size = New System.Drawing.Size(75, 23)
        Me.btnBlockReset.TabIndex = 53
        Me.btnBlockReset.Text = "Reset"
        Me.ToolTip1.SetToolTip(Me.btnBlockReset, "Resets the blochchain. This process will take some time.")
        Me.btnBlockReset.UseVisualStyleBackColor = True
        '
        'btnUTXORebuild
        '
        Me.btnUTXORebuild.Location = New System.Drawing.Point(148, 29)
        Me.btnUTXORebuild.Name = "btnUTXORebuild"
        Me.btnUTXORebuild.Size = New System.Drawing.Size(75, 23)
        Me.btnUTXORebuild.TabIndex = 52
        Me.btnUTXORebuild.Text = "Rebuild"
        Me.ToolTip1.SetToolTip(Me.btnUTXORebuild, "Rebuild the UTXO database. This process will take some time.")
        Me.btnUTXORebuild.UseVisualStyleBackColor = True
        '
        'lblRebuild
        '
        Me.lblRebuild.AutoSize = True
        Me.lblRebuild.Location = New System.Drawing.Point(14, 34)
        Me.lblRebuild.Name = "lblRebuild"
        Me.lblRebuild.Size = New System.Drawing.Size(128, 13)
        Me.lblRebuild.TabIndex = 51
        Me.lblRebuild.Text = "Rebuild UTXO Database:"
        Me.ToolTip1.SetToolTip(Me.lblRebuild, resources.GetString("lblRebuild.ToolTip"))
        '
        'lblRest
        '
        Me.lblRest.AutoSize = True
        Me.lblRest.Location = New System.Drawing.Point(14, 103)
        Me.lblRest.Name = "lblRest"
        Me.lblRest.Size = New System.Drawing.Size(94, 13)
        Me.lblRest.TabIndex = 50
        Me.lblRest.Text = "Reset Blockchain:"
        Me.ToolTip1.SetToolTip(Me.lblRest, resources.GetString("lblRest.ToolTip"))
        '
        'lblNetSet
        '
        Me.lblNetSet.AutoSize = True
        Me.lblNetSet.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetSet.Location = New System.Drawing.Point(18, 199)
        Me.lblNetSet.Name = "lblNetSet"
        Me.lblNetSet.Size = New System.Drawing.Size(146, 20)
        Me.lblNetSet.TabIndex = 53
        Me.lblNetSet.Text = "Network Settings"
        Me.ToolTip1.SetToolTip(Me.lblNetSet, "Change the current network settings of the program.")
        '
        'grpNetSet
        '
        Me.grpNetSet.Controls.Add(Me.txtParentIP)
        Me.grpNetSet.Controls.Add(Me.lblParentIP)
        Me.grpNetSet.Controls.Add(Me.cmbUPNP)
        Me.grpNetSet.Controls.Add(Me.lblNetShip)
        Me.grpNetSet.Controls.Add(Me.cmbNetShip)
        Me.grpNetSet.Controls.Add(Me.cmbNetType)
        Me.grpNetSet.Controls.Add(Me.lblNetType)
        Me.grpNetSet.Controls.Add(Me.lblUPNP)
        Me.grpNetSet.Location = New System.Drawing.Point(8, 202)
        Me.grpNetSet.Name = "grpNetSet"
        Me.grpNetSet.Size = New System.Drawing.Size(245, 202)
        Me.grpNetSet.TabIndex = 54
        Me.grpNetSet.TabStop = False
        '
        'txtParentIP
        '
        Me.txtParentIP.Enabled = False
        Me.txtParentIP.Location = New System.Drawing.Point(114, 136)
        Me.txtParentIP.Name = "txtParentIP"
        Me.txtParentIP.Size = New System.Drawing.Size(121, 20)
        Me.txtParentIP.TabIndex = 65
        Me.ToolTip1.SetToolTip(Me.txtParentIP, "If this node is a child, please enter" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "the local IPv4 of the parent node here.")
        '
        'lblParentIP
        '
        Me.lblParentIP.AutoSize = True
        Me.lblParentIP.Location = New System.Drawing.Point(11, 139)
        Me.lblParentIP.Name = "lblParentIP"
        Me.lblParentIP.Size = New System.Drawing.Size(54, 13)
        Me.lblParentIP.TabIndex = 58
        Me.lblParentIP.Text = "Parent IP:"
        '
        'cmbUPNP
        '
        Me.cmbUPNP.FormattingEnabled = True
        Me.cmbUPNP.Items.AddRange(New Object() {"Enabled", "Disabled"})
        Me.cmbUPNP.Location = New System.Drawing.Point(115, 65)
        Me.cmbUPNP.Name = "cmbUPNP"
        Me.cmbUPNP.Size = New System.Drawing.Size(121, 21)
        Me.cmbUPNP.TabIndex = 54
        Me.cmbUPNP.Text = "Disabled"
        Me.ToolTip1.SetToolTip(Me.cmbUPNP, "Allows the client to map ports using UPNP (Universal Plug and Play)." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(Currently " & _
                "not supported).")
        '
        'lblNetShip
        '
        Me.lblNetShip.AutoSize = True
        Me.lblNetShip.Location = New System.Drawing.Point(11, 95)
        Me.lblNetShip.Name = "lblNetShip"
        Me.lblNetShip.Size = New System.Drawing.Size(74, 26)
        Me.lblNetShip.TabIndex = 55
        Me.lblNetShip.Text = "Network" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Releationship:"
        '
        'cmbNetShip
        '
        Me.cmbNetShip.FormattingEnabled = True
        Me.cmbNetShip.Items.AddRange(New Object() {"Parent", "Child"})
        Me.cmbNetShip.Location = New System.Drawing.Point(114, 100)
        Me.cmbNetShip.Name = "cmbNetShip"
        Me.cmbNetShip.Size = New System.Drawing.Size(121, 21)
        Me.cmbNetShip.TabIndex = 55
        Me.cmbNetShip.Text = "Parent"
        Me.ToolTip1.SetToolTip(Me.cmbNetShip, resources.GetString("cmbNetShip.ToolTip"))
        '
        'cmbNetType
        '
        Me.cmbNetType.FormattingEnabled = True
        Me.cmbNetType.Items.AddRange(New Object() {"LAN", "WAN"})
        Me.cmbNetType.Location = New System.Drawing.Point(115, 31)
        Me.cmbNetType.Name = "cmbNetType"
        Me.cmbNetType.Size = New System.Drawing.Size(121, 21)
        Me.cmbNetType.TabIndex = 53
        Me.cmbNetType.Text = "LAN"
        Me.ToolTip1.SetToolTip(Me.cmbNetType, "Is this node connecting over a local LAN or wider WAN." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(Currently WAN network ty" & _
                "pe is not supported.)")
        '
        'lblNetType
        '
        Me.lblNetType.AutoSize = True
        Me.lblNetType.Location = New System.Drawing.Point(11, 34)
        Me.lblNetType.Name = "lblNetType"
        Me.lblNetType.Size = New System.Drawing.Size(77, 13)
        Me.lblNetType.TabIndex = 49
        Me.lblNetType.Text = "Network Type:"
        '
        'lblUPNP
        '
        Me.lblUPNP.AutoSize = True
        Me.lblUPNP.Location = New System.Drawing.Point(11, 68)
        Me.lblUPNP.Name = "lblUPNP"
        Me.lblUPNP.Size = New System.Drawing.Size(76, 13)
        Me.lblUPNP.TabIndex = 52
        Me.lblUPNP.Text = "Enable UPNP:"
        '
        'lstActiveNodes
        '
        Me.lstActiveNodes.FormattingEnabled = True
        Me.lstActiveNodes.Location = New System.Drawing.Point(511, 32)
        Me.lstActiveNodes.Name = "lstActiveNodes"
        Me.lstActiveNodes.Size = New System.Drawing.Size(162, 407)
        Me.lstActiveNodes.TabIndex = 36
        '
        'lblKnownNodes
        '
        Me.lblKnownNodes.AutoSize = True
        Me.lblKnownNodes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblKnownNodes.Location = New System.Drawing.Point(676, 13)
        Me.lblKnownNodes.Name = "lblKnownNodes"
        Me.lblKnownNodes.Size = New System.Drawing.Size(85, 13)
        Me.lblKnownNodes.TabIndex = 35
        Me.lblKnownNodes.Text = "Known Nodes"
        Me.ToolTip1.SetToolTip(Me.lblKnownNodes, "Full list of IP's stroed.")
        '
        'lblActiveNodes
        '
        Me.lblActiveNodes.AutoSize = True
        Me.lblActiveNodes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblActiveNodes.Location = New System.Drawing.Point(511, 13)
        Me.lblActiveNodes.Name = "lblActiveNodes"
        Me.lblActiveNodes.Size = New System.Drawing.Size(83, 13)
        Me.lblActiveNodes.TabIndex = 34
        Me.lblActiveNodes.Text = "Active Nodes"
        Me.ToolTip1.SetToolTip(Me.lblActiveNodes, "IP's you are currently connected to.")
        '
        'lblMemPool1
        '
        Me.lblMemPool1.AutoSize = True
        Me.lblMemPool1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMemPool1.ForeColor = System.Drawing.Color.Red
        Me.lblMemPool1.Location = New System.Drawing.Point(119, 154)
        Me.lblMemPool1.Name = "lblMemPool1"
        Me.lblMemPool1.Size = New System.Drawing.Size(14, 13)
        Me.lblMemPool1.TabIndex = 32
        Me.lblMemPool1.Text = "0"
        Me.ToolTip1.SetToolTip(Me.lblMemPool1, "The total number of transactions stored in this clients memory pool.")
        '
        'lblNodes
        '
        Me.lblNodes.AutoSize = True
        Me.lblNodes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNodes.ForeColor = System.Drawing.Color.Red
        Me.lblNodes.Location = New System.Drawing.Point(119, 128)
        Me.lblNodes.Name = "lblNodes"
        Me.lblNodes.Size = New System.Drawing.Size(14, 13)
        Me.lblNodes.TabIndex = 31
        Me.lblNodes.Text = "0"
        Me.ToolTip1.SetToolTip(Me.lblNodes, "Number of active node connections.")
        '
        'lblSync2
        '
        Me.lblSync2.AutoSize = True
        Me.lblSync2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSync2.ForeColor = System.Drawing.Color.Red
        Me.lblSync2.Location = New System.Drawing.Point(119, 99)
        Me.lblSync2.Name = "lblSync2"
        Me.lblSync2.Size = New System.Drawing.Size(74, 13)
        Me.lblSync2.TabIndex = 30
        Me.lblSync2.Text = "Out of Sync"
        Me.ToolTip1.SetToolTip(Me.lblSync2, resources.GetString("lblSync2.ToolTip"))
        '
        'lblStat2
        '
        Me.lblStat2.AutoSize = True
        Me.lblStat2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStat2.ForeColor = System.Drawing.Color.Red
        Me.lblStat2.Location = New System.Drawing.Point(120, 66)
        Me.lblStat2.Name = "lblStat2"
        Me.lblStat2.Size = New System.Drawing.Size(44, 13)
        Me.lblStat2.TabIndex = 29
        Me.lblStat2.Text = "Offline"
        Me.ToolTip1.SetToolTip(Me.lblStat2, "You can login to your Halfcoin account and create new addresses while offline." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "T" & _
                "o send and receive funds, mine and check your balance you must be connected" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "to " & _
                "the to the network.")
        '
        'lblNodeCon
        '
        Me.lblNodeCon.AutoSize = True
        Me.lblNodeCon.Location = New System.Drawing.Point(19, 126)
        Me.lblNodeCon.Name = "lblNodeCon"
        Me.lblNodeCon.Size = New System.Drawing.Size(98, 13)
        Me.lblNodeCon.TabIndex = 25
        Me.lblNodeCon.Text = "Node Connections:"
        Me.ToolTip1.SetToolTip(Me.lblNodeCon, "Total number of connections to other nodes. A minimum of 3 is required.")
        '
        'lblNetStat
        '
        Me.lblNetStat.AutoSize = True
        Me.lblNetStat.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetStat.Location = New System.Drawing.Point(18, 23)
        Me.lblNetStat.Name = "lblNetStat"
        Me.lblNetStat.Size = New System.Drawing.Size(132, 20)
        Me.lblNetStat.TabIndex = 22
        Me.lblNetStat.Text = "Network Status"
        Me.ToolTip1.SetToolTip(Me.lblNetStat, "Information about the current network status of the program.")
        '
        'grbNetStat
        '
        Me.grbNetStat.Controls.Add(Me.lblNetStat2)
        Me.grbNetStat.Controls.Add(Me.lblSyncStat2)
        Me.grbNetStat.Controls.Add(Me.lblMemPool2)
        Me.grbNetStat.Location = New System.Drawing.Point(8, 26)
        Me.grbNetStat.Name = "grbNetStat"
        Me.grbNetStat.Size = New System.Drawing.Size(245, 159)
        Me.grbNetStat.TabIndex = 40
        Me.grbNetStat.TabStop = False
        '
        'lblNetStat2
        '
        Me.lblNetStat2.AutoSize = True
        Me.lblNetStat2.Location = New System.Drawing.Point(11, 39)
        Me.lblNetStat2.Name = "lblNetStat2"
        Me.lblNetStat2.Size = New System.Drawing.Size(83, 13)
        Me.lblNetStat2.TabIndex = 27
        Me.lblNetStat2.Text = "Network Status:"
        '
        'lblSyncStat2
        '
        Me.lblSyncStat2.AutoSize = True
        Me.lblSyncStat2.Location = New System.Drawing.Point(11, 69)
        Me.lblSyncStat2.Name = "lblSyncStat2"
        Me.lblSyncStat2.Size = New System.Drawing.Size(85, 13)
        Me.lblSyncStat2.TabIndex = 28
        Me.lblSyncStat2.Text = "Synchronization:"
        Me.ToolTip1.SetToolTip(Me.lblSyncStat2, "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
        '
        'lblMemPool2
        '
        Me.lblMemPool2.AutoSize = True
        Me.lblMemPool2.Location = New System.Drawing.Point(11, 128)
        Me.lblMemPool2.Name = "lblMemPool2"
        Me.lblMemPool2.Size = New System.Drawing.Size(71, 13)
        Me.lblMemPool2.TabIndex = 26
        Me.lblMemPool2.Text = "Memory Pool:"
        '
        'Console
        '
        Me.Console.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Console.Controls.Add(Me.btnEXE)
        Me.Console.Controls.Add(Me.txtCom)
        Me.Console.Controls.Add(Me.txtConsoleMain)
        Me.Console.Controls.Add(Me.lblConsole)
        Me.Console.Controls.Add(Me.grpConsole)
        Me.Console.Location = New System.Drawing.Point(4, 22)
        Me.Console.Name = "Console"
        Me.Console.Size = New System.Drawing.Size(868, 452)
        Me.Console.TabIndex = 4
        Me.Console.Text = "Console"
        Me.ToolTip1.SetToolTip(Me.Console, "View status of background operations and execute commands.")
        '
        'btnEXE
        '
        Me.btnEXE.Location = New System.Drawing.Point(721, 416)
        Me.btnEXE.Name = "btnEXE"
        Me.btnEXE.Size = New System.Drawing.Size(123, 25)
        Me.btnEXE.TabIndex = 26
        Me.btnEXE.Text = "Execute"
        Me.ToolTip1.SetToolTip(Me.btnEXE, "Execute the command entered.")
        Me.btnEXE.UseVisualStyleBackColor = True
        '
        'txtCom
        '
        Me.txtCom.Location = New System.Drawing.Point(24, 418)
        Me.txtCom.Name = "txtCom"
        Me.txtCom.Size = New System.Drawing.Size(691, 20)
        Me.txtCom.TabIndex = 25
        Me.ToolTip1.SetToolTip(Me.txtCom, "Enter a command here. Type ""Help"" for a list of avaliable commands." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "All commands" & _
                " are case sensitive.")
        '
        'txtConsoleMain
        '
        Me.txtConsoleMain.BackColor = System.Drawing.Color.White
        Me.txtConsoleMain.Location = New System.Drawing.Point(24, 46)
        Me.txtConsoleMain.Name = "txtConsoleMain"
        Me.txtConsoleMain.ReadOnly = True
        Me.txtConsoleMain.Size = New System.Drawing.Size(820, 364)
        Me.txtConsoleMain.TabIndex = 22
        Me.txtConsoleMain.Text = ""
        '
        'lblConsole
        '
        Me.lblConsole.AutoSize = True
        Me.lblConsole.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConsole.Location = New System.Drawing.Point(20, 11)
        Me.lblConsole.Name = "lblConsole"
        Me.lblConsole.Size = New System.Drawing.Size(74, 20)
        Me.lblConsole.TabIndex = 21
        Me.lblConsole.Text = "Console"
        Me.ToolTip1.SetToolTip(Me.lblConsole, "Execute commands here.")
        '
        'grpConsole
        '
        Me.grpConsole.Location = New System.Drawing.Point(8, 16)
        Me.grpConsole.Name = "grpConsole"
        Me.grpConsole.Size = New System.Drawing.Size(851, 433)
        Me.grpConsole.TabIndex = 27
        Me.grpConsole.TabStop = False
        '
        'Blockchain_Explorer
        '
        Me.Blockchain_Explorer.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Blockchain_Explorer.Controls.Add(Me.grpOutExp)
        Me.Blockchain_Explorer.Controls.Add(Me.lblInExp)
        Me.Blockchain_Explorer.Controls.Add(Me.grpInExp)
        Me.Blockchain_Explorer.Controls.Add(Me.lblTransExp)
        Me.Blockchain_Explorer.Controls.Add(Me.grpTransExp)
        Me.Blockchain_Explorer.Controls.Add(Me.lblBlockExp)
        Me.Blockchain_Explorer.Controls.Add(Me.grpBlockExp)
        Me.Blockchain_Explorer.Location = New System.Drawing.Point(4, 22)
        Me.Blockchain_Explorer.Name = "Blockchain_Explorer"
        Me.Blockchain_Explorer.Padding = New System.Windows.Forms.Padding(3)
        Me.Blockchain_Explorer.Size = New System.Drawing.Size(868, 452)
        Me.Blockchain_Explorer.TabIndex = 7
        Me.Blockchain_Explorer.Text = "Blockchain Explorer"
        '
        'grpOutExp
        '
        Me.grpOutExp.Controls.Add(Me.lblXOS)
        Me.grpOutExp.Controls.Add(Me.txtXOS)
        Me.grpOutExp.Controls.Add(Me.lblOutExp)
        Me.grpOutExp.Controls.Add(Me.txtXOV)
        Me.grpOutExp.Controls.Add(Me.lblXOV)
        Me.grpOutExp.Location = New System.Drawing.Point(584, 244)
        Me.grpOutExp.Name = "grpOutExp"
        Me.grpOutExp.Size = New System.Drawing.Size(275, 196)
        Me.grpOutExp.TabIndex = 61
        Me.grpOutExp.TabStop = False
        '
        'lblXOS
        '
        Me.lblXOS.AutoSize = True
        Me.lblXOS.Location = New System.Drawing.Point(30, 63)
        Me.lblXOS.Name = "lblXOS"
        Me.lblXOS.Size = New System.Drawing.Size(78, 13)
        Me.lblXOS.TabIndex = 65
        Me.lblXOS.Text = "Locking Script:"
        '
        'txtXOS
        '
        Me.txtXOS.Location = New System.Drawing.Point(11, 79)
        Me.txtXOS.Name = "txtXOS"
        Me.txtXOS.Size = New System.Drawing.Size(254, 108)
        Me.txtXOS.TabIndex = 54
        Me.txtXOS.Text = ""
        '
        'lblOutExp
        '
        Me.lblOutExp.AutoSize = True
        Me.lblOutExp.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOutExp.Location = New System.Drawing.Point(13, -3)
        Me.lblOutExp.Name = "lblOutExp"
        Me.lblOutExp.Size = New System.Drawing.Size(135, 20)
        Me.lblOutExp.TabIndex = 62
        Me.lblOutExp.Text = "Output Explorer"
        Me.ToolTip1.SetToolTip(Me.lblOutExp, "Provides information on specific transaction outputs.")
        '
        'txtXOV
        '
        Me.txtXOV.Location = New System.Drawing.Point(114, 32)
        Me.txtXOV.Name = "txtXOV"
        Me.txtXOV.ReadOnly = True
        Me.txtXOV.Size = New System.Drawing.Size(155, 20)
        Me.txtXOV.TabIndex = 51
        '
        'lblXOV
        '
        Me.lblXOV.AutoSize = True
        Me.lblXOV.Location = New System.Drawing.Point(71, 35)
        Me.lblXOV.Name = "lblXOV"
        Me.lblXOV.Size = New System.Drawing.Size(37, 13)
        Me.lblXOV.TabIndex = 44
        Me.lblXOV.Text = "Value:"
        '
        'lblInExp
        '
        Me.lblInExp.AutoSize = True
        Me.lblInExp.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInExp.Location = New System.Drawing.Point(597, 13)
        Me.lblInExp.Name = "lblInExp"
        Me.lblInExp.Size = New System.Drawing.Size(122, 20)
        Me.lblInExp.TabIndex = 45
        Me.lblInExp.Text = "Input Explorer"
        Me.ToolTip1.SetToolTip(Me.lblInExp, "Provides information on specific transaction inputs.")
        '
        'grpInExp
        '
        Me.grpInExp.Controls.Add(Me.lblXIS)
        Me.grpInExp.Controls.Add(Me.txtXIS)
        Me.grpInExp.Controls.Add(Me.txtXII)
        Me.grpInExp.Controls.Add(Me.txtXIT)
        Me.grpInExp.Controls.Add(Me.lblXII)
        Me.grpInExp.Controls.Add(Me.lblXIT)
        Me.grpInExp.Location = New System.Drawing.Point(584, 16)
        Me.grpInExp.Name = "grpInExp"
        Me.grpInExp.Size = New System.Drawing.Size(275, 222)
        Me.grpInExp.TabIndex = 46
        Me.grpInExp.TabStop = False
        '
        'lblXIS
        '
        Me.lblXIS.AutoSize = True
        Me.lblXIS.Location = New System.Drawing.Point(20, 89)
        Me.lblXIS.Name = "lblXIS"
        Me.lblXIS.Size = New System.Drawing.Size(88, 13)
        Me.lblXIS.TabIndex = 64
        Me.lblXIS.Text = "Unlocking Script:"
        '
        'txtXIS
        '
        Me.txtXIS.Location = New System.Drawing.Point(11, 105)
        Me.txtXIS.Name = "txtXIS"
        Me.txtXIS.Size = New System.Drawing.Size(254, 108)
        Me.txtXIS.TabIndex = 53
        Me.txtXIS.Text = ""
        '
        'txtXII
        '
        Me.txtXII.Location = New System.Drawing.Point(114, 61)
        Me.txtXII.Name = "txtXII"
        Me.txtXII.ReadOnly = True
        Me.txtXII.Size = New System.Drawing.Size(155, 20)
        Me.txtXII.TabIndex = 52
        '
        'txtXIT
        '
        Me.txtXIT.Location = New System.Drawing.Point(114, 34)
        Me.txtXIT.Name = "txtXIT"
        Me.txtXIT.ReadOnly = True
        Me.txtXIT.Size = New System.Drawing.Size(155, 20)
        Me.txtXIT.TabIndex = 51
        '
        'lblXII
        '
        Me.lblXII.AutoSize = True
        Me.lblXII.Location = New System.Drawing.Point(72, 64)
        Me.lblXII.Name = "lblXII"
        Me.lblXII.Size = New System.Drawing.Size(36, 13)
        Me.lblXII.TabIndex = 45
        Me.lblXII.Text = "Index:"
        '
        'lblXIT
        '
        Me.lblXIT.AutoSize = True
        Me.lblXIT.Location = New System.Drawing.Point(14, 39)
        Me.lblXIT.Name = "lblXIT"
        Me.lblXIT.Size = New System.Drawing.Size(94, 13)
        Me.lblXIT.TabIndex = 44
        Me.lblXIT.Text = "Transaction Hash:"
        '
        'lblTransExp
        '
        Me.lblTransExp.AutoSize = True
        Me.lblTransExp.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTransExp.Location = New System.Drawing.Point(309, 12)
        Me.lblTransExp.Name = "lblTransExp"
        Me.lblTransExp.Size = New System.Drawing.Size(174, 20)
        Me.lblTransExp.TabIndex = 43
        Me.lblTransExp.Text = "Transaction Explorer"
        Me.ToolTip1.SetToolTip(Me.lblTransExp, "Provides information on specific transactions on the Blockchain.")
        '
        'grpTransExp
        '
        Me.grpTransExp.Controls.Add(Me.lblXTH)
        Me.grpTransExp.Controls.Add(Me.txtXTH)
        Me.grpTransExp.Controls.Add(Me.btnOutGetData)
        Me.grpTransExp.Controls.Add(Me.btnINGetData)
        Me.grpTransExp.Controls.Add(Me.lblXTO)
        Me.grpTransExp.Controls.Add(Me.lstXTIn)
        Me.grpTransExp.Controls.Add(Me.lstXTO)
        Me.grpTransExp.Controls.Add(Me.lstXTI)
        Me.grpTransExp.Controls.Add(Me.txtXTV)
        Me.grpTransExp.Controls.Add(Me.txtXTI)
        Me.grpTransExp.Controls.Add(Me.lblXTV)
        Me.grpTransExp.Controls.Add(Me.lblXTI)
        Me.grpTransExp.Location = New System.Drawing.Point(296, 15)
        Me.grpTransExp.Name = "grpTransExp"
        Me.grpTransExp.Size = New System.Drawing.Size(275, 425)
        Me.grpTransExp.TabIndex = 44
        Me.grpTransExp.TabStop = False
        '
        'lblXTH
        '
        Me.lblXTH.AutoSize = True
        Me.lblXTH.Location = New System.Drawing.Point(16, 21)
        Me.lblXTH.Name = "lblXTH"
        Me.lblXTH.Size = New System.Drawing.Size(94, 13)
        Me.lblXTH.TabIndex = 72
        Me.lblXTH.Text = "Transaction Hash:"
        '
        'txtXTH
        '
        Me.txtXTH.Location = New System.Drawing.Point(14, 38)
        Me.txtXTH.Name = "txtXTH"
        Me.txtXTH.ReadOnly = True
        Me.txtXTH.Size = New System.Drawing.Size(247, 20)
        Me.txtXTH.TabIndex = 71
        '
        'btnOutGetData
        '
        Me.btnOutGetData.Location = New System.Drawing.Point(141, 90)
        Me.btnOutGetData.Name = "btnOutGetData"
        Me.btnOutGetData.Size = New System.Drawing.Size(128, 22)
        Me.btnOutGetData.TabIndex = 65
        Me.btnOutGetData.Text = "Get Data"
        Me.ToolTip1.SetToolTip(Me.btnOutGetData, "Extracts data about the output selected in the list box.")
        Me.btnOutGetData.UseVisualStyleBackColor = True
        '
        'btnINGetData
        '
        Me.btnINGetData.Location = New System.Drawing.Point(9, 90)
        Me.btnINGetData.Name = "btnINGetData"
        Me.btnINGetData.Size = New System.Drawing.Size(125, 22)
        Me.btnINGetData.TabIndex = 64
        Me.btnINGetData.Text = "Get Data"
        Me.ToolTip1.SetToolTip(Me.btnINGetData, "Extracts data about the input selected in the list box.")
        Me.btnINGetData.UseVisualStyleBackColor = True
        '
        'lblXTO
        '
        Me.lblXTO.AutoSize = True
        Me.lblXTO.Location = New System.Drawing.Point(177, 117)
        Me.lblXTO.Name = "lblXTO"
        Me.lblXTO.Size = New System.Drawing.Size(47, 13)
        Me.lblXTO.TabIndex = 63
        Me.lblXTO.Text = "Outputs:"
        '
        'lstXTIn
        '
        Me.lstXTIn.AutoSize = True
        Me.lstXTIn.Location = New System.Drawing.Point(51, 116)
        Me.lstXTIn.Name = "lstXTIn"
        Me.lstXTIn.Size = New System.Drawing.Size(39, 13)
        Me.lstXTIn.TabIndex = 62
        Me.lstXTIn.Text = "Inputs:"
        '
        'lstXTO
        '
        Me.lstXTO.FormattingEnabled = True
        Me.lstXTO.Location = New System.Drawing.Point(141, 134)
        Me.lstXTO.Name = "lstXTO"
        Me.lstXTO.Size = New System.Drawing.Size(128, 277)
        Me.lstXTO.TabIndex = 61
        '
        'lstXTI
        '
        Me.lstXTI.FormattingEnabled = True
        Me.lstXTI.Location = New System.Drawing.Point(9, 134)
        Me.lstXTI.Name = "lstXTI"
        Me.lstXTI.Size = New System.Drawing.Size(126, 277)
        Me.lstXTI.TabIndex = 60
        '
        'txtXTV
        '
        Me.txtXTV.Location = New System.Drawing.Point(199, 63)
        Me.txtXTV.Name = "txtXTV"
        Me.txtXTV.ReadOnly = True
        Me.txtXTV.Size = New System.Drawing.Size(70, 20)
        Me.txtXTV.TabIndex = 52
        '
        'txtXTI
        '
        Me.txtXTI.Location = New System.Drawing.Point(55, 63)
        Me.txtXTI.Name = "txtXTI"
        Me.txtXTI.ReadOnly = True
        Me.txtXTI.Size = New System.Drawing.Size(80, 20)
        Me.txtXTI.TabIndex = 51
        '
        'lblXTV
        '
        Me.lblXTV.AutoSize = True
        Me.lblXTV.Location = New System.Drawing.Point(148, 66)
        Me.lblXTV.Name = "lblXTV"
        Me.lblXTV.Size = New System.Drawing.Size(45, 13)
        Me.lblXTV.TabIndex = 45
        Me.lblXTV.Text = "Version:"
        '
        'lblXTI
        '
        Me.lblXTI.AutoSize = True
        Me.lblXTI.Location = New System.Drawing.Point(14, 67)
        Me.lblXTI.Name = "lblXTI"
        Me.lblXTI.Size = New System.Drawing.Size(36, 13)
        Me.lblXTI.TabIndex = 44
        Me.lblXTI.Text = "Index:"
        '
        'lblBlockExp
        '
        Me.lblBlockExp.AutoSize = True
        Me.lblBlockExp.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBlockExp.Location = New System.Drawing.Point(22, 13)
        Me.lblBlockExp.Name = "lblBlockExp"
        Me.lblBlockExp.Size = New System.Drawing.Size(124, 20)
        Me.lblBlockExp.TabIndex = 41
        Me.lblBlockExp.Text = "Block Explorer"
        Me.ToolTip1.SetToolTip(Me.lblBlockExp, "Provides information on specific blocks in the blockchain.")
        '
        'grpBlockExp
        '
        Me.grpBlockExp.Controls.Add(Me.lblXBH)
        Me.grpBlockExp.Controls.Add(Me.txtXBH)
        Me.grpBlockExp.Controls.Add(Me.btnDataMinus)
        Me.grpBlockExp.Controls.Add(Me.btnDataPlus)
        Me.grpBlockExp.Controls.Add(Me.btnTransGetData)
        Me.grpBlockExp.Controls.Add(Me.lblXBTs)
        Me.grpBlockExp.Controls.Add(Me.btnBlockGetData)
        Me.grpBlockExp.Controls.Add(Me.lstXBT)
        Me.grpBlockExp.Controls.Add(Me.txtXBN)
        Me.grpBlockExp.Controls.Add(Me.txtXBD)
        Me.grpBlockExp.Controls.Add(Me.txtXBT)
        Me.grpBlockExp.Controls.Add(Me.txtXBP)
        Me.grpBlockExp.Controls.Add(Me.txtXBM)
        Me.grpBlockExp.Controls.Add(Me.txtXBV)
        Me.grpBlockExp.Controls.Add(Me.txtXBB)
        Me.grpBlockExp.Controls.Add(Me.lblXBN)
        Me.grpBlockExp.Controls.Add(Me.lblXBD)
        Me.grpBlockExp.Controls.Add(Me.lblXBT)
        Me.grpBlockExp.Controls.Add(Me.lblXBM)
        Me.grpBlockExp.Controls.Add(Me.lblXBP)
        Me.grpBlockExp.Controls.Add(Me.lblXBV)
        Me.grpBlockExp.Controls.Add(Me.lblXBB)
        Me.grpBlockExp.Location = New System.Drawing.Point(9, 16)
        Me.grpBlockExp.Name = "grpBlockExp"
        Me.grpBlockExp.Size = New System.Drawing.Size(275, 425)
        Me.grpBlockExp.TabIndex = 42
        Me.grpBlockExp.TabStop = False
        '
        'lblXBH
        '
        Me.lblXBH.AutoSize = True
        Me.lblXBH.Location = New System.Drawing.Point(16, 61)
        Me.lblXBH.Name = "lblXBH"
        Me.lblXBH.Size = New System.Drawing.Size(65, 13)
        Me.lblXBH.TabIndex = 70
        Me.lblXBH.Text = "Block Hash:"
        '
        'txtXBH
        '
        Me.txtXBH.Location = New System.Drawing.Point(14, 78)
        Me.txtXBH.Name = "txtXBH"
        Me.txtXBH.ReadOnly = True
        Me.txtXBH.Size = New System.Drawing.Size(247, 20)
        Me.txtXBH.TabIndex = 69
        '
        'btnDataMinus
        '
        Me.btnDataMinus.Location = New System.Drawing.Point(212, 45)
        Me.btnDataMinus.Name = "btnDataMinus"
        Me.btnDataMinus.Size = New System.Drawing.Size(49, 23)
        Me.btnDataMinus.TabIndex = 68
        Me.btnDataMinus.Text = "▼"
        Me.ToolTip1.SetToolTip(Me.btnDataMinus, "Get data from the previous block.")
        Me.btnDataMinus.UseVisualStyleBackColor = True
        '
        'btnDataPlus
        '
        Me.btnDataPlus.Location = New System.Drawing.Point(161, 45)
        Me.btnDataPlus.Name = "btnDataPlus"
        Me.btnDataPlus.Size = New System.Drawing.Size(49, 23)
        Me.btnDataPlus.TabIndex = 67
        Me.btnDataPlus.Text = "▲"
        Me.ToolTip1.SetToolTip(Me.btnDataPlus, "Get data from the next block.")
        Me.btnDataPlus.UseVisualStyleBackColor = True
        '
        'btnTransGetData
        '
        Me.btnTransGetData.Location = New System.Drawing.Point(88, 210)
        Me.btnTransGetData.Name = "btnTransGetData"
        Me.btnTransGetData.Size = New System.Drawing.Size(173, 22)
        Me.btnTransGetData.TabIndex = 66
        Me.btnTransGetData.Text = "Get Data"
        Me.ToolTip1.SetToolTip(Me.btnTransGetData, "Extracts data about the transaction selected in the list box.")
        Me.btnTransGetData.UseVisualStyleBackColor = True
        '
        'lblXBTs
        '
        Me.lblXBTs.AutoSize = True
        Me.lblXBTs.Location = New System.Drawing.Point(11, 215)
        Me.lblXBTs.Name = "lblXBTs"
        Me.lblXBTs.Size = New System.Drawing.Size(71, 13)
        Me.lblXBTs.TabIndex = 66
        Me.lblXBTs.Text = "Transactions:"
        '
        'btnBlockGetData
        '
        Me.btnBlockGetData.Location = New System.Drawing.Point(161, 19)
        Me.btnBlockGetData.Name = "btnBlockGetData"
        Me.btnBlockGetData.Size = New System.Drawing.Size(100, 23)
        Me.btnBlockGetData.TabIndex = 61
        Me.btnBlockGetData.Text = "Get Data"
        Me.ToolTip1.SetToolTip(Me.btnBlockGetData, "Extracts data from the blockchain about the block at the height entered in the te" & _
                "xt box.")
        Me.btnBlockGetData.UseVisualStyleBackColor = True
        '
        'lstXBT
        '
        Me.lstXBT.FormattingEnabled = True
        Me.lstXBT.Location = New System.Drawing.Point(6, 237)
        Me.lstXBT.Name = "lstXBT"
        Me.lstXBT.Size = New System.Drawing.Size(255, 173)
        Me.lstXBT.TabIndex = 60
        '
        'txtXBN
        '
        Me.txtXBN.Location = New System.Drawing.Point(212, 185)
        Me.txtXBN.Name = "txtXBN"
        Me.txtXBN.ReadOnly = True
        Me.txtXBN.Size = New System.Drawing.Size(49, 20)
        Me.txtXBN.TabIndex = 57
        '
        'txtXBD
        '
        Me.txtXBD.Location = New System.Drawing.Point(88, 182)
        Me.txtXBD.Name = "txtXBD"
        Me.txtXBD.ReadOnly = True
        Me.txtXBD.Size = New System.Drawing.Size(49, 20)
        Me.txtXBD.TabIndex = 56
        '
        'txtXBT
        '
        Me.txtXBT.Location = New System.Drawing.Point(180, 104)
        Me.txtXBT.Name = "txtXBT"
        Me.txtXBT.ReadOnly = True
        Me.txtXBT.Size = New System.Drawing.Size(81, 20)
        Me.txtXBT.TabIndex = 55
        '
        'txtXBP
        '
        Me.txtXBP.Location = New System.Drawing.Point(88, 130)
        Me.txtXBP.Name = "txtXBP"
        Me.txtXBP.ReadOnly = True
        Me.txtXBP.Size = New System.Drawing.Size(173, 20)
        Me.txtXBP.TabIndex = 54
        '
        'txtXBM
        '
        Me.txtXBM.Location = New System.Drawing.Point(88, 156)
        Me.txtXBM.Name = "txtXBM"
        Me.txtXBM.ReadOnly = True
        Me.txtXBM.Size = New System.Drawing.Size(173, 20)
        Me.txtXBM.TabIndex = 53
        '
        'txtXBV
        '
        Me.txtXBV.Location = New System.Drawing.Point(88, 104)
        Me.txtXBV.Name = "txtXBV"
        Me.txtXBV.ReadOnly = True
        Me.txtXBV.Size = New System.Drawing.Size(49, 20)
        Me.txtXBV.TabIndex = 52
        '
        'txtXBB
        '
        Me.txtXBB.Location = New System.Drawing.Point(88, 36)
        Me.txtXBB.Name = "txtXBB"
        Me.txtXBB.Size = New System.Drawing.Size(49, 20)
        Me.txtXBB.TabIndex = 51
        Me.txtXBB.Text = "1"
        Me.ToolTip1.SetToolTip(Me.txtXBB, "Enter the block height of the block you wish to explore.")
        '
        'lblXBN
        '
        Me.lblXBN.AutoSize = True
        Me.lblXBN.Location = New System.Drawing.Point(164, 189)
        Me.lblXBN.Name = "lblXBN"
        Me.lblXBN.Size = New System.Drawing.Size(42, 13)
        Me.lblXBN.TabIndex = 50
        Me.lblXBN.Text = "Nonce:"
        '
        'lblXBD
        '
        Me.lblXBD.AutoSize = True
        Me.lblXBD.Location = New System.Drawing.Point(32, 185)
        Me.lblXBD.Name = "lblXBD"
        Me.lblXBD.Size = New System.Drawing.Size(50, 13)
        Me.lblXBD.TabIndex = 49
        Me.lblXBD.Text = "Difficulty:"
        '
        'lblXBT
        '
        Me.lblXBT.AutoSize = True
        Me.lblXBT.Location = New System.Drawing.Point(141, 108)
        Me.lblXBT.Name = "lblXBT"
        Me.lblXBT.Size = New System.Drawing.Size(33, 13)
        Me.lblXBT.TabIndex = 48
        Me.lblXBT.Text = "Time:"
        '
        'lblXBM
        '
        Me.lblXBM.AutoSize = True
        Me.lblXBM.Location = New System.Drawing.Point(14, 161)
        Me.lblXBM.Name = "lblXBM"
        Me.lblXBM.Size = New System.Drawing.Size(68, 13)
        Me.lblXBM.TabIndex = 47
        Me.lblXBM.Text = "Merkle Root:"
        '
        'lblXBP
        '
        Me.lblXBP.AutoSize = True
        Me.lblXBP.Location = New System.Drawing.Point(3, 133)
        Me.lblXBP.Name = "lblXBP"
        Me.lblXBP.Size = New System.Drawing.Size(79, 13)
        Me.lblXBP.TabIndex = 46
        Me.lblXBP.Text = "Previous Hash:"
        '
        'lblXBV
        '
        Me.lblXBV.AutoSize = True
        Me.lblXBV.Location = New System.Drawing.Point(37, 108)
        Me.lblXBV.Name = "lblXBV"
        Me.lblXBV.Size = New System.Drawing.Size(45, 13)
        Me.lblXBV.TabIndex = 45
        Me.lblXBV.Text = "Version:"
        '
        'lblXBB
        '
        Me.lblXBB.AutoSize = True
        Me.lblXBB.Location = New System.Drawing.Point(14, 39)
        Me.lblXBB.Name = "lblXBB"
        Me.lblXBB.Size = New System.Drawing.Size(68, 13)
        Me.lblXBB.TabIndex = 44
        Me.lblXBB.Text = "Block Height"
        '
        'ToolTip1
        '
        Me.ToolTip1.AutoPopDelay = 10000
        Me.ToolTip1.InitialDelay = 200
        Me.ToolTip1.ReshowDelay = 100
        '
        'TCPTimer
        '
        Me.TCPTimer.Interval = 10
        '
        'Clock
        '
        Me.Clock.Interval = 1000
        '
        'Halfcoin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(875, 476)
        Me.Controls.Add(Me.TabControl)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximumSize = New System.Drawing.Size(891, 515)
        Me.MinimumSize = New System.Drawing.Size(891, 515)
        Me.Name = "Halfcoin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Halfcoin"
        Me.TransparencyKey = System.Drawing.Color.Maroon
        Me.TabControl.ResumeLayout(False)
        Me.Wallet.ResumeLayout(False)
        Me.Wallet.PerformLayout()
        CType(Me.picboxWarn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picboxLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grbAccInfo.ResumeLayout(False)
        Me.Send_Money.ResumeLayout(False)
        Me.Send_Money.PerformLayout()
        Me.Address_Book.ResumeLayout(False)
        Me.Address_Book.PerformLayout()
        Me.Mine.ResumeLayout(False)
        Me.Mine.PerformLayout()
        Me.grpMining.ResumeLayout(False)
        Me.grpMining.PerformLayout()
        Me.Settings.ResumeLayout(False)
        Me.Settings.PerformLayout()
        Me.grpChainStat.ResumeLayout(False)
        Me.grpChainStat.PerformLayout()
        Me.grpFileSet.ResumeLayout(False)
        Me.grpFileSet.PerformLayout()
        Me.grpNetSet.ResumeLayout(False)
        Me.grpNetSet.PerformLayout()
        Me.grbNetStat.ResumeLayout(False)
        Me.grbNetStat.PerformLayout()
        Me.Console.ResumeLayout(False)
        Me.Console.PerformLayout()
        Me.Blockchain_Explorer.ResumeLayout(False)
        Me.Blockchain_Explorer.PerformLayout()
        Me.grpOutExp.ResumeLayout(False)
        Me.grpOutExp.PerformLayout()
        Me.grpInExp.ResumeLayout(False)
        Me.grpInExp.PerformLayout()
        Me.grpTransExp.ResumeLayout(False)
        Me.grpTransExp.PerformLayout()
        Me.grpBlockExp.ResumeLayout(False)
        Me.grpBlockExp.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl As System.Windows.Forms.TabControl
    Friend WithEvents Wallet As System.Windows.Forms.TabPage
    Friend WithEvents Send_Money As System.Windows.Forms.TabPage
    Friend WithEvents Mine As System.Windows.Forms.TabPage
    Friend WithEvents Settings As System.Windows.Forms.TabPage
    Friend WithEvents Console As System.Windows.Forms.TabPage
    Friend WithEvents btnHidePri As System.Windows.Forms.Button
    Friend WithEvents btnLogin As System.Windows.Forms.Button
    Friend WithEvents lblSuffix2 As System.Windows.Forms.Label
    Friend WithEvents lblSuffix As System.Windows.Forms.Label
    Friend WithEvents txtUnBal As System.Windows.Forms.TextBox
    Friend WithEvents txtBal As System.Windows.Forms.TextBox
    Friend WithEvents txtPubKey As System.Windows.Forms.TextBox
    Friend WithEvents txtPriKey As System.Windows.Forms.TextBox
    Friend WithEvents txtLogin As System.Windows.Forms.TextBox
    Friend WithEvents lblBalanceInfo As System.Windows.Forms.Label
    Friend WithEvents lblUnconfirmedBalance As System.Windows.Forms.Label
    Friend WithEvents lblBalance As System.Windows.Forms.Label
    Friend WithEvents lblPublicKey As System.Windows.Forms.Label
    Friend WithEvents lblPrivateKey As System.Windows.Forms.Label
    Friend WithEvents lblLoginWord As System.Windows.Forms.Label
    Friend WithEvents lblAccountInfo As System.Windows.Forms.Label
    Friend WithEvents lblStat1 As System.Windows.Forms.Label
    Friend WithEvents lblSyncStat1 As System.Windows.Forms.Label
    Friend WithEvents lblNetStat1 As System.Windows.Forms.Label
    Friend WithEvents lblBlocksRem As System.Windows.Forms.Label
    Friend WithEvents prbBlocks As System.Windows.Forms.ProgressBar
    Friend WithEvents lblSync1 As System.Windows.Forms.Label
    Friend WithEvents btnNewAdr As System.Windows.Forms.Button
    Friend WithEvents txtAdr As System.Windows.Forms.TextBox
    Friend WithEvents lblTransDetails As System.Windows.Forms.Label
    Friend WithEvents lblAddress As System.Windows.Forms.Label
    Friend WithEvents lblAddresses As System.Windows.Forms.Label
    Friend WithEvents lblTransFee As System.Windows.Forms.Label
    Friend WithEvents lblSendAmount As System.Windows.Forms.Label
    Friend WithEvents txtRepAdr As System.Windows.Forms.TextBox
    Friend WithEvents lblRAddress As System.Windows.Forms.Label
    Friend WithEvents txtRepPubKey As System.Windows.Forms.TextBox
    Friend WithEvents lblRPublicKey As System.Windows.Forms.Label
    Friend WithEvents btnSend As System.Windows.Forms.Button
    Friend WithEvents lblSuffix4 As System.Windows.Forms.Label
    Friend WithEvents lblSuffix3 As System.Windows.Forms.Label
    Friend WithEvents txtTransFee As System.Windows.Forms.TextBox
    Friend WithEvents txtSendAmount As System.Windows.Forms.TextBox
    Friend WithEvents lblOptional As System.Windows.Forms.Label
    Friend WithEvents txtTime As System.Windows.Forms.TextBox
    Friend WithEvents lblTimeElapsed As System.Windows.Forms.Label
    Friend WithEvents btnStopMine As System.Windows.Forms.Button
    Friend WithEvents txtConsoleMine As System.Windows.Forms.RichTextBox
    Friend WithEvents btnStartMine As System.Windows.Forms.Button
    Friend WithEvents lblMemPool1 As System.Windows.Forms.Label
    Friend WithEvents lblNodes As System.Windows.Forms.Label
    Friend WithEvents lblSync2 As System.Windows.Forms.Label
    Friend WithEvents lblStat2 As System.Windows.Forms.Label
    Friend WithEvents lblSyncStat2 As System.Windows.Forms.Label
    Friend WithEvents lblNetStat2 As System.Windows.Forms.Label
    Friend WithEvents lblMemPool2 As System.Windows.Forms.Label
    Friend WithEvents lblNodeCon As System.Windows.Forms.Label
    Friend WithEvents lblNetStat As System.Windows.Forms.Label
    Friend WithEvents lblKnownNodes As System.Windows.Forms.Label
    Friend WithEvents lblActiveNodes As System.Windows.Forms.Label
    Friend WithEvents btnEXE As System.Windows.Forms.Button
    Friend WithEvents txtCom As System.Windows.Forms.TextBox
    Friend WithEvents txtConsoleMain As System.Windows.Forms.RichTextBox
    Friend WithEvents lblConsole As System.Windows.Forms.Label
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents Address_Book As System.Windows.Forms.TabPage
    Friend WithEvents btnDel As System.Windows.Forms.Button
    Friend WithEvents btnAddNew As System.Windows.Forms.Button
    Friend WithEvents btnSelect As System.Windows.Forms.Button
    Friend WithEvents txtEnterPubKey As System.Windows.Forms.TextBox
    Friend WithEvents lblPublicKey1 As System.Windows.Forms.Label
    Friend WithEvents lstAdr As System.Windows.Forms.ListBox
    Friend WithEvents lblAdrBook As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents txtLabel As System.Windows.Forms.TextBox
    Friend WithEvents lblLabel As System.Windows.Forms.Label
    Friend WithEvents TCPTimer As System.Windows.Forms.Timer
    Friend WithEvents lstActiveNodes As System.Windows.Forms.ListBox
    Friend WithEvents btnConnect As System.Windows.Forms.Button
    Friend WithEvents grbAccInfo As System.Windows.Forms.GroupBox
    Friend WithEvents grbAccBal As System.Windows.Forms.GroupBox
    Friend WithEvents grbYourAdr As System.Windows.Forms.GroupBox
    Friend WithEvents grbTransDet As System.Windows.Forms.GroupBox
    Friend WithEvents grbNetStat As System.Windows.Forms.GroupBox
    Friend WithEvents picboxLogo As System.Windows.Forms.PictureBox
    Friend WithEvents grpMining As System.Windows.Forms.GroupBox
    Friend WithEvents lblMining As System.Windows.Forms.Label
    Friend WithEvents Blockchain_Explorer As System.Windows.Forms.TabPage
    Friend WithEvents lblBlockExp As System.Windows.Forms.Label
    Friend WithEvents grpBlockExp As System.Windows.Forms.GroupBox
    Friend WithEvents lblXBB As System.Windows.Forms.Label
    Friend WithEvents lblXBV As System.Windows.Forms.Label
    Friend WithEvents lblXBP As System.Windows.Forms.Label
    Friend WithEvents lblXBM As System.Windows.Forms.Label
    Friend WithEvents lblXBD As System.Windows.Forms.Label
    Friend WithEvents lblXBT As System.Windows.Forms.Label
    Friend WithEvents lblXBN As System.Windows.Forms.Label
    Friend WithEvents txtXBM As System.Windows.Forms.TextBox
    Friend WithEvents txtXBV As System.Windows.Forms.TextBox
    Friend WithEvents txtXBB As System.Windows.Forms.TextBox
    Friend WithEvents txtXBN As System.Windows.Forms.TextBox
    Friend WithEvents txtXBD As System.Windows.Forms.TextBox
    Friend WithEvents txtXBT As System.Windows.Forms.TextBox
    Friend WithEvents txtXBP As System.Windows.Forms.TextBox
    Friend WithEvents lblInExp As System.Windows.Forms.Label
    Friend WithEvents grpInExp As System.Windows.Forms.GroupBox
    Friend WithEvents txtXII As System.Windows.Forms.TextBox
    Friend WithEvents txtXIT As System.Windows.Forms.TextBox
    Friend WithEvents lblXII As System.Windows.Forms.Label
    Friend WithEvents lblXIT As System.Windows.Forms.Label
    Friend WithEvents lblTransExp As System.Windows.Forms.Label
    Friend WithEvents grpTransExp As System.Windows.Forms.GroupBox
    Friend WithEvents txtXTV As System.Windows.Forms.TextBox
    Friend WithEvents txtXTI As System.Windows.Forms.TextBox
    Friend WithEvents lblXTV As System.Windows.Forms.Label
    Friend WithEvents lblXTI As System.Windows.Forms.Label
    Friend WithEvents lstXBT As System.Windows.Forms.ListBox
    Friend WithEvents grpOutExp As System.Windows.Forms.GroupBox
    Friend WithEvents lblOutExp As System.Windows.Forms.Label
    Friend WithEvents txtXOV As System.Windows.Forms.TextBox
    Friend WithEvents lblXOV As System.Windows.Forms.Label
    Friend WithEvents lstXTI As System.Windows.Forms.ListBox
    Friend WithEvents lblXTO As System.Windows.Forms.Label
    Friend WithEvents lstXTIn As System.Windows.Forms.Label
    Friend WithEvents lstXTO As System.Windows.Forms.ListBox
    Friend WithEvents btnBlockGetData As System.Windows.Forms.Button
    Friend WithEvents txtXOS As System.Windows.Forms.RichTextBox
    Friend WithEvents txtXIS As System.Windows.Forms.RichTextBox
    Friend WithEvents lblXIS As System.Windows.Forms.Label
    Friend WithEvents lblXOS As System.Windows.Forms.Label
    Friend WithEvents btnINGetData As System.Windows.Forms.Button
    Friend WithEvents btnOutGetData As System.Windows.Forms.Button
    Friend WithEvents btnTransGetData As System.Windows.Forms.Button
    Friend WithEvents lblXBTs As System.Windows.Forms.Label
    Friend WithEvents btnHidLog As System.Windows.Forms.Button
    Friend WithEvents Clock As System.Windows.Forms.Timer
    Friend WithEvents lblBlocksMined As System.Windows.Forms.Label
    Friend WithEvents lblTotalTimeElapsed As System.Windows.Forms.Label
    Friend WithEvents txtBlocksMissed As System.Windows.Forms.TextBox
    Friend WithEvents lblBlocksMissed As System.Windows.Forms.Label
    Friend WithEvents txtBlocksMined As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalTime As System.Windows.Forms.TextBox
    Friend WithEvents lblRebuild As System.Windows.Forms.Label
    Friend WithEvents lblRest As System.Windows.Forms.Label
    Friend WithEvents lblNetType As System.Windows.Forms.Label
    Friend WithEvents lblUPNP As System.Windows.Forms.Label
    Friend WithEvents lblFileSet As System.Windows.Forms.Label
    Friend WithEvents grpFileSet As System.Windows.Forms.GroupBox
    Friend WithEvents lblNetSet As System.Windows.Forms.Label
    Friend WithEvents grpNetSet As System.Windows.Forms.GroupBox
    Friend WithEvents cmbNetType As System.Windows.Forms.ComboBox
    Friend WithEvents btnBlockReset As System.Windows.Forms.Button
    Friend WithEvents btnUTXORebuild As System.Windows.Forms.Button
    Friend WithEvents cmbUPNP As System.Windows.Forms.ComboBox
    Friend WithEvents lblChainStat As System.Windows.Forms.Label
    Friend WithEvents grpChainStat As System.Windows.Forms.GroupBox
    Friend WithEvents txtUTXOSize As System.Windows.Forms.TextBox
    Friend WithEvents txtUTXONum As System.Windows.Forms.TextBox
    Friend WithEvents txtBlockSize As System.Windows.Forms.TextBox
    Friend WithEvents txtLocalHeight As System.Windows.Forms.TextBox
    Friend WithEvents lblUTXOSize As System.Windows.Forms.Label
    Friend WithEvents lblUTXONum As System.Windows.Forms.Label
    Friend WithEvents lblChainSize As System.Windows.Forms.Label
    Friend WithEvents lblLocal As System.Windows.Forms.Label
    Friend WithEvents lblCores As System.Windows.Forms.Label
    Friend WithEvents cmbCores As System.Windows.Forms.ComboBox
    Friend WithEvents btnDataMinus As System.Windows.Forms.Button
    Friend WithEvents btnDataPlus As System.Windows.Forms.Button
    Friend WithEvents btnCopyRight As System.Windows.Forms.Button
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents txtHashRate As System.Windows.Forms.TextBox
    Friend WithEvents lblHashRate As System.Windows.Forms.Label
    Friend WithEvents lblHashUnit As System.Windows.Forms.Label
    Friend WithEvents ckbStop As System.Windows.Forms.CheckBox
    Friend WithEvents grpAdrBook As System.Windows.Forms.GroupBox
    Friend WithEvents grpConsole As System.Windows.Forms.GroupBox
    Friend WithEvents lblXBH As System.Windows.Forms.Label
    Friend WithEvents txtXBH As System.Windows.Forms.TextBox
    Friend WithEvents lblXTH As System.Windows.Forms.Label
    Friend WithEvents txtXTH As System.Windows.Forms.TextBox
    Friend WithEvents lstKnownNodes As System.Windows.Forms.ListBox
    Friend WithEvents lblParentIP As System.Windows.Forms.Label
    Friend WithEvents cmbNetShip As System.Windows.Forms.ComboBox
    Friend WithEvents lblNetShip As System.Windows.Forms.Label
    Friend WithEvents picboxWarn As System.Windows.Forms.PictureBox
    Friend WithEvents btnHashRebuild As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtParentIP As System.Windows.Forms.TextBox

End Class
