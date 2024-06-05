using bruno.Klir.Domain.Product.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace bruno.Klir.Infrastructure.Products
{
    public class ProductIdConverter : ValueConverter<ProductId, Guid>
    {
        public ProductIdConverter()
            : base(
                id => id.Value,    // Convert from ProductId to Guid
                value => new ProductId(value)    // Convert from Guid to ProductId
            )
        {
        }
    }
}
