using System;
using System.Configuration;
using Microsoft.Owin.Security.DataHandler.Encoder;

namespace TAL.Developer.Test.Domain
{
    public class ServiceSettings
    {
        public static string ApiPort => ConfigurationManager.AppSettings.Get("API:Port");
        public static string ApiCorsOrigins => ConfigurationManager.AppSettings.Get("API:CorsOrigins");

        // OAuth
        public static string OAuthIssuer => ConfigurationManager.AppSettings.Get("OAUTH:Issuer");
        public static byte[] OAuthSecret => TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings.Get("OAUTH:Secret"));
    }
}