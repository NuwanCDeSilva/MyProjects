using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
    public class Production_ref
    {
        public string code { get; set; }
        public string description { get; set; }
        public string charge { get; set; }
        public string itemCat1 { get; set; }
        public string itemCat2 { get; set; }
        public string itemCat3 { get; set; }           
        public string company { get; set; }
        public string createuser { get; set; }
        public string updateuser { get; set; }
        public int isactive { get; set; }
        public int intialsetup { get; set; } 
    }
}
