using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class HpCustomer
    {
        #region Private Members
        private string _htc_acc_no;
        private string _htc_adr_01;
        private string _htc_adr_02;
        private string _htc_adr_03;
        private Int32 _htc_adr_tp;
        private string _htc_cre_by;
        private DateTime _htc_cre_dt;
        private string _htc_cust_cd;
        private string _htc_cust_tp;
        private Int32 _htc_seq;
        #endregion

        public string Htc_acc_no
        {
            get { return _htc_acc_no; }
            set { _htc_acc_no = value; }
        }
        public string Htc_adr_01
        {
            get { return _htc_adr_01; }
            set { _htc_adr_01 = value; }
        }
        public string Htc_adr_02
        {
            get { return _htc_adr_02; }
            set { _htc_adr_02 = value; }
        }
        public string Htc_adr_03
        {
            get { return _htc_adr_03; }
            set { _htc_adr_03 = value; }
        }
        public Int32 Htc_adr_tp
        {
            get { return _htc_adr_tp; }
            set { _htc_adr_tp = value; }
        }
        public string Htc_cre_by
        {
            get { return _htc_cre_by; }
            set { _htc_cre_by = value; }
        }
        public DateTime Htc_cre_dt
        {
            get { return _htc_cre_dt; }
            set { _htc_cre_dt = value; }
        }
        public string Htc_cust_cd
        {
            get { return _htc_cust_cd; }
            set { _htc_cust_cd = value; }
        }
        public string Htc_cust_tp
        {
            get { return _htc_cust_tp; }
            set { _htc_cust_tp = value; }
        }
        public Int32 Htc_seq
        {
            get { return _htc_seq; }
            set { _htc_seq = value; }
        }

        public static HpCustomer Converter(DataRow row)
        {
            return new HpCustomer
            {
                Htc_acc_no = row["HTC_ACC_NO"] == DBNull.Value ? string.Empty : row["HTC_ACC_NO"].ToString(),
                Htc_adr_01 = row["HTC_ADR_01"] == DBNull.Value ? string.Empty : row["HTC_ADR_01"].ToString(),
                Htc_adr_02 = row["HTC_ADR_02"] == DBNull.Value ? string.Empty : row["HTC_ADR_02"].ToString(),
                Htc_adr_03 = row["HTC_ADR_03"] == DBNull.Value ? string.Empty : row["HTC_ADR_03"].ToString(),
                Htc_adr_tp = row["HTC_ADR_TP"] == DBNull.Value ? 0 : Convert.ToInt32(row["HTC_ADR_TP"]),
                Htc_cre_by = row["HTC_CRE_BY"] == DBNull.Value ? string.Empty : row["HTC_CRE_BY"].ToString(),
                Htc_cre_dt = row["HTC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HTC_CRE_DT"]),
                Htc_cust_cd = row["HTC_CUST_CD"] == DBNull.Value ? string.Empty : row["HTC_CUST_CD"].ToString(),
                Htc_cust_tp = row["HTC_CUST_TP"] == DBNull.Value ? string.Empty : row["HTC_CUST_TP"].ToString(),
                Htc_seq = row["HTC_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HTC_SEQ"])

            };
        }
    }
}
