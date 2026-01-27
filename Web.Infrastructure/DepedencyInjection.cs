using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Common.Interfaces;
using Web.Infrastructure.BaseCategories.Persistence;
using Web.Infrastructure.Common.Persistence.Data;
using Web.Infrastructure.MenuCategories.Persistence;
using Web.Infrastructure.OrderItems.Persistence;
using Web.Infrastructure.Orders.Persistence;
using Web.Infrastructure.Products.Persistence;
using Web.Infrastructure.Restaurants.Persistence;
using Web.Infrastructure.Service.Auth;
using Web.Infrastructure.Service.Communication.Email;
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
                .AddDatabaseConfig(configuration )
                .AddIdentityConfig()
                .AddAuthorizationConfig();
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
           // services.AddScoped<IAdminsRepository, AdminsRepository>();
            services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<AppDbContext>());
            services.AddScoped<IBaseCategoryRepository, BaseCategoryRepository>();
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<IProductRepository, ProductsRepository>();
            services.AddScoped<IMenuCategoryRepository, MenuCategoriesRepository>();
            services.AddScoped<IFuzzySearchRepository, FuzzySearchRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemsRepository>();
            services.AddScoped<IOrderRepository, OrdersRepository>();
            services.AddScoped<TokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();


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
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }

        private static IServiceCollection AddAuthorizationConfig(this IServiceCollection services)
        {
            services.AddAuthorization();
            return services;
        }
    }
}
