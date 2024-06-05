using bruno.Klir.Application.Product;
using bruno.Klir.Domain.Shopping;

namespace bruno.Klir.Application.Shopping
{
    public record ShoppingResult
    {        
        public Guid ShoppingId { get; set; }
        public decimal Total { get; set; }
        public ICollection<ShoppingItemResult> Items { get; set; }

        public static ShoppingResult ToShoppingResult(ShoppingGroup shoppingGroup)
        {
            return new ShoppingResult
            {
                ShoppingId = shoppingGroup.Id.Value,
                Total = shoppingGroup.Total,
                Items = shoppingGroup.Items.Select(item => new ShoppingItemResult
                {
                    ShoppingItemId = item.Id.Value,
                    ShoppingId = shoppingGroup.Id.Value,
                    ProductId = item.ProductId.Value,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Total = item.Total,
                    IsPromotionApplied = item.PromotionApplied,
                    Product = new ProductFullResult
                    {
                        Id = item.ProductId.Value,
                        Name = item.Product.Name,
                        Price = item.Product.Price,
                        PromotionId = item.Product.Id.Value,
                        IsActive = item.Product.IsActive,
                        Promotion = item.Product.Promotion is null ? null : new PromotionFullResult
                        {
                            Id = item.Product.Promotion.Id.Value,
                            Name = item.Product.Promotion.Name,
                            IsActive = item.Product.Promotion.IsPromotionActive,
                            Quantity = item.Product.Promotion.Quantity,
                            Type = item.Product.Promotion.DiscountType,
                        }
                    }
                }).OrderBy(x => x.Product.Name).ToList()
            };
        }
    }

    public record ShoppingItemResult
    {
        public Guid ShoppingItemId { get; set; }
        public Guid ShoppingId { get; set; }
        public ShoppingResult Shopping { get; set; }
        public Guid ProductId { get; set; }
        public ProductFullResult Product { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public int Quantity { get; set; }
        public bool IsPromotionApplied { get; set; }
    }

}
