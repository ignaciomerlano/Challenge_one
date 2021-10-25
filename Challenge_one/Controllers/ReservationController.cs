using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Challenge_one.Model;
using Challenge_one.Aplication.Command;
using Challenge_one.Aplication.Queries;
using System.Net.Http;

namespace Challenge_one.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/[controller]/[Action]")]
    public class ReservationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReservationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> SaveReservation(Reservation reservation)
        {
            var command = new SendReservationCommand(reservation);
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<Reservation> FinishReservation(int Id)
        {
            var command = new SendFinishReservationCommand(Id);
            return await _mediator.Send(command);
        }

        [HttpGet("{Id}")]
        public async Task<Reservation> GetReservation(int Id)
        {
            var query = new GetReservationByIdQuery(Id);
            return await _mediator.Send(query);
        }
    }
}
