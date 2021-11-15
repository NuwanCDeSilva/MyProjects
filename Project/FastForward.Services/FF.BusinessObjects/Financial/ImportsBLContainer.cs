using System;
using System.Data;

namespace FF.BusinessObjects
{

    //===========================================================================================================
    // This code is generated by Code gen V.1 
    // Computer :- ITPD11  | User :- suneth On 09-Jul-2015 03:29:31
    //===========================================================================================================

    public class ImportsBLContainer
    {
        public Int32 Ibc_seq_no { get; set; }
        public Int32 Ibc_line { get; set; }
        public String Ibc_doc_no { get; set; }
        public String Ibc_tp { get; set; }
        public String Ibc_desc { get; set; }
        public Int32 Ibc_act { get; set; }
        public String Ibc_cre_by { get; set; }
        public DateTime Ibc_cre_dt { get; set; }
        public Int32 Ibc_unit { get; set; }
        public static ImportsBLContainer Converter(DataRow row)
        {
            return new ImportsBLContainer
            {
                Ibc_seq_no = row["IBC_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["IBC_SEQ_NO"].ToString()),
                Ibc_line = row["IBC_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IBC_LINE"].ToString()),
                Ibc_doc_no = row["IBC_DOC_NO"] == DBNull.Value ? string.Empty : row["IBC_DOC_NO"].ToString(),
                Ibc_tp = row["IBC_TP"] == DBNull.Value ? string.Empty : row["IBC_TP"].ToString(),
                Ibc_desc = row["IBC_DESC"] == DBNull.Value ? string.Empty : row["IBC_DESC"].ToString(),
                Ibc_act = row["IBC_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["IBC_ACT"].ToString()),
                Ibc_cre_by = row["IBC_CRE_BY"] == DBNull.Value ? string.Empty : row["IBC_CRE_BY"].ToString(),
                Ibc_cre_dt = row["IBC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IBC_CRE_DT"].ToString())
            };
        }
        public static ImportsBLContainer Converter_ord(DataRow row)
        {
            return new ImportsBLContainer
            {
                Ibc_seq_no = row["IOC_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["IOC_SEQ_NO"].ToString()),
                Ibc_line = row["IOC_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IOC_LINE"].ToString()),
                Ibc_doc_no = row["IOC_DOC_NO"] == DBNull.Value ? string.Empty : row["IOC_DOC_NO"].ToString(),
                Ibc_tp = row["IOC_TP"] == DBNull.Value ? string.Empty : row["IOC_TP"].ToString(),
                Ibc_desc = row["IOC_DESC"] == DBNull.Value ? string.Empty : row["IOC_DESC"].ToString(),
                Ibc_unit = row["IOC_UNITS"] == DBNull.Value ? 0 : Convert.ToInt32(row["IOC_UNITS"].ToString()),
                Ibc_act = row["IOC_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["IOC_ACT"].ToString())
            };
        }
    }
}

