using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class PBonusVouDedc
    {
        #region Private Members
        private decimal _pbpdd_amt;
        private string _pbpdd_vou_ref;
        private Int32 _pbpdd_ded_cd;
        private string _pbpdd_tp;
        private string _pbpdd_desc;
        #endregion

        #region Public Property Definition

        public Int32 Pbpdd_ded_cd
        {
            get { return _pbpdd_ded_cd; }
            set { _pbpdd_ded_cd = value; }
        }

        public decimal Pbpdd_amt
        {
            get { return _pbpdd_amt; }
            set { _pbpdd_amt = value; }
        }
        public string Pbpdd_vou_ref
        {
            get { return _pbpdd_vou_ref; }
            set { _pbpdd_vou_ref = value; }
        }
        public string Pbpdd_tp
        {
            get { return _pbpdd_tp; }
            set { _pbpdd_tp = value; }
        }
        public string Pbpdd_desc
        {
            get { return _pbpdd_desc; }
            set { _pbpdd_desc = value; }
        }
        #endregion

        #region Converters
        public static PBonusVouDedc Converter(DataRow row)
        {
            return new PBonusVouDedc
            {
                Pbpdd_ded_cd = row["Pbpdd_ded_cd"] == DBNull.Value ? 0 : Convert.ToInt32(row["Pbpdd_ded_cd"]),
                Pbpdd_tp = row["Pbpdd_tp"] == DBNull.Value ? string.Empty : row["Pbpdd_tp"].ToString(),
                Pbpdd_amt = row["Pbpdd_amt"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Pbpdd_amt"]),
                Pbpdd_desc = row["Pbpdd_desc"] == DBNull.Value ? string.Empty : row["Pbpdd_desc"].ToString(),
                Pbpdd_vou_ref = row["PBPDD_VOU_REF"] == DBNull.Value ? string.Empty : row["PBPDD_VOU_REF"].ToString()

            };
        }
        #endregion
    }
}

