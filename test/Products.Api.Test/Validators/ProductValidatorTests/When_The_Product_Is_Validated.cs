using FluentAssertions;
using Products.Api.Models;
using Products.Api.Validators;

namespace Products.Api.Test.Validators.ProductValidatorTests
{
    internal class When_The_Product_Is_Validated 
    {
        private ProductValidator _systemUnderTest;

        private Product _product;

        [SetUp]
        public void SetUp()
        {
            _systemUnderTest = new ProductValidator();

            _product = new Product()
            {
                Id = 1,
                Description = "description",
                Price = 10,
                Quantity = 1
            };
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-3)]
        [TestCase(-5)]
        public async Task If_The_Product_Id_Equal_To_Or_Less_Than_0_Then_Validation_Fails(int productId)
        {
            _product.Id = productId;

            var validationResponse = await _systemUnderTest.ValidateAsync(_product);

            validationResponse.IsValid.Should().BeFalse();
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-3)]
        [TestCase(-5)]
        public async Task If_The_Price_Is_Equal_To_Or_Less_Than_0_Then_Validation_Fails(decimal price)
        {
            _product.Price = price;

            var validationResponse = await _systemUnderTest.ValidateAsync(_product);

            validationResponse.IsValid.Should().BeFalse();
        }

        [Test]
        [TestCase(-1)]
        [TestCase(-3)]
        [TestCase(-5)]
        public async Task If_The_Quantity_Is_Negative_Then_Validation_Fails(int quantity)
        {
            _product.Quantity = quantity;

            var validationResponse = await _systemUnderTest.ValidateAsync(_product);

            validationResponse.IsValid.Should().BeFalse();
        }

        [Test]
        public async Task If_Product_Is_Valid_Then_Validation_Passes()
        {
            var validationResponse = await _systemUnderTest.ValidateAsync(_product);

            validationResponse.IsValid.Should().BeTrue();
        }

    }
}
