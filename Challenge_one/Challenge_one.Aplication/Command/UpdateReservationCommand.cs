using Challenge_one.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Challenge_one.Aplication.Command
{
    public class UpdateReservationCommand : IRequest
    {
        public Reservation Reservation { get; }

        public UpdateReservationCommand(Reservation reservation)
        {
            Reservation = reservation;
        }
    }
}
