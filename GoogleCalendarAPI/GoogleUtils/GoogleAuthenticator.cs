using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace GoogleCalendarAPI.GoogleUtils
{
    public class GoogleAuthenticator
    {
        public static CalendarService AuthenticateOauth(string clientId, string clientSecret, string userName)
        {
            string[] scopes = new string[] {
        CalendarService.Scope.Calendar  ,  // Manage your calendars
        CalendarService.Scope.CalendarReadonly    // View your Calendars
            };

            try
            {
                // here is where we Request the user to give us access, or use the Refresh Token that was previously stored in %AppData%
                UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets { ClientId = clientId, ClientSecret = clientSecret }
                                                                    , scopes
                                                                    , userName
                                                                    , CancellationToken.None
                                                                    , new FileDataStore("Lwe.GoogleAuthenticator.store")).Result;



                CalendarService service = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Calendar API Sample",
                });
                return service;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                return null;

            }

        }
    }
}