using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Challenge_one.Aplication.Extensions;
using Challenge_one.Manage;
using Challenge_one.ManagerQueue;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Core.DependencyInjection;
using RabbitMQ.Client.Core.DependencyInjection.Services;

namespace Challenge_one.Manager
{
    public class Program
    {
        public static IConfiguration Configuration { get; set; }
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var queueService = serviceProvider.GetRequiredService<IQueueService>();
            queueService.StartConsuming();

            Console.ReadLine();
        }

        static void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationDependencies(Configuration, false);
            services.AddAsyncMessageHandlerSingleton<SlotsEventHandler>("AddSlot")
                    .AddAsyncMessageHandlerSingleton<SlotsEventHandler>("UpdateSlot")
                    .AddAsyncMessageHandlerSingleton<UpdateReservationEventHandler>("UpdateReservation")
                    .AddAsyncMessageHandlerSingleton<AddReservationEventHandler>("AddReservation");
        }
    }
}
