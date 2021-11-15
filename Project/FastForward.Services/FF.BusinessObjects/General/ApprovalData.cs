using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    public class ApprovalData
    {
        public string ReqCategory { get; set; } 
        public string Warrenty { get; set; }
        public string AccScheduleDt { get; set; }
        public string priceConsDate { get; set; }
        public bool Remark { get; set; }
        public string RemarkText { get; set; }
        public List<ItemDetails> InItemDet { get; set; }
        public string LoginUser { get; set; }
        public string AppCd { get; set; }
        public string ReqNo { get; set; }
        public string status { get; set; }
        public Int32 AllowPromotion { get; set; }
        public Int32 ReceiveBuyBack { get; set; }
        public Int32 AllowFreeItemIssue { get; set; }
        public Int32 ItmQty { get; set; }
        public List<ReferenceDetails> RefferenceData { get; set; }
        public ApprovalPermission UserPermission { get; set; }
        public List<ApprovalNewItem> NewItems { get; set; }
    }

    public class ApprovalNewItem
    {
        public string ItemCode { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string StatusDesc { get; set; }
        public Int32 Quantity { get; set; }
        public decimal TotUnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotPrice { get; set; }
        public string PriceBook { get; set; }
        public string PriceBookLvl { get; set; }
    }
    public class ItemDetails
    {
        public string Status { get; set; }
        public string Possition { get; set; }
        public string Direction { get; set; }
        public string ItemCode { get; set; }
        public decimal Discount { get; set; }
        public Int32 Active { get; set; }
        public Int32 ReciveFreeItm { get; set; }
    }
}
