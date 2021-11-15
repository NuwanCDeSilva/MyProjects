using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

//darshana 17-01-2014
namespace FF.BusinessObjects
{
    [Serializable]
    public class MasterItemTaxClaim
    {
        #region Private Members
        private decimal _mic_claim;
        private string _mic_com;
        private string _mic_cre_by;
        private DateTime _mic_cre_dt;
        private string _mic_itm_cd;
        private Boolean _mic_stus;
        private string _mic_tax_cd;
        private decimal _mic_tax_rt;
        #endregion

        public decimal Mic_claim
        {
            get { return _mic_claim; }
            set { _mic_claim = value; }
        }
        public string Mic_com
        {
            get { return _mic_com; }
            set { _mic_com = value; }
        }
        public string Mic_cre_by
        {
            get { return _mic_cre_by; }
            set { _mic_cre_by = value; }
        }
        public DateTime Mic_cre_dt
        {
            get { return _mic_cre_dt; }
            set { _mic_cre_dt = value; }
        }
        public string Mic_itm_cd
        {
            get { return _mic_itm_cd; }
            set { _mic_itm_cd = value; }
        }
        public Boolean Mic_stus
        {
            get { return _mic_stus; }
            set { _mic_stus = value; }
        }
        public string Mic_tax_cd
        {
            get { return _mic_tax_cd; }
            set { _mic_tax_cd = value; }
        }
        public decimal Mic_tax_rt
        {
            get { return _mic_tax_rt; }
            set { _mic_tax_rt = value; }
        }

        public static MasterItemTaxClaim Converter(DataRow row)
        {
            return new MasterItemTaxClaim
            {
                Mic_claim = row["MIC_CLAIM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MIC_CLAIM"]),
                Mic_com = row["MIC_COM"] == DBNull.Value ? string.Empty : row["MIC_COM"].ToString(),
                Mic_cre_by = row["MIC_CRE_BY"] == DBNull.Value ? string.Empty : row["MIC_CRE_BY"].ToString(),
                Mic_cre_dt = row["MIC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MIC_CRE_DT"]),
                Mic_itm_cd = row["MIC_ITM_CD"] == DBNull.Value ? string.Empty : row["MIC_ITM_CD"].ToString(),
                Mic_stus = row["MIC_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["MIC_STUS"]),
                Mic_tax_cd = row["MIC_TAX_CD"] == DBNull.Value ? string.Empty : row["MIC_TAX_CD"].ToString(),
                Mic_tax_rt = row["MIC_TAX_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MIC_TAX_RT"])

            };
        }

    }
}
