using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Infrastructure.Seeding.DTO;

namespace Web.Infrastructure.Seeding
{
    public static class SeedHelper
    {
        public static decimal GenerateRating()
        {
            return Math.Round((decimal)(Random.Shared.NextDouble() * 2 + 3), 1);
        }

        public static string GenerateRestaurantName(string category, int index)
        {
            var suffixes = new[]
            {
            "House", "Grill", "Kitchen", "Express", "Corner",
            "Spot", "Hub", "Bistro", "Place", "Zone"
        };

            var randomSuffix = suffixes[Random.Shared.Next(suffixes.Length)];

            return $"{category} {randomSuffix} {index}";
        }

        public static List<MealDto> ExpandMeals(
            List<MealDto> meals,
            int requiredPerRestaurant,
            int restaurantsCount)
        {
            var result = new List<MealDto>();

            while (result.Count < requiredPerRestaurant * restaurantsCount)
            {
                result.AddRange(meals);
            }

            return result;
        }
    }
}
