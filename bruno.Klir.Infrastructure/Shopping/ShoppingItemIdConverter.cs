using bruno.Klir.Domain.Shopping.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace bruno.Klir.Infrastructure.Products
{
    public class ShoppingItemIdConverter : ValueConverter<ShoppingItemId, Guid>
    {
        public ShoppingItemIdConverter()
            : base(
                id => id.Value,    // Convert from ProductId to Guid
                value => new ShoppingItemId(value)    // Convert from Guid to ProductId
            )
        {
        }
    }
}
