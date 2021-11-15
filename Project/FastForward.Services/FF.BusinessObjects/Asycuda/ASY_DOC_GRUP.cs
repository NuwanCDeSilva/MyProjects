using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Asycuda
{
    public class ASY_DOC_GRUP
    {
        public Int32 Adg_grup_id { get; set; }
        public String Adg_grup_name { get; set; }
        public Int32 Adg_grup_act { get; set; }
        public static ASY_DOC_GRUP Converter(DataRow row)
        {
            return new ASY_DOC_GRUP
            {
                Adg_grup_id = row["ADG_GRUP_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ADG_GRUP_ID"].ToString()),
                Adg_grup_name = row["ADG_GRUP_NAME"] == DBNull.Value ? string.Empty : row["ADG_GRUP_NAME"].ToString(),
                Adg_grup_act = row["ADG_GRUP_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["ADG_GRUP_ACT"].ToString())
            };
        }
    }
}
