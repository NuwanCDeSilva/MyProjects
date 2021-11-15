using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
 

    public class BM_SALE_DETAILS
    {
        public string BMS_PC_CHNL { get; set; }
        public string NUMBER_OF_SALES { get; set; }
        public string SALE_VALUE { get; set; }

        public string BMS_PC_CHNL_DESC { get; set; }
        public static BM_SALE_DETAILS Converter(DataRow row)
        {
            return new BM_SALE_DETAILS
            {
                BMS_PC_CHNL = row["BMS_PC_CHNL"] == DBNull.Value ? string.Empty : row["BMS_PC_CHNL"].ToString(),
                NUMBER_OF_SALES = row["NUMBER_OF_SALES"] == DBNull.Value ? string.Empty : row["NUMBER_OF_SALES"].ToString(),
                SALE_VALUE = row["SALE_VALUE"] == DBNull.Value ? string.Empty : row["SALE_VALUE"].ToString(),
               
            };
        }
        public static BM_SALE_DETAILS ConverterSub(DataRow row)
        {
            return new BM_SALE_DETAILS
            {
                BMS_PC_CHNL = row["BMS_PC_CHNL"] == DBNull.Value ? string.Empty : row["BMS_PC_CHNL"].ToString(),
                NUMBER_OF_SALES = row["NUMBER_OF_SALES"] == DBNull.Value ? string.Empty : row["NUMBER_OF_SALES"].ToString(),
                SALE_VALUE = row["SALE_VALUE"] == DBNull.Value ? string.Empty : row["SALE_VALUE"].ToString(),
                BMS_PC_CHNL_DESC = row["BMS_PC_CHNL_DESC"] == DBNull.Value ? string.Empty : row["BMS_PC_CHNL_DESC"].ToString(),
            };
        }
    }
}
