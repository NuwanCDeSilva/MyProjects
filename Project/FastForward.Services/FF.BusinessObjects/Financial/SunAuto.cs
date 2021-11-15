using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Financial
{
   public class SunAuto
    {
        public string TransactionRef { get; set; }
        public string JournalType { get; set; }
        public string Period { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public string AccountCode { get; set; }
        public decimal BankBill { get; set; }
        public decimal Freight { get; set; }
        public decimal Insuarance { get; set; }
        public decimal Other { get; set; }
        public decimal Otherex { get; set; }
        public decimal Amount { get; set; }
        public string LC_VehicleNo { get; set; }
        public string DebtCrdt { get; set; }
        public string PC { get; set; }
        public string JournalSours { get; set; }
        public string CommonCat { get; set; }
        public decimal CommonVal { get; set; }
        public string Docno { get; set; }
        public string AccType { get; set; }
        public string Actgrnno { get; set; }
        public DateTime GrnDate { get; set; }
        public DateTime ETA { get; set; }
        public DateTime ETD { get; set; }
        public string DiliveryTerm { get; set; }

    }
}
