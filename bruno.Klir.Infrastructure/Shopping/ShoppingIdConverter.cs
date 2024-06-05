using bruno.Klir.Domain.Shopping.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace bruno.Klir.Infrastructure.Products
{
    public class ShoppingIdConverter : ValueConverter<ShoppingGroupId, Guid>
    {
        public ShoppingIdConverter()
            : base(
                id => id.Value,    // Convert from ProductId to Guid
                value => new ShoppingGroupId(value)    // Convert from Guid to ProductId
            )
        {
        }
    }
}
