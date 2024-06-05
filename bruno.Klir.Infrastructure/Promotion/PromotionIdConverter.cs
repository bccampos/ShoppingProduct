using bruno.Klir.Domain.Product.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace bruno.Klir.Infrastructure.Products
{
    public class PromotionIdConverter : ValueConverter<PromotionId, Guid>
    {
        public PromotionIdConverter()
            : base(
                id => id.Value,    // Convert from ProductId to Guid
                value => new PromotionId(value)    // Convert from Guid to ProductId
            )
        {
        }
    }
}
