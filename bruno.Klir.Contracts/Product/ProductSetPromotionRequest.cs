namespace bruno.Klir.Contracts.Product
{
    public record ProductSetPromotionRequest(
        Guid ProductId,
        Guid PromotionId);
}
