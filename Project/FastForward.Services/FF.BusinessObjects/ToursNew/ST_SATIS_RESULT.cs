using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class ST_SATIS_RESULT
    {
        public Int32 SSVL_QSTSEQ { get; set; }
        public Int32 SSVL_VALSEQ { get; set; }
        public String SSVL_ANS { get; set; }
        public String SSVL_ENQID { get; set; }
        public String SSVL_CHNL { get; set; }
        public String SSVL_PC { get; set; }
        public String SSVL_CRE_BY { get; set; }
        public DateTime SSVL_CRE_DT { get; set; }
        public String SSVL_MOD_BY { get; set; }
        public DateTime SSVL_MOD_DT { get; set; }
        public string SSVL_COM { get; set; }
        public static ST_SATIS_RESULT Converter(DataRow row)
        {
            return new ST_SATIS_RESULT
            {
                SSVL_QSTSEQ = row["SSVL_QSTSEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSVL_QSTSEQ"].ToString()),
                SSVL_VALSEQ = row["SSVL_VALSEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSVL_VALSEQ"].ToString()),
                SSVL_ANS = row["SSVL_ANS"] == DBNull.Value ? string.Empty : row["SSVL_ANS"].ToString(),
                SSVL_ENQID = row["SSVL_ENQID"] == DBNull.Value ? string.Empty : row["SSVL_ENQID"].ToString(),
                SSVL_CHNL = row["SSVL_CHNL"] == DBNull.Value ? string.Empty : row["SSVL_CHNL"].ToString(),
                SSVL_PC = row["SSVL_PC"] == DBNull.Value ? string.Empty : row["SSVL_PC"].ToString(),
                SSVL_CRE_BY = row["SSVL_CRE_BY"] == DBNull.Value ? string.Empty : row["SSVL_CRE_BY"].ToString(),
                SSVL_CRE_DT = row["SSVL_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SSVL_CRE_DT"].ToString()),
                SSVL_MOD_BY = row["SSVL_MOD_BY"] == DBNull.Value ? string.Empty : row["SSVL_MOD_BY"].ToString(),
                SSVL_MOD_DT = row["SSVL_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SSVL_MOD_DT"].ToString()),
                SSVL_COM = row["SSVL_COM"] == DBNull.Value ? string.Empty : row["SSVL_COM"].ToString()

            };
        }
    }
}

