using Domain.Abstractions.Repositories;
using Domain.Implementations;
using Microsoft.Extensions.Logging;
using Moq;

namespace Domain.Test.Implementations.ProductInventoryTests
{
    internal abstract class ProductInventoryTest
    {
        protected ProductInventory _systemUnderTest;

        protected Mock<IReadProducts> _mockProductReader;

        protected Mock<IWriteProducts> _mockProductWriter;

        protected Mock<IRemoveProducts> _mockProductRemover;

        protected Mock<ILogger<ProductInventory>> _mockLogger;

        [SetUp]
        public void SetUp()
        {

            _mockProductReader = new Mock<IReadProducts>();
            _mockProductWriter = new Mock<IWriteProducts>();
            _mockProductRemover = new Mock<IRemoveProducts>();
            _mockLogger = new Mock<ILogger<ProductInventory>>();

            _systemUnderTest = new ProductInventory(_mockProductReader.Object, 
                _mockProductWriter.Object, 
                _mockProductRemover.Object, 
                _mockLogger.Object);
        }

    }
}
