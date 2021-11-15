using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class MasterBankAccount
    {
        #region Private Members
        private string _msba_acc_cd;
        private string _msba_acc_desc;
        private DateTime _msba_acc_dt;
        private string _msba_cd;
        private string _msba_com;
        private string _msba_cre_by;
        private DateTime _msba_cre_dt;
        private string _msba_desc;
        private string _msba_mod_by;
        private DateTime _msba_mod_dt;
        private Boolean _msba_stus;
        private string _msba_brn_cd;
        #endregion

        public string Msba_acc_cd { get { return _msba_acc_cd; } set { _msba_acc_cd = value; } }
        public string Msba_acc_desc { get { return _msba_acc_desc; } set { _msba_acc_desc = value; } }
        public DateTime Msba_acc_dt { get { return _msba_acc_dt; } set { _msba_acc_dt = value; } }
        public string Msba_cd { get { return _msba_cd; } set { _msba_cd = value; } }
        public string Msba_com { get { return _msba_com; } set { _msba_com = value; } }
        public string Msba_cre_by { get { return _msba_cre_by; } set { _msba_cre_by = value; } }
        public DateTime Msba_cre_dt { get { return _msba_cre_dt; } set { _msba_cre_dt = value; } }
        public string Msba_desc { get { return _msba_desc; } set { _msba_desc = value; } }
        public string Msba_mod_by { get { return _msba_mod_by; } set { _msba_mod_by = value; } }
        public DateTime Msba_mod_dt { get { return _msba_mod_dt; } set { _msba_mod_dt = value; } }
        public Boolean Msba_stus { get { return _msba_stus; } set { _msba_stus = value; } }
        public string Msba_brn_cd { get { return _msba_brn_cd; } set { _msba_brn_cd = value; } }
        public static MasterBankAccount Converter(DataRow row)
        {
            return new MasterBankAccount
            {
                Msba_acc_cd = row["MSBA_ACC_CD"] == DBNull.Value ? string.Empty : row["MSBA_ACC_CD"].ToString(),
                Msba_acc_desc = row["MSBA_ACC_DESC"] == DBNull.Value ? string.Empty : row["MSBA_ACC_DESC"].ToString(),
                Msba_acc_dt = row["MSBA_ACC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MSBA_ACC_DT"]),
                Msba_cd = row["MSBA_CD"] == DBNull.Value ? string.Empty : row["MSBA_CD"].ToString(),
                Msba_com = row["MSBA_COM"] == DBNull.Value ? string.Empty : row["MSBA_COM"].ToString(),
                Msba_cre_by = row["MSBA_CRE_BY"] == DBNull.Value ? string.Empty : row["MSBA_CRE_BY"].ToString(),
                Msba_cre_dt = row["MSBA_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MSBA_CRE_DT"]),
                Msba_desc = row["MSBA_DESC"] == DBNull.Value ? string.Empty : row["MSBA_DESC"].ToString(),
                Msba_mod_by = row["MSBA_MOD_BY"] == DBNull.Value ? string.Empty : row["MSBA_MOD_BY"].ToString(),
                Msba_mod_dt = row["MSBA_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MSBA_MOD_DT"]),
                Msba_stus = row["MSBA_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["MSBA_STUS"]),
                Msba_brn_cd = row["MSBA_BRN_CD"] == DBNull.Value ? string.Empty : row["MSBA_BRN_CD"].ToString()
            };
        }
    }
}

