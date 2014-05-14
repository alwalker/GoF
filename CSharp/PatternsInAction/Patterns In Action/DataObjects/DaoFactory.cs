using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataObjects
{
    /// <summary>
    /// Abstract factory class that creates data access objects.
    /// </summary>
    /// <remarks>
    /// GoF Design Pattern: Factory.
    /// </remarks>
    public abstract class DaoFactory
    {
        /// <summary>
        /// Gets a customer data access object.
        /// </summary>
        public abstract ICustomerDao CustomerDao { get; }

        /// <summary>
        /// Gets an order data access object.
        /// </summary>
        public abstract IOrderDao OrderDao { get; }

        /// <summary>
        /// Gets a product data access object.
        /// </summary>
        public abstract IProductDao ProductDao { get; }
    }

    /// <summary>
    /// Abstract factory class that creates data access objects.
    /// </summary>
    /// <remarks>
    /// GoF Design Pattern: Factory.
    /// </remarks>
    public abstract class CopyOfDaoFactory
    {
        /// <summary>
        /// Gets a customer data access object.
        /// </summary>
        public abstract ICustomerDao CustomerDao { get; }

        /// <summary>
        /// Gets an order data access object.
        /// </summary>
        public abstract IOrderDao OrderDao { get; }

        /// <summary>
        /// Gets a product data access object.
        /// </summary>
        public abstract IProductDao ProductDao { get; }
    }
}
