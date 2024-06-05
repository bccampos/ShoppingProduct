using bruno.Klir.Domain.Common.Interfaces.Persistence;
using bruno.Klir.Domain.Product.Entities;

namespace bruno.Klir.Infrastructure.Repositories
{
    public class FakePromotionRepository : IPromotionRepository
    {
        private readonly static List<Promotion> _existing = GetFakePromotion();

        public int CommitCalledCount { get; set; }
        public int DeleteCalledCount { get; set; }

        public FakePromotionRepository()
        {
        }

        public Task<List<Promotion>> GetAllAsync()
        {
            return Task.FromResult(_existing);
        }


        public Task CommitAsync()
        {
            CommitCalledCount++;
            return Task.CompletedTask;
        }

        private static List<Promotion> GetFakePromotion()
        {
            return new List<Promotion>()
            {
                new Promotion().Create(new Guid("ba35b678-9289-45ce-a601-80f59cec908e"), "Buy 1 Get 1 Free", 0, Domain.Common.DiscountType.BuyOneGetOneFree, 2, 0),
                new Promotion().Create(new Guid("47133cb6-1135-4d05-b44a-bdfe41eb1b45"), "3 for 10 Euro", 1, Domain.Common.DiscountType.ThreeForTen, 3, 10)
            };
        }

    }
}
