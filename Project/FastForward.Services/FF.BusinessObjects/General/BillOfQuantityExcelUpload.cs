using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    public class BillOfQuantityExcelUpload
    {
        public Int32 Line { get; set; }
        public decimal Qty { get; set; }
        public string Itm_cd { get; set; }
        public string Uom { get; set; }
        public string KitCode { get; set; }
        public string MrnBal { get; set; }
        public string Model { get; set; }
        public decimal Unit_price { get; set; }
        public string Tmp_err { get; set; }
        public string Tmp_err_text { get; set; }

        public string Desctription { get; set; }
    }
}
