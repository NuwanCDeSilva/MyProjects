using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class SAR_FOR_PD
    {
        public Int32 SFP_SEQ { get; set; }
        public String SFP_COM { get; set; }
        public Int32 SFP_PD_TP { get; set; }
        public String SFP_PAR_CD { get; set; }
        public String SFP_PD_CD { get; set; }
        public DateTime SFP_FRM_PD { get; set; }
        public DateTime SFP_TO_PD { get; set; }
        public String SFP_DESC { get; set; }
        public Int32 SFP_ACT { get; set; }
        public String SFP_CRE_BY { get; set; }
        public DateTime SFP_CRE_DT { get; set; }
        public String SFP_MOD_BY { get; set; }
        public DateTime SFP_MOD_DT { get; set; }
        public String SFP_CAL_CD { get; set; }
        public static SAR_FOR_PD Converter(DataRow row)
        {
            return new SAR_FOR_PD
            {
                SFP_SEQ = row["SFP_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SFP_SEQ"].ToString()),
                SFP_COM = row["SFP_COM"] == DBNull.Value ? string.Empty : row["SFP_COM"].ToString(),
                SFP_PD_TP = row["SFP_PD_TP"] == DBNull.Value ? 0 : Convert.ToInt32(row["SFP_PD_TP"].ToString()),
                SFP_PAR_CD = row["SFP_PAR_CD"] == DBNull.Value ? string.Empty : row["SFP_PAR_CD"].ToString(),
                SFP_PD_CD = row["SFP_PD_CD"] == DBNull.Value ? string.Empty : row["SFP_PD_CD"].ToString(),
                SFP_FRM_PD = row["SFP_FRM_PD"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SFP_FRM_PD"].ToString()),
                SFP_TO_PD = row["SFP_TO_PD"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SFP_TO_PD"].ToString()),
                SFP_DESC = row["SFP_DESC"] == DBNull.Value ? string.Empty : row["SFP_DESC"].ToString(),
                SFP_ACT = row["SFP_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SFP_ACT"].ToString()),
                SFP_CRE_BY = row["SFP_CRE_BY"] == DBNull.Value ? string.Empty : row["SFP_CRE_BY"].ToString(),
                SFP_CRE_DT = row["SFP_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SFP_CRE_DT"].ToString()),
                SFP_MOD_BY = row["SFP_MOD_BY"] == DBNull.Value ? string.Empty : row["SFP_MOD_BY"].ToString(),
                SFP_MOD_DT = row["SFP_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SFP_MOD_DT"].ToString()),
                SFP_CAL_CD = row["SFP_CAL_CD"] == DBNull.Value ? string.Empty : row["SFP_CAL_CD"].ToString()
            };
        } 
    }
}
