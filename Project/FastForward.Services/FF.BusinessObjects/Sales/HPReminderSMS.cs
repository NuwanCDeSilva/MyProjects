using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class HPReminderSMS
    {

        #region Private Members
        private string _hsrm_acc;
        private string _hsrm_com;
        private string _hsrm_contact;
        private string _hsrm_cre_by;
        private DateTime _hsrm_cre_dt;
        private string _hsrm_cust_tp;
        private string _hsrm_pc;
        private DateTime _hsrm_rmd_dt;
        private Int32 _hsrm_seq;
        private string _hsrm_tp;
        private decimal _hsrm_val;
        #endregion

        public string Hsrm_acc
        {
            get { return _hsrm_acc; }
            set { _hsrm_acc = value; }
        }
        public string Hsrm_com
        {
            get { return _hsrm_com; }
            set { _hsrm_com = value; }
        }
        public string Hsrm_contact
        {
            get { return _hsrm_contact; }
            set { _hsrm_contact = value; }
        }
        public string Hsrm_cre_by
        {
            get { return _hsrm_cre_by; }
            set { _hsrm_cre_by = value; }
        }
        public DateTime Hsrm_cre_dt
        {
            get { return _hsrm_cre_dt; }
            set { _hsrm_cre_dt = value; }
        }
        public string Hsrm_cust_tp
        {
            get { return _hsrm_cust_tp; }
            set { _hsrm_cust_tp = value; }
        }
        public string Hsrm_pc
        {
            get { return _hsrm_pc; }
            set { _hsrm_pc = value; }
        }
        public DateTime Hsrm_rmd_dt
        {
            get { return _hsrm_rmd_dt; }
            set { _hsrm_rmd_dt = value; }
        }
        public Int32 Hsrm_seq
        {
            get { return _hsrm_seq; }
            set { _hsrm_seq = value; }
        }
        public string Hsrm_tp
        {
            get { return _hsrm_tp; }
            set { _hsrm_tp = value; }
        }
        public decimal Hsrm_val
        {
            get { return _hsrm_val; }
            set { _hsrm_val = value; }
        }

        public static HPReminderSMS Converter(DataRow row)
        {
            return new HPReminderSMS
            {
                Hsrm_acc = row["HSRM_ACC"] == DBNull.Value ? string.Empty : row["HSRM_ACC"].ToString(),
                Hsrm_com = row["HSRM_COM"] == DBNull.Value ? string.Empty : row["HSRM_COM"].ToString(),
                Hsrm_contact = row["HSRM_CONTACT"] == DBNull.Value ? string.Empty : row["HSRM_CONTACT"].ToString(),
                Hsrm_cre_by = row["HSRM_CRE_BY"] == DBNull.Value ? string.Empty : row["HSRM_CRE_BY"].ToString(),
                Hsrm_cre_dt = row["HSRM_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HSRM_CRE_DT"]),
                Hsrm_cust_tp = row["HSRM_CUST_TP"] == DBNull.Value ? string.Empty : row["HSRM_CUST_TP"].ToString(),
                Hsrm_pc = row["HSRM_PC"] == DBNull.Value ? string.Empty : row["HSRM_PC"].ToString(),
                Hsrm_rmd_dt = row["HSRM_RMD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HSRM_RMD_DT"]),
                Hsrm_seq = row["HSRM_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HSRM_SEQ"]),
                Hsrm_tp = row["HSRM_TP"] == DBNull.Value ? string.Empty : row["HSRM_TP"].ToString(),
                Hsrm_val = row["HSRM_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HSRM_VAL"])

            };
        }
    }
}
     
    
