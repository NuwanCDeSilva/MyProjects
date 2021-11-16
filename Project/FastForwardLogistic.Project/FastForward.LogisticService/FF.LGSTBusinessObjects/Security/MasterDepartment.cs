using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Security
{
    public class MasterDepartment
    {
        #region Private Members

        private Boolean _msdt_act;
        private string _msdt_cd;
        private string _msdt_cre_by;
        private DateTime _msdt_cre_dt;
        private string _msdt_desc;
        private string _msdt_mod_by;
        private DateTime _msdt_mod_dt;
        private string _msdt_session_id;

        #endregion

        public Boolean Msdt_act
        {
            get { return _msdt_act; }
            set { _msdt_act = value; }
        }

        public string Msdt_cd
        {
            get { return _msdt_cd; }
            set { _msdt_cd = value; }
        }

        public string Msdt_cre_by
        {
            get { return _msdt_cre_by; }
            set { _msdt_cre_by = value; }
        }

        public DateTime Msdt_cre_dt
        {
            get { return _msdt_cre_dt; }
            set { _msdt_cre_dt = value; }
        }

        public string Msdt_desc
        {
            get { return _msdt_desc; }
            set { _msdt_desc = value; }
        }

        public string Msdt_mod_by
        {
            get { return _msdt_mod_by; }
            set { _msdt_mod_by = value; }
        }

        public DateTime Msdt_mod_dt
        {
            get { return _msdt_mod_dt; }
            set { _msdt_mod_dt = value; }
        }

        public string Msdt_session_id
        {
            get { return _msdt_session_id; }
            set { _msdt_session_id = value; }
        }




        public static MasterDepartment BaseConverter(DataRow row)
        {
            return new MasterDepartment
            {
                Msdt_act = row["MSDT_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MSDT_ACT"]),
                Msdt_cd = row["MSDT_CD"] == DBNull.Value ? string.Empty : row["MSDT_CD"].ToString(),
                Msdt_cre_by = row["MSDT_CRE_BY"] == DBNull.Value ? string.Empty : row["MSDT_CRE_BY"].ToString(),
                Msdt_cre_dt = row["MSDT_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MSDT_CRE_DT"]),
                Msdt_desc = row["MSDT_DESC"] == DBNull.Value ? string.Empty : row["MSDT_DESC"].ToString(),
                Msdt_mod_by = row["MSDT_MOD_BY"] == DBNull.Value ? string.Empty : row["MSDT_MOD_BY"].ToString(),
                Msdt_mod_dt = row["MSDT_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MSDT_MOD_DT"]),
                Msdt_session_id = row["MSDT_SESSION_ID"] == DBNull.Value ? string.Empty : row["MSDT_SESSION_ID"].ToString()

            };
        }

        public static MasterDepartment DeptConverter(DataRow row)
        {
            return new MasterDepartment
            {

                Msdt_cd = row["MSDT_CD"] == DBNull.Value ? string.Empty : row["MSDT_CD"].ToString(),
                Msdt_desc = row["MSDT_DESC"] == DBNull.Value ? string.Empty : row["MSDT_DESC"].ToString()

            };
        }
    }

}
