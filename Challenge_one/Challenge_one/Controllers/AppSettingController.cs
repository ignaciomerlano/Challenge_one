using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Challenge_one.Model;
using Challenge_one.Aplication.Command;
using Challenge_one.Aplication.Queries;

namespace Challenge_one.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/[controller]/[Action]")]
    public class AppSettingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppSettingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{AppKey}")]
        public async Task<AppSetting> GetSetting(string AppKey)
        {
            var query = new GetAppSettingValueQuery(AppKey);
            return await _mediator.Send(query);
        }
    }
}
