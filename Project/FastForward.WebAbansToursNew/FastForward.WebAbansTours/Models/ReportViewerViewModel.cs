using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FastForward.WebAbansTours.Models
{
    public class ReportViewerViewModel
    {
            public string ReportPath { get; set; }
            public string fileName { get; set; }
            public string reportName { get; set; }
            public DateTime frmDate { get; set; }
            public DateTime todate { get; set; }
            public string Customer { get; set; }
            public string selectCom { get; set; }
            public string seleUserDefPro { get; set; }
            public DataTable reportData { get; set; }
            public string itemCd { get; set; }
            public string UserId { get; set; }
            public string EnquiryNumber { get; set; }
            public Int16 EnquiryStatus { get; set; }
            public string EnquiryType { get; set; }
    } 
}