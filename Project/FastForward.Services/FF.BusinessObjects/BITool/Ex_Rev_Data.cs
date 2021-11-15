using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects.BITool
{
    public class Ex_Rev_Data
    {
        public string mmct_sdesc { get; set; }
        public string sah_pc { get; set; }
        public string mmct_scd { get; set; }
        public string mi_cate_1 { get; set; }
        public Int32 reversal { get; set; }
        public string channel { get; set; }
        public string mpc_desc { get; set; }
        public string ric1_desc { get; set; }
        public string sah_pc_Code { get; set; }

        public static Ex_Rev_Data Converter(DataRow row)
        {
            return new Ex_Rev_Data
            {
                mmct_sdesc = row["mmct_sdesc"] == DBNull.Value ? string.Empty : row["mmct_sdesc"].ToString(),
                sah_pc = row["sah_pc"] == DBNull.Value ? string.Empty : row["sah_pc"].ToString(),
                mmct_scd = row["mmct_scd"] == DBNull.Value ? string.Empty : row["mmct_scd"].ToString(),
                mi_cate_1 = row["mi_cate_1"] == DBNull.Value ? string.Empty : row["mi_cate_1"].ToString(),
                reversal = row["reversal"] == DBNull.Value ? 0 : Convert.ToInt32(row["reversal"].ToString()),
                channel = row["channel"] == DBNull.Value ? string.Empty : row["channel"].ToString(),
                mpc_desc = row["mpc_desc"] == DBNull.Value ? string.Empty : row["mpc_desc"].ToString(),
                ric1_desc = row["ric1_desc"] == DBNull.Value ? string.Empty : row["ric1_desc"].ToString(),
                sah_pc_Code = row["sah_pc_Code"] == DBNull.Value ? string.Empty : row["sah_pc_Code"].ToString(),
            };
        }
    }
}
