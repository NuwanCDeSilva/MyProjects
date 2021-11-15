using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Tours
{
  public  class FleetAllocDaily
    {
      public string mfd_veh_no { get; set; }
      public DateTime mfd_frm_dt { get; set; }
      public DateTime mfd_to_dt { get; set; }

      public static FleetAllocDaily Convertor(DataRow row)
      {
          return new FleetAllocDaily
          {
              mfd_veh_no = row["mfd_veh_no"] == DBNull.Value ? string.Empty : row["mfd_veh_no"].ToString(),
              mfd_frm_dt = row["mfd_frm_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["mfd_frm_dt"].ToString()),
              mfd_to_dt = row["mfd_to_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["mfd_to_dt"].ToString()),
          };
      }
  }
}
