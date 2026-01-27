using ErrorOr;
using Web.Domain.BaseModels;
using Web.Domain.Restaurants;
using Web.Domain.Users;

namespace Web.Domain.BaseCategories
{
    public class BaseCategory : BaseModel
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;

        public string UserId { get; private set; } = string.Empty;
        public AppUser AppUser { get; private set; } = default!;

        private readonly List<Restaurant> _restaurants = new();
        public IReadOnlyCollection<Restaurant> Restaurants => _restaurants.AsReadOnly();

        private BaseCategory() { }

        public BaseCategory(string name, string description, string userId)
        {
            Id = Guid.NewGuid();
            SetName(name);
            SetDescription(description);
            UserId = userId ?? throw new ArgumentNullException(nameof(userId));
        }

        public ErrorOr<Success> AddRestaurant(Restaurant restaurant)
        {
            if (restaurant is null)
                return BaseCategoryErrors.RestaurantIsNull;

            if (_restaurants.Any(r => r.Name == restaurant.Name))
                return BaseCategoryErrors.DuplicatedRestaurant;

            _restaurants.Add(restaurant);
            return Result.Success;
        }

        public ErrorOr<Success> RemoveRestaurant(Guid restaurantId)
        {
            if (restaurantId == Guid.Empty)
                return BaseCategoryErrors.InvalidRestaurantId;

            var restaurant = _restaurants.FirstOrDefault(r => r.Id == restaurantId);

            if (restaurant is null)
                return BaseCategoryErrors.RestaurantNotFound;

            _restaurants.Remove(restaurant);
            return Result.Success;
        }

        public void Rename(string newName, string updatedById)
        {
            SetName(newName);
            Touch(updatedById);
        }

        public void ChangeDescription(string newDescription, string updatedById)
        {
            SetDescription(newDescription);
            Touch(updatedById);
        }

        public ErrorOr<Success> AssignUser(AppUser user, string updatedById)
        {
            if (user is null)
                return BaseCategoryErrors.CannotAssignUser;

            AppUser = user;
            UserId = user.Id;
            Touch(updatedById);

            return Result.Success;
        }

        public void Delete(string updatedById)
        {
            SoftDelete(updatedById);
            _restaurants.Clear();
        }
        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.", nameof(name));

            Name = name.Trim();
        }

        private void SetDescription(string description)
        {
            Description = description?.Trim() ?? string.Empty;
        }
    }
}
