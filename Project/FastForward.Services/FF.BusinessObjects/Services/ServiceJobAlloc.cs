using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace FF.BusinessObjects
{
    [Serializable]
    public class ServiceJobAlloc
    {
        #region Private Members
        private string _gni_alloc;
        private Int32 _gni_alloc_index;
        private string _gni_brnd;
        private DateTime _gni_cancel_dt;
        private string _gni_cancel_rem;
        private string _gni_cat_1;
        private string _gni_cat_2;
        private string _gni_cus;
        private string _gni_cus_add;
        private DateTime _gni_do_dt;
        private string _gni_do_loc;
        private string _gni_do_no;
        private string _gni_hold_by;
        private DateTime _gni_hold_dt;
        private string _gni_hold_rem;
        private DateTime _gni_inv_dt;
        private string _gni_inv_no;
        private Int32 _gni_is_cancel;
        private Int32 _gni_is_sr_job_update;
        private Int32 _gni_is_updated;
        private string _gni_itm_cd;
        private string _gni_itm_desc;
        private Int32 _gni_job_com;
        private DateTime _gni_job_dt;
        private DateTime _gni_job_ed_dt;
        private string _gni_job_no;
        private Int32 _gni_job_stus;
        private DateTime _gni_job_st_dt;
        private string _gni_model;
        private string _gni_oth_rem;
        private string _gni_re_alloc_by;
        private DateTime _gni_re_alloc_dt;
        private string _gni_re_alloc_rem;
        private string _gni_serial;
        private string _gni_service_loc;
        private string _gni_wara_no;
        private Int32 _gni_wara_period;
        private string _gni_wara_rem;
        private string _GNI_CONT_NO;
        private string _GNI_CONT_PERSON;
        private string _GNI_SERV_ADDR;
        private string _GNI_INSTALL_LOC;
        #endregion

        public string Gni_alloc
        {
            get { return _gni_alloc; }
            set { _gni_alloc = value; }
        }
        public Int32 Gni_alloc_index
        {
            get { return _gni_alloc_index; }
            set { _gni_alloc_index = value; }
        }
        public string Gni_brnd
        {
            get { return _gni_brnd; }
            set { _gni_brnd = value; }
        }
        public DateTime Gni_cancel_dt
        {
            get { return _gni_cancel_dt; }
            set { _gni_cancel_dt = value; }
        }
        public string Gni_cancel_rem
        {
            get { return _gni_cancel_rem; }
            set { _gni_cancel_rem = value; }
        }
        public string Gni_cat_1
        {
            get { return _gni_cat_1; }
            set { _gni_cat_1 = value; }
        }
        public string Gni_cat_2
        {
            get { return _gni_cat_2; }
            set { _gni_cat_2 = value; }
        }
        public string Gni_cus
        {
            get { return _gni_cus; }
            set { _gni_cus = value; }
        }
        public string Gni_cus_add
        {
            get { return _gni_cus_add; }
            set { _gni_cus_add = value; }
        }
        public DateTime Gni_do_dt
        {
            get { return _gni_do_dt; }
            set { _gni_do_dt = value; }
        }
        public string Gni_do_loc
        {
            get { return _gni_do_loc; }
            set { _gni_do_loc = value; }
        }
        public string Gni_do_no
        {
            get { return _gni_do_no; }
            set { _gni_do_no = value; }
        }
        public string Gni_hold_by
        {
            get { return _gni_hold_by; }
            set { _gni_hold_by = value; }
        }
        public DateTime Gni_hold_dt
        {
            get { return _gni_hold_dt; }
            set { _gni_hold_dt = value; }
        }
        public string Gni_hold_rem
        {
            get { return _gni_hold_rem; }
            set { _gni_hold_rem = value; }
        }
        public DateTime Gni_inv_dt
        {
            get { return _gni_inv_dt; }
            set { _gni_inv_dt = value; }
        }
        public string Gni_inv_no
        {
            get { return _gni_inv_no; }
            set { _gni_inv_no = value; }
        }
        public Int32 Gni_is_cancel
        {
            get { return _gni_is_cancel; }
            set { _gni_is_cancel = value; }
        }
        public Int32 Gni_is_sr_job_update
        {
            get { return _gni_is_sr_job_update; }
            set { _gni_is_sr_job_update = value; }
        }
        public Int32 Gni_is_updated
        {
            get { return _gni_is_updated; }
            set { _gni_is_updated = value; }
        }
        public string Gni_itm_cd
        {
            get { return _gni_itm_cd; }
            set { _gni_itm_cd = value; }
        }
        public string Gni_itm_desc
        {
            get { return _gni_itm_desc; }
            set { _gni_itm_desc = value; }
        }
        public Int32 Gni_job_com
        {
            get { return _gni_job_com; }
            set { _gni_job_com = value; }
        }
        public DateTime Gni_job_dt
        {
            get { return _gni_job_dt; }
            set { _gni_job_dt = value; }
        }
        public DateTime Gni_job_ed_dt
        {
            get { return _gni_job_ed_dt; }
            set { _gni_job_ed_dt = value; }
        }
        public string Gni_job_no
        {
            get { return _gni_job_no; }
            set { _gni_job_no = value; }
        }
        public Int32 Gni_job_stus
        {
            get { return _gni_job_stus; }
            set { _gni_job_stus = value; }
        }
        public DateTime Gni_job_st_dt
        {
            get { return _gni_job_st_dt; }
            set { _gni_job_st_dt = value; }
        }
        public string Gni_model
        {
            get { return _gni_model; }
            set { _gni_model = value; }
        }
        public string Gni_oth_rem
        {
            get { return _gni_oth_rem; }
            set { _gni_oth_rem = value; }
        }
        public string Gni_re_alloc_by
        {
            get { return _gni_re_alloc_by; }
            set { _gni_re_alloc_by = value; }
        }
        public DateTime Gni_re_alloc_dt
        {
            get { return _gni_re_alloc_dt; }
            set { _gni_re_alloc_dt = value; }
        }
        public string Gni_re_alloc_rem
        {
            get { return _gni_re_alloc_rem; }
            set { _gni_re_alloc_rem = value; }
        }
        public string Gni_serial
        {
            get { return _gni_serial; }
            set { _gni_serial = value; }
        }
        public string Gni_service_loc
        {
            get { return _gni_service_loc; }
            set { _gni_service_loc = value; }
        }
        public string Gni_wara_no
        {
            get { return _gni_wara_no; }
            set { _gni_wara_no = value; }
        }
        public Int32 Gni_wara_period
        {
            get { return _gni_wara_period; }
            set { _gni_wara_period = value; }
        }
        public string Gni_wara_rem
        {
            get { return _gni_wara_rem; }
            set { _gni_wara_rem = value; }
        }
        public string GNI_CONT_NO
        {
            get { return _GNI_CONT_NO; }
            set { _GNI_CONT_NO = value; }
        }
        public string GNI_CONT_PERSON
        {
            get { return _GNI_CONT_PERSON; }
            set { _GNI_CONT_PERSON = value; }
        }
        public string GNI_SERV_ADDR
        {
            get { return _GNI_SERV_ADDR; }
            set { _GNI_SERV_ADDR = value; }
        }
        public string GNI_INSTALL_LOC
        {
            get { return _GNI_INSTALL_LOC; }
            set { _GNI_INSTALL_LOC = value; }
        }


        public static ServiceJobAlloc Converter(DataRow row)
        {
            return new ServiceJobAlloc
            {
                Gni_alloc = row["GNI_ALLOC"] == DBNull.Value ? string.Empty : row["GNI_ALLOC"].ToString(),
                Gni_alloc_index = row["GNI_ALLOC_INDEX"] == DBNull.Value ? 0 : Convert.ToInt32(row["GNI_ALLOC_INDEX"]),
                Gni_brnd = row["GNI_BRND"] == DBNull.Value ? string.Empty : row["GNI_BRND"].ToString(),
                Gni_cancel_dt = row["GNI_CANCEL_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GNI_CANCEL_DT"]),
                Gni_cancel_rem = row["GNI_CANCEL_REM"] == DBNull.Value ? string.Empty : row["GNI_CANCEL_REM"].ToString(),
                Gni_cat_1 = row["GNI_CAT_1"] == DBNull.Value ? string.Empty : row["GNI_CAT_1"].ToString(),
                Gni_cat_2 = row["GNI_CAT_2"] == DBNull.Value ? string.Empty : row["GNI_CAT_2"].ToString(),
                Gni_cus = row["GNI_CUS"] == DBNull.Value ? string.Empty : row["GNI_CUS"].ToString(),
                Gni_cus_add = row["GNI_CUS_ADD"] == DBNull.Value ? string.Empty : row["GNI_CUS_ADD"].ToString(),
                Gni_do_dt = row["GNI_DO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GNI_DO_DT"]),
                Gni_do_loc = row["GNI_DO_LOC"] == DBNull.Value ? string.Empty : row["GNI_DO_LOC"].ToString(),
                Gni_do_no = row["GNI_DO_NO"] == DBNull.Value ? string.Empty : row["GNI_DO_NO"].ToString(),
                Gni_hold_by = row["GNI_HOLD_BY"] == DBNull.Value ? string.Empty : row["GNI_HOLD_BY"].ToString(),
                Gni_hold_dt = row["GNI_HOLD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GNI_HOLD_DT"]),
                Gni_hold_rem = row["GNI_HOLD_REM"] == DBNull.Value ? string.Empty : row["GNI_HOLD_REM"].ToString(),
                Gni_inv_dt = row["GNI_INV_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GNI_INV_DT"]),
                Gni_inv_no = row["GNI_INV_NO"] == DBNull.Value ? string.Empty : row["GNI_INV_NO"].ToString(),
                Gni_is_cancel = row["GNI_IS_CANCEL"] == DBNull.Value ? 0 : Convert.ToInt32(row["GNI_IS_CANCEL"]),
                Gni_is_sr_job_update = row["GNI_IS_SR_JOB_UPDATE"] == DBNull.Value ? 0 : Convert.ToInt32(row["GNI_IS_SR_JOB_UPDATE"]),
                Gni_is_updated = row["GNI_IS_UPDATED"] == DBNull.Value ? 0 : Convert.ToInt32(row["GNI_IS_UPDATED"]),
                Gni_itm_cd = row["GNI_ITM_CD"] == DBNull.Value ? string.Empty : row["GNI_ITM_CD"].ToString(),
                Gni_itm_desc = row["GNI_ITM_DESC"] == DBNull.Value ? string.Empty : row["GNI_ITM_DESC"].ToString(),
                Gni_job_com = row["GNI_JOB_COM"] == DBNull.Value ? 0 : Convert.ToInt32(row["GNI_JOB_COM"]),
                Gni_job_dt = row["GNI_JOB_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GNI_JOB_DT"]),
                Gni_job_ed_dt = row["GNI_JOB_ED_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GNI_JOB_ED_DT"]),
                Gni_job_no = row["GNI_JOB_NO"] == DBNull.Value ? string.Empty : row["GNI_JOB_NO"].ToString(),
                Gni_job_stus = row["GNI_JOB_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["GNI_JOB_STUS"]),
                Gni_job_st_dt = row["GNI_JOB_ST_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GNI_JOB_ST_DT"]),
                Gni_model = row["GNI_MODEL"] == DBNull.Value ? string.Empty : row["GNI_MODEL"].ToString(),
                Gni_oth_rem = row["GNI_OTH_REM"] == DBNull.Value ? string.Empty : row["GNI_OTH_REM"].ToString(),
                Gni_re_alloc_by = row["GNI_RE_ALLOC_BY"] == DBNull.Value ? string.Empty : row["GNI_RE_ALLOC_BY"].ToString(),
                Gni_re_alloc_dt = row["GNI_RE_ALLOC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GNI_RE_ALLOC_DT"]),
                Gni_re_alloc_rem = row["GNI_RE_ALLOC_REM"] == DBNull.Value ? string.Empty : row["GNI_RE_ALLOC_REM"].ToString(),
                Gni_serial = row["GNI_SERIAL"] == DBNull.Value ? string.Empty : row["GNI_SERIAL"].ToString(),
                Gni_service_loc = row["GNI_SERVICE_LOC"] == DBNull.Value ? string.Empty : row["GNI_SERVICE_LOC"].ToString(),
                Gni_wara_no = row["GNI_WARA_NO"] == DBNull.Value ? string.Empty : row["GNI_WARA_NO"].ToString(),
                Gni_wara_period = row["GNI_WARA_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["GNI_WARA_PERIOD"]),

                GNI_CONT_NO = row["GNI_CONT_NO"] == DBNull.Value ? string.Empty : row["GNI_CONT_NO"].ToString(),
                GNI_CONT_PERSON = row["GNI_CONT_PERSON"] == DBNull.Value ? string.Empty : row["GNI_CONT_PERSON"].ToString(),
                GNI_SERV_ADDR = row["GNI_SERV_ADDR"] == DBNull.Value ? string.Empty : row["GNI_SERV_ADDR"].ToString(),
                GNI_INSTALL_LOC = row["GNI_INSTALL_LOC"] == DBNull.Value ? string.Empty : row["GNI_INSTALL_LOC"].ToString(),

                Gni_wara_rem = row["GNI_WARA_REM"] == DBNull.Value ? string.Empty : row["GNI_WARA_REM"].ToString()

            };
        }

    }
}
