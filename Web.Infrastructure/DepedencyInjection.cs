using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web.Application.Common.File;
using Web.Application.Common.Interfaces;
using Web.Infrastructure.Addresses.Persistence;
using Web.Infrastructure.BaseCategories.Persistence;
using Web.Infrastructure.Common.Persistence.Data;
using Web.Infrastructure.MenuCategories.Persistence;
using Web.Infrastructure.OrderItems.Persistence;
using Web.Infrastructure.Orders.Persistence;
using Web.Infrastructure.Products.Persistence;
using Web.Infrastructure.Restaurants.Persistence;
using Web.Infrastructure.Seeding;
using Web.Infrastructure.Service.Adress;
using Web.Infrastructure.Service.Auth;
using Web.Infrastructure.Service.File;
using Web.Infrastructure.Service.FuzzzySearch;
using Web.Infrastructure.Users.Persistence;

namespace Web.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddPersistence()
                .AddDatabaseConfig(configuration)
                .AddIdentityConfig()
                .Adress();
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            // services.AddScoped<IAdminsRepository, AdminsRepository>();
            services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<AppDbContext>());
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IBaseCategoryRepository, BaseCategoryRepository>();
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<IProductRepository, ProductsRepository>();
            services.AddScoped<IMenuCategoryRepository, MenuCategoriesRepository>();
            services.AddScoped<IFuzzySearchRepository, FuzzySearchRepository>();
            services.AddScoped<IBaseUrlService, BaseUrlService>();
            services.AddHttpContextAccessor();
            services.AddScoped<IFileStorageService, LocalFileStorageService>();
            services.AddScoped<IExcelService, ExcelService>();
            services.AddScoped<IOrderItemRepository, OrderItemsRepository>();
            services.AddScoped<IOrderRepository, OrdersRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAdressesRepository, AdressesRepository>();
            services.AddScoped<ISeedRepository, SeedRepository>();


            return services;
        }
        private static IServiceCollection AddDatabaseConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }

        private static IServiceCollection AddIdentityConfig(this IServiceCollection services)
        {
            services.AddIdentityCore<AppUser>()
       .AddRoles<IdentityRole>()
       .AddEntityFrameworkStores<AppDbContext>()
       .AddDefaultTokenProviders();

            return services;
        }
        public static IServiceCollection Adress(this IServiceCollection services)
        {
            services.AddHttpClient<IReverseGeocodingService, OpenStreetMapGeocodingService>();
            return services;
        }

    }
}
