using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XeroConnection.API.Functions;
using XeroConnection.Application.Features.XeroConnection.Queries.CheckXeroConnection;
using XeroConnection.Application.Features.XeroConnection.Queries.DeleteXeroConnection;
using XeroConnection.Application.Features.XeroConnection.Queries.XeroCallback;
using XeroConnection.Application.Queries.GetXeroConnection;

namespace XeroConnection.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class XeroConnectionController : BaseController
    {
        public XeroConnectionController(IMediator mediator, ILogger<XeroConnectionController> logger) : base(mediator, logger)
        {
        }

        [HttpGet]
        public async Task<JsonResult> GetXeroConnection() => await HandleControllerAction.Execute(this, new GetXeroConnectionQuery {});
        
        [HttpGet("CheckXeroConnection")]
        public async Task<JsonResult> CheckXeroConnection() => await HandleControllerAction.Execute(this, new CheckXeroConnectionQuery { });

        [HttpGet("XeroCallback")]
        public async Task<JsonResult> XeroCallback(string code, string state) => await HandleControllerAction.Execute(this, new XeroCallbackQuery { Code = code, State = state });

        [HttpDelete]
        public async Task<JsonResult> DeleteXeroConnection() => await HandleControllerAction.Execute(this, new DeleteXeroConnectionQuery { });
    }
}
