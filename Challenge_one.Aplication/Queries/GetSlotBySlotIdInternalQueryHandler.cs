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
    class GetSlotBySlotIdInternalQueryHandler : IRequestHandler<GetSlotBySlotIdInternalQuery, Slot>
    {
        private readonly ISlotRepository _slotRepository;

        public GetSlotBySlotIdInternalQueryHandler(ISlotRepository slotRepository)
        {
            _slotRepository = slotRepository;
        }

        public async Task<Slot> Handle(GetSlotBySlotIdInternalQuery request, CancellationToken cancellationToken)
        {
            return await _slotRepository.GetSlotBySlotIdInternal(request.SlotId);
        }
    }
}
