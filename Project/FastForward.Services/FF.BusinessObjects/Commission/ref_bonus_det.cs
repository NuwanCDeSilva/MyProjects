using System;
using System.Data;

namespace FF.BusinessObjects
{

    public class ref_bonus_det
    {
        public Int32 Rbd_seq { get; set; }
        public String Rbd_docno { get; set; }
        public String Rbd_item_cd { get; set; }
        public String Rbd_cat1 { get; set; }
        public String Rbd_cat2 { get; set; }
        public String Rbd_model { get; set; }
        public String Rbd_pb { get; set; }
        public String Rbd_pl { get; set; }
        public String Rbd_price_circul { get; set; }
        public String Rbd_cus_cd { get; set; }
        public String Rbd_hp_schm { get; set; }
        public String Rbd_sales_tp { get; set; }
        public String Rbd_pay_mode { get; set; }
        public String Rbd_pay_sub_tp { get; set; }
        public String Rbd_slab_base { get; set; }
        public Int64 Rbd_from_val { get; set; }
        public Int64 Rdb_to_val { get; set; }
        public decimal Rdb_marks { get; set; }
        public String Rdb_anal1 { get; set; }
        public String Rdb_anal2 { get; set; }
        public String Rdb_anal3 { get; set; }
        public string Rdb_brand { get; set; }
        public Int32 Rdb_line { get; set; }
        public decimal Rdb_tot_comb_qty { get; set; }
        public string Rdb_anal4 { get; set; }
        public string Rdb_anal5 { get; set; }
        public static ref_bonus_det Converter(DataRow row)
        {
            return new ref_bonus_det
            {
                Rbd_seq = row["RBD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["RBD_SEQ"].ToString()),
                Rbd_docno = row["RBD_DOCNO"] == DBNull.Value ? string.Empty : row["RBD_DOCNO"].ToString(),
                Rbd_item_cd = row["RBD_ITEM_CD"] == DBNull.Value ? string.Empty : row["RBD_ITEM_CD"].ToString(),
                Rbd_cat1 = row["RBD_CAT1"] == DBNull.Value ? string.Empty : row["RBD_CAT1"].ToString(),
                Rbd_cat2 = row["RBD_CAT2"] == DBNull.Value ? string.Empty : row["RBD_CAT2"].ToString(),
                Rbd_model = row["RBD_MODEL"] == DBNull.Value ? string.Empty : row["RBD_MODEL"].ToString(),
                Rbd_pb = row["RBD_PB"] == DBNull.Value ? string.Empty : row["RBD_PB"].ToString(),
                Rbd_pl = row["RBD_PL"] == DBNull.Value ? string.Empty : row["RBD_PL"].ToString(),
                Rbd_price_circul = row["RBD_PRICE_CIRCUL"] == DBNull.Value ? string.Empty : row["RBD_PRICE_CIRCUL"].ToString(),
                Rbd_cus_cd = row["RBD_CUS_CD"] == DBNull.Value ? string.Empty : row["RBD_CUS_CD"].ToString(),
                Rbd_hp_schm = row["RBD_HP_SCHM"] == DBNull.Value ? string.Empty : row["RBD_HP_SCHM"].ToString(),
                Rbd_sales_tp = row["RBD_SALES_TP"] == DBNull.Value ? string.Empty : row["RBD_SALES_TP"].ToString(),
                Rbd_pay_mode = row["RBD_PAY_MODE"] == DBNull.Value ? string.Empty : row["RBD_PAY_MODE"].ToString(),
                Rbd_pay_sub_tp = row["RBD_PAY_SUB_TP"] == DBNull.Value ? string.Empty : row["RBD_PAY_SUB_TP"].ToString(),
                Rbd_slab_base = row["RBD_SLAB_BASE"] == DBNull.Value ? string.Empty : row["RBD_SLAB_BASE"].ToString(),
                Rbd_from_val = row["RBD_FROM_VAL"] == DBNull.Value ? 0 : Convert.ToInt32(row["RBD_FROM_VAL"].ToString()),
                Rdb_to_val = row["RDB_TO_VAL"] == DBNull.Value ? 0 : Convert.ToInt32(row["RDB_TO_VAL"].ToString()),
                Rdb_marks = row["RDB_MARKS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RDB_MARKS"].ToString()),
                Rdb_anal1 = row["RDB_ANAL1"] == DBNull.Value ? string.Empty : row["RDB_ANAL1"].ToString(),
                Rdb_anal2 = row["RDB_ANAL2"] == DBNull.Value ? string.Empty : row["RDB_ANAL2"].ToString(),
                Rdb_anal3 = row["RDB_ANAL3"] == DBNull.Value ? string.Empty : row["RDB_ANAL3"].ToString(),
                Rdb_brand = row["RDB_BRAND"] == DBNull.Value ? string.Empty : row["RDB_BRAND"].ToString(),
                Rdb_line = row["RDB_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["RDB_LINE"].ToString()),
                Rdb_tot_comb_qty = row["RDB_TOT_COMB_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RDB_TOT_COMB_QTY"].ToString()),
                Rdb_anal4 = row["RDB_ANAL4"] == DBNull.Value ? string.Empty : row["RDB_ANAL4"].ToString(),
                Rdb_anal5 = row["RDB_ANAL5"] == DBNull.Value ? string.Empty : row["RDB_ANAL5"].ToString(),
            };
        }
    }
}

