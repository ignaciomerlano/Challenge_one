using Challenge_one.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Challenge_one.Aplication.Command
{
    public class SendReservationCommand : IRequest<HttpResponseMessage>
    {
        public Reservation Reservation { get; }
        public SendReservationCommand(Reservation reservation)
        {
            Reservation = reservation;
        }
    }
}
