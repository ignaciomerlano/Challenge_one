using Challenge_one.Model;
using RabbitMQ.Client.Core.DependencyInjection.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_one.MsSql.ReservationRepository
{
    public class ReservationRepository : IReservationRepository
    {
        readonly IQueueService _queueService;

        public ReservationRepository(IQueueService queueService)
        {
            _queueService = queueService;
        }
        public async Task UpdateReservation(Reservation reservation)
        {
            using (SqlConnection conn = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Challenge_oneDB;Trusted_Connection=True;"))
            {
                SqlCommand command = new SqlCommand("UpdateReservation", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ReservationId", reservation.ReservationId);
                command.Parameters.AddWithValue("@CarLicensePlate", reservation.CarLicensePlate);
                command.Parameters.AddWithValue("@CarType", reservation.CarType);
                command.Parameters.AddWithValue("@SlotId", reservation.Slot.SlotId);
                command.Parameters.AddWithValue("@CheckIn", reservation.CheckIn);
                command.Parameters.AddWithValue("@CheckOut", reservation.CheckOut);
                command.Parameters.AddWithValue("@Cost", reservation.Cost);
                command.Parameters.AddWithValue("@UpdatedDate", reservation.UpdatedDate);
                command.Parameters.AddWithValue("@CreatedDate", reservation.CreatedDate);
                await conn.OpenAsync();

                await _queueService.SendAsync(
                    @object: command.ExecuteNonQueryAsync(),
                exchangeName: "exchange.reservation",
                routingKey: "UpdateReservation");
            }
        }

        public async Task<Reservation> GetReservationById(Guid Id)
        {
            using (SqlConnection conn = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Challenge_oneDB;Trusted_Connection=True;"))
            {
                SqlCommand command = new SqlCommand("GetReservationById", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ReservationId", Id);
                await conn.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();
                Reservation reservation = new Reservation();
                while (reader.Read())
                {
                    reservation.ReservationId = Guid.Parse(reader["ReservationId"].ToString());
                    reservation.CarLicensePlate = reader["ReservationId"].ToString();
                    reservation.CarType = reader["ReservationId"].ToString();
                    reservation.CarColor = reader["ReservationId"].ToString();
                    reservation.CheckIn = DateTime.Parse(reader["ReservationId"].ToString());
                    reservation.CheckOut = DateTime.Parse(reader["ReservationId"].ToString());
                    reservation.Cost = decimal.Parse(reader["ReservationId"].ToString());
                    reservation.Slot.SlotId = Guid.Parse(reader["UpdatedDate"].ToString());
                    reservation.UpdatedDate = DateTime.Parse(reader["UpdatedDate"].ToString());
                    reservation.CreatedDate = DateTime.Parse(reader["CreatedDate"].ToString());
                }
                return reservation;
            }
        }
    }
}
