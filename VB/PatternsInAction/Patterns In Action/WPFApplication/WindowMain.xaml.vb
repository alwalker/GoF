Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Shapes

Imports WPFModel.Provider
Imports WPFModel.BusinessModelObjects
Imports WPFViewModel

Namespace WPFApplication
	''' <summary>
	''' Main window for WPF application. Shows list of customers.
	''' </summary>
	Partial Public Class WindowMain
		Inherits Window
        Private _viewModel As CustomerViewModel
        Public Property ViewModel() As CustomerViewModel
            Get
                Return _viewModel
            End Get
            Private Set(ByVal value As CustomerViewModel)
                _viewModel = value
            End Set
        End Property

		''' <summary>
		''' Constructor
		''' </summary>
		Public Sub New()
			InitializeComponent()

			' Create viewmodel and set data context.
			ViewModel = New CustomerViewModel(New Provider())
			DataContext = ViewModel
		End Sub

		''' <summary>
		''' Loads all customer sorted by company name.
		''' </summary>
		Public Sub LoadCustomers()
			Cursor = Cursors.Wait
			ViewModel.LoadCustomers("CompanyName ASC")
			Cursor = Cursors.Arrow
		End Sub

		''' <summary>
		''' Unloads customers.
		''' </summary>
		Public Sub UnloadCustomers()
			ViewModel.UnloadCustomers()
		End Sub

		''' <summary>
		''' Double clicking on customer rectangle opens Orders dialog.
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub CustomerListBox_MouseDoubleClick(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
			ViewOrdersCommand_Executed(Nothing, Nothing)
		End Sub

		#Region "Menu Command handlers"

		''' <summary>
		''' Checks if login command can execute.
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub LoginCommand_CanExecute(ByVal sender As Object, ByVal e As CanExecuteRoutedEventArgs)
			e.CanExecute = (ViewModel.IsLoaded = False)
		End Sub

		''' <summary>
		''' Executes login command. Opens login dialog and loads customers.
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub LoginCommand_Executed(ByVal sender As Object, ByVal e As ExecutedRoutedEventArgs)
			Dim window As New WindowLogin()
			window.Owner = Me ' This will center dialog in owner window

			If window.ShowDialog() = True Then
				LoadCustomers()

				CommandManager.InvalidateRequerySuggested()
			End If
		End Sub

		''' <summary>
		''' Checks if logout command can execute.
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub LogoutCommand_CanExecute(ByVal sender As Object, ByVal e As CanExecuteRoutedEventArgs)
			e.CanExecute = (ViewModel.IsLoaded = True)
		End Sub


		''' <summary>
		''' Executes logout command. Unload customers.
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub LogoutCommand_Executed(ByVal sender As Object, ByVal e As ExecutedRoutedEventArgs)
			UnloadCustomers()

			CommandManager.InvalidateRequerySuggested()
		End Sub

		''' <summary>
		''' Executes exit command. Shutdown application.
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub ExitCommand_Executed(ByVal sender As Object, ByVal e As ExecutedRoutedEventArgs)
			Application.Current.Shutdown()
		End Sub

		''' <summary>
		''' Checks if add-customer command can execute.
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub AddCommand_CanExecute(ByVal sender As Object, ByVal e As CanExecuteRoutedEventArgs)
			e.CanExecute = ViewModel.CanAdd
		End Sub

		''' <summary>
		''' Executes add-customer command. Opens customer dialog.
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub AddCommand_Executed(ByVal sender As Object, ByVal e As ExecutedRoutedEventArgs)
			Dim window As New WindowCustomer()
			window.Owner = Me
			window.IsNewCustomer = True

			If window.ShowDialog() = True Then
				CommandManager.InvalidateRequerySuggested()
			End If
		End Sub

		''' <summary>
		''' Checks if edit-customer command can execute.
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub EditCommand_CanExecute(ByVal sender As Object, ByVal e As CanExecuteRoutedEventArgs)
			e.CanExecute = ViewModel.CanEdit
		End Sub

		''' <summary>
		''' Execute edit-customer command. Opens customer dialog.
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub EditCommand_Executed(ByVal sender As Object, ByVal e As ExecutedRoutedEventArgs)
			Dim window As New WindowCustomer()
			window.Owner = Me

			If window.ShowDialog() = True Then
				CommandManager.InvalidateRequerySuggested()
			End If
		End Sub

		''' <summary>
		''' Checks if delete-customer command can execute.
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub DeleteCommand_CanExecute(ByVal sender As Object, ByVal e As CanExecuteRoutedEventArgs)
			e.CanExecute = ViewModel.CanDelete
		End Sub

		''' <summary>
		''' Executes delete-customer command.
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub DeleteCommand_Executed(ByVal sender As Object, ByVal e As ExecutedRoutedEventArgs)
			ViewModel.DeleteCommandModel.OnExecute(Me, Nothing)

			If ViewModel.SelectedCustomerModel IsNot Nothing Then
				MessageBox.Show("Cannot delete customer because they have existing orders.", "Delete Customer")
			End If

			CommandManager.InvalidateRequerySuggested()
		End Sub

		''' <summary>
		''' Checks if view-orders command can execute.
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub ViewOrdersCommand_CanExecute(ByVal sender As Object, ByVal e As CanExecuteRoutedEventArgs)
			e.CanExecute = ViewModel.CanViewOrders
		End Sub

		''' <summary>
		''' Execute view-orders command. Opens orders dialog.
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub ViewOrdersCommand_Executed(ByVal sender As Object, ByVal e As ExecutedRoutedEventArgs)
			Dim window As New WindowOrders()
			window.Owner = Me
			window.ShowDialog()

			CommandManager.InvalidateRequerySuggested()
		End Sub

		''' <summary>
		''' Executes How-do-I menu command
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub HowDoICommand_Executed(ByVal sender As Object, ByVal e As ExecutedRoutedEventArgs)
			MessageBox.Show("How do I help is not implemented", "How Do I")
		End Sub

		''' <summary>
		''' Executes help index command.
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub IndexCommand_Executed(ByVal sender As Object, ByVal e As ExecutedRoutedEventArgs)
			MessageBox.Show("Help index is not implemented", "Index")
		End Sub

		''' <summary>
		''' Executes about command. Opens about box.
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub AboutCommand_Executed(ByVal sender As Object, ByVal e As ExecutedRoutedEventArgs)
			Dim window As New WindowAbout()
			window.Owner = Me
			window.ShowDialog()
		End Sub

		#End Region


	End Class
End Namespace
