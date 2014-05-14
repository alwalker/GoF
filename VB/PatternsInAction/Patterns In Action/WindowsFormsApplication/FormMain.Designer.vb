Imports Microsoft.VisualBasic
Imports System
Namespace WindowsFormsApplication
	Partial Public Class FormMain
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.components = New System.ComponentModel.Container()
			Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(FormMain))
			Dim treeNode1 As New System.Windows.Forms.TreeNode("Customers")
			Dim dataGridViewCellStyle1 As New System.Windows.Forms.DataGridViewCellStyle()
			Dim dataGridViewCellStyle2 As New System.Windows.Forms.DataGridViewCellStyle()
			Dim dataGridViewCellStyle3 As New System.Windows.Forms.DataGridViewCellStyle()
			Dim dataGridViewCellStyle4 As New System.Windows.Forms.DataGridViewCellStyle()
			Dim dataGridViewCellStyle5 As New System.Windows.Forms.DataGridViewCellStyle()
			Dim dataGridViewCellStyle6 As New System.Windows.Forms.DataGridViewCellStyle()
			Me.imageListCustomer = New System.Windows.Forms.ImageList(Me.components)
			Me.toolStripAction = New System.Windows.Forms.ToolStrip()
			Me.toolStripButtonLogin = New System.Windows.Forms.ToolStripButton()
			Me.toolStripButtonLogout = New System.Windows.Forms.ToolStripButton()
			Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
			Me.toolStripButtonCut = New System.Windows.Forms.ToolStripButton()
			Me.toolStripButtonCopy = New System.Windows.Forms.ToolStripButton()
			Me.toolStripButtonPaste = New System.Windows.Forms.ToolStripButton()
			Me.toolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
			Me.toolStripButtonAdd = New System.Windows.Forms.ToolStripButton()
			Me.toolStripButtonEdit = New System.Windows.Forms.ToolStripButton()
			Me.toolStripButtonDelete = New System.Windows.Forms.ToolStripButton()
			Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
			Me.toolStripButtonHelp = New System.Windows.Forms.ToolStripButton()
			Me.menuStripAction = New System.Windows.Forms.MenuStrip()
			Me.fileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.loginToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.logoutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
			Me.exitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.editToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
			Me.cutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.copyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.pasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.customerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.addToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.editToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.deleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.helpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.indexToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
			Me.aboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.contextMenuStripCustomer = New System.Windows.Forms.ContextMenuStrip(Me.components)
			Me.addNewCustomerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.editCustomerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.deleteCustomerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.treeViewCustomer = New System.Windows.Forms.TreeView()
			Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
			Me.splitContainer1 = New System.Windows.Forms.SplitContainer()
			Me.splitContainer3 = New System.Windows.Forms.SplitContainer()
			Me.panel2 = New System.Windows.Forms.Panel()
			Me.pictureBox1 = New System.Windows.Forms.PictureBox()
			Me.splitContainer2 = New System.Windows.Forms.SplitContainer()
			Me.dataGridViewOrders = New System.Windows.Forms.DataGridView()
			Me.panel1 = New System.Windows.Forms.Panel()
			Me.dataGridViewOrderDetails = New System.Windows.Forms.DataGridView()
			Me.buttonOrderDetails = New System.Windows.Forms.Button()
			Me.contextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
			Me.toolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripMenuItem6 = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripMenuItem7 = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripMenuItem9 = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripMenuItem10 = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripMenuItem12 = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripMenuItem13 = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
			Me.toolStripMenuItem15 = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripMenuItem16 = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripMenuItem17 = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripMenuItem18 = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
			Me.toolStripMenuItem19 = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripAction.SuspendLayout()
			Me.menuStripAction.SuspendLayout()
			Me.contextMenuStripCustomer.SuspendLayout()
			Me.splitContainer1.Panel1.SuspendLayout()
			Me.splitContainer1.Panel2.SuspendLayout()
			Me.splitContainer1.SuspendLayout()
			Me.splitContainer3.Panel1.SuspendLayout()
			Me.splitContainer3.Panel2.SuspendLayout()
			Me.splitContainer3.SuspendLayout()
			Me.panel2.SuspendLayout()
			CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.splitContainer2.Panel1.SuspendLayout()
			Me.splitContainer2.Panel2.SuspendLayout()
			Me.splitContainer2.SuspendLayout()
			CType(Me.dataGridViewOrders, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.panel1.SuspendLayout()
			CType(Me.dataGridViewOrderDetails, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.contextMenuStrip1.SuspendLayout()
			Me.SuspendLayout()
			' 
			' imageListCustomer
			' 
			Me.imageListCustomer.ImageStream = (CType(resources.GetObject("imageListCustomer.ImageStream"), System.Windows.Forms.ImageListStreamer))
			Me.imageListCustomer.TransparentColor = System.Drawing.Color.Transparent
			Me.imageListCustomer.Images.SetKeyName(0, "")
			Me.imageListCustomer.Images.SetKeyName(1, "")
			' 
			' toolStripAction
			' 
			Me.toolStripAction.Items.AddRange(New System.Windows.Forms.ToolStripItem() { Me.toolStripButtonLogin, Me.toolStripButtonLogout, Me.toolStripSeparator1, Me.toolStripButtonCut, Me.toolStripButtonCopy, Me.toolStripButtonPaste, Me.toolStripSeparator3, Me.toolStripButtonAdd, Me.toolStripButtonEdit, Me.toolStripButtonDelete, Me.toolStripSeparator2, Me.toolStripButtonHelp})
			Me.toolStripAction.Location = New System.Drawing.Point(0, 24)
			Me.toolStripAction.Name = "toolStripAction"
			Me.toolStripAction.Size = New System.Drawing.Size(870, 36)
			Me.toolStripAction.TabIndex = 3
			Me.toolStripAction.Text = "toolStrip1"
			' 
			' toolStripButtonLogin
			' 
			Me.toolStripButtonLogin.Image = (CType(resources.GetObject("toolStripButtonLogin.Image"), System.Drawing.Image))
			Me.toolStripButtonLogin.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.toolStripButtonLogin.Name = "toolStripButtonLogin"
			Me.toolStripButtonLogin.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
			Me.toolStripButtonLogin.Size = New System.Drawing.Size(40, 33)
			Me.toolStripButtonLogin.Text = "Login"
			Me.toolStripButtonLogin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
			Me.toolStripButtonLogin.ToolTipText = "Login to Web Service"
'			Me.toolStripButtonLogin.Click += New System.EventHandler(Me.toolStripButtonLogin_Click);
			' 
			' toolStripButtonLogout
			' 
			Me.toolStripButtonLogout.Image = (CType(resources.GetObject("toolStripButtonLogout.Image"), System.Drawing.Image))
			Me.toolStripButtonLogout.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.toolStripButtonLogout.Name = "toolStripButtonLogout"
			Me.toolStripButtonLogout.Padding = New System.Windows.Forms.Padding(1, 0, 1, 0)
			Me.toolStripButtonLogout.Size = New System.Drawing.Size(46, 33)
			Me.toolStripButtonLogout.Text = "Logout"
			Me.toolStripButtonLogout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
			Me.toolStripButtonLogout.ToolTipText = "Logout from Web Service"
'			Me.toolStripButtonLogout.Click += New System.EventHandler(Me.toolStripButtonLogout_Click);
			' 
			' toolStripSeparator1
			' 
			Me.toolStripSeparator1.Name = "toolStripSeparator1"
			Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 36)
			' 
			' toolStripButtonCut
			' 
			Me.toolStripButtonCut.Image = (CType(resources.GetObject("toolStripButtonCut.Image"), System.Drawing.Image))
			Me.toolStripButtonCut.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.toolStripButtonCut.Name = "toolStripButtonCut"
			Me.toolStripButtonCut.Padding = New System.Windows.Forms.Padding(6, 0, 6, 0)
			Me.toolStripButtonCut.Size = New System.Drawing.Size(40, 33)
			Me.toolStripButtonCut.Text = "Cut"
			Me.toolStripButtonCut.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
			Me.toolStripButtonCut.ToolTipText = "Cut"
			' 
			' toolStripButtonCopy
			' 
			Me.toolStripButtonCopy.Image = (CType(resources.GetObject("toolStripButtonCopy.Image"), System.Drawing.Image))
			Me.toolStripButtonCopy.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.toolStripButtonCopy.Name = "toolStripButtonCopy"
			Me.toolStripButtonCopy.Padding = New System.Windows.Forms.Padding(5, 0, 5, 0)
			Me.toolStripButtonCopy.Size = New System.Drawing.Size(46, 33)
			Me.toolStripButtonCopy.Text = "Copy"
			Me.toolStripButtonCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
			Me.toolStripButtonCopy.ToolTipText = "Copy"
			' 
			' toolStripButtonPaste
			' 
			Me.toolStripButtonPaste.Image = (CType(resources.GetObject("toolStripButtonPaste.Image"), System.Drawing.Image))
			Me.toolStripButtonPaste.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.toolStripButtonPaste.Name = "toolStripButtonPaste"
			Me.toolStripButtonPaste.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
			Me.toolStripButtonPaste.Size = New System.Drawing.Size(42, 33)
			Me.toolStripButtonPaste.Text = "Paste"
			Me.toolStripButtonPaste.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
			Me.toolStripButtonPaste.ToolTipText = "Paste"
			' 
			' toolStripSeparator3
			' 
			Me.toolStripSeparator3.Name = "toolStripSeparator3"
			Me.toolStripSeparator3.Size = New System.Drawing.Size(6, 36)
			' 
			' toolStripButtonAdd
			' 
			Me.toolStripButtonAdd.Image = (CType(resources.GetObject("toolStripButtonAdd.Image"), System.Drawing.Image))
			Me.toolStripButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.toolStripButtonAdd.Name = "toolStripButtonAdd"
			Me.toolStripButtonAdd.Padding = New System.Windows.Forms.Padding(7, 0, 7, 0)
			Me.toolStripButtonAdd.Size = New System.Drawing.Size(44, 33)
			Me.toolStripButtonAdd.Text = "Add"
			Me.toolStripButtonAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
			Me.toolStripButtonAdd.ToolTipText = "Add New Customer"
'			Me.toolStripButtonAdd.Click += New System.EventHandler(Me.toolStripButtonAdd_Click);
			' 
			' toolStripButtonEdit
			' 
			Me.toolStripButtonEdit.Image = (CType(resources.GetObject("toolStripButtonEdit.Image"), System.Drawing.Image))
			Me.toolStripButtonEdit.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.toolStripButtonEdit.Name = "toolStripButtonEdit"
			Me.toolStripButtonEdit.Padding = New System.Windows.Forms.Padding(5, 0, 5, 0)
			Me.toolStripButtonEdit.Size = New System.Drawing.Size(39, 33)
			Me.toolStripButtonEdit.Text = "Edit"
			Me.toolStripButtonEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
			Me.toolStripButtonEdit.ToolTipText = "Edit Customer"
'			Me.toolStripButtonEdit.Click += New System.EventHandler(Me.toolStripButtonEdit_Click);
			' 
			' toolStripButtonDelete
			' 
			Me.toolStripButtonDelete.Image = (CType(resources.GetObject("toolStripButtonDelete.Image"), System.Drawing.Image))
			Me.toolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.toolStripButtonDelete.Name = "toolStripButtonDelete"
			Me.toolStripButtonDelete.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
			Me.toolStripButtonDelete.Size = New System.Drawing.Size(46, 33)
			Me.toolStripButtonDelete.Text = "Delete"
			Me.toolStripButtonDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
			Me.toolStripButtonDelete.ToolTipText = "Delete Customer"
'			Me.toolStripButtonDelete.Click += New System.EventHandler(Me.toolStripButtonDelete_Click);
			' 
			' toolStripSeparator2
			' 
			Me.toolStripSeparator2.Name = "toolStripSeparator2"
			Me.toolStripSeparator2.Size = New System.Drawing.Size(6, 36)
			' 
			' toolStripButtonHelp
			' 
			Me.toolStripButtonHelp.Image = (CType(resources.GetObject("toolStripButtonHelp.Image"), System.Drawing.Image))
			Me.toolStripButtonHelp.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.toolStripButtonHelp.Name = "toolStripButtonHelp"
			Me.toolStripButtonHelp.Padding = New System.Windows.Forms.Padding(5, 0, 5, 0)
			Me.toolStripButtonHelp.Size = New System.Drawing.Size(42, 33)
			Me.toolStripButtonHelp.Text = "Help"
			Me.toolStripButtonHelp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
			Me.toolStripButtonHelp.ToolTipText = "Help"
'			Me.toolStripButtonHelp.Click += New System.EventHandler(Me.toolStripButtonHelp_Click);
			' 
			' menuStripAction
			' 
			Me.menuStripAction.Items.AddRange(New System.Windows.Forms.ToolStripItem() { Me.fileToolStripMenuItem, Me.editToolStripMenuItem1, Me.customerToolStripMenuItem, Me.helpToolStripMenuItem})
			Me.menuStripAction.Location = New System.Drawing.Point(0, 0)
			Me.menuStripAction.Name = "menuStripAction"
			Me.menuStripAction.Size = New System.Drawing.Size(870, 24)
			Me.menuStripAction.TabIndex = 2
			Me.menuStripAction.Text = "menuStrip1"
			' 
			' fileToolStripMenuItem
			' 
			Me.fileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() { Me.loginToolStripMenuItem, Me.logoutToolStripMenuItem, Me.toolStripMenuItem1, Me.exitToolStripMenuItem})
			Me.fileToolStripMenuItem.Name = "fileToolStripMenuItem"
			Me.fileToolStripMenuItem.Size = New System.Drawing.Size(35, 20)
			Me.fileToolStripMenuItem.Text = "&File"
			' 
			' loginToolStripMenuItem
			' 
			Me.loginToolStripMenuItem.Image = (CType(resources.GetObject("loginToolStripMenuItem.Image"), System.Drawing.Image))
			Me.loginToolStripMenuItem.Name = "loginToolStripMenuItem"
			Me.loginToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
			Me.loginToolStripMenuItem.Text = "&Login..."
'			Me.loginToolStripMenuItem.Click += New System.EventHandler(Me.loginToolStripMenuItem_Click);
			' 
			' logoutToolStripMenuItem
			' 
			Me.logoutToolStripMenuItem.Image = (CType(resources.GetObject("logoutToolStripMenuItem.Image"), System.Drawing.Image))
			Me.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem"
			Me.logoutToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
			Me.logoutToolStripMenuItem.Text = "Lo&gout"
'			Me.logoutToolStripMenuItem.Click += New System.EventHandler(Me.logoutToolStripMenuItem_Click);
			' 
			' toolStripMenuItem1
			' 
			Me.toolStripMenuItem1.Name = "toolStripMenuItem1"
			Me.toolStripMenuItem1.Size = New System.Drawing.Size(119, 6)
			' 
			' exitToolStripMenuItem
			' 
			Me.exitToolStripMenuItem.Name = "exitToolStripMenuItem"
			Me.exitToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
			Me.exitToolStripMenuItem.Text = "E&xit"
'			Me.exitToolStripMenuItem.Click += New System.EventHandler(Me.exitToolStripMenuItem_Click);
			' 
			' editToolStripMenuItem1
			' 
			Me.editToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() { Me.cutToolStripMenuItem, Me.copyToolStripMenuItem, Me.pasteToolStripMenuItem})
			Me.editToolStripMenuItem1.Name = "editToolStripMenuItem1"
			Me.editToolStripMenuItem1.Size = New System.Drawing.Size(37, 20)
			Me.editToolStripMenuItem1.Text = "&Edit"
			' 
			' cutToolStripMenuItem
			' 
			Me.cutToolStripMenuItem.Image = (CType(resources.GetObject("cutToolStripMenuItem.Image"), System.Drawing.Image))
			Me.cutToolStripMenuItem.Name = "cutToolStripMenuItem"
			Me.cutToolStripMenuItem.ShortcutKeys = (CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys))
			Me.cutToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
			Me.cutToolStripMenuItem.Text = "Cu&t"
			' 
			' copyToolStripMenuItem
			' 
			Me.copyToolStripMenuItem.Image = (CType(resources.GetObject("copyToolStripMenuItem.Image"), System.Drawing.Image))
			Me.copyToolStripMenuItem.Name = "copyToolStripMenuItem"
			Me.copyToolStripMenuItem.ShortcutKeys = (CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys))
			Me.copyToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
			Me.copyToolStripMenuItem.Text = "&Copy"
			' 
			' pasteToolStripMenuItem
			' 
			Me.pasteToolStripMenuItem.Image = (CType(resources.GetObject("pasteToolStripMenuItem.Image"), System.Drawing.Image))
			Me.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem"
			Me.pasteToolStripMenuItem.ShortcutKeys = (CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys))
			Me.pasteToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
			Me.pasteToolStripMenuItem.Text = "Paste"
			' 
			' customerToolStripMenuItem
			' 
			Me.customerToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() { Me.addToolStripMenuItem, Me.editToolStripMenuItem, Me.deleteToolStripMenuItem})
			Me.customerToolStripMenuItem.Name = "customerToolStripMenuItem"
			Me.customerToolStripMenuItem.Size = New System.Drawing.Size(65, 20)
			Me.customerToolStripMenuItem.Text = "&Customer"
			' 
			' addToolStripMenuItem
			' 
			Me.addToolStripMenuItem.Enabled = False
			Me.addToolStripMenuItem.Image = (CType(resources.GetObject("addToolStripMenuItem.Image"), System.Drawing.Image))
			Me.addToolStripMenuItem.Name = "addToolStripMenuItem"
			Me.addToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
			Me.addToolStripMenuItem.Text = "&Add New"
'			Me.addToolStripMenuItem.Click += New System.EventHandler(Me.addToolStripMenuItem_Click);
			' 
			' editToolStripMenuItem
			' 
			Me.editToolStripMenuItem.Enabled = False
			Me.editToolStripMenuItem.Image = (CType(resources.GetObject("editToolStripMenuItem.Image"), System.Drawing.Image))
			Me.editToolStripMenuItem.Name = "editToolStripMenuItem"
			Me.editToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
			Me.editToolStripMenuItem.Text = "&Edit"
'			Me.editToolStripMenuItem.Click += New System.EventHandler(Me.editToolStripMenuItem_Click);
			' 
			' deleteToolStripMenuItem
			' 
			Me.deleteToolStripMenuItem.Enabled = False
			Me.deleteToolStripMenuItem.Image = (CType(resources.GetObject("deleteToolStripMenuItem.Image"), System.Drawing.Image))
			Me.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem"
			Me.deleteToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
			Me.deleteToolStripMenuItem.Text = "&Delete"
'			Me.deleteToolStripMenuItem.Click += New System.EventHandler(Me.deleteToolStripMenuItem_Click);
			' 
			' helpToolStripMenuItem
			' 
			Me.helpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() { Me.indexToolStripMenuItem, Me.toolStripMenuItem2, Me.aboutToolStripMenuItem})
			Me.helpToolStripMenuItem.Name = "helpToolStripMenuItem"
			Me.helpToolStripMenuItem.Size = New System.Drawing.Size(40, 20)
			Me.helpToolStripMenuItem.Text = "&Help"
			' 
			' indexToolStripMenuItem
			' 
			Me.indexToolStripMenuItem.Image = (CType(resources.GetObject("indexToolStripMenuItem.Image"), System.Drawing.Image))
			Me.indexToolStripMenuItem.Name = "indexToolStripMenuItem"
			Me.indexToolStripMenuItem.Size = New System.Drawing.Size(114, 22)
			Me.indexToolStripMenuItem.Text = "Index"
'			Me.indexToolStripMenuItem.Click += New System.EventHandler(Me.indexToolStripMenuItem_Click);
			' 
			' toolStripMenuItem2
			' 
			Me.toolStripMenuItem2.Name = "toolStripMenuItem2"
			Me.toolStripMenuItem2.Size = New System.Drawing.Size(111, 6)
			' 
			' aboutToolStripMenuItem
			' 
			Me.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem"
			Me.aboutToolStripMenuItem.Size = New System.Drawing.Size(114, 22)
			Me.aboutToolStripMenuItem.Text = "&About"
'			Me.aboutToolStripMenuItem.Click += New System.EventHandler(Me.aboutToolStripMenuItem_Click);
			' 
			' contextMenuStripCustomer
			' 
			Me.contextMenuStripCustomer.Items.AddRange(New System.Windows.Forms.ToolStripItem() { Me.addNewCustomerToolStripMenuItem, Me.editCustomerToolStripMenuItem, Me.deleteCustomerToolStripMenuItem})
			Me.contextMenuStripCustomer.Name = "contextMenuStripCustomer"
			Me.contextMenuStripCustomer.Size = New System.Drawing.Size(190, 70)
			' 
			' addNewCustomerToolStripMenuItem
			' 
			Me.addNewCustomerToolStripMenuItem.Image = (CType(resources.GetObject("addNewCustomerToolStripMenuItem.Image"), System.Drawing.Image))
			Me.addNewCustomerToolStripMenuItem.Name = "addNewCustomerToolStripMenuItem"
			Me.addNewCustomerToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
			Me.addNewCustomerToolStripMenuItem.Text = "Add New Customer..."
'			Me.addNewCustomerToolStripMenuItem.Click += New System.EventHandler(Me.toolStripButtonAdd_Click);
			' 
			' editCustomerToolStripMenuItem
			' 
			Me.editCustomerToolStripMenuItem.Image = (CType(resources.GetObject("editCustomerToolStripMenuItem.Image"), System.Drawing.Image))
			Me.editCustomerToolStripMenuItem.Name = "editCustomerToolStripMenuItem"
			Me.editCustomerToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
			Me.editCustomerToolStripMenuItem.Text = "Edit Customer..."
'			Me.editCustomerToolStripMenuItem.Click += New System.EventHandler(Me.toolStripButtonEdit_Click);
			' 
			' deleteCustomerToolStripMenuItem
			' 
			Me.deleteCustomerToolStripMenuItem.Image = (CType(resources.GetObject("deleteCustomerToolStripMenuItem.Image"), System.Drawing.Image))
			Me.deleteCustomerToolStripMenuItem.Name = "deleteCustomerToolStripMenuItem"
			Me.deleteCustomerToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
			Me.deleteCustomerToolStripMenuItem.Text = "Delete Customer"
'			Me.deleteCustomerToolStripMenuItem.Click += New System.EventHandler(Me.toolStripButtonDelete_Click);
			' 
			' treeViewCustomer
			' 
			Me.treeViewCustomer.Dock = System.Windows.Forms.DockStyle.Fill
			Me.treeViewCustomer.ImageIndex = 0
			Me.treeViewCustomer.ImageList = Me.imageList1
			Me.treeViewCustomer.Location = New System.Drawing.Point(0, 0)
			Me.treeViewCustomer.Margin = New System.Windows.Forms.Padding(3, 3, 0, 3)
			Me.treeViewCustomer.Name = "treeViewCustomer"
			treeNode1.Name = "Node0"
			treeNode1.Tag = "root"
			treeNode1.Text = "Customers"
			Me.treeViewCustomer.Nodes.AddRange(New System.Windows.Forms.TreeNode() { treeNode1})
			Me.treeViewCustomer.SelectedImageIndex = 0
			Me.treeViewCustomer.ShowPlusMinus = False
			Me.treeViewCustomer.ShowRootLines = False
			Me.treeViewCustomer.Size = New System.Drawing.Size(258, 391)
			Me.treeViewCustomer.TabIndex = 0
'			Me.treeViewCustomer.MouseUp += New System.Windows.Forms.MouseEventHandler(Me.treeViewCustomer_MouseUp);
'			Me.treeViewCustomer.AfterSelect += New System.Windows.Forms.TreeViewEventHandler(Me.treeViewCustomer_AfterSelect);
			' 
			' imageList1
			' 
			Me.imageList1.ImageStream = (CType(resources.GetObject("imageList1.ImageStream"), System.Windows.Forms.ImageListStreamer))
			Me.imageList1.TransparentColor = System.Drawing.Color.Transparent
			Me.imageList1.Images.SetKeyName(0, "")
			Me.imageList1.Images.SetKeyName(1, "")
			' 
			' splitContainer1
			' 
			Me.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.splitContainer1.Location = New System.Drawing.Point(0, 60)
			Me.splitContainer1.Name = "splitContainer1"
			' 
			' splitContainer1.Panel1
			' 
			Me.splitContainer1.Panel1.Controls.Add(Me.splitContainer3)
			Me.splitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(5, 5, 2, 5)
			' 
			' splitContainer1.Panel2
			' 
			Me.splitContainer1.Panel2.Controls.Add(Me.splitContainer2)
			Me.splitContainer1.Panel2.Padding = New System.Windows.Forms.Padding(2, 5, 5, 5)
			Me.splitContainer1.Size = New System.Drawing.Size(870, 508)
			Me.splitContainer1.SplitterDistance = 265
			Me.splitContainer1.TabIndex = 5
			' 
			' splitContainer3
			' 
			Me.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
			Me.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
			Me.splitContainer3.IsSplitterFixed = True
			Me.splitContainer3.Location = New System.Drawing.Point(5, 5)
			Me.splitContainer3.Name = "splitContainer3"
			Me.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
			' 
			' splitContainer3.Panel1
			' 
			Me.splitContainer3.Panel1.Controls.Add(Me.panel2)
			' 
			' splitContainer3.Panel2
			' 
			Me.splitContainer3.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
			Me.splitContainer3.Panel2.Controls.Add(Me.pictureBox1)
			Me.splitContainer3.Panel2MinSize = 85
			Me.splitContainer3.Size = New System.Drawing.Size(258, 498)
			Me.splitContainer3.SplitterDistance = 391
			Me.splitContainer3.TabIndex = 0
			' 
			' panel2
			' 
			Me.panel2.Controls.Add(Me.treeViewCustomer)
			Me.panel2.Dock = System.Windows.Forms.DockStyle.Fill
			Me.panel2.ForeColor = System.Drawing.SystemColors.Control
			Me.panel2.Location = New System.Drawing.Point(0, 0)
			Me.panel2.Name = "panel2"
			Me.panel2.Size = New System.Drawing.Size(258, 391)
			Me.panel2.TabIndex = 1
			' 
			' pictureBox1
			' 
			Me.pictureBox1.BackColor = System.Drawing.Color.FromArgb((CInt(Fix((CByte(255))))), (CInt(Fix((CByte(154))))), (CInt(Fix((CByte(0))))))
			Me.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
			Me.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.pictureBox1.Image = (CType(resources.GetObject("pictureBox1.Image"), System.Drawing.Image))
			Me.pictureBox1.Location = New System.Drawing.Point(0, 0)
			Me.pictureBox1.Name = "pictureBox1"
			Me.pictureBox1.Size = New System.Drawing.Size(258, 103)
			Me.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
			Me.pictureBox1.TabIndex = 0
			Me.pictureBox1.TabStop = False
			' 
			' splitContainer2
			' 
			Me.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
			Me.splitContainer2.Location = New System.Drawing.Point(2, 5)
			Me.splitContainer2.Name = "splitContainer2"
			Me.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
			' 
			' splitContainer2.Panel1
			' 
			Me.splitContainer2.Panel1.Controls.Add(Me.dataGridViewOrders)
			' 
			' splitContainer2.Panel2
			' 
			Me.splitContainer2.Panel2.Controls.Add(Me.panel1)
			Me.splitContainer2.Size = New System.Drawing.Size(594, 498)
			Me.splitContainer2.SplitterDistance = 279
			Me.splitContainer2.TabIndex = 0
			' 
			' dataGridViewOrders
			' 
			Me.dataGridViewOrders.AllowUserToAddRows = False
			Me.dataGridViewOrders.AllowUserToDeleteRows = False
			Me.dataGridViewOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
			Me.dataGridViewOrders.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
			dataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(0)))
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True
			Me.dataGridViewOrders.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1
			Me.dataGridViewOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
			dataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(0)))
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False
			Me.dataGridViewOrders.DefaultCellStyle = dataGridViewCellStyle2
			Me.dataGridViewOrders.Dock = System.Windows.Forms.DockStyle.Fill
			Me.dataGridViewOrders.Location = New System.Drawing.Point(0, 0)
			Me.dataGridViewOrders.Margin = New System.Windows.Forms.Padding(0)
			Me.dataGridViewOrders.MultiSelect = False
			Me.dataGridViewOrders.Name = "dataGridViewOrders"
			Me.dataGridViewOrders.ReadOnly = True
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
			dataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(0)))
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True
			Me.dataGridViewOrders.RowHeadersDefaultCellStyle = dataGridViewCellStyle3
			Me.dataGridViewOrders.RowTemplate.Height = 18
			Me.dataGridViewOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
			Me.dataGridViewOrders.Size = New System.Drawing.Size(594, 279)
			Me.dataGridViewOrders.TabIndex = 0
'			Me.dataGridViewOrders.SelectionChanged += New System.EventHandler(Me.dataGridViewOrders_SelectionChanged);
			' 
			' panel1
			' 
			Me.panel1.Controls.Add(Me.dataGridViewOrderDetails)
			Me.panel1.Controls.Add(Me.buttonOrderDetails)
			Me.panel1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.panel1.Location = New System.Drawing.Point(0, 0)
			Me.panel1.Name = "panel1"
			Me.panel1.Size = New System.Drawing.Size(594, 215)
			Me.panel1.TabIndex = 0
			' 
			' dataGridViewOrderDetails
			' 
			Me.dataGridViewOrderDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
			Me.dataGridViewOrderDetails.BorderStyle = System.Windows.Forms.BorderStyle.None
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
			dataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(0)))
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True
			Me.dataGridViewOrderDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4
			Me.dataGridViewOrderDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
			dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
			dataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(0)))
			dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
			dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False
			Me.dataGridViewOrderDetails.DefaultCellStyle = dataGridViewCellStyle5
			Me.dataGridViewOrderDetails.Dock = System.Windows.Forms.DockStyle.Fill
			Me.dataGridViewOrderDetails.Location = New System.Drawing.Point(0, 23)
			Me.dataGridViewOrderDetails.Name = "dataGridViewOrderDetails"
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
			dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
			dataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(0)))
			dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True
			Me.dataGridViewOrderDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle6
			Me.dataGridViewOrderDetails.RowTemplate.Height = 18
			Me.dataGridViewOrderDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
			Me.dataGridViewOrderDetails.Size = New System.Drawing.Size(594, 192)
			Me.dataGridViewOrderDetails.TabIndex = 1
			' 
			' buttonOrderDetails
			' 
			Me.buttonOrderDetails.Dock = System.Windows.Forms.DockStyle.Top
			Me.buttonOrderDetails.Location = New System.Drawing.Point(0, 0)
			Me.buttonOrderDetails.Name = "buttonOrderDetails"
			Me.buttonOrderDetails.Size = New System.Drawing.Size(594, 23)
			Me.buttonOrderDetails.TabIndex = 0
			Me.buttonOrderDetails.Text = "Order Details"
			Me.buttonOrderDetails.UseVisualStyleBackColor = True
			' 
			' contextMenuStrip1
			' 
			Me.contextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { Me.toolStripMenuItem3, Me.toolStripMenuItem4, Me.toolStripMenuItem5})
			Me.contextMenuStrip1.Name = "contextMenuStripCustomer"
			Me.contextMenuStrip1.Size = New System.Drawing.Size(190, 70)
			' 
			' toolStripMenuItem3
			' 
			Me.toolStripMenuItem3.Image = (CType(resources.GetObject("toolStripMenuItem3.Image"), System.Drawing.Image))
			Me.toolStripMenuItem3.Name = "toolStripMenuItem3"
			Me.toolStripMenuItem3.Size = New System.Drawing.Size(189, 22)
			Me.toolStripMenuItem3.Text = "Add New Customer..."
			' 
			' toolStripMenuItem4
			' 
			Me.toolStripMenuItem4.Image = (CType(resources.GetObject("toolStripMenuItem4.Image"), System.Drawing.Image))
			Me.toolStripMenuItem4.Name = "toolStripMenuItem4"
			Me.toolStripMenuItem4.Size = New System.Drawing.Size(189, 22)
			Me.toolStripMenuItem4.Text = "Edit Customer..."
			' 
			' toolStripMenuItem5
			' 
			Me.toolStripMenuItem5.Image = (CType(resources.GetObject("toolStripMenuItem5.Image"), System.Drawing.Image))
			Me.toolStripMenuItem5.Name = "toolStripMenuItem5"
			Me.toolStripMenuItem5.Size = New System.Drawing.Size(189, 22)
			Me.toolStripMenuItem5.Text = "Delete Customer"
			' 
			' toolStripMenuItem6
			' 
			Me.toolStripMenuItem6.Image = (CType(resources.GetObject("toolStripMenuItem6.Image"), System.Drawing.Image))
			Me.toolStripMenuItem6.Name = "toolStripMenuItem6"
			Me.toolStripMenuItem6.ShortcutKeys = (CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys))
			Me.toolStripMenuItem6.Size = New System.Drawing.Size(150, 22)
			Me.toolStripMenuItem6.Text = "Cu&t"
			' 
			' toolStripMenuItem7
			' 
			Me.toolStripMenuItem7.Image = (CType(resources.GetObject("toolStripMenuItem7.Image"), System.Drawing.Image))
			Me.toolStripMenuItem7.Name = "toolStripMenuItem7"
			Me.toolStripMenuItem7.ShortcutKeys = (CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys))
			Me.toolStripMenuItem7.Size = New System.Drawing.Size(150, 22)
			Me.toolStripMenuItem7.Text = "&Copy"
			' 
			' toolStripMenuItem9
			' 
			Me.toolStripMenuItem9.Image = (CType(resources.GetObject("toolStripMenuItem9.Image"), System.Drawing.Image))
			Me.toolStripMenuItem9.Name = "toolStripMenuItem9"
			Me.toolStripMenuItem9.ShortcutKeys = (CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys))
			Me.toolStripMenuItem9.Size = New System.Drawing.Size(150, 22)
			Me.toolStripMenuItem9.Text = "Paste"
			' 
			' toolStripMenuItem10
			' 
			Me.toolStripMenuItem10.Name = "toolStripMenuItem10"
			Me.toolStripMenuItem10.Size = New System.Drawing.Size(122, 22)
			Me.toolStripMenuItem10.Text = "E&xit"
			' 
			' toolStripMenuItem12
			' 
			Me.toolStripMenuItem12.Image = (CType(resources.GetObject("toolStripMenuItem12.Image"), System.Drawing.Image))
			Me.toolStripMenuItem12.Name = "toolStripMenuItem12"
			Me.toolStripMenuItem12.Size = New System.Drawing.Size(122, 22)
			Me.toolStripMenuItem12.Text = "&Login..."
			' 
			' toolStripMenuItem13
			' 
			Me.toolStripMenuItem13.Image = (CType(resources.GetObject("toolStripMenuItem13.Image"), System.Drawing.Image))
			Me.toolStripMenuItem13.Name = "toolStripMenuItem13"
			Me.toolStripMenuItem13.Size = New System.Drawing.Size(122, 22)
			Me.toolStripMenuItem13.Text = "Lo&gout"
			' 
			' toolStripSeparator6
			' 
			Me.toolStripSeparator6.Name = "toolStripSeparator6"
			Me.toolStripSeparator6.Size = New System.Drawing.Size(119, 6)
			' 
			' toolStripMenuItem15
			' 
			Me.toolStripMenuItem15.Enabled = False
			Me.toolStripMenuItem15.Image = (CType(resources.GetObject("toolStripMenuItem15.Image"), System.Drawing.Image))
			Me.toolStripMenuItem15.Name = "toolStripMenuItem15"
			Me.toolStripMenuItem15.Size = New System.Drawing.Size(128, 22)
			Me.toolStripMenuItem15.Text = "&Add New"
			' 
			' toolStripMenuItem16
			' 
			Me.toolStripMenuItem16.Enabled = False
			Me.toolStripMenuItem16.Image = (CType(resources.GetObject("toolStripMenuItem16.Image"), System.Drawing.Image))
			Me.toolStripMenuItem16.Name = "toolStripMenuItem16"
			Me.toolStripMenuItem16.Size = New System.Drawing.Size(128, 22)
			Me.toolStripMenuItem16.Text = "&Edit"
			' 
			' toolStripMenuItem17
			' 
			Me.toolStripMenuItem17.Enabled = False
			Me.toolStripMenuItem17.Image = (CType(resources.GetObject("toolStripMenuItem17.Image"), System.Drawing.Image))
			Me.toolStripMenuItem17.Name = "toolStripMenuItem17"
			Me.toolStripMenuItem17.Size = New System.Drawing.Size(128, 22)
			Me.toolStripMenuItem17.Text = "&Delete"
			' 
			' toolStripMenuItem18
			' 
			Me.toolStripMenuItem18.Name = "toolStripMenuItem18"
			Me.toolStripMenuItem18.Size = New System.Drawing.Size(114, 22)
			Me.toolStripMenuItem18.Text = "&About"
			' 
			' toolStripSeparator8
			' 
			Me.toolStripSeparator8.Name = "toolStripSeparator8"
			Me.toolStripSeparator8.Size = New System.Drawing.Size(111, 6)
			' 
			' toolStripMenuItem19
			' 
			Me.toolStripMenuItem19.Image = (CType(resources.GetObject("toolStripMenuItem19.Image"), System.Drawing.Image))
			Me.toolStripMenuItem19.Name = "toolStripMenuItem19"
			Me.toolStripMenuItem19.Size = New System.Drawing.Size(114, 22)
			Me.toolStripMenuItem19.Text = "Index"
			' 
			' FormMain
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(870, 568)
			Me.Controls.Add(Me.splitContainer1)
			Me.Controls.Add(Me.toolStripAction)
			Me.Controls.Add(Me.menuStripAction)
			Me.Name = "FormMain"
			Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
			Me.Text = "Windows Forms Application -- Patterns-In-Action 3.5"
			Me.toolStripAction.ResumeLayout(False)
			Me.toolStripAction.PerformLayout()
			Me.menuStripAction.ResumeLayout(False)
			Me.menuStripAction.PerformLayout()
			Me.contextMenuStripCustomer.ResumeLayout(False)
			Me.splitContainer1.Panel1.ResumeLayout(False)
			Me.splitContainer1.Panel2.ResumeLayout(False)
			Me.splitContainer1.ResumeLayout(False)
			Me.splitContainer3.Panel1.ResumeLayout(False)
			Me.splitContainer3.Panel2.ResumeLayout(False)
			Me.splitContainer3.ResumeLayout(False)
			Me.panel2.ResumeLayout(False)
			CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.splitContainer2.Panel1.ResumeLayout(False)
			Me.splitContainer2.Panel2.ResumeLayout(False)
			Me.splitContainer2.ResumeLayout(False)
			CType(Me.dataGridViewOrders, System.ComponentModel.ISupportInitialize).EndInit()
			Me.panel1.ResumeLayout(False)
			CType(Me.dataGridViewOrderDetails, System.ComponentModel.ISupportInitialize).EndInit()
			Me.contextMenuStrip1.ResumeLayout(False)
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

		#End Region

		Private imageListCustomer As System.Windows.Forms.ImageList
		Private toolStripAction As System.Windows.Forms.ToolStrip
		Private WithEvents toolStripButtonLogin As System.Windows.Forms.ToolStripButton
		Private WithEvents toolStripButtonLogout As System.Windows.Forms.ToolStripButton
		Private toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
		Private toolStripButtonCut As System.Windows.Forms.ToolStripButton
		Private toolStripButtonCopy As System.Windows.Forms.ToolStripButton
		Private toolStripButtonPaste As System.Windows.Forms.ToolStripButton
		Private toolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
		Private WithEvents toolStripButtonAdd As System.Windows.Forms.ToolStripButton
		Private WithEvents toolStripButtonEdit As System.Windows.Forms.ToolStripButton
		Private WithEvents toolStripButtonDelete As System.Windows.Forms.ToolStripButton
		Private toolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
		Private WithEvents toolStripButtonHelp As System.Windows.Forms.ToolStripButton
		Private menuStripAction As System.Windows.Forms.MenuStrip
		Private fileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents loginToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents logoutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private toolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
		Private WithEvents exitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private editToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
		Private cutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private copyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private pasteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private customerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents addToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents editToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents deleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private helpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents indexToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private toolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
		Private WithEvents aboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private contextMenuStripCustomer As System.Windows.Forms.ContextMenuStrip
		Private WithEvents addNewCustomerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents editCustomerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents deleteCustomerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents treeViewCustomer As System.Windows.Forms.TreeView
		Private imageList1 As System.Windows.Forms.ImageList
		Private splitContainer1 As System.Windows.Forms.SplitContainer
		Private splitContainer3 As System.Windows.Forms.SplitContainer
		Private panel2 As System.Windows.Forms.Panel
		Private pictureBox1 As System.Windows.Forms.PictureBox
		Private splitContainer2 As System.Windows.Forms.SplitContainer
		Private WithEvents dataGridViewOrders As System.Windows.Forms.DataGridView
		Private panel1 As System.Windows.Forms.Panel
		Private dataGridViewOrderDetails As System.Windows.Forms.DataGridView
		Private buttonOrderDetails As System.Windows.Forms.Button
		Private contextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
		Private toolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
		Private toolStripMenuItem4 As System.Windows.Forms.ToolStripMenuItem
		Private toolStripMenuItem5 As System.Windows.Forms.ToolStripMenuItem
		Private toolStripMenuItem6 As System.Windows.Forms.ToolStripMenuItem
		Private toolStripMenuItem7 As System.Windows.Forms.ToolStripMenuItem
		Private toolStripMenuItem9 As System.Windows.Forms.ToolStripMenuItem
		Private toolStripMenuItem10 As System.Windows.Forms.ToolStripMenuItem
		Private toolStripMenuItem12 As System.Windows.Forms.ToolStripMenuItem
		Private toolStripMenuItem13 As System.Windows.Forms.ToolStripMenuItem
		Private toolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
		Private toolStripMenuItem15 As System.Windows.Forms.ToolStripMenuItem
		Private toolStripMenuItem16 As System.Windows.Forms.ToolStripMenuItem
		Private toolStripMenuItem17 As System.Windows.Forms.ToolStripMenuItem
		Private toolStripMenuItem18 As System.Windows.Forms.ToolStripMenuItem
		Private toolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
		Private toolStripMenuItem19 As System.Windows.Forms.ToolStripMenuItem
	End Class
End Namespace