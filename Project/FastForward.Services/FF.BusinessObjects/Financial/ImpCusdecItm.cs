using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{

    //===========================================================================================================
    // This code is generated by Code gen V.1 
    // All rights reserved.
    // Suneththaraka02@gmail.com 
    // Computer :- MILKYWAY  | User :- Chamald On 17-Dec-2015 09:19:11
    //===========================================================================================================
    [Serializable]
    public class ImpCusdecItm
    {
        public Int32 Cui_seq_no { get; set; }
        public Int32 Cui_line { get; set; }
        public String Cui_doc_no { get; set; }
        public String Cui_itm_cd { get; set; }
        public String Cui_itm_stus { get; set; }
        public String Cui_hs_cd { get; set; }
        public String Cui_model { get; set; }
        public String Cui_itm_desc { get; set; }
        public String Cui_tp { get; set; }
        public String Cui_tag { get; set; }
        public Decimal Cui_pi_unit_rt { get; set; }
        public Decimal Cui_bl_unit_rt { get; set; }
        public Decimal Cui_unit_rt { get; set; }
        public Decimal Cui_itm_price { get; set; }
        public Decimal Cui_qty { get; set; }
        public Decimal Cui_bal_qty1 { get; set; }
        public Decimal Cui_bal_qty2 { get; set; }
        public Decimal Cui_bal_qty3 { get; set; }
        public Decimal Cui_req_qty { get; set; }
        public String Cui_oth_doc_no { get; set; }
        public Int32 Cui_oth_doc_line { get; set; }
        public String Cui_fin_no { get; set; }
        public String Cui_pi_no { get; set; }
        public Int32 Cui_pi_line { get; set; }
        public Int32 Cui_kit_line { get; set; }
        public String Cui_kit_itm_cd { get; set; }
        public Decimal Cui_gross_mass { get; set; }
        public Decimal Cui_net_mass { get; set; }
        public String Cui_bl_no { get; set; }
        public String Cui_quota { get; set; }
        public String Cui_preferance { get; set; }
        public String Cui_def_cnty { get; set; }
        public String Cui_orgin_cnty { get; set; }
        public String Cui_pkgs { get; set; }
        public String Cui_capacity { get; set; }
        public String Cui_anal_1 { get; set; }
        public String Cui_anal_2 { get; set; }
        public String Cui_anal_3 { get; set; }
        public String Cui_anal_4 { get; set; }
        public String Cui_anal_5 { get; set; }
        public String Cui_cre_by { get; set; }
        public DateTime Cui_cre_dt { get; set; }
        public String Cui_cre_session { get; set; }
        public String Cui_mod_by { get; set; }
        public DateTime Cui_mod_dt { get; set; }
        public String Cui_mod_session { get; set; }
        public Decimal Cui_unit_amt { get; set; }

        public String Cui_Bond_no { get; set; } //Rukshan 30 /Jun/2016
        public bool Cui_is_res { get; set; } //Rukshan 05 /Aug/2016

        public String Cui_base_itm { get; set; } //Rukshan 05 /Aug/2016
        public bool itri_itm_cond { get; set; } //subodana 
        public Int32 Tmp_itri_line { get; set; } //add by lakshan
        public Int32 EntryLine { get; set; }
        public string ItemCat1 { get; set; }
        public string ItemCat2 { get; set; }
        public static ImpCusdecItm Converter(DataRow row)
        {
            return new ImpCusdecItm
            {
                Cui_seq_no = row["CUI_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["CUI_SEQ_NO"].ToString()),
                Cui_line = row["CUI_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["CUI_LINE"].ToString()),
                Cui_doc_no = row["CUI_DOC_NO"] == DBNull.Value ? string.Empty : row["CUI_DOC_NO"].ToString(),
                Cui_itm_cd = row["CUI_ITM_CD"] == DBNull.Value ? string.Empty : row["CUI_ITM_CD"].ToString(),
                Cui_itm_stus = row["CUI_ITM_STUS"] == DBNull.Value ? string.Empty : row["CUI_ITM_STUS"].ToString(),
                Cui_hs_cd = row["CUI_HS_CD"] == DBNull.Value ? string.Empty : row["CUI_HS_CD"].ToString(),
                Cui_model = row["CUI_MODEL"] == DBNull.Value ? string.Empty : row["CUI_MODEL"].ToString(),
                Cui_itm_desc = row["CUI_ITM_DESC"] == DBNull.Value ? string.Empty : row["CUI_ITM_DESC"].ToString(),
                Cui_tp = row["CUI_TP"] == DBNull.Value ? string.Empty : row["CUI_TP"].ToString(),
                Cui_tag = row["CUI_TAG"] == DBNull.Value ? string.Empty : row["CUI_TAG"].ToString(),
                Cui_pi_unit_rt = row["CUI_PI_UNIT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUI_PI_UNIT_RT"].ToString()),
                Cui_bl_unit_rt = row["CUI_BL_UNIT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUI_BL_UNIT_RT"].ToString()),
                Cui_unit_rt = row["CUI_UNIT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUI_UNIT_RT"].ToString()),
                Cui_itm_price = row["CUI_ITM_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUI_ITM_PRICE"].ToString()),
                Cui_qty = row["CUI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUI_QTY"].ToString()),
                Cui_bal_qty1 = row["CUI_BAL_QTY1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUI_BAL_QTY1"].ToString()),
                Cui_bal_qty2 = row["CUI_BAL_QTY2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUI_BAL_QTY2"].ToString()),
                Cui_bal_qty3 = row["CUI_BAL_QTY3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUI_BAL_QTY3"].ToString()),
                Cui_req_qty = row["CUI_REQ_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUI_REQ_QTY"].ToString()),
                Cui_oth_doc_no = row["CUI_OTH_DOC_NO"] == DBNull.Value ? string.Empty : row["CUI_OTH_DOC_NO"].ToString(),
                Cui_oth_doc_line = row["CUI_OTH_DOC_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["CUI_OTH_DOC_LINE"].ToString()),
                Cui_fin_no = row["CUI_FIN_NO"] == DBNull.Value ? string.Empty : row["CUI_FIN_NO"].ToString(),
                Cui_pi_no = row["CUI_PI_NO"] == DBNull.Value ? string.Empty : row["CUI_PI_NO"].ToString(),
                Cui_pi_line = row["CUI_PI_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["CUI_PI_LINE"].ToString()),
                Cui_kit_line = row["CUI_KIT_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["CUI_KIT_LINE"].ToString()),
                Cui_kit_itm_cd = row["CUI_KIT_ITM_CD"] == DBNull.Value ? string.Empty : row["CUI_KIT_ITM_CD"].ToString(),
                Cui_gross_mass = row["CUI_GROSS_MASS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUI_GROSS_MASS"].ToString()),
                Cui_net_mass = row["CUI_NET_MASS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUI_NET_MASS"].ToString()),
                Cui_bl_no = row["CUI_BL_NO"] == DBNull.Value ? string.Empty : row["CUI_BL_NO"].ToString(),
                Cui_quota = row["CUI_QUOTA"] == DBNull.Value ? string.Empty : row["CUI_QUOTA"].ToString(),
                Cui_preferance = row["CUI_PREFERANCE"] == DBNull.Value ? string.Empty : row["CUI_PREFERANCE"].ToString(),
                Cui_def_cnty = row["CUI_DEF_CNTY"] == DBNull.Value ? string.Empty : row["CUI_DEF_CNTY"].ToString(),
                Cui_orgin_cnty = row["CUI_ORGIN_CNTY"] == DBNull.Value ? string.Empty : row["CUI_ORGIN_CNTY"].ToString(),
                Cui_pkgs = row["CUI_PKGS"] == DBNull.Value ? string.Empty : row["CUI_PKGS"].ToString(),
                Cui_capacity = row["CUI_CAPACITY"] == DBNull.Value ? string.Empty : row["CUI_CAPACITY"].ToString(),
                Cui_anal_1 = row["CUI_ANAL_1"] == DBNull.Value ? string.Empty : row["CUI_ANAL_1"].ToString(),
                Cui_anal_2 = row["CUI_ANAL_2"] == DBNull.Value ? string.Empty : row["CUI_ANAL_2"].ToString(),
                Cui_anal_3 = row["CUI_ANAL_3"] == DBNull.Value ? string.Empty : row["CUI_ANAL_3"].ToString(),
                Cui_anal_4 = row["CUI_ANAL_4"] == DBNull.Value ? string.Empty : row["CUI_ANAL_4"].ToString(),
                Cui_anal_5 = row["CUI_ANAL_5"] == DBNull.Value ? string.Empty : row["CUI_ANAL_5"].ToString(),
                Cui_cre_by = row["CUI_CRE_BY"] == DBNull.Value ? string.Empty : row["CUI_CRE_BY"].ToString(),
                Cui_cre_dt = row["CUI_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["CUI_CRE_DT"].ToString()),
                Cui_cre_session = row["CUI_CRE_SESSION"] == DBNull.Value ? string.Empty : row["CUI_CRE_SESSION"].ToString(),
                Cui_mod_by = row["CUI_MOD_BY"] == DBNull.Value ? string.Empty : row["CUI_MOD_BY"].ToString(),
                Cui_mod_dt = row["CUI_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["CUI_MOD_DT"].ToString()),
                Cui_mod_session = row["CUI_MOD_SESSION"] == DBNull.Value ? string.Empty : row["CUI_MOD_SESSION"].ToString(),
                Cui_unit_amt = row["CUI_UNIT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUI_UNIT_AMT"].ToString())
            };
        }

        public static ImpCusdecItm ConverterITMGRN(DataRow row)
        {
            return new ImpCusdecItm
            {
                Cui_itm_cd = row["CUI_ITM_CD"] == DBNull.Value ? string.Empty : row["CUI_ITM_CD"].ToString(),    
                Cui_qty = row["CUI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUI_QTY"].ToString()),
                Cui_itm_desc = row["CUI_ITM_DESC"] == DBNull.Value ? string.Empty : row["CUI_ITM_DESC"].ToString(),
                Cui_doc_no = row["CUI_DOC_NO"] == DBNull.Value ? string.Empty : row["CUI_DOC_NO"].ToString(),
                Cui_base_itm = row["inb_base_itmcd"] == DBNull.Value ? string.Empty : row["inb_base_itmcd"].ToString(),
                Cui_line = row["CUI_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["CUI_LINE"].ToString()),
                Cui_oth_doc_no = row["CUI_OTH_DOC_NO"] == DBNull.Value ? string.Empty : row["CUI_OTH_DOC_NO"].ToString(),
            };
        }

        public static ImpCusdecItm ConverterITMCOST(DataRow row)
        {
            return new ImpCusdecItm
            {
                Cui_itm_cd = row["CUI_ITM_CD"] == DBNull.Value ? string.Empty : row["CUI_ITM_CD"].ToString(),
                Cui_qty = row["CUI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUI_QTY"].ToString()),
                Cui_itm_desc = row["CUI_ITM_DESC"] == DBNull.Value ? string.Empty : row["CUI_ITM_DESC"].ToString(),
                Cui_doc_no = row["CUI_DOC_NO"] == DBNull.Value ? string.Empty : row["CUI_DOC_NO"].ToString(),
                Cui_base_itm = row["inb_base_itmcd"] == DBNull.Value ? string.Empty : row["inb_base_itmcd"].ToString(),
                Cui_line = row["CUI_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["CUI_LINE"].ToString()),
            };
        }
        public static ImpCusdecItm Converter5(DataRow row)
        {
            return new ImpCusdecItm
            {
                Cui_itm_cd = row["CUI_ITM_CD"] == DBNull.Value ? string.Empty : row["CUI_ITM_CD"].ToString(),
                Cui_qty = row["CUI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUI_QTY"].ToString()),
                Cui_itm_desc = row["CUI_ITM_DESC"] == DBNull.Value ? string.Empty : row["CUI_ITM_DESC"].ToString(),
                Cui_doc_no = row["CUI_DOC_NO"] == DBNull.Value ? string.Empty : row["CUI_DOC_NO"].ToString(),
                Cui_base_itm = row["inb_base_itmcd"] == DBNull.Value ? string.Empty : row["inb_base_itmcd"].ToString(),
                Cui_line = row["CUI_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["CUI_LINE"].ToString()),
                Cui_oth_doc_no = row["CUI_OTH_DOC_NO"] == DBNull.Value ? string.Empty : row["CUI_OTH_DOC_NO"].ToString(),
                Tmp_itri_line = row["itri_line"] == DBNull.Value ? 0 : Convert.ToInt32(row["itri_line"].ToString())
            };
        }
        public static ImpCusdecItm ConverterNew(DataRow row)
        {
            return new ImpCusdecItm
            {
                Cui_seq_no = row["CUI_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["CUI_SEQ_NO"].ToString()),
                Cui_line = row["CUI_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["CUI_LINE"].ToString()),
                Cui_doc_no = row["CUI_DOC_NO"] == DBNull.Value ? string.Empty : row["CUI_DOC_NO"].ToString(),
                Cui_itm_cd = row["CUI_ITM_CD"] == DBNull.Value ? string.Empty : row["CUI_ITM_CD"].ToString(),
                Cui_itm_stus = row["CUI_ITM_STUS"] == DBNull.Value ? string.Empty : row["CUI_ITM_STUS"].ToString(),
                Cui_hs_cd = row["CUI_HS_CD"] == DBNull.Value ? string.Empty : row["CUI_HS_CD"].ToString(),
                Cui_model = row["CUI_MODEL"] == DBNull.Value ? string.Empty : row["CUI_MODEL"].ToString(),
                Cui_itm_desc = row["CUI_ITM_DESC"] == DBNull.Value ? string.Empty : row["CUI_ITM_DESC"].ToString(),
                Cui_tp = row["CUI_TP"] == DBNull.Value ? string.Empty : row["CUI_TP"].ToString(),
                Cui_tag = row["CUI_TAG"] == DBNull.Value ? string.Empty : row["CUI_TAG"].ToString(),
                Cui_pi_unit_rt = row["CUI_PI_UNIT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUI_PI_UNIT_RT"].ToString()),
                Cui_bl_unit_rt = row["CUI_BL_UNIT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUI_BL_UNIT_RT"].ToString()),
                Cui_unit_rt = row["CUI_UNIT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUI_UNIT_RT"].ToString()),
                Cui_itm_price = row["CUI_ITM_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUI_ITM_PRICE"].ToString()),
                Cui_qty = row["CUI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUI_QTY"].ToString()),
                Cui_bal_qty1 = row["CUI_BAL_QTY1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUI_BAL_QTY1"].ToString()),
                Cui_bal_qty2 = row["CUI_BAL_QTY2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUI_BAL_QTY2"].ToString()),
                Cui_bal_qty3 = row["CUI_BAL_QTY3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUI_BAL_QTY3"].ToString()),
                Cui_req_qty = row["CUI_REQ_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUI_REQ_QTY"].ToString()),
                Cui_oth_doc_no = row["CUI_OTH_DOC_NO"] == DBNull.Value ? string.Empty : row["CUI_OTH_DOC_NO"].ToString(),
                Cui_oth_doc_line = row["CUI_OTH_DOC_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["CUI_OTH_DOC_LINE"].ToString()),
                Cui_fin_no = row["CUI_FIN_NO"] == DBNull.Value ? string.Empty : row["CUI_FIN_NO"].ToString(),
                Cui_pi_no = row["CUI_PI_NO"] == DBNull.Value ? string.Empty : row["CUI_PI_NO"].ToString(),
                Cui_pi_line = row["CUI_PI_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["CUI_PI_LINE"].ToString()),
                Cui_kit_line = row["CUI_KIT_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["CUI_KIT_LINE"].ToString()),
                Cui_kit_itm_cd = row["CUI_KIT_ITM_CD"] == DBNull.Value ? string.Empty : row["CUI_KIT_ITM_CD"].ToString(),
                Cui_gross_mass = row["CUI_GROSS_MASS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUI_GROSS_MASS"].ToString()),
                Cui_net_mass = row["CUI_NET_MASS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUI_NET_MASS"].ToString()),
                Cui_bl_no = row["CUI_BL_NO"] == DBNull.Value ? string.Empty : row["CUI_BL_NO"].ToString(),
                Cui_quota = row["CUI_QUOTA"] == DBNull.Value ? string.Empty : row["CUI_QUOTA"].ToString(),
                Cui_preferance = row["CUI_PREFERANCE"] == DBNull.Value ? string.Empty : row["CUI_PREFERANCE"].ToString(),
                Cui_def_cnty = row["CUI_DEF_CNTY"] == DBNull.Value ? string.Empty : row["CUI_DEF_CNTY"].ToString(),
                Cui_orgin_cnty = row["CUI_ORGIN_CNTY"] == DBNull.Value ? string.Empty : row["CUI_ORGIN_CNTY"].ToString(),
                Cui_pkgs = row["CUI_PKGS"] == DBNull.Value ? string.Empty : row["CUI_PKGS"].ToString(),
                Cui_capacity = row["CUI_CAPACITY"] == DBNull.Value ? string.Empty : row["CUI_CAPACITY"].ToString(),
                Cui_anal_1 = row["CUI_ANAL_1"] == DBNull.Value ? string.Empty : row["CUI_ANAL_1"].ToString(),
                Cui_anal_2 = row["CUI_ANAL_2"] == DBNull.Value ? string.Empty : row["CUI_ANAL_2"].ToString(),
                Cui_anal_3 = row["CUI_ANAL_3"] == DBNull.Value ? string.Empty : row["CUI_ANAL_3"].ToString(),
                Cui_anal_4 = row["CUI_ANAL_4"] == DBNull.Value ? string.Empty : row["CUI_ANAL_4"].ToString(),
                Cui_anal_5 = row["CUI_ANAL_5"] == DBNull.Value ? string.Empty : row["CUI_ANAL_5"].ToString(),
                Cui_cre_by = row["CUI_CRE_BY"] == DBNull.Value ? string.Empty : row["CUI_CRE_BY"].ToString(),
                Cui_cre_dt = row["CUI_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["CUI_CRE_DT"].ToString()),
                Cui_cre_session = row["CUI_CRE_SESSION"] == DBNull.Value ? string.Empty : row["CUI_CRE_SESSION"].ToString(),
                Cui_mod_by = row["CUI_MOD_BY"] == DBNull.Value ? string.Empty : row["CUI_MOD_BY"].ToString(),
                Cui_mod_dt = row["CUI_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["CUI_MOD_DT"].ToString()),
                Cui_mod_session = row["CUI_MOD_SESSION"] == DBNull.Value ? string.Empty : row["CUI_MOD_SESSION"].ToString(),
                Cui_unit_amt = row["CUI_UNIT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUI_UNIT_AMT"].ToString()),
                EntryLine = row["EntryLine"] == DBNull.Value ? 0 : Convert.ToInt32(row["EntryLine"].ToString()),
            };
        }
    }
} 


