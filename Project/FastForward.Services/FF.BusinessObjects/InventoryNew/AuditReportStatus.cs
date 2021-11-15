using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class AuditReportStatus
    {
        #region Private Members
        private Boolean _aurs_act;
        private string _aurs_code;
        private string _aurs_com;
        private string _aurs_cre_by;
        private DateTime _aurs_cre_dt;
        private string _aurs_desc;
        private Int32 _aurs_id;
        private string _aurs_main_cd;
        #endregion

        public Boolean Aurs_act
        {
            get { return _aurs_act; }
            set { _aurs_act = value; }
        }
        public string Aurs_code
        {
            get { return _aurs_code; }
            set { _aurs_code = value; }
        }
        public string Aurs_com
        {
            get { return _aurs_com; }
            set { _aurs_com = value; }
        }
        public string Aurs_cre_by
        {
            get { return _aurs_cre_by; }
            set { _aurs_cre_by = value; }
        }
        public DateTime Aurs_cre_dt
        {
            get { return _aurs_cre_dt; }
            set { _aurs_cre_dt = value; }
        }
        public string Aurs_desc
        {
            get { return _aurs_desc; }
            set { _aurs_desc = value; }
        }
        public Int32 Aurs_id
        {
            get { return _aurs_id; }
            set { _aurs_id = value; }
        }
        public string Aurs_main_cd
        {
            get { return _aurs_main_cd; }
            set { _aurs_main_cd = value; }
        }

        public static AuditReportStatus Converter(DataRow row)
        {
            return new AuditReportStatus
            {
                Aurs_act = row["AURS_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["AURS_ACT"]),
                Aurs_code = row["AURS_CODE"] == DBNull.Value ? string.Empty : row["AURS_CODE"].ToString(),
                Aurs_com = row["AURS_COM"] == DBNull.Value ? string.Empty : row["AURS_COM"].ToString(),
                Aurs_cre_by = row["AURS_CRE_BY"] == DBNull.Value ? string.Empty : row["AURS_CRE_BY"].ToString(),
                Aurs_cre_dt = row["AURS_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AURS_CRE_DT"]),
                Aurs_desc = row["AURS_DESC"] == DBNull.Value ? string.Empty : row["AURS_DESC"].ToString(),
                Aurs_id = row["AURS_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["AURS_ID"]),
                Aurs_main_cd = row["AURS_MAIN_CD"] == DBNull.Value ? string.Empty : row["AURS_MAIN_CD"].ToString()

            };
        }

    }
}
