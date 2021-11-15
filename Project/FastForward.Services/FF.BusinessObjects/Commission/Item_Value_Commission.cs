using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Commission
{
  public  class Item_Value_Commission
    {
      public string ItemCode { get; set; }
      public string ItemModel { get; set; }
      public string ItemBrand { get; set; }
      public string Cat1 { get; set; }
      public string Cat2 { get; set; }
      public decimal MaxValue { get; set; }
      public decimal MinValue { get; set; }
      public decimal Commission { get; set; }
      public string Range { get; set; }
      public Int32 StlmntStDays { get; set; }
      public Int32 StlmntEndDays { get; set; }
      public string SettlRange { get; set; }
      public string InvType { get; set; }
      public string Cat3 { get; set; }
      public string Btu1 { get; set; }
      public string Btu2 { get; set; }
    }
}
