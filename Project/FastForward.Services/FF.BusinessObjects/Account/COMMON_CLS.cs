using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Account
{
    public class COMMON_CLS
    {

    }

    public class ACCOUNT_DETAILS
    {
        public string ACC_NO { get; set; }
        public decimal ACC_AMOUNT { get; set; }
        public Int32 STUS { get; set; }
        public Int32 UPDATED { get; set; }
        public Int32 NEW_ADDED { get; set; }
    }
}
