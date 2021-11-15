using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
    public class DisposalCurrStatus
    {
        public Int32 Ids_seq { get; set; }
        public String Ids_doc_no { get; set; }
        public String Ids_stus { get; set; }
        public Int32 Ids_act { get; set; }
        public String Ids_cre_by { get; set; }
        public DateTime Ids_cre_dt { get; set; }
        public String Ids_mod_by { get; set; }
        public DateTime Ids_mod_dt { get; set; }

        //Additional
        public string Ids_Stus_desc { get; set; }

        public static DisposalCurrStatus Converter(DataRow row)
        {
            return new DisposalCurrStatus
            {
                Ids_seq = row["IDS_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["IDS_SEQ"].ToString()),
                Ids_doc_no = row["IDS_DOC_NO"] == DBNull.Value ? string.Empty : row["IDS_DOC_NO"].ToString(),
                Ids_stus = row["IDS_STUS"] == DBNull.Value ? string.Empty : row["IDS_STUS"].ToString(),
                Ids_act = row["IDS_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["IDS_ACT"].ToString()),
                Ids_cre_by = row["IDS_CRE_BY"] == DBNull.Value ? string.Empty : row["IDS_CRE_BY"].ToString(),
                Ids_cre_dt = row["IDS_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IDS_CRE_DT"].ToString()),
                Ids_mod_by = row["IDS_MOD_BY"] == DBNull.Value ? string.Empty : row["IDS_MOD_BY"].ToString(),
                Ids_mod_dt = row["IDS_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IDS_MOD_DT"].ToString()),
                Ids_Stus_desc = row["MIS_DESC"] == DBNull.Value ? string.Empty : row["MIS_DESC"].ToString()
            };
        }
    }

}
