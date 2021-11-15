using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{

    /// <summary>
    /// Added by Udesh 26-Nov-2018
    /// </summary>
    public class HdrObj
    {
        public List<string> mobile { get; set; }
        public string message { get; set; }
        public string recipient { get; set; }
        public string type { get; set; }
        public string sending_user { get; set; }
        public string subsiderycode { get; set; }
        public string sessionid { get; set; }
        public string creby { get; set; }

    }

    /// <summary>
    /// Added by Udesh 26-Nov-2018
    /// </summary>
    public class GVHdrObj
    {
        public List<GvMsg> mobmsg { get; set; }
        public string recipient { get; set; }
        public string type { get; set; }
        public string sending_user { get; set; }
        public string subsiderycode { get; set; }
        public string sessionid { get; set; }
        public string creby { get; set; }

    }

    /// <summary>
    /// Added by Udesh 26-Nov-2018
    /// </summary>
    public class GvMsg
    {
        public string mobile { get; set; }
        public string message { get; set; }
    }

    /// <summary>
    /// Added by Udesh 26-Nov-2018
    /// </summary>
    public class response
    {
        public string is_error { get; set; }
        public string result { get; set; }
        public string error_code { get; set; }
        public string error_message { get; set; }
    }
}
