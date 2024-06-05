using bruno.Klir.Domain.Common.Command;
using bruno.Klir.Domain.Common.Interfaces.Persistence;
using bruno.Klir.Domain.Product.ValueObjects;
using FluentResults;
using MediatR;

namespace bruno.Klir.Application.Product.Commands.Handlers
{
    public class CreateProductCommandHandler : CommandHandler, IRequestHandler<CreateProductCommand, Result>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Domain.Aggregate.Product().Create(request.Name, request.Price, request.Quantity, PromotionId.Parse(request.PromotionId));

            _productRepository.Add(product);

            await UnitOfWork(_productRepository.UnitOfWork);

            return Result.Ok();
        }
    }
}
