using Challenge_one.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_one.MsSql.ReservationRepository
{
    public interface IReservationRepository
    {
        Task UpdateReservation(Reservation reservation);
        Task<Reservation> GetReservationById(Guid Id);

    }
}
