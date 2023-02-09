namespace Northwind.Repository.Models
{
    public class ProductSearchModel
    {
        /// <summary>
        ///
        /// </summary>
        public int? ProductID { get; set; } //(int, not null)
        /// <summary>
        ///
        /// </summary>
        public string ProductName { get; set; } //((nvarchar(40)), not null)
    }
}
