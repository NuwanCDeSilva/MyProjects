using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class MAIN_CAT_SEARCH
    {
        public string main_cat_cd { get; set; }
        public string main_cat_desc { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public Int32 SELECT { get; set; }
        public static MAIN_CAT_SEARCH Converter(DataRow row)
        {
            return new MAIN_CAT_SEARCH
            {
                main_cat_cd = row["code"] == DBNull.Value ? string.Empty : row["code"].ToString(),
                main_cat_desc = row["description"] == DBNull.Value ? string.Empty : row["description"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        } 
    }
    public class ITEM_MAINCAT_SELECTED
    {
        public string main_cat_cd { get; set; }
        public string main_cat_desc { get; set; }
    }
    public class SCHMTYP_SELECTED
    {
        public string main_cat_cd { get; set; }
        public string main_cat_desc { get; set; }
    }
    public class SCHMTCD_SELECTED
    {
        public string main_cat_cd { get; set; }
        public string main_cat_desc { get; set; }
    }
    public class PAYTP_SELECTED
    {
        public string main_cat_cd { get; set; }
        public string main_cat_desc { get; set; }
    }
}
