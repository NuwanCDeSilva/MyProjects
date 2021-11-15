using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Asycuda
{
    public class ASY_XML_TAG
    {
        public decimal Axt_seq { get; set; }
        public String Axt_tag_name { get; set; }
        public decimal Axt_possision_id { get; set; }
        public decimal Axt_parent_id { get; set; }
        public decimal Axt_level_id { get; set; }
        public decimal Axt_act { get; set; }
        public decimal Axt_column_id { get; set; }
        public string Axt_column_name { get; set; }

        public static ASY_XML_TAG Converter(DataRow row)
        {
            return new ASY_XML_TAG
            {
                Axt_seq = row["AXT_SEQ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AXT_SEQ"].ToString()),
                Axt_tag_name = row["AXT_TAG_NAME"] == DBNull.Value ? string.Empty : row["AXT_TAG_NAME"].ToString(),
                Axt_possision_id = row["AXT_POSSISION_ID"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AXT_POSSISION_ID"].ToString()),
                Axt_parent_id = row["AXT_PARENT_ID"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AXT_PARENT_ID"].ToString()),
                Axt_level_id = row["AXT_LEVEL_ID"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AXT_LEVEL_ID"].ToString()),
                Axt_act = row["AXT_ACT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AXT_ACT"].ToString()),
                Axt_column_id = row["AXT_COLUMN_ID"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AXT_COLUMN_ID"].ToString()),
                Axt_column_name = row["AXT_COLUMN_NAME"] == DBNull.Value ? string.Empty : row["AXT_COLUMN_NAME"].ToString()
            };
        }
    }
}
