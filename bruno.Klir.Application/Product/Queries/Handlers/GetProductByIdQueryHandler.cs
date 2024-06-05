using bruno.Klir.Application.Common.Errors;
using bruno.Klir.Domain.Common.Interfaces.Persistence;
using bruno.Klir.Domain.Exceptions;
using bruno.Klir.Domain.Product.ValueObjects;
using FluentResults;
using MediatR;

namespace bruno.Klir.Application.Product.Queries.Handlers
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductResult>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<ProductResult>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _productRepository.GetByIdAsync(ProductId.Parse(request.Id));

            if (result is null)
            {
                return Result.Fail<ProductResult>(new NotFoundError());
            }

            return new ProductResult(result.Id.Value, result.PromotionId?.Value, result.Name, result.Price, result.Quantity);
        }
    }
}
