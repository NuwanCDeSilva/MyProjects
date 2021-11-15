using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    public class ApproveItemDetail
    {
        public string MI_SHORTDESC {get;set;}
        public string MI_BRAND {get;set;}
        public string MI_MODEL { get; set; }
        public string MI_STATUS_DESC { get; set; }
        public string MI_IN_SER { get; set; }
        public static ApproveItemDetail Converter(DataRow row)
        {
            return new ApproveItemDetail
            {
                MI_SHORTDESC = row["MI_SHORTDESC"] == DBNull.Value ? string.Empty : row["MI_SHORTDESC"].ToString(),
                MI_BRAND = row["MI_BRAND"] == DBNull.Value ? string.Empty : row["MI_BRAND"].ToString(),
                MI_MODEL = row["MI_MODEL"] == DBNull.Value ? string.Empty : row["MI_MODEL"].ToString()
            };
        }
    }
}
