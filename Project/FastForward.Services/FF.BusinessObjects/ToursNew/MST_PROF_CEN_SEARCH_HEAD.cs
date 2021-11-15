using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class MST_PROF_CEN_SEARCH_HEAD
    {
        public string MPC_CD { get; set; }
        public string MPC_DESC { get; set; }
        public string RESULT_COUNT { get; set; }

        public Int32 mpc_ord_alpbt { get; set; }
        public string R__ { get; set; }

        public static MST_PROF_CEN_SEARCH_HEAD Converter(DataRow row)
        {
            return new MST_PROF_CEN_SEARCH_HEAD
            {

                MPC_CD = row["MPC_CD"] == DBNull.Value ? string.Empty : row["MPC_CD"].ToString(),
                MPC_DESC = row["MPC_DESC"] == DBNull.Value ? string.Empty : row["MPC_DESC"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                mpc_ord_alpbt = row["mpc_ord_alpbt"] == DBNull.Value ? 0 : Convert.ToInt32(row["mpc_ord_alpbt"].ToString()),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString(),
            };
        }
    }
}
