using bruno.Klir.Domain.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bruno.Klir.Domain.Product.ValueObjects
{
    public sealed class ProductId : ValueObject
    {
        public Guid Value { get; }

        private ProductId()
        {
        }

        public ProductId(Guid value)
        { 
            Value = value;
        }

        public static ProductId Create()
        {
            return new(Guid.NewGuid());
        }

        public static ProductId Create(Guid Value)
        {
            return new ProductId(Value);
        }

        public static ProductId Parse(Guid value)
        {
            return new ProductId(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
