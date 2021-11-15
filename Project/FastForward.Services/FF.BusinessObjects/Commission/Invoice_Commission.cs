using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Commission
{
  public  class Invoice_Commission
    {
      public string InvoiceNo { get; set; }
      public string ExecCode { get; set; }
      public string ExecName { get; set; }
      public string Item { get; set; }
      public Int32 Qty { get; set; }
      public decimal TotValue { get; set; }
      public decimal ItemCommission { get; set; }
      public decimal EmpCommission { get; set; }
      public decimal FinalCommission { get; set; }
      public decimal taxammount { get; set; }
      public decimal discountammount { get; set; }
      public string commissioncode { get; set; }
      public string ProfitCenter { get; set; }
      public Int32 isreversal { get; set; }
      public DateTime invoicedate { get; set; }
      public string empcode { get; set; }
      public string empcat { get; set; }
      public Int32 stdates { get; set; }
      public Int32 enddates { get; set; }
      public string ManagerCd { get; set; }
      public decimal ManagerCommission { get; set; }

      public decimal ItemCommissionRate { get; set; }
      public string Company { get; set; }

      public Int32 Settlementdates { get; set; }
      public decimal multypleval { get; set; }


    }
}
