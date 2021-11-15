using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
   public class MST_GNR_ACC
    {
        public String ACC_CD { get; set; }
        public String ACC_TP { get; set; }
        public string ACC_SUB_TP { get; set; }
        public String ACC_ENTRY_REF { get; set; }
        public String ACC_JOURNAL_SOURCE { get; set; }
        public string ACC_JOURNAL_TP { get; set; }
        public String ACC_JOURNAL_TP_DESC { get; set; }
        public String ACC_JOURNAL_TXN_REF { get; set; }

        public static MST_GNR_ACC Converter(DataRow row)
        {
            return new MST_GNR_ACC
            {

                ACC_CD = row["ACC_CD"] == DBNull.Value ? string.Empty : row["ACC_CD"].ToString(),
                ACC_TP = row["ACC_TP"] == DBNull.Value ? string.Empty : row["ACC_TP"].ToString(),
                ACC_SUB_TP = row["ACC_SUB_TP"] == DBNull.Value ? string.Empty : row["ACC_SUB_TP"].ToString(),
                ACC_ENTRY_REF = row["ACC_ENTRY_REF"] == DBNull.Value ? string.Empty : row["ACC_ENTRY_REF"].ToString(),
                ACC_JOURNAL_SOURCE = row["ACC_JOURNAL_SOURCE"] == DBNull.Value ? string.Empty : row["ACC_JOURNAL_SOURCE"].ToString(),
                ACC_JOURNAL_TP = row["ACC_JOURNAL_TP"] == DBNull.Value ? string.Empty : row["ACC_JOURNAL_TP"].ToString(),
                ACC_JOURNAL_TP_DESC = row["ACC_JOURNAL_TP_DESC"] == DBNull.Value ? string.Empty : row["ACC_JOURNAL_TP_DESC"].ToString(),
                ACC_JOURNAL_TXN_REF = row["ACC_JOURNAL_TXN_REF"] == DBNull.Value ? string.Empty : row["ACC_JOURNAL_TXN_REF"].ToString(),
             
            };
        }
    }
}
