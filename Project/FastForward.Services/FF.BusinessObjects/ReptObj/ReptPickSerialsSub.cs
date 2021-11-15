using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
      [Serializable]
    public class ReptPickSerialsSub 
    {
        //
        // Function            - Report database common for scan sub serials
        // Function Wriiten By - P.Wijetunge
        // Date                - 28/03/2012
        //

        #region Private Members
        private string _tpss_itm_brand;
        private string _tpss_itm_cd;
        private string _tpss_itm_desc;
        private string _tpss_itm_model;
        private string _tpss_itm_stus;
        private string _tpss_mfc;
        private string _tpss_m_itm_cd;
        private string _tpss_m_ser;
        private string _tpss_sub_ser;
        private string _tpss_tp;
        private Int32 _tpss_usrseq_no;
        private string _tpss_warr_no;
        private Int32 _tpss_warr_period;
        private string _tpss_warr_rem;
        private Int32 _tpss_ser_id;//add by Rukshan 2015/oct/01

        #endregion

        public string Tpss_itm_brand { get { return _tpss_itm_brand; } set { _tpss_itm_brand = value; } }
        public string Tpss_itm_cd { get { return _tpss_itm_cd; } set { _tpss_itm_cd = value; } }
        public string Tpss_itm_desc { get { return _tpss_itm_desc; } set { _tpss_itm_desc = value; } }
        public string Tpss_itm_model { get { return _tpss_itm_model; } set { _tpss_itm_model = value; } }
        public string Tpss_itm_stus { get { return _tpss_itm_stus; } set { _tpss_itm_stus = value; } }
        public string Tpss_mfc { get { return _tpss_mfc; } set { _tpss_mfc = value; } }
        public string Tpss_m_itm_cd { get { return _tpss_m_itm_cd; } set { _tpss_m_itm_cd = value; } }
        public string Tpss_m_ser { get { return _tpss_m_ser; } set { _tpss_m_ser = value; } }
        public string Tpss_sub_ser { get { return _tpss_sub_ser; } set { _tpss_sub_ser = value; } }
        public string Tpss_tp { get { return _tpss_tp; } set { _tpss_tp = value; } }
        public Int32 Tpss_usrseq_no { get { return _tpss_usrseq_no; } set { _tpss_usrseq_no = value; } }
        public string Tpss_warr_no { get { return _tpss_warr_no; } set { _tpss_warr_no = value; } }
        public Int32 Tpss_warr_period { get { return _tpss_warr_period; } set { _tpss_warr_period = value; } }
        public string Tpss_warr_rem { get { return _tpss_warr_rem; } set { _tpss_warr_rem = value; } }
        public Int32 Tpss_ser_id { get { return _tpss_ser_id; } set { _tpss_ser_id = value; } }
          

        public static ReptPickSerialsSub ConvertTotal(DataRow row)
        {
            return new ReptPickSerialsSub
            {
                Tpss_itm_brand = row["TPSS_ITM_BRAND"] == DBNull.Value ? string.Empty : row["TPSS_ITM_BRAND"].ToString(),
                Tpss_itm_cd = row["TPSS_ITM_CD"] == DBNull.Value ? string.Empty : row["TPSS_ITM_CD"].ToString(),
                Tpss_itm_desc = row["TPSS_ITM_DESC"] == DBNull.Value ? string.Empty : row["TPSS_ITM_DESC"].ToString(),
                Tpss_itm_model = row["TPSS_ITM_MODEL"] == DBNull.Value ? string.Empty : row["TPSS_ITM_MODEL"].ToString(),
                Tpss_itm_stus = row["TPSS_ITM_STUS"] == DBNull.Value ? string.Empty : row["TPSS_ITM_STUS"].ToString(),
                Tpss_mfc = row["TPSS_MFC"] == DBNull.Value ? string.Empty : row["TPSS_MFC"].ToString(),
                Tpss_m_itm_cd = row["TPSS_M_ITM_CD"] == DBNull.Value ? string.Empty : row["TPSS_M_ITM_CD"].ToString(),
                Tpss_m_ser = row["TPSS_M_SER"] == DBNull.Value ? string.Empty : row["TPSS_M_SER"].ToString(),
                Tpss_sub_ser = row["TPSS_SUB_SER"] == DBNull.Value ? string.Empty : row["TPSS_SUB_SER"].ToString(),
                Tpss_tp = row["TPSS_TP"] == DBNull.Value ? string.Empty : row["TPSS_TP"].ToString(),
                Tpss_usrseq_no = row["TPSS_USRSEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["TPSS_USRSEQ_NO"]),
                Tpss_warr_no = row["TPSS_WARR_NO"] == DBNull.Value ? string.Empty : row["TPSS_WARR_NO"].ToString(),
                Tpss_warr_period = row["TPSS_WARR_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["TPSS_WARR_PERIOD"]),
                Tpss_warr_rem = row["TPSS_WARR_REM"] == DBNull.Value ? string.Empty : row["TPSS_WARR_REM"].ToString(),
                Tpss_ser_id = row["TPSS_SER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["TPSS_SER_ID"])
                


            };
        }
    }
}

