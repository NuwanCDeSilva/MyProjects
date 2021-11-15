using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class HpProofDoc
    {
        #region Private Members
        private string _hsp_cre_by;
        private DateTime _hsp_cre_dt;
        private Boolean _hsp_is_required;
        private int _hsp_prd_cd;
        private string _hsp_sch_cd;
        #endregion

        public string Hsp_cre_by
        {
            get { return _hsp_cre_by; }
            set { _hsp_cre_by = value; }
        }
        public DateTime Hsp_cre_dt
        {
            get { return _hsp_cre_dt; }
            set { _hsp_cre_dt = value; }
        }
        public Boolean Hsp_is_required
        {
            get { return _hsp_is_required; }
            set { _hsp_is_required = value; }
        }
        public int Hsp_prd_cd
        {
            get { return _hsp_prd_cd; }
            set { _hsp_prd_cd = value; }
        }
        public string Hsp_sch_cd
        {
            get { return _hsp_sch_cd; }
            set { _hsp_sch_cd = value; }
        }

        public static HpProofDoc Converter(DataRow row)
        {
            return new HpProofDoc
            {
                Hsp_cre_by = row["HSP_CRE_BY"] == DBNull.Value ? string.Empty : row["HSP_CRE_BY"].ToString(),
                Hsp_cre_dt = row["HSP_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HSP_CRE_DT"]),
                Hsp_is_required = row["HSP_IS_REQUIRED"] == DBNull.Value ? false : Convert.ToBoolean(row["HSP_IS_REQUIRED"]),
                Hsp_prd_cd = row["HSP_PRD_CD"] == DBNull.Value ? 0 : Convert.ToInt16(row["HSP_PRD_CD"]),
                Hsp_sch_cd = row["HSP_SCH_CD"] == DBNull.Value ? string.Empty : row["HSP_SCH_CD"].ToString()

            };
        }

    }
}
