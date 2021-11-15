using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Commission
{
  public  class Override_Commission
    {
      public string EmpCode { get; set; }
      public decimal Commission { get; set; }

      public string Epf { get; set; }

      public Int32 startdays { get; set; }
      public Int32 enddays { get; set; }

      public string Ovrt { get; set; }
      public string btuinv { get; set; }
      
    }
}
