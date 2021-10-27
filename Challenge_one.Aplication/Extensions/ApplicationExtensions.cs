using Challenge_one.Aplication.ReservationQueue;
using Challenge_one.Aplication.SlotQueue;
using Challenge_one.MsSql.AppSettingRepository;
using Challenge_one.MsSql.ReservationRepository;
using Challenge_one.MsSql.SlotRepository;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client.Core.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Challenge_one.Aplication.Extensions
{
    public static class ApplicationExtensions
    {

        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration, bool isSend)
        {
            var rabbitMqSection = configuration.GetSection("RabbitMq");
            var exchangeSection = configuration.GetSection("RabbitMqExchange");

            if (isSend)
            {
                services.AddRabbitMqClient(rabbitMqSection)
                    .AddProductionExchange("exchange.slot", exchangeSection)
                    .AddProductionExchange("exchange.reservation", exchangeSection);
            }
            else
            {
                services.AddRabbitMqClient(rabbitMqSection)
                    .AddConsumptionExchange("exchange.slot", exchangeSection)
                    .AddConsumptionExchange("exchange.reservation", exchangeSection);
            }



            services.AddMediatR(typeof(IApplicationAnchor));
            services.AddScoped<ISlotQueue, SlotQueue.SlotQueue>();
            services.AddScoped<IUpdateSlotQueue, UpdateSlotQueue>();
            services.AddScoped<IUpdateReservationQueue, UpdateReservationQueue>();
            services.AddScoped<IAddReservationQueue, AddReservationQueue>();
            services.AddScoped<ISlotRepository, SlotRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IAppSettingRepository, AppSettingRepository>();
            return services;
        }

    }
}
