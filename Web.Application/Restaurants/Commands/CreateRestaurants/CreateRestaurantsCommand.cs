
namespace Web.Application.Restaurants.Commands.CreateRestaurants
{
    public record CreateRestaurantsCommand(
          string name,
          string description,
          string userId):IRequest<ErrorOr<Restaurant>>;
}
