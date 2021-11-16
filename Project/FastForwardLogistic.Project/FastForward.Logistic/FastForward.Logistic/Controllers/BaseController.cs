using FastForward.Logistic.Services;
using FF.BusinessObjects.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastForward.Logistic.Controllers
{
    public class BaseController : Controller
    {
        public readonly string GlbVersionNo = "";
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

            Session["GlbUserSessionID"] = _loginUSER.User_session_id;
        }
    }
}