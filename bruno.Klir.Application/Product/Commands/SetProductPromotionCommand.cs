using FluentResults;
using MediatR;

namespace bruno.Klir.Application.Product.Commands
{
    public record SetProductPromotionCommand(
        Guid ProductId,
        Guid PromotionId
        ) : IRequest<Result>;
}
