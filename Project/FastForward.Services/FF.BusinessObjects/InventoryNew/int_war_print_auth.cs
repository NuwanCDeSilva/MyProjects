using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
    public class int_war_print_auth
    {
        //Create by Lakshan 16 Nov 2016
        public String Iwa_doc_no { get; set; }
        public Int32 Iwa_line { get; set; }
        public Int32 Iwa_ser_id { get; set; }
        public String Iwa_ser_1 { get; set; }
        public String Iwa_ser_2 { get; set; }
        public String Iwa_print_usr { get; set; }
        public String Iwa_cre_by { get; set; }
        public DateTime Iwa_cre_dt { get; set; }
        public String Iwa_mod_by { get; set; }
        public DateTime Iwa_mod_dt { get; set; }
        public Int32 Iwa_act { get; set; }
        public static int_war_print_auth Converter(DataRow row)
        {
            return new int_war_print_auth
            {
                Iwa_doc_no = row["IWA_DOC_NO"] == DBNull.Value ? string.Empty : row["IWA_DOC_NO"].ToString(),
                Iwa_line = row["IWA_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IWA_LINE"].ToString()),
                Iwa_ser_id = row["IWA_SER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["IWA_SER_ID"].ToString()),
                Iwa_ser_1 = row["IWA_SER_1"] == DBNull.Value ? string.Empty : row["IWA_SER_1"].ToString(),
                Iwa_ser_2 = row["IWA_SER_2"] == DBNull.Value ? string.Empty : row["IWA_SER_2"].ToString(),
                Iwa_print_usr = row["IWA_PRINT_USR"] == DBNull.Value ? string.Empty : row["IWA_PRINT_USR"].ToString(),
                Iwa_cre_by = row["IWA_CRE_BY"] == DBNull.Value ? string.Empty : row["IWA_CRE_BY"].ToString(),
                Iwa_cre_dt = row["IWA_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IWA_CRE_DT"].ToString()),
                Iwa_mod_by = row["IWA_MOD_BY"] == DBNull.Value ? string.Empty : row["IWA_MOD_BY"].ToString(),
                Iwa_mod_dt = row["IWA_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IWA_MOD_DT"].ToString()),
                Iwa_act = row["IWA_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["IWA_ACT"].ToString())
            };
        }
    }
}
