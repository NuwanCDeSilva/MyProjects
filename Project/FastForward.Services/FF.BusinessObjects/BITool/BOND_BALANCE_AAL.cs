using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    [Serializable]
    public class BOND_BALANCE_AAL
    {
        public string ItemCode { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public decimal BondWH { get; set; }
        public decimal RaddSKD { get; set; }
        public decimal RaddCBU { get; set; }
        public decimal RaddDmgInsu { get; set; }
        public decimal KadSKD { get; set; }
        public decimal KadSKDGit { get; set; }
         public decimal KadCBU { get; set; }
        public decimal KadCBUGit { get; set; }
        public decimal KadDam { get; set; }
        public decimal KadDmgGit { get; set; }
        public decimal KaPDI { get; set; }
        public decimal KaPDIGit { get; set; }
        public decimal KaCAN { get; set; }
        public decimal KaCANGit { get; set; }
        public decimal KaMIN { get; set; }
        public decimal KaMINGit { get; set; }
        public decimal NugeSK { get; set; }
        public decimal NugeC { get; set; }
        public decimal Returnable { get; set; }
        public decimal WHRevt { get; set; }
        public decimal MBSR { get; set; }
        public decimal RC1SR { get; set; }
        public decimal RE2SR { get; set; }
        public decimal AZESR { get; set; }
        public decimal EliteSR { get; set; }
        public decimal Dealer { get; set; }
        public decimal FLDRevt { get; set; }
        public decimal PendShipment { get; set; }
        public decimal Jul { get; set; }
        public decimal Aug { get; set; }
        public decimal Sep { get; set; }
        public decimal Oct { get; set; }
        public decimal Nov { get; set; }
        public decimal Dece { get; set; }
        public decimal MBSRLM { get; set; }
        public decimal RC1SRLM { get; set; }
        public decimal RE2SRLM { get; set; }
        public decimal AZESRLM { get; set; }
        public decimal ELITESRLM { get; set; }
        public decimal DEALERLM { get; set; }
        public decimal COOPCHNLLM { get; set; }
        public decimal MBSRCM { get; set; }
        public decimal RC1SRCM { get; set; }
        public decimal RE2SRCM { get; set; }
        public decimal AZESRCM { get; set; }
        public decimal ELITESRCM { get; set; }
        public decimal DEALERCM { get; set; }
        public decimal COOPCHNLCM { get; set; }
        public decimal PI_QTY { get; set; }
        public decimal FOB { get; set; }
        



        public static BOND_BALANCE_AAL ConverterSub(DataRow row)
        {
            return new BOND_BALANCE_AAL
            {
                ItemCode = row["ItemCode"] == DBNull.Value ? string.Empty : row["ItemCode"].ToString(),
                Model = row["Model"] == DBNull.Value ? string.Empty : row["Model"].ToString(),
                Description = row["Description"] == DBNull.Value ? string.Empty : row["Description"].ToString(),
                BondWH = row["BondWH"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BondWH"].ToString()),
                RaddSKD = row["RaddSKD"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RaddSKD"].ToString()),
                RaddCBU = row["RaddCBU"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RaddCBU"].ToString()),
                RaddDmgInsu = row["RaddDmgInsu"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RaddDmgInsu"].ToString()),
                KadSKD = row["KadSKD"] == DBNull.Value ? 0 : Convert.ToDecimal(row["KadSKD"].ToString()),
                KadSKDGit = row["KadSKDGit"] == DBNull.Value ? 0 : Convert.ToDecimal(row["KadSKDGit"].ToString()),
                KadCBU = row["KadCBU"] == DBNull.Value ? 0 : Convert.ToDecimal(row["KadCBU"].ToString()),
                KadCBUGit = row["KadCBUGit"] == DBNull.Value ? 0 : Convert.ToDecimal(row["KadCBUGit"].ToString()),
                KadDam = row["KadDam"] == DBNull.Value ? 0 : Convert.ToDecimal(row["KadDam"].ToString()),
                KadDmgGit = row["KadDmgGit"] == DBNull.Value ? 0 : Convert.ToDecimal(row["KadDmgGit"].ToString()),
                KaPDI = row["KaPDI"] == DBNull.Value ? 0 : Convert.ToDecimal(row["KaPDI"].ToString()),
                KaPDIGit = row["KaPDIGit"] == DBNull.Value ? 0 : Convert.ToDecimal(row["KaPDIGit"].ToString()),
                KaCAN = row["KaCAN"] == DBNull.Value ? 0 : Convert.ToDecimal(row["KaCAN"].ToString()),
                KaCANGit = row["KaCANGit"] == DBNull.Value ? 0 : Convert.ToDecimal(row["KaCANGit"].ToString()),
                KaMIN = row["KaMIN"] == DBNull.Value ? 0 : Convert.ToDecimal(row["KaMIN"].ToString()),
                KaMINGit = row["KaMINGit"] == DBNull.Value ? 0 : Convert.ToDecimal(row["KaMINGit"].ToString()),
                NugeSK = row["NugeSK"] == DBNull.Value ? 0 : Convert.ToDecimal(row["NugeSK"].ToString()),
                NugeC = row["NugeC"] == DBNull.Value ? 0 : Convert.ToDecimal(row["NugeC"].ToString()),
                Returnable = row["Returnable"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Returnable"].ToString()),
                WHRevt = row["WHRevt"] == DBNull.Value ? 0 : Convert.ToDecimal(row["WHRevt"].ToString()),
                MBSR = row["MBSR"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MBSR"].ToString()),
                RC1SR = row["RC1SR"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RC1SR"].ToString()),
                RE2SR = row["RE2SR"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RE2SR"].ToString()),
                AZESR = row["AZESR"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AZESR"].ToString()),
                EliteSR = row["EliteSR"] == DBNull.Value ? 0 : Convert.ToDecimal(row["EliteSR"].ToString()),
                Dealer = row["Dealer"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Dealer"].ToString()),
                FLDRevt = row["FLDRevt"] == DBNull.Value ? 0 : Convert.ToDecimal(row["FLDRevt"].ToString()),
                PendShipment = row["PendShipment"] == DBNull.Value ? 0 : Convert.ToDecimal(row["PendShipment"].ToString()),
                Jul = row["Jul"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Jul"].ToString()),
                Aug = row["Aug"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Aug"].ToString()),
                Sep = row["Sep"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Sep"].ToString()),
                Oct = row["Oct"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Oct"].ToString()),
                Nov = row["Nov"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Nov"].ToString()),
                Dece = row["Dece"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Dece"].ToString()),
                MBSRLM = row["MBSRLM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MBSRLM"].ToString()),
                RC1SRLM = row["RC1SRLM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RC1SRLM"].ToString()),
                RE2SRLM = row["RE2SRLM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RE2SRLM"].ToString()),
                AZESRLM = row["AZESRLM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AZESRLM"].ToString()),
                ELITESRLM = row["ELITESRLM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ELITESRLM"].ToString()),
                DEALERLM = row["DEALERLM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DEALERLM"].ToString()),
                COOPCHNLLM = row["COOPCHNLLM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["COOPCHNLLM"].ToString()),
                MBSRCM = row["MBSRCM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MBSRCM"].ToString()),
                RC1SRCM = row["RC1SRCM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RC1SRCM"].ToString()),
                RE2SRCM = row["RE2SRCM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RE2SRCM"].ToString()),
                AZESRCM = row["AZESRCM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AZESRCM"].ToString()),
                ELITESRCM = row["ELITESRCM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ELITESRCM"].ToString()),
                DEALERCM = row["DEALERCM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DEALERCM"].ToString()),
                COOPCHNLCM = row["COOPCHNLCM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["COOPCHNLCM"].ToString()),
                PI_QTY = row["PI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["PI_QTY"].ToString()),
                FOB = row["FOB"] == DBNull.Value ? 0 : Convert.ToDecimal(row["FOB"].ToString())
            };
        }

    }
}
