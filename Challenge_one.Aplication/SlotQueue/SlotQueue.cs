using Challenge_one.Aplication.SlotQueue;
using Challenge_one.Model;
using RabbitMQ.Client.Core.DependencyInjection.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_one.Aplication.SlotQueue
{
    public class SlotQueue : ISlotQueue
    {
        readonly IQueueService _queueService;

        public SlotQueue(IQueueService queueService)
        {
            _queueService = queueService;
        }

        public async Task SendSlot(Slot slot)
        {
            await _queueService.SendAsync(
            @object: slot,
            exchangeName: "exchange.addslot",
            routingKey: "AddSlot");
        }
    }
}
