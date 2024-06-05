using FluentResults;
using MediatR;

namespace bruno.Klir.Application.Shopping.Queries
{
    public record GetShoppingByIdQuery(Guid Id) : IRequest<Result<ShoppingResult>>;
}
