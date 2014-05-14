Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Linq

Imports BusinessObjects.BusinessRules

Namespace BusinessObjects
	''' <summary>
	''' Class that holds information about a customer.
	''' </summary>
	''' <remarks>
	''' Enterprise Design Pattern: Domain Model, Identity Field.
	''' 
	''' This is also the place where business rules are established.
	''' 
	''' The Domain Model Design Pattern states that domain objects incorporate 
	''' both behavior and data. Behavior may include simple or complex business logic.
	''' 
	''' The Identity Field Design Pattern saves the ID field in an object to maintain
	''' identity between an in-memory business object and that database rows.
	''' </remarks>
	Public Class Customer
		Inherits BusinessObject
		''' <summary>
		''' Default constructor for customer class.
		''' Initializes automatic properties.
		''' </summary>
		Public Sub New()
			' Default property values
			Orders = New List(Of Order)()

			' Business rules
			AddRule(New ValidateId("CustomerId"))

			AddRule(New ValidateRequired("Company"))
			AddRule(New ValidateLength("Company", 1, 40))

			AddRule(New ValidateRequired("City"))
			AddRule(New ValidateLength("City", 1, 15))

			AddRule(New ValidateRequired("Country"))
			AddRule(New ValidateLength("Country", 1, 15))

			Version = _versionDefault
		End Sub

		''' <summary>
		''' Overloaded constructor for the Customer class.
		''' </summary>
		''' <param name="customerId">Unique Identifier for the Customer.</param>
		''' <param name="company">Name of the Customer.</param>
		''' <param name="city">City where Customer is located.</param>
		''' <param name="country">Country where Customer is located.</param>
		Public Sub New(ByVal customerId As Integer, ByVal company As String, ByVal city As String, ByVal country As String)
			Me.New()
			Me.CustomerId = customerId
			Me.Company = company
			Me.City = city
			Me.Country = country
		End Sub

		''' <summary>
		''' Gets or sets unique customer identifier.
		''' The Identity Field Design Pattern. 
		''' </summary>
        Private _customerId As Integer
        Public Property CustomerId() As Integer
            Get
                Return _customerId
            End Get
            Set(ByVal value As Integer)
                _customerId = value
            End Set
        End Property

		''' <summary>
		''' Gets or sets the customer name.
		''' </summary>
        Private _company As String
        Public Property Company() As String
            Get
                Return _company
            End Get
            Set(ByVal value As String)
                _company = value
            End Set
        End Property

		''' <summary>
		''' Gets or sets the customer city.
		''' </summary>
        Private _city As String
        Public Property City() As String
            Get
                Return _city
            End Get
            Set(ByVal value As String)
                _city = value
            End Set
        End Property

		''' <summary>
		''' Gets or sets the customer country.
		''' </summary>
        Private _country As String
        Public Property Country() As String
            Get
                Return _country
            End Get
            Set(ByVal value As String)
                _country = value
            End Set
        End Property

		''' <summary>
		''' Gets or sets the total number of orders placed by the customer.
		''' </summary>
        Private _numOrders As Integer
		Public Property NumOrders() As Integer
			Get
                Return _numOrders
			End Get
			Set(ByVal value As Integer)
                _numOrders = value
			End Set
		End Property

		''' <summary>
		''' Gets or sets the last date the customer placed an order.
		''' </summary>
        Private _lastOrderDate As DateTime
		Public Property LastOrderDate() As DateTime
			Get
                Return _lastOrderDate
			End Get
			Set(ByVal value As DateTime)
                _lastOrderDate = value
			End Set
		End Property

		''' <summary>
		''' Gets or sets a list of all orders placed by the customer.
		''' </summary>
        Private _orders As IList(Of Order)
		Public Property Orders() As IList(Of Order)
			Get
                Return _orders
			End Get
            Set(ByVal value As IList(Of Order))
                _orders = value
            End Set
		End Property

		''' <summary>
		''' Gets or sets the version number. Used for optimistic concurrency.
		''' </summary>
        Private _version As String
        Public Property Version() As String
            Get
                Return _version
            End Get
            Set(ByVal value As String)
                _version = value
            End Set
        End Property
	End Class
End Namespace
