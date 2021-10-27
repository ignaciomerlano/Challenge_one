using Challenge_one.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Challenge_one.Aplication.Queries
{
    public class GetReservationByReservationIdQuery : IRequest<Reservation>
    {
        public Guid ReservationId { get; }
        public GetReservationByReservationIdQuery(Guid reservationId)
        {
            ReservationId = reservationId;
        }
    }
}
