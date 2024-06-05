using bruno.Klir.Application.Shopping;
using FluentResults;
using MediatR;

namespace bruno.Klir.Application.Product.Queries
{
    public record GetShoppingListQuery : IRequest<Result<List<ShoppingResult>>>;
}
