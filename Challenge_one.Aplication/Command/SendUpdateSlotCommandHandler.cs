using Challenge_one.Aplication.SlotQueue;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge_one.Aplication.Command
{
    public class SendUpdateSlotCommandHandler : IRequestHandler<SendUpdateSlotCommand>
    {
        private readonly IUpdateSlotQueue _slotQueue;

        public SendUpdateSlotCommandHandler(IUpdateSlotQueue slotQueue)
        {
            _slotQueue = slotQueue;
        }

        public async Task<Unit> Handle(SendUpdateSlotCommand request, CancellationToken cancellationToken)
        {
            await _slotQueue.SendUpdateSlot(request.Slot);

            return Unit.Value;
        }
    }
}
