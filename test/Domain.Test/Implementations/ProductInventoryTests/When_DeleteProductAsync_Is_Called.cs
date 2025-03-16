using Moq;
using Domain.Models;
using FluentAssertions;
using Domain.Exceptions;

namespace Domain.Test.Implementations.ProductInventoryTests
{
    internal class When_DeleteProductAsync_Is_Called : ProductInventoryTest
    {

        long _productId;

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


        }

        [Test]
        public void If_DeleteProductAsync_Throws_An_Exception_Then_It_Is_Caught_And_Rethrown()
        {

        }

        [Test]
        public void If_The_Product_Does_Exist_Then_It_Is_Deleted_Successfully()
        {

        }
      
        private static long GenerateProductId()
        {
            Random rnd = new Random();

            return rnd.Next();
        }

    }
}
