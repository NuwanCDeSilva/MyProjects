using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
   public class ref_bud_ele_form
    {
       public string RBEF_CODE { get; set; }
       public string RBEF_MAIN_CD { get; set; }
       public Int32 RBEF_ORD { get; set; }
       public string RBEF_DESC { get; set; }
       public string RBEF_MAIN_DESC { get; set; }
       public decimal RBEF_DEF_VAL { get; set; }
       public string RBEF_ID { get; set; }
       public string RBEF_NAME { get; set; }
       public string RBEF_CSS { get; set; }
       public string RBEF_TP { get; set; }
       public string RBEF_FPRMUL1 { get; set; }
       public string RBEF_FPRMUL2 { get; set; }
       public string RBEF_FPRMUL3 { get; set; }
       public string RBEF_FPRMUL4 { get; set; }
       public string RBEF_FPRMUL5 { get; set; }
       public string RBEF_FPRMULCOMM { get; set; }
       public string RBEF_EFFECT_ID { get; set; }
       public string RBEF_COM { get; set; }
       public Int32 RBEF_ACT { get; set; }
       public Int32 RBEF_MAIN_ORD { get; set; }
       public string RBEF_BASE_ID { get; set; }

       public static ref_bud_ele_form Converter(DataRow row)
       {
           return new ref_bud_ele_form
           {
               RBEF_CODE = row["RBEF_CODE"] == DBNull.Value ? string.Empty : row["RBEF_CODE"].ToString(),
               RBEF_MAIN_CD = row["RBEF_MAIN_CD"] == DBNull.Value ? string.Empty : row["RBEF_MAIN_CD"].ToString(),
               RBEF_ORD = row["RBEF_ORD"] == DBNull.Value ? 0 : Convert.ToInt32(row["RBEF_ORD"].ToString()),
               RBEF_DESC = row["RBEF_DESC"] == DBNull.Value ? string.Empty : row["RBEF_DESC"].ToString(),
               RBEF_MAIN_DESC = row["RBEF_MAIN_DESC"] == DBNull.Value ? string.Empty : row["RBEF_MAIN_DESC"].ToString(),
               RBEF_DEF_VAL = row["RBEF_DEF_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RBEF_DEF_VAL"].ToString()),
               RBEF_ID = row["RBEF_ID"] == DBNull.Value ? string.Empty : row["RBEF_ID"].ToString(),
               RBEF_NAME = row["RBEF_NAME"] == DBNull.Value ? string.Empty : row["RBEF_NAME"].ToString(),
               RBEF_CSS = row["RBEF_CSS"] == DBNull.Value ? string.Empty : row["RBEF_CSS"].ToString(),
               RBEF_TP = row["RBEF_TP"] == DBNull.Value ? string.Empty : row["RBEF_TP"].ToString(),
               RBEF_FPRMUL1 = row["RBEF_FPRMUL1"] == DBNull.Value ? string.Empty : row["RBEF_FPRMUL1"].ToString(),
               RBEF_FPRMUL2 = row["RBEF_FPRMUL2"] == DBNull.Value ? string.Empty : row["RBEF_FPRMUL2"].ToString(),
               RBEF_FPRMUL3 = row["RBEF_FPRMUL3"] == DBNull.Value ? string.Empty : row["RBEF_FPRMUL3"].ToString(),
               RBEF_FPRMUL4 = row["RBEF_FPRMUL4"] == DBNull.Value ? string.Empty : row["RBEF_FPRMUL4"].ToString(),
               RBEF_FPRMUL5 = row["RBEF_FPRMUL5"] == DBNull.Value ? string.Empty : row["RBEF_FPRMUL5"].ToString(),
               RBEF_FPRMULCOMM = row["RBEF_FPRMULCOMM"] == DBNull.Value ? string.Empty : row["RBEF_FPRMULCOMM"].ToString(),
               RBEF_EFFECT_ID = row["RBEF_EFFECT_ID"] == DBNull.Value ? string.Empty : row["RBEF_EFFECT_ID"].ToString(),
               RBEF_COM = row["RBEF_COM"] == DBNull.Value ? string.Empty : row["RBEF_COM"].ToString(),
               RBEF_ACT = row["RBEF_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["RBEF_ACT"].ToString()),
               RBEF_MAIN_ORD = row["RBEF_MAIN_ORD"] == DBNull.Value ? 0 : Convert.ToInt32(row["RBEF_MAIN_ORD"].ToString()),
               RBEF_BASE_ID = row["RBEF_BASE_ID"] == DBNull.Value ? string.Empty : row["RBEF_BASE_ID"].ToString(),
              
           };
       }
    }
}
