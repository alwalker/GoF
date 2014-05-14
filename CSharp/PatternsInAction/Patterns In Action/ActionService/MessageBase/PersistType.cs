using System;
using System.Data;
using System.Configuration;

using System.Runtime.Serialization;

namespace ActionService.MessageBase
{
    /// <summary>
    /// Enumeration of database persistence actions.
    /// (Also called CRUD operations: Create, Read, Update, Delete).
    /// 
    /// SOA Design Pattern: Command Message. Basically this in an instruction
    /// or command to the receiver which operatin to execute.
    /// </summary>
    [DataContract(Namespace = "http://www.yourcompany.com/types/")]
    public enum PersistType
    {
        /// <summary>
        /// Insert record in database.
        /// </summary>
        [EnumMember]
        Insert = 1,

        /// <summary>
        /// Update record in database.
        /// </summary>
        [EnumMember]
        Update = 2,

        /// <summary>
        /// Delete record from database.
        /// </summary>
        [EnumMember]
        Delete = 3
    }
}
