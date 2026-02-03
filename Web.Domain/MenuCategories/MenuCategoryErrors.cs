using ErrorOr;

namespace Web.Domain.MenuCategories
{
    public static class MenuCategoryErrors
    {
        public static readonly Error CannotAssignUserforthisIdAllows = Error.NotFound(
            code: "User not Found!",
            description: "User with this ID is  not Found!");

        public static readonly Error productCategoryNotFound = Error.NotFound(
            code: "product not Found!",
            description: "product with this ID is  not Found!");

        public static readonly Error productCategoryisNull = Error.Validation(
            code: "Product is Null",
            description: "Product is Null!");

        public static readonly Error DuplicatedProduct = Error.Conflict(
            code: "Product is Duplicated",
            description: "Product is already Exist!");

    }
}
