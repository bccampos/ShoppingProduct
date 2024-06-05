
using bruno.Klir.Domain.Common;
using bruno.Klir.Domain.Common.Interfaces.Persistence;
using bruno.Klir.Domain.Product.Entities;
using MediatR;

namespace bruno.Klir.Application.Product.Queries.Handlers
{
    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, PagedResult<ProductFullResult>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductListQueryHandler(IProductRepository productRepository) 
        {
            _productRepository = productRepository;
        }
        
        public async Task<PagedResult<ProductFullResult>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var pagedResult = new PagedResult<ProductFullResult>
            {
                PageIndex = 1,
                PageSize = 100,
            };

            var result = await _productRepository.GetAllAsync();

            pagedResult.List = result.Select(x => MapToProductFullResult(x)).ToList();

            pagedResult.TotalResults = result.Count();

            return pagedResult;
        }

        private ProductFullResult MapToProductFullResult(Domain.Aggregate.Product result)
        {
            return new ProductFullResult
            {
                Id = result.Id.Value,
                Name = result.Name,
                Price = result.Price,
                IsActive = result.IsActive,
                PromotionId = result.Promotion != null ? result.PromotionId.Value : null,
                Promotion = result.Promotion == null ? null : new PromotionFullResult 
                {
                    Id = result.PromotionId.Value,
                    Name = result.Promotion.Name,
                    Quantity = result.Promotion.Quantity,
                    Type = result.Promotion.DiscountType,
                    IsActive = result.Promotion.IsPromotionActive
                },
            };
        }
    }
}
