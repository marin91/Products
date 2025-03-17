using Moq;
using Domain.Models;
using FluentAssertions;
using Domain.Exceptions;
using Domain.Abstractions.Repositories;
using Moq.Language.Flow;

namespace Domain.Test.Implementations.ProductInventoryTests
{
    internal class When_DeleteProductAsync_Is_Called : ProductInventoryTest
    {

        private long _productId;

        [SetUp]
        public void Setup()
        {
            _productId = GenerateProductId();
        }

        [Test]
        public async Task If_The_Product_Does_Not_Exist_Then_A_ProductDoesNotExistException_Is_Thrown()
        {
            Product product = null!;

            SetupGetStoreProductByIdAsync(_productId).ReturnsAsync(product);

            Func<Task> act = async () => { await _systemUnderTest.DeleteProductAsync(_productId); };

            await act.Should().ThrowAsync<ProductDoesNotExistException>()
                .WithMessage($"The product with {_productId} does not exist in the system.");

            _mockProductRemover.Verify(x => x.DeleteProductAsync(It.IsAny<long>()), Times.Never);
        }

        [Test]
        public async Task If_DeleteProductAsync_Throws_An_Exception_Then_It_Is_Caught_And_Rethrown()
        {
            var product = CreateFakeProduct(_productId);

            SetupGetStoreProductByIdAsync(_productId).ReturnsAsync(product);


            var someSqlDownstreamException = new Exception("Something occurred while removing the product in the data store.");

            SetupDeleteProductAsync(_productId).ThrowsAsync(someSqlDownstreamException);

            Func<Task> act = async () => { await _systemUnderTest.DeleteProductAsync(_productId); };

            await act.Should().ThrowAsync<Exception>()
                .WithMessage(someSqlDownstreamException.Message);
        }

        [Test]
        public void If_The_Product_Does_Exist_Then_It_Is_Deleted_Successfully()
        {
            var product = CreateFakeProduct(_productId);

            SetupGetStoreProductByIdAsync(_productId).ReturnsAsync(product);

            Func<Task> act = async () => { await _systemUnderTest.DeleteProductAsync(_productId); };

            act.Should().NotThrowAsync();

            _mockProductRemover.Verify(x => x.DeleteProductAsync(_productId), Times.Once());
        }
      
        private static long GenerateProductId()
        {
            Random rnd = new Random();

            return rnd.Next(10000);
        }

        private static int GenerateProductQty()
        {
            Random rnd = new Random(1000);

            return rnd.Next();
        }

        private static decimal GenerateProductPrice()
        {
            Random rnd = new Random();

            return rnd.Next(500);
        }

        private static Product CreateFakeProduct(long productId)
        {
            return new Product(productId, $"Some Description for {productId}", GenerateProductPrice(), GenerateProductQty());
        }

        protected ISetup<IRemoveProducts, Task> SetupDeleteProductAsync(long productId)
        {
            return _mockProductRemover.Setup(x => x.DeleteProductAsync(productId));
        }

    }
}
