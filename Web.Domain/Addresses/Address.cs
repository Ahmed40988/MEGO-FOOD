using Web.Domain.BaseModels;
using Web.Domain.Users;

namespace Web.Domain.Addresses
{
    public class Address : BaseModel
    {
        public int Id { get; private set; }

        public string Street { get; private set; } = null!;
        public string City { get; private set; } = null!;
        public string State { get; private set; } = null!;
        public string PostalCode { get; private set; } = null!;
        public string Country { get; private set; } = null!;

        public string UserId { get; private set; } = string.Empty;
        public AppUser User { get; private set; } = null!;

        private Address() { }

        private Address(
            string street,
            string city,
            string state,
            string postalCode,
            string country,
            string userId)
        {
            Street = Validate(street, nameof(Street));
            City = Validate(city, nameof(City));
            State = Validate(state, nameof(State));
            PostalCode = Validate(postalCode, nameof(PostalCode));
            Country = Validate(country, nameof(Country));
            UserId = Validate(userId, nameof(UserId));
        }

        public static Address Create(
            string street,
            string city,
            string state,
            string postalCode,
            string country,
            string userId)
        {
            return new Address(street, city, state, postalCode, country, userId);
        }

        public void Update(
            string street,
            string city,
            string state,
            string postalCode,
            string country)
        {
            Street = Validate(street, nameof(Street));
            City = Validate(city, nameof(City));
            State = Validate(state, nameof(State));
            PostalCode = Validate(postalCode, nameof(PostalCode));
            Country = Validate(country, nameof(Country));
        }

        private static string Validate(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{fieldName} cannot be empty.");

            return value.Trim();
        }
    }
}
