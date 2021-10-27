using Challenge_one.Aplication.Command;
using Challenge_one.Aplication.Queries;
using Challenge_one.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Challenge_one.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/[controller]/[Action]")]
    public class SlotController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SlotController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<Guid> AddSlot(Slot slot)
        {
            slot.SlotId = Guid.NewGuid();
            var command = new SendSlotCommand(slot);
            await _mediator.Send(command);
            return slot.SlotId;
        }

        [HttpPut]
        public async Task<Slot> UpdateSlot(Slot slot)
        {
            var command = new SendUpdateSlotCommand(slot);
            return await _mediator.Send(command);
        }

        [HttpGet]
        public async Task<List<Slot>> GetSlots()
        {
            var query = new GetSlotsQuery();
            return await _mediator.Send(query);
        }

        [HttpGet]
        public async Task<List<Slot>> GetAvailableSlots()
        {
            var query = new GetAvailableSlotsQuery();
            return await _mediator.Send(query);
        }

        [HttpGet("{SlotId:guid}")]
        public async Task<Slot> GetSlot(Guid SlotId)
        {
            var query = new GetSlotBySlotIdQuery(SlotId);
            return await _mediator.Send(query);
        }
    }
}
