using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace FF.BusinessObjects.Genaral
{
    public class MST_EMPLOYEES
    {
        public String MEMP_EPF { get; set; }
        public String MEMP_COM_CD { get; set; }
        public String MEMP_CD { get; set; }
        public String MEMP_CAT_CD { get; set; }
        public String MEMP_CAT_SUBCD { get; set; }
        public String MEMP_MANAGER_CD { get; set; }
        public String MEMP_TITLE { get; set; }
        public String MEMP_NAME_INITIALS { get; set; }
        public String MEMP_FIRST_NAME { get; set; }
        public String MEMP_LAST_NAME { get; set; }
        public DateTime MEMP_DOJ { get; set; }
        public String MEMP_SEX { get; set; }
        public DateTime MEMP_DOB { get; set; }
        public String MEMP_PER_ADD_1 { get; set; }
        public String MEMP_PER_ADD_2 { get; set; }
        public String MEMP_PER_ADD_3 { get; set; }
        public String MEMP_LIVING_ADD_1 { get; set; }
        public String MEMP_LIVING_ADD_2 { get; set; }
        public String MEMP_LIVING_ADD_3 { get; set; }
        public String MEMP_POLICE_STATION { get; set; }
        public String MEMP_MOBI_NO { get; set; }
        public String MEMP_TEL_HOME_NO { get; set; }
        public String MEMP_TEL_OFF_NO { get; set; }
        public String MEMP_NIC { get; set; }
        public String MEMP_CONTRACTOR { get; set; }
        public Int32 MEMP_IS_MAX_STOCK_VAL { get; set; }
        public Int32 MEMP_MAX_STOCK_VAL { get; set; }
        public Int32 MEMP_ACT { get; set; }
        public String MEMP_CRE_BY { get; set; }
        public DateTime MEMP_CRE_DT { get; set; }
        public String MEMP_MOD_BY { get; set; }
        public DateTime MEMP_MOD_DT { get; set; }
        public String MEMP_SESSION_ID { get; set; }
        public String MEMP_DEF_PROFIT { get; set; }
        public String MEMP_SUPWISE_CD { get; set; }
        public String MEMP_EMAIL { get; set; }
        public String MEMP_CON_PER { get; set; }
        public String MEMP_CON_PER_MOB { get; set; }
        public String MEMP_LIC_NO { get; set; }
        public String MEMP_LIC_CAT { get; set; }
        public DateTime MEMP_LIC_EXDT { get; set; }
        public String MEMP_TOU_LIC { get; set; }
        public String MEMP_ANAL1 { get; set; }
        public String MEMP_ANAL2 { get; set; }
        public String MEMP_ANAL3 { get; set; }
        public DateTime MEMP_ANAL4 { get; set; }
        public Decimal MEMP_COST { get; set; }
        //public List<MST_PCEMP> profitCenterLst { get; set; }
        //public List<mst_fleet_driver> mstFleetDriver { get; set; }
        public static MST_EMPLOYEES Converter(DataRow row)
        {
            return new MST_EMPLOYEES
            {
                MEMP_EPF = row["MEMP_EPF"] == DBNull.Value ? string.Empty : row["MEMP_EPF"].ToString(),
                MEMP_COM_CD = row["MEMP_COM_CD"] == DBNull.Value ? string.Empty : row["MEMP_COM_CD"].ToString(),
                MEMP_CD = row["MEMP_CD"] == DBNull.Value ? string.Empty : row["MEMP_CD"].ToString(),
                MEMP_CAT_CD = row["MEMP_CAT_CD"] == DBNull.Value ? string.Empty : row["MEMP_CAT_CD"].ToString(),
                MEMP_CAT_SUBCD = row["MEMP_CAT_SUBCD"] == DBNull.Value ? string.Empty : row["MEMP_CAT_SUBCD"].ToString(),
                MEMP_MANAGER_CD = row["MEMP_MANAGER_CD"] == DBNull.Value ? string.Empty : row["MEMP_MANAGER_CD"].ToString(),
                MEMP_TITLE = row["MEMP_TITLE"] == DBNull.Value ? string.Empty : row["MEMP_TITLE"].ToString(),
                MEMP_NAME_INITIALS = row["MEMP_NAME_INITIALS"] == DBNull.Value ? string.Empty : row["MEMP_NAME_INITIALS"].ToString(),
                MEMP_FIRST_NAME = row["MEMP_FIRST_NAME"] == DBNull.Value ? string.Empty : row["MEMP_FIRST_NAME"].ToString(),
                MEMP_LAST_NAME = row["MEMP_LAST_NAME"] == DBNull.Value ? string.Empty : row["MEMP_LAST_NAME"].ToString(),
                MEMP_DOJ = row["MEMP_DOJ"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MEMP_DOJ"].ToString()),
                MEMP_SEX = row["MEMP_SEX"] == DBNull.Value ? string.Empty : row["MEMP_SEX"].ToString(),
                MEMP_DOB = row["MEMP_DOB"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MEMP_DOB"].ToString()),
                MEMP_PER_ADD_1 = row["MEMP_PER_ADD_1"] == DBNull.Value ? string.Empty : row["MEMP_PER_ADD_1"].ToString(),
                MEMP_PER_ADD_2 = row["MEMP_PER_ADD_2"] == DBNull.Value ? string.Empty : row["MEMP_PER_ADD_2"].ToString(),
                MEMP_PER_ADD_3 = row["MEMP_PER_ADD_3"] == DBNull.Value ? string.Empty : row["MEMP_PER_ADD_3"].ToString(),
                MEMP_LIVING_ADD_1 = row["MEMP_LIVING_ADD_1"] == DBNull.Value ? string.Empty : row["MEMP_LIVING_ADD_1"].ToString(),
                MEMP_LIVING_ADD_2 = row["MEMP_LIVING_ADD_2"] == DBNull.Value ? string.Empty : row["MEMP_LIVING_ADD_2"].ToString(),
                MEMP_LIVING_ADD_3 = row["MEMP_LIVING_ADD_3"] == DBNull.Value ? string.Empty : row["MEMP_LIVING_ADD_3"].ToString(),
                MEMP_POLICE_STATION = row["MEMP_POLICE_STATION"] == DBNull.Value ? string.Empty : row["MEMP_POLICE_STATION"].ToString(),
                MEMP_MOBI_NO = row["MEMP_MOBI_NO"] == DBNull.Value ? string.Empty : row["MEMP_MOBI_NO"].ToString(),
                MEMP_TEL_HOME_NO = row["MEMP_TEL_HOME_NO"] == DBNull.Value ? string.Empty : row["MEMP_TEL_HOME_NO"].ToString(),
                MEMP_TEL_OFF_NO = row["MEMP_TEL_OFF_NO"] == DBNull.Value ? string.Empty : row["MEMP_TEL_OFF_NO"].ToString(),
                MEMP_NIC = row["MEMP_NIC"] == DBNull.Value ? string.Empty : row["MEMP_NIC"].ToString(),
                MEMP_CONTRACTOR = row["MEMP_CONTRACTOR"] == DBNull.Value ? string.Empty : row["MEMP_CONTRACTOR"].ToString(),
                MEMP_IS_MAX_STOCK_VAL = row["MEMP_IS_MAX_STOCK_VAL"] == DBNull.Value ? 0 : Convert.ToInt32(row["MEMP_IS_MAX_STOCK_VAL"].ToString()),
                MEMP_MAX_STOCK_VAL = row["MEMP_MAX_STOCK_VAL"] == DBNull.Value ? 0 : Convert.ToInt32(row["MEMP_MAX_STOCK_VAL"].ToString()),
                MEMP_ACT = row["MEMP_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MEMP_ACT"].ToString()),
                MEMP_CRE_BY = row["MEMP_CRE_BY"] == DBNull.Value ? string.Empty : row["MEMP_CRE_BY"].ToString(),
                MEMP_CRE_DT = row["MEMP_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MEMP_CRE_DT"].ToString()),
                MEMP_MOD_BY = row["MEMP_MOD_BY"] == DBNull.Value ? string.Empty : row["MEMP_MOD_BY"].ToString(),
                MEMP_MOD_DT = row["MEMP_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MEMP_MOD_DT"].ToString()),
                MEMP_SESSION_ID = row["MEMP_SESSION_ID"] == DBNull.Value ? string.Empty : row["MEMP_SESSION_ID"].ToString(),
                MEMP_DEF_PROFIT = row["MEMP_DEF_PROFIT"] == DBNull.Value ? string.Empty : row["MEMP_DEF_PROFIT"].ToString(),
                MEMP_SUPWISE_CD = row["MEMP_SUPWISE_CD"] == DBNull.Value ? string.Empty : row["MEMP_SUPWISE_CD"].ToString(),
                MEMP_EMAIL = row["MEMP_EMAIL"] == DBNull.Value ? string.Empty : row["MEMP_EMAIL"].ToString(),
                MEMP_CON_PER = row["MEMP_CON_PER"] == DBNull.Value ? string.Empty : row["MEMP_CON_PER"].ToString(),
                MEMP_CON_PER_MOB = row["MEMP_CON_PER_MOB"] == DBNull.Value ? string.Empty : row["MEMP_CON_PER_MOB"].ToString(),
                MEMP_LIC_NO = row["MEMP_LIC_NO"] == DBNull.Value ? string.Empty : row["MEMP_LIC_NO"].ToString(),
                MEMP_LIC_CAT = row["MEMP_LIC_CAT"] == DBNull.Value ? string.Empty : row["MEMP_LIC_CAT"].ToString(),
                MEMP_LIC_EXDT = row["MEMP_LIC_EXDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MEMP_LIC_EXDT"].ToString()),
                MEMP_TOU_LIC = row["MEMP_TOU_LIC"] == DBNull.Value ? string.Empty : row["MEMP_TOU_LIC"].ToString(),
                MEMP_ANAL1 = row["MEMP_ANAL1"] == DBNull.Value ? string.Empty : row["MEMP_ANAL1"].ToString(),
                MEMP_ANAL2 = row["MEMP_ANAL2"] == DBNull.Value ? string.Empty : row["MEMP_ANAL2"].ToString(),
                MEMP_ANAL3 = row["MEMP_ANAL3"] == DBNull.Value ? string.Empty : row["MEMP_ANAL3"].ToString(),
                MEMP_ANAL4 = row["MEMP_ANAL4"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MEMP_ANAL4"].ToString()),
                MEMP_COST = row["MEMP_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MEMP_COST"].ToString()),
            };
        }
    }
}
