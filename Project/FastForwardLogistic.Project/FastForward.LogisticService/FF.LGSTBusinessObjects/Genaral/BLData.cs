using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Genaral
{
    public class BLData
    {
        public string bl_doc_no { get; set; }
        public string bl_d_doc_no { get; set; }
        public string bl_h_doc_no { get; set; }
        public string bl_m_doc_no { get; set; }
        public string BL_MANUAL_D_REF { get; set; }
        public string BL_MANUAL_H_REF { get; set; }
        public string BL_MANUAL_M_REF { get; set; }
        public DateTime bl_est_time_arr { get; set; }
        public DateTime bl_est_time_dep { get; set; }
        
        // Added by Chathura on 18-sep-2017
        public static BLData Converter(DataRow row)
        {
            return new BLData
            {
                bl_doc_no = row["bl_doc_no"] == DBNull.Value ? string.Empty : row["bl_doc_no"].ToString(),
                bl_d_doc_no = row["bl_d_doc_no"] == DBNull.Value ? string.Empty : row["bl_d_doc_no"].ToString(),
                bl_h_doc_no = row["bl_h_doc_no"] == DBNull.Value ? string.Empty : row["bl_h_doc_no"].ToString(),
                bl_m_doc_no = row["bl_m_doc_no"] == DBNull.Value ? string.Empty : row["bl_m_doc_no"].ToString(),
                BL_MANUAL_D_REF = row["BL_MANUAL_D_REF"] == DBNull.Value ? string.Empty : row["BL_MANUAL_D_REF"].ToString(),
                BL_MANUAL_H_REF = row["BL_MANUAL_H_REF"] == DBNull.Value ? string.Empty : row["BL_MANUAL_H_REF"].ToString(),
                BL_MANUAL_M_REF = row["BL_MANUAL_M_REF"] == DBNull.Value ? string.Empty : row["BL_MANUAL_M_REF"].ToString(),
                bl_est_time_arr = row["bl_est_time_arr"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["bl_est_time_arr"].ToString()),
                bl_est_time_dep = row["bl_est_time_dep"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["bl_est_time_dep"].ToString())

            };
        }
    }
}
