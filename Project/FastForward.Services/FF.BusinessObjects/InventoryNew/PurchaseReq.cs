using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
    [Serializable]
    public class PurchaseReq
    {
        #region Private Members
        private Boolean _por_act;
        private string _por_cre_by;
        private DateTime _por_cre_dt;
        private string _por_itm_cd;
        private string _por_itm_stus;
        private decimal _por_qty;
        private Int32 _por_req_line;
        private string _por_req_no;
        private Int32 _por_seq_no;
        #endregion

        public Boolean Por_act
        {
            get { return _por_act; }
            set { _por_act = value; }
        }
        public string Por_cre_by
        {
            get { return _por_cre_by; }
            set { _por_cre_by = value; }
        }
        public DateTime Por_cre_dt
        {
            get { return _por_cre_dt; }
            set { _por_cre_dt = value; }
        }
        public string Por_itm_cd
        {
            get { return _por_itm_cd; }
            set { _por_itm_cd = value; }
        }
        public string Por_itm_stus
        {
            get { return _por_itm_stus; }
            set { _por_itm_stus = value; }
        }
        public decimal Por_qty
        {
            get { return _por_qty; }
            set { _por_qty = value; }
        }
        public Int32 Por_req_line
        {
            get { return _por_req_line; }
            set { _por_req_line = value; }
        }
        public string Por_req_no
        {
            get { return _por_req_no; }
            set { _por_req_no = value; }
        }
        public Int32 Por_seq_no
        {
            get { return _por_seq_no; }
            set { _por_seq_no = value; }
        }

        public static PurchaseReq Converter(DataRow row)
        {
            return new PurchaseReq
            {
                Por_act = row["POR_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["POR_ACT"]),
                Por_cre_by = row["POR_CRE_BY"] == DBNull.Value ? string.Empty : row["POR_CRE_BY"].ToString(),
                Por_cre_dt = row["POR_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["POR_CRE_DT"]),
                Por_itm_cd = row["POR_ITM_CD"] == DBNull.Value ? string.Empty : row["POR_ITM_CD"].ToString(),
                Por_itm_stus = row["POR_ITM_STUS"] == DBNull.Value ? string.Empty : row["POR_ITM_STUS"].ToString(),
                Por_qty = row["POR_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["POR_QTY"]),
                Por_req_line = row["POR_REQ_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["POR_REQ_LINE"]),
                Por_req_no = row["POR_REQ_NO"] == DBNull.Value ? string.Empty : row["POR_REQ_NO"].ToString(),
                Por_seq_no = row["POR_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["POR_SEQ_NO"])

            };
        }


    }
}
