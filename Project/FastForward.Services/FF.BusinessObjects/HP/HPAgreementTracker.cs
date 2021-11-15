using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{


    public class HPAgreementTracker
    {
        #region Private Members
        private string _prft_center;

        private string _doc_no;
        private string _pod_no;
        private Int32 _acc_no_from;
        private Int32 _acc_no_to;
        private DateTime _date_from;
        private string _acc_no;
        private string _insert_prft_cntr;
        private DateTime _insert_date;
        private string _insert_user_com;
        private string _process_type;
        private string _oth_close_typ;
        private string _remark;
        private DateTime _clsTypeDate;
        private DateTime _checkdate;
        private DateTime _date_to;
        private string _ischeck;


        
        
        
        
       
        
       
       
       
        
        #endregion
       
        public string Prft_center
        {
            get { return _prft_center; }
            set { _prft_center = value; }
        }
        public DateTime Date_from
        {
            get { return _date_from; }
            set { _date_from = value; }
        }
        public string Doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }
        public string Pod_no
        {
            get { return _pod_no; }
            set { _pod_no = value; }
        }
        public Int32 Acc_no_from
        {
            get { return _acc_no_from; }
            set { _acc_no_from = value; }
        }
        public Int32 Acc_no_to
        {
            get { return _acc_no_to; }
            set { _acc_no_to = value; }
        }
        public string Acc_no
        {
            get { return _acc_no; }
            set { _acc_no = value; }
        }
        public string Insert_prft_cntr
        {
            get { return _insert_prft_cntr; }
            set { _insert_prft_cntr = value; }
        }
        public DateTime Insert_date
        {
            get { return _insert_date; }
            set { _insert_date = value; }
        }
        public string Insert_user_com
        {
            get { return _insert_user_com; }
            set { _insert_user_com = value; }
        }
        public string Process_type
        {
            get { return _process_type; }
            set { _process_type = value; }
        }
        
        public string Oth_close_typ
        {
            get { return _oth_close_typ; }
            set { _oth_close_typ = value; }
        }
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        public DateTime ClsTypeDate
        {
            get { return _clsTypeDate; }
            set { _clsTypeDate = value; }
        }
        public DateTime Checkdate
        {
            get { return _checkdate; }
            set { _checkdate = value; }
        }
        public DateTime Date_to
        {
            get { return _date_to; }
            set { _date_to = value; }
        }
        public string Ischeck
        {
            get { return _ischeck; }
            set { _ischeck = value; }
        }
    }
}
