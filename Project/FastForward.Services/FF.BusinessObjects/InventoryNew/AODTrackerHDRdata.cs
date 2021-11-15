using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
 public   class AODTrackerHDRdata
    {
        public string CurrentLoc { get; set; }
        public string OtherLoc { get; set; }
        public string DocNo { get; set; }
        public string ManualRef { get; set; }
        public string Remarks { get; set; }
        public DateTime DocDate { get; set; }
        public string ith_stus { get; set; }
        public string User { get; set; }
        public DateTime CreDate { get; set; }



         public static AODTrackerHDRdata Converter(DataRow row)
        {
            return new AODTrackerHDRdata
            {
                CurrentLoc = row["CurrentLoc"] == DBNull.Value ? string.Empty : row["CurrentLoc"].ToString(),
                OtherLoc = row["OtherLoc"] == DBNull.Value ? string.Empty : row["OtherLoc"].ToString(),
                DocNo = row["DocNo"] == DBNull.Value ? string.Empty : row["DocNo"].ToString(),
                ManualRef = row["ManualRef"] == DBNull.Value ? string.Empty : row["ManualRef"].ToString(),
                Remarks = row["Remarks"] == DBNull.Value ? string.Empty : row["Remarks"].ToString(),
                DocDate = row["DocDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["DocDate"].ToString()),
                ith_stus = row["ith_stus"] == DBNull.Value ? string.Empty : row["ith_stus"].ToString(),
                User = row["User"] == DBNull.Value ? string.Empty : row["User"].ToString(),
                CreDate = row["CreDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["CreDate"].ToString()),
            };
         }

    }
}
