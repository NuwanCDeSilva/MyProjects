using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
  public  class InventorySubSerialMaster
    {
        //
        // Function             - Inventory Sub Serial Master
        // Function Wriiten By  - P.Wijetunge
        // Date                 - 12/03/2012
        // Table                - INR_SERMSTSUB
        //

        /// <summary>
        /// Private Data Members
        /// </summary>
        #region Private Members
        private Boolean _irsms_act;
        private string _irsms_itm_cd;
        private string _irsms_itm_stus;
        private string _irsms_mfc;
        private Int32 _irsms_ser_id;
        private Int32 _irsms_ser_line;
        private string _irsms_tp;
        private string _irsms_warr_no;
        private Int32 _irsms_warr_period;
        private string _irsms_warr_rem;
        private string _irsms_sub_ser;
        private decimal _irsms_cost_per;
        private string _irsms_itm_des;
        private string _irsms_itm_sts_chg;
        private string _irsms_loc;
        private string _irsms_loc_chg;
        private string _irsms_costType;
        #endregion

        /// <summary>
        /// Definitions for the private data members
        /// </summary>
        /// 
        #region Definition - Properties - Referance
        public Boolean Irsms_act { get { return _irsms_act; } set { _irsms_act = value; } }
        public string Irsms_itm_cd { get { return _irsms_itm_cd; } set { _irsms_itm_cd = value; } }
        public string Irsms_itm_stus { get { return _irsms_itm_stus; } set { _irsms_itm_stus = value; } }
        public string Irsms_mfc { get { return _irsms_mfc; } set { _irsms_mfc = value; } }
        public Int32 Irsms_ser_id { get { return _irsms_ser_id; } set { _irsms_ser_id = value; } }
        public Int32 Irsms_ser_line { get { return _irsms_ser_line; } set { _irsms_ser_line = value; } }
        public string Irsms_tp { get { return _irsms_tp; } set { _irsms_tp = value; } }
        public string Irsms_warr_no { get { return _irsms_warr_no; } set { _irsms_warr_no = value; } }
        public Int32 Irsms_warr_period { get { return _irsms_warr_period; } set { _irsms_warr_period = value; } }
        public string Irsms_warr_rem { get { return _irsms_warr_rem; } set { _irsms_warr_rem = value; } }
        public string Irsms_sub_ser { get { return _irsms_sub_ser; } set { _irsms_sub_ser = value; } }
        public decimal Irsms_cost_per { get { return _irsms_cost_per; } set { _irsms_cost_per = value; } }
        public string Irsms_itm_des { get { return _irsms_itm_des; } set { _irsms_itm_des = value; } }
        public string Irsms_itm_sts_chg { get { return _irsms_itm_sts_chg; } set { _irsms_itm_sts_chg = value; } }
        public string Irsms_loc { get { return _irsms_loc; } set { _irsms_loc = value; } }
        public string Irsms_loc_chg { get { return _irsms_loc_chg; } set { _irsms_loc_chg = value; } }
        public string Irsms_costType { get { return _irsms_costType; } set { _irsms_costType = value; } }
        public string Irsms_itm_ch_stus { get { return _irsms_costType; } set { _irsms_costType = value; } }

      //Add temp Lakshan 29 Apr 2016 
        public bool _subSerIsAvailable { get; set; }
        public bool SubSerIsAvailable { get { return _subSerIsAvailable; } set { _subSerIsAvailable = value; } }
        #endregion

        /// <summary>
        /// Convert and map to the data table into a list
        /// </summary>
        /// <param name="row">Used to allocate data table row</param>
        /// <returns>Maped Inventory Sub Serial Master</returns>
        #region Converter - Transaction
        public static InventorySubSerialMaster ConvertTotal(DataRow row)
        {
            return new InventorySubSerialMaster
            {
                Irsms_act = row["IRSMS_ACT"] == DBNull.Value ? false : Convert.ToBoolean(Convert.ToInt16(row["IRSMS_ACT"].ToString())),
                Irsms_itm_cd = row["IRSMS_ITM_CD"] == DBNull.Value ? string.Empty : row["IRSMS_ITM_CD"].ToString(),
                Irsms_itm_stus = row["IRSMS_ITM_STUS"] == DBNull.Value ? string.Empty : row["IRSMS_ITM_STUS"].ToString(),
                Irsms_mfc = row["IRSMS_MFC"] == DBNull.Value ? string.Empty : row["IRSMS_MFC"].ToString(),
                Irsms_ser_id = row["IRSMS_SER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRSMS_SER_ID"].ToString()),
                Irsms_ser_line = row["IRSMS_SER_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRSMS_SER_LINE"]),
                Irsms_tp = row["IRSMS_TP"] == DBNull.Value ? string.Empty : row["IRSMS_TP"].ToString(),
                Irsms_warr_no = row["IRSMS_WARR_NO"] == DBNull.Value ? string.Empty : row["IRSMS_WARR_NO"].ToString(),
                Irsms_warr_period = row["IRSMS_WARR_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRSMS_WARR_PERIOD"].ToString()),
                Irsms_warr_rem = row["IRSMS_WARR_REM"] == DBNull.Value ? string.Empty : row["IRSMS_WARR_REM"].ToString(),
                Irsms_sub_ser = row["IRSMS_SUB_SER"] == DBNull.Value ? string.Empty : row["IRSMS_SUB_SER"].ToString()

            };
        }
        public static InventorySubSerialMaster ConvertSelected(DataRow row)
        {
            return new InventorySubSerialMaster
            {
                Irsms_act = row["IRSMS_ACT"] == DBNull.Value ? false : Convert.ToBoolean(Convert.ToInt16(row["IRSMS_ACT"].ToString())),
                Irsms_itm_cd = row["IRSMS_ITM_CD"] == DBNull.Value ? string.Empty : row["IRSMS_ITM_CD"].ToString(),
                Irsms_itm_stus = row["IRSMS_ITM_STUS"] == DBNull.Value ? string.Empty : row["IRSMS_ITM_STUS"].ToString(),
                Irsms_mfc = row["IRSMS_MFC"] == DBNull.Value ? string.Empty : row["IRSMS_MFC"].ToString(),
                Irsms_ser_id = row["IRSMS_SER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRSMS_SER_ID"].ToString()),
                Irsms_ser_line = row["IRSMS_SER_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRSMS_SER_LINE"]),
                Irsms_tp = row["IRSMS_TP"] == DBNull.Value ? string.Empty : row["IRSMS_TP"].ToString(),
                Irsms_warr_no = row["IRSMS_WARR_NO"] == DBNull.Value ? string.Empty : row["IRSMS_WARR_NO"].ToString(),
                Irsms_warr_period = row["IRSMS_WARR_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRSMS_WARR_PERIOD"].ToString()),
                Irsms_warr_rem = row["IRSMS_WARR_REM"] == DBNull.Value ? string.Empty : row["IRSMS_WARR_REM"].ToString(),
                Irsms_sub_ser = row["IRSMS_SUB_SER"] == DBNull.Value ? string.Empty : row["IRSMS_SUB_SER"].ToString(),
                Irsms_cost_per = row["costPer"] == DBNull.Value ? 0 : Convert.ToDecimal(row["costPer"].ToString()),
                Irsms_itm_des = row["ItemDes"] == DBNull.Value ? string.Empty : row["ItemDes"].ToString(),
                Irsms_itm_sts_chg = row["IRSMS_ITM_STUS"] == DBNull.Value ? string.Empty : row["IRSMS_ITM_STUS"].ToString(),
                Irsms_loc = row["irsm_loc"] == DBNull.Value ? string.Empty : row["irsm_loc"].ToString(),
                Irsms_loc_chg = row["ChangeLoc"] == DBNull.Value ? string.Empty : row["ChangeLoc"].ToString(),
                Irsms_costType = row["COST_TYPE"] == DBNull.Value ? string.Empty : row["COST_TYPE"].ToString()

            };
        }
        public static InventorySubSerialMaster ConvertNew(DataRow row)
        {
            return new InventorySubSerialMaster
            {
                Irsms_act = row["IRSMS_ACT"] == DBNull.Value ? false : Convert.ToBoolean(Convert.ToInt16(row["IRSMS_ACT"].ToString())),
                Irsms_itm_cd = row["IRSMS_ITM_CD"] == DBNull.Value ? string.Empty : row["IRSMS_ITM_CD"].ToString(),
                Irsms_itm_stus = row["IRSMS_ITM_STUS"] == DBNull.Value ? string.Empty : row["IRSMS_ITM_STUS"].ToString(),
                Irsms_mfc = row["IRSMS_MFC"] == DBNull.Value ? string.Empty : row["IRSMS_MFC"].ToString(),
                Irsms_ser_id = row["IRSMS_SER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRSMS_SER_ID"].ToString()),
                Irsms_ser_line = row["IRSMS_SER_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRSMS_SER_LINE"]),
                Irsms_tp = row["IRSMS_TP"] == DBNull.Value ? string.Empty : row["IRSMS_TP"].ToString(),
                Irsms_warr_no = row["IRSMS_WARR_NO"] == DBNull.Value ? string.Empty : row["IRSMS_WARR_NO"].ToString(),
                Irsms_warr_period = row["IRSMS_WARR_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRSMS_WARR_PERIOD"].ToString()),
                Irsms_warr_rem = row["IRSMS_WARR_REM"] == DBNull.Value ? string.Empty : row["IRSMS_WARR_REM"].ToString(),
                Irsms_sub_ser = row["IRSMS_SUB_SER"] == DBNull.Value ? string.Empty : row["IRSMS_SUB_SER"].ToString(),
                Irsms_cost_per = row["costPer"] == DBNull.Value ? 0 : Convert.ToDecimal(row["costPer"].ToString()),
                Irsms_itm_des = row["ItemDes"] == DBNull.Value ? string.Empty : row["ItemDes"].ToString(),
                Irsms_itm_sts_chg = row["IRSMS_ITM_STUS"] == DBNull.Value ? string.Empty : row["IRSMS_ITM_STUS"].ToString(),
                Irsms_loc = row["irsm_loc"] == DBNull.Value ? string.Empty : row["irsm_loc"].ToString(),
                Irsms_loc_chg = row["ChangeLoc"] == DBNull.Value ? string.Empty : row["ChangeLoc"].ToString(),
                Irsms_costType = row["COST_TYPE"] == DBNull.Value ? string.Empty : row["COST_TYPE"].ToString()

            };
        }
        #endregion
    }
}

