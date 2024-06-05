using bruno.Klir.Application.Common.Errors;
using bruno.Klir.Application.Product;
using bruno.Klir.Domain.Common.Interfaces.Persistence;
using bruno.Klir.Domain.Product.ValueObjects;
using bruno.Klir.Domain.Shopping.ValueObjects;
using FluentResults;
using MediatR;

namespace bruno.Klir.Application.Shopping.Queries.Handlers
{
    public class GetShoppingByIdQueryHandler : IRequestHandler<GetShoppingByIdQuery, Result<ShoppingResult>>
    {
        private readonly IShoppingGroupRepository _shoppingRepository;

        public GetShoppingByIdQueryHandler(IShoppingGroupRepository shoppingRepository)
        {
            _shoppingRepository = shoppingRepository;
        }

        public async Task<Result<ShoppingResult>> Handle(GetShoppingByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _shoppingRepository.GetByIdAsync(ShoppingGroupId.Parse(request.Id));

            if (result is null)
            {
                return Result.Fail<ShoppingResult>(new NotFoundError());
            }

            //TODO: Add mappear to Items
            return Result.Ok<ShoppingResult>(ShoppingResult.ToShoppingResult(result));
        }
    }
}
