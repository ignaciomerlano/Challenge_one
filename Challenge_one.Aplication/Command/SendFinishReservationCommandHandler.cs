using Challenge_one.Aplication.Queries;
using Challenge_one.Aplication.ReservationQueue;
using Challenge_one.Aplication.SlotQueue;
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
    public class SendFinishReservationCommandHandler : IRequestHandler<SendFinishReservationCommand, Reservation>
    {
        private readonly IUpdateReservationQueue _updateReservationQueue;
        private readonly ISlotQueue _slotQueue;
        private readonly IMediator _mediator;

        public SendFinishReservationCommandHandler(IUpdateReservationQueue updateReservationQueue, ISlotQueue slotQueue, IMediator mediator)
        {
            _updateReservationQueue = updateReservationQueue;
            _mediator = mediator;
            _slotQueue = slotQueue;
        }

        public async Task<Reservation> Handle(SendFinishReservationCommand request, CancellationToken cancellationToken)
        {
            var query = new GetReservationByIdQuery(request.Id);
            Reservation reservation = await _mediator.Send(query);

            decimal hourlyPrice = 3;
            decimal dayPrice = 57;

            decimal reservationCost = 0;

            var now = DateTime.Now;
            TimeSpan reservationTime = now.Subtract(reservation.CheckIn);

            if (reservationTime.TotalMinutes <= new TimeSpan(3, 0, 0).TotalMinutes) //The first three hours count as one
            {
                reservationCost = hourlyPrice;
            }
            else if (reservationTime.TotalMinutes > new TimeSpan(3, 0, 0).TotalMinutes && reservationTime.TotalMinutes <= new TimeSpan(8, 0, 0).TotalMinutes)
            {
                reservationCost = hourlyPrice + hourlyPrice * (reservationTime.Hours - 3);
            } 
            else if (reservationTime.TotalMinutes > new TimeSpan(8, 0, 0).TotalMinutes && reservationTime.TotalMinutes < new TimeSpan(24, 0, 0).TotalMinutes) //After eight hours the fee is the amount of one day
            {
                reservationCost = dayPrice;
            } 
            else if (reservationTime.TotalMinutes > new TimeSpan(24, 0, 0).TotalMinutes)
            {
                int hoursDays = reservationTime.Days * 24;
                int totalDays = hoursDays < reservationTime.TotalHours ? reservationTime.Days+1 : hoursDays;
                reservationCost = dayPrice * totalDays;
            }

            reservation.Cost = reservationCost;
            reservation.CheckOut = now;

            await _updateReservationQueue.SendUpdateReservation(reservation);

            //Update slot to IsAvailable = true
            var slotQuery = new GetSlotByIdQuery(reservation.Slot.Id);
            var slot = await _mediator.Send(slotQuery);
            slot.IsAvailable = true;
            slot.UpdatedDate = DateTime.Now;
            await _slotQueue.SendSlot(slot);

            return reservation;
        }
    }
}
