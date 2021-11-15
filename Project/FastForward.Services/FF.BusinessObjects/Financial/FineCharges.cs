using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects
{
    public class FineCharges 
    {
        public string comcode { get; set; } 
        public string pccode { get; set; }
        public DateTime finedate { get; set; }
        public string remarks { get; set; }
        public decimal amount { get; set; }
        public DateTime createdate { get; set; }
        public int ismailsend { get; set; }
        public int isdelete { get; set; }
        public string createby { get; set; }
        public string finecode { get; set; }
        public string rate { get; set; }
        public decimal balance { get; set; }
        public string status { get; set; }
        public string createsession { get; set; }
        public string modifysession { get; set; }
        public string modby { get; set; }
        public DateTime moddt { get; set; }
        public int seqno { get; set; }
        public int issetoff { get; set; }
        public string description { get; set; }


    }
}
