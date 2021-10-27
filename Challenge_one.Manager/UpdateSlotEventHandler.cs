using Challenge_one.Aplication.Command;
using Challenge_one.Model;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client.Core.DependencyInjection.MessageHandlers;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Challenge_one.ManagerQueue
{
    public class UpdateSlotEventHandler : IAsyncMessageHandler
    {
        private readonly IMediator _mediator;

        public UpdateSlotEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(BasicDeliverEventArgs eventArgs, string matchingRoute)
        {
            var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
            var slot = JsonConvert.DeserializeObject<Slot>(message);
            var updateSlotCommand = new UpdateSlotCommand(slot);
            await _mediator.Send(updateSlotCommand);
        }
    }
}
