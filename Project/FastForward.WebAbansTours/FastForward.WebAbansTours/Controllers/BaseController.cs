using FastForward.WebAbansTours.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Resources;
using FastForward.WebAbansTours;
using FF.BusinessObjects;
using FastForward.WebAbansTours.Services;
using System.Text.RegularExpressions;
using System.Data;
using System.ComponentModel;
namespace FastForward.WebAbansTours.Controllers
{
    public class BaseController : Controller
    {

        public readonly string GlbVersionNo = ""; 
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
        public enum EnquiryStages
        {
            [Description("Cancelled")]
            Cancelled = 0,
            [Description("Pending")]
            Pending = 1,
            [Description("Quotation prepared")]
            Quotation_prepaird = 2,
            [Description("Send Quotation to customer")]
            Send_Quotation_to_customer = 3,
            [Description("Customer acceptance for quotation")]
            Customer_acceptance_for_quotation = 4,
            [Description("Invoiced")]
            Invoiced = 5,
            [Description("Customer paid")]
            Customer_paid = 6,
            [Description("Submit to customer")]
            Submit_to_customer = 7,
            [Description("Quotation Approved")]
            Quotation_Approved = 8,
            [Description("Quotation Approval Re-Set")]
            Quotation_Approved_Reset = 11,
            [Description("PO Genarated")]
            Quotation_Po_Genarated = 12,
            [Description("Invoice Reversed")]
            Invoice_Revered = 13,
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
            _loginUSER.User_session_id = Convert.ToString(CHNLSVC.Security.SaveLoginSession((string)Session["GlbUserID"], (string)Session["GlbUserComCode"], (string)Session["GlbUserIP"], (string)Session["GlbHostName"], (string)Session["_winLogonname"], (string)Session["_winUser"]));

            Session["GlbUserSessionID"] = _loginUSER.User_session_id;
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