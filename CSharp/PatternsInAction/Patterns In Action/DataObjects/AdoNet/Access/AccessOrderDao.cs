using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using BusinessObjects;

namespace DataObjects.AdoNet.Access
{
    /// <summary>
    /// Microsoft Access specific data access object that handles data access
    /// of customer related orders and order details.
    /// </summary>
    public class AccessOrderDao : IOrderDao
    {
        /// <summary>
        /// Gets customers with order statistics in given sort order.
        /// </summary>
        /// <param name="customers">Customer list.</param>
        /// <returns>Sorted list of customers with order statistics.</returns>
        public IList<Customer> GetOrderStatistics(IList<Customer> customers)
        {
            string customerIds = CommaSeparateCustomerIds(customers);

            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT CustomerId, MAX(OrderDate) AS LastOrderDate, COUNT(OrderId) AS NumOrders ");
            sql.Append("   FROM [Order]");
            sql.Append("  WHERE CustomerId IN (" + customerIds + ")");
            sql.Append("  GROUP BY CustomerId ");

            DataTable dt = Db.GetDataTable(sql.ToString());

            // Loop over customers first to preserve sort order
            foreach (Customer customer in customers)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (int.Parse(row["CustomerId"].ToString()) == customer.CustomerId)
                    {
                        customer.NumOrders = int.Parse(row["NumOrders"].ToString());
                        customer.LastOrderDate = DateTime.Parse(row["LastOrderDate"].ToString());

                        break;
                    }
                }
            }

            return customers;
        }

        // Generates string of comma separated ids
        private string CommaSeparateCustomerIds(IList<Customer> customers)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Customer customer in customers)
            {
                sb.Append(customer.CustomerId);
                sb.Append(",");
            }
            return sb.Remove(sb.Length - 1, 1).ToString();
        }

        /// <summary>
        /// Gets a list of customers with order summary statistics.
        /// </summary>
        /// <param name="customers">Customer list.</param>
        /// <param name="sortExpression">Sort order.</param>
        /// <returns>Customer list with order summary statistics.</returns>
        public IList<Customer> GetOrderStatistics(IList<Customer> customers, string sortExpression)
        {
            string customerIds = CommaSeparateCustomerIds(customers);

            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT CustomerId, MAX(OrderDate) AS LastOrderDate, COUNT(OrderId) AS NumOrders ");
            sql.Append("   FROM [Order]");
            sql.Append("  WHERE CustomerId IN (" + customerIds + ")");
            sql.Append("  GROUP BY CustomerId ");

            if (!string.IsNullOrEmpty(sortExpression))
            {
                // MS Access does not ORDER BY column alias name
                // Change alias to aggregate function
                sortExpression = sortExpression.Replace("LastOrderDate", "MAX(OrderDate)");
                sortExpression = sortExpression.Replace("NumOrders", "COUNT(OrderId)");

                sql.Append("  ORDER BY " + sortExpression);
            }

            DataTable dt = Db.GetDataTable(sql.ToString());

            IList<Customer> list = new List<Customer>();

            // Loop over datatable rows first to preserve sort order.
            foreach (DataRow row in dt.Rows)
            {
                foreach (Customer customer in customers)
                {
                    if (int.Parse(row["CustomerId"].ToString()) == customer.CustomerId)
                    {
                        customer.NumOrders = int.Parse(row["NumOrders"].ToString());
                        customer.LastOrderDate = DateTime.Parse(row["LastOrderDate"].ToString());

                        list.Add(customer);
                        break;
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// Gets all orders for a customer.
        /// </summary>
        /// <param name="customerId">Unique customer identifier.</param>
        /// <returns>Order list.</returns>
        public IList<Order> GetOrders(int customerId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT OrderId, OrderDate, RequiredDate, Freight ");
            sql.Append("   FROM [Order]");
            sql.Append("  WHERE CustomerId = " + customerId);
            sql.Append("  ORDER BY OrderDate ASC ");

            DataTable dt = Db.GetDataTable(sql.ToString());
            return MakeOrders(dt);
        }

        /// <summary>
        /// Gets a list of orders placed within a date range.
        /// </summary>
        /// <param name="dateFrom">Date range begin date.</param>
        /// <param name="dateThru">Date range end date.</param>
        /// <returns>List of orders.</returns>
        public IList<Order> GetOrdersByDate(DateTime dateFrom, DateTime dateThru)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT OrderId, OrderDate, RequiredDate, Freight ");
            sql.Append("   FROM [Order]");
            sql.Append("  WHERE OrderDate >= '" + dateFrom + "' ");
            sql.Append("    AND OrderDate <= '" + dateThru + "' ");
            sql.Append("  ORDER BY OrderDate ASC ");

            DataTable dt = Db.GetDataTable(sql.ToString());
            return MakeOrders(dt);
        }

        /// <summary>
        /// Gets a list of order details for a given order.
        /// </summary>
        /// <param name="orderId">Unique order identifier.</param>
        /// <returns>List of order details.</returns>
        public IList<OrderDetail> GetOrderDetails(int orderId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT ProductName, O.UnitPrice, Quantity, Discount ");
            sql.Append("   FROM OrderDetail O INNER JOIN Product ON O.ProductId = Product.ProductId ");
            sql.Append("  WHERE O.OrderId = " + orderId);

            DataTable dt = Db.GetDataTable(sql.ToString());
            return MakeOrderDetails(dt);
        }

        private IList<OrderDetail> MakeOrderDetails(DataTable dt)
        {
            IList<OrderDetail> list = new List<OrderDetail>();
            foreach (DataRow row in dt.Rows)
                list.Add(MakeOrderDetail(row));

            return list;
        }

        private OrderDetail MakeOrderDetail(DataRow row)
        {
            string product = row["ProductName"].ToString();
            int quantity = int.Parse(row["Quantity"].ToString());
            float unitPrice = float.Parse(row["UnitPrice"].ToString());
            float discount = float.Parse(row["Discount"].ToString());
            Order order = null;

            return new OrderDetail(product, quantity, unitPrice, discount, order);
        }

        /// <summary>
        /// Gets a list of orders. Private helper method.
        /// </summary>
        /// <param name="sql">Sql statement.</param>
        /// <returns>Order list.</returns>
        private IList<Order> MakeOrders(DataTable dt)
        {
            IList<Order> list = new List<Order>();
            foreach (DataRow row in dt.Rows)
                list.Add(MakeOrder(row));

            return list;
        }

        private Order MakeOrder(DataRow row)
        {
            int orderId = int.Parse(row["OrderId"].ToString());
            DateTime orderDate = DateTime.Parse(row["OrderDate"].ToString());
            DateTime requiredDate = DateTime.Parse(row["RequiredDate"].ToString());
            float freight = float.Parse(row["Freight"].ToString());

            return new Order(orderId, orderDate, requiredDate, freight);
        }

        /// <summary>
        /// Gets a specific order.
        /// </summary>
        /// <param name="orderId">Unique order identifier.</param>
        /// <returns>Order.</returns>
        public Order GetOrder(int orderId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT OrderId, OrderDate, RequiredDate, Freight ");
            sql.Append("   FROM [Order] ");
            sql.Append("  WHERE OrderId = " + orderId);

            DataRow row = Db.GetDataRow(sql.ToString());
            if (row == null) return null;

            return MakeOrder(row);
        }
    }
}
