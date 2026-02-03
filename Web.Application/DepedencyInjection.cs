using Microsoft.Extensions.DependencyInjection;
using Web.Application.Common.Behaviors;

namespace Web.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));
                options.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));

            return services;
        }
    }
}
