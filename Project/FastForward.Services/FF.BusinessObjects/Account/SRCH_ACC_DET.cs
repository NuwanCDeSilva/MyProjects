using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Account
{
    public class SRCH_ACC_DET
    {
        public string RCA_ACC_NO { get; set; }
        public string RCA_ACC_DESC { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public static SRCH_ACC_DET Converter(DataRow row)
        {
            return new SRCH_ACC_DET
            {
                RCA_ACC_NO = row["RCA_ACC_NO"] == DBNull.Value ? string.Empty : row["RCA_ACC_NO"].ToString(),
                RCA_ACC_DESC = row["RCA_ACC_DESC"] == DBNull.Value ? string.Empty : row["RCA_ACC_DESC"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }
}
