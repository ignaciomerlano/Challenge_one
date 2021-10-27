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
    public class GetSlotBySlotIdQueryHandler : IRequestHandler<GetSlotBySlotIdQuery, Slot>
    {
        private readonly ISlotRepository _slotRepository;

        public GetSlotBySlotIdQueryHandler(ISlotRepository slotRepository)
        {
            _slotRepository = slotRepository;
        }

        public async Task<Slot> Handle(GetSlotBySlotIdQuery request, CancellationToken cancellationToken)
        {
            return await _slotRepository.GetSlotBySlotId(request.SlotId);
        }
    }
}
