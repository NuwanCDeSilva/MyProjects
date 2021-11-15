using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class MST_CHANNEL_SEARCH_HEAD
    {
        public string MPI_VAL { get; set; }
        public string MSC_DESC { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static MST_CHANNEL_SEARCH_HEAD Converter(DataRow row)
        {
            return new MST_CHANNEL_SEARCH_HEAD
            {
                MPI_VAL = row["MPI_VAL"] == DBNull.Value ? string.Empty : row["MPI_VAL"].ToString(),
                MSC_DESC = row["MSC_DESC"] == DBNull.Value ? string.Empty : row["MSC_DESC"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        } 
    }
}
