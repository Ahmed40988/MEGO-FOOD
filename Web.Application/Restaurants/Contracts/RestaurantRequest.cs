namespace Web.Application.Restaurants.Contracts
{
    public record RestaurantRequest(
          string Name,
          string Description,
        Guid BaseCatgoryId);
}
