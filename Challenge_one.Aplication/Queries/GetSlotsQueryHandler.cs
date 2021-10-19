using Challenge_one.Model;
using Challenge_one.MsSql.SlotRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge_one.Aplication.Queries
{
    class GetSlotsQueryHandler : IRequestHandler<GetSlotsQuery, List<Slot>>
    {
        private readonly ISlotRepository _slotRepository;

        public GetSlotsQueryHandler(ISlotRepository slotRepository)
        {
            _slotRepository = slotRepository;
        }

        public async Task<List<Slot>> Handle(GetSlotsQuery request, CancellationToken cancellationToken)
        {
            return await _slotRepository.GetSlots();
        }
    }
}
