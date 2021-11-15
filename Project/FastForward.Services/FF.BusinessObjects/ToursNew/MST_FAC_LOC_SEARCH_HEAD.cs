using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class MST_FAC_LOC_SEARCH_HEAD
    {
        public string FAC_COM { get; set; }
        public string FAC_PC { get; set; }
        public string FAC_CODE { get; set; }
        public string FAC_DESC { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static MST_FAC_LOC_SEARCH_HEAD Converter(DataRow row)
        {
            return new MST_FAC_LOC_SEARCH_HEAD
            {

                FAC_COM = row["FAC_COM"] == DBNull.Value ? string.Empty : row["FAC_COM"].ToString(),
                FAC_PC = row["FAC_PC"] == DBNull.Value ? string.Empty : row["FAC_PC"].ToString(),
                FAC_CODE = row["FAC_CODE"] == DBNull.Value ? string.Empty : row["FAC_CODE"].ToString(),
                FAC_DESC = row["FAC_DESC"] == DBNull.Value ? string.Empty : row["FAC_DESC"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString(),
            };
        }
    }
}
