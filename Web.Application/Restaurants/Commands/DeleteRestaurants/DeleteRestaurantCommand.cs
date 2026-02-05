namespace Web.Application.Restaurants.Commands.DeleteRestaurants
{
    public record DeleteRestaurantCommand(Guid Id,string AdminId) : IRequest<ErrorOr<Deleted>>;
}
