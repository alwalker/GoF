using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using BusinessObjects;

namespace DataObjects.AdoNet.SqlServer
{
    /// <summary>
    /// Sql Server specific data access object that handles data access
    /// of categories and products.
    /// </summary>
    public class SqlServerProductDao : IProductDao
    {
        /// <summary>
        /// Gets a list of categories.
        /// </summary>
        /// <returns>Category list.</returns>
        public IList<Category> GetCategories()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT CategoryId, CategoryName, Description ");
            sql.Append("   FROM Category");

            DataTable dt = Db.GetDataTable(sql.ToString());
            return MakeCategories(dt);

        }

        // Creates list of categories from datatable
        private IList<Category> MakeCategories(DataTable dt)
        {
            IList<Category> list = new List<Category>();
            foreach (DataRow row in dt.Rows)
                list.Add(MakeCategory(row));

            return list;
        }

        // Creates category business object from datarow
        private Category MakeCategory(DataRow row)
        {
            int categoryId = int.Parse(row["CategoryId"].ToString());
            string name = row["CategoryName"].ToString();
            string description = row["Description"].ToString();

            return new Category(categoryId, name, description);
        }

        /// <summary>
        /// Gets a list of products for a given category.
        /// </summary>
        /// <param name="categoryId">Unique category identifier.</param>
        /// <param name="sortExpression">Sort order.</param>
        /// <returns>Sorted list of products.</returns>
        public IList<Product> GetProductsByCategory(int categoryId, string sortExpression)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT ProductId, ProductName, Weight, UnitPrice, UnitsInStock ");
            sql.Append("   FROM Product INNER JOIN Category ON Product.CategoryId = Category.CategoryId ");
            sql.Append("  WHERE Category.CategoryId = " + categoryId);
            if (!string.IsNullOrEmpty(sortExpression))
                sql.Append(" ORDER BY " + sortExpression);

            DataTable dt = Db.GetDataTable(sql.ToString());
            return MakeProducts(dt);
        }

        /// <summary>
        /// Performs a search for products given several criteria.
        /// </summary>
        /// <param name="productName">Product name criterium.</param>
        /// <param name="priceFrom">Low end of price range.</param>
        /// <param name="priceThru">High end of price range.</param>
        /// <param name="sortExpression">Sort order.</param>
        /// <returns>Sorted list of products.</returns>
        public IList<Product> SearchProducts(string productName, double priceFrom, double priceThru, string sortExpression)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT ProductId, ProductName, Weight, UnitPrice, UnitsInStock ");
            sql.Append("   FROM Product ");

            bool where = false;
            if (!string.IsNullOrEmpty(productName))
            {
                sql.Append("  WHERE ProductName LIKE '" + productName + "%' ");
                where = true;
            }

            if (priceFrom != -1 && priceThru != -1)
            {
                if (where)
                    sql.Append("   AND UnitPrice >= " + priceFrom);
                else
                    sql.Append(" WHERE UnitPrice >= " + priceFrom);

                sql.Append(" AND UnitPrice <= " + priceThru);
            }

            if (!String.IsNullOrEmpty(sortExpression))
                sql.Append(" ORDER BY " + sortExpression);

            DataTable dt = Db.GetDataTable(sql.ToString());
            return MakeProducts(dt);
        }

        /// <summary>
        /// Gets a product list. A private helper method.
        /// </summary>
        /// <param name="sql">Sql statement.</param>
        /// <returns>List of products.</returns>
        private IList<Product> MakeProducts(DataTable dt)
        {
            IList<Product> list = new List<Product>();
            foreach (DataRow row in dt.Rows)
                list.Add(MakeProduct(row));

            return list;
        }

        // Creates product business object from data row
        private Product MakeProduct(DataRow row)
        {
            int productId = int.Parse(row["ProductId"].ToString());
            string name = row["ProductName"].ToString();
            string weight = row["Weight"].ToString();
            double unitPrice = double.Parse(row["UnitPrice"].ToString());
            int unitsInStock = int.Parse(row["UnitsInStock"].ToString());

            return new Product(productId, name, weight, unitPrice, unitsInStock);
        }

        /// <summary>
        /// Gets a product.
        /// </summary>
        /// <param name="id">Unique product identifier.</param>
        /// <returns>Product.</returns>
        public Product GetProduct(int productId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT ProductId, ProductName, Weight, UnitPrice, UnitsInStock, ");
            sql.Append("        Category.CategoryId, CategoryName, Description ");
            sql.Append("   FROM Product INNER JOIN Category ON Product.CategoryId = Category.CategoryId ");
            sql.Append("  WHERE Product.ProductId = " + productId);

            DataRow row = Db.GetDataRow(sql.ToString());
            if (row == null) return null;

            return MakeProduct(row);
        }
       

        /// <summary>
        /// Gets a category for a given product.
        /// </summary>
        /// <param name="productId">Unique product identifier.</param>
        /// <returns>Category.</returns>
        public Category GetCategoryByProduct(int productId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT Category.CategoryId, CategoryName, Description ");
            sql.Append("  FROM Category INNER JOIN Product ON Product.CategoryId = Category.CategoryId ");
            sql.Append(" WHERE Product.ProductId = " + productId);

            DataRow row = Db.GetDataRow(sql.ToString());
            if (row == null) return null;

            return MakeCategory(row);
        }
    }
}
