using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Financial
{
  public  class SunAALRec
    {
      public string sard_ref_no { get; set; }
      public decimal sard_settle_amt { get; set; }
      public string sard_deposit_bank_cd { get; set; }

      public static SunAALRec Converter(DataRow row)
      {
          return new SunAALRec
          {
              sard_ref_no = row["sard_ref_no"] == DBNull.Value ? string.Empty : row["sard_ref_no"].ToString(),
              sard_settle_amt = row["sard_settle_amt"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sard_settle_amt"].ToString()),
              sard_deposit_bank_cd = row["sard_deposit_bank_cd"] == DBNull.Value ? string.Empty : row["sard_deposit_bank_cd"].ToString(),
          };
      }
    }
}
