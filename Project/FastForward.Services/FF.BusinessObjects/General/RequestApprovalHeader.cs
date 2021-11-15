using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class RequestApprovalHeader:RequestApprovalDetail
    {
        #region Private Members
        private string _grah_app_by;
        private DateTime _grah_app_dt;
        private int _grah_app_lvl;
        private string _grah_app_stus;
        private string _grah_app_tp;
        private string _grah_com;
        private string _grah_cre_by;
        private DateTime _grah_cre_dt;
        private string _grah_fuc_cd;
        private string _grah_loc;
        private string _grah_mod_by;
        private DateTime _grah_mod_dt;
        private string _grah_oth_loc;
        private string _grah_ref;
        private string _grah_remaks;
        private string _grah_sub_type;
        private string _grah_oth_pc;
        private string _grah_PC_text;
        private string _CustomerName;
        private string _job;
        private Int32 _grah_anal1;
        private Int32 _grah_anal2;
        private string _grah_req_rem;
        private Int32 _grah_is_alw_freitmisu;   //kapila 18/4/2017
        private string _grad_anal10;
        private string _grad_anal11;
        private DateTime _GRAD_DATE_PARAM;
       
        #endregion
        public Int32 Grah_is_alw_freitmisu { get { return _grah_is_alw_freitmisu; } set { _grah_is_alw_freitmisu = value; } }
        public string Grah_app_by { get { return _grah_app_by; } set { _grah_app_by = value; } }
        public DateTime Grah_app_dt { get { return _grah_app_dt; } set { _grah_app_dt = value; } }
        public int Grah_app_lvl { get { return _grah_app_lvl; } set { _grah_app_lvl = value; } }
        public string Grah_app_stus { get { return _grah_app_stus; } set { _grah_app_stus = value; } }
        public string Grah_app_tp { get { return _grah_app_tp; } set { _grah_app_tp = value; } }
        public string Grah_com { get { return _grah_com; } set { _grah_com = value; } }
        public string Grah_cre_by { get { return _grah_cre_by; } set { _grah_cre_by = value; } }
        public DateTime Grah_cre_dt { get { return _grah_cre_dt; } set { _grah_cre_dt = value; } }
        public string Grah_fuc_cd { get { return _grah_fuc_cd; } set { _grah_fuc_cd = value; } }
        public string Grah_loc { get { return _grah_loc; } set { _grah_loc = value; } }
        public string Grah_mod_by { get { return _grah_mod_by; } set { _grah_mod_by = value; } }
        public DateTime Grah_mod_dt { get { return _grah_mod_dt; } set { _grah_mod_dt = value; } }
        public string Grah_oth_loc { get { return _grah_oth_loc; } set { _grah_oth_loc = value; } }
        public string Grah_ref { get { return _grah_ref; } set { _grah_ref = value; } }
        public string Grah_remaks { get { return _grah_remaks; } set { _grah_remaks = value; } }
        public string Grah_sub_type { get { return _grah_sub_type; } set { _grah_sub_type = value; } }
        public string Grah_oth_pc { get { return _grah_oth_pc; } set { _grah_oth_pc = value; } }
        public string Grah_PC_text { get { return _grah_PC_text; } set { _grah_PC_text = value; } }
        public string CustomerName { get { return _CustomerName; } set { _CustomerName = value; } }
        public string JOB { get { return _job; } set { _job = value; } }
        public Int32 Grah_anal1 { get { return _grah_anal1; } set { _grah_anal1 = value; } }
        public Int32 Grah_anal2 { get { return _grah_anal2; } set { _grah_anal2 = value; } }
        public string Grah_req_rem { get { return _grah_req_rem; } set { _grah_req_rem = value; } }
        public string grad_anal10 { get { return _grad_anal10; } set { _grad_anal10 = value; } }
        public string grad_anal11 { get { return _grad_anal11; } set { _grad_anal11 = value; } }
        public DateTime GRAD_DATE_PARAM { get { return _GRAD_DATE_PARAM; } set { _GRAD_DATE_PARAM = value; } }

        public string Grah_anal3 { get; set; }//add by akila 2017/09/06
        public string Grah_anal4 { get; set; }
        public static RequestApprovalHeader Converter(DataRow row)
        {
            return new RequestApprovalHeader
            {
                Grah_app_by = row["GRAH_APP_BY"] == DBNull.Value ? string.Empty : row["GRAH_APP_BY"].ToString(),
                Grah_app_dt = row["GRAH_APP_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRAH_APP_DT"]),
                Grah_app_lvl = row["GRAH_APP_LVL"] == DBNull.Value ? 0 : Convert.ToInt16(row["GRAH_APP_LVL"]),
                Grah_app_stus = row["GRAH_APP_STUS"] == DBNull.Value ? string.Empty : row["GRAH_APP_STUS"].ToString(),
                Grah_app_tp = row["GRAH_APP_TP"] == DBNull.Value ? string.Empty : row["GRAH_APP_TP"].ToString(),
                Grah_com = row["GRAH_COM"] == DBNull.Value ? string.Empty : row["GRAH_COM"].ToString(),
                Grah_cre_by = row["GRAH_CRE_BY"] == DBNull.Value ? string.Empty : row["GRAH_CRE_BY"].ToString(),
                Grah_cre_dt = row["GRAH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRAH_CRE_DT"]),
                Grah_fuc_cd = row["GRAH_FUC_CD"] == DBNull.Value ? string.Empty : row["GRAH_FUC_CD"].ToString(),
                Grah_loc = row["GRAH_LOC"] == DBNull.Value ? string.Empty : row["GRAH_LOC"].ToString(),
                Grah_mod_by = row["GRAH_MOD_BY"] == DBNull.Value ? string.Empty : row["GRAH_MOD_BY"].ToString(),
                Grah_mod_dt = row["GRAH_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRAH_MOD_DT"]),
                Grah_oth_loc = row["GRAH_OTH_LOC"] == DBNull.Value ? string.Empty : row["GRAH_OTH_LOC"].ToString(),
                Grah_ref = row["GRAH_REF"] == DBNull.Value ? string.Empty : row["GRAH_REF"].ToString(),
                Grah_remaks = row["GRAH_REMAKS"] == DBNull.Value ? string.Empty : row["GRAH_REMAKS"].ToString(),
                Grah_sub_type = row["GRAH_SUB_TYPE"] == DBNull.Value ? string.Empty : row["GRAH_SUB_TYPE"].ToString(),
                Grah_oth_pc = row["GRAH_OTH_PC"] == DBNull.Value ? string.Empty : row["GRAH_OTH_PC"].ToString(),
                JOB = row["JOB"] == DBNull.Value ? string.Empty : row["JOB"].ToString(),
                Grah_anal1 = row["GRAH_ANAL1"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRAH_ANAL1"]),
                Grah_anal2 = row["GRAH_ANAL2"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRAH_ANAL2"]),           
                Grah_req_rem = row["GRAH_REQ_REM"] == DBNull.Value ? string.Empty : row["GRAH_REQ_REM"].ToString(),
                grad_anal10 = row["grad_anal10"] == DBNull.Value ? string.Empty : row["grad_anal10"].ToString(),
                grad_anal11 = row["grad_anal11"] == DBNull.Value ? string.Empty : row["grad_anal11"].ToString(),
                GRAD_DATE_PARAM = row["GRAD_DATE_PARAM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRAD_DATE_PARAM"])
            };
        }


     public static RequestApprovalHeader ConvertWithDetail(DataRow row)
        {
            return new RequestApprovalHeader
            {
                Grah_app_by = row["GRAH_APP_BY"] == DBNull.Value ? string.Empty : row["GRAH_APP_BY"].ToString(),
                Grah_app_dt = row["GRAH_APP_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRAH_APP_DT"]),
                Grah_app_lvl = row["GRAH_APP_LVL"] == DBNull.Value ? 0 : Convert.ToInt16(row["GRAH_APP_LVL"]),
                Grah_app_stus = row["GRAH_APP_STUS"] == DBNull.Value ? string.Empty : row["GRAH_APP_STUS"].ToString(),
                Grah_app_tp = row["GRAH_APP_TP"] == DBNull.Value ? string.Empty : row["GRAH_APP_TP"].ToString(),
                Grah_com = row["GRAH_COM"] == DBNull.Value ? string.Empty : row["GRAH_COM"].ToString(),
                Grah_cre_by = row["GRAH_CRE_BY"] == DBNull.Value ? string.Empty : row["GRAH_CRE_BY"].ToString(),
                Grah_cre_dt = row["GRAH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRAH_CRE_DT"]),
                Grah_fuc_cd = row["GRAH_FUC_CD"] == DBNull.Value ? string.Empty : row["GRAH_FUC_CD"].ToString(),
                Grah_loc = row["GRAH_LOC"] == DBNull.Value ? string.Empty : row["GRAH_LOC"].ToString(),
                Grah_mod_by = row["GRAH_MOD_BY"] == DBNull.Value ? string.Empty : row["GRAH_MOD_BY"].ToString(),
                Grah_mod_dt = row["GRAH_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRAH_MOD_DT"]),
                Grah_oth_loc = row["GRAH_OTH_LOC"] == DBNull.Value ? string.Empty : row["GRAH_OTH_LOC"].ToString(),
                Grah_ref = row["GRAH_REF"] == DBNull.Value ? string.Empty : row["GRAH_REF"].ToString(),
                Grah_remaks = row["GRAH_REMAKS"] == DBNull.Value ? string.Empty : row["GRAH_REMAKS"].ToString(),
                Grah_sub_type = row["GRAH_SUB_TYPE"] == DBNull.Value ? string.Empty : row["GRAH_SUB_TYPE"].ToString(),
                Grah_oth_pc = row["GRAH_OTH_PC"] == DBNull.Value ? string.Empty : row["GRAH_OTH_PC"].ToString(),

                Grad_anal1 = row["GRAD_ANAL1"] == DBNull.Value ? string.Empty : row["GRAD_ANAL1"].ToString(),
                Grad_anal2 = row["GRAD_ANAL2"] == DBNull.Value ? string.Empty : row["GRAD_ANAL2"].ToString(),
                Grad_anal3 = row["GRAD_ANAL3"] == DBNull.Value ? string.Empty : row["GRAD_ANAL3"].ToString(),
                Grad_anal4 = row["GRAD_ANAL4"] == DBNull.Value ? string.Empty : row["GRAD_ANAL4"].ToString(),
                Grad_anal5 = row["GRAD_ANAL5"] == DBNull.Value ? string.Empty : row["GRAD_ANAL5"].ToString(),
                Grad_date_param = row["GRAD_DATE_PARAM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRAD_DATE_PARAM"]),
                Grad_is_rt1 = row["GRAD_IS_RT1"] == DBNull.Value ? false : Convert.ToBoolean(row["GRAD_IS_RT1"]),
                Grad_is_rt2 = row["GRAD_IS_RT2"] == DBNull.Value ? false : Convert.ToBoolean(row["GRAD_IS_RT2"]),
                Grad_line = row["GRAD_LINE"] == DBNull.Value ? 0 : Convert.ToInt16(row["GRAD_LINE"]),
                Grad_ref = row["GRAD_REF"] == DBNull.Value ? string.Empty : row["GRAD_REF"].ToString(),
                Grad_req_param = row["GRAD_REQ_PARAM"] == DBNull.Value ? string.Empty : row["GRAD_REQ_PARAM"].ToString(),
                Grad_val1 = row["GRAD_VAL1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_VAL1"]),
                Grad_val2 = row["GRAD_VAL2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_VAL2"]),
                Grad_val3 = row["GRAD_VAL3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_VAL3"]),
                Grad_val4 = row["GRAD_VAL4"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_VAL4"]),
                Grad_val5 = row["GRAD_VAL5"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_VAL5"])

            };
        }


     public static RequestApprovalHeader ConverterFINSRN(DataRow row)
     {
         return new RequestApprovalHeader
         {
           
           
             Grah_fuc_cd = row["GRAH_FUC_CD"] == DBNull.Value ? string.Empty : row["GRAH_FUC_CD"].ToString(),
             Grah_loc = row["GRAH_LOC"] == DBNull.Value ? string.Empty : row["GRAH_LOC"].ToString(),
             Grah_app_stus = row["GRAH_APP_STUS"] == DBNull.Value ? string.Empty : row["GRAH_APP_STUS"].ToString(),
             Grah_ref = row["GRAH_REF"] == DBNull.Value ? string.Empty : row["GRAH_REF"].ToString(),
             Grah_remaks = row["GRAH_REMAKS"] == DBNull.Value ? string.Empty : row["GRAH_REMAKS"].ToString(),
             Grah_sub_type = row["GRAH_SUB_TYPE"] == DBNull.Value ? string.Empty : row["GRAH_SUB_TYPE"].ToString(),
             Grah_oth_pc = row["GRAH_OTH_PC"] == DBNull.Value ? string.Empty : row["GRAH_OTH_PC"].ToString(),
             Grah_cre_dt = row["GRAH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRAH_CRE_DT"]),
          

             grad_anal10 = row["grad_anal10"] == DBNull.Value ? string.Empty : row["grad_anal10"].ToString(),
             grad_anal11 = row["grad_anal11"] == DBNull.Value ? string.Empty : row["grad_anal11"].ToString(),
           

         };
     }
     public static RequestApprovalHeader ConverterPda(DataRow row)
     {
         return new RequestApprovalHeader
         {
             Grah_app_by = row["GRAH_APP_BY"] == DBNull.Value ? string.Empty : row["GRAH_APP_BY"].ToString(),
             Grah_app_dt = row["GRAH_APP_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRAH_APP_DT"]),
             Grah_app_lvl = row["GRAH_APP_LVL"] == DBNull.Value ? 0 : Convert.ToInt16(row["GRAH_APP_LVL"]),
             Grah_app_stus = row["GRAH_APP_STUS"] == DBNull.Value ? string.Empty : row["GRAH_APP_STUS"].ToString(),
             Grah_app_tp = row["GRAH_APP_TP"] == DBNull.Value ? string.Empty : row["GRAH_APP_TP"].ToString(),
             Grah_com = row["GRAH_COM"] == DBNull.Value ? string.Empty : row["GRAH_COM"].ToString(),
             Grah_cre_by = row["GRAH_CRE_BY"] == DBNull.Value ? string.Empty : row["GRAH_CRE_BY"].ToString(),
             Grah_cre_dt = row["GRAH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRAH_CRE_DT"]),
             Grah_fuc_cd = row["GRAH_FUC_CD"] == DBNull.Value ? string.Empty : row["GRAH_FUC_CD"].ToString(),
             Grah_loc = row["GRAH_LOC"] == DBNull.Value ? string.Empty : row["GRAH_LOC"].ToString(),
             Grah_mod_by = row["GRAH_MOD_BY"] == DBNull.Value ? string.Empty : row["GRAH_MOD_BY"].ToString(),
             Grah_mod_dt = row["GRAH_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRAH_MOD_DT"]),
             Grah_oth_loc = row["GRAH_OTH_LOC"] == DBNull.Value ? string.Empty : row["GRAH_OTH_LOC"].ToString(),
             Grah_ref = row["GRAH_REF"] == DBNull.Value ? string.Empty : row["GRAH_REF"].ToString(),
             Grah_remaks = row["GRAH_REMAKS"] == DBNull.Value ? string.Empty : row["GRAH_REMAKS"].ToString(),
             Grah_sub_type = row["GRAH_SUB_TYPE"] == DBNull.Value ? string.Empty : row["GRAH_SUB_TYPE"].ToString(),
             Grah_oth_pc = row["GRAH_OTH_PC"] == DBNull.Value ? string.Empty : row["GRAH_OTH_PC"].ToString(),
             JOB = row["JOB"] == DBNull.Value ? string.Empty : row["JOB"].ToString(),
             Grah_anal1 = row["GRAH_ANAL1"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRAH_ANAL1"]),
             Grah_anal2 = row["GRAH_ANAL2"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRAH_ANAL2"]),
             Grah_anal3 = row["GRAH_ANAL3"] == DBNull.Value ? string.Empty : row["GRAH_ANAL3"].ToString(),
             Grah_anal4 = row["GRAH_ANAL4"] == DBNull.Value ? string.Empty : row["GRAH_ANAL4"].ToString()
         };
     }

    }
}
