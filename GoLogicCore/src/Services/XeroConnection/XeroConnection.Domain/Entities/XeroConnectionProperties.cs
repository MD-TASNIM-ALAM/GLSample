using System;
using System.Collections.Generic;
using System.Text;
using XeroConnection.Core.Entities.Base;

namespace XeroConnection.Core.Entities
{
    public class XeroConnectionProperties : EntityBase
    {
        public string XeroLoginUrl { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Tenant { get; set; }
        public string IdToken { get; set; }
        public bool IsConnected {get; set;}
        public DateTime ExpiresAtUtc { get; set; }


        public XeroConnectionProperties()
        {
            CreatedDate = DateTime.UtcNow;
        }
    }
}
