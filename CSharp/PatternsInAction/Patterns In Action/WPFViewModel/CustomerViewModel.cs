using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using System.Windows.Data;
using System.Windows.Input;

using WPFModel.BusinessModelObjects;
using WPFModel.Provider;

namespace WPFViewModel
{
    /// <summary>
    /// ViewModel for Customer.
    /// </summary>
    /// <remarks>
    /// MV Patterns: MV-VM Design Pattern.
    /// </remarks>
    public class CustomerViewModel
    {
        private IProvider _provider;
        public ObservableCollection<CustomerModel> Customers { private set; get; }

        public CommandModel AddCommandModel{ private set; get; }
        public CommandModel EditCommandModel { private set; get; }
        public CommandModel DeleteCommandModel { private set; get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="provider">The provider.</param>
        public CustomerViewModel(IProvider provider)
        {
            _provider = provider;

            Customers = new ObservableCollection<CustomerModel>();

            AddCommandModel = new AddCommand(this);
            EditCommandModel = new EditCommand(this);
            DeleteCommandModel = new DeleteCommand(this);

            Index = -1;
        }

        /// <summary>
        /// Gets the selected customer
        /// </summary>
        public CustomerModel SelectedCustomerModel
        {
            get { return Index >= 0 ? Customers[Index] : null; }
        }

        /// <summary>
        /// Gets or sets the index of the currently selected customer in the customer list.
        /// </summary>
        public int Index{ private get; set; }

        /// <summary>
        /// Indicates whether the customer data has been loaded.
        /// </summary>
        public bool IsLoaded { private set; get; }

        /// <summary>
        /// Gets a new customer.
        /// </summary>
        public CustomerModel NewCustomerModel
        {
            get { return new CustomerModel(_provider); }
        }

        /// <summary>
        /// Indicates whether a new customer can be added.
        /// </summary>
        public bool CanAdd
        {
            get { return IsLoaded; }
        }

        /// <summary>
        /// Indicates whether a customer is currently selected.
        /// </summary>
        public bool CanEdit
        {
            get { return IsLoaded && SelectedCustomerModel != null; }
        }

        /// <summary>
        /// Indicates whether a customer is selected that can be deleted.
        /// </summary>
        public bool CanDelete
        {
            get { return IsLoaded && SelectedCustomerModel != null; }
        }

        /// <summary>
        /// Indicates whether a customer is selected and orders can be viewed.
        /// </summary>
        public bool CanViewOrders
        {
            get { return IsLoaded && SelectedCustomerModel != null; }
        }

        /// <summary>
        /// Retrieves and displays customers in given sort order.
        /// </summary>
        /// <param name="sortExpression">Sort order.</param>
        public void LoadCustomers(string sortExpression)
        {
            foreach(CustomerModel model in _provider.GetCustomers(sortExpression))
                Customers.Add(model);

            IsLoaded = true;
        }

        /// <summary>
        /// Clear customers from display.
        /// </summary>
        public void UnloadCustomers()
        {
            Customers.Clear();
            IsLoaded = false;
        }

        #region Private Command classes

        /// <summary>
        /// Private implementation of the Add Command.
        /// </summary>
        private class AddCommand : CommandModel
        {
            private CustomerViewModel _viewModel;

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="viewModel"></param>
            public AddCommand(CustomerViewModel viewModel)
            {
                _viewModel = viewModel;
            }

            public override void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
            {
                CustomerModel customer = e.Parameter as CustomerModel;
                
                // Check that all values have been entered.
                e.CanExecute =
                    (!string.IsNullOrEmpty(customer.Company)
                  && !string.IsNullOrEmpty(customer.City)
                  && !string.IsNullOrEmpty(customer.Country));

                e.Handled = true;
            }

            public override void OnExecute(object sender, ExecutedRoutedEventArgs e)
            {
                CustomerModel customer = e.Parameter as CustomerModel;
                customer.Add();

                _viewModel.Customers.Add(customer);
            }
        }

        /// <summary>
        /// Private implementation of the Edit command
        /// </summary>
        private class EditCommand : CommandModel
        {
            private CustomerViewModel _viewModel;

            public EditCommand(CustomerViewModel viewModel)
            {
                _viewModel = viewModel;
            }

            public override void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
            {
                CustomerModel customer = e.Parameter as CustomerModel;

                // Check that all values have been set
                e.CanExecute = (customer.CustomerId > 0
                  && !string.IsNullOrEmpty(customer.Company)
                  && !string.IsNullOrEmpty(customer.City)
                  && !string.IsNullOrEmpty(customer.Country));

                e.Handled = true;
            }

            public override void OnExecute(object sender, ExecutedRoutedEventArgs e)
            {
                CustomerModel customerModel = e.Parameter as CustomerModel;
                customerModel.Update();
            }
        }

        /// <summary>
        /// Private implementation of the Delete command
        /// </summary>
        private class DeleteCommand : CommandModel
        {
            private CustomerViewModel _viewModel;

            public DeleteCommand(CustomerViewModel viewModel)
            {
                _viewModel = viewModel;
            }

            public override void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
            {
                e.CanExecute = _viewModel.CanDelete;
                e.Handled = true;
            }

            public override void OnExecute(object sender, ExecutedRoutedEventArgs e)
            {
                CustomerModel customerModel = _viewModel.SelectedCustomerModel;
                if (customerModel.Delete() > 0)
                    _viewModel.Customers.Remove(customerModel);
            }
        }

        #endregion
    }
}
