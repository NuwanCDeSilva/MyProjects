using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    /// <summary>
    /// Description : Business Object class for Inventory Request Item.
    /// Created By : Miginda Geeganage.
    /// Created On : 24/03/2012
    /// </summary>
    [Serializable]
    public class InventoryRequestItem:MasterItem
    {

        #region Private Members
        private decimal _itri_app_qty = 0;
        private string _itri_itm_cd = string.Empty;
        private string _itri_itm_stus = string.Empty;
        private Int32 _itri_line_no = 0;
        private string _itri_note = string.Empty;
        private decimal _itri_qty = 0;
        private string _itri_res_no = string.Empty;
        private Int32 _itri_seq_no = 0;
        private decimal _itri_unit_price = 0;

        private string _itri_mitm_cd = string.Empty; //ITRI_MITM_CD 
        private string _itri_mitm_stus = string.Empty; //ITRI_MITM_STUS 
        private decimal _itri_mqty = 0;                //ITRI_MQTY 

        private MasterItem _masterItem = null;

        #endregion

        #region Public Property Definition
        public decimal Itri_app_qty
        {
            get { return _itri_app_qty; }
            set { _itri_app_qty = value; }
        }
        public string Itri_itm_cd
        {
            get { return _itri_itm_cd; }
            set { _itri_itm_cd = value; }
        }
        public string Itri_itm_stus
        {
            get { return _itri_itm_stus; }
            set { _itri_itm_stus = value; }
        }
        public Int32 Itri_line_no
        {
            get { return _itri_line_no; }
            set { _itri_line_no = value; }
        }
        public string Itri_note
        {
            get { return _itri_note; }
            set { _itri_note = value; }
        }
        public decimal Itri_qty
        {
            get { return _itri_qty; }
            set { _itri_qty = value; }
        }
        public string Itri_res_no
        {
            get { return _itri_res_no; }
            set { _itri_res_no = value; }
        }
        public Int32 Itri_seq_no
        {
            get { return _itri_seq_no; }
            set { _itri_seq_no = value; }
        }
        public decimal Itri_unit_price
        {
            get { return _itri_unit_price; }
            set { _itri_unit_price = value; }
        }

        public MasterItem MasterItem
        {
            get { return _masterItem; }
            set { _masterItem = value; }
        }

        public string Itri_mitm_cd
        {
            get { return _itri_mitm_cd; }
            set { _itri_mitm_cd = value; }
        }

        public string Itri_mitm_stus
        {
            get { return _itri_mitm_stus; }
            set { _itri_mitm_stus = value; }
        }

        public decimal Itri_mqty
        {
            get { return _itri_mqty; }
            set { _itri_mqty = value; }
        }

        #endregion

        public static InventoryRequestItem Converter(DataRow row)
        {
            return new InventoryRequestItem
            {
                Itri_app_qty = row["ITRI_APP_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_APP_QTY"]),
                Itri_itm_cd = row["ITRI_ITM_CD"] == DBNull.Value ? string.Empty : row["ITRI_ITM_CD"].ToString(),
                Itri_itm_stus = row["ITRI_ITM_STUS"] == DBNull.Value ? string.Empty : row["ITRI_ITM_STUS"].ToString(),
                Itri_line_no = row["ITRI_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRI_LINE_NO"]),
                Itri_note = row["ITRI_NOTE"] == DBNull.Value ? string.Empty : row["ITRI_NOTE"].ToString(),
                Itri_qty = row["ITRI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_QTY"]),
                Itri_res_no = row["ITRI_RES_NO"] == DBNull.Value ? string.Empty : row["ITRI_RES_NO"].ToString(),
                Itri_seq_no = row["ITRI_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRI_SEQ_NO"]),
                Itri_unit_price = row["ITRI_UNIT_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_UNIT_PRICE"]),

                Itri_mitm_cd = row["ITRI_MITM_CD"] == DBNull.Value ? string.Empty : row["ITRI_MITM_CD"].ToString(),
                Itri_mitm_stus = row["ITRI_MITM_STUS"] == DBNull.Value ? string.Empty : row["ITRI_MITM_STUS"].ToString(),
                Itri_mqty = row["ITRI_MQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_MQTY"])

            };

        }

        public static InventoryRequestItem ConverterForCommonOut(DataRow row)
        {
            return new InventoryRequestItem
            {

                Itri_line_no = row["ITRI_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRI_LINE_NO"]),
                Itri_itm_cd = row["ITRI_ITM_CD"] == DBNull.Value ? string.Empty : row["ITRI_ITM_CD"].ToString(),
                Itri_itm_stus = row["ITRI_ITM_STUS"] == DBNull.Value ? string.Empty : row["ITRI_ITM_STUS"].ToString(),
                Itri_qty = row["ITRI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_QTY"]),
                Itri_app_qty = row["ITRI_APP_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITRI_APP_QTY"]),

                Mi_longdesc = row["Mi_longdesc"] == DBNull.Value ? string.Empty : Convert.ToString(row["MI_LONGDESC"]),
                Mi_brand = row["MI_BRAND"] == DBNull.Value ? string.Empty : Convert.ToString(row["MI_BRAND"]),
                Mi_model = row["MI_MODEL"] == DBNull.Value ? string.Empty : Convert.ToString(row["MI_MODEL"])

            };

        }
    }
}
