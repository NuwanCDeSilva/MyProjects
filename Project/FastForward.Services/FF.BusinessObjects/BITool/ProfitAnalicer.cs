using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
  public  class ProfitAnalicer
    {
      public string SAPD_PB_TP_CD { get; set; }
      public string SAPD_PBK_LVL_CD { get; set; }
      public string SAPD_ERP_REF { get; set; }
      public decimal SAPD_ITM_PRICE { get; set; }
      public string SARPT_CD { get; set; }
      public DateTime SAPD_MOD_WHEN { get; set; }
      public string SRTP_MAIN_TP { get; set; }

      public static ProfitAnalicer Converter(DataRow row)
      {
          return new ProfitAnalicer
          {
              SAPD_PB_TP_CD = row["SAPD_PB_TP_CD"] == DBNull.Value ? string.Empty : row["SAPD_PB_TP_CD"].ToString(),
              SAPD_PBK_LVL_CD = row["SAPD_PBK_LVL_CD"] == DBNull.Value ? string.Empty : row["SAPD_PBK_LVL_CD"].ToString(),
              SAPD_ERP_REF = row["SAPD_ERP_REF"] == DBNull.Value ? string.Empty : row["SAPD_ERP_REF"].ToString(),
              SAPD_ITM_PRICE = row["SAPD_ITM_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_ITM_PRICE"].ToString()),
              SARPT_CD = row["SARPT_CD"] == DBNull.Value ? string.Empty : row["SARPT_CD"].ToString(),
              SAPD_MOD_WHEN = row["SAPD_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPD_MOD_WHEN"].ToString()),
              SRTP_MAIN_TP = row["SRTP_MAIN_TP"] == DBNull.Value ? string.Empty : row["SRTP_MAIN_TP"].ToString(),
          };
      }

    }
}
