using bruno.Klir.Domain.Common.Command;
using bruno.Klir.Domain.Common.Interfaces;
using bruno.Klir.Domain.Common.Interfaces.Persistence;
using bruno.Klir.Domain.Models;
using bruno.Klir.Domain.Product.Entities;
using bruno.Klir.Domain.Product.ValueObjects;
using bruno.Klir.Domain.Shopping;
using bruno.Klir.Domain.Shopping.ValueObjects;
using MediatR;

namespace bruno.Klir.Application.Shopping.Commands.Handlers
{
    public class CreateShoppingCommandHandler : CommandHandler, IRequestHandler<CreateShoppingCommand, CommandResult<Guid>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IShoppingGroupRepository _shoppingRepository;
        private readonly IShoppingService _shoppingService;

        public CreateShoppingCommandHandler(IShoppingGroupRepository shoppingRepository, IShoppingService shoppingService)
        {
            _shoppingRepository = shoppingRepository;
            _shoppingService = shoppingService;
        }

        public async Task<CommandResult<Guid>> Handle(CreateShoppingCommand request, CancellationToken cancellationToken)
        {
            var shopping = new ShoppingGroup();
            shopping.Id = ShoppingGroupId.Parse(Guid.NewGuid());

            shopping.Items = request.Items.Select(i => new ShoppingItem
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                Price = i.Price,
                Total = i.Total,
                PromotionApplied = i.PromotionApplied
            }).ToList();

            _shoppingRepository.Add(shopping);

            await UnitOfWork(_shoppingRepository.UnitOfWork);

            return CommandResult<Guid>.Ok(shopping.Id.Value);  
        }
    }
}
