using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Search
{
    public class MST_PC_SEARCH
    {
        public String Mpc_cd { get; set; }
        public String Mpc_desc { get; set; }
        public String Mpc_add_1 { get; set; }
        public String Mpc_chnl { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static MST_PC_SEARCH Converter(DataRow row)
        {
            return new MST_PC_SEARCH
            {
                Mpc_cd = row["MPC_CD"] == DBNull.Value ? string.Empty : row["MPC_CD"].ToString(),
                Mpc_desc = row["MPC_DESC"] == DBNull.Value ? string.Empty : row["MPC_DESC"].ToString(),
                Mpc_add_1 = row["MPC_ADD_1"] == DBNull.Value ? string.Empty : row["MPC_ADD_1"].ToString(),
                Mpc_chnl = row["MPC_CHNL"] == DBNull.Value ? string.Empty : row["MPC_CHNL"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()

            };
        }
    }
    public class MST_PC_SELECTED
    {
        public string Mpc_cd { get; set; }
        public string Mpc_desc { get; set; }
    }
}
