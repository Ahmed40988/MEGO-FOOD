using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Infrastructure.Seeding.DTO
{
    public class CategoryResponse
    {
        public List<CategoryDto> Categories { get; set; } = new();
    }

    public class CategoryDto
    {
        public string idCategory { get; set; } = string.Empty;
        public string strCategory { get; set; } = string.Empty;
        public string strCategoryThumb { get; set; } = string.Empty;
        public string strCategoryDescription { get; set; } = string.Empty;
    }
    public class MealsResponse
    {
        public List<MealDto> Meals { get; set; } = new();
    }

    public class MealDto
    {
        public string idMeal { get; set; } = string.Empty;
        public string strMeal { get; set; } = string.Empty;
        public string strMealThumb { get; set; } = string.Empty;
    }
    public class MealDetailsResponse
    {
        public List<MealDetailsDto> Meals { get; set; } = new();
    }

    public class MealDetailsDto
    {
        public string idMeal { get; set; } = string.Empty;
        public string strMeal { get; set; } = string.Empty;
        public string strInstructions { get; set; } = string.Empty;
        public string strMealThumb { get; set; } = string.Empty;
    }
}
