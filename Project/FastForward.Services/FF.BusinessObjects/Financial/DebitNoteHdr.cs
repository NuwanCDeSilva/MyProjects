using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Financial
{
    [Serializable]
    public class DebitNoteHdr
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

        private string _creditnoteno;
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
        private string _InvSubType;

        private string _managerCode;
        private decimal _sah_tax_inv;
        private decimal _sah_tax_exempted;
        private decimal _sah_is_svat;
        private string _sah_d_cust_name;
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

        public string CreditNoteNo
        {
            get { return _creditnoteno; }
            set { _creditnoteno = value; }
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
        public string InvSubType
        {
            get { return _InvSubType; }
            set { _InvSubType = value; }
        }

        public string ManagerCode
        {
            get { return _managerCode; }
            set { _managerCode = value; }
        }

        public decimal TaxInv
        {
            get { return _sah_tax_inv; }
            set { _sah_tax_inv = value; }
        }

        public decimal TaxExecpted
        {
            get { return _sah_tax_exempted; }
            set { _sah_tax_exempted = value; }
        }

        public decimal IsSvat
        {
            get { return _sah_is_svat; }
            set { _sah_is_svat = value; }
        }

        public string DCustName
        {
            get { return _sah_d_cust_name; }
            set { _sah_d_cust_name = value; }
        }
        public string OtherAccount { get; set; }//Added By Dulaj 2018/May/2018

    }
}
