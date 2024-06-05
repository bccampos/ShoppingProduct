using bruno.Klir.Domain.Models;

namespace bruno.Klir.Domain.Shopping.ValueObjects
{
    public sealed class ShoppingGroupId : ValueObject
    {
        public Guid Value { get; set; }

        public ShoppingGroupId()
        {
        }

        public ShoppingGroupId(Guid value)
        {
            Value = value;
        }

        public static ShoppingGroupId Create()
        {
            return new(Guid.NewGuid());
        }

        public static ShoppingGroupId Create(Guid guid)
        {
            return new ShoppingGroupId(guid);
        }

        public void Set(Guid guid)
        {
            Value = guid;
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static ShoppingGroupId? Parse(Guid? value)
        {
            if (value == null) return null;

            return new ShoppingGroupId(value.Value);
        }
    }
}
