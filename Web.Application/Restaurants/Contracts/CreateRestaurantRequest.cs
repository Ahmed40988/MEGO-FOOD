namespace Web.Application.RestaurantCategories.Contracts
{
    public record CreateRestaurantRequest(
          string name,
          string description,
          string userId);
}
