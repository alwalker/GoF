using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjects.AdoNet.Oracle
{
    /// <summary>
    /// Oracle specific factory that creates Oracle specific data access objects.
    /// 
    /// GoF Design Pattern: Factory.
    /// </summary>
    public class OracleDaoFactory : DaoFactory
    {
        /// <summary>
        /// Gets an Oracle specific customer data access object.
        /// </summary>
        public override ICustomerDao CustomerDao
        {
            get { return new OracleCustomerDao(); }
        }

        /// <summary>
        /// Gets an Oracle specific order data access object.
        /// </summary>
        public override IOrderDao OrderDao
        {
            get { return new OracleOrderDao(); }
        }

        /// <summary>
        /// Gets an Oracle specific product data access object.
        /// </summary>
        public override IProductDao ProductDao
        {
            get { return new OracleProductDao(); }
        }
    }
}
