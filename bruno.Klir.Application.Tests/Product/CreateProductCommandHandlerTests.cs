using bruno.Klir.Application.Product.Commands;
using bruno.Klir.Application.Product.Commands.Handlers;
using bruno.Klir.Domain.Common.Interfaces.Persistence;
using FluentAssertions;
using FluentResults;
using NSubstitute;

namespace bruno.Klir.Application.Tests.Product
{
    public class CreateProductCommandHandlerTests
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandlerTests()
        {
            _productRepository = Substitute.For<IProductRepository>();
        }

        [Fact]
        public async Task ShouldCreateWithSuccess()
        {
            var command = new CreateProductCommand("product_test", 10, 1, null);

            var handler = new CreateProductCommandHandler(_productRepository);

            Result response = await handler.Handle(command, CancellationToken.None);

            response.Should().NotBeNull();
            response.IsSuccess.Should().BeTrue();
        }


    }
}
