using FluentResults;
using MediatR;

namespace bruno.Klir.Application.Shopping.Commands
{
    public record CalculateShoppingCommand(
        Guid ShoppingId
        ) : IRequest<Result<ShoppingResult>>;
}
