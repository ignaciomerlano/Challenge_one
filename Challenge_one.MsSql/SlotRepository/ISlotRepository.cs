using Challenge_one.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_one.MsSql.SlotRepository
{
    public interface ISlotRepository
    {
        Task AddSlot(Slot slot);
        Task<List<Slot>> GetSlots();
        Task<Slot> GetSlotById(int Id);
    }
}
