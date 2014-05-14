using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataObjects
{
    /// <summary>
    /// Factory of factories. This class is a factory class that creates
    /// data-base specific factories which in turn create data acces objects.
    /// </summary>
    /// <remarks>
    /// GoF Design Patterns: Factory.
    /// 
    /// This is the abstract factory design pattern applied in a hierarchy
    /// in which there is a factory of factories.
    /// </remarks>
    public class DaoFactories
    {
        /// <summary>
        /// Gets a provider specific (i.e. database specific) factory 
        /// 
        /// GoF Design Pattern: Factory
        /// </summary>
        /// <param name="dataProvider">Database provider.</param>
        /// <returns>Data access object factory.</returns>
        public static DaoFactory GetFactory(string dataProvider)
        {
            // Return the requested DaoFactory
            switch (dataProvider)
            {
                case "ADO.NET.Access": return new AdoNet.Access.AccessDaoFactory();
                case "ADO.NET.SqlExpress": return new AdoNet.SqlServer.SqlServerDaoFactory();
                case "ADO.NET.SqlServer": return new AdoNet.SqlServer.SqlServerDaoFactory();
                case "ADO.NET.Oracle": return new AdoNet.Oracle.OracleDaoFactory();
                case "LinqToSql.SqlExpress": return new Linq.LinqImplementation.LinqDaoFactory();
                case "LinqToSql.SqlServer": return new Linq.LinqImplementation.LinqDaoFactory();
                
                // Just in case: the Design Pattern Framework always has MS Access available.
                default: return new AdoNet.Access.AccessDaoFactory();
            }
        }
    }
}
