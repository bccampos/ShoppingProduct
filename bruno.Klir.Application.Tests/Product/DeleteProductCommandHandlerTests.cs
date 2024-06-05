﻿using bruno.Klir.Application.Product.Commands;
using bruno.Klir.Application.Product.Commands.Handlers;
using bruno.Klir.Domain.Common.Interfaces.Persistence;
using bruno.Klir.Domain.Product.ValueObjects;
using FluentAssertions;
using FluentResults;
using Moq;

namespace bruno.Klir.Application.Tests.Product
{
    public class DeleteProductCommandHandlerTests
    {
        [Fact]
        public async Task ShouldDeleteWithSuccess()
        {
            var product = new Domain.Aggregate.Product().Create("product_test", 10, 1, null);

            Moq.Mock<IProductRepository> mock = new Moq.Mock<IProductRepository>();
            mock.Setup(x => x.GetByIdAsync(It.IsAny<ProductId>())).ReturnsAsync(product);

            var command = new DeleteProductCommand(product.Id.Value);

            var handler = new DeleteProductCommandHandler(mock.Object);

            Result response = await handler.Handle(command, CancellationToken.None);

            response.Should().NotBeNull();
            response.IsSuccess.Should().BeTrue();
        }
    }
}
