using Northwind.Api.Models;
using Northwind.Service.Models;
using Northwind.Service.Services;
using Northwind.Service.Services.Interfaces;
using System.Collections.Generic;
using System.Web.Http;

namespace Northwind.Api.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private readonly IProductsService _productsService;
        public ProductsController()
        {
            _productsService = new ProductsService();
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get([FromUri] ProductQueryParams queryParams)
        {
            IEnumerable<ProductDTO> products = _productsService.GetProducts();

            return Ok(products);
        }
    }
}