using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class BRND_NEW_STUS
    {
        public string itm_stus_cd { get; set; }
        public string itm_stus_desc { get; set; }
        public Int32  check { get; set; }
        public static BRND_NEW_STUS Converter(DataRow row)
        {
            return new BRND_NEW_STUS
            {
                itm_stus_cd = row["code"] == DBNull.Value ? string.Empty : row["code"].ToString(),
                itm_stus_desc = row["description"] == DBNull.Value ? string.Empty : row["description"].ToString(),
            };
        } 
    }
}
