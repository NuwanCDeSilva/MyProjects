using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class MST_FAC_LOC
    {
        public Int32 FAC_SEQ { get; set; }
        public String FAC_COM { get; set; }
        public String FAC_PC { get; set; }
        public String FAC_CODE { get; set; }
        public String FAC_DESC { get; set; }
        public Int32 FAC_ACT { get; set; }
        public String FAC_CRE_BY { get; set; }
        public DateTime FAC_CRE_DT { get; set; }
        public String FAC_MOD_BY { get; set; }
        public DateTime FAC_MOD_DT { get; set; }
        public static MST_FAC_LOC Converter(DataRow row)
        {
            return new MST_FAC_LOC
            {
                FAC_SEQ = row["FAC_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["FAC_SEQ"].ToString()),
                FAC_COM = row["FAC_COM"] == DBNull.Value ? string.Empty : row["FAC_COM"].ToString(),
                FAC_PC = row["FAC_PC"] == DBNull.Value ? string.Empty : row["FAC_PC"].ToString(),
                FAC_CODE = row["FAC_CODE"] == DBNull.Value ? string.Empty : row["FAC_CODE"].ToString(),
                FAC_DESC = row["FAC_DESC"] == DBNull.Value ? string.Empty : row["FAC_DESC"].ToString(),
                FAC_ACT = row["FAC_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["FAC_ACT"].ToString()),
                FAC_CRE_BY = row["FAC_CRE_BY"] == DBNull.Value ? string.Empty : row["FAC_CRE_BY"].ToString(),
                FAC_CRE_DT = row["FAC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["FAC_CRE_DT"].ToString()),
                FAC_MOD_BY = row["FAC_MOD_BY"] == DBNull.Value ? string.Empty : row["FAC_MOD_BY"].ToString(),
                FAC_MOD_DT = row["FAC_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["FAC_MOD_DT"].ToString())
            };
        }
    } 

}
