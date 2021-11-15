using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects.BITool
{
   public class Exchange_Reversal
    {
        public string sah_com { get; set; }
        public string sah_pc { get; set; }
        public string mmct_scd { get; set; }
        public string mmct_sdesc { get; set; }
        public string sad_itm_cd { get; set; }
        public string mi_cate_1 { get; set; }
        public string mi_brand { get; set; }
        public string mi_model { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static Exchange_Reversal Converter(DataRow row)
        {
            return new Exchange_Reversal
            {
                sah_com = row["sah_com"] == DBNull.Value ? string.Empty : row["sah_com"].ToString(),
                sah_pc = row["sah_pc"] == DBNull.Value ? string.Empty : row["sah_pc"].ToString(),
                mmct_scd = row["mmct_scd"] == DBNull.Value ? string.Empty : row["mmct_scd"].ToString(),
                mmct_sdesc = row["mmct_sdesc"] == DBNull.Value ? string.Empty : row["mmct_sdesc"].ToString(),
                sad_itm_cd = row["sad_itm_cd"] == DBNull.Value ? string.Empty : row["sad_itm_cd"].ToString(),
                mi_cate_1 = row["mi_cate_1"] == DBNull.Value ? string.Empty : row["mi_cate_1"].ToString(),
                mi_brand = row["mi_brand"] == DBNull.Value ? string.Empty : row["mi_brand"].ToString(),
                mi_model = row["mi_model"] == DBNull.Value ? string.Empty : row["mi_model"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()

            };
        } 
    }
}
