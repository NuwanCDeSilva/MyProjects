using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class PurchaseOrderDetail : MasterItem
    {
        #region Private Members
        private decimal _pod_act_unit_price;
        private decimal _pod_dis_amt;
        private decimal _pod_dis_rt;
        private decimal _pod_grn_bal;
        private string _pod_item_desc;
        private string _pod_itm_cd;
        private string _pod_itm_stus;
        private string _pod_itm_tp;
        private string _pod_kit_itm_cd;
        private Int32 _pod_kit_line_no;
        private decimal _pod_lc_bal;
        private decimal _pod_line_amt;
        private Int32 _pod_line_no;
        private decimal _pod_line_tax;
        private decimal _pod_line_val;
        private decimal _pod_nbt;
        private decimal _pod_nbt_before;
        private decimal _pod_pi_bal;
        private decimal _pod_qty;
        private string _pod_ref_no;
        private Int32 _pod_seq_no;
        private decimal _pod_si_bal;
        private decimal _pod_tot_tax_before;
        private decimal _pod_unit_price;
        private string _pod_uom;
        private decimal _pod_vat;
        private decimal _pod_vat_before;


        #endregion

        public decimal Pod_act_unit_price { get { return _pod_act_unit_price; } set { _pod_act_unit_price = value; } }
        public decimal Pod_dis_amt { get { return _pod_dis_amt; } set { _pod_dis_amt = value; } }
        public decimal Pod_dis_rt { get { return _pod_dis_rt; } set { _pod_dis_rt = value; } }
        public decimal Pod_grn_bal { get { return _pod_grn_bal; } set { _pod_grn_bal = value; } }
        public string Pod_item_desc { get { return _pod_item_desc; } set { _pod_item_desc = value; } }
        public string Pod_itm_cd { get { return _pod_itm_cd; } set { _pod_itm_cd = value; } }
        public string Pod_itm_stus { get { return _pod_itm_stus; } set { _pod_itm_stus = value; } }
        public string Pod_itm_tp { get { return _pod_itm_tp; } set { _pod_itm_tp = value; } }
        public string Pod_kit_itm_cd { get { return _pod_kit_itm_cd; } set { _pod_kit_itm_cd = value; } }
        public Int32 Pod_kit_line_no { get { return _pod_kit_line_no; } set { _pod_kit_line_no = value; } }
        public decimal Pod_lc_bal { get { return _pod_lc_bal; } set { _pod_lc_bal = value; } }
        public decimal Pod_line_amt { get { return _pod_line_amt; } set { _pod_line_amt = value; } }
        public Int32 Pod_line_no { get { return _pod_line_no; } set { _pod_line_no = value; } }
        public decimal Pod_line_tax { get { return _pod_line_tax; } set { _pod_line_tax = value; } }
        public decimal Pod_line_val { get { return _pod_line_val; } set { _pod_line_val = value; } }
        public decimal Pod_nbt { get { return _pod_nbt; } set { _pod_nbt = value; } }
        public decimal Pod_nbt_before { get { return _pod_nbt_before; } set { _pod_nbt_before = value; } }
        public decimal Pod_pi_bal { get { return _pod_pi_bal; } set { _pod_pi_bal = value; } }
        public decimal Pod_qty { get { return _pod_qty; } set { _pod_qty = value; } }
        public string Pod_ref_no { get { return _pod_ref_no; } set { _pod_ref_no = value; } }
        public Int32 Pod_seq_no { get { return _pod_seq_no; } set { _pod_seq_no = value; } }
        public decimal Pod_si_bal { get { return _pod_si_bal; } set { _pod_si_bal = value; } }
        public decimal Pod_tot_tax_before { get { return _pod_tot_tax_before; } set { _pod_tot_tax_before = value; } }
        public decimal Pod_unit_price { get { return _pod_unit_price; } set { _pod_unit_price = value; } }
        public string Pod_uom { get { return _pod_uom; } set { _pod_uom = value; } }
        public decimal Pod_vat { get { return _pod_vat; } set { _pod_vat = value; } }
        public decimal Pod_vat_before { get { return _pod_vat_before; } set { _pod_vat_before = value; } }


        public static PurchaseOrderDetail ConvertTotal(DataRow row)
        {
            return new PurchaseOrderDetail
            {
                Pod_act_unit_price = row["POD_ACT_UNIT_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["POD_ACT_UNIT_PRICE"]),
                Pod_dis_amt = row["POD_DIS_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["POD_DIS_AMT"]),
                Pod_dis_rt = row["POD_DIS_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["POD_DIS_RT"]),
                Pod_grn_bal = row["POD_GRN_BAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["POD_GRN_BAL"]),
                Pod_item_desc = row["POD_ITEM_DESC"] == DBNull.Value ? string.Empty : row["POD_ITEM_DESC"].ToString(),
                Pod_itm_cd = row["POD_ITM_CD"] == DBNull.Value ? string.Empty : row["POD_ITM_CD"].ToString(),
                Pod_itm_stus = row["POD_ITM_STUS"] == DBNull.Value ? string.Empty : row["POD_ITM_STUS"].ToString(),
                Pod_itm_tp = row["POD_ITM_TP"] == DBNull.Value ? string.Empty : row["POD_ITM_TP"].ToString(),
                Pod_kit_itm_cd = row["POD_KIT_ITM_CD"] == DBNull.Value ? string.Empty : row["POD_KIT_ITM_CD"].ToString(),
                Pod_kit_line_no = row["POD_KIT_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["POD_KIT_LINE_NO"]),
                Pod_lc_bal = row["POD_LC_BAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["POD_LC_BAL"]),
                Pod_line_amt = row["POD_LINE_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["POD_LINE_AMT"]),
                Pod_line_no = row["POD_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["POD_LINE_NO"]),
                Pod_line_tax = row["POD_LINE_TAX"] == DBNull.Value ? 0 : Convert.ToDecimal(row["POD_LINE_TAX"]),
                Pod_line_val = row["POD_LINE_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["POD_LINE_VAL"]),
                Pod_nbt = row["POD_NBT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["POD_NBT"]),
                Pod_nbt_before = row["POD_NBT_BEFORE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["POD_NBT_BEFORE"]),
                Pod_pi_bal = row["POD_PI_BAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["POD_PI_BAL"]),
                Pod_qty = row["POD_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["POD_QTY"]),
                Pod_ref_no = row["POD_REF_NO"] == DBNull.Value ? string.Empty : row["POD_REF_NO"].ToString(),
                Pod_seq_no = row["POD_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["POD_SEQ_NO"]),
                Pod_si_bal = row["POD_SI_BAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["POD_SI_BAL"]),
                Pod_tot_tax_before = row["POD_TOT_TAX_BEFORE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["POD_TOT_TAX_BEFORE"]),
                Pod_unit_price = row["POD_UNIT_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["POD_UNIT_PRICE"]),
                Pod_uom = row["POD_UOM"] == DBNull.Value ? string.Empty : row["POD_UOM"].ToString(),
                Pod_vat = row["POD_VAT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["POD_VAT"]),
                Pod_vat_before = row["POD_VAT_BEFORE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["POD_VAT_BEFORE"])

            };
        }

        public static PurchaseOrderDetail ConvertDetails(DataRow row)
        {
            return new PurchaseOrderDetail
            {

                Pod_grn_bal = row["POD_GRN_BAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["POD_GRN_BAL"]),
                Pod_item_desc = row["POD_ITEM_DESC"] == DBNull.Value ? string.Empty : row["POD_ITEM_DESC"].ToString(),
                Pod_itm_cd = row["POD_ITM_CD"] == DBNull.Value ? string.Empty : row["POD_ITM_CD"].ToString(),
                Pod_itm_stus = row["POD_ITM_STUS"] == DBNull.Value ? string.Empty : row["POD_ITM_STUS"].ToString(),
                Pod_uom = row["POD_UOM"] == DBNull.Value ? string.Empty : row["POD_UOM"].ToString(),
                Mi_model = row["MI_MODEL"] == DBNull.Value ? string.Empty : row["MI_MODEL"].ToString()

            };
        }
    }
}
