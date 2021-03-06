using Challenge_one.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Challenge_one.Aplication.Queries
{
    public class GetSlotBySlotIdQuery : IRequest<Slot>
    {
        public Guid SlotId { get; }
        public GetSlotBySlotIdQuery(Guid slotId)
        {
            SlotId = slotId;
        }
    }
}
