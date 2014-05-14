using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataObjects.Linq.LinqImplementation
{
    /// <summary>
    /// Linq-to-Sql specific factory that creates data access objects.
    /// </summary>
    /// <remarks>
    /// GoF Design Patterns: Factory.
    /// </remarks>
    public class LinqDaoFactory : DaoFactory
    {
        /// <summary>
        /// Gets a Linq specific customer data access object.
        /// </summary>
        public override ICustomerDao CustomerDao
        {
            get { return new LinqCustomerDao(); }
        }

        /// <summary>
        /// Gets a Linq specific order data access object.
        /// </summary>
        public override IOrderDao OrderDao
        {
            get { return new LinqOrderDao(); }
        }

        /// <summary>
        /// Gets a Linq specific product data access object.
        /// </summary>
        public override IProductDao ProductDao
        {
            get { return new LinqProductDao(); }
        }
    }
}

