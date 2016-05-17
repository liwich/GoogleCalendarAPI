using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Google.Apis.Auth.OAuth2.Mvc;
using GoogleCalendarAPI.GoogleUtils;

namespace GoogleCalendarAPI.Controllers
{
    public class AuthCallbackController : Google.Apis.Auth.OAuth2.Mvc.Controllers.AuthCallbackController
    {
        protected override FlowMetadata FlowData
        {
            get
            {
                return new AppFlowMetadata();
            }
        }

        // GET: AuthCallback
        public ActionResult Index()
        {
            return View();
        }
    }
}