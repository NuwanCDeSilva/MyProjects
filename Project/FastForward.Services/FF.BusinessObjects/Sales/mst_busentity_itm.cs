using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects.Sales
{
   public class  mst_busentity_itm
    {  
        public Int32 mbii_log_seq { get; set; }
        public String mbii_com { get; set; }
        public String mbii_cd { get; set; }
        public String mbii_tp { get; set; }
        public String mbii_itm_cd { get; set; }
        public String mbii_cre_by { get; set; }
        public String mbii_mod_by { get; set; }
        public String mbii_plu_cd { get; set; }
        public DateTime mbii_cre_dt { get; set; }
        public DateTime mbii_mod_dt { get; set; }
        public Int32 mbii_act { get; set; }
        public String tmp_err_desc { get; set; }
        public String mbii_warr_rmk { get; set; }
        public Int32 mbii_warr_peri{ get; set; }
                           
        

        public static mst_busentity_itm Converter(DataRow row)
        {
            return new mst_busentity_itm
            {
                mbii_log_seq = row["MBII_LOG_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBII_LOG_SEQ"].ToString()),
                mbii_com = row["MBII_COM"] == DBNull.Value ? string.Empty : row["MBII_COM"].ToString(),
                mbii_cd = row["MBII_CD"] == DBNull.Value ? string.Empty : row["MBII_CD"].ToString(),
                mbii_tp = row["MBII_TP"] == DBNull.Value ? string.Empty : row["MBII_TP"].ToString(),
                mbii_itm_cd = row["MBII_ITM_CD"] == DBNull.Value ? string.Empty : row["MBII_ITM_CD"].ToString(),
                mbii_cre_by = row["MBII_CRE_BY"] == DBNull.Value ? string.Empty : row["MBII_CRE_BY"].ToString(),
                mbii_mod_by = row["MBII_MOD_BY"] == DBNull.Value ? string.Empty : row["MBII_MOD_BY"].ToString(),
                mbii_plu_cd = row["MBII_PLU_CD"] == DBNull.Value ? string.Empty : row["MBII_PLU_CD"].ToString(),
                mbii_cre_dt = row["MBII_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBII_CRE_DT"].ToString()),
                mbii_mod_dt = row["MBII_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBII_MOD_DT"].ToString()),
                mbii_act = row["MBII_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBII_ACT"].ToString()),
                //mbii_warr_peri = row["mbii_warr_peri"] == DBNull.Value ? 0 : Convert.ToInt32(row["mbii_warr_peri"].ToString()),
                //mbii_warr_rmk = row["mbii_warr_rmk"] == DBNull.Value ? string.Empty : row["mbii_warr_rmk"].ToString(),
                
            };
        }
    }
}