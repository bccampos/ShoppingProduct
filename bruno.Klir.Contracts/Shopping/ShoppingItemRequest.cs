namespace bruno.Klir.Contracts.Shopping
{
    public record ShoppingItemRequest
    {
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public record ShoppingItemUpdateRequest
    {
        public Guid ShoppingId { get; set; }
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
