using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Net.Mail;
using EMS_Upload_Console;
using FF.BusinessObjects;
using Oracle.DataAccess.Client;
using UtilityClasses;
using System.Data.SqlClient;

namespace Hero_Service_Consol
{
    class Send_SMS_Wellawatte : Conn
    {
        string _sql;

        public void Send_SMS_Wellawatte1(string _fromCom, string _toCom, Int16 _db, string _team)
        {
            try
            {
                Console.WriteLine("");

                //OpenSql();
                    SqlBegin();
                EmsBegin(_db);

                OracleDataAdapter oDA = new OracleDataAdapter();
                SqlDataAdapter oDASQL = new SqlDataAdapter();
                OracleCommand _oCom = new OracleCommand();
                SqlCommand _oComSQL = new SqlCommand();
                SqlCommand _oComSQL1 = new SqlCommand();
                DataSet dsWel = new DataSet();
                int _ref = 0;

                string _sql = "SELECT * from SMS_jobstatus where pick=0";
                _oComSQL = new SqlCommand(_sql, oConnSql);
                //_oCom.Parameters.Add(":P_invoice_no", OracleDbType.NVarchar2).Value = "WPCR003841";
                _oComSQL.Transaction = oTrSql;
                oDASQL.SelectCommand = _oComSQL;
                oDASQL.Fill(dsWel, "SMS_jobstatus");
                Console.WriteLine("SMS Count :- " + dsWel.Tables["SMS_jobstatus"].Rows.Count.ToString());
                for (int I = 0; I <= dsWel.Tables["SMS_jobstatus"].Rows.Count - 1; I++)
                {
                    if (dsWel.Tables["SMS_jobstatus"].Rows[I]["senderno"].ToString() != "")
                    {
                        Console.WriteLine(dsWel.Tables["SMS_jobstatus"].Rows[I]["jobcardno"].ToString());
                        _sql = "INSERT INTO sms_out(SENDER,SENDERPHNO,RECEIVER,RECEIVERPHNO,MSG,MSGID,MSGSTATUS,REFDOCNO,CREATETIME,comcode,cateid) VALUES " +
                            "(:SENDER,:SENDERPHNO,:RECIEVER,:RECIEVERPHNO,:MSG,'',0,:REFDOCNO,CURRENT_DATE,'ABE',1)";
                        _oCom = new OracleCommand(_sql, oConnEMS) { CommandType = CommandType.Text, BindByName = true };
                        _oCom.Parameters.Add(":SENDER", OracleDbType.NVarchar2).Value = "ABE";
                        _oCom.Parameters.Add(":SENDERPHNO", OracleDbType.NVarchar2).Value = dsWel.Tables["SMS_jobstatus"].Rows[I]["senderno"].ToString();
                        _oCom.Parameters.Add(":RECIEVER", OracleDbType.NVarchar2).Value = dsWel.Tables["SMS_jobstatus"].Rows[I]["seqno"].ToString();
                        _oCom.Parameters.Add(":RECIEVERPHNO", OracleDbType.NVarchar2).Value = dsWel.Tables["SMS_jobstatus"].Rows[I]["senderno"].ToString();
                        _oCom.Parameters.Add(":REFDOCNO", OracleDbType.NVarchar2).Value = dsWel.Tables["SMS_jobstatus"].Rows[I]["jobcardno"].ToString();
                        _oCom.Parameters.Add(":MSG", OracleDbType.NVarchar2).Value = dsWel.Tables["SMS_jobstatus"].Rows[I]["msg"].ToString();
                        _ref = _oCom.ExecuteNonQuery();

                        _sql = "update SMS_jobstatus set pick = 1, picktime=getdate() WHERE seqno = " + Convert.ToInt32(dsWel.Tables["SMS_jobstatus"].Rows[I]["seqno"].ToString());
                        _oComSQL1 = new SqlCommand(_sql, oConnSql);
                        _oComSQL1.Transaction = oTrSql;
                        //_oComSQL1.Parameters.Add(":seqno", OracleDbType.Int16).Value = Convert.ToInt16(dsWel.Tables["SMS_jobstatus"].Rows[I]["seqno"].ToString());
                        _ref = _oComSQL1.ExecuteNonQuery();
                    }
                }

                EmsCommit();
                SqlCommit();
                //ConnectionCloseSQL();

            }
            catch (Exception ex)
            {
                EmsRollback();
                SqlRollback();
                //ConnectionCloseSQL();

                SendSMS("0773973588", "0773973588", "Send SMS Wellawatte Issue " + ex.Message);
                Send_SMTPMail("chamald@abansgroup.com", "chamald@abansgroup.com", "chamald@abansgroup.com", "chamald@abansgroup.com", "chamald@abansgroup.com", "Send SMS Wellawatte Issue", "Send SMS Wellawatte" + ex.Message);
                Console.WriteLine("");
                Console.WriteLine("***********************ERROR :-" + ex.Message);
            }
        }

        public void SendSMS(string _SENDER, string _SENDERPHNO, string _MSG)
        {
            OracleDataReader Ordsub = default(OracleDataReader);
            int _ref = 0;
            OpenSCM();
            _sql = " select MSG from SCM_MSG_OUT where MSG =:MSG and SENDERPHNO=:SENDERPHNO";
            OracleCommand _oCom1 = new OracleCommand(_sql, oConnSCM) { CommandType = CommandType.Text, BindByName = true };
            _oCom1.Transaction = oTrSCM;
            _oCom1.Parameters.Add(":MSG", OracleDbType.NVarchar2).Value = _MSG;
            _oCom1.Parameters.Add(":SENDERPHNO", OracleDbType.NVarchar2).Value = _SENDERPHNO;
            Ordsub = _oCom1.ExecuteReader();
            if (Ordsub.HasRows == false)
            {
                _sql = "INSERT INTO SCM_MSG_OUT(SENDER,SENDERPHNO,RECEIVER,RECEIVERPHNO,MSG,MSGID,MSGSTATUS,REFDOCNO,CREATETIME) VALUES " +
                "(:SENDER,:SENDERPHNO,'SCM2','+9477777777',:MSG,'ERR',0,'ERR',CURRENT_DATE)";
                OracleCommand _oCom = new OracleCommand(_sql, oConnSCM) { CommandType = CommandType.Text, BindByName = true };
                _oCom.Parameters.Add(":SENDER", OracleDbType.NVarchar2).Value = _SENDER;
                _oCom.Parameters.Add(":SENDERPHNO", OracleDbType.NVarchar2).Value = _SENDERPHNO;
                _oCom.Parameters.Add(":MSG", OracleDbType.NVarchar2).Value = _MSG;
                _ref = _oCom.ExecuteNonQuery();
            }

        }
        public void Send_SMTPMail(string _recipientEmailAddress, string _superior_mail1, string _superior_mail2, string _superior_mail3, string _superior_mail4, string _subject, string _message)
        {
            OracleDataReader Ordsub = default(OracleDataReader);
            OpenSCM();
            _sql = " select M_DES from SCM_MAIL_LOG where M_DES =:M_DES and M_TYPE=:M_TYPE";
            OracleCommand _oCom1 = new OracleCommand(_sql, oConnSCM) { CommandType = CommandType.Text, BindByName = true };
            _oCom1.Transaction = oTrSCM;
            _oCom1.Parameters.Add(":M_DES", OracleDbType.NVarchar2).Value = _message;
            _oCom1.Parameters.Add(":M_TYPE", OracleDbType.NVarchar2).Value = "BIGDEAL";
            Ordsub = _oCom1.ExecuteReader();
            if (Ordsub.HasRows == false)
            {
                _sql = "INSERT INTO SCM_MAIL_LOG(M_DATE,M_EMAL,M_DES,M_CRE_DATE,M_TYPE) VALUES " +
               "(:M_DATE,:M_EMAL,:M_DES,CURRENT_DATE,:M_TYPE)";
                OracleCommand _oCom = new OracleCommand(_sql, oConnSCM) { CommandType = CommandType.Text, BindByName = true };
                _oCom.Parameters.Add(":M_DATE", OracleDbType.Date).Value = DateTime.Today;
                _oCom.Parameters.Add(":M_EMAL", OracleDbType.NVarchar2).Value = _recipientEmailAddress;
                _oCom.Parameters.Add(":M_DES", OracleDbType.NVarchar2).Value = _message;
                _oCom.Parameters.Add(":M_TYPE", OracleDbType.NVarchar2).Value = "BIGDEAL";
                _oCom.ExecuteNonQuery();

                _sql = "INSERT INTO SCM_MAIL_LOG(M_DATE,M_EMAL,M_DES,M_CRE_DATE,M_TYPE) VALUES " +
               "(:M_DATE,:M_EMAL,:M_DES,CURRENT_DATE,:M_TYPE)";
                _oCom = new OracleCommand(_sql, oConnSCM) { CommandType = CommandType.Text, BindByName = true };
                _oCom.Parameters.Add(":M_DATE", OracleDbType.Date).Value = DateTime.Today;
                _oCom.Parameters.Add(":M_EMAL", OracleDbType.NVarchar2).Value = _superior_mail1;
                _oCom.Parameters.Add(":M_DES", OracleDbType.NVarchar2).Value = _message;
                _oCom.Parameters.Add(":M_TYPE", OracleDbType.NVarchar2).Value = "BIGDEAL";
                _oCom.ExecuteNonQuery();

                _sql = "INSERT INTO SCM_MAIL_LOG(M_DATE,M_EMAL,M_DES,M_CRE_DATE,M_TYPE) VALUES " +
               "(:M_DATE,:M_EMAL,:M_DES,CURRENT_DATE,:M_TYPE)";
                _oCom = new OracleCommand(_sql, oConnSCM) { CommandType = CommandType.Text, BindByName = true };
                _oCom.Parameters.Add(":M_DATE", OracleDbType.Date).Value = DateTime.Today;
                _oCom.Parameters.Add(":M_EMAL", OracleDbType.NVarchar2).Value = _superior_mail2;
                _oCom.Parameters.Add(":M_DES", OracleDbType.NVarchar2).Value = _message;
                _oCom.Parameters.Add(":M_TYPE", OracleDbType.NVarchar2).Value = "BIGDEAL";
                _oCom.ExecuteNonQuery();

                _sql = "INSERT INTO SCM_MAIL_LOG(M_DATE,M_EMAL,M_DES,M_CRE_DATE,M_TYPE) VALUES " +
               "(:M_DATE,:M_EMAL,:M_DES,CURRENT_DATE,:M_TYPE)";
                _oCom = new OracleCommand(_sql, oConnSCM) { CommandType = CommandType.Text, BindByName = true };
                _oCom.Parameters.Add(":M_DATE", OracleDbType.Date).Value = DateTime.Today;
                _oCom.Parameters.Add(":M_EMAL", OracleDbType.NVarchar2).Value = _superior_mail3;
                _oCom.Parameters.Add(":M_DES", OracleDbType.NVarchar2).Value = _message;
                _oCom.Parameters.Add(":M_TYPE", OracleDbType.NVarchar2).Value = "BIGDEAL";
                _oCom.ExecuteNonQuery();

                _sql = "INSERT INTO SCM_MAIL_LOG(M_DATE,M_EMAL,M_DES,M_CRE_DATE,M_TYPE) VALUES " +
               "(:M_DATE,:M_EMAL,:M_DES,CURRENT_DATE,:M_TYPE)";
                _oCom = new OracleCommand(_sql, oConnSCM) { CommandType = CommandType.Text, BindByName = true };
                _oCom.Parameters.Add(":M_DATE", OracleDbType.Date).Value = DateTime.Today;
                _oCom.Parameters.Add(":M_EMAL", OracleDbType.NVarchar2).Value = _superior_mail4;
                _oCom.Parameters.Add(":M_DES", OracleDbType.NVarchar2).Value = _message;
                _oCom.Parameters.Add(":M_TYPE", OracleDbType.NVarchar2).Value = "BIGDEAL";
                _oCom.ExecuteNonQuery();

                SmtpClient smtpClient = new SmtpClient();
                MailMessage message = new MailMessage();

                MailAddress _senderEmailAddress = new MailAddress("SCM@abansgroup.com", "SCM");

                smtpClient.Host = System.Configuration.ConfigurationManager.ConnectionStrings["MailHost"].ConnectionString;
                smtpClient.Port = 25;
                message.From = _senderEmailAddress;

                //string _email = _generalDAL.GetMailFooterMsg();

                message.To.Add(_recipientEmailAddress);
                message.Subject = _subject;
                message.CC.Add(new MailAddress(_superior_mail1));
                message.CC.Add(new MailAddress(_superior_mail2));
                message.CC.Add(new MailAddress(_superior_mail3));
                //  message.CC.Add(new MailAddress(_superior_mail4));
                //message.Bcc.Add(new MailAddress(""));
                message.IsBodyHtml = false;
                message.Body = _message;
                message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;

                // Send SMTP mail
                smtpClient.Send(message);
            }
        }

    }
}
