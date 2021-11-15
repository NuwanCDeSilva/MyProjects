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
    public class PaymentController : BaseController
    {
        // GET: Payment
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

        public  JsonResult loadPaymenttypes(string payFormType)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string subChanel = HttpContext.Session["UserSubChannl"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string payFrom = payFormType.Trim();
                    List<PaymentType> _paymentTypeRef = CHNLSVC.Sales.GetPossiblePaymentTypes_new(company, subChanel, userDefPro, payFrom, DateTime.Now,1);
                    List<string> payTypes = new List<string>();
                    if (_paymentTypeRef != null && _paymentTypeRef.Count > 0)
                    {
                        foreach (PaymentType pt in _paymentTypeRef)
                        {
                            payTypes.Add(pt.Stp_pay_tp);
                        }
                    }
                    payTypes = payTypes.Distinct().ToList();
                    if (payTypes != null)
                    {
                        if (payTypes.Count > 0)
                        {
                            return Json(new { success = true, login = true, data = payTypes, defPayType = payTypes[0].ToString().ToUpper() }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = Resource.txtNoPayMde, type = "Info" }, JsonRequestBehavior.AllowGet);
                        }

                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = Resource.txtNoPayMde, type = "Info" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult getBankCode(string bankId) { 
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string subChanel = HttpContext.Session["UserSubChannl"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string bank = bankId.Trim();
                    if (bank != "")
                    {
                        string bankCode = CHNLSVC.Tours.getBankCode(bankId);
                        if (bankCode != null)
                        {
                            return Json(new { success = true, login = true, code = bankCode }, JsonRequestBehavior.AllowGet);
                        }
                        else {
                            return Json(new { success = false, login = true, msg = Resource.errInvBnkCd, type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else {
                        return Json(new { success = false, login = true, msg = Resource.errInvBnkCd, type = "Info" }, JsonRequestBehavior.AllowGet);
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

        public JsonResult LoadMIDno(string online, string offline, string promotion, string bank, string piriod)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string subChanel = HttpContext.Session["UserSubChannl"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    int mode = 0;
                    string branch_code = "";
                    string pc = userDefPro;
                    //string MIDcode = "";
                    int period = 0;
                    if (offline == "true") mode = 0;
                    if (online == "true") mode = 1;
                    if (bank != "") branch_code = bank;
                    if (promotion == "true") period = Convert.ToInt32(piriod);
                    DataTable MID = CHNLSVC.Sales.get_bank_mid_code(branch_code, pc, mode, period,DateTime.Now,company);
                    if (MID.Rows.Count > 0)
                    {
                        DataRow dr;

                        dr = MID.Rows[0];
                        return Json(new { success = true, login = true, micode = dr["MPM_MID_NO"].ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No MID code" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult LoadCardType(string bank)
        {

            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string subChanel = HttpContext.Session["UserSubChannl"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    MasterOutsideParty _bankAccounts = CHNLSVC.Sales.GetOutSidePartyDetailsById(bank.Trim());
                    if (_bankAccounts != null)
                    {
                        DataTable _dt = CHNLSVC.Sales.GetBankCC(_bankAccounts.Mbi_cd);
                        if (_dt.Rows.Count > 0)
                        {
                            List<ComboBoxObject> oList = new List<ComboBoxObject>();
                            foreach (DataRow dtr in _dt.Rows)
                            {

                                ComboBoxObject cmb = new ComboBoxObject();
                                cmb.Text = dtr["MBCT_CC_TP"].ToString();
                                cmb.Value = dtr["MBCT_CC_TP"].ToString();
                                oList.Add(cmb);
                            }

                            return Json(new { success = true, login = true, data = oList }, JsonRequestBehavior.AllowGet);
                            //comboBoxCardType.DataSource = _dt;
                            //comboBoxCardType.DisplayMember = "mbct_cc_tp";
                            //comboBoxCardType.ValueMember = "mbct_cc_tp";
                        }
                        else
                        {
                            return Json(new { success = true, login = true, data = "" }, JsonRequestBehavior.AllowGet);
                        }

                        //var dr = _dt.AsEnumerable().Where(x => x["MBCT_CC_TP"].ToString() == "VISA");

                        //if (dr.Count() > 0)
                        //    comboBoxCardType.SelectedValue = "VISA";
                    }
                    else {
                        return Json(new { success = false, login = true }, JsonRequestBehavior.AllowGet);
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
        public JsonResult getAdvanceRefAmount(string cuscd, string receiptno)
        { 
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    receiptno = receiptno.Trim();
                    if (receiptno != "")
                    {
                        cuscd = cuscd.Trim();
                        string amount = CHNLSVC.Tours.getAdvanceRefAmount(cuscd, company, receiptno);
                        if (amount != null)
                        {
                            return Json(new { success = true, login = true, data = amount }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = Resource.errInvRefCd, type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else {
                        return Json(new { success = false, login = true, msg = Resource.errEnterRefCd, type = "Info" }, JsonRequestBehavior.AllowGet);
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

        public JsonResult getCreditRefAmount(string cuscd, string receiptno)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {

                    cuscd = cuscd.Trim();
                    string amount = CHNLSVC.Tours.getCreditRefAmount(cuscd, company, receiptno, userDefPro);
                    if (amount != null)
                    {
                        return Json(new { success = true, login = true, data = amount }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = Resource.errInvRefCd, type = "Info" }, JsonRequestBehavior.AllowGet);
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

        public JsonResult getGiftVoucherDetails(string voucherPageNo)
        { 
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    voucherPageNo = voucherPageNo.Trim();
                    int n;
                    bool isNumeric = int.TryParse(voucherPageNo, out n);
                    if (voucherPageNo != "" && isNumeric)
                    {
                        List<GiftVoucherPages> _gift = new List<GiftVoucherPages>();
                        List<GiftVoucherPages> _Allgv = CHNLSVC.Inventory.GetGiftVoucherPages(null, Convert.ToInt32(voucherPageNo));
                        if (_Allgv.Count>0)
                        {
                            if (_Allgv != null)
                            {
                                foreach (GiftVoucherPages _tmp in _Allgv)
                                {
                                    DataTable _allCom = CHNLSVC.Inventory.GetGVAlwCom(company, _tmp.Gvp_gv_cd, 1);
                                    if (_allCom.Rows.Count > 0)
                                    {
                                        _gift.Add(_tmp);
                                    }

                                }
                            }
                            return Json(new { success = true, login = true, data = _gift }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = Resource.errInvalidVou, type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else {
                        return Json(new { success = false, login = true, msg = Resource.errInvalidVou, type = "Info" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult getLoyaltyDetails(string customer, string loyalNu)
        { 
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    loyalNu = loyalNu.Trim();
                    LoyaltyMemeber loyalCus = CHNLSVC.Sales.getLoyaltyDetails(customer, loyalNu);
                    LoyaltyPointRedeemDefinition _definition = CHNLSVC.Sales.GetLoyaltyRedeemDefinition("COM", company, DateTime.Now, loyalCus.Salcm_loty_tp);
                    loyalCus.Salcm_col_pt = _definition.Salre_red_pt;
                    return Json(new { success = true, login = true, data = loyalCus }, JsonRequestBehavior.AllowGet);
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
       
        bool IsZeroAllow = false;
        decimal TotalAmount = 0;

        public JsonResult updateEazyCashStarPoPayment(string payMode, string totAmount, string addedAmount, string invoiceNo,string customer)
        { 
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (totAmount == "") {
                        return Json(new { success = false, login = true, msg = "Add invoice item to pay.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    TotalAmount = Convert.ToDecimal(totAmount);
                    if (TotalAmount <= 0)
                    {
                        return Json(new { success = false, login = true, msg = "Add invoice item to pay.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    decimal n;
                    bool isNumeric = decimal.TryParse(addedAmount, out n);
                    if (isNumeric)
                    {
                        string paidAmountString = Session["totalPaidAmount"] as string;
                        decimal totPaidAmount = Convert.ToDecimal(paidAmountString);
                        // Session["totAmount"] = totAmount;
                        decimal currentBalanceAmount = (TotalAmount - totPaidAmount);
                        decimal currnetPayAmount = Convert.ToDecimal(addedAmount);
                        if (currentBalanceAmount >= currnetPayAmount)
                        {
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
                            RecieptItemTBS _item = new RecieptItemTBS();
                            if (!string.IsNullOrEmpty(customer))
                            {
                                BlackListCustomers _cus = CHNLSVC.Sales.GetBlackListCustomerDetails(customer, 1);
                                if (_cus.Hbl_cust_cd != null)
                                {
                                    return Json(new { success = false, login = true, msg = "This Customer is Blacklist Customer.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    bool has = RecieptItemList.Any(list => list.Sird_pay_tp == payMode && list.Sird_inv_no == invoiceNo);
                                    if (!has)
                                    {
                                        if (currentBalanceAmount >= currnetPayAmount)
                                        {
                                            _item.Sird_inv_no = invoiceNo;
                                            _item.Sird_pay_tp = payMode;
                                            _item.Sird_settle_amt = Math.Round(Convert.ToDecimal(addedAmount), 4);
                                            RecieptItemList.Add(_item);
                                            Session["RecieptItemList"] = RecieptItemList;
                                            totPaidAmount = RecieptItemList.Sum(r => r.Sird_settle_amt);
                                            Session["totalPaidAmount"] = totPaidAmount;
                                            return Json(new { success = true, login = true, totPaid = totPaidAmount.ToString() }, JsonRequestBehavior.AllowGet);
                                        }
                                        else {
                                            return Json(new { success = false, login = true, msg = "Exceed the pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                        }

                                    }
                                    else {
                                        return Json(new { success = false, login = true, msg = "Cannot add duplicate payment.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                    }
                                    
                                }
                            }
                            else {
                                bool has = RecieptItemList.Any(list => list.Sird_pay_tp == payMode && list.Sird_inv_no == invoiceNo);
                                if (!has)
                                {
                                    if (currentBalanceAmount >= currnetPayAmount)
                                    {
                                        _item.Sird_inv_no = invoiceNo;
                                        _item.Sird_pay_tp = payMode;
                                        _item.Sird_settle_amt = Math.Round(Convert.ToDecimal(addedAmount), 4);
                                        RecieptItemList.Add(_item);
                                        Session["RecieptItemList"] = RecieptItemList;
                                        totPaidAmount = RecieptItemList.Sum(r => r.Sird_settle_amt);
                                        Session["totalPaidAmount"] = totPaidAmount;
                                        return Json(new { success = true, login = true, totPaid = totPaidAmount.ToString() }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        return Json(new { success = false, login = true, msg = "Exceed the pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                    }

                                }
                                else
                                {
                                    return Json(new { success = false, login = true, msg = "Cannot add duplicate payment.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                            }
                           
                            
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "Exceed the pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else {
                        return Json(new { success = false, login = true, msg = "Invalid price amount entered.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult updateBankSlipPayment(string payMode, string totAmount, string addedAmount, string accountNo, string accountDate, string depositBank, string depositBranch, string invoiceNo,string customer)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string subChannel = HttpContext.Session["UserSubChannl"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (totAmount == "")
                    {
                        return Json(new { success = false, login = true, msg = "Add invoice item to pay.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    payMode = payMode.Trim();
                    accountNo = accountNo.Trim();
                    accountDate = accountDate.Trim();
                    invoiceNo = invoiceNo.Trim();
                    depositBank = depositBank.Trim();
                    depositBranch = depositBranch.Trim();
                    addedAmount = addedAmount.Trim();
                    TotalAmount = Convert.ToDecimal(totAmount);
                    if (TotalAmount <= 0)
                    {
                        return Json(new { success = false, login = true, msg = "Add invoice item to pay.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    decimal n;
                    bool isNumeric = decimal.TryParse(addedAmount, out n);
                    if (isNumeric)
                    {
                        if (!string.IsNullOrEmpty(accountNo))
                        {
                            if (!string.IsNullOrEmpty(accountDate))
                            {
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
                                RecieptItemTBS _item = new RecieptItemTBS();
                                string paidAmountString = Session["totalPaidAmount"] as string;
                                decimal totPaidAmount = Convert.ToDecimal(paidAmountString);
                                // Session["totAmount"] = totAmount;
                                decimal currentBalanceAmount = (TotalAmount - totPaidAmount);
                                decimal currnetPayAmount = Convert.ToDecimal(addedAmount);
                                DataTable _dtDepBank = CHNLSVC.General.getSubChannelDet(company, subChannel);
                                if (_dtDepBank.Rows.Count > 0)
                                {
                                    if (Convert.ToInt32(_dtDepBank.Rows[0]["MSSC_IS_BNK_MAN"]) == 1)
                                    {
                                        DataTable BankName = CHNLSVC.Sales.get_Dep_Bank_Name(company, userDefPro, payMode, depositBank);
                                        if (BankName.Rows.Count == 0)
                                        {
                                            return Json(new { success = false, login = true, msg = "Invalid deposit bank account.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(customer))
                                            {
                                                BlackListCustomers _cus = CHNLSVC.Sales.GetBlackListCustomerDetails(customer, 1);
                                                if (_cus.Hbl_cust_cd != null)
                                                {
                                                    return Json(new { success = false, login = true, msg = "This Customer is Blacklist Customer.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                }
                                                else
                                                {
                                                    bool has = RecieptItemList.Any(list => list.Sird_ref_no == accountNo && list.Sird_inv_no == invoiceNo);
                                                     if (!has)
                                                     {
                                                         if (currentBalanceAmount >= currnetPayAmount)
                                                         {
                                                             _item.Sird_ref_no = accountNo;
                                                             //accountDate
                                                             _item.Sird_deposit_bank_cd = depositBank;
                                                             _item.Sird_deposit_branch = depositBranch;
                                                             _item.Sird_inv_no = invoiceNo;
                                                             _item.Sird_pay_tp = payMode;
                                                             _item.Sird_settle_amt = Math.Round(Convert.ToDecimal(addedAmount), 4);
                                                             RecieptItemList.Add(_item);
                                                             Session["RecieptItemList"] = RecieptItemList;
                                                             totPaidAmount = RecieptItemList.Sum(r => r.Sird_settle_amt);
                                                             Session["totalPaidAmount"] = totPaidAmount;
                                                             return Json(new { success = true, login = true, totPaid = totPaidAmount.ToString() }, JsonRequestBehavior.AllowGet);
                                                         }
                                                         else
                                                         {
                                                             return Json(new { success = false, login = true, msg = "Exceed the pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                         }
                                                     }
                                                     else {
                                                         return Json(new { success = false, login = true, msg = "Cannot duplicate same bank slip.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                     }
                                                }
                                            }
                                            else
                                            {
                                                BlackListCustomers _cus = CHNLSVC.Sales.GetBlackListCustomerDetails(customer, 1);
                                                if (_cus.Hbl_cust_cd != null)
                                                {
                                                    return Json(new { success = false, login = true, msg = "This Customer is Blacklist Customer.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                }
                                                else
                                                {
                                                    bool has = RecieptItemList.Any(list => list.Sird_ref_no == accountNo && list.Sird_inv_no == invoiceNo);
                                                    if (!has)
                                                    {
                                                        if (currentBalanceAmount >= currnetPayAmount)
                                                        {
                                                            _item.Sird_ref_no = accountNo;
                                                            //accountDate
                                                            _item.Sird_deposit_bank_cd = depositBank;
                                                            _item.Sird_deposit_branch = depositBranch;
                                                            _item.Sird_inv_no = invoiceNo;
                                                            _item.Sird_pay_tp = payMode;
                                                            _item.Sird_settle_amt = Math.Round(Convert.ToDecimal(addedAmount), 4);
                                                            RecieptItemList.Add(_item);
                                                            Session["RecieptItemList"] = RecieptItemList;
                                                            totPaidAmount = RecieptItemList.Sum(r => r.Sird_settle_amt);
                                                            Session["totalPaidAmount"] = totPaidAmount;
                                                            return Json(new { success = true, login = true, totPaid = totPaidAmount.ToString() }, JsonRequestBehavior.AllowGet);
                                                        }
                                                        else
                                                        {
                                                            return Json(new { success = false, login = true, msg = "Exceed the pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        return Json(new { success = false, login = true, msg = "Cannot duplicate same bank slip.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                    }
                                                }
                                            }

                                        }
                                    }
                                    else {
                                        BlackListCustomers _cus = CHNLSVC.Sales.GetBlackListCustomerDetails(customer, 1);
                                        if (_cus.Hbl_cust_cd != null)
                                        {
                                            return Json(new { success = false, login = true, msg = "This Customer is Blacklist Customer.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                        }
                                        else
                                        {
                                            bool has = RecieptItemList.Any(list => list.Sird_ref_no == accountNo && list.Sird_inv_no == invoiceNo);
                                            if (!has)
                                            {
                                                if (currentBalanceAmount >= currnetPayAmount)
                                                {
                                                    _item.Sird_ref_no = accountNo;
                                                    //accountDate
                                                    _item.Sird_deposit_bank_cd = depositBank;
                                                    _item.Sird_deposit_branch = depositBranch;
                                                    _item.Sird_inv_no = invoiceNo;
                                                    _item.Sird_pay_tp = payMode;
                                                    _item.Sird_settle_amt = Math.Round(Convert.ToDecimal(addedAmount), 4);
                                                    RecieptItemList.Add(_item);
                                                    Session["RecieptItemList"] = RecieptItemList;
                                                    totPaidAmount = RecieptItemList.Sum(r => r.Sird_settle_amt);
                                                    Session["totalPaidAmount"] = totPaidAmount;
                                                    return Json(new { success = true, login = true, totPaid = totPaidAmount.ToString() }, JsonRequestBehavior.AllowGet);
                                                }
                                                else
                                                {
                                                    return Json(new { success = false, login = true, msg = "Exceed the pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                }
                                            }
                                            else
                                            {
                                                return Json(new { success = false, login = true, msg = "Cannot duplicate same bank slip.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                            }
                                        }
                                    }
                                }
                                else {
                                    BlackListCustomers _cus = CHNLSVC.Sales.GetBlackListCustomerDetails(customer, 1);
                                    if (_cus.Hbl_cust_cd != null)
                                    {
                                        return Json(new { success = false, login = true, msg = "This Customer is Blacklist Customer.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        bool has = RecieptItemList.Any(list => list.Sird_ref_no == accountNo && list.Sird_inv_no == invoiceNo);
                                        if (!has)
                                        {
                                            if (currentBalanceAmount >= currnetPayAmount)
                                            {
                                                _item.Sird_ref_no = accountNo;
                                                //accountDate
                                                _item.Sird_deposit_bank_cd = depositBank;
                                                _item.Sird_deposit_branch = depositBranch;
                                                _item.Sird_inv_no = invoiceNo;
                                                _item.Sird_pay_tp = payMode;
                                                _item.Sird_settle_amt = Math.Round(Convert.ToDecimal(addedAmount), 4);
                                                RecieptItemList.Add(_item);
                                                Session["RecieptItemList"] = RecieptItemList;
                                                totPaidAmount = RecieptItemList.Sum(r => r.Sird_settle_amt);
                                                Session["totalPaidAmount"] = totPaidAmount;
                                                return Json(new { success = true, login = true, totPaid = totPaidAmount.ToString() }, JsonRequestBehavior.AllowGet);
                                            }
                                            else
                                            {
                                                return Json(new { success = false, login = true, msg = "Exceed the pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                            }
                                        }
                                        else
                                        {
                                            return Json(new { success = false, login = true, msg = "Cannot duplicate same bank slip.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                }
                               
                            }
                            else {
                                return Json(new { success = false, login = true, msg = "Please enter account date.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            
                        }
                        else {
                            return Json(new { success = false, login = true, msg = "Please enter account number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }

                    }
                    else {
                        return Json(new { success = false, login = true, msg = "Invalid price amount entered.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult updateDEBTPayment(string payMode, string totAmount, string addedAmount, string cardNo, string cardBank, string cardBankCd, string depositBank, string depositBranch, string invoiceNo, string customer)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string subChannel = HttpContext.Session["UserSubChannl"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (totAmount == "")
                    {
                        return Json(new { success = false, login = true, msg = "Add invoice item to pay.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    payMode = payMode.Trim();
                    cardNo = cardNo.Trim();
                    cardBank = cardBank.Trim();
                    cardBankCd = cardBankCd.Trim();
                    invoiceNo = invoiceNo.Trim();
                    depositBank = depositBank.Trim();
                    depositBranch = depositBranch.Trim();
                    addedAmount = addedAmount.Trim();
                    TotalAmount = Convert.ToDecimal(totAmount);
                    if (TotalAmount <= 0)
                    {
                        return Json(new { success = false, login = true, msg = "Add invoice item to pay.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    decimal n;
                    bool isNumeric = decimal.TryParse(addedAmount, out n);
                    if (isNumeric)
                    {
                        if (!string.IsNullOrEmpty(cardNo))
                        {
                            MasterOutsideParty _bankAccounts = CHNLSVC.Sales.GetOutSidePartyDetailsById(cardBankCd.Trim());
                            if (_bankAccounts == null)
                            {
                                return Json(new { success = false, login = true, msg = "Invalid bank code.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            else {
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
                                RecieptItemTBS _item = new RecieptItemTBS();
                                string paidAmountString = Session["totalPaidAmount"] as string;
                                decimal totPaidAmount = Convert.ToDecimal(paidAmountString);
                                // Session["totAmount"] = totAmount;
                                decimal currentBalanceAmount = (TotalAmount - totPaidAmount);
                                decimal currnetPayAmount = Convert.ToDecimal(addedAmount);
                                DataTable _dtDepBank = CHNLSVC.General.getSubChannelDet(company, subChannel);
                                if (_dtDepBank.Rows.Count > 0)
                                {
                                    if (Convert.ToInt32(_dtDepBank.Rows[0]["MSSC_IS_BNK_MAN"]) == 1)
                                    {
                                        DataTable BankName = CHNLSVC.Sales.get_Dep_Bank_Name(company, userDefPro, payMode, depositBank);
                                        if (BankName.Rows.Count == 0)
                                        {
                                            return Json(new { success = false, login = true, msg = "Invalid deposit bank account.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(customer))
                                            {
                                                BlackListCustomers _cus = CHNLSVC.Sales.GetBlackListCustomerDetails(customer, 1);
                                                if (_cus.Hbl_cust_cd != null)
                                                {
                                                    return Json(new { success = false, login = true, msg = "This Customer is Blacklist Customer.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                }
                                                else
                                                {
                                                    bool has = RecieptItemList.Any(list => list.Sird_ref_no == cardNo && list.Sird_inv_no == invoiceNo);
                                                    if (!has)
                                                    {
                                                        if (currentBalanceAmount >= currnetPayAmount)
                                                        {
                                                            _item.Sird_chq_bank_cd = _bankAccounts.Mbi_cd;
                                                            _item.Sird_ref_no = cardNo;
                                                            _item.Sird_deposit_bank_cd = depositBank;
                                                            _item.Sird_deposit_branch = depositBranch;
                                                            _item.Sird_inv_no = invoiceNo;
                                                            _item.Sird_pay_tp = payMode;
                                                            _item.Sird_settle_amt = Math.Round(Convert.ToDecimal(addedAmount), 4);
                                                            RecieptItemList.Add(_item);
                                                            Session["RecieptItemList"] = RecieptItemList;
                                                            totPaidAmount = RecieptItemList.Sum(r => r.Sird_settle_amt);
                                                            Session["totalPaidAmount"] = totPaidAmount;
                                                            return Json(new { success = true, login = true, totPaid = totPaidAmount.ToString() }, JsonRequestBehavior.AllowGet);
                                                        }
                                                        else
                                                        {
                                                            return Json(new { success = false, login = true, msg = "Exceed the pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                        }
                                                    }
                                                    else {
                                                        return Json(new { success = false, login = true, msg = "Cannot duplicate debit card.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                    }
                                                }
                                            }
                                            else {
                                                bool has = RecieptItemList.Any(list => list.Sird_ref_no == cardNo && list.Sird_inv_no == invoiceNo);
                                                if (!has)
                                                {
                                                    if (currentBalanceAmount >= currnetPayAmount)
                                                    {
                                                        _item.Sird_chq_bank_cd = _bankAccounts.Mbi_cd;
                                                        _item.Sird_ref_no = cardNo;
                                                        _item.Sird_deposit_bank_cd = depositBank;
                                                        _item.Sird_deposit_branch = depositBranch;
                                                        _item.Sird_inv_no = invoiceNo;
                                                        _item.Sird_pay_tp = payMode;
                                                        _item.Sird_settle_amt = Math.Round(Convert.ToDecimal(addedAmount), 4);
                                                        RecieptItemList.Add(_item);
                                                        Session["RecieptItemList"] = RecieptItemList;
                                                        totPaidAmount = RecieptItemList.Sum(r => r.Sird_settle_amt);
                                                        Session["totalPaidAmount"] = totPaidAmount;
                                                        return Json(new { success = true, login = true, totPaid = totPaidAmount.ToString() }, JsonRequestBehavior.AllowGet);
                                                    }
                                                    else
                                                    {
                                                        return Json(new { success = false, login = true, msg = "Exceed the pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                    }
                                                }
                                                else
                                                {
                                                    return Json(new { success = false, login = true, msg = "Cannot duplicate debit card.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                }
                                            }

                                        }
                                    }
                                    else {
                                        if (!string.IsNullOrEmpty(customer))
                                        {
                                            BlackListCustomers _cus = CHNLSVC.Sales.GetBlackListCustomerDetails(customer, 1);
                                            if (_cus.Hbl_cust_cd != null)
                                            {
                                                return Json(new { success = false, login = true, msg = "This Customer is Blacklist Customer.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                            }
                                            else
                                            {
                                                bool has = RecieptItemList.Any(list => list.Sird_ref_no == cardNo && list.Sird_inv_no == invoiceNo);
                                                if (!has)
                                                {
                                                    if (currentBalanceAmount >= currnetPayAmount)
                                                    {
                                                        _item.Sird_chq_bank_cd = _bankAccounts.Mbi_cd;
                                                        _item.Sird_ref_no = cardNo;
                                                        _item.Sird_deposit_bank_cd = depositBank;
                                                        _item.Sird_deposit_branch = depositBranch;
                                                        _item.Sird_inv_no = invoiceNo;
                                                        _item.Sird_pay_tp = payMode;
                                                        _item.Sird_settle_amt = Math.Round(Convert.ToDecimal(addedAmount), 4);
                                                        RecieptItemList.Add(_item);
                                                        Session["RecieptItemList"] = RecieptItemList;
                                                        totPaidAmount = RecieptItemList.Sum(r => r.Sird_settle_amt);
                                                        Session["totalPaidAmount"] = totPaidAmount;
                                                        return Json(new { success = true, login = true, totPaid = totPaidAmount.ToString() }, JsonRequestBehavior.AllowGet);
                                                    }
                                                    else
                                                    {
                                                        return Json(new { success = false, login = true, msg = "Exceed the pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                    }
                                                }
                                                else
                                                {
                                                    return Json(new { success = false, login = true, msg = "Cannot duplicate debit card.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                }
                                            }
                                        }
                                        else {
                                            bool has = RecieptItemList.Any(list => list.Sird_ref_no == cardNo && list.Sird_inv_no == invoiceNo);
                                            if (!has)
                                            {
                                                if (currentBalanceAmount >= currnetPayAmount)
                                                {
                                                    _item.Sird_chq_bank_cd = _bankAccounts.Mbi_cd;
                                                    _item.Sird_ref_no = cardNo;
                                                    _item.Sird_deposit_bank_cd = depositBank;
                                                    _item.Sird_deposit_branch = depositBranch;
                                                    _item.Sird_inv_no = invoiceNo;
                                                    _item.Sird_pay_tp = payMode;
                                                    _item.Sird_settle_amt = Math.Round(Convert.ToDecimal(addedAmount), 4);
                                                    RecieptItemList.Add(_item);
                                                    Session["RecieptItemList"] = RecieptItemList;
                                                    totPaidAmount = RecieptItemList.Sum(r => r.Sird_settle_amt);
                                                    Session["totalPaidAmount"] = totPaidAmount;
                                                    return Json(new { success = true, login = true, totPaid = totPaidAmount.ToString() }, JsonRequestBehavior.AllowGet);
                                                }
                                                else
                                                {
                                                    return Json(new { success = false, login = true, msg = "Exceed the pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                }
                                            }
                                            else
                                            {
                                                return Json(new { success = false, login = true, msg = "Cannot duplicate debit card.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                            }
                                        }
                                    }
                                }
                                else {
                                    if (!string.IsNullOrEmpty(customer))
                                    {
                                        BlackListCustomers _cus = CHNLSVC.Sales.GetBlackListCustomerDetails(customer, 1);
                                        if (_cus.Hbl_cust_cd != null)
                                        {
                                            return Json(new { success = false, login = true, msg = "This Customer is Blacklist Customer.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                        }
                                        else
                                        {
                                            bool has = RecieptItemList.Any(list => list.Sird_ref_no == cardNo && list.Sird_inv_no == invoiceNo);
                                            if (!has)
                                            {
                                                if (currentBalanceAmount >= currnetPayAmount)
                                                {
                                                    _item.Sird_chq_bank_cd = _bankAccounts.Mbi_cd;
                                                    _item.Sird_ref_no = cardNo;
                                                    _item.Sird_deposit_bank_cd = depositBank;
                                                    _item.Sird_deposit_branch = depositBranch;
                                                    _item.Sird_inv_no = invoiceNo;
                                                    _item.Sird_pay_tp = payMode;
                                                    _item.Sird_settle_amt = Math.Round(Convert.ToDecimal(addedAmount), 4);
                                                    RecieptItemList.Add(_item);
                                                    Session["RecieptItemList"] = RecieptItemList;
                                                    totPaidAmount = RecieptItemList.Sum(r => r.Sird_settle_amt);
                                                    Session["totalPaidAmount"] = totPaidAmount;
                                                    return Json(new { success = true, login = true, totPaid = totPaidAmount.ToString() }, JsonRequestBehavior.AllowGet);
                                                }
                                                else
                                                {
                                                    return Json(new { success = false, login = true, msg = "Exceed the pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                }
                                            }
                                            else
                                            {
                                                return Json(new { success = false, login = true, msg = "Cannot duplicate debit card.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                            }
                                        }
                                    }
                                    else {
                                        bool has = RecieptItemList.Any(list => list.Sird_ref_no == cardNo && list.Sird_inv_no == invoiceNo);
                                        if (!has)
                                        {
                                            if (currentBalanceAmount >= currnetPayAmount)
                                            {
                                                _item.Sird_chq_bank_cd = _bankAccounts.Mbi_cd;
                                                _item.Sird_ref_no = cardNo;
                                                _item.Sird_deposit_bank_cd = depositBank;
                                                _item.Sird_deposit_branch = depositBranch;
                                                _item.Sird_inv_no = invoiceNo;
                                                _item.Sird_pay_tp = payMode;
                                                _item.Sird_settle_amt = Math.Round(Convert.ToDecimal(addedAmount), 4);
                                                RecieptItemList.Add(_item);
                                                Session["RecieptItemList"] = RecieptItemList;
                                                totPaidAmount = RecieptItemList.Sum(r => r.Sird_settle_amt);
                                                Session["totalPaidAmount"] = totPaidAmount;
                                                return Json(new { success = true, login = true, totPaid = totPaidAmount.ToString() }, JsonRequestBehavior.AllowGet);
                                            }
                                            else
                                            {
                                                return Json(new { success = false, login = true, msg = "Exceed the pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                            }
                                        }
                                        else
                                        {
                                            return Json(new { success = false, login = true, msg = "Cannot duplicate debit card.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                }

                            }
                        }
                        else {
                            return Json(new { success = false, login = true, msg = "Invalid price card number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else { 
                        return Json(new { success = false, login = true, msg = "Invalid price amount entered.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult updateCashPayment(string payMode, string totAmount, string addedAmount,  string depositBank, string depositBranch, string invoiceNo, string customer)
        { 
        
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string subChannel = HttpContext.Session["UserSubChannl"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (totAmount == "")
                    {
                        return Json(new { success = false, login = true, msg = "Add invoice item to pay.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    payMode = payMode.Trim();
                    invoiceNo = invoiceNo.Trim();
                    depositBank = depositBank.Trim();
                    depositBranch = depositBranch.Trim();
                    addedAmount = addedAmount.Trim();
                    TotalAmount = Convert.ToDecimal(totAmount);
                    if (TotalAmount <= 0)
                    {
                        return Json(new { success = false, login = true, msg = "Add invoice item to pay.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    decimal n;
                    bool isNumeric = decimal.TryParse(addedAmount, out n);
                    if (isNumeric)
                    {
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
                        RecieptItemTBS _item = new RecieptItemTBS();
                        string paidAmountString = Session["totalPaidAmount"] as string;
                        decimal totPaidAmount = Convert.ToDecimal(paidAmountString);
                        decimal currentBalanceAmount = (TotalAmount - totPaidAmount);
                        decimal currnetPayAmount = Convert.ToDecimal(addedAmount);
                        DataTable _dtDepBank = CHNLSVC.General.getSubChannelDet(company, subChannel);
                        if (_dtDepBank.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(_dtDepBank.Rows[0]["MSSC_IS_BNK_MAN"]) == 1)
                            {
                                DataTable BankName = CHNLSVC.Sales.get_Dep_Bank_Name(company, userDefPro, payMode, depositBank);
                                if (BankName.Rows.Count == 0 && true == false)
                                {
                                    return Json(new { success = false, login = true, msg = "Invalid deposit bank account.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(customer))
                                    {
                                        BlackListCustomers _cus = CHNLSVC.Sales.GetBlackListCustomerDetails(customer, 1);
                                        if (_cus.Hbl_cust_cd != null)
                                        {
                                            return Json(new { success = false, login = true, msg = "This Customer is Blacklist Customer.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                        }
                                        else
                                        {

                                            bool has = RecieptItemList.Any(list => list.Sird_pay_tp == payMode && list.Sird_inv_no == invoiceNo);
                                            if (!has)
                                            {
                                                if (currentBalanceAmount >= currnetPayAmount)
                                                {
                                                    _item.Sird_deposit_bank_cd = depositBank;
                                                    _item.Sird_deposit_branch = depositBranch;
                                                    _item.Sird_inv_no = invoiceNo;
                                                    _item.Sird_pay_tp = payMode;
                                                    _item.Sird_settle_amt = Math.Round(Convert.ToDecimal(addedAmount), 4);
                                                    RecieptItemList.Add(_item);
                                                    Session["RecieptItemList"] = RecieptItemList;
                                                    totPaidAmount = RecieptItemList.Sum(r => r.Sird_settle_amt);
                                                    Session["totalPaidAmount"] = totPaidAmount;
                                                    return Json(new { success = true, login = true, totPaid = totPaidAmount.ToString() }, JsonRequestBehavior.AllowGet);
                                                }
                                                else
                                                {
                                                    return Json(new { success = false, login = true, msg = "Exceed the pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                }
                                            }
                                            else {
                                                return Json(new { success = false, login = true, msg = "Cannot add duplicate payment.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                            }

                                        }
                                    }
                                    else {
                                        bool has = RecieptItemList.Any(list => list.Sird_pay_tp == payMode && list.Sird_inv_no == invoiceNo);
                                        if (!has)
                                        {
                                            if (currentBalanceAmount >= currnetPayAmount)
                                            {
                                                _item.Sird_deposit_bank_cd = depositBank;
                                                _item.Sird_deposit_branch = depositBranch;
                                                _item.Sird_inv_no = invoiceNo;
                                                _item.Sird_pay_tp = payMode;
                                                _item.Sird_settle_amt = Math.Round(Convert.ToDecimal(addedAmount), 4);
                                                RecieptItemList.Add(_item);
                                                Session["RecieptItemList"] = RecieptItemList;
                                                totPaidAmount = RecieptItemList.Sum(r => r.Sird_settle_amt);
                                                Session["totalPaidAmount"] = totPaidAmount;
                                                return Json(new { success = true, login = true, totPaid = totPaidAmount.ToString() }, JsonRequestBehavior.AllowGet);
                                            }
                                            else
                                            {
                                                return Json(new { success = false, login = true, msg = "Exceed the pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                            }
                                        }
                                        else
                                        {
                                            return Json(new { success = false, login = true, msg = "Cannot add duplicate payment.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(customer))
                                {
                                    BlackListCustomers _cus = CHNLSVC.Sales.GetBlackListCustomerDetails(customer, 1);
                                    if (_cus.Hbl_cust_cd != null)
                                    {
                                        return Json(new { success = false, login = true, msg = "This Customer is Blacklist Customer.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        bool has = RecieptItemList.Any(list => list.Sird_pay_tp == payMode && list.Sird_inv_no == invoiceNo);
                                        if (!has)
                                        {
                                            if (currentBalanceAmount >= currnetPayAmount)
                                            {
                                                _item.Sird_deposit_bank_cd = depositBank;
                                                _item.Sird_deposit_branch = depositBranch;
                                                _item.Sird_inv_no = invoiceNo;
                                                _item.Sird_pay_tp = payMode;
                                                _item.Sird_settle_amt = Math.Round(Convert.ToDecimal(addedAmount), 4);
                                                RecieptItemList.Add(_item);
                                                Session["RecieptItemList"] = RecieptItemList;
                                                totPaidAmount = RecieptItemList.Sum(r => r.Sird_settle_amt);
                                                Session["totalPaidAmount"] = totPaidAmount;
                                                return Json(new { success = true, login = true, totPaid = totPaidAmount.ToString() }, JsonRequestBehavior.AllowGet);
                                            }
                                            else
                                            {
                                                return Json(new { success = false, login = true, msg = "Exceed the pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                            }
                                        }
                                        else
                                        {
                                            return Json(new { success = false, login = true, msg = "Cannot add duplicate payment.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                }
                                else {
                                    bool has = RecieptItemList.Any(list => list.Sird_pay_tp == payMode && list.Sird_inv_no == invoiceNo);
                                    if (!has)
                                    {
                                        if (currentBalanceAmount >= currnetPayAmount)
                                        {
                                            _item.Sird_deposit_bank_cd = depositBank;
                                            _item.Sird_deposit_branch = depositBranch;
                                            _item.Sird_inv_no = invoiceNo;
                                            _item.Sird_pay_tp = payMode;
                                            _item.Sird_settle_amt = Math.Round(Convert.ToDecimal(addedAmount), 4);
                                            RecieptItemList.Add(_item);
                                            Session["RecieptItemList"] = RecieptItemList;
                                            totPaidAmount = RecieptItemList.Sum(r => r.Sird_settle_amt);
                                            Session["totalPaidAmount"] = totPaidAmount;
                                            return Json(new { success = true, login = true, totPaid = totPaidAmount.ToString() }, JsonRequestBehavior.AllowGet); ;
                                        }
                                        else
                                        {
                                            return Json(new { success = false, login = true, msg = "Exceed the pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    else
                                    {
                                        return Json(new { success = false, login = true, msg = "Cannot add duplicate payment.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                        }
                        else {
                            if (!string.IsNullOrEmpty(customer))
                            {
                                BlackListCustomers _cus = CHNLSVC.Sales.GetBlackListCustomerDetails(customer, 1);
                                if (_cus.Hbl_cust_cd != null)
                                {
                                    return Json(new { success = false, login = true, msg = "This Customer is Blacklist Customer.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    bool has = RecieptItemList.Any(list => list.Sird_pay_tp == payMode && list.Sird_inv_no == invoiceNo);
                                    if (!has)
                                    {
                                        if (currentBalanceAmount >= currnetPayAmount)
                                        {
                                            _item.Sird_deposit_bank_cd = depositBank;
                                            _item.Sird_deposit_branch = depositBranch;
                                            _item.Sird_inv_no = invoiceNo;
                                            _item.Sird_pay_tp = payMode;
                                            _item.Sird_settle_amt = Math.Round(Convert.ToDecimal(addedAmount), 4);
                                            RecieptItemList.Add(_item);
                                            Session["RecieptItemList"] = RecieptItemList;
                                            totPaidAmount = RecieptItemList.Sum(r => r.Sird_settle_amt);
                                            Session["totalPaidAmount"] = totPaidAmount;
                                            return Json(new { success = true, login = true, totPaid = totPaidAmount.ToString() }, JsonRequestBehavior.AllowGet);
                                        }
                                        else
                                        {
                                            return Json(new { success = false, login = true, msg = "Exceed the pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    else
                                    {
                                        return Json(new { success = false, login = true, msg = "Cannot add duplicate payment.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                            else {
                                bool has = RecieptItemList.Any(list => list.Sird_pay_tp == payMode && list.Sird_inv_no == invoiceNo);
                                if (!has)
                                {
                                    if (currentBalanceAmount >= currnetPayAmount)
                                    {
                                        _item.Sird_deposit_bank_cd = depositBank;
                                        _item.Sird_deposit_branch = depositBranch;
                                        _item.Sird_inv_no = invoiceNo;
                                        _item.Sird_pay_tp = payMode;
                                        _item.Sird_settle_amt = Math.Round(Convert.ToDecimal(addedAmount), 4);
                                        RecieptItemList.Add(_item);
                                        Session["RecieptItemList"] = RecieptItemList;
                                        totPaidAmount = RecieptItemList.Sum(r => r.Sird_settle_amt);
                                        Session["totalPaidAmount"] = totPaidAmount;
                                        return Json(new { success = true, login = true, totPaid = totPaidAmount.ToString() }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        return Json(new { success = false, login = true, msg = "Exceed the pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                                else
                                {
                                    return Json(new { success = false, login = true, msg = "Cannot add duplicate payment.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Invalid price amount entered.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult updateChequePayment(string payMode, string totAmount, string addedAmount, string chequebank, string chequeBankCd, string chqBranchCd, string chequeNum, string chqDt, string depositBank, string depositBranch, string customer, string invoiceNo)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string subChannel = HttpContext.Session["UserSubChannl"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (totAmount == "")
                    {
                        return Json(new { success = false, login = true, msg = "Add invoice item to pay.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    TotalAmount = Convert.ToDecimal(totAmount);
                    if (TotalAmount <= 0)
                    {
                        return Json(new { success = false, login = true, msg = "Add invoice item to pay.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    chequebank = chequebank.Trim();
                    chqBranchCd = chqBranchCd.Trim();
                    chequeNum = chequeNum.Trim();
                    chqDt = chqDt.Trim();
                    depositBank = depositBank.Trim();
                    depositBranch = depositBranch.Trim();
                    customer = customer.Trim();
                    chequeBankCd = chequeBankCd.Trim();
                    string paidAmountString = Session["totalPaidAmount"] as string;
                    decimal totPaidAmount = Convert.ToDecimal(paidAmountString);
                   // Session["totAmount"] = totAmount;
                    decimal currentBalanceAmount=(TotalAmount-totPaidAmount);
                    decimal n;
                    bool isNumeric = decimal.TryParse(addedAmount, out n);
                    if(isNumeric){
                    decimal currnetPayAmount=Convert.ToDecimal(addedAmount);
                    List<RecieptItemTBS> RecieptItemList = new List<RecieptItemTBS>();
                    List<RecieptItemTBS> sesRecieptItemList = Session["RecieptItemList"] as List<RecieptItemTBS>;
                        if (sesRecieptItemList == null)
                        {
                            sesRecieptItemList = new List<RecieptItemTBS>();
                        }
                        else {
                            RecieptItemList = sesRecieptItemList;
                        }
                        RecieptItemTBS _item = new RecieptItemTBS();
                        MasterOutsideParty _bankAccounts = CHNLSVC.Sales.GetOutSidePartyDetailsById(chequebank.Trim());
                        if (_bankAccounts.Mbi_cd == null)
                        {
                            return Json(new { success = false, login = true, msg = "Invalid bank code.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        else {
                            if (string.IsNullOrEmpty(chqBranchCd))
                            {
                                return Json(new { success = false, login = true, msg = "Please enter cheque branch.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            else {
                                if (chequeNum.Length != 6)
                                {
                                    return Json(new { success = false, login = true, msg = "Please enter correct cheque number. [Cheque number should be 6 numbers.].", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                                else {
                                    if (string.IsNullOrEmpty(chqDt))
                                    {
                                        return Json(new { success = false, login = true, msg = "Invalid chque date.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                    }
                                    else {
                                        if (!CheckBank(chequebank))
                                        {
                                            return Json(new { success = false, login = true, msg = "Invalid Bank Code.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                        }
                                        else {
                                            if (chqBranchCd != "" && !CheckBankBranch(chequebank, chqBranchCd))
                                            {
                                                return Json(new { success = false, login = true, msg = "Cheque Bank and Branch not match.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                            }
                                            else {


                                                DataTable _dtDepBank = CHNLSVC.General.getSubChannelDet(company, subChannel);
                                                if (_dtDepBank.Rows.Count > 0)
                                                {
                                                    if (Convert.ToInt32(_dtDepBank.Rows[0]["MSSC_IS_BNK_MAN"]) == 1)
                                                    {
                                                        DataTable BankName = CHNLSVC.Sales.get_Dep_Bank_Name(company, userDefPro, payMode, depositBank);
                                                        if (BankName.Rows.Count == 0)
                                                        {
                                                            return Json(new { success = false, login = true, msg = "Invalid deposit bank account.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                        }
                                                        else
                                                        {
                                                            if (!string.IsNullOrEmpty(customer))
                                                            {

                                                                BlackListCustomers _cus = CHNLSVC.Sales.GetBlackListCustomerDetails(customer, 1);
                                                                if (_cus.Hbl_cust_cd != null)
                                                                {
                                                                    return Json(new { success = false, login = true, msg = "This Customer is Blacklist Customer.", type = "Info" }, JsonRequestBehavior.AllowGet);

                                                                }
                                                                else
                                                                {
                                                                    bool has = RecieptItemList.Any(list => list.Sird_ref_no == _bankAccounts.Mbi_cd + chqBranchCd + chequeNum && list.Sird_inv_no == invoiceNo);
                                                                    if (!has)
                                                                    {
                                                                        if (currentBalanceAmount >= currnetPayAmount)
                                                                        {

                                                                            _item.Sird_chq_dt = Convert.ToDateTime(chqDt);
                                                                            _item.Sird_chq_bank_cd = _bankAccounts.Mbi_cd;
                                                                            _item.Sird_chq_branch = chqBranchCd;
                                                                            _item.Sird_deposit_bank_cd = depositBank;
                                                                            _item.Sird_deposit_branch = depositBranch;
                                                                            _item.Sird_ref_no = _bankAccounts.Mbi_cd + chqBranchCd + chequeNum;
                                                                            _item.Sird_anal_5 = Convert.ToDateTime(chqDt);
                                                                            _item.Sird_inv_no = invoiceNo;
                                                                            _item.Sird_pay_tp = payMode;
                                                                            _item.Sird_settle_amt = Math.Round(Convert.ToDecimal(addedAmount), 4);
                                                                            RecieptItemList.Add(_item);
                                                                            Session["RecieptItemList"] = RecieptItemList;
                                                                            totPaidAmount = RecieptItemList.Sum(r => r.Sird_settle_amt);
                                                                            Session["totalPaidAmount"] = totPaidAmount;
                                                                            return Json(new { success = true, login = true, totPaid = totPaidAmount.ToString() }, JsonRequestBehavior.AllowGet);
                                                                        }
                                                                        else
                                                                        {
                                                                            return Json(new { success = false, login = true, msg = "Exceed the pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        return Json(new { success = false, login = true, msg = "Already paid using this cheque.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                bool has = RecieptItemList.Any(list => list.Sird_ref_no == _bankAccounts.Mbi_cd + chqBranchCd + chequeNum && list.Sird_inv_no == invoiceNo);
                                                                if (!has)
                                                                {
                                                                    if (currentBalanceAmount >= currnetPayAmount)
                                                                    {

                                                                        _item.Sird_chq_dt = Convert.ToDateTime(chqDt);
                                                                        _item.Sird_chq_bank_cd = _bankAccounts.Mbi_cd;
                                                                        _item.Sird_chq_branch = chqBranchCd;
                                                                        _item.Sird_deposit_bank_cd = depositBank;
                                                                        _item.Sird_deposit_branch = depositBranch;
                                                                        _item.Sird_ref_no = _bankAccounts.Mbi_cd + chqBranchCd + chequeNum;
                                                                        _item.Sird_anal_5 = Convert.ToDateTime(chqDt);
                                                                        _item.Sird_inv_no = invoiceNo;
                                                                        _item.Sird_pay_tp = payMode;
                                                                        _item.Sird_settle_amt = Math.Round(Convert.ToDecimal(addedAmount), 4);
                                                                        RecieptItemList.Add(_item);
                                                                        Session["RecieptItemList"] = RecieptItemList;
                                                                        totPaidAmount = RecieptItemList.Sum(r => r.Sird_settle_amt);
                                                                        Session["totalPaidAmount"] = totPaidAmount;
                                                                        return Json(new { success = true, login = true, totPaid = totPaidAmount.ToString() }, JsonRequestBehavior.AllowGet);
                                                                    }
                                                                    else
                                                                    {
                                                                        return Json(new { success = false, login = true, msg = "Exceed the pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    return Json(new { success = false, login = true, msg = "Already paid using this cheque.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                                }
                                                            }



                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!string.IsNullOrEmpty(customer))
                                                        {
                                                            BlackListCustomers _cus = CHNLSVC.Sales.GetBlackListCustomerDetails(customer, 1);
                                                            if (_cus.Hbl_cust_cd != null)
                                                            {
                                                                return Json(new { success = false, login = true, msg = "This Customer is Blacklist Customer.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                            }
                                                            else
                                                            {
                                                                bool has = RecieptItemList.Any(list => list.Sird_ref_no == _bankAccounts.Mbi_cd + chqBranchCd + chequeNum && list.Sird_inv_no == invoiceNo);
                                                                if (!has)
                                                                {
                                                                    if (currentBalanceAmount >= currnetPayAmount)
                                                                    {

                                                                        _item.Sird_chq_dt = Convert.ToDateTime(chqDt);
                                                                        _item.Sird_chq_bank_cd = _bankAccounts.Mbi_cd;
                                                                        _item.Sird_chq_branch = chqBranchCd;
                                                                        _item.Sird_deposit_bank_cd = depositBank;
                                                                        _item.Sird_deposit_branch = depositBranch;
                                                                        _item.Sird_ref_no = _bankAccounts.Mbi_cd + chqBranchCd + chequeNum;
                                                                        _item.Sird_anal_5 = Convert.ToDateTime(chqDt);
                                                                        _item.Sird_inv_no = invoiceNo;
                                                                        _item.Sird_pay_tp = payMode;
                                                                        _item.Sird_settle_amt = Math.Round(Convert.ToDecimal(addedAmount), 4);
                                                                        RecieptItemList.Add(_item);
                                                                        Session["RecieptItemList"] = RecieptItemList;
                                                                        totPaidAmount = RecieptItemList.Sum(r => r.Sird_settle_amt);
                                                                        Session["totalPaidAmount"] = totPaidAmount;
                                                                        return Json(new { success = true, login = true, totPaid = totPaidAmount.ToString() }, JsonRequestBehavior.AllowGet);
                                                                    }
                                                                    else
                                                                    {
                                                                        return Json(new { success = false, login = true, msg = "Exceed the pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    return Json(new { success = false, login = true, msg = "Already paid using this cheque.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            bool has = RecieptItemList.Any(list => list.Sird_ref_no == _bankAccounts.Mbi_cd + chqBranchCd + chequeNum && list.Sird_inv_no == invoiceNo);
                                                            if (!has)
                                                            {
                                                                if (currentBalanceAmount >= currnetPayAmount)
                                                                {
                                                                    _item.Sird_chq_dt = Convert.ToDateTime(chqDt);
                                                                    _item.Sird_chq_bank_cd = _bankAccounts.Mbi_cd;
                                                                    _item.Sird_chq_branch = chqBranchCd;
                                                                    _item.Sird_deposit_bank_cd = depositBank;
                                                                    _item.Sird_deposit_branch = depositBranch;
                                                                    _item.Sird_ref_no = _bankAccounts.Mbi_cd + chqBranchCd + chequeNum;
                                                                    _item.Sird_anal_5 = Convert.ToDateTime(chqDt);
                                                                    _item.Sird_inv_no = invoiceNo;
                                                                    _item.Sird_pay_tp = payMode;
                                                                    _item.Sird_settle_amt = Math.Round(Convert.ToDecimal(addedAmount), 4);
                                                                    RecieptItemList.Add(_item);
                                                                    Session["RecieptItemList"] = RecieptItemList;
                                                                    totPaidAmount = RecieptItemList.Sum(r => r.Sird_settle_amt);
                                                                    Session["totalPaidAmount"] = totPaidAmount;
                                                                    return Json(new { success = true, login = true, totPaid = totPaidAmount.ToString() }, JsonRequestBehavior.AllowGet);
                                                                }
                                                                else
                                                                {
                                                                    return Json(new { success = false, login = true, msg = "Exceed the pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                return Json(new { success = false, login = true, msg = "Already paid using this cheque.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                            }
                                                        }
                                                    }
                                                }
                                                else {
                                                    if (!string.IsNullOrEmpty(customer))
                                                    {
                                                        BlackListCustomers _cus = CHNLSVC.Sales.GetBlackListCustomerDetails(customer, 1);
                                                        if (_cus.Hbl_cust_cd != null)
                                                        {
                                                            return Json(new { success = false, login = true, msg = "This Customer is Blacklist Customer.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                        }
                                                        else
                                                        {
                                                            bool has = RecieptItemList.Any(list => list.Sird_ref_no == _bankAccounts.Mbi_cd + chqBranchCd + chequeNum && list.Sird_inv_no == invoiceNo);
                                                            if (!has)
                                                            {
                                                                if (currentBalanceAmount >= currnetPayAmount)
                                                                {

                                                                    _item.Sird_chq_dt = Convert.ToDateTime(chqDt);
                                                                    _item.Sird_chq_bank_cd = _bankAccounts.Mbi_cd;
                                                                    _item.Sird_chq_branch = chqBranchCd;
                                                                    _item.Sird_deposit_bank_cd = depositBank;
                                                                    _item.Sird_deposit_branch = depositBranch;
                                                                    _item.Sird_ref_no = _bankAccounts.Mbi_cd + chqBranchCd + chequeNum;
                                                                    _item.Sird_anal_5 = Convert.ToDateTime(chqDt);
                                                                    _item.Sird_inv_no = invoiceNo;
                                                                    _item.Sird_pay_tp = payMode;
                                                                    _item.Sird_settle_amt = Math.Round(Convert.ToDecimal(addedAmount), 4);
                                                                    RecieptItemList.Add(_item);
                                                                    Session["RecieptItemList"] = RecieptItemList;
                                                                    totPaidAmount = RecieptItemList.Sum(r => r.Sird_settle_amt);
                                                                    Session["totalPaidAmount"] = totPaidAmount;
                                                                    return Json(new { success = true, login = true, totPaid = totPaidAmount.ToString() }, JsonRequestBehavior.AllowGet);
                                                                }
                                                                else
                                                                {
                                                                    return Json(new { success = false, login = true, msg = "Exceed the pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                return Json(new { success = false, login = true, msg = "Already paid using this cheque.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        bool has = RecieptItemList.Any(list => list.Sird_ref_no == _bankAccounts.Mbi_cd + chqBranchCd + chequeNum && list.Sird_inv_no == invoiceNo);
                                                        if (!has)
                                                        {
                                                            if (currentBalanceAmount >= currnetPayAmount)
                                                            {

                                                                _item.Sird_chq_dt = Convert.ToDateTime(chqDt);
                                                                _item.Sird_chq_bank_cd = _bankAccounts.Mbi_cd;
                                                                _item.Sird_chq_branch = chqBranchCd;
                                                                _item.Sird_deposit_bank_cd = depositBank;
                                                                _item.Sird_deposit_branch = depositBranch;
                                                                _item.Sird_ref_no = _bankAccounts.Mbi_cd + chqBranchCd + chequeNum;
                                                                _item.Sird_anal_5 = Convert.ToDateTime(chqDt);
                                                                _item.Sird_inv_no = invoiceNo;
                                                                _item.Sird_pay_tp = payMode;
                                                                _item.Sird_settle_amt = Math.Round(Convert.ToDecimal(addedAmount), 4);
                                                                RecieptItemList.Add(_item);
                                                                Session["RecieptItemList"] = RecieptItemList;
                                                                totPaidAmount = RecieptItemList.Sum(r => r.Sird_settle_amt);
                                                                Session["totalPaidAmount"] = totPaidAmount;
                                                                return Json(new { success = true, login = true, totPaid = totPaidAmount.ToString() }, JsonRequestBehavior.AllowGet);
                                                            }
                                                            else
                                                            {
                                                                return Json(new { success = false, login = true, msg = "Exceed the pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            return Json(new { success = false, login = true, msg = "Already paid using this cheque.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                        }
                                                    }
                                                }
                                            }
                                            
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else{
                        return Json(new { success = false, login = true, msg = "Invalid price amount entered.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = Resource.ServerError,type="Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult updateLOREPayment(string payMode, string totAmount, string addedAmount, string loreCrdNo, string depositBank, string depositBranch, string customer, string invoiceNo)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string subChannel = HttpContext.Session["UserSubChannl"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (totAmount == "")
                    {
                        return Json(new { success = false, login = true, msg = "Add invoice item to pay.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    payMode = payMode.Trim();
                    loreCrdNo = loreCrdNo.Trim();
                    invoiceNo = invoiceNo.Trim();
                    depositBank = depositBank.Trim();
                    depositBranch = depositBranch.Trim();
                    addedAmount = addedAmount.Trim();
                    TotalAmount = Convert.ToDecimal(totAmount);
                    if (TotalAmount <= 0)
                    {
                        return Json(new { success = false, login = true, msg = "Add invoice item to pay.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    string paidAmountString = Session["totalPaidAmount"] as string;
                    decimal totPaidAmount = Convert.ToDecimal(paidAmountString);

                   
                    decimal n;
                    bool isNumeric = decimal.TryParse(addedAmount, out n);
                    if (isNumeric)
                    {
                        if (!string.IsNullOrEmpty(loreCrdNo))
                        {
                            LoyaltyMemeber loyalCus = CHNLSVC.Sales.getLoyaltyDetails(customer, loreCrdNo);
                            if (loyalCus.Salcm_no != null)
                            {
                                LoyaltyPointRedeemDefinition _definition = CHNLSVC.Sales.GetLoyaltyRedeemDefinition("COM", company, DateTime.Now, loyalCus.Salcm_loty_tp);
                                if (_definition.Salre_red_pt > 0)
                                {
                                    decimal currentBalanceAmount = (TotalAmount - totPaidAmount);
                                    if (currentBalanceAmount - (Convert.ToDecimal(addedAmount) * Convert.ToDecimal(_definition.Salre_red_pt)) < 0)
                                    {
                                        return Json(new { success = false, login = true, msg = "Please select the valid pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                    }
                                    else {
                                        if (Convert.ToDecimal(addedAmount) > Convert.ToDecimal(loyalCus.Salcm_bal_pt))
                                        {
                                            return Json(new { success = false, login = true, msg = "You can redeem only " + loyalCus.Salcm_bal_pt + " points.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                        }
                                        else {
                                            decimal currnetPayAmount = Convert.ToDecimal(addedAmount) * Convert.ToDecimal(_definition.Salre_red_pt);
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
                                            RecieptItemTBS _item = new RecieptItemTBS();
                                             BlackListCustomers _cus = CHNLSVC.Sales.GetBlackListCustomerDetails(customer, 1);
                                             if (_cus.Hbl_cust_cd != null)
                                             {
                                                 return Json(new { success = false, login = true, msg = "This Customer is Blacklist Customer.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                             }
                                             else
                                             {
                                                 DataTable _dtDepBank = CHNLSVC.General.getSubChannelDet(company, subChannel);
                                                 if (_dtDepBank.Rows.Count > 0)
                                                 {
                                                     if (Convert.ToInt32(_dtDepBank.Rows[0]["MSSC_IS_BNK_MAN"]) == 1)
                                                     {
                                                         DataTable BankName = CHNLSVC.Sales.get_Dep_Bank_Name(company, userDefPro, payMode, depositBank);
                                                         if (BankName.Rows.Count == 0)
                                                         {
                                                             return Json(new { success = false, login = true, msg = "Invalid deposit bank account.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                         }
                                                         else
                                                         {
                                                             bool has = RecieptItemList.Any(list => list.Sird_pay_tp == payMode && list.Sird_inv_no == invoiceNo);
                                                             if (!has)
                                                             {
                                                                 if (currentBalanceAmount >= currnetPayAmount)
                                                                 {
                                                                     _item.Sird_ref_no = loreCrdNo;
                                                                     _item.Sird_deposit_bank_cd = depositBank;
                                                                     _item.Sird_deposit_branch = depositBranch;
                                                                     _item.Sird_anal_4 = Convert.ToDecimal(addedAmount);
                                                                     _item.Sird_inv_no = invoiceNo;
                                                                     _item.Sird_pay_tp = payMode;
                                                                     _item.Sird_settle_amt = Math.Round((Convert.ToDecimal(addedAmount) * Convert.ToDecimal(_definition.Salre_red_pt)), 4);
                                                                     RecieptItemList.Add(_item);
                                                                     Session["RecieptItemList"] = RecieptItemList;
                                                                     totPaidAmount = RecieptItemList.Sum(r => r.Sird_settle_amt);
                                                                     Session["totalPaidAmount"] = totPaidAmount;
                                                                     return Json(new { success = true, login = true, totPaid = totPaidAmount.ToString() }, JsonRequestBehavior.AllowGet);
                                                                 }
                                                                 else
                                                                 {
                                                                     return Json(new { success = false, login = true, msg = "Exceed the pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                                 }
                                                             }
                                                             else
                                                             {
                                                                 return Json(new { success = false, login = true, msg = "Cannot duplicate payment method.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                             }
                                                         }
                                                     }
                                                     else {
                                                         bool has = RecieptItemList.Any(list => list.Sird_pay_tp == payMode && list.Sird_inv_no == invoiceNo);
                                                         if (!has)
                                                         {
                                                             if (currentBalanceAmount >= currnetPayAmount)
                                                             {
                                                                 _item.Sird_ref_no = loreCrdNo;
                                                                 _item.Sird_deposit_bank_cd = depositBank;
                                                                 _item.Sird_deposit_branch = depositBranch;
                                                                 _item.Sird_anal_4 = Convert.ToDecimal(addedAmount);
                                                                 _item.Sird_inv_no = invoiceNo;
                                                                 _item.Sird_pay_tp = payMode;
                                                                 _item.Sird_settle_amt = Math.Round((Convert.ToDecimal(addedAmount) * Convert.ToDecimal(_definition.Salre_red_pt)), 4);
                                                                 RecieptItemList.Add(_item);
                                                                 Session["RecieptItemList"] = RecieptItemList;
                                                                 totPaidAmount = RecieptItemList.Sum(r => r.Sird_settle_amt);
                                                                 Session["totalPaidAmount"] = totPaidAmount;
                                                                 return Json(new { success = true, login = true, totPaid = totPaidAmount.ToString() }, JsonRequestBehavior.AllowGet);
                                                             }
                                                             else
                                                             {
                                                                 return Json(new { success = false, login = true, msg = "Exceed the pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                             }
                                                         }
                                                         else
                                                         {
                                                             return Json(new { success = false, login = true, msg = "Cannot duplicate payment method.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                         }
                                                     }
                                                 }
                                                 else {
                                                     bool has = RecieptItemList.Any(list => list.Sird_pay_tp == payMode && list.Sird_inv_no == invoiceNo);
                                                     if (!has)
                                                     {
                                                         if (currentBalanceAmount >= currnetPayAmount)
                                                         {
                                                             _item.Sird_ref_no = loreCrdNo;
                                                             _item.Sird_deposit_bank_cd = depositBank;
                                                             _item.Sird_deposit_branch = depositBranch;
                                                             _item.Sird_anal_4 = Convert.ToDecimal(addedAmount);
                                                             _item.Sird_inv_no = invoiceNo;
                                                             _item.Sird_pay_tp = payMode;
                                                             _item.Sird_settle_amt = Math.Round((Convert.ToDecimal(addedAmount) * Convert.ToDecimal(_definition.Salre_red_pt)), 4);
                                                             RecieptItemList.Add(_item);
                                                             Session["RecieptItemList"] = RecieptItemList;
                                                             totPaidAmount = RecieptItemList.Sum(r => r.Sird_settle_amt);
                                                             Session["totalPaidAmount"] = totPaidAmount;
                                                             return Json(new { success = true, login = true, totPaid = totPaidAmount.ToString() }, JsonRequestBehavior.AllowGet);
                                                         }
                                                         else
                                                         {
                                                             return Json(new { success = false, login = true, msg = "Exceed the pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                         }
                                                     }
                                                     else
                                                     {
                                                         return Json(new { success = false, login = true, msg = "Cannot duplicate payment method.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                     }
                                                 }
                                             }
                                        }
                                    }
                                   
                                }
                                else {
                                    return Json(new { success = false, login = true, msg = "No redeem definition found.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                               
                            }
                            else {
                                return Json(new { success = false, login = true, msg = "Invalid loyalty number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }else{
                            return Json(new { success = false, login = true, msg = "Invalid loyalty number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else {
                        return Json(new { success = false, login = true, msg = "Invalid price amount entered.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
        public string GVLOC;
        public DateTime GVISSUEDATE = DateTime.MinValue;
        public string GVCOM;
        public JsonResult updateGVOPayment(string payMode, string totAmount, string addedAmount,string voucherNo,string voucherBook,string invType, string depositBank, string depositBranch, string customer, string invoiceNo)
        { 
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string subChannel = HttpContext.Session["UserSubChannl"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (totAmount == "")
                    {
                        return Json(new { success = false, login = true, msg = "Add invoice item to pay.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    payMode = payMode.Trim();
                    voucherNo = voucherNo.Trim();
                    voucherBook = voucherBook.Trim();
                    invoiceNo = invoiceNo.Trim();
                    invType = invType.Trim();
                    depositBank = depositBank.Trim();
                    depositBranch = depositBranch.Trim();
                    addedAmount = addedAmount.Trim();
                    TotalAmount = Convert.ToDecimal(totAmount);
                    if (TotalAmount <= 0)
                    {
                        return Json(new { success = false, login = true, msg = "Add invoice item to pay.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    string paidAmountString = Session["totalPaidAmount"] as string;
                    decimal totPaidAmount = Convert.ToDecimal(paidAmountString);
                    GiftVoucherPages giftVouPage;

                     decimal n;
                    bool isNumeric = decimal.TryParse(addedAmount, out n);
                    if (isNumeric)
                    {
                        if (voucherNo == "")
                        {
                            return Json(new { success = false, login = true, msg = "Gift voucher number can not be empty.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        else {
                            int val;
                            if (!int.TryParse(voucherNo, out val))
                            {
                                return Json(new { success = false, login = true, msg = "Gift voucher number has to be number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            else {
                                if (string.IsNullOrEmpty(voucherBook))
                                {
                                    return Json(new { success = false, login = true, msg = "Gift voucher book not found.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                                else {
                                    giftVouPage = CHNLSVC.Sales.getGiftVoucherPage(voucherNo, voucherBook, company);
                                    if (giftVouPage.Gvp_book.ToString() != "")
                                    {
                                        if (string.IsNullOrEmpty(giftVouPage.Gvp_gv_prefix))
                                        {
                                            return Json(new { success = false, login = true, msg = "Gift voucher pefix not found.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                           
                                        }
                                        if (string.IsNullOrEmpty(giftVouPage.Gvp_gv_cd))
                                        {
                                            return Json(new { success = false, login = true, msg = "Gift voucher code not found.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    else {
                                        return Json(new { success = false, login = true, msg = "Invalid giftvoucher book.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                    }



                                }
                                
                            }
                        }

                         List<GiftVoucherPages> _gift = new List<GiftVoucherPages>();
                         List<GiftVoucherPages> _Allgv = CHNLSVC.Inventory.GetGiftVoucherPages(null, Convert.ToInt32(voucherNo));
                    //List<GiftVoucherPages> _gift = _base.CHNLSVC.Inventory.GetGiftVoucherPages(BaseCls.GlbUserComCode, Convert.ToInt32(txtGiftVoucher.Text));

                    if (_Allgv != null)
                    {
                        foreach (GiftVoucherPages _tmp in _Allgv)
                        {
                            DataTable _allCom = CHNLSVC.Inventory.GetGVAlwCom(company, _tmp.Gvp_gv_cd, 1);
                            if (_allCom.Rows.Count > 0)
                            {
                                _gift.Add(_tmp);
                            }

                        }
                    }

                    if (_gift != null && _gift.Count > 0)
                    {
                        if (_gift.Count == 1)
                        {
                            if (Convert.ToDecimal(addedAmount) > _gift[0].Gvp_bal_amt)
                            {
                                return Json(new { success = false, login = true, msg = "Gift voucher amount to be greater than pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            if (_gift[0].Gvp_stus != "A")
                            {
                                return Json(new { success = false, login = true, msg = "Gift voucher is not Active.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            if (_gift[0].Gvp_gv_tp != "VALUE")
                            {
                                return Json(new { success = false, login = true, msg = "Gift voucher type is invalid.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }

                            DateTime Date = DateTime.Now.Date;
                            if (!(_gift[0].Gvp_valid_from <= DateTime.Now && _gift[0].Gvp_valid_to >= DateTime.Now))
                            {
                                return Json(new { success = false, login = true, msg = "Gift voucher From and To dates not in range\nFrom Date - " + _gift[0].Gvp_valid_from.ToString("dd/MMM/yyyy") + "\nTo Date - " + _gift[0].Gvp_valid_to.ToString("dd/MMM/yyyy"), type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            Boolean _isGVCode = false;
                            Boolean _isGV = false;
                            List<PaymentType> _paymentTypeRefGV = CHNLSVC.Sales.GetPossiblePaymentTypes_new(company, subChannel, userDefPro, invType, DateTime.Now.Date,1);
                            if (_paymentTypeRefGV != null)
                            {
                                List<PaymentType> _paymentTypeRef1GV = _paymentTypeRefGV.FindAll(y => !string.IsNullOrEmpty(y.Stp_vou_cd) && y.Stp_pay_tp == "GVO");
                                if (_paymentTypeRef1GV != null && _paymentTypeRef1GV.Count > 0)
                                {
                                    _isGV = true;
                                    if (_paymentTypeRef1GV.FindAll(x => x.Stp_vou_cd == _gift[0].Gvp_gv_cd).Count > 0)
                                    {
                                        PaymentType pt = _paymentTypeRef1GV.Where(x => x.Stp_vou_cd == _gift[0].Gvp_gv_cd).OrderByDescending(x => x.Stp_seq).First();
                                        if (_gift[0].Gvp_gv_cd == pt.Stp_vou_cd)
                                        {
                                            if (!string.IsNullOrEmpty(pt.Stp_sch_cd))
                                            {
                                                _isGVCode = true;
                                            }
                                            else
                                            {
                                                _isGVCode = true;
                                            }

                                        }
                                    }

                                }
                                if (_isGVCode == false && _isGV == true)
                                {
                                    return Json(new { success = false, login = true, msg = "Selected voucher code and define voucher code not matching.", type = "Info" }, JsonRequestBehavior.AllowGet);

                                }

                            }

                             MasterItem _itemdetail = new MasterItem();
                            _itemdetail = CHNLSVC.Inventory.GetItem(company, _gift[0].Gvp_gv_cd);
                            if (_itemdetail != null)
                            {
                                if (_itemdetail.MI_CHK_CUST == 1)
                                {
                                    if (customer != _gift[0].Gvp_cus_cd)
                                    {
                                        return Json(new { success = false, login = true, msg = "This Gift voucher is not allocated to selected customer.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                    

                                    }
                                }
                            }
                            GVLOC = _gift[0].Gvp_pc;
                            GVISSUEDATE = _gift[0].Gvp_issue_dt;
                            GVCOM = _gift[0].Gvp_com;

                        }
                        else {

                            if (voucherBook != "")
                            {
                                GiftVoucherPages _giftPage = CHNLSVC.Inventory.GetGiftVoucherPage(null, "%", giftVouPage.Gvp_gv_cd, Convert.ToInt32(giftVouPage.Gvp_book), Convert.ToInt32(voucherNo), giftVouPage.Gvp_gv_prefix);
                     

                                if (_giftPage == null)
                                {
                                    return Json(new { success = false, login = true, msg = "Please select gift voucher page from grid.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                    
                                }
                                if (Convert.ToDecimal(addedAmount) > _giftPage.Gvp_bal_amt)
                                {
                                    return Json(new { success = false, login = true, msg = "Gift voucher amount to be greater than pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                    
                                }
                                if (_giftPage.Gvp_stus != "A")
                                {
                                    return Json(new { success = false, login = true, msg = "Gift voucher is not Active.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                  
                                }
                                if (_giftPage.Gvp_gv_tp != "VALUE")
                                {
                                    return Json(new { success = false, login = true, msg = "Gift voucher type is invalid.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                    
                                }
                                if (!(_giftPage.Gvp_valid_from <= DateTime.Now && _giftPage.Gvp_valid_to >= DateTime.Now))
                                {
                                    return Json(new { success = false, login = true, msg = "Gift voucher From and To dates not in range\nFrom Date - " + _giftPage.Gvp_valid_from.ToString("dd/MMM/yyyy") + "\nTo Date - " + _giftPage.Gvp_valid_to.ToString("dd/MMM/yyyy"), type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                                
                                Boolean _isGVCode = false;
                                Boolean _isGV = false;
                                List<PaymentType> _paymentTypeRefGV = CHNLSVC.Sales.GetPossiblePaymentTypes_new(company, subChannel, userDefPro, invType, DateTime.Now.Date,1);
                                if (_paymentTypeRefGV != null)
                                {
                                    List<PaymentType> _paymentTypeRef1GV = _paymentTypeRefGV.FindAll(y => !string.IsNullOrEmpty(y.Stp_vou_cd) && y.Stp_pay_tp == "GVO");
                                    if (_paymentTypeRef1GV != null && _paymentTypeRef1GV.Count > 0)
                                    {
                                        _isGV = true;
                                        if (_paymentTypeRef1GV.FindAll(x => x.Stp_vou_cd == _giftPage.Gvp_gv_cd).Count > 0)
                                        {
                                            PaymentType pt = _paymentTypeRef1GV.Where(x => x.Stp_vou_cd == _giftPage.Gvp_gv_cd).OrderByDescending(x => x.Stp_seq).First();


                                            if (_giftPage.Gvp_gv_cd == pt.Stp_vou_cd)
                                            {
                                                if (!string.IsNullOrEmpty(pt.Stp_sch_cd))
                                                {
                                                        _isGVCode = true;
                                                    
                                                }
                                                else
                                                {
                                                    _isGVCode = true;
                                                }

                                            }
                                        }


                                    }
                                    if (_isGVCode == false && _isGV == true)
                                    {
                                        return Json(new { success = false, login = true, msg = "Selected voucher code and define voucher code not matching.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                    }
                                }

                                MasterItem _itemdetail = new MasterItem();
                                _itemdetail = CHNLSVC.Inventory.GetItem(company, _giftPage.Gvp_gv_cd);
                                if (_itemdetail != null)
                                {
                                    if (_itemdetail.MI_CHK_CUST == 1)
                                    {
                                        if (customer != _giftPage.Gvp_cus_cd)
                                        {
                                            return Json(new { success = false, login = true, msg = "This Gift voucher is not allocated to selected customer.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                }

                                GVLOC = _giftPage.Gvp_pc;
                                GVISSUEDATE = _giftPage.Gvp_issue_dt;
                                GVCOM = _giftPage.Gvp_com;
                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = "Please select gift voucher page from grid.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }

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
                        RecieptItemTBS _item = new RecieptItemTBS();
                        decimal currentBalanceAmount = (TotalAmount - totPaidAmount);
                        decimal currnetPayAmount = Convert.ToDecimal(addedAmount);
                        DataTable _dtDepBank = CHNLSVC.General.getSubChannelDet(company, subChannel);
                        if (_dtDepBank.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(_dtDepBank.Rows[0]["MSSC_IS_BNK_MAN"]) == 1)
                            {
                                DataTable BankName = CHNLSVC.Sales.get_Dep_Bank_Name(company, userDefPro, payMode, depositBank);
                                if (BankName.Rows.Count == 0 && true == false)
                                {
                                    return Json(new { success = false, login = true, msg = "Invalid deposit bank account.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(customer))
                                    {
                                        BlackListCustomers _cus = CHNLSVC.Sales.GetBlackListCustomerDetails(customer, 1);
                                        if (_cus.Hbl_cust_cd != null)
                                        {
                                            return Json(new { success = false, login = true, msg = "This Customer is Blacklist Customer.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                }
                            }
                        }

                        bool has = RecieptItemList.Any(list => list.Sird_sim_ser == giftVouPage.Gvp_book.ToString() && list.Sird_inv_no == invoiceNo);
                        if (has)
                        {
                            return Json(new { success = false, login = true, msg = "Already used this gift voucher.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }


                        _item.Sird_ref_no = voucherNo;
                        _item.Sird_sim_ser = giftVouPage.Gvp_book.ToString();
                        _item.Sird_anal_2 = giftVouPage.Gvp_gv_prefix;
                        _item.Sird_deposit_bank_cd = depositBank;
                        _item.Sird_deposit_branch = depositBranch;
                        _item.Sird_cc_tp = giftVouPage.Gvp_gv_cd;
                        _item.Sird_gv_issue_loc = GVLOC;
                        _item.Sird_gv_issue_dt = GVISSUEDATE;
                        _item.Sird_anal_1 = GVCOM;
                        _item.Sird_inv_no = invoiceNo;
                        _item.Sird_pay_tp = payMode;
                        _item.Sird_settle_amt = Math.Round((Convert.ToDecimal(addedAmount)), 4);
                        RecieptItemList.Add(_item);
                        Session["RecieptItemList"] = RecieptItemList;
                        totPaidAmount = RecieptItemList.Sum(r => r.Sird_settle_amt);
                        Session["totalPaidAmount"] = totPaidAmount;
                        return Json(new { success = true, login = true, totPaid = totPaidAmount.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else {
                        return Json(new { success = false, login = true, msg = "Invalid price amount entered.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult updateADVANPayment(string payMode, string totAmount, string addedAmount, string refNo, string refAmount, string depositBank, string depositBranch, string customer, string invoiceNo)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string subChannel = HttpContext.Session["UserSubChannl"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (totAmount == "")
                    {
                        return Json(new { success = false, login = true, msg = "Add invoice item to pay.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    payMode = payMode.Trim();
                    refNo = refNo.Trim();
                    refAmount = refAmount.Trim();
                    invoiceNo = invoiceNo.Trim();
                    depositBank = depositBank.Trim();
                    depositBranch = depositBranch.Trim();
                    addedAmount = addedAmount.Trim();
                    TotalAmount = Convert.ToDecimal(totAmount);
                    if (TotalAmount <= 0)
                    {
                        return Json(new { success = false, login = true, msg = "Add invoice item to pay.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    string paidAmountString = Session["totalPaidAmount"] as string;
                    decimal totPaidAmount = Convert.ToDecimal(paidAmountString);
                     decimal n;
                    bool isNumeric = decimal.TryParse(addedAmount, out n);
                    if (isNumeric)
                    {
                        if (!string.IsNullOrEmpty(refNo))
                        {
                            if (!string.IsNullOrEmpty(refAmount))
                            {
                                string amount = CHNLSVC.Tours.getAdvanceRefAmount(customer, company, refNo);
                                if (amount != null)
                                {
                                    refAmount = amount;
                                    DataTable _dt = CHNLSVC.Tours.GetReceipt(refNo);
                                    if (_dt != null && _dt.Rows.Count > 0)
                                    {
                                        bool checkCustomer = false;
                                        if (_dt.Rows[0]["sir_debtor_cd"].ToString() != "CASH")
                                        {
                                            if (customer != "CASH")
                                            {
                                                if (_dt.Rows[0]["sir_debtor_cd"].ToString() != customer)
                                                {
                                                    return Json(new { success = false, login = true, msg = "Advance receipt customer mismatch.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                }
                                                else
                                                {
                                                    checkCustomer = true;
                                                }
                                            }
                                            else
                                            {
                                                checkCustomer = true;
                                            }
                                        }
                                        else
                                        {
                                            checkCustomer = true;
                                        }
                                        if (checkCustomer == true)
                                        {
                                            bool valid = false;
                                            DateTime dte;
                                            if (_dt.Rows[0]["SIR_VALID_TO"].ToString() == "")
                                            {
                                                valid = true;
                                            }
                                            else
                                            {
                                                dte = Convert.ToDateTime(_dt.Rows[0]["SIR_VALID_TO"]);
                                                if (dte < DateTime.Now)
                                                {
                                                    return Json(new { success = false, login = true, msg = "Advance receipt is expire. Pls. contact accounts dept.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                }
                                                else
                                                {
                                                    valid = true;
                                                }
                                            }
                                            if (valid == true)
                                            {
                                                decimal currnetPayAmount = Convert.ToDecimal(addedAmount);
                                                if (currnetPayAmount > Convert.ToDecimal(refAmount))
                                                {
                                                    return Json(new { success = false, login = true, msg = "Amount larger than advance amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                }
                                                else
                                                {
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
                                                    RecieptItemTBS _item = new RecieptItemTBS();
                                                    BlackListCustomers _cus = CHNLSVC.Sales.GetBlackListCustomerDetails(customer, 1);
                                                    decimal currentBalanceAmount = (TotalAmount - totPaidAmount);
                                                    if (_cus.Hbl_cust_cd != null)
                                                    {
                                                        return Json(new { success = false, login = true, msg = "This Customer is Blacklist Customer.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                    }
                                                    else
                                                    {

                                                        DataTable _dtDepBank = CHNLSVC.General.getSubChannelDet(company, subChannel);
                                                        if (_dtDepBank.Rows.Count > 0)
                                                        {
                                                            if (Convert.ToInt32(_dtDepBank.Rows[0]["MSSC_IS_BNK_MAN"]) == 1)
                                                            {
                                                                DataTable BankName = CHNLSVC.Sales.get_Dep_Bank_Name(company, userDefPro, payMode, depositBank);
                                                                if (BankName.Rows.Count == 0)
                                                                {
                                                                    return Json(new { success = false, login = true, msg = "Invalid deposit bank account.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                                }
                                                                else
                                                                {

                                                                    bool has = RecieptItemList.Any(list => list.Sird_ref_no == refNo && list.Sird_inv_no == invoiceNo);
                                                                    if (!has)
                                                                    {
                                                                        if (currentBalanceAmount >= currnetPayAmount)
                                                                        {
                                                                            _item.Sird_ref_no = refNo;
                                                                            _item.Sird_deposit_bank_cd = depositBank;
                                                                            _item.Sird_deposit_branch = depositBranch;
                                                                            _item.Sird_inv_no = invoiceNo;
                                                                            _item.Sird_pay_tp = payMode;
                                                                            _item.Sird_settle_amt = Math.Round((Convert.ToDecimal(addedAmount)), 4);
                                                                            RecieptItemList.Add(_item);
                                                                            Session["RecieptItemList"] = RecieptItemList;
                                                                            totPaidAmount = RecieptItemList.Sum(r => r.Sird_settle_amt);
                                                                            Session["totalPaidAmount"] = totPaidAmount;
                                                                            return Json(new { success = true, login = true, totPaid = totPaidAmount.ToString() }, JsonRequestBehavior.AllowGet);
                                                                        }
                                                                        else
                                                                        {
                                                                            return Json(new { success = false, login = true, msg = "Exceed the pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        return Json(new { success = false, login = true, msg = "Already used this credit note.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                bool has = RecieptItemList.Any(list => list.Sird_ref_no == refNo && list.Sird_inv_no == invoiceNo);
                                                                if (!has)
                                                                {
                                                                    if (currentBalanceAmount >= currnetPayAmount)
                                                                    {
                                                                        _item.Sird_ref_no = refNo;
                                                                        _item.Sird_deposit_bank_cd = depositBank;
                                                                        _item.Sird_deposit_branch = depositBranch;
                                                                        _item.Sird_inv_no = invoiceNo;
                                                                        _item.Sird_pay_tp = payMode;
                                                                        _item.Sird_settle_amt = Math.Round((Convert.ToDecimal(addedAmount)), 4);
                                                                        RecieptItemList.Add(_item);
                                                                        Session["RecieptItemList"] = RecieptItemList;
                                                                        totPaidAmount = RecieptItemList.Sum(r => r.Sird_settle_amt);
                                                                        Session["totalPaidAmount"] = totPaidAmount;
                                                                        return Json(new { success = true, login = true, totPaid = totPaidAmount.ToString() }, JsonRequestBehavior.AllowGet);
                                                                    }
                                                                    else
                                                                    {
                                                                        return Json(new { success = false, login = true, msg = "Exceed the pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    return Json(new { success = false, login = true, msg = "Already used this credit note.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                                }

                                                            }
                                                        }
                                                        else
                                                        {
                                                            bool has = RecieptItemList.Any(list => list.Sird_ref_no == refNo && list.Sird_inv_no == invoiceNo);
                                                            if (!has)
                                                            {
                                                                if (currentBalanceAmount >= currnetPayAmount)
                                                                {
                                                                    _item.Sird_ref_no = refNo;
                                                                    _item.Sird_deposit_bank_cd = depositBank;
                                                                    _item.Sird_deposit_branch = depositBranch;
                                                                    _item.Sird_inv_no = invoiceNo;
                                                                    _item.Sird_pay_tp = payMode;
                                                                    _item.Sird_settle_amt = Math.Round((Convert.ToDecimal(addedAmount)), 4);
                                                                    RecieptItemList.Add(_item);
                                                                    Session["RecieptItemList"] = RecieptItemList;
                                                                    totPaidAmount = RecieptItemList.Sum(r => r.Sird_settle_amt);
                                                                    Session["totalPaidAmount"] = totPaidAmount;
                                                                    return Json(new { success = true, login = true, totPaid = totPaidAmount.ToString() }, JsonRequestBehavior.AllowGet);
                                                                }
                                                                else
                                                                {
                                                                    return Json(new { success = false, login = true, msg = "Exceed the pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                return Json(new { success = false, login = true, msg = "Already used this credit note.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                            }

                                                        }
                                                    }
                                                }
                                            }
                                            else {
                                                return Json(new { success = false, login = true, msg = "Advance receipt is expire. Pls. contact accounts dept.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                            }
                                                    
                                        }
                                        else
                                        {
                                            return Json(new { success = false, login = true, msg = "Advance receipt customer mismatch.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    else
                                    {
                                        return Json(new { success = false, login = true, msg = "Invalid Advanced Receipt No.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                                else {
                                    return Json(new { success = false, login = true, msg = "Invalid advance reference number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                                
                            }
                            else {
                                return Json(new { success = false, login = true, msg = "Invalid advance reference amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else {
                            return Json(new { success = false, login = true, msg = "Invalid advance reference number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }

                    }
                    else {
                        return Json(new { success = false, login = true, msg = "Invalid price amount entered.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult updateCRNOTEPayment(string payMode, string totAmount, string addedAmount, string refNo, string refAmount, string depositBank, string depositBranch, string customer, string invoiceNo)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string subChannel = HttpContext.Session["UserSubChannl"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (totAmount == "")
                    {
                        return Json(new { success = false, login = true, msg = "Add invoice item to pay.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    payMode = payMode.Trim();
                    refNo = refNo.Trim();
                    refAmount = refAmount.Trim();
                    invoiceNo = invoiceNo.Trim();
                    depositBank = depositBank.Trim();
                    depositBranch = depositBranch.Trim();
                    addedAmount = addedAmount.Trim();
                    TotalAmount = Convert.ToDecimal(totAmount);
                    if (TotalAmount <= 0)
                    {
                        return Json(new { success = false, login = true, msg = "Add invoice item to pay.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    customer = customer.Trim();
                    string paidAmountString = Session["totalPaidAmount"] as string;
                    decimal totPaidAmount = Convert.ToDecimal(paidAmountString);

                   
                    decimal n;
                    bool isNumeric = decimal.TryParse(addedAmount, out n);
                    if (isNumeric)
                    {
                        if (!string.IsNullOrEmpty(refNo))
                        {
                            if (!string.IsNullOrEmpty(refAmount))
                            {
                                string amount = CHNLSVC.Tours.getCreditRefAmount(customer, company, refNo, userDefPro);
                                if (amount != null)
                                {
                                    refAmount = amount;

                                    InvoiceHeader _invoice = CHNLSVC.Sales.GetInvoiceHeaderDetails(refNo);
                                    if (_invoice != null)
                                    {
                                        if (_invoice.Sah_direct)
                                        {
                                            return Json(new { success = false, login = true, msg = "Invalid reference number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                        }
                                        else {
                                            if (_invoice.Sah_stus == "C")
                                            {
                                                return Json(new { success = false, login = true, msg = "Cancelled Credit note.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                            }
                                            else {
                                                if (_invoice.Sah_cus_cd != customer)
                                                {
                                                    return Json(new { success = false, login = true, msg = "Credit note customer mismatch.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                }
                                                else {
                                                    if (((_invoice.Sah_anal_7 - _invoice.Sah_anal_8) * _invoice.Sah_ex_rt) < Convert.ToDecimal(addedAmount))
                                                    {
                                                        return Json(new { success = false, login = true, msg = "Amount larger than credit note amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                    }
                                                    else
                                                    {
                                                        decimal currnetPayAmount = Convert.ToDecimal(addedAmount);
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
                                                        RecieptItemTBS _item = new RecieptItemTBS();
                                                        BlackListCustomers _cus = CHNLSVC.Sales.GetBlackListCustomerDetails(customer, 1);
                                                        decimal currentBalanceAmount = (TotalAmount - totPaidAmount);
                                                        if (_cus.Hbl_cust_cd != null)
                                                        {
                                                            return Json(new { success = false, login = true, msg = "This Customer is Blacklist Customer.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                        }
                                                        else
                                                        {

                                                            DataTable _dtDepBank = CHNLSVC.General.getSubChannelDet(company, subChannel);
                                                            if (_dtDepBank.Rows.Count > 0)
                                                            {
                                                                if (Convert.ToInt32(_dtDepBank.Rows[0]["MSSC_IS_BNK_MAN"]) == 1)
                                                                {
                                                                    DataTable BankName = CHNLSVC.Sales.get_Dep_Bank_Name(company, userDefPro, payMode, depositBank);
                                                                    if (BankName.Rows.Count == 0)
                                                                    {
                                                                        return Json(new { success = false, login = true, msg = "Invalid deposit bank account.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                                    }
                                                                    else
                                                                    {
                                                                        bool has = RecieptItemList.Any(list => list.Sird_ref_no == refNo && list.Sird_inv_no == invoiceNo);
                                                                        if (!has)
                                                                        {
                                                                            if (currentBalanceAmount >= currnetPayAmount)
                                                                            {
                                                                                _item.Sird_ref_no = refNo;
                                                                                _item.Sird_deposit_bank_cd = depositBank;
                                                                                _item.Sird_deposit_branch = depositBranch;
                                                                                _item.Sird_inv_no = invoiceNo;
                                                                                _item.Sird_pay_tp = payMode;
                                                                                _item.Sird_settle_amt = Math.Round((Convert.ToDecimal(addedAmount)), 4);
                                                                                RecieptItemList.Add(_item);
                                                                                Session["RecieptItemList"] = RecieptItemList;
                                                                                totPaidAmount = RecieptItemList.Sum(r => r.Sird_settle_amt);
                                                                                Session["totalPaidAmount"] = totPaidAmount;
                                                                                return Json(new { success = true, login = true, totPaid = totPaidAmount.ToString() }, JsonRequestBehavior.AllowGet);
                                                                            }
                                                                            else
                                                                            {
                                                                                return Json(new { success = false, login = true, msg = "Exceed the pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            return Json(new { success = false, login = true, msg = "Already used this credit note.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                                        }
                                                                    }
                                                                }
                                                                else {
                                                                    bool has = RecieptItemList.Any(list => list.Sird_ref_no == refNo && list.Sird_inv_no == invoiceNo);
                                                                        if (!has)
                                                                        {
                                                                            if (currentBalanceAmount >= currnetPayAmount)
                                                                            {
                                                                                _item.Sird_ref_no = refNo;
                                                                                _item.Sird_deposit_bank_cd = depositBank;
                                                                                _item.Sird_deposit_branch = depositBranch;
                                                                                _item.Sird_inv_no = invoiceNo;
                                                                                _item.Sird_pay_tp = payMode;
                                                                                _item.Sird_settle_amt = Math.Round((Convert.ToDecimal(addedAmount)), 4);
                                                                                RecieptItemList.Add(_item);
                                                                                Session["RecieptItemList"] = RecieptItemList;
                                                                                totPaidAmount = RecieptItemList.Sum(r => r.Sird_settle_amt);
                                                                                Session["totalPaidAmount"] = totPaidAmount;
                                                                                return Json(new { success = true, login = true, totPaid = totPaidAmount.ToString() }, JsonRequestBehavior.AllowGet);

                                                                            }
                                                                            else
                                                                            {
                                                                                return Json(new { success = false, login = true, msg = "Exceed the pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            return Json(new { success = false, login = true, msg = "Already used this credit note.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                                        }
                                                                
                                                                }
                                                            }
                                                            else {
                                                                bool has = RecieptItemList.Any(list => list.Sird_ref_no == refNo && list.Sird_inv_no == invoiceNo);
                                                                if (!has)
                                                                {
                                                                    if (currentBalanceAmount >= currnetPayAmount)
                                                                    {
                                                                        _item.Sird_ref_no = refNo;
                                                                        _item.Sird_deposit_bank_cd = depositBank;
                                                                        _item.Sird_deposit_branch = depositBranch;
                                                                        _item.Sird_inv_no = invoiceNo;
                                                                        _item.Sird_pay_tp = payMode;
                                                                        _item.Sird_settle_amt = Math.Round((Convert.ToDecimal(addedAmount)), 4);
                                                                        RecieptItemList.Add(_item);
                                                                        Session["RecieptItemList"] = RecieptItemList;
                                                                        totPaidAmount = RecieptItemList.Sum(r => r.Sird_settle_amt);
                                                                        Session["totalPaidAmount"] = totPaidAmount;
                                                                        return Json(new { success = true, login = true, totPaid = totPaidAmount.ToString() }, JsonRequestBehavior.AllowGet);

                                                                    }
                                                                    else
                                                                    {
                                                                        return Json(new { success = false, login = true, msg = "Exceed the pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    return Json(new { success = false, login = true, msg = "Already used this credit note.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else {
                                        return Json(new { success = false, login = true, msg = "Invalid reference number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                                else {
                                    return Json(new { success = false, login = true, msg = "Invalid reference number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else {
                                return Json(new { success = false, login = true, msg = "Invalid reference amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else {
                            return Json(new { success = false, login = true, msg = "Invalid reference number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else {
                        return Json(new { success = false, login = true, msg = "Invalid price amount entered.", type = "Info" }, JsonRequestBehavior.AllowGet);
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

        Decimal BankOrOther_Charges = 0;
        Decimal BankOrOtherCharge = 0;
        public JsonResult updateCRCDPayment(string payMode, string totAmount, string addedAmount,string crdNo, string cardBank,string crdExpDt,string cardType,string batch,string offline,string online,string depositBank, string depositBranch, string customer, string invoiceNo)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string subChannel = HttpContext.Session["UserSubChannl"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (totAmount == "")
                    {
                        return Json(new { success = false, login = true, msg = "Add invoice item to pay.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    payMode = payMode.Trim();
                    crdNo = crdNo.Trim();
                    cardBank = cardBank.Trim();
                    crdExpDt = crdExpDt.Trim();
                    cardType = cardType.Trim();
                    invoiceNo = invoiceNo.Trim();
                    batch = batch.Trim();
                    depositBank = depositBank.Trim();
                    depositBranch = depositBranch.Trim();
                    addedAmount = addedAmount.Trim();
                    TotalAmount = Convert.ToDecimal(totAmount);
                    if (TotalAmount <= 0)
                    {
                        return Json(new { success = false, login = true, msg = "Add invoice item to pay.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    customer = (customer!=null)?customer.Trim():"";
                    string paidAmountString = Session["totalPaidAmount"] as string;
                    decimal totPaidAmount = Convert.ToDecimal(paidAmountString);
             
                   
                    decimal n;
                    bool isNumeric = decimal.TryParse(addedAmount, out n);
                    if (!isNumeric)
                    {
                        return Json(new { success = false, login = true, msg = "Invalid price amount entered.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(crdNo)) {
                        return Json(new { success = false, login = true, msg = "Invalid credit card number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(cardBank)) {
                        return Json(new { success = false, login = true, msg = "Invalid credit card bank.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(crdExpDt)) {
                        return Json(new { success = false, login = true, msg = "Invalid credit card expire date.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (!CheckBank(cardBank))
                    {
                        return Json(new { success = false, login = true, msg = "Invalid credit card bank.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(cardType)) {
                        return Json(new { success = false, login = true, msg = "Invalid credit card type.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }

                    decimal currnetPayAmount = Convert.ToDecimal(addedAmount);
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
                    RecieptItemTBS _item = new RecieptItemTBS();
                    BlackListCustomers _cus = CHNLSVC.Sales.GetBlackListCustomerDetails(customer, 1);
                    decimal currentBalanceAmount = (TotalAmount - totPaidAmount);
                    if (_cus.Hbl_cust_cd != null)
                    {
                        return Json(new { success = false, login = true, msg = "This Customer is Blacklist Customer.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                     DataTable _dtDepBank = CHNLSVC.General.getSubChannelDet(company, subChannel);
                     if (_dtDepBank.Rows.Count > 0)
                     {
                         if (Convert.ToInt32(_dtDepBank.Rows[0]["MSSC_IS_BNK_MAN"]) == 1)
                         {
                             DataTable BankName = CHNLSVC.Sales.get_Dep_Bank_Name(company, userDefPro, payMode, depositBank);
                             if (BankName.Rows.Count == 0)
                             {
                                 return Json(new { success = false, login = true, msg = "Invalid deposit bank account.", type = "Info" }, JsonRequestBehavior.AllowGet);
                             }
                         }
                     }
                     MasterOutsideParty _bankAccounts = CHNLSVC.Sales.GetOutSidePartyDetailsById(cardBank.Trim());
                     if (_bankAccounts == null)
                     {
                         return Json(new { success = false, login = true, msg = "Bank not found for code.", type = "Info" }, JsonRequestBehavior.AllowGet);
                     }


                    bool has = RecieptItemList.Any(list => list.Sird_ref_no == crdNo && list.Sird_inv_no == invoiceNo);
                    if (has)
                    {
                        return Json(new { success = false, login = true, msg = "Cannot pay using same credit card.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (currentBalanceAmount < currnetPayAmount)
                    {
                        return Json(new { success = false, login = true, msg = "Exceed the pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }


                    _item.Sird_chq_bank_cd = _bankAccounts.Mbi_cd;
                    //END
                    _item.Sird_cc_tp = cardType.ToString();
                    _item.Sird_cc_batch = batch;
                    _item.Sird_chq_bank_cd = "";

                    string _refNo = "";
                    try
                    {
                        if (crdNo.Length > 4)
                        {
                            string _last = crdNo.Substring(crdNo.Length - 4, 4);
                            string _first = "";
                            for (int i = 0; i < crdNo.Length - 4; i++)
                            {
                                _first = _first + "*";
                            }
                            _refNo = _first + _last;
                        }
                        else
                        {
                            _refNo = crdNo;
                        }
                    }
                    catch (Exception) { _refNo = crdNo; }
                    _item.Sird_ref_no = _refNo;
                    _item.Sird_cc_expiry_dt = Convert.ToDateTime(crdExpDt);


                    int mode = 0;
                    string branch_code = "";
                    string pc = userDefPro;
                    int period = 0;
                    if (offline == "true") mode = 0;
                    if (online == "true") mode = 1;
                    if (cardBank != "") branch_code = cardBank;
                    DataTable MID = CHNLSVC.Sales.get_bank_mid_code(branch_code, pc, mode, period,DateTime.Now,company);
                    if (MID.Rows.Count > 0)
                    {
                        DataRow dr;
                        dr = MID.Rows[0];
                        _item.Sird_chq_branch = MID.Rows[0].ToString().Trim();
                    }
                    _item.Sird_credit_card_bank = _bankAccounts.Mbi_cd;
                    _item.Sird_deposit_bank_cd = depositBank;
                    _item.Sird_deposit_branch = depositBranch;
                    _item.Sird_inv_no = invoiceNo;
                    _item.Sird_pay_tp = payMode;
                    _item.Sird_settle_amt = Math.Round((Convert.ToDecimal(addedAmount)), 4);
                    RecieptItemList.Add(_item);
                    Session["RecieptItemList"] = RecieptItemList;
                    totPaidAmount = RecieptItemList.Sum(r => r.Sird_settle_amt);
                    Session["totalPaidAmount"] = totPaidAmount;
                    return Json(new { success = true, login = true, totPaid = totPaidAmount.ToString() }, JsonRequestBehavior.AllowGet);
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
        public JsonResult removePaymode(string payMode, string amount)
        { 
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string subChannel = HttpContext.Session["UserSubChannl"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (payMode != "") {
                        decimal amt = 0;
                        bool isDecimal = decimal.TryParse(amount, out amt);
                        if (isDecimal)
                        {
                            List<RecieptItemTBS> RecieptItemList = new List<RecieptItemTBS>();
                            List<RecieptItemTBS> sesRecieptItemList = Session["RecieptItemList"] as List<RecieptItemTBS>;
                            if (sesRecieptItemList == null)
                            {
                                sesRecieptItemList = new List<RecieptItemTBS>();
                            }
                            RecieptItemList = sesRecieptItemList;
                            decimal amountVal = Convert.ToDecimal(amount);
                            var itemToRemove = RecieptItemList.Single(r => r.Sird_pay_tp == payMode && r.Sird_settle_amt == amountVal);
                            RecieptItemList.Remove(itemToRemove);
                            Session["RecieptItemList"] = RecieptItemList;
                            decimal totPaidAmount = RecieptItemList.Sum(r => r.Sird_settle_amt);
                            Session["totalPaidAmount"] = totPaidAmount;
                            return Json(new { success = true, login = true, totPaid = totPaidAmount.ToString() }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = true, login = true, msg="Invalid price amount.",type="Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }else{
                        return Json(new { success = true, login = true, msg = "Invalid pay mode.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
        
        private bool CheckBank(string bank)
        {
            try
            {
                    MasterOutsideParty _bankAccounts = new MasterOutsideParty();
                    List<PaymentType> _paymentTypeRef = Session["PaymentTypeRef"] as List<PaymentType>;
                    if (!string.IsNullOrEmpty(bank))
                    {
                        _bankAccounts = CHNLSVC.Sales.GetOutSidePartyDetailsById(bank.Trim());

                        if (_bankAccounts.Mbi_cd != null)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    return false;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        private bool CheckBankBranch(string bank, string branch)
        {
            if (!string.IsNullOrEmpty(branch))
            {
                MasterOutsideParty _bankAccounts = CHNLSVC.Sales.GetOutSidePartyDetailsById(bank.Trim());
                if (_bankAccounts != null)
                {
                    bool valid = CHNLSVC.Sales.validateBank_and_Branch(_bankAccounts.Mbi_cd, branch, "BANK");
                    return valid;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        
    }
}