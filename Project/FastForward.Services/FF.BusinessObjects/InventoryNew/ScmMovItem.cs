using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects
{
    public class ScmMovItem
    {
        private string _year_seq_no;

        public string Year_seq_no
        {
            get { return _year_seq_no; }
            set { _year_seq_no = value; }
        } 
        
        private string _loca;

        public string Loca
        {
            get { return _loca; }
            set { _loca = value; }
        } 
        
        private string _doc_no;

        public string Doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        } 
        
        private int _line;

        public int Line
        {
            get { return _line; }
            set { _line = value; }
        } private string _item_code;

        public string Item_code
        {
            get { return _item_code; }
            set { _item_code = value; }
        } 
        
        private string _item_status;

        public string Item_status
        {
            get { return _item_status; }
            set { _item_status = value; }
        } 
        
        private decimal _qty;

        public decimal Qty
        {
            get { return _qty; }
            set { _qty = value; }
        } 
        
        private string _uom;

        public string Uom
        {
            get { return _uom; }
            set { _uom = value; }
        } 
        
        private string _user;

        public string User
        {
            get { return _user; }
            set { _user = value; }
        } 
        
        private decimal _unit_price;

        public decimal Unit_price
        {
            get { return _unit_price; }
            set { _unit_price = value; }
        } 
        
        private string _cate1;

        public string Cate1
        {
            get { return _cate1; }
            set { _cate1 = value; }
        } 
        
        private string _cate2;

        public string Cate2
        {
            get { return _cate2; }
            set { _cate2 = value; }
        } 
        
        private string _cate3;

        public string Cate3
        {
            get { return _cate3; }
            set { _cate3 = value; }
        } private string _brand;

        public string Brand
        {
            get { return _brand; }
            set { _brand = value; }
        } 
        
        private string _model;

        public string Model
        {
            get { return _model; }
            set { _model = value; }
        }

    }
}
