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
                    slot.SlotId = Guid.Parse(reader["SlotId"].ToString());
                    slot.Number = int.Parse(reader["Number"].ToString());
                    slot.IsAvailable = bool.Parse(reader["IsAvailable"].ToString());
                    slot.UpdatedDate = DateTime.Parse(reader["UpdatedDate"].ToString());
                    slot.CreatedDate = DateTime.Parse(reader["CreatedDate"].ToString());
                    results.Add(slot);
                }
                return results;
            }
        }

        public async Task<Slot> GetSlotById(Guid Id)
        {
            using (SqlConnection conn = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Challenge_oneDB;Trusted_Connection=True;"))
            {
                SqlCommand command = new SqlCommand("GetSlotById", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@SlotId", Id);
                await conn.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();
                Slot slot = new Slot();
                while (reader.Read())
                {
                    slot.SlotId = Guid.Parse(reader["SlotId"].ToString());
                    slot.Number = int.Parse(reader["Number"].ToString());
                    slot.IsAvailable = bool.Parse(reader["IsAvailable"].ToString());
                    slot.UpdatedDate = DateTime.Parse(reader["UpdatedDate"].ToString());
                    slot.CreatedDate = DateTime.Parse(reader["CreatedDate"].ToString());
                }
                return slot;
            }
        }
    }
}
