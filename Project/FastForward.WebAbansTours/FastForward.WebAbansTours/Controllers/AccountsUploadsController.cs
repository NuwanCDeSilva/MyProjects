using FF.BusinessObjects;
using FF.BusinessObjects.ToursNew;
using FF.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastForward.WebAbansTours.Controllers
{
    public class AccountsUploadsController : BaseController
    {
        // GET: AccountsUploads
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                Session["wrongType"] = null;
                return View();
            }
            else
            {
                return Redirect("~/Login");
            }
        }
        public FilePathResult GetFile(DateTime startdate, DateTime enddate)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string filename1 = "ToursINV.txt";
                    string filename2 = "ToursREC.txt";
                    // string name = "C:/Users/subodanam/Desktop/ToursAccountsFile.txt";
                    var path = System.Web.Configuration.WebConfigurationManager.AppSettings["SUNUploadPath"].ToString();
                    string name = path + "/ToursINV.txt";
                    string name2 = path + "/ToursREC.txt";
                    string type = Request["pay_type"].ToString();


                    if (type == "INVOICE")
                    {
                        FileInfo info = new FileInfo(name);
                        if (info.Exists || !info.Exists)
                        {
                            using (StreamWriter writer = info.CreateText())
                            {
                                List<MST_GNR_ACC> gnrlist = CHNLSVC.Tours.GetgrnalDetails(company);
                                List<InvoiceHeader> list = CHNLSVC.Tours.GetAllSalesHRDdata(company, userDefPro, startdate, enddate);
                                List<InvoiceHeader> listorder = list.OrderBy(a => a.Sah_inv_tp).ToList();

                                string acctype1 = "CR";
                                string acctype2 = "DR";
                                string entryref1 = "CASH SALES";
                                string entryref2 = "CREDIT SALES";

                                foreach (var hdlist in listorder)
                                {
                                    if (hdlist.Sah_inv_tp != null | hdlist.Sah_inv_tp != "")
                                    {
                                        if (hdlist.Sah_inv_tp.Trim() == "CS")
                                        {
                                            //
                                            string AccCode1 = (gnrlist.Count>0)?gnrlist.Where(a => a.ACC_SUB_TP == acctype1 && a.ACC_ENTRY_REF == entryref1).Max(b => b.ACC_CD).ToString():"";
                                            string AccCode2 = (gnrlist.Count>0)?gnrlist.Where(a => a.ACC_SUB_TP == acctype2 && a.ACC_ENTRY_REF == entryref1).Max(b => b.ACC_CD).ToString():"";
                                            decimal Ammount1 = (hdlist!=null)?Convert.ToDecimal(hdlist.Sah_anal_7):0;
                                            string Ammount = Ammount1.ToString("F3", CultureInfo.InvariantCulture);
                                            DateTime transdate = hdlist.Sah_dt;
                                            string refNo = hdlist.Sah_inv_no.ToString();
                                            string cusname = hdlist.Sah_cus_name.ToString();
                                            string pc = hdlist.Sah_pc.ToString();
                                            string seq = hdlist.Sah_seq_no.ToString();


                                            if (hdlist.Sah_direct == true)
                                            {
                                                writer.WriteLine(GetLine(AccCode1, Ammount.ToString(), "C", transdate, refNo, cusname, pc, seq));
                                                writer.WriteLine(GetLine(AccCode2, Ammount.ToString(), "D", transdate, refNo, cusname, pc, seq));
                                            }
                                            else
                                            {
                                                writer.WriteLine(GetLine(AccCode1, Ammount.ToString(), "D", transdate, refNo, cusname, pc, seq));
                                                writer.WriteLine(GetLine(AccCode2, Ammount.ToString(), "C", transdate, refNo, cusname, pc, seq));
                                            }

                                          
                                           //Int32 result = CHNLSVC.Tours.UPDATE_INV_HDRENGLOG(refNo,1);

                                        }
                                        if (hdlist.Sah_inv_tp.Trim() == "CRED")
                                        {
                                            //
                                            String accCd = CHNLSVC.Sales.GetCustomerProfileByCom(hdlist.Sah_cus_cd, null, null, null, null, company).Mbe_acc_cd;
                                            string AccCode1 = (gnrlist.Count>0)? gnrlist.Where(a => a.ACC_SUB_TP == acctype1 && a.ACC_ENTRY_REF == entryref2).Max(b => b.ACC_CD).ToString():"";
                                            string AccCode2 = accCd;//(hdlist!=null)?hdlist.Sah_acc_no.ToString() : ""; //gnrlist.Where(a => a.ACC_SUB_TP == acctype2 && a.ACC_ENTRY_REF == entryref2).Max(b => b.ACC_CD).ToString();
                                            decimal Ammount1 = (hdlist!=null)?Convert.ToDecimal(hdlist.Sah_anal_7):0;
                                            string Ammount = Ammount1.ToString("F3", CultureInfo.InvariantCulture);
                                            DateTime transdate = hdlist.Sah_dt;
                                            string refNo = hdlist.Sah_inv_no.ToString();
                                            string cusname = hdlist.Sah_cus_name.ToString();
                                            string pc = hdlist.Sah_pc.ToString();
                                            string seq = hdlist.Sah_seq_no.ToString();

                                            if (hdlist.Sah_direct == true)
                                            {
                                                writer.WriteLine(GetLine(AccCode1, Ammount.ToString(), "C", transdate, refNo, cusname, pc, seq));
                                                writer.WriteLine(GetLine(AccCode2, Ammount.ToString(), "D", transdate, refNo, cusname, pc, seq));
                                            }
                                            else
                                            {
                                                writer.WriteLine(GetLine(AccCode1, Ammount.ToString(), "D", transdate, refNo, cusname, pc, seq));
                                                writer.WriteLine(GetLine(AccCode2, Ammount.ToString(), "C", transdate, refNo, cusname, pc, seq));
                                            }

                                           
                                            // Int32 result = CHNLSVC.Tours.UPDATE_INV_HDRENGLOG(refNo, 1);
                                        }
                                        if (hdlist.Sah_inv_tp.Trim() == "CRCD")
                                        {
                                            //
                                            String accCd = CHNLSVC.Sales.GetCustomerProfileByCom(hdlist.Sah_cus_cd, null, null, null, null, company).Mbe_acc_cd;
                                            string AccCode1 = (gnrlist.Count > 0) ? gnrlist.Where(a => a.ACC_SUB_TP == acctype1 && a.ACC_ENTRY_REF == entryref2).Max(b => b.ACC_CD).ToString() : "";
                                            string AccCode2 = accCd;//(hdlist!=null)?hdlist.Sah_acc_no.ToString() : ""; //gnrlist.Where(a => a.ACC_SUB_TP == acctype2 && a.ACC_ENTRY_REF == entryref2).Max(b => b.ACC_CD).ToString();
                                            decimal Ammount1 = (hdlist != null) ? Convert.ToDecimal(hdlist.Sah_anal_7) : 0;
                                            string Ammount = Ammount1.ToString("F3", CultureInfo.InvariantCulture);
                                            DateTime transdate = hdlist.Sah_dt;
                                            string refNo = hdlist.Sah_inv_no.ToString();
                                            string cusname = hdlist.Sah_cus_name.ToString();
                                            string pc = hdlist.Sah_pc.ToString();
                                            string seq = hdlist.Sah_seq_no.ToString();

                                            if (hdlist.Sah_direct == true)
                                            {
                                                writer.WriteLine(GetLine(AccCode2, Ammount.ToString(), "C", transdate, refNo, cusname, pc, seq));
                                                writer.WriteLine(GetLine("ABDR31010", Ammount.ToString(), "D", transdate, refNo, cusname, pc, seq));
                                            }
                                            else
                                            {
                                                writer.WriteLine(GetLine(AccCode2, Ammount.ToString(), "D", transdate, refNo, cusname, pc, seq));
                                                writer.WriteLine(GetLine("ABDR31010", Ammount.ToString(), "C", transdate, refNo, cusname, pc, seq));
                                            }


                                            // Int32 result = CHNLSVC.Tours.UPDATE_INV_HDRENGLOG(refNo, 1);
                                        }
                                        if (hdlist.Sah_inv_tp.Trim() != "CRED" && hdlist.Sah_inv_tp.Trim() != "CS")
                                        {
                                            Session["wrongType"] = "Undifine Invoice Type";
                                        }
                                    }



                                }
                            }
                        }
                        // System.IO.File.Copy(@"C:\SUN\" + name, "\\\\192.168.1.224\\aal\\" + name, true);
                        System.IO.File.Copy(@"C:/SUN/" + filename1, "\\\\192.168.1.45\\SUN\\" + filename1, true);
                        // System.IO.File.Copy(@"C:/SUN/" + filename1, "C:/SUN/Subodana/" + filename1, true);
                        return File(name, "text/plain");


                    }
                    else
                    {
                        FileInfo info = new FileInfo(name2);
                        if (info.Exists || !info.Exists)
                        {
                            using (StreamWriter writer = info.CreateText())
                            {
                                List<MST_GNR_ACC> gnrlist = CHNLSVC.Tours.GetgrnalDetails(company);
                                List<RecieptHeader> list = CHNLSVC.Tours.GetAllRecieptHRDdata(company, userDefPro, startdate, enddate);
                                List<RecieptHeader> listorder = list.OrderBy(a => a.Sar_inv_type).ToList(); ;

                                string acctype1 = "CR";
                                string acctype2 = "DR";
                                string entryref1 = "RECEIPT";
                                string entryref2 = "CREDIT SALES";

                                foreach (var hdlist in listorder)
                                {
                                    if (hdlist.Sar_receipt_type != null | hdlist.Sar_receipt_type != "")
                                    {
                                        String accCd = CHNLSVC.Sales.GetCustomerProfileByCom(hdlist.Sar_debtor_cd, null, null, null, null, company).Mbe_acc_cd;
                                        if (hdlist.Sar_receipt_type == "DEBT")
                                        {
                                            string AccCode1 = (accCd!=null)?accCd:"";//(hdlist!=null)?hdlist.Sar_acc_no.ToString():""; //gnrlist.Where(a => a.ACC_SUB_TP == acctype1 && a.ACC_ENTRY_REF==entryref2).Max(b => b.ACC_CD).ToString();
                                            string AccCode2 = (gnrlist.Count>0)?gnrlist.Where(a => a.ACC_SUB_TP == acctype2 && a.ACC_ENTRY_REF == entryref1).Max(b => b.ACC_CD).ToString():"";
                                            decimal Ammount1 = (hdlist!=null)?Convert.ToDecimal(hdlist.Sar_tot_settle_amt):0;
                                            string Ammount = Ammount1.ToString("F3", CultureInfo.InvariantCulture);
                                            DateTime transdate = hdlist.Sar_receipt_date;
                                            string refNo = hdlist.Sar_receipt_no.ToString();
                                            string cusname = hdlist.Sar_debtor_name.ToString();
                                            string pc = hdlist.Sar_profit_center_cd.ToString();
                                            string seq = hdlist.Sar_seq_no.ToString();
                                            writer.WriteLine(GetLine(AccCode1, Ammount.ToString(), "C", transdate, refNo, cusname, pc, seq));
                                            writer.WriteLine(GetLine(AccCode2, Ammount.ToString(), "D", transdate, refNo, cusname, pc, seq));
                                            // Int32 result = CHNLSVC.Tours.UPDATE_RECIEPT_HDRENGLOG(refNo, 1);
                                        }
                                        //else
                                        //{
                                        //    string AccCode1 = accCd;//(hdlist != null) ? hdlist.Sar_acc_no.ToString() : ""; //gnrlist.Where(a => a.ACC_SUB_TP == acctype1 && a.ACC_ENTRY_REF == entryref1).Max(b => b.ACC_CD).ToString();
                                        //    string AccCode2 = (gnrlist.Count>0)?gnrlist.Where(a => a.ACC_SUB_TP == acctype2 && a.ACC_ENTRY_REF == entryref1).Max(b => b.ACC_CD).ToString():"";
                                        //    decimal Ammount1 = (hdlist!=null)?Convert.ToDecimal(hdlist.Sar_tot_settle_amt):0;
                                        //    string Ammount = Ammount1.ToString("F3", CultureInfo.InvariantCulture);
                                        //    DateTime transdate = hdlist.Sar_receipt_date;
                                        //    string refNo = hdlist.Sar_receipt_no.ToString();
                                        //    string cusname = hdlist.Sar_debtor_name.ToString();
                                        //    string pc = hdlist.Sar_profit_center_cd.ToString();
                                        //    string seq = hdlist.Sar_seq_no.ToString();
                                        //    writer.WriteLine(GetLine(AccCode1, Ammount.ToString(), "C", transdate, refNo, cusname, pc, seq));
                                        //    writer.WriteLine(GetLine(AccCode2, Ammount.ToString(), "D", transdate, refNo, cusname, pc, seq));
                                        //    //  Int32 result = CHNLSVC.Tours.UPDATE_RECIEPT_HDRENGLOG(refNo, 1);
                                        //}
                                    }
                                    //if (hdlist.Sar_receipt_type.Trim() != "CRED" && hdlist.Sar_receipt_type.Trim() != "CS")
                                    //{
                                    //    Session["wrongType"] = "Undifine Invoice Type";
                                    //}
                                }
                            }
                        }
                        // System.IO.File.Copy(@"C:\SUN\" + name, "\\\\192.168.1.224\\aal\\" + name, true);
                        System.IO.File.Copy(@"C:/SUN/" + filename2, "\\\\192.168.1.45\\SUN\\" + filename2, true);
                        // System.IO.File.Copy(@"C:/SUN/" + filename2, "C:/SUN/Subodana/" + filename2, true);
                        return File(name2, "text/plain");
                    }


                }
                return File("", "text/plain");
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        private string GetLine(string AccCode, string Ammount, string AmmountType, DateTime trdate, string refNo, string cusname, string pc, string seq)
        {
            string space10 = "          ";
            string ammnotes18 = "000000000000000000";
           
            string space5 = "     ";
            string space2 = "  ";
            string space1 = " ";
            string space7 = "       ";
            DateTime summerdate = trdate;
            summerdate = summerdate.AddMonths(-3);
            string jrnum = "0000000";
            string jrlinenum = "00000";
            string allocmak = " ";
            string jrtype = "GEN  ";
            string jrscource = "KKK  ";
            string newrefNo;
            string space25 = "                         ";
            string newcusname;
            string otherdatenote8 = "00000000";
            string paymentallocnote10 = "0000000000";
            // string pppp = "00/00/0000 00:00:00 AM";
            //DateTime paymentallocdate8 = Convert.ToDateTime(pppp);
            string paymentallocdate8 = "00000000";
            string paymentallocdate7 = "0000000";
            string converrate = "000000000000000000";
            string otherammount = "000000000000000000";
            string oienter = "   ";
            string oipost = "ABC";
            string oilast = "   ";
            string nextperdrev = " ";
            string linktext = " ";
            //Addtional space
            string additinalsp2 = "  ";
            ///////////
            string space15 = "               ";
            string pcnew;
            string annlcd1 = space15;
            string annlcd2 = space15;
            string seqnew;
            string annlcd5 = space15;
            string annlcd6 = space15;
            string annlcd7 = "N/A";
            string ann7new;
            string annlcd8 = space15;
            string annlcd9 = space15;
            string blankfinal = space15 + space15 + space15 + space15 + space15 + space15 + space15 + space7;

            ////////
            int sp10lenth = space10.Length;
            int spdef = sp10lenth - AccCode.Length;
          
           
            string acccdnew;
            if (spdef >= 0)
            {
                acccdnew = AccCode + space10.Substring(0, spdef);
            }
            else
            {
                acccdnew = AccCode.Substring(0, 10);
            }
            ////////////
            string[] words = Ammount.Split('.');
            string Ammountnew = words[0] + words[1];

            int ammnolenth = ammnotes18.Length;
            int ammlenth = Ammountnew.Length;
            int def = ammnolenth - ammlenth;
            string ammountnew;
            if (def >= 0)
            {
                ammountnew = ammnotes18.Substring(0, def) + Ammountnew;
            }
            else
            {
                ammountnew = Ammountnew.Substring(0, 18);
            }

            if (refNo.Length > 10)
            {
                newrefNo = refNo.Substring(0, 5)+refNo.Substring(refNo.Length-5);
               
            }
            else
            {
                int defref = 10 - refNo.Length;
                newrefNo = refNo + space10.Substring(0, defref);

            }

            int cusdef = space25.Length - cusname.Length;
            if (cusdef >= 0)
            {
                newcusname = cusname + space25.Substring(0, cusdef);
            }
            else
            {
                newcusname = cusname.Substring(0, 25);
            }
            int pcdef = space15.Length - pc.Length;
            if (pcdef >= 0)
            {
                pcnew = pc + space15.Substring(0, pcdef);
            }
            else
            {
                pcnew = pc.Substring(0, 15);
            }
            int seqdef = space15.Length - seq.Length;
            if (seqdef >= 0)
            {
                seqnew = seq + space15.Substring(0, seqdef);
            }
            else
            {
                seqnew = seq.Substring(0, 15);
            }

            int annl7def = space15.Length - annlcd7.Length;
            if (annl7def >= 0)
            {
                ann7new = annlcd7 + space15.Substring(0, annl7def);
            }
            else
            {
                ann7new = annlcd7.Substring(0, 15);
            }
            string line = acccdnew +
                space5 +
                summerdate.ToString("yyyy"+"0"+ "MM") + trdate.ToString("yyyyMMdd") +
                space2 +
                "L" + jrnum + jrlinenum +
                space2 +
                ammountnew + AmmountType +
                allocmak +
                jrtype +
                jrscource +
                newrefNo +
                space5 +
                newcusname +
                 trdate.ToString("yyyyMMdd") +
                 summerdate.ToString("yyyy" + "0" + "MM") +
                 otherdatenote8 +
                 paymentallocnote10 +
                 space5 +
                 paymentallocdate8 +
                 paymentallocdate7 +
                 space1 +
                 space10 +
                 space5 +
                 space5 +
                 converrate +
                 otherammount +
                 space1 +
                 oienter +
                 oipost +
                 oilast +
                 nextperdrev +
                 linktext +
                 additinalsp2 +
                 pcnew +
                 annlcd1 +
                 annlcd2 +
                 seqnew +
                 annlcd5 +
                 annlcd6 +
                 ann7new +
                 annlcd8 +
                 annlcd9 +
                 blankfinal
                 ;

            return line;
        }

        public JsonResult GetAlert()
        {
            return Json(new { success = false, login = true, msg = "Cannot Found Invoice Type", type = "Info" }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SubmitionAlert()
        {
            return Json(new { success = true, login = true, msg = "Upload Complete", type = "Success" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UploadAccounts()
        {
            DateTime startdate;
            DateTime enddate;
            startdate = Convert.ToDateTime(Request["st_date"].ToString());
            enddate = Convert.ToDateTime(Request["end_date"].ToString());
            GetFile(startdate, enddate);

            if (Session["wrongType"] != null)
            {
                return Json(new { success = true, login = true, msg = "Upload Complete But Some Undifine Invoice Type", type = "Success" }, JsonRequestBehavior.AllowGet);
                
            }
            else
            {
                Session["wrongType"] = "";
                return Json(new { success = true, login = true, msg = "Upload Complete", type = "Success" }, JsonRequestBehavior.AllowGet);
            }

           
        }

        public JsonResult LoadStatus()
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    ComboBoxObject o1 = new ComboBoxObject();
                    o1.Text = "INVOICE";
                    o1.Value = "INVOICE";
                    oList.Add(o1);

                    ComboBoxObject o2 = new ComboBoxObject();
                    o2.Text = "RECEIPT";
                    o2.Value = "RECEIPT";
                    oList.Add(o2);


                    return Json(new { success = true, login = true, data = oList }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = true, data = Resource.ServerError }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}