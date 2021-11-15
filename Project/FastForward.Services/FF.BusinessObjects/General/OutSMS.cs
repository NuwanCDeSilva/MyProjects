using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class OutSMS
    {
       //Written By Prabhath on 24/08/2012
        //Table - SMS_OUT

        #region Private Members
        private DateTime _createtime;
        private DateTime _deletedtime;
        private DateTime _deliveredtime;
        private DateTime _downloadtime;
        private string _msg;
        private string _msgid;
        private Int32 _msgstatus;
        private string _msgtype;
        private DateTime _receivedtime;
        private string _receiver;
        private string _receiverphno;
        private string _refdocno;
        private string _sender;
        private string _senderphno;
        private Int32 _seqno;
        private string _comcode;        //kapila 22/11/2016
        #endregion

        public DateTime Createtime { get { return _createtime; } set { _createtime = value; } }
        public DateTime Deletedtime { get { return _deletedtime; } set { _deletedtime = value; } }
        public DateTime Deliveredtime { get { return _deliveredtime; } set { _deliveredtime = value; } }
        public DateTime Downloadtime { get { return _downloadtime; } set { _downloadtime = value; } }
        public string Msg { get { return _msg; } set { _msg = value; } }
        public string Msgid { get { return _msgid; } set { _msgid = value; } }
        public Int32 Msgstatus { get { return _msgstatus; } set { _msgstatus = value; } }
        public string Msgtype { get { return _msgtype; } set { _msgtype = value; } }
        public DateTime Receivedtime { get { return _receivedtime; } set { _receivedtime = value; } }
        public string Receiver { get { return _receiver; } set { _receiver = value; } }
        public string Receiverphno { get { return _receiverphno; } set { _receiverphno = value; } }
        public string Refdocno { get { return _refdocno; } set { _refdocno = value; } }
        public string Sender { get { return _sender; } set { _sender = value; } }
        public string Senderphno { get { return _senderphno; } set { _senderphno = value; } }
        public Int32 Seqno { get { return _seqno; } set { _seqno = value; } }

        public string comcode { get { return _comcode; } set { _comcode = value; } }

        public static OutSMS Converter(DataRow row)
        {
            return new OutSMS
            {
                Createtime = row["CREATETIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["CREATETIME"]),
                Deletedtime = row["DELETEDTIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["DELETEDTIME"]),
                Deliveredtime = row["DELIVEREDTIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["DELIVEREDTIME"]),
                Downloadtime = row["DOWNLOADTIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["DOWNLOADTIME"]),
                Msg = row["MSG"] == DBNull.Value ? string.Empty : row["MSG"].ToString(),
                Msgid = row["MSGID"] == DBNull.Value ? string.Empty : row["MSGID"].ToString(),
                Msgstatus = row["MSGSTATUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["MSGSTATUS"]),
                Msgtype = row["MSGTYPE"] == DBNull.Value ? string.Empty : row["MSGTYPE"].ToString(),
                Receivedtime = row["RECEIVEDTIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RECEIVEDTIME"]),
                Receiver = row["RECEIVER"] == DBNull.Value ? string.Empty : row["RECEIVER"].ToString(),
                Receiverphno = row["RECEIVERPHNO"] == DBNull.Value ? string.Empty : row["RECEIVERPHNO"].ToString(),
                Refdocno = row["REFDOCNO"] == DBNull.Value ? string.Empty : row["REFDOCNO"].ToString(),
                Sender = row["SENDER"] == DBNull.Value ? string.Empty : row["SENDER"].ToString(),
                Senderphno = row["SENDERPHNO"] == DBNull.Value ? string.Empty : row["SENDERPHNO"].ToString(),
                Seqno = row["SEQNO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SEQNO"])

            };
        }
    }
}

