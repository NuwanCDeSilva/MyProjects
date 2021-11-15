using System;
using System.Data;

namespace FF.BusinessObjects
{
    //===========================================================================================================
    // This code is generated by Code gen V.1
    // Computer :- ITPD11  | User :- suneth On 30-Sep-2014 02:59:48
    //===========================================================================================================
    [Serializable]
    public class Service_Job_Defects
    {
        public Int32 SRD_SEQ_NO { get; set; }

        public String SRD_JOB_NO { get; set; }

        public Int32 SRD_JOB_LINE { get; set; }

        public Int32 SRD_DEF_LINE { get; set; }

        public String SRD_DEF_TP { get; set; }

        public String SRD_DEF_RMK { get; set; }

        public Int32 SRD_ACT { get; set; }

        public String SRD_CRE_BY { get; set; }

        public DateTime SRD_CRE_DT { get; set; }

        public String SRD_MOD_BY { get; set; }

        public DateTime SRD_MOD_DT { get; set; }

        public String SRD_STAGE { get; set; } 

        public String SDT_DESC { get; set; }

        public String jbd_itm_cd { get; set; }

        public string jbd_ser1 { get; set; }

        public Int32 srd_def_repeat { get; set; }
        public string jbd_req_no { get; set; }
        public Int32 SRD_IS_MAIN_DEF { get; set; }
        public string SRD_IS_MAIN_DEF_DISPLAY { get; set; } 

        public static Service_Job_Defects ConverterLess(DataRow row)
        {
            return new Service_Job_Defects
            {
                SRD_SEQ_NO = row["SRD_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRD_SEQ_NO"].ToString()),
                SRD_JOB_NO = row["SRD_JOB_NO"] == DBNull.Value ? string.Empty : row["SRD_JOB_NO"].ToString(),
                SRD_JOB_LINE = row["SRD_JOB_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRD_JOB_LINE"].ToString()),
                SRD_DEF_LINE = row["SRD_DEF_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRD_DEF_LINE"].ToString()),
                SRD_DEF_TP = row["SRD_DEF_TP"] == DBNull.Value ? string.Empty : row["SRD_DEF_TP"].ToString(),
                SRD_DEF_RMK = row["SRD_DEF_RMK"] == DBNull.Value ? string.Empty : row["SRD_DEF_RMK"].ToString(),
                SDT_DESC = row["SDT_DESC"] == DBNull.Value ? string.Empty : row["SDT_DESC"].ToString()
            };
        }

        public static Service_Job_Defects ConverterAll(DataRow row)
        {
            return new Service_Job_Defects
            {
                SRD_SEQ_NO = row["SRD_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRD_SEQ_NO"].ToString()),
                SRD_JOB_NO = row["SRD_JOB_NO"] == DBNull.Value ? string.Empty : row["SRD_JOB_NO"].ToString(),
                SRD_JOB_LINE = row["SRD_JOB_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRD_JOB_LINE"].ToString()),
                SRD_STAGE = row["SRD_STAGE"] == DBNull.Value ? string.Empty : row["SRD_STAGE"].ToString(),
                SRD_DEF_LINE = row["SRD_DEF_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRD_DEF_LINE"].ToString()),
                SRD_DEF_TP = row["SRD_DEF_TP"] == DBNull.Value ? string.Empty : row["SRD_DEF_TP"].ToString(),
                SRD_DEF_RMK = row["SRD_DEF_RMK"] == DBNull.Value ? string.Empty : row["SRD_DEF_RMK"].ToString(),
                SRD_ACT = row["SRD_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRD_ACT"].ToString()),
                SRD_CRE_BY = row["SRD_CRE_BY"] == DBNull.Value ? string.Empty : row["SRD_CRE_BY"].ToString(),
                SRD_CRE_DT = row["SRD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRD_CRE_DT"].ToString()),
                SRD_MOD_BY = row["SRD_MOD_BY"] == DBNull.Value ? string.Empty : row["SRD_MOD_BY"].ToString(),
                SRD_MOD_DT = row["SRD_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRD_MOD_DT"].ToString()),
                SDT_DESC = row["SDT_DESC"] == DBNull.Value ? string.Empty : row["SDT_DESC"].ToString(),
                jbd_itm_cd = row["JBD_ITM_CD"] == DBNull.Value ? string.Empty : row["JBD_ITM_CD"].ToString(),
                jbd_ser1 = row["JBD_SER1"] == DBNull.Value ? string.Empty : row["JBD_SER1"].ToString(),

                SRD_IS_MAIN_DEF = row["SRD_IS_MAIN_DEF"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRD_IS_MAIN_DEF"].ToString()),
                SRD_IS_MAIN_DEF_DISPLAY = row["SRD_IS_MAIN_DEF_DISPLAY"] == DBNull.Value ? string.Empty : row["SRD_IS_MAIN_DEF_DISPLAY"].ToString()

            };
        }


        public static Service_Job_Defects ConverterAllReuest(DataRow row)
        {
            return new Service_Job_Defects
            {
                SRD_SEQ_NO = row["SRDF_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRDF_SEQ_NO"].ToString()),
                SRD_JOB_NO = row["SRDF_REQ_NO"] == DBNull.Value ? string.Empty : row["SRDF_REQ_NO"].ToString(),
                SRD_JOB_LINE = row["SRDF_REQ_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRDF_REQ_LINE"].ToString()),
                SRD_STAGE = row["SRDF_STAGE"] == DBNull.Value ? string.Empty : row["SRDF_STAGE"].ToString(),
                SRD_DEF_LINE = row["SRDF_DEF_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRDF_DEF_LINE"].ToString()),
                SRD_DEF_TP = row["SRDF_DEF_TP"] == DBNull.Value ? string.Empty : row["SRDF_DEF_TP"].ToString(),
                SRD_DEF_RMK = row["SRDF_DEF_RMK"] == DBNull.Value ? string.Empty : row["SRDF_DEF_RMK"].ToString(),
                SRD_ACT = row["SRDF_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRDF_ACT"].ToString()),
                SRD_CRE_BY = row["SRDF_CRE_BY"] == DBNull.Value ? string.Empty : row["SRDF_CRE_BY"].ToString(),
                SRD_CRE_DT = row["SRDF_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRDF_CRE_DT"].ToString()),
                SRD_MOD_BY = row["SRDF_MOD_BY"] == DBNull.Value ? string.Empty : row["SRDF_MOD_BY"].ToString(),
                SRD_MOD_DT = row["SRDF_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRDF_MOD_DT"].ToString()),
                SDT_DESC = row["SDT_DESC"] == DBNull.Value ? string.Empty : row["SDT_DESC"].ToString(),
                jbd_itm_cd = row["JRD_ITM_CD"] == DBNull.Value ? string.Empty : row["JRD_ITM_CD"].ToString(),
                jbd_ser1 = row["JRD_SER1"] == DBNull.Value ? string.Empty : row["JRD_SER1"].ToString()
            };
        } 
    }
}