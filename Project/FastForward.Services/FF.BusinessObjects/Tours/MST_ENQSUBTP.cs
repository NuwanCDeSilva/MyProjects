using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Tours
{
    public class MST_ENQSUBTP
    {
        public String MEST_COM { get; set; }
        public String MEST_TPCD { get; set; }
        public String MEST_STPCD { get; set; }
        public String MEST_DESC { get; set; }
        public Int32 MEST_ACT { get; set; }
        public String MEST_CRE_BY { get; set; }
        public DateTime MEST_CRE_DT { get; set; }
        public String MEST_MOD_BY { get; set; }
        public DateTime MEST_MOD_DT { get; set; }
        public static MST_ENQSUBTP Converter(DataRow row)
        {
            return new MST_ENQSUBTP
            {
                MEST_COM = row["MEST_COM"] == DBNull.Value ? string.Empty : row["MEST_COM"].ToString(),
                MEST_TPCD = row["MEST_TPCD"] == DBNull.Value ? string.Empty : row["MEST_TPCD"].ToString(),
                MEST_STPCD = row["MEST_STPCD"] == DBNull.Value ? string.Empty : row["MEST_STPCD"].ToString(),
                MEST_DESC = row["MEST_DESC"] == DBNull.Value ? string.Empty : row["MEST_DESC"].ToString(),
                MEST_ACT = row["MEST_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MEST_ACT"].ToString()),
                MEST_CRE_BY = row["MEST_CRE_BY"] == DBNull.Value ? string.Empty : row["MEST_CRE_BY"].ToString(),
                MEST_CRE_DT = row["MEST_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MEST_CRE_DT"].ToString()),
                MEST_MOD_BY = row["MEST_MOD_BY"] == DBNull.Value ? string.Empty : row["MEST_MOD_BY"].ToString(),
                MEST_MOD_DT = row["MEST_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MEST_MOD_DT"].ToString())
            };
        }
    }
}