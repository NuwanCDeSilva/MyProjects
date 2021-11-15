using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class ITService
    {
        public String BMS_COM_CD { get; set; }
        public String BMS_DO_LOC { get; set; }
        public String BMS_DO_LOC_DESC { get; set; }
        public String BMS_PC_CD { get; set; }
        public String BMS_PC_DESC { get; set; }
        public Int32 BMS_DO_YEAR { get; set; }
        public Int32 BMS_DO_MONTH { get; set; }
        public String BMS_INV_NO { get; set; }
        public String BMS_INV_TP { get; set; }
        public String BMS_INV_SUB_TP { get; set; }
        public DateTime BMS_INV_DT { get; set; }
        public String BMS_JOB_NO { get; set; }
        public String BMS_DO_NO { get; set; }
        public DateTime BMS_DO_DT { get; set; }
        public String BMS_CUST_CD { get; set; }
        public String BMS_CUST_NAME { get; set; }
        public String BMS_TECH_EPF { get; set; }
        public String BMS_TECH_NAME { get; set; }
        public String BMS_EXEC_CD { get; set; }
        public String BMS_EXEC_NAME { get; set; }
        public String BMS_PROMO_CD { get; set; }
        public String BMS_ITM_CAT1 { get; set; }
        public String BMS_ITM_CAT1_DESC { get; set; }
        public String BMS_ITM_CAT2 { get; set; }
        public String BMS_ITM_CAT2_DESC { get; set; }
        public String BMS_ITM_CAT3 { get; set; }
        public String BMS_ITM_CAT3_DESC { get; set; }
        public String BMS_ITM_CAT4 { get; set; }
        public String BMS_ITM_CAT4_DESC { get; set; }
        public String BMS_ITM_CAT5 { get; set; }
        public String BMS_ITM_CAT5_DESC { get; set; }
        public String BMS_BRND_CD { get; set; }
        public String BMS_BRND_NAME { get; set; }
        public String BMS_ITM_CD { get; set; }
        public String BMS_ITM_DESC { get; set; }
        public String BMS_ITM_MDL { get; set; }
        public String BMS_ITM_STUS { get; set; }
        public String BMS_PB_CD { get; set; }
        public String BMS_PB_LVL { get; set; }
        public Decimal BMS_D_QTY { get; set; }
        public Decimal BMS_D_COST { get; set; }
        public Decimal BMS_INST_SERCHRG { get; set; }
        public Decimal BMS_INIT_SERCHRG { get; set; }
        public Decimal BMS_D_UNIT_AMT { get; set; }
        public Decimal BMS_D_DISC_AMT { get; set; }
        public Decimal BMS_D_TAX_AMT { get; set; }
        public Decimal BMS_D_NET_AMT { get; set; }
        public Decimal BMS_GP { get; set; }
        public Decimal BMS_GP_MARGIN { get; set; }
        public Decimal BMS_WARR_PERIOD { get; set; }
        public int BMS_WARR_STUS { get; set; }
        public String BMS_SUPP_CD { get; set; }
        public String BMS_CLAIM_ITM_SERIAL { get; set; }
        public int BMS_DIRECT { get; set; }
        public String BMS_PC_CHNL { get; set; }
        public String BMS_LOC_CHNL { get; set; }
        public static ITService Converter(DataRow row)
        {
            return new ITService
            {
                BMS_COM_CD = row["BMS_COM_CD"] == DBNull.Value ? string.Empty : row["BMS_COM_CD"].ToString(),
                BMS_DO_LOC = row["BMS_DO_LOC"] == DBNull.Value ? string.Empty : row["BMS_DO_LOC"].ToString(),
                BMS_DO_LOC_DESC = row["BMS_DO_LOC_DESC"] == DBNull.Value ? string.Empty : row["BMS_DO_LOC_DESC"].ToString(),
                BMS_PC_CD = row["BMS_PC_CD"] == DBNull.Value ? string.Empty : row["BMS_PC_CD"].ToString(),
                BMS_PC_DESC = row["BMS_PC_DESC"] == DBNull.Value ? string.Empty : row["BMS_PC_DESC"].ToString(),
                BMS_DO_YEAR = row["BMS_DO_YEAR"] == DBNull.Value ? 0 : Convert.ToInt32(row["BMS_DO_YEAR"].ToString()),
                BMS_DO_MONTH = row["BMS_DO_MONTH"] == DBNull.Value ? 0 : Convert.ToInt32(row["BMS_DO_MONTH"].ToString()),
                BMS_INV_NO = row["BMS_INV_NO"] == DBNull.Value ? string.Empty : row["BMS_INV_NO"].ToString(),
                BMS_INV_TP = row["BMS_INV_TP"] == DBNull.Value ? string.Empty : row["BMS_INV_TP"].ToString(),
                BMS_INV_SUB_TP = row["BMS_INV_SUB_TP"] == DBNull.Value ? string.Empty : row["BMS_INV_SUB_TP"].ToString(),
                BMS_INV_DT = row["BMS_INV_DT"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(row["BMS_INV_DT"].ToString()),
                BMS_JOB_NO = row["BMS_JOB_NO"] == DBNull.Value ? string.Empty : row["BMS_JOB_NO"].ToString(),
                BMS_DO_NO = row["BMS_DO_NO"] == DBNull.Value ? string.Empty : row["BMS_DO_NO"].ToString(),
                BMS_DO_DT = row["BMS_DO_DT"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(row["BMS_DO_DT"].ToString()),
                BMS_CUST_CD = row["BMS_CUST_CD"] == DBNull.Value ? string.Empty : row["BMS_CUST_CD"].ToString(),
                BMS_CUST_NAME = row["BMS_CUST_NAME"] == DBNull.Value ? string.Empty : row["BMS_CUST_NAME"].ToString(),
                BMS_TECH_EPF = row["BMS_TECH_EPF"] == DBNull.Value ? string.Empty : row["BMS_TECH_EPF"].ToString(),
                BMS_TECH_NAME = row["BMS_TECH_NAME"] == DBNull.Value ? string.Empty : row["BMS_TECH_NAME"].ToString(),
                BMS_EXEC_CD = row["BMS_EXEC_CD"] == DBNull.Value ? string.Empty : row["BMS_EXEC_CD"].ToString(),
                BMS_ITM_CAT1 = row["BMS_ITM_CAT1"] == DBNull.Value ? string.Empty : row["BMS_ITM_CAT1"].ToString(),
                BMS_ITM_CAT1_DESC = row["BMS_ITM_CAT1_DESC"] == DBNull.Value ? string.Empty : row["BMS_ITM_CAT1_DESC"].ToString(),
                BMS_ITM_CAT2 = row["BMS_ITM_CAT2"] == DBNull.Value ? string.Empty : row["BMS_ITM_CAT2"].ToString(),
                BMS_ITM_CAT2_DESC = row["BMS_ITM_CAT2_DESC"] == DBNull.Value ? string.Empty : row["BMS_ITM_CAT2_DESC"].ToString(),
                BMS_ITM_CAT3 = row["BMS_ITM_CAT3"] == DBNull.Value ? string.Empty : row["BMS_ITM_CAT3"].ToString(),
                BMS_ITM_CAT3_DESC = row["BMS_ITM_CAT3_DESC"] == DBNull.Value ? string.Empty : row["BMS_ITM_CAT3_DESC"].ToString(),
                BMS_ITM_CAT4 = row["BMS_ITM_CAT4"] == DBNull.Value ? string.Empty : row["BMS_ITM_CAT4"].ToString(),
                BMS_ITM_CAT4_DESC = row["BMS_ITM_CAT4_DESC"] == DBNull.Value ? string.Empty : row["BMS_ITM_CAT4_DESC"].ToString(),
                BMS_ITM_CAT5 = row["BMS_ITM_CAT5"] == DBNull.Value ? string.Empty : row["BMS_ITM_CAT5"].ToString(),
                BMS_ITM_CAT5_DESC = row["BMS_ITM_CAT5_DESC"] == DBNull.Value ? string.Empty : row["BMS_ITM_CAT5_DESC"].ToString(),
                BMS_BRND_CD = row["BMS_BRND_CD"] == DBNull.Value ? string.Empty : row["BMS_BRND_CD"].ToString(),
                BMS_BRND_NAME = row["BMS_BRND_NAME"] == DBNull.Value ? string.Empty : row["BMS_BRND_NAME"].ToString(),
                BMS_ITM_CD = row["BMS_ITM_CD"] == DBNull.Value ? string.Empty : row["BMS_ITM_CD"].ToString(),
                BMS_ITM_DESC = row["BMS_ITM_DESC"] == DBNull.Value ? string.Empty : row["BMS_ITM_DESC"].ToString(),
                BMS_ITM_MDL = row["BMS_ITM_MDL"] == DBNull.Value ? string.Empty : row["BMS_ITM_MDL"].ToString(),
                BMS_ITM_STUS = row["BMS_ITM_STUS"] == DBNull.Value ? string.Empty : row["BMS_ITM_STUS"].ToString(),
                BMS_PB_CD = row["BMS_PB_CD"] == DBNull.Value ? string.Empty : row["BMS_PB_CD"].ToString(),
                BMS_PB_LVL = row["BMS_PB_LVL"] == DBNull.Value ? string.Empty : row["BMS_PB_LVL"].ToString(),
                BMS_D_QTY = row["BMS_D_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMS_D_QTY"].ToString()),
                BMS_D_COST = row["BMS_D_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMS_D_COST"].ToString()),
                BMS_INST_SERCHRG = row["BMS_INST_SERCHRG"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMS_INST_SERCHRG"].ToString()),
                BMS_INIT_SERCHRG = row["BMS_INIT_SERCHRG"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMS_INIT_SERCHRG"].ToString()),
                BMS_D_UNIT_AMT = row["BMS_D_UNIT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMS_D_UNIT_AMT"].ToString()),
                BMS_D_DISC_AMT = row["BMS_D_DISC_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMS_D_DISC_AMT"].ToString()),
                BMS_D_TAX_AMT = row["BMS_D_TAX_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMS_D_TAX_AMT"].ToString()),
                BMS_D_NET_AMT = row["BMS_D_NET_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMS_D_NET_AMT"].ToString()),
                BMS_GP = row["BMS_GP"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMS_GP"].ToString()),
                BMS_GP_MARGIN = row["BMS_GP_MARGIN"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMS_GP_MARGIN"].ToString()),
                BMS_WARR_PERIOD = row["BMS_WARR_PERIOD"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMS_WARR_PERIOD"].ToString()),
                BMS_WARR_STUS = row["BMS_WARR_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["BMS_WARR_STUS"].ToString()),
                BMS_SUPP_CD = row["BMS_SUPP_CD"] == DBNull.Value ? string.Empty : row["BMS_SUPP_CD"].ToString(),
                BMS_CLAIM_ITM_SERIAL = row["BMS_CLAIM_ITM_SERIAL"] == DBNull.Value ? string.Empty : row["BMS_CLAIM_ITM_SERIAL"].ToString(),
                BMS_DIRECT = row["BMS_DIRECT"] == DBNull.Value ? 0 : Convert.ToInt32(row["BMS_DIRECT"].ToString()),
                BMS_PC_CHNL = row["BMS_PC_CHNL"] == DBNull.Value ? string.Empty : row["BMS_PC_CHNL"].ToString(),
                BMS_LOC_CHNL = row["BMS_LOC_CHNL"] == DBNull.Value ? string.Empty : row["BMS_LOC_CHNL"].ToString()          
            };
        }
    }
}
