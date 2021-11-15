using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    public class ApprovalItemStatus
    {

         public string MIS_CD{get;set;}
         public string MIS_DESC { get; set; }
         public static ApprovalItemStatus Converter(DataRow row)
        {
            return new ApprovalItemStatus
            {
                MIS_CD = row["MIS_CD"] == DBNull.Value ? string.Empty : row["MIS_CD"].ToString(),
                MIS_DESC = row["MIS_DESC"] == DBNull.Value ? string.Empty : row["MIS_DESC"].ToString(),
            };
         }
              
    }
}
