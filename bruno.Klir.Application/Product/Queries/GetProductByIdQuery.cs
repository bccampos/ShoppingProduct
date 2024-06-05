using FluentResults;
using MediatR;

namespace bruno.Klir.Application.Product.Queries
{
    public record GetProductByIdQuery(Guid Id) : IRequest<Result<ProductResult>>;
}
