using bruno.Klir.Domain.Common.Command;
using bruno.Klir.Domain.Common.Interfaces;
using bruno.Klir.Domain.Common.Interfaces.Persistence;
using bruno.Klir.Domain.Product.Entities;
using bruno.Klir.Domain.Shopping.ValueObjects;
using FluentResults;
using MediatR;

namespace bruno.Klir.Application.Shopping.Commands.Handlers
{
    public class UpdateShoppingCommandHandler : CommandHandler, IRequestHandler<UpdateShoppingCommand, Result<ShoppingResult>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IShoppingGroupRepository _shoppingRepository;
        private readonly IShoppingService _shoppingService;

        public UpdateShoppingCommandHandler(IShoppingGroupRepository shoppingRepository, IShoppingService shoppingService, IProductRepository productRepository)
        {
            _shoppingRepository = shoppingRepository;
            _shoppingService = shoppingService;
            _productRepository = productRepository;
        }


        public async Task<Result<ShoppingResult>> Handle(UpdateShoppingCommand request, CancellationToken cancellationToken)
        {
            var shoppingGroup = await _shoppingRepository.GetByIdAsync(ShoppingGroupId.Parse(request.Id));

            if (shoppingGroup is null)
            {
                return Result.Fail("Shopping is not found");
            }

            foreach (var item in request.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);

                if (!product.IsProductValid())
                {
                    return Result.Fail("Product is not valid");
                }

                var current = shoppingGroup.Items.FirstOrDefault(i => i.ProductId.Equals(item.ProductId));

                if (current is null)
                {
                    shoppingGroup.AddShoppingItem(new ShoppingItem().Create(shoppingGroup.Id, item.ProductId, item.Price, item.Quantity,
                        item.Total = 0, item.PromotionApplied = false)); 

                    continue;
                }

                current.SetPrice(product.Price);
                current.SetQuantity(item.Quantity);
                current.SetTotal(0);
            }

            _shoppingRepository.Update(shoppingGroup);
            await UnitOfWork(_shoppingRepository.UnitOfWork);

            //TODO: Add the mappear
            return Result.Ok<ShoppingResult>(ShoppingResult.ToShoppingResult(shoppingGroup));
        }
    }
}
