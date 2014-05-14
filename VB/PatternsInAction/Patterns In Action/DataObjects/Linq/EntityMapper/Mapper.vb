Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Imports DataObjects.Linq
Imports BusinessObjects

Namespace DataObjects.Linq.EntityMapper
    ''' <summary>
    ''' Maps entities to business objects and vice versa.
    ''' </summary>
    Public NotInheritable Class Mapper
        ''' <summary>
        ''' Maps customer entity to customer business object.
        ''' </summary>
        ''' <param name="c">A customer entity to be transformed.</param>
        ''' <returns>A customer business object.</returns>
        Private Sub New()
        End Sub
        Friend Shared Function ToBusinessObject(ByVal c As CustomerEntity) As Customer
            Return New Customer With {.CustomerId = c.CustomerId, .Company = c.CompanyName, .City = c.City, .Country = c.Country, .Version = VersionConverter.ToString(c.Version)}
        End Function

        ''' <summary>
        ''' Maps customer business object to customer entity.
        ''' </summary>
        ''' <param name="customer">A customer business object.</param>
        ''' <returns>A customer entity.</returns>
        Friend Shared Function ToEntity(ByVal customer As Customer) As CustomerEntity
            Return New CustomerEntity With {.CustomerId = customer.CustomerId, .CompanyName = customer.Company, .City = customer.City, .Country = customer.Country, .Version = VersionConverter.ToBinary(customer.Version)}
        End Function

        ''' <summary>
        ''' Maps order entity to order business object.
        ''' </summary>
        ''' <param name="o">An order entity.</param>
        ''' <returns>An order business object.</returns>
        Friend Shared Function ToBusinessObject(ByVal o As OrderEntity) As Order
            Return New Order With {.OrderId = o.OrderId, .Freight = CSng(IIf(o.Freight.HasValue, o.Freight, Nothing)), .OrderDate = o.OrderDate, .RequiredDate = CDate(IIf(o.RequiredDate.HasValue, o.RequiredDate, Nothing)), .Version = VersionConverter.ToString(o.Version)}
        End Function

        ''' <summary>
        ''' Maps order detail entity to order detail business object.
        ''' </summary>
        ''' <param name="od">An order detail entity.</param>
        ''' <returns>An order detail business object.</returns>
        Friend Shared Function ToBusinessObject(ByVal od As OrderDetailEntity) As OrderDetail
            Return New OrderDetail With {.ProductName = od.ProductEntity.ProductName, .Discount = CSng(od.Discount), .Quantity = od.Quantity, .UnitPrice = CSng(od.UnitPrice), .Version = VersionConverter.ToString(od.Version)}
        End Function

        ''' <summary>
        ''' Maps product category entity to category business object.
        ''' </summary>
        ''' <param name="c">A category entity.</param>
        ''' <returns>A category business object.</returns>
        Friend Shared Function ToBusinessObject(ByVal c As CategoryEntity) As Category
            Return New Category With {.CategoryId = c.CategoryId, .Description = c.Description, .Name = c.CategoryName, .Version = VersionConverter.ToString(c.Version)}
        End Function

        ''' <summary>
        ''' Maps product entity to product business object.
        ''' </summary>
        ''' <param name="p">A product entity.</param>
        ''' <returns>A product business object.</returns>
        Friend Shared Function ToBusinessObject(ByVal p As ProductEntity) As Product
            Return New Product With {.ProductId = p.ProductId, .ProductName = p.ProductName, .UnitPrice = CDbl(p.UnitPrice), .UnitsInStock = p.UnitsInStock, .Weight = p.Weight, .Version = VersionConverter.ToString(p.Version)}
        End Function
    End Class
End Namespace
