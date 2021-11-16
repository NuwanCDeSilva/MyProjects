using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Sales
{
    public class BlackListCustomers
    {
        #region Private Members
        private Boolean _hbl_act;
        private decimal _hbl_cls_bal;
        private string _hbl_com;
        private string _hbl_cre_by;
        private DateTime _hbl_cre_dt;
        private string _hbl_cust_cd;
        private decimal _hbl_def_val;
        private DateTime _hbl_dt;
        private string _hbl_pc;
        private string _hbl_rmk;
        private DateTime _hbl_rmv_dt;
        #endregion

        public Boolean Hbl_act
        {
            get { return _hbl_act; }
            set { _hbl_act = value; }
        }
        public decimal Hbl_cls_bal
        {
            get { return _hbl_cls_bal; }
            set { _hbl_cls_bal = value; }
        }
        public string Hbl_com
        {
            get { return _hbl_com; }
            set { _hbl_com = value; }
        }
        public string Hbl_cre_by
        {
            get { return _hbl_cre_by; }
            set { _hbl_cre_by = value; }
        }
        public DateTime Hbl_cre_dt
        {
            get { return _hbl_cre_dt; }
            set { _hbl_cre_dt = value; }
        }
        public string Hbl_cust_cd
        {
            get { return _hbl_cust_cd; }
            set { _hbl_cust_cd = value; }
        }
        public decimal Hbl_def_val
        {
            get { return _hbl_def_val; }
            set { _hbl_def_val = value; }
        }
        public DateTime Hbl_dt
        {
            get { return _hbl_dt; }
            set { _hbl_dt = value; }
        }
        public string Hbl_pc
        {
            get { return _hbl_pc; }
            set { _hbl_pc = value; }
        }
        public string Hbl_rmk
        {
            get { return _hbl_rmk; }
            set { _hbl_rmk = value; }
        }
        public DateTime Hbl_rmv_dt
        {
            get { return _hbl_rmv_dt; }
            set { _hbl_rmv_dt = value; }
        }


        public static BlackListCustomers Converter(DataRow row)
        {
            return new BlackListCustomers
            {
                Hbl_act = row["HBL_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["HBL_ACT"]),
                Hbl_cls_bal = row["HBL_CLS_BAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HBL_CLS_BAL"]),
                Hbl_cre_by = row["HBL_CRE_BY"] == DBNull.Value ? string.Empty : row["HBL_CRE_BY"].ToString(),
                Hbl_cre_dt = row["HBL_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HBL_CRE_DT"]),
                Hbl_cust_cd = row["HBL_CUST_CD"] == DBNull.Value ? string.Empty : row["HBL_CUST_CD"].ToString(),
                Hbl_def_val = row["HBL_DEF_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HBL_DEF_VAL"]),
                Hbl_dt = row["HBL_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HBL_DT"]),
                Hbl_rmk = row["HBL_RMK"] == DBNull.Value ? string.Empty : row["HBL_RMK"].ToString(),
                Hbl_rmv_dt = row["HBL_RMV_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HBL_RMV_DT"]),
                Hbl_com = row["HBL_COM"] == DBNull.Value ? string.Empty : row["HBL_COM"].ToString(),
                Hbl_pc = row["HBL_PC"] == DBNull.Value ? string.Empty : row["HBL_PC"].ToString()
            };
        }
    }
}

