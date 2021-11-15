using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    [Serializable]
    public class SupplierWiseNBT
    {
        #region private members
        private string _mbit_com;
        private string _mbit_cd;
        private string _mbit_tp;
        private string _mbit_tax_cd;
        private string _mbit_tax_rt_cd;
        private decimal _mbit_tax_rt;
        private bool _mbit_act;
        private DateTime _mbit_effct_dt;
        private decimal _mbit_div_rt;
        private string _mbit_cre_by;
        private DateTime _mbit_cre_dt;
        private string _mbit_mod_by;
        private DateTime _mbit_mod_dt;
        #endregion

        public string MBIT_COM
        {
            get { return _mbit_com; }
            set{_mbit_com = value;}
        }
        public string MBIT_CD
        {
            get { return _mbit_cd; }
            set { _mbit_cd = value; }
        }
        public string MBIT_TP
        {
            get { return _mbit_tp; }
            set { _mbit_tp = value; }
        }
        public string MBIT_TAX_CD
        {
            get { return _mbit_tax_cd; }
            set { _mbit_tax_cd = value; }
        }
        public string MBIT_TAX_RT_CD
        {
            get { return _mbit_tax_rt_cd; }
            set { _mbit_tax_rt_cd = value; }
        }
        public decimal MBIT_TAX_RT
        {
            get { return _mbit_tax_rt; }
            set { _mbit_tax_rt = value; }
        }
        public bool MBIT_ACT
        {
            get { return _mbit_act; }
            set { _mbit_act = value; }
        }
        public DateTime MBIT_EFFCT_DT
        {
            get { return _mbit_effct_dt; }
            set { _mbit_effct_dt = value; }
        }
        public decimal MBIT_DIV_RT
        {
            get { return _mbit_div_rt; }
            set { _mbit_div_rt = value; }
        }
        public string MBIT_CRE_BY
        {
            get { return _mbit_cre_by; }
            set { _mbit_cre_by = value; }
        }
        public DateTime MBIT_CRE_DT
        {
            get { return _mbit_cre_dt; }
            set { _mbit_cre_dt = value; }
        }
        public string MBIT_MOD_BY
        {
            get { return _mbit_mod_by; }
            set { _mbit_mod_by = value; }
        }
        public DateTime MBIT_MOD_DT
        {
            get { return _mbit_mod_dt; }
            set { _mbit_mod_dt = value; }
        }

        public static SupplierWiseNBT Converter(DataRow row)
        {
            return new SupplierWiseNBT
            {
                MBIT_CD = row["MBIT_CD"] == DBNull.Value ? string.Empty : row["MBIT_CD"].ToString(),
                MBIT_TAX_CD = row["MBIT_TAX_CD"] == DBNull.Value ? string.Empty : row["MBIT_TAX_CD"].ToString(),
                MBIT_TAX_RT = row["MBIT_TAX_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MBIT_TAX_RT"]),
                MBIT_DIV_RT = row["MBIT_DIV_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MBIT_DIV_RT"])
            };
        }
    }
}
