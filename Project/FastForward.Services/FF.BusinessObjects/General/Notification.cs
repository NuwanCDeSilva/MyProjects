using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects.General
{
   public class Notification
    {
       public string Discription { get; set; }
       public string Value { get; set; }
       public int NotificationType { get; set; }

       public string Text1 { get; set; }
       public string Value1 { get; set; }
       public string Text2 { get; set; }
       public string Value2 { get; set; }

    }

   public class Thoughts
   {
       public int RDT_SEQ { get; set; }
       public string RDT_COL1 { get; set; }
       public string RDT_COL2 { get; set; }
       public string RDT_COL3 { get; set; }
       public string RDT_COL4 { get; set; }
       public string RDT_COL5 { get; set; }
       public int RDT_IS_PERIOD { get; set; }
       public DateTime RDT_FROM { get; set; }
       public DateTime RDT_TO { get; set; }
       public int RDT_IS_READ { get; set; }
       public int RDT_ACT { get; set; }
       public DateTime RDT_UPDATE_DT { get; set; }

       public static Thoughts Converter(DataRow row)
       {
           return new Thoughts
           {
               RDT_SEQ = row["RDT_SEQ"] == DBNull.Value ? -99 : Convert.ToInt16(row["RDT_SEQ"].ToString()),
               RDT_COL1 = row["RDT_COL1"] == DBNull.Value ? string.Empty : row["RDT_COL1"].ToString(),
               RDT_COL2 = row["RDT_COL2"] == DBNull.Value ? string.Empty : row["RDT_COL2"].ToString(),
               RDT_COL3 = row["RDT_COL3"] == DBNull.Value ? string.Empty : row["RDT_COL3"].ToString(),
               RDT_COL4 = row["RDT_COL4"] == DBNull.Value ? string.Empty : row["RDT_COL4"].ToString(),
               RDT_COL5 = row["RDT_COL5"] == DBNull.Value ? string.Empty : row["RDT_COL5"].ToString(),
               RDT_IS_PERIOD = row["RDT_IS_PERIOD"] == DBNull.Value ? -99 : Convert.ToInt16(row["RDT_IS_PERIOD"].ToString()),
               RDT_FROM = row["RDT_FROM"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(row["RDT_FROM"].ToString()),
               RDT_TO = row["RDT_TO"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(row["RDT_TO"].ToString()),
               RDT_IS_READ = row["RDT_IS_READ"] == DBNull.Value ? -99 : Convert.ToInt16(row["RDT_IS_READ"].ToString()),
               RDT_ACT = row["RDT_ACT"] == DBNull.Value ? -99 : Convert.ToInt16(row["RDT_ACT"].ToString()),
               RDT_UPDATE_DT = row["RDT_UPDATE_DT"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(row["RDT_UPDATE_DT"].ToString())
           };
       }
   }


}
