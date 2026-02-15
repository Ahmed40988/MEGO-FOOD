
using Web.Application.Restaurants.Contracts;

namespace Web.Application.Restaurants.Commands.CreateRestaurants
{
    public record CreateRestaurantsCommand(
          string Name,
          string Description,
          string UserId,
          Guid BaseCatgoryId) : IRequest<ErrorOr<Guid>>;
}
