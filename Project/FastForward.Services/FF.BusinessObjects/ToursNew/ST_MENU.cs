using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class ST_MENU
    {
        public Int32 MNU_ID{get;set;}
        public string MNU_LABEL{get;set;}
        public string MNU_CONTRL { get; set; }
        public string MNU_ACTN { get; set; }
        public Int32 MNU_PARENT_ID{get;set;}
        public string MNU_CRE_BY{get;set;}
        public DateTime MNU_CRE_WHEN{get;set;}
        public string MNU_MOD_BY{get;set;}
        public DateTime MNU_MOD_WHEN{get;set;}
        public Int32 MNU_ACT { get; set; }
        public static ST_MENU Converter(DataRow row)
        {
            return new ST_MENU
            {

                MNU_ID = row["MNU_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["MNU_ID"].ToString()),
                MNU_LABEL = row["MNU_LABEL"] == DBNull.Value ? string.Empty : row["MNU_LABEL"].ToString(),
                MNU_CONTRL = row["MNU_CONTRL"] == DBNull.Value ? string.Empty : row["MNU_CONTRL"].ToString(),
                MNU_ACTN = row["MNU_ACTN"] == DBNull.Value ? string.Empty : row["MNU_ACTN"].ToString(),
                MNU_PARENT_ID = row["MNU_PARENT_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["MNU_PARENT_ID"].ToString()),
                MNU_CRE_BY = row["MNU_CRE_BY"] == DBNull.Value ? string.Empty : row["MNU_CRE_BY"].ToString(),
                MNU_CRE_WHEN = row["MNU_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MNU_CRE_WHEN"].ToString()),
                MNU_MOD_BY = row["MNU_MOD_BY"] == DBNull.Value ? string.Empty : row["MNU_MOD_BY"].ToString(),
                MNU_MOD_WHEN = row["MNU_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MNU_MOD_WHEN"].ToString()),
                MNU_ACT = row["MNU_ACT"] == DBNull.Value ? 0 :Convert.ToInt32(row["MNU_ACT"].ToString())
                
            };
        }
    }
}
