using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Asycuda
{
    public class ASY_CUSDEC_HS_ITM
    {
        public string  Aci_hs_cd{get;set;}
        public decimal Aci_net_mass{get;set;}
        public decimal Aci_gross_mass{get;set;}
        public Decimal Aci_qty { get; set; }
        public Decimal Ach_items_qty { get; set; }
        public Decimal Aci_item_price { get; set; }
        public decimal Ach_num_of_pkg { get; set; }
        public decimal Aci_stat_val { get; set; }
        public string Aci_hs_cd_desc{get;set;}
            
          public static ASY_CUSDEC_HS_ITM Converter(DataRow row)
        {
            return new ASY_CUSDEC_HS_ITM
            {
                Aci_hs_cd = row["ACI_HS_CD"] == DBNull.Value ? string.Empty : row["ACI_HS_CD"].ToString(),
                Aci_net_mass = row["ACI_NET_MASS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACI_NET_MASS"].ToString()),
                Aci_gross_mass = row["ACI_GROSS_MASS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACI_GROSS_MASS"].ToString()),
                Aci_qty = row["ACI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACI_QTY"].ToString()),
                Ach_items_qty = row["ACH_ITEMS_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACH_ITEMS_QTY"].ToString()),
                Aci_item_price = row["ACI_ITEM_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACI_ITEM_PRICE"].ToString()),
                Ach_num_of_pkg = row["ACH_NUM_OF_PKG"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACH_NUM_OF_PKG"].ToString()),
                Aci_stat_val = row["ACI_STAT_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACI_STAT_VAL"].ToString()),
                Aci_hs_cd_desc = row["ACI_HS_CD_DESC"] == DBNull.Value ? string.Empty : row["ACI_HS_CD_DESC"].ToString()
            };
          }
    }
    public class ASY_CUSDEC_ITM_DET {
        public String Aci_itm_desc { get; set; }
        public String Ach_exp_cnty_cd { get; set; }
        public String Aci_model { get; set; }
        public String Aci_uom { get; set; }
        public Decimal Aci_qty { get; set; }
        public static ASY_CUSDEC_ITM_DET Converter(DataRow row)
        {
            return new ASY_CUSDEC_ITM_DET
            {
                Aci_itm_desc = row["ACI_ITM_DESC"] == DBNull.Value ? string.Empty : row["ACI_ITM_DESC"].ToString(),
                Ach_exp_cnty_cd = row["ACH_EXP_CNTY_CD"] == DBNull.Value ? string.Empty : row["ACH_EXP_CNTY_CD"].ToString(),
                Aci_model = row["ACI_MODEL"] == DBNull.Value ? string.Empty : row["ACI_MODEL"].ToString(),
                Aci_uom = row["ACI_UOM"] == DBNull.Value ? string.Empty : row["ACI_UOM"].ToString(),
                Aci_qty = row["ACI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACI_QTY"].ToString())

            };
     }
    }
}
