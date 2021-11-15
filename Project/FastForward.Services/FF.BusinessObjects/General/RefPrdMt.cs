using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    public class RefPrdMt
    {
        public Int32 Prd_seq_no { get; set; }
        public String Prd_com_cd { get; set; }
        public DateTime Prd_from { get; set; }
        public DateTime Prd_to { get; set; }
        public String Prd_stus { get; set; }
        public String Prd_cre_by { get; set; }
        public DateTime Prd_cre_when { get; set; }
        public String Prd_mod_by { get; set; }
        public DateTime Prd_mod_when { get; set; }
        public String Prd_session_id { get; set; }
        public static RefPrdMt Converter(DataRow row)
        {
            return new RefPrdMt
            {
                Prd_seq_no = row["PRD_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["PRD_SEQ_NO"].ToString()),
                Prd_com_cd = row["PRD_COM_CD"] == DBNull.Value ? string.Empty : row["PRD_COM_CD"].ToString(),
                Prd_from = row["PRD_FROM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["PRD_FROM"].ToString()),
                Prd_to = row["PRD_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["PRD_TO"].ToString()),
                Prd_stus = row["PRD_STUS"] == DBNull.Value ? string.Empty : row["PRD_STUS"].ToString(),
                Prd_cre_by = row["PRD_CRE_BY"] == DBNull.Value ? string.Empty : row["PRD_CRE_BY"].ToString(),
                Prd_cre_when = row["PRD_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["PRD_CRE_WHEN"].ToString()),
                Prd_mod_by = row["PRD_MOD_BY"] == DBNull.Value ? string.Empty : row["PRD_MOD_BY"].ToString(),
                Prd_mod_when = row["PRD_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["PRD_MOD_WHEN"].ToString()),
                Prd_session_id = row["PRD_SESSION_ID"] == DBNull.Value ? string.Empty : row["PRD_SESSION_ID"].ToString()
            };
        }
    }
}
