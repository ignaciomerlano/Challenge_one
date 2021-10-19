using Challenge_one.MsSql.SlotRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge_one.Aplication.Command
{
    class AddSlotCommandHandler : IRequestHandler<AddSlotCommand>
    {
        private readonly ISlotRepository _slotRepository;

        public AddSlotCommandHandler(ISlotRepository slotRepository)
        {
            _slotRepository = slotRepository;
        }

        public async Task<Unit> Handle(AddSlotCommand request, CancellationToken cancellationToken)
        {
            await _slotRepository.AddSlot(request.Slot);
            return Unit.Value;
        }
    }
}
