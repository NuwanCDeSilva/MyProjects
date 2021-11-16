using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Genaral
{
    public class trn_bl_cont_det
    {
        public String Blct_seq_no { get; set; }
        public String Blct_bl_doc { get; set; }
        public String Blct_cont_no { get; set; }
        public string blct_con_tp { get; set; }
        public String Blct_seal_no { get; set; }
        public String Blct_fully_empty { get; set; }
        public String Blct_pack { get; set; }
        public Int32 Blct_line { get; set; }
        public static trn_bl_cont_det Converter(DataRow row)
        {
            return new trn_bl_cont_det
            {
                Blct_seq_no = row["BLCT_SEQ_NO"] == DBNull.Value ? string.Empty : row["BLCT_SEQ_NO"].ToString(),
                Blct_bl_doc = row["BLCT_BL_DOC"] == DBNull.Value ? string.Empty : row["BLCT_BL_DOC"].ToString(),
                Blct_cont_no = row["BLCT_CONT_NO"] == DBNull.Value ? string.Empty : row["BLCT_CONT_NO"].ToString(),
                Blct_seal_no = row["BLCT_SEAL_NO"] == DBNull.Value ? string.Empty : row["BLCT_SEAL_NO"].ToString(),
                Blct_fully_empty = row["BLCT_FULLY_EMPTY"] == DBNull.Value ? string.Empty : row["BLCT_FULLY_EMPTY"].ToString(),
                Blct_pack = row["BLCT_PACK"] == DBNull.Value ? string.Empty : row["BLCT_PACK"].ToString(),
                Blct_line = row["BLCT_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["BLCT_LINE"].ToString()),
                blct_con_tp = row["blct_con_tp"] == DBNull.Value ? string.Empty : row["blct_con_tp"].ToString(),
            };
        }
    }
}

