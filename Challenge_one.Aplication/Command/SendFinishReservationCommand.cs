using Challenge_one.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;


namespace Challenge_one.Aplication.Command
{
    public class SendFinishReservationCommand : IRequest<Reservation>
    {
        public Guid Id { get; }
        public SendFinishReservationCommand(Guid id)
        {
            Id = id;
        }
    }
}
