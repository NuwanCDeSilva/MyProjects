using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Sales
{
    public class MST_EMP
    {
        public String ESEP_EPF { get; set; }
        public String ESEP_COM_CD { get; set; }
        public String ESEP_CD { get; set; }
        public String ESEP_CAT_CD { get; set; }
        public String ESEP_CAT_SUBCD { get; set; }
        public String ESEP_MANAGER_CD { get; set; }
        public String ESEP_TITLE { get; set; }
        public String ESEP_NAME_INITIALS { get; set; }
        public String ESEP_FIRST_NAME { get; set; }
        public String ESEP_LAST_NAME { get; set; }
        public String ESEP_SEX { get; set; }
        public DateTime ESEP_DOB { get; set; }
        public String ESEP_PER_ADD_1 { get; set; }
        public String ESEP_PER_ADD_2 { get; set; }
        public String ESEP_PER_ADD_3 { get; set; }
        public String ESEP_LIVING_ADD_1 { get; set; }
        public String ESEP_LIVING_ADD_2 { get; set; }
        public String ESEP_LIVING_ADD_3 { get; set; }
        public String ESEP_POLICE_STATION { get; set; }
        public String ESEP_MOBI_NO { get; set; }
        public String ESEP_TEL_HOME_NO { get; set; }
        public String ESEP_TEL_OFF_NO { get; set; }
        public String ESEP_NIC { get; set; }
        public String ESEP_CONTRACTOR { get; set; }
        public Int32 ESEP_IS_MAX_STOCK_VAL { get; set; }
        public Int32 ESEP_MAX_STOCK_VAL { get; set; }
        public Int32 ESEP_ACT { get; set; }
        public String ESEP_CRE_BY { get; set; }
        public DateTime ESEP_CRE_DT { get; set; }
        public String ESEP_MOD_BY { get; set; }
        public DateTime ESEP_MOD_DT { get; set; }
        public String ESEP_SESSION_ID { get; set; }
        public String ESEP_DEF_PROFIT { get; set; }
        public String ESEP_SUPWISE_CD { get; set; }
        public String ESEP_EMAIL { get; set; }
        public String ESEP_DL_NO { get; set; }
        public DateTime ESEP_DL_ISSDT { get; set; }
        public DateTime ESEP_DL_EXPDT { get; set; }
        public String ESEP_DL_CLASS { get; set; }
        public DateTime ESEP_DOJ { get; set; }
        
        public static MST_EMP Converter(DataRow row)
        {
            return new MST_EMP
            {
                ESEP_EPF = row["ESEP_EPF"] == DBNull.Value ? string.Empty : row["ESEP_EPF"].ToString(),
                ESEP_COM_CD = row["ESEP_COM_CD"] == DBNull.Value ? string.Empty : row["ESEP_COM_CD"].ToString(),
                ESEP_CD = row["ESEP_CD"] == DBNull.Value ? string.Empty : row["ESEP_CD"].ToString(),
                ESEP_CAT_CD = row["ESEP_CAT_CD"] == DBNull.Value ? string.Empty : row["ESEP_CAT_CD"].ToString(),
                ESEP_CAT_SUBCD = row["ESEP_CAT_SUBCD"] == DBNull.Value ? string.Empty : row["ESEP_CAT_SUBCD"].ToString(),
                ESEP_MANAGER_CD = row["ESEP_MANAGER_CD"] == DBNull.Value ? string.Empty : row["ESEP_MANAGER_CD"].ToString(),
                ESEP_TITLE = row["ESEP_TITLE"] == DBNull.Value ? string.Empty : row["ESEP_TITLE"].ToString(),
                ESEP_NAME_INITIALS = row["ESEP_NAME_INITIALS"] == DBNull.Value ? string.Empty : row["ESEP_NAME_INITIALS"].ToString(),
                ESEP_FIRST_NAME = row["ESEP_FIRST_NAME"] == DBNull.Value ? string.Empty : row["ESEP_FIRST_NAME"].ToString(),
                ESEP_LAST_NAME = row["ESEP_LAST_NAME"] == DBNull.Value ? string.Empty : row["ESEP_LAST_NAME"].ToString(),
                ESEP_SEX = row["ESEP_SEX"] == DBNull.Value ? string.Empty : row["ESEP_SEX"].ToString(),
                ESEP_DOB = row["ESEP_DOB"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ESEP_DOB"].ToString()),
                ESEP_PER_ADD_1 = row["ESEP_PER_ADD_1"] == DBNull.Value ? string.Empty : row["ESEP_PER_ADD_1"].ToString(),
                ESEP_PER_ADD_2 = row["ESEP_PER_ADD_2"] == DBNull.Value ? string.Empty : row["ESEP_PER_ADD_2"].ToString(),
                ESEP_PER_ADD_3 = row["ESEP_PER_ADD_3"] == DBNull.Value ? string.Empty : row["ESEP_PER_ADD_3"].ToString(),
                ESEP_LIVING_ADD_1 = row["ESEP_LIVING_ADD_1"] == DBNull.Value ? string.Empty : row["ESEP_LIVING_ADD_1"].ToString(),
                ESEP_LIVING_ADD_2 = row["ESEP_LIVING_ADD_2"] == DBNull.Value ? string.Empty : row["ESEP_LIVING_ADD_2"].ToString(),
                ESEP_LIVING_ADD_3 = row["ESEP_LIVING_ADD_3"] == DBNull.Value ? string.Empty : row["ESEP_LIVING_ADD_3"].ToString(),
                ESEP_POLICE_STATION = row["ESEP_POLICE_STATION"] == DBNull.Value ? string.Empty : row["ESEP_POLICE_STATION"].ToString(),
                ESEP_MOBI_NO = row["ESEP_MOBI_NO"] == DBNull.Value ? string.Empty : row["ESEP_MOBI_NO"].ToString(),
                ESEP_TEL_HOME_NO = row["ESEP_TEL_HOME_NO"] == DBNull.Value ? string.Empty : row["ESEP_TEL_HOME_NO"].ToString(),
                ESEP_TEL_OFF_NO = row["ESEP_TEL_OFF_NO"] == DBNull.Value ? string.Empty : row["ESEP_TEL_OFF_NO"].ToString(),
                ESEP_NIC = row["ESEP_NIC"] == DBNull.Value ? string.Empty : row["ESEP_NIC"].ToString(),
                ESEP_CONTRACTOR = row["ESEP_CONTRACTOR"] == DBNull.Value ? string.Empty : row["ESEP_CONTRACTOR"].ToString(),
                //ESEP_IS_MAX_STOCK_VAL = row["ESEP_IS_MAX_STOCK_VAL"] == DBNull.Value ? 0 : Convert.ToInt32(row["ESEP_IS_MAX_STOCK_VAL"].ToString()),
                //ESEP_MAX_STOCK_VAL = row["ESEP_MAX_STOCK_VAL"] == DBNull.Value ? 0 : Convert.ToInt32(row["ESEP_MAX_STOCK_VAL"].ToString()),
                ESEP_ACT = row["ESEP_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["ESEP_ACT"].ToString()),
                ESEP_CRE_BY = row["ESEP_CRE_BY"] == DBNull.Value ? string.Empty : row["ESEP_CRE_BY"].ToString(),
                ESEP_CRE_DT = row["ESEP_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ESEP_CRE_DT"].ToString()),
                ESEP_MOD_BY = row["ESEP_MOD_BY"] == DBNull.Value ? string.Empty : row["ESEP_MOD_BY"].ToString(),
                ESEP_MOD_DT = row["ESEP_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ESEP_MOD_DT"].ToString()),
                ESEP_SESSION_ID = row["ESEP_SESSION_ID"] == DBNull.Value ? string.Empty : row["ESEP_SESSION_ID"].ToString(),
                ESEP_DEF_PROFIT = row["ESEP_DEF_PROFIT"] == DBNull.Value ? string.Empty : row["ESEP_DEF_PROFIT"].ToString(),
                ESEP_SUPWISE_CD = row["ESEP_SUPWISE_CD"] == DBNull.Value ? string.Empty : row["ESEP_SUPWISE_CD"].ToString(),
                ESEP_EMAIL = row["ESEP_EMAIL"] == DBNull.Value ? string.Empty : row["ESEP_EMAIL"].ToString(),
                ESEP_DL_NO = row["ESEP_DL_NO"] == DBNull.Value ? string.Empty : row["ESEP_DL_NO"].ToString(),
                ESEP_DL_ISSDT = row["ESEP_DL_ISSDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ESEP_DL_ISSDT"].ToString()),
                ESEP_DL_EXPDT = row["ESEP_DL_EXPDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ESEP_DL_EXPDT"].ToString()),
                ESEP_DL_CLASS = row["ESEP_DL_CLASS"] == DBNull.Value ? string.Empty : row["ESEP_DL_CLASS"].ToString(),
                ESEP_DOJ = row["ESEP_DOJ"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ESEP_DOJ"].ToString())
            }; 
        }
    } 

}
