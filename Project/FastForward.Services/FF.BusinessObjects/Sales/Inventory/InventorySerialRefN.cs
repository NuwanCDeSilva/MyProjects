using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class InventorySerialRefN
    {
        #region Private Members
        private Int32 _ins_available;
        private Int32 _ins_batch_line;
        private string _ins_bin;
        private string _ins_com;
        private Int32 _ins_cross_batchline;
        private Int32 _ins_cross_itmline;
        private Int32 _ins_cross_seqno;
        private Int32 _ins_cross_serline;
        private Boolean _ins_direct;
        private DateTime _ins_doc_dt;
        private string _ins_doc_no;
        private string _ins_exist_grncom;
        private DateTime _ins_exist_grndt;
        private string _ins_exist_grnno;
        private string _ins_exist_supp;
        private DateTime _ins_issue_dt;
        private string _ins_itm_cd;
        private Int32 _ins_itm_line;
        private string _ins_itm_stus;
        private string _ins_loc;
        private string _ins_orig_grncom;
        private DateTime _ins_orig_grndt;
        private string _ins_orig_grnno;
        private string _ins_orig_supp;
        private Int32 _ins_seq_no;
        private string _ins_ser_1;
        private string _ins_ser_2;
        private string _ins_ser_3;
        private string _ins_ser_4;
        private Int32 _ins_ser_id;
        private Int32 _ins_ser_line;
        private decimal _ins_unit_cost;
        private decimal _ins_unit_price;
        private string _ins_warr_no;
        private Int32 _ins_warr_period;
        private Int32 _ins_pick;
        private Int32 _ins_reversed; //add Chamal 21/07/2012 (when the SRN time, update as 1 in DO record)

        public Int32 Ins_available { get { return _ins_available; } set { _ins_available = value; } }
        public Int32 Ins_batch_line { get { return _ins_batch_line; } set { _ins_batch_line = value; } }
        public string Ins_bin { get { return _ins_bin; } set { _ins_bin = value; } }
        public string Ins_com { get { return _ins_com; } set { _ins_com = value; } }
        public Int32 Ins_cross_batchline { get { return _ins_cross_batchline; } set { _ins_cross_batchline = value; } }
        public Int32 Ins_cross_itmline { get { return _ins_cross_itmline; } set { _ins_cross_itmline = value; } }
        public Int32 Ins_cross_seqno { get { return _ins_cross_seqno; } set { _ins_cross_seqno = value; } }
        public Int32 Ins_cross_serline { get { return _ins_cross_serline; } set { _ins_cross_serline = value; } }
        public Boolean Ins_direct { get { return _ins_direct; } set { _ins_direct = value; } }
        public DateTime Ins_doc_dt { get { return _ins_doc_dt; } set { _ins_doc_dt = value; } }
        public string Ins_doc_no { get { return _ins_doc_no; } set { _ins_doc_no = value; } }
        public string Ins_exist_grncom { get { return _ins_exist_grncom; } set { _ins_exist_grncom = value; } }
        public DateTime Ins_exist_grndt { get { return _ins_exist_grndt; } set { _ins_exist_grndt = value; } }
        public string Ins_exist_grnno { get { return _ins_exist_grnno; } set { _ins_exist_grnno = value; } }
        public string Ins_exist_supp { get { return _ins_exist_supp; } set { _ins_exist_supp = value; } }
        public DateTime Ins_issue_dt { get { return _ins_issue_dt; } set { _ins_issue_dt = value; } }
        public string Ins_itm_cd { get { return _ins_itm_cd; } set { _ins_itm_cd = value; } }
        public Int32 Ins_itm_line { get { return _ins_itm_line; } set { _ins_itm_line = value; } }
        public string Ins_itm_stus { get { return _ins_itm_stus; } set { _ins_itm_stus = value; } }
        public string Ins_loc { get { return _ins_loc; } set { _ins_loc = value; } }
        public string Ins_orig_grncom { get { return _ins_orig_grncom; } set { _ins_orig_grncom = value; } }
        public DateTime Ins_orig_grndt { get { return _ins_orig_grndt; } set { _ins_orig_grndt = value; } }
        public string Ins_orig_grnno { get { return _ins_orig_grnno; } set { _ins_orig_grnno = value; } }
        public string Ins_orig_supp { get { return _ins_orig_supp; } set { _ins_orig_supp = value; } }
        public Int32 Ins_seq_no { get { return _ins_seq_no; } set { _ins_seq_no = value; } }
        public string Ins_ser_1 { get { return _ins_ser_1; } set { _ins_ser_1 = value; } }
        public string Ins_ser_2 { get { return _ins_ser_2; } set { _ins_ser_2 = value; } }
        public string Ins_ser_3 { get { return _ins_ser_3; } set { _ins_ser_3 = value; } }
        public string Ins_ser_4 { get { return _ins_ser_4; } set { _ins_ser_4 = value; } }
        public Int32 Ins_ser_id { get { return _ins_ser_id; } set { _ins_ser_id = value; } }
        public Int32 Ins_ser_line { get { return _ins_ser_line; } set { _ins_ser_line = value; } }
        public decimal Ins_unit_cost { get { return _ins_unit_cost; } set { _ins_unit_cost = value; } }
        public decimal Ins_unit_price { get { return _ins_unit_price; } set { _ins_unit_price = value; } }
        public string Ins_warr_no { get { return _ins_warr_no; } set { _ins_warr_no = value; } }
        public Int32 Ins_warr_period { get { return _ins_warr_period; } set { _ins_warr_period = value; } }
        public Int32 Ins_pick { get { return _ins_pick; } set { _ins_pick = value; } }
        public Int32 Ins_reversed { get { return _ins_reversed; } set { _ins_reversed = value; } }
        #endregion

        public static InventorySerialRefN Converter(DataRow row)
        {
            return new InventorySerialRefN
            {
                Ins_available = row["INS_AVAILABLE"] == DBNull.Value ? 0 : Convert.ToInt32(row["INS_AVAILABLE"]),
                Ins_batch_line = row["INS_BATCH_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["INS_BATCH_LINE"]),
                Ins_bin = row["INS_BIN"] == DBNull.Value ? string.Empty : row["INS_BIN"].ToString(),
                Ins_com = row["INS_COM"] == DBNull.Value ? string.Empty : row["INS_COM"].ToString(),
                Ins_cross_batchline = row["INS_CROSS_BATCHLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["INS_CROSS_BATCHLINE"]),
                Ins_cross_itmline = row["INS_CROSS_ITMLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["INS_CROSS_ITMLINE"]),
                Ins_cross_seqno = row["INS_CROSS_SEQNO"] == DBNull.Value ? 0 : Convert.ToInt32(row["INS_CROSS_SEQNO"]),
                Ins_cross_serline = row["INS_CROSS_SERLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["INS_CROSS_SERLINE"]),
                Ins_direct = row["INS_DIRECT"] == DBNull.Value ? false : Convert.ToBoolean(row["INS_DIRECT"]),
                Ins_doc_dt = row["INS_DOC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INS_DOC_DT"]),
                Ins_doc_no = row["INS_DOC_NO"] == DBNull.Value ? string.Empty : row["INS_DOC_NO"].ToString(),
                Ins_exist_grncom = row["INS_EXIST_GRNCOM"] == DBNull.Value ? string.Empty : row["INS_EXIST_GRNCOM"].ToString(),
                Ins_exist_grndt = row["INS_EXIST_GRNDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INS_EXIST_GRNDT"]),
                Ins_exist_grnno = row["INS_EXIST_GRNNO"] == DBNull.Value ? string.Empty : row["INS_EXIST_GRNNO"].ToString(),
                Ins_exist_supp = row["INS_EXIST_SUPP"] == DBNull.Value ? string.Empty : row["INS_EXIST_SUPP"].ToString(),
                Ins_issue_dt = row["INS_ISSUE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INS_ISSUE_DT"]),
                Ins_itm_cd = row["INS_ITM_CD"] == DBNull.Value ? string.Empty : row["INS_ITM_CD"].ToString(),
                Ins_itm_line = row["INS_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["INS_ITM_LINE"]),
                Ins_itm_stus = row["INS_ITM_STUS"] == DBNull.Value ? string.Empty : row["INS_ITM_STUS"].ToString(),
                Ins_loc = row["INS_LOC"] == DBNull.Value ? string.Empty : row["INS_LOC"].ToString(),
                Ins_orig_grncom = row["INS_ORIG_GRNCOM"] == DBNull.Value ? string.Empty : row["INS_ORIG_GRNCOM"].ToString(),
                Ins_orig_grndt = row["INS_ORIG_GRNDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INS_ORIG_GRNDT"]),
                Ins_orig_grnno = row["INS_ORIG_GRNNO"] == DBNull.Value ? string.Empty : row["INS_ORIG_GRNNO"].ToString(),
                Ins_orig_supp = row["INS_ORIG_SUPP"] == DBNull.Value ? string.Empty : row["INS_ORIG_SUPP"].ToString(),
                Ins_seq_no = row["INS_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["INS_SEQ_NO"]),
                Ins_ser_1 = row["INS_SER_1"] == DBNull.Value ? string.Empty : row["INS_SER_1"].ToString(),
                Ins_ser_2 = row["INS_SER_2"] == DBNull.Value ? string.Empty : row["INS_SER_2"].ToString(),
                Ins_ser_3 = row["INS_SER_3"] == DBNull.Value ? string.Empty : row["INS_SER_3"].ToString(),
                Ins_ser_4 = row["INS_SER_4"] == DBNull.Value ? string.Empty : row["INS_SER_4"].ToString(),
                Ins_ser_id = row["INS_SER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["INS_SER_ID"]),
                Ins_ser_line = row["INS_SER_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["INS_SER_LINE"]),
                Ins_unit_cost = row["INS_UNIT_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INS_UNIT_COST"]),
                Ins_unit_price = row["INS_UNIT_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INS_UNIT_PRICE"]),
                Ins_warr_no = row["INS_WARR_NO"] == DBNull.Value ? string.Empty : row["INS_WARR_NO"].ToString(),
                Ins_warr_period = row["INS_WARR_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["INS_WARR_PERIOD"])
            };
        }
    }
}

