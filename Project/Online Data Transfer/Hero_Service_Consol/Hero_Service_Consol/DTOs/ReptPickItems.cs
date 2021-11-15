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
        #endregion

        public string Tui_pic_itm_cd { get { return _tui_pic_itm_cd; } set { _tui_pic_itm_cd = value; } }
        public decimal Tui_pic_itm_qty { get { return _tui_pic_itm_qty; } set { _tui_pic_itm_qty = value; } }
        public string Tui_pic_itm_stus { get { return _tui_pic_itm_stus; } set { _tui_pic_itm_stus = value; } }
        public string Tui_req_itm_cd { get { return _tui_req_itm_cd; } set { _tui_req_itm_cd = value; } }
        public decimal Tui_req_itm_qty { get { return _tui_req_itm_qty; } set { _tui_req_itm_qty = value; } }
        public string Tui_req_itm_stus { get { return _tui_req_itm_stus; } set { _tui_req_itm_stus = value; } }
        public Int32 Tui_usrseq_no { get { return _tui_usrseq_no; } set { _tui_usrseq_no = value; } }

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
                Tui_usrseq_no = row["TUI_USRSEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUI_USRSEQ_NO"])

            };
        }
    }
}

