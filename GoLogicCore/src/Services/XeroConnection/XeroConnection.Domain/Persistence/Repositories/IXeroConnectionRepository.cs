using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XeroConnection.Core.Entities;

namespace XeroConnection.Core.Persistence.Repositories
{
    public interface IXeroConnectionRepository
    {
        XeroConnectionProperties GetXeroConnection();
        Task<XeroConnectionProperties> CheckXeroConnection();
        Task<XeroConnectionProperties> DeleteXeroConnection();
        Task<XeroConnectionProperties> XeroCallback(string code, string state);
    }
}
