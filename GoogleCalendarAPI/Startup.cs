using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GoogleCalendarAPI.Startup))]
namespace GoogleCalendarAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
