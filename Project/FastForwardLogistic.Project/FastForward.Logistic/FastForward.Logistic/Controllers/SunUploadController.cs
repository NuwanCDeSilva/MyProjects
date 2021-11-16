using FF.BusinessObjects.Genaral;
using FF.BusinessObjects.Sales;
using FF.BusinessObjects.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastForward.Logistic.Controllers
{
    public class SunUploadController : BaseController
    {
        //
        // GET: /SunUpload/
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserId"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                return View();
            }
            else
            {
                return Redirect("~/Home/index");
            }
        }
        public JsonResult LoadTypes()
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
                    o1.Text = "SelectType";
                    o1.Value = "SelectType";
                    oList.Add(o1);
                    ComboBoxObject o2 = new ComboBoxObject();
                    o2.Text = "Debtor";
                    o2.Value = "Debtor";
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
                return Json(new { success = false, login = true, data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult LoadDevTypes()
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
                    o1.Text = "SelectType";
                    o1.Value = "SelectType";
                    oList.Add(o1);
                    ComboBoxObject o2 = new ComboBoxObject();
                    o2.Text = "Invoice";
                    o2.Value = "Invoice";
                    oList.Add(o2);
                    ComboBoxObject o3 = new ComboBoxObject();
                    o3.Text = "Reciept";
                    o3.Value = "Reciept";
                    oList.Add(o3);
                    ComboBoxObject o4 = new ComboBoxObject();
                    o4.Text = "PettyCash";
                    o4.Value = "PettyCash";
                    oList.Add(o4);

                    ComboBoxObject o5 = new ComboBoxObject();
                    o5.Text = "CreditNote";
                    o5.Value = "CreditNote";
                    oList.Add(o5);
                    ComboBoxObject o7 = new ComboBoxObject();
                    o7.Text = "Payment";
                    o7.Value = "Payment";
                    oList.Add(o7);
                    ComboBoxObject o8 = new ComboBoxObject();
                    o8.Text = "TTPayments";
                    o8.Value = "TTPayments";
                    oList.Add(o8);
                    ComboBoxObject o9 = new ComboBoxObject();
                    o9.Text = "Settlement";
                    o9.Value = "Settlement";
                    oList.Add(o9);
                    ComboBoxObject o10 = new ComboBoxObject();
                    o10.Text = "AD-HOC Reciept";
                    o10.Value = "Container";
                    oList.Add(o10);
                    return Json(new { success = true, login = true, data = oList }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { success = true, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = true, data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult LoadPcs(string type)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if(type == "Debtor")
                    {
                        type = "DEBT";
                    }
                    DataTable pcdt = new DataTable();
                    pcdt = CHNLSVC.Sales.GetSunPC(type, company);
                    List<Sun_pc> pclist = new List<Sun_pc>();
                    int i = 0;
                    if (pcdt != null)
                    {
                        foreach (var dt in pcdt.Rows)
                        {
                            Sun_pc ob = new Sun_pc();
                            ob.PC = pcdt.Rows[i]["sn_pccd"].ToString();
                            pclist.Add(ob);
                            i++;
                        }
                    }


                    return Json(new { success = true, login = true, data = pclist }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { success = true, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = true, data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Upload(string pcs, DateTime fdate, DateTime tdate, string Dtype)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

                string txtpath = "";

                //if (pcs =="" || Dtype=="")
                //{
                //    //return Json(new { success = false, login = true }, JsonRequestBehavior.AllowGet);
                //    return Json(new { success = false, login = true, msg = "Not Define " }, JsonRequestBehavior.AllowGet);
                //    //return Json(new { success = false, login = true, data = "Genarated fail!!", msg = "Genarated fail!!" }, JsonRequestBehavior.AllowGet);
                //}

                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string name = "C:/SUN" + "/LGTinv.txt";
                    string name2 = "C:/SUN" + "/LGTrec.txt";
                    string name3 = "C:/SUN" + "/LGTsttlement.txt";
                    string name4 = "C:/SUN" + "/LGTTTPay.txt";
                    string name5 = "C:/SUN" + "/LGTTTPettych.txt";
                    pcs = pcs.Replace(@"chkpc=", string.Empty);
                    string[] words222 = pcs.Split('&');
                    string _pcstrng = "";
                    string execcode = "N/A";
                    string ISTAX = "";
                    int count = 0;
                    string Acccode = "";
                    string jnaltype = "";
                    string date = tdate.Date.ToString("dd");
                    date = company + date + ".txt";
                    foreach (var pclist in words222)
                    {
                        _pcstrng = _pcstrng + pclist.ToString() + ",";

                    }

                    #region Invoice Upload
                    if (Dtype == "Invoice")
                    {
                        FileInfo info = new FileInfo(name);
                        if (info.Exists || !info.Exists)
                        {
                            using (StreamWriter writer = info.CreateText())
                            {
                                List<SUN_JURNAL> gnrlist = CHNLSVC.Sales.GetSunJurnalnew(company);
                                gnrlist = gnrlist.OrderBy(a => a.ledg_sales_tp).ToList();
                                List<SUNINVHDR> LIORDERSUM= new List<SUNINVHDR>();
                                List<SUNINVHDR> listorder = CHNLSVC.Sales.GetSunInvdatanew(company, _pcstrng, fdate, tdate);
                                if (listorder == null)
                                {
                                    return Json(new { success = false, login = true, data = "", msg = "No data found", Type = "Info" }, JsonRequestBehavior.AllowGet);                     
                                }
                                else
                                {
                                   LIORDERSUM = listorder.GroupBy(l => new { l.sah_inv_no,l.sad_itm_cd })
    .Select(cl => new SUNINVHDR
    {
        sah_inv_no = cl.First().sah_inv_no,
        sah_inv_tp = cl.First().sah_inv_tp,
        sah_inv_sub_tp = cl.First().sah_inv_sub_tp,
        sah_tp = cl.First().sah_tp,
        total = cl.First().total,
        totalunit = cl.Sum(A => A.totalunit),
        taxtotal = 0,
        sah_seq_no = cl.First().sah_seq_no,
        sah_pc = cl.First().sah_pc,
        sah_cus_name = cl.First().sah_cus_name,
        sah_dt = cl.First().sah_dt,
        sah_cus_cd = cl.First().sah_cus_cd,
        sah_direct = cl.First().sah_direct,
        sah_man_ref = cl.First().sah_man_ref,
        tax_cd = cl.First().tax_cd,
        isdiliver = cl.First().isdiliver,
        sah_ref_doc = cl.First().sah_ref_doc,
        sah_sales_ex_cd = cl.First().sah_sales_ex_cd,
        sah_is_svat = cl.First().sah_is_svat,
        svatcd = cl.First().svatcd,
        EPF = cl.First().EPF,
        CODE = cl.First().CODE,
        RealTotalwithtax = cl.Sum(a => a.RealTotalwithtax),
        NBTValue = cl.Sum(a => a.NBTValue),
        sah_tax_exempted = cl.First().sah_tax_exempted,
        sah_anal_4 = cl.First().sah_anal_4,
        SerCode = cl.First().SerCode,
        sad_itm_cd=cl.First().sad_itm_cd,
        tid_cha_desc = cl.First().tid_cha_desc,
        tih_job_no = cl.First().tih_job_no
    }).ToList();
                                    LIORDERSUM = LIORDERSUM.OrderBy(a => a.sah_inv_no).ToList();
                                    listorder = listorder.GroupBy(l => new { l.sah_inv_no })
        .Select(cl => new SUNINVHDR
        {
            sah_inv_no = cl.First().sah_inv_no,
            sah_inv_tp = cl.First().sah_inv_tp,
            sah_inv_sub_tp = cl.First().sah_inv_sub_tp,
            sah_tp = cl.First().sah_tp,
            total = cl.First().total,
            totalunit = cl.Sum(A => A.totalunit),
            taxtotal = 0,
            sah_seq_no = cl.First().sah_seq_no,
            sah_pc = cl.First().sah_pc,
            sah_cus_name = cl.First().sah_cus_name,
            sah_dt = cl.First().sah_dt,
            sah_cus_cd = cl.First().sah_cus_cd,
            sah_direct = cl.First().sah_direct,
            sah_man_ref = cl.First().sah_man_ref,
            tax_cd = cl.First().tax_cd,
            isdiliver = cl.First().isdiliver,
            sah_ref_doc = cl.First().sah_ref_doc,
            sah_sales_ex_cd = cl.First().sah_sales_ex_cd,
            sah_is_svat = cl.First().sah_is_svat,
            svatcd = cl.First().svatcd,
            EPF = cl.First().EPF,
            CODE = cl.First().CODE,
            RealTotalwithtax = cl.Sum(a => a.RealTotalwithtax),
            NBTValue = cl.Sum(a => a.NBTValue),
            sah_tax_exempted = cl.First().sah_tax_exempted,
            sah_anal_4 = cl.First().sah_anal_4,
            SerCode = cl.First().SerCode,
            sad_itm_cd = cl.First().sad_itm_cd,
            tid_cha_desc = cl.First().tid_cha_desc,
            tih_job_no = cl.First().tih_job_no
        }).ToList();
                                    listorder = listorder.OrderBy(a => a.sah_inv_no).ToList();
                                }
                                string tempinvno = "";
                                if (listorder != null)
                                    listorder = listorder.OrderBy(a => a.sah_inv_tp).ToList();
                                if (listorder != null && gnrlist != null)
                                {
                                    foreach (var invdata in listorder)
                                    {
                                        if (invdata.totalunit != 0)
                                        {
                                            execcode = "N/A";
                                            if (invdata.sah_sales_ex_cd != null) execcode = invdata.sah_sales_ex_cd.ToString();

                                            int paytpcount = 0;
                                            string DUTY = "";
                                            string paytype = "1";
                                            if (invdata.sah_inv_tp.ToString() != null)
                                                paytype = invdata.sah_inv_tp.Trim().ToString();

                                            foreach (var gnr in gnrlist)
                                            {
                                                if (gnr.ledg_is_dealer == null)
                                                {
                                                    gnr.ledg_is_dealer = "";
                                                }
                                                ISTAX = gnr.ledg_is_dealer;
                                                string gnrtype = "2";
                                                if (gnr.ledg_sales_tp != null)
                                                    gnrtype = gnr.ledg_sales_tp.Trim().ToString();

                                                if (paytype == gnrtype)
                                                    count = 1;
                                                if (gnr.ledg_pc == "IMPS")
                                                {

                                                }


                                                string job = invdata.tih_job_no;
                                                string[] _job = job.Split('/');
                                                string newjob = "";
                                                if (_job.Length>1)
                                                {
                                                    newjob = _job[1].Substring(0, 1) + "/";
                                                    if (invdata.sah_pc == "IMP" || invdata.sah_pc == "CLR")
                                                    {
                                                        newjob = newjob + "I" + "/";
                                                    }
                                                    else if (invdata.sah_pc == "SFW" || invdata.sah_pc == "SXP")
                                                    {
                                                        newjob = newjob + "E" + "/";
                                                    }
                                                    else if (invdata.sah_pc == "AEX" || invdata.sah_pc == "ACL")
                                                    {
                                                        newjob = newjob + "E" + "/";
                                                    }
                                                    else
                                                    {
                                                        newjob = newjob + "I" + "/";
                                                    }
                                                }
                                                else
                                                {
                                                    newjob = _job[0].Substring(0, 1) + "/";
                                                }

                                                if (_job.Length>2)
                                              {
                                                  if (_job[2].Length > 6)
                                                  {
                                                      int sunrefmaxle = _job[2].Length;
                                                      _job[2] = _job[2].Substring(sunrefmaxle - 6);
                                                  }

                                                  newjob = newjob + _job[2];
                                              }
                                                else
                                                {
                                                    newjob = newjob + _job[0];
                                                }
                                               
                                              

                                                #region customer
                                                if (paytype == gnrtype && gnr.ledg_sub_tp == "SA" && gnr.ledg_acc_tp == "DR" && gnr.ledg_pc == invdata.sah_pc && (gnr.ledg_desc == "CASH" | gnr.ledg_desc == "CRED") && gnr.ledg_jnl_tp == "15" && gnr.ledg_jnl_desc == invdata.SerCode)
                                                {
                                                    decimal alltotal = 0;
                                                    if (tempinvno != invdata.sah_inv_no)
                                                        alltotal = invdata.total;



                                                    //REVERSISSUE
                                                    tempinvno = invdata.sah_inv_no;
                                                    decimal TOTNEW = Convert.ToDecimal(invdata.totalunit) + Convert.ToDecimal(invdata.taxtotal);
                                                    string TOTNEW2 = alltotal.ToString("F3", CultureInfo.InvariantCulture);
                                                    string Cuscode = "";
                                                    Acccode = gnr.ledg_acc_cd.ToString();
                                                    string TOTAmmount = invdata.total.ToString("F3", CultureInfo.InvariantCulture);
                                                    string TAXAmmount = invdata.taxtotal.ToString("F3", CultureInfo.InvariantCulture);
                                                    string TOTunit = invdata.totalunit.ToString("F3", CultureInfo.InvariantCulture);

                                                    if (invdata.sah_cus_cd != null)
                                                        Cuscode = invdata.sah_cus_cd.ToString();
                                                    DateTime transdate = invdata.sah_dt;
                                                    string refNo = invdata.sah_inv_no.ToString();
                                                    string cusname = invdata.sah_cus_name.ToString().Trim();
                                                    string pc = invdata.sah_pc.ToString();
                                                    string seq = invdata.tax_cd.ToString();
                                                    jnaltype = "INV";//gnr.ledg_jnl_tp.ToString();

                                                    

                                                    if ((Cuscode.Contains("CONT") || Cuscode.Contains("CASH")))
                                                    {
                                                        string _cusacc = CHNLSVC.Sales.GetAccountCodeByTp(company, "CONTCUS");
                                                        Cuscode = _cusacc;
                                                    }

                                                    //check svat customer
                                                    //List<cus_details> _cus = CHNLSVC.General.GetCustormerdata(company,invdata.sah_cus_cd.ToString());
                                                    //if (_cus != null && _cus.Count>0)
                                                    //{
                                                    //        if (_cus.First().MBE_IS_SVAT==true)
                                                    //        {
                                                    //            Cuscode =  CHNLSVC.Sales.GetEleAccount("SVAT", "COS");
                                                    //        }
                                                    //}
                                                    if (seq == "" | seq == "M" | seq == "N") seq = "O";
                                                    string[] words = refNo.Split('-');
                                                    if (words.Length > 2)
                                                    {
                                                        refNo = words[0] + words[1] + words[3];
                                                    }
                                                    if (invdata.sah_direct == 0)
                                                    {
                                                        int invlenght = invdata.sah_inv_no.Length;
                                                        refNo = invdata.sah_ref_doc.ToString();
                                                        if (invdata.sah_anal_4 == "DISC")
                                                        {
                                                            refNo = invdata.sah_inv_no.ToString();
                                                        }
                                                        string[] words2 = refNo.Split('-');
                                                        if (words2.Length == 2)
                                                        {
                                                            if (invdata.sah_anal_4 == "DISC")
                                                            {
                                                                refNo = invdata.sah_pc.ToString() + words2[1];
                                                            }
                                                            else
                                                            {
                                                                refNo = "Z" + words2[0] + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                            }
                                                        }
                                                        else if (words2.Length == 3)
                                                        {
                                                            if (invdata.sah_anal_4 == "DISC")
                                                            {
                                                                refNo = invdata.sah_pc.ToString() + words2[2];
                                                            }
                                                            else
                                                            {

                                                                refNo = "Z" + words2[1] + invdata.sah_inv_no.Substring(invlenght - 5, 5);

                                                            }

                                                        }
                                                        else
                                                        {
                                                            if (invdata.sah_anal_4 == "DISC")
                                                            {
                                                                refNo = invdata.sah_pc.ToString() + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                            }
                                                            else
                                                            {
                                                                refNo = "Z" + refNo.Substring(0, 4) + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                            }

                                                        }

                                                    }

                                                    if (refNo.Length > 10)
                                                    {
                                                        refNo = refNo.Replace(@"-", string.Empty);
                                                    }
                                                    //CHECK T4

                                                    DataTable _dt = CHNLSVC.Sales.CheckSUNLC(company, "T4", newjob);
                                                    if (_dt != null && _dt.Rows.Count > 0)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        savejobnosun(company, "T4", newjob, DateTime.Now.Date, cusname);
                                                    }

                                                    if (invdata.sah_is_svat == 1) seq = invdata.svatcd.ToString();
                                                    if (jnaltype == "") jnaltype = "INV1";
                                                    if (gnr.ledg_desc == "DUTY")
                                                    {
                                                        jnaltype = "INV8";
                                                        seq = "O";
                                                    }
                                                    if (invdata.sah_tax_exempted == 1)
                                                    {
                                                        seq = "E";
                                                    }
                                                    cusname = newjob + cusname;
                                                    if (invdata.sah_direct == 1)
                                                    {
                                                        if (alltotal != 0)
                                                            writer.WriteLine(PrintSunLineInv(Cuscode, TOTNEW2.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode, newjob));
                                                    }
                                                    else if (invdata.sah_direct == 0 && invdata.sah_anal_4 == "DISC")
                                                    {
                                                        jnaltype = "REC1";
                                                        cusname = invdata.sah_ref_doc.ToString();
                                                        if (alltotal != 0)
                                                            writer.WriteLine(PrintSunLineInv(Cuscode, TOTNEW2.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode, newjob));
                                                    }
                                                    else
                                                    {

                                                        if (invdata.isdiliver == "0" && invdata.sah_man_ref.Contains("SRN") == false)
                                                        {
                                                            //cancel srn 2016-11-19
                                                            string refNo1 = invdata.sah_inv_no.Replace(@"-", string.Empty);
                                                            refNo1 = refNo1.Replace(@"REV", string.Empty);
                                                            int srnlenght = refNo1.Length;
                                                            if (srnlenght > 10)
                                                            {
                                                                refNo = refNo1.Substring(0, 10);
                                                            }
                                                            else
                                                            {
                                                                refNo = refNo1;
                                                            }
                                                            cusname = invdata.sah_ref_doc.ToString();
                                                        }
                                                        else
                                                        {

                                                            cusname = invdata.sah_man_ref.ToString();
                                                            if (cusname == "" || cusname == "N/A" || (cusname.Contains("SRN") == false))
                                                            {
                                                                cusname = invdata.sah_ref_doc.ToString();
                                                            }
                                                        }

                                                        jnaltype = "SRTN1";
                                                        if (alltotal != 0)
                                                            writer.WriteLine(PrintSunLineInv(Cuscode, TOTNEW2.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode, newjob));

                                                       
                                                    }

                                                    paytpcount++;
                                                    DUTY = gnr.ledg_desc;
                                                }
#endregion 
                                                #region vat
                                                if (paytype == gnrtype && gnr.ledg_sub_tp == "VAT" && gnr.ledg_acc_tp == "CR" && gnr.ledg_pc == invdata.sah_pc && (gnr.ledg_desc == "CASH" | gnr.ledg_desc == "CRED") && gnr.ledg_jnl_tp == "15" && gnr.ledg_jnl_desc == invdata.SerCode)
                                                {
                                                   
                                                    Acccode = gnr.ledg_acc_cd.ToString();
                                                    string TOTAmmount = invdata.total.ToString("F3", CultureInfo.InvariantCulture);
                                                    string TAXAmmount = invdata.taxtotal.ToString("F3", CultureInfo.InvariantCulture);
                                                    string TOTunit = invdata.totalunit.ToString("F3", CultureInfo.InvariantCulture);
                                                    DateTime transdate = invdata.sah_dt;
                                                    string refNo = invdata.sah_inv_no.ToString();
                                                    string cusname = invdata.sah_cus_name.ToString().Trim();
                                                    string pc = invdata.sah_pc.ToString();
                                                    string seq = invdata.tax_cd.ToString();
                                                    jnaltype = "INV"; //gnr.ledg_jnl_tp.ToString();
                                                    if (seq == "" | seq == "M" | seq == "N") seq = "O";
                                                    if (jnaltype == "") jnaltype = "INV1";

                                                    if (invdata.sah_is_svat == 1) seq = invdata.svatcd.ToString();

                                                    if (gnr.ledg_desc == "DUTY")
                                                    {
                                                        jnaltype = "INV8";
                                                        seq = "O";
                                                    }
                                                    //split ref

                                                    string[] words = refNo.Split('-');
                                                    if (words.Length > 3)
                                                    {

                                                        refNo = words[0] + words[1] + words[3];

                                                    }
                                                    if (invdata.sah_direct == 0)
                                                    {
                                                        int invlenght = invdata.sah_inv_no.Length;
                                                        refNo = invdata.sah_ref_doc.ToString();
                                                        if (invdata.sah_anal_4 == "DISC")
                                                        {
                                                            refNo = invdata.sah_inv_no.ToString();
                                                        }
                                                        string[] words2 = refNo.Split('-');
                                                        if (words2.Length == 2)
                                                        {
                                                            if (invdata.sah_anal_4 == "DISC")
                                                            {
                                                                refNo = invdata.sah_pc.ToString() + words2[1];
                                                            }
                                                            else
                                                            {
                                                                refNo = "Z" + words2[0] + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                            }
                                                        }
                                                        else if (words2.Length == 3)
                                                        {
                                                            if (invdata.sah_anal_4 == "DISC")
                                                            {
                                                                refNo = invdata.sah_pc.ToString() + words2[2];
                                                            }
                                                            else
                                                            {

                                                                refNo = "Z" + words2[1] + invdata.sah_inv_no.Substring(invlenght - 5, 5);

                                                            }

                                                        }
                                                        else
                                                        {
                                                            if (invdata.sah_anal_4 == "DISC")
                                                            {
                                                                refNo = invdata.sah_pc.ToString() + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                            }
                                                            else
                                                            {
                                                                refNo = "Z" + refNo.Substring(0, 4) + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                            }

                                                        }
                                                    }
                                                    if (refNo.Length > 10)
                                                    {
                                                        refNo = refNo.Replace(@"-", string.Empty);
                                                    }

                                                    //CHECK T4

                                                    DataTable _dt = CHNLSVC.Sales.CheckSUNLC(company, "T4", newjob);
                                                    if (_dt != null && _dt.Rows.Count > 0)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        savejobnosun(company, "T4", newjob, DateTime.Now.Date, cusname);
                                                    }

                                                    if (invdata.sah_tax_exempted == 1)
                                                    {
                                                        seq = "E";
                                                    }
                                                   
                                                    if (invdata.sah_direct == 1)
                                                    {
                                                            if (invdata.NBTValue == 0)
                                                            {

                                                                //chck nbt, doc .....
                                                                List<SUNINVHDR> LIORDERSUM1 = LIORDERSUM.Where(a => a.sah_inv_no == invdata.sah_inv_no).ToList();

                                                                foreach (var _vat in LIORDERSUM1)
                                                                {
                                                                    string _eleacc = CHNLSVC.Sales.GetEleAccount(_vat.sad_itm_cd, "REV");
                                                                    if (_vat.sad_itm_cd == "VAT")
                                                                    {

                                                                       // check svat customer
                                                                        List<cus_details> _cus = CHNLSVC.General.GetCustormerdata(company,invdata.sah_cus_cd.ToString());
                                                                        if (_cus != null && _cus.Count>0)
                                                                        {
                                                                                if (_cus.First().MBE_IS_SVAT==true)
                                                                                {
                                                                                    Acccode = CHNLSVC.Sales.GetEleAccount("SVAT", "INV");
                                                                                }
                                                                        }

                                                                        string _tot = _vat.totalunit.ToString("F3", CultureInfo.InvariantCulture);
                                                                        if (_vat.totalunit != 0)
                                                                            writer.WriteLine(PrintSunLineInv(Acccode, _tot.ToString(), "C", transdate, refNo, newjob + _vat.tid_cha_desc + invdata.sah_cus_cd.ToString(), pc, seq, jnaltype, execcode, newjob));
                                                                    }
                                                                }


                                                               // writer.WriteLine(PrintSunLine(Acccode, TAXAmmount.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                            }
                                                            else
                                                            {
                                                                decimal itemtottax = invdata.taxtotal;
                                                                decimal nbtvalue = invdata.NBTValue;

                                                                decimal realtax = itemtottax - nbtvalue;
                                                                string strnbt = nbtvalue.ToString("F3", CultureInfo.InvariantCulture);
                                                                string strrealtax = realtax.ToString("F3", CultureInfo.InvariantCulture);

                                                                string _nbtacc = CHNLSVC.Sales.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "NBT");
                                                                if (_nbtacc == "")
                                                                {
                                                                    _nbtacc = Acccode;
                                                                }

                                                              //  writer.WriteLine(PrintSunLine(_nbtacc, strnbt.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                              //  writer.WriteLine(PrintSunLine(Acccode, strrealtax.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                            }

                                                    }
                                                    else
                                                    {
                                                        if (invdata.isdiliver == "0" && invdata.sah_man_ref.Contains("SRN") == false)
                                                        {
                                                            //cancel srn 2016-11-19
                                                            string refNo1 = invdata.sah_inv_no.Replace(@"-", string.Empty);
                                                            refNo1 = refNo1.Replace(@"REV", string.Empty);
                                                            int srnlenght = refNo1.Length;
                                                            if (srnlenght > 10)
                                                            {
                                                                refNo = refNo1.Substring(0, 10);
                                                            }
                                                            else
                                                            {
                                                                refNo = refNo1;
                                                            }
                                                            cusname = invdata.sah_ref_doc.ToString();
                                                        }
                                                        else
                                                        {

                                                            cusname = invdata.sah_man_ref.ToString();
                                                            if (cusname == "" || cusname == "N/A" || (cusname.Contains("SRN") == false))
                                                            {
                                                                cusname = invdata.sah_ref_doc.ToString();
                                                            }
                                                        }
                                                        jnaltype = "SRTN1";

                                                        string _nbtacc = CHNLSVC.Sales.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "VATREV");
                                                        if (_nbtacc == "")
                                                        {
                                                            _nbtacc = Acccode;
                                                        }
                                                       // if (invdata.taxtotal != 0)
                                                           // writer.WriteLine(PrintSunLine(_nbtacc, TAXAmmount.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                    }

                                                    paytpcount++;

                                                }
                                                #endregion
                                                if (paytype == gnrtype && gnr.ledg_sub_tp == "SA" && gnr.ledg_acc_tp == "CR" && gnr.ledg_pc == invdata.sah_pc && (gnr.ledg_desc == "CASH" | gnr.ledg_desc == "CRED") && gnr.ledg_jnl_tp == "15" && gnr.ledg_jnl_desc == invdata.SerCode)
                                                {
                                                    Acccode = gnr.ledg_acc_cd.ToString();
                                                    string TOTAmmount = invdata.total.ToString("F3", CultureInfo.InvariantCulture);
                                                    string TAXAmmount = invdata.taxtotal.ToString("F3", CultureInfo.InvariantCulture);

                                                    decimal realtax = invdata.taxtotal;
                                                    decimal realvalue = invdata.RealTotalwithtax - realtax;
                                                    if (invdata.sah_inv_no == "LGT1712IMPS0039")
                                                    {

                                                    }
                                                    string TOTunit = realvalue.ToString("F3", CultureInfo.InvariantCulture); //invdata.totalunit.ToString("F3", CultureInfo.InvariantCulture);
                                                    DateTime transdate = invdata.sah_dt;
                                                    string refNo = invdata.sah_inv_no.ToString();
                                                    string cusname = invdata.sah_cus_name.ToString().Trim();
                                                    string pc = invdata.sah_pc.ToString();
                                                    string seq = invdata.tax_cd.ToString();
                                                    jnaltype = "INV"; //gnr.ledg_jnl_tp.ToString();
                                                    if (seq == "" | seq == "M" | seq == "N") seq = "O";
                                                    if (jnaltype == "") jnaltype = "INV1";

                                                    if (invdata.sah_is_svat == 1) seq = invdata.svatcd.ToString();

                                                    if (gnr.ledg_desc == "DUTY")
                                                    {
                                                        jnaltype = "INV8";
                                                        seq = "O";
                                                    }

                                                    //split ref

                                                    string[] words = refNo.Split('-');
                                                    if (words.Length > 2)
                                                    {
                                                        refNo = words[0] + words[1] + words[3];

                                                    }
                                                    if (invdata.sah_direct == 0)
                                                    {
                                                        int invlenght = invdata.sah_inv_no.Length;
                                                        refNo = invdata.sah_ref_doc.ToString();
                                                        if (invdata.sah_anal_4 == "DISC")
                                                        {
                                                            refNo = invdata.sah_inv_no.ToString();
                                                        }
                                                        string[] words2 = refNo.Split('-');
                                                        if (words2.Length == 2)
                                                        {
                                                            if (invdata.sah_anal_4 == "DISC")
                                                            {
                                                                refNo = invdata.sah_pc.ToString() + words2[1];
                                                            }
                                                            else
                                                            {
                                                                refNo = "Z" + words2[0] + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                            }
                                                        }
                                                        else if (words2.Length == 3)
                                                        {
                                                            if (invdata.sah_anal_4 == "DISC")
                                                            {
                                                                refNo = invdata.sah_pc.ToString() + words2[2];
                                                            }
                                                            else
                                                            {

                                                                refNo = "Z" + words2[1] + invdata.sah_inv_no.Substring(invlenght - 5, 5);

                                                            }

                                                        }
                                                        else
                                                        {
                                                            if (invdata.sah_anal_4 == "DISC")
                                                            {
                                                                refNo = invdata.sah_pc.ToString() + "CRN" + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                            }
                                                            else
                                                            {
                                                                refNo = "Z" + refNo.Substring(0, 4) + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                            }

                                                        }
                                                    }
                                                    if (refNo.Length > 10)
                                                    {
                                                        refNo = refNo.Replace(@"-", string.Empty);
                                                    }
                                                    if (invdata.sah_tax_exempted == 1)
                                                    {
                                                        seq = "E";
                                                    }

                                                    //CHECK T4

                                                    DataTable _dt = CHNLSVC.Sales.CheckSUNLC(company, "T4", newjob);
                                                    if (_dt != null && _dt.Rows.Count > 0)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        savejobnosun(company, "T4", newjob, DateTime.Now.Date, cusname);
                                                    }

                                                    if (invdata.sah_direct == 1)
                                                    {
                                                        //chck nbt, doc .....
                                                        List<SUNINVHDR> LIORDERSUM1 = LIORDERSUM.Where(a => a.sah_inv_no == invdata.sah_inv_no).ToList();
                                                        
                                                        foreach (var _vat in LIORDERSUM1)
                                                        {
                                                            string _eleacc = CHNLSVC.Sales.GetEleAccount(_vat.sad_itm_cd, "REV");
                                                            if (_eleacc != "" )
                                                            {
                                                                realvalue = realvalue - _vat.totalunit;
                                                                string _tot = _vat.totalunit.ToString("F3", CultureInfo.InvariantCulture);
                                                                if (_vat.totalunit != 0 && _vat.sad_itm_cd != "VAT")
                                                                {
                                                                    if (_vat.sad_itm_cd == "NBT" || _vat.sad_itm_cd == "SVAT" || _vat.sad_itm_cd == "COD")
                                                                    {
                                                                        writer.WriteLine(PrintSunLineInv(_eleacc, _tot.ToString(), "C", transdate, refNo, newjob+_vat.tid_cha_desc, pc, seq, jnaltype, execcode, newjob));
                                                                    }
                                                                    else
                                                                    {
                                                                        //checknbtvat
                                                                        bool _chkdualtax = CHNLSVC.Sales.CheckNBTVAT(company, invdata.sah_pc, _vat.sad_itm_cd);
                                                                        string _cusacc = CHNLSVC.Sales.GetAccountCodeByTp(company, invdata.sah_pc);
                                                                        if (_cusacc != "" && _chkdualtax)
                                                                        {

                                                                            writer.WriteLine(PrintSunLineInv(_cusacc, _tot.ToString(), "C", transdate, refNo, newjob + _vat.tid_cha_desc, pc, seq, jnaltype, execcode, newjob));
                                                                        }
                                                                        else
                                                                        {
                                                                            writer.WriteLine(PrintSunLineInv(Acccode, _tot.ToString(), "C", transdate, refNo, newjob + _vat.tid_cha_desc, pc, seq, jnaltype, execcode, newjob));
                                                                        }
                                                                    }
                                                                }


                                                                  
                                                            }
                                                        }
                                                        cusname = newjob + cusname;
                                                        string _aftervat = realvalue.ToString("F3", CultureInfo.InvariantCulture);

                                                        if (realvalue != 0)
                                                            writer.WriteLine(PrintSunLineInv(Acccode, _aftervat.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode, newjob));
                                                    }
                                                    else
                                                    {
                                                        if (invdata.isdiliver != "0" || invdata.sah_man_ref.Contains("SRN") == true)
                                                        {


                                                            cusname = invdata.sah_man_ref.ToString();
                                                            if ((cusname == "" || cusname == "N/A" || (cusname.Contains("SRN") == false)))
                                                            {
                                                                cusname = invdata.sah_ref_doc.ToString();
                                                                string _nbtacc = CHNLSVC.Sales.GetAccountCodeByTp(company, "REV");
                                                                if (_nbtacc == "")
                                                                {
                                                                    _nbtacc = Acccode;
                                                                }
                                                                if (invdata.totalunit != 0)
                                                                    writer.WriteLine(PrintSunLineInv(_nbtacc, TOTunit.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode, newjob));
                                                            }
                                                            else
                                                            {
                                                                jnaltype = "SRTN1";
                                                                string _nbtacc = CHNLSVC.Sales.GetAccountCodeByTp(company, "SRN");
                                                                if (_nbtacc == "")
                                                                {
                                                                    _nbtacc = Acccode;
                                                                }
                                                                if (invdata.totalunit != 0)
                                                                    writer.WriteLine(PrintSunLineInv(_nbtacc, TOTunit.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode, newjob));
                                                            }

                                                        }
                                                        else if (((invdata.isdiliver == "0" && gnr.ledg_desc == "CASH") || (invdata.sah_man_ref.Contains("SRN") == false && gnr.ledg_desc == "CASH")) && invdata.sah_anal_4 != "DISC")
                                                        {
                                                            //cancel srn 2016-11-19
                                                            string refNo1 = invdata.sah_inv_no.Replace(@"-", string.Empty);
                                                            refNo1 = refNo1.Replace(@"REV", string.Empty);
                                                            int srnlenght = refNo1.Length;
                                                            if (srnlenght > 10)
                                                            {
                                                                refNo = refNo1.Substring(0, 10);
                                                            }
                                                            else
                                                            {
                                                                refNo = refNo1;
                                                            }
                                                            cusname = invdata.sah_ref_doc.ToString();
                                                            jnaltype = "SRTN1";

                                                            string _nbtacc = CHNLSVC.Sales.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "REVCASH");
                                                            if (_nbtacc == "")
                                                            {
                                                                _nbtacc = Acccode;
                                                            }
                                                            if (invdata.totalunit != 0)
                                                                writer.WriteLine(PrintSunLineInv(_nbtacc, TOTunit.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode, newjob));
                                                        }
                                                        else if (invdata.sah_direct == 0 && invdata.sah_anal_4 == "DISC")
                                                        {
                                                            jnaltype = "REC1";
                                                            cusname = invdata.sah_ref_doc.ToString();

                                                            string _nbtacc = CHNLSVC.Sales.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "DISC");
                                                            if (_nbtacc == "")
                                                            {
                                                                _nbtacc = Acccode;
                                                            }
                                                            if (invdata.totalunit != 0)
                                                                writer.WriteLine(PrintSunLineInv(_nbtacc, TOTunit.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode, newjob));
                                                        }
                                                        else
                                                        {
                                                            //cancel srn 2016-11-19
                                                            string refNo1 = invdata.sah_inv_no.Replace(@"-", string.Empty);
                                                            refNo1 = refNo1.Replace(@"REV", string.Empty);
                                                            int srnlenght = refNo1.Length;
                                                            if (srnlenght > 10)
                                                            {
                                                                refNo = refNo1.Substring(0, 10);
                                                            }
                                                            else
                                                            {
                                                                refNo = refNo1;
                                                            }
                                                            cusname = invdata.sah_ref_doc.ToString();
                                                            jnaltype = "SRTN1";
                                                            string _nbtacc = CHNLSVC.Sales.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "REV");
                                                            if (_nbtacc == "")
                                                            {
                                                                _nbtacc = Acccode;
                                                            }
                                                            if (invdata.totalunit != 0)
                                                                writer.WriteLine(PrintSunLineInv(_nbtacc, TOTunit.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode, newjob));
                                                        }

                                                    }
                                                    paytpcount++;
                                                    DUTY = gnr.ledg_desc;
                                                }
                                            }
                                            if (paytpcount < 2 && DUTY != "DUTY" && paytype != "DEBT")
                                            {
                                                return Json(new { success = false, login = true, msg = "Not Define " + paytype + " Type " + invdata.sah_pc.ToString() + " :" + invdata.SerCode }, JsonRequestBehavior.AllowGet);
                                            }
                                           else if (paytpcount > 3)
                                            {
                                                return Json(new { success = false, login = true, msg = "Not Define " + paytype + " Type " + invdata.sah_pc.ToString() }, JsonRequestBehavior.AllowGet);

                                            }
                                           else if (count == 0)
                                            {
                                                return Json(new { success = false, login = true, msg = "Not Define " + paytype + " Type " + invdata.sah_pc.ToString() }, JsonRequestBehavior.AllowGet);
                                            }
                                           
    
                                            int effect = CHNLSVC.Sales.UPDATE_INV_HDRENGLOG(invdata.sah_inv_no, 1, Session["UserCompanyCode"].ToString());

                                        }
                                    }
                                }
                            }
                        }
                        string _PATH = CHNLSVC.Sales.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "PATHINV");
                        System.IO.File.Copy(@name, _PATH +userId+ "INV" + date, true);
                        //txtpath = _PATH + "INV" + date;
                        txtpath =userId+ "INV" + date;
                    }
                    #endregion
                    #region Reciept Upload
                    if (Dtype == "Reciept")
                    {
                        FileInfo info = new FileInfo(name2);
                        if (info.Exists || !info.Exists)
                        {
                            using (StreamWriter writer = info.CreateText())
                            {
                                List<SUNRECIEPTHDR> listorder = CHNLSVC.Sales.GetSunRecieptdatanew(company, _pcstrng, fdate, tdate, "DEBT");
                                if (listorder == null)
                                {
                                    return Json(new { success = false, login = true, data = "", msg = "No data found", Type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    listorder = listorder.GroupBy(l => new { l.sar_receipt_no })
                            .Select(cl => new SUNRECIEPTHDR
                            {
                                sar_receipt_no = cl.First().sar_receipt_no,
                                sar_receipt_type = cl.First().sar_receipt_type,
                                sar_seq_no = cl.First().sar_seq_no,
                                sar_profit_center_cd = cl.First().sar_profit_center_cd,
                                sar_debtor_name = cl.First().sar_debtor_name,
                                sar_receipt_date = cl.First().sar_receipt_date,
                                sar_debtor_cd = cl.First().sar_debtor_cd,
                                sard_ref_no = cl.First().sard_ref_no,
                                sar_tot_settle_amt = cl.Sum(a => a.sar_tot_settle_amt),
                                checkno = cl.First().checkno,
                                sar_remarks = cl.First().sar_remarks,
                                RecieptType = cl.First().RecieptType,
                                sard_chq_bank_cd = cl.First().sard_chq_bank_cd
                            }).ToList();
                                }
                                if (listorder != null)
                                    listorder = listorder.OrderBy(a => a.sar_receipt_type).ToList();
                                if (listorder != null)
                                {
                                    foreach (var invdata in listorder)
                                    {
                                        if (invdata.sar_receipt_no.Contains("SRN") == true || invdata.sar_receipt_no.Contains("ADV") == true)
                                        {
                                        }
                                        else
                                        {
                                            string Cuscode = "";
                                            Acccode = invdata.sard_ref_no.ToString();
                                            if (Acccode.Trim() == "CASH" | Acccode.Trim() == "")
                                            {
                                                string _cusacc = CHNLSVC.Sales.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "RECACCCASH");
                                                Acccode = _cusacc;
                                            }

                                            string TOTAmmount = invdata.sar_tot_settle_amt.ToString("F3", CultureInfo.InvariantCulture);
                                            jnaltype = "REC1";
                                            if (invdata.sar_debtor_cd.ToString() != null)
                                                Cuscode = invdata.sar_debtor_cd.ToString();

                                            if (Cuscode.Trim() == "" | Cuscode.Trim() == "CASH")
                                            {
                                                string _cusacc = CHNLSVC.Sales.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "RECCASH");
                                                Cuscode = _cusacc;
                                            }
                                            DateTime transdate = invdata.sar_receipt_date;
                                            string refNo = invdata.sar_receipt_no.ToString();
                                            string cusname = invdata.checkno.ToString();
                                            string pc = invdata.sar_profit_center_cd.ToString();
                                            string seq = "Z"; //invdata.sar_seq_no.ToString();

                                            //svat
                                            if (invdata.sar_receipt_no.Contains("SVAT"))
                                            {
                                                string _cusacc = CHNLSVC.Sales.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "RECSVAT");
                                                Acccode = _cusacc;
                                                jnaltype = "REC1";
                                                seq = "W";
                                            }
                                            if (invdata.sar_receipt_type == "CASH")
                                            {
                                                cusname = invdata.sard_chq_bank_cd + "-" + "CASH";
                                            }
                                            else if (invdata.sar_receipt_type == "CHEQUE")
                                            {
                                                if (invdata.checkno.Length>6)
                                                {
                                                    invdata.checkno = invdata.checkno.Substring((invdata.checkno.Length - 6));
                                                }
                                                cusname = invdata.sard_chq_bank_cd + "-" + invdata.checkno.ToString();
                                            }
                                            else
                                            {
                                                cusname = invdata.sard_chq_bank_cd + "-" + invdata.RecieptType;
                                            }
                                            string[] words = refNo.Split('-');
                                            if (words.Length > 2)
                                            {
                                                refNo = words[0] + words[1] + words[3];
                                            }


                                            if (invdata.sar_receipt_no.Contains("ORC"))
                                            {
                                                cusname = invdata.sar_remarks.ToString();
                                            }
                                            if (invdata.RecieptType == "RTCHQ")
                                            {
                                                int reflenght = cusname.Length;
                                                if (reflenght >= 6)
                                                {
                                                    refNo = cusname.Substring(reflenght - 6);
                                                }
                                                else
                                                {
                                                    refNo = cusname;
                                                }

                                                writer.WriteLine(PrintSunLine(Cuscode, TOTAmmount.ToString(), "D", transdate, "CHQ-" + refNo, refNo, pc, seq, "RTN1", execcode));
                                                writer.WriteLine(PrintSunLine(Acccode, TOTAmmount.ToString(), "C", transdate, "CHQ-" + refNo, refNo, pc, seq, "RTN1", execcode));
                                            }
                                            else
                                            {
                                                writer.WriteLine(PrintSunLine(Cuscode, TOTAmmount.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                writer.WriteLine(PrintSunLine(Acccode, TOTAmmount.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                            }


                                                int effect = CHNLSVC.Sales.UPDATE_RECIEPT_HDRENGLOG(invdata.sar_receipt_no, 1, Session["UserCompanyCode"].ToString());
                                        }
                                    }
                                }
                            }
                        }
                        string _PATH = CHNLSVC.Sales.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "PATHREC");
                        System.IO.File.Copy(@name2, _PATH + userId + "REC" + date, true);
                        //txtpath = _PATH + "REC" + date;
                        txtpath =userId+ "REC" + date;
                    }
                    #endregion
                    #region Settlement Upload
                    if (Dtype == "Settlement")
                    {
                        FileInfo info = new FileInfo(name3);
                        if (info.Exists || !info.Exists)
                        {
                            using (StreamWriter writer = info.CreateText())
                            {
                                List<PetyCashUpload> _list = CHNLSVC.Sales.GetSunPetyCash(company, fdate, tdate);
                                if (_list == null)
                                {
                                    return Json(new { success = false, login = true, data = "", msg = "No data found", Type = "Info" }, JsonRequestBehavior.AllowGet);
                                }

                                List<PetyCashUpload> _listgrpcd = _list.GroupBy(l => new { l.tprh_req_no, l.Customer })
.Select(cl => new PetyCashUpload
{
    tprh_req_no = cl.First().tprh_req_no,
    tprh_manual_ref = cl.First().tprh_manual_ref,
    tprh_req_dt = cl.First().tprh_req_dt,
    tprh_remarks = cl.First().tprh_remarks,
    tprh_tot_amt = cl.Sum(A => A.tprh_tot_amt),
    tpsh_pc_cd = cl.First().tpsh_pc_cd,
    Customer = cl.First().Customer,
    JobNo = cl.First().JobNo
}).ToList();
                                List<PetyCashUpload> _listgrpreq = _list.GroupBy(l => new { l.tprh_req_no })
                                .Select(cl => new PetyCashUpload
                                {
                                    tprh_req_no = cl.First().tprh_req_no,
                                    tprh_manual_ref = cl.First().tprh_manual_ref,
                                    tprh_req_dt = cl.First().tprh_req_dt,
                                    tprh_remarks = cl.First().tprh_remarks,
                                    tprh_tot_amt = cl.Sum(A => A.tprh_tot_amt),
                                    tpsh_pc_cd = cl.First().tpsh_pc_cd,
                                    Customer = cl.First().Customer,
                                    JobNo = cl.First().JobNo,
                                }).ToList();




                                if (_list != null)
                                {
                                    if (_list.Count > 0)
                                    {
                                        foreach (var _petty in _listgrpreq)
                                        {
                                            string elecode = _petty.Customer;
                                            string Account = "ABBCA3012";
                                            jnaltype = "CRIN";
                                            string TOTAmmount = _petty.tprh_tot_amt.ToString("F3", CultureInfo.InvariantCulture);
                                            DateTime transdate = _petty.tprh_req_dt;
                                            string refNo = _petty.tprh_req_no.ToString();
                                            string[] words = refNo.Split('-');
                                            if (words.Length == 3)
                                            {
                                                refNo = words[0] + words[1] + words[2].Substring(1, 5);

                                            }
                                            else if (words.Length > 3)
                                            {
                                                refNo = words[0] + words[2] + words[3].Substring(1, 5);
                                            }
                                            string pc = _petty.tpsh_pc_cd;
                                            string seq = " ";
                                            string job = _petty.JobNo;
                                            string[] _job = job.Split('/');
                                            string newjob = "";
                                            if (_job.Length > 1)
                                            {
                                                newjob = _job[1].Substring(0, 1) + "/";
                                            }
                                            else
                                            {
                                                newjob = pc + "/";
                                            }

                                          
                                            if (userDefPro == "IMP" || userDefPro == "CLR")
                                            {
                                                newjob = newjob + "I" + "/";
                                            }
                                            else
                                            {
                                                newjob = newjob + "I" + "/";
                                            }

                                            if (_job.Length > 2)
                                            {
                                                if (_job[2].Length > 6)
                                                {
                                                    int sunrefmaxle = _job[2].Length;
                                                    _job[2] = _job[2].Substring(sunrefmaxle - 6);
                                                }

                                                newjob = newjob + _job[2];
                                            }
                                            else
                                            {
                                                newjob = newjob + _job[0];
                                            }
                                           

                                            //CHECK T4
                                            if (newjob.Length > 12)
                                            {
                                                newjob = newjob.Substring(0, 12);
                                            }
                                            DataTable _dt = CHNLSVC.Sales.CheckSUNLC(company, "T4", newjob);
                                            if (_dt != null && _dt.Rows.Count > 0)
                                            {

                                            }
                                            else
                                            {
                                                if (newjob.Length > 15)
                                                {
                                                    newjob = newjob.Substring(0, 15);
                                                }
                                                savejobnosun(company, "T4", newjob, DateTime.Now.Date, elecode);
                                            }

                                            elecode = newjob + " " + elecode;

                                            List<PetyCashUpload> _listgrpcd2 = _listgrpcd.Where(a => a.tprh_req_no == _petty.tprh_req_no).ToList();
                                            foreach (var _elilist in _listgrpcd2)
                                            {
                                                string _eleacc = CHNLSVC.Sales.GetEleAccount(_elilist.Customer, "COS");
                                                string setamt = _elilist.tprh_tot_amt.ToString("F3", CultureInfo.InvariantCulture);
                                                writer.WriteLine(PrintSunLinePetty(_eleacc, setamt.ToString(), "D", transdate, refNo, newjob + _elilist.Customer, pc, seq, jnaltype, execcode, newjob));
                                            }


                                           
                                            writer.WriteLine(PrintSunLinePetty(Account, TOTAmmount.ToString(), "C", transdate, refNo, elecode, pc, seq, jnaltype, execcode, newjob));
                                            int effect = CHNLSVC.Sales.UPDATE_PETTYSETTL(_petty.tprh_req_no, 1, Session["UserCompanyCode"].ToString());
                                        }
                                    }
                                }
                            }
                        }
                        string _PATH = CHNLSVC.Sales.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "PATHPETTY");
                        System.IO.File.Copy(@name3, _PATH + userId + "sett" + date, true);
                        //txtpath = _PATH + "pety" + date;
                        txtpath =userId+ "sett" + date;
                    }
                    #endregion

                    #region TTPayments
                    if (Dtype == "TTPayments")
                    {
                        FileInfo info = new FileInfo(name4);
                        if (info.Exists || !info.Exists)
                        {
                            using (StreamWriter writer = info.CreateText())
                            {
                                List<PetyCashUpload> _list = CHNLSVC.Sales.GetSunPetyCashReq("TTREQ", fdate, tdate, company);
                                if (_list == null)
                                {
                                    return Json(new { success = false, login = true, data = "", msg = "No data found", Type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                                if (_list != null)
                                {
                                    if (_list.Count > 0)
                                    {
                                        foreach (var _petty in _list)
                                        {
                                            string Cuscode = _petty.Customer;
                                            string cusname = "";
                                            List<GET_CUS_BASIC_DATA> Cus_det = CHNLSVC.General.GetCustormerBasicData(_petty.Customer, company, "");
                                            if (Cus_det != null)
                                            {
                                                if (Cus_det.Count > 0)
                                                {
                                                    cusname = Cus_det.First().mbe_name;
                                                }
                                            }
                                            //REF NO
                                            string refNo = _petty.tprh_req_no.ToString();
                                            string[] words = refNo.Split('-');
                                            if (words.Length == 3)
                                            {
                                                refNo = words[0] + words[1] + words[2].Substring(1, 5);

                                            }
                                            else if (words.Length > 3)
                                            {
                                                refNo = words[0] + words[2] + words[3].Substring(1, 5);
                                            }
                                        
                                            string Account = "ABPCO1004";
                                            jnaltype = "CRIN";
                                            string TOTAmmount = _petty.tprh_tot_amt.ToString("F3", CultureInfo.InvariantCulture);
                                            DateTime transdate = _petty.tprh_req_dt;

                                            string pc = _petty.tpsh_pc_cd;// _petty.tpsh_pc_cd;
                                            string seq = " ";
                                            string job = _petty.JobNo;
                                            string[] _job = job.Split('/');
                                            string newjob = "";
                                            if (_job.Length > 1)
                                            {
                                                newjob = _job[1].Substring(0, 1) + "/";
                                            }
                                            else
                                            {
                                                newjob = pc + "/";
                                            }

                                           
                                            if (userDefPro == "IMP" || userDefPro == "CLR")
                                            {
                                                newjob = newjob + "I" + "/";
                                            }
                                            else
                                            {
                                                newjob = newjob + "I" + "/";
                                            }
                                            if (_job.Length > 2)
                                            {
                                                if (_job[2].Length > 6)
                                                {
                                                    int sunrefmaxle = _job[2].Length;
                                                    _job[2] = _job[2].Substring(sunrefmaxle - 6);
                                                }

                                                newjob = newjob + _job[2];
                                            }
                                            else
                                            {
                                                newjob = newjob + _job[0];
                                            }
                                           

                                            //CHECK T4
                                            if (newjob.Length > 12)
                                            {
                                                newjob = newjob.Substring(0, 12);
                                            }
                                            DataTable _dt = CHNLSVC.Sales.CheckSUNLC(company, "T4", newjob);
                                            if (_dt != null && _dt.Rows.Count > 0)
                                            {

                                            }
                                            else
                                            {
                                                if (newjob.Length > 15)
                                                {
                                                    newjob = newjob.Substring(0, 15);
                                                }
                                                savejobnosun(company, "T4", newjob, DateTime.Now.Date, cusname);
                                            }

                                            cusname = newjob + " " + cusname;


                                            writer.WriteLine(PrintSunLinePetty(Account, TOTAmmount.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode, newjob));
                                            writer.WriteLine(PrintSunLinePetty(Cuscode, TOTAmmount.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode, newjob));
                                            int effect = CHNLSVC.Sales.UPDATE_PETTYREQ(_petty.tprh_req_no, 1, Session["UserCompanyCode"].ToString());
                                        }
                                    }
                                }
                            }
                        }
                        string _PATH = CHNLSVC.Sales.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "PATHPETTY");
                        System.IO.File.Copy(@name4, _PATH + userId + "TT" + date, true);
                        //txtpath = _PATH + "pety" + date;
                        txtpath =userId+ "TT" + date;
                    }
                    #endregion
                    #region PettyCash
                    if (Dtype == "PettyCash")
                    {
                        FileInfo info = new FileInfo(name5);
                        if (info.Exists || !info.Exists)
                        {
                            using (StreamWriter writer = info.CreateText())
                            {
                                List<PetyCashUpload> _list = CHNLSVC.Sales.GetSunPetyCashReq("PTCSHREQ", fdate, tdate, company);
                                if (_list == null)
                                {
                                    return Json(new { success = false, login = true, data = "", msg = "No data found", Type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                                if (_list != null)
                                {
                                    if (_list.Count > 0)
                                    {
                                        foreach (var _petty in _list)
                                        {
                                            string Cuscode = "ABBCA3012";
                                            string cusname = "";
                                            List<GET_CUS_BASIC_DATA> Cus_det = CHNLSVC.General.GetCustormerBasicData(_petty.Customer, company, "");
                                            if (Cus_det != null)
                                            {
                                                if (Cus_det.Count > 0)
                                                {
                                                    cusname = Cus_det.First().mbe_name;
                                                }
                                            }
                                            //REF NO
                                            string refNo = _petty.tprh_req_no.ToString();
                                            string[] words = refNo.Split('-');
                                            if (words.Length == 3)
                                            {
                                                refNo = words[0] + words[1] + words[2].Substring(1, 5);

                                            }
                                            else if (words.Length > 3)
                                            {
                                                refNo = words[0] + words[2] + words[3].Substring(1, 5);
                                            }

                                            string Account = "ABBCA3001";
                                            jnaltype = "CRIN";
                                            string TOTAmmount = _petty.tprh_tot_amt.ToString("F3", CultureInfo.InvariantCulture);
                                            DateTime transdate = _petty.tprh_req_dt;


                                            string pc = _petty.tpsh_pc_cd;// _petty.tpsh_pc_cd;
                                            string seq = " ";
                                            string job = _petty.JobNo;
                                            string[] _job = job.Split('/');
                                            string newjob = "";
                                            if (_job.Length>1)
                                            {
                                                newjob = _job[1].Substring(0, 1) + "/";
                                            }
                                            else
                                            {
                                                newjob = pc + "/";
                                            }
                                          
                                            if (userDefPro == "IMP" || userDefPro == "CLR")
                                            {
                                                newjob = newjob + "I"+"/";
                                            }
                                            else
                                            {
                                                newjob = newjob + "I" + "/";
                                            }
                                            if (_job.Length > 2)
                                            {
                                                if (_job[2].Length > 6)
                                                {
                                                    int sunrefmaxle = _job[2].Length;
                                                    _job[2] = _job[2].Substring(sunrefmaxle - 6);
                                                }

                                                newjob = newjob + _job[2];
                                            }
                                            else
                                            {
                                                newjob = newjob + _job[0];
                                            }

                                            if (newjob.Length > 12)
                                            {
                                                newjob = newjob.Substring(0, 12);
                                            }
                                            //CHECK T4

                                            DataTable _dt = CHNLSVC.Sales.CheckSUNLC(company, "T4", newjob);
                                            if (_dt != null && _dt.Rows.Count>0)
                                            {

                                            }
                                            else
                                            {
                                                if (newjob.Length > 12)
                                                {
                                                    newjob = newjob.Substring(0, 12);
                                                }
                                                savejobnosun(company, "T4", newjob, DateTime.Now.Date, cusname);
                                            }

                                            cusname = newjob + " " + cusname;
                                            writer.WriteLine(PrintSunLinePetty(Cuscode, TOTAmmount.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode, newjob));
                                            writer.WriteLine(PrintSunLinePetty(Account, TOTAmmount.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode, newjob));

                                            int effect = CHNLSVC.Sales.UPDATE_PETTYREQ(_petty.tprh_req_no, 1, Session["UserCompanyCode"].ToString());
                                        }
                                    }
                                }
                            }
                        }
                        string _PATH = CHNLSVC.Sales.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "PATHPETTY");
                        System.IO.File.Copy(@name5, _PATH + userId + "Pettych" + date, true);
                        //txtpath = _PATH + "pety" + date;
                        txtpath =userId+ "Pettych" + date;
                    }
                    #endregion
                    #region Petty Payment Req
                    if (Dtype == "Payment")
                    {
                        FileInfo info = new FileInfo(name3);
                        if (info.Exists || !info.Exists)
                        {
                            using (StreamWriter writer = info.CreateText())
                            {
                                List<PetyCashUpload> _list = CHNLSVC.Sales.GetSunPetyCashPaymentReq(company, fdate, tdate);
                                if (_list == null)
                                {
                                    return Json(new { success = false, login = true, data = "", msg = "No data found", Type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                                List<PetyCashUpload> _listgrpcd = _list.GroupBy(l => new { l.tprh_req_no, l.tprh_remarks })
.Select(cl => new PetyCashUpload
{
    tprh_req_no = cl.First().tprh_req_no,
    tprh_manual_ref = cl.First().tprh_manual_ref,
    tprh_req_dt = cl.First().tprh_req_dt,
    tprh_remarks = cl.First().tprh_remarks,
    tprh_tot_amt = cl.Sum(A => A.tprh_tot_amt),
    tpsh_pc_cd = cl.First().tpsh_pc_cd,
    Customer = cl.First().Customer,
    JobNo = cl.First().JobNo,
}).ToList();
                                List<PetyCashUpload> _listgrpreq = _list.GroupBy(l => new { l.tprh_req_no })
                                .Select(cl => new PetyCashUpload
                                {
                                    tprh_req_no = cl.First().tprh_req_no,
                                    tprh_manual_ref = cl.First().tprh_manual_ref,
                                    tprh_req_dt = cl.First().tprh_req_dt,
                                    tprh_remarks = cl.First().tprh_remarks,
                                    tprh_tot_amt = cl.Sum(A => A.tprh_tot_amt),
                                    tpsh_pc_cd = cl.First().tpsh_pc_cd,
                                    Customer = cl.First().Customer,
                                    JobNo = cl.First().JobNo,
                                }).ToList();




                                if (_list != null)
                                {
                                    if (_list.Count > 0)
                                    {
                                        foreach (var _petty in _listgrpreq)
                                        {
                                            string elecode = _petty.tprh_remarks;
                                            string Account = _petty.Customer;
                                            jnaltype = "CRIN";
                                            string TOTAmmount = _petty.tprh_tot_amt.ToString("F3", CultureInfo.InvariantCulture);
                                            DateTime transdate = _petty.tprh_req_dt;

                                            string cusname = "";
                                            List<GET_CUS_BASIC_DATA> Cus_det = CHNLSVC.General.GetCustormerBasicData(_petty.Customer, company, "");
                                            if (Cus_det != null)
                                            {
                                                if (Cus_det.Count > 0)
                                                {
                                                    cusname = Cus_det.First().mbe_name;
                                                }
                                            }

                                            string refNo = _petty.tprh_req_no.ToString();
                                            string[] words = refNo.Split('-');
                                            if (words.Length == 3)
                                            {
                                                refNo = words[0] + words[1] + words[2].Substring(1, 5);

                                            }
                                            else if (words.Length > 3)
                                            {
                                                refNo = words[0] + words[2] + words[3].Substring(1, 5);
                                            }

                                            string pc = _petty.tpsh_pc_cd;
                                            string seq = " ";
                                            string job = _petty.JobNo;
                                            string[] _job = job.Split('/');
                                            string newjob = "";
                                            if (_job.Length > 1)
                                            {
                                                newjob = _job[1].Substring(0, 1) + "/";
                                            }
                                            else
                                            {
                                                newjob = _job[0].Substring(0, 1) + "/"; ;
                                            }

                                            if (userDefPro == "IMP" || userDefPro == "CLR")
                                            {
                                                newjob = newjob + "I" + "/";
                                            }
                                            else
                                            {
                                                newjob = newjob + "I" + "/";
                                            }

                                            if (_job.Length > 2)
                                            {
                                                if (_job[2].Length > 6)
                                                {
                                                    int sunrefmaxle = _job[2].Length;
                                                    _job[2] = _job[2].Substring(sunrefmaxle - 6);
                                                    newjob = newjob + _job[2];
                                                }
                                            }
                                            else
                                            {
                                                newjob = newjob + _job[0];
                                            }


                                            //CHECK T4
                                            if (newjob.Length > 12)
                                            {
                                                newjob = newjob.Substring(0, 12);
                                            }
                                            DataTable _dt = CHNLSVC.Sales.CheckSUNLC(company, "T4", newjob);
                                            if (_dt != null && _dt.Rows.Count > 0)
                                            {

                                            }
                                            else
                                            {
                                                if (newjob.Length > 15)
                                                {
                                                    newjob = newjob.Substring(0, 15);
                                                }

                                                savejobnosun(company, "T4", newjob, DateTime.Now.Date, elecode);
                                            }

                                            elecode = newjob + " " + elecode;

                                            List<PetyCashUpload> _llist = _listgrpcd.Where(a => a.tprh_req_no == _petty.tprh_req_no).ToList();
                                            foreach (var _elilist in _llist)
                                            {
                                                string _eleacc = CHNLSVC.Sales.GetEleAccount(_elilist.tprh_remarks, "COS");
                                                string setamt = _elilist.tprh_tot_amt.ToString("F3", CultureInfo.InvariantCulture);
                                                writer.WriteLine(PrintSunLinePetty(_eleacc, setamt.ToString(), "D", transdate, refNo, newjob + _elilist.tprh_remarks, pc, seq, jnaltype, execcode, newjob));
                                            }



                                            writer.WriteLine(PrintSunLinePetty(Account, TOTAmmount.ToString(), "C", transdate, refNo, newjob + cusname, pc, seq, jnaltype, execcode, newjob));
                                            int effect = CHNLSVC.Sales.UPDATE_PETTYREQ(_petty.tprh_req_no, 1, Session["UserCompanyCode"].ToString());
                                        }
                                    }
                                }
                            }
                        }
                        string _PATH = CHNLSVC.Sales.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "PATHPETTY");
                        System.IO.File.Copy(@name3, _PATH + userId + "payreq" + date, true);
                        //txtpath = _PATH + "pety" + date;
                        txtpath = userId + "payreq" + date;
                    }
                    #endregion 
                    #region Container
                    if (Dtype == "Container")
                    {
                        FileInfo info = new FileInfo(name2);
                        if (info.Exists || !info.Exists)
                        {
                            using (StreamWriter writer = info.CreateText())
                            {
                                List<SUNRECIEPTHDR> listorder = CHNLSVC.Sales.GetSunRecieptdatanew(company, _pcstrng, fdate, tdate, "AD-HOC");
                                if (listorder == null)
                                {
                                    return Json(new { success = false, login = true, data = "", msg = "No data found", Type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    listorder = listorder.GroupBy(l => new { l.sar_receipt_no })
                            .Select(cl => new SUNRECIEPTHDR
                            {
                                sar_receipt_no = cl.First().sar_receipt_no,
                                sar_receipt_type = cl.First().sar_receipt_type,
                                sar_seq_no = cl.First().sar_seq_no,
                                sar_profit_center_cd = cl.First().sar_profit_center_cd,
                                sar_debtor_name = cl.First().sar_debtor_name,
                                sar_receipt_date = cl.First().sar_receipt_date,
                                sar_debtor_cd = cl.First().sar_debtor_cd,
                                sard_ref_no = cl.First().sard_ref_no,
                                sar_tot_settle_amt = cl.Sum(a => a.sar_tot_settle_amt),
                                checkno = cl.First().checkno,
                                sar_remarks = cl.First().sar_remarks,
                                RecieptType = cl.First().RecieptType,
                                sard_chq_bank_cd = cl.First().sard_chq_bank_cd
                            }).ToList();
                                }
                                if (listorder != null)
                                    listorder = listorder.OrderBy(a => a.sar_receipt_type).ToList();
                                if (listorder != null)
                                {
                                    foreach (var invdata in listorder)
                                    {
                                        if (invdata.sar_receipt_no.Contains("SRN") == true || invdata.sar_receipt_no.Contains("ADV") == true)
                                        {
                                        }
                                        else
                                        {
                                            string Cuscode = "";
                                            Acccode = invdata.sard_ref_no.ToString();
                                            if (Acccode.Trim() == "CASH" | Acccode.Trim() == "")
                                            {
                                                string _cusacc = CHNLSVC.Sales.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "RECACCCASH");
                                                Acccode = _cusacc;
                                            }

                                            string TOTAmmount = invdata.sar_tot_settle_amt.ToString("F3", CultureInfo.InvariantCulture);
                                            jnaltype = "REC1";
                                            if (invdata.sar_debtor_cd.ToString() != null)
                                                Cuscode = invdata.sar_debtor_cd.ToString();

                                            if (Cuscode.Trim() == "" | Cuscode.Trim() == "CASH")
                                            {
                                                string _cusacc = CHNLSVC.Sales.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "RECCASH");
                                                Cuscode = _cusacc;
                                            }
                                            DateTime transdate = invdata.sar_receipt_date;
                                            string refNo = invdata.sar_receipt_no.ToString();
                                            string cusname = invdata.checkno.ToString();
                                            string pc = invdata.sar_profit_center_cd.ToString();
                                            string seq = "Z"; //invdata.sar_seq_no.ToString();

                                            //svat
                                            if (invdata.sar_receipt_no.Contains("SVAT"))
                                            {
                                                string _cusacc = CHNLSVC.Sales.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "RECSVAT");
                                                Acccode = _cusacc;
                                                jnaltype = "REC1";
                                                seq = "W";
                                            }

                                            if (invdata.sar_receipt_type == "CASH")
                                            {
                                                cusname = invdata.sard_chq_bank_cd + "-" + "CASH";
                                            }
                                            else if (invdata.sar_receipt_type == "CHEQUE")
                                            {
                                                if (invdata.checkno.Length > 6)
                                                {
                                                    invdata.checkno = invdata.checkno.Substring((invdata.checkno.Length - 6));
                                                }
                                                cusname = invdata.sard_chq_bank_cd + "-" + invdata.checkno.ToString();
                                            }
                                            else
                                            {
                                                cusname = invdata.sard_chq_bank_cd + "-" + invdata.RecieptType;
                                            }
                                            string[] words = refNo.Split('-');
                                            if (words.Length > 3)
                                            {
                                                refNo = words[0] + words[1] + words[3];
                                            }

                                            if (invdata.sar_receipt_no.Contains("ORC"))
                                            {
                                                cusname = invdata.sar_remarks.ToString();
                                            }
                                            if (invdata.RecieptType == "RTCHQ")
                                            {
                                                int reflenght = cusname.Length;
                                                if (reflenght >= 6)
                                                {
                                                    refNo = cusname.Substring(reflenght - 6);
                                                }
                                                else
                                                {
                                                    refNo = cusname;
                                                }

                                                writer.WriteLine(PrintSunLine(Cuscode, TOTAmmount.ToString(), "D", transdate, "CHQ-" + refNo, refNo, pc, seq, "RTN1", execcode));
                                                writer.WriteLine(PrintSunLine(Acccode, TOTAmmount.ToString(), "C", transdate, "CHQ-" + refNo, refNo, pc, seq, "RTN1", execcode));
                                            }
                                            else
                                            {
                                                writer.WriteLine(PrintSunLine(Cuscode, TOTAmmount.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                writer.WriteLine(PrintSunLine(Acccode, TOTAmmount.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                            }


                                            int effect = CHNLSVC.Sales.UPDATE_RECIEPT_HDRENGLOG(invdata.sar_receipt_no, 1, Session["UserCompanyCode"].ToString());
                                        }
                                    }
                                }
                            }
                        }
                        string _PATH = CHNLSVC.Sales.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "PATHREC");
                        System.IO.File.Copy(@name2, _PATH + userId + "Cont" + date, true);
                        //txtpath = _PATH + "REC" + date;
                        txtpath =userId+ "Cont" + date;
                    }
                    #endregion
                    #region Creditnote Upload
                    if (Dtype == "CreditNote")
                    {
                        FileInfo info = new FileInfo(name);
                        if (info.Exists || !info.Exists)
                        {
                            using (StreamWriter writer = info.CreateText())
                            {
                                List<SUN_JURNAL> gnrlist = CHNLSVC.Sales.GetSunJurnalnew(company);
                                gnrlist = gnrlist.OrderBy(a => a.ledg_sales_tp).ToList();
                                List<SUNINVHDR> LIORDERSUM = new List<SUNINVHDR>();
                                List<SUNINVHDR> listorder = CHNLSVC.Sales.GetSunInvdatanewRev(company, _pcstrng, fdate, tdate);
                                if (listorder == null)
                                {
                                    return Json(new { success = false, login = true, data = "", msg = "No data found", Type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    LIORDERSUM = listorder.GroupBy(l => new { l.sah_inv_no, l.sad_itm_cd })
     .Select(cl => new SUNINVHDR
     {
         sah_inv_no = cl.First().sah_inv_no,
         sah_inv_tp = cl.First().sah_inv_tp,
         sah_inv_sub_tp = cl.First().sah_inv_sub_tp,
         sah_tp = cl.First().sah_tp,
         total = cl.First().total,
         totalunit = cl.Sum(A => A.totalunit),
         taxtotal = 0,
         sah_seq_no = cl.First().sah_seq_no,
         sah_pc = cl.First().sah_pc,
         sah_cus_name = cl.First().sah_cus_name,
         sah_dt = cl.First().sah_dt,
         sah_cus_cd = cl.First().sah_cus_cd,
         sah_direct = cl.First().sah_direct,
         sah_man_ref = cl.First().sah_man_ref,
         tax_cd = cl.First().tax_cd,
         isdiliver = cl.First().isdiliver,
         sah_ref_doc = cl.First().sah_ref_doc,
         sah_sales_ex_cd = cl.First().sah_sales_ex_cd,
         sah_is_svat = cl.First().sah_is_svat,
         svatcd = cl.First().svatcd,
         EPF = cl.First().EPF,
         CODE = cl.First().CODE,
         RealTotalwithtax = cl.Sum(a => a.RealTotalwithtax),
         NBTValue = cl.Sum(a => a.NBTValue),
         sah_tax_exempted = cl.First().sah_tax_exempted,
         sah_anal_4 = cl.First().sah_anal_4,
         SerCode = cl.First().SerCode,
         sad_itm_cd = cl.First().sad_itm_cd,
         tid_cha_desc = cl.First().tid_cha_desc,
         tih_job_no = cl.First().tih_job_no
     }).ToList();
                                    LIORDERSUM = LIORDERSUM.OrderBy(a => a.sah_inv_no).ToList();
                                    listorder = listorder.GroupBy(l => new { l.sah_inv_no })
        .Select(cl => new SUNINVHDR
        {
            sah_inv_no = cl.First().sah_inv_no,
            sah_inv_tp = cl.First().sah_inv_tp,
            sah_inv_sub_tp = cl.First().sah_inv_sub_tp,
            sah_tp = cl.First().sah_tp,
            total = cl.First().total,
            totalunit = cl.Sum(A => A.totalunit),
            taxtotal = 0,
            sah_seq_no = cl.First().sah_seq_no,
            sah_pc = cl.First().sah_pc,
            sah_cus_name = cl.First().sah_cus_name,
            sah_dt = cl.First().sah_dt,
            sah_cus_cd = cl.First().sah_cus_cd,
            sah_direct = cl.First().sah_direct,
            sah_man_ref = cl.First().sah_man_ref,
            tax_cd = cl.First().tax_cd,
            isdiliver = cl.First().isdiliver,
            sah_ref_doc = cl.First().sah_ref_doc,
            sah_sales_ex_cd = cl.First().sah_sales_ex_cd,
            sah_is_svat = cl.First().sah_is_svat,
            svatcd = cl.First().svatcd,
            EPF = cl.First().EPF,
            CODE = cl.First().CODE,
            RealTotalwithtax = cl.Sum(a => a.RealTotalwithtax),
            NBTValue = cl.Sum(a => a.NBTValue),
            sah_tax_exempted = cl.First().sah_tax_exempted,
            sah_anal_4 = cl.First().sah_anal_4,
            SerCode = cl.First().SerCode,
            sad_itm_cd = cl.First().sad_itm_cd,
            tid_cha_desc = cl.First().tid_cha_desc,
            tih_job_no = cl.First().tih_job_no
        }).ToList();
                                    listorder = listorder.OrderBy(a => a.sah_inv_no).ToList();
                                }
                                string tempinvno = "";
                                if (listorder != null)
                                    listorder = listorder.OrderBy(a => a.sah_inv_tp).ToList();
                                if (listorder != null && gnrlist != null)
                                {
                                    foreach (var invdata in listorder)
                                    {
                                        if (invdata.totalunit != 0)
                                        {
                                            execcode = "N/A";
                                            if (invdata.sah_sales_ex_cd != null) execcode = invdata.sah_sales_ex_cd.ToString();

                                            int paytpcount = 0;
                                            string DUTY = "";
                                            string paytype = "1";
                                            if (invdata.sah_inv_tp.ToString() != null)
                                                paytype = invdata.sah_inv_tp.Trim().ToString();

                                            foreach (var gnr in gnrlist)
                                            {
                                                if (gnr.ledg_is_dealer == null)
                                                {
                                                    gnr.ledg_is_dealer = "";
                                                }
                                                ISTAX = gnr.ledg_is_dealer;
                                                string gnrtype = "2";
                                                if (gnr.ledg_sales_tp != null)
                                                    gnrtype = gnr.ledg_sales_tp.Trim().ToString();

                                                if (paytype == gnrtype)
                                                    count = 1;
                                                if (gnr.ledg_pc == "IMPS")
                                                {

                                                }


                                                string job = invdata.tih_job_no;
                                                string[] _job = job.Split('/');
                                                string newjob = "";
                                                newjob = _job[1].Substring(0, 1) + "/";
                                                if (invdata.sah_pc == "IMP" || invdata.sah_pc == "CLR")
                                                {
                                                    newjob = newjob + "I" + "/";
                                                }
                                                else if (invdata.sah_pc == "SFW" || invdata.sah_pc == "SXP")
                                                {
                                                    newjob = newjob + "E" + "/";
                                                }
                                                else if (invdata.sah_pc == "AEX" || invdata.sah_pc == "ACL")
                                                {
                                                    newjob = newjob + "E" + "/";
                                                }
                                                else
                                                {
                                                    newjob = newjob + "I" + "/";
                                                }
                                                if (_job[2].Length > 6)
                                                {
                                                    int sunrefmaxle = _job[2].Length;
                                                    _job[2] = _job[2].Substring(sunrefmaxle - 6);
                                                }

                                                newjob = newjob + _job[2];

                                                #region customer
                                                if (paytype == gnrtype && gnr.ledg_sub_tp == "SA" && gnr.ledg_acc_tp == "DR" && gnr.ledg_pc == invdata.sah_pc && (gnr.ledg_desc == "CASH" | gnr.ledg_desc == "CRED") && gnr.ledg_jnl_tp == "15" && gnr.ledg_jnl_desc == invdata.SerCode)
                                                {
                                                    decimal alltotal = 0;
                                                    if (tempinvno != invdata.sah_inv_no)
                                                        alltotal = invdata.total;



                                                    //REVERSISSUE
                                                    tempinvno = invdata.sah_inv_no;
                                                    decimal TOTNEW = Convert.ToDecimal(invdata.totalunit) + Convert.ToDecimal(invdata.taxtotal);
                                                    string TOTNEW2 = alltotal.ToString("F3", CultureInfo.InvariantCulture);
                                                    string Cuscode = "";
                                                    Acccode = gnr.ledg_acc_cd.ToString();
                                                    string TOTAmmount = invdata.total.ToString("F3", CultureInfo.InvariantCulture);
                                                    string TAXAmmount = invdata.taxtotal.ToString("F3", CultureInfo.InvariantCulture);
                                                    string TOTunit = invdata.totalunit.ToString("F3", CultureInfo.InvariantCulture);

                                                    if (invdata.sah_cus_cd != null)
                                                        Cuscode = invdata.sah_cus_cd.ToString();
                                                    DateTime transdate = invdata.sah_dt;
                                                    string refNo = invdata.sah_inv_no.ToString();
                                                    string cusname = invdata.sah_cus_name.ToString().Trim();
                                                    string pc = invdata.sah_pc.ToString();
                                                    string seq = invdata.tax_cd.ToString();
                                                    jnaltype = "INV";//gnr.ledg_jnl_tp.ToString();



                                                    if ((Cuscode.Contains("CONT") || Cuscode.Contains("CASH")))
                                                    {
                                                        string _cusacc = CHNLSVC.Sales.GetAccountCodeByTp(company, "CONTCUS");
                                                        Cuscode = _cusacc;
                                                    }
                                                    if (seq == "" | seq == "M" | seq == "N") seq = "O";
                                                    string[] words = refNo.Split('-');
                                                    if (words.Length > 2)
                                                    {
                                                        refNo = words[0] + words[1] + words[2];
                                                    }


                                                    if (refNo.Length > 10)
                                                    {
                                                        refNo = refNo.Replace(@"-", string.Empty);
                                                    }
                                                    //CHECK T4

                                                    DataTable _dt = CHNLSVC.Sales.CheckSUNLC(company, "T4", newjob);
                                                    if (_dt != null && _dt.Rows.Count > 0)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        savejobnosun(company, "T4", newjob, DateTime.Now.Date, cusname);
                                                    }

                                                    if (invdata.sah_is_svat == 1) seq = invdata.svatcd.ToString();
                                                    if (jnaltype == "") jnaltype = "INV1";
                                                    if (gnr.ledg_desc == "DUTY")
                                                    {
                                                        jnaltype = "INV8";
                                                        seq = "O";
                                                    }
                                                    if (invdata.sah_tax_exempted == 1)
                                                    {
                                                        seq = "E";
                                                    }
                                                    cusname = newjob + cusname;
                                                    if (invdata.sah_direct == 0)
                                                    {
                                                        if (alltotal != 0)
                                                            writer.WriteLine(PrintSunLineInv(Cuscode, TOTNEW2.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode, newjob));
                                                    }
                                                    else if (invdata.sah_direct == 1 && invdata.sah_anal_4 == "DISC")
                                                    {
                                                        jnaltype = "REC1";
                                                        cusname = invdata.sah_ref_doc.ToString();
                                                        if (alltotal != 0)
                                                            writer.WriteLine(PrintSunLineInv(Cuscode, TOTNEW2.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode, newjob));
                                                    }
                                                    else
                                                    {

                                                        if (invdata.isdiliver == "0" && invdata.sah_man_ref.Contains("SRN") == false)
                                                        {
                                                            //cancel srn 2016-11-19
                                                            string refNo1 = invdata.sah_inv_no.Replace(@"-", string.Empty);
                                                            refNo1 = refNo1.Replace(@"REV", string.Empty);
                                                            int srnlenght = refNo1.Length;
                                                            if (srnlenght > 10)
                                                            {
                                                                refNo = refNo1.Substring(0, 10);
                                                            }
                                                            else
                                                            {
                                                                refNo = refNo1;
                                                            }
                                                            cusname = invdata.sah_ref_doc.ToString();
                                                        }
                                                        else
                                                        {

                                                            cusname = invdata.sah_man_ref.ToString();
                                                            if (cusname == "" || cusname == "N/A" || (cusname.Contains("SRN") == false))
                                                            {
                                                                cusname = invdata.sah_ref_doc.ToString();
                                                            }
                                                        }

                                                        jnaltype = "SRTN1";
                                                        if (alltotal != 0)
                                                            writer.WriteLine(PrintSunLineInv(Cuscode, TOTNEW2.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode, newjob));


                                                    }

                                                    paytpcount++;
                                                    DUTY = gnr.ledg_desc;
                                                }
                                                #endregion
                                                #region vat
                                                if (paytype == gnrtype && gnr.ledg_sub_tp == "VAT" && gnr.ledg_acc_tp == "CR" && gnr.ledg_pc == invdata.sah_pc && (gnr.ledg_desc == "CASH" | gnr.ledg_desc == "CRED") && gnr.ledg_jnl_tp == "15" && gnr.ledg_jnl_desc == invdata.SerCode)
                                                {

                                                    Acccode = gnr.ledg_acc_cd.ToString();
                                                    string TOTAmmount = invdata.total.ToString("F3", CultureInfo.InvariantCulture);
                                                    string TAXAmmount = invdata.taxtotal.ToString("F3", CultureInfo.InvariantCulture);
                                                    string TOTunit = invdata.totalunit.ToString("F3", CultureInfo.InvariantCulture);
                                                    DateTime transdate = invdata.sah_dt;
                                                    string refNo = invdata.sah_inv_no.ToString();
                                                    string cusname = invdata.sah_cus_name.ToString().Trim();
                                                    string pc = invdata.sah_pc.ToString();
                                                    string seq = invdata.tax_cd.ToString();
                                                    jnaltype = "INV"; //gnr.ledg_jnl_tp.ToString();
                                                    if (seq == "" | seq == "M" | seq == "N") seq = "O";
                                                    if (jnaltype == "") jnaltype = "INV1";

                                                    if (invdata.sah_is_svat == 1) seq = invdata.svatcd.ToString();

                                                    if (gnr.ledg_desc == "DUTY")
                                                    {
                                                        jnaltype = "INV8";
                                                        seq = "O";
                                                    }
                                                    //split ref

                                                    string[] words = refNo.Split('-');
                                                    if (words.Length > 2)
                                                    {

                                                        refNo = words[0] + words[1] + words[2];

                                                    }
                                                    if (refNo.Length > 10)
                                                    {
                                                        refNo = refNo.Replace(@"-", string.Empty);
                                                    }

                                                    //CHECK T4

                                                    DataTable _dt = CHNLSVC.Sales.CheckSUNLC(company, "T4", newjob);
                                                    if (_dt != null && _dt.Rows.Count > 0)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        savejobnosun(company, "T4", newjob, DateTime.Now.Date, cusname);
                                                    }

                                                    if (invdata.sah_tax_exempted == 1)
                                                    {
                                                        seq = "E";
                                                    }

                                                    if (invdata.sah_direct == 0)
                                                    {
                                                        if (invdata.NBTValue == 0)
                                                        {

                                                            //chck nbt, doc .....
                                                            List<SUNINVHDR> LIORDERSUM1 = LIORDERSUM.Where(a => a.sah_inv_no == invdata.sah_inv_no).ToList();

                                                            foreach (var _vat in LIORDERSUM1)
                                                            {
                                                                string _eleacc = CHNLSVC.Sales.GetEleAccount(_vat.sad_itm_cd, "REV");
                                                                if (_vat.sad_itm_cd == "VAT")
                                                                {

                                                                    // check svat customer
                                                                    List<cus_details> _cus = CHNLSVC.General.GetCustormerdata(company, invdata.sah_cus_cd.ToString());
                                                                    if (_cus != null && _cus.Count > 0)
                                                                    {
                                                                        if (_cus.First().MBE_IS_SVAT == true)
                                                                        {
                                                                            Acccode = CHNLSVC.Sales.GetEleAccount("SVAT", "COS");
                                                                        }
                                                                    }

                                                                    string _tot = _vat.totalunit.ToString("F3", CultureInfo.InvariantCulture);
                                                                    if (_vat.totalunit != 0)
                                                                        writer.WriteLine(PrintSunLineInv(Acccode, _tot.ToString(), "D", transdate, refNo, newjob + _vat.tid_cha_desc + invdata.sah_cus_cd.ToString(), pc, seq, jnaltype, execcode, newjob));
                                                                }
                                                            }


                                                            // writer.WriteLine(PrintSunLine(Acccode, TAXAmmount.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                        }
                                                        else
                                                        {
                                                            decimal itemtottax = invdata.taxtotal;
                                                            decimal nbtvalue = invdata.NBTValue;

                                                            decimal realtax = itemtottax - nbtvalue;
                                                            string strnbt = nbtvalue.ToString("F3", CultureInfo.InvariantCulture);
                                                            string strrealtax = realtax.ToString("F3", CultureInfo.InvariantCulture);

                                                            string _nbtacc = CHNLSVC.Sales.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "NBT");
                                                            if (_nbtacc == "")
                                                            {
                                                                _nbtacc = Acccode;
                                                            }

                                                            //  writer.WriteLine(PrintSunLine(_nbtacc, strnbt.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                            //  writer.WriteLine(PrintSunLine(Acccode, strrealtax.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                        }

                                                    }
                                                    else
                                                    {
                                                        if (invdata.isdiliver == "0" && invdata.sah_man_ref.Contains("SRN") == false)
                                                        {
                                                            //cancel srn 2016-11-19
                                                            string refNo1 = invdata.sah_inv_no.Replace(@"-", string.Empty);
                                                            refNo1 = refNo1.Replace(@"REV", string.Empty);
                                                            int srnlenght = refNo1.Length;
                                                            if (srnlenght > 10)
                                                            {
                                                                refNo = refNo1.Substring(0, 10);
                                                            }
                                                            else
                                                            {
                                                                refNo = refNo1;
                                                            }
                                                            cusname = invdata.sah_ref_doc.ToString();
                                                        }
                                                        else
                                                        {

                                                            cusname = invdata.sah_man_ref.ToString();
                                                            if (cusname == "" || cusname == "N/A" || (cusname.Contains("SRN") == false))
                                                            {
                                                                cusname = invdata.sah_ref_doc.ToString();
                                                            }
                                                        }
                                                        jnaltype = "SRTN1";

                                                        string _nbtacc = CHNLSVC.Sales.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "VATREV");
                                                        if (_nbtacc == "")
                                                        {
                                                            _nbtacc = Acccode;
                                                        }
                                                        // if (invdata.taxtotal != 0)
                                                        // writer.WriteLine(PrintSunLine(_nbtacc, TAXAmmount.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                    }

                                                    paytpcount++;

                                                }
                                                #endregion
                                                if (paytype == gnrtype && gnr.ledg_sub_tp == "SA" && gnr.ledg_acc_tp == "CR" && gnr.ledg_pc == invdata.sah_pc && (gnr.ledg_desc == "CASH" | gnr.ledg_desc == "CRED") && gnr.ledg_jnl_tp == "15" && gnr.ledg_jnl_desc == invdata.SerCode)
                                                {
                                                    Acccode = gnr.ledg_acc_cd.ToString();
                                                    string TOTAmmount = invdata.total.ToString("F3", CultureInfo.InvariantCulture);
                                                    string TAXAmmount = invdata.taxtotal.ToString("F3", CultureInfo.InvariantCulture);

                                                    decimal realtax = invdata.taxtotal;
                                                    decimal realvalue = invdata.RealTotalwithtax - realtax;
                                                    if (invdata.sah_inv_no == "LGT1712IMPS0039")
                                                    {

                                                    }
                                                    string TOTunit = realvalue.ToString("F3", CultureInfo.InvariantCulture); //invdata.totalunit.ToString("F3", CultureInfo.InvariantCulture);
                                                    DateTime transdate = invdata.sah_dt;
                                                    string refNo = invdata.sah_inv_no.ToString();
                                                    string cusname = invdata.sah_cus_name.ToString().Trim();
                                                    string pc = invdata.sah_pc.ToString();
                                                    string seq = invdata.tax_cd.ToString();
                                                    jnaltype = "INV"; //gnr.ledg_jnl_tp.ToString();
                                                    if (seq == "" | seq == "M" | seq == "N") seq = "O";
                                                    if (jnaltype == "") jnaltype = "INV1";

                                                    if (invdata.sah_is_svat == 1) seq = invdata.svatcd.ToString();

                                                    if (gnr.ledg_desc == "DUTY")
                                                    {
                                                        jnaltype = "INV8";
                                                        seq = "O";
                                                    }

                                                    //split ref

                                                    string[] words = refNo.Split('-');
                                                    if (words.Length > 2)
                                                    {
                                                        refNo = words[0] + words[1] + words[2];

                                                    }
                                                    if (refNo.Length > 10)
                                                    {
                                                        refNo = refNo.Replace(@"-", string.Empty);
                                                    }
                                                    if (invdata.sah_tax_exempted == 1)
                                                    {
                                                        seq = "E";
                                                    }

                                                    //CHECK T4

                                                    DataTable _dt = CHNLSVC.Sales.CheckSUNLC(company, "T4", newjob);
                                                    if (_dt != null && _dt.Rows.Count > 0)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        savejobnosun(company, "T4", newjob, DateTime.Now.Date, cusname);
                                                    }

                                                    if (invdata.sah_direct == 0)
                                                    {
                                                        //chck nbt, doc .....
                                                        List<SUNINVHDR> LIORDERSUM1 = LIORDERSUM.Where(a => a.sah_inv_no == invdata.sah_inv_no).ToList();

                                                        foreach (var _vat in LIORDERSUM1)
                                                        {
                                                            string _eleacc = CHNLSVC.Sales.GetEleAccount(_vat.sad_itm_cd, "REV");
                                                            if (_eleacc != "")
                                                            {
                                                                realvalue = realvalue - _vat.totalunit;
                                                                string _tot = _vat.totalunit.ToString("F3", CultureInfo.InvariantCulture);
                                                                if (_vat.totalunit != 0 && _vat.sad_itm_cd != "VAT")
                                                                {
                                                                    if (_vat.sad_itm_cd == "NBT" || _vat.sad_itm_cd == "SVAT" || _vat.sad_itm_cd == "COD")
                                                                    {
                                                                        writer.WriteLine(PrintSunLineInv(_eleacc, _tot.ToString(), "D", transdate, refNo, newjob + _vat.tid_cha_desc, pc, seq, jnaltype, execcode, newjob));
                                                                    }
                                                                    else
                                                                    {
                                                                        //checknbtvat
                                                                        bool _chkdualtax = CHNLSVC.Sales.CheckNBTVAT(company, invdata.sah_pc, _vat.sad_itm_cd);
                                                                        string _cusacc = CHNLSVC.Sales.GetAccountCodeByTp(company, invdata.sah_pc);
                                                                        if (_cusacc != "" && _chkdualtax)
                                                                        {

                                                                            writer.WriteLine(PrintSunLineInv(_cusacc, _tot.ToString(), "D", transdate, refNo, newjob + _vat.tid_cha_desc, pc, seq, jnaltype, execcode, newjob));
                                                                        }
                                                                        else
                                                                        {
                                                                            writer.WriteLine(PrintSunLineInv(Acccode, _tot.ToString(), "D", transdate, refNo, newjob + _vat.tid_cha_desc, pc, seq, jnaltype, execcode, newjob));
                                                                        }
                                                                    }
                                                                }



                                                            }
                                                        }
                                                        cusname = newjob + cusname;
                                                        string _aftervat = realvalue.ToString("F3", CultureInfo.InvariantCulture);

                                                        if (realvalue != 0)
                                                            writer.WriteLine(PrintSunLineInv(Acccode, _aftervat.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode, newjob));
                                                    }
                                                    else
                                                    {
                                                        if (invdata.isdiliver != "0" || invdata.sah_man_ref.Contains("SRN") == true)
                                                        {


                                                            cusname = invdata.sah_man_ref.ToString();
                                                            if ((cusname == "" || cusname == "N/A" || (cusname.Contains("SRN") == false)))
                                                            {
                                                                cusname = invdata.sah_ref_doc.ToString();
                                                                string _nbtacc = CHNLSVC.Sales.GetAccountCodeByTp(company, "REV");
                                                                if (_nbtacc == "")
                                                                {
                                                                    _nbtacc = Acccode;
                                                                }
                                                                if (invdata.totalunit != 0)
                                                                    writer.WriteLine(PrintSunLineInv(_nbtacc, TOTunit.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode, newjob));
                                                            }
                                                            else
                                                            {
                                                                jnaltype = "SRTN1";
                                                                string _nbtacc = CHNLSVC.Sales.GetAccountCodeByTp(company, "SRN");
                                                                if (_nbtacc == "")
                                                                {
                                                                    _nbtacc = Acccode;
                                                                }
                                                                if (invdata.totalunit != 0)
                                                                    writer.WriteLine(PrintSunLineInv(_nbtacc, TOTunit.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode, newjob));
                                                            }

                                                        }
                                                        else if (((invdata.isdiliver == "0" && gnr.ledg_desc == "CASH") || (invdata.sah_man_ref.Contains("SRN") == false && gnr.ledg_desc == "CASH")) && invdata.sah_anal_4 != "DISC")
                                                        {
                                                            //cancel srn 2016-11-19
                                                            string refNo1 = invdata.sah_inv_no.Replace(@"-", string.Empty);
                                                            refNo1 = refNo1.Replace(@"REV", string.Empty);
                                                            int srnlenght = refNo1.Length;
                                                            if (srnlenght > 10)
                                                            {
                                                                refNo = refNo1.Substring(0, 10);
                                                            }
                                                            else
                                                            {
                                                                refNo = refNo1;
                                                            }
                                                            cusname = invdata.sah_ref_doc.ToString();
                                                            jnaltype = "SRTN1";

                                                            string _nbtacc = CHNLSVC.Sales.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "REVCASH");
                                                            if (_nbtacc == "")
                                                            {
                                                                _nbtacc = Acccode;
                                                            }
                                                            if (invdata.totalunit != 0)
                                                                writer.WriteLine(PrintSunLineInv(_nbtacc, TOTunit.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode, newjob));
                                                        }
                                                        else if (invdata.sah_direct == 1 && invdata.sah_anal_4 == "DISC")
                                                        {
                                                            jnaltype = "REC1";
                                                            cusname = invdata.sah_ref_doc.ToString();

                                                            string _nbtacc = CHNLSVC.Sales.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "DISC");
                                                            if (_nbtacc == "")
                                                            {
                                                                _nbtacc = Acccode;
                                                            }
                                                            if (invdata.totalunit != 0)
                                                                writer.WriteLine(PrintSunLineInv(_nbtacc, TOTunit.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode, newjob));
                                                        }
                                                        else
                                                        {
                                                            //cancel srn 2016-11-19
                                                            string refNo1 = invdata.sah_inv_no.Replace(@"-", string.Empty);
                                                            refNo1 = refNo1.Replace(@"REV", string.Empty);
                                                            int srnlenght = refNo1.Length;
                                                            if (srnlenght > 10)
                                                            {
                                                                refNo = refNo1.Substring(0, 10);
                                                            }
                                                            else
                                                            {
                                                                refNo = refNo1;
                                                            }
                                                            cusname = invdata.sah_ref_doc.ToString();
                                                            jnaltype = "SRTN1";
                                                            string _nbtacc = CHNLSVC.Sales.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "REV");
                                                            if (_nbtacc == "")
                                                            {
                                                                _nbtacc = Acccode;
                                                            }
                                                            if (invdata.totalunit != 0)
                                                                writer.WriteLine(PrintSunLineInv(_nbtacc, TOTunit.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode, newjob));
                                                        }

                                                    }
                                                    paytpcount++;
                                                    DUTY = gnr.ledg_desc;
                                                }
                                            }
                                            if (paytpcount < 2 && DUTY != "DUTY" && paytype != "DEBT")
                                            {
                                                return Json(new { success = false, login = true, msg = "Not Define " + paytype + " Type " + invdata.sah_pc.ToString() + " :" + invdata.SerCode }, JsonRequestBehavior.AllowGet);
                                            }
                                            else if (paytpcount > 3)
                                            {
                                                return Json(new { success = false, login = true, msg = "Not Define " + paytype + " Type " + invdata.sah_pc.ToString() }, JsonRequestBehavior.AllowGet);

                                            }
                                            else if (count == 0)
                                            {
                                                return Json(new { success = false, login = true, msg = "Not Define " + paytype + " Type " + invdata.sah_pc.ToString() }, JsonRequestBehavior.AllowGet);
                                            }


                                            int effect = CHNLSVC.Sales.UPDATE_INV_HDRENGLOG(invdata.sah_inv_no, 1, Session["UserCompanyCode"].ToString());

                                        }
                                    }
                                }
                            }
                        }
                        string _PATH = CHNLSVC.Sales.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "PATHINV");
                        System.IO.File.Copy(@name, _PATH + userId + "CRED" + date, true);
                        //txtpath = _PATH + "INV" + date;
                        txtpath =userId+ "CRED" + date;
                    }
                    #endregion
                    if (pcs == "" || Dtype == "SelectType")
                    {
                        //return Json(new { success = false, login = true }, JsonRequestBehavior.AllowGet);
                        return Json(new { success = false, login = true, data = "", msg = "Please select a profit center or Divition Type", Type = "Info" }, JsonRequestBehavior.AllowGet);
                        //return Json(new { success = false, login = true, data = "Genarated fail!!", msg = "Genarated fail!!" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = true, login = true, msg = "Successfully Upload " + " Path: " + txtpath }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = true, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = true, data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        private string PrintSunLine(string AccCode, string Ammount, string AmmountType, DateTime trdate, string refNo, string cusname, string pc, string seq, string jnaltype, string execcode)
        {
            try
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
                string jrtype = jnaltype;
                //jnaltype
                if (jrtype.Length > 5)
                {
                    jrtype.Substring(0, 5);
                }
                else
                {
                    jrtype = jrtype + space5.Substring(0, space5.Length - jrtype.Length);
                }



                string jrscource = "     ";
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
                string oipost = "   ";
                string oilast = "   ";
                string nextperdrev = " ";
                string linktext = " ";
                //Addtional space
                string additinalsp2 = "  ";
                ///////////
                string space15 = "               ";
                string pcnew;
                string annlcd1 = space15;
                string annlcd2 = "N/A            ";// space15;
                string seqnew;
                string annlcd5 = "N/A            "; //space15;
                string annlcd6 = space15;
                string annlcd7 = execcode;
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
                    int sunrefmaxle = refNo.Length;

                    //newrefNo = refNo.Substring(0, 5) + refNo.Substring(refNo.Length - 5);
                    newrefNo = refNo.Substring(sunrefmaxle - 10);

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

                //chenged Vat Cat Z
                //seq = "Z";
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
                    summerdate.ToString("yyyy" + "0" + "MM") + trdate.ToString("yyyyMMdd") +
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
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        private string PrintSunLinePetty(string AccCode, string Ammount, string AmmountType, DateTime trdate, string refNo, string cusname, string pc, string seq, string jnaltype, string execcode, string job)
        {
            try
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
                string jrtype = jnaltype;
                //jnaltype
                if (jrtype.Length > 5)
                {
                    jrtype.Substring(0, 5);
                }
                else
                {
                    jrtype = jrtype + space5.Substring(0, space5.Length - jrtype.Length);
                }



                string jrscource = "     ";
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
                string oipost = "   ";
                string oilast = "   ";
                string nextperdrev = " ";
                string linktext = " ";
                //Addtional space
                string additinalsp2 = "  ";
                ///////////
                string space15 = "               ";
                string pcnew;
                string annlcd1 = space15;
                string annlcd2 = "N/A            ";// space15;
                string seqnew;
                string annlcd5 = "N/A            "; //space15;
                string annlcd6 = space15;
                string annlcd7 = execcode;
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

                if (refNo.Length > 15)
                {
                    int sunrefmaxle = refNo.Length;

                    //newrefNo = refNo.Substring(0, 5) + refNo.Substring(refNo.Length - 5);
                    newrefNo = refNo.Substring(sunrefmaxle - 15);

                }
                else
                {
                    int defref = 15 - refNo.Length;
                    newrefNo = refNo + space15.Substring(0, defref);

                }

                if(job.Length>15)
                {
                    int sunrefmaxle = job.Length;

                    //newrefNo = refNo.Substring(0, 5) + refNo.Substring(refNo.Length - 5);
                    job = job.Substring(sunrefmaxle - 15);
                }
                else
                {
                    int defref = 15 - job.Length;
                    job = job + space15.Substring(0, defref);
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

                //chenged Vat Cat Z
                //seq = "Z";
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
                    summerdate.ToString("yyyy" + "0" + "MM") + trdate.ToString("yyyyMMdd") +
                    space2 +
                    "L" + jrnum + jrlinenum +
                    space2 +
                    ammountnew + AmmountType +
                    allocmak +
                    jrtype +
                    jrscource +
                    newrefNo +
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
                     job +
                     annlcd6 +
                     ann7new +
                     annlcd8 +
                     annlcd9 +
                     blankfinal
                     ;

                return line;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        private string PrintSunLineInv(string AccCode, string Ammount, string AmmountType, DateTime trdate, string refNo, string cusname, string pc, string seq, string jnaltype, string execcode, string job)
        {
            try
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
                string jrtype = jnaltype;
                //jnaltype
                if (jrtype.Length > 5)
                {
                    jrtype.Substring(0, 5);
                }
                else
                {
                    jrtype = jrtype + space5.Substring(0, space5.Length - jrtype.Length);
                }



                string jrscource = "     ";
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
                string oipost = "   ";
                string oilast = "   ";
                string nextperdrev = " ";
                string linktext = " ";
                //Addtional space
                string additinalsp2 = "  ";
                ///////////
                string space15 = "               ";
                string pcnew;
                string annlcd1 = space15;
                string annlcd2 = "N/A            ";// space15;
                string seqnew;
                string annlcd5 = "N/A            "; //space15;
                string annlcd6 = space15;
                string annlcd7 = execcode;
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
                    int sunrefmaxle = refNo.Length;

                    //newrefNo = refNo.Substring(0, 5) + refNo.Substring(refNo.Length - 5);
                    newrefNo = refNo.Substring(sunrefmaxle - 10);

                }
                else
                {
                    int defref = 10 - refNo.Length;
                    newrefNo = refNo + space10.Substring(0, defref);

                }

                if (job.Length > 15)
                {
                    int sunrefmaxle = job.Length;

                    //newrefNo = refNo.Substring(0, 5) + refNo.Substring(refNo.Length - 5);
                    job = job.Substring(sunrefmaxle - 15);
                }
                else
                {
                    int defref = 15 - job.Length;
                    job = job + space15.Substring(0, defref);
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

                //chenged Vat Cat Z
                //seq = "Z";
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
                    summerdate.ToString("yyyy" + "0" + "MM") + trdate.ToString("yyyyMMdd") +
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
                     job +
                     annlcd6 +
                     ann7new +
                     annlcd8 +
                     annlcd9 +
                     blankfinal
                     ;

                return line;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        private void savejobnosun(string com, string cat, string code,DateTime date, string name)
        {
            SunLC ob = new SunLC();
            ob.category = cat;
            string space12 = "            ";
            string space30 = "                              ";
            if (code.Length > 12)
            {
                int sunrefmaxle = code.Length;

                //newrefNo = refNo.Substring(0, 5) + refNo.Substring(refNo.Length - 5);
                code = code.Substring(sunrefmaxle - 12);
            }
            else
            {
                int defref = 12 - code.Length;
                code = code + space12.Substring(0, defref);
            }
            ob.code = code;
            if (name.Length > 30)
            {
                int sunrefmaxle = name.Length;

                //newrefNo = refNo.Substring(0, 5) + refNo.Substring(refNo.Length - 5);
                name = name.Substring(sunrefmaxle - 30);
            }
            else
            {
                int defref = 30 - name.Length;
                name = name + space30.Substring(0, defref);
            }
            ob.name = name;

            ob.sun_db = com;

            ob.updated = Convert.ToInt32(date.ToString("yyyyMMdd")).ToString();
            ob.lookup = "          ";
            ob.prohb_post = "Y";
            ob.budget_check = " ";
            ob.data_1 = "     ";
            ob.budget_stop = " ";
            int effect = CHNLSVC.Sales.SaveSunT4(ob);
        }
    }
}