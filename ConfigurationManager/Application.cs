using ConfigurationManager.Manager;
using ConfigurationManager.Parsers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ConfigurationManager
{
    public class Application : IHostedService
    {
        private readonly IServiceProvider _services;
        private const string BaseConfigFileLocation = @"Base_Config.txt";
        private const string ProjectConfigFileLocation = @"Project_Config.txt";

        public Application(IServiceProvider services)
        {
            _services = services;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Log.Logger.Information("Configuration Service Started");

            var scope = _services.CreateScope();

            var parser = scope.ServiceProvider.GetRequiredService<IConfigurationParser>();

            var configWriter = scope.ServiceProvider.GetRequiredService<IConfigWriter>();
            var configReader = scope.ServiceProvider.GetRequiredService<IConfigReader>();

            var dic = await parser.ParseConfigurationAsync(BaseConfigFileLocation);
            configWriter.AddConfigurationLayer(0, dic);

            dic = await parser.ParseConfigurationAsync(ProjectConfigFileLocation);
            configWriter.AddConfigurationLayer(1, dic);

            var config =  configReader.GetFullConfiguration();

            Console.WriteLine("Full configuration:");
            Console.WriteLine();

            foreach (var item in config)
            {
                Console.WriteLine($"{item.Key}:\t{item.Value}");
            }


            Console.WriteLine();
            Console.WriteLine("Only numberOfSystems, powerSupply, orderLinesPerOrder, resultStartTime: ");
            Console.WriteLine();

            config = configReader.GetConfiguration(new List<string> { "numberOfSystems", "powerSupply", "orderLinesPerOrder", "resultStartTime" });

            foreach (var item in config)
            {
                Console.WriteLine($"{item.Key}:\t{item.Value}");
            }

            Console.WriteLine();
            Console.WriteLine("Specific data type: ");
            Console.WriteLine();

            var resultStartTime = configReader.GetValue<DateTime>("resultStartTime");

            if(resultStartTime.Succeeded)
                Console.WriteLine($"resultStartTime:\t{resultStartTime.Value.TimeOfDay}");
            else
            {
                foreach(var error in resultStartTime.Errors)
                {
                    Console.WriteLine(error);
                } 
            }
               
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Log.Logger.Information("Configuration Service Stopped");
            return null;
        }
    }
}
