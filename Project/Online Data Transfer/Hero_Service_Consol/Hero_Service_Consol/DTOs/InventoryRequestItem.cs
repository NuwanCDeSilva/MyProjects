using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Hero_Service_Consol.DTOs
{
    public class InventoryRequestItem : MasterItem
    {
        #region Private Members
        private decimal _itri_app_qty = 0;
        private string _itri_itm_cd = string.Empty;
        private string _itri_itm_stus = string.Empty;
        private Int32 _itri_line_no = 0;
        private string _itri_note = string.Empty;
        private decimal _itri_qty = 0;
        private string _itri_res_no = string.Empty;
        private Int32 _itri_seq_no = 0;
        private decimal _itri_unit_price = 0;

        private string _itri_mitm_cd = string.Empty; //ITRI_MITM_CD 
        private string _itri_mitm_stus = string.Empty; //ITRI_MITM_STUS 
        private decimal _itri_mqty = 0;                //ITRI_MQTY 
        private decimal _itri_bqty = 0;                //ITRI_BQTY 
        private string _ITRI_ITM_COND = string.Empty;

        private string _itri_job_no = string.Empty;
        private Int32 _itri_job_line = 0;

        private string _itri_supplier;
        private string _itri_grnno;
        private string _itri_batchno;
        private DateTime _itri_grndate;
        private DateTime _itri_expdate;

        private MasterItem _masterItem = null;

        private decimal _itri_po_qty = 0; //Rukshan 30/sep/2015
        private string _itri_com;//Rukshan 17/oct/2015
        private string _itri_loc;//Rukshan 17/oct/2015
        private decimal _itri_res_qty = 0; //Rukshan 17/oct/2015
        private Int32 _itri_res_line = 0; //Rukshan 17/oct/2015
        private decimal _itri_cncl_qty = 0; //Rukshan 17/oct/2015
        private decimal _itri_shop_qty = 0; //Rukshan 17/oct/2015
        private decimal _itri_fd_qty = 0; //Rukshan 17/oct/2015
        private decimal _itri_git_qty = 0; //Rukshan 17/oct/2015
        private decimal _itri_buffer = 0; //Rukshan 17/oct/2015
        private decimal _itri_advan_qty = 0; //Rukshan 17/oct/2015
        private string _mst_item_model;//Rukshan 17/oct/2015
        private string _Approv_status;//Rukshan 20/oct/2015 additional
        private string _PoType;//Rukshan 21/oct/2015 additional
        private string _mi_longdesc;//Rukshan 05/Nov/2015 additional
        private string _to_bond;//Rukshan 05/Nov/2015 additional
        private string _BL;//Rukshan 05/Nov/2015 additional

        private string _itri_base_req_no; //Rukshan 19/Dec/2015
        private Int32 _itri_base_req_line = 0; //Rukshan 19/Dec/2015
        private decimal _itri_Prev_sales_qty = 0; //Rukshan 24/Jan/2015

        private string _showroom; //Rukshan 19/Feb/2016
        private decimal _backqty = 0;

        #endregion

        #region Public Property Definition
        //kapila 3/7/2015
        public string Itri_supplier { get { return _itri_supplier; } set { _itri_supplier = value; } }
        public string Itri_grnno { get { return _itri_grnno; } set { _itri_grnno = value; } }
        public string Itri_batchno { get { return _itri_batchno; } set { _itri_batchno = value; } }
        public DateTime Itri_expdate { get { return _itri_expdate; } set { _itri_expdate = value; } }
        public DateTime Itri_grndate { get { return _itri_grndate; } set { _itri_grndate = value; } }
        public int Tmp_Itri_app_rem_show { get; set; }
        public string Tmp_Itri_app_rem { get; set; }
        public string ITRI_ITM_COND
        {
            get { return _ITRI_ITM_COND; }
            set { _ITRI_ITM_COND = value; }
        }
        public decimal Itri_app_qty
        {
            get { return _itri_app_qty; }
            set { _itri_app_qty = value; }
        }
        public string Itri_itm_cd
        {
            get { return _itri_itm_cd; }
            set { _itri_itm_cd = value; }
        }
        public string Itri_itm_stus
        {
            get { return _itri_itm_stus; }
            set { _itri_itm_stus = value; }
        }
        public Int32 Itri_line_no
        {
            get { return _itri_line_no; }
            set { _itri_line_no = value; }
        }
        public string Itri_note
        {
            get { return _itri_note; }
            set { _itri_note = value; }
        }
        public decimal Itri_qty
        {
            get { return _itri_qty; }
            set { _itri_qty = value; }
        }
        public string Itri_res_no
        {
            get { return _itri_res_no; }
            set { _itri_res_no = value; }
        }
        public Int32 Itri_seq_no
        {
            get { return _itri_seq_no; }
            set { _itri_seq_no = value; }
        }
        public decimal Itri_unit_price
        {
            get { return _itri_unit_price; }
            set { _itri_unit_price = value; }
        }

        public MasterItem MasterItem
        {
            get { return _masterItem; }
            set { _masterItem = value; }
        }

        public string Itri_mitm_cd
        {
            get { return _itri_mitm_cd; }
            set { _itri_mitm_cd = value; }
        }

        public string Itri_mitm_stus
        {
            get { return _itri_mitm_stus; }
            set { _itri_mitm_stus = value; }
        }

        public decimal Itri_mqty
        {
            get { return _itri_mqty; }
            set { _itri_mqty = value; }
        }

        public decimal Itri_bqty
        {
            get { return _itri_bqty; }
            set { _itri_bqty = value; }
        }

        public string Itri_job_no
        {
            get { return _itri_job_no; }
            set { _itri_job_no = value; }
        }

        public Int32 Itri_job_line
        {
            get { return _itri_job_line; }
            set { _itri_job_line = value; }
        }
        public decimal Itri_po_qty
        {
            get { return _itri_po_qty; }
            set { _itri_po_qty = value; }
        }

        public string Mis_desc { get; set; } // Tharaka 2015-10-14
        public string Itri_com
        {
            get { return _itri_com; }
            set { _itri_com = value; }
        }
        public string Itri_loc
        {
            get { return _itri_loc; }
            set { _itri_loc = value; }
        }

        public decimal Itri_issue_qty { get; set; }
        public decimal Itri_res_qty { get; set; }
        public Int32 Itri_res_line { get; set; }
        public string Itr_req_no { get; set; }

        public decimal Itri_cncl_qty
        {
            get { return _itri_cncl_qty; }
            set { _itri_cncl_qty = value; }
        }
        public decimal Itri_shop_qty
        {
            get { return _itri_shop_qty; }
            set { _itri_shop_qty = value; }
        }
        public decimal Itri_fd_qty
        {
            get { return _itri_fd_qty; }
            set { _itri_fd_qty = value; }
        }
        public decimal Itri_git_qty
        {
            get { return _itri_git_qty; }
            set { _itri_git_qty = value; }
        }
        public decimal Itri_buffer
        {
            get { return _itri_buffer; }
            set { _itri_buffer = value; }
        }
        public decimal Itri_advan_qty
        {
            get { return _itri_advan_qty; }
            set { _itri_advan_qty = value; }
        }
        public string Mst_item_model
        {
            get { return _mst_item_model; }
            set { _mst_item_model = value; }
        }
        public string Approv_status
        {
            get { return _Approv_status; }
            set { _Approv_status = value; }
        }
        public string PoType
        {
            get { return _PoType; }
            set { _PoType = value; }
        }
        public string Mi_longdesc
        {
            get { return _mi_longdesc; }
            set { _mi_longdesc = value; }
        }
        public string To_bond
        {
            get { return _to_bond; }
            set { _to_bond = value; }
        }
        public string BL
        {
            get { return _BL; }
            set { _BL = value; }
        }

        public string Itri_itm_stus_desc { get; set; }

        public string Itri_note_desc { get; set; }

        public string Itri_base_req_no
        {
            get { return _itri_base_req_no; }
            set { _itri_base_req_no = value; }
        }

        public Int32 Itri_base_req_line
        {
            get { return _itri_base_req_line; }
            set { _itri_base_req_line = value; }
        }

        public decimal Itri_Prev_sales_qty
        {
            get { return _itri_Prev_sales_qty; }
            set { _itri_Prev_sales_qty = value; }
        }
        public string Showroom
        {
            get { return _showroom; }
            set { _showroom = value; }
        }


        public decimal Backqty
        {
            get { return _backqty; }
            set { _backqty = value; }
        }
        public Int32 RowNo { get; set; }
        public int Temp_is_allocation_err { get; set; }
        public int Temp_itri_is_allocation { get; set; }
        public int Tmp_loc_bal_show { get; set; }
        public decimal Tmp_loc_bal { get; set; }
        public decimal Tmp_dis_loc_bal { get; set; }
        public bool Tmp_itm_err_ava { get; set; }

        #endregion

        public static InventoryRequestItem Converter(DataRow row)
        {
            return new InventoryRequestItem
            {
                Itri_app_qty = row["ITRI_APP_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_APP_QTY"]),
                Itri_itm_cd = row["ITRI_ITM_CD"] == DBNull.Value ? string.Empty : row["ITRI_ITM_CD"].ToString(),
                Itri_itm_stus = row["ITRI_ITM_STUS"] == DBNull.Value ? string.Empty : row["ITRI_ITM_STUS"].ToString(),
                Itri_line_no = row["ITRI_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRI_LINE_NO"]),
                Itri_note = row["ITRI_NOTE"] == DBNull.Value ? string.Empty : row["ITRI_NOTE"].ToString(),
                Itri_qty = row["ITRI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_QTY"]),
                Itri_res_no = row["ITRI_RES_NO"] == DBNull.Value ? string.Empty : row["ITRI_RES_NO"].ToString(),
                Itri_seq_no = row["ITRI_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRI_SEQ_NO"]),
                Itri_unit_price = row["ITRI_UNIT_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_UNIT_PRICE"]),

                Itri_mitm_cd = row["ITRI_MITM_CD"] == DBNull.Value ? string.Empty : row["ITRI_MITM_CD"].ToString(),
                Itri_mitm_stus = row["ITRI_MITM_STUS"] == DBNull.Value ? string.Empty : row["ITRI_MITM_STUS"].ToString(),
                Itri_mqty = row["ITRI_MQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_MQTY"]),
                ITRI_ITM_COND = row["ITRI_ITM_COND"] == DBNull.Value ? string.Empty : row["ITRI_ITM_COND"].ToString(),
                Itri_bqty = row["ITRI_BQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_BQTY"]),
                Itri_po_qty = row["ITRI_PO_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_PO_QTY"]),


            };

        }

        public static InventoryRequestItem ConverterForCommonOut(DataRow row)
        {
            return new InventoryRequestItem
            {

                Itri_line_no = row["ITRI_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRI_LINE_NO"]),
                Itri_itm_cd = row["ITRI_ITM_CD"] == DBNull.Value ? string.Empty : row["ITRI_ITM_CD"].ToString(),
                Itri_itm_stus = row["ITRI_ITM_STUS"] == DBNull.Value ? string.Empty : row["ITRI_ITM_STUS"].ToString(),
                Itri_qty = row["ITRI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_QTY"]),
                Itri_app_qty = row["ITRI_APP_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_APP_QTY"]),

                Mi_longdesc = row["Mi_longdesc"] == DBNull.Value ? string.Empty : Convert.ToString(row["MI_LONGDESC"]),
                Mi_brand = row["MI_BRAND"] == DBNull.Value ? string.Empty : Convert.ToString(row["MI_BRAND"]),
                Mi_model = row["MI_MODEL"] == DBNull.Value ? string.Empty : Convert.ToString(row["MI_MODEL"]),
                Itri_mitm_cd = row["ITRI_MITM_CD"] == DBNull.Value ? string.Empty : Convert.ToString(row["ITRI_MITM_CD"]),
                Itri_job_no = row["ITRI_JOB_NO"] == DBNull.Value ? string.Empty : Convert.ToString(row["ITRI_JOB_NO"]),
                Itri_job_line = row["ITRI_JOB_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRI_JOB_LINE"])

            };

        }

        //Rukshan 20/May/2016
        public static InventoryRequestItem ConverterForCommonOut_2(DataRow row)
        {
            return new InventoryRequestItem
            {

                Itri_line_no = row["ITRI_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRI_LINE_NO"]),
                Itri_itm_cd = row["ITRI_ITM_CD"] == DBNull.Value ? string.Empty : row["ITRI_ITM_CD"].ToString(),
                Itri_itm_stus = row["ITRI_ITM_STUS"] == DBNull.Value ? string.Empty : row["ITRI_ITM_STUS"].ToString(),
                Itri_qty = row["ITRI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_QTY"]),
                Itri_app_qty = row["ITRI_APP_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_APP_QTY"]),
                Itri_bqty = row["itri_bqty"] == DBNull.Value ? 0 : Convert.ToDecimal(row["itri_bqty"]),
                Mi_longdesc = row["Mi_longdesc"] == DBNull.Value ? string.Empty : Convert.ToString(row["MI_LONGDESC"]),
                Mi_brand = row["MI_BRAND"] == DBNull.Value ? string.Empty : Convert.ToString(row["MI_BRAND"]),
                Mi_model = row["MI_MODEL"] == DBNull.Value ? string.Empty : Convert.ToString(row["MI_MODEL"]),
                Itri_mitm_cd = row["ITRI_MITM_CD"] == DBNull.Value ? string.Empty : Convert.ToString(row["ITRI_MITM_CD"]),
                Itri_job_no = row["ITRI_JOB_NO"] == DBNull.Value ? string.Empty : Convert.ToString(row["ITRI_JOB_NO"]),
                Itri_job_line = row["ITRI_JOB_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRI_JOB_LINE"]),
                Itri_res_qty = row["itri_res_qty"] == DBNull.Value ? 0 : Convert.ToDecimal(row["itri_res_qty"])

            };

        }

        //Sahan 15/Sep/2015
        public static InventoryRequestItem ConverterForCommonOutCopy(DataRow row)
        {
            return new InventoryRequestItem
            {
                Itri_line_no = row["ITRI_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRI_LINE_NO"]),
                Itri_itm_cd = row["ITRI_ITM_CD"] == DBNull.Value ? string.Empty : row["ITRI_ITM_CD"].ToString(),
                Itri_itm_stus = row["ITRI_ITM_STUS"] == DBNull.Value ? string.Empty : row["ITRI_ITM_STUS"].ToString(),
                Itri_qty = row["ITRI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_QTY"]),
                Itri_app_qty = row["ITRI_APP_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_APP_QTY"]),
                Mi_longdesc = row["Mi_longdesc"] == DBNull.Value ? string.Empty : Convert.ToString(row["MI_LONGDESC"]),
                Mi_model = row["MI_MODEL"] == DBNull.Value ? string.Empty : Convert.ToString(row["MI_MODEL"]),
            };
        }

        //Tharaka 2015-10-14
        public static InventoryRequestItem ConverterDispatchPlan(DataRow row)
        {
            return new InventoryRequestItem
            {
                Itri_seq_no = row["Itri_seq_no"] == DBNull.Value ? 0 : Convert.ToInt32(row["Itri_seq_no"]),
                Itri_line_no = row["Itri_line_no"] == DBNull.Value ? 0 : Convert.ToInt32(row["Itri_line_no"]),
                Itri_itm_cd = row["Itri_itm_cd"] == DBNull.Value ? string.Empty : Convert.ToString(row["Itri_itm_cd"]),
                Itri_itm_stus = row["Itri_itm_stus"] == DBNull.Value ? string.Empty : Convert.ToString(row["Itri_itm_stus"]),
                Itri_qty = row["Itri_qty"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Itri_qty"]),
                Itri_unit_price = row["Itri_unit_price"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Itri_unit_price"]),
                Itri_app_qty = row["Itri_app_qty"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Itri_app_qty"]),
                Itri_bqty = row["itri_bqty"] == DBNull.Value ? 0 : Convert.ToDecimal(row["itri_bqty"]),
                Itri_res_no = row["Itri_res_no"] == DBNull.Value ? string.Empty : Convert.ToString(row["Itri_res_no"]),
                Mi_shortdesc = row["Mi_shortdesc"] == DBNull.Value ? string.Empty : Convert.ToString(row["Mi_shortdesc"]),
                Mi_model = row["Mi_model"] == DBNull.Value ? string.Empty : Convert.ToString(row["Mi_model"]),
                Mi_itm_uom = row["Mi_itm_uom"] == DBNull.Value ? string.Empty : Convert.ToString(row["Mi_itm_uom"]),
                Mis_desc = row["Mis_desc"] == DBNull.Value ? string.Empty : Convert.ToString(row["Mis_desc"]),
                Itr_req_no = row["ITR_REQ_NO"] == DBNull.Value ? string.Empty : Convert.ToString(row["ITR_REQ_NO"]),
                Itri_job_line = row["Itri_job_line"] == DBNull.Value ? 0 : Convert.ToInt32(row["Itri_job_line"]),
                Itri_job_no = row["Itri_job_no"] == DBNull.Value ? string.Empty : Convert.ToString(row["Itri_job_no"]),
                Itri_loc = row["itri_loc"] == DBNull.Value ? string.Empty : Convert.ToString(row["itri_loc"]),
                Itri_base_req_line = row["itri_base_req_line"] == DBNull.Value ? 0 : Convert.ToInt32(row["itri_base_req_line"]),
                Itri_res_line = row["Itri_res_line"] == DBNull.Value ? 0 : Convert.ToInt32(row["Itri_res_line"]),
                Itri_res_qty = row["Itri_res_qty"] == DBNull.Value ? 0 : Convert.ToInt32(row["Itri_res_qty"]),
            };
        }

        //Rukshan 2015-10-17
        public static InventoryRequestItem MRNConverter(DataRow row)
        {
            return new InventoryRequestItem
            {
                Itri_app_qty = row["ITRI_APP_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_APP_QTY"]),
                Itri_itm_cd = row["ITRI_ITM_CD"] == DBNull.Value ? string.Empty : row["ITRI_ITM_CD"].ToString(),
                Itri_itm_stus = row["ITRI_ITM_STUS"] == DBNull.Value ? string.Empty : row["ITRI_ITM_STUS"].ToString(),
                Itri_line_no = row["ITRI_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRI_LINE_NO"]),
                Itri_note = row["ITRI_NOTE"] == DBNull.Value ? string.Empty : row["ITRI_NOTE"].ToString(),
                Itri_qty = row["ITRI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_QTY"]),
                Itri_res_no = row["ITRI_RES_NO"] == DBNull.Value ? string.Empty : row["ITRI_RES_NO"].ToString(),
                Itri_seq_no = row["ITRI_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRI_SEQ_NO"]),
                Itri_unit_price = row["ITRI_UNIT_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_UNIT_PRICE"]),
                Itri_mitm_cd = row["ITRI_MITM_CD"] == DBNull.Value ? string.Empty : row["ITRI_MITM_CD"].ToString(),
                Itri_mitm_stus = row["ITRI_MITM_STUS"] == DBNull.Value ? string.Empty : row["ITRI_MITM_STUS"].ToString(),
                Itri_mqty = row["ITRI_MQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_MQTY"]),
                ITRI_ITM_COND = row["ITRI_ITM_COND"] == DBNull.Value ? string.Empty : row["ITRI_ITM_COND"].ToString(),
                Itri_bqty = row["ITRI_BQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_BQTY"]),
                Itri_po_qty = row["ITRI_PO_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_PO_QTY"]),
                Itri_job_no = row["itri_job_no"] == DBNull.Value ? string.Empty : row["itri_job_no"].ToString(),
                Itri_job_line = row["itri_job_line"] == DBNull.Value ? 0 : Convert.ToInt32(row["itri_job_line"]),

                Itri_com = row["itri_com"] == DBNull.Value ? string.Empty : row["itri_com"].ToString(),
                Itri_loc = row["itri_loc"] == DBNull.Value ? string.Empty : row["itri_loc"].ToString(),
                Itri_base_req_line = row["itri_base_req_line"] == DBNull.Value ? 0 : Convert.ToInt32(row["itri_base_req_line"]),


                Itri_res_qty = row["ITRI_RES_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_RES_QTY"]),
                Itri_res_line = row["ITRI_RES_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRI_RES_LINE"]),
                Itri_cncl_qty = row["ITRI_CNCL_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_CNCL_QTY"]),
                Itri_shop_qty = row["ITRI_SHOP_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_SHOP_QTY"]),
                Itri_fd_qty = row["ITRI_FD_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_FD_QTY"]),
                Itri_git_qty = row["ITRI_GIT_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_GIT_QTY"]),
                Itri_buffer = row["ITRI_BUFFER"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_BUFFER"]),
                Mst_item_model = row["mi_model"] == DBNull.Value ? string.Empty : row["mi_model"].ToString(),
                Mi_longdesc = row["mi_longdesc"] == DBNull.Value ? string.Empty : row["mi_longdesc"].ToString()

            };

        }
        //Add by Lakshan 30 Sep 2016
        public static InventoryRequestItem ConverterNew(DataRow row)
        {
            return new InventoryRequestItem
            {
                Itri_seq_no = row["ITRI_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRI_SEQ_NO"].ToString()),
                Itri_line_no = row["ITRI_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRI_LINE_NO"].ToString()),
                Itri_itm_cd = row["ITRI_ITM_CD"] == DBNull.Value ? string.Empty : row["ITRI_ITM_CD"].ToString(),
                Itri_itm_stus = row["ITRI_ITM_STUS"] == DBNull.Value ? string.Empty : row["ITRI_ITM_STUS"].ToString(),
                Itri_qty = row["ITRI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_QTY"].ToString()),
                Itri_unit_price = row["ITRI_UNIT_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_UNIT_PRICE"].ToString()),
                Itri_app_qty = row["ITRI_APP_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_APP_QTY"].ToString()),
                Itri_res_no = row["ITRI_RES_NO"] == DBNull.Value ? string.Empty : row["ITRI_RES_NO"].ToString(),
                Itri_note = row["ITRI_NOTE"] == DBNull.Value ? string.Empty : row["ITRI_NOTE"].ToString(),
                Itri_mitm_cd = row["ITRI_MITM_CD"] == DBNull.Value ? string.Empty : row["ITRI_MITM_CD"].ToString(),
                Itri_mitm_stus = row["ITRI_MITM_STUS"] == DBNull.Value ? string.Empty : row["ITRI_MITM_STUS"].ToString(),
                Itri_mqty = row["ITRI_MQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_MQTY"].ToString()),
                Itri_bqty = row["ITRI_BQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_BQTY"].ToString()),
                ITRI_ITM_COND = row["ITRI_ITM_COND"] == DBNull.Value ? string.Empty : row["ITRI_ITM_COND"].ToString(),
                Itri_job_no = row["ITRI_JOB_NO"] == DBNull.Value ? string.Empty : row["ITRI_JOB_NO"].ToString(),
                Itri_job_line = row["ITRI_JOB_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRI_JOB_LINE"].ToString()),
                Itri_com = row["ITRI_COM"] == DBNull.Value ? string.Empty : row["ITRI_COM"].ToString(),
                Itri_loc = row["ITRI_LOC"] == DBNull.Value ? string.Empty : row["ITRI_LOC"].ToString(),
                Itri_po_qty = row["ITRI_PO_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_PO_QTY"].ToString()),
                Itri_issue_qty = row["ITRI_ISSUE_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_ISSUE_QTY"].ToString()),
                Itri_res_qty = row["ITRI_RES_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_RES_QTY"].ToString()),
                Itri_res_line = row["ITRI_RES_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRI_RES_LINE"].ToString()),
                Itri_cncl_qty = row["ITRI_CNCL_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_CNCL_QTY"].ToString()),
                Itri_shop_qty = row["ITRI_SHOP_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_SHOP_QTY"].ToString()),
                Itri_fd_qty = row["ITRI_FD_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_FD_QTY"].ToString()),
                Itri_git_qty = row["ITRI_GIT_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_GIT_QTY"].ToString()),
                Itri_buffer = row["ITRI_BUFFER"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_BUFFER"].ToString()),
                Itri_advan_qty = row["ITRI_ADVAN_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_ADVAN_QTY"].ToString()),
                Itri_base_req_no = row["ITRI_BASE_REQ_NO"] == DBNull.Value ? string.Empty : row["ITRI_BASE_REQ_NO"].ToString(),
                Itri_base_req_line = row["ITRI_BASE_REQ_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRI_BASE_REQ_LINE"].ToString())
            };
        }

        public static InventoryRequestItem CreateNewObject(InventoryRequestItem objOld)
        {
            InventoryRequestItem _new = new InventoryRequestItem();
            _new.Itri_seq_no = objOld.Itri_seq_no;
            _new.Itri_line_no = objOld.Itri_line_no;
            _new.Itri_itm_cd = objOld.Itri_itm_cd;
            _new.Itri_itm_stus = objOld.Itri_itm_stus;
            _new.Itri_qty = objOld.Itri_qty;
            _new.Itri_unit_price = objOld.Itri_unit_price;
            _new.Itri_app_qty = objOld.Itri_app_qty;
            _new.Itri_res_no = objOld.Itri_res_no;
            _new.Itri_note = objOld.Itri_note;
            _new.Itri_mitm_cd = objOld.Itri_mitm_cd;
            _new.Itri_mitm_stus = objOld.Itri_mitm_stus;
            _new.Itri_mqty = objOld.Itri_mqty;
            _new.Itri_bqty = objOld.Itri_bqty;
            _new.ITRI_ITM_COND = objOld.ITRI_ITM_COND;
            _new.Itri_job_no = objOld.Itri_job_no;
            _new.Itri_job_line = objOld.Itri_job_line;
            _new.Itri_com = objOld.Itri_com;
            _new.Itri_loc = objOld.Itri_loc;
            _new.Itri_po_qty = objOld.Itri_po_qty;
            _new.Itri_issue_qty = objOld.Itri_issue_qty;
            _new.Itri_res_qty = objOld.Itri_res_qty;
            _new.Itri_res_line = objOld.Itri_res_line;
            _new.Itri_cncl_qty = objOld.Itri_cncl_qty;
            _new.Itri_shop_qty = objOld.Itri_shop_qty;
            _new.Itri_fd_qty = objOld.Itri_fd_qty;
            _new.Itri_git_qty = objOld.Itri_git_qty;
            _new.Itri_buffer = objOld.Itri_buffer;
            _new.Itri_advan_qty = objOld.Itri_advan_qty;
            _new.Itri_base_req_no = objOld.Itri_base_req_no;
            _new.Itri_base_req_line = objOld.Itri_base_req_line;
            return _new;
        }
    }
}
