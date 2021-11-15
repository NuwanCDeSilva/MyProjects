using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
  public  class DeliverSale
    {
      public DateTime fromdate { get; set; }
      public DateTime todate { get; set; }
      public string com { get; set; }
      public string pc { get; set; }

      public string type { get; set; }
      public string user { get; set; }
    }
}
