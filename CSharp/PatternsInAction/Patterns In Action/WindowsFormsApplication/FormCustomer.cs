using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using WindowsFormsPresenter;
using WindowsFormsView;

namespace WindowsFormsApplication
{
    /// <summary>
    /// This form is used to add new customer data or edit 
    /// existing customer data. 
    /// </summary>
    public partial class FormCustomer : Form, ICustomerView
    {
        // The customer Presenter.
        private CustomerPresenter _customerPresenter;
        private bool _cancelClose;

        /// <summary>
        /// Default constructor of FormCustomer.
        /// </summary>
        public FormCustomer()
        {
            InitializeComponent();
            this.Closing += FormCustomer_Closing;

            // Initialize Presenter.
            _customerPresenter = new CustomerPresenter(this);
        }

        /// <summary>
        /// Gets or sets customer id
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets customer version.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets customer (company) name.
        /// </summary>
        public string Company
        {
            get { return textBoxCompany.Text.Trim(); }
            set { textBoxCompany.Text = value; }
        }

        /// <summary>
        /// Gets or sets customer city.
        /// </summary>
        public string City
        {
            get { return textBoxCity.Text.Trim(); }
            set { textBoxCity.Text = value; }
        }

        /// <summary>
        /// Gets or set customer country.
        /// </summary>
        public string Country
        {
            get { return textBoxCountry.Text.Trim(); }
            set { textBoxCountry.Text = value; }
        }

        /// <summary>
        /// Validates user input and, if valid, closes window. 
        /// </summary>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(CompanyName) ||
                String.IsNullOrEmpty(City) ||
                String.IsNullOrEmpty(Country))
            {
                // Do not close the dialog 
                MessageBox.Show("All fields are required");
                return;
            }

            try
            {
                _customerPresenter.Save();
                this.Close();
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message, "Save failed");
                _cancelClose = true;
            }
        }

        /// <summary>
        /// Provides opportunity to cancel window close.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormCustomer_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = _cancelClose;
            _cancelClose = false;
        }

        /// <summary>
        /// Checks for new customer or edit existing customer.
        /// After that it displays customer data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormCustomer_Load(object sender, EventArgs e)
        {
            // Check if this a new customer or existing one
            if (CustomerId == 0)
                this.Text = "New Customer";
            else
                this.Text = "Edit Customer";

            _customerPresenter.Display(CustomerId);
        }
    }
}
