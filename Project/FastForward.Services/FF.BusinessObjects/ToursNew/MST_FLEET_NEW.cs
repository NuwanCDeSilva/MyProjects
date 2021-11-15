using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
  public  class MST_FLEET_NEW
    {
        public Int32 MSTF_SEQ { get; set; }
        public String MSTF_REGNO { get; set; }
        public DateTime MSTF_DT { get; set; }
        public String MSTF_BRD { get; set; }
        public String MSTF_MODEL { get; set; }
        public String MSTF_VEH_TP { get; set; }
        public String MSTF_SIPP_CD { get; set; }
        public Int32 MSTF_ST_METER { get; set; }
        public String MSTF_OWN { get; set; }
        public String MSTF_OWN_NM { get; set; }
        public Int32 MSTF_OWN_CONT { get; set; }
        public String MSTF_OWN_EMAIL { get; set; }
        public Int32 MSTF_LST_SERMET { get; set; }
        public String MSTF_TOU_REGNO { get; set; }
        public Int32 MSTF_IS_LEASE { get; set; }
        public String MSTF_INSU_COM { get; set; }
        public DateTime MSTF_INSU_EXP { get; set; }
        public DateTime MSTF_REG_EXP { get; set; }
        public String MSTF_FUAL_TP { get; set; }
        public Int32 MSTF_ACT { get; set; }
        public String MSTF_CRE_BY { get; set; }
        public DateTime MSTF_CRE_DT { get; set; }
        public String MSTF_MOD_BY { get; set; }
        public DateTime MSTF_MOD_DT { get; set; }
        public String MSTF_ENGIN_CAP { get; set; }
        public Int32 MSTF_NOOF_SEAT { get; set; }
        public String MSTF_COMMENTS { get; set; }
        public Decimal MSTF_COST { get; set; }
        public string MSTF_REASON { get; set; }
        public string MSTF_OWN_ADD1 { get; set; }
        public string MSTF_OWN_ADD2 { get; set; }
        public DateTime MSTF_FROM_DT { get; set; }
        public DateTime MSTF_TO_DT { get; set; }
        public string MSTF_OWN_NIC { get; set; }
        public decimal MSTF_PRO_MILGE { get; set; }
        public decimal MSTF_ADD_FULL_DAY { get; set; }
        public decimal MSTF_ADD_HALF_DAY { get; set; }
        public decimal MSTF_ADD_AIR_RET { get; set; }
        public decimal MSTF_CORR_AMT { get; set; }
        public string month { get; set; }
        public decimal rental { get; set; }
        public decimal days { get; set; }
        public string year { get; set; }
        public decimal MSTF_HIRING_DEPOSITE { get; set; }
        
        public List<MST_PCEMP> profitCenterLst { get; set; }

        public List<mst_fleet_alloc> profitCenterLstss { get; set; }
        public List<mst_fleet_driver> mstFleetDriver { get; set; }

        public static MST_FLEET_NEW Converter(DataRow row)
        {
            return new MST_FLEET_NEW
            {
                MSTF_SEQ = row["MSTF_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["MSTF_SEQ"].ToString()),
                MSTF_REGNO = row["MSTF_REGNO"] == DBNull.Value ? string.Empty : row["MSTF_REGNO"].ToString(),
                MSTF_DT = row["MSTF_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MSTF_DT"].ToString()),
                MSTF_BRD = row["MSTF_BRD"] == DBNull.Value ? string.Empty : row["MSTF_BRD"].ToString(),
                MSTF_MODEL = row["MSTF_MODEL"] == DBNull.Value ? string.Empty : row["MSTF_MODEL"].ToString(),
                MSTF_VEH_TP = row["MSTF_VEH_TP"] == DBNull.Value ? string.Empty : row["MSTF_VEH_TP"].ToString(),
                MSTF_SIPP_CD = row["MSTF_SIPP_CD"] == DBNull.Value ? string.Empty : row["MSTF_SIPP_CD"].ToString(),
                MSTF_ST_METER = row["MSTF_ST_METER"] == DBNull.Value ? 0 : Convert.ToInt32(row["MSTF_ST_METER"].ToString()),
                MSTF_OWN = row["MSTF_OWN"] == DBNull.Value ? string.Empty : row["MSTF_OWN"].ToString(),
                MSTF_OWN_NM = row["MSTF_OWN_NM"] == DBNull.Value ? string.Empty : row["MSTF_OWN_NM"].ToString(),
                MSTF_OWN_CONT = row["MSTF_OWN_CONT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MSTF_OWN_CONT"].ToString()),
                MSTF_OWN_EMAIL = row["MSTF_OWN_EMAIL"] == DBNull.Value ? string.Empty : row["MSTF_OWN_EMAIL"].ToString(),
                MSTF_LST_SERMET = row["MSTF_LST_SERMET"] == DBNull.Value ? 0 : Convert.ToInt32(row["MSTF_LST_SERMET"].ToString()),
                MSTF_TOU_REGNO = row["MSTF_TOU_REGNO"] == DBNull.Value ? string.Empty : row["MSTF_TOU_REGNO"].ToString(),
                MSTF_IS_LEASE = row["MSTF_IS_LEASE"] == DBNull.Value ? 0 : Convert.ToInt32(row["MSTF_IS_LEASE"].ToString()),
                MSTF_FUAL_TP = row["MSTF_FUAL_TP"] == DBNull.Value ? string.Empty : row["MSTF_FUAL_TP"].ToString(),
                MSTF_INSU_EXP = row["MSTF_INSU_EXP"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MSTF_INSU_EXP"].ToString()),
                MSTF_REG_EXP = row["MSTF_REG_EXP"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MSTF_REG_EXP"].ToString()),
                MSTF_INSU_COM = row["MSTF_INSU_COM"] == DBNull.Value ? string.Empty : row["MSTF_INSU_COM"].ToString(),
                MSTF_ACT = row["MSTF_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MSTF_ACT"].ToString()),
                MSTF_CRE_BY = row["MSTF_CRE_BY"] == DBNull.Value ? string.Empty : row["MSTF_CRE_BY"].ToString(),
                MSTF_CRE_DT = row["MSTF_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MSTF_CRE_DT"].ToString()),
                MSTF_MOD_BY = row["MSTF_MOD_BY"] == DBNull.Value ? string.Empty : row["MSTF_MOD_BY"].ToString(),
                MSTF_MOD_DT = row["MSTF_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MSTF_MOD_DT"].ToString()),
                MSTF_ENGIN_CAP = row["MSTF_ENGIN_CAP"] == DBNull.Value ? string.Empty : row["MSTF_ENGIN_CAP"].ToString(),
                MSTF_COMMENTS = row["MSTF_COMMENTS"] == DBNull.Value ? string.Empty : row["MSTF_COMMENTS"].ToString(),
                MSTF_NOOF_SEAT = row["MSTF_NOOF_SEAT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MSTF_NOOF_SEAT"].ToString()),
                MSTF_COST = row["MSTF_COST"] == DBNull.Value ? 0 : Convert.ToInt32(row["MSTF_COST"].ToString()),
                MSTF_REASON = row["MSTF_REASON"] == DBNull.Value ? string.Empty : row["MSTF_REASON"].ToString(),
                MSTF_OWN_ADD1 = row["MSTF_OWN_ADD1"] == DBNull.Value ? string.Empty : row["MSTF_OWN_ADD1"].ToString(),
                MSTF_OWN_ADD2 = row["MSTF_OWN_ADD2"] == DBNull.Value ? string.Empty : row["MSTF_OWN_ADD2"].ToString(),
                MSTF_FROM_DT = row["MSTF_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MSTF_FROM_DT"].ToString()),
                MSTF_TO_DT = row["MSTF_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MSTF_TO_DT"].ToString()),
                MSTF_OWN_NIC = row["MSTF_OWN_NIC"] == DBNull.Value ? string.Empty : row["MSTF_OWN_NIC"].ToString(),
                MSTF_PRO_MILGE = row["MSTF_PRO_MILGE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MSTF_PRO_MILGE"].ToString()),
                MSTF_ADD_AIR_RET = row["MSTF_ADD_AIR_RET"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MSTF_ADD_AIR_RET"].ToString()),
                MSTF_ADD_HALF_DAY = row["MSTF_ADD_HALF_DAY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MSTF_ADD_HALF_DAY"].ToString()),
                MSTF_ADD_FULL_DAY = row["MSTF_ADD_FULL_DAY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MSTF_ADD_FULL_DAY"].ToString()),
                MSTF_CORR_AMT = row["MSTF_CORR_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MSTF_CORR_AMT"].ToString()),
                MSTF_HIRING_DEPOSITE = row["MSTF_HIRING_DEPOSITE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MSTF_HIRING_DEPOSITE"].ToString()),
            };
        }
    }
}

