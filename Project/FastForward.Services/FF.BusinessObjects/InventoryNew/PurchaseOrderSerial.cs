using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace FF.BusinessObjects
{
    /// <summary>
    /// Description : Business Object class for PurchaseOrder Serials.
    /// Created By : Wimal
    /// Created On : 25/Jan/2019
    /// </summary>
    [Serializable]
    public class PurchaseOrderSerial
    {
        #region Private Members
        private decimal _podi_bal_qty;
        private Int32 _podi_del_line_no;
        private string _podi_itm_cd;
        private string _podi_itm_stus;
        private Int32 _podi_line_no;
        private string _podi_loca;
        private decimal _podi_qty;
        private string _podi_remarks;
        private Int32 _podi_seq_no;

        //UI specific propertise.
        private MasterItem _masterItem;
        private PurchaseOrderDetail _purchaseOrderDetail;
        private decimal _actual_qty;
        private DateTime _podi_ex_dt;
        private Int32 _Podi_warr_period;
        private string _Podi_warr_remrk;
        #endregion

        #region Public Property Definition
        public decimal Podi_bal_qty
        {
            get { return _podi_bal_qty; }
            set { _podi_bal_qty = value; }
        }
        public Int32 Podi_del_line_no
        {
            get { return _podi_del_line_no; }
            set { _podi_del_line_no = value; }
        }
        public string Podi_itm_cd
        {
            get { return _podi_itm_cd; }
            set { _podi_itm_cd = value; }
        }
        public string Podi_itm_stus
        {
            get { return _podi_itm_stus; }
            set { _podi_itm_stus = value; }
        }
        public Int32 Podi_line_no
        {
            get { return _podi_line_no; }
            set { _podi_line_no = value; }
        }
        public string Podi_loca
        {
            get { return _podi_loca; }
            set { _podi_loca = value; }
        }
        public decimal Podi_qty
        {
            get { return _podi_qty; }
            set { _podi_qty = value; }
        }
        public string Podi_remarks
        {
            get { return _podi_remarks; }
            set { _podi_remarks = value; }
        }
        public Int32 Podi_seq_no
        {
            get { return _podi_seq_no; }
            set { _podi_seq_no = value; }
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
        public DateTime Podi_ex_dt
        {
            get { return _podi_ex_dt; }
            set { _podi_ex_dt = value; }
        }
        public PurchaseOrderDetail PurchaseOrderDetail
        {
            get { return _purchaseOrderDetail; }
            set { _purchaseOrderDetail = value; }
        }
        public Int32 Podi_warr_period
        {
            get { return _Podi_warr_period; }
            set { _Podi_warr_period = value; }
        }
        public string Podi_warr_remrk
        {
            get { return _Podi_warr_remrk; }
            set { _Podi_warr_remrk = value; }
        }
        #endregion

        public static PurchaseOrderDelivery Converter(DataRow row)
        {
            return new PurchaseOrderDelivery
            {
                Podi_bal_qty = row["PODI_BAL_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["PODI_BAL_QTY"]),
                Podi_del_line_no = row["PODI_DEL_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["PODI_DEL_LINE_NO"]),
                Podi_itm_cd = row["PODI_ITM_CD"] == DBNull.Value ? string.Empty : row["PODI_ITM_CD"].ToString(),
                Podi_itm_stus = row["PODI_ITM_STUS"] == DBNull.Value ? string.Empty : row["PODI_ITM_STUS"].ToString(),
                Podi_line_no = row["PODI_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["PODI_LINE_NO"]),
                Podi_loca = row["PODI_LOCA"] == DBNull.Value ? string.Empty : row["PODI_LOCA"].ToString(),
                Podi_qty = row["PODI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["PODI_QTY"]),
                Podi_remarks = row["PODI_REMARKS"] == DBNull.Value ? string.Empty : row["PODI_REMARKS"].ToString(),
                Podi_seq_no = row["PODI_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["PODI_SEQ_NO"]),
                Podi_ex_dt = row["podi_exp_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["podi_exp_dt"].ToString())
            };
        }
        public static PurchaseOrderDelivery ConverterPOGRN(DataRow row)
        {
            return new PurchaseOrderDelivery
            {
                Podi_bal_qty = row["PODI_BAL_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["PODI_BAL_QTY"]),
                Podi_del_line_no = row["PODI_DEL_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["PODI_DEL_LINE_NO"]),
            };
        }
    }
}
