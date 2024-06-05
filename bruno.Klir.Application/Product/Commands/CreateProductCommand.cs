using FluentResults;
using MediatR;

namespace bruno.Klir.Application.Product.Commands
{
    public record CreateProductCommand(
        string Name,
        decimal Price,
        int Quantity,
        Guid? PromotionId
        ) : IRequest<Result>;
}
