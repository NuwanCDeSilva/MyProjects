using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects
{
    public class CashSales_Out_RevItems
    {
        #region private members

        private string _invoice_No;
        private string _sart_cd;
        private string _flag;
        private string _usr_id;
        private string _usr_Remark;


        private double _refundAmnt;

        
        private string _effective_date;

        
        private string _item_code;

        
        private string _Itm_status;


        private string _remark;

       
        private string _accStatus;

        private double _usr_charge;
        private double _discount;
        private double _OutItmTax;


        private double _PriceWithOutTax;

       

        private string _out_req_param;


        private decimal _bookno;

       
        private string _creditStatus;

      
        private string _costStatus;

        private string _usr;
       
       
        private string _intStatus;
        private string In_ItemCode;


        private string _in_Itm_status;

     
        private List<CashSales_Out_Rev_Items_Details> _listOut_Rev_Items_Details;
        private List<CashSales_Out_Rev_Items_Details> _listReversalItems;

        #endregion


        #region Public members
        public double OutItmTax
        {
            get { return _OutItmTax; }
            set { _OutItmTax = value; }
        }
        public double PriceWithOutTax
        {
            get { return _PriceWithOutTax; }
            set { _PriceWithOutTax = value; }
        }
        public double RefundAmnt
        {
            get { return _refundAmnt; }
            set { _refundAmnt = value; }
        }
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        public string Effective_date
        {
            get { return _effective_date; }
            set { _effective_date = value; }
        }
        public string In_Itm_status
        {
            get { return _in_Itm_status; }
            set { _in_Itm_status = value; }
        }
        public string In_ItemCode1
        {
            get { return In_ItemCode; }
            set { In_ItemCode = value; }
        }
        public string Usr_Remark
        {
            get { return _usr_Remark; }
            set { _usr_Remark = value; }
        }
        public string Usr
        {
            get { return _usr; }
            set { _usr = value; }
        }
        public string Invoice_No
        {
            get { return _invoice_No; }
            set { _invoice_No = value; }
        }
        public string Usr_id
        {
            get { return _usr_id; }
            set { _usr_id = value; }
        }
      

        public double Discount
        {
            get { return _discount; }
            set { _discount = value; }
        }
        public string Item_code
        {
            get { return _item_code; }
            set { _item_code = value; }
        }
        public double Usr_charge
        {
            get { return _usr_charge; }
            set { _usr_charge = value; }
        }
        public string Itm_status
        {
            get { return _Itm_status; }
            set { _Itm_status = value; }
        }
        public string AccStatus
        {
            get { return _accStatus; }
            set { _accStatus = value; }
        }

        public string CreditStatus
        {
            get { return _creditStatus; }
            set { _creditStatus = value; }
        }

        public string Flag
        {
            get { return _flag; }
            set { _flag = value; }
        }
       

        public string CostStatus
        {
            get { return _costStatus; }
            set { _costStatus = value; }
        }
        public string Out_req_param
        {
            get { return _out_req_param; }
            set { _out_req_param = value; }
        }
      
        public string IntStatus
        {
            get { return _intStatus; }
            set { _intStatus = value; }
        }
        public decimal Bookno
        {
            get { return _bookno; }
            set { _bookno = value; }
        }
        public List<CashSales_Out_Rev_Items_Details> ListOut_Rev_Items_Details
        {
            get { return _listOut_Rev_Items_Details; }
            set { _listOut_Rev_Items_Details = value; }
        }

        public List<CashSales_Out_Rev_Items_Details> ListReversalItems
        {
            get { return _listReversalItems; }
            set { _listReversalItems = value; }
        }

        public string Sart_cd
        {
            get { return _sart_cd; }
            set { _sart_cd = value; }
        }

        #endregion


    }
}
