using bruno.Klir.Domain.Product.Entities;
using bruno.Klir.Domain.Product.ValueObjects;
using bruno.Klir.Domain.Shopping;
using bruno.Klir.Domain.Shopping.ValueObjects;

namespace bruno.Klir.Application.Tests.Shopping.Mock
{
    public class MockShopping
    {
        private static MockShopping _instance { get; set; }

        private MockShopping()
        {
            ShoppingItem = new List<ShoppingItem>
            {
                new ShoppingItem().FullCreate(ShoppingItemId.Parse(new Guid("ba35b678-9289-45ce-a601-80f59cec908e")), ShoppingGroupId.Parse(new Guid("d000c53e-0713-47f2-8d48-5308258f5fa5")), 
                                              ProductId.Parse(new Guid("aa967a5d-5edd-418d-804d-7ea68e2b65fe")), 20, 2, 0, false, 
                                              MockProduct.GetInstance().Products.FirstOrDefault(x => x.Id.Value.Equals(Guid.Parse("aa967a5d-5edd-418d-804d-7ea68e2b65fe")))),


                new ShoppingItem().FullCreate(ShoppingItemId.Parse(new Guid("ba35b678-9289-45ce-a601-80f59cec909e")), ShoppingGroupId.Parse(new Guid("d000c53e-0713-47f2-8d48-5308258f5fa5")), ProductId.Parse(new Guid("df3c40e7-a27b-4777-9198-c78dce1d351f")), 4, 3, 0, false, MockProduct.GetInstance().Products.FirstOrDefault(x => x.Id.Value.Equals(Guid.Parse("df3c40e7-a27b-4777-9198-c78dce1d351f")))),
                new ShoppingItem().FullCreate(ShoppingItemId.Parse(new Guid("ba35b678-9289-45ce-a601-80f59cec910e")), ShoppingGroupId.Parse(new Guid("d000c53e-0713-47f2-8d48-5308258f5fa5")), ProductId.Parse(new Guid("416a5727-1815-46c7-ad69-0d32bd9952a2")), 2, 5, 0, false, MockProduct.GetInstance().Products.FirstOrDefault(x => x.Id.Value.Equals(Guid.Parse("416a5727-1815-46c7-ad69-0d32bd9952a2")))),
                new ShoppingItem().FullCreate(ShoppingItemId.Parse(new Guid("ba35b678-9289-45ce-a601-80f59cec911e")), ShoppingGroupId.Parse(new Guid("d000c53e-0713-47f2-8d48-5308258f5fa5")), ProductId.Parse(new Guid("cf3390e9-5642-4e81-babe-7b3e01e43faa")), 4, 2, 0, false, MockProduct.GetInstance().Products.FirstOrDefault(x => x.Id.Value.Equals(Guid.Parse("cf3390e9-5642-4e81-babe-7b3e01e43faa")))),
            };

            ShoppingGroup = new List<ShoppingGroup>
            {
                new ShoppingGroup
                {
                    Id = ShoppingGroupId.Parse(new Guid("d000c53e-0713-47f2-8d48-5308258f5fa5")),                    
                    Items = ShoppingItem.ToList(),
                    Total = 0
                },
                new ShoppingGroup
                {
                    Id = ShoppingGroupId.Parse(new Guid("1bc489e3-6990-4744-8262-17ea00e8332d")),
                    Items = Enumerable.Empty<ShoppingItem>().ToList(),
                    Total = 0
                }
            };
        }
        public static MockShopping GetInstance()
        {
            if (_instance is null)
                _instance = new MockShopping();

            return _instance;
        }

        public IList<ShoppingGroup> ShoppingGroup { get; set; }
        public IList<ShoppingItem> ShoppingItem { get; set; }
        public ShoppingGroup GetById(Guid id) => ShoppingGroup.FirstOrDefault(x => x.Id.Equals(id));
    }
}
