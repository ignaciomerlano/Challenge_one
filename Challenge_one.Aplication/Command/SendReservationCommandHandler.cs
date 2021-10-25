using Challenge_one.Aplication.Queries;
using Challenge_one.Aplication.ReservationQueue;
using Challenge_one.Aplication.SlotQueue;
using Challenge_one.Model;
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
    public class SendReservationCommandHandler : IRequestHandler<SendReservationCommand, HttpResponseMessage>
    {
        private readonly IAddReservationQueue _reservationQueue;
        private readonly ISlotQueue _slotQueue;
        private readonly IMediator _mediator;

        public SendReservationCommandHandler(IAddReservationQueue reservationQueue, ISlotQueue slotQueue, IMediator mediator)
        {
            _reservationQueue = reservationQueue;
            _mediator = mediator;
            _slotQueue = slotQueue;
        }

        public async Task<HttpResponseMessage> Handle(SendReservationCommand request, CancellationToken cancellationToken)
        {
            var availableSlotsQuery = new GetAvailableSlotsQuery();
            List<Slot> availableSlots = await _mediator.Send(availableSlotsQuery);

            if (!availableSlots.Exists(x => x.Id == request.Reservation.Slot.Id))
            {
                return new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.FailedDependency, ReasonPhrase = "Error: The selected Slot is not Available." };
            }
            else if (availableSlots.Count <= 0)
            {
                return new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.FailedDependency, ReasonPhrase = "Error: There are no Available Slots at the moment." };
            }

            //Update slot to IsAvailable = false
            var slotQuery = new GetSlotByIdQuery(request.Reservation.Slot.Id);
            var slot = await _mediator.Send(slotQuery);
            slot.IsAvailable = false;
            slot.UpdatedDate = DateTime.Now;
            var slotCommand = new SendSlotCommand(slot);
            await _mediator.Send(slotCommand);

            //Save reservation
            await _reservationQueue.SendAddReservation(request.Reservation);

            return new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.OK};
        }
    }
}
