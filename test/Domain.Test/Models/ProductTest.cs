using Domain.Models;
using FluentAssertions;
using FluentAssertions.Execution;

namespace Domain.Test.Models
{
    internal class ProductTest
    {
        private long _productId;

        private string _description;

        private decimal _price;

        private int _quantity;

        [SetUp]
        public void SetUp()
        {
            _productId = 123;
            _description = "Some Description";
            _price = 10;
            _quantity = 5;

        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-3)]
        [TestCase(-5)]
        public void If_ProductId_Is_Less_Than_Or_Equal_To_0_Then_An_Exception_Of_Type_ArgumentOutOfRangeException_Is_Thrown(long invalidProductId)
        {
            _productId = invalidProductId;

            Action act = () => new Product(_productId, "Some Description", _price, _quantity);

            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("The product's Id must be larger than 0. (Parameter 'productId')");
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-3)]
        [TestCase(-5)]
        public void If_Price_Is_Less_Than_Or_Equal_To_0_Then_An_Exception_Of_Type_ArgumentOutOfRangeException_Is_Thrown(decimal invalidPrice)
        {
            _price = invalidPrice;

            Action act = () => new Product(_productId, "Some Description", _price, _quantity);

            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("The product's current price must be larger than 0. (Parameter 'price')");
        }

        [Test]
        [TestCase(-1)]
        [TestCase(-3)]
        [TestCase(-5)]
        public void If_Quantity_Is_Less_Than_To_0_Then_An_Exception_Of_Type_ArgumentOutOfRangeException_Is_Thrown(int invalidQuantity)
        {
            _quantity = invalidQuantity;

            Action act = () => new Product(_productId, "Some Description", _price, _quantity);

            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("The product's quantity cannot be negative. (Parameter 'quantity')");
        }

        [Test]
        public void Then_Valid_Values_Are_Passed_And_Assigned_To_The_Model()
        {
            var product = new Product(_productId, "Some Description", _price, _quantity);

            using (var scope = new AssertionScope())
            {
                product.Should().BeOfType<Product>();

                product.Id.Should().Be(_productId);
                product.Description.Should().Be(_description);
                product.Price.Should().Be(_price);
                product.Quantity.Should().Be(_quantity);
            }
        }
    }
}
