using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Asycuda
{
    public class ASY_DOC_TP
    {
        public Int32 Adt_tp_id { get; set; }
        public Int32 Adt_grup_id { get; set; }
        public String Adt_tp_name { get; set; }
        public Int32 Adt_tp_act { get; set; }
        public static ASY_DOC_TP Converter(DataRow row)
        {
            return new ASY_DOC_TP
            {
                Adt_tp_id = row["ADT_TP_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ADT_TP_ID"].ToString()),
                Adt_grup_id = row["ADT_GRUP_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ADT_GRUP_ID"].ToString()),
                Adt_tp_name = row["ADT_TP_NAME"] == DBNull.Value ? string.Empty : row["ADT_TP_NAME"].ToString(),
                Adt_tp_act = row["ADT_TP_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["ADT_TP_ACT"].ToString())
            };
        } 
    }
}
