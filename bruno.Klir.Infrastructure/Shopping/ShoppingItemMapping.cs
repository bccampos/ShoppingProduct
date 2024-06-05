using bruno.Klir.Domain.Product.Entities;
using bruno.Klir.Infrastructure.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Klir.TechChallenge.Infra.ShoppingCart.Mapping
{
    public class ShoppingItemMapping : IEntityTypeConfiguration<ShoppingItem>
    {
        public void Configure(EntityTypeBuilder<ShoppingItem> builder)
        {
            builder.ToTable("ShoppingItems");

            var shoppingItemIdConverter = new ShoppingItemIdConverter();

            builder
           .Property(p => p.Id)
           .HasConversion(shoppingItemIdConverter);

            builder.Property(p => p.Quantity).IsRequired();
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(p => p.PromotionApplied).IsRequired().HasDefaultValue(false);
            builder.Property(p => p.Total).IsRequired().HasColumnType("decimal(18,2)");

            builder.HasOne(i => i.Product)
                .WithMany(p => p.Items)
                .HasForeignKey(i => i.ProductId)
                .HasPrincipalKey(p => p.Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
