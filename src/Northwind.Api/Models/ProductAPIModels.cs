﻿namespace Northwind.Api.Models
{
    public class ProductQueryParams
    {
        /// <summary>
        ///
        /// </summary>
        public string ProductID { get; set; } //(int, not null)
        /// <summary>
        ///
        /// </summary>
        public string ProductName { get; set; } //((nvarchar(40)), not null)
    }
}