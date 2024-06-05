using bruno.Klir.Application.Common.Errors;
using bruno.Klir.Domain.Common.Interfaces.Persistence;
using bruno.Klir.Domain.Exceptions;
using bruno.Klir.Domain.Product.ValueObjects;
using FluentResults;
using MediatR;

namespace bruno.Klir.Application.Product.Commands.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(ProductId.Parse(request.Id));

            if (product is null)
            {
                return Result.Fail(new NotFoundError());
            }

            product.Update(request.Name, request.Price);

            _productRepository.Update(product);

            return Result.Ok();
        }
    }
}
