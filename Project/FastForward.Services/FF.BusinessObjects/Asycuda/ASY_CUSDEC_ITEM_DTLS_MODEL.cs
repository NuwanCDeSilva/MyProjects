using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Asycuda
{
    public class ASY_CUSDEC_ITEM_DTLS_MODEL
    {
        public String DOCUMENT_TYPE { get; set; }
        public String BOND_NO { get; set; }
        public String ITEM_CODE { get; set; }
        public String HS_CODE { get; set; }
        public decimal BOND_QTY { get; set; }
        public decimal ACTUAL_QTY { get; set; }
        public decimal RESERVE_QTY { get; set; }
        public decimal UNIT_COST { get; set; }
        public String MODEL { get; set; }
        public String DESCRIPTION { get; set; }
        public decimal TOTAL_UNIT_COST { get; set; }
        public decimal RESAERVATION_QTY { get; set; }
        public String PI_NO { get; set; }
        public String UOM { get; set; }
        public decimal AOD_QTY { get; set; }
        public decimal BOI_LINE_NO { get; set; }
        public decimal TO_BOND_ITEM_LINE_NO { get; set; }
        public decimal GROSS_MASS { get; set; }
        public decimal NET_MASS { get; set; }
        public String BL_NO { get; set; }
        public String QUOTA { get; set; }
        public String PREFERANCE { get; set; }
        public String DEF_COUNTRY { get; set; }
        public decimal UNIT_COST2 { get; set; }
        public decimal TOTAL_UNIT_COST2 { get; set; }
        public String OTHER_BOND_NO { get; set; }
        public string OTHER_BOND_NO_LINE_NO { get; set; }
        public String ORGIN_COUNTRY { get; set; }
        public decimal NULOAD_EXBOND_QTY_1 { get; set; }
        public decimal IGNORE_ENTRY_QTY { get; set; }
        public String CAPACITY { get; set; }
        public decimal BAL_QTY { get; set; }
        public decimal ITEM_PRICE { get; set; }
        public decimal TOT_NUMOF_PKG { get; set; }
        public string S_NUMBER { get; set; }
        public string COUNTRY_OF_ORGIN { get; set; }
        public string CUH_CUSDEC_ENTRY_NO { get; set; }

        public string CUI_OTH_DOC_LINE { get; set; }
        public static ASY_CUSDEC_ITEM_DTLS_MODEL Converter(DataRow row)
        {
            return new ASY_CUSDEC_ITEM_DTLS_MODEL
            {
                DOCUMENT_TYPE = row["DOCUMENT_TYPE"] == DBNull.Value ? string.Empty : row["DOCUMENT_TYPE"].ToString(),
                BOND_NO = row["BOND_NO"] == DBNull.Value ? string.Empty : row["BOND_NO"].ToString(),
                ITEM_CODE = row["ITEM_CODE"] == DBNull.Value ? string.Empty : row["ITEM_CODE"].ToString(),
                HS_CODE = row["HS_CODE"] == DBNull.Value ? string.Empty : row["HS_CODE"].ToString(),
                BOND_QTY = row["BOND_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BOND_QTY"].ToString()),
                ACTUAL_QTY = row["ACTUAL_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACTUAL_QTY"].ToString()),
                RESERVE_QTY = row["RESERVE_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RESERVE_QTY"].ToString()),
                UNIT_COST = row["UNIT_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["UNIT_COST"].ToString()),
                MODEL = row["MODEL"] == DBNull.Value ? string.Empty : row["MODEL"].ToString(),
                DESCRIPTION = row["DESCRIPTION"] == DBNull.Value ? string.Empty : row["DESCRIPTION"].ToString(),
                TOTAL_UNIT_COST = row["TOTAL_UNIT_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TOTAL_UNIT_COST"].ToString()),
                RESAERVATION_QTY = row["RESAERVATION_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RESAERVATION_QTY"].ToString()),
                PI_NO = row["PI_NO"] == DBNull.Value ? string.Empty : row["PI_NO"].ToString(),
                UOM = row["UOM"] == DBNull.Value ? string.Empty : row["UOM"].ToString(),
                AOD_QTY = row["AOD_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AOD_QTY"].ToString()),
                BOI_LINE_NO = row["BOI_LINE_NO"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BOI_LINE_NO"].ToString()),
                TO_BOND_ITEM_LINE_NO = row["TO_BOND_ITEM_LINE_NO"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TO_BOND_ITEM_LINE_NO"].ToString()),
                GROSS_MASS = row["GROSS_MASS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GROSS_MASS"].ToString()),
                NET_MASS = row["NET_MASS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["NET_MASS"].ToString()),
                BL_NO = row["BL_NO"] == DBNull.Value ? string.Empty : row["BL_NO"].ToString(),
                QUOTA = row["QUOTA"] == DBNull.Value ? string.Empty : row["QUOTA"].ToString(),
                PREFERANCE = row["PREFERANCE"] == DBNull.Value ? string.Empty : row["PREFERANCE"].ToString(),
                DEF_COUNTRY = row["DEF_COUNTRY"] == DBNull.Value ? string.Empty : row["DEF_COUNTRY"].ToString(),
                UNIT_COST2 = row["UNIT_COST2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["UNIT_COST2"].ToString()),
                TOTAL_UNIT_COST2 = row["TOTAL_UNIT_COST2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TOTAL_UNIT_COST2"].ToString()),
                OTHER_BOND_NO = row["OTHER_BOND_NO"] == DBNull.Value ? string.Empty : row["OTHER_BOND_NO"].ToString(),
                OTHER_BOND_NO_LINE_NO = row["OTHER_BOND_NO_LINE_NO"] == DBNull.Value ? string.Empty : row["OTHER_BOND_NO_LINE_NO"].ToString(),
                ORGIN_COUNTRY = row["ORGIN_COUNTRY"] == DBNull.Value ? string.Empty : row["ORGIN_COUNTRY"].ToString(),
                NULOAD_EXBOND_QTY_1 = row["NULOAD_EXBOND_QTY_1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["NULOAD_EXBOND_QTY_1"].ToString()),
                IGNORE_ENTRY_QTY = row["IGNORE_ENTRY_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IGNORE_ENTRY_QTY"].ToString()),
                CAPACITY = row["CAPACITY"] == DBNull.Value ? string.Empty : row["CAPACITY"].ToString(),
                BAL_QTY = row["BAL_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BAL_QTY"].ToString()),
                ITEM_PRICE = row["ITEM_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITEM_PRICE"].ToString()),
                TOT_NUMOF_PKG = row["TOT_NUMOF_PKG"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TOT_NUMOF_PKG"].ToString()),
                S_NUMBER = row["S_NUMBER"] == DBNull.Value ? string.Empty : row["S_NUMBER"].ToString(),
                COUNTRY_OF_ORGIN = row["COUNTRY_OF_ORGIN"] == DBNull.Value ? string.Empty : row["COUNTRY_OF_ORGIN"].ToString(),
                CUH_CUSDEC_ENTRY_NO = row["CUH_CUSDEC_ENTRY_NO"] == DBNull.Value ? string.Empty : row["CUH_CUSDEC_ENTRY_NO"].ToString(),
                CUI_OTH_DOC_LINE = row["CUI_OTH_DOC_LINE"] == DBNull.Value ? string.Empty : row["CUI_OTH_DOC_LINE"].ToString()
            };
        } 
    }
}
