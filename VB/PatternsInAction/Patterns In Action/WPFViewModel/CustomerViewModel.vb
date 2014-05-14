Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq
Imports System.Text

Imports System.Windows.Data
Imports System.Windows.Input

Imports WPFModel.BusinessModelObjects
Imports WPFModel.Provider

Namespace WPFViewModel
	''' <summary>
	''' ViewModel for Customer.
	''' </summary>
	''' <remarks>
	''' MV Patterns: MV-VM Design Pattern.
	''' </remarks>
    Public Class CustomerViewModel

        Private _provider As IProvider
        Private _customers As ObservableCollection(Of CustomerModel)
        Public Property Customers() As ObservableCollection(Of CustomerModel)
            Get
                Return _customers
            End Get
            Private Set(ByVal value As ObservableCollection(Of CustomerModel))
                _customers = value
            End Set
        End Property

        Private _addCommandModel As CommandModel
        Public Property AddCommandModel() As CommandModel
            Get
                Return _addCommandModel
            End Get
            Private Set(ByVal value As CommandModel)
                _addCommandModel = value
            End Set
        End Property
        Private _editCommandModel As CommandModel
        Public Property EditCommandModel() As CommandModel
            Get
                Return _editCommandModel
            End Get
            Private Set(ByVal value As CommandModel)
                _editCommandModel = value
            End Set
        End Property
        Private _deleteCommandModel As CommandModel
        Public Property DeleteCommandModel() As CommandModel
            Get
                Return _deleteCommandModel
            End Get
            Private Set(ByVal value As CommandModel)
                _deleteCommandModel = value
            End Set
        End Property

        ''' <summary>
        ''' Constructor
        ''' </summary>
        ''' <param name="provider">The provider.</param>
        Public Sub New(ByVal provider As IProvider)
            _provider = provider

            Customers = New ObservableCollection(Of CustomerModel)()

            AddCommandModel = New AddCommand(Me)
            EditCommandModel = New EditCommand(Me)
            DeleteCommandModel = New DeleteCommand(Me)

            Index = -1
        End Sub

        ''' <summary>
        ''' Gets the selected customer
        ''' </summary>
        Public ReadOnly Property SelectedCustomerModel() As CustomerModel
            Get
                Return If(Index >= 0, Customers(Index), Nothing)
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the index of the currently selected customer in the customer list.
        ''' </summary>
        Private _index As Integer
        Public Property Index() As Integer
            Private Get
                Return _index
            End Get
            Set(ByVal value As Integer)
                _index = value
            End Set
        End Property

        ''' <summary>
        ''' Indicates whether the customer data has been loaded.
        ''' </summary>
        Private _isLoaded As Boolean
        Public Property IsLoaded() As Boolean
            Get
                Return _isLoaded
            End Get
            Private Set(ByVal value As Boolean)
                _isLoaded = value
            End Set
        End Property

        ''' <summary>
        ''' Gets a new customer.
        ''' </summary>
        Public ReadOnly Property NewCustomerModel() As CustomerModel
            Get
                Return New CustomerModel(_provider)
            End Get
        End Property

        ''' <summary>
        ''' Indicates whether a new customer can be added.
        ''' </summary>
        Public ReadOnly Property CanAdd() As Boolean
            Get
                Return IsLoaded
            End Get
        End Property

        ''' <summary>
        ''' Indicates whether a customer is currently selected.
        ''' </summary>
        Public ReadOnly Property CanEdit() As Boolean
            Get
                Return IsLoaded AndAlso SelectedCustomerModel IsNot Nothing
            End Get
        End Property

        ''' <summary>
        ''' Indicates whether a customer is selected that can be deleted.
        ''' </summary>
        Public ReadOnly Property CanDelete() As Boolean
            Get
                Return IsLoaded AndAlso SelectedCustomerModel IsNot Nothing
            End Get
        End Property

        ''' <summary>
        ''' Indicates whether a customer is selected and orders can be viewed.
        ''' </summary>
        Public ReadOnly Property CanViewOrders() As Boolean
            Get
                Return IsLoaded AndAlso SelectedCustomerModel IsNot Nothing
            End Get
        End Property

        ''' <summary>
        ''' Retrieves and displays customers in given sort order.
        ''' </summary>
        ''' <param name="sortExpression">Sort order.</param>
        Public Sub LoadCustomers(ByVal sortExpression As String)
            For Each model As CustomerModel In _provider.GetCustomers(sortExpression)
                Customers.Add(model)
            Next model

            IsLoaded = True
        End Sub

        ''' <summary>
        ''' Clear customers from display.
        ''' </summary>
        Public Sub UnloadCustomers()
            Customers.Clear()
            IsLoaded = False
        End Sub

#Region "Private Command classes"

        ''' <summary>
        ''' Private implementation of the Add Command.
        ''' </summary>
        Private Class AddCommand
            Inherits CommandModel
            Private _viewModel As CustomerViewModel

            ''' <summary>
            ''' Constructor
            ''' </summary>
            ''' <param name="viewModel"></param>
            Public Sub New(ByVal viewModel As CustomerViewModel)
                _viewModel = viewModel
            End Sub

            Public Overrides Sub OnCanExecute(ByVal sender As Object, ByVal e As CanExecuteRoutedEventArgs)
                Dim customer As CustomerModel = TryCast(e.Parameter, CustomerModel)

                ' Check that all values have been entered.
                e.CanExecute = ((Not String.IsNullOrEmpty(customer.Company)) AndAlso (Not String.IsNullOrEmpty(customer.City)) AndAlso (Not String.IsNullOrEmpty(customer.Country)))

                e.Handled = True
            End Sub

            Public Overrides Sub OnExecute(ByVal sender As Object, ByVal e As ExecutedRoutedEventArgs)
                Dim customer As CustomerModel = TryCast(e.Parameter, CustomerModel)
                customer.Add()

                _viewModel.Customers.Add(customer)
            End Sub
        End Class

        ''' <summary>
        ''' Private implementation of the Edit command
        ''' </summary>
        Private Class EditCommand
            Inherits CommandModel
            Private _viewModel As CustomerViewModel

            Public Sub New(ByVal viewModel As CustomerViewModel)
                _viewModel = viewModel
            End Sub

            Public Overrides Sub OnCanExecute(ByVal sender As Object, ByVal e As CanExecuteRoutedEventArgs)
                Dim customer As CustomerModel = TryCast(e.Parameter, CustomerModel)

                ' Check that all values have been set
                e.CanExecute = (customer.CustomerId > 0 AndAlso (Not String.IsNullOrEmpty(customer.Company)) AndAlso (Not String.IsNullOrEmpty(customer.City)) AndAlso (Not String.IsNullOrEmpty(customer.Country)))

                e.Handled = True
            End Sub

            Public Overrides Sub OnExecute(ByVal sender As Object, ByVal e As ExecutedRoutedEventArgs)
                Dim customerModel As CustomerModel = TryCast(e.Parameter, CustomerModel)
                customerModel.Update()
            End Sub
        End Class

        ''' <summary>
        ''' Private implementation of the Delete command
        ''' </summary>
        Private Class DeleteCommand
            Inherits CommandModel
            Private _viewModel As CustomerViewModel

            Public Sub New(ByVal viewModel As CustomerViewModel)
                _viewModel = viewModel
            End Sub

            Public Overrides Sub OnCanExecute(ByVal sender As Object, ByVal e As CanExecuteRoutedEventArgs)
                e.CanExecute = _viewModel.CanDelete
                e.Handled = True
            End Sub

            Public Overrides Sub OnExecute(ByVal sender As Object, ByVal e As ExecutedRoutedEventArgs)
                Dim customerModel As CustomerModel = _viewModel.SelectedCustomerModel
                If customerModel.Delete() > 0 Then
                    _viewModel.Customers.Remove(customerModel)
                End If
            End Sub
        End Class

#End Region
    End Class
End Namespace
