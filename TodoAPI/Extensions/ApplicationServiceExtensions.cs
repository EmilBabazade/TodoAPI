using MediatR;
using TodoAPI.Helpers;

namespace TodoAPI.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddMediatR(typeof(Program));
            return services;
        }
    }
}
