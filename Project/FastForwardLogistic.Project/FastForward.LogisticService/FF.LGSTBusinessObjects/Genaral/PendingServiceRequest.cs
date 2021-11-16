using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Genaral
{
  public  class PendingServiceRequest
    {
      public string rq_no { get; set; }
      public DateTime rq_dt { get; set; }
      public string rq_pouch_no { get; set; }
      public string rq_rmk { get; set; }
      public string rq_seq_no { get; set; }
      public string customer { get; set; }
      public string pc { get;set; }
      public static PendingServiceRequest Converter(DataRow row)
      {
          return new PendingServiceRequest
          {
              rq_no = row["rq_no"] == DBNull.Value ? string.Empty : row["rq_no"].ToString(),
              rq_dt = row["rq_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["rq_dt"].ToString()),
              rq_pouch_no = row["rq_pouch_no"] == DBNull.Value ? string.Empty : row["rq_pouch_no"].ToString(),
              rq_rmk = row["rq_rmk"] == DBNull.Value ? string.Empty : row["rq_rmk"].ToString(),
              rq_seq_no = row["rq_seq_no"] == DBNull.Value ? string.Empty : row["rq_seq_no"].ToString(),
          };
      }
    }
}
