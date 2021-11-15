using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class MasterProofDocs
    {
        #region Private Members
        private string _hpd_cre_by;
        private DateTime _hpd_cre_dt;
        private string _hpd_desc;
        private int _hpd_prd_cd;
        #endregion

        public string Hpd_cre_by
        {
            get { return _hpd_cre_by; }
            set { _hpd_cre_by = value; }
        }
        public DateTime Hpd_cre_dt
        {
            get { return _hpd_cre_dt; }
            set { _hpd_cre_dt = value; }
        }
        public string Hpd_desc
        {
            get { return _hpd_desc; }
            set { _hpd_desc = value; }
        }
        public int Hpd_prd_cd
        {
            get { return _hpd_prd_cd; }
            set { _hpd_prd_cd = value; }
        }

        public static MasterProofDocs Converter(DataRow row)
        {
            return new MasterProofDocs
            {
                Hpd_cre_by = row["HPD_CRE_BY"] == DBNull.Value ? string.Empty : row["HPD_CRE_BY"].ToString(),
                Hpd_cre_dt = row["HPD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPD_CRE_DT"]),
                Hpd_desc = row["HPD_DESC"] == DBNull.Value ? string.Empty : row["HPD_DESC"].ToString(),
                Hpd_prd_cd = row["HPD_PRD_CD"] == DBNull.Value ? 0 : Convert.ToInt16(row["HPD_PRD_CD"])

            };
        }

    }
}
