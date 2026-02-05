namespace Web.Application.Restaurants.Commands.UpdateRestaurants;

public record UpdateRestaurantCommand(
    Guid Id,
    string Name,
    string Description,
    string AdminId,
    Guid BaseCatgoryId
) : IRequest<ErrorOr<Guid>>;

