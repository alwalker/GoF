using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.Linq;
using System.Data.Linq.Mapping;

using DataObjects.Linq.EntityMapper;
using BusinessObjects;
using DataObjects.Linq;

namespace DataObjects.Linq.LinqImplementation
{
    /// <summary>
    /// Linq-to-Sql implementation of the ICustomerDao interface.
    /// </summary>
    public class LinqCustomerDao : ICustomerDao
    {
        /// <summary>
        /// Gets list of customers in default sort order.
        /// </summary>
        /// <returns>List of customers.</returns>
        public IList<Customer> GetCustomers()
        {
            return GetCustomers("CustomerId ASC");
        }

        /// <summary>
        /// Gets list of customers in given sortorder.
        /// </summary>
        /// <param name="sortExpression">The required sort order.</param>
        /// <returns>List of customers.</returns>
        public IList<Customer> GetCustomers(string sortExpression)
        {
            using (ActionDataContext db = DataContextFactory.CreateContext())
            {
                IQueryable<CustomerEntity> query = db.CustomerEntities;

                if (sortExpression.Length > 0)
                {
                    string[] sort = sortExpression.Split(' ');
                    string sortColumn = sort[0];
                    string sortOrder = sort[1];

                    switch (sortColumn)
                    {
                        case "CustomerId":
                            if (sortOrder == "ASC")
                                query = query.OrderBy(c => c.CustomerId);
                            else
                                query = query.OrderByDescending(c => c.CustomerId);
                            break;
                        case "CompanyName":
                            if (sortOrder == "ASC")
                                query = query.OrderBy(c => c.CompanyName);
                            else
                                query = query.OrderByDescending(c => c.CompanyName);
                            break;
                        case "City":
                            if (sortOrder == "ASC")
                                query = query.OrderBy(c => c.City);
                            else
                                query = query.OrderByDescending(c => c.City);
                            break;
                        case "Country":
                            if (sortOrder == "ASC")
                                query = query.OrderBy(c => c.Country);
                            else
                                query = query.OrderByDescending(c => c.Country);
                            break;
                    }
                }
                return query.Select(c => Mapper.ToBusinessObject(c)).ToList();
            }
        }

        /// <summary>
        /// Gets a customer given a customer identifier.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns>The customer.</returns>
        public Customer GetCustomer(int customerId)
        {
            using (ActionDataContext db = DataContextFactory.CreateContext())
            {
                return Mapper.ToBusinessObject(db.CustomerEntities
                            .SingleOrDefault(p => p.CustomerId == customerId));
            }
        }

        /// <summary>
        /// Gets customer given an order.
        /// </summary>
        /// <param name="orderId">The identifier for the order for which customer is requested.</param>
        /// <returns>The customer.</returns>
        public Customer GetCustomerByOrder(int orderId)
        {
            using (ActionDataContext db = DataContextFactory.CreateContext())
            {
                return Mapper.ToBusinessObject( db.CustomerEntities.SelectMany(c => db.OrderEntities
                    .Where(o => c.CustomerId == o.CustomerId && o.OrderId == orderId),
                     (c, o) => c).SingleOrDefault(c => true)); 
            }
        }

        /// <summary>
        /// Inserts a new customer record to the database.
        /// </summary>
        /// <param name="customer">The customer to be inserted.</param>
        public void InsertCustomer(Customer customer)
        {
            using (ActionDataContext db = DataContextFactory.CreateContext())
            {
                try
                {
                    CustomerEntity entity = Mapper.ToEntity(customer);
                    db.CustomerEntities.InsertOnSubmit(entity);
                    db.SubmitChanges();

                    // update business object with new version and id
                    customer.CustomerId = entity.CustomerId;
                    customer.Version = VersionConverter.ToString(entity.Version);
                }
                catch (ChangeConflictException)
                {
                    throw new Exception("A change to customer record was made before your changes.");
                }
            }
        }

        /// <summary>
        /// Updates a customer record in the database.
        /// </summary>
        /// <param name="customer">The customer with updated values.</param>
        /// <returns>Number of rows affected.</returns>
        public int UpdateCustomer(Customer customer)
        {
            using (ActionDataContext db = DataContextFactory.CreateContext())
            {
                try
                {
                    CustomerEntity entity = Mapper.ToEntity(customer);
                    db.CustomerEntities.Attach(entity, true);
                    db.SubmitChanges();

                    // Update business object with new version
                    customer.Version = VersionConverter.ToString(entity.Version);

                    return 1;
                }
                catch (ChangeConflictException)
                {
                    throw new Exception("A change to customer record was made before your changes.");
                }
                catch
                {
                    return 0;
                }
                
            }
        }

        /// <summary>
        /// Deletes a customer record from the database.
        /// </summary>
        /// <param name="customer">The customer to be deleted.</param>
        /// <returns>Number of rows affected.</returns>
        public int DeleteCustomer(Customer customer)
        {
            using (ActionDataContext db = DataContextFactory.CreateContext())
            {
                try
                {
                    CustomerEntity entity = Mapper.ToEntity(customer);
                    db.CustomerEntities.Attach(entity, false);
                    db.CustomerEntities.DeleteOnSubmit(entity);
                    db.SubmitChanges(); 
                    
                    return 1; 
                }
                catch (ChangeConflictException)
                {
                    throw new Exception("A change to customer record was made before your changes.");
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }
    }
}
