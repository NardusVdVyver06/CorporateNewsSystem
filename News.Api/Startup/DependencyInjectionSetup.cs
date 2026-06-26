using NewsApi.Services;

namespace NewsApi.Startup;

public static class DependencyInjectionSetup
{
    extension(IServiceCollection services)
    {
        public void ConfigureServices()
        {
            services.AddOpenApi();
            services.AddEndpointsApiExplorer();

            services.AddSingleton<MonitorService>();
            
            services.AddScoped<AuthService>();
            services.AddScoped<NewsArticleService>();
        }
    }
}
