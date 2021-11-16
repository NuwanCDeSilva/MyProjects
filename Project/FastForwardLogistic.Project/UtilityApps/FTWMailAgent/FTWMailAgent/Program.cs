using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FTWMailAgent.Model;
using FTWMailAgent.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTWMailAgent
{
    class Program
    {
        public static string PDFSavePath = String.Empty;
        public static string ReportPath = String.Empty;
        public static string path = String.Empty;
        public static string copyright = String.Empty;
        public static string emailBody = string.Empty;
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            path = System.Configuration.ConfigurationManager.AppSettings["ErrorLogPath"].ToString();
            path = path + "/ErrorMail.txt";
            copyright = System.Configuration.ConfigurationManager.AppSettings["Copyright"].ToString();
            SendMail mail = new SendMail();
            PDFSavePath = System.Configuration.ConfigurationManager.AppSettings["PdfSavePath"].ToString();
            ReportPath = System.Configuration.ConfigurationManager.AppSettings["ReportPath"].ToString();
            try
            {
                MainMail();
            }
            catch (Exception ex)
            {
                SendMailAndSMS(ex.Message.ToString(), "Mail Error");
                Console.WriteLine("Exception :" + ex.Message.ToString());
            }

            Console.WriteLine("End");
        }

        public static void MainMail()
        {
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                int eff = 0;
                DataAccess dal = new DataAccess();
                DateTime current = DateTime.Now;
                try
                {
                    Console.WriteLine("Mail send function statrt. " + DateTime.Now);
                    bool Active = false;
                    DataTable ddsAll = new DataTable();
                    DataTable ddsCompany = new DataTable();
                    List<MST_MODULE_CONF> config = new List<MST_MODULE_CONF>();

                    config = dal.getRunningMethod();
                    foreach (MST_MODULE_CONF conf in config)
                    {
                        if (conf.MMC_MOD_CD == "UNSETTPETREQ")
                        {
                            writer.WriteLine("Start Module :" + DateTime.Now + " " + conf.MMC_MOD_CD);
                            string emailList = String.Empty;
                            Console.WriteLine("Mail send started  for:" + conf.MMC_MOD_CD);
                            MST_MODULE_CONF con = dal.getRunningMethodCof(conf.MMC_MOD_CD);
                            DateTime nextRunTime = con.MMC_LAST_RUN_DT.AddDays(con.MMC_RUN_TIME);
                            List<MST_ALERT_CRITERIA> userList = new List<MST_ALERT_CRITERIA>();
                            DateTime now = DateTime.Now;


                            List<MST_ALERT_CRITERIA> userMailCriteria = dal.getUserMailCriteria(conf.MMC_MOD_CD);
                            foreach (MST_ALERT_CRITERIA criteria in userMailCriteria)
                            {
                                userList = dal.getCriterialUSer(criteria.ALC_MODULE);
                                int i = 1;
                                emailList = string.Empty;
                                List<MST_ALERT_CRITERIA> newList = userList.Where(a => a.ALC_CODE == criteria.ALC_CODE && a.ALC_CRITERIA_TYPE == criteria.ALC_CRITERIA_TYPE && a.ALC_CA1 == criteria.ALC_CA1 && a.ALC_BRAND == criteria.ALC_BRAND && a.ALC_LATE_NOOF_DT==criteria.ALC_LATE_NOOF_DT).ToList();
                                foreach (MST_ALERT_CRITERIA email in newList)
                                {
                                    emailList += email.ALC_USER_EMAIL + ((i == newList.Count) ? "" : ",");
                                    i++;
                                }
                                try
                                {
                                    string crittyp = criteria.ALC_CRITERIA_TYPE;
                                    string crival = criteria.ALC_CODE;
                                    Int32 lteDate = criteria.ALC_LATE_NOOF_DT;

                                    ddsAll = dal.getUnsettledPettyCaseRequsts(crittyp, crival, lteDate);
                                    DataTable filter=new DataTable("filter");
                                    filter.Columns.Add("COM",typeof(string));
                                    string comdec = "";
                                    string[] words = criteria.ALC_CODE.Split(',');
                                    foreach (string word in words)
                                    {
                                        comdec = comdec + dal.getComDesc(word) + ",";
                                    }
                                    comdec = comdec.Remove(comdec.Length - 1);

                                    filter.Rows.Add(comdec);
                                    if (ddsAll.Rows.Count > 0)
                                    {
                                        genarateMailForPendingSettlement(ddsAll, filter, emailList, crival);
                                    }
                                   
                                }
                                catch (Exception ex)
                                {

                                    writer.WriteLine("Exception :" + " " + DateTime.Now.ToString() + " " + "Message :" + ex.Message.ToString());

                                    SendMailAndSMS(ex.Message.ToString(), "genarateMailForPendingSettlement");
                                    Console.WriteLine("Exception :" + ex.Message.ToString());
                                }
                            }
                            #region send all other users
                            try
                            {
                                DataTable getExecu = dal.getExcecIds("LGT", conf.MMC_MINIMUM_SEND_DT);
                                if (getExecu.Rows.Count > 0)
                                {
                                    Int32 i = 0;
                                    foreach (DataRow row in getExecu.Rows)
                                    {
                                        i++;
                                        DataTable filter = new DataTable("filter");
                                        filter.Columns.Add("COM", typeof(string));
                                        string comdec = "";
                                        comdec = dal.getComDesc("LGT");
                                        if (row["ESEP_EMAIL"] != DBNull.Value)
                                        {
                                            ddsAll = new DataTable("tbl");
                                            ddsAll = dal.getUnsettledPettyCaseRequstsUser("", "LGT", conf.MMC_MINIMUM_SEND_DT, row["TPRH_REQ_BY"].ToString());
                                            emailList = row["ESEP_EMAIL"].ToString();
                                            filter.Rows.Add(comdec);
                                            if (ddsAll.Rows.Count > 0)
                                            {
                                                genarateMailForPendingSettlement(ddsAll, filter, emailList, "LGT-" + i.ToString());
                                            }
                                        }
                                    }

                                }
                            }
                            catch (Exception ex)
                            {
                                writer.WriteLine("Exception :" + " " + DateTime.Now.ToString() + " " + "Message :" + ex.Message.ToString());

                                SendMailAndSMS(ex.Message.ToString(), "genarateMailForPendingSettlement Excecutive");
                                Console.WriteLine("Exception :" + ex.Message.ToString());
                            }
                            #endregion end send all other users

                            dal.updateConfTable(conf.MMC_MOD_CD, now);
                            Console.WriteLine("Mail send success criteria :" + conf.MMC_MOD_CD);

                            writer.WriteLine("End Module :" + DateTime.Now + " " + conf.MMC_MOD_CD);
                        }
                        else if (conf.MMC_MOD_CD == "UNSETTINV")
                        {
                            writer.WriteLine("Start Module :" + DateTime.Now + " " + conf.MMC_MOD_CD);
                            string emailList = String.Empty;
                            Console.WriteLine("Mail send started  for:" + conf.MMC_MOD_CD);
                            MST_MODULE_CONF con = dal.getRunningMethodCof(conf.MMC_MOD_CD);
                            DateTime nextRunTime = con.MMC_LAST_RUN_DT.AddDays(con.MMC_RUN_TIME);
                            List<MST_ALERT_CRITERIA> userList = new List<MST_ALERT_CRITERIA>();
                            DateTime now = DateTime.Now;


                            List<MST_ALERT_CRITERIA> userMailCriteria = dal.getUserMailCriteria(conf.MMC_MOD_CD);
                            foreach (MST_ALERT_CRITERIA criteria in userMailCriteria)
                            {
                                userList = dal.getCriterialUSer(criteria.ALC_MODULE);
                                int i = 1;
                                emailList = string.Empty;
                                List<MST_ALERT_CRITERIA> newList = userList.Where(a => a.ALC_CODE == criteria.ALC_CODE && a.ALC_CRITERIA_TYPE == criteria.ALC_CRITERIA_TYPE && a.ALC_CA1 == criteria.ALC_CA1 && a.ALC_BRAND == criteria.ALC_BRAND && a.ALC_LATE_NOOF_DT == criteria.ALC_LATE_NOOF_DT).ToList();
                                foreach (MST_ALERT_CRITERIA email in newList)
                                {
                                    emailList += email.ALC_USER_EMAIL + ((i == newList.Count) ? "" : ",");
                                    i++;
                                }
                                try
                                {
                                    string crittyp = criteria.ALC_CRITERIA_TYPE;
                                    string crival = criteria.ALC_CODE;
                                    Int32 lteDate = criteria.ALC_LATE_NOOF_DT;

                                    ddsAll = dal.getUnsettledPInvoice(crittyp, crival, lteDate);
                                    DataTable filter = new DataTable("filter");
                                    filter.Columns.Add("COM", typeof(string));
                                    string comdec = "";
                                    string[] words = criteria.ALC_CODE.Split(',');
                                    foreach (string word in words)
                                    {
                                        comdec = comdec + dal.getComDesc(word) + ",";
                                    }
                                    comdec = comdec.Remove(comdec.Length - 1);

                                    filter.Rows.Add(comdec);
                                    if (ddsAll.Rows.Count > 0)
                                    {
                                        genarateMailForPendingInvoice(ddsAll, filter, emailList, crival);
                                    }
                                }
                                catch (Exception ex)
                                {

                                    writer.WriteLine("Exception :" + " " + DateTime.Now.ToString() + " " + "Message :" + ex.Message.ToString());

                                    SendMailAndSMS(ex.Message.ToString(), "genarateMailForPendingInvoice");
                                    Console.WriteLine("Exception :" + ex.Message.ToString());
                                }
                            }

                            #region send all other users
                            try
                            {
                                DataTable getExecu = dal.getExcecInvoiceIds("LGT", conf.MMC_MINIMUM_SEND_DT);
                                if (getExecu.Rows.Count > 0)
                                {
                                    Int32 i = 0;
                                    foreach (DataRow row in getExecu.Rows)
                                    {
                                        i++;
                                        DataTable filter = new DataTable("filter");
                                        filter.Columns.Add("COM", typeof(string));
                                        string comdec = "";
                                        comdec = dal.getComDesc("LGT");
                                        if (row["ESEP_EMAIL"] != DBNull.Value)
                                        {
                                            ddsAll = new DataTable("tbl");
                                            ddsAll = dal.getUnsettledPInvoiceUser("LGT", conf.MMC_MINIMUM_SEND_DT, row["SAH_SALES_EX_CD"].ToString());
                                            emailList = row["ESEP_EMAIL"].ToString();
                                            filter.Rows.Add(comdec);
                                            if (ddsAll.Rows.Count > 0)
                                            {
                                                genarateMailForPendingInvoice(ddsAll, filter, emailList, "LGT-" + i.ToString());
                                            }
                                        }
                                    }

                                }
                            }
                            catch (Exception ex)
                            {
                                writer.WriteLine("Exception :" + " " + DateTime.Now.ToString() + " " + "Message :" + ex.Message.ToString());

                                SendMailAndSMS(ex.Message.ToString(), "genarateMailForPendingSettlement Excecutive");
                                Console.WriteLine("Exception :" + ex.Message.ToString());
                            }
                            #endregion end send all other users

                            dal.updateConfTable(conf.MMC_MOD_CD, now);
                            Console.WriteLine("Mail send success criteria :" + conf.MMC_MOD_CD);

                            writer.WriteLine("End Module :" + DateTime.Now + " " + conf.MMC_MOD_CD);
                        }
                    }
                    Console.WriteLine("Mail send function end. " + DateTime.Now);

                }
                catch (Exception ex)
                {
                    writer.WriteLine("Exception :" + " " + DateTime.Now.ToString() + " " + "Message :" + ex.Message.ToString());

                    SendMailAndSMS(ex.Message.ToString(), "Mail Error");
                    Console.WriteLine("Exception :" + ex.Message.ToString());
                }
            }
        }
        private static bool genarateMailForPendingSettlement(DataTable ddsAll, DataTable filter, string emailList,  string com)
        {
            try
            {
                PendingSetPetycash rpt = new PendingSetPetycash();
                ReportDocument rptDoc = new ReportDocument();
                rpt.Database.Tables["PENSETDET"].SetDataSource(ddsAll);
                rpt.Database.Tables["FILTER"].SetDataSource(filter);
                rptDoc.Load(ReportPath + "\\PendingSetPetycash" + ".rpt");

                string AttachmentPath = PDFSavePath + "\\Unsettle pettycase request details -" + com + ".pdf";
                rpt.ExportToDisk(ExportFormatType.PortableDocFormat, AttachmentPath);
                Console.WriteLine("File export success.");
                rptDoc.Close();
                rpt.Close();
                emailBody = "<html style='width: 590px;'>" +
                "<head> </head>" +
                "<body style='width: 450px; border: 2px solid rgb(156, 38, 204); padding: 15px; color: rgb(156, 38, 204);'>" +
                    "<h2 style='margin-bottom: 0px;'>SCM2 Infor Portal</h2>" +
                        "<div style='border-top:1px solid #9C26CC'> </div>" +
                        "<div style='color:#07000A'>" +
                            "<div>Dear Sir/Madam,</div><br/>" +
                            "<div>Please find '@reportname.' attached herewith.</div><br/>" +
                            "<div>** This is an auto generated mail from FTW infor portal. Please don't Reply **</div>" +
                        "</div><br/>" +
                        "<div style='border-top:1px solid #9C26CC'> </div>" +
                        "<h4 style='margin-bottom: 0px;'>**" + copyright + "**</h4>" +
                    "<span style='font-family:Arial;font-size:10pt'> </span></body>" +
                "</html>";
                emailBody = emailBody.Replace("@reportname", "Unsettle pettycase request details.");
                SendMail mail = new SendMail();

                Console.WriteLine("Unsettle pettycase request details report. Created |  Name : " + emailList + " | Mail :" + emailList);

                return mail.SendEMail(emailList, "Unsettle pettycase request details", emailBody, AttachmentPath, "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static bool genarateMailForPendingInvoice(DataTable ddsAll, DataTable filter, string emailList, string com)
        {
            try
            {
                PendingInvoice rpt = new PendingInvoice();
                ReportDocument rptDoc = new ReportDocument();
                rpt.Database.Tables["PENINV"].SetDataSource(ddsAll);
                rpt.Database.Tables["FILTER"].SetDataSource(filter);
                rptDoc.Load(ReportPath + "\\PendingInvoice" + ".rpt");

                string AttachmentPath = PDFSavePath + "\\Unsettle invoice details -" + com + ".pdf";
                rpt.ExportToDisk(ExportFormatType.PortableDocFormat, AttachmentPath);
                Console.WriteLine("File export success.");
                rptDoc.Close();
                rpt.Close();
                emailBody = "<html style='width: 590px;'>" +
                "<head> </head>" +
                "<body style='width: 450px; border: 2px solid rgb(156, 38, 204); padding: 15px; color: rgb(156, 38, 204);'>" +
                    "<h2 style='margin-bottom: 0px;'>SCM2 Infor Portal</h2>" +
                        "<div style='border-top:1px solid #9C26CC'> </div>" +
                        "<div style='color:#07000A'>" +
                            "<div>Dear Sir/Madam,</div><br/>" +
                            "<div>Please find '@reportname.' attached herewith.</div><br/>" +
                            "<div>** This is an auto generated mail from FTW infor portal. Please don't Reply **</div>" +
                        "</div><br/>" +
                        "<div style='border-top:1px solid #9C26CC'> </div>" +
                        "<h4 style='margin-bottom: 0px;'>**" + copyright + "**</h4>" +
                    "<span style='font-family:Arial;font-size:10pt'> </span></body>" +
                "</html>";
                emailBody = emailBody.Replace("@reportname", "Unsettle invoice details.");
                SendMail mail = new SendMail();

                Console.WriteLine("Unsettle invoice details report. Created |  Name : " + emailList + " | Mail :" + emailList);

                return mail.SendEMail(emailList, "Unsettle invoice details", emailBody, AttachmentPath, "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void SendMailAndSMS(string message, string function)
        {
            try
            {

                Console.WriteLine("Send mail start .");
                SendMail _mail = new SendMail();
                _mail.SendError("FTW Mail Agent:" + message + " <br\\> Function :" + function);
                Console.WriteLine("Send mail finish.");
                //Console.WriteLine("Start send SMS");
                //InventoryDAL DAL = new InventoryDAL();
                //DAL.SendSMS("NUWAN", "+94716430796", "Message :" + message + " Location :" + location + " Function :" + function);
                //Console.WriteLine("End send SMS");
            }
            catch (Exception ex)
            {
                Console.WriteLine("SendMailAndSMS Exception :" + ex.Message.ToString());
            }
        }
    }
}
