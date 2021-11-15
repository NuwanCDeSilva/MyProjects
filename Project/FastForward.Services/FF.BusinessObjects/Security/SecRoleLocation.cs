using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    //table =sec_role_loc
    //created by = Shani  on 03-08-2013
   public class SecRoleLocation
   {
       #region Private Members
       private Boolean _ssrl_act;
       private string _ssrl_com;
       private string _ssrl_cre_by;
       private DateTime _ssrl_cre_dt;
       private string _ssrl_loc;
       private string _ssrl_mod_by;
       private DateTime _ssrl_mod_dt;
       private Boolean _ssrl_readonly;
       private Int32 _ssrl_roleid;
       #endregion

       public Boolean Ssrl_act { get { return _ssrl_act; } set { _ssrl_act = value; } }
       public string Ssrl_com { get { return _ssrl_com; } set { _ssrl_com = value; } }
       public string Ssrl_cre_by { get { return _ssrl_cre_by; } set { _ssrl_cre_by = value; } }
       public DateTime Ssrl_cre_dt { get { return _ssrl_cre_dt; } set { _ssrl_cre_dt = value; } }
       public string Ssrl_loc { get { return _ssrl_loc; } set { _ssrl_loc = value; } }
       public string Ssrl_mod_by { get { return _ssrl_mod_by; } set { _ssrl_mod_by = value; } }
       public DateTime Ssrl_mod_dt { get { return _ssrl_mod_dt; } set { _ssrl_mod_dt = value; } }
       public Boolean Ssrl_readonly { get { return _ssrl_readonly; } set { _ssrl_readonly = value; } }
       public Int32 Ssrl_roleid { get { return _ssrl_roleid; } set { _ssrl_roleid = value; } }

       public static SecRoleLocation Converter(DataRow row)
       {
           return new SecRoleLocation
           {
               Ssrl_act = row["SSRL_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["SSRL_ACT"]),
               Ssrl_com = row["SSRL_COM"] == DBNull.Value ? string.Empty : row["SSRL_COM"].ToString(),
               Ssrl_cre_by = row["SSRL_CRE_BY"] == DBNull.Value ? string.Empty : row["SSRL_CRE_BY"].ToString(),
               Ssrl_cre_dt = row["SSRL_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SSRL_CRE_DT"]),
               Ssrl_loc = row["SSRL_LOC"] == DBNull.Value ? string.Empty : row["SSRL_LOC"].ToString(),
               Ssrl_mod_by = row["SSRL_MOD_BY"] == DBNull.Value ? string.Empty : row["SSRL_MOD_BY"].ToString(),
               Ssrl_mod_dt = row["SSRL_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SSRL_MOD_DT"]),
               Ssrl_readonly = row["SSRL_READONLY"] == DBNull.Value ? false : Convert.ToBoolean(row["SSRL_READONLY"]),
               Ssrl_roleid = row["SSRL_ROLEID"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSRL_ROLEID"])

           };
       }

    }
}
