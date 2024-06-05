using FluentResults;
using MediatR;

namespace bruno.Klir.Application.Product.Commands
{
    public record RemoveProductPromotionCommand(
        Guid ProductId
        ) : IRequest<Result>;
}
