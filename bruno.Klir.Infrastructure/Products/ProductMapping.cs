using bruno.Klir.Domain.Aggregate;
using bruno.Klir.Domain.Product.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bruno.Klir.Infrastructure.Products
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            var productIdConverter = new ProductIdConverter();

            builder
           .Property(p => p.Id)
           .HasConversion(productIdConverter);

           builder.HasKey(x => x.Id);

            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)").IsRequired();

            builder.Property(p => p.IsActive)
                .IsRequired();

            var promotionIdConverter = new PromotionIdConverter();

            builder.HasOne(x => x.Promotion)
                .WithMany(x => x.Products)
                .HasPrincipalKey(x => x.Id)
                //.HasConversion(promotionIdConverter)
                .HasForeignKey(x => x.PromotionId)                
                .OnDelete(DeleteBehavior.SetNull);

            SeedData(builder);
        }

        private void SeedData(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(new List<Product>()
            {
                {
                    new Product
                    {
                        Id = ProductId.Parse(new Guid("61420e17-37fb-4c34-9a5c-7bc45a75aa3e")),
                        Name = "Product A",
                        Price = 20.00m,
                        PromotionId = PromotionId.Parse(new Guid("ba35b678-9289-45ce-a601-80f59cec908e")),
                        IsActive = true
                    }
                },
                {
                    new Product
                    {
                        Id = ProductId.Parse(new Guid("c81102fd-f418-4497-bbbc-1d8bf677f9e8")),
                        Name = "Product B",
                        Price = 4.00m,
                        PromotionId = PromotionId.Parse(new Guid("47133cb6-1135-4d05-b44a-bdfe41eb1b45")),
                        IsActive = true
                    }
                },
                {
                    new Product
                    {
                        Id = ProductId.Parse(new Guid("b5b566ad-1406-4a58-a3a6-76318c60e0af")),
                        Name = "Product C",
                        Price = 2.00m,
                        IsActive = true
                    }
                },
                {
                    new Product
                    {
                        Id = ProductId.Parse(new Guid("431f430f-bb42-462f-9294-68488f0e31a4")),
                        Name = "Product D",
                        Price = 4.00m,
                        IsActive = true
                    }
                }
            });
        }
    }
}
