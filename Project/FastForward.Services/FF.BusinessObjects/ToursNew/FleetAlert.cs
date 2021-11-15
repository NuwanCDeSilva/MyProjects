using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
   public class FleetAlert
    {
       public string MSTF_REGNO { get; set; }
       public string MSTF_BRD { get; set; }
       public string MSTF_MODEL { get; set; }
       public string MSTF_VEH_TP { get; set; }
       public string MSTF_OWN_NM { get; set; }
       public string MSTF_OWN_CONT { get; set; }
       public DateTime EXPDATE { get; set; }
       public static FleetAlert Converter(DataRow row)
       {
           return new FleetAlert
           {
               MSTF_REGNO = row["MSTF_REGNO"] == DBNull.Value ? string.Empty : row["MSTF_REGNO"].ToString(),
               MSTF_BRD = row["MSTF_BRD"] == DBNull.Value ? string.Empty : row["MSTF_BRD"].ToString(),
               MSTF_MODEL = row["MSTF_MODEL"] == DBNull.Value ? string.Empty : row["MSTF_MODEL"].ToString(),
               MSTF_VEH_TP = row["MSTF_VEH_TP"] == DBNull.Value ? string.Empty : row["MSTF_VEH_TP"].ToString(),
               MSTF_OWN_NM = row["MSTF_OWN_NM"] == DBNull.Value ? string.Empty : row["MSTF_OWN_NM"].ToString(),
               MSTF_OWN_CONT = row["MSTF_OWN_CONT"] == DBNull.Value ? string.Empty : row["MSTF_OWN_CONT"].ToString(),
               EXPDATE = row["EXPDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["EXPDATE"].ToString()),
           };
       }
    }

}
