namespace bruno.Klir.Contracts.Product
{
    public record ProductRequest(
        string Name,
        decimal Price,
        int Quantity);
}
