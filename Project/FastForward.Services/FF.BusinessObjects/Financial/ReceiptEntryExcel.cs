using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Financial
{
    [Serializable]
    public class ReceiptEntryExcel
    {
        private decimal _amount;
        private string _invoice;
        private string _receiptType;

        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public string Invoice
        {
            get { return _invoice; }
            set { _invoice = value; }
        }
        public string ReceiptType
        {
            get { return _receiptType; }
            set { _receiptType = value; }
        }
    }
}
