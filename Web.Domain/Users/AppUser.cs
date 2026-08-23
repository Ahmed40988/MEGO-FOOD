using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Web.Domain.Addresses;
using Web.Domain.RefreshTokens;
using Web.Domain.Restaurants;
using static System.Net.Mime.MediaTypeNames;

namespace Web.Domain.Users
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public string? PhotoURl { get; set; } = string.Empty;
        public DateTime Createdon { get; private set; } = DateTime.UtcNow;
        public bool Deleted { get; private set; }
        public string? UpdatedByid { get; private set; }
        public DateTime? Updatedon { get; private set; }

        public DateOnly DateOfBirth { get; set; }

        private readonly List<RefreshToken> _refreshTokens = new();
        public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

        private readonly List<Restaurant> _restaurant = new();
        public IReadOnlyCollection<Restaurant> Restaurant => _restaurant.AsReadOnly();

        private readonly List<UserAddress> _Addresses = new();
        public IReadOnlyCollection<UserAddress> Addresses => _Addresses.AsReadOnly();

        private void Touch(string updatedById)
        {
            UpdatedByid = updatedById;
            Updatedon = DateTime.UtcNow;
        }

        private void SoftDelete(string updatedById)
        {
            if (Deleted) return;

            Deleted = true;
            Touch(updatedById);
        }

        public void Restore(string updatedById)
        {
            if (!Deleted) return;

            Deleted = false;
            Touch(updatedById);
        }
        private AppUser() { }

        public AppUser(string email)
        {
            Email = email;
            UserName = email;
        }
        public void UpdateProfile(
    string fullName,
    string phone,
    DateOnly dateOfBirth)
        {
            FullName = fullName;
            PhoneNumber = phone;
            DateOfBirth = dateOfBirth;
        }

        public ErrorOr<Success> AddrefreshTokens(RefreshToken refreshToken)
        {
            if (_refreshTokens.Contains(refreshToken))
                return RefreshTokensErrors.DuplicatedRefreshToken;

            if (refreshToken is null)
                return RefreshTokensErrors.RefreshTokenIsnulll;

            _refreshTokens.Add(refreshToken);
            return Result.Success;
        }

        public ErrorOr<Success> AddRestaurant(Restaurant restaurant)
        {
            if (restaurant is null)
                return RestaurantsErrors.RestaurantisNull;

            if (_restaurant.Any(m => m.Name == restaurant.Name))
                return RestaurantsErrors.DuplicatedRestaurant;

            _restaurant.Add(restaurant);
            return Result.Success;
        }
        public ErrorOr<Success> DeleteRestaurant(Guid restaurantid)
        {
            if (restaurantid == Guid.Empty)
                return RestaurantsErrors.RestaurantIdisNull;

            var restaurant = _restaurant
                .FirstOrDefault(m => m.Id == restaurantid);

            if (restaurant is null)
                return RestaurantsErrors.RestaurantNotFound;

            _restaurant.Remove(restaurant);

            return Result.Success;
        }

        public void Delete(string updatedById)
        {
            SoftDelete(updatedById);
            _restaurant.Clear();
            _refreshTokens.Clear();
            _Addresses.Clear();
        }
        public bool IsProfileCompleted()
        {
            return !string.IsNullOrWhiteSpace(FullName)
                   && !string.IsNullOrWhiteSpace(PhoneNumber)
                   && DateOfBirth != default
                   && !string.IsNullOrWhiteSpace(PhotoURl);
        }
    }
}
