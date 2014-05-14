Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Imports BusinessObjects

Namespace DataObjects.AdoNet.SqlServer
	''' <summary>
	''' Sql Server specific data access object that handles data access
	''' of categories and products.
	''' </summary>
	Public Class SqlServerProductDao
		Implements IProductDao
		''' <summary>
		''' Gets a list of categories.
		''' </summary>
		''' <returns>Category list.</returns>
		Public Function GetCategories() As IList(Of Category) Implements IProductDao.GetCategories
			Dim sql As New StringBuilder()
			sql.Append(" SELECT CategoryId, CategoryName, Description ")
			sql.Append("   FROM Category")

			Dim dt As DataTable = Db.GetDataTable(sql.ToString())
			Return MakeCategories(dt)

		End Function

		' Creates list of categories from datatable
		Private Function MakeCategories(ByVal dt As DataTable) As IList(Of Category)
			Dim list As IList(Of Category) = New List(Of Category)()
			For Each row As DataRow In dt.Rows
				list.Add(MakeCategory(row))
			Next row

			Return list
		End Function

		' Creates category business object from datarow
		Private Function MakeCategory(ByVal row As DataRow) As Category
			Dim categoryId As Integer = Integer.Parse(row("CategoryId").ToString())
			Dim name As String = row("CategoryName").ToString()
			Dim description As String = row("Description").ToString()

			Return New Category(categoryId, name, description)
		End Function

		''' <summary>
		''' Gets a list of products for a given category.
		''' </summary>
		''' <param name="categoryId">Unique category identifier.</param>
		''' <param name="sortExpression">Sort order.</param>
		''' <returns>Sorted list of products.</returns>
		Public Function GetProductsByCategory(ByVal categoryId As Integer, ByVal sortExpression As String) As IList(Of Product) Implements IProductDao.GetProductsByCategory
			Dim sql As New StringBuilder()
			sql.Append(" SELECT ProductId, ProductName, Weight, UnitPrice, UnitsInStock ")
			sql.Append("   FROM Product INNER JOIN Category ON Product.CategoryId = Category.CategoryId ")
			sql.Append("  WHERE Category.CategoryId = " & categoryId)
			If (Not String.IsNullOrEmpty(sortExpression)) Then
				sql.Append(" ORDER BY " & sortExpression)
			End If

			Dim dt As DataTable = Db.GetDataTable(sql.ToString())
			Return MakeProducts(dt)
		End Function

		''' <summary>
		''' Performs a search for products given several criteria.
		''' </summary>
		''' <param name="productName">Product name criterium.</param>
		''' <param name="priceFrom">Low end of price range.</param>
		''' <param name="priceThru">High end of price range.</param>
		''' <param name="sortExpression">Sort order.</param>
		''' <returns>Sorted list of products.</returns>
		Public Function SearchProducts(ByVal productName As String, ByVal priceFrom As Double, ByVal priceThru As Double, ByVal sortExpression As String) As IList(Of Product) Implements IProductDao.SearchProducts
			Dim sql As New StringBuilder()
			sql.Append(" SELECT ProductId, ProductName, Weight, UnitPrice, UnitsInStock ")
			sql.Append("   FROM Product ")

			Dim where As Boolean = False
			If (Not String.IsNullOrEmpty(productName)) Then
				sql.Append("  WHERE ProductName LIKE '" & productName & "%' ")
				where = True
			End If

			If priceFrom <> -1 AndAlso priceThru <> -1 Then
				If where Then
					sql.Append("   AND UnitPrice >= " & priceFrom)
				Else
					sql.Append(" WHERE UnitPrice >= " & priceFrom)
				End If

				sql.Append(" AND UnitPrice <= " & priceThru)
			End If

			If (Not String.IsNullOrEmpty(sortExpression)) Then
				sql.Append(" ORDER BY " & sortExpression)
			End If

			Dim dt As DataTable = Db.GetDataTable(sql.ToString())
			Return MakeProducts(dt)
		End Function

		''' <summary>
		''' Gets a product list. A private helper method.
		''' </summary>
		''' <param name="sql">Sql statement.</param>
		''' <returns>List of products.</returns>
		Private Function MakeProducts(ByVal dt As DataTable) As IList(Of Product)
			Dim list As IList(Of Product) = New List(Of Product)()
			For Each row As DataRow In dt.Rows
				list.Add(MakeProduct(row))
			Next row

			Return list
		End Function

		' Creates product business object from data row
		Private Function MakeProduct(ByVal row As DataRow) As Product
			Dim productId As Integer = Integer.Parse(row("ProductId").ToString())
			Dim name As String = row("ProductName").ToString()
			Dim weight As String = row("Weight").ToString()
			Dim unitPrice As Double = Double.Parse(row("UnitPrice").ToString())
			Dim unitsInStock As Integer = Integer.Parse(row("UnitsInStock").ToString())

			Return New Product(productId, name, weight, unitPrice, unitsInStock)
		End Function

		''' <summary>
		''' Gets a product.
		''' </summary>
		''' <param name="id">Unique product identifier.</param>
		''' <returns>Product.</returns>
		Public Function GetProduct(ByVal productId As Integer) As Product Implements IProductDao.GetProduct
			Dim sql As New StringBuilder()
			sql.Append(" SELECT ProductId, ProductName, Weight, UnitPrice, UnitsInStock, ")
			sql.Append("        Category.CategoryId, CategoryName, Description ")
			sql.Append("   FROM Product INNER JOIN Category ON Product.CategoryId = Category.CategoryId ")
			sql.Append("  WHERE Product.ProductId = " & productId)

			Dim row As DataRow = Db.GetDataRow(sql.ToString())
			If row Is Nothing Then
				Return Nothing
			End If

			Return MakeProduct(row)
		End Function


		''' <summary>
		''' Gets a category for a given product.
		''' </summary>
		''' <param name="productId">Unique product identifier.</param>
		''' <returns>Category.</returns>
		Public Function GetCategoryByProduct(ByVal productId As Integer) As Category Implements IProductDao.GetCategoryByProduct
			Dim sql As New StringBuilder()
			sql.Append("SELECT Category.CategoryId, CategoryName, Description ")
			sql.Append("  FROM Category INNER JOIN Product ON Product.CategoryId = Category.CategoryId ")
			sql.Append(" WHERE Product.ProductId = " & productId)

			Dim row As DataRow = Db.GetDataRow(sql.ToString())
			If row Is Nothing Then
				Return Nothing
			End If

			Return MakeCategory(row)
		End Function
	End Class
End Namespace
