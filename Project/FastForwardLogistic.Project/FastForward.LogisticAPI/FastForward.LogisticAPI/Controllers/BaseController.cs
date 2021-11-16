using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FastForward.LogisticAPI.Services;

namespace FastForward.LogisticAPI.Controllers
{
    public class BaseController : Controller
    {
        private ChannelOperator channelService = new ChannelOperator();

        public ChannelOperator CHNLSVC
        {
            get
            {
                return channelService;
            }
        }
    }
}