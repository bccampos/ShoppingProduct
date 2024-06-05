using FluentResults;
using MediatR;

namespace bruno.Klir.Application.Product.Commands
{
    public record DeleteProductCommand(
        Guid Id
        ) : IRequest<Result>;
}
