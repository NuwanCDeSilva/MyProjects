using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    //Table :hpr_flag_bank
    [Serializable]
   public class HPR_FlagBank
    {
        #region Private Members 
        private string _hfb_cd;
        private string _hfb_tp;
        private string _hpf_cre_by;
        private DateTime _hpf_cre_dt;
        private string _hpf_desc;
        #endregion

        public string Hfb_cd { get { return _hfb_cd; } set { _hfb_cd = value; } }
        public string Hfb_tp { get { return _hfb_tp; } set { _hfb_tp = value; } }
        public string Hpf_cre_by { get { return _hpf_cre_by; } set { _hpf_cre_by = value; } }
        public DateTime Hpf_cre_dt { get { return _hpf_cre_dt; } set { _hpf_cre_dt = value; } }
        public string Hpf_desc { get { return _hpf_desc; } set { _hpf_desc = value; } }

        public static HPR_FlagBank Converter(DataRow row)
        {
            return new HPR_FlagBank
            {
                Hfb_cd = row["HFB_CD"] == DBNull.Value ? string.Empty : row["HFB_CD"].ToString(),
                Hfb_tp = row["HFB_TP"] == DBNull.Value ? string.Empty : row["HFB_TP"].ToString(),
                Hpf_cre_by = row["HPF_CRE_BY"] == DBNull.Value ? string.Empty : row["HPF_CRE_BY"].ToString(),
                Hpf_cre_dt = row["HPF_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPF_CRE_DT"]),
                Hpf_desc = row["HPF_DESC"] == DBNull.Value ? string.Empty : row["HPF_DESC"].ToString()

            };
        }

    }
}
