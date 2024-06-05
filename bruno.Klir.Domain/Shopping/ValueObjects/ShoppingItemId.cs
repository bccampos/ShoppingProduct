using bruno.Klir.Domain.Models;

namespace bruno.Klir.Domain.Shopping.ValueObjects
{
    public sealed class ShoppingItemId : ValueObject
    {
        public Guid Value { get; set; }

        public ShoppingItemId()
        {
        }

        public ShoppingItemId(Guid value)
        {
            Value = value;
        }

        public static ShoppingItemId Create()
        {
            return new(Guid.NewGuid());
        }

        public static ShoppingItemId Create(Guid guid)
        {
            return new ShoppingItemId(guid);
        }

        public void Set(Guid guid)
        {
            Value = guid;
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static ShoppingItemId? Parse(Guid? value)
        {
            if (value == null) return null;

            return new ShoppingItemId(value.Value);
        }
    }
}
