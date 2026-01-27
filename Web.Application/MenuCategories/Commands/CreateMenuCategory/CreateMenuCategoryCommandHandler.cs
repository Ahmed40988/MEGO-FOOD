
using Web.Application.Common.Interfaces;
using Web.Application.Restaurants.Commands.CreateRestaurants;
using Web.Domain.MenuCategories;

namespace Web.Application.MenuCategories.Commands.CreateMenuCategory
{
    public class CreateMenuCategoryCommandHandler
      (IRestaurantRepository restaurantCategoryRepository,
      IUnitOfWork unitOfWork)
      : IRequestHandler<CreateMenuCategoryCommand, ErrorOr<MenuCategory>>
    {
        private readonly IRestaurantRepository _restaurantCategoryRepository = restaurantCategoryRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ErrorOr<MenuCategory>> Handle(CreateMenuCategoryCommand command, CancellationToken cancellationToken)
        {
            var restaurantcategory=await _restaurantCategoryRepository.GetByIdAsync(command.restaurantcategoryid);

            if(restaurantcategory == null)
              return Error.NotFound(description: "RestaurantCategory For this Product is not found");


            var category = new MenuCategory(
                command.name,
                command.description, 
                command.restaurantcategoryid);

            var addmenucategoryResult = restaurantcategory.AddMenuCategory(category);

            if (addmenucategoryResult.IsError)
                return addmenucategoryResult.Errors;


            await _restaurantCategoryRepository.UpdateAsync(restaurantcategory);
            await _unitOfWork.CommitChangesAsync();
            throw new NotImplementedException();
        }
    }
}
