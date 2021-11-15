using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class DenomiSum
    {
        #region Private Members
        private Boolean _gds_act;
        private string _gds_cashier;
        private string _gds_cre_by;
        private DateTime _gds_cre_dt;
        private Int32 _gds_line_no;
        private string _gds_mod_by;
        private DateTime _gds_mod_dt;
        private string _gds_pay_tp;
        private decimal _gds_phy_amt;
        private Int32 _gds_seq_no;
        private decimal _gds_sys_amt;
        #endregion

        #region Public Property Definition
        public Boolean Gds_act
        {
            get { return _gds_act; }
            set { _gds_act = value; }
        }
        public string Gds_cashier
        {
            get { return _gds_cashier; }
            set { _gds_cashier = value; }
        }
        public string Gds_cre_by
        {
            get { return _gds_cre_by; }
            set { _gds_cre_by = value; }
        }
        public DateTime Gds_cre_dt
        {
            get { return _gds_cre_dt; }
            set { _gds_cre_dt = value; }
        }
        public Int32 Gds_line_no
        {
            get { return _gds_line_no; }
            set { _gds_line_no = value; }
        }
        public string Gds_mod_by
        {
            get { return _gds_mod_by; }
            set { _gds_mod_by = value; }
        }
        public DateTime Gds_mod_dt
        {
            get { return _gds_mod_dt; }
            set { _gds_mod_dt = value; }
        }
        public string Gds_pay_tp
        {
            get { return _gds_pay_tp; }
            set { _gds_pay_tp = value; }
        }
        public decimal Gds_phy_amt
        {
            get { return _gds_phy_amt; }
            set { _gds_phy_amt = value; }
        }
        public Int32 Gds_seq_no
        {
            get { return _gds_seq_no; }
            set { _gds_seq_no = value; }
        }
        public decimal Gds_sys_amt
        {
            get { return _gds_sys_amt; }
            set { _gds_sys_amt = value; }
        }

        #endregion

        #region Converters
        public static DenomiSum Converter(DataRow row)
        {
            return new DenomiSum
            {

                Gds_act = row["GDS_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["GDS_ACT"]),
                Gds_cashier = row["GDS_CASHIER"] == DBNull.Value ? string.Empty : row["GDS_CASHIER"].ToString(),
                Gds_cre_by = row["GDS_CRE_BY"] == DBNull.Value ? string.Empty : row["GDS_CRE_BY"].ToString(),
                Gds_cre_dt = row["GDS_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GDS_CRE_DT"]),
                Gds_line_no = row["GDS_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["GDS_LINE_NO"]),
                Gds_mod_by = row["GDS_MOD_BY"] == DBNull.Value ? string.Empty : row["GDS_MOD_BY"].ToString(),
                Gds_mod_dt = row["GDS_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GDS_MOD_DT"]),
                Gds_pay_tp = row["GDS_PAY_TP"] == DBNull.Value ? string.Empty : row["GDS_PAY_TP"].ToString(),
                Gds_phy_amt = row["GDS_PHY_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GDS_PHY_AMT"]),
                Gds_seq_no = row["GDS_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["GDS_SEQ_NO"]),
                Gds_sys_amt = row["GDS_SYS_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GDS_SYS_AMT"])

            };
        }
        #endregion
    }
}

