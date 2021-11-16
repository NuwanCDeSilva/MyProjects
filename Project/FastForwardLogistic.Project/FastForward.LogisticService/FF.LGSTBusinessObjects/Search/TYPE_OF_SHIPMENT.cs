using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Search
{
    public class TYPE_OF_SHIPMENT
    {
        public string CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static TYPE_OF_SHIPMENT Converter(DataRow row)
        {
            return new TYPE_OF_SHIPMENT
            {
                CODE = row["CODE"] == DBNull.Value ? string.Empty : row["CODE"].ToString(),
                DESCRIPTION = row["DESCRIPTION"] == DBNull.Value ? string.Empty : row["DESCRIPTION"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString(),
            };
        }
    }
}
