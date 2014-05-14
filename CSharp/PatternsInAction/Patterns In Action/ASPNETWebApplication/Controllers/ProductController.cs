using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.ComponentModel;

using ASPNETWebApplication.ActionServiceReference;

namespace ASPNETWebApplication.Controllers
{
    /// <summary>
    /// Controller for products.
    /// </summary>
    /// <remarks>
    /// MV Patterns: Model View Controller Pattern.
    /// This is an 'informal' implementation of the MVC pattern.
    /// </remarks>
    [DataObject(true)]
    public class ProductController : ControllerBase
    {
        /// <summary>
        /// Gets a list of product categories.
        /// </summary>
        /// <returns>List of categories.</returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public IList<Category> GetCategories()
        {
            ProductRequest request = new ProductRequest();
            request.RequestId = NewRequestId;
            request.AccessToken = AccessToken;
            request.ClientTag = ClientTag;

            request.LoadOptions = new string[] { "Categories" };

            ProductResponse response = ActionServiceClient.GetProducts(request);

            if (request.RequestId != response.CorrelationId)
                throw new ApplicationException("GetCategories: RequestId and CorrelationId do not match.");

            return response.Categories;
        }

        /// <summary>
        /// Gets a list of products.
        /// </summary>
        /// <param name="categoryId">The category for which products are requested.</param>
        /// <param name="sortExpression">Sort order in which products are returned.</param>
        /// <returns>List of products.</returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public IList<Product> GetProducts(int categoryId, string sortExpression)
        {
            ProductRequest request = new ProductRequest();
            request.RequestId = NewRequestId;
            request.AccessToken = AccessToken;
            request.ClientTag = ClientTag;

            request.LoadOptions = new string[] { "Products" };
            request.Criteria = new ProductCriteria
            {
                CategoryId = categoryId,
                SortExpression = sortExpression
            };
       
            ProductResponse response = ActionServiceClient.GetProducts(request);

            if (request.RequestId != response.CorrelationId)
                throw new ApplicationException("GetProductsByCategory: RequestId and CorrelationId do not match.");

            return response.Products;

        }

        /// <summary>
        /// Gets a specific product.
        /// </summary>
        /// <param name="productId">Unique product identifier.</param>
        /// <returns>The requested product.</returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public Product GetProduct(int productId)
        {
            ProductRequest request = new ProductRequest();
            request.RequestId = NewRequestId;
            request.AccessToken = AccessToken;
            request.ClientTag = ClientTag;

            request.LoadOptions = new string[] { "Product" };
            request.Criteria = new ProductCriteria { ProductId = productId };

            ProductResponse response = ActionServiceClient.GetProducts(request);

            if (request.RequestId != response.CorrelationId)
                throw new ApplicationException("GetProductsByCategory: RequestId and CorrelationId do not match.");

            return response.Product;

        }

        /// <summary>
        /// Searches for products.
        /// </summary>
        /// <param name="productName">Product name.</param>
        /// <param name="priceRangeId">Price range identifier.</param>
        /// <param name="sortExpression">Sort order in which products are returned.</param>
        /// <returns>List of products that meet the search criteria.</returns>
        public IList<Product> SearchProducts(string productName, int priceRangeId, string sortExpression)
        {
            ProductRequest request = new ProductRequest();
            request.RequestId = NewRequestId;
            request.AccessToken = AccessToken;
            request.ClientTag = ClientTag;

            request.LoadOptions = new string[] { "Products", "Search" };

            double priceFrom = -1;
            double priceThru = -1;
            if (priceRangeId > 0)
            {
                PriceRangeItem pri = PriceRange.List[priceRangeId];
                priceFrom = pri.RangeFrom;
                priceThru = pri.RangeThru;
            }

            request.Criteria = new ProductCriteria
            {
                ProductName = productName,
                PriceFrom = priceFrom,
                PriceThru = priceThru,
                SortExpression = sortExpression
            };

            ProductResponse response = ActionServiceClient.GetProducts(request);

            if (request.RequestId != response.CorrelationId)
                throw new ApplicationException("SearchProducts: Request and CorrelationId do not match.");

            return response.Products;
        }

        /// <summary>
        /// Gets a list of price ranges.
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public IList<PriceRangeItem> GetProductPriceRange()
        {
            return PriceRange.List;
        }
    }

    /// <summary>
    /// A utility class with a list of price ranges. The price ranges are  
    /// used in building search criteria for the product catalog. 
    /// </summary>
    public static class PriceRange
    {
        private static IList<PriceRangeItem> _list = null;

        /// <summary>
        /// Static constructor for PriceRange.
        /// </summary>
        static PriceRange()
        {
            _list = new List<PriceRangeItem>();

            _list.Add(new PriceRangeItem(0, 0, 0, ""));
            _list.Add(new PriceRangeItem(1, 0, 50, "$0 - $50"));
            _list.Add(new PriceRangeItem(2, 51, 100, "$51 - $100"));
            _list.Add(new PriceRangeItem(3, 101, 250, "$101 - $250"));
            _list.Add(new PriceRangeItem(4, 251, 1000, "$251 - $1,000"));
            _list.Add(new PriceRangeItem(5, 1001, 2000, "$1,001 - $2,000"));
            _list.Add(new PriceRangeItem(6, 2001, 10000, "$2,001 - $10,000"));
        }

        /// <summary>
        /// Gets the list of price ranges.
        /// </summary>
        public static IList<PriceRangeItem> List
        {
            get { return _list; }
        }
    }

    /// <summary>
    /// A PriceRange item used in the PriceRange list.  PriceRanges are used for 
    /// searching the product catalog. 
    /// </summary>
    public class PriceRangeItem
    {
        /// <summary>
        /// Constructor for PriceRangeItem.
        /// </summary>
        /// <param name="rangeId">Unique identifier for the price range.</param>
        /// <param name="rangeFrom">Lower end of the price range.</param>
        /// <param name="rangeThru">Higher end of the price range.</param>
        /// <param name="rangeText">Easy-to-read form of the price range.</param>
        public PriceRangeItem(int rangeId, double rangeFrom, double rangeThru, string rangeText)
        {
            RangeId = rangeId;
            RangeFrom = rangeFrom;
            RangeThru = rangeThru;
            RangeText = rangeText;
        }

        /// <summary>
        /// Gets the unique PriceRange identifier.
        /// </summary>
        public int RangeId { get; private set; }

        /// <summary>
        /// Gets the low end of the PriceRange item.
        /// </summary>
        public double RangeFrom { get; private set; }

        /// <summary>
        /// Gets the high end of the PriceRange item.
        /// </summary>
        public double RangeThru { get; private set; }

        /// <summary>
        /// Gets an easy-to-read form of the PriceRange item.
        /// </summary>
        public string RangeText { get; private set; }
    }
}
