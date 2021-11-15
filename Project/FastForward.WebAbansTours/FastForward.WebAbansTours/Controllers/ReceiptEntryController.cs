using FastForward.WebAbansTours.Models;
using FF.BusinessObjects;
using FF.BusinessObjects.ToursNew;
using FF.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Security.Authentication;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FastForward.WebAbansTours.Controllers
{
    public class ReceiptEntryController : BaseController
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
                ST_MENU per = CHNLSVC.Tours.getAcccessPermission(userId, 1022);
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
        // GET: ReceiptEntry
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                Session["closeBtn"] = "true";
                return View();
            }
            else
            {
                return Redirect("~/Login/index");
            }
        }
        public JsonResult loadPrefixes(string val)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string manualText = "";
                    bool enableFild = false;
                    if (val == "1")
                    {
                        Int32 _NextNo = CHNLSVC.Inventory.GetNextManualDocNo(company, userDefPro, "MDOC_AVREC");
                        if (_NextNo != 0)
                        {
                            manualText = _NextNo.ToString();
                            enableFild = true;
                        }
                        else
                        {
                            manualText = "";
                        }
                    }


                    MasterProfitCenter profCenter = CHNLSVC.Sales.GetProfitCenter(company, userDefPro);
                    string docTp = "";
                    if (val == "1")
                    {
                        //ISSys = false;
                        docTp = "MDOC_AVREC";
                    }
                    else
                    {
                        //   ISSys = true;
                        docTp = "SDOC_AVREC";
                    }
                    List<string> prifixes = new List<string>();
                    try
                    {
                        prifixes = CHNLSVC.Sales.GetAll_prifixes(profCenter.Mpc_chnl, docTp, 1);
                    }
                    catch (Exception)
                    {
                        return Json(new { success = false, login = true, msg = "" }, JsonRequestBehavior.AllowGet);
                    }
                    return Json(new { success = true, login = true, data = prifixes, enableFild = enableFild }, JsonRequestBehavior.AllowGet);
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

        public JsonResult loadDistrict()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<DistrictProvince> _district = CHNLSVC.Sales.GetDistrict("");
                    if (_district.Count > 0)
                    {
                        List<string> list = new List<string>();
                        var _final = (from _lst in _district
                                      select _lst.Mds_district).ToList();
                        list = _final.ToList();
                        return Json(new { success = true, login = true, data = list }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
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
        public JsonResult getProvince(string district)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    district = district.Trim();
                    if (district != "")
                    {
                        DistrictProvince _type = CHNLSVC.Sales.GetDistrict(district)[0];
                        if (_type.Mds_district == null)
                        {
                            return Json(new { success = false, login = true, msg = "Invalid district selected.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        return Json(new { success = true, login = true, province = _type.Mds_province }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Invalid district selected.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = Resource.ServerError, type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
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
                    return Json(new { success = false, login = true, msg = Resource.ServerError, type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult validDivision(string division)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    division = division.Trim();
                    if (division != "")
                    {
                        if (!CHNLSVC.Sales.IsValidDivision(company, userDefPro, division))
                        {
                            return Json(new { success = false, login = true, msg = "Invalid division.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Please enter division.", type = "Info" }, JsonRequestBehavior.AllowGet);
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

        public JsonResult getInvoiceDetails(string invNo, string cusCd, string type, string date, string chkOth, string chkOthVal)
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
                    cusCd = cusCd.Trim();
                    type = type.Trim();
                    if (!string.IsNullOrEmpty(invNo))
                    {
                        string invoiceAmount = "0.00";
                        if (!string.IsNullOrEmpty(invNo))
                        {
                            if (type == "DEBT")
                            {
                                if (string.IsNullOrEmpty(cusCd))
                                {
                                    return Json(new { success = false, login = true, msg = "Please select customer first.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                                //check valid invoice
                                List<InvoiceHeaderTBS> _invHdr = new List<InvoiceHeaderTBS>();
                                if (chkOth == "1")
                                    if (chkOthVal != "")
                                    {
                                        _invHdr = CHNLSVC.Sales.GetPendingInvoicesTBS(company, chkOthVal, cusCd, invNo, "C", date, date);
                                    }
                                    else
                                    {
                                        return Json(new { success = false, login = true, msg = "Please enter profit center.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                    }

                                else
                                    _invHdr = CHNLSVC.Sales.GetPendingInvoicesTBS(company, userDefPro, cusCd, invNo, "C", date, date);

                                if (_invHdr == null || _invHdr.Count == 0)
                                {
                                    return Json(new { success = false, login = true, msg = "Invalid invoice number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                                foreach (InvoiceHeaderTBS _tmpInv in _invHdr)
                                {
                                    if (_tmpInv.Sih_stus == "C" || _tmpInv.Sih_stus == "R")
                                    {
                                        return Json(new { success = false, login = true, msg = "Selected invoice is cancelled or reversed.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                                if (chkOth == "1")
                                    if (chkOthVal != "")
                                    {
                                        invoiceAmount = Convert.ToDecimal(CHNLSVC.Sales.GetOutInvAmtTBS(company, chkOthVal, cusCd, invNo)).ToString("n");
                                    }
                                    else
                                    {
                                        return Json(new { success = false, login = true, msg = "Please enter profit center.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                    }
                                else
                                    invoiceAmount = Convert.ToDecimal(CHNLSVC.Sales.GetOutInvAmtTBS(company, userDefPro, cusCd, invNo)).ToString("n");

                                if (Convert.ToDecimal(invoiceAmount) <= 0)
                                {
                                    return Json(new { success = false, login = true, msg = "Cannot find outstanding amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                invoiceAmount = "0.00";
                            }
                        }

                        return Json(new { success = true, login = true, invAmnt = invoiceAmount }, JsonRequestBehavior.AllowGet);
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
        public bool isDecimal(string val)
        {
            decimal result;
            return decimal.TryParse(val, out result);
        }

        public JsonResult addSettlementDetails(string paymentValue, string customer, string type, string invAmount, string invNo)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string paidAmountString = Session["totalPaidAmount"] as string;
                    decimal totPaidAmount = Convert.ToDecimal(paidAmountString);
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
                    List<INVOICE_SES_ITEM> InvItemsList = new List<INVOICE_SES_ITEM>();
                    List<INVOICE_SES_ITEM> sesInvItemsList = Session["SesInvoiceItemList"] as List<INVOICE_SES_ITEM>;
                    if (sesInvItemsList == null)
                    {
                        sesInvItemsList = new List<INVOICE_SES_ITEM>();
                    }
                    else
                    {
                        InvItemsList = sesInvItemsList;
                    }
                    bool oldInvNo = InvItemsList.Exists(element => element.SES_INV_NO == invNo);
                    
                    if (type == "DEBT")
                    {
                        if (!CHNLSVC.Security.Is_OptionPerimitted(company, userId, 16036))
                        {
                            if (Convert.ToDecimal(paymentValue) > Convert.ToDecimal(invAmount))
                            {
                                return Json(new { success = false, login = true, msg = "Payment cannot exceed outstanding amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if (oldInvNo)
                        {
                            return Json(new { success = false, login = true, msg = "Already add this invoice.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }

                        //if (balanceAmount > 0)
                        //{
                        //    return Json(new { success = false, login = true, msg = "Please pay added invoice amount payment.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        //}
                        if (Convert.ToDecimal(paymentValue) <= 0)
                        {
                            return Json(new { success = false, login = true, msg = "Settle amount cannot be zero.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        if (!CHNLSVC.Security.Is_OptionPerimitted(company, userId, 16036))
                        {
                            foreach (RecieptItemTBS line in RecieptItemList)
                            {
                                if (line.Sird_inv_no == invNo)
                                {
                                    return Json(new { success = false, login = true, msg = "Already add this invoice.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }

                        //if (!oldInvNo)
                        //    {
                        //        if (balanceAmount > 0)
                        //        {
                        //            return Json(new { success = false, login = true, msg = "Not allowed to go another payment with out complete selected payment.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        //        }
                        //    }
                        


                        decimal pdAmt = InvItemsList.Sum(m => m.SES_PAID_AMNT);
                        //decimal invlst = RecieptItemList.Where(m => m.Sard_inv_no == invNo).Sum(n => n.Sard_settle_amt);
                        decimal invlst = RecieptItemList.Sum(n => n.Sird_settle_amt);
                        if (pdAmt != invlst)
                        {
                            return Json(new { success = false, login = true, msg = "Not allowed to go another payment with out complete selected payment.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        INVOICE_SES_ITEM InvItems = new INVOICE_SES_ITEM();
                        InvItems.SES_INV_NO = invNo;
                        InvItems.SES_INV_AMNT = Convert.ToDecimal(invAmount);
                        InvItems.SES_PAID_AMNT = Convert.ToDecimal(paymentValue);
                        InvItemsList.Add(InvItems);
                        Session["SesInvoiceItemList"] = InvItemsList;
                        decimal bal = invlst - pdAmt;
                        return Json(new { success = true, login = true, balance = bal }, JsonRequestBehavior.AllowGet);
                    }






                    decimal _Amt = Convert.ToDecimal(paymentValue);
                    decimal TotalAmount = balanceAmount + _Amt;
                    ////////add session to update added invoice details

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
        public JsonResult clearValues() {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    Session["SesInvoiceItemList"] = null;
                    Session["totalPaidAmount"] = null;
                    Session["RecieptItemList"] = null;
                    Session["IssuedGiftVouchers"] = null;
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
        public JsonResult getGiftVoucherDetails(string vouNo) {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (string.IsNullOrEmpty(vouNo))
                    {
                        return Json(new { success = false, login = true, msg = "Please enter gift voucher code.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    MasterItem _itemList = new MasterItem();
                    _itemList = CHNLSVC.Inventory.GetItem(company, vouNo);

                    if (_itemList ==null ||_itemList.Mi_cd == null)
                    {
                        return Json(new { success = false, login = true, msg = "Invalid item selected.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }

                    if (_itemList.Mi_cate_3 != "N/A")
                    {
                        return Json(new { success = false, login = true, msg = "Selected Gift voucher is not allowed to sales.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }

                    DataTable _book = CHNLSVC.Inventory.GetAvailableGvBooks(company, userDefPro, "VALUE", "P", vouNo,"");
             

                    List<String> list = new List<String>();


                    for (Int32 i = 0; i < _book.Rows.Count; i++) {
                        string val = _book.Rows[i]["GVP_BOOK"].ToString();
                        list.Add(val);
                    }
                    return Json(new { success = true, login = true, data = list }, JsonRequestBehavior.AllowGet);
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
        public Boolean IsNumeric(string stringToTest)
        {
            decimal result;
            return decimal.TryParse(stringToTest, out result);
        }
        public JsonResult getGiftVoucherPagesTo(string vouBook,string vouNo) {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (String.IsNullOrEmpty(vouBook)) {
                        return Json(new { success = false, login = true, msg = "Invalid book number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }

                    if (!IsNumeric(vouBook))
                    {
                        return Json(new { success = false, login = true, msg = "Invalid book number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    List<GiftVoucherPages> _tmpList = new List<GiftVoucherPages>();
                    _tmpList = CHNLSVC.Inventory.GetAvailableGvPages(company, userDefPro, "VALUE", "P", Convert.ToInt32(vouBook), vouNo);

                    string lblFrompg = "";
                    if (_tmpList != null)
                    {
                        foreach (GiftVoucherPages tmp in _tmpList)
                        {
                            lblFrompg = tmp.Gvp_page.ToString();
                            goto L1;
                        }
                    }
                L1: return Json(new { success = true, login = true, data = _tmpList, fromPg = lblFrompg }, JsonRequestBehavior.AllowGet);
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
        public JsonResult getIssuPages(string pagesto,string vouNo,string vouBook,string frmPg) {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<GiftVoucherPages> _tmpList = new List<GiftVoucherPages>();

                    if (!IsNumeric(vouBook) || !IsNumeric(pagesto) || !IsNumeric(frmPg))
                    {
                        return Json(new { success = false, login = true, msg = "Invalid to page number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    string pageCnt="";
                    _tmpList = CHNLSVC.Inventory.GetAvailableGvPagesRange(company, userDefPro, "VALUE", "P", Convert.ToInt32(vouBook), vouNo, Convert.ToInt32(frmPg), Convert.ToInt32(pagesto));

                    if (_tmpList != null && _tmpList.Count > 0)
                    {
                        pageCnt = _tmpList.Count.ToString();
                    }
                    return Json(new { success = true, login = true, pageCnt = pageCnt }, JsonRequestBehavior.AllowGet);
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
        public JsonResult addGiftVouchers(RecieptHeaderTBS reciptVal, string vouBook, string vouCd, string frmPg, string toPg, string pgAmt, string ExpireOnDt,string pages)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    decimal _Amt = 0;
                    decimal totGftVouAmt = Convert.ToDecimal(pages) * Convert.ToDecimal(pgAmt);
                    
                    if (string.IsNullOrEmpty(reciptVal.Sir_debtor_cd))
                    {
                        return Json(new { success = false, login = true, msg = "Please select customer.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }

                    if (string.IsNullOrEmpty(reciptVal.Sir_debtor_add_1))
                    {
                        return Json(new { success = false, login = true, msg = "Please enter customer address.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    List<GiftVoucherPages> _gvDetails;
                    List<GiftVoucherPages> sesGftDet = Session["IssuedGiftVouchers"] as List<GiftVoucherPages>;
                    if (sesGftDet == null)
                    {
                        _gvDetails = new List<GiftVoucherPages>();
                    }
                    else
                    {
                        _gvDetails = sesGftDet;
                    }
                    if (_gvDetails.Count>0) 
                    {
                        return Json(new { success = false, login = true, msg = "Cannot add more vouchers.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    List<GiftVoucherPages> _tmpList = new List<GiftVoucherPages>();

                    _tmpList = CHNLSVC.Inventory.GetAvailableGvPagesRange(company, userDefPro, "VALUE", "P", Convert.ToInt32(vouBook), vouCd, Convert.ToInt32(frmPg), Convert.ToInt32(toPg));

                    if (_tmpList == null)
                    {
                        return Json(new { success = false, login = true, msg = "Cannot find details.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    foreach (GiftVoucherPages _lst in _tmpList)
                    {
                        GiftVoucherPages _newTmp = new GiftVoucherPages();
                        _newTmp.Gvp_amt = Convert.ToDecimal(pgAmt);
                        _newTmp.Gvp_app_by = null;
                        _newTmp.Gvp_bal_amt = Convert.ToDecimal(pgAmt);
                        _newTmp.Gvp_book = _lst.Gvp_book;
                        _newTmp.Gvp_can_by = null;
                        _newTmp.Gvp_can_dt = Convert.ToDateTime(DateTime.Today);
                        _newTmp.Gvp_com = _lst.Gvp_com;
                        _newTmp.Gvp_cre_by = userId;
                        _newTmp.Gvp_cre_dt = Convert.ToDateTime(DateTime.Today);
                        _newTmp.Gvp_cus_add1 = reciptVal.Sir_debtor_add_1;
                        _newTmp.Gvp_cus_add2 = reciptVal.Sir_debtor_add_2;
                        _newTmp.Gvp_cus_cd = reciptVal.Sir_debtor_cd;
                        _newTmp.Gvp_cus_mob = reciptVal.Sir_mob_no;
                        _newTmp.Gvp_cus_name = reciptVal.Sir_debtor_name;
                        _newTmp.Gvp_gv_cd = _lst.Gvp_gv_cd;
                        _newTmp.Gvp_gv_prefix = _lst.Gvp_gv_prefix;
                        _newTmp.Gvp_gv_tp = _lst.Gvp_gv_tp;
                        _newTmp.Gvp_issue_by = userId;
                        _newTmp.Gvp_issue_dt = Convert.ToDateTime(reciptVal.Sir_receipt_date).Date;
                        _newTmp.Gvp_line = _lst.Gvp_line;
                        _newTmp.Gvp_mod_by = userId;
                        _newTmp.Gvp_mod_dt = Convert.ToDateTime(DateTime.Today);
                        _newTmp.Gvp_oth_ref = null;
                        _newTmp.Gvp_page = _lst.Gvp_page;
                        _newTmp.Gvp_pc = _lst.Gvp_pc;
                        _newTmp.Gvp_ref = _lst.Gvp_ref;
                        _newTmp.Gvp_stus = "A";
                        _newTmp.Gvp_valid_from = Convert.ToDateTime(reciptVal.Sir_receipt_date).Date;
                        MasterItemWarrantyPeriod _period = CHNLSVC.Inventory.GetItemWarrantyDetail(_lst.Gvp_gv_cd, "GOD");
                        if (_period == null)
                        {
                            return Json(new { success = false, login = true, msg = "Voucher valid period not set.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }

                        if (Convert.ToDateTime(ExpireOnDt).Date > DateTime.Now.Date)
                            _newTmp.Gvp_valid_to = Convert.ToDateTime(ExpireOnDt).Date;
                        else
                            _newTmp.Gvp_valid_to = Convert.ToDateTime(ExpireOnDt).Date.AddMonths(_period.Mwp_val).Date;

                        _gvDetails.Add(_newTmp);
                    }
                    Session["IssuedGiftVouchers"] = _gvDetails;
                    return Json(new { success = true, login = true, data = _gvDetails }, JsonRequestBehavior.AllowGet);
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
        public JsonResult saveReciptData(RecieptHeaderTBS reciptVal, string Division, string CheckManSys,string isOtherParty)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string OthSr="";
            string OthSrVal="";
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string checkTimeMsg = string.Empty;
                    if (CheckServerDateTime(out checkTimeMsg) == false)
                    {
                        return Json(new { success = false, login = true, msg = checkTimeMsg, type = "Info" }, JsonRequestBehavior.AllowGet);
                    }

                    if (String.IsNullOrEmpty(reciptVal.Sir_receipt_type)) {
                        return Json(new { success = false, login = true, msg = "Please enter receipt type.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(reciptVal.Sir_debtor_cd)) {
                        return Json(new { success = false, login = true, msg = "Please enter customer.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(Division))
                    {
                        return Json(new { success = false, login = true, msg = "Receipt division is missing.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }

                    if (!CHNLSVC.Sales.IsValidDivision(company, userDefPro, Division))
                    {
                        return Json(new { success = false, login = true, msg = "Invalid division.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }

                    //if (reciptVal.Sir_receipt_type == "ADVAN" || reciptVal.Sir_receipt_type == "OTHER")
                    //{
                    //    if (string.IsNullOrEmpty(reciptVal.Sir_prefix))
                    //    {
                    //        return Json(new { success = false, login = true, msg = "Please enter prefix.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    //    }
                    //}

                    //if (CheckManSys == "1")
                    //{
                    //    if (String.IsNullOrEmpty(reciptVal.Sir_manual_ref_no))
                    //    {
                    //        return Json(new { success = false, login = true, msg = "Please enter manual document number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    //    }

                    //    Boolean _IsValid = CHNLSVC.Inventory.IsValidManualDocument_prefix(company, userDefPro, "MDOC_AVREC", Convert.ToInt32(reciptVal.Sir_manual_ref_no), reciptVal.Sir_prefix);
                    //    if (_IsValid == false)
                    //    {
                    //        return Json(new { success = false, login = true, msg = "Invalid manual document number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    //    }

                    //    RecieptHeader rh = new RecieptHeader();
                    //    rh = CHNLSVC.Sales.Check_ManRef_Rec_prefix(company, userDefPro, reciptVal.Sir_receipt_type, reciptVal.Sir_manual_ref_no, reciptVal.Sir_prefix);

                    //    if (rh != null)
                    //    {
                    //        return Json(new { success = false, login = true, msg = "Receipt number : " + reciptVal.Sir_manual_ref_no + " already used.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    //    }
                    //}
                    if (reciptVal.Sir_receipt_type == "ADVAN" || reciptVal.Sir_receipt_type == "OTHER")
                    {
                        //if (CheckManSys == "0")
                        //{
                        //    if (string.IsNullOrEmpty(reciptVal.Sir_manual_ref_no))
                        //    {
                        //        return Json(new { success = false, login = true, msg = "Please enter manual document number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                
                        //    }

                        //    Boolean _IsValid = CHNLSVC.Inventory.IsValidManualDocument_prefix(company, userDefPro, "SDOC_AVREC", Convert.ToInt32(reciptVal.Sir_manual_ref_no), reciptVal.Sir_prefix);
                        //    if (_IsValid == false)
                        //    {
                        //        return Json(new { success = false, login = true, msg = "Invalid manual document number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        //    }

                        //    RecieptHeader rh = new RecieptHeader();
                        //    rh = CHNLSVC.Sales.Check_ManRef_Rec_prefix(company, userDefPro,reciptVal.Sir_receipt_type, reciptVal.Sir_manual_ref_no, reciptVal.Sir_prefix);

                        //    if (rh != null)
                        //    {
                        //        return Json(new { success = false, login = true, msg = "Receipt number : " + reciptVal.Sir_manual_ref_no + " already used.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        //    }
                        //}
                        List<Hpr_SysParameter> para = CHNLSVC.Sales.GetAll_hpr_Para("ADREFMAXDT", "COM",company);

                        if (para.Count <= 0)
                        {
                            return Json(new { success = false, login = true, msg = "System parameter not setup for Advance receipt valid period.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
                    if (RecieptItemList.Count == 0) {
                        return Json(new { success = false, login = true, msg = "Payment not completed.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (reciptVal.Sir_receipt_type == "ADVAN" || reciptVal.Sir_receipt_type == "OTHER") {
                        List<INVOICE_SES_ITEM> InvItemsList = new List<INVOICE_SES_ITEM>();
                        List<INVOICE_SES_ITEM> sesInvItemsList = Session["SesInvoiceItemList"] as List<INVOICE_SES_ITEM>;
                        if (sesInvItemsList != null)
                        {
                            if (sesInvItemsList == null)
                            {
                                sesInvItemsList = new List<INVOICE_SES_ITEM>();
                            }
                            else
                            {
                                InvItemsList = sesInvItemsList;
                            }
                            if (InvItemsList.Count == 0)
                            {
                                return Json(new { success = false, login = true, msg = "Please add payment.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            decimal thisPayAmt = InvItemsList.Sum(m => m.SES_PAID_AMNT);
                            decimal patAmt = RecieptItemList.Sum(n => n.Sird_settle_amt);
                            if (thisPayAmt != patAmt)
                            {
                                return Json(new { success = false, login = true, msg = "Payment not completed.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else {
                            decimal patAmt = RecieptItemList.Sum(n => n.Sird_settle_amt);
                            if (patAmt==0)
                            {
                                return Json(new { success = false, login = true, msg = "Payment not completed.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }

                    }
                    if (reciptVal.Sir_receipt_type == "DEBT") {
                        
                    }
                    if (reciptVal.Sir_receipt_type == "ADVAN")
                    {
                        if (isOtherParty == "True")
                        {
                            if (reciptVal.Sir_oth_partycd == "" || reciptVal.Sir_oth_partycd==null)
                            {
                                return Json(new { success = false, login = true, msg = "Please select other party agent.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            else { 
                                decimal patAmt = RecieptItemList.Sum(n => n.Sird_settle_amt);
                                reciptVal.Sir_oth_partystltamt = patAmt;
                                reciptVal.Sir_oth_party = 1;
                            }
                        }
                        else {
                            reciptVal.Sir_oth_partycd = "";
                            reciptVal.Sir_oth_partyname = "";
                            reciptVal.Sir_oth_party = 0;
                        }
                    }
                    else {
                        reciptVal.Sir_oth_partycd = "";
                        reciptVal.Sir_oth_partyname = "";
                        reciptVal.Sir_oth_party = 0;
                    }
                    Int32 row_aff = SaveReceiptHeader(reciptVal, Division, CheckManSys, OthSr, OthSrVal);
                    //if (CheckManSys == "1")
                    //{
                    //    if (userDefLoc != userDefPro)
                    //    {
                    //        CHNLSVC.Inventory.UpdateManualDocNo(userDefLoc, "MDOC_AVREC", Convert.ToInt32(reciptVal.Sir_manual_ref_no), QTNum);
                    //    }
                    //}

                    //if (CheckManSys == "0")
                    //{
                    //    if (userDefLoc != userDefPro)
                    //    {
                    //        CHNLSVC.Inventory.UpdateManualDocNo(userDefLoc, "SDOC_AVREC", Convert.ToInt32(reciptVal.Sir_manual_ref_no), QTNum);
                    //    }
                    //}

                    if (row_aff == 1)
                    {
                        //if (CheckManSys == "1")
                        //{
                        //    return Json(new { success = true, login = true, msg ="Successfully created.Receipt No: " + QTNum,recNo=QTNum, type = "Success" }, JsonRequestBehavior.AllowGet);
                        //}
                        //else
                        //{
                            return Json(new { success = true, login = true, msg = "Successfully created.Receipt No: " + QTNum, recNo = QTNum, type = "Success" }, JsonRequestBehavior.AllowGet);
                       // }
                    }
                    else
                    {
                        //if (!string.IsNullOrEmpty(QTNum))
                        //{
                        //    return Json(new { success = true, login = true, msg = QTNum, type = "Error" }, JsonRequestBehavior.AllowGet);
                        //}
                        //else
                        //{
                            return Json(new { success = false, login = true, msg = "Creation Fail.", type = "Error" }, JsonRequestBehavior.AllowGet);
                        //}
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
        string QTNum;
        private Int32 SaveReceiptHeader(RecieptHeaderTBS receiptVal, string Division, string CheckManSys, string OthSr, string OthSrVal)
        {
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userId = HttpContext.Session["UserID"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            Int32 row_aff = 0;
            string msg = "";
            string _msg = string.Empty;
            decimal _valPd = 0;

            if (receiptVal.Sir_receipt_type == "ADVAN" || receiptVal.Sir_receipt_type == "OTHER")
            {
                List<Hpr_SysParameter> para = CHNLSVC.Sales.GetAll_hpr_Para("ADREFMAXDT", "COM", company);
                if (para.Count > 0)
                {
                    _valPd = para[0].Hsy_val;
                }
            }

            RecieptHeaderTBS _ReceiptHeader = new RecieptHeaderTBS();
            _ReceiptHeader.Sir_seq_no = CHNLSVC.Inventory.GetSerialID(); //CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "RECEIPT", 1, Session["UserCompanyCode"].ToString());
            _ReceiptHeader.Sir_com_cd = company;
            _ReceiptHeader.Sir_receipt_type = receiptVal.Sir_receipt_type.Trim();
            _ReceiptHeader.Sir_receipt_no = _ReceiptHeader.Sir_seq_no.ToString();// txtRecNo.Text.Trim();
            if (receiptVal.Sir_receipt_type == "ADVAN" || receiptVal.Sir_receipt_type == "OTHER")
            {
                _ReceiptHeader.Sir_prefix = receiptVal.Sir_prefix;
            }
            else
            {
                _ReceiptHeader.Sir_prefix = Division;
            }
            if (string.IsNullOrEmpty(receiptVal.Sir_manual_ref_no))
            {
                receiptVal.Sir_manual_ref_no = "0";
            }
            else
            {
                _ReceiptHeader.Sir_manual_ref_no = receiptVal.Sir_manual_ref_no;
            }

            _ReceiptHeader.Sir_receipt_date = Convert.ToDateTime(receiptVal.Sir_receipt_date).Date;
            _ReceiptHeader.Sir_direct = true;
            _ReceiptHeader.Sir_acc_no = "";
            if (OthSr == "1")
            {
                _ReceiptHeader.Sir_is_oth_shop = true;
                _ReceiptHeader.Sir_oth_sr = OthSrVal;
            }
            else
            {
                _ReceiptHeader.Sir_is_oth_shop = false;
                _ReceiptHeader.Sir_oth_sr = "";
            }
            _ReceiptHeader.Sir_profit_center_cd = userDefPro;
            _ReceiptHeader.Sir_debtor_cd = receiptVal.Sir_debtor_cd.Trim();
            _ReceiptHeader.Sir_debtor_name = receiptVal.Sir_debtor_name.Trim();
            _ReceiptHeader.Sir_debtor_add_1 = (receiptVal.Sir_debtor_add_1!=null)?receiptVal.Sir_debtor_add_1.Trim():"";
            _ReceiptHeader.Sir_debtor_add_2 = (receiptVal.Sir_debtor_add_2!=null)?receiptVal.Sir_debtor_add_2.Trim():"";
            _ReceiptHeader.Sir_tel_no = "";
            _ReceiptHeader.Sir_mob_no = (receiptVal.Sir_mob_no!=null)?receiptVal.Sir_mob_no.Trim():"";
            _ReceiptHeader.Sir_nic_no = (receiptVal.Sir_nic_no!=null)?receiptVal.Sir_nic_no.Trim():"";
            _ReceiptHeader.Sir_oth_partycd = receiptVal.Sir_oth_partycd;
            _ReceiptHeader.Sir_oth_partyname = receiptVal.Sir_oth_partyname;
            _ReceiptHeader.Sir_oth_party = receiptVal.Sir_oth_party;
            _ReceiptHeader.Sir_oth_partystltamt = receiptVal.Sir_oth_partystltamt;
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
            decimal PaidAmountLabel = RecieptItemList.Sum(m => m.Sird_settle_amt);

            _ReceiptHeader.Sir_tot_settle_amt = Convert.ToDecimal(PaidAmountLabel);
            _ReceiptHeader.Sir_comm_amt = 0;
            _ReceiptHeader.Sir_is_mgr_iss = false;
            _ReceiptHeader.Sir_esd_rate = 0;
            _ReceiptHeader.Sir_wht_rate = 0;
            _ReceiptHeader.Sir_epf_rate = 0;
            _ReceiptHeader.Sir_currency_cd = "LKR";
            _ReceiptHeader.Sir_uploaded_to_finance = false;
            _ReceiptHeader.Sir_act = true;
            _ReceiptHeader.Sir_direct_deposit_bank_cd = "";
            _ReceiptHeader.Sir_direct_deposit_branch = "";
            _ReceiptHeader.Sir_remarks = (receiptVal.Sir_remarks!=null)?receiptVal.Sir_remarks.Trim():"";
            _ReceiptHeader.Sir_is_used = false;
            if (receiptVal.Sir_receipt_type == "ADVAN")
            {
                _ReceiptHeader.Sir_ref_doc = Division.Trim();
            }
            else
            {
                _ReceiptHeader.Sir_ref_doc = "";
            }
            _ReceiptHeader.Sir_ser_job_no = "";
            _ReceiptHeader.Sir_used_amt = 0;
            List<INVOICE_SES_ITEM> InvItemsList = new List<INVOICE_SES_ITEM>();
            List<INVOICE_SES_ITEM> sesInvItemsList = Session["SesInvoiceItemList"] as List<INVOICE_SES_ITEM>;
            if (sesInvItemsList == null)
            {
                sesInvItemsList = new List<INVOICE_SES_ITEM>();
            }
            else
            {
                InvItemsList = sesInvItemsList;
            }
            if (receiptVal.Sir_receipt_type == "DEBT" && InvItemsList.Count == 0)
            {
                _ReceiptHeader.Sir_used_amt = -1;
            }

            _ReceiptHeader.Sir_create_by = Session["UserID"].ToString();
            _ReceiptHeader.Sir_mod_by = Session["UserID"].ToString();
            _ReceiptHeader.Sir_session_id = userId;
            _ReceiptHeader.Sir_anal_1 = receiptVal.Sir_anal_1;
            _ReceiptHeader.Sir_anal_2 = (receiptVal.Sir_anal_2 != null) ? receiptVal.Sir_anal_2.Trim() : "";

            //if (CheckManSys == "1")
            //{
            //    _ReceiptHeader.Sir_anal_3 = "MANUAL";
            //    _ReceiptHeader.Sir_anal_8 = 1;
            //}
            //else
            //{
            //    _ReceiptHeader.Sir_anal_3 = "SYSTEM";
            //    _ReceiptHeader.Sir_anal_8 = 0;
            //}

            _ReceiptHeader.Sir_anal_4 = receiptVal.Sir_anal_4;
            _ReceiptHeader.Sir_anal_5 = 0;
            _ReceiptHeader.Sir_anal_6 = 0;
            _ReceiptHeader.Sir_anal_7 = 0;
            _ReceiptHeader.Sir_anal_9 = 0;
            _ReceiptHeader.Sir_VALID_TO = _ReceiptHeader.Sir_receipt_date.AddDays(Convert.ToDouble(_valPd));
            //_ReceiptHeader.Sir_scheme = lblSchme.Text;
            //_ReceiptHeader.Sir_inv_type = lblSalesType.Text;

            List<RecieptItemTBS> _ReceiptDetailsSave = new List<RecieptItemTBS>();
            Int32 _line = 0;
            foreach (RecieptItemTBS line in RecieptItemList)
            {
                line.Sird_seq_no = _ReceiptHeader.Sir_seq_no;
                _line = _line + 1;
                line.Sird_line_no = _line;
                _ReceiptDetailsSave.Add(line);
            }

            MasterAutoNumber masterAuto = new MasterAutoNumber();
            masterAuto.Aut_cate_cd = userDefPro;
            masterAuto.Aut_cate_tp = "PC";
            masterAuto.Aut_direction = null;
            masterAuto.Aut_modify_dt = null;
            masterAuto.Aut_moduleid = "RECEIPT";
            masterAuto.Aut_number = 5;//what is Aut_number
            masterAuto.Aut_start_char = Division.Trim();
            masterAuto.Aut_year = null;// DateTime.Now.Year;

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
            masterAutoRecTp.Aut_start_char = receiptVal.Sir_receipt_type.Trim();
            masterAutoRecTp.Aut_year = null;

            

            List<RecieptHeaderTBS> otest = new List<RecieptHeaderTBS>();
            otest.Add(_ReceiptHeader);
            //DataTable dt = ToDataTable(_ReceiptDetailsSave);
            //DataTable d2t = ToDataTable(otest);

            row_aff = (Int32)CHNLSVC.Sales.SaveNewReceiptTBS(_ReceiptHeader, _ReceiptDetailsSave, masterAuto, null, null, null, null, null, masterAutoRecTp, null, out QTNum);

           
            return row_aff;
        }
        public JsonResult getReceiptDetails(string receiptNo) {
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
                        Boolean _sunUpload = false;
                        Boolean _isValidRec = false;

                        RecieptHeaderTBS _ReceiptHeader = null;
                        _ReceiptHeader = CHNLSVC.Tours.GetReceiptHeaderTBS(company, userDefPro, receiptNo.Trim());

                        if (_ReceiptHeader != null)
                        {
                            _isValidRec = CHNLSVC.Sales.IsValidReceiptType(company, _ReceiptHeader.Sir_receipt_type);

                            if (_isValidRec == false)
                            {
                                return Json(new { success = false, login = true, msg = "Not allowed to view receipt type.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }

                            RecieptItemTBS _paramRecDetails = new RecieptItemTBS();

                            _paramRecDetails.Sird_receipt_no = receiptNo;
                            List<RecieptItemTBS> _list = new List<RecieptItemTBS>();
                            _list = CHNLSVC.Sales.GetReceiptDetailsTBS(_paramRecDetails);


                            List<GiftVoucherPages> _gvDetails = new List<GiftVoucherPages>();
                            _gvDetails = CHNLSVC.Inventory.GetGiftVoucherByOthRef(_ReceiptHeader.Sir_com_cd, _ReceiptHeader.Sir_profit_center_cd, _ReceiptHeader.Sir_receipt_no);

                            if (receiptNo == "GVISU")
                            {
                                //tbOth.SelectedTab = tbGv;
                            }
                            List<ReceiptItemDetails> _tmpRecItem = new List<ReceiptItemDetails>();
                            _tmpRecItem = CHNLSVC.Sales.GetAdvanReceiptItems(_ReceiptHeader.Sir_receipt_no);
                            return Json(new { success = true, login = true, ReceiptHeader = _ReceiptHeader, receiptItem = _list, gvDetails = _gvDetails, tmpRecItem = _tmpRecItem }, JsonRequestBehavior.AllowGet);
                        }
                        else {
                            return Json(new { success = false, login = true, msg = "Invalid receipt number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else {
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
                    return Json(new { success = false, login = true, msg = Resource.ServerError, type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult ReceiptReport(string recNo = null)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                recNo = (recNo != null) ? recNo.Trim() : null;
                if (recNo != null)
                {
                    string content;
                    PrintModel model = new PrintModel();
                    content = Url.Content("~/Print_Module/Print_Viwer/ReceiptPrintViwer.aspx");
                    model.ReportPath = content;
                    model.receiptNo = recNo;
                    Session["ReceiptNumber"] = recNo;
                    return View("ReceiptReport", model);
                }
                else
                {
                    return Redirect("~/ReceiptEntry");
                }

            }
            else
            {
                return Redirect("~/Login");
            }
        }

        public JsonResult getEnquiryData(string enqId) { 
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (enqId != "")
                    {
                        string Status = "0,1,2,3,4,5,6,7,8,9,10,11,12,13";
                        List<GEN_CUST_ENQ> enqData = CHNLSVC.Tours.getAllEnquiryData(enqId,company, userDefPro, Status, userId, 15001);
                        if (enqData.Count > 0)
                        {
                            return Json(new { success = true, login = true, enqData = enqData[0] }, JsonRequestBehavior.AllowGet);
                        }
                        else {
                            return Json(new { success = false, login = true,msg="Invalid enquiry id.",type="Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else {
                        return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
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
    }

}
public class INVOICE_SES_ITEM
{

    public string SES_INV_NO { get; set; }
    public decimal SES_INV_AMNT { get; set; }
    public decimal SES_PAID_AMNT { get; set; }
}