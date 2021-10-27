using Challenge_one.Aplication.SlotQueue;
using Challenge_one.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge_one.Aplication.Command
{
    public class SendUpdateSlotCommandHandler : IRequestHandler<SendUpdateSlotCommand, Slot>
    {
        private readonly IUpdateSlotQueue _slotQueue;

        public SendUpdateSlotCommandHandler(IUpdateSlotQueue slotQueue)
        {
            _slotQueue = slotQueue;
        }

        public async Task<Slot> Handle(SendUpdateSlotCommand request, CancellationToken cancellationToken)
        {
            await _slotQueue.SendUpdateSlot(request.Slot);

            return request.Slot;
        }
    }
}
