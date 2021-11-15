using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class ReceiptItemDetails
    {
        #region Private Members
        private string _sari_cre_by;
        private DateTime _sari_cre_date;
        private string _sari_item;
        private string _sari_item_desc;
        private Int32 _sari_line;
        private string _sari_model;
        private string _sari_rec_no;
        private Int32 _sari_seq_no;
        private string _sari_serial;
        private string _sari_serial_1;
        #endregion

        public string Sari_cre_by
        {
            get { return _sari_cre_by; }
            set { _sari_cre_by = value; }
        }
        public DateTime Sari_cre_date
        {
            get { return _sari_cre_date; }
            set { _sari_cre_date = value; }
        }
        public string Sari_item
        {
            get { return _sari_item; }
            set { _sari_item = value; }
        }
        public string Sari_item_desc
        {
            get { return _sari_item_desc; }
            set { _sari_item_desc = value; }
        }
        public Int32 Sari_line
        {
            get { return _sari_line; }
            set { _sari_line = value; }
        }
        public string Sari_model
        {
            get { return _sari_model; }
            set { _sari_model = value; }
        }
        public string Sari_rec_no
        {
            get { return _sari_rec_no; }
            set { _sari_rec_no = value; }
        }
        public Int32 Sari_seq_no
        {
            get { return _sari_seq_no; }
            set { _sari_seq_no = value; }
        }
        public string Sari_serial
        {
            get { return _sari_serial; }
            set { _sari_serial = value; }
        }
        public string Sari_serial_1
        {
            get { return _sari_serial_1; }
            set { _sari_serial_1 = value; }
        }

        //New fields 2015-08-06
        public Decimal Sari_qty { get; set; }
        public String Sari_pb { get; set; }
        public String Sari_pb_lvl { get; set; }
        public Decimal Sari_unit_rate { get; set; }
        public Decimal Sari_tax_amt { get; set; }
        public Decimal Sari_unit_amt { get; set; }
        public String Sari_sts { get; set; }
        public string Sari_promo { get; set; } // Tharaka 2015-10-31
        public Decimal sari_res_qty { get; set; } // tharanga 2017/11/07
        public Int32 sari_is_res { get; set; } // tharanga 2017/11/07

        public static ReceiptItemDetails Converter(DataRow row)
        {
            return new ReceiptItemDetails
            {
                Sari_cre_by = row["SARI_CRE_BY"] == DBNull.Value ? string.Empty : row["SARI_CRE_BY"].ToString(),
                Sari_cre_date = row["SARI_CRE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SARI_CRE_DATE"]),
                Sari_item = row["SARI_ITEM"] == DBNull.Value ? string.Empty : row["SARI_ITEM"].ToString(),
                Sari_item_desc = row["SARI_ITEM_DESC"] == DBNull.Value ? string.Empty : row["SARI_ITEM_DESC"].ToString(),
                Sari_line = row["SARI_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SARI_LINE"]),
                Sari_model = row["SARI_MODEL"] == DBNull.Value ? string.Empty : row["SARI_MODEL"].ToString(),
                Sari_rec_no = row["SARI_REC_NO"] == DBNull.Value ? string.Empty : row["SARI_REC_NO"].ToString(),
                Sari_seq_no = row["SARI_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SARI_SEQ_NO"]),
                Sari_serial = row["SARI_SERIAL"] == DBNull.Value ? string.Empty : row["SARI_SERIAL"].ToString(),
                Sari_serial_1 = row["SARI_SERIAL_1"] == DBNull.Value ? string.Empty : row["SARI_SERIAL_1"].ToString()

            };
        }

        public static ReceiptItemDetails ConverterNew(DataRow row)
        {
            return new ReceiptItemDetails
            {
                Sari_seq_no = row["SARI_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SARI_SEQ_NO"].ToString()),
                Sari_line = row["SARI_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SARI_LINE"].ToString()),
                Sari_rec_no = row["SARI_REC_NO"] == DBNull.Value ? string.Empty : row["SARI_REC_NO"].ToString(),
                Sari_item = row["SARI_ITEM"] == DBNull.Value ? string.Empty : row["SARI_ITEM"].ToString(),
                Sari_item_desc = row["SARI_ITEM_DESC"] == DBNull.Value ? string.Empty : row["SARI_ITEM_DESC"].ToString(),
                Sari_model = row["SARI_MODEL"] == DBNull.Value ? string.Empty : row["SARI_MODEL"].ToString(),
                Sari_serial = row["SARI_SERIAL"] == DBNull.Value ? string.Empty : row["SARI_SERIAL"].ToString(),
                Sari_serial_1 = row["SARI_SERIAL_1"] == DBNull.Value ? string.Empty : row["SARI_SERIAL_1"].ToString(),
                Sari_cre_by = row["SARI_CRE_BY"] == DBNull.Value ? string.Empty : row["SARI_CRE_BY"].ToString(),
                Sari_cre_date = row["SARI_CRE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SARI_CRE_DATE"].ToString()),
                Sari_qty = row["SARI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SARI_QTY"].ToString()),
                Sari_pb = row["SARI_PB"] == DBNull.Value ? string.Empty : row["SARI_PB"].ToString(),
                Sari_pb_lvl = row["SARI_PB_LVL"] == DBNull.Value ? string.Empty : row["SARI_PB_LVL"].ToString(),
                Sari_unit_rate = row["SARI_UNIT_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SARI_UNIT_RATE"].ToString()),
                Sari_tax_amt = row["SARI_TAX_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SARI_TAX_AMT"].ToString()),
                Sari_unit_amt = row["SARI_UNIT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SARI_UNIT_AMT"].ToString()),
                Sari_sts = row["Sari_sts"] == DBNull.Value ? string.Empty : row["Sari_sts"].ToString(),
                Sari_promo = row["Sari_promo"] == DBNull.Value ? string.Empty : row["Sari_promo"].ToString(),
                sari_res_qty = row["sari_res_qty"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sari_res_qty"].ToString()),
                sari_is_res = row["sari_is_res"] == DBNull.Value ? 0 : Convert.ToInt32(row["sari_is_res"].ToString())
            };
        }
        public static ReceiptItemDetails ConverterNewT(DataRow row)
        {
            return new ReceiptItemDetails
            {
                Sari_seq_no = row["SARI_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SARI_SEQ_NO"].ToString()),
                Sari_line = row["SARI_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SARI_LINE"].ToString()),
                Sari_rec_no = row["SARI_REC_NO"] == DBNull.Value ? string.Empty : row["SARI_REC_NO"].ToString(),
                Sari_item = row["SARI_ITEM"] == DBNull.Value ? string.Empty : row["SARI_ITEM"].ToString(),
                Sari_item_desc = row["SARI_ITEM_DESC"] == DBNull.Value ? string.Empty : row["SARI_ITEM_DESC"].ToString(),
                Sari_model = row["SARI_MODEL"] == DBNull.Value ? string.Empty : row["SARI_MODEL"].ToString(),
                Sari_serial = row["SARI_SERIAL"] == DBNull.Value ? string.Empty : row["SARI_SERIAL"].ToString(),
                Sari_serial_1 = row["SARI_SERIAL_1"] == DBNull.Value ? string.Empty : row["SARI_SERIAL_1"].ToString(),
                Sari_cre_by = row["SARI_CRE_BY"] == DBNull.Value ? string.Empty : row["SARI_CRE_BY"].ToString(),
                Sari_cre_date = row["SARI_CRE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SARI_CRE_DATE"].ToString()),
                Sari_qty = row["SARI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SARI_QTY"].ToString()),
                Sari_pb = row["SARI_PB"] == DBNull.Value ? string.Empty : row["SARI_PB"].ToString(),
                Sari_pb_lvl = row["SARI_PB_LVL"] == DBNull.Value ? string.Empty : row["SARI_PB_LVL"].ToString(),
                Sari_unit_rate = row["SARI_UNIT_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SARI_UNIT_RATE"].ToString()),
                Sari_tax_amt = row["SARI_TAX_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SARI_TAX_AMT"].ToString()),
                Sari_unit_amt = row["SARI_UNIT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SARI_UNIT_AMT"].ToString()),
                Sari_sts = row["Sari_sts"] == DBNull.Value ? string.Empty : row["Sari_sts"].ToString(),
                Sari_promo = row["Sari_promo"] == DBNull.Value ? string.Empty : row["Sari_promo"].ToString(),
                sari_res_qty = row["sari_res_qty"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sari_res_qty"].ToString()),
                sari_is_res = row["sari_is_res"] == DBNull.Value ? 0 : Convert.ToInt32(row["sari_is_res"].ToString())
                
            };
        }

    }
}
