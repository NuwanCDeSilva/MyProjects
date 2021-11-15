using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class MST_LOYALTY_SEARCH
    {
        public string salcm_cd_ser { get; set; }
        public string salcm_loty_tp { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public Int32 SELECT { get; set; }
        public static MST_LOYALTY_SEARCH Converter(DataRow row)
        {
            return new MST_LOYALTY_SEARCH
            {
                salcm_cd_ser = row["salcm_cd_ser"] == DBNull.Value ? string.Empty : row["salcm_cd_ser"].ToString(),
                salcm_loty_tp = row["salcm_loty_tp"] == DBNull.Value ? string.Empty : row["salcm_loty_tp"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
        public static MST_LOYALTY_SEARCH Converter1(DataRow row)
        {
            return new MST_LOYALTY_SEARCH
            {
                //salcm_cd_ser = row["salcm_cd_ser"] == DBNull.Value ? string.Empty : row["salcm_cd_ser"].ToString(),
                salcm_loty_tp = row["salcm_loty_tp"] == DBNull.Value ? string.Empty : row["salcm_loty_tp"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }

    public class MST_LOYALTY_SELECTED
    {
        public string salcm_cd_ser { get; set; }
        public string salcm_loty_tp { get; set; }
    }

}
