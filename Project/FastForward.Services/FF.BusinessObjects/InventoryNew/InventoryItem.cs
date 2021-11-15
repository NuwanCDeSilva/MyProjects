using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
  public  class InventoryItem
    {
        //
        // Function             - Inventory Item
        // Function Wriiten By  - P.Wijetunge
        // Date                 - 12/03/2012
        // Table                - INT_ITM
        //

        /// <summary>
        /// Private Data Members
        /// </summary>
        #region Private Members
        private decimal _iti_bal_qty;
        private string _iti_bin_code;
        private string _iti_doc_no;
        private string _iti_item_code;
        private Int32 _iti_item_line;
        private string _iti_item_status;
        private decimal _iti_qty;
        private Int32 _iti_seq_no;
        private Int32 _iti_year;
        #endregion

        /// <summary>
        /// Definitions for the private data members
        /// </summary>
        /// 
        #region Definition - Properties
        public decimal Iti_bal_qty { get { return _iti_bal_qty; } set { _iti_bal_qty = value; } }
        public string Iti_bin_code { get { return _iti_bin_code; } set { _iti_bin_code = value; } }
        public string Iti_doc_no { get { return _iti_doc_no; } set { _iti_doc_no = value; } }
        public string Iti_item_code { get { return _iti_item_code; } set { _iti_item_code = value; } }
        public Int32 Iti_item_line { get { return _iti_item_line; } set { _iti_item_line = value; } }
        public string Iti_item_status { get { return _iti_item_status; } set { _iti_item_status = value; } }
        public decimal Iti_qty { get { return _iti_qty; } set { _iti_qty = value; } }
        public Int32 Iti_seq_no { get { return _iti_seq_no; } set { _iti_seq_no = value; } }
        public Int32 Iti_year { get { return _iti_year; } set { _iti_year = value; } }
        public string Iti_pkg_uom_tp { get; set; }
        public decimal Iti_pkg_uom_qty { get; set; }
        #endregion

        /// <summary>
        /// Convert and map to the data table into a list
        /// </summary>
        /// <param name="row">Used to allocate data table row</param>
        /// <returns>Maped Inventory Item</returns>
        #region Converter
        public static InventoryItem ConvertTotal(DataRow row)
        {
            return new InventoryItem
            {
                Iti_seq_no  = row["ITI_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITI_SEQ_NO"].ToString()), 
                Iti_item_line  = row["ITI_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITI_ITM_LINE"].ToString()), 
                Iti_doc_no  = row["ITI_DOC_NO"] == DBNull.Value ? string.Empty : row["ITI_DOC_NO"].ToString(), 
                Iti_bin_code  = row["ITI_BIN_CD"] == DBNull.Value ? string.Empty : row["ITI_BIN_CD"].ToString(), 
                Iti_item_code  = row["ITI_ITM_CD"] == DBNull.Value ? string.Empty : row["ITI_ITM_CD"].ToString(), 
                Iti_item_status  = row["ITI_ITM_STUS"] == DBNull.Value ? string.Empty : row["ITI_ITM_STUS"].ToString(), 
                Iti_qty  = row["ITI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITI_QTY"].ToString()), 
                Iti_bal_qty  = row["ITI_BAL_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITI_BAL_QTY"].ToString()), 
                Iti_year  = row["ITI_YEAR"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITI_YEAR"].ToString())}; 
        }
        public static InventoryItem Convert2(DataRow row)
        {
            return new InventoryItem
            {
                Iti_seq_no = row["ITI_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITI_SEQ_NO"].ToString()),
                Iti_item_line = row["ITI_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITI_ITM_LINE"].ToString()),
                Iti_doc_no = row["ITI_DOC_NO"] == DBNull.Value ? string.Empty : row["ITI_DOC_NO"].ToString(),
                Iti_bin_code = row["ITI_BIN_CD"] == DBNull.Value ? string.Empty : row["ITI_BIN_CD"].ToString(),
                Iti_item_code = row["ITI_ITM_CD"] == DBNull.Value ? string.Empty : row["ITI_ITM_CD"].ToString(),
                Iti_item_status = row["ITI_ITM_STUS"] == DBNull.Value ? string.Empty : row["ITI_ITM_STUS"].ToString(),
                Iti_qty = row["ITI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITI_QTY"].ToString()),
                Iti_bal_qty = row["ITI_BAL_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITI_BAL_QTY"].ToString()),
                Iti_year = row["ITI_YEAR"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITI_YEAR"].ToString()),
                Iti_pkg_uom_tp = row["Iti_uom_tp"] == DBNull.Value ? string.Empty : row["Iti_uom_tp"].ToString(),
                Iti_pkg_uom_qty = row["Iti_uom_qty"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Iti_uom_qty"].ToString())
            };
        }
        #endregion
    }
}

