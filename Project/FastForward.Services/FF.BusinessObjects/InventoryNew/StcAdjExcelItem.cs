using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
    public class StcAdjExcelItem
    {
        public string item_code { get; set; }
        public string uom { get; set; }
        public string qty { get; set; }
        public string status { get; set; }
        public decimal uomqty { get; set; }
    }
}
