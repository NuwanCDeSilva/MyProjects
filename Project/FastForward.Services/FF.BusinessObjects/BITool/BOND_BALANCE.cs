using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
   public class BOND_BALANCE
    {
        public string Brand { get; set; }
        public string Category1 { get; set; }
        public string Category2 { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public string Model { get; set; }
        public decimal DF { get; set; }
        public decimal ABLDP { get; set; }
        public decimal GITDP { get; set; }
        public decimal LRPDP { get; set; }
        public decimal Tot { get; set; }
        public decimal ABLReg { get; set; }
        public decimal LRPReg { get; set; }
        public decimal ABLElite { get; set; }
        public decimal LRPElite { get; set; }
        public decimal ABLRetail1 { get; set; }
        public decimal LRPRetail1 { get; set; }
        public decimal ABLRetail2 { get; set; }
        public decimal LRPRetail2 { get; set; }
        public decimal AZSR { get; set; }
        public decimal ABLApple { get; set; }
        public decimal LRPApple { get; set; }
        public decimal MSM { get; set; }
        public decimal DFSR { get; set; }
        public decimal Other { get; set; }
        public decimal Month1 { get; set; }
        public decimal Month2 { get; set; }
        public decimal Month3 { get; set; }
        public decimal Month4 { get; set; }
        public decimal Month5 { get; set; }
        public decimal Month6 { get; set; }
        public decimal DFSale { get; set; }
        public decimal Exp { get; set; }
        public decimal Dir { get; set; }
        public decimal OTS03 { get; set; }
        public decimal Elite { get; set; }
        public decimal Retail { get; set; }
        public decimal Other1 { get; set; }
        public decimal Aze { get; set; }
        public decimal AOADir { get; set; }
        public decimal AOADel { get; set; }
        public decimal MSM1 { get; set; }
        public decimal Appsr { get; set; }
        public decimal ABT { get; set; }
        public decimal Tot1 { get; set; }
        public decimal Pending_Import_M1 { get; set; }
        public decimal Pending_Import_M2 { get; set; }
        public decimal Pending_Import_M3 { get; set; }
        public decimal Pending_LP_M1 { get; set; }
        public decimal Pending_LP_M2 { get; set; }
        public decimal Pending_LP_M3 { get; set; }

        public static BOND_BALANCE ConverterSub(DataRow row)
        {
            return new BOND_BALANCE
            {
                Brand = row["Brand"] == DBNull.Value ? string.Empty : row["Brand"].ToString(),
                Category1 = row["Category1"] == DBNull.Value ? string.Empty : row["Category1"].ToString(),
                Category2 = row["Category2"] == DBNull.Value ? string.Empty : row["Category2"].ToString(),
                ItemCode = row["ItemCode"] == DBNull.Value ? string.Empty : row["ItemCode"].ToString(),
                ItemDescription = row["ItemDescription"] == DBNull.Value ? string.Empty : row["ItemDescription"].ToString(),
                Model = row["Model"] == DBNull.Value ? string.Empty : row["Model"].ToString(),
                DF = row["DF"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DF"].ToString()),
                ABLDP = row["ABLDP"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ABLDP"].ToString()),
                GITDP = row["GITDP"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GITDP"].ToString()),
                LRPDP = row["LRPDP"] == DBNull.Value ? 0 : Convert.ToDecimal(row["LRPDP"].ToString()),
                Tot = row["Tot"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Tot"].ToString()),
                ABLReg = row["ABLReg"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ABLReg"].ToString()),
                LRPReg = row["LRPReg"] == DBNull.Value ? 0 : Convert.ToDecimal(row["LRPReg"].ToString()),
                ABLElite = row["ABLElite"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ABLElite"].ToString()),
                LRPElite = row["LRPElite"] == DBNull.Value ? 0 : Convert.ToDecimal(row["LRPElite"].ToString()),
                ABLRetail1 = row["ABLRetail1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ABLRetail1"].ToString()),
                LRPRetail1 = row["LRPRetail1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["LRPRetail1"].ToString()),
                ABLRetail2 = row["ABLRetail2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ABLRetail2"].ToString()),
                LRPRetail2 = row["LRPRetail2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["LRPRetail2"].ToString()),
                AZSR = row["AZSR"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AZSR"].ToString()),
                ABLApple = row["ABLApple"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ABLApple"].ToString()),
                LRPApple = row["LRPApple"] == DBNull.Value ? 0 : Convert.ToDecimal(row["LRPApple"].ToString()),
                MSM = row["MSM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MSM"].ToString()),
                DFSR = row["DFSR"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DFSR"].ToString()),
                Other = row["Other"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Other"].ToString()),
                Month1 = row["Month1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Month1"].ToString()),
                Month2 = row["Month2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Month2"].ToString()),
                Month3 = row["Month3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Month3"].ToString()),
                Month4 = row["Month4"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Month4"].ToString()),
                Month5 = row["Month5"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Month5"].ToString()),
                Month6 = row["Month6"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Month6"].ToString()),
                DFSale = row["DFSale"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DFSale"].ToString()),
                Exp = row["Exp"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Exp"].ToString()),
                Dir = row["Dir"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Dir"].ToString()),
                OTS03 = row["OTS03"] == DBNull.Value ? 0 : Convert.ToDecimal(row["OTS03"].ToString()),
                Elite = row["Elite"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Elite"].ToString()),
                Retail = row["Retail"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Retail"].ToString()),
                Aze = row["Aze"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Aze"].ToString()),
                AOADir = row["AOADir"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AOADir"].ToString()),
                AOADel = row["AOADel"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AOADel"].ToString()),
                Appsr = row["Appsr"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Appsr"].ToString()),
                ABT = row["ABT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ABT"].ToString()),
                Tot1 = row["Tot1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Tot1"].ToString()),
                Other1 = row["Other1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Other1"].ToString()),
                Pending_Import_M1 = row["Pending_Import_M1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Pending_Import_M1"].ToString()),
                Pending_Import_M2 = row["Pending_Import_M2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Pending_Import_M2"].ToString()),
                Pending_Import_M3 = row["Pending_Import_M3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Pending_Import_M3"].ToString()),
                Pending_LP_M1 = row["Pending_LP_M1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Pending_LP_M1"].ToString()),
                Pending_LP_M2 = row["Pending_LP_M2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Pending_LP_M2"].ToString()),
                Pending_LP_M3 = row["Pending_LP_M3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Pending_LP_M3"].ToString())
            };
        }
    }
}
