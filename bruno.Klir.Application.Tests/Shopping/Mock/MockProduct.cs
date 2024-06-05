using bruno.Klir.Domain.Common;
using bruno.Klir.Domain.Product.Entities;
using bruno.Klir.Domain.Product.ValueObjects;
using bruno.Klir.Domain.Shopping.ValueObjects;

namespace bruno.Klir.Application.Tests.Shopping.Mock
{
    public class MockProduct
    {
        private static MockProduct _instance { get; set; }

        private MockProduct()
        {
            Products = new List<Domain.Aggregate.Product>()
            {
                new Domain.Aggregate.Product().FullCreate(ProductId.Parse(new Guid("aa967a5d-5edd-418d-804d-7ea68e2b65fe")), "Product A", 20.00m, 2, PromotionId.Parse(new Guid("ba35b678-9289-45ce-a601-80f59cec908e")), MockPromotion.GetInstance().Promotions.FirstOrDefault(x => x.DiscountType == DiscountType.BuyOneGetOneFree)),
                new Domain.Aggregate.Product().FullCreate(ProductId.Parse(new Guid("df3c40e7-a27b-4777-9198-c78dce1d351f")), "Product B", 4.00m, 3, PromotionId.Parse(new Guid("47133cb6-1135-4d05-b44a-bdfe41eb1b45")), MockPromotion.GetInstance().Promotions.FirstOrDefault(x => x.DiscountType == DiscountType.ThreeForTen)),
                new Domain.Aggregate.Product().FullCreate(ProductId.Parse(new Guid("416a5727-1815-46c7-ad69-0d32bd9952a2")), "Product C", 2.00m, 5, null, null),
                new Domain.Aggregate.Product().FullCreate(ProductId.Parse(new Guid("cf3390e9-5642-4e81-babe-7b3e01e43faa")), "Product D", 4.00m, 2, null, null)
            };
        }

        public static MockProduct GetInstance()
        {
            if (_instance is null)
                _instance = new MockProduct();

            return _instance;
        }

        public IList<Domain.Aggregate.Product> Products { get; set; }

    }

    public class MockPromotion
    {
        private static MockPromotion _instance { get; set; }

        private MockPromotion()
        {
            Promotions = new List<Promotion>()
            {
                new Promotion().Create(new Guid("ba35b678-9289-45ce-a601-80f59cec908e"), "Buy 1 Get 1 Free", 0, Domain.Common.DiscountType.BuyOneGetOneFree, 2, 0),
                new Promotion().Create(new Guid("47133cb6-1135-4d05-b44a-bdfe41eb1b45"), "3 for 10 Euro", 1, Domain.Common.DiscountType.ThreeForTen, 3, 10)
            };
        }

        public static MockPromotion GetInstance()
        {
            if (_instance is null)
                _instance = new MockPromotion();

            return _instance;
        }
        public IList<Promotion> Promotions { get; set; }
    }
}
