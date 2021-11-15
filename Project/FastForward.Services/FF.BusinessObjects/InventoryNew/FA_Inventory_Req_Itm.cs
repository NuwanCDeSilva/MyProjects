using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class FA_Inventory_Req_Itm
    {
        #region Private Members
        private decimal _dispd_aod_qty;
        private string _dispd_approved_item;
        private decimal _dispd_approved_qty;
        private decimal _dispd_bal_qty;
        private decimal _dispd_buffer_limit;
        private decimal _dispd_can_qty;
        private Boolean _dispd_chk_status;
        private DateTime _dispd_date;
        private decimal _dispd_forward_sales;
        private string _dispd_item_brand;
        private string _dispd_item_code;
        private string _dispd_item_desc;
        private string _dispd_item_model;
        private string _dispd_item_status;
        private Int32 _dispd_line_no;
        private string _dispd_remarks;
        private string _dispd_reqt_seq;
        private decimal _dispd_requsted_qty;
        private decimal _dispd_res_balqty;
        private string _dispd_res_item_code;
        private Int32 _dispd_res_line_no;
        private string _dispd_res_no;
        private decimal _dispd_res_qty;
        private string _dispd_res_req_no;  
        private decimal _dispd_shop_qty;
        private decimal _dispd_sr_request_qty;
        private string _dispd_uom;
        private Int32 _dispf_line;  
        private string _disp_alternate_item; 
        private Int32 _disp_any_status;
        private Int32 _disp_base_kline;
        private Int32 _disp_base_line;
        private Boolean _disp_cost_applicable;
        private string _disp_kit_item_code;
        private Int32 _disp_line;
        private decimal _disp_mrn_bal_qty;
        private decimal _disp_pro_in_qty;
        private decimal _disp_pro_out_qty;
        private decimal _disp_rtn_qty;
        private Int32 _disp_so_res;

        private MasterItem _masterItem = null;
        #endregion

        public MasterItem MasterItem
        {
            get { return _masterItem; }
            set { _masterItem = value; }
        }

        public decimal Dispd_aod_qty
        {
            get { return _dispd_aod_qty; }
            set { _dispd_aod_qty = value; }
        }

        public string Dispd_approved_item
        {
            get { return _dispd_approved_item; }
            set { _dispd_approved_item = value; }
        }

        public decimal Dispd_approved_qty
        {
            get { return _dispd_approved_qty; }
            set { _dispd_approved_qty = value; }
        }

        public decimal Dispd_bal_qty
        {
            get { return _dispd_bal_qty; }
            set { _dispd_bal_qty = value; }
        }

        public decimal Dispd_buffer_limit
        {
            get { return _dispd_buffer_limit; }
            set { _dispd_buffer_limit = value; }
        }

        public decimal Dispd_can_qty
        {
            get { return _dispd_can_qty; }
            set { _dispd_can_qty = value; }
        }
  
        public Boolean Dispd_chk_status
        {
            get { return _dispd_chk_status; }
            set { _dispd_chk_status = value; }
        }

        public DateTime Dispd_date
        {
            get { return _dispd_date; }
            set { _dispd_date = value; }
        }

        public decimal Dispd_forward_sales
        {
            get { return _dispd_forward_sales; }
            set { _dispd_forward_sales = value; }
        }

        public string Dispd_item_brand
        {
            get { return _dispd_item_brand; }
            set { _dispd_item_brand = value; }
        }
 
        public string Dispd_item_code
        {
            get { return _dispd_item_code; }
            set { _dispd_item_code = value; }
        }

        public string Dispd_item_desc
        {
            get { return _dispd_item_desc; }
            set { _dispd_item_desc = value; }
        }

        public string Dispd_item_model
        {
            get { return _dispd_item_model; }
            set { _dispd_item_model = value; }
        }

        public string Dispd_item_status
        {
            get { return _dispd_item_status; }
            set { _dispd_item_status = value; }
        }

        public Int32 Dispd_line_no
        {
            get { return _dispd_line_no; }
            set { _dispd_line_no = value; }
        }

        public string Dispd_remarks
        {
            get { return _dispd_remarks; }
            set { _dispd_remarks = value; }
        }

        public string Dispd_reqt_seq
        {
            get { return _dispd_reqt_seq; }
            set { _dispd_reqt_seq = value; }
        }

        public decimal Dispd_requsted_qty
        {
            get { return _dispd_requsted_qty; }
            set { _dispd_requsted_qty = value; }
        }

        public decimal Dispd_res_balqty
        {
            get { return _dispd_res_balqty; }
            set { _dispd_res_balqty = value; }
        }

        public string Dispd_res_item_code
        {
            get { return _dispd_res_item_code; }
            set { _dispd_res_item_code = value; }
        }

        public Int32 Dispd_res_line_no
        {
            get { return _dispd_res_line_no; }
            set { _dispd_res_line_no = value; }
        }

        public string Dispd_res_no
        {
            get { return _dispd_res_no; }
            set { _dispd_res_no = value; }
        }

        public decimal Dispd_res_qty
        {
            get { return _dispd_res_qty; }
            set { _dispd_res_qty = value; }
        }

        public string Dispd_res_req_no
        {
            get { return _dispd_res_req_no; }
            set { _dispd_res_req_no = value; }
        }

        public decimal Dispd_shop_qty
        {
            get { return _dispd_shop_qty; }
            set { _dispd_shop_qty = value; }
        }

        public decimal Dispd_sr_request_qty
        {
            get { return _dispd_sr_request_qty; }
            set { _dispd_sr_request_qty = value; }
        }

        public string Dispd_uom
        {
            get { return _dispd_uom; }
            set { _dispd_uom = value; }
        }

        public Int32 Dispf_line
        {
            get { return _dispf_line; }
            set { _dispf_line = value; }
        }

        public string Disp_alternate_item
        {
            get { return _disp_alternate_item; }
            set { _disp_alternate_item = value; }
        }

        public Int32 Disp_any_status
        {
            get { return _disp_any_status; }
            set { _disp_any_status = value; }
        }

        public Int32 Disp_base_kline
        {
            get { return _disp_base_kline; }
            set { _disp_base_kline = value; }
        }

        public Int32 Disp_base_line
        {
            get { return _disp_base_line; }
            set { _disp_base_line = value; }
        }

        public Boolean Disp_cost_applicable
        {
            get { return _disp_cost_applicable; }
            set { _disp_cost_applicable = value; }
        }

        public string Disp_kit_item_code
        {
            get { return _disp_kit_item_code; }
            set { _disp_kit_item_code = value; }
        }
  
        public Int32 Disp_line
        {
            get { return _disp_line; }
            set { _disp_line = value; }
        }

        public decimal Disp_mrn_bal_qty
        {
            get { return _disp_mrn_bal_qty; }
            set { _disp_mrn_bal_qty = value; }
        }

        public decimal Disp_pro_in_qty
        {
            get { return _disp_pro_in_qty; }
            set { _disp_pro_in_qty = value; }
        }

        public decimal Disp_pro_out_qty
        {
            get { return _disp_pro_out_qty; }
            set { _disp_pro_out_qty = value; }
        }

        public decimal Disp_rtn_qty
        {
            get { return _disp_rtn_qty; }
            set { _disp_rtn_qty = value; }
        }

        public Int32 Disp_so_res
        {
            get { return _disp_so_res; }
            set { _disp_so_res = value; }
        }





        public static FA_Inventory_Req_Itm Converter(DataRow row)
        {
            return new FA_Inventory_Req_Itm
            {

                Dispd_aod_qty = row["DISPD_AOD_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DISPD_AOD_QTY"]),
           
                Dispd_approved_item = row["DISPD_APPROVED_ITEM"] == DBNull.Value ? string.Empty : row["DISPD_APPROVED_ITEM"].ToString(),
           
                Dispd_approved_qty = row["DISPD_APPROVED_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DISPD_APPROVED_QTY"]),
           
                Dispd_bal_qty = row["DISPD_BAL_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DISPD_BAL_QTY"]),
           
                Dispd_buffer_limit = row["DISPD_BUFFER_LIMIT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DISPD_BUFFER_LIMIT"]),
           
                Dispd_can_qty = row["DISPD_CAN_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DISPD_CAN_QTY"]),
           
                Dispd_chk_status = row["DISPD_CHK_STATUS"] == DBNull.Value ? false : Convert.ToBoolean(row["DISPD_CHK_STATUS"]),
           
                Dispd_date = row["DISPD_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["DISPD_DATE"]),
           
                Dispd_forward_sales = row["DISPD_FORWARD_SALES"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DISPD_FORWARD_SALES"]),
           
                Dispd_item_brand = row["DISPD_ITEM_BRAND"] == DBNull.Value ? string.Empty : row["DISPD_ITEM_BRAND"].ToString(),
           
                Dispd_item_code = row["DISPD_ITEM_CODE"] == DBNull.Value ? string.Empty : row["DISPD_ITEM_CODE"].ToString(),
           
                Dispd_item_desc = row["DISPD_ITEM_DESC"] == DBNull.Value ? string.Empty : row["DISPD_ITEM_DESC"].ToString(),
           
                Dispd_item_model = row["DISPD_ITEM_MODEL"] == DBNull.Value ? string.Empty : row["DISPD_ITEM_MODEL"].ToString(),
           
                Dispd_item_status = row["DISPD_ITEM_STATUS"] == DBNull.Value ? string.Empty : row["DISPD_ITEM_STATUS"].ToString(),
           
                Dispd_line_no = row["DISPD_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["DISPD_LINE_NO"]),
           
                Dispd_remarks = row["DISPD_REMARKS"] == DBNull.Value ? string.Empty : row["DISPD_REMARKS"].ToString(),
           
                Dispd_reqt_seq = row["DISPD_REQT_SEQ"] == DBNull.Value ? string.Empty : row["DISPD_REQT_SEQ"].ToString(),
           
                Dispd_requsted_qty = row["DISPD_REQUSTED_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DISPD_REQUSTED_QTY"]),
           
                Dispd_res_balqty = row["DISPD_RES_BALQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DISPD_RES_BALQTY"]),
           
                Dispd_res_item_code = row["DISPD_RES_ITEM_CODE"] == DBNull.Value ? string.Empty : row["DISPD_RES_ITEM_CODE"].ToString(),
           
                Dispd_res_line_no = row["DISPD_RES_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["DISPD_RES_LINE_NO"]),
           
                Dispd_res_no = row["DISPD_RES_NO"] == DBNull.Value ? string.Empty : row["DISPD_RES_NO"].ToString(),
           
                Dispd_res_qty = row["DISPD_RES_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DISPD_RES_QTY"]),
           
                Dispd_res_req_no = row["DISPD_RES_REQ_NO"] == DBNull.Value ? string.Empty : row["DISPD_RES_REQ_NO"].ToString(),
           
                Dispd_shop_qty = row["DISPD_SHOP_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DISPD_SHOP_QTY"]),
           
                Dispd_sr_request_qty = row["DISPD_SR_REQUEST_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DISPD_SR_REQUEST_QTY"]),
           
                Dispd_uom = row["DISPD_UOM"] == DBNull.Value ? string.Empty : row["DISPD_UOM"].ToString(),
           
                Dispf_line = row["DISPF_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["DISPF_LINE"]),
           
                Disp_alternate_item = row["DISP_ALTERNATE_ITEM"] == DBNull.Value ? string.Empty : row["DISP_ALTERNATE_ITEM"].ToString(),
           
                Disp_any_status = row["DISP_ANY_STATUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["DISP_ANY_STATUS"]),
           
                Disp_base_kline = row["DISP_BASE_KLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["DISP_BASE_KLINE"]),
           
                Disp_base_line = row["DISP_BASE_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["DISP_BASE_LINE"]),
           
                Disp_cost_applicable = row["DISP_COST_APPLICABLE"] == DBNull.Value ? false : Convert.ToBoolean(row["DISP_COST_APPLICABLE"]),
           
                Disp_kit_item_code = row["DISP_KIT_ITEM_CODE"] == DBNull.Value ? string.Empty : row["DISP_KIT_ITEM_CODE"].ToString(),
           
                Disp_line = row["DISP_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["DISP_LINE"]),
           
                Disp_mrn_bal_qty = row["DISP_MRN_BAL_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DISP_MRN_BAL_QTY"]),
           
                Disp_pro_in_qty = row["DISP_PRO_IN_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DISP_PRO_IN_QTY"]),
           
                Disp_pro_out_qty = row["DISP_PRO_OUT_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DISP_PRO_OUT_QTY"]),
           
                Disp_rtn_qty = row["DISP_RTN_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DISP_RTN_QTY"]),
           
                Disp_so_res = row["DISP_SO_RES"] == DBNull.Value ? 0 : Convert.ToInt32(row["DISP_SO_RES"])
           


            };
        }

    }
}
