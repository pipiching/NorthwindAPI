using Dapper.Contrib.Extensions;

namespace Northwind.Repository.Entities
{
    /// <summary>
    /// Products
    /// </summary>
    [Table("Products")]
    public class Product
    {
        /// <summary>
        ///
        /// </summary>
        [Key]
        public int ProductID { get; set; } //(int, not null)
        /// <summary>
        ///
        /// </summary>
        public string ProductName { get; set; } //((nvarchar(40)), not null)
        /// <summary>
        ///
        /// </summary>
        public int? SupplierID { get; set; } //(int, null)
        /// <summary>
        ///
        /// </summary>
        public int? CategoryID { get; set; } //(int, null)
        /// <summary>
        ///
        /// </summary>
        public string QuantityPerUnit { get; set; } //((nvarchar(20)), null)
        /// <summary>
        ///
        /// </summary>
        public decimal? UnitPrice { get; set; } //(money, null)
        /// <summary>
        ///
        /// </summary>
        public short? UnitsInStock { get; set; } //(smallint, null)
        /// <summary>
        ///
        /// </summary>
        public short? UnitsOnOrder { get; set; } //(smallint, null)
        /// <summary>
        ///
        /// </summary>
        public short? ReorderLevel { get; set; } //(smallint, null)
        /// <summary>
        ///
        /// </summary>
        public bool? Discontinued { get; set; } //(bit, not null)
    }
}
