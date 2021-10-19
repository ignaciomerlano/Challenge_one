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
    class GetSlotByIdQueryHandler : IRequestHandler<GetSlotByIdQuery, Slot>
    {
        private readonly ISlotRepository _slotRepository;

        public GetSlotByIdQueryHandler(ISlotRepository slotRepository)
        {
            _slotRepository = slotRepository;
        }

        public async Task<Slot> Handle(GetSlotByIdQuery request, CancellationToken cancellationToken)
        {
            return await _slotRepository.GetSlotById(request.Id);
        }
    }
}
