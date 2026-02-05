using ErrorOr;
using Web.Domain.BaseCategories;
using Web.Domain.BaseModels;
using Web.Domain.MenuCategories;
using Web.Domain.Users;

namespace Web.Domain.Restaurants
{
    public class Restaurant : BaseModel
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public string AppUserId { get; private set; } = string.Empty;
        public AppUser AppUser { get; private set; } = default!;
        public Guid BaseCatgoryId { get; private set; }
        public BaseCategory BaseCatgory { get; private set; } = default!;

        private readonly List<MenuCategory> _menuCategories = new();
        public IReadOnlyCollection<MenuCategory> MenuCategories => _menuCategories.AsReadOnly();


        private Restaurant() { }
        public Restaurant(
          string name,
          string description,
          string userId,
            Guid baseCatgoryId
            )
        {
            Id = Guid.NewGuid();
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            AppUserId = userId ?? throw new ArgumentNullException(nameof(userId));
            BaseCatgoryId = baseCatgoryId ;
        }
        

        public ErrorOr<Success> AddMenuCategory(MenuCategory menuCategory)
        {
            if (menuCategory is null)
                return RestaurantsErrors.MenuCategoryisNull;

            if (_menuCategories.Any(m => m.Name == menuCategory.Name))
                return RestaurantsErrors.DuplicatedMenuCategory;

            _menuCategories.Add(menuCategory);
            return Result.Success;
        }
        public ErrorOr<Success> DeleteMenuCategory(Guid menuCategoryId)
        {
            if (menuCategoryId == Guid.Empty)
                return RestaurantsErrors.MenuCategoryisNull;

            var menuCategory = _menuCategories
                .FirstOrDefault(m => m.Id == menuCategoryId);

            if (menuCategory is null)
                return RestaurantsErrors.MenuCategoryNotFound;

            _menuCategories.Remove(menuCategory);

            return Result.Success;
        }
        public void Update(string adminId, string name, string description,Guid baseCatgoryId)
        {
            SetName(name);
            SetDescription(description);
            SetBaseCategoryId(baseCatgoryId);
            Touch(adminId);
        }
        public void Rename(string newName, string UpdatedByid)
        {
            Name = newName;
            Touch(UpdatedByid);

        }
        public void ChangeDescription(string newDescription, string updatedById)
        {
            Description = newDescription;
            Touch(updatedById);
        }

        public void ChangeOwner(string newUserId, string updatedById)
        {
            AppUserId = newUserId;
            Touch(updatedById);
        }

        public ErrorOr<Success> AssignUser(AppUser user, string updatedById)
        {
            if (user is null)
                return RestaurantsErrors.CannotAssignUserforthisIdAllows;

            AppUser = user;
            AppUserId = user.Id;
            Touch(updatedById);
            return Result.Success;
        }


        public void Delete(string updatedById)
        {
            SoftDelete(updatedById);
            _menuCategories.Clear();
        }


        public void RestoreRestaurant(string updatedById)
        {
            Restore(updatedById);
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
        private void SetBaseCategoryId(Guid baseCategoryId)
        {
            BaseCatgoryId = baseCategoryId;
        }


    }
}
