using bruno.Klir.Application.Common.Errors;
using bruno.Klir.Application.Product.Commands;
using bruno.Klir.Application.Product.Commands.Handlers;
using bruno.Klir.Domain.Common.Interfaces.Persistence;
using bruno.Klir.Domain.Exceptions;
using bruno.Klir.Domain.Product.ValueObjects;
using Castle.Components.DictionaryAdapter;
using FluentAssertions;
using FluentAssertions.Equivalency;
using FluentResults;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;
using System;

namespace bruno.Klir.Application.Tests.Product
{
    public class UpdateProductCommandHandlerTests
    {
        [Fact]
        public async Task ShouldUpdateWithSuccess()
        {
            var product = new Domain.Aggregate.Product().Create("product_test", 10, 1, null);

            Moq.Mock<IProductRepository> mock = new Moq.Mock<IProductRepository>();
            mock.Setup(x => x.GetByIdAsync(It.IsAny<ProductId>())).ReturnsAsync(product);

            var command = new UpdateProductCommand(product.Id.Value, "product_test2", 5);

            var handler = new UpdateProductCommandHandler(mock.Object);

            Result response = await handler.Handle(command, CancellationToken.None);

            response.Should().NotBeNull();
            response.IsSuccess.Should().BeTrue();
        }


        [Fact]
        public async Task IfProductDoesNotExist_ShouldThrowException()
        {
            var product = new Domain.Aggregate.Product().Create("product_test", 10, 1, null);

            var command = new UpdateProductCommand(product.Id.Value, "product_test2", 5);

            Moq.Mock<IProductRepository> mock = new Moq.Mock<IProductRepository>();
            var handler = new UpdateProductCommandHandler(mock.Object);

            Result response = await handler.Handle(command, CancellationToken.None);

            response.Should().NotBeNull();
            response.IsSuccess.Should().BeFalse();
            response.IsFailed.Should().BeTrue();

            Assert.IsType<NotFoundError>(response.Reasons.First());

        }
    }
}
