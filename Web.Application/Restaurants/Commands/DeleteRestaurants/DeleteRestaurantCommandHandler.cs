
using Web.Domain.Users;
namespace Web.Application.Restaurants.Commands.DeleteRestaurants
{
    public class DeleteRestaurantCommandHandler(IRestaurantRepository restaurantRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteRestaurantCommand, ErrorOr<Deleted>>
    {
        private readonly IRestaurantRepository _restaurantRepository = restaurantRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ErrorOr<Deleted>> Handle(DeleteRestaurantCommand command, CancellationToken cancellationToken)
        {
            var restaurant = await _restaurantRepository
                .GetByIdAsync(command.Id, cancellationToken);

            if (restaurant == null)
                return Error.NotFound(
                 "Restaurant.NotFound",
                 "Restaurant with the given id was not found");

  

            restaurant.Delete(command.AdminId);
            await _restaurantRepository.UpdateAsync(restaurant);
            await _unitOfWork.CommitChangesAsync();

            return Result.Deleted;
        }
    }
}

