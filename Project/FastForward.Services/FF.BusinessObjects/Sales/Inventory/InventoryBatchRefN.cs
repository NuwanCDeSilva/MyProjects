using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace FF.BusinessObjects
{
    public class InventoryBatchRefN
    {
        #region Private Members
        private string _inb_base_doc_no;
        private string _inb_base_doc_no1;
        private string _inb_base_doc_no2;
        private string _inb_base_doc_no3;
        private string _inb_base_itmcd;
        private Int32 _inb_base_itmline;
        private Int32 _inb_base_batchline;
        private string _inb_base_itmstus;
        private decimal _inb_base_qty;
        private string _inb_base_ref_no;
        private Int32 _inb_batch_line;
        private string _inb_batch_no;
        private string _inb_bin;
        private string _inb_com;
        private string _inb_cur_cd;
        private DateTime _inb_doc_dt;
        private string _inb_doc_no;
        private decimal _inb_excess_qty;
        private decimal _inb_ex_rate;
        private decimal _inb_free_qty;
        private string _inb_grup_cur_cd;
        private decimal _inb_grup_ex_rate;
        private decimal _inb_isu_qty;
        private string _inb_itm_cd;
        private Int32 _inb_itm_line;
        private string _inb_itm_stus;
        private string _inb_job_no;
        private string _inb_loc;
        private decimal _inb_qty;
        private Int32 _inb_res_lineno;
        private string _inb_res_no;
        private decimal _inb_res_qty;
        private Int32 _inb_seq_no;
        private decimal _inb_unit_cost;
        private decimal _inb_unit_price;
        private Int32 _inb_base_refline; //Add Chamal 12-07-2012
        #endregion

        public string Inb_base_doc_no { get { return _inb_base_doc_no; } set { _inb_base_doc_no = value; } }
        public string Inb_base_doc_no1 { get { return _inb_base_doc_no1; } set { _inb_base_doc_no1 = value; } }
        public string Inb_base_doc_no2 { get { return _inb_base_doc_no2; } set { _inb_base_doc_no2 = value; } }
        public string Inb_base_doc_no3 { get { return _inb_base_doc_no3; } set { _inb_base_doc_no3 = value; } }
        public string Inb_base_itmcd { get { return _inb_base_itmcd; } set { _inb_base_itmcd = value; } }
        public Int32 Inb_base_itmline { get { return _inb_base_itmline; } set { _inb_base_itmline = value; } }
        public Int32 Inb_base_batchline { get { return _inb_base_batchline; } set { _inb_base_batchline = value; } }
        public string Inb_base_itmstus { get { return _inb_base_itmstus; } set { _inb_base_itmstus = value; } }
        public decimal Inb_base_qty { get { return _inb_base_qty; } set { _inb_base_qty = value; } }
        public string Inb_base_ref_no { get { return _inb_base_ref_no; } set { _inb_base_ref_no = value; } }
        public Int32 Inb_batch_line { get { return _inb_batch_line; } set { _inb_batch_line = value; } }
        public string Inb_batch_no { get { return _inb_batch_no; } set { _inb_batch_no = value; } }
        public string Inb_bin { get { return _inb_bin; } set { _inb_bin = value; } }
        public string Inb_com { get { return _inb_com; } set { _inb_com = value; } }
        public string Inb_cur_cd { get { return _inb_cur_cd; } set { _inb_cur_cd = value; } }
        public DateTime Inb_doc_dt { get { return _inb_doc_dt; } set { _inb_doc_dt = value; } }
        public string Inb_doc_no { get { return _inb_doc_no; } set { _inb_doc_no = value; } }
        public decimal Inb_excess_qty { get { return _inb_excess_qty; } set { _inb_excess_qty = value; } }
        public decimal Inb_ex_rate { get { return _inb_ex_rate; } set { _inb_ex_rate = value; } }
        public decimal Inb_free_qty { get { return _inb_free_qty; } set { _inb_free_qty = value; } }
        public string Inb_grup_cur_cd { get { return _inb_grup_cur_cd; } set { _inb_grup_cur_cd = value; } }
        public decimal Inb_grup_ex_rate { get { return _inb_grup_ex_rate; } set { _inb_grup_ex_rate = value; } }
        public decimal Inb_isu_qty { get { return _inb_isu_qty; } set { _inb_isu_qty = value; } }
        public string Inb_itm_cd { get { return _inb_itm_cd; } set { _inb_itm_cd = value; } }
        public Int32 Inb_itm_line { get { return _inb_itm_line; } set { _inb_itm_line = value; } }
        public string Inb_itm_stus { get { return _inb_itm_stus; } set { _inb_itm_stus = value; } }
        public string Inb_job_no { get { return _inb_job_no; } set { _inb_job_no = value; } }
        public string Inb_loc { get { return _inb_loc; } set { _inb_loc = value; } }
        public decimal Inb_qty { get { return _inb_qty; } set { _inb_qty = value; } }
        public Int32 Inb_res_lineno { get { return _inb_res_lineno; } set { _inb_res_lineno = value; } }
        public string Inb_res_no { get { return _inb_res_no; } set { _inb_res_no = value; } }
        public decimal Inb_res_qty { get { return _inb_res_qty; } set { _inb_res_qty = value; } }
        public Int32 Inb_seq_no { get { return _inb_seq_no; } set { _inb_seq_no = value; } }
        public decimal Inb_unit_cost { get { return _inb_unit_cost; } set { _inb_unit_cost = value; } }
        public decimal Inb_unit_price { get { return _inb_unit_price; } set { _inb_unit_price = value; } }
        public Int32 Inb_base_refline { get { return _inb_base_refline; } set { _inb_base_refline = value; } }

        public static InventoryBatchRefN Converter(DataRow row)
        {
            return new InventoryBatchRefN
            {
                Inb_base_doc_no = row["INB_BASE_DOC_NO"] == DBNull.Value ? string.Empty : row["INB_BASE_DOC_NO"].ToString(),
                Inb_base_doc_no1 = row["INB_BASE_DOC_NO1"] == DBNull.Value ? string.Empty : row["INB_BASE_DOC_NO1"].ToString(),
                Inb_base_doc_no2 = row["INB_BASE_DOC_NO2"] == DBNull.Value ? string.Empty : row["INB_BASE_DOC_NO2"].ToString(),
                Inb_base_doc_no3 = row["INB_BASE_DOC_NO3"] == DBNull.Value ? string.Empty : row["INB_BASE_DOC_NO3"].ToString(),
                Inb_base_itmcd = row["INB_BASE_ITMCD"] == DBNull.Value ? string.Empty : row["INB_BASE_ITMCD"].ToString(),
                Inb_base_itmline = row["INB_BASE_ITMLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["INB_BASE_ITMLINE"]),
                Inb_base_batchline = row["INB_BASE_BATCHLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["INB_BASE_BATCHLINE"]),
                Inb_base_itmstus = row["INB_BASE_ITMSTUS"] == DBNull.Value ? string.Empty : row["INB_BASE_ITMSTUS"].ToString(),
                Inb_base_qty = row["INB_BASE_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INB_BASE_QTY"]),
                Inb_base_ref_no = row["INB_BASE_REF_NO"] == DBNull.Value ? string.Empty : row["INB_BASE_REF_NO"].ToString(),
                Inb_batch_line = row["INB_BATCH_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["INB_BATCH_LINE"]),
                Inb_batch_no = row["INB_BATCH_NO"] == DBNull.Value ? string.Empty : row["INB_BATCH_NO"].ToString(),
                Inb_bin = row["INB_BIN"] == DBNull.Value ? string.Empty : row["INB_BIN"].ToString(),
                Inb_com = row["INB_COM"] == DBNull.Value ? string.Empty : row["INB_COM"].ToString(),
                Inb_cur_cd = row["INB_CUR_CD"] == DBNull.Value ? string.Empty : row["INB_CUR_CD"].ToString(),
                Inb_doc_dt = row["INB_DOC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INB_DOC_DT"]),
                Inb_doc_no = row["INB_DOC_NO"] == DBNull.Value ? string.Empty : row["INB_DOC_NO"].ToString(),
                Inb_excess_qty = row["INB_EXCESS_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INB_EXCESS_QTY"]),
                Inb_ex_rate = row["INB_EX_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INB_EX_RATE"]),
                Inb_free_qty = row["INB_FREE_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INB_FREE_QTY"]),
                Inb_grup_cur_cd = row["INB_GRUP_CUR_CD"] == DBNull.Value ? string.Empty : row["INB_GRUP_CUR_CD"].ToString(),
                Inb_grup_ex_rate = row["INB_GRUP_EX_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INB_GRUP_EX_RATE"]),
                Inb_isu_qty = row["INB_ISU_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INB_ISU_QTY"]),
                Inb_itm_cd = row["INB_ITM_CD"] == DBNull.Value ? string.Empty : row["INB_ITM_CD"].ToString(),
                Inb_itm_line = row["INB_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["INB_ITM_LINE"]),
                Inb_itm_stus = row["INB_ITM_STUS"] == DBNull.Value ? string.Empty : row["INB_ITM_STUS"].ToString(),
                Inb_job_no = row["INB_JOB_NO"] == DBNull.Value ? string.Empty : row["INB_JOB_NO"].ToString(),
                Inb_loc = row["INB_LOC"] == DBNull.Value ? string.Empty : row["INB_LOC"].ToString(),
                Inb_qty = row["INB_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INB_QTY"]),
                Inb_res_lineno = row["INB_RES_LINENO"] == DBNull.Value ? 0 : Convert.ToInt32(row["INB_RES_LINENO"]),
                Inb_res_no = row["INB_RES_NO"] == DBNull.Value ? string.Empty : row["INB_RES_NO"].ToString(),
                Inb_res_qty = row["INB_RES_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INB_RES_QTY"]),
                Inb_seq_no = row["INB_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["INB_SEQ_NO"]),
                Inb_unit_cost = row["INB_UNIT_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INB_UNIT_COST"]),
                Inb_unit_price = row["INB_UNIT_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INB_UNIT_PRICE"]),
                Inb_base_refline = row["INB_BASE_REFLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["INB_BASE_REFLINE"])

            };
        }
    }
}




