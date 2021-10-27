using Challenge_one.Model;
using Challenge_one.MsSql.AppSettingRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge_one.Aplication.Queries
{
    public class GetAppSettingValueQueryHandler : IRequestHandler<GetAppSettingValueQuery, AppSetting>
    {
        private readonly IAppSettingRepository _appSettingRepository;

        public GetAppSettingValueQueryHandler(IAppSettingRepository appStettingRepository)
        {
            _appSettingRepository = appStettingRepository;
        }

        public async Task<AppSetting> Handle(GetAppSettingValueQuery request, CancellationToken cancellationToken)
        {
            return await _appSettingRepository.GetAppSettingValueByKey(request.AppKey);
        }
    }
}

