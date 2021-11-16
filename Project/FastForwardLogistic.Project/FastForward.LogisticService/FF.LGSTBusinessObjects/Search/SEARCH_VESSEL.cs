using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Search
{
   public class SEARCH_VESSEL
    {
       public string VM_VESSAL_CD { get; set; }
       public string VM_VESSAL_NAME { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static SEARCH_VESSEL Converter(DataRow row)
        {
            return new SEARCH_VESSEL
            {
                VM_VESSAL_CD = row["VM_VESSAL_CD"] == DBNull.Value ? string.Empty : row["VM_VESSAL_CD"].ToString(),
                VM_VESSAL_NAME = row["VM_VESSAL_NAME"] == DBNull.Value ? string.Empty : row["VM_VESSAL_NAME"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString(),
            };
        }
    }
}
