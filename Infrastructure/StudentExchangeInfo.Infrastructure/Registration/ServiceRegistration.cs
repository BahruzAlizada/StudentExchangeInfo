using Microsoft.Extensions.DependencyInjection;
using StudentExchangeInfo.Infrastructure.Abstract;
using StudentExchangeInfo.Infrastructure.Concrete;

namespace StudentExchangeInfo.Infrastructure.Registration
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IPhotoService, PhotoService>();
        }
    }
}
