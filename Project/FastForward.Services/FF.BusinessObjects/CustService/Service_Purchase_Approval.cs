using System;
using System.Data;

namespace FF.BusinessObjects
{

    public class Service_Purchase_Approval
    {
        public Int32 Posa_seq { get; set; }
        public Int32 Posa_po_seq { get; set; }
        public String Posa_po_no { get; set; }
        public String Posa_po_itm { get; set; }
        public Int32 Posa_po_itm_line { get; set; }
        public Int32 Posa_po_del_line { get; set; }
        public String Posa_job_no { get; set; }
        public String Posa_job_itm { get; set; }
        public Int32 Posa_job_line { get; set; }
        public Decimal Posa_qty { get; set; }
        public Decimal Posa_unit_cost { get; set; }
        public String Posa_cre_by { get; set; }
        public DateTime Posa_cre_dt { get; set; }
        public String Posa_mod_by { get; set; }
        public DateTime Posa_mod_dt { get; set; }
        public Int32 Posa_act { get; set; }
        public DateTime Posa_app_dt { get; set; }

        public static Service_Purchase_Approval Converter(DataRow row)
        {
            return new Service_Purchase_Approval
            {
                Posa_seq = row["POSA_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["POSA_SEQ"].ToString()),
                Posa_po_seq = row["POSA_PO_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["POSA_PO_SEQ"].ToString()),
                Posa_po_no = row["POSA_PO_NO"] == DBNull.Value ? string.Empty : row["POSA_PO_NO"].ToString(),
                Posa_po_itm = row["POSA_PO_ITM"] == DBNull.Value ? string.Empty : row["POSA_PO_ITM"].ToString(),
                Posa_po_itm_line = row["POSA_PO_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["POSA_PO_ITM_LINE"].ToString()),
                Posa_po_del_line = row["POSA_PO_DEL_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["POSA_PO_DEL_LINE"].ToString()),
                Posa_job_no = row["POSA_JOB_NO"] == DBNull.Value ? string.Empty : row["POSA_JOB_NO"].ToString(),
                Posa_job_itm = row["POSA_JOB_ITM"] == DBNull.Value ? string.Empty : row["POSA_JOB_ITM"].ToString(),
                Posa_job_line = row["POSA_JOB_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["POSA_JOB_LINE"].ToString()),
                Posa_qty = row["POSA_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["POSA_QTY"].ToString()),
                Posa_unit_cost = row["POSA_UNIT_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["POSA_UNIT_COST"].ToString()),
                Posa_cre_by = row["POSA_CRE_BY"] == DBNull.Value ? string.Empty : row["POSA_CRE_BY"].ToString(),
                Posa_cre_dt = row["POSA_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["POSA_CRE_DT"].ToString()),
                Posa_mod_by = row["POSA_MOD_BY"] == DBNull.Value ? string.Empty : row["POSA_MOD_BY"].ToString(),
                Posa_mod_dt = row["POSA_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["POSA_MOD_DT"].ToString()),
                Posa_act = row["POSA_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["POSA_ACT"].ToString()),
                Posa_app_dt = row["POSA_APP_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["POSA_APP_DT"].ToString()),
            };
        }
    }
}

