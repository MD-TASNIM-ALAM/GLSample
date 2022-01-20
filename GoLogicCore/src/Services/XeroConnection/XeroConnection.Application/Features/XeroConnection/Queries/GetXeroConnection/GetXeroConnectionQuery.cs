using MediatR;
using Shared.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace XeroConnection.Application.Queries.GetXeroConnection
{
    public class GetXeroConnectionQuery : IRequest<Result<GetXeroConnectionViewModel>>
    {
       
    }
}
