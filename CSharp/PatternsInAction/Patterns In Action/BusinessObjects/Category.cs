using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using BusinessObjects.BusinessRules;

namespace BusinessObjects
{
    /// <summary>
    /// Class that holds information about a product category.
    /// </summary>
    /// <remarks>
    /// Enterprise Design Pattern: Domain Model, Identity Field.
    /// 
    /// This is where your business logic resides. In this example there are none.
    /// Another place for business logic and business rules is in the Facade.  
    /// For an example see CustomerFacade in the Facade layer.
    /// 
    /// The Domain Model Design Pattern states that domain objects incorporate 
    /// both behavior and data. Behavior may include simple or complex business logic.
    /// 
    /// The Identity Field Design Pattern saves the ID field in an object to maintain
    /// identity between an in-memory business object and that database rows.
    /// </remarks>
    public class Category : BusinessObject
    {
        /// <summary>
        /// Default constructor. Establishes simple business rules.
        /// </summary>
        public Category()
        {
            AddRule(new ValidateId("CategoryId"));
            AddRule(new ValidateRequired("Name"));
            AddRule(new ValidateLength("Name", 0, 20));

            Version = _versionDefault;
        }

        /// <summary>
        /// Overloaded constructor for Category class.
        /// </summary>
        /// <param name="categoryId">Unique Identifier for the Category.</param>
        /// <param name="name">Name of the Category.</param>
        /// <param name="description">Description of the Category.</param>
        public Category(int categoryId, string name, string description)
            : this()
        {
            CategoryId = categoryId;
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Gets or sets unique category identifier.
        /// The Identity Field Design Pattern. 
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the category name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the category description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Version number. Used in optimistic concurrency decisions.
        /// </summary>
        public string Version { get; set; }
    }
}
