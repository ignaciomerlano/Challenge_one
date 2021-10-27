using Challenge_one.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_one.MsSql.AppSettingRepository
{
    public interface IAppSettingRepository
    {
        Task<AppSetting> GetAppSettingValueByKey(string AppKey);
    }
}
