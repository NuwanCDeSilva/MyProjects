using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Search
{
    public class MST_DESIGNATION
    {
        public string DesignationCat { get; set; }
        public string DesignationDesc { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static MST_DESIGNATION Converter(DataRow row)
        {
            return new MST_DESIGNATION
            {
                DesignationCat = row["mec_cat"] == DBNull.Value ? string.Empty : row["mec_cat"].ToString(),
                DesignationDesc = row["mec_desc"] == DBNull.Value ? string.Empty : row["mec_desc"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }
}
