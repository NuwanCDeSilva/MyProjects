using FF.BusinessObjects;
using FF.BusinessObjects.Financial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FF.SCMWebMVC.Controllers
{
    public class RentPaymentsController : BaseController
    {
        // GET: RentPayments
        public ActionResult Index()
        {
            if (Session["OwnerDetails"] != null)
            {
                Session["OwnerDetails"] = "";
            }
            return View();
        }

        [HttpPost]
        public JsonResult SAVE_RENT_PAYMENT_DETAILS(FormCollection data, string rentfromdt, string renttodt)
        {

            string userId = HttpContext.Session["UserId"] as string;
            string companyId = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string p_paymenttype = data["paymenttype"].ToString();
            string p_paymentsubtype = data["paymensubttype"].ToString();
            string psh_com = companyId;
            string rentdts = data["rentdts"].ToString();
            string psh_pc = data["pft"].ToString();
            string psh_add1 = data["addresse1"].ToString();
            string psh_add2 = data["addresse2"].ToString();
            string psh_dist = data["district"].ToString();
            string psh_prv = data["provision"].ToString();
            string p_creditaccount = data["creditacc"].ToString();
            string p_debitaccount = data["debitacc"].ToString();
            string psh_frm_dt = rentfromdt.ToString();
            string psh_to_dt = renttodt.ToString();
            string psh_ref_no = data["agreementref"].ToString();
            string psh_rmk = data["remark"].ToString();
            string psh_trm = data["terminatetype"].ToString();
            string psh_stp = data["paymensubttype"].ToString();
            string psh_cre_by = userId;
            string p_sqfeet = data["sqfeet"].ToString();
            List<PAY_SCH_OWNERS_DET> Ownerdetails = new List<PAY_SCH_OWNERS_DET>();
            List<PAY_SCHEDULE_DETAILS> scheduledet = new List<PAY_SCHEDULE_DETAILS>();
            if (rentdts == null)
            {
                rentdts = "-1";
            }
            if (psh_trm == "terminatewithdate")
            {
                psh_trm = "0";
            }
            else { psh_trm = "1"; }

            MasterAutoNumber mastAutoNo = new MasterAutoNumber();
            mastAutoNo.Aut_cate_cd = Session["UserCompanyCode"].ToString();
            mastAutoNo.Aut_cate_tp = "COM";
            mastAutoNo.Aut_direction = 1;
            mastAutoNo.Aut_moduleid = "RENT";
            mastAutoNo.Aut_start_char = "RNT";
            mastAutoNo.Aut_year = DateTime.Now.Year;

            if (Session["OwnerDetails"] != null)
            {
                Ownerdetails = (List<PAY_SCH_OWNERS_DET>)Session["OwnerDetails"];
            }

            if (Session["ScheduleDetails"] != null)
            {
                scheduledet = (List<PAY_SCHEDULE_DETAILS>)Session["ScheduleDetails"];
            }

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(companyId) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string err = "0";
                    int documents = CHNLSVC.Finance.SAVE_RENT_PAYMENT_DETAILS(mastAutoNo, p_paymenttype, p_paymentsubtype, psh_com, psh_pc, psh_add1, psh_add2, psh_dist, psh_prv,
                        p_creditaccount, p_debitaccount, psh_frm_dt, psh_to_dt, psh_ref_no, psh_rmk, psh_trm, psh_cre_by, p_sqfeet, Ownerdetails, rentdts, scheduledet, out err);

                    if (documents != 0)
                    {
                        return Json(new { success = true, login = true, data = documents }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No data found for this search criteria.", type = "Error" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(companyId) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }

        public JsonResult getSCHColumns(string p_type)
        {
            string userId = HttpContext.Session["UserId"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {

                    List<SCH_PAY_COLUMN> documents = CHNLSVC.Finance.getSCHColumns(p_type);


                    if (documents != null)
                    {

                        List<SCH_COL_DET> ownercol = new List<SCH_COL_DET>();
                        for (int i = 0; i < documents.Count; i++)
                        {
                            SCH_COL_DET ownerobj = new SCH_COL_DET();
                            ownerobj.psi_hed_cd = documents[i].psi_hed_cd.ToString();
                            ownerobj.psi_col_seq = documents[i].psi_col_seq.ToString();
                            ownercol.Add(ownerobj);
                        }

                        Session["OwnerCols"] = ownercol;
                        return Json(new { success = true, login = true, data = documents }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No data found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }

        [HttpPost]
        public void OwnerDetailsManagement(FormCollection data, string scheduleid, string recid)
        {
            try
            {
                List<PAY_SCH_OWNERS_DET> Ownerdetails = new List<PAY_SCH_OWNERS_DET>();
                if (Session["OwnerDetails"] != null)
                {
                    Ownerdetails = (List<PAY_SCH_OWNERS_DET>)Session["OwnerDetails"];
                }
                String ownercoldata = "";
                List<SCH_COL_DET> ownercol = new List<SCH_COL_DET>();
                ownercol = (List<SCH_COL_DET>)Session["OwnerCols"];

                foreach (SCH_COL_DET item in ownercol)
                {
                    PAY_SCH_OWNERS_DET owner = new PAY_SCH_OWNERS_DET();
                    ownercoldata = data[item.psi_hed_cd + '-' + item.psi_col_seq];
                    owner.psa_clm_id = item.psi_col_seq;
                    owner.psa_fld_cd = scheduleid;
                    owner.psa_hed_cd = item.psi_hed_cd;
                    owner.psa_rec_id = recid;
                    owner.psa_value = ownercoldata;
                    Ownerdetails.Add(owner);
                }

                Session["OwnerDetails"] = Ownerdetails;

            }
            catch (Exception ex) { }
        }

        public JsonResult getSCHOwnerDetails(string p_schid, string p_type)
        {
            Session["OwnerDetails"] = "";
            string userId = HttpContext.Session["UserId"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {

                    List<PAY_SCH_OWNERS_DET> documents = CHNLSVC.Finance.getSCHOwnerDetails(p_schid, p_type);
                    Session["OwnerDetails"] = documents;


                    if (documents != null)
                    {
                        return Json(new { success = true, login = true, data = documents }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No data found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }

        public JsonResult getOwnersToTable()
        {
            string userId = HttpContext.Session["UserId"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (Session["OwnerDetails"] != null)
            {
                List<PAY_SCH_OWNERS_DET> documents = (List<PAY_SCH_OWNERS_DET>)Session["OwnerDetails"];
                return Json(new { success = true, login = true, data = documents }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getColumns()
        {
            string userId = HttpContext.Session["UserId"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (Session["OwnerCols"] != null)
            {
                List<SCH_COL_DET> documents = (List<SCH_COL_DET>)Session["OwnerCols"];
                return Json(new { success = true, login = true, data = documents }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getRecordidcounts()
        {
            try
            {
                List<PAY_SCH_OWNERS_DET> Ownerdetails = new List<PAY_SCH_OWNERS_DET>();
                Ownerdetails = (List<PAY_SCH_OWNERS_DET>)Session["OwnerDetails"];

                var documents = Ownerdetails.GroupBy(x => x.psa_rec_id)
                                      .Select(g => g.First()).OrderBy(x => x.psa_rec_id).ToList();

                return Json(new { success = true, login = true, data = documents }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = true, data = "" }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult getRecordbyid(string recordid)
        {
            List<PAY_SCH_OWNERS_DET> Ownerdetails = new List<PAY_SCH_OWNERS_DET>();
            Ownerdetails = (List<PAY_SCH_OWNERS_DET>)Session["OwnerDetails"];

            var documents = (from aa in Ownerdetails where aa.psa_rec_id == recordid select aa).ToList();

            return Json(new { success = true, login = true, data = documents }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteRecord(string recordid)
        {
            try
            {
                List<PAY_SCH_OWNERS_DET> Ownerdetails = new List<PAY_SCH_OWNERS_DET>();
                Ownerdetails = (List<PAY_SCH_OWNERS_DET>)Session["OwnerDetails"];
                var test = "";

                Ownerdetails.RemoveAll(x => x.psa_rec_id == recordid);

                Session["OwnerDetails"] = Ownerdetails;
                return Json(new { success = true, login = true, data = test }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = true, data = "" }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult GetOwnerRecordDetails(string recordid)
        {
            try
            {
                List<PAY_SCH_OWNERS_DET> Ownerdetails = new List<PAY_SCH_OWNERS_DET>();
                Ownerdetails = (List<PAY_SCH_OWNERS_DET>)Session["OwnerDetails"];
                var test = (from aa in Ownerdetails where aa.psa_rec_id == recordid select aa).ToList();
                Ownerdetails.RemoveAll(x => x.psa_rec_id == recordid);
                Session["OwnerDetails"] = Ownerdetails;

                return Json(new { success = true, login = true, data = test }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = true, data = "" }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult RemoveSCHOwnerDetails(string p_psa_fld_cd, string p_psa_hed_cd, string p_psa_rec_id)
        {
            string userId = HttpContext.Session["UserId"] as string;
            string companyId = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string p_session = HttpContext.Session["Session"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(companyId) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {

                    int documents = CHNLSVC.Finance.REMOVE_SCH_OWNER(p_psa_fld_cd, p_psa_hed_cd, p_psa_rec_id, userId);

                    List<PAY_SCH_OWNERS_DET> Ownerdetails = new List<PAY_SCH_OWNERS_DET>();

                    if (Session["OwnerDetails"] != null)
                    {
                        Ownerdetails = (List<PAY_SCH_OWNERS_DET>)Session["OwnerDetails"];
                    }

                    Ownerdetails.RemoveAll(x => x.psa_rec_id == p_psa_rec_id);


                    if (documents != 0)
                    {
                        return Json(new { success = true, login = true, data = documents }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "error", type = "Error" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(companyId) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult CrtShedule(string rentfromdate, string renttodate, string advancedamout, string advancedfromdate, string advancedtodate, List<string> MonthlyInsList, List<string> MonthlyInsFromDate, List<string> MonthlyInsToDate)
        {
            int datediff1 = 0;
            float advanceblanace = 0;
            List<SHEDULE_INSTALLEMENTS> InstallementList = new List<SHEDULE_INSTALLEMENTS>();

            try
            {
                for (int i = 0; i < MonthlyInsList.Count; i++)
                {
                    SHEDULE_INSTALLEMENTS newinst = new SHEDULE_INSTALLEMENTS();
                    newinst.monthly_installement = MonthlyInsList[i].ToString();
                    newinst.from_period = MonthlyInsFromDate[i].ToString();
                    newinst.to_period = MonthlyInsToDate[i].ToString();

                    InstallementList.Add(newinst);
                }

                List<PAY_SCHEDULE_DETAILS> SheduleDetails = new List<PAY_SCHEDULE_DETAILS>();
                DateTime RfromDate = DateTime.Parse(rentfromdate);
                DateTime RtoDate = DateTime.Parse(renttodate);
                DateTime AdfromDate = DateTime.Parse(advancedfromdate);
                DateTime ADtoDate = DateTime.Parse(advancedtodate);

                for (int y = 0; y < InstallementList.Count; y++)
                {
                    DateTime RfromDateins = DateTime.Parse(InstallementList[y].from_period);
                    DateTime RtoDateins = DateTime.Parse(InstallementList[y].to_period);
                    //First Date of the month
                    //var ToDate = new DateTime(RfromDate.Year, RfromDate.Month, 1);

                    var firstDay = new DateTime(RfromDateins.Year, RfromDateins.Month, 1);
                    var lastDay = new DateTime(RfromDateins.Year, RfromDateins.Month, 1).AddMonths(1).AddDays(-1);

                    while (lastDay < RtoDateins)
                    {
                        int datediff = (int)(lastDay - RfromDateins).TotalDays;
                        int monthdiff = (int)(lastDay - firstDay).TotalDays;
                        PAY_SCHEDULE_DETAILS schedule = new PAY_SCHEDULE_DETAILS();
                        schedule.psd_due = lastDay.ToString();
                        schedule.psd_amt = ((float.Parse(InstallementList[y].monthly_installement) / monthdiff) * datediff).ToString();
                        SheduleDetails.Add(schedule);

                        firstDay = lastDay.AddDays(1);
                        RfromDateins = lastDay.AddDays(1);
                        lastDay = lastDay.AddMonths(1);

                    }
                    //final installement
                    var firstDay2 = new DateTime(lastDay.Year, lastDay.Month, 1);

                    int datediffinal = (int)(RtoDateins - RfromDateins).TotalDays;


                    datediff1 = (int)(lastDay - firstDay2).TotalDays;
                    int datediff2 = (int)(RtoDateins - firstDay2).TotalDays;
                    float mothlyins = float.Parse(InstallementList[y].monthly_installement) / datediff1;
                    PAY_SCHEDULE_DETAILS schedulefinaldate = new PAY_SCHEDULE_DETAILS();
                    schedulefinaldate.psd_due = RtoDateins.ToString();
                    schedulefinaldate.psd_amt = (mothlyins * datediffinal).ToString();
                    SheduleDetails.Add(schedulefinaldate);
                }

                var firstDay1 = new DateTime(AdfromDate.Year, AdfromDate.Month, 1);
                var lastDay1 = new DateTime(AdfromDate.Year, AdfromDate.Month, 1).AddMonths(1).AddDays(-1);
                int monthdiffadvance = GetMonthDifference(AdfromDate, ADtoDate);
                advanceblanace = (float.Parse(advancedamout));
                int count = 0;
                while (AdfromDate < ADtoDate)
                {



                    int datediff = (int)(lastDay1 - AdfromDate).TotalDays;
                    int monthdiff = (int)(lastDay1 - firstDay1).TotalDays;

                    if (monthdiffadvance != 0)
                    {
                        float advancepermonth = (float.Parse(advancedamout) / monthdiffadvance);
                        float advanceperdate = ((float.Parse(advancedamout) / monthdiffadvance) / monthdiff) * datediff;

                        if ((advanceblanace - advanceperdate) < 0) { break; }
                        SheduleDetails[count++].psd_ded_amt = advanceperdate.ToString();

                        advanceblanace = advanceblanace - advanceperdate;

                    }
                    firstDay1 = lastDay1.AddDays(1);
                    AdfromDate = lastDay1.AddDays(1);
                    lastDay1 = lastDay1.AddMonths(1);
                }

                SheduleDetails[count].psd_ded_amt = advanceblanace.ToString();


                for (int i = 0; i < SheduleDetails.Count; i++)
                {

                    SheduleDetails[i].psd_net_amt = (float.Parse(SheduleDetails[i].psd_amt) - float.Parse(SheduleDetails[i].psd_ded_amt)).ToString();
                    SheduleDetails[i].psd_line = (++i).ToString();

                }


                for (int i = 0; i < SheduleDetails.Count; i++)
                {

                    if (SheduleDetails[i].psd_amt == null)
                    {
                        SheduleDetails[i].psd_amt = "0";
                    }
                    if (SheduleDetails[i].psd_ded_amt == null)
                    {
                        SheduleDetails[i].psd_ded_amt = "0";
                    }
                    if (SheduleDetails[i].psd_net_amt == null)
                    {
                        SheduleDetails[i].psd_net_amt = "0";
                    }
                    if (SheduleDetails[i].psd_pay_amt == null)
                    {
                        SheduleDetails[i].psd_pay_amt = "";
                    }

                }

                Session["ScheduleDetails"] = SheduleDetails;

                return Json(new { success = true, login = true, data = SheduleDetails }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = true, data = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        public int GetMonthDifference(DateTime startDate, DateTime endDate)
        {
            int monthsApart = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
            return Math.Abs(monthsApart);
        }

        public JsonResult ApproveRentPayment()
        {
            string userId = HttpContext.Session["UserId"] as string;
            string companyId = HttpContext.Session["UserCompanyCode"] as string;

            if (!CHNLSVC.Security.Is_OptionPerimitted(companyId, userId, 16110))
            {
                return Json(new { success = false, login = true, msg = "Sorry, You have no permission to approve this rent payment.( Advice: Required permission code : 16110) !", type = "Info" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, login = true, msg = "1" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UpdateSCHStatus(string p_psh_no)
        {
            string userId = HttpContext.Session["UserId"] as string;
            string companyId = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            string err = "";

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(companyId) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    int documents = CHNLSVC.Finance.UPDATE_SCH_STATUS(p_psh_no, userId, out err);
                    return Json(new { success = true, login = true, data = documents }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetScheduleDetails(string p_psh_no)
        {
            try
            {
                List<PAY_SCHEDULE_DETAILS> ScheduleDetails = new List<PAY_SCHEDULE_DETAILS>();
                ScheduleDetails = CHNLSVC.Finance.getScheduleDetails(p_psh_no);

                if (ScheduleDetails.Count != 0)
                {
                    Session["ScheduleDetails"] = ScheduleDetails;
                    return Json(new { success = true, login = true, data = ScheduleDetails }, JsonRequestBehavior.AllowGet);
                }
                else {
                    return Json(new { success = false, login = true, data = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = true, data = "" }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}