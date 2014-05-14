using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace DataObjects.Linq
{
    /// <summary>
    /// DataContext factory caches the connectionstring and the 
    /// mapping source so that DataContext instances can be created quickly.
    /// This significantly reduces the DataContext creation times. 
    /// </summary>
    public static class DataContextFactory
    {
        private static readonly string _connectionString;
        private static readonly MappingSource _mappingSource;

        /// <summary>
        /// Static constructor.
        /// </summary>
        /// <remarks>
        /// Static initialization of connectionstring and mappingSource.
        /// This significantly increases performance, primarily due to mappingSource cache.
        /// </remarks>        
        static DataContextFactory()
        {
            string connectionStringName = ConfigurationManager.AppSettings.Get("ConnectionStringName");
            _connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            
            DataContext context = new ActionDataContext(_connectionString);
            _mappingSource = context.Mapping.MappingSource;
        }

        /// <summary>
        /// Rapidly creates a new DataContext using cached connectionstring and mapping source.
        /// </summary>
        /// <returns></returns>
        public static ActionDataContext CreateContext()
        {
            return new ActionDataContext(_connectionString, _mappingSource);
        }
    }
}
