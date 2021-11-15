using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
   public class SALE_BAL_DETAILS
    {
        public String Com_code { get; set; }
        public String Com_name { get; set; }
        public String Pwd_by { get; set; }
        public String Pc_code { get; set; }
        public String Pc_desc { get; set; }
        public String Do_loc { get; set; }
        public String Do_loc_desc { get; set; }
        public String Cust_code { get; set; }
        public String Cust_name { get; set; }
        public String Ex_code { get; set; }
        public String Ex_name { get; set; }
        public String Inv_no { get; set; }
        public DateTime Inv_date { get; set; }
        public String Do_no { get; set; }
        public DateTime Do_date { get; set; }
        public String Inv_type { get; set; }
        public String Inv_tp_desc { get; set; }
        public String Item_code { get; set; }
        public String Item_desc { get; set; }
        public String Model { get; set; }
        public String Brand { get; set; }
        public String Cat1 { get; set; }
        public String Cat2 { get; set; }
        public String Cat3 { get; set; }
        public Decimal Qty { get; set; }
        public Decimal Gross_amt { get; set; }
        public Decimal Disc_amt { get; set; }
        public Decimal Tax_amt { get; set; }
        public Decimal Net_amt { get; set; }
        public Decimal Tot_amt { get; set; }
        public String Cat1_desc { get; set; }
        public String Cat2_desc { get; set; }
        public String Cat3_desc { get; set; }
        public String Stk_type { get; set; }
        public String Stk_typedesc { get; set; }
        public String Sah_man_ref { get; set; }
        public String Mi_brand { get; set; }
        public String Telephone { get; set; }
        public String Nic { get; set; }
        public String Promoter_code { get; set; }
        public String Promoter_name { get; set; }
        public Int32 Do_item_line { get; set; }
        public Int32 Do_batch_line { get; set; }
        public String Job_no { get; set; }
        public String Cat4 { get; set; }
        public String Cat5 { get; set; }
        public String Cat4_desc { get; set; }
        public String Cat5_desc { get; set; }
        public Decimal Cost_amt { get; set; }
        public String Main_itm { get; set; }
        public Decimal Free_itm_cost { get; set; }
        public String Inv_subtp { get; set; }
        public String Price_book { get; set; }
        public String Pb_lvl { get; set; }
        public Int32 Cash_dir { get; set; }
        public string Currency { get; set; }
        public decimal anal_7 { get; set; }
        public decimal anal_8 { get; set; }
        public decimal DiscountRate { get; set; }
        public string PromotionType { get; set; }

        public decimal currgp { get; set; }

        public decimal lastgp { get; set; }
        public decimal currentsale { get; set; }
        public decimal lastsale { get; set; }

        public decimal ManagerCommissionRate { get; set; }
        public string ManagerCode { get; set; }

        public decimal EmployeeCommission { get; set; }
        public string EmployeeCode { get; set; }

        public decimal ExecCommission { get; set; }
        public static SALE_BAL_DETAILS Converter(DataRow row)
        {
            return new SALE_BAL_DETAILS
            {
                Com_code = row["COM_CODE"] == DBNull.Value ? string.Empty : row["COM_CODE"].ToString(),
                Pc_code = row["PC_CODE"] == DBNull.Value ? string.Empty : row["PC_CODE"].ToString(),
                Pc_desc = row["PC_DESC"] == DBNull.Value ? string.Empty : row["PC_DESC"].ToString(),
                Do_loc = row["DO_LOC"] == DBNull.Value ? string.Empty : row["DO_LOC"].ToString(),
                Do_loc_desc = row["DO_LOC_DESC"] == DBNull.Value ? string.Empty : row["DO_LOC_DESC"].ToString(),
                Cust_code = row["CUST_CODE"] == DBNull.Value ? string.Empty : row["CUST_CODE"].ToString(),
                Cust_name = row["CUST_NAME"] == DBNull.Value ? string.Empty : row["CUST_NAME"].ToString(),
                Ex_code = row["EX_CODE"] == DBNull.Value ? string.Empty : row["EX_CODE"].ToString(),
                Ex_name = row["EX_NAME"] == DBNull.Value ? string.Empty : row["EX_NAME"].ToString(),
                Inv_no = row["INV_NO"] == DBNull.Value ? string.Empty : row["INV_NO"].ToString(),
                Inv_date = row["INV_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INV_DATE"].ToString()),
                Do_no = row["DO_NO"] == DBNull.Value ? string.Empty : row["DO_NO"].ToString(),
                Do_date = row["DO_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["DO_DATE"].ToString()),
                Inv_type = row["INV_TYPE"] == DBNull.Value ? string.Empty : row["INV_TYPE"].ToString(),
                Item_code = row["ITEM_CODE"] == DBNull.Value ? string.Empty : row["ITEM_CODE"].ToString(),
                Item_desc = row["ITEM_DESC"] == DBNull.Value ? string.Empty : row["ITEM_DESC"].ToString(),
                Model = row["MODEL"] == DBNull.Value ? string.Empty : row["MODEL"].ToString(),
                Brand = row["BRAND"] == DBNull.Value ? string.Empty : row["BRAND"].ToString(),
                Cat1 = row["CAT1"] == DBNull.Value ? string.Empty : row["CAT1"].ToString(),
                Cat2 = row["CAT2"] == DBNull.Value ? string.Empty : row["CAT2"].ToString(),
                Cat3 = row["CAT3"] == DBNull.Value ? string.Empty : row["CAT3"].ToString(),
                Qty = row["QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QTY"].ToString()),
                Gross_amt = row["GROSS_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GROSS_AMT"].ToString()),
                Disc_amt = row["DISC_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DISC_AMT"].ToString()),
                Tax_amt = row["TAX_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TAX_AMT"].ToString()),
                Net_amt = row["NET_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["NET_AMT"].ToString()),
                Tot_amt = row["TOT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TOT_AMT"].ToString()),
                Cat1_desc = row["CAT1_DESC"] == DBNull.Value ? string.Empty : row["CAT1_DESC"].ToString(),
                Cat2_desc = row["CAT2_DESC"] == DBNull.Value ? string.Empty : row["CAT2_DESC"].ToString(),
                Cat3_desc = row["CAT3_DESC"] == DBNull.Value ? string.Empty : row["CAT3_DESC"].ToString(),
                Cat4 = row["CAT4"] == DBNull.Value ? string.Empty : row["CAT4"].ToString(),
                Cat5 = row["CAT5"] == DBNull.Value ? string.Empty : row["CAT5"].ToString(),
                Cat4_desc = row["CAT4_DESC"] == DBNull.Value ? string.Empty : row["CAT4_DESC"].ToString(),
                Cat5_desc = row["CAT5_DESC"] == DBNull.Value ? string.Empty : row["CAT5_DESC"].ToString(),
                Inv_subtp = row["INV_SUBTP"] == DBNull.Value ? string.Empty : row["INV_SUBTP"].ToString(),
                Price_book = row["PRICE_BOOK"] == DBNull.Value ? string.Empty : row["PRICE_BOOK"].ToString(),
                Pb_lvl = row["PB_LVL"] == DBNull.Value ? string.Empty : row["PB_LVL"].ToString(),
                anal_7 = row["anal_7"] == DBNull.Value ? 0 : Convert.ToDecimal(row["anal_7"].ToString()),
                anal_8 = row["anal_8"] == DBNull.Value ? 0 : Convert.ToDecimal(row["anal_8"].ToString()),
                //DiscountRate = row["DiscountRate"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DiscountRate"].ToString()),
                //PromotionType = row["PromotionType"] == DBNull.Value ? string.Empty : row["PromotionType"].ToString(),
            };
        }
        public static SALE_BAL_DETAILS Converter2(DataRow row)
        {
            return new SALE_BAL_DETAILS
            {
                Com_code = row["COM_CODE"] == DBNull.Value ? string.Empty : row["COM_CODE"].ToString(),
                Pc_code = row["PC_CODE"] == DBNull.Value ? string.Empty : row["PC_CODE"].ToString(),
                Pc_desc = row["PC_DESC"] == DBNull.Value ? string.Empty : row["PC_DESC"].ToString(),
                Do_loc = row["DO_LOC"] == DBNull.Value ? string.Empty : row["DO_LOC"].ToString(),
                Do_loc_desc = row["DO_LOC_DESC"] == DBNull.Value ? string.Empty : row["DO_LOC_DESC"].ToString(),
                Cust_code = row["CUST_CODE"] == DBNull.Value ? string.Empty : row["CUST_CODE"].ToString(),
                Cust_name = row["CUST_NAME"] == DBNull.Value ? string.Empty : row["CUST_NAME"].ToString(),
                Ex_code = row["EX_CODE"] == DBNull.Value ? string.Empty : row["EX_CODE"].ToString(),
                Ex_name = row["EX_NAME"] == DBNull.Value ? string.Empty : row["EX_NAME"].ToString(),
                Inv_no = row["INV_NO"] == DBNull.Value ? string.Empty : row["INV_NO"].ToString(),
                Inv_date = row["INV_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INV_DATE"].ToString()),
                Do_no = row["DO_NO"] == DBNull.Value ? string.Empty : row["DO_NO"].ToString(),
                Do_date = row["DO_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["DO_DATE"].ToString()),
                Inv_type = row["INV_TYPE"] == DBNull.Value ? string.Empty : row["INV_TYPE"].ToString(),
                Item_code = row["ITEM_CODE"] == DBNull.Value ? string.Empty : row["ITEM_CODE"].ToString(),
                Item_desc = row["ITEM_DESC"] == DBNull.Value ? string.Empty : row["ITEM_DESC"].ToString(),
                Model = row["MODEL"] == DBNull.Value ? string.Empty : row["MODEL"].ToString(),
                Brand = row["BRAND"] == DBNull.Value ? string.Empty : row["BRAND"].ToString(),
                Cat1 = row["CAT1"] == DBNull.Value ? string.Empty : row["CAT1"].ToString(),
                Cat2 = row["CAT2"] == DBNull.Value ? string.Empty : row["CAT2"].ToString(),
                Cat3 = row["CAT3"] == DBNull.Value ? string.Empty : row["CAT3"].ToString(),
                Qty = row["QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QTY"].ToString()),
                Gross_amt = row["GROSS_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GROSS_AMT"].ToString()),
                Disc_amt = row["DISC_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DISC_AMT"].ToString()),
                Tax_amt = row["TAX_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TAX_AMT"].ToString()),
                Net_amt = row["NET_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["NET_AMT"].ToString()),
                Tot_amt = row["TOT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TOT_AMT"].ToString()),
                Cat1_desc = row["CAT1_DESC"] == DBNull.Value ? string.Empty : row["CAT1_DESC"].ToString(),
                Cat2_desc = row["CAT2_DESC"] == DBNull.Value ? string.Empty : row["CAT2_DESC"].ToString(),
                Cat3_desc = row["CAT3_DESC"] == DBNull.Value ? string.Empty : row["CAT3_DESC"].ToString(),
                Cat4 = row["CAT4"] == DBNull.Value ? string.Empty : row["CAT4"].ToString(),
                Cat5 = row["CAT5"] == DBNull.Value ? string.Empty : row["CAT5"].ToString(),
                Cat4_desc = row["CAT4_DESC"] == DBNull.Value ? string.Empty : row["CAT4_DESC"].ToString(),
                Cat5_desc = row["CAT5_DESC"] == DBNull.Value ? string.Empty : row["CAT5_DESC"].ToString(),
                Inv_subtp = row["INV_SUBTP"] == DBNull.Value ? string.Empty : row["INV_SUBTP"].ToString(),
                Price_book = row["PRICE_BOOK"] == DBNull.Value ? string.Empty : row["PRICE_BOOK"].ToString(),
                Pb_lvl = row["PB_LVL"] == DBNull.Value ? string.Empty : row["PB_LVL"].ToString()
            };
        }
    }
}
