using Microsoft.Extensions.DependencyInjection;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Persistence.EntityFramework;

namespace StudentExchangeInfo.Persistence.Registration
{
    public static class ServiceRegistration
    {
        public static void AddPersistencesServices(this IServiceCollection services)
        {
            services.AddScoped<ISliderReadRepository, SliderReadRepository>();
            services.AddScoped<ISliderWriteRepository, SliderWriteRepository>();

            services.AddScoped<IUniversityReadRepository, UniversityReadRepository>();
            services.AddScoped<IUniversityWriteRepository, UniversityWriteRepository>();

            services.AddScoped<IAboutReadRepository, AboutReadRepository>();
            services.AddScoped<IAboutWriteRepository, AboutWriteRepository>();
            
            services.AddScoped<IFaqReadRepository, FaqReadRepository>();
            services.AddScoped<IFaqWriteRepository,  FaqWriteRepository>();

            services.AddScoped<ISocialMediaReadRepository, SocialMediaReadRepository>();
            services.AddScoped<ISocialMediaWriteRepository, SocialMediaWriteRepository>();

            services.AddScoped<ISubscribeReadRepository, SubscribeReadRepository>();
            services.AddScoped<ISubscribeWriteRepository, SubscribeWriteRepository>();

            services.AddScoped<IPrerequisiteReadRepository, PrerequisiteReadRepository>();
            services.AddScoped<IPrerequisiteWriteRepository, PrerequisiteWriteRepository>();

            services.AddScoped<IMotivationReadRepository, MotivationReadRepository>();
            services.AddScoped<IMotivationWriteRepository, MotivationWriteRepository>();

            services.AddScoped<IContactReadRepository, ContactReadRepository>();
            services.AddScoped<IContactWriteRepository, ContactWriteRepository>();

            services.AddScoped<IExchangeProgramReadRepository, ExchangeProgramReadRepository>();
            services.AddScoped<IExchangeProgramWriteRepository, ExchangeProgramWriteRepository>();

            
        }
    }
}
