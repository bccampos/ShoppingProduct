using bruno.Klir.Domain.Models;
using bruno.Klir.Domain.Product.Entities;
using bruno.Klir.Domain.Shopping.ValueObjects;

namespace bruno.Klir.Domain.Shopping
{
    public class ShoppingGroup : AggregateRoot<ShoppingGroupId>, IEquatable<ShoppingGroup>, IAggregateRoot
    {
        //public ShoppingGroupId ShoppingGroupId { get; set; }
        public List<ShoppingItem> Items { get; set; }

        public decimal Total { get; set; }

        public ShoppingGroup()
        {
            Items = new List<ShoppingItem>();
        }

        public void SumTotal(decimal value)
        {
            Total += value;
        }

        public void AddShoppingItem(ShoppingItem item)
        {
            Items.Add(item);
        }

        public void Clear()
        {
            Items.Clear();

            Total = 0;
        }

        public bool Equals(ShoppingGroup other)
        {
            return other != null && other.Id == Id;
        }
    }
}
