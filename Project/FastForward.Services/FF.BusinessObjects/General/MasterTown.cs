using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class MasterTown
    {

        #region Private Members
        private Boolean _mt_act;
        private string _mt_cd;
        private string _mt_country_cd;
        private string _mt_cre_by;
        private DateTime _mt_cre_dt;
        private string _mt_desc;
        private decimal _mt_distance;
        private string _mt_distance_from;
        private string _mt_distance_uom;
        private string _mt_distric_cd;
        private decimal _mt_height;
        private string _mt_height_uom;
        private string _mt_lat;
        private string _mt_lon;
        private string _mt_mod_by;
        private DateTime _mt_mod_dt;
        private string _mt_postal_cd;
        private string _mt_province_cd;
        private string _mt_session_id;
        private string _mt_tp;
        private string _mt_ds_cd;
        #endregion

        #region Public Property Definition
        public Boolean Mt_act
        {
            get { return _mt_act; }
            set { _mt_act = value; }
        }
        public string Mt_ds_cd
        {
            get { return _mt_ds_cd; }
            set { _mt_ds_cd = value; }
        }
        public string Mt_cd
        {
            get { return _mt_cd; }
            set { _mt_cd = value; }
        }
        public string Mt_country_cd
        {
            get { return _mt_country_cd; }
            set { _mt_country_cd = value; }
        }
        public string Mt_cre_by
        {
            get { return _mt_cre_by; }
            set { _mt_cre_by = value; }
        }
        public DateTime Mt_cre_dt
        {
            get { return _mt_cre_dt; }
            set { _mt_cre_dt = value; }
        }
        public string Mt_desc
        {
            get { return _mt_desc; }
            set { _mt_desc = value; }
        }
        public decimal Mt_distance
        {
            get { return _mt_distance; }
            set { _mt_distance = value; }
        }
        public string Mt_distance_from
        {
            get { return _mt_distance_from; }
            set { _mt_distance_from = value; }
        }
        public string Mt_distance_uom
        {
            get { return _mt_distance_uom; }
            set { _mt_distance_uom = value; }
        }
        public string Mt_distric_cd
        {
            get { return _mt_distric_cd; }
            set { _mt_distric_cd = value; }
        }
        public decimal Mt_height
        {
            get { return _mt_height; }
            set { _mt_height = value; }
        }
        public string Mt_height_uom
        {
            get { return _mt_height_uom; }
            set { _mt_height_uom = value; }
        }
        public string Mt_lat
        {
            get { return _mt_lat; }
            set { _mt_lat = value; }
        }
        public string Mt_lon
        {
            get { return _mt_lon; }
            set { _mt_lon = value; }
        }
        public string Mt_mod_by
        {
            get { return _mt_mod_by; }
            set { _mt_mod_by = value; }
        }
        public DateTime Mt_mod_dt
        {
            get { return _mt_mod_dt; }
            set { _mt_mod_dt = value; }
        }
        public string Mt_postal_cd
        {
            get { return _mt_postal_cd; }
            set { _mt_postal_cd = value; }
        }
        public string Mt_province_cd
        {
            get { return _mt_province_cd; }
            set { _mt_province_cd = value; }
        }
        public string Mt_session_id
        {
            get { return _mt_session_id; }
            set { _mt_session_id = value; }
        }
        public string Mt_tp
        {
            get { return _mt_tp; }
            set { _mt_tp = value; }
        }
        #endregion

        public static MasterTown Converter(DataRow row)
        {
            return new MasterTown
            {
                Mt_act = row["MT_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MT_ACT"]),
                Mt_cd = row["MT_CD"] == DBNull.Value ? string.Empty : row["MT_CD"].ToString(),
                Mt_country_cd = row["MT_COUNTRY_CD"] == DBNull.Value ? string.Empty : row["MT_COUNTRY_CD"].ToString(),
                Mt_cre_by = row["MT_CRE_BY"] == DBNull.Value ? string.Empty : row["MT_CRE_BY"].ToString(),
                Mt_cre_dt = row["MT_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MT_CRE_DT"]),
                Mt_desc = row["MT_DESC"] == DBNull.Value ? string.Empty : row["MT_DESC"].ToString(),
                Mt_distance = row["MT_DISTANCE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MT_DISTANCE"]),
                Mt_distance_from = row["MT_DISTANCE_FROM"] == DBNull.Value ? string.Empty : row["MT_DISTANCE_FROM"].ToString(),
                Mt_distance_uom = row["MT_DISTANCE_UOM"] == DBNull.Value ? string.Empty : row["MT_DISTANCE_UOM"].ToString(),
                Mt_distric_cd = row["MT_DISTRIC_CD"] == DBNull.Value ? string.Empty : row["MT_DISTRIC_CD"].ToString(),
                Mt_height = row["MT_HEIGHT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MT_HEIGHT"]),
                Mt_height_uom = row["MT_HEIGHT_UOM"] == DBNull.Value ? string.Empty : row["MT_HEIGHT_UOM"].ToString(),
                Mt_lat = row["MT_LAT"] == DBNull.Value ? string.Empty : row["MT_LAT"].ToString(),
                Mt_lon = row["MT_LON"] == DBNull.Value ? string.Empty : row["MT_LON"].ToString(),
                Mt_mod_by = row["MT_MOD_BY"] == DBNull.Value ? string.Empty : row["MT_MOD_BY"].ToString(),
                Mt_mod_dt = row["MT_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MT_MOD_DT"]),
                Mt_postal_cd = row["MT_POSTAL_CD"] == DBNull.Value ? string.Empty : row["MT_POSTAL_CD"].ToString(),
                Mt_province_cd = row["MT_PROVINCE_CD"] == DBNull.Value ? string.Empty : row["MT_PROVINCE_CD"].ToString(),
                Mt_session_id = row["MT_SESSION_ID"] == DBNull.Value ? string.Empty : row["MT_SESSION_ID"].ToString(),
                Mt_ds_cd = row["Mt_ds_cd"] == DBNull.Value ? string.Empty : row["Mt_ds_cd"].ToString(),
                Mt_tp = row["MT_TP"] == DBNull.Value ? string.Empty : row["MT_TP"].ToString()

            };

        }
    }
}


