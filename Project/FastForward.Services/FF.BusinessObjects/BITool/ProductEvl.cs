using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
   public class ProductEvl
    {
       public decimal _price { get; set; }
       public decimal _cost { get; set; }
       public decimal _varServiceCharge { get; set; }
       public decimal _varInsAmount { get; set; }
       public decimal _varInterestAmt { get; set; }
       public decimal _inscomm { get; set; }
       public decimal _dbcommvalue { get; set; }
       public decimal _cashcommval { get; set; }
       public decimal _credcommval { get; set; }
       public decimal monthn { get; set; }
       public decimal _finrtf { get; set; }
       public decimal collectbons { get; set; }
       public decimal _cashcommrt { get; set; }
       public decimal _credcommrt { get; set; }
       public decimal ServiceIncomeRt { get; set; }
       public string intrsthprt { get; set; }
       public string DiriyaRt { get; set; }
       public string inscommrt { get; set; }
       public string dpcommrt { get; set; }
       public decimal _TAXVALLL { get; set; }
       public int errerstatus { get; set; }
       public int avalable { get; set; }
       public int Schemeval { get; set; }
       public int ApproxMonths { get; set; }
       public decimal SalesRate { get; set; }
       public decimal VatRt { get; set; }
    }
}
