using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Xero.NetStandard.OAuth2.Client;
using Xero.NetStandard.OAuth2.Config;
using Xero.NetStandard.OAuth2.Models;
using Xero.NetStandard.OAuth2.Token;
using XeroConnection.Infrastructure.Persistence.Utilities;
using XeroConnection.Core.Entities;
using XeroConnection.Core.Persistence.Repositories;

namespace XeroConnection.Infrastructure.Repositories
{
    public class XeroConnectionRepository: IXeroConnectionRepository
    {
        private readonly ILogger<XeroConnectionRepository> _logger;
        private readonly IOptions<XeroConfiguration> _xeroConfig;
        private readonly XeroClient _xeroClient;

        public XeroConnectionRepository(
            ILogger<XeroConnectionRepository> logger,
            IOptions<XeroConfiguration> xeroConfig)
        {
            _logger = logger;
            _xeroConfig = xeroConfig;
            _xeroClient = new XeroClient(_xeroConfig.Value);
        }

        public XeroConnectionProperties GetXeroConnection()
        {

            // var client = new XeroClient(_xeroConfig.Value);
            var xeroLoginUrl = _xeroClient.BuildLoginUri();
            var xeroConnectionProperties = new XeroConnectionProperties()
            {
                XeroLoginUrl = xeroLoginUrl,
            };

            return xeroConnectionProperties;
        }

        public async Task<XeroConnectionProperties> XeroCallback(string code, string state)
        {
            // var client = new XeroClient(_xeroConfig.Value);
            var xeroToken = (XeroOAuth2Token)await _xeroClient.RequestAccessTokenAsync(code);

            List<Tenant> tenants = await _xeroClient.GetConnectionsAsync(xeroToken);

            Tenant firstTenant = tenants[0];

            TokenUtilities.StoreToken(xeroToken);

            var xeroConnectionProperties = new XeroConnectionProperties
            {
                Tenant = firstTenant.TenantId.ToString(),
                AccessToken = xeroToken.AccessToken,
                RefreshToken = xeroToken.RefreshToken,
                IdToken = xeroToken.IdToken,
                ExpiresAtUtc = xeroToken.ExpiresAtUtc,
                IsConnected = true
            };

            return xeroConnectionProperties;
        }

        public async Task<XeroConnectionProperties> DeleteXeroConnection()
        {
            var xeroConnectionProperties = new XeroConnectionProperties();

            if (TokenUtilities.GetStoredToken() != null)
            {
                var xeroToken = TokenUtilities.GetStoredToken();
                var utcTimeNow = DateTime.UtcNow;

                if (utcTimeNow > xeroToken.ExpiresAtUtc)
                {
                    xeroToken = (XeroOAuth2Token)await _xeroClient.RefreshAccessTokenAsync(xeroToken);
                    TokenUtilities.StoreToken(xeroToken);
                }

                // Tenant xeroTenant = xeroToken.Tenants[0];
                foreach (var tenant in xeroToken.Tenants)
                {
                    await _xeroClient.DeleteConnectionAsync(xeroToken, tenant);
                }

                TokenUtilities.DestroyToken();

                xeroConnectionProperties = new XeroConnectionProperties
                {
                    IdToken = xeroToken.IdToken,
                    ExpiresAtUtc = xeroToken.ExpiresAtUtc,
                    IsConnected = false
                };

                return xeroConnectionProperties;
            }

            xeroConnectionProperties = new XeroConnectionProperties
            {
                IsConnected = false
            };

            return xeroConnectionProperties;
        }

        public async Task<XeroConnectionProperties> CheckXeroConnection()
        {
            var xeroConnectionProperties = new XeroConnectionProperties();

            if (TokenUtilities.GetStoredToken() != null)
            {
                var xeroToken = TokenUtilities.GetStoredToken();
                var utcTimeNow = DateTime.UtcNow;

                if (utcTimeNow > xeroToken.ExpiresAtUtc)
                {
                    xeroToken = (XeroOAuth2Token)await _xeroClient.RefreshAccessTokenAsync(xeroToken);
                    TokenUtilities.StoreToken(xeroToken);
                }

                xeroConnectionProperties = new XeroConnectionProperties
                {
                    IsConnected = true
                };

                return xeroConnectionProperties;
            }

            xeroConnectionProperties = new XeroConnectionProperties
            {
                IsConnected = false
            };

            return xeroConnectionProperties;
        }
    }
}
