using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Financial
{
    [Serializable]
    public class DebitNotes
    {
        #region
        private string _item;
        private string _description;
        private decimal _qty;
        private decimal _unitprice;
        private decimal _total;
        private decimal _disRate;
        private decimal _disAmt;
        private decimal _tax;
        private decimal _lineAmt;
        private string _comcode;
        private string _profitcenter;
        private string _cuscode;
        private string _salesexcode;

        private string _debitnoteno;
        private DateTime _date;
        private string _invoiceno;
        private string _invoicetype;
        private decimal _invamt;
        private decimal _invamtpaid;
        private decimal _invamtbal;

        private string _sessionCreateBy;
        private DateTime _sessionCreateDate;
        private string _sessionModBy;
        private DateTime _sessionModDate;
        private string _sessionId;

        private Int32 _invoiceseqno;
        private string _salesex;

        private string _receiptno;
        private string _cusname;
        private string _cusadd1;
        private string _cusadd2;
        private int _lineNo;
        private string _sad_itm_stus;
        private string _sad_uom;
        private string _sad_itm_tp;
        private decimal _taxRate;
        #endregion

        public string Item
        {
            get { return _item; }
            set { _item = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public decimal Qty
        {
            get { return _qty; }
            set { _qty = value; }
        }

        public decimal UnitPrice
        {
            get { return _unitprice; }
            set { _unitprice = value; }
        }

        public decimal Total
        {
            get { return _total; }
            set { _total = value; }
        }

        public decimal DisRate
        {
            get { return _disRate; }
            set { _disRate = value; }
        }

        public decimal DisAmt
        {
            get { return _disAmt; }
            set { _disAmt = value; }
        }

        public decimal Tax
        {
            get { return _tax; }
            set { _tax = value; }
        }

        public decimal LineAmt
        {
            get { return _lineAmt; }
            set { _lineAmt = value; }
        }

        public string ComCode
        {
            get { return _comcode; }
            set { _comcode = value; }
        }

        public string ProfitCenter
        {
            get { return _profitcenter; }
            set { _profitcenter = value; }
        }

        public string CusCode
        {
            get { return _cuscode; }
            set { _cuscode = value; }
        }

        public string SalesExCode
        {
            get { return _salesexcode; }
            set { _salesexcode = value; }
        }

        public string DebitNoteNo
        {
            get { return _debitnoteno; }
            set { _debitnoteno = value; }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        public string InvoiceNo
        {
            get { return _invoiceno; }
            set { _invoiceno = value; }
        }
        public string InvoiceType
        {
            get { return _invoicetype; }
            set { _invoicetype = value; }
        }

        public decimal InvAmt
        {
            get { return _invamt; }
            set { _invamt = value; }
        }

        public decimal InvAmtPaid
        {
            get { return _invamtpaid; }
            set { _invamtpaid = value; }
        }

        public decimal InvAmtBal
        {
            get { return _invamtbal; }
            set { _invamtbal = value; }
        }

        public string sessionCreateBy
        {
            get { return _sessionCreateBy; }
            set { _sessionCreateBy = value; }
        }
        public DateTime sessionCreateDate
        {
            get { return _sessionCreateDate; }
            set { _sessionCreateDate = value; }
        }
        public string sessionModBy
        {
            get { return _sessionModBy; }
            set { _sessionModBy = value; }
        }
        public DateTime sessionModDate
        {
            get { return _sessionModDate; }
            set { _sessionModDate = value; }
        }

        public string SessionId
        {
            get { return _sessionId; }
            set { _sessionId = value; }
        }
        public Int32 InvoiceSeqNo
        {
            get { return _invoiceseqno; }
            set { _invoiceseqno = value; }
        }
        public string SalesEx
        {
            get { return _salesex; }
            set { _salesex = value; }
        }
        public string ReceiptNo
        {
            get { return _receiptno; }
            set { _receiptno = value; }
        }
        public string CusName
        {
            get { return _cusname; }
            set { _cusname = value; }
        }
        public string CusAdd1
        {
            get { return _cusadd1; }
            set { _cusadd1 = value; }
        }
        public string CusAdd2
        {
            get { return _cusadd2; }
            set { _cusadd2 = value; }
        }
        public int lineNo
        {
            get { return _lineNo; }
            set { _lineNo = value; }
        }
        public string sad_itm_stus
        {
            get { return _sad_itm_stus; }
            set { _sad_itm_stus = value; }
        }
        public string sad_uom
        {
            get { return _sad_uom; }
            set { _sad_uom = value; }
        }

        public string ItemTp
        {
            get { return _sad_itm_tp; }
            set { _sad_itm_tp = value; }
        }
        public decimal TaxRate
        {
            get { return _taxRate; }
            set { _taxRate = value; }
        }
        public static DebitNotes Converter(DataRow row)
        {
            return new DebitNotes
            {
                InvoiceNo = row["sah_inv_no"] == DBNull.Value ? string.Empty : row["sah_inv_no"].ToString(),
                InvoiceType = row["sah_tp"] == DBNull.Value ? string.Empty : row["sah_tp"].ToString(),
                CusCode = row["sah_cus_cd"] == DBNull.Value ? string.Empty : row["sah_cus_cd"].ToString(),
                Date = row["sah_dt"] == DBNull.Value ? DateTime.Now.Date : Convert.ToDateTime(row["sah_dt"]),
                SalesEx = row["SalesExCode"] == DBNull.Value ? string.Empty : row["SalesExCode"].ToString(),
                InvAmt = row["TotalAmt"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TotalAmt"].ToString()),
                InvAmtPaid = row["PaidAmt"] == DBNull.Value ? 0 : Convert.ToDecimal(row["PaidAmt"].ToString()),
                InvAmtBal = row["ToBePaidAmt"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ToBePaidAmt"].ToString()),
                Item = row["sad_itm_cd"] == DBNull.Value ? string.Empty : row["sad_itm_cd"].ToString(),
                Description = row["Description"] == DBNull.Value ? string.Empty : row["Description"].ToString(),
                Qty = row["sad_qty"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sad_qty"].ToString()),
                UnitPrice = row["sad_unit_rt"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sad_unit_rt"].ToString()),
                Total = row["sad_unit_amt"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sad_unit_amt"].ToString()),
                DisRate = row["sad_disc_rt"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sad_disc_rt"].ToString()),
                DisAmt = row["sad_disc_amt"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sad_disc_amt"].ToString()),
                Tax = row["sad_itm_tax_amt"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sad_itm_tax_amt"].ToString()),
                LineAmt = row["sad_tot_amt"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sad_tot_amt"].ToString())
            };
        }
    }
}
