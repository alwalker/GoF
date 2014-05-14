using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using WindowsFormsModel.BusinessObjects;
using WindowsFormsPresenter;
using WindowsFormsView;

namespace WindowsFormsApplication
{
    /// <summary>
    /// Main window for Windows Forms application. Most business logic resides 
    /// in this window as it responds to local control events, menu events, and 
    /// closed dialog events. This is usually the preferred model, unless the 
    /// child windows have significant processing requirements, then they handle 
    /// that themselves. 
    /// </summary>
    /// <remarks>
    /// All communications required for this application runs via the Service layer. 
    /// The application uses the Model View Presenter design pattern. Each of these
    /// reside in its own Visual Studio project.
    /// 
    /// MV Patterns: MVP design pattern is used throughout this WinForms application.
    /// </remarks>
    public partial class FormMain : Form, ICustomersView, IOrdersView
    {
        private CustomersPresenter _customersPresenter;
        private OrdersPresenter _ordersPresenter;

        /// <summary>
        /// Default form constructor. 
        /// </summary>
        public FormMain()
        {
            InitializeComponent();

            // Create two Presenters. Note: the form is the view. 
            _customersPresenter = new CustomersPresenter(this);
            _ordersPresenter = new OrdersPresenter(this);
        }

        /// <summary>
        /// Displays login dialog box and loads customer list in treeview.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FormLogin form = new FormLogin())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _customersPresenter.Display();

                    Status = LoginStatus.LoggedIn;
                }
            }
        }

        /// <summary>
        /// Logoff user, empties datagridviews, and disables menus.
        /// </summary>
        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new LogoutPresenter(null).Logout();
            Status = LoginStatus.LoggedOut;
        }

        /// <summary>
        /// Exits application.
        /// </summary>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Retrieves order data from the web service for the customer currently selected.
        /// If, however, orders were retrieved previously, then these will be displayed. 
        /// The effect is that the client application speeds up over time. 
        /// </summary>
        private void treeViewCustomer_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Get selected customer. Note: root node does not have a customer record
            CustomerModel customer = treeViewCustomer.SelectedNode.Tag as CustomerModel;
            if (customer == null) return;

            // Check if orders were already retrieved for customer
            if (customer.Orders.Count > 0)
                BindOrders(customer.Orders);
            else
            {
                this.Cursor = Cursors.WaitCursor;
                _ordersPresenter.Display(customer.CustomerId);

                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Databinds orders to dataGridView control. Private helper method.
        /// </summary>
        /// <param name="orders">Order list.</param>
        private void BindOrders(IList<OrderModel> orders)
        {
            if (orders == null) return;

            dataGridViewOrders.DataSource = orders;

            dataGridViewOrders.Columns["Customer"].Visible = false;
            dataGridViewOrders.Columns["OrderDetails"].Visible = false;
            dataGridViewOrders.Columns["Version"].Visible = false;


            dataGridViewOrders.Columns["Freight"].DefaultCellStyle.Format = "C";
            dataGridViewOrders.Columns["Freight"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridViewOrders.Columns["RequiredDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewOrders.Columns["RequiredDate"].DefaultCellStyle.BackColor = Color.Cornsilk;
            dataGridViewOrders.Columns["RequiredDate"].DefaultCellStyle.Font = new Font(dataGridViewOrders.Font, FontStyle.Bold);

            dataGridViewOrders.Columns["OrderDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        /// <summary>
        /// Displays order details (line items) that are part of the currently 
        /// selected order. 
        /// </summary>
        private void dataGridViewOrders_SelectionChanged(object sender, EventArgs e)
        {
            dataGridViewOrderDetails.DataSource = null;
            if (dataGridViewOrders.SelectedRows.Count == 0) return;

            DataGridViewRow row = dataGridViewOrders.SelectedRows[0];
            if (row == null) return;

            int orderId = int.Parse(row.Cells["OrderId"].Value.ToString());

            // Get customer record from treeview control.
            CustomerModel customer = treeViewCustomer.SelectedNode.Tag as CustomerModel;

            // Check for root node. It does not have a customer record
            if (customer == null) return;

            // Locate order record
            foreach (OrderModel order in customer.Orders)
            {
                if (order.OrderId == orderId)
                {
                    if (order.OrderDetails.Count == 0) return;

                    dataGridViewOrderDetails.DataSource = order.OrderDetails;

                    dataGridViewOrderDetails.Columns["Order"].Visible = false;
                    dataGridViewOrderDetails.Columns["Version"].Visible = false;

                    dataGridViewOrderDetails.Columns["Discount"].DefaultCellStyle.Format = "C";
                    dataGridViewOrderDetails.Columns["UnitPrice"].DefaultCellStyle.Format = "C";
                    dataGridViewOrderDetails.Columns["Discount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridViewOrderDetails.Columns["UnitPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    dataGridViewOrderDetails.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridViewOrderDetails.Columns["UnitPrice"].DefaultCellStyle.BackColor = Color.Cornsilk;

                    return;
                }
            }
        }

        /// <summary>
        /// Adds a new customer.
        /// </summary>
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FormCustomer form = new FormCustomer())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // Redisplay list of customers
                    _customersPresenter.Display();
                }
            }
        }

        /// <summary>
        /// Edits an existing customer record.
        /// </summary>
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check if a node is selected (and not the root)
            if (treeViewCustomer.SelectedNode == null ||
                treeViewCustomer.SelectedNode.Text == "Customers")
            {
                MessageBox.Show("No customer is currently selected",
                            "Edit Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (FormCustomer form = new FormCustomer())
            {
                CustomerModel customer = treeViewCustomer.SelectedNode.Tag as CustomerModel;
                form.CustomerId = customer.CustomerId;

                if (form.ShowDialog() == DialogResult.OK)
                {
                    // Redisplay list of customers
                    _customersPresenter.Display();
                }
            }
        }

        /// <summary>
        /// Deletes a customer.
        /// </summary>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check if a node is selected (and not the root)
            if (treeViewCustomer.SelectedNode == null ||
                treeViewCustomer.SelectedNode.Text == "Customers")
            {
                MessageBox.Show("No customer is currently selected",
                            "Delete Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CustomerModel customer = treeViewCustomer.SelectedNode.Tag as CustomerModel;

            try
            {
                int rowsAffected = new CustomerPresenter(null).Delete(customer.CustomerId);
                if (rowsAffected == 0)
                {
                    MessageBox.Show("Cannot delete " + customer.Company + " because they have existing orders!", "Cannot Delete");
                    return;
                }

                // Remove node
                treeViewCustomer.Nodes[0].Nodes.Remove(treeViewCustomer.SelectedNode);
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message, "Delete failed");
            }
        }

        /// <summary>
        /// Selects clicked node and then displays context menu. The tree node selection
        /// is important here because this does not happen by default in this event. 
        /// </summary>
        private void treeViewCustomer_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                treeViewCustomer.SelectedNode =
                    treeViewCustomer.GetNodeAt(e.Location);

                contextMenuStripCustomer.Show((Control)sender, e.Location);
            }
        }

        /// <summary>
        /// Redirects login request to equivalent menu event handler.
        /// </summary>
        private void toolStripButtonLogin_Click(object sender, EventArgs e)
        {
            loginToolStripMenuItem_Click(this, null);
        }

        /// <summary>
        /// Redirects logout request to equivalent menu event handler.
        /// </summary>
        private void toolStripButtonLogout_Click(object sender, EventArgs e)
        {
            logoutToolStripMenuItem_Click(this, null);
        }

        /// <summary>
        /// Redirects add customer request to equivalent menu event handler.
        /// </summary>
        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            if (addToolStripMenuItem.Enabled)
                addToolStripMenuItem_Click(this, null);
        }

        /// <summary>
        /// Redirects edit customer request to equivalent menu event handler.
        /// </summary>
        private void toolStripButtonEdit_Click(object sender, EventArgs e)
        {
            if (editToolStripMenuItem.Enabled)
                editToolStripMenuItem_Click(this, null);
        }

        /// <summary>
        /// Redirects delete customer request to equivalent menu event handler.
        /// </summary>
        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            if (deleteToolStripMenuItem.Enabled)
                deleteToolStripMenuItem_Click(this, null);
        }

        /// <summary>
        /// Opens the about dialog window.
        /// </summary>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout form = new FormAbout();
            form.ShowDialog();
        }

        /// <summary>
        /// Help toolbutton clicked event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" Help is not implemented... ", "Help");
        }

        /// <summary>
        /// Help menu item event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void indexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" Help is not implemented... ", "Help");
        }

        /// <summary>
        /// Adds a list of customers to the left tree control.
        /// </summary>
        public IList<CustomerModel> Customers
        {
            set
            {
                IList<CustomerModel> customers = value;
                // Clear nodes under root of tree
                TreeNode root = treeViewCustomer.Nodes[0];
                root.Nodes.Clear();

                // Build the customer tree
                foreach (CustomerModel customer in customers)
                {
                    AddCustomerToTree(customer);
                }
            }
        }

        /// <summary>
        /// Private helper function that appends a customer to the next node
        /// in the treeview control
        /// </summary>
        private TreeNode AddCustomerToTree(CustomerModel customer)
        {
            TreeNode node = new TreeNode();
            node.Text = customer.Company + " (" + customer.Country + ")";
            node.Tag = customer;
            node.ImageIndex = 1;
            node.SelectedImageIndex = 1;
            this.treeViewCustomer.Nodes[0].Nodes.Add(node);

            return node;
        }

        #region IOrderView Members

        /// <summary>
        /// Databinds orders to dataGridView control
        /// </summary>
        public IList<OrderModel> Orders
        {
            set
            {
                // Unpack order transfer objects into order business objects.
                IList<OrderModel> orders = value;

                // Store orders for next time this customer is selected.
                CustomerModel customer = treeViewCustomer.SelectedNode.Tag as CustomerModel;
                customer.Orders = orders;

                BindOrders(orders);
            }
        }
        #endregion

        #region Window State

        // Enumerates login status: Logged In or Logged Out.
        private enum LoginStatus
        {
            LoggedIn,
            LoggedOut
        }

        /// <summary>
        /// Central place where controls are enabled/disabled depending on
        /// logged in / logged out state.
        /// </summary>
        private LoginStatus Status
        {
            set
            {
                if (value == LoginStatus.LoggedIn)
                {
                    // Display tree expanded
                    treeViewCustomer.ExpandAll();

                    // Enable customer add/edit/delete menu items.
                    this.addToolStripMenuItem.Enabled = true;
                    this.editToolStripMenuItem.Enabled = true;
                    this.deleteToolStripMenuItem.Enabled = true;

                    // Disable login menu
                    this.loginToolStripMenuItem.Enabled = false;
                }
                else
                {
                    // Clear nodes under root of tree
                    TreeNode root = treeViewCustomer.Nodes[0];
                    root.Nodes.Clear();

                    // Clear orders (this is databound, cannot touch rows)
                    dataGridViewOrders.DataSource = null;

                    // Clear order details (this is not databound)
                    dataGridViewOrderDetails.Rows.Clear();

                    // Disable customer add/edit/delete menu items.
                    this.addToolStripMenuItem.Enabled = false;
                    this.editToolStripMenuItem.Enabled = false;
                    this.deleteToolStripMenuItem.Enabled = false;

                    // Disable login menu
                    this.loginToolStripMenuItem.Enabled = true;
                }
            }
        }

        #endregion
    }
}
