using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class InventoryBatchN : InventoryBatchRefN
    {
        #region Private Members
        private decimal _itb_bal_qty1;
        private decimal _itb_bal_qty2;
        private Int32 _itb_base_batchline;
        private DateTime _itb_base_doc_dt;
        //private string _itb_base_doc_no;
        //private string _itb_base_doc_no1;
        //private string _itb_base_doc_no2;
        //private string _itb_base_doc_no3;
        //private string _itb_base_itmcd;
        //private Int32 _itb_base_itmline;
        //private string _itb_base_itmstus;
        //private string _itb_base_ref_no;
        //private Int32 _itb_batch_line;
        //private string _itb_batch_no;
        //private string _itb_bin;
        //private string _itb_com;
        //private string _itb_cur_cd;
        //private string _itb_doc_no;
        private Boolean _itb_git_ignore;
        private string _itb_git_ignore_by;
        private DateTime _itb_git_ignore_dt;
        private DateTime _itb_git_ignore_effdt;
        //private string _itb_grup_cur_cd;
        //private string _itb_itm_cd;
        //private Int32 _itb_itm_line;
        //private string _itb_itm_stus;
        //private string _itb_job_no;
        //private string _itb_loc;
        //private decimal _itb_qty;
        //private Int32 _itb_res_lineno;
        //private string _itb_res_no;
        //private Int32 _itb_seq_no;
        //private decimal _itb_unit_cost;
        //private decimal _itb_unit_price;
        #endregion

        public decimal Itb_bal_qty1 { get { return _itb_bal_qty1; } set { _itb_bal_qty1 = value; } }
        public decimal Itb_bal_qty2 { get { return _itb_bal_qty2; } set { _itb_bal_qty2 = value; } }
        public Int32 Itb_base_batchline { get { return _itb_base_batchline; } set { _itb_base_batchline = value; } }
        public DateTime Itb_base_doc_dt { get { return _itb_base_doc_dt; } set { _itb_base_doc_dt = value; } }
        //public string Itb_base_doc_no { get { return _itb_base_doc_no; } set { _itb_base_doc_no = value; } }
        //public string Itb_base_doc_no1 { get { return _itb_base_doc_no1; } set { _itb_base_doc_no1 = value; } }
        //public string Itb_base_doc_no2 { get { return _itb_base_doc_no2; } set { _itb_base_doc_no2 = value; } }
        //public string Itb_base_doc_no3 { get { return _itb_base_doc_no3; } set { _itb_base_doc_no3 = value; } }
        //public string Itb_base_itmcd { get { return _itb_base_itmcd; } set { _itb_base_itmcd = value; } }
        //public Int32 Itb_base_itmline { get { return _itb_base_itmline; } set { _itb_base_itmline = value; } }
        //public string Itb_base_itmstus { get { return _itb_base_itmstus; } set { _itb_base_itmstus = value; } }
        //public string Itb_base_ref_no { get { return _itb_base_ref_no; } set { _itb_base_ref_no = value; } }
        //public Int32 Itb_batch_line { get { return _itb_batch_line; } set { _itb_batch_line = value; } }
        //public string Itb_batch_no { get { return _itb_batch_no; } set { _itb_batch_no = value; } }
        //public string Itb_bin { get { return _itb_bin; } set { _itb_bin = value; } }
        //public string Itb_com { get { return _itb_com; } set { _itb_com = value; } }
        //public string Itb_cur_cd { get { return _itb_cur_cd; } set { _itb_cur_cd = value; } }
        //public string Itb_doc_no { get { return _itb_doc_no; } set { _itb_doc_no = value; } }
        public Boolean Itb_git_ignore { get { return _itb_git_ignore; } set { _itb_git_ignore = value; } }
        public string Itb_git_ignore_by { get { return _itb_git_ignore_by; } set { _itb_git_ignore_by = value; } }
        public DateTime Itb_git_ignore_dt { get { return _itb_git_ignore_dt; } set { _itb_git_ignore_dt = value; } }
        public DateTime Itb_git_ignore_effdt { get { return _itb_git_ignore_effdt; } set { _itb_git_ignore_effdt = value; } }
        public Int32 Tmp_is_serialized { get; set; }
        //public string Itb_grup_cur_cd { get { return _itb_grup_cur_cd; } set { _itb_grup_cur_cd = value; } }
        //public string Itb_itm_cd { get { return _itb_itm_cd; } set { _itb_itm_cd = value; } }
        //public Int32 Itb_itm_line { get { return _itb_itm_line; } set { _itb_itm_line = value; } }
        //public string Itb_itm_stus { get { return _itb_itm_stus; } set { _itb_itm_stus = value; } }
        //public string Itb_job_no { get { return _itb_job_no; } set { _itb_job_no = value; } }
        //public string Itb_loc { get { return _itb_loc; } set { _itb_loc = value; } }
        //public decimal Itb_qty { get { return _itb_qty; } set { _itb_qty = value; } }
        //public Int32 Itb_res_lineno { get { return _itb_res_lineno; } set { _itb_res_lineno = value; } }
        //public string Itb_res_no { get { return _itb_res_no; } set { _itb_res_no = value; } }
        //public Int32 Itb_seq_no { get { return _itb_seq_no; } set { _itb_seq_no = value; } }
        //public decimal Itb_unit_cost { get { return _itb_unit_cost; } set { _itb_unit_cost = value; } }
        //public decimal Itb_unit_price { get { return _itb_unit_price; } set { _itb_unit_price = value; } }
        public decimal TmpBalQty { get; set; }
        public static InventoryBatchN Converter(DataRow row)
        {
            return new InventoryBatchN
            {
                Itb_bal_qty1 = row["ITB_BAL_QTY1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITB_BAL_QTY1"]),
                Itb_bal_qty2 = row["ITB_BAL_QTY2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITB_BAL_QTY2"]),
                Itb_base_batchline = row["ITB_BASE_BATCHLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_BASE_BATCHLINE"]),
                Itb_base_doc_dt = row["ITB_BASE_DOC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITB_BASE_DOC_DT"]),
                Inb_base_doc_no = row["ITB_BASE_DOC_NO"] == DBNull.Value ? string.Empty : row["ITB_BASE_DOC_NO"].ToString(),
                Inb_base_doc_no1 = row["ITB_BASE_DOC_NO1"] == DBNull.Value ? string.Empty : row["ITB_BASE_DOC_NO1"].ToString(),
                Inb_base_doc_no2 = row["ITB_BASE_DOC_NO2"] == DBNull.Value ? string.Empty : row["ITB_BASE_DOC_NO2"].ToString(),
                Inb_base_doc_no3 = row["ITB_BASE_DOC_NO3"] == DBNull.Value ? string.Empty : row["ITB_BASE_DOC_NO3"].ToString(),
                Inb_base_itmcd = row["ITB_BASE_ITMCD"] == DBNull.Value ? string.Empty : row["ITB_BASE_ITMCD"].ToString(),
                Inb_base_itmline = row["ITB_BASE_ITMLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_BASE_ITMLINE"]),
                Inb_base_batchline = row["ITB_BASE_BATCHLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_BASE_BATCHLINE"]),
                Inb_base_itmstus = row["ITB_BASE_ITMSTUS"] == DBNull.Value ? string.Empty : row["ITB_BASE_ITMSTUS"].ToString(),
                Inb_base_ref_no = row["ITB_BASE_REF_NO"] == DBNull.Value ? string.Empty : row["ITB_BASE_REF_NO"].ToString(),
                Inb_batch_line = row["ITB_BATCH_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_BATCH_LINE"]),
                Inb_batch_no = row["ITB_BATCH_NO"] == DBNull.Value ? string.Empty : row["ITB_BATCH_NO"].ToString(),
                Inb_bin = row["ITB_BIN"] == DBNull.Value ? string.Empty : row["ITB_BIN"].ToString(),
                Inb_com = row["ITB_COM"] == DBNull.Value ? string.Empty : row["ITB_COM"].ToString(),
                Inb_cur_cd = row["ITB_CUR_CD"] == DBNull.Value ? string.Empty : row["ITB_CUR_CD"].ToString(),
                Inb_doc_no = row["ITB_DOC_NO"] == DBNull.Value ? string.Empty : row["ITB_DOC_NO"].ToString(),
                Itb_git_ignore = row["ITB_GIT_IGNORE"] == DBNull.Value ? false : Convert.ToBoolean(row["ITB_GIT_IGNORE"]),
                Itb_git_ignore_by = row["ITB_GIT_IGNORE_BY"] == DBNull.Value ? string.Empty : row["ITB_GIT_IGNORE_BY"].ToString(),
                Itb_git_ignore_dt = row["ITB_GIT_IGNORE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITB_GIT_IGNORE_DT"]),
                Itb_git_ignore_effdt = row["ITB_GIT_IGNORE_EFFDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITB_GIT_IGNORE_EFFDT"]),
                Inb_grup_cur_cd = row["ITB_GRUP_CUR_CD"] == DBNull.Value ? string.Empty : row["ITB_GRUP_CUR_CD"].ToString(),
                Inb_itm_cd = row["ITB_ITM_CD"] == DBNull.Value ? string.Empty : row["ITB_ITM_CD"].ToString(),
                Inb_itm_line = row["ITB_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_ITM_LINE"]),
                Inb_itm_stus = row["ITB_ITM_STUS"] == DBNull.Value ? string.Empty : row["ITB_ITM_STUS"].ToString(),
                Inb_job_no = row["ITB_JOB_NO"] == DBNull.Value ? string.Empty : row["ITB_JOB_NO"].ToString(),
                Inb_loc = row["ITB_LOC"] == DBNull.Value ? string.Empty : row["ITB_LOC"].ToString(),
                Inb_qty = row["ITB_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITB_QTY"]),
                Inb_res_lineno = row["ITB_RES_LINENO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_RES_LINENO"]),
                Inb_res_no = row["ITB_RES_NO"] == DBNull.Value ? string.Empty : row["ITB_RES_NO"].ToString(),
                Inb_seq_no = row["ITB_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_SEQ_NO"]),
                Inb_unit_cost = row["ITB_UNIT_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITB_UNIT_COST"]),
                Inb_unit_price = row["ITB_UNIT_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITB_UNIT_PRICE"]),
                Inb_base_refline = row["ITB_BASE_REFLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_BASE_REFLINE"])
            };
        }
        
        //Add by lakshan
        public static InventoryBatchN ConverterNew(DataRow row)
        {
            return new InventoryBatchN
            {
                Itb_bal_qty1 = row["ITB_BAL_QTY1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITB_BAL_QTY1"]),
                Itb_bal_qty2 = row["ITB_BAL_QTY2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITB_BAL_QTY2"]),
                Itb_base_batchline = row["ITB_BASE_BATCHLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_BASE_BATCHLINE"]),
                Itb_base_doc_dt = row["ITB_BASE_DOC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITB_BASE_DOC_DT"]),
                Inb_base_doc_no = row["ITB_BASE_DOC_NO"] == DBNull.Value ? string.Empty : row["ITB_BASE_DOC_NO"].ToString(),
                Inb_base_doc_no1 = row["ITB_BASE_DOC_NO1"] == DBNull.Value ? string.Empty : row["ITB_BASE_DOC_NO1"].ToString(),
                Inb_base_doc_no2 = row["ITB_BASE_DOC_NO2"] == DBNull.Value ? string.Empty : row["ITB_BASE_DOC_NO2"].ToString(),
                Inb_base_doc_no3 = row["ITB_BASE_DOC_NO3"] == DBNull.Value ? string.Empty : row["ITB_BASE_DOC_NO3"].ToString(),
                Inb_base_itmcd = row["ITB_BASE_ITMCD"] == DBNull.Value ? string.Empty : row["ITB_BASE_ITMCD"].ToString(),
                Inb_base_itmline = row["ITB_BASE_ITMLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_BASE_ITMLINE"]),
                Inb_base_batchline = row["ITB_BASE_BATCHLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_BASE_BATCHLINE"]),
                Inb_base_itmstus = row["ITB_BASE_ITMSTUS"] == DBNull.Value ? string.Empty : row["ITB_BASE_ITMSTUS"].ToString(),
                Inb_base_ref_no = row["ITB_BASE_REF_NO"] == DBNull.Value ? string.Empty : row["ITB_BASE_REF_NO"].ToString(),
                Inb_batch_line = row["ITB_BATCH_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_BATCH_LINE"]),
                Inb_batch_no = row["ITB_BATCH_NO"] == DBNull.Value ? string.Empty : row["ITB_BATCH_NO"].ToString(),
                Inb_bin = row["ITB_BIN"] == DBNull.Value ? string.Empty : row["ITB_BIN"].ToString(),
                Inb_com = row["ITB_COM"] == DBNull.Value ? string.Empty : row["ITB_COM"].ToString(),
                Inb_cur_cd = row["ITB_CUR_CD"] == DBNull.Value ? string.Empty : row["ITB_CUR_CD"].ToString(),
                Inb_doc_no = row["ITB_DOC_NO"] == DBNull.Value ? string.Empty : row["ITB_DOC_NO"].ToString(),
                Itb_git_ignore = row["ITB_GIT_IGNORE"] == DBNull.Value ? false : Convert.ToBoolean(row["ITB_GIT_IGNORE"]),
                Itb_git_ignore_by = row["ITB_GIT_IGNORE_BY"] == DBNull.Value ? string.Empty : row["ITB_GIT_IGNORE_BY"].ToString(),
                Itb_git_ignore_dt = row["ITB_GIT_IGNORE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITB_GIT_IGNORE_DT"]),
                Itb_git_ignore_effdt = row["ITB_GIT_IGNORE_EFFDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITB_GIT_IGNORE_EFFDT"]),
                Inb_grup_cur_cd = row["ITB_GRUP_CUR_CD"] == DBNull.Value ? string.Empty : row["ITB_GRUP_CUR_CD"].ToString(),
                Inb_itm_cd = row["ITB_ITM_CD"] == DBNull.Value ? string.Empty : row["ITB_ITM_CD"].ToString(),
                Inb_itm_line = row["ITB_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_ITM_LINE"]),
                Inb_itm_stus = row["ITB_ITM_STUS"] == DBNull.Value ? string.Empty : row["ITB_ITM_STUS"].ToString(),
                Inb_job_no = row["ITB_JOB_NO"] == DBNull.Value ? string.Empty : row["ITB_JOB_NO"].ToString(),
                Inb_loc = row["ITB_LOC"] == DBNull.Value ? string.Empty : row["ITB_LOC"].ToString(),
                Inb_qty = row["ITB_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITB_QTY"]),
                Inb_res_lineno = row["ITB_RES_LINENO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_RES_LINENO"]),
                Inb_res_no = row["ITB_RES_NO"] == DBNull.Value ? string.Empty : row["ITB_RES_NO"].ToString(),
                Inb_seq_no = row["ITB_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_SEQ_NO"]),
                Inb_unit_cost = row["ITB_UNIT_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITB_UNIT_COST"]),
                Inb_unit_price = row["ITB_UNIT_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITB_UNIT_PRICE"]),
                Inb_base_refline = row["ITB_BASE_REFLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_BASE_REFLINE"]),
                Inb_job_line = row["ITB_JOB_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["itb_job_line"])
            };
        }
        //Add by lakshan 10 Dec 2016
        public static InventoryBatchN Converter3(DataRow row)
        {
            return new InventoryBatchN
            {
                Inb_seq_no = row["ITB_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_SEQ_NO"].ToString()),
                Inb_itm_line = row["ITB_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_ITM_LINE"].ToString()),
                Inb_batch_line = row["ITB_BATCH_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_BATCH_LINE"].ToString()),
                Inb_batch_no = row["ITB_BATCH_NO"] == DBNull.Value ? string.Empty : row["ITB_BATCH_NO"].ToString(),
                Inb_doc_no = row["ITB_DOC_NO"] == DBNull.Value ? string.Empty : row["ITB_DOC_NO"].ToString(),
                Inb_com = row["ITB_COM"] == DBNull.Value ? string.Empty : row["ITB_COM"].ToString(),
                Inb_loc = row["ITB_LOC"] == DBNull.Value ? string.Empty : row["ITB_LOC"].ToString(),
                Inb_bin = row["ITB_BIN"] == DBNull.Value ? string.Empty : row["ITB_BIN"].ToString(),
                Inb_itm_cd = row["ITB_ITM_CD"] == DBNull.Value ? string.Empty : row["ITB_ITM_CD"].ToString(),
                Inb_itm_stus = row["ITB_ITM_STUS"] == DBNull.Value ? string.Empty : row["ITB_ITM_STUS"].ToString(),
                Inb_qty = row["ITB_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITB_QTY"].ToString()),
                Inb_unit_cost = row["ITB_UNIT_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITB_UNIT_COST"].ToString()),
                Itb_bal_qty1 = row["ITB_BAL_QTY1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITB_BAL_QTY1"].ToString()),
                Itb_bal_qty2 = row["ITB_BAL_QTY2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITB_BAL_QTY2"].ToString()),
                Inb_unit_price = row["ITB_UNIT_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITB_UNIT_PRICE"].ToString()),
                Inb_base_doc_no = row["ITB_BASE_DOC_NO"] == DBNull.Value ? string.Empty : row["ITB_BASE_DOC_NO"].ToString(),
                Inb_base_ref_no = row["ITB_BASE_REF_NO"] == DBNull.Value ? string.Empty : row["ITB_BASE_REF_NO"].ToString(),
                Itb_base_doc_dt = row["ITB_BASE_DOC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITB_BASE_DOC_DT"].ToString()),
                Inb_base_itmcd = row["ITB_BASE_ITMCD"] == DBNull.Value ? string.Empty : row["ITB_BASE_ITMCD"].ToString(),
                Inb_base_itmline = row["ITB_BASE_ITMLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_BASE_ITMLINE"].ToString()),
                Inb_base_itmstus = row["ITB_BASE_ITMSTUS"] == DBNull.Value ? string.Empty : row["ITB_BASE_ITMSTUS"].ToString(),
                Inb_job_no = row["ITB_JOB_NO"] == DBNull.Value ? string.Empty : row["ITB_JOB_NO"].ToString(),
                //Itb_git_ignore = row["ITB_GIT_IGNORE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_GIT_IGNORE"].ToString()),
                Itb_git_ignore_by = row["ITB_GIT_IGNORE_BY"] == DBNull.Value ? string.Empty : row["ITB_GIT_IGNORE_BY"].ToString(),
                Itb_git_ignore_dt = row["ITB_GIT_IGNORE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITB_GIT_IGNORE_DT"].ToString()),
                Itb_git_ignore_effdt = row["ITB_GIT_IGNORE_EFFDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITB_GIT_IGNORE_EFFDT"].ToString()),
                Inb_res_no = row["ITB_RES_NO"] == DBNull.Value ? string.Empty : row["ITB_RES_NO"].ToString(),
                Inb_res_lineno = row["ITB_RES_LINENO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_RES_LINENO"].ToString()),
                Inb_base_doc_no1 = row["ITB_BASE_DOC_NO1"] == DBNull.Value ? string.Empty : row["ITB_BASE_DOC_NO1"].ToString(),
                Inb_base_doc_no2 = row["ITB_BASE_DOC_NO2"] == DBNull.Value ? string.Empty : row["ITB_BASE_DOC_NO2"].ToString(),
                Inb_base_doc_no3 = row["ITB_BASE_DOC_NO3"] == DBNull.Value ? string.Empty : row["ITB_BASE_DOC_NO3"].ToString(),
                Inb_cur_cd = row["ITB_CUR_CD"] == DBNull.Value ? string.Empty : row["ITB_CUR_CD"].ToString(),
                Inb_grup_cur_cd = row["ITB_GRUP_CUR_CD"] == DBNull.Value ? string.Empty : row["ITB_GRUP_CUR_CD"].ToString(),
                Itb_base_batchline = row["ITB_BASE_BATCHLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_BASE_BATCHLINE"].ToString()),
                Inb_base_refline = row["ITB_BASE_REFLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_BASE_REFLINE"].ToString()),
                Inb_job_line = row["ITB_JOB_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_JOB_LINE"].ToString()),
                Inb_exp_dt = row["ITB_EXP_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITB_EXP_DT"].ToString()),
                Inb_manufac_dt = row["ITB_MANUFAC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITB_MANUFAC_DT"].ToString()),
                Inb_mitm_cd = row["ITB_MITM_CD"] == DBNull.Value ? string.Empty : row["ITB_MITM_CD"].ToString()
            };
        }
        //Add by lakshan
        public static InventoryBatchN ConverterForValidate(DataRow row)
        {
            return new InventoryBatchN
            {
                Itb_bal_qty1 = row["ITB_BAL_QTY1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITB_BAL_QTY1"]),
                Itb_bal_qty2 = row["ITB_BAL_QTY2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITB_BAL_QTY2"]),
                Itb_base_batchline = row["ITB_BASE_BATCHLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_BASE_BATCHLINE"]),
                Itb_base_doc_dt = row["ITB_BASE_DOC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITB_BASE_DOC_DT"]),
                Inb_base_doc_no = row["ITB_BASE_DOC_NO"] == DBNull.Value ? string.Empty : row["ITB_BASE_DOC_NO"].ToString(),
                Inb_base_doc_no1 = row["ITB_BASE_DOC_NO1"] == DBNull.Value ? string.Empty : row["ITB_BASE_DOC_NO1"].ToString(),
                Inb_base_doc_no2 = row["ITB_BASE_DOC_NO2"] == DBNull.Value ? string.Empty : row["ITB_BASE_DOC_NO2"].ToString(),
                Inb_base_doc_no3 = row["ITB_BASE_DOC_NO3"] == DBNull.Value ? string.Empty : row["ITB_BASE_DOC_NO3"].ToString(),
                Inb_base_itmcd = row["ITB_BASE_ITMCD"] == DBNull.Value ? string.Empty : row["ITB_BASE_ITMCD"].ToString(),
                Inb_base_itmline = row["ITB_BASE_ITMLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_BASE_ITMLINE"]),
                Inb_base_batchline = row["ITB_BASE_BATCHLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_BASE_BATCHLINE"]),
                Inb_base_itmstus = row["ITB_BASE_ITMSTUS"] == DBNull.Value ? string.Empty : row["ITB_BASE_ITMSTUS"].ToString(),
                Inb_base_ref_no = row["ITB_BASE_REF_NO"] == DBNull.Value ? string.Empty : row["ITB_BASE_REF_NO"].ToString(),
                Inb_batch_line = row["ITB_BATCH_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_BATCH_LINE"]),
                Inb_batch_no = row["ITB_BATCH_NO"] == DBNull.Value ? string.Empty : row["ITB_BATCH_NO"].ToString(),
                Inb_bin = row["ITB_BIN"] == DBNull.Value ? string.Empty : row["ITB_BIN"].ToString(),
                Inb_com = row["ITB_COM"] == DBNull.Value ? string.Empty : row["ITB_COM"].ToString(),
                Inb_cur_cd = row["ITB_CUR_CD"] == DBNull.Value ? string.Empty : row["ITB_CUR_CD"].ToString(),
                Inb_doc_no = row["ITB_DOC_NO"] == DBNull.Value ? string.Empty : row["ITB_DOC_NO"].ToString(),
                Itb_git_ignore = row["ITB_GIT_IGNORE"] == DBNull.Value ? false : Convert.ToBoolean(row["ITB_GIT_IGNORE"]),
                Itb_git_ignore_by = row["ITB_GIT_IGNORE_BY"] == DBNull.Value ? string.Empty : row["ITB_GIT_IGNORE_BY"].ToString(),
                Itb_git_ignore_dt = row["ITB_GIT_IGNORE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITB_GIT_IGNORE_DT"]),
                Itb_git_ignore_effdt = row["ITB_GIT_IGNORE_EFFDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITB_GIT_IGNORE_EFFDT"]),
                Inb_grup_cur_cd = row["ITB_GRUP_CUR_CD"] == DBNull.Value ? string.Empty : row["ITB_GRUP_CUR_CD"].ToString(),
                Inb_itm_cd = row["ITB_ITM_CD"] == DBNull.Value ? string.Empty : row["ITB_ITM_CD"].ToString(),
                Inb_itm_line = row["ITB_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_ITM_LINE"]),
                Inb_itm_stus = row["ITB_ITM_STUS"] == DBNull.Value ? string.Empty : row["ITB_ITM_STUS"].ToString(),
                Inb_job_no = row["ITB_JOB_NO"] == DBNull.Value ? string.Empty : row["ITB_JOB_NO"].ToString(),
                Inb_loc = row["ITB_LOC"] == DBNull.Value ? string.Empty : row["ITB_LOC"].ToString(),
                Inb_qty = row["ITB_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITB_QTY"]),
                Inb_res_lineno = row["ITB_RES_LINENO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_RES_LINENO"]),
                Inb_res_no = row["ITB_RES_NO"] == DBNull.Value ? string.Empty : row["ITB_RES_NO"].ToString(),
                Inb_seq_no = row["ITB_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_SEQ_NO"]),
                Inb_unit_cost = row["ITB_UNIT_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITB_UNIT_COST"]),
                Inb_unit_price = row["ITB_UNIT_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITB_UNIT_PRICE"]),
                Inb_base_refline = row["ITB_BASE_REFLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_BASE_REFLINE"]),
                Inb_job_line = row["ITB_JOB_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["itb_job_line"]),
                Tmp_is_serialized = row["mi_is_ser1"] == DBNull.Value ? 0 : Convert.ToInt32(row["mi_is_ser1"])
            };
        }
                  public static InventoryBatchN Converternew2(DataRow row)
        {
            return new InventoryBatchN
            {
                Inb_base_itmcd = row["Inb_base_itmcd"] == DBNull.Value ? string.Empty : row["Inb_base_itmcd"].ToString(),
            };
        }
    }
}


