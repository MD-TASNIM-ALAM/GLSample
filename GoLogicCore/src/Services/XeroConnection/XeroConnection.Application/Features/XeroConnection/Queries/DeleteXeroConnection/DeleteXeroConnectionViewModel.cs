using System;
using System.Collections.Generic;
using System.Text;

namespace XeroConnection.Application.Features.XeroConnection.Queries.DeleteXeroConnection
{
    public class DeleteXeroConnectionViewModel
    {
        public string Tenant { get; set; }
        public string IdToken { get; set; }
        public bool IsConnected { get; set; }
    }
}
