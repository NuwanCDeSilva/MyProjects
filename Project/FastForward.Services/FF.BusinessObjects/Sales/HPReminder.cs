using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class HPReminder
    {
        #region Private Members
        private string _hra_com;
        private string _hra_cre_by;
        private DateTime _hra_cre_dt;
        private string _hra_cust_mob;
        private DateTime _hra_dt;
        private string _hra_mgr_mob;
        private string _hra_mod_by;
        private string _hra_pc;
        private string _hra_ref;
        private string _hra_rmd;
        private Int32 _hra_seq;
        private string _hra_stus;
        private DateTime _hra_stus_dt;
        private string _hra_tp;
        #endregion

        public string Hra_com
        {
            get { return _hra_com; }
            set { _hra_com = value; }
        }
        public string Hra_cre_by
        {
            get { return _hra_cre_by; }
            set { _hra_cre_by = value; }
        }
        public DateTime Hra_cre_dt
        {
            get { return _hra_cre_dt; }
            set { _hra_cre_dt = value; }
        }
        public string Hra_cust_mob
        {
            get { return _hra_cust_mob; }
            set { _hra_cust_mob = value; }
        }
        public DateTime Hra_dt
        {
            get { return _hra_dt; }
            set { _hra_dt = value; }
        }
        public string Hra_mgr_mob
        {
            get { return _hra_mgr_mob; }
            set { _hra_mgr_mob = value; }
        }
        public string Hra_mod_by
        {
            get { return _hra_mod_by; }
            set { _hra_mod_by = value; }
        }
        public string Hra_pc
        {
            get { return _hra_pc; }
            set { _hra_pc = value; }
        }
        public string Hra_ref
        {
            get { return _hra_ref; }
            set { _hra_ref = value; }
        }
        public string Hra_rmd
        {
            get { return _hra_rmd; }
            set { _hra_rmd = value; }
        }
        public Int32 Hra_seq
        {
            get { return _hra_seq; }
            set { _hra_seq = value; }
        }
        public string Hra_stus
        {
            get { return _hra_stus; }
            set { _hra_stus = value; }
        }
        public DateTime Hra_stus_dt
        {
            get { return _hra_stus_dt; }
            set { _hra_stus_dt = value; }
        }
        public string Hra_tp
        {
            get { return _hra_tp; }
            set { _hra_tp = value; }
        }

        public static HPReminder Converter(DataRow row)
        {
            return new HPReminder
            {
                Hra_com = row["HRA_COM"] == DBNull.Value ? string.Empty : row["HRA_COM"].ToString(),
                Hra_cre_by = row["HRA_CRE_BY"] == DBNull.Value ? string.Empty : row["HRA_CRE_BY"].ToString(),
                Hra_cre_dt = row["HRA_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HRA_CRE_DT"]),
                Hra_cust_mob = row["HRA_CUST_MOB"] == DBNull.Value ? string.Empty : row["HRA_CUST_MOB"].ToString(),
                Hra_dt = row["HRA_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HRA_DT"]),
                Hra_mgr_mob = row["HRA_MGR_MOB"] == DBNull.Value ? string.Empty : row["HRA_MGR_MOB"].ToString(),
                Hra_mod_by = row["HRA_MOD_BY"] == DBNull.Value ? string.Empty : row["HRA_MOD_BY"].ToString(),
                Hra_pc = row["HRA_PC"] == DBNull.Value ? string.Empty : row["HRA_PC"].ToString(),
                Hra_ref = row["HRA_REF"] == DBNull.Value ? string.Empty : row["HRA_REF"].ToString(),
                Hra_rmd = row["HRA_RMD"] == DBNull.Value ? string.Empty : row["HRA_RMD"].ToString(),
                Hra_seq = row["HRA_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HRA_SEQ"]),
                Hra_stus = row["HRA_STUS"] == DBNull.Value ? string.Empty : row["HRA_STUS"].ToString(),
                Hra_stus_dt = row["HRA_STUS_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HRA_STUS_DT"]),
                Hra_tp = row["HRA_TP"] == DBNull.Value ? string.Empty : row["HRA_TP"].ToString()

            };
        }

    }
}
