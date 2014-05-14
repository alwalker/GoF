Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text

Namespace WindowsFormsModel.BusinessObjects
	''' <summary>
	''' Order business object as seen by the Service client.
	''' </summary>
	Public Class OrderModel
		''' <summary>
		''' Gets or sets order identifier. 
		''' </summary>
        Private _orderId As Integer
        Public Property OrderId() As Integer
            Get
                Return _orderId
            End Get
            Set(ByVal value As Integer)
                _orderId = value
            End Set
        End Property

		''' <summary>
		''' Gets or sets order date.
		''' </summary>
        Private _orderDate As DateTime
        Public Property OrderDate() As DateTime
            Get
                Return _orderDate
            End Get
            Set(ByVal value As DateTime)
                _orderDate = value
            End Set
        End Property

		''' <summary>
		''' Gets or set required order delivery date.
		''' </summary>
        Private _requiredDate As DateTime
        Public Property RequiredDate() As DateTime
            Get
                Return _requiredDate
            End Get
            Set(ByVal value As DateTime)
                _requiredDate = value
            End Set
        End Property

		''' <summary>
		''' Gets or sets freight (shipping) costs.
		''' </summary>
        Private _freight As Single
        Public Property Freight() As Single
            Get
                Return _freight
            End Get
            Set(ByVal value As Single)
                _freight = value
            End Set
        End Property

		''' <summary>
		''' Gets or sets list of order details (line items) for order.
		''' </summary>
        Private _orderDetails As IList(Of OrderDetailModel)
        Public Property OrderDetails() As IList(Of OrderDetailModel)
            Get
                Return _orderDetails
            End Get
            Set(ByVal value As IList(Of OrderDetailModel))
                _orderDetails = value
            End Set
        End Property

		''' <summary>
		''' Gets or sets customer for which order is placed.
		''' </summary>
        Private _customer As CustomerModel
        Public Property Customer() As CustomerModel
            Get
                Return _customer
            End Get
            Set(ByVal value As CustomerModel)
                _customer = value
            End Set
        End Property

		''' <summary>
		''' Version number for optimistic concurrency. Not used at the client.
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
