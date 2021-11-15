using System;
using System.Data;

namespace FF.BusinessObjects
{
    //===========================================================================================================
    // This code is generated by Code gen V.1
    // Computer :- ITPD11  | User :- suneth On 14-Nov-2014 09:56:04
    //===========================================================================================================

    public class Service_SupplierWarrantyClaim
    {
        public Int32 SEQ { get; set; }

        public String JOB { get; set; }

        public Int32 JOBLINE { get; set; }

        public String ITEM { get; set; }

        public String ItemStatus { get; set; }

        public String Serial { get; set; }

        public Decimal QTY { get; set; }

        public String FROMTABLE { get; set; }

        public String PartID { get; set; }

        public String OEM { get; set; }

        public String CaseID { get; set; }

        public Int32 JBD_ISEXTERNALITM { get; set; }

        public String JBD_SUPP_CD { get; set; }

        public Int32 JBD_WARR_STUS { get; set; }

        public String MI_CATE_1 { get; set; }

        public String MI_CATE_2 { get; set; }

        public String MI_CATE_3 { get; set; }

        public String MI_BRAND { get; set; }

        public DateTime Date { get; set; }

        public Decimal JobStage { get; set; }

        public String User { get; set; }


        public static Service_SupplierWarrantyClaim Converter(DataRow row)
        {
            return new Service_SupplierWarrantyClaim
        {
            SEQ = row["SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SEQ"].ToString()),
            JOB = row["JOB"] == DBNull.Value ? string.Empty : row["JOB"].ToString(),
            JOBLINE = row["JOBLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JOBLINE"].ToString()),
            ITEM = row["ITEM"] == DBNull.Value ? string.Empty : row["ITEM"].ToString(),
            ItemStatus = row["ItemStatus"] == DBNull.Value ? string.Empty : row["ItemStatus"].ToString(),
            Serial = row["Serial"] == DBNull.Value ? string.Empty : row["Serial"].ToString(),
            QTY = row["QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QTY"].ToString()),
            FROMTABLE = row["FROMTABLE"] == DBNull.Value ? string.Empty : row["FROMTABLE"].ToString(),
            PartID = row["PartID"] == DBNull.Value ? string.Empty : row["PartID"].ToString(),
            OEM = row["OEM"] == DBNull.Value ? string.Empty : row["OEM"].ToString(),
            CaseID = row["CaseID"] == DBNull.Value ? string.Empty : row["CaseID"].ToString(),
            JBD_ISEXTERNALITM = row["JBD_ISEXTERNALITM"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_ISEXTERNALITM"].ToString()),
            JBD_SUPP_CD = row["JBD_SUPP_CD"] == DBNull.Value ? string.Empty : row["JBD_SUPP_CD"].ToString(),
            JBD_WARR_STUS = row["JBD_WARR_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_WARR_STUS"].ToString()),
            MI_CATE_1 = row["MI_CATE_1"] == DBNull.Value ? string.Empty : row["MI_CATE_1"].ToString(),
            MI_CATE_2 = row["MI_CATE_2"] == DBNull.Value ? string.Empty : row["MI_CATE_2"].ToString(),
            MI_CATE_3 = row["MI_CATE_3"] == DBNull.Value ? string.Empty : row["MI_CATE_3"].ToString(),
            MI_BRAND = row["MI_BRAND"] == DBNull.Value ? string.Empty : row["MI_BRAND"].ToString()
        };
        }
    }
}