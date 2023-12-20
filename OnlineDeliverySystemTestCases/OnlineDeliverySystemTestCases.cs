using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using OnlineDeliverySystem.Controllers;
using OnlineDeliverySystem.Models;
using OnlineDeliverySystem;
using Moq;

namespace OnlineDeliverySystemTestCases
{
    [TestFixture]
    public class Tests
    {
        private AddressValidateController _controller;
        private Mock<IAddressValidator> _addressValidatorMock;

        [SetUp]
        public void SetUp()
        {
            _addressValidatorMock = new Mock<IAddressValidator>();
            _controller = new AddressValidateController(_addressValidatorMock.Object);
        }

        [Test]
        public void ValidateAddress_WithValidAddress_ShouldReturnOk()
        {
            // Arrange
            var request = new DeliveryAddressModel { Area = "London", Address = "London" };
            _addressValidatorMock.Setup(x => x.ValidateAddress(request)).Returns(new AddressValidationResult { IsValid = true });

            // Act
            var result = _controller.ValidateAddress(request);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }
        [Test]
        public void ValidateAddress_WithInvalidAddress_ShouldReturnBadRequest()
        {
            // Arrange
            var request = new DeliveryAddressModel { Area = "Watford", Address = "Scotland" };
            _addressValidatorMock.Setup(x => x.ValidateAddress(request)).Returns(new AddressValidationResult { IsValid = false, ErrorMessage = "Invalid address" });

            // Act
            var result = _controller.ValidateAddress(request);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void DeliveryProviders_WithValidAddress_ShouldReturnOk()
        {
            // Arrange
            var request = new DeliveryAddressModel { Area = "Croydon", Address = "Croydon" };
            _addressValidatorMock.Setup(x => x.GetDeliveryProvider(request)).Returns("Panda Parcels");

            // Act
            var result = _controller.DeliveryProviders(request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void DeliveryProviders_WithInvalidAddress_ShouldReturnOk()
        {
            // Arrange
            var request = new DeliveryAddressModel { Area = "Coventry", Address = "Coventry" };
            _addressValidatorMock.Setup(x => x.GetDeliveryProvider(request)).Returns("Royal Mail");

            // Act
            var result = _controller.DeliveryProviders(request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}