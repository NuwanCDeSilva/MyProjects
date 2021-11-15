using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    //Written by Udesh 12-Nov-2018
    //Table - REF_BIN_ASSIGN
    public class REF_BIN_ASSIGN
    {
        #region Private Members
        private string _rba_seq;
        private string _rba_bnk_id;
        private string _rba_tp;
        private DateTime _rba_frm_dt;
        private DateTime _rba_to_dt;
        private string _rba_bin_no;
        private Boolean _rba_act;
        private string _rba_session_id;
        private string _rba_cre_by;
        private DateTime _rba_cre_dt;
        private string _rba_mod_by;
        private DateTime _rba_mod_dt;
        #endregion

        #region Public Property Definition
        public string RBA_SEQ
        {
            get { return _rba_seq; }
            set { _rba_seq = value; }
        }
        public string RBA_BNK_ID
        {
            get { return _rba_bnk_id; }
            set { _rba_bnk_id = value; }
        }
        public string RBA_TP
        {
            get { return _rba_tp; }
            set { _rba_tp = value; }
        }
        public DateTime RBA_FRM_DT
        {
            get { return _rba_frm_dt; }
            set { _rba_frm_dt = value; }
        }
        public DateTime RBA_TO_DT
        {
            get { return _rba_to_dt; }
            set { _rba_to_dt = value; }
        }
        public string RBA_BIN_NO
        {
            get { return _rba_bin_no; }
            set { _rba_bin_no = value; }
        }
        public Boolean RBA_ACT
        {
            get { return _rba_act; }
            set { _rba_act = value; }
        }
        public string RBA_SESSION_ID
        {
            get { return _rba_session_id; }
            set { _rba_session_id = value; }
        }
        public string RBA_CRE_BY
        {
            get { return _rba_cre_by; }
            set { _rba_cre_by = value; }
        }
        public DateTime RBA_CRE_DT
        {
            get { return _rba_cre_dt; }
            set { _rba_cre_dt = value; }
        }
        public string RBA_MOD_BY
        {
            get { return _rba_mod_by; }
            set { _rba_mod_by = value; }
        }
        public DateTime RBA_MOD_DT
        {
            get { return _rba_mod_dt; }
            set { _rba_mod_dt = value; }
        }
        #endregion

        public static REF_BIN_ASSIGN Converter(DataRow row)
        {
            return new REF_BIN_ASSIGN
            {
                RBA_SEQ = row["RBA_SEQ"] == DBNull.Value ? string.Empty : row["RBA_SEQ"].ToString(),
                RBA_BNK_ID = row["RBA_BNK_ID"] == DBNull.Value ? string.Empty : row["RBA_BNK_ID"].ToString(),
                RBA_TP = row["RBA_TP"] == DBNull.Value ? string.Empty : row["RBA_TP"].ToString(),
                RBA_FRM_DT = row["RBA_FRM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RBA_FRM_DT"]),
                RBA_TO_DT = row["RBA_FRM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RBA_FRM_DT"]),
                RBA_BIN_NO = row["RBA_BIN_NO"] == DBNull.Value ? string.Empty : row["RBA_BIN_NO"].ToString(),
                RBA_ACT = row["RBA_ACT"] == DBNull.Value ? false : Convert.ToBoolean(int.Parse(row["RBA_ACT"].ToString())),
                RBA_SESSION_ID = row["RBA_SESSION_ID"] == DBNull.Value ? string.Empty : row["RBA_SESSION_ID"].ToString(),
                RBA_CRE_BY = row["RBA_CRE_BY"] == DBNull.Value ? string.Empty : row["RBA_CRE_BY"].ToString(),
                RBA_CRE_DT = row["RBA_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RBA_CRE_DT"]),
                RBA_MOD_BY = row["RBA_MOD_BY"] == DBNull.Value ? string.Empty : row["RBA_MOD_BY"].ToString(),
                RBA_MOD_DT = row["RBA_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RBA_MOD_DT"])
            };
        }
    }
}
