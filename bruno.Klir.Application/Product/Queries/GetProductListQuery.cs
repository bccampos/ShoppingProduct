using FluentResults;
using MediatR;

namespace bruno.Klir.Application.Product.Queries
{
    public record GetProductListQuery : IRequest<PagedResult<ProductFullResult>>;
}
