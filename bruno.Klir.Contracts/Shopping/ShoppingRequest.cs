namespace bruno.Klir.Contracts.Shopping
{
    public record ShoppingRequest
    {
        public ICollection<ShoppingItemRequest> Items { get; set; } = new List<ShoppingItemRequest>();
    }

    public record ShoppingUpdateRequest
    {
        public Guid Id { get; set; }
        public ICollection<ShoppingItemUpdateRequest> Items { get; set; } = new List<ShoppingItemUpdateRequest>();
    }
}
