using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Security
{
    public class SystemUserCompany
    {
        #region private members

        private string _SEC_USR_ID = string.Empty;
        private string _SEC_COM_CD = string.Empty;
        private int _SEC_DEF_COMCD = 0;
        private int _SEC_ACT = 0;
        private string _SEC_CRE_BY = string.Empty;
        private DateTime _SEC_CRE_DT = DateTime.MinValue;
        private string _SEC_MOD_BY = string.Empty;
        private DateTime _SEC_MOD_DT = DateTime.MinValue;
        private string _SEC_SESSION_ID = string.Empty;


        private string _SEC_Mc_Desc = string.Empty;

        //private string _COMP_DESN = string.Empty;
        MasterCompany _masterComp = null;

        #endregion

        #region public property definition
        public string SEC_USR_ID
        {
            get { return _SEC_USR_ID; }
            set { _SEC_USR_ID = value; }
        }
        public string SEC_COM_CD
        {
            get { return _SEC_COM_CD; }
            set { _SEC_COM_CD = value; }
        }
        public int SEC_DEF_COMCD
        {
            get { return _SEC_DEF_COMCD; }
            set { _SEC_DEF_COMCD = value; }
        }
        public int SEC_ACT
        {
            get { return _SEC_ACT; }
            set { _SEC_ACT = value; }
        }
        public string SEC_CRE_BY
        {
            get { return _SEC_CRE_BY; }
            set { _SEC_CRE_BY = value; }
        }
        public DateTime SEC_CRE_DT
        {
            get { return _SEC_CRE_DT; }
            set { _SEC_CRE_DT = value; }
        }
        public string SEC_MOD_BY
        {
            get { return _SEC_MOD_BY; }
            set { _SEC_MOD_BY = value; }
        }

        public DateTime SEC_MOD_DT
        {
            get { return _SEC_MOD_DT; }
            set { _SEC_MOD_DT = value; }
        }
        public string SEC_SESSION_ID
        {
            get { return _SEC_SESSION_ID; }
            set { _SEC_SESSION_ID = value; }
        }

        public string SEC_Mc_Desc
        {
            get { return _SEC_Mc_Desc; }
            set { _SEC_Mc_Desc = value; }

        }

        public MasterCompany MasterComp
        {
            get { return _masterComp; }
            set { _masterComp = value; }
        }

        #endregion

        public static SystemUserCompany converter(DataRow row)
        {
            return new SystemUserCompany
            {
                SEC_USR_ID = ((row["SEC_USR_ID"] == DBNull.Value) ? string.Empty : row["SEC_USR_ID"].ToString()),
                SEC_COM_CD = ((row["SEC_COM_CD"] == DBNull.Value) ? string.Empty : row["SEC_COM_CD"].ToString()),
                SEC_DEF_COMCD = ((row["SEC_DEF_COMCD"] == DBNull.Value) ? 0 : Convert.ToInt32(row["SEC_DEF_COMCD"].ToString())),
                SEC_ACT = ((row["SEC_ACT"] == DBNull.Value) ? 0 : Convert.ToInt32(row["SEC_ACT"].ToString())),
                SEC_CRE_BY = ((row["SEC_CRE_BY"] == DBNull.Value) ? string.Empty : row["SEC_CRE_BY"].ToString()),
                SEC_CRE_DT = ((row["SEC_CRE_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SEC_CRE_DT"])),
                SEC_MOD_BY = ((row["SEC_MOD_BY"] == DBNull.Value) ? string.Empty : row["SEC_MOD_BY"].ToString()),
                SEC_MOD_DT = ((row["SEC_MOD_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SEC_MOD_DT"])),
                SEC_SESSION_ID = ((row["SEC_SESSION_ID"] == DBNull.Value) ? string.Empty : row["SEC_SESSION_ID"].ToString()),
                //COMP_DESN = ((row["mc_desc"] == DBNull.Value) ? string.Empty : row["mc_desc"].ToString()),
                SEC_Mc_Desc = ((row["mc_desc"] == DBNull.Value) ? string.Empty : row["mc_desc"].ToString()),
            };
        }
    }
}
