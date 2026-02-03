namespace Web.Application.Restaurants.Queries.ListRestaurant
{
    public record ListRestaurantQuerys : IRequest<ErrorOr<List<Restaurant>>>;
}
