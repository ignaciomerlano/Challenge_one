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
    class GetSlotByIdInternalQueryHandler : IRequestHandler<GetSlotByIdInternalQuery, Slot>
    {
        private readonly ISlotRepository _slotRepository;

        public GetSlotByIdInternalQueryHandler(ISlotRepository slotRepository)
        {
            _slotRepository = slotRepository;
        }

        public async Task<Slot> Handle(GetSlotByIdInternalQuery request, CancellationToken cancellationToken)
        {
            return await _slotRepository.GetSlotByIdInternal(request.Id);
        }
    }
}
