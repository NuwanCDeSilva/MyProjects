using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class ReptPickItems
    {
        #region Private Members
        private string _tui_pic_itm_cd;
        private decimal _tui_pic_itm_qty;
        private string _tui_pic_itm_stus;
        private string _tui_req_itm_cd;
        private decimal _tui_req_itm_qty;
        private string _tui_req_itm_stus;
        private Int32 _tui_usrseq_no;
        //kapila 14/8/2015
        private string _tui_sup;
        private string _tui_batch;
        private string _tui_grn;
        private DateTime _tui_grn_dt;
        private DateTime _tui_exp_dt;
        #endregion

        public string Tui_pic_itm_cd { get { return _tui_pic_itm_cd; } set { _tui_pic_itm_cd = value; } }
        public decimal Tui_pic_itm_qty { get { return _tui_pic_itm_qty; } set { _tui_pic_itm_qty = value; } }
        public string Tui_pic_itm_stus { get { return _tui_pic_itm_stus; } set { _tui_pic_itm_stus = value; } }
        public string Tui_req_itm_cd { get { return _tui_req_itm_cd; } set { _tui_req_itm_cd = value; } }
        public decimal Tui_req_itm_qty { get { return _tui_req_itm_qty; } set { _tui_req_itm_qty = value; } }
        public string Tui_req_itm_stus { get { return _tui_req_itm_stus; } set { _tui_req_itm_stus = value; } }
        public Int32 Tui_usrseq_no { get { return _tui_usrseq_no; } set { _tui_usrseq_no = value; } }
        public string Tui_sup { get { return _tui_sup; } set { _tui_sup = value; } }
        public string Tui_batch { get { return _tui_batch; } set { _tui_batch = value; } }
        public string Tui_grn { get { return _tui_grn; } set { _tui_grn = value; } }
        public DateTime Tui_grn_dt { get { return _tui_grn_dt; } set { _tui_grn_dt = value; } }
        public DateTime Tui_exp_dt { get { return _tui_exp_dt; } set { _tui_exp_dt = value; } }
        public int TMP_ROW_NO{ get; set; }
        public decimal TMP_ITM_APP_QTY { get; set; }
        public decimal TMP_ITM_BAL_QTY { get; set; }
        public decimal TMP_ITM_PICK_QTY { get; set; }
        public bool ls_par_save{ get; set; }


        public static ReptPickItems Converter(DataRow row)
        {
            return new ReptPickItems
            {
                Tui_pic_itm_cd = row["TUI_PIC_ITM_CD"] == DBNull.Value ? string.Empty : row["TUI_PIC_ITM_CD"].ToString(),
                Tui_pic_itm_qty = row["TUI_PIC_ITM_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TUI_PIC_ITM_QTY"]),
                Tui_pic_itm_stus = row["TUI_PIC_ITM_STUS"] == null ? string.Empty : Convert.ToString(row["TUI_PIC_ITM_STUS"]),
                Tui_req_itm_cd = row["TUI_REQ_ITM_CD"] == DBNull.Value ? string.Empty : row["TUI_REQ_ITM_CD"].ToString(),
                Tui_req_itm_qty = row["TUI_REQ_ITM_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TUI_REQ_ITM_QTY"]),
                Tui_req_itm_stus = row["TUI_REQ_ITM_STUS"] == DBNull.Value ? string.Empty : row["TUI_REQ_ITM_STUS"].ToString(),
                Tui_usrseq_no = row["TUI_USRSEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUI_USRSEQ_NO"]),
                Tui_sup = row["Tui_sup"] == DBNull.Value ? string.Empty : row["Tui_sup"].ToString(),
                Tui_batch = row["Tui_batch"] == DBNull.Value ? string.Empty : row["Tui_batch"].ToString(),
                Tui_grn = row["Tui_grn"] == DBNull.Value ? string.Empty : row["Tui_grn"].ToString(),
                Tui_grn_dt = row["Tui_grn_dt"] == DBNull.Value ? DateTime.MinValue :  Convert.ToDateTime(row["Tui_grn_dt"]),
                Tui_exp_dt = row["Tui_exp_dt"] == DBNull.Value ? DateTime.MinValue :  Convert.ToDateTime(row["Tui_exp_dt"])

            };
        }
    }
}

