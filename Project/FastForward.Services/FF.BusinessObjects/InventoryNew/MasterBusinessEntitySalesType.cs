using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class MasterBusinessEntitySalesType
    {
        #region Private Members
        private Boolean _mbsa_act;
        private string _mbsa_cd;
        private string _mbsa_com;
        private string _mbsa_cre_by;
        private DateTime _mbsa_cre_dt;
        private string _mbsa_mod_by;
        private DateTime _mbsa_mod_dt;
        private string _mbsa_sa_tp;
        private string _mbsa_tp;
        #endregion

        public Boolean Mbsa_act
        {
            get { return _mbsa_act; }
            set { _mbsa_act = value; }
        }
        public string Mbsa_cd
        {
            get { return _mbsa_cd; }
            set { _mbsa_cd = value; }
        }
        public string Mbsa_com
        {
            get { return _mbsa_com; }
            set { _mbsa_com = value; }
        }
        public string Mbsa_cre_by
        {
            get { return _mbsa_cre_by; }
            set { _mbsa_cre_by = value; }
        }
        public DateTime Mbsa_cre_dt
        {
            get { return _mbsa_cre_dt; }
            set { _mbsa_cre_dt = value; }
        }
        public string Mbsa_mod_by
        {
            get { return _mbsa_mod_by; }
            set { _mbsa_mod_by = value; }
        }
        public DateTime Mbsa_mod_dt
        {
            get { return _mbsa_mod_dt; }
            set { _mbsa_mod_dt = value; }
        }
        public string Mbsa_sa_tp
        {
            get { return _mbsa_sa_tp; }
            set { _mbsa_sa_tp = value; }
        }
        public string Mbsa_tp
        {
            get { return _mbsa_tp; }
            set { _mbsa_tp = value; }
        }

        //updated by akila 2017/10/26
        public DateTime? Mbsa_valid_frm_dt { get; set; }
        public DateTime? Mbsa_valid_to_dt { get; set; }
        public static MasterBusinessEntitySalesType Converter(DataRow row)
        {
            return new MasterBusinessEntitySalesType
            {
                Mbsa_act = row["MBSA_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MBSA_ACT"]),
                Mbsa_cd = row["MBSA_CD"] == DBNull.Value ? string.Empty : row["MBSA_CD"].ToString(),
                Mbsa_com = row["MBSA_COM"] == DBNull.Value ? string.Empty : row["MBSA_COM"].ToString(),
                Mbsa_cre_by = row["MBSA_CRE_BY"] == DBNull.Value ? string.Empty : row["MBSA_CRE_BY"].ToString(),
                Mbsa_cre_dt = row["MBSA_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBSA_CRE_DT"]),
                Mbsa_mod_by = row["MBSA_MOD_BY"] == DBNull.Value ? string.Empty : row["MBSA_MOD_BY"].ToString(),
                Mbsa_mod_dt = row["MBSA_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBSA_MOD_DT"]),
                Mbsa_sa_tp = row["MBSA_SA_TP"] == DBNull.Value ? string.Empty : row["MBSA_SA_TP"].ToString(),
                Mbsa_tp = row["MBSA_TP"] == DBNull.Value ? string.Empty : row["MBSA_TP"].ToString(),
                Mbsa_valid_frm_dt = row["MBSA_VALID_FRM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBSA_VALID_FRM_DT"]),
                Mbsa_valid_to_dt = row["MBSA_VALID_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBSA_VALID_TO_DT"])
            };
        }

    }
}
