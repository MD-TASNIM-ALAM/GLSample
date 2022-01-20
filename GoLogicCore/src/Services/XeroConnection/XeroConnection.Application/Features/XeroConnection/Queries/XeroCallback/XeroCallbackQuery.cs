using MediatR;
using Shared.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using XeroConnection.Application.Queries.GetXeroConnection;

namespace XeroConnection.Application.Features.XeroConnection.Queries.XeroCallback
{
    public class XeroCallbackQuery : IRequest<Result<XeroCallbackViewModel>>
    {
        public string Code { get; set; }
        public string State { get; set; }
    }
}
