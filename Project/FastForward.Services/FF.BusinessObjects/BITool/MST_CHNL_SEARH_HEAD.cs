using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class MST_CHNL_SEARH_HEAD
    {
        public string MSC_CD { get; set; }
        public string MSC_DESC { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public Int32 SELECT { get; set; }
        public static MST_CHNL_SEARH_HEAD Converter(DataRow row)
        {
            return new MST_CHNL_SEARH_HEAD
            {
                MSC_CD = row["MSC_CD"] == DBNull.Value ? string.Empty : row["MSC_CD"].ToString(),
                MSC_DESC = row["MSC_DESC"] == DBNull.Value ? string.Empty : row["MSC_DESC"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        } 
    }
    public class SELECTED_CHNL
    {
        public string MSC_CD { get; set; }
    }
    public class MST_HIC_SEARH_HEAD
    {
        public string MSC_CD { get; set; }
        public string MSC_DESC { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public Int32 SELECT { get; set; }
        public static MST_HIC_SEARH_HEAD Converter(DataRow row)
        {
            return new MST_HIC_SEARH_HEAD
            {
                MSC_CD = row["MSC_CD"] == DBNull.Value ? string.Empty : row["MSC_CD"].ToString(),
                MSC_DESC = row["MSC_DESC"] == DBNull.Value ? string.Empty : row["MSC_DESC"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }
    public class SELECTED_SUBCHNL
    {
        public string MSC_CD { get; set; }
    }
    public class SELECTED_AREA
    {
        public string MSC_CD { get; set; }
    }
    public class SELECTED_REGION
    {
        public string MSC_CD { get; set; }
    }
    public class SELECTED_ZONE
    {
        public string MSC_CD { get; set; }
    }
    public class SELECTED_PC
    {
        public string MSC_CD { get; set; }
    }
}
