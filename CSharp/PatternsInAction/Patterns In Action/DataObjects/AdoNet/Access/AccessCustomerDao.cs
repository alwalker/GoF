using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using BusinessObjects;

namespace DataObjects.AdoNet.Access
{
    /// <summary>
    /// Microsoft Access specific data access object that handles data access
    /// of customers.
    /// </summary>
    public class AccessCustomerDao : ICustomerDao
    {
        /// <summary>
        /// Gets a list of all customers.
        /// </summary>
        /// <returns>Customer list.</returns>
        public IList<Customer> GetCustomers()
        {
            // Set default sortExpression
            return GetCustomers("CustomerId ASC");
        }

        /// <summary>
        /// Gets a sorted list of all customers.
        /// </summary>
        /// <param name="sortExpression">Sort order.</param>
        /// <returns>Sorted list of customers.</returns>
        public IList<Customer> GetCustomers(string sortExpression)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT CustomerId, CompanyName, City, Country ");
            sql.Append("   FROM Customer ");
            if (!string.IsNullOrEmpty(sortExpression))
                sql.Append(" ORDER BY " + sortExpression);

            DataTable dt = Db.GetDataTable(sql.ToString());

            return MakeCustomers(dt);
        }

        private IList<Customer> MakeCustomers(DataTable dt)
        {
            IList<Customer> list = new List<Customer>();
            foreach (DataRow row in dt.Rows)
                list.Add(MakeCustomer(row));

            return list;
        }

        private Customer MakeCustomer(DataRow row)
        {
            int customerId = int.Parse(row["CustomerId"].ToString());
            string company = row["CompanyName"].ToString();
            string city = row["City"].ToString();
            string country = row["Country"].ToString();

            return new Customer(customerId, company, city, country);
        }

        /// <summary>
        /// Gets a customer.
        /// </summary>
        /// <param name="customerId">Unique customer identifier.</param>
        /// <returns>Customer.</returns>
        public Customer GetCustomer(int customerId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT CustomerId, CompanyName, City, Country ");
            sql.Append("   FROM Customer");
            sql.Append("  WHERE CustomerId = " + customerId);

            DataRow row = Db.GetDataRow(sql.ToString());
            if (row == null) return null;

            return MakeCustomer(row);
        }

        /// <summary>
        /// Gets customer given an order.
        /// </summary>
        /// <param name="orderId">Unique order identifier.</param>
        /// <returns>Customer.</returns>
        public Customer GetCustomerByOrder(int orderId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT Customer.CustomerId, CompanyName, City, Country ");
            sql.Append("   FROM [Order] LEFT JOIN Customer ON [Order].CustomerId = Customer.CustomerId ");
            sql.Append("  WHERE OrderId = " + orderId);

            DataRow row = Db.GetDataRow(sql.ToString());
            if (row == null) return null;

            return MakeCustomer(row);

            //Customer customer = MakeCustomer(row);
            //order.Customer = customer;
            //return customer;
        }

        /// <summary>
        /// Inserts a new customer. 
        /// </summary>
        /// <remarks>
        /// Following insert, customer object will contain the new identifier.
        /// </remarks>
        /// <param name="customer">Customer.</param>
        public void InsertCustomer(Customer customer)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" INSERT INTO Customer (CompanyName, City, Country) ");
            sql.Append("  VALUES( " + Db.Escape(customer.Company) + ", ");
            sql.Append("          " + Db.Escape(customer.City)    + ", ");
            sql.Append("          " + Db.Escape(customer.Country) + ") ");

            // Assign new customer Id back to business object
            int newId = Db.Insert(sql.ToString(),true);
            customer.CustomerId = newId;
        }

        /// <summary>
        /// Updates a customer.
        /// </summary>
        /// <param name="customer">Customer.</param>
        /// <returns>Number of customer records updated.</returns>
        public int UpdateCustomer(Customer customer)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" UPDATE Customer ");
            sql.Append("    SET CompanyName = " + Db.Escape(customer.Company) + ", ");
            sql.Append("        City = " + Db.Escape(customer.City) + ", ");
            sql.Append("        Country = " + Db.Escape(customer.Country) + " ");
            sql.Append("  WHERE CustomerId = " + customer.CustomerId);

            return Db.Update(sql.ToString());
        }

        /// <summary>
        /// Deletes a customer.
        /// </summary>
        /// <param name="customer">Customer.</param>
        /// <returns>Number of customer records deleted.</returns>
        public int DeleteCustomer(Customer customer)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" DELETE FROM Customer  ");
            sql.Append("  WHERE CustomerId = " + customer.CustomerId );

            try { return Db.Update(sql.ToString()); }
            catch { return 0; }
        }
    }
}
