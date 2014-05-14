Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Xml.Serialization
Imports System.Linq
Imports System.Web
Imports System.Xml.Linq

Imports System.Runtime.Serialization

Namespace ActionService.DataTransferObjects
	''' <summary>
	''' Customer Data Transfer Object.
	''' 
	''' The purpose of the CustomerTransferObject is to facilitate transport of 
	''' customer business data in a serializable format. Business data is kept in 
	''' publicly accessible auto properties. This class has no methods. 
	''' </summary>
	''' <remarks>
	''' Pattern: Data Transfer Objects.
	''' 
	''' Data Transfer Objects are objects that transfer data between processes, but without behavior.
	''' </remarks>
	<DataContract(Name := "Customer", Namespace := "http://www.yourcompany.com/types/")> _
	Public Class CustomerDto
		''' <summary>
		''' Unique customer identifier.
		''' The Identity Field Design Pattern. 
		''' </summary>
        Private _customerId As Integer
        <DataMember()> Public Property CustomerId() As Integer
            Get
                Return _customerId
            End Get
            Set(ByVal value As Integer)
                _customerId = value
            End Set
        End Property

		''' <summary>
		''' Customer or company name.
		''' </summary>
        Private _company As String
        <DataMember()> Public Property Company() As String
            Get
                Return _company
            End Get
            Set(ByVal value As String)
                _company = value
            End Set
        End Property

		''' <summary>
		''' Customer city.
		''' </summary>
        Private _city As String
        <DataMember()> Public Property City() As String
            Get
                Return _city
            End Get
            Set(ByVal value As String)
                _city = value
            End Set
        End Property

		''' <summary>
		''' Customer country.
		''' </summary>
        Private _country As String
        <DataMember()> Public Property Country() As String
            Get
                Return _country
            End Get
            Set(ByVal value As String)
                _country = value
            End Set
        End Property

		''' <summary>
		''' Total number of orders placed by customer.
		''' </summary>
        Private _numOrders As Integer
        <DataMember()> Public Property NumOrders() As Integer
            Get
                Return _numOrders
            End Get
            Set(ByVal value As Integer)
                _numOrders = value
            End Set
        End Property

		''' <summary>
		''' Last order date for customer.
		''' </summary>
        Private _lastOrderDate As DateTime
        <DataMember()> Public Property LastOrderDate() As DateTime
            Get
                Return _lastOrderDate
            End Get
            Set(ByVal value As DateTime)
                _lastOrderDate = value
            End Set
        End Property

		''' <summary>
		''' List of orders placed by customer.
		''' </summary>
        Private _orders As OrderDto()
        <DataMember()> Public Property Orders() As OrderDto()
            Get
                Return _orders
            End Get
            Set(ByVal value As OrderDto())
                _orders = value
            End Set
        End Property

		''' <summary>
		''' Version number. Used in optimistic concurrency.
		''' </summary>
        Private _version As String
        <DataMember()> Public Property Version() As String
            Get
                Return _version
            End Get
            Set(ByVal value As String)
                _version = value
            End Set
        End Property
	End Class
End Namespace