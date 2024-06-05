using bruno.Klir.Application.Common.Errors;
using bruno.Klir.Domain.Common.Command;
using bruno.Klir.Domain.Common.Interfaces.Persistence;
using bruno.Klir.Domain.Product.ValueObjects;
using FluentResults;
using MediatR;

namespace bruno.Klir.Application.Product.Commands.Handlers
{
    public class RemoveShoppingCommandHandler : CommandHandler, IRequestHandler<RemoveProductPromotionCommand, Result>
    {
        private readonly IProductRepository _productRepository;

        public RemoveShoppingCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result> Handle(RemoveProductPromotionCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(ProductId.Parse(request.ProductId));

            if (product is null)
            {
                return Result.Fail(new NotFoundError());
            }

            product.ClearPromotion();

            _productRepository.Update(product);

            await UnitOfWork(_productRepository.UnitOfWork);

            return Result.Ok();
        }
    }
}
