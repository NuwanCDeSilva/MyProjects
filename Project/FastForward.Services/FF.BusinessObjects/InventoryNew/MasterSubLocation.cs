using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class MasterSubLocation
    {
        #region Private Members
        private Boolean _msl_act;
        private string _msl_com;
        private string _msl_cont_no;
        private string _msl_cont_per;
        private string _msl_cover_ref;
        private string _msl_cre_by;
        private DateTime _msl_cre_dt;
        private DateTime _msl_insu_dt;
        private decimal _msl_insu_val;
        private string _msl_mloc;
        private string _msl_mod_by;
        private DateTime _msl_mod_dt;
        private DateTime _msl_operate_till;
        private string _msl_rmk;
        private string _msl_sadd1;
        private string _msl_sadd2;
        private string _msl_sloc;
        private string _msl_sloc_desc;
        private DateTime _msl_start_frm;
        #endregion

        public Boolean Msl_act
        {
            get { return _msl_act; }
            set { _msl_act = value; }
        }
        public string Msl_com
        {
            get { return _msl_com; }
            set { _msl_com = value; }
        }
        public string Msl_cont_no
        {
            get { return _msl_cont_no; }
            set { _msl_cont_no = value; }
        }
        public string Msl_cont_per
        {
            get { return _msl_cont_per; }
            set { _msl_cont_per = value; }
        }
        public string Msl_cover_ref
        {
            get { return _msl_cover_ref; }
            set { _msl_cover_ref = value; }
        }
        public string Msl_cre_by
        {
            get { return _msl_cre_by; }
            set { _msl_cre_by = value; }
        }
        public DateTime Msl_cre_dt
        {
            get { return _msl_cre_dt; }
            set { _msl_cre_dt = value; }
        }
        public DateTime Msl_insu_dt
        {
            get { return _msl_insu_dt; }
            set { _msl_insu_dt = value; }
        }
        public decimal Msl_insu_val
        {
            get { return _msl_insu_val; }
            set { _msl_insu_val = value; }
        }
        public string Msl_mloc
        {
            get { return _msl_mloc; }
            set { _msl_mloc = value; }
        }
        public string Msl_mod_by
        {
            get { return _msl_mod_by; }
            set { _msl_mod_by = value; }
        }
        public DateTime Msl_mod_dt
        {
            get { return _msl_mod_dt; }
            set { _msl_mod_dt = value; }
        }
        public DateTime Msl_operate_till
        {
            get { return _msl_operate_till; }
            set { _msl_operate_till = value; }
        }
        public string Msl_rmk
        {
            get { return _msl_rmk; }
            set { _msl_rmk = value; }
        }
        public string Msl_sadd1
        {
            get { return _msl_sadd1; }
            set { _msl_sadd1 = value; }
        }
        public string Msl_sadd2
        {
            get { return _msl_sadd2; }
            set { _msl_sadd2 = value; }
        }
        public string Msl_sloc
        {
            get { return _msl_sloc; }
            set { _msl_sloc = value; }
        }
        public string Msl_sloc_desc
        {
            get { return _msl_sloc_desc; }
            set { _msl_sloc_desc = value; }
        }
        public DateTime Msl_start_frm
        {
            get { return _msl_start_frm; }
            set { _msl_start_frm = value; }
        }


        public static MasterSubLocation Converter(DataRow row)
        {
            return new MasterSubLocation
            {
                Msl_act = row["MSL_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MSL_ACT"]),
                Msl_com = row["MSL_COM"] == DBNull.Value ? string.Empty : row["MSL_COM"].ToString(),
                Msl_cont_no = row["MSL_CONT_NO"] == DBNull.Value ? string.Empty : row["MSL_CONT_NO"].ToString(),
                Msl_cont_per = row["MSL_CONT_PER"] == DBNull.Value ? string.Empty : row["MSL_CONT_PER"].ToString(),
                Msl_cover_ref = row["MSL_COVER_REF"] == DBNull.Value ? string.Empty : row["MSL_COVER_REF"].ToString(),
                Msl_cre_by = row["MSL_CRE_BY"] == DBNull.Value ? string.Empty : row["MSL_CRE_BY"].ToString(),
                Msl_cre_dt = row["MSL_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MSL_CRE_DT"]),
                Msl_insu_dt = row["MSL_INSU_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MSL_INSU_DT"]),
                Msl_insu_val = row["MSL_INSU_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MSL_INSU_VAL"]),
                Msl_mloc = row["MSL_MLOC"] == DBNull.Value ? string.Empty : row["MSL_MLOC"].ToString(),
                Msl_mod_by = row["MSL_MOD_BY"] == DBNull.Value ? string.Empty : row["MSL_MOD_BY"].ToString(),
                Msl_mod_dt = row["MSL_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MSL_MOD_DT"]),
                Msl_operate_till = row["MSL_OPERATE_TILL"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MSL_OPERATE_TILL"]),
                Msl_rmk = row["MSL_RMK"] == DBNull.Value ? string.Empty : row["MSL_RMK"].ToString(),
                Msl_sadd1 = row["MSL_SADD1"] == DBNull.Value ? string.Empty : row["MSL_SADD1"].ToString(),
                Msl_sadd2 = row["MSL_SADD2"] == DBNull.Value ? string.Empty : row["MSL_SADD2"].ToString(),
                Msl_sloc = row["MSL_SLOC"] == DBNull.Value ? string.Empty : row["MSL_SLOC"].ToString(),
                Msl_sloc_desc = row["MSL_SLOC_DESC"] == DBNull.Value ? string.Empty : row["MSL_SLOC_DESC"].ToString(),
                Msl_start_frm = row["MSL_START_FRM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MSL_START_FRM"])

            };
        }

    }
}
