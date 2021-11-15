using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace FF.BusinessObjects
{
    // Written By P.Wijetunge on 05/04/2012

    /// <summary>
    /// Warranty Sub Detail
    /// </summary>
    public class InventoryWarrantySubDetail
    {
        #region Private Members
        private Boolean _irsms_act;
        private string _irsms_itm_cd;
        private string _irsms_itm_stus;
        private string _irsms_mfc;
        private Int32 _irsms_ser_id;
        private Int32 _irsms_ser_line;
        private string _irsms_sub_ser;
        private string _irsms_tp;
        private string _irsms_warr_no;
        private Int32 _irsms_warr_period;
        private string _irsms_warr_rem;
        #endregion

        public Boolean Irsms_act { get { return _irsms_act; } set { _irsms_act = value; } }
        public string Irsms_itm_cd { get { return _irsms_itm_cd; } set { _irsms_itm_cd = value; } }
        public string Irsms_itm_stus { get { return _irsms_itm_stus; } set { _irsms_itm_stus = value; } }
        public string Irsms_mfc { get { return _irsms_mfc; } set { _irsms_mfc = value; } }
        public Int32 Irsms_ser_id { get { return _irsms_ser_id; } set { _irsms_ser_id = value; } }
        public Int32 Irsms_ser_line { get { return _irsms_ser_line; } set { _irsms_ser_line = value; } }
        public string Irsms_sub_ser { get { return _irsms_sub_ser; } set { _irsms_sub_ser = value; } }
        public string Irsms_tp { get { return _irsms_tp; } set { _irsms_tp = value; } }
        public string Irsms_warr_no { get { return _irsms_warr_no; } set { _irsms_warr_no = value; } }
        public Int32 Irsms_warr_period { get { return _irsms_warr_period; } set { _irsms_warr_period = value; } }
        public string Irsms_warr_rem { get { return _irsms_warr_rem; } set { _irsms_warr_rem = value; } }

        public static InventoryWarrantySubDetail ConvertTotal(DataRow row)
        {
            return new InventoryWarrantySubDetail
            {
                Irsms_act = row["IRSMS_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["IRSMS_ACT"]),
                Irsms_itm_cd = row["IRSMS_ITM_CD"] == DBNull.Value ? string.Empty : row["IRSMS_ITM_CD"].ToString(),
                Irsms_itm_stus = row["IRSMS_ITM_STUS"] == DBNull.Value ? string.Empty : row["IRSMS_ITM_STUS"].ToString(),
                Irsms_mfc = row["IRSMS_MFC"] == DBNull.Value ? string.Empty : row["IRSMS_MFC"].ToString(),
                Irsms_ser_id = row["IRSMS_SER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRSMS_SER_ID"]),
                Irsms_ser_line = row["IRSMS_SER_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRSMS_SER_LINE"]),
                Irsms_sub_ser = row["IRSMS_SUB_SER"] == DBNull.Value ? string.Empty : row["IRSMS_SUB_SER"].ToString(),
                Irsms_tp = row["IRSMS_TP"] == DBNull.Value ? string.Empty : row["IRSMS_TP"].ToString(),
                Irsms_warr_no = row["IRSMS_WARR_NO"] == DBNull.Value ? string.Empty : row["IRSMS_WARR_NO"].ToString(),
                Irsms_warr_period = row["IRSMS_WARR_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRSMS_WARR_PERIOD"]),
                Irsms_warr_rem = row["IRSMS_WARR_REM"] == DBNull.Value ? string.Empty : row["IRSMS_WARR_REM"].ToString()

            };
        }
    }
}
