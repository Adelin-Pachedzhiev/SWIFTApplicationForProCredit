using SwiftApplicationAPI.Services;
using Serilog;
using SwiftApplicationAPI.Services;
using SwiftApplicationAPI.Data;

namespace SwiftApplicationAPI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                        .WriteTo
                        .File("logs/log.txt", rollingInterval: RollingInterval.Day)
                        .CreateLogger();

            services.AddLogging(configure =>
            {
                configure.AddConsole().AddSerilog();
            });
            services.AddSingleton<SWIFTMessagesDataContext>();
            services.AddSingleton<ISwiftParserService, SwiftParserService>();
            services.AddScoped<ISwiftMessageRepository, SwiftMessageRepository>();



            return services;
        }
    }
}
