using OnlineDeliverySystem.Models;

namespace OnlineDeliverySystem
{
    public interface IAddressValidator
    {
        AddressValidationResult ValidateAddress(DeliveryAddressModel address);

        string GetDeliveryProvider(DeliveryAddressModel address);
    }
}
