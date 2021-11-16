using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FastForward.Logistic.Models
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
        public string groupType { get; set; }
        public string CustomerType { get; set; }
        public string PayType { get; set; }
        public string UserName { get; set; }

        public string JobNo { get; set; }
        public DateTime asatdate { get; set; }
        public string InvNo { get; set; }
    }
}