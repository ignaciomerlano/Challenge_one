using Challenge_one.Model;
using Challenge_one.MsSql.ReservationRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge_one.Aplication.Queries
{
    public class GetReservationByReservationIdQueryHandler : IRequestHandler<GetReservationByReservationIdQuery, Reservation>
    {
        private readonly IReservationRepository _reservationRepository;

        public GetReservationByReservationIdQueryHandler(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<Reservation> Handle(GetReservationByReservationIdQuery request, CancellationToken cancellationToken)
        {
            return await _reservationRepository.GetReservationById(request.ReservationId);
        }
    }
}
