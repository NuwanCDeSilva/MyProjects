using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects
{
    public class REF_BONUS_SCHEME
    {
        public Int32 Rbh_seq { get; set; }
        public String Rbh_doc_no { get; set; }
        public DateTime Rbh_date { get; set; }
        public Int32 Rbh_fw_sale_lmt { get; set; }
        public Int32 Rbh_areas_lmt { get; set; }
        public Int32 Rbh_outs_lmt { get; set; }
        public Int32 Rbh_outs_dt_lmt { get; set; }
        public String Rbh_sales_methd { get; set; }
        public String Rbh_calc_methd { get; set; }
        public String Rbh_anal1 { get; set; }
        public String Rbh_anal2 { get; set; }
        public String Rbh_anal3 { get; set; }
        public String Rbh_create_by { get; set; }
        public DateTime Rbh_create_dt { get; set; }
        public String Rbh_mod_by { get; set; }
        public DateTime Rbh_mod_dt { get; set; }
        public String Rbh_cre_session_id { get; set; }
        public String Rbh_mod_session_id { get; set; }
        public int Rbh_act { get; set; }
        public string Rbh_com { get; set; }
        public string Rbh_man_circucd { get; set; }
        public Int32 Rbh_disc_con { get; set; }
        public Int32 Rbh_pb_cond { get; set; }
        public string Rbh_anal4 { get; set; }
        public string Rbh_anal5 { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static REF_BONUS_SCHEME Converter(DataRow row)
        {
            return new REF_BONUS_SCHEME
            {
                Rbh_seq = row["RBH_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["RBH_SEQ"].ToString()),
                Rbh_doc_no = row["RBH_DOC_NO"] == DBNull.Value ? string.Empty : row["RBH_DOC_NO"].ToString(),
                Rbh_date = row["RBH_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RBH_DATE"].ToString()),
                Rbh_fw_sale_lmt = row["RBH_FW_SALE_LMT"] == DBNull.Value ? 0 : Convert.ToInt32(row["RBH_FW_SALE_LMT"].ToString()),
                Rbh_areas_lmt = row["RBH_AREAS_LMT"] == DBNull.Value ? 0 : Convert.ToInt32(row["RBH_AREAS_LMT"].ToString()),
                Rbh_outs_lmt = row["RBH_OUTS_LMT"] == DBNull.Value ? 0 : Convert.ToInt32(row["RBH_OUTS_LMT"].ToString()),
                Rbh_outs_dt_lmt = row["RBH_OUTS_DT_LMT"] == DBNull.Value ? 0 : Convert.ToInt32(row["RBH_OUTS_DT_LMT"].ToString()),
                Rbh_sales_methd = row["RBH_SALES_METHD"] == DBNull.Value ? string.Empty : row["RBH_SALES_METHD"].ToString(),
                Rbh_calc_methd = row["RBH_CALC_METHD"] == DBNull.Value ? string.Empty : row["RBH_CALC_METHD"].ToString(),
                Rbh_anal1 = row["RBH_ANAL1"] == DBNull.Value ? string.Empty : row["RBH_ANAL1"].ToString(),
                Rbh_anal2 = row["RBH_ANAL2"] == DBNull.Value ? string.Empty : row["RBH_ANAL2"].ToString(),
                Rbh_anal3 = row["RBH_ANAL3"] == DBNull.Value ? string.Empty : row["RBH_ANAL3"].ToString(),
                Rbh_create_by = row["RBH_CREATE_BY"] == DBNull.Value ? string.Empty : row["RBH_CREATE_BY"].ToString(),
                Rbh_create_dt = row["RBH_CREATE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RBH_CREATE_DT"].ToString()),
                Rbh_mod_by = row["RBH_MOD_BY"] == DBNull.Value ? string.Empty : row["RBH_MOD_BY"].ToString(),
                Rbh_mod_dt = row["RBH_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RBH_MOD_DT"].ToString()),
                Rbh_cre_session_id = row["RBH_CRE_SESSION_ID"] == DBNull.Value ? string.Empty : row["RBH_CRE_SESSION_ID"].ToString(),
                Rbh_mod_session_id = row["RBH_MOD_SESSION_ID"] == DBNull.Value ? string.Empty : row["RBH_MOD_SESSION_ID"].ToString(),
                Rbh_act = row["RBH_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["RBH_ACT"].ToString()),
                Rbh_com = row["RBH_COM"] == DBNull.Value ? string.Empty : row["RBH_COM"].ToString(),
                Rbh_disc_con = row["RBH_DISC_CON"] == DBNull.Value ? 0 : Convert.ToInt32(row["RBH_DISC_CON"].ToString()),
                Rbh_pb_cond = row["RBH_PB_COND"] == DBNull.Value ? 0 : Convert.ToInt32(row["RBH_PB_COND"].ToString()),
                Rbh_anal4 = row["RBH_ANAL4"] == DBNull.Value ? string.Empty : row["RBH_ANAL4"].ToString(),
                Rbh_anal5 = row["RBH_ANAL5"] == DBNull.Value ? string.Empty : row["RBH_ANAL5"].ToString(),

            };
        }

        public static REF_BONUS_SCHEME Converter2(DataRow row)
        {
            return new REF_BONUS_SCHEME
            {
                Rbh_anal1 = row["RBH_ANAL1"] == DBNull.Value ? string.Empty : row["RBH_ANAL1"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }
}