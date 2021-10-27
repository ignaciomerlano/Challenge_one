using Challenge_one.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_one.MsSql.AppSettingRepository
{
    public class AppSettingRepository : IAppSettingRepository
    {
        public async Task<AppSetting> GetAppSettingValueByKey(string AppKey)
        {
            using (SqlConnection conn = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Challenge_oneDB;Trusted_Connection=True;"))
            {
                SqlCommand command = new SqlCommand("GetAppSettingValueByKey", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@AppKey", AppKey);
                await conn.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();
                AppSetting appSetting = new AppSetting();
                while (reader.Read())
                {   
                    appSetting.Id = (int)reader["Id"];
                    appSetting.AppKey = reader["AppKey"].ToString();
                    appSetting.AppValue = reader["AppValue"].ToString();
                    appSetting.UpdatedDate = (DateTime)reader["UpdatedDate"];
                    appSetting.CreatedDate = (DateTime)reader["CreatedDate"];
                }
                await conn.CloseAsync();
                return appSetting;
            }
        }
    }
}
