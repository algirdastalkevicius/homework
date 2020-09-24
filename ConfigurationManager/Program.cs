using ConfigurationManager.Factories;
using ConfigurationManager.Manager;
using ConfigurationManager.Parsers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace ConfigurationManager
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).UseSerilog(LoggerSetup.ConfigureSerilog).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) =>
          {
              services.AddHostedService<Application>();
              services.AddSingleton<ILayeredConfigurationFactory, LayeredConfigurationFactory>();
              services.AddScoped<IConfigReader, ConfigManager>();
              services.AddScoped<IConfigWriter, ConfigManager>();
              services.AddScoped<IConfigurationParser, SimpleConfigurationParser>();
          });
    }
}
