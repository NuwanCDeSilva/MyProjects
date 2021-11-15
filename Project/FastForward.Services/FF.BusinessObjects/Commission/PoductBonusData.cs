using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Commission
{
   public class PoductBonusData
    {
       public string InvoiceNo { get; set; }
       public DateTime InvoiceDate { get; set; }
       public string ExecCode { get; set; }
       public string ExecName { get; set; }
       public string ItemCode { get; set; }
       public string ItemDesc { get; set; }
       public Int32 Qty { get; set; }
       public decimal TotAmmount { get; set; }
       public decimal TotMarks { get; set; }
       public string circular { get; set; }
       public string pc { get; set; }
       public string loc { get; set; }
       public DateTime frmdate { get; set; }
       public DateTime todate { get; set; }
       public Int16 Combineno { get; set; }
       public DateTime FromsalesDate { get; set; }
       public DateTime TosalesDate { get; set; }
       public string ShowroomCat { get; set; }



    }
}
