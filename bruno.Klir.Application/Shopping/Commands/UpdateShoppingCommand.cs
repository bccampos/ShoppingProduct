using bruno.Klir.Domain.Product.Entities;
using FluentResults;
using MediatR;

namespace bruno.Klir.Application.Shopping.Commands
{
    public class UpdateShoppingCommand : IRequest<Result<ShoppingResult>>
    {
        public Guid Id { get; set; }
        public IEnumerable<ShoppingItem> Items { get; set; } = Enumerable.Empty<ShoppingItem>();

    }

}
