using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects
{
   public class CashSales_Out_Rev_Items_Details
   {
       #region private members


       private string _item_code;

       private string _usr;

       private string _Dis_Usrchrge;

       private string _remark;
       private decimal _bookno;

       
       private double _OutItmTax;




       private double _PriceWithOutTax;

      
       private string _Itm_status;
       private double _usr_charge;


       private double _discount;
       private double _refundAmnt;

      
      
       private string _flag;
       private DateTime _effDate;

     





       private string _drp_value;

       private string _effective_date;

       
       private string _GRAD_ANAL2;
       private string _GRAD_ANAL3;


       private int _GRAD_ANAL8;

       private int _GRAD_ANAL7;
       private string _itemCode;
       private string In_ItemCode;


       private string _in_Itm_status;

     
     
      

       private CashSales_Out_RevItems objOutRevItems;

      
     


       #endregion

       #region Public members
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
       public string Drp_value
       {
           get { return _drp_value; }
           set { _drp_value = value; }
       }

       public DateTime EffDate
       {
           get { return _effDate; }
           set { _effDate = value; }
       }
       public string Dis_Usrchrge
       {
           get { return _Dis_Usrchrge; }
           set { _Dis_Usrchrge = value; }
       }
       public double Usr_charge
       {
           get { return _usr_charge; }
           set { _usr_charge = value; }
       }
       public string Usr
       {
           get { return _usr; }
           set { _usr = value; }
       }
       public string Effective_date
       {
           get { return _effective_date; }
           set { _effective_date = value; }
       }
       public double Discount
       {
           get { return _discount; }
           set { _discount = value; }
       }
       public string Flag
       {
           get { return _flag; }
           set { _flag = value; }
       }
       public string Itm_status
       {
           get { return _Itm_status; }
           set { _Itm_status = value; }
       }
       public string Item_code
       {
           get { return _item_code; }
           set { _item_code = value; }
       }
       public string GRAD_ANAL2
       {
           get { return _GRAD_ANAL2; }
           set { _GRAD_ANAL2 = value; }
       }

       public CashSales_Out_RevItems ObjOutRevItems
       {
           get { return objOutRevItems; }
           set { objOutRevItems = value; }
       }
       public decimal Bookno
       {
           get { return _bookno; }
           set { _bookno = value; }
       }
       public string ItemCode
       {
           get { return _itemCode; }
           set { _itemCode = value; }
       }

       public string GRAD_ANAL3
       {
           get { return _GRAD_ANAL3; }
           set { _GRAD_ANAL3 = value; }
       }

       public int GRAD_ANAL8
       {
           get { return _GRAD_ANAL8; }
           set { _GRAD_ANAL8 = value; }
       }

       public int GRAD_ANAL7
       {
           get { return _GRAD_ANAL7; }
           set { _GRAD_ANAL7 = value; }
       }
       public string In_ItemCode1
       {
           get { return In_ItemCode; }
           set { In_ItemCode = value; }
       }
       public string In_Itm_status
       {
           get { return _in_Itm_status; }
           set { _in_Itm_status = value; }
       }

       #endregion




   }
}
