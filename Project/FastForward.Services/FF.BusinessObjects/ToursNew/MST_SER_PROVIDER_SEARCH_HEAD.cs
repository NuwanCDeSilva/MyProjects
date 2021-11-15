using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
  public  class MST_SER_PROVIDER_SEARCH_HEAD
    {
        public String MSP_CD { get; set; }
        public String MSP_COM { get; set; }
        public String MSP_CATE { get; set; }
        public String MSP_DESC { get; set; }
        public Int32 MSP_ACT { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static MST_SER_PROVIDER_SEARCH_HEAD Converter(DataRow row)
        {
            return new MST_SER_PROVIDER_SEARCH_HEAD
            {

                MSP_CD = row["MSP_CD"] == DBNull.Value ? string.Empty : row["MSP_CD"].ToString(),
                MSP_COM = row["MSP_COM"] == DBNull.Value ? string.Empty : row["MSP_COM"].ToString(),
                MSP_CATE = row["MSP_CATE"] == DBNull.Value ? string.Empty : row["MSP_CATE"].ToString(),
                MSP_DESC = row["MSP_DESC"] == DBNull.Value ? string.Empty : row["MSP_DESC"].ToString(),
                MSP_ACT = row["MSP_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MSP_ACT"].ToString()),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString(),
            };
        }
    }
}
