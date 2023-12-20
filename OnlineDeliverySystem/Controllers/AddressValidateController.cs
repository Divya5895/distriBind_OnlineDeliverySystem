using Microsoft.AspNetCore.Mvc;
using OnlineDeliverySystem.Models;

namespace OnlineDeliverySystem.Controllers
{
    [ApiController]
    [Route("api/address")]
    public class AddressValidateController : ControllerBase
    {
        private readonly IAddressValidator _addressValidator;

        public AddressValidateController(IAddressValidator addressValidator)
        {
            _addressValidator = addressValidator;
        }

        [HttpPost("validateAddress")]
        public IActionResult ValidateAddress([FromBody] DeliveryAddressModel request)
        {
            if (request == null || string.IsNullOrEmpty(request.Area) || string.IsNullOrEmpty(request.Address))
            {
                return BadRequest("Delivery address is required.");
            }

            var result = _addressValidator.ValidateAddress(request);

            if (!result.IsValid)
            {
                return BadRequest(result.ErrorMessage);

            }
            return Ok();
        }

        [HttpPost("GetDeliveryProvider")]
        public IActionResult DeliveryProviders([FromBody] DeliveryAddressModel request)
        {
            var result = _addressValidator.GetDeliveryProvider(request);
            return Ok(result);

        }
    }
}