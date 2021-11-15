using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
   public class Deliver_forcst_data
    {
       public string code { get; set; }
       public int preyrqty { get; set; }
       public int curryrqty { get; set; }
       public decimal yrdefprecn { get; set; }
       public int forcast { get; set; }
       public decimal varience { get; set; }
       public int days { get; set; }
       public int precurractualdate { get; set; }
       public string zitemcode { get; set; }
       public string zmodel { get; set; }
       public string zbrand { get; set; }
       public string zcat1 { get; set; }
       public string zcat2 { get; set; }
    }
}
