using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects.General
{
    public class BonusDefinition
    {
        public Int32 hbp_seq { get; set; }
        public String hbp_com { get; set; }
        public String hbp_circular { get; set; }
        public DateTime hbp_strdate { get; set; }
        public String hbp_pccat { get; set; }
        public Int32 hbp_arrper { get; set; }
        public Int32 hbp_sryear { get; set; }
        public DateTime hbp_frmdate { get; set; }
        public DateTime hbp_todate { get; set; }
        public Int32 hbp_taccount { get; set; }
        public Int32 hbp_accbalance { get; set; }
        public Decimal hbp_bnsper { get; set; }
        public String hbp_cre_by { get; set; }
        public DateTime hbp_cre_dt { get; set; }
        public String hbp_mod_by { get; set; }
        public DateTime hbp_mod_dt { get; set; }
        public Decimal hbp_from_arrper { get; set; }
        public Decimal hbp_to_arrper { get; set; }
        public Int32 hbp_sr_fyear { get; set; }
        public Int32 hbp_sr_tyear { get; set; }
        public Decimal hbp_from_bal { get; set; }
        public Decimal hbp_to_bal { get; set; }
        public String hbp_channel { get; set; }
        public DateTime hbp_shstrdate { get; set; }
        public String hbp_shstrab { get; set; }

        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public static BonusDefinition webConverter(DataRow row)
        {
            return new BonusDefinition
            {
                hbp_seq = row["HBP_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HBP_SEQ"].ToString()),
                hbp_com = row["HBP_COM"] == DBNull.Value ? string.Empty : row["HBP_COM"].ToString(),
                hbp_circular = row["HBP_CIRCULAR"] == DBNull.Value ? string.Empty : row["HBP_CIRCULAR"].ToString(),
                hbp_strdate = row["HBP_STRDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HBP_STRDATE"]),
                hbp_pccat = row["HBP_PCCAT"] == DBNull.Value ? string.Empty : row["HBP_PCCAT"].ToString(),
                hbp_arrper = row["HBP_ARRPER"] == DBNull.Value ? 0 : Convert.ToInt32(row["HBP_ARRPER"].ToString()),
                hbp_sryear = row["HBP_SRYEAR"] == DBNull.Value ? 0 : Convert.ToInt32(row["HBP_SRYEAR"].ToString()),
                hbp_frmdate = row["HBP_FRMDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HBP_FRMDATE"]),
                hbp_todate = row["HBP_TODATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HBP_TODATE"]),
                hbp_taccount = row["HBP_TACCOUNT"] == DBNull.Value ? 0 : Convert.ToInt32(row["HBP_TACCOUNT"].ToString()),
                hbp_accbalance = row["HBP_ACCBALANCE"] == DBNull.Value ? 0 : Convert.ToInt32(row["HBP_ACCBALANCE"].ToString()),
                hbp_bnsper = row["HBP_BNSPER"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HBP_BNSPER"].ToString()),
                hbp_cre_by = row["HBP_CRE_BY"] == DBNull.Value ? string.Empty : row["HBP_CRE_BY"].ToString(),
                hbp_cre_dt = row["HBP_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HBP_CRE_DT"]),
                hbp_mod_by = row["HBP_MOD_BY"] == DBNull.Value ? string.Empty : row["HBP_MOD_BY"].ToString(),
                hbp_mod_dt = row["HBP_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HBP_MOD_DT"]),
                hbp_from_arrper = row["HBP_FROM_ARRPER"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HBP_FROM_ARRPER"].ToString()),
                hbp_to_arrper = row["HBP_TO_ARRPER"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HBP_TO_ARRPER"].ToString()),
                hbp_sr_fyear = row["HBP_SR_FYEAR"] == DBNull.Value ? 0 : Convert.ToInt32(row["HBP_SR_FYEAR"].ToString()),
                hbp_sr_tyear = row["HBP_SR_TYEAR"] == DBNull.Value ? 0 : Convert.ToInt32(row["HBP_SR_TYEAR"].ToString()),
                hbp_from_bal = row["HBP_FROM_BAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HBP_FROM_BAL"].ToString()),
                hbp_to_bal = row["HBP_TO_BAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HBP_TO_BAL"].ToString()),
                hbp_channel = row["HBP_CHANNEL"] == DBNull.Value ? string.Empty : row["HBP_CHANNEL"].ToString(),
                hbp_shstrdate = row["HBP_SHSTRDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HBP_SHSTRDATE"]),
                hbp_shstrab = row["HBP_SHSTRAB"] == DBNull.Value ? string.Empty : row["HBP_SHSTRAB"].ToString(),
               
            };
        }

        public static BonusDefinition Converter(DataRow row)
        {
            return new BonusDefinition
            {

                hbp_circular = row["HBP_CIRCULAR"] == DBNull.Value ? string.Empty : row["HBP_CIRCULAR"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()

            };
        }
    }
}
