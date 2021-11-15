using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class SCV_AGR_ITM
    {
        #region Private Members
        private string _sai_agr_no;
        private Int32 _sai_line;
        private Int32 _sai_seq_no;
        private string _shi_brand;
        private string _shi_cate1;
        private string _shi_cate2;
        private string _shi_cate3;
        private string _shi_itm_cd;
        private string _shi_itm_desc;
        private string _shi_model;
        private decimal _shi_qty;
        private string _shi_regno;
        private string _shi_ser1;
        private string _shi_ser2;
        private string _shi_ser_id;
        private Int32 _shi_sessions;
        private string _shi_warrno;
        private Boolean _shi_warr_stus;
        private string _shi_inv_no;     //kapila 9/11/2015
        #endregion

        #region Public Property Definition
        public string Shi_inv_no
        {
            get { return _shi_inv_no; }
            set { _shi_inv_no = value; }
        }
        public string Sai_agr_no
        {
            get { return _sai_agr_no; }
            set { _sai_agr_no = value; }
        }
        public Int32 Sai_line
        {
            get { return _sai_line; }
            set { _sai_line = value; }
        }
        public Int32 Sai_seq_no
        {
            get { return _sai_seq_no; }
            set { _sai_seq_no = value; }
        }
        public string Shi_brand
        {
            get { return _shi_brand; }
            set { _shi_brand = value; }
        }
        public string Shi_cate1
        {
            get { return _shi_cate1; }
            set { _shi_cate1 = value; }
        }
        public string Shi_cate2
        {
            get { return _shi_cate2; }
            set { _shi_cate2 = value; }
        }
        public string Shi_cate3
        {
            get { return _shi_cate3; }
            set { _shi_cate3 = value; }
        }
        public string Shi_itm_cd
        {
            get { return _shi_itm_cd; }
            set { _shi_itm_cd = value; }
        }
        public string Shi_itm_desc
        {
            get { return _shi_itm_desc; }
            set { _shi_itm_desc = value; }
        }
        public string Shi_model
        {
            get { return _shi_model; }
            set { _shi_model = value; }
        }
        public decimal Shi_qty
        {
            get { return _shi_qty; }
            set { _shi_qty = value; }
        }
        public string Shi_regno
        {
            get { return _shi_regno; }
            set { _shi_regno = value; }
        }
        public string Shi_ser1
        {
            get { return _shi_ser1; }
            set { _shi_ser1 = value; }
        }
        public string Shi_ser2
        {
            get { return _shi_ser2; }
            set { _shi_ser2 = value; }
        }
        public string Shi_ser_id
        {
            get { return _shi_ser_id; }
            set { _shi_ser_id = value; }
        }
        public Int32 Shi_sessions
        {
            get { return _shi_sessions; }
            set { _shi_sessions = value; }
        }
        public string Shi_warrno
        {
            get { return _shi_warrno; }
            set { _shi_warrno = value; }
        }
        public Boolean Shi_warr_stus
        {
            get { return _shi_warr_stus; }
            set { _shi_warr_stus = value; }
        }


        #endregion

        #region Converters
        public static SCV_AGR_ITM Converter(DataRow row)
        {
            return new SCV_AGR_ITM
            {

                Sai_agr_no = row["SAI_AGR_NO"] == DBNull.Value ? string.Empty : row["SAI_AGR_NO"].ToString(),
                Sai_line = row["SAI_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAI_LINE"]),
                Sai_seq_no = row["SAI_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAI_SEQ_NO"]),
                Shi_brand = row["SHI_BRAND"] == DBNull.Value ? string.Empty : row["SHI_BRAND"].ToString(),
                Shi_cate1 = row["SHI_CATE1"] == DBNull.Value ? string.Empty : row["SHI_CATE1"].ToString(),
                Shi_cate2 = row["SHI_CATE2"] == DBNull.Value ? string.Empty : row["SHI_CATE2"].ToString(),
                Shi_cate3 = row["SHI_CATE3"] == DBNull.Value ? string.Empty : row["SHI_CATE3"].ToString(),
                Shi_itm_cd = row["SHI_ITM_CD"] == DBNull.Value ? string.Empty : row["SHI_ITM_CD"].ToString(),
                Shi_itm_desc = row["SHI_ITM_DESC"] == DBNull.Value ? string.Empty : row["SHI_ITM_DESC"].ToString(),
                Shi_model = row["SHI_MODEL"] == DBNull.Value ? string.Empty : row["SHI_MODEL"].ToString(),
                Shi_qty = row["SHI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SHI_QTY"]),
                Shi_regno = row["SHI_REGNO"] == DBNull.Value ? string.Empty : row["SHI_REGNO"].ToString(),
                Shi_ser1 = row["SHI_SER1"] == DBNull.Value ? string.Empty : row["SHI_SER1"].ToString(),
                Shi_ser2 = row["SHI_SER2"] == DBNull.Value ? string.Empty : row["SHI_SER2"].ToString(),
                Shi_ser_id = row["SHI_SER_ID"] == DBNull.Value ? string.Empty : row["SHI_SER_ID"].ToString(),
                Shi_sessions = row["SHI_SESSIONS"] == DBNull.Value ? 0 : Convert.ToInt32(row["SHI_SESSIONS"]),
                Shi_warrno = row["SHI_WARRNO"] == DBNull.Value ? string.Empty : row["SHI_WARRNO"].ToString(),
                Shi_warr_stus = row["SHI_WARR_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["SHI_WARR_STUS"]),
                Shi_inv_no = row["SHI_INV_NO"] == DBNull.Value ? string.Empty : row["SHI_INV_NO"].ToString()
            };
        }
        #endregion
    }
}

