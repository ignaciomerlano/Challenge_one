using System;
using System.Collections.Generic;
using System.Text;

namespace Challenge_one.Model
{
    public class Reservation
    {
        public int Id { get; set; }
        public Guid ReservationId { get; set; }
        public string CarLicensePlate { get; set; }
        public string CarType { get; set; }
        public string CarColor { get; set; }
        public Slot Slot { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public decimal? Cost{ get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
