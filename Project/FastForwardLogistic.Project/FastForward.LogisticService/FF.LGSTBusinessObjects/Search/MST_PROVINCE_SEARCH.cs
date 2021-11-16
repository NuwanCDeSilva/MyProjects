using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace FF.BusinessObjects.Search
{
    public class MST_PROVINCE_SEARCH
    {
        public string mpro_cd { get; set; }
        public string mpro_desc { get; set; }
        //public string mpro_desc { get; set; }
        //public string mt_cd { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static MST_PROVINCE_SEARCH Converter(DataRow row)
        {
            return new MST_PROVINCE_SEARCH
            {
                mpro_cd = row["mpro_cd"] == DBNull.Value ? string.Empty : row["mpro_cd"].ToString(),
                mpro_desc = row["mpro_desc"] == DBNull.Value ? string.Empty : row["mpro_desc"].ToString(),
                //mt_cd = row["mt_cd"] == DBNull.Value ? string.Empty : row["mt_cd"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }
    public class MST_PROVINCE_SELECTED
    {
        public string mpro_cd { get; set; }
        public string mpro_desc { get; set; }
    }
}
