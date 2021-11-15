using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

//by darshana 27/08/2012
namespace FF.BusinessObjects
{
    [Serializable]
    public class HpSchemeType
    {
        #region Private Members
        private Boolean _hst_act;
        private string _hst_cd;
        private decimal _hst_def_intr;
        private string _hst_desc;
        private string _hst_sch_cat;
        #endregion

        public Boolean Hst_act
        {
            get { return _hst_act; }
            set { _hst_act = value; }
        }
        public string Hst_cd
        {
            get { return _hst_cd; }
            set { _hst_cd = value; }
        }
        public decimal Hst_def_intr
        {
            get { return _hst_def_intr; }
            set { _hst_def_intr = value; }
        }
        public string Hst_desc
        {
            get { return _hst_desc; }
            set { _hst_desc = value; }
        }
        public string Hst_sch_cat
        {
            get { return _hst_sch_cat; }
            set { _hst_sch_cat = value; }
        }

        public static HpSchemeType Converter(DataRow row)
        {
            return new HpSchemeType
            {
                Hst_act = row["HST_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["HST_ACT"]),
                Hst_cd = row["HST_CD"] == DBNull.Value ? string.Empty : row["HST_CD"].ToString(),
                Hst_def_intr = row["HST_DEF_INTR"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HST_DEF_INTR"]),
                Hst_desc = row["HST_DESC"] == DBNull.Value ? string.Empty : row["HST_DESC"].ToString(),
                Hst_sch_cat = row["HST_SCH_CAT"] == DBNull.Value ? string.Empty : row["HST_SCH_CAT"].ToString()

            };
        }


    }
}
