using Domain.Abstractions;
using FluentAssertions;
using FluentValidation;
using DomainProduct = Domain.Models.Product;
using FluentValidation.Results;
using Moq;
using Moq.Language.Flow;
using Products.Api.Models;

namespace Products.Api.Test.Controllers.ProductControllerTests
{
    internal class When_AddProduct_Is_Called : ProductControllerTest
    {
        private Product _product;

        [SetUp]
        public void SetUp()
        {
            _product = new Product()
            {
                Id = 1,
                Description = "description",
                Price = 10,
                Quantity = 1
            };
        }

        [Test]
        public async Task If_Validation_Fails_Then_An_Exception_Of_Type_ValidationException_Is_Thrown()
        {
            Func<Task> act = async () => { await _systemUnderTest.AddProduct(_product); };


            var validationResultWithErrors = CreateFakeValidationResultWithErrors();

            SetupValidateAsync(_product).ReturnsAsync(validationResultWithErrors);

            await act.Should().ThrowAsync<ValidationException>();

            ValidateAddProductAsyncWasNeverCalled();
        }

        [Test]
        public async Task If_ValidateAsync_Throws_An_Exception_Then_It_Is_Caught_And_Rethrown()
        {
            Func<Task> act = async () => { await _systemUnderTest.AddProduct(_product); };

            var exceptionToThrow = new Exception("Some exception to throw");

            SetupValidateAsync(_product).ThrowsAsync(exceptionToThrow);

            await act.Should().ThrowAsync<Exception>()
                .WithMessage(exceptionToThrow.Message);

            ValidateAddProductAsyncWasNeverCalled();
        }

        [Test]
        public async Task If_The_Mapper_Throws_An_Exception_Then_It_Is_Caught_And_Rethrown()
        {
            Func<Task> act = async () => { await _systemUnderTest.AddProduct(_product); };


            var successfulValidationResult = new ValidationResult();

            SetupValidateAsync(_product).ReturnsAsync(successfulValidationResult);

            var exceptionToThrow = new Exception("Some exception to throw");

            SetupMap(_product).Throws(exceptionToThrow);

            await act.Should().ThrowAsync<Exception>()
                .WithMessage(exceptionToThrow.Message);

            ValidateAddProductAsyncWasNeverCalled();
        }


        private ValidationResult CreateFakeValidationResultWithErrors()
        {
            var validationResult = new ValidationResult(
                new List<ValidationFailure>() 
                { 
                    new ValidationFailure("SomeProperty", "SomeError") 
                });

            return validationResult;
        }

        private void ValidateAddProductAsyncWasNeverCalled()
        {
            _mockProductInventory.Verify(x => x.AddProductAsync(It.IsAny<DomainProduct>()), Times.Never);
        }

        private ISetup<IValidator<Product>, Task<ValidationResult>> SetupValidateAsync(Product product)
        {
            return _mockProductValidator.Setup(x => x.ValidateAsync(product, It.IsAny<CancellationToken>()));
        }

        private ISetup<IMap<Product, DomainProduct>, DomainProduct> SetupMap(Product product)
        {
            return _mockApiToDomainMapper.Setup(x => x.Map(product));
        }

    }
}
