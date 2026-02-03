namespace Web.Application.Adresses.AddressDTO.validators
{
    public class AdressRequestDtoValidators : AbstractValidator<AddressRequestDto>
    {
        public AdressRequestDtoValidators()
        {
            RuleFor(x => x.Street)
                .NotEmpty().WithMessage("Street address is required.")
                .MaximumLength(200).WithMessage("Street address must not exceed 200 characters.");

            RuleFor(x => x.City)
                .NotEmpty()
                .WithMessage("City is required.")
                .MaximumLength(100).WithMessage("City must not exceed 100 characters.");

            RuleFor(x => x.State)
                .MaximumLength(100)
                .WithMessage("State name must not exceed 100 characters.");

            RuleFor(x => x.PostalCode)
                .Matches(@"^\d{4,10}$").WithMessage("Postal code must contain only numbers and be between 4–10 digits.");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required.")
                .MaximumLength(100).WithMessage("Country name must not exceed 100 characters.");

        }
    }
}
