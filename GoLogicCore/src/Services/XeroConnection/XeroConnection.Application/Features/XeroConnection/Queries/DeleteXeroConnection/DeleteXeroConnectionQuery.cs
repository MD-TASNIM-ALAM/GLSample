using MediatR;
using Shared.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace XeroConnection.Application.Features.XeroConnection.Queries.DeleteXeroConnection
{
    public class DeleteXeroConnectionQuery : IRequest<Result<DeleteXeroConnectionViewModel>>
    {
        public string Tenant { get; set; }
    }
}
