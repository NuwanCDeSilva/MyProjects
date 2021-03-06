using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Sales
{

    public class MST_BUSENTITY
    {
        public String MBE_COM { get; set; }
        public String MBE_CD { get; set; }
        public String MBE_TP { get; set; }
        public String MBE_SUB_TP { get; set; }
        public String MBE_ACC_CD { get; set; }
        public String MBE_NAME { get; set; }
        public String MBE_ADD1 { get; set; }
        public String MBE_ADD2 { get; set; }
        public String MBE_COUNTRY_CD { get; set; }
        public String MBE_PROVINCE_CD { get; set; }
        public String MBE_DISTRIC_CD { get; set; }
        public String MBE_TOWN_CD { get; set; }
        public String MBE_TEL { get; set; }
        public String MBE_FAX { get; set; }
        public String MBE_MOB { get; set; }
        public String MBE_NIC { get; set; }
        public String MBE_EMAIL { get; set; }
        public String MBE_CONTACT { get; set; }
        public Int32 MBE_ACT { get; set; }
        public Int32 MBE_IS_SUSPEND { get; set; }
        public Int32 MBE_IS_TAX { get; set; }
        public String MBE_TAX_NO { get; set; }
        public String MBE_PP_NO { get; set; }
        public String MBE_DL_NO { get; set; }
        public String MBE_BR_NO { get; set; }
        public String MBE_CRE_BY { get; set; }
        public DateTime MBE_CRE_DT { get; set; }
        public String MBE_SEX { get; set; }
        public DateTime MBE_DOB { get; set; }
        public Int32 MBE_TAX_EX { get; set; }
        public String MBE_INCOME_GRUP { get; set; }
        public String MBE_CATE { get; set; }
        public String MBE_OTH_ID_TP { get; set; }
        public String MBE_OTH_ID_NO { get; set; }
        public String MBE_HO_STUS { get; set; }
        public String MBE_PC_STUS { get; set; }
        public String MBE_CRE_PC { get; set; }
        public Int32 MBE_IS_SVAT { get; set; }
        public String MBE_SVAT_NO { get; set; }
        public String MBE_CR_ADD1 { get; set; }
        public String MBE_CR_ADD2 { get; set; }
        public String MBE_CR_COUNTRY_CD { get; set; }
        public String MBE_CR_PROVINCE_CD { get; set; }
        public String MBE_CR_DISTRIC_CD { get; set; }
        public String MBE_CR_TOWN_CD { get; set; }
        public String MBE_CR_TEL { get; set; }
        public String MBE_CR_FAX { get; set; }
        public String MBE_CR_EMAIL { get; set; }
        public String MBE_WR_ADD1 { get; set; }
        public String MBE_WR_ADD2 { get; set; }
        public String MBE_WR_COUNTRY_CD { get; set; }
        public String MBE_WR_PROVINCE_CD { get; set; }
        public String MBE_WR_DISTRIC_CD { get; set; }
        public String MBE_WR_TOWN_CD { get; set; }
        public String MBE_WR_TEL { get; set; }
        public String MBE_WR_FAX { get; set; }
        public String MBE_WR_EMAIL { get; set; }
        public String MBE_WR_COM_NAME { get; set; }
        public String MBE_WR_PROFFESION { get; set; }
        public String MBE_WR_DEPT { get; set; }
        public String MBE_WR_DESIGNATION { get; set; }
        public String MBE_POSTAL_CD { get; set; }
        public String MBE_CR_POSTAL_CD { get; set; }
        public Int32 MBE_AGRE_SEND_SMS { get; set; }
        public Int32 MBE_INTR_COM { get; set; }
        public String MBE_CUST_COM { get; set; }
        public String MBE_CUST_LOC { get; set; }
        public String MBE_NATIONALITY { get; set; }
        public String MBE_TIT { get; set; }
        public String MBE_INI { get; set; }
        public String MBE_FNAME { get; set; }
        public String MBE_SNAME { get; set; }
        public Int32 MBE_CHQ_ISS { get; set; }
        public Int32 MBE_AGRE_SEND_EMAIL { get; set; }
        public String MBE_CUST_LANG { get; set; }
        public String MBE_CUR_CD { get; set; }
        public String MBE_WEB { get; set; }
        public Int32 MBE_CR_PERIOD { get; set; }
        public String MBE_MOD_BY { get; set; }
        public DateTime MBE_MOD_DT { get; set; }
        public String MBE_MOD_SESSION { get; set; }
        public String MBE_CRE_SESSION { get; set; }
        public Int32 MBE_ALW_DCN { get; set; }
        public Int32 MBE_INS_MAN { get; set; }
        public Int32 MBE_MIN_DP_PER { get; set; }
        public Int32 MBE_QNO_GEN_SEQ { get; set; }
        public DateTime MBE_PP_ISU_DTE { get; set; }
        public DateTime MBE_PP_EXP_DTE { get; set; }
        public DateTime MBE_DL_ISU_DTE { get; set; }
        public DateTime MBE_DL_EXP_DTE { get; set; }
        public String MBE_PROC_CD { get; set; }
        public String MBE_WH_CD { get; set; }
        public String MBE_PROC_VAL1 { get; set; }
        public String MBE_PROC_VAL2 { get; set; }
        public String MBE_REF_NO { get; set; }
        public Int32 MBE_FOC { get; set; }
        public DateTime MBE_BI_YEAR { get; set; }
        public String MBE_CREDIT_PERIOD { get; set; }
        public static MST_BUSENTITY Converter(DataRow row)
        {
            return new MST_BUSENTITY
            {
                MBE_COM = row["MBE_COM"] == DBNull.Value ? string.Empty : row["MBE_COM"].ToString(),
                MBE_CD = row["MBE_CD"] == DBNull.Value ? string.Empty : row["MBE_CD"].ToString(),
                MBE_TP = row["MBE_TP"] == DBNull.Value ? string.Empty : row["MBE_TP"].ToString(),
                MBE_SUB_TP = row["MBE_SUB_TP"] == DBNull.Value ? string.Empty : row["MBE_SUB_TP"].ToString(),
                MBE_ACC_CD = row["MBE_ACC_CD"] == DBNull.Value ? string.Empty : row["MBE_ACC_CD"].ToString(),
                MBE_NAME = row["MBE_NAME"] == DBNull.Value ? string.Empty : row["MBE_NAME"].ToString(),
                MBE_ADD1 = row["MBE_ADD1"] == DBNull.Value ? string.Empty : row["MBE_ADD1"].ToString(),
                MBE_ADD2 = row["MBE_ADD2"] == DBNull.Value ? string.Empty : row["MBE_ADD2"].ToString(),
                MBE_COUNTRY_CD = row["MBE_COUNTRY_CD"] == DBNull.Value ? string.Empty : row["MBE_COUNTRY_CD"].ToString(),
                MBE_PROVINCE_CD = row["MBE_PROVINCE_CD"] == DBNull.Value ? string.Empty : row["MBE_PROVINCE_CD"].ToString(),
                MBE_DISTRIC_CD = row["MBE_DISTRIC_CD"] == DBNull.Value ? string.Empty : row["MBE_DISTRIC_CD"].ToString(),
                MBE_TOWN_CD = row["MBE_TOWN_CD"] == DBNull.Value ? string.Empty : row["MBE_TOWN_CD"].ToString(),
                MBE_TEL = row["MBE_TEL"] == DBNull.Value ? string.Empty : row["MBE_TEL"].ToString(),
                MBE_FAX = row["MBE_FAX"] == DBNull.Value ? string.Empty : row["MBE_FAX"].ToString(),
                MBE_MOB = row["MBE_MOB"] == DBNull.Value ? string.Empty : row["MBE_MOB"].ToString(),
                MBE_NIC = row["MBE_NIC"] == DBNull.Value ? string.Empty : row["MBE_NIC"].ToString(),
                MBE_EMAIL = row["MBE_EMAIL"] == DBNull.Value ? string.Empty : row["MBE_EMAIL"].ToString(),
                MBE_CONTACT = row["MBE_CONTACT"] == DBNull.Value ? string.Empty : row["MBE_CONTACT"].ToString(),
                MBE_ACT = row["MBE_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBE_ACT"].ToString()),
                MBE_IS_SUSPEND = row["MBE_IS_SUSPEND"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBE_IS_SUSPEND"].ToString()),
                MBE_IS_TAX = row["MBE_IS_TAX"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBE_IS_TAX"].ToString()),
                MBE_TAX_NO = row["MBE_TAX_NO"] == DBNull.Value ? string.Empty : row["MBE_TAX_NO"].ToString(),
                MBE_PP_NO = row["MBE_PP_NO"] == DBNull.Value ? string.Empty : row["MBE_PP_NO"].ToString(),
                MBE_DL_NO = row["MBE_DL_NO"] == DBNull.Value ? string.Empty : row["MBE_DL_NO"].ToString(),
                MBE_BR_NO = row["MBE_BR_NO"] == DBNull.Value ? string.Empty : row["MBE_BR_NO"].ToString(),
                MBE_CRE_BY = row["MBE_CRE_BY"] == DBNull.Value ? string.Empty : row["MBE_CRE_BY"].ToString(),
                MBE_CRE_DT = row["MBE_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBE_CRE_DT"].ToString()),
                MBE_SEX = row["MBE_SEX"] == DBNull.Value ? string.Empty : row["MBE_SEX"].ToString(),
                MBE_DOB = row["MBE_DOB"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBE_DOB"].ToString()),
                MBE_TAX_EX = row["MBE_TAX_EX"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBE_TAX_EX"].ToString()),
                MBE_INCOME_GRUP = row["MBE_INCOME_GRUP"] == DBNull.Value ? string.Empty : row["MBE_INCOME_GRUP"].ToString(),
                MBE_CATE = row["MBE_CATE"] == DBNull.Value ? string.Empty : row["MBE_CATE"].ToString(),
                MBE_OTH_ID_TP = row["MBE_OTH_ID_TP"] == DBNull.Value ? string.Empty : row["MBE_OTH_ID_TP"].ToString(),
                MBE_OTH_ID_NO = row["MBE_OTH_ID_NO"] == DBNull.Value ? string.Empty : row["MBE_OTH_ID_NO"].ToString(),
                MBE_HO_STUS = row["MBE_HO_STUS"] == DBNull.Value ? string.Empty : row["MBE_HO_STUS"].ToString(),
                MBE_PC_STUS = row["MBE_PC_STUS"] == DBNull.Value ? string.Empty : row["MBE_PC_STUS"].ToString(),
                MBE_CRE_PC = row["MBE_CRE_PC"] == DBNull.Value ? string.Empty : row["MBE_CRE_PC"].ToString(),
                MBE_IS_SVAT = row["MBE_IS_SVAT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBE_IS_SVAT"].ToString()),
                MBE_SVAT_NO = row["MBE_SVAT_NO"] == DBNull.Value ? string.Empty : row["MBE_SVAT_NO"].ToString(),
                MBE_CR_ADD1 = row["MBE_CR_ADD1"] == DBNull.Value ? string.Empty : row["MBE_CR_ADD1"].ToString(),
                MBE_CR_ADD2 = row["MBE_CR_ADD2"] == DBNull.Value ? string.Empty : row["MBE_CR_ADD2"].ToString(),
                MBE_CR_COUNTRY_CD = row["MBE_CR_COUNTRY_CD"] == DBNull.Value ? string.Empty : row["MBE_CR_COUNTRY_CD"].ToString(),
                MBE_CR_PROVINCE_CD = row["MBE_CR_PROVINCE_CD"] == DBNull.Value ? string.Empty : row["MBE_CR_PROVINCE_CD"].ToString(),
                MBE_CR_DISTRIC_CD = row["MBE_CR_DISTRIC_CD"] == DBNull.Value ? string.Empty : row["MBE_CR_DISTRIC_CD"].ToString(),
                MBE_CR_TOWN_CD = row["MBE_CR_TOWN_CD"] == DBNull.Value ? string.Empty : row["MBE_CR_TOWN_CD"].ToString(),
                MBE_CR_TEL = row["MBE_CR_TEL"] == DBNull.Value ? string.Empty : row["MBE_CR_TEL"].ToString(),
                MBE_CR_FAX = row["MBE_CR_FAX"] == DBNull.Value ? string.Empty : row["MBE_CR_FAX"].ToString(),
                MBE_CR_EMAIL = row["MBE_CR_EMAIL"] == DBNull.Value ? string.Empty : row["MBE_CR_EMAIL"].ToString(),
                MBE_WR_ADD1 = row["MBE_WR_ADD1"] == DBNull.Value ? string.Empty : row["MBE_WR_ADD1"].ToString(),
                MBE_WR_ADD2 = row["MBE_WR_ADD2"] == DBNull.Value ? string.Empty : row["MBE_WR_ADD2"].ToString(),
                MBE_WR_COUNTRY_CD = row["MBE_WR_COUNTRY_CD"] == DBNull.Value ? string.Empty : row["MBE_WR_COUNTRY_CD"].ToString(),
                MBE_WR_PROVINCE_CD = row["MBE_WR_PROVINCE_CD"] == DBNull.Value ? string.Empty : row["MBE_WR_PROVINCE_CD"].ToString(),
                MBE_WR_DISTRIC_CD = row["MBE_WR_DISTRIC_CD"] == DBNull.Value ? string.Empty : row["MBE_WR_DISTRIC_CD"].ToString(),
                MBE_WR_TOWN_CD = row["MBE_WR_TOWN_CD"] == DBNull.Value ? string.Empty : row["MBE_WR_TOWN_CD"].ToString(),
                MBE_WR_TEL = row["MBE_WR_TEL"] == DBNull.Value ? string.Empty : row["MBE_WR_TEL"].ToString(),
                MBE_WR_FAX = row["MBE_WR_FAX"] == DBNull.Value ? string.Empty : row["MBE_WR_FAX"].ToString(),
                MBE_WR_EMAIL = row["MBE_WR_EMAIL"] == DBNull.Value ? string.Empty : row["MBE_WR_EMAIL"].ToString(),
                MBE_WR_COM_NAME = row["MBE_WR_COM_NAME"] == DBNull.Value ? string.Empty : row["MBE_WR_COM_NAME"].ToString(),
                MBE_WR_PROFFESION = row["MBE_WR_PROFFESION"] == DBNull.Value ? string.Empty : row["MBE_WR_PROFFESION"].ToString(),
                MBE_WR_DEPT = row["MBE_WR_DEPT"] == DBNull.Value ? string.Empty : row["MBE_WR_DEPT"].ToString(),
                MBE_WR_DESIGNATION = row["MBE_WR_DESIGNATION"] == DBNull.Value ? string.Empty : row["MBE_WR_DESIGNATION"].ToString(),
                MBE_POSTAL_CD = row["MBE_POSTAL_CD"] == DBNull.Value ? string.Empty : row["MBE_POSTAL_CD"].ToString(),
                MBE_CR_POSTAL_CD = row["MBE_CR_POSTAL_CD"] == DBNull.Value ? string.Empty : row["MBE_CR_POSTAL_CD"].ToString(),
                MBE_AGRE_SEND_SMS = row["MBE_AGRE_SEND_SMS"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBE_AGRE_SEND_SMS"].ToString()),
                MBE_INTR_COM = row["MBE_INTR_COM"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBE_INTR_COM"].ToString()),
                MBE_CUST_COM = row["MBE_CUST_COM"] == DBNull.Value ? string.Empty : row["MBE_CUST_COM"].ToString(),
                MBE_CUST_LOC = row["MBE_CUST_LOC"] == DBNull.Value ? string.Empty : row["MBE_CUST_LOC"].ToString(),
                MBE_NATIONALITY = row["MBE_NATIONALITY"] == DBNull.Value ? string.Empty : row["MBE_NATIONALITY"].ToString(),
                MBE_TIT = row["MBE_TIT"] == DBNull.Value ? string.Empty : row["MBE_TIT"].ToString(),
                MBE_INI = row["MBE_INI"] == DBNull.Value ? string.Empty : row["MBE_INI"].ToString(),
                MBE_FNAME = row["MBE_FNAME"] == DBNull.Value ? string.Empty : row["MBE_FNAME"].ToString(),
                MBE_SNAME = row["MBE_SNAME"] == DBNull.Value ? string.Empty : row["MBE_SNAME"].ToString(),
                MBE_CHQ_ISS = row["MBE_CHQ_ISS"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBE_CHQ_ISS"].ToString()),
                MBE_AGRE_SEND_EMAIL = row["MBE_AGRE_SEND_EMAIL"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBE_AGRE_SEND_EMAIL"].ToString()),
                MBE_CUST_LANG = row["MBE_CUST_LANG"] == DBNull.Value ? string.Empty : row["MBE_CUST_LANG"].ToString(),
                MBE_CUR_CD = row["MBE_CUR_CD"] == DBNull.Value ? string.Empty : row["MBE_CUR_CD"].ToString(),
                MBE_WEB = row["MBE_WEB"] == DBNull.Value ? string.Empty : row["MBE_WEB"].ToString(),
                MBE_CR_PERIOD = row["MBE_CR_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBE_CR_PERIOD"].ToString()),
                MBE_MOD_BY = row["MBE_MOD_BY"] == DBNull.Value ? string.Empty : row["MBE_MOD_BY"].ToString(),
                MBE_MOD_DT = row["MBE_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBE_MOD_DT"].ToString()),
                MBE_MOD_SESSION = row["MBE_MOD_SESSION"] == DBNull.Value ? string.Empty : row["MBE_MOD_SESSION"].ToString(),
                MBE_CRE_SESSION = row["MBE_CRE_SESSION"] == DBNull.Value ? string.Empty : row["MBE_CRE_SESSION"].ToString(),
                MBE_ALW_DCN = row["MBE_ALW_DCN"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBE_ALW_DCN"].ToString()),
                MBE_INS_MAN = row["MBE_INS_MAN"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBE_INS_MAN"].ToString()),
                MBE_MIN_DP_PER = row["MBE_MIN_DP_PER"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBE_MIN_DP_PER"].ToString()),
                MBE_QNO_GEN_SEQ = row["MBE_QNO_GEN_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBE_QNO_GEN_SEQ"].ToString()),
                MBE_PP_ISU_DTE = row["MBE_PP_ISU_DTE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBE_PP_ISU_DTE"].ToString()),
                MBE_PP_EXP_DTE = row["MBE_PP_EXP_DTE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBE_PP_EXP_DTE"].ToString()),
                MBE_DL_ISU_DTE = row["MBE_DL_ISU_DTE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBE_DL_ISU_DTE"].ToString()),
                MBE_DL_EXP_DTE = row["MBE_DL_EXP_DTE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBE_DL_EXP_DTE"].ToString()),
                MBE_PROC_CD = row["MBE_PROC_CD"] == DBNull.Value ? string.Empty : row["MBE_PROC_CD"].ToString(),
                MBE_WH_CD = row["MBE_WH_CD"] == DBNull.Value ? string.Empty : row["MBE_WH_CD"].ToString(),
                MBE_PROC_VAL1 = row["MBE_PROC_VAL1"] == DBNull.Value ? string.Empty : row["MBE_PROC_VAL1"].ToString(),
                MBE_PROC_VAL2 = row["MBE_PROC_VAL2"] == DBNull.Value ? string.Empty : row["MBE_PROC_VAL2"].ToString(),
                MBE_REF_NO = row["MBE_REF_NO"] == DBNull.Value ? string.Empty : row["MBE_REF_NO"].ToString(),
                MBE_FOC = row["MBE_FOC"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBE_FOC"].ToString()),
                MBE_BI_YEAR = row["MBE_BI_YEAR"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBE_BI_YEAR"].ToString()),
                MBE_CREDIT_PERIOD = row["MBE_CREDIT_PERIOD"] == DBNull.Value ? string.Empty : row["MBE_CREDIT_PERIOD"].ToString()
            };
        }
    } 

}
