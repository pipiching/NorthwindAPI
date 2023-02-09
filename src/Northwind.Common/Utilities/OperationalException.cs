using Northwind.Common.Enums;
using System;

namespace Northwind.Common.Utilities
{
    /// <summary>
    /// Operational Exception that could be controlled。
    /// </summary>
    public class OperationalException : Exception
    {
        public ErrorType ErrorType { get; set; }

        public string Details { get; set; }

        public OperationalException(string message) : base(message)
        {
            ErrorType = ErrorType.OPERATIONAL_EXCEPTION;
            Details = "";
        }

        public OperationalException(ErrorType errorType, string message) : base(message)
        {
            ErrorType = errorType;
            Details = "";
        }

        public OperationalException(ErrorType errorType, string message, string details) : base(message)
        {
            ErrorType = errorType;
            Details = details;
        }

        public OperationalException(ErrorType errorType, string message, string details, Exception inner) : base(message, inner)
        {
            ErrorType = errorType;
            Details = details;
        }

    }
}
