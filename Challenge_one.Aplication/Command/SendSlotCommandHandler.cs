using Challenge_one.Aplication.SlotQueue;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge_one.Aplication.Command
{
    class SendSlotCommandHandler : IRequestHandler<SendSlotCommand>
    {
        private readonly ISlotQueue _slotQueue;

        public SendSlotCommandHandler(ISlotQueue slotQueue)
        {
            _slotQueue = slotQueue;
        }

        public async Task<Unit> Handle(SendSlotCommand request, CancellationToken cancellationToken)
        {
            await _slotQueue.SendSlot(request.Slot);

            return Unit.Value;
        }
    }
}
