using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
   public class GV_SEARCH_HEAD
    {
       public string Item { get; set; }
       public string Description { get; set; }
       public string Model { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public static GV_SEARCH_HEAD Converter(DataRow row)
        {
            return new GV_SEARCH_HEAD
            {
                Item = row["Item"] == DBNull.Value ? string.Empty : row["Item"].ToString(),
                Description = row["Description"] == DBNull.Value ? string.Empty : row["Description"].ToString(),
                Model = row["Model"] == DBNull.Value ? string.Empty : row["Model"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        } 
    }
}
