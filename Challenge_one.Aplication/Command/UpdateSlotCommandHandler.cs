using Challenge_one.MsSql.SlotRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge_one.Aplication.Command
{
    public class UpdateSlotCommandHandler : IRequestHandler<UpdateSlotCommand>
    {
        private readonly ISlotRepository _slotRepository;

        public UpdateSlotCommandHandler(ISlotRepository slotRepository)
        {
            _slotRepository = slotRepository;
        }

        public async Task<Unit> Handle(UpdateSlotCommand request, CancellationToken cancellationToken)
        {
            await _slotRepository.UpdateSlot(request.Slot);
            return Unit.Value;
        }
    }
}
