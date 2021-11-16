using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Search
{
    public class PAY_TP_SEARCH
    {
        public string sapt_cd { get; set; }
        public string sapt_desc { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static PAY_TP_SEARCH Converter(DataRow row)
        {
            return new PAY_TP_SEARCH
            {
                sapt_cd = row["sapt_cd"] == DBNull.Value ? string.Empty : row["sapt_cd"].ToString(),
                //met_trans_sbtp = row["met_trans_sbtp"] == DBNull.Value ? string.Empty : row["met_trans_sbtp"].ToString(),
                sapt_desc = row["sapt_desc"] == DBNull.Value ? string.Empty : row["sapt_desc"].ToString(),
                //met_act = row["met_act"] == DBNull.Value ? 0 : Convert.ToInt32(row["met_act"].ToString()),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }

    }
}
