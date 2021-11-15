using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class AuditStatus
    {
        #region Private Members
        private Boolean _auss_act;
        private string _auss_code;
        private string _auss_cre_by;
        private DateTime _auss_cre_dt;
        private string _auss_desc;
        private Int32 _auss_direction;
        private Int32 _auss_id;
        #endregion

        public Boolean Auss_act
        {
            get { return _auss_act; }
            set { _auss_act = value; }
        }
        public string Auss_code
        {
            get { return _auss_code; }
            set { _auss_code = value; }
        }
        public string Auss_cre_by
        {
            get { return _auss_cre_by; }
            set { _auss_cre_by = value; }
        }
        public DateTime Auss_cre_dt
        {
            get { return _auss_cre_dt; }
            set { _auss_cre_dt = value; }
        }
        public string Auss_desc
        {
            get { return _auss_desc; }
            set { _auss_desc = value; }
        }
        public Int32 Auss_direction
        {
            get { return _auss_direction; }
            set { _auss_direction = value; }
        }
        public Int32 Auss_id
        {
            get { return _auss_id; }
            set { _auss_id = value; }
        }

        public static AuditStatus Converter(DataRow row)
        {
            return new AuditStatus
            {
                Auss_act = row["AUSS_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["AUSS_ACT"]),
                Auss_code = row["AUSS_CODE"] == DBNull.Value ? string.Empty : row["AUSS_CODE"].ToString(),
                Auss_cre_by = row["AUSS_CRE_BY"] == DBNull.Value ? string.Empty : row["AUSS_CRE_BY"].ToString(),
                Auss_cre_dt = row["AUSS_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AUSS_CRE_DT"]),
                Auss_desc = row["AUSS_DESC"] == DBNull.Value ? string.Empty : row["AUSS_DESC"].ToString(),
                Auss_direction = row["AUSS_DIRECTION"] == DBNull.Value ? 0 : Convert.ToInt32(row["AUSS_DIRECTION"]),
                Auss_id = row["AUSS_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["AUSS_ID"])

            };
        }

    }
}
