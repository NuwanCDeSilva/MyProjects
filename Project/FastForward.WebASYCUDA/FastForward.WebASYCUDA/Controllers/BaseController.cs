using FastForward.WebASYCUDA.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Resources;
using FastForward.WebASYCUDA.Services;
using FF.BusinessObjects;
using System.Globalization;
namespace FastForward.WebASYCUDA.Controllers
{
    public class BaseController : Controller
    {
        //public readonly string GlbVersionNo = "";
        //public static string GlbUserSessionID = string.Empty;
        //public static string GlbUserComCode = string.Empty;
        //public static string GlbUserID = string.Empty;
        //public static bool GlbIsExit = false;
        //public static string GlbUserIP = string.Empty;
        //public static string GlbHostName = string.Empty;
        private ChannelOperator channelService = new ChannelOperator();

        //public BaseController() {
        //    GlbVersionNo = System.Configuration.ConfigurationManager.AppSettings.Get("GlbVersionNo");
        //}
        public ChannelOperator CHNLSVC
        {
            get
            {
                return channelService;
            }
        }
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = null;

            // Attempt to read the culture cookie from Request
            HttpCookie cultureCookie = Request.Cookies["_culture"];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ? Request.UserLanguages[0] : null; // obtain it from HTTP header AcceptLanguages

            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe


            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;


            return base.BeginExecuteCore(callback, state);
        }
        public void SetLoginCacheLayer(LoginUser _loginUSER, string _winLogonname, string _winUser)
        {
            _loginUSER.User_session_id = Convert.ToString(CHNLSVC.Security.SaveLoginSession((string)Session["GlbUserID"], (string)Session["GlbUserComCode"], (string)Session["GlbUserIP"], (string)Session["GlbHostName"], _winLogonname, _winUser));

             Session["GlbUserSessionID"] = _loginUSER.User_session_id;
        }
        public string FirstCharToUpper(string text)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());
        }
    }
}