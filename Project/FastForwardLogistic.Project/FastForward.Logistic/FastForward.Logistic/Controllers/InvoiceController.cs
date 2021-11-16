using CrystalDecisions.CrystalReports.Engine;
using FF.BusinessObjects;
using FF.BusinessObjects.Genaral;
using FF.BusinessObjects.Sales;
using FF.BusinessObjects.Search;
using FF.BusinessObjects.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;


namespace FastForward.Logistic.Controllers
{
    public class InvoiceController : BaseController
    {
        //
        // GET: /Invoice/
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserId"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                ViewBag.Logincompany = company;
                Session["trn_inv_det"] = null;
                Session["RecieptItemList"] = null;
                Session["totalPaidAmount"] = null;
                 Session["totalPaidAmount"]="0.0";
                 Session["RecieptItemList"] = null;
                return View();
            }
            else
            {
                return Redirect("~/Home/index");
            }
        }
        public JsonResult LoadJobServices()
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MainServices> P_list = CHNLSVC.General.GetMainServicesCodes();
                    if (P_list == null)
                    {
                        P_list = new List<MainServices>();
                    }
                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    if (P_list.Count > 0)
                    {
                        foreach (var list in P_list)
                        {
                            ComboBoxObject o1 = new ComboBoxObject();
                            o1.Text = list.fms_ser_desc;
                            o1.Value = list.fms_ser_cd;
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
                return Json(new { success = false, login = true, data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult AddItems(string Service, string Ele, string Desc, string Uom, string Units, string UnPri, string Curr, string Rate, string Total, string Remark, string Merge, string Job, string InvoiceCurr, string invparty)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                string userDefChnl = HttpContext.Session["UserDefChnl"] as string;

                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(company, userDefPro);
                    decimal _exchangRate = 0;
                    MasterExchangeRate _exc1 = CHNLSVC.Sales.GetExchangeRate(company, InvoiceCurr, DateTime.Now, _pc.Mpc_def_exrate, userDefPro);
                    _exchangRate = (_exc1 != null) ? _exc1.Mer_bnkbuy_rt : 0;
                    List<trn_inv_det> _invitem = Session["trn_inv_det"] as List<trn_inv_det>;

                    List<mst_item_tax> taxData = new List<mst_item_tax>();
                    if (userDefChnl == "WH" || userDefChnl == "TRP")
                    {
                        taxData = CHNLSVC.Sales.GetAllTaxDetails(userDefChnl, company);
                        if (userDefChnl == "TRP")
                        {
                            if (Ele != "YOC")
                            {
                                taxData = taxData.Where(a => a.TAX_TYPE_CODE != "NBT").ToList();
                            }
                            else
                            {
                                taxData = taxData.Where(a => a.TAX_TYPE_CODE != "VAT").ToList();
                                taxData = taxData.Where(a => a.TAX_TYPE_CODE != "NBT").ToList();
                            }
                        }
                    }
                    else
                    {
                        taxData = CHNLSVC.Sales.GetElementWiseTaxDetails(Ele, userDefPro, company);
                    }
                        
                    int element_count = 0;
                    try
                    {
                        //element_count = (_invitem == null) ? 0 : _invitem.Count;                        
                        bool has_nbt = taxData.Any(x => x.TAX_TYPE_CODE == "NBT");
                        bool has_vat = taxData.Any(x => x.TAX_TYPE_CODE == "VAT");
                        element_count = (has_nbt == true) ? ((has_vat == true) ? 3 : 2) : ((has_vat == true) ? 1 : 0);
                    }
                    catch { }

                    if (invparty != "" && invparty != null && (userDefChnl == "TRP" || userDefChnl == "YD"))
                    {
                        List<cus_details> customer_tax = CHNLSVC.Sales.GetCustomerTaxEx(invparty);

                        bool? is_tax = customer_tax.Select(t => t.MBE_TAX_EX).FirstOrDefault();
                        if (is_tax == true)
                        {
                            if (taxData != null)
                            {
                                taxData.Clear();
                            }
                        }

                    }
                    


                    #region
                    if (_invitem != null)
                    {
                        if (_invitem.Count > 0)
                        {

                            var maxnum = _invitem.Max(a => a.Tid_line_no);
                            maxnum = maxnum + 1;
                            trn_inv_det ob = new trn_inv_det();
                            ob.Tid_bl_d_no = "";
                            ob.Tid_bl_h_no = "";
                            ob.Tid_bl_m_no = "";
                            ob.Tid_cha_amt = Convert.ToDecimal(Total);
                            ob.Tid_cha_code = Ele;
                            ob.Tid_cha_desc = Desc;
                            ob.Tid_cha_rate = Convert.ToDecimal(UnPri);
                            ob.Tid_com_cd = company;
                            ob.Tid_curr_cd = Curr;
                            ob.Tid_doc_no = Job;
                            ob.Tid_docline = maxnum;
                            ob.Tid_ex_rate = Convert.ToDecimal(Rate);
                            ob.Tid_job_no = Job;
                            ob.Tid_line_no = maxnum;
                            ob.Tid_merge_chacode = Merge;
                            ob.Tid_qty = Convert.ToDecimal(Units);
                            ob.Tid_rmk = Remark;
                            ob.Tid_ser_cd = Service;
                            ob.Tid_unit_amnt = Convert.ToDecimal(UnPri);
                            ob.Tid_units = Uom;
                            ob.Tid_invr_merge_line = element_count;
                            _invitem.Add(ob);
                            Session["trn_inv_det"] = _invitem;


                        }
                        else
                        {
                            trn_inv_det ob = new trn_inv_det();
                            ob.Tid_bl_d_no = "";
                            ob.Tid_bl_h_no = "";
                            ob.Tid_bl_m_no = "";
                            ob.Tid_cha_amt = Convert.ToDecimal(Total);
                            ob.Tid_cha_code = Ele;
                            ob.Tid_cha_desc = Desc;
                            ob.Tid_cha_rate = Convert.ToDecimal(UnPri);
                            ob.Tid_com_cd = company;
                            ob.Tid_curr_cd = Curr;
                            ob.Tid_doc_no = Job;
                            ob.Tid_docline = 1;
                            ob.Tid_ex_rate = Convert.ToDecimal(Rate);
                            ob.Tid_job_no = Job;
                            ob.Tid_line_no = 1;
                            ob.Tid_merge_chacode = Merge;
                            ob.Tid_qty = Convert.ToDecimal(Units);
                            ob.Tid_rmk = Remark;
                            ob.Tid_ser_cd = Service;
                            ob.Tid_unit_amnt = Convert.ToDecimal(UnPri);
                            ob.Tid_units = Uom;
                            ob.Tid_invr_merge_line = element_count;
                            _invitem.Add(ob);
                            Session["trn_inv_det"] = _invitem;

                        }

                    }
                    else
                    {
                        _invitem = new List<trn_inv_det>();
                        trn_inv_det ob = new trn_inv_det();
                        ob.Tid_bl_d_no = "";
                        ob.Tid_bl_h_no = "";
                        ob.Tid_bl_m_no = "";
                        ob.Tid_cha_amt = Convert.ToDecimal(Total);
                        ob.Tid_cha_code = Ele;
                        ob.Tid_cha_desc = Desc;
                        ob.Tid_cha_rate = Convert.ToDecimal(UnPri);
                        ob.Tid_com_cd = company;
                        ob.Tid_curr_cd = Curr;
                        ob.Tid_doc_no = Job;
                        ob.Tid_docline = 1;
                        ob.Tid_ex_rate = Convert.ToDecimal(Rate);
                        ob.Tid_job_no = Job;
                        ob.Tid_line_no = 1;
                        ob.Tid_merge_chacode = Merge;
                        ob.Tid_qty = Convert.ToDecimal(Units);
                        ob.Tid_rmk = Remark;
                        ob.Tid_ser_cd = Service;
                        ob.Tid_unit_amnt = Convert.ToDecimal(UnPri);
                        ob.Tid_units = Uom;
                        ob.Tid_invr_merge_line = element_count;
                        _invitem.Add(ob);
                        Session["trn_inv_det"] = _invitem;
                    }
                    #endregion  
                    

                    /* NBT VAT */
                    //List<mst_item_tax> taxData = CHNLSVC.Sales.GetElementWiseTaxDetails(Ele, userDefPro, company);
                    #region
                    try
                    {
                        if (taxData.Count > 0)
                        {
                            bool hasNbt = taxData.Any(x => x.TAX_TYPE_CODE == "NBT");
                            if (hasNbt == true)
                            {
                                decimal rateNbt = (from vnbt in taxData
                                                   where vnbt.TAX_TYPE_CODE == "NBT"
                                                   select vnbt.TAX_RATE).FirstOrDefault();
                                decimal basedNbt = (from vnbt in taxData
                                                    where vnbt.TAX_TYPE_CODE == "NBT"
                                                    select vnbt.BASED_ON).FirstOrDefault();

                                //decimal unitPriceNbt = (rateNbt / basedNbt) * Convert.ToDecimal(UnPri) * Convert.ToDecimal(Rate);
                                decimal unitPriceNbt = (rateNbt / basedNbt) * Convert.ToDecimal(UnPri);

                                var maxnum = _invitem.Max(a => a.Tid_line_no);
                                maxnum = maxnum + 1;
                                trn_inv_det ob = new trn_inv_det();
                                ob.Tid_bl_d_no = "";
                                ob.Tid_bl_h_no = "";
                                ob.Tid_bl_m_no = "";
                                ob.Tid_cha_amt = Math.Round(Convert.ToDecimal(unitPriceNbt * Convert.ToDecimal(Units) * Convert.ToDecimal(Rate)),2); //Convert.ToDecimal(unitPriceNbt * Convert.ToDecimal(Units) * Convert.ToDecimal(Rate));
                                ob.Tid_cha_code = "NBT";
                                ob.Tid_cha_desc = "NBT";
                                ob.Tid_cha_rate = Math.Round(unitPriceNbt, 2);
                                ob.Tid_com_cd = company;
                                ob.Tid_curr_cd = Curr;
                                ob.Tid_doc_no = Job;
                                ob.Tid_docline = maxnum;
                                ob.Tid_ex_rate = Convert.ToDecimal(Rate);
                                ob.Tid_job_no = Job;
                                ob.Tid_line_no = maxnum;
                                ob.Tid_merge_chacode = Merge;
                                ob.Tid_qty = 1; // Convert.ToDecimal(Units);
                                ob.Tid_rmk = Remark;
                                ob.Tid_ser_cd = Service;
                                ob.Tid_unit_amnt = Math.Round(Convert.ToDecimal(unitPriceNbt * Convert.ToDecimal(Units)),2); // unitPriceNbt; //Math.Round(unitPriceNbt, 2); // Convert.ToDecimal(UnPri);
                                ob.Tid_units = "P/D/O"; // Uom;
                                ob.Tid_invr_merge_line = 0;// element_count;
                                if (_invitem.Any(x => x.Tid_cha_code == "NBT"))
                                {
                                    decimal cur_nbt = (from _invitemnbt in _invitem
                                                       where _invitemnbt.Tid_cha_code == "NBT"
                                                       select _invitemnbt.Tid_cha_amt).FirstOrDefault();
                                    decimal cur_unit_amnt = (from _invitemnbt in _invitem
                                                             where _invitemnbt.Tid_cha_code == "NBT"
                                                             select _invitemnbt.Tid_unit_amnt).FirstOrDefault();

                                    _invitem.Where(w => w.Tid_cha_code == "NBT").ToList()
                                        .ForEach(i =>
                                        {
                                            i.Tid_cha_amt = (cur_nbt + Convert.ToDecimal(unitPriceNbt * Convert.ToDecimal(Units) * Convert.ToDecimal(Rate)));
                                            i.Tid_unit_amnt = (cur_nbt + Convert.ToDecimal(unitPriceNbt * Convert.ToDecimal(Units) * Convert.ToDecimal(Rate))); //cur_unit_amnt + unitPriceNbt;
                                        });
                                }
                                else
                                {
                                    _invitem.Add(ob);
                                }
                                Session["trn_inv_det"] = _invitem;

                                bool hasVat = taxData.Any(x => x.TAX_TYPE_CODE == "VAT");

                                if (hasVat == true)
                                {
                                    decimal rateVat = (from vnbt in taxData
                                                       where vnbt.TAX_TYPE_CODE == "VAT"
                                                       select vnbt.TAX_RATE).FirstOrDefault();
                                    decimal basedVat = (from vnbt in taxData
                                                        where vnbt.TAX_TYPE_CODE == "VAT"
                                                        select vnbt.BASED_ON).FirstOrDefault();

                                    //decimal unitPriceVat = (rateVat / basedVat) * ((Convert.ToDecimal(UnPri) * Convert.ToDecimal(Rate)) + unitPriceNbt);
                                    decimal unitPriceVat = (rateVat / basedVat) * ((Convert.ToDecimal(UnPri) ) + unitPriceNbt);

                                    var maxnum_vat = _invitem.Max(a => a.Tid_line_no);
                                    maxnum_vat = maxnum_vat + 1;
                                    trn_inv_det obVat = new trn_inv_det();
                                    obVat.Tid_bl_d_no = "";
                                    obVat.Tid_bl_h_no = "";
                                    obVat.Tid_bl_m_no = "";
                                    obVat.Tid_cha_amt = Math.Round(Convert.ToDecimal(unitPriceVat * Convert.ToDecimal(Units) * Convert.ToDecimal(Rate)),2); //Convert.ToDecimal(unitPriceVat * Convert.ToDecimal(Units) * Convert.ToDecimal(Rate));
                                    obVat.Tid_cha_code = "VAT";
                                    obVat.Tid_cha_desc = "VAT";
                                    obVat.Tid_cha_rate = Math.Round(unitPriceVat, 2);
                                    obVat.Tid_com_cd = company;
                                    obVat.Tid_curr_cd = Curr;
                                    obVat.Tid_doc_no = Job;
                                    obVat.Tid_docline = maxnum_vat;
                                    obVat.Tid_ex_rate = Convert.ToDecimal(Rate);
                                    obVat.Tid_job_no = Job;
                                    obVat.Tid_line_no = maxnum_vat;
                                    obVat.Tid_merge_chacode = Merge;
                                    obVat.Tid_qty = 1; // Convert.ToDecimal(Units);
                                    obVat.Tid_rmk = Remark;
                                    obVat.Tid_ser_cd = Service;
                                    obVat.Tid_unit_amnt = Math.Round(Convert.ToDecimal(unitPriceVat * Convert.ToDecimal(Units)),2); // unitPriceVat; //Math.Round(unitPriceVat, 2); // Convert.ToDecimal(UnPri);
                                    obVat.Tid_units = "P/D/O"; // Uom;
                                    obVat.Tid_invr_merge_line = 0;// element_count;
                                    if (_invitem.Any(x => x.Tid_cha_code == "VAT"))
                                    {
                                        decimal cur_vat = (from _invitemnbt in _invitem
                                                           where _invitemnbt.Tid_cha_code == "VAT"
                                                           select _invitemnbt.Tid_cha_amt).FirstOrDefault();
                                        decimal cur_unit_amnt = (from _invitemnbt in _invitem
                                                                 where _invitemnbt.Tid_cha_code == "VAT"
                                                                 select _invitemnbt.Tid_unit_amnt).FirstOrDefault();

                                        _invitem.Where(w => w.Tid_cha_code == "VAT").ToList()
                                            .ForEach(i =>
                                            {
                                                i.Tid_cha_amt = (cur_vat + Convert.ToDecimal(unitPriceVat * Convert.ToDecimal(Units) * Convert.ToDecimal(Rate)));
                                                i.Tid_unit_amnt = (cur_vat + Convert.ToDecimal(unitPriceVat * Convert.ToDecimal(Units) * Convert.ToDecimal(Rate))); //cur_unit_amnt + Math.Round(unitPriceVat, 2);
                                            });
                                    }
                                    else
                                    {
                                        _invitem.Add(obVat);
                                    }
                                    Session["trn_inv_det"] = _invitem;
                                }


                            }
                            else
                            {
                                bool hasVat = taxData.Any(x => x.TAX_TYPE_CODE == "VAT");

                                if (hasVat == true)
                                {
                                    decimal rateVat = (from vnbt in taxData
                                                       where vnbt.TAX_TYPE_CODE == "VAT"
                                                       select vnbt.TAX_RATE).FirstOrDefault();
                                    decimal basedVat = (from vnbt in taxData
                                                        where vnbt.TAX_TYPE_CODE == "VAT"
                                                        select vnbt.BASED_ON).FirstOrDefault();

                                    //decimal unitPriceVat = (rateVat / basedVat) * (Convert.ToDecimal(UnPri) * Convert.ToDecimal(Rate));
                                    decimal unitPriceVat = (rateVat / basedVat) * (Convert.ToDecimal(UnPri));

                                    var maxnum_vat = _invitem.Max(a => a.Tid_line_no);
                                    maxnum_vat = maxnum_vat + 1;
                                    trn_inv_det obVat = new trn_inv_det();
                                    obVat.Tid_bl_d_no = "";
                                    obVat.Tid_bl_h_no = "";
                                    obVat.Tid_bl_m_no = "";
                                    obVat.Tid_cha_amt = Math.Round(Convert.ToDecimal(unitPriceVat * Convert.ToDecimal(Units) * Convert.ToDecimal(Rate)), 2); //Convert.ToDecimal(unitPriceVat * Convert.ToDecimal(Units) * Convert.ToDecimal(Rate));
                                    obVat.Tid_cha_code = "VAT";
                                    obVat.Tid_cha_desc = "VAT";
                                    obVat.Tid_cha_rate = Math.Round(unitPriceVat, 2);
                                    obVat.Tid_com_cd = company;
                                    obVat.Tid_curr_cd = Curr;
                                    obVat.Tid_doc_no = Job;
                                    obVat.Tid_docline = maxnum_vat;
                                    obVat.Tid_ex_rate = Convert.ToDecimal(Rate);
                                    obVat.Tid_job_no = Job;
                                    obVat.Tid_line_no = maxnum_vat;
                                    obVat.Tid_merge_chacode = Merge;
                                    obVat.Tid_qty = 1; // Convert.ToDecimal(Units);
                                    obVat.Tid_rmk = Remark;
                                    obVat.Tid_ser_cd = Service;
                                    obVat.Tid_unit_amnt = Math.Round(Convert.ToDecimal(unitPriceVat * Convert.ToDecimal(Units)),2); // unitPriceVat; //Math.Round(unitPriceVat, 2); // Convert.ToDecimal(UnPri);
                                    obVat.Tid_units = "P/D/O"; // Uom;
                                    obVat.Tid_invr_merge_line = 0;// element_count;
                                    if (_invitem.Any(x => x.Tid_cha_code == "VAT"))
                                    {
                                        decimal cur_vat = (from _invitemnbt in _invitem
                                                           where _invitemnbt.Tid_cha_code == "VAT"
                                                           select _invitemnbt.Tid_cha_amt).FirstOrDefault();
                                        decimal cur_unit_amnt = (from _invitemnbt in _invitem
                                                                 where _invitemnbt.Tid_cha_code == "VAT"
                                                                 select _invitemnbt.Tid_unit_amnt).FirstOrDefault();

                                        _invitem.Where(w => w.Tid_cha_code == "VAT").ToList()
                                            .ForEach(i =>
                                            {
                                                i.Tid_cha_amt = (cur_vat + Convert.ToDecimal(unitPriceVat * Convert.ToDecimal(Units) * Convert.ToDecimal(Rate)));
                                                i.Tid_unit_amnt = (cur_vat + Convert.ToDecimal(unitPriceVat * Convert.ToDecimal(Units) * Convert.ToDecimal(Rate))); //cur_unit_amnt + Math.Round(unitPriceVat, 2);
                                            });
                                    }
                                    else
                                    {
                                        _invitem.Add(obVat);
                                    }
                                    Session["trn_inv_det"] = _invitem;
                                }
                            }
                        }
                    }
                    #endregion
                    catch { }
                    decimal total = 0;
                    //total = _invitem.Sum(a => a.Tid_cha_rate * a.Tid_qty * a.Tid_ex_rate) / _exchangRate;
                    total = _invitem.Sum(a => Math.Round(a.Tid_cha_amt, 2));
                    //total = Math.Round(total, 2);
                    total = Math.Truncate(100 * total) / 100; // Request to add by vajira. Rounding commented and truncate
                    return Json(new { success = true, login = true, data = _invitem, total = total }, JsonRequestBehavior.AllowGet);

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
        public JsonResult RemoveInvItem(string linenumber, string InvoiceCurr)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string userDefChnl = HttpContext.Session["UserDefChnl"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<trn_inv_det> _invdet = new List<trn_inv_det>();
                MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(company, userDefPro);
                decimal _exchangRate = 0;
                MasterExchangeRate _exc1 = CHNLSVC.Sales.GetExchangeRate(company, InvoiceCurr, DateTime.Now, _pc.Mpc_def_exrate, userDefPro);
                _exchangRate = (_exc1 != null) ? _exc1.Mer_bnkbuy_rt : 0;
                if (Session["trn_inv_det"] != null)
                {
                    _invdet = Session["trn_inv_det"] as List<trn_inv_det>;
                }
                else
                {
                    _invdet = new List<trn_inv_det>();

                }
                var itemToRemove = _invdet.First(r => r.Tid_line_no == Convert.ToInt16(linenumber));

                int inv_set = itemToRemove.Tid_invr_merge_line;
                decimal inv_qty = itemToRemove.Tid_qty;
                decimal inv_price = itemToRemove.Tid_unit_amnt;
                decimal inv_rate = itemToRemove.Tid_ex_rate;

                //List<mst_item_tax> taxData = CHNLSVC.Sales.GetElementWiseTaxDetails(itemToRemove.Tid_cha_code, userDefPro, company);

                List<mst_item_tax> taxData = new List<mst_item_tax>();
                if (userDefChnl == "WH" || userDefChnl == "TRP")
                {
                    taxData = CHNLSVC.Sales.GetAllTaxDetails(userDefChnl, company);
                }
                else
                {
                    taxData = CHNLSVC.Sales.GetElementWiseTaxDetails(itemToRemove.Tid_cha_code, userDefPro, company);
                }
                               
                
                if (taxData != null)
                {
                    int countNBTVAT = (from n in _invdet
                                       where n.Tid_invr_merge_line == 3
                                       select n).Count();
                    int countNBT = (from n in _invdet
                                    where n.Tid_invr_merge_line == 2
                                    select n).Count();
                    int countVAT = (from n in _invdet
                                    where n.Tid_invr_merge_line == 1
                                    select n).Count();

                    decimal rateNbt = (from vnbt in taxData
                                       where vnbt.TAX_TYPE_CODE == "NBT"
                                       select vnbt.TAX_RATE).FirstOrDefault();
                    decimal basedNbt = (from vnbt in taxData
                                        where vnbt.TAX_TYPE_CODE == "NBT"
                                        select vnbt.BASED_ON).FirstOrDefault();

                    decimal rateVat = (from vnbt in taxData
                                       where vnbt.TAX_TYPE_CODE == "VAT"
                                       select vnbt.TAX_RATE).FirstOrDefault();
                    decimal basedVat = (from vnbt in taxData
                                        where vnbt.TAX_TYPE_CODE == "VAT"
                                        select vnbt.BASED_ON).FirstOrDefault();

                    /* NBT and VAT Amounts */
                    decimal cur_nbt = (from _invitemnbt in _invdet
                                       where _invitemnbt.Tid_cha_code == "NBT"
                                       select _invitemnbt.Tid_cha_amt).FirstOrDefault();
                    decimal cur_unit_amnt = (from _invitemnbt in _invdet
                                             where _invitemnbt.Tid_cha_code == "NBT"
                                             select _invitemnbt.Tid_unit_amnt).FirstOrDefault();

                    decimal cur_vat = (from _invitemnbt in _invdet
                                       where _invitemnbt.Tid_cha_code == "VAT"
                                       select _invitemnbt.Tid_cha_amt).FirstOrDefault();
                    decimal cur_unit_amnt_vat = (from _invitemnbt in _invdet
                                                 where _invitemnbt.Tid_cha_code == "VAT"
                                                 select _invitemnbt.Tid_unit_amnt).FirstOrDefault();


                    if (inv_set == 3)
                    {
                        decimal nbtVal = ((rateNbt / basedNbt) * Convert.ToDecimal(inv_price)) * inv_qty * inv_rate;
                        decimal vatVal = ((rateVat / basedVat) * (Convert.ToDecimal(inv_price) + ((rateNbt / basedNbt) * Convert.ToDecimal(inv_price)))) * inv_qty * inv_rate;

                        if (countNBTVAT > 1)
                        {
                            _invdet.Where(w => w.Tid_cha_code == "NBT").ToList() 
                                .ForEach(i =>
                                {
                                    i.Tid_cha_amt = (cur_nbt - Convert.ToDecimal(nbtVal));
                                    i.Tid_unit_amnt = cur_unit_amnt - (nbtVal / inv_qty);
                                });

                            _invdet.Where(w => w.Tid_cha_code == "VAT").ToList()
                                .ForEach(i =>
                                {
                                    i.Tid_cha_amt = (cur_vat - Convert.ToDecimal(vatVal));
                                    i.Tid_unit_amnt = cur_unit_amnt_vat - (vatVal / inv_qty);
                                });
                        }
                        else if (countNBTVAT == 1)
                        {
                            if (countNBT > 1)
                            {
                                _invdet.Where(w => w.Tid_cha_code == "NBT").ToList()
                                .ForEach(i =>
                                {
                                    i.Tid_cha_amt = (cur_nbt - Convert.ToDecimal(nbtVal));
                                    i.Tid_unit_amnt = cur_unit_amnt - (nbtVal / inv_qty);
                                });
                            }
                            else
                            {
                                _invdet.RemoveAll(x => x.Tid_cha_code == "NBT");
                            }
                            if (countVAT > 1)
                            {
                                _invdet.Where(w => w.Tid_cha_code == "VAT").ToList()
                                .ForEach(i =>
                                {
                                    i.Tid_cha_amt = (cur_vat - Convert.ToDecimal(vatVal));
                                    i.Tid_unit_amnt = cur_unit_amnt_vat - (vatVal / inv_qty);
                                });
                            }
                            else
                            {
                                _invdet.RemoveAll(x => x.Tid_cha_code == "VAT");
                            }

                        }
                    }
                    else if (inv_set == 2)
                    {
                        decimal nbtVal = ((rateNbt / basedNbt) * Convert.ToDecimal(inv_price)) * inv_qty * inv_rate;

                        if (countNBTVAT > 0)
                        {
                            _invdet.Where(w => w.Tid_cha_code == "NBT").ToList()
                                .ForEach(i =>
                                {
                                    i.Tid_cha_amt = (cur_nbt - Convert.ToDecimal(nbtVal));
                                    i.Tid_unit_amnt = cur_unit_amnt - (nbtVal / inv_qty);
                                });
                        }
                        else
                        {
                            if (countNBT > 1)
                            {
                                _invdet.Where(w => w.Tid_cha_code == "NBT").ToList()
                                .ForEach(i =>
                                {
                                    i.Tid_cha_amt = (cur_nbt - Convert.ToDecimal(nbtVal));
                                    i.Tid_unit_amnt = cur_unit_amnt - (nbtVal / inv_qty);
                                });
                            }
                            else if (countNBT == 1)
                            {
                                _invdet.RemoveAll(x => x.Tid_cha_code == "NBT");
                            }
                        }
                    }
                    else if (inv_set == 1)
                    {
                        decimal vatVal = ((rateVat / basedVat) * (Convert.ToDecimal(inv_price))) * inv_qty * inv_rate;

                        if (countNBTVAT > 0)
                        {
                            _invdet.Where(w => w.Tid_cha_code == "VAT").ToList()
                                .ForEach(i =>
                                {
                                    i.Tid_cha_amt = (cur_vat - Convert.ToDecimal(vatVal));
                                    i.Tid_unit_amnt = cur_unit_amnt_vat - (vatVal / inv_qty);
                                });
                        }
                        else
                        {
                            if (countVAT > 1)
                            {
                                _invdet.Where(w => w.Tid_cha_code == "VAT").ToList()
                                .ForEach(i =>
                                {
                                    i.Tid_cha_amt = (cur_vat - Convert.ToDecimal(vatVal));
                                    i.Tid_unit_amnt = cur_unit_amnt_vat - (vatVal / inv_qty);
                                });
                            }
                            else if (countVAT == 1)
                            {
                                _invdet.RemoveAll(x => x.Tid_cha_code == "VAT");
                            }
                        } 
                    }
                }
                _invdet.Remove(itemToRemove);
                //_invdet.RemoveAll(x => x.Tid_invr_merge_line == inv_set);
                Session["trn_inv_det"] = _invdet;
                decimal total = 0;
                //total = _invdet.Sum(a => a.Tid_cha_rate * a.Tid_qty * a.Tid_ex_rate) / _exchangRate;
                //total = _invdet.Sum(a => a.Tid_cha_amt);
                //total = Math.Round(total, 2);

                total = _invdet.Sum(a => Math.Round(a.Tid_cha_amt, 2));
                //total = Math.Round(total, 2);
                total = Math.Truncate(100 * total) / 100; // Request to add by vajira. Rounding commented and truncate
                return Json(new { success = true, login = true, data = _invdet, total = total }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SaveInvoice(trn_inv_hdr _hdr)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string err = "";
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<InvoiceCom> has_sun_up_restrict = CHNLSVC.Sales.GetSunUpRestrictStatus(company, userDefPro, _hdr.Tih_inv_dt.Date);
                int has_sunup = 0; // has_sun_up_restrict.Select(s => s.HAS_DATE).FirstOrDefault();
                if (has_sunup == 1)
                {
                    return Json(new { success = false, login = true, Type = "Info", msg = "Selected date is not valid due to Sun Upload" }, JsonRequestBehavior.AllowGet);
                }

                List<InvoiceCom> num_of_backdates = CHNLSVC.Sales.GetNumOfBackdates(company, userDefPro);
                int numof_bkdate = num_of_backdates.Select(b => b.NUM_OF_BKDATES).FirstOrDefault();
                if(_hdr.Tih_inv_dt.Date<DateTime.Now.Date){
                    if (CHNLSVC.Security.Is_OptionPerimitted(company, userId.ToUpper(), 1013)) // Invoice Backdate Permission
                    {
                        int date_diff = (int)(DateTime.Now.Date - _hdr.Tih_inv_dt.Date).TotalDays;
                        if (numof_bkdate < date_diff)
                        {
                            return Json(new { success = false, login = true, Type = "Info", msg = "" + numof_bkdate +" Days of maximum backdate exceeded" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else{
                        return Json(new { success = false, login = true, Type = "Info", msg = "No permission to select a backdate" }, JsonRequestBehavior.AllowGet);
                    }
                }


                List<trn_inv_det> _invdet = new List<trn_inv_det>();
                if (Session["trn_inv_det"] != null)
                {
                    _invdet = Session["trn_inv_det"] as List<trn_inv_det>;
                }
                else
                {
                    return Json(new { success = false, login = true, msg = "Please Add Item Details", Type = "Info" }, JsonRequestBehavior.AllowGet);

                }
                List<trn_bl_header> _bl_hdr = CHNLSVC.General.GetBLHdr(_hdr.Tih_bl_h_no, company);
                if (_bl_hdr != null)
                {
                    if (_bl_hdr.Count>0)
                    {
                        _hdr.Tih_bl_d_no = _bl_hdr.First().Bl_d_doc_no;
                        _hdr.Tih_bl_m_no = _bl_hdr.First().Bl_m_doc_no;
                    }
                }
                _hdr.Tih_pc_cd = userDefPro;
                _hdr.Tih_com_cd = company;
                _hdr.Tih_cre_by = userId;
                _hdr.Tih_cre_dt = DateTime.Now;
                _hdr.Tih_mod_by = userId;
                _hdr.Tih_mod_dt = DateTime.Now;
                _hdr.Tih_inv_status = "P";
                _hdr.Tih_doc_type = "INV";
                _hdr.Tih_inv_tp = "CRED";
                _hdr.Tih_inv_sub_tp = "SA";
                List<RecieptItem> _recieptItem = new List<RecieptItem>();
                List<RecieptItemTBS> _recieptItemtbs = (List<RecieptItemTBS>)Session["RecieptItemList"];
                RecieptHeader _ReceiptHeader = new RecieptHeader();
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
                    _ReceiptHeader.Sar_receipt_date = Convert.ToDateTime(_hdr.Tih_inv_dt).Date;
                    _ReceiptHeader.Sar_direct = true;
                    _ReceiptHeader.Sar_acc_no = "";
                    _ReceiptHeader.Sar_is_oth_shop = false;
                    _ReceiptHeader.Sar_oth_sr = "";
                    _ReceiptHeader.Sar_profit_center_cd = userDefPro;
                    _ReceiptHeader.Sar_debtor_cd = _hdr.Tih_cus_cd;
                    _ReceiptHeader.Sar_debtor_name = _hdr.Tih_acc_cus_name;
                    _ReceiptHeader.Sar_debtor_add_1 = _hdr.Tih_acc_cus_add1;
                    _ReceiptHeader.Sar_debtor_add_2 = _hdr.Tih_acc_cus_add2;
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
                MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
                _invoiceAuto.Aut_cate_cd = Session["UserDefProf"].ToString();
                _invoiceAuto.Aut_cate_tp = "PC";
                _invoiceAuto.Aut_direction = 1;
                _invoiceAuto.Aut_moduleid = _hdr.Tih_inv_tp;
                _invoiceAuto.Aut_number = 0;
                _invoiceAuto.Aut_start_char = Session["UserDefProf"].ToString() + "-" + _hdr.Tih_inv_tp + "-";
                _invoiceAuto.Aut_year = Convert.ToDateTime(_hdr.Tih_inv_dt).Year;

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
                        _receiptAuto.Aut_start_char = "DIR";
                        _receiptAuto.Aut_year = Convert.ToDateTime(_hdr.Tih_inv_dt).Year;
                    }

                MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(company, userDefPro);
                decimal _exchangRate = 0;
                MasterExchangeRate _exc1 = CHNLSVC.Sales.GetExchangeRate(company, _hdr.Tih_inv_curr, DateTime.Now, _pc.Mpc_def_exrate, userDefPro);
                _exchangRate = (_exc1 != null) ? _exc1.Mer_bnkbuy_rt : 0;
                decimal total = 0;
                //total = _invdet.Sum(a => a.Tid_cha_rate * a.Tid_qty * a.Tid_ex_rate) / _exchangRate;
                //total = Math.Round(total, 2);
                total = _invdet.Sum(a => Math.Round(a.Tid_cha_amt, 2));
                total = Math.Truncate(100 * total) / 100; // Request to add by vajira. Rounding commented and truncate
                _hdr.Tih_inv_amt = total;
                _hdr.Tih_tot_amt =Math.Round( total * _exchangRate,2);

                int effect = CHNLSVC.Sales.SaveJobInvoice(_hdr, _invdet, _ReceiptHeader, _recieptItem, _invoiceAuto, _receiptAuto, out err);
                if (effect >0)
                {
                    return Json(new { success = true, login = true, msg = "Successfully Saved : "+ err }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (err.Contains("(FTW.CHK_BAL)"))
                    {
                        err = "Cannot exceed the invoice balance";
                    }
                    return Json(new { success = false, login = true, msg = err, Type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult validatePrint(string Inv)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string UserDefChnl = HttpContext.Session["UserDefChnl"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (Inv == "" || Inv == null)
                    {
                        return Json(new { success = false, login = true, msg = "Invalid invoice number", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    string error = "";

                    if (CHNLSVC.Security.Is_OptionPerimitted(company, userId.ToUpper(), 1012))
                    {
                        DataTable request = CHNLSVC.Sales.Inv_Details(Inv, company, UserDefChnl, out error);
                        if (error == "")
                        {
                            if (request.Rows[0]["Tih_inv_no"].ToString() != null)
                            {
                                return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = "Invalid invoice number", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = error, type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { success = true, login = true, msg = "No permission to print invoice", type = "Info" }, JsonRequestBehavior.AllowGet);
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
        
        public ActionResult Print(string Inv, string InvType)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string userDefChnl = HttpContext.Session["UserDefChnl"] as string;
            

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                string error = "";
                DataTable request = CHNLSVC.Sales.Inv_Details(Inv, company, userDefChnl, out error);
                if (error == "" && request.Rows.Count > 0)
                {
                    if (request.Rows[0]["Tih_inv_no"].ToString() != null)
                    {
                        string reportName = "";
                        string fileName = "";
                        string ReportPath = System.Configuration.ConfigurationManager.AppSettings["ReportPath"].ToString();
                        string report = "";

                        //trn_inv_hdr rptData = CHNLSVC.Sales.Inv_Details(Inv, company, out error);
                        DataTable comData = new DataTable("comdata");
                        comData = CHNLSVC.Sales.getCompanyDetailsBycd(company);

                        //ReportDocument rd = new ReportDocument();

                        //reportName = InvType == "Half" ? "rpt_InvoiceHalf.rpt" : InvType == "FullWOLogo" ? "rpt_InvoiceWithoutLogo.rpt" : "rpt_Invoice.rpt";
                        //fileName = InvType == "Half" ? "Invoice Half.pdf" : InvType == "FullWOLogo" ? "Invoice FullWOLogo.pdf" : "Invoice Full.pdf";
                        //report = ReportPath + "\\" + reportName;
                        //rd.Load(report);
                        //rd.Database.Tables["InvoiceFull"].SetDataSource(request);
                        //rd.Database.Tables["COMPANY"].SetDataSource(comData);

                        //Response.Buffer = false;
                        //Response.ClearContent();
                        //Response.ClearHeaders();
                        fileName = "Invoice Full.pdf";
                        ReportDocument rd = new ReportDocument();
                        rd.Load(Path.Combine(Server.MapPath("~/Reports"), "rpt_Invoice.rpt"));

                        rd.Database.Tables["InvoiceFull"].SetDataSource(request);
                        rd.Database.Tables["COMPANY"].SetDataSource(comData);

                        Response.Buffer = false;
                        Response.ClearContent();
                        Response.ClearHeaders();


                        //Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                        //stream.Seek(0, SeekOrigin.Begin);
                        //return File(stream, "application/pdf", "CustomerList.pdf"); 

                        try
                        {
                            this.Response.Clear();
                            this.Response.ContentType = "application/pdf";
                            Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName);
                            int reprinted = CHNLSVC.Sales.setInvoicePrintedStatus(Inv, company);
                            //return File(rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), "application/pdf");

                            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                            stream.Seek(0, SeekOrigin.Begin);
                            rd.Close();
                            rd.Dispose();
                            return File(stream, "application/pdf"); 

                        }
                        catch (Exception ex)
                        {
                            CHNLSVC.General.SaveReportErrorLog("rpt_Invoice", "rpt_Invoice", ex.Message, Session["UserID"].ToString());
                            throw;
                        }
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Invalid request number", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = true, msg = error, type = "Info" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Redirect("~/Login");
            }
        }

        public ActionResult Preview(string Inv, string InvType)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string userDefChnl = HttpContext.Session["UserDefChnl"] as string;


            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                string error = "";
                DataTable request = CHNLSVC.Sales.Inv_Details(Inv, company, userDefChnl, out error);
                if (error == "" && request.Rows.Count > 0)
                {
                    if (request.Rows[0]["Tih_inv_no"].ToString() != null)
                    {
                        string reportName = "";
                        string fileName = "";
                        string ReportPath = System.Configuration.ConfigurationManager.AppSettings["ReportPath"].ToString();
                        string report = "";

                        DataTable comData = new DataTable("comdata");
                        comData = CHNLSVC.Sales.getCompanyDetailsBycd(company);
                        //ReportDocument rd = new ReportDocument();
                        ////trn_inv_hdr rptData = CHNLSVC.Sales.Inv_Details(Inv, company, out error);

                        //reportName = InvType == "Half" ? "rpt_InvoiceHalf.rpt" : InvType == "FullWOLogo" ? "rpt_InvoiceWithoutLogo.rpt" : "rpt_Invoice_preview.rpt";
                        //fileName = InvType == "Half" ? "Invoice Half.pdf" : InvType == "FullWOLogo" ? "Invoice FullWOLogo.pdf" : "Invoice Full Preview.pdf";
                        //report = ReportPath + "\\" + reportName;
                        //rd.Load(report);
                        //rd.Database.Tables["InvoiceFull"].SetDataSource(request);
                        //rd.Database.Tables["COMPANY"].SetDataSource(comData);



                        //Response.Buffer = false;
                        //Response.ClearContent();
                        //Response.ClearHeaders();

                        fileName = "Invoice Full Preview.pdf";
                        ReportDocument rd = new ReportDocument();
                        rd.Load(Path.Combine(Server.MapPath("~/Reports"), "rpt_Invoice_preview.rpt"));

                        rd.Database.Tables["InvoiceFull"].SetDataSource(request);
                        rd.Database.Tables["COMPANY"].SetDataSource(comData);

                        Response.Buffer = false;
                        Response.ClearContent();
                        Response.ClearHeaders();


                        //Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                        //stream.Seek(0, SeekOrigin.Begin);
                        //return File(stream, "application/pdf", "CustomerList.pdf"); 
                        try
                        {
                            this.Response.Clear();
                            this.Response.ContentType = "application/pdf";
                            Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName);
                            //int reprinted = CHNLSVC.Sales.setInvoicePrintedStatus(Inv, company);
                            //return File(rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), "application/pdf");
                            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                            stream.Seek(0, SeekOrigin.Begin);
                            rd.Close();
                            rd.Dispose();
                            return File(stream, "application/pdf"); 

                        }
                        catch (Exception ex)
                        {
                            CHNLSVC.General.SaveReportErrorLog("rpt_Invoice_preview", "rpt_Invoice_preview", ex.Message, Session["UserID"].ToString());
                            throw;
                        }
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Invalid request number", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = true, msg = error, type = "Info" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Redirect("~/Login");
            }
        }

        public ActionResult CreditPrint(string Inv, string InvType)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string userDefChnl = HttpContext.Session["UserDefChnl"] as string;


            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                string error = "";
                DataTable request = CHNLSVC.Sales.Inv_Details(Inv, company, userDefChnl, out error);
                if (error == "" && request.Rows.Count > 0)
                {
                    if (request.Rows[0]["Tih_inv_no"].ToString() != null)
                    {
                        //string reportName = "";
                        string fileName = "";
                        string ReportPath = System.Configuration.ConfigurationManager.AppSettings["ReportPath"].ToString();
                        //string report = "";
                        DataTable comData = new DataTable("comdata");
                        comData = CHNLSVC.Sales.getCompanyDetailsBycd(company);

                        //ReportDocument rd = new ReportDocument();
                        ////trn_inv_hdr rptData = CHNLSVC.Sales.Inv_Details(Inv, company, out error);

                        //reportName = InvType == "Half" ? "rpt_InvoiceHalf.rpt" : InvType == "FullWOLogo" ? "rpt_InvoiceWithoutLogo.rpt" : "rpt_CreditNote.rpt";
                        //fileName = InvType == "Half" ? "Invoice Half.pdf" : InvType == "FullWOLogo" ? "Invoice FullWOLogo.pdf" : "Invoice Full.pdf";
                        //report = ReportPath + "\\" + reportName;
                        //rd.Load(report);
                        //rd.Database.Tables["InvoiceFull"].SetDataSource(request);
                        //rd.Database.Tables["COMPANY"].SetDataSource(comData);



                        //Response.Buffer = false;
                        //Response.ClearContent();
                        //Response.ClearHeaders();
                        fileName = "Credit Note.pdf";
                        ReportDocument rd = new ReportDocument();
                        rd.Load(Path.Combine(Server.MapPath("~/Reports"), "rpt_CreditNote.rpt"));

                        rd.Database.Tables["InvoiceFull"].SetDataSource(request);
                        rd.Database.Tables["COMPANY"].SetDataSource(comData);

                        Response.Buffer = false;
                        Response.ClearContent();
                        Response.ClearHeaders();


                        //Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                        //stream.Seek(0, SeekOrigin.Begin);
                        //return File(stream, "application/pdf", "CustomerList.pdf"); 

                        try
                        {
                            this.Response.Clear();
                            this.Response.ContentType = "application/pdf";
                            Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName);
                            int reprinted = CHNLSVC.Sales.setInvoicePrintedStatus(Inv, company);
                            //return File(rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), "application/pdf");

                            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                            stream.Seek(0, SeekOrigin.Begin);
                            rd.Close();
                            rd.Dispose();
                            return File(stream, "application/pdf"); 
                        }
                        catch (Exception ex)
                        {
                            CHNLSVC.General.SaveReportErrorLog("rpt_CreditNote", "rpt_CreditNote", ex.Message, Session["UserID"].ToString());
                            throw;
                        }
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Invalid request number", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = true, msg = error, type = "Info" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Redirect("~/Login");
            }
        }

        public ActionResult CreditPrintPreview(string Inv, string InvType)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string userDefChnl = HttpContext.Session["UserDefChnl"] as string;


            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                string error = "";
                DataTable request = CHNLSVC.Sales.Inv_Details(Inv, company, userDefChnl, out error);
                if (error == "" && request.Rows.Count > 0)
                {
                    if (request.Rows[0]["Tih_inv_no"].ToString() != null)
                    {
                        string reportName = "";
                        string fileName = "";
                        string ReportPath = System.Configuration.ConfigurationManager.AppSettings["ReportPath"].ToString();
                        string report = "";
                        DataTable comData = new DataTable("comdata");
                        comData = CHNLSVC.Sales.getCompanyDetailsBycd(company);

                        //ReportDocument rd = new ReportDocument();
                        ////trn_inv_hdr rptData = CHNLSVC.Sales.Inv_Details(Inv, company, out error);

                        //reportName = InvType == "Half" ? "rpt_InvoiceHalf.rpt" : InvType == "FullWOLogo" ? "rpt_InvoiceWithoutLogo.rpt" : "rpt_CreditNote_preview.rpt";
                        //fileName = InvType == "Half" ? "Invoice Half.pdf" : InvType == "FullWOLogo" ? "Invoice FullWOLogo.pdf" : "Invoice Full.pdf";
                        //report = ReportPath + "\\" + reportName;
                        //rd.Load(report);
                        //rd.Database.Tables["InvoiceFull"].SetDataSource(request);
                        //rd.Database.Tables["COMPANY"].SetDataSource(comData);



                        //Response.Buffer = false;
                        //Response.ClearContent();
                        //Response.ClearHeaders();

                        fileName = "Credit Note Preview.pdf";
                        ReportDocument rd = new ReportDocument();
                        rd.Load(Path.Combine(Server.MapPath("~/Reports"), "rpt_CreditNote_preview.rpt"));

                        rd.Database.Tables["InvoiceFull"].SetDataSource(request);
                        rd.Database.Tables["COMPANY"].SetDataSource(comData);

                        Response.Buffer = false;
                        Response.ClearContent();
                        Response.ClearHeaders();


                        //Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                        //stream.Seek(0, SeekOrigin.Begin);
                        //return File(stream, "application/pdf", "CustomerList.pdf"); 

                        try
                        {
                            this.Response.Clear();
                            this.Response.ContentType = "application/pdf";
                            Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName);
                            //int reprinted = CHNLSVC.Sales.setInvoicePrintedStatus(Inv, company);
                            //return File(rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), "application/pdf");

                            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                            stream.Seek(0, SeekOrigin.Begin);
                            rd.Close();
                            rd.Dispose();
                            return File(stream, "application/pdf"); 
                        }
                        catch (Exception ex)
                        {
                            CHNLSVC.General.SaveReportErrorLog("rpt_CreditNote_preview", "rpt_CreditNote_preview", ex.Message, Session["UserID"].ToString());
                            throw;
                        }
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Invalid request number", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = true, msg = error, type = "Info" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Redirect("~/Login");
            }
        }

        public ActionResult DebitPrint(string Inv, string InvType)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string userDefChnl = HttpContext.Session["UserDefChnl"] as string;


            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                string error = "";
                DataTable request = CHNLSVC.Sales.Inv_Details(Inv, company, userDefChnl, out error);
                if (error == "" && request.Rows.Count > 0)
                {
                    if (request.Rows[0]["Tih_inv_no"].ToString() != null)
                    {
                        string reportName = "";
                        string fileName = "";
                        string ReportPath = System.Configuration.ConfigurationManager.AppSettings["ReportPath"].ToString();
                        string report = "";

                        DataTable comData = new DataTable("comdata");
                        comData = CHNLSVC.Sales.getCompanyDetailsBycd(company);
                        //ReportDocument rd = new ReportDocument();
                        ////trn_inv_hdr rptData = CHNLSVC.Sales.Inv_Details(Inv, company, out error);

                        //reportName = InvType == "Half" ? "rpt_InvoiceHalf.rpt" : InvType == "FullWOLogo" ? "rpt_InvoiceWithoutLogo.rpt" : "rpt_DebitNote.rpt";
                        //fileName = InvType == "Half" ? "Invoice Half.pdf" : InvType == "FullWOLogo" ? "Invoice FullWOLogo.pdf" : "Invoice Full.pdf";
                        //report = ReportPath + "\\" + reportName;
                        //rd.Load(report);
                        //rd.Database.Tables["InvoiceFull"].SetDataSource(request);
                        //rd.Database.Tables["COMPANY"].SetDataSource(comData);




                        //Response.Buffer = false;
                        //Response.ClearContent();
                        //Response.ClearHeaders();

                        fileName = "Debit Note.pdf";
                        ReportDocument rd = new ReportDocument();
                        rd.Load(Path.Combine(Server.MapPath("~/Reports"), "rpt_DebitNote.rpt"));

                        rd.Database.Tables["InvoiceFull"].SetDataSource(request);
                        rd.Database.Tables["COMPANY"].SetDataSource(comData);

                        Response.Buffer = false;
                        Response.ClearContent();
                        Response.ClearHeaders();


                        //Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                        //stream.Seek(0, SeekOrigin.Begin);
                        //return File(stream, "application/pdf", "CustomerList.pdf"); 

                        try
                        {
                            this.Response.Clear();
                            this.Response.ContentType = "application/pdf";
                            Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName);
                            int reprinted = CHNLSVC.Sales.setInvoicePrintedStatus(Inv, company);
                            //return File(rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), "application/pdf");

                            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                            stream.Seek(0, SeekOrigin.Begin);
                            rd.Close();
                            rd.Dispose();
                            return File(stream, "application/pdf"); 
                        }
                        catch (Exception ex)
                        {
                            CHNLSVC.General.SaveReportErrorLog("rpt_DebitNote", "rpt_DebitNote", ex.Message, Session["UserID"].ToString());
                            throw;
                        }
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Invalid request number", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = true, msg = error, type = "Info" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Redirect("~/Login");
            }
        }

        public ActionResult DebitPrintPreview(string Inv, string InvType)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string userDefChnl = HttpContext.Session["UserDefChnl"] as string;


            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                string error = "";
                DataTable request = CHNLSVC.Sales.Inv_Details(Inv, company, userDefChnl, out error);
                if (error == "" && request.Rows.Count > 0)
                {
                    if (request.Rows[0]["Tih_inv_no"].ToString() != null)
                    {
                        string reportName = "";
                        string fileName = "";
                        string ReportPath = System.Configuration.ConfigurationManager.AppSettings["ReportPath"].ToString();
                        string report = "";

                        DataTable comData = new DataTable("comdata");
                        comData = CHNLSVC.Sales.getCompanyDetailsBycd(company);
                        //ReportDocument rd = new ReportDocument();
                        ////trn_inv_hdr rptData = CHNLSVC.Sales.Inv_Details(Inv, company, out error);
                        //DataTable comData = new DataTable("comdata");
                        //comData = CHNLSVC.Sales.getCompanyDetailsBycd(company);

                        //reportName = InvType == "Half" ? "rpt_InvoiceHalf.rpt" : InvType == "FullWOLogo" ? "rpt_InvoiceWithoutLogo.rpt" : "rpt_DebitNote_preview.rpt";
                        //fileName = InvType == "Half" ? "Invoice Half.pdf" : InvType == "FullWOLogo" ? "Invoice FullWOLogo.pdf" : "Invoice Full.pdf";
                        //report = ReportPath + "\\" + reportName;
                        //rd.Load(report);
                        //rd.Database.Tables["InvoiceFull"].SetDataSource(request);
                        //rd.Database.Tables["COMPANY"].SetDataSource(comData);




                        //Response.Buffer = false;
                        //Response.ClearContent();
                        //Response.ClearHeaders();

                        fileName = "Debit Note Preview.pdf";

                        ReportDocument rd = new ReportDocument();
                        rd.Load(Path.Combine(Server.MapPath("~/Reports"), "rpt_DebitNote_preview.rpt"));

                        rd.Database.Tables["InvoiceFull"].SetDataSource(request);
                        rd.Database.Tables["COMPANY"].SetDataSource(comData);

                        Response.Buffer = false;
                        Response.ClearContent();
                        Response.ClearHeaders();


                        //Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                        //stream.Seek(0, SeekOrigin.Begin);
                        //return File(stream, "application/pdf", "CustomerList.pdf"); 

                        try
                        {
                            this.Response.Clear();
                            this.Response.ContentType = "application/pdf";
                            Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName);
                            //int reprinted = CHNLSVC.Sales.setInvoicePrintedStatus(Inv, company);
                            //return File(rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), "application/pdf");

                            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                            stream.Seek(0, SeekOrigin.Begin);
                            rd.Close();
                            rd.Dispose();
                            return File(stream, "application/pdf"); 
                        }
                        catch (Exception ex)
                        {
                            CHNLSVC.General.SaveReportErrorLog("rpt_DebitNote_preview", "rpt_DebitNote_preview", ex.Message, Session["UserID"].ToString());
                            throw;
                        }
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Invalid request number", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = true, msg = error, type = "Info" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Redirect("~/Login");
            }
        }
        public JsonResult GetAllData(string InvNo)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    InvNo = InvNo.Trim();
                    List<trn_inv_hdr> inv_hdr = CHNLSVC.Sales.GetInvHdr(InvNo, company);
                    List<trn_inv_det> inv_det = new List<trn_inv_det>();
                    List<RecieptItem> _recitm = new List<RecieptItem>();
                    string cusname = "";
                    string partyname = "";
                    Int32 applvl = Convert.ToInt32(Session["Log_Autho"].ToString());
                    if (applvl != 3)
                    {
                        inv_hdr = inv_hdr.Where(a => a.Tih_pc_cd == userDefPro).ToList();
                    }

                    if (inv_hdr != null)
                    {
                        if (inv_hdr.Count>0)
                        {
                         inv_det = CHNLSVC.Sales.Get_Inv_det(inv_hdr.First().Tih_seq_no.ToString());
                         Session["trn_inv_det"] = inv_det;
                            List<GET_CUS_BASIC_DATA> Cus_det = CHNLSVC.General.GetCustormerBasicData(inv_hdr.First().Tih_cus_cd, company, "");
                            List<GET_CUS_BASIC_DATA> Pty_det = CHNLSVC.General.GetCustormerBasicData(inv_hdr.First().Tih_inv_party_cd, company, "");
                         cusname = Cus_det.First().mbe_name;
                         partyname = Pty_det.First().mbe_name;

                        _recitm = CHNLSVC.Sales.GetReceiptDetailsWithinvno(InvNo);
                            if (_recitm == null)
                        {
                            _recitm = new List<RecieptItem>();
                        }
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "Invalid Invoice No" }, JsonRequestBehavior.AllowGet);
                        }


                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Invalid Invoice No" }, JsonRequestBehavior.AllowGet);
                    }

                    bool pms = false;

                    
                    if (CHNLSVC.Security.Is_OptionPerimitted(company, userId.ToUpper(), 1011))
                    {
                        pms = true;
                    }
                    else
                    {
                        if (CHNLSVC.Security.Is_OptionPerimitted(company, userId.ToUpper(), 1010))
                        {
                            pms = true;
                        }
                        else
                        {
                            pms = false;
                        }
                    }

                    
                    return Json(new { success = true, login = true, hdrdata = inv_hdr, itemdata = inv_det, Cusname = cusname, Ptyname = partyname, Recitem = _recitm, permission = pms }, JsonRequestBehavior.AllowGet);

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
        public void ClearSession()
        {
            Session["trn_inv_det"] = null;
            Session["RecieptItemList"] = null;
            Session["totalPaidAmount"] = null;
            Session["totalPaidAmount"] = "0.0";
            Session["RecieptItemList"] = null;
        }

        public JsonResult LoadBLDetails(string BLno)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro))
                {
                    List<InvoiceCom> datelist = CHNLSVC.Sales.GetEtaEtdInvoiceDate(BLno, userDefPro);
                    DateTime invdate = datelist.Select(d => d.INV_DATE).FirstOrDefault();

                    List<InvoiceCom> num_of_backdates = CHNLSVC.Sales.GetNumOfBackdates(company, userDefPro);
                    int numof_bkdate = num_of_backdates.Select(b => b.NUM_OF_BKDATES).FirstOrDefault();

                    int date_diff = (int)(DateTime.Now.Date - invdate.Date).TotalDays;
                    int backdatestatus = 0;
                    if (numof_bkdate < date_diff)
                    {
                        backdatestatus = 1;
                    }

                    List<BLData> _list = CHNLSVC.General.LoadBLDetails(BLno);
                    if (_list == null)
                    {
                        _list = new List<BLData>();
                    }
                    return Json(new { success = true, login = true, data = _list, invdate = invdate.Date.ToString(), backdates = backdatestatus }, JsonRequestBehavior.AllowGet);

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

        public JsonResult StatusChangeInvoiceCancel(string Invoiceno, string Status, DateTime invDate)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (invDate.Date >= DateTime.Now.Date)
                    {
                        if (CHNLSVC.Security.Is_OptionPerimitted(company, userId.ToUpper(), 1010))
                        {
                            List<trn_inv_hdr> inv_hdr = CHNLSVC.Sales.GetInvHdr(Invoiceno, company);
                            if (inv_hdr != null)
                            {
                                if (inv_hdr.Count > 0)
                                {
                                    if (inv_hdr.First().Tih_inv_status == "C")
                                    {
                                        //Already Cancelled
                                        return Json(new { success = false, login = true, msg = "Already Cancelled", type = "Info" }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }

                            int effect = CHNLSVC.Sales.UpdateInvoiceStatus(Invoiceno, company, Status, userId);
                            if (effect > 0)
                            {
                                return Json(new { success = true, login = true, msg = "Successfully Cancelled!!", type = "Succ" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = true, login = true, msg = "Error", type = "Error" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new { success = true, login = true, msg = "No permission to cancel", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else if (invDate.Date < DateTime.Now.Date)
                    {
                        if (CHNLSVC.Security.Is_OptionPerimitted(company, userId.ToUpper(), 1011))
                        {
                            List<trn_inv_hdr> inv_hdr = CHNLSVC.Sales.GetInvHdr(Invoiceno, company);
                            if (inv_hdr != null)
                            {
                                if (inv_hdr.Count > 0)
                                {
                                    if (inv_hdr.First().Tih_inv_status == "C")
                                    {
                                        //Already Cancelled
                                        return Json(new { success = false, login = true, msg = "Already Cancelled", type = "Info" }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }

                            int effect = CHNLSVC.Sales.UpdateInvoiceStatus(Invoiceno, company, Status, userId);
                            if (effect > 0)
                            {
                                return Json(new { success = true, login = true, msg = "Successfully Cancelled!!", type = "Succ" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = true, login = true, msg = "Error", type = "Error" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new { success = true, login = true, msg = "No permission to cancel back date invoice", type = "Error" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { success = true, login = true, msg = "Invalid invoice date", type = "Info" }, JsonRequestBehavior.AllowGet);
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

        public JsonResult GetJobServiceCode(string jobno, string cusid)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MainServices> servs = CHNLSVC.Sales.GetJobServiceCode(jobno.Trim(), cusid.Trim(), company, userDefPro);

                    string service_code = "";
                    service_code = servs.Select(s => s.fms_ser_cd).FirstOrDefault();

                    return Json(new { success = true, login = true, service = service_code }, JsonRequestBehavior.AllowGet);

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

        public JsonResult InvoiceDateValidate(DateTime invdate)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<InvoiceCom> has_sun_up_restrict = CHNLSVC.Sales.GetSunUpRestrictStatus(company, userDefPro, invdate);
                    int has_sunup = 0; // has_sun_up_restrict.Select(s => s.HAS_DATE).FirstOrDefault();

                    List<InvoiceCom> num_of_backdates = CHNLSVC.Sales.GetNumOfBackdates(company, userDefPro);
                    int numof_bkdate = num_of_backdates.Select(b => b.NUM_OF_BKDATES).FirstOrDefault();

                    if (CHNLSVC.Security.Is_OptionPerimitted(company, userId.ToUpper(), 1013)) // Invoice Backdate Permission
                    {
                        if (has_sunup == 1)
                        {
                            return Json(new { success = true, login = true, type = "sun", msg = "Selected date is not valid due to Sun Upload" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            int date_diff = (int)(DateTime.Now.Date - invdate.Date).TotalDays;
                            //if (numof_bkdate < date_diff)
                            if(false)
                            {
                                //return Json(new { success = true, login = true, type = "over", msg = "" + numof_bkdate +" Days of maximum backdate exceeded" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = true, login = true, type = "succ", msg = "" }, JsonRequestBehavior.AllowGet);
                            }                            
                        }
                    }
                    else
                    {
                        if (has_sunup == 1)
                        {
                            return Json(new { success = true, login = true, type = "sun", msg = "Selected date is not valid due to Sun Upload" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = true, login = true, type = "denide", msg = "No permission to select a backdate" }, JsonRequestBehavior.AllowGet);
                        }
                    }

                    //return Json(new { success = true, login = true, service = service_code }, JsonRequestBehavior.AllowGet);

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




    }
}