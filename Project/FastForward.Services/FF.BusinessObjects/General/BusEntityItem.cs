using System; 
using System.Data;

namespace FF.BusinessObjects
{

    //===========================================================================================================
    // This code is generated by Code gen V.1 
    // All rights reserved.
    // Suneththaraka02@gmail.com 
    // Computer :- ITPD14  | User :- sahanj On 03-Aug-2015 01:10:13
    //===========================================================================================================
    [Serializable]
    public class BusEntityItem
    {
        public String MBII_COM { get; set; }
        public String MBII_CD { get; set; }
        public String MBII_TP { get; set; }
        public String MBII_ITM_CD { get; set; }
        public Int32 MBII_ACT { get; set; }
        public String MBII_CRE_BY { get; set; }
        public DateTime MBII_CRE_DT { get; set; }
        public String MBII_MOD_BY { get; set; }
        public DateTime MBII_MOD_DT { get; set; }
        public string MI_SHORTDESC { get; set; }    //kapila
        public string MBII_CUSTNAME { get; set; }    //Nadeeka

        public string MBII_ACT_status { get; set; }//rukshan 15/Dec/2015
        public Int32 MBII_WARR_PERI { get; set; }//dilshan 13/Nov/2017
        public string MBII_WARR_RMK { get; set; }//dilshan 13/Nov/2017

        public static BusEntityItem Converter(DataRow row)
        {
            return new BusEntityItem
            {
                MBII_COM = row["MBII_COM"] == DBNull.Value ? string.Empty : row["MBII_COM"].ToString(),
                MBII_CD = row["MBII_CD"] == DBNull.Value ? string.Empty : row["MBII_CD"].ToString(),
                MBII_TP = row["MBII_TP"] == DBNull.Value ? string.Empty : row["MBII_TP"].ToString(),
                MBII_ITM_CD = row["MBII_ITM_CD"] == DBNull.Value ? string.Empty : row["MBII_ITM_CD"].ToString(),
                MBII_ACT = row["MBII_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBII_ACT"].ToString()),
                MBII_CRE_BY = row["MBII_CRE_BY"] == DBNull.Value ? string.Empty : row["MBII_CRE_BY"].ToString(),
                MBII_CRE_DT = row["MBII_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBII_CRE_DT"].ToString()),
                MBII_MOD_BY = row["MBII_MOD_BY"] == DBNull.Value ? string.Empty : row["MBII_MOD_BY"].ToString(),
                MBII_MOD_DT = row["MBII_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBII_MOD_DT"].ToString()),
                MBII_CUSTNAME = row["MBII_CUSTNAME"] == DBNull.Value ? string.Empty : row["MBII_CUSTNAME"].ToString(),
                MBII_WARR_PERI = row["MBII_WARR_PERI"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBII_WARR_PERI"].ToString()),
                MBII_WARR_RMK = row["MBII_WARR_RMK"] == DBNull.Value ? string.Empty : row["MBII_WARR_RMK"].ToString()
            };
        }
        //Add by lakshan 28Dec2017
        public static BusEntityItem Converter2(DataRow row)
        {
            return new BusEntityItem
            {
                MBII_COM = row["MBII_COM"] == DBNull.Value ? string.Empty : row["MBII_COM"].ToString(),
                MBII_CD = row["MBII_CD"] == DBNull.Value ? string.Empty : row["MBII_CD"].ToString(),
                MBII_TP = row["MBII_TP"] == DBNull.Value ? string.Empty : row["MBII_TP"].ToString(),
                MBII_ITM_CD = row["MBII_ITM_CD"] == DBNull.Value ? string.Empty : row["MBII_ITM_CD"].ToString(),
                MBII_ACT = row["MBII_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBII_ACT"].ToString()),
                MBII_CRE_BY = row["MBII_CRE_BY"] == DBNull.Value ? string.Empty : row["MBII_CRE_BY"].ToString(),
                MBII_CRE_DT = row["MBII_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBII_CRE_DT"].ToString()),
                MBII_MOD_BY = row["MBII_MOD_BY"] == DBNull.Value ? string.Empty : row["MBII_MOD_BY"].ToString(),
                MBII_MOD_DT = row["MBII_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBII_MOD_DT"].ToString()),
             //   MBII_CUSTNAME = row["MBII_CUSTNAME"] == DBNull.Value ? string.Empty : row["MBII_CUSTNAME"].ToString(),
                MBII_WARR_PERI = row["MBII_WARR_PERI"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBII_WARR_PERI"].ToString()),
                MBII_WARR_RMK = row["MBII_WARR_RMK"] == DBNull.Value ? string.Empty : row["MBII_WARR_RMK"].ToString()
            };
        }
    }
}
