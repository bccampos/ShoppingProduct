using bruno.Klir.Domain.Common;
using bruno.Klir.Domain.Common.Command;
using bruno.Klir.Domain.Common.Interfaces;
using bruno.Klir.Domain.Common.Interfaces.Persistence;
using bruno.Klir.Domain.Factories.Promotion;
using bruno.Klir.Domain.Shopping;
using bruno.Klir.Domain.Shopping.ValueObjects;

namespace bruno.Klir.Domain.Services
{
    public class ShoppingService : CommandHandler, IShoppingService
    {
        private readonly IShoppingGroupRepository _shoppingGroupRepository;
        private readonly IProductRepository _productRepository;

        public ShoppingService(IShoppingGroupRepository shoppingGroupRepository, IProductRepository productRepository)
        {
            _shoppingGroupRepository = shoppingGroupRepository;
            _productRepository = productRepository; 
        }

        public async Task<ShoppingGroup> RecalculatePrice(ShoppingGroupId shoppingGroupId)
        {
            try
            {
                var group = await _shoppingGroupRepository.GetByIdAsync(shoppingGroupId);

                if (group == null)
                {
                    throw new Exception("Database error");
                }
                foreach (var item in group.Items)
                {
                    var product = await _productRepository.GetByIdAsync(item.ProductId);
                    if (product.IsProductValid())
                    {
                        try
                        {
                            PromotionFactory.ApplyPromotion(item, product.Promotion?.DiscountType ?? DiscountType.None);
                        }
                        catch (Exception ex)
                        {
                            // Service error during promotion application
                            throw new Exception("Service error", ex);
                        }
                    }
                }

                group.Total = group.Items.Sum(x => x.Total);
                _shoppingGroupRepository.Update(group);

                await UnitOfWork(_shoppingGroupRepository.UnitOfWork);

                return group;
            }
            catch (Exception ex)
            {
                // Log exception or handle it as needed
                throw; 
            }
        }
    }
}
