using FluentResults;
using MediatR;

namespace bruno.Klir.Application.Shopping.Commands
{
    public record RemoveShoppingCommand(
    Guid Id
    ) : IRequest<Result>;
}
