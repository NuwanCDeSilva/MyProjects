using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

//darshana on 02-11-2012
namespace FF.BusinessObjects
{
    public class AllowFunction
    {
        #region Private Members
        private Boolean _saf_active;
        private string _saf_cre_by;
        private DateTime _saf_cre_dt;
        private string _saf_func_cd;
        private Int32 _saf_id;
        private string _saf_mod_by;
        private string _saf_mod_cd;
        private DateTime _saf_mod_dt;
        private string _saf_pc;
        #endregion


        public Boolean Saf_active
        {
            get { return _saf_active; }
            set { _saf_active = value; }
        }
        public string Saf_cre_by
        {
            get { return _saf_cre_by; }
            set { _saf_cre_by = value; }
        }
        public DateTime Saf_cre_dt
        {
            get { return _saf_cre_dt; }
            set { _saf_cre_dt = value; }
        }
        public string Saf_func_cd
        {
            get { return _saf_func_cd; }
            set { _saf_func_cd = value; }
        }
        public Int32 Saf_id
        {
            get { return _saf_id; }
            set { _saf_id = value; }
        }
        public string Saf_mod_by
        {
            get { return _saf_mod_by; }
            set { _saf_mod_by = value; }
        }
        public string Saf_mod_cd
        {
            get { return _saf_mod_cd; }
            set { _saf_mod_cd = value; }
        }
        public DateTime Saf_mod_dt
        {
            get { return _saf_mod_dt; }
            set { _saf_mod_dt = value; }
        }
        public string Saf_pc
        {
            get { return _saf_pc; }
            set { _saf_pc = value; }
        }

        public static AllowFunction Converter(DataRow row)
        {
            return new AllowFunction
            {
                Saf_active = row["SAF_ACTIVE"] == DBNull.Value ? false : Convert.ToBoolean(row["SAF_ACTIVE"]),
                Saf_cre_by = row["SAF_CRE_BY"] == DBNull.Value ? string.Empty : row["SAF_CRE_BY"].ToString(),
                Saf_cre_dt = row["SAF_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAF_CRE_DT"]),
                Saf_func_cd = row["SAF_FUNC_CD"] == DBNull.Value ? string.Empty : row["SAF_FUNC_CD"].ToString(),
                Saf_id = row["SAF_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAF_ID"]),
                Saf_mod_by = row["SAF_MOD_BY"] == DBNull.Value ? string.Empty : row["SAF_MOD_BY"].ToString(),
                Saf_mod_cd = row["SAF_MOD_CD"] == DBNull.Value ? string.Empty : row["SAF_MOD_CD"].ToString(),
                Saf_mod_dt = row["SAF_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAF_MOD_DT"]),
                Saf_pc = row["SAF_PC"] == DBNull.Value ? string.Empty : row["SAF_PC"].ToString()

            };
        }

    }
}
