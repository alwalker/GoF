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
    /// Linq-to-Sql implementation of the IOrderDao interface.
    /// </summary>
    public class LinqOrderDao : IOrderDao
    {
        #region IOrderDao Members

        /// <summary>
        /// Provides orderstatistic to a given list of customers.
        /// </summary>
        /// <param name="customers">List of customers for which statistics are requested.</param>
        /// <returns>List of customers including their order statistics.</returns>
        public IList<Customer> GetOrderStatistics(IList<Customer> customers)
        {
            return GetOrderStatistics(customers, "");
        }

        /// <summary>
        /// Gets a list of customers with order summary statistics.
        /// </summary>
        /// <param name="customers"></param>
        /// <param name="sortExpression"></param>
        /// <returns></returns>
        public IList<Customer> GetOrderStatistics(IList<Customer> customers, string sortExpression)
        {
            // Place customerIds in an integer list
            IList<int> customerIds = new List<int>();
            foreach (Customer customer in customers)
                customerIds.Add(customer.CustomerId);

            using (ActionDataContext db = DataContextFactory.CreateContext())
            {
                var query = from o in db.OrderEntities
                            where customerIds.Contains<int>(o.CustomerId)
                            group o by o.CustomerId into g
                            select new
                            {
                                CustomerId = g.Key,
                                LastOrderDate = g.Max(d => d.OrderDate),
                                NumOrders = g.Count()
                            };

                IList<Customer> list = new List<Customer>();

                // Loop over customer list first to preserve sort order
                foreach (Customer customer in customers)
                {
                    foreach (var item in query)
                    {
                        if (item.CustomerId == customer.CustomerId)
                        {
                            customer.NumOrders = item.NumOrders;
                            customer.LastOrderDate = item.LastOrderDate;

                            list.Add(customer);
                            break;
                        }
                    }
                }

                // Here we perform in-memory postprocessing of sort order 
                if (sortExpression.Length > 0)
                {
                    string[] sort = sortExpression.Split(' ');
                    string sortColumn = sort[0];
                    string sortOrder = sort[1];

                    switch (sortColumn)
                    {
                        case "NumOrders":
                            if (sortOrder == "ASC")
                                list = list.OrderBy(c => c.NumOrders).ToList();
                            else
                                list = list.OrderByDescending(c => c.NumOrders).ToList();
                            break;
                        case "LastOrderDate":
                            if (sortOrder == "ASC")
                                list = list.OrderBy(c => c.LastOrderDate).ToList();
                            else
                                list = list.OrderByDescending(c => c.LastOrderDate).ToList();
                            break;
                    }
                }
                return list;
            }
        }

        /// <summary>
        /// Gets all orders for a given customer.
        /// </summary>
        /// <param name="customerId">Unique customer identifier.</param>
        /// <returns>List of orders.</returns>
        public IList<Order> GetOrders(int customerId)
        {
            using (ActionDataContext db = DataContextFactory.CreateContext())
            {
                return db.OrderEntities
                    .Where(o => o.CustomerId == customerId)
                    .Select(c => Mapper.ToBusinessObject(c)).ToList();
            }
        }

        /// <summary>
        /// Gets the orders between a given data range.
        /// </summary>
        /// <param name="dateFrom">Start date.</param>
        /// <param name="dateThru">End date.</param>
        /// <returns></returns>
        public IList<Order> GetOrdersByDate(DateTime dateFrom, DateTime dateThru)
        {
            using (ActionDataContext db = DataContextFactory.CreateContext())
            {
                return db.OrderEntities
                    .Where(o => o.OrderDate >= dateFrom && o.OrderDate <= dateThru)
                    .Select(c => Mapper.ToBusinessObject(c)).ToList();
            }
        }

        /// <summary>
        /// Gets the orderdetails for a given order.
        /// </summary>
        /// <param name="orderId">Unique order identifier.</param>
        /// <returns>List of orderdetails.</returns>
        public IList<OrderDetail> GetOrderDetails(int orderId)
        {
            using (ActionDataContext db = DataContextFactory.CreateContext())
            {
                return db.OrderDetailEntities
                             .Where(od => od.OrderId == orderId)
                             .Select(od => Mapper.ToBusinessObject(od))
                             .ToList();
            }
        }

        /// <summary>
        /// Gets order given an order identifier.
        /// </summary>
        /// <param name="orderId">Order identifier.</param>
        /// <returns>The order.</returns>
        public Order GetOrder(int orderId)
        {
            using (ActionDataContext db = DataContextFactory.CreateContext())
            {
                return Mapper.ToBusinessObject(db.OrderEntities
                            .SingleOrDefault(o => o.OrderId == orderId));
            }
        }

        #endregion
    }
}
