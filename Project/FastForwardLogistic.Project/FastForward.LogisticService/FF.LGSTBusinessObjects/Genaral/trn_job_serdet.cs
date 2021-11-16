using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Genaral
{
   public class trn_job_serdet
    {
       public string JS_SEQ_NO { get; set; }
       public Int32 JS_LINE_NO { get; set; }
       public string JS_SER_TP { get;set; }
       public string JS_PC { get; set; }
       public string JS_RMK { get; set; }
       public string JS_CRE_BY { get; set; }
       public DateTime JS_CRE_DT { get; set; }
       public string JS_MOD_BY { get; set; }
       public DateTime JS_MOD_DT { get; set; }
       public string JS_CUS_CD { get; set; }
       public string Name { get; set; }

       public static trn_job_serdet Converter(DataRow row)
       {
           return new trn_job_serdet
           {
               JS_SEQ_NO = row["JS_SEQ_NO"] == DBNull.Value ? string.Empty : row["JS_SEQ_NO"].ToString(),
               JS_LINE_NO = row["JS_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["JS_LINE_NO"].ToString()),
               JS_SER_TP = row["JS_SER_TP"] == DBNull.Value ? string.Empty : row["JS_SER_TP"].ToString(),
               JS_PC = row["JS_PC"] == DBNull.Value ? string.Empty : row["JS_PC"].ToString(),
               JS_RMK = row["JS_RMK"] == DBNull.Value ? string.Empty : row["JS_RMK"].ToString(),
               JS_CRE_BY = row["JS_CRE_BY"] == DBNull.Value ? string.Empty : row["JS_CRE_BY"].ToString(),
               JS_CRE_DT = row["JS_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JS_CRE_DT"].ToString()),
               JS_MOD_BY = row["JS_MOD_BY"] == DBNull.Value ? string.Empty : row["JS_MOD_BY"].ToString(),
               JS_MOD_DT = row["JS_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JS_MOD_DT"].ToString()),
               JS_CUS_CD = row["JS_CUS_CD"] == DBNull.Value ? string.Empty : row["JS_CUS_CD"].ToString(),
               Name = row["NAME"] == DBNull.Value ? string.Empty : row["NAME"].ToString()
           };
       }
    }
}
