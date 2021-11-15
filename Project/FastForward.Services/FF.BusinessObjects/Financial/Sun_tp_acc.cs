using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Financial
{
 public   class Sun_tp_acc
    {
     public string gsva_acc_cd { get; set; }
     public static Sun_tp_acc Converter(DataRow row)
     {
         return new Sun_tp_acc
         {
             gsva_acc_cd = row["gsva_acc_cd"] == DBNull.Value ? string.Empty : row["gsva_acc_cd"].ToString()
         };
     }
    }
}
