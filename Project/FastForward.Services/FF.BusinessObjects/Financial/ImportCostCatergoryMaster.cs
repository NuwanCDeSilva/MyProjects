using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Financial
{
    public class ImportCostCatergoryMaster
    {
        #region Private Members
        private Boolean _mca_act;
        private string _mca_cd;
        private string _mca_cre_by;
        private DateTime _mca_cre_dt;
        private string _mca_desc;
        private string _mca_mod_by;
        private DateTime _mca_mod_dt;
        private string _mca_session_id;
        #endregion
        #region property definition

        public Boolean Mca_act
        {
            get { return _mca_act; }
            set { _mca_act = value; }
        }
        public string Mca_cd
        {
            get { return _mca_cd; }
            set { _mca_cd = value; }
        }
        public string Mca_cre_by
        {
            get { return _mca_cre_by; }
            set { _mca_cre_by = value; }
        }
        public DateTime Mca_cre_dt
        {
            get { return _mca_cre_dt; }
            set { _mca_cre_dt = value; }
        }
        public string Mca_desc
        {
            get { return _mca_desc; }
            set { _mca_desc = value; }
        }
        public string Mca_mod_by
        {
            get { return _mca_mod_by; }
            set { _mca_mod_by = value; }
        }
        public DateTime Mca_mod_dt
        {
            get { return _mca_mod_dt; }
            set { _mca_mod_dt = value; }
        }
        public string Mca_session_id
        {
            get { return _mca_session_id; }
            set { _mca_session_id = value; }
        }
        #endregion
        public static ImportCostCatergoryMaster Converter(DataRow row)
        {
            return new ImportCostCatergoryMaster
            {
                Mca_act = row["MCA_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MCA_ACT"]),
                Mca_cd = row["MCA_CD"] == DBNull.Value ? string.Empty : row["MCA_CD"].ToString(),
                Mca_cre_by = row["MCA_CRE_BY"] == DBNull.Value ? string.Empty : row["MCA_CRE_BY"].ToString(),
                Mca_cre_dt = row["MCA_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MCA_CRE_DT"]),
                Mca_desc = row["MCA_DESC"] == DBNull.Value ? string.Empty : row["MCA_DESC"].ToString(),
                Mca_mod_by = row["MCA_MOD_BY"] == DBNull.Value ? string.Empty : row["MCA_MOD_BY"].ToString(),
                Mca_mod_dt = row["MCA_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MCA_MOD_DT"]),
                Mca_session_id = row["MCA_SESSION_ID"] == DBNull.Value ? string.Empty : row["MCA_SESSION_ID"].ToString()

            };
        }
    }
}