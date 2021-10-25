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

        public async Task AddReservation(Reservation reservation)
        {
            using (SqlConnection conn = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Challenge_oneDB;Trusted_Connection=True;"))
            {
                SqlCommand command = new SqlCommand("AddReservation", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ReservationId", reservation.ReservationId);
                command.Parameters.AddWithValue("@CarLicensePlate", reservation.CarLicensePlate);
                command.Parameters.AddWithValue("@CarType", reservation.CarType);
                command.Parameters.AddWithValue("@CarColor", reservation.CarColor);
                command.Parameters.AddWithValue("@SlotId", reservation.Slot.Id);
                command.Parameters.AddWithValue("@CheckIn", reservation.CheckIn);
                command.Parameters.AddWithValue("@CheckOut", reservation.CheckOut);
                command.Parameters.AddWithValue("@Cost", reservation.Cost);
                command.Parameters.AddWithValue("@UpdatedDate", reservation.UpdatedDate);
                command.Parameters.AddWithValue("@CreatedDate", reservation.CreatedDate);
                await conn.OpenAsync();
                await command.ExecuteReaderAsync();
                await conn.CloseAsync();
            }
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
                command.Parameters.AddWithValue("@CarColor", reservation.CarColor);
                command.Parameters.AddWithValue("@SlotId", reservation.Slot.Id);
                command.Parameters.AddWithValue("@CheckIn", reservation.CheckIn);
                command.Parameters.AddWithValue("@CheckOut", reservation.CheckOut);
                command.Parameters.AddWithValue("@Cost", reservation.Cost);
                command.Parameters.AddWithValue("@UpdatedDate", reservation.UpdatedDate);
                command.Parameters.AddWithValue("@CreatedDate", reservation.CreatedDate);
                await conn.OpenAsync();
                await command.ExecuteReaderAsync();
                await conn.CloseAsync();
            }
        }

        public async Task<Reservation> GetReservationById(int Id)
        {
            using (SqlConnection conn = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Challenge_oneDB;Trusted_Connection=True;"))
            {
                SqlCommand command = new SqlCommand("GetReservationById", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", Id);
                await conn.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();
                Reservation reservation = new Reservation();
                while (reader.Read())
                {
                    reservation.Id = (int)reader["Id"];
                    reservation.ReservationId = (Guid)reader["ReservationId"];
                    reservation.CarLicensePlate = reader["CarLicensePlate"].ToString();
                    reservation.CarType = reader["CarType"].ToString();
                    reservation.CarColor = reader["CarColor"].ToString();
                    reservation.CheckIn = (DateTime)reader["CheckIn"];
                    if (reader["CheckOut"] != DBNull.Value)
                    {
                        reservation.CheckOut = (DateTime)reader["CheckOut"];
                    } 
                    if (reader["Cost"] != DBNull.Value)
                    {
                        reservation.Cost = (decimal)reader["Cost"];
                    }
                    var slot = new Slot{ Id = (int)reader["SlotId"] };
                    reservation.Slot = slot;
                    reservation.UpdatedDate = (DateTime)reader["UpdatedDate"];
                    reservation.CreatedDate = (DateTime)reader["CreatedDate"];
                }
                await conn.CloseAsync();
                return reservation;
            }
        }
    }
}
