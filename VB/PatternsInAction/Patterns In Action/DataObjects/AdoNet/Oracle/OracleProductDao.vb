Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Imports BusinessObjects

Namespace DataObjects.AdoNet.Oracle
	''' <summary>
	''' Oracle specific data access object that handles data access
	''' of categories and products. The details are stubbed out (in a crude way) but should be 
	''' relatively easy to implement as they are similar to MS Access and 
	''' Sql Server Data access objects.
	'''
	''' Enterprise Design Pattern: Service Stub.
	''' </summary>
	Public Class OracleProductDao
		Implements IProductDao
		''' <summary>
		''' Gets a list of categories. Stubbed.
		''' </summary>
		''' <returns>Category list.</returns>
		Public Function GetCategories() As IList(Of Category) Implements IProductDao.GetCategories
			Throw New NotImplementedException("Oracle data access not implemented.")
		End Function

		''' <summary>
		''' Gets a list of products for a given category. Stubbed.
		''' </summary>
		''' <param name="categoryId">Unique category identifier.</param>
		''' <param name="sortExpression">Sort order.</param>
		''' <returns>Sorted product list.</returns>
		Public Function GetProductsByCategory(ByVal categoryId As Integer, ByVal sortExpression As String) As IList(Of Product) Implements IProductDao.GetProductsByCategory
			Throw New NotImplementedException("Oracle data access not implemented.")
		End Function

		''' <summary>
		''' Performs a search for products given several criteria. Stubbed.
		''' </summary>
		''' <param name="productName">Product name criterium.</param>
		''' <param name="priceFrom">Low end of price range.</param>
		''' <param name="priceThru">High end of price range.</param>
		''' <param name="sortExpression">Sort order.</param>
		''' <returns>Sorted product list.</returns>
		Public Function SearchProducts(ByVal productName As String, ByVal priceFrom As Double, ByVal priceThru As Double, ByVal sortExpression As String) As IList(Of Product) Implements IProductDao.SearchProducts
			Throw New NotImplementedException("Oracle data access not implemented.")
		End Function

		''' <summary>
		''' Gets a product list. A private helper method.
		''' </summary>
		''' <param name="sql">Sql statement</param>
		''' <returns>Product list.</returns>
		Private Function GetProductList(ByVal sql As String) As IList(Of Product)
			Throw New NotImplementedException("Oracle data access not implemented.")
		End Function

		''' <summary>
		''' Gets a product. Stubbed.
		''' </summary>
		''' <param name="id">Unique product identifier.</param>
		''' <returns>Product.</returns>
		Public Function GetProduct(ByVal id As Integer) As Product Implements IProductDao.GetProduct
			Throw New NotImplementedException("Oracle data access not implemented.")
		End Function

		''' <summary>
		''' Gets a category for a given product. Stubbed.
		''' </summary>
		''' <param name="productId">Unique product identifier.</param>
		''' <returns>Category.</returns>
		Public Function GetCategoryByProduct(ByVal productId As Integer) As Category Implements IProductDao.GetCategoryByProduct
			Throw New NotImplementedException("Oracle data access not implemented.")
		End Function
	End Class
End Namespace
