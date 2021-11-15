using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects
{
    public class InsuItem
    {
        private string _item;
        private decimal _value;
        private string _serial;
        private string _promotion;
        private string _circuler;
        private string _cat1;
        private string _cat2;
        private string _brand;
        public string Item
        {
            get { return _item; }
            set { _item = value; }
        }
        public decimal Value
        {
            get { return _value; }
            set { _value = value; }
        }
        public string Serial
        {
            get { return _serial; }
            set { _serial = value; }
        }
        public string Promotion
        {
            get { return _promotion; }
            set { _promotion = value; }
        }
        public string Circuler
        {
            get { return _circuler; }
            set { _circuler = value; }
        }
        public string Cat1
        {
            get { return _cat1; }
            set { _cat1 = value; }
        }
        public string Cat2
        {
            get { return _cat2; }
            set { _cat2 = value; }
        }
        public string Brand
        {
            get { return _brand; }
            set { _brand = value; }
        }
    }
}
