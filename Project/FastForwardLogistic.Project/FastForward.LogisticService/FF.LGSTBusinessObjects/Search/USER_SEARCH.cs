using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Search
{
    public class USER_SEARCH
    {
        public string se_usr_id { get; set; }
        public string se_usr_name { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static USER_SEARCH Converter(DataRow row)
        {
            return new USER_SEARCH
            {
                se_usr_id = row["se_usr_id"] == DBNull.Value ? string.Empty : row["se_usr_id"].ToString(),
                //met_trans_sbtp = row["met_trans_sbtp"] == DBNull.Value ? string.Empty : row["met_trans_sbtp"].ToString(),
                se_usr_name = row["se_usr_name"] == DBNull.Value ? string.Empty : row["se_usr_name"].ToString(),
                //met_act = row["met_act"] == DBNull.Value ? 0 : Convert.ToInt32(row["met_act"].ToString()),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }
}
