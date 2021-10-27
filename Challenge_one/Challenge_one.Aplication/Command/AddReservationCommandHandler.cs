using Challenge_one.MsSql.ReservationRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge_one.Aplication.Command
{
    public class AddReservationCommandHandler : IRequestHandler<AddReservationCommand>
    {
        private readonly IReservationRepository _reservationRepository;

        public AddReservationCommandHandler(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<Unit> Handle(AddReservationCommand request, CancellationToken cancellationToken)
        {
            await _reservationRepository.AddReservation(request.Reservation);
            return Unit.Value;
        }
    }
}
