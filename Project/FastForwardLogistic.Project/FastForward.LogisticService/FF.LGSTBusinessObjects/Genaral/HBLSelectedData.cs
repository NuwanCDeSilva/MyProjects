using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Genaral
{
    public class HBLSelectedData
    {
        public string blno { get; set; }
        public string Bl_doc_no { get; set; }
        public string Bl_h_doc_no { get; set; }
        public string Bl_d_doc_no { get; set; }
        public string Bl_job_no { get; set; }
        public string Bl_pouch_no { get; set; }
        public string Bl_cus_cd { get; set; }

        // Added by Chathura on 18-sep-2017
        public static HBLSelectedData Converter(DataRow row)
        {
            return new HBLSelectedData
            {
                blno = row["BL_DOC_NO"] == DBNull.Value ? string.Empty : row["BL_DOC_NO"].ToString(),
                Bl_doc_no = row["BL_DOC_NO"] == DBNull.Value ? string.Empty : row["BL_DOC_NO"].ToString(),
                Bl_h_doc_no = row["BL_DOC_NO"] == DBNull.Value ? string.Empty : row["BL_DOC_NO"].ToString(),
                Bl_d_doc_no = row["BL_D_DOC_NO"] == DBNull.Value ? string.Empty : row["BL_D_DOC_NO"].ToString(),
                Bl_job_no = row["BL_JOB_NO"] == DBNull.Value ? string.Empty : row["BL_JOB_NO"].ToString(),
                Bl_pouch_no = row["BL_POUCH_NO"] == DBNull.Value ? string.Empty : row["BL_POUCH_NO"].ToString(),
                Bl_cus_cd = row["BL_CUS_CD"] == DBNull.Value ? string.Empty : row["BL_CUS_CD"].ToString()
            };
        }
    }
}
