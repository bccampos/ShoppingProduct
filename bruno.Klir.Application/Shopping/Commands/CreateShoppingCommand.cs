using bruno.Klir.Domain.Product.Entities;
using FluentResults;
using MediatR;

namespace bruno.Klir.Application.Shopping.Commands
{
    public class CreateShoppingCommand : IRequest<CommandResult<Guid>>
    {
        public IEnumerable<ShoppingItem> Items { get; set; } = Enumerable.Empty<ShoppingItem>();

    }

}
