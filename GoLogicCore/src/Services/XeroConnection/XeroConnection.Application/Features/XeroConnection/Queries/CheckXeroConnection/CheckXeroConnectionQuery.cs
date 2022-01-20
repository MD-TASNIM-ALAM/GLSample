using MediatR;
using Shared.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace XeroConnection.Application.Features.XeroConnection.Queries.CheckXeroConnection
{
    public class CheckXeroConnectionQuery : IRequest<Result<CheckXeroConnectionViewModel>>
    {

    }
}
