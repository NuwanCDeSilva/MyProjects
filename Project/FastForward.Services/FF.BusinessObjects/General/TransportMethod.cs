using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    public class TransportMethod
    {
        public Int32 Rtm_seq { get; set; }
        public String Rtm_tp { get; set; }
        public Int32 Rtm_act { get; set; }
        public Int32 Rtm_def { get; set; }
        public Int32 Rtm_sub_lvl { get; set; }
        public String Rtm_sub_dis { get; set; }
        public Int32 Rtm_nxt_lvl { get; set; }
        public String Rtm_disp_1 { get; set; }
        public String Rtm_disp_2 { get; set; }
        public static TransportMethod Converter(DataRow row)
        {
            return new TransportMethod
            {
                Rtm_seq = row["RTM_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTM_SEQ"].ToString()),
                Rtm_tp = row["RTM_TP"] == DBNull.Value ? string.Empty : row["RTM_TP"].ToString(),
                Rtm_act = row["RTM_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTM_ACT"].ToString()),
                Rtm_def = row["RTM_DEF"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTM_DEF"].ToString()),
                Rtm_sub_lvl = row["RTM_SUB_LVL"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTM_SUB_LVL"].ToString()),
                Rtm_sub_dis = row["RTM_SUB_DIS"] == DBNull.Value ? string.Empty : row["RTM_SUB_DIS"].ToString(),
                Rtm_nxt_lvl = row["RTM_NXT_LVL"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTM_NXT_LVL"].ToString()),
                Rtm_disp_1 = row["RTM_DISP_1"] == DBNull.Value ? string.Empty : row["RTM_DISP_1"].ToString(),
                Rtm_disp_2 = row["RTM_DISP_2"] == DBNull.Value ? string.Empty : row["RTM_DISP_2"].ToString()
            };
        }
    }
}
