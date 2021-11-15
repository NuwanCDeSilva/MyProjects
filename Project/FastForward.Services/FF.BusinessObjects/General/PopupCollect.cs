using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class PopupCollect
    {
        #region Private Members
        private string _channel;
        private string _company;
        private string _create_user;
        private DateTime _create_when;
        private DateTime _cur_date;
        private string _direction;
        private string _doc_no;
        private string _doc_status;
        private string _doc_type;
        private Boolean _isvisible;
        private string _location;
        private string _message;
        private string _modify_by;
        private DateTime _modify_when;
        private string _other_doc_no;
        private string _receiver;
        private string _remarks;
        private string _sender;

        #endregion

        public string Channel
        {
            get { return _channel; }
            set { _channel = value; }
        }
        public string Company
        {
            get { return _company; }
            set { _company = value; }
        }
        public string Create_user
        {
            get { return _create_user; }
            set { _create_user = value; }
        }
        public DateTime Create_when
        {
            get { return _create_when; }
            set { _create_when = value; }
        }
        public DateTime Cur_date
        {
            get { return _cur_date; }
            set { _cur_date = value; }
        }
        public string Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }
        public string Doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }
        public string Doc_status
        {
            get { return _doc_status; }
            set { _doc_status = value; }
        }
        public string Doc_type
        {
            get { return _doc_type; }
            set { _doc_type = value; }
        }
        public Boolean Isvisible
        {
            get { return _isvisible; }
            set { _isvisible = value; }
        }
        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
        public string Modify_by
        {
            get { return _modify_by; }
            set { _modify_by = value; }
        }
        public DateTime Modify_when
        {
            get { return _modify_when; }
            set { _modify_when = value; }
        }
        public string Other_doc_no
        {
            get { return _other_doc_no; }
            set { _other_doc_no = value; }
        }
        public string Receiver
        {
            get { return _receiver; }
            set { _receiver = value; }
        }
        public string Remarks
        {
            get { return _remarks; }
            set { _remarks = value; }
        }
        public string Sender
        {
            get { return _sender; }
            set { _sender = value; }
        }

        public static PopupCollect Converter(DataRow row)
        {
            return new PopupCollect
         {
             Channel = row["CHANNEL"] == DBNull.Value ? string.Empty : row["CHANNEL"].ToString(),
             Company = row["COMPANY"] == DBNull.Value ? string.Empty : row["COMPANY"].ToString(),
             Create_user = row["CREATE_USER"] == DBNull.Value ? string.Empty : row["CREATE_USER"].ToString(),
             Create_when = row["CREATE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["CREATE_WHEN"]),
             Cur_date = row["CUR_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["CUR_DATE"]),
             Direction = row["DIRECTION"] == DBNull.Value ? string.Empty : row["DIRECTION"].ToString(),
             Doc_no = row["DOC_NO"] == DBNull.Value ? string.Empty : row["DOC_NO"].ToString(),
             Doc_status = row["DOC_STATUS"] == DBNull.Value ? string.Empty : row["DOC_STATUS"].ToString(),
             Doc_type = row["DOC_TYPE"] == DBNull.Value ? string.Empty : row["DOC_TYPE"].ToString(),
             Isvisible = row["ISVISIBLE"] == DBNull.Value ? false : Convert.ToBoolean(row["ISVISIBLE"]),
             Location = row["LOCATION"] == DBNull.Value ? string.Empty : row["LOCATION"].ToString(),
             Message = row["MESSAGE"] == DBNull.Value ? string.Empty : row["MESSAGE"].ToString(),
             Modify_by = row["MODIFY_BY"] == DBNull.Value ? string.Empty : row["MODIFY_BY"].ToString(),
             Modify_when = row["MODIFY_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MODIFY_WHEN"]),
             Other_doc_no = row["OTHER_DOC_NO"] == DBNull.Value ? string.Empty : row["OTHER_DOC_NO"].ToString(),
             Receiver = row["RECEIVER"] == DBNull.Value ? string.Empty : row["RECEIVER"].ToString(),
             Remarks = row["REMARKS"] == DBNull.Value ? string.Empty : row["REMARKS"].ToString(),
             Sender = row["SENDER"] == DBNull.Value ? string.Empty : row["SENDER"].ToString()

         };
        }
    }
}
