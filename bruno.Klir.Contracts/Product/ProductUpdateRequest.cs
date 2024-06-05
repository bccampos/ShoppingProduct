namespace bruno.Klir.Contracts.Product
{
    public record ProductUpdateRequest(
        Guid Id,
        string Name,
        decimal Price);
}
