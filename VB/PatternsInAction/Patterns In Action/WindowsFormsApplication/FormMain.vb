Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms

Imports WindowsFormsModel.BusinessObjects
Imports WindowsFormsPresenter
Imports WindowsFormsView

Namespace WindowsFormsApplication
	''' <summary>
	''' Main window for Windows Forms application. Most business logic resides 
	''' in this window as it responds to local control events, menu events, and 
	''' closed dialog events. This is usually the preferred model, unless the 
	''' child windows have significant processing requirements, then they handle 
	''' that themselves. 
	''' </summary>
	''' <remarks>
	''' All communications required for this application runs via the Service layer. 
	''' The application uses the Model View Presenter design pattern. Each of these
	''' reside in its own Visual Studio project.
	''' 
	''' MV Patterns: MVP design pattern is used throughout this WinForms application.
	''' </remarks>
	Partial Public Class FormMain
		Inherits Form
		Implements ICustomersView, IOrdersView
		Private _customersPresenter As CustomersPresenter
		Private _ordersPresenter As OrdersPresenter

		''' <summary>
		''' Default form constructor. 
		''' </summary>
		Public Sub New()
			InitializeComponent()

			' Create two Presenters. Note: the form is the view. 
			_customersPresenter = New CustomersPresenter(Me)
			_ordersPresenter = New OrdersPresenter(Me)
		End Sub

		''' <summary>
		''' Displays login dialog box and loads customer list in treeview.
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub loginToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles loginToolStripMenuItem.Click
			Using form As New FormLogin()
				If form.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
					_customersPresenter.Display()

					Status = LoginStatus.LoggedIn
				End If
			End Using
		End Sub

		''' <summary>
		''' Logoff user, empties datagridviews, and disables menus.
		''' </summary>
		Private Sub logoutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles logoutToolStripMenuItem.Click
			CType(New LogoutPresenter(Nothing), LogoutPresenter).Logout()
			Status = LoginStatus.LoggedOut
		End Sub

		''' <summary>
		''' Exits application.
		''' </summary>
		Private Sub exitToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles exitToolStripMenuItem.Click
			Me.Close()
		End Sub

		''' <summary>
		''' Retrieves order data from the web service for the customer currently selected.
		''' If, however, orders were retrieved previously, then these will be displayed. 
		''' The effect is that the client application speeds up over time. 
		''' </summary>
		Private Sub treeViewCustomer_AfterSelect(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles treeViewCustomer.AfterSelect
			' Get selected customer. Note: root node does not have a customer record
			Dim customer As CustomerModel = TryCast(treeViewCustomer.SelectedNode.Tag, CustomerModel)
			If customer Is Nothing Then
				Return
			End If

			' Check if orders were already retrieved for customer
			If customer.Orders.Count > 0 Then
				BindOrders(customer.Orders)
			Else
				Me.Cursor = Cursors.WaitCursor
				_ordersPresenter.Display(customer.CustomerId)

				Me.Cursor = Cursors.Default
			End If
		End Sub

		''' <summary>
		''' Databinds orders to dataGridView control. Private helper method.
		''' </summary>
		''' <param name="orders">Order list.</param>
		Private Sub BindOrders(ByVal orders As IList(Of OrderModel))
			If orders Is Nothing Then
				Return
			End If

			dataGridViewOrders.DataSource = orders

			dataGridViewOrders.Columns("Customer").Visible = False
			dataGridViewOrders.Columns("OrderDetails").Visible = False
			dataGridViewOrders.Columns("Version").Visible = False


			dataGridViewOrders.Columns("Freight").DefaultCellStyle.Format = "C"
			dataGridViewOrders.Columns("Freight").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

			dataGridViewOrders.Columns("RequiredDate").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
			dataGridViewOrders.Columns("RequiredDate").DefaultCellStyle.BackColor = Color.Cornsilk
			dataGridViewOrders.Columns("RequiredDate").DefaultCellStyle.Font = New Font(dataGridViewOrders.Font, FontStyle.Bold)

			dataGridViewOrders.Columns("OrderDate").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
		End Sub

		''' <summary>
		''' Displays order details (line items) that are part of the currently 
		''' selected order. 
		''' </summary>
		Private Sub dataGridViewOrders_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles dataGridViewOrders.SelectionChanged
			dataGridViewOrderDetails.DataSource = Nothing
			If dataGridViewOrders.SelectedRows.Count = 0 Then
				Return
			End If

			Dim row As DataGridViewRow = dataGridViewOrders.SelectedRows(0)
			If row Is Nothing Then
				Return
			End If

			Dim orderId As Integer = Integer.Parse(row.Cells("OrderId").Value.ToString())

			' Get customer record from treeview control.
			Dim customer As CustomerModel = TryCast(treeViewCustomer.SelectedNode.Tag, CustomerModel)

			' Check for root node. It does not have a customer record
			If customer Is Nothing Then
				Return
			End If

			' Locate order record
			For Each order As OrderModel In customer.Orders
				If order.OrderId = orderId Then
					If order.OrderDetails.Count = 0 Then
						Return
					End If

					dataGridViewOrderDetails.DataSource = order.OrderDetails

					dataGridViewOrderDetails.Columns("Order").Visible = False
					dataGridViewOrderDetails.Columns("Version").Visible = False

					dataGridViewOrderDetails.Columns("Discount").DefaultCellStyle.Format = "C"
					dataGridViewOrderDetails.Columns("UnitPrice").DefaultCellStyle.Format = "C"
					dataGridViewOrderDetails.Columns("Discount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
					dataGridViewOrderDetails.Columns("UnitPrice").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

					dataGridViewOrderDetails.Columns("Quantity").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
					dataGridViewOrderDetails.Columns("UnitPrice").DefaultCellStyle.BackColor = Color.Cornsilk

					Return
				End If
			Next order
		End Sub

		''' <summary>
		''' Adds a new customer.
		''' </summary>
		Private Sub addToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addToolStripMenuItem.Click
			Using form As New FormCustomer()
				If form.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
					' Redisplay list of customers
					_customersPresenter.Display()
				End If
			End Using
		End Sub

		''' <summary>
		''' Edits an existing customer record.
		''' </summary>
		Private Sub editToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles editToolStripMenuItem.Click
			' Check if a node is selected (and not the root)
			If treeViewCustomer.SelectedNode Is Nothing OrElse treeViewCustomer.SelectedNode.Text = "Customers" Then
				MessageBox.Show("No customer is currently selected", "Edit Customer", MessageBoxButtons.OK, MessageBoxIcon.Information)
				Return
			End If

			Using form As New FormCustomer()
				Dim customer As CustomerModel = TryCast(treeViewCustomer.SelectedNode.Tag, CustomerModel)
				form.CustomerId = customer.CustomerId

				If form.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
					' Redisplay list of customers
					_customersPresenter.Display()
				End If
			End Using
		End Sub

		''' <summary>
		''' Deletes a customer.
		''' </summary>
		Private Sub deleteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles deleteToolStripMenuItem.Click
			' Check if a node is selected (and not the root)
			If treeViewCustomer.SelectedNode Is Nothing OrElse treeViewCustomer.SelectedNode.Text = "Customers" Then
				MessageBox.Show("No customer is currently selected", "Delete Customer", MessageBoxButtons.OK, MessageBoxIcon.Information)
				Return
			End If

			Dim customer As CustomerModel = TryCast(treeViewCustomer.SelectedNode.Tag, CustomerModel)

			Try
				Dim rowsAffected As Integer = New CustomerPresenter(Nothing).Delete(customer.CustomerId)
				If rowsAffected = 0 Then
					MessageBox.Show("Cannot delete " & customer.Company & " because they have existing orders!", "Cannot Delete")
					Return
				End If

				' Remove node
				treeViewCustomer.Nodes(0).Nodes.Remove(treeViewCustomer.SelectedNode)
			Catch ex As ApplicationException
				MessageBox.Show(ex.Message, "Delete failed")
			End Try
		End Sub

		''' <summary>
		''' Selects clicked node and then displays context menu. The tree node selection
		''' is important here because this does not happen by default in this event. 
		''' </summary>
		Private Sub treeViewCustomer_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles treeViewCustomer.MouseUp
			If e.Button = MouseButtons.Right Then
				treeViewCustomer.SelectedNode = treeViewCustomer.GetNodeAt(e.Location)

				contextMenuStripCustomer.Show(CType(sender, Control), e.Location)
			End If
		End Sub

		''' <summary>
		''' Redirects login request to equivalent menu event handler.
		''' </summary>
		Private Sub toolStripButtonLogin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles toolStripButtonLogin.Click
			loginToolStripMenuItem_Click(Me, Nothing)
		End Sub

		''' <summary>
		''' Redirects logout request to equivalent menu event handler.
		''' </summary>
		Private Sub toolStripButtonLogout_Click(ByVal sender As Object, ByVal e As EventArgs) Handles toolStripButtonLogout.Click
			logoutToolStripMenuItem_Click(Me, Nothing)
		End Sub

		''' <summary>
		''' Redirects add customer request to equivalent menu event handler.
		''' </summary>
		Private Sub toolStripButtonAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles toolStripButtonAdd.Click, addNewCustomerToolStripMenuItem.Click
			If addToolStripMenuItem.Enabled Then
				addToolStripMenuItem_Click(Me, Nothing)
			End If
		End Sub

		''' <summary>
		''' Redirects edit customer request to equivalent menu event handler.
		''' </summary>
		Private Sub toolStripButtonEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles toolStripButtonEdit.Click, editCustomerToolStripMenuItem.Click
			If editToolStripMenuItem.Enabled Then
				editToolStripMenuItem_Click(Me, Nothing)
			End If
		End Sub

		''' <summary>
		''' Redirects delete customer request to equivalent menu event handler.
		''' </summary>
		Private Sub toolStripButtonDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles toolStripButtonDelete.Click, deleteCustomerToolStripMenuItem.Click
			If deleteToolStripMenuItem.Enabled Then
				deleteToolStripMenuItem_Click(Me, Nothing)
			End If
		End Sub

		''' <summary>
		''' Opens the about dialog window.
		''' </summary>
		Private Sub aboutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles aboutToolStripMenuItem.Click
			Dim form As New FormAbout()
			form.ShowDialog()
		End Sub

		''' <summary>
		''' Help toolbutton clicked event handler.
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub toolStripButtonHelp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles toolStripButtonHelp.Click
			MessageBox.Show(" Help is not implemented... ", "Help")
		End Sub

		''' <summary>
		''' Help menu item event handler.
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub indexToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles indexToolStripMenuItem.Click
			MessageBox.Show(" Help is not implemented... ", "Help")
		End Sub

		''' <summary>
		''' Adds a list of customers to the left tree control.
		''' </summary>
        Public WriteOnly Property Customers() As IList(Of CustomerModel) Implements ICustomersView.Customers
            Set(ByVal value As IList(Of CustomerModel))
                Dim theCustomers As IList(Of CustomerModel) = value
                ' Clear nodes under root of tree
                Dim root As TreeNode = treeViewCustomer.Nodes(0)
                root.Nodes.Clear()

                ' Build the customer tree
                For Each customer As CustomerModel In theCustomers
                    AddCustomerToTree(customer)
                Next customer
            End Set
        End Property

		''' <summary>
		''' Private helper function that appends a customer to the next node
		''' in the treeview control
		''' </summary>
		Private Function AddCustomerToTree(ByVal customer As CustomerModel) As TreeNode
			Dim node As New TreeNode()
			node.Text = customer.Company & " (" & customer.Country & ")"
			node.Tag = customer
			node.ImageIndex = 1
			node.SelectedImageIndex = 1
			Me.treeViewCustomer.Nodes(0).Nodes.Add(node)

			Return node
		End Function

		#Region "IOrderView Members"

		''' <summary>
		''' Databinds orders to dataGridView control
        ''' </summary>
        Public WriteOnly Property Orders() As IList(Of OrderModel) Implements IOrdersView.Orders
            Set(ByVal value As IList(Of OrderModel))
                ' Unpack order transfer objects into order business objects.
                Dim theOrders As IList(Of OrderModel) = value

                ' Store orders for next time this customer is selected.
                Dim customer As CustomerModel = TryCast(treeViewCustomer.SelectedNode.Tag, CustomerModel)
                customer.Orders = theOrders

                BindOrders(theOrders)
            End Set
        End Property
		#End Region

		#Region "Window State"

		' Enumerates login status: Logged In or Logged Out.
		Private Enum LoginStatus
			LoggedIn
			LoggedOut
		End Enum

		''' <summary>
		''' Central place where controls are enabled/disabled depending on
		''' logged in / logged out state.
		''' </summary>
		Private WriteOnly Property Status() As LoginStatus
			Set(ByVal value As LoginStatus)
				If value = LoginStatus.LoggedIn Then
					' Display tree expanded
					treeViewCustomer.ExpandAll()

					' Enable customer add/edit/delete menu items.
					Me.addToolStripMenuItem.Enabled = True
					Me.editToolStripMenuItem.Enabled = True
					Me.deleteToolStripMenuItem.Enabled = True

					' Disable login menu
					Me.loginToolStripMenuItem.Enabled = False
				Else
					' Clear nodes under root of tree
					Dim root As TreeNode = treeViewCustomer.Nodes(0)
					root.Nodes.Clear()

					' Clear orders (this is databound, cannot touch rows)
					dataGridViewOrders.DataSource = Nothing

					' Clear order details (this is not databound)
					dataGridViewOrderDetails.Rows.Clear()

					' Disable customer add/edit/delete menu items.
					Me.addToolStripMenuItem.Enabled = False
					Me.editToolStripMenuItem.Enabled = False
					Me.deleteToolStripMenuItem.Enabled = False

					' Disable login menu
					Me.loginToolStripMenuItem.Enabled = True
				End If
			End Set
		End Property

		#End Region
    End Class
End Namespace
