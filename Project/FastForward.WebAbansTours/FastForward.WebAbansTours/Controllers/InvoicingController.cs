using CrystalDecisions.CrystalReports.Engine;
using FastForward.WebAbansTours.Models;
using FF.BusinessObjects;
using FF.BusinessObjects.Tours;
using FF.BusinessObjects.ToursNew;
using FF.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FastForward.WebAbansTours.Controllers
{
    public class InvoicingController : BaseController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                ST_MENU per = CHNLSVC.Tours.getAcccessPermission(userId, 1019);
                if (per.MNU_ID == 0)
                {
                    throw new AuthenticationException("You do not have the necessary permission to perform this action");
                }
            }
            else
            {
                Redirect("~/Login/index");
            }

        }
        // GET: Invoicing

        List<InvoiceItem> oMainItemsList;
        List<RecieptItem> _recieptItem;
        List<MST_ST_PAX_DET> paxDetList;
        private InvoiceHeader oHeader = null;
        public ActionResult Index(string enqid, string cuscd, string address, string mobile)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string date = DateTime.Today.Date.ToString();
            string[] tokens = date.Split(' ');
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                ViewBag.date = tokens[0];
                Session["oMainItemsList"] = null;
                Session["_recieptItem"] = null;
                Session["RecieptItemList"] = null;
                Session["totalPaidAmount"] = null;
                Session["paxDetList"] = null;
                Session["oHeader"] = null;
                if (enqid != null && cuscd != null)
                {
                    ViewBag.enqid = enqid;
                    ViewBag.cuscd = cuscd;
                    ViewBag.address = address;
                    ViewBag.mobile = mobile;

                    List<InvoiceHeader> InvData = CHNLSVC.Tours.GetInvoiceData(enqid);
                    var invNo = InvData.Where(b => b.Sah_stus != "C" && b.Sah_direct == true).Max(a => a.Sah_inv_no);
                    ViewBag.InvNo = invNo;
                }
                string InvId = HttpContext.Session["InvNo"] as string;
                if (InvId != "") ViewBag.InvId = InvId;
                Session["InvNo"] = null;

                return View();
            }
            else
            {
                return Redirect("~/Login/index");
            }
        }

        public JsonResult getCostSheetData(string enqid)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            string err;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {

                QUO_COST_HDR oCostHeader = null;
                List<QUO_COST_DET> oCostMainItems = null;

                Int32 result = CHNLSVC.Tours.getCostSheetDetails(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), enqid, "3", out oCostHeader, out oCostMainItems, out err);
                //ViewBag.oCostMainItems = oCostMainItems;
                return Json(new { success = true, login = true, data = oCostMainItems }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult getEnquiryCharges(string enqid)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            string err;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<ST_ENQ_CHARGES> oCostMainItems = CHNLSVC.Tours.tempEnquiryCharges(enqid);
                oCostMainItems = oCostMainItems.Where(a => a.SCH_INVOICED == 0).ToList();
                return Json(new { success = true, login = true, data = oCostMainItems }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult InvoiceCreate(InvoiceHeader inv)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
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
                oHeader.Sah_inv_tp = inv.Sah_inv_tp;
                oHeader.Sah_is_acc_upload = false;
                oHeader.Sah_manual = false;
                oHeader.Sah_mod_by = userId;
                oHeader.Sah_mod_when = DateTime.Now;
                oHeader.Sah_pc = userDefPro;
                oHeader.Sah_pdi_req = 0;
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


                //RecieptItem _item = new RecieptItem();
                //_item.Sard_cc_is_promo = false;
                // _item.Sard_cc_period = 5;
                // _item.Sard_cc_tp ="123";

                //if (Request["Sird_pay_tp"] == "CASH")
                //{
                //    _item.Sard_deposit_bank_cd = Request["Deposit_bank_cd"];
                //    _item.Sard_deposit_branch = Request["Deposit_branch_cd"];
                //    _item.Sard_pay_tp = Request["Sird_pay_tp"];
                //    _item.Sard_settle_amt = Convert.ToDecimal(Request["Sird_settle_amt"]);
                //}
                //else
                //{
                //    _item.Sard_chq_bank_cd = Request["Cheque_bnk_cd"];
                //    _item.Sard_chq_branch = Request["Cheque_branch_cd"];

                //    _item.Sard_credit_card_bank = Request["Cred_crd_bank"];
                //    _item.Sard_ref_no = Request["Cred_crd_ref_no"];
                //    _item.Sard_deposit_bank_cd = Request["Deposit_bank_cd"];
                //    _item.Sard_deposit_branch = Request["Deposit_branch_cd"];
                //    _item.Sard_pay_tp = Request["Sird_pay_tp"];
                //    _item.Sard_settle_amt = Convert.ToDecimal(Request["Sird_settle_amt"]);
                //    //  _item.Sard_anal_3 = Math.Round(150.5, 2);

                //    _item.Sard_chq_dt = Convert.ToDateTime(Request["Cheque_dt"]);
                //    _item.Sard_chq_bank_cd = Request["Cheque_bnk_cd"];
                //    _item.Sard_chq_branch = Request["Cheque_branch_cd"];
                //    _item.Sard_ref_no = Request["Cheque_ref_no"];

                //    _item.Sard_credit_card_bank = Request["Cred_crd_bank"];
                //    _item.Sard_deposit_bank_cd = Request["Deposit_bank_cd"];
                //    _item.Sard_deposit_branch = Request["Deposit_branch_cd"];
                //    _item.Sard_pay_tp = Request["Sird_pay_tp"];
                //    _item.Sard_settle_amt = Convert.ToDecimal(Request["Sird_settle_amt"]);
                //}




                List<RecieptItemTBS> _recieptItemtbs = (List<RecieptItemTBS>)Session["RecieptItemList"];
                if (_recieptItemtbs == null)
                {
                    _recieptItemtbs = new List<RecieptItemTBS>();
                }
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


                RecieptHeader _ReceiptHeader = new RecieptHeader();
                _ReceiptHeader.Sar_seq_no = -1; //CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "RECEIPT", 1, BaseCls.GlbUserComCode);
                _ReceiptHeader.Sar_com_cd = company;
                _ReceiptHeader.Sar_receipt_type = "DIR";
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
                string packagetype = Request["PackageType"].ToString();

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
                _invoiceAuto.Aut_cate_tp = "TINVO";
                _invoiceAuto.Aut_direction = 1;
                _invoiceAuto.Aut_modify_dt = null;
                _invoiceAuto.Aut_moduleid = "CS";
                _invoiceAuto.Aut_number = 0;
                _invoiceAuto.Aut_start_char = "TINVO";
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
                        _receiptAuto.Aut_start_char = "DIR";
                        _receiptAuto.Aut_year = Convert.ToDateTime(inv.Sah_dt).Year;
                    }

                string _invoiceNo = string.Empty;
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

                if (Session["paxDetList"] != null)
                {
                    paxDetList = (List<MST_ST_PAX_DET>)Session["paxDetList"];

                }
                else
                {
                    paxDetList = new List<MST_ST_PAX_DET>();
                }

                int result = CHNLSVC.Tours.SaveToursrInvoice(oHeader, oMainItemsList2, null, _ReceiptHeader, _recieptItem, null, null, null, _invoiceAuto, _receiptAuto, null, false, out _invoiceNo, out _receiptNo, out _deliveryOrder, oCust, false, false, out _error, null, null, null, null, out _buybackadj, false, null, null, null, paxDetList, true);
                Session["InvNo"] = _invoiceNo;
                // int result = _basePage.CHNLSVC.Tours.SaveToursrInvoice(oHeader, oMainItemsList, _invoiceSerial, _ReceiptHeader, _recieptItem, _inventoryHeader, _pickSerial, _pickSubSerial, _invoiceAuto, _receiptAuto, _inventoryAuto, false, out _invoiceNo, out _receiptNo, out _deliveryOrder, oCust, false, false, out _error, _voucher, _buybackheader, _buybackauto, _buybacklist, out _buybackadj);

            }
            return RedirectToAction("Index");
        }
        public JsonResult getEnqDetailsByCusCD(string val)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                val = val.Trim();

                return Json(new { success = true, login = true, data = "" }, JsonRequestBehavior.AllowGet);
                List<GEN_CUST_ENQ> Enq = CHNLSVC.Tours.GET_ENQRY_BY_CUST_PEN_INV(company, val);
                List<GEN_CUST_ENQ> Enq2 = Enq.Where(A => A.GCE_STUS == 5).ToList();

                foreach (var enqq in Enq2)
                {
                    int count = CHNLSVC.Tours.Isinvoiced(enqq.GCE_COM,enqq.GCE_ENQ_ID);
                    if (count >0)
                    {

                        Enq.RemoveAll(a => a.GCE_ENQ_ID == enqq.GCE_ENQ_ID);
                    }
                }

                Enq2 = Enq.Where(A => A.GCE_STUS == 5).ToList();
                if (Enq != null)
                {
                    return Json(new { success = true, login = true, data = Enq2 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, login = true, data = "" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult LoadInvoiceType()
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<PriceDefinitionRef> _PriceDefinitionRef = CHNLSVC.Tours.GetToursPriceDefByBookAndLevel(company, string.Empty, string.Empty, string.Empty, userDefPro);
                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    if (_PriceDefinitionRef.Count > 0)
                    {
                        var _types = _PriceDefinitionRef.Where(X => !X.Sadd_doc_tp.Contains("HS")).Select(x => x.Sadd_doc_tp).Distinct().ToList();
                        foreach (var list in _types)
                        {

                            ComboBoxObject o1 = new ComboBoxObject();
                            o1.Text = list;
                            o1.Value = list;
                            oList.Add(o1);
                        }

                    }
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
        public JsonResult LoadExecutive()
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<SEARCH_MST_EMP> emp = CHNLSVC.Tours.Get_mst_emp(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());


                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    if (emp.Count > 0)
                    {
                        foreach (var list in emp)
                        {

                            ComboBoxObject o1 = new ComboBoxObject();
                            o1.Text = list.ESEP_FIRST_NAME;
                            o1.Value = list.ESEP_CD;
                            oList.Add(o1);
                        }


                    }
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

        public JsonResult LoadPriceBook(string invType)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {

                    List<PriceDefinitionRef> pricebook = CHNLSVC.Tours.GetToursPriceDefByBookAndLevel(company, string.Empty, string.Empty, string.Empty, userDefPro);
                    var _books = pricebook.Select(x => x.Sadd_pb).Distinct().ToList();

                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    if (_books.Count > 0)
                    {
                        foreach (var list in _books)
                        {

                            ComboBoxObject o1 = new ComboBoxObject();
                            o1.Text = list;
                            o1.Value = list;
                            oList.Add(o1);

                        }


                    }
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
        public JsonResult LoadPriceBookNew(string invType)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {

                    List<PriceDefinitionRef> pricebook = CHNLSVC.Tours.GetToursPriceDefByBookAndLevel(company, string.Empty, string.Empty, string.Empty, userDefPro);
                    var _books = pricebook.Where(x => x.Sadd_doc_tp == invType).Select(x => x.Sadd_pb).Distinct().ToList();

                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    if (_books.Count > 0)
                    {
                        foreach (var list in _books)
                        {

                            ComboBoxObject o1 = new ComboBoxObject();
                            o1.Text = list;
                            o1.Value = list;
                            oList.Add(o1);

                        }


                    }
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
        public JsonResult LoadPriceLevel(string invType, string book)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {

                    List<PriceDefinitionRef> pricebook = CHNLSVC.Tours.GetToursPriceDefByBookAndLevel(company, string.Empty, string.Empty, string.Empty, userDefPro);
                    var _levels = pricebook.Select(y => y.Sadd_p_lvl).Distinct().ToList();

                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    if (_levels.Count > 0)
                    {
                        foreach (var list in _levels)
                        {

                            ComboBoxObject o1 = new ComboBoxObject();
                            o1.Text = list;
                            o1.Value = list;
                            oList.Add(o1);
                        }


                    }
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

        public JsonResult LoadPriceLevelNew(string invType, string book)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {

                    List<PriceDefinitionRef> pricebook = CHNLSVC.Tours.GetToursPriceDefByBookAndLevel(company, string.Empty, string.Empty, string.Empty, userDefPro);
                    var _levels = pricebook.Where(x => x.Sadd_doc_tp == invType && x.Sadd_pb == book).Select(y => y.Sadd_p_lvl).Distinct().ToList();

                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    if (_levels.Count > 0)
                    {
                        foreach (var list in _levels)
                        {

                            ComboBoxObject o1 = new ComboBoxObject();
                            o1.Text = list;
                            o1.Value = list;
                            oList.Add(o1);
                        }


                    }
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

        public JsonResult LoadPackageType()
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<ComboBoxObject> oItems = CHNLSVC.Tours.GET_TOUR_PACKAGE_TYPES();


                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    if (oItems.Count > 0)
                    {
                        foreach (var list in oItems)
                        {

                            ComboBoxObject o1 = new ComboBoxObject();
                            o1.Text = list.Text;
                            o1.Value = list.Value;
                            oList.Add(o1);
                        }


                    }
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
        public JsonResult CostType()
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MST_COST_CATE> oCate = CHNLSVC.Tours.GET_COST_CATE(company, userDefPro);
                    List<MST_COST_CATE> oCat2 = oCate.OrderBy(a => a.MCC_DESC).ToList();

                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    if (oCat2.Count > 0)
                    {
                        foreach (var list in oCat2)
                        {

                            ComboBoxObject o1 = new ComboBoxObject();
                            o1.Text = list.MCC_DESC;
                            o1.Value = list.MCC_CD;
                            oList.Add(o1);
                        }


                    }
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
        public JsonResult Currency()
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MasterCurrency> _cur = CHNLSVC.General.GetAllCurrency(null);


                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    if (_cur.Count > 0)
                    {
                        foreach (var list in _cur)
                        {

                            ComboBoxObject o1 = new ComboBoxObject();
                            o1.Text = list.Mcr_cd;
                            o1.Value = list.Mcr_cd;
                            oList.Add(o1);
                        }


                    }
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
        public JsonResult currencyCodeChange(string currency)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MasterCurrency> _cur = CHNLSVC.General.GetAllCurrency(null);
                    MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(company, userDefPro);
                    decimal _exchangRate = 0;
                    MasterExchangeRate _exc1 = CHNLSVC.Sales.GetExchangeRate(company, currency, DateTime.Now, _pc.Mpc_def_exrate, userDefPro);
                    _exchangRate = (_exc1 != null) ? _exc1.Mer_bnkbuy_rt : 0;
                    string oList = String.Format("{0:#,##0.00;($#,##0.00);0.00}", _exchangRate);
                    if (_exc1 != null)
                    {
                        _exchangRate = _exc1.Mer_bnkbuy_rt;
                        oList = String.Format("{0:#,##0.00;($#,##0.00);0.00}", _exchangRate);

                    }
                    else if (currency == "LKR")
                    {
                        _exchangRate = 1;
                        oList = String.Format("{0:#,##0.00;($#,##0.00);0.00}", _exchangRate);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "please update exchange rates for selected currency", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
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

        //public JsonResult updateTourCharges(string chgCd, string service, string currencyCode, int pax, string remarks, float dis, float discount, float unitrate, float Rate, float Markup)
        //{
        //    string userId = HttpContext.Session["UserID"] as string;
        //    string company = HttpContext.Session["UserCompanyCode"] as string;
        //    string userDefPro = HttpContext.Session["UserDefProf"] as string;
        //    string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
        //    if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
        //    {
        //        string chargCdDesc = "";


        //        if (Session["oMainItemsList"] != null)
        //        {
        //            oMainItemsList = (List<InvoiceItem>)Session["oMainItemsList"];
        //        }
        //        else
        //        {
        //            oMainItemsList = new List<InvoiceItem>();

        //        }
        //        InvoiceItem oItem = new InvoiceItem();
        //        oItem.Sad_itm_cd = chgCd;
        //        oItem.Sad_alt_itm_desc = chargCdDesc;
        //        oItem.Sad_itm_stus = "GOD";
        //        oItem.Sad_qty = Convert.ToDecimal(pax);

        //        oItem.Sad_tot_amt =Convert.ToDecimal( Markup);
        //        oItem.Sad_unit_rt = Convert.ToDecimal(Markup) / Convert.ToDecimal(pax);
        //        oItem.Sad_unit_amt = oItem.Sad_unit_rt * Convert.ToDecimal(pax);
        //        oItem.Sad_print_stus = true;
        //        oItem.SII_CURR = currencyCode;
        //        oItem.SII_EX_RT = Convert.ToDecimal(Rate);
        //        oItem.Sad_warr_remarks = remarks;
        //        oMainItemsList.Add(oItem);
        //        Session["oMainItemsList"] = oMainItemsList;

        //        return Json(new { success = true, login = true, oMainItemsList = oMainItemsList }, JsonRequestBehavior.AllowGet);

        //    }
        //    else
        //    {
        //        return Json(new { success = false, login = true, data = Resource.ServerError }, JsonRequestBehavior.AllowGet);
        //    }
        //}
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
                    oItem.Sad_disc_amt = Convert.ToDecimal(discount);

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
                        oItem.Sad_unit_rt = oItem.Sad_tot_amt / oItem.Sad_qty;
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


        public JsonResult getInvoiceDetails(string invNo)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            RecieptHeader oRecieptHeader = null;
            string err;
            Session["oMainItemsList"] = null;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                // int asd = CHNLSVC.Tours.GetInvoiceDetails(userId, userDefPro, invNo, out oHeader, out oMainItemsList, out oRecieptHeader, out _recieptItem, out   err);

                List<InvoiceItem> invdata = CHNLSVC.Sales.GetInvoiceHeaderDetailsList(invNo).ToList();
                return Json(new { success = true, login = true, data = invdata }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetInvData(string cuscode, string enqid)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<QUO_COST_DET> invdata = CHNLSVC.Sales.GetInvoiceDetailsForEnq(cuscode, enqid);
                return Json(new { success = true, login = true, data = invdata }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ChangeEnqStatus(string cuscode, string enqid)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                int invdata = CHNLSVC.Tours.UPDATE_ENQ_STATUS(cuscode, enqid);
                return Json(new { success = true, login = true, data = invdata }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult InvoicingReport(string invNo = null)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                invNo = (invNo != null) ? invNo.Trim() : null;
                //invNo = "TINVO00024";
                if (invNo != null)
                {
                    string content;
                    PrintModel model = new PrintModel();
                    content = Url.Content("~/Print_Module/Print_Viwer/InvoicePrintViwer.aspx");
                    model.ReportPath = content;
                    model.invoiceNo = invNo;
                    Session["invoiceNum"] = invNo;
                    return View("InvoicingReport", model);
                }
                else
                {
                    return Redirect("~/Invoicing");
                }

            }
            else
            {
                return Redirect("~/Login");
            }

        }
        public ActionResult InvoicingReport2(string invNo = null)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                invNo = (invNo != null) ? invNo.Trim() : null;
                if (invNo != null)
                {
                    ReportDocument rd = new ReportDocument();
                    DataTable INVDATA = CHNLSVC.Tours.GETINVCHARGES(invNo);
                    int i = 0;
                    foreach(var date in INVDATA.Rows)
                    {
                        if (INVDATA.Rows.Count>i+1)
                        {

                            if (INVDATA.Rows[i]["gce_tr_code"].ToString() == INVDATA.Rows[i + 1]["gce_tr_code"].ToString() && INVDATA.Rows[i]["tld_chr_cd"].ToString() == INVDATA.Rows[i + 1]["tld_chr_cd"].ToString())
                            {
                                INVDATA.Rows[i + 1]["tld_chr_cd"] = "";
                                INVDATA.Rows[i + 1]["tld_tot"] = 0;
                            }
                        }
                        i++;
                    }






                    rd.Load(Server.MapPath("/Reports/" + "rpt_invoice_charges.rpt"));
                    rd.Database.Tables["InvoiceData"].SetDataSource(INVDATA);
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
                    return Redirect("~/Invoicing");
                }

            }
            else
            {
                return Redirect("~/Login");
            }

        }
        public JsonResult GetInvItmDeatails(string invno, string enqno)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<InvoiceHeader> InvData3 = CHNLSVC.Tours.GetInvoiceData(enqno);
                var exe = InvData3.Max(a => a.Sah_sales_ex_cd);
                var pack = InvData3.Max(a => a.Sah_anal_3);
                List<InvoiceHeader> InvData2 = CHNLSVC.Tours.GetInvoiceData(enqno);
                var type = InvData2.Max(a => a.Sah_inv_tp);
                var InvData = CHNLSVC.Tours.GetInvoiceDetailforInvNo(invno).ToList();
                return Json(new { success = true, login = true, data = InvData, type = type, exe = exe, pack = pack }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult GetInvHDRDeatails(string invNo)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                var InvData = CHNLSVC.Tours.GetInvoiceHDRData(invNo).ToList();
                List<MST_ST_PAX_DET> paxDet = CHNLSVC.Tours.GetInvoicePaxDet(invNo);
                Session["paxDetList"] = paxDet;
                return Json(new { success = true, login = true, data = InvData }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult InvoiceCreateNew(InvoiceHeader inv)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
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
                oHeader.Sah_inv_tp = inv.Sah_inv_tp;
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


                //RecieptItem _item = new RecieptItem();
                //_item.Sard_cc_is_promo = false;
                // _item.Sard_cc_period = 5;
                // _item.Sard_cc_tp ="123";

                //if (Request["Sird_pay_tp"] == "CASH")
                //{
                //    _item.Sard_deposit_bank_cd = Request["Deposit_bank_cd"];
                //    _item.Sard_deposit_branch = Request["Deposit_branch_cd"];
                //    _item.Sard_pay_tp = Request["Sird_pay_tp"];
                //    _item.Sard_settle_amt = Convert.ToDecimal(Request["Sird_settle_amt"]);
                //}
                //else
                //{
                //    _item.Sard_chq_bank_cd = Request["Cheque_bnk_cd"];
                //    _item.Sard_chq_branch = Request["Cheque_branch_cd"];

                //    _item.Sard_credit_card_bank = Request["Cred_crd_bank"];
                //    _item.Sard_ref_no = Request["Cred_crd_ref_no"];
                //    _item.Sard_deposit_bank_cd = Request["Deposit_bank_cd"];
                //    _item.Sard_deposit_branch = Request["Deposit_branch_cd"];
                //    _item.Sard_pay_tp = Request["Sird_pay_tp"];
                //    _item.Sard_settle_amt = Convert.ToDecimal(Request["Sird_settle_amt"]);
                //    //  _item.Sard_anal_3 = Math.Round(150.5, 2);

                //    _item.Sard_chq_dt = Convert.ToDateTime(Request["Cheque_dt"]);
                //    _item.Sard_chq_bank_cd = Request["Cheque_bnk_cd"];
                //    _item.Sard_chq_branch = Request["Cheque_branch_cd"];
                //    _item.Sard_ref_no = Request["Cheque_ref_no"];

                //    _item.Sard_credit_card_bank = Request["Cred_crd_bank"];
                //    _item.Sard_deposit_bank_cd = Request["Deposit_bank_cd"];
                //    _item.Sard_deposit_branch = Request["Deposit_branch_cd"];
                //    _item.Sard_pay_tp = Request["Sird_pay_tp"];
                //    _item.Sard_settle_amt = Convert.ToDecimal(Request["Sird_settle_amt"]);
                //}




                List<RecieptItemTBS> _recieptItemtbs = (List<RecieptItemTBS>)Session["RecieptItemList"];
                if (_recieptItemtbs == null)
                {
                    _recieptItemtbs = new List<RecieptItemTBS>();
                }
                if (inv.Sah_inv_tp != "CRED" && inv.Sah_inv_tp != "DEBT" && _recieptItemtbs.Count == 0)
                {
                    return Json(new { success = false, login = true, msg = "Please Add Payment", type = "Error" }, JsonRequestBehavior.AllowGet);
                }
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

                if (inv.Sah_inv_no != "CRED" && inv.Sah_inv_no != "DEBT")
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
                string packagetype = Request["PackageType"].ToString();

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
                _invoiceAuto.Aut_moduleid = inv.Sah_inv_tp;
                _invoiceAuto.Aut_number = 0;
                _invoiceAuto.Aut_start_char = Session["UserDefProf"].ToString() + "-" + inv.Sah_inv_tp + "-";
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
                        _receiptAuto.Aut_start_char = "DIR";
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
                if (oHeader.Sah_ref_doc != "")
                {
                    if (Session["paxDetList"] != null)
                    {
                        paxDetList = (List<MST_ST_PAX_DET>)Session["paxDetList"];

                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Please add invoice customer for save.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    decimal totalInv = oMainItemsList.Sum(m => m.Sad_tot_amt / m.SII_EX_RT);

                    decimal totleCus = paxDetList.Sum(n => n.SPD_AMT);

                    if (totleCus != Math.Round(totalInv, 2))
                    {
                        return Json(new { success = false, login = true, msg = "Customer amounts not equals to invoice amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                }
               
                int result = CHNLSVC.Tours.SaveToursrInvoice(oHeader, oMainItemsList2, null, _ReceiptHeader, _recieptItem, null, null, null, _invoiceAuto, _receiptAuto, null, false, out _invoiceNo, out _receiptNo, out _deliveryOrder, oCust, false, false, out _error, null, null, null, null, out _buybackadj, false, null, null, null, paxDetList);

                // int result = _basePage.CHNLSVC.Tours.SaveToursrInvoice(oHeader, oMainItemsList, _invoiceSerial, _ReceiptHeader, _recieptItem, _inventoryHeader, _pickSerial, _pickSubSerial, _invoiceAuto, _receiptAuto, _inventoryAuto, false, out _invoiceNo, out _receiptNo, out _deliveryOrder, oCust, false, false, out _error, _voucher, _buybackheader, _buybackauto, _buybacklist, out _buybackadj);
                if (result == 1)
                {
                    return Json(new { success = true, login = true, msg = "New Invoiced created. Invoice No : " + _invoiceNo, InvNo = _invoiceNo, type = "Success" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = true, msg = _error, type = "Error" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }


        }
        public JsonResult clearValues()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    Session["oMainItemsList"] = null;
                    Session["_recieptItem"] = null;
                    Session["RecieptItemList"] = null;
                    Session["totalPaidAmount"] = null;
                    Session["paxDetList"] = null;
                    Session["oHeader"] = null;
                    return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
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
        public JsonResult loadChargCode(string code, string service)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (code != "")
                    {
                        if (service != "")
                        {
                            if (service == "TRANS")
                            {
                                SR_TRANS_CHA oSR_Trans_CHARGE = CHNLSVC.Tours.GetTransferChargeDetailsByCode(company, service, code, userDefPro);
                                if (oSR_Trans_CHARGE != null && oSR_Trans_CHARGE.STC_CD != null)
                                {
                                    return Json(new { success = true, login = true, data = oSR_Trans_CHARGE }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json(new { success = false, login = true, msg = "Please enter valid Sub Type.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else if (service == "AIRTVL")
                            {
                                SR_AIR_CHARGE oSR_Airs_CHARGE = CHNLSVC.Tours.GetChargeDetailsByCode(company, service, code, userDefPro);
                                if (oSR_Airs_CHARGE != null && oSR_Airs_CHARGE.SAC_CD != null)
                                {
                                    return Json(new { success = true, login = true, data = oSR_Airs_CHARGE }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json(new { success = false, login = true, msg = "Please enter valid Sub Type.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else if (service == "MSCELNS")
                            {
                                SR_SER_MISS oSR_Miss_CHARGE = CHNLSVC.Tours.GetMiscellaneousChargeDetailsByCode(company, service, code, userDefPro);
                                if (oSR_Miss_CHARGE != null && oSR_Miss_CHARGE.SSM_CD != null)
                                {
                                    return Json(new { success = true, login = true, data = oSR_Miss_CHARGE }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json(new { success = false, login = true, msg = "Please enter valid Sub Type.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                SR_SER_MISS oSR_Miss_CHARGE = CHNLSVC.Tours.GetMiscellaneousChargeDetailsByCode(company, service, code, userDefPro);
                                if (oSR_Miss_CHARGE != null && oSR_Miss_CHARGE.SSM_CD != null)
                                {
                                    return Json(new { success = true, login = true, data = oSR_Miss_CHARGE }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json(new { success = false, login = true, msg = "Please enter valid charge code.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                            }

                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "Please select service.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = Resource.ServerError, type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult addInvoiceCustomers(string cusCd, string cusName, string remk, string mob, string pp, string nic, string amount)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    cusCd = cusCd.Trim();
                    cusName = cusName.Trim();
                    remk = remk.Trim();
                    mob = mob.Trim();
                    nic = nic.Trim();
                    pp = pp.Trim();
                    if (cusCd != "" && cusName != "")
                    {
                        if (Session["paxDetList"] != null)
                        {
                            paxDetList = (List<MST_ST_PAX_DET>)Session["paxDetList"];
                        }
                        else
                        {
                            paxDetList = new List<MST_ST_PAX_DET>();
                        }
                        MST_ST_PAX_DET det = new MST_ST_PAX_DET();
                        det.SPD_CUS_CD = cusCd;
                        det.SPD_CUS_NAME = cusName;
                        det.SPD_RMK = remk;
                        det.SPD_MOB = mob;
                        det.SPD_NIC = nic;
                        det.SPD_PP_NO = pp;
                        det.SPD_STUS = 1;
                        det.SPD_AMT = Convert.ToDecimal(amount);
                        paxDetList.Add(det);
                        Session["paxDetList"] = paxDetList;
                        decimal balance = 0;
                        decimal paxToTal = paxDetList.Sum(m => m.SPD_AMT);

                        if (Session["oMainItemsList"] != null)
                        {
                            oMainItemsList = (List<InvoiceItem>)Session["oMainItemsList"];
                        }
                        else
                        {
                            oMainItemsList = new List<InvoiceItem>();

                        }
                        decimal invTot = oMainItemsList.Sum(n => n.Sad_tot_amt / n.SII_EX_RT);
                        balance = Math.Round((invTot - paxToTal), 2);

                        return Json(new { success = true, login = true, paxDetList = paxDetList, balance = balance }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Invalid customer details.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = Resource.ServerError, type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult removeInvoiceCustomers(string cusCd)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    cusCd = cusCd.Trim();
                    if (cusCd != "")
                    {

                        if (Session["paxDetList"] != null)
                        {
                            paxDetList = (List<MST_ST_PAX_DET>)Session["paxDetList"];

                        }
                        else
                        {
                            paxDetList = new List<MST_ST_PAX_DET>();
                        }
                        if (paxDetList.FindAll(x => x.SPD_CUS_CD == cusCd).Count > 0)
                        {
                            try
                            {
                                var itemToRemove = paxDetList.First(x => x.SPD_CUS_CD == cusCd);
                                paxDetList.Remove(itemToRemove);
                            }
                            catch (Exception e)
                            {
                                return Json(new { success = false, login = true, msg = e.ToString(), type = "Info" }, JsonRequestBehavior.AllowGet);
                            }

                            Session["paxDetList"] = paxDetList;
                            decimal balance = 0;
                            decimal paxToTal = paxDetList.Sum(m => m.SPD_AMT);

                            if (Session["oMainItemsList"] != null)
                            {
                                oMainItemsList = (List<InvoiceItem>)Session["oMainItemsList"];
                            }
                            else
                            {
                                oMainItemsList = new List<InvoiceItem>();

                            }
                            decimal invTot = oMainItemsList.Sum(n => n.Sad_tot_amt);
                            balance = invTot - paxToTal;

                            return Json(new { success = true, login = true, paxDetList = paxDetList, balance = balance }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "Invalid customer code.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Invalid customer code.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = Resource.ServerError, type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult getInvoiceCustomers()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (Session["paxDetList"] != null)
                    {
                        paxDetList = (List<MST_ST_PAX_DET>)Session["paxDetList"];

                    }
                    else
                    {
                        paxDetList = new List<MST_ST_PAX_DET>();
                    }
                    decimal balance = 0;
                    decimal paxToTal = paxDetList.Sum(m => m.SPD_AMT);

                    if (Session["oMainItemsList"] != null)
                    {
                        oMainItemsList = (List<InvoiceItem>)Session["oMainItemsList"];
                    }
                    else
                    {
                        oMainItemsList = new List<InvoiceItem>();

                    }
                    decimal invTot = oMainItemsList.Sum(n => n.Sad_tot_amt / n.SII_EX_RT);
                    balance = Math.Round((invTot - paxToTal), 2);
                    return Json(new { success = true, login = true, paxDetList = paxDetList, balance = balance }, JsonRequestBehavior.AllowGet);
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
        public JsonResult getCustomerAmount(string enqId)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (Session["oMainItemsList"] != null)
                    {
                        oMainItemsList = (List<InvoiceItem>)Session["oMainItemsList"];
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Invalid invoice details.", type = "Info" }, JsonRequestBehavior.AllowGet);

                    }
                    decimal totle = oMainItemsList.Sum(m => m.Sad_tot_amt / m.SII_EX_RT);
                    GEN_CUST_ENQ enq = CHNLSVC.Tours.GET_CUST_ENQRY(company, userDefPro, enqId);
                    decimal paxAmount = Math.Round((Convert.ToDecimal(totle) / enq.GCE_NO_PASS), 2);
                    return Json(new { success = true, login = true, paxAmount = paxAmount }, JsonRequestBehavior.AllowGet);
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

        public JsonResult checkCustomerAmounts()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (Session["oMainItemsList"] != null)
                    {
                        oMainItemsList = (List<InvoiceItem>)Session["oMainItemsList"];
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Invalid invoice details.", type = "Info" }, JsonRequestBehavior.AllowGet);

                    }
                    decimal totle = oMainItemsList.Sum(m => m.Sad_tot_amt / m.SII_EX_RT);
                    if (Session["paxDetList"] != null)
                    {
                        paxDetList = (List<MST_ST_PAX_DET>)Session["paxDetList"];

                    }
                    else
                    {
                        paxDetList = new List<MST_ST_PAX_DET>();
                    }
                    decimal totleCus = paxDetList.Sum(n => n.SPD_AMT);
                    if (totleCus == 0)
                    {
                        return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                    }
                    if (totleCus == Math.Round(totle, 2))
                    {
                        return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Customer amounts not equals to invoice amount.Do you want to continue?" }, JsonRequestBehavior.AllowGet);

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
                    return Json(new { success = false, login = true, msg = Resource.ServerError, type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult clearCustomers()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    Session["paxDetList"] = null;
                    return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
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
        public JsonResult cancelInvoice(string invNo)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    invNo = invNo.Trim();
                    if (invNo != "")
                    {
                        if (!CHNLSVC.Security.Is_OptionPerimitted(company, userId, 16049))
                        {
                            return Json(new { success = false, login = true, msg = "Sorry, You have no permission to cancel this invoice.( Advice: Required permission code : 16049) !", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        InvoiceHeader invHed = CHNLSVC.Tours.getInvoiceHederData(invNo, company, userDefPro);
                        if (invHed.Sah_inv_no != null)
                        {
                            if (invHed.Sah_stus != "C")
                            {
                                string _error = "";
                                if (invHed.Sah_dt >= DateTime.Now.AddDays(-5))
                                {
                                    InvoiceHeader _invoiceHeder = new InvoiceHeader();
                                    _invoiceHeder.Sah_inv_no = invNo;
                                    _invoiceHeder.Sah_com = company;
                                    _invoiceHeder.Sah_ref_doc = invHed.Sah_ref_doc;
                                    _invoiceHeder.Sah_stus = "C";
                                    _invoiceHeder.Sah_pc = userDefPro;
                                    _invoiceHeder.Sah_mod_by = userId;
                                    _invoiceHeder.Sah_mod_when = DateTime.Now;
                                    _invoiceHeder.Sah_seq_no = invHed.Sah_seq_no;
                                    _invoiceHeder.Sah_is_dayend = 0;

                                    List<RecieptItem> RecieptItemList = new List<RecieptItem>();
                                    RecieptItemList = CHNLSVC.Tours.getReceiptItemList(invNo);

                                    RecieptHeader _recieptHeader = new RecieptHeader();
                                    if (RecieptItemList.Count > 0)
                                    {
                                        _recieptHeader.Sar_seq_no = RecieptItemList[0].Sard_seq_no;
                                        _recieptHeader.Sar_receipt_no = RecieptItemList[0].Sard_receipt_no;
                                        _recieptHeader.Sar_act = false;
                                        _recieptHeader.Sar_mod_by = userId;
                                        _recieptHeader.Sar_is_dayend = 0;
                                        _recieptHeader.Sar_com_cd = company;
                                    }
                                    else
                                    {
                                        _recieptHeader = null;
                                    }
                                    //return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                                    Int32 result = CHNLSVC.Tours.cancelInvoice(_invoiceHeder, _recieptHeader, (Int32)EnquiryStages.Cancelled, (Int32)ToursStatus.POGenarated, out _error);
                                    if (result > 0)
                                    {
                                        return Json(new { success = true, login = true, msg = "Invoce " + invNo + " cancel success.", type = "Success" }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        return Json(new { success = false, login = true, msg = _error, type = "Info" }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                                else
                                {
                                    return Json(new { success = false, login = true, msg = "Cannot cancel more than 5 days older invoice.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = "Already cancled invoice.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "Invalid invoice number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Invalid invoice number.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = Resource.ServerError, type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult reverseInvoice(string invNo)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    invNo = invNo.Trim();
                    string _invoiceNo = string.Empty;
                    if (invNo != "")
                    {
                        List<InvoiceHeader> invData = CHNLSVC.Tours.GetInvoiceHDRData(invNo);
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
                                    oHeader.Sah_direct = true;
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
                                    oHeader.Sah_anal_4 = invNo;
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
                                    oHeader.Sah_stus = "A";
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
                                    _invoiceAuto.Aut_year = DateTime.Now.Year;




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

                                    if (Session["paxDetList"] != null)
                                    {
                                        paxDetList = (List<MST_ST_PAX_DET>)Session["paxDetList"];

                                    }
                                    else
                                    {
                                        paxDetList = new List<MST_ST_PAX_DET>();
                                    }

                                    int result = CHNLSVC.Tours.SaveToursrInvoiceReverce(oHeader, oMainItemsList2, _invoiceAuto, out _invoiceNo, oCust, out _error, paxDetList, invoice);
                                    Session["InvNo"] = _invoiceNo;
                                }
                                else
                                {
                                    if (invoice.Sah_stus == "C")
                                    {
                                        return Json(new { success = false, login = true, msg = "Cannot reverse canceled invoice.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
                            return Json(new { success = false, login = true, msg = "Please enter valid invoice number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Invalid invoice number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    return Json(new { success = true, login = true, msg = "Successfully revered the invoice " + _invoiceNo, type = "Success" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult getEnquiryData(string enqId)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (!string.IsNullOrEmpty(enqId))
                    {
                        enqId = enqId.Trim();

                        GEN_CUST_ENQ oItem = CHNLSVC.Tours.getEnquiryDetails(company, userDefPro, enqId);
                        List<RecieptItemTBS> recItems = new List<RecieptItemTBS>();
                        if (oItem != null)
                        {
                            if (oItem.CHARGER_VALUE != null)
                            {
                                if (oItem.CHARGER_VALUE.Count > 0)
                                {
                                    recItems = CHNLSVC.Tours.getReceiptItemByinvNo(oItem.CHARGER_VALUE[0].Sad_inv_no, company, userDefPro);
                                }
                            }

                            Session["oMainInvoiceItems"] = oItem.CHARGER_VALUE;
                            return Json(new { success = true, login = true, data = oItem, recItems = recItems }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "Invalid enquiry id.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = Resource.ServerError, type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        //public JsonResult GetImageDetails(string enqid)
        //{
        //    string userId = HttpContext.Session["UserID"] as string;
        //    string company = HttpContext.Session["UserCompanyCode"] as string;
        //    string userDefPro = HttpContext.Session["UserDefProf"] as string;
        //    string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

        //    if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
        //    {

        //        List<TBS_IMG_UPLOAD> oCostMainItems = CHNLSVC.CustService.GetImageDetails(enqid);
        //        List<TBS_IMG_UPLOAD> oCostMainItemsnew = new List<TBS_IMG_UPLOAD>();
        //        foreach (var oCostMainItems1 in oCostMainItems)
        //        {

        //            string url = oCostMainItems1.Jbimg_img_path.ToString() + oCostMainItems1.Jbimg_img.ToString();
        //            if (checkImage(url) == true)
        //            {
        //                oCostMainItemsnew.Add(oCostMainItems1);
        //            }



        //        }
        //        return Json(new { success = true, login = true, data = oCostMainItemsnew }, JsonRequestBehavior.AllowGet);

        //    }
        //    else
        //    {
        //        return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
        //    }

        //}
        //private bool checkImage(string url)
        //{
        //    WebRequest request = WebRequest.Create(Server.MapPath("~/") + url);

        //    try
        //    {
        //        WebResponse response = request.GetResponse();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
    }
}


