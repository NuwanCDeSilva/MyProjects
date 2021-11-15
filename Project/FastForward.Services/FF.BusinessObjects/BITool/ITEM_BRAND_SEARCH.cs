using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{

    public class ITEM_BRAND_SEARCH
    {
        public string mb_cd { get; set; }
        public string mb_desc { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public Int32 SELECT { get; set; }
        public static ITEM_BRAND_SEARCH Converter(DataRow row)
        {
            return new ITEM_BRAND_SEARCH
            {
                mb_cd = row["code"] == DBNull.Value ? string.Empty : row["code"].ToString(),
                mb_desc = row["description"] == DBNull.Value ? string.Empty : row["description"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }
    public class ITEM_BRAND_SELECTED
    {
        public string mb_cd { get; set; }
        public string mb_desc { get; set; }
    }
}
