using OnlineDeliverySystem.Models;

namespace OnlineDeliverySystem
{
    public class AddressValidator : IAddressValidator
    {
        public string GetDeliveryProvider(DeliveryAddressModel address)
        {
            var _parcelAreas = new List<string> { "Croydon", "Thornton Heath", "Dartford" }; 
            return _parcelAreas.Contains(address.Area) ? "Panda Parcels" : "Royal Mail";
        }

        public AddressValidationResult ValidateAddress(DeliveryAddressModel address)
        {
            var _restrictedAreas = new List<string> { "Cornwall", "Exeter", "Watford", "Scotland" };
            if (_restrictedAreas.Any(restricted => restricted.Equals(address.Area) || restricted.Equals(address.Address)))
            {
                if (address.Address == "Scotland")
                {
                    return new AddressValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = $"No postal service available to deliver to {address.Address}"
                    };

                }
                return new AddressValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "No postal service available to cover the area"
                };
            }
            return new AddressValidationResult { IsValid = true };
        }
    }
}
