using Challenge_one.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_one.Aplication.SlotQueue
{
    public interface ISlotQueue
    {
        Task SendSlot(Slot slot);
    }
}
