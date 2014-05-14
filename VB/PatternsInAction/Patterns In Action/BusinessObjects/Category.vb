Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Linq

Imports BusinessObjects.BusinessRules

Namespace BusinessObjects
	''' <summary>
	''' Class that holds information about a product category.
	''' </summary>
	''' <remarks>
	''' Enterprise Design Pattern: Domain Model, Identity Field.
	''' 
	''' This is where your business logic resides. In this example there are none.
	''' Another place for business logic and business rules is in the Facade.  
	''' For an example see CustomerFacade in the Facade layer.
	''' 
	''' The Domain Model Design Pattern states that domain objects incorporate 
	''' both behavior and data. Behavior may include simple or complex business logic.
	''' 
	''' The Identity Field Design Pattern saves the ID field in an object to maintain
	''' identity between an in-memory business object and that database rows.
	''' </remarks>
	Public Class Category
		Inherits BusinessObject
		''' <summary>
		''' Default constructor. Establishes simple business rules.
		''' </summary>
		Public Sub New()
			AddRule(New ValidateId("CategoryId"))
			AddRule(New ValidateRequired("Name"))
			AddRule(New ValidateLength("Name", 0, 20))

			Version = _versionDefault
		End Sub

		''' <summary>
		''' Overloaded constructor for Category class.
		''' </summary>
		''' <param name="categoryId">Unique Identifier for the Category.</param>
		''' <param name="name">Name of the Category.</param>
		''' <param name="description">Description of the Category.</param>
		Public Sub New(ByVal categoryId As Integer, ByVal name As String, ByVal description As String)
			Me.New()
			Me.CategoryId = categoryId
			Me.Name = name
			Me.Description = description
		End Sub

		''' <summary>
		''' Gets or sets unique category identifier.
		''' The Identity Field Design Pattern. 
		''' </summary>
        Private _categoryId As Integer
        Public Property CategoryId() As Integer
            Get
                Return _categoryId
            End Get
            Set(ByVal value As Integer)
                _categoryId = value
            End Set
        End Property

		''' <summary>
		''' Gets or sets the category name.
		''' </summary>
        Private _name As String
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

		''' <summary>
		''' Gets or sets the category description.
		''' </summary>
        Private _description As String
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property

		''' <summary>
		''' Version number. Used in optimistic concurrency decisions.
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
