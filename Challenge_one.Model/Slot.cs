using System;

namespace Challenge_one.Model
{
    public class Slot
    {
        public Guid SlotId { get; set; }
        public int Number { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
