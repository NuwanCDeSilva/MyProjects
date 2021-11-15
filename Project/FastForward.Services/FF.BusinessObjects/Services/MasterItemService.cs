using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    /// <summary>
    /// Written By Shani on 02/06/2013
    /// Table: mst_itm_sev   
    /// </summary>
   public class MasterItemService
    {

        #region Private Members
        private Boolean _misv_act;
        private string _misv_chg_pty;
        private string _misv_cre_by;
        private DateTime _misv_cre_dt;
        private string _misv_itm_cd;
        private string _misv_mod_by;
        private DateTime _misv_mod_dt;
        private string _misv_sevitm_cd;
        private string _misv_sevitm_stus;
        #endregion

        public Boolean Misv_act { get { return _misv_act; } set { _misv_act = value; } }
        public string Misv_chg_pty { get { return _misv_chg_pty; } set { _misv_chg_pty = value; } }
        public string Misv_cre_by { get { return _misv_cre_by; } set { _misv_cre_by = value; } }
        public DateTime Misv_cre_dt { get { return _misv_cre_dt; } set { _misv_cre_dt = value; } }
        public string Misv_itm_cd { get { return _misv_itm_cd; } set { _misv_itm_cd = value; } }
        public string Misv_mod_by { get { return _misv_mod_by; } set { _misv_mod_by = value; } }
        public DateTime Misv_mod_dt { get { return _misv_mod_dt; } set { _misv_mod_dt = value; } }
        public string Misv_sevitm_cd { get { return _misv_sevitm_cd; } set { _misv_sevitm_cd = value; } }
        public string Misv_sevitm_stus { get { return _misv_sevitm_stus; } set { _misv_sevitm_stus = value; } }

        public static MasterItemService Converter(DataRow row)
        {
            return new MasterItemService
            {
                Misv_act = row["MISV_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MISV_ACT"]),
                Misv_chg_pty = row["MISV_CHG_PTY"] == DBNull.Value ? string.Empty : row["MISV_CHG_PTY"].ToString(),
                Misv_cre_by = row["MISV_CRE_BY"] == DBNull.Value ? string.Empty : row["MISV_CRE_BY"].ToString(),
                Misv_cre_dt = row["MISV_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MISV_CRE_DT"]),
                Misv_itm_cd = row["MISV_ITM_CD"] == DBNull.Value ? string.Empty : row["MISV_ITM_CD"].ToString(),
                Misv_mod_by = row["MISV_MOD_BY"] == DBNull.Value ? string.Empty : row["MISV_MOD_BY"].ToString(),
                Misv_mod_dt = row["MISV_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MISV_MOD_DT"]),
                Misv_sevitm_cd = row["MISV_SEVITM_CD"] == DBNull.Value ? string.Empty : row["MISV_SEVITM_CD"].ToString(),
                Misv_sevitm_stus = row["MISV_SEVITM_STUS"] == DBNull.Value ? string.Empty : row["MISV_SEVITM_STUS"].ToString()

            };
        }

    }
}
