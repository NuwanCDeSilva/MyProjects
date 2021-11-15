using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Sales
{
    public class sar_tp
    {

        public String srtp_cd { get; set; }
        public String srtp_desc { get; set; }
        public String srtp_cate { get; set; }
        public Int32 srtp_is_main { get; set; }
        public String srtp_sun_status { get; set; }
        public String srtp_main_tp { get; set; }


        public static sar_tp Converter(DataRow row)
        {
            return new sar_tp
            {

                srtp_cd = row["SRTP_CD"] == DBNull.Value ? string.Empty : row["SRTP_CD"].ToString(),
                srtp_desc = row["SRTP_DESC"] == DBNull.Value ? string.Empty : row["SRTP_DESC"].ToString(),
                srtp_cate = row["SRTP_CATE"] == DBNull.Value ? string.Empty : row["SRTP_CATE"].ToString(),
                srtp_is_main = row["SRTP_IS_MAIN"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRTP_IS_MAIN"].ToString()),
                srtp_sun_status = row["SRTP_SUN_STATUS"] == DBNull.Value ? string.Empty : row["SRTP_SUN_STATUS"].ToString(),
                srtp_main_tp = row["SRTP_MAIN_TP"] == DBNull.Value ? string.Empty : row["SRTP_MAIN_TP"].ToString(),

            };
        }
    }
}
