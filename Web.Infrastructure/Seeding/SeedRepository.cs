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

        public async Task SeedAsync(string userId, CancellationToken cancellationToken)
        {
            //if (await _context.BaseCategories.AnyAsync(cancellationToken))
            //    return;

            var response = await _http.GetFromJsonAsync<CategoryResponse>(
                "https://www.themealdb.com/api/json/v1/1/categories.php",
                cancellationToken);

            foreach (var cat in response.Categories)
            {
                var baseCategory = new BaseCategory(
                    cat.strCategory,
                    cat.strCategoryDescription,
                    userId);

                var mealsResponse = await _http.GetFromJsonAsync<MealsResponse>(
                    $"https://www.themealdb.com/api/json/v1/1/filter.php?c={cat.strCategory}",
                    cancellationToken);

                var meals = mealsResponse?.Meals ?? new List<MealDto>();

                if (!meals.Any()) continue;

                var expandedMeals = SeedHelper.ExpandMeals(
                    meals,
                    _settings.ProductsPerRestaurant,
                    _settings.RestaurantsPerCategory);

                for (int i = 1; i <= _settings.RestaurantsPerCategory; i++)
                {
                    var restaurant = new Restaurant(
                        SeedHelper.GenerateRestaurantName(cat.strCategory, i),
                        "Auto generated restaurant",
                        userId,
                        baseCategory.Id);

                    var menu = new MenuCategory(
                        "Main Menu",
                        "Default Menu",
                        restaurant.Id);

                    var restaurantMeals = expandedMeals
                        .Skip((i - 1) * _settings.ProductsPerRestaurant)
                        .Take(_settings.ProductsPerRestaurant)
                        .ToList();

                    foreach (var meal in restaurantMeals)
                    {
                        var product = new Product(
                            meal.strMeal,
                            "",
                            meal.strMealThumb,
                            Random.Shared.Next(50, 200),
                            menu.Id);

                        product.Rating = SeedHelper.GenerateRating();

                        menu.AddProduct(product);
                    }

                    restaurant.AddMenuCategory(menu);
                    baseCategory.AddRestaurant(restaurant);
                }

                _context.BaseCategories.Add(baseCategory);
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
