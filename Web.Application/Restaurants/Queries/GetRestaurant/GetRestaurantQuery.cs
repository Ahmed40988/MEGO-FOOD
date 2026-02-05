using Web.Application.Restaurants.Contracts;

namespace Web.Application.Restaurants.Queries.GetRestaurant
{
    public record GetRestaurantQuery(Guid Id) : IRequest<ErrorOr<RestaurantResponce>>;
}
