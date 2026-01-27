using ErrorOr;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Addresses;
using Web.Domain.MenuCategories;
using Web.Domain.RefreshTokens;
using Web.Domain.Restaurants;

namespace Web.Domain.Users
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public Gender gender { get; set; }
        public string? PhotoURl { get; set; } = string.Empty;

        public DateOnly DateOfBirth { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; } = [];
        public ICollection<Address> Addresses { get; set; } = [];


        private AppUser() { }

        public AppUser( string email)
        {
            Email =email;   
            UserName = email;
        }

        private readonly List<Restaurant> _restaurant = new();
        public IReadOnlyCollection<Restaurant> Restaurant=> _restaurant.AsReadOnly();


        public ErrorOr<Success> AddRestaurant(Restaurant restaurantCategory)
        {
            if (restaurantCategory is null)
                return RestaurantsErrors.RestaurantisNull;

            if (_restaurant.Any(m => m.Name == restaurantCategory.Name))
                return RestaurantsErrors.DuplicatedMenuCategory;

            _restaurant.Add(restaurantCategory);
            return Result.Success;
        }
        public ErrorOr<Success> DeleteRestaurant(Guid restaurantCategoryId)
        {
            if (restaurantCategoryId == Guid.Empty)
                return RestaurantsErrors.MenuCategoryisNull;

            var restaurantCategory = _restaurant
                .FirstOrDefault(m => m.Id == restaurantCategoryId);

            if (restaurantCategory is null)
                return RestaurantsErrors.MenuCategoryNotFound;

            _restaurant.Remove(restaurantCategory);

            return Result.Success;
        }

    }
}
