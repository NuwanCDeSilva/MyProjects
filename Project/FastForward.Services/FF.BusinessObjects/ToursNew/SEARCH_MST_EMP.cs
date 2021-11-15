using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
   public class SEARCH_MST_EMP
    {
        public string ESEP_CAT_CD { get; set; }
        public string ESEP_FIRST_NAME { get; set; }
        public string ESEP_CD { get; set; }
        public static SEARCH_MST_EMP Converter(DataRow row)
        {
            return new SEARCH_MST_EMP
            {
                ESEP_CAT_CD = row["ESEP_CAT_CD"] == DBNull.Value ? string.Empty : row["ESEP_CAT_CD"].ToString(),
                ESEP_FIRST_NAME = row["ESEP_FIRST_NAME"] == DBNull.Value ? string.Empty : row["ESEP_FIRST_NAME"].ToString(),
                ESEP_CD = row["ESEP_CD"] == DBNull.Value ? string.Empty : row["ESEP_CD"].ToString(),
              
            };
        }
    }
}
