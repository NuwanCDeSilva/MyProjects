using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    // created by Shani 12-07-2013
    // table = sec_app_usr_prem

   [Serializable]
   public class SecApproveUserPerm
   {
       #region Private Members
       private Boolean _saup_act;
       private string _saup_cre_by;
       private DateTime _saup_cre_when;
       private decimal _saup_max_app_limit;
       private string _saup_mod_by;
       private DateTime _saup_mod_when;
       private string _saup_prem_cd;
       private string _saup_session_id;
       private string _saup_usr_id;
       private decimal _saup_val_limit;
       #endregion

       public Boolean Saup_act { get { return _saup_act; } set { _saup_act = value; } }
       public string Saup_cre_by { get { return _saup_cre_by; } set { _saup_cre_by = value; } }
       public DateTime Saup_cre_when { get { return _saup_cre_when; } set { _saup_cre_when = value; } }
       public decimal Saup_max_app_limit { get { return _saup_max_app_limit; } set { _saup_max_app_limit = value; } }
       public string Saup_mod_by { get { return _saup_mod_by; } set { _saup_mod_by = value; } }
       public DateTime Saup_mod_when { get { return _saup_mod_when; } set { _saup_mod_when = value; } }
       public string Saup_prem_cd { get { return _saup_prem_cd; } set { _saup_prem_cd = value; } }
       public string Saup_session_id { get { return _saup_session_id; } set { _saup_session_id = value; } }
       public string Saup_usr_id { get { return _saup_usr_id; } set { _saup_usr_id = value; } }
       public decimal Saup_val_limit { get { return _saup_val_limit; } set { _saup_val_limit = value; } }

       public static SecApproveUserPerm Converter(DataRow row)
       {
           return new SecApproveUserPerm
           {
               Saup_act = row["SAUP_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["SAUP_ACT"]),
               Saup_cre_by = row["SAUP_CRE_BY"] == DBNull.Value ? string.Empty : row["SAUP_CRE_BY"].ToString(),
               Saup_cre_when = row["SAUP_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAUP_CRE_WHEN"]),
               Saup_max_app_limit = row["SAUP_MAX_APP_LIMIT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAUP_MAX_APP_LIMIT"]),
               Saup_mod_by = row["SAUP_MOD_BY"] == DBNull.Value ? string.Empty : row["SAUP_MOD_BY"].ToString(),
               Saup_mod_when = row["SAUP_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAUP_MOD_WHEN"]),
               Saup_prem_cd = row["SAUP_PREM_CD"] == DBNull.Value ? string.Empty : row["SAUP_PREM_CD"].ToString(),
               Saup_session_id = row["SAUP_SESSION_ID"] == DBNull.Value ? string.Empty : row["SAUP_SESSION_ID"].ToString(),
               Saup_usr_id = row["SAUP_USR_ID"] == DBNull.Value ? string.Empty : row["SAUP_USR_ID"].ToString(),
               Saup_val_limit = row["SAUP_VAL_LIMIT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAUP_VAL_LIMIT"])

           };
       }
    }
}
