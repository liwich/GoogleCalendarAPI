
using System;
using System.Web.Mvc;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Util.Store;
using System.Threading.Tasks;
using System.Threading;
using Google.Apis.Services;

namespace GoogleCalendarAPI.GoogleUtils
{
    //https://developers.google.com/api-client-library/dotnet/guide/aaa_oauth#web-applications-aspnet-mvc

    class AppFlowMetadata : FlowMetadata
    {
        private static readonly IAuthorizationCodeFlow flow =
          new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
          {
              ClientSecrets = new ClientSecrets
              {
                  ClientId = "532795570917-9r3579rsaduikg1pva5fnq84vel36d3v.apps.googleusercontent.com",
                  ClientSecret = "YkoQY3atg4vbLNTJKnpqo8dS"
              },
              Scopes = new[] { CalendarService.Scope.Calendar, CalendarService.Scope.CalendarReadonly},
              DataStore = new FileDataStore("Calendar.Api.Auth.Store")
          });

        public override IAuthorizationCodeFlow Flow
        {
            get
            {
                 return flow; 
            }
        }

        public override string GetUserId(Controller controller)
        {
            // In this sample we use the session to store the user identifiers.
            // That's not the best practice, because you should have a logic to identify
            // a user. You might want to use "OpenID Connect".
            // You can read more about the protocol in the following link:
            // https://developers.google.com/accounts/docs/OAuth2Login.
            var user = controller.Session["user"];
            if (user == null)
            {
                user = Guid.NewGuid();
                controller.Session["user"] = user;
            }
            return user.ToString();
        }

        public static async Task<object> Authenticate(Controller controller,CancellationToken cancellationToken)
        {
            var result = await new AuthorizationCodeMvcApp(controller, new AppFlowMetadata()).
                AuthorizeAsync(cancellationToken);
            if (result.Credential != null)
            {
                var service = new CalendarService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = result.Credential,
                    ApplicationName = "GoogleCalendarAPI"
                });

                return service;
            }
            else
            {
                return new RedirectResult(result.RedirectUri);
            }
        }
    }
}
