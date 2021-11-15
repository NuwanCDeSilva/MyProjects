using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class GroupSaleHeader
    {
        #region Private Members
        private string _hgr_app_by;
        private DateTime _hgr_app_dt;
        private Int32 _hgr_app_stus;
        private string _hgr_grup_com;
        private string _hgr_com;
        private string _hgr_cont_cust;
        private string _hgr_cont_no;
        private string _hgr_cre_by;
        private DateTime _hgr_cre_dt;
        private string _hgr_desc;
        private DateTime _hgr_from_dt;
        private string _hgr_grup_cd;
        private Int32 _hgr_no_acc;
        private Int32 _hgr_no_cust;
        private Int32 _hgr_no_itm;
        private string _hgr_pc;
        private decimal _hgr_tot_val;
        private DateTime _hgr_to_dt;
        private string _hgr_tp;
        private DateTime _HGR_VISIT_DT;
        private string _HGR_FOLLOW_UP;

        List<GroupSaleCustomer> _groupSaleCustomerList = null;

        #endregion

        public string Hgr_app_by
        {
            get { return _hgr_app_by; }
            set { _hgr_app_by = value; }
        }
        public DateTime Hgr_app_dt
        {
            get { return _hgr_app_dt; }
            set { _hgr_app_dt = value; }
        }
        public Int32 Hgr_app_stus
        {
            get { return _hgr_app_stus; }
            set { _hgr_app_stus = value; }
        }
        public string Hgr_com
        {
            get { return _hgr_com; }
            set { _hgr_com = value; }
        }
        public string Hgr_Grup_com
        {
            get { return _hgr_grup_com; }
            set { _hgr_grup_com = value; }
        }
        public string Hgr_cont_cust
        {
            get { return _hgr_cont_cust; }
            set { _hgr_cont_cust = value; }
        }
        public string Hgr_cont_no
        {
            get { return _hgr_cont_no; }
            set { _hgr_cont_no = value; }
        }
        public string Hgr_cre_by
        {
            get { return _hgr_cre_by; }
            set { _hgr_cre_by = value; }
        }
        public DateTime Hgr_cre_dt
        {
            get { return _hgr_cre_dt; }
            set { _hgr_cre_dt = value; }
        }
        public string Hgr_desc
        {
            get { return _hgr_desc; }
            set { _hgr_desc = value; }
        }
        public DateTime Hgr_from_dt
        {
            get { return _hgr_from_dt; }
            set { _hgr_from_dt = value; }
        }
        public string Hgr_grup_cd
        {
            get { return _hgr_grup_cd; }
            set { _hgr_grup_cd = value; }
        }
        public Int32 Hgr_no_acc
        {
            get { return _hgr_no_acc; }
            set { _hgr_no_acc = value; }
        }
        public Int32 Hgr_no_cust
        {
            get { return _hgr_no_cust; }
            set { _hgr_no_cust = value; }
        }
        public Int32 Hgr_no_itm
        {
            get { return _hgr_no_itm; }
            set { _hgr_no_itm = value; }
        }
        public string Hgr_pc
        {
            get { return _hgr_pc; }
            set { _hgr_pc = value; }
        }
        public decimal Hgr_tot_val
        {
            get { return _hgr_tot_val; }
            set { _hgr_tot_val = value; }
        }
        public DateTime Hgr_to_dt
        {
            get { return _hgr_to_dt; }
            set { _hgr_to_dt = value; }
        }
        public string Hgr_tp
        {
            get { return _hgr_tp; }
            set { _hgr_tp = value; }
        }
        public DateTime HGR_VISIT_DT
        {
            get { return _HGR_VISIT_DT; }
            set { _HGR_VISIT_DT = value; }
        }
        public string HGR_FOLLOW_UP
        {
            get { return _HGR_FOLLOW_UP; }
            set { _HGR_FOLLOW_UP = value; }
        }

        public List<GroupSaleCustomer> GroupSaleCustomerList
        {
            get { return _groupSaleCustomerList; }
            set { _groupSaleCustomerList = value; }
        }

        public static GroupSaleHeader Converter(DataRow row)
        {
            return new GroupSaleHeader
            {
                Hgr_app_by = row["HGR_APP_BY"] == DBNull.Value ? string.Empty : row["HGR_APP_BY"].ToString(),
                Hgr_app_dt = row["HGR_APP_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HGR_APP_DT"]),
                Hgr_app_stus = row["HGR_APP_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["HGR_APP_STUS"]),
                Hgr_com = row["HGR_COM"] == DBNull.Value ? string.Empty : row["HGR_COM"].ToString(),
                Hgr_Grup_com = row["HGR_GRUP_COM"] == DBNull.Value ? string.Empty : row["HGR_GRUP_COM"].ToString(),
                Hgr_cont_cust = row["HGR_CONT_CUST"] == DBNull.Value ? string.Empty : row["HGR_CONT_CUST"].ToString(),
                Hgr_cont_no = row["HGR_CONT_NO"] == DBNull.Value ? string.Empty : row["HGR_CONT_NO"].ToString(),
                Hgr_cre_by = row["HGR_CRE_BY"] == DBNull.Value ? string.Empty : row["HGR_CRE_BY"].ToString(),
                Hgr_cre_dt = row["HGR_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HGR_CRE_DT"]),
                Hgr_desc = row["HGR_DESC"] == DBNull.Value ? string.Empty : row["HGR_DESC"].ToString(),
                Hgr_from_dt = row["HGR_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HGR_FROM_DT"]),
                Hgr_grup_cd = row["HGR_GRUP_CD"] == DBNull.Value ? string.Empty : row["HGR_GRUP_CD"].ToString(),
                Hgr_no_acc = row["HGR_NO_ACC"] == DBNull.Value ? 0 : Convert.ToInt32(row["HGR_NO_ACC"]),
                Hgr_no_cust = row["HGR_NO_CUST"] == DBNull.Value ? 0 : Convert.ToInt32(row["HGR_NO_CUST"]),
                Hgr_no_itm = row["HGR_NO_ITM"] == DBNull.Value ? 0 : Convert.ToInt32(row["HGR_NO_ITM"]),
                Hgr_pc = row["HGR_PC"] == DBNull.Value ? string.Empty : row["HGR_PC"].ToString(),
                Hgr_tot_val = row["HGR_TOT_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HGR_TOT_VAL"]),
                Hgr_to_dt = row["HGR_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HGR_TO_DT"]),
                Hgr_tp = row["HGR_TP"] == DBNull.Value ? string.Empty : row["HGR_TP"].ToString(),
                HGR_VISIT_DT = row["HGR_VISIT_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HGR_VISIT_DT"]),
                HGR_FOLLOW_UP = row["HGR_FOLLOW_UP"] == DBNull.Value ? string.Empty : row["HGR_FOLLOW_UP"].ToString()

            };
        }
    }
}
