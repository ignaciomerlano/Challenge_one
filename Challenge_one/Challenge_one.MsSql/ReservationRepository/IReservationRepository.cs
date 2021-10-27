using Challenge_one.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_one.MsSql.ReservationRepository
{
    public interface IReservationRepository
    {
        Task AddReservation(Reservation reservation);
        Task UpdateReservation(Reservation reservation);
        Task<Reservation> GetReservationById(int Id);
        Task<Reservation> GetReservationById(Guid Id);
    }
}
