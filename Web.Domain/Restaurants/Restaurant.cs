using ErrorOr;
using Web.Domain.Addresses;
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

        public decimal Rating { get; private set; }

        public bool IsOpen { get; private set; } = true;

        public bool HasFreeDelivery { get; private set; }

        public bool HasFastDelivery { get; private set; }

        public bool HasOffers { get; private set; }

        public string AppUserId { get; private set; } = string.Empty;

        public AppUser AppUser { get; private set; } = default!;

        public Guid BaseCatgoryId { get; private set; }

        public BaseCategory BaseCatgory { get; private set; } = default!;

        private readonly List<MenuCategory> _menuCategories = new();
        public IReadOnlyCollection<MenuCategory> MenuCategories =>
            _menuCategories.AsReadOnly();

        private readonly List<RestaurantAdress> _addresses = new();
        public IReadOnlyCollection<RestaurantAdress> Addresses =>
            _addresses.AsReadOnly();

        private Restaurant() { }

        public Restaurant(
            string name,
            string description,
            string userId,
            Guid baseCatgoryId)
        {
            Id = Guid.NewGuid();

            Name = name ?? throw new ArgumentNullException(nameof(name));

            Description = description ?? throw new ArgumentNullException(nameof(description));

            AppUserId = userId ?? throw new ArgumentNullException(nameof(userId));

            BaseCatgoryId = baseCatgoryId;
        }

        #region Menu Categories

        public ErrorOr<Success> AddMenuCategory(MenuCategory menuCategory)
        {
            if (menuCategory is null)
                return RestaurantsErrors.MenuCategoryisNull;

            if (_menuCategories.Any(x => x.Name == menuCategory.Name))
                return RestaurantsErrors.DuplicatedMenuCategory;

            _menuCategories.Add(menuCategory);

            return Result.Success;
        }

        public ErrorOr<Success> DeleteMenuCategory(Guid menuCategoryId)
        {
            if (menuCategoryId == Guid.Empty)
                return RestaurantsErrors.MenuCategoryisNull;

            var menuCategory =
                _menuCategories.FirstOrDefault(x => x.Id == menuCategoryId);

            if (menuCategory is null)
                return RestaurantsErrors.MenuCategoryNotFound;

            _menuCategories.Remove(menuCategory);

            return Result.Success;
        }


        #endregion

        public void AddAddress(
    double latitude,
    double longitude,
    string? address)
        {
            var restaurantAddress = new RestaurantAdress(
                latitude,
                longitude,
                address,
                Id);

            _addresses.Add(restaurantAddress);
        }

        #region Update

        public void Update(
            string adminId,
            string name,
            string description,
            Guid baseCatgoryId
            //bool isOpen,
            //bool hasFreeDelivery,
            //bool hasFastDelivery,
            //bool hasOffers
            )
        {
            SetName(name);
            SetDescription(description);
            SetBaseCategoryId(baseCatgoryId);

            //IsOpen = isOpen;
            //HasFreeDelivery = hasFreeDelivery;
            //HasFastDelivery = hasFastDelivery;
            //HasOffers = hasOffers;

            Touch(adminId);
        }

        #endregion

        #region Restaurant Settings

        public void Open(string updatedById)
        {
            IsOpen = true;
            Touch(updatedById);
        }

        public void Close(string updatedById)
        {
            IsOpen = false;
            Touch(updatedById);
        }

        public void ToggleOpenStatus(string updatedById)
        {
            IsOpen = !IsOpen;
            Touch(updatedById);
        }

        public void SetFreeDelivery(
            bool enabled,
            string updatedById)
        {
            HasFreeDelivery = enabled;
            Touch(updatedById);
        }

        public void SetFastDelivery(
            bool enabled,
            string updatedById)
        {
            HasFastDelivery = enabled;
            Touch(updatedById);
        }

        public void SetOffers(
            bool enabled,
            string updatedById)
        {
            HasOffers = enabled;
            Touch(updatedById);
        }

        public void SetRating(decimal rating)
        {
            Rating = rating;
        }

        #endregion

        #region Ownership

        public void Rename(
            string newName,
            string updatedById)
        {
            Name = newName;
            Touch(updatedById);
        }

        public void ChangeDescription(
            string newDescription,
            string updatedById)
        {
            Description = newDescription;
            Touch(updatedById);
        }

        public void ChangeOwner(
            string newUserId,
            string updatedById)
        {
            AppUserId = newUserId;
            Touch(updatedById);
        }

        public ErrorOr<Success> AssignUser(
            AppUser user,
            string updatedById)
        {
            if (user is null)
                return RestaurantsErrors.CannotAssignUserforthisIdAllows;

            AppUser = user;
            AppUserId = user.Id;

            Touch(updatedById);

            return Result.Success;
        }

        #endregion

        #region Soft Delete

        public void Delete(string updatedById)
        {
            SoftDelete(updatedById);

            _menuCategories.Clear();
            _addresses.Clear();
        }

        public void RestoreRestaurant(string updatedById)
        {
            Restore(updatedById);
        }

        #endregion

        #region Private Methods

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

        #endregion
    }
}