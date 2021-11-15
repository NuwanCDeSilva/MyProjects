using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
    public class TmpValidation
    {
        public string Sad_itm_cd { get; set; }
        public Int32 Sad_itm_line { get; set; }
        public decimal Sad_qty { get; set; }
        public decimal Sad_do_qty { get; set; }
        public decimal Sad_srn_qty { get; set; }
        public decimal Ava_bal_bf_save { get; set; }
        public decimal Act_do_qty_bf_save { get; set; }
        public decimal Req_qty { get; set; }
        public decimal Req_bal_qty { get; set; }
        public decimal Pro_kit_out_qty { get; set; }
        public decimal Pro_no_of_kit_qty { get; set; }

        public string Ser_1;
        public string Inl_itm_cd;
        public string Inl_itm_stus;
        public string Inl_com;
        public string Inl_loc;
        public decimal Inl_qty;
        public decimal Inl_free_qty;
        public decimal Inl_res_qty;
        public decimal Inl_qty_bef_save { get; set; }
        public decimal Inl_qty_aft_save { get; set; }
        public decimal Inl_qty_need_to_update { get; set; }
        public decimal Inl_qty_save_diff { get; set; }
        public decimal Inl_err_ava { get; set; }
        public decimal Inl_freeQty { get; set; }
        public decimal Inl_resQty { get; set; }
        public decimal  Pick_qty { get; set; }
        public bool view { get; set; }
        public bool _isErr { get; set; }
        public string errorMsg { get; set; }
        public string ErrTP { get; set; }
        public string itemCode
        {
            get { return Inl_itm_cd; }
            set { Inl_itm_cd = value; }
        }
        public string itemStatus
        {
            get { return Inl_itm_stus; }
            set { Inl_itm_stus = value; }
        }
        public decimal inHand
        {
            get { return Inl_qty; }
            set { Inl_qty = value; }
        }
        public decimal free
        {
            get { return Inl_free_qty; }
            set { Inl_free_qty = value; }
        }
        public decimal reserved
        {
            get { return Inl_res_qty; }
            set { Inl_res_qty = value; }
        }
    }
}
