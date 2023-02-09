using Northwind.Api.Models;
using System.Web.Http;

namespace Northwind.Api.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        public ProductsController()
        {

        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get([FromUri] ProductQueryParams queryParams)
        {
            var products = new { test = 1 };

            return Ok(products);
        }
    }
}