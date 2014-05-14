using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DataObjects.Linq;
using BusinessObjects;

namespace DataObjects.Linq.EntityMapper
{
    /// <summary>
    /// Maps entities to business objects and vice versa.
    /// </summary>
    public static class Mapper
    {
        /// <summary>
        /// Maps customer entity to customer business object.
        /// </summary>
        /// <param name="c">A customer entity to be transformed.</param>
        /// <returns>A customer business object.</returns>
        internal static Customer ToBusinessObject(CustomerEntity c)
        {
            return new Customer
            {
                CustomerId = c.CustomerId,
                Company = c.CompanyName,
                City = c.City,
                Country = c.Country,
                Version = VersionConverter.ToString(c.Version)
            };
        }

        /// <summary>
        /// Maps customer business object to customer entity.
        /// </summary>
        /// <param name="customer">A customer business object.</param>
        /// <returns>A customer entity.</returns>
        internal static CustomerEntity ToEntity(Customer customer)
        {
            return new CustomerEntity
            {
                CustomerId = customer.CustomerId,
                CompanyName = customer.Company,
                City = customer.City,
                Country = customer.Country,
                Version = VersionConverter.ToBinary(customer.Version)
            };
        }

        /// <summary>
        /// Maps order entity to order business object.
        /// </summary>
        /// <param name="o">An order entity.</param>
        /// <returns>An order business object.</returns>
        internal static Order ToBusinessObject(OrderEntity o)
        {
            return new Order
            {
                OrderId = o.OrderId,
                Freight = o.Freight.HasValue ? (float)o.Freight : default(float),
                OrderDate = o.OrderDate,
                RequiredDate = o.RequiredDate.HasValue ? (DateTime)o.RequiredDate : default(DateTime),
                Version = VersionConverter.ToString(o.Version)
            };
        }

        /// <summary>
        /// Maps order detail entity to order detail business object.
        /// </summary>
        /// <param name="od">An order detail entity.</param>
        /// <returns>An order detail business object.</returns>
        internal static OrderDetail ToBusinessObject(OrderDetailEntity od)
        {
            return new OrderDetail
            {
                ProductName = od.ProductEntity.ProductName,
                Discount = (float)od.Discount,
                Quantity = od.Quantity,
                UnitPrice = (float)od.UnitPrice,
                Version = VersionConverter.ToString(od.Version)
            };
        }

        /// <summary>
        /// Maps product category entity to category business object.
        /// </summary>
        /// <param name="c">A category entity.</param>
        /// <returns>A category business object.</returns>
        internal static Category ToBusinessObject(CategoryEntity c)
        {
            return new Category
            {
                CategoryId = c.CategoryId,
                Description = c.Description,
                Name = c.CategoryName,
                Version = VersionConverter.ToString(c.Version)
            };
        }

        /// <summary>
        /// Maps product entity to product business object.
        /// </summary>
        /// <param name="p">A product entity.</param>
        /// <returns>A product business object.</returns>
        internal static Product ToBusinessObject(ProductEntity p)
        {
            return new Product
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                UnitPrice = (double)p.UnitPrice,
                UnitsInStock = p.UnitsInStock,
                Weight = p.Weight,
                Version = VersionConverter.ToString(p.Version)
            };
        }
    }
}
