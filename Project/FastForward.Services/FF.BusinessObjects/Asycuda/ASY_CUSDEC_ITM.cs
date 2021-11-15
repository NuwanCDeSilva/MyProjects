using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Asycuda
{
    public class ASY_CUSDEC_ITM
    {
        public Int32 Aci_seq_no { get; set; }
        public String Ach_doc_no { get; set; }
        public Int32 Aci_line { get; set; }
        public String Aci_itm_cd { get; set; }
        public String Aci_hs_cd { get; set; }
        public String Aci_model { get; set; }
        public String Aci_itm_desc { get; set; }
        public Decimal Aci_qty { get; set; }
        public String Aci_uom { get; set; }
        public Decimal Aci_unit_cost { get; set; }
        public Decimal Aci_item_price { get; set; }
        public Decimal Aci_gross_mass { get; set; }
        public Decimal Aci_net_mass { get; set; }
        public String Aci_preferance { get; set; }
        public String Aci_quota { get; set; }
        public String Aci_bl_no { get; set; }
        public String Ach_othdoc1_no { get; set; }
        public Int32 Ach_othdoc1_line { get; set; }
        public String Ach_othdoc2_no { get; set; }
        public Int32 Ach_othdoc2_line { get; set; }
        public Decimal Ach_num_of_pkg { get; set; }
        public String Ach_mrk1_pkg { get; set; }
        public String Ach_knd_of_pkg { get; set; }
        public String Ach_knd_of_pkg_name { get; set; }
        public String Aci_comd_cd { get; set; }
        public String Aci_prec_1 { get; set; }
        public String Aci_prec_4 { get; set; }
        public String Ach_ext_cust_proc { get; set; }
        public String Ach_nat_cust_proc { get; set; }
        public String Ach_val_itm { get; set; }
        public String Ach_cnty_of_oregn { get; set; }
        public String Aci_desc_of_goods { get; set; }
        public decimal Aci_itm_stat_val { get; set; }
        public decimal Aci_tot_cost_itm { get; set; }
        public String Aci_rte_of_adj { get; set; }
        public String Ach_cur_cd { get; set; }
        public decimal Aci_inv_nat_curr { get; set; }
        public decimal Aci_inv_forgn_curr { get; set; }
        public decimal Aci_int_fre_nat_curr { get; set; }
        public decimal Aci_int_fre_forgn_curr { get; set; }
        public decimal Aci_ins_nat_curr { get; set; }
        public decimal Aci_ins_forgn_curr { get; set; }
        public decimal Aci_oth_cst_nat_curr { get; set; }
        public decimal Aci_oth_cst_forgn_curr { get; set; }
        public decimal Aci_dedu_nat_curr { get; set; }
        public decimal Aci_dedu_forgn_curr { get; set; }
        public decimal Aci_curr_amnt { get; set; }
        public decimal Aci_ext_fre_nat_curr { get; set; }
        public decimal Aci_ext_fre_forgn_curr { get; set; }
        public string Ach_hs_cd_desc { get; set; }

        //Dulaj 2018/Nov/13
        public decimal Ach_to_bond_line_no { get; set; }
        public string Ach_to_bond_no { get; set; }

        public string Ach_cus_dec_no { get; set; }

        public static ASY_CUSDEC_ITM Converter(DataRow row)
        {
            return new ASY_CUSDEC_ITM
            {
                Aci_seq_no = row["ACI_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ACI_SEQ_NO"].ToString()),
                Ach_doc_no = row["ACH_DOC_NO"] == DBNull.Value ? string.Empty : row["ACH_DOC_NO"].ToString(),
                Aci_line = row["ACI_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ACI_LINE"].ToString()),
                Aci_itm_cd = row["ACI_ITM_CD"] == DBNull.Value ? string.Empty : row["ACI_ITM_CD"].ToString(),
                Aci_hs_cd = row["ACI_HS_CD"] == DBNull.Value ? string.Empty : row["ACI_HS_CD"].ToString(),
                Aci_model = row["ACI_MODEL"] == DBNull.Value ? string.Empty : row["ACI_MODEL"].ToString(),
                Aci_itm_desc = row["ACI_ITM_DESC"] == DBNull.Value ? string.Empty : row["ACI_ITM_DESC"].ToString(),
                Aci_qty = row["ACI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACI_QTY"].ToString()),
                Aci_uom = row["ACI_UOM"] == DBNull.Value ? string.Empty : row["ACI_UOM"].ToString(),
                Aci_unit_cost = row["ACI_UNIT_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACI_UNIT_COST"].ToString()),
                Aci_item_price = row["ACI_ITEM_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACI_ITEM_PRICE"].ToString()),
                Aci_gross_mass = row["ACI_GROSS_MASS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACI_GROSS_MASS"].ToString()),
                Aci_net_mass = row["ACI_NET_MASS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACI_NET_MASS"].ToString()),
                Aci_preferance = row["ACI_PREFERANCE"] == DBNull.Value ? string.Empty : row["ACI_PREFERANCE"].ToString(),
                Aci_quota = row["ACI_QUOTA"] == DBNull.Value ? string.Empty : row["ACI_QUOTA"].ToString(),
                Aci_bl_no = row["ACI_BL_NO"] == DBNull.Value ? string.Empty : row["ACI_BL_NO"].ToString(),
                Ach_othdoc1_no = row["ACH_OTHDOC1_NO"] == DBNull.Value ? string.Empty : row["ACH_OTHDOC1_NO"].ToString(),
                Ach_othdoc1_line = row["ACH_OTHDOC1_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ACH_OTHDOC1_LINE"].ToString()),
                Ach_othdoc2_no = row["ACH_OTHDOC2_NO"] == DBNull.Value ? string.Empty : row["ACH_OTHDOC2_NO"].ToString(),
                Ach_othdoc2_line = row["ACH_OTHDOC2_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ACH_OTHDOC2_LINE"].ToString()),
                Ach_num_of_pkg = row["ACH_NUM_OF_PKG"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACH_NUM_OF_PKG"].ToString()),
                Ach_mrk1_pkg = row["ACH_MRK1_PKG"] == DBNull.Value ? string.Empty : row["ACH_MRK1_PKG"].ToString(),
                Ach_knd_of_pkg = row["ACH_KND_OF_PKG"] == DBNull.Value ? string.Empty : row["ACH_KND_OF_PKG"].ToString(),
                Ach_knd_of_pkg_name = row["ACH_KND_OF_PKG_NAME"] == DBNull.Value ? string.Empty : row["ACH_KND_OF_PKG_NAME"].ToString(),
                Aci_comd_cd = row["ACI_COMD_CD"] == DBNull.Value ? string.Empty : row["ACI_COMD_CD"].ToString(),
                Aci_prec_1 = row["ACI_PREC_1"] == DBNull.Value ? string.Empty : row["ACI_PREC_1"].ToString(),
                Aci_prec_4 = row["ACI_PREC_4"] == DBNull.Value ? string.Empty : row["ACI_PREC_4"].ToString(),
                Ach_ext_cust_proc = row["ACH_EXT_CUST_PROC"] == DBNull.Value ? string.Empty : row["ACH_EXT_CUST_PROC"].ToString(),
                Ach_nat_cust_proc = row["ACH_NAT_CUST_PROC"] == DBNull.Value ? string.Empty : row["ACH_NAT_CUST_PROC"].ToString(),
                Ach_val_itm = row["ACH_VAL_ITM"] == DBNull.Value ? string.Empty : row["ACH_VAL_ITM"].ToString(),
                Ach_cnty_of_oregn = row["ACH_CNTY_OF_OREGN"] == DBNull.Value ? string.Empty : row["ACH_CNTY_OF_OREGN"].ToString(),
                Aci_desc_of_goods = row["ACI_DESC_OF_GOODS"] == DBNull.Value ? string.Empty : row["ACI_DESC_OF_GOODS"].ToString(),
                Aci_itm_stat_val = row["ACI_ITM_STAT_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACI_ITM_STAT_VAL"].ToString()),
                Aci_tot_cost_itm = row["ACI_TOT_COST_ITM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACI_TOT_COST_ITM"].ToString()),
                Aci_rte_of_adj = row["ACI_RTE_OF_ADJ"] == DBNull.Value ? string.Empty : row["ACI_RTE_OF_ADJ"].ToString(),
                Ach_cur_cd = row["ACH_CUR_CD"] == DBNull.Value ? string.Empty : row["ACH_CUR_CD"].ToString(),
                Aci_inv_nat_curr = row["ACI_INV_NAT_CURR"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACI_INV_NAT_CURR"].ToString()),
                Aci_inv_forgn_curr = row["ACI_INV_FORGN_CURR"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACI_INV_FORGN_CURR"].ToString()),
                Aci_int_fre_nat_curr = row["ACI_INT_FRE_NAT_CURR"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACI_INT_FRE_NAT_CURR"].ToString()),
                Aci_int_fre_forgn_curr = row["ACI_INT_FRE_FORGN_CURR"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACI_INT_FRE_FORGN_CURR"].ToString()),
                Aci_ins_nat_curr = row["ACI_INS_NAT_CURR"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACI_INS_NAT_CURR"].ToString()),
                Aci_ins_forgn_curr = row["ACI_INS_FORGN_CURR"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACI_INS_FORGN_CURR"].ToString()),
                Aci_oth_cst_nat_curr = row["ACI_OTH_CST_NAT_CURR"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACI_OTH_CST_NAT_CURR"].ToString()),
                Aci_oth_cst_forgn_curr = row["ACI_OTH_CST_FORGN_CURR"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACI_OTH_CST_FORGN_CURR"].ToString()),
                Aci_dedu_nat_curr = row["ACI_DEDU_NAT_CURR"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACI_DEDU_NAT_CURR"].ToString()),
                Aci_dedu_forgn_curr = row["ACI_DEDU_FORGN_CURR"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACI_DEDU_FORGN_CURR"].ToString()),
                Aci_curr_amnt = row["ACI_CURR_AMNT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACI_CURR_AMNT"].ToString()),
                Ach_hs_cd_desc= row["ACI_HS_CD_DESC"] == DBNull.Value ? string.Empty : row["ACI_HS_CD_DESC"].ToString()
               
            };
        } 
    }
}
