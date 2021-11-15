using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects
{
    public class InventoryItemSimilarITM
    {
        public string COMPANY { get; set; }
        public string LOC { get; set; }
        public string LOC_DESC { get; set; }
        public string ITEM_CODE { get; set; }
        public string ITEM_DESC { get; set; }
        public string MODEL { get; set; }
        public string BRAND { get; set; }
        public string ITEM_STATUS { get; set; }
        public string UOM { get; set; }
        public string QTY { get; set; }
        public string COST_VAL { get; set; }
        public string FREE_QTY { get; set; }
        public string RES_QTY { get; set; }
        public string BUFFER_LEVEL { get; set; }
        public string MI_PART_NO { get; set; }
        public static InventoryItemSimilarITM Converter(DataRow row)
        {
            return new InventoryItemSimilarITM
            {
                COMPANY = row["COMPANY"] == DBNull.Value ? string.Empty : row["COMPANY"].ToString(),
                LOC = row["LOC"] == DBNull.Value ? string.Empty : row["LOC"].ToString(),
                LOC_DESC = row["LOC_DESC"] == DBNull.Value ? string.Empty : row["LOC_DESC"].ToString(),
                ITEM_CODE = row["ITEM_CODE"] == DBNull.Value ? string.Empty : row["ITEM_CODE"].ToString(),
                ITEM_DESC = row["ITEM_DESC"] == DBNull.Value ? string.Empty : row["ITEM_DESC"].ToString(),
                MODEL = row["MODEL"] == DBNull.Value ? string.Empty : row["MODEL"].ToString(),
                BRAND = row["BRAND"] == DBNull.Value ? string.Empty : row["BRAND"].ToString(),
                ITEM_STATUS = row["ITEM_STATUS"] == DBNull.Value ? string.Empty : row["ITEM_STATUS"].ToString(),
                UOM = row["UOM"] == DBNull.Value ? string.Empty : row["UOM"].ToString(),
                QTY = row["QTY"] == DBNull.Value ? string.Empty : row["QTY"].ToString(),
                COST_VAL = row["COST_VAL"] == DBNull.Value ? string.Empty : row["COST_VAL"].ToString(),
                FREE_QTY = row["FREE_QTY"] == DBNull.Value ? string.Empty : row["FREE_QTY"].ToString(),
                RES_QTY = row["RES_QTY"] == DBNull.Value ? string.Empty : row["RES_QTY"].ToString(),
                BUFFER_LEVEL = row["BUFFER_LEVEL"] == DBNull.Value ? string.Empty : row["BUFFER_LEVEL"].ToString(),
                MI_PART_NO = row["MI_PART_NO"] == DBNull.Value ? string.Empty : row["MI_PART_NO"].ToString(),
            };
        }
    }   
}
