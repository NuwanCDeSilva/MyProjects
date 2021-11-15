using FF.BusinessObjects;
using FF.BusinessObjects.ToursNew;
using FF.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Authentication;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FastForward.WebAbansTours.Controllers
{
    public class OtherPartyPaymentConfController : BaseController
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
                ST_MENU per = CHNLSVC.Tours.getAcccessPermission(userId, 1030);
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
        // GET: OtherPartyPaymentConf
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                return View();
            }
            else
            {
                return Redirect("~/Login/index");
            }
        }
        public JsonResult cusCodeTextChanged(string cusCd)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (!string.IsNullOrEmpty(cusCd))
                    {
                        MasterBusinessEntity custProf = GetbyCustCD(cusCd.Trim());
                        if (custProf.Mbe_cd != null && custProf.Mbe_act == true)
                        {
                            return Json(new { success = true, local = true, login = true, data = custProf }, JsonRequestBehavior.AllowGet);
                        }
                        else if (custProf.Mbe_cd != null && custProf.Mbe_act == false)
                        {
                            return Json(new { success = false, login = true, msg = Resource.txtInacCus, data = "", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            GroupBussinessEntity _grupProf = GetbyCustCDGrup(cusCd.Trim());
                            if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                            {
                                return Json(new { success = true, group = true, login = true, data = _grupProf }, JsonRequestBehavior.AllowGet);
                            }
                            else if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == false)
                            {
                                return Json(new { success = false, login = true, msg = Resource.txtInacCus, data = "" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = Resource.ServerError, type = "Error" }, JsonRequestBehavior.AllowGet);
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

        public JsonResult loadOtherPartyReceipts(string dateFrom, string dateTo, string OthCus, string Cus)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            { 
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (dateFrom != "" && dateTo != "")
                    {
                        if (validDate(dateFrom) && validDate(dateTo))
                        {
                            DateTime fmDt = Convert.ToDateTime(dateFrom);
                            DateTime toDt = Convert.ToDateTime(dateTo);
                            if (fmDt <= DateTime.Now.Date && toDt <= DateTime.Now.Date && fmDt < toDt) {
                                dateFrom = fmDt.ToString("yyyy/MM/dd");
                                dateTo = toDt.ToString("yyyy/MM/dd");
                                OthCus = OthCus.Trim();
                                Cus = Cus.Trim();
                                List<RecieptHeaderTBS> recItems = CHNLSVC.Tours.getOtherPartyReceipts(dateFrom,dateTo, OthCus, Cus, company, userDefPro);
                                Session["OthReceiptItems"] = recItems; 
                                return Json(new { success = true, login = true, data=recItems}, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = "Please enter valid date range.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else {
                            return Json(new { success = false, login = true, msg = "Please enter valid date range.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else {
                        return Json(new { success = false, login = true, msg = "Please enter valid date range.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult UpdateLinePrice(string check,string receiptNo,string payAmount,string othPartyCd) { 
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    receiptNo = receiptNo.Trim();
                    othPartyCd=othPartyCd.Trim();
                    List<RecieptItemTBS> RecieptItemList = new List<RecieptItemTBS>();
                    List<RecieptItemTBS> sesRecieptItemList = Session["RecieptItemList"] as List<RecieptItemTBS>;
                    if (sesRecieptItemList == null)
                    {
                        sesRecieptItemList = new List<RecieptItemTBS>();
                    }
                    else
                    {
                        RecieptItemList = sesRecieptItemList;
                    }
                    if (sesRecieptItemList.Count > 0) {
                        return Json(new { success = false, login = true, msg = "Cannot add receipt values after add payment.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }

                    if (receiptNo != "")
                    {
                        List<RecieptHeaderTBS> recItems=new List<RecieptHeaderTBS>();

                        if (Session["OthReceiptItems"] != null)
                        {
                            recItems = (List<RecieptHeaderTBS>)Session["OthReceiptItems"];
                        }
                        if (recItems.Count == 0) {
                            return Json(new { success = false, login = true, msg = "Invalid item selected.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        if (payAmount == "")
                        {
                            if (check != "")
                            {
                                if (check == "True")
                                {
                                    var toUpdate = recItems.Single(x => x.Sir_receipt_no == receiptNo);
                                    toUpdate.selected = 1;


                                    var toUpdateAmt = recItems.Single(x => x.Sir_receipt_no == receiptNo);
                                    toUpdate.payAmount = 0;
                                }
                                else
                                {
                                    var toUpdate = recItems.Single(x => x.Sir_receipt_no == receiptNo);
                                    toUpdate.selected = 0;


                                    var toUpdateAmt = recItems.Single(x => x.Sir_receipt_no == receiptNo);
                                    toUpdate.payAmount = 0;
                                }

                                var numSpecialBooks = recItems.Count(n => n.selected == 1);
                                if (numSpecialBooks > 0)
                                {
                                    var recItmCnt = recItems.Count(m => m.selected == 1 && m.Sir_oth_partycd == othPartyCd);
                                    if (recItmCnt != numSpecialBooks)
                                    {
                                        var toUpdate = recItems.Single(x => x.Sir_receipt_no == receiptNo);
                                        toUpdate.selected = 0;
                                        Session["OthReceiptItems"] = recItems;
                                        return Json(new { success = false, login = true, msg = "Unable to select different rental agents.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                                Session["OthReceiptItems"] = recItems;
                                
                            }
                            else {
                                return Json(new { success = false, login = true, msg = "Please check line first.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else {
                            if (check != "")
                            {
                                if (check == "True")
                                {
                                    if (validDecemal(payAmount))
                                    {
                                        var recCount = recItems.Count(l => l.selected == 1 && l.Sir_oth_partystltamt > 0 && l.Sir_receipt_no == receiptNo);
                                        if (recCount > 0)
                                        {
                                            var amount = recItems.Where(l => l.selected == 1 && l.Sir_oth_partystltamt > 0 && l.Sir_receipt_no == receiptNo).First();
                                            decimal balanceAmount = Convert.ToDecimal(amount.Sir_tot_settle_amt - amount.Sir_oth_paidamt);
                                            if (balanceAmount < Convert.ToDecimal(payAmount))
                                            {
                                                var toUpdate = recItems.Single(x => x.Sir_receipt_no == receiptNo);
                                                toUpdate.payAmount = 0;
                                                Session["OthReceiptItems"] = recItems;
                                                return Json(new { success = false, login = true, msg = "Cannot exceeed balance amaount to pay.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                               
                                            }
                                            else
                                            {
                                                var toUpdate = recItems.Single(x => x.Sir_receipt_no == receiptNo);
                                                toUpdate.payAmount =  Convert.ToDecimal(payAmount);
                                                Session["OthReceiptItems"] = recItems;
                                            }
                                        }
                                        else {
                                            return Json(new { success = false, login = true, msg = "Invalid receipt number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                        }
                                        
                                    }
                                    else
                                    {
                                        return Json(new { success = false, login = true, msg = "Invalid amount entered.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                                else {
                                    return Json(new { success = false, login = true, msg = "Please check line first.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                               
                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = "Please check line first.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        decimal totPaid = recItems.Sum(s => s.payAmount);
                        return Json(new { success = true, login = true, totPaid = totPaid }, JsonRequestBehavior.AllowGet);
                    }
                    else {
                        return Json(new { success = false, login = true,msg="Invalid receipt number.",type="Info" }, JsonRequestBehavior.AllowGet);
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

        public JsonResult saveOtherPartyPayment() {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<RecieptHeaderTBS> recItems=new List<RecieptHeaderTBS>();

                        if (Session["OthReceiptItems"] != null)
                        {
                            recItems = (List<RecieptHeaderTBS>)Session["OthReceiptItems"];
                        }
                        if (recItems.Count == 0) {
                            return Json(new { success = false, login = true, msg = "Invalid item selected.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        var updatedCount=recItems.Count(m=>m.selected==1 && m.payAmount>0);
                        if (updatedCount == 0)
                        {
                            return Json(new { success = false, login = true, msg = "No receipt selected for save.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        string checkTimeMsg = string.Empty;
                        if (CheckServerDateTime(out checkTimeMsg) == false)
                        {
                            return Json(new { success = false, login = true, msg = checkTimeMsg, type = "Info" }, JsonRequestBehavior.AllowGet);
                        }

                        

                        RecieptHeaderTBS reciptVal = new RecieptHeaderTBS();
                        reciptVal.Sir_receipt_type = "OTHER";
                        List<Hpr_SysParameter> para = CHNLSVC.Sales.GetAll_hpr_Para("ADREFMAXDT", "COM", company);
                        if (para.Count <= 0)
                        {
                            return Json(new { success = false, login = true, msg = "System parameter not setup for Advance receipt valid period.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                       
                        decimal totalPayble = recItems.Where(m => m.selected == 1 && m.payAmount > 0).Sum(n => n.payAmount);

                        if (totalPayble == 0) {
                            return Json(new { success = false, login = true, msg = "Please select receipt and add paying amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }


                        List<RecieptItemTBS> RecieptItemList = new List<RecieptItemTBS>();
                        List<RecieptItemTBS> sesRecieptItemList = Session["RecieptItemList"] as List<RecieptItemTBS>;
                        if (sesRecieptItemList != null)
                        {
                            RecieptItemList = sesRecieptItemList;
                        }
                        if (RecieptItemList.Count  > 1) {
                            return Json(new { success = false, login = true, msg = "Please add payments using one payment method.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        decimal payTotal = RecieptItemList.Sum(s => s.Sird_settle_amt);
                        if (payTotal == 0)
                        {
                            return Json(new { success = false, login = true, msg = "Please add payments before save.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        if (totalPayble != payTotal) {
                            return Json(new { success = false, login = true, msg = "You need to pay total amount to proceed.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        var othParty = recItems.First(m => m.selected == 1 && m.payAmount > 0);
                        reciptVal.Sir_debtor_cd = othParty.Sir_oth_partycd;
                        string Division = company;
                        reciptVal.Sir_com_cd = company;
                        reciptVal.Sir_direct = true;
                        reciptVal.Sir_profit_center_cd = userDefPro;

                        reciptVal.Sir_tot_settle_amt = totalPayble;
                        reciptVal.Sir_currency_cd = "LKR";
                        reciptVal.Sir_act = true;
                        reciptVal.Sir_session_id = Session["SessionID"].ToString();
                        reciptVal.Sir_create_by = userId;
                        reciptVal.Sir_create_when = DateTime.Now;
                        reciptVal.Sir_seq_no = CHNLSVC.Inventory.GetSerialID();
                        MasterBusinessEntity custProf = GetbyCustCD(reciptVal.Sir_debtor_cd);
                        if (custProf.Mbe_cd != null && custProf.Mbe_act == true)
                        {
                            reciptVal.Sir_debtor_name = custProf.Mbe_name;
                            reciptVal.Sir_debtor_add_1 = custProf.Mbe_add1;
                            reciptVal.Sir_debtor_add_2 = custProf.Mbe_add2;
                            reciptVal.Sir_tel_no=custProf.Mbe_tel;
                            reciptVal.Sir_mob_no=custProf.Mbe_mob;
                            reciptVal.Sir_nic_no = custProf.Mbe_nic;
                        }
                        else if (custProf.Mbe_cd != null && custProf.Mbe_act == false)
                        {
                            return Json(new { success = false, login = true, msg = Resource.txtInacCus, data = "", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            GroupBussinessEntity _grupProf = GetbyCustCDGrup(reciptVal.Sir_debtor_cd);
                            if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                            {
                                reciptVal.Sir_debtor_name = _grupProf.Mbg_name;
                                reciptVal.Sir_debtor_add_1 = _grupProf.Mbg_add1;
                                reciptVal.Sir_debtor_add_2 = _grupProf.Mbg_add2;
                                reciptVal.Sir_tel_no = _grupProf.Mbg_tel;
                                reciptVal.Sir_mob_no = _grupProf.Mbg_mob;
                                reciptVal.Sir_nic_no = _grupProf.Mbg_nic;
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
                        reciptVal.Sir_prefix = Division;
                        reciptVal.Sir_manual_ref_no = "0";

                        MasterAutoNumber masterAuto = new MasterAutoNumber();
                        masterAuto.Aut_cate_cd = userDefPro;
                        masterAuto.Aut_cate_tp = "PC";
                        masterAuto.Aut_direction = null;
                        masterAuto.Aut_modify_dt = null;
                        masterAuto.Aut_moduleid = "RECEIPT";
                        masterAuto.Aut_number = 5;//what is Aut_number
                        masterAuto.Aut_start_char = Division;
                        masterAuto.Aut_year = null;

                        DataTable _pcInfo = new DataTable();
                        _pcInfo = CHNLSVC.Sales.GetProfitCenterTable(company, userDefPro);


                        MasterAutoNumber masterAutoRecTp = new MasterAutoNumber();
                        masterAutoRecTp.Aut_cate_cd = userDefPro;
                        masterAutoRecTp.Aut_cate_tp = "PC";
                        masterAutoRecTp.Aut_direction = null;
                        masterAutoRecTp.Aut_modify_dt = null;

                        if (_pcInfo.Rows[0]["mpc_ope_cd"].ToString() == "INV_LRP" && userDefPro == "LRP")
                        {
                            masterAutoRecTp.Aut_moduleid = "REC_LRP";
                        }
                        else
                        {
                            masterAutoRecTp.Aut_moduleid = "RECEIPT";
                        }
                        masterAutoRecTp.Aut_number = 5;//what is Aut_number
                        masterAutoRecTp.Aut_start_char = reciptVal.Sir_receipt_type.Trim();
                        masterAutoRecTp.Aut_year = null;
                        string docNo="";


                        List<RecieptItemTBS> RecieptItemListNew = new List<RecieptItemTBS>();
                        foreach (RecieptHeaderTBS  item in recItems) {
                            RecieptItemTBS receiptItem = new RecieptItemTBS();
                            if (item.Sir_oth_party == 1 && item.payAmount > 0 && item.selected == 1) { 
                                receiptItem.Sird_anal_1=RecieptItemList[0].Sird_anal_1;
                                receiptItem.Sird_anal_2 =RecieptItemList[0].Sird_anal_2;
                                receiptItem.Sird_anal_3 =RecieptItemList[0].Sird_anal_3;
                                receiptItem.Sird_anal_4 =RecieptItemList[0].Sird_anal_4;
                                receiptItem.Sird_anal_5 =RecieptItemList[0].Sird_anal_5;
                                receiptItem.Sird_cc_expiry_dt=RecieptItemList[0].Sird_cc_expiry_dt;
                                receiptItem.Sird_cc_is_promo=RecieptItemList[0].Sird_cc_is_promo;
                                receiptItem.Sird_cc_period=RecieptItemList[0].Sird_cc_period;
                                receiptItem.Sird_cc_tp=RecieptItemList[0].Sird_cc_tp;
                                receiptItem.Sird_chq_bank_cd=RecieptItemList[0].Sird_chq_bank_cd;
                                receiptItem.Sird_chq_branch=RecieptItemList[0].Sird_chq_branch;
                                receiptItem.Sird_credit_card_bank=RecieptItemList[0].Sird_credit_card_bank;
                                receiptItem.Sird_deposit_bank_cd=RecieptItemList[0].Sird_deposit_bank_cd;
                                receiptItem.Sird_deposit_branch=RecieptItemList[0].Sird_deposit_branch;
                                receiptItem.Sird_gv_issue_dt=RecieptItemList[0].Sird_gv_issue_dt;
                                receiptItem.Sird_gv_issue_loc=RecieptItemList[0].Sird_gv_issue_loc;
                                receiptItem.Sird_inv_no=item.Sir_receipt_no;
                                receiptItem.Sird_seq_no=reciptVal.Sir_seq_no;
                                receiptItem.Sird_line_no=RecieptItemList[0].Sird_line_no;
                                receiptItem.Sird_pay_tp=RecieptItemList[0].Sird_pay_tp;
                                receiptItem.Sird_receipt_no=RecieptItemList[0].Sird_receipt_no;
                                receiptItem.Sird_ref_no=RecieptItemList[0].Sird_ref_no;
                                receiptItem.Sird_settle_amt=item.payAmount;
                                receiptItem.Sird_sim_ser=RecieptItemList[0].Sird_sim_ser;
                                receiptItem.Sird_rmk = RecieptItemList[0].Sird_rmk;
                                RecieptItemListNew.Add(receiptItem);
                            }
                        }

                        Int32 effect = CHNLSVC.Tours.saveOtherPartyPayments(reciptVal, recItems, RecieptItemListNew, masterAuto, out docNo);

                        if (effect == -1)
                        {
                            return Json(new { success = false, login = true, msg = docNo, type = "Error" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = true, login = true,  msg ="Successfully create the other party payment receipt. Receipt No : "+ docNo, type = "Success" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult clearSession() {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    Session["RecieptItemList"] = null;
                    Session["OthReceiptItems"] = null;
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
    }
}