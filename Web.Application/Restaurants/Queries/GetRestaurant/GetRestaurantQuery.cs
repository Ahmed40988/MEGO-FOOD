namespace Web.Application.Restaurants.Queries.GetRestaurant
{
    public record GetRestaurantQuery(Guid CategoryId) : IRequest<ErrorOr<Restaurant>>;
}
