using bruno.Klir.Domain.Shopping;
using bruno.Klir.Infrastructure.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Klir.TechChallenge.Infra.ShoppingCart.Mapping
{
    public class ShoppingMapping : IEntityTypeConfiguration<ShoppingGroup>
    {
        public void Configure(EntityTypeBuilder<ShoppingGroup> builder)
        {
            builder.ToTable("Shopping");

            var shoppingIdConverter = new ShoppingIdConverter();

            builder
           .Property(p => p.Id)
           .HasConversion(shoppingIdConverter);

            //builder.HasKey(x => x.Id);

            builder.Property(p => p.Total).HasColumnType("decimal(18,2)").IsRequired();

            builder.HasMany(c => c.Items)
                .WithOne(i => i.ShoppingGroup)
                .HasPrincipalKey(c => c.Id)
                .HasForeignKey(i => i.ShoppingGroupId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
