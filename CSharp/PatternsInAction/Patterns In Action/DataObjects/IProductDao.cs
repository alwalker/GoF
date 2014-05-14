using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BusinessObjects;

namespace DataObjects
{
    /// <summary>
    /// Defines methods to access categories and products.
    /// This is a database-independent interface. The implementations will 
    /// be database specific.
    /// </summary>
    public interface IProductDao
    {
        /// <summary>
        /// Gets a list of product categories.
        /// </summary>
        /// <returns>List of product categories.</returns>
        IList<Category> GetCategories();

        /// <summary>
        /// Gets a list of products for a given category.
        /// </summary>
        /// <param name="categoryId">Unique category identifier.</param>
        /// <param name="sortExpression">Sort order.</param>
        /// <returns>Sorted list of products.</returns>
        IList<Product> GetProductsByCategory(int categoryId, string sortExpression );

        /// <summary>
        /// Performs a search for products given several criteria.
        /// </summary>
        /// <param name="productName">Product name criterium.</param>
        /// <param name="priceFrom">Low end of price range.</param>
        /// <param name="priceThru">High end of price range.</param>
        /// <param name="sortExpression">Sort order.</param>
        /// <returns>Sorted list of products.</returns>
        IList<Product> SearchProducts(string productName, double priceFrom, double priceThru, string sortExpression);

        /// <summary>
        /// Gets a specific product.
        /// </summary>
        /// <param name="productId">Unique product identifier.</param>
        /// <returns>Product.</returns>
        Product GetProduct(int productId);

        /// <summary>
        /// Gets a product category for a given product.
        /// </summary>
        /// <param name="productId">Unique product identifier.</param>
        /// <returns>Product category.</returns>
        Category GetCategoryByProduct(int productId);
    }
}
