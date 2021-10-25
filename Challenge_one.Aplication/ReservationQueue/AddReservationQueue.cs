using Challenge_one.Model;
using RabbitMQ.Client.Core.DependencyInjection.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Challenge_one.Aplication.ReservationQueue
{
    public class AddReservationQueue : IAddReservationQueue
    {
        readonly IQueueService _queueService;

        public AddReservationQueue(IQueueService queueService)
        {
            _queueService = queueService;
        }

        public async Task SendAddReservation(Reservation reservation)
        {
            await _queueService.SendAsync(
            @object: reservation,
            exchangeName: "exchange.reservation",
            routingKey: "AddReservation");
        }
    }
}