using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class DELI_SALE
    {
        public string com_code { get; set; }
        public string com_name { get; set; }
        public string pwd_by { get; set; }
        public string pc_code { get; set; }
        public string pc_desc { get; set; }
        public string do_loc { get; set; }
        public string do_loc_desc { get; set; }
        public string cust_code { get; set; }
        public string cust_name { get; set; }
        public string ex_code { get; set; }
        public string ex_name { get; set; }
        public string inv_no { get; set; }
        public string inv_date { get; set; }
        public string do_no { get; set; }
        public string do_date { get; set; }
        public string inv_type { get; set; }
        public string inv_tp_desc { get; set; }
        public string item_code { get; set; }
        public string item_desc { get; set; }
        public string model { get; set; }
        public string brand { get; set; }
        public string cat1 { get; set; }
        public string cat2 { get; set; }
        public string cat3 { get; set; }
        public string qty { get; set; }
        public string gross_amt { get; set; }
        public string disc_amt { get; set; }
        public string tax_amt { get; set; }
        public string net_amt { get; set; }
        public string tot_amt { get; set; }
        public string cat1_desc { get; set; }
        public string cat2_desc { get; set; }
        public string cat3_desc { get; set; }
        public string stk_type { get; set; }
        public string stk_typedesc { get; set; }
        public string sah_man_ref { get; set; }
        public string mi_brand { get; set; }
        public string telephone { get; set; }
        public string nic { get; set; }
        public string promoter_code { get; set; }
        public string promoter_name { get; set; }
        public string do_item_line { get; set; }
        public string do_batch_line { get; set; }
        public string job_no { get; set; }
        public string cat4 { get; set; }
        public string cat5 { get; set; }
        public string cat4_desc { get; set; }
        public string cat5_desc { get; set; }
        public string cost_amt { get; set; }
        public string main_itm { get; set; }
        public string free_itm_cost { get; set; }
        public string inv_subtp { get; set; }
        public string price_book { get; set; }
        public string pb_lvl { get; set; }
        public string cash_dir { get; set; }
        public string icr_class { get; set; }
        public string stock_qty { get; set; }
        public string yoy_qty { get; set; }
        public string m1_qty { get; set; }
        public string m2_qty { get; set; }
        public string m3_qty { get; set; }


        public static DELI_SALE Converter(DataRow row)
        {
            return new DELI_SALE
            {
                com_code = row["com_code"] == DBNull.Value ? string.Empty : row["com_code"].ToString(),
                com_name = row["com_name"] == DBNull.Value ? string.Empty : row["com_name"].ToString(),
                pwd_by = row["pwd_by"] == DBNull.Value ? string.Empty : row["pwd_by"].ToString(),
                pc_code = row["pc_code"] == DBNull.Value ? string.Empty : row["pc_code"].ToString(),
                pc_desc = row["pc_desc"] == DBNull.Value ? string.Empty : row["pc_desc"].ToString(),
                do_loc = row["do_loc"] == DBNull.Value ? string.Empty : row["do_loc"].ToString(),
                do_loc_desc = row["do_loc_desc"] == DBNull.Value ? string.Empty : row["do_loc_desc"].ToString(),
                cust_code = row["cust_code"] == DBNull.Value ? string.Empty : row["cust_code"].ToString(),
                cust_name = row["cust_name"] == DBNull.Value ? string.Empty : row["cust_name"].ToString(),
                ex_code = row["ex_code"] == DBNull.Value ? string.Empty : row["ex_code"].ToString(),
                ex_name = row["ex_name"] == DBNull.Value ? string.Empty : row["ex_name"].ToString(),
                inv_no = row["inv_no"] == DBNull.Value ? string.Empty : row["inv_no"].ToString(),
                inv_date = row["inv_date"] == DBNull.Value ? string.Empty : row["inv_date"].ToString(),
                do_no = row["do_no"] == DBNull.Value ? string.Empty : row["do_no"].ToString(),
                do_date = row["do_date"] == DBNull.Value ? string.Empty : row["do_date"].ToString(),
                inv_type = row["inv_type"] == DBNull.Value ? string.Empty : row["inv_type"].ToString(),
                inv_tp_desc = row["inv_tp_desc"] == DBNull.Value ? string.Empty : row["inv_tp_desc"].ToString(),
                item_code = row["item_code"] == DBNull.Value ? string.Empty : row["item_code"].ToString(),
                item_desc = row["item_desc"] == DBNull.Value ? string.Empty : row["item_desc"].ToString(),
                model = row["model"] == DBNull.Value ? string.Empty : row["model"].ToString(),
                brand = row["brand"] == DBNull.Value ? string.Empty : row["brand"].ToString(),
                cat1 = row["cat1"] == DBNull.Value ? string.Empty : row["cat1"].ToString(),
                cat2 = row["cat2"] == DBNull.Value ? string.Empty : row["cat2"].ToString(),
                cat3 = row["cat3"] == DBNull.Value ? string.Empty : row["cat3"].ToString(),
                qty = row["qty"] == DBNull.Value ? string.Empty : row["qty"].ToString(),
                gross_amt = row["gross_amt"] == DBNull.Value ? string.Empty : row["gross_amt"].ToString(),
                disc_amt = row["disc_amt"] == DBNull.Value ? string.Empty : row["disc_amt"].ToString(),
                tax_amt = row["tax_amt"] == DBNull.Value ? string.Empty : row["tax_amt"].ToString(),
                net_amt = row["net_amt"] == DBNull.Value ? string.Empty : row["net_amt"].ToString(),
                tot_amt = row["tot_amt"] == DBNull.Value ? string.Empty : row["tot_amt"].ToString(),
                cat1_desc = row["cat1_desc"] == DBNull.Value ? string.Empty : row["cat1_desc"].ToString(),
                cat2_desc = row["cat2_desc"] == DBNull.Value ? string.Empty : row["cat2_desc"].ToString(),
                cat3_desc = row["cat3_desc"] == DBNull.Value ? string.Empty : row["cat3_desc"].ToString(),
                stk_type = row["stk_type"] == DBNull.Value ? string.Empty : row["stk_type"].ToString(),
                stk_typedesc = row["stk_typedesc"] == DBNull.Value ? string.Empty : row["stk_typedesc"].ToString(),
                sah_man_ref = row["sah_man_ref"] == DBNull.Value ? string.Empty : row["sah_man_ref"].ToString(),
                mi_brand = row["mi_brand"] == DBNull.Value ? string.Empty : row["mi_brand"].ToString(),

                telephone = row["telephone"] == DBNull.Value ? string.Empty : row["telephone"].ToString(),
                nic = row["nic"] == DBNull.Value ? string.Empty : row["nic"].ToString(),
                promoter_code = row["promoter_code"] == DBNull.Value ? string.Empty : row["promoter_code"].ToString(),
                promoter_name = row["promoter_name"] == DBNull.Value ? string.Empty : row["promoter_name"].ToString(),
                do_item_line = row["do_item_line"] == DBNull.Value ? string.Empty : row["do_item_line"].ToString(),
                do_batch_line = row["do_batch_line"] == DBNull.Value ? string.Empty : row["do_batch_line"].ToString(),
                job_no = row["job_no"] == DBNull.Value ? string.Empty : row["job_no"].ToString(),
                cat4 = row["cat4"] == DBNull.Value ? string.Empty : row["cat4"].ToString(),
                cat5 = row["cat5"] == DBNull.Value ? string.Empty : row["cat5"].ToString(),
                cat4_desc = row["cat4_desc"] == DBNull.Value ? string.Empty : row["cat4_desc"].ToString(),
                cat5_desc = row["cat5_desc"] == DBNull.Value ? string.Empty : row["cat5_desc"].ToString(),
                cost_amt = row["cost_amt"] == DBNull.Value ? string.Empty : row["cost_amt"].ToString(),
                main_itm = row["main_itm"] == DBNull.Value ? string.Empty : row["main_itm"].ToString(),
                free_itm_cost = row["free_itm_cost"] == DBNull.Value ? string.Empty : row["free_itm_cost"].ToString(),
                inv_subtp = row["inv_subtp"] == DBNull.Value ? string.Empty : row["inv_subtp"].ToString(),
                price_book = row["price_book"] == DBNull.Value ? string.Empty : row["price_book"].ToString(),
                pb_lvl = row["pb_lvl"] == DBNull.Value ? string.Empty : row["pb_lvl"].ToString(),
                cash_dir = row["cash_dir"] == DBNull.Value ? string.Empty : row["cash_dir"].ToString(),
                icr_class = row["icr_class"] == DBNull.Value ? string.Empty : row["icr_class"].ToString()
            };
        }
    }
}
