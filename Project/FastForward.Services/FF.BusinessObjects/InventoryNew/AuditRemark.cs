using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class AuditRemark
    {
        #region Private Members
        private Boolean _ausr_act;
        private string _ausr_com;
        private string _ausr_cre_by;
        private DateTime _ausr_cre_dt;
        private Int32 _ausr_id;
        private int _ausr_line;
        private string _ausr_rmk;
        private string _ausr_rpt_type;
        #endregion

        public Boolean Ausr_act
        {
            get { return _ausr_act; }
            set { _ausr_act = value; }
        }
        public string Ausr_com
        {
            get { return _ausr_com; }
            set { _ausr_com = value; }
        }
        public string Ausr_cre_by
        {
            get { return _ausr_cre_by; }
            set { _ausr_cre_by = value; }
        }
        public DateTime Ausr_cre_dt
        {
            get { return _ausr_cre_dt; }
            set { _ausr_cre_dt = value; }
        }
        public Int32 Ausr_id
        {
            get { return _ausr_id; }
            set { _ausr_id = value; }
        }
        public int Ausr_line
        {
            get { return _ausr_line; }
            set { _ausr_line = value; }
        }
        public string Ausr_rmk
        {
            get { return _ausr_rmk; }
            set { _ausr_rmk = value; }
        }
        public string Ausr_rpt_type
        {
            get { return _ausr_rpt_type; }
            set { _ausr_rpt_type = value; }
        }

        public static AuditRemark Converter(DataRow row)
        {
            return new AuditRemark
            {
                Ausr_act = row["AUSR_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["AUSR_ACT"]),
                Ausr_com = row["AUSR_COM"] == DBNull.Value ? string.Empty : row["AUSR_COM"].ToString(),
                Ausr_cre_by = row["AUSR_CRE_BY"] == DBNull.Value ? string.Empty : row["AUSR_CRE_BY"].ToString(),
                Ausr_cre_dt = row["AUSR_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AUSR_CRE_DT"]),
                Ausr_id = row["AUSR_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["AUSR_ID"]),
                Ausr_line = row["AUSR_LINE"] == DBNull.Value ? 0 : Convert.ToInt16(row["AUSR_LINE"]),
                Ausr_rmk = row["AUSR_RMK"] == DBNull.Value ? string.Empty : row["AUSR_RMK"].ToString(),
                Ausr_rpt_type = row["AUSR_RPT_TYPE"] == DBNull.Value ? string.Empty : row["AUSR_RPT_TYPE"].ToString()

            };
        }

    }
}
