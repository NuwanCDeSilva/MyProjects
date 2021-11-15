using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace FF.BusinessObjects
{

    [Serializable]
    public class PurchaseOrderAlloc
    {
        #region Private Members
        private decimal _poal_bal_qty;
        private Int32 _poal_del_line_no;
        private string _poal_itm_cd;
        private string _poal_itm_stus;
        private Int32 _poal_line_no;
        private string _poal_loca;
        private decimal _poal_qty;
        private string _poal_remarks;
        private Int32 _poal_seq_no;

        //UI specific propertise.
        private MasterItem _masterItem;
        private PurchaseOrderDetail _purchaseOrderDetail;
        private decimal _actual_qty;

        #endregion

        #region Public Property Definition
        public decimal poal_bal_qty
        {
            get { return _poal_bal_qty; }
            set { _poal_bal_qty = value; }
        }
        public Int32 poal_del_line_no
        {
            get { return _poal_del_line_no; }
            set { _poal_del_line_no = value; }
        }
        public string poal_itm_cd
        {
            get { return _poal_itm_cd; }
            set { _poal_itm_cd = value; }
        }
        public string poal_itm_stus
        {
            get { return _poal_itm_stus; }
            set { _poal_itm_stus = value; }
        }
        public Int32 poal_line_no
        {
            get { return _poal_line_no; }
            set { _poal_line_no = value; }
        }
        public string poal_loca
        {
            get { return _poal_loca; }
            set { _poal_loca = value; }
        }
        public decimal poal_qty
        {
            get { return _poal_qty; }
            set { _poal_qty = value; }
        }
        public string poal_remarks
        {
            get { return _poal_remarks; }
            set { _poal_remarks = value; }
        }
        public Int32 poal_seq_no
        {
            get { return _poal_seq_no; }
            set { _poal_seq_no = value; }
        }

        public MasterItem MasterItem
        {
            get { return _masterItem; }
            set { _masterItem = value; }
        }

        public decimal Actual_qty
        {
            get { return _actual_qty; }
            set { _actual_qty = value; }
        }

        public PurchaseOrderDetail PurchaseOrderDetail
        {
            get { return _purchaseOrderDetail; }
            set { _purchaseOrderDetail = value; }
        }

        #endregion

        public static PurchaseOrderAlloc Converter(DataRow row)
        {
            return new PurchaseOrderAlloc
            {
                poal_bal_qty = row["poal_BAL_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["poal_BAL_QTY"]),
                poal_del_line_no = row["poal_DEL_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["poal_DEL_LINE_NO"]),
                poal_itm_cd = row["poal_ITM_CD"] == DBNull.Value ? string.Empty : row["poal_ITM_CD"].ToString(),
                poal_itm_stus = row["poal_ITM_STUS"] == DBNull.Value ? string.Empty : row["poal_ITM_STUS"].ToString(),
                poal_line_no = row["poal_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["poal_LINE_NO"]),
                poal_loca = row["poal_LOCA"] == DBNull.Value ? string.Empty : row["poal_LOCA"].ToString(),
                poal_qty = row["poal_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["poal_QTY"]),
                poal_remarks = row["poal_REMARKS"] == DBNull.Value ? string.Empty : row["poal_REMARKS"].ToString(),
                poal_seq_no = row["poal_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["poal_SEQ_NO"])

            };
        }
    }
}
