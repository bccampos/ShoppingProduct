using bruno.Klir.Application.Common.Errors;
using bruno.Klir.Domain.Common.Interfaces.Persistence;
using bruno.Klir.Domain.Product.ValueObjects;
using FluentResults;
using MediatR;

namespace bruno.Klir.Application.Product.Commands.Handlers
{
    public class SetProductPromotionCommandHandler : IRequestHandler<SetProductPromotionCommand, Result>
    {
        private readonly IProductRepository _productRepository;

        public SetProductPromotionCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result> Handle(SetProductPromotionCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(ProductId.Parse(request.ProductId));

            if (product is null)
            {
                return Result.Fail(new NotFoundError());
            } 
            
            //TODO: Validate if promotionId exit
            
            product.SetPromotion(request.PromotionId);

            _productRepository.Update(product);

            return Result.Ok();
        }
    }
}
