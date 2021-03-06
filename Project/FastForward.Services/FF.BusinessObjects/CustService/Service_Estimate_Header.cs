using System;
using System.Data;

namespace FF.BusinessObjects
{
    //===========================================================================================================
    // This code is generated by Code gen V.1
    // Computer :- ITPD11  | User :- suneth On 20-Oct-2014 10:28:22
    //===========================================================================================================

    public class Service_Estimate_Header
    {
        public Int32 ESH_SEQ_NO { get; set; }

        public String ESH_ESTNO { get; set; }

        public DateTime ESH_DT { get; set; }

        public String ESH_COM { get; set; }

        public String ESH_LOC { get; set; }

        public String EST_PC { get; set; }

        public String ESH_TP { get; set; }

        public String ESH_JOB_NO { get; set; }

        public String EST_STUS { get; set; }

        public Int32 EST_APP { get; set; }

        public String EST_APP_BY { get; set; }

        public DateTime EST_APP_DT { get; set; }

        public String EST_RMK { get; set; }

        public String EST_MAN_REF { get; set; }

        public Int32 EST_DURATION { get; set; }

        public String EST_CRE_BY { get; set; }

        public DateTime EST_CRE_DT { get; set; }

        public String EST_MOD_BY { get; set; }

        public DateTime EST_MOD_DT { get; set; }

        public String EST_PRINT_RMK { get; set; }

        public String EST_CUST_CD { get; set; }

        public String EST_CUST_TP { get; set; }

        public Decimal EST_IS_TAX { get; set; }

        public String EST_TAX_NO { get; set; }

        public Decimal EST_TAX_EX { get; set; }

        public Decimal EST_IS_SVAT { get; set; }

        public String EST_SVAT_NO { get; set; }

        public static Service_Estimate_Header Converter(DataRow row)
        {
            return new Service_Estimate_Header
            {
                ESH_SEQ_NO = row["ESH_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ESH_SEQ_NO"].ToString()),
                ESH_ESTNO = row["ESH_ESTNO"] == DBNull.Value ? string.Empty : row["ESH_ESTNO"].ToString(),
                ESH_DT = row["ESH_DT"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(row["ESH_DT"].ToString()),
                ESH_COM = row["ESH_COM"] == DBNull.Value ? string.Empty : row["ESH_COM"].ToString(),
                ESH_LOC = row["ESH_LOC"] == DBNull.Value ? string.Empty : row["ESH_LOC"].ToString(),
                EST_PC = row["EST_PC"] == DBNull.Value ? string.Empty : row["EST_PC"].ToString(),
                ESH_TP = row["ESH_TP"] == DBNull.Value ? string.Empty : row["ESH_TP"].ToString(),
                ESH_JOB_NO = row["ESH_JOB_NO"] == DBNull.Value ? string.Empty : row["ESH_JOB_NO"].ToString(),
                EST_STUS = row["EST_STUS"] == DBNull.Value ? string.Empty : row["EST_STUS"].ToString(),
                EST_APP = row["EST_APP"] == DBNull.Value ? 0 : Convert.ToInt32(row["EST_APP"].ToString()),
                EST_APP_BY = row["EST_APP_BY"] == DBNull.Value ? string.Empty : row["EST_APP_BY"].ToString(),
                EST_APP_DT = row["EST_APP_DT"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(row["EST_APP_DT"].ToString()),
                EST_RMK = row["EST_RMK"] == DBNull.Value ? string.Empty : row["EST_RMK"].ToString(),
                EST_MAN_REF = row["EST_MAN_REF"] == DBNull.Value ? string.Empty : row["EST_MAN_REF"].ToString(),
                EST_DURATION = row["EST_DURATION"] == DBNull.Value ? 0 : Convert.ToInt32(row["EST_DURATION"].ToString()),
                EST_CRE_BY = row["EST_CRE_BY"] == DBNull.Value ? string.Empty : row["EST_CRE_BY"].ToString(),
                EST_CRE_DT = row["EST_CRE_DT"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(row["EST_CRE_DT"].ToString()),
                EST_MOD_BY = row["EST_MOD_BY"] == DBNull.Value ? string.Empty : row["EST_MOD_BY"].ToString(),
                EST_MOD_DT = row["EST_MOD_DT"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(row["EST_MOD_DT"].ToString()),
                EST_PRINT_RMK = row["EST_PRINT_RMK"] == DBNull.Value ? string.Empty : row["EST_PRINT_RMK"].ToString(),
                EST_CUST_CD = row["EST_CUST_CD"] == DBNull.Value ? string.Empty : row["EST_CUST_CD"].ToString(),
                EST_CUST_TP = row["EST_CUST_TP"] == DBNull.Value ? string.Empty : row["EST_CUST_TP"].ToString(),
                EST_IS_TAX = row["EST_IS_TAX"] == DBNull.Value ? 0 : Convert.ToDecimal(row["EST_IS_TAX"].ToString()),
                EST_TAX_NO = row["EST_TAX_NO"] == DBNull.Value ? string.Empty : row["EST_TAX_NO"].ToString(),
                EST_TAX_EX = row["EST_TAX_EX"] == DBNull.Value ? 0 : Convert.ToDecimal(row["EST_TAX_EX"].ToString()),
                EST_IS_SVAT = row["EST_IS_SVAT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["EST_IS_SVAT"].ToString()),
                EST_SVAT_NO = row["EST_SVAT_NO"] == DBNull.Value ? string.Empty : row["EST_SVAT_NO"].ToString()
            };
        }
    }
}