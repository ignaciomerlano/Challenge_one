using Challenge_one.Aplication.Command;
using Challenge_one.Model;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client.Core.DependencyInjection.MessageHandlers;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_one.ManagerQueue
{
    public class AddReservationEventHandler : IAsyncMessageHandler
    {
        private readonly IMediator _mediator;

        public AddReservationEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(BasicDeliverEventArgs eventArgs, string matchingRoute)
        {
            var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
            var reservation = JsonConvert.DeserializeObject<Reservation>(message);
            var addReservationCommand = new AddReservationCommand(reservation);
            await _mediator.Send(addReservationCommand);
        }
    }
}
