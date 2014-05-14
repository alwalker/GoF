using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using BusinessObjects;

namespace DataObjects.AdoNet.Oracle
{
    /// <summary>
    /// Oracle specific data access object that handles data access
    /// of categories and products. The details are stubbed out (in a crude way) but should be 
    /// relatively easy to implement as they are similar to MS Access and 
    /// Sql Server Data access objects.
    ///
    /// Enterprise Design Pattern: Service Stub.
    /// </summary>
    public class OracleProductDao : IProductDao
    {
        /// <summary>
        /// Gets a list of categories. Stubbed.
        /// </summary>
        /// <returns>Category list.</returns>
        public IList<Category> GetCategories()
        {
            throw new NotImplementedException("Oracle data access not implemented.");
        }

        /// <summary>
        /// Gets a list of products for a given category. Stubbed.
        /// </summary>
        /// <param name="categoryId">Unique category identifier.</param>
        /// <param name="sortExpression">Sort order.</param>
        /// <returns>Sorted product list.</returns>
        public IList<Product> GetProductsByCategory(int categoryId, string sortExpression)
        {
            throw new NotImplementedException("Oracle data access not implemented.");
        }

        /// <summary>
        /// Performs a search for products given several criteria. Stubbed.
        /// </summary>
        /// <param name="productName">Product name criterium.</param>
        /// <param name="priceFrom">Low end of price range.</param>
        /// <param name="priceThru">High end of price range.</param>
        /// <param name="sortExpression">Sort order.</param>
        /// <returns>Sorted product list.</returns>
        public IList<Product> SearchProducts(string productName, double priceFrom, double priceThru, string sortExpression)
        {
            throw new NotImplementedException("Oracle data access not implemented.");
        }

        /// <summary>
        /// Gets a product list. A private helper method.
        /// </summary>
        /// <param name="sql">Sql statement</param>
        /// <returns>Product list.</returns>
        private IList<Product> GetProductList(string sql)
        {
            throw new NotImplementedException("Oracle data access not implemented.");
        }

        /// <summary>
        /// Gets a product. Stubbed.
        /// </summary>
        /// <param name="id">Unique product identifier.</param>
        /// <returns>Product.</returns>
        public Product GetProduct(int id)
        {
            throw new NotImplementedException("Oracle data access not implemented.");
        }

        /// <summary>
        /// Gets a category for a given product. Stubbed.
        /// </summary>
        /// <param name="productId">Unique product identifier.</param>
        /// <returns>Category.</returns>
        public Category GetCategoryByProduct(int productId)
        {
            throw new NotImplementedException("Oracle data access not implemented.");
        }
    }
}
