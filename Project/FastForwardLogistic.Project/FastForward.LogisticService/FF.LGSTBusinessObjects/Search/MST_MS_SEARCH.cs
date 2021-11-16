using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Search
{
    // Class added by Chathura on 13-Sep-2017
    public class MST_MS_SEARCH
    {
        public string Mms_cd { get; set; }
        public string Mms_desc { get; set; }
        public string R__ { get; set; }
        public string RESULT_COUNT { get; set; }

        public static MST_MS_SEARCH Converter(DataRow row)
        {
            return new MST_MS_SEARCH
            {
                Mms_cd = row["MSC_CD"] == DBNull.Value ? string.Empty : row["MSC_CD"].ToString(),
                Mms_desc = row["MSC_DESC"] == DBNull.Value ? string.Empty : row["MSC_DESC"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()

            };
        }
    }
    public class MST_MS_SELECTED
    {
        public string Mms_cd { get; set; }
        public string Mms_desc { get; set; }
    }
}
