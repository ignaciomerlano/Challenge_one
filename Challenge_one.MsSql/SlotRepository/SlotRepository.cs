using Challenge_one.Model;
using RabbitMQ.Client.Core.DependencyInjection.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_one.MsSql.SlotRepository
{
    public class SlotRepository : ISlotRepository
    {
        readonly IQueueService _queueService;

        public SlotRepository(IQueueService queueService)
        {
            _queueService = queueService;
        }
        public async Task AddSlot(Slot slot)
        {
            using(SqlConnection conn = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Challenge_oneDB;Trusted_Connection=True;"))
            {
                SqlCommand command = new SqlCommand("AddSlot", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@SlotId", slot.SlotId);
                command.Parameters.AddWithValue("@Number", slot.Number);
                command.Parameters.AddWithValue("@IsAvailable", slot.IsAvailable);
                command.Parameters.AddWithValue("@UpdatedDate", slot.UpdatedDate);
                command.Parameters.AddWithValue("@CreatedDate", slot.CreatedDate);
                await conn.OpenAsync();
                await command.ExecuteNonQueryAsync();
                await conn.CloseAsync();
            }
        }

        public async Task UpdateSlot(Slot slot)
        {
            using (SqlConnection conn = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Challenge_oneDB;Trusted_Connection=True;"))
            {
                SqlCommand command = new SqlCommand("UpdateSlot", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@SlotId", slot.SlotId);
                command.Parameters.AddWithValue("@Number", slot.Number);
                command.Parameters.AddWithValue("@IsAvailable", slot.IsAvailable);
                command.Parameters.AddWithValue("@UpdatedDate", slot.UpdatedDate);
                command.Parameters.AddWithValue("@CreatedDate", slot.CreatedDate);
                await conn.OpenAsync();
                await command.ExecuteNonQueryAsync();
                await conn.CloseAsync();
            }
        }

        public async Task<List<Slot>> GetSlots()
        {
            using (SqlConnection conn = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Challenge_oneDB;Trusted_Connection=True;"))
            {
                SqlCommand command = new SqlCommand("GetSlots", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                await conn.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();
                var results = new List<Slot>();
                Slot slot;
                while (reader.Read())
                {
                    slot = new Slot();
                    //slot.Id = (int)reader["Id"];
                    slot.SlotId = (Guid)reader["SlotId"];
                    slot.Number = (int)reader["Number"];
                    slot.IsAvailable = (bool)reader["IsAvailable"];
                    slot.UpdatedDate = (DateTime)reader["UpdatedDate"];
                    slot.CreatedDate = (DateTime)reader["CreatedDate"];
                    results.Add(slot);
                }
                await conn.CloseAsync();
                return results;
            }
        }

        public async Task<Slot> GetSlotBySlotIdInternal(Guid Id)
        {
            using (SqlConnection conn = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Challenge_oneDB;Trusted_Connection=True;"))
            {
                SqlCommand command = new SqlCommand("GetSlotByGuidId", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", Id);
                await conn.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();
                Slot slot = new Slot();
                while (reader.Read())
                {
                    slot.Id = (int)reader["Id"];
                    slot.SlotId = (Guid)reader["SlotId"];
                    slot.Number = (int)reader["Number"];
                    slot.IsAvailable = (bool)reader["IsAvailable"];
                    slot.UpdatedDate = (DateTime)reader["UpdatedDate"];
                    slot.CreatedDate = (DateTime)reader["CreatedDate"];
                }
                await conn.CloseAsync();
                return slot;
            }
        }

        public async Task<Slot> GetSlotByIdInternal(int Id)
        {
            using (SqlConnection conn = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Challenge_oneDB;Trusted_Connection=True;"))
            {
                SqlCommand command = new SqlCommand("GetSlotById", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", Id);
                await conn.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();
                Slot slot = new Slot();
                while (reader.Read())
                {
                    slot.Id = (int)reader["Id"];
                    slot.SlotId = (Guid)reader["SlotId"];
                    slot.Number = (int)reader["Number"];
                    slot.IsAvailable = (bool)reader["IsAvailable"];
                    slot.UpdatedDate = (DateTime)reader["UpdatedDate"];
                    slot.CreatedDate = (DateTime)reader["CreatedDate"];
                }
                await conn.CloseAsync();
                return slot;
            }
        }

        public async Task<Slot> GetSlotBySlotId(Guid Id)
        {
            using (SqlConnection conn = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Challenge_oneDB;Trusted_Connection=True;"))
            {
                SqlCommand command = new SqlCommand("GetSlotByGuidId", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", Id);
                await conn.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();
                Slot slot = new Slot();
                while (reader.Read())
                {
                    slot.SlotId = (Guid)reader["SlotId"];
                    slot.Number = (int)reader["Number"];
                    slot.IsAvailable = (bool)reader["IsAvailable"];
                    slot.UpdatedDate = (DateTime)reader["UpdatedDate"];
                    slot.CreatedDate = (DateTime)reader["CreatedDate"];
                }
                await conn.CloseAsync();
                return slot;
            }
        }
    }
}
