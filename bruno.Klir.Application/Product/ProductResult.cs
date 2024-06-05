using bruno.Klir.Domain.Common;

namespace bruno.Klir.Application.Product
{
    public record ProductResult(
        Guid ProductId,
        Guid? PromotionId,
        string Name,
        decimal Price,
        int Quantity);

    public record ProductFullResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public Guid? PromotionId { get; set; }
        public PromotionFullResult? Promotion { get; set; }
    }

    public record PromotionFullResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DiscountType Type { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; }
    }
}
