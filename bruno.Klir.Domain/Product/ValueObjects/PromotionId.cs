using bruno.Klir.Domain.Models;

namespace bruno.Klir.Domain.Product.ValueObjects
{
    public sealed class PromotionId : ValueObject
    {
        public Guid Value { get; set; }

        public PromotionId()
        {
        }
        public PromotionId(Guid value)
        { 
            Value = value;
        }

        public static PromotionId Create()
        {
            return new(Guid.NewGuid());
        }

        public static PromotionId Create(Guid guid)
        {
            return new PromotionId(guid);
        }

        public void Set(Guid guid)
        {
            Value = guid;
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static PromotionId? Parse(Guid? value)
        {
            if (value == null) return null;

            return new PromotionId(value.Value);
        }
    }
}
