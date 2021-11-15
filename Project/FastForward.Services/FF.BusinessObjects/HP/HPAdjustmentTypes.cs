using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

//By darshana 02/08/2012

namespace FF.BusinessObjects
{
    [Serializable]
    public class HPAdjustmentTypes
    {
        #region Private Members
        private string _hajt_adj_cd;
        private string _hajt_adj_desc;
        private int _hajt_adj_tp;
        private string _hajt_cre_by;
        private DateTime _hajt_cre_dt;
        private Boolean _hajt_mult_acc;
        #endregion

        public string Hajt_adj_cd
        {
            get { return _hajt_adj_cd; }
            set { _hajt_adj_cd = value; }
        }
        public string Hajt_adj_desc
        {
            get { return _hajt_adj_desc; }
            set { _hajt_adj_desc = value; }
        }
        public int Hajt_adj_tp
        {
            get { return _hajt_adj_tp; }
            set { _hajt_adj_tp = value; }
        }
        public string Hajt_cre_by
        {
            get { return _hajt_cre_by; }
            set { _hajt_cre_by = value; }
        }
        public DateTime Hajt_cre_dt
        {
            get { return _hajt_cre_dt; }
            set { _hajt_cre_dt = value; }
        }
        public Boolean Hajt_mult_acc
        {
            get { return _hajt_mult_acc; }
            set { _hajt_mult_acc = value; }
        }

        public static HPAdjustmentTypes Converter(DataRow row)
        {
            return new HPAdjustmentTypes
            {
                Hajt_adj_cd = row["HAJT_ADJ_CD"] == DBNull.Value ? string.Empty : row["HAJT_ADJ_CD"].ToString(),
                Hajt_adj_desc = row["HAJT_ADJ_DESC"] == DBNull.Value ? string.Empty : row["HAJT_ADJ_DESC"].ToString(),
                Hajt_adj_tp = row["HAJT_ADJ_TP"] == DBNull.Value ? 0 : Convert.ToInt16(row["HAJT_ADJ_TP"]),
                Hajt_cre_by = row["HAJT_CRE_BY"] == DBNull.Value ? string.Empty : row["HAJT_CRE_BY"].ToString(),
                Hajt_cre_dt = row["HAJT_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAJT_CRE_DT"]),
                Hajt_mult_acc = row["HAJT_MULT_ACC"] == DBNull.Value ? false : Convert.ToBoolean(row["HAJT_MULT_ACC"])

            };
        }

    }
}
