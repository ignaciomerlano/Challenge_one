using Challenge_one.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;


namespace Challenge_one.Aplication.Queries
{
    public class GetReservationByIdQuery : IRequest<Reservation>
    {
        public Guid Id { get; }
        public GetReservationByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
