namespace Web.Application.Restaurants.Commands.DeleteRestaurants
{
    public record DeleteRestaurantCommand(Guid CategoryId):IRequest<ErrorOr<Deleted>>; 
}
