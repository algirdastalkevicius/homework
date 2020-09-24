using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace ConfigurationLibrary
{
    public static class LoggerSetup 
    {
        public static void ConfigureSerilog(HostBuilderContext hostingContext, LoggerConfiguration config)
        {
            config.MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .WriteTo.File("logs\\log-.txt",
                    LogEventLevel.Information,
                    "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} ({RequestId}) [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 60);
        }

    }
}