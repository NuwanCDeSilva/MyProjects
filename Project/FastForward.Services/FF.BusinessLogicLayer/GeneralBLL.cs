using FF.BusinessObjects;
using FF.BusinessObjects.General;
using FF.BusinessObjects.InventoryNew;
using FF.BusinessObjects.Sales;
using FF.DataAccessLayer;
using FF.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;

namespace FF.BusinessLogicLayer
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class GeneralBLL : IGeneral
    {
        GeneralDAL _generalDAL = null;
        InventoryDAL _inventoryDAL = null;
        ReptCommonDAL _inventoryRepDAL = null;
        SalesDAL _saleDAL = null;
        SecurityDAL _securityDAL = null;
        FinancialDAL _financialDAL = null;


        public List<MasterColor> GetItemColorByCode(string _code)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetItemColorByCode(_code);
        }
        public DataTable GetExpenseLimitAloc(string _com, string _pc, string _expcd, DateTime _date)
        {
            DataTable DT1 = new DataTable();
            DataTable _dtChnl = new DataTable();
            _generalDAL = new GeneralDAL();
            _saleDAL = new SalesDAL();

            DT1 = _generalDAL.GetExpenseLimitAloc(_com, "PC", _pc, _expcd, _date);
            if (DT1.Rows.Count > 0)
            {
                return DT1;
            }

            _dtChnl = _saleDAL.GetChanelOnPC(_com, _pc, "SCHNL");
            DT1 = _generalDAL.GetExpenseLimitAloc(_com, "SCHNL", _dtChnl.Rows[0]["MPI_VAL"].ToString(), _expcd, _date);
            if (DT1.Rows.Count > 0)
            {
                return DT1;
            }
            _dtChnl = _saleDAL.GetChanelOnPC(_com, _pc, "CHNL");
            DT1 = _generalDAL.GetExpenseLimitAloc(_com, "CHNL", _dtChnl.Rows[0]["MPI_VAL"].ToString(), _expcd, _date);
            if (DT1.Rows.Count > 0)
            {
                return DT1;
            }

            return DT1;

        }
        public DataTable GetMstItmUOM(string _code)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetMstItmUOM(_code);
        }
        public MasterLocation GetAllLocationByLocCode(string _CompCode, string _LocCode, Int32 _isAll)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetAllLocationByLocCode(_CompCode, _LocCode, _isAll);
        }
        public DataTable GetPBSchemeRem(string _code)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetPBSchemeRem(_code);
        }
        public DataTable GetPBScheme(string _code)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetPBScheme(_code);
        }
        public DataTable GetTaxRateCode(string _type, string _code)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetTaxRateCode(_type, _code);
        }
        public DataTable getreqSerByReqno(string _req_no, string _item, string _ser)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getreqSerByReqno(_req_no, _item, _ser);
        }
        public DataTable Get_gen_reqapp_ser_byitem(string com, string reqNo, string _item)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.Get_gen_reqapp_ser_byitem(com, reqNo, _item);
        }
        public DataTable getPSITransLocs(DateTime _from, DateTime _to)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getPSITransLocs(_from, _to);
        }
        public DataTable getMyAbansSMSContacts(Int32 _opt, string _lan, String _town)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getMyAbansSMSContacts(_opt, _lan, _town);
        }
        public Int32 UpdateMarkAsPrint(string _com, DateTime _fromDate, DateTime _toDate, Int32 _isAll, string _user)
        {
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            int Y = _generalDAL.UpdateMarkAsPrint(_com, _fromDate, _toDate, _isAll, _user);
            _generalDAL.ConnectionClose();
            return Y;
        }

        public DataTable getMyAbansBySerial(string _ser, Int32 _isNIC)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getMyAbansBySerial(_ser, _isNIC);
        }
        public DataTable getFavouriteByCat(string _cat)
        {
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();

            return _generalDAL.getFavouriteByCat(_cat);
        }
        //kapila
        public Int32 CancelTempWaraWaranty(List<ReptPickSerials> _serList)
        {
            Int32 _eff = 0;
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();

            foreach (ReptPickSerials _ser in _serList)
            {
                _eff = _generalDAL.CancelTempWaraWaranty(_ser.Tus_itm_cd, _ser.Tus_ser_1, _ser.Tus_warr_no);
            }

            _generalDAL.ConnectionClose();
            return 1;
        }

        //kapila
        public Int32 SaveExpenseLimitAloc(List<Expense_Limit_Alloc> _serList, out string _err)
        {
            Int32 effect = 0;
            Int32 _seqno = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();

                foreach (Expense_Limit_Alloc _ser in _serList)
                {
                    _seqno = _generalDAL.GetSerialID_1();
                    _ser.Exla_seq = _seqno;
                    effect = _generalDAL.SaveExpenseLimitAloc(_ser);
                }

                _generalDAL.TransactionCommit();

                effect = 1;
                _err = "";
            }
            catch (Exception err)
            {
                effect = -1;
                _err = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();
            }
            return effect;
        }

        //kapila
        public Int32 SaveSubLocations(MasterSubLocation _subLoc, out string _err)
        {
            Int32 effect = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();

                _err = "Successfully Saved ! Sub location Code : " + _subLoc.Msl_sloc;

                effect = _generalDAL.SaveSubLocations(_subLoc);

                _generalDAL.TransactionCommit();

                effect = 1;
            }
            catch (Exception err)
            {
                effect = -1;
                _err = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();
            }
            return effect;
        }
        public Int32 UpdateVoucherPara(List<PromotionVoucherPara> _lstVouPara)
        {
            Int32 _effect = 0;
            try
            {

                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                if (_lstVouPara != null)
                {
                    foreach (PromotionVoucherPara _vou in _lstVouPara)
                    {
                        _effect = _generalDAL.UpdateVoucherPromo(_vou);
                    }
                }

                _generalDAL.TransactionCommit();
                _effect = 1;
            }
            catch (Exception err)
            {
                _effect = -1;
                // _docNo = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();
            }

            return _effect;
        }
        public DataTable getInvBySerial(string _com, string _loc, string _type, string _ser)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getInvBySerial(_com, _loc, _type, _ser);
        }
        public DataTable GetSCM2LocationByCompany(string _Comp)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetSCM2LocationByCompany(_Comp);
        }
        public List<VehicalRegistration> GetVehRegNoByInvoiceNo(string com, string pc, string invNo)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetVehRegNoByInvoiceNo(com, pc, invNo);
        }
        public DataTable get_job_defects(string _jobNo, string _tp)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.get_job_defects(_jobNo, _tp);
        }
        public DataTable GetSerialSupplierGRN(String _com, String _item, String _serial, Int16 _key)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetSerialSupplierGRN(_com, _item, _serial, _key);
        }
        public DataTable GetSerialSupplierCode(String _com, String _item, String _serial, Int16 _key)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetSerialSupplierCode(_com, _item, _serial, _key);
        }
        public DataTable GetStockRequest(string _restp, string _loc, string _chnl, string _com, DateTime _date, string _item, String _brand, string _cat1, string _cat2, string _cat3, string _cat4, string _cat5)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetStockRequest(_restp, _loc, _chnl, _com, _date, _item, _brand, _cat1, _cat2, _cat3, _cat4, _cat5);
        }
        public DataTable GetItemGIT(string _com, string _loc, string _item, string _brand, string _cat1, string _cat2, string _cat3, string _cat4, string _cat5, Decimal _days)//, Decimal _wsdays, Decimal _ssdays)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetItemGIT(_com, _loc, _item, _brand, _cat1, _cat2, _cat3, _cat4, _cat5, _days);//, _wsdays, _ssdays);
        }
        //dilshan
        public DataTable GetItemGITWH(string _com, string _loc, string _item, string _brand, string _cat1, string _cat2, string _cat3, string _cat4, string _cat5, Decimal _days, string _uloc)//, Decimal _wsdays, Decimal _ssdays)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetItemGITWH(_com, _loc, _item, _brand, _cat1, _cat2, _cat3, _cat4, _cat5, _days, _uloc);//, _wsdays, _ssdays);
        }
        public DataTable ManualDocumentsReport(DateTime _fromDate, DateTime _toDate, Int32 _isasat, DateTime _asatdate, string _user, string _com, string _pc)
        {
            _generalDAL = new GeneralDAL();
            _securityDAL = new SecurityDAL();
            if (_securityDAL.Is_Report_DR("ManualDocument") == true) _generalDAL.ConnectionOpen_DR();
            return _generalDAL.ManualDocumentsReport(_fromDate, _toDate, _isasat, _asatdate, _user, _com, _pc);
        }
        public Boolean IsVehRegNoExist(string _com, string _recno, string _regno)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.IsVehRegNoExist(_com, _recno, _regno);
        }

        public Int32 SaveSMSOut(OutSMS _smsout)
        {
            Int32 Sgdd_seq = 0;
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();

            //kapila 8/7/2015
            if (_smsout.Msgtype == "SATIS")
            {
                Sgdd_seq = _inventoryDAL.GetSMSSerialID();
                _smsout.Msgid = Sgdd_seq.ToString();
            }
            Int32 _x = _generalDAL.SaveSMSOut(_smsout);
            _generalDAL.ConnectionClose();
            return _x;
        }

        //Added by Udesh - 26-Nov-2018
        public async Task<int> SendPromotionSMS(string userId, string company, OutSMS _smsout, string sessionId)
        {
            try
            {
                _generalDAL = new GeneralDAL();
                DataTable urlTable1 = _generalDAL.SP_Get_Sys_Para_Detail_By_Name("BULK SMS NOT USED MYSQL");
                string _com = string.Empty;

                if (urlTable1.Rows.Count > 0)
                {
                    if (urlTable1.Rows[0]["rsp_val"] != null)
                    {
                        _com = urlTable1.Rows[0]["rsp_val"].ToString();
                    }
                }

                if (string.Compare(company, _com, true) == 0)
                {
                    SaveSMSOut(_smsout);
                    return 0;
                }
                else
                {

                    if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company))
                    {
                        HdrObj hed = new HdrObj();
                        hed.mobile = new List<string>() { _smsout.Receiverphno };
                        hed.recipient = "Abans";
                        hed.type = "SMS";
                        hed.sending_user = "Abans";
                        hed.subsiderycode = company;
                        hed.message = _smsout.Msg.Trim();
                        hed.sessionid = sessionId;
                        hed.sending_user = userId;

                        response res = new response();
                        string url = string.Empty;
                        string token = string.Empty;

                        DataTable urlTable = _generalDAL.SP_Get_Sys_Para_Detail_By_Name("BULK SMS API PATH");
                        if (urlTable.Rows.Count > 0) { url = urlTable.Rows[0]["rsp_val"].ToString(); }

                        DataTable tokenTable = _generalDAL.SP_Get_Sys_Para_Detail_By_Name("BULK SMS MYSQL");
                        if (tokenTable.Rows.Count > 0) { token = tokenTable.Rows[0]["rsp_val"].ToString(); }

                        string obj = JsonConvert.SerializeObject(hed).ToString();
                        using (HttpClient client = new HttpClient())
                        {
                            var formContent = new FormUrlEncodedContent(new[]
                        {
                            new KeyValuePair<string, string>("type", "A"),
                            new KeyValuePair<string, string>("data", obj)
                        });
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                            HttpResponseMessage response = await client.PostAsync(url + "sms-send-bulk", formContent);
                            string responseBody = await response.Content.ReadAsStringAsync();
                            res = JsonConvert.DeserializeObject<response>(responseBody);
                        }

                        if (res.error_code == "200")
                        {
                            return 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                return 1;
            }
            return 1;
        }

        //Added by Udesh 26-Nov-2018
        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        public DataTable GetLocationSubChannel(string _com, string _loc)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetLocationSubChannel(_com, _loc);
        }

        #region master locations
        public List<MasterLocation> GetAllLocationData()
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetAllLocationData();
        }

        public List<MasterLocation> GetLocationByCompany(string _Comp)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetLocationByCompany(_Comp);
        }

        public MasterLocation GetLocationByLocCode(string _CompCode, string _LocCode)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetLocationByLocCode(_CompCode, _LocCode);
        }

        //Chamal 30-05-2012
        public MasterLocation GetLocationInfor(string _CompCode, string _LocCode)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetLocationInfor(_CompCode, _LocCode);
        }

        //Lakshan 10/12/2015
        public DataTable GetLocationByComs(string _CompCode)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetLocationByComs(_CompCode);
        }

        #endregion

        #region Master Companies

        public List<MasterCompany> GetALLMasterCompaniesData()
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetALLMasterCompaniesData();
        }

        public MasterCompany GetCompByCode(string _Code)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetCompByCode(_Code);
        }

        #region Pending Approve *** Shanuka Perera 10/05/2014

        public List<PendingApproval> GetPendingApproveDetails(string _userid, string _com)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetPendingApproveDetails(_userid, _com);
        }

        public List<PendingApproval> GetPendingReqTypes(string _user_id, string _srtmaintp, string _com)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetPendingReqTypes(_user_id, _srtmaintp, _com);
        }



        #endregion

        // Nadeeka 28-12-12
        public DataTable GetCompanyByCode(string _Code)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetCompanyByCode(_Code);
        }
        //Shalika 06/05/2014
        public DataTable GetAuth_Transactions(string _Trn_Type, string _user_id, string com)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetAuth_Transactions(_Trn_Type, _user_id, com);
        }
        public DataTable GetHireSales_Transaction(string _sart_desc, string _user_id, string com)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetHireSales_Transaction(_sart_desc, _user_id, com);
        }
        public DataTable GetDept_Comment()
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetDept_Comment();
        }
        public DataTable GetRequestDetails_For_history(string _sart_desc, DateTime FromDate, DateTime ToDate, decimal Level, string status, string Location)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetRequestDetails_For_history(_sart_desc, FromDate, ToDate, Level, status, Location);
        }
        #endregion



        #region Master User Category
        public List<MasterUserCategory> GetUserCategory()
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetUserCategory();
        }

        //kapila 26/3/2012
        public MasterUserCategory GetUserCatByCode(string _catCode)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetUserCatByCode(_catCode);
        }
        #endregion

        #region Master Department
        public List<MasterDepartment> GetDepartment()
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetDepartment();
        }

        //kapila 26/3/2012
        public MasterDepartment GetDeptByCode(string _deptCode)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetDeptByCode(_deptCode);
        }

        #endregion

        #region Master Types
        public List<MasterType> GetAllType(string _Cate)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetAllTypes(_Cate);
        }

        public List<IncentiveSch> GetIncentiveSchemes(string _circ)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetIncentiveSchemes(_circ);
        }
        public List<IncentiveSchDet> GetIncSchDet(string _ref)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetIncSchDet(_ref);
        }
        public List<MasterType> GetOutwardTypes()
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetOutwardTypes();
        }
        #endregion

        #region Master Sub Types
        /// <summary>
        /// Created By : Miginda Geeganage On 26-03-2012
        /// </summary>
        public List<MasterSubType> GetAllSubTypes(string _mainCode)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetAllSubTypes(_mainCode);
        }

        #endregion

        #region Check back date *** Chamal De Silva 29/06/2012
        /// <summary>
        /// Created By : Chamal De Silva On 29-06-2012
        /// </summary>
        public bool IsAllowBackDate(string _com, string _loc, string _pc, string _backdate)
        {
            _generalDAL = new GeneralDAL();
            string _ope = string.Empty;
            string _chnl = string.Empty;
            bool _isallowbackdates = false;

            if (!string.IsNullOrEmpty(_loc))
            {
                MasterLocation _mLoc = new MasterLocation();
                _mLoc = _generalDAL.GetLocationByLocCode(_com, _loc);
                _ope = _mLoc.Ml_ope_cd;
                _chnl = _mLoc.Ml_cate_2;
            }
            if (!string.IsNullOrEmpty(_pc))
            {
                SalesDAL _salesDAL = new SalesDAL();
                MasterProfitCenter _mPc = new MasterProfitCenter();
                _mPc = _salesDAL.GetProfitCenter(_com, _pc);
            }

            BackDates _backdates = _generalDAL.IsAllowBackDate(_com, _ope, _chnl, _loc, _pc, _backdate);
            if (_backdates != null)
            {
                _isallowbackdates = true;
                if (!string.IsNullOrEmpty(_backdate))
                {
                    if (!(Convert.ToDateTime(_backdate).Date >= _backdates.Gad_act_from_dt.Date && Convert.ToDateTime(_backdate).Date <= _backdates.Gad_act_to_dt.Date))
                    {
                        _isallowbackdates = false;
                    }
                }
            }
            else
            {
                if (_backdate != null)
                {
                    _isallowbackdates = true;
                }
            }

            return _isallowbackdates;
        }

        //Chamal 10-08-2012
        public bool IsAllowBackDateForModule(string _com, string _loc, string _pc, string _module, string _backdate, out BackDates _backdateobj)
        {
            _generalDAL = new GeneralDAL();
            _inventoryDAL = new InventoryDAL();
            string _ope = string.Empty;
            string _chnl = string.Empty;
            bool _isallowbackdates = false;

            if (!string.IsNullOrEmpty(_loc))
            {
                MasterLocation _mLoc = new MasterLocation();
                _mLoc = _generalDAL.GetLocationByLocCode(_com, _loc);
                _ope = _mLoc.Ml_ope_cd;
                _chnl = _mLoc.Ml_cate_2;
            }
            if (!string.IsNullOrEmpty(_pc))
            {
                SalesDAL _salesDAL = new SalesDAL();
                MasterProfitCenter _mPc = new MasterProfitCenter();
                _mPc = _salesDAL.GetProfitCenter(_com, _pc);
            }

            BackDates _backdates = _generalDAL.IsAllowBackDateForModule(_com, _ope, _chnl, _loc, _pc, _module, _backdate);
            if (_backdates != null)
            {
                _isallowbackdates = true;
                if (!string.IsNullOrEmpty(_backdate))
                {
                    if (!(Convert.ToDateTime(_backdate).Date >= _backdates.Gad_act_from_dt.Date && Convert.ToDateTime(_backdate).Date <= _backdates.Gad_act_to_dt.Date))
                    {
                        _isallowbackdates = false;
                    }
                }
            }
            //else
            //{
            //    if (!string.IsNullOrEmpty(_backdate))
            //    {
            //        _isallowbackdates = true;
            //    }
            //}

            _backdateobj = _backdates;
            if (_isallowbackdates)
            {
                #region add validation to chk period close or not add by lakshan 27 Mar 2017
                if (!string.IsNullOrEmpty(_backdate))
                {
                    DateTime _tmp = new DateTime();
                    _tmp = DateTime.TryParse(_backdate, out _tmp) ? Convert.ToDateTime(_backdate) : DateTime.MinValue;
                    if (_tmp != DateTime.MinValue)
                    {
                        List<RefPrdMt> _perBlockList = _inventoryDAL.GET_REF_PRD_MT_DATA(new RefPrdMt()
                        {
                            Prd_stus = "CLOSE",
                            Prd_com_cd = _com,
                            Prd_from = Convert.ToDateTime(_backdate),
                            Prd_to = Convert.ToDateTime(_backdate)
                        });
                        if (_perBlockList.Count > 0)
                        {
                            _isallowbackdates = false;
                        }                       
                    }
                }
                else
                {
                    //_backdates.Gad_act_from_dt.Date
                    if (_backdates.Gad_act_from_dt.Date != DateTime.MinValue)
                    {
                        List<RefPrdMt> _perBlockList = _inventoryDAL.GET_REF_PRD_MT_DATA(new RefPrdMt()
                        {
                            Prd_stus = "CLOSE",
                            Prd_com_cd = _com,
                            Prd_from = _backdates.Gad_act_from_dt.Date,
                            Prd_to = _backdates.Gad_act_from_dt.Date
                        });
                        if (_perBlockList.Count > 0)
                        {
                            _isallowbackdates = false;
                        }
                    }                   
                }                                
             
                #endregion
            }
  
            return _isallowbackdates;
        }

        #endregion

        //Written by Prabhath on 21/05/2012
        public List<MasterCurrency> GetAllCurrency(string _currencyCode)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetAllCurrency(_currencyCode);
        }

        //Written by darshana 04-06-2012
        public List<RequestApprovalHeader> GetAllPendingRequest(RequestApprovalHeader _paramRequestApproval)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetAllPendingRequest(_paramRequestApproval);
        }


        //Written by darshana 05-06-2012
        public List<RequestApprovalDetail> GetRequestApprovalDetails(RequestApprovalDetail _paramRequestApprovalDetails)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetRequestApprovalDetails(_paramRequestApprovalDetails);
        }

        public DataTable GetESDTransactions(string _com, string _pc, string _epf, DateTime _month)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetESDTransactions(_com, _pc, _epf, _month);
        }
        public DataTable GetESDBalance(string _com, string _pc, string _epf, DateTime _month)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetESDBalance(_com, _pc, _epf, _month);
        }
        public DataTable Get_Dept_Cont_Dt(string _com, string _chnl, string _cd, Int32 _tp)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.Get_Dept_Cont_Dt(_com, _chnl, _cd, _tp);
        }
        //kapila 26/5/2014
        public int update_area(MasterArea _area, MasterAutoNumber _masterAutoNumber, out string _code)
        {
            Int32 _effects = 0;
            _generalDAL = new GeneralDAL();
            try
            {
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();

                if (string.IsNullOrEmpty(_area.Msar_cd))
                {
                    Int32 _autoNo = _generalDAL.GetAutoNumber(_masterAutoNumber.Aut_moduleid, null, _masterAutoNumber.Aut_start_char, null, null, null, null).Aut_number;
                    string _documentNo = _masterAutoNumber.Aut_start_char + _autoNo.ToString("0000", CultureInfo.InvariantCulture);
                    _area.Msar_cd = _documentNo;
                    int _effect = _generalDAL.UpdateAutoNumber(_masterAutoNumber);
                    _code = _documentNo;
                }
                else
                {
                    _code = _area.Msar_cd;
                }
                _effects = _generalDAL.update_area(_area);
                _generalDAL.TransactionCommit();
            }
            catch (Exception err)
            {
                _effects = -1;
                _code = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();
            }
            return _effects;
        }

        //kapila 26/5/2014
        public int update_region(MasterRegion _region, MasterAutoNumber _masterAutoNumber, out string _code)
        {
            Int32 _effects = 0;
            _generalDAL = new GeneralDAL();
            try
            {
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();

                if (string.IsNullOrEmpty(_region.Msrg_cd))
                {
                    Int32 _autoNo = _generalDAL.GetAutoNumber(_masterAutoNumber.Aut_moduleid, null, _masterAutoNumber.Aut_start_char, null, null, null, null).Aut_number;
                    string _documentNo = _masterAutoNumber.Aut_start_char + _autoNo.ToString("0000", CultureInfo.InvariantCulture);
                    _region.Msrg_cd = _documentNo;
                    int _effect = _generalDAL.UpdateAutoNumber(_masterAutoNumber);
                    _code = _documentNo;
                }
                else
                {
                    _code = _region.Msrg_cd;
                }
                _effects = _generalDAL.update_region(_region);
                _generalDAL.TransactionCommit();
            }
            catch (Exception err)
            {
                _effects = -1;
                _code = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();
            }
            return _effects;
        }

        //kapila
        public Int32 Update_ESD_SR_Details(string _com, string _pc, string _epf, DateTime _join, DateTime _ho, DateTime _AD, string _sunacc, string _user)
        {
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            Int32 x = _generalDAL.Update_ESD_SR_Details(_com, _pc, _epf, _join, _ho, _AD, _sunacc, _user);
            _generalDAL.ConnectionClose();
            return x;
        }

        //kapila
        public Int16 SaveStockRequest(string p_MRQ_COM, DateTime p_MRQ_VALID_FRM, DateTime p_MRQ_VALID_TO, string p_MRQ_RES_TP, string p_MRQ_PTY_TP, string p_MRQ_PTY_CD, string p_MRQ_ITM_CD, string p_MRQ_BRD, string p_MRQ_CAT1, string p_MRQ_CAT2, string p_MRQ_CAT3, string p_MRQ_CAT4, string p_MRQ_CAT5, decimal p_MRQ_QTY, Int32 p_MRQ_STUS, string p_MRQ_CRE_BY, string p_MRQ_MOD_BY, decimal p_MRQ_DAYS, decimal p_MRQ_WSDAYS, decimal p_MRQ_SSDAYS)
        {
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            Int16 x = _generalDAL.SaveStockRequest(p_MRQ_COM, p_MRQ_VALID_FRM, p_MRQ_VALID_TO, p_MRQ_RES_TP, p_MRQ_PTY_TP, p_MRQ_PTY_CD, p_MRQ_ITM_CD, p_MRQ_BRD, p_MRQ_CAT1, p_MRQ_CAT2, p_MRQ_CAT3, p_MRQ_CAT4, p_MRQ_CAT5, p_MRQ_QTY, p_MRQ_STUS, p_MRQ_CRE_BY, p_MRQ_MOD_BY, p_MRQ_DAYS, p_MRQ_WSDAYS, p_MRQ_SSDAYS);
            _generalDAL.ConnectionClose();
            return x;
        }

        //kapila
        public int GetESDPrvBalance(string _com, string _pc, string _epf, DateTime _month, DateTime _prvmonth, out Decimal _prvESDBal, out Decimal _prvIntBal, out Decimal _ESDContr, out Decimal _ESDAdj, out Decimal _IntAdj)
        {
            Decimal _ESDBal = 0;
            Decimal _TotRec = 0;
            DateTime _from = _month.AddDays(1 - _month.Day);
            DateTime _to = _from.AddMonths(1).AddSeconds(-1);

            _generalDAL = new GeneralDAL();
            //prv. month closing balance
            int _eff = _generalDAL.GetESDPrvBal(_com, _pc, _epf, _prvmonth, out  _ESDBal, out  _prvIntBal);

            //current month receipt total
            int _eff1 = _generalDAL.GetESDCurRecTotal(_com, _pc, _epf, _month, out  _TotRec);
            _prvESDBal = _ESDBal;

            //curent month contribution
            int _eff2 = _generalDAL.GetESDContribution(_com, _pc, _from, _to, out  _ESDContr, _epf);

            //esd adjustment
            int _eff3 = _generalDAL.GetESDAdj(_com, _pc, _epf, _month, out  _ESDAdj);
            _ESDAdj = _ESDAdj + _TotRec;

            //int adjustment
            int _eff4 = _generalDAL.GetIntAdj(_com, _pc, _epf, _month, out  _IntAdj);
            return _eff;

        }

        public Int32 SaveBrandAlloc(string _mba_com, string _mba_pty_tp, string _mba_pty_cd, string _mba_emp_id, string _mba_brnd, string _mba_ca1, string _mba_ca2, string _mba_ca3, string _mba_ca4, string _mba_ca5, DateTime _mba_frm_dt, DateTime _mba_to_dt, Int32 _mba_act, string _mba_cre_by, string _mba_mod_by)
        {
            Int32 _eff = 0;
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            _eff = _generalDAL.SaveBrandAlloc(_mba_com, _mba_pty_tp, _mba_pty_cd, _mba_emp_id, _mba_brnd, _mba_ca1, _mba_ca2, _mba_ca3, _mba_ca4, _mba_ca5, _mba_frm_dt, _mba_to_dt, _mba_act, _mba_cre_by, _mba_mod_by);
            _generalDAL.ConnectionClose();
            return _eff;
        }
        public Int32 SaveESDBalance(ESDBal _esdBal)
        {
            Int32 _effects = 0;
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();

            Int32 x = _generalDAL.SaveESDBalance(_esdBal);

            _generalDAL.ConnectionClose();
            return _effects;
        }

        public Int32 SaveESDTransaction(List<ESDTxn> _esdtxnList)
        {
            Int32 _effects = 0;
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            foreach (ESDTxn _lst in _esdtxnList)
            {
                _effects = _generalDAL.SaveESDTransaction(_lst);
            }

            _generalDAL.ConnectionClose();
            return _effects;
        }

        //kapila 26/5/2014
        public int update_zone(MasterZone _zone, MasterAutoNumber _masterAutoNumber, out string _code)
        {
            Int32 _effects = 0;
            _generalDAL = new GeneralDAL();
            try
            {
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();

                if (string.IsNullOrEmpty(_zone.Mszn_cd))
                {
                    Int32 _autoNo = _generalDAL.GetAutoNumber(_masterAutoNumber.Aut_moduleid, null, _masterAutoNumber.Aut_start_char, null, null, null, null).Aut_number;
                    string _documentNo = _masterAutoNumber.Aut_start_char + _autoNo.ToString("0000", CultureInfo.InvariantCulture);
                    _zone.Mszn_cd = _documentNo;
                    int _effect = _generalDAL.UpdateAutoNumber(_masterAutoNumber);
                    _code = _documentNo;
                }
                else
                {
                    _code = _zone.Mszn_cd;
                }
                _effects = _generalDAL.update_zone(_zone);
                _generalDAL.TransactionCommit();
            }
            catch (Exception err)
            {
                _effects = -1;
                _code = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();
            }
            return _effects;
        }

        //Writen by darshana 05-06-2012
        public Int32 UpdateApprovalStatus(RequestApprovalHeader _UpdateApproval)
        {
            Int32 _effects = 0;
            _generalDAL = new GeneralDAL();
            //using (TransactionScope _tr = new TransactionScope())
            //{
            try
            {
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                _generalDAL.UpdateApprovalStatus(_UpdateApproval);


                //_generalDAL.ConnectionClose();
                _generalDAL.TransactionCommit();
                _effects = 1;
                //_tr.Complete();
            }
            catch (Exception err)
            {
                _effects = -1;
                //documentNo = "ERROR : " + err.Message.ToString();
                //documentNoGRN = documentNo;
                _generalDAL.TransactionRollback();

            }
            return _effects;
        }

        //kapila 2/7/2012
        public int SaveOutsideParty(MasterOutsideParty _outsideParty, MasterAutoNumber _masterAutoNumber, out string _CompCode)
        {
            Int32 _effects = 0;
            _generalDAL = new GeneralDAL();

            try
            {
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                Int32 _docSeqNo = _generalDAL.GetSerialID();
                Int16 effect = 0;

                if (string.IsNullOrEmpty(_outsideParty.Mbi_cd))
                {
                    Int32 _autoNo = _generalDAL.GetAutoNumber(_masterAutoNumber.Aut_moduleid, _masterAutoNumber.Aut_direction, _masterAutoNumber.Aut_start_char, _masterAutoNumber.Aut_cate_tp, _masterAutoNumber.Aut_cate_cd, _masterAutoNumber.Aut_modify_dt, _masterAutoNumber.Aut_year).Aut_number;
                    string _documentNo = _masterAutoNumber.Aut_start_char + "-" + _autoNo.ToString("000000", CultureInfo.InvariantCulture);
                    _outsideParty.Mbi_cd = _documentNo;
                    effect = _generalDAL.UpdateAutoNumber(_masterAutoNumber);
                    _CompCode = _documentNo;
                }
                else
                {
                    _CompCode = _outsideParty.Mbi_cd;
                }
                _effects = _generalDAL.SaveOutsideParty(_outsideParty);
                _generalDAL.TransactionCommit();
            }
            catch (Exception err)
            {
                _effects = -1;
                _CompCode = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();
            }
            return _effects;
        }

        public int SaveBankBranch(MasterBankBranch _bankBranch, out string _error)
        {
            Int32 _effects = 0;
            _error = "";
            _generalDAL = new GeneralDAL();

            try
            {
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();

                _effects = _generalDAL.SaveBankBranch(_bankBranch);
                _generalDAL.TransactionCommit();
            }
            catch (Exception err)
            {
                _effects = -1;
                _error = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();
            }
            return _effects;
        }

        public MasterOutsideParty GetOutsideParty(string _code)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetOutsideParty(_code);
        }


        #region request approval

        public Int32 SaveRequestApproveHear(RequestApprovalHeader ReqApp_hdr)
        {
            Int16 effect = 0;
            using (TransactionScope tr = new TransactionScope())
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                if (ReqApp_hdr != null)
                    _generalDAL.SaveRequestApproveHear(ReqApp_hdr);


                _generalDAL.ConnectionClose();
                effect = 1;
                tr.Complete();

            }
            return effect;
        }

        public Int32 SaveRequestApproveHear_Log(RequestApprovalHeaderLog ReqApp_hdrLog)
        {
            Int16 effect = 0;
            using (TransactionScope tr = new TransactionScope())
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                if (ReqApp_hdrLog != null)
                    _generalDAL.SaveRequestApproveHear_Log(ReqApp_hdrLog);


                _generalDAL.ConnectionClose();
                effect = 1;
                tr.Complete();

            }
            return effect;
        }

        public Int32 Save_RequestApprove_Det(RequestApprovalDetail ReqApp_det)
        {
            Int16 effect = 0;
            using (TransactionScope tr = new TransactionScope())
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                if (ReqApp_det != null)
                    _generalDAL.Save_RequestApprove_Det(ReqApp_det);


                _generalDAL.ConnectionClose();
                effect = 1;
                tr.Complete();

            }
            return effect;
        }

        public Int32 Save_RequestApprove_Det_Log(RequestApprovalDetailLog ReqApp_detLog)
        {
            Int16 effect = 0;
            using (TransactionScope tr = new TransactionScope())
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                if (ReqApp_detLog != null)
                    _generalDAL.Save_RequestApprove_Det_Log(ReqApp_detLog);


                _generalDAL.ConnectionClose();
                effect = 1;
                tr.Complete();

            }
            return effect;
        }

        //public Int32 SendRequestApprove(RequestApprovalHeader reqAppHdr,RequestApprovalHeaderLog reqAppHdrLog,RequestApprovalDetail reqAppDet,RequestApprovalDetailLog reqAppDetLog)
        //{
        //    Int32 effect = -1;
        //    effect=SaveRequestApproveHear(reqAppHdr);
        //    effect=SaveRequestApproveHear_Log(reqAppHdrLog);

        //    effect=Save_RequestApprove_Det(reqAppDet);
        //    effect=Save_RequestApprove_Det_Log(reqAppDetLog);

        //    return effect;
        //}


        public DataTable GetApprovedRequestDetails(string com, string pc, string fucCD, string appType, Int32 isInApprovedStatus, Int32 userApprovalLevel)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetApprovedRequestDetails(com, pc, fucCD, appType, isInApprovedStatus, userApprovalLevel);
        }

        public DataTable GetPendingRequestDetails(string com, string pc, string fucCD, string appType, Int32 isInApprovedStatus, Int32 userApprovalLevel)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetApprovedRequestDetails(com, pc, fucCD, appType, isInApprovedStatus, userApprovalLevel);
        }

        public List<RequestApprovalHeader> GetApprovedRequestDetailsList(string com, string pc, string fucCD, string appType, Int32 isInApprovedStatus, Int32 userApprovalLevel)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetApprovedRequestDetailsList(com, pc, fucCD, appType, isInApprovedStatus, userApprovalLevel);
        }

        //Chamal 02/08/2012
        public List<RequestApprovalDetailLog> GetRequestApprovalDetailLog(string seqNo)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetRequestApprovalDetailLog(seqNo);
        }

        public Int32 SaveHirePurchasRequest(RequestApprovalHeader _hdr, List<RequestApprovalDetail> _det, RequestApprovalHeaderLog _hdrlog, List<RequestApprovalDetailLog> _delLog, int UserPermissionLevel, bool _isApprovalUser, bool _isRequestGenerateUser, out string _userSeqNo)
        {
            Int32 _effect = 0;

            _generalDAL = new GeneralDAL();
            int _userSeq = -1;

            //if (!string.IsNullOrEmpty(_hdr.Grah_ref) && _det != null)
            using (TransactionScope _tr = new TransactionScope())
            {
                _generalDAL.ConnectionOpen();
                bool _isNewRequest = string.IsNullOrEmpty(_hdr.Grah_ref) ? true : false;

                if (_isRequestGenerateUser)
                {//level 0
                    _userSeq = _generalDAL.GetSerialID();
                    _hdrlog.Grah_ref = Convert.ToString(_userSeq);
                    _hdr.Grah_ref = Convert.ToString(_userSeq);
                    _generalDAL.SaveRequestApproveHear(_hdr);
                    foreach (RequestApprovalDetail _itm in _det)
                    {
                        _itm.Grad_ref = Convert.ToString(_userSeq);
                        _generalDAL.Save_RequestApprove_Det(_itm);
                    }
                }

                else
                {
                    //level 1,2,3

                    if (_isNewRequest)
                    {
                        _userSeq = _generalDAL.GetSerialID();
                        _hdr.Grah_ref = _userSeq.ToString();

                        _generalDAL.SaveRequestApproveHear(_hdr);

                        foreach (RequestApprovalDetail _itm in _det)
                        {

                            _itm.Grad_ref = _userSeq.ToString();

                            _generalDAL.Save_RequestApprove_Det(_itm);
                        }
                    }

                    //Delete original record in detail
                    if (_isApprovalUser)
                    {
                        _generalDAL.DeleteRequestHeaderDetail(_hdr.Grah_ref);
                        foreach (RequestApprovalDetail _itm in _det)
                        {
                            _generalDAL.Save_RequestApprove_Det(_itm);
                        }

                    }
                    ////Insert new record
                    ////_generalDAL.SaveRequestApproveHear(_hdr);
                    //_hdrlog.Grah_ref = _hdr.Grah_ref;
                    //_generalDAL.SaveRequestApproveHear_Log(_hdrlog);
                    //foreach (RequestApprovalDetailLog _itm in _delLog)
                    //{
                    //    _itm.Grad_ref = Convert.ToString(_userSeq);
                    //    _generalDAL.Save_RequestApprove_Det_Log(_itm);
                    //}



                }
                _hdrlog.Grah_ref = _hdr.Grah_ref;
                _generalDAL.SaveRequestApproveHear_Log(_hdrlog);
                foreach (RequestApprovalDetailLog _itm in _delLog)
                {
                    _itm.Grad_ref = _hdr.Grah_ref;
                    _generalDAL.Save_RequestApprove_Det_Log(_itm);
                }

                //Add new code by Chamal De Silva 03-08-2012
                _generalDAL.UpdateMidApprovalStatus(_hdr);

                if (_isApprovalUser)
                {
                    //Update Status
                    _hdr.Grah_app_stus = "A";
                    _generalDAL.UpdateApprovalStatus(_hdr);
                }

                _generalDAL.ConnectionClose();
                _effect = 1;
                _userSeqNo = _hdr.Grah_ref;
                _tr.Complete();
            }
            _userSeqNo = _hdr.Grah_ref;
            return _effect;
        }

        //Duplicated by Prabhath on 13/06/2013
        public Int32 SaveHirePurchasRequestForRevertRelease(RequestApprovalHeader _hdr, List<RequestApprovalDetail> _det, RequestApprovalHeaderLog _hdrlog, List<RequestApprovalDetailLog> _delLog, int UserPermissionLevel, bool _isApprovalUser, bool _isRequestGenerateUser, out string _userSeqNo, MasterAutoNumber _auto)
        {
            Int32 _effect = 0;

            _generalDAL = new GeneralDAL();
            InventoryDAL _inventoryDAL = new InventoryDAL();
            string _userSeq = "-1";

            //if (!string.IsNullOrEmpty(_hdr.Grah_ref) && _det != null)
            using (TransactionScope _tr = new TransactionScope())
            {
                _generalDAL.ConnectionOpen();
                _inventoryDAL.ConnectionOpen();

                bool _isNewRequest = string.IsNullOrEmpty(_hdr.Grah_ref) ? true : false;
                string _autonumber = string.Empty;
                if (_isRequestGenerateUser || _isNewRequest)
                {
                    MasterAutoNumber _number = _inventoryDAL.GetAutoNumber(_auto.Aut_moduleid, (short)_auto.Aut_direction, _auto.Aut_start_char, _auto.Aut_cate_tp, _auto.Aut_cate_cd, _auto.Aut_modify_dt, _auto.Aut_year);
                    _autonumber = _auto.Aut_cate_cd + _auto.Aut_start_char + _auto.Aut_year + _number.Aut_number.ToString("000", CultureInfo.InvariantCulture);
                    _inventoryDAL.UpdateAutoNumber(_auto);
                }
                else
                {
                    _autonumber = _hdr.Grah_ref;
                }


                if (_isRequestGenerateUser)
                {//level 0



                    _userSeq = _autonumber;
                    _hdrlog.Grah_ref = Convert.ToString(_userSeq);
                    _hdr.Grah_ref = Convert.ToString(_userSeq);
                    _generalDAL.SaveRequestApproveHear(_hdr);
                    foreach (RequestApprovalDetail _itm in _det)
                    {
                        _itm.Grad_ref = Convert.ToString(_userSeq);
                        _generalDAL.Save_RequestApprove_Det(_itm);
                    }
                }

                else
                {
                    //level 1,2,3

                    if (_isNewRequest)
                    {
                        _userSeq = _autonumber;
                        _hdr.Grah_ref = _userSeq.ToString();

                        _generalDAL.SaveRequestApproveHear(_hdr);

                        foreach (RequestApprovalDetail _itm in _det)
                        {

                            _itm.Grad_ref = _userSeq.ToString();

                            _generalDAL.Save_RequestApprove_Det(_itm);
                        }
                    }

                    //Delete original record in detail
                    if (_isApprovalUser)
                    {
                        _generalDAL.DeleteRequestHeaderDetail(_hdr.Grah_ref);
                        foreach (RequestApprovalDetail _itm in _det)
                        {
                            _generalDAL.Save_RequestApprove_Det(_itm);
                        }

                    }
                    ////Insert new record
                    ////_generalDAL.SaveRequestApproveHear(_hdr);
                    //_hdrlog.Grah_ref = _hdr.Grah_ref;
                    //_generalDAL.SaveRequestApproveHear_Log(_hdrlog);
                    //foreach (RequestApprovalDetailLog _itm in _delLog)
                    //{
                    //    _itm.Grad_ref = Convert.ToString(_userSeq);
                    //    _generalDAL.Save_RequestApprove_Det_Log(_itm);
                    //}



                }
                _hdrlog.Grah_ref = _hdr.Grah_ref;
                _generalDAL.SaveRequestApproveHear_Log(_hdrlog);
                foreach (RequestApprovalDetailLog _itm in _delLog)
                {
                    _itm.Grad_ref = _hdr.Grah_ref;
                    _generalDAL.Save_RequestApprove_Det_Log(_itm);
                }

                //Add new code by Chamal De Silva 03-08-2012
                _generalDAL.UpdateMidApprovalStatus(_hdr);

                if (_isApprovalUser)
                {
                    //Update Status
                    _hdr.Grah_app_stus = "A";
                    _generalDAL.UpdateApprovalStatus(_hdr);
                }

                _generalDAL.ConnectionClose();
                _inventoryDAL.ConnectionOpen();
                _effect = 1;
                _userSeqNo = _hdr.Grah_ref;
                _tr.Complete();
            }
            _userSeqNo = _hdr.Grah_ref;
            return _effect;
        }


        public RequestApprovalHeader GetRequestApprovalHeaderByRequestNo(string _company, string _location, string _request)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetRequestApprovalHeaderByRequestNo(_company, _location, _request);
        }

        #endregion

        //kapila
        //Udesh Add string _company parameter - 08-Oct-2018
        public DataTable GetReprintDocs(string _docType, string _loc, string _pc, string _company, DateTime _fromDate, DateTime _toDate)
        {
            _generalDAL = new GeneralDAL();
            DataTable _tbl = _generalDAL.GetReprintDocs(_docType, _loc, _pc, _company, _fromDate, _toDate);
            return _tbl;
        }

        //kapila
        public int SaveReprintDocRequest(Reprint_Docs _reprintDoc)
        {
            _generalDAL = new GeneralDAL();

            if (IsReprintDocFound(_reprintDoc.Drp_doc_no) == true)
            {
                return 9999;
            }
            _generalDAL.ConnectionOpen();
            Int32 _SeqNo = _generalDAL.GetSerialID();

            _reprintDoc.Drp_seq_no = _SeqNo;
            int X = _generalDAL.SaveReprintDocRequest(_reprintDoc);
            _generalDAL.ConnectionClose();
            return X;
        }

        //kapila
        public DataTable GetRequestedReprintDocs(string _loc, string _status, DateTime _fromDate, DateTime _toDate)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetRequestedReprintDocs(_loc, _status, _fromDate, _toDate);
        }

        public int UpdatePrintStatus(string _docno)
        {
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            int X = _generalDAL.UpdatePrintStatus(_docno);
            _generalDAL.ConnectionClose();
            return X;
        }

        public Boolean checkESDStatus(string _com, string _pc, string _epf, DateTime _month)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.checkESDStatus(_com, _pc, _epf, _month);
        }

        public Boolean checkESDBalFound(string _com, string _pc, string _epf)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.checkESDBalFound(_com, _pc, _epf);
        }
        public Boolean IsReprintDocFound(string _docno)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.IsReprintDocFound(_docno);
        }

        public Boolean IsBackDateFound(string _pc, DateTime _date, string _module)
        {
            _generalDAL = new GeneralDAL();
            Boolean _x = _generalDAL.IsBackDateFound(_pc, _date, "ALL");     //kapila 1/11/2016
            if (_x == false)
                _x = _generalDAL.IsBackDateFound(_pc, _date, _module);
            return _x;

        }


        public int UpdateReprintApproval(string _docno, string _status, string _statusChg)
        {

            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            int Y = _generalDAL.UpdateReprintApproval(_docno, _status, _statusChg);
            _generalDAL.ConnectionClose();
            return Y;
        }

        public Int32 UpdateCashComEndDate(string _comm, DateTime _toDate, string _user)
        {
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            int Y = _generalDAL.UpdateCashComEndDate(_comm, _toDate, _user);
            _generalDAL.ConnectionClose();
            return Y;
        }

        public DataTable GetVehicalRegistrationBrand(string _mainCt, string _SubCt, string _Range, string _code, string _brand, string _type)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetVehicalRegistrationBrand(_mainCt, _SubCt, _Range, _code, _brand, _type);
        }

        public DataTable GetPartyTypes()
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetPartyTypes();
        }

        public DataTable GetPartyCodes(string _com, string _loc)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetPartyCodes(_com, _loc);
        }

        public DataTable GetSalesTypes(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetSalesTypes(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetIncentiveSTypeByCode(string _code)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetIncentiveSTypeByCode(_code);
        }

        public DataTable GetIncentiveSaleTypes()
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetIncentiveSaleTypes();
        }

        public Int32 SaveVehicalRegistrationDefinition(List<MasterProfitCenter> pcs, List<string> items, DateTime from, DateTime to, string satlesType, decimal regVal, decimal calVal, string cre, int isMan)
        {
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            InventoryDAL inv = new InventoryDAL();
            inv.ConnectionOpen();
            Int32 result = _generalDAL.SaveVehicalRegistrationDefinition(pcs, items, from, to, satlesType, regVal, calVal, cre, isMan);
            _generalDAL.ConnectionClose();
            inv.ConnectionClose();
            return result;
        }

        public DataTable GetRequestApprovalDetailFromLog(string seqNo)
        {

            _generalDAL = new GeneralDAL();
            return _generalDAL.GetRequestApprovalDetailFromLog(seqNo);
        }

        public DataTable GetVehicalreciept(string _com, string _loc, string _type)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetVehicalreciept(_com, _loc, _type);
        }

        public List<VehicalRegistration> GetVehicalRegistrations(string _regNo)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetVehicalRegistrations(_regNo);
        }

        public Int32 SaveVehicalRegistration(VehicalRegistration vehical, out string _err)
        {
            Int32 result = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();

                result = _generalDAL.SaveVehicalRegistration(vehical);

                _generalDAL.TransactionCommit();
                _err = string.Empty;

            }
            catch (Exception err)
            {
                result = -1;
                _err = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();
            }
            return result;
        }

        public Int32 SaveSignOnOff(Sign_On _signon, out Int32 _seq)
        {
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            Int32 result = _generalDAL.SaveSignOnOff(_signon);
            _seq = result;
            _generalDAL.ConnectionClose();
            return result;
        }

        public Int32 SaveCredSaleDocHeader(CreditSaleDocsHeader _credSaleDocHdr, out Int32 _seqno)
        {
            _generalDAL = new GeneralDAL();
            _inventoryDAL = new InventoryDAL();
            _inventoryDAL.ConnectionOpen();
            _generalDAL.ConnectionOpen();
            _credSaleDocHdr.Gdh_seq = _inventoryDAL.GetSerialID();
            _seqno = _credSaleDocHdr.Gdh_seq;
            Int32 result = _generalDAL.SaveCredSaleDocHeader(_credSaleDocHdr);
            _generalDAL.ConnectionClose();
            _inventoryDAL.ConnectionClose();
            return result;
        }

        public Int32 SaveCredSaleDocDetail(List<CreditSaleDocsDetail> _credSaleDocdet)
        {
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            foreach (CreditSaleDocsDetail _doc in _credSaleDocdet)
            {
                Int32 eff = _generalDAL.SaveCredSaleDocDetail(_doc);
            }
            _generalDAL.ConnectionClose();
            return 1;
        }

        public int UpdateInsuranceFromReg(string _regNo, string _regBy, DateTime _regDate, string _ref)
        {
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            Int32 result = _generalDAL.UpdateInsuranceFromReg(_regNo, _regBy, _regDate, _ref);
            _generalDAL.ConnectionClose();
            return result;
        }

        public MasterProfitCenter GetPCByPCCode(string _CompCode, string _PCCode)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetPCByPCCode(_CompCode, _PCCode);
        }

        public DataTable getSubChannelDet(string _company, string _code)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getSubChannelDet(_company, _code);
        }
        public DataTable GetInsuranceCompanies()
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetInsuranceCompanies();
        }

        public DataTable GetInsurancePolicies()
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetInsurancePolicies();
        }

        public Int32 SaveVehicalInsuranceDefinition(List<MasterProfitCenter> pcs, List<string> items, DateTime from, DateTime to, string satlesType, decimal regVal, string cre, string insCom, string insPol, int term, bool isReq)
        {
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            InventoryDAL inv = new InventoryDAL();
            inv.ConnectionOpen();
            Int32 result = _generalDAL.SaveVehicalInsuranceDefinition(pcs, items, from, to, satlesType, regVal, cre, insCom, insPol, term, isReq);
            inv.ConnectionClose();
            _generalDAL.ConnectionClose();
            return result;
        }

        public int SaveInsurancePolicy(string _desc, string _cre)
        {

            _generalDAL = new GeneralDAL();
            InventoryDAL inv = new InventoryDAL();

            _generalDAL.ConnectionOpen();
            int result = _generalDAL.SaveInsurancePolicy(_desc, _cre);
            inv.ConnectionClose();
            _generalDAL.ConnectionClose();
            return result;
        }

        public int SaveInsuranceCompany(MasterOutsideParty _outPar)
        {
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            MasterOutsideParty outPar = _generalDAL.GetOutsideParty(_outPar.Mbi_cd);
            if (outPar == null)
            {
                int result = _generalDAL.SaveOutsideParty(_outPar);
                _generalDAL.ConnectionClose();
                return result;
            }
            else
                return -999;

        }


        public DataTable GetInsuranceInvoice(string _com, string _pc, DateTime _from, DateTime _to, string _type, string _veh, string _acc, string _invNo, string _enginNo, string _chassisNo)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetInsuranceInvoice(_com, _pc, _from, _to, _type, _veh, _acc, _invNo, _enginNo, _chassisNo);
        }

        public List<VehicleInsuarance> GetInsuranceByRef(string _recNo)
        {
            _generalDAL = new GeneralDAL();
            List<VehicleInsuarance> _vehicalInsurance = _generalDAL.GetVehicalInsurance(_recNo, null);
            if (_vehicalInsurance != null)
            {
                return _vehicalInsurance;
            }
            else
            {
                return null;
            }
        }

        public List<VehicleInsuarance> GetInsuranceDetails(string _type, string _recNo)
        {
            _generalDAL = new GeneralDAL();
            List<VehicleInsuarance> _vehicalInsurance = _generalDAL.GetVehicalInsurance(_recNo, null);
            if (_vehicalInsurance != null)
            {
                if (_type == "IssInsCovNot")
                {
                    if (_vehicalInsurance[0].Svit_cvnt_issue == 0)
                    {
                        return _vehicalInsurance;
                    }
                    else
                        return null;
                }
                else if (_type == "ExtCovNot")
                {
                    if (_vehicalInsurance[0].Svit_cvnt_issue == 1)
                    {
                        return _vehicalInsurance;
                    }
                    else
                        return null;
                }
                else if (_type == "IssCer")
                {
                    if (_vehicalInsurance[0].Svit_cvnt_issue == 1)
                    {
                        return _vehicalInsurance;
                    }
                    else
                        return null;
                }
                else if (_type == "SetDebNot")
                {
                    if (_vehicalInsurance[0].Svit_cvnt_issue == 1 && _vehicalInsurance[0].Svit_dbt_set_stus == false)
                    {
                        return _vehicalInsurance;
                    }
                    else
                        return null;
                }
                else
                {

                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public List<VehicleInsuarance> GetVehicalInsuarance(string _recNo, string _debNo)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetVehicalInsurance(_recNo, _debNo);
        }

        //kapila
        public int GetWeek(DateTime _Date, out Decimal _week, string _com)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetWeek(_Date, out  _week, _com);
        }

        public Int32 SaveVehicalInsurance(VehicleInsuarance insurance, MasterAutoNumber _receiptAuto)
        {
            int result = 0;
            try
            {

                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                InventoryDAL _inventoryDAL = new InventoryDAL();


                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                _inventoryDAL.ConnectionOpen();
                _inventoryDAL.BeginTransaction();

                if (_receiptAuto != null)
                {
                    _receiptAuto.Aut_modify_dt = null;
                    _receiptAuto = _inventoryDAL.GetAutoNumber(_receiptAuto.Aut_moduleid, (short)_receiptAuto.Aut_direction, _receiptAuto.Aut_start_char, _receiptAuto.Aut_cate_tp, _receiptAuto.Aut_cate_cd, _receiptAuto.Aut_modify_dt, _receiptAuto.Aut_year);
                    string _cusNo = "";

                    if (_receiptAuto.Aut_cate_cd == "CN001")
                        _cusNo = _receiptAuto.Aut_start_char + "/" + _receiptAuto.Aut_year.ToString().Substring(2) + "/1" + _receiptAuto.Aut_number.ToString("000000", CultureInfo.InvariantCulture);

                    if (_receiptAuto.Aut_cate_cd == "JS001")
                        _cusNo = _receiptAuto.Aut_start_char + _receiptAuto.Aut_year + _receiptAuto.Aut_number.ToString("000000", CultureInfo.InvariantCulture);

                    if (_receiptAuto.Aut_cate_cd == "UAL01")
                        _cusNo = _receiptAuto.Aut_start_char + "/89" + _receiptAuto.Aut_number.ToString("0000", CultureInfo.InvariantCulture);

                    _receiptAuto.Aut_modify_dt = null;
                    _inventoryDAL.UpdateAutoNumber(_receiptAuto);

                    insurance.Svit_cvnt_no = _cusNo;
                }

                result = _generalDAL.SaveVehicalInsurance(insurance);

                _generalDAL.TransactionCommit();
                _inventoryDAL.TransactionCommit();
                result = 1;
            }
            catch (Exception err)
            {
                result = -1;
                _inventoryDAL.TransactionRollback();
                _generalDAL.TransactionRollback();
            }

            return result;
        }

        public Int32 SaveInsurancePay(VehicalInsurancePay _insPay)
        {
            _generalDAL = new GeneralDAL();
            InventoryDAL inv = new InventoryDAL();
            inv.ConnectionOpen();
            _generalDAL.ConnectionOpen();
            int result = _generalDAL.SaveInsurancePay(_insPay);
            inv.ConnectionClose();
            _generalDAL.ConnectionClose();
            return result;

        }

        public Int32 SaveInsuranceClaim(VehicalInsuranceClaim _insClaim)
        {
            _generalDAL = new GeneralDAL();
            InventoryDAL _inv = new InventoryDAL();
            _inv.ConnectionOpen();
            _generalDAL.ConnectionOpen();
            _insClaim.Seq_no = _inv.GetSerialID();
            int result = _generalDAL.SaveInsuranceClaim(_insClaim);
            _inv.ConnectionClose();
            _generalDAL.ConnectionClose();
            return result;
        }

        public List<VehicleInsuarance> GetClaimCusDetails(string _regNo)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetClaimCusDetails(_regNo);
        }

        public List<VehicalInsuranceClaim> GetClaimDetails(string type, string _regNo, DateTime date)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetClaimDetails(type, _regNo, date);
        }

        public List<MsgInformation> GetMsgInformation(string _company, string _location, string _documenttype)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetMsgInformation(_company, _location, _documenttype);
        }

        public Int32 UpdateCoverNote(string _invNo, string _itmCd, string _com)
        {

            _generalDAL = new GeneralDAL();
            _inventoryRepDAL = new ReptCommonDAL();
            Int32 result = 0; ;
            using (TransactionScope tr = new TransactionScope())
            {
                _generalDAL.ConnectionOpen();
                _inventoryRepDAL.ConnectionOpen();
                _generalDAL.UpdateItemCoverNote(_invNo, _itmCd);
                _inventoryRepDAL.UpdateTemCoverNote(_invNo, _com);
                tr.Complete();
                _generalDAL.ConnectionClose();
                _inventoryRepDAL.ConnectionClose();
            }

            return result;
        }

        #region Vehicle Approval
        //Shani
        public List<VehicalRegistration> GetVehiclesByInvoiceNo(string com, string pc, string invNo)
        {
            _generalDAL = new GeneralDAL();
            List<VehicalRegistration> list = new List<VehicalRegistration>();

            list = _generalDAL.GetVehiclesByInvoiceNo(com, pc, invNo);
            return list;
        }
        public List<ReptPickSerials> getserialByInvoiceNum(string docNo, string com)
        {
            _inventoryRepDAL = new ReptCommonDAL();
            return _inventoryRepDAL.getserialByInvoiceNum(docNo, com);
        }
        public Int32 Update_VehicleApproveStatus(string p_com, Int32 p_usrseq_no, string p_engineNo, string p_chasseNo, Int32 isApprove)
        {
            _inventoryRepDAL = new ReptCommonDAL();
            _inventoryRepDAL.ConnectionOpen();
            Int32 effect = _inventoryRepDAL.Update_VehicleApproveStatus(p_com, p_usrseq_no, p_engineNo, p_chasseNo, isApprove);
            _inventoryRepDAL.ConnectionClose();
            return effect;
        }
        public Int32 Update_ListVehicleApproveStatus(string p_com, List<ReptPickSerials> approvedList, DateTime approvedDate, string approvedBy, string inoviceNo)
        {
            Int32 effect = 0;
            Int32 effect2 = 0;
            Int32 effect3 = 0;

            using (TransactionScope tr = new TransactionScope())
            {
                _inventoryRepDAL = new ReptCommonDAL();
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _inventoryRepDAL.ConnectionOpen();
                foreach (ReptPickSerials rps in approvedList)
                {
                    effect = effect + _generalDAL.Update_VehicleApproveStatus(p_com, inoviceNo, rps.Tus_base_doc_no, rps.Tus_itm_cd, 1);//update sat_veh_reg_txn and sat_itm

                    // effect = effect + _inventoryRepDAL.Update_VehicleApproveStatus(p_com, rps.Tus_usrseq_no, rps.Tus_ser_1, rps.Tus_ser_2, 1); //update temp_pick_ser
                    //  effect2 = effect2 + Update_VehicleApproveStatus(p_com, rps.Tus_usrseq_no, rps.Tus_ser_1, rps.Tus_ser_2, 1);

                    effect3 = effect3 + _generalDAL.Save_sat_rls_itm(rps, rps.Tus_base_doc_no, approvedDate, approvedBy); //insert record into sat_rls_itm 
                }
                _generalDAL.ConnectionClose();
                _inventoryRepDAL.ConnectionClose();
                tr.Complete();

            }

            return effect;
        }
        public Int32 Update_VehicleApprove_Status(string p_com, string p_invoiceNo, string p_invoiceRefNo, string p_itmCD, Int32 isapp)
        {
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            Int32 effect = 0;

            effect = _generalDAL.Update_VehicleApproveStatus(p_com, p_invoiceNo, p_invoiceRefNo, p_itmCD, isapp);

            _generalDAL.ConnectionClose();
            return effect;
        }
        // public DataTable SearchInvoiceNo_forVehicle(string com, string pc, string AccountNo, string _regNo, string InvoiceDate)//pass null for that are not relevant.
        public DataTable SearchInvoiceNo_forVehicle(string com, string pc, string AccountNo, string _regNo, string InvoiceFromDate, string InvoiceToDate)
        {
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            DateTime InvoiceDt = DateTime.MinValue;
            DateTime InvoiceToDt = DateTime.MinValue;
            if (InvoiceFromDate != null)
            {
                InvoiceDt = Convert.ToDateTime(InvoiceFromDate);
            }
            if (InvoiceToDate != null)
            {
                InvoiceToDt = Convert.ToDateTime(InvoiceToDate);
            }

            DataTable dt = _generalDAL.SearchInvoiceNo_forVehicle(com, pc, AccountNo, _regNo, InvoiceFromDate, InvoiceToDate);

            return dt;
        }
        public DataTable Get_RequiredDocuments(string schemeCD)
        {
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();

            DataTable dt = _generalDAL.Get_RequiredDocuments(schemeCD);

            return dt;
        }
        #endregion
        public DataTable Get_DetBy_town(string town)
        {
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            DataTable dt = _generalDAL.Get_DetBy_town(town);
            return dt;
        }

        public Int32 SaveBackDate(BackDates _backdate, int _Dayend, Day_End_Log _log, DayEnd _dayend, bool noLoc, bool isPc, bool dayEndModule, out string _err)
        {
            Int32 effect = 0;
            _generalDAL = new GeneralDAL();
            FinancialDAL _financialDal = new FinancialDAL();
            try
            {
                //using (TransactionScope tr = new TransactionScope())
                //{
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();

                _financialDal.ConnectionOpen();
                _financialDal.BeginTransaction();

                //if (_Dayend > 0)
                //{
                //    //day end process
                //    FinancialDAL fin = new FinancialDAL();
                //    fin.ConnectionOpen();
                //    if (noLoc && isPc)
                //    {
                //        int _numdays = DateTime.Now.Subtract(_backdate.Gad_act_from_dt).Days;
                //        List<MasterLocation> loclist = GetLocationByCompany(_backdate.Gad_com);
                //        for (int tem = 0; tem < loclist.Count; tem++)
                //        {

                //            for (int i = 0; i < _numdays; i++)
                //            {
                //                _generalDAL.ConnectionOpen();
                //                _log.Upd_pc = loclist[tem].Ml_loc_cd;
                //                _log.Upd_dt = _backdate.Gad_act_from_dt.AddDays(i);
                //                _generalDAL.SaveDayEndLog(_log);
                //                _dayend.Upd_pc = loclist[tem].Ml_loc_cd;
                //                _dayend.Upd_dt = _backdate.Gad_act_from_dt.AddDays(i);
                //                fin.SaveDayEnd(_dayend);
                //            }

                //        }
                //    }
                //    else if (noLoc && !isPc)
                //    {
                //        int _numdays = DateTime.Now.Subtract(_backdate.Gad_act_from_dt).Days;

                //        DataTable _pcDt = _generalDAL.GetPartyCodes(_backdate.Gad_com, "%");
                //        for (int tem = 0; tem < _pcDt.Rows.Count; tem++)
                //        {



                //            for (int i = 0; i < _numdays; i++)
                //            {
                //                _generalDAL.ConnectionOpen();
                //                _log.Upd_pc = _pcDt.Rows[tem]["mpc_cd"].ToString();
                //                _log.Upd_dt = _backdate.Gad_act_from_dt.AddDays(i);
                //                _generalDAL.SaveDayEndLog(_log);
                //                _ dayend.Upd_pc = _pcDt.Rows[tem]["mpc_cd"].ToString();
                //                _dayend.Upd_dt = _backdate.Gad_act_from_dt.AddDays(i);
                //                fin.SaveDayEnd(_dayend);

                //            }

                //        }
                //    }
                //    else if (!noLoc)
                //    {

                //        int _numdays = DateTime.Now.Subtract(_backdate.Gad_act_from_dt).Days;
                //        for (int i = 0; i < _numdays; i++)
                //        {
                //            _log.Upd_dt = _backdate.Gad_act_from_dt.AddDays(i);
                //            _generalDAL.SaveDayEndLog(_log);
                //            _dayend.Upd_dt = _backdate.Gad_act_from_dt.AddDays(i);
                //            fin.SaveDayEnd(_dayend);

                //        }

                //    }
                //    fin.ConnectionClose();
                //}


                if (dayEndModule || _Dayend > 0)
                {

                    if (noLoc && isPc)
                    {
                        int _numdays = DateTime.Now.Subtract(_backdate.Gad_act_from_dt).Days;
                        List<MasterLocation> loclist = GetLocationByCompany(_backdate.Gad_com);
                        for (int tem = 0; tem < loclist.Count; tem++)
                        {
                            List<DayEnd> _list = _financialDal.GetDayEnds(_backdate.Gad_com, loclist[tem].Ml_loc_cd, _backdate.Gad_act_from_dt, DateTime.Now);

                            foreach (DayEnd day in _list)
                            {
                                Day_End_Log _daylog = new Day_End_Log();
                                _daylog.Upd_com = day.Upd_com;
                                _daylog.Upd_cre_by = day.Upd_cre_by;
                                _daylog.Upd_cre_dt = day.Upd_cre_dt;
                                _daylog.Upd_dt = day.Upd_dt;
                                _daylog.Upd_log_dt = DateTime.Now;
                                _daylog.Upd_ov_wrt = day.Upd_ov_wrt;
                                _daylog.Upd_pc = day.Upd_pc;
                                _generalDAL.SaveDayEndLog(_daylog);
                            }
                            _financialDal.DeleteDayEnd(_backdate.Gad_com, loclist[tem].Ml_loc_cd, _backdate.Gad_act_from_dt, DateTime.Now);
                        }
                    }

                    else if (noLoc && !isPc)
                    {

                        DataTable _pcDt = _generalDAL.GetPartyCodes(_backdate.Gad_com, "%");
                        for (int tem = 0; tem < _pcDt.Rows.Count; tem++)
                        {

                            List<DayEnd> _list = _financialDal.GetDayEnds(_backdate.Gad_com, _pcDt.Rows[tem]["mpc_cd"].ToString(), _backdate.Gad_act_from_dt, DateTime.Now);

                            foreach (DayEnd day in _list)
                            {
                                Day_End_Log _daylog = new Day_End_Log();
                                _daylog.Upd_com = day.Upd_com;
                                _daylog.Upd_cre_by = day.Upd_cre_by;
                                _daylog.Upd_cre_dt = day.Upd_cre_dt;
                                _daylog.Upd_dt = day.Upd_dt;
                                _daylog.Upd_log_dt = DateTime.Now;
                                _daylog.Upd_ov_wrt = day.Upd_ov_wrt;
                                _daylog.Upd_pc = day.Upd_pc;
                                _generalDAL.SaveDayEndLog(_daylog);
                            }

                            _financialDal.DeleteDayEnd(_backdate.Gad_com, _pcDt.Rows[tem]["mpc_cd"].ToString(), _backdate.Gad_act_from_dt, DateTime.Now);
                        }
                    }

                    else if (!noLoc)
                    {
                        List<DayEnd> _list = _financialDal.GetDayEnds(_backdate.Gad_com, _backdate.Gad_loc, _backdate.Gad_act_from_dt, DateTime.Now);

                        foreach (DayEnd day in _list)
                        {
                            Day_End_Log _daylog = new Day_End_Log();
                            _daylog.Upd_com = day.Upd_com;
                            _daylog.Upd_cre_by = day.Upd_cre_by;
                            _daylog.Upd_cre_dt = day.Upd_cre_dt;
                            _daylog.Upd_dt = day.Upd_dt;
                            _daylog.Upd_log_dt = DateTime.Now;
                            _daylog.Upd_ov_wrt = day.Upd_ov_wrt;
                            _daylog.Upd_pc = day.Upd_pc;
                            _generalDAL.SaveDayEndLog(_daylog);
                        }
                        _financialDal.DeleteDayEnd(_backdate.Gad_com, _backdate.Gad_loc, _backdate.Gad_act_from_dt, DateTime.Now);
                    }
                    //_financialDal.TransactionCommit(); 
                    //_financialDal.ConnectionClose();
                }

                effect = _generalDAL.SaveBackDate(_backdate);

                _financialDal.TransactionCommit();
                _generalDAL.TransactionCommit();
                _err = string.Empty;
                //_generalDAL.ConnectionClose();

                //    tr.Complete();
                //}
            }
            catch (Exception err)
            {
                effect = -1;
                _err = "ERROR : " + err.Message.ToString();
                _financialDal.TransactionRollback();
                _generalDAL.TransactionRollback();
            }
            return effect;
        }

        public Int32 CheckBackDate(string _com, string _ope)
        {
            Int32 effect = 0;
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            effect = _generalDAL.CheckBackDate(_com, _ope);
            _generalDAL.ConnectionClose();
            return effect;
        }

        public Int32 SaveDayEndLog(Day_End_Log _log)
        {
            Int32 effect = 0;
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            effect = _generalDAL.SaveDayEndLog(_log);
            _generalDAL.ConnectionClose();
            return effect;

        }

        public string GetCoverNoteNo(MasterAutoNumber _receiptAuto, string _type)
        {
            InventoryDAL _inventoryDAL = new InventoryDAL();
            _inventoryDAL.ConnectionOpen();
            MasterAutoNumber _number = _inventoryDAL.GetAutoNumber(_receiptAuto.Aut_moduleid, (short)_receiptAuto.Aut_direction, _receiptAuto.Aut_start_char, _receiptAuto.Aut_cate_tp, _receiptAuto.Aut_cate_cd, _receiptAuto.Aut_modify_dt, _receiptAuto.Aut_year);
            string _cusNo = "";
            if (_type == "Cover")
                if (_receiptAuto.Aut_cate_cd == "JS001")
                {
                    _cusNo = _receiptAuto.Aut_start_char + _number.Aut_number.ToString("000000", CultureInfo.InvariantCulture);
                }
                else
                {
                    _cusNo = _receiptAuto.Aut_start_char + "/" + _number.Aut_number.ToString("000000", CultureInfo.InvariantCulture);
                }
            else
                _cusNo = _receiptAuto.Aut_cate_cd + "-" + _number.Aut_start_char + "-" + _number.Aut_number.ToString("00000", CultureInfo.InvariantCulture); ;
            return _cusNo;
        }

        public DataTable GetVehicalSearch(string _com, string _loc, string _type, string _reg, string _chassis, string _engine, string _inv, string _rec, string _acc, DateTime _fromdate, DateTime _todate, string _cust)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetVehicalSearch(_com, _loc, _type, _reg, _chassis, _engine, _inv, _rec, _acc, _fromdate, _todate, _cust);
        }
        public DataTable GetRegAndInsSearch(string _item, string _model, string _sales, string _term, string _pc, string _com)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetRegAndInsSearch(_item, _model, _sales, _term, _pc, _com);
        }

        public DataTable GetHpSch(string _inv)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetHpSch(_inv);
        }

        public Int32 UpdateVehReg(VehicalRegistration _vehReg)
        {
            int effect = 0;
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            effect = _generalDAL.UpdateVehReg(_vehReg);
            _generalDAL.ConnectionClose();
            return effect;
        }

        public DataTable Get_All_User_paramTypes(string type)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.Get_All_User_paramTypes(type);
        }
        public List<MasterSalesPriorityHierarchy> GetPCHeirachy(string _loc, string com)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetPCHeirachy(_loc, com);
        }

        #region PC Definition

        public Int32 Save_profit_center(MasterProfitCenter _pcheader, string _chnl, string _schnl, string _area, string _reg, string _zone, string _user, out string _err)
        {
            int effect = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();

                _saleDAL = new SalesDAL();
                _saleDAL.ConnectionOpen();

                effect = _generalDAL.Save_profit_center(_pcheader, _user);
                _err = "";

                MasterSalesPriorityHierarchy infoCom = new MasterSalesPriorityHierarchy();
                infoCom.Mpi_act = true;
                infoCom.Mpi_pc_cd = _pcheader.Mpc_cd;
                infoCom.Mpi_com_cd = _pcheader.Mpc_com;
                infoCom.Mpi_tp = "PC_PRIT_HIERARCHY";
                infoCom.Mpi_val = _pcheader.Mpc_com;
                infoCom.Mpi_cd = "COM";
                effect = _generalDAL.Save_MST_PC_INFO(infoCom);

                MasterSalesPriorityHierarchy infoPC = new MasterSalesPriorityHierarchy();
                infoPC.Mpi_act = true;
                infoPC.Mpi_pc_cd = _pcheader.Mpc_cd;
                infoPC.Mpi_com_cd = _pcheader.Mpc_com;
                infoPC.Mpi_tp = "PC_PRIT_HIERARCHY";
                infoPC.Mpi_val = _pcheader.Mpc_cd;
                infoPC.Mpi_cd = "PC";
                effect = _generalDAL.Save_MST_PC_INFO(infoPC);

                MasterSalesPriorityHierarchy infoChnl = new MasterSalesPriorityHierarchy();
                infoChnl.Mpi_act = true;
                infoChnl.Mpi_pc_cd = _pcheader.Mpc_cd;
                infoChnl.Mpi_com_cd = _pcheader.Mpc_com;
                infoChnl.Mpi_tp = "PC_PRIT_HIERARCHY";
                infoChnl.Mpi_val = _chnl;
                infoChnl.Mpi_cd = "CHNL";
                effect = _generalDAL.Save_MST_PC_INFO(infoChnl);

                MasterSalesPriorityHierarchy infoSChnl = new MasterSalesPriorityHierarchy();
                infoSChnl.Mpi_act = true;
                infoSChnl.Mpi_pc_cd = _pcheader.Mpc_cd;
                infoSChnl.Mpi_com_cd = _pcheader.Mpc_com;
                infoSChnl.Mpi_tp = "PC_PRIT_HIERARCHY";
                infoSChnl.Mpi_val = _schnl;
                infoSChnl.Mpi_cd = "SCHNL";
                effect = _generalDAL.Save_MST_PC_INFO(infoSChnl);

                MasterSalesPriorityHierarchy infoArea = new MasterSalesPriorityHierarchy();
                infoArea.Mpi_act = true;
                infoArea.Mpi_pc_cd = _pcheader.Mpc_cd;
                infoArea.Mpi_com_cd = _pcheader.Mpc_com;
                infoArea.Mpi_tp = "PC_PRIT_HIERARCHY";
                infoArea.Mpi_val = _area;
                infoArea.Mpi_cd = "AREA";
                effect = _generalDAL.Save_MST_PC_INFO(infoArea);

                MasterSalesPriorityHierarchy infoReg = new MasterSalesPriorityHierarchy();
                infoReg.Mpi_act = true;
                infoReg.Mpi_pc_cd = _pcheader.Mpc_cd;
                infoReg.Mpi_com_cd = _pcheader.Mpc_com;
                infoReg.Mpi_tp = "PC_PRIT_HIERARCHY";
                infoReg.Mpi_val = _reg;
                infoReg.Mpi_cd = "REGION";
                effect = _generalDAL.Save_MST_PC_INFO(infoReg);

                MasterSalesPriorityHierarchy infoZone = new MasterSalesPriorityHierarchy();
                infoZone.Mpi_act = true;
                infoZone.Mpi_pc_cd = _pcheader.Mpc_cd;
                infoZone.Mpi_com_cd = _pcheader.Mpc_com;
                infoZone.Mpi_tp = "PC_PRIT_HIERARCHY";
                infoZone.Mpi_val = _zone;
                infoZone.Mpi_cd = "ZONE";
                effect = _generalDAL.Save_MST_PC_INFO(infoZone);

                //By Akila 2017/12/04 - Save HPR_SYS_PARA Details

                //Get existing parameters for default pc
                //ABL--> RAMP, LRP --> VAMP, AAL ---> AAZPG these are hard corded values

                if (_pcheader != null)
                {
                    DataTable _defaultParameters = new DataTable();
                    _defaultParameters = _generalDAL.GetSysParaDetails(_pcheader.Mpc_com, _pcheader.Mpc_chnl, "DEFAULTPC");

                    string _type = "PC";
                    string _code = string.Empty;
                    if (_defaultParameters.Rows.Count > 0)
                    {
                        _type = _defaultParameters.Rows[0]["msp_rest_cate_tp"] == DBNull.Value ? string.Empty : _defaultParameters.Rows[0]["msp_rest_cate_tp"].ToString();
                        _code = _defaultParameters.Rows[0]["msp_rest_cate_cd"] == DBNull.Value ? string.Empty : _defaultParameters.Rows[0]["msp_rest_cate_cd"].ToString();

                        if ((!string.IsNullOrEmpty(_type)) && (!string.IsNullOrEmpty(_code)))
                        {
                            List<Hpr_SysParameter> _parameters = new List<Hpr_SysParameter>();
                            _parameters = _generalDAL.GetHprSysParaDetails(_type, _code);
                            if (_parameters != null && _parameters.Count > 0)
                            {
                                foreach (Hpr_SysParameter _parameter in _parameters)
                                {
                                    //duplicate existing data for new PC
                                    _parameter.Hsy_seq = _generalDAL.GetSerialID();
                                    _parameter.Hsy_pty_cd = _pcheader.Mpc_cd;
                                    effect = _saleDAL.Save_hpr_sys_para(_parameter);
                                }
                            }
                        }
                    }
                }
              

                _generalDAL.ConnectionClose();
                _saleDAL.ConnectionClose();
            }
            catch (Exception err)
            {
                effect = -1;
                _err = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();
                _saleDAL.TransactionRollback();
            }
            return effect;

        }
        public Int32 Update_profit_center(MasterProfitCenter _pcheader)
        {
            Int32 effect = 0;
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();

            effect = _generalDAL.Update_profit_center(_pcheader);

            _generalDAL.ConnectionClose();

            return effect;
        }
        public Int32 Save_MST_PC_INFO(List<MasterSalesPriorityHierarchy> _pcInfoHeaders, Dictionary<string, string> code_and_value)
        {
            Int32 effect = 0;
            _generalDAL = new GeneralDAL();
            _saleDAL = new SalesDAL();
            try
            {
                using (TransactionScope _tr = new TransactionScope())
                {
                    _generalDAL.ConnectionOpen();
                    _saleDAL.ConnectionOpen();

                    List<MasterSalesPriorityHierarchy> _totalHierarchy = _saleDAL.GetSalesPriorityHierarchyWithDescription("PC_PRIT_HIERARCHY", _pcInfoHeaders[0].Mpi_com_cd, string.Empty);
                    foreach (MasterSalesPriorityHierarchy pc_info in _pcInfoHeaders)
                    {
                        foreach (string code in code_and_value.Keys)
                        {
                            pc_info.Mpi_cd = code;
                            pc_info.Mpi_val = code_and_value[code];
                            if (code == "PC") pc_info.Mpi_val = pc_info.Mpi_pc_cd;
                            if (code == "CHNL") _saleDAL.UpdateProfitCenterChannel(pc_info.Mpi_com_cd, pc_info.Mpi_pc_cd, pc_info.Mpi_val);

                            effect = _generalDAL.Save_MST_PC_INFO(pc_info);
                            var _isExist = _totalHierarchy.Where(x => x.Mpi_cd == code && x.Mpi_pc_cd == pc_info.Mpi_pc_cd && x.Mpi_com_cd == pc_info.Mpi_com_cd).ToList();

                            if (_isExist != null)
                                if (_isExist.Count > 0)
                                {
                                    //TODO: Insert Record to history
                                    effect = _generalDAL.Update_MST_PC_INFO(pc_info);
                                }

                        }
                    }

                    _generalDAL.ConnectionClose();
                    effect = 1;
                    _tr.Complete();
                }

                return effect;
            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public List<MasterSalesPriorityHierarchy> Get_AllPc_info(string com, string pc, string code, string type)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.Get_AllPc_info(com, pc, code, type);
        }
        public Int32 Update_MST_PC_INFO(MasterSalesPriorityHierarchy pc_info, Dictionary<string, string> code_and_value)
        {
            Int32 effect = 0;
            _generalDAL = new GeneralDAL();
            try
            {
                using (TransactionScope _tr = new TransactionScope())
                {
                    _generalDAL.ConnectionOpen();
                    //foreach (MasterSalesPriorityHierarchy pc_info in _pcInfoHeaders)
                    //{
                    foreach (string code in code_and_value.Keys)
                    {
                        pc_info.Mpi_cd = code;
                        pc_info.Mpi_val = code_and_value[code];
                        effect = _generalDAL.Update_MST_PC_INFO(pc_info);
                    }
                    //}

                    _generalDAL.ConnectionClose();
                    effect = 1;
                    _tr.Complete();
                }

                return effect;
            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public Int32 Save_MST_REC_DIV(List<MasterReceiptDivision> recHeaderList, List<string> pcList)
        {
            Int32 effect = 0;
            _generalDAL = new GeneralDAL();
            try
            {
                using (TransactionScope _tr = new TransactionScope())
                {
                    _generalDAL.ConnectionOpen();
                    foreach (string pc_CODE in pcList)
                    {
                        foreach (MasterReceiptDivision recDiv in recHeaderList)
                        {
                            //  recDiv.Msrd_pc=pc.Mpc_cd;
                            recDiv.Msrd_pc = pc_CODE;
                            effect = _generalDAL.Save_MST_REC_DIV(recDiv);
                        }
                    }
                    _generalDAL.ConnectionClose();
                    effect = 1;
                    _tr.Complete();
                }
                return effect;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }


        public Int32 Save_MST_PC_CHG(List<MasterProfitCenterCharges> chargePcList)
        {
            Int32 effect = 0;
            _generalDAL = new GeneralDAL();
            try
            {
                using (TransactionScope _tr = new TransactionScope())
                {
                    _generalDAL.ConnectionOpen();
                    foreach (MasterProfitCenterCharges pc_chg in chargePcList)
                    {
                        effect = _generalDAL.Save_MST_PC_CHG(pc_chg);
                    }
                    _generalDAL.ConnectionClose();
                    effect = 1;
                    _tr.Complete();
                }
                return effect;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public Int32 Save_SAR_TXN_PAY_TP(List<PaymentType> txnPTList, List<string> pc_List, Boolean _isNew)
        {
            Int32 effect = 0;
            _generalDAL = new GeneralDAL();
            try
            {
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                if (_isNew == true)
                {
                    foreach (PaymentType pc_txn in txnPTList)
                    {
                        foreach (string pc_ in pc_List)
                        {
                            pc_txn.Stp_pty_cd = pc_;
                            effect = _generalDAL.Save_SAR_TXN_PAY_TP(pc_txn);
                        }

                    }
                }
                else
                {
                    foreach (PaymentType pc_txn in txnPTList)
                    {
                        effect = _generalDAL.Update_SAR_TXN_PAY_TP(pc_txn.Stp_circ, pc_txn.Stp_to_dt);
                    }
                }
                _generalDAL.TransactionCommit();
                effect = 1;

                return effect;
            }
            catch (Exception ex)
            {
                _generalDAL.TransactionRollback();

                return -1;
            }

        }

        public Int32 Update_SAR_TXN_PAY_TP(string _Stp_circ, DateTime _Stp_to_dt)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.Update_SAR_TXN_PAY_TP(_Stp_circ, _Stp_to_dt);
        }

        public List<MasterProfitCenterCharges> Get_latest_PcCharges(string com, string pc)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.Get_latest_PcCharges(com, pc);
        }
        #endregion PC Definition

        public List<RequestApprovalDetail> GetRequestByRef(string _com, string _pc, string _ref)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetRequestByRef(_com, _pc, _ref);
        }

        public int GetReprintDocCount(string docNo)
        {
            int effect = 0;
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            effect = _generalDAL.GetReprintDocCount(docNo);
            _generalDAL.ConnectionClose();
            return effect;

        }

        public int GetPrintDocCopies(string _com, string _docName)
        {
            int effect = 0;
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            effect = _generalDAL.GetPrintDocCopies(_com, _docName);
            _generalDAL.ConnectionClose();
            return effect;

        }


        public Boolean IsUser_InRole(string userCompany, string userId, string roleId)
        {
            Int32 effect = 0;
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            effect = _generalDAL.IsUser_InRole(userCompany, userId, roleId);
            _generalDAL.ConnectionClose();

            if (effect > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public DataTable Get_backdates(string com, string pc_or_loc_Code, string p_pc_or_loc, out List<BackDates> backDatesList)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.Get_backdates(com, pc_or_loc_Code, p_pc_or_loc, out backDatesList);
        }

        //darshana 12-03-2013
        public RequestApprovalHeader GetRelatedRequestByRef(string _com, string _pc, string _ref, string _appTP)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetRelatedRequestByRef(_com, _pc, _ref, _appTP);
        }

        public Int32 Save_RequestApprove_Ser(RequestApprovalSerials ReqApp_ser)
        {
            int effect = 0;
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            effect = _generalDAL.Save_RequestApprove_Ser(ReqApp_ser);
            _generalDAL.ConnectionClose();
            return effect;
        }
        //public Int32 Save_RequestApprove_Ser_Log(List<RequestApprovalSerials> ReqApp_ser, Int32 approval_level)
        //{
        //    int effect = 0;
        //    _generalDAL = new GeneralDAL();       

        //    using (TransactionScope tr = new TransactionScope())
        //    {
        //        _generalDAL.ConnectionOpen();
        //        _inventoryRepDAL.ConnectionOpen();
        //        foreach (RequestApprovalSerials ras in ReqApp_ser)
        //        {
        //            effect = _generalDAL.Save_RequestApprove_Ser_Log(ras, approval_level);
        //        }

        //        tr.Complete();
        //        _generalDAL.ConnectionClose();

        //    }
        //    return effect;
        //}

        public Int32 Save_RequestApprove_Ser_and_log(List<RequestApprovalSerials> ReqApp_ser, Int32 approveUser_level)
        {
            int effect = 0;
            _generalDAL = new GeneralDAL();

            using (TransactionScope tr = new TransactionScope())
            {
                _generalDAL.ConnectionOpen();

                Int32 line = 1;
                foreach (RequestApprovalSerials ras in ReqApp_ser)
                {
                    ras.Gras_line = line;
                    effect = _generalDAL.Save_RequestApprove_Ser(ras);
                    effect = _generalDAL.Save_RequestApprove_Ser_Log(ras, approveUser_level);
                    line = line + 1;
                }

                tr.Complete();
                _generalDAL.ConnectionClose();

            }
            return effect;
        }

        public Int32 SaveHirePurchasRequest_NEW(MasterAutoNumber reqAuto, RequestApprovalHeader _hdr, List<RequestApprovalDetail> _det, RequestApprovalHeaderLog _hdrlog, List<RequestApprovalDetailLog> _delLog, int UserPermissionLevel, bool _isApprovalUser, bool _isRequestGenerateUser, out string _userSeqNo, out string requestStatus)
        {
            Int32 _effect = 0;

            _generalDAL = new GeneralDAL();
            //int _userSeq = -1;

            InventoryDAL _inventoryDAL = new InventoryDAL();
            using (TransactionScope _tr = new TransactionScope())
            {
                _generalDAL.ConnectionOpen();
                bool _isNewRequest = string.IsNullOrEmpty(_hdr.Grah_ref) ? true : false;
                requestStatus = _hdr.Grah_app_stus;
                if (_isRequestGenerateUser)
                {//level 0

                    //******************newly adde**********************
                    _inventoryDAL.ConnectionOpen();
                    MasterAutoNumber _number = _inventoryDAL.GetAutoNumber(reqAuto.Aut_moduleid, (short)reqAuto.Aut_direction, reqAuto.Aut_start_char, reqAuto.Aut_cate_tp, reqAuto.Aut_cate_cd, reqAuto.Aut_modify_dt, reqAuto.Aut_year);
                    string _cusNo = reqAuto.Aut_cate_cd + "-" + reqAuto.Aut_start_char + "-" + _number.Aut_number.ToString("000000", CultureInfo.InvariantCulture);
                    _inventoryDAL.UpdateAutoNumber(reqAuto);
                    _inventoryDAL.ConnectionOpen();


                    _hdr.Grah_ref = _cusNo;
                    _hdrlog.Grah_ref = _cusNo;
                    //*************************************************
                    // _userSeq = _generalDAL.GetSerialID();
                    //_hdrlog.Grah_ref = Convert.ToString(_userSeq);

                    _generalDAL.SaveRequestApproveHear(_hdr);
                    foreach (RequestApprovalDetail _itm in _det)
                    {
                        _itm.Grad_ref = _hdr.Grah_ref;//Convert.ToString(_userSeq);
                        _generalDAL.Save_RequestApprove_Det(_itm);
                    }
                }

                else
                {
                    //level 1,2,3

                    if (_isNewRequest)
                    {
                        //******************newly adde**********************
                        _inventoryDAL.ConnectionOpen();
                        MasterAutoNumber _number = _inventoryDAL.GetAutoNumber(reqAuto.Aut_moduleid, (short)reqAuto.Aut_direction, reqAuto.Aut_start_char, reqAuto.Aut_cate_tp, reqAuto.Aut_cate_cd, reqAuto.Aut_modify_dt, reqAuto.Aut_year);
                        string _cusNo = reqAuto.Aut_cate_cd + "-" + reqAuto.Aut_start_char + "-" + _number.Aut_number.ToString("000000", CultureInfo.InvariantCulture);
                        _inventoryDAL.UpdateAutoNumber(reqAuto);
                        _inventoryDAL.ConnectionOpen();

                        _hdr.Grah_ref = _cusNo;
                        _hdrlog.Grah_ref = _cusNo;
                        //*************************************************
                        // _userSeq = _generalDAL.GetSerialID();                    

                        _generalDAL.SaveRequestApproveHear(_hdr);

                        foreach (RequestApprovalDetail _itm in _det)
                        {

                            _itm.Grad_ref = _cusNo; ;//_userSeq.ToString();

                            _generalDAL.Save_RequestApprove_Det(_itm);
                        }
                    }

                    //Delete original record in detail
                    if (_isApprovalUser)
                    {
                        _generalDAL.DeleteRequestHeaderDetail(_hdr.Grah_ref);
                        foreach (RequestApprovalDetail _itm in _det)
                        {
                            _generalDAL.Save_RequestApprove_Det(_itm);
                        }

                    }

                }
                _hdrlog.Grah_ref = _hdr.Grah_ref;
                try
                {
                    _generalDAL.SaveRequestApproveHear_Log(_hdrlog);
                }
                catch (Exception ex) { }

                foreach (RequestApprovalDetailLog _itm in _delLog)
                {
                    _itm.Grad_ref = _hdr.Grah_ref;
                    _generalDAL.Save_RequestApprove_Det_Log(_itm);
                }
                //Add new code by Chamal De Silva 03-08-2012
                _generalDAL.UpdateMidApprovalStatus(_hdr);

                if (_isApprovalUser)
                {
                    //Update Status
                    //_hdr.Grah_app_stus = "A";
                    _generalDAL.UpdateApprovalStatus(_hdr);
                }

                _generalDAL.ConnectionClose();
                _effect = 1;
                _userSeqNo = _hdr.Grah_ref;
                requestStatus = _hdr.Grah_app_stus;
                _tr.Complete();
            }
            _userSeqNo = _hdr.Grah_ref;
            return _effect;
        }

        //Shani 13-03-2013
        public Int32 Save_RequestApprove_forReceiptReverse(List<RequestApprovalSerials> ReqApp_ser, MasterAutoNumber reqAuto, RequestApprovalHeader _hdr, List<RequestApprovalDetail> _det, RequestApprovalHeaderLog _hdrlog, List<RequestApprovalDetailLog> _delLog, int UserPermissionLevel, bool _isApprovalUser, bool _isRequestGenerateUser, out string _userReqNo, out string newRequstsStatus)
        {
            int effect = 0;
            _generalDAL = new GeneralDAL();

            using (TransactionScope tr = new TransactionScope())
            {
                bool _isNewRequest = string.IsNullOrEmpty(_hdr.Grah_ref) ? true : false;
                // _generalDAL.ConnectionOpen();
                string reqStatus = "";
                newRequstsStatus = reqStatus;
                Int32 eff = SaveHirePurchasRequest_NEW(reqAuto, _hdr, _det, _hdrlog, _delLog, UserPermissionLevel, _isApprovalUser, _isRequestGenerateUser, out _userReqNo, out reqStatus);

                //--------------------------save serials--------------------------------------------------
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                Int32 line = 1;
                if (ReqApp_ser != null)
                {
                    foreach (RequestApprovalSerials ras in ReqApp_ser)
                    {
                        ras.Gras_line = line;
                        ras.Gras_ref = _userReqNo;
                        if (_isNewRequest == true)
                        {
                            effect = _generalDAL.Save_RequestApprove_Ser(ras);
                        }
                        effect = _generalDAL.Save_RequestApprove_Ser_Log(ras, UserPermissionLevel);
                        line = line + 1;
                    }
                }
                newRequstsStatus = reqStatus;
                effect = 1;
                tr.Complete();
                _generalDAL.ConnectionClose();

            }
            return effect;
        }

        public DataTable Get_gen_reqapp_ser(string com, string reqNo, out List<RequestApprovalSerials> List)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.Get_gen_reqapp_ser(com, reqNo, out List);
        }

        public DataTable GetWeekDefinition(Int32 _month, Int32 _year, Int32 _week, out DateTime _from, out DateTime _to, string _com, string _tp)
        {
            DateTime _fromDate = Convert.ToDateTime("31/Dec/9999");
            DateTime _toDate = Convert.ToDateTime("31/Dec/9999");

            _generalDAL = new GeneralDAL();
            DataTable _weekDef = _generalDAL.GetWeekDefinition(_month, _year, _week, _com, _tp);
            if (_weekDef.Rows.Count == 1)
            {
                _fromDate = Convert.ToDateTime(_weekDef.Rows[0]["gw_from_dt"]);
                _toDate = Convert.ToDateTime(_weekDef.Rows[0]["gw_to_dt"]);
            }
            _from = _fromDate;
            _to = _toDate;
            return _weekDef;
        }

        public DataTable Get_PC_Hierarchy(string _company, string _profitCenter)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.Get_PC_Hierarchy(_company, _profitCenter);
        }

        public Int32 Save_approve_ser_and_Log(List<RequestApprovalSerials> ReqApp_ser, bool _isNewRequest, string _userReqNo, int UserPermissionLevel, bool isFinalApprovalLevel)
        {
            Int32 effect = 0;
            using (TransactionScope tr = new TransactionScope())
            {
                //--------------------------save serials--------------------------------------------------
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                Int32 line = 1;
                foreach (RequestApprovalSerials ras in ReqApp_ser)
                {
                    ras.Gras_line = line;
                    ras.Gras_ref = _userReqNo;
                    if (_isNewRequest == true)
                    {
                        effect = _generalDAL.Save_RequestApprove_Ser(ras);
                    }
                    try
                    {
                        effect = _generalDAL.Save_RequestApprove_Ser_Log(ras, UserPermissionLevel);
                    }
                    catch (Exception ex)
                    {
                    }

                    line = line + 1;
                }

                if (isFinalApprovalLevel == true)
                {
                    _generalDAL.Delete_gen_reqapp_ser(_userReqNo);

                    line = 1;
                    foreach (RequestApprovalSerials ras in ReqApp_ser)
                    {
                        ras.Gras_line = line;
                        ras.Gras_ref = _userReqNo;
                        effect = _generalDAL.Save_RequestApprove_Ser(ras);
                        line = line + 1;
                    }
                }
                effect = 1;
                tr.Complete();
                _generalDAL.ConnectionClose();

            }
            return effect;

        }


        public Int32 save_mst_loc_info(List<MasterLocationPriorityHierarchy> _locInfoHeaders, Dictionary<string, string> code_and_value)
        {
            Int32 effect = 0;
            _generalDAL = new GeneralDAL();
            _saleDAL = new SalesDAL();
            try
            {
                using (TransactionScope _tr = new TransactionScope())
                {
                    _generalDAL.ConnectionOpen();
                    _saleDAL.ConnectionOpen();
                    List<MasterLocationPriorityHierarchy> _totalHierarchy = _saleDAL.GetLocationPriorityHierarchyWithDescription("LOC_PRIT_HIERARCHY", _locInfoHeaders[0].Mli_com_cd, string.Empty);
                    foreach (MasterLocationPriorityHierarchy pc_info in _locInfoHeaders)
                    {
                        foreach (string code in code_and_value.Keys)
                        {
                            pc_info.Mli_cd = code;
                            pc_info.Mli_val = code_and_value[code];
                            if (code == "LOC") pc_info.Mli_val = pc_info.Mli_loc_cd;

                            effect = _generalDAL.save_mst_loc_info(pc_info);

                            var _duplicate = _totalHierarchy.Where(x => x.Mli_cd == code && x.Mli_loc_cd == pc_info.Mli_loc_cd && x.Mli_com_cd == pc_info.Mli_com_cd).ToList();
                            if (_duplicate != null)
                                if (_duplicate.Count > 0)
                                {
                                    //TODO: Insert record to history
                                }

                        }
                    }

                    _generalDAL.ConnectionClose();
                    _saleDAL.ConnectionClose();
                    effect = 1;
                    _tr.Complete();
                }

                return effect;
            }
            catch
            {
                return -1;
            }

        }

        public DataTable GetAllReceiptDivision(string _company, string _profitcenter)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetAllReceiptDivision(_company, _profitcenter);
        }

        public Int32 Delete_Doc_Check_List(string _com, string _pc, DateTime monthYear, Int32 week)
        {
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            Int32 result = _generalDAL.Delete_Doc_Check_List(_com, _pc, monthYear, week);
            _generalDAL.ConnectionClose();
            return result;
        }

        public Int32 Save_Doc_Check_List(string createBy, DateTime createDt, string com, string pc, Int32 month, Int32 year, Int32 week, DateTime monthYear)
        {
            _generalDAL = new GeneralDAL();

            _generalDAL.ConnectionOpen();
            _generalDAL.Save_Doc_Check_List(createBy, createDt, com, pc, month, year, week, monthYear);
            _generalDAL.ConnectionClose();
            return 1;
        }

        public DataTable Get_Doc_Check_List(string com, string pc, DateTime monthYear, Int32 week)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.Get_Doc_Check_List(com, pc, monthYear, week);
        }

        public Int32 Update_Doc_Check_List(List<DocCheckList> _docLst)
        {
            Int32 eff = 0;
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            foreach (DocCheckList _doc in _docLst)
            {
                eff = _generalDAL.Update_Doc_Check_List(_doc);
            }
            _generalDAL.ConnectionClose();
            return eff;
        }

        public DataTable Get_Party_Types(string _cd)
        {
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            DataTable X = _generalDAL.Get_Party_Types(_cd);
            _generalDAL.ConnectionClose();
            return X;
        }

        public DataTable GetUnReadMessages(string _receiver, string _company, string _location, string _profitcenter)
        {
            _inventoryRepDAL = new ReptCommonDAL();
            return _inventoryRepDAL.GetUnReadMessages(_receiver, _company, _location, _profitcenter);
        }


        public DataTable Get_Partycodes(string _pType, string _cd)
        {
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            DataTable X = _generalDAL.Get_Partycodes(_pType, _cd);
            _generalDAL.ConnectionClose();
            return X;
        }

        public DataTable GetOperationAdminTeam(string _company)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetOperationAdminTeam(_company);
        }
        public DataTable GetLocationType()
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetLocationType();
        }
        public DataTable GetLocationGrade(string _company, string _loctype)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetLocationGrade(_company, _loctype);
        }
        public DataTable GetLocationCategory1()
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetLocationCategory1();
        }
        public DataTable GetLocationCategory2(string _company)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetLocationCategory2(_company);
        }
        public DataTable GetLocationCategory3()
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetLocationCategory3();
        }
        public DataTable GetCountry()
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetCountry();
        }
        public DataTable GetProvince()
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetProvince();
        }
        public DataTable GetDistrict(string _province)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetDistrict(_province);
        }
        public DataTable GetTown(string _province, string _district)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetTown(_province, _district);
        }
        public DataTable GetOutletDepartment(string _company)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetOutletDepartment(_company);

        }
        public DataTable GetEmployeeCategory()
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetEmployeeCategory();
        }

        public List<RequestApprovalHeader> GetAllRequests(string _com, string _pc, string _type, string _status)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetAllRequests(_com, _pc, _type, _status);
        }

        public Int32 Confirm_Check_List(string _com, string _pc, DateTime _month, Int32 _week, Int32 _SR, Int32 _RT, Int32 _SE, DateTime _cour_date, string _POD, string _user, DateTime _ho_dt)
        {
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            Int32 X = _generalDAL.Confirm_Check_List(_com, _pc, _month, _week, _SR, _RT, _SE, _cour_date, _POD, _user, _ho_dt);
            _generalDAL.ConnectionClose();
            return X;
        }

        public Boolean IsConfirmCheckList(string _com, string _pc, DateTime _month, Int32 _week, string _type)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.IsConfirmCheckList(_com, _pc, _month, _week, _type);
        }

        public Boolean IsDayEndDone4ScanDocs(string _com, string _pc, DateTime _date)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.IsDayEndDone4ScanDocs(_com, _pc, _date);
        }
        public DataTable Get_GET_GPC(string gpc, string desc)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.Get_GET_GPC(gpc, desc);
        }
        public DataTable GetReqAppStatusLog(string _ref)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetReqAppStatusLog(_ref);
        }
        public DataTable GetReqAppStatusLogInItems(string _ref, Int32 _level)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetReqAppStatusLogInItems(_ref, _level);
        }
        public DataTable GetReqAppStatusLogOutItems(string _ref, Int32 _level)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetReqAppStatusLogOutItems(_ref, _level);
        }
        public DataTable Get_hpr_insu(string sch_cd, string pty_tp, string pty_cd)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.Get_hpr_insu(sch_cd, pty_tp, pty_cd);
        }

        public DataTable GetArea(string _comapny, string _area)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetArea(_comapny, _area);
        }

        public DataTable GetProfitCenterCharge(string _company, string _profitcenter)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetProfitCenterCharge(_company, _profitcenter);
        }

        public DataTable GetMainCategoryDetail(string _mainCat)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetMainCategoryDetail(_mainCat);
        }

        //Added by Udesh 22-Oct-2018
        public DataTable GetModelDetail(string _modelCode)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetModelDetail(_modelCode);
        }

        public bool IsSaleFigureRoundUp(string _company)
        {
            bool _isok = false;
            _generalDAL = new GeneralDAL();
            DataTable _result = _generalDAL.IsSaleFigureRoundUp(_company);
            if (_result != null && _result.Rows.Count > 0)
            {
                if (_result.Rows[0].Field<Int16>("MC_ANAL12") == 1) _isok = true; else _isok = false;
            }
            else _isok = false;


            return _isok;

        }

        public DataTable GetCrdSaleDocsData(string _saleTp)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetCrdSaleDocsData(_saleTp);
        }

        public DataTable GetCrdSaleDocsSavedData(Int32 _seq)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetCrdSaleDocsSavedData(_seq);
        }

        public Boolean checkCredSaleDocs(string _eng, out Int32 _seq_no)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.checkCredSaleDocs(_eng, out _seq_no);
        }
        public RequestApprovalHeader GetRequest_HeaderByRef(string _com, string _pc, string _ref)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetRequest_HeaderByRef(_com, _pc, _ref);
        }


        public Int32 UpdateCredSaleDocAllIssue(DateTime _gdh_iss_dt, Int32 _gdh_seq)
        {
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            Int32 X = _generalDAL.UpdateCredSaleDocAllIssue(_gdh_iss_dt, _gdh_seq);
            _generalDAL.ConnectionClose();
            return X;
        }
        public DataTable Get_ITEMS_ONSelect(string _mainCt, string _SubCt, string _Range, string _code, string _brand, string _type)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.Get_ITEMS_ONSelect(_mainCt, _SubCt, _Range, _code, _brand, _type);
        }

        public DataTable GetAC_SevCharge_itmes(string _company, string itemCode)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetAC_SevCharge_itmes(_company, itemCode);
        }

        public Int32 Save_MST_ITM_BLOCK(List<MasterItemBlock> itmBlockList, Int32 approveUser_level)
        {
            #region MyRegion
            //int effect = 0;
            //_generalDAL = new GeneralDAL();
            //using (TransactionScope tr = new TransactionScope())
            //{
            //    _generalDAL.ConnectionOpen();

            //    foreach (MasterItemBlock itmBlock in itmBlockList)
            //    {
            //        effect += _generalDAL.Save_MST_ITM_BLOCK(itmBlock);
            //    }
            //    //effect = 1;
            //    tr.Complete();
            //    _generalDAL.ConnectionClose();
            //}
            //return effect; 
            #endregion

            // Edit by thara 2014-09-27

            int effect = 0;
            _generalDAL = new GeneralDAL();

            try
            {
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                foreach (MasterItemBlock itmBlock in itmBlockList)
                {
                    effect += _generalDAL.Save_MST_ITM_BLOCK(itmBlock);
                }
                _generalDAL.TransactionCommit();

            }
            catch (Exception ex)
            {
                effect = -1;
                _generalDAL.TransactionRollback();
            }

            return effect;
        }

        //public Int32 Save_MST_ITM_BLOCK(List<MasterItemBlock> itmBlockList, Int32 approveUser_level)
        //{
        //    int effect = 0;
        //    _generalDAL = new GeneralDAL();
        //    using (TransactionScope tr = new TransactionScope())
        //    {
        //        _generalDAL.ConnectionOpen();

        //        foreach (MasterItemBlock itmBlock in itmBlockList)
        //        {
        //            effect = _generalDAL.Save_MST_ITM_BLOCK(itmBlock);
        //        }
        //        effect = 1;
        //        tr.Complete();
        //        _generalDAL.ConnectionClose();
        //    }
        //    return effect;
        //}

        public DataTable Get_sar_price_type(string _tpCode)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.Get_sar_price_type(_tpCode);
        }

        public int SaveIncentiveSchPC(List<IncentiveSchInc> _detSchInc, List<IncentiveSchIncDet> _detSchIncDet, List<IncentiveSch> _detSch, List<IncentiveSchPC> _detail, List<IncentiveSchDet> _incDet, List<IncentiveSchStkTp> _detStkTp, List<IncentiveSchPersn> _detPerson, List<IncentiveSchSerial> _detSerial, List<IncentiveSchPB> _detPB, List<IncentiveSchMode> _detMode)
        {
            int result = 0;

            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();

            //schemes incentive
            if (_detSchInc != null)
            {
                foreach (IncentiveSchInc _det5 in _detSchInc)
                {
                    result = _generalDAL.SaveIncentiveSchInc(_det5);
                }
            }

            //schemes incentive detail
            if (_detSchIncDet != null)
            {
                foreach (IncentiveSchIncDet _det6 in _detSchIncDet)
                {
                    result = _generalDAL.SaveIncentiveSchIncDet(_det6);
                }
            }

            //schemes
            if (_detSch != null)
            {
                foreach (IncentiveSch _det4 in _detSch)
                {
                    result = _generalDAL.SaveIncentiveScheme(_det4);
                }
            }

            //scheme details
            if (_incDet != null)
            {
                foreach (IncentiveSchDet _inc_det in _incDet)
                {
                    result = _generalDAL.SaveIncentiveSchDet(_inc_det);
                }
            }

            // scheme profit centers
            if (_detail != null)
            {
                foreach (IncentiveSchPC _det in _detail)
                {
                    result = _generalDAL.SaveIncentiveSchPC(_det);
                }
            }

            //stock types
            if (_detStkTp != null)
            {
                foreach (IncentiveSchStkTp _det1 in _detStkTp)
                {
                    result = _generalDAL.SaveIncentiveSchStockTp(_det1);
                }
            }

            //applicable party
            if (_detPerson != null)
            {
                foreach (IncentiveSchPersn _det2 in _detPerson)
                {
                    result = _generalDAL.SaveIncentiveSchPerson(_det2);
                }
            }

            //serials
            if (_detSerial != null)
            {
                foreach (IncentiveSchSerial _det3 in _detSerial)
                {
                    result = _generalDAL.SaveIncentiveSchSerial(_det3);
                }
            }

            //price book
            if (_detPB != null)
            {
                foreach (IncentiveSchPB _det7 in _detPB)
                {
                    result = _generalDAL.SaveIncentiveSchPB(_det7);
                }
            }
            //pay mode
            if (_detMode != null)
            {
                foreach (IncentiveSchMode _det8 in _detMode)
                {
                    result = _generalDAL.SaveIncentiveSchMode(_det8);
                }
            }
            _generalDAL.ConnectionClose();
            return result;
        }

        public List<MasterItemStatus> GetAllStockTypes(string _comp)
        {
            _generalDAL = new GeneralDAL();
            List<MasterItemStatus> _list = _generalDAL.GetAllStockTypes(_comp);
            return _list;

        }

        public DataTable GetBusDeptByCode(string _code)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetBusDeptByCode(_code);
        }
        public DataTable getDetail_on_serial2(string _company, string _location, string _serial1)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getDetail_on_serial2(_company, _location, _serial1);
        }
        public DataTable GetPayCircByCode(string _com, string _circ)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetPayCircByCode(_com, _circ);
        }
        public DataTable GetBusDesigByCode(string _code)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetBusDesigByCode(_code);
        }
        public DataTable GetTownByCode(string _code)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetTownByCode(_code);
        }
        public DataTable GetDistrictByCode(string _code)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetDistrictByCode(_code);
        }
        public DataTable GetProvinceByCode(string _code)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetProvinceByCode(_code);
        }
        public Int32 SaveVehicalRegistrationDefinition_NEW(List<VehicalRegistrationDefnition> VehRegDef_List)
        {
            //_generalDAL = new GeneralDAL();
            //_generalDAL.ConnectionOpen();
            //InventoryDAL inv = new InventoryDAL();
            //inv.ConnectionOpen();
            //Int32 result = _generalDAL.SaveVehicalRegistrationDefinition(pcs, items, from, to, satlesType, regVal, calVal, cre, isMan);
            //_generalDAL.ConnectionClose();
            //inv.ConnectionClose();
            //return result;

            Int32 _effects = 0;
            _generalDAL = new GeneralDAL();
            InventoryDAL inv = new InventoryDAL();
            using (TransactionScope _tr = new TransactionScope())
            {
                _generalDAL.ConnectionOpen();
                inv.ConnectionOpen();
                foreach (VehicalRegistrationDefnition defn in VehRegDef_List)
                {
                    Int32 result = _generalDAL.SaveVehicalRegistrationDefinition_NEW(defn);
                }

                _generalDAL.ConnectionClose();
                inv.ConnectionClose();
                _effects = 1;
                _tr.Complete();
            }
            return _effects;

        }

        public List<VehicalRegistrationDefnition> Get_vehRegDefnByCircular(string _circular)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.Get_vehRegDefnByCircular(_circular);
        }

        public Int32 Update_Veh_reg_defn(string _circular, DateTime fromNew, DateTime ToNew)
        {
            Int32 _effects = 0;
            _generalDAL = new GeneralDAL();

            using (TransactionScope _tr = new TransactionScope())
            {
                _generalDAL.ConnectionOpen();

                _generalDAL.Update_Veh_reg_defn(_circular, fromNew, ToNew);
                _generalDAL.ConnectionClose();
                _effects = 1;
                _tr.Complete();
            }
            return _effects;
        }

        public DataTable GetReceiptDivision(string _company, List<string> _profitcenter)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetReceiptDivision(_company, _profitcenter);
        }
        //T-----------------------------------------------------------------------------------------
        public int Save_AccountAgreementDetails(string AccountNo, AccountAgreement _agreement, List<AccountAgreementDoc> _docList)
        {
            Int32 _effects = 0;
            _generalDAL = new GeneralDAL();

            //using (TransactionScope _tr = new TransactionScope())
            //{
            try
            {
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                _effects = _generalDAL.Save_AccountAgreement(_agreement);

                foreach (AccountAgreementDoc Doc in _docList)
                {
                    _generalDAL.Save_AccountAgreement_Doc(Doc);
                }

                _effects = 1;
                // _generalDAL.ConnectionClose();
                _generalDAL.TransactionCommit();

            }
            catch (Exception ex)
            {

                _effects = -1;
                _generalDAL.TransactionRollback();
            }
            // _tr.Complete();
            //}
            return _effects;

        }

        public Decimal GetNotReciveCount(string accNo)
        {
            Decimal _effects = 0;
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();

            _effects = _generalDAL.GetNotReciveCount(accNo);
            _generalDAL.ConnectionClose();
            return _effects;
        }

        public DataTable Get_AccountAgreementHdr(string accountNo)
        {
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();

            DataTable dt = _generalDAL.Get_AccountAgreementHdr(accountNo);

            _generalDAL.ConnectionClose();

            return dt;
        }


        public DataTable Get_PromotorType()
        {
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();

            DataTable dt = _generalDAL.Get_PromotorType();

            _generalDAL.ConnectionClose();

            return dt;
        }

        public List<AccountAgreementDoc> Get_HPT_AccountAgreementDocs(string accountNo, string isRecieved)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.Get_HPT_AccountAgreementDocs(accountNo, isRecieved);
        }

        public List<MST_PROMOTOR> Get_PROMOTOR(string _CODE, string _MOB, string _NIC)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.Get_PROMOTOR(_CODE, _MOB, _NIC);
        }

        public int Save_AccountAgreement_MisMatch_Details(string AccountNo, AccountAgreementContact AgrContact, AccountAgreementScheme AgrSchm)
        {
            Int32 _effects = 0;
            _generalDAL = new GeneralDAL();

            //using (TransactionScope _tr = new TransactionScope())
            //{
            try
            {
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();

                // _effects = _generalDAL.Save_AccountAgreementContact(AgrContact);
                _effects = _generalDAL.Save_AccountAgreementContact_new(AgrContact);
                _effects = _generalDAL.Save_AccountAgreementScheme(AgrSchm);

                _effects = 1;
                // _generalDAL.ConnectionClose();
                _generalDAL.TransactionCommit();

            }
            catch (Exception ex)
            {
                _effects = -1;
                _generalDAL.TransactionRollback();
            }
            // _tr.Complete();
            //}
            return _effects;

        }
        public int Save_AccountAgreementProduct(AccountAgreementProduct _agrProduct)
        {
            Int32 _effects = 0;
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            _effects = _generalDAL.Save_AccountAgreementProduct(_agrProduct);
            _generalDAL.ConnectionClose();
            return _effects;
        }

        public Int32 Delete_AccountAgreementProduct(string _accountNo, string itemCode)
        {
            Int32 _effects = 0;
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            _effects = _generalDAL.Delete_AccountAgreementProduct(_accountNo, itemCode);
            _generalDAL.ConnectionClose();
            return _effects;

        }

        public AccountAgreementContact Get_Accounts_Agreement_Contact(string _accountNo)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.Get_Accounts_Agreement_Contact(_accountNo);
        }
        public AccountAgreementScheme Get_Accounts_Agreement_Scheme(string _accountNo)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.Get_Accounts_Agreement_Scheme(_accountNo);
        }

        public DataTable Get_Account_Agreement_StartDt(string pc)
        {
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            DataTable dt = _generalDAL.Get_Account_Agreement_StartDt(pc);
            _generalDAL.ConnectionClose();
            return dt;

        }
        public int Save_accountAgreement_startdate(AccountAgreementStartDate _startDt)
        {
            Int32 _effects = 0;
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            _effects = _generalDAL.Save_accountAgreement_startdate(_startDt);
            _generalDAL.ConnectionClose();
            return _effects;

        }

        public List<AccountAgreementProduct> Get_Acc_Agreement_products(string accountNo)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.Get_Acc_Agreement_products(accountNo);
        }


        public int DuplicateCashDiscount(string _oldCircular, string _newCircular, DateTime _from, DateTime _to, decimal _discountRate, decimal _discountValue, List<string> _items, List<string> _location, string _creby, DateTime _credate, string _com, out string _error)
        {

            try
            {
                _saleDAL = new SalesDAL();
                _saleDAL.ConnectionOpen();
                _saleDAL.BeginTransaction();
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                _inventoryDAL = new InventoryDAL();
                _inventoryDAL.ConnectionOpen();
                _inventoryDAL.BeginTransaction();
                //get header list

                List<CashPromotionDiscountHeader> _header = _saleDAL.GetPromotionalHeader(_oldCircular);
                if (_header != null && _header.Count > 0)
                {
                    foreach (CashPromotionDiscountHeader _hdr in _header)
                    {
                        int seq = _inventoryDAL.GetSerialID();
                        List<CashPromotionDiscountDetail> _detail = _saleDAL.GetPromotinalDiscountDetail(_hdr.Spdh_seq);
                        int line = 1;
                        foreach (CashPromotionDiscountDetail _det in _detail)
                        {
                            _det.Spdd_seq = seq;
                            _det.Spdd_line = line;
                            _det.Spdd_from_dt = _from;
                            _det.Spdd_to_dt = _to;
                            _det.Spdd_disc_rt = _discountRate;
                            _det.Spdd_disc_val = _discountValue;
                            _det.Spdd_cre_by = _creby;
                            _det.Spdd_cre_dt = _credate;
                            _det.Spdd_stus = 1;
                            _saleDAL.SavePromotionalDiscountDetails(_det);
                            foreach (string itm in _items)
                            {
                                CashPromotionDiscountItem _itm = new CashPromotionDiscountItem();
                                _itm.Spdi_seq = seq;
                                _itm.Spdi_line = line;
                                _itm.Spdi_itm = itm;
                                _itm.Spdi_act = true;
                                _itm.Spdi_cre_by = _creby;
                                _itm.Spdi_cre_dt = _credate;
                                _itm.Spdi_act = true;
                                _saleDAL.SavePromotionalDiscountItem(_itm);
                                _saleDAL.UpdateCashPromotionalDiscountItem(_hdr.Spdh_seq, _creby, _credate, itm, 0);
                            }
                            line++;
                        }
                        //line = 1;

                        foreach (string loc in _location)
                        {
                            CashPromotionDiscountLocation _loc = new CashPromotionDiscountLocation();
                            _loc.Spdl_seq = seq;
                            _loc.Spdl_pc = loc;
                            _loc.Spdl_com = _com;
                            _loc.Spdl_act = true;
                            _loc.Spdl_cre_by = _creby;
                            _loc.Spdl_cre_dt = _credate;
                            _loc.Spdl_act = true;
                            _saleDAL.SavePromotionalDiscountLocation(_loc);
                            //  _saleDAL.UpdateCashPromotionalDiscountLocation(_hdr.Spdh_seq, _creby, _credate, loc, 0);
                        }

                        _hdr.Spdh_circular = _newCircular;
                        _hdr.Spdh_from_dt = _from;
                        _hdr.Spdh_to_dt = _to;
                        _hdr.Spdh_seq = seq;
                        _hdr.Spdh_cre_by = _creby;
                        _hdr.Spdh_cre_dt = _credate;

                        _saleDAL.SavePromotionalDiscountHeader(_hdr);

                    }


                }

                _saleDAL.TransactionCommit();
                _generalDAL.TransactionCommit();
                _inventoryDAL.TransactionCommit();
            }
            catch (Exception ex)
            {
                _saleDAL.TransactionRollback();
                _generalDAL.TransactionRollback();
                _inventoryDAL.TransactionRollback();
                _error = ex.Message;
                return -1;
            }
            _error = "";
            return 1;
        }

        public int UpdateCashPromotionalDiscountHdr(int _seq, DateTime _from, DateTime _to, decimal _disval, decimal _disrt, string user, string session)
        {
            int result = 0;
            _generalDAL = new GeneralDAL();
            _saleDAL = new SalesDAL();
            _saleDAL.ConnectionOpen();
            _saleDAL.BeginTransaction();
            CashPromotionDiscountHeader _header = _generalDAL.GetPromotionalDiscountBySeq(_seq);

            LogPriceDef _log = new LogPriceDef();
            _log.From_dt = _header.Spdh_from_dt;
            _log.Log_rmk = "Edit";
            _log.Log_session = session;
            _log.Log_usr = user;
            _log.Promo_cd = _header.Spdh_seq.ToString();
            _log.To_dt = _header.Spdh_to_dt;
            _log.Cir_no = _header.Spdh_circular;
            _saleDAL.SaveDiscountDefinitionLog(_log);

            _saleDAL.TransactionCommit();

            _generalDAL.ConnectionOpen();
            result = _generalDAL.UpdateCashPromotionalDiscountHdr(_seq, _from, _to, _disval, _disrt);
            _generalDAL.ConnectionClose();
            return result;
        }

        public int updateESDStatus(string _com, string _pc, string _epf, DateTime _month)
        {
            int result = 0;
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            result = _generalDAL.updateESDStatus(_com, _pc, _epf, _month);
            _generalDAL.ConnectionClose();
            return result;
        }

        public Int32 Save_MST_PC_INFO_LOG(List<MasterSalesPriorityHierarchyLog> _pcInfoHeaders, Dictionary<string, string> code_and_value, out string _msg)
        {
            Int32 effect = 0;
            _generalDAL = new GeneralDAL();
            _saleDAL = new SalesDAL();
            try
            {
                using (TransactionScope _tr = new TransactionScope())
                {
                    _generalDAL.ConnectionOpen();
                    _saleDAL.ConnectionOpen();
                    foreach (MasterSalesPriorityHierarchyLog pc_info in _pcInfoHeaders)
                    {
                        foreach (string code in code_and_value.Keys)
                        {
                            MasterSalesPriorityHierarchy _one = new MasterSalesPriorityHierarchy();
                            pc_info.Mpil_cd = code;
                            pc_info.Mpil_val = code_and_value[code];
                            if (code == "PC") pc_info.Mpil_val = pc_info.Mpil_pc_cd;
                            DateTime _now = DateTime.Now.Date;
                            DateTime _from = pc_info.Mpil_frm_dt;
                            DateTime _to = pc_info.Mpil_to_dt;
                            if (_from.Date <= _now.Date) pc_info.Mpil_isupdt = true; else pc_info.Mpil_isupdt = false;
                            if (code == "CHNL" && _from.Date <= _now.Date) _saleDAL.UpdateProfitCenterChannel(pc_info.Mpil_com_cd, pc_info.Mpil_pc_cd, pc_info.Mpil_val);
                            if (_from.Date <= _now.Date) _generalDAL.UpdateInvoiceStructure(pc_info.Mpil_com_cd, pc_info.Mpil_pc_cd, _from, _to, code, pc_info.Mpil_val);
                            effect = _generalDAL.Save_MST_PC_INFO_log(pc_info);
                            if (_from.Date <= _now.Date)
                            {
                                _one.Mpi_act = pc_info.Mpil_act;
                                _one.Mpi_cd = pc_info.Mpil_cd;
                                _one.Mpi_com_cd = pc_info.Mpil_com_cd;
                                _one.Mpi_pc_cd = pc_info.Mpil_pc_cd;
                                _one.Mpi_tp = pc_info.Mpil_tp;
                                _one.Mpi_val = pc_info.Mpil_val;
                                _generalDAL.Save_MST_PC_INFO(_one);
                            }


                            // Nadeeka 21-05-2015
                            if (code == "SCHNL" && !String.IsNullOrEmpty(pc_info.Mpil_val))
                            {
                                if (pc_info.Mpil_val != "N/A")
                                {
                                    _saleDAL.AssignChannelAccestoPC(pc_info.Mpil_val, pc_info.Mpil_pc_cd, pc_info.Mpil_com_cd, pc_info.Mpil_mod_by);
                                }
                            }

                        }
                    }
                    _generalDAL.ConnectionClose();
                    effect = 1;
                    _tr.Complete();
                }
                _msg = string.Empty;
                return effect;
            }
            catch (Exception ex)
            {
                _msg = ex.Message;
                return -1;
            }
        }
        public Int32 save_mst_loc_info_log(List<MasterLocationPriorityHierarchyLog> _locInfoHeaders, Dictionary<string, string> code_and_value, out string _msg)
        {
            Int32 effect = 0;
            _generalDAL = new GeneralDAL();
            _saleDAL = new SalesDAL();
            try
            {
                using (TransactionScope _tr = new TransactionScope())
                {
                    _generalDAL.ConnectionOpen();
                    _saleDAL.ConnectionOpen();
                    foreach (MasterLocationPriorityHierarchyLog pc_info in _locInfoHeaders)
                    {
                        foreach (string code in code_and_value.Keys)
                        {
                            DateTime _now = DateTime.Now.Date;
                            DateTime _from = pc_info.Mlil_frm_dt;
                            DateTime _to = pc_info.Mlil_to_dt;
                            pc_info.Mlil_cd = code;
                            pc_info.Mlil_val = code_and_value[code];
                            if (code == "LOC") pc_info.Mlil_val = pc_info.Mlil_loc_cd;
                            if (_from.Date <= _now.Date) pc_info.Mlil_isupdt = true; else pc_info.Mlil_isupdt = false;
                            effect = _generalDAL.save_mst_loc_info_log(pc_info);
                            MasterLocationPriorityHierarchy _one = new MasterLocationPriorityHierarchy();
                            if (_from.Date <= _now.Date)
                            {
                                _one.Mli_act = pc_info.Mlil_act;
                                _one.Mli_cd = pc_info.Mlil_cd;
                                _one.Mli_com_cd = pc_info.Mlil_com_cd;
                                _one.Mli_loc_cd = pc_info.Mlil_loc_cd;
                                _one.Mli_tp = pc_info.Mlil_tp;
                                _one.Mli_val = pc_info.Mlil_val;
                                _generalDAL.save_mst_loc_info(_one);
                            }
                        }
                    }
                    _generalDAL.ConnectionClose();
                    _saleDAL.ConnectionClose();
                    effect = 1;
                    _tr.Complete();
                }
                _msg = string.Empty;
                return effect;
            }
            catch (Exception ex)
            {
                _msg = ex.Message;
                return -1;
            }

        }

        public DataTable GetInforBackDate(string _company, string _module)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetInforBackDate(_company, _module);
        }

        public DataTable SearchRequestApprovalDetails(string _com, string _usr, string _type, DateTime _frmDt, DateTime _toDt, string _stus, string _baseTp)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.SearchRequestApprovalDetails(_com, _usr, _type, _frmDt, _toDt, _stus, _baseTp);
        }
        public DataTable SearchrequestAppDetByRef(string _ref)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.SearchrequestAppDetByRef(_ref);
        }

        public void ProcessRccAgent()
        {

            Process.Start("C:\\Users\\Sachith\\Desktop\\RCC Agent - SACHITH\\EMS_Upload_Console\\bin\\Debug\\RCC_AGENT.exe");
        }

        public List<RequestApprovalHeader> GetPendingSRNRequest(string _com, string _pc, string _inv, string _req)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetPendingSRNRequest(_com, _pc, _inv, _req);
        }

        public List<RequestApprovalHeader> GetPendingExchangeRequest(string _com, string _pc, string _itm, string _app)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetPendingExchangeRequest(_com, _pc, _itm, _app);
        }

        public Int32 sp_updateexchangeissuenew(List<RequestApprovalDetail> _list, Boolean isdutyfree = false)
        {
            _generalDAL = new GeneralDAL();
            Int32 _isdutyfree = 0;
            if (isdutyfree == true)
            {
                _isdutyfree = 1;
            }
            _generalDAL.ConnectionOpen();
            int _eff = 0;
            foreach (RequestApprovalDetail _itm in _list)
            {
                _eff = _generalDAL.sp_updateexchangeissuenew(_itm.Grad_ref, _itm.Grad_line, _itm.Grad_anal1, _itm.Grad_anal10, _itm.Grad_anal11, _itm.Grad_val5, _isdutyfree, _itm.Grad_val4, _itm.Grad_val2);
            }
            _generalDAL.TransactionCommit();
            return _eff;
        }
        public Int32 UpdateExchangeApprovalStatus(RequestApprovalHeader _UpdateApproval, List<RequestApprovalDetail> _AppDet, bool _isItemChanged, RequestApprovalHeaderLog _AppHdrLog, List<RequestApprovalDetailLog> _AppDetLog, List<RequestApprovalSerialsLog> _AppSerLog)
        {
            Int32 _effects = 0;
            Int32 _line = 0;
            _generalDAL = new GeneralDAL();
            _inventoryDAL = new InventoryDAL();
            try
            {
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                _inventoryDAL.ConnectionOpen();
                _inventoryDAL.BeginTransaction();

                _generalDAL.UpdateApprovalStatus(_UpdateApproval);
                _generalDAL.UpdateExchangeRemark(_UpdateApproval.Grah_ref, _UpdateApproval.Grad_anal6);
                if (_isItemChanged)
                {
                    if (_AppDet != null)
                    {
                        _generalDAL.InactiveOldIssueItem(_AppDet[0].Grad_ref);

                        foreach (RequestApprovalDetail _itm in _AppDet)
                        {

                            //if (_itm.Grad_anal5 == "EX-RECEIVE")
                            //{
                            //    _generalDAL.Update_RequestApprove_Det(_itm);// Nadeeka
                            //}
                            //else
                            //{

                            _itm.Grad_line = _generalDAL.GetExchangeNextLineNo(_itm.Grad_ref);
                            _itm.Grad_is_rt1 = true;
                            _generalDAL.Save_RequestApprove_Det(_itm);
                            //}



                        }
                    }
                }

                if (_AppHdrLog != null) _generalDAL.SaveRequestApproveHear_Log(_AppHdrLog);

                _line = 0;
                if (_AppDetLog != null)
                {
                    foreach (RequestApprovalDetailLog _itm in _AppDetLog)
                    {
                        _line++;
                        _itm.Grad_ref = _UpdateApproval.Grah_ref;
                        _itm.Grad_line = _line;
                        _generalDAL.Save_RequestApprove_Det_Log(_itm);
                    }
                }

                if (_AppSerLog != null)
                {
                    foreach (RequestApprovalSerialsLog _ser in _AppSerLog)
                    {
                        _ser.Gras_ref = _UpdateApproval.Grah_ref;
                        _generalDAL.Save_RequestApprove_Serial_Log(_ser);

                        int eff = _inventoryDAL.Update_Warranty_sts(_ser.Gras_anal2, _ser.Gras_anal6, _ser.Gras_anal3, "N", _ser.Gras_anal3);


                    }
                }


                _generalDAL.TransactionCommit();
                _inventoryDAL.TransactionCommit();
                _effects = 1;
            }
            catch (Exception err)
            {
                _effects = -1;
                _generalDAL.TransactionRollback();
                _inventoryDAL.TransactionRollback();

            }
            return _effects;
        }


        public DataTable GetSCMCreditNote(string _inv, string _cus)
        {
            SCMCommonDAL _ScmCommonDAL = new SCMCommonDAL();
            return _ScmCommonDAL.GetSCMCreditNote(_inv, _cus);

        }

        public DataTable GetGvCategory(string _company, string _category)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetGvCategory(_company, _category);
        }

        public int SaveGvCategoryCombination(MasterGiftVoucherCategory _hdr, List<MasterGiftVoucherCategoryDetail> _detail, out string _error)
        {
            int _effect = -1;
            string _err = string.Empty;
            _generalDAL = new GeneralDAL();
            _inventoryDAL = new InventoryDAL();
            try
            {
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                _inventoryDAL.ConnectionOpen();
                _inventoryDAL.BeginTransaction();
                if (_hdr.Gvct_seq == -1) _hdr.Gvct_seq = _inventoryDAL.GetSerialID();
                _detail.ForEach(x => x.Gvctd_seq = _hdr.Gvct_seq);
                _generalDAL.SaveGvCategoryHeader(_hdr);
                _generalDAL.UpdateGvCategoryDetail(_hdr.Gvct_seq);
                foreach (MasterGiftVoucherCategoryDetail _one in _detail) _generalDAL.SaveGvCategoryDetail(_one);
                _generalDAL.TransactionCommit();
                _inventoryDAL.TransactionCommit();
                _effect = 1;
            }
            catch (Exception ex)
            {
                _effect = -1;
                _err = ex.Message;
                _inventoryDAL.TransactionRollback();
                _generalDAL.TransactionRollback();
            }
            _error = _err;
            return _effect;
        }

        public decimal GetRentalValue(string _account)
        {
            decimal _value = 0;
            _generalDAL = new GeneralDAL();
            DataTable _tbl = _generalDAL.GetRentalValue(_account);
            if (_tbl != null && _tbl.Rows.Count > 0)
                _value = Convert.ToDecimal(_tbl.Rows[0].Field<double>("hts_rnt_val"));
            return _value;
        }

        public CashPromotionDiscountHeader GetPromotionalDiscountBySeq(int _seq)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetPromotionalDiscountBySeq(_seq);
        }

        public DataTable Get_All_PC(string _com)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.Get_All_PC(_com);
        }
        public bool CheckCompany(string _company)
        {
            _generalDAL = new GeneralDAL();
            DataTable _tbl = _generalDAL.CheckCompany(_company);
            if (_tbl != null && _tbl.Rows.Count > 0) return true; else return false;
        }

        public bool CheckChannel(string _company, string _channel)
        {
            _generalDAL = new GeneralDAL();
            DataTable _tbl = _generalDAL.CheckChannel(_company, _channel);
            if (_tbl != null && _tbl.Rows.Count > 0) return true; else return false;
        }

        public bool CheckSubChannel(string _company, string _subchannel)
        {
            _generalDAL = new GeneralDAL();
            DataTable _tbl = _generalDAL.CheckSubChannel(_company, _subchannel);
            if (_tbl != null && _tbl.Rows.Count > 0) return true; else return false;
        }

        public bool CheckArea(string _company, string _area)
        {
            _generalDAL = new GeneralDAL();
            DataTable _tbl = _generalDAL.CheckArea(_company, _area);
            if (_tbl != null && _tbl.Rows.Count > 0) return true; else return false;
        }

        public bool CheckRegion(string _company, string _region)
        {
            _generalDAL = new GeneralDAL();
            DataTable _tbl = _generalDAL.CheckRegion(_company, _region);
            if (_tbl != null && _tbl.Rows.Count > 0) return true; else return false;
        }

        public bool CheckZone(string _company, string _zone)
        {
            _generalDAL = new GeneralDAL();
            DataTable _tbl = _generalDAL.CheckZone(_company, _zone);
            if (_tbl != null && _tbl.Rows.Count > 0) return true; else return false;
        }

        public bool CheckProfitCenter(string _company, string _profitcenter)
        {
            _generalDAL = new GeneralDAL();
            DataTable _tbl = _generalDAL.CheckProfitCenter(_company, _profitcenter);
            if (_tbl != null && _tbl.Rows.Count > 0) return true; else return false;
        }

        public DataTable GetDeductionDet(Int32 _cd)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetDeductionDet(_cd);
        }

        public DataTable SearchrequestAppAddDetByRef(string _ref)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.SearchrequestAppAddDetByRef(_ref);
        }

        public DataTable GetAgentCategory()
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetAgentCategory();
        }

        public DataTable SearchServiceAgent(string _company, string _code)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.SearchServiceAgent(_company, _code);
        }

        public DataTable CheckGroupSaleInvoiceStatus(string _com, string _pc, string _code)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.CheckGroupSaleInvoiceStatus(_com, _pc, _code);

        }

        public void CheckReportName(string _com, string _chnl, string _doctp, out string _repname, out Int16 _ShowComName, out string _PaperSize)
        {
            _generalDAL = new GeneralDAL();
            DataTable _dtResults = _generalDAL.CheckReportName(_com, _chnl, _doctp);

            if (_dtResults == null || _dtResults.Rows.Count <= 0)
            { _repname = string.Empty; _ShowComName = 0; _PaperSize = ""; return; }
            _repname = _dtResults.Rows[0].Field<string>("rep_name");
            _ShowComName = _dtResults.Rows[0].Field<Int16>("rep_shw_com");
            _PaperSize = _dtResults.Rows[0].Field<string>("rep_psize");
        }

        public bool CheckInsCircular(string _company, string _circular)
        {
            _generalDAL = new GeneralDAL();
            DataTable _tbl = _generalDAL.CheckInsCircular(_company, _circular);
            if (_tbl != null && _tbl.Rows.Count > 0) return true; else return false;
        }

        public Int32 SaveVehicalInsuranceDefinitionNew(List<MasterProfitCenter> pcs, List<InsuItem> items, DateTime from, DateTime to, string satlesType, decimal regVal, string cre, string insCom, string insPol, int term, bool isReq, string _tp, string _book, string _level, string _serial, string _promotion, string _circuler, decimal _isRate, decimal _fromValue, decimal _toValue, string _type, string _circular)
        {
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            InventoryDAL inv = new InventoryDAL();
            inv.ConnectionOpen();
            Int32 result = _generalDAL.SaveVehicalInsuranceDefinitionNew(pcs, items, from, to, satlesType, regVal, cre, insCom, insPol, term, isReq, _tp, _book, _level, _serial, _promotion, _circuler, _isRate, _fromValue, _toValue, _type, _circular);
            inv.ConnectionClose();
            _generalDAL.ConnectionClose();
            return result;
        }

        #region IGeneral Members


        public DataTable GetItemLPStatus()
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetItemLPStatus();
        }

        #endregion

        #region PromotionVoucherType

        public DataTable GetProVoutype(string _company, string _Code)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetProVoutype(_company, _Code);
        }

        public int UpdateProVouTypes(string p_com, string vou_cd, string vou_desc, Int32 vou_act, string p_mod_by, DateTime p_mod_when, Int32 _QtyWise, Int32 _isSMS, string _purSMS, string _redSMS, decimal _minVal, String _cond, List<PromotionVoucherPara> _lstVouPara, Int32 _opt, Int32 _separetPrint)
        {
            int result = 0;
            try
            {

                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();// tharanga 2017/06/09 add _separetPrint
                result = _generalDAL.UpdateProVouTypes(p_com, vou_cd, vou_desc, vou_act, p_mod_by, p_mod_when, _QtyWise, _isSMS, _purSMS, _redSMS, _minVal, _cond, _opt, _separetPrint);

                int _X = _generalDAL.deletePromoVouPara(vou_cd);

                if (_lstVouPara != null)
                {
                    foreach (PromotionVoucherPara _vou in _lstVouPara)
                    {
                        int _effect = _generalDAL.UpdateVoucherPromo(_vou);
                    }
                }

                _generalDAL.TransactionCommit();
                result = 1;
            }
            catch (Exception err)
            {
                result = -1;
                // _docNo = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();
            }
            return result;

        }

        public int SavePromoVouType(string p_com, string vou_cd, string vou_desc, Int32 vou_act, string p_Cre_by, Int32 vou_qty_wise, Int32 _SMS, string _purSMS, string _redSMS, decimal _minVal, String _cond, List<PromotionVoucherPara> _lstVouPara, Int32 _opt, Int32 _sp_print, out string Error, Int32 _dule_auth=0)
        {   // tharanga 2017/06/09 add _sp_print
            int result = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();

                result = _generalDAL.SavePromoVouType(p_com, vou_cd, vou_desc, vou_act, p_Cre_by, vou_qty_wise, _SMS, _purSMS, _redSMS, _minVal, _cond, _opt, _sp_print, _dule_auth);
                if (_lstVouPara != null)
                {
                    foreach (PromotionVoucherPara _vou in _lstVouPara)
                    {
                        int _effect = _generalDAL.UpdateVoucherPromo(_vou);
                    }
                }
                
                _generalDAL.TransactionCommit();
                Error = "Sucessfully Saved";
                result = 1;
               
            }
            catch (Exception err)
            {
                result = -1;
                Error = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();
            }
            return result;
        }

        public Int32 SavePromoVouDefinition(string p_Cre_by)
        {
            Int32 result = 0;
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            result = _generalDAL.SavePromoVouDefinition(p_Cre_by);
            _generalDAL.ConnectionClose();
            return result;
        }

        #endregion

        public DataTable GetCompanyItemsByCompany(string _company)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetCompanyItemsByCompany(_company);
        }

        public bool GetNotification(string _company, string Location, string ProfitCenter, string User, out List<Notification> _ControlActivities, out List<Notification> _Reminders, out List<Thoughts> _Thoughts)
        {
            bool status = false;
            _generalDAL = new GeneralDAL();

            // Nadeeka 28-12-2015
            _securityDAL = new SecurityDAL();
            if (_securityDAL.Is_Report_DR("Notification") == true) _generalDAL.ConnectionOpen_DR();


            DataTable dtNotification = new DataTable();
            dtNotification.Clear();
            dtNotification.Merge(_generalDAL.GetNotification(_company, Location, ProfitCenter, User));

            _ControlActivities = new List<Notification>();
            _Reminders = new List<Notification>();

            DateTime now = DateTime.Now;
            DateTime FromDateTemp = new DateTime(now.Year, now.Month, 1);



            if (dtNotification.Rows.Count > 0)
            {
                status = true;

                for (int i = 0; i < dtNotification.Rows.Count; i++)
                {

                    Notification ob = new Notification();
                    ob.Value = "0";
                    ob.Discription = dtNotification.Rows[i]["RDID_DISPLAY"].ToString();

                    #region GIT

                    if (dtNotification.Rows[i]["RDID_DESC"].ToString().ToUpper() == "GIT".ToUpper())
                    {
                        DataTable dt = new DataTable();
                        dt.Merge(_generalDAL.Notification_Get_GIT(_company, Location));

                        if (dtNotification.Rows[i]["rdid_tp"].ToString() == "CA")
                        {
                            string value = dt.Compute("SUM(DOC_NOS)", "").ToString();
                            if (value == "0")
                            {
                                continue;
                            }
                            ob.Value = value;
                            ob.NotificationType = (int)NotificationTypes.GIT;
                        }
                        else
                        {
                            string value = dt.Rows.Count.ToString();
                            ob.Value = value;
                            if (value == "0")
                            {
                                continue;
                            }

                            int AccseptaceCount = 0;
                            AccseptaceCount = dt.Select("ITH_DOC_TP ='AOD' AND ITH_SUB_TP ='SERVICE'").Length;
                            ob.Discription = "You have " + value.ToString() + " # of " + dtNotification.Rows[i]["RDID_DISPLAY"].ToString();
                            if (AccseptaceCount > 0)
                            {
                                ob.Discription = "You have " + value.ToString() + " # of " + dtNotification.Rows[i]["RDID_DISPLAY"].ToString()
                                                  + ", " + AccseptaceCount.ToString() + " # of AOD(s) pending for RCC.";
                            }
                            ob.NotificationType = (int)NotificationTypes.GIT;
                        }
                    }

                    #endregion

                    #region Account Reminders

                    if (dtNotification.Rows[i]["RDID_DESC"].ToString().ToUpper() == "Account Reminders".ToUpper())
                    {
                        DataTable dt = new DataTable();
                        dt.Merge(_generalDAL.Notification_Get_AccountReminders(_company, ProfitCenter, DateTime.Now.Date.AddDays(1)));
                        string value = dt.Compute("SUM(AccRemdCount)", "").ToString();
                        ob.Value = value;
                        if (value == "0")
                        {
                            continue;
                        }
                        ob.Discription = "You have " + ob.Value.ToString() + " " + ob.Discription;
                        ob.NotificationType = (int)NotificationTypes.AccountReminders;
                    }

                    #endregion

                    //kapila 24/2/2017
                    #region last log on date

                    if (dtNotification.Rows[i]["RDID_DESC"].ToString().ToUpper() == "Last Log On Date".ToUpper())
                    {
                        DataTable dt = new DataTable();
                        dt.Merge(_securityDAL.GetUserLastLogTrans(_company, User, 1));
                        string logon = dt.Rows[0]["log on"].ToString();
                        string IP = dt.Rows[0]["login ip"].ToString();
                        string Domain = dt.Rows[0]["Login Domain"].ToString();

                        if (string.IsNullOrEmpty(logon))
                        {
                            continue;
                        }

                        ob.Discription = ob.Discription + " is " + logon + " with IP : " + IP + " and Domain : " + Domain;
                        ob.NotificationType = (int)NotificationTypes.LastLogOnDate;
                    }

                    #endregion

                    #region Forward Sales

                    if (dtNotification.Rows[i]["RDID_DESC"].ToString().ToUpper() == "FD Sales".ToUpper())
                    {
                        ob.Value = string.Empty;
                        ob.NotificationType = (int)NotificationTypes.FDSales;
                    }

                    #endregion

                    #region Sales Figures

                    if (dtNotification.Rows[i]["RDID_DESC"].ToString().ToUpper() == "Sales Figure".ToUpper())
                    {


                        ob.Value = string.Empty;
                        ob.NotificationType = (int)NotificationTypes.SalesFigure;

                        DataTable dt = new DataTable();
                        dt.Merge(_generalDAL.Notification_Get_Sales_Figure(_company, ProfitCenter, DateTime.Today, DateTime.Today));
                        if (dt.Rows.Count > 0)
                        {
                            _ControlActivities.Add(ob);

                            DateTime nowasd = DateTime.Now;
                            DateTime FromDateTempasd = new DateTime(now.Year, now.Month, 1);
                            Notification obasd = new Notification();
                            obasd.Discription = "Duration";
                            obasd.Value = "From :- " + FromDateTempasd.ToString("dd/MMM/yyyy");
                            obasd.Value2 = "To :- " + DateTime.Today.ToString("dd/MMM/yyyy");
                            obasd.NotificationType = (int)NotificationTypes.SalesFigureItem;
                            _ControlActivities.Add(obasd);
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                Notification obSalesFigure = new Notification();
                                obSalesFigure.Discription = dt.Rows[j]["srtp_desc"].ToString();
                                // obSalesFigure.Value = "Total Sales = " + Convert.ToDecimal(dt.Rows[j]["total_sales"].ToString()).ToString("N") + "   Net Sales = " + Convert.ToDecimal(dt.Rows[j]["net_sale"].ToString()).ToString("N");
                                obSalesFigure.Value = "Total Sales = " + Convert.ToDecimal(dt.Rows[j]["total_sales"].ToString()).ToString("N");// +"   Net Sales = " + Convert.ToDecimal(dt.Rows[j]["net_sale"].ToString()).ToString("N");
                                obSalesFigure.Value2 = "Net Sales = " + Convert.ToDecimal(dt.Rows[j]["net_sale"].ToString()).ToString("N");
                                obSalesFigure.NotificationType = (int)NotificationTypes.SalesFigureItem;
                                _ControlActivities.Add(obSalesFigure);
                            }
                            //ob.Value = "Total sales =" + Convert.ToDecimal(dt.Compute("SUM(total_sales)", "").ToString()).ToString("N") + "     Total Net sales = " + Convert.ToDecimal(dt.Compute("SUM(net_sale)", "").ToString()).ToString("N");
                            //richTextBox1.Text += "\n" + textBox1.Text.PadRight(15, ' ') + textBox2.Text.PadRight(18, ' ') + textBox3.Text.PadLeft(17, ' ');
                            // ob.Value = "Total sales =" + Convert.ToDecimal(dt.Compute("SUM(total_sales)", "").ToString()).ToString("N") + "Total Net sales = " + Convert.ToDecimal(dt.Compute("SUM(net_sale)", "").ToString());
                            ob.Value = "Total sales =" + Convert.ToDecimal(dt.Compute("SUM(total_sales)", "").ToString()).ToString("N");// +"Total Net sales = " + Convert.ToDecimal(dt.Compute("SUM(net_sale)", "").ToString());
                            ob.Value2 = "Total Net sales = " + Convert.ToDecimal(dt.Compute("SUM(net_sale)", "").ToString()).ToString("N");
                        }
                        continue;
                    }

                    #endregion

                    #region LastDayendProcessedDate

                    if (dtNotification.Rows[i]["RDID_DESC"].ToString().ToUpper() == "Last dayend processed date".ToUpper())
                    {
                        DataTable dt = new DataTable();
                        dt.Merge(_generalDAL.Notification_Get_LastDayendProcessedDate(_company, ProfitCenter));
                        if (dt.Rows.Count > 0)
                        {
                            ob.Value = Convert.ToDateTime(dt.Rows[0]["upd_dt"].ToString()).ToString("dd/MMM/yyyy") + "  when : " + Convert.ToDateTime(dt.Rows[0]["upd_cre_dt"].ToString()).ToString("dd/MMM/yyyy  HH:mm:ss");
                            ob.Discription = ob.Discription + " : " + ob.Value;
                            ob.NotificationType = (int)NotificationTypes.LastDayendprocessedDate;

                        }
                        else
                        {
                            continue;
                        }
                    }

                    #endregion

                    #region Sales Status

                    if (dtNotification.Rows[i]["RDID_DESC"].ToString().ToUpper() == "Account status".ToUpper())
                    {
                        List<AccountRestriction> oResults = new List<AccountRestriction>();
                        oResults = _generalDAL.NOtification_Get_AccountSatatus(_company, ProfitCenter, DateTime.Today, DateTime.Today);

                        if (oResults.Count > 0)
                        {
                            ob.Value = string.Empty;
                            _ControlActivities.Add(ob);

                            Notification NftPeriod = new Notification();
                            NftPeriod.Discription = "Period";
                            NftPeriod.Value = oResults[0].Hrs_from_dt.ToString("dd/MMM/yyyy") + " To " + oResults[0].Hrs_to_dt.ToString("dd/MMM/yyyy");

                            NftPeriod.NotificationType = (int)NotificationTypes.AccountStatusItem;
                            _ControlActivities.Add(NftPeriod);

                            DataTable dtAccValues = new DataTable();
                            dtAccValues.Merge(_generalDAL.Notification_Get_AccountDetails(_company, ProfitCenter, DateTime.Today, DateTime.Today));

                            Notification obSales1 = new Notification();
                            obSales1.Discription = "# of Accounts";
                            obSales1.Value = dtAccValues.Rows[0]["AccCount"].ToString() + " Out of " + oResults[0].Hrs_no_ac.ToString();
                            obSales1.NotificationType = (int)NotificationTypes.AccountStatusItem;
                            _ControlActivities.Add(obSales1);

                            Notification obSales2 = new Notification();
                            obSales2.Discription = "Total value";
                            obSales2.Value = Convert.ToDecimal(dtAccValues.Rows[0]["cash_price"].ToString()).ToString("N") + " Out of " + Convert.ToDecimal(oResults[0].Hrs_tot_val.ToString()).ToString("N");
                            obSales2.NotificationType = (int)NotificationTypes.AccountStatusItem;
                            _ControlActivities.Add(obSales2);
                        }
                        continue;
                    }

                    #endregion

                    #region RCC
                    if (dtNotification.Rows[i]["RDID_DESC"].ToString().ToUpper() == "Allowed RCC".ToUpper())
                    {
                        MasterLocation _sa = _generalDAL.GetLocationByLocCode(_company, Location);
                        decimal _allowRCC = _sa.Ml_fwsale_qty;
                        if (_allowRCC == 0)
                        {
                            continue;
                        }
                        ob.Value = _allowRCC.ToString();
                        ob.NotificationType = (int)NotificationTypes.AllowedRcc;
                    }

                    #endregion

                    #region Other shop collections
                    if (dtNotification.Rows[i]["RDID_DESC"].ToString().ToUpper() == "Other shop collections".ToUpper())
                    {
                        Int32 Count = _generalDAL.GetOtherShopCollectionsCount(_company, Location, DateTime.Today);
                        ob.Value = Count.ToString();

                        ob.Discription = "No of " + ob.Value + " other shop collection(s) received";
                        ob.NotificationType = (int)NotificationTypes.OtherShopCollection;
                    }

                    #endregion

                    if (dtNotification.Rows[i]["rdid_tp"].ToString() == "CA")
                    {
                        _ControlActivities.Add(ob);

                    }
                    else if (dtNotification.Rows[i]["rdid_tp"].ToString() == "RM")
                    {
                        _Reminders.Add(ob);
                    }
                }
            }

            #region Thoughts
            {
                _Thoughts = new List<Thoughts>();
                List<Thoughts> _ThoughtsAll = new List<Thoughts>();

                _ThoughtsAll = _generalDAL.Notification_GetThoughts();

                if (_ThoughtsAll.Count > 0)
                {
                    if (_ThoughtsAll.FindAll(x => x.RDT_IS_PERIOD == 1 && x.RDT_FROM <= DateTime.Today && x.RDT_TO > DateTime.Today).Count > 0)
                    {
                        Thoughts Ob = new Thoughts();
                        Ob = _ThoughtsAll.Find(x => x.RDT_IS_PERIOD == 1 && x.RDT_FROM <= DateTime.Today && x.RDT_TO > DateTime.Today);
                        _Thoughts.Add(Ob);
                    }
                    else
                    {

                        if (DateTime.Today == FirstDayOfWeek(DateTime.Today))  //chech whether first Date of the week
                        {
                            Thoughts Ob = new Thoughts();
                            Ob = _ThoughtsAll.Find(x => x.RDT_IS_READ == -1);

                            //if (Ob.RDT_UPDATE_DT.Date == DateTime.Today)
                            //{
                            //    _Thoughts.Add(Ob);
                            //    return status;
                            //}
                            //Ob.RDT_IS_READ = 1;

                            //_generalDAL = new GeneralDAL();
                            //_generalDAL.ConnectionOpen();
                            //_generalDAL.Notification_UpdateThoughts(Ob);
                            //_generalDAL.ConnectionClose();

                            //List<Thoughts> UnreadThought = new List<Thoughts>();
                            //UnreadThought = _ThoughtsAll.FindAll(x => x.RDT_IS_READ == 0 && x.RDT_IS_PERIOD != 1);

                            //Random r = new Random();
                            //int rInt = r.Next(0, UnreadThought.Count);

                            //Thoughts selectedForWeek = UnreadThought[rInt];
                            //selectedForWeek.RDT_IS_READ = -1;

                            //_generalDAL = new GeneralDAL();
                            //_generalDAL.ConnectionOpen();
                            //_generalDAL.Notification_UpdateThoughts(selectedForWeek);
                            //_generalDAL.ConnectionClose();

                            //_Thoughts.Clear();
                            //_Thoughts.Add(selectedForWeek);

                        }
                        else
                        {
                            Thoughts Ob = new Thoughts();
                            Ob = _ThoughtsAll.Find(x => x.RDT_IS_READ == -1);
                            _Thoughts.Add(Ob);
                        }
                    }
                }
            }

            #endregion

            return status;
        }

        public DataTable Notification_Get_LastDayendDates(string _company, string ProfitCenter)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.Notification_Get_LastDayendDates(_company, ProfitCenter);
        }

        public DateTime FirstDayOfWeek(DateTime date)
        {
            var candidateDate = date;
            while (candidateDate.DayOfWeek != DayOfWeek.Monday)
            {
                candidateDate = candidateDate.AddDays(-1);
            }
            return candidateDate;
        }

        public DateTime LastDayOfWeek(DateTime date)
        {
            var candidateDate = date;
            while (candidateDate.DayOfWeek != DayOfWeek.Saturday)
            {
                candidateDate = candidateDate.AddDays(+1);
            }
            return candidateDate;
        }

        public List<HPReminder> Notification_Get_AccountRemindersDetails(string _company, string ProfitCenter)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.Notification_Get_AccountRemindersDetails(_company, ProfitCenter, DateTime.Now.Date.AddDays(1));
        }

        public DataTable GetProfitCenterAllocatedPromotors(string _company, string _profitcenter)
        {

            DataTable DT1 = new DataTable();
            _generalDAL = new GeneralDAL();
            SalesDAL _salesDAL = new SalesDAL();

            List<MasterSalesPriorityHierarchy> _hierarchy = _salesDAL.GetSalesPriorityHierarchy(_company, _profitcenter, "PC_PRIT_HIERARCHY", "PC");



            DT1 = _generalDAL.GetProfitCenterAllocatedPromotors("PC", _profitcenter);
            if (DT1.Rows.Count > 0)
            {
                return DT1;
            }


            DT1 = _generalDAL.GetProfitCenterAllocatedPromotors("COM", _company);

            if (DT1.Rows.Count > 0)
            {
                return DT1;
            }


            if (_hierarchy != null && _hierarchy.Count > 0) _hierarchy = _hierarchy.Where(x => x.Mpi_cd == "SCHNL").ToList();
            if (_hierarchy != null && _hierarchy.Count > 0)
            {
                foreach (MasterSalesPriorityHierarchy _one in _hierarchy)
                {
                    DT1 = _generalDAL.GetProfitCenterAllocatedPromotors("SCHNL", _one.Mpi_val);
                }
                if (DT1.Rows.Count > 0)
                {
                    return DT1;
                }
            }

            if (_hierarchy != null && _hierarchy.Count > 0) _hierarchy = _hierarchy.Where(x => x.Mpi_cd == "CHNL").ToList();
            if (_hierarchy != null && _hierarchy.Count > 0)
            {
                foreach (MasterSalesPriorityHierarchy _one in _hierarchy)
                {
                    DT1 = _generalDAL.GetProfitCenterAllocatedPromotors("CHNL", _one.Mpi_val);
                }
                if (DT1.Rows.Count > 0)
                {
                    return DT1;
                }
            }


            return DT1;

        }

        public DataTable GetFunctionApplevel_UserAppLevel(string user, string sart_desc)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetFunctionApplevel_UserAppLevel(user, sart_desc);
        }
        public string Get_ProfitCenter_desc(string _company, string _profitcenter)
        {
            _generalDAL = new GeneralDAL();
            string _desc = "";
            DataTable _tbl = _generalDAL.CheckProfitCenter(_company, _profitcenter);
            if (_tbl != null && _tbl.Rows.Count > 0)
            {
                _desc = Convert.ToString(_tbl.Rows[0]["mpc_desc"]);
                return _desc;
            }
            return _desc;

        }


        public List<ItemConditionCategori> Get_ItemConditionCategories(string Com)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.Get_ItemConditionCategories(Com);
        }

        public List<ItemConditionSetup> Get_ItemConditionBySerial(string Com, string serialNo, string Loc, string itemCode)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.Get_ItemConditionBySerial(Com, serialNo, Loc, itemCode);
        }

        public int Save_itemConditions(ItemConditionSetup oItemConditionSetup)
        {
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            int result = _generalDAL.Save_itemConditions(oItemConditionSetup);
            _generalDAL.ConnectionClose();
            return result;
        }

        public int Update_itemConditions(ItemConditionSetup oItemConditionSetup)
        {
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            int result = _generalDAL.Update_itemConditions(oItemConditionSetup);
            _generalDAL.ConnectionClose();
            return result;
        }

        //shanuka 
        public bool check_avl_chn(string com, string code)
        {
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            DataTable _tbl = _generalDAL.check_avl_chn(com, code);
            _generalDAL.ConnectionClose();
            if (_tbl != null && _tbl.Rows.Count > 0) return true; else return false;
        }

        public DataTable get_stus_chnl(string com, string code)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.check_avl_chn(com, code);

        }

        //shanuka 
        public Int32 Insert_to_chanelDets(Deposit_Bank_Pc_wise _objchanel, out string _err)
        {
            Int32 effect = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                string _documentNo = "";
                _err = _documentNo;
                effect = _generalDAL.Insert_to_chanelDets(_objchanel);


                _generalDAL.TransactionCommit();
                _err = "Sucessfully Saved";
                effect = 1;
            }
            catch (Exception err)
            {
                effect = -1;
                _err = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();

            }
            return effect;

        }


        //shanuka 
        public Int32 Update_to_chanelDets(Deposit_Bank_Pc_wise _objcha, out string _err)
        {
            Int32 effect = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                string _documentNo = "";
                _err = _documentNo;
                effect = _generalDAL.Update_to_chanelDets(_objcha);


                _generalDAL.TransactionCommit();
                _err = "Sucessfully Saved";
                effect = 1;
            }
            catch (Exception err)
            {
                effect = -1;
                _err = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();

            }
            return effect;

        }

        public DataTable sp_get_pc_prit_hierarchy(string _com, string _type)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.sp_get_pc_prit_hierarchy(_com, _type);
        }
        //shanuka 
        public Int32 Insert_to_subchanelDets(Deposit_Bank_Pc_wise objChanel, out string _err)
        {
            Int32 effect = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                string _documentNo = "";
                _err = _documentNo;
                effect = _generalDAL.Insert_to_subchanelDets(objChanel);


                _generalDAL.TransactionCommit();
                _err = "Sucessfully Saved";
                effect = 1;
            }
            catch (Exception err)
            {
                effect = -1;
                _err = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();

            }
            return effect;

        }

        //shanuka 
        public Int32 Update_to_subchanelDets(Deposit_Bank_Pc_wise obj_chanel, out string _err)
        {
            Int32 effect = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                string _documentNo = "";
                _err = _documentNo;
                effect = _generalDAL.Update_to_subchanelDets(obj_chanel);


                _generalDAL.TransactionCommit();
                _err = "Sucessfully Saved";
                effect = 1;
            }
            catch (Exception err)
            {
                effect = -1;
                _err = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();

            }
            return effect;

        }

        //shanuka 30/09/2014
        public DataTable getAllChannel_details(string com)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getAllChannel_details(com);
        }

        public DataTable getAllSubChannel_details(string com)
        {//shanuka 1/10/2014
            _generalDAL = new GeneralDAL();
            return _generalDAL.getAllSubChannel_details(com);
        }
        public DataTable getAllarea_details(string com)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getAllarea_details(com);
        }
        public DataTable getAllregion_details(string com)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getAllregion_details(com);
        }
        public DataTable getAllzone_details(string com)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getAllzone_details(com);
        }



        public DataTable Get_ItemCondition_Inquiary(string UserName, string comnapy, string MainCategori, string SubCategori, string Brand, string ItemCode, string Model, string Condition, List<string> LocationList)
        {
            DataTable DtDetails = new DataTable();

            try
            {
                if (LocationList != null)
                {
                    _generalDAL = new GeneralDAL();
                    _generalDAL.ConnectionOpen();
                    _generalDAL.BeginTransaction();

                    int effectDetele1 = _generalDAL.Delete_TEMP_PC_LOC_SCMREPP(UserName, comnapy, "", "");

                    for (int i = 0; i < LocationList.Count; i++)
                    {
                        int effectSave = _generalDAL.Save_TEMP_PC_LOC_SCMREPP(UserName, comnapy, "", LocationList[i]);
                    }
                }
                _generalDAL.TransactionCommit();


                DtDetails = _generalDAL.Get_ItemCondition_Inquiary(comnapy, MainCategori, SubCategori, Brand, ItemCode, Model, Condition, UserName);

                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                int effectDetele2 = _generalDAL.Delete_TEMP_PC_LOC_SCMREPP(UserName, comnapy, "", "");
                _generalDAL.TransactionCommit();

                return DtDetails;
            }
            catch (Exception ex)
            {
                _saleDAL.TransactionRollback();
                return DtDetails;
            }

        }


        //Tharaka 2014-10-07
        public Service_Chanal_parameter GetChannelParamers(string com, string Location)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetChannelParamers(com, Location);
        }


        //shanuka 2014-10-09
        public Int32 Insert_to_serviceChnl_para(ServiceChnlDetails objservice_chnl, out string _err)
        {
            Int32 effect = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                string _documentNo = "";
                _err = _documentNo;
                effect = _generalDAL.Insert_to_serviceChnl_para(objservice_chnl);


                _generalDAL.TransactionCommit();
                _err = "Sucessfully Saved";
                effect = 1;
            }
            catch (Exception err)
            {
                effect = -1;
                _err = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();

            }
            return effect;

        }


        //shanuka 09-10-2014
        public DataTable getAllserviceChannels(string com)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getAllserviceChannels(com);

        }
        public Int32 Update_to_serviceChnl_para(ServiceChnlDetails objservice_chnl, out string _err)
        {
            Int32 effect = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                string _documentNo = "";
                _err = _documentNo;
                effect = _generalDAL.Update_to_serviceChnl_para(objservice_chnl);


                _generalDAL.TransactionCommit();
                _err = "Sucessfully Saved";
                effect = 1;
            }
            catch (Exception err)
            {
                effect = -1;
                _err = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();

            }
            return effect;
        }


        public Int32 SaveItemBrand(List<MasterItemBrand> _lstbrand, List<MasterItemBrand> _lstbranddel, out string _err)
        {
            Int32 effect = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                string _documentNo = "";
                _err = _documentNo;

                effect = _generalDAL.DeleteItemBrand(_lstbranddel);
                effect = _generalDAL.SaveItemBrand(_lstbrand);


                _generalDAL.TransactionCommit();
                _err = "Sucessfully Saved";
                effect = 1;
            }
            catch (Exception err)
            {
                effect = -1;
                _err = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();

            }
            return effect;
        }

        public Int32 SaveItemModel(List<MasterItemModel> _lstmodel, List<MasterItemModel> _lstmodeldel, List<mst_model_replace> _lstRplModel, List<mst_commodel> _lstComModel, List<BusinessEntityVal> _lstBusEntity, List<UnitConvert> _uomdata, out string _err)
        {
            Int32 effect = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                string _documentNo = "";
                _err = _documentNo;


                effect = _generalDAL.DeleteItemModel(_lstmodeldel);
                effect = _generalDAL.SaveItemModel(_lstmodel, _lstRplModel, _lstComModel, _lstBusEntity, _uomdata); //ERROR COMMENT BY CHAMAL 18-07-2016


                _generalDAL.TransactionCommit();
                _err = "Sucessfully Saved";
                effect = 1;
            }
            catch (Exception err)
            {
                effect = -1;
                _err = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();

            }
            return effect;
        }

        public Int32 SaveItemModelWeb(List<MasterItemModel> _lstmodel, List<MasterItemModel> _lstmodeldel, List<mst_model_replace> _lstRplModel, List<mst_commodel> _lstComModel, List<BusinessEntityVal> _lstBusEntity, List<UnitConvert> _uomdata, out string _err)
        {
            Int32 effect = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                string _documentNo = "";
                _err = _documentNo;


                effect = _generalDAL.DeleteItemModel(_lstmodeldel);
                effect = _generalDAL.SaveItemModelWeb(_lstmodel, _lstRplModel, _lstComModel, _lstBusEntity, _uomdata); //ERROR COMMENT BY CHAMAL 18-07-2016


                _generalDAL.TransactionCommit();
                _err = "Sucessfully Saved";
                effect = 1;
            }
            catch (Exception err)
            {
                effect = -1;
                _err = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();

            }
            return effect;
        }

        public Int32 SaveItemUOM(List<MasterUOM> _lstUOM, List<MasterUOM> _lstUOMdel, out string _err)
        {
            Int32 effect = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                string _documentNo = "";
                _err = _documentNo;

                effect = _generalDAL.DeleteItemUOM(_lstUOMdel);
                effect = _generalDAL.SaveItemUOM(_lstUOM);


                _generalDAL.TransactionCommit();
                _err = "Sucessfully Saved";
                effect = 1;
            }
            catch (Exception err)
            {
                effect = -1;
                _err = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();

            }
            return effect;
        }




        public Int32 SaveItemColor(List<MasterColor> _lstUOM, List<MasterColor> _lstUOMdel, out string _err)
        {
            Int32 effect = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                string _documentNo = "";
                _err = _documentNo;

                effect = _generalDAL.DeleteItemcolor(_lstUOMdel);
                effect = _generalDAL.SaveItemColor(_lstUOM);


                _generalDAL.TransactionCommit();
                _err = "Sucessfully Saved";
                effect = 1;
            }
            catch (Exception err)
            {
                effect = -1;
                _err = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();

            }
            return effect;
        }

        public Int32 SaveItemTaxStructure(mst_itm_tax_structure_hdr _tax, List<mst_itm_tax_structure_det> _taxDet, MasterAutoNumber _masterAuto, out string _err)
        {
            Int32 effect = 0;
            try
            { // Nadeeka
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();

                _inventoryDAL = new InventoryDAL();
                _inventoryDAL.ConnectionOpen();
                _inventoryDAL.BeginTransaction();
                int SeqNum = 0;
                string _documentNo = "";
                _err = _documentNo;
                string DocNum = string.Empty;
                if (!string.IsNullOrEmpty(_tax.Ish_stuc_code))
                {
                    effect = _generalDAL.DeleteItemTaxStructureDet(_tax.Ish_stuc_code, _tax.Ish_mod_by);
                    DocNum = _tax.Ish_stuc_code;
                    SeqNum = _tax.Ish_stuc_seq;
                    _err = "Successfully Updated ! Tax Structure Code : ";
                }
                else
                {
                    SeqNum = _generalDAL.GetTaxCodeSeq();

                    MasterAutoNumber _StuDoc = _inventoryDAL.GetAutoNumber(_masterAuto.Aut_moduleid, _masterAuto.Aut_direction, _masterAuto.Aut_start_char, _masterAuto.Aut_cate_tp, _masterAuto.Aut_cate_cd, _masterAuto.Aut_modify_dt, _masterAuto.Aut_year);
                    DocNum = _tax.Ish_com + "-" + _StuDoc.Aut_start_char + "-" + Convert.ToString(DateTime.Now.Date.Year).Remove(0, 2) + "-" + _StuDoc.Aut_number.ToString("00000", CultureInfo.InvariantCulture);

                    _inventoryDAL.UpdateAutoNumber(_masterAuto);
                    _err = "Successfully Saved ! Tax Structure Code : ";
                }

                _tax.Ish_stuc_seq = SeqNum;
                _tax.Ish_stuc_code = DocNum;
                effect = _generalDAL.SaveItemTaxStructureHdr(_tax);


                foreach (mst_itm_tax_structure_det item in _taxDet)
                {
                    item.Its_stuc_seq = SeqNum;
                    item.Its_stuc_code = DocNum;
                }
                effect = _generalDAL.SaveItemTaxStructureDet(_taxDet);

                _generalDAL.TransactionCommit();
                _inventoryDAL.TransactionCommit();

                _err = _err + DocNum;

                effect = 1;
            }
            catch (Exception err)
            {
                effect = -1;
                _err = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();
                _inventoryDAL.TransactionRollback();
            }
            return effect;
        }
        public Int32 SaveItemCate1(List<REF_ITM_CATE1> _cate1, List<REF_ITM_CATE1> _cate1del, out string _err)
        {
            Int32 effect = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                string _documentNo = "";
                _err = _documentNo;

                effect = _generalDAL.DeleteItemCate1(_cate1del);
                effect = _generalDAL.SaveItemCate1(_cate1);


                _generalDAL.TransactionCommit();
                _err = "Sucessfully Saved";
                effect = 1;
            }
            catch (Exception err)
            {
                effect = -1;
                _err = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();

            }
            return effect;
        }
        public Int32 SaveItemCate2(List<MasterItemSubCate> _cate2, List<MasterItemSubCate> _cate2del, out string _err)
        {
            Int32 effect = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                string _documentNo = "";
                _err = _documentNo;
                effect = _generalDAL.DeleteItemCate2(_cate2del);

                effect = _generalDAL.SaveItemCate2(_cate2);


                _generalDAL.TransactionCommit();
                _err = "Sucessfully Saved";
                effect = 1;
            }
            catch (Exception err)
            {
                effect = -1;
                _err = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();

            }
            return effect;
        }
        public Int32 SaveItemCate3(List<REF_ITM_CATE3> _cate3, List<REF_ITM_CATE3> _cate3del, out string _err)
        {
            Int32 effect = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                string _documentNo = "";
                _err = _documentNo;
                effect = _generalDAL.DeleteItemCate3(_cate3del);

                effect = _generalDAL.SaveItemCate3(_cate3);


                _generalDAL.TransactionCommit();
                _err = "Sucessfully Saved";
                effect = 1;
            }
            catch (Exception err)
            {
                effect = -1;
                _err = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();

            }
            return effect;
        }

        public Int32 SaveItemCate4(List<REF_ITM_CATE4> _cate4, List<REF_ITM_CATE4> _cate4del, out string _err)
        {
            Int32 effect = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                string _documentNo = "";
                _err = _documentNo;

                effect = _generalDAL.DeleteItemCate4(_cate4del);
                effect = _generalDAL.SaveItemCate4(_cate4);


                _generalDAL.TransactionCommit();
                _err = "Sucessfully Saved";
                effect = 1;
            }
            catch (Exception err)
            {
                effect = -1;
                _err = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();

            }
            return effect;
        }
        public Int32 SaveItemCate5(List<REF_ITM_CATE5> _cate5, List<REF_ITM_CATE5> _cate5del, out string _err)
        {
            Int32 effect = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                string _documentNo = "";
                _err = _documentNo;


                effect = _generalDAL.DeleteItemCate5(_cate5del);
                effect = _generalDAL.SaveItemCate5(_cate5);


                _generalDAL.TransactionCommit();
                _err = "Sucessfully Saved";
                effect = 1;
            }
            catch (Exception err)
            {
                effect = -1;
                _err = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();

            }
            return effect;
        }
        public REF_ITM_CATE3 GetItemCategory3(string _cd)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetItemCategory3(_cd);

        }
        public REF_ITM_CATE4 GetItemCategory4(string _cd)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetItemCategory4(_cd);

        }
        public REF_ITM_CATE5 GetItemCategory5(string _cd)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetItemCategory5(_cd);

        }

        public DataTable get_sub_chanels(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.get_sub_chanels(_initialSearchParams, _searchCatergory, _searchText);

        }
        public DataTable GetServiceLocation(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetServiceLocation(_initialSearchParams, _searchCatergory, _searchText);

        }
        //shanuka 09-10-2014
        public DataTable get_serviceCenters(string com, string chnl)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.get_serviceCenters(com, chnl);
        }
        // Nadeeka
        public DataTable get_Language()
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.get_Language();
        }

        // Nadeeka
        public DataTable get_Buss_ent_type()
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.get_Buss_ent_type();
        }
        string GetRealPhoneNumber(string _phone)
        {
            Regex digitsOnly = new Regex(@"[^\d]");
            return digitsOnly.Replace(_phone, "");
        }
        public Int32 GetCutomerValidationCode(string _mob)
        {
            Int32 _effect = 0;
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            _generalDAL.BeginTransaction();
            try
            {


                _effect = _generalDAL.GetCutomerValidationCode();

                String SmsBody = "Dear Sir/ Madam, ";
                SmsBody += "\n Verification Code is " + _effect + " .";
                string _realPhNo = string.Empty;
                _realPhNo = GetRealPhoneNumber(_mob);
                OutSMS _out = new OutSMS();
                _out.Createtime = DateTime.Now;
                _out.Msg = "Verification Code is" + _effect;
                _out.Msgstatus = 0;
                _out.Msgtype = "S";
                _out.Receivedtime = DateTime.Now;
                _out.Receiver = "CustomerPortal";
                _out.Senderphno = _mob;
                _out.Refdocno = "CustomerPortal";
                _out.Sender = "CustomerPortal";
                _out.Createtime = DateTime.Now;
                _generalDAL.SaveSMSOut(_out);


                _generalDAL.TransactionCommit();

                _generalDAL.ConnectionClose();
            }
            catch
            {
                _generalDAL.TransactionRollback();


                _effect = -1;
            }

            return _effect;
        }


        public Int32 UpdateCutomerMobile(string _mobile, string _type, out string _err)
        {
            Int32 _effect = 0;

            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            _generalDAL.BeginTransaction();
            try
            {


                _effect = _generalDAL.UpdateCutomerMobile(_mobile, _type);




                _generalDAL.TransactionCommit();

                _generalDAL.ConnectionClose();
                _err = "";
            }
            catch (Exception err)
            {
                _generalDAL.TransactionRollback();

                _effect = -1;
                _err = "ERROR : " + err.Message.ToString();


            }

            return _effect;
        }

        //shanuka 11-10-2014
        public Int32 Insert_to_serviceloc(List<ServiceChnlDetails> lstserviceloc, out string _err)
        {
            Int32 effect = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                string _documentNo = "";
                _err = _documentNo;
                effect = _generalDAL.Insert_to_serviceloc(lstserviceloc);


                _generalDAL.TransactionCommit();
                _err = "Sucessfully Saved";
                effect = 1;
            }
            catch (Exception err)
            {
                effect = -1;
                _err = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();

            }
            return effect;

        }
        //shanuka 11-10-2014
        public DataTable getAllservice_locDetails(string com, string loc, string sloc)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getAllservice_locDetails(com, loc, sloc);
        }
        //shanuka 11-10-2014
        public DataTable getAllservice_paraDets(string com)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getAllservice_paraDets(com);

        }
        //shanuka 11-10-2014
        public DataTable get_loc_services(string com, string srcenter, string type)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.get_loc_services(com, srcenter, type);
        }
        //shanuka 11-10-2014
        public DataTable get_service_center_dets(string com)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.get_service_center_dets(com);
        }
        //shanuka 11-10-2014
        public DataTable get_Status_for_services(string com, string srcenter, string type, string loc)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.get_Status_for_services(com, srcenter, type, loc);
        }
        //shanuka 11-10-2014
        public Int32 Update_to_InactiveServices(ServiceChnlDetails obj_services, out string _err)
        {
            Int32 effect = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                string _documentNo = "";
                _err = _documentNo;
                effect = _generalDAL.Update_to_InactiveServices(obj_services);


                _generalDAL.TransactionCommit();
                _err = "Sucessfully Saved";
                effect = 1;
            }
            catch (Exception err)
            {
                effect = -1;
                _err = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();

            }
            return effect;
        }



        //Tharaka 2014-10-14
        public DataTable GetTownByDesc(string _Desc)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetTownByDesc(_Desc);
        }
        //shanuka 15-10-2014
        public DataTable get_sub_chnl_stus(string com, string code)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.get_sub_chnl_stus(com, code);
        }
        //shanuka 18-10-2014
        public DataTable get_Default_val(string code)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.get_Default_val(code);
        }
        //shanuka 21-10-2014
        public DataTable get_availble_AllocateItems(string com, string tpe, string loc, string cate)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.get_availble_AllocateItems(com, tpe, loc, cate);
        }
        //Darshana 23-10-2014
        public List<HpAccount> GetHpAccountsByDtRange(string _com, string _pc, DateTime _frmDt, DateTime _toDt)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetHpAccountsByDtRange(_com, _pc, _frmDt, _toDt);
        }

        // Nadeeka
        public List<MasterItemBrand> GetItemBrand()
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetItemBrand();
        }

        // Nadeeka
        public List<MasterItemModel> GetItemModel(string _code = null)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetItemModel(_code);
        }

        // Nadeeka
        public List<MasterUOM> GetItemUOM()
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetItemUOM();
        }
        // Nadeeka
        public List<MasterColor> GetItemColor()
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetItemColor();
        }
        // Nadeeka
        public List<REF_ITM_CATE1> GetItemCate1()
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetItemCate1();
        }
        // Nadeeka
        public List<MasterItemSubCate> GetItemCate2(string _cate1)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetItemCate2(_cate1);
        }


        // Nadeeka
        public List<REF_ITM_CATE3> GetItemCate3(string _cate1, string _cate2)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetItemCate3(_cate1, _cate2);
        }

        // Nadeeka
        public List<REF_ITM_CATE4> GetItemCate4(string _cate1, string _cate2, string _cate3)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetItemCate4(_cate1, _cate2, _cate3);
        }
        // Nadeeka
        public List<REF_ITM_CATE5> GetItemCate5(string _cate1, string _cate2, string _cate3, string _cate4)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetItemCate5(_cate1, _cate2, _cate3, _cate4);
        }


        //shanuka 25-10-2014
        public DataTable get_All_Bnk_StmDetails(DateTime from, DateTime to, string acc)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.get_All_Bnk_StmDetails(from, to, acc);
        }

        // 
        /// <summary>
        /// damith 29-12-2014
        /// Gets the job estimate total.
        /// </summary>
        /// <param name="jobNo">The job no.</param>
        /// <returns>string estimate total</returns>
        public string GetJobEstimateTot(string jobNo)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetJobEstiamteTot(jobNo.Trim());
        }

        //Tharaka 2015-01-13
        public List<Notification_OthShpCollecitons> GetOtherShopCollections(String Com, String Loc, DateTime Date)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetOtherShopCollections(Com, Loc, Date);
        }

        //Tharaka 2015-02-19
        public Master_Employee GetMasterEmployee(String Com, String emp)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetMasterEmployee(Com, emp);
        }
        //Darshana 2015-03-04
        public DataTable GetProfitCenter(string _company, string _profitcenter)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.CheckProfitCenter(_company, _profitcenter);

        }

        //Tharaka 2015-05-16
        public List<SEC_SYSTEM_MENU_SUB> GET_REPORT_LIST_BY_MENU(String MenuName)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GET_REPORT_LIST_BY_MENU(MenuName);
        }

        //Tharaka 2015-05-21
        public DataTable GetSecurityUsers(String Com, String User, String Department, DateTime FromDAte, DateTime Todate)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetSecurityUsers(Com, User, Department, FromDAte, Todate);
        }

        public Int32 CheckSCMPeriodIsOpen(BackDates _backdate, out string _err)
        {
            Int32 effect = 0;
            try
            {

                string _loc = _backdate.Gad_loc;

                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();

                if (_backdate.Gad_rmk == "PC")
                {
                    MasterProfitCenter _pc = new MasterProfitCenter();
                    _pc = _generalDAL.GetPCByPCCode(_backdate.Gad_com, _backdate.Gad_loc);
                    _loc = _pc.Mpc_def_loc;
                }

                effect = 1;
                _err = string.Empty;
                if (_backdate.Gad_com != "AST") //kapila 18/7/2016
                {
                    effect = _generalDAL.CheckSCMPeriodIsOpen(_backdate.Gad_com, _loc, _backdate.Gad_act_from_dt.Year, _backdate.Gad_act_from_dt.Month);
                    if (effect != 1)
                        _err = "SCM period not open for location " + _loc;
                    else
                        _err = string.Empty;
                }

                _generalDAL.ConnectionClose();
                return effect;
            }
            catch (Exception err)
            {
                effect = -1;
                _err = "ERROR : " + err.Message.ToString();
                return effect;
            }
        }


        public Int32 SaveItemComTax(List<MasterItemTax> _lstItmComtax, out string _err)
        {
            Int32 effect = 0;
            try
            {
                _saleDAL = new SalesDAL();
                _saleDAL.ConnectionOpen();
                _saleDAL.BeginTransaction();
                if (_lstItmComtax != null)
                {
                    foreach (MasterItemTax _itax in _lstItmComtax)
                    {
                        effect = _saleDAL.Save_mst_itm_comtax(_itax);
                    }
                }

                _saleDAL.TransactionCommit();
                _err = "Sucessfully saved tax structure.";
                effect = 1;
            }
            catch (Exception err)
            {
                effect = -1;
                _err = "ERROR : " + err.Message.ToString();
                _saleDAL.TransactionRollback();
            }
            return effect;
        }

        //kapila
        public Int32 Save_Item_Master(List<MasterItem> _item, List<MasterCompanyItem> _lstcomItem, out string _err)
        {
            Int32 effect = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();

                foreach (MasterItem _itm in _item)
                {
                    effect = _generalDAL.Save_Item_Master(_itm);
                }

                effect = _generalDAL.SaveComItemNEW(_lstcomItem);

                _generalDAL.TransactionCommit();
                _err = "Sucessfully uploaded";
                effect = 1;
            }
            catch (Exception err)
            {
                effect = -1;
                _err = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();
            }
            return effect;
        }

        public Int32 SaveItemMaster(MasterItem _item, List<mst_itm_channlwara> _lstChnelWarra, List<mst_itm_pc_warr> _lstpcwara, List<MasterItemWarrantyPeriod> _lstitemWara, List<mst_itm_sevpd> _lstserPrd, List<MasterItemTaxClaim> _lstitemClaim, List<mst_itm_container> _lstcont, List<MasterItemComponent> _lstitemCompo, List<mst_itm_replace> _lstitemprep, List<mst_itm_mrn_com> _lstmrn, List<mst_itm_redeem_com> _lstredCom, List<BusEntityItem> _lstspCom, List<BusEntityItem> _lstcutItem, List<MasterCompanyItem> _lstcomItem, List<mst_itm_com_reorder> _lstreorder, List<mst_itm_fg_cost> _lstfg, List<ItemPrefix> _Prefix, Boolean _autoCode, string _com, out string _err)
        {
            Int32 effect = 0;
            try
            { // Nadeeka 11-07-2015
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                string _documentNo = "";
                _err = _documentNo;
                string _itemCode = "";
                _inventoryDAL = new InventoryDAL();
                _inventoryDAL.ConnectionOpen();
                _inventoryDAL.BeginTransaction();
                MasterAutoNumber _masterAuto = new MasterAutoNumber();
                _masterAuto.Aut_cate_cd = _com;
                _masterAuto.Aut_cate_tp = "COM";
                _masterAuto.Aut_direction = null;
                _masterAuto.Aut_modify_dt = null;
                _masterAuto.Aut_moduleid = "ITEM";
                _masterAuto.Aut_number = 5;//what is Aut_number
                _masterAuto.Aut_start_char = "ABS";
                _masterAuto.Aut_year = null;

                if (_autoCode == true && string.IsNullOrEmpty(_item.Mi_cd))
                {
                    MasterAutoNumber _itemobj = _inventoryDAL.GetAutoNumber(_masterAuto.Aut_moduleid, _masterAuto.Aut_direction, _masterAuto.Aut_start_char, _masterAuto.Aut_cate_tp, _masterAuto.Aut_cate_cd, _masterAuto.Aut_modify_dt, _masterAuto.Aut_year);
                    _itemCode = DateTime.Today.ToString("yy") + _itemobj.Aut_number.ToString("00000", CultureInfo.InvariantCulture);
                    _item.Mi_cd = _itemCode;
                    _inventoryDAL.UpdateAutoNumber(_masterAuto);
                }



                effect = _generalDAL.SaveItemMasterLog(_item.Mi_cd, _item.Tmp_user_id);
                effect = _generalDAL.SaveItemMaster(_item);

                if (_lstChnelWarra != null)
                {
                    effect = _generalDAL.SaveChannelWarranty_log(_item.Mi_cd);
                    _lstChnelWarra.ForEach(x => x.Cw_item_code = _item.Mi_cd);
                    effect = _generalDAL.SaveChannelWarranty(_lstChnelWarra);
                }
                if (_lstpcwara != null)
                {
                    _lstpcwara.ForEach(x => x.Pc_item_code = _item.Mi_cd);
                    effect = _generalDAL.SavePcWarranty_log(_item.Mi_cd);
                    // effect = _generalDAL.SavePcWarranty(_lstpcwara);
                    //Change as per the darshana
                    effect = _generalDAL.SAVE_SAR_PC_WARA(_lstpcwara);
                }
                if (_lstitemWara != null)
                {
                    _lstitemWara.ForEach(x => x.Mwp_itm_cd = _item.Mi_cd);
                    effect = _generalDAL.SaveStatusWarranty_log(_item.Mi_cd);
                    effect = _generalDAL.SaveStatusWarranty(_lstitemWara);
                }
                if (_lstserPrd != null)
                {
                    _lstserPrd.ForEach(x => x.Msp_itm_cd = _item.Mi_cd);
                    effect = _generalDAL.SaveServiceSchedule_log(_item.Mi_cd);
                    effect = _generalDAL.SaveServiceSchedule(_lstserPrd);
                }
                if (_lstitemClaim != null)
                {
                    _lstitemClaim.ForEach(x => x.Mic_itm_cd = _item.Mi_cd);
                    effect = _generalDAL.SaveTaxClaimItem_log(_item.Mi_cd);
                    effect = _generalDAL.SaveTaxClaimItem(_lstitemClaim);
                }
                if (_lstcont != null)
                {
                    _lstcont.ForEach(x => x.Ic_item_code = _item.Mi_cd);
                    //effect = _generalDAL.SaveContainerItem_log(_item.Mi_cd);
                    //effect = _generalDAL.SaveContainerItem(_lstcont);
                    effect = _generalDAL.SaveContainerItemNEW(_lstcont);
                }
                if (_lstitemCompo != null)
                {
                    _lstitemCompo.ForEach(x => x.Micp_itm_cd = _item.Mi_cd);
                    effect = _generalDAL.SaveComponentItem_log(_item.Mi_cd);
                    effect = _generalDAL.SaveComponentItem(_lstitemCompo);
                }
                if (_lstitemprep != null)
                {
                    _lstitemprep.ForEach(x => x.Rpl_item = _item.Mi_cd);
                    effect = _generalDAL.SaveReplaceItem_log(_item.Mi_cd);
                    effect = _generalDAL.SaveReplaceItem(_lstitemprep);
                }
                if (_lstmrn != null)
                {
                    _lstmrn.ForEach(x => x.Imc_itemcode = _item.Mi_cd);
                    effect = _generalDAL.SaveMRNItem_log(_item.Mi_cd);
                    effect = _generalDAL.SaveMRNItem(_lstmrn);
                }
                if (_lstredCom != null)
                {
                    _lstredCom.ForEach(x => x.Red_item_code = _item.Mi_cd);
                    effect = _generalDAL.SaveRedeem_log(_item.Mi_cd);
                    effect = _generalDAL.SaveRedeem(_lstredCom);
                }
                if (_lstspCom != null)
                {
                    _lstspCom.ForEach(x => x.MBII_ITM_CD = _item.Mi_cd);
                    effect = _generalDAL.SaveBusinessEntityItem_log(_item.Mi_cd, "S");
                    effect = _generalDAL.SaveBusniessEntityItem(_lstspCom);
                }
                if (_lstcutItem != null)
                {
                    _lstcutItem.ForEach(x => x.MBII_ITM_CD = _item.Mi_cd);
                    effect = _generalDAL.SaveBusinessEntityItem_log(_item.Mi_cd, "C");
                    effect = _generalDAL.SaveBusniessEntityItem(_lstcutItem);
                }
                if (_lstcomItem != null)
                {
                    _lstcomItem.ForEach(x => x.Mci_itm_cd = _item.Mi_cd);
                    effect = _generalDAL.SaveComItem_log(_item.Mi_cd);
                    //effect = _generalDAL.SaveComItem(_lstcomItem);
                    effect = _generalDAL.SaveComItemNEW(_lstcomItem);// Chg by Lakshan 27 Jan 2017
                }
                if (_lstreorder != null)
                {
                    _lstreorder.ForEach(x => x.Icr_itm_code = _item.Mi_cd);
                    effect = _generalDAL.SaveReOrder_log(_item.Mi_cd);
                    effect = _generalDAL.SaveReOrder(_lstreorder);
                }

                if (_lstfg != null)
                {
                    _lstfg.ForEach(x => x.Ifc_item_code = _item.Mi_cd);
                    effect = _generalDAL.SaveFinishGood_log(_item.Mi_cd);
                    effect = _generalDAL.SaveFinishGood(_lstfg);
                }
                if (_Prefix != null)
                {
                    foreach (ItemPrefix _itemPre in _Prefix)
                    {
                        effect = _generalDAL.SAVE_ITM_PREFIX(_itemPre);
                    }

                }


                _generalDAL.TransactionCommit();
                _inventoryDAL.TransactionCommit();
                _err = "Sucessfully saved, item code " + _item.Mi_cd;
                effect = 1;
            }
            catch (Exception err)
            {
                effect = -1;
                _err = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();
                _inventoryDAL.TransactionRollback();
            }
            return effect;
        }
        public List<mst_itm_tax_structure_det> getItemTaxStructure(string _code)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetItemTaxStructure(_code);
        }
        public Int32 SaveComItem(List<MasterCompanyItem> _lstcomItem)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.SaveComItemNEW(_lstcomItem);
        }
        public List<mst_itm_tax_structure_hdr> GetItemTaxStructureHeader(string _code)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetItemTaxStructureHeader(_code);
        }


        public MasterItem GetItemMaster(string _item)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetItemMaster(_item);
        }

        public DataTable GetItemTpAll()
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetItemTpAll();
        }
        public DataTable GetTaxCode()
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetTaxCode();
        }
        public DataTable GetContainerType()
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetContainerType();
        }
        public DataTable GetWarrantyPeriod()
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetWarrantyPeriod();
        }
        public List<mst_itm_fg_cost> GetFinishGood(string _item)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetFinishGood(_item);
        }

        public List<mst_itm_com_reorder> GetReOrder(string _item)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetReOrder(_item);
        }
        public List<MasterCompanyItem> GetComItem(string _item)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetComItem(_item);
        }

        public List<BusEntityItem> GetBuninessEntityItem(string _item, string _type)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetBuninessEntityItem(_item, _type);
        }

        //public List<mst_sup_itm> GetSupplierItem(string _item)
        //{
        //    _generalDAL = new GeneralDAL();
        //    return _generalDAL.GetSupplierItem(_item);
        //}

        public List<mst_itm_redeem_com> GetRedeem(string _item)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetRedeem(_item);
        }

        public List<mst_itm_mrn_com> getItemMRN(string _item)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getItemMRN(_item);
        }



        public List<mst_itm_replace> getReplaceItem(string _item)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getReplaceItem(_item);
        }
        public List<mst_itm_container> getRContainerItem(string _item)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getRContainerItem(_item);
        }

        public List<mst_itm_sevpd> getServiceSchedule(string _item)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getServiceSchedule(_item);
        }

        public List<mst_itm_pc_warr> getPcWarrant(string _item)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getPcWarrant(_item);
        }

        public List<mst_itm_channlwara> getChannelWarranty(string _item)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getChannelWarranty(_item);
        }


        public List<MasterItemComponent> getitemComponent(string _item)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getitemComponent(_item);
        }
        public List<MasterItemTaxClaim> getitemTaxClaim(string _item)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getitemTaxClaim(_item);
        }


        public List<MasterItemWarrantyPeriod> getitemWarranty(string _item)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getitemWarranty(_item);
        }

        public Int32 Common_send_Email()
        {
            _generalDAL = new GeneralDAL();

            MailAddress toAddress = new MailAddress("pemil@abansgroup.com");
            MailAddress cc = null;
            MailAddress bcc = null;

            string body = "Dear Sir/Madam,\n\n Insuarance receipt generated for the profit center :AAZBT \n\n" +
            "Receipt # : AAZBTAUTO1115 \n\n" + "Inv # :AAZBT-HS00355 \n\n" + "Customer : CONT-360759-K.PUSHPARASA \n\n" +
            "Address : VANNIYARS VEETHY,KALUTHAVALAI CENTRAL,KALUWANCHIKUDY,BATTICALOA \n\n" +
            "contact # : 0778843132\n\n" + "District : BATTICALOA\n\n" + "Province : EASTERN\n\n" +
            " Item & Model : HEHNKADDSCCRFWT-HUNK DOUBLE DISC -  Engine & chassis :KC13EFFGE00680 / MBLKC13EGFGE00716 - Insuarance company code :JS001  \n\n";


            string subject = "Vehicle Insuarance Receipt.";

            string[] at = new string[] { "D:\\20150529_125418.JPG" };


            return _generalDAL.Common_send_Email(toAddress, cc, bcc, subject, false, body, at);
        }


        /*Warehouse Zone*/
        string _aisle_Zn, _Row_A, _Level_R;
        public Int32 SaveWarehouseZone(List<WarehouseZone> _WarehouseZone, List<WarehouseAisle> _WarehouseAisle, List<WarehouseRow> _WarehouseRow, List<WarehouseLevel> _WarehouseLevel, DataTable _tbl)
        {
            Int32 _effect = 0;
            Tuple<int, int> _effectZone;
            Tuple<int, int> _effectAisle;
            Tuple<int, int> _effectRow;
            Tuple<int, int> _effectLevel;
            Int32 _Zseq = 0;
            Int32 _Aseq = 0;
            Int32 _Rseq = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();

                if (_WarehouseZone != null)
                {
                    if (_tbl != null && _tbl.Rows.Count > 0)
                    {
                        _aisle_Zn = _tbl.Rows[0][0].ToString();
                        _Row_A = _tbl.Rows[0][1].ToString();
                        _Level_R = _tbl.Rows[0][2].ToString();
                    }
                    foreach (WarehouseZone _Zone in _WarehouseZone)
                    {
                        // _Zseq = _Zone.Iz_zn_seq;
                        _effectZone = _generalDAL.SaveWarehouseZone(_Zone);

                        if (_Zone.Iz_zn_id == _aisle_Zn)// ||currrange !=null)
                        {

                            foreach (WarehouseAisle _Aisle in _WarehouseAisle)
                            {
                                if (_Aisle.Ia_zn_seq == 0)
                                {
                                    _Aisle.Ia_zn_seq = _effectZone.Item2;
                                }


                                _effectAisle = _generalDAL.SaveWarehouseAisle(_Aisle);
                                bool areEqual2 = System.Object.ReferenceEquals(_Aisle.Ia_asl_id, _Row_A);

                                if (_Aisle.Ia_asl_id == _Row_A)
                                {
                                    foreach (WarehouseRow _Row in _WarehouseRow)
                                    {
                                        if (_Row.Ir_zn_seq == 0)
                                        {
                                            _Row.Ir_zn_seq = _effectZone.Item2;
                                        }
                                        if (_Row.Ir_asl_seq == 0)
                                        {
                                            _Row.Ir_asl_seq = _effectAisle.Item2;
                                        }



                                        _effectRow = _generalDAL.SaveWarehouseRow(_Row);
                                        // _WarehouseLevel
                                        bool areEqual3 = System.Object.ReferenceEquals(_Row.Ir_row_id, _Level_R);

                                        if (_Row.Ir_row_id == _Level_R)
                                        {
                                            foreach (WarehouseLevel _Level in _WarehouseLevel)
                                            {
                                                if (_Level.Il_zn_seq == 0)
                                                {
                                                    _Level.Il_zn_seq = _effectZone.Item2;
                                                }
                                                if (_Level.Il_asl_seq == 0)
                                                {
                                                    _Level.Il_asl_seq = _effectAisle.Item2;
                                                }
                                                if (_Level.Il_row_seq == 0)
                                                {
                                                    _Level.Il_row_seq = _effectRow.Item2;
                                                }



                                                _effectLevel = _generalDAL.SaveWarehouseLevel(_Level);
                                            }
                                        }
                                    }

                                }

                            }
                        }
                    }
                }

                _generalDAL.TransactionCommit();
                _effect = 1;
            }
            catch (Exception err)
            {
                _effect = -1;
                // _docNo = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();
            }

            return _effect;
        }

        public Int32 SaveWarehouseByExcel(List<WarehouseZone> _WarehouseZone, List<WarehouseAisle> _WarehouseAisle, List<WarehouseRow> _WarehouseRow, List<WarehouseLevel> _WarehouseLevel, List<WarehouseBin> _WarehouseBin, out string _code)
        {
            Int32 _effect = 0;
            Tuple<int, int> _effectZone;
            Tuple<int, int> _effectAisle;
            Tuple<int, int> _effectRow;
            Tuple<int, int> _effectLevel;
            Tuple<int, int> _effectBin;
            string ZoneID = null;
            string AisleID = null;
            string RowID = null;
            string levelID = null;
            string BinLoc = null;

            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();

                if (_WarehouseZone != null)
                {

                    foreach (WarehouseZone _Zone in _WarehouseZone)
                    {
                        if (ZoneID != _Zone.Iz_zn_id)
                        {
                            AisleID = null;
                            RowID = null;
                            levelID = null;
                            BinLoc = null;
                            ZoneID = _Zone.Iz_zn_id;
                            _effectZone = _generalDAL.SaveWarehouseZone(_Zone);

                            foreach (WarehouseAisle _Aisle in _WarehouseAisle)
                            {
                                if (AisleID != _Aisle.Ia_asl_id)
                                {
                                    if (_Zone.Iz_zn_id == _Aisle.Ia_mod_by)
                                    {

                                        AisleID = _Aisle.Ia_asl_id;
                                        _Aisle.Ia_zn_seq = _effectZone.Item2;
                                        _effectAisle = _generalDAL.SaveWarehouseAisle(_Aisle);

                                        foreach (WarehouseRow _Row in _WarehouseRow)
                                        {
                                            if (RowID != _Row.Ir_row_id)
                                            {
                                                if ((_Row.Ir_tempAisleId == _Aisle.Ia_asl_id) && (_Row.Ir_tempZoonId == _Zone.Iz_zn_id))
                                                {
                                                    RowID = _Row.Ir_row_id;
                                                    _Row.Ir_zn_seq = _effectZone.Item2;
                                                    _Row.Ir_asl_seq = _effectAisle.Item2;
                                                    _effectRow = _generalDAL.SaveWarehouseRow(_Row);

                                                    foreach (WarehouseLevel _Level in _WarehouseLevel)
                                                    {
                                                        if (levelID != _Level.Il_lvl_id)
                                                        {
                                                            if ((_Level.Ir_tempRowlId == _Row.Ir_row_id) && (_Level.Ir_tempZoonId == _Row.Ir_tempZoonId))
                                                            {
                                                                levelID = _Level.Il_lvl_id;
                                                                _Level.Il_zn_seq = _effectZone.Item2;
                                                                _Level.Il_asl_seq = _effectAisle.Item2;
                                                                _Level.Il_row_seq = _effectRow.Item2;
                                                                _effectLevel = _generalDAL.SaveWarehouseLevelExcel(_Level);
                                                                if (_WarehouseBin != null)
                                                                {
                                                                    foreach (WarehouseBin _Bin in _WarehouseBin)
                                                                    {
                                                                        if (BinLoc != _Bin.Ibn_bin_cd)
                                                                        {
                                                                            if ((_Bin.Ibn_tem_LevelID == _Level.Il_lvl_id) && (_Bin.Ibn_tem_ZonelID == _Zone.Iz_zn_id))
                                                                            {
                                                                                BinLoc = _Bin.Ibn_bin_cd;

                                                                                _Bin.Ibn_zn_seq = _effectZone.Item2;
                                                                                _Bin.Ibn_asl_seq = _effectAisle.Item2;
                                                                                _Bin.Ibn_row_seq = _effectRow.Item2;
                                                                                _Bin.Ibn_lvl_seq = _effectLevel.Item2;
                                                                                _effectBin = _generalDAL.SaveWarehouseBin(_Bin);
                                                                            }
                                                                            //end if
                                                                        }

                                                                    }
                                                                    //end Foreach
                                                                }
                                                                //end if

                                                            }
                                                            //end if
                                                        } //end if
                                                    }
                                                    //end Foreach
                                                }
                                                //end if
                                            }//end if
                                        }
                                        //end Foreach

                                    }
                                    //end if
                                }
                            }
                        }


                    }
                }

                _generalDAL.TransactionCommit();
                _code = "Sucessfully Saved Excel Sheet";
                _effect = 1;
            }
            catch (Exception err)
            {
                _effect = -1;
                //_code = "ERROR : " + err.Message.ToString();
                _code = "Excel Sheet Data Invalid Please Check Data ...!";
                _generalDAL.TransactionRollback();
            }

            return _effect;
        }

        public Int32 UpdateWarehouseZone(List<WarehouseZone> _WarehouseZone, List<WarehouseAisle> _WarehouseAisle, List<WarehouseRow> _WarehouseRow, List<WarehouseLevel> _WarehouseLevel)
        {
            Int32 _effect = 0;
            try
            {

                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                if (_WarehouseZone != null)
                {
                    foreach (WarehouseZone _Zone in _WarehouseZone)
                    {
                        _effect = _generalDAL.UpdateWarehouseZone(_Zone);
                    }
                }
                if (_WarehouseAisle != null)
                {
                    foreach (WarehouseAisle _Aisle in _WarehouseAisle)
                    {
                        _effect = _generalDAL.UpdateWarehouseAisle(_Aisle);
                    }
                }
                if (_WarehouseRow != null)
                {
                    foreach (WarehouseRow _Row in _WarehouseRow)
                    {
                        _effect = _generalDAL.UpdateWarehouseRow(_Row);
                    }
                }
                if (_WarehouseLevel != null)
                {
                    foreach (WarehouseLevel _Level in _WarehouseLevel)
                    {
                        _effect = _generalDAL.UpdateWarehouseLevel(_Level);
                    }
                }




                _generalDAL.TransactionCommit();
                _effect = 1;
            }
            catch (Exception err)
            {
                _effect = -1;
                // _docNo = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();
            }

            return _effect;
        }
        public List<WarehouseZone> GetWarehouseZone(string _Com, string _Loc)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetWarehouseZone(_Com, _Loc);
        }
        public List<WarehouseAisle> GetWarehouseAisle(string _Com, string _Loc, string _Zone)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetWarehouseAisle(_Com, _Loc, _Zone);
        }
        public List<WarehouseRow> GetWarehouseRow(string _Com, string _Loc, string _Zseq, string _Aisle)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetWarehouseRow(_Com, _Loc, _Zseq, _Aisle);
        }
        public List<WarehouseLevel> GetWarehouseLevel(string _Com, string _Loc, string p_Zseq, string _Aseq, string Rseq)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetWarehouseLevel(_Com, _Loc, p_Zseq, _Aseq, Rseq);
        }
        public List<MasterUOM> GetItemUOM_active()
        {
            _generalDAL = new GeneralDAL();
            List<MasterUOM> _ListItems = null;
            _ListItems = _generalDAL.GetItemUOM();
            List<MasterUOM> filteredList = _ListItems.Where(x => x.Msu_act = true).ToList();
            return filteredList;
        }
        public DataTable GetItemCate1_active(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _generalDAL = new GeneralDAL();

            return _generalDAL.GetItemCate1_active(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetItemSubCate2_active(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _generalDAL = new GeneralDAL();

            return _generalDAL.GetItemSubCate2_active(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetItemSubCate3_active(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _generalDAL = new GeneralDAL();

            return _generalDAL.GetItemSubCate3_active(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetItemSubCate4_active(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _generalDAL = new GeneralDAL();

            return _generalDAL.GetItemSubCate4_active(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetItemSubCate5_active(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _generalDAL = new GeneralDAL();

            return _generalDAL.GetItemSubCate5_active(_initialSearchParams, _searchCatergory, _searchText);
        }
        public DataTable GetWarehouseStorage()
        {
            _generalDAL = new GeneralDAL();

            return _generalDAL.GetWarehouseStorage();
        }
        public Int32 SaveWarehouseBin(List<WarehouseBin> _WarehouseBin, List<warehouseStorageFacility> _warehouseStorageFacility, List<warehouseBinItems> _warehouseBinItems)
        {
            Int32 _effect = 0;
            Tuple<int, int> _effectBin;
            Int32 AutoNo = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                if (_WarehouseBin != null)
                {
                    foreach (WarehouseBin _Bin in _WarehouseBin)
                    {
                        _effectBin = _generalDAL.SaveWarehouseBin(_Bin);
                        AutoNo = _effectBin.Item2;
                    }
                }
                if (_warehouseStorageFacility != null)
                {
                    int sline = 1;
                    foreach (warehouseStorageFacility _StorageFacility in _warehouseStorageFacility)
                    {
                        _StorageFacility.Ibns_bin_seq = AutoNo;
                        _StorageFacility.Ibns_stor_line = sline;
                        _effect = _generalDAL.SaveWarehouseBinStoreFacility(_StorageFacility);
                        sline++;
                    }
                }
                if (_warehouseBinItems != null)
                {
                    int itemline = 1;
                    foreach (warehouseBinItems _Binitem in _warehouseBinItems)
                    {
                        _Binitem.Ibni_bin_seq = AutoNo;
                        _Binitem.Ibni_cat_line = itemline;
                        _effect = _generalDAL.SaveWarehouseBinItem(_Binitem);
                        itemline++;
                    }
                }
                _generalDAL.TransactionCommit();
                _effect = 1;
            }
            catch (Exception err)
            {
                _effect = -1;
                // _docNo = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();
            }

            return _effect;
        }

        public Int32 UpdateWarehouseBin(List<WarehouseBin> _WarehouseBin, List<warehouseStorageFacility> _warehouseStorageFacility, List<warehouseBinItems> _warehouseBinItems)
        {
            Int32 _effect = 0;
            Int32 AutoNo = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                if (_WarehouseBin != null)
                {
                    foreach (WarehouseBin _Bin in _WarehouseBin)
                    {
                        _effect = _generalDAL.UpdateWarehouseBin(_Bin);
                    }
                }
                foreach (warehouseStorageFacility _StorageFacility in _warehouseStorageFacility)
                {
                    _effect = _generalDAL.SaveWarehouseBinStoreFacility(_StorageFacility);
                }
                foreach (warehouseBinItems BinItems in _warehouseBinItems)
                {
                    _effect = _generalDAL.SaveWarehouseBinItem(BinItems);
                }
                _generalDAL.TransactionCommit();
                _effect = 1;
            }
            catch (Exception err)
            {
                _effect = -1;
                // _docNo = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();
            }

            return _effect;
        }
        public Tuple<List<WarehouseBin>, List<warehouseBinItems>, List<warehouseStorageFacility>> GetWarehouseBin(int Seq, string _Loc, string _Com, int _Zone, int _Aisle, int _Row, int _Level)
        {
            _generalDAL = new GeneralDAL();
            List<WarehouseBin> _WarehouseBin = null;
            List<warehouseBinItems> _warehouseBinItems = null;
            List<warehouseStorageFacility> _warehouseStorageFacility = null;

            _WarehouseBin = _generalDAL.GetWarehouseBin(Seq, _Loc, _Com, _Zone, _Aisle, _Row, _Level);
            _warehouseBinItems = _generalDAL.GetWarehouseBinItems(Seq);
            _warehouseStorageFacility = _generalDAL.GetWarehouseBinStorage(Seq);

            return new Tuple<List<WarehouseBin>, List<warehouseBinItems>, List<warehouseStorageFacility>>(_WarehouseBin, _warehouseBinItems, _warehouseStorageFacility);

        }
        //24/Aug/2015 Rukshan
        public DataTable GetWarehouseBinByLoc(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _generalDAL = new GeneralDAL();

            return _generalDAL.GetWarehouseBinByLoc(_initialSearchParams, _searchCatergory, _searchText);
        }

        //10/12/2015
        public DataTable GetItemSubCat4(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetItemSubCat4(_initialSearchParams, _searchCatergory, _searchText);
        }

        //10/12/2015
        public DataTable GetItemSubCat5(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetItemSubCat5(_initialSearchParams, _searchCatergory, _searchText);
        }
        #region FastForwardSCMWeb

        //Sahan
        public Int32 UpdateSupplierCurrency(SupplierCurrency currency)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.UpdateSupplierCurrency(currency);
        }

        //Sahan
        public Int32 DeactivateSupplierCurrency(SupplierCurrency currency)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.DeactivateSupplierCurrency(currency);
        }

        //Sahan
        public Int32 UpdateSupplierPorts(SupplierPort port)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.UpdateSupplierPorts(port);
        }

        //Sahan
        public Int32 DeactivateSupplierPorts(SupplierPort port)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.DeactivateSupplierPorts(port);
        }

        //Sahan
        public Int32 DeactivateSupplierItems(BusEntityItem _item)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.DeactivateSupplierItems(_item);
        }

        //Sahan

        public Int32 UpdateSupplierItems(BusEntityItem _item)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.UpdateSupplierItems(_item);
        }

        //Sahan

        public Int32 UpdateSupplierTax(BusEntityTax _tax)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.UpdateSupplierTax(_tax);
        }

        #endregion

        //09/Dec/2015 Rukshan
        public Int32 SaveDemurrage(List<DemurrageTracker> _Demurrage)
        {
            Int32 _effect = 0;
            Int32 AutoNo = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                if (_Demurrage != null)
                {
                    foreach (DemurrageTracker _Trac in _Demurrage)
                    {
                        _effect = _generalDAL.SaveDemurrage(_Trac);
                    }

                }
                _generalDAL.TransactionCommit();
                _effect = 1;
            }
            catch (Exception err)
            {
                _effect = -1;
                // _docNo = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();
            }

            return _effect;
        }

        //Lakshan 16/12/2015
        public Int32 SaveHsCode(List<HsCode> hsCodeList)
        {
            Int32 _effect = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                if (hsCodeList != null)
                {
                    foreach (HsCode hsCode in hsCodeList)
                    {
                        _effect = _generalDAL.SaveHsCode(hsCode);

                    }

                }
                _generalDAL.TransactionCommit();
                _effect = 1;
            }
            catch (Exception err)
            {
                _effect = -1;
                _generalDAL.TransactionRollback();
            }
            return _effect;
        }

        //Lakshan 17/12/2015
        public Int32 SaveHsCodeClaim(List<HsCodeClaim> hsCodeClaimList)
        {
            Int32 _effect = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                if (hsCodeClaimList != null)
                {
                    foreach (HsCodeClaim hsCodeClaim in hsCodeClaimList)
                    {
                        if (hsCodeClaim.Mhcl_claim_rt > 0)
                            _effect = _generalDAL.SaveHsCodeClaim(hsCodeClaim);
                    }

                }
                _generalDAL.TransactionCommit();
                _effect = 1;
            }
            catch (Exception err)
            {
                _effect = -1;
                _generalDAL.TransactionRollback();
            }
            return _effect;
        }

        //Lakshan 13/01/2016
        public Int32 UpdateSupplierPort(List<SupplierPort> portList)
        {
            Int32 _effect = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                if (portList != null)
                {
                    foreach (SupplierPort port in portList)
                    {
                        _effect = _generalDAL.UpdateSupplierPort(port);

                    }

                }
                _generalDAL.TransactionCommit();
                _effect = 1;
            }
            catch (Exception err)
            {
                _effect = -1;
                _generalDAL.TransactionRollback();
            }
            return _effect;
        }

        //Chamal 18-Dec-2015
        public DataTable GetCompanyInforDT(string _comCode)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.CheckCompany(_comCode);
        }

        //Rukshan 2015-12-28
        public List<ItemPrefix> GET_ITM_PREFIX(string _itm)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GET_ITM_PREFIX(_itm);
        }

        //Lakshan 2015/12/28
        public List<BusEntityItem> GetBuninessEntityItemBySupplier(string _comp, string _code)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetBuninessEntityItemBySupplier(_comp, _code);
        }


        //Rukshan 2015/12/29
        public DataTable GetBinType()
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetBinType();
        }

        //Rukshan 2015/12/29
        public DataTable[] GetITEMCOST(string _com, string _Ord, string _item, string _model, string _status, int _row, string _bl)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetITEMCOST(_com, _Ord, _item, _model, _status, _row, _bl);
        }

        //Randima 2016/07/20
        public DataTable SearchPurOrdCostingDet(string _initialSearchParams, string _PONo, string _item, string _model, string serachType, DateTime? _frmDt, DateTime? _toDt)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.SearchPurOrdCostingDet(_initialSearchParams, _PONo, _item, _model, serachType, _frmDt, _toDt);
        }

        //Randima 2016/07/20
        public DataTable SearchLatestPriceBooks(string _initialSearchParams, string _item, string _status)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.SearchLatestPriceBooks(_initialSearchParams, _item, _status);
        }

        //Randima 2016/07/21
        public DataTable SearchLatestOrders(string _item, string _tp)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.SearchLatestOrders(_item, _tp);
        }

        //Randima 2016/07/21
        public DataTable SearchPriceForGPCal(string _item, string _status, DateTime _frmDt, DateTime _toDt)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.SearchPriceForGPCal(_item, _status, _frmDt, _toDt);
        }

        //Randima 2016/08/11
        public DataTable checkOrdType(string ordNo)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.checkOrdType(ordNo);
        }

        //Randima 2016/07/26
        public DataTable SerachBLOrdDet(string _blNo, string _item, string searchType, DateTime? _frmDt, DateTime? _toDt, string com)
        {
            _generalDAL = new GeneralDAL();
            DataTable dt1 = _generalDAL.SerachBLOrdDet(_blNo, _item, searchType, _frmDt, _toDt, com);
            if (dt1 != null && dt1.Rows.Count > 0)
            {
                string grnitem = _generalDAL.GetGrnItem(com, _item);
                if (grnitem != "")
                {
                    DataTable dt2 = _generalDAL.SerachBLOrdDet(_blNo, grnitem, searchType, _frmDt, _toDt, com);
                  dt1.Merge(dt2);
                }
            }
            else
            {
                string grnitem = _generalDAL.GetGrnItem(com, _item);
                if (grnitem != "")
                { 
                dt1 = _generalDAL.SerachBLOrdDet(_blNo, grnitem, searchType, _frmDt, _toDt, com);
                }
            }
            foreach(DataRow dr in dt1.Rows)
            {
                decimal buyingRate = Convert.ToDecimal(dr["Buying"].ToString());
                decimal exchngeRt = Convert.ToDecimal(dr["CusdecRate"].ToString());
                decimal forecast = Convert.ToDecimal(dr["Forcast"].ToString());
                decimal palRs = Convert.ToDecimal(dr["DF_PAL_Rs"].ToString());
                decimal df = Convert.ToDecimal(dr["DF"].ToString());
                decimal dp = Convert.ToDecimal(dr["DP"].ToString());
                if (buyingRate==1)
                {
                    buyingRate = exchngeRt - 3;
                    dr["Buying"] = exchngeRt - 3;
                }
                if(buyingRate>0)
                {
                    dr["DF_PAL_D"] = palRs / buyingRate;
                    dr["DFUSD"] = df / buyingRate;                    
                }
                if (exchngeRt > 0)
                {
                    dr["ForeCastedCost"] = (dp / exchngeRt) * forecast;
                }
                else
                {
                    dr["ForeCastedCost"] = 0;
                }
            }
            return dt1;
        }

        // Randima 19 DEC 2016
        public DataTable GetItmCostDetail(string _item, string com, string type, string reccount, string status)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetItmCostDetail(_item, com, type, reccount, status);
        }

        //Randima 2016/08/10
        public DataTable GetVATRate(string _com, string _item)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetVATRate(_com, _item);
        }

        //Rukshan 2015/12/30
        public DataTable GetDUTYPRICE_BYITEM(string _com, string _item, DateTime _date)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetDUTYPRICE_BYITEM(_com, _item, _date);
        }
        //Rukshan 2016/1/2
        public DataTable GETLOGISTIC_COST(string _com, string _item, string _doc)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GETLOGISTIC_COST(_com, _item, _doc);
        }

        //Sahan 06 Jan 2015
        public Int32 SaveFleet(RouteHeader Header, RouteWareHouse Warehouse, RouteShowRooms ShowRoom, DataTable dtwarehouses, DataTable dtlocations, string user, string session)
        {
            Int32 _effect1 = 0;
            Int32 _effect2 = 0;
            Int32 _effect3 = 0;

            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();

                _effect1 = _generalDAL.SaveRoutes(Header);

                if (dtwarehouses.Rows.Count > 0)
                {
                    foreach (DataRow ddr1 in dtwarehouses.Rows)
                    {
                        Int32 act = 1;
                        if (ddr1["frw_act"].ToString() == "1")
                        {
                            act = 1;
                        }
                        else
                        {
                            act = 0;
                        }
                        Warehouse.FRW_CD = Header.FRH_CD;
                        Warehouse.FRW_WH_COM = ddr1["frw_wh_com"].ToString();
                        Warehouse.FRW_WH_CD = ddr1["frw_wh_cd"].ToString();
                        Warehouse.FRW_COM_CD = ddr1["frw_com_cd"].ToString();
                        Warehouse.FRW_LOC_CD = ddr1["frw_loc_cd"].ToString();
                        Warehouse.FRW_ROUTE_DISTANCE = Convert.ToDecimal(ddr1["frw_route_distance"].ToString());
                        Warehouse.FRW_ROUTE_UOM = "KM";
                        Warehouse.FRW_ACT = Convert.ToInt32(act);
                        Warehouse.FRW_CRE_BY = user;
                        Warehouse.FRW_CRE_DT = DateTime.Now;
                        Warehouse.FRW_CRE_SESSION = session;
                        _effect2 = _generalDAL.AssignWareHouses(Warehouse);
                    }
                }

                if (dtlocations.Rows.Count > 0)
                {
                    foreach (DataRow ddr2 in dtlocations.Rows)
                    {
                        Int32 act2 = 1;
                        if (ddr2["frs_act"].ToString() == "1")
                        {
                            act2 = 1;
                        }
                        else
                        {
                            act2 = 0;
                        }
                        bool ifexist = false;
                        int existLine = 0;
                        //DataTable existingLoc = LoadRouteLocations(ddr2["frs_cd"].ToString());
                        //foreach(DataRow roe in existingLoc.Rows ){
                        //    if(roe["frs_cd"].ToString()==ddr2["frs_cd"].ToString() && roe["frs_com_cd"].ToString()==ddr2["frs_com_cd"].ToString()
                        //        && roe["frs_loc_cd"].ToString() == ddr2["frs_loc_cd"].ToString())
                        //    {
                        //        ifexist = true;
                        //        existLine = Convert.ToInt32(roe["frs_line"].ToString());
                        //    }
                        //}

                        Int32 rowline = 0;
                        //if (!ifexist)
                        //{
                        Int32 _tmpLine = 0, _lineNo = 0;
                        _lineNo = Int32.TryParse(ddr2["frs_line"].ToString(), out _tmpLine) ? Convert.ToInt32(ddr2["frs_line"].ToString()) : 0;
                        if (_lineNo != 0)
                        {
                            rowline = _lineNo;
                        }
                        else
                        {
                            rowline = dtlocations.Rows.IndexOf(ddr2) + 1;
                        }

                        //}
                        //else
                        //{
                        //    rowline = existLine;
                        //}    

                        ShowRoom.FRS_CD = Header.FRH_CD;
                        ShowRoom.FRS_LINE = rowline;
                        ShowRoom.FRS_COM_CD = ddr2["frs_com_cd"].ToString();
                        ShowRoom.FRS_LOC_CD = ddr2["frs_loc_cd"].ToString();
                        ShowRoom.FRS_DISTANCE = Convert.ToDecimal(ddr2["frs_distance"].ToString());
                        ShowRoom.FRS_UOM = "KM";
                        ShowRoom.FRS_ACT = act2;
                        ShowRoom.FRS_CRE_BY = user;
                        ShowRoom.FRS_CRE_DT = DateTime.Now;
                        ShowRoom.FRS_CRE_SESSION = session;
                        if (ShowRoom.FRS_LINE == 91)
                        {
                            ShowRoom.FRS_LINE = rowline;
                        }
                        _effect3 = _generalDAL.AssignLocations(ShowRoom);
                    }
                }

                _generalDAL.TransactionCommit();

                return _effect1;
            }
            catch (Exception err)
            {
                _effect1 = -1;
                _effect2 = -1;
                _effect3 = -1;
                _generalDAL.TransactionRollback();
                return _effect1;
            }
            finally
            {
                _generalDAL.ConnectionClose();
            }
        }

        //Sahan 06 Jan 2015
        public Int32 CreateSchedule(RouteSchedule Schedule)
        {
            Int32 _effect1 = 0;

            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();

                _effect1 = _generalDAL.CreateSchedule(Schedule);

                _generalDAL.TransactionCommit();

                return _effect1;
            }
            catch (Exception err)
            {
                _effect1 = -1;
                _generalDAL.TransactionRollback();
                return _effect1;
            }
            finally
            {
                _generalDAL.ConnectionClose();
            }
        }

        //Sahan 06 Jan 2015
        public DataTable SearchRoutes(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.SearchRoutes(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Sahan 11 Jan 2016
        public DataTable LoadRouteSchedules(string p_route_code)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.LoadRouteSchedules(p_route_code);
        }

        //Sahan 11 Jan 2016
        public DataTable LoadRouteWareHouses(string p_route_code)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.LoadRouteWareHouses(p_route_code);
        }

        //Sahan 11 Jan 2016
        public DataTable LoadRouteLocations(string p_route_code)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.LoadRouteLocations(p_route_code);
        }

        //Sahan 11 Jan 2015
        public DataTable SearchWareHouses(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.SearchWareHouses(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Sahan 11 Jan 2015
        public DataTable SearchCompanies(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.SearchCompanies(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Tharaka 2016-01-12
        public Int32 BackDateProcess(String Company, String GlbUserID, String GlbUserSessionID, List<string> selected_Module_list, List<string> pc_list, List<string> loc_list, Int32 SelectedIndex, bool IsAllowTxn, DateTime from, DateTime To, DateTime FromApply, DateTime ToApply, String OtheCom, String OthChanal, String OPE, Int32 SelectedOption, out string err)
        {
            Int32 result = 0;
            err = string.Empty;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();

                _financialDAL = new FinancialDAL();
                _financialDAL.ConnectionOpen();
                _financialDAL.BeginTransaction();

                _securityDAL = new SecurityDAL();
                _securityDAL.ConnectionOpen();
                _securityDAL.BeginTransaction();


                List<BackDates> backDateList = new List<BackDates>();
                MasterLocation _loc;
                MasterProfitCenter _pc;

                if (SelectedIndex == 2)//Other
                {
                    foreach (string T_node in selected_Module_list)
                    {
                        BackDates _backdate = new BackDates();
                        _backdate.Gad_from_dt = from;
                        _backdate.Gad_to_dt = To;
                        _backdate.Gad_act_from_dt = FromApply;
                        _backdate.Gad_act_to_dt = ToApply;

                        _backdate.Gad_cre_by = GlbUserID;
                        _backdate.Gad_cre_dt = DateTime.Now;
                        _backdate.Gad_session_id = GlbUserSessionID;
                        _backdate.Gad_act = true;

                        _backdate.Gad_com = OtheCom;
                        if (SelectedOption == 0)
                        {
                            _backdate.Gad_com = OtheCom;
                            _backdate.Gad_chnl = null;
                            _backdate.Gad_ope = null;
                            _backdate.Gad_loc = null;
                            _backdate.Gad_module = T_node;
                            _backdate.Gad_rmk = "COM";
                            backDateList.Add(_backdate);
                        }
                        if (SelectedOption == 1)
                        {
                            _backdate.Gad_com = OtheCom;
                            _backdate.Gad_ope = OPE;
                            _backdate.Gad_chnl = null;
                            _backdate.Gad_loc = null;
                            _backdate.Gad_module = T_node;
                            _backdate.Gad_rmk = "OPE";
                            backDateList.Add(_backdate);
                        }
                        if (SelectedOption == 2)
                        {
                            _backdate.Gad_com = OtheCom;
                            _backdate.Gad_chnl = OthChanal;
                            _backdate.Gad_ope = null;
                            _backdate.Gad_loc = null;
                            _backdate.Gad_module = T_node;
                            _backdate.Gad_rmk = "CHNL";
                            backDateList.Add(_backdate);
                        }
                    }//foreach nodes
                }

                else if (SelectedIndex == 0)//PC
                {
                    foreach (string pc in pc_list)
                    {
                        foreach (string T_node in selected_Module_list)
                        {
                            BackDates _backdate = new BackDates();
                            _backdate.Gad_from_dt = from;
                            _backdate.Gad_to_dt = To;
                            _backdate.Gad_act_from_dt = FromApply;
                            _backdate.Gad_act_to_dt = ToApply;

                            _backdate.Gad_cre_by = GlbUserID;
                            _backdate.Gad_cre_dt = DateTime.Now;
                            _backdate.Gad_session_id = GlbUserSessionID;
                            _backdate.Gad_act = true;

                            try
                            {
                                _pc = _generalDAL.GetPCByPCCode(Company, pc);
                                _backdate.Gad_com = _pc.Mpc_com;
                                _backdate.Gad_ope = _pc.Mpc_ope_cd;
                                _backdate.Gad_chnl = _pc.Mpc_chnl;
                                _backdate.Gad_loc = _pc.Mpc_cd;
                                _backdate.Gad_module = T_node;
                                _backdate.Gad_rmk = "PC";
                            }
                            catch (Exception ex)
                            {
                                _backdate.Gad_com = "";
                                _backdate.Gad_ope = "";
                                _backdate.Gad_chnl = "";
                                _backdate.Gad_loc = "";
                                _backdate.Gad_module = "";
                                _backdate.Gad_rmk = "PC";
                            }


                            backDateList.Add(_backdate);
                        }//foreach nodes
                    }//foreach pc

                    //************ADD ON 23-05-2013******************************************************************************************************

                    List<string> Fin_pc_list = new List<string>();
                    if (SelectedIndex == 0)//profit centers
                    {
                        if (pc_list.Count > 0)
                        {
                            foreach (string PC in pc_list)
                            {
                                DateTime FRMdt = FromApply;
                                Boolean IS_Finalized = _financialDAL.Is_PC_Finalized(Company, PC, FRMdt);
                                if (IS_Finalized == true)
                                {
                                    Fin_pc_list.Add(PC);
                                }
                            }
                        }
                    }

                    if (Fin_pc_list.Count > 0)
                    {
                        string fin_pcList = "";
                        foreach (string prof in Fin_pc_list)
                        {
                            fin_pcList = fin_pcList + "\n" + prof;
                        }

                        err = "Following Profit centers are finalized for the given back date.\n" + fin_pcList;
                        result = -1;
                        return result;
                    }
                    //******************************************************************************************************************
                }
                else if (SelectedIndex == 1)
                {
                    foreach (string loc in loc_list)
                    {
                        foreach (string T_node in selected_Module_list)
                        {
                            BackDates _backdate = new BackDates();
                            _backdate.Gad_from_dt = from;
                            _backdate.Gad_to_dt = To;
                            _backdate.Gad_act_from_dt = FromApply;
                            _backdate.Gad_act_to_dt = ToApply;

                            _backdate.Gad_cre_by = GlbUserID;
                            _backdate.Gad_cre_dt = DateTime.Now;
                            _backdate.Gad_session_id = GlbUserSessionID;
                            _backdate.Gad_act = true;

                            _loc = _generalDAL.GetLocationByLocCode(Company, loc);
                            _backdate.Gad_com = _loc.Ml_com_cd;
                            _backdate.Gad_ope = _loc.Ml_ope_cd;
                            _backdate.Gad_chnl = _loc.Ml_cate_2;
                            _backdate.Gad_loc = _loc.Ml_loc_cd;
                            _backdate.Gad_module = T_node;
                            _backdate.Gad_rmk = "LOC";

                            backDateList.Add(_backdate);
                        }//foreach nodes
                    }//foreach loc
                }

                List<SystemMenus> list_menus = new List<SystemMenus>();
                DataTable DT = _securityDAL.Get_Menu(string.Empty, out list_menus);
                List<BackDates> finalBackDateList = new List<BackDates>();
                foreach (BackDates bd in backDateList)
                {
                    var _duplicate = from _dup in list_menus
                                     where _dup.Ssm_isallowbackdt == true && _dup.Ssm_name == bd.Gad_module
                                     select _dup;
                    if (_duplicate.Count() > 0)
                    {
                        finalBackDateList.Add(bd);
                    }
                }
                #region check constraints
                //check valuvation
                //check financal or both status
                result = 0;
                Int32 testValue = 0;
                foreach (BackDates _backdate in finalBackDateList)
                {
                    int _isSucessful = 0;
                    string _err = string.Empty;
                    string temOpe = (_backdate.Gad_ope == null) ? "%" : _backdate.Gad_ope;

                    _isSucessful = _generalDAL.CheckBackDate(_backdate.Gad_com, temOpe);
                    bool dayEnd = false;
                    if (_backdate.Gad_module == "m_Trans_Finance_DayEnd")
                    {
                        dayEnd = true;
                    }
                    if (_isSucessful <= 0)
                    {
                        //Check Currect Date transactions are allow or not :: Chamal 23-Dec-2013
                        if (IsAllowTxn)
                        {
                            _backdate.Gad_alw_curr_trans = true;
                        }
                        else
                        {
                            _backdate.Gad_alw_curr_trans = false;
                        }

                        List<SystemMenus> list_menus2 = new List<SystemMenus>();
                        DataTable DT2 = new DataTable();
                        try
                        {
                            DT2 = _securityDAL.Get_Menu(_backdate.Gad_module, out list_menus2);
                        }
                        catch (Exception EX)
                        {
                            testValue = 0;
                        };
                        if (list_menus2[0].Ssm_needdayend == true)
                        {
                            testValue = 1;
                        }
                        string _Err = string.Empty;
                        if (testValue > 0)
                        {
                            //save to log table
                            Day_End_Log _log = new Day_End_Log();
                            _log.Upd_com = _backdate.Gad_com;
                            _log.Upd_pc = _backdate.Gad_loc;
                            _log.Upd_log_by = GlbUserID;
                            _log.Upd_log_dt = DateTime.Now;
                            _log.Upd_cre_by = GlbUserID;
                            _log.Upd_cre_dt = DateTime.Now;
                            _log.Upd_ov_wrt = true;

                            //overwrite dayend table
                            DayEnd _dayend = new DayEnd();
                            _dayend.Upd_com = _backdate.Gad_com;
                            _dayend.Upd_pc = _backdate.Gad_loc;
                            _dayend.Upd_cre_by = GlbUserID;
                            _dayend.Upd_cre_dt = DateTime.Now;
                            _dayend.Upd_ov_wrt = true;

                            if (_backdate.Gad_loc == null)
                            {
                                result = SaveBackDateNonCommit(_backdate, testValue, _log, _dayend, true, false, dayEnd, out _Err);
                            }
                            else
                            {
                                result = SaveBackDateNonCommit(_backdate, testValue, _log, _dayend, false, false, dayEnd, out _Err);
                            }
                        }
                        else
                        {
                            result = SaveBackDateNonCommit(_backdate, testValue, null, null, false, false, dayEnd, out _Err);
                        }
                        if (result == -1)
                        {
                            err = _Err.ToString();
                            result = -1;
                            return result;
                        }
                    }
                    else
                    {
                        err = "Not allowed to save back date for: " + _backdate.Gad_loc;
                        result = -1;
                        return result;
                    }
                }

                #endregion check constraints

                _generalDAL.TransactionCommit();
                _financialDAL.TransactionCommit();
            }
            catch (Exception ex)
            {
                err = ex.Message;
                _generalDAL.TransactionRollback();
                _financialDAL.TransactionRollback();
                result = -1;
            }
            return result;
        }

        //Tharaka 2016-01-12
        public Int32 SaveBackDateNonCommit(BackDates _backdate, int _Dayend, Day_End_Log _log, DayEnd _dayend, bool noLoc, bool isPc, bool dayEndModule, out string _err)
        {
            Int32 effect = 0;

            if (dayEndModule || _Dayend > 0)
            {
                if (noLoc && isPc)
                {
                    int _numdays = DateTime.Now.Subtract(_backdate.Gad_act_from_dt).Days;
                    List<MasterLocation> loclist = GetLocationByCompany(_backdate.Gad_com);
                    for (int tem = 0; tem < loclist.Count; tem++)
                    {
                        List<DayEnd> _list = _financialDAL.GetDayEnds(_backdate.Gad_com, loclist[tem].Ml_loc_cd, _backdate.Gad_act_from_dt, DateTime.Now);

                        foreach (DayEnd day in _list)
                        {
                            Day_End_Log _daylog = new Day_End_Log();
                            _daylog.Upd_com = day.Upd_com;
                            _daylog.Upd_cre_by = day.Upd_cre_by;
                            _daylog.Upd_cre_dt = day.Upd_cre_dt;
                            _daylog.Upd_dt = day.Upd_dt;
                            _daylog.Upd_log_dt = DateTime.Now;
                            _daylog.Upd_ov_wrt = day.Upd_ov_wrt;
                            _daylog.Upd_pc = day.Upd_pc;
                            _generalDAL.SaveDayEndLog(_daylog);
                        }
                        _financialDAL.DeleteDayEnd(_backdate.Gad_com, loclist[tem].Ml_loc_cd, _backdate.Gad_act_from_dt, DateTime.Now);
                    }
                }
                else if (noLoc && !isPc)
                {
                    DataTable _pcDt = _generalDAL.GetPartyCodes(_backdate.Gad_com, "%");
                    for (int tem = 0; tem < _pcDt.Rows.Count; tem++)
                    {
                        List<DayEnd> _list = _financialDAL.GetDayEnds(_backdate.Gad_com, _pcDt.Rows[tem]["mpc_cd"].ToString(), _backdate.Gad_act_from_dt, DateTime.Now);

                        foreach (DayEnd day in _list)
                        {
                            Day_End_Log _daylog = new Day_End_Log();
                            _daylog.Upd_com = day.Upd_com;
                            _daylog.Upd_cre_by = day.Upd_cre_by;
                            _daylog.Upd_cre_dt = day.Upd_cre_dt;
                            _daylog.Upd_dt = day.Upd_dt;
                            _daylog.Upd_log_dt = DateTime.Now;
                            _daylog.Upd_ov_wrt = day.Upd_ov_wrt;
                            _daylog.Upd_pc = day.Upd_pc;
                            _generalDAL.SaveDayEndLog(_daylog);
                        }
                        _financialDAL.DeleteDayEnd(_backdate.Gad_com, _pcDt.Rows[tem]["mpc_cd"].ToString(), _backdate.Gad_act_from_dt, DateTime.Now);
                    }
                }

                else if (!noLoc)
                {
                    List<DayEnd> _list = _financialDAL.GetDayEnds(_backdate.Gad_com, _backdate.Gad_loc, _backdate.Gad_act_from_dt, DateTime.Now);

                    foreach (DayEnd day in _list)
                    {
                        Day_End_Log _daylog = new Day_End_Log();
                        _daylog.Upd_com = day.Upd_com;
                        _daylog.Upd_cre_by = day.Upd_cre_by;
                        _daylog.Upd_cre_dt = day.Upd_cre_dt;
                        _daylog.Upd_dt = day.Upd_dt;
                        _daylog.Upd_log_dt = DateTime.Now;
                        _daylog.Upd_ov_wrt = day.Upd_ov_wrt;
                        _daylog.Upd_pc = day.Upd_pc;
                        _generalDAL.SaveDayEndLog(_daylog);
                    }
                    _financialDAL.DeleteDayEnd(_backdate.Gad_com, _backdate.Gad_loc, _backdate.Gad_act_from_dt, DateTime.Now);
                }
            }

            effect = _generalDAL.SaveBackDate(_backdate);
            _err = string.Empty;
            return effect;
        }
        /*Lakshan 13/01/2016*/
        public DataTable GET_PORT_DATA_BY_CD(string code)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GET_PORT_DATA_BY_CD(code);
        }



        //Lakshan 22/01/2016
        public Int32 SaveWarrAmendReq(List<WarrantyAmendRequest> warrAmendReq, out string err)
        {
            string error = "";
            Int32 _effect = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                if (warrAmendReq != null)
                {
                    foreach (WarrantyAmendRequest war in warrAmendReq)
                    {
                        _effect = _generalDAL.SaveWarrAmendReq(war);
                    }

                }
                _generalDAL.TransactionCommit();
                //_effect = 1;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                _effect = -1;
                _generalDAL.TransactionRollback();
            }
            err = error;
            return _effect;
        }

        //Lakshan 22/01/2016
        public Int32 UpdateWarrAmendReq(List<WarrantyAmendRequest> warrAmendReq, out string err)
        {
            string error = "";
            Int32 _effect = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                if (warrAmendReq != null)
                {
                    foreach (WarrantyAmendRequest war in warrAmendReq)
                    {
                        _effect = _generalDAL.UpdateWarrAmendReq(war);
                    }

                }
                _generalDAL.TransactionCommit();
                //_effect = 1;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                _effect = -1;
                _generalDAL.TransactionRollback();
            }
            err = error;
            return _effect;
        }

        //Lakshan 25/01/2016
        public Int32 SaveSerialMasterLog(List<SerialMasterLog> objs, out string err)
        {
            string error = "";
            Int32 _effect = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                if (objs != null)
                {
                    foreach (SerialMasterLog i in objs)
                    {
                        _effect = _effect + _generalDAL.SaveSerialMasterLog(i);
                    }
                }
                _generalDAL.TransactionCommit();
            }
            catch (Exception ex)
            {
                error = ex.Message;
                _effect = -1;
                _generalDAL.TransactionRollback();
            }
            err = error;
            return _effect;
        }

        //Lakshan 25/01/2016
        public Int32 UpdateWarrantyMasterAmend(List<InventoryWarrantyDetail> objs, out string err)
        {
            string error = "";
            Int32 _effect = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                if (objs != null)
                {
                    foreach (InventoryWarrantyDetail i in objs)
                    {
                        _effect = _effect + _generalDAL.UpdateWarrantyMasterAmend(i);
                    }
                }
                _generalDAL.TransactionCommit();
            }
            catch (Exception ex)
            {
                error = ex.Message;
                _effect = -1;
                _generalDAL.TransactionRollback();
            }
            err = error;
            return _effect;
        }


        public WarrantyAmendRequest GetWarrantyAmendReqData(WarrantyAmendRequest obj)
        {
            /*Lakshan 13/01/2016*/
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetWarrantyAmendReqData(obj);
        }
        public DataTable GET_REF_SER2()
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GET_REF_SER2();
        }

        //Sahan 29 Jan 2016
        public DataTable LoadLocationDetailsByCode(string company, string code)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.LoadLocationDetailsByCode(company, code);
        }


        //Lakshan 29/01/2016
        public DataTable SearchCurrencyData(string curr)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.SearchCurrencyData(curr);
        }

        //Lakshan 29/01/2016
        public bool CheckCurrency(string curr)
        {
            _generalDAL = new GeneralDAL();
            DataTable _tbl = _generalDAL.SearchCurrencyData(curr);
            if (_tbl != null && _tbl.Rows.Count > 0) return true; else return false;
        }

        //Lakshan 29/01/2016
        public string Get_Currency_desc(string curr)
        {
            _generalDAL = new GeneralDAL();
            string _desc = "";
            DataTable _tbl = _generalDAL.SearchCurrencyData(curr);
            if (_tbl != null && _tbl.Rows.Count > 0)
            {
                _desc = Convert.ToString(_tbl.Rows[0]["MCR_DESC"]);
                return _desc;
            }
            return _desc;
        }


        //Lakshan 02/02/2016
        public bool CheckCustomer(string cust)
        {
            _generalDAL = new GeneralDAL();
            DataTable _tbl = _generalDAL.SearchCustomerData(cust);
            if (_tbl != null && _tbl.Rows.Count > 0) return true; else return false;
        }

        //Lakshan 02/02/2016
        public string Get_Customer_desc(string cust_cd)
        {
            _generalDAL = new GeneralDAL();
            string _desc = "";
            DataTable _tbl = _generalDAL.SearchCustomerData(cust_cd);
            if (_tbl != null && _tbl.Rows.Count > 0)
            {
                _desc = Convert.ToString(_tbl.Rows[0]["mbe_name"]);
                return _desc;
            }
            return _desc;
        }

        //Lakshan 02/02/2016
        public DataTable SearchCustomerData(string customer)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.SearchCustomerData(customer);
        }


        //Tharaka 2016-02-02
        public List<PromotionVoucherPara> GET_VOUPARA_BY_CD(string Code)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GET_VOUPARA_BY_CD(Code);
        }

        //Tharaka 2016-02-03
        public List<REF_ALERT_SETUP> GET_REF_ALERT_SETUP(string Com, string Type, string PartyCode, string ModuleName, string Status)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GET_REF_ALERT_SETUP(Com, Type, PartyCode, ModuleName, Status);
        }

        //Sahan 05/Feb/2016
        public Int32 SaveItemKitComponent(ItemKitComponent ItemKitComp)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.SaveItemKitComponent(ItemKitComp);
        }

        //Sahan 05/Feb/2016
        public DataTable LoadItemKitComponents(string item)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.LoadItemKitComponents(item);
        }

        //Sahan 8 Feb 2016
        public DataTable SearchMainItems(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.SearchMainItems(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Sahan 8 Feb 2016
        public DataTable SearchComponentItems(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.SearchComponentItems(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Sahan 08/Feb/2016
        public DataTable LoadSerialByBin(string company, string location, string serial, string _bin)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.LoadSerialByBin(company, location, serial, _bin);
        }
        //Rukshan 11/Feb/2016
        public bool Check_MRN_Item(string _com, string _loc, string _item, out string qty)
        {

            bool _result = false;
            _generalDAL = new GeneralDAL();
            DataTable _val = _generalDAL.Check_MRN_Item(_com, _loc, _item);

            if (_val.Rows.Count > 0)
            {

                qty = _val.Rows[0][0].ToString();
                if (qty == "0")
                {
                    return _result = true;
                }
                return _result = false;
            }
            else
            {
                qty = "";
                return _result = true;
            }
        }

        //Lakshan 17/02/2016
        public List<MasterLocationNew> GetMasterLocations(MasterLocationNew _loc)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetMasterLocations(_loc);
        }

        //Lakshan 17/02/2016
        public MasterLocationNew GetMasterLocation(MasterLocationNew _loc)
        {
            _generalDAL = new GeneralDAL();
            List<MasterLocationNew> _locations = GetMasterLocations(_loc);
            if (_locations.Count > 0)
            {
                return _locations.FirstOrDefault();
            }
            return null;
        }
        //Update by Lakshan 17/02/2016
        public Int32 UpdateLocationMasterNew(MasterLocationNew _loc, string err)
        {
            Int32 _effect = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    _generalDAL = new GeneralDAL();
                    _inventoryDAL = new InventoryDAL();
                    _generalDAL.ConnectionOpen();
                    _inventoryDAL.ConnectionOpen();

                    _effect = _generalDAL.UpdateLocationMasterNew(_loc);

                    if (_effect == 1)
                    {
                        WarehouseBin _binData = _inventoryDAL.GET_INR_BIN_DATA(new WarehouseBin() { Ibn_com_cd = _loc.Ml_com_cd, Ibn_loc_cd = _loc.Ml_loc_cd }).FirstOrDefault();
                        if (_binData == null)
                        {
                            WarehouseBin _Bin = new WarehouseBin();
                            _Bin.Ibn_act = true;
                            //_Bin.Ibn_asl_seq = 2372;
                            _Bin.Ibn_asl_seq = 2;
                            _Bin.Ibn_bin_cd = _loc.Ml_loc_cd + "B01";
                            _Bin.Ibn_bin_desc = _loc.Ml_loc_cd;
                            _Bin.Ibn_capacity = 0;
                            _Bin.Ibn_capacity_used = 0;
                            _Bin.Ibn_com_cd = _loc.Ml_com_cd;
                            _Bin.Ibn_cre_by = _loc.Ml_cre_by;
                            _Bin.Ibn_cre_when = _loc.Ml_cre_dt;
                            _Bin.Ibn_height = 0;
                            _Bin.Ibn_is_def = true;
                            _Bin.Ibn_length = 0;
                            _Bin.Ibn_loc_cd = _loc.Ml_loc_cd;
                            //_Bin.Ibn_lvl_seq = 2374;
                            _Bin.Ibn_lvl_seq = 4;
                            //_Bin.Ibn_row_seq = 2373;
                            _Bin.Ibn_row_seq = 3;
                            _Bin.Ibn_session_id = _loc.Ml_session_id;
                            _Bin.Ibn_weight = 0;
                            _Bin.Ibn_width = 0;
                            //_Bin.Ibn_zn_seq = 2371;
                            _Bin.Ibn_zn_seq = 1;
                            _Bin.Ibn_tp = "";
                            _Bin.Ibn_weight_uom = null;
                            _Bin.Ibn_diam_uom = null;
                            //Add some data
                            _Bin.Ibn_cre_by = _loc.Ml_cre_by;
                            _Bin.Ibn_cre_when = _loc.Ml_cre_dt;

                            _Bin.Ibn_mod_by = _loc.Ml_mod_by;
                            _Bin.Ibn_mod_when = _loc.Ml_mod_dt;

                            _Bin.Ibn_act = true;
                            _Bin.Ibn_is_def = true;
                            _effect = _generalDAL.UpdateWarehouseBin(_Bin);
                        }
                    }
                    scope.Complete();
                    _generalDAL.TransactionCommit();
                    return _effect;
                }
            }
            catch (Exception ex)
            {
                _effect = -1;
                err = ex.Message;
                _generalDAL.TransactionRollback();
                return _effect;
            }
            finally
            {
                _generalDAL.ConnectionClose();
            }
        }

        //Lakshan 2016-Mar-01
        public Int32 Save_profit_center_new(MasterProfitCenter _pcheader, string _chnl, string _schnl, string _area, string _reg, string _zone, string _user, out string _err)
        {
            int effect = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();

                _saleDAL = new SalesDAL();
                _saleDAL.ConnectionOpen();

                effect = _generalDAL.Save_profit_center_new(_pcheader, _user);
                _err = "";

                MasterSalesPriorityHierarchy infoCom = new MasterSalesPriorityHierarchy();
                infoCom.Mpi_act = true;
                infoCom.Mpi_pc_cd = _pcheader.Mpc_cd;
                infoCom.Mpi_com_cd = _pcheader.Mpc_com;
                infoCom.Mpi_tp = "PC_PRIT_HIERARCHY";
                infoCom.Mpi_val = _pcheader.Mpc_com;
                infoCom.Mpi_cd = "COM";
                effect = _generalDAL.Save_MST_PC_INFO(infoCom);

                MasterSalesPriorityHierarchy infoPC = new MasterSalesPriorityHierarchy();
                infoPC.Mpi_act = true;
                infoPC.Mpi_pc_cd = _pcheader.Mpc_cd;
                infoPC.Mpi_com_cd = _pcheader.Mpc_com;
                infoPC.Mpi_tp = "PC_PRIT_HIERARCHY";
                infoPC.Mpi_val = _pcheader.Mpc_cd;
                infoPC.Mpi_cd = "PC";
                effect = _generalDAL.Save_MST_PC_INFO(infoPC);

                MasterSalesPriorityHierarchy infoChnl = new MasterSalesPriorityHierarchy();
                infoChnl.Mpi_act = true;
                infoChnl.Mpi_pc_cd = _pcheader.Mpc_cd;
                infoChnl.Mpi_com_cd = _pcheader.Mpc_com;
                infoChnl.Mpi_tp = "PC_PRIT_HIERARCHY";
                infoChnl.Mpi_val = _chnl;
                infoChnl.Mpi_cd = "CHNL";
                effect = _generalDAL.Save_MST_PC_INFO(infoChnl);

                MasterSalesPriorityHierarchy infoSChnl = new MasterSalesPriorityHierarchy();
                infoSChnl.Mpi_act = true;
                infoSChnl.Mpi_pc_cd = _pcheader.Mpc_cd;
                infoSChnl.Mpi_com_cd = _pcheader.Mpc_com;
                infoSChnl.Mpi_tp = "PC_PRIT_HIERARCHY";
                infoSChnl.Mpi_val = _schnl;
                infoSChnl.Mpi_cd = "SCHNL";
                effect = _generalDAL.Save_MST_PC_INFO(infoSChnl);

                MasterSalesPriorityHierarchy infoArea = new MasterSalesPriorityHierarchy();
                infoArea.Mpi_act = true;
                infoArea.Mpi_pc_cd = _pcheader.Mpc_cd;
                infoArea.Mpi_com_cd = _pcheader.Mpc_com;
                infoArea.Mpi_tp = "PC_PRIT_HIERARCHY";
                infoArea.Mpi_val = _area;
                infoArea.Mpi_cd = "AREA";
                effect = _generalDAL.Save_MST_PC_INFO(infoArea);

                MasterSalesPriorityHierarchy infoReg = new MasterSalesPriorityHierarchy();
                infoReg.Mpi_act = true;
                infoReg.Mpi_pc_cd = _pcheader.Mpc_cd;
                infoReg.Mpi_com_cd = _pcheader.Mpc_com;
                infoReg.Mpi_tp = "PC_PRIT_HIERARCHY";
                infoReg.Mpi_val = _reg;
                infoReg.Mpi_cd = "REGION";
                effect = _generalDAL.Save_MST_PC_INFO(infoReg);

                MasterSalesPriorityHierarchy infoZone = new MasterSalesPriorityHierarchy();
                infoZone.Mpi_act = true;
                infoZone.Mpi_pc_cd = _pcheader.Mpc_cd;
                infoZone.Mpi_com_cd = _pcheader.Mpc_com;
                infoZone.Mpi_tp = "PC_PRIT_HIERARCHY";
                infoZone.Mpi_val = _zone;
                infoZone.Mpi_cd = "ZONE";
                effect = _generalDAL.Save_MST_PC_INFO(infoZone);

                MasterSalesPriorityHierarchy infonew = new MasterSalesPriorityHierarchy();
                infonew.Mpi_act = true;
                infonew.Mpi_pc_cd = _pcheader.Mpc_cd;
                infonew.Mpi_com_cd = _pcheader.Mpc_com;
                infonew.Mpi_tp = "PC_PRIT_HIERARCHY";
                infonew.Mpi_val = "GRUP01";
                infonew.Mpi_cd = "GPC";
                effect = _generalDAL.Save_MST_PC_INFO(infonew);

                effect = _saleDAL.AssignChannelAccestoPC(_schnl, _pcheader.Mpc_cd, _pcheader.Mpc_com, _user);

                if (_pcheader != null)
                {
                    DataTable _defaultParameters = new DataTable();
                    _defaultParameters = _generalDAL.GetSysParaDetails(_pcheader.Mpc_com, _pcheader.Mpc_chnl, "DEFAULTPC");

                    string _type = "PC";
                    string _code = string.Empty;
                    if (_defaultParameters.Rows.Count > 0)
                    {
                        _type = _defaultParameters.Rows[0]["msp_rest_cate_tp"] == DBNull.Value ? string.Empty : _defaultParameters.Rows[0]["msp_rest_cate_tp"].ToString();
                        _code = _defaultParameters.Rows[0]["msp_rest_cate_cd"] == DBNull.Value ? string.Empty : _defaultParameters.Rows[0]["msp_rest_cate_cd"].ToString();

                        if ((!string.IsNullOrEmpty(_type)) && (!string.IsNullOrEmpty(_code)))
                        {
                            List<Hpr_SysParameter> _parameters = new List<Hpr_SysParameter>();
                            _parameters = _generalDAL.GetHprSysParaDetails(_type, _code);
                            if (_parameters != null && _parameters.Count > 0)
                            {
                                foreach (Hpr_SysParameter _parameter in _parameters)
                                {
                                    //duplicate existing data for new PC
                                    _parameter.Hsy_seq = _generalDAL.GetSerialID();
                                    _parameter.Hsy_pty_cd = _pcheader.Mpc_cd;
                                    effect = _saleDAL.Save_hpr_sys_para(_parameter);
                                }
                            }
                        }
                    }
                }


                _generalDAL.ConnectionClose();
                _saleDAL.ConnectionClose();
            }
            catch (Exception err)
            {
                effect = -1;
                _err = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();
                _saleDAL.TransactionRollback();

            }
            return effect;

        }

        //Lakshan 2016-Mar-04
        public List<MasterBinLocation> GetMasterBinLocations(MasterBinLocation _bin)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetMasterBinLocations(_bin);
        }

        //Lakshan 2016-Mar-04
        public MasterBinLocation GetMasterBinLocation(MasterBinLocation _bin)
        {
            _generalDAL = new GeneralDAL();
            List<MasterBinLocation> _bins = GetMasterBinLocations(_bin);
            if (_bins.Count > 0)
            {
                return _bins.FirstOrDefault();
            }
            return null;
        }

        /*Lakshan 13/01/2016*/
        public List<SupplierPort> GetSupplierPorts(SupplierPort _port)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetSupplierPort(_port);
        }

        /* Lakshan 16 Mar 2016 */
        public SupplierPort GetSupplierPort(SupplierPort _port)
        {
            _generalDAL = new GeneralDAL();
            List<SupplierPort> _list = GetSupplierPorts(_port);
            if (_list.Count > 0)
            {
                return _list.FirstOrDefault();
            }
            return null;
        }

        //Sahan 25 March 2016
        public Int32 UpdateBankAccounts(MasterBankAccount Account)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.UpdateBankAccounts(Account);
        }

        //Sahan 25 March 2016
        public DataTable LoadBankAccountData(MasterBankAccount AccountData)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.LoadBankAccountData(AccountData);
        }

        //Sahan 25 March 2016
        public Int32 UpdateAccountFacility(BankAccountFacility Facility)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.UpdateAccountFacility(Facility);
        }

        //Sahan 25 March 2016
        public DataTable LoadBankAccountFacilityData(BankAccountFacility FacilityData)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.LoadBankAccountFacilityData(FacilityData);
        }

        //Sahan 30 MAr 2016
        public DataTable SearchAgent(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.SearchAgent(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Sahan 30 MAr 2016
        public DataTable SearchBankAccounts(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.SearchBankAccounts(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Lakshan 01 Apr 2016
        public List<TransportMethod> GET_TRANS_METH(TransportMethod _obj)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GET_TRANS_METH(_obj);
        }
        //Lakshan 01 Apr 2016
        public List<TransportParty> GET_TRANSPORT_PTY(TransportParty _obj)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GET_TRANSPORT_PTY(_obj);
        }

        //Sahan April 6 2016
        public MasterBusinessEntity GetAllCustomerProfileByCom(string CustCD, string nic, string DL, string PPNo, string brNo, string com)
        {
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();

            MasterBusinessEntity ent = _generalDAL.GetAllCustomerProfileByCom(CustCD, nic, DL, PPNo, brNo, com);
            _generalDAL.ConnectionClose();
            return ent;
        }
        //Rukshan 12/Apr/2016
        public DataTable GetItemStatusByCom(string _comp)
        {
            _generalDAL = new GeneralDAL();
            DataTable _list = _generalDAL.GetItemStatusByCom(_comp);
            return _list;
        }
        //Lakshan 12 APr 2016
        public Int32 Save_Int_Transport(Transport _obj)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.Save_Int_Transport(_obj);
        }

        //Rukshan 12/Apr/2016
        public bool CHECKALLOCATE_CUS(string _company, string _profitcenter, string _emp)
        {
            _generalDAL = new GeneralDAL();
            bool _list = _generalDAL.CHECKALLOCATE_CUS(_company, _profitcenter, _emp);
            return _list;
        }

        //Kelum : SaveNewEmployee : 20-April-2016
        //Kelum : Modified SaveNewEmployee with other tables : 25-April-2016
        public Int32 SaveNewEmployee(Employee _employeeNew, List<MasterProfitCenter_LocationEmployee> _MPcE, List<MasterProfitCenter_LocationEmployee> _MLE, List<MasterCustomerEmployee> _MstCusEmp, string _com, out string _err)
        {
            Int32 _effect = 0;

            try
            {
                string _documentNo = "";
                _err = _documentNo;

                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                _effect = _generalDAL.SaveNewEmployee(_employeeNew);

                /*
                if (_MPcE != null)
                {
                    _MPcE.ForEach(x => x._mpce_epf = _employeeNew.ESEP_epf);
                    _effect = _generalDAL.SaveEmployeeProfitCenter(_MPcE);                    
                }

                if (_MLE != null)
                {
                    _MPcE.ForEach(x => x._mpce_epf = _employeeNew.ESEP_epf);
                    _effect = _generalDAL.SaveEmployeeLocation(_MLE);
                }

                if (_MstCusEmp != null)
                {
                    _MPcE.ForEach(x => x._mpce_epf = _employeeNew.ESEP_epf);
                    _effect = _generalDAL.SaveEmployeeCustomers(_MstCusEmp);
                }
                */

                if (_MPcE != null)
                {
                    _MPcE.ForEach(x => x._mpce_epf = _employeeNew.ESEP_epf);
                    _effect = _generalDAL.SaveEmployeeProfitCenter(_MPcE);
                }

                if (_MLE != null)
                {
                    _MLE.ForEach(x => x._mpce_epf = _employeeNew.ESEP_epf);
                    _effect = _generalDAL.SaveEmployeeLocation(_MLE);
                }

                if (_MstCusEmp != null)
                {
                    _MstCusEmp.ForEach(x => x._mpce_emp_cd = _employeeNew.ESEP_epf);
                    _effect = _generalDAL.SaveEmployeeCustomers(_MstCusEmp);
                }

                _generalDAL.TransactionCommit();

            }


            catch (Exception err)
            {
                _effect = -1;
                _err = "ERROR : " + err.Message.ToString();
                _generalDAL.ConnectionClose();
            }

            return _effect;
        }

        //Kelum : UpdateEmployee : 20-April-2016
        public Int32 UpdateEmployee(Employee _employeeNew, List<MasterProfitCenter_LocationEmployee> _MPcE, List<MasterProfitCenter_LocationEmployee> _MLE, List<MasterCustomerEmployee> _MstCusEmp, string _com, out string _err)
        {
            Int32 _effect = 0;

            try
            {
                string _documentNo = "";
                _err = _documentNo;

                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                _effect = _generalDAL.SaveNewEmployee(_employeeNew);

                /*
                if (_MPcE != null)
                {
                    //  _MPcE.ForEach(x => x._mpce_epf = _employeeNew.ESEP_epf);
                    _effect = _generalDAL.SaveEmployeeProfitCenter(_MPcE);
                }

                if (_MLE != null)
                {
                  _MPcE.ForEach(x => x._mpce_epf = _employeeNew.ESEP_epf);
                  _effect = _generalDAL.SaveEmployeeLocation(_MLE);
                }

                if (_MstCusEmp != null)
                {
                    //  _MPcE.ForEach(x => x._mpce_epf = _employeeNew.ESEP_epf);
                    _effect = _generalDAL.SaveEmployeeCustomers(_MstCusEmp);
                }
                 
                */

                if (_MPcE != null)
                {
                    _MPcE.ForEach(x => x._mpce_epf = _employeeNew.ESEP_epf);
                    _effect = _generalDAL.SaveEmployeeProfitCenter(_MPcE);
                }

                if (_MLE != null)
                {
                    _MLE.ForEach(x => x._mpce_epf = _employeeNew.ESEP_epf);
                    _effect = _generalDAL.SaveEmployeeLocation(_MLE);
                }

                if (_MstCusEmp != null)
                {
                    _MstCusEmp.ForEach(x => x._mpce_emp_cd = _employeeNew.ESEP_epf);
                    _effect = _generalDAL.SaveEmployeeCustomers(_MstCusEmp);
                }

                _generalDAL.TransactionCommit();

            }


            catch (Exception err)
            {
                _effect = -1;
                _err = "ERROR : " + err.Message.ToString();
                _generalDAL.ConnectionClose();
            }

            return _effect;
        }

        //Kelum : Get EPF No : 22-April-2016
        public DataTable Get_EPFNo(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.Get_EPFNo(_initialSearchParams, _searchCatergory, _searchText);

        }

        //Kelum : Get EMP Code : 22-April-2016
        public DataTable Get_EMPCode(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.Get_EMPCode(_initialSearchParams, _searchCatergory, _searchText);

        }

        //Kelum : Get Category : 22-April-2016
        public DataTable Get_Category(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.Get_Category(_initialSearchParams, _searchCatergory, _searchText);

        }

        //Kelum : Get SubCategory : 22-April-2016
        public DataTable Get_SubCategory(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.Get_SubCategory(_initialSearchParams, _searchCatergory, _searchText);

        }

        //Kelum : Get Employee Details : 2016-April-26
        public Employee GetEmployeeMaster(string _employee, string _com)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetEmployeeMaster(_employee, _com);
        }

        // Kelum : Get Employee Profit Center or Location Details : 2016-April-26
        public List<MasterProfitCenter_LocationEmployee> getEmployeeProfitCenter_Location(string _employee, string _com, string PCorL)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getEmployeeProfitCenter_Location(_employee, _com, PCorL);
        }

        // Kelum : Get Employee's Assinged Customers : 2016-April-26
        public List<MasterCustomerEmployee> getEmployeeCustomers(string _com, string _empcode)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getEmployeeCustomers(_com, _empcode);
        }

        // Rukshan  : 2016-April-27
        public DataTable getSeason(string _com)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getSeason(_com);
        }

        // Lakshan 2016 Apr 30
        public List<Transport> GET_INT_TRANSPORT(Transport _obj)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GET_INT_TRANSPORT(_obj);
        }

        // Kelum : Get Customer's Office of Entry : 2016-May-02
        public List<MasterBusinessOfficeEntry> getCustomerOfficeofEntry(string _com, string _emp, string _type)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getCustomerOfficeofEntry(_com, _emp, _type);
        }

        // Rukshan 2016 Apr 30
        public DataTable SearchBOQDocNo(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.SearchBOQDocNo(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Kelum : Get PO/BL Number : 2016-June-01 
        public DataTable Get_PoBlNumber(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.Get_PoBlNumber(_initialSearchParams, _searchCatergory, _searchText);

        }

        //Kelum : Get DOC_GRN Number : 2016-June-01
        public DataTable Get_DocGrnNumber(string _initialSearchParams, DateTime? _docdtfrom, DateTime? _docdtto, string _searchCatergory, string _searchText)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.Get_DocGrnNumber(_initialSearchParams, _docdtfrom, _docdtto, _searchCatergory, _searchText);

        }
        //Kelum : Get DOC_GRN Number : 2016-June-01
        public DataTable Get_DocGrnNumber2(string _initialSearchParams, DateTime? _docdtfrom, DateTime? _docdtto, string _searchCatergory, string _searchText)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.Get_DocGrnNumber2(_initialSearchParams, _docdtfrom, _docdtto, _searchCatergory, _searchText);

        }

        //Kelum : Get Entry Number : 2016-June-01
        public DataTable Get_EntryNumber(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.Get_EntryNumber(_initialSearchParams, _searchCatergory, _searchText);

        }

        //Kelum : Load Document Wise Balances : 2016-June-01
        public DataTable LoadDocumentWiseBalance(string _initialSearchParams, string _poblnumber, string _docno, string _entryno)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.LoadDocumentWiseBalance(_initialSearchParams, _poblnumber, _docno, _entryno);

        }
        public DataTable GetAllChannel(string _company, string _channel)
        {
            _generalDAL = new GeneralDAL();
            DataTable _tbl = _generalDAL.CheckChannel(_company, _channel);
            return _tbl;
        }
        // Lakshan 2016 Jul 30
        public List<MasterStatus> GET_MST_STUS(MasterStatus _obj)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GET_MST_STUS(_obj);

        }
        //Lakshika 2016-07-21
        public List<mst_commodel> GetCompanyByModel(string _code = null)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetCompanyByModel(_code);
        }

        //Lakshika 2016-07-21
        public List<mst_model_replace> GetReplacedModelsByModel(string _code = null)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetReplacedModelsByModel(_code);
        }

        //Lakshika 2016-07-22
        public List<BusinessEntityVal> GetModelClassificationByModel(string _code = null)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetModelClassificationByModel(_code);
        }
        //Lakshan 2016-08-29
        public List<MasterBankBranch> GetBankBranchData(MasterBankBranch _obj)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetBankBranchData(_obj);
        }
        //Lakshan 2016-08-29
        public List<SystemUserLoc> GET_SEC_USER_LOC_DATA(SystemUserLoc _obj)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GET_SEC_USER_LOC_DATA(_obj);
        }
        //Nuwan 2016.11.23
        public List<PendingExchgAppHead> getPendingExchangeRequests(string company, string status, string appcd, string user, string page, string pagesize, string searchVal, string searchFld, string finstus, Int32 appLvl)
        {
            _generalDAL = new GeneralDAL();
            _securityDAL = new SecurityDAL();
            if (_securityDAL.Is_Report_DR("APPROVAL_PORTAL") == true) _generalDAL.ConnectionOpen_DR();
            return _generalDAL.getPendingExchangeRequests(company, status, appcd, user, page, pagesize, searchVal, searchFld, finstus, appLvl);
        }
        //Nuwan 2016.11.24
        public string getApproveItemsModels(string reqno)
        {
            string models = string.Empty;
            _generalDAL = new GeneralDAL();
            DataTable dt = _generalDAL.getApproveItemsModels(reqno);
            if (dt.Rows.Count > 0)
            {
                int i = 1;
                foreach (DataRow row in dt.Rows)
                {
                    models += row["MI_MODEL"].ToString() + ((dt.Rows.Count != i) ? " /" : "");
                    i++;
                }
            }
            return models;
        }
        public List<UnitConvert> GetModelUOM(string MODEL)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetModelUOM(MODEL);
        }


        // Rukshan 2016 Apr 30
        public DataTable SearchProDocNo(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _from, DateTime _to)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.SearchProDocNo(_initialSearchParams, _searchCatergory, _searchText, _from, _to);
        }
        public List<ApprovalPermission> getApprovalPermission(string userid, string docLoc, string appcd)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getApprovalPermission(userid, docLoc, appcd);
        }
        public List<ReferenceDetails> getReferenceDetails(string reqNo)
        {
            _generalDAL = new GeneralDAL();
            List<ReferenceDetails> reqData = _generalDAL.getReferenceDetails(reqNo);
            foreach (ReferenceDetails data in reqData)
            {
                data.MMCT_SDESC = _generalDAL.getSubTypDescription(data.GRAH_SUB_TYPE);
                data.ITM_SATUS_LIST = _generalDAL.getItemStatusList(data.GRAH_COM);
                data.APP_ITM_DET = _generalDAL.getApproveItemDetails(data.GRAD_REQ_PARAM);
                string ser = _generalDAL.getItemInSerial(reqNo, data.GRAD_LINE);
                data.APP_ITM_DET.MI_IN_SER = (!string.IsNullOrEmpty(ser)) ? ser : "N/A";
                data.TOT_UNIT_PRICE = Math.Round(data.GRAD_VAL5 + data.GRAD_ANAL18);
                data.GRAH_REQ_REM = _generalDAL.getReqCatCode(reqNo);
                data.APP_REQ_CATE = _generalDAL.getAppReqCateList();
            }

            return reqData;
        }
        public List<InvoiceCustomer> getInvoiceCustomer(string invNo)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getInvoiceCustomer(invNo);
        }
        public string getAccountSchema(string accNo)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getAccountSchema(accNo);
        }
        public string getPromotionDesc(string promocd)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getPromotionDesc(promocd);
        }
        public List<ApprovalHistory> getApprovalHistoryDetails(string reqNo, string appcd)
        {
            _generalDAL = new GeneralDAL();
            List<ApprovalHistory> data = _generalDAL.getApprovalHistoryDetails(reqNo);
            if (data.Count > 0)
            {
                foreach (ApprovalHistory list in data)
                {
                    List<ApproveHistoryDetails> histryDet = new List<ApproveHistoryDetails>();
                    list.CATE_DESC = _generalDAL.getCategoryDescription(list.GRAH_REQ_REM);
                    string hedText = string.Empty;
                    string appLvl = _generalDAL.getReqestApprovalLeval(appcd);
                    string level = list.GRAH_APP_LVL.ToString();
                    if (list.GRAH_APP_LVL.ToString() == appLvl)
                        level = "Final";

                    string headerText = "Rejected by(Level:" + level + "):- ";
                    if (list.GRAH_APP_STUS == "A")
                    {
                        headerText = "Approved by(Level:" + level + "):- ";
                    }
                    else if (list.GRAH_APP_STUS == "P")
                    {
                        headerText = "Pending :- ";
                        if (list.GRAH_APP_LVL == 0 || list.GRAH_APP_LVL == -1)
                            headerText = "Approval Requested by:- ";
                    }
                    list.APP_HED_TEXT = headerText;
                    histryDet = _generalDAL.getApprovalItemHistory(reqNo, list.GRAH_APP_LVL, list.GRAH_REF_LINE_NO);

                    foreach (ApproveHistoryDetails aphisDet in histryDet)
                    {
                        ApproveItemDetail appItm = new ApproveItemDetail();
                        appItm = _generalDAL.getApproveItemDetails(aphisDet.GRAD_REQ_PARAM);
                        if (aphisDet.GRAD_ANAL5 == "EX-ISSUE(INV)")
                        {
                            appItm.MI_STATUS_DESC = _generalDAL.getItemStatusDesc(aphisDet.GRAD_ANAL1);
                        }
                        else if (aphisDet.GRAD_ANAL5 == "EX-RECEIVE")
                        {
                            appItm.MI_STATUS_DESC = _generalDAL.getItemStatusDesc(aphisDet.GRAD_ANAL15);
                        }

                        string ser = _generalDAL.getItemInSerial(reqNo, aphisDet.GRAD_LINE);
                        appItm.MI_IN_SER = (!string.IsNullOrEmpty(ser)) ? ser : "N/A";
                        aphisDet.TOT_UNIT_PRICE = Math.Round(aphisDet.GRAD_VAL5 + aphisDet.GRAD_ANAL18);
                        aphisDet.APP_ITEM_DET = appItm;
                    }
                    list.PPROVE_HISTORY_DET = histryDet;

                    List<ApprovalRemarkLog> rmkLog = new List<ApprovalRemarkLog>();
                    rmkLog = _generalDAL.getApprovalRmkDetails(reqNo, list.GRAH_APP_LVL);
                    list.APP_REMARK_LOG = rmkLog;

                }

            }
            return data;
        }
        public ReferenceDetails getCreditTypData(string reqNo)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getCreditTypData(reqNo);
        }
        public bool validateItemCode(string itemCode)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.validateItemCode(itemCode);
        }
        public bool ApproveExchangeRequest(ApprovalData data, out string error)
        {
            string status = data.status;
            error = string.Empty;
            bool e = false;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                _saleDAL = new SalesDAL();
                _saleDAL.ConnectionOpen();
                _saleDAL.BeginTransaction();
                List<InvoiceCustomer> customer = new List<InvoiceCustomer>();
                customer = _generalDAL.getInvoiceCustomer(data.RefferenceData[0].GRAH_FUC_CD);

                string ApproveLevel = _generalDAL.getApproveLevelForFinApprovalNew(data.AppCd, data.RefferenceData[0].GRAH_COM, customer[0].SAH_PC, data.RefferenceData[0].GRAH_REQ_REM);
                if (Convert.ToInt32(data.UserPermission.SARP_APP_LVL) >= Convert.ToInt32(ApproveLevel))
                {
                    Int32 eff = _generalDAL.UpdateApprovalHeader(data, status);
                    if (eff <= 0)
                    {
                        error = "Unable to update request header details.";
                        e = true;
                        goto A;
                    }
                }
                else
                {
                    Int32 eff = _generalDAL.UpdateApprovalHeader(data, null);
                    if (eff <= 0)
                    {
                        error = "Unable to update request header details.";
                        e = true;
                        goto A;
                    }
                }
                Int32 refline = _generalDAL.getMaxNumber(data.ReqNo, Convert.ToInt32(data.UserPermission.SARP_APP_LVL));

                Int32 effect = _generalDAL.InserHdrLogAccExchange(data.ReqNo, data.status, data.RemarkText, Convert.ToInt32(data.UserPermission.SARP_APP_LVL), data.ReqCategory, data.LoginUser, data, refline);


                Int32 lineno = 0;//_generalDAL.getMaxNumber(data.ReqNo, -1) + 1;

                if (data.RefferenceData.Count > 0)
                {
                    //lineno = _generalDAL.getMaxNumber(data.ReqNo, Convert.ToInt32(data.UserPermission.SARP_APP_LVL));
                    Int32 ar = 0;
                    foreach (ReferenceDetails refDet in data.RefferenceData)
                    {
                        lineno++;
                        if (refDet.GRAD_ANAL5 == "EX-RECEIVE")
                        {
                            string app_item_code = refDet.GRAD_REQ_PARAM;
                            Int32 app_value = Convert.ToInt32(data.UserPermission.SARP_APP_LVL);
                            string GRAD_ANAL7 = refDet.GRAD_ANAL1;
                            string GRAD_ANAL8 = data.InItemDet[0].Status;
                            decimal discountRate = data.InItemDet[0].Discount;
                            decimal total_price = refDet.GRAD_VAL5;

                            string GRAD_DATE_PARAM = data.priceConsDate;
                            string GRAD_ANAL10 = data.AccScheduleDt;
                            decimal discount = refDet.GRAD_ANAL18;
                            decimal qty = refDet.GRAD_VAL1;
                            decimal totalUPrice = refDet.GRAD_VAL5;
                            decimal tot_price = refDet.GRAD_VAL5;
                            string status1 = refDet.GRAD_ANAL1;
                            refDet.GRAD_ANAL1 = data.InItemDet[ar].Status;
                            refDet.GRAD_ANAL15 = data.InItemDet[ar].Status;
                            refDet.GRAD_ANAL18 = data.InItemDet[ar].Discount;
                            refDet.GRAD_RCV_FREE_ITM = data.InItemDet[ar].ReciveFreeItm;
                            refDet.GRAD_ANAL10 = data.AccScheduleDt;
                            refDet.GRAD_ANAL11 = data.Warrenty;
                            Int32 eff = _generalDAL.InsertApproveItemLogDetails(data.ReqNo, refDet, app_item_code, data.Warrenty, app_value, lineno, data, refline);

                        }
                        else if (refDet.GRAD_ANAL5 == "EX-ISSUE(INV)")
                        {
                            string app_item_code = data.InItemDet[ar].ItemCode;
                            Int32 app_value = Convert.ToInt32(data.UserPermission.SARP_APP_LVL);
                            refDet.GRAD_ANAL1 = data.InItemDet[ar].Status;
                            refDet.GRAD_ANAL18 = data.InItemDet[ar].Discount;
                            //List<InvoiceCustomer> customer = new List<InvoiceCustomer>();
                            //customer = _generalDAL.getInvoiceCustomer(refDet.GRAH_FUC_CD);
                            decimal totalUPrice = refDet.TOT_UNIT_PRICE - refDet.GRAD_ANAL18;
                            refDet.GRAD_RCV_FREE_ITM = data.InItemDet[ar].ReciveFreeItm;
                            refDet.GRAD_VAL2 = refDet.GRAD_VAL2;
                            refDet.GRAD_VAL5 = totalUPrice;
                            refDet.GRAD_ANAL10 = data.AccScheduleDt;
                            refDet.GRAD_ANAL11 = data.Warrenty;
                            List<MasterItemTax> _list = _saleDAL.GetItemTax(refDet.GRAH_COM, refDet.GRAD_REQ_PARAM, data.InItemDet[ar].Status, null, null);
                            if (_list.Count > 0)
                            {
                                refDet.GRAD_VAL4 = Math.Round(refDet.GRAD_VAL5 * (_list[0].Mict_tax_rate / (100 + _list[0].Mict_tax_rate)));
                            }
                            else
                            {
                                refDet.GRAD_VAL4 = 0;
                            }
                            if (data.InItemDet[ar].Active == 1)
                            {
                                Int32 eff = _generalDAL.InsertApproveItemLogDetails(data.ReqNo, refDet, app_item_code, data.Warrenty, app_value, lineno, data, refline);
                            }
                        }

                        ar++;
                    }
                    if (data.NewItems.Count > 0)
                    {
                        foreach (ApprovalNewItem item in data.NewItems)
                        {
                            lineno++;
                            ReferenceDetails refDet = new ReferenceDetails();
                            refDet.GRAD_ANAL1 = item.Status;
                            refDet.GRAD_ANAL18 = item.Discount;
                            refDet.GRAD_VAL1 = item.Quantity;
                            refDet.GRAD_ANAL2 = item.PriceBook;
                            refDet.GRAD_ANAL3 = item.PriceBookLvl;
                            refDet.GRAD_ANAL5 = "EX-ISSUE(INV)";
                            refDet.GRAD_DATE_PARAM = Convert.ToDateTime(data.priceConsDate).Date;
                            refDet.GRAD_ANAL11 = data.Warrenty;
                            refDet.GRAD_ANAL10 = data.AccScheduleDt;
                            refDet.GRAD_ANAL4 = data.RefferenceData[0].GRAD_ANAL4;
                            refDet.GRAD_VAL5 = item.TotPrice;
                            List<MasterItemTax> _list = _saleDAL.GetItemTax(data.RefferenceData[0].GRAH_COM, item.ItemCode, item.Status, null, null);
                            if (_list.Count > 0)
                            {
                                refDet.GRAD_VAL4 = Math.Round(refDet.GRAD_VAL5 * (_list[0].Mict_tax_rate / (100 + _list[0].Mict_tax_rate)));
                                refDet.GRAD_VAL2 = Math.Round(Convert.ToInt32(item.TotUnitPrice) - (Convert.ToInt32(item.TotUnitPrice) * (_list[0].Mict_tax_rate / (100 + _list[0].Mict_tax_rate))), 2);
                            }
                            else
                            {
                                refDet.GRAD_VAL4 = 0;
                                refDet.GRAD_VAL2 = Convert.ToInt32(item.TotUnitPrice);
                            }


                            refDet.GRAD_VAL3 = data.RefferenceData[0].GRAD_VAL3;
                            refDet.GRAD_IS_RT1 = data.RefferenceData[0].GRAD_IS_RT1;
                            Int32 app_value = Convert.ToInt32(data.UserPermission.SARP_APP_LVL);

                            Int32 eff = _generalDAL.InsertApproveItemLogDetails(data.ReqNo, refDet, item.ItemCode, data.Warrenty, app_value, lineno, data, refline);
                            if (eff == 0)
                            {
                                error = "Unable to add new item details.";
                                e = true;
                                goto A;
                            }
                        }
                    }
                    if (data.ItmQty != 0)
                    {
                        lineno++;
                        if (data.ItmQty > 100 || data.ItmQty < 0)
                        {
                            error = "Item quantity range is 0-100.";
                            e = true;
                            goto A;
                        }
                        Int32 effc = _generalDAL.InsertItmQtyLog(data, data.RefferenceData[0], Convert.ToInt32(data.UserPermission.SARP_APP_LVL), lineno, refline);
                        if (effc == 0)
                        {
                            error = "Unable to add item quantity log details.";
                            e = true;
                            goto A;
                        }
                    }

                    List<ReferenceDetailsLog> getLogDet = _generalDAL.getApprovalLogDetails(data.ReqNo, Convert.ToInt32(data.UserPermission.SARP_APP_LVL));
                    if (getLogDet.Count > 0)
                    {
                        Int32 br = 0;
                        Int32 effs = _generalDAL.updateInItmReqDet(getLogDet[0], data.ReqNo);
                        foreach (ReferenceDetailsLog log in getLogDet)
                        {
                            log.GRAD_ANAL12 = "";
                            if (log.GRAD_ANAL5 == "EX-RECEIVE")
                            {
                                Int32 eff = _generalDAL.updateReqAppDetails(log, data.ReqNo);
                            }
                            else if (log.GRAD_ANAL5 == "EX-ISSUE(INV)")
                            {

                                if (data.NewItems.Count > 0)
                                {
                                    var item = data.NewItems.Where(x => x.ItemCode == log.GRAD_REQ_PARAM && x.Status == log.GRAD_ANAL1).ToList();
                                    if (item.Count > 0)
                                    {
                                        Int32 maxline = _generalDAL.getMaxNumber(data.ReqNo, -1);
                                        Int32 nextLine = maxline + 1;
                                        Int32 eff1 = _generalDAL.InserReqAppDetails(log, nextLine, data.ReqNo, 1, data);
                                        if (eff1 <= 0)
                                        {
                                            error = "Unable to update request item details.";
                                            e = true;
                                            goto A;
                                        }
                                    }
                                    else
                                    {
                                        //Int32 eff = _generalDAL.updateInItmReqDet(log, data.ReqNo);
                                        //if (eff <= 0)
                                        //{
                                        //    error = "Unable to update request item details.";
                                        //    e = true;
                                        //    goto A;
                                        //}
                                        Int32 maxline = _generalDAL.getMaxNumber(data.ReqNo, -1);
                                        Int32 nextLine = maxline + 1;
                                        Int32 act = data.InItemDet.Where(x => x.ItemCode == log.GRAD_REQ_PARAM).ToList()[0].Active;
                                        Int32 eff1 = _generalDAL.InserReqAppDetails(log, nextLine, data.ReqNo, act, data);
                                        if (eff1 <= 0)
                                        {
                                            error = "Unable to update request item details.";
                                            e = true;
                                            goto A;
                                        }
                                    }
                                }
                                else
                                {
                                    //Int32 eff = _generalDAL.updateInItmReqDet(log, data.ReqNo);
                                    //if (eff <= 0)
                                    //{
                                    //    error = "Unable to update request item details.";
                                    //    e = true;
                                    //    goto A;
                                    //}
                                    Int32 maxline = _generalDAL.getMaxNumber(data.ReqNo, -1);
                                    Int32 nextLine = maxline + 1;
                                    Int32 act = data.InItemDet.Where(x => x.ItemCode == log.GRAD_REQ_PARAM).ToList()[0].Active;
                                    Int32 eff1 = _generalDAL.InserReqAppDetails(log, nextLine, data.ReqNo, act, data);
                                    if (eff1 <= 0)
                                    {
                                        error = "Unable to update request item details.";
                                        e = true;
                                        goto A;
                                    }
                                }

                            }
                            else if (log.GRAD_ANAL5 == "EX_ALW_QTY")
                            {
                                //Int32 eff = _generalDAL.updateInItmReqDet(log, data.ReqNo);
                                Int32 maxline = _generalDAL.getMaxNumber(data.ReqNo, -1);
                                Int32 nextLine = maxline + 1;
                                Int32 eff1 = _generalDAL.InserReqAppQtyDetails(log, nextLine, data.ReqNo);
                                if (eff1 <= 0)
                                {
                                    error = "Unable to update request item details.";
                                    e = true;
                                    goto A;
                                }
                            }
                            br++;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _generalDAL.TransactionRollback();
                _generalDAL.ConnectionClose();
                _saleDAL.TransactionRollback();
                _saleDAL.ConnectionClose();
                error = ex.Message.ToString();
                return false;
            }
        A:
            if (e)
            {
                _generalDAL.TransactionRollback();
                _generalDAL.ConnectionClose();
                _saleDAL.TransactionRollback();
                _saleDAL.ConnectionClose();
                return false;
            }
            else
            {
                _generalDAL.TransactionCommit();
                _generalDAL.ConnectionClose();
                _saleDAL.TransactionCommit();
                _saleDAL.ConnectionClose();
                return true;
            }
        }
        public List<ApprovalPriceBook> getRelatedPriceBook(string pc, string type, string com)
        {
            _generalDAL = new GeneralDAL();
            List<ApprovalPriceBook> pbbok = _generalDAL.getRelatedPriceBook(pc, type, com);
            return pbbok;
        }
        public List<ApprovalPriceBookLevel> getRelatedPriceBookLevel(string pc, string type, string com, string pbook)
        {
            _generalDAL = new GeneralDAL();
            List<ApprovalPriceBookLevel> pbbok = _generalDAL.getRelatedPriceBookLevel(pc, type, com, pbook);
            return pbbok;
        }
        public List<ApprovalItemStatus> getPbLvlItemStatus(string pb, string lvl, string com)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getPbLvlItemStatus(pb, lvl, com);
        }
        public decimal getPricebookItemPrice(string pbook, string pbooklvl, string itmcd, string cuscd, DateTime curdate, Int32 qty, string com, string status)
        {
            _saleDAL = new SalesDAL();
            _generalDAL = new GeneralDAL();
            decimal price = _generalDAL.getitemPrice(pbook, pbooklvl, itmcd, qty, curdate, cuscd, com);
            List<MasterItemTax> _list = _saleDAL.GetItemTax(com, itmcd, status, null, null);
            if (_list.Count > 0)
            {
                price = price + (price * _list[0].Mict_tax_rate / 100);
            }
            return price;
        }
        public decimal getPricebookItemPriceNew(string pbook, string pbooklvl, string itmcd, string cuscd, DateTime curdate, Int32 qty, string com, string status)
        {
            _saleDAL = new SalesDAL();
            _generalDAL = new GeneralDAL();
            _inventoryDAL = new InventoryDAL();
            decimal price = _generalDAL.getitemPrice(pbook, pbooklvl, itmcd, qty, curdate, cuscd, com);
            MasterCompany _masterComp = null;
            List<MasterItemTax> _list = new List<MasterItemTax>();
            _masterComp = GetCompByCode(com);
            if (_masterComp.MC_TAX_CALC_MTD == "1")
            {
                MasterItem _mstItem = _inventoryDAL.GetItem(com, itmcd);
                _list = _saleDAL.GetItemTax_strucbase(com, itmcd, status, string.Empty, string.Empty, _mstItem.Mi_anal1);
            }
            else
            {
                _list = _saleDAL.GetItemTax(com, itmcd, status, null, null);
            }
            if (_list.Count > 0)
            {
                price = price + (price * _list[0].Mict_tax_rate / 100);
            }
            return price;
        }
        public ApproveItemDetail getApproveItemDetails(string reqParam)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getApproveItemDetails(reqParam);

        }

        //subodana
        public Int32 AmmendEPFNew(string empcd, string epfnw, string com)
        {
            Int32 _effect = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                //
                _effect = _generalDAL.AmmendEPFNew(empcd, epfnw, com);
                //
                _generalDAL.TransactionCommit();

            }
            catch (Exception ex)
            {
                _effect = -1;
                _generalDAL.TransactionRollback();
            }
            return _effect;
        }
        public List<InventorySerialN> getSerialDetailsForDeVal(string com, string location)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getSerialDetailsForDeVal(com, location);
        }
        //Lakshan 03 Jan 2017
        public Int32 SaveFleetVehicleMaster(FleetVehicleMaster _obj, out string _err)
        {
            _err = "";
            Int32 _effect = 0;
            _generalDAL = new GeneralDAL();
            try
            {
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                _effect = _generalDAL.SaveFleetVehicleMaster(_obj);
                if (_effect > 0)
                {
                    _generalDAL.TransactionCommit();
                    _generalDAL.ConnectionClose();
                }
                else
                {
                    _effect = -1;
                    _generalDAL.TransactionRollback();
                    _generalDAL.ConnectionClose();
                }
            }
            catch (Exception ex)
            {
                _err = ex.Message;
                _effect = -1;
                _generalDAL.TransactionRollback();
                _generalDAL.ConnectionClose();
            }
            return _effect;
        }
        //Lakshan 03 Jan 2017
        public List<FleetVehicleMaster> GET_FLT_VEHICLE_DATA(FleetVehicleMaster _obj)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GET_FLT_VEHICLE_DATA(_obj);
        }
        //Lakshan 24 Jan 2017
        public List<RouteHeader> GET_ROUTE_HDR_DATA(RouteHeader _obj)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GET_ROUTE_HDR_DATA(_obj);
        }
        //Lakshan 24 Jan 2017
        public List<RouteShowRooms> GET_ROUTE_SHOWROOM_DATA(RouteShowRooms _obj)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GET_ROUTE_SHOWROOM_DATA(_obj);
        }
        //Lakshan 24 Jan 2017
        public List<RouteWareHouse> GET_ROUTE_WAREHOUSE_DATA(RouteWareHouse _obj)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GET_ROUTE_WAREHOUSE_DATA(_obj);
        }
        //Lakshan 24 Jan 2017
        public List<RouteSchedule> GET_ROUTE_SHEDULE_DATA(RouteSchedule _obj)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GET_ROUTE_SHEDULE_DATA(_obj);
        }
        public List<mst_itm_pc_warr> getPcWarrantNew(string _item)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getPcWarrantNew(_item);
        }

        public DataTable GetItemPrefix(string _item)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetItemPrefix(_item);
        }
        public Int32 SaveItemKitComponentNEW(List<ItemKitComponent> _comlist, List<mst_itm_fg_cost> _cost, List<MasterKitComFineItem> _FITEM, out string _err)
        {
            _err = "";
            Int32 _effect = 0;
            _generalDAL = new GeneralDAL();
            try
            {
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                if (_comlist != null)
                {
                    foreach (ItemKitComponent _com in _comlist)
                    {
                        _effect = _generalDAL.SaveItemKitComponentNEW(_com);
                    }
                }
                if (_cost != null)
                {
                    _effect = _generalDAL.SaveFinishGood(_cost);
                }
                if (_FITEM != null)
                {
                    if (_FITEM.Count > 0)
                    {
                        _effect = _generalDAL.SaveFinishGood_Itm(_FITEM);
                    }

                }
                if (_effect > 0)
                {
                    _generalDAL.TransactionCommit();
                    _generalDAL.ConnectionClose();
                }
                else
                {
                    _effect = -1;
                    _generalDAL.TransactionRollback();
                    _generalDAL.ConnectionClose();
                }
            }
            catch (Exception ex)
            {
                _err = ex.Message;
                _effect = -1;
                _generalDAL.TransactionRollback();
                _generalDAL.ConnectionClose();
            }
            return _effect;
        }

        public DataTable CostInqirytblitemlist(List<string> itmList, string com, bool chkGPAllDoc, string pricebook, string pricelevel, string reccount, string status, string user)
        {
            _generalDAL = new GeneralDAL();
            MsgPortalBLL _messageptal = new MsgPortalBLL();
            _inventoryDAL = new InventoryDAL();
            try
            {

                DataTable dt = new DataTable("dt");
                DataTable dt1;
                DataTable dt3 = new DataTable("dt3");
                Dictionary<string, decimal> priValue = new Dictionary<string, decimal>();
                foreach (string itm in itmList)
                {
                    if (chkGPAllDoc)
                    {
                        dt1 = _generalDAL.GetItmCostDetail(itm, com, "All", reccount, status);
                    }
                    else
                    {
                        dt1 = _generalDAL.GetItmCostDetail(itm, com, "GRN", reccount, status);
                    }

                    dt.Merge(dt1);

                    DataTable GPTbl = _generalDAL.SearchPriceForGPCal(itm, "A", DateTime.Today, DateTime.Today);

                    DataTable GPTblNew = GPTbl.Clone();
                    GPTblNew.Columns.Add(new DataColumn("Markup_Per", typeof(decimal)));
                    GPTblNew.Columns.Add(new DataColumn("GP_Per", typeof(decimal)));
                    GPTblNew.Columns.Add(new DataColumn("Expct_MU_Per", typeof(decimal)));
                    GPTblNew.Columns.Add(new DataColumn("Expct_GP_Per", typeof(decimal)));
                    GPTblNew.Columns.Add(new DataColumn("New_MU_Price", typeof(decimal)));
                    GPTblNew.Columns.Add(new DataColumn("New_GP_Price", typeof(decimal)));

                    int count = 0;
                    foreach (DataRow row in GPTbl.Rows)
                    {
                        if (row["sapd_price_type"].ToString() != "0")
                        {
                            GPTblNew.ImportRow(row);
                        }
                        else
                        {
                            foreach (DataRow rowNew in GPTblNew.Rows)
                            {
                                if (row["sapd_pbk_lvl_cd"].ToString() == rowNew["sapd_pbk_lvl_cd"].ToString() && rowNew["sapd_price_type"].ToString() == "0")
                                {
                                    count++;
                                }
                            }
                            if (count < 1)
                            {
                                GPTblNew.ImportRow(row);
                            }
                            count = 0;
                        }

                    }
                    decimal cost = 5.7m;

                    foreach (DataRow rowNew in GPTblNew.Rows)
                    {

                        if (rowNew["sapd_pb_tp_cd"].ToString() == pricebook && rowNew["sapd_pbk_lvl_cd"].ToString() == pricelevel)
                        {
                            priValue[rowNew["SAPD_ITM_CD"].ToString()] = Convert.ToDecimal(rowNew["sapd_itm_price"].ToString());
                        }
                    }
                }
                dt3 = dt.Clone();
                dt3.Columns.Add(new DataColumn("CUR_PRI", typeof(decimal)));
                dt3.Columns.Add(new DataColumn("PRI_BK", typeof(string)));
                dt3.Columns.Add(new DataColumn("PRI_LVL", typeof(string)));
                dt3.Columns.Add(new DataColumn("GP_PER", typeof(decimal)));
                dt3.Columns.Add(new DataColumn("MK_PER", typeof(decimal)));
                dt3.Columns.Add(new DataColumn("CUR_NWMU", typeof(decimal)));
                dt3.Columns.Add(new DataColumn("CUR_NWGP", typeof(decimal)));
                foreach (DataRow row in dt.Rows)
                {
                    dt3.ImportRow(row);
                }
                foreach (DataRow row in dt3.Rows)
                {
                    string searchItm = row["ITEM"].ToString() == "" ? row["ITEM1"].ToString() : row["ITEM"].ToString();
                    decimal curItmPri = Convert.ToDecimal(priValue.Where(x => x.Key == searchItm).Select(x => x.Value).FirstOrDefault().ToString());
                    decimal costnw = 0;
                    row["CUR_PRI"] = curItmPri.ToString();
                    row["PRI_BK"] = pricebook;
                    row["PRI_LVL"] = pricelevel;
                    if (IsNumeric(row["DP_COST"].ToString()))
                    {
                        costnw = Convert.ToDecimal(row["DP_COST"].ToString());
                    }
                    decimal itemprice = Convert.ToDecimal(row["CUR_PRI"].ToString());
                    decimal gp_per = 0;
                    decimal mk_per = 0;
                    if (itemprice == 0)
                    {

                    }
                    else
                    {
                        gp_per = ((itemprice - costnw) / itemprice) * 100;
                    }
                    if (costnw == 0)
                    {

                    }
                    else
                    {
                        mk_per = ((itemprice - costnw) / costnw) * 100;
                    }
                    row["GP_PER"] = gp_per;
                    row["MK_PER"] = mk_per;


                }

                if (dt3 != null && dt3.Rows.Count > 0)
                {
                    string out1 = "";
                    string _toemail = "";
                    dt3.TableName = "dt";
                    string _email = "";
                    string path = _messageptal.ExportExcel2007(com, user, dt, out out1);
                    DataTable dtCreateUser = _inventoryDAL.GetUserNameByUserID(user);
                    if (dtCreateUser != null && dtCreateUser.Rows.Count > 0)
                    {
                        if (dtCreateUser.Rows[0]["se_email"] != null && IsValidEmail(dtCreateUser.Rows[0]["se_email"].ToString()))
                        {
                            _toemail = dtCreateUser.Rows[0]["se_email"].ToString();
                        }
                    }
                    SmtpClient smtpClient = new SmtpClient();
                    MailMessage message = new MailMessage();
                    MailAddress fromAddress = new MailAddress(_generalDAL.GetMailAddress(), _generalDAL.GetMailDispalyName());
                    smtpClient.Host = _generalDAL.GetMailHost();
                    smtpClient.Port = 25;
                    message.From = fromAddress;
                    string FOOTER = _generalDAL.GetMailFooterMsg();
                    _email = "Dear Sir/Madam, <br>" + "Your Report generated.  <br> Please find the  system generated  Report.";
                    message.To.Add(_toemail);
                    message.Subject = "Cost Enquiry  Report";
                    //message.CC.Add(new MailAddress(_info.Mmi_superior_mail));
                    //message.Bcc.Add(new MailAddress(""));
                    Attachment at = new Attachment(path);
                    message.Attachments.Add(at);
                    message.IsBodyHtml = false;
                    message.Body = _email;
                    message.IsBodyHtml = true;
                    message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                    // Send SMTP mail
                    smtpClient.Send(message);

                }

                return dt3;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public bool IsValidEmail(string email)
        {
            string pattern = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";

            System.Text.RegularExpressions.Match match = Regex.Match(email.Trim(), pattern, RegexOptions.IgnoreCase);
            if (match.Success)
                return true;
            else
                return false;
        }

        //public DataTable CostInqirytblitem(List<string> itmList, string com, bool chkGPAllDoc, string pricebook, string pricelevel, string item, string reccount, string status)
        //{
        //    _generalDAL = new GeneralDAL();
        //    DataTable dt = new DataTable();
        //    DataTable dt1;
        //    Dictionary<string, decimal> priValue = new Dictionary<string, decimal>();
        //    decimal curpri = 0.0m;
        //    if (chkGPAllDoc)
        //    {
        //        dt = _generalDAL.GetItmCostDetail(item, com, "ALL", reccount, status);
        //    }
        //    else
        //    {
        //        dt = _generalDAL.GetItmCostDetail(item, com, "GRN", reccount, status);
        //    }

        //    DataTable GPTbl = _generalDAL.SearchPriceForGPCal(item, "A", DateTime.Today, DateTime.Today);

        //    DataTable GPTblNew = GPTbl.Clone();
        //    GPTblNew.Columns.Add(new DataColumn("Markup_Per", typeof(decimal)));
        //    GPTblNew.Columns.Add(new DataColumn("GP_Per", typeof(decimal)));
        //    GPTblNew.Columns.Add(new DataColumn("Expct_MU_Per", typeof(decimal)));
        //    GPTblNew.Columns.Add(new DataColumn("Expct_GP_Per", typeof(decimal)));
        //    GPTblNew.Columns.Add(new DataColumn("New_MU_Price", typeof(decimal)));
        //    GPTblNew.Columns.Add(new DataColumn("New_GP_Price", typeof(decimal)));

        //    int count = 0;
        //    foreach (DataRow row in GPTbl.Rows)
        //    {
        //        if (row["sapd_price_type"].ToString() == "0")
        //        {
        //            GPTblNew.ImportRow(row);
        //        }
        //        else
        //        {
        //            foreach (DataRow rowNew in GPTblNew.Rows)
        //            {
        //                if (row["sapd_pbk_lvl_cd"].ToString() == rowNew["sapd_pbk_lvl_cd"].ToString() && rowNew["sapd_price_type"].ToString() == "0")
        //                {
        //                    count++;
        //                }
        //            }
        //            if (count < 1)
        //            {
        //                GPTblNew.ImportRow(row);
        //            }
        //            count = 0;
        //        }

        //    }
        //    decimal cost = 5.7m;

        //    foreach (DataRow rowNew in GPTblNew.Rows)
        //    {

        //        if (rowNew["sapd_pb_tp_cd"].ToString() == pricebook && rowNew["sapd_pbk_lvl_cd"].ToString() == pricelevel)
        //        {
        //            priValue[rowNew["SAPD_ITM_CD"].ToString()] = Convert.ToDecimal(rowNew["sapd_itm_price"].ToString());
        //        }
        //    }
        //    DataTable dt2 = dt.Clone();
        //    dt2.Columns.Add(new DataColumn("CUR_PRI", typeof(decimal)));
        //    dt2.Columns.Add(new DataColumn("PRI_BK", typeof(string)));
        //    dt2.Columns.Add(new DataColumn("PRI_LVL", typeof(string)));
        //    dt2.Columns.Add(new DataColumn("GP_PER", typeof(decimal)));
        //    dt2.Columns.Add(new DataColumn("MK_PER", typeof(decimal)));
        //    dt2.Columns.Add(new DataColumn("CUR_NWMU", typeof(decimal)));
        //    dt2.Columns.Add(new DataColumn("CUR_NWGP", typeof(decimal)));
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        dt2.ImportRow(row);
        //    }
        //    foreach (DataRow row in dt2.Rows)
        //    {
        //        string searchItm = row["ITEM"].ToString() == "" ? row["ITEM1"].ToString() : row["ITEM"].ToString();
        //        decimal curItmPri = Convert.ToDecimal(priValue.Where(x => x.Key == searchItm).Select(x => x.Value).FirstOrDefault().ToString());
        //        decimal costnw = 0;
        //        row["CUR_PRI"] = curItmPri.ToString();
        //        row["PRI_BK"] = pricebook;
        //        row["PRI_LVL"] = pricelevel;
        //        if (IsNumeric(row["DP_COST"].ToString()))
        //        {
        //            costnw = Convert.ToDecimal(row["DP_COST"].ToString());
        //        }
        //        decimal itemprice = Convert.ToDecimal(row["CUR_PRI"].ToString());
        //        decimal gp_per = 0;
        //        decimal mk_per = 0;
        //        if (itemprice == 0)
        //        {

        //        }
        //        else
        //        {
        //            gp_per = ((itemprice - costnw) / itemprice) * 100;
        //        }
        //        if (costnw == 0)
        //        {

        //        }
        //        else
        //        {
        //            mk_per = ((itemprice - costnw) / costnw) * 100;
        //        }
        //        row["GP_PER"] = gp_per;
        //        row["MK_PER"] = mk_per;

        //        // grdCGGPPer.Text=
        //    }
        //    return dt2;
        //}

        public DataTable CostInqirytblitem(List<string> itmList, string com, bool chkGPAllDoc, string pricebook, string pricelevel, string item, string reccount, string status, bool chkAllPrice)
        {
            //bool chkAllPrice = false;
            _generalDAL = new GeneralDAL();
            DataTable dt = new DataTable();
            DataTable dt1;
            Dictionary<string, decimal> priValue = new Dictionary<string, decimal>();
            decimal curpri = 0.0m;
            if (chkGPAllDoc)
            {
                dt = _generalDAL.GetItmCostDetail(item, com, "ALL", reccount, status);
            }
            else
            {
                dt = _generalDAL.GetItmCostDetail(item, com, "GRN", reccount, status);
            }

            DataTable GPTbl = _generalDAL.SearchPriceForGPCal(item, "A", DateTime.Today, DateTime.Today);

            DataTable GPTblNew = GPTbl.Clone();
            GPTblNew.Columns.Add(new DataColumn("Markup_Per", typeof(decimal)));
            GPTblNew.Columns.Add(new DataColumn("GP_Per", typeof(decimal)));
            GPTblNew.Columns.Add(new DataColumn("Expct_MU_Per", typeof(decimal)));
            GPTblNew.Columns.Add(new DataColumn("Expct_GP_Per", typeof(decimal)));
            GPTblNew.Columns.Add(new DataColumn("New_MU_Price", typeof(decimal)));
            GPTblNew.Columns.Add(new DataColumn("New_GP_Price", typeof(decimal)));

            int count = 0;
            foreach (DataRow row in GPTbl.Rows)
            {
                if (row["sapd_price_type"].ToString() == "0")
                {
                    GPTblNew.ImportRow(row);
                }
                else
                {
                    foreach (DataRow rowNew in GPTblNew.Rows)
                    {
                        if (row["sapd_pbk_lvl_cd"].ToString() == rowNew["sapd_pbk_lvl_cd"].ToString() && rowNew["sapd_price_type"].ToString() == "0")
                        {
                            count++;
                        }
                    }
                    if (count < 1)
                    {
                        GPTblNew.ImportRow(row);
                    }
                    count = 0;
                }

            }
            decimal cost = 5.7m;

            foreach (DataRow rowNew in GPTblNew.Rows)
            {

                if (rowNew["sapd_pb_tp_cd"].ToString() == pricebook && rowNew["sapd_pbk_lvl_cd"].ToString() == pricelevel)
                {
                    priValue[rowNew["SAPD_ITM_CD"].ToString()] = Convert.ToDecimal(rowNew["sapd_itm_price"].ToString());
                    break;
                }
            }
            DataTable dt2 = dt.Clone();
            dt2.Columns.Add(new DataColumn("CUR_PRI", typeof(decimal)));
            dt2.Columns.Add(new DataColumn("PRI_BK", typeof(string)));
            dt2.Columns.Add(new DataColumn("PRI_LVL", typeof(string)));
            dt2.Columns.Add(new DataColumn("GP_PER", typeof(decimal)));
            dt2.Columns.Add(new DataColumn("MK_PER", typeof(decimal)));
            dt2.Columns.Add(new DataColumn("CUR_NWMU", typeof(decimal)));
            dt2.Columns.Add(new DataColumn("CUR_NWGP", typeof(decimal)));
            foreach (DataRow row in dt.Rows)
            {
                dt2.ImportRow(row);
            }

            DataTable dt3 = dt2.Clone();
            foreach (DataRow row in dt2.Rows)
            {
                string searchItm = row["ITEM"].ToString() == "" ? row["ITEM1"].ToString() : row["ITEM"].ToString();
                decimal curItmPri = Convert.ToDecimal(priValue.Where(x => x.Key == searchItm).Select(x => x.Value).FirstOrDefault().ToString());
                //----------------------------------------------
                //var query = GPTblNew.AsEnumerable().Where(x => x.Field<string>("SAPD_ITM_CD") == searchItm);      // Comment by Wimal 07/May/2018 - Stop looping all prices
                //if (query != null && query.Count() > 0)                                                          // Comment by Wimal 07/May/2018 - Stop looping all prices 
                if (curItmPri > 0)
                { 
                //foreach (DataRow row_ in query)                                                                  // Comment by Wimal 07/May/2018 - Stop looping all prices
                //{                                                                                                // Comment by Wimal 07/May/2018 - Stop looping all prices
                // curItmPri = Convert.ToDecimal(row_["sapd_itm_price"]);                                         // Comment by Wimal 07/May/2018 - Stop looping all prices


                        //----------------------------------------------


                        decimal costnw = 0;
                        row["CUR_PRI"] = curItmPri.ToString();
                        row["PRI_BK"] = pricebook;
                        row["PRI_LVL"] = pricelevel;
                        if (IsNumeric(row["DP_COST"].ToString()))
                        {
                            costnw = Convert.ToDecimal(row["DP_COST"].ToString());
                        }
                        decimal itemprice = Convert.ToDecimal(row["CUR_PRI"].ToString());
                        decimal gp_per = 0;
                        decimal mk_per = 0;
                        if (itemprice == 0)
                        {

                        }
                        else
                        {
                            gp_per = ((itemprice - costnw) / itemprice) * 100;
                        }
                        if (costnw == 0)
                        {

                        }
                        else
                        {
                            mk_per = ((itemprice - costnw) / costnw) * 100;
                        }
                        row["GP_PER"] = gp_per;
                        row["MK_PER"] = mk_per;
                        dt3.ImportRow(row);
                        //if (chkAllPrice == false)   // Comment by Wimal 07/May/2018 - Stop looping all prices
                    //{                           // Comment by Wimal 07/May/2018 - Stop looping all prices
                    //    break;                 // Comment by Wimal 07/May/2018 - Stop looping all prices
                    //}                      // Comment by Wimal 07/May/2018 - Stop looping all prices
                  }  
                //} Comment by Wimal 07/May/2018 - Stop looping all prices
                else
                {
                    curItmPri = Convert.ToDecimal(0);


                    //----------------------------------------------


                    decimal costnw = 0;
                    row["CUR_PRI"] = curItmPri.ToString();
                    row["PRI_BK"] = pricebook;
                    row["PRI_LVL"] = pricelevel;
                    if (IsNumeric(row["DP_COST"].ToString()))
                    {
                        costnw = Convert.ToDecimal(row["DP_COST"].ToString());
                    }
                    decimal itemprice = Convert.ToDecimal(row["CUR_PRI"].ToString());
                    decimal gp_per = 0;
                    decimal mk_per = 0;
                    if (itemprice == 0)
                    {

                    }
                    else
                    {
                        gp_per = ((itemprice - costnw) / itemprice) * 100;
                    }
                    if (costnw == 0)
                    {

                    }
                    else
                    {
                        mk_per = ((itemprice - costnw) / costnw) * 100;
                    }
                    row["GP_PER"] = gp_per;
                    row["MK_PER"] = mk_per;
                    dt3.ImportRow(row);
                    //if (chkAllPrice == false)  // Comment by Wimal 07/May/2018 - Stop looping all prices
                    //{                          // Comment by Wimal 07/May/2018 - Stop looping all prices
                    //    break;                 // Comment by Wimal 07/May/2018 - Stop looping all prices
                    //}                          // Comment by Wimal 07/May/2018 - Stop looping all prices
                }
          

                // grdCGGPPer.Text=
                
            }
            //return dt2;
            return dt3;
        }

        public static Boolean IsNumeric(string stringToTest)
        {
            decimal result;
            return decimal.TryParse(stringToTest, out result);
        }
        //Lakshan 18 feb 2017
        public MasterLocationNew GetLocationDataForPdaSend(MasterLocationNew _loc)
        {
            List<MasterLocationNew> _locMstList = new List<MasterLocationNew>();
            _generalDAL = new GeneralDAL();
            _locMstList = _generalDAL.GetMasterLocationDataNew(_loc);
            MasterLocationNew _locData = _locMstList.Where(c => c.Ml_wh_cd != null && c.Ml_act == 1).FirstOrDefault();
            return _locData;
        }
        //Lakshan 18 feb 2017
        public MasterLocationNew GET_MST_LOC_DATA(MasterLocationNew _loc)
        {
            List<MasterLocationNew> _locMstList = new List<MasterLocationNew>();
            _generalDAL = new GeneralDAL();
            _locMstList = _generalDAL.GetMasterLocationDataNew(_loc);
            MasterLocationNew _locData = _locMstList[0];
            return _locData;
        }
        //Lakshan 18 feb 2017
        public MasterLocationPriorityHierarchy GET_MST_LOC_INFO_DATA(string _mliLocCd, string _mliCd)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GET_MST_LOC_INFO_DATA(_mliLocCd, _mliCd);
        }
        public string userEmailList(string reqno)
        {
            _generalDAL = new GeneralDAL();
            DataTable email = _generalDAL.userEmailList(reqno);
            string emailList = string.Empty;
            int i = 1;
            foreach (DataRow row in email.Rows)
            {
                if (row["SE_EMAIL"] != DBNull.Value)
                    emailList += row["SE_EMAIL"].ToString() + ((i == email.Rows.Count) ? "" : ",");
                i++;
            }
            return emailList;
        }
        public string getFinalApprovalLevel(string appcd, string company, string pc, string cate)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getApproveLevelForFinApprovalNew(appcd, company, pc, cate);
        }

        public Int32 saveLoadingBay(List<LoadingBay> _loadingBay)
        {
            Int32 _effect = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                if (_loadingBay != null)
                {
                    int sline = 1;
                    foreach (LoadingBay _loadBa in _loadingBay)
                    {
                        _effect = _generalDAL.saveLoadingBay(_loadBa);
                        sline++;
                    }
                }

                _generalDAL.TransactionCommit();
                _effect = 1;
            }
            catch (Exception err)
            {
                _effect = -1;
                // _docNo = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();
            }
            return _effect;
        }

        public Int32 saveDocLoadingBay(List<ReptPickHeader> _loadingBay)
        {
            Int32 _effect = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                if (_loadingBay != null)
                {
                    int sline = 1;
                    foreach (ReptPickHeader _loadBa in _loadingBay)
                    {
                        _effect = _generalDAL.saveDocLoadingBay(_loadBa);
                        sline++;
                    }
                }

                _generalDAL.TransactionCommit();
                _effect = 1;
            }
            catch (Exception err)
            {
                _effect = -1;
                // _docNo = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();
            }
            return _effect;
        }
        //Isuru 2017/03/24
        public DataTable LoadSunPrefixFacilityData(gnr_acc_sun_ledger FacilityData)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.LoadSunPrefixFacilityData(FacilityData);
        }

        //Isuru 2017/03/25
        public List<sar_tp> GetMasterPrefixDatas(sar_tp _loc)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetMasterPrefixData(_loc);
        }

        //Isuru 2017/03/25
        public sar_tp GetMasterPrefixData(sar_tp _loc)
        {
            _generalDAL = new GeneralDAL();
            List<sar_tp> _locations = GetMasterPrefixDatas(_loc);
            if (_locations.Count > 0)
            {
                return _locations.FirstOrDefault();
            }
            return null;
        }
        //Udaya 27/03/2017
        public DataTable ViewCommissionsDetails(DateTime FromData, DateTime ToDate, string circularCode, string userid, string com)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.ViewCommissionsDetails(FromData, ToDate, circularCode, userid, com);
        }
        public List<MasterItemModel> GetItemModelNew(string _code = null)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetItemModelNew(_code);
        }
        public MasterItem GetItemMasterNew(string _item)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetItemMasterNew(_item);
        }
        //Udaya 25/04/2017
        public DataTable SearchProjectCode(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime FromDate, DateTime ToDate, string code)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.SearchProjectCode(_initialSearchParams, _searchCatergory, _searchText, FromDate, ToDate, code);
        }

        //Isuru 2017/05/03
        public Int32 uploadlocmasterdet(List<MasterLocationNew> _locmasterdet)
        {
            _generalDAL = new GeneralDAL();
            Int32 _effect = 0;
            try
            {

                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                foreach (MasterLocationNew _list in _locmasterdet)
                {
                    _effect = _generalDAL.uploadlocmasterdet(_list);
                }

                _generalDAL.TransactionCommit();
                _generalDAL.ConnectionClose();

            }
            catch (Exception ex)
            {
                _generalDAL.TransactionRollback();
                _generalDAL.ConnectionClose();
                _effect = -1;
            }


            return _effect;
        }


        //Isuru 2017/05/03
        public Int32 Updatelogdetails(List<MasterLocationNew> _locmasterdet)
        {
            _generalDAL = new GeneralDAL();
            Int32 _effect = 0;
            try
            {

                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                foreach (MasterLocationNew _list in _locmasterdet)
                {
                    _effect = _generalDAL.Updatelogdetails(_list);
                }

                _generalDAL.TransactionCommit();
                _generalDAL.ConnectionClose();

            }
            catch (Exception ex)
            {
                _generalDAL.TransactionRollback();
                _generalDAL.ConnectionClose();
                _effect = -1;
            }


            return _effect;
        }
        //Tharanga 2017/05/13
        public Int32 SaveVehicalTransportDefinitionNew(List<MasterProfitCenter> pcs, DateTime from, DateTime to, string satlesType, decimal regVal, string cre, string insCom, string insPol, int term, bool isReq, string _tp, string _book, string _level, string _serial, string _promotion, string _circuler, decimal _isRate, decimal _fromValue, decimal _toValue, string _type, string _circular)
        {
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            InventoryDAL inv = new InventoryDAL();
            inv.ConnectionOpen();
            Int32 result = _generalDAL.SaveVehicalTransportDefinitionNew(pcs, from, to, satlesType, regVal, cre, insCom, insPol, term, isReq, _tp, _book, _level, _serial, _promotion, _circuler, _isRate, _fromValue, _toValue, _type, _circular);
            inv.ConnectionClose();
            _generalDAL.ConnectionClose();
            return result;
        }
        //Udaya 30/05/2017
        public Int32 saveItemWarrenty(List<mst_itm_pc_warr> _lstpcwara, List<MasterItemWarrantyPeriod> _lstitmWrd, out string _err)
        {
            Int32 _effect = 0;
            _err = string.Empty;
            //if (_lstpcwara != null)
            //{
                _generalDAL = new GeneralDAL();
                try
                {
                    _generalDAL.ConnectionOpen();
                    _generalDAL.BeginTransaction();
                    
                    List<MasterItem> _itmList = new List<MasterItem>();
                    if (_lstpcwara != null)
                    {
                        //foreach (var item in _lstpcwara)
                        //{
                        //    var _tmpItm = _itmList.Where(c => c.Mi_cd == item.Pc_item_code).FirstOrDefault();
                        //    if (_tmpItm == null)
                        //    {
                        //        _itmList.Add(new MasterItem() { Mi_cd = item.Pc_item_code });
                        //    }
                        //}
                        //foreach (var item in _itmList)
                        //{
                        //    var _saveList = _lstpcwara.Where(c => c.Pc_item_code == item.Mi_cd).ToList();
                        //    _effect = _generalDAL.SavePcWarranty_log(item.Mi_cd);
                        //    _effect = _generalDAL.SAVE_SAR_PC_WARA(_saveList);
                        //}
                        foreach (var item in _lstpcwara)
                        {
                            _effect = _generalDAL.SaveDeletePcWarranty_log(item.Pc_item_code, item.Pc_item_stus, item.Pc_code, item.Pc_com);
                        }
                        _effect = _generalDAL.SAVE_SAR_PC_WARA(_lstpcwara);
                    }
                    else if (_lstitmWrd != null)
                    {
                        //foreach (var item in _lstitmWrd)
                        //{
                        //    var _tmpItm = _itmList.Where(c => c.Mi_cd == item.Mwp_itm_cd).FirstOrDefault();
                        //    if (_tmpItm == null)
                        //    {
                        //        _itmList.Add(new MasterItem() { Mi_cd = item.Mwp_itm_cd, Mi_itm_stus = item.Mwp_itm_stus, Mi_uom_warrperiodmain = item.Mwp_warr_tp });
                        //    }
                        //}
                        //foreach (var item in _itmList)
                        //{
                        //    var _saveList = _lstitmWrd.Where(c => c.Mwp_itm_cd == item.Mi_cd).ToList();
                        //    _effect = _generalDAL.SaveANDDeleteStatusWarranty_log(item.Mi_cd, item.Mi_itm_stus, item.Mi_uom_warrperiodmain);
                        //    _effect = _generalDAL.SaveStatusWarranty(_saveList);
                        //}
                        foreach (var item in _lstitmWrd)
                        {
                            if (item.Mwp_rmk.Length > 200)
                            {
                            _err = "Warrenty remark lenth exceed item :" + item.Mwp_itm_cd + ". Max lenth is 200.";
                                _generalDAL.TransactionRollback();
                                _generalDAL.ConnectionClose();
                                return -1;
                            }
                            _effect = _generalDAL.SaveANDDeleteStatusWarranty_log(item.Mwp_itm_cd, item.Mwp_itm_stus, item.Mwp_warr_tp);
                        }
                        _effect = _generalDAL.SaveStatusWarranty(_lstitmWrd);
                    }
                    _generalDAL.TransactionCommit();
                    _generalDAL.ConnectionClose();
                }
                catch (Exception ex)
                {
                    _generalDAL.TransactionRollback();
                    _generalDAL.ConnectionClose();
                    _err = ex.Message;
                    _effect = -1;
                }
            //}
            return _effect;
        }

        //Add by Lakshan 05 Jun 2017
        public List<mst_itm_tax_structure_det> GetItemTaxStructureWeb(string _code)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetItemTaxStructureWeb(_code);
        }
        //Add by Dilshan 26 sep 2017
        public List<MgrCreation> GetMgrlocationWeb(string _code)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetMgrlocationWeb(_code);
        }
        //Add by Lakshan 05 Jun 2017
        public MST_TAX_CD GET_MST_TAX_CD_DATA(string _taxRtCode)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GET_MST_TAX_CD_DATA(_taxRtCode);
        }
        //Tharanga 2017/07/04
        public Int32 Save_MST_PC_INFO_LOG_Excle_upload(List<MasterSalesPriorityHierarchyLog> _pcInfoHeaders, List<PcList> _PcList, out string _msg)
        {
            Int32 effect = 0;
            _generalDAL = new GeneralDAL();
            _saleDAL = new SalesDAL();
            try
            {
                using (TransactionScope _tr = new TransactionScope())
                {
                    _generalDAL.ConnectionOpen();
                    _saleDAL.ConnectionOpen();
                    foreach (MasterSalesPriorityHierarchyLog pc_info in _pcInfoHeaders)
                    {
                        foreach (var code in _PcList)
                      //  foreach (string code in code_and_value.Keys)
                        {
                            MasterSalesPriorityHierarchy _one = new MasterSalesPriorityHierarchy();
                           //
                            pc_info.Mpil_cd = code.Type;
                            pc_info.Mpil_val = code.Type_value;
                            if (code.Type == "PC") 
                                pc_info.Mpil_val = pc_info.Mpil_pc_cd;
                            DateTime _now = DateTime.Now.Date;
                            DateTime _from = pc_info.Mpil_frm_dt;
                            DateTime _to = pc_info.Mpil_to_dt;
                            if (_from.Date <= _now.Date) 
                                pc_info.Mpil_isupdt = true; 
                                else 
                                pc_info.Mpil_isupdt = false;
                            if (code.Type == "CHNL" && _from.Date <= _now.Date) 
                                _saleDAL.UpdateProfitCenterChannel(pc_info.Mpil_com_cd, pc_info.Mpil_pc_cd, pc_info.Mpil_val);
                            if (_from.Date <= _now.Date)
                                _generalDAL.UpdateInvoiceStructure(pc_info.Mpil_com_cd, pc_info.Mpil_pc_cd, _from, _to, code.Type, pc_info.Mpil_val);
                            effect = _generalDAL.Save_MST_PC_INFO_log(pc_info);
                            if (_from.Date <= _now.Date)
                            {
                                _one.Mpi_act = pc_info.Mpil_act;
                                _one.Mpi_cd = pc_info.Mpil_cd;
                                _one.Mpi_com_cd = pc_info.Mpil_com_cd;
                                _one.Mpi_pc_cd = pc_info.Mpil_pc_cd;
                                _one.Mpi_tp = pc_info.Mpil_tp;
                                _one.Mpi_val = pc_info.Mpil_val;
                                _generalDAL.Save_MST_PC_INFO(_one);
                            }


                            // Nadeeka 21-05-2015
                            if (code.Type == "SCHNL" && !String.IsNullOrEmpty(pc_info.Mpil_val))
                            {
                                if (pc_info.Mpil_val != "N/A")
                                {
                                    _saleDAL.AssignChannelAccestoPC(pc_info.Mpil_val, pc_info.Mpil_pc_cd, pc_info.Mpil_com_cd, pc_info.Mpil_mod_by);
                                }
                            }

                        }
                    }
                    _generalDAL.ConnectionClose();
                    effect = 1;
                    _tr.Complete();
                }
                _msg = string.Empty;
                return effect;
            }
            catch (Exception ex)
            {
                _msg = ex.Message;
                return -1;
            }
        }
    
    //Tharanga 2017/07/04
        public Int32 save_mst_loc_info_log_exle(List<MasterLocationPriorityHierarchyLog> _locInfoHeaders, List<PcList> _PcList, out string _msg)
        {
            Int32 effect = 0;
            _generalDAL = new GeneralDAL();
            _saleDAL = new SalesDAL();
            try
            {
                using (TransactionScope _tr = new TransactionScope())
                {
                    _generalDAL.ConnectionOpen();
                    _saleDAL.ConnectionOpen();
                    foreach (MasterLocationPriorityHierarchyLog pc_info in _locInfoHeaders)
                    {
                        foreach (var code in _PcList)
                        //foreach (string code in code_and_value.Keys)
                        {
                            DateTime _now = DateTime.Now.Date;
                            DateTime _from = pc_info.Mlil_frm_dt;
                            DateTime _to = pc_info.Mlil_to_dt;
                            pc_info.Mlil_cd = code.Type;
                            pc_info.Mlil_val = code.Type_value;
                            if (code.Type == "LOC") pc_info.Mlil_val = pc_info.Mlil_loc_cd;
                            if (_from.Date <= _now.Date) pc_info.Mlil_isupdt = true; else pc_info.Mlil_isupdt = false;
                            effect = _generalDAL.save_mst_loc_info_log(pc_info);
                            MasterLocationPriorityHierarchy _one = new MasterLocationPriorityHierarchy();
                            if (_from.Date <= _now.Date)
                            {
                                _one.Mli_act = pc_info.Mlil_act;
                                _one.Mli_cd = pc_info.Mlil_cd;
                                _one.Mli_com_cd = pc_info.Mlil_com_cd;
                                _one.Mli_loc_cd = pc_info.Mlil_loc_cd;
                                _one.Mli_tp = pc_info.Mlil_tp;
                                _one.Mli_val = pc_info.Mlil_val;
                                _generalDAL.save_mst_loc_info(_one);
                            }
                        }
                    }
                    _generalDAL.ConnectionClose();
                    _saleDAL.ConnectionClose();
                    effect = 1;
                    _tr.Complete();
                }
                _msg = string.Empty;
                return effect;
            }
            catch (Exception ex)
            {
                _msg = ex.Message;
                return -1;
            }

        }
        //Dilshan 2017-09-26
        public int SaveMangerDetails(List<MgrCreation> _lst, out string _errer)
        {
            int effect = 0;
            _errer = "";
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            try
            {
                foreach (var list in _lst)
                {
                    effect = _generalDAL.SaveMangerDetails(list);
                }
                _generalDAL.TransactionCommit();
                //_salesDAL.TransactionCommit();
                _generalDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                _errer = ex.Message;
                _generalDAL.TransactionRollback();
                //_salesDAL.TransactionRollback();
                effect = -1;
            }
            return effect;
        }

        

        //Dilshan 2017-09-28
        public int SaveCirDetails(List<hpr_disr_val_ref> _lst, int status, out string _errer)
        {
            int effect = 0;
            _errer = "";
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            try
            {
                foreach (var list in _lst)
                {
                    effect = _generalDAL.SaveCirDetails(list, status);
                }
                _generalDAL.TransactionCommit();
                //_salesDAL.TransactionCommit();
                _generalDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                _errer = ex.Message;
                _generalDAL.TransactionRollback();
                //_salesDAL.TransactionRollback();
                effect = -1;
            }
            return effect;
        }
        //Dilshan 2017-09-28
        public int SaveArrearsDetails(List<hpr_ars_rls_sch> _lst, out string _errer)
        {
            int effect = 0;
            _errer = "";
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            try
            {
                foreach (var list in _lst)
                {
                    effect = _generalDAL.SaveArrearsDetails(list);
                }
                _generalDAL.TransactionCommit();
                //_salesDAL.TransactionCommit();
                _generalDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                _errer = ex.Message;
                _generalDAL.TransactionRollback();
                //_salesDAL.TransactionRollback();
                effect = -1;
            }
            return effect;
        }
        //Dilshan 2017-09-28
        public int SaveBonusDetails(List<BonusDefinition> _lst, out string _errer)
        {
            int effect = 0;
            _errer = "";
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            try
            {
                foreach (var list in _lst)
                {
                    effect = _generalDAL.SaveBonusDetails(list);
                }
                _generalDAL.TransactionCommit();
                //_salesDAL.TransactionCommit();
                _generalDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                _errer = ex.Message;
                _generalDAL.TransactionRollback();
                //_salesDAL.TransactionRollback();
                effect = -1;
            }
            return effect;
        }
        //Dilshan 2017-09-28
        public int SaveLocDetails(List<hpr_disr_pc_defn> _lst, out string _errer)
        {
            int effect = 0;
            _errer = "";
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            try
            {
                foreach (var list in _lst)
                {
                    effect = _generalDAL.SaveLocDetails(list);
                }
                _generalDAL.TransactionCommit();
                //_salesDAL.TransactionCommit();
                _generalDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                _errer = ex.Message;
                _generalDAL.TransactionRollback();
                //_salesDAL.TransactionRollback();
                effect = -1;
            }
            return effect;
        }

        public List<MgrCreation> GetMgrextData(String com, string mgr)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetMgrextData(com, mgr);
        }

        //By Akila 2017/09/29
        public DataTable GetBankDetailsByBinCode(string _binCode)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetBankDetailsByBinCode(_binCode);
        }
        public List<hpr_disr_val_ref> GetCircularData(String com, string mgr, string cat)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetCircularData(com, mgr, cat);
        }
        public List<hpr_disr_val_ref> CheckCircularData(String com, string mgr, string cat)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.CheckCircularData(com, mgr, cat);
        }
        public List<hpr_disr_pc_defn> GetCirLocData(String com, string mgr)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetCirLocData(com, mgr);
        }
        public List<BonusDefinition> GetCircularcbdData(String com, string mgr)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetCircularcbdData(com, mgr);
        }
        public List<hpr_ars_rls_sch> GetSchemeData(string com, string channel, string location)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetSchemeData(com, channel, location);
        }
        public Int32 GetSchemeTerm(string _type, string _code, string _term)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetSchemeTerm(_type, _code, _term);
        }
        public DataTable getItemInSerial_Rev(string reqNo)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getItemInSerial_Rev(reqNo);
        }
        public DataTable GetItemInProduction(string docNo, string comCode, string chk)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetItemInProduction(docNo, comCode, chk);
        }

        //By Akila 2017/09/29
        public List<PcAllowBanks> GetPcAllowBanks(string _comCode, string _profitCenter, string _moduleName)
        {
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();

            return _generalDAL.GetPcAllowBanks(_comCode, _profitCenter, _moduleName);

        }

        //Akila 2017/10/26
        public DataTable SearchSalesTypes(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.SearchSalesTypes(_initialSearchParams, _searchCatergory, _searchText);
        }

        public DataTable GetAllCompanies() // Added by Chathura on 09-nov-2017
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetAllCompanies();
        }
        //Add by lakshan 28Nov2017
        public MasterItemBrand GET_ITM_BRAND_DATA(string _cd)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GET_ITM_BRAND_DATA(_cd);
        }
        //Add by lakshan 28Nov2017
        public GradeMaster GET_MST_GRADE_DATA(string _com, string _chnl, string _cd)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GET_MST_GRADE_DATA(_com, _chnl, _cd);
        }

        //Akila 2017/12/11
        public DataTable GetSysParaDetails(string _company, string _chnl, string _type)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetSysParaDetails(_company, _chnl, _type);
        }

        //akila 2017/12/15
        public string UpdateRccReminderDetails(RCC _rcc, out string _message)
        {
            _message = string.Empty;
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            try
            {
                int _effects = _generalDAL.UpdateRccReminderDetails(_rcc);
                if (_effects < 1)
                {
                    throw new Exception("Couldn't save RCC reminder details");
                }

                _generalDAL.TransactionCommit();
                _generalDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                _message = ex.Message;
                _generalDAL.TransactionRollback();
            }
            return _message;
        }

        //Tharindu 2017-12-12
        public DataTable GetBrand(string _brnd)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetBrand(_brnd);
        }

        //Tharindu 2017-12-12
        public DataTable GetItemStatus(string _itmstatus)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetItemStatus(_itmstatus);
        }

        //akila 2017/12/28
        public string UpdateRccWithRequest(RCC _rcc)
        {
            string _message = string.Empty;
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            try
            {
                Int16 _effects = _generalDAL.UpdateRccWithRequest(_rcc);
                if (_effects < 1)
                {
                    throw new Exception("Couldn't save RCC details");
                }

                _generalDAL.TransactionCommit();
                _generalDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                _message = ex.Message;
                _generalDAL.TransactionRollback();
            }
            return _message;
        }

        //Add by lakshan 28Dec2017
        public BusEntityItem GetBuninessEntityItemBySupplierItm(string _comp, string _code, string _itm)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetBuninessEntityItemBySupplierItm(_comp, _code, _itm);
        }
        public DataTable GET_REF_AGE_SLOT(string _com)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GET_REF_AGE_SLOT(_com);
        }

        //Dulaj 2018-Feb-08
        public Int32 SaveUpdateWarrentyAmend(List<SerialMasterLog> objs, List<InventoryWarrantyDetail> objsWarrenty, out string err)
        {
            string error = "";
            Int32 _effect = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                if (objs != null)
                {
                    foreach (SerialMasterLog i in objs)
                    {
                        _effect = _effect + _generalDAL.SaveSerialMasterLog(i);
                    }
                }
                if (objsWarrenty != null)
                {
                    foreach (InventoryWarrantyDetail i in objsWarrenty)
                    {
                        _effect = _effect + _generalDAL.UpdateWarrantyMasterAmend(i);
                    }
                }

                _generalDAL.TransactionCommit();
            }
            catch (Exception ex)
            {
                error = ex.Message;
                _effect = -1;
                _generalDAL.TransactionRollback();
            }
            err = error;
            return _effect;
        }
        public List<ApprovalReqCategory> getAppReqCateList_New(string _Type, string _main)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.getAppReqCateList_New(_Type, _main);
        }
        //Dulaj 2018-MAr-10
        public DataTable GetProductCondtionParameters(string _com, string para_cd, string _tp, string to_stus)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetProductCondtionParameters(_com, para_cd, _tp, to_stus);
        }
        //Dulaj 2018-MAr-10
        public DataTable GetProductCondtionParametersByCd(string _com, string para_cd, string _cd, string to_stus)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetProductCondtionParametersByCd(_com, para_cd, _cd, to_stus);
        }
        //Dulaj 2018-Mar-19
        public DataTable GetBatchStatus(string _com, string _loc, string _docType, string _direction, DateTime _frm, DateTime _to, Int16 _dateType, Int16 _isFinished, Int16 _wip, string doctypePending,int sp)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetBatchStatus(_com, _loc, _docType, _direction, _frm, _to, _dateType, _isFinished, _wip,doctypePending,sp);
        }
        //Dulaj 2018-Mar-20
        public DataTable GetItemStatusByUserSeq(string _tus_usrseq_no)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetItemStatusByUserSeq(_tus_usrseq_no);
        }
        //Nuwan 2018.03.27
        public DataTable getDetailsForDocGenarate(string pc, string type)
        {
            _inventoryRepDAL = new ReptCommonDAL();
            return _inventoryRepDAL.getDetailsForDocGenarate(pc, type);
        }
        //Nuwan 2018.03.29
        public Int32 updateTempAndAnalDetails(string invoiceno, decimal cost, string pc, string com, out string error)
        {
            error = "";
            int eff = 0;
            try
            {
                _inventoryRepDAL = new ReptCommonDAL();
                eff = _inventoryRepDAL.updateTempAndAnalDetails(invoiceno, cost, pc, com);
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }
            return eff;
        }
        //dilshan
        public DataTable GetItemAvailability(string _item, string _com)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetItemAvailability(_item, _com);
        }
        //Dulaj 2018-Apr-10
        public DataTable GetUserDefineTemplate(string template, string user)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetUserDefineTemplate(template, user);
        }
        //Dulaj 2018-Apr-11
        public Int32 SaveUserProfileTemplate(string _templateName, string _codes, string _values, string _userId, string _dectription, string _key, string sessionId)
        {
            Int32 _effect = 0;
            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                _effect = _generalDAL.SaveUserProfileTemplate(_templateName, _codes, _values, _userId, _dectription, _key, sessionId);
                if (_effect > 0)
                {
                    _generalDAL.TransactionCommit();
                    _generalDAL.ConnectionClose();
                    return _effect;
                }
                else
                {
                    _generalDAL.TransactionRollback();
                    _generalDAL.ConnectionClose();
                    return _effect;
                }
            }
            catch (Exception e)
            {
                _generalDAL.TransactionRollback();
                _generalDAL.ConnectionClose();
                return _effect;

            }
        }

        //Dulaj 2018-apr-12

        public Int32 CheckTemplateName(string _tempName)
        {
            Int32 _effect = 0;
            _generalDAL = new GeneralDAL();
            _generalDAL.ConnectionOpen();
            _effect = _generalDAL.CheckTemplateName(_tempName);
            _generalDAL.ConnectionClose();
            return _effect;
        }
        //Pasindu 2018/05/31
        public DataTable GetItemDetails(string p_location, string p_itemcode, string p_serial, string p_fromdt, string p_todt, string category01, string category02, string com)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetItemDetails(p_location, p_itemcode, p_serial, p_fromdt, p_todt, category01, category02, com);
        }

        //Pasindu 2018/05/31
        public DataTable GetItemAvaialableLocation(string p_serial)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetItemAvaialableLocation(p_serial);
        }
        //Dulaj 2018/Jul/04
        public DataTable GetRestrictedMrnLoc(string p_location, string p_itemcode, string p_todt)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetRestrictedMrnLoc(p_location, p_itemcode, p_todt);
        }
        //Dulaj 2018/Aug/16
        public DataTable CostInqirytblitemSerial(List<string> itmList, string com, bool chkGPAllDoc, string pricebook, string pricelevel, string item, string reccount, string status, bool chkAllPrice, string serial)
         {
             //bool chkAllPrice = false;
             _generalDAL = new GeneralDAL();
             DataTable dt = new DataTable();
             DataTable dt1;
             Dictionary<string, decimal> priValue = new Dictionary<string, decimal>();
             decimal curpri = 0.0m;
             if (chkGPAllDoc)
             {
                dt = _generalDAL.GetItmCostDetailSerial(item, com, "ALL", reccount, status, serial);
             }
             else
             {
                 dt = _generalDAL.GetItmCostDetail(item, com, "GRN", reccount, status);
             }

             DataTable GPTbl = _generalDAL.SearchPriceForGPCal(item, "A", DateTime.Today, DateTime.Today);

             DataTable GPTblNew = GPTbl.Clone();
             GPTblNew.Columns.Add(new DataColumn("Markup_Per", typeof(decimal)));
             GPTblNew.Columns.Add(new DataColumn("GP_Per", typeof(decimal)));
             GPTblNew.Columns.Add(new DataColumn("Expct_MU_Per", typeof(decimal)));
             GPTblNew.Columns.Add(new DataColumn("Expct_GP_Per", typeof(decimal)));
             GPTblNew.Columns.Add(new DataColumn("New_MU_Price", typeof(decimal)));
             GPTblNew.Columns.Add(new DataColumn("New_GP_Price", typeof(decimal)));

             int count = 0;
             foreach (DataRow row in GPTbl.Rows)
             {
                 if (row["sapd_price_type"].ToString() == "0")
                 {
                     GPTblNew.ImportRow(row);
                 }
                 else
                 {
                     foreach (DataRow rowNew in GPTblNew.Rows)
                     {
                         if (row["sapd_pbk_lvl_cd"].ToString() == rowNew["sapd_pbk_lvl_cd"].ToString() && rowNew["sapd_price_type"].ToString() == "0")
                         {
                             count++;
                         }
                     }
                     if (count < 1)
                     {
                         GPTblNew.ImportRow(row);
                     }
                     count = 0;
                 }

             }
             decimal cost = 5.7m;

             foreach (DataRow rowNew in GPTblNew.Rows)
             {

                 if (rowNew["sapd_pb_tp_cd"].ToString() == pricebook && rowNew["sapd_pbk_lvl_cd"].ToString() == pricelevel)
                 {
                     priValue[rowNew["SAPD_ITM_CD"].ToString()] = Convert.ToDecimal(rowNew["sapd_itm_price"].ToString());
                     break;
                 }
             }
             DataTable dt2 = dt.Clone();
             dt2.Columns.Add(new DataColumn("CUR_PRI", typeof(decimal)));
             dt2.Columns.Add(new DataColumn("PRI_BK", typeof(string)));
             dt2.Columns.Add(new DataColumn("PRI_LVL", typeof(string)));
             dt2.Columns.Add(new DataColumn("GP_PER", typeof(decimal)));
             dt2.Columns.Add(new DataColumn("MK_PER", typeof(decimal)));
             dt2.Columns.Add(new DataColumn("CUR_NWMU", typeof(decimal)));
             dt2.Columns.Add(new DataColumn("CUR_NWGP", typeof(decimal)));
             foreach (DataRow row in dt.Rows)
             {
                 dt2.ImportRow(row);
             }

             DataTable dt3 = dt2.Clone();
             foreach (DataRow row in dt2.Rows)
             {
                 string searchItm = row["ITEM"].ToString() == "" ? row["ITEM1"].ToString() : row["ITEM"].ToString();
                 decimal curItmPri = Convert.ToDecimal(priValue.Where(x => x.Key == searchItm).Select(x => x.Value).FirstOrDefault().ToString());
                 //----------------------------------------------
                 //var query = GPTblNew.AsEnumerable().Where(x => x.Field<string>("SAPD_ITM_CD") == searchItm);      // Comment by Wimal 07/May/2018 - Stop looping all prices
                 //if (query != null && query.Count() > 0)                                                          // Comment by Wimal 07/May/2018 - Stop looping all prices 
                 if (curItmPri > 0)
                 {
                     //foreach (DataRow row_ in query)                                                                  // Comment by Wimal 07/May/2018 - Stop looping all prices
                     //{                                                                                                // Comment by Wimal 07/May/2018 - Stop looping all prices
                     // curItmPri = Convert.ToDecimal(row_["sapd_itm_price"]);                                         // Comment by Wimal 07/May/2018 - Stop looping all prices


                     //----------------------------------------------


                     decimal costnw = 0;
                     row["CUR_PRI"] = curItmPri.ToString();
                     row["PRI_BK"] = pricebook;
                     row["PRI_LVL"] = pricelevel;
                     if (IsNumeric(row["DP_COST"].ToString()))
                     {
                         costnw = Convert.ToDecimal(row["DP_COST"].ToString());
                     }
                     decimal itemprice = Convert.ToDecimal(row["CUR_PRI"].ToString());
                     decimal gp_per = 0;
                     decimal mk_per = 0;
                     if (itemprice == 0)
                     {

                     }
                     else
                     {
                         gp_per = ((itemprice - costnw) / itemprice) * 100;
                     }
                     if (costnw == 0)
                     {

                     }
                     else
                     {
                         mk_per = ((itemprice - costnw) / costnw) * 100;
                     }
                     row["GP_PER"] = gp_per;
                     row["MK_PER"] = mk_per;
                     dt3.ImportRow(row);
                     //if (chkAllPrice == false)   // Comment by Wimal 07/May/2018 - Stop looping all prices
                     //{                           // Comment by Wimal 07/May/2018 - Stop looping all prices
                     //    break;                 // Comment by Wimal 07/May/2018 - Stop looping all prices
                     //}                      // Comment by Wimal 07/May/2018 - Stop looping all prices
                 }
                 //} Comment by Wimal 07/May/2018 - Stop looping all prices
                 else
                 {
                     curItmPri = Convert.ToDecimal(0);


                     //----------------------------------------------


                     decimal costnw = 0;
                     row["CUR_PRI"] = curItmPri.ToString();
                     row["PRI_BK"] = pricebook;
                     row["PRI_LVL"] = pricelevel;
                     if (IsNumeric(row["DP_COST"].ToString()))
                     {
                         costnw = Convert.ToDecimal(row["DP_COST"].ToString());
                     }
                     decimal itemprice = Convert.ToDecimal(row["CUR_PRI"].ToString());
                     decimal gp_per = 0;
                     decimal mk_per = 0;
                     if (itemprice == 0)
                     {

                     }
                     else
                     {
                         gp_per = ((itemprice - costnw) / itemprice) * 100;
                     }
                     if (costnw == 0)
                     {

                     }
                     else
                     {
                         mk_per = ((itemprice - costnw) / costnw) * 100;
                     }
                     row["GP_PER"] = gp_per;
                     row["MK_PER"] = mk_per;
                     dt3.ImportRow(row);
                     //if (chkAllPrice == false)  // Comment by Wimal 07/May/2018 - Stop looping all prices
                     //{                          // Comment by Wimal 07/May/2018 - Stop looping all prices
                     //    break;                 // Comment by Wimal 07/May/2018 - Stop looping all prices
                     //}                          // Comment by Wimal 07/May/2018 - Stop looping all prices
                 }


                 // grdCGGPPer.Text=

             }
             //return dt2;
             return dt3;
         }
         public Int32 save_busentity_newcom(string p_com, string p_cust_cd)
         {
             Int32 _effect = 0;
             try
             {
                 _generalDAL = new GeneralDAL();
                 _generalDAL.ConnectionOpen();
                 _generalDAL.BeginTransaction();
                 _effect = _generalDAL.save_busentity_newcom(p_com, p_cust_cd);
                 if (_effect > 0)
                 {
                     _generalDAL.TransactionCommit();
                     _generalDAL.ConnectionClose();
                     return _effect;
                 }
                 else
                 {
                     _generalDAL.TransactionRollback();
                     _generalDAL.ConnectionClose();
                     return _effect;
                 }
             }
             catch (Exception e)
             {
                 _generalDAL.TransactionRollback();
                 _generalDAL.ConnectionClose();
                 return _effect;

             }
         }

         //Dulaj 2018/Oct/17
         public DataTable GetBinLocGRN(string com, string loc, string binCd, string decs)
         {
             DataTable _dtResults = new DataTable();
             _generalDAL = new GeneralDAL();
             _dtResults = _generalDAL.GetBinLocGRN(com, loc, binCd, "");
             return _dtResults;
         }
         public DataTable LoadItemKitComponents_ACTIVE(string _itm)
         {
             _generalDAL = new GeneralDAL();
             return _generalDAL.LoadItemKitComponents_ACTIVE(_itm);
         }

        public List<MasterItem> GetItemFromModel(string _Model)
        {// Wimal 16/06/2018
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetItemFromModel(_Model);
        }

        //Udesh 31-Oct-2018
        public OtpAuthentication OTPGenerator(OtpAuthentication _otpAuth)
        {
            _generalDAL = new GeneralDAL();
            int OTPLength = 7;
            string NewCharacters = "";

            //This one tells you which characters are allowed in this new password
            string allowedChars = "";
            //Here Specify your OTP Characters
            allowedChars = "1,2,3,4,5,6,7,8,9,0";
            allowedChars += "A,B,N,S,P,L,C";        
            allowedChars += "~,!,@,#,$,%,^,&,*,+,?";



            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);

            string IDString = "";
            string temp = "";

            //utilize the "random" class
            Random rand = new Random();


            for (int i = 0; i < OTPLength; i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                IDString += temp;
            }
            NewCharacters = IDString;

            //Here Random class used for Random character of User ID
            Random rng = new Random();
            char[] userIDArray = _otpAuth.SDK_CRE_BY.ToCharArray();
            string uniqueCustomerIdentity = userIDArray[rng.Next(0, userIDArray.Length)].ToString();

            #region Createing More Secure OTP Password by Using MD5 algorithm

            string oneTimePassword = string.Empty;
            string strParsedReqId = string.Empty;

            DateTime dateTime = DateTime.Now;
            string _strParseDate = dateTime.Day.ToString();
            _strParseDate = _strParseDate + dateTime.Month.ToString();
            _strParseDate = _strParseDate + dateTime.Year.ToString();
            _strParseDate = _strParseDate + dateTime.Hour.ToString();
            _strParseDate = _strParseDate + dateTime.Minute.ToString();
            _strParseDate = _strParseDate + dateTime.Second.ToString();
            _strParseDate = _strParseDate + dateTime.Millisecond.ToString();
            string _strParsedReqNo = _strParseDate + uniqueCustomerIdentity;


            _strParsedReqNo = NewCharacters + _strParsedReqNo;
            using (MD5 md5 = MD5.Create())
            {
                //Get hash code of entered request id in byte format.
                byte[] _reqByte = md5.ComputeHash(Encoding.UTF8.GetBytes(_strParsedReqNo));

                //convert byte array to integer.
                int _parsedReqNo = BitConverter.ToInt32(_reqByte, 0);
                strParsedReqId = Math.Abs(_parsedReqNo).ToString();

                //Check if length of hash code is less than OTPLength.
                //If so, then prepend multiple zeros upto the length becomes atleast OTPLength characters.
                if (strParsedReqId.Length < OTPLength)
                {
                    StringBuilder sb = new StringBuilder(strParsedReqId);
                    for (int k = 0; k < (OTPLength - strParsedReqId.Length); k++)
                    {
                        sb.Insert(0, '0');
                    }
                    strParsedReqId = sb.ToString();
                }
                oneTimePassword = strParsedReqId;
            }

            if (oneTimePassword.Length >= OTPLength)
            {
                oneTimePassword = oneTimePassword.Substring(0, OTPLength);
            }

            strParsedReqId = oneTimePassword + _strParseDate;
            #endregion

            _otpAuth.SDK_HASH_KEY = strParsedReqId;
            _otpAuth.SDK_KEY = int.Parse(oneTimePassword);
            _otpAuth.SDK_CRE_DT = DateTime.Now;
            _otpAuth.SDK_ACT = 1;
            _otpAuth.SDK_IS_VALIDATE = 0;

            //Check Duplicate Authentication keys in DB
            DataTable _tblDuplicateRecords = _generalDAL.GetDuplicateAuthentication(_otpAuth);
            if (_tblDuplicateRecords != null)
            {
                int _rowValue = 0;
                int.TryParse(_tblDuplicateRecords.Rows[0]["Duplicates"].ToString(), out _rowValue);
                if (_rowValue > 0)
                {
                    return OTPGenerator(_otpAuth);
                }
            }

            //Save OTP
            try
            {
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();

                _generalDAL.SaveOtpAuthentication(_otpAuth);

                _generalDAL.TransactionCommit();
            }
            catch (Exception ex)
            {
                _generalDAL.TransactionRollback();
                throw ex;
            }
            finally
            {
                _generalDAL.ConnectionClose();
            }


            return _otpAuth;
        }

        //Udesh 31-Oct-2018
        public int UpdateOTPAthentication(OtpAuthentication _otpAuth)
        {
            int _result = 0;

            _generalDAL = new GeneralDAL();
            try
            {
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                _result = _generalDAL.UpdateOtpAuthentication(_otpAuth);
                _generalDAL.TransactionCommit();
            }
            catch (Exception ex)
            {
                _generalDAL.TransactionRollback();
                throw ex;
            }
            finally
            {
                _generalDAL.ConnectionClose();
            }

            return _result;
        }

        //Udesh 31-Oct-2018
        public int SendOtpAuthenticationSMS(OutSMS _out)
        {
            Int32 _effect = 0;
            _generalDAL = new GeneralDAL();
            try
            {
                #region Prepare SMS details
                _out.Createtime = DateTime.Now;
                _out.Msgstatus = 0;
                _out.Msgtype = "S";
                _out.Receivedtime = DateTime.Now;
                _out.Createtime = DateTime.Now;
                #endregion

                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
               _effect = _generalDAL.SaveSMSOut(_out);
                _generalDAL.TransactionCommit();
            }
            catch
            {
                _generalDAL.TransactionRollback();

                _effect = -1;
            }
            finally
            {
                _generalDAL.ConnectionClose();
    }

            return _effect;
}
 

        /// <summary>
        /// Gets the MST grade by company.
        /// Added by Udesh 15-Nov-2018
        /// </summary>
        /// <param name="_com">The COM.</param>
        /// <returns></returns>
        public DataTable GetMstGradeByCompany(string _com)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetMstGradeByCompany(_com);
        }

        /// <summary>
        /// Saves the bin assignment.
        /// Udesh 12-Nov-2018
        /// </summary>
        /// <param name="_binAssignList">The bin assign list.</param>
        /// <param name="_error">The error.</param>
        /// <returns>int</returns>
        public int SaveBinAssignment(List<REF_BIN_ASSIGN> _binAssignList, out string _error)
        {
            Int32 _effects = 0;
            _error = string.Empty;
            _generalDAL = new GeneralDAL();

            try
            {
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();

                _effects = _generalDAL.SaveBinAssignment(_binAssignList);
                _generalDAL.TransactionCommit();
            }
            catch (Exception err)
            {
                _effects = -1;
                _error = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();
            }
            return _effects;
        }


        /// <summary>
        /// Gets the bin assignment details by bank code.
        /// Udesh 12-Nov-2018
        /// </summary>
        /// <param name="_binAssign">The bin assign.</param>
        /// <returns>Bin Assignment Details</returns>
        public DataTable GetBinAssignmentDetailsByBankCode(REF_BIN_ASSIGN _binAssign)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetBinAssignmentDetailsByBankCode(_binAssign.RBA_BNK_ID);
        }


        /// <summary>
        /// Gets the bin assignment details by bin number.
        /// Udesh 23-Nov-2018
        /// </summary>
        /// <param name="_binAssign">The bin assign.</param>
        /// <returns>Bin Assignment Details</returns>
        public REF_BIN_ASSIGN GetBinAssignmentDetailsByBinNumber(REF_BIN_ASSIGN _binAssign)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetBinAssignmentDetailsByBinNumber(_binAssign.RBA_BIN_NO, _binAssign.RBA_TO_DT);
        }
        public DataTable GetContribution(string itmCd)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetContribution(itmCd);
        }
        public Int32 UpdateItemContribution(string itmCd, string invenCon, string gPCon, string brnCon, out string _error)
        {
            Int32 _effects = 0;
            _error = string.Empty;
            _generalDAL = new GeneralDAL();

            try
            {
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();

                _effects = _generalDAL.UpdateItemContribution(itmCd, invenCon, gPCon, brnCon);
                _generalDAL.TransactionCommit();
            }
            catch (Exception err)
            {
                _effects = -1;
                _error = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();
            }
            return _effects;
        }
        public Int32 UpdateItemCatContribution(string cat, string invenCon, string gPCon, out string _error)
        {
            Int32 _effects = 0;
            _error = string.Empty;
            _generalDAL = new GeneralDAL();

            try
            {
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();

                _effects = _generalDAL.UpdateItemCatContribution(cat, invenCon, gPCon);
                _generalDAL.TransactionCommit();
            }
            catch (Exception err)
            {
                _effects = -1;
                _error = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();
            }
            return _effects;
        }
        public REF_ITM_CATE1 GetItemCate1ById(string cd)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetItemCate1ById(cd);
        }
        public Int32 UpdateItemBrndContribution(string brndCon, string brnd, out string _error)
        {
            Int32 _effects = 0;
            _error = string.Empty;
            _generalDAL = new GeneralDAL();

            try
            {
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();

                _effects = _generalDAL.UpdateItemBrndContribution(brndCon, brnd);
                _generalDAL.TransactionCommit();
            }
            catch (Exception err)
            {
                _effects = -1;
                _error = "ERROR : " + err.Message.ToString();
                _generalDAL.TransactionRollback();
            }
            return _effects;
        }

        public bool IsvalidMobileNo(string mobileNo, out string msg)
        {
            msg = "";
            bool isvalid = false;
            mobileNo = mobileNo.Trim();
            if (string.IsNullOrEmpty((mobileNo)))
            {
                msg = "Please Enter Mobile No";
                return isvalid;
            }
            if (mobileNo.Length != 10 && mobileNo.Length != 12)
            {
                msg = "Invalid Mobile Number!";
                return isvalid;
            }
            if (mobileNo.Length == 10)
            {
                string pattern = @"^[0-9]{10}$";
                System.Text.RegularExpressions.Match match = Regex.Match(mobileNo.Trim(), pattern, RegexOptions.IgnoreCase);
                if (!match.Success)
                {
                    msg = "Invalid Mobile Number!";
                    return isvalid;
                }
                else
                {
                    string prefix = mobileNo.Trim().Substring(1, 2);
                    _generalDAL = new GeneralDAL();
                    List<MobilePrefix> mobileprefix = _generalDAL.GetMobilePrefixByPrefixCd(Convert.ToInt32(prefix));
                    if (mobileprefix != null)
                    {
                        if (mobileprefix.Count > 0)
                        {
                            isvalid = true;
                        }
                        else
                        {
                            msg = "Invalid Mobile Number!";
                            return isvalid;
                        }
                    }
                    else
                    {
                        msg = "Invalid Mobile Number!";
                        return isvalid;
                    }
                }
            }
            if (mobileNo.Length == 12)
            {
                string prefix = mobileNo.Trim().Substring(0, 3);
                if (!(prefix.Equals("+94")))
                {
                    msg = "Invalid Mobile Number!";
                    return isvalid;
                }
                string pattern = @"^[0-9]{9}$";
                System.Text.RegularExpressions.Match match = Regex.Match(mobileNo.Substring(3, 9), pattern, RegexOptions.IgnoreCase);
                if (!match.Success)
                {
                    msg = "Invalid Mobile Number!";
                    return isvalid;
                }
                else
                {
                    prefix = mobileNo.Trim().Substring(3, 2);
                    _generalDAL = new GeneralDAL();
                    List<MobilePrefix> mobileprefix = _generalDAL.GetMobilePrefixByPrefixCd(Convert.ToInt32(prefix));
                    if (mobileprefix != null)
                    {
                        if (mobileprefix.Count > 0)
                        {
                            isvalid = true;
                        }
                        else
                        {
                            msg = "Invalid Mobile Number!";
                            return isvalid;
                        }
                    }
                    else
                    {
                        msg = "Invalid Mobile Number!";
                        return isvalid;
                    }
                }
            }
            return isvalid;
        }

        //Dilan 2019-02-06
        public DataTable GetReimbusmentStus(string docNo)
        {
            _generalDAL = new GeneralDAL();
            return _generalDAL.GetReimbusmentStus(docNo);
        }

    }
}
