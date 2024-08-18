using WebApplication1.Services;
using Serilog;
using Serilog.Extensions.Logging;

namespace WebApplication1
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {

            services.AddLogging(configure =>
            {
                configure.AddConsole().AddSerilog();
            });

            services.AddSingleton<ISwiftParserService, SwiftParserService>();

            return services;
        }
    }
}
