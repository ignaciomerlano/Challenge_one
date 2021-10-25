using Challenge_one.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Challenge_one.Aplication.Command
{
    public class SendUpdateSlotCommand : IRequest
    {
        public Slot Slot { get; }
        public SendUpdateSlotCommand(Slot slot)
        {
            Slot = slot;
        }
    }
}
