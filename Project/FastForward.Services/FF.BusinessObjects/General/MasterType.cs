using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class MasterType
    {
        #region Private Members
        private Boolean _mtp_act;
        private string _mtp_cate;
        private string _mtp_cd;
        private string _mtp_cre_by;
        private DateTime _mtp_cre_dt;
        private string _mtp_desc;
        private string _mtp_mod_by;
        private DateTime _mtp_mod_dt;
        private string _mtp_session_id;
        private string _percentage;
        #endregion

        #region public property definition
        public Boolean Mtp_act { get; set; }
        public string Mtp_cate { get; set; }
        public string Mtp_cd { get; set; }
        public string Mtp_cre_by { get; set; }
        public DateTime Mtp_cre_dt { get; set; }
        public string Mtp_desc { get; set; }
        public string Mtp_mod_by { get; set; }
        public DateTime Mtp_mod_dt { get; set; }
        public string Mtp_session_id { get; set; }
        public string  Percentage { get; set; }
        #endregion

        #region converter
        public static MasterType Converter(DataRow row)
        {
            return new MasterType
            {
                Mtp_act = row["MTP_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MTP_ACT"]),
                Mtp_cate = row["MTP_CATE"] == DBNull.Value ? string.Empty : row["MTP_CATE"].ToString(),
                Mtp_cd = row["MTP_CD"] == DBNull.Value ? string.Empty : row["MTP_CD"].ToString(),
                Mtp_cre_by = row["MTP_CRE_BY"] == DBNull.Value ? string.Empty : row["MTP_CRE_BY"].ToString(),
                Mtp_cre_dt = row["MTP_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MTP_CRE_DT"]),
                Mtp_desc = row["MTP_DESC"] == DBNull.Value ? string.Empty : row["MTP_DESC"].ToString(),
                Mtp_mod_by = row["MTP_MOD_BY"] == DBNull.Value ? string.Empty : row["MTP_MOD_BY"].ToString(),
                Mtp_mod_dt = row["MTP_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MTP_MOD_DT"]),
                Mtp_session_id = row["MTP_SESSION_ID"] == DBNull.Value ? string.Empty : row["MTP_SESSION_ID"].ToString()

            };
        }

        #endregion
    }
}
