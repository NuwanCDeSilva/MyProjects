using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects
{
    [Serializable]
    public class ScmMovHeader
    {
        //
        // Function             - SCM Movement Header
        // Function Wriiten By  - Chamal De Silva
        // Date                 - 20/07/2014
        // Table                - INV_MOVEMENT_HEADER
        //

        #region Private Members
        private string _year_seq_no;
        public string Year_seq_no { get { return _year_seq_no; } set { _year_seq_no = value; } }

        private string _loca;
        public string Loca { get { return _loca; } set { _loca = value; } }

        private string _doc_no;
        public string Doc_no { get { return _doc_no; } set { _doc_no = value; } }
        private int _year;

        public int Year { get { return _year; } set { _year = value; } }
        private string _doc_type;

        public string Doc_type { get { return _doc_type; } set { _doc_type = value; } }
        private string _other_doc_no;

        public string Other_doc_no { get { return _other_doc_no; } set { _other_doc_no = value; } }
        private DateTime _doc_date;

        public DateTime Doc_date { get { return _doc_date; } set { _doc_date = value; } }
        private string _other_loca;

        public string Other_loca
        {
            get { return _other_loca; }
            set { _other_loca = value; }
        }
        private string _entry_no;

        public string Entry_no
        {
            get { return _entry_no; }
            set { _entry_no = value; }
        }
        private string _doc_status;

        public string Doc_status
        {
            get { return _doc_status; }
            set { _doc_status = value; }
        }
        private string _app_by_1;

        public string App_by_1
        {
            get { return _app_by_1; }
            set { _app_by_1 = value; }
        }
        private string _app_by_2;

        public string App_by_2
        {
            get { return _app_by_2; }
            set { _app_by_2 = value; }
        }
        private string _app_by_3;

        public string App_by_3
        {
            get { return _app_by_3; }
            set { _app_by_3 = value; }
        }
        private string _user;

        public string User
        {
            get { return _user; }
            set { _user = value; }
        }
        private string _manual_ref_no;

        public string Manual_ref_no
        {
            get { return _manual_ref_no; }
            set { _manual_ref_no = value; }
        }
        private string _doc_sub_type;

        public string Doc_sub_type
        {
            get { return _doc_sub_type; }
            set { _doc_sub_type = value; }
        }
        private string _entry_type;

        public string Entry_type
        {
            get { return _entry_type; }
            set { _entry_type = value; }
        }
        private string _company;

        public string Company
        {
            get { return _company; }
            set { _company = value; }
        }
        private string _supplier_code;

        public string Supplier_code
        {
            get { return _supplier_code; }
            set { _supplier_code = value; }
        }
        private string _currency_code;

        public string Currency_code
        {
            get { return _currency_code; }
            set { _currency_code = value; }
        }
        private double _exchange_rate;

        public double Exchange_rate
        {
            get { return _exchange_rate; }
            set { _exchange_rate = value; }
        }
        private string _remarks;

        public string Remarks
        {
            get { return _remarks; }
            set { _remarks = value; }
        }
        private string _vehicle_no;

        public string Vehicle_no
        {
            get { return _vehicle_no; }
            set { _vehicle_no = value; }
        }
        private string _inv_direction;

        public string Inv_direction
        {
            get { return _inv_direction; }
            set { _inv_direction = value; }
        }
        private string _channel_code;

        public string Channel_code
        {
            get { return _channel_code; }
            set { _channel_code = value; }
        }
        private string _cost_profit_code;

        public string Cost_profit_code
        {
            get { return _cost_profit_code; }
            set { _cost_profit_code = value; }
        }
        private string _del_add1;

        public string Del_add1
        {
            get { return _del_add1; }
            set { _del_add1 = value; }
        }
        private string _del_add2;

        public string Del_add2
        {
            get { return _del_add2; }
            set { _del_add2 = value; }
        }
        #endregion

        #region Definition - Properties
        //public string Ith_acc_no { get { return _ith_acc_no; } set { _ith_acc_no = value; } }
        #endregion
    }
}
