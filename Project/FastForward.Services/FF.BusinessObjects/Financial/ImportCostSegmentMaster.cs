using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Financial
{
    public class ImportCostSegmentMaster
    {
        #region Private Members
        private Boolean _msse_act;
        private string _msse_cd;
        private string _msse_cre_by;
        private DateTime _msse_cre_dt;
        private string _msse_desc;
        private string _msse_mod_by;
        private DateTime _msse_mod_dt;
        private string _msse_session_id;
        #endregion
        #region property definition

        public Boolean Msse_act
        {
            get { return _msse_act; }
            set { _msse_act = value; }
        }
        public string Msse_cd
        {
            get { return _msse_cd; }
            set { _msse_cd = value; }
        }
        public string Msse_cre_by
        {
            get { return _msse_cre_by; }
            set { _msse_cre_by = value; }
        }
        public DateTime Msse_cre_dt
        {
            get { return _msse_cre_dt; }
            set { _msse_cre_dt = value; }
        }
        public string Msse_desc
        {
            get { return _msse_desc; }
            set { _msse_desc = value; }
        }
        public string Msse_mod_by
        {
            get { return _msse_mod_by; }
            set { _msse_mod_by = value; }
        }
        public DateTime Msse_mod_dt
        {
            get { return _msse_mod_dt; }
            set { _msse_mod_dt = value; }
        }
        public string Msse_session_id
        {
            get { return _msse_session_id; }
            set { _msse_session_id = value; }
        }
        #endregion
        public static ImportCostSegmentMaster Converter(DataRow row)
        {
            return new ImportCostSegmentMaster
            {
                Msse_act = row["MSSE_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MSSE_ACT"]),
                Msse_cd = row["MSSE_CD"] == DBNull.Value ? string.Empty : row["MSSE_CD"].ToString(),
                Msse_cre_by = row["MSSE_CRE_BY"] == DBNull.Value ? string.Empty : row["MSSE_CRE_BY"].ToString(),
                Msse_cre_dt = row["MSSE_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MSSE_CRE_DT"]),
                Msse_desc = row["MSSE_DESC"] == DBNull.Value ? string.Empty : row["MSSE_DESC"].ToString(),
                Msse_mod_by = row["MSSE_MOD_BY"] == DBNull.Value ? string.Empty : row["MSSE_MOD_BY"].ToString(),
                Msse_mod_dt = row["MSSE_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MSSE_MOD_DT"]),
                Msse_session_id = row["MSSE_SESSION_ID"] == DBNull.Value ? string.Empty : row["MSSE_SESSION_ID"].ToString()

            };
        }
    }
}