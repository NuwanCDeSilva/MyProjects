using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class MasterUserCategory
    {
        #region Private Members
        private Boolean _mec_act;
        private string _mec_cat;
        private string _mec_cre_by;
        private DateTime _mec_cre_dt;
        private string _mec_desc;
        private string _mec_mod_by;
        private DateTime _mec_mod_dt;
        private string _mec_session_id;

        #endregion

        public Boolean Mec_act
        {
            get { return _mec_act; }
            set { _mec_act = value; }
        }
        public string Mec_cat
        {
            get { return _mec_cat; }
            set { _mec_cat = value; }
        }
        public string Mec_cre_by
        {
            get { return _mec_cre_by; }
            set { _mec_cre_by = value; }
        }
        public DateTime Mec_cre_dt
        {
            get { return _mec_cre_dt; }
            set { _mec_cre_dt = value; }
        }
        public string Mec_desc
        {
            get { return _mec_desc; }
            set { _mec_desc = value; }
        }
        public string Mec_mod_by
        {
            get { return _mec_mod_by; }
            set { _mec_mod_by = value; }
        }
        public DateTime Mec_mod_dt
        {
            get { return _mec_mod_dt; }
            set { _mec_mod_dt = value; }
        }
        public string Mec_session_id
        {
            get { return _mec_session_id; }
            set { _mec_session_id = value; }
        }
        

   

        public static MasterUserCategory BaseConverter(DataRow row)
        {
            return new MasterUserCategory
            {
                Mec_act = row["MEC_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MEC_ACT"]),
                Mec_cat = row["MEC_CAT"] == DBNull.Value ? string.Empty : row["MEC_CAT"].ToString(),
                Mec_cre_by = row["MEC_CRE_BY"] == DBNull.Value ? string.Empty : row["MEC_CRE_BY"].ToString(),
                Mec_cre_dt = row["MEC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MEC_CRE_DT"]),
                Mec_desc = row["MEC_DESC"] == DBNull.Value ? string.Empty : row["MEC_DESC"].ToString(),
                Mec_mod_by = row["MEC_MOD_BY"] == DBNull.Value ? string.Empty : row["MEC_MOD_BY"].ToString(),
                Mec_mod_dt = row["MEC_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MEC_MOD_DT"]),
                Mec_session_id = row["MEC_SESSION_ID"] == DBNull.Value ? string.Empty : row["MEC_SESSION_ID"].ToString()

            };
        }


        public static MasterUserCategory CategoryConverter(DataRow row)
        {
            return new MasterUserCategory
            {
                Mec_cat = row["MEC_CAT"] == DBNull.Value ? string.Empty : row["MEC_CAT"].ToString(),
                Mec_desc = row["MEC_DESC"] == DBNull.Value ? string.Empty : row["MEC_DESC"].ToString()
            };
        }
    }
}
