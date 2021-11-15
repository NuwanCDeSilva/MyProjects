using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class RCC_Det
    {
        #region Private Members
        private string _inrd_acc_no;
        private DateTime _inrd_inv_dt;
        private string _inrd_inv_no;
        private string _inrd_itm;
        private int _inrd_line;
        private string _inrd_no;
        private DateTime _inrd_oth_doc_dt;
        private string _inrd_oth_doc_no;
        private Int32 _inrd_qty;
        private string _inrd_ser;
        private string _inrd_warr;
        #endregion

        #region Public Property Definition
        public string Inrd_acc_no
        {
            get { return _inrd_acc_no; }
            set { _inrd_acc_no = value; }
        }
        public DateTime Inrd_inv_dt
        {
            get { return _inrd_inv_dt; }
            set { _inrd_inv_dt = value; }
        }
        public string Inrd_inv_no
        {
            get { return _inrd_inv_no; }
            set { _inrd_inv_no = value; }
        }
        public string Inrd_itm
        {
            get { return _inrd_itm; }
            set { _inrd_itm = value; }
        }
        public int Inrd_line
        {
            get { return _inrd_line; }
            set { _inrd_line = value; }
        }
        public string Inrd_no
        {
            get { return _inrd_no; }
            set { _inrd_no = value; }
        }
        public DateTime Inrd_oth_doc_dt
        {
            get { return _inrd_oth_doc_dt; }
            set { _inrd_oth_doc_dt = value; }
        }
        public string Inrd_oth_doc_no
        {
            get { return _inrd_oth_doc_no; }
            set { _inrd_oth_doc_no = value; }
        }
        public Int32 Inrd_qty
        {
            get { return _inrd_qty; }
            set { _inrd_qty = value; }
        }
        public string Inrd_ser
        {
            get { return _inrd_ser; }
            set { _inrd_ser = value; }
        }
        public string Inrd_warr
        {
            get { return _inrd_warr; }
            set { _inrd_warr = value; }
        }

        #endregion

        #region Converters
        public static RCC_Det Converter(DataRow row)
        {
            return new RCC_Det
            {

                Inrd_acc_no = row["INRD_ACC_NO"] == DBNull.Value ? string.Empty : row["INRD_ACC_NO"].ToString(),
                Inrd_inv_dt = row["INRD_INV_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INRD_INV_DT"]),
                Inrd_inv_no = row["INRD_INV_NO"] == DBNull.Value ? string.Empty : row["INRD_INV_NO"].ToString(),
                Inrd_itm = row["INRD_ITM"] == DBNull.Value ? string.Empty : row["INRD_ITM"].ToString(),
                Inrd_line = row["INRD_LINE"] == DBNull.Value ? 0 : Convert.ToInt16(row["INRD_LINE"]),
                Inrd_no = row["INRD_NO"] == DBNull.Value ? string.Empty : row["INRD_NO"].ToString(),
                Inrd_oth_doc_dt = row["INRD_OTH_DOC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INRD_OTH_DOC_DT"]),
                Inrd_oth_doc_no = row["INRD_OTH_DOC_NO"] == DBNull.Value ? string.Empty : row["INRD_OTH_DOC_NO"].ToString(),
                Inrd_qty = row["INRD_QTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["INRD_QTY"]),
                Inrd_ser = row["INRD_SER"] == DBNull.Value ? string.Empty : row["INRD_SER"].ToString(),
                Inrd_warr = row["INRD_WARR"] == DBNull.Value ? string.Empty : row["INRD_WARR"].ToString()

            };
        }

        #endregion
    }
}

