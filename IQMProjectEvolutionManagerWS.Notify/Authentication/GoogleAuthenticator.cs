// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GoogleAuthenticator.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   The google authenticator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Notify.Authentication
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Threading;

    using Google.Apis.Auth.OAuth2;

    /// <summary>
    /// The google authenticator.
    /// </summary>
    public static class GoogleAuthenticator
    {
        /// <summary>
        /// The get authenticated credential.
        /// </summary>
        /// <param name="accessName">
        /// The access name.
        /// </param>
        /// <param name="scopes">
        /// The scopes.
        /// </param>
        /// <returns>
        /// The <see cref="UserCredential"/>.
        /// </returns>
        public static UserCredential GetAuthenticatedCredential(string accessName, IEnumerable<string> scopes)
        {
            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = ConfigurationManager.AppSettings["ClientId"],
                    ClientSecret = ConfigurationManager.AppSettings["ClientSecret"],
                },
                scopes,
                accessName,
                CancellationToken.None).Result;

            return credential;
        }
    }
}