using ErrorOr;
using Web.Domain.BaseModels;
using Web.Domain.Restaurants;

namespace Web.Domain.MenuCategories
{
    public class MenuCategory : BaseModel
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;

        public Guid RestaurantId { get; private set; }
        public Restaurant restaurant { get; private set; } = default!;

        private readonly List<Product> _products = new();
        public IReadOnlyCollection<Product> Products => _products.AsReadOnly();



        private MenuCategory() { }

        public MenuCategory(
          string name,
          string description,
          Guid restaurantcategoryid)
        {
            Id = Guid.NewGuid();
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            RestaurantId = restaurantcategoryid;
        }

        public ErrorOr<Success> AddProduct(Product product)
        {
            if (product is null)
                return MenuCategoryErrors.productCategoryisNull;

            if (_products.Any(m => m.Name == product.Name))
                return MenuCategoryErrors.DuplicatedProduct;

            _products.Add(product);
            return Result.Success;
        }
        public ErrorOr<Success> Deleteproduct(Guid productId)
        {
            if (productId == Guid.Empty)
                return MenuCategoryErrors.productCategoryisNull;

            var proudct = _products
                .FirstOrDefault(m => m.Id == productId);

            if (proudct is null)
                return MenuCategoryErrors.productCategoryNotFound;

            _products.Remove(proudct);

            return Result.Success;
        }

        public void Delete(string updatedById)
        {
            SoftDelete(updatedById);
            _products.Clear();
        }
        public void Update(string adminId, string name, string description, Guid restaurantId)
        {
            SetName(name);
            SetDescription(description);
            SetrestaurantId(restaurantId);
            Touch(adminId);
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
        private void SetrestaurantId(Guid restaurantId)
        {
            RestaurantId = restaurantId;
        }

    } 
}
