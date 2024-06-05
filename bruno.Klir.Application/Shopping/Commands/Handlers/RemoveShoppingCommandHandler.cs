using bruno.Klir.Domain.Common.Command;
using bruno.Klir.Domain.Common.Interfaces.Persistence;
using bruno.Klir.Domain.Shopping.ValueObjects;
using FluentResults;
using MediatR;

namespace bruno.Klir.Application.Shopping.Commands.Handlers
{
    public class RemoveShoppingCommandHandler : CommandHandler, IRequestHandler<RemoveShoppingCommand, Result>
    {
        private readonly IShoppingGroupRepository _shoppingRepository;
        
        public RemoveShoppingCommandHandler(IShoppingGroupRepository shoppingRepository)
        {
            _shoppingRepository = shoppingRepository;
        }

        public async Task<Result> Handle(RemoveShoppingCommand request, CancellationToken cancellationToken)
        {
            var shoppingGroup = await _shoppingRepository.GetByIdAsync(ShoppingGroupId.Parse(request.Id));

            if (shoppingGroup is null)
            {
                return Result.Fail("Shopping is not found");
            }

            shoppingGroup.Clear();

            _shoppingRepository.Update(shoppingGroup);

            //await UnitOfWork(_shoppingRepository.UnitOfWork);

            return Result.Ok();
        }
    }
}
