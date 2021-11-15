using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class PROVINCES_LIST
    {
        public string mpro_cd { get; set; }

        public static PROVINCES_LIST Converter(DataRow row)
        {
            return new PROVINCES_LIST
            {
                mpro_cd = row["mpro_cd"] == DBNull.Value ? string.Empty : row["mpro_cd"].ToString(),
            };
        }
    }
}
