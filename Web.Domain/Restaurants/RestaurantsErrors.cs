using ErrorOr;

namespace Web.Domain.Restaurants
{
    public static class RestaurantsErrors
    {
        public static readonly Error CannotAssignUserforthisIdAllows = Error.NotFound(
            code: "User not Found!",
            description: "User with this ID is  not Found!");

        public static readonly Error RestaurantNotFound = Error.NotFound(
            code: " Restaurant not Found!",
            description: " Restaurant with this ID is  not Found!");

        public static readonly Error RestaurantisNull = Error.Validation(
            code: "Restaurant  is Null",
            description: "Restaurant is Null!");
        public static readonly Error RestaurantIdisNull = Error.Validation(
            code: "RestaurantId  is Null",
            description: "Restaurant Id is Null!");


        public static readonly Error DuplicatedRestaurant = Error.Conflict(
            code: " Restaurant is Duplicated",
            description: " Restaurant is already Exist!");


        public static readonly Error DuplicatedMenuCategory = Error.Conflict(
            code: " MenuCategory is Duplicated",
            description: " MenuCategory is already Exist!");

        public static readonly Error MenuCategoryisNull = Error.Validation(
     code: "MenuCategory  is Null",
     description: "MenuCategory is Null!");

        public static readonly Error MenuCategoryNotFound = Error.NotFound(
    code: " MenuCategory not Found!",
    description: " MenuCategory with this ID is  not Found!");

    }
}
