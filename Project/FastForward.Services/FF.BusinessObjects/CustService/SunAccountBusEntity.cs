using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects
{
  public  class SunAccountBusEntity
    {
      public string accnt_code { get; set; }
      public string lookup { get; set; }
      public string ADDRESS_1 { get; set; }
      public string accnt_name { get; set; }

      public string ADDRESS_2 { get; set; }
      public string ADDRESS_3 { get; set; }
      public string TELEPHONE { get; set; }
      public string CONTACT { get; set; }
      public string TELEX { get; set; }
      public string E_MAIL { get; set; }
      public string WEB_PAGE { get; set; }
      public string PAYMNT_DAY { get; set; }
      public decimal ACC_BALNCE { get; set; }
      public decimal ORD_BALNCE { get; set; }
      public decimal CREDIT_LIM { get; set; }

      public static SunAccountBusEntity Converter(DataRow row)
      {
          return new SunAccountBusEntity
          {
              accnt_code = row["accnt_code"] == DBNull.Value ? string.Empty : row["accnt_code"].ToString(),
              lookup = row["lookup"] == DBNull.Value ? string.Empty : row["lookup"].ToString(),
              ADDRESS_1 = row["ADDRESS_1"] == DBNull.Value ? string.Empty : row["ADDRESS_1"].ToString(),
              accnt_name = row["accnt_name"] == DBNull.Value ? string.Empty : row["accnt_name"].ToString(),
              ADDRESS_2 = row["ADDRESS_2"] == DBNull.Value ? string.Empty : row["ADDRESS_2"].ToString(),
              ADDRESS_3 = row["ADDRESS_3"] == DBNull.Value ? string.Empty : row["ADDRESS_3"].ToString(),
              TELEPHONE = row["TELEPHONE"] == DBNull.Value ? string.Empty : row["TELEPHONE"].ToString(),
              CONTACT = row["CONTACT"] == DBNull.Value ? string.Empty : row["CONTACT"].ToString(),
              TELEX = row["TELEX"] == DBNull.Value ? string.Empty : row["TELEX"].ToString(),
              E_MAIL = row["E_MAIL"] == DBNull.Value ? string.Empty : row["E_MAIL"].ToString(),
              WEB_PAGE = row["WEB_PAGE"] == DBNull.Value ? string.Empty : row["WEB_PAGE"].ToString(),
              PAYMNT_DAY = row["PAYMNT_DAY"] == DBNull.Value ? string.Empty : row["PAYMNT_DAY"].ToString(),
              ACC_BALNCE = row["ACC_BALNCE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACC_BALNCE"]),
              ORD_BALNCE = row["ORD_BALNCE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ORD_BALNCE"]),
              CREDIT_LIM = row["CREDIT_LIM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CREDIT_LIM"]),
          };
      }
      public static SunAccountBusEntity Converterso(DataRow row)
      {
          return new SunAccountBusEntity
          { 
              ACC_BALNCE = row["ACC_BALNCE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACC_BALNCE"]),
              ORD_BALNCE = row["ORD_BALNCE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ORD_BALNCE"]),
              CREDIT_LIM = row["CREDIT_LIM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CREDIT_LIM"]),
          };
      }
    }
}
