using FF.BusinessObjects;
using FF.BusinessObjects.Genaral;
using FF.BusinessObjects.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using FastForward.Logistic.Models;
using CrystalDecisions.Shared;
using System.Security.Authentication;
using FF.BusinessObjects.Security;
using System.IO;

namespace FastForward.Logistic.Controllers
{
    public class RecieptEntryController : BaseController
    {
        //
        // GET: /RecieptEntry/
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserId"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                ViewBag.Logincompany = company;
                ViewBag.Loginpc = userDefPro;
                Session["trn_inv_det"] = null;
                Session["RecieptItemList"] = null;
                Session["totalPaidAmount"] = null;
                return View();
            }
            else
            {
                return Redirect("~/Home/index");
            }
        }
        public JsonResult getDivision()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    MasterReceiptDivision _RecDiv = new MasterReceiptDivision();
                    _RecDiv = CHNLSVC.Sales.GetDefRecDivision(company, userDefPro);
                    if (_RecDiv.Msrd_cd != null)
                    {
                        return Json(new { success = true, login = true, division = _RecDiv.Msrd_cd }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = true, login = true, division = "" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult AddInvoiceAmmount(string Invoice, string Ammount)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    MasterReceiptDivision _RecDiv = new MasterReceiptDivision();
                    _RecDiv = CHNLSVC.Sales.GetDefRecDivision(company, userDefPro);
                    if (_RecDiv.Msrd_cd != null)
                    {
                        return Json(new { success = true, login = true, division = _RecDiv.Msrd_cd }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = true, login = true, division = "" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult SaveRecieptEntry(RecieptHeader _ReceiptHeader,string recdate ,string Division,bool unallow=false)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string err = "";
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                if (Division == "")
                {
                    return Json(new { success = false, login = true, msg = "Please enter division.", Type = "Info" }, JsonRequestBehavior.AllowGet);

                }
                _ReceiptHeader.Sar_profit_center_cd = userDefPro;
                _ReceiptHeader.Sar_com_cd = company;
                _ReceiptHeader.Sar_create_by = userId;
                _ReceiptHeader.Sar_create_when = DateTime.Now;
                _ReceiptHeader.Sar_mod_by = userId;
                _ReceiptHeader.Sar_mod_when = DateTime.Now;
                _ReceiptHeader.Sar_act = true;
                _ReceiptHeader.Sar_receipt_date = Convert.ToDateTime(recdate);
                List<RecieptItem> _recieptItem = new List<RecieptItem>();
                List<RecieptItemTBS> _recieptItemtbs = (List<RecieptItemTBS>)Session["RecieptItemList"];
                if (_recieptItemtbs == null)
                {
                    _recieptItemtbs = new List<RecieptItemTBS>();
                }
                if (_recieptItemtbs.Count > 0)
                {
                    _recieptItem = new List<RecieptItem>();
                    foreach (RecieptItemTBS tbs in _recieptItemtbs)
                    {
                        RecieptItem recItm = new RecieptItem();
                        recItm.Sard_anal_1 = tbs.Sird_anal_1;
                        recItm.Sard_anal_2 = tbs.Sird_anal_2;
                        recItm.Sard_anal_3 = tbs.Sird_anal_3;
                        recItm.Sard_anal_4 = tbs.Sird_anal_4;
                        recItm.Sard_anal_5 = tbs.Sird_anal_5;
                        recItm.Sard_cc_batch = tbs.Sird_cc_batch;
                        recItm.Sard_cc_expiry_dt = tbs.Sird_cc_expiry_dt;
                        recItm.Sard_cc_is_promo = tbs.Sird_cc_is_promo;
                        recItm.Sard_cc_period = tbs.Sird_cc_period;
                        recItm.Sard_cc_tp = tbs.Sird_cc_tp;
                        recItm.Sard_chq_bank_cd = tbs.Sird_chq_bank_cd;
                        recItm.Sard_chq_branch = tbs.Sird_chq_branch;
                        recItm.Sard_chq_dt = tbs.Sird_chq_dt;
                        recItm.Sard_credit_card_bank = tbs.Sird_credit_card_bank;
                        recItm.Sard_deposit_bank_cd = tbs.Sird_deposit_bank_cd;
                        recItm.Sard_deposit_branch = tbs.Sird_deposit_branch;
                        recItm.Sard_gv_issue_dt = tbs.Sird_gv_issue_dt;
                        recItm.Sard_gv_issue_loc = tbs.Sird_gv_issue_loc;
                        recItm.Sard_inv_no = tbs.Sird_inv_no;
                        recItm.Sard_line_no = tbs.Sird_line_no;
                        recItm.Sard_pay_tp = tbs.Sird_pay_tp;
                        recItm.Sard_receipt_no = tbs.Sird_receipt_no;
                        recItm.Sard_ref_no = tbs.Sird_ref_no;
                        recItm.Sard_rmk = tbs.Sird_rmk;
                        recItm.Sard_seq_no = tbs.Sird_seq_no;
                        recItm.Sard_settle_amt = tbs.Sird_settle_amt;
                        recItm.Sard_sim_ser = tbs.Sird_sim_ser;
                        recItm.Newpayment = tbs.Newpayment;
                        _recieptItem.Add(recItm);
                    }

                    if (unallow == true && _recieptItem.Count > 1)
                    {
                        return Json(new { success = false, login = true, msg = "Cannot pay multiple payment mothod for unallowcated payment receipt.", Type = "Info" }, JsonRequestBehavior.AllowGet);

                    }
                    _ReceiptHeader.Sar_seq_no = -1; //CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "RECEIPT", 1, BaseCls.GlbUserComCode);
                    // _ReceiptHeader.Sar_receipt_no = txtRecNo.Text.Trim();
                    MasterReceiptDivision _RecDiv = new MasterReceiptDivision();
                    _RecDiv = CHNLSVC.Sales.GetDefRecDivision(company, userDefPro);
                    if (_RecDiv.Msrd_cd != null)
                    {
                        _ReceiptHeader.Sar_prefix = _RecDiv.Msrd_cd;
                    }
                    else
                    {
                        _ReceiptHeader.Sar_prefix = "";
                    }

                    //_ReceiptHeader.Sar_prefix = txtDivision.Text.Trim();
                    // _ReceiptHeader.Sar_manual_ref_no = txtManual.Text.Trim();
                    _ReceiptHeader.Sar_direct = true;
                    _ReceiptHeader.Sar_acc_no = "";
                    _ReceiptHeader.Sar_is_oth_shop = false;
                    _ReceiptHeader.Sar_oth_sr = "";
                    _ReceiptHeader.Sar_tel_no = "";
                    _ReceiptHeader.Sar_mob_no = "";
                    _ReceiptHeader.Sar_nic_no = "";
                    _ReceiptHeader.Sar_comm_amt = 0;
                    _ReceiptHeader.Sar_is_mgr_iss = false;
                    _ReceiptHeader.Sar_esd_rate = 0;
                    _ReceiptHeader.Sar_wht_rate = 0;
                    _ReceiptHeader.Sar_epf_rate = 0;
                    _ReceiptHeader.Sar_currency_cd = "LKR";
                    _ReceiptHeader.Sar_uploaded_to_finance = false;
                    _ReceiptHeader.Sar_act = true;
                    _ReceiptHeader.Sar_direct_deposit_bank_cd = "";
                    _ReceiptHeader.Sar_direct_deposit_branch = "";
                    if (_recieptItem.Count>0)
                        _ReceiptHeader.Sar_tot_settle_amt = _recieptItem.Sum(x => x.Sard_settle_amt);

                    MST_BUSENTITY Cus_det = CHNLSVC.Sales.getConsigneeDetailsByAccCode(_ReceiptHeader.Sar_debtor_cd, company, "");
                    if (Cus_det.MBE_CD  !=null)
                    {
                        _ReceiptHeader.Sar_debtor_name = Cus_det.MBE_NAME;
                        _ReceiptHeader.Sar_debtor_add_1 = Cus_det.MBE_ADD1;
                        _ReceiptHeader.Sar_debtor_add_2 = Cus_det.MBE_ADD2;
                        _ReceiptHeader.Sar_debtor_add_2 = Cus_det.MBE_TEL;
                        _ReceiptHeader.Sar_debtor_add_2 = Cus_det.MBE_NIC;
                        _ReceiptHeader.Sar_debtor_add_2 = Cus_det.MBE_MOB;
                    }

                    // _ReceiptHeader.Sar_remarks = txtNote.Text.Trim();
                    _ReceiptHeader.Sar_is_used = false;
                    _ReceiptHeader.Sar_ref_doc = "";
                    _ReceiptHeader.Sar_ser_job_no = "";
                    _ReceiptHeader.Sar_used_amt = 0;
                    _ReceiptHeader.Sar_create_by = Session["UserID"].ToString();
                    _ReceiptHeader.Sar_mod_by = Session["UserID"].ToString();
                    _ReceiptHeader.Sar_session_id = Session["SessionID"].ToString();
                    _ReceiptHeader.Sar_anal_1 = "";
                    _ReceiptHeader.Sar_anal_2 = "";
                    _ReceiptHeader.Sar_anal_9 = (unallow) ? 1 : 0;
                }

                MasterAutoNumber _receiptAuto = null;
                if (_recieptItem != null)
                    if (_recieptItem.Count > 0)
                    {
                        _receiptAuto = new MasterAutoNumber();
                        _receiptAuto.Aut_cate_cd = userDefPro;
                        _receiptAuto.Aut_cate_tp = "PRO";
                        _receiptAuto.Aut_direction = 1;
                        _receiptAuto.Aut_moduleid = "RECEIPT";
                        _receiptAuto.Aut_number = 0;
                        _receiptAuto.Aut_start_char = "REC";
                        //_receiptAuto.Aut_year = Convert.ToDateTime(_ReceiptHeader.Sar_receipt_date).Year;
                        _receiptAuto.Aut_year = Convert.ToDateTime(_ReceiptHeader.Sar_create_when).Year; // Added by Chathura due to above value is always 1
                    }
                if (_ReceiptHeader.Sar_receipt_type == "DEBT")
                {
                   
                    List<RecieptItemTBS> sesRecieptItemList = Session["SecRecieptItemList"] as List<RecieptItemTBS>;
                    if (sesRecieptItemList == null)
                    {
                        sesRecieptItemList = new List<RecieptItemTBS>();
                    }
                    List<RecieptItem> newrecieptItem = new List<RecieptItem>();
                    if (sesRecieptItemList.Count > 1)
                    {
                        if (_recieptItemtbs.Count > 1 && sesRecieptItemList.Count > 1)
                        {
                            return Json(new { success = false, login = true, msg = "Cannot pay multiple payment mothod for multiple invoice settlement.", Type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        
                        foreach (RecieptItemTBS item in sesRecieptItemList)
                        {
                            RecieptItem recItm = new RecieptItem();
                            recItm.Sard_anal_1 = _recieptItemtbs[0].Sird_anal_1;
                            recItm.Sard_anal_2 = _recieptItemtbs[0].Sird_anal_2;
                            recItm.Sard_anal_3 = _recieptItemtbs[0].Sird_anal_3;
                            recItm.Sard_anal_4 = _recieptItemtbs[0].Sird_anal_4;
                            recItm.Sard_anal_5 = _recieptItemtbs[0].Sird_anal_5;
                            recItm.Sard_cc_batch = _recieptItemtbs[0].Sird_cc_batch;
                            recItm.Sard_cc_expiry_dt = _recieptItemtbs[0].Sird_cc_expiry_dt;
                            recItm.Sard_cc_is_promo = _recieptItemtbs[0].Sird_cc_is_promo;
                            recItm.Sard_cc_period = _recieptItemtbs[0].Sird_cc_period;
                            recItm.Sard_cc_tp = _recieptItemtbs[0].Sird_cc_tp;
                            recItm.Sard_chq_bank_cd = _recieptItemtbs[0].Sird_chq_bank_cd;
                            recItm.Sard_chq_branch = _recieptItemtbs[0].Sird_chq_branch;
                            recItm.Sard_chq_dt = _recieptItemtbs[0].Sird_chq_dt;
                            recItm.Sard_credit_card_bank = _recieptItemtbs[0].Sird_credit_card_bank;
                            recItm.Sard_deposit_bank_cd = _recieptItemtbs[0].Sird_deposit_bank_cd;
                            recItm.Sard_deposit_branch = _recieptItemtbs[0].Sird_deposit_branch;
                            recItm.Sard_gv_issue_dt = _recieptItemtbs[0].Sird_gv_issue_dt;
                            recItm.Sard_gv_issue_loc = _recieptItemtbs[0].Sird_gv_issue_loc;
                            recItm.Sard_inv_no = item.Sird_inv_no;
                            recItm.Sard_line_no = _recieptItemtbs[0].Sird_line_no;
                            recItm.Sard_pay_tp = _recieptItemtbs[0].Sird_pay_tp;
                            recItm.Sard_receipt_no = _recieptItemtbs[0].Sird_receipt_no;
                            recItm.Sard_ref_no = _recieptItemtbs[0].Sird_ref_no;
                            recItm.Sard_rmk = _recieptItemtbs[0].Sird_rmk;
                            recItm.Sard_seq_no = _recieptItemtbs[0].Sird_seq_no;
                            recItm.Sard_settle_amt = item.Sird_settle_amt;
                            recItm.Sard_sim_ser = _recieptItemtbs[0].Sird_sim_ser;
                            recItm.Newpayment = _recieptItemtbs[0].Newpayment;
                            newrecieptItem.Add(recItm);

                        }
                        _recieptItem = new List<RecieptItem>();
                        _recieptItem = newrecieptItem;
                        
                    }
                    if (unallow == true && _recieptItem.Sum(x => x.Sard_settle_amt) > newrecieptItem.Sum(x => x.Sard_settle_amt))
                    {
                        RecieptItem recItm = new RecieptItem();
                        recItm.Sard_anal_1 = _recieptItemtbs[0].Sird_anal_1;
                        recItm.Sard_anal_2 = _recieptItemtbs[0].Sird_anal_2;
                        recItm.Sard_anal_3 = _recieptItemtbs[0].Sird_anal_3;
                        recItm.Sard_anal_4 = _recieptItemtbs[0].Sird_anal_4;
                        recItm.Sard_anal_5 = _recieptItemtbs[0].Sird_anal_5;
                        recItm.Sard_cc_batch = _recieptItemtbs[0].Sird_cc_batch;
                        recItm.Sard_cc_expiry_dt = _recieptItemtbs[0].Sird_cc_expiry_dt;
                        recItm.Sard_cc_is_promo = _recieptItemtbs[0].Sird_cc_is_promo;
                        recItm.Sard_cc_period = _recieptItemtbs[0].Sird_cc_period;
                        recItm.Sard_cc_tp = _recieptItemtbs[0].Sird_cc_tp;
                        recItm.Sard_chq_bank_cd = _recieptItemtbs[0].Sird_chq_bank_cd;
                        recItm.Sard_chq_branch = _recieptItemtbs[0].Sird_chq_branch;
                        recItm.Sard_chq_dt = _recieptItemtbs[0].Sird_chq_dt;
                        recItm.Sard_credit_card_bank = _recieptItemtbs[0].Sird_credit_card_bank;
                        recItm.Sard_deposit_bank_cd = _recieptItemtbs[0].Sird_deposit_bank_cd;
                        recItm.Sard_deposit_branch = _recieptItemtbs[0].Sird_deposit_branch;
                        recItm.Sard_gv_issue_dt = _recieptItemtbs[0].Sird_gv_issue_dt;
                        recItm.Sard_gv_issue_loc = _recieptItemtbs[0].Sird_gv_issue_loc;
                        recItm.Sard_inv_no = "";
                        recItm.Sard_line_no = _recieptItemtbs[0].Sird_line_no;
                        recItm.Sard_pay_tp = _recieptItemtbs[0].Sird_pay_tp;
                        recItm.Sard_receipt_no = _recieptItemtbs[0].Sird_receipt_no;
                        recItm.Sard_ref_no = _recieptItemtbs[0].Sird_ref_no;
                        recItm.Sard_rmk = _recieptItemtbs[0].Sird_rmk;
                        recItm.Sard_seq_no = _recieptItemtbs[0].Sird_seq_no;
                        recItm.Sard_settle_amt = (_recieptItem.Sum(x => x.Sard_settle_amt) - newrecieptItem.Sum(x => x.Sard_settle_amt));
                        recItm.Sard_sim_ser = _recieptItemtbs[0].Sird_sim_ser;
                        recItm.Newpayment = _recieptItemtbs[0].Newpayment;
                        newrecieptItem.Add(recItm);
                        _recieptItem = new List<RecieptItem>();
                        _recieptItem = newrecieptItem;
                    }
                    
                }
                
                if (_recieptItem.Count < 1)
                {
                    return Json(new { success = false, login = true, msg = "Please add payment details.", Type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    int effect = CHNLSVC.Sales.SaveJobReciept(_ReceiptHeader, _recieptItem, _receiptAuto, out err);
                    if (effect > 0)
                    {
                        return Json(new { success = true, login = true, msg = "Successfully Saved : " + err, Type = err }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = err, Type = "Error" }, JsonRequestBehavior.AllowGet);
                    }
                }
                

            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult getReciptTypes(string type)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    bool _isValidRec = CHNLSVC.Sales.IsValidReceiptType(company, type);

                    if (_isValidRec == false)
                    {
                        string msg = "Not allowed to view receipt type " + type + " in receipt module.";
                        return Json(new { success = false, login = true, msg = msg, type = "Info" }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = ex.Message, type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getReceiptDetails(string receiptNo)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (!string.IsNullOrEmpty(receiptNo))
                    {
                        RecieptHeader _ReceiptHeader = null;
                        _ReceiptHeader = CHNLSVC.Sales.GetReceiptHeader(company, userDefPro, receiptNo.Trim());

                        if (_ReceiptHeader != null)
                        {
                            RecieptItem _paramRecDetails = new RecieptItem();
                            _paramRecDetails.Sard_receipt_no = receiptNo.Trim();
                            List<RecieptItem> _list = new List<RecieptItem>();
                            _list = CHNLSVC.Sales.GetReceiptDetails(_paramRecDetails);
                            return Json(new { success = true, login = true, ReceiptHeader = _ReceiptHeader, receiptItem = _list }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "Invalid receipt number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { success = true, login = false }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = ex.Message, type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public void ClearSession()
        {
            Session["trn_inv_det"] = null;
            Session["RecieptItemList"] = null;
            Session["totalPaidAmount"] = null;
            Session["SecRecieptItemList"] = null;
            Session["RecieptItemList"] = null;
        }

        public JsonResult CancelRecieptEntry(string data, string receptDate,string type)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string err = "";
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                DateTime recDt;
                try
                {
                    recDt = Convert.ToDateTime(receptDate);
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, login = true, msg = "Please enter valid receipt date.", Type = "Info" }, JsonRequestBehavior.AllowGet);

                }

                if (!CHNLSVC.Security.Is_OptionPerimitted(company, userId, 1007))
                {
                    return Json(new { success = false, login = true, msg = "You don't have permission to cancel receipt.(Requsted permission code 1007)", Type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                if (recDt.Date < DateTime.Now.Date)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(company, userId, 1008))
                    {
                        return Json(new { success = false, login = true, msg = "You don't have permission to cancel back date receipt.(Requsted permission code 1008)", Type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                }


                string error = "";
                Int32 effect = CHNLSVC.Sales.CancelJobReciept(data, type, out error);
                if (effect > 0)
                {
                    return Json(new { success = true, login = true, msg = "Successfully canceled : " + err }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = true, msg = err, Type = "Error" }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveRecieptEntryRefund(RecieptHeader _ReceiptHeader)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string err = "";
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                _ReceiptHeader.Sar_profit_center_cd = userDefPro;
                _ReceiptHeader.Sar_com_cd = company;
                _ReceiptHeader.Sar_create_by = userId;
                _ReceiptHeader.Sar_create_when = DateTime.Now;
                _ReceiptHeader.Sar_mod_by = userId;
                _ReceiptHeader.Sar_mod_when = DateTime.Now;
                _ReceiptHeader.Sar_act = true;
                List<RecieptItem> _recieptItem = new List<RecieptItem>();
                List<RecieptItemTBS> _recieptItemtbs = (List<RecieptItemTBS>)Session["RecieptItemList"];
                if (_recieptItemtbs == null)
                {
                    _recieptItemtbs = new List<RecieptItemTBS>();
                }
                if (_recieptItemtbs.Count > 0)
                {
                    _recieptItem = new List<RecieptItem>();
                    foreach (RecieptItemTBS tbs in _recieptItemtbs)
                    {
                        RecieptItem recItm = new RecieptItem();
                        recItm.Sard_anal_1 = tbs.Sird_anal_1;
                        recItm.Sard_anal_2 = tbs.Sird_anal_2;
                        recItm.Sard_anal_3 = tbs.Sird_anal_3;
                        recItm.Sard_anal_4 = tbs.Sird_anal_4;
                        recItm.Sard_anal_5 = tbs.Sird_anal_5;
                        recItm.Sard_cc_batch = tbs.Sird_cc_batch;
                        recItm.Sard_cc_expiry_dt = tbs.Sird_cc_expiry_dt;
                        recItm.Sard_cc_is_promo = tbs.Sird_cc_is_promo;
                        recItm.Sard_cc_period = tbs.Sird_cc_period;
                        recItm.Sard_cc_tp = tbs.Sird_cc_tp;
                        recItm.Sard_chq_bank_cd = tbs.Sird_chq_bank_cd;
                        recItm.Sard_chq_branch = tbs.Sird_chq_branch;
                        recItm.Sard_chq_dt = tbs.Sird_chq_dt;
                        recItm.Sard_credit_card_bank = tbs.Sird_credit_card_bank;
                        recItm.Sard_deposit_bank_cd = tbs.Sird_deposit_bank_cd;
                        recItm.Sard_deposit_branch = tbs.Sird_deposit_branch;
                        recItm.Sard_gv_issue_dt = tbs.Sird_gv_issue_dt;
                        recItm.Sard_gv_issue_loc = tbs.Sird_gv_issue_loc;
                        recItm.Sard_inv_no = tbs.Sird_inv_no;
                        recItm.Sard_line_no = tbs.Sird_line_no;
                        recItm.Sard_pay_tp = tbs.Sird_pay_tp;
                        recItm.Sard_receipt_no = tbs.Sird_receipt_no;
                        recItm.Sard_ref_no = tbs.Sird_ref_no;
                        recItm.Sard_rmk = tbs.Sird_rmk;
                        recItm.Sard_seq_no = tbs.Sird_seq_no;
                        recItm.Sard_settle_amt = tbs.Sird_settle_amt;
                        recItm.Sard_sim_ser = tbs.Sird_sim_ser;
                        recItm.Newpayment = tbs.Newpayment;
                        _recieptItem.Add(recItm);
                    }

                    _ReceiptHeader.Sar_seq_no = -1; //CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "RECEIPT", 1, BaseCls.GlbUserComCode);
                    // _ReceiptHeader.Sar_receipt_no = txtRecNo.Text.Trim();
                    MasterReceiptDivision _RecDiv = new MasterReceiptDivision();
                    _RecDiv = CHNLSVC.Sales.GetDefRecDivision(company, userDefPro);
                    if (_RecDiv.Msrd_cd != null)
                    {
                        _ReceiptHeader.Sar_prefix = _RecDiv.Msrd_cd;
                    }
                    else
                    {
                        _ReceiptHeader.Sar_prefix = "";
                    }

                    //_ReceiptHeader.Sar_prefix = txtDivision.Text.Trim();
                    // _ReceiptHeader.Sar_manual_ref_no = txtManual.Text.Trim();
                    _ReceiptHeader.Sar_direct = true;
                    _ReceiptHeader.Sar_acc_no = "";
                    _ReceiptHeader.Sar_is_oth_shop = false;
                    _ReceiptHeader.Sar_oth_sr = "";
                    _ReceiptHeader.Sar_tel_no = "";
                    _ReceiptHeader.Sar_mob_no = "";
                    _ReceiptHeader.Sar_nic_no = "";
                    _ReceiptHeader.Sar_comm_amt = 0;
                    _ReceiptHeader.Sar_is_mgr_iss = false;
                    _ReceiptHeader.Sar_esd_rate = 0;
                    _ReceiptHeader.Sar_wht_rate = 0;
                    _ReceiptHeader.Sar_epf_rate = 0;
                    _ReceiptHeader.Sar_currency_cd = "LKR";
                    _ReceiptHeader.Sar_uploaded_to_finance = false;
                    _ReceiptHeader.Sar_act = true;
                    _ReceiptHeader.Sar_direct_deposit_bank_cd = "";
                    _ReceiptHeader.Sar_direct_deposit_branch = "";
                    // _ReceiptHeader.Sar_remarks = txtNote.Text.Trim();
                    _ReceiptHeader.Sar_is_used = false;
                    _ReceiptHeader.Sar_ref_doc = "";
                    _ReceiptHeader.Sar_ser_job_no = "";
                    _ReceiptHeader.Sar_used_amt = 0;
                    _ReceiptHeader.Sar_create_by = Session["UserID"].ToString();
                    _ReceiptHeader.Sar_mod_by = Session["UserID"].ToString();
                    _ReceiptHeader.Sar_session_id = Session["SessionID"].ToString();
                    _ReceiptHeader.Sar_anal_1 = "";
                    _ReceiptHeader.Sar_anal_2 = "";
                }

                MasterAutoNumber _receiptAuto = null;
                if (_recieptItem != null)
                    if (_recieptItem.Count > 0)
                    {
                        _receiptAuto = new MasterAutoNumber();
                        _receiptAuto.Aut_cate_cd = userDefPro;
                        _receiptAuto.Aut_cate_tp = "PRO";
                        _receiptAuto.Aut_direction = 1;
                        _receiptAuto.Aut_moduleid = "REFUND";
                        _receiptAuto.Aut_number = 0;
                        _receiptAuto.Aut_start_char = "REC";
                        //_receiptAuto.Aut_year = Convert.ToDateTime(_ReceiptHeader.Sar_receipt_date).Year;
                        _receiptAuto.Aut_year = Convert.ToDateTime(_ReceiptHeader.Sar_create_when).Year; // Added by Chathura due to above value is always 1
                    }

                if (_recieptItem.Count < 1)
                {
                    return Json(new { success = false, login = true, msg = "Please add payment details.", Type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    int effect = CHNLSVC.Sales.SaveJobReciept(_ReceiptHeader, _recieptItem, _receiptAuto, out err);
                    if (effect > 0)
                    {
                        return Json(new { success = true, login = true, msg = "Successfully Saved : " + err }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = err, Type = "Error" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult addSettlementDetails(string paymentValue, string customer, string type, string invAmount, string invNo,bool unallow=false)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<RecieptItemTBS> PayRecieptItemList = new List<RecieptItemTBS>();
                    List<RecieptItemTBS> PayItemList = Session["RecieptItemList"] as List<RecieptItemTBS>;
                    if (PayItemList == null)
                    {
                        PayRecieptItemList = new List<RecieptItemTBS>();
                    }
                    else
                    {
                        PayRecieptItemList = PayItemList;
                    }
                    if (string.IsNullOrEmpty(invAmount) && unallow == true)
                    {
                        invAmount = "0";
                    }
                    string paidAmountString = Session["totalPaidAmount"] as string;
                    decimal totPaidAmount = Convert.ToDecimal(paidAmountString);
                    List<RecieptItemTBS> RecieptItemList = new List<RecieptItemTBS>();
                    List<RecieptItemTBS> sesRecieptItemList = Session["SecRecieptItemList"] as List<RecieptItemTBS>;
                    if (sesRecieptItemList == null)
                    {
                        sesRecieptItemList = new List<RecieptItemTBS>();
                    }
                    else
                    {
                        RecieptItemList = sesRecieptItemList;
                    }
                    paymentValue = paymentValue.Trim();
                    customer = customer.Trim();
                    type = type.Trim();
                    invAmount = invAmount.Trim();
                    invNo = invNo.Trim();
                    decimal balanceAmount = 0;
                    if (string.IsNullOrEmpty(paymentValue))
                    {
                        return Json(new { success = false, login = true, msg = "Please enter amount which customer is going to pay.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (!isDecimal(paymentValue))
                    {
                        return Json(new { success = false, login = true, msg = "Payment amount should be numeric.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(customer))
                    {
                        return Json(new { success = false, login = true, msg = "Please select payment customer.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }

                    if (type == "ADVAN" || type == "OTHER")
                    {

                        if (RecieptItemList.Count > 0)
                        {
                            return Json(new { success = false, login = true, msg = "Payments are already added. Now you cannot add more details.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (type == "DEBT")
                    {
                        if (PayRecieptItemList.Count > 0)
                        {
                            return Json(new { success = false, login = true, msg = "Payments are already added. Now you cannot add more invoice details.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }

                        if (Convert.ToDecimal(paymentValue) > Convert.ToDecimal(invAmount))
                        {
                            if (unallow == false)
                            {
                                return Json(new { success = false, login = true, msg = "Payment cannot exceed outstanding amount.", type = "Info" }, JsonRequestBehavior.AllowGet);

                            }
                        }

                        if (Convert.ToDecimal(paymentValue) <= 0)
                        {
                            return Json(new { success = false, login = true, msg = "Settle amount cannot be zero.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        if (!CHNLSVC.Security.Is_OptionPerimitted(company, userId, 16036))
                        {
                            if (RecieptItemList.Count > 0)
                            {
                                foreach (RecieptItemTBS line in RecieptItemList)
                                {
                                    if (line.Sird_inv_no == invNo)
                                    {
                                        return Json(new { success = false, login = true, msg = "Already add this invoice.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(invNo))
                        {
                            RecieptItemTBS item = new RecieptItemTBS();
                            item.Sird_inv_no = invNo.ToString();
                            item.Sird_settle_amt = Convert.ToDecimal(paymentValue.ToString());
                            item.Sird_anal_3 = Convert.ToDecimal(invAmount.ToString());
                            sesRecieptItemList.Add(item);
                            Session["SecRecieptItemList"] = sesRecieptItemList;
                        }
                    }
                    decimal _Amt = Convert.ToDecimal(paymentValue);
                    decimal TotalAmount = 0;
                    if (sesRecieptItemList.Count > 0)
                    {
                        TotalAmount = sesRecieptItemList.Sum(x => x.Sird_settle_amt);
                    }
                    else
                    {
                        if (unallow == true)
                        {
                            TotalAmount = _Amt;
                        }
                    }
                    return Json(new { success = true, login = true, TotalAmount = TotalAmount, sesRecieptItemList = sesRecieptItemList, settleAmt = _Amt }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public bool isDecimal(string val)
        {
            decimal result;
            return decimal.TryParse(val, out result);
        }
        public JsonResult getInvoiceNoByCus(string invno, string cus, string othpc)
        {
             string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    trn_inv_hdr _isValidRec = CHNLSVC.Sales.validateInvoiceNUmber(company, cus, othpc, userDefPro);

                    if (_isValidRec.Tih_inv_no ==null)
                    {
                        return Json(new { success = false, login = true, msg = "Invalid invoce number.", type = "Info" }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        return Json(new { success = true, login = true, data = _isValidRec }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult removeInvoice(string invNo)
        {
             string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                decimal settleamnt = 0;
                List<RecieptItemTBS> PayRecieptItemList = new List<RecieptItemTBS>();
                List<RecieptItemTBS> PayItemList = Session["RecieptItemList"] as List<RecieptItemTBS>;
                if (PayItemList == null)
                {
                    PayRecieptItemList = new List<RecieptItemTBS>();
                }
                else
                {
                    PayRecieptItemList = PayItemList;
                }

                string paidAmountString = Session["totalPaidAmount"] as string;
                decimal totPaidAmount = Convert.ToDecimal(paidAmountString);
                List<RecieptItemTBS> RecieptItemList = new List<RecieptItemTBS>();
                List<RecieptItemTBS> sesRecieptItemList = Session["SecRecieptItemList"] as List<RecieptItemTBS>;
                if (sesRecieptItemList == null)
                {
                    sesRecieptItemList = new List<RecieptItemTBS>();
                }
                else
                {
                    RecieptItemList = sesRecieptItemList;
                }

                if (PayRecieptItemList.Count > 0)
                {
                    return Json(new { success = false, login = true, msg = "Please remove payment before remove invoice.", type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                if (sesRecieptItemList.Count > 0)
                {
                    var itemToRemove = sesRecieptItemList.Single(x=> x.Sird_inv_no == invNo);
                    settleamnt = Convert.ToDecimal(itemToRemove.Sird_settle_amt);
                    sesRecieptItemList.Remove(itemToRemove);
                }
                Session["SecRecieptItemList"] = sesRecieptItemList;
                decimal setamt = sesRecieptItemList.Sum(x=>x.Sird_settle_amt);
                return Json(new { success = true, login = true, sesRecieptItemList = sesRecieptItemList, setamt = setamt, settleamnt = settleamnt }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult updateReceiptdetails(RecieptHeader _ReceiptHeaderNew)
        {
            string userId = HttpContext.Session["UserId"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    RecieptHeader _ReceiptHeader = null;
                    _ReceiptHeader = CHNLSVC.Sales.GetReceiptHeader(company, userDefPro, _ReceiptHeaderNew.Sar_receipt_no.Trim());

                    if (_ReceiptHeader.Sar_debtor_cd != _ReceiptHeaderNew.Sar_debtor_cd)
                    {
                        return Json(new { success = false, login = true, msg = "Customer not match with original receipt customer.", Type = "Info" }, JsonRequestBehavior.AllowGet);

                    }
                    List<RecieptItemTBS> RecieptItemList = new List<RecieptItemTBS>();
                    List<RecieptItemTBS> sesRecieptItemList = Session["SecRecieptItemList"] as List<RecieptItemTBS>;
                    if (sesRecieptItemList == null)
                    {
                        sesRecieptItemList = new List<RecieptItemTBS>();
                    }
                    else
                    {
                        RecieptItemList = sesRecieptItemList;
                    }

                    List<RecieptItem> _recieptItemtbs = new List<RecieptItem>();

                    _recieptItemtbs = CHNLSVC.Sales.GetReceiptDetailsNonAlocated(_ReceiptHeaderNew.Sar_receipt_no.Trim());

                    if (_recieptItemtbs.Sum(x => x.Sard_settle_amt) < sesRecieptItemList.Sum(x => x.Sird_settle_amt))
                    {
                        return Json(new { success = false, login = true, msg = "Cannot exceed original receipt amount of " + _recieptItemtbs.Sum(x => x.Sard_settle_amt), Type = "Info" }, JsonRequestBehavior.AllowGet);

                    }
                    decimal settleAmt=RecieptItemList.Sum(x => x.Sird_settle_amt);
                   List<RecieptItem> newrecieptItem = new List<RecieptItem>();
                    if (sesRecieptItemList.Count > 0)
                    {
                        
                        foreach (RecieptItemTBS item in sesRecieptItemList)
                        {
                            RecieptItem recItm = new RecieptItem();
                            recItm.Sard_anal_1 = _recieptItemtbs[0].Sard_anal_1;
                            recItm.Sard_anal_2 = _recieptItemtbs[0].Sard_anal_2;
                            recItm.Sard_anal_3 = _recieptItemtbs[0].Sard_anal_3;
                            recItm.Sard_anal_4 = _recieptItemtbs[0].Sard_anal_4;
                            recItm.Sard_anal_5 = _recieptItemtbs[0].Sard_anal_5;
                            recItm.Sard_cc_batch = _recieptItemtbs[0].Sard_cc_batch;
                            recItm.Sard_cc_expiry_dt = _recieptItemtbs[0].Sard_cc_expiry_dt;
                            recItm.Sard_cc_is_promo = _recieptItemtbs[0].Sard_cc_is_promo;
                            recItm.Sard_cc_period = _recieptItemtbs[0].Sard_cc_period;
                            recItm.Sard_cc_tp = _recieptItemtbs[0].Sard_cc_tp;
                            recItm.Sard_chq_bank_cd = _recieptItemtbs[0].Sard_chq_bank_cd;
                            recItm.Sard_chq_branch = _recieptItemtbs[0].Sard_chq_branch;
                            recItm.Sard_chq_dt = _recieptItemtbs[0].Sard_chq_dt;
                            recItm.Sard_credit_card_bank = _recieptItemtbs[0].Sard_credit_card_bank;
                            recItm.Sard_deposit_bank_cd = _recieptItemtbs[0].Sard_deposit_bank_cd;
                            recItm.Sard_deposit_branch = _recieptItemtbs[0].Sard_deposit_branch;
                            recItm.Sard_gv_issue_dt = _recieptItemtbs[0].Sard_gv_issue_dt;
                            recItm.Sard_gv_issue_loc = _recieptItemtbs[0].Sard_gv_issue_loc;
                            recItm.Sard_inv_no = item.Sird_inv_no;
                            recItm.Sard_line_no = _recieptItemtbs[0].Sard_line_no;
                            recItm.Sard_pay_tp = _recieptItemtbs[0].Sard_pay_tp;
                            recItm.Sard_receipt_no = _recieptItemtbs[0].Sard_receipt_no;
                            recItm.Sard_ref_no = _recieptItemtbs[0].Sard_ref_no;
                            recItm.Sard_rmk = _recieptItemtbs[0].Sard_rmk;
                            recItm.Sard_seq_no = _recieptItemtbs[0].Sard_seq_no;
                            recItm.Sard_settle_amt = item.Sird_settle_amt;
                            recItm.Sard_sim_ser = _recieptItemtbs[0].Sard_sim_ser;
                            recItm.Newpayment = _recieptItemtbs[0].Newpayment;
                            newrecieptItem.Add(recItm);

                        }
                    }
                    string error = "";
                    Int32 eff = CHNLSVC.Sales.updateunalocatedReceipt(_ReceiptHeaderNew.Sar_receipt_no.Trim(), settleAmt, newrecieptItem, _ReceiptHeader, out error);
                    if (eff > 0)
                    {

                        return Json(new { success = true, login = true, msg = "Successfully update receipt number : " + _ReceiptHeaderNew.Sar_receipt_no.Trim(), Type = "Success" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        if (error != "")
                        {
                            return Json(new { success = false, login = true, msg = error, Type = "Error" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "Unable to update receipt.", Type = "Error" }, JsonRequestBehavior.AllowGet);

                        }
                    }
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult PrintReceipt(string ReceiptNo)
        {
            string err_option = "";
            string err_form = "";

            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    MasterCompany _msCom = CHNLSVC.Security.GetCompByCode(Session["UserCompanyCode"].ToString());
                    ReportViewerViewModel model = new ReportViewerViewModel();
                    ReportDocument rd = new ReportDocument();
                    DataTable dt1 = new DataTable();
                    DataTable dt2 = new DataTable();
                 
                    string reportName = "";
                    string fileName = "";
                    string ReportPath = System.Configuration.ConfigurationManager.AppSettings["ReportPath"].ToString();
                    string report = "";

                    //THarindu 2018-01-04

                    dt1 = CHNLSVC.Sales.getCompanyDetailsBycd(company);
                    dt2 = CHNLSVC.Sales.GetRptReceiptDetails(company, userDefPro, ReceiptNo);


                    if(company == "CCD")
                    {
                        err_option = "rpt_Reciptwithlogo-PDF";
                        err_form = "rpt_Reciptwithlogo";

                        reportName = "rpt_Reciptwithlogo.rpt";
                        fileName = "rpt_Reciptwithlogo.pdf";                                              
                    }
                    else
                    {
                        err_option = "rpt_ReceiptWothoutLogo-PDF";
                        err_form = "rpt_ReceiptWothoutLogo";

                        reportName = "rpt_ReceiptWothoutLogo.rpt";
                        fileName = "ReceiptWothoutLogo.pdf";
                    }
                 
                    report = ReportPath + "\\" + reportName;
                    rd.Load(report);

                    rd.Database.Tables["COMPANY"].SetDataSource(dt1);
                    rd.Database.Tables["ReceiptDetails"].SetDataSource(dt2);
                  
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();

                    try
                    {
                        this.Response.Clear();
                        this.Response.ContentType = "application/pdf";
                        Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName);

                        Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                        stream.Seek(0, SeekOrigin.Begin);
                        rd.Close();
                        rd.Dispose();
                        return File(stream, "application/pdf"); 
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                    //return File(rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), "application/pdf");
                }
                else
                {
                    return Redirect("~/Home/index");
                }


            }

            catch (Exception ex)
            {
                CHNLSVC.General.SaveReportErrorLog(err_option, err_form, ex.Message, Session["UserID"].ToString());
                ViewData["Error"] = ex.Message.ToString();
                return View("RequestPrint");
            }

            // return Redirect("~/Home/index");
        }

	}
}