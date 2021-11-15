using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class InSMS
    {
       //Written By Prabhath on 24/08/2012
        //Table : SMS_IN

        #region Private Members
        private Boolean _isread;
        private Boolean _isreplied;
        private string _msg;
        private string _msgtype;
        private DateTime _readtime;
        private string _receiverphno;
        private DateTime _receivertime;
        private string _refno;
        private DateTime _repliedtime;
        private string _senderphno;
        private DateTime _sendertime;
        private Int32 _seqno;
        #endregion

        public Boolean Isread { get { return _isread; } set { _isread = value; } }
        public Boolean Isreplied { get { return _isreplied; } set { _isreplied = value; } }
        public string Msg { get { return _msg; } set { _msg = value; } }
        public string Msgtype { get { return _msgtype; } set { _msgtype = value; } }
        public DateTime Readtime { get { return _readtime; } set { _readtime = value; } }
        public string Receiverphno { get { return _receiverphno; } set { _receiverphno = value; } }
        public DateTime Receivertime { get { return _receivertime; } set { _receivertime = value; } }
        public string Refno { get { return _refno; } set { _refno = value; } }
        public DateTime Repliedtime { get { return _repliedtime; } set { _repliedtime = value; } }
        public string Senderphno { get { return _senderphno; } set { _senderphno = value; } }
        public DateTime Sendertime { get { return _sendertime; } set { _sendertime = value; } }
        public Int32 Seqno { get { return _seqno; } set { _seqno = value; } }

        public static InSMS Converter(DataRow row)
        {
            return new InSMS
            {
                Isread = row["ISREAD"] == DBNull.Value ? false : Convert.ToBoolean(row["ISREAD"]),
                Isreplied = row["ISREPLIED"] == DBNull.Value ? false : Convert.ToBoolean(row["ISREPLIED"]),
                Msg = row["MSG"] == DBNull.Value ? string.Empty : row["MSG"].ToString(),
                Msgtype = row["MSGTYPE"] == DBNull.Value ? string.Empty : row["MSGTYPE"].ToString(),
                Readtime = row["READTIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["READTIME"]),
                Receiverphno = row["RECEIVERPHNO"] == DBNull.Value ? string.Empty : row["RECEIVERPHNO"].ToString(),
                Receivertime = row["RECEIVERTIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RECEIVERTIME"]),
                Refno = row["REFNO"] == DBNull.Value ? string.Empty : row["REFNO"].ToString(),
                Repliedtime = row["REPLIEDTIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["REPLIEDTIME"]),
                Senderphno = row["SENDERPHNO"] == DBNull.Value ? string.Empty : row["SENDERPHNO"].ToString(),
                Sendertime = row["SENDERTIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SENDERTIME"]),
                Seqno = row["SEQNO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SEQNO"])

            };
        }
    }
}

