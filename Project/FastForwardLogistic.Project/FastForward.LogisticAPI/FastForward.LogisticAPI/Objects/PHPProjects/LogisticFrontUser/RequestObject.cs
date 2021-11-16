using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FastForward.LogisticAPI.Objects.PHPProjects.LogisticFrontUser
{
    public class RequestObject
    {
        public string main_services { get; set;}
        public string sub_services { get; set;}
        public string name { get; set;}
        public string company_name { get; set; }
        public string address_1 { get; set; }
        public string address_2 { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string email { get; set; }
        public string telephone { get; set; }
        public string enquiry { get; set; }
        public string cus_code { get; set; }
    }
}