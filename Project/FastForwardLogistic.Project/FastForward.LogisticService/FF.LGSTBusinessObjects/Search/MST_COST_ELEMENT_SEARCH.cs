using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Search
{
   public class MST_COST_ELEMENT_SEARCH
    {
        public String MCE_CD { get; set; }
        public String MCE_DESC { get; set; }
        public String MCE_ACC_CD { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public static MST_COST_ELEMENT_SEARCH Converter(DataRow row)
        {
            return new MST_COST_ELEMENT_SEARCH
            {
                MCE_CD = row["MCE_CD"] == DBNull.Value ? string.Empty : row["MCE_CD"].ToString(),
                MCE_DESC = row["MCE_DESC"] == DBNull.Value ? string.Empty : row["MCE_DESC"].ToString(),
                MCE_ACC_CD = row["MCE_ACC_CD"] == DBNull.Value ? string.Empty : row["MCE_ACC_CD"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }
}
