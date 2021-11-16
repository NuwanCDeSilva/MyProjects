using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Search
{
   public class UOM_SEARCH
    {
       public string PT_TP_CD { get; set; }
       public string PT_DESC { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static UOM_SEARCH Converter(DataRow row)
        {
            return new UOM_SEARCH
            {
                PT_TP_CD = row["PT_TP_CD"] == DBNull.Value ? string.Empty : row["PT_TP_CD"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                PT_DESC = row["PT_DESC"] == DBNull.Value ? string.Empty : row["PT_DESC"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString(),
            };
        }
    }
}
