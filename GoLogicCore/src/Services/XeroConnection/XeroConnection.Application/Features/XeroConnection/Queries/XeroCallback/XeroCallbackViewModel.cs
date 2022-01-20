using System;
using System.Collections.Generic;
using System.Text;

namespace XeroConnection.Application.Features.XeroConnection.Queries.XeroCallback
{
    public class XeroCallbackViewModel
    {
        public string Tenant { get; set; }
        public DateTime ExpiresAtUtc { get; set; }
    }
}
