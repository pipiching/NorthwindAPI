using Northwind.Api.Models;
using Northwind.Common.Enums;
using Northwind.Common.Helpers;
using System;
using System.ComponentModel;

namespace Northwind.Api.Helpers
{
    public class APIHelper
    {
        /// <summary>
        /// Create API Error object
        /// </summary>
        /// <param name="errorType">enums.ErrorType, it's base on enums.ErrorType</param>
        /// <param name="text">error message for user</param>
        /// <param name="exception">stacktrace</param>
        /// <returns></returns>
        public static APIError CreateAPIError(ErrorType errorType, string text, Exception exception = null)
        {
            APIError response = new APIError
            {
                Error = new APIErrorInfo
                {
                    Text = text,
                    Type = errorType.GetAttributeOfType<DescriptionAttribute>().Description,
                    Message = exception?.Message,
                    StackTrace = exception?.StackTrace,
                }
            };

            return response;
        }
    }
}