using System;
using System.Data;

namespace FF.BusinessObjects
{
    //===========================================================================================================
    // This code is generated by Code gen V.1
    // Computer :- ITPD11  | User :- suneth On 02-Oct-2014 11:05:33
    //===========================================================================================================

    public class Service_Tech_Aloc_Hdr
    {
        public Int32 STH_SEQ { get; set; }

        public String STH_ALOCNO { get; set; }

        public String STH_COM { get; set; }

        public String STH_LOC { get; set; }

        public String STH_TP { get; set; }

        public String STH_JOBNO { get; set; }

        public Int32 STH_JOBLINE { get; set; }

        public String STH_EMP_CD { get; set; }

        public String STH_STUS { get; set; }

        public String STH_CRE_BY { get; set; }

        public DateTime STH_CRE_WHEN { get; set; }

        public String STH_MOD_BY { get; set; }

        public DateTime STH_MOD_WHEN { get; set; }

        public String STH_SESSION_ID { get; set; }

        public String STH_TOWN { get; set; }

        public DateTime STH_FROM_DT { get; set; }

        public DateTime STH_TO_DT { get; set; }

        public String STH_REQNO { get; set; }

        public Int32 STH_REQLINE { get; set; }

        public Int32 STH_TERMINAL { get; set; }

        public String MT_DESC { get; set; }

        public String ESEP_FIRST_NAME { get; set; }

        public String STH_JOB_ITM { get; set; }

        public String STH_SER { get; set; }

        public Int32 STH_CURR_STUS { get; set; }
        public String esep_mobi_no { get; set; }
        public String STH_JOB_REQ_NO { get; set; }
        public Int32 STH_IS_MAIN_TECH { get; set; }
        public String STH_IS_MAIN_TECH_DISPLAY { get; set; }
        
        public static Service_Tech_Aloc_Hdr Converter(DataRow row)
        {
            return new Service_Tech_Aloc_Hdr
            {
                STH_SEQ = row["STH_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["STH_SEQ"].ToString()),
                STH_ALOCNO = row["STH_ALOCNO"] == DBNull.Value ? string.Empty : row["STH_ALOCNO"].ToString(),
                STH_COM = row["STH_COM"] == DBNull.Value ? string.Empty : row["STH_COM"].ToString(),
                STH_LOC = row["STH_LOC"] == DBNull.Value ? string.Empty : row["STH_LOC"].ToString(),
                STH_TP = row["STH_TP"] == DBNull.Value ? string.Empty : row["STH_TP"].ToString(),
                STH_JOBNO = row["STH_JOBNO"] == DBNull.Value ? string.Empty : row["STH_JOBNO"].ToString(),
                STH_JOBLINE = row["STH_JOBLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["STH_JOBLINE"].ToString()),
                STH_EMP_CD = row["STH_EMP_CD"] == DBNull.Value ? string.Empty : row["STH_EMP_CD"].ToString(),
                STH_STUS = row["STH_STUS"] == DBNull.Value ? string.Empty : row["STH_STUS"].ToString(),
                STH_CRE_BY = row["STH_CRE_BY"] == DBNull.Value ? string.Empty : row["STH_CRE_BY"].ToString(),
                STH_CRE_WHEN = row["STH_CRE_WHEN"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(row["STH_CRE_WHEN"].ToString()),
                STH_MOD_BY = row["STH_MOD_BY"] == DBNull.Value ? string.Empty : row["STH_MOD_BY"].ToString(),
                STH_MOD_WHEN = row["STH_MOD_WHEN"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(row["STH_MOD_WHEN"].ToString()),
                STH_SESSION_ID = row["STH_SESSION_ID"] == DBNull.Value ? string.Empty : row["STH_SESSION_ID"].ToString(),
                STH_TOWN = row["STH_TOWN"] == DBNull.Value ? string.Empty : row["STH_TOWN"].ToString(),
                STH_FROM_DT = row["STH_FROM_DT"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(row["STH_FROM_DT"].ToString()),
                STH_TO_DT = row["STH_TO_DT"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(row["STH_TO_DT"].ToString()),
                STH_REQNO = row["STH_REQNO"] == DBNull.Value ? string.Empty : row["STH_REQNO"].ToString(),
                STH_REQLINE = row["STH_REQLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["STH_REQLINE"].ToString()),
                STH_TERMINAL = row["STH_TERMINAL"] == DBNull.Value ? 0 : Convert.ToInt32(row["STH_TERMINAL"].ToString()),
                MT_DESC = row["MT_DESC"] == DBNull.Value ? string.Empty : row["MT_DESC"].ToString(),
                ESEP_FIRST_NAME = row["ESEP_FIRST_NAME"] == DBNull.Value ? string.Empty : row["ESEP_FIRST_NAME"].ToString(),
                STH_CURR_STUS = row["STH_CURR_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["STH_CURR_STUS"].ToString()),
                esep_mobi_no = row["esep_mobi_no"] == DBNull.Value ? string.Empty : row["esep_mobi_no"].ToString()
               

            };
        }

        public static Service_Tech_Aloc_Hdr Converter1(DataRow row)
        {
            return new Service_Tech_Aloc_Hdr
            {
                STH_SEQ = row["STH_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["STH_SEQ"].ToString()),
                STH_ALOCNO = row["STH_ALOCNO"] == DBNull.Value ? string.Empty : row["STH_ALOCNO"].ToString(),
                STH_COM = row["STH_COM"] == DBNull.Value ? string.Empty : row["STH_COM"].ToString(),
                STH_LOC = row["STH_LOC"] == DBNull.Value ? string.Empty : row["STH_LOC"].ToString(),
                STH_TP = row["STH_TP"] == DBNull.Value ? string.Empty : row["STH_TP"].ToString(),
                STH_JOBNO = row["STH_JOBNO"] == DBNull.Value ? string.Empty : row["STH_JOBNO"].ToString(),
                STH_JOBLINE = row["STH_JOBLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["STH_JOBLINE"].ToString()),
                STH_EMP_CD = row["STH_EMP_CD"] == DBNull.Value ? string.Empty : row["STH_EMP_CD"].ToString(),
                STH_STUS = row["STH_STUS"] == DBNull.Value ? string.Empty : row["STH_STUS"].ToString(),
                STH_CRE_BY = row["STH_CRE_BY"] == DBNull.Value ? string.Empty : row["STH_CRE_BY"].ToString(),
                STH_CRE_WHEN = row["STH_CRE_WHEN"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(row["STH_CRE_WHEN"].ToString()),
                STH_MOD_BY = row["STH_MOD_BY"] == DBNull.Value ? string.Empty : row["STH_MOD_BY"].ToString(),
                STH_MOD_WHEN = row["STH_MOD_WHEN"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(row["STH_MOD_WHEN"].ToString()),
                STH_SESSION_ID = row["STH_SESSION_ID"] == DBNull.Value ? string.Empty : row["STH_SESSION_ID"].ToString(),
                STH_TOWN = row["STH_TOWN"] == DBNull.Value ? string.Empty : row["STH_TOWN"].ToString(),
                STH_FROM_DT = row["STH_FROM_DT"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(row["STH_FROM_DT"].ToString()),
                STH_TO_DT = row["STH_TO_DT"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(row["STH_TO_DT"].ToString()),
                STH_REQNO = row["STH_REQNO"] == DBNull.Value ? string.Empty : row["STH_REQNO"].ToString(),
                STH_REQLINE = row["STH_REQLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["STH_REQLINE"].ToString()),
                STH_TERMINAL = row["STH_TERMINAL"] == DBNull.Value ? 0 : Convert.ToInt32(row["STH_TERMINAL"].ToString()),
                MT_DESC = row["MT_DESC"] == DBNull.Value ? string.Empty : row["MT_DESC"].ToString(),
                ESEP_FIRST_NAME = row["ESEP_FIRST_NAME"] == DBNull.Value ? string.Empty : row["ESEP_FIRST_NAME"].ToString(),
                STH_CURR_STUS = row["STH_CURR_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["STH_CURR_STUS"].ToString()),
                esep_mobi_no = row["esep_mobi_no"] == DBNull.Value ? string.Empty : row["esep_mobi_no"].ToString(),
                STH_IS_MAIN_TECH = row["STH_IS_MAIN_TECH"] == DBNull.Value ? 0 : Convert.ToInt32(row["STH_IS_MAIN_TECH"].ToString()),
                STH_IS_MAIN_TECH_DISPLAY = row["STH_IS_MAIN_TECH_DISPLAY"] == DBNull.Value ? string.Empty : row["STH_IS_MAIN_TECH_DISPLAY"].ToString()


            };
        }
    
    }
}