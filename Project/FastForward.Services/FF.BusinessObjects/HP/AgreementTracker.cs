using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    
    public class AgreementTracker
    {
        #region Private Members
        private string _prft_center;
       
        private string _doc_no;
        private string _pod_no;
        private Int32 _acc_no_from;
        private Int32 _acc_no_to;
        #endregion
        public string Prft_center
        {
            get { return _prft_center; }
            set { _prft_center = value; }
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
    }
}
