Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Imports DataObjects.Linq.EntityMapper
Imports BusinessObjects
Imports DataObjects.Linq

Namespace DataObjects.Linq.LinqImplementation
	''' <summary>
	''' Linq-to-Sql implementation of the IProductDao interface.
	''' </summary>
	Public Class LinqProductDao
		Implements IProductDao
		#Region "IProductDao Members"

		''' <summary>
		''' Gets list of product categories
		''' </summary>
		''' <returns>List of categories.</returns>
		Public Function GetCategories() As IList(Of Category) Implements IProductDao.GetCategories
			Using db As ActionDataContext = DataContextFactory.CreateContext()
				Return db.CategoryEntities.Select(Function(c) Mapper.ToBusinessObject(c)).ToList()
			End Using
		End Function

		''' <summary>
		''' Gets list of product categories for a given category
		''' </summary>
		''' <param name="categoryId">The category for which products are requested.</param>
		''' <param name="sortExpression">Sort order.</param>
		''' <returns>List of products.</returns>
		Public Function GetProductsByCategory(ByVal categoryId As Integer, ByVal sortExpression As String) As IList(Of Product) Implements IProductDao.GetProductsByCategory
			Dim sort() As String = sortExpression.Split(" "c)
			Dim sortColumn As String = sort(0)
			Dim sortOrder As String = sort(1)

			Using db As ActionDataContext = DataContextFactory.CreateContext()
				' build query tree
				Dim query = db.ProductEntities.Where(Function(p) p.CategoryId = categoryId)

				Select Case sortColumn
					Case "ProductId"
						If sortOrder = "ASC" Then
							query = query.OrderBy(Function(p) p.ProductId)
						Else
							query = query.OrderByDescending(Function(p) p.ProductId)
						End If
					Case "ProductName"
						If sortOrder = "ASC" Then
							query = query.OrderBy(Function(p) p.ProductName)
						Else
							query = query.OrderByDescending(Function(p) p.ProductName)
						End If
					Case "Weight"
						If sortOrder = "ASC" Then
							query = query.OrderBy(Function(p) p.Weight)
						Else
							query = query.OrderByDescending(Function(p) p.Weight)
						End If
					Case "UnitPrice"
						If sortOrder = "ASC" Then
							query = query.OrderBy(Function(p) p.UnitPrice)
						Else
							query = query.OrderByDescending(Function(p) p.UnitPrice)
						End If
				End Select

				Return query.Select(Function(p) Mapper.ToBusinessObject(p)).ToList()
			End Using
		End Function

		''' <summary>
		''' Searches for products given a set of criteria
		''' </summary>
		''' <param name="productName">Product Name criterium. Could be partial.</param>
		''' <param name="priceFrom">Minimumn price criterium.</param>
		''' <param name="priceThru">Maximumn price criterium.</param>
		''' <param name="sortExpression">Sort order in which to return product list.</param>
		''' <returns>List of found products.</returns>
		Public Function SearchProducts(ByVal productName As String, ByVal priceFrom As Double, ByVal priceThru As Double, ByVal sortExpression As String) As IList(Of Product) Implements IProductDao.SearchProducts
			Using db As ActionDataContext = DataContextFactory.CreateContext()
				Dim query As IQueryable(Of ProductEntity) = db.ProductEntities
				If (Not String.IsNullOrEmpty(productName)) Then
					query = query.Where(Function(p) p.ProductName.StartsWith(productName))
				End If

				If priceFrom <> -1 AndAlso priceThru <> -1 Then
					query = query.Where(Function(p) p.UnitPrice >= CDec(priceFrom) AndAlso p.UnitPrice <= CDec(priceThru))
				End If

				Dim sort() As String = sortExpression.Split(" "c)
				Dim sortColumn As String = sort(0)
				Dim sortOrder As String = sort(1)

				Select Case sortColumn
					Case "ProductId"
						If sortOrder = "ASC" Then
							query = query.OrderBy(Function(p) p.ProductId)
						Else
							query = query.OrderByDescending(Function(p) p.ProductId)
						End If
					Case "ProductName"
						If sortOrder = "ASC" Then
							query = query.OrderBy(Function(p) p.ProductName)
						Else
							query = query.OrderByDescending(Function(p) p.ProductName)
						End If
					Case "Weight"
						If sortOrder = "ASC" Then
							query = query.OrderBy(Function(p) p.Weight)
						Else
							query = query.OrderByDescending(Function(p) p.Weight)
						End If
					Case "UnitPrice"
						If sortOrder = "ASC" Then
							query = query.OrderBy(Function(p) p.UnitPrice)
						Else
							query = query.OrderByDescending(Function(p) p.UnitPrice)
						End If
				End Select

				Return query.Select(Function(p) Mapper.ToBusinessObject(p)).ToList()
			End Using
		End Function

		''' <summary>
		''' Gets a product given a product identifier.
		''' </summary>
		''' <param name="productId">Product identifier.</param>
		''' <returns>The product.</returns>
		Public Function GetProduct(ByVal productId As Integer) As Product Implements IProductDao.GetProduct
			Using db As ActionDataContext = DataContextFactory.CreateContext()
				Return Mapper.ToBusinessObject(db.ProductEntities.SingleOrDefault(Function(p) p.ProductId = productId))
			End Using
		End Function

		''' <summary>
		''' Gets category for a given a product.
		''' </summary>
		''' <param name="productId">The product identifier.</param>
		''' <returns>The category.</returns>
		Public Function GetCategoryByProduct(ByVal productId As Integer) As Category Implements IProductDao.GetCategoryByProduct
			Using db As ActionDataContext = DataContextFactory.CreateContext()
				Return Mapper.ToBusinessObject(db.CategoryEntities.SelectMany(Function(c) db.ProductEntities.Where(Function(p) c.CategoryId = p.CategoryId).Where(Function(p) p.ProductId = productId), Function(c, p) c).SingleOrDefault(Function(c) True))
			End Using
		End Function
		#End Region
	End Class
End Namespace
