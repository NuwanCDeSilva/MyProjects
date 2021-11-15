using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{

    public class IncentiveSch
    {
        #region Private Members
        private Boolean _inc_allow_disc;
        private Boolean _inc_allow_fwsales;
        private Boolean _inc_all_itms;
        private string _inc_circ;
        private string _inc_cre_by;
        private DateTime _inc_cre_when;
        private DateTime _inc_dt;
        private DateTime _inc_from;
        private Int32 _inc_fwd_period;
        private string _inc_mod_by;
        private DateTime _inc_mod_when;
        private string _inc_procesed;
        private string _inc_ref;
        private string _inc_session_id;
        private string _inc_stus;
        private DateTime _inc_to;
        private string _inc_xmod_by;
        private DateTime _inc_xmod_dt;
        private Int32 _inc_xno_of_times_upd;
        private string _inc_xref;
        #endregion

        public Boolean Inc_allow_disc
        {
            get { return _inc_allow_disc; }
            set { _inc_allow_disc = value; }
        }
        public Boolean Inc_allow_fwsales
        {
            get { return _inc_allow_fwsales; }
            set { _inc_allow_fwsales = value; }
        }
        public Boolean Inc_all_itms
        {
            get { return _inc_all_itms; }
            set { _inc_all_itms = value; }
        }
        public string Inc_circ
        {
            get { return _inc_circ; }
            set { _inc_circ = value; }
        }
        public string Inc_cre_by
        {
            get { return _inc_cre_by; }
            set { _inc_cre_by = value; }
        }
        public DateTime Inc_cre_when
        {
            get { return _inc_cre_when; }
            set { _inc_cre_when = value; }
        }
        public DateTime Inc_dt
        {
            get { return _inc_dt; }
            set { _inc_dt = value; }
        }
        public DateTime Inc_from
        {
            get { return _inc_from; }
            set { _inc_from = value; }
        }
        public Int32 Inc_fwd_period
        {
            get { return _inc_fwd_period; }
            set { _inc_fwd_period = value; }
        }
        public string Inc_mod_by
        {
            get { return _inc_mod_by; }
            set { _inc_mod_by = value; }
        }
        public DateTime Inc_mod_when
        {
            get { return _inc_mod_when; }
            set { _inc_mod_when = value; }
        }
        public string Inc_procesed
        {
            get { return _inc_procesed; }
            set { _inc_procesed = value; }
        }
        public string Inc_ref
        {
            get { return _inc_ref; }
            set { _inc_ref = value; }
        }
        public string Inc_session_id
        {
            get { return _inc_session_id; }
            set { _inc_session_id = value; }
        }
        public string Inc_stus
        {
            get { return _inc_stus; }
            set { _inc_stus = value; }
        }
        public DateTime Inc_to
        {
            get { return _inc_to; }
            set { _inc_to = value; }
        }
        public string Inc_xmod_by
        {
            get { return _inc_xmod_by; }
            set { _inc_xmod_by = value; }
        }
        public DateTime Inc_xmod_dt
        {
            get { return _inc_xmod_dt; }
            set { _inc_xmod_dt = value; }
        }
        public Int32 Inc_xno_of_times_upd
        {
            get { return _inc_xno_of_times_upd; }
            set { _inc_xno_of_times_upd = value; }
        }
        public string Inc_xref
        {
            get { return _inc_xref; }
            set { _inc_xref = value; }
        }

        public static IncentiveSch Converter(DataRow row)
        {
            return new IncentiveSch
            {
                Inc_allow_disc = row["INC_ALLOW_DISC"] == DBNull.Value ? false : Convert.ToBoolean(row["INC_ALLOW_DISC"]),
                Inc_allow_fwsales = row["INC_ALLOW_FWSALES"] == DBNull.Value ? false : Convert.ToBoolean(row["INC_ALLOW_FWSALES"]),
                Inc_all_itms = row["INC_ALL_ITMS"] == DBNull.Value ? false : Convert.ToBoolean(row["INC_ALL_ITMS"]),
                Inc_circ = row["INC_CIRC"] == DBNull.Value ? string.Empty : row["INC_CIRC"].ToString(),
                Inc_cre_by = row["INC_CRE_BY"] == DBNull.Value ? string.Empty : row["INC_CRE_BY"].ToString(),
                Inc_cre_when = row["INC_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INC_CRE_WHEN"]),
                Inc_dt = row["INC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INC_DT"]),
                Inc_from = row["INC_FROM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INC_FROM"]),
                Inc_fwd_period = row["INC_FWD_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["INC_FWD_PERIOD"]),
                Inc_mod_by = row["INC_MOD_BY"] == DBNull.Value ? string.Empty : row["INC_MOD_BY"].ToString(),
                Inc_mod_when = row["INC_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INC_MOD_WHEN"]),
                Inc_procesed = row["INC_PROCESED"] == DBNull.Value ? string.Empty : row["INC_PROCESED"].ToString(),
                Inc_ref = row["INC_REF"] == DBNull.Value ? string.Empty : row["INC_REF"].ToString(),
                Inc_session_id = row["INC_SESSION_ID"] == DBNull.Value ? string.Empty : row["INC_SESSION_ID"].ToString(),
                Inc_stus = row["INC_STUS"] == DBNull.Value ? string.Empty : row["INC_STUS"].ToString(),
                Inc_to = row["INC_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INC_TO"]),
                Inc_xmod_by = row["INC_XMOD_BY"] == DBNull.Value ? string.Empty : row["INC_XMOD_BY"].ToString(),
                Inc_xmod_dt = row["INC_XMOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INC_XMOD_DT"]),
                Inc_xno_of_times_upd = row["INC_XNO_OF_TIMES_UPD"] == DBNull.Value ? 0 : Convert.ToInt32(row["INC_XNO_OF_TIMES_UPD"]),
                Inc_xref = row["INC_XREF"] == DBNull.Value ? string.Empty : row["INC_XREF"].ToString()
            };
        }

    }
}
