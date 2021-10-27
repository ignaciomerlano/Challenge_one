using Challenge_one.Model;
using Challenge_one.MsSql.SlotRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge_one.Aplication.Queries
{
    class GetAvailableSlotsQueryHandler : IRequestHandler<GetAvailableSlotsQuery, List<Slot>>
    {
        private readonly ISlotRepository _slotRepository;

        public GetAvailableSlotsQueryHandler(ISlotRepository slotRepository)
        {
            _slotRepository = slotRepository;
        }

        public async Task<List<Slot>> Handle(GetAvailableSlotsQuery request, CancellationToken cancellationToken)
        {
            var slots = await _slotRepository.GetSlots();
            return slots.Where(x => x.IsAvailable == true).ToList();
        }
    }
}
