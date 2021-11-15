﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Asycuda
{
    public class ASY_IMP_CUSDEC_HDR
    {
        public Int32 CUH_SEQ_NO { get; set; }
        public String CUH_COM { get; set; }
        public String CUH_SBU { get; set; }
        public String CUH_CNTY { get; set; }
        public String CUH_TP { get; set; }
        public String CUH_DOC_NO { get; set; }
        public String CUH_OTH_NO { get; set; }
        public String CUH_FIN_DOC_NO { get; set; }
        public DateTime CUH_DT { get; set; }
        public String CUH_PROC_CD { get; set; }
        public String CUH_DECL_1 { get; set; }
        public String CUH_DECL_2 { get; set; }
        public String CUH_DECL_3 { get; set; }
        public String CUH_SUPP_CD { get; set; }
        public String CUH_CONSI_CD { get; set; }
        public String CUH_DECL_CD { get; set; }
        public String CUH_VESSEL { get; set; }
        public String CUH_VOYAGE { get; set; }
        public DateTime CUH_VOYAGE_DT { get; set; }
        public String CUH_PLACE_OF_LOADING { get; set; }
        public String CUH_OFFICE_OF_ENTRY { get; set; }
        public String CUH_LOCATION_OF_GOODS { get; set; }
        public String CUH_PREFERENCE { get; set; }
        public String CUH_ACC_NO { get; set; }
        public String CUH_FCL { get; set; }
        public String CUH_PAGE_1 { get; set; }
        public String CUH_PAGE_2 { get; set; }
        public Decimal CUH_ITEMS_QTY { get; set; }
        public String CUH_TOT_PKG { get; set; }
        public String CUH_TOT_PKG_UNIT { get; set; }
        public Decimal CUH_TOT_GROSS_MASS { get; set; }
        public Decimal CUH_TOT_NET_MASS { get; set; }
        public String CUH_CITY_OF_LAST_CONSIGNEE { get; set; }
        public String CUH_CNTY_OF_EXPORT { get; set; }
        public String CUH_CNTY_OF_ORIGIN { get; set; }
        public String CUH_CNTY_OF_DESTINATION { get; set; }
        public String CUH_DELIVERY_TERMS { get; set; }
        public String CUH_CUR_CD { get; set; }
        public Decimal CUH_TOT_AMT { get; set; }
        public Decimal CUH_EX_RT { get; set; }
        public String CUH_NATURE_OF_TRANCE { get; set; }
        public String CUH_TERMS_OF_PAYMENT { get; set; }
        public String CUH_BANK_CD { get; set; }
        public String CUH_BANK_NAME { get; set; }
        public Int32 CUH_COM_CHG { get; set; }
        public Int32 CUH_GRN_STAUS { get; set; }
        public Int32 CUH_AOD_STUS { get; set; }
        public String CUH_ENTRY_NO { get; set; }
        public String CUH_BL_NO { get; set; }
        public String CUH_LISION_NO { get; set; }
        public String CUH_WH_AND_PERIOD { get; set; }
        public String CUH_MARKS_AND_NO { get; set; }
        public String CUH_BANK_REF_CD { get; set; }
        public String CUH_CONTAINER_FCL { get; set; }
        public String CUH_SUN_REQ_NO { get; set; }
        public String CUH_SUN_BOND_NO { get; set; }
        public DateTime CUH_DOC_REC_DT { get; set; }
        public String CUH_CUSDEC_ENTRY_NO { get; set; }
        public DateTime CUH_CUSDEC_ENTRY_DT { get; set; }
        public String CUH_BANK_BRANCH { get; set; }
        public Decimal CUH_BOI_VAL { get; set; }
        public Int32 CUH_IGNORE { get; set; }
        public String CUH_CUST_CD { get; set; }
        public String CUH_DECL_SEQ_NO { get; set; }
        public String CUH_PPC_NO { get; set; }
        public String CUH_PROCE_CD_1 { get; set; }
        public String CUH_PROCE_CD_2 { get; set; }
        public Int32 CUH_AST_STUS { get; set; }
        public String CUH_AST_NO { get; set; }
        public String CUH_AST_NOTIES_NO { get; set; }
        public DateTime CUH_AST_DT { get; set; }
        public Int32 CUH_AST_IGNORE { get; set; }
        public String CUH_AST_IGNORE_BY { get; set; }
        public DateTime CUH_AST_IGNORE_DT { get; set; }
        public Int32 CUH_STL_STUS { get; set; }
        public String CUH_STL_NO { get; set; }
        public DateTime CUH_STL_DT { get; set; }
        public String CUH_CUS_ENTRY_USER { get; set; }
        public DateTime CUH_CUS_ENTRY_DT { get; set; }
        public Int32 CUH_ADD_SERIALS { get; set; }
        public String CUH_REQ_NO { get; set; }
        public String CUH_TRADING_COUNTRY { get; set; }
        public String CUH_INSU_TEXT { get; set; }
        public String CUH_FREIGHT_TEXT { get; set; }
        public String CUH_MAIN_HS { get; set; }
        public String CUH_STUS { get; set; }
        public String CUH_CRE_BY { get; set; }
        public DateTime CUH_CRE_DT { get; set; }
        public String CUH_CRE_SESSION { get; set; }
        public String CUH_MOD_BY { get; set; }
        public DateTime CUH_MOD_DT { get; set; }
        public String CUH_MOD_SESSION { get; set; }
        public String CUH_SUPP_TIN { get; set; }
        public String CUH_SUPP_NAME { get; set; }
        public String CUH_SUPP_ADDR { get; set; }
        public String CUH_CONSI_TIN { get; set; }
        public String CUH_CONSI_NAME { get; set; }
        public String CUH_CONSI_ADDR { get; set; }
        public String CUH_DECL_TIN { get; set; }
        public String CUH_DECL_NAME { get; set; }
        public String CUH_DECL_ADDR { get; set; }
        public String CUH_EXP_CNTY_NAME { get; set; }
        public String CUH_ORIGIN_CNTY_NAME { get; set; }
        public String CUH_DESTI_CNTY_NAME { get; set; }
        public Decimal CUH_GROSS_MASS { get; set; }
        public Decimal CUH_NET_MASS { get; set; }
        public String CUH_REF_NO { get; set; }
        public String CUH_RMK { get; set; }
        public String CUH_FILE_NO { get; set; }
        public String CUH_CUSTOM_LC_TP { get; set; }
        public String CUH_SUB_TP { get; set; }
        public String CUH_FIN_SETTLE_TEXT { get; set; }
        public static ASY_IMP_CUSDEC_HDR Converter(DataRow row)
        {
            return new ASY_IMP_CUSDEC_HDR
            {
                CUH_SEQ_NO = row["CUH_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["CUH_SEQ_NO"].ToString()),
                CUH_COM = row["CUH_COM"] == DBNull.Value ? string.Empty : row["CUH_COM"].ToString(),
                CUH_SBU = row["CUH_SBU"] == DBNull.Value ? string.Empty : row["CUH_SBU"].ToString(),
                CUH_CNTY = row["CUH_CNTY"] == DBNull.Value ? string.Empty : row["CUH_CNTY"].ToString(),
                CUH_TP = row["CUH_TP"] == DBNull.Value ? string.Empty : row["CUH_TP"].ToString(),
                CUH_DOC_NO = row["CUH_DOC_NO"] == DBNull.Value ? string.Empty : row["CUH_DOC_NO"].ToString(),
                CUH_OTH_NO = row["CUH_OTH_NO"] == DBNull.Value ? string.Empty : row["CUH_OTH_NO"].ToString(),
                CUH_FIN_DOC_NO = row["CUH_FIN_DOC_NO"] == DBNull.Value ? string.Empty : row["CUH_FIN_DOC_NO"].ToString(),
                CUH_DT = row["CUH_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["CUH_DT"].ToString()),
                CUH_PROC_CD = row["CUH_PROC_CD"] == DBNull.Value ? string.Empty : row["CUH_PROC_CD"].ToString(),
                CUH_DECL_1 = row["CUH_DECL_1"] == DBNull.Value ? string.Empty : row["CUH_DECL_1"].ToString(),
                CUH_DECL_2 = row["CUH_DECL_2"] == DBNull.Value ? string.Empty : row["CUH_DECL_2"].ToString(),
                CUH_DECL_3 = row["CUH_DECL_3"] == DBNull.Value ? string.Empty : row["CUH_DECL_3"].ToString(),
                CUH_SUPP_CD = row["CUH_SUPP_CD"] == DBNull.Value ? string.Empty : row["CUH_SUPP_CD"].ToString(),
                CUH_CONSI_CD = row["CUH_CONSI_CD"] == DBNull.Value ? string.Empty : row["CUH_CONSI_CD"].ToString(),
                CUH_DECL_CD = row["CUH_DECL_CD"] == DBNull.Value ? string.Empty : row["CUH_DECL_CD"].ToString(),
                CUH_VESSEL = row["CUH_VESSEL"] == DBNull.Value ? string.Empty : row["CUH_VESSEL"].ToString(),
                CUH_VOYAGE = row["CUH_VOYAGE"] == DBNull.Value ? string.Empty : row["CUH_VOYAGE"].ToString(),
                CUH_VOYAGE_DT = row["CUH_VOYAGE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["CUH_VOYAGE_DT"].ToString()),
                CUH_PLACE_OF_LOADING = row["CUH_PLACE_OF_LOADING"] == DBNull.Value ? string.Empty : row["CUH_PLACE_OF_LOADING"].ToString(),
                CUH_OFFICE_OF_ENTRY = row["CUH_OFFICE_OF_ENTRY"] == DBNull.Value ? string.Empty : row["CUH_OFFICE_OF_ENTRY"].ToString(),
                CUH_LOCATION_OF_GOODS = row["CUH_LOCATION_OF_GOODS"] == DBNull.Value ? string.Empty : row["CUH_LOCATION_OF_GOODS"].ToString(),
                CUH_PREFERENCE = row["CUH_PREFERENCE"] == DBNull.Value ? string.Empty : row["CUH_PREFERENCE"].ToString(),
                CUH_ACC_NO = row["CUH_ACC_NO"] == DBNull.Value ? string.Empty : row["CUH_ACC_NO"].ToString(),
                CUH_FCL = row["CUH_FCL"] == DBNull.Value ? string.Empty : row["CUH_FCL"].ToString(),
                CUH_PAGE_1 = row["CUH_PAGE_1"] == DBNull.Value ? string.Empty : row["CUH_PAGE_1"].ToString(),
                CUH_PAGE_2 = row["CUH_PAGE_2"] == DBNull.Value ? string.Empty : row["CUH_PAGE_2"].ToString(),
                CUH_ITEMS_QTY = row["CUH_ITEMS_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUH_ITEMS_QTY"].ToString()),
                CUH_TOT_PKG = row["CUH_TOT_PKG"] == DBNull.Value ? string.Empty : row["CUH_TOT_PKG"].ToString(),
                CUH_TOT_PKG_UNIT = row["CUH_TOT_PKG_UNIT"] == DBNull.Value ? string.Empty : row["CUH_TOT_PKG_UNIT"].ToString(),
                CUH_TOT_GROSS_MASS = row["CUH_TOT_GROSS_MASS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUH_TOT_GROSS_MASS"].ToString()),
                CUH_TOT_NET_MASS = row["CUH_TOT_NET_MASS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUH_TOT_NET_MASS"].ToString()),
                CUH_CITY_OF_LAST_CONSIGNEE = row["CUH_CITY_OF_LAST_CONSIGNEE"] == DBNull.Value ? string.Empty : row["CUH_CITY_OF_LAST_CONSIGNEE"].ToString(),
                CUH_CNTY_OF_EXPORT = row["CUH_CNTY_OF_EXPORT"] == DBNull.Value ? string.Empty : row["CUH_CNTY_OF_EXPORT"].ToString(),
                CUH_CNTY_OF_ORIGIN = row["CUH_CNTY_OF_ORIGIN"] == DBNull.Value ? string.Empty : row["CUH_CNTY_OF_ORIGIN"].ToString(),
                CUH_CNTY_OF_DESTINATION = row["CUH_CNTY_OF_DESTINATION"] == DBNull.Value ? string.Empty : row["CUH_CNTY_OF_DESTINATION"].ToString(),
                CUH_DELIVERY_TERMS = row["CUH_DELIVERY_TERMS"] == DBNull.Value ? string.Empty : row["CUH_DELIVERY_TERMS"].ToString(),
                CUH_CUR_CD = row["CUH_CUR_CD"] == DBNull.Value ? string.Empty : row["CUH_CUR_CD"].ToString(),
                CUH_TOT_AMT = row["CUH_TOT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUH_TOT_AMT"].ToString()),
                CUH_EX_RT = row["CUH_EX_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUH_EX_RT"].ToString()),
                CUH_NATURE_OF_TRANCE = row["CUH_NATURE_OF_TRANCE"] == DBNull.Value ? string.Empty : row["CUH_NATURE_OF_TRANCE"].ToString(),
                CUH_TERMS_OF_PAYMENT = row["CUH_TERMS_OF_PAYMENT"] == DBNull.Value ? string.Empty : row["CUH_TERMS_OF_PAYMENT"].ToString(),
                CUH_BANK_CD = row["CUH_BANK_CD"] == DBNull.Value ? string.Empty : row["CUH_BANK_CD"].ToString(),
                CUH_BANK_NAME = row["CUH_BANK_NAME"] == DBNull.Value ? string.Empty : row["CUH_BANK_NAME"].ToString(),
                CUH_COM_CHG = row["CUH_COM_CHG"] == DBNull.Value ? 0 : Convert.ToInt32(row["CUH_COM_CHG"].ToString()),
                CUH_GRN_STAUS = row["CUH_GRN_STAUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["CUH_GRN_STAUS"].ToString()),
                CUH_AOD_STUS = row["CUH_AOD_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["CUH_AOD_STUS"].ToString()),
                CUH_ENTRY_NO = row["CUH_ENTRY_NO"] == DBNull.Value ? string.Empty : row["CUH_ENTRY_NO"].ToString(),
                CUH_BL_NO = row["CUH_BL_NO"] == DBNull.Value ? string.Empty : row["CUH_BL_NO"].ToString(),
                CUH_LISION_NO = row["CUH_LISION_NO"] == DBNull.Value ? string.Empty : row["CUH_LISION_NO"].ToString(),
                CUH_WH_AND_PERIOD = row["CUH_WH_AND_PERIOD"] == DBNull.Value ? string.Empty : row["CUH_WH_AND_PERIOD"].ToString(),
                CUH_MARKS_AND_NO = row["CUH_MARKS_AND_NO"] == DBNull.Value ? string.Empty : row["CUH_MARKS_AND_NO"].ToString(),
                CUH_BANK_REF_CD = row["CUH_BANK_REF_CD"] == DBNull.Value ? string.Empty : row["CUH_BANK_REF_CD"].ToString(),
                CUH_CONTAINER_FCL = row["CUH_CONTAINER_FCL"] == DBNull.Value ? string.Empty : row["CUH_CONTAINER_FCL"].ToString(),
                CUH_SUN_REQ_NO = row["CUH_SUN_REQ_NO"] == DBNull.Value ? string.Empty : row["CUH_SUN_REQ_NO"].ToString(),
                CUH_SUN_BOND_NO = row["CUH_SUN_BOND_NO"] == DBNull.Value ? string.Empty : row["CUH_SUN_BOND_NO"].ToString(),
                CUH_DOC_REC_DT = row["CUH_DOC_REC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["CUH_DOC_REC_DT"].ToString()),
                CUH_CUSDEC_ENTRY_NO = row["CUH_CUSDEC_ENTRY_NO"] == DBNull.Value ? string.Empty : row["CUH_CUSDEC_ENTRY_NO"].ToString(),
                CUH_CUSDEC_ENTRY_DT = row["CUH_CUSDEC_ENTRY_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["CUH_CUSDEC_ENTRY_DT"].ToString()),
                CUH_BANK_BRANCH = row["CUH_BANK_BRANCH"] == DBNull.Value ? string.Empty : row["CUH_BANK_BRANCH"].ToString(),
                CUH_BOI_VAL = row["CUH_BOI_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUH_BOI_VAL"].ToString()),
                CUH_IGNORE = row["CUH_IGNORE"] == DBNull.Value ? 0 : Convert.ToInt32(row["CUH_IGNORE"].ToString()),
                CUH_CUST_CD = row["CUH_CUST_CD"] == DBNull.Value ? string.Empty : row["CUH_CUST_CD"].ToString(),
                CUH_DECL_SEQ_NO = row["CUH_DECL_SEQ_NO"] == DBNull.Value ? string.Empty : row["CUH_DECL_SEQ_NO"].ToString(),
                CUH_PPC_NO = row["CUH_PPC_NO"] == DBNull.Value ? string.Empty : row["CUH_PPC_NO"].ToString(),
                CUH_PROCE_CD_1 = row["CUH_PROCE_CD_1"] == DBNull.Value ? string.Empty : row["CUH_PROCE_CD_1"].ToString(),
                CUH_PROCE_CD_2 = row["CUH_PROCE_CD_2"] == DBNull.Value ? string.Empty : row["CUH_PROCE_CD_2"].ToString(),
                CUH_AST_STUS = row["CUH_AST_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["CUH_AST_STUS"].ToString()),
                CUH_AST_NO = row["CUH_AST_NO"] == DBNull.Value ? string.Empty : row["CUH_AST_NO"].ToString(),
                CUH_AST_NOTIES_NO = row["CUH_AST_NOTIES_NO"] == DBNull.Value ? string.Empty : row["CUH_AST_NOTIES_NO"].ToString(),
                CUH_AST_DT = row["CUH_AST_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["CUH_AST_DT"].ToString()),
                CUH_AST_IGNORE = row["CUH_AST_IGNORE"] == DBNull.Value ? 0 : Convert.ToInt32(row["CUH_AST_IGNORE"].ToString()),
                CUH_AST_IGNORE_BY = row["CUH_AST_IGNORE_BY"] == DBNull.Value ? string.Empty : row["CUH_AST_IGNORE_BY"].ToString(),
                CUH_AST_IGNORE_DT = row["CUH_AST_IGNORE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["CUH_AST_IGNORE_DT"].ToString()),
                CUH_STL_STUS = row["CUH_STL_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["CUH_STL_STUS"].ToString()),
                CUH_STL_NO = row["CUH_STL_NO"] == DBNull.Value ? string.Empty : row["CUH_STL_NO"].ToString(),
                CUH_STL_DT = row["CUH_STL_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["CUH_STL_DT"].ToString()),
                CUH_CUS_ENTRY_USER = row["CUH_CUS_ENTRY_USER"] == DBNull.Value ? string.Empty : row["CUH_CUS_ENTRY_USER"].ToString(),
                CUH_CUS_ENTRY_DT = row["CUH_CUS_ENTRY_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["CUH_CUS_ENTRY_DT"].ToString()),
                CUH_ADD_SERIALS = row["CUH_ADD_SERIALS"] == DBNull.Value ? 0 : Convert.ToInt32(row["CUH_ADD_SERIALS"].ToString()),
                CUH_REQ_NO = row["CUH_REQ_NO"] == DBNull.Value ? string.Empty : row["CUH_REQ_NO"].ToString(),
                CUH_TRADING_COUNTRY = row["CUH_TRADING_COUNTRY"] == DBNull.Value ? string.Empty : row["CUH_TRADING_COUNTRY"].ToString(),
                CUH_INSU_TEXT = row["CUH_INSU_TEXT"] == DBNull.Value ? string.Empty : row["CUH_INSU_TEXT"].ToString(),
                CUH_FREIGHT_TEXT = row["CUH_FREIGHT_TEXT"] == DBNull.Value ? string.Empty : row["CUH_FREIGHT_TEXT"].ToString(),
                CUH_MAIN_HS = row["CUH_MAIN_HS"] == DBNull.Value ? string.Empty : row["CUH_MAIN_HS"].ToString(),
                CUH_STUS = row["CUH_STUS"] == DBNull.Value ? string.Empty : row["CUH_STUS"].ToString(),
                CUH_CRE_BY = row["CUH_CRE_BY"] == DBNull.Value ? string.Empty : row["CUH_CRE_BY"].ToString(),
                CUH_CRE_DT = row["CUH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["CUH_CRE_DT"].ToString()),
                CUH_CRE_SESSION = row["CUH_CRE_SESSION"] == DBNull.Value ? string.Empty : row["CUH_CRE_SESSION"].ToString(),
                CUH_MOD_BY = row["CUH_MOD_BY"] == DBNull.Value ? string.Empty : row["CUH_MOD_BY"].ToString(),
                CUH_MOD_DT = row["CUH_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["CUH_MOD_DT"].ToString()),
                CUH_MOD_SESSION = row["CUH_MOD_SESSION"] == DBNull.Value ? string.Empty : row["CUH_MOD_SESSION"].ToString(),
                CUH_SUPP_TIN = row["CUH_SUPP_TIN"] == DBNull.Value ? string.Empty : row["CUH_SUPP_TIN"].ToString(),
                CUH_SUPP_NAME = row["CUH_SUPP_NAME"] == DBNull.Value ? string.Empty : row["CUH_SUPP_NAME"].ToString(),
                CUH_SUPP_ADDR = row["CUH_SUPP_ADDR"] == DBNull.Value ? string.Empty : row["CUH_SUPP_ADDR"].ToString(),
                CUH_CONSI_TIN = row["CUH_CONSI_TIN"] == DBNull.Value ? string.Empty : row["CUH_CONSI_TIN"].ToString(),
                CUH_CONSI_NAME = row["CUH_CONSI_NAME"] == DBNull.Value ? string.Empty : row["CUH_CONSI_NAME"].ToString(),
                CUH_CONSI_ADDR = row["CUH_CONSI_ADDR"] == DBNull.Value ? string.Empty : row["CUH_CONSI_ADDR"].ToString(),
                CUH_DECL_TIN = row["CUH_DECL_TIN"] == DBNull.Value ? string.Empty : row["CUH_DECL_TIN"].ToString(),
                CUH_DECL_NAME = row["CUH_DECL_NAME"] == DBNull.Value ? string.Empty : row["CUH_DECL_NAME"].ToString(),
                CUH_DECL_ADDR = row["CUH_DECL_ADDR"] == DBNull.Value ? string.Empty : row["CUH_DECL_ADDR"].ToString(),
                CUH_EXP_CNTY_NAME = row["CUH_EXP_CNTY_NAME"] == DBNull.Value ? string.Empty : row["CUH_EXP_CNTY_NAME"].ToString(),
                CUH_ORIGIN_CNTY_NAME = row["CUH_ORIGIN_CNTY_NAME"] == DBNull.Value ? string.Empty : row["CUH_ORIGIN_CNTY_NAME"].ToString(),
                CUH_DESTI_CNTY_NAME = row["CUH_DESTI_CNTY_NAME"] == DBNull.Value ? string.Empty : row["CUH_DESTI_CNTY_NAME"].ToString(),
                CUH_GROSS_MASS = row["CUH_GROSS_MASS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUH_GROSS_MASS"].ToString()),
                CUH_NET_MASS = row["CUH_NET_MASS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUH_NET_MASS"].ToString()),
                CUH_REF_NO = row["CUH_REF_NO"] == DBNull.Value ? string.Empty : row["CUH_REF_NO"].ToString(),
                CUH_RMK = row["CUH_RMK"] == DBNull.Value ? string.Empty : row["CUH_RMK"].ToString(),
                CUH_FILE_NO = row["CUH_FILE_NO"] == DBNull.Value ? string.Empty : row["CUH_FILE_NO"].ToString(),
                CUH_CUSTOM_LC_TP = row["CUH_CUSTOM_LC_TP"] == DBNull.Value ? string.Empty : row["CUH_CUSTOM_LC_TP"].ToString(),
                CUH_SUB_TP = row["CUH_SUB_TP"] == DBNull.Value ? string.Empty : row["CUH_SUB_TP"].ToString(),
                CUH_FIN_SETTLE_TEXT = row["CUH_FIN_SETTLE_TEXT"] == DBNull.Value ? string.Empty : row["CUH_FIN_SETTLE_TEXT"].ToString()
            };
        } 
    }
}