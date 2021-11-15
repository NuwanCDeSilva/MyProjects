﻿using System;
using System.Data;

namespace FF.BusinessObjects.General
{

    //===========================================================================================================
    // This code is generated by Code gen V.1 
    // All rights reserved.
    // Suneththaraka02@gmail.com 
    // Computer :- ITPD12  | User :- pemil On 21-Jul-2015 11:13:03
    //===========================================================================================================

    public class SMS_OUT
    {
        public Int32 SEQNO { get; set; }
        public String SENDER { get; set; }
        public String SENDERPHNO { get; set; }
        public String RECEIVER { get; set; }
        public String RECEIVERPHNO { get; set; }
        public String MSG { get; set; }
        public String MSGID { get; set; }
        public Int32 MSGSTATUS { get; set; }
        public String MSGTYPE { get; set; }
        public String REFDOCNO { get; set; }
        public DateTime CREATETIME { get; set; }
        public DateTime DOWNLOADTIME { get; set; }
        public DateTime RECEIVEDTIME { get; set; }
        public DateTime DELETEDTIME { get; set; }
        public DateTime DELIVEREDTIME { get; set; }
        public String COMCODE { get; set; }
        public Int32 CATEID { get; set; }
        public static SMS_OUT Converter(DataRow row)
        {
            return new SMS_OUT
            {
                SEQNO = row["SEQNO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SEQNO"].ToString()),
                SENDER = row["SENDER"] == DBNull.Value ? string.Empty : row["SENDER"].ToString(),
                SENDERPHNO = row["SENDERPHNO"] == DBNull.Value ? string.Empty : row["SENDERPHNO"].ToString(),
                RECEIVER = row["RECEIVER"] == DBNull.Value ? string.Empty : row["RECEIVER"].ToString(),
                RECEIVERPHNO = row["RECEIVERPHNO"] == DBNull.Value ? string.Empty : row["RECEIVERPHNO"].ToString(),
                MSG = row["MSG"] == DBNull.Value ? string.Empty : row["MSG"].ToString(),
                MSGID = row["MSGID"] == DBNull.Value ? string.Empty : row["MSGID"].ToString(),
                MSGSTATUS = row["MSGSTATUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["MSGSTATUS"].ToString()),
                MSGTYPE = row["MSGTYPE"] == DBNull.Value ? string.Empty : row["MSGTYPE"].ToString(),
                REFDOCNO = row["REFDOCNO"] == DBNull.Value ? string.Empty : row["REFDOCNO"].ToString(),
                CREATETIME = row["CREATETIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["CREATETIME"].ToString()),
                DOWNLOADTIME = row["DOWNLOADTIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["DOWNLOADTIME"].ToString()),
                RECEIVEDTIME = row["RECEIVEDTIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RECEIVEDTIME"].ToString()),
                DELETEDTIME = row["DELETEDTIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["DELETEDTIME"].ToString()),
                DELIVEREDTIME = row["DELIVEREDTIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["DELIVEREDTIME"].ToString()),
                COMCODE = row["COMCODE"] == DBNull.Value ? string.Empty : row["COMCODE"].ToString(),
                CATEID = row["CATEID"] == DBNull.Value ? 0 : Convert.ToInt32(row["CATEID"].ToString())
            };
        }
    }
}

