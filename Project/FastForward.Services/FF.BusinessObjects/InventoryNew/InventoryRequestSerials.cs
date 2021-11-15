using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class InventoryRequestSerials : MasterItem
    {
        /// <summary>
        /// Code by Chamal on 06/07/2012
        /// </summary>
        #region Private Members
        private Int32 _itrs_in_batchline;
        private DateTime _itrs_in_docdt;
        private string _itrs_in_docno;
        private Int32 _itrs_in_itmline;
        private Int32 _itrs_in_seqno;
        private Int32 _itrs_in_serline;
        private string _itrs_itm_cd;
        private string _itrs_itm_stus;
        private Int32 _itrs_line_no;
        private decimal _itrs_qty;
        private string _itrs_rmk;
        private Int32 _itrs_seq_no;
        private string _itrs_ser_1;
        private string _itrs_ser_2;
        private string _itrs_ser_3;
        private string _itrs_ser_4;
        private Int32 _itrs_ser_id;
        private Int32 _itrs_ser_line;
        private string _itrs_trns_tp;
        private string _itrs_nitm_stus;
        private string _itri_itm_cond;
        private string _Itri_itm_cat;
        private bool _itm_alw_auto_approvel;
        private string _Itri_itm_new_ser;
        private string _ITRS_ITM_NEW_CD;
        private string _ITRS_ITM_SUP;
     
        #endregion

        public Int32 Itrs_in_batchline { get { return _itrs_in_batchline; } set { _itrs_in_batchline = value; } }
        public DateTime Itrs_in_docdt { get { return _itrs_in_docdt; } set { _itrs_in_docdt = value; } }
        public string Itrs_in_docno { get { return _itrs_in_docno; } set { _itrs_in_docno = value; } }
        public Int32 Itrs_in_itmline { get { return _itrs_in_itmline; } set { _itrs_in_itmline = value; } }
        public Int32 Itrs_in_seqno { get { return _itrs_in_seqno; } set { _itrs_in_seqno = value; } }
        public Int32 Itrs_in_serline { get { return _itrs_in_serline; } set { _itrs_in_serline = value; } }
        public string Itrs_itm_cd { get { return _itrs_itm_cd; } set { _itrs_itm_cd = value; } }
        public string Itrs_itm_stus { get { return _itrs_itm_stus; } set { _itrs_itm_stus = value; } }
        public Int32 Itrs_line_no { get { return _itrs_line_no; } set { _itrs_line_no = value; } }
        public decimal Itrs_qty { get { return _itrs_qty; } set { _itrs_qty = value; } }
        public string Itrs_rmk { get { return _itrs_rmk; } set { _itrs_rmk = value; } }
        public Int32 Itrs_seq_no { get { return _itrs_seq_no; } set { _itrs_seq_no = value; } }
        public string Itrs_ser_1 { get { return _itrs_ser_1; } set { _itrs_ser_1 = value; } }
        public string Itrs_ser_2 { get { return _itrs_ser_2; } set { _itrs_ser_2 = value; } }
        public string Itrs_ser_3 { get { return _itrs_ser_3; } set { _itrs_ser_3 = value; } }
        public string Itrs_ser_4 { get { return _itrs_ser_4; } set { _itrs_ser_4 = value; } }
        public Int32 Itrs_ser_id { get { return _itrs_ser_id; } set { _itrs_ser_id = value; } }
        public Int32 Itrs_ser_line { get { return _itrs_ser_line; } set { _itrs_ser_line = value; } }
        public string Itrs_trns_tp { get { return _itrs_trns_tp; } set { _itrs_trns_tp = value; } }
        public string Itrs_nitm_stus { get { return _itrs_nitm_stus; } set { _itrs_nitm_stus = value; } }
        public string Itri_itm_cond { get { return _itri_itm_cond; } set { _itri_itm_cond = value; } }
        public string Itri_itm_cat { get { return _Itri_itm_cat; } set { _Itri_itm_cat = value; } }
        public bool itm_alw_auto_approvel { get { return _itm_alw_auto_approvel; } set { _itm_alw_auto_approvel = value; } }
        public string Itri_itm_new_ser { get { return _Itri_itm_new_ser; } set { _Itri_itm_new_ser = value; } }
        public string ITRS_ITM_NEW_CD { get { return _ITRS_ITM_NEW_CD; } set { _ITRS_ITM_NEW_CD = value; } }
        public string ITRS_ITM_SUP { get { return _ITRS_ITM_SUP; } set { _ITRS_ITM_SUP = value; } } 

        public static InventoryRequestSerials ConvertTotal(DataRow row)
        {
            return new InventoryRequestSerials
            {
                Itrs_in_batchline = row["ITRS_IN_BATCHLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRS_IN_BATCHLINE"]),
                Itrs_in_docdt = row["ITRS_IN_DOCDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITRS_IN_DOCDT"]),
                Itrs_in_docno = row["ITRS_IN_DOCNO"] == DBNull.Value ? string.Empty : row["ITRS_IN_DOCNO"].ToString(),
                Itrs_in_itmline = row["ITRS_IN_ITMLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRS_IN_ITMLINE"]),
                Itrs_in_seqno = row["ITRS_IN_SEQNO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRS_IN_SEQNO"]),
                Itrs_in_serline = row["ITRS_IN_SERLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRS_IN_SERLINE"]),
                Itrs_itm_cd = row["ITRS_ITM_CD"] == DBNull.Value ? string.Empty : row["ITRS_ITM_CD"].ToString(),
                Itrs_itm_stus = row["ITRS_ITM_STUS"] == DBNull.Value ? string.Empty : row["ITRS_ITM_STUS"].ToString(),
                Itrs_line_no = row["ITRS_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRS_LINE_NO"]),
                Itrs_qty = row["ITRS_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRS_QTY"]),
                Itrs_rmk = row["ITRS_RMK"] == DBNull.Value ? string.Empty : row["ITRS_RMK"].ToString(),
                Itrs_seq_no = row["ITRS_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRS_SEQ_NO"]),
                Itrs_ser_1 = row["ITRS_SER_1"] == DBNull.Value ? string.Empty : row["ITRS_SER_1"].ToString(),
                Itrs_ser_2 = row["ITRS_SER_2"] == DBNull.Value ? string.Empty : row["ITRS_SER_2"].ToString(),
                Itrs_ser_3 = row["ITRS_SER_3"] == DBNull.Value ? string.Empty : row["ITRS_SER_3"].ToString(),
                Itrs_ser_4 = row["ITRS_SER_4"] == DBNull.Value ? string.Empty : row["ITRS_SER_4"].ToString(),
                Itrs_ser_id = row["ITRS_SER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRS_SER_ID"]),
                Itrs_ser_line = row["ITRS_SER_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRS_SER_LINE"]),
                Itrs_trns_tp = row["ITRS_TRNS_TP"] == DBNull.Value ? string.Empty : row["ITRS_TRNS_TP"].ToString(),
                Itrs_nitm_stus = row["ITRS_NITM_STUS"] == DBNull.Value ? string.Empty : row["ITRS_NITM_STUS"].ToString(),
                Itri_itm_cond = row["Itri_itm_cond"] == DBNull.Value ? string.Empty : row["Itri_itm_cond"].ToString(),  //kapila 29/5/2014

                Mi_longdesc = row["MI_LONGDESC"] == DBNull.Value ? string.Empty : row["MI_LONGDESC"].ToString(),
                Mi_model = row["MI_MODEL"] == DBNull.Value ? string.Empty : row["MI_MODEL"].ToString(),
                Mi_brand = row["MI_BRAND"] == DBNull.Value ? string.Empty : row["MI_BRAND"].ToString()

            };
        }
    }
}

