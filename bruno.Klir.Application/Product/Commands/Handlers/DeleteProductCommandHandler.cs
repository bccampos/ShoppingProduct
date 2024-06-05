using bruno.Klir.Application.Common.Errors;
using bruno.Klir.Domain.Common.Command;
using bruno.Klir.Domain.Common.Interfaces.Persistence;
using bruno.Klir.Domain.Product.ValueObjects;
using FluentResults;
using MediatR;

namespace bruno.Klir.Application.Product.Commands.Handlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(ProductId.Parse(request.Id));

            if (product is null)
            {
                return Result.Fail(new NotFoundError());
            }
            
            _productRepository.Delete(product);

            return Result.Ok();

        }
    }
}
