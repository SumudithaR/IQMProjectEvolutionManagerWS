using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using IQMProjectEvolutionManager.Core.Interfaces.Domain;
using IQMProjectEvolutionManagerWS.Notify.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;

namespace IQMProjectEvolutionManagerWS.Notify.Authentication
{
    public static class GoogleAuthenticator
    {
        public static UserCredential GetAuthenticatedCredential(string accessName, IEnumerable<string> scopes)
        {
            UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = ConfigurationManager.AppSettings["ClientId"],
                    ClientSecret = ConfigurationManager.AppSettings["ClientSecret"],
                }, scopes, accessName, CancellationToken.None).Result;

            return credential;
        }
    }
}