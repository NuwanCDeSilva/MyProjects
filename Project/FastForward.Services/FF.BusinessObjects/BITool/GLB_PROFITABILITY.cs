using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
   public class GLB_PROFITABILITY
    {
        public String HED_CD { get; set; }
        public String HED_DESC { get; set; }
        public Int32 HED_ORDER { get; set; }
        public String GRP_CD { get; set; }
        public String GRP_DESC { get; set; }
        public Int32 GRP_ORDER { get; set; }
        public Decimal TARGET { get; set; }
        public Decimal ACTUAL { get; set; }
        public Decimal PERCENTAGE { get; set; }
        public Int32 DIRECTION{ get; set; }
        public decimal PREVIOUS { get; set; }
        public String ITM_CAT1 { get; set; }
        public Int32 YEAR { get; set; }
        public Int32 MONTH { get; set; }
        public String ACCOUNT { get; set; }
        public decimal BUDGET { get; set; }
       
        public static GLB_PROFITABILITY Converter(DataRow row)
        {
            return new GLB_PROFITABILITY
            {
                HED_CD = row["HED_CD"] == DBNull.Value ? string.Empty : row["HED_CD"].ToString(),
                HED_DESC = row["HED_DESC"] == DBNull.Value ? string.Empty : row["HED_DESC"].ToString(),
                HED_ORDER = row["HED_ORDER"] == DBNull.Value ? 0 : Convert.ToInt32(row["HED_ORDER"].ToString()),
                GRP_CD = row["GRP_CD"] == DBNull.Value ? string.Empty : row["GRP_CD"].ToString(),
                GRP_DESC = row["GRP_DESC"] == DBNull.Value ? string.Empty : row["GRP_DESC"].ToString(),
                GRP_ORDER = row["GRP_ORDER"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRP_ORDER"].ToString()),
                TARGET = row["TARGET"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TARGET"].ToString()),
                ACTUAL = row["ACTUAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACTUAL"].ToString()),
                DIRECTION = row["DIRECTION"] == DBNull.Value ? 0 : Convert.ToInt32(row["DIRECTION"].ToString()),
                PREVIOUS = row["PREVIOUS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["PREVIOUS"].ToString())
                

            };
        }
        public static GLB_PROFITABILITY Converter1(DataRow row)
        {
            return new GLB_PROFITABILITY
            {
                HED_CD = row["HED_CD"] == DBNull.Value ? string.Empty : row["HED_CD"].ToString(),
                HED_DESC = row["HED_DESC"] == DBNull.Value ? string.Empty : row["HED_DESC"].ToString(),
                HED_ORDER = row["HED_ORDER"] == DBNull.Value ? 0 : Convert.ToInt32(row["HED_ORDER"].ToString()),
                GRP_CD = row["GRP_CD"] == DBNull.Value ? string.Empty : row["GRP_CD"].ToString(),
                GRP_DESC = row["GRP_DESC"] == DBNull.Value ? string.Empty : row["GRP_DESC"].ToString(),
                GRP_ORDER = row["GRP_ORDER"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRP_ORDER"].ToString()),
                TARGET = row["TARGET"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TARGET"].ToString()),
                ACTUAL = row["ACTUAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACTUAL"].ToString()),
                DIRECTION = row["DIRECTION"] == DBNull.Value ? 0 : Convert.ToInt32(row["DIRECTION"].ToString()),
                PREVIOUS = row["PREVIOUS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["PREVIOUS"].ToString()),

                BUDGET = row["TARGET"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TARGET"].ToString()),
                YEAR = row["rbt_yer"] == DBNull.Value ? 0 : Convert.ToInt32(row["rbt_yer"].ToString()),
                MONTH = row["rbt_mnth"] == DBNull.Value ? 0 : Convert.ToInt32(row["rbt_mnth"].ToString()),
                ACCOUNT = row["rbt_acc_cd"] == DBNull.Value ? string.Empty : row["rbt_acc_cd"].ToString()
            };
        }

        public static GLB_PROFITABILITY Converter2(DataRow row)
        {
            return new GLB_PROFITABILITY
            {
                HED_CD = row["HED_CD"] == DBNull.Value ? string.Empty : row["HED_CD"].ToString(),
                HED_DESC = row["HED_DESC"] == DBNull.Value ? string.Empty : row["HED_DESC"].ToString(),
                HED_ORDER = row["HED_ORDER"] == DBNull.Value ? 0 : Convert.ToInt32(row["HED_ORDER"].ToString()),
                GRP_CD = row["GRP_CD"] == DBNull.Value ? string.Empty : row["GRP_CD"].ToString(),
                GRP_DESC = row["GRP_DESC"] == DBNull.Value ? string.Empty : row["GRP_DESC"].ToString(),
                GRP_ORDER = row["GRP_ORDER"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRP_ORDER"].ToString()),
                TARGET = row["TARGET"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TARGET"].ToString()),
                ACTUAL = row["ACTUAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACTUAL"].ToString()),
                DIRECTION = row["DIRECTION"] == DBNull.Value ? 0 : Convert.ToInt32(row["DIRECTION"].ToString()),
                PREVIOUS = row["PREVIOUS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["PREVIOUS"].ToString()),
                ITM_CAT1 = row["ITM_CAT1"] == DBNull.Value ? string.Empty : row["ITM_CAT1"].ToString(),
                BUDGET = row["TARGET"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TARGET"].ToString()),
                YEAR = row["rbt_yer"] == DBNull.Value ? 0 : Convert.ToInt32(row["rbt_yer"].ToString()),
                MONTH = row["rbt_mnth"] == DBNull.Value ? 0 : Convert.ToInt32(row["rbt_mnth"].ToString()),
                ACCOUNT = row["rbt_acc_cd"] == DBNull.Value ? string.Empty : row["rbt_acc_cd"].ToString()
            };
        } 
    }
}
