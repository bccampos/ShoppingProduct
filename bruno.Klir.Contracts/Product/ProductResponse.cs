namespace bruno.Klir.Contracts.Product
{
    public record ProductResponse(
        Guid Id,
        string Name,
        decimal Price,
        int Quantity);
}
