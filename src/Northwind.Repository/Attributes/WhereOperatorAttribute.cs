using Northwind.Common.Constants;
using System;

namespace Northwind.Repository.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class WhereOperatorAttribute : Attribute
    {
        private string _operator = TSqlOperator.EqualTo;
        public string Operator
        {
            get { return _operator; }
        }

        public WhereOperatorAttribute(string @operator = TSqlOperator.EqualTo)
        {
            _operator = @operator;
        }
    }
}
