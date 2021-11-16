using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Sales
{
   public class MST_PROFIT_CENTER
    {
        public String MPC_COM { get; set; }
        public String MPC_CD { get; set; }
        public String MPC_DESC { get; set; }
        public String MPC_TP { get; set; }
        public String MPC_OTH_REF { get; set; }
        public String MPC_ADD_1 { get; set; }
        public String MPC_ADD_2 { get; set; }
        public String MPC_TEL { get; set; }
        public String MPC_FAX { get; set; }
        public Int32 MPC_ACT { get; set; }
        public String MPC_CHNL { get; set; }
        public String MPC_OPE_CD { get; set; }
        public String MPC_DEF_PB { get; set; }
        public Int32 MPC_EDIT_PRICE { get; set; }
        public Int32 MPC_CHK_CREDIT { get; set; }
        public Int32 MPC_EDIT_RATE { get; set; }
        public Int32 MPC_DEF_DIS_RATE { get; set; }
        public Int32 MPC_PRINT_WARA_REMARKS { get; set; }
        public Int32 MPC_INTER_COM { get; set; }
        public Int32 MPC_PRINT_DIS { get; set; }
        public Int32 MPC_PRINT_PAYMENT { get; set; }
        public Int32 MPC_CHECK_PAY { get; set; }
        public Int32 MPC_CHECK_CM { get; set; }
        public Int32 MPC_WITHOUT_PRICE { get; set; }
        public Int32 MPC_ORDER_VALID_PD { get; set; }
        public Int32 MPC_ORDER_RESTRIC { get; set; }
        public Int32 MPC_WARA_EXTEND { get; set; }
        public Int32 MPC_SO_SMS { get; set; }
        public Int32 MPC_MULTI_DEPT { get; set; }
        public String MPC_DEF_DEPT { get; set; }
        public String MPC_DEF_LOC { get; set; }
        public String MPC_MAN { get; set; }
        public String MPC_DEF_EXRATE { get; set; }
        public String MPC_DEF_CUSTOMER { get; set; }
        public Int32 MPC_ADD_HOURS { get; set; }
        public String MPC_EMAIL { get; set; }
        public DateTime MPC_FWD_SALE_ST { get; set; }
        public Int32 MPC_MAX_FWDSALE { get; set; }
        public Int32 MPC_HP_SYS_REC { get; set; }
        public Int32 MPC_IS_CHK_MAN_DOC { get; set; }
        public Int32 MPC_IS_DO_NOW { get; set; }
        public String MPC_DIST { get; set; }
        public String MPC_PROV { get; set; }
        public DateTime MPC_OPN_DT { get; set; }
        public Int32 MPC_SQ_FT { get; set; }
        public String MPC_MAN_NAME { get; set; }
        public DateTime MPC_JOINED_DT { get; set; }
        public DateTime MPC_HOVR_DT { get; set; }
        public Int32 MPC_NO_OF_STAFF { get; set; }
        public String MPC_GRADE { get; set; }
        public Int32 MPC_NUM_FWDSALE { get; set; }
        public Int32 MPC_MUL_CRNOTE { get; set; }
        public String MPC_CATE { get; set; }
        public Int32 MPC_ISSP_TAX { get; set; }
        public String MPC_CRE_BY { get; set; }
        public DateTime MPC_CRE_DT { get; set; }
        public String MPC_MOD_BY { get; set; }
        public DateTime MPC_MOD_DT { get; set; }
        public Int32 MPC_SO_RES { get; set; }
        public Int32 MPC_SO_REST_STK { get; set; }
        public Int32 MPC_ORD_ALPBT { get; set; }
        public Int32 MPC_CHK_AUTO_APP { get; set; }
        public static MST_PROFIT_CENTER Converter(DataRow row)
        {
            return new MST_PROFIT_CENTER
            {
                MPC_COM = row["MPC_COM"] == DBNull.Value ? string.Empty : row["MPC_COM"].ToString(),
                MPC_CD = row["MPC_CD"] == DBNull.Value ? string.Empty : row["MPC_CD"].ToString(),
                MPC_DESC = row["MPC_DESC"] == DBNull.Value ? string.Empty : row["MPC_DESC"].ToString(),
                MPC_TP = row["MPC_TP"] == DBNull.Value ? string.Empty : row["MPC_TP"].ToString(),
                MPC_OTH_REF = row["MPC_OTH_REF"] == DBNull.Value ? string.Empty : row["MPC_OTH_REF"].ToString(),
                MPC_ADD_1 = row["MPC_ADD_1"] == DBNull.Value ? string.Empty : row["MPC_ADD_1"].ToString(),
                MPC_ADD_2 = row["MPC_ADD_2"] == DBNull.Value ? string.Empty : row["MPC_ADD_2"].ToString(),
                MPC_TEL = row["MPC_TEL"] == DBNull.Value ? string.Empty : row["MPC_TEL"].ToString(),
                MPC_FAX = row["MPC_FAX"] == DBNull.Value ? string.Empty : row["MPC_FAX"].ToString(),
                MPC_ACT = row["MPC_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_ACT"].ToString()),
                MPC_CHNL = row["MPC_CHNL"] == DBNull.Value ? string.Empty : row["MPC_CHNL"].ToString(),
                MPC_OPE_CD = row["MPC_OPE_CD"] == DBNull.Value ? string.Empty : row["MPC_OPE_CD"].ToString(),
                MPC_DEF_PB = row["MPC_DEF_PB"] == DBNull.Value ? string.Empty : row["MPC_DEF_PB"].ToString(),
                MPC_EDIT_PRICE = row["MPC_EDIT_PRICE"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_EDIT_PRICE"].ToString()),
                MPC_CHK_CREDIT = row["MPC_CHK_CREDIT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_CHK_CREDIT"].ToString()),
                MPC_EDIT_RATE = row["MPC_EDIT_RATE"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_EDIT_RATE"].ToString()),
                MPC_DEF_DIS_RATE = row["MPC_DEF_DIS_RATE"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_DEF_DIS_RATE"].ToString()),
                MPC_PRINT_WARA_REMARKS = row["MPC_PRINT_WARA_REMARKS"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_PRINT_WARA_REMARKS"].ToString()),
                MPC_INTER_COM = row["MPC_INTER_COM"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_INTER_COM"].ToString()),
                MPC_PRINT_DIS = row["MPC_PRINT_DIS"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_PRINT_DIS"].ToString()),
                MPC_PRINT_PAYMENT = row["MPC_PRINT_PAYMENT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_PRINT_PAYMENT"].ToString()),
                MPC_CHECK_PAY = row["MPC_CHECK_PAY"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_CHECK_PAY"].ToString()),
                MPC_CHECK_CM = row["MPC_CHECK_CM"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_CHECK_CM"].ToString()),
                MPC_WITHOUT_PRICE = row["MPC_WITHOUT_PRICE"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_WITHOUT_PRICE"].ToString()),
                MPC_ORDER_VALID_PD = row["MPC_ORDER_VALID_PD"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_ORDER_VALID_PD"].ToString()),
                MPC_ORDER_RESTRIC = row["MPC_ORDER_RESTRIC"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_ORDER_RESTRIC"].ToString()),
                MPC_WARA_EXTEND = row["MPC_WARA_EXTEND"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_WARA_EXTEND"].ToString()),
                MPC_SO_SMS = row["MPC_SO_SMS"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_SO_SMS"].ToString()),
                MPC_MULTI_DEPT = row["MPC_MULTI_DEPT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_MULTI_DEPT"].ToString()),
                MPC_DEF_DEPT = row["MPC_DEF_DEPT"] == DBNull.Value ? string.Empty : row["MPC_DEF_DEPT"].ToString(),
                MPC_DEF_LOC = row["MPC_DEF_LOC"] == DBNull.Value ? string.Empty : row["MPC_DEF_LOC"].ToString(),
                MPC_MAN = row["MPC_MAN"] == DBNull.Value ? string.Empty : row["MPC_MAN"].ToString(),
                MPC_DEF_EXRATE = row["MPC_DEF_EXRATE"] == DBNull.Value ? string.Empty : row["MPC_DEF_EXRATE"].ToString(),
                MPC_DEF_CUSTOMER = row["MPC_DEF_CUSTOMER"] == DBNull.Value ? string.Empty : row["MPC_DEF_CUSTOMER"].ToString(),
                MPC_ADD_HOURS = row["MPC_ADD_HOURS"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_ADD_HOURS"].ToString()),
                MPC_EMAIL = row["MPC_EMAIL"] == DBNull.Value ? string.Empty : row["MPC_EMAIL"].ToString(),
                MPC_FWD_SALE_ST = row["MPC_FWD_SALE_ST"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPC_FWD_SALE_ST"].ToString()),
                MPC_MAX_FWDSALE = row["MPC_MAX_FWDSALE"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_MAX_FWDSALE"].ToString()),
                MPC_HP_SYS_REC = row["MPC_HP_SYS_REC"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_HP_SYS_REC"].ToString()),
                MPC_IS_CHK_MAN_DOC = row["MPC_IS_CHK_MAN_DOC"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_IS_CHK_MAN_DOC"].ToString()),
                MPC_IS_DO_NOW = row["MPC_IS_DO_NOW"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_IS_DO_NOW"].ToString()),
                MPC_DIST = row["MPC_DIST"] == DBNull.Value ? string.Empty : row["MPC_DIST"].ToString(),
                MPC_PROV = row["MPC_PROV"] == DBNull.Value ? string.Empty : row["MPC_PROV"].ToString(),
                MPC_OPN_DT = row["MPC_OPN_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPC_OPN_DT"].ToString()),
                MPC_SQ_FT = row["MPC_SQ_FT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_SQ_FT"].ToString()),
                MPC_MAN_NAME = row["MPC_MAN_NAME"] == DBNull.Value ? string.Empty : row["MPC_MAN_NAME"].ToString(),
                MPC_JOINED_DT = row["MPC_JOINED_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPC_JOINED_DT"].ToString()),
                MPC_HOVR_DT = row["MPC_HOVR_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPC_HOVR_DT"].ToString()),
                MPC_NO_OF_STAFF = row["MPC_NO_OF_STAFF"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_NO_OF_STAFF"].ToString()),
                MPC_GRADE = row["MPC_GRADE"] == DBNull.Value ? string.Empty : row["MPC_GRADE"].ToString(),
                MPC_NUM_FWDSALE = row["MPC_NUM_FWDSALE"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_NUM_FWDSALE"].ToString()),
                MPC_MUL_CRNOTE = row["MPC_MUL_CRNOTE"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_MUL_CRNOTE"].ToString()),
                MPC_CATE = row["MPC_CATE"] == DBNull.Value ? string.Empty : row["MPC_CATE"].ToString(),
                MPC_ISSP_TAX = row["MPC_ISSP_TAX"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_ISSP_TAX"].ToString()),
                MPC_CRE_BY = row["MPC_CRE_BY"] == DBNull.Value ? string.Empty : row["MPC_CRE_BY"].ToString(),
                MPC_CRE_DT = row["MPC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPC_CRE_DT"].ToString()),
                MPC_MOD_BY = row["MPC_MOD_BY"] == DBNull.Value ? string.Empty : row["MPC_MOD_BY"].ToString(),
                MPC_MOD_DT = row["MPC_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPC_MOD_DT"].ToString()),
                MPC_SO_RES = row["MPC_SO_RES"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_SO_RES"].ToString()),
                MPC_SO_REST_STK = row["MPC_SO_REST_STK"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_SO_REST_STK"].ToString()),
                MPC_ORD_ALPBT = row["MPC_ORD_ALPBT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_ORD_ALPBT"].ToString()),
                MPC_CHK_AUTO_APP = row["MPC_CHK_AUTO_APP"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_CHK_AUTO_APP"].ToString())
            };
        } 
    }
}
