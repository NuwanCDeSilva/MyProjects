using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
     [Serializable]
   public class CashCommissionHeaderRef
    {
        #region Private Members
        private string _scch_cd;
        private string _scch_circular;
        private string _scch_cre_by;
        private DateTime _scch_cre_dt;
        private string _scch_desc;
        private string _scch_sale_tp;
        private Int32 _scch_seq;
        #endregion

        public string Scch_cd { get { return _scch_cd; } set { _scch_cd = value; } }
        public string Scch_circular { get { return _scch_circular; } set { _scch_circular = value; } }
        public string Scch_cre_by { get { return _scch_cre_by; } set { _scch_cre_by = value; } }
        public DateTime Scch_cre_dt { get { return _scch_cre_dt; } set { _scch_cre_dt = value; } }
        public string Scch_desc { get { return _scch_desc; } set { _scch_desc = value; } }
        public string Scch_sale_tp { get { return _scch_sale_tp; } set { _scch_sale_tp = value; } }
        public Int32 Scch_seq { get { return _scch_seq; } set { _scch_seq = value; } }

        public static CashCommissionHeaderRef Converter(DataRow row)
        {
            return new CashCommissionHeaderRef
            {
                Scch_cd = row["SCCH_CD"] == DBNull.Value ? string.Empty : row["SCCH_CD"].ToString(),
                Scch_circular = row["SCCH_CIRCULAR"] == DBNull.Value ? string.Empty : row["SCCH_CIRCULAR"].ToString(),
                Scch_cre_by = row["SCCH_CRE_BY"] == DBNull.Value ? string.Empty : row["SCCH_CRE_BY"].ToString(),
                Scch_cre_dt = row["SCCH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SCCH_CRE_DT"]),
                Scch_desc = row["SCCH_DESC"] == DBNull.Value ? string.Empty : row["SCCH_DESC"].ToString(),
                Scch_sale_tp = row["SCCH_SALE_TP"] == DBNull.Value ? string.Empty : row["SCCH_SALE_TP"].ToString(),
                Scch_seq = row["SCCH_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SCCH_SEQ"])

            };
        }
    }
}

