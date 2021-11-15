using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class ITM_CUR_AGE_DET
    {
        public string com_cd { get; set; }
        public string com_desc { get; set; }
        public string powered_by { get; set; }
        public string loc_cd { get; set; }
        public string loc_desc { get; set; }
        public string supp_cd { get; set; }
        public string supp_name { get; set; }
        public string item_cat1 { get; set; }
        public string item_cat1_desc { get; set; }
        public string item_cat2 { get; set; }
        public string item_cat2_desc { get; set; }
        public string item_cat3 { get; set; }
        public string item_cat3_desc { get; set; }
        public string item_cat4 { get; set; }
        public string item_cat4_desc { get; set; }
        public string item_cat5 { get; set; }
        public string item_cat5_desc { get; set; }
        public string item_cd { get; set; }
        public string item_desc { get; set; }
        public string item_model { get; set; }
        public string item_brand { get; set; }
        public string item_brand_desc { get; set; }
        public string brand_mngr { get; set; }
        public string brand_desc { get; set; }
        public string sell_price { get; set; }
        public string sec1_qty { get; set; }
        public string sec1_val { get; set; }
        public string sec2_qty { get; set; }
        public string sec2_val { get; set; }
        public string sec3_qty { get; set; }
        public string sec3_val { get; set; }
        public string sec4_qty { get; set; }
        public string sec4_val { get; set; }
        public string sec5_qty { get; set; }
        public string sec5_val { get; set; }
        public string sec6_qty { get; set; }
        public string sec6_val { get; set; }
        public string tot_qty { get; set; }
        public string tot_val { get; set; }
        public string item_stus { get; set; }
        public static ITM_CUR_AGE_DET Converter(DataRow row)
        {
            return new ITM_CUR_AGE_DET
            {
                com_cd = row["com_cd"] == DBNull.Value ? string.Empty : row["com_cd"].ToString(),
                com_desc = row["com_desc"] == DBNull.Value ? string.Empty : row["com_desc"].ToString(),
                powered_by = row["powered_by"] == DBNull.Value ? string.Empty : row["powered_by"].ToString(),
                loc_cd = row["loc_cd"] == DBNull.Value ? string.Empty : row["loc_cd"].ToString(),
                loc_desc = row["loc_desc"] == DBNull.Value ? string.Empty : row["loc_desc"].ToString(),
                supp_cd = row["supp_cd"] == DBNull.Value ? string.Empty : row["supp_cd"].ToString(),
                supp_name = row["supp_name"] == DBNull.Value ? string.Empty : row["supp_name"].ToString(),
                item_cat1 = row["item_cat1"] == DBNull.Value ? string.Empty : row["item_cat1"].ToString(),
                item_cat1_desc = row["item_cat1_desc"] == DBNull.Value ? string.Empty : row["item_cat1_desc"].ToString(),
                item_cat2 = row["item_cat2"] == DBNull.Value ? string.Empty : row["item_cat2"].ToString(),
                item_cat2_desc = row["item_cat2_desc"] == DBNull.Value ? string.Empty : row["item_cat2_desc"].ToString(),
                item_cat3 = row["item_cat3"] == DBNull.Value ? string.Empty : row["item_cat3"].ToString(),
                item_cat3_desc = row["item_cat3_desc"] == DBNull.Value ? string.Empty : row["item_cat3_desc"].ToString(),
                item_cat4 = row["item_cat4"] == DBNull.Value ? string.Empty : row["item_cat4"].ToString(),
                item_cat4_desc = row["item_cat4_desc"] == DBNull.Value ? string.Empty : row["item_cat4_desc"].ToString(),
                item_cat5 = row["item_cat5"] == DBNull.Value ? string.Empty : row["item_cat5"].ToString(),
                item_cat5_desc = row["item_cat5_desc"] == DBNull.Value ? string.Empty : row["item_cat5_desc"].ToString(),
                item_cd = row["item_cd"] == DBNull.Value ? string.Empty : row["item_cd"].ToString(),
                item_desc = row["item_desc"] == DBNull.Value ? string.Empty : row["item_desc"].ToString(),
                item_model = row["item_model"] == DBNull.Value ? string.Empty : row["item_model"].ToString(),
                item_brand = row["item_brand"] == DBNull.Value ? string.Empty : row["item_brand"].ToString(),
                brand_desc = row["brand_desc"] == DBNull.Value ? string.Empty : row["brand_desc"].ToString(),
                sell_price = row["sell_price"] == DBNull.Value ? string.Empty : row["sell_price"].ToString(),
                sec1_qty = row["sec1_qty"] == DBNull.Value ? string.Empty : row["sec1_qty"].ToString(),
                sec1_val = row["sec1_val"] == DBNull.Value ? string.Empty : row["sec1_val"].ToString(),
                sec2_qty = row["sec2_qty"] == DBNull.Value ? string.Empty : row["sec2_qty"].ToString(),
                sec2_val = row["sec2_val"] == DBNull.Value ? string.Empty : row["sec2_val"].ToString(),
                sec3_qty = row["sec3_qty"] == DBNull.Value ? string.Empty : row["sec3_qty"].ToString(),
                sec3_val = row["sec3_val"] == DBNull.Value ? string.Empty : row["sec3_val"].ToString(),
                sec4_qty = row["sec4_qty"] == DBNull.Value ? string.Empty : row["sec4_qty"].ToString(),
                sec4_val = row["sec4_val"] == DBNull.Value ? string.Empty : row["sec4_val"].ToString(),
                sec5_qty = row["sec5_qty"] == DBNull.Value ? string.Empty : row["sec5_qty"].ToString(),
                sec5_val = row["sec5_val"] == DBNull.Value ? string.Empty : row["sec5_val"].ToString(),
                sec6_qty = row["sec6_qty"] == DBNull.Value ? string.Empty : row["sec6_qty"].ToString(),
                sec6_val = row["sec6_val"] == DBNull.Value ? string.Empty : row["sec6_val"].ToString(),
                tot_val = row["tot_val"] == DBNull.Value ? string.Empty : row["tot_val"].ToString(),
                item_stus = row["item_stus"] == DBNull.Value ? string.Empty : row["item_stus"].ToString()
            };
        }

        public static ITM_CUR_AGE_DET ConverterSub(DataRow row)
        {
            return new ITM_CUR_AGE_DET
            {
                item_cat1 = row["item_cat1"] == DBNull.Value ? string.Empty : row["item_cat1"].ToString(),
                item_cat2 = row["item_cat2"] == DBNull.Value ? string.Empty : row["item_cat2"].ToString(),
                item_cat3 = row["item_cat3"] == DBNull.Value ? string.Empty : row["item_cat3"].ToString(),
                sec1_qty = row["sec1_qty"] == DBNull.Value ? string.Empty : row["sec1_qty"].ToString(),
                sec1_val = row["sec1_val"] == DBNull.Value ? string.Empty : row["sec1_val"].ToString(),
                sec2_qty = row["sec2_qty"] == DBNull.Value ? string.Empty : row["sec2_qty"].ToString(),
                sec2_val = row["sec2_val"] == DBNull.Value ? string.Empty : row["sec2_val"].ToString(),
                sec3_qty = row["sec3_qty"] == DBNull.Value ? string.Empty : row["sec3_qty"].ToString(),
                sec3_val = row["sec3_val"] == DBNull.Value ? string.Empty : row["sec3_val"].ToString(),
                sec4_qty = row["sec4_qty"] == DBNull.Value ? string.Empty : row["sec4_qty"].ToString(),
                sec4_val = row["sec4_val"] == DBNull.Value ? string.Empty : row["sec4_val"].ToString(),
                sec5_qty = row["sec5_qty"] == DBNull.Value ? string.Empty : row["sec5_qty"].ToString(),
                sec5_val = row["sec5_val"] == DBNull.Value ? string.Empty : row["sec5_val"].ToString(),
                sec6_qty = row["sec6_qty"] == DBNull.Value ? string.Empty : row["sec6_qty"].ToString(),
                sec6_val = row["sec6_val"] == DBNull.Value ? string.Empty : row["sec6_val"].ToString(),
                tot_val = row["tot_val"] == DBNull.Value ? string.Empty : row["tot_val"].ToString(),
                tot_qty = row["tot_qty"] == DBNull.Value ? string.Empty : row["tot_qty"].ToString(),
                item_stus = row["item_stus"] == DBNull.Value ? string.Empty : row["item_stus"].ToString(),
                item_brand = row["item_brand"] == DBNull.Value ? string.Empty : row["item_brand"].ToString(),
                brand_mngr = row["brand_mngr"] == DBNull.Value ? string.Empty : row["brand_mngr"].ToString(),
                item_brand_desc = row["item_brand_desc"] == DBNull.Value ? string.Empty : row["item_brand_desc"].ToString()
            };
        }
       
    }
    public class ITM_CUR_AGE_DETALL
    {
        
        public string Category1 { get; set; }
        public string Category2 { get; set; }
        public string Category3 { get; set; }
        public string Status { get; set; }
        public string Brand { get; set; }
        public string Brand_DESC { get; set; }
        public string Manager { get; set; }
        public string Quantity_360 { get; set; }
        public string Value_360 { get; set; }
        public string Quantity_271_360 { get; set; }
        public string Value_271_360 { get; set; }
        public string Quantity_181_270{ get; set; }
        public string Value_181_270 { get; set; }
        public string Quantity_121_180 { get; set; }
        public string Value_121_180 { get; set; }
        public string Quantity_90_121 { get; set; }
        public string Value_90_121 { get; set; }
        public string Quantity_0_90 { get; set; }
        public string Value_0_90 { get; set; }
        public string Total_Quantity { get; set; }
        public string Total_Value { get; set; }
        
    }
    public class ITM_CUR_AGE_DET360
    {
        public string Category1 { get; set; }
        public string Category2 { get; set; }
        public string Category3 { get; set; }
        public string Status { get; set; }
        public string Quantity { get; set; }
        public string Value { get; set; }
    }
    public class ITM_CUR_AGE_DET270360
    {
        public string Category1 { get; set; }
        public string Category2 { get; set; }
        public string Category3 { get; set; }
        public string Status { get; set; }
        public string Quantity { get; set; }
        public string Value { get; set; }
        
    }
    public class ITM_CUR_AGE_DET180270
    {
        public string Category1 { get; set; }
        public string Category2 { get; set; }
        public string Category3 { get; set; }
        public string Status { get; set; }
        public string Quantity { get; set; }
        public string Value { get; set; }
       
    }
    public class ITM_CUR_AGE_DET120180
    {
        public string Category1 { get; set; }
        public string Category2 { get; set; }
        public string Category3 { get; set; }
        public string Status { get; set; }
        public string Quantity { get; set; }
        public string Value { get; set; }
        
    }
    public class ITM_CUR_AGE_DET90120
    {
        public string Category1 { get; set; }
        public string Category2 { get; set; }
        public string Category3 { get; set; }
        public string Status { get; set; }
        public string Quantity { get; set; }
        public string Value { get; set; }
        
    }
    public class ITM_CUR_AGE_DET90
    {
        public string Category1 { get; set; }
        public string Category2 { get; set; }
        public string Category3 { get; set; }
        public string Status { get; set; }
        public string Quantity { get; set; }
        public string Value { get; set; }
        
    }

}
