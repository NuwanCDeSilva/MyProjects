using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
  
    public class ITEM_MODEL_SEARCH
    {
        public string mm_cd { get; set; }
        public string mm_desc { get; set; }
        public string MM_CAT1{get;set;}
        public string MM_BRAND { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public Int32 SELECT { get; set; }
        public static ITEM_MODEL_SEARCH Converter(DataRow row)
        {
            return new ITEM_MODEL_SEARCH
            {
                mm_cd = row["code"] == DBNull.Value ? string.Empty : row["code"].ToString(),
                mm_desc = row["description"] == DBNull.Value ? string.Empty : row["description"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
        public static ITEM_MODEL_SEARCH ConverterItem(DataRow row)
        {
            return new ITEM_MODEL_SEARCH
            {
                mm_cd = row["mm_cd"] == DBNull.Value ? string.Empty : row["mm_cd"].ToString(),
                mm_desc = row["mm_desc"] == DBNull.Value ? string.Empty : row["mm_desc"].ToString(),
                MM_CAT1 = row["MM_CAT1"] == DBNull.Value ? string.Empty : row["MM_CAT1"].ToString(),
                MM_BRAND = row["MM_BRAND"] == DBNull.Value ? string.Empty : row["MM_BRAND"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }

    }
    public class ITEM_MODEL_SELECTED
    {
        public string mm_cd { get; set; }
        public string mm_desc { get; set; }
    }
}
