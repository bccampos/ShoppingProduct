using bruno.Klir.Domain.Common.Interfaces;
using bruno.Klir.Domain.Common.Interfaces.Persistence;
using bruno.Klir.Domain.Shopping.ValueObjects;
using FluentResults;
using MediatR;

namespace bruno.Klir.Application.Shopping.Commands.Handlers
{
    public class CalculateShoppingCommandHandler : IRequestHandler<CalculateShoppingCommand, Result<ShoppingResult>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IShoppingGroupRepository _shoppingRepository;
        private readonly IShoppingService _shoppingService;

        public CalculateShoppingCommandHandler(IShoppingGroupRepository shoppingRepository, IShoppingService shoppingService, IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _shoppingRepository = shoppingRepository;
            _shoppingService = shoppingService;
        }

        public async Task<Result<ShoppingResult>> Handle(CalculateShoppingCommand request, CancellationToken cancellationToken)
        {
            var shoppingGroup = await _shoppingRepository.GetByIdAsync(ShoppingGroupId.Parse(request.ShoppingId));

            if (shoppingGroup is null)
            {
                return Result.Fail("Shopping is not found");
            }       

            var calculationResult = await _shoppingService.RecalculatePrice(shoppingGroup.Id);

            //TODO: Add the mappear
            return Result.Ok<ShoppingResult>(ShoppingResult.ToShoppingResult(calculationResult));
        }

    }
}
