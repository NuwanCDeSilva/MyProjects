using System;
using System.Data;

namespace FF.BusinessObjects
{
    public class InterVoucherDetails
    {
        public Int32 GIVD_SEQ { get; set; }
        public Int32 GIVD_LINE { get; set; }
        public String GIVD_VOU_NO { get; set; }
        public String GIVD_EXPE_CD { get; set; }
        public String GIVD_EXPE_DESC { get; set; }
        public Decimal GIVD_EXPE_VAL { get; set; }
        public int GIVD_EXPE_DIRECT { get; set; }
        public String GIVD_PREP_BY { get; set; }
        public String GIVD_ACPT_BY { get; set; }
        public String GIVD_AUTH_BY { get; set; }
        public Int32 GIVD_ANAL1 { get; set; }
        public String GIVD_ANAL2 { get; set; }
        public DateTime GVID_DT { get; set; }
        public String GIVD_ANAL3 { get; set; }
        public String GIVD_ANAL4 { get; set; }
        public String GIVD_ANAL5 { get; set; }
        public String GIVD_ANAL6 { get; set; }
        public String GIVD_ANAL7 { get; set; }
        public String GIVD_ANAL8 { get; set; }
        public String GIVD_ANAL9 { get; set; }
        public String GIVD_ANAL10 { get; set; }
        public Int32 GIVD_ANAL11 { get; set; }
        public Int32 GIVD_ANAL12 { get; set; }
        public Decimal Credt { get; set; }
        public Decimal Debit { get; set; }
        public String AccountCodeDescription { get; set; }
        public String DefaultSupplier { get; set; }
        public Int32 isSupplierPrint  { get; set; }

        public static InterVoucherDetails Converter(DataRow row)
        {
            return new InterVoucherDetails
            {
                GIVD_SEQ = row["GIVD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["GIVD_SEQ"].ToString()),
                GIVD_LINE = row["GIVD_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["GIVD_LINE"].ToString()),
                GIVD_VOU_NO = row["GIVD_VOU_NO"] == DBNull.Value ? string.Empty : row["GIVD_VOU_NO"].ToString(),
                GIVD_EXPE_CD = row["GIVD_EXPE_CD"] == DBNull.Value ? string.Empty : row["GIVD_EXPE_CD"].ToString(),
                GIVD_EXPE_DESC = row["GIVD_EXPE_DESC"] == DBNull.Value ? string.Empty : row["GIVD_EXPE_DESC"].ToString(),
                GIVD_EXPE_VAL = row["GIVD_EXPE_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GIVD_EXPE_VAL"].ToString()),
                GIVD_EXPE_DIRECT = row["GIVD_EXPE_DIRECT"] == DBNull.Value ? 0 : Convert.ToInt32(row["GIVD_EXPE_DIRECT"].ToString()),
                GIVD_PREP_BY = row["GIVD_PREP_BY"] == DBNull.Value ? string.Empty : row["GIVD_PREP_BY"].ToString(),
                GIVD_ACPT_BY = row["GIVD_ACPT_BY"] == DBNull.Value ? string.Empty : row["GIVD_ACPT_BY"].ToString(),
                GIVD_AUTH_BY = row["GIVD_AUTH_BY"] == DBNull.Value ? string.Empty : row["GIVD_AUTH_BY"].ToString(),
                GIVD_ANAL1 = row["GIVD_ANAL1"] == DBNull.Value ? 0 : Convert.ToInt32(row["GIVD_ANAL1"].ToString()),
                GIVD_ANAL2 = row["GIVD_ANAL2"] == DBNull.Value ? string.Empty : row["GIVD_ANAL2"].ToString(),
                GVID_DT = row["GVID_DT"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(row["GVID_DT"].ToString()),
                GIVD_ANAL3 = row["GIVD_ANAL3"] == DBNull.Value ? string.Empty : row["GIVD_ANAL3"].ToString(),
                GIVD_ANAL4 = row["GIVD_ANAL4"] == DBNull.Value ? string.Empty : row["GIVD_ANAL4"].ToString(),
                GIVD_ANAL5 = row["GIVD_ANAL5"] == DBNull.Value ? string.Empty : row["GIVD_ANAL5"].ToString(),
                GIVD_ANAL6 = row["GIVD_ANAL6"] == DBNull.Value ? string.Empty : row["GIVD_ANAL6"].ToString(),
                GIVD_ANAL7 = row["GIVD_ANAL7"] == DBNull.Value ? string.Empty : row["GIVD_ANAL7"].ToString(),
                GIVD_ANAL8 = row["GIVD_ANAL8"] == DBNull.Value ? string.Empty : row["GIVD_ANAL8"].ToString(),
                GIVD_ANAL9 = row["GIVD_ANAL9"] == DBNull.Value ? string.Empty : row["GIVD_ANAL9"].ToString(),
                GIVD_ANAL10 = row["GIVD_ANAL10"] == DBNull.Value ? string.Empty : row["GIVD_ANAL10"].ToString(),
                GIVD_ANAL11 = row["GIVD_ANAL11"] == DBNull.Value ? 0 : Convert.ToInt32(row["GIVD_ANAL11"].ToString()),
                GIVD_ANAL12 = row["GIVD_ANAL12"] == DBNull.Value ? 0 : Convert.ToInt32(row["GIVD_ANAL12"].ToString()),
            };
        }
    }
}

