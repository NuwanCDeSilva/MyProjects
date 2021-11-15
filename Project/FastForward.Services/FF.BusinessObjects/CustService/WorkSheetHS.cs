using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.CustService
{//Dulaj 2018/Dec/26
   public class WorkSheetHS
    {
       public Int32 Line { get; set; }

       public Int32 MainHs { get; set; }
       public Int32 No { get; set; }
       public string HsCode { get; set; }
       public Int32 Item { get; set; }
       public string Description { get; set; }
       public string Country { get; set; }
       public string Unit { get; set; }
       public decimal Qty { get; set; }
       public decimal DecVal { get; set; }
       public decimal Addition { get; set; }
       public decimal CusVal { get; set; }

    }
}
