using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class GntManualDocument
    {
        #region Private Members
        private string _mdd_bk_no;
        private string _mdd_bk_tp;
        private Int32 _mdd_cnt;
        private Int32 _mdd_current;
        private DateTime _mdd_dt;
        private Int32 _mdd_first;
        private string _mdd_issue_by;
        private string _mdd_issue_loc;
        private string _mdd_itm_cd;
        private Int32 _mdd_last;
        private Int32 _mdd_line;
        private string _mdd_loc;
        private string _mdd_prefix;
        private DateTime _mdd_receive_dt;
        private string _mdd_ref;
        private string _mdd_rem;
        private string _mdd_status;
        private string _mdd_trans_loc;
        private Int32 _mdd_used;
        private string _mdd_user;
        private Int32 _mdd_using;
        #endregion

        public string Mdd_bk_no { get { return _mdd_bk_no; } set { _mdd_bk_no = value; } }
        public string Mdd_bk_tp { get { return _mdd_bk_tp; } set { _mdd_bk_tp = value; } }
        public Int32 Mdd_cnt { get { return _mdd_cnt; } set { _mdd_cnt = value; } }
        public Int32 Mdd_current { get { return _mdd_current; } set { _mdd_current = value; } }
        public DateTime Mdd_dt { get { return _mdd_dt; } set { _mdd_dt = value; } }
        public Int32 Mdd_first { get { return _mdd_first; } set { _mdd_first = value; } }
        public string Mdd_issue_by { get { return _mdd_issue_by; } set { _mdd_issue_by = value; } }
        public string Mdd_issue_loc { get { return _mdd_issue_loc; } set { _mdd_issue_loc = value; } }
        public string Mdd_itm_cd { get { return _mdd_itm_cd; } set { _mdd_itm_cd = value; } }
        public Int32 Mdd_last { get { return _mdd_last; } set { _mdd_last = value; } }
        public Int32 Mdd_line { get { return _mdd_line; } set { _mdd_line = value; } }
        public string Mdd_loc { get { return _mdd_loc; } set { _mdd_loc = value; } }
        public string Mdd_prefix { get { return _mdd_prefix; } set { _mdd_prefix = value; } }
        public DateTime Mdd_receive_dt { get { return _mdd_receive_dt; } set { _mdd_receive_dt = value; } }
        public string Mdd_ref { get { return _mdd_ref; } set { _mdd_ref = value; } }
        public string Mdd_rem { get { return _mdd_rem; } set { _mdd_rem = value; } }
        public string Mdd_status { get { return _mdd_status; } set { _mdd_status = value; } }
        public string Mdd_trans_loc { get { return _mdd_trans_loc; } set { _mdd_trans_loc = value; } }
        public Int32 Mdd_used { get { return _mdd_used; } set { _mdd_used = value; } }
        public string Mdd_user { get { return _mdd_user; } set { _mdd_user = value; } }
        public Int32 Mdd_using { get { return _mdd_using; } set { _mdd_using = value; } }

        public static GntManualDocument Converter(DataRow row)
        {
            return new GntManualDocument
            {
                Mdd_bk_no = row["MDD_BK_NO"] == DBNull.Value ? string.Empty : row["MDD_BK_NO"].ToString(),
                Mdd_bk_tp = row["MDD_BK_TP"] == DBNull.Value ? string.Empty : row["MDD_BK_TP"].ToString(),
                Mdd_cnt = row["MDD_CNT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MDD_CNT"]),
                Mdd_current = row["MDD_CURRENT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MDD_CURRENT"]),
                Mdd_dt = row["MDD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MDD_DT"]),
                Mdd_first = row["MDD_FIRST"] == DBNull.Value ? 0 : Convert.ToInt32(row["MDD_FIRST"]),
                Mdd_issue_by = row["MDD_ISSUE_BY"] == DBNull.Value ? string.Empty : row["MDD_ISSUE_BY"].ToString(),
                Mdd_issue_loc = row["MDD_ISSUE_LOC"] == DBNull.Value ? string.Empty : row["MDD_ISSUE_LOC"].ToString(),
                Mdd_itm_cd = row["MDD_ITM_CD"] == DBNull.Value ? string.Empty : row["MDD_ITM_CD"].ToString(),
                Mdd_last = row["MDD_LAST"] == DBNull.Value ? 0 : Convert.ToInt32(row["MDD_LAST"]),
                Mdd_line = row["MDD_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["MDD_LINE"]),
                Mdd_loc = row["MDD_LOC"] == DBNull.Value ? string.Empty : row["MDD_LOC"].ToString(),
                Mdd_prefix = row["MDD_PREFIX"] == DBNull.Value ? string.Empty : row["MDD_PREFIX"].ToString(),
                Mdd_receive_dt = row["MDD_RECEIVE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MDD_RECEIVE_DT"]),
                Mdd_ref = row["MDD_REF"] == DBNull.Value ? string.Empty : row["MDD_REF"].ToString(),
                Mdd_rem = row["MDD_REM"] == DBNull.Value ? string.Empty : row["MDD_REM"].ToString(),
                Mdd_status = row["MDD_STATUS"] == DBNull.Value ? string.Empty : row["MDD_STATUS"].ToString(),
                Mdd_trans_loc = row["MDD_TRANS_LOC"] == DBNull.Value ? string.Empty : row["MDD_TRANS_LOC"].ToString(),
                Mdd_used = row["MDD_USED"] == DBNull.Value ? 0 : Convert.ToInt32(row["MDD_USED"]),
                Mdd_user = row["MDD_USER"] == DBNull.Value ? string.Empty : row["MDD_USER"].ToString(),
                Mdd_using = row["MDD_USING"] == DBNull.Value ? 0 : Convert.ToInt32(row["MDD_USING"])

            };
        }

    }
}
