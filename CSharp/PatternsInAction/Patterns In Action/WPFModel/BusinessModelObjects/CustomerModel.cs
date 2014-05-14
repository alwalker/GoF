using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Threading;
using System.Configuration;
using System.Collections.ObjectModel;

using WPFModel.Provider;

namespace WPFModel.BusinessModelObjects
{
    /// <summary>
    /// Model of the customer. 
    /// </summary>
    public class CustomerModel : BaseModel
    {
        // Set ImageService url only once
        private static readonly string ImageService = ConfigurationManager.AppSettings.Get("ImageService");
        private string _imageUrl = ImageService + "GetCustomerImageSmall/0";

        private IProvider _provider;

        private int _customerId = 0;
        private string _company;
        private string _city;
        private string _country;
        private string _version;

        private ObservableCollection<OrderModel> _orders;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="provider">The provider for the customer.</param>
        public CustomerModel(IProvider provider)
        {
            _provider = provider;
        }
        /// <summary>
        /// Adds a new customer.
        /// </summary>
        public int Add()
        {
            int rowsAffected = _provider.AddCustomer(this);
            return rowsAffected;
        }

        /// <summary>
        /// Deletes a customer.
        /// </summary>
        public int Delete()
        {
            int rowsAffected = _provider.DeleteCustomer(this.CustomerId);
            return rowsAffected;
        }

        /// <summary>
        /// Updates a customer.
        /// </summary>
        public int Update()
        {
            int rowsAffected = _provider.UpdateCustomer(this);
            return rowsAffected;
        }

        /// <summary>
        /// Gets or sets customerId
        /// </summary>
        public int CustomerId
        {
            get { ConfirmOnUIThread(); return _customerId; }
            set { ConfirmOnUIThread(); if (_customerId != value) { _customerId = value; Notify("CustomerId"); } }
        }

        /// <summary>
        /// Gets or sets customer name.
        /// </summary>
        public string Company
        {
            get { ConfirmOnUIThread(); return _company; }
            set { ConfirmOnUIThread(); if (_company != value) { _company = value; Notify("Company"); } }
        }

        /// <summary>
        /// Gets or sets customer city.
        /// </summary>
        public string City
        {
            get { ConfirmOnUIThread(); return _city; }
            set { ConfirmOnUIThread(); if (_city != value) { _city = value; Notify("City"); } }
        }

        /// <summary>
        /// Gets or set customer country.
        /// </summary>
        public string Country
        {
            get { ConfirmOnUIThread(); return _country; }
            set { ConfirmOnUIThread(); if (_country != value) { _country = value; Notify("Country"); } }
        }

        /// <summary>
        /// Gets or sets list of orders associated with customer.
        /// </summary>
        public ObservableCollection<OrderModel> Orders
        {
            get { ConfirmOnUIThread(); LazyloadOrders(); return _orders; }
            set { ConfirmOnUIThread(); _orders = value; Notify("Orders"); }
        }

        // Helper
        private void LazyloadOrders()
        {
            if (_orders == null || _orders.Count == 0)
                Orders = _provider.GetOrders(this.CustomerId);
        }

        /// <summary>
        /// Gets url for small images.
        /// </summary>
        public string SmallImageUrl
        {
            get { ConfirmOnUIThread(); return ImageService + "GetCustomerImageSmall/" + _customerId; }
        }

        /// <summary>
        /// Gets url for large images.
        /// </summary>
        public string LargeImageUrl
        {
            get { ConfirmOnUIThread(); return ImageService + "GetCustomerImageLarge/" + _customerId; }
        }

        /// <summary>
        /// Gets or sets version number
        /// </summary>
        public string Version
        {
            get { ConfirmOnUIThread(); return _version; }
            set { ConfirmOnUIThread(); if (_version != value) { _version = value; Notify("Version"); } }
        }
    }
}
