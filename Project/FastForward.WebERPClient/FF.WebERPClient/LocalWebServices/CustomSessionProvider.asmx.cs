using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace FF.WebERPClient.LocalWebServices
{
    /// <summary>
    /// This is a web service class for update Session variables from javascript.
    /// Created By : Miginda Geeganage.
    /// Created On : 27/03/2012
    /// Modified By :
    /// Modified On :
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CustomSessionProvider : System.Web.Services.WebService
    {
        /// <summary>
        /// Use to set Session variable value from javaScript.
        /// </summary>
        /// <param name="sessionId">Session variable Name</param>
        /// <param name="sessionValue">Session variable Value</param>
        [WebMethod(Description = "Set Session", EnableSession = true)]
        public void SetSession(string sessionId, string sessionValue)
        {
            HttpContext.Current.Session[sessionId] = sessionValue;
        }


        /// <summary>
        /// Use to get Session variable value from javaScript.
        /// </summary>
        /// <param name="sessionId">Session variable Name</param>
        [WebMethod(Description = "Get Session", EnableSession = true)]
        public string GetSession(string sessionId)
        {
            string result = "";
            if (HttpContext.Current.Session[sessionId] != null)
            {
                result = (string)HttpContext.Current.Session[sessionId];
            }
            return result;
        }


        [WebMethod(EnableSession = true)]
        public void SetGlbSearchResult(string _result)
        {
            HttpContext.Current.Session["GlbSearchResult"] = _result;
        }

    }
}
