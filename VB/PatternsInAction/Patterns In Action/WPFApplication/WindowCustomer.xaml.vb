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

Imports WPFModel.BusinessModelObjects
Imports WPFViewModel

Namespace WPFApplication
	''' <summary>
	''' Customer details edit window.
	''' </summary>
	Partial Public Class WindowCustomer
		Inherits Window
        Private _isNewCustomer As Boolean
        Public Property IsNewCustomer() As Boolean
            Get
                Return _isNewCustomer
            End Get
            Set(ByVal value As Boolean)
                _isNewCustomer = value
            End Set
        End Property

		Private _originalCompany As String
		Private _originalCity As String
		Private _originalCountry As String

		Public Sub New()
			InitializeComponent()
		End Sub

		''' <summary>
		''' Helper. Makes it easy to get to customer ViewModel.
		''' </summary>
		Private ReadOnly Property CustomerViewModel() As CustomerViewModel
			Get
				Return (TryCast(Application.Current, App)).CustomerViewModel
			End Get
		End Property

		''' <summary>
		''' Loads new or existing record.
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub Window_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
			Dim commandModel As CommandModel

			' New customer.
			If IsNewCustomer Then

				Me.DataContext = CustomerViewModel.NewCustomerModel
				Title = "Add new customer"

				Dim bitmap As New BitmapImage(New Uri(CustomerViewModel.NewCustomerModel.LargeImageUrl))
				CustomerImage.Source = bitmap

				commandModel = CustomerViewModel.AddCommandModel

				' Display little hint message
				LabelNewMessage1.Visibility = Visibility.Visible
				LabelNewMessage2.Visibility = Visibility.Visible
			Else
				Me.DataContext = CustomerViewModel.SelectedCustomerModel

				' Save off original values. Due to binding viewmodel is changed immediately when editing.
				' So, when canceling we have these values to restore original state.
				' Suggestion: could be implemented as Memento pattern.
				_originalCompany = CustomerViewModel.SelectedCustomerModel.Company
				_originalCity = CustomerViewModel.SelectedCustomerModel.City
				_originalCountry = CustomerViewModel.SelectedCustomerModel.Country

				Title = "Edit customer"

				Dim bitmap As New BitmapImage(New Uri(CustomerViewModel.SelectedCustomerModel.LargeImageUrl))
				CustomerImage.Source = bitmap

				commandModel = CustomerViewModel.EditCommandModel
			End If

			textBoxCustomer.Focus()

			' The command helps determine whether save button is enabled or not
			buttonSave.Command = commandModel.Command
			buttonSave.CommandParameter = Me.DataContext
            buttonSave.CommandBindings.Add(New CommandBinding(commandModel.Command, _
                New Input.ExecutedRoutedEventHandler(AddressOf commandModel.OnExecute), _
                New Input.CanExecuteRoutedEventHandler(AddressOf commandModel.OnCanExecute)))
		End Sub

		' Save button was clicked
		Private Sub buttonSave_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
			Close()
		End Sub

		' Cancel button was clicked
		Private Sub buttonCancel_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
			' Restore viewmodel to original values
			If (Not IsNewCustomer) Then
				CustomerViewModel.SelectedCustomerModel.Company = _originalCompany
				CustomerViewModel.SelectedCustomerModel.City = _originalCity
				CustomerViewModel.SelectedCustomerModel.Country = _originalCountry
			End If
		End Sub
	End Class
End Namespace
