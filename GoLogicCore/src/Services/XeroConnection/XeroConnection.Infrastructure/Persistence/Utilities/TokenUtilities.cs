using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Xero.NetStandard.OAuth2.Token;

namespace XeroConnection.Infrastructure.Persistence.Utilities
{
    public class TokenUtilities
    {
        static string tokenFile = @"./xerotoken.json";

        public static void StoreToken(XeroOAuth2Token xeroToken)
        {
            string serializedXeroToken = JsonSerializer.Serialize(xeroToken);
            System.IO.File.WriteAllText("./xerotoken.json", serializedXeroToken);
        }

        public static XeroOAuth2Token GetStoredToken()
        {
            if (File.Exists(tokenFile))
            {
                string serializedXeroToken = System.IO.File.ReadAllText("./xerotoken.json");
                var xeroToken = JsonSerializer.Deserialize<XeroOAuth2Token>(serializedXeroToken);
                return xeroToken;
            }
            return null;
        }

        public static bool TokenExists()
        {
            string serializedXeroTokenPath = "./xerotoken.json";
            bool fileExist = File.Exists(serializedXeroTokenPath);

            return fileExist;
        }

        public static void DestroyToken()
        {
            string serializedXeroTokenPath = "./xerotoken.json";
            File.Delete(serializedXeroTokenPath);

            return;
        }
    }
}
