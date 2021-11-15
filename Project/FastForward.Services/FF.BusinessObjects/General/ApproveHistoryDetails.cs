using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    public class ApproveHistoryDetails
    {
        public string GRAH_COM{get;set;}
        public string GRAD_REQ_PARAM{get;set;}
        public decimal GRAD_VAL2{get;set;}
        public decimal GRAD_VAL5{get;set;}
        public Int32 GRAD_VAL1{get;set;}
        public decimal GRAD_ANAL18{get;set;}
        public string GRAD_ANAL2{get;set;}
        public string GRAD_ANAL3{get;set;}
        public string GRAD_ANAL5{get;set;}
        public string GRAD_ANAL7{get;set;}
        public string GRAD_ANAL8{get;set;}
        public string GRAD_ANAL10{get;set;}
        public string GRAD_ANAL11{get;set;}
        public string GRAD_ANAL15 { get; set; }
        public string GRAD_ANAL1{get;set;}
        public Int32 GRAD_LINE{get;set;}
        public DateTime GRAH_CRE_DT{get;set;}
        public string SE_USR_NAME{get;set;}
        public decimal TOT_UNIT_PRICE { get; set; }
        public ApproveItemDetail APP_ITEM_DET { get; set; }
        
        public static ApproveHistoryDetails Converter(DataRow row)
        {
            //ApproveHistoryDetails A = new ApproveHistoryDetails();
             
            //    A.GRAH_COM = row["GRAH_COM"] == DBNull.Value ? string.Empty : row["GRAH_COM"].ToString();
            //    A.GRAD_REQ_PARAM = row["GRAD_REQ_PARAM"] == DBNull.Value ? string.Empty : row["GRAD_REQ_PARAM"].ToString();
            //    A.GRAD_VAL2 = row["GRAD_VAL2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_VAL2"].ToString());
            //    A.GRAD_VAL5 = row["GRAD_VAL5"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_VAL5"].ToString());
            //    A.GRAD_VAL1 = row["GRAD_VAL1"] == DBNull.Value ? 0 :Convert.ToInt32(row["GRAD_VAL1"].ToString());
            //    A.GRAD_ANAL18 = row["GRAD_ANAL18"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_ANAL18"].ToString());
            //    A.GRAD_ANAL2 = row["GRAD_ANAL2"] == DBNull.Value ? string.Empty : row["GRAD_ANAL2"].ToString();
            //    A.GRAD_ANAL3 = row["GRAD_ANAL3"] == DBNull.Value ? string.Empty : row["GRAD_ANAL3"].ToString();
            //    A.GRAD_ANAL5 = row["GRAD_ANAL5"] == DBNull.Value ? string.Empty : row["GRAD_ANAL5"].ToString();
            //    A.GRAD_ANAL7 = row["GRAD_ANAL7"] == DBNull.Value ? string.Empty : row["GRAD_ANAL7"].ToString();
            //    A.GRAD_ANAL8 = row["GRAD_ANAL8"] == DBNull.Value ? string.Empty : row["GRAD_ANAL8"].ToString();
            //    A.GRAD_ANAL10 = row["GRAD_ANAL10"] == DBNull.Value ? string.Empty : row["GRAD_ANAL10"].ToString();
            //    A.GRAD_ANAL11 = row["GRAD_ANAL11"] == DBNull.Value ? string.Empty : row["GRAD_ANAL11"].ToString();
            //    A.GRAD_ANAL1 = row["GRAD_ANAL1"] == DBNull.Value ? string.Empty : row["GRAD_ANAL1"].ToString();
            //    A.GRAD_LINE = row["GRAD_LINE"] == DBNull.Value ? 0 :Convert.ToInt32(row["GRAD_LINE"].ToString());
            //    A.GRAH_CRE_DT = row["GRAH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRAH_CRE_DT"].ToString());
            //    A.SE_USR_NAME = row["SE_USR_NAME"] == DBNull.Value ? string.Empty : row["SE_USR_NAME"].ToString();
            //    A.GRAD_ANAL15 = row["GRAD_ANAL15"] == DBNull.Value ? string.Empty : row["GRAD_ANAL15"].ToString();
            //    return A;
                return new ApproveHistoryDetails
                {
                    GRAH_COM = row["GRAH_COM"] == DBNull.Value ? string.Empty : row["GRAH_COM"].ToString(),
                    GRAD_REQ_PARAM = row["GRAD_REQ_PARAM"] == DBNull.Value ? string.Empty : row["GRAD_REQ_PARAM"].ToString(),
                    GRAD_VAL2 = row["GRAD_VAL2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_VAL2"].ToString()),
                    GRAD_VAL5 = row["GRAD_VAL5"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_VAL5"].ToString()),
                    GRAD_VAL1 = row["GRAD_VAL1"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRAD_VAL1"].ToString()),
                    GRAD_ANAL18 = row["GRAD_ANAL18"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_ANAL18"].ToString()),
                    GRAD_ANAL2 = row["GRAD_ANAL2"] == DBNull.Value ? string.Empty : row["GRAD_ANAL2"].ToString(),
                    GRAD_ANAL3 = row["GRAD_ANAL3"] == DBNull.Value ? string.Empty : row["GRAD_ANAL3"].ToString(),
                    GRAD_ANAL5 = row["GRAD_ANAL5"] == DBNull.Value ? string.Empty : row["GRAD_ANAL5"].ToString(),
                    GRAD_ANAL7 = row["GRAD_ANAL7"] == DBNull.Value ? string.Empty : row["GRAD_ANAL7"].ToString(),
                    GRAD_ANAL8 = row["GRAD_ANAL8"] == DBNull.Value ? string.Empty : row["GRAD_ANAL8"].ToString(),
                    GRAD_ANAL10 = row["GRAD_ANAL10"] == DBNull.Value ? string.Empty : row["GRAD_ANAL10"].ToString(),
                    GRAD_ANAL11 = row["GRAD_ANAL11"] == DBNull.Value ? string.Empty : row["GRAD_ANAL11"].ToString(),
                    GRAD_ANAL1 = row["GRAD_ANAL1"] == DBNull.Value ? string.Empty : row["GRAD_ANAL1"].ToString(),
                    GRAD_LINE = row["GRAD_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRAD_LINE"].ToString()),
                    GRAH_CRE_DT = row["GRAH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRAH_CRE_DT"].ToString()),
                    SE_USR_NAME = row["SE_USR_NAME"] == DBNull.Value ? string.Empty : row["SE_USR_NAME"].ToString(),
                    GRAD_ANAL15 = row["GRAD_ANAL15"] == DBNull.Value ? string.Empty : row["GRAD_ANAL15"].ToString()
                };
        }
    }
}
