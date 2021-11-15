using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects
{
    public class ScmMovSerial
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
        private DateTime _docdate;

        public DateTime Docdate
        {
            get { return _docdate; }
            set { _docdate = value; }
        }
        private string _bin;

        public string Bin
        {
            get { return _bin; }
            set { _bin = value; }
        }
        private int _mcostlineno;

        public int Mcostlineno
        {
            get { return _mcostlineno; }
            set { _mcostlineno = value; }
        }
        private string _item_code;

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
        private int _serialseqno;

        public int Serialseqno
        {
            get { return _serialseqno; }
            set { _serialseqno = value; }
        }
        private string _serno;

        public string Serno
        {
            get { return _serno; }
            set { _serno = value; }
        }
        private string _chassisno;

        public string Chassisno
        {
            get { return _chassisno; }
            set { _chassisno = value; }
        }
        private string _warrno;

        public string Warrno
        {
            get { return _warrno; }
            set { _warrno = value; }
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
        private decimal _unitcost;

        public decimal Unitcost
        {
            get { return _unitcost; }
            set { _unitcost = value; }
        }
        private string _docrefno;

        public string Docrefno
        {
            get { return _docrefno; }
            set { _docrefno = value; }
        }
        private int _updatelineno;

        public int Updatelineno
        {
            get { return _updatelineno; }
            set { _updatelineno = value; }
        }
        private DateTime _comindate;

        public DateTime Comindate
        {
            get { return _comindate; }
            set { _comindate = value; }
        }
        private string _cominno;

        public string Cominno
        {
            get { return _cominno; }
            set { _cominno = value; }
        }
        private string _mfc;

        public string Mfc
        {
            get { return _mfc; }
            set { _mfc = value; }
        }
        private string _grna;

        public string Grna
        {
            get { return _grna; }
            set { _grna = value; }
        }
        private int _doclineno;

        public int Doclineno
        {
            get { return _doclineno; }
            set { _doclineno = value; }
        }
    }
}
