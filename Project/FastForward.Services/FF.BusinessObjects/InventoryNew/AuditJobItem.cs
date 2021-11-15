using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
    public class AuditJobItem
    {
        public string Audji_cre_by { get; set; }

        public DateTime Audji_cre_dt { get; set; }

        public string Audji_job { get; set; }

        public decimal Audji_db_qty { get; set; }
        
        public string Audji_itm { get; set; }

        public string Audji_stus { get; set; }

        public decimal Audji_ledger_qty { get; set; }

        public decimal Audji_physical_qty { get; set; }
        public Int32 Audji_seq { get; set; }

        
        public string Audji_desc { get; set; }

        //Akila 2017/04/28
        public string Audji_session_id { get; set; }

        public static AuditJobItem Converter(DataRow row)
        {
            return new AuditJobItem
            {
                Audji_cre_by = row["AUDJI_CRE_BY"] == DBNull.Value ? string.Empty : row["AUDJI_CRE_BY"].ToString(),
                Audji_cre_dt = row["AUDJI_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AUDJI_CRE_DT"]),
                Audji_db_qty = row["AUDJI_DB_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AUDJI_DB_QTY"]),
                Audji_itm = row["AUDJI_ITMCD"] == DBNull.Value ? string.Empty : row["AUDJI_ITMCD"].ToString(),
                Audji_job = row["AUDJI_JOB"] == DBNull.Value ? string.Empty : row["AUDJI_JOB"].ToString(),
                Audji_ledger_qty = row["AUDJI_LEDGER_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AUDJI_LEDGER_QTY"]),
                Audji_physical_qty = row["AUDJI_PHYSIC_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AUDJI_PHYSIC_QTY"]),
                Audji_seq = row["AUDJI_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["AUDJI_SEQ"]),
                Audji_stus = row["AUDJI_ITMST"] == DBNull.Value ? string.Empty : row["AUDJI_ITMST"].ToString()
            };
        }
    }
}
