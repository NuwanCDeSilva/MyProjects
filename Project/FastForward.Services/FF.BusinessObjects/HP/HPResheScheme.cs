using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

//darshana on 22-10-2012

namespace FF.BusinessObjects
{
    [Serializable]
    public class HPResheScheme
    {
        #region Private Members
        private string _hsr_rsch_cd;
        private string _hsr_sch_cd;
        #endregion

        public string Hsr_rsch_cd
        {
            get { return _hsr_rsch_cd; }
            set { _hsr_rsch_cd = value; }
        }
        public string Hsr_sch_cd
        {
            get { return _hsr_sch_cd; }
            set { _hsr_sch_cd = value; }
        }

        public static HPResheScheme Converter(DataRow row)
        {
            return new HPResheScheme
            {
                Hsr_rsch_cd = row["HSR_RSCH_CD"] == DBNull.Value ? string.Empty : row["HSR_RSCH_CD"].ToString(),
                Hsr_sch_cd = row["HSR_SCH_CD"] == DBNull.Value ? string.Empty : row["HSR_SCH_CD"].ToString()

            };
        }
    }
}
