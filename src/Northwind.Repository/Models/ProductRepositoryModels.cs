using Northwind.Common.Constants;
using Northwind.Repository.Attributes;

namespace Northwind.Repository.Models
{
    public class ProductSearchModel
    {
        public int? ProductID { get; set; }
        [WhereOperator(TSqlOperator.Like)]
        public string ProductName { get; set; }
    }
}
