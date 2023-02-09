using Northwind.Api.Helpers;
using Northwind.Common.Enums;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace Northwind.Api.Filters
{
    public class ErrorHandlerAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionContext)
        {
            if (actionContext.Exception == null)
            {
                return;
            }

            var response = APIHelper.CreateAPIError(ErrorType.SERVER_INTERNAL_ERROR, "Internal Server Error", actionContext.Exception);

            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.InternalServerError, response);

            throw new HttpResponseException(actionContext.Response);
        }
    }
}