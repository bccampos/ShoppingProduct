
using bruno.Klir.Application.Shopping;
using bruno.Klir.Domain.Common.Interfaces.Persistence;
using FluentResults;
using MediatR;

namespace bruno.Klir.Application.Product.Queries.Handlers
{
    public class GetShoppingListQueryHandler : IRequestHandler<GetShoppingListQuery, Result<List<ShoppingResult>>>
    {
        private readonly IShoppingGroupRepository _shoppingRepository;
        private readonly IProductRepository _productRepository;

        public GetShoppingListQueryHandler(IShoppingGroupRepository shoppingRepository, IProductRepository productRepository) 
        {
            _shoppingRepository = shoppingRepository;
            _productRepository = productRepository;
        }

        public async Task<Result<List<ShoppingResult>>> Handle(GetShoppingListQuery request, CancellationToken cancellationToken)
        {
            var result = await _shoppingRepository.GetAllAsync();

            foreach (var shopping in result)
            {
                foreach (var item in shopping.Items) 
                {
                    var product = await _productRepository.GetByIdAsync(item.ProductId);
                    item.Product = product;
                }                
            }
            return Result.Ok<List<ShoppingResult>>(result.Select(x => ShoppingResult.ToShoppingResult(x)).ToList());
        }
    }
}
