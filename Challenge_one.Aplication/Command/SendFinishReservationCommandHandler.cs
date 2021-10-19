using Challenge_one.Aplication.Queries;
using Challenge_one.Aplication.ReservationQueue;
using Challenge_one.Model;
using Challenge_one.MsSql.ReservationRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge_one.Aplication.Command
{
    class SendFinishReservationCommandHandler : IRequestHandler<SendFinishReservationCommand, Reservation>
    {
        private readonly IUpdateReservationQueue _updateReservationQueue;
        private readonly IMediator _mediator;

        public SendFinishReservationCommandHandler(IUpdateReservationQueue updateReservationQueue, IMediator mediator)
        {
            _updateReservationQueue = updateReservationQueue;
            _mediator = mediator;
        }

        public async Task<Reservation> Handle(SendFinishReservationCommand request, CancellationToken cancellationToken)
        {
            var query = new GetReservationByIdQuery(request.Id);
            Reservation reservation = await _mediator.Send(query);

            reservation.Cost = 150;

            //logic
            //update slot queue?

            await _updateReservationQueue.SendUpdateReservation(reservation);

            return reservation;
        }
    }
}
