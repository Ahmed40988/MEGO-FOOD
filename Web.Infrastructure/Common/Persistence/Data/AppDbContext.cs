using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;
using Web.Application.Common.Interfaces;
using Web.Domain.BaseCategories;
using Web.Domain.MenuCategories;
using Web.Domain.OrderItems;
using Web.Domain.Orders;
using Web.Domain.Restaurants;

namespace Web.Infrastructure.Common.Persistence.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>, IUnitOfWork
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public async Task CommitChangesAsync()
        {
            await SaveChangesAsync();
        }
        public DbSet<BaseCategory> BaseCategories => Set<BaseCategory>();
        public DbSet<Restaurant> Restaurants => Set<Restaurant>();
        public DbSet<MenuCategory> MenuCategories => Set<MenuCategory>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<orderitem> Orderitems => Set<orderitem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Important for Identity

            #region explanation
            //   انا هنا فصلت كل الكونفيجريشن بتاع كل مودل ف كلاس لوحده للتنظيم وعملت هنا كول لكل الكونفيجريشن
            // IEntityTypeConfiguration دي عن طريق انه هيطبق كل الكلاسسز اللي بتورث من  
            #endregion
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            #region explanation2
            // Restrict بص ي هندسه (مصطفي و محمد )انا ضيفت الكود دا هنا علشان اغير سلوك المسح  كله يكون  
            //  علشان مدخلش ف مشكله زي اني امسح كاتيجوري مثلا تمسح كل المنتجات اللي تحتها  
            // Cascade على أكتر من مستوى ====>(CategoryId → Product → OrderItems)، وده يمسح آلاف الصفوف من غير ما تحس.
            //soft delete بيحظرك ويجبرك انك تمسح العلاقات اللي بين الجداول دي وبعض او انك تستخدم   Restrict لكن بقا سلوك 
            #endregion
            var cascadeFKs = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;


        }
    }
}
