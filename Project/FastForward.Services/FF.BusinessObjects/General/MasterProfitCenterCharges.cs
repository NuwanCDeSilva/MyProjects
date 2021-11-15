using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    //table : mst_pc_chg
    //created by Shani 05-12-2012

    [Serializable]
    public class MasterProfitCenterCharges
    {
        #region Private Members
        private string _mpch_com;
        private string _mpch_cre_by;
        private DateTime _mpch_cre_dt;
        private decimal _mpch_epf;
        private decimal _mpch_esd;
        private DateTime _mpch_from_dt;
        private string _mpch_pc;
        private Int32 _mpch_seq;
        private DateTime _mpch_to_dt;
        private decimal _mpch_wht;
        #endregion

        public string Mpch_com { get { return _mpch_com; } set { _mpch_com = value; } }
        public string Mpch_cre_by { get { return _mpch_cre_by; } set { _mpch_cre_by = value; } }
        public DateTime Mpch_cre_dt { get { return _mpch_cre_dt; } set { _mpch_cre_dt = value; } }
        public decimal Mpch_epf { get { return _mpch_epf; } set { _mpch_epf = value; } }
        public decimal Mpch_esd { get { return _mpch_esd; } set { _mpch_esd = value; } }
        public DateTime Mpch_from_dt { get { return _mpch_from_dt; } set { _mpch_from_dt = value; } }
        public string Mpch_pc { get { return _mpch_pc; } set { _mpch_pc = value; } }
        public Int32 Mpch_seq { get { return _mpch_seq; } set { _mpch_seq = value; } }
        public DateTime Mpch_to_dt { get { return _mpch_to_dt; } set { _mpch_to_dt = value; } }
        public decimal Mpch_wht { get { return _mpch_wht; } set { _mpch_wht = value; } }

        public static MasterProfitCenterCharges Converter(DataRow row)
        {
            return new MasterProfitCenterCharges
            {
                Mpch_com = row["MPCH_COM"] == DBNull.Value ? string.Empty : row["MPCH_COM"].ToString(),
                Mpch_cre_by = row["MPCH_CRE_BY"] == DBNull.Value ? string.Empty : row["MPCH_CRE_BY"].ToString(),
                Mpch_cre_dt = row["MPCH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPCH_CRE_DT"]),
                Mpch_epf = row["MPCH_EPF"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MPCH_EPF"]),
                Mpch_esd = row["MPCH_ESD"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MPCH_ESD"]),
                Mpch_from_dt = row["MPCH_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPCH_FROM_DT"]),
                Mpch_pc = row["MPCH_PC"] == DBNull.Value ? string.Empty : row["MPCH_PC"].ToString(),
                Mpch_seq = row["MPCH_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPCH_SEQ"]),
                Mpch_to_dt = row["MPCH_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPCH_TO_DT"]),
                Mpch_wht = row["MPCH_WHT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MPCH_WHT"])

            };
        }

    }
}
