using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects.General
{
   public class PLU_SEARCH_ITM
    {
        public String MI_CD { get; set; }
        public String MI_LONGDESC { get; set; }
        public String mbii_plu_cd { get; set; }
        public String MI_BRAND { get; set; }
        public String MI_MODEL { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static PLU_SEARCH_ITM Converter(DataRow row)
        {
            return new PLU_SEARCH_ITM
            {
                MI_CD = row["MI_CD"] == DBNull.Value ? string.Empty : row["MI_CD"].ToString(),
                MI_LONGDESC = row["MI_LONGDESC"] == DBNull.Value ? string.Empty : row["MI_LONGDESC"].ToString(),
                mbii_plu_cd = row["mbii_plu_cd"] == DBNull.Value ? string.Empty : row["mbii_plu_cd"].ToString(),
                MI_BRAND = row["MI_BRAND"] == DBNull.Value ? string.Empty : row["MI_BRAND"].ToString(),
                MI_MODEL = row["MI_MODEL"] == DBNull.Value ? string.Empty : row["MI_MODEL"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()

            };
        }

    }
}
