using ErrorOr;

namespace Web.Domain.BaseCategories
    {
        public static class BaseCategoryErrors
        {
            public static Error RestaurantIsNull =>
                Error.Validation(
                    code: "BaseCategory.Restaurant.Null",
                    description: "Restaurant cannot be null.");

            public static Error DuplicatedRestaurant =>
                Error.Conflict(
                    code: "BaseCategory.Restaurant.Duplicated",
                    description: "A restaurant with the same name already exists in this category.");

            public static Error InvalidRestaurantId =>
                Error.Validation(
                    code: "BaseCategory.Restaurant.InvalidId",
                    description: "Restaurant id is invalid.");

            public static Error RestaurantNotFound =>
                Error.NotFound(
                    code: "BaseCategory.Restaurant.NotFound",
                    description: "Restaurant was not found in this category.");

            public static Error CannotAssignUser =>
                Error.Validation(
                    code: "BaseCategory.User.Invalid",
                    description: "Cannot assign user to this base category.");
        }
    }

