using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
  public class WarehseItemSetup
    {
        public string definitionType { get; set; }
        public string location { get; set; }
        public string itemCat1 { get; set; }
        public string itemCat2 { get; set; }
        public string itemCat3 { get; set; }
        public string brand { get; set; }
        public string itemStatus { get; set; }

        public string tostatus { get; set; }

       public int activeStatus { get; set; }
        public string company { get; set; }
        public string createuser { get; set; }
        public string updateuser { get; set; }
    }
}
