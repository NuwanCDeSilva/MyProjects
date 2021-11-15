using CrystalDecisions.CrystalReports.Engine;
using FF.BusinessObjects;
using FF.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastForward.WebAbansTours.Controllers
{
    public class CreditNoteController : BaseController
    {
        //
        // GET: /CreditNote/

        List<InvoiceItem> oMainItemsList;
        private InvoiceHeader oHeader = null;
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                Session["oMainItemsList"] = null;
                Session["RecieptItemList"] = null;
                return View();
            }
            else
            {
                return Redirect("~/Login/index");
            }
        }

        public JsonResult getInvoiceDetails(string invNo)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                Session["oMainItemsList"] = null;
                InvoiceHeader invHed = CHNLSVC.Tours.getInvoiceHederData(invNo, company, userDefPro);
                List<InvoiceItem> invdetails = CHNLSVC.Sales.GetInvoiceHeaderDetailsList(invNo).ToList();
                List<InvoiceItem> refindatarev = CHNLSVC.Sales.GetRefInvHeaderDetailsList(invNo, 0).ToList();
                List<InvoiceItem> refindatadeb = CHNLSVC.Sales.GetRefInvHeaderDetailsList(invNo, 1).ToList();
                foreach (var items in invdetails)
                {
                    if (refindatarev != null)
                    {
                        var pax = refindatarev.Where(a => a.Sad_itm_cd == items.Sad_itm_cd).Sum(a => a.Sad_qty);
                        if (pax != null)
                        {
                            items.Sad_qty = items.Sad_qty - pax;
                            items.Sad_tot_amt = items.Sad_qty * items.Sad_unit_rt;
                        }

                    }
                    if (refindatadeb != null)
                    {
                        var pax = refindatadeb.Where(a => a.Sad_itm_cd == items.Sad_itm_cd).Sum(a => a.Sad_qty);
                        if (pax != null)
                        {
                            items.Sad_qty = items.Sad_qty + pax;
                            items.Sad_tot_amt = items.Sad_qty * items.Sad_unit_rt;
                        }

                    }

                }

                Session["InvoiceHeader"] = invHed;
                return Json(new { success = true, login = true, invHed = invHed, invdetails = invdetails }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult updateTourCharges(string chgCd, string service, string currencyCode, int pax, string remarks, float dis, float discount, float unitrate, float Rate, float Markup, float Tax)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                if (Rate == 0)
                {
                    string chargCdDesc = "";


                    if (Session["oMainItemsList"] != null)
                    {
                        oMainItemsList = (List<InvoiceItem>)Session["oMainItemsList"];
                    }
                    else
                    {
                        oMainItemsList = new List<InvoiceItem>();

                    }
                    InvoiceItem oItem = new InvoiceItem();
                    oItem.Sad_itm_cd = chgCd;
                    oItem.Sad_alt_itm_desc = chargCdDesc;
                    oItem.Sad_itm_stus = "GOD";
                    oItem.Sad_qty = Convert.ToDecimal(pax);
                    int n;
                    bool isNumeric = int.TryParse(discount.ToString(), out n);
                    if (isNumeric == true)
                    {
                        oItem.Sad_disc_amt = Convert.ToDecimal(discount);
                    }
                    else
                    {
                        oItem.Sad_disc_amt = 0;
                    }


                    oItem.Sad_tot_amt = Convert.ToDecimal(pax) * Convert.ToDecimal(unitrate + Tax) - Convert.ToDecimal(discount);
                    oItem.Sad_unit_rt = Convert.ToDecimal(unitrate + Tax);
                    oItem.Sad_unit_amt = Convert.ToDecimal(unitrate + Tax) * Convert.ToDecimal(pax);
                    oItem.Sad_print_stus = true;
                    oItem.SII_CURR = currencyCode;
                    oItem.SII_EX_RT = Convert.ToDecimal(1);
                    oItem.Sad_warr_remarks = remarks;
                    oMainItemsList.Add(oItem);
                    Session["oMainItemsList"] = oMainItemsList;

                    return Json(new { success = true, login = true, oMainItemsList = oMainItemsList }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    if (Markup != 0)
                    {
                        string chargCdDesc = "";


                        if (Session["oMainItemsList"] != null)
                        {
                            oMainItemsList = (List<InvoiceItem>)Session["oMainItemsList"];
                        }
                        else
                        {
                            oMainItemsList = new List<InvoiceItem>();

                        }
                        InvoiceItem oItem = new InvoiceItem();
                        oItem.Sad_itm_cd = chgCd;
                        oItem.Sad_alt_itm_desc = chargCdDesc;
                        oItem.Sad_itm_stus = "GOD";
                        oItem.Sad_qty = Convert.ToDecimal(pax);

                        oItem.Sad_tot_amt = Convert.ToDecimal(Markup);
                        oItem.Sad_unit_rt = Convert.ToDecimal(Markup) / Convert.ToDecimal(pax);
                        oItem.Sad_unit_amt = oItem.Sad_unit_rt * Convert.ToDecimal(pax);
                        oItem.Sad_print_stus = true;
                        oItem.SII_CURR = currencyCode;
                        oItem.SII_EX_RT = Convert.ToDecimal(Rate);
                        oItem.Sad_warr_remarks = remarks;
                        oMainItemsList.Add(oItem);
                        Session["oMainItemsList"] = oMainItemsList;

                        return Json(new { success = true, login = true, oMainItemsList = oMainItemsList }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        string chargCdDesc = "";


                        if (Session["oMainItemsList"] != null)
                        {
                            oMainItemsList = (List<InvoiceItem>)Session["oMainItemsList"];
                        }
                        else
                        {
                            oMainItemsList = new List<InvoiceItem>();

                        }
                        InvoiceItem oItem = new InvoiceItem();
                        oItem.Sad_itm_cd = chgCd;
                        oItem.Sad_alt_itm_desc = chargCdDesc;
                        oItem.Sad_itm_stus = "GOD";
                        oItem.Sad_qty = Convert.ToDecimal(pax);
                        oItem.Sad_disc_amt = Convert.ToDecimal(discount);
                        oItem.Sad_itm_tax_amt = Convert.ToDecimal(Tax);
                        oItem.Sad_tot_amt = Convert.ToDecimal(pax) * Convert.ToDecimal(unitrate + Tax) - Convert.ToDecimal(discount);
                        oItem.Sad_unit_rt = (oItem.Sad_qty != 0) ? oItem.Sad_tot_amt / oItem.Sad_qty : oItem.Sad_qty;
                        oItem.Sad_unit_amt = Convert.ToDecimal(unitrate + Tax) * Convert.ToDecimal(pax);
                        //oItem.Sad_tot_amt = Convert.ToDecimal(unitrate) * Convert.ToDecimal(pax);
                        //oItem.Sad_unit_rt = Convert.ToDecimal(unitrate) ;
                        //oItem.Sad_unit_amt = oItem.Sad_unit_rt * Convert.ToDecimal(pax);
                        oItem.Sad_print_stus = true;
                        oItem.SII_CURR = currencyCode;
                        oItem.SII_EX_RT = Convert.ToDecimal(Rate);
                        oItem.Sad_warr_remarks = remarks;
                        oMainItemsList.Add(oItem);
                        Session["oMainItemsList"] = oMainItemsList;

                        return Json(new { success = true, login = true, oMainItemsList = oMainItemsList }, JsonRequestBehavior.AllowGet);
                    }

                }


            }
            else
            {
                return Json(new { success = false, login = true, data = Resource.ServerError }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult removeChargeItem(string chgCd, string pax, string totcst)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {

                if (Session["oMainItemsList"] != null)
                {
                    oMainItemsList = (List<InvoiceItem>)Session["oMainItemsList"];
                }
                else
                {
                    oMainItemsList = new List<InvoiceItem>();

                }
                Int32 pax1 = Convert.ToInt32(pax);
                decimal totcst1 = Convert.ToDecimal(totcst);
                var itemToRemove = oMainItemsList.First(r => r.Sad_itm_cd == chgCd && r.Sad_qty == pax1 && r.Sad_tot_amt == totcst1);
                oMainItemsList.Remove(itemToRemove);
                Session["oMainItemsList"] = oMainItemsList;
                return Json(new { success = true, login = true, oMainItemsList = oMainItemsList }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = true, data = Resource.ServerError }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult cusCodeTextChanged(string val)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (!string.IsNullOrEmpty(val))
                    {
                        Session["_isExsit"] = "false";
                        Session["_isGroup"] = "false";
                        MasterBusinessEntity custProf = GetbyCustCD(val.Trim());
                        if (custProf.Mbe_cd != null && custProf.Mbe_act == true)
                        {
                            Session["_isExsit"] = "true";
                            return Json(new { success = true, local = true, login = true, data = custProf }, JsonRequestBehavior.AllowGet);
                        }
                        else if (custProf.Mbe_cd != null && custProf.Mbe_act == false)
                        {
                            return Json(new { success = false, login = true, msg = Resource.txtInacCus, data = "" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            GroupBussinessEntity _grupProf = GetbyCustCDGrup(val.Trim());
                            if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                            {
                                Session["_isExsit"] = "true";
                                Session["_isGroup"] = "true";
                                return Json(new { success = true, group = true, login = true, data = _grupProf }, JsonRequestBehavior.AllowGet);
                            }
                            else if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == false)
                            {
                                return Json(new { success = false, login = true, msg = Resource.txtInacCus, type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = Resource.invalidCusCd, type = "Info" }, JsonRequestBehavior.AllowGet);
                            }


                        }
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
                    return Json(new { success = false, login = true, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public MasterBusinessEntity GetbyCustCD(string custCD)
        {
            return CHNLSVC.Sales.GetCustomerProfileByCom(custCD, null, null, null, null, HttpContext.Session["UserCompanyCode"] as string);
        }
        public GroupBussinessEntity GetbyCustCDGrup(string custCD)
        {
            return CHNLSVC.Sales.GetCustomerProfileByGrup(custCD, null, null, null, null, null);
        }
        public JsonResult SaveCreditNote(InvoiceHeader inv, string cnno, int iscreditnote)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    cnno = cnno.Trim();
                    string _invoiceNo = string.Empty;
                    if (iscreditnote == 1)
                    {
                        List<InvoiceHeader> invData = CHNLSVC.Tours.GetInvoiceHDRData(cnno);
                        if (invData.Count > 0)
                        {
                            InvoiceHeader invoice = invData[0];
                            invoice.Sah_mod_by = userId;
                            if (invoice.Sah_inv_no != null)
                            {
                                if (invoice.Sah_stus != "C" && invoice.Sah_stus != "R")
                                {

                                    MasterBusinessEntity _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(invoice.Sah_cus_cd, null, null, null, null, company);
                                    Session["Customer"] = _masterBusinessCompany;
                                    MasterBusinessEntity oCust = (MasterBusinessEntity)Session["Customer"];
                                    InvoiceHeader oHeader = new InvoiceHeader();
                                    oHeader.Sah_com = company;
                                    oHeader.Sah_cre_by = userId;
                                    oHeader.Sah_cre_when = DateTime.Now;
                                    oHeader.Sah_currency = invoice.Sah_currency;
                                    oHeader.Sah_cus_add1 = oCust.Mbe_add1;
                                    oHeader.Sah_cus_add2 = oCust.Mbe_add2;
                                    oHeader.Sah_cus_cd = oCust.Mbe_cd;
                                    oHeader.Sah_cus_name = oCust.Mbe_name;
                                    oHeader.Sah_d_cust_add1 = oCust.Mbe_add1;
                                    oHeader.Sah_d_cust_add2 = oCust.Mbe_add2;
                                    oHeader.Sah_d_cust_cd = oCust.Mbe_cd;
                                    oHeader.Sah_d_cust_name = oCust.MBE_FNAME;
                                    //oHeader.Sah_direct = true;
                                    oHeader.Sah_dt = DateTime.Now.Date;
                                    oHeader.Sah_epf_rt = 0;
                                    oHeader.Sah_esd_rt = 0;
                                    oHeader.Sah_ex_rt = 1;
                                    oHeader.Sah_inv_no = "na";
                                    oHeader.Sah_inv_sub_tp = "SA";
                                    oHeader.Sah_inv_tp = invoice.Sah_inv_tp;
                                    oHeader.Sah_is_acc_upload = false;
                                    oHeader.Sah_manual = false;
                                    oHeader.Sah_mod_by = userId;
                                    oHeader.Sah_mod_when = DateTime.Now;
                                    oHeader.Sah_pc = userDefPro;
                                    oHeader.Sah_pdi_req = 0;
                                    //oHeader.Sah_ref_doc = invNo;this set on service
                                    oHeader.Sah_anal_4 = cnno;
                                    oHeader.Sah_sales_chn_cd = "";
                                    oHeader.Sah_sales_chn_man = "";
                                    oHeader.Sah_sales_ex_cd = invoice.Sah_sales_ex_cd;
                                    oHeader.Sah_sales_region_cd = "";
                                    oHeader.Sah_sales_region_man = "";
                                    oHeader.Sah_sales_sbu_cd = "";
                                    oHeader.Sah_sales_sbu_man = "";
                                    oHeader.Sah_sales_str_cd = "";
                                    oHeader.Sah_sales_zone_cd = "";
                                    oHeader.Sah_sales_zone_man = "";
                                    oHeader.Sah_seq_no = 1;
                                    oHeader.Sah_session_id = Session["SessionID"].ToString();
                                    oMainItemsList = (List<InvoiceItem>)Session["oMainItemsList"];
                                    InvoiceHeader invHed = CHNLSVC.Tours.getInvoiceHederData(invoice.Sah_inv_no, company, userDefPro);
                                    decimal TOT = oMainItemsList.Sum(A => A.Sad_tot_amt);
                                    if (invHed.Sah_anal_8 + TOT >= invHed.Sah_anal_7)
                                    {
                                        oHeader.Sah_stus = "R";
                                        oHeader.Sah_anal_7 = TOT;
                                        oHeader.Sah_anal_8 = invHed.Sah_anal_8 + TOT;
                                    }
                                    else
                                    {
                                        oHeader.Sah_stus = "A";
                                        oHeader.Sah_anal_8 = invHed.Sah_anal_8 + TOT;
                                        oHeader.Sah_anal_7 = TOT;
                                    }


                                    oHeader.Sah_town_cd = "";
                                    oHeader.Sah_tp = invoice.Sah_tp;
                                    oHeader.Sah_wht_rt = 0;
                                    oHeader.Sah_direct = false;
                                    oHeader.Sah_tax_inv = invoice.Sah_tax_inv;
                                    oHeader.Sah_del_loc = string.Empty;
                                    oHeader.Sah_anal_2 = invoice.Sah_anal_2;
                                    oHeader.Sah_anal_3 = invoice.Sah_anal_3;
                                    //oHeader.Sah_anal_6 = txtLoyalty.Text.Trim();
                                    oHeader.Sah_man_cd = userDefPro;
                                    oHeader.Sah_is_dayend = 0;
                                    oHeader.Sah_remarks = invoice.Sah_remarks;


                                    List<InvoiceItem> oMainItemsList2;
                                    oMainItemsList2 = new List<InvoiceItem>();
                                    int j = 0;
                                    foreach (InvoiceItem invtb in oMainItemsList)
                                    {
                                        InvoiceItem om = new InvoiceItem();
                                        invtb.Sad_res_line_no = j;
                                        om.Sad_alt_itm_cd = invtb.Sad_alt_itm_cd;
                                        om.Sad_alt_itm_desc = invtb.Sad_alt_itm_desc;
                                        om.Sad_comm_amt = invtb.Sad_comm_amt;
                                        om.Sad_disc_amt = invtb.Sad_disc_amt;
                                        om.Sad_disc_rt = invtb.Sad_disc_rt;
                                        om.Sad_do_qty = invtb.Sad_do_qty;
                                        om.Sad_fws_ignore_qty = invtb.Sad_fws_ignore_qty;
                                        om.Sad_inv_no = invtb.Sad_inv_no;
                                        om.Sad_is_promo = invtb.Sad_is_promo;
                                        om.Sad_itm_cd = invtb.Sad_itm_cd;
                                        om.Sad_itm_line = j;
                                        om.Sad_itm_seq = invtb.Sad_itm_seq;
                                        om.Sad_itm_stus = invtb.Sad_itm_stus;
                                        om.Sad_itm_tax_amt = invtb.Sad_itm_tax_amt;
                                        om.Sad_itm_tp = invtb.Sad_itm_tp;
                                        om.Sad_job_line = invtb.Sad_job_line;
                                        om.Sad_job_no = invtb.Sad_job_no;
                                        om.Sad_merge_itm = invtb.Sad_merge_itm;
                                        om.Sad_outlet_dept = invtb.Sad_outlet_dept;
                                        om.Sad_pbook = invtb.Sad_pbook;
                                        om.Sad_pb_lvl = invtb.Sad_pb_lvl;
                                        om.Sad_pb_price = invtb.Sad_pb_price;
                                        om.Sad_print_stus = invtb.Sad_print_stus;
                                        om.Sad_promo_cd = invtb.Sad_promo_cd;
                                        om.Sad_qty = invtb.Sad_qty;
                                        om.Sad_res_line_no = invtb.Sad_res_line_no;
                                        om.Sad_res_no = invtb.Sad_res_no;
                                        om.Sad_seq = invtb.Sad_seq;
                                        om.Sad_seq_no = invtb.Sad_seq_no;
                                        om.Sad_sim_itm_cd = invtb.Sad_sim_itm_cd;
                                        om.Sad_srn_qty = invtb.Sad_srn_qty;
                                        om.Sad_tot_amt = invtb.Sad_tot_amt;
                                        om.Sad_trd_svc_chrg = invtb.Sad_trd_svc_chrg;
                                        om.Sad_unit_amt = invtb.Sad_unit_amt;
                                        om.Sad_unit_rt = invtb.Sad_unit_rt;
                                        om.Sad_uom = invtb.Sad_uom;
                                        om.Sad_warr_based = invtb.Sad_warr_based;
                                        om.Sad_warr_period = invtb.Sad_warr_period;
                                        om.Sad_warr_remarks = invtb.Sad_warr_remarks;
                                        om.Sad_isapp = invtb.Sad_isapp;
                                        om.Sad_iscovernote = invtb.Sad_iscovernote;
                                        om.Sad_dis_line = invtb.Sad_dis_line;
                                        om.Sad_dis_seq = invtb.Sad_dis_seq;
                                        om.Sad_dis_type = invtb.Sad_dis_type;
                                        om.SII_EX_RT = invtb.SII_EX_RT;
                                        om.SII_CURR = invtb.SII_CURR;
                                        om.Sad_conf_no = invtb.Sad_conf_no;
                                        om.Sad_conf_line = invtb.Sad_conf_line;
                                        om.Sad_itm_stus_desc = invtb.Sad_itm_stus_desc;
                                        om.Sad_pbook = invtb.Sad_pbook;
                                        om.Sad_pb_lvl = invtb.Sad_pb_lvl;
                                        oMainItemsList2.Add(om);
                                        j++;
                                    }


                                    MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
                                    _invoiceAuto.Aut_cate_cd = Session["UserDefProf"].ToString();
                                    _invoiceAuto.Aut_cate_tp = "PC";
                                    _invoiceAuto.Aut_direction = 1;
                                    _invoiceAuto.Aut_modify_dt = null;
                                    _invoiceAuto.Aut_moduleid = "REV";
                                    _invoiceAuto.Aut_number = 0;
                                    _invoiceAuto.Aut_start_char = "INREV";
                                    _invoiceAuto.Aut_year = DateTime.Now.Year;


                                    string _error;


                                    List<InvoiceSerial> _invoiceSerial = new List<InvoiceSerial>();
                                    InventoryHeader _inventoryHeader = new InventoryHeader();
                                    List<ReptPickSerials> _pickSerial = new List<ReptPickSerials>();
                                    List<ReptPickSerialsSub> _pickSubSerial = new List<ReptPickSerialsSub>();

                                    MasterAutoNumber _inventoryAuto = new MasterAutoNumber();
                                    List<InvoiceVoucher> _voucher = new List<InvoiceVoucher>();
                                    InventoryHeader _buybackheader = new InventoryHeader();
                                    MasterAutoNumber _buybackauto = new MasterAutoNumber();
                                    List<ReptPickSerials> _buybacklist = new List<ReptPickSerials>();



                                    int result = CHNLSVC.Tours.SaveCreditNote(oHeader, oMainItemsList2, _invoiceAuto, out _invoiceNo, oCust, out _error, null, invoice);
                                    Session["cnno"] = _invoiceNo;
                                }
                                else
                                {
                                    if (invoice.Sah_stus == "C")
                                    {
                                        return Json(new { success = false, login = true, msg = "Cannot save, canceled invoice.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                    }
                                    if (invoice.Sah_stus == "R")
                                    {
                                        return Json(new { success = false, login = true, msg = "Already revered invoice.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = "Please enter valid invoice number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            //custom credit note
                            MasterBusinessEntity _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(inv.Sah_cus_cd, null, null, null, null, company);
                            Session["Customer"] = _masterBusinessCompany;
                            MasterBusinessEntity oCust = (MasterBusinessEntity)Session["Customer"];
                            InvoiceHeader oHeader = new InvoiceHeader();
                            oHeader.Sah_com = company;
                            oHeader.Sah_cre_by = userId;
                            oHeader.Sah_cre_when = DateTime.Now;
                            oHeader.Sah_currency = inv.Sah_currency;
                            oHeader.Sah_cus_add1 = oCust.Mbe_add1;
                            oHeader.Sah_cus_add2 = oCust.Mbe_add2;
                            oHeader.Sah_cus_cd = oCust.Mbe_cd;
                            oHeader.Sah_cus_name = oCust.Mbe_name;
                            oHeader.Sah_d_cust_add1 = oCust.Mbe_add1;
                            oHeader.Sah_d_cust_add2 = oCust.Mbe_add2;
                            oHeader.Sah_d_cust_cd = oCust.Mbe_cd;
                            oHeader.Sah_d_cust_name = oCust.MBE_FNAME;
                           // oHeader.Sah_direct = true;
                            oHeader.Sah_dt = DateTime.Now.Date;
                            oHeader.Sah_epf_rt = 0;
                            oHeader.Sah_esd_rt = 0;
                            oHeader.Sah_ex_rt = 1;
                            oHeader.Sah_inv_no = "na";
                            oHeader.Sah_inv_sub_tp = "SA";
                            oHeader.Sah_inv_tp = "CRED";
                            oHeader.Sah_is_acc_upload = false;
                            oHeader.Sah_manual = false;
                            oHeader.Sah_mod_by = userId;
                            oHeader.Sah_mod_when = DateTime.Now;
                            oHeader.Sah_pc = userDefPro;
                            oHeader.Sah_pdi_req = 0;
                            //oHeader.Sah_ref_doc = invNo;this set on service
                            oHeader.Sah_anal_4 = cnno;
                            oHeader.Sah_sales_chn_cd = "";
                            oHeader.Sah_sales_chn_man = "";
                            oHeader.Sah_sales_ex_cd = inv.Sah_sales_ex_cd;
                            oHeader.Sah_sales_region_cd = "";
                            oHeader.Sah_sales_region_man = "";
                            oHeader.Sah_sales_sbu_cd = "";
                            oHeader.Sah_sales_sbu_man = "";
                            oHeader.Sah_sales_str_cd = "";
                            oHeader.Sah_sales_zone_cd = "";
                            oHeader.Sah_sales_zone_man = "";
                            oHeader.Sah_seq_no = 1;
                            oHeader.Sah_session_id = Session["SessionID"].ToString();
                            oHeader.Sah_stus = "R";
                            oHeader.Sah_town_cd = "";
                            oHeader.Sah_tp = "REV";
                            oHeader.Sah_wht_rt = 0;
                            oHeader.Sah_direct = false;
                            oHeader.Sah_tax_inv = inv.Sah_tax_inv;
                            oHeader.Sah_del_loc = string.Empty;
                            oHeader.Sah_anal_2 = inv.Sah_anal_2;
                            oHeader.Sah_anal_3 = inv.Sah_anal_3;
                            //oHeader.Sah_anal_6 = txtLoyalty.Text.Trim();
                            oHeader.Sah_man_cd = userDefPro;
                            oHeader.Sah_is_dayend = 0;
                            oHeader.Sah_remarks = inv.Sah_remarks;
                            oHeader.Sah_mod_by = userId;

                            oMainItemsList = (List<InvoiceItem>)Session["oMainItemsList"];
                            List<InvoiceItem> oMainItemsList2;
                            oMainItemsList2 = new List<InvoiceItem>();
                            int j = 0;
                            foreach (InvoiceItem invtb in oMainItemsList)
                            {
                                InvoiceItem om = new InvoiceItem();
                                invtb.Sad_res_line_no = j;
                                om.Sad_alt_itm_cd = invtb.Sad_alt_itm_cd;
                                om.Sad_alt_itm_desc = invtb.Sad_alt_itm_desc;
                                om.Sad_comm_amt = invtb.Sad_comm_amt;
                                om.Sad_disc_amt = invtb.Sad_disc_amt;
                                om.Sad_disc_rt = invtb.Sad_disc_rt;
                                om.Sad_do_qty = invtb.Sad_do_qty;
                                om.Sad_fws_ignore_qty = invtb.Sad_fws_ignore_qty;
                                om.Sad_inv_no = invtb.Sad_inv_no;
                                om.Sad_is_promo = invtb.Sad_is_promo;
                                om.Sad_itm_cd = invtb.Sad_itm_cd;
                                om.Sad_itm_line = j;
                                om.Sad_itm_seq = invtb.Sad_itm_seq;
                                om.Sad_itm_stus = invtb.Sad_itm_stus;
                                om.Sad_itm_tax_amt = invtb.Sad_itm_tax_amt;
                                om.Sad_itm_tp = invtb.Sad_itm_tp;
                                om.Sad_job_line = invtb.Sad_job_line;
                                om.Sad_job_no = invtb.Sad_job_no;
                                om.Sad_merge_itm = invtb.Sad_merge_itm;
                                om.Sad_outlet_dept = invtb.Sad_outlet_dept;
                                om.Sad_pbook = invtb.Sad_pbook;
                                om.Sad_pb_lvl = invtb.Sad_pb_lvl;
                                om.Sad_pb_price = invtb.Sad_pb_price;
                                om.Sad_print_stus = invtb.Sad_print_stus;
                                om.Sad_promo_cd = invtb.Sad_promo_cd;
                                om.Sad_qty = invtb.Sad_qty;
                                om.Sad_res_line_no = invtb.Sad_res_line_no;
                                om.Sad_res_no = invtb.Sad_res_no;
                                om.Sad_seq = invtb.Sad_seq;
                                om.Sad_seq_no = invtb.Sad_seq_no;
                                om.Sad_sim_itm_cd = invtb.Sad_sim_itm_cd;
                                om.Sad_srn_qty = invtb.Sad_srn_qty;
                                om.Sad_tot_amt = invtb.Sad_tot_amt;
                                om.Sad_trd_svc_chrg = invtb.Sad_trd_svc_chrg;
                                om.Sad_unit_amt = invtb.Sad_unit_amt;
                                om.Sad_unit_rt = invtb.Sad_unit_rt;
                                om.Sad_uom = invtb.Sad_uom;
                                om.Sad_warr_based = invtb.Sad_warr_based;
                                om.Sad_warr_period = invtb.Sad_warr_period;
                                om.Sad_warr_remarks = invtb.Sad_warr_remarks;
                                om.Sad_isapp = invtb.Sad_isapp;
                                om.Sad_iscovernote = invtb.Sad_iscovernote;
                                om.Sad_dis_line = invtb.Sad_dis_line;
                                om.Sad_dis_seq = invtb.Sad_dis_seq;
                                om.Sad_dis_type = invtb.Sad_dis_type;
                                om.SII_EX_RT = invtb.SII_EX_RT;
                                om.SII_CURR = invtb.SII_CURR;
                                om.Sad_conf_no = invtb.Sad_conf_no;
                                om.Sad_conf_line = invtb.Sad_conf_line;
                                om.Sad_itm_stus_desc = invtb.Sad_itm_stus_desc;
                                om.Sad_pbook = invtb.Sad_pbook;
                                om.Sad_pb_lvl = invtb.Sad_pb_lvl;
                                oMainItemsList2.Add(om);
                                j++;
                            }


                            MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
                            _invoiceAuto.Aut_cate_cd = Session["UserDefProf"].ToString();
                            _invoiceAuto.Aut_cate_tp = "PC";
                            _invoiceAuto.Aut_direction = 1;
                            _invoiceAuto.Aut_modify_dt = null;
                            _invoiceAuto.Aut_moduleid = "REV";
                            _invoiceAuto.Aut_number = 0;
                            _invoiceAuto.Aut_start_char = "INREV";
                            _invoiceAuto.Aut_year =DateTime.Now.Year;


                            string _error;


                            List<InvoiceSerial> _invoiceSerial = new List<InvoiceSerial>();
                            InventoryHeader _inventoryHeader = new InventoryHeader();
                            List<ReptPickSerials> _pickSerial = new List<ReptPickSerials>();
                            List<ReptPickSerialsSub> _pickSubSerial = new List<ReptPickSerialsSub>();

                            MasterAutoNumber _inventoryAuto = new MasterAutoNumber();
                            List<InvoiceVoucher> _voucher = new List<InvoiceVoucher>();
                            InventoryHeader _buybackheader = new InventoryHeader();
                            MasterAutoNumber _buybackauto = new MasterAutoNumber();
                            List<ReptPickSerials> _buybacklist = new List<ReptPickSerials>();



                            int result = CHNLSVC.Tours.SaveCreditNote(oHeader, oMainItemsList2, _invoiceAuto, out _invoiceNo, oCust, out _error, null, inv);
                            Session["cnno"] = _invoiceNo;
                        }
                    }
                    else
                    {
                        //debit note
                        return Json(new { success = false, login = true, msg = "Invalid invoice number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    return Json(new { success = true, login = true, msg = "Successfully saved the invoice " + _invoiceNo, type = "Success", no = _invoiceNo }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = Resource.ServerError, type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult DebitNoteSave(InvoiceHeader inv, string dbno)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {

                if (dbno != null)
                {
                    dbno = dbno.Trim();
                    string _invoiceNo = string.Empty;
                    List<InvoiceHeader> invData = CHNLSVC.Tours.GetInvoiceHDRData(dbno);
                    InvoiceHeader invoice = invData[0];
                    invoice.Sah_mod_by = userId;
                    if (invoice.Sah_inv_no != null)
                    {
                        if (invoice.Sah_stus != "C" && invoice.Sah_stus != "R")
                        {

                            MasterBusinessEntity _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(invoice.Sah_cus_cd, null, null, null, null, company);
                            Session["Customer"] = _masterBusinessCompany;
                            MasterBusinessEntity oCust = (MasterBusinessEntity)Session["Customer"];
                            InvoiceHeader oHeader = new InvoiceHeader();
                            oHeader.Sah_com = company;
                            oHeader.Sah_cre_by = userId;
                            oHeader.Sah_cre_when = DateTime.Now;
                            oHeader.Sah_currency = invoice.Sah_currency;
                            oHeader.Sah_cus_add1 = oCust.Mbe_add1;
                            oHeader.Sah_cus_add2 = oCust.Mbe_add2;
                            oHeader.Sah_cus_cd = oCust.Mbe_cd;
                            oHeader.Sah_cus_name = oCust.Mbe_name;
                            oHeader.Sah_d_cust_add1 = oCust.Mbe_add1;
                            oHeader.Sah_d_cust_add2 = oCust.Mbe_add2;
                            oHeader.Sah_d_cust_cd = oCust.Mbe_cd;
                            oHeader.Sah_d_cust_name = oCust.MBE_FNAME;
                            oHeader.Sah_direct = true;
                            oHeader.Sah_dt = DateTime.Now.Date;
                            oHeader.Sah_epf_rt = 0;
                            oHeader.Sah_esd_rt = 0;
                            oHeader.Sah_ex_rt = 1;
                            oHeader.Sah_inv_no = "";
                            oHeader.Sah_inv_sub_tp = "SA";
                            oHeader.Sah_inv_tp = "DEBT";
                            oHeader.Sah_is_acc_upload = false;
                            oHeader.Sah_manual = false;
                            oHeader.Sah_mod_by = userId;
                            oHeader.Sah_mod_when = DateTime.Now;
                            oHeader.Sah_pc = userDefPro;
                            oHeader.Sah_pdi_req = 0;
                            //oHeader.Sah_ref_doc = invNo;this set on service
                            oHeader.Sah_anal_4 = dbno;
                            oHeader.Sah_sales_chn_cd = "";
                            oHeader.Sah_sales_chn_man = "";
                            oHeader.Sah_sales_ex_cd = invoice.Sah_sales_ex_cd;
                            oHeader.Sah_sales_region_cd = "";
                            oHeader.Sah_sales_region_man = "";
                            oHeader.Sah_sales_sbu_cd = "";
                            oHeader.Sah_sales_sbu_man = "";
                            oHeader.Sah_sales_str_cd = "";
                            oHeader.Sah_sales_zone_cd = "";
                            oHeader.Sah_sales_zone_man = "";
                            oHeader.Sah_seq_no = 1;
                            oHeader.Sah_ref_doc = invoice.Sah_inv_no;
                            oHeader.Sah_session_id = Session["SessionID"].ToString();
                            oMainItemsList = (List<InvoiceItem>)Session["oMainItemsList"];
                            InvoiceHeader invHed = CHNLSVC.Tours.getInvoiceHederData(invoice.Sah_inv_no, company, userDefPro);
                            decimal TOT = oMainItemsList.Sum(A => A.Sad_tot_amt);

                            oHeader.Sah_stus = "A";
                            oHeader.Sah_anal_8 = 0;
                            oHeader.Sah_anal_7 = TOT;
                            oHeader.With_INV_DBNT = invoice.Sah_inv_no;


                            oHeader.Sah_town_cd = "";
                            oHeader.Sah_tp = invoice.Sah_tp;
                            oHeader.Sah_wht_rt = 0;
                            //oHeader.Sah_direct = false;
                            oHeader.Sah_tax_inv = invoice.Sah_tax_inv;
                            oHeader.Sah_del_loc = string.Empty;
                            oHeader.Sah_anal_2 = invoice.Sah_anal_2;
                            oHeader.Sah_anal_3 = invoice.Sah_anal_3;
                            //oHeader.Sah_anal_6 = txtLoyalty.Text.Trim();
                            oHeader.Sah_man_cd = userDefPro;
                            oHeader.Sah_is_dayend = 0;
                            oHeader.Sah_remarks = invoice.Sah_remarks;

                            List<RecieptItemTBS> _recieptItemtbs = (List<RecieptItemTBS>)Session["RecieptItemList"];
                            if (_recieptItemtbs == null)
                            {
                                _recieptItemtbs = new List<RecieptItemTBS>();
                            }
                            if (oHeader.Sah_inv_tp != "CRED" && _recieptItemtbs.Count == 0)
                            {
                                if (oHeader.Sah_inv_tp != "DEBT")
                                {
                                    return Json(new { success = false, login = true, msg = "Please Add Payment", type = "Error" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    oHeader.Sah_anal_8 = 0;
                                }

                            }
                            else
                            {
                                oHeader.Sah_anal_8 = TOT;
                            }
                            List<RecieptItem> _recieptItem = new List<RecieptItem>();
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


                            RecieptHeader _ReceiptHeader = new RecieptHeader();
                            _ReceiptHeader.Sar_seq_no = -1; //CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "RECEIPT", 1, BaseCls.GlbUserComCode);
                            _ReceiptHeader.Sar_com_cd = company;
                            _ReceiptHeader.Sar_receipt_type = "VHREG";
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
                            _ReceiptHeader.Sar_receipt_date = Convert.ToDateTime(inv.Sah_dt).Date;
                            _ReceiptHeader.Sar_direct = true;
                            _ReceiptHeader.Sar_acc_no = "";
                            _ReceiptHeader.Sar_is_oth_shop = false;
                            _ReceiptHeader.Sar_oth_sr = "";
                            _ReceiptHeader.Sar_profit_center_cd = userDefPro;
                            _ReceiptHeader.Sar_debtor_cd = inv.Sah_cus_cd;
                            _ReceiptHeader.Sar_debtor_name = oCust.Mbe_name;
                            _ReceiptHeader.Sar_debtor_add_1 = oCust.Mbe_add1;
                            _ReceiptHeader.Sar_debtor_add_2 = oCust.Mbe_add2;
                            _ReceiptHeader.Sar_tel_no = "";
                            _ReceiptHeader.Sar_mob_no = Request["cus_mobile"];
                            _ReceiptHeader.Sar_nic_no = oCust.Mbe_nic;

                            if (inv.Sah_inv_no != "CRED")
                            {
                                _ReceiptHeader.Sar_tot_settle_amt = _recieptItem.Sum(x => x.Sard_settle_amt);
                            }
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
                            _ReceiptHeader.Sar_anal_1 = oCust.Mbe_cr_distric_cd;
                            _ReceiptHeader.Sar_anal_2 = oCust.Mbe_cr_province_cd;
                            string pricebook = Request["PriceBook"].ToString();
                            string priceLevel = Request["PriceLevel"].ToString();
                            //string packagetype = Request["PackageType"].ToString();



                            List<InvoiceItem> oMainItemsList2;
                            oMainItemsList2 = new List<InvoiceItem>();
                            int j = 0;
                            foreach (InvoiceItem invtb in oMainItemsList)
                            {
                                InvoiceItem om = new InvoiceItem();
                                invtb.Sad_res_line_no = j;
                                om.Sad_alt_itm_cd = invtb.Sad_alt_itm_cd;
                                om.Sad_alt_itm_desc = invtb.Sad_alt_itm_desc;
                                om.Sad_comm_amt = invtb.Sad_comm_amt;
                                om.Sad_disc_amt = invtb.Sad_disc_amt;
                                om.Sad_disc_rt = invtb.Sad_disc_rt;
                                om.Sad_do_qty = invtb.Sad_do_qty;
                                om.Sad_fws_ignore_qty = invtb.Sad_fws_ignore_qty;
                                om.Sad_inv_no = invtb.Sad_inv_no;
                                om.Sad_is_promo = invtb.Sad_is_promo;
                                om.Sad_itm_cd = invtb.Sad_itm_cd;
                                om.Sad_itm_line = j;
                                om.Sad_itm_seq = invtb.Sad_itm_seq;
                                om.Sad_itm_stus = invtb.Sad_itm_stus;
                                om.Sad_itm_tax_amt = invtb.Sad_itm_tax_amt;
                                om.Sad_itm_tp = invtb.Sad_itm_tp;
                                om.Sad_job_line = invtb.Sad_job_line;
                                om.Sad_job_no = invtb.Sad_job_no;
                                om.Sad_merge_itm = invtb.Sad_merge_itm;
                                om.Sad_outlet_dept = invtb.Sad_outlet_dept;
                                om.Sad_pbook = invtb.Sad_pbook;
                                om.Sad_pb_lvl = invtb.Sad_pb_lvl;
                                om.Sad_pb_price = invtb.Sad_pb_price;
                                om.Sad_print_stus = invtb.Sad_print_stus;
                                om.Sad_promo_cd = invtb.Sad_promo_cd;
                                om.Sad_qty = invtb.Sad_qty;
                                om.Sad_res_line_no = invtb.Sad_res_line_no;
                                om.Sad_res_no = invtb.Sad_res_no;
                                om.Sad_seq = invtb.Sad_seq;
                                om.Sad_seq_no = invtb.Sad_seq_no;
                                om.Sad_sim_itm_cd = invtb.Sad_sim_itm_cd;
                                om.Sad_srn_qty = invtb.Sad_srn_qty;
                                om.Sad_tot_amt = invtb.Sad_tot_amt;
                                om.Sad_trd_svc_chrg = invtb.Sad_trd_svc_chrg;
                                om.Sad_unit_amt = invtb.Sad_unit_amt;
                                om.Sad_unit_rt = invtb.Sad_unit_rt;
                                om.Sad_uom = invtb.Sad_uom;
                                om.Sad_warr_based = invtb.Sad_warr_based;
                                om.Sad_warr_period = invtb.Sad_warr_period;
                                om.Sad_warr_remarks = invtb.Sad_warr_remarks;
                                om.Sad_isapp = invtb.Sad_isapp;
                                om.Sad_iscovernote = invtb.Sad_iscovernote;
                                om.Sad_dis_line = invtb.Sad_dis_line;
                                om.Sad_dis_seq = invtb.Sad_dis_seq;
                                om.Sad_dis_type = invtb.Sad_dis_type;
                                om.SII_EX_RT = invtb.SII_EX_RT;
                                om.SII_CURR = invtb.SII_CURR;
                                om.Sad_conf_no = invtb.Sad_conf_no;
                                om.Sad_conf_line = invtb.Sad_conf_line;
                                om.Sad_itm_stus_desc = invtb.Sad_itm_stus_desc;
                                om.Sad_pbook = invtb.Sad_pbook;
                                om.Sad_pb_lvl = invtb.Sad_pb_lvl;
                                oMainItemsList2.Add(om);
                                j++;
                            }


                            MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
                            _invoiceAuto.Aut_cate_cd = Session["UserDefProf"].ToString();
                            _invoiceAuto.Aut_cate_tp = "PC";
                            _invoiceAuto.Aut_direction = 1;
                            _invoiceAuto.Aut_modify_dt = null;
                            _invoiceAuto.Aut_moduleid = "DEBT";
                            _invoiceAuto.Aut_number = 0;
                            _invoiceAuto.Aut_start_char = Session["UserDefProf"].ToString() + "-" + "DEBT" + "-";
                            _invoiceAuto.Aut_year = Convert.ToDateTime(inv.Sah_dt).Year;

                            MasterAutoNumber _receiptAuto = null;
                            if (_recieptItem != null)
                                if (_recieptItem.Count > 0)
                                {
                                    _receiptAuto = new MasterAutoNumber();
                                    _receiptAuto.Aut_cate_cd = userDefPro;
                                    _receiptAuto.Aut_cate_tp = "PRO";
                                    _receiptAuto.Aut_direction = 1;
                                    _receiptAuto.Aut_modify_dt = null;
                                    _receiptAuto.Aut_moduleid = "RECEIPT";
                                    _receiptAuto.Aut_number = 0;
                                    _receiptAuto.Aut_start_char = "DEBT";
                                    _receiptAuto.Aut_year = Convert.ToDateTime(inv.Sah_dt).Year;
                                }

                             _invoiceNo = String.Empty;
                            string _receiptNo;
                            string _deliveryOrder;
                            string _error;
                            string _buybackadj;

                            List<InvoiceSerial> _invoiceSerial = new List<InvoiceSerial>();
                            InventoryHeader _inventoryHeader = new InventoryHeader();
                            List<ReptPickSerials> _pickSerial = new List<ReptPickSerials>();
                            List<ReptPickSerialsSub> _pickSubSerial = new List<ReptPickSerialsSub>();

                            MasterAutoNumber _inventoryAuto = new MasterAutoNumber();
                            List<InvoiceVoucher> _voucher = new List<InvoiceVoucher>();
                            InventoryHeader _buybackheader = new InventoryHeader();
                            MasterAutoNumber _buybackauto = new MasterAutoNumber();
                            List<ReptPickSerials> _buybacklist = new List<ReptPickSerials>();



                          //  int result = CHNLSVC.Tours.SaveCreditNote(oHeader, oMainItemsList2, _invoiceAuto, out _invoiceNo, oCust, out _error, null, invoice);
                            int result = CHNLSVC.Tours.SaveToursrInvoiceDBNT(oHeader, oMainItemsList2, null, _ReceiptHeader, _recieptItem, null, null, null, _invoiceAuto, _receiptAuto, null, false, out _invoiceNo, out _receiptNo, out _deliveryOrder, oCust, false, false, out _error, null, null, null, null, out _buybackadj, false, null, null, null, null);
                            Session["dbno"] = _invoiceNo;


                            if (result == 1)
                            {
                                return Json(new { success = true, login = true, msg = "Successfully saved the invoice " + _invoiceNo, type = "Success", no = _invoiceNo }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = "Get Error", type = "Error" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            if (invoice.Sah_stus == "C")
                            {
                                return Json(new { success = false, login = true, msg = "Cannot save, canceled invoice.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            if (invoice.Sah_stus == "R")
                            {
                                return Json(new { success = false, login = true, msg = "Already revered invoice.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = "Please Check Invoice no.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Please enter valid invoice number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    MasterBusinessEntity _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(inv.Sah_cus_cd, null, null, null, null, company);
                    Session["Customer"] = _masterBusinessCompany;
                    MasterBusinessEntity oCust = (MasterBusinessEntity)Session["Customer"];
                    InvoiceHeader oHeader = new InvoiceHeader();
                    oHeader.Sah_com = company;
                    oHeader.Sah_cre_by = userId;
                    oHeader.Sah_cre_when = DateTime.Now;
                    oHeader.Sah_currency = "LKR";
                    oHeader.Sah_cus_add1 = oCust.Mbe_add1;
                    oHeader.Sah_cus_add2 = oCust.Mbe_add2;
                    oHeader.Sah_cus_cd = oCust.Mbe_cd;
                    oHeader.Sah_cus_name = oCust.Mbe_name;
                    oHeader.Sah_d_cust_add1 = oCust.Mbe_add1;
                    oHeader.Sah_d_cust_add2 = oCust.Mbe_add2;
                    oHeader.Sah_d_cust_cd = oCust.Mbe_cd;
                    oHeader.Sah_d_cust_name = oCust.MBE_FNAME;
                    oHeader.Sah_direct = true;
                    oHeader.Sah_dt = Convert.ToDateTime(inv.Sah_dt);
                    oHeader.Sah_epf_rt = 0;
                    oHeader.Sah_esd_rt = 0;
                    oHeader.Sah_ex_rt = 1;
                    oHeader.Sah_inv_no = "na";
                    oHeader.Sah_inv_sub_tp = "SA";
                    oHeader.Sah_inv_tp = "DEBT";
                    oHeader.Sah_is_acc_upload = false;
                    oHeader.Sah_manual = false;
                    oHeader.Sah_mod_by = userId;
                    oHeader.Sah_mod_when = DateTime.Now;
                    oHeader.Sah_pc = userDefPro;
                    oHeader.Sah_pdi_req = 0;
                    oHeader.Sah_man_ref = inv.Sah_man_ref;
                    oHeader.Sah_ref_doc = Request["enq_id"];
                    oHeader.Sah_sales_chn_cd = "";
                    oHeader.Sah_sales_chn_man = "";
                    oHeader.Sah_sales_ex_cd = inv.Sah_sales_ex_cd;
                    oHeader.Sah_sales_region_cd = "";
                    oHeader.Sah_sales_region_man = "";
                    oHeader.Sah_sales_sbu_cd = "";
                    oHeader.Sah_sales_sbu_man = "";
                    oHeader.Sah_sales_str_cd = "";
                    oHeader.Sah_sales_zone_cd = "";
                    oHeader.Sah_sales_zone_man = "";
                    oHeader.Sah_seq_no = 1;
                    oHeader.Sah_session_id = Session["SessionID"].ToString();
                    // oHeader.Sah_structure_seq = txtQuotation.Text.Trim();
                    oHeader.Sah_stus = "A";
                    //  if (chkDeliverLater.Checked == false || chkDeliverNow.Checked) oHeader.Sah_stus = "D";
                    oHeader.Sah_town_cd = "";
                    oHeader.Sah_tp = "INV";
                    oHeader.Sah_wht_rt = 0;
                    oHeader.Sah_direct = true;
                    oHeader.Sah_tax_inv = inv.Sah_tax_inv;
                    //oHeader.Sah_anal_11 = (chkDeliverLater.Checked || chkDeliverNow.Checked) ? 0 : 1;
                    //oHeader.Sah_del_loc = (chkDeliverLater.Checked == false || chkDeliverNow.Checked) ? BaseCls.GlbUserDefLoca : !string.IsNullOrEmpty(txtDelLocation.Text) ? txtDelLocation.Text : string.Empty;
                    oHeader.Sah_del_loc = string.Empty;
                    //oHeader.Sah_grn_com = _customerCompany;
                    //oHeader.Sah_grn_loc = _customerLocation;
                    //oHeader.Sah_is_grn = _isCustomerHasCompany;
                    //oHeader.Sah_grup_cd = string.IsNullOrEmpty(txtGroup.Text.Trim()) ? string.Empty : txtGroup.Text.Trim();
                    //   oHeader.Sah_is_svat = lblSVATStatus.Text == "Available" ? true : false;
                    //  oHeader.Sah_tax_exempted = lblExemptStatus.Text == "Available" ? true : false;
                    oHeader.Sah_anal_2 = "SCV";
                    oHeader.Sah_anal_3 = Request["PackageType"];
                    //oHeader.Sah_anal_6 = txtLoyalty.Text.Trim();
                    oHeader.Sah_man_cd = userDefPro;
                    oHeader.Sah_is_dayend = 0;
                    oHeader.Sah_remarks = inv.Sah_remarks;

                    List<RecieptItemTBS> _recieptItemtbs = (List<RecieptItemTBS>)Session["RecieptItemList"];
                    if (_recieptItemtbs == null)
                    {
                        _recieptItemtbs = new List<RecieptItemTBS>();
                    }
                    if (inv.Sah_inv_tp != "CRED" && _recieptItemtbs.Count == 0)
                    {
                        if (inv.Sah_inv_tp != "DEBT")
                        {
                            return Json(new { success = false, login = true, msg = "Please Add Payment", type = "Error" }, JsonRequestBehavior.AllowGet);
                        }

                    }
                    List<RecieptItem> _recieptItem = new List<RecieptItem>();
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


                    RecieptHeader _ReceiptHeader = new RecieptHeader();
                    _ReceiptHeader.Sar_seq_no = -1; //CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "RECEIPT", 1, BaseCls.GlbUserComCode);
                    _ReceiptHeader.Sar_com_cd = company;
                    _ReceiptHeader.Sar_receipt_type = "VHREG";
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
                    _ReceiptHeader.Sar_receipt_date = Convert.ToDateTime(inv.Sah_dt).Date;
                    _ReceiptHeader.Sar_direct = true;
                    _ReceiptHeader.Sar_acc_no = "";
                    _ReceiptHeader.Sar_is_oth_shop = false;
                    _ReceiptHeader.Sar_oth_sr = "";
                    _ReceiptHeader.Sar_profit_center_cd = userDefPro;
                    _ReceiptHeader.Sar_debtor_cd = inv.Sah_cus_cd;
                    _ReceiptHeader.Sar_debtor_name = oCust.Mbe_name;
                    _ReceiptHeader.Sar_debtor_add_1 = oCust.Mbe_add1;
                    _ReceiptHeader.Sar_debtor_add_2 = oCust.Mbe_add2;
                    _ReceiptHeader.Sar_tel_no = "";
                    _ReceiptHeader.Sar_mob_no = Request["cus_mobile"];
                    _ReceiptHeader.Sar_nic_no = oCust.Mbe_nic;

                    if (inv.Sah_inv_no != "CRED")
                    {
                        _ReceiptHeader.Sar_tot_settle_amt = _recieptItem.Sum(x => x.Sard_settle_amt);
                    }
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
                    _ReceiptHeader.Sar_anal_1 = oCust.Mbe_cr_distric_cd;
                    _ReceiptHeader.Sar_anal_2 = oCust.Mbe_cr_province_cd;
                    string pricebook = Request["PriceBook"].ToString();
                    string priceLevel = Request["PriceLevel"].ToString();
                    //string packagetype = Request["PackageType"].ToString();

                    oMainItemsList = (List<InvoiceItem>)Session["oMainItemsList"];
                    List<InvoiceItem> oMainItemsList2;
                    oMainItemsList2 = new List<InvoiceItem>();
                    int j = 0;
                    foreach (InvoiceItem invtb in oMainItemsList)
                    {
                        InvoiceItem om = new InvoiceItem();
                        invtb.Sad_res_line_no = j;
                        om.Sad_alt_itm_cd = invtb.Sad_alt_itm_cd;
                        om.Sad_alt_itm_desc = invtb.Sad_alt_itm_desc;
                        om.Sad_comm_amt = invtb.Sad_comm_amt;
                        om.Sad_disc_amt = invtb.Sad_disc_amt;
                        om.Sad_disc_rt = invtb.Sad_disc_rt;
                        om.Sad_do_qty = invtb.Sad_do_qty;
                        om.Sad_fws_ignore_qty = invtb.Sad_fws_ignore_qty;
                        om.Sad_inv_no = invtb.Sad_inv_no;
                        om.Sad_is_promo = invtb.Sad_is_promo;
                        om.Sad_itm_cd = invtb.Sad_itm_cd;
                        om.Sad_itm_line = j;
                        om.Sad_itm_seq = invtb.Sad_itm_seq;
                        om.Sad_itm_stus = invtb.Sad_itm_stus;
                        om.Sad_itm_tax_amt = invtb.Sad_itm_tax_amt;
                        om.Sad_itm_tp = invtb.Sad_itm_tp;
                        om.Sad_job_line = invtb.Sad_job_line;
                        om.Sad_job_no = invtb.Sad_job_no;
                        om.Sad_merge_itm = invtb.Sad_merge_itm;
                        om.Sad_outlet_dept = invtb.Sad_outlet_dept;
                        om.Sad_pbook = invtb.Sad_pbook;
                        om.Sad_pb_lvl = invtb.Sad_pb_lvl;
                        om.Sad_pb_price = invtb.Sad_pb_price;
                        om.Sad_print_stus = invtb.Sad_print_stus;
                        om.Sad_promo_cd = invtb.Sad_promo_cd;
                        om.Sad_qty = invtb.Sad_qty;
                        om.Sad_res_line_no = invtb.Sad_res_line_no;
                        om.Sad_res_no = invtb.Sad_res_no;
                        om.Sad_seq = invtb.Sad_seq;
                        om.Sad_seq_no = invtb.Sad_seq_no;
                        om.Sad_sim_itm_cd = invtb.Sad_sim_itm_cd;
                        om.Sad_srn_qty = invtb.Sad_srn_qty;
                        om.Sad_tot_amt = invtb.Sad_tot_amt;
                        om.Sad_trd_svc_chrg = invtb.Sad_trd_svc_chrg;
                        om.Sad_unit_amt = invtb.Sad_unit_amt;
                        om.Sad_unit_rt = invtb.Sad_unit_rt;
                        om.Sad_uom = invtb.Sad_uom;
                        om.Sad_warr_based = invtb.Sad_warr_based;
                        om.Sad_warr_period = invtb.Sad_warr_period;
                        om.Sad_warr_remarks = invtb.Sad_warr_remarks;
                        om.Sad_isapp = invtb.Sad_isapp;
                        om.Sad_iscovernote = invtb.Sad_iscovernote;
                        om.Sad_dis_line = invtb.Sad_dis_line;
                        om.Sad_dis_seq = invtb.Sad_dis_seq;
                        om.Sad_dis_type = invtb.Sad_dis_type;
                        om.SII_EX_RT = invtb.SII_EX_RT;
                        om.SII_CURR = invtb.SII_CURR;
                        om.Sad_conf_no = invtb.Sad_conf_no;
                        om.Sad_conf_line = invtb.Sad_conf_line;
                        om.Sad_itm_stus_desc = invtb.Sad_itm_stus_desc;
                        om.Sad_pbook = pricebook;
                        om.Sad_pb_lvl = priceLevel;
                        oMainItemsList2.Add(om);
                        j++;


                    }


                    MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
                    _invoiceAuto.Aut_cate_cd = Session["UserDefProf"].ToString();
                    _invoiceAuto.Aut_cate_tp = "PC";
                    _invoiceAuto.Aut_direction = 1;
                    _invoiceAuto.Aut_modify_dt = null;
                    _invoiceAuto.Aut_moduleid = "DEBT";
                    _invoiceAuto.Aut_number = 0;
                    _invoiceAuto.Aut_start_char = Session["UserDefProf"].ToString() + "-" + "DEBT" + "-";
                    _invoiceAuto.Aut_year = Convert.ToDateTime(inv.Sah_dt).Year;

                    MasterAutoNumber _receiptAuto = null;
                    if (_recieptItem != null)
                        if (_recieptItem.Count > 0)
                        {
                            _receiptAuto = new MasterAutoNumber();
                            _receiptAuto.Aut_cate_cd = userDefPro;
                            _receiptAuto.Aut_cate_tp = "PRO";
                            _receiptAuto.Aut_direction = 1;
                            _receiptAuto.Aut_modify_dt = null;
                            _receiptAuto.Aut_moduleid = "RECEIPT";
                            _receiptAuto.Aut_number = 0;
                            _receiptAuto.Aut_start_char = "DEBT";
                            _receiptAuto.Aut_year = Convert.ToDateTime(inv.Sah_dt).Year;
                        }

                    string _invoiceNo = String.Empty;
                    string _receiptNo;
                    string _deliveryOrder;
                    string _error;
                    string _buybackadj;

                    List<InvoiceSerial> _invoiceSerial = new List<InvoiceSerial>();
                    InventoryHeader _inventoryHeader = new InventoryHeader();
                    List<ReptPickSerials> _pickSerial = new List<ReptPickSerials>();
                    List<ReptPickSerialsSub> _pickSubSerial = new List<ReptPickSerialsSub>();

                    MasterAutoNumber _inventoryAuto = new MasterAutoNumber();
                    List<InvoiceVoucher> _voucher = new List<InvoiceVoucher>();
                    InventoryHeader _buybackheader = new InventoryHeader();
                    MasterAutoNumber _buybackauto = new MasterAutoNumber();
                    List<ReptPickSerials> _buybacklist = new List<ReptPickSerials>();
                    int result = CHNLSVC.Tours.SaveToursrInvoice(oHeader, oMainItemsList2, null, _ReceiptHeader, _recieptItem, null, null, null, _invoiceAuto, _receiptAuto, null, false, out _invoiceNo, out _receiptNo, out _deliveryOrder, oCust, false, false, out _error, null, null, null, null, out _buybackadj, false, null, null, null, null);

                    // int result = _basePage.CHNLSVC.Tours.SaveToursrInvoice(oHeader, oMainItemsList, _invoiceSerial, _ReceiptHeader, _recieptItem, _inventoryHeader, _pickSerial, _pickSubSerial, _invoiceAuto, _receiptAuto, _inventoryAuto, false, out _invoiceNo, out _receiptNo, out _deliveryOrder, oCust, false, false, out _error, _voucher, _buybackheader, _buybackauto, _buybacklist, out _buybackadj);
                    if (result == 1)
                    {
                        return Json(new { success = true, login = true, msg = "New Invoiced created. Invoice No : " + _invoiceNo, InvNo = _invoiceNo, type = "Success" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Get Error", type = "Error" }, JsonRequestBehavior.AllowGet);
                    }
                }

            }
            else
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult ClearCreditNote()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                Session["oMainItemsList"] = null;
                Session["oMainItemsList"] = null;
                Session["RecieptItemList"] = null;
                return Json(new { success = true, login = true, invHed = "", invdetails = "" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult ChangePrice(string chgcd, string reference, decimal totalcostne)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                oMainItemsList = (List<InvoiceItem>)Session["oMainItemsList"];
                foreach (var newitem in oMainItemsList)
                {
                    if (newitem.Sad_itm_cd == chgcd && newitem.Sad_warr_remarks == reference)
                    {
                        newitem.Sad_qty = totalcostne;
                        newitem.Sad_tot_amt = totalcostne * newitem.Sad_unit_rt;
                    }
                }


                return Json(new { success = true, login = true, invHed = "", invdetails = oMainItemsList }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }

        }


        public ActionResult CreditNotePrint(string invoiceno)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                invoiceno = (invoiceno != null) ? invoiceno.Trim() : null;
                if (invoiceno != null)
                {
                    ReportDocument rd = new ReportDocument();
                    DataTable dt = CHNLSVC.Tours.GET_PRINT_DATA(invoiceno, company);
                    rd.Load(Server.MapPath("/Reports/" + "rpt_creditnote_report.rpt"));
                    rd.Database.Tables["CreditData"].SetDataSource(dt);
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        this.Response.Clear();
                        this.Response.ContentType = "application/pdf";
                        Response.AppendHeader("Content-Disposition", "inline; filename=something.pdf");
                        return File(rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), "application/pdf");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
                else
                {
                    return Redirect("~/CreditNote");
                }

            }
            else
            {
                return Redirect("~/Login");
            }

        }
    }
}