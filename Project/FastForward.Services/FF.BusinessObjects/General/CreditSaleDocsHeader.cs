using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class CreditSaleDocsHeader
    {
        #region Private Members
        private decimal _gdh_amt;
        private string _gdh_chassis;
        private string _gdh_chq_no;
        private Boolean _gdh_coll_cheq;
        private DateTime _gdh_coll_dt;
        private string _gdh_com;
        private DateTime _gdh_cr_dt;
        private string _gdh_cr_no;
        private Boolean _gdh_cr_rec;
        private string _gdh_cr_rmks;
        private string _gdh_engine;
        private string _gdh_final_rmks;
        private string _gdh_inv;
        private DateTime _gdh_inv_dt;
        private Boolean _gdh_isse_doc;
        private string _gdh_issue_rmks;
        private DateTime _gdh_iss_dt;
        private string _gdh_pay_remarks;
        private DateTime _gdh_pay_send_dt;
        private string _gdh_pc;
        private string _gdh_rec;
        private DateTime _gdh_recipt_dt;
        private Boolean _gdh_rec_doc;
        private DateTime _gdh_rec_dt;
        private string _gdh_rec_rmks;
        private string _gdh_reg_rmks;
        private DateTime _gdh_reg_send_dt;
        private Boolean _gdh_send_pay;
        private Boolean _gdh_send_reg;
        private Int32 _gdh_seq;
        #endregion

        #region Public Property Definition
        public decimal Gdh_amt
        {
            get { return _gdh_amt; }
            set { _gdh_amt = value; }
        }
        public string Gdh_chassis
        {
            get { return _gdh_chassis; }
            set { _gdh_chassis = value; }
        }
        public string Gdh_chq_no
        {
            get { return _gdh_chq_no; }
            set { _gdh_chq_no = value; }
        }
        public Boolean Gdh_coll_cheq
        {
            get { return _gdh_coll_cheq; }
            set { _gdh_coll_cheq = value; }
        }
        public DateTime Gdh_coll_dt
        {
            get { return _gdh_coll_dt; }
            set { _gdh_coll_dt = value; }
        }
        public string Gdh_com
        {
            get { return _gdh_com; }
            set { _gdh_com = value; }
        }
        public DateTime Gdh_cr_dt
        {
            get { return _gdh_cr_dt; }
            set { _gdh_cr_dt = value; }
        }
        public string Gdh_cr_no
        {
            get { return _gdh_cr_no; }
            set { _gdh_cr_no = value; }
        }
        public Boolean Gdh_cr_rec
        {
            get { return _gdh_cr_rec; }
            set { _gdh_cr_rec = value; }
        }
        public string Gdh_cr_rmks
        {
            get { return _gdh_cr_rmks; }
            set { _gdh_cr_rmks = value; }
        }
        public string Gdh_engine
        {
            get { return _gdh_engine; }
            set { _gdh_engine = value; }
        }
        public string Gdh_final_rmks
        {
            get { return _gdh_final_rmks; }
            set { _gdh_final_rmks = value; }
        }
        public string Gdh_inv
        {
            get { return _gdh_inv; }
            set { _gdh_inv = value; }
        }
        public DateTime Gdh_inv_dt
        {
            get { return _gdh_inv_dt; }
            set { _gdh_inv_dt = value; }
        }
        public Boolean Gdh_isse_doc
        {
            get { return _gdh_isse_doc; }
            set { _gdh_isse_doc = value; }
        }
        public string Gdh_issue_rmks
        {
            get { return _gdh_issue_rmks; }
            set { _gdh_issue_rmks = value; }
        }
        public DateTime Gdh_iss_dt
        {
            get { return _gdh_iss_dt; }
            set { _gdh_iss_dt = value; }
        }
        public string Gdh_pay_remarks
        {
            get { return _gdh_pay_remarks; }
            set { _gdh_pay_remarks = value; }
        }
        public DateTime Gdh_pay_send_dt
        {
            get { return _gdh_pay_send_dt; }
            set { _gdh_pay_send_dt = value; }
        }
        public string Gdh_pc
        {
            get { return _gdh_pc; }
            set { _gdh_pc = value; }
        }
        public string Gdh_rec
        {
            get { return _gdh_rec; }
            set { _gdh_rec = value; }
        }
        public DateTime Gdh_recipt_dt
        {
            get { return _gdh_recipt_dt; }
            set { _gdh_recipt_dt = value; }
        }
        public Boolean Gdh_rec_doc
        {
            get { return _gdh_rec_doc; }
            set { _gdh_rec_doc = value; }
        }
        public DateTime Gdh_rec_dt
        {
            get { return _gdh_rec_dt; }
            set { _gdh_rec_dt = value; }
        }
        public string Gdh_rec_rmks
        {
            get { return _gdh_rec_rmks; }
            set { _gdh_rec_rmks = value; }
        }
        public string Gdh_reg_rmks
        {
            get { return _gdh_reg_rmks; }
            set { _gdh_reg_rmks = value; }
        }
        public DateTime Gdh_reg_send_dt
        {
            get { return _gdh_reg_send_dt; }
            set { _gdh_reg_send_dt = value; }
        }
        public Boolean Gdh_send_pay
        {
            get { return _gdh_send_pay; }
            set { _gdh_send_pay = value; }
        }
        public Boolean Gdh_send_reg
        {
            get { return _gdh_send_reg; }
            set { _gdh_send_reg = value; }
        }
        public Int32 Gdh_seq
        {
            get { return _gdh_seq; }
            set { _gdh_seq = value; }
        }
        #endregion

        public static CreditSaleDocsHeader Converter(DataRow row)
        {
            return new CreditSaleDocsHeader
            {

                Gdh_amt = row["GDH_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GDH_AMT"]),
                Gdh_chassis = row["GDH_CHASSIS"] == DBNull.Value ? string.Empty : row["GDH_CHASSIS"].ToString(),
                Gdh_chq_no = row["GDH_CHQ_NO"] == DBNull.Value ? string.Empty : row["GDH_CHQ_NO"].ToString(),
                Gdh_coll_cheq = row["GDH_COLL_CHEQ"] == DBNull.Value ? false : Convert.ToBoolean(row["GDH_COLL_CHEQ"]),
                Gdh_coll_dt = row["GDH_COLL_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GDH_COLL_DT"]),
                Gdh_com = row["GDH_COM"] == DBNull.Value ? string.Empty : row["GDH_COM"].ToString(),
                Gdh_cr_dt = row["GDH_CR_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GDH_CR_DT"]),
                Gdh_cr_no = row["GDH_CR_NO"] == DBNull.Value ? string.Empty : row["GDH_CR_NO"].ToString(),
                Gdh_cr_rec = row["GDH_CR_REC"] == DBNull.Value ? false : Convert.ToBoolean(row["GDH_CR_REC"]),
                Gdh_cr_rmks = row["GDH_CR_RMKS"] == DBNull.Value ? string.Empty : row["GDH_CR_RMKS"].ToString(),
                Gdh_engine = row["GDH_ENGINE"] == DBNull.Value ? string.Empty : row["GDH_ENGINE"].ToString(),
                Gdh_final_rmks = row["GDH_FINAL_RMKS"] == DBNull.Value ? string.Empty : row["GDH_FINAL_RMKS"].ToString(),
                Gdh_inv = row["GDH_INV"] == DBNull.Value ? string.Empty : row["GDH_INV"].ToString(),
                Gdh_inv_dt = row["GDH_INV_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GDH_INV_DT"]),
                Gdh_isse_doc = row["GDH_ISSE_DOC"] == DBNull.Value ? false : Convert.ToBoolean(row["GDH_ISSE_DOC"]),
                Gdh_issue_rmks = row["GDH_ISSUE_RMKS"] == DBNull.Value ? string.Empty : row["GDH_ISSUE_RMKS"].ToString(),
                Gdh_iss_dt = row["GDH_ISS_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GDH_ISS_DT"]),
                Gdh_pay_remarks = row["GDH_PAY_REMARKS"] == DBNull.Value ? string.Empty : row["GDH_PAY_REMARKS"].ToString(),
                Gdh_pay_send_dt = row["GDH_PAY_SEND_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GDH_PAY_SEND_DT"]),
                Gdh_pc = row["GDH_PC"] == DBNull.Value ? string.Empty : row["GDH_PC"].ToString(),
                Gdh_rec = row["GDH_REC"] == DBNull.Value ? string.Empty : row["GDH_REC"].ToString(),
                Gdh_recipt_dt = row["GDH_RECIPT_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GDH_RECIPT_DT"]),
                Gdh_rec_doc = row["GDH_REC_DOC"] == DBNull.Value ? false : Convert.ToBoolean(row["GDH_REC_DOC"]),
                Gdh_rec_dt = row["GDH_REC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GDH_REC_DT"]),
                Gdh_rec_rmks = row["GDH_REC_RMKS"] == DBNull.Value ? string.Empty : row["GDH_REC_RMKS"].ToString(),
                Gdh_reg_rmks = row["GDH_REG_RMKS"] == DBNull.Value ? string.Empty : row["GDH_REG_RMKS"].ToString(),
                Gdh_reg_send_dt = row["GDH_REG_SEND_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GDH_REG_SEND_DT"]),
                Gdh_send_pay = row["GDH_SEND_PAY"] == DBNull.Value ? false : Convert.ToBoolean(row["GDH_SEND_PAY"]),
                Gdh_send_reg = row["GDH_SEND_REG"] == DBNull.Value ? false : Convert.ToBoolean(row["GDH_SEND_REG"]),
                Gdh_seq = row["GDH_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["GDH_SEQ"])
            };
        }
    }
}


