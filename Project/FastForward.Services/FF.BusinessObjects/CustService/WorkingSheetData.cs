using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.CustService
{
 public   class WorkingSheetData
    {
        public Int32 No { get; set; }
        public string HScode { get; set; }
        public string HSDescription { get; set; }
        public string Country { get; set; }
        public decimal Quantity { get; set; }
        public string ItmDescription { get; set; }
        public decimal FOB { get; set; }
        public decimal Freight { get; set; }
        public decimal Insurance { get; set; }
        public decimal Other { get; set; }
        public decimal CIF { get; set; }
        public decimal NetMass { get; set; }
        public double GrossMass { get; set; }
        public decimal Packages { get; set; }
        public string MainHS { get; set; }
        public string Itemcode { get; set; }
        public string Cat1 { get; set; }
        public string Cat2 { get; set; }
        public string Model { get; set; }

    


 
    }
}
