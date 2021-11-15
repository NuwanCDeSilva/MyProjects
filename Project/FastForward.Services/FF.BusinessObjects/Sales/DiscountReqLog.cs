using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Sales
{
    [Serializable]
    public class DiscountReqLog
    {
        public Int32 LDQ_SQE { get; set; }
        public Int32 REF_SEQ { get; set; }
        public string REF_ITM { get; set; }
        public string REF_REQ_REF { get; set; }
        public decimal REF_DISC_RT { get; set; }
        public decimal REF_DISC_VALUE { get; set; }
        public string REF_CRE_BY { get; set; }
        public DateTime REF_CRE_DT { get; set; }
        public string REF_MOD_BY { get; set; }
        public DateTime REF_MOD_DT { get; set; }
        public Int16 REF_STATUS { get; set; }
    }
}
