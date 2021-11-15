using FF.BusinessObjects;
using FF.SCMWebMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace FF.SCMWebMVC.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        public readonly string GlbVersionNo = "";
        //public const string GlbVersionNo = "1:0:0:228.2";
        //public  string GlbUserSessionID = string.Empty;
        //public  string GlbUserComCode = string.Empty;
        //public  string GlbUserID = string.Empty;
        //public  bool GlbIsExit = false;
        //public  string GlbUserIP = string.Empty;
        //public  string GlbHostName = string.Empty;

        private ChannelOperator channelService = new ChannelOperator();
        public BaseController()
        {
            GlbVersionNo = System.Configuration.ConfigurationManager.AppSettings.Get("GlbVersionNo");
        }
        public ChannelOperator CHNLSVC
        {
            get
            {
                return channelService;
            }
        }
       
       
        public void SetLoginCacheLayer(LoginUser _loginUSER, string _winLogonname, string _winUser)
        {
            _loginUSER.User_session_id = Convert.ToString(CHNLSVC.Security.SaveLoginSession((string)Session["GlbUserID"], (string)Session["GlbUserComCode"], (string)Session["GlbUserIP"], (string)Session["GlbHostName"], (string)Session["_winLogonname"], (string)Session["_winUser"]));

            Session["SessionID"] = _loginUSER.User_session_id;
        }
        public static bool IsValidMobileOrLandNo(string mobile)
        {
            string pattern = @"^[0-9]{10}$";

            System.Text.RegularExpressions.Match match = Regex.Match(mobile.Trim(), pattern, RegexOptions.IgnoreCase);
            if (match.Success)
                return true;
            else
                return false;
        }
        public bool CheckServerDateTime(out string msg)
        {
            msg = string.Empty;
            if (CHNLSVC.Security.GetServerDateTime().Date != DateTime.Now.Date)
            {
                msg = "Your machine date conflict with the server date! \nPlease contact system administrator....";
                return false;
            }

            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            TimeZone zone = TimeZone.CurrentTimeZone;
            TimeSpan offset = zone.GetUtcOffset(DateTime.Now);

            string _serverUTC = CHNLSVC.Security.GetServerTimeZoneOffset();
            string _localUTC = offset.ToString();

            if (_serverUTC != _localUTC)
            {
                msg = "Your machine time zone conflict with the server time zone! \nPlease contact system administrator....";
                return false;
            }

            return true;
        }
        public bool validDate(string date)
        {
            DateTime temp;
            if (DateTime.TryParse(date, out temp))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool validDecemal(string num)
        {
            decimal temp;
            if (Decimal.TryParse(num, out temp))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}