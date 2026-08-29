using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Common.Interfaces;
using Web.Domain.BaseCategories;
using Web.Domain.MenuCategories;
using Web.Domain.Restaurants;
using Web.Infrastructure.Common.Persistence.Data;
using Web.Infrastructure.Seeding.DTO;

namespace Web.Infrastructure.Seeding
{
    public class SeedRepository : ISeedRepository
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _http;
        private readonly SeedSettings _settings;

        public SeedRepository(
            AppDbContext context,
            HttpClient http,
            IOptions<SeedSettings> settings)
        {
            _context = context;
            _http = http;
            _settings = settings.Value;
        }

        public async Task SeedAsync(
            string userId,
            CancellationToken cancellationToken)
        {
            //if (await _context.BaseCategories.AnyAsync(cancellationToken))
            //    return;

            var response = await _http.GetFromJsonAsync<CategoryResponse>(
                "https://www.themealdb.com/api/json/v1/1/categories.php",
                cancellationToken);

            if (response is null || response.Categories.Count == 0)
                return;

            var baseCategories = new List<BaseCategory>();

            foreach (var cat in response.Categories)
            {
                var baseCategory = new BaseCategory(
                    cat.strCategory,
                    cat.strCategoryDescription,
                    userId);

                baseCategories.Add(baseCategory);
            }

            var mealsCache = new Dictionary<string, List<MealDto>>();

            foreach (var category in baseCategories)
            {
                var mealsResponse =
                    await _http.GetFromJsonAsync<MealsResponse>(
                        $"https://www.themealdb.com/api/json/v1/1/filter.php?c={category.Name}",
                        cancellationToken);

                mealsCache[category.Name] =
                    mealsResponse?.Meals ?? [];
            }

            foreach (var seed in RestaurantSeedData.Restaurants)
            {
                var category = baseCategories[
                    Random.Shared.Next(baseCategories.Count)];

                var restaurant = new Restaurant(
                    seed.Name,
                    $"Welcome to {seed.Name}",
                    userId,
                    category.Id);

                restaurant.SetRating(
                    Math.Round(
                        (decimal)(Random.Shared.NextDouble() * 3 + 2),
                        1));

                restaurant.AddAddress(
                    seed.Lat,
                    seed.Lng,
                    seed.Address);

                restaurant.SetFreeDelivery(
                    Random.Shared.Next(2) == 1,
                    userId);

                restaurant.SetFastDelivery(
                    Random.Shared.Next(2) == 1,
                    userId);

                restaurant.SetOffers(
                    Random.Shared.Next(2) == 1,
                    userId);

                if (Random.Shared.Next(100) < 15)
                    restaurant.Close(userId);

                var menu = new MenuCategory(
                    "Main Menu",
                    "Default Menu",
                    restaurant.Id);

                var meals = mealsCache.GetValueOrDefault(
                    category.Name,
                    []);

                var selectedMeals = meals
                    .OrderBy(_ => Guid.NewGuid())
                    .Take(_settings.ProductsPerRestaurant)
                    .ToList();

                foreach (var meal in selectedMeals)
                {
                    var product = new Product(
                        meal.strMeal,
                        $"Delicious {meal.strMeal}",
                        new List<string>
                        {
                    meal.strMealThumb
                        },
                        Random.Shared.Next(50, 300),
                        menu.Id);

                    product.Rating = Math.Round(
                        (decimal)(Random.Shared.NextDouble() * 3 + 2),
                        1);

                    menu.AddProduct(product);
                }

                restaurant.AddMenuCategory(menu);

                category.AddRestaurant(restaurant);
            }

            _context.BaseCategories.AddRange(baseCategories);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public static class RestaurantSeedData
    {
        public static readonly List<(string Name, string Address, double Lat, double Lng)> Restaurants =
        [
            ("Koshary El Tahrir", "Tahrir Square", 30.0444, 31.2357),
        ("Pizza House", "Dokki", 30.0384, 31.2122),
        ("Burger Spot", "Mohandessin", 30.0561, 31.2005),
        ("Grill Master", "Nasr City", 30.0617, 31.3300),
        ("Chicken Hub", "Heliopolis", 30.0910, 31.3200),
        ("Sea Food King", "Maadi", 29.9602, 31.2569),
        ("Italian Corner", "Zamalek", 30.0626, 31.2197),
        ("Shawarma Station", "6 October", 29.9765, 30.9495),
        ("Taco House", "Sheikh Zayed", 30.0135, 30.9722),
        ("Oriental Taste", "Haram", 29.9923, 31.1512),

        ("Food Republic", "Faisal", 29.9997, 31.2056),
        ("BBQ Express", "Agouza", 30.0533, 31.2135),
        ("Sushi World", "New Cairo", 30.0284, 31.4913),
        ("Pasta Palace", "Rehab", 30.0638, 31.4897),
        ("Falafel Factory", "Shorouk", 30.1281, 31.6315),
        ("Steak Point", "Badr City", 30.1457, 31.7151),
        ("Hot Chicken", "Obour", 30.2288, 31.4765),
        ("Fresh Fish", "Qalyub", 30.1801, 31.2064),
        ("Royal Grill", "Shubra", 30.1104, 31.2459),
        ("Tasty Meals", "Rod El Farag", 30.0823, 31.2454),

        ("Quick Bites", "Ain Shams", 30.1295, 31.3190),
        ("Food Garden", "Mataria", 30.1216, 31.3136),
        ("Arabian Nights", "Mokattam", 30.0103, 31.2856),
        ("Golden Spoon", "Katameya", 30.0152, 31.4205),
        ("Happy Burger", "Madinaty", 30.0821, 31.6390),
        ("Fire Grill", "Mostakbal City", 30.1031, 31.7115),
        ("Spicy House", "New Capital", 30.0130, 31.8300),
        ("Healthy Bowl", "Garden City", 30.0388, 31.2296),
        ("Family Kitchen", "Manial", 30.0270, 31.2308),
        ("Taste Factory", "Helwan", 29.8414, 31.3008)
        ];
    }
}
    
