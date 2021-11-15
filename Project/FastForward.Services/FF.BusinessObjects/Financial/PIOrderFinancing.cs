using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Financial
{
   public class PIOrderFinancing
    {

        #region Private Members
       private Int32 _ifp_act;
        private string _ifp_amd_by;
        private DateTime _ifp_amd_dt;
        private string _ifp_anal_1;
        private string _ifp_anal_2;
        private string _ifp_cre_by;
        private DateTime _ifp_cre_dt;
        private string _ifp_doc_no;
        private decimal _ifp_ex_rt;
        private Boolean _ifp_is_pi_amd;
        private int _ifp_line;
        private string _ifp_mod_by;
        private DateTime _ifp_mod_dt;
        private string _ifp_pi_no;
        private Int32 _ifp_pi_seq_no;
        private Int32 _ifp_seq_no;
        private string _ifp_session_id;
        private decimal _ifp_tot_amt;
        private decimal _ifp_tot_amt_deal;
        #endregion
        #region public property definition
        public Int32 Ifp_act
        {
            get { return _ifp_act; }
            set { _ifp_act = value; }
        }
        public string Ifp_amd_by
        {
            get { return _ifp_amd_by; }
            set { _ifp_amd_by = value; }
        }
        public DateTime Ifp_amd_dt
        {
            get { return _ifp_amd_dt; }
            set { _ifp_amd_dt = value; }
        }
        public string Ifp_anal_1
        {
            get { return _ifp_anal_1; }
            set { _ifp_anal_1 = value; }
        }
        public string Ifp_anal_2
        {
            get { return _ifp_anal_2; }
            set { _ifp_anal_2 = value; }
        }
        public string Ifp_cre_by
        {
            get { return _ifp_cre_by; }
            set { _ifp_cre_by = value; }
        }
        public DateTime Ifp_cre_dt
        {
            get { return _ifp_cre_dt; }
            set { _ifp_cre_dt = value; }
        }
        public string Ifp_doc_no
        {
            get { return _ifp_doc_no; }
            set { _ifp_doc_no = value; }
        }
        public decimal Ifp_ex_rt
        {
            get { return _ifp_ex_rt; }
            set { _ifp_ex_rt = value; }
        }
        public Boolean Ifp_is_pi_amd
        {
            get { return _ifp_is_pi_amd; }
            set { _ifp_is_pi_amd = value; }
        }
        public int Ifp_line
        {
            get { return _ifp_line; }
            set { _ifp_line = value; }
        }
        public string Ifp_mod_by
        {
            get { return _ifp_mod_by; }
            set { _ifp_mod_by = value; }
        }
        public DateTime Ifp_mod_dt
        {
            get { return _ifp_mod_dt; }
            set { _ifp_mod_dt = value; }
        }
        public string Ifp_pi_no
        {
            get { return _ifp_pi_no; }
            set { _ifp_pi_no = value; }
        }
        public Int32 Ifp_pi_seq_no
        {
            get { return _ifp_pi_seq_no; }
            set { _ifp_pi_seq_no = value; }
        }
        public Int32 Ifp_seq_no
        {
            get { return _ifp_seq_no; }
            set { _ifp_seq_no = value; }
        }
        public string Ifp_session_id
        {
            get { return _ifp_session_id; }
            set { _ifp_session_id = value; }
        }
        public decimal Ifp_tot_amt
        {
            get { return _ifp_tot_amt; }
            set { _ifp_tot_amt = value; }
        }
        public decimal Ifp_tot_amt_deal
        {
            get { return _ifp_tot_amt_deal; }
            set { _ifp_tot_amt_deal = value; }
        }
        #endregion
        public static PIOrderFinancing Converter(DataRow row)
        {
            return new PIOrderFinancing
            {

                Ifp_act = row["IFP_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["IFP_ACT"]),
                Ifp_amd_by = row["IFP_AMD_BY"] == DBNull.Value ? string.Empty : row["IFP_AMD_BY"].ToString(),
                Ifp_amd_dt = row["IFP_AMD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IFP_AMD_DT"]),
                Ifp_anal_1 = row["IFP_ANAL_1"] == DBNull.Value ? string.Empty : row["IFP_ANAL_1"].ToString(),
                Ifp_anal_2 = row["IFP_ANAL_2"] == DBNull.Value ? string.Empty : row["IFP_ANAL_2"].ToString(),
                Ifp_cre_by = row["IFP_CRE_BY"] == DBNull.Value ? string.Empty : row["IFP_CRE_BY"].ToString(),
                Ifp_cre_dt = row["IFP_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IFP_CRE_DT"]),
                Ifp_doc_no = row["IFP_DOC_NO"] == DBNull.Value ? string.Empty : row["IFP_DOC_NO"].ToString(),
                Ifp_ex_rt = row["IFP_EX_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IFP_EX_RT"]),
                Ifp_is_pi_amd = row["IFP_IS_PI_AMD"] == DBNull.Value ? false : Convert.ToBoolean(row["IFP_IS_PI_AMD"]),
                Ifp_line = row["IFP_LINE"] == DBNull.Value ? 0 : Convert.ToInt16(row["IFP_LINE"]),
                Ifp_mod_by = row["IFP_MOD_BY"] == DBNull.Value ? string.Empty : row["IFP_MOD_BY"].ToString(),
                Ifp_mod_dt = row["IFP_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IFP_MOD_DT"]),
                Ifp_pi_no = row["IFP_PI_NO"] == DBNull.Value ? string.Empty : row["IFP_PI_NO"].ToString(),
               // Ifp_pi_seq_no = row["IFP_PI_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["IFP_PI_SEQ_NO"]),
                Ifp_seq_no = row["IFP_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["IFP_SEQ_NO"]),
                Ifp_session_id = row["IFP_SESSION_ID"] == DBNull.Value ? string.Empty : row["IFP_SESSION_ID"].ToString(),
                Ifp_tot_amt = row["IFP_TOT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IFP_TOT_AMT"]),
                Ifp_tot_amt_deal = row["IFP_TOT_AMT_DEAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IFP_TOT_AMT_DEAL"])

            };
        }
    }
}

