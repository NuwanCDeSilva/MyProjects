using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FF.DataAccessLayer;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using FF.BusinessObjects;
using FF.BusinessObjects.Genaral;
using FF.DataAccessLayer.BaseDAL;
using System.ServiceModel;
using FF.Interfaces;
using FF.BusinessObjects.Sales;
using FF.BusinessObjects.Security;
using FF.BusinessObjects.Search;


namespace FF.BusinessLogicLayer
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]

    public class SalesBLL : ISales
    {
        SalesDAL _salesDAL;
        GenaralDAL _genaralDal;

        //public Int32 SaveCustomerDetails(cus_details cust, string userId, string userDefPro , string company , string loc, out string err)
        //{
        //    int count = 0;
        //    err = string.Empty;
        //    try
        //    {
        //        _salesDAL = new SalesDAL();
        //        _salesDAL.ConnectionOpen();
        //        _salesDAL.BeginTransaction();
        //        count = _salesDAL.SaveCustomerDetails(cust, userId, loc, company);



        //        _salesDAL.TransactionCommit();
        //        _salesDAL.ConnectionClose();
        //    }
        //    catch (Exception ex)
        //    {
        //        _salesDAL.TransactionRollback();
        //        err = ex.Message;
        //    }

        //    return count;
        //}

        public String SaveCustomerDetails(cus_details cust, string _IS_TAX, string _ACT, string _IS_SVAT, string _TAX_EX, string _IS_SUSPEND, string _AGRE_SEND_SMS, string _AGRE_SEND_EMAIL, string userId, string userDefPro, string company, string loc, out string err)
        {
            err = "";
            Int32 res = -1;
            string count = "";
            err = string.Empty;
            try
            {
                _salesDAL = new SalesDAL();
                _genaralDal = new GenaralDAL();
                _salesDAL.ConnectionOpen();
                _genaralDal.ConnectionOpen();
                _salesDAL.BeginTransaction();
                _genaralDal.BeginTransaction();

                MasterAutoNumber _auto = new MasterAutoNumber();
                //_auto.Aut_cate_cd = _businessEntity.Mbe_cre_pc;//_invoiceHeader.Sah_pc;
                //_auto.Aut_cate_tp = "PRO";
                _auto.Aut_moduleid = "CUS";
                _auto.Aut_number = 0;
                _auto.Aut_start_char = "CONT";
                _auto.Aut_direction = 1;
                _auto.Aut_cate_tp = "COM";
                _auto.Aut_cate_cd = "LGT";

                MasterAutoNumber _number = _genaralDal.GetAutoNumber(_auto.Aut_moduleid, _auto.Aut_direction, _auto.Aut_start_char, _auto.Aut_cate_tp, _auto.Aut_cate_cd, _auto.Aut_modify_dt, _auto.Aut_year);
                // MasterAutoNumber _number = _inventoryDAL.GetAutoNumber(_auto.Aut_moduleid, _auto.Aut_direction, _auto.Aut_start_char, _auto.Aut_cate_tp, null, _auto.Aut_modify_dt, _auto.Aut_year);
                string _cusNo = _auto.Aut_start_char + "" + _number.Aut_number.ToString("000000", CultureInfo.InvariantCulture);

                //MasterAutoNumber _number = _genaralDal.GetAutoNumber(_ptyAuto.Aut_moduleid, _ptyAuto.Aut_direction, _ptyAuto.Aut_start_char, _ptyAuto.Aut_cate_tp, _ptyAuto.Aut_cate_cd, _ptyAuto.Aut_modify_dt, _ptyAuto.Aut_year);
                //string reqNo = hdr.TPRH_PC_CD + "-" + "REQ" + "-" + Convert.ToString(DateTime.Now.Date.Year).Remove(0, 2) + "-" + _number.Aut_number.ToString("000000", CultureInfo.InvariantCulture);
                _genaralDal.UpdateAutoNumber(_auto);
                cust.MBE_CD = _cusNo;


                count = _salesDAL.SaveCustomerDetails(cust, _IS_TAX, _ACT, _IS_SVAT, _TAX_EX, _IS_SUSPEND, _AGRE_SEND_SMS, _AGRE_SEND_EMAIL, userId, loc, company);



                _salesDAL.TransactionCommit();
                _salesDAL.ConnectionClose();
                _genaralDal.TransactionCommit();
                _genaralDal.ConnectionClose();
            }
            catch (Exception ex)
            {
                _salesDAL.TransactionRollback();
                _genaralDal.TransactionRollback();
                err = ex.Message;
            }

            return count;
        }

        public Int32 UpdateCustomerDetails(cus_details cust, string _IS_TAX, string _ACT, string _IS_SVAT, string _TAX_EX, string _IS_SUSPEND, string _AGRE_SEND_SMS, string _AGRE_SEND_EMAIL, string userId, string userDefPro, string company, string loc, out string err)
        {
            int count = 0;
            err = string.Empty;
            try
            {
                _salesDAL = new SalesDAL();
                _salesDAL.ConnectionOpen();
                _salesDAL.BeginTransaction();
                count = _salesDAL.UpdateCustomerDetails(cust, _IS_TAX, _ACT, _IS_SVAT, _TAX_EX, _IS_SUSPEND, _AGRE_SEND_SMS, _AGRE_SEND_EMAIL, userId, loc, company);

                _salesDAL.TransactionCommit();
                _salesDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                _salesDAL.TransactionRollback();
                err = ex.Message;
            }

            return count;
        }

        public Int32 SaveEmployeeData(MST_EMP mst_employee_tbs, string userId, string userDefPro, DateTime serverDatetime, string driverCom, out string err)
        {
            int count = 0;
            err = string.Empty;
            try
            {
                _salesDAL = new SalesDAL();
                _salesDAL.ConnectionOpen();
                _salesDAL.BeginTransaction();
                count = _salesDAL.SaveEmployeeData(mst_employee_tbs);

                //if (count == 1)
                //{
                //    foreach (MST_PCEMP pcemp in mst_pcemp)
                //    {
                //        count = _ToursDAL.SavePcemp(pcemp);
                //    }
                //if (vehicleList != null)
                //{
                //    foreach (mst_fleet_driver vehicle in vehicleList)
                //    {
                //        string seq_no = vehicle.MFD_SEQ.ToString();
                //        string drivercom = driverCom;
                //        string p_mfd_veh_no = vehicle.MFD_VEH_NO.ToUpper();
                //        string p_mfd_dri = vehicle.MFD_DRI;
                //        Int32 p_mfd_act = vehicle.MFD_ACT;
                //        DateTime p_mfd_frm_dt = vehicle.MFD_FRM_DT;
                //        DateTime p_mfd_to_dt = vehicle.MFD_TO_DT;
                //        string p_mfd_cre_by = userId;
                //        DateTime p_mfd_cre_dt = serverDatetime;
                //        string p_mfd_mod_by = userId;
                //        DateTime p_mfd_mod_dt = serverDatetime;
                //        string p_mfd_com = drivercom;
                //        string p_mfd_pc = userDefPro;
                //        Int32 result = _ToursDAL.sp_tours_update_driver_alloc(seq_no, p_mfd_veh_no, p_mfd_dri, p_mfd_act, p_mfd_frm_dt, p_mfd_to_dt, p_mfd_cre_by, p_mfd_cre_dt, p_mfd_mod_by, p_mfd_mod_dt, p_mfd_com, p_mfd_pc);
                //        //sp_tours_update_driver_alloc(seq_no, p_mfd_veh_no, p_mfd_dri, p_mfd_act, p_mfd_frm_dt, p_mfd_to_dt, p_mfd_cre_by, p_mfd_cre_dt, p_mfd_mod_by, p_mfd_mod_dt, p_mfd_com, p_mfd_pc);
                //    }
                //}
                //}

                _salesDAL.TransactionCommit();
                _salesDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                _salesDAL.TransactionRollback();
                err = ex.Message;
            }

            return count;
        }

        public Int32 UpdateEmployeeData(MST_EMP mst_employee_tbs, string userId, string userDefPro, DateTime serverDatetime, string driverCom, out string err)
        {
            int count = 0;
            err = string.Empty;
            try
            {
                _salesDAL = new SalesDAL();
                _salesDAL.ConnectionOpen();
                _salesDAL.BeginTransaction();
                count = _salesDAL.UpdateEmployeeData(mst_employee_tbs);

                //if (count == 1)
                //{
                //foreach (MST_PCEMP pcemp in mst_pcemp)
                //{
                //    int coutItm = _ToursDAL.UpdatePcemp(pcemp);
                //}
                //if (vehicleList != null)
                //{
                //    foreach (mst_fleet_driver vehicle in vehicleList)
                //    {

                //        string seq_no = vehicle.MFD_SEQ.ToString();
                //        string drivercom = driverCom;
                //        string p_mfd_veh_no = vehicle.MFD_VEH_NO.ToUpper();
                //        string p_mfd_dri = vehicle.MFD_DRI;
                //        Int32 p_mfd_act = vehicle.MFD_ACT;
                //        DateTime p_mfd_frm_dt = vehicle.MFD_FRM_DT;
                //        DateTime p_mfd_to_dt = vehicle.MFD_TO_DT;
                //        string p_mfd_cre_by = userId;
                //        DateTime p_mfd_cre_dt = serverDatetime;
                //        string p_mfd_mod_by = userId;
                //        DateTime p_mfd_mod_dt = serverDatetime;
                //        string p_mfd_com = drivercom;
                //        string p_mfd_pc = userDefPro;
                //        Int32 result = _ToursDAL.sp_tours_update_driver_alloc(seq_no, p_mfd_veh_no, p_mfd_dri, p_mfd_act, p_mfd_frm_dt, p_mfd_to_dt, p_mfd_cre_by, p_mfd_cre_dt, p_mfd_mod_by, p_mfd_mod_dt, p_mfd_com, p_mfd_pc);
                //        //sp_tours_update_driver_alloc(seq_no, p_mfd_veh_no, p_mfd_dri, p_mfd_act, p_mfd_frm_dt, p_mfd_to_dt, p_mfd_cre_by, p_mfd_cre_dt, p_mfd_mod_by, p_mfd_mod_dt, p_mfd_com, p_mfd_pc);

                //    }
                //}
                //}

                _salesDAL.TransactionCommit();
                _salesDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                _salesDAL.TransactionRollback();
                err = ex.Message;
            }

            return count;
        }

        //Dilshan 2017/09/06
        public Int32 SaveVessalData(MST_VESSEL mst_vessal_tbs, string userId, string userDefPro, DateTime serverDatetime, out string err)
        {
            int count = 0;
            err = string.Empty;
            try
            {
                _salesDAL = new SalesDAL();
                _salesDAL.ConnectionOpen();
                _salesDAL.BeginTransaction();
                count = _salesDAL.SaveVessalData(mst_vessal_tbs);

                _salesDAL.TransactionCommit();
                _salesDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                _salesDAL.TransactionRollback();
                err = ex.Message;
            }

            return count;
        }

        public Int32 UpdateVessalData(MST_VESSEL mst_vessal_tbs, string userId, string userDefPro, DateTime serverDatetime, out string err)
        {
            int count = 0;
            err = string.Empty;
            try
            {
                _salesDAL = new SalesDAL();
                _salesDAL.ConnectionOpen();
                _salesDAL.BeginTransaction();
                count = _salesDAL.UpdateVessalData(mst_vessal_tbs);

                _salesDAL.TransactionCommit();
                _salesDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                _salesDAL.TransactionRollback();
                err = ex.Message;
            }

            return count;
        }

        //Dilshan 2017/09/08
        public Int32 SaveCostEleData(MST_VESSEL mst_vessal_tbs, string userId, string userDefPro, DateTime serverDatetime, out string err)
        {
            int count = 0;
            err = string.Empty;
            try
            {
                _salesDAL = new SalesDAL();
                _salesDAL.ConnectionOpen();
                _salesDAL.BeginTransaction();
                count = _salesDAL.SaveCostEleData(mst_vessal_tbs);

                _salesDAL.TransactionCommit();
                _salesDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                _salesDAL.TransactionRollback();
                err = ex.Message;
            }

            return count;
        }

        public Int32 UpdateCostEleData(MST_VESSEL mst_vessal_tbs, string userId, string userDefPro, DateTime serverDatetime, out string err)
        {
            int count = 0;
            err = string.Empty;
            try
            {
                _salesDAL = new SalesDAL();
                _salesDAL.ConnectionOpen();
                _salesDAL.BeginTransaction();
                count = _salesDAL.UpdateCostEleData(mst_vessal_tbs);

                _salesDAL.TransactionCommit();
                _salesDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                _salesDAL.TransactionRollback();
                err = ex.Message;
            }

            return count;
        }

        //Dilshan 2017/09/08
        public Int32 SavePortData(MST_VESSEL mst_vessal_tbs, string userId, string userDefPro, DateTime serverDatetime, out string err)
        {
            int count = 0;
            err = string.Empty;
            try
            {
                _salesDAL = new SalesDAL();
                _salesDAL.ConnectionOpen();
                _salesDAL.BeginTransaction();
                count = _salesDAL.SavePortData(mst_vessal_tbs);

                _salesDAL.TransactionCommit();
                _salesDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                _salesDAL.TransactionRollback();
                err = ex.Message;
            }

            return count;
        }

        public Int32 UpdatePortData(MST_VESSEL mst_vessal_tbs, string userId, string userDefPro, DateTime serverDatetime, out string err)
        {
            int count = 0;
            err = string.Empty;
            try
            {
                _salesDAL = new SalesDAL();
                _salesDAL.ConnectionOpen();
                _salesDAL.BeginTransaction();
                count = _salesDAL.UpdatePortData(mst_vessal_tbs);

                _salesDAL.TransactionCommit();
                _salesDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                _salesDAL.TransactionRollback();
                err = ex.Message;
            }

            return count;
        }
        //Isuru 2017/05/29
        //public List<cus_details> CustomerSearchAll(string _com, string _nic, string _mob, string _br, string _pp, string _dl, int _type)
        //{
        //    _salesDAL = new SalesDAL();
        //    return _salesDAL.CustomerSearchAll(_com, _nic, _mob, _br, _pp, _dl, _type);

        //}

        public List<MST_REQ_TYPE> getReqyestTypes(string module, out string error)
        {
            List<MST_REQ_TYPE> list = new List<MST_REQ_TYPE>();
            error = "";
            try
            {
                _salesDAL = new SalesDAL();
                list = _salesDAL.getReqyestTypes(module);
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }
            return list;
        }
        public MST_PROFIT_CENTER getProfitCenterDetails(string pccd, string com, string userid)
        {
            MST_PROFIT_CENTER pc = new MST_PROFIT_CENTER();
            _salesDAL = new SalesDAL();
            pc = _salesDAL.getProfitCenterDetails(pccd, com, userid);
            return pc;
        }
        public MST_EMP getEmployeeDetails(string epf, string com)
        {
            MST_EMP EMP = new MST_EMP();
            _salesDAL = new SalesDAL();
            EMP = _salesDAL.getEmployeeDetails(epf, com);
            return EMP;
        }
        public MST_EMP getReqEmployeeDetails(string epf, string com)
        {
            MST_EMP EMP = new MST_EMP();
            _salesDAL = new SalesDAL();
            EMP = _salesDAL.getReqEmployeeDetails(epf, com);
            return EMP;
        }
        public MST_BUSENTITY getConsigneeDetailsByAccCode(string cuscd, string company, string type)
        {
            MST_BUSENTITY res = new MST_BUSENTITY();
            _salesDAL = new SalesDAL();
            res = _salesDAL.getConsigneeDetailsByAccCode(cuscd, company, type);
            return res;

        }
        public TRN_PETTYCASH_REQ_HDR getReqyestDetials(string type, string reqno, string company, string userDefPro, out string error)
        {
            error = "";
            TRN_PETTYCASH_REQ_HDR req = new TRN_PETTYCASH_REQ_HDR();
            try
            {
                _salesDAL = new SalesDAL();
                req = _salesDAL.getReqyestDetials(type, reqno, company, userDefPro);
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }
            return req;
        }
        public List<TRN_PETTYCASH_REQ_DTL> getReqyestItemDetials(Int32 seq, out string error)
        {
            error = "";
            List<TRN_PETTYCASH_REQ_DTL> itm = new List<TRN_PETTYCASH_REQ_DTL>();
            try
            {
                _salesDAL = new SalesDAL();
                itm = _salesDAL.getReqyestItemDetials(seq);
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }
            return itm;

        }
        public trn_jb_hdr GetJobDetails(string jobno, string company, out string error)
        {
            error = "";
            trn_jb_hdr res = new trn_jb_hdr();
            try
            {
                _salesDAL = new SalesDAL();
                res = _salesDAL.GetJobDetails(jobno, company);
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }
            return res;
        }
        public MST_COST_ELEMENT GetCostElementDetails(string eleCode, out string error)
        {
            error = "";
            MST_COST_ELEMENT res = new MST_COST_ELEMENT();
            try
            {
                _salesDAL = new SalesDAL();
                res = _salesDAL.GetCostElementDetails(eleCode);
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }
            return res;
        }
        public FTW_MES_TP GetUOMDetails(string uomcd, out string error)
        {
            error = "";
            FTW_MES_TP res = new FTW_MES_TP();
            try
            {
                _salesDAL = new SalesDAL();
                res = _salesDAL.GetUOMDetails(uomcd);
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }
            return res;
        }

        public MST_COM getCompanyDetails(string company, out string error)
        {
            error = "";
            MST_COM res = new MST_COM();
            try
            {
                _salesDAL = new SalesDAL();
                res = _salesDAL.getCompanyDetails(company);
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }
            return res;
        }

        public MST_CUR GetCurrencyDetails(string curcd, out string error)
        {
            error = "";
            MST_CUR res = new MST_CUR();
            try
            {
                _salesDAL = new SalesDAL();
                res = _salesDAL.GetCurrencyDetails(curcd);
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }
            return res;
        }
        public FTW_VEHICLE_NO getTelVehLcDet(string code, out string error)
        {
            error = "";
            FTW_VEHICLE_NO res = new FTW_VEHICLE_NO();
            try
            {
                _salesDAL = new SalesDAL();
                res = _salesDAL.getTelVehLcDet(code);
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }
            return res;
        }
        public MasterProfitCenter GetProfitCenter(string _company, string _profitCenter)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetProfitCenter(_company, _profitCenter);

        }
        public MasterCompany GetUserCompanySet(string _company, string _profitCenter)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetUserCompanySet(_company, _profitCenter);

        }
        public MasterExchangeRate GetExchangeRate(string _com, string _fromCur, DateTime _date, string _toCur, string _pc)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetExchangeRate(_com, _fromCur, _date, _toCur, _pc);
        }
        public Int32 savePetttyCashRequest(TRN_PETTYCASH_REQ_HDR hdr, List<TRN_PETTYCASH_REQ_DTL> reqDet, MasterAutoNumber _ptyAuto, out string error)
        {
            error = "";
            Int32 res = -1;
            _salesDAL = new SalesDAL();
            _genaralDal = new GenaralDAL();
            try
            {
                _salesDAL.ConnectionOpen();
                _genaralDal.ConnectionOpen();
                _salesDAL.BeginTransaction();
                _genaralDal.BeginTransaction();
                MasterAutoNumber _number = _genaralDal.GetAutoNumber(_ptyAuto.Aut_moduleid, _ptyAuto.Aut_direction, _ptyAuto.Aut_start_char, _ptyAuto.Aut_cate_tp, _ptyAuto.Aut_cate_cd, _ptyAuto.Aut_modify_dt, _ptyAuto.Aut_year);
                //string reqNo = hdr.TPRH_PC_CD + "-" + "REQ" + "-" + Convert.ToString(DateTime.Now.Date.Year).Remove(0, 2) + "-" + _number.Aut_number.ToString("000000", CultureInfo.InvariantCulture);
                string reqNo = hdr.TPRH_PC_CD + "-" + "REQ" + "-" + _number.Aut_number.ToString("000000", CultureInfo.InvariantCulture);
                _genaralDal.UpdateAutoNumber(_ptyAuto);
                hdr.TPRH_REQ_NO = reqNo;

                Int32 eff = _salesDAL.saveRequestHdr(hdr);
                if (eff > 0)
                {
                    Int32 i = 1;
                    foreach (TRN_PETTYCASH_REQ_DTL RQ in reqDet)
                    {
                        RQ.TPRD_REQ_NO = reqNo;
                        RQ.TPRD_SEQ = eff;
                        res = _salesDAL.saveRequestDtl(RQ);
                        i++;
                    }
                    error = reqNo;
                    _salesDAL.TransactionCommit();
                    _salesDAL.ConnectionClose();
                    _genaralDal.TransactionCommit();
                    _genaralDal.ConnectionClose();

                }
                else
                {
                    res = -1;
                    _salesDAL.TransactionRollback();
                    _salesDAL.ConnectionClose();
                    _genaralDal.TransactionRollback();
                    _genaralDal.ConnectionClose();

                    error = "Unable to update header details.";
                }
            }
            catch (Exception ex)
            {
                res = -1;
                _genaralDal.TransactionRollback();
                _genaralDal.ConnectionClose();
                _salesDAL.TransactionRollback();
                _salesDAL.ConnectionClose();
                error = ex.Message.ToString();
            }
            return res;
        }
        public Int32 updateRequestApproveStus(TRN_PETTYCASH_REQ_HDR request, Int32 appl, out string error)
        {
            error = "";
            Int32 res = -1;
            _salesDAL = new SalesDAL();
            try
            {
                _salesDAL.ConnectionOpen();
                _salesDAL.BeginTransaction();
                res = _salesDAL.updateApproveHdrStus(request, appl);
                _salesDAL.TransactionCommit();
                _salesDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                res = -1;
                _salesDAL.TransactionRollback();
                _salesDAL.ConnectionClose();
                error = ex.Message.ToString();
            }
            return res;
        }
        //dilshan
        public Int32 updateReprintDocStus(string request, string userId, out string error)
        {
            error = "";
            Int32 res = -1;
            _salesDAL = new SalesDAL();
            try
            {
                _salesDAL.ConnectionOpen();
                _salesDAL.BeginTransaction();
                res = _salesDAL.updateReprintDocStus(request, userId);
                _salesDAL.TransactionCommit();
                _salesDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                res = -1;
                _salesDAL.TransactionRollback();
                _salesDAL.ConnectionClose();
                error = ex.Message.ToString();
            }
            return res;
        }
        public Int32 updateReprintDocStus_New(string request, string company, out string error)
        {
            error = "";
            Int32 res = -1;
            _salesDAL = new SalesDAL();
            try
            {
                _salesDAL.ConnectionOpen();
                _salesDAL.BeginTransaction();
                res = _salesDAL.updateReprintDocStus_New(request, company);
                _salesDAL.TransactionCommit();
                _salesDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                res = -1;
                _salesDAL.TransactionRollback();
                _salesDAL.ConnectionClose();
                error = ex.Message.ToString();
            }
            return res;
        }
        public List<TRN_PETTYCASH_REQ_HDR> loadPendingptyReq(string company, string pc, DateTime fromdt, DateTime tdt, Int32 applvl, out string error)
        {
            error = "";
            List<TRN_PETTYCASH_REQ_HDR> list = new List<TRN_PETTYCASH_REQ_HDR>();
            try
            {
                _salesDAL = new SalesDAL();
                list = _salesDAL.getPendingData(company, pc, fromdt, tdt, applvl);
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }
            return list;
        }
        public TRN_PETTYCASH_REQ_HDR loadRequestDetailsbySeq(Int32 seq, string company, string userDefPro, out string error)
        {
            error = "";
            TRN_PETTYCASH_REQ_HDR req = new TRN_PETTYCASH_REQ_HDR();
            try
            {
                _salesDAL = new SalesDAL();
                req = _salesDAL.loadRequestDetailsbySeq(seq, company, userDefPro);
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }
            return req;
        }
        public DataTable getPetyCshRptData(Int32 reqSeq, string type, out string error)
        {
            error = "";
            DataTable dt = new DataTable("dt");
            try
            {
                _salesDAL = new SalesDAL();
                dt = _salesDAL.getPetyCshRptData(reqSeq, type);
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }
            return dt;
        }
        public DataTable getCompanyDetailsBycd(string company)
        {
            DataTable dt = new DataTable("dt");
            _salesDAL = new SalesDAL();
            dt = _salesDAL.getCompanyDetailsBycd(company);
            return dt;
        }
        public Int32 rejectPtyCshRequest(Int32 sqNo, string userId, DateTime dt, string sessionid, out string error)
        {
            error = "";
            Int32 req = -1;
            try
            {
                _salesDAL = new SalesDAL();
                _salesDAL.ConnectionOpen();

                req = _salesDAL.rejectPtyCshRequest(sqNo, userId, dt, sessionid);
                _salesDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }
            return req;
        }
        public List<TRN_PETTYCASH_REQ_HDR> loadPendingSetReq(string company, string pc, string type, DateTime fmdt, DateTime tdt, string jobno, out string error)
        {
            error = "";
            List<TRN_PETTYCASH_REQ_HDR> list = new List<TRN_PETTYCASH_REQ_HDR>();
            try
            {
                _salesDAL = new SalesDAL();
                list = _salesDAL.loadPendingSetReq(company, pc, type, fmdt, tdt, jobno);
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }
            return list;
        }
        public TRN_PETTYCASH_SETTLE_HDR loadSettlementHdr(string company, string pc, string reqNo, out string error)
        {
            error = "";
            TRN_PETTYCASH_SETTLE_HDR list = new TRN_PETTYCASH_SETTLE_HDR();
            try
            {
                _salesDAL = new SalesDAL();
                list = _salesDAL.loadSettlementHdr(company, pc, reqNo);
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }
            return list;
        }
        public List<TRN_PETTYCASH_SETTLE_DTL> loadSettlementDet(string company, string pc, string reqNo, Int32 reqSeq, out string error)
        {
            error = "";
            List<TRN_PETTYCASH_SETTLE_DTL> list = new List<TRN_PETTYCASH_SETTLE_DTL>();
            try
            {
                _salesDAL = new SalesDAL();
                list = _salesDAL.loadSettlementDet(company, pc, reqNo, reqSeq);
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }
            return list;
        }
        public Int32 saveSetlementDetails(TRN_PETTYCASH_SETTLE_HDR hdr, List<TRN_PETTYCASH_SETTLE_DTL> setDets, MasterAutoNumber _ptyAuto, string sessionid, out string error)
        {
            error = "";
            Int32 res = -1;
            _salesDAL = new SalesDAL();
            _genaralDal = new GenaralDAL();
            try
            {
                _salesDAL.ConnectionOpen();
                _genaralDal.ConnectionOpen();
                _salesDAL.BeginTransaction();
                _genaralDal.BeginTransaction();
                MasterAutoNumber _number = _genaralDal.GetAutoNumber(_ptyAuto.Aut_moduleid, _ptyAuto.Aut_direction, _ptyAuto.Aut_start_char, _ptyAuto.Aut_cate_tp, _ptyAuto.Aut_cate_cd, _ptyAuto.Aut_modify_dt, _ptyAuto.Aut_year);
                //string reqNo = hdr.TPSH_PC_CD + "-" + "SET" + "-" + Convert.ToString(DateTime.Now.Date.Year).Remove(0, 2) + "-" + _number.Aut_number.ToString("000000", CultureInfo.InvariantCulture);
                string reqNo = hdr.TPSH_PC_CD + "-" + "SET" + "-" + _number.Aut_number.ToString("000000", CultureInfo.InvariantCulture);
                _genaralDal.UpdateAutoNumber(_ptyAuto);
                hdr.TPSH_SETTLE_NO = reqNo;

                Int32 eff = _salesDAL.saveSetleRequestHdr(hdr);
                Int32 updt = 0;
                if (eff > 0)
                {
                    Int32 del = _salesDAL.deleteSettlementCost(hdr.TPSH_SETTLE_NO);
                    foreach (TRN_PETTYCASH_SETTLE_DTL RQ in setDets)
                    {
                        RQ.TPSD_SETTLE_NO = reqNo;
                        RQ.TPSD_SEQ_NO = eff;
                        res = _salesDAL.saveRequestDtl(RQ);
                        updt = _salesDAL.updateSettlementRequest(RQ.TPSD_REQ_NO, RQ.TPSD_CRE_BY, RQ.TPSD_CRE_DT, sessionid);
                        if (RQ.TPSD_SETTLE_AMT > 0)
                        {
                            Int32 set = _salesDAL.saveRequestCostDetails(RQ, hdr);
                        }
                    }
                    error = reqNo;
                    _salesDAL.TransactionCommit();
                    _salesDAL.ConnectionClose();
                    _genaralDal.TransactionCommit();
                    _genaralDal.ConnectionClose();

                }
                else
                {
                    res = -1;
                    _salesDAL.TransactionRollback();
                    _salesDAL.ConnectionClose();
                    _genaralDal.TransactionRollback();
                    _genaralDal.ConnectionClose();

                    error = "Unable to update header details.";
                }
            }
            catch (Exception ex)
            {
                res = -1;
                _genaralDal.TransactionRollback();
                _genaralDal.ConnectionClose();
                _salesDAL.TransactionRollback();
                _salesDAL.ConnectionClose();
                error = ex.Message.ToString();
            }
            return res;
        }
        public Int32 updateSetlementDetails(TRN_PETTYCASH_SETTLE_HDR hdr, List<TRN_PETTYCASH_SETTLE_DTL> oldsetDets, List<TRN_PETTYCASH_SETTLE_DTL> setDets, Int32 setSeq, string sessionid, out string error)
        {
            error = "";
            Int32 res = -1;
            _salesDAL = new SalesDAL();
            try
            {
                _salesDAL.ConnectionOpen();
                _salesDAL.BeginTransaction();

                res = _salesDAL.updateSettlementHdr(hdr);
                foreach (TRN_PETTYCASH_SETTLE_DTL RQ in setDets)
                {
                    RQ.TPSD_SETTLE_NO = hdr.TPSH_SETTLE_NO;
                    RQ.TPSD_SEQ_NO = setSeq;
                    res = _salesDAL.saveRequestDtl(RQ);
                    if (RQ.TPSD_SETTLE_AMT > 0)
                    {
                        Int32 set = _salesDAL.saveRequestCostDetails(RQ, hdr);
                    }
                }
                foreach (TRN_PETTYCASH_SETTLE_DTL RQ in oldsetDets)
                {
                    if (RQ.TPSD_ACT == 0)
                    {
                        RQ.TPSD_SETTLE_NO = hdr.TPSH_SETTLE_NO;
                        RQ.TPSD_SEQ_NO = setSeq;
                        res = _salesDAL.updateRequestDtl(RQ);
                        if (RQ.TPSD_SETTLE_AMT > 0)
                        {
                            Int32 set = _salesDAL.updateRequestCostDetails(RQ, hdr);
                        }
                    }
                }

                error = hdr.TPSH_SETTLE_NO;
                _salesDAL.TransactionCommit();
                _salesDAL.ConnectionClose();

            }
            catch (Exception ex)
            {
                res = -1;
                _salesDAL.TransactionRollback();
                _salesDAL.ConnectionClose();
                error = ex.Message.ToString();
            }
            return res;
        }

        public Int32 updateSetlementDetailsRefund(TRN_PETTYCASH_SETTLE_HDR hdr, List<TRN_PETTYCASH_SETTLE_DTL> oldsetDets, List<TRN_PETTYCASH_SETTLE_DTL> setDets, Int32 setSeq, string sessionid, out string error)
        {
            error = "";
            Int32 res = -1;
            _salesDAL = new SalesDAL();
            try
            {
                _salesDAL.ConnectionOpen();
                _salesDAL.BeginTransaction();
                foreach (TRN_PETTYCASH_SETTLE_DTL RQ in setDets)
                {
                    RQ.TPSD_SETTLE_NO = hdr.TPSH_SETTLE_NO;
                    RQ.TPSD_SEQ_NO = setSeq;
                    res = _salesDAL.saveRequestDtl(RQ);

                    Int32 set = _salesDAL.saveRequestCostDetails(RQ, hdr);

                }
                foreach (TRN_PETTYCASH_SETTLE_DTL RQ in oldsetDets)
                {
                    if (RQ.TPSD_ACT == 0)
                    {
                        RQ.TPSD_SETTLE_NO = hdr.TPSH_SETTLE_NO;
                        RQ.TPSD_SEQ_NO = setSeq;
                        res = _salesDAL.updateRequestDtl(RQ);

                        Int32 set = _salesDAL.updateRequestCostDetails(RQ, hdr);

                    }
                }

                error = hdr.TPSH_SETTLE_NO;
                _salesDAL.TransactionCommit();
                _salesDAL.ConnectionClose();

            }
            catch (Exception ex)
            {
                res = -1;
                _salesDAL.TransactionRollback();
                _salesDAL.ConnectionClose();
                error = ex.Message.ToString();
            }
            return res;
        }

        public Int32 updateSetlementApproveStus(TRN_PETTYCASH_SETTLE_HDR request, Int32 appl, out string error)
        {
            error = "";
            Int32 res = -1;
            _salesDAL = new SalesDAL();
            try
            {
                _salesDAL.ConnectionOpen();
                _salesDAL.BeginTransaction();
                res = _salesDAL.updateSetlementApproveStus(request, appl);
                _salesDAL.TransactionCommit();
                _salesDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                res = -1;
                _salesDAL.TransactionRollback();
                _salesDAL.ConnectionClose();
                error = ex.Message.ToString();
            }
            return res;
        }
        public Int32 rejectSettlementRequest(string reqno, string userId, DateTime date, string sessionid, List<TRN_PETTYCASH_SETTLE_DTL> setDets, out string error)
        {
            error = "";
            Int32 req = -1;
            Int32 updt = -1;
            try
            {
                _salesDAL = new SalesDAL();
                _salesDAL.ConnectionOpen();
                _salesDAL.BeginTransaction();
                req = _salesDAL.rejectSettlementRequest(reqno, userId, date, sessionid);
                foreach (TRN_PETTYCASH_SETTLE_DTL RQ in setDets)
                {
                    updt = _salesDAL.updateSettlementRequestStatus(RQ.TPSD_REQ_NO);
                    if (updt < 0)
                    {
                        req = -1;

                    }
                }
                _salesDAL.TransactionCommit();
                _salesDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                _salesDAL.TransactionRollback();
                _salesDAL.ConnectionClose();
                error = ex.Message.ToString();
            }
            if (req == -1)
            {
                _salesDAL.TransactionRollback();
                _salesDAL.ConnectionClose();
                error = "Unable to update settlement request items.";
            }
            return req;
        }
        public List<PaymentType> GetPossiblePaymentTypes_new(string _com, string _schnl, string _pc, string txn_tp, DateTime today, Int32 _isBOCN)
        {
            _salesDAL = new SalesDAL();
            List<PaymentType> pc_list = _salesDAL.GetPossiblePaymentTypes_new(_com, "PC", _pc, txn_tp, today, _isBOCN);
            if (pc_list == null)
                pc_list = _salesDAL.GetPossiblePaymentTypes_new(_com, "SCHNL", _schnl, txn_tp, today, _isBOCN);
            //else
            //    pc_list = null;

            return pc_list;

        }
        public string getBankCode(string bankId)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.getBankCode(bankId);
        }
        public DataTable get_bank_mid_code(string branch_code, string pc, int mode, int period, DateTime _trdate, string _com)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.get_bank_mid_code(branch_code, pc, mode, period, _trdate, _com);
        }
        public MasterOutsideParty GetOutSidePartyDetailsById(string _code)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetOutSidePartyDetailsById(_code);
        }
        public DataTable GetBankCC(string _bank)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetBankCC(_bank);
        }
        public string getAdvanceRefAmount(string cuscd, string company, string receiptno)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.getAdvanceRefAmount(cuscd, company, receiptno);
        }
        public string getCreditRefAmount(string cuscd, string company, string refNo, string profcen)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.getCreditRefAmount(cuscd, company, refNo, profcen);
        }
        public List<GiftVoucherPages> GetGiftVoucherPages(string _com, int _page)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetGiftVoucherPages(_com, _page);
        }
        public DataTable GetGVAlwCom(string _comCode, string _itm, Int32 _act)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetGVAlwCom(_comCode, _itm, _act);
        }
        public LoyaltyMemeber getLoyaltyDetails(string customer, string loyalNu)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.getLoyaltyDetails(customer, loyalNu);
        }
        public LoyaltyPointRedeemDefinition GetLoyaltyRedeemDefinition(string prtTp, string prt, DateTime date, string loyalty)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetLoyaltyRedeemDefinition(prtTp, prt, date, loyalty);
        }
        public BlackListCustomers GetBlackListCustomerDetails(string _cus, Int32 _active)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetBlackListCustomerDetails(_cus, _active);
        }
        public DataTable get_Dep_Bank_Name(string _com, string _pc, string _paytp, string _acc)
        {

            _salesDAL = new SalesDAL();
            return _salesDAL.get_Dep_Bank_Name(_com, _pc, _paytp, _acc);
        }
        public GiftVoucherPages getGiftVoucherPage(string voucherNo, string voucherBook, string company)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.getGiftVoucherPage(voucherNo, voucherBook, company);
        }
        public MasterItem GetItem(string _company, string _item)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetItem(_company, _item);
        }
        public GiftVoucherPages GetGiftVoucherPage(string _com, string _pc, string _item, int _book, int _page, string _prefix)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetGiftVoucherPage(_com, _pc, _item, _book, _page, _prefix);
        }
        public DataTable GetReceipt(string _doc)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetReceipt(_doc);
        }
        public Boolean validateBank_and_Branch(string bus_cd, string branch_cd, string _type)
        {
            _salesDAL = new SalesDAL();
            _salesDAL.ConnectionOpen();

            Boolean eff = false;
            try
            {
                if (branch_cd == null)
                {
                    MasterOutsideParty bank_ = _salesDAL.GetOutSidePartyDetails(bus_cd, _type);
                    if (bank_.Mbi_cd == null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                MasterOutsideParty bank = _salesDAL.GetOutSidePartyDetails(bus_cd, _type);
                if (bank.Mbi_cd == null)
                {
                    return false;
                }
                else
                {
                    DataTable tblDet = _salesDAL.Get_buscom_branch_det(bus_cd);

                    foreach (DataRow rw in tblDet.Rows)
                    {
                        string branchCD = Convert.ToString(rw["MBB_CD"]);
                        if (branchCD == branch_cd)
                        {
                            eff = true;
                        }
                        else
                        {
                            //do nothing.
                        }


                    }
                }

            }
            catch (Exception ex)
            {
                return false;
            }

            _salesDAL.ConnectionClose();
            return eff;
        }
        public DataTable PettyCash_SettlementDetls(Int32 reqSeq, string comCode, out string error)
        {
            error = "";
            DataTable dt = new DataTable("dt");
            try
            {
                _salesDAL = new SalesDAL();
                dt = _salesDAL.PettyCash_SettlementDetls(reqSeq, comCode);
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }
            return dt;
        }
        public TRN_PETTYCASH_SETTLE_HDR load_PtyC_Setl_RequestDetails(Int32 seq, string company, string userDefPro, out string error)
        {
            error = "";
            TRN_PETTYCASH_SETTLE_HDR req = new TRN_PETTYCASH_SETTLE_HDR();
            try
            {
                _salesDAL = new SalesDAL();
                req = _salesDAL.load_PtyC_Setl_RequestDetails(seq, company, userDefPro);
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }
            return req;
        }
        public Int32 updatePetttyCashRequest(TRN_PETTYCASH_REQ_HDR request, List<TRN_PETTYCASH_REQ_DTL> reqDet, out string error)
        {
            error = "";
            Int32 res = -1;
            _salesDAL = new SalesDAL();
            _genaralDal = new GenaralDAL();
            try
            {
                _salesDAL.ConnectionOpen();
                _salesDAL.BeginTransaction();


                Int32 eff = _salesDAL.updateRequestHdr(request);
                if (eff > 0)
                {
                    Int32 re = _salesDAL.updateActivePtyDet(request);
                    foreach (TRN_PETTYCASH_REQ_DTL RQ in reqDet)
                    {
                        res = _salesDAL.saveRequestDtl(RQ);
                    }
                    _salesDAL.TransactionCommit();
                    _salesDAL.ConnectionClose();
                }
                else
                {
                    res = -1;
                    _salesDAL.TransactionRollback();
                    _salesDAL.ConnectionClose();
                    error = "Unable to update header details.";
                }
            }
            catch (Exception ex)
            {
                res = -1;
                _salesDAL.TransactionRollback();
                _salesDAL.ConnectionClose();
                error = ex.Message.ToString();
            }
            return res;
        }
        // Added by Chathura on 3-oct-2017
        public Int32 setInvoicePrintedStatus(string Inv, string company)
        {
            Int32 res = -1;
            _salesDAL = new SalesDAL();
            try
            {
                _salesDAL.ConnectionOpen();
                _salesDAL.BeginTransaction();
                Int32 eff = _salesDAL.setInvoicePrintedStatus(Inv, company);
                _salesDAL.TransactionCommit();
            }
            catch (Exception ex)
            {
                res = -1;
                _salesDAL.TransactionRollback();
                _salesDAL.ConnectionClose();
            }
            return res;
        }
        public string requestTypeDesc(Int32 seq)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.requestTypeDesc(seq);
        }
        public DataTable Inv_Details(string InvNo, string company,string type, out string error)
        {
            error = "";
            DataTable req = new DataTable();
            try
            {
                _salesDAL = new SalesDAL();
                req = _salesDAL.Inv_Details(InvNo, company, type);
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }
            return req;
        }
        public MasterReceiptDivision GetDefRecDivision(string _com, string _pc)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetDefRecDivision(_com, _pc);
        }
        //subodana 2017/07/19
        public Int32 SaveJobInvoice(trn_inv_hdr _hdr, List<trn_inv_det> _job_inv_det, RecieptHeader _rec, List<RecieptItem> _rec_itm, MasterAutoNumber _masterinvauto, MasterAutoNumber _masterrecauto, out string err)
        {
            err = "";
            Int32 seqno = 0;
            int effect = 0;
            string documentNo = "";
            _salesDAL = new SalesDAL();
            _salesDAL.ConnectionOpen();
            _salesDAL.BeginTransaction();
            GenaralDAL _GenaralDAL = new GenaralDAL();
            _GenaralDAL.ConnectionOpen();
            _GenaralDAL.BeginTransaction();
            try
            {
                if (_hdr.Tih_inv_no == null || _hdr.Tih_inv_no == "")
                {
                    Int32 _autoNo = _GenaralDAL.GetAutoNumber(_masterinvauto.Aut_moduleid, _masterinvauto.Aut_direction, _masterinvauto.Aut_start_char, _masterinvauto.Aut_cate_tp, _masterinvauto.Aut_cate_cd, _masterinvauto.Aut_modify_dt, _masterinvauto.Aut_year).Aut_number;
                    //documentNo = _masterinvauto.Aut_start_char + "" + Convert.ToString(_masterinvauto.Aut_year).Remove(0, 2) + "-" + _autoNo.ToString("0000", CultureInfo.InvariantCulture);
                    documentNo = _hdr.Tih_com_cd + "" + Convert.ToString(_masterinvauto.Aut_year).Remove(0, 2) + Convert.ToString(DateTime.Now.Month) + _masterinvauto.Aut_cate_cd + _autoNo.ToString("0000", CultureInfo.InvariantCulture);
                    effect = _GenaralDAL.UpdateAutoNumber(_masterinvauto);
                    _hdr.Tih_inv_no = documentNo;
                    err = documentNo;
                }
                _hdr.Tih_bal_settle_amt = _hdr.Tih_inv_amt;
                if (_hdr.Tih_cus_cd != "" && _hdr.Tih_cus_cd != null)
                {
                    List<GET_CUS_BASIC_DATA> _cusdata = _GenaralDAL.GetCustormerBasicData(_hdr.Tih_cus_cd, _hdr.Tih_com_cd, "C");
                    if (_cusdata != null)
                    {
                        if (_cusdata.Count > 0)
                        {
                            _hdr.Tih_acc_cus_name = _cusdata.First().mbe_name.ToString();
                            _hdr.Tih_acc_cus_add1 = _cusdata.First().mbe_add1.ToString();
                            _hdr.Tih_acc_cus_add2 = _cusdata.First().mbe_add2.ToString();
                            _hdr.Tih_acc_cus_cd = _hdr.Tih_cus_cd;
                        }
                    }

                }
                effect = _salesDAL.saveInvoiceHDr(_hdr, out seqno);
                err = _hdr.Tih_inv_no;

                if (_job_inv_det != null)
                {
                    foreach (var _detlist in _job_inv_det)
                    {
                        _detlist.Tid_seq_no = seqno;
                        _detlist.Tid_inv_no = _hdr.Tih_inv_no;
                        //_detlist.Tid_invr_merge_line = _detlist.Tid_line_no;
                        _detlist.Tid_invr_merge_line = _detlist.Tid_invr_merge_line;
                        _detlist.Tid_bl_d_no = _hdr.Tih_bl_d_no;
                        _detlist.Tid_bl_m_no = _hdr.Tih_bl_m_no;
                        _detlist.Tid_bl_h_no = _hdr.Tih_bl_h_no;
                        if (_hdr.Tih_inv_dt == DateTime.Now.Date)
                        {
                            List<MasterItemTax> _itmTax = new List<MasterItemTax>();
                            _itmTax = _salesDAL.GetItemTax(_hdr.Tih_com_cd, _detlist.Tid_cha_code, "GOD", string.Empty, string.Empty);

                            foreach (MasterItemTax _one in _itmTax)
                            {
                                trn_inv_itmtax _tax = new trn_inv_itmtax();
                                _tax.Tiit_inv_no = _detlist.Tid_inv_no;
                                _tax.Tiit_tax_element = _one.Mict_itm_cd;
                                _tax.Tiit_seq_no = _detlist.Tid_seq_no;
                                _tax.Tiid_com_cd = _hdr.Tih_com_cd;
                                _tax.Tiid_tax_clb_amt = _one.Mict_tax_rate * _detlist.Tid_cha_amt / 100;
                                _tax.Tiid_tax_clb_rate = _one.Mict_tax_rate;
                                _tax.Tiid_tax_job_no = _hdr.Tih_job_no;
                                _tax.Tiid_tax_rate = _one.Mict_tax_rate;
                                _tax.Tiid_tax_ser_cd = _detlist.Tid_ser_cd;
                                _tax.Tiid_tax_type = _one.Mict_taxrate_cd;
                                _tax.Tiid_tax_unc_amt = _one.Mict_tax_rate * _detlist.Tid_cha_amt / 100;
                                _tax.Tiid_tax_unc_rate = _one.Mict_tax_rate;
                                _salesDAL.SaveInvoiceTaxItem(_tax);
                            }
                        }
                        else
                        {
                            List<MasterItemTax> _itmTaxEff = new List<MasterItemTax>();
                            _itmTaxEff = _salesDAL.GetItemTaxEffDt(_hdr.Tih_com_cd, _detlist.Tid_cha_code, "GOD", string.Empty, string.Empty, _hdr.Tih_inv_dt);

                            if (_itmTaxEff.Count > 0)
                            {
                                foreach (MasterItemTax _one in _itmTaxEff)
                                {
                                    trn_inv_itmtax _tax = new trn_inv_itmtax();
                                    _tax.Tiit_inv_no = _detlist.Tid_inv_no;
                                    _tax.Tiit_tax_element = _one.Mict_itm_cd;
                                    _tax.Tiit_seq_no = _detlist.Tid_seq_no;
                                    _tax.Tiid_com_cd = _hdr.Tih_com_cd;
                                    _tax.Tiid_tax_clb_amt = _one.Mict_tax_rate * _detlist.Tid_cha_amt / 100;
                                    _tax.Tiid_tax_clb_rate = _one.Mict_tax_rate;
                                    _tax.Tiid_tax_job_no = _hdr.Tih_job_no;
                                    _tax.Tiid_tax_rate = _one.Mict_tax_rate;
                                    _tax.Tiid_tax_ser_cd = _detlist.Tid_ser_cd;
                                    _tax.Tiid_tax_type = _one.Mict_taxrate_cd;
                                    _tax.Tiid_tax_unc_amt = _one.Mict_tax_rate * _detlist.Tid_cha_amt / 100;
                                    _tax.Tiid_tax_unc_rate = _one.Mict_tax_rate;
                                    _salesDAL.SaveInvoiceTaxItem(_tax);
                                }
                            }
                            else
                            {
                                List<LogMasterItemTax> _itmTax = new List<LogMasterItemTax>();
                                _itmTax = _salesDAL.GetItemTaxLog(_hdr.Tih_com_cd, _detlist.Tid_cha_code, "GOD", string.Empty, string.Empty, _hdr.Tih_inv_dt);

                                foreach (LogMasterItemTax _one in _itmTax)
                                {
                                    trn_inv_itmtax _tax = new trn_inv_itmtax();
                                    _tax.Tiit_inv_no = _detlist.Tid_inv_no;
                                    _tax.Tiit_tax_element = _one.Lict_itm_cd;
                                    _tax.Tiit_seq_no = _detlist.Tid_seq_no;
                                    _tax.Tiid_com_cd = _hdr.Tih_com_cd;
                                    _tax.Tiid_tax_clb_amt = _one.Lict_tax_rate * _detlist.Tid_cha_amt / 100;
                                    _tax.Tiid_tax_clb_rate = _one.Lict_tax_rate;
                                    _tax.Tiid_tax_job_no = _hdr.Tih_job_no;
                                    _tax.Tiid_tax_rate = _one.Lict_tax_rate;
                                    _tax.Tiid_tax_ser_cd = _detlist.Tid_ser_cd;
                                    _tax.Tiid_tax_type = _one.Lict_tax_cd;
                                    _tax.Tiid_tax_unc_amt = _one.Lict_tax_rate * _detlist.Tid_cha_amt / 100;
                                    _tax.Tiid_tax_unc_rate = _one.Lict_tax_rate;
                                    _salesDAL.SaveInvoiceTaxItem(_tax);
                                }
                            }
                        }
                        effect = _salesDAL.SaveInvDetails(_detlist);
                    }
                }
                string _documentNo = "";
                string _autoNumberRecType = "";
                if (_rec_itm != null)
                {
                    if (_rec_itm.Count > 0)
                    {
                        if (_rec.Sar_receipt_no == null || _rec.Sar_receipt_no == "")
                        {
                            Int32 _autoNo = _GenaralDAL.GetAutoNumber(_masterrecauto.Aut_moduleid, _masterrecauto.Aut_direction, _masterrecauto.Aut_start_char, _masterrecauto.Aut_cate_tp, _masterrecauto.Aut_cate_cd, _masterrecauto.Aut_modify_dt, _masterrecauto.Aut_year).Aut_number;
                            _documentNo = _masterrecauto.Aut_cate_cd + _masterrecauto.Aut_start_char + string.Format("{0:0000}", _autoNo);
                            effect = _GenaralDAL.UpdateAutoNumber(_masterrecauto);
                            if (_rec.Sar_anal_3 == "SYSTEM")
                            {
                                Int32 _autoNoRecTp = _GenaralDAL.GetAutoNumber(_masterrecauto.Aut_moduleid, _masterrecauto.Aut_direction, _masterrecauto.Aut_start_char, _masterrecauto.Aut_cate_tp, _masterrecauto.Aut_cate_cd, _masterrecauto.Aut_modify_dt, _masterrecauto.Aut_year).Aut_number;
                                _autoNumberRecType = _masterrecauto.Aut_cate_cd + "-" + _masterrecauto.Aut_start_char + "-" + string.Format("{0:000000}", _autoNoRecTp);
                                effect = _GenaralDAL.UpdateAutoNumber(_masterrecauto);
                            }
                            else
                            {
                                _autoNumberRecType = _rec.Sar_manual_ref_no;
                            }
                            _rec.Sar_receipt_no = _documentNo;
                        }

                        effect = _salesDAL.SaveReceiptHeader(_rec);
                        _rec.Sar_seq_no = effect;
                        foreach (var _rlist in _rec_itm)
                        {
                            _rlist.Sard_seq_no = _rec.Sar_seq_no;
                            _rlist.Sard_inv_no = _hdr.Tih_inv_no;
                            _rlist.Sard_receipt_no = _rec.Sar_receipt_no;
                            effect = _salesDAL.SaveReceiptItem(_rlist);
                            effect = _salesDAL.UpdateInvoiceBalance(_hdr.Tih_inv_no, _rlist.Sard_settle_amt);
                        }
                    }
                }

                _GenaralDAL.TransactionCommit();
                _salesDAL.TransactionCommit();

            }
            catch (Exception ex)
            {
                err = ex.Message;
                _GenaralDAL.TransactionRollback();
                _GenaralDAL.ConnectionClose();
                _salesDAL.TransactionRollback();
                _salesDAL.ConnectionClose();
                effect = -1;
            }

            return effect;
        }
        //subodana 2017/07/20
        public List<trn_inv_hdr> GetInvHdr(string doc, string com)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetInvHdr(doc, com);
        }
        //subodana 2017/07/20
        public List<trn_inv_det> Get_Inv_det(string seq)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.Get_Inv_det(seq);
        }
        public List<trn_inv_det> Get_Inv_detApp(string seq)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.Get_Inv_detApp(seq);
        }
        public bool IsValidReceiptType(string _company, string _type)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.IsValidReceiptType(_company, _type);
        }
        //subodana 2017/07/27
        public Int32 SaveJobReciept(RecieptHeader _rec, List<RecieptItem> _rec_itm, MasterAutoNumber _masterrecauto, out string err)
        {
            err = "";
            int effect = 0;
            _salesDAL = new SalesDAL();
            _salesDAL.ConnectionOpen();
            _salesDAL.BeginTransaction();
            GenaralDAL _GenaralDAL = new GenaralDAL();
            _GenaralDAL.ConnectionOpen();
            _GenaralDAL.BeginTransaction();
            try
            {
                string _documentNo = "";
                string _autoNumberRecType = "";
                if (_rec_itm != null)
                {
                    if (_rec_itm.Count > 0)
                    {
                        if (_rec.Sar_receipt_no == null || _rec.Sar_receipt_no == "")
                        {
                            Int32 _autoNo = _GenaralDAL.GetAutoNumber(_masterrecauto.Aut_moduleid, _masterrecauto.Aut_direction, _masterrecauto.Aut_start_char, _masterrecauto.Aut_cate_tp, _masterrecauto.Aut_cate_cd, _masterrecauto.Aut_modify_dt, _masterrecauto.Aut_year).Aut_number;
                            //added by dilshan on 05/02/2018
                            _documentNo = _rec.Sar_com_cd + "-" + _masterrecauto.Aut_cate_cd + "-" + _masterrecauto.Aut_start_char + "-" + string.Format("{0:0000}", _autoNo);
                            
//commented by dilshan on 05/02/2018
                            //_documentNo = _masterrecauto.Aut_cate_cd + "-" + _masterrecauto.Aut_start_char + "-" + string.Format("{0:0000}", _autoNo);                     
                            effect = _GenaralDAL.UpdateAutoNumber(_masterrecauto);

                            if (_rec.Sar_anal_3 == "SYSTEM")
                            {
                                Int32 _autoNoRecTp = _GenaralDAL.GetAutoNumber(_masterrecauto.Aut_moduleid, _masterrecauto.Aut_direction, _masterrecauto.Aut_start_char, _masterrecauto.Aut_cate_tp, _masterrecauto.Aut_cate_cd, _masterrecauto.Aut_modify_dt, _masterrecauto.Aut_year).Aut_number;
                                _autoNumberRecType = _masterrecauto.Aut_cate_cd + "-" + _masterrecauto.Aut_start_char + "-" + string.Format("{0:000000}", _autoNoRecTp);
                                effect = _GenaralDAL.UpdateAutoNumber(_masterrecauto);
                            }
                            else
                            {
                                _autoNumberRecType = _rec.Sar_manual_ref_no;
                            }
                            _rec.Sar_receipt_no = _documentNo;
                        }

                        effect = _salesDAL.SaveReceiptHeader(_rec);
                        err = _rec.Sar_receipt_no;
                        _rec.Sar_seq_no = effect;
                        int count = 0;
                        foreach (var _rlist in _rec_itm)
                        {
                            count++;
                            _rlist.Sard_line_no = count;
                            _rlist.Sard_seq_no = _rec.Sar_seq_no;
                            _rlist.Sard_receipt_no = _rec.Sar_receipt_no;
                            effect = _salesDAL.SaveReceiptItem(_rlist);
                            //Update Invoice bal
                            if (_rlist.Sard_inv_no != null)
                            {
                                if (_rlist.Sard_inv_no.Trim() != "")
                                {
                                    effect = _salesDAL.UpdateInvoiceBalance(_rlist.Sard_inv_no, _rlist.Sard_settle_amt);
                                }
                            }

                        }
                    }
                }

                _GenaralDAL.TransactionCommit();
                _salesDAL.TransactionCommit();

            }
            catch (Exception ex)
            {
                err = ex.Message;
                _GenaralDAL.TransactionRollback();
                _GenaralDAL.ConnectionClose();
                _salesDAL.TransactionRollback();
                _salesDAL.ConnectionClose();
                effect = -1;
            }

            return effect;
        }
        //Udaya 26.07.2017 Get Data for Manifest Letter
        public DataTable GetManifestLetter_Dtl(DateTime frmDate, DateTime toDate, string docNo, string comCode)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetManifestLetter_Dtl(frmDate, toDate, docNo, comCode);
        }
        //Udaya 26.07.2017 Get Data for Cargo Manifest
        public DataTable Get_CargoManifest_Dtl(DateTime frmDate, DateTime toDate, string docNo, string comCode)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.Get_CargoManifest_Dtl(frmDate, toDate, docNo, comCode);
        }
        //Udaya 27.07.2017 Get Data for Delivary Order
        public DataTable Get_Container_Dtl(string docNo)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.Get_Container_Dtl(docNo);
        }
        //DILSHAN
        public DataTable Get_Container_Dtlcount(string docNo)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.Get_Container_Dtlcount(docNo);
        }
        //Udaya 27.07.2017 Get Data for Delivary Order
        public DataTable Get_DeliveryOrder_Dtl(DateTime frmDate, DateTime toDate, string docNo, string comCode)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.Get_DeliveryOrder_Dtl(frmDate, toDate, docNo, comCode);
        }
        public RecieptHeader GetReceiptHeader(string _com, string _pc, string _doc)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetReceiptHeader(_com, _pc, _doc);
        }
        public List<RecieptItem> GetReceiptDetails(RecieptItem _paramRecDetails)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetReceiptDetails(_paramRecDetails.Sard_receipt_no);
        }
        public List<RecieptItem> GetReceiptDetailsWithinvno(string _invoiceno)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetReceiptDetailsWithinvno(_invoiceno);
        }
        //Udaya 28.07.2017 Get Data for Draft
        public DataTable Get_Draft_Dtl(DateTime frmDate, DateTime toDate, string docNo, string comCode)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.Get_Draft_Dtl(frmDate, toDate, docNo, comCode);
        }
        //Udaya 28.07.2017 Get Data for House rpt (Air Wise Bill Report)
        public DataTable Get_House_Dtl(DateTime frmDate, DateTime toDate, string docNo, string comCode)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.Get_House_Dtl(frmDate, toDate, docNo, comCode);
        }
        //subodana 2017/07/29
        public Int32 SaveJobCreditNote(trn_inv_hdr _hdr, List<trn_inv_det> _job_inv_det, MasterAutoNumber _masterinvauto, out string err)
        {
            err = "";
            Int32 seqno = 0;
            int effect = 0;
            string documentNo = "";
            _salesDAL = new SalesDAL();
            _salesDAL.ConnectionOpen();
            _salesDAL.BeginTransaction();
            GenaralDAL _GenaralDAL = new GenaralDAL();
            _GenaralDAL.ConnectionOpen();
            _GenaralDAL.BeginTransaction();
            try
            {
                if (_hdr.Tih_inv_no == null || _hdr.Tih_inv_no == "")
                {
                    Int32 _autoNo = _GenaralDAL.GetAutoNumber(_masterinvauto.Aut_moduleid, _masterinvauto.Aut_direction, _masterinvauto.Aut_start_char, _masterinvauto.Aut_cate_tp, _masterinvauto.Aut_cate_cd, _masterinvauto.Aut_modify_dt, _masterinvauto.Aut_year).Aut_number;
                    documentNo = _masterinvauto.Aut_start_char + "-" + _autoNo.ToString("0000", CultureInfo.InvariantCulture);
                    effect = _GenaralDAL.UpdateAutoNumber(_masterinvauto);
                    _hdr.Tih_inv_no = documentNo;
                    err = documentNo;
                }
                _hdr.Tih_bal_settle_amt = _hdr.Tih_inv_amt;
                if (_hdr.Tih_cus_cd != "" && _hdr.Tih_cus_cd != null)
                {
                    List<GET_CUS_BASIC_DATA> _cusdata = _GenaralDAL.GetCustormerBasicData(_hdr.Tih_cus_cd, _hdr.Tih_com_cd, "C");
                    if (_cusdata != null)
                    {
                        if (_cusdata.Count > 0)
                        {
                            _hdr.Tih_acc_cus_name = _cusdata.First().mbe_name.ToString();
                            _hdr.Tih_acc_cus_add1 = _cusdata.First().mbe_add1.ToString();
                            _hdr.Tih_acc_cus_add2 = _cusdata.First().mbe_add2.ToString();
                            _hdr.Tih_acc_cus_cd = _hdr.Tih_cus_cd;
                        }
                    }

                }
                effect = _salesDAL.saveInvoiceHDr(_hdr, out seqno);
                err = _hdr.Tih_inv_no;

                if (_job_inv_det != null)
                {
                    foreach (var _detlist in _job_inv_det)
                    {
                        _detlist.Tid_seq_no = seqno;
                        _detlist.Tid_inv_no = _hdr.Tih_inv_no;
                        _detlist.Tid_invr_merge_line = _detlist.Tid_line_no;
                        _detlist.Tid_bl_d_no = _hdr.Tih_bl_d_no;
                        _detlist.Tid_bl_m_no = _hdr.Tih_bl_m_no;
                        _detlist.Tid_bl_h_no = _hdr.Tih_bl_h_no;

                        if (_hdr.Tih_inv_dt == DateTime.Now.Date)
                        {
                            List<MasterItemTax> _itmTax = new List<MasterItemTax>();
                            _itmTax = _salesDAL.GetItemTax(_hdr.Tih_com_cd, _detlist.Tid_cha_code, "GOD", string.Empty, string.Empty);

                            foreach (MasterItemTax _one in _itmTax)
                            {
                                trn_inv_itmtax _tax = new trn_inv_itmtax();
                                _tax.Tiit_inv_no = _detlist.Tid_inv_no;
                                _tax.Tiit_tax_element = _one.Mict_itm_cd;
                                _tax.Tiit_seq_no = _detlist.Tid_seq_no;
                                _tax.Tiid_com_cd = _hdr.Tih_com_cd;
                                _tax.Tiid_tax_clb_amt = _one.Mict_tax_rate * _detlist.Tid_cha_amt / 100;
                                _tax.Tiid_tax_clb_rate = _one.Mict_tax_rate;
                                _tax.Tiid_tax_job_no = _hdr.Tih_job_no;
                                _tax.Tiid_tax_rate = _one.Mict_tax_rate;
                                _tax.Tiid_tax_ser_cd = _detlist.Tid_ser_cd;
                                _tax.Tiid_tax_type = _one.Mict_taxrate_cd;
                                _tax.Tiid_tax_unc_amt = _one.Mict_tax_rate * _detlist.Tid_cha_amt / 100;
                                _tax.Tiid_tax_unc_rate = _one.Mict_tax_rate;
                                _salesDAL.SaveInvoiceTaxItem(_tax);
                            }
                        }
                        else
                        {
                            List<MasterItemTax> _itmTaxEff = new List<MasterItemTax>();
                            _itmTaxEff = _salesDAL.GetItemTaxEffDt(_hdr.Tih_com_cd, _detlist.Tid_cha_code, "GOD", string.Empty, string.Empty, _hdr.Tih_inv_dt);

                            if (_itmTaxEff.Count > 0)
                            {
                                foreach (MasterItemTax _one in _itmTaxEff)
                                {
                                    trn_inv_itmtax _tax = new trn_inv_itmtax();
                                    _tax.Tiit_inv_no = _detlist.Tid_inv_no;
                                    _tax.Tiit_tax_element = _one.Mict_itm_cd;
                                    _tax.Tiit_seq_no = _detlist.Tid_seq_no;
                                    _tax.Tiid_com_cd = _hdr.Tih_com_cd;
                                    _tax.Tiid_tax_clb_amt = _one.Mict_tax_rate * _detlist.Tid_cha_amt / 100;
                                    _tax.Tiid_tax_clb_rate = _one.Mict_tax_rate;
                                    _tax.Tiid_tax_job_no = _hdr.Tih_job_no;
                                    _tax.Tiid_tax_rate = _one.Mict_tax_rate;
                                    _tax.Tiid_tax_ser_cd = _detlist.Tid_ser_cd;
                                    _tax.Tiid_tax_type = _one.Mict_taxrate_cd;
                                    _tax.Tiid_tax_unc_amt = _one.Mict_tax_rate * _detlist.Tid_cha_amt / 100;
                                    _tax.Tiid_tax_unc_rate = _one.Mict_tax_rate;
                                    _salesDAL.SaveInvoiceTaxItem(_tax);
                                }
                            }
                            else
                            {
                                List<LogMasterItemTax> _itmTax = new List<LogMasterItemTax>();
                                _itmTax = _salesDAL.GetItemTaxLog(_hdr.Tih_com_cd, _detlist.Tid_cha_code, "GOD", string.Empty, string.Empty, _hdr.Tih_inv_dt);

                                foreach (LogMasterItemTax _one in _itmTax)
                                {
                                    trn_inv_itmtax _tax = new trn_inv_itmtax();
                                    _tax.Tiit_inv_no = _detlist.Tid_inv_no;
                                    _tax.Tiit_tax_element = _one.Lict_itm_cd;
                                    _tax.Tiit_seq_no = _detlist.Tid_seq_no;
                                    _tax.Tiid_com_cd = _hdr.Tih_com_cd;
                                    _tax.Tiid_tax_clb_amt = _one.Lict_tax_rate * _detlist.Tid_cha_amt / 100;
                                    _tax.Tiid_tax_clb_rate = _one.Lict_tax_rate;
                                    _tax.Tiid_tax_job_no = _hdr.Tih_job_no;
                                    _tax.Tiid_tax_rate = _one.Lict_tax_rate;
                                    _tax.Tiid_tax_ser_cd = _detlist.Tid_ser_cd;
                                    _tax.Tiid_tax_type = _one.Lict_tax_cd;
                                    _tax.Tiid_tax_unc_amt = _one.Lict_tax_rate * _detlist.Tid_cha_amt / 100;
                                    _tax.Tiid_tax_unc_rate = _one.Lict_tax_rate;
                                    _salesDAL.SaveInvoiceTaxItem(_tax);
                                }
                            }
                        }
                        effect = _salesDAL.SaveInvDetails(_detlist);
                    }


                }
                //if(_hdr.Tih_other_ref_no !="" && _hdr.Tih_other_ref_no != null)
                //{
                //    effect = _salesDAL.UpdateInvoiceBalance(_hdr.Tih_other_ref_no, _hdr.Tih_inv_amt);
                //}
                //COMMENTED BY DILSHAN ON 07/06/2018(CREDIT NOTE AMOUNT UPDATING ISSUE)
                if (_hdr.Tih_inv_no != "" && _hdr.Tih_inv_no != null)
                {
                    effect = _salesDAL.UpdateInvoiceBalance(_hdr.Tih_inv_no, _hdr.Tih_inv_amt);
                }
                _GenaralDAL.TransactionCommit();
                _salesDAL.TransactionCommit();

            }
            catch (Exception ex)
            {
                err = ex.Message;
                _GenaralDAL.TransactionRollback();
                _GenaralDAL.ConnectionClose();
                _salesDAL.TransactionRollback();
                _salesDAL.ConnectionClose();
                effect = -1;
            }

            return effect;
        }
        //subodana 2017/07/31
        public Int32 SaveJobDebitNote(trn_inv_hdr _hdr, List<trn_inv_det> _job_inv_det, MasterAutoNumber _masterinvauto, out string err)
        {
            err = "";
            Int32 seqno = 0;
            int effect = 0;
            string documentNo = "";
            _salesDAL = new SalesDAL();
            _salesDAL.ConnectionOpen();
            _salesDAL.BeginTransaction();
            GenaralDAL _GenaralDAL = new GenaralDAL();
            _GenaralDAL.ConnectionOpen();
            _GenaralDAL.BeginTransaction();
            try
            {
                if (_hdr.Tih_inv_no == null || _hdr.Tih_inv_no == "")
                {
                    Int32 _autoNo = _GenaralDAL.GetAutoNumber(_masterinvauto.Aut_moduleid, _masterinvauto.Aut_direction, _masterinvauto.Aut_start_char, _masterinvauto.Aut_cate_tp, _masterinvauto.Aut_cate_cd, _masterinvauto.Aut_modify_dt, _masterinvauto.Aut_year).Aut_number;
                    documentNo = _masterinvauto.Aut_start_char + "-" + Convert.ToString(_masterinvauto.Aut_year).Remove(0, 2) + "-" + _autoNo.ToString("0000", CultureInfo.InvariantCulture);
                    effect = _GenaralDAL.UpdateAutoNumber(_masterinvauto);
                    _hdr.Tih_inv_no = documentNo;
                    err = documentNo;
                }
                _hdr.Tih_bal_settle_amt = _hdr.Tih_inv_amt;
                if (_hdr.Tih_cus_cd != "" && _hdr.Tih_cus_cd != null)
                {
                    List<GET_CUS_BASIC_DATA> _cusdata = _GenaralDAL.GetCustormerBasicData(_hdr.Tih_cus_cd, _hdr.Tih_com_cd, "C");
                    if (_cusdata != null)
                    {
                        if (_cusdata.Count > 0)
                        {
                            _hdr.Tih_acc_cus_name = _cusdata.First().mbe_name.ToString();
                            _hdr.Tih_acc_cus_add1 = _cusdata.First().mbe_add1.ToString();
                            _hdr.Tih_acc_cus_add2 = _cusdata.First().mbe_add2.ToString();
                            _hdr.Tih_acc_cus_cd = _hdr.Tih_cus_cd;
                        }
                    }

                }
                effect = _salesDAL.saveInvoiceHDr(_hdr, out seqno);
                err = _hdr.Tih_inv_no;

                if (_job_inv_det != null)
                {
                    foreach (var _detlist in _job_inv_det)
                    {
                        _detlist.Tid_seq_no = seqno;
                        _detlist.Tid_inv_no = _hdr.Tih_inv_no;
                        _detlist.Tid_invr_merge_line = _detlist.Tid_line_no;
                        _detlist.Tid_bl_d_no = _hdr.Tih_bl_d_no;
                        _detlist.Tid_bl_m_no = _hdr.Tih_bl_m_no;
                        _detlist.Tid_bl_h_no = _hdr.Tih_bl_h_no;

                        if (_hdr.Tih_inv_dt == DateTime.Now.Date)
                        {
                            List<MasterItemTax> _itmTax = new List<MasterItemTax>();
                            _itmTax = _salesDAL.GetItemTax(_hdr.Tih_com_cd, _detlist.Tid_cha_code, "GOD", string.Empty, string.Empty);

                            foreach (MasterItemTax _one in _itmTax)
                            {
                                trn_inv_itmtax _tax = new trn_inv_itmtax();
                                _tax.Tiit_inv_no = _detlist.Tid_inv_no;
                                _tax.Tiit_tax_element = _one.Mict_itm_cd;
                                _tax.Tiit_seq_no = _detlist.Tid_seq_no;
                                _tax.Tiid_com_cd = _hdr.Tih_com_cd;
                                _tax.Tiid_tax_clb_amt = _one.Mict_tax_rate * _detlist.Tid_cha_amt / 100;
                                _tax.Tiid_tax_clb_rate = _one.Mict_tax_rate;
                                _tax.Tiid_tax_job_no = _hdr.Tih_job_no;
                                _tax.Tiid_tax_rate = _one.Mict_tax_rate;
                                _tax.Tiid_tax_ser_cd = _detlist.Tid_ser_cd;
                                _tax.Tiid_tax_type = _one.Mict_taxrate_cd;
                                _tax.Tiid_tax_unc_amt = _one.Mict_tax_rate * _detlist.Tid_cha_amt / 100;
                                _tax.Tiid_tax_unc_rate = _one.Mict_tax_rate;
                                _salesDAL.SaveInvoiceTaxItem(_tax);
                            }
                        }
                        else
                        {
                            List<MasterItemTax> _itmTaxEff = new List<MasterItemTax>();
                            _itmTaxEff = _salesDAL.GetItemTaxEffDt(_hdr.Tih_com_cd, _detlist.Tid_cha_code, "GOD", string.Empty, string.Empty, _hdr.Tih_inv_dt);

                            if (_itmTaxEff.Count > 0)
                            {
                                foreach (MasterItemTax _one in _itmTaxEff)
                                {
                                    trn_inv_itmtax _tax = new trn_inv_itmtax();
                                    _tax.Tiit_inv_no = _detlist.Tid_inv_no;
                                    _tax.Tiit_tax_element = _one.Mict_itm_cd;
                                    _tax.Tiit_seq_no = _detlist.Tid_seq_no;
                                    _tax.Tiid_com_cd = _hdr.Tih_com_cd;
                                    _tax.Tiid_tax_clb_amt = _one.Mict_tax_rate * _detlist.Tid_cha_amt / 100;
                                    _tax.Tiid_tax_clb_rate = _one.Mict_tax_rate;
                                    _tax.Tiid_tax_job_no = _hdr.Tih_job_no;
                                    _tax.Tiid_tax_rate = _one.Mict_tax_rate;
                                    _tax.Tiid_tax_ser_cd = _detlist.Tid_ser_cd;
                                    _tax.Tiid_tax_type = _one.Mict_taxrate_cd;
                                    _tax.Tiid_tax_unc_amt = _one.Mict_tax_rate * _detlist.Tid_cha_amt / 100;
                                    _tax.Tiid_tax_unc_rate = _one.Mict_tax_rate;
                                    _salesDAL.SaveInvoiceTaxItem(_tax);
                                }
                            }
                            else
                            {
                                List<LogMasterItemTax> _itmTax = new List<LogMasterItemTax>();
                                _itmTax = _salesDAL.GetItemTaxLog(_hdr.Tih_com_cd, _detlist.Tid_cha_code, "GOD", string.Empty, string.Empty, _hdr.Tih_inv_dt);

                                foreach (LogMasterItemTax _one in _itmTax)
                                {
                                    trn_inv_itmtax _tax = new trn_inv_itmtax();
                                    _tax.Tiit_inv_no = _detlist.Tid_inv_no;
                                    _tax.Tiit_tax_element = _one.Lict_itm_cd;
                                    _tax.Tiit_seq_no = _detlist.Tid_seq_no;
                                    _tax.Tiid_com_cd = _hdr.Tih_com_cd;
                                    _tax.Tiid_tax_clb_amt = _one.Lict_tax_rate * _detlist.Tid_cha_amt / 100;
                                    _tax.Tiid_tax_clb_rate = _one.Lict_tax_rate;
                                    _tax.Tiid_tax_job_no = _hdr.Tih_job_no;
                                    _tax.Tiid_tax_rate = _one.Lict_tax_rate;
                                    _tax.Tiid_tax_ser_cd = _detlist.Tid_ser_cd;
                                    _tax.Tiid_tax_type = _one.Lict_tax_cd;
                                    _tax.Tiid_tax_unc_amt = _one.Lict_tax_rate * _detlist.Tid_cha_amt / 100;
                                    _tax.Tiid_tax_unc_rate = _one.Lict_tax_rate;
                                    _salesDAL.SaveInvoiceTaxItem(_tax);
                                }
                            }
                        }
                        effect = _salesDAL.SaveInvDetails(_detlist);
                    }
                }
                _GenaralDAL.TransactionCommit();
                _salesDAL.TransactionCommit();

            }
            catch (Exception ex)
            {
                err = ex.Message;
                _GenaralDAL.TransactionRollback();
                _GenaralDAL.ConnectionClose();
                _salesDAL.TransactionRollback();
                _salesDAL.ConnectionClose();
                effect = -1;
            }

            return effect;
        }
        //Udaya 31.07.2017 Get Data for Sales rpt
        public DataTable Get_Sales_Dtl(DateTime frmDate, DateTime toDate, string docNo, string comCode)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.Get_Sales_Dtl(frmDate, toDate, docNo, comCode);
        }
        public DataTable GetContainerType()
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetContainerType();
        }
        //Udaya 01.08.2017 Get Data for Debtors Outstanding rpt
        public DataTable Get_Debtors_Outstanding(DateTime frmDate, DateTime toDate, string cusCode, string comCode, string proCntCode, string userId)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.Get_Debtors_Outstanding(frmDate, toDate, cusCode, comCode, proCntCode, userId);
        }
        //DISHAN 
        public DataTable Get_Debtors_Out(DateTime frmDate, DateTime toDate, string cusCode, string comCode, string proCntCode, string userId)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.Get_Debtors_Out(frmDate, toDate, cusCode, comCode, proCntCode, userId);
        }
        //DISHAN 
        public DataTable Get_Debtors_Out_Summ(DateTime frmDate, DateTime toDate, string cusCode, string comCode, string proCntCode, string userId)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.Get_Debtors_Out_Summ(frmDate, toDate, cusCode, comCode, proCntCode, userId);
        }
        //Udaya 02.08.2017 Get Data for Invoice Enquiry
        public List<trn_inv_hdr> GetInvHdr_Dtls(string JobNo, string modOfShpmnt, string typOfShpmnt, string cusCode, string hbl, string comCode)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetInvHdr_Dtls(JobNo, modOfShpmnt, typOfShpmnt, cusCode, hbl, comCode);
        }
        //subodana
        public DataTable GetSunPC(string type, string com)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetSunPC(type, com);
        }
        //Udaya 05.08.2017 Get Data for Payment Voucher Details Enquiry
        public List<TRN_PETTYCASH_REQ_DTL> GetVou_Dtls(string ReqNo, string SeqNo)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetVou_Dtls(ReqNo, SeqNo);
        }
        //Udaya 05.08.2017 Get Data for Payment Voucher Header Enquiry
        public List<TRN_PETTYCASH_REQ_HDR> GetVou_Hdr(DateTime frmDate, DateTime toDate, string reqNo, string jobNo, string manRefNo, string proCnt)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetVou_Hdr(frmDate, toDate, reqNo, jobNo, manRefNo, proCnt);
        }
        //subodana 2017-08-04
        public List<SUN_JURNAL> GetSunJurnalnew(String com)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetSunJurnalnew(com);
        }
        public List<SUNINVHDR> GetSunInvdatanew(String Com, string pc, DateTime sdate, DateTime edate)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetSunInvdatanew(Com, pc, sdate, edate);
        }
        public List<SUNINVHDR> GetSunInvdatanewRev(String Com, string pc, DateTime sdate, DateTime edate)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetSunInvdatanewRev(Com, pc, sdate, edate);
        }
        //subodana
        public string GetAccountCodeByTp(string _company, string _type)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetAccountCodeByTp(_company, _type);
        }
        public int UpdateInvoiceStatus(string invno, string com, string status, string user)
        {
            int effect = 0;
            _salesDAL = new SalesDAL();
            _salesDAL.ConnectionOpen();
            _salesDAL.BeginTransaction();

            try
            {
                effect = _salesDAL.UpdateInvoiceSatatus(invno, com, status, user);
                _salesDAL.TransactionCommit();
            }
            catch (Exception ex)
            {
                _salesDAL.TransactionRollback();
                _salesDAL.ConnectionClose();
                effect = -1;
            }

            return effect;
        }
        public List<TRN_JOB_COST> GetJobCostData(String jobno)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetJobCostData(jobno);
        }
        public List<TRN_JOB_COST> GetJobCostData_New(String jobno, String com, String pc)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetJobCostData_New(jobno, com, pc);
        }
        public Int32 GetJobCostSavedData(String jobno, String com, String pc)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetJobCostSavedData(jobno, com, pc);
        }
        public int SaveJobClose(string jobno, string remark, string user, DateTime date, List<TRN_JOB_COST> _cost, out string err)
        {
            int effect = 0;
            err = "";
            _salesDAL = new SalesDAL();
            _salesDAL.ConnectionOpen();
            _salesDAL.BeginTransaction();

            try
            {
                if (_cost != null)
                {
                    if (_cost.Count > 0)
                    {
                        foreach (var list in _cost)
                        {
                            effect = _salesDAL.saveJobCostData(list);
                        }
                    }
                }
                effect = _salesDAL.UpdateJobStatus(jobno, "F", user);
                _salesDAL.TransactionCommit();
            }
            catch (Exception ex)
            {
                _salesDAL.TransactionRollback();
                _salesDAL.ConnectionClose();
                effect = -1;
                err = ex.Message;
            }

            return effect;
        }
        public int SaveJobCosting(string com,string jobno, string remark, string user, DateTime date, List<TRN_JOB_COST> _cost,List<TRN_JOB_COST> _rev, out string err)
        {
            int effect = 0;
            err = "";
            _salesDAL = new SalesDAL();
            _salesDAL.ConnectionOpen();
            _salesDAL.BeginTransaction();

            try
            {
                if (_cost != null)
                {
                    if (_cost.Count > 0)
                    {
                        foreach (var list in _cost)
                        {
                            effect = _salesDAL.savePreJobCostData(list, "C", com, user);
                        }
                    }
                }
                if (_rev != null)
                {
                    if (_rev.Count > 0)
                    {
                        foreach (var list in _rev)
                        {
                            effect = _salesDAL.savePreJobCostData(list, "R", com, user);
                        }
                    }
                }
                //effect = _salesDAL.UpdateJobStatus(jobno, "F", user);
                _salesDAL.TransactionCommit();
            }
            catch (Exception ex)
            {
                _salesDAL.TransactionRollback();
                _salesDAL.ConnectionClose();
                effect = -1;
                err = ex.Message;
            }

            return effect;
        }
        public int ApproveJobCosting(string jobno, string remark, string user, DateTime date, List<TRN_JOB_COST> _cost,List<TRN_JOB_COST> _rev,int x, out string err)
        {
            int effect = 0;
            err = "";
            _salesDAL = new SalesDAL();
            _salesDAL.ConnectionOpen();
            _salesDAL.BeginTransaction();

            try
            {
                if (_cost != null)
                {
                    if (_cost.Count > 0)
                    {
                        foreach (var list in _cost)
                        {
                            effect = _salesDAL.AppPreJobCostData(list, x, user, date);
                        }
                    }
                }
                if (_rev != null)
                {
                    if (_rev.Count > 0)
                    {
                        foreach (var list in _rev)
                        {
                            effect = _salesDAL.AppPreJobCostData(list, x, user, date);
                        }
                    }
                }
                //effect = _salesDAL.UpdateJobStatus(jobno, "F", user);
                _salesDAL.TransactionCommit();
            }
            catch (Exception ex)
            {
                _salesDAL.TransactionRollback();
                _salesDAL.ConnectionClose();
                effect = -1;
                err = ex.Message;
            }

            return effect;
        }
        //dilshan on 05/09/2018
        public int SaveAutoJobClose(string jobno, string remark, string user, DateTime date, List<TRN_JOB_COST> _cost, out string err)
        {
            int effect = 0;
            err = "";
            _salesDAL = new SalesDAL();
            _salesDAL.ConnectionOpen();
            _salesDAL.BeginTransaction();

            try
            {
                effect = _salesDAL.AutoUpdateJobStatus(date, "F", user);
                _salesDAL.TransactionCommit();
            }
            catch (Exception ex)
            {
                _salesDAL.TransactionRollback();
                _salesDAL.ConnectionClose();
                effect = -1;
                err = ex.Message;
            }

            return effect;
        }
        //dilshan
        public int ReopenJobClose(string jobno, string remark, string user, DateTime date, out string err)
        {
            int effect = 0;
            err = "";
            _salesDAL = new SalesDAL();
            _salesDAL.ConnectionOpen();
            _salesDAL.BeginTransaction();

            try
            {
                effect = _salesDAL.UpdateReopenJobStatus(jobno, "P", user, remark);
                _salesDAL.TransactionCommit();
            }
            catch (Exception ex)
            {
                _salesDAL.TransactionRollback();
                _salesDAL.ConnectionClose();
                effect = -1;
                err = ex.Message;
            }

            return effect;
        }
        public List<SUNRECIEPTHDR> GetSunRecieptdatanew(String Com, string pc, DateTime sdate, DateTime edate, string type)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetSunRecieptdatanew(Com, pc, sdate, edate, type);
        }
        public List<PetyCashUpload> GetSunPetyCash(String Com, DateTime sdate, DateTime edate)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetSunPetyCash(Com, sdate, edate);
        }
        public List<PetyCashUpload> GetSunPetyCashReq(String Type, DateTime sdate, DateTime edate, string com)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetSunPetyCashReq(Type, sdate, edate, com);
        }
        public List<PetyCashUpload> GetSunPetyCashPaymentReq(String Type, DateTime sdate, DateTime edate)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetSunPetyCashPaymentReq(Type, sdate, edate);
        }
        public List<PayReqUploads> GetPayCashReq(String Type, DateTime sdate, DateTime edate)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetPayCashReq(Type, sdate, edate);
        }
        public int CheckJobUse(string job)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.CheckJobUse(job);
        }
        public DataTable PettyCash_SettlementDetls_Summ(Int32 reqSeq, string comCode, out string error)
        {
            error = "";
            DataTable dt = new DataTable("dt");
            try
            {
                _salesDAL = new SalesDAL();
                dt = _salesDAL.PettyCash_SettlementDetls_Summ(reqSeq, comCode);
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }
            return dt;
        }
        public TRN_PETTYCASH_SETTLE_HDR load_PtyC_Setl_RequestDtl_Validate(Int32 seq, string company, string userDefPro, out string error)
        {
            error = "";
            TRN_PETTYCASH_SETTLE_HDR req = new TRN_PETTYCASH_SETTLE_HDR();
            try
            {
                _salesDAL = new SalesDAL();
                req = _salesDAL.load_PtyC_Setl_RequestDtl_Validate(seq, company, userDefPro);
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }
            return req;
        }
        public TRN_PETTYCASH_REQ_HDR loadRequestDetailsbySeq_val(Int32 seq, string company, string userDefPro, out string error)
        {
            error = "";
            TRN_PETTYCASH_REQ_HDR req = new TRN_PETTYCASH_REQ_HDR();
            try
            {
                _salesDAL = new SalesDAL();
                req = _salesDAL.loadRequestDetailsbySeq_val(seq, company, userDefPro);
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }
            return req;
        }

        // Added by Chathura on 13-sep-2017
        public List<SystemUserChannel> GetUserChannels(string UserID, string Comp)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetUserChannels(UserID, Comp);

        }

        // Added by Chathura on 13-sep-2017
        public List<SystemUserProf> GetUserProfCenters(string UserID, string Comp, string User_def_chnl)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetUserProfCenters(UserID, Comp, User_def_chnl);

        }
        // Added by Chathura on 21-sep-2017
        public Int32 CancelJobReciept(string receiptNo, string type, out string error)
        {
            int effect = 0;
            _salesDAL = new SalesDAL();
            _salesDAL.ConnectionOpen();
            _salesDAL.BeginTransaction();
            error = "";
            try
            {
                List<RecieptItem> _list = _salesDAL.GetReceiptDetails(receiptNo);
                effect = _salesDAL.CancelJobReciept(receiptNo);
                if (type == "DEBT")
                {
                    if (_list != null)
                    {
                        foreach (RecieptItem itm in _list)
                        {
                            effect = _salesDAL.cancelReceiptDetails(itm, receiptNo);
                        }
                    }
                }
                else if (type == "REFUND")
                {
                    effect = _salesDAL.cancelRefundDetails(receiptNo);
                }
                _salesDAL.TransactionCommit();
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
                _salesDAL.TransactionRollback();
                _salesDAL.ConnectionClose();
                effect = -1;
            }

            return effect;

        }

        public List<TRN_PETTYCASH_SETTLE_DTL> LoadAllRefundableJobData(string jobno)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.LoadAllRefundableJobData(jobno);

        }

        //public Int32 saveSetlementDetailsRefund(TRN_PETTYCASH_SETTLE_HDR hdr, List<TRN_PETTYCASH_SETTLE_DTL> setDets, MasterAutoNumber _ptyAuto, string sessionid, out string error)
        //{
        //    error = "";
        //    Int32 res = -1;
        //    _salesDAL = new SalesDAL();
        //    _genaralDal = new GenaralDAL();
        //    try
        //    {
        //        _salesDAL.ConnectionOpen();
        //        _genaralDal.ConnectionOpen();
        //        _salesDAL.BeginTransaction();
        //        _genaralDal.BeginTransaction();
        //        MasterAutoNumber _number = _genaralDal.GetAutoNumber(_ptyAuto.Aut_moduleid, _ptyAuto.Aut_direction, _ptyAuto.Aut_start_char, _ptyAuto.Aut_cate_tp, _ptyAuto.Aut_cate_cd, _ptyAuto.Aut_modify_dt, _ptyAuto.Aut_year);
        //        string reqNo = hdr.TPSH_PC_CD + "-" + "SET" + "-" + Convert.ToString(DateTime.Now.Date.Year).Remove(0, 2) + "-" + _number.Aut_number.ToString("000000", CultureInfo.InvariantCulture);
        //        _genaralDal.UpdateAutoNumber(_ptyAuto);
        //        hdr.TPSH_SETTLE_NO = "";

        //        Int32 eff = _salesDAL.saveSetleRequestHdr(hdr);
        //        Int32 updt = 0;
        //        if (eff > 0)
        //        {
        //            Int32 del = _salesDAL.deleteSettlementCost(hdr.TPSH_SETTLE_NO);
        //            foreach (TRN_PETTYCASH_SETTLE_DTL RQ in setDets)
        //            {
        //                RQ.TPSD_SETTLE_NO = reqNo;
        //                RQ.TPSD_SEQ_NO = eff;
        //                res = _salesDAL.saveRequestDtl(RQ);
        //                updt = _salesDAL.updateSettlementRequest(RQ.TPSD_REQ_NO, RQ.TPSD_CRE_BY, RQ.TPSD_CRE_DT, sessionid);
        //                if (RQ.TPSD_SETTLE_AMT > 0)
        //                {
        //                    Int32 set = _salesDAL.saveRequestCostDetails(RQ, hdr);
        //                }
        //            }
        //            error = reqNo;
        //            _salesDAL.TransactionCommit();
        //            _salesDAL.ConnectionClose();
        //            _genaralDal.TransactionCommit();
        //            _genaralDal.ConnectionClose();

        //        }
        //        else
        //        {
        //            res = -1;
        //            _salesDAL.TransactionRollback();
        //            _salesDAL.ConnectionClose();
        //            _genaralDal.TransactionRollback();
        //            _genaralDal.ConnectionClose();

        //            error = "Unable to update header details.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        res = -1;
        //        _genaralDal.TransactionRollback();
        //        _genaralDal.ConnectionClose();
        //        _salesDAL.TransactionRollback();
        //        _salesDAL.ConnectionClose();
        //        error = ex.Message.ToString();
        //    }
        //    return res;
        //}

        public Int32 saveSetlementDetailsRefund(TRN_PETTYCASH_SETTLE_HDR hdr, List<TRN_PETTYCASH_SETTLE_DTL> setDets, MasterAutoNumber _ptyAuto, string sessionid, out string error)
        {
            error = "";
            Int32 res = -1;
            _salesDAL = new SalesDAL();
            _genaralDal = new GenaralDAL();
            try
            {
                _salesDAL.ConnectionOpen();
                _genaralDal.ConnectionOpen();
                _salesDAL.BeginTransaction();
                _genaralDal.BeginTransaction();

                hdr.TPSH_SETTLE_NO = "";

                Int32 eff = _salesDAL.saveSetleRequestHdr(hdr);
                Int32 updt = 0;
                if (eff > 0)
                {
                    Int32 del = _salesDAL.deleteSettlementCost(hdr.TPSH_SETTLE_NO);
                    foreach (TRN_PETTYCASH_SETTLE_DTL RQ in setDets)
                    {
                        //RQ.TPSD_SETTLE_NO = "";
                        RQ.TPSD_SEQ_NO = eff;
                        res = _salesDAL.saveRequestDtl(RQ);
                        updt = _salesDAL.updateSettlementRequest(RQ.TPSD_REQ_NO, RQ.TPSD_CRE_BY, RQ.TPSD_CRE_DT, sessionid);
                        if (RQ.TPSD_SETTLE_AMT > 0)
                        {
                            Int32 set = _salesDAL.saveRequestCostDetails(RQ, hdr);
                        }
                    }
                    error = "";
                    _salesDAL.TransactionCommit();
                    _salesDAL.ConnectionClose();
                    _genaralDal.TransactionCommit();
                    _genaralDal.ConnectionClose();

                }
                else
                {
                    res = -1;
                    _salesDAL.TransactionRollback();
                    _salesDAL.ConnectionClose();
                    _genaralDal.TransactionRollback();
                    _genaralDal.ConnectionClose();

                    error = "Unable to update header details.";
                }
            }
            catch (Exception ex)
            {
                res = -1;
                _genaralDal.TransactionRollback();
                _genaralDal.ConnectionClose();
                _salesDAL.TransactionRollback();
                _salesDAL.ConnectionClose();
                error = ex.Message.ToString();
            }
            return res;
        }

        public Int32 CancelRefund(string jobno)
        {
            Int32 res = -1;
            _salesDAL = new SalesDAL();
            try
            {
                _salesDAL.ConnectionOpen();
                _salesDAL.BeginTransaction();

                Int32 updt = _salesDAL.CancelRefund(jobno);

                if (updt > 0) res = 1; else res = -1;

                _salesDAL.TransactionCommit();
                _salesDAL.ConnectionClose();

            }
            catch (Exception ex)
            {
                res = -1;
                _salesDAL.TransactionRollback();
                _salesDAL.ConnectionClose();
            }
            return res;
        }

        public List<TRN_PETTYCASH_SETTLE_DTL> CheckJobAlreadyHasRefunds(string jobno)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.CheckJobAlreadyHasRefunds(jobno);
        }

        public List<TRN_PETTYCASH_SETTLE_DTL> CheckJobFullyRefunded(string jobno)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.CheckJobFullyRefunded(jobno);
        }

        public List<TRN_PETTYCASH_SETTLE_DTL> CheckSettlementRejected(string jobno)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.CheckSettlementRejected(jobno);
        }

        public List<TRN_PETTYCASH_SETTLE_DTL> CheckSettlementApproved(string jobno)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.CheckSettlementApproved(jobno);
        }

        public trn_inv_hdr validateInvoiceNUmber(string company, string cus, string othpc, string pc)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.validateInvoiceNUmber(company, cus, othpc, pc);
        }

        public List<JOB_NUM_SEARCH> JobOrPouchDetails(string code, string type, string company, string userId)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.JobOrPouchDetails(code, type, company, userId);
        }
        public List<JOB_NUM_SEARCH> JobOrPouchCostDetails(string code, string type, string company, string userId)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.JobOrPouchCostDetails(code, type, company, userId);
        }
        public List<TRN_JOB_COST> GetJobActualCostData(String jobno)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetJobActualCostData(jobno);
        }
        public List<TRN_JOB_COST> GetJobActualCostData_New(String job, String com, String pc)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetJobActualCostData_New(job, com, pc);
        }
        
        public List<RecieptItem> GetReceiptDetailsNonAlocated(string receiptNo)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetReceiptDetailsNonAlocated(receiptNo);

        }
        public Int32 updateunalocatedReceipt(string recno, decimal setleamt, List<RecieptItem> newrecieptItem, RecieptHeader _ReceiptHeader, out string error)
        {
            error = "";
            Int32 effect = -1;
            _salesDAL = new SalesDAL();
            try
            {
                _salesDAL.ConnectionOpen();
                _salesDAL.BeginTransaction();

                RecieptItem _paramRecDetails = new RecieptItem();
                _paramRecDetails.Sard_receipt_no = _ReceiptHeader.Sar_receipt_no;
                List<RecieptItem> _list = new List<RecieptItem>();
                _list = _salesDAL.GetReceiptDetails(_paramRecDetails.Sard_receipt_no);

                int count = _list.Count;
                foreach (var _rlist in newrecieptItem)
                {
                    count++;
                    _rlist.Sard_line_no = count;
                    _rlist.Sard_seq_no = _ReceiptHeader.Sar_seq_no;
                    _rlist.Sard_receipt_no = _ReceiptHeader.Sar_receipt_no;
                    effect = _salesDAL.SaveReceiptItem(_rlist);
                    //Update Invoice bal
                    if (_rlist.Sard_inv_no != null)
                    {
                        if (_rlist.Sard_inv_no.Trim() != "")
                        {
                            effect = _salesDAL.UpdateInvoiceBalance(_rlist.Sard_inv_no, _rlist.Sard_settle_amt);
                        }
                    }

                }
                effect = _salesDAL.updateReceiptItemOriginal(_ReceiptHeader.Sar_seq_no, setleamt);
                _salesDAL.TransactionCommit();
                _salesDAL.ConnectionClose();

            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
                effect = -1;
                _salesDAL.TransactionRollback();
                _salesDAL.ConnectionClose();
            }
            return effect;
        }

        public List<mst_item_tax> GetElementWiseTaxDetails(String element, string channel, string company)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetElementWiseTaxDetails(element, channel, company);
        }

        public List<mst_item_tax> GetAllTaxDetails(string channel, string company)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetAllTaxDetails(channel, company);
        }

        public List<MainServices> GetJobServiceCode(String jobno, string cusid, string company, string pc)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetJobServiceCode(jobno, cusid, company, pc);
        }

        public List<cus_details> GetCustomerTaxEx(string invparty)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetCustomerTaxEx(invparty);
        }

        public List<InvoiceCom> GetSunUpRestrictStatus(string company, string userDefPro, DateTime invdate)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetSunUpRestrictStatus(company, userDefPro, invdate);
        }

        public List<InvoiceCom> GetNumOfBackdates(string company, string userDefPro)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetNumOfBackdates(company, userDefPro);
        }

        public List<InvoiceCom> GetEtaEtdInvoiceDate(string hblnum, string pc)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetEtaEtdInvoiceDate(hblnum, pc);
        }

        //Tharindu 2017-12-26
        public DataTable Get_Cash_Outstanding(DateTime frmDate, DateTime toDate, string comCode, string proCntCode)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.Get_Cash_Outstanding(frmDate, toDate, comCode, proCntCode);
        }
        public DataTable Get_Job_Status_Detail(DateTime frmDate, DateTime toDate, string comCode, string proCntCode, string jobstatus)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.Get_Job_Status_Detail(frmDate, toDate, comCode, proCntCode, jobstatus);
        }
        public DataTable Get_IRD_Detail(DateTime frmDate, DateTime toDate, string comCode, string proCntCode, string jobstatus)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.Get_IRD_Detail(frmDate, toDate, comCode, proCntCode, jobstatus);
        }
        //Tharindu 2017-12-26
        public DataTable Get_Collection_Summary(DateTime frmDate, DateTime toDate, string comCode, string proCntCode,string type,string paytype,string usertype)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.Get_Collection_Summary(frmDate, toDate, comCode, proCntCode,type,paytype,usertype);
        }

        //Tharindu 2018-04-01
        public DataTable Get_jb_header_new(string comCode,string jobnum, string hbl)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.Get_jb_header_new(comCode, jobnum, hbl);
        }
        public DataTable Get_jb_header(string comCode, string jobnum)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.Get_jb_header(comCode, jobnum);
        }
        //Tharindu 2018-04-01
        public DataTable GetJobCostingData(string job)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetJobCostingData(job);
        }
        //dilshan 2018-11-06
        public DataTable GetJobCostingData_new(string job,string status)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetJobCostingData_new(job, status);
        }
        //Tharindu 2018-04-01
        public DataTable GetJobActualCostingData(string job)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetJobActualCostingData(job);
        }

        //Tharindu 2018-01-05
        public DataTable GetInvoiceAuditTrail(DateTime frmDate, DateTime toDate, string comCode, string pc, string cusid)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetInvoiceAuditTrail(frmDate, toDate, comCode, pc, cusid);
        }
        //subodana
        public string GetEleAccount(string code, string costtype)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetEleAccount(code, costtype);
        }

        //Tharindu 2018-01-10
        public DataTable GetRptReceiptDetails(string comCode, string pc, string receiptid)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetRptReceiptDetails(comCode, pc, receiptid);
        }


        //Tharindu 2018-01-10
        public DataTable GetrptContainerDetails(string comCode, string BLNo)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetrptContainerDetails(comCode, BLNo);
        }

        //Tharindu 2018-01-10
        public DataTable GetfrightChargePayble(string comCode, string HouseblNo, string InvNo)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetfrightChargePayble(comCode, HouseblNo, InvNo);
        }
        public Int32 SaveSunT4(SunLC _obsunlcdata)
        {
            Int32 res = -1;
            _salesDAL = new SalesDAL();
            try
            {
                _salesDAL.ConnectionOpen();
                _salesDAL.BeginTransaction();

                res = _salesDAL.SaveSunT4(_obsunlcdata);

                _salesDAL.TransactionCommit();
                _salesDAL.ConnectionClose();

            }
            catch (Exception ex)
            {
                res = -1;
                _salesDAL.TransactionRollback();
                _salesDAL.ConnectionClose();
            }
            return res;
        }
        public DataTable CheckSUNLC(string COM, string CAT, string CODE)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.CheckSUNLC(COM,CAT,CODE);
        }
        public Int32 UPDATE_RECIEPT_HDRENGLOG(string recNo, Int32 value, string com)
        {
            int resutl = 0;
            _salesDAL = new SalesDAL();
            _salesDAL.ConnectionOpen();
            _salesDAL.BeginTransaction();
            resutl = _salesDAL.UPDATE_RECIEPT_HDRENGLOG(recNo, value, com);
            _salesDAL.TransactionCommit();
            _salesDAL.ConnectionClose();
            return resutl;
        }
        public Int32 UPDATE_INV_HDRENGLOG(string invNo, Int32 value, string com)
        {
            int resutl = 0;
            _salesDAL = new SalesDAL();
            _salesDAL.ConnectionOpen();
            _salesDAL.BeginTransaction();
            resutl = _salesDAL.UPDATE_INV_HDRENGLOG(invNo, value, com);
            _salesDAL.TransactionCommit();
            _salesDAL.ConnectionClose();
            return resutl;
        }
        public Int32 UPDATE_PETTYREQ(string doc, Int32 value, string com)
        {
            int resutl = 0;
            _salesDAL = new SalesDAL();
            _salesDAL.ConnectionOpen();
            _salesDAL.BeginTransaction();
            resutl = _salesDAL.UPDATE_PETTYREQ(doc, value, com);
            _salesDAL.TransactionCommit();
            _salesDAL.ConnectionClose();
            return resutl;
        }
        public Int32 UPDATE_PETTYSETTL(string doc, Int32 value, string com)
        {
            int resutl = 0;
            _salesDAL = new SalesDAL();
            _salesDAL.ConnectionOpen();
            _salesDAL.BeginTransaction();
            resutl = _salesDAL.UPDATE_PETTYSETTL(doc, value, com);
            _salesDAL.TransactionCommit();
            _salesDAL.ConnectionClose();
            return resutl;
        }
        //subodana
        public bool CheckNBTVAT(string com, string pc, string code)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.CheckNBTVAT(com, pc, code);
        }
        public Int32 SaveSunAccountsAll(SunAccountall _sundata)
        {
            Int32 res = -1;
            _salesDAL = new SalesDAL();
            try
            {
                _salesDAL.ConnectionOpen();
                _salesDAL.BeginTransaction();

                res = _salesDAL.SaveSunAccountsAll(_sundata);

                _salesDAL.TransactionCommit();
                _salesDAL.ConnectionClose();

            }
            catch (Exception ex)
            {
                res = -1;
                _salesDAL.TransactionRollback();
                _salesDAL.ConnectionClose();
            }
            return res;
        }
        public Int32 UPDATE_JNALNUMBER( string com)
        {
            int resutl = 0;
            _salesDAL = new SalesDAL();
            _salesDAL.ConnectionOpen();
            _salesDAL.BeginTransaction();
            resutl = _salesDAL.UPDATE_JNALNUMBER(com);
            _salesDAL.TransactionCommit();
            _salesDAL.ConnectionClose();
            return resutl;
        }
        //Dilshan 19-04-2018 Get Data for Sales with gp productwise
        public DataTable Get_Sales_GPProduct(DateTime frmDate, DateTime toDate, string docNo, string comCode)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.Get_Sales_GPProduct(frmDate, toDate, docNo, comCode);
        }
        public DataTable Get_Cost_Of_Sales(DateTime frmDate, DateTime toDate, string comCode, string proCntCode, string userId)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.Get_Cost_Of_Sales(frmDate, toDate, comCode, proCntCode, userId);
        }
        public DataTable Get_Cost_Of_Sales_Req(DateTime frmDate, DateTime toDate, string comCode, string proCntCode, string userId)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.Get_Cost_Of_Sales_Req(frmDate, toDate, comCode, proCntCode, userId);
        }
        public DataTable Get_Cost_Of_Sales_Hdr(DateTime frmDate, DateTime toDate, string comCode, string proCntCode, string userId)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.Get_Cost_Of_Sales_Hdr(frmDate, toDate, comCode, proCntCode, userId);
        }
        public DataTable Get_GP_Closed_Job(DateTime frmDate, DateTime toDate, string comCode, string proCntCode, string userId)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.Get_GP_Closed_Job(frmDate, toDate, comCode, proCntCode, userId);
        }
        public DataTable Get_GP_Closed_Job_Cost(DateTime frmDate, DateTime toDate, string comCode, string proCntCode, string userId)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.Get_GP_Closed_Job_Cost(frmDate, toDate, comCode, proCntCode, userId);
        }
        public DataTable Get_Pending_Adv(DateTime frmDate, DateTime toDate, string comCode, string proCntCode, string userId)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.Get_Pending_Adv(frmDate, toDate, comCode, proCntCode, userId);
        }
        public DataTable Get_Sales_GPSales(DateTime frmDate, DateTime toDate, string docNo, string comCode)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.Get_Sales_GPSales(frmDate, toDate, docNo, comCode);
        }
        public DataTable GetBusinessEntityAllValues(string category, string type_)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetBusinessEntityAllValues(category, type_);
        }
        public List<CUSTOMER_SALES> getCustomerDetails(string selectedcompany, string Channel, string Subchnl, string Area, string Region, string Zone, string pc, DateTime SalesFrom, DateTime SalesTo, string Brand, string MainCat, string txtModel, string txtItem, decimal CheckAmount, string filterby, string cat2, string cat3, Int32 visit, string age, string salary, string customer, string invtype, string schemetype, string schemecode, string CTown, string PTown, string BankCode, string Withserial, string Paymenttype, string user, string dist, string prov)
        {
            try
            {

                _salesDAL = new SalesDAL();
                //if (_salesDAL.Is_Report_DR("BI_CUSTOMERINVOICE_DTL") == true)
                //{
                //    DashboardDal.ConnectionOpen_DR();
                //}
                string company = selectedcompany;//[0];
                //if (Channel != "" || Subchnl != "" || Area != "" || Region != "" || Zone != "")
                //{
                //    DataTable pcDet = DashboardDal.getCustomerSalesPcList(company, Channel, Subchnl, Area, Region, Zone, pc, user);
                //}
                //List<List<DataRow>> table = SplitDataTable(pcDet, 300);
                List<CUSTOMER_SALES> finaldt = new List<CUSTOMER_SALES>();
                //foreach (List<DataRow> lstrow in table)
                //{
                List<CUSTOMER_SALES> cusInvDet = new List<CUSTOMER_SALES>();
                Int32 cnt = 1;
                string pclst = "";
                //foreach (DataRow row in lstrow)
                //{
                //    pclst = pclst + ((cnt != pcDet.Rows.Count) ? row["mpi_pc_cd"].ToString() + "," : row["mpi_pc_cd"].ToString());
                //    cnt++;
                //}
                //pclst = pclst.Remove(pclst.Length - 1);
                cusInvDet = _salesDAL.getCustomerDetails(SalesFrom, SalesTo, CheckAmount, company, pc, MainCat, Brand, txtModel, txtItem, filterby, cat2, cat3, visit, age, salary, customer, invtype, schemetype, schemecode, CTown, PTown, BankCode, Withserial, Paymenttype, Channel, Subchnl, Area, Region, Zone, pc, user, dist, prov);
                finaldt.AddRange(cusInvDet);
                //}
                return finaldt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CUSTOMER_SALES> getCustomerDetails_new(string town, string selectedcompany, string mode, string procenter, string district, string province, string dist, string prov, DateTime SalesFrom, DateTime SalesTo, decimal CheckAmount, string CheckAge, string CheckSalary)
        {
            try
            {
                _salesDAL = new SalesDAL();

                string company = selectedcompany;//[0];

                List<CUSTOMER_SALES> finaldt = new List<CUSTOMER_SALES>();

                List<CUSTOMER_SALES> cusInvDet = new List<CUSTOMER_SALES>();
                Int32 cnt = 1;
                string pclst = "";

                cusInvDet = _salesDAL.getCustomerDetails_new(town, selectedcompany, mode, procenter, district, province, dist, prov, SalesFrom, SalesTo, CheckAmount, CheckAge, CheckSalary);
                finaldt.AddRange(cusInvDet);

                return finaldt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<trn_inv_hdr> GetInvHdrAct(string doc, string com)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetInvHdrAct(doc, com);
        }
        public Int32 saveSetlementDetailsAllocate( string Sett_no,string userid, DateTime credt, string sessionid, out string error)
        {
            error = "";
            Int32 res = -1;
            _salesDAL = new SalesDAL();
            _genaralDal = new GenaralDAL();
            try
            {
                _salesDAL.ConnectionOpen();
                _genaralDal.ConnectionOpen();
                _salesDAL.BeginTransaction();
                _genaralDal.BeginTransaction();


                res = _salesDAL.saveSettlementRequestAllocate(Sett_no, userid, credt, sessionid);

                _salesDAL.TransactionCommit();
                _salesDAL.ConnectionClose();
                _genaralDal.TransactionCommit();
                _genaralDal.ConnectionClose();

            }
            catch (Exception ex)
            {
                res = -1;
                _genaralDal.TransactionRollback();
                _genaralDal.ConnectionClose();
                _salesDAL.TransactionRollback();
                _salesDAL.ConnectionClose();
                error = ex.Message.ToString();
            }
            return res;
        }

        public DataTable Get_Job_Invoice_Detail(DateTime frmDate, DateTime toDate, string comCode, string proCntCode, string jobstatus)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.Get_Job_Invoice_Detail(frmDate, toDate, comCode, proCntCode, jobstatus);
        }
        //Dilshan
        public DataTable GetJobStatusForSun(string _company, string _type, string _no)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetJobStatusForSun(_company, _type, _no);
        }
        //Dilshan
        public DataTable GetFwdAccForSun(string _company, string _type, string _pc, string _doctype)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetFwdAccForSun(_company, _type, _pc, _doctype);
        }
    }
}
