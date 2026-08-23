using Web.Domain.BaseModels;
using Web.Domain.MenuCategories;

public class Product : BaseModel
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    public List<string>ImagesURL { get; private set; } = new List<string>();

    public decimal Price { get; private set; }

    public decimal Rating { get; set; }

    public Guid MenuCategoryId { get; private set; }
    public MenuCategory MenuCategory { get; private set; } = default!;


    private Product() { }

    public Product(
        string name,
        string description,
        List<string> imagesurl,
        decimal price,
        Guid menuCategoryId)
    {
        Id = Guid.NewGuid();

        SetName(name);
        SetDescription(description);
        SetImagesURL(imagesurl);
        SetPrice(price);
        SetMenuCategoryId(menuCategoryId);

    }

    public void Update(string name, string description, List<string> imagesurl, decimal price, string updatedById)
    {
        SetName(name);
        SetDescription(description);
        SetImagesURL(imagesurl);
        SetPrice(price);
        Touch(updatedById);
    }
    public void Delete(string updatedById)
    {
        SoftDelete(updatedById);
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.", nameof(name));

        if (name.Length > 100)
            throw new ArgumentException("Name cannot exceed 100 characters.");

        Name = name.Trim();
    }

    private void SetDescription(string description)
    {
        Description = description?.Trim() ?? string.Empty;
    }
    private void SetImagesURL(List<string> imagesURL)
    {
        if (imagesURL == null)
            throw new ArgumentNullException(nameof(imagesURL), "ImagesURL cannot be null.");
        ImagesURL = imagesURL.Select(url => url?.Trim() ?? string.Empty).ToList();
    }
    private void SetPrice(decimal price)
    {
        if (price < 0)
            throw new ArgumentException("Price cannot be negative.", nameof(price));

        Price = price;
    }

    private void SetMenuCategoryId(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("MenuCategoryId is required.", nameof(id));

        MenuCategoryId = id;
    }

}
