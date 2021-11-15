using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class TARGET_ALIGNMENT
    {
        public string cateCode { get; set; }
        public string hiarachy { get; set; }
        public decimal targetSale { get; set; }
        public decimal targetGP { get; set; }
        public decimal targetQuantity { get; set; }
        public decimal targerGPPer { get; set; }
        public decimal pySale { get; set; }
        public decimal pyGP { get; set; }
        public decimal pyQuantity { get; set; }
        public decimal pyGPPer { get; set; }
        public decimal actualSales { get; set; }
        public decimal actualGP { get; set; }
        public decimal actualQuantity { get; set; }
        public decimal actualGPPer { get; set; }
        public decimal vsTargetSale { get; set; }
        public decimal vsTargetGP { get; set; }
        public decimal vsTargetQuantity { get; set; }
        public decimal vsTargetGPPer { get; set; }
        public decimal vsPySale { get; set; }
        public decimal vsPyGP { get; set; }
        public decimal vsPyQuantity { get; set; }
        public decimal vsPyGPPer { get; set; }
    }
}
