using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
    public class ItemStatus_Change_def
    {
        //public int active { get; set; }
        public string mainCategory { get; set; }
        public string category { get; set; }
        public string subCategory { get; set; }
        public string brand { get; set; }
        public string model { get; set; }
        public string item { get; set; }
        public string pttype { get; set; }
        public string hierarchy { get; set; }

        public string Fromstatus { get; set; } //From Status // Added by Udesh 22-Oct-2018
        public string status { get; set; } //To Status
        public string alowQuantity { get; set; }       
        public string alowItemQuantity { get; set; }

        public DateTime FromDate { get; set; }      // Added by Udesh 22-Oct-2018
        public DateTime ToDate { get; set; }        // Added by Udesh 22-Oct-2018


        public string company { get; set; }
        public string createuser { get; set; }
        public string updateuser { get; set; }
    
    }
}
