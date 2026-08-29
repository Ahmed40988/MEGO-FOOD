using Microsoft.EntityFrameworkCore;
using Serilog;
using Web.Application;
using Web.Application.Common.Interfaces;
using Web.Infrastructure;
using Web.Infrastructure.Common.Persistence.Data;


namespace Web.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDependencies(builder.Configuration);

            builder.Services
                    .AddApplication()
                    .AddInfrastructure(builder.Configuration);

            builder.Host.UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration));

            var app = builder.Build();
            //Seeding
            //using (var scope = app.Services.CreateScope())
            //{
            //    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            //    var seedRepository = scope.ServiceProvider.GetRequiredService<ISeedRepository>();

            //    if (!await context.BaseCategories.AnyAsync())
            //    {
            //        await seedRepository.SeedAsync(
            //            "9912cd0d-78a6-49a3-84b7-2dcc17cce814",
            //            CancellationToken.None);
            //    }
            //}

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MEGO FOOD v1");
                c.RoutePrefix = "swagger";
                c.ConfigObject.PersistAuthorization = true;
            });

            app.UseSerilogRequestLogging();

            app.UseCors();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            await app.RunAsync();
        }
    }
}
