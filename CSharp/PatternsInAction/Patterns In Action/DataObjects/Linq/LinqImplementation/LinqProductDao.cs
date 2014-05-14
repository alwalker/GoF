using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DataObjects.Linq.EntityMapper;
using BusinessObjects;
using DataObjects.Linq;

namespace DataObjects.Linq.LinqImplementation
{
    /// <summary>
    /// Linq-to-Sql implementation of the IProductDao interface.
    /// </summary>
    public class LinqProductDao : IProductDao
    {
        #region IProductDao Members

        /// <summary>
        /// Gets list of product categories
        /// </summary>
        /// <returns>List of categories.</returns>
        public IList<Category> GetCategories()
        {
            using (ActionDataContext db = DataContextFactory.CreateContext())
            {
                return db.CategoryEntities.Select(c => Mapper.ToBusinessObject(c)).ToList();
            }
        }

        /// <summary>
        /// Gets list of product categories for a given category
        /// </summary>
        /// <param name="categoryId">The category for which products are requested.</param>
        /// <param name="sortExpression">Sort order.</param>
        /// <returns>List of products.</returns>
        public IList<Product> GetProductsByCategory(int categoryId, string sortExpression)
        {
            string[] sort = sortExpression.Split(' ');
            string sortColumn = sort[0];
            string sortOrder = sort[1];

            using (ActionDataContext db = DataContextFactory.CreateContext())
            {
                // build query tree
                var query = db.ProductEntities.Where(p => p.CategoryId == categoryId);

                switch (sortColumn)
                {
                    case "ProductId":
                        if (sortOrder == "ASC")
                            query = query.OrderBy(p => p.ProductId);
                        else
                            query = query.OrderByDescending(p => p.ProductId);
                        break;
                    case "ProductName":
                        if (sortOrder == "ASC")
                            query = query.OrderBy(p => p.ProductName);
                        else
                            query = query.OrderByDescending(p => p.ProductName);
                        break;
                    case "Weight":
                        if (sortOrder == "ASC")
                            query = query.OrderBy(p => p.Weight);
                        else
                            query = query.OrderByDescending(p => p.Weight); 
                        break;
                    case "UnitPrice":
                        if (sortOrder == "ASC")
                            query = query.OrderBy(p => p.UnitPrice);
                        else
                            query = query.OrderByDescending(p => p.UnitPrice); 
                        break;
                }

                return query.Select(p => Mapper.ToBusinessObject(p)).ToList();
            }
        }

        /// <summary>
        /// Searches for products given a set of criteria
        /// </summary>
        /// <param name="productName">Product Name criterium. Could be partial.</param>
        /// <param name="priceFrom">Minimumn price criterium.</param>
        /// <param name="priceThru">Maximumn price criterium.</param>
        /// <param name="sortExpression">Sort order in which to return product list.</param>
        /// <returns>List of found products.</returns>
        public IList<Product> SearchProducts(string productName, double priceFrom, double priceThru, string sortExpression)
        {
            using (ActionDataContext db = DataContextFactory.CreateContext())
            {
                IQueryable<ProductEntity> query = db.ProductEntities;
                if (!string.IsNullOrEmpty(productName))
                    query = query.Where(p => p.ProductName.StartsWith(productName));

                if (priceFrom != -1 && priceThru != -1)
                {
                    query = query.Where(p => p.UnitPrice >= (decimal)priceFrom && p.UnitPrice <= (decimal)priceThru);
                }

                string[] sort = sortExpression.Split(' ');
                string sortColumn = sort[0];
                string sortOrder = sort[1];

                switch (sortColumn)
                {
                    case "ProductId":
                        if (sortOrder == "ASC")
                            query = query.OrderBy(p => p.ProductId);
                        else
                            query = query.OrderByDescending(p => p.ProductId);
                        break;
                    case "ProductName":
                        if (sortOrder == "ASC")
                            query = query.OrderBy(p => p.ProductName);
                        else
                            query = query.OrderByDescending(p => p.ProductName);
                        break;
                    case "Weight":
                        if (sortOrder == "ASC")
                            query = query.OrderBy(p => p.Weight);
                        else
                            query = query.OrderByDescending(p => p.Weight);
                        break;
                    case "UnitPrice":
                        if (sortOrder == "ASC")
                            query = query.OrderBy(p => p.UnitPrice);
                        else
                            query = query.OrderByDescending(p => p.UnitPrice);
                        break;
                }

                return query.Select(p => Mapper.ToBusinessObject(p)).ToList();
            }
        }

        /// <summary>
        /// Gets a product given a product identifier.
        /// </summary>
        /// <param name="productId">Product identifier.</param>
        /// <returns>The product.</returns>
        public Product GetProduct(int productId)
        {
            using (ActionDataContext db = DataContextFactory.CreateContext())
            {
                return Mapper.ToBusinessObject(db.ProductEntities
                            .SingleOrDefault(p => p.ProductId == productId));
            }
        }

        /// <summary>
        /// Gets category for a given a product.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns>The category.</returns>
        public Category GetCategoryByProduct(int productId)
        {
            using (ActionDataContext db = DataContextFactory.CreateContext())
            {
                return Mapper.ToBusinessObject(db.CategoryEntities.SelectMany(c => db.ProductEntities
                    .Where(p => c.CategoryId == p.CategoryId)
                    .Where(p => p.ProductId == productId),
                     (c, p) => c).SingleOrDefault(c => true)); 
            }
        }
        #endregion
    }
}
