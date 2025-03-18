using Domain.Abstractions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using Products.Api.Controllers;
using Products.Api.Models;
using DomainProduct = Domain.Models.Product;

namespace Products.Api.Test.Controllers.ProductControllerTests
{
    internal abstract class ProductControllerTest
    {

        protected ProductController _systemUnderTest;

        protected Mock<ILogger<ProductController>> _mockLogger;

        protected Mock<IValidator<Product>> _mockProductValidator;

        protected Mock<IProductInventory> _mockProductInventory;

        protected Mock<IMap<Product, DomainProduct>> _mockApiToDomainMapper;

        protected Mock<IMap<DomainProduct, Product>> _mockDomainToApiProductMapper;

        [SetUp]
        public void Setup()
        {
            _mockLogger = new Mock<ILogger<ProductController>>();
            _mockProductValidator = new Mock<IValidator<Product>>();
            _mockProductInventory = new Mock<IProductInventory>();
            _mockApiToDomainMapper = new Mock<IMap<Product, DomainProduct>>();
            _mockDomainToApiProductMapper = new Mock<IMap<DomainProduct, Product>>();

            _systemUnderTest = new ProductController(_mockLogger.Object, _mockProductInventory.Object, _mockApiToDomainMapper.Object, _mockDomainToApiProductMapper.Object, _mockProductValidator.Object);
        }

    }
}
