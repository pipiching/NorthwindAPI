using AutoMapper;
using Northwind.Api.Helpers;
using Northwind.Api.Models;
using Northwind.Common.Enums;
using Northwind.Common.Utilities;
using Northwind.Repository.Entities;
using Northwind.Repository.Models;
using Northwind.Service.Models;
using Northwind.Service.Services.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace Northwind.Api.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private readonly IProductsService _productsService;
        private readonly IMapper _mapper;
        public ProductsController(IProductsService productsService, IMapper mapper)
        {
            _productsService = productsService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get([FromUri] ProductQueryParams queryParams)
        {
            ProductSearchModel searchModel = new ProductSearchModel();
            if (!string.IsNullOrEmpty(queryParams?.ProductID))
            {
                if (!int.TryParse(queryParams.ProductID, out int productID))
                {
                    return Content(HttpStatusCode.BadRequest, APIHelper.CreateAPIError(
                        ErrorType.INVALID_ID, "convert ProductID failed")
                    );
                }
                searchModel.ProductID = productID;
            }
            if (!string.IsNullOrEmpty(queryParams?.ProductName))
            {
                searchModel.ProductName = queryParams.ProductName;
            }

            IEnumerable<ProductDTO> products = _productsService.Get(searchModel);

            return Ok(products);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] ProductCreateAPIModel createModel)
        {
            Product product = _mapper.Map<ProductCreateAPIModel, Product>(createModel);
            _productsService.Create(product);

            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Get(string id)
        {
            try
            {
                if (!int.TryParse(id, out int productID))
                {
                    return Content(HttpStatusCode.BadRequest, APIHelper.CreateAPIError(
                        ErrorType.INVALID_ID, "convert ProductID failed")
                    );
                }

                ProductDTO product = _productsService.Get(productID);

                return Ok(product);
            }
            catch (OperationalException ex)
            {
                return Content(HttpStatusCode.BadRequest, APIHelper.CreateAPIError(ex.ErrorType, ex.Message));
            }

        }

        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Put(string id, [FromBody] ProductUpdateAPIModel updateModel)
        {
            try
            {
                if (!int.TryParse(id, out int productID))
                {
                    return Content(HttpStatusCode.BadRequest, APIHelper.CreateAPIError(
                        ErrorType.INVALID_ID, "convert ProductID failed")
                    );
                }

                Product product = _mapper.Map<ProductUpdateAPIModel, Product>(updateModel);
                product.ProductID = productID;
                _productsService.Update(product);
            }
            catch (OperationalException ex)
            {
                return Content(HttpStatusCode.BadRequest, APIHelper.CreateAPIError(ex.ErrorType, ex.Message));
            }

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(string id)
        {
            try
            {
                if (!int.TryParse(id, out int productID))
                {
                    return Content(HttpStatusCode.BadRequest, APIHelper.CreateAPIError(
                        ErrorType.INVALID_ID, "convert ProductID failed")
                    );
                }

                _productsService.Delete(productID);
            }
            catch (OperationalException ex)
            {
                return Content(HttpStatusCode.BadRequest, APIHelper.CreateAPIError(ex.ErrorType, ex.Message));
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}