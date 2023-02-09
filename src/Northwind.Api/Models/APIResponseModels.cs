namespace Northwind.Api.Models
{
    /// <summary>
    /// APIError
    /// </summary>
    public class APIError
    {
        /// <summary>
        /// API Error information
        /// </summary>
        public APIErrorInfo Error { get; set; }
    }

    /// <summary>
    /// API Error information
    /// </summary>
    public class APIErrorInfo
    {
        /// <summary>
        /// Enums.ErrorType description
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// error message for user
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Exception.Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Exception.StackTrace
        /// </summary>
        public string StackTrace { get; set; }
    }
}