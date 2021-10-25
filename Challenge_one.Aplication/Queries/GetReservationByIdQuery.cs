using Challenge_one.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;


namespace Challenge_one.Aplication.Queries
{
    public class GetReservationByIdQuery : IRequest<Reservation>
    {
        public int Id { get; }
        public GetReservationByIdQuery(int id)
        {
            Id = id;
        }
    }
}
