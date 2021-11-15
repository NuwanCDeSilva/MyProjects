using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Commission
{
  public  class ref_comm_target
    {
      public Int32 rct_seq { get; set; }
      public String rct_docno { get; set; }
      public decimal rct_st_val { get; set; }
      public decimal rct_end_val { get; set; }
      public decimal rct_rate { get; set; }
      public string rct_anal1 { get; set; }
      public string rct_anal2 { get; set; }
      public static ref_comm_target Converter(DataRow row)
      {
          return new ref_comm_target
          {
              rct_seq = row["rct_seq"] == DBNull.Value ? 0 : Convert.ToInt32(row["rct_seq"].ToString()),
              rct_docno = row["rct_docno"] == DBNull.Value ? string.Empty : row["rct_docno"].ToString(),
              rct_st_val = row["rct_st_val"] == DBNull.Value ? 0 : Convert.ToDecimal(row["rct_st_val"].ToString()),
              rct_end_val = row["rct_end_val"] == DBNull.Value ? 0 : Convert.ToDecimal(row["rct_end_val"].ToString()),
              rct_rate = row["rct_rate"] == DBNull.Value ? 0 : Convert.ToDecimal(row["rct_rate"].ToString()),
              rct_anal1 = row["rct_anal1"] == DBNull.Value ? string.Empty : row["rct_anal1"].ToString(),
              rct_anal2 = row["rct_anal2"] == DBNull.Value ? string.Empty : row["rct_anal2"].ToString(),
             
          };
      }
    }
}
