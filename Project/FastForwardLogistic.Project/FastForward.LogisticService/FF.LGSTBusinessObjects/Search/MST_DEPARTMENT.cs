using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Search
{
    public class MST_DEPARTMENT
    {

        public string Departcode { get; set; }
        public string DepartDesc{ get; set; }
        public string DepartHead { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static MST_DEPARTMENT Converter(DataRow row)
        {
            return new MST_DEPARTMENT
            {
                Departcode = row["msdt_cd"] == DBNull.Value ? string.Empty : row["msdt_cd"].ToString(),
                DepartDesc = row["msdt_desc"] == DBNull.Value ? string.Empty : row["msdt_desc"].ToString(),
                DepartHead = row["msdt_hod"] == DBNull.Value ? string.Empty : row["msdt_hod"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }
}
