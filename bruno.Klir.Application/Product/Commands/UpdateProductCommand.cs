using FluentResults;
using MediatR;

namespace bruno.Klir.Application.Product.Commands
{
    public record UpdateProductCommand(
        Guid Id,
        string Name,
        decimal Price
        ) : IRequest<Result>;
}
