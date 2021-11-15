using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
   public class PC_WISE_SALES
    {
        public string PC_CODE { get; set; }
        public string PC_DESCRIPTION { get; set; }
        public decimal DELIVERY_SALES { get; set; }
        public decimal INVOICE_SALES { get; set; }
    }
}
