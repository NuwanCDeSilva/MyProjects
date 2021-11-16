using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Search
{
   public class MST_CUR_SEARCH
    {
        public String MCR_CD { get; set; }
        public String MCR_DESC { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public static MST_CUR_SEARCH Converter(DataRow row)
        {
            return new MST_CUR_SEARCH
            {
                MCR_CD = row["MCR_CD"] == DBNull.Value ? string.Empty : row["MCR_CD"].ToString(),
                MCR_DESC = row["MCR_DESC"] == DBNull.Value ? string.Empty : row["MCR_DESC"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }
}
