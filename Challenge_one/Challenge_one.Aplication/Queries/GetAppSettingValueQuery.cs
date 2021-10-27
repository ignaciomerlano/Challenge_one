using Challenge_one.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Challenge_one.Aplication.Queries
{
    public class GetAppSettingValueQuery : IRequest<AppSetting>
    {
        public string AppKey { get; set; }
        public GetAppSettingValueQuery(string appKey)
        {
            AppKey = appKey;
        }
    }
}
