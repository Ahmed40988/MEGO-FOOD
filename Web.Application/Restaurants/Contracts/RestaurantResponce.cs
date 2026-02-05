namespace Web.Application.Restaurants.Contracts
{
    public record RestaurantResponce(
            Guid Id,
          string name,
          string description,
            Guid  BaseCatgoryId
        );

}
