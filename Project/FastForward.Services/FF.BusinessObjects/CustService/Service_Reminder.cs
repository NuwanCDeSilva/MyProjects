using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class Service_Reminder
    {
        public String SjbJobNO { get; set; }
        public DateTime SjbJobDate { get; set; }
        public String SjbJobTitle { get; set; }
        public String SjcJobCustomer { get; set; }
        public String SjcJobCustomerID { get; set; }
        public String SjcJobAddress { get; set; }
        public String SjcJobMobileNo { get; set; }
        public String SjcJobEmail { get; set; }
        public String SjcJobduration { get; set; }
        public bool sjcChcekState { get; set; }

        public static Service_Reminder Converter(DataRow _dtaRow)
        {
            return new Service_Reminder
            {
                SjbJobNO = _dtaRow["SJB_JOBNO"] == DBNull.Value ? string.Empty : _dtaRow["SJB_JOBNO"].ToString(),
                SjbJobDate = _dtaRow["SJB_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(_dtaRow["SJB_DT"].ToString()),
                SjcJobCustomer = _dtaRow["MBE_NAME"] == DBNull.Value ? string.Empty : _dtaRow["MBE_NAME"].ToString(),
                SjcJobAddress = _dtaRow["ADDRESS"] == DBNull.Value ? string.Empty : _dtaRow["ADDRESS"].ToString(),
                SjcJobMobileNo = _dtaRow["MBE_MOB"] == DBNull.Value ? string.Empty : _dtaRow["MBE_MOB"].ToString(),
                SjcJobEmail = _dtaRow["MBE_EMAIL"] == DBNull.Value ? string.Empty : _dtaRow["MBE_EMAIL"].ToString(),
                SjcJobduration = _dtaRow["duration"] == DBNull.Value ? string.Empty : _dtaRow["duration"].ToString(),
                SjcJobCustomerID = _dtaRow["mbe_cd"] == DBNull.Value ? string.Empty : _dtaRow["mbe_cd"].ToString()
            };
             
        }
    }

    public class ServiceRreminder {
        public string comCode { get; set; }
        public string locCode { get; set; }
        public int msgType { get; set; }
        public DateTime frmDate { get; set; }
        public DateTime toDate { get; set; }
        public string jobNO { get; set; }
    }

    public class SmsOutMember
    {
        public string SmsOutMsg { get; set; }
        public string SmsOutReciver { get; set; }
        public string SmsOutReciverPhNo { get; set; }
        public string SmsOutSender { get; set; }
        public string SmsOutSnderPhnNo { get; set; }
        public string SmsOutValidPhnNo { get; set; }

        //sms ref data\
        public Int32 refReminderID { get; set; }
        public String refComName { get; set; }//Company
        public String refProfitCnter { get; set; }//Profit Center
        public String refLocation { get; set; }//Location
        public String refJobNo { get; set; }//job no
        public Int32 refLineNo { get; set; }//job line no   
        public String refEstimateNum { get; set; }//estimate number
        public String refSmsTxt { get; set; }//SMS Text
        public String refEmail { get; set; }//Email Text
        public Int32 refOutSeq { get; set; }//SMS Out Seq
        public Int32 refSmsStus { get; set; }
        public Int32 refEmStus { get; set; }
        public String refCreBy { get; set; }
        public DateTime refCreDate { get; set; }


    }

   


}
