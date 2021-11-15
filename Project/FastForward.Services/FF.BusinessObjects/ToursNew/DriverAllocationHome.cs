using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
  public  class DriverAllocationHome
    {
      public string memp_first_name { get; set; }
      public string memp_last_name { get; set; }
      public string gce_enq_id { get; set; }
      public string mstf_regno { get; set; }
      public string mstf_model { get; set; }
      public string mstf_veh_tp { get; set; }
      public DateTime mfd_frm_dt { get; set; }
      public DateTime mfd_to_dt { get; set; }
      public Int32 RESULT_COUNT { get; set; }

      public static DriverAllocationHome Converter(DataRow row)
      {
          return new DriverAllocationHome
          {
              memp_first_name = row["memp_first_name"] == DBNull.Value ? string.Empty : row["memp_first_name"].ToString(),
              memp_last_name = row["memp_last_name"] == DBNull.Value ? string.Empty : row["memp_last_name"].ToString(),
              gce_enq_id = row["gce_enq_id"] == DBNull.Value ? string.Empty : row["gce_enq_id"].ToString(),
              mstf_regno = row["mstf_regno"] == DBNull.Value ? string.Empty : row["mstf_regno"].ToString(),
              mstf_model = row["mstf_model"] == DBNull.Value ? string.Empty : row["mstf_model"].ToString(),
              mstf_veh_tp = row["mstf_veh_tp"] == DBNull.Value ? string.Empty : row["mstf_veh_tp"].ToString(),
              mfd_frm_dt = row["mfd_frm_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["mfd_frm_dt"].ToString()),
              mfd_to_dt = row["mfd_to_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["mfd_to_dt"].ToString()),
              RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? 0 : Convert.ToInt32(row["RESULT_COUNT"].ToString()),
          };
      }
    }
}
