using Northwind.Api.Models;
using Northwind.Repository.Models;
using Northwind.Service.Models;
using Northwind.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Northwind.Api.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private readonly IProductsService _productsService;
        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get([FromUri] ProductQueryParams queryParams)
        {
            ProductSearchModel searchModel = new ProductSearchModel();
            if (!string.IsNullOrEmpty(queryParams.ProductID))
            {
                if (!int.TryParse(queryParams.ProductID, out int productID))
                {
                    throw new Exception("convert ProductID error");
                }
                searchModel.ProductID = productID;
            }
            if (!string.IsNullOrEmpty(queryParams.ProductName))
            {
                searchModel.ProductName = queryParams.ProductName;
            }

            IEnumerable<ProductDTO> products = _productsService.GetProducts(searchModel);

            return Ok(products);
        }
    }
}