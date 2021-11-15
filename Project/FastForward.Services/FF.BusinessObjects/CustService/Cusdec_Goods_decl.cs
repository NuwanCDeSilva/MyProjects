using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.CustService
{
  public  class Cusdec_Goods_decl
    {
        public Int32 cui_line { get; set; }
        public string cui_itm_desc { get; set; }
        public decimal cui_gross_mass { get; set; }
        public decimal cui_net_mass { get; set; }
        public string cui_bl_no { get; set; }
        public string cui_orgin_cnty { get; set; }
        public string cui_pkgs { get; set; }
        public string cui_hs_cd { get; set; }
        public decimal cui_qty { get; set; }
        public decimal cui_bal_qty1 { get; set; }
        public decimal cui_bal_qty2 { get; set; }
        public decimal cui_bal_qty3 { get; set; }
        public decimal cui_req_qty { get; set; }
        public decimal cui_itm_price { get; set; }
        public string cuic_ele_cd { get; set; }
        public decimal cuic_ele_base { get; set; }
        public decimal cuic_ele_rt { get; set; }
        public decimal cuic_ele_amt { get; set; }
        public Int32 cuic_ele_mp { get; set; }
        public string HSDescription { get; set; }
        public decimal cui_itm_price2 { get; set; }
        public decimal cui_unit_rt { get; set; }
        public string cuic_itm_cd { get; set; }
        public string cui_preferance { get; set; }

        public string cui_model { get; set; }
        public Int32 cui_oth_doc_line { get; set; }
      public string MainHS { get; set; }

      


      public static Cusdec_Goods_decl Converter(DataRow row)
        {
            return new Cusdec_Goods_decl
            {
                cui_line = row["cui_line"] == DBNull.Value ? 0 : Convert.ToInt32(row["cui_line"].ToString()),
                cui_itm_desc = row["cui_itm_desc"] == DBNull.Value ? string.Empty : row["cui_itm_desc"].ToString(),
                cui_gross_mass = row["cui_gross_mass"] == DBNull.Value ? 0 : Convert.ToDecimal(row["cui_gross_mass"].ToString()),
                cui_net_mass = row["cui_net_mass"] == DBNull.Value ? 0 : Convert.ToDecimal(row["cui_net_mass"].ToString()),
                cui_bl_no = row["cui_bl_no"] == DBNull.Value ? string.Empty : row["cui_bl_no"].ToString(),
                cui_orgin_cnty = row["cui_orgin_cnty"] == DBNull.Value ? string.Empty : row["cui_orgin_cnty"].ToString(),
                cui_pkgs = row["cui_pkgs"] == DBNull.Value ? string.Empty : row["cui_pkgs"].ToString(),
                cui_hs_cd = row["cui_hs_cd"] == DBNull.Value ? string.Empty : row["cui_hs_cd"].ToString(),
                cui_qty = row["cui_qty"] == DBNull.Value ? 0 : Convert.ToDecimal(row["cui_qty"].ToString()),
                cui_bal_qty1 = row["cui_bal_qty1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["cui_bal_qty1"].ToString()),
                cui_bal_qty2 = row["cui_bal_qty2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["cui_bal_qty2"].ToString()),
                cui_bal_qty3 = row["cui_bal_qty3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["cui_bal_qty3"].ToString()),
                cui_req_qty = row["cui_req_qty"] == DBNull.Value ? 0 : Convert.ToDecimal(row["cui_req_qty"].ToString()),
                cui_itm_price = row["cui_itm_price"] == DBNull.Value ? 0 : Convert.ToDecimal(row["cui_itm_price"].ToString()),
                cuic_ele_cd = row["cuic_ele_cd"] == DBNull.Value ? string.Empty : row["cuic_ele_cd"].ToString(),
                cuic_ele_base = row["cuic_ele_base"] == DBNull.Value ? 0 : Convert.ToDecimal(row["cuic_ele_base"].ToString()),
                cuic_ele_rt = row["cuic_ele_rt"] == DBNull.Value ? 0 : Convert.ToDecimal(row["cuic_ele_rt"].ToString()),
                cuic_ele_amt = row["cuic_ele_amt"] == DBNull.Value ? 0 : Convert.ToDecimal(row["cuic_ele_amt"].ToString()),
                cuic_ele_mp = row["cuic_ele_mp"] == DBNull.Value ? 0 : Convert.ToInt32(row["cuic_ele_mp"].ToString()),
                HSDescription = row["HSDescription"] == DBNull.Value ? string.Empty : row["HSDescription"].ToString(),
                cui_itm_price2 = row["cui_itm_price2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["cui_itm_price2"].ToString()),
                cui_unit_rt = row["cui_unit_rt"] == DBNull.Value ? 0 : Convert.ToDecimal( row["cui_unit_rt"].ToString()),
                cuic_itm_cd = row["cuic_itm_cd"] == DBNull.Value ? string.Empty : row["cuic_itm_cd"].ToString(),
                cui_preferance = row["cui_preferance"] == DBNull.Value ? string.Empty : row["cui_preferance"].ToString(),
                cui_model = row["cui_model"] == DBNull.Value ? string.Empty : row["cui_model"].ToString(),
                cui_oth_doc_line = row["cui_oth_doc_line"] == DBNull.Value ? 0 : Convert.ToInt32(row["cui_oth_doc_line"].ToString()),
                MainHS = row["MainHS"] == DBNull.Value ? string.Empty : row["MainHS"].ToString(),
            };
      }


    }
}
