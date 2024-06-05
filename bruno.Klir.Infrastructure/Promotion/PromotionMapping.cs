using bruno.Klir.Domain.Product.Entities;
using bruno.Klir.Domain.Product.ValueObjects;
using bruno.Klir.Infrastructure.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Klir.TechChallenge.Infra.Promotions.Mapping
{
    public class PromotionMapping : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.ToTable("Promotions");

            var promotionIdConverter = new PromotionIdConverter();

            builder
           .Property(p => p.Id)           
           .HasConversion(promotionIdConverter);

            //builder.HasKey(x => x.PromotionId);

            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Quantity).IsRequired();
            builder.Property(p => p.DiscountType).IsRequired();
            builder.Property(p => p.ApplyAtEach);
            builder.Property(p => p.DiscountValue).HasColumnType("decimal(18,2)");
            builder.Property(p => p.IsPromotionActive)
                .IsRequired();

            SeedData(builder);
        }

        private void SeedData(EntityTypeBuilder<Promotion> builder)
        {
            builder.HasData(new List<Promotion>
            {
                {
                    new Promotion
                    {
                        Id = PromotionId.Parse(new Guid("ba35b678-9289-45ce-a601-80f59cec908e")),
                        Name = "Buy 1 Get 1 Free",
                        DiscountType = bruno.Klir.Domain.Common.DiscountType.BuyOneGetOneFree,
                        Quantity = 2,
                        ApplyAtEach = 0,
                        DiscountValue = 0,
                        IsPromotionActive = true
                    }
                },
                {
                    new Promotion
                    {
                        Id = PromotionId.Parse(new Guid("47133cb6-1135-4d05-b44a-bdfe41eb1b45")),
                        Name = "3 for 10 Euro",
                        DiscountType = bruno.Klir.Domain.Common.DiscountType.ThreeForTen,
                        Quantity = 2,
                        ApplyAtEach = 0,
                        DiscountValue = 0,
                        IsPromotionActive = true
                    }
                }
            });
        }
    }
}
