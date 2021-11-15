using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class MST_BUSCOM_BANKBRANCH_SEARCH_HEAD
    {
        public String MBB_CD { get; set; }
        public String MBB_DESC { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static MST_BUSCOM_BANKBRANCH_SEARCH_HEAD Converter(DataRow row)
        {
            return new MST_BUSCOM_BANKBRANCH_SEARCH_HEAD
            {

                MBB_CD = row["MBB_CD"] == DBNull.Value ? string.Empty : row["MBB_CD"].ToString(),
                MBB_DESC = row["MBB_DESC"] == DBNull.Value ? string.Empty : row["MBB_DESC"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString(),
            };
        }
    }
}
