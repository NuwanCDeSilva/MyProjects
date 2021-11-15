using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    //table - sec_user_perm
    //created by Shani 17-06-2013
   public class SecUserPerm
   {
       #region Private Members
       private Boolean _seur_act;
       private string _seur_cd;
       private string _seur_com;
       private string _seur_cre_by;
       private DateTime _seur_cre_dt;
       private DateTime _seur_mod_by;
       private string _seur_mod_dt;
       private string _seur_party;
       private string _seur_usr_id;
       #endregion

       public Boolean Seur_act { get { return _seur_act; } set { _seur_act = value; } }
       public string Seur_cd { get { return _seur_cd; } set { _seur_cd = value; } }
       public string Seur_com { get { return _seur_com; } set { _seur_com = value; } }
       public string Seur_cre_by { get { return _seur_cre_by; } set { _seur_cre_by = value; } }
       public DateTime Seur_cre_dt { get { return _seur_cre_dt; } set { _seur_cre_dt = value; } }
       public DateTime Seur_mod_by { get { return _seur_mod_by; } set { _seur_mod_by = value; } }
       public string Seur_mod_dt { get { return _seur_mod_dt; } set { _seur_mod_dt = value; } }
       public string Seur_party { get { return _seur_party; } set { _seur_party = value; } }
       public string Seur_usr_id { get { return _seur_usr_id; } set { _seur_usr_id = value; } }

       public static SecUserPerm Converter(DataRow row)
       {
           return new SecUserPerm
           {
               Seur_act = row["SEUR_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["SEUR_ACT"]),
               Seur_cd = row["SEUR_CD"] == DBNull.Value ? string.Empty : row["SEUR_CD"].ToString(),
               Seur_com = row["SEUR_COM"] == DBNull.Value ? string.Empty : row["SEUR_COM"].ToString(),
               Seur_cre_by = row["SEUR_CRE_BY"] == DBNull.Value ? string.Empty : row["SEUR_CRE_BY"].ToString(),
               Seur_cre_dt = row["SEUR_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SEUR_CRE_DT"]),
               Seur_mod_by = row["SEUR_MOD_BY"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SEUR_MOD_BY"]),
               Seur_mod_dt = row["SEUR_MOD_DT"] == DBNull.Value ? string.Empty : row["SEUR_MOD_DT"].ToString(),
               Seur_party = row["SEUR_PARTY"] == DBNull.Value ? string.Empty : row["SEUR_PARTY"].ToString(),
               Seur_usr_id = row["SEUR_USR_ID"] == DBNull.Value ? string.Empty : row["SEUR_USR_ID"].ToString()

           };
       }

       
    }
}
