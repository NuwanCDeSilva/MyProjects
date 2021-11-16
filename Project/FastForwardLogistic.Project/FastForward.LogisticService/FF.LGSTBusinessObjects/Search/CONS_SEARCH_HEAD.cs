using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Search
{
    public class CONS_SEARCH_HEAD
    {
        public String Mbe_acc_cd { get; set; }
        public String Mbe_cd { get; set; }
        public String Mbe_name { get; set; }
        public String Mbe_add1 { get; set; }
        public String Mbe_mob { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static CONS_SEARCH_HEAD Converter(DataRow row)
        {
            return new CONS_SEARCH_HEAD
            {
                Mbe_acc_cd = row["Mbe_acc_cd"] == DBNull.Value ? string.Empty : row["Mbe_acc_cd"].ToString(),
                Mbe_cd = row["Mbe_cd"] == DBNull.Value ? string.Empty : row["Mbe_cd"].ToString(),
                Mbe_name = row["Mbe_name"] == DBNull.Value ? string.Empty : row["Mbe_name"].ToString(),
                Mbe_add1 = row["Mbe_add1"] == DBNull.Value ? string.Empty : row["Mbe_add1"].ToString(),
                Mbe_mob = row["Mbe_mob"] == DBNull.Value ? string.Empty : row["Mbe_mob"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }
}
