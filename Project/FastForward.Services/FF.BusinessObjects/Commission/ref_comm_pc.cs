using System;
using System.Data;

namespace FF.BusinessObjects
{
    public class ref_comm_pc
    {
        public Int32 Rcp_seq { get; set; }
        public String Rcp_comm_cd { get; set; }
        public String Rcp_pc { get; set; }
        public String Rcp_anal1 { get; set; }
        public String Rcp_anal2  { get; set; }

        public static ref_comm_pc Converter(DataRow row)
        {
            return new ref_comm_pc
            {
                Rcp_seq = row["RCP_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["RCP_SEQ"].ToString()),
                Rcp_comm_cd = row["RCP_COMM_CD"] == DBNull.Value ? string.Empty : row["RCP_COMM_CD"].ToString(),
                Rcp_pc = row["RCP_PC"] == DBNull.Value ? string.Empty : row["RCP_PC"].ToString(),
                Rcp_anal1 = row["RCP_ANAL1"] == DBNull.Value ? string.Empty : row["RCP_ANAL1"].ToString(),
                Rcp_anal2 = row["RCP_ANAL2"] == DBNull.Value ? string.Empty : row["RCP_ANAL2"].ToString()
            };
        }
    }
}

