using System;

namespace Northwind.Repository.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class WhereColumnNameAttribute : Attribute
    {
        private string _columnName = "";
        public string ColumnName
        {
            get { return _columnName; }
        }

        public WhereColumnNameAttribute(string columnName)
        {
            _columnName = columnName;
        }
    }
}
