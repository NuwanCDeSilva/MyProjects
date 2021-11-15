using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Commission
{
   public class ArrearsAccDetails
    {
       public string hal_acc_no { get; set; }
       public decimal ARREARS { get; set; }
       public decimal CLOSING_BALANCE { get; set; }
       public string HAL_SCH_CD { get; set; }
       public string HAL_SCH_TP { get; set; }
       public decimal STANDEDARREARS { get; set; }
       public DateTime GRASDATE { get; set; }
       public DateTime SUPDATE { get; set; }
       public DateTime HPA_ACC_CRE_DT { get; set; }
       public DateTime HAL_CLS_DT { get; set; }
       public DateTime HAL_RV_DT { get; set; }
       public DateTime HAL_RLS_DT { get; set; }
       public static ArrearsAccDetails Converter(DataRow row)
       {
           return new ArrearsAccDetails
           {
               hal_acc_no = row["hal_acc_no"] == DBNull.Value ? string.Empty : row["hal_acc_no"].ToString(),
               ARREARS = row["ARREARS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ARREARS"].ToString()),
               CLOSING_BALANCE = row["CLOSING_BALANCE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CLOSING_BALANCE"].ToString()),
               HAL_SCH_CD = row["HAL_SCH_CD"] == DBNull.Value ? string.Empty : row["HAL_SCH_CD"].ToString(),
               HAL_SCH_TP = row["HAL_SCH_TP"] == DBNull.Value ? string.Empty : row["HAL_SCH_TP"].ToString(),
               STANDEDARREARS = row["STANDEDARREARS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["STANDEDARREARS"].ToString()),
               GRASDATE = row["GRASDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRASDATE"].ToString()),
               SUPDATE = row["SUPDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SUPDATE"].ToString()),
               HPA_ACC_CRE_DT = row["HPA_ACC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPA_ACC_CRE_DT"].ToString()),
               HAL_CLS_DT = row["HAL_CLS_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAL_CLS_DT"].ToString()),
               HAL_RV_DT = row["HAL_RV_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAL_RV_DT"].ToString()),
               HAL_RLS_DT = row["HAL_RLS_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAL_RLS_DT"].ToString()),
           };
       }
    }
}
