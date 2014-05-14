using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using WPFModel.Provider;
using WPFModel.BusinessModelObjects;
using WPFViewModel;

namespace WPFApplication
{
    /// <summary>
    /// Main window for WPF application. Shows list of customers.
    /// </summary>
    public partial class WindowMain : Window
    {
        public CustomerViewModel ViewModel { private set; get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public WindowMain()
        {
            InitializeComponent();

            // Create viewmodel and set data context.
            ViewModel = new CustomerViewModel(new Provider());
            DataContext = ViewModel;
        }

        /// <summary>
        /// Loads all customer sorted by company name.
        /// </summary>
        public void LoadCustomers()
        {
            Cursor = Cursors.Wait;
            ViewModel.LoadCustomers("CompanyName ASC");
            Cursor = Cursors.Arrow;
        }

        /// <summary>
        /// Unloads customers.
        /// </summary>
        public void UnloadCustomers()
        {
            ViewModel.UnloadCustomers();
        }

        /// <summary>
        /// Double clicking on customer rectangle opens Orders dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ViewOrdersCommand_Executed(null, null);
        }

        #region Menu Command handlers

        /// <summary>
        /// Checks if login command can execute.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (ViewModel.IsLoaded == false);
        }

        /// <summary>
        /// Executes login command. Opens login dialog and loads customers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            WindowLogin window = new WindowLogin();
            window.Owner = this; // This will center dialog in owner window

            if (window.ShowDialog() == true)
            {
                LoadCustomers();

                CommandManager.InvalidateRequerySuggested();
            }
        }

        /// <summary>
        /// Checks if logout command can execute.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogoutCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (ViewModel.IsLoaded == true);
        }


        /// <summary>
        /// Executes logout command. Unload customers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogoutCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            UnloadCustomers();

            CommandManager.InvalidateRequerySuggested();
        }

        /// <summary>
        /// Executes exit command. Shutdown application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Checks if add-customer command can execute.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ViewModel.CanAdd;
        }

        /// <summary>
        /// Executes add-customer command. Opens customer dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            WindowCustomer window = new WindowCustomer();
            window.Owner = this;
            window.IsNewCustomer = true;

            if (window.ShowDialog() == true)
            {
                CommandManager.InvalidateRequerySuggested();
            }
        }

        /// <summary>
        /// Checks if edit-customer command can execute.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ViewModel.CanEdit;
        }

        /// <summary>
        /// Execute edit-customer command. Opens customer dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            WindowCustomer window = new WindowCustomer();
            window.Owner = this;

            if (window.ShowDialog() == true)
            {
                CommandManager.InvalidateRequerySuggested();
            }
        }

        /// <summary>
        /// Checks if delete-customer command can execute.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ViewModel.CanDelete;
        }

        /// <summary>
        /// Executes delete-customer command.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModel.DeleteCommandModel.OnExecute(this, null);

            if (ViewModel.SelectedCustomerModel != null)
                MessageBox.Show("Cannot delete customer because they have existing orders.", "Delete Customer");

            CommandManager.InvalidateRequerySuggested();
        }

        /// <summary>
        /// Checks if view-orders command can execute.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewOrdersCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ViewModel.CanViewOrders;
        }

        /// <summary>
        /// Execute view-orders command. Opens orders dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewOrdersCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            WindowOrders window = new WindowOrders();
            window.Owner = this;
            window.ShowDialog();

            CommandManager.InvalidateRequerySuggested();
        }

        /// <summary>
        /// Executes How-do-I menu command
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HowDoICommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("How do I help is not implemented", "How Do I");
        }

        /// <summary>
        /// Executes help index command.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IndexCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Help index is not implemented", "Index");
        }

        /// <summary>
        /// Executes about command. Opens about box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            WindowAbout window = new WindowAbout();
            window.Owner = this;
            window.ShowDialog();
        }

        #endregion

        
    }
}
