Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Threading
Imports System.Windows.Threading
Imports System.Configuration
Imports System.Collections.ObjectModel

Imports WPFModel.Provider

Namespace WPFModel.BusinessModelObjects
	''' <summary>
	''' Model of the customer. 
	''' </summary>
	Public Class CustomerModel
		Inherits BaseModel
		' Set ImageService url only once
		Private Shared ReadOnly ImageService As String = ConfigurationManager.AppSettings.Get("ImageService")
		Private _imageUrl As String = ImageService & "GetCustomerImageSmall/0"

		Private _provider As IProvider

		Private _customerId As Integer = 0
		Private _company As String
		Private _city As String
		Private _country As String
		Private _version As String

		Private _orders As ObservableCollection(Of OrderModel)

		''' <summary>
		''' Constructor.
		''' </summary>
		''' <param name="provider">The provider for the customer.</param>
		Public Sub New(ByVal provider As IProvider)
			_provider = provider
		End Sub
		''' <summary>
		''' Adds a new customer.
		''' </summary>
		Public Function Add() As Integer
			Dim rowsAffected As Integer = _provider.AddCustomer(Me)
			Return rowsAffected
		End Function

		''' <summary>
		''' Deletes a customer.
		''' </summary>
		Public Function Delete() As Integer
			Dim rowsAffected As Integer = _provider.DeleteCustomer(Me.CustomerId)
			Return rowsAffected
		End Function

		''' <summary>
		''' Updates a customer.
		''' </summary>
		Public Function Update() As Integer
			Dim rowsAffected As Integer = _provider.UpdateCustomer(Me)
			Return rowsAffected
		End Function

		''' <summary>
		''' Gets or sets customerId
		''' </summary>
		Public Property CustomerId() As Integer
			Get
				ConfirmOnUIThread()
				Return _customerId
			End Get
			Set(ByVal value As Integer)
				ConfirmOnUIThread()
				If _customerId <> value Then
					_customerId = value
					Notify("CustomerId")
				End If
			End Set
		End Property

		''' <summary>
		''' Gets or sets customer name.
		''' </summary>
		Public Property Company() As String
			Get
				ConfirmOnUIThread()
				Return _company
			End Get
			Set(ByVal value As String)
				ConfirmOnUIThread()
				If _company <> value Then
					_company = value
					Notify("Company")
				End If
			End Set
		End Property

		''' <summary>
		''' Gets or sets customer city.
		''' </summary>
		Public Property City() As String
			Get
				ConfirmOnUIThread()
				Return _city
			End Get
			Set(ByVal value As String)
				ConfirmOnUIThread()
				If _city <> value Then
					_city = value
					Notify("City")
				End If
			End Set
		End Property

		''' <summary>
		''' Gets or set customer country.
		''' </summary>
		Public Property Country() As String
			Get
				ConfirmOnUIThread()
				Return _country
			End Get
			Set(ByVal value As String)
				ConfirmOnUIThread()
				If _country <> value Then
					_country = value
					Notify("Country")
				End If
			End Set
		End Property

		''' <summary>
		''' Gets or sets list of orders associated with customer.
		''' </summary>
		Public Property Orders() As ObservableCollection(Of OrderModel)
			Get
				ConfirmOnUIThread()
				LazyloadOrders()
				Return _orders
			End Get
			Set(ByVal value As ObservableCollection(Of OrderModel))
				ConfirmOnUIThread()
				_orders = value
				Notify("Orders")
			End Set
		End Property

		' Helper
		Private Sub LazyloadOrders()
			If _orders Is Nothing OrElse _orders.Count = 0 Then
				Orders = _provider.GetOrders(Me.CustomerId)
			End If
		End Sub

		''' <summary>
		''' Gets url for small images.
		''' </summary>
		Public ReadOnly Property SmallImageUrl() As String
			Get
				ConfirmOnUIThread()
				Return ImageService & "GetCustomerImageSmall/" & _customerId
			End Get
		End Property

		''' <summary>
		''' Gets url for large images.
		''' </summary>
		Public ReadOnly Property LargeImageUrl() As String
			Get
				ConfirmOnUIThread()
				Return ImageService & "GetCustomerImageLarge/" & _customerId
			End Get
		End Property

		''' <summary>
		''' Gets or sets version number
		''' </summary>
		Public Property Version() As String
			Get
				ConfirmOnUIThread()
				Return _version
			End Get
			Set(ByVal value As String)
				ConfirmOnUIThread()
				If _version <> value Then
					_version = value
					Notify("Version")
				End If
			End Set
		End Property
	End Class
End Namespace
