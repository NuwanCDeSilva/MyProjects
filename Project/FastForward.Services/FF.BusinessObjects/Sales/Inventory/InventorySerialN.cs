using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
  
    public class InventorySerialN : InventorySerialRefN
    {
        #region Private Members
        //private Int32 _its_batch_line;
        //private string _its_bin;
        //private string _its_com;
        //private Int32 _its_cross_batchline;
        //private Int32 _its_cross_itmline;
        //private Int32 _its_cross_seqno;
        //private Int32 _its_cross_serline;
        //private Boolean _its_direct;
        //private DateTime _its_doc_dt;
        //private string _its_doc_no;
        //private string _its_exist_grncom;
        //private DateTime _its_exist_grndt;
        //private string _its_exist_grnno;
        //private string _its_exist_supp;
        //private DateTime _its_issue_dt;
        //private string _its_itm_cd;
        //private Int32 _its_itm_line;
        //private string _its_itm_stus;
        //private string _its_loc;
        //private string _its_orig_grncom;
        //private DateTime _its_orig_grndt;
        //private string _its_orig_grnno;
        //private string _its_orig_supp;
        //private Int32 _its_seq_no;
        //private string _its_ser_1;
        //private string _its_ser_2;
        //private string _its_ser_3;
        //private string _its_ser_4;
        //private Int32 _its_ser_id;
        //private Int32 _its_ser_line;
        //private decimal _its_unit_cost;
        //private string _its_warr_no;
        //private Int32 _its_warr_period;
        #endregion

        //public Int32 Its_batch_line { get { return _its_batch_line; } set { _its_batch_line = value; } }
        //public string Its_bin { get { return _its_bin; } set { _its_bin = value; } }
        //public string Its_com { get { return _its_com; } set { _its_com = value; } }
        //public Int32 Its_cross_batchline { get { return _its_cross_batchline; } set { _its_cross_batchline = value; } }
        //public Int32 Its_cross_itmline { get { return _its_cross_itmline; } set { _its_cross_itmline = value; } }
        //public Int32 Its_cross_seqno { get { return _its_cross_seqno; } set { _its_cross_seqno = value; } }
        //public Int32 Its_cross_serline { get { return _its_cross_serline; } set { _its_cross_serline = value; } }
        //public Boolean Its_direct { get { return _its_direct; } set { _its_direct = value; } }
        //public DateTime Its_doc_dt { get { return _its_doc_dt; } set { _its_doc_dt = value; } }
        //public string Its_doc_no { get { return _its_doc_no; } set { _its_doc_no = value; } }
        //public string Its_exist_grncom { get { return _its_exist_grncom; } set { _its_exist_grncom = value; } }
        //public DateTime Its_exist_grndt { get { return _its_exist_grndt; } set { _its_exist_grndt = value; } }
        //public string Its_exist_grnno { get { return _its_exist_grnno; } set { _its_exist_grnno = value; } }
        //public string Its_exist_supp { get { return _its_exist_supp; } set { _its_exist_supp = value; } }
        //public DateTime Its_issue_dt { get { return _its_issue_dt; } set { _its_issue_dt = value; } }
        //public string Its_itm_cd { get { return _its_itm_cd; } set { _its_itm_cd = value; } }
        //public Int32 Its_itm_line { get { return _its_itm_line; } set { _its_itm_line = value; } }
        //public string Its_itm_stus { get { return _its_itm_stus; } set { _its_itm_stus = value; } }
        //public string Its_loc { get { return _its_loc; } set { _its_loc = value; } }
        //public string Its_orig_grncom { get { return _its_orig_grncom; } set { _its_orig_grncom = value; } }
        //public DateTime Its_orig_grndt { get { return _its_orig_grndt; } set { _its_orig_grndt = value; } }
        //public string Its_orig_grnno { get { return _its_orig_grnno; } set { _its_orig_grnno = value; } }
        //public string Its_orig_supp { get { return _its_orig_supp; } set { _its_orig_supp = value; } }
        //public Int32 Its_seq_no { get { return _its_seq_no; } set { _its_seq_no = value; } }
        //public string Its_ser_1 { get { return _its_ser_1; } set { _its_ser_1 = value; } }
        //public string Its_ser_2 { get { return _its_ser_2; } set { _its_ser_2 = value; } }
        //public string Its_ser_3 { get { return _its_ser_3; } set { _its_ser_3 = value; } }
        //public string Its_ser_4 { get { return _its_ser_4; } set { _its_ser_4 = value; } }
        //public Int32 Its_ser_id { get { return _its_ser_id; } set { _its_ser_id = value; } }
        //public Int32 Its_ser_line { get { return _its_ser_line; } set { _its_ser_line = value; } }
        //public decimal Its_unit_cost { get { return _its_unit_cost; } set { _its_unit_cost = value; } }
        //public string Its_warr_no { get { return _its_warr_no; } set { _its_warr_no = value; } }
        //public Int32 Its_warr_period { get { return _its_warr_period; } set { _its_warr_period = value; } }

        public static InventorySerialN ConverterTotal(DataRow row)
        {
            return new InventorySerialN
            {
                Ins_batch_line = row["ITS_BATCH_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITS_BATCH_LINE"]),
                Ins_bin = row["ITS_BIN"] == DBNull.Value ? string.Empty : row["ITS_BIN"].ToString(),
                Ins_com = row["ITS_COM"] == DBNull.Value ? string.Empty : row["ITS_COM"].ToString(),
                Ins_cross_batchline = row["ITS_CROSS_BATCHLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITS_CROSS_BATCHLINE"]),
                Ins_cross_itmline = row["ITS_CROSS_ITMLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITS_CROSS_ITMLINE"]),
                Ins_cross_seqno = row["ITS_CROSS_SEQNO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITS_CROSS_SEQNO"]),
                Ins_cross_serline = row["ITS_CROSS_SERLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITS_CROSS_SERLINE"]),
                Ins_direct = row["ITS_DIRECT"] == DBNull.Value ? false : Convert.ToBoolean(row["ITS_DIRECT"]),
                Ins_doc_dt = row["ITS_DOC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITS_DOC_DT"]),
                Ins_doc_no = row["ITS_DOC_NO"] == DBNull.Value ? string.Empty : row["ITS_DOC_NO"].ToString(),
                Ins_exist_grncom = row["ITS_EXIST_GRNCOM"] == DBNull.Value ? string.Empty : row["ITS_EXIST_GRNCOM"].ToString(),
                Ins_exist_grndt = row["ITS_EXIST_GRNDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITS_EXIST_GRNDT"]),
                Ins_exist_grnno = row["ITS_EXIST_GRNNO"] == DBNull.Value ? string.Empty : row["ITS_EXIST_GRNNO"].ToString(),
                Ins_exist_supp = row["ITS_EXIST_SUPP"] == DBNull.Value ? string.Empty : row["ITS_EXIST_SUPP"].ToString(),
                Ins_issue_dt = row["ITS_ISSUE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITS_ISSUE_DT"]),
                Ins_itm_cd = row["ITS_ITM_CD"] == DBNull.Value ? string.Empty : row["ITS_ITM_CD"].ToString(),
                Ins_itm_line = row["ITS_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITS_ITM_LINE"]),
                Ins_itm_stus = row["ITS_ITM_STUS"] == DBNull.Value ? string.Empty : row["ITS_ITM_STUS"].ToString(),
                Ins_loc = row["ITS_LOC"] == DBNull.Value ? string.Empty : row["ITS_LOC"].ToString(),
                Ins_orig_grncom = row["ITS_ORIG_GRNCOM"] == DBNull.Value ? string.Empty : row["ITS_ORIG_GRNCOM"].ToString(),
                Ins_orig_grndt = row["ITS_ORIG_GRNDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITS_ORIG_GRNDT"]),
                Ins_orig_grnno = row["ITS_ORIG_GRNNO"] == DBNull.Value ? string.Empty : row["ITS_ORIG_GRNNO"].ToString(),
                Ins_orig_supp = row["ITS_ORIG_SUPP"] == DBNull.Value ? string.Empty : row["ITS_ORIG_SUPP"].ToString(),
                Ins_seq_no = row["ITS_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITS_SEQ_NO"]),
                Ins_ser_1 = row["ITS_SER_1"] == DBNull.Value ? string.Empty : row["ITS_SER_1"].ToString(),
                Ins_ser_2 = row["ITS_SER_2"] == DBNull.Value ? string.Empty : row["ITS_SER_2"].ToString(),
                Ins_ser_3 = row["ITS_SER_3"] == DBNull.Value ? string.Empty : row["ITS_SER_3"].ToString(),
                Ins_ser_4 = row["ITS_SER_4"] == DBNull.Value ? string.Empty : row["ITS_SER_4"].ToString(),
                Ins_ser_id = row["ITS_SER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITS_SER_ID"]),
                Ins_ser_line = row["ITS_SER_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITS_SER_LINE"]),
                Ins_unit_cost = row["ITS_UNIT_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITS_UNIT_COST"]),
                Ins_warr_no = row["ITS_WARR_NO"] == DBNull.Value ? string.Empty : row["ITS_WARR_NO"].ToString(),
                Ins_warr_period = row["ITS_WARR_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITS_WARR_PERIOD"]),
                Ins_pick = row["ITS_PICK"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITS_PICK"]),
                Ins_reversed = row["ITS_REVERSED"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITS_REVERSED"])

            };
        }
    }
}

