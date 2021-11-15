using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
     [Serializable]
    public class QoutationDetails
    {
        /// <summary>
        /// Written By Shani on 19/05/2012
        /// table = QUO_DET
        /// </summary>
        /// 
        #region Private Members
        private decimal _qd_amt;
        private Int32 _qd_cbatch_line;
        private string _qd_cdoc_no;
        private Int32 _qd_citm_line;
        private decimal _qd_cost_amt;
        private decimal _qd_dis_amt;
        private decimal _qd_dit_rt;
        private decimal _qd_frm_qty;
        private decimal _qd_issue_qty;
        private string _qd_itm_cd;
        private string _qd_itm_desc;
        private string _qd_itm_stus;
        private decimal _qd_itm_tax;
        private Int32 _qd_line_no;
        private string _qd_nitm_cd;
        private string _qd_nitm_desc;
        private string _qd_no;
        private string _qd_pbook;
        private string _qd_pb_lvl;
        private decimal _qd_pb_price;
        private Int32 _qd_pb_seq;
        private string _qd_quo_tp;
        private decimal _qd_resbal_qty;
        private string _qd_resitm_cd;
        private Int32 _qd_resline_no;
        private string _qd_resreq_no;
        private string _qd_res_no;
        private decimal _qd_res_qty;
        private Int32 _qd_seq_no;
        private decimal _qd_tot_amt;
        private decimal _qd_to_qty;
        private decimal _qd_unit_cost;
        private decimal _qd_unit_price;
        private string _qd_uom;
        private string _qd_warr_rmk;
        private Int32 _qd_warr_pd;
        private decimal _qd_pick_qty;
        private string _mi_longdesc;
        private string _mi_model;
        private string _mi_type;
        private string _mi_statusDes;
        private Int32 _Qd_quo_base; //kapila 11/3/2016
        QuotationHeader _QuotationHeader = null;
        
        #endregion

        public decimal Qd_amt { get { return _qd_amt; } set { _qd_amt = value; } }
        public Int32 Qd_cbatch_line { get { return _qd_cbatch_line; } set { _qd_cbatch_line = value; } }
        public string Qd_cdoc_no { get { return _qd_cdoc_no; } set { _qd_cdoc_no = value; } }
        public Int32 Qd_citm_line { get { return _qd_citm_line; } set { _qd_citm_line = value; } }
        public decimal Qd_cost_amt { get { return _qd_cost_amt; } set { _qd_cost_amt = value; } }
        public decimal Qd_dis_amt { get { return _qd_dis_amt; } set { _qd_dis_amt = value; } }
        public decimal Qd_dit_rt { get { return _qd_dit_rt; } set { _qd_dit_rt = value; } }
        public decimal Qd_frm_qty { get { return _qd_frm_qty; } set { _qd_frm_qty = value; } }
        public decimal Qd_issue_qty { get { return _qd_issue_qty; } set { _qd_issue_qty = value; } }
        public string Qd_itm_cd { get { return _qd_itm_cd; } set { _qd_itm_cd = value; } }
        public string Qd_itm_desc { get { return _qd_itm_desc; } set { _qd_itm_desc = value; } }
        public string Qd_itm_stus { get { return _qd_itm_stus; } set { _qd_itm_stus = value; } }
        public decimal Qd_itm_tax { get { return _qd_itm_tax; } set { _qd_itm_tax = value; } }
        public Int32 Qd_line_no { get { return _qd_line_no; } set { _qd_line_no = value; } }
        public string Qd_nitm_cd { get { return _qd_nitm_cd; } set { _qd_nitm_cd = value; } }
        public string Qd_nitm_desc { get { return _qd_nitm_desc; } set { _qd_nitm_desc = value; } }
        public string Qd_no { get { return _qd_no; } set { _qd_no = value; } }
        public string Qd_pbook { get { return _qd_pbook; } set { _qd_pbook = value; } }
        public string Qd_pb_lvl { get { return _qd_pb_lvl; } set { _qd_pb_lvl = value; } }
        public decimal Qd_pb_price { get { return _qd_pb_price; } set { _qd_pb_price = value; } }
        public Int32 Qd_pb_seq { get { return _qd_pb_seq; } set { _qd_pb_seq = value; } }
        public string Qd_quo_tp { get { return _qd_quo_tp; } set { _qd_quo_tp = value; } }
        public decimal Qd_resbal_qty { get { return _qd_resbal_qty; } set { _qd_resbal_qty = value; } }
        public string Qd_resitm_cd { get { return _qd_resitm_cd; } set { _qd_resitm_cd = value; } }
        public Int32 Qd_resline_no { get { return _qd_resline_no; } set { _qd_resline_no = value; } }
        public string Qd_resreq_no { get { return _qd_resreq_no; } set { _qd_resreq_no = value; } }
        public string Qd_res_no { get { return _qd_res_no; } set { _qd_res_no = value; } }
        public decimal Qd_res_qty { get { return _qd_res_qty; } set { _qd_res_qty = value; } }
        public Int32 Qd_seq_no { get { return _qd_seq_no; } set { _qd_seq_no = value; } }
        public decimal Qd_tot_amt { get { return _qd_tot_amt; } set { _qd_tot_amt = value; } }
        public decimal Qd_to_qty { get { return _qd_to_qty; } set { _qd_to_qty = value; } }
        public decimal Qd_unit_cost { get { return _qd_unit_cost; } set { _qd_unit_cost = value; } }
        public decimal Qd_unit_price { get { return _qd_unit_price; } set { _qd_unit_price = value; } }
        public string Qd_uom { get { return _qd_uom; } set { _qd_uom = value; } }
        public string Qd_warr_rmk { get { return _qd_warr_rmk; } set { _qd_warr_rmk = value; } }
        public Int32 Qd_warr_pd { get { return _qd_warr_pd; } set { _qd_warr_pd = value; } }
        public decimal Qd_pick_qty { get { return _qd_pick_qty; } set { _qd_pick_qty = value; } }
        public string Mi_longdesc { get { return _mi_longdesc; } set { _mi_longdesc = value; } }
        public string Mi_model { get { return _mi_model; } set { _mi_model = value; } }
        public string Mi_type { get { return _mi_type; } set { _mi_type = value; } }
        public Int32 Qd_quo_base { get { return _Qd_quo_base; } set { _Qd_quo_base = value; } }

        public QuotationHeader QuotationHeader
        {
            get { return _QuotationHeader; }
            set { _QuotationHeader = value; }
        }
        public string Mi_statusDes { get { return _mi_statusDes; } set { _mi_statusDes = value; } } //rukshan
        public static QoutationDetails ConvertTotal(DataRow row)
        {
            return new QoutationDetails
            {
                Qd_amt = row["QD_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QD_AMT"]),
                Qd_cbatch_line = row["QD_CBATCH_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["QD_CBATCH_LINE"]),
                Qd_cdoc_no = row["QD_CDOC_NO"] == DBNull.Value ? string.Empty : row["QD_CDOC_NO"].ToString(),
                Qd_citm_line = row["QD_CITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["QD_CITM_LINE"]),
                Qd_cost_amt = row["QD_COST_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QD_COST_AMT"]),
                Qd_dis_amt = row["QD_DIS_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QD_DIS_AMT"]),
                Qd_dit_rt = row["QD_DIT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QD_DIT_RT"]),
                Qd_frm_qty = row["QD_FRM_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QD_FRM_QTY"]),
                Qd_issue_qty = row["QD_ISSUE_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QD_ISSUE_QTY"]),
                Qd_itm_cd = row["QD_ITM_CD"] == DBNull.Value ? string.Empty : row["QD_ITM_CD"].ToString(),
                Qd_itm_desc = row["QD_ITM_DESC"] == DBNull.Value ? string.Empty : row["QD_ITM_DESC"].ToString(),
                Qd_itm_stus = row["QD_ITM_STUS"] == DBNull.Value ? string.Empty : row["QD_ITM_STUS"].ToString(),
                Qd_itm_tax = row["QD_ITM_TAX"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QD_ITM_TAX"]),
                Qd_line_no = row["QD_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["QD_LINE_NO"]),
                Qd_nitm_cd = row["QD_NITM_CD"] == DBNull.Value ? string.Empty : row["QD_NITM_CD"].ToString(),
                Qd_nitm_desc = row["QD_NITM_DESC"] == DBNull.Value ? string.Empty : row["QD_NITM_DESC"].ToString(),
                Qd_no = row["QD_NO"] == DBNull.Value ? string.Empty : row["QD_NO"].ToString(),
                Qd_pbook = row["QD_PBOOK"] == DBNull.Value ? string.Empty : row["QD_PBOOK"].ToString(),
                Qd_pb_lvl = row["QD_PB_LVL"] == DBNull.Value ? string.Empty : row["QD_PB_LVL"].ToString(),
                Qd_pb_price = row["QD_PB_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QD_PB_PRICE"]),
                Qd_pb_seq = row["QD_PB_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["QD_PB_SEQ"]),
                Qd_quo_tp = row["QD_QUO_TP"] == DBNull.Value ? string.Empty : row["QD_QUO_TP"].ToString(),
                Qd_resbal_qty = row["QD_RESBAL_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QD_RESBAL_QTY"]),
                Qd_resitm_cd = row["QD_RESITM_CD"] == DBNull.Value ? string.Empty : row["QD_RESITM_CD"].ToString(),
                Qd_resline_no = row["QD_RESLINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["QD_RESLINE_NO"]),
                Qd_resreq_no = row["QD_RESREQ_NO"] == DBNull.Value ? string.Empty : row["QD_RESREQ_NO"].ToString(),
                Qd_res_no = row["QD_RES_NO"] == DBNull.Value ? string.Empty : row["QD_RES_NO"].ToString(),
                Qd_res_qty = row["QD_RES_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QD_RES_QTY"]),
                Qd_seq_no = row["QD_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["QD_SEQ_NO"]),
                Qd_tot_amt = row["QD_TOT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QD_TOT_AMT"]),
                Qd_to_qty = row["QD_TO_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QD_TO_QTY"]),
                Qd_unit_cost = row["QD_UNIT_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QD_UNIT_COST"]),
                Qd_unit_price = row["QD_UNIT_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QD_UNIT_PRICE"]),
                Qd_uom = row["QD_UOM"] == DBNull.Value ? string.Empty : row["QD_UOM"].ToString(),
                Qd_warr_rmk = row["QD_WARR_RMK"] == DBNull.Value ? string.Empty : row["QD_WARR_RMK"].ToString(),
                Qd_warr_pd = row["QD_WARR_PD"] == DBNull.Value ? 0 : Convert.ToInt32(row["QD_WARR_PD"]) 
            };
        }

        public static QoutationDetails convertforquotation(DataRow row)
        {
            return new QoutationDetails
            {
                Qd_frm_qty = row["QD_FRM_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QD_FRM_QTY"]),
                Qd_to_qty = row["QD_TO_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QD_TO_QTY"]),
                Qd_unit_price = row["QD_UNIT_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QD_UNIT_PRICE"]),
            };
        }

        public static QoutationDetails ConvertTotalDO(DataRow row)
        {
            return new QoutationDetails
            {
                Qd_amt = row["QD_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QD_AMT"]),
                Qd_cbatch_line = row["QD_CBATCH_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["QD_CBATCH_LINE"]),
                Qd_cdoc_no = row["QD_CDOC_NO"] == DBNull.Value ? string.Empty : row["QD_CDOC_NO"].ToString(),
                Qd_citm_line = row["QD_CITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["QD_CITM_LINE"]),
                Qd_cost_amt = row["QD_COST_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QD_COST_AMT"]),
                Qd_dis_amt = row["QD_DIS_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QD_DIS_AMT"]),
                Qd_dit_rt = row["QD_DIT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QD_DIT_RT"]),
                Qd_frm_qty = row["QD_FRM_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QD_FRM_QTY"]),
                Qd_issue_qty = row["QD_ISSUE_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QD_ISSUE_QTY"]),
                Qd_itm_cd = row["QD_ITM_CD"] == DBNull.Value ? string.Empty : row["QD_ITM_CD"].ToString(),
                Qd_itm_desc = row["QD_ITM_DESC"] == DBNull.Value ? string.Empty : row["QD_ITM_DESC"].ToString(),
                Qd_itm_stus = row["QD_ITM_STUS"] == DBNull.Value ? string.Empty : row["QD_ITM_STUS"].ToString(),
                Qd_itm_tax = row["QD_ITM_TAX"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QD_ITM_TAX"]),
                Qd_line_no = row["QD_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["QD_LINE_NO"]),
                Qd_nitm_cd = row["QD_NITM_CD"] == DBNull.Value ? string.Empty : row["QD_NITM_CD"].ToString(),
                Qd_nitm_desc = row["QD_NITM_DESC"] == DBNull.Value ? string.Empty : row["QD_NITM_DESC"].ToString(),
                Qd_no = row["QD_NO"] == DBNull.Value ? string.Empty : row["QD_NO"].ToString(),
                Qd_pbook = row["QD_PBOOK"] == DBNull.Value ? string.Empty : row["QD_PBOOK"].ToString(),
                Qd_pb_lvl = row["QD_PB_LVL"] == DBNull.Value ? string.Empty : row["QD_PB_LVL"].ToString(),
                Qd_pb_price = row["QD_PB_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QD_PB_PRICE"]),
                Qd_pb_seq = row["QD_PB_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["QD_PB_SEQ"]),
                Qd_quo_tp = row["QD_QUO_TP"] == DBNull.Value ? string.Empty : row["QD_QUO_TP"].ToString(),
                Qd_resbal_qty = row["QD_RESBAL_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QD_RESBAL_QTY"]),
                Qd_resitm_cd = row["QD_RESITM_CD"] == DBNull.Value ? string.Empty : row["QD_RESITM_CD"].ToString(),
                Qd_resline_no = row["QD_RESLINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["QD_RESLINE_NO"]),
                Qd_resreq_no = row["QD_RESREQ_NO"] == DBNull.Value ? string.Empty : row["QD_RESREQ_NO"].ToString(),
                Qd_res_no = row["QD_RES_NO"] == DBNull.Value ? string.Empty : row["QD_RES_NO"].ToString(),
                Qd_res_qty = row["QD_RES_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QD_RES_QTY"]),
                Qd_seq_no = row["QD_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["QD_SEQ_NO"]),
                Qd_tot_amt = row["QD_TOT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QD_TOT_AMT"]),
                Qd_to_qty = row["QD_TO_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QD_TO_QTY"]),
                Qd_unit_cost = row["QD_UNIT_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QD_UNIT_COST"]),
                Qd_unit_price = row["QD_UNIT_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QD_UNIT_PRICE"]),
                Qd_uom = row["QD_UOM"] == DBNull.Value ? string.Empty : row["QD_UOM"].ToString(),
                Qd_warr_rmk = row["QD_WARR_RMK"] == DBNull.Value ? string.Empty : row["QD_WARR_RMK"].ToString(),
                Qd_warr_pd = row["QD_WARR_PD"] == DBNull.Value ? 0 : Convert.ToInt32(row["QD_WARR_PD"]),
                Qd_pick_qty = row["QD_PICK_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QD_PICK_QTY"]),
                Mi_longdesc = row["MI_LONGDESC"] == DBNull.Value ? string.Empty : row["MI_LONGDESC"].ToString(),
                Mi_model = row["MI_MODEL"] == DBNull.Value ? string.Empty : row["MI_MODEL"].ToString()
            };
        }

    }

}
