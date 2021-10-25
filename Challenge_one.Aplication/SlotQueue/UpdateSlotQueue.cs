using Challenge_one.Model;
using RabbitMQ.Client.Core.DependencyInjection.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_one.Aplication.SlotQueue
{
    class UpdateSlotQueue : IUpdateSlotQueue
    {
        readonly IQueueService _queueService;

        public UpdateSlotQueue(IQueueService queueService)
        {
            _queueService = queueService;
        }

        public async Task SendUpdateSlot(Slot slot)
        {
            await _queueService.SendAsync(
            @object: slot,
            exchangeName: "exchange.slot",
            routingKey: "UpdateSlot");
        }
    }
}
