using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using FF.BusinessObjects;
using FF.DataAccessLayer;
using FF.Interfaces;
using FF.BusinessObjects.Tours;
using System.Net.Mail;
using FF.BusinessObjects.ToursNew;

namespace FF.BusinessLogicLayer
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class ToursBLL : ITours
    {
        public ToursDAL _ToursDAL;
        public InventoryDAL _inventoryDAL;
        public SalesDAL _salesDAL = null;
        public FMS_InventoryDAL _fmsInventoryDal = null;
        public GeneralDAL _generalDAL = null;
        public ReptCommonDAL _inventoryRepDAL = null;
        public SecurityDAL _securityDAL = null;

        private static class DataBase
        {
            public static string _scm = "(SCM)";
            public static string _ems = "(EMS)";
            public static string _fms = "(FMS)";
            public static string _reportdb = "(REPORTDB)";
        }

        //Sahan
        public DataTable SP_TOURS_GET_OVERLAP_DATES(string P_MFD_VEH_NO, string P_MFD_DRI, DateTime p_mfd_frm_dt, DateTime p_mfd_to_dt, string p_mfd_seq_no)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.SP_TOURS_GET_OVERLAP_DATES(P_MFD_VEH_NO, P_MFD_DRI, p_mfd_frm_dt, p_mfd_to_dt, p_mfd_seq_no);
        }

        //Sahan
        public DataTable sp_tours_get_Selected_alloc(string p_mfd_veh_no, string p_mfd_dri, Int32 p_mfd_act, DateTime p_mfd_frm_dt, DateTime p_mfd_to_dt)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.sp_tours_get_Selected_alloc(p_mfd_veh_no, p_mfd_dri, p_mfd_act, p_mfd_frm_dt, p_mfd_to_dt);
        }

        //Sahan
        public DataTable SP_TOURS_GET_ALLOCATIONS(string P_MFD_VEH_NO, string p_MFD_DRI)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.SP_TOURS_GET_ALLOCATIONS(P_MFD_VEH_NO, p_MFD_DRI);
        }

        //Sahan
        public DataTable SP_TOURS_GET_DRIVER_COM(string P_MPE_EPF)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.SP_TOURS_GET_DRIVER_COM(P_MPE_EPF);
        }

        //Sahan
        public Int32 sp_tours_update_driver_alloc(string p_mfd_seq, string p_mfd_veh_no, string p_mfd_dri, Int32 p_mfd_act, DateTime p_mfd_frm_dt, DateTime p_mfd_to_dt, string p_mfd_cre_by, DateTime p_mfd_cre_dt, string p_mfd_mod_by, DateTime p_mfd_mod_dt, string p_mfd_com, string p_mfd_pc)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.sp_tours_update_driver_alloc(p_mfd_seq, p_mfd_veh_no, p_mfd_dri, p_mfd_act, p_mfd_frm_dt, p_mfd_to_dt, p_mfd_cre_by, p_mfd_cre_dt, p_mfd_mod_by, p_mfd_mod_dt, p_mfd_com, p_mfd_pc);
        }



        //Sahan
        public DataTable SP_TOURS_GET_DRIVER(string P_MFA_PC)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.SP_TOURS_GET_DRIVER(P_MFA_PC);
        }

        //Sahan
        public DataTable SP_TOURS_GET_VEHICLE(string P_MFA_PC)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.SP_TOURS_GET_VEHICLE(P_MFA_PC);
        }

        //Sahan
        public DataTable sp_tours_get_fleet_alloc2(string p_mfa_regno)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.sp_tours_get_fleet_alloc2(p_mfa_regno);
        }

        //Sahan
        public DataTable sp_tours_get_fleet_alloc(string p_mfa_regno, string p_mfa_pc)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.sp_tours_get_fleet_alloc(p_mfa_regno, p_mfa_pc);
        }

        ////Sahan
        //public Int32 sp_tours_update_fleet(string p_mstf_regno, DateTime p_mstf_dt, string p_mstf_brd, string p_mstf_model, string p_mstf_veh_tp, string p_mstf_sipp_cd, Int32 p_mstf_st_meter, string p_mstf_own, string p_mstf_own_nm, Int32 p_mstf_own_cont, Int32 p_mstf_lst_sermet, string p_mstf_tou_regno, Int32 p_mstf_is_lease, DateTime p_mstf_insu_exp, DateTime p_mstf_reg_exp, string p_mstf_fual_tp, Int32 p_mstf_act, string p_mstf_cre_by, DateTime p_mstf_cre_dt, string p_mstf_mod_by, DateTime p_mstf_mod_dt, string p_mstf_engin_cap, Int32 p_mstf_noof_seat, string p_mst_email, string p_mst_comment, string p_mst_inscom)
        //{
        //    _ToursDAL = new ToursDAL();
        //    return _ToursDAL.sp_tours_update_fleet(p_mstf_regno, p_mstf_dt, p_mstf_brd, p_mstf_model, p_mstf_veh_tp, p_mstf_sipp_cd, p_mstf_st_meter, p_mstf_own, p_mstf_own_nm, p_mstf_own_cont, p_mstf_lst_sermet, p_mstf_tou_regno, p_mstf_is_lease, p_mstf_insu_exp, p_mstf_reg_exp, p_mstf_fual_tp, p_mstf_act, p_mstf_cre_by, p_mstf_cre_dt, p_mstf_mod_by, p_mstf_mod_dt, p_mstf_engin_cap, p_mstf_noof_seat, p_mst_email, p_mst_comment, p_mst_inscom);
        //}
        //Sahan
        public DataTable Get_Fleet(string p_mstf_regno)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.Get_Fleet(p_mstf_regno);
        }

        //Sahan
        public Int32 sp_tours_update_fleet_alloc(string p_mfa_regno, string p_mfa_pc, Int32 p_mfa_act, string p_mfa_cre_by, DateTime p_mfa_cre_dt, string p_mfa_mod_by, DateTime p_mfa_mod_dt)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.sp_tours_update_fleet_alloc(p_mfa_regno, p_mfa_pc, p_mfa_act, p_mfa_cre_by, p_mfa_cre_dt, p_mfa_mod_by, p_mfa_mod_dt);
        }

        //Sahan
        public DataTable Get_Vehicle_Type()
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.Get_Vehicle_Type();
        }

        //Rukshan
        public DataTable GetInvoiceDetailsForPrint(string _invNo, string _Code)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.InvoiceDeatilsForPrint(_invNo, _Code);
        }
        //Rukshan
        public List<invoiceCenter> InvoiceDeatilsForPrintList(string _invNo)
        {
            //List<invoiceCenter> _list = null;
            _ToursDAL = new ToursDAL();
            return _ToursDAL.InvoiceDeatilsForPrintList(_invNo);
        }
        //Tharaka 2015-03-09
        public List<MST_ENQTP> GET_ENQUIRY_TYPE(string Com)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.GET_ENQUIRY_TYPE(Com);
        }

        // Nadeeka 06-04-2015
        public DataTable GET_ENQUIRY_STATUS(string Com)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.GET_ENQUIRY_STATUS(Com);
        }

        //Tharaka 2015-03-09
        public List<MST_FACBY> GET_FACILITY_BY(string Com, string type)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.GET_FACILITY_BY(Com, type);
        }

        //Tharaka 2015-03-09
        public int Save_GEN_CUST_ENQ(GEN_CUST_ENQ oItem, MasterAutoNumber _ReqInsAuto, out string err)
        {
            int resutl = 0;
            Boolean _isNew = false;
            string errMSG = string.Empty;
            err = string.Empty;

            try
            {
                _ToursDAL = new ToursDAL();
                _inventoryDAL = new InventoryDAL();
                _generalDAL = new GeneralDAL();

                _ToursDAL.ConnectionOpen();
                _inventoryDAL.ConnectionOpen();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();

                if (String.IsNullOrEmpty(oItem.GCE_ENQ_ID) && oItem.GCE_SEQ == 0)
                {
                    MasterAutoNumber _reversInv = _inventoryDAL.GetAutoNumber(_ReqInsAuto.Aut_moduleid, _ReqInsAuto.Aut_direction, _ReqInsAuto.Aut_start_char, _ReqInsAuto.Aut_cate_tp, _ReqInsAuto.Aut_cate_cd, _ReqInsAuto.Aut_modify_dt, _ReqInsAuto.Aut_year);
                    _reversInv.Aut_direction = null;
                    _reversInv.Aut_modify_dt = null;
                    err = _reversInv.Aut_cate_cd + "-" + _reversInv.Aut_start_char + "-" + Convert.ToString(DateTime.Now.Date.Year).Remove(0, 2) + "-" + _reversInv.Aut_number.ToString("00000", CultureInfo.InvariantCulture);
                    _inventoryDAL.UpdateAutoNumber(_reversInv);
                    oItem.GCE_ENQ_ID = err;
                    _isNew = true;
                }

                resutl = _ToursDAL.Save_GEN_CUST_ENQ(oItem);

                GEN_ENQLOG oLog = new GEN_ENQLOG();
                oLog.GEL_ENQ_ID = oItem.GCE_ENQ_ID;
                oLog.GEL_USR = oItem.GCE_CRE_BY;
                oLog.GEL_STAGE = oItem.GCE_STUS;
                oLog.GEL_LOGWHEN = DateTime.Now;
                resutl = _ToursDAL.SAVE_GEN_ENQLOG(oLog);

                if (_isNew == true)
                {
                    err = "Successfully Created , Enquiry ID  " + oItem.GCE_ENQ_ID;
                }
                else
                {
                    err = "Successfully Updated , Enquiry ID  " + oItem.GCE_ENQ_ID;
                }

               // OutSMS _out = new OutSMS();
               // if (oItem.GCE_MOB != "N/A" && !string.IsNullOrEmpty(oItem.GCE_MOB))
                //{
                    /*String msg = "Dear Customer, \nYou have created a enquiry.\nEnquiry Id - " + oItem.GCE_ENQ_ID;
                    String mobi = "+94" + oItem.GCE_MOB.Substring(1, 9);
                    _out.Msgstatus = 0;
                    _out.Msgtype = "S";
                    _out.Receivedtime = DateTime.Now;
                    _out.Receiver = mobi;
                    _out.Msg = msg;
                    _out.Receiverphno = mobi;
                    _out.Senderphno = mobi;
                    _out.Refdocno = "0";
                    _out.Sender = "Abans Tours";
                    _out.Createtime = DateTime.Now;
                    SendSMS_InternalMethod(_out, out errMSG);*/
                //}


                _ToursDAL.TransactionCommit();
                _ToursDAL.ConnectionClose();

                _inventoryDAL.TransactionCommit();
                _inventoryDAL.ConnectionClose();

                _generalDAL.TransactionCommit();
                _generalDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                err = ex.Message;
                _ToursDAL.TransactionRollback();
                _inventoryDAL.TransactionRollback();
                _generalDAL.TransactionRollback();
            }
            return resutl;
        }

        //Tharaka 2015-03-09
        public GEN_CUST_ENQ GET_CUST_ENQRY(string Com, string PC, string ENQID)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.GET_CUST_ENQRY(Com, PC, ENQID);
        }

        //Tharaka 2015-03-10
        public List<GEN_CUST_ENQ> GET_ENQRY_BY_CUST(string Com, string CustomerCode)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.GET_ENQRY_BY_CUST(Com, CustomerCode);
        }

        //Tharaka 2015-03-11
        public List<GEN_CUST_ENQ> GET_ENQRY_BY_PC_STATUS(string Com, string PC, String Status, string UserID, Int32 PermissionCode)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            _securityDAL = new SecurityDAL();
            List<GEN_CUST_ENQ> oItems = new List<GEN_CUST_ENQ>();
            string ProfitC = string.Empty;
            int _effect = _securityDAL.Is_OptionPerimitted(Com, UserID, PermissionCode);
            if (_effect > 0)
            {
                ProfitC = string.Empty; ;
            }
            else
            {
                ProfitC = PC;
            }


            oItems = _ToursDAL.GET_ENQRY_BY_PC_STATUS(Com, ProfitC, Status);

            if (oItems.Count > 0)
            {
                foreach (GEN_CUST_ENQ item in oItems)
                {
                    TimeSpan diffResult = DateTime.Now - item.GCE_CRE_DT;
                    item.PendingDate = diffResult.Days + " Days";

                    List<MST_TIMEEXPECT> oExpectTime = _ToursDAL.GET_EXPETTIME(item.GCE_ENQ_COM, item.GCE_ENQ_PC);

                    if (oExpectTime.Count > 0 && oExpectTime.FindAll(x => x.MTE_STUSID == item.GCE_STUS).Count > 0)
                    {
                        MST_TIMEEXPECT oExptTime = oExpectTime.Find(x => x.MTE_STUSID == item.GCE_STUS);
                        if (item.GCE_CRE_DT.AddMinutes(oExptTime.MTE_TIME) < DateTime.Now)
                        {
                            item.IsLateToNextStage = true;
                        }
                        else
                        {
                            item.IsLateToNextStage = false;
                        }
                    }
                }
            }
            return oItems;
        }

        //Tharaka 2015-03-11
        public Int32 SAVE_GEN_ENQLOG(GEN_ENQLOG lst, out string err)
        {
            int resutl = 0;
            err = string.Empty;

            try
            {
                _ToursDAL = new ToursDAL();
                _ToursDAL.ConnectionOpen();

                resutl = _ToursDAL.SAVE_GEN_ENQLOG(lst);

                _ToursDAL.TransactionCommit();
                _ToursDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                _ToursDAL.TransactionRollback();
                err = ex.Message;
            }

            return resutl;
        }

        //Tharaka 2015-03-11
        public List<MST_COST_CATE> GET_COST_CATE(string Com, string PC)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.GET_COST_CATE(Com, PC);
        }
        public List<GEN_CUST_ENQ> GET_ENQRY_BY_PC_STATUS_NEW(string Com, string PC, String Status, string UserID, Int32 PermissionCode)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.GET_ENQRY_BY_PC_STATUS_NEW(Com, PC, Status);
        }
        //Tharaka 2015-03-16
        public Int32 Save_QUO_COST_HDR(QUO_COST_HDR lst, out string err)
        {
            int resutl = 0;
            err = string.Empty;

            try
            {
                _ToursDAL = new ToursDAL();
                _ToursDAL.ConnectionOpen();
                _ToursDAL.BeginTransaction();

                resutl = _ToursDAL.Save_QUO_COST_HDR(lst);

                _ToursDAL.TransactionCommit();
                _ToursDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                _ToursDAL.TransactionRollback();
                err = ex.Message;
            }

            return resutl;
        }

        //Tharaka 2015-03-17
        public Int32 SaveCostingSheet(QUO_COST_HDR oHeader, List<QUO_COST_DET> oItems, MasterAutoNumber _auto, out string err)
        {
            err = string.Empty;
            int resutl = 0;
            err = string.Empty;

            try
            {
                _ToursDAL = new ToursDAL();
                _ToursDAL.ConnectionOpen();
                _inventoryDAL = new InventoryDAL();
                _inventoryDAL.ConnectionOpen();
                _inventoryDAL.BeginTransaction();
                _ToursDAL.BeginTransaction();
                resutl = _ToursDAL.UPDATE_COST_DET_STATUS(0, oHeader.QCH_SEQ, oHeader.QCH_COST_NO, oHeader.QCH_CRE_BY);

                err = oHeader.QCH_COST_NO;

                if (oHeader.QCH_SEQ == 0)
                {
                    Int32 seqNumber = _ToursDAL.SP_GETCOSTSEQ();
                    oHeader.QCH_SEQ = seqNumber;

                    MasterAutoNumber _number = _inventoryDAL.GetAutoNumber(_auto.Aut_moduleid, (short)_auto.Aut_direction, _auto.Aut_start_char, _auto.Aut_cate_tp, _auto.Aut_cate_cd, _auto.Aut_modify_dt, _auto.Aut_year);
                    String NewCostNumber = oHeader.QCH_COM + "/" + oHeader.QCH_SBU + "/" + _auto.Aut_cate_cd + "-" + _auto.Aut_start_char + "-" + _number.Aut_number.ToString("000000", CultureInfo.InvariantCulture);
                    _inventoryDAL.UpdateAutoNumber(_auto);
                    oHeader.QCH_COST_NO = NewCostNumber;
                    err = NewCostNumber;

                    foreach (QUO_COST_DET item in oItems)
                    {
                        item.QCD_COST_NO = NewCostNumber;
                        item.QCD_SEQ = seqNumber;
                    }
                }

                resutl = _ToursDAL.Save_QUO_COST_HDR(oHeader);
                if (resutl > 0)
                {
                    foreach (QUO_COST_DET item in oItems)
                    {
                        item.QCD_SEQ = oHeader.QCH_SEQ;
                        item.QCD_COST_NO = oHeader.QCH_COST_NO;
                        if (item.QCD_SUB_CATE != "TOTAL")
                        {
                            resutl = _ToursDAL.SAVE_COST_DET(item);
                        }
                    }

                    resutl = _ToursDAL.UpdateEnquiryStage(2, oHeader.QCH_CRE_BY, oHeader.QCH_OTH_DOC, oHeader.QCH_COM, oHeader.QCH_SBU);
                    GEN_ENQLOG oLog = new GEN_ENQLOG();
                    oLog.GEL_ENQ_ID = oHeader.QCH_OTH_DOC;
                    oLog.GEL_USR = oHeader.QCH_CRE_BY;
                    oLog.GEL_STAGE = 2;
                    oLog.GEL_LOGWHEN = DateTime.Now;
                    resutl = _ToursDAL.SAVE_GEN_ENQLOG(oLog);
                }

                _ToursDAL.TransactionCommit();
                _ToursDAL.ConnectionClose();

                _inventoryDAL.TransactionCommit();
                _inventoryDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                _ToursDAL.TransactionRollback();
                _inventoryDAL.TransactionRollback();
                err = ex.Message;
            }

            return resutl;
        }

        //Tharaka 2015-03-17
        public Int32 getCostSheetDetails(string Com, string PC, string enquiryID, string stages, out QUO_COST_HDR oHeader, out  List<QUO_COST_DET> oDetails, out String err)
        {
            Int32 result = 0;
            err = string.Empty;

            oHeader = new QUO_COST_HDR();
            oDetails = new List<QUO_COST_DET>();
            try
            {
                _ToursDAL = new ToursDAL();
                oHeader = _ToursDAL.GetCostSheetHeaderByEnquiryID(Com, PC, enquiryID, stages);

                if (oHeader != null && oHeader.QCH_COST_NO != null)
                {
                    oDetails = _ToursDAL.GetCostSheetDetailBySeq(oHeader.QCH_SEQ);
                }
                result = 1;
            }
            catch (Exception ex)
            {
                result = 0;
                err = ex.Message;
            }

            return result;
        }

        //Tharaka 2015-03-23
        public Int32 SaveToursrInvoice(InvoiceHeader _invoiceHeader, List<InvoiceItem> _invoiceItem, List<InvoiceSerial> _invoiceSerial, RecieptHeader _recieptHeader, List<RecieptItem> _recieptItem, InventoryHeader _inventoryHeader, List<ReptPickSerials> _pickSerial, List<ReptPickSerialsSub> _pickSubSerial, MasterAutoNumber _invoiceAuto, MasterAutoNumber _recieptAuto, MasterAutoNumber _inventoryAuto, bool _isDeliveryNow, out  string _invoiceNo, out string _receiptNo, out string _deliveryOrder, MasterBusinessEntity _businessCompany, bool _isHold, bool _isHoldInvoiceProcess, out string _errorlist, List<InvoiceVoucher> _voucher, InventoryHeader _buybackheader, MasterAutoNumber _buybackauto, List<ReptPickSerials> _buybacklist, out string BuyBackInvNo, bool isTranInvoice, GEN_CUST_ENQSER _genCustEnqser = null, GEN_CUST_ENQ oItem = null, MasterAutoNumber _enqAuto = null, List<MST_ST_PAX_DET> paxDetList = null, bool Tours = true, MasterBusinessEntity cus = null, GroupBussinessEntity _custGroup = null, List<ST_ENQ_CHARGES> enqChrgItems = null)
        {
            string _invNo = string.Empty;
            string _recNo = string.Empty;
            string _DONo = string.Empty;
            string _buybackinv = string.Empty;
            Int32 _effect = 0;
            string _location = string.Empty;
            string _error = string.Empty;
            string _db = string.Empty;
            MasterAutoNumber _auto = null;
            bool _VoucherPromotion = false;
            Boolean _isNew = false;
            string enqMsg = string.Empty;
            //#region Check promotion voucher exist for invoice items
            //_inventoryDAL = new InventoryDAL();
            //_inventoryDAL.ConnectionOpen();
            //foreach (InvoiceItem _itm in _invoiceItem)
            //{
            //    MasterItem _mitm = _inventoryDAL.GetItem(_invoiceHeader.Sah_com, _itm.Sad_itm_cd);
            //    if (_mitm.Mi_is_ser1 != -1)
            //    {
            //        for (int i = 1; i <= _itm.Sad_qty; i++)
            //        {
            //            List<PromoVoucherDefinition> _proVouList = new List<PromoVoucherDefinition>();
            //            _proVouList = GetPromotionalVouchersDefinition(_invoiceHeader.Sah_com, _invoiceHeader.Sah_inv_tp, _invoiceHeader.Sah_pc, _invoiceHeader.Sah_dt.Date, _itm.Sad_pbook, _itm.Sad_pb_lvl, _mitm.Mi_brand, _mitm.Mi_cate_1, _mitm.Mi_cate_2, _itm.Sad_itm_cd, true);
            //            if (_proVouList != null && _proVouList.Count > 0) _VoucherPromotion = true;
            //        }
            //    }

            //    //kapila 19/1/2015
            //    if (!string.IsNullOrEmpty(_itm.Sad_job_no))
            //        _salesDAL.UPDATE_SCV_CONF_HDR(1, _itm.Sad_job_no);
            //}
            //_inventoryDAL.ConnectionClose();
            //#endregion
            //using (TransactionScope _tr = new TransactionScope(TransactionScopeOption.RequiresNew))
            // {
            try
            {
                // try
                //  {
                _db = DataBase._ems;
                _salesDAL = new SalesDAL();
                _salesDAL.ConnectionOpen();
                _salesDAL.BeginTransaction();
                _db = DataBase._ems;
                _inventoryDAL = new InventoryDAL();
                _inventoryDAL.ConnectionOpen();
                _inventoryDAL.BeginTransaction();
                _db = DataBase._fms;
                _fmsInventoryDal = new FMS_InventoryDAL();
                _fmsInventoryDal.ConnectionOpen();
                _fmsInventoryDal.BeginTransaction();
                _db = DataBase._reportdb;
                _inventoryRepDAL = new ReptCommonDAL();
                _inventoryRepDAL.ConnectionOpen();
                _inventoryRepDAL.BeginTransaction();
                _db = DataBase._ems;
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                _db = DataBase._ems;
                _ToursDAL = new ToursDAL();
                _ToursDAL.ConnectionOpen();
                _ToursDAL.BeginTransaction();

                //  }
                //   catch { _invoiceNo = string.Empty; _receiptNo = string.Empty; _deliveryOrder = string.Empty; _errorlist = "Database" + _db + " is not responding. Please contact IT Operation."; BuyBackInvNo = _buybackinv; _effect = -1; return _effect; }

                //Transaction tx = Transaction.Current;
                //_salesDAL.EnlistTransaction(tx);
                //_inventoryDAL.EnlistTransaction(tx);
                //_fmsInventoryDal.EnlistTransaction(tx);
                //// _inventoryRepDAL.EnlistTransaction(tx);
                //_generalDAL.EnlistTransaction(tx);
                int effect = 0;
                if (cus != null && cus.Mbe_name != null && _custGroup.Mbg_name != null)
                {
                    string customerCD = (oItem != null && oItem.GCE_CUS_CD != null) ? oItem.GCE_CUS_CD : "";
                    MasterAutoNumber _cusauto = new MasterAutoNumber();
                    //_auto.Aut_cate_cd = _businessEntity.Mbe_cre_pc;//_invoiceHeader.Sah_pc;
                    //_auto.Aut_cate_tp = "PRO";
                    _cusauto.Aut_moduleid = "CUS";
                    _cusauto.Aut_number = 0;
                    _cusauto.Aut_start_char = "CONT";

                    if (customerCD == "")
                    {
                        MasterAutoNumber _number = _inventoryDAL.GetAutoNumber(_cusauto.Aut_moduleid, _cusauto.Aut_direction, _cusauto.Aut_start_char, _cusauto.Aut_cate_tp, _cusauto.Aut_cate_cd, _cusauto.Aut_modify_dt, _cusauto.Aut_year);
                        // MasterAutoNumber _number = _inventoryDAL.GetAutoNumber(_auto.Aut_moduleid, _auto.Aut_direction, _auto.Aut_start_char, _auto.Aut_cate_tp, null, _auto.Aut_modify_dt, _auto.Aut_year);
                        string _cusNo = _cusauto.Aut_start_char + "-" + _number.Aut_number.ToString("000000", CultureInfo.InvariantCulture);
                        _inventoryDAL.UpdateAutoNumber(_cusauto);
                        cus.Mbe_cd = _cusNo;
                        _custGroup.Mbg_cd = _cusNo;

                        customerCD = _cusNo;
                        effect = _salesDAL.SaveBusinessEntityDetailGroup(_custGroup);
                        effect = _salesDAL.SaveBusinessEntityDetail(cus);

                        oItem.GCE_CUS_CD = customerCD;
                    }
                    else if (cus.Mbe_cd == null)
                    {
                        MasterAutoNumber _number = _inventoryDAL.GetAutoNumber(_cusauto.Aut_moduleid, _cusauto.Aut_direction, _cusauto.Aut_start_char, _cusauto.Aut_cate_tp, _cusauto.Aut_cate_cd, _cusauto.Aut_modify_dt, _cusauto.Aut_year);
                        // MasterAutoNumber _number = _inventoryDAL.GetAutoNumber(_auto.Aut_moduleid, _auto.Aut_direction, _auto.Aut_start_char, _auto.Aut_cate_tp, null, _auto.Aut_modify_dt, _auto.Aut_year);
                        string _cusNo = _cusauto.Aut_start_char + "-" + _number.Aut_number.ToString("000000", CultureInfo.InvariantCulture);
                        _inventoryDAL.UpdateAutoNumber(_cusauto);
                        cus.Mbe_cd = _cusNo;
                        _custGroup.Mbg_cd = _cusNo;

                        customerCD = _cusNo;
                        effect = _salesDAL.SaveBusinessEntityDetailGroup(_custGroup);
                        effect = _salesDAL.SaveBusinessEntityDetail(cus);

                        oItem.GCE_CONT_CD = customerCD;

                    }


                    if (_invoiceHeader != null)
                    {
                        _invoiceHeader.Sah_cus_add1 = cus.Mbe_add1;
                        _invoiceHeader.Sah_cus_add2 = cus.Mbe_add2;
                        _invoiceHeader.Sah_cus_cd = customerCD;
                        _invoiceHeader.Sah_cus_name = cus.Mbe_name;
                    }
                    if (_recieptHeader != null)
                    {
                        _recieptHeader.Sar_debtor_cd = customerCD;
                        _recieptHeader.Sar_debtor_name = cus.Mbe_name;
                        _recieptHeader.Sar_debtor_add_1 = cus.Mbe_add1;
                        _recieptHeader.Sar_debtor_add_2 = cus.Mbe_add2;
                    }

                }
                if (oItem != null)
                {
                    if (String.IsNullOrEmpty(oItem.GCE_ENQ_ID) && oItem.GCE_SEQ == 0)
                    {
                        MasterAutoNumber _reversInv = _inventoryDAL.GetAutoNumber(_enqAuto.Aut_moduleid, _enqAuto.Aut_direction, _enqAuto.Aut_start_char, _enqAuto.Aut_cate_tp, _enqAuto.Aut_cate_cd, _enqAuto.Aut_modify_dt, _enqAuto.Aut_year);
                        _reversInv.Aut_direction = null;
                        _reversInv.Aut_modify_dt = null;
                        string enqno = oItem.GCE_COM + "/" + _reversInv.Aut_cate_cd + "/" + _reversInv.Aut_number.ToString("00000", CultureInfo.InvariantCulture) + "/" + oItem.GCE_FRM_TN + "/" + Convert.ToString(DateTime.Now.Date.Month) + "/" + Convert.ToString(DateTime.Now.Date.Year).Remove(0, 2);
                        _inventoryDAL.UpdateAutoNumber(_reversInv);
                        oItem.GCE_ENQ_ID = enqno;
                        oItem.GCE_CONT_CD = cus.Mbe_cd;
                        _isNew = true;
                        string trcode = oItem.GCE_PC + "-" + "TR" + Convert.ToString(DateTime.Now.Date.Year).Remove(0, 2) + _reversInv.Aut_number.ToString("00000", CultureInfo.InvariantCulture);
                        oItem.GCE_TR_CODE = trcode;
                    }

                    _effect = _ToursDAL.Save_GEN_CUST_ENQ(oItem);

                    if (_isNew == true)
                    {
                        enqMsg = "Successfully Created , Enquiry ID  " + oItem.GCE_ENQ_ID;
                    }
                    else
                    {
                        enqMsg = "Successfully Updated , Enquiry ID  " + oItem.GCE_ENQ_ID;
                    }
                    if (_invoiceHeader != null)
                        _invoiceHeader.Sah_ref_doc = oItem.GCE_ENQ_ID;
                }
                string isupdate = "true";
                if (!_isHold && _invoiceHeader != null)
                {
                    _invoiceAuto.Aut_year = null;
                    MasterAutoNumber InvoiceAuto = _inventoryDAL.GetAutoNumber(_invoiceAuto.Aut_moduleid, _invoiceAuto.Aut_direction, _invoiceAuto.Aut_start_char, _invoiceAuto.Aut_cate_tp, _invoiceAuto.Aut_cate_cd, _invoiceAuto.Aut_modify_dt, _invoiceAuto.Aut_year);
                    _invNo = _invoiceAuto.Aut_start_char + InvoiceAuto.Aut_number.ToString("00000", CultureInfo.InvariantCulture);
                    _invoiceAuto.Aut_year = null;
                    _invoiceAuto.Aut_modify_dt = null;
                    _salesDAL.UpdateInvoiceAutoNumber(_invoiceAuto);
                    _invoiceHeader.Sah_inv_no = _invNo;
                    isupdate = "false";
                }

                _db = string.Empty;
                _location = (_inventoryHeader != null && !string.IsNullOrEmpty(_inventoryHeader.Ith_com)) ? _inventoryHeader.Ith_loc : string.Empty;
                string _recieptSeq = null;
                string _invoiceSeq = null;
                InventoryHeader _invHdr = null;
                DataTable _dataTable = null;
                string invNo = (_invNo != "") ? _invNo : "";

                if (_invoiceHeader != null || _recieptHeader != null)
                {
                    CommonSaveInvoiceWithDeliveryOrderWithTransaction(_invoiceHeader, _invoiceItem, _invoiceSerial, _recieptHeader, _recieptItem, _inventoryHeader, _pickSerial, _pickSubSerial, _invoiceAuto, _recieptAuto, _inventoryAuto, _isDeliveryNow, out _invNo, out _recNo, out _DONo, _inventoryDAL, _salesDAL, _inventoryRepDAL, _isHold, _isHoldInvoiceProcess, out _error, false, out _invoiceSeq, out _recieptSeq, out _invHdr, out _dataTable, invNo);
                }
                if (_genCustEnqser != null)
                {
                    _genCustEnqser.GCS_SEQ = (_invoiceSeq != null) ? Convert.ToInt32(_invoiceSeq) : 0;
                    _genCustEnqser.GCS_ENQ_ID = oItem.GCE_ENQ_ID;
                    _ToursDAL.SAVE_GEN_ENQSER(_genCustEnqser);
                }
                Int32 result = 0;
                if (_invoiceHeader != null)
                {
                    result = _ToursDAL.UpdateEnquiryStage(5, _invoiceHeader.Sah_cre_by, _invoiceHeader.Sah_ref_doc, _invoiceHeader.Sah_com, _invoiceHeader.Sah_pc);
                }
                else
                {
                    if (oItem != null)
                        result = _ToursDAL.UpdateEnquiryStage(5, oItem.GCE_CRE_BY, oItem.GCE_ENQ_ID, oItem.GCE_COM, oItem.GCE_ENQ_PC);
                }

                GEN_ENQLOG oLog = new GEN_ENQLOG();
                oLog.GEL_ENQ_ID = (_invoiceHeader != null) ? _invoiceHeader.Sah_ref_doc : (oItem != null) ? oItem.GCE_ENQ_ID : "";
                oLog.GEL_USR = (_invoiceHeader != null) ? _invoiceHeader.Sah_cre_by : (oItem != null) ? oItem.GCE_CRE_BY : "";
                oLog.GEL_STAGE = 5;
                oLog.GEL_LOGWHEN = DateTime.Now;
                result = _ToursDAL.SAVE_GEN_ENQLOG(oLog);

                //2015-06-26
                if (isTranInvoice && _invoiceHeader != null)
                {
                    foreach (InvoiceItem item in _invoiceItem)
                    {
                        result = _ToursDAL.UPDATE_LOG_HDR_INVOICE(Convert.ToInt32(item.Sad_promo_cd), 1, _invoiceHeader.Sah_cre_by);
                    }
                }
                //SAVE ENQ CHARGES
                if (enqChrgItems != null && enqChrgItems.Count > 0)
                {
                    if (enqChrgItems[0].SCH_ENQ_NO != null)
                    {
                        result = _ToursDAL.deleteEnquiryCharges(enqChrgItems[0].SCH_ENQ_NO);
                    }

                    foreach (ST_ENQ_CHARGES enqChg in enqChrgItems)
                    {
                        enqChg.SCH_SEQ_NO = oItem.GCE_SEQ;
                        if (invNo != "" && enqChg.SCH_INVOICED == 0)
                        {
                            enqChg.SCH_INVOICED_NO = invNo;
                            enqChg.SCH_INVOICED = 1;
                        }
                        else
                        {
                            enqChg.SCH_INVOICED_NO = enqChg.SCH_INVOICED_NO;
                            enqChg.SCH_INVOICED = enqChg.SCH_INVOICED;
                        }
                        enqChg.SCH_ENQ_NO = oItem.GCE_ENQ_ID;
                        result = _ToursDAL.saveEnquiryCharges(enqChg);
                    }

                }
                //UPDATE ENQ CHARGES - only goes multiple enquiries
                if (_invoiceItem != null)
                {
                    foreach (var chg in _invoiceItem)
                    {
                        ST_ENQ_CHARGES ob = new ST_ENQ_CHARGES();
                        ob.SCH_ITM_CD = chg.Sad_itm_cd;
                        ob.SCH_QTY = chg.Sad_qty;
                        ob.SCH_ENQ_NO = chg.Sad_warr_remarks;
                        ob.SCH_INVOICED = 1;
                        ob.SCH_INVOICED_NO = chg.Sad_inv_no;
                        result = _ToursDAL.UpdateEnqChargesInvoiced(ob);
                    }

                }



                string _customerCode = (_invoiceHeader != null) ? _invoiceHeader.Sah_cus_cd : (oItem != null) ? oItem.GCE_CUS_CD : "";
                GroupBussinessEntity _businessEntityGrup = new GroupBussinessEntity();
                if (string.IsNullOrEmpty(_error) || oItem != null || oItem.GCE_ENQ_ID != "")
                {
                    #region Customer Creation

                    //if (_invoiceHeader.Sah_cus_cd == "CASH" && (!string.IsNullOrEmpty(_businessCompany.Mbe_nic) || !string.IsNullOrEmpty(_businessCompany.Mbe_mob)))
                    //{
                    //    // MasterBusinessEntity _nic = _salesDAL.GetActiveBusinessCompanyDetail(_invoiceHeader.Sah_com, string.Empty, _businessCompany.Mbe_nic, string.Empty, "C");
                    //    // MasterBusinessEntity _mobile = _salesDAL.GetActiveBusinessCompanyDetail(_invoiceHeader.Sah_com, string.Empty, string.Empty, _businessCompany.Mbe_mob, "C");
                    //    // if (_nic.Mbe_cd == null && _mobile.Mbe_cd == null)
                    //    {
                    //        _businessEntityGrup = new GroupBussinessEntity();
                    //        _businessEntityGrup.Mbg_act = true;
                    //        _businessEntityGrup.Mbg_add1 = _businessCompany.Mbe_add1;
                    //        _businessEntityGrup.Mbg_add2 = _businessCompany.Mbe_add2;
                    //        _businessEntityGrup.Mbg_cd = "c1";
                    //        _businessEntityGrup.Mbg_contact = string.Empty;
                    //        _businessEntityGrup.Mbg_email = string.Empty;
                    //        _businessEntityGrup.Mbg_fax = string.Empty;
                    //        _businessEntityGrup.Mbg_mob = _businessCompany.Mbe_mob;
                    //        _businessEntityGrup.Mbg_name = _businessCompany.Mbe_name;
                    //        _businessEntityGrup.Mbg_nic = _businessCompany.Mbe_nic;
                    //        _businessEntityGrup.Mbg_tel = string.Empty;
                    //        _businessEntityGrup.Mbg_tit = _businessCompany.MBE_TIT;
                    //        _businessEntityGrup.Mbg_nationality = "SL";
                    //        _businessEntityGrup.Mbg_cre_by = _invoiceHeader.Sah_cre_by;
                    //        _businessEntityGrup.Mbg_mod_by = _invoiceHeader.Sah_mod_by;

                    //        //new customer
                    //        _auto = new MasterAutoNumber();
                    //        _auto.Aut_cate_cd = string.Empty;
                    //        _auto.Aut_cate_tp = string.Empty;
                    //        _auto.Aut_moduleid = "CUS";
                    //        _auto.Aut_number = 0;
                    //        _auto.Aut_start_char = "CONT";

                    //    nxt1:
                    //        _auto.Aut_year = null;
                    //        MasterAutoNumber _number = _inventoryDAL.GetAutoNumber(_auto.Aut_moduleid, _auto.Aut_direction, _auto.Aut_start_char, _auto.Aut_cate_tp, _auto.Aut_cate_cd, _auto.Aut_modify_dt, _auto.Aut_year);
                    //        _customerCode = _auto.Aut_start_char + "-" + _number.Aut_number.ToString("000000", CultureInfo.InvariantCulture);

                    //        if (_salesDAL.CheckSalesNo("sp_getcustomer", "p_customer", _customerCode) == 1)
                    //        {
                    //            goto nxt1;
                    //        }
                    //        _businessCompany.Mbe_cd = _customerCode;
                    //        _businessEntityGrup.Mbg_cd = _customerCode;
                    //        _invoiceHeader.Sah_cus_cd = _customerCode;
                    //        _salesDAL.SaveBusinessEntityDetailGroup(_businessEntityGrup);
                    //        _salesDAL.SaveBusinessEntityDetail(_businessCompany);
                    //        _salesDAL.UpdateInvoiceforNewCustomer(_invoiceHeader.Sah_com, _invoiceHeader.Sah_pc, _invoiceHeader.Sah_seq_no, _customerCode);
                    //        _salesDAL.UpdateInventoryCustomer(_inventoryHeader.Ith_seq_no, _customerCode);
                    //    }

                    //}
                    //else if (_invoiceHeader.Sah_cus_cd != "CASH")
                    //{
                    //    MasterBusinessEntity _chkList = new MasterBusinessEntity();
                    //    _chkList = _salesDAL.GetCustomerProfileByCom(_invoiceHeader.Sah_cus_cd, null, null, null, null, _invoiceHeader.Sah_com);

                    //    if (_chkList.Mbe_cd == null)
                    //    {
                    //        _businessEntityGrup = new GroupBussinessEntity();
                    //        _businessEntityGrup.Mbg_act = true;
                    //        _businessEntityGrup.Mbg_add1 = _businessCompany.Mbe_add1;
                    //        _businessEntityGrup.Mbg_add2 = _businessCompany.Mbe_add2;
                    //        _businessEntityGrup.Mbg_cd = _invoiceHeader.Sah_cus_cd;
                    //        _businessEntityGrup.Mbg_contact = string.Empty;
                    //        _businessEntityGrup.Mbg_email = string.Empty;
                    //        _businessEntityGrup.Mbg_fax = string.Empty;
                    //        _businessEntityGrup.Mbg_mob = _businessCompany.Mbe_mob;
                    //        _businessEntityGrup.Mbg_name = _businessCompany.Mbe_name;
                    //        _businessEntityGrup.Mbg_nic = _businessCompany.Mbe_nic;
                    //        _businessEntityGrup.Mbg_tel = string.Empty;
                    //        _businessEntityGrup.Mbg_nationality = "SL";
                    //        _businessEntityGrup.Mbg_tit = _businessCompany.MBE_TIT;
                    //        _businessEntityGrup.Mbg_cre_by = _invoiceHeader.Sah_cre_by;
                    //        _businessEntityGrup.Mbg_mod_by = _invoiceHeader.Sah_mod_by;

                    //        _businessCompany.Mbe_cd = _invoiceHeader.Sah_cus_cd;
                    //        _salesDAL.SaveBusinessEntityDetailGroup(_businessEntityGrup);
                    //        _salesDAL.SaveBusinessEntityDetail(_businessCompany);

                    //    }
                    //    else
                    //    {
                    //        GroupBussinessEntity _grupList = new GroupBussinessEntity();
                    //        _grupList = _salesDAL.GetCustomerProfileByGrup(_invoiceHeader.Sah_cus_cd, null, null, null, null, null);

                    //        if (_grupList.Mbg_cd == null)
                    //        {
                    //            _businessEntityGrup = new GroupBussinessEntity();
                    //            _businessEntityGrup.Mbg_act = true;
                    //            _businessEntityGrup.Mbg_add1 = _businessCompany.Mbe_add1;
                    //            _businessEntityGrup.Mbg_add2 = _businessCompany.Mbe_add2;
                    //            _businessEntityGrup.Mbg_cd = _invoiceHeader.Sah_cus_cd;
                    //            _businessEntityGrup.Mbg_contact = string.Empty;
                    //            _businessEntityGrup.Mbg_email = string.Empty;
                    //            _businessEntityGrup.Mbg_fax = string.Empty;
                    //            _businessEntityGrup.Mbg_mob = _businessCompany.Mbe_mob;
                    //            _businessEntityGrup.Mbg_name = _businessCompany.Mbe_name;
                    //            _businessEntityGrup.Mbg_nic = _businessCompany.Mbe_nic;
                    //            _businessEntityGrup.Mbg_tel = string.Empty;
                    //            _businessEntityGrup.Mbg_nationality = "SL";
                    //            _businessEntityGrup.Mbg_tit = _businessCompany.MBE_TIT;
                    //            _businessEntityGrup.Mbg_cre_by = _invoiceHeader.Sah_cre_by;
                    //            _businessEntityGrup.Mbg_mod_by = _invoiceHeader.Sah_mod_by;
                    //            _salesDAL.SaveBusinessEntityDetailGroup(_businessEntityGrup);
                    //        }
                    //    }

                    //}

                    #endregion Customer Creation

                    #region update auto no

                    if (!_isHold)
                    {
                        //nxt1:
                        /*_invoiceAuto.Aut_year = null;
                        MasterAutoNumber InvoiceAuto = _inventoryDAL.GetAutoNumber(_invoiceAuto.Aut_moduleid, _invoiceAuto.Aut_direction, _invoiceAuto.Aut_start_char, _invoiceAuto.Aut_cate_tp, _invoiceAuto.Aut_cate_cd, _invoiceAuto.Aut_modify_dt, _invoiceAuto.Aut_year);
                        _invNo = _invoiceAuto.Aut_start_char + InvoiceAuto.Aut_number.ToString("00000", CultureInfo.InvariantCulture);

                        _invoiceAuto.Aut_year = null;
                        _invoiceAuto.Aut_modify_dt = null;
                        if (_salesDAL.CheckSalesNo("sp_getinvno", "p_inv_no", _invNo) == 1)
                        {
                            //_salesDAL.UpdateInvoiceAutoNumber(_invoiceAuto);
                            //goto nxt1;
                            _error = "Invoice process terminated. Please re-process.(Hint - Duplicating Invoice No)";
                            _invoiceNo = string.Empty;
                            _receiptNo = string.Empty;
                            _deliveryOrder = string.Empty;
                            _errorlist = _error;
                            BuyBackInvNo = string.Empty;
                            _effect = -1;

                            _salesDAL.TransactionRollback();
                            _inventoryDAL.TransactionRollback();
                            _fmsInventoryDal.TransactionRollback();
                            _inventoryRepDAL.TransactionRollback();
                            _generalDAL.TransactionRollback();
                            return _effect;
                        }

                        _salesDAL.UpdateInvoiceAutoNumber(_invoiceAuto);*/

                        #region Buyback Item

                        //if (_buybacklist != null) if (_buybacklist.Count > 0)
                        //{
                        //    InventoryBLL _bll = new InventoryBLL();
                        //    _bll._salesDAL = _salesDAL;
                        //    _bll._inventoryDAL = _inventoryDAL;
                        //    _bll._FMSinventoryDAL = _fmsInventoryDal;
                        //    _bll._inventoryRepDAL = _inventoryRepDAL;
                        //    _buybackheader.Ith_manual_ref = _invNo;
                        //    _bll.SaveInwardScanSerial(_buybackheader, _buybacklist, null);
                        //    _bll.UpdateInventoryAutoNumber(_buybackheader, _buybackauto, "+", out  _buybackinv);
                        //    _inventoryDAL.UpdateMovementDocNo_Other(_buybackheader.Ith_seq_no, _buybackinv);
                        //}

                        #endregion Buyback Item

                        //#region Generate Promotion Vouchers :: Chamal 26-Jun-2014
                        //if (_voucher == null) _voucher = new List<InvoiceVoucher>();

                        //bool _promoVouApplied = false;
                        //if (_VoucherPromotion == true)
                        //{
                        //    foreach (InvoiceItem _itm in _invoiceItem)
                        //    {
                        //        MasterItem _mitm = _inventoryDAL.GetItem(_invoiceHeader.Sah_com, _itm.Sad_itm_cd);
                        //        if (_mitm.Mi_is_ser1 != -1)
                        //        {
                        //            for (int i = 1; i <= _itm.Sad_qty; i++)
                        //            {
                        //                List<PromoVoucherDefinition> _proVouList = GetPromotionalVouchersDefinition(_invoiceHeader.Sah_com, _invoiceHeader.Sah_inv_tp, _invoiceHeader.Sah_pc, _invoiceHeader.Sah_dt.Date, _itm.Sad_pbook, _itm.Sad_pb_lvl, _mitm.Mi_brand, _mitm.Mi_cate_1, _mitm.Mi_cate_2, _itm.Sad_itm_cd, false);
                        //                if (_proVouList != null)
                        //                {
                        //                    if (_proVouList.Count > 0)
                        //                    {
                        //                        foreach (PromoVoucherDefinition _proitm in _proVouList)
                        //                        {
                        //                            InvoiceVoucher _vou = new InvoiceVoucher();
                        //                            GiftVoucherPages _gvou = new GiftVoucherPages();

                        //                            _vou.Stvo_inv_no = _invNo;
                        //                            _vou.Stvo_prefix = _proitm.Spd_vou_cd;
                        //                            _vou.Stvo_bookno = _proitm.Spd_seq;
                        //                            _vou.Stvo_pageno = _salesDAL.GetPromotionVoucherNo();
                        //                            _vou.Stvo_gv_itm = _proitm.Spd_vou_cd;
                        //                            _vou.Stvo_price = _proitm.Spd_disc;
                        //                            _vou.Stvo_itm_cd = _itm.Sad_itm_cd;
                        //                            _vou.Stvo_cre_by = "PRO_VOU";
                        //                            if (_proitm.Spd_disc_isrt == true)
                        //                            {
                        //                                _vou.Stvo_stus = 1;
                        //                                _gvou.Gvp_gv_tp = "RATE";
                        //                            }
                        //                            else
                        //                            {
                        //                                _vou.Stvo_stus = 0;
                        //                                _gvou.Gvp_gv_tp = "VALUE";
                        //                            }

                        //                            _gvou.Gvp_amt = _proitm.Spd_disc;
                        //                            _gvou.Gvp_app_by = _invoiceHeader.Sah_cre_by;
                        //                            _gvou.Gvp_bal_amt = 0;
                        //                            _gvou.Gvp_book = _itm.Sad_itm_line;
                        //                            _gvou.Gvp_can_by = "";
                        //                            _gvou.Gvp_can_dt = DateTime.Now.Date;
                        //                            _gvou.Gvp_com = _invoiceHeader.Sah_com;
                        //                            _gvou.Gvp_cre_by = _invoiceHeader.Sah_cre_by;
                        //                            _gvou.Gvp_cre_dt = DateTime.Now.Date;
                        //                            _gvou.Gvp_cus_add1 = _invoiceHeader.Sah_cus_add1;
                        //                            _gvou.Gvp_cus_add2 = _invoiceHeader.Sah_cus_add2;
                        //                            _gvou.Gvp_cus_cd = _invoiceHeader.Sah_cus_cd;
                        //                            _gvou.Gvp_cus_mob = _businessCompany.Mbe_mob;
                        //                            _gvou.Gvp_cus_name = _invoiceHeader.Sah_cus_name;
                        //                            _gvou.Gvp_from = "-";
                        //                            _gvou.Gvp_gv_cd = _proitm.Spd_vou_cd;
                        //                            _gvou.Gvp_gv_prefix = "P_GV";
                        //                            _gvou.Gvp_is_allow_promo = false;
                        //                            _gvou.Gvp_issu_itm = 0;
                        //                            _gvou.Gvp_issue_by = "";
                        //                            _gvou.Gvp_issue_dt = DateTime.Now.Date;
                        //                            _gvou.Gvp_line = i;
                        //                            _gvou.Gvp_mod_by = "";
                        //                            _gvou.Gvp_mod_dt = DateTime.Now.Date;
                        //                            _gvou.Gvp_noof_itm = 1;
                        //                            _gvou.Gvp_oth_ref = _invNo;// _proitm.Spd_circular_no;
                        //                            _gvou.Gvp_page = _vou.Stvo_pageno;
                        //                            _gvou.Gvp_pc = _invoiceHeader.Sah_pc;
                        //                            _gvou.Gvp_ref = _proitm.Spd_seq.ToString();
                        //                            _gvou.Gvp_stus = "A";
                        //                            _gvou.Gvp_valid_from = _invoiceHeader.Sah_dt.Date;
                        //                            //_gvou.Gvp_valid_to = _invoiceHeader.Sah_dt.Date.AddMonths(_proitm.Spd_period);
                        //                            _gvou.Gvp_valid_to = _invoiceHeader.Sah_dt.Date.AddDays(_proitm.Spd_period); //Chamal 24-09-2014
                        //                            _gvou.Gvp_cus_nic = _businessCompany.Mbe_nic;

                        //                            _voucher.Add(_vou);
                        //                            _fmsInventoryDal.SaveGiftVoucherPages(_gvou);
                        //                            _promoVouApplied = true;
                        //                            break;
                        //                        }
                        //                    }
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                        //if (_promoVouApplied == true)
                        //{
                        //    if (_voucher != null)
                        //    {
                        //        if (_voucher.Count > 0)
                        //        {
                        //            //var _vouProCodes = _voucher.Where(x => x.Stvo_cre_by == "PRO_VOU").Select(x => x.Stvo_prefix).Distinct().ToList();
                        //            var _vouProCodes = _voucher.Where(x => x.Stvo_cre_by == "PRO_VOU").Select(x => x.Stvo_bookno).Distinct().ToList();
                        //            if (_vouProCodes != null && _vouProCodes.Count > 0)
                        //            {
                        //                foreach (var _cd in _vouProCodes)
                        //                {
                        //                    DataTable _dtothCom = _salesDAL.GetProVouAllowCompanies(_invoiceHeader.Sah_com, _cd);

                        //                    if (_dtothCom.Rows.Count > 0)
                        //                    {
                        //                        foreach (DataRow drow in _dtothCom.Rows)
                        //                        {
                        //                            MasterBusinessEntity _othCust = new MasterBusinessEntity();
                        //                            _othCust = _businessCompany;
                        //                            _othCust.Mbe_cd = _customerCode;
                        //                            _othCust.Mbe_com = drow["R_COM"].ToString();
                        //                            if (!string.IsNullOrEmpty(_othCust.Mbe_nic) || !string.IsNullOrEmpty(_othCust.Mbe_mob))
                        //                            {
                        //                                MasterBusinessEntity _nic = _salesDAL.GetActiveBusinessCompanyDetail(_invoiceHeader.Sah_com, string.Empty, _othCust.Mbe_nic, string.Empty, "C");
                        //                                MasterBusinessEntity _mobile = _salesDAL.GetActiveBusinessCompanyDetail(_invoiceHeader.Sah_com, string.Empty, string.Empty, _othCust.Mbe_mob, "C");
                        //                                if (_nic.Mbe_cd == null && _mobile.Mbe_cd == null)
                        //                                {
                        //                                    int _isUpdate = _salesDAL.UpdateBusinessEntityProfile(_othCust, 1);
                        //                                    if (_isUpdate <= 0) _salesDAL.SaveBusinessEntityDetail(_othCust);
                        //                                }
                        //                            }

                        //                        }
                        //                    }

                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                        //#endregion

                        //#region Save/Update Voucher
                        //if (_voucher != null)
                        //    if (_voucher.Count > 0)
                        //    {
                        //        _voucher.ForEach(x => x.Stvo_inv_no = _invNo);
                        //        UpdateInvoiceGiftVoucher(_invoiceHeader.Sah_com, _invoiceHeader.Sah_pc, _customerCode, _invoiceHeader.Sah_cus_name, _invoiceHeader.Sah_d_cust_add1, _invoiceHeader.Sah_d_cust_add2, _businessCompany.Mbe_mob, _invoiceHeader.Sah_dt, _invNo, _invoiceHeader.Sah_cre_by, _voucher, _salesDAL, _fmsInventoryDal);
                        //    }
                        //#endregion

                        //_salesDAL.UpdateInvoiceWithTaxCommission(_invNo, string.Empty, Convert.ToInt32(_invoiceSeq), Convert.ToInt32(_recieptSeq));
                        //string RecieptNo = "";
                        if (_recieptHeader != null)
                        {
                            if (_recieptAuto != null)
                            {
                                //nxt2:
                                _recieptAuto.Aut_year = null;
                                MasterAutoNumber RecieptAuto = _inventoryDAL.GetAutoNumber(_recieptAuto.Aut_moduleid, _recieptAuto.Aut_direction, _recieptAuto.Aut_start_char, _recieptAuto.Aut_cate_tp, _recieptAuto.Aut_cate_cd, _recieptAuto.Aut_modify_dt, _recieptAuto.Aut_year);
                                _receiptNo = _recieptAuto.Aut_cate_cd + "-" + RecieptAuto.Aut_start_char + RecieptAuto.Aut_number.ToString("00000", CultureInfo.InvariantCulture);
                                _recieptAuto.Aut_year = null;
                                _recieptAuto.Aut_modify_dt = null;
                                _recNo = _receiptNo;
                                if (_ToursDAL.CheckTBSSalesNo("SP_GETTBSRECEIPTNO", "p_rept_no", _recNo) == 1)
                                {
                                    // _salesDAL.UpdateInvoiceAutoNumber(_recieptAuto);
                                    // goto nxt2;
                                    _error = "Invoice process terminated. Please re-process.(Hint - Duplicating Receipt No)";
                                    _invoiceNo = string.Empty;
                                    _receiptNo = string.Empty;
                                    _deliveryOrder = string.Empty;
                                    _errorlist = _error;
                                    BuyBackInvNo = string.Empty;
                                    _effect = -1;

                                    _salesDAL.TransactionRollback();
                                    _inventoryDAL.TransactionRollback();
                                    _fmsInventoryDal.TransactionRollback();
                                    _inventoryRepDAL.TransactionRollback();
                                    _generalDAL.TransactionRollback();
                                    _ToursDAL.TransactionRollback();
                                    return _effect;
                                }
                                //_invDAL.UpdateAutoNumber(_recieptAuto);

                                _salesDAL.UpdateInvoiceAutoNumber(_recieptAuto);
                            }

                            if (Tours == true)
                            {
                                _salesDAL.UpdateReceiptTbs(_invNo, _recNo, Convert.ToInt32(_invoiceSeq), Convert.ToInt32(_recieptSeq));
                            }
                            else
                            {
                                _salesDAL.UpdateReceipt(_invNo, _recNo, Convert.ToInt32(_invoiceSeq), Convert.ToInt32(_recieptSeq));
                            }

                            //Update receipt no which allocated by receipt entry as per invoice
                            _inventoryRepDAL.UpdateAdvanceReceiptNofromInvoice(string.Empty, Convert.ToString(_invoiceSeq), _invNo);
                        }
                        //if (_isDeliveryNow)
                        //{
                        //    // string _doc;
                        //    _inventoryDAL.UpdateInventoryAutoNumber(_invHdr, _inventoryAuto, "-", out _DONo);
                        //    MasterAutoNumber _AutoNo = new MasterAutoNumber();
                        //    int _e = 0;
                        //    if (_dataTable.Rows.Count > 0)
                        //    {
                        //        for (int i = 0; i < _dataTable.Rows.Count; i++)
                        //        {
                        //            if (_dataTable.Rows[i]["DocType"].ToString() == "ADJ")
                        //            {
                        //                _AutoNo.Aut_cate_cd = _invHdr.Ith_loc;
                        //                _AutoNo.Aut_moduleid = "ADJ";
                        //                _AutoNo.Aut_direction = null;
                        //                _AutoNo.Aut_start_char = "ADJ";
                        //                _AutoNo.Aut_cate_tp = "LOC";
                        //                //_AutoNo.Aut_year = _inventoryMovementHeader.Ith_doc_date.Year;
                        //                _AutoNo.Aut_year = null;

                        //                Int32 _autoNo = _inventoryDAL.GetAutoNumber(_AutoNo.Aut_moduleid, _AutoNo.Aut_direction, _AutoNo.Aut_start_char, _AutoNo.Aut_cate_tp, _AutoNo.Aut_cate_cd, _AutoNo.Aut_modify_dt, _AutoNo.Aut_year).Aut_number;
                        //                string _documentNo = _invHdr.Ith_loc + "-" + _AutoNo.Aut_start_char + "-" + Convert.ToString(_invHdr.Ith_doc_date.Year).Remove(0, 2) + "-" + _autoNo.ToString("00000", CultureInfo.InvariantCulture);
                        //                _e = _inventoryDAL.UpdateMovementDocNo(Convert.ToInt32(_dataTable.Rows[i]["SeqNo"].ToString()), _documentNo);
                        //                _e = _inventoryDAL.UpdateAutoNumber(_AutoNo);
                        //            }
                        //            else if (_dataTable.Rows[i]["DocType"].ToString() == "GRN")
                        //            {
                        //                _AutoNo.Aut_cate_cd = _invHdr.Ith_loc;
                        //                _AutoNo.Aut_moduleid = "GRN";
                        //                _AutoNo.Aut_direction = null;
                        //                _AutoNo.Aut_start_char = "GRN";
                        //                _AutoNo.Aut_cate_tp = "LOC";
                        //                //_AutoNo.Aut_year = _inventoryMovementHeader.Ith_doc_date.Year;
                        //                _AutoNo.Aut_year = null;

                        //                Int32 _autoNo = _inventoryDAL.GetAutoNumber(_AutoNo.Aut_moduleid, _AutoNo.Aut_direction, _AutoNo.Aut_start_char, _AutoNo.Aut_cate_tp, _AutoNo.Aut_cate_cd, _AutoNo.Aut_modify_dt, _AutoNo.Aut_year).Aut_number;
                        //                string _documentNo = _invHdr.Ith_loc + "-" + _AutoNo.Aut_start_char + "-" + Convert.ToString(_invHdr.Ith_doc_date.Year).Remove(0, 2) + "-" + _autoNo.ToString("00000", CultureInfo.InvariantCulture);
                        //                _e = _inventoryDAL.UpdateMovementDocNo(Convert.ToInt32(_dataTable.Rows[i]["SeqNo"].ToString()), _documentNo);
                        //                _e = _inventoryDAL.UpdateAutoNumber(_AutoNo);
                        //            }
                        //            else if (_dataTable.Rows[i]["DocType"].ToString() == "PO")
                        //            {
                        //                //_AutoNo.Aut_cate_cd = _inventoryMovementHeader.Ith_com;
                        //                //_AutoNo.Aut_moduleid = "PO_LOCAL";
                        //                //_AutoNo.Aut_direction = null;
                        //                //_AutoNo.Aut_start_char = "PO";
                        //                //_AutoNo.Aut_cate_tp = "COM";
                        //                //_AutoNo.Aut_year = null;

                        //                _AutoNo.Aut_cate_cd = _invHdr.Ith_com;
                        //                _AutoNo.Aut_cate_tp = "COM";
                        //                _AutoNo.Aut_direction = null;
                        //                _AutoNo.Aut_modify_dt = null;
                        //                _AutoNo.Aut_moduleid = "PUR";
                        //                _AutoNo.Aut_start_char = "PUR";
                        //                _AutoNo.Aut_year = null;

                        //                Int32 _autoNo = _inventoryDAL.GetAutoNumber(_AutoNo.Aut_moduleid, _AutoNo.Aut_direction, _AutoNo.Aut_start_char, _AutoNo.Aut_cate_tp, _AutoNo.Aut_cate_cd, _AutoNo.Aut_modify_dt, _AutoNo.Aut_year).Aut_number;
                        //                string _documentNo = _AutoNo.Aut_cate_cd + "-" + _AutoNo.Aut_start_char + string.Format("{0:000000}", _autoNo);
                        //                _inventoryDAL.UpdatePODocNo(Convert.ToInt32(_dataTable.Rows[i]["SeqNo"].ToString()), _documentNo);
                        //                _e = _inventoryDAL.UpdateAutoNumber(_AutoNo);
                        //                _inventoryDAL.UpdateGRNPODocNo(Convert.ToInt32(_dataTable.Rows[i]["SeqNo"].ToString()), _documentNo);
                        //            }
                        //        }
                        //    }

                        //    //update inv no
                        //    _inventoryDAL.UpdateOtherDocuments(_DONo, _invNo);
                        //    _inventoryDAL.UpdateBatchRefDoc(_DONo, _invNo);

                        //}
                    }

                    #endregion update auto no

                    #region Update Manual Doc
                    if (_invoiceHeader != null)
                        if (_invoiceHeader.Sah_manual) _fmsInventoryDal.UpdateManualDocNo(_location, "MDOC_INV", Convert.ToInt32(_invoiceHeader.Sah_ref_doc), _invNo);

                    #endregion Update Manual Doc

                    #region update invoice discount / Promotion Voucher page as F
                    if (_invoiceHeader != null)
                    {
                        foreach (InvoiceItem _itm in _invoiceItem)
                        {
                            if (_itm.Sad_dis_type == "P")
                            {
                                _salesDAL.UpdateDiscountUsedTimes(_itm.Sad_dis_seq, 1);
                            }

                            if (_itm.Sad_res_no == "PROMO_VOU" && _itm.Sad_res_line_no > 0)
                            {
                                //Add by Chamal 6-Jul-2014
                                _salesDAL.Update_GV_Pages(1, _invoiceHeader.Sah_com, _invoiceHeader.Sah_pc, _invoiceHeader.Sah_dt.Date, "", "F", _itm.Sad_res_line_no, "P_GV", _invoiceHeader.Sah_cre_by, _invNo);
                            }
                        }


                    #endregion update invoice discount / Promotion Voucher page as F

                        if (paxDetList != null)
                        {
                            Int32 ln = 1;
                            foreach (MST_ST_PAX_DET pax in paxDetList)
                            {
                                pax.SPD_SEQ = Convert.ToInt32(_invoiceSeq);
                                pax.SPD_LINE = ln;
                                pax.SPD_INV_NO = _invNo;
                                pax.SPD_ENQ_ID = _invoiceHeader.Sah_ref_doc;
                                Int32 ef = _ToursDAL.saveInvoicePax(pax);
                                ln++;
                            }
                        }


                    }
                    _error = enqMsg + _error;
                    _error = _error.Trim();
                    _effect = 1;
                }
                else
                    _effect = -1;

                try
                {
                    // _db = DataBase._ems; _salesDAL.ConnectionClose(); _db = DataBase._ems; _inventoryDAL.ConnectionClose(); _db = DataBase._fms; _fmsInventoryDal.ConnectionClose(); _db = DataBase._reportdb; _inventoryRepDAL.ConnectionClose(); _db = DataBase._ems; _generalDAL.ConnectionClose();
                    if (string.IsNullOrEmpty(_error) || oItem != null)
                    {
                        _db = DataBase._ems;
                        _salesDAL.TransactionCommit();
                        _db = DataBase._ems;
                        _inventoryDAL.TransactionCommit();
                        _db = DataBase._fms;
                        _fmsInventoryDal.TransactionCommit();
                        _db = DataBase._reportdb;
                        _inventoryRepDAL.TransactionCommit();
                        _db = DataBase._ems;
                        _generalDAL.TransactionCommit();
                        _db = DataBase._ems;
                        _ToursDAL.TransactionCommit();

                        if (_invNo != "")
                            _inventoryDAL.UpdateInvoiceDOStatus(_invNo);

                        //cus code update
                        if (_auto != null)
                        {
                            _inventoryDAL.UpdateAutoNumber(_auto);
                        }
                    }
                    else
                    {
                        _salesDAL.TransactionRollback();
                        _inventoryDAL.TransactionRollback();
                        _fmsInventoryDal.TransactionRollback();
                        _inventoryRepDAL.TransactionRollback();
                        _generalDAL.TransactionRollback();
                        _ToursDAL.TransactionRollback();
                    }
                }
                catch (Exception ex)
                {
                    _invoiceNo = string.Empty;
                    _receiptNo = string.Empty;
                    _deliveryOrder = string.Empty;
                    _errorlist = "Database" + _db + " is not responding. Please contact IT Operation.\n" + ex.Message;
                    BuyBackInvNo = string.Empty;
                    _effect = -1;
                    return _effect;
                }
            }
            catch (Exception ex)
            {
                if (_error == null || _error=="")
                {
                    _error = ex.Message.ToString();
                }
                
                _invoiceNo = string.Empty;
                _receiptNo = string.Empty;
                _deliveryOrder = string.Empty;
                _errorlist = _error;
                BuyBackInvNo = string.Empty;
                _effect = -1;

                _salesDAL.TransactionRollback();
                _inventoryDAL.TransactionRollback();
                _fmsInventoryDal.TransactionRollback();
                _inventoryRepDAL.TransactionRollback();
                _generalDAL.TransactionRollback();
                _ToursDAL.TransactionRollback();
            }

            _invoiceNo = _invNo;
            _receiptNo = _recNo;
            _deliveryOrder = _DONo;
            _errorlist = _error;
            BuyBackInvNo = _buybackinv;
            return _effect;
        }
        public Int32 SaveToursrInvoiceDBNT(InvoiceHeader _invoiceHeader, List<InvoiceItem> _invoiceItem, List<InvoiceSerial> _invoiceSerial, RecieptHeader _recieptHeader, List<RecieptItem> _recieptItem, InventoryHeader _inventoryHeader, List<ReptPickSerials> _pickSerial, List<ReptPickSerialsSub> _pickSubSerial, MasterAutoNumber _invoiceAuto, MasterAutoNumber _recieptAuto, MasterAutoNumber _inventoryAuto, bool _isDeliveryNow, out  string _invoiceNo, out string _receiptNo, out string _deliveryOrder, MasterBusinessEntity _businessCompany, bool _isHold, bool _isHoldInvoiceProcess, out string _errorlist, List<InvoiceVoucher> _voucher, InventoryHeader _buybackheader, MasterAutoNumber _buybackauto, List<ReptPickSerials> _buybacklist, out string BuyBackInvNo, bool isTranInvoice, GEN_CUST_ENQSER _genCustEnqser = null, GEN_CUST_ENQ oItem = null, MasterAutoNumber _enqAuto = null, List<MST_ST_PAX_DET> paxDetList = null, bool Tours = true, MasterBusinessEntity cus = null, GroupBussinessEntity _custGroup = null, List<ST_ENQ_CHARGES> enqChrgItems = null)
        {
            string _invNo = string.Empty;
            string _recNo = string.Empty;
            string _DONo = string.Empty;
            string _buybackinv = string.Empty;
            Int32 _effect = 0;
            string _location = string.Empty;
            string _error = string.Empty;
            string _db = string.Empty;
            MasterAutoNumber _auto = null;
            bool _VoucherPromotion = false;
            Boolean _isNew = false;
            string enqMsg = string.Empty;
            decimal anal8val = _invoiceHeader.Sah_anal_8; ;
            //#region Check promotion voucher exist for invoice items
            //_inventoryDAL = new InventoryDAL();
            //_inventoryDAL.ConnectionOpen();
            //foreach (InvoiceItem _itm in _invoiceItem)
            //{
            //    MasterItem _mitm = _inventoryDAL.GetItem(_invoiceHeader.Sah_com, _itm.Sad_itm_cd);
            //    if (_mitm.Mi_is_ser1 != -1)
            //    {
            //        for (int i = 1; i <= _itm.Sad_qty; i++)
            //        {
            //            List<PromoVoucherDefinition> _proVouList = new List<PromoVoucherDefinition>();
            //            _proVouList = GetPromotionalVouchersDefinition(_invoiceHeader.Sah_com, _invoiceHeader.Sah_inv_tp, _invoiceHeader.Sah_pc, _invoiceHeader.Sah_dt.Date, _itm.Sad_pbook, _itm.Sad_pb_lvl, _mitm.Mi_brand, _mitm.Mi_cate_1, _mitm.Mi_cate_2, _itm.Sad_itm_cd, true);
            //            if (_proVouList != null && _proVouList.Count > 0) _VoucherPromotion = true;
            //        }
            //    }

            //    //kapila 19/1/2015
            //    if (!string.IsNullOrEmpty(_itm.Sad_job_no))
            //        _salesDAL.UPDATE_SCV_CONF_HDR(1, _itm.Sad_job_no);
            //}
            //_inventoryDAL.ConnectionClose();
            //#endregion
            //using (TransactionScope _tr = new TransactionScope(TransactionScopeOption.RequiresNew))
            // {
            try
            {
                // try
                //  {
                _db = DataBase._ems;
                _salesDAL = new SalesDAL();
                _salesDAL.ConnectionOpen();
                _salesDAL.BeginTransaction();
                _db = DataBase._ems;
                _inventoryDAL = new InventoryDAL();
                _inventoryDAL.ConnectionOpen();
                _inventoryDAL.BeginTransaction();
                _db = DataBase._fms;
                _fmsInventoryDal = new FMS_InventoryDAL();
                _fmsInventoryDal.ConnectionOpen();
                _fmsInventoryDal.BeginTransaction();
                _db = DataBase._reportdb;
                _inventoryRepDAL = new ReptCommonDAL();
                _inventoryRepDAL.ConnectionOpen();
                _inventoryRepDAL.BeginTransaction();
                _db = DataBase._ems;
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                _db = DataBase._ems;
                _ToursDAL = new ToursDAL();
                _ToursDAL.ConnectionOpen();
                _ToursDAL.BeginTransaction();

                //  }
                //   catch { _invoiceNo = string.Empty; _receiptNo = string.Empty; _deliveryOrder = string.Empty; _errorlist = "Database" + _db + " is not responding. Please contact IT Operation."; BuyBackInvNo = _buybackinv; _effect = -1; return _effect; }

                //Transaction tx = Transaction.Current;
                //_salesDAL.EnlistTransaction(tx);
                //_inventoryDAL.EnlistTransaction(tx);
                //_fmsInventoryDal.EnlistTransaction(tx);
                //// _inventoryRepDAL.EnlistTransaction(tx);
                //_generalDAL.EnlistTransaction(tx);
                int effect = 0;
                if (cus != null && cus.Mbe_name != null && _custGroup.Mbg_name != null)
                {
                    string customerCD = (oItem != null && oItem.GCE_CUS_CD != null) ? oItem.GCE_CUS_CD : "";
                    MasterAutoNumber _cusauto = new MasterAutoNumber();
                    //_auto.Aut_cate_cd = _businessEntity.Mbe_cre_pc;//_invoiceHeader.Sah_pc;
                    //_auto.Aut_cate_tp = "PRO";
                    _cusauto.Aut_moduleid = "CUS";
                    _cusauto.Aut_number = 0;
                    _cusauto.Aut_start_char = "CONT";

                    if (customerCD == "")
                    {
                        MasterAutoNumber _number = _inventoryDAL.GetAutoNumber(_cusauto.Aut_moduleid, _cusauto.Aut_direction, _cusauto.Aut_start_char, _cusauto.Aut_cate_tp, _cusauto.Aut_cate_cd, _cusauto.Aut_modify_dt, _cusauto.Aut_year);
                        // MasterAutoNumber _number = _inventoryDAL.GetAutoNumber(_auto.Aut_moduleid, _auto.Aut_direction, _auto.Aut_start_char, _auto.Aut_cate_tp, null, _auto.Aut_modify_dt, _auto.Aut_year);
                        string _cusNo = _cusauto.Aut_start_char + "-" + _number.Aut_number.ToString("000000", CultureInfo.InvariantCulture);
                        _inventoryDAL.UpdateAutoNumber(_cusauto);
                        cus.Mbe_cd = _cusNo;
                        _custGroup.Mbg_cd = _cusNo;

                        customerCD = _cusNo;
                        effect = _salesDAL.SaveBusinessEntityDetailGroup(_custGroup);
                        effect = _salesDAL.SaveBusinessEntityDetail(cus);

                        oItem.GCE_CUS_CD = customerCD;
                    }
                    else if (cus.Mbe_cd == null)
                    {
                        MasterAutoNumber _number = _inventoryDAL.GetAutoNumber(_cusauto.Aut_moduleid, _cusauto.Aut_direction, _cusauto.Aut_start_char, _cusauto.Aut_cate_tp, _cusauto.Aut_cate_cd, _cusauto.Aut_modify_dt, _cusauto.Aut_year);
                        // MasterAutoNumber _number = _inventoryDAL.GetAutoNumber(_auto.Aut_moduleid, _auto.Aut_direction, _auto.Aut_start_char, _auto.Aut_cate_tp, null, _auto.Aut_modify_dt, _auto.Aut_year);
                        string _cusNo = _cusauto.Aut_start_char + "-" + _number.Aut_number.ToString("000000", CultureInfo.InvariantCulture);
                        _inventoryDAL.UpdateAutoNumber(_cusauto);
                        cus.Mbe_cd = _cusNo;
                        _custGroup.Mbg_cd = _cusNo;

                        customerCD = _cusNo;
                        effect = _salesDAL.SaveBusinessEntityDetailGroup(_custGroup);
                        effect = _salesDAL.SaveBusinessEntityDetail(cus);

                        oItem.GCE_CONT_CD = customerCD;

                    }


                    if (_invoiceHeader != null)
                    {
                        _invoiceHeader.Sah_cus_add1 = cus.Mbe_add1;
                        _invoiceHeader.Sah_cus_add2 = cus.Mbe_add2;
                        _invoiceHeader.Sah_cus_cd = customerCD;
                        _invoiceHeader.Sah_cus_name = cus.Mbe_name;
                    }
                    if (_recieptHeader != null)
                    {
                        _recieptHeader.Sar_debtor_cd = customerCD;
                        _recieptHeader.Sar_debtor_name = cus.Mbe_name;
                        _recieptHeader.Sar_debtor_add_1 = cus.Mbe_add1;
                        _recieptHeader.Sar_debtor_add_2 = cus.Mbe_add2;
                    }

                }
                if (oItem != null)
                {
                    if (String.IsNullOrEmpty(oItem.GCE_ENQ_ID) && oItem.GCE_SEQ == 0)
                    {
                        MasterAutoNumber _reversInv = _inventoryDAL.GetAutoNumber(_enqAuto.Aut_moduleid, _enqAuto.Aut_direction, _enqAuto.Aut_start_char, _enqAuto.Aut_cate_tp, _enqAuto.Aut_cate_cd, _enqAuto.Aut_modify_dt, _enqAuto.Aut_year);
                        _reversInv.Aut_direction = null;
                        _reversInv.Aut_modify_dt = null;
                        string enqno = oItem.GCE_COM + "/" + _reversInv.Aut_cate_cd + "/" + _reversInv.Aut_number.ToString("00000", CultureInfo.InvariantCulture) + "/" + oItem.GCE_FRM_TN + "/" + Convert.ToString(DateTime.Now.Date.Month) + "/" + Convert.ToString(DateTime.Now.Date.Year).Remove(0, 2);
                        _inventoryDAL.UpdateAutoNumber(_reversInv);
                        oItem.GCE_ENQ_ID = enqno;
                        oItem.GCE_CONT_CD = cus.Mbe_cd;
                        _isNew = true;
                        string trcode = oItem.GCE_PC + "-" + "TR" + Convert.ToString(DateTime.Now.Date.Year).Remove(0, 2) + _reversInv.Aut_number.ToString("00000", CultureInfo.InvariantCulture);
                        oItem.GCE_TR_CODE = trcode;
                    }

                    _effect = _ToursDAL.Save_GEN_CUST_ENQ(oItem);

                    if (_isNew == true)
                    {
                        enqMsg = "Successfully Created , Enquiry ID  " + oItem.GCE_ENQ_ID;
                    }
                    else
                    {
                        enqMsg = "Successfully Updated , Enquiry ID  " + oItem.GCE_ENQ_ID;
                    }
                    if (_invoiceHeader != null)
                        _invoiceHeader.Sah_ref_doc = oItem.GCE_ENQ_ID;
                }
                string isupdate = "true";
                if (!_isHold && _invoiceHeader != null)
                {
                    _invoiceAuto.Aut_year = null;
                    MasterAutoNumber InvoiceAuto = _inventoryDAL.GetAutoNumber(_invoiceAuto.Aut_moduleid, _invoiceAuto.Aut_direction, _invoiceAuto.Aut_start_char, _invoiceAuto.Aut_cate_tp, _invoiceAuto.Aut_cate_cd, _invoiceAuto.Aut_modify_dt, _invoiceAuto.Aut_year);
                    _invNo = _invoiceAuto.Aut_start_char + InvoiceAuto.Aut_number.ToString("00000", CultureInfo.InvariantCulture);
                    _invoiceAuto.Aut_year = null;
                    _invoiceAuto.Aut_modify_dt = null;
                    _salesDAL.UpdateInvoiceAutoNumber(_invoiceAuto);
                    _invoiceHeader.Sah_inv_no = _invNo;
                    isupdate = "false";
                }

                _db = string.Empty;
                _location = (_inventoryHeader != null && !string.IsNullOrEmpty(_inventoryHeader.Ith_com)) ? _inventoryHeader.Ith_loc : string.Empty;
                string _recieptSeq = null;
                string _invoiceSeq = null;
                InventoryHeader _invHdr = null;
                DataTable _dataTable = null;
                string invNo = (_invNo != "") ? _invNo : "";

                if (_invoiceHeader != null || _recieptHeader != null)
                {
                    CommonSaveInvoiceWithDeliveryOrderWithTransaction(_invoiceHeader, _invoiceItem, _invoiceSerial, _recieptHeader, _recieptItem, _inventoryHeader, _pickSerial, _pickSubSerial, _invoiceAuto, _recieptAuto, _inventoryAuto, _isDeliveryNow, out _invNo, out _recNo, out _DONo, _inventoryDAL, _salesDAL, _inventoryRepDAL, _isHold, _isHoldInvoiceProcess, out _error, false, out _invoiceSeq, out _recieptSeq, out _invHdr, out _dataTable, invNo);
                }
                if (_genCustEnqser != null)
                {
                    _genCustEnqser.GCS_SEQ = (_invoiceSeq != null) ? Convert.ToInt32(_invoiceSeq) : 0;
                    _genCustEnqser.GCS_ENQ_ID = oItem.GCE_ENQ_ID;
                    _ToursDAL.SAVE_GEN_ENQSER(_genCustEnqser);
                }
                Int32 result = 0;
                if (_invoiceHeader != null)
                {
                    result = _ToursDAL.UpdateEnquiryStage(5, _invoiceHeader.Sah_cre_by, _invoiceHeader.Sah_ref_doc, _invoiceHeader.Sah_com, _invoiceHeader.Sah_pc);
                }
                else
                {
                    if (oItem != null)
                        result = _ToursDAL.UpdateEnquiryStage(5, oItem.GCE_CRE_BY, oItem.GCE_ENQ_ID, oItem.GCE_COM, oItem.GCE_ENQ_PC);
                }

                GEN_ENQLOG oLog = new GEN_ENQLOG();
                oLog.GEL_ENQ_ID = (_invoiceHeader != null) ? _invoiceHeader.Sah_ref_doc : (oItem != null) ? oItem.GCE_ENQ_ID : "";
                oLog.GEL_USR = (_invoiceHeader != null) ? _invoiceHeader.Sah_cre_by : (oItem != null) ? oItem.GCE_CRE_BY : "";
                oLog.GEL_STAGE = 5;
                oLog.GEL_LOGWHEN = DateTime.Now;
                result = _ToursDAL.SAVE_GEN_ENQLOG(oLog);

                //2015-06-26
                if (isTranInvoice && _invoiceHeader != null)
                {
                    foreach (InvoiceItem item in _invoiceItem)
                    {
                        result = _ToursDAL.UPDATE_LOG_HDR_INVOICE(Convert.ToInt32(item.Sad_promo_cd), 1, _invoiceHeader.Sah_cre_by);
                    }
                }
                //SAVE ENQ CHARGES
                if (enqChrgItems != null && enqChrgItems.Count > 0)
                {
                    if (enqChrgItems[0].SCH_ENQ_NO != null)
                    {
                        result = _ToursDAL.deleteEnquiryCharges(enqChrgItems[0].SCH_ENQ_NO);
                    }

                    foreach (ST_ENQ_CHARGES enqChg in enqChrgItems)
                    {
                        enqChg.SCH_SEQ_NO = oItem.GCE_SEQ;
                        if (invNo != "" && enqChg.SCH_INVOICED == 0)
                        {
                            enqChg.SCH_INVOICED_NO = invNo;
                            enqChg.SCH_INVOICED = 1;
                        }
                        else
                        {
                            enqChg.SCH_INVOICED_NO = enqChg.SCH_INVOICED_NO;
                            enqChg.SCH_INVOICED = enqChg.SCH_INVOICED;
                        }
                        enqChg.SCH_ENQ_NO = oItem.GCE_ENQ_ID;
                        result = _ToursDAL.saveEnquiryCharges(enqChg);
                    }

                }
                //UPDATE ENQ CHARGES - only goes multiple enquiries
                if (_invoiceItem != null)
                {
                    foreach (var chg in _invoiceItem)
                    {
                        ST_ENQ_CHARGES ob = new ST_ENQ_CHARGES();
                        ob.SCH_ITM_CD = chg.Sad_itm_cd;
                        ob.SCH_QTY = chg.Sad_qty;
                        ob.SCH_ENQ_NO = chg.Sad_warr_remarks;
                        ob.SCH_INVOICED = 1;
                        ob.SCH_INVOICED_NO = chg.Sad_inv_no;
                        result = _ToursDAL.UpdateEnqChargesInvoiced(ob);
                    }

                }
                //
                InvoiceHeader _oldinv = new InvoiceHeader();
                _oldinv.Sah_inv_no = _invoiceHeader.With_INV_DBNT;
                _oldinv.Sah_com = _invoiceHeader.Sah_com;
                _oldinv.Sah_pc = _invoiceHeader.Sah_pc;
                _oldinv.Sah_anal_8 = anal8val;
                _oldinv.Sah_mod_by = _invoiceHeader.Sah_mod_by;
                _oldinv.Sah_stus = _invoiceHeader.Sah_stus;
              //  result = _ToursDAL.updateOldTourInvoiceanal8(_oldinv);


                string _customerCode = (_invoiceHeader != null) ? _invoiceHeader.Sah_cus_cd : (oItem != null) ? oItem.GCE_CUS_CD : "";
                GroupBussinessEntity _businessEntityGrup = new GroupBussinessEntity();
                if (string.IsNullOrEmpty(_error) || oItem != null || oItem.GCE_ENQ_ID != "")
                {
                    #region Customer Creation

                    //if (_invoiceHeader.Sah_cus_cd == "CASH" && (!string.IsNullOrEmpty(_businessCompany.Mbe_nic) || !string.IsNullOrEmpty(_businessCompany.Mbe_mob)))
                    //{
                    //    // MasterBusinessEntity _nic = _salesDAL.GetActiveBusinessCompanyDetail(_invoiceHeader.Sah_com, string.Empty, _businessCompany.Mbe_nic, string.Empty, "C");
                    //    // MasterBusinessEntity _mobile = _salesDAL.GetActiveBusinessCompanyDetail(_invoiceHeader.Sah_com, string.Empty, string.Empty, _businessCompany.Mbe_mob, "C");
                    //    // if (_nic.Mbe_cd == null && _mobile.Mbe_cd == null)
                    //    {
                    //        _businessEntityGrup = new GroupBussinessEntity();
                    //        _businessEntityGrup.Mbg_act = true;
                    //        _businessEntityGrup.Mbg_add1 = _businessCompany.Mbe_add1;
                    //        _businessEntityGrup.Mbg_add2 = _businessCompany.Mbe_add2;
                    //        _businessEntityGrup.Mbg_cd = "c1";
                    //        _businessEntityGrup.Mbg_contact = string.Empty;
                    //        _businessEntityGrup.Mbg_email = string.Empty;
                    //        _businessEntityGrup.Mbg_fax = string.Empty;
                    //        _businessEntityGrup.Mbg_mob = _businessCompany.Mbe_mob;
                    //        _businessEntityGrup.Mbg_name = _businessCompany.Mbe_name;
                    //        _businessEntityGrup.Mbg_nic = _businessCompany.Mbe_nic;
                    //        _businessEntityGrup.Mbg_tel = string.Empty;
                    //        _businessEntityGrup.Mbg_tit = _businessCompany.MBE_TIT;
                    //        _businessEntityGrup.Mbg_nationality = "SL";
                    //        _businessEntityGrup.Mbg_cre_by = _invoiceHeader.Sah_cre_by;
                    //        _businessEntityGrup.Mbg_mod_by = _invoiceHeader.Sah_mod_by;

                    //        //new customer
                    //        _auto = new MasterAutoNumber();
                    //        _auto.Aut_cate_cd = string.Empty;
                    //        _auto.Aut_cate_tp = string.Empty;
                    //        _auto.Aut_moduleid = "CUS";
                    //        _auto.Aut_number = 0;
                    //        _auto.Aut_start_char = "CONT";

                    //    nxt1:
                    //        _auto.Aut_year = null;
                    //        MasterAutoNumber _number = _inventoryDAL.GetAutoNumber(_auto.Aut_moduleid, _auto.Aut_direction, _auto.Aut_start_char, _auto.Aut_cate_tp, _auto.Aut_cate_cd, _auto.Aut_modify_dt, _auto.Aut_year);
                    //        _customerCode = _auto.Aut_start_char + "-" + _number.Aut_number.ToString("000000", CultureInfo.InvariantCulture);

                    //        if (_salesDAL.CheckSalesNo("sp_getcustomer", "p_customer", _customerCode) == 1)
                    //        {
                    //            goto nxt1;
                    //        }
                    //        _businessCompany.Mbe_cd = _customerCode;
                    //        _businessEntityGrup.Mbg_cd = _customerCode;
                    //        _invoiceHeader.Sah_cus_cd = _customerCode;
                    //        _salesDAL.SaveBusinessEntityDetailGroup(_businessEntityGrup);
                    //        _salesDAL.SaveBusinessEntityDetail(_businessCompany);
                    //        _salesDAL.UpdateInvoiceforNewCustomer(_invoiceHeader.Sah_com, _invoiceHeader.Sah_pc, _invoiceHeader.Sah_seq_no, _customerCode);
                    //        _salesDAL.UpdateInventoryCustomer(_inventoryHeader.Ith_seq_no, _customerCode);
                    //    }

                    //}
                    //else if (_invoiceHeader.Sah_cus_cd != "CASH")
                    //{
                    //    MasterBusinessEntity _chkList = new MasterBusinessEntity();
                    //    _chkList = _salesDAL.GetCustomerProfileByCom(_invoiceHeader.Sah_cus_cd, null, null, null, null, _invoiceHeader.Sah_com);

                    //    if (_chkList.Mbe_cd == null)
                    //    {
                    //        _businessEntityGrup = new GroupBussinessEntity();
                    //        _businessEntityGrup.Mbg_act = true;
                    //        _businessEntityGrup.Mbg_add1 = _businessCompany.Mbe_add1;
                    //        _businessEntityGrup.Mbg_add2 = _businessCompany.Mbe_add2;
                    //        _businessEntityGrup.Mbg_cd = _invoiceHeader.Sah_cus_cd;
                    //        _businessEntityGrup.Mbg_contact = string.Empty;
                    //        _businessEntityGrup.Mbg_email = string.Empty;
                    //        _businessEntityGrup.Mbg_fax = string.Empty;
                    //        _businessEntityGrup.Mbg_mob = _businessCompany.Mbe_mob;
                    //        _businessEntityGrup.Mbg_name = _businessCompany.Mbe_name;
                    //        _businessEntityGrup.Mbg_nic = _businessCompany.Mbe_nic;
                    //        _businessEntityGrup.Mbg_tel = string.Empty;
                    //        _businessEntityGrup.Mbg_nationality = "SL";
                    //        _businessEntityGrup.Mbg_tit = _businessCompany.MBE_TIT;
                    //        _businessEntityGrup.Mbg_cre_by = _invoiceHeader.Sah_cre_by;
                    //        _businessEntityGrup.Mbg_mod_by = _invoiceHeader.Sah_mod_by;

                    //        _businessCompany.Mbe_cd = _invoiceHeader.Sah_cus_cd;
                    //        _salesDAL.SaveBusinessEntityDetailGroup(_businessEntityGrup);
                    //        _salesDAL.SaveBusinessEntityDetail(_businessCompany);

                    //    }
                    //    else
                    //    {
                    //        GroupBussinessEntity _grupList = new GroupBussinessEntity();
                    //        _grupList = _salesDAL.GetCustomerProfileByGrup(_invoiceHeader.Sah_cus_cd, null, null, null, null, null);

                    //        if (_grupList.Mbg_cd == null)
                    //        {
                    //            _businessEntityGrup = new GroupBussinessEntity();
                    //            _businessEntityGrup.Mbg_act = true;
                    //            _businessEntityGrup.Mbg_add1 = _businessCompany.Mbe_add1;
                    //            _businessEntityGrup.Mbg_add2 = _businessCompany.Mbe_add2;
                    //            _businessEntityGrup.Mbg_cd = _invoiceHeader.Sah_cus_cd;
                    //            _businessEntityGrup.Mbg_contact = string.Empty;
                    //            _businessEntityGrup.Mbg_email = string.Empty;
                    //            _businessEntityGrup.Mbg_fax = string.Empty;
                    //            _businessEntityGrup.Mbg_mob = _businessCompany.Mbe_mob;
                    //            _businessEntityGrup.Mbg_name = _businessCompany.Mbe_name;
                    //            _businessEntityGrup.Mbg_nic = _businessCompany.Mbe_nic;
                    //            _businessEntityGrup.Mbg_tel = string.Empty;
                    //            _businessEntityGrup.Mbg_nationality = "SL";
                    //            _businessEntityGrup.Mbg_tit = _businessCompany.MBE_TIT;
                    //            _businessEntityGrup.Mbg_cre_by = _invoiceHeader.Sah_cre_by;
                    //            _businessEntityGrup.Mbg_mod_by = _invoiceHeader.Sah_mod_by;
                    //            _salesDAL.SaveBusinessEntityDetailGroup(_businessEntityGrup);
                    //        }
                    //    }

                    //}

                    #endregion Customer Creation

                    #region update auto no

                    if (!_isHold)
                    {
                        //nxt1:
                        /*_invoiceAuto.Aut_year = null;
                        MasterAutoNumber InvoiceAuto = _inventoryDAL.GetAutoNumber(_invoiceAuto.Aut_moduleid, _invoiceAuto.Aut_direction, _invoiceAuto.Aut_start_char, _invoiceAuto.Aut_cate_tp, _invoiceAuto.Aut_cate_cd, _invoiceAuto.Aut_modify_dt, _invoiceAuto.Aut_year);
                        _invNo = _invoiceAuto.Aut_start_char + InvoiceAuto.Aut_number.ToString("00000", CultureInfo.InvariantCulture);

                        _invoiceAuto.Aut_year = null;
                        _invoiceAuto.Aut_modify_dt = null;
                        if (_salesDAL.CheckSalesNo("sp_getinvno", "p_inv_no", _invNo) == 1)
                        {
                            //_salesDAL.UpdateInvoiceAutoNumber(_invoiceAuto);
                            //goto nxt1;
                            _error = "Invoice process terminated. Please re-process.(Hint - Duplicating Invoice No)";
                            _invoiceNo = string.Empty;
                            _receiptNo = string.Empty;
                            _deliveryOrder = string.Empty;
                            _errorlist = _error;
                            BuyBackInvNo = string.Empty;
                            _effect = -1;

                            _salesDAL.TransactionRollback();
                            _inventoryDAL.TransactionRollback();
                            _fmsInventoryDal.TransactionRollback();
                            _inventoryRepDAL.TransactionRollback();
                            _generalDAL.TransactionRollback();
                            return _effect;
                        }

                        _salesDAL.UpdateInvoiceAutoNumber(_invoiceAuto);*/

                        #region Buyback Item

                        //if (_buybacklist != null) if (_buybacklist.Count > 0)
                        //{
                        //    InventoryBLL _bll = new InventoryBLL();
                        //    _bll._salesDAL = _salesDAL;
                        //    _bll._inventoryDAL = _inventoryDAL;
                        //    _bll._FMSinventoryDAL = _fmsInventoryDal;
                        //    _bll._inventoryRepDAL = _inventoryRepDAL;
                        //    _buybackheader.Ith_manual_ref = _invNo;
                        //    _bll.SaveInwardScanSerial(_buybackheader, _buybacklist, null);
                        //    _bll.UpdateInventoryAutoNumber(_buybackheader, _buybackauto, "+", out  _buybackinv);
                        //    _inventoryDAL.UpdateMovementDocNo_Other(_buybackheader.Ith_seq_no, _buybackinv);
                        //}

                        #endregion Buyback Item

                        //#region Generate Promotion Vouchers :: Chamal 26-Jun-2014
                        //if (_voucher == null) _voucher = new List<InvoiceVoucher>();

                        //bool _promoVouApplied = false;
                        //if (_VoucherPromotion == true)
                        //{
                        //    foreach (InvoiceItem _itm in _invoiceItem)
                        //    {
                        //        MasterItem _mitm = _inventoryDAL.GetItem(_invoiceHeader.Sah_com, _itm.Sad_itm_cd);
                        //        if (_mitm.Mi_is_ser1 != -1)
                        //        {
                        //            for (int i = 1; i <= _itm.Sad_qty; i++)
                        //            {
                        //                List<PromoVoucherDefinition> _proVouList = GetPromotionalVouchersDefinition(_invoiceHeader.Sah_com, _invoiceHeader.Sah_inv_tp, _invoiceHeader.Sah_pc, _invoiceHeader.Sah_dt.Date, _itm.Sad_pbook, _itm.Sad_pb_lvl, _mitm.Mi_brand, _mitm.Mi_cate_1, _mitm.Mi_cate_2, _itm.Sad_itm_cd, false);
                        //                if (_proVouList != null)
                        //                {
                        //                    if (_proVouList.Count > 0)
                        //                    {
                        //                        foreach (PromoVoucherDefinition _proitm in _proVouList)
                        //                        {
                        //                            InvoiceVoucher _vou = new InvoiceVoucher();
                        //                            GiftVoucherPages _gvou = new GiftVoucherPages();

                        //                            _vou.Stvo_inv_no = _invNo;
                        //                            _vou.Stvo_prefix = _proitm.Spd_vou_cd;
                        //                            _vou.Stvo_bookno = _proitm.Spd_seq;
                        //                            _vou.Stvo_pageno = _salesDAL.GetPromotionVoucherNo();
                        //                            _vou.Stvo_gv_itm = _proitm.Spd_vou_cd;
                        //                            _vou.Stvo_price = _proitm.Spd_disc;
                        //                            _vou.Stvo_itm_cd = _itm.Sad_itm_cd;
                        //                            _vou.Stvo_cre_by = "PRO_VOU";
                        //                            if (_proitm.Spd_disc_isrt == true)
                        //                            {
                        //                                _vou.Stvo_stus = 1;
                        //                                _gvou.Gvp_gv_tp = "RATE";
                        //                            }
                        //                            else
                        //                            {
                        //                                _vou.Stvo_stus = 0;
                        //                                _gvou.Gvp_gv_tp = "VALUE";
                        //                            }

                        //                            _gvou.Gvp_amt = _proitm.Spd_disc;
                        //                            _gvou.Gvp_app_by = _invoiceHeader.Sah_cre_by;
                        //                            _gvou.Gvp_bal_amt = 0;
                        //                            _gvou.Gvp_book = _itm.Sad_itm_line;
                        //                            _gvou.Gvp_can_by = "";
                        //                            _gvou.Gvp_can_dt = DateTime.Now.Date;
                        //                            _gvou.Gvp_com = _invoiceHeader.Sah_com;
                        //                            _gvou.Gvp_cre_by = _invoiceHeader.Sah_cre_by;
                        //                            _gvou.Gvp_cre_dt = DateTime.Now.Date;
                        //                            _gvou.Gvp_cus_add1 = _invoiceHeader.Sah_cus_add1;
                        //                            _gvou.Gvp_cus_add2 = _invoiceHeader.Sah_cus_add2;
                        //                            _gvou.Gvp_cus_cd = _invoiceHeader.Sah_cus_cd;
                        //                            _gvou.Gvp_cus_mob = _businessCompany.Mbe_mob;
                        //                            _gvou.Gvp_cus_name = _invoiceHeader.Sah_cus_name;
                        //                            _gvou.Gvp_from = "-";
                        //                            _gvou.Gvp_gv_cd = _proitm.Spd_vou_cd;
                        //                            _gvou.Gvp_gv_prefix = "P_GV";
                        //                            _gvou.Gvp_is_allow_promo = false;
                        //                            _gvou.Gvp_issu_itm = 0;
                        //                            _gvou.Gvp_issue_by = "";
                        //                            _gvou.Gvp_issue_dt = DateTime.Now.Date;
                        //                            _gvou.Gvp_line = i;
                        //                            _gvou.Gvp_mod_by = "";
                        //                            _gvou.Gvp_mod_dt = DateTime.Now.Date;
                        //                            _gvou.Gvp_noof_itm = 1;
                        //                            _gvou.Gvp_oth_ref = _invNo;// _proitm.Spd_circular_no;
                        //                            _gvou.Gvp_page = _vou.Stvo_pageno;
                        //                            _gvou.Gvp_pc = _invoiceHeader.Sah_pc;
                        //                            _gvou.Gvp_ref = _proitm.Spd_seq.ToString();
                        //                            _gvou.Gvp_stus = "A";
                        //                            _gvou.Gvp_valid_from = _invoiceHeader.Sah_dt.Date;
                        //                            //_gvou.Gvp_valid_to = _invoiceHeader.Sah_dt.Date.AddMonths(_proitm.Spd_period);
                        //                            _gvou.Gvp_valid_to = _invoiceHeader.Sah_dt.Date.AddDays(_proitm.Spd_period); //Chamal 24-09-2014
                        //                            _gvou.Gvp_cus_nic = _businessCompany.Mbe_nic;

                        //                            _voucher.Add(_vou);
                        //                            _fmsInventoryDal.SaveGiftVoucherPages(_gvou);
                        //                            _promoVouApplied = true;
                        //                            break;
                        //                        }
                        //                    }
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                        //if (_promoVouApplied == true)
                        //{
                        //    if (_voucher != null)
                        //    {
                        //        if (_voucher.Count > 0)
                        //        {
                        //            //var _vouProCodes = _voucher.Where(x => x.Stvo_cre_by == "PRO_VOU").Select(x => x.Stvo_prefix).Distinct().ToList();
                        //            var _vouProCodes = _voucher.Where(x => x.Stvo_cre_by == "PRO_VOU").Select(x => x.Stvo_bookno).Distinct().ToList();
                        //            if (_vouProCodes != null && _vouProCodes.Count > 0)
                        //            {
                        //                foreach (var _cd in _vouProCodes)
                        //                {
                        //                    DataTable _dtothCom = _salesDAL.GetProVouAllowCompanies(_invoiceHeader.Sah_com, _cd);

                        //                    if (_dtothCom.Rows.Count > 0)
                        //                    {
                        //                        foreach (DataRow drow in _dtothCom.Rows)
                        //                        {
                        //                            MasterBusinessEntity _othCust = new MasterBusinessEntity();
                        //                            _othCust = _businessCompany;
                        //                            _othCust.Mbe_cd = _customerCode;
                        //                            _othCust.Mbe_com = drow["R_COM"].ToString();
                        //                            if (!string.IsNullOrEmpty(_othCust.Mbe_nic) || !string.IsNullOrEmpty(_othCust.Mbe_mob))
                        //                            {
                        //                                MasterBusinessEntity _nic = _salesDAL.GetActiveBusinessCompanyDetail(_invoiceHeader.Sah_com, string.Empty, _othCust.Mbe_nic, string.Empty, "C");
                        //                                MasterBusinessEntity _mobile = _salesDAL.GetActiveBusinessCompanyDetail(_invoiceHeader.Sah_com, string.Empty, string.Empty, _othCust.Mbe_mob, "C");
                        //                                if (_nic.Mbe_cd == null && _mobile.Mbe_cd == null)
                        //                                {
                        //                                    int _isUpdate = _salesDAL.UpdateBusinessEntityProfile(_othCust, 1);
                        //                                    if (_isUpdate <= 0) _salesDAL.SaveBusinessEntityDetail(_othCust);
                        //                                }
                        //                            }

                        //                        }
                        //                    }

                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                        //#endregion

                        //#region Save/Update Voucher
                        //if (_voucher != null)
                        //    if (_voucher.Count > 0)
                        //    {
                        //        _voucher.ForEach(x => x.Stvo_inv_no = _invNo);
                        //        UpdateInvoiceGiftVoucher(_invoiceHeader.Sah_com, _invoiceHeader.Sah_pc, _customerCode, _invoiceHeader.Sah_cus_name, _invoiceHeader.Sah_d_cust_add1, _invoiceHeader.Sah_d_cust_add2, _businessCompany.Mbe_mob, _invoiceHeader.Sah_dt, _invNo, _invoiceHeader.Sah_cre_by, _voucher, _salesDAL, _fmsInventoryDal);
                        //    }
                        //#endregion

                        //_salesDAL.UpdateInvoiceWithTaxCommission(_invNo, string.Empty, Convert.ToInt32(_invoiceSeq), Convert.ToInt32(_recieptSeq));
                        //string RecieptNo = "";
                        if (_recieptHeader != null)
                        {
                            if (_recieptAuto != null)
                            {
                                //nxt2:
                                _recieptAuto.Aut_year = null;
                                MasterAutoNumber RecieptAuto = _inventoryDAL.GetAutoNumber(_recieptAuto.Aut_moduleid, _recieptAuto.Aut_direction, _recieptAuto.Aut_start_char, _recieptAuto.Aut_cate_tp, _recieptAuto.Aut_cate_cd, _recieptAuto.Aut_modify_dt, _recieptAuto.Aut_year);
                                _receiptNo = _recieptAuto.Aut_cate_cd + "-" + RecieptAuto.Aut_start_char + RecieptAuto.Aut_number.ToString("00000", CultureInfo.InvariantCulture);
                                _recieptAuto.Aut_year = null;
                                _recieptAuto.Aut_modify_dt = null;
                                _recNo = _receiptNo;
                                if (_ToursDAL.CheckTBSSalesNo("SP_GETTBSRECEIPTNO", "p_rept_no", _recNo) == 1)
                                {
                                    // _salesDAL.UpdateInvoiceAutoNumber(_recieptAuto);
                                    // goto nxt2;
                                    _error = "Invoice process terminated. Please re-process.(Hint - Duplicating Receipt No)";
                                    _invoiceNo = string.Empty;
                                    _receiptNo = string.Empty;
                                    _deliveryOrder = string.Empty;
                                    _errorlist = _error;
                                    BuyBackInvNo = string.Empty;
                                    _effect = -1;

                                    _salesDAL.TransactionRollback();
                                    _inventoryDAL.TransactionRollback();
                                    _fmsInventoryDal.TransactionRollback();
                                    _inventoryRepDAL.TransactionRollback();
                                    _generalDAL.TransactionRollback();
                                    _ToursDAL.TransactionRollback();
                                    return _effect;
                                }
                                //_invDAL.UpdateAutoNumber(_recieptAuto);

                                _salesDAL.UpdateInvoiceAutoNumber(_recieptAuto);
                            }

                            if (Tours == true)
                            {
                                _salesDAL.UpdateReceiptTbs(_invNo, _recNo, Convert.ToInt32(_invoiceSeq), Convert.ToInt32(_recieptSeq));
                            }
                            else
                            {
                                _salesDAL.UpdateReceipt(_invNo, _recNo, Convert.ToInt32(_invoiceSeq), Convert.ToInt32(_recieptSeq));
                            }

                            //Update receipt no which allocated by receipt entry as per invoice
                            _inventoryRepDAL.UpdateAdvanceReceiptNofromInvoice(string.Empty, Convert.ToString(_invoiceSeq), _invNo);
                        }
                        //update old inv

                        //update anal8

                  


                        //if (_isDeliveryNow)
                        //{
                        //    // string _doc;
                        //    _inventoryDAL.UpdateInventoryAutoNumber(_invHdr, _inventoryAuto, "-", out _DONo);
                        //    MasterAutoNumber _AutoNo = new MasterAutoNumber();
                        //    int _e = 0;
                        //    if (_dataTable.Rows.Count > 0)
                        //    {
                        //        for (int i = 0; i < _dataTable.Rows.Count; i++)
                        //        {
                        //            if (_dataTable.Rows[i]["DocType"].ToString() == "ADJ")
                        //            {
                        //                _AutoNo.Aut_cate_cd = _invHdr.Ith_loc;
                        //                _AutoNo.Aut_moduleid = "ADJ";
                        //                _AutoNo.Aut_direction = null;
                        //                _AutoNo.Aut_start_char = "ADJ";
                        //                _AutoNo.Aut_cate_tp = "LOC";
                        //                //_AutoNo.Aut_year = _inventoryMovementHeader.Ith_doc_date.Year;
                        //                _AutoNo.Aut_year = null;

                        //                Int32 _autoNo = _inventoryDAL.GetAutoNumber(_AutoNo.Aut_moduleid, _AutoNo.Aut_direction, _AutoNo.Aut_start_char, _AutoNo.Aut_cate_tp, _AutoNo.Aut_cate_cd, _AutoNo.Aut_modify_dt, _AutoNo.Aut_year).Aut_number;
                        //                string _documentNo = _invHdr.Ith_loc + "-" + _AutoNo.Aut_start_char + "-" + Convert.ToString(_invHdr.Ith_doc_date.Year).Remove(0, 2) + "-" + _autoNo.ToString("00000", CultureInfo.InvariantCulture);
                        //                _e = _inventoryDAL.UpdateMovementDocNo(Convert.ToInt32(_dataTable.Rows[i]["SeqNo"].ToString()), _documentNo);
                        //                _e = _inventoryDAL.UpdateAutoNumber(_AutoNo);
                        //            }
                        //            else if (_dataTable.Rows[i]["DocType"].ToString() == "GRN")
                        //            {
                        //                _AutoNo.Aut_cate_cd = _invHdr.Ith_loc;
                        //                _AutoNo.Aut_moduleid = "GRN";
                        //                _AutoNo.Aut_direction = null;
                        //                _AutoNo.Aut_start_char = "GRN";
                        //                _AutoNo.Aut_cate_tp = "LOC";
                        //                //_AutoNo.Aut_year = _inventoryMovementHeader.Ith_doc_date.Year;
                        //                _AutoNo.Aut_year = null;

                        //                Int32 _autoNo = _inventoryDAL.GetAutoNumber(_AutoNo.Aut_moduleid, _AutoNo.Aut_direction, _AutoNo.Aut_start_char, _AutoNo.Aut_cate_tp, _AutoNo.Aut_cate_cd, _AutoNo.Aut_modify_dt, _AutoNo.Aut_year).Aut_number;
                        //                string _documentNo = _invHdr.Ith_loc + "-" + _AutoNo.Aut_start_char + "-" + Convert.ToString(_invHdr.Ith_doc_date.Year).Remove(0, 2) + "-" + _autoNo.ToString("00000", CultureInfo.InvariantCulture);
                        //                _e = _inventoryDAL.UpdateMovementDocNo(Convert.ToInt32(_dataTable.Rows[i]["SeqNo"].ToString()), _documentNo);
                        //                _e = _inventoryDAL.UpdateAutoNumber(_AutoNo);
                        //            }
                        //            else if (_dataTable.Rows[i]["DocType"].ToString() == "PO")
                        //            {
                        //                //_AutoNo.Aut_cate_cd = _inventoryMovementHeader.Ith_com;
                        //                //_AutoNo.Aut_moduleid = "PO_LOCAL";
                        //                //_AutoNo.Aut_direction = null;
                        //                //_AutoNo.Aut_start_char = "PO";
                        //                //_AutoNo.Aut_cate_tp = "COM";
                        //                //_AutoNo.Aut_year = null;

                        //                _AutoNo.Aut_cate_cd = _invHdr.Ith_com;
                        //                _AutoNo.Aut_cate_tp = "COM";
                        //                _AutoNo.Aut_direction = null;
                        //                _AutoNo.Aut_modify_dt = null;
                        //                _AutoNo.Aut_moduleid = "PUR";
                        //                _AutoNo.Aut_start_char = "PUR";
                        //                _AutoNo.Aut_year = null;

                        //                Int32 _autoNo = _inventoryDAL.GetAutoNumber(_AutoNo.Aut_moduleid, _AutoNo.Aut_direction, _AutoNo.Aut_start_char, _AutoNo.Aut_cate_tp, _AutoNo.Aut_cate_cd, _AutoNo.Aut_modify_dt, _AutoNo.Aut_year).Aut_number;
                        //                string _documentNo = _AutoNo.Aut_cate_cd + "-" + _AutoNo.Aut_start_char + string.Format("{0:000000}", _autoNo);
                        //                _inventoryDAL.UpdatePODocNo(Convert.ToInt32(_dataTable.Rows[i]["SeqNo"].ToString()), _documentNo);
                        //                _e = _inventoryDAL.UpdateAutoNumber(_AutoNo);
                        //                _inventoryDAL.UpdateGRNPODocNo(Convert.ToInt32(_dataTable.Rows[i]["SeqNo"].ToString()), _documentNo);
                        //            }
                        //        }
                        //    }

                        //    //update inv no
                        //    _inventoryDAL.UpdateOtherDocuments(_DONo, _invNo);
                        //    _inventoryDAL.UpdateBatchRefDoc(_DONo, _invNo);

                        //}
                    }

                    #endregion update auto no

                    #region Update Manual Doc
                    if (_invoiceHeader != null)
                        if (_invoiceHeader.Sah_manual) _fmsInventoryDal.UpdateManualDocNo(_location, "MDOC_INV", Convert.ToInt32(_invoiceHeader.Sah_ref_doc), _invNo);

                    #endregion Update Manual Doc

                    #region update invoice discount / Promotion Voucher page as F
                    if (_invoiceHeader != null)
                    {
                        foreach (InvoiceItem _itm in _invoiceItem)
                        {
                            if (_itm.Sad_dis_type == "P")
                            {
                                _salesDAL.UpdateDiscountUsedTimes(_itm.Sad_dis_seq, 1);
                            }

                            if (_itm.Sad_res_no == "PROMO_VOU" && _itm.Sad_res_line_no > 0)
                            {
                                //Add by Chamal 6-Jul-2014
                                _salesDAL.Update_GV_Pages(1, _invoiceHeader.Sah_com, _invoiceHeader.Sah_pc, _invoiceHeader.Sah_dt.Date, "", "F", _itm.Sad_res_line_no, "P_GV", _invoiceHeader.Sah_cre_by, _invNo);
                            }
                        }


                    #endregion update invoice discount / Promotion Voucher page as F

                        if (paxDetList != null)
                        {
                            Int32 ln = 1;
                            foreach (MST_ST_PAX_DET pax in paxDetList)
                            {
                                pax.SPD_SEQ = Convert.ToInt32(_invoiceSeq);
                                pax.SPD_LINE = ln;
                                pax.SPD_INV_NO = _invNo;
                                pax.SPD_ENQ_ID = _invoiceHeader.Sah_ref_doc;
                                Int32 ef = _ToursDAL.saveInvoicePax(pax);
                                ln++;
                            }
                        }


                    }
                    _error = enqMsg + _error;
                    _error = _error.Trim();
                    _effect = 1;
                }
                else
                    _effect = -1;

                try
                {
                    // _db = DataBase._ems; _salesDAL.ConnectionClose(); _db = DataBase._ems; _inventoryDAL.ConnectionClose(); _db = DataBase._fms; _fmsInventoryDal.ConnectionClose(); _db = DataBase._reportdb; _inventoryRepDAL.ConnectionClose(); _db = DataBase._ems; _generalDAL.ConnectionClose();
                    if (string.IsNullOrEmpty(_error) || oItem != null)
                    {
                        _db = DataBase._ems;
                        _salesDAL.TransactionCommit();
                        _db = DataBase._ems;
                        _inventoryDAL.TransactionCommit();
                        _db = DataBase._fms;
                        _fmsInventoryDal.TransactionCommit();
                        _db = DataBase._reportdb;
                        _inventoryRepDAL.TransactionCommit();
                        _db = DataBase._ems;
                        _generalDAL.TransactionCommit();
                        _db = DataBase._ems;
                        _ToursDAL.TransactionCommit();

                        if (_invNo != "")
                            _inventoryDAL.UpdateInvoiceDOStatus(_invNo);

                        //cus code update
                        if (_auto != null)
                        {
                            _inventoryDAL.UpdateAutoNumber(_auto);
                        }
                    }
                    else
                    {
                        _salesDAL.TransactionRollback();
                        _inventoryDAL.TransactionRollback();
                        _fmsInventoryDal.TransactionRollback();
                        _inventoryRepDAL.TransactionRollback();
                        _generalDAL.TransactionRollback();
                        _ToursDAL.TransactionRollback();
                    }
                }
                catch (Exception ex)
                {
                    _invoiceNo = string.Empty;
                    _receiptNo = string.Empty;
                    _deliveryOrder = string.Empty;
                    _errorlist = "Database" + _db + " is not responding. Please contact IT Operation.\n" + ex.Message;
                    BuyBackInvNo = string.Empty;
                    _effect = -1;
                    return _effect;
                }
            }
            catch (Exception ex)
            {
                _error = ex.Message.ToString();
                _invoiceNo = string.Empty;
                _receiptNo = string.Empty;
                _deliveryOrder = string.Empty;
                _errorlist = _error;
                BuyBackInvNo = string.Empty;
                _effect = -1;

                _salesDAL.TransactionRollback();
                _inventoryDAL.TransactionRollback();
                _fmsInventoryDal.TransactionRollback();
                _inventoryRepDAL.TransactionRollback();
                _generalDAL.TransactionRollback();
                _ToursDAL.TransactionRollback();
            }

            _invoiceNo = _invNo;
            _receiptNo = _recNo;
            _deliveryOrder = _DONo;
            _errorlist = _error;
            BuyBackInvNo = _buybackinv;
            return _effect;
        }

        //Tharaka 2015-03-23
        public void CommonSaveInvoiceWithDeliveryOrderWithTransaction(InvoiceHeader _invoiceHeader, List<InvoiceItem> _invoiceItem, List<InvoiceSerial> _invoiceSerial, RecieptHeader _recieptHeader, List<RecieptItem> _recieptItem, InventoryHeader _inventoryHeader, List<ReptPickSerials> _pickSerial, List<ReptPickSerialsSub> _pickSubSerial, MasterAutoNumber _invoiceAuto, MasterAutoNumber _recieptAuto, MasterAutoNumber _inventoryAuto, bool _isDeliveryNow, out  string _invoiceNo, out string _receiptNo, out string _deliveryOrder, InventoryDAL _invDAL, SalesDAL _salDAL, ReptCommonDAL _invRepDAL, bool _isHold, bool _isHoldInvoiceProcess, out string _errorlist, bool _ishireSale, out string _invSeq, out string _recieptSeq, out InventoryHeader _mov, out DataTable _datatable, string InvNo)
        {
            string _invNo = "";
            string _recNo = "";
            string _doNo = "";

            List<ReptPickSerials> _remakeReptSerialList = new List<ReptPickSerials>();
            string _error = string.Empty;

            #region CMT

            //Delivery Now - Reserve Serialized/Non-Serialized Item inline
            //if (_isDeliveryNow)
            //{
            //    bool _isOK = true;
            //    string _list = string.Empty;
            //    _isOK = TakeInventorySerialwithItem(_invoiceHeader.Sah_dt, _invoiceHeader.Sah_com, _invoiceHeader.Sah_pc, _inventoryHeader.Ith_loc, _invoiceItem, _pickSerial, _invDAL, _salDAL, _invRepDAL, out _remakeReptSerialList, out _list);

            //    if (_isOK == false)
            //    {
            //        _error = "Inventory and invoice qty mismatch found. process aborted!";
            //        _invoiceNo = _invNo;
            //        _receiptNo = _recNo;
            //        _deliveryOrder = _doNo;
            //        _errorlist = _error;
            //        _invSeq = "";
            //        _recieptSeq = "";
            //        _mov = null;
            //        _datatable = null;
            //        return;
            //    }

            //    _pickSerial = new List<ReptPickSerials>();
            //    _pickSerial = _remakeReptSerialList;
            //}

            #endregion CMT

            string InvoiceNo = _invoiceHeader.Sah_inv_no;
            string RecieptNo = string.Empty;
            try
            {
                Int32 _invoiceLine = 1;
                DataTable _tbl = _salDAL.GetEmployee(_invoiceHeader.Sah_com, _invoiceHeader.Sah_sales_ex_cd);
                string _executiveType = string.Empty;
                foreach (DataRow _r in _tbl.Rows)
                {
                    _executiveType = Convert.ToString(_r["esep_cat_cd"]);
                }
                List<SaleCommission> _saveCommission = new List<SaleCommission>();

                #region Delete invoice if its a HOLD status

                //if (_isHold || _isHoldInvoiceProcess) _salDAL.DeleteInvoiceDetailForHold(_invoiceHeader.Sah_seq_no);

                #endregion Delete invoice if its a HOLD status

                //Generate SeqNo
                //If hold, then client should generate the sequence and assign to header,ie; if its recall hold invoice and need to second time hold, could set sequence no
                //as per the recalled sequence no
                Int32 InvoiceSeqNo = _isHoldInvoiceProcess ? _invoiceHeader.Sah_seq_no : _ToursDAL.GETINVOSEQ();
                Int32 RecieptSeqNo = (_recieptHeader != null) ? !string.IsNullOrEmpty(_recieptHeader.Sar_receipt_type) ? _ToursDAL.GETRECIPTSEQ() : -1 : -1;
                Int32 InventorySeqNo = 0;

                //if (_isDeliveryNow) InventorySeqNo = _invDAL.GetSerialID();

                //-------------------------------------------------------------------------- Invoice------------------------------------------------------------------------

                _invoiceHeader.Sah_seq_no = InvoiceSeqNo;
                //_invoiceHeader.Sah_inv_no = Convert.ToString(InvoiceSeqNo);

                #region Calculation for total of the payment to infiltrate invoice header

                decimal _totalValue = _invoiceItem.Sum(x => x.Sad_tot_amt);
                decimal _totalReceiptAmt = 0;
                if (_recieptItem != null) if (_recieptItem.Count > 0) _totalReceiptAmt = _recieptItem.Sum(x => x.Sard_settle_amt);

                _invoiceHeader.Sah_anal_7 = _totalValue;// -_totalReceiptAmt; //Total Invoice Amount - Total Receipt AmountBY DARSHANA 3/12/2012
                _invoiceHeader.Sah_anal_8 = _totalReceiptAmt;//Receipt Amount

                //ADDED SACHITH 2013/12/04
                //CREDIT SALES
                //FOR SVAT CUSTOMERS ADD TAX VALUE TO ANAL_8
                if (_invoiceHeader.Sah_is_svat && _invoiceHeader.Sah_inv_tp == "CRED")
                {
                    decimal vatTotal = _invoiceItem.Sum(X => X.Sad_itm_tax_amt);
                    _invoiceHeader.Sah_anal_8 = _invoiceHeader.Sah_anal_8 + vatTotal;
                }

                if (_invoiceSerial != null)
                    if (_invoiceSerial.Count > 0)
                    {
                        //_invoiceSerial.ForEach(X => X.Sap_inv_no = Convert.ToString(InvoiceSeqNo));
                        _invoiceSerial.ForEach(X => X.Sap_inv_no = _invoiceHeader.Sah_inv_no);
                        _invoiceSerial.ForEach(x => x.Sap_seq_no = InvoiceSeqNo);
                    }

                DataTable _tblESDEPFWHF = new DataTable();
                _tblESDEPFWHF = _salDAL.Get_ESD_EPF_WHT(_invoiceHeader.Sah_com, _invoiceHeader.Sah_pc, _invoiceHeader.Sah_dt);
                Decimal ESD_rt = 0; Decimal EPF_rt = 0; Decimal WHT_rt = 0;
                if (_tblESDEPFWHF.Rows.Count > 0) { ESD_rt = Convert.ToDecimal(_tblESDEPFWHF.Rows[0]["MPCH_ESD"]); EPF_rt = Convert.ToDecimal(_tblESDEPFWHF.Rows[0]["MPCH_EPF"]); WHT_rt = Convert.ToDecimal(_tblESDEPFWHF.Rows[0]["MPCH_WHT"]); }
                _invoiceHeader.Sah_esd_rt = ESD_rt;
                _invoiceHeader.Sah_epf_rt = EPF_rt;
                _invoiceHeader.Sah_wht_rt = WHT_rt;
                if (_recieptHeader != null)
                {
                    _recieptHeader.Sar_esd_rate = ESD_rt;
                    _recieptHeader.Sar_epf_rate = EPF_rt;
                    _recieptHeader.Sar_wht_rate = WHT_rt;
                }
                #endregion Calculation for total of the payment to infiltrate invoice header

                //Save Invoice Header

                #region Save Invoice Header

                _ToursDAL.SaveInvoiceHeader(_invoiceHeader);

                #endregion Save Invoice Header

                //Save Invoice Items

                #region Invoice Item Detail

                foreach (InvoiceItem _itm in _invoiceItem)
                {

                    // MasterItem _item = _invDAL.GetItem(_invoiceHeader.Sah_com, _itm.Sad_itm_cd);
                    _itm.Sad_seq_no = InvoiceSeqNo;
                    _invoiceLine = _itm.Sad_itm_line;
                    //_itm.Sad_inv_no = Convert.ToString(InvoiceSeqNo);
                    _itm.Sad_inv_no = _invoiceHeader.Sah_inv_no;
                    _itm.Sad_unit_amt = _itm.Sad_unit_rt * _itm.Sad_qty;
                    _itm.Sad_itm_tp = "V";//_itm.Mi_itm_tp;
                    //_itm.Sad_uom = _item.Mi_itm_uom;
                    _itm.Sad_trd_svc_chrg = Math.Round((_itm.Sad_tot_amt - _itm.Sad_itm_tax_amt) / _itm.Sad_qty, 2);
                    if (_invoiceHeader.Sah_tax_exempted)
                    {
                        _itm.Sad_tot_amt = _itm.Sad_tot_amt - _itm.Sad_itm_tax_amt;
                        _itm.Sad_itm_tax_amt = 0;
                    }

                    //update DO qty
                    //if (_isDeliveryNow)
                    //{
                    //    decimal _doQty = 0;
                    //    List<ReptPickSerials> _temp = _pickSerial.Where(x => x.Tus_base_itm_line == _itm.Sad_itm_line).ToList<ReptPickSerials>();
                    //    if (_temp != null && _temp.Count > 0)
                    //    {
                    //        _doQty = _temp.Sum(p => p.Tus_qty);
                    //    }
                    //    _itm.Sad_do_qty = _doQty;
                    //}
                    _itm.Sad_job_no = (_invoiceHeader.Sah_ref_doc != null) ? _invoiceHeader.Sah_ref_doc : InvNo;
                    _ToursDAL.SaveInvoiceItem(_itm);
                    _ToursDAL.UpdateTBSPrice(_itm.Sad_itm_cd, _itm.Sad_pbook, _itm.Sad_pb_lvl, _invoiceHeader.Sah_cus_cd, _itm.Sad_promo_cd, _itm.Sad_seq, _itm.Sad_itm_seq);

                    if (_invoiceHeader.Sah_dt == DateTime.Now.Date)
                    {
                        List<MasterItemTax> _itmTax = new List<MasterItemTax>();
                        _itmTax = _salDAL.GetItemTax(_invoiceHeader.Sah_com, _itm.Sad_itm_cd, _itm.Sad_itm_stus, string.Empty, string.Empty);

                        foreach (MasterItemTax _one in _itmTax)
                        {
                            InvoiceItemTax _tax = new InvoiceItemTax();
                            _tax.Satx_inv_no = _itm.Sad_inv_no;
                            _tax.Satx_itm_cd = _itm.Sad_itm_cd;
                            _tax.Satx_itm_line = _itm.Sad_itm_line;
                            _tax.Satx_itm_tax_amt = _invoiceHeader.Sah_tax_exempted ? 0 : _itm.Sad_itm_tax_amt;// ((_itm.Sad_unit_rt - _itm.Sad_disc_amt / _itm.Sad_qty) * _one.Mict_tax_rate / 100) * _itm.Sad_qty;
                            _tax.Satx_itm_tax_rt = _one.Mict_tax_rate;
                            _tax.Satx_itm_tax_tp = _one.Mict_tax_cd;
                            _tax.Satx_job_line = 0;
                            _tax.Satx_job_no = "";
                            _tax.Satx_seq_no = InvoiceSeqNo;
                            _ToursDAL.SaveTBSSalesItemTax(_tax);
                        }
                    }
                    else
                    {
                        List<MasterItemTax> _itmTaxEff = new List<MasterItemTax>();
                        _itmTaxEff = _salDAL.GetItemTaxEffDt(_invoiceHeader.Sah_com, _itm.Sad_itm_cd, _itm.Sad_itm_stus, string.Empty, string.Empty, _invoiceHeader.Sah_dt);

                        if (_itmTaxEff.Count > 0)
                        {
                            foreach (MasterItemTax _one in _itmTaxEff)
                            {
                                InvoiceItemTax _tax = new InvoiceItemTax();
                                _tax.Satx_inv_no = _itm.Sad_inv_no;
                                _tax.Satx_itm_cd = _itm.Sad_itm_cd;
                                _tax.Satx_itm_line = _itm.Sad_itm_line;
                                _tax.Satx_itm_tax_amt = _invoiceHeader.Sah_tax_exempted ? 0 : _itm.Sad_itm_tax_amt;// ((_itm.Sad_unit_rt - _itm.Sad_disc_amt / _itm.Sad_qty) * _one.Mict_tax_rate / 100) * _itm.Sad_qty;
                                _tax.Satx_itm_tax_rt = _one.Mict_tax_rate;
                                _tax.Satx_itm_tax_tp = _one.Mict_tax_cd;
                                _tax.Satx_job_line = 0;
                                _tax.Satx_job_no = "";
                                _tax.Satx_seq_no = InvoiceSeqNo;
                                _ToursDAL.SaveTBSSalesItemTax(_tax);
                            }
                        }
                        else
                        {
                            List<LogMasterItemTax> _itmTax = new List<LogMasterItemTax>();
                            _itmTax = _salDAL.GetItemTaxLog(_invoiceHeader.Sah_com, _itm.Sad_itm_cd, _itm.Sad_itm_stus, string.Empty, string.Empty, _invoiceHeader.Sah_dt);

                            foreach (LogMasterItemTax _one in _itmTax)
                            {
                                InvoiceItemTax _tax = new InvoiceItemTax();
                                _tax.Satx_inv_no = _itm.Sad_inv_no;
                                _tax.Satx_itm_cd = _itm.Sad_itm_cd;
                                _tax.Satx_itm_line = _itm.Sad_itm_line;
                                _tax.Satx_itm_tax_amt = _invoiceHeader.Sah_tax_exempted ? 0 : _itm.Sad_itm_tax_amt;// ((_itm.Sad_unit_rt - _itm.Sad_disc_amt / _itm.Sad_qty) * _one.Mict_tax_rate / 100) * _itm.Sad_qty;
                                _tax.Satx_itm_tax_rt = _one.Lict_tax_rate;
                                _tax.Satx_itm_tax_tp = _one.Lict_tax_cd;
                                _tax.Satx_job_line = 0;
                                _tax.Satx_job_no = "";
                                _tax.Satx_seq_no = InvoiceSeqNo;
                                _ToursDAL.SaveTBSSalesItemTax(_tax);
                            }
                        }
                    }

                    #region cmt

                    //Dictionary<ItemHierarchyElement, string> _itemHierarchyElement = new Dictionary<ItemHierarchyElement, string>();
                    //_itemHierarchyElement.Add(ItemHierarchyElement.BRAND, _item.Mi_brand);
                    //_itemHierarchyElement.Add(ItemHierarchyElement.ITEM, _item.Mi_cd);
                    //_itemHierarchyElement.Add(ItemHierarchyElement.MAIN_CATEGORY, _item.Mi_cate_1);
                    //_itemHierarchyElement.Add(ItemHierarchyElement.PRICE_BOOK, _itm.Sad_pbook);
                    //_itemHierarchyElement.Add(ItemHierarchyElement.PRICE_LEVEL, _itm.Sad_pb_lvl);
                    //_itemHierarchyElement.Add(ItemHierarchyElement.PROMOTION, _itm.Sad_promo_cd);
                    //_itemHierarchyElement.Add(ItemHierarchyElement.SERIAL, string.Empty);
                    //_itemHierarchyElement.Add(ItemHierarchyElement.SUB_CATEGORY, _item.Mi_cate_2);

                    //List<CashCommissionDetailRef> _CashCommissionDetail = GetCommissionStructure(_invoiceHeader.Sah_com, _invoiceHeader.Sah_pc, _invoiceHeader.Sah_inv_tp, _invoiceHeader.Sah_dt, _itm.Sad_pbook, _itm.Sad_pb_lvl, _invoiceHeader.Sah_sales_ex_cd, _executiveType, _itemHierarchyElement, "PC_PRIT_HIERARCHY", "PC");
                    //List<SaleCommission> _commission = null;

                    ////commission calculation
                    //if (_CashCommissionDetail != null)
                    //{
                    //    if (_CashCommissionDetail.Count > 0)
                    //    {
                    //        _commission = GetCalculatedCommission(_itm.Sad_itm_cd, _invoiceLine, _itm.Sad_unit_rt * _itm.Sad_qty - _itm.Sad_disc_amt, _totalValue, _recieptItem, _CashCommissionDetail[0]);
                    //    }
                    //}

                    //if (_commission != null)
                    //    _saveCommission.AddRange(_commission);

                    //_invoiceLine += 1;

                    #endregion cmt
                }

                //Update Discount Definition
                var _discountseq = _invoiceItem.Where(x => x.Sad_dis_type == "M").Select(x => x.Sad_dis_seq).Distinct().ToList();
                if (_discountseq != null && _discountseq.Count > 0)
                {
                    foreach (var _i in _discountseq)
                    {
                        int _seqno = Convert.ToInt32(_i);
                        _salDAL.UpdateGeneralDiscount("M", _seqno, 0);
                    }
                }

                #endregion Invoice Item Detail

                #region Commission Part - Commented :)

                //if (_saveCommission != null)
                //    if (_saveCommission.Count > 0)
                //    {
                //        //Save Commissiom
                //        _saveCommission.ForEach(x => x.Sac_invoice_no = Convert.ToString(_invoiceHeader.Sah_seq_no));
                //        _saveCommission.ForEach(x => x.Sac_seq_no = _invoiceHeader.Sah_seq_no);
                //        foreach (SaleCommission _com in _saveCommission)
                //        {
                //            _salDAL.SaveSalesCommission(_com);
                //        }
                //    }

                #endregion Commission Part - Commented :)

                //Save Invoice Serials

                #region Invoice Serial

                //if (_invoiceSerial != null)
                //    if (_invoiceSerial.Count > 0)
                //    {
                //        foreach (InvoiceSerial _itm in _invoiceSerial)
                //        {
                //            _itm.Sap_seq_no = InvoiceSeqNo;
                //            _salDAL.SaveSalesSerial(_itm);
                //        }
                //    }

                #endregion Invoice Serial

                //-------------------------------------------------------------------------- Payment

                #region Payment


                if (_recieptHeader != null && _recieptItem != null && _recieptItem.Count > 0)
                {
                    string _shortcompany = _generalDAL.GetCompByCode(_invoiceHeader.Sah_com).Mc_anal5;
                    _recieptHeader.Sar_seq_no = RecieptSeqNo;
                    _recieptHeader.Sar_receipt_no = Convert.ToString(RecieptSeqNo);
                    _recieptHeader.Sar_tot_settle_amt = _totalReceiptAmt;
                    _ToursDAL.SaveTBSReceiptHeader(_recieptHeader);

                    if (_recieptItem != null)
                        if (_recieptItem.Count > 0)
                        {
                            foreach (RecieptItem _itm in _recieptItem)
                            {

                                _itm.Sard_seq_no = RecieptSeqNo;
                                _itm.Sard_inv_no = _invoiceHeader.Sah_inv_no;
                                _itm.Sard_receipt_no = Convert.ToString(RecieptSeqNo);
                                _ToursDAL.SaveTBSReceiptItem(_itm);

                                if (_itm.Sard_pay_tp.Trim() == "ADVAN")
                                {
                                    _invRepDAL.UpdateAdvanceReceiptNofromInvoice(_itm.Sard_ref_no, Convert.ToString(InvoiceSeqNo), string.Empty);
                                    RecieptHeader _rHdr = new RecieptHeader();
                                    _rHdr.Sar_receipt_no = _itm.Sard_ref_no;
                                    _rHdr.Sar_used_amt = _itm.Sard_settle_amt;
                                    _rHdr.Sar_act = true;
                                    _rHdr.Sar_direct = true;
                                    _rHdr.Sar_mod_by = _recieptHeader.Sar_mod_by;
                                    _ToursDAL.SaveTBSReceiptHeader(_rHdr);
                                }
                                if (_itm.Sard_pay_tp.Trim() == "CRNOTE")
                                {
                                    _ToursDAL.UpdateTBSCreditNoteBalance(_recieptHeader.Sar_com_cd, _recieptHeader.Sar_profit_center_cd, _itm.Sard_ref_no, _itm.Sard_settle_amt);
                                    MasterBusinessEntity _entity = _ToursDAL.GetBusinessCompanyDetail(_invoiceHeader.Sah_com, _invoiceHeader.Sah_cus_cd, null, null, "C");
                                    MasterProfitCenter _profit = _salDAL.GetProfitCenter(_invoiceHeader.Sah_com, _invoiceHeader.Sah_pc);
                                    //if (_entity != null)
                                    //    if (!string.IsNullOrEmpty(_entity.Mbe_com) && !string.IsNullOrEmpty(_entity.Mbe_mob))
                                    //    {
                                    //        string _realPhNo = GetRealPhoneNumber(_entity.Mbe_mob);
                                    //        OutSMS _out = new OutSMS();
                                    //        _out.Createtime = DateTime.Now;
                                    //        _out.Msg = "Your customer code : " + _entity.Mbe_cd + " auth. payment " + _itm.Sard_settle_amt + " deduct from your cred. bal. at " + _profit.Mpc_desc + ". Enq SMS/Call " + _profit.Mpc_tel + ". " + _shortcompany + ",(" + DateTime.Now.Day + "," + DateTime.Now.Month + ")";
                                    //        _out.Msgstatus = 0;
                                    //        _out.Msgtype = "S";
                                    //        _out.Receivedtime = DateTime.Now;
                                    //        _out.Receiver = _entity.Mbe_cd;
                                    //        _out.Receiverphno = _entity.Mbe_mob;
                                    //        _out.Refdocno = _itm.Sard_ref_no;
                                    //        _out.Sender = _invoiceHeader.Sah_cre_by;
                                    //        _out.Createtime = DateTime.Now;
                                    //        _invDAL.SaveSMSOut(_out);
                                    //    }
                                }

                                //if (_itm.Sard_pay_tp.Trim() == "LORE")
                                //{
                                //    _salesDAL.RedeemLoyaltyPoint(_itm.Sard_ref_no, _invoiceHeader.Sah_cus_cd, _itm.Sard_anal_4);
                                //}

                                //if (_itm.Sard_pay_tp.Trim() == "GVO")
                                //{
                                //    //_fmsInventoryDal.UpdateRedeemVoucher(_itm.Sard_anal_1, _invoiceHeader.Sah_pc, _itm.Sard_sim_ser, _itm.Sard_ref_no, _itm.Sard_cc_tp, _itm.Sard_anal_2, _invoiceHeader.Sah_cre_by, _itm.Sard_settle_amt);
                                //    _fmsInventoryDal.UpdateRedeemVoucher(_invoiceHeader.Sah_com, _invoiceHeader.Sah_pc, _itm.Sard_sim_ser, _itm.Sard_ref_no, _itm.Sard_cc_tp, _itm.Sard_anal_2, _invoiceHeader.Sah_cre_by, _itm.Sard_settle_amt);
                                //}
                            }
                        }
                }

                #endregion Payment

                #region Customer Account Maintain

                if (_invoiceHeader.Sah_cus_cd != "CASH")
                {
                    decimal _invoiceTotal = 0;
                    decimal _paidTotal = 0;

                    if (_invoiceItem != null)
                        if (_invoiceItem.Count > 0)
                        {
                            //Updating Account Balance
                            var _grandTotal = (from _total in _invoiceItem
                                               select _total.Sad_tot_amt).Sum();
                            _invoiceTotal = _grandTotal;
                        }

                    if (_recieptItem != null)
                        if (_recieptItem.Count > 0)
                        {
                            var _payTotal = (from _pay in _recieptItem
                                             select _pay.Sard_settle_amt).Sum();
                            _paidTotal = _payTotal;
                        }

                    CustomerAccountRef _account = new CustomerAccountRef();
                    _account.Saca_acc_bal = _invoiceTotal - _paidTotal;
                    _account.Saca_com_cd = _invoiceHeader.Sah_com;
                    _account.Saca_crdt_lmt = 0;
                    _account.Saca_cre_by = _invoiceHeader.Sah_cre_by;
                    _account.Saca_cre_when = _invoiceHeader.Sah_cre_when;
                    _account.Saca_cust_cd = _invoiceHeader.Sah_cus_cd;
                    _account.Saca_mod_by = _invoiceHeader.Sah_cre_by;
                    _account.Saca_mod_when = _invoiceHeader.Sah_cre_when;
                    _account.Saca_ord_bal = 0;
                    _account.Saca_session_id = _invoiceHeader.Sah_session_id;

                    //
                    // _salDAL.SaveCustomerAccount(_account);
                }

                #endregion Customer Account Maintain

                if (!string.IsNullOrEmpty(_invoiceHeader.Sah_anal_6))
                {
                    //DataTable _loldetail = _salDAL.GetLoyaltyCardDetail(_invoiceHeader.Sah_cus_cd, _invoiceHeader.Sah_anal_6);
                    //string _loltype = string.Empty;
                    //string _membership = string.Empty;
                    //if (_loldetail != null && _loldetail.Rows.Count >= 0) { _loltype = _loldetail.Rows[0].Field<string>("salcm_loty_tp"); _membership = _loldetail.Rows[0].Field<string>("salcm_cus_spec"); }
                    //decimal _points = GetLoyaltyPoint(_loltype, _membership, _invoiceHeader.Sah_com, _invoiceHeader.Sah_pc, _invoiceHeader.Sah_dt, _invoiceItem, _invoiceSerial, _recieptItem, _salDAL, _invDAL);
                    //if (_points > 0)
                    //{
                    //    _salDAL.UpdateLoyaltyCardPoint(_invoiceHeader.Sah_cus_cd, _invoiceHeader.Sah_anal_6, _points, _invoiceHeader.Sah_cre_by);
                    //    InvoiceLoyalty _lol = new InvoiceLoyalty();
                    //    _lol.Stlt_inv_no = Convert.ToString(_invoiceHeader.Sah_seq_no);
                    //    _lol.Stlt_pt = _points;
                    //    _lol.Stlt_seq_no = _invoiceHeader.Sah_seq_no;
                    //    _salDAL.SaveInvoiceLoyalty(_lol);
                    //}
                }

                InventoryHeader _invHdr = null;
                DataTable _dataTable = null; ;

                #region Invoice Auto Number/Delivery Order

                if (!_isHold)
                {
                    /*
                nxt1:
                    _invoiceAuto.Aut_year = null;
                    MasterAutoNumber InvoiceAuto = _invDAL.GetAutoNumber(_invoiceAuto.Aut_moduleid, _invoiceAuto.Aut_direction, _invoiceAuto.Aut_start_char, _invoiceAuto.Aut_cate_tp, _invoiceAuto.Aut_cate_cd, _invoiceAuto.Aut_modify_dt, _invoiceAuto.Aut_year);
                    if (!_ishireSale)
                        InvoiceNo = _invoiceAuto.Aut_start_char + InvoiceAuto.Aut_number.ToString("00000", CultureInfo.InvariantCulture);
                    else
                        InvoiceNo = _invoiceAuto.Aut_cate_cd + "-" + _invoiceAuto.Aut_start_char + InvoiceAuto.Aut_number.ToString("00000", CultureInfo.InvariantCulture);

                    _invoiceAuto.Aut_year = null;
                    _invoiceAuto.Aut_modify_dt = null;
                    if (_salDAL.CheckSalesNo("sp_getinvno", "p_inv_no", InvoiceNo) == 1)
                    {
                        _salDAL.UpdateInvoiceAutoNumber(_invoiceAuto);
                        goto nxt1;
                    }

                    _salDAL.UpdateInvoiceAutoNumber(_invoiceAuto);
                    _salDAL.UpdateInvoiceWithTaxCommission(InvoiceNo, string.Empty, InvoiceSeqNo, RecieptSeqNo);

                    if (_recieptAuto != null)
                    {
                    nxt2:
                        _recieptAuto.Aut_year = null;
                        MasterAutoNumber RecieptAuto = _invDAL.GetAutoNumber(_recieptAuto.Aut_moduleid, _recieptAuto.Aut_direction, _recieptAuto.Aut_start_char, _recieptAuto.Aut_cate_tp, _recieptAuto.Aut_cate_cd, _recieptAuto.Aut_modify_dt, _recieptAuto.Aut_year);
                        RecieptNo = _recieptAuto.Aut_cate_cd + "-" + RecieptAuto.Aut_start_char + RecieptAuto.Aut_number.ToString("00000", CultureInfo.InvariantCulture);
                        _recieptAuto.Aut_year = null;
                        _recieptAuto.Aut_modify_dt = null;

                        if (_salDAL.CheckSalesNo("sp_getreceiptno", "p_rept_no", RecieptNo) == 1)
                        {
                            _salDAL.UpdateInvoiceAutoNumber(_recieptAuto);
                            goto nxt2;
                        }
                        //_invDAL.UpdateAutoNumber(_recieptAuto);

                        _salDAL.UpdateInvoiceAutoNumber(_recieptAuto);
                    }
                    _salDAL.UpdateReceipt(InvoiceNo, RecieptNo, InvoiceSeqNo, RecieptSeqNo);
                    //Update receipt no which allocated by receipt entry as per invoice
                    _invRepDAL.UpdateAdvanceReceiptNofromInvoice(string.Empty, Convert.ToString(InvoiceSeqNo), InvoiceNo);
                    */

                    //Raise Delivery Order if the invoice going to deliver now!
                    //if (_isDeliveryNow)
                    //{
                    //    _pickSerial.ForEach(x => x.Tus_base_doc_no = InvoiceSeqNo.ToString());
                    //    _pickSerial.ForEach(x => x.Tus_usrseq_no = InventorySeqNo);
                    //    _inventoryHeader.Ith_pc = _invoiceHeader.Sah_pc;
                    //    _inventoryHeader.Ith_oth_docno = InvoiceSeqNo.ToString();
                    //    _inventoryHeader.Ith_entry_no = InvoiceSeqNo.ToString();
                    //    _inventoryHeader.Ith_seq_no = InventorySeqNo;
                    //    InventoryBLL _bll = new InventoryBLL();
                    //    //_bll.DeliveryOrderInterCompany(_inventoryHeader, _pickSerial, _pickSubSerial, _inventoryAuto, out  _doNo, _invRepDAL, _invDAL);
                    //    //Code by Chamal 13-May-2013 edit DeliveryOrder function and new DeliveryOrderEntry

                    //    _bll.DeliveryOrderWithoutAutoNo(_inventoryHeader, _pickSerial, _pickSubSerial, _inventoryAuto, _invRepDAL, _invDAL, true, out _invHdr, out _dataTable);
                    //}
                }
                else
                {
                    InvoiceNo = Convert.ToString(InvoiceSeqNo);
                }

                _invSeq = InvoiceSeqNo.ToString();
                _recieptSeq = RecieptSeqNo.ToString();
                _mov = _invHdr;
                _datatable = _dataTable;

                #endregion Invoice Auto Number/Delivery Order
            }
            catch (Exception ex)
            {
                _invSeq = "";
                _recieptSeq = "";
                _mov = null;
                _datatable = null;
                _error += "Generated error " + ex.Message;
                if (_error.Contains("UK_SAHINVNO") || _error.Contains("ORA-00001"))
                {
                    _error = "Please try again in a few seconds.";
                }
            }

            _invNo = InvoiceNo;
            _recNo = RecieptNo;

            _invoiceNo = _invNo;
            _receiptNo = _recNo;
            _deliveryOrder = _doNo;
            _errorlist = _error;
        }

        //Tharaka 2015-03-23
        private string GetRealPhoneNumber(string _phone)
        {
            Regex digitsOnly = new Regex(@"[^\d]");
            return digitsOnly.Replace(_phone, "");
        }

        //Tharaka 2015-03-23
        public decimal GetLoyaltyPoint(string _loyaltytype, string _membership, string _company, string _profitcenter, DateTime _date, List<InvoiceItem> _invoiceitem, List<InvoiceSerial> _seriallist, List<RecieptItem> _receiptdetail, SalesDAL _salDll, InventoryDAL _invDll)
        {
            decimal _lolpt = 0;
            bool _isfound = false;
            List<MasterSalesPriorityHierarchy> _hierarchy = _salesDAL.GetSalesPriorityHierarchy(_company, _profitcenter, "PC_PRIT_HIERARCHY", "PC");
            if (_hierarchy == null || _hierarchy.Count <= 0) return 0;
            foreach (MasterSalesPriorityHierarchy _zero in _hierarchy)
            {
                foreach (InvoiceItem _one in _invoiceitem)
                {
                    decimal _itemvalue = _one.Sad_tot_amt;
                    //Filter serial as per item
                    var seriallist = _seriallist.Where(x => x.Sap_itm_cd == _one.Sad_itm_cd && x.Sap_ser_1 != "N/A" && !string.IsNullOrEmpty(x.Sap_ser_1)).ToList();

                    List<LoyaltyPointDefinition> _init = GetFesibleLoyaltyDefinition(_company, _loyaltytype, _membership, _zero.Mpi_cd, _zero.Mpi_val, _date, _one, seriallist, _receiptdetail, _salDll, _invDll);
                    if (_init != null) if (_init.Count > 0)
                        {
                            _init.OrderByDescending(X => X.Saldf_seq).OrderBy(X => X.Saldf_line);

                            if (_init[0].Saldf_is_multi == true)
                            {
                                decimal _tovalue = _init[0].Saldf_value_to;
                                if (_tovalue == 0) _lolpt += 0; else _lolpt += (_itemvalue / _tovalue) * _init[0].Saldf_pt;
                                _isfound = true;
                            }
                            else if (_init[0].Saldf_is_multi == false)
                            {
                                int _seqance = _init[0].Saldf_seq;
                                var _slabs = _init.Where(x => x.Saldf_seq == _seqance).OrderBy(x => x.Saldf_line).ToList();
                                var _result = _slabs.Where(x => x.Saldf_value_frm <= _itemvalue && x.Saldf_value_to >= _itemvalue).ToList();
                                if (_result != null) if (_result.Count > 0)
                                        _lolpt += _result[0].Saldf_pt;
                                _isfound = true;
                            }
                        }
                }
                if (_isfound) return _lolpt;
            }
            return _lolpt;
        }

        //Tharaka 2015-03-23
        public List<LoyaltyPointDefinition> GetFesibleLoyaltyDefinition(string _company, string _loyaltytype, string _membership, string _partytype, string _partycode, DateTime _date, InvoiceItem _invoiceitem, List<InvoiceSerial> _seriallist, List<RecieptItem> _receiptdetail, SalesDAL _salDll, InventoryDAL _invDll)
        {
            List<LoyaltyPointDefinition> _returnlist = null;
            Int32 _isDis = 0;
            Int32 _isIns = 0;
            Int32 _pTP = 1000;
            if (_invoiceitem.Sad_disc_amt > 0)
            {
                _isDis = 1;
            }

            //Get price type
            DataTable _pbType = _salDll.GetPTypeByInvSeq(_invoiceitem.Sad_pbook, _invoiceitem.Sad_pb_lvl, _invoiceitem.Sad_itm_seq, _invoiceitem.Sad_seq);

            if (_pbType != null)
            {
                if (_pbType.Rows.Count > 0)
                {
                    foreach (DataRow drow in _pbType.Rows)
                    {
                        _pTP = Convert.ToInt32(drow["sapd_price_type"]);
                    }
                }
            }

            //Check Installment basis
            foreach (RecieptItem _rec in _receiptdetail)
            {
                if (_rec.Sard_cc_period > 0)
                {
                    _isIns = 1;
                }
            }

            List<LoyaltyPointDefinition> _initial = _salDll.GetLoyaltyDefinition(_loyaltytype, _membership, _partytype, _partycode, _invoiceitem.Sad_pbook, _invoiceitem.Sad_pb_lvl, _date.Date, _isDis, _pTP, _isIns);
            if (_initial == null || _initial.Count <= 0)
                return _returnlist;

            if (_receiptdetail == null || _receiptdetail.Count <= 0)
            {
                List<LoyaltyPointDefinition> _intermediate = new List<LoyaltyPointDefinition>();
                _intermediate = GetFesibleLoyaltyPayMode(_initial, null);
                return _intermediate;
            }

            if (_initial != null) if (_initial.Count > 0)
                {
                    List<LoyaltyPointDefinition> _serialitem0 = null;
                    List<LoyaltyPointDefinition> _intermediate = new List<LoyaltyPointDefinition>();
                    MasterItem _itmdet = _invDll.GetItem(_company, _invoiceitem.Sad_itm_cd);
                    Int16 _isSerialized = Convert.ToInt16(_salesDAL.GetPriceLevel(_company, _invoiceitem.Sad_pbook, _invoiceitem.Sad_pb_lvl).Sapl_is_serialized);

                    //Check for serial/item only
                    if (_isSerialized == 1)
                        if (_seriallist != null) if (_seriallist.Count > 0)
                            {
                                foreach (InvoiceSerial _serial in _seriallist)
                                { _serialitem0 = _initial.Where(x => x.Saldf_ser == _serial.Sap_ser_1 && x.Saldf_itm == _invoiceitem.Sad_itm_cd && string.IsNullOrEmpty(x.Saldf_brand) && string.IsNullOrEmpty(x.Saldf_cat_1) && string.IsNullOrEmpty(x.Saldf_cat_2)).ToList(); if (_serialitem0 != null) if (_serialitem0.Count > 0) { _intermediate = GetFesibleLoyaltyPayMode(_serialitem0, _receiptdetail); if (_intermediate != null) if (_intermediate.Count > 0) return _intermediate; } }
                            }

                    //Check for promotion only
                    if (!string.IsNullOrEmpty(_invoiceitem.Sad_promo_cd))
                    {
                        var _promo1 = _initial.Where(x => x.Saldf_promo == _invoiceitem.Sad_promo_cd && string.IsNullOrEmpty(x.Saldf_brand) && string.IsNullOrEmpty(x.Saldf_cat_1) && string.IsNullOrEmpty(x.Saldf_cat_2) && string.IsNullOrEmpty(x.Saldf_itm) && x.Saldf_qt_frm <= _invoiceitem.Sad_qty && x.Saldf_qt_to >= _invoiceitem.Sad_qty).ToList();
                        if (_promo1 != null) if (_promo1.Count > 0) { _intermediate = GetFesibleLoyaltyPayMode(_promo1, _receiptdetail); if (_intermediate != null) if (_intermediate.Count > 0) return _intermediate; }
                    }

                    //Check for Item
                    var _item2 = _initial.Where(x => x.Saldf_itm == _invoiceitem.Sad_itm_cd && string.IsNullOrEmpty(x.Saldf_brand) && string.IsNullOrEmpty(x.Saldf_cat_1) && string.IsNullOrEmpty(x.Saldf_cat_2) && x.Saldf_qt_frm <= _invoiceitem.Sad_qty && x.Saldf_qt_to >= _invoiceitem.Sad_qty).ToList();
                    if (_item2 != null) if (_item2.Count > 0) { _intermediate = GetFesibleLoyaltyPayMode(_item2, _receiptdetail); if (_intermediate != null) if (_intermediate.Count > 0) return _intermediate; }

                    //Check for Brand/Category 1
                    var _brandcate13 = _initial.Where(x => string.IsNullOrEmpty(x.Saldf_promo) && x.Saldf_brand == _itmdet.Mi_brand && x.Saldf_cat_1 == _itmdet.Mi_cate_1 && string.IsNullOrEmpty(x.Saldf_cat_2) && string.IsNullOrEmpty(x.Saldf_itm) && x.Saldf_qt_frm <= _invoiceitem.Sad_qty && x.Saldf_qt_to >= _invoiceitem.Sad_qty).ToList();
                    if (_brandcate13 != null) if (_brandcate13.Count > 0) { _intermediate = GetFesibleLoyaltyPayMode(_brandcate13, _receiptdetail); if (_intermediate != null) if (_intermediate.Count > 0) return _intermediate; }

                    //Check for Brand/Category 2
                    var _brandcate24 = _initial.Where(x => string.IsNullOrEmpty(x.Saldf_promo) && x.Saldf_brand == _itmdet.Mi_brand && string.IsNullOrEmpty(x.Saldf_cat_1) && x.Saldf_cat_2 == _itmdet.Mi_cate_2 && string.IsNullOrEmpty(x.Saldf_itm) && x.Saldf_qt_frm <= _invoiceitem.Sad_qty && x.Saldf_qt_to >= _invoiceitem.Sad_qty).ToList();
                    if (_brandcate24 != null) if (_brandcate24.Count > 0) { _intermediate = GetFesibleLoyaltyPayMode(_brandcate24, _receiptdetail); if (_intermediate != null) if (_intermediate.Count > 0) return _intermediate; }

                    //Check for Brand
                    var _brand5 = _initial.Where(x => string.IsNullOrEmpty(x.Saldf_promo) && x.Saldf_brand == _itmdet.Mi_brand && string.IsNullOrEmpty(x.Saldf_cat_1) && string.IsNullOrEmpty(x.Saldf_cat_2) && string.IsNullOrEmpty(x.Saldf_itm) && x.Saldf_qt_frm <= _invoiceitem.Sad_qty && x.Saldf_qt_to >= _invoiceitem.Sad_qty).ToList();
                    if (_brand5 != null) if (_brand5.Count > 0) { _intermediate = GetFesibleLoyaltyPayMode(_brand5, _receiptdetail); if (_intermediate != null) if (_intermediate.Count > 0) return _intermediate; }

                    //Check pay mode only
                    var _payMode = _initial.Where(x => string.IsNullOrEmpty(x.Saldf_promo) && string.IsNullOrEmpty(x.Saldf_brand) && string.IsNullOrEmpty(x.Saldf_cat_1) && string.IsNullOrEmpty(x.Saldf_itm) && string.IsNullOrEmpty(x.Saldf_cat_2) && x.Saldf_qt_frm <= _invoiceitem.Sad_qty && x.Saldf_qt_to >= _invoiceitem.Sad_qty).ToList();
                    if (_payMode != null) if (_payMode.Count > 0) { _intermediate = GetFesibleLoyaltyPayMode(_payMode, _receiptdetail); if (_intermediate != null) if (_intermediate.Count > 0) return _intermediate; }

                    //If there is no filter one
                    var _allnull6 = _initial.Where(x => string.IsNullOrEmpty(x.Saldf_brand) && string.IsNullOrEmpty(x.Saldf_cat_1) && string.IsNullOrEmpty(x.Saldf_cat_2) && string.IsNullOrEmpty(x.Saldf_itm) && string.IsNullOrEmpty(x.Saldf_promo) && string.IsNullOrEmpty(x.Saldf_ser) && string.IsNullOrEmpty(x.Saldf_pmod) && x.Saldf_qt_frm <= _invoiceitem.Sad_qty && x.Saldf_qt_to >= _invoiceitem.Sad_qty).ToList();
                    if (_allnull6 != null) if (_allnull6.Count > 0) { _returnlist = _allnull6; return _returnlist; }
                }
            return _returnlist;
        }

        //Tharaka 2015-03-23
        public List<LoyaltyPointDefinition> GetFesibleLoyaltyPayMode(List<LoyaltyPointDefinition> _foundlist, List<RecieptItem> _receiptdetail)
        {
            List<LoyaltyPointDefinition> _returnlist = null;
            if (_receiptdetail == null || _receiptdetail.Count <= 0)
            {
                var _wopmode = _foundlist.Where(x => string.IsNullOrEmpty(x.Saldf_pmod)).ToList();
                if (_wopmode != null)
                {
                    if (_wopmode.Count > 0)
                    {
                        _returnlist = _wopmode; return _returnlist;
                    }
                    else return _returnlist;
                }
                else return _returnlist;
            }

            foreach (RecieptItem _one in _receiptdetail)
            {
                var _pmode = _foundlist.Where(x => x.Saldf_pmod == _one.Sard_pay_tp).ToList();

                if (_one.Sard_pay_tp.ToUpper() == "CRCD")
                {
                    var _bankNtype0 = _pmode.Where(x => x.Saldf_bank == _one.Sard_credit_card_bank && x.Saldf_cd_tp == _one.Sard_cc_tp).ToList();
                    if (_bankNtype0 != null) if (_bankNtype0.Count > 0) { _returnlist = _bankNtype0; return _returnlist; }

                    var _bankNtype1 = _pmode.Where(x => x.Saldf_bank == _one.Sard_credit_card_bank && string.IsNullOrEmpty(x.Saldf_cd_tp)).ToList();
                    if (_bankNtype1 != null) if (_bankNtype1.Count > 0) { _returnlist = _bankNtype1; return _returnlist; }

                    var _bankNtype2 = _pmode.Where(x => string.IsNullOrEmpty(x.Saldf_bank) && x.Saldf_cd_tp == _one.Sard_cc_tp).ToList();
                    if (_bankNtype2 != null) if (_bankNtype2.Count > 0) { _returnlist = _bankNtype2; return _returnlist; }

                    var _bankNtype3 = _pmode.Where(x => string.IsNullOrEmpty(x.Saldf_bank) && string.IsNullOrEmpty(x.Saldf_cd_tp)).ToList();
                    if (_bankNtype3 != null) if (_bankNtype3.Count > 0) { _returnlist = _bankNtype3; return _returnlist; }
                }
                else if (_one.Sard_pay_tp.ToUpper() == "CHEQUE")
                {
                    var _bank0 = _pmode.Where(x => x.Saldf_bank == _one.Sard_chq_bank_cd).ToList();
                    if (_bank0 != null) if (_bank0.Count > 0) { _returnlist = _bank0; return _returnlist; }

                    var _banknull1 = _pmode.Where(x => string.IsNullOrEmpty(x.Saldf_bank)).ToList();
                    if (_banknull1 != null) if (_banknull1.Count > 0) { _returnlist = _banknull1; return _returnlist; }
                }
                else if (_one.Sard_pay_tp.ToUpper() == "DEBIT")
                {
                    var _bank0 = _pmode.Where(x => x.Saldf_bank == _one.Sard_chq_bank_cd).ToList();
                    if (_bank0 != null) if (_bank0.Count > 0) { _returnlist = _bank0; return _returnlist; }

                    var _banknull1 = _pmode.Where(x => string.IsNullOrEmpty(x.Saldf_bank)).ToList();
                    if (_banknull1 != null) if (_banknull1.Count > 0) { _returnlist = _banknull1; return _returnlist; }
                }
                else if (_one.Sard_pay_tp.ToUpper() == "CASH")
                {
                    var _paymode0 = _pmode.Where(x => x.Saldf_pmod == _one.Sard_pay_tp).ToList();
                    if (_paymode0 != null) if (_paymode0.Count > 0) { _returnlist = _paymode0; return _returnlist; }
                }
                else if (_one.Sard_pay_tp.ToUpper() == "ADVAN")
                {
                    var _paymode0 = _pmode.Where(x => x.Saldf_pmod == _one.Sard_pay_tp).ToList();
                    if (_paymode0 != null) if (_paymode0.Count > 0) { _returnlist = _paymode0; return _returnlist; }
                }
                else if (_one.Sard_pay_tp.ToUpper() == "CRNOTE")
                {
                    var _paymode0 = _pmode.Where(x => x.Saldf_pmod == _one.Sard_pay_tp).ToList();
                    if (_paymode0 != null) if (_paymode0.Count > 0) { _returnlist = _paymode0; return _returnlist; }
                }
                else if (_one.Sard_pay_tp.ToUpper() == "GVO")
                {
                    var _paymode0 = _pmode.Where(x => x.Saldf_pmod == _one.Sard_pay_tp).ToList();
                    if (_paymode0 != null) if (_paymode0.Count > 0) { _returnlist = _paymode0; return _returnlist; }
                }
                else if (_one.Sard_pay_tp.ToUpper() == "GVS")
                {
                    var _paymode0 = _pmode.Where(x => x.Saldf_pmod == _one.Sard_pay_tp).ToList();
                    if (_paymode0 != null) if (_paymode0.Count > 0) { _returnlist = _paymode0; return _returnlist; }
                }
                else if (_one.Sard_pay_tp.ToUpper() == "LORE")
                {
                    var _paymode0 = _pmode.Where(x => x.Saldf_pmod == _one.Sard_pay_tp).ToList();
                    if (_paymode0 != null) if (_paymode0.Count > 0) { _returnlist = _paymode0; return _returnlist; }
                }

                var _allnull0 = _pmode.Where(x => string.IsNullOrEmpty(x.Saldf_cat_1) && string.IsNullOrEmpty(x.Saldf_cat_2) && string.IsNullOrEmpty(x.Saldf_itm) && string.IsNullOrEmpty(x.Saldf_promo) && string.IsNullOrEmpty(x.Saldf_ser)).ToList();
                if (_allnull0 != null) if (_allnull0.Count > 0) { _returnlist = _allnull0; return _returnlist; }

                var _wopmode = _foundlist.Where(x => string.IsNullOrEmpty(x.Saldf_pmod)).ToList();
                if (_wopmode != null) if (_wopmode.Count > 0) { _returnlist = _wopmode; return _returnlist; }
            }

            return _returnlist;
        }

        //Tharaka 2015-03-26
        public Int32 UpdateEnquiryStage(Int32 Stage, String user, String enquiryID, String com, String PC, out String err)
        {
            Int32 result = 0;
            err = string.Empty;

            try
            {
                _ToursDAL = new ToursDAL();
                result = _ToursDAL.UpdateEnquiryStage(Stage, user, enquiryID, com, PC);

                result = 1;
            }
            catch (Exception ex)
            {
                result = 0;
                err = ex.Message;
            }

            return result;
        }

        //Tharaka 2015-03-26
        public int GetInvoiceDetails(string Com, string PC, string InvoiceNum, out InvoiceHeader oheader, out List<InvoiceItem> oMainItems, out RecieptHeader oRecieptHeader, out List<RecieptItem> oRecieptItems, out String err)
        {
            int result = 0;
            err = string.Empty;

            _ToursDAL = new ToursDAL();
            oheader = new InvoiceHeader();
            oMainItems = new List<InvoiceItem>();
            oRecieptHeader = new RecieptHeader();
            oRecieptItems = new List<RecieptItem>();
            try
            {
                oheader = _ToursDAL.GetInvoiceHeader(Com, PC, InvoiceNum);
                if (oheader != null && oheader.Sah_inv_no != null)
                {
                    oMainItems = _ToursDAL.GetInvoiceDetail(oheader.Sah_seq_no);
                    oRecieptHeader = _ToursDAL.GetRecieptHeader(oheader.Sah_seq_no);
                    oRecieptItems = _ToursDAL.GetRecieptItems(oheader.Sah_seq_no);
                }
            }
            catch (Exception ex)
            {
                result = -1;
                err = ex.Message;
            }

            return result;
        }

        //Tharaka 2015-03-31
        public int UPDATE_COST_HDR_STATUS(Int32 StageEnqiry, Int32 costHDRStatus, Int32 SeqCost, String com, String pc, String User, String enquiryID, out string err, bool updatePo = false, QUO_COST_HDR oHeader = null)
        {
            int result = 0;
            err = string.Empty;

            try
            {
                _ToursDAL = new ToursDAL();
                _ToursDAL.ConnectionOpen();
                _ToursDAL.BeginTransaction();

                result = _ToursDAL.UPDATE_COST_HDR_STATUS(costHDRStatus, SeqCost, com, pc);

                result = _ToursDAL.UpdateEnquiryStage(StageEnqiry, User, enquiryID, com, pc);
                if (updatePo)
                {
                    result = _ToursDAL.UpdatePOStatus(enquiryID, User, DateTime.Now);
                }
                if (oHeader != null)
                {
                    result = _ToursDAL.UpdateCostHeaderCustomerApprove(oHeader);
                }
                GEN_ENQLOG oLog = new GEN_ENQLOG();
                oLog.GEL_ENQ_ID = enquiryID;
                oLog.GEL_USR = User;
                oLog.GEL_STAGE = StageEnqiry;
                oLog.GEL_LOGWHEN = DateTime.Now;
                result = _ToursDAL.SAVE_GEN_ENQLOG(oLog);

                _ToursDAL.TransactionCommit();
                _ToursDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                err = ex.Message;
                _ToursDAL.TransactionRollback();
                throw;
            }
            return result;
        }

        //Tharaka 2015-03-30
        public SR_AIR_CHARGE GetChargeDetailsByCode(string com, String Cate, string Code, string pc)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.GetChargeDetailsByCode(com, Cate, Code, pc);
        }

        //Tharaka 2015-04-01
        public SR_TRANS_CHA GetTransferChargeDetailsByCode(string com, String Cate, string Code, string pc)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.GetTransferChargeDetailsByCode(com, Cate, Code, pc);
        }

        //Tharaka 2015-04-02
        public SR_SER_MISS GetMiscellaneousChargeDetailsByCode(string com, String Cate, string Code, string PC)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.GetMiscellaneousChargeDetailsByCode(com, Cate, Code, PC);
        }

        //Tharaka 2015-04-06
        public List<PriceDefinitionRef> GetToursPriceDefByBookAndLevel(string _company, string _book, string _level, string _invoiceType, string _profitCenter)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.GetToursPriceDefByBookAndLevel(_company, _book, _level, _invoiceType, _profitCenter);
        }

        //Tharaka 2015-04-07
        public List<PriceDetailRef> GetToursPrice(string _company, string _profitCenter, string _invType, string _priceBook, string _priceLevel, string _customer, string _item, decimal _qty, DateTime _currentDate)
        {
            _ToursDAL = new ToursDAL();
            //Check the price for the specific customer availabillity (even for special promotions)
            //Check the price for special promotion without Customer
            //Check the price for normal price
            //If no price =>message

            List<PriceDetailRef> _priceDetailRef = new List<PriceDetailRef>();
            List<PriceDetailRef> _returnlist = new List<PriceDetailRef>();

            //1.With customer
            _priceDetailRef = _ToursDAL.GetToursPriceDetail(_priceBook, _priceLevel, _item, _qty, _currentDate, _customer);

            //2.Without Customer
            if (_priceDetailRef.Count <= 0)
            {
                _priceDetailRef = _ToursDAL.GetToursPriceDetail(_priceBook, _priceLevel, _item, _qty, _currentDate, string.Empty);
            }

            List<PriceDetailRef> _lists = new List<PriceDetailRef>();
            var _types = _priceDetailRef.Select(x => x.Sapd_price_type).Distinct();
            //add only price type 0 prices
            foreach (var _type in _types)
            {
                if (_type != 0)
                {
                    var _lst = _priceDetailRef.Where(x => x.Sapd_price_type == _type).ToList();
                    if (_lst != null)
                        if (_lst.Count > 0) ;
                    // _lists.AddRange(_lst);
                }
                else
                {
                    var _lst = _priceDetailRef.Where(x => x.Sapd_price_type == _type).ToList();
                    if (_lst != null)
                        if (_lst.Count > 0)
                            _lists.Add(_lst[0]);
                }
            }
            return _lists;
        }

        //Tharaka 2015-04-08
        public List<ComboBoxObject> GetServiceClasses(string com, String Cate)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.GetServiceClasses(com, Cate);
        }

        //Tharaka 2015-04-08
        public List<ComboBoxObject> GetServiceProviders(string com, String Cate)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.GetServiceProviders(com, Cate);
        }

        //Tharaka 2015-04-08
        public Int32 SaveAirChageCodes(SR_AIR_CHARGE lst, out string err)
        {
            int resutl = 0;
            err = string.Empty;

            try
            {
                _ToursDAL = new ToursDAL();
                _ToursDAL.ConnectionOpen();

                resutl = _ToursDAL.SaveAitChageCodes(lst);

                _ToursDAL.TransactionCommit();
                _ToursDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                _ToursDAL.TransactionRollback();
                err = ex.Message;
            }

            return resutl;
        }

        //Tharaka 2015-04-09
        public Int32 SaveTrasportChageCodes(SR_TRANS_CHA lst, out string err)
        {
            int resutl = 0;
            err = string.Empty;

            try
            {
                _ToursDAL = new ToursDAL();
                _ToursDAL.ConnectionOpen();

                resutl = _ToursDAL.SaveTrasportChageCodes(lst);

                _ToursDAL.TransactionCommit();
                _ToursDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                _ToursDAL.TransactionRollback();
                err = ex.Message;
            }

            return resutl;
        }

        //Tharaka 2015-04-09
        public Int32 SaveMiscellaneousChageCodes(SR_SER_MISS lst, out string err)
        {
            int resutl = 0;
            err = string.Empty;

            try
            {
                _ToursDAL = new ToursDAL();
                _ToursDAL.ConnectionOpen();

                resutl = _ToursDAL.SaveMiscellaneousChageCodes(lst);

                _ToursDAL.TransactionCommit();
                _ToursDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                _ToursDAL.TransactionRollback();
                err = ex.Message;
            }

            return resutl;
        }

        //Tharaka 2015-04-21
        public int SendSMS_InternalMethod(OutSMS smsOut, out String err)
        {
            int result = 0;
            err = string.Empty;

            try
            {
                //_generalDAL = new GeneralDAL();
                //_generalDAL.ConnectionOpen();
                //_generalDAL.BeginTransaction();
                smsOut.Msg = smsOut.Msg.Replace(@"\n", Environment.NewLine).ToString();
                result = _generalDAL.SaveSMSOut(smsOut);
                //_generalDAL.TransactionCommit();
                //_generalDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                result = -1;
                err = ex.Message;
            }

            return result;
        }

        //Tharaka 2015-04-21
        public int SendSMS(OutSMS smsOut, out String err)
        {
            int result = 0;
            err = string.Empty;

            try
            {
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                smsOut.Msg = smsOut.Msg.Replace(@"\n", Environment.NewLine).ToString();
                result = _generalDAL.SaveSMSOut(smsOut);
                _generalDAL.TransactionCommit();
                _generalDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                result = -1;
                err = ex.Message;
            }

            return result;
        }

        //Tharaka 2015-05-13
        public Int32 UpdateEnquiryStageWithlog(Int32 Stage, String user, String enquiryID, String com, String PC, out string err)
        {
            int resutl = 0;
            err = string.Empty;

            try
            {
                _ToursDAL = new ToursDAL();
                _ToursDAL.ConnectionOpen();

                GEN_ENQLOG oLog = new GEN_ENQLOG();
                oLog.GEL_ENQ_ID = enquiryID;
                oLog.GEL_USR = user;
                oLog.GEL_STAGE = Stage;
                oLog.GEL_LOGWHEN = DateTime.Now;

                resutl = _ToursDAL.SAVE_GEN_ENQLOG(oLog);

                resutl = _ToursDAL.UpdateEnquiryStage(Stage, user, enquiryID, com, PC);

                _ToursDAL.TransactionCommit();
                _ToursDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                _ToursDAL.TransactionRollback();
                err = ex.Message;
            }

            return resutl;
        }

        public DataTable Get_CostingFormat(string costNumber)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.Get_CostingFormat(costNumber);
        }

        //Tharaka 2015-05-27 
        public RecieptHeaderTBS GetReceiptHeaderTBS(string _com, string _pc, string _doc)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.GetReceiptHeaderTBS(_com, _pc, _doc);
        }

        //Tharaka 2015-05-28
        public List<ComboBoxObject> GET_TOUR_PACKAGE_TYPES()
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.GET_TOUR_PACKAGE_TYPES();
        }

        //Tharaka 2015-05-28
        public Int32 UPDATE_INVOICE_STATUS(string status, string user, string com, string pc, string invoice, out string err)
        {
            int resutl = 0;
            err = string.Empty;

            try
            {
                _ToursDAL = new ToursDAL();
                _ToursDAL.ConnectionOpen();

                resutl = _ToursDAL.UPDATE_INVOICE_STATUS(status, user, com, pc, invoice);

                _ToursDAL.TransactionCommit();
                _ToursDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                _ToursDAL.TransactionRollback();
                err = ex.Message;
            }

            return resutl;
        }

        //Pemil 2015-06-01
        public List<Ref_Title> GET_REF_TITLE()
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.GET_REF_TITLE();
        }

        //Pemil 2015-06-03
        public List<Mst_empcate> Get_mst_empcate()
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.Get_mst_empcate();
        }

        //Pemil 2015-06-03
        public List<MST_VEH_TP> Get_mst_veh_tp()
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.Get_mst_veh_tp();
        }

        //Pemil 2015-06-03
        public DataTable Get_mst_profit_center(string com)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.Get_mst_profit_center(com);
        }

        //Pemil 2015-06-03
        public Int32 SaveEmployee(MST_EMPLOYEE_TBS mst_employee_tbs, List<MST_PCEMP> mst_pcemp, out string err)
        {
            int count = 0;
            err = string.Empty;
            try
            {
                _ToursDAL = new ToursDAL();
                _ToursDAL.ConnectionOpen();

                count = _ToursDAL.SaveEmployee(mst_employee_tbs);

                if (count == 1)
                {
                    foreach (MST_PCEMP pcemp in mst_pcemp)
                    {
                        count = _ToursDAL.SavePcemp(pcemp);
                    }
                }

                _ToursDAL.TransactionCommit();
                _ToursDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                _ToursDAL.TransactionRollback();
                err = ex.Message;
            }

            return count;
        }

        //Pemil 2015-06-04
        public Int32 UpdateEmployee(MST_EMPLOYEE_TBS mst_employee_tbs, List<MST_PCEMP> mst_pcemp, out string err)
        {
            int count = 0;
            err = string.Empty;
            try
            {
                _ToursDAL = new ToursDAL();
                _ToursDAL.ConnectionOpen();

                count = _ToursDAL.UpdateEmployee(mst_employee_tbs);

                if (count == 1)
                {
                    foreach (MST_PCEMP pcemp in mst_pcemp)
                    {
                        int coutItm = _ToursDAL.UpdatePcemp(pcemp);
                    }
                }

                _ToursDAL.TransactionCommit();
                _ToursDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                _ToursDAL.TransactionRollback();
                err = ex.Message;
            }

            return count;
        }

        //Pemil 2015-06-04
        public MST_EMPLOYEE_TBS Get_mst_employee(string memp_epf)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.Get_mst_employee(memp_epf);
        }

        //Pemil 2015-06-04
        public List<MST_PCEMP> Get_mst_pcemp(string mpe_epf)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.Get_mst_pcemp(mpe_epf);
        }

        //Tharaka 2015-06-03
        public List<ComboBoxObject> GET_ALL_TOWN_FOR_COMBO()
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.GET_ALL_TOWN_FOR_COMBO();
        }

        //Tharaka 2015-06-03
        public List<ComboBoxObject> GET_ALL_VEHICLE_FOR_COMBO()
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.GET_ALL_VEHICLE_FOR_COMBO();
        }

        //Pemil 2015-06-05
        public DataTable Get_gen_cust_enq(string com, string PC, string enq_tp, string fleet, string driver, string cus_cd, DateTime? fromDate, DateTime? toDate)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.Get_gen_cust_enq(com, PC, enq_tp, fleet, driver, cus_cd, fromDate, toDate);
        }

        //Tharaka 2015-06-06
        public MST_EMPLOYEE_TBS GetEmployeeByComPC(String com, String PC, String EPF)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.GetEmployeeByComPC(com, PC, EPF);
        }

        //Tharaka 2015-06-08
        public Int32 SaveTripRequestWithInvoice(InvoiceHeader _invoiceHeader, List<InvoiceItem> _invoiceItem, List<InvoiceSerial> _invoiceSerial, RecieptHeader _recieptHeader, List<RecieptItem> _recieptItem, InventoryHeader _inventoryHeader, List<ReptPickSerials> _pickSerial, List<ReptPickSerialsSub> _pickSubSerial, MasterAutoNumber _invoiceAuto, MasterAutoNumber _recieptAuto, MasterAutoNumber _inventoryAuto, bool _isDeliveryNow, out  string _invoiceNo, out string _receiptNo, out string _deliveryOrder, MasterBusinessEntity _businessCompany, bool _isHold, bool _isHoldInvoiceProcess, out string _errorlist, List<InvoiceVoucher> _voucher, InventoryHeader _buybackheader, MasterAutoNumber _buybackauto, List<ReptPickSerials> _buybacklist, out string BuyBackInvNo, GEN_CUST_ENQ oItem, MasterAutoNumber _ReqInsAuto, out string errEnquiry, bool isInvoice, GEN_CUST_ENQSER _genCustEnqser = null)
        {
            string _invNo = string.Empty;
            string _recNo = string.Empty;
            string _DONo = string.Empty;
            string _buybackinv = string.Empty;
            Int32 _effect = 0;
            string _location = string.Empty;
            string _error = string.Empty;
            string _db = string.Empty;
            MasterAutoNumber _auto = null;

            Boolean _isNew = false;
            string errMSG = string.Empty;
            errEnquiry = string.Empty;

            try
            {
                _db = DataBase._ems;
                _salesDAL = new SalesDAL();
                _salesDAL.ConnectionOpen();
                _salesDAL.BeginTransaction();
                _db = DataBase._ems;
                _inventoryDAL = new InventoryDAL();
                _inventoryDAL.ConnectionOpen();
                _inventoryDAL.BeginTransaction();
                _db = DataBase._fms;
                _fmsInventoryDal = new FMS_InventoryDAL();
                _fmsInventoryDal.ConnectionOpen();
                _fmsInventoryDal.BeginTransaction();
                _db = DataBase._reportdb;
                _inventoryRepDAL = new ReptCommonDAL();
                _inventoryRepDAL.ConnectionOpen();
                _inventoryRepDAL.BeginTransaction();
                _db = DataBase._ems;
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                _db = DataBase._ems;
                _ToursDAL = new ToursDAL();
                _ToursDAL.ConnectionOpen();
                _ToursDAL.BeginTransaction();


                if (String.IsNullOrEmpty(oItem.GCE_ENQ_ID) && oItem.GCE_SEQ == 0)
                {
                    MasterAutoNumber _reversInv = _inventoryDAL.GetAutoNumber(_ReqInsAuto.Aut_moduleid, _ReqInsAuto.Aut_direction, _ReqInsAuto.Aut_start_char, _ReqInsAuto.Aut_cate_tp, _ReqInsAuto.Aut_cate_cd, _ReqInsAuto.Aut_modify_dt, _ReqInsAuto.Aut_year);
                    _reversInv.Aut_direction = null;
                    _reversInv.Aut_modify_dt = null;
                    errEnquiry = _reversInv.Aut_cate_cd + "-" + _reversInv.Aut_start_char + "-" + Convert.ToString(DateTime.Now.Date.Year).Remove(0, 2) + "-" + _reversInv.Aut_number.ToString("00000", CultureInfo.InvariantCulture);
                    _inventoryDAL.UpdateAutoNumber(_reversInv);
                    oItem.GCE_ENQ_ID = errEnquiry;
                    _isNew = true;
                }

                _effect = _ToursDAL.Save_GEN_CUST_ENQ(oItem);

                if (_isNew == true)
                {
                    errEnquiry = "Successfully Created , Enquiry ID  " + oItem.GCE_ENQ_ID;
                }
                else
                {
                    errEnquiry = "Successfully Updated , Enquiry ID  " + oItem.GCE_ENQ_ID;
                }

                if (isInvoice)
                {
                    if (!_isHold)
                    {
                        _invoiceAuto.Aut_year = null;
                        MasterAutoNumber InvoiceAuto = _inventoryDAL.GetAutoNumber(_invoiceAuto.Aut_moduleid, _invoiceAuto.Aut_direction, _invoiceAuto.Aut_start_char, _invoiceAuto.Aut_cate_tp, _invoiceAuto.Aut_cate_cd, _invoiceAuto.Aut_modify_dt, _invoiceAuto.Aut_year);
                        _invNo = _invoiceAuto.Aut_start_char + InvoiceAuto.Aut_number.ToString("00000", CultureInfo.InvariantCulture);
                        _invoiceAuto.Aut_year = null;
                        _invoiceAuto.Aut_modify_dt = null;
                        _salesDAL.UpdateInvoiceAutoNumber(_invoiceAuto);
                        _invoiceHeader.Sah_inv_no = _invNo;
                    }

                    _db = string.Empty;
                    _location = (_inventoryHeader != null && !string.IsNullOrEmpty(_inventoryHeader.Ith_com)) ? _inventoryHeader.Ith_loc : string.Empty;
                    string _recieptSeq = null;
                    string _invoiceSeq = null;
                    InventoryHeader _invHdr = null;
                    DataTable _dataTable = null;
                    CommonSaveInvoiceWithDeliveryOrderWithTransaction(_invoiceHeader, _invoiceItem, _invoiceSerial, _recieptHeader, _recieptItem, _inventoryHeader, _pickSerial, _pickSubSerial, _invoiceAuto, _recieptAuto, _inventoryAuto, _isDeliveryNow, out _invNo, out _recNo, out _DONo, _inventoryDAL, _salesDAL, _inventoryRepDAL, _isHold, _isHoldInvoiceProcess, out _error, false, out _invoiceSeq, out _recieptSeq, out _invHdr, out _dataTable, "");
                    if (_genCustEnqser != null)
                    {
                        _genCustEnqser.GCS_SEQ = Convert.ToInt32(_invoiceSeq);
                        _genCustEnqser.GCS_ENQ_ID = oItem.GCE_ENQ_ID;
                        _ToursDAL.SAVE_GEN_ENQSER(_genCustEnqser);
                    }
                    Int32 result = _ToursDAL.UpdateEnquiryStage(5, _invoiceHeader.Sah_cre_by, _invoiceHeader.Sah_ref_doc, _invoiceHeader.Sah_com, _invoiceHeader.Sah_pc);
                    GEN_ENQLOG oLog = new GEN_ENQLOG();
                    oLog.GEL_ENQ_ID = _invoiceHeader.Sah_ref_doc;
                    oLog.GEL_USR = _invoiceHeader.Sah_cre_by;
                    oLog.GEL_STAGE = 5;
                    oLog.GEL_LOGWHEN = DateTime.Now;
                    result = _ToursDAL.SAVE_GEN_ENQLOG(oLog);

                    string _customerCode = _invoiceHeader.Sah_cus_cd;
                    GroupBussinessEntity _businessEntityGrup = new GroupBussinessEntity();
                    if (string.IsNullOrEmpty(_error))
                    {
                        #region update auto no

                        if (!_isHold)
                        {
                            if (_recieptAuto != null)
                            {
                                _recieptAuto.Aut_year = null;
                                MasterAutoNumber RecieptAuto = _inventoryDAL.GetAutoNumber(_recieptAuto.Aut_moduleid, _recieptAuto.Aut_direction, _recieptAuto.Aut_start_char, _recieptAuto.Aut_cate_tp, _recieptAuto.Aut_cate_cd, _recieptAuto.Aut_modify_dt, _recieptAuto.Aut_year);
                                _receiptNo = _recieptAuto.Aut_cate_cd + "-" + RecieptAuto.Aut_start_char + RecieptAuto.Aut_number.ToString("00000", CultureInfo.InvariantCulture);
                                _recieptAuto.Aut_year = null;
                                _recieptAuto.Aut_modify_dt = null;
                                _recNo = _receiptNo;
                                if (_ToursDAL.CheckTBSSalesNo("SP_GETTBSRECEIPTNO", "p_rept_no", _recNo) == 1)
                                {
                                    _error = "Invoice process terminated. Please re-process.(Hint - Duplicating Receipt No)";
                                    _invoiceNo = string.Empty;
                                    _receiptNo = string.Empty;
                                    _deliveryOrder = string.Empty;
                                    _errorlist = _error;
                                    BuyBackInvNo = string.Empty;
                                    _effect = -1;

                                    _salesDAL.TransactionRollback();
                                    _inventoryDAL.TransactionRollback();
                                    _fmsInventoryDal.TransactionRollback();
                                    _inventoryRepDAL.TransactionRollback();
                                    _generalDAL.TransactionRollback();
                                    _ToursDAL.TransactionRollback();
                                    return _effect;
                                }
                                _salesDAL.UpdateInvoiceAutoNumber(_recieptAuto);
                            }

                            _ToursDAL.UpdateTourReceipt(_invNo, _recNo, Convert.ToInt32(_invoiceSeq), Convert.ToInt32(_recieptSeq));
                            _inventoryRepDAL.UpdateAdvanceReceiptNofromInvoice(string.Empty, Convert.ToString(_invoiceSeq), _invNo);
                        }

                        #endregion update auto no

                        #region Update Manual Doc

                        if (_invoiceHeader.Sah_manual) _fmsInventoryDal.UpdateManualDocNo(_location, "MDOC_INV", Convert.ToInt32(_invoiceHeader.Sah_ref_doc), _invNo);

                        #endregion Update Manual Doc

                        #region update invoice discount / Promotion Voucher page as F

                        foreach (InvoiceItem _itm in _invoiceItem)
                        {
                            if (_itm.Sad_dis_type == "P")
                            {
                                _salesDAL.UpdateDiscountUsedTimes(_itm.Sad_dis_seq, 1);
                            }
                        }

                        #endregion update invoice discount / Promotion Voucher page as F

                        _effect = 1;

                    }
                    else
                    {
                        _effect = -1;
                    }
                }
                try
                {
                    if (string.IsNullOrEmpty(_error))
                    {
                        _db = DataBase._ems;
                        _salesDAL.TransactionCommit();
                        _db = DataBase._ems;
                        _inventoryDAL.TransactionCommit();
                        _db = DataBase._fms;
                        _fmsInventoryDal.TransactionCommit();
                        _db = DataBase._reportdb;
                        _inventoryRepDAL.TransactionCommit();
                        _db = DataBase._ems;
                        _generalDAL.TransactionCommit();
                        _db = DataBase._ems;
                        _ToursDAL.TransactionCommit();

                        _inventoryDAL.UpdateInvoiceDOStatus(_invNo);

                        if (_auto != null)
                        {
                            _inventoryDAL.UpdateAutoNumber(_auto);
                        }
                    }
                    else
                    {
                        _salesDAL.TransactionRollback();
                        _inventoryDAL.TransactionRollback();
                        _fmsInventoryDal.TransactionRollback();
                        _inventoryRepDAL.TransactionRollback();
                        _generalDAL.TransactionRollback();
                        _ToursDAL.TransactionRollback();
                    }
                }
                catch (Exception ex)
                {
                    _invoiceNo = string.Empty;
                    _receiptNo = string.Empty;
                    _deliveryOrder = string.Empty;
                    _errorlist = "Database" + _db + " is not responding. Please contact IT Operation.\n" + ex.Message;
                    BuyBackInvNo = string.Empty;
                    _effect = -1;
                    return _effect;
                }
            }
            catch (Exception ex)
            {
                _error = ex.Message.ToString();
                _invoiceNo = string.Empty;
                _receiptNo = string.Empty;
                _deliveryOrder = string.Empty;
                _errorlist = _error;
                BuyBackInvNo = string.Empty;
                _effect = -1;

                _salesDAL.TransactionRollback();
                _inventoryDAL.TransactionRollback();
                _fmsInventoryDal.TransactionRollback();
                _inventoryRepDAL.TransactionRollback();
                _generalDAL.TransactionRollback();
                _ToursDAL.TransactionRollback();
            }

            _invoiceNo = _invNo;
            _receiptNo = _recNo;
            _deliveryOrder = _DONo;
            _errorlist = _error;
            BuyBackInvNo = _buybackinv;
            return _effect;
        }

        //Pemil 2015-06-04
        public Int32 Check_Employeeepf(string epf)
        {
            _ToursDAL = new ToursDAL();
            _ToursDAL.ConnectionOpen();
            int retu = _ToursDAL.Check_Employeeepf(epf);
            _ToursDAL.TransactionCommit();
            _ToursDAL.ConnectionClose();
            return retu;
        }

        //Pemil 2015-06-09
        public DataTable Get_triprequest(string enq_id)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.Get_triprequest(enq_id);
        }

        //Pemil 2015-06-12
        public DataTable Get_tour_searchreceipttype(string com, Int32 is_refund)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.Get_tour_searchreceipttype(com, is_refund);
        }

        //Pemil 2015-06-13
        public DataTable Get_tour_logsheet(string com, string pc, string dri_cd, DateTime from_dt, DateTime to_dt)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.Get_tour_logsheet(com, pc, dri_cd, from_dt, to_dt);
        }

        //Tharaka 2015-05-13
        public Int32 saveLogSheet(TR_LOGSHEET_HDR oHeader, List<TR_LOGSHEET_DET> oItems, bool isNew, MasterAutoNumber _auto, out String err, out String logNumber)
        {
            Int32 result = 0;
            err = string.Empty;
            Int32 SeqNumber = 0;
            logNumber = string.Empty;

            try
            {
                _ToursDAL = new ToursDAL();
                _ToursDAL.ConnectionOpen();

                _inventoryDAL = new InventoryDAL();
                _inventoryDAL.ConnectionOpen();

                if (isNew)
                {
                    if (oHeader.TLH_SEQ == 0)
                    {
                        SeqNumber = _ToursDAL.GETTOURLOGSEQ();

                        MasterAutoNumber _number = _inventoryDAL.GetAutoNumber(_auto.Aut_moduleid, (short)_auto.Aut_direction, _auto.Aut_start_char, _auto.Aut_cate_tp, _auto.Aut_cate_cd, _auto.Aut_modify_dt, _auto.Aut_year);
                        String newLogNumber = oHeader.TLH_COM + "/" + _auto.Aut_cate_cd + "-" + _auto.Aut_start_char + "-" + _number.Aut_number.ToString("000000", CultureInfo.InvariantCulture);
                        _inventoryDAL.UpdateAutoNumber(_auto);
                       // oHeader.TLH_LOG_NO = newLogNumber;
                        logNumber = newLogNumber;
                        logNumber = oHeader.TLH_LOG_NO;
                        oHeader.TLH_SEQ = SeqNumber;
                    }
                    else
                    {
                        SeqNumber = oHeader.TLH_SEQ;
                        logNumber = oHeader.TLH_LOG_NO;
                    }
                }
                else
                {
                    SeqNumber = oHeader.TLH_SEQ;
                    logNumber = oHeader.TLH_LOG_NO;
                }

                result = _ToursDAL.SAVE_LOGSHEETHEADER(oHeader);

                foreach (TR_LOGSHEET_DET item in oItems)
                {
                    item.TLD_SEQ = SeqNumber;
                    result = _ToursDAL.SAVE_LOGSHEETDET(item);
                }

                _ToursDAL.TransactionCommit();
                _ToursDAL.ConnectionClose();

                _inventoryDAL.TransactionCommit();
                _inventoryDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                _ToursDAL.TransactionRollback();

                _inventoryDAL.TransactionRollback();

                err = ex.Message;
                result = 0;
            }

            return result;
        }

        //Tharaka 2015-06-13
        public TR_LOGSHEET_HDR GetLogSheetHeader(String com, String PC, String LOG)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.GetLogSheetHeader(com, PC, LOG);
        }

        //Tharaka 2015-06-13
        public List<TR_LOGSHEET_DET> GetLogSheetDetails(Int32 seqNum)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.GetLogSheetDetails(seqNum);
        }

        //Pramil 2015-06-16
        public Int32 SaveNewReceiptTBS(RecieptHeaderTBS _NewReceipt, List<RecieptItemTBS> _NewReceiptDetails, List<SR_PAY_LOG> sr_pay_logList, MasterAutoNumber _masterAutoNumber, ReptPickHeader _pickHeader, List<ReptPickSerials> _pickSerials, List<VehicalRegistration> _vehReg, List<VehicleInsuarance> _insList, List<HpSheduleDetails> _HPSheduleDetails, MasterAutoNumber _masterAutoNumberType, List<GiftVoucherPages> _pageList, string lohNo, Int16 pay_dri, out string docno)
        {
            Int32 _effects = 0;
            int _lineNo = 0;
            string _refNo = "";
            string _itmDetails = "";
            string _invNo = "";
            string _customer = "";
            string _distric = "";
            string _province = "";
            string _contact = "";
            string _cusAdd = "";
            string _autoNumberRecType = "";
            string _documentNo = "";
            Int32 user_seq_num = 0;

            _salesDAL = new SalesDAL();
            ToursDAL toursdal = new ToursDAL();

            ReptCommonDAL _reptCommonDAL = new ReptCommonDAL();

            if (_pickHeader != null)
            {
                user_seq_num = _reptCommonDAL.GET_SEQNUM_FOR_INVOICE("DO", _NewReceipt.Sir_com_cd, _pickHeader.Tuh_doc_no, 0);
            }

            //using (TransactionScope _tr = new TransactionScope())
            try
            {

                _inventoryDAL = new InventoryDAL(); _inventoryDAL.ConnectionOpen(); _inventoryDAL.BeginTransaction();
                _salesDAL = new SalesDAL(); _salesDAL.ConnectionOpen(); _salesDAL.BeginTransaction();
                _inventoryRepDAL = new ReptCommonDAL(); _inventoryRepDAL.ConnectionOpen(); _inventoryRepDAL.BeginTransaction();
                _generalDAL = new GeneralDAL(); _generalDAL.ConnectionOpen(); _generalDAL.BeginTransaction();
                _fmsInventoryDal = new FMS_InventoryDAL(); _fmsInventoryDal.ConnectionOpen(); _fmsInventoryDal.BeginTransaction();


                //_FMSDAL = new FMS_InventoryDAL(); 

                toursdal.ConnectionOpen();

                _effects = _salesDAL.SaveReceiptHeaderTBS(_NewReceipt);
                if (_effects > 0)
                {
                    foreach (SR_PAY_LOG sr_pay in sr_pay_logList)
                    {
                        _effects = toursdal.SaveSR_PAY_LOGTBS(sr_pay);
                    }
                }
                if (_effects > 0)
                    _effects = toursdal.UPDATE_LOGSHEET_HDRTBS(lohNo, pay_dri);


                _customer = _NewReceipt.Sir_debtor_cd + "-" + _NewReceipt.Sir_debtor_name;
                _distric = _NewReceipt.Sir_anal_1;
                _province = _NewReceipt.Sir_anal_2;
                _cusAdd = _NewReceipt.Sir_debtor_add_1 + "," + _NewReceipt.Sir_debtor_add_2;

                if (_NewReceiptDetails != null)
                {
                    foreach (RecieptItemTBS _ReceiptDetails in _NewReceiptDetails)
                    {
                        _effects = _salesDAL.SaveReceiptItemTBS(_ReceiptDetails);
                        if (_NewReceipt.Sir_receipt_type == "DEBT")
                        {
                            decimal _curBalance = 0;
                            if (_NewReceipt.Sir_is_oth_shop == true)        //kapila   27/6/2014
                            {
                                _curBalance = _salesDAL.GetOutInvAmtTBS(_NewReceipt.Sir_com_cd, _NewReceipt.Sir_oth_sr, _NewReceipt.Sir_debtor_cd, _ReceiptDetails.Sird_inv_no);
                                if (_curBalance >= _ReceiptDetails.Sird_settle_amt)
                                {
                                    _salesDAL.UpdateInvoiceSettleAmtTBS(_NewReceipt.Sir_com_cd, _NewReceipt.Sir_oth_sr, _NewReceipt.Sir_debtor_cd, _ReceiptDetails.Sird_inv_no, _ReceiptDetails.Sird_settle_amt);
                                }
                                else
                                {
                                    _effects = -1;
                                    docno = "Cannot proceed : Outstanding balance is " + _curBalance + " and settlement amount is " + _ReceiptDetails.Sird_settle_amt + " for the invoice " + _ReceiptDetails.Sird_inv_no;
                                    _inventoryDAL.TransactionRollback();
                                    _salesDAL.TransactionRollback();
                                    _inventoryRepDAL.TransactionRollback();
                                    _generalDAL.TransactionRollback();
                                    _fmsInventoryDal.TransactionRollback();
                                    return _effects;
                                }
                            }
                            else
                            {
                                _curBalance = _salesDAL.GetOutInvAmtTBS(_NewReceipt.Sir_com_cd, _NewReceipt.Sir_profit_center_cd, _NewReceipt.Sir_debtor_cd, _ReceiptDetails.Sird_inv_no);
                                if (_curBalance >= _ReceiptDetails.Sird_settle_amt)
                                {
                                    _salesDAL.UpdateInvoiceSettleAmtTBS(_NewReceipt.Sir_com_cd, _NewReceipt.Sir_profit_center_cd, _NewReceipt.Sir_debtor_cd, _ReceiptDetails.Sird_inv_no, _ReceiptDetails.Sird_settle_amt);
                                }
                                else
                                {
                                    _effects = -1;
                                    docno = "Cannot proceed : Outstanding balance is " + _curBalance + " and settlement amount is " + _ReceiptDetails.Sird_settle_amt + " for the invoice " + _ReceiptDetails.Sird_inv_no;
                                    _inventoryDAL.TransactionRollback();
                                    _salesDAL.TransactionRollback();
                                    _inventoryRepDAL.TransactionRollback();
                                    _generalDAL.TransactionRollback();
                                    _fmsInventoryDal.TransactionRollback();
                                    return _effects;
                                }

                            }

                        }
                    }

                    if (_NewReceipt.Sir_receipt_type == "DEBT")
                    {
                        // get invoice numbers
                        var _lst = (from n in _NewReceiptDetails
                                    group n by new { n.Sird_inv_no } into r
                                    select new { Sird_inv_no = r.Key.Sird_inv_no }).ToList();

                        decimal _Balance = 0;
                        decimal _commAmt = 0;
                        decimal _minCommAllow = 0;
                        decimal _wkNo = 0;

                        foreach (var s in _lst)
                        {
                            if (_NewReceipt.Sir_is_oth_shop == true)        //kapila   27/6/2014
                                _Balance = _salesDAL.GetOutInvAmtTBS(_NewReceipt.Sir_com_cd, _NewReceipt.Sir_oth_sr, _NewReceipt.Sir_debtor_cd, s.Sird_inv_no);
                            else
                                _Balance = _salesDAL.GetOutInvAmtTBS(_NewReceipt.Sir_com_cd, _NewReceipt.Sir_profit_center_cd, _NewReceipt.Sir_debtor_cd, s.Sird_inv_no);

                            HpSystemParameters _getSystemParameter = _salesDAL.GetSystemParameter("COM", _NewReceipt.Sir_com_cd, "CRCOMMINAW", _NewReceipt.Sir_receipt_date);

                            if (_getSystemParameter.Hsy_cd != null)
                            {
                                _minCommAllow = _getSystemParameter.Hsy_val;
                            }
                            else
                            {
                                _minCommAllow = 0;
                            }

                            if (_Balance <= _minCommAllow)
                            {
                                //get commission amount
                                _commAmt = _salesDAL.GetFinalCreditCommission(s.Sird_inv_no);

                                //save GNT_REM_SUM 
                                RemitanceSummaryDetail _remDet = new RemitanceSummaryDetail();
                                _remDet.Rem_com = _NewReceipt.Sir_com_cd;
                                if (_NewReceipt.Sir_is_oth_shop == true)    //kapila  27/6/2014
                                    _remDet.Rem_pc = _NewReceipt.Sir_oth_sr;
                                else
                                    _remDet.Rem_pc = _NewReceipt.Sir_profit_center_cd;

                                _remDet.Rem_dt = _NewReceipt.Sir_receipt_date;
                                _remDet.Rem_sec = "02";
                                _remDet.Rem_cd = "005";
                                _remDet.Rem_sh_desc = "Group sale comm";
                                _remDet.Rem_lg_desc = "GROUP SALE COMM";
                                _remDet.Rem_val = _commAmt;
                                _remDet.Rem_val_final = _commAmt;
                                int _weekNo = _generalDAL.GetWeek(Convert.ToDateTime(_NewReceipt.Sir_receipt_date).Date, out _wkNo, _NewReceipt.Sir_com_cd);
                                _remDet.Rem_week = _wkNo + "S";
                                _remDet.Rem_ref_no = _NewReceipt.Sir_seq_no.ToString();
                                _remDet.Rem_rmk = s.Sird_inv_no;
                                _remDet.Rem_cr_acc = null;
                                _remDet.Rem_db_acc = null;
                                _remDet.Rem_del_alw = false;
                                _remDet.Rem_cre_by = _NewReceipt.Sir_create_by;
                                _remDet.Rem_cre_dt = _NewReceipt.Sir_receipt_date;
                                _remDet.Rem_is_sos = true;
                                _remDet.Rem_is_dayend = true;
                                _remDet.Rem_is_sun = true;
                                _remDet.Rem_cat = 17;
                                _remDet.Rem_add = 0;
                                _remDet.Rem_ded = 0;
                                _remDet.Rem_net = _commAmt;
                                _remDet.Rem_epf = 0;
                                _remDet.Rem_esd = 0;
                                _remDet.Rem_wht = 0;
                                _remDet.Rem_add_fin = 0;
                                _remDet.Rem_ded_fin = 0;
                                _remDet.Rem_net_fin = _commAmt;
                                _remDet.Rem_rmk_fin = s.Sird_inv_no;
                                _remDet.Rem_bnk_cd = null;
                                _remDet.Rem_is_rem_sum = true;
                                _salesDAL.SaveRemSummaryForFinalCreditComm(_remDet);
                            }
                        }
                    }
                }



                Int32 _autoNo = _inventoryDAL.GetAutoNumber(_masterAutoNumber.Aut_moduleid, _masterAutoNumber.Aut_direction, _masterAutoNumber.Aut_start_char, _masterAutoNumber.Aut_cate_tp, _masterAutoNumber.Aut_cate_cd, _masterAutoNumber.Aut_modify_dt, _masterAutoNumber.Aut_year).Aut_number;
                _documentNo = _masterAutoNumber.Aut_cate_cd + _masterAutoNumber.Aut_start_char + string.Format("{0:0000}", _autoNo);

                if (_NewReceipt.Sir_anal_3 == "SYSTEM")
                {
                    Int32 _autoNoRecTp = _inventoryDAL.GetAutoNumber(_masterAutoNumberType.Aut_moduleid, _masterAutoNumberType.Aut_direction, _masterAutoNumberType.Aut_start_char, _masterAutoNumberType.Aut_cate_tp, _masterAutoNumberType.Aut_cate_cd, _masterAutoNumberType.Aut_modify_dt, _masterAutoNumberType.Aut_year).Aut_number;
                    _autoNumberRecType = _masterAutoNumberType.Aut_cate_cd + "-" + _masterAutoNumberType.Aut_start_char + "-" + string.Format("{0:000000}", _autoNoRecTp);
                }
                else
                {
                    _autoNumberRecType = _NewReceipt.Sir_manual_ref_no;
                }

                //_salesDAL.UpdateInvoiceReceipts("-1", _documentNo, -1, _NewReceipt.Sar_seq_no);
                _salesDAL.UpdateRecAutoNumberTBS(_documentNo, _NewReceipt.Sir_seq_no, _autoNumberRecType);

                //update receipt no to commistion table
                if (_NewReceipt.Sir_receipt_type == "DEBT")
                {
                    _salesDAL.UpdateFinalComRec(_NewReceipt.Sir_seq_no.ToString(), _documentNo, _NewReceipt.Sir_com_cd, _NewReceipt.Sir_profit_center_cd, "02", "005");
                }

                if (_pickHeader != null)
                {
                    if (_pickHeader.Tuh_usrseq_no != 0)
                    {
                        if (_NewReceipt.Sir_receipt_type == "VHREG" || _NewReceipt.Sir_receipt_type == "VHINS")
                        {

                        }
                        else
                        {
                            _pickHeader.Tuh_doc_no = _documentNo;
                        }

                        if (_pickHeader.Tuh_ischek_itmstus == true)
                        {

                            if (user_seq_num == -1)
                            {
                                //Generate pick header header
                                _inventoryRepDAL.SavePickedHeader(_pickHeader);
                            }

                        }
                        if (_pickSerials != null)
                        {
                            foreach (ReptPickSerials _list in _pickSerials)
                            {
                                _lineNo = _lineNo + 1;
                                if (_NewReceipt.Sir_receipt_type == "VHREG" || _NewReceipt.Sir_receipt_type == "VHINS")
                                {

                                    //_list.Tus_isapp = 0;
                                    //_list.Tus_iscovernote = 0;
                                }
                                else
                                {
                                    _list.Tus_base_doc_no = _pickHeader.Tuh_doc_no;
                                }

                                if (user_seq_num != -1)
                                {
                                    _list.Tus_usrseq_no = user_seq_num;
                                }

                                //_list.Tus_base_itm_line = _lineNo;
                                if (_pickHeader.Tuh_ischek_itmstus == true)
                                {
                                    _inventoryRepDAL.SavePickedItemSerials(_list);
                                    _inventoryDAL.Update_serial_status(_list.Tus_com, _list.Tus_loc, _list.Tus_itm_cd, _list.Tus_ser_1, -1, _list.Tus_seq_no);
                                    //Added by Prabhath on 12/12/2013 ---- Reservation on Inr_loc
                                    if (_NewReceipt.Sir_receipt_type == "ADVAN" && !string.IsNullOrEmpty(_list.Tus_ser_1)) _inventoryDAL.UpdateLocationRes(_list.Tus_com, _list.Tus_loc, _list.Tus_itm_cd, _list.Tus_itm_stus, _pickHeader.Tuh_usr_id, _list.Tus_qty);
                                }
                                _salesDAL.SaveReceiptItemDetails(_NewReceipt.Sir_seq_no, _lineNo, _documentNo, _list.Tus_itm_cd, _list.Tus_itm_desc, _list.Tus_itm_model, _list.Tus_ser_1, _list.Tus_ser_2, _NewReceipt.Sir_create_by, 0, null, null, 0, 0, 0, null, null);

                            }
                        }
                    }
                }

                //save vehicle registration details
                if (_vehReg != null)
                {
                    foreach (VehicalRegistration _vehList in _vehReg)
                    {
                        _refNo = _salesDAL.GetInsuRefBySerial(_vehList.P_svrt_inv_no, _vehList.P_svrt_chassis, _vehList.P_svrt_engine, _vehList.P_srvt_itm_cd);
                        _vehList.P_srvt_ref_no = _documentNo;
                        _vehList.P_srvt_insu_ref = _refNo;
                        _salesDAL.SaveVehRegistration(_vehList);
                        _salesDAL.UpdateInsTxnByRegNo(_vehList.P_srvt_ref_no, _vehList.P_srvt_itm_cd, _vehList.P_svrt_inv_no, _vehList.P_svrt_chassis, _vehList.P_svrt_engine);
                        if (_itmDetails == "")
                        {
                            _itmDetails = " Engine & chassis :" + _vehList.P_svrt_engine + "," + _vehList.P_svrt_chassis;
                        }
                        else
                        {
                            _itmDetails = "," + _itmDetails + _vehList.P_svrt_engine + "," + _vehList.P_svrt_chassis;
                        }


                        if (_invNo == "")
                        {
                            _invNo = " Inv # :" + _vehList.P_svrt_inv_no;
                        }

                        _contact = _vehList.P_svrt_contact;
                    }
                }

                //save vehicle insuarance details
                if (_insList != null)
                {

                    foreach (VehicleInsuarance _insu in _insList)
                    {
                        _refNo = _salesDAL.GetRegRefBySerial(_insu.Svit_inv_no, _insu.Svit_chassis, _insu.Svit_engine, _insu.Svit_itm_cd);
                        _insu.Svit_ref_no = _documentNo;
                        _insu.Svit_veg_ref = _refNo;
                        _salesDAL.SaveVehInsuarance(_insu);
                        if (_NewReceipt.Sir_receipt_type == "ADINS")
                        {
                            if (_insu.Svit_engine != "N/A")
                            {
                                _salesDAL.UpdateAddWarr(_insu.Svit_itm_cd, _insu.Svit_engine, 12, "SMART INSUARANCE", _insu.Svit_dt, _documentNo);
                            }
                        }

                        _salesDAL.UpdateRegTxnByInsNo(_insu.Svit_ref_no, _insu.Svit_itm_cd, _insu.Svit_inv_no, _insu.Svit_chassis, _insu.Svit_engine);
                        if (_itmDetails == "")
                        {
                            _itmDetails = " Engine & chassis :" + _insu.Svit_engine + "," + _insu.Svit_chassis;
                        }
                        else
                        {
                            _itmDetails = "," + _itmDetails + _insu.Svit_engine + "," + _insu.Svit_chassis;
                        }


                        if (_invNo == "")
                        {
                            _invNo = " Inv # :" + _insu.Svit_inv_no;
                        }

                        _contact = _insu.Svit_contact;
                    }


                }

                //shedule update part
                if (_HPSheduleDetails != null)
                {
                    foreach (HpSheduleDetails _sch in _HPSheduleDetails)
                    {
                        _salesDAL.UpdateHPShedule(_sch.Hts_acc_no, _sch.Hts_rnt_no, _sch.Hts_veh_insu, _sch.Hts_tot_val);

                    }

                }

                //update and save gift voucher
                if (_pageList != null)
                {
                    int I = 0;
                    foreach (GiftVoucherPages _pg in _pageList)
                    {
                        I = I + 1;
                        _pg.Gvp_oth_ref = _documentNo;
                        _fmsInventoryDal.UpdateGiftVoucherByReceipt(_pg);
                        _salesDAL.SaveReceiptItemDetails(_NewReceipt.Sir_seq_no, I, _documentNo, _pg.Gvp_gv_cd, null, null, _pg.Gvp_book.ToString(), _pg.Gvp_page.ToString(), _pg.Gvp_mod_by, 0, null, null, 0, 0, 0, null, null);
                    }
                }

                //if (_NewReceipt.Sar_receipt_type == "VHINS")
                //{


                //    //send SMS 
                //    List<MsgInformation> _msg = _generalDAL.GetMsgInformation(_NewReceipt.Sar_com_cd, _NewReceipt.Sar_profit_center_cd, _NewReceipt.Sar_receipt_type);

                //    if (_msg != null)
                //        if (_msg.Count > 0)
                //        {
                //            foreach (MsgInformation _info in _msg)
                //            {
                //                string _mg = "Insuarance receipt generated. Rec. # " + _documentNo + _invNo + _itmDetails + " - SCM2 -";
                //                OutSMS _out = new OutSMS();
                //                _out.Msg = _mg;
                //                _out.Msgstatus = 0;
                //                _out.Msgtype = _info.Mmi_msg_tp;
                //                _out.Receivedtime = DateTime.Now;
                //                _out.Receiver = _info.Mmi_receiver;
                //                _out.Receiverphno = _info.Mmi_mobi_no;
                //                _out.Refdocno = _documentNo;
                //                _out.Sender = _NewReceipt.Sar_create_by;
                //                _out.Createtime = DateTime.Now;
                //                _generalDAL.SaveSMSOut(_out);

                //            }
                //        }




                //    //send mail
                //    List<MsgInformation> _mail = _generalDAL.GetMsgInformation(_NewReceipt.Sar_com_cd, null, _NewReceipt.Sar_receipt_type);
                //    if (_mail != null)
                //        if (_mail.Count > 0)
                //        {
                //            foreach (MsgInformation _info in _mail)
                //            {
                //                SmtpClient smtpClient = new SmtpClient();
                //                MailMessage message = new MailMessage();

                //                MailAddress fromAddress = new MailAddress(_generalDAL.GetMailAddress(), _generalDAL.GetMailDispalyName());

                //                smtpClient.Host = _generalDAL.GetMailHost();
                //                smtpClient.Port = 25;
                //                message.From = fromAddress;

                //                string _email = "";

                //                _email = "Dear Sir/Madam, \n\n" + _email;
                //                _email += "Insuarance receipt generated for the profit center :" + _NewReceipt.Sar_profit_center_cd + " \n";
                //                _email += "\n  Receipt # : " + _documentNo + "\n";
                //                _email += "\n " + _invNo + "\n";
                //                _email += "\n  Customer : " + _customer + "\n";
                //                _email += "\n  Address : " + _cusAdd + "\n";
                //                _email += "\n  contact # : " + _contact + "\n";
                //                _email += "\n  District : " + _distric + "\n";
                //                _email += "\n  Province : " + _province + "\n";

                //                foreach (VehicleInsuarance _insu in _insList)
                //                {
                //                    _itmDetails = "";
                //                    _itmDetails = "Item & Model : " + _insu.Svit_itm_cd + "-" + _insu.Svit_model + " -  Engine & chassis :" + _insu.Svit_engine + " / " + _insu.Svit_chassis;
                //                    _email += "\n " + _itmDetails + " \n";
                //                }



                //                _email += _generalDAL.GetMailFooterMsg();

                //                message.To.Add(_info.Mmi_email);
                //                message.Subject = "Vehicle Insuarance Receipt";
                //                //message.CC.Add(new MailAddress(_info.Mmi_superior_mail));
                //                //message.Bcc.Add(new MailAddress(""));
                //                message.IsBodyHtml = false;
                //                message.Body = _email;
                //                message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;

                //                // Send SMTP mail
                //                smtpClient.Send(message);
                //            }
                //        }


                //}
                //else if (_NewReceipt.Sar_receipt_type == "VHREG")
                //{
                //    //send SMS 
                //    List<MsgInformation> _msg = _generalDAL.GetMsgInformation(_NewReceipt.Sar_com_cd, _NewReceipt.Sar_profit_center_cd, _NewReceipt.Sar_receipt_type);

                //    if (_msg != null)
                //        if (_msg.Count > 0)
                //        {
                //            foreach (MsgInformation _info in _msg)
                //            {
                //                string _mg = "Vehicle registration receipt generated. Rec. # " + _documentNo + _invNo + _itmDetails + " - SCM2 -";
                //                OutSMS _out = new OutSMS();
                //                _out.Msg = _mg;
                //                _out.Msgstatus = 0;
                //                _out.Msgtype = _info.Mmi_msg_tp;
                //                _out.Receivedtime = DateTime.Now;
                //                _out.Receiver = _info.Mmi_receiver;
                //                _out.Receiverphno = _info.Mmi_mobi_no;
                //                _out.Refdocno = _documentNo;
                //                _out.Sender = _NewReceipt.Sar_create_by;
                //                _out.Createtime = DateTime.Now;
                //                _generalDAL.SaveSMSOut(_out);

                //            }
                //        }


                //    //send mail
                //    List<MsgInformation> _mail = _generalDAL.GetMsgInformation(_NewReceipt.Sar_com_cd, null, _NewReceipt.Sar_receipt_type);
                //    if (_mail != null)
                //        if (_mail.Count > 0)
                //        {
                //            foreach (MsgInformation _info in _mail)
                //            {
                //                SmtpClient smtpClient = new SmtpClient();
                //                MailMessage message = new MailMessage();

                //                MailAddress fromAddress = new MailAddress(_generalDAL.GetMailAddress(), _generalDAL.GetMailDispalyName());

                //                smtpClient.Host = _generalDAL.GetMailHost();
                //                smtpClient.Port = 25;
                //                message.From = fromAddress;

                //                string _email = "";

                //                _email = "Dear Sir/Madam, \n\n" + _email;
                //                _email += "Vehicle registration receipt generated for the profit center :" + _NewReceipt.Sar_profit_center_cd + " \n";
                //                _email += "\n  Receipt # : " + _documentNo + "\n";
                //                _email += "\n " + _invNo + "\n";
                //                _email += "\n  Customer : " + _customer + "\n";
                //                _email += "\n  Address : " + _cusAdd + "\n";
                //                _email += "\n  contact # : " + _contact + "\n";
                //                _email += "\n  District : " + _distric + "\n";
                //                _email += "\n  Province : " + _province + "\n";

                //                foreach (VehicalRegistration _reg in _vehReg)
                //                {
                //                    _itmDetails = "";
                //                    _itmDetails = "Item & Model : " + _reg.P_srvt_itm_cd + "-" + _reg.P_svrt_model + " -  Engine & chassis :" + _reg.P_svrt_engine + " / " + _reg.P_svrt_chassis;
                //                    _email += "\n " + _itmDetails + " \n";
                //                }



                //                _email += _generalDAL.GetMailFooterMsg();

                //                message.To.Add(_info.Mmi_email);
                //                message.Subject = "Vehicle Registration Receipt";
                //                //message.CC.Add(new MailAddress(_info.Mmi_superior_mail));
                //                //message.Bcc.Add(new MailAddress(""));
                //                message.IsBodyHtml = false;
                //                message.Body = _email;
                //                message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;

                //                // Send SMTP mail
                //                smtpClient.Send(message);
                //            }
                //        }
                //}


                _inventoryDAL.UpdateAutoNumber(_masterAutoNumber);

                if (_NewReceipt.Sir_anal_3 == "SYSTEM")
                {
                    _inventoryDAL.UpdateAutoNumber(_masterAutoNumberType);
                }

                if (_NewReceipt.Sir_anal_3 == "MANUAL" && _NewReceipt.Sir_anal_8 == 1)
                {
                    _fmsInventoryDal.Update_Manual_DocNo(_NewReceipt.Sir_profit_center_cd, "MDOC_AVREC", Convert.ToInt32(_NewReceipt.Sir_manual_ref_no), _documentNo);
                }

                docno = _documentNo;

                //_inventoryDAL.ConnectionClose();
                //_salesDAL.ConnectionClose();
                //_inventoryRepDAL.ConnectionClose();
                //_generalDAL.ConnectionClose();
                //_fmsInventoryDal.ConnectionClose();
                //_effects = 1;
                //_tr.Complete();

                // _generalDAL = new GeneralDAL(); _generalDAL.ConnectionOpen();
                if (_NewReceipt.Sir_receipt_type == "VHINS")
                {
                    //send SMS 
                    List<MsgInformation> _msg = _generalDAL.GetMsgInformation(_NewReceipt.Sir_com_cd, _NewReceipt.Sir_profit_center_cd, _NewReceipt.Sir_receipt_type);

                    if (_msg != null)
                        if (_msg.Count > 0)
                        {
                            foreach (MsgInformation _info in _msg)
                            {
                                if (_info.Mmi_mobi_no.Length > 9)
                                {
                                    string _mg = "Insuarance receipt generated. Rec. # " + _documentNo + _invNo + _itmDetails + " - SCM2 -";
                                    OutSMS _out = new OutSMS();
                                    _out.Msg = _mg;
                                    _out.Msgstatus = 0;
                                    _out.Msgtype = _info.Mmi_msg_tp;
                                    _out.Receivedtime = DateTime.Now;
                                    _out.Receiver = _info.Mmi_receiver;
                                    //_out.Receiverphno = _info.Mmi_mobi_no;//Edit by Chamal 31-03-2015
                                    _out.Senderphno = _info.Mmi_mobi_no;
                                    _out.Refdocno = _documentNo;
                                    _out.Sender = _NewReceipt.Sir_create_by;
                                    _out.Createtime = DateTime.Now;
                                    _generalDAL.SaveSMSOut(_out);
                                }
                            }
                        }

                    //send mail
                    List<MsgInformation> _mail = _generalDAL.GetMsgInformation(_NewReceipt.Sir_com_cd, null, _NewReceipt.Sir_receipt_type);
                    if (_mail != null)
                        if (_mail.Count > 0)
                        {
                            foreach (MsgInformation _info in _mail)
                            {
                                SmtpClient smtpClient = new SmtpClient();
                                MailMessage message = new MailMessage();

                                MailAddress fromAddress = new MailAddress(_generalDAL.GetMailAddress(), _generalDAL.GetMailDispalyName());

                                smtpClient.Host = _generalDAL.GetMailHost();
                                smtpClient.Port = 25;
                                message.From = fromAddress;

                                string _email = "";

                                _email = "Dear Sir/Madam, \n\n" + _email;
                                _email += "Insuarance receipt generated for the profit center :" + _NewReceipt.Sir_profit_center_cd + " \n";
                                _email += "\n  Receipt # : " + _documentNo + "\n";
                                _email += "\n " + _invNo + "\n";
                                _email += "\n  Customer : " + _customer + "\n";
                                _email += "\n  Address : " + _cusAdd + "\n";
                                _email += "\n  contact # : " + _contact + "\n";
                                _email += "\n  District : " + _distric + "\n";
                                _email += "\n  Province : " + _province + "\n";

                                foreach (VehicleInsuarance _insu in _insList)
                                {
                                    _itmDetails = "";
                                    _itmDetails = "Item & Model : " + _insu.Svit_itm_cd + "-" + _insu.Svit_model + " -  Engine & chassis :" + _insu.Svit_engine + " / " + _insu.Svit_chassis + " - Insuarance company code :" + _insu.Svit_ins_com;
                                    _email += "\n " + _itmDetails + " \n";
                                }



                                _email += _generalDAL.GetMailFooterMsg();

                                message.To.Add(_info.Mmi_email);
                                message.Subject = "Vehicle Insuarance Receipt";
                                //message.CC.Add(new MailAddress(_info.Mmi_superior_mail));
                                //message.Bcc.Add(new MailAddress(""));
                                message.IsBodyHtml = false;
                                message.Body = _email;
                                message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;

                                // Send SMTP mail
                                smtpClient.Send(message);
                            }
                        }


                }
                else if (_NewReceipt.Sir_receipt_type == "VHREG")
                {
                    //send SMS 
                    List<MsgInformation> _msg = _generalDAL.GetMsgInformation(_NewReceipt.Sir_com_cd, _NewReceipt.Sir_profit_center_cd, _NewReceipt.Sir_receipt_type);

                    if (_msg != null)
                        if (_msg.Count > 0)
                        {
                            foreach (MsgInformation _info in _msg)
                            {
                                if (_info.Mmi_mobi_no.Length > 9)
                                {
                                    string _mg = "Vehicle registration receipt generated. Rec. # " + _documentNo + _invNo + _itmDetails + " - SCM2 -";
                                    OutSMS _out = new OutSMS();
                                    _out.Msg = _mg;
                                    _out.Msgstatus = 0;
                                    _out.Msgtype = _info.Mmi_msg_tp;
                                    _out.Receivedtime = DateTime.Now;
                                    _out.Receiver = _info.Mmi_receiver;
                                    //_out.Receiverphno = _info.Mmi_mobi_no; //Edit by Chamal 31-Mar-2015
                                    _out.Senderphno = _info.Mmi_mobi_no;
                                    _out.Refdocno = _documentNo;
                                    _out.Sender = _NewReceipt.Sir_create_by;
                                    _out.Createtime = DateTime.Now;
                                    _generalDAL.SaveSMSOut(_out);
                                }
                            }
                        }


                    //send mail
                    List<MsgInformation> _mail = _generalDAL.GetMsgInformation(_NewReceipt.Sir_com_cd, null, _NewReceipt.Sir_receipt_type);
                    if (_NewReceipt.Sir_profit_center_cd == "BOC") _mail = null;        //kapila 3/10/2014
                    if (_mail != null)
                        if (_mail.Count > 0)
                        {
                            foreach (MsgInformation _info in _mail)
                            {
                                SmtpClient smtpClient = new SmtpClient();
                                MailMessage message = new MailMessage();

                                MailAddress fromAddress = new MailAddress(_generalDAL.GetMailAddress(), _generalDAL.GetMailDispalyName());

                                smtpClient.Host = _generalDAL.GetMailHost();
                                smtpClient.Port = 25;
                                message.From = fromAddress;

                                string _email = "";

                                _email = "Dear Sir/Madam, \n\n" + _email;
                                _email += "Vehicle registration receipt generated for the profit center :" + _NewReceipt.Sir_profit_center_cd + " \n";
                                _email += "\n  Receipt # : " + _documentNo + "\n";
                                _email += "\n " + _invNo + "\n";
                                _email += "\n  Customer : " + _customer + "\n";
                                _email += "\n  Address : " + _cusAdd + "\n";
                                _email += "\n  contact # : " + _contact + "\n";
                                _email += "\n  District : " + _distric + "\n";
                                _email += "\n  Province : " + _province + "\n";

                                foreach (VehicalRegistration _reg in _vehReg)
                                {
                                    _itmDetails = "";
                                    _itmDetails = "Item & Model : " + _reg.P_srvt_itm_cd + "-" + _reg.P_svrt_model + " -  Engine & chassis :" + _reg.P_svrt_engine + " / " + _reg.P_svrt_chassis;
                                    _email += "\n " + _itmDetails + " \n";
                                }



                                _email += _generalDAL.GetMailFooterMsg();

                                message.To.Add(_info.Mmi_email);
                                message.Subject = "Vehicle Registration Receipt";
                                //message.CC.Add(new MailAddress(_info.Mmi_superior_mail));
                                //message.Bcc.Add(new MailAddress(""));
                                message.IsBodyHtml = false;
                                message.Body = _email;
                                message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;

                                // Send SMTP mail
                                smtpClient.Send(message);
                            }
                        }
                }
                else if (_NewReceipt.Sir_receipt_type == "ABTCO")
                {
                    List<MsgInformation> _msg = _generalDAL.GetMsgInformation(_NewReceipt.Sir_com_cd, _NewReceipt.Sir_profit_center_cd, _NewReceipt.Sir_receipt_type);

                    if (_msg != null)
                        if (_msg.Count > 0)
                        {
                            foreach (MsgInformation _info in _msg)
                            {
                                string _mg = "Collection received for air ticket at " + _NewReceipt.Sir_profit_center_cd + " S/R.Rec. # " + _documentNo + " - SCM2 -";
                                if (_info.Mmi_mobi_no.Length > 9)
                                {
                                    OutSMS _out = new OutSMS();
                                    _out.Msg = _mg;
                                    _out.Msgstatus = 0;
                                    _out.Msgtype = _info.Mmi_msg_tp;
                                    _out.Receivedtime = DateTime.Now;
                                    _out.Receiver = _info.Mmi_receiver;
                                    //_out.Receiverphno = _info.Mmi_mobi_no;
                                    _out.Senderphno = _info.Mmi_mobi_no;
                                    _out.Refdocno = _documentNo;
                                    _out.Sender = _NewReceipt.Sir_create_by;
                                    _out.Createtime = DateTime.Now;
                                    _generalDAL.SaveSMSOut(_out);
                                }
                            }
                        }

                    //send mail
                    List<MsgInformation> _mail = _generalDAL.GetMsgInformation(_NewReceipt.Sir_com_cd, null, _NewReceipt.Sir_receipt_type);
                    if (_mail != null)
                        if (_mail.Count > 0)
                        {
                            foreach (MsgInformation _info in _mail)
                            {
                                SmtpClient smtpClient = new SmtpClient();
                                MailMessage message = new MailMessage();

                                MailAddress fromAddress = new MailAddress(_generalDAL.GetMailAddress(), _generalDAL.GetMailDispalyName());

                                smtpClient.Host = _generalDAL.GetMailHost();
                                smtpClient.Port = 25;
                                message.From = fromAddress;

                                string _email = "";

                                _email = "Dear Sir/Madam, \n\n" + _email;
                                _email += "Collection received for the profit center :" + _NewReceipt.Sir_profit_center_cd + " \n";
                                _email += "\n  Receipt # : " + _documentNo + "\n";
                                _email += "\n  Customer : " + _customer + "\n";
                                _email += "\n  Address : " + _cusAdd + "\n";
                                _email += "\n  contact # : " + _contact + "\n";
                                _email += "\n  Amount # : " + _NewReceipt.Sir_tot_settle_amt + "\n";
                                _email += "\n  Note : " + _NewReceipt.Sir_remarks + "\n";


                                _email += " \n Thank You, \n ** This is an auto generated mail from SCM2 infor portal. Please don't Reply ** \n ** IT Department 2012 **";

                                message.To.Add(_info.Mmi_email);
                                message.Subject = "Air Ticket collection";
                                //message.CC.Add(new MailAddress(_info.Mmi_superior_mail));
                                //message.Bcc.Add(new MailAddress(""));
                                message.IsBodyHtml = false;
                                message.Body = _email;
                                message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;

                                // Send SMTP mail
                                smtpClient.Send(message);
                            }
                        }
                }


                //Send notification SMS to customer - 14-10-2014 - Darshana
                //List<MasterBusinessEntity> _cusdet = new List<MasterBusinessEntity>();
                string _mobilNo = "";
                string _cusName = "";
                string _Newmg = "";
                string _recTp = "";
                decimal _recAmt = 0;
                //_cusdet = _salesDAL.GetActiveCustomerDetailList(_NewReceipt.Sar_com_cd, _NewReceipt.Sar_debtor_cd, null, null, "C");
                //foreach (MasterBusinessEntity _cus in _cusdet)
                //{
                _mobilNo = _NewReceipt.Sir_mob_no;
                _cusName = _NewReceipt.Sir_debtor_name;
                _recAmt = _NewReceipt.Sir_tot_settle_amt;


                if (!string.IsNullOrEmpty(_mobilNo))
                {

                    if (_mobilNo.Length >= 9)
                    {
                        if (_NewReceipt.Sir_receipt_type == "ADVAN")
                        {
                            _recTp = "Advance payment";
                        }
                        else if (_NewReceipt.Sir_receipt_type == "GVISU")
                        {
                            _recTp = "Gift voucher purchase";
                        }
                        else if (_NewReceipt.Sir_receipt_type == "DEBT")
                        {
                            _recTp = "Credit sale settlement";
                        }
                        else if (_NewReceipt.Sir_receipt_type == "VHINS")
                        {
                            _recTp = "Vechicle insuarance";
                        }
                        else if (_NewReceipt.Sir_receipt_type == "VHREG")
                        {
                            _recTp = "Vechicle registration";
                        }
                        else if (_NewReceipt.Sir_receipt_type == "ADINS")
                        {
                            _recTp = "Insuarance";
                        }
                        else if (_NewReceipt.Sir_receipt_type == "ABTCO")
                        {
                            _recTp = "Air tickets";
                        }
                        else
                        {
                            _recTp = "";
                        }

                        if (!string.IsNullOrEmpty(_recTp))
                        {
                            if (_recAmt > 0)
                            {
                                string _pcName = "";
                                DataTable dt = _generalDAL.CheckProfitCenter(_NewReceipt.Sir_com_cd, _NewReceipt.Sir_profit_center_cd);
                                if (dt.Rows.Count > 0)
                                {
                                    _pcName = Convert.ToString(dt.Rows[0]["mpc_desc"]);
                                }

                                _Newmg = "Thank u for the payment of LKR " + _recAmt + " received to the " + _pcName + " for " + _recTp + ",R/N " + _documentNo;// "Thank u for the purchased items on HP A/C: " + _AccountNo + " @ " + _pcName + " S/R. HP value Rs.: " + _HPAccount.Hpa_hp_val + " and " + _smsInsu + " Rs.: " + _insuAmt + "-" + _generalDAL.GetHPCustContactPhoneNo();
                                OutSMS _out = new OutSMS();
                                _out.Msg = _Newmg;
                                _out.Msgstatus = 0;
                                _out.Msgtype = "S";
                                _out.Receivedtime = DateTime.Now;
                                _out.Receiver = "CUSTOMER";
                                //_out.Receiverphno = _info.Mmi_mobi_no;

                                if (_mobilNo.Length == 10)
                                {
                                    _out.Receiverphno = "+94" + _mobilNo.Substring(1, 9);
                                    _out.Senderphno = "+94" + _mobilNo.Substring(1, 9);
                                }
                                if (_mobilNo.Length == 9)
                                {
                                    _out.Receiverphno = "+94" + _mobilNo;
                                    _out.Senderphno = "+94" + _mobilNo;
                                }

                                _out.Refdocno = _documentNo;
                                _out.Sender = _NewReceipt.Sir_create_by;
                                _out.Createtime = DateTime.Now;
                                _generalDAL.SaveSMSOut(_out);
                            }
                        }
                    }
                }
                //}



                //_inventoryDAL.ConnectionClose();
                //_salesDAL.ConnectionClose();
                //_inventoryRepDAL.ConnectionClose();
                //_generalDAL.ConnectionClose();
                //_fmsInventoryDal.ConnectionClose();

                _inventoryDAL.TransactionCommit();
                _salesDAL.TransactionCommit();
                _inventoryRepDAL.TransactionCommit();
                _generalDAL.TransactionCommit();
                _fmsInventoryDal.TransactionCommit();

                toursdal.TransactionCommit();
                toursdal.ConnectionClose();

                _effects = 1;
                // _generalDAL.ConnectionClose();
            }
            catch (Exception err)
            {
                _effects = -1;
                docno = "ERROR : " + err.Message.ToString();
                //documentNoGRN = documentNo;
                _inventoryDAL.TransactionRollback();
                _salesDAL.TransactionRollback();
                _inventoryRepDAL.TransactionRollback();
                _generalDAL.TransactionRollback();
                _fmsInventoryDal.TransactionRollback();

            }
            return _effects;

        }


        //Sahan 15 Jun 2015
        public DataTable SP_TOURS_GET_ALL_OVERLAP_DATES(string P_MFD_VEH_NO, string P_MFD_DRI, DateTime p_mfd_frm_dt, DateTime p_mfd_to_dt)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.SP_TOURS_GET_ALL_OVERLAP_DATES(P_MFD_VEH_NO, P_MFD_DRI, p_mfd_frm_dt, p_mfd_to_dt);
        }

        //Pemil 2015-06-16
        public Int32 Cancel_paymentTBS(int seq, string log_no, string receipt_no)
        {
            _ToursDAL = new ToursDAL();
            _ToursDAL.ConnectionOpen();
            _ToursDAL.BeginTransaction();
            int retu = _ToursDAL.Cancel_paymentTBS(seq, log_no, receipt_no);
            _ToursDAL.TransactionCommit();
            _ToursDAL.ConnectionClose();
            return retu;
        }

        //Pemil 2015-06-17
        public DataTable Get_tour_searchDriverTBS(string com_cd, string ep, string cat_subcd2, string first_name2, string last_name, string nic, string tou_lic)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.Get_tour_searchDriverTBS(com_cd, ep, cat_subcd2, first_name2, last_name, nic, tou_lic);
        }

        //Pemil 2015-06-18
        public DataTable Get_sr_pay_log(string rec_no)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.Get_sr_pay_log(rec_no);
        }

        //Tharaka 2015-06-24
        public List<TR_LOGSHEET_DET> GetLogDetailsCustInvoice(String custCode, String Com, DateTime From, DateTime TO, Int32 Status)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.GetLogDetailsCustInvoice(custCode, Com, From, TO, Status);
        }

        //Tharaka 2015-06-26
        public Int32 UPDATE_LOG_HDR_INVOICE(int seq, int STATUS, string USER, out String err)
        {
            int count = 0;
            err = string.Empty;
            try
            {
                _ToursDAL = new ToursDAL();
                _ToursDAL.ConnectionOpen();
                _ToursDAL.BeginTransaction();
                count = _ToursDAL.UPDATE_LOG_HDR_INVOICE(seq, STATUS, USER);

                _ToursDAL.TransactionCommit();
                _ToursDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                _ToursDAL.TransactionRollback();
                err = ex.Message;
            }

            return count;
        }

        //Tharaka 2015-06-30
        public TR_LOGSHEET_HDR GET_LOG_HDR_BY_ENQRY(String enquiryID)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.GET_LOG_HDR_BY_ENQRY(enquiryID);
        }


        //Tharaka 2016-01-14
        public int SaveEnquiryRequestList(List<GEN_CUST_ENQ> oItems, MasterAutoNumber _ReqInsAuto, MasterAutoNumber _mainNumber, out string err)
        {
            int resutl = 0;
            Boolean _isNew = false;
            string errMSG = string.Empty;
            err = string.Empty;

            try
            {
                _ToursDAL = new ToursDAL();
                _inventoryDAL = new InventoryDAL();
                _generalDAL = new GeneralDAL();

                _ToursDAL.ConnectionOpen();
                _inventoryDAL.ConnectionOpen();

                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                _ToursDAL.BeginTransaction();
                _inventoryDAL.BeginTransaction();

                _salesDAL = new SalesDAL();

                string InqNumbers = string.Empty;
                string mainReqNumebr = string.Empty;
                string[] oMainReqNumer = oItems.Where(z => z.Gce_mainreqid != "" && z.Gce_mainreqid != null).Select(x => x.Gce_mainreqid).Distinct().ToArray();
                if (oMainReqNumer.Length == 0)
                {
                    MasterAutoNumber _reversInv = _inventoryDAL.GetAutoNumber(_mainNumber.Aut_moduleid, _mainNumber.Aut_direction, _mainNumber.Aut_start_char, _mainNumber.Aut_cate_tp, _mainNumber.Aut_cate_cd, _mainNumber.Aut_modify_dt, _mainNumber.Aut_year);
                    _reversInv.Aut_direction = null;
                    _reversInv.Aut_modify_dt = null;
                    err = _reversInv.Aut_cate_cd + "-" + _reversInv.Aut_start_char + "-" + Convert.ToString(DateTime.Now.Date.Year).Remove(0, 2) + "-" + _reversInv.Aut_number.ToString("00000", CultureInfo.InvariantCulture);
                    _inventoryDAL.UpdateAutoNumber(_reversInv);
                    mainReqNumebr = err;
                    _isNew = true;

                    oItems.ForEach(x => x.Gce_mainreqid = mainReqNumebr);
                }

                foreach (GEN_CUST_ENQ oItem in oItems)
                {
                    if (String.IsNullOrEmpty(oItem.GCE_ENQ_ID) && oItem.GCE_SEQ == 0)
                    {
                        MasterAutoNumber _reversInv = _inventoryDAL.GetAutoNumber(_ReqInsAuto.Aut_moduleid, _ReqInsAuto.Aut_direction, _ReqInsAuto.Aut_start_char, _ReqInsAuto.Aut_cate_tp, _ReqInsAuto.Aut_cate_cd, _ReqInsAuto.Aut_modify_dt, _ReqInsAuto.Aut_year);
                        _reversInv.Aut_direction = null;
                        _reversInv.Aut_modify_dt = null;
                        err = _reversInv.Aut_cate_cd + "-" + _reversInv.Aut_start_char + "-" + Convert.ToString(DateTime.Now.Date.Year).Remove(0, 2) + "-" + _reversInv.Aut_number.ToString("00000", CultureInfo.InvariantCulture);
                        _inventoryDAL.UpdateAutoNumber(_reversInv);
                        oItem.GCE_ENQ_ID = err;
                        _isNew = true;
                    }

                    MasterBusinessEntity oCusto = _ToursDAL.GetCustomerProfile(oItem.Gce_bill_cuscd, null, null, null, null);
                    if (oCusto != null && oCusto.Mbe_cd != "")
                    {
                        oItem.Gce_bill_cusname = oCusto.Mbe_name;
                    }

                    resutl = _ToursDAL.Save_GEN_CUST_ENQ(oItem);

                    GEN_ENQLOG oLog = new GEN_ENQLOG();
                    oLog.GEL_ENQ_ID = oItem.GCE_ENQ_ID;
                    oLog.GEL_USR = oItem.GCE_CRE_BY;
                    oLog.GEL_STAGE = oItem.GCE_STUS;
                    oLog.GEL_LOGWHEN = DateTime.Now;
                    resutl = _ToursDAL.SAVE_GEN_ENQLOG(oLog);


                    InqNumbers = InqNumbers + ", " + oItem.GCE_ENQ_ID;
                    //if (_isNew == true)
                    //{
                    //    err = "Successfully Created , Enquiry ID  " + oItem.GCE_ENQ_ID;
                    //}
                    //else
                    //{
                    //    err = "Successfully Updated , Enquiry ID  " + oItem.GCE_ENQ_ID;
                    //}
                }

                OutSMS _out = new OutSMS();
                if (oItems[0].GCE_MOB != "N/A" && !string.IsNullOrEmpty(oItems[0].GCE_MOB))
                {
                    String msg = "Dear Customer, \nYou have created a enquiry.\nEnquiry Id(s) - " + InqNumbers;
                    String mobi = "+94" + oItems[0].GCE_MOB.Substring(1, 9);
                    _out.Msgstatus = 0;
                    _out.Msgtype = "S";
                    _out.Receivedtime = DateTime.Now;
                    _out.Receiver = mobi;
                    _out.Msg = msg;
                    _out.Receiverphno = mobi;
                    _out.Senderphno = mobi;
                    _out.Refdocno = "0";
                    _out.Sender = "Abans Tours";
                    _out.Createtime = DateTime.Now;
                    SendSMS_InternalMethod(_out, out errMSG);
                }

                err = InqNumbers;

                _ToursDAL.TransactionCommit();
                _ToursDAL.ConnectionClose();

                _inventoryDAL.TransactionCommit();
                _inventoryDAL.ConnectionClose();

                _generalDAL.TransactionCommit();
                _generalDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                err = ex.Message;
                _ToursDAL.TransactionRollback();
                _inventoryDAL.TransactionRollback();
                _generalDAL.TransactionRollback();
            }
            return resutl;
        }

        //Rukshan 2016-1-28
        public List<QUO_COST_HDR> GET_COST_PRFITABILITY(string _com, string _procenter, string _req, string _costRef, string _customer, string _category,
            DateTime _fromdate, DateTime _todate)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.GET_COST_PRFITABILITY(_com, _procenter, _req, _costRef, _customer, _category, _fromdate, _todate);
        }

        //Rukshan 2016-1-28
        public List<QUO_COST_HDR> GET_COST_PRFITABILITY_DETAILS(string _com, string _procenter, string _req, string _costRef, string _customer, string _category,
            DateTime _fromdate, DateTime _todate)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.GET_COST_PRFITABILITY_DETAILS(_com, _procenter, _req, _costRef, _customer, _category, _fromdate, _todate);
        }

        //Rukshan 2016-02-29
        public DataTable Get_COST_HDR_NO(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.Get_COST_HDR_NO(_initialSearchParams, _searchCatergory, _searchText);
        }
        //Rukshan 2016-02-29
        public DataTable Get_SERVICE_CODE(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.Get_SERVICE_CODE(_initialSearchParams, _searchCatergory, _searchText);
        }

        //Rukshan 2016-01-30
        public List<SR_AIR_CHARGE> GetALLChargeDetailsByCode(string com, string Cate, string Code, DateTime date)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.GetALLChargeDetailsByCode(com, Cate, Code, date);
        }

        //Rukshan 2016-01-30
        public List<SR_TRANS_CHA> GetAllTransferChargeDetailsByCode(string com, String Cate, string Code, DateTime date)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.GetAllTransferChargeDetailsByCode(com, Cate, Code, date);
        }

        //Tharaka 2016-01-30
        public List<SR_SER_MISS> GetALLMiscellaneousChargeDetailsByCode(string com, String Cate, string Code, DateTime date)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.GetALLMiscellaneousChargeDetailsByCode(com, Cate, Code, date);
        }
        /// <summary>
        /// get title list
        /// </summary>
        /// <returns> List<MST_TITLE></returns>
        public List<MST_TITLE> GetTitleList()
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.GetTitleList();
        }

        /* Created on 09/Feb/2016 4:40:04 PM Lakshan*/
        public List<MST_ENQSUBTP> GET_ENQRY_SUB_TP(MST_ENQSUBTP obj)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.GET_ENQRY_SUB_TP(obj);
        }
        /// <summary>
        /// get employee desials to add employee
        /// </summary>
        /// <param name="empCode">epf no</param>
        /// <returns>MST_EMPLOYEE_NEW_TBS</returns>
        public MST_EMPLOYEE_NEW_TBS getMstEmployeeDetails(string empCode)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.getMstEmployeeDetails(empCode);
        }



        public Int32 SaveEmployeeData(MST_EMPLOYEE_NEW_TBS mst_employee_tbs, List<MST_PCEMP> mst_pcemp, List<mst_fleet_driver> vehicleList, string userId, string userDefPro, DateTime serverDatetime, string driverCom, out string err)
        {
            int count = 0;
            err = string.Empty;
            try
            {
                _ToursDAL = new ToursDAL();
                _ToursDAL.ConnectionOpen();
                _ToursDAL.BeginTransaction();
                count = _ToursDAL.SaveEmployeeData(mst_employee_tbs);

                if (count == 1)
                {
                    foreach (MST_PCEMP pcemp in mst_pcemp)
                    {
                        count = _ToursDAL.SavePcemp(pcemp);
                    }
                    if (vehicleList != null)
                    {
                        foreach (mst_fleet_driver vehicle in vehicleList)
                        {
                            string seq_no = vehicle.MFD_SEQ.ToString();
                            string drivercom = driverCom;
                            string p_mfd_veh_no = vehicle.MFD_VEH_NO.ToUpper();
                            string p_mfd_dri = vehicle.MFD_DRI;
                            Int32 p_mfd_act = vehicle.MFD_ACT;
                            DateTime p_mfd_frm_dt = vehicle.MFD_FRM_DT;
                            DateTime p_mfd_to_dt = vehicle.MFD_TO_DT;
                            string p_mfd_cre_by = userId;
                            DateTime p_mfd_cre_dt = serverDatetime;
                            string p_mfd_mod_by = userId;
                            DateTime p_mfd_mod_dt = serverDatetime;
                            string p_mfd_com = drivercom;
                            string p_mfd_pc = userDefPro;
                            Int32 result = _ToursDAL.sp_tours_update_driver_alloc(seq_no, p_mfd_veh_no, p_mfd_dri, p_mfd_act, p_mfd_frm_dt, p_mfd_to_dt, p_mfd_cre_by, p_mfd_cre_dt, p_mfd_mod_by, p_mfd_mod_dt, p_mfd_com, p_mfd_pc);
                            //sp_tours_update_driver_alloc(seq_no, p_mfd_veh_no, p_mfd_dri, p_mfd_act, p_mfd_frm_dt, p_mfd_to_dt, p_mfd_cre_by, p_mfd_cre_dt, p_mfd_mod_by, p_mfd_mod_dt, p_mfd_com, p_mfd_pc);
                        }
                    }
                }

                _ToursDAL.TransactionCommit();
                _ToursDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                _ToursDAL.TransactionRollback();
                err = ex.Message;
            }

            return count;
        }

        public Int32 UpdateEmployeeData(MST_EMPLOYEE_NEW_TBS mst_employee_tbs, List<MST_PCEMP> mst_pcemp, List<mst_fleet_driver> vehicleList, string userId, string userDefPro, DateTime serverDatetime, string driverCom, out string err)
        {
            int count = 0;
            err = string.Empty;
            try
            {
                _ToursDAL = new ToursDAL();
                _ToursDAL.ConnectionOpen();
                _ToursDAL.BeginTransaction();
                count = _ToursDAL.UpdateEmployeeData(mst_employee_tbs);

                if (count == 1)
                {
                    foreach (MST_PCEMP pcemp in mst_pcemp)
                    {
                        int coutItm = _ToursDAL.UpdatePcemp(pcemp);
                    }
                    if (vehicleList != null)
                    {
                        foreach (mst_fleet_driver vehicle in vehicleList)
                        {

                            string seq_no = vehicle.MFD_SEQ.ToString();
                            string drivercom = driverCom;
                            string p_mfd_veh_no = vehicle.MFD_VEH_NO.ToUpper();
                            string p_mfd_dri = vehicle.MFD_DRI;
                            Int32 p_mfd_act = vehicle.MFD_ACT;
                            DateTime p_mfd_frm_dt = vehicle.MFD_FRM_DT;
                            DateTime p_mfd_to_dt = vehicle.MFD_TO_DT;
                            string p_mfd_cre_by = userId;
                            DateTime p_mfd_cre_dt = serverDatetime;
                            string p_mfd_mod_by = userId;
                            DateTime p_mfd_mod_dt = serverDatetime;
                            string p_mfd_com = drivercom;
                            string p_mfd_pc = userDefPro;
                            Int32 result = _ToursDAL.sp_tours_update_driver_alloc(seq_no, p_mfd_veh_no, p_mfd_dri, p_mfd_act, p_mfd_frm_dt, p_mfd_to_dt, p_mfd_cre_by, p_mfd_cre_dt, p_mfd_mod_by, p_mfd_mod_dt, p_mfd_com, p_mfd_pc);
                            //sp_tours_update_driver_alloc(seq_no, p_mfd_veh_no, p_mfd_dri, p_mfd_act, p_mfd_frm_dt, p_mfd_to_dt, p_mfd_cre_by, p_mfd_cre_dt, p_mfd_mod_by, p_mfd_mod_dt, p_mfd_com, p_mfd_pc);

                        }
                    }
                }

                _ToursDAL.TransactionCommit();
                _ToursDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                _ToursDAL.TransactionRollback();
                err = ex.Message;
                count = -1;
            }

            return count;
        }
        public List<mst_fleet_driver> getAllocateVehicles(string driver)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.getAllocateVehicles(driver);
        }
        public List<Mst_Fleet_driver_new> getAllocateVehiclesnew(string driver)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.getAllocateVehiclesnew(driver);
        }

        public List<mst_fleet_driver> getAllocateVehiclesByID(string regNo)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.getAllocateVehiclesByID(regNo);
        }

        public string getBankCode(string bankId)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.getBankCode(bankId);
        }
        //subodana
        public Int32 SaveFleetData(string SeqNo, string p_mstf_regno, DateTime p_mstf_dt, string p_mstf_brd, string p_mstf_model,
            string p_mstf_veh_tp, string p_mstf_sipp_cd, Int32 p_mstf_st_meter, string p_mstf_own, string p_mstf_own_nm, Int32 p_mstf_own_cont, Int32 p_mstf_lst_sermet, string p_mstf_tou_regno, Int32 p_mstf_is_lease, DateTime p_mstf_insu_exp, DateTime p_mstf_reg_exp, string p_mstf_fual_tp, Int32 p_mstf_act, string p_mstf_cre_by, DateTime p_mstf_cre_dt, string p_mstf_mod_by, DateTime p_mstf_mod_dt, string p_mstf_engin_cap,
            Int32 p_mstf_noof_seat, string p_mst_email, string p_mst_comment, string p_mst_inscom, string p_mfd_veh_no,
            string p_mfd_dri, Int32 p_mfd_act, DateTime p_mfd_frm_dt, DateTime p_mfd_to_dt, string p_mfd_cre_by,
            DateTime p_mfd_cre_dt, string p_mfd_mod_by, DateTime p_mfd_mod_dt, string p_mfd_com, string p_mfd_pc,
            List<mst_fleet_alloc> mst_pcemp, decimal cost, string reason, string ownadd1, string ownadd2,
            DateTime fromdt, DateTime todate, string p_nic, decimal mileage, decimal fullday, decimal halfday, decimal air, decimal corramount, decimal deposit)
        {
            int count = 0;
            // err = string.Empty;
            try
            {
                _ToursDAL = new ToursDAL();
                _ToursDAL.ConnectionOpen();
                _ToursDAL.BeginTransaction();
                count = _ToursDAL.sp_tours_update_fleet(p_mstf_regno, p_mstf_dt, p_mstf_brd, p_mstf_model, p_mstf_veh_tp, p_mstf_sipp_cd, p_mstf_st_meter, p_mstf_own, p_mstf_own_nm, p_mstf_own_cont, p_mstf_lst_sermet, p_mstf_tou_regno, p_mstf_is_lease, p_mstf_insu_exp, p_mstf_reg_exp, p_mstf_fual_tp, p_mstf_act, p_mstf_cre_by, p_mstf_cre_dt, p_mstf_mod_by, p_mstf_mod_dt, p_mstf_engin_cap, p_mstf_noof_seat, p_mst_email, p_mst_inscom, p_mst_comment, cost, reason, ownadd1, ownadd2, fromdt, todate, p_nic, mileage, fullday, halfday, air, corramount, deposit);

                if (count == 1 && p_mfd_frm_dt != new DateTime(1974, 7, 10, 7, 10, 24))
                {
                    count = _ToursDAL.sp_tours_update_driver_alloc(SeqNo, p_mfd_veh_no, p_mfd_dri, p_mfd_act, p_mfd_frm_dt, p_mfd_to_dt, p_mfd_cre_by, p_mfd_cre_dt, p_mfd_mod_by, p_mfd_mod_dt, p_mfd_com, p_mfd_pc);
                }
                if (count == 1)
                {
                    foreach (mst_fleet_alloc prof in mst_pcemp)
                    {
                        string p_mfa_pc = prof.MFA_PC;
                        string p_mfa_regno = p_mstf_regno;
                        int p_mfa_act = prof.MFA_ACT;
                        string p_mfa_cre_by = p_mstf_cre_by;
                        DateTime p_mfa_cre_dt = p_mstf_cre_dt;
                        string p_mfa_mod_by = p_mstf_mod_by;
                        DateTime p_mfa_mod_dt = p_mstf_mod_dt;

                        count = _ToursDAL.sp_tours_update_fleet_alloc(p_mfa_regno, p_mfa_pc, p_mfa_act, p_mfa_cre_by, p_mfa_cre_dt, p_mfa_mod_by, p_mfa_mod_dt);
                    }


                }

                _ToursDAL.TransactionCommit();
                _ToursDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                _ToursDAL.TransactionRollback();
                //err = ex.Message;
            }

            return count;
        }

        public MST_FLEET_NEW getMstFleetDetails(string regNo)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.getMstFleetDetails(regNo);
        }


        public List<mst_fleet_alloc> Get_mst_fleet_alloc(string regNo)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.Get_mst_fleet_alloc(regNo);
        }
        public string getAdvanceRefAmount(string cuscd, string company, string receiptno)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.getAdvanceRefAmount(cuscd, company, receiptno);
        }
        public string getCreditRefAmount(string cuscd, string company, string refNo, string profcen)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.getCreditRefAmount(cuscd, company, refNo, profcen);
        }
        //Subodana 2016-02-24
        public Int32 UPDATE_DRIVER_ALLO_STATUS_TO_INACT(Int32 MFD_SEQ)
        {
            int resutl = 0;
            _ToursDAL = new ToursDAL();
            resutl = _ToursDAL.UPDATE_DRIVER_ALLO_STATUS_TO_INACT(MFD_SEQ);
            return resutl;
        }

        public Int32 UPDATE_DRIVER_ALLO_STATUS_TO_ACT(Int32 MFD_SEQ)
        {
            int resutl = 0;
            _ToursDAL = new ToursDAL();
            resutl = _ToursDAL.UPDATE_DRIVER_ALLO_STATUS_TO_ACT(MFD_SEQ);
            return resutl;
        }
        public GEN_CUST_ENQ getEnquiryDetails(string Com, string PC, string ENQID)
        {
            GEN_CUST_ENQ custEnq = new GEN_CUST_ENQ();
            ToursDAL _ToursDAL = new ToursDAL();
            custEnq = _ToursDAL.GET_CUST_ENQRY(Com, PC, ENQID);
            MST_EMPLOYEE_TBS oMST_EMPLOYEE_TBS = GetEmployeeByComPC(Com, PC, custEnq.GCE_DRIVER);
            if (oMST_EMPLOYEE_TBS != null && oMST_EMPLOYEE_TBS.MEMP_EPF != null)
            {
                GroupBussinessEntity be = new GroupBussinessEntity();
                be.Mbg_name = oMST_EMPLOYEE_TBS.MEMP_FIRST_NAME + " " + oMST_EMPLOYEE_TBS.MEMP_LAST_NAME;
                be.Mbg_contact = oMST_EMPLOYEE_TBS.MEMP_MOBI_NO;
                custEnq.DRIVER_DETAILS = be;
            }
            List<InvoiceHeader> hed = _ToursDAL.getInvoiceHedDataByEnqId(ENQID);
            List<InvoiceItem> items = new List<InvoiceItem>();
            if (hed.Count > 0)
            {
                items = saleGetInvoiceDetail(hed[0].Sah_seq_no);
            }

            custEnq.CHARGER_VALUE = items;
            return custEnq;
        }
        public GroupBussinessEntity GetCustomerProfileByGrup(string CustCD, string nic, string DL, string PPNo, string brNo, string mobNo)
        {
            _salesDAL = new SalesDAL();
            return _salesDAL.GetCustomerProfileByGrup(CustCD, nic, DL, PPNo, brNo, mobNo);
        }
        public List<InvoiceItem> saleGetInvoiceDetail(Int32 seqNo)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.GetInvoiceDetail(seqNo);
        }

        public Int32 UPDATE_ENQ_STATUS_WITH_REASON(string GCE_ENQ_ID, string GCE_ENQ)
        {
            int resutl = 0;
            _ToursDAL = new ToursDAL();
            resutl = _ToursDAL.UPDATE_ENQ_STATUS_WITH_REASON(GCE_ENQ_ID, GCE_ENQ);
            return resutl;
        }

        //subodana
        public int Save_ENQ_DATA(GEN_CUST_ENQ oItem, MasterAutoNumber _ReqInsAuto, GEN_CUST_ENQSER sItem, out string err, string countrycode, string userDefPro, MasterBusinessEntity cus)
        {
            int resutl = 0;
            Boolean _isNew = false;
            string errMSG = string.Empty;
            err = string.Empty;

            try
            {
                _ToursDAL = new ToursDAL();
                _inventoryDAL = new InventoryDAL();
                _generalDAL = new GeneralDAL();
                _salesDAL = new SalesDAL();

                _ToursDAL.ConnectionOpen();
                _inventoryDAL.ConnectionOpen();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                _inventoryDAL.BeginTransaction();
                _ToursDAL.BeginTransaction();
                _salesDAL.ConnectionOpen();
                _salesDAL.BeginTransaction();

                //save custormer


                if (cus.Mbe_cd == "CASH" || cus.Mbe_cd == "")
                {
                    MasterAutoNumber _auto = new MasterAutoNumber();
                    _auto.Aut_moduleid = "CUS";
                    _auto.Aut_number = 0;
                    _auto.Aut_start_char = "CONT";

                    MasterAutoNumber _number = _inventoryDAL.GetAutoNumber(_auto.Aut_moduleid, _auto.Aut_direction, _auto.Aut_start_char, _auto.Aut_cate_tp, _auto.Aut_cate_cd, _auto.Aut_modify_dt, _auto.Aut_year);
                    string _cusNo = _auto.Aut_start_char + "-" + _number.Aut_number.ToString("000000", CultureInfo.InvariantCulture);
                    _inventoryDAL.UpdateAutoNumber(_auto);
                    cus.Mbe_cd = _cusNo;

                    int effect = _salesDAL.SaveBusinessEntityDetail(cus);
                }



                if (String.IsNullOrEmpty(oItem.GCE_ENQ_ID) && oItem.GCE_SEQ == 0)
                {
                    MasterAutoNumber _reversInv = _inventoryDAL.GetAutoNumber(_ReqInsAuto.Aut_moduleid, _ReqInsAuto.Aut_direction, _ReqInsAuto.Aut_start_char, _ReqInsAuto.Aut_cate_tp, _ReqInsAuto.Aut_cate_cd, _ReqInsAuto.Aut_modify_dt, _ReqInsAuto.Aut_year);
                    _reversInv.Aut_direction = null;
                    _reversInv.Aut_modify_dt = null;
                    err = userDefPro + "/" + oItem.GCE_DEST_CONTRY + "/" + _reversInv.Aut_number.ToString("00000", CultureInfo.InvariantCulture);
                    _inventoryDAL.UpdateAutoNumber(_reversInv);
                    oItem.GCE_ENQ_ID = err;
                    sItem.GCS_SEQ = _reversInv.Aut_number;
                    _isNew = true;
                }
                else
                {
                    oItem.GCE_ENQ_ID = oItem.GCE_ENQ_ID;
                    oItem.GCE_SEQ = oItem.GCE_SEQ;

                    sItem.GCS_ENQ_ID = oItem.GCE_ENQ_ID;
                }

                resutl = _ToursDAL.Save_GEN_CUST_ENQ(oItem);

                List<SearchEnqSEQ> GetseqDetails = _ToursDAL.GetseqDetails(oItem.GCE_ENQ_ID);
                int SEQ = GetseqDetails.FirstOrDefault().gce_seq;
                sItem.GCS_SEQ = SEQ;

                GEN_ENQLOG oLog = new GEN_ENQLOG();
                oLog.GEL_ENQ_ID = oItem.GCE_ENQ_ID;
                oLog.GEL_USR = oItem.GCE_CRE_BY;
                oLog.GEL_STAGE = oItem.GCE_STUS;
                oLog.GEL_LOGWHEN = DateTime.Now;
                resutl = _ToursDAL.SAVE_GEN_ENQLOG(oLog);

                sItem.GCS_ENQ_ID = oItem.GCE_ENQ_ID;
                resutl = _ToursDAL.SAVE_GEN_ENQSER(sItem);

                if (_isNew == true)
                {
                    err = "Successfully Created , Enquiry ID  " + oItem.GCE_ENQ_ID;
                }
                else
                {
                    err = "Successfully Updated , Enquiry ID  " + oItem.GCE_ENQ_ID;
                }

                //OutSMS _out = new OutSMS();
                //if (oItem.GCE_MOB != "N/A" && !string.IsNullOrEmpty(oItem.GCE_MOB))
                //{
                //    String msg = "Dear Customer, \nYou have created a enquiry.\nEnquiry Id - " + oItem.GCE_ENQ_ID;
                //    String mobi = "+94" + oItem.GCE_MOB.Substring(1, 9);
                //    _out.Msgstatus = 0;
                //    _out.Msgtype = "S";
                //    _out.Receivedtime = DateTime.Now;
                //    _out.Receiver = mobi;
                //    _out.Msg = msg;
                //    _out.Receiverphno = mobi;
                //    _out.Senderphno = mobi;
                //    _out.Refdocno = "0";
                //    _out.Sender = "Abans Tours";
                //    _out.Createtime = DateTime.Now;
                //    SendSMS_InternalMethod(_out, out errMSG);
                //}


                _ToursDAL.TransactionCommit();
                _ToursDAL.ConnectionClose();

                _inventoryDAL.TransactionCommit();
                _inventoryDAL.ConnectionClose();

                _generalDAL.TransactionCommit();
                _generalDAL.ConnectionClose();

                _salesDAL.TransactionCommit();
                _salesDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                err = ex.Message;
                _ToursDAL.TransactionRollback();
                _inventoryDAL.TransactionRollback();
                _generalDAL.TransactionRollback();
                _salesDAL.TransactionRollback();
            }
            return resutl;
        }
        public Int32 SAVE_GEN_ENQSER(GEN_CUST_ENQSER lst, out string err)
        {
            int resutl = 0;
            err = string.Empty;

            try
            {
                _ToursDAL = new ToursDAL();
                _ToursDAL.ConnectionOpen();
                _ToursDAL.BeginTransaction();

                resutl = _ToursDAL.SAVE_GEN_ENQSER(lst);

                _ToursDAL.TransactionCommit();
                _ToursDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                _ToursDAL.TransactionRollback();
                err = ex.Message;
            }

            return resutl;
        }
        public List<SEARCH_MST_EMP> Get_mst_emp(string _company, string _profitcenter)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.Get_mst_emp(_company, _profitcenter);
        }
        public MST_EMPLOYEE_NEW_TBS getMstEmployeeDetailsByNic(string Nic)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.getMstEmployeeDetailsByNic(Nic);
        }

        //Subodana 2016-03-19
        public List<GEN_CUST_ENQ> GET_ENQRY_BY_CUST_PEN_INV(string Com, string CustomerCode)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.GET_ENQRY_BY_CUST(Com, CustomerCode);
        }

        public Int32 UPDATE_ENQ_STATUS(string cuscode, string enqid)
        {
            int resutl = 0;
            _ToursDAL = new ToursDAL();
            resutl = 0;// _ToursDAL.UPDATE_ENQ_STATUS(cuscode, enqid);
            return resutl;
        }
        //Sanjeewa 2016-03-24
        public DataTable Get_DailySalesReport(DateTime _fdate, DateTime _tdate, string _customer, string _itemcode, string _com, string _pc, string _user)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.Get_DailySalesReport(_fdate, _tdate, _customer, _itemcode, _com, _pc, _user);
        }
        //Sanjeewa 2016-04-19
        public DataTable Get_DebtorStatementReport(DateTime _fdate, DateTime _tdate, string _customer, string _com, string _pc, string _user)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.Get_DebtorStatementReport(_fdate, _tdate, _customer, _com, _pc, _user);
        }
        //Subodana 2016-03-24
        public List<GEN_CUST_ENQSER> GetEnqSerData(string enqid)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.GetEnqSerData(enqid);
        }
        //Sanjeewa 2016-03-24
        public DataTable Get_DailySalesDetailReport(DateTime _fdate, DateTime _tdate, string _customer, string _itemcode, string _com, string _pc, string _user)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.Get_DailySalesDetailReport(_fdate, _tdate, _customer, _itemcode, _com, _pc, _user);
        }
        //Sanjeewa 2016-03-25
        public DataTable Get_ReceiptDetailReport(DateTime _fdate, DateTime _tdate, string _customer, string _com, string _pc, string _user)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.Get_ReceiptDetailReport(_fdate, _tdate, _customer, _com, _pc, _user);
        }
        //Sanjeewa 2016-04-01
        public DataTable Get_ATSInquiryReport(DateTime _fdate, DateTime _tdate, string _customer, string _inqid, Int16 _status, string _type, string _com, string _pc, string _user)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.Get_ATSInquiryReport(_fdate, _tdate, _customer, _inqid, _status, _type, _com, _pc, _user);
        }

        //Subodana 2016-03-30
        public List<InvoiceHeader> GetInvoiceData(string enqid)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.GetInvoiceData(enqid);
        }
        //subodana
        public List<InvoiceItem> GetInvoiceDetailforInvNo(string InvNo)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.GetInvoiceDetailforInvNo(InvNo);
        }
        //Subodana 2016-03-31
        public List<InvoiceHeader> GetInvoiceHDRData(string InvNo)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.GetInvoiceHDRData(InvNo);
        }
        public string GetServiceByCode(string company, string userDefPro, string chgCd)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.GetServiceByCode(company, userDefPro, chgCd);
        }
        public string GetServiceByCodeTRANS(string company, string userDefPro, string chgCd)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.GetServiceByCodeTRANS(company, userDefPro, chgCd);
        }
        public string GetServiceByCodeMSCELNS(string company, string userDefPro, string chgCd)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.GetServiceByCodeMSCELNS(company, userDefPro, chgCd);
        }
        public Int32 genarateCostngPurchaseOrder(List<MST_PR_HED_DET> hetdet, List<string> serpro, MasterAutoNumber _Auto, out string err, string company, string pc, string userid, string sessionid, Int32 statPo, Int32 statCost)
        {
            Int32 effect = 0;
            Int32 resutl = 0;
            err = string.Empty;

            try
            {
                _ToursDAL = new ToursDAL();
                _inventoryDAL=new InventoryDAL();
                _ToursDAL.ConnectionOpen();
                _inventoryDAL.ConnectionOpen();
                _ToursDAL.BeginTransaction();
                _inventoryDAL.BeginTransaction();

                MST_PR_HDR prHeader = new MST_PR_HDR();
                MST_PR_DET prMainItems = new MST_PR_DET();

                foreach (string serProviders in serpro)
                {
                    List<MST_PR_HED_DET> hed = hetdet.Where(item => item.QCD_ANAL1 == serProviders).ToList();

                    MasterAutoNumber _reversPo = new MasterAutoNumber();
                    _reversPo = _ToursDAL.GetAutoNumber(_Auto.Aut_moduleid, _Auto.Aut_direction, _Auto.Aut_start_char, _Auto.Aut_cate_tp, _Auto.Aut_cate_cd, _Auto.Aut_modify_dt, _Auto.Aut_year);

                    err = _Auto.Aut_cate_cd + "-" + _Auto.Aut_start_char + "-" + _reversPo.Aut_number.ToString("00000", CultureInfo.InvariantCulture);
                    _ToursDAL.UpdateAutoNumber(_Auto);


                    if (hed.Count > 0)
                    {

                        Int32 seq = _inventoryDAL.Generate_new_seq_num(userid, "PO", 1, company);
                        prHeader.PH_SEQ_NO = seq;
                        prHeader.PH_TP = "L";
                        prHeader.PH_SUB_TP = "N";
                        prHeader.PH_DOC_NO = err;
                        prHeader.PH_COM = company;
                        prHeader.PH_PROFIT_CD = pc;
                        prHeader.PH_DT = DateTime.Now;
                        prHeader.PH_REF = hed[0].QCH_OTH_DOC;
                        prHeader.PH_JOB_NO = hed[0].QCH_COST_NO;
                        //prHeader.PH_PAY_TERM      
                        prHeader.PH_SUPP = serProviders;
                        prHeader.PH_CUR_CD = hed[0].QCD_CURR;
                        prHeader.PH_EX_RT = hed[0].QCD_EX_RATE;
                        //prHeader.PH_TRANS_TERM    
                        //prHeader.PH_PORT_OF_ORIG  
                        //prHeader.PH_CRE_PERIOD    
                        //prHeader.PH_FRM_YER       
                        //prHeader.PH_FRM_MON       
                        //prHeader.PH_TO_YER        
                        //prHeader.PH_TO_MON        
                        //prHeader.PH_PREFERD_ETA   
                        //prHeader.PH_CONTAIN_KIT   
                        //prHeader.PH_SENT_TO_VENDOR
                        //prHeader.PH_SENT_BY       
                        //prHeader.PH_SENT_VIA      
                        //prHeader.PH_SENT_ADD      
                        prHeader.PH_STUS = "A";
                        prHeader.PH_REMARKS = "";
                        prHeader.PH_SUB_TOT = hed.Sum(x => x.QCD_TOT_COST);
                        prHeader.PH_TAX_TOT = 0;
                        prHeader.PH_DIS_RT = 0;
                        prHeader.PH_DIS_AMT = 0;
                        prHeader.PH_OTH_TOT = 0;
                        prHeader.PH_TOT = hed.Sum(x => x.QCD_TOT_COST);
                        //prHeader.PH_REPRINT       
                        //prHeader.PH_TAX_CHG       
                        //prHeader.PH_IS_CONSPO     
                        prHeader.PH_CRE_BY = userid;
                        prHeader.PH_CRE_DT = DateTime.Now;
                        //prHeader.PH_MOD_BY        
                        //prHeader.PH_MOD_DT        
                        prHeader.PH_SESSION_ID = sessionid;

                        effect = _ToursDAL.Save_costPurchaseOrderHead(prHeader);
                        if (effect > 0)
                        {
                            Int32 i = 1;
                            foreach (MST_PR_HED_DET alldet in hed)
                            {
                                prMainItems.PD_SEQ_NO = effect;
                                prMainItems.PD_LINE_NO = i;
                                prMainItems.PD_ITM_CD = alldet.QCD_CAT;
                                prMainItems.PD_ITM_STUS = "GDLP";
                                //prMainItems.PD_ITM_TP=
                                prMainItems.PD_QTY = alldet.QCD_QTY;
                                //prMainItems.PD_UOM
                                prMainItems.PD_UNIT_PRICE = alldet.QCD_UNIT_COST;
                                //prMainItems.PD_LINE_VAL
                                prMainItems.PD_DIS_RT = 0;
                                prMainItems.PD_DIS_AMT = 0;
                                prMainItems.PD_LINE_TAX = 0;
                                prMainItems.PD_LINE_AMT = 0;
                                prMainItems.PD_PI_BAL = 0;
                                prMainItems.PD_LC_BAL = 0;
                                prMainItems.PD_SI_BAL = 0;
                                prMainItems.PD_GRN_BAL = 0;
                                //prMainItems.PD_REF_NO
                                //prMainItems.PD_ACT_UNIT_PRICE
                                //prMainItems.PD_ITEM_DESC
                                //prMainItems.PD_KIT_LINE_NO
                                //prMainItems.PD_KIT_ITM_CD
                                //prMainItems.PD_VAT
                                //prMainItems.PD_NBT
                                //prMainItems.PD_VAT_BEFORE
                                //prMainItems.PD_NBT_BEFORE
                                //prMainItems.PD_TOT_TAX_BEFORE
                                resutl = _ToursDAL.Save_costPurchaseOrderDet(prMainItems);
                                i++;
                            }
                        }

                    }
                }
                string costNo = err;
                Int32 res = 0;// UPDATE_COST_HDR_STATUS(statPo, statCost, hetdet[0].QCH_SEQ, company, pc, userid, hetdet[0].QCH_OTH_DOC, out  err);
                Int32 result2 = 0;
                res = _ToursDAL.UPDATE_COST_HDR_STATUS(statCost, hetdet[0].QCH_SEQ, company, pc);

                res = _ToursDAL.UpdateEnquiryStage(statPo, userid, hetdet[0].QCH_OTH_DOC, company, pc);

                GEN_ENQLOG oLog = new GEN_ENQLOG();
                oLog.GEL_ENQ_ID = hetdet[0].QCH_OTH_DOC;
                oLog.GEL_USR = userid;
                oLog.GEL_STAGE = statPo;
                oLog.GEL_LOGWHEN = DateTime.Now;
                result2 = _ToursDAL.SAVE_GEN_ENQLOG(oLog);


                if (res > 0)
                {
                    err = costNo;
                }
                _ToursDAL.TransactionCommit();
                _ToursDAL.ConnectionClose();

                _inventoryDAL.TransactionCommit();
                _inventoryDAL.ConnectionClose();
            }
            catch (Exception ex)
            {
                _ToursDAL.TransactionRollback();
                _inventoryDAL.TransactionRollback();
                err = ex.Message;
            }

            return effect;
        }
        public List<MST_PR_HED_DET> getcostingforPurchaseOrder(string costNo, string com, string procen)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.getcostingforPurchaseOrder(costNo, com, procen);
        }
        public List<String> getcostingSerProPurchaseOrder(string costNo)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.getcostingSerProPurchaseOrder(costNo);
        }
        //subodana
        public List<InvoiceHeader> GetAllSalesHRDdata(string com, string procen, DateTime startdate, DateTime enddate)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.GetAllSalesHRDdata(com, procen, startdate, enddate);
        }
        public List<RecieptHeader> GetAllRecieptHRDdata(string com, string procen, DateTime startdate, DateTime enddate)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.GetAllRecieptHRDdata(com, procen, startdate, enddate);
        }
        public List<MST_GNR_ACC> GetgrnalDetails(string com)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.GetgrnalDetails(com);
        }
        //subodana 2016-04-09
        public Int32 UPDATE_INV_HDRENGLOG(string invNo, Int32 value)
        {
            int resutl = 0;
            _ToursDAL = new ToursDAL();
            resutl = _ToursDAL.UPDATE_INV_HDRENGLOG(invNo, value);
            return resutl;
        }
        public Int32 UPDATE_RECIEPT_HDRENGLOG(string recNo, Int32 value)
        {
            int resutl = 0;
            _ToursDAL = new ToursDAL();
            resutl = _ToursDAL.UPDATE_RECIEPT_HDRENGLOG(recNo, value);
            return resutl;
        }
        public List<MST_ST_PAX_DET> GetInvoicePaxDet(string invNo)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.GetInvoicePaxDet(invNo);
        }
        public List<GEN_CUST_ENQ> SP_TOUR_GET_TRANSPORT_ENQRY(string Com, string PC, String Status, string type, string UserID, Int32 PermissionCode)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            _securityDAL = new SecurityDAL();
            List<GEN_CUST_ENQ> oItems = new List<GEN_CUST_ENQ>();
            string ProfitC = string.Empty;
            int _effect = _securityDAL.Is_OptionPerimitted(Com, UserID, PermissionCode);
            if (_effect > 0)
            {
                ProfitC = string.Empty; ;
            }
            else
            {
                ProfitC = PC;
            }
            return _ToursDAL.SP_TOUR_GET_TRANSPORT_ENQRY(Com, ProfitC, Status, type);
        }
        public InvoiceHeader getInvoiceHederData(string invNo, string com, string procen)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.getInvoiceHederData(invNo, com, procen);
        }
        public Int32 cancelInvoice(InvoiceHeader _invoiceHeder, RecieptHeader _recieptHeader, Int32 EnquiryStages, Int32 ToursStatus, out string _error)
        {
            Int32 result = 0;
            try
            {
                _error = string.Empty;
                ToursDAL _ToursDAL = new ToursDAL();
                _ToursDAL.ConnectionOpen();
                _ToursDAL.BeginTransaction();


                result = _ToursDAL.SaveInvoiceHeader(_invoiceHeder);
                if (result > 0)
                {
                    if (_recieptHeader != null)
                        result = _ToursDAL.updateReceiptHeader(_recieptHeader);
                    if (result > 0)
                    {
                        //result = _ToursDAL.UPDATE_COST_HDR_STATUS(ToursStatus, hetdet[0].QCH_SEQ, company, pc);

                        result = _ToursDAL.UpdateEnquiryStage(EnquiryStages, _invoiceHeder.Sah_cre_by, _invoiceHeder.Sah_ref_doc, _invoiceHeder.Sah_com, _invoiceHeder.Sah_pc);
                        if (result <= 0)
                        {
                            _error = "Unable to update enquiry status.Enquiry Id :" + _invoiceHeder.Sah_ref_doc;
                        }
                    }
                    else
                    {
                        _error = "Unable to update receipt.Receipt No :" + _recieptHeader.Sar_receipt_no;
                    }
                }
                else
                {
                    _error = "Unable to update invoice.Invoice No :" + _invoiceHeder.Sah_inv_no;
                }

                if (_error != "")
                {
                    _ToursDAL.TransactionRollback();
                    _ToursDAL.ConnectionClose();
                }
                else
                {
                    _ToursDAL.TransactionCommit();
                    _ToursDAL.ConnectionClose();
                }


            }
            catch (Exception ex)
            {
                _error = ex.Message;
                _ToursDAL.TransactionRollback();
                _ToursDAL.ConnectionClose();
            }
            return result;
        }
        public List<RecieptItem> getReceiptItemList(string invNo)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.getReceiptItemList(invNo);
        }
        public MST_CHKINOUT getEnqChkData(string enqId)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.getEnqChkData(enqId);
        }
        public Int32 saveCheckoutDetails(MST_CHKINOUT check, out string _error)
        {
            _error = string.Empty;
            Int32 result = 0;
            ToursDAL _ToursDAL = new ToursDAL();
            _ToursDAL.ConnectionOpen();
            _ToursDAL.BeginTransaction();
            try
            {
                result = _ToursDAL.saveCheckoutDetails(check);
                if (result > 0)
                {
                    _ToursDAL.TransactionCommit();
                    _ToursDAL.ConnectionClose();
                }
                else
                {
                    _error = "Unavle to update enquiry check.";
                    _ToursDAL.TransactionRollback();
                    _ToursDAL.ConnectionClose();
                }

            }
            catch (Exception ex)
            {
                _error = ex.Message.ToString();
                _ToursDAL.TransactionRollback();
                _ToursDAL.ConnectionClose();
            }
            return result;
        }
        public List<MST_TEMP_MESSAGES> getTempSmsMessage(string company, string pc, string msgtyp)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.getTempSmsMessage(company, pc, msgtyp);
        }
        public List<GEN_CUST_ENQ> getAllEnquiryData(string enqId, string Com, string PC, String Status, string UserID, Int32 PermissionCode)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            _securityDAL = new SecurityDAL();
            List<GEN_CUST_ENQ> oItems = new List<GEN_CUST_ENQ>();
            string ProfitC = string.Empty;
            int _effect = _securityDAL.Is_OptionPerimitted(Com, UserID, PermissionCode);
            if (_effect > 0)
            {
                ProfitC = string.Empty; ;
            }
            else
            {
                ProfitC = PC;
            }
            return _ToursDAL.getAllEnquiryData(enqId, Com, ProfitC, Status);
        }
        //Nuwan 2016/05/04
        public List<QUO_COST_HDR> GET_INVOICEDCOST_PRFITABILITY_DETAILS(string _com, string _procenter, string _req, string _costRef, string _customer, string _category,
            DateTime _fromdate, DateTime _todate)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.GET_INVOICEDCOST_PRFITABILITY_DETAILS(_com, _procenter, _req, _costRef, _customer, _category, _fromdate, _todate);
        }
        //Nuwan 2016/05/04
        public List<QUO_COST_HDR> GET_INVOICEDCOST_PRFITABILITY(string _com, string _procenter, string _req, string _costRef, string _customer, string _category,
            DateTime _fromdate, DateTime _todate)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.GET_INVOICEDCOST_PRFITABILITY(_com, _procenter, _req, _costRef, _customer, _category, _fromdate, _todate);
        }
        //Nuwan 2016/05/05
        public int SaveToursrInvoiceReverce(InvoiceHeader _invoiceHeader, List<InvoiceItem> _invoiceItem, MasterAutoNumber _invoiceAuto, out  string _invoiceNo, MasterBusinessEntity _businessCompany, out string _errorlist, List<MST_ST_PAX_DET> paxDetList = null, InvoiceHeader _oldInvoiceHeader = null)
        {
            string _invNo = string.Empty;
            string _recNo = string.Empty;
            string _DONo = string.Empty;
            string _buybackinv = string.Empty;
            Int32 _effect = 0;
            string _location = string.Empty;
            string _error = string.Empty;
            string _db = string.Empty;
            MasterAutoNumber _auto = null;
            bool _VoucherPromotion = false;
            Boolean _isNew = false;
            string enqMsg = string.Empty;
            bool _isHoldInvoiceProcess = false;
            try
            {
                _db = DataBase._ems;
                _salesDAL = new SalesDAL();
                _salesDAL.ConnectionOpen();
                _salesDAL.BeginTransaction();
                _db = DataBase._ems;
                _inventoryDAL = new InventoryDAL();
                _inventoryDAL.ConnectionOpen();
                _inventoryDAL.BeginTransaction();
                _db = DataBase._fms;
                _fmsInventoryDal = new FMS_InventoryDAL();
                _fmsInventoryDal.ConnectionOpen();
                _fmsInventoryDal.BeginTransaction();
                _db = DataBase._reportdb;
                _inventoryRepDAL = new ReptCommonDAL();
                _inventoryRepDAL.ConnectionOpen();
                _inventoryRepDAL.BeginTransaction();
                _db = DataBase._ems;
                _ToursDAL = new ToursDAL();
                _ToursDAL.ConnectionOpen();
                _ToursDAL.BeginTransaction();

                if (_invoiceHeader != null)
                {
                    _invoiceAuto.Aut_year = null;
                    MasterAutoNumber InvoiceAuto = _inventoryDAL.GetAutoNumber(_invoiceAuto.Aut_moduleid, _invoiceAuto.Aut_direction, _invoiceAuto.Aut_start_char, _invoiceAuto.Aut_cate_tp, _invoiceAuto.Aut_cate_cd, _invoiceAuto.Aut_modify_dt, _invoiceAuto.Aut_year);
                    _invNo = _invoiceAuto.Aut_start_char + InvoiceAuto.Aut_number.ToString("00000", CultureInfo.InvariantCulture);
                    _invoiceAuto.Aut_year = null;
                    _invoiceAuto.Aut_modify_dt = null;
                    _salesDAL.UpdateInvoiceAutoNumber(_invoiceAuto);
                    _invoiceHeader.Sah_inv_no = _invNo;
                    _invoiceHeader.Sah_ref_doc = _invoiceHeader.Sah_anal_4;
                }

                _db = string.Empty;
                _location = _invoiceHeader.Sah_com;
                string _recieptSeq = null;
                string _invoiceSeq = null;
                InventoryHeader _invHdr = null;
                DataTable _dataTable = null;

                string invNo = (_invNo != "") ? _invNo : "";

                //CommonSaveInvoiceWithDeliveryOrderWithTransaction(_invoiceHeader, _invoiceItem,null, null,null, null,null,null, _invoiceAuto, null, null, false, out  string _invoiceNo, out _receiptNo, out _deliveryOrder, null,null, null, false, false, out  _error, false, out  _invoiceSeq, out  _recieptSeq, out _invHdr,  out _dataTable,string invNo);
                CommonSaveInvoiceWithDeliveryOrderWithTransaction(_invoiceHeader, _invoiceItem, null, null, null, null, null, null, _invoiceAuto, null, null, false, out _invNo, out _recNo, out _DONo, _inventoryDAL, _salesDAL, _inventoryRepDAL, false, _isHoldInvoiceProcess, out _error, false, out _invoiceSeq, out _recieptSeq, out _invHdr, out _dataTable, invNo);


                Int32 result = 0;

                result = _ToursDAL.UpdateEnquiryStage(13, _invoiceHeader.Sah_cre_by, _invoiceHeader.Sah_ref_doc, _invoiceHeader.Sah_com, _invoiceHeader.Sah_pc);

                result = _ToursDAL.updateOldTourInvoiceStatus(_oldInvoiceHeader);
                //2015-06-26
                if (_invoiceHeader != null)
                {
                    foreach (InvoiceItem item in _invoiceItem)
                    {
                        result = _ToursDAL.UPDATE_LOG_HDR_INVOICE(Convert.ToInt32(item.Sad_promo_cd), 1, _invoiceHeader.Sah_cre_by);
                    }
                }


                string _customerCode = (_invoiceHeader != null) ? _invoiceHeader.Sah_cus_cd : "";
                GroupBussinessEntity _businessEntityGrup = new GroupBussinessEntity();
                if (string.IsNullOrEmpty(_error))
                {
                    #region Customer Creation

                    #endregion Customer Creation

                    #region update auto no


                    #endregion update auto no

                    #region Update Manual Doc
                    if (_invoiceHeader != null)
                        if (_invoiceHeader.Sah_manual) _fmsInventoryDal.UpdateManualDocNo(_location, "MDOC_INV", Convert.ToInt32(_invoiceHeader.Sah_ref_doc), _invNo);

                    #endregion Update Manual Doc

                    #region update invoice discount / Promotion Voucher page as F
                    if (_invoiceHeader != null)
                    {
                        foreach (InvoiceItem _itm in _invoiceItem)
                        {
                            if (_itm.Sad_dis_type == "P")
                            {
                                _salesDAL.UpdateDiscountUsedTimes(_itm.Sad_dis_seq, 1);
                            }

                            if (_itm.Sad_res_no == "PROMO_VOU" && _itm.Sad_res_line_no > 0)
                            {
                                //Add by Chamal 6-Jul-2014
                                _salesDAL.Update_GV_Pages(1, _invoiceHeader.Sah_com, _invoiceHeader.Sah_pc, _invoiceHeader.Sah_dt.Date, "", "F", _itm.Sad_res_line_no, "P_GV", _invoiceHeader.Sah_cre_by, _invNo);
                            }
                        }


                    #endregion update invoice discount / Promotion Voucher page as F

                        if (paxDetList != null)
                        {
                            Int32 ln = 1;
                            foreach (MST_ST_PAX_DET pax in paxDetList)
                            {
                                pax.SPD_SEQ = Convert.ToInt32(_invoiceSeq);
                                pax.SPD_LINE = ln;
                                pax.SPD_INV_NO = _invNo;
                                pax.SPD_ENQ_ID = _invoiceHeader.Sah_ref_doc;
                                Int32 ef = _ToursDAL.saveInvoicePax(pax);
                                ln++;
                            }
                        }
                    }
                    _error = enqMsg + _error;
                    _error = _error.Trim();
                    _effect = 1;
                }
                else
                    _effect = -1;

                try
                {
                    if (string.IsNullOrEmpty(_error))
                    {
                        _db = DataBase._ems;
                        _salesDAL.TransactionCommit();
                        _db = DataBase._ems;
                        _inventoryDAL.TransactionCommit();
                        _db = DataBase._fms;
                        _fmsInventoryDal.TransactionCommit();
                        _db = DataBase._reportdb;
                        _inventoryRepDAL.TransactionCommit();
                        _db = DataBase._ems;
                        _ToursDAL.TransactionCommit();

                        if (_invNo != "")
                            _inventoryDAL.UpdateInvoiceDOStatus(_invNo);

                        //cus code update
                        if (_auto != null)
                        {
                            _inventoryDAL.UpdateAutoNumber(_auto);
                        }
                    }
                    else
                    {
                        _salesDAL.TransactionRollback();
                        _inventoryDAL.TransactionRollback();
                        _fmsInventoryDal.TransactionRollback();
                        _inventoryRepDAL.TransactionRollback();
                        _ToursDAL.TransactionRollback();
                    }
                }
                catch (Exception ex)
                {
                    _invoiceNo = string.Empty;
                    _errorlist = "Database" + _db + " is not responding. Please contact IT Operation.\n" + ex.Message;
                    _effect = -1;
                    return _effect;
                }
            }
            catch (Exception ex)
            {
                _error = ex.Message.ToString();
                _invoiceNo = string.Empty;
                _errorlist = _error;
                _effect = -1;

                _salesDAL.TransactionRollback();
                _inventoryDAL.TransactionRollback();
                _fmsInventoryDal.TransactionRollback();
                _inventoryRepDAL.TransactionRollback();
                _ToursDAL.TransactionRollback();
            }

            _invoiceNo = _invNo;
            _errorlist = _error;
            return _effect;
        }
        public List<MST_COUNTRY> getCountryDetails(string countryCd)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.getCountryDetails(countryCd);
        }
        public List<MST_CUSTOMER_TYPE> getCustomerTypes()
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.getCustomerTypes();
        }
        public List<RecieptItemTBS> getReceiptItemByinvNo(string invNo, string com, string pc)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.getReceiptItemByinvNo(invNo, com, pc);
        }
        public List<SearchEnqSEQ> GetseqDetails(string ENQID)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.GetseqDetails(ENQID);
        }
        public List<RecieptHeader> GET_RECIEPT_BY_ENQ(string company, string pc, string ENQID)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.GET_RECIEPT_BY_ENQ(company, pc, ENQID);
        }
        public List<ST_MENU> getUserMenu(string userId)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.getUserMenu(userId);
        }
        public ST_MENU getAcccessPermission(string userId, Int32 menuId)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.getAcccessPermission(userId, menuId);
        }
        public MST_PCADDPARA getPcAdditionalPara(string com, string pc, string code)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.getPcAdditionalPara(com, pc, code);
        }
        public List<RecieptHeaderTBS> getReceiptItems(string company, string pc, string type, string enqId)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.getReceiptItems(company, pc, type, enqId);
        }
        public List<ST_SATIS_QUEST> getFeedBackQuestions(string channel, string company, string userDefPro)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            List<ST_SATIS_QUEST> qus = _ToursDAL.getFeedBackQuestions(channel, company, userDefPro);
            foreach (ST_SATIS_QUEST one in qus)
            {
                if (one.SSQ_TYPE == "Radio")
                {
                    List<ST_SATIS_VAL> ans = _ToursDAL.getFeedBackQuestionsAns(one.SSQ_SEQ);
                    var obj = qus.FirstOrDefault(x => x.SSQ_SEQ == one.SSQ_SEQ);
                    if (obj != null) obj.SSQ_SATIS_VAL = ans;
                }
            }
            return qus;
        }
        public List<ST_SATIS_QUEST> getFeedBackQuestionsOnly(string channel, string company, string userDefPro)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.getFeedBackQuestions(channel, company, userDefPro);
        }
        public Int32 SaveCustomerFeedBack(List<ST_SATIS_RESULT> resultData, out string error)
        {
            Int32 result = 0;
            error = "";
            ToursDAL _ToursDAL = new ToursDAL();
            try
            {
                _ToursDAL.ConnectionOpen();
                _ToursDAL.BeginTransaction();
                foreach (ST_SATIS_RESULT data in resultData)
                {
                    result = _ToursDAL.SaveCustomerFeedBac(data);
                    if (result == 0)
                    {
                        result = -1;
                        error = "Unable to save feed back data.";
                        break;
                    }
                }
                if (result == 1)
                {
                    _ToursDAL.TransactionCommit();
                    _ToursDAL.ConnectionClose();
                }
                else
                {
                    _ToursDAL.TransactionRollback();
                    _ToursDAL.ConnectionClose();
                }
            }
            catch (Exception ex)
            {
                result = -1;
                error = ex.Message.ToString();
                _ToursDAL.TransactionRollback();
                _ToursDAL.ConnectionClose();
            }
            return result;
        }
        public List<ST_SATIS_RESULT> getCustermerFeedData(string channel, string company, string userDefPro, string enqId)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.getCustermerFeedData(channel, company, userDefPro, enqId);
        }
        public DataTable GetReceipt(string _doc)
        {   // Nadeeka
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.GetReceipt(_doc);
        }
        public List<MST_FAC_LOC> getFacLocations(string company, string pc)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.getFacLocations(company, pc);
        }
        //Nuwan 2016-05-30
        public Int32 SaveToursrInvoiceTransport(InvoiceHeader _invoiceHeader, List<InvoiceItem> _invoiceItem, List<InvoiceSerial> _invoiceSerial, RecieptHeader _recieptHeader, List<RecieptItem> _recieptItem, InventoryHeader _inventoryHeader, List<ReptPickSerials> _pickSerial, List<ReptPickSerialsSub> _pickSubSerial, MasterAutoNumber _invoiceAuto, MasterAutoNumber _recieptAuto, MasterAutoNumber _inventoryAuto, bool _isDeliveryNow, out  string _invoiceNo, out string _receiptNo, out string _deliveryOrder, MasterBusinessEntity _businessCompany, bool _isHold, bool _isHoldInvoiceProcess, out string _errorlist, List<InvoiceVoucher> _voucher, InventoryHeader _buybackheader, MasterAutoNumber _buybackauto, List<ReptPickSerials> _buybacklist, out string BuyBackInvNo, bool isTranInvoice, GEN_CUST_ENQSER _genCustEnqser = null, GEN_CUST_ENQ oItem = null, MasterAutoNumber _enqAuto = null, List<MST_ST_PAX_DET> paxDetList = null, bool Tours = true, MasterBusinessEntity _custProfile = null, GroupBussinessEntity _custGroup = null, List<ST_ENQ_CHARGES> enqChrgItems = null, List<GEN_CUS_ENQ_DRIVER> driverList = null, List<GEN_CUS_ENQ_FLEET> vehicleList = null)
        {
            string _invNo = string.Empty;
            string _recNo = string.Empty;
            string _DONo = string.Empty;
            string _buybackinv = string.Empty;
            Int32 _effect = 0;
            string _location = string.Empty;
            string _error = string.Empty;
            string _db = string.Empty;
            MasterAutoNumber _auto = null;
            bool _VoucherPromotion = false;
            Boolean _isNew = false;
            string enqMsg = string.Empty;
            try
            {
                _db = DataBase._ems;
                _salesDAL = new SalesDAL();
                _salesDAL.ConnectionOpen();
                _salesDAL.BeginTransaction();
                _db = DataBase._ems;
                _inventoryDAL = new InventoryDAL();
                _inventoryDAL.ConnectionOpen();
                _inventoryDAL.BeginTransaction();
                _db = DataBase._fms;
                _fmsInventoryDal = new FMS_InventoryDAL();
                _fmsInventoryDal.ConnectionOpen();
                _fmsInventoryDal.BeginTransaction();
                _db = DataBase._reportdb;
                _inventoryRepDAL = new ReptCommonDAL();
                _inventoryRepDAL.ConnectionOpen();
                _inventoryRepDAL.BeginTransaction();
                _db = DataBase._ems;
                _generalDAL = new GeneralDAL();
                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();
                _db = DataBase._ems;
                _ToursDAL = new ToursDAL();
                _ToursDAL.ConnectionOpen();
                _ToursDAL.BeginTransaction();


                int effect = 0;
                if (_custProfile.Mbe_name != null && _custGroup.Mbg_name != null && oItem != null)
                {
                    string customerCD = (oItem.GCE_CUS_CD != null) ? oItem.GCE_CUS_CD : "";
                    GroupBussinessEntity cus = _salesDAL.GetCustomerProfileByGrup(oItem.GCE_CUS_CD, null, null, null, null, null);
                    if (customerCD != "")
                    {
                        if (cus.Mbg_cd == null)
                        {
                            effect = _salesDAL.SaveBusinessEntityDetailGroup(_custGroup);
                            effect = _salesDAL.UpdateBusinessEntityProfile(_custProfile, 1);
                        }
                    }
                    else
                    {
                        MasterAutoNumber _cusauto = new MasterAutoNumber();
                        //_auto.Aut_cate_cd = _businessEntity.Mbe_cre_pc;//_invoiceHeader.Sah_pc;
                        //_auto.Aut_cate_tp = "PRO";
                        _cusauto.Aut_moduleid = "CUS";
                        _cusauto.Aut_number = 0;
                        _cusauto.Aut_start_char = "CONT";

                        MasterAutoNumber _number = _inventoryDAL.GetAutoNumber(_cusauto.Aut_moduleid, _cusauto.Aut_direction, _cusauto.Aut_start_char, _cusauto.Aut_cate_tp, _cusauto.Aut_cate_cd, _cusauto.Aut_modify_dt, _cusauto.Aut_year);
                        // MasterAutoNumber _number = _inventoryDAL.GetAutoNumber(_auto.Aut_moduleid, _auto.Aut_direction, _auto.Aut_start_char, _auto.Aut_cate_tp, null, _auto.Aut_modify_dt, _auto.Aut_year);
                        string _cusNo = _cusauto.Aut_start_char + "-" + _number.Aut_number.ToString("000000", CultureInfo.InvariantCulture);
                        _inventoryDAL.UpdateAutoNumber(_cusauto);
                        _custProfile.Mbe_cd = _cusNo;
                        _custGroup.Mbg_cd = _cusNo;

                        customerCD = _cusNo;
                        effect = _salesDAL.SaveBusinessEntityDetailGroup(_custGroup);
                        effect = _salesDAL.SaveBusinessEntityDetail(_custProfile);

                        oItem.GCE_CUS_CD = customerCD;

                        if (_invoiceHeader != null)
                        {
                            _invoiceHeader.Sah_cus_add1 = _custProfile.Mbe_add1;
                            _invoiceHeader.Sah_cus_add2 = _custProfile.Mbe_add2;
                            _invoiceHeader.Sah_cus_cd = customerCD;
                            _invoiceHeader.Sah_cus_name = _custProfile.Mbe_name;
                        }
                        if (_recieptHeader != null)
                        {
                            _recieptHeader.Sar_debtor_cd = customerCD;
                            _recieptHeader.Sar_debtor_name = _custProfile.Mbe_name;
                            _recieptHeader.Sar_debtor_add_1 = _custProfile.Mbe_add1;
                            _recieptHeader.Sar_debtor_add_2 = _custProfile.Mbe_add2;
                        }
                    }

                }
                if (oItem != null)
                {
                    if (String.IsNullOrEmpty(oItem.GCE_ENQ_ID) && oItem.GCE_SEQ == 0)
                    {
                        MasterAutoNumber _reversInv = _inventoryDAL.GetAutoNumber(_enqAuto.Aut_moduleid, _enqAuto.Aut_direction, _enqAuto.Aut_start_char, _enqAuto.Aut_cate_tp, _enqAuto.Aut_cate_cd, _enqAuto.Aut_modify_dt, _enqAuto.Aut_year);
                        _reversInv.Aut_direction = null;
                        _reversInv.Aut_modify_dt = null;
                        string enqno = "";
                        if (oItem.GCE_COM == "ATS")
                        {
                            enqno = "ATL" + "/" + _reversInv.Aut_cate_cd + "/" + _reversInv.Aut_number.ToString("00000", CultureInfo.InvariantCulture) + "/" + oItem.GCE_FRM_TN + "/" + Convert.ToString(DateTime.Now.Date.Month) + "/" + Convert.ToString(DateTime.Now.Date.Year).Remove(0, 2);
                        }
                        else
                        {
                            enqno = oItem.GCE_COM + "/" + _reversInv.Aut_cate_cd + "/" + _reversInv.Aut_number.ToString("00000", CultureInfo.InvariantCulture) + "/" + oItem.GCE_FRM_TN + "/" + Convert.ToString(DateTime.Now.Date.Month) + "/" + Convert.ToString(DateTime.Now.Date.Year).Remove(0, 2);
                        }
                        _inventoryDAL.UpdateAutoNumber(_reversInv);
                        oItem.GCE_ENQ_ID = enqno;
                        _isNew = true;
                    }

                    _effect = _ToursDAL.Save_GEN_CUST_ENQ(oItem);

                    if (_isNew == true)
                    {
                        enqMsg = "Successfully Created , Enquiry ID  " + oItem.GCE_ENQ_ID;
                    }
                    else
                    {
                        enqMsg = "Successfully Updated , Enquiry ID  " + oItem.GCE_ENQ_ID;
                    }
                    if (_invoiceHeader != null)
                        _invoiceHeader.Sah_ref_doc = oItem.GCE_ENQ_ID;

                    if (driverList != null && driverList.Count > 0)
                    {
                        foreach (GEN_CUS_ENQ_DRIVER driver in driverList)
                        {
                            driver.GCD_ENQ_NO = oItem.GCE_ENQ_ID;
                            _effect = _ToursDAL.Save_GEN_CUST_ENQ_DRIVER(driver);
                        }
                    }
                    if (vehicleList != null && vehicleList.Count > 0)
                    {
                        foreach (GEN_CUS_ENQ_FLEET fleet in vehicleList)
                        {
                            fleet.GCF_ENQ_NO = oItem.GCE_ENQ_ID;
                            _effect = _ToursDAL.Save_GEN_CUST_ENQ_FLEET(fleet);
                        }
                    }
                }

                if (!_isHold && _invoiceHeader != null)
                {
                    _invoiceAuto.Aut_year = null;
                    MasterAutoNumber InvoiceAuto = _inventoryDAL.GetAutoNumber(_invoiceAuto.Aut_moduleid, _invoiceAuto.Aut_direction, _invoiceAuto.Aut_start_char, _invoiceAuto.Aut_cate_tp, _invoiceAuto.Aut_cate_cd, _invoiceAuto.Aut_modify_dt, _invoiceAuto.Aut_year);
                    _invNo = _invoiceAuto.Aut_start_char + InvoiceAuto.Aut_number.ToString("00000", CultureInfo.InvariantCulture);
                    _invoiceAuto.Aut_year = null;
                    _invoiceAuto.Aut_modify_dt = null;
                    _salesDAL.UpdateInvoiceAutoNumber(_invoiceAuto);
                    _invoiceHeader.Sah_inv_no = _invNo;
                }

                _db = string.Empty;
                _location = (_inventoryHeader != null && !string.IsNullOrEmpty(_inventoryHeader.Ith_com)) ? _inventoryHeader.Ith_loc : string.Empty;
                string _recieptSeq = null;
                string _invoiceSeq = null;
                InventoryHeader _invHdr = null;
                DataTable _dataTable = null;
                string invNo = (_invNo != "") ? _invNo : "";

                if (_invoiceHeader != null || _recieptHeader != null)
                {
                    CommonSaveInvoiceWithDeliveryOrderWithTransactionTransport(_invoiceHeader, _invoiceItem, _invoiceSerial, _recieptHeader, _recieptItem, _inventoryHeader, _pickSerial, _pickSubSerial, _invoiceAuto, _recieptAuto, _inventoryAuto, _isDeliveryNow, out _invNo, out _recNo, out _DONo, _inventoryDAL, _salesDAL, _inventoryRepDAL, _isHold, _isHoldInvoiceProcess, out _error, false, out _invoiceSeq, out _recieptSeq, out _invHdr, out _dataTable, invNo);
                }
                if (_genCustEnqser != null)
                {
                    _genCustEnqser.GCS_SEQ = (_invoiceSeq != null) ? Convert.ToInt32(_invoiceSeq) : 0;
                    _genCustEnqser.GCS_ENQ_ID = oItem.GCE_ENQ_ID;
                    _ToursDAL.SAVE_GEN_ENQSER(_genCustEnqser);
                }
                Int32 result = 0;
                if (_invoiceHeader != null)
                {
                    result = _ToursDAL.UpdateEnquiryStage(5, _invoiceHeader.Sah_cre_by, _invoiceHeader.Sah_ref_doc, _invoiceHeader.Sah_com, _invoiceHeader.Sah_pc);
                }
                else
                {
                    if (oItem != null)
                        result = _ToursDAL.UpdateEnquiryStage(5, oItem.GCE_CRE_BY, oItem.GCE_ENQ_ID, oItem.GCE_COM, oItem.GCE_ENQ_PC);
                }

                GEN_ENQLOG oLog = new GEN_ENQLOG();
                oLog.GEL_ENQ_ID = (_invoiceHeader != null) ? _invoiceHeader.Sah_ref_doc : (oItem != null) ? oItem.GCE_ENQ_ID : "";
                oLog.GEL_USR = (_invoiceHeader != null) ? _invoiceHeader.Sah_cre_by : (oItem != null) ? oItem.GCE_CRE_BY : "";
                oLog.GEL_STAGE = 5;
                oLog.GEL_LOGWHEN = DateTime.Now;
                result = _ToursDAL.SAVE_GEN_ENQLOG(oLog);

                //2015-06-26
                if (isTranInvoice && _invoiceHeader != null)
                {
                    foreach (InvoiceItem item in _invoiceItem)
                    {
                        result = _ToursDAL.UPDATE_LOG_HDR_INVOICE(Convert.ToInt32(item.Sad_promo_cd), 1, _invoiceHeader.Sah_cre_by);
                    }
                }
                if (enqChrgItems != null && enqChrgItems.Count > 0)
                {
                    result = _ToursDAL.deleteEnquiryCharges(enqChrgItems[0].SCH_ENQ_NO);
                    foreach (ST_ENQ_CHARGES enqChg in enqChrgItems)
                    {
                        if (invNo != "" && enqChg.SCH_INVOICED == 0)
                        {
                            enqChg.SCH_INVOICED_NO = invNo;
                            enqChg.SCH_INVOICED = 1;
                        }
                        else
                        {
                            enqChg.SCH_INVOICED_NO = enqChg.SCH_INVOICED_NO;
                            enqChg.SCH_INVOICED = enqChg.SCH_INVOICED;
                        }
                        enqChg.SCH_ENQ_NO = oItem.GCE_ENQ_ID;
                        result = _ToursDAL.saveEnquiryCharges(enqChg);
                    }

                }
                string _customerCode = (_invoiceHeader != null) ? _invoiceHeader.Sah_cus_cd : (oItem != null) ? oItem.GCE_CUS_CD : "";
                GroupBussinessEntity _businessEntityGrup = new GroupBussinessEntity();
                if (string.IsNullOrEmpty(_error) || oItem != null || oItem.GCE_ENQ_ID != "")
                {
                    #region Customer Creation

                    #endregion Customer Creation

                    #region update auto no

                    if (!_isHold)
                    {
                        if (_recieptHeader != null)
                        {
                            if (_recieptAuto != null)
                            {
                                //nxt2:
                                _recieptAuto.Aut_year = null;
                                MasterAutoNumber RecieptAuto = _inventoryDAL.GetAutoNumber(_recieptAuto.Aut_moduleid, _recieptAuto.Aut_direction, _recieptAuto.Aut_start_char, _recieptAuto.Aut_cate_tp, _recieptAuto.Aut_cate_cd, _recieptAuto.Aut_modify_dt, _recieptAuto.Aut_year);
                                _receiptNo = _recieptAuto.Aut_cate_cd + "-" + RecieptAuto.Aut_start_char + RecieptAuto.Aut_number.ToString("00000", CultureInfo.InvariantCulture);
                                _recieptAuto.Aut_year = null;
                                _recieptAuto.Aut_modify_dt = null;
                                _recNo = _receiptNo;
                                if (_ToursDAL.CheckTBSSalesNo("SP_GETTBSRECEIPTNO", "p_rept_no", _recNo) == 1)
                                {
                                    // _salesDAL.UpdateInvoiceAutoNumber(_recieptAuto);
                                    // goto nxt2;
                                    _error = "Invoice process terminated. Please re-process.(Hint - Duplicating Receipt No)";
                                    _invoiceNo = string.Empty;
                                    _receiptNo = string.Empty;
                                    _deliveryOrder = string.Empty;
                                    _errorlist = _error;
                                    BuyBackInvNo = string.Empty;
                                    _effect = -1;

                                    _salesDAL.TransactionRollback();
                                    _inventoryDAL.TransactionRollback();
                                    _fmsInventoryDal.TransactionRollback();
                                    _inventoryRepDAL.TransactionRollback();
                                    _generalDAL.TransactionRollback();
                                    _ToursDAL.TransactionRollback();
                                    return _effect;
                                }
                                //_invDAL.UpdateAutoNumber(_recieptAuto);

                                _salesDAL.UpdateInvoiceAutoNumber(_recieptAuto);
                            }

                            if (Tours == true)
                            {
                                _salesDAL.UpdateReceiptTbs(_invNo, _recNo, Convert.ToInt32(_invoiceSeq), Convert.ToInt32(_recieptSeq));
                            }
                            else
                            {
                                _salesDAL.UpdateReceipt(_invNo, _recNo, Convert.ToInt32(_invoiceSeq), Convert.ToInt32(_recieptSeq));
                            }

                            //Update receipt no which allocated by receipt entry as per invoice
                            _inventoryRepDAL.UpdateAdvanceReceiptNofromInvoice(string.Empty, Convert.ToString(_invoiceSeq), _invNo);
                        }

                    }

                    #endregion update auto no

                    #region Update Manual Doc
                    if (_invoiceHeader != null)
                        if (_invoiceHeader.Sah_manual) _fmsInventoryDal.UpdateManualDocNo(_location, "MDOC_INV", Convert.ToInt32(_invoiceHeader.Sah_ref_doc), _invNo);

                    #endregion Update Manual Doc

                    #region update invoice discount / Promotion Voucher page as F
                    if (_invoiceHeader != null)
                    {
                        foreach (InvoiceItem _itm in _invoiceItem)
                        {
                            if (_itm.Sad_dis_type == "P")
                            {
                                _salesDAL.UpdateDiscountUsedTimes(_itm.Sad_dis_seq, 1);
                            }

                            if (_itm.Sad_res_no == "PROMO_VOU" && _itm.Sad_res_line_no > 0)
                            {
                                //Add by Chamal 6-Jul-2014
                                _salesDAL.Update_GV_Pages(1, _invoiceHeader.Sah_com, _invoiceHeader.Sah_pc, _invoiceHeader.Sah_dt.Date, "", "F", _itm.Sad_res_line_no, "P_GV", _invoiceHeader.Sah_cre_by, _invNo);
                            }
                        }


                    #endregion update invoice discount / Promotion Voucher page as F

                        if (paxDetList != null)
                        {
                            Int32 ln = 1;
                            foreach (MST_ST_PAX_DET pax in paxDetList)
                            {
                                pax.SPD_SEQ = Convert.ToInt32(_invoiceSeq);
                                pax.SPD_LINE = ln;
                                pax.SPD_INV_NO = _invNo;
                                pax.SPD_ENQ_ID = _invoiceHeader.Sah_ref_doc;
                                Int32 ef = _ToursDAL.saveInvoicePax(pax);
                                ln++;
                            }
                        }
                    }
                    _error = enqMsg + _error;
                    _error = _error.Trim();
                    _effect = 1;
                }
                else
                    _effect = -1;

                try
                {
                    // _db = DataBase._ems; _salesDAL.ConnectionClose(); _db = DataBase._ems; _inventoryDAL.ConnectionClose(); _db = DataBase._fms; _fmsInventoryDal.ConnectionClose(); _db = DataBase._reportdb; _inventoryRepDAL.ConnectionClose(); _db = DataBase._ems; _generalDAL.ConnectionClose();
                    if (string.IsNullOrEmpty(_error) || oItem != null)
                    {
                        _db = DataBase._ems;
                        _salesDAL.TransactionCommit();
                        _db = DataBase._ems;
                        _inventoryDAL.TransactionCommit();
                        _db = DataBase._fms;
                        _fmsInventoryDal.TransactionCommit();
                        _db = DataBase._reportdb;
                        _inventoryRepDAL.TransactionCommit();
                        _db = DataBase._ems;
                        _generalDAL.TransactionCommit();
                        _db = DataBase._ems;
                        _ToursDAL.TransactionCommit();

                        if (_invNo != "")
                            _inventoryDAL.UpdateInvoiceDOStatus(_invNo);

                        //cus code update
                        if (_auto != null)
                        {
                            _inventoryDAL.UpdateAutoNumber(_auto);
                        }
                    }
                    else
                    {
                        _salesDAL.TransactionRollback();
                        _inventoryDAL.TransactionRollback();
                        _fmsInventoryDal.TransactionRollback();
                        _inventoryRepDAL.TransactionRollback();
                        _generalDAL.TransactionRollback();
                        _ToursDAL.TransactionRollback();
                    }
                }
                catch (Exception ex)
                {
                    _invoiceNo = string.Empty;
                    _receiptNo = string.Empty;
                    _deliveryOrder = string.Empty;
                    _errorlist = "Database" + _db + " is not responding. Please contact IT Operation.\n" + ex.Message;
                    BuyBackInvNo = string.Empty;
                    _effect = -1;
                    return _effect;
                }
            }
            catch (Exception ex)
            {
                _error = ex.Message.ToString();
                _invoiceNo = string.Empty;
                _receiptNo = string.Empty;
                _deliveryOrder = string.Empty;
                _errorlist = _error;
                BuyBackInvNo = string.Empty;
                _effect = -1;

                _salesDAL.TransactionRollback();
                _inventoryDAL.TransactionRollback();
                _fmsInventoryDal.TransactionRollback();
                _inventoryRepDAL.TransactionRollback();
                _generalDAL.TransactionRollback();
                _ToursDAL.TransactionRollback();
            }

            _invoiceNo = _invNo;
            _receiptNo = _recNo;
            _deliveryOrder = _DONo;
            _errorlist = _error;
            BuyBackInvNo = _buybackinv;
            return _effect;
        }
        //Nuwan 2016.05.30
        public void CommonSaveInvoiceWithDeliveryOrderWithTransactionTransport(InvoiceHeader _invoiceHeader, List<InvoiceItem> _invoiceItem, List<InvoiceSerial> _invoiceSerial, RecieptHeader _recieptHeader, List<RecieptItem> _recieptItem, InventoryHeader _inventoryHeader, List<ReptPickSerials> _pickSerial, List<ReptPickSerialsSub> _pickSubSerial, MasterAutoNumber _invoiceAuto, MasterAutoNumber _recieptAuto, MasterAutoNumber _inventoryAuto, bool _isDeliveryNow, out  string _invoiceNo, out string _receiptNo, out string _deliveryOrder, InventoryDAL _invDAL, SalesDAL _salDAL, ReptCommonDAL _invRepDAL, bool _isHold, bool _isHoldInvoiceProcess, out string _errorlist, bool _ishireSale, out string _invSeq, out string _recieptSeq, out InventoryHeader _mov, out DataTable _datatable, string InvNo)
        {
            string _invNo = "";
            string _recNo = "";
            string _doNo = "";

            List<ReptPickSerials> _remakeReptSerialList = new List<ReptPickSerials>();
            string _error = string.Empty;


            string InvoiceNo = _invoiceHeader.Sah_inv_no;
            string RecieptNo = string.Empty;
            try
            {
                Int32 _invoiceLine = 1;
                DataTable _tbl = _salDAL.GetEmployee(_invoiceHeader.Sah_com, _invoiceHeader.Sah_sales_ex_cd);
                string _executiveType = string.Empty;
                foreach (DataRow _r in _tbl.Rows)
                {
                    _executiveType = Convert.ToString(_r["esep_cat_cd"]);
                }
                List<SaleCommission> _saveCommission = new List<SaleCommission>();

                #region Delete invoice if its a HOLD status

                //if (_isHold || _isHoldInvoiceProcess) _salDAL.DeleteInvoiceDetailForHold(_invoiceHeader.Sah_seq_no);

                #endregion Delete invoice if its a HOLD status

                //Generate SeqNo
                //If hold, then client should generate the sequence and assign to header,ie; if its recall hold invoice and need to second time hold, could set sequence no
                //as per the recalled sequence no
                Int32 InvoiceSeqNo = _isHoldInvoiceProcess ? _invoiceHeader.Sah_seq_no : _ToursDAL.GETINVOSEQ();
                Int32 RecieptSeqNo = (_recieptHeader != null) ? !string.IsNullOrEmpty(_recieptHeader.Sar_receipt_type) ? _ToursDAL.GETRECIPTSEQ() : -1 : -1;
                Int32 InventorySeqNo = 0;

                //if (_isDeliveryNow) InventorySeqNo = _invDAL.GetSerialID();

                //-------------------------------------------------------------------------- Invoice------------------------------------------------------------------------

                _invoiceHeader.Sah_seq_no = InvoiceSeqNo;
                //_invoiceHeader.Sah_inv_no = Convert.ToString(InvoiceSeqNo);

                #region Calculation for total of the payment to infiltrate invoice header

                decimal _totalValue = _invoiceItem.Sum(x => x.Sad_tot_amt);
                decimal _totalReceiptAmt = 0;
                if (_recieptItem != null) if (_recieptItem.Count > 0) _totalReceiptAmt = _recieptItem.Sum(x => x.Sard_settle_amt);

                _invoiceHeader.Sah_anal_7 = _totalValue;// -_totalReceiptAmt; //Total Invoice Amount - Total Receipt AmountBY DARSHANA 3/12/2012
                _invoiceHeader.Sah_anal_8 = _totalReceiptAmt;//Receipt Amount

                //ADDED SACHITH 2013/12/04
                //CREDIT SALES
                //FOR SVAT CUSTOMERS ADD TAX VALUE TO ANAL_8
                if (_invoiceHeader.Sah_is_svat && _invoiceHeader.Sah_inv_tp == "CRED")
                {
                    decimal vatTotal = _invoiceItem.Sum(X => X.Sad_itm_tax_amt);
                    _invoiceHeader.Sah_anal_8 = _invoiceHeader.Sah_anal_8 + vatTotal;
                }

                if (_invoiceSerial != null)
                    if (_invoiceSerial.Count > 0)
                    {
                        //_invoiceSerial.ForEach(X => X.Sap_inv_no = Convert.ToString(InvoiceSeqNo));
                        _invoiceSerial.ForEach(X => X.Sap_inv_no = _invoiceHeader.Sah_inv_no);
                        _invoiceSerial.ForEach(x => x.Sap_seq_no = InvoiceSeqNo);
                    }

                DataTable _tblESDEPFWHF = new DataTable();
                _tblESDEPFWHF = _salDAL.Get_ESD_EPF_WHT(_invoiceHeader.Sah_com, _invoiceHeader.Sah_pc, _invoiceHeader.Sah_dt);
                Decimal ESD_rt = 0; Decimal EPF_rt = 0; Decimal WHT_rt = 0;
                if (_tblESDEPFWHF.Rows.Count > 0) { ESD_rt = Convert.ToDecimal(_tblESDEPFWHF.Rows[0]["MPCH_ESD"]); EPF_rt = Convert.ToDecimal(_tblESDEPFWHF.Rows[0]["MPCH_EPF"]); WHT_rt = Convert.ToDecimal(_tblESDEPFWHF.Rows[0]["MPCH_WHT"]); }
                _invoiceHeader.Sah_esd_rt = ESD_rt;
                _invoiceHeader.Sah_epf_rt = EPF_rt;
                _invoiceHeader.Sah_wht_rt = WHT_rt;
                if (_recieptHeader != null)
                {
                    _recieptHeader.Sar_esd_rate = ESD_rt;
                    _recieptHeader.Sar_epf_rate = EPF_rt;
                    _recieptHeader.Sar_wht_rate = WHT_rt;
                }
                #endregion Calculation for total of the payment to infiltrate invoice header

                //Save Invoice Header

                #region Save Invoice Header

                _ToursDAL.SaveInvoiceHeader(_invoiceHeader);

                #endregion Save Invoice Header

                //Save Invoice Items

                #region Invoice Item Detail

                foreach (InvoiceItem _itm in _invoiceItem)
                {

                    // MasterItem _item = _invDAL.GetItem(_invoiceHeader.Sah_com, _itm.Sad_itm_cd);
                    _itm.Sad_seq_no = InvoiceSeqNo;
                    _invoiceLine = _itm.Sad_itm_line;
                    //_itm.Sad_inv_no = Convert.ToString(InvoiceSeqNo);
                    _itm.Sad_inv_no = _invoiceHeader.Sah_inv_no;
                    _itm.Sad_unit_amt = _itm.Sad_unit_rt * _itm.Sad_qty;
                    //_itm.Sad_itm_tp = _item.Mi_itm_tp;
                    //_itm.Sad_uom = _item.Mi_itm_uom;
                    _itm.Sad_trd_svc_chrg = Math.Round((_itm.Sad_tot_amt - _itm.Sad_itm_tax_amt) / _itm.Sad_qty, 2);
                    if (_invoiceHeader.Sah_tax_exempted)
                    {
                        _itm.Sad_tot_amt = _itm.Sad_tot_amt - _itm.Sad_itm_tax_amt;
                        _itm.Sad_itm_tax_amt = 0;
                    }


                    _itm.Sad_job_no = (_invoiceHeader.Sah_ref_doc != null) ? _invoiceHeader.Sah_ref_doc : InvNo;
                    _ToursDAL.SaveInvoiceItem(_itm);
                    _ToursDAL.UpdateTBSPrice(_itm.Sad_itm_cd, _itm.Sad_pbook, _itm.Sad_pb_lvl, _invoiceHeader.Sah_cus_cd, _itm.Sad_promo_cd, _itm.Sad_seq, _itm.Sad_itm_seq);

                    if (_invoiceHeader.Sah_dt == DateTime.Now.Date)
                    {
                        List<MasterItemTax> _itmTax = new List<MasterItemTax>();
                        _itmTax = _salDAL.GetItemTax(_invoiceHeader.Sah_com, _itm.Sad_itm_cd, _itm.Sad_itm_stus, string.Empty, string.Empty);

                        foreach (MasterItemTax _one in _itmTax)
                        {
                            InvoiceItemTax _tax = new InvoiceItemTax();
                            _tax.Satx_inv_no = _itm.Sad_inv_no;
                            _tax.Satx_itm_cd = _itm.Sad_itm_cd;
                            _tax.Satx_itm_line = _itm.Sad_itm_line;
                            _tax.Satx_itm_tax_amt = _invoiceHeader.Sah_tax_exempted ? 0 : _itm.Sad_itm_tax_amt;// ((_itm.Sad_unit_rt - _itm.Sad_disc_amt / _itm.Sad_qty) * _one.Mict_tax_rate / 100) * _itm.Sad_qty;
                            _tax.Satx_itm_tax_rt = _one.Mict_tax_rate;
                            _tax.Satx_itm_tax_tp = _one.Mict_tax_cd;
                            _tax.Satx_job_line = 0;
                            _tax.Satx_job_no = "";
                            _tax.Satx_seq_no = InvoiceSeqNo;
                            _ToursDAL.SaveTBSSalesItemTax(_tax);
                        }
                    }
                    else
                    {
                        List<MasterItemTax> _itmTaxEff = new List<MasterItemTax>();
                        _itmTaxEff = _salDAL.GetItemTaxEffDt(_invoiceHeader.Sah_com, _itm.Sad_itm_cd, _itm.Sad_itm_stus, string.Empty, string.Empty, _invoiceHeader.Sah_dt);

                        if (_itmTaxEff.Count > 0)
                        {
                            foreach (MasterItemTax _one in _itmTaxEff)
                            {
                                InvoiceItemTax _tax = new InvoiceItemTax();
                                _tax.Satx_inv_no = _itm.Sad_inv_no;
                                _tax.Satx_itm_cd = _itm.Sad_itm_cd;
                                _tax.Satx_itm_line = _itm.Sad_itm_line;
                                _tax.Satx_itm_tax_amt = _invoiceHeader.Sah_tax_exempted ? 0 : _itm.Sad_itm_tax_amt;// ((_itm.Sad_unit_rt - _itm.Sad_disc_amt / _itm.Sad_qty) * _one.Mict_tax_rate / 100) * _itm.Sad_qty;
                                _tax.Satx_itm_tax_rt = _one.Mict_tax_rate;
                                _tax.Satx_itm_tax_tp = _one.Mict_tax_cd;
                                _tax.Satx_job_line = 0;
                                _tax.Satx_job_no = "";
                                _tax.Satx_seq_no = InvoiceSeqNo;
                                _ToursDAL.SaveTBSSalesItemTax(_tax);
                            }
                        }
                        else
                        {
                            List<LogMasterItemTax> _itmTax = new List<LogMasterItemTax>();
                            _itmTax = _salDAL.GetItemTaxLog(_invoiceHeader.Sah_com, _itm.Sad_itm_cd, _itm.Sad_itm_stus, string.Empty, string.Empty, _invoiceHeader.Sah_dt);

                            foreach (LogMasterItemTax _one in _itmTax)
                            {
                                InvoiceItemTax _tax = new InvoiceItemTax();
                                _tax.Satx_inv_no = _itm.Sad_inv_no;
                                _tax.Satx_itm_cd = _itm.Sad_itm_cd;
                                _tax.Satx_itm_line = _itm.Sad_itm_line;
                                _tax.Satx_itm_tax_amt = _invoiceHeader.Sah_tax_exempted ? 0 : _itm.Sad_itm_tax_amt;// ((_itm.Sad_unit_rt - _itm.Sad_disc_amt / _itm.Sad_qty) * _one.Mict_tax_rate / 100) * _itm.Sad_qty;
                                _tax.Satx_itm_tax_rt = _one.Lict_tax_rate;
                                _tax.Satx_itm_tax_tp = _one.Lict_tax_cd;
                                _tax.Satx_job_line = 0;
                                _tax.Satx_job_no = "";
                                _tax.Satx_seq_no = InvoiceSeqNo;
                                _ToursDAL.SaveTBSSalesItemTax(_tax);
                            }
                        }
                    }

                    #region cmt



                    #endregion cmt
                }

                //Update Discount Definition
                var _discountseq = _invoiceItem.Where(x => x.Sad_dis_type == "M").Select(x => x.Sad_dis_seq).Distinct().ToList();
                if (_discountseq != null && _discountseq.Count > 0)
                {
                    foreach (var _i in _discountseq)
                    {
                        int _seqno = Convert.ToInt32(_i);
                        _salDAL.UpdateGeneralDiscount("M", _seqno, 0);
                    }
                }

                #endregion Invoice Item Detail

                #region Commission Part - Commented :)

                #endregion Commission Part - Commented :)

                //Save Invoice Serials

                #region Invoice Serial


                #endregion Invoice Serial

                //-------------------------------------------------------------------------- Payment

                #region Payment


                if (_recieptHeader != null && _recieptItem != null && _recieptItem.Count > 0)
                {
                    string _shortcompany = _generalDAL.GetCompByCode(_invoiceHeader.Sah_com).Mc_anal5;
                    _recieptHeader.Sar_seq_no = RecieptSeqNo;
                    _recieptHeader.Sar_receipt_no = Convert.ToString(RecieptSeqNo);
                    _recieptHeader.Sar_tot_settle_amt = _totalReceiptAmt;
                    _ToursDAL.SaveTBSReceiptHeader(_recieptHeader);

                    if (_recieptItem != null)
                        if (_recieptItem.Count > 0)
                        {
                            foreach (RecieptItem _itm in _recieptItem)
                            {

                                _itm.Sard_seq_no = RecieptSeqNo;
                                _itm.Sard_inv_no = _invoiceHeader.Sah_inv_no;
                                _itm.Sard_receipt_no = Convert.ToString(RecieptSeqNo);
                                _ToursDAL.SaveTBSReceiptItem(_itm);

                                if (_itm.Sard_pay_tp.Trim() == "ADVAN")
                                {
                                    _invRepDAL.UpdateAdvanceReceiptNofromInvoice(_itm.Sard_ref_no, Convert.ToString(InvoiceSeqNo), string.Empty);
                                    RecieptHeader _rHdr = new RecieptHeader();
                                    _rHdr.Sar_receipt_no = _itm.Sard_ref_no;
                                    _rHdr.Sar_used_amt = _itm.Sard_settle_amt;
                                    _rHdr.Sar_act = true;
                                    _rHdr.Sar_direct = true;
                                    _rHdr.Sar_mod_by = _recieptHeader.Sar_mod_by;
                                    _ToursDAL.SaveTBSReceiptHeader(_rHdr);
                                }
                                if (_itm.Sard_pay_tp.Trim() == "CRNOTE")
                                {
                                    _ToursDAL.UpdateTBSCreditNoteBalance(_recieptHeader.Sar_com_cd, _recieptHeader.Sar_profit_center_cd, _itm.Sard_ref_no, _itm.Sard_settle_amt);
                                    MasterBusinessEntity _entity = _ToursDAL.GetBusinessCompanyDetail(_invoiceHeader.Sah_com, _invoiceHeader.Sah_cus_cd, null, null, "C");
                                    MasterProfitCenter _profit = _salDAL.GetProfitCenter(_invoiceHeader.Sah_com, _invoiceHeader.Sah_pc);

                                }


                            }
                        }
                }

                #endregion Payment

                #region Customer Account Maintain

                if (_invoiceHeader.Sah_cus_cd != "CASH")
                {
                    decimal _invoiceTotal = 0;
                    decimal _paidTotal = 0;

                    if (_invoiceItem != null)
                        if (_invoiceItem.Count > 0)
                        {
                            //Updating Account Balance
                            var _grandTotal = (from _total in _invoiceItem
                                               select _total.Sad_tot_amt).Sum();
                            _invoiceTotal = _grandTotal;
                        }

                    if (_recieptItem != null)
                        if (_recieptItem.Count > 0)
                        {
                            var _payTotal = (from _pay in _recieptItem
                                             select _pay.Sard_settle_amt).Sum();
                            _paidTotal = _payTotal;
                        }

                    CustomerAccountRef _account = new CustomerAccountRef();
                    _account.Saca_acc_bal = _invoiceTotal - _paidTotal;
                    _account.Saca_com_cd = _invoiceHeader.Sah_com;
                    _account.Saca_crdt_lmt = 0;
                    _account.Saca_cre_by = _invoiceHeader.Sah_cre_by;
                    _account.Saca_cre_when = _invoiceHeader.Sah_cre_when;
                    _account.Saca_cust_cd = _invoiceHeader.Sah_cus_cd;
                    _account.Saca_mod_by = _invoiceHeader.Sah_cre_by;
                    _account.Saca_mod_when = _invoiceHeader.Sah_cre_when;
                    _account.Saca_ord_bal = 0;
                    _account.Saca_session_id = _invoiceHeader.Sah_session_id;

                    //
                    // _salDAL.SaveCustomerAccount(_account);
                }

                #endregion Customer Account Maintain

                if (!string.IsNullOrEmpty(_invoiceHeader.Sah_anal_6))
                {

                }

                InventoryHeader _invHdr = null;
                DataTable _dataTable = null; ;

                #region Invoice Auto Number/Delivery Order

                if (!_isHold)
                {

                }
                else
                {
                    InvoiceNo = Convert.ToString(InvoiceSeqNo);
                }

                _invSeq = InvoiceSeqNo.ToString();
                _recieptSeq = RecieptSeqNo.ToString();
                _mov = _invHdr;
                _datatable = _dataTable;

                #endregion Invoice Auto Number/Delivery Order
            }
            catch (Exception ex)
            {
                _invSeq = "";
                _recieptSeq = "";
                _mov = null;
                _datatable = null;
                _error += "Generated error " + ex.Message;
                if (_error.Contains("UK_SAHINVNO") || _error.Contains("ORA-00001"))
                {
                    _error = "Please try again in a few seconds.";
                }
            }

            _invNo = InvoiceNo;
            _recNo = RecieptNo;

            _invoiceNo = _invNo;
            _receiptNo = _recNo;
            _deliveryOrder = _doNo;
            _errorlist = _error;
        }

        public GEN_CUST_ENQ getEnquiryDetailsTours(string Com, string PC, string ENQID)
        {
            GEN_CUST_ENQ custEnq = new GEN_CUST_ENQ();
            ToursDAL _ToursDAL = new ToursDAL();
            custEnq = _ToursDAL.GET_CUST_ENQRY(Com, PC, ENQID);
            MST_EMPLOYEE_TBS oMST_EMPLOYEE_TBS = GetEmployeeByComPC(Com, PC, custEnq.GCE_DRIVER);
            if (oMST_EMPLOYEE_TBS != null && oMST_EMPLOYEE_TBS.MEMP_EPF != null)
            {
                GroupBussinessEntity be = new GroupBussinessEntity();
                be.Mbg_name = oMST_EMPLOYEE_TBS.MEMP_FIRST_NAME + " " + oMST_EMPLOYEE_TBS.MEMP_LAST_NAME;
                be.Mbg_contact = oMST_EMPLOYEE_TBS.MEMP_MOBI_NO;
                custEnq.DRIVER_DETAILS = be;
            }
            //List<InvoiceHeader> hed = _ToursDAL.getInvoiceHedDataByEnqId(ENQID);
            //List<InvoiceItem> items = new List<InvoiceItem>();
            //if (hed.Count > 0)
            //{
            //    items = saleGetInvoiceDetail(hed[0].Sah_seq_no);
            //}
            custEnq.ENQ_CHARGES = _ToursDAL.saleGetChargItemDetails(ENQID);
            return custEnq;
        }
        public List<ST_AIRTCKT_TYPS> getAirTicketTypes()
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.getAirTicketTypes();
        }
        public List<ST_VEHICLE_TP> getVehicleTypes()
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.getVehicleTypes();
        }
        public List<ST_PKG_TP> getpKGTypes()
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.getpKGTypes();
        }
        public List<RecieptHeaderTBS> getOtherPartyReceipts(string dateFrom, string dateTo, string OthCus, string Cus, string company, string pc)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.getOtherPartyReceipts(dateFrom, dateTo, OthCus, Cus, company, pc);
        }

        public Int32 saveOtherPartyPayments(RecieptHeaderTBS receiptHeaderTBS, List<RecieptHeaderTBS> existsReceiptItems, List<RecieptItemTBS> RecieptItemList, MasterAutoNumber _masterAutoNumber, out string docno)
        {
            Int32 _effects = 0;
            string _autoNumberRecType = "";
            string _documentNo = "";

            _salesDAL = new SalesDAL();
            ToursDAL toursdal = new ToursDAL();


            try
            {

                _inventoryDAL = new InventoryDAL(); _inventoryDAL.ConnectionOpen(); _inventoryDAL.BeginTransaction();
                _salesDAL = new SalesDAL(); _salesDAL.ConnectionOpen(); _salesDAL.BeginTransaction();
                _generalDAL = new GeneralDAL(); _generalDAL.ConnectionOpen(); _generalDAL.BeginTransaction();
                toursdal = new ToursDAL(); toursdal.ConnectionOpen(); toursdal.BeginTransaction();

                Int32 _autoNo = _inventoryDAL.GetAutoNumber(_masterAutoNumber.Aut_moduleid, _masterAutoNumber.Aut_direction, _masterAutoNumber.Aut_start_char, _masterAutoNumber.Aut_cate_tp, _masterAutoNumber.Aut_cate_cd, _masterAutoNumber.Aut_modify_dt, _masterAutoNumber.Aut_year).Aut_number;
                _documentNo = _masterAutoNumber.Aut_cate_cd + _masterAutoNumber.Aut_start_char + string.Format("{0:0000}", _autoNo);

                _autoNumberRecType = receiptHeaderTBS.Sir_manual_ref_no;
                _salesDAL.UpdateRecAutoNumberTBS(_documentNo, receiptHeaderTBS.Sir_seq_no, _autoNumberRecType);

                _inventoryDAL.UpdateAutoNumber(_masterAutoNumber);

                docno = _documentNo;

                receiptHeaderTBS.Sir_receipt_no = docno;

                _effects = _salesDAL.SaveReceiptHeaderTBS(receiptHeaderTBS);

                if (RecieptItemList != null)
                {
                    Int32 line = 1;
                    foreach (RecieptItemTBS _ReceiptDetails in RecieptItemList)
                    {
                        _ReceiptDetails.Sird_line_no = line;
                        _ReceiptDetails.Sird_receipt_no = docno;
                        _effects = _salesDAL.SaveReceiptItemTBS(_ReceiptDetails);
                        line++;
                    }
                }

                if (existsReceiptItems != null && existsReceiptItems.Count > 0)
                {
                    foreach (RecieptHeaderTBS hedDetails in existsReceiptItems)
                    {
                        _effects = toursdal.updateTBSReceiptHeaderDetails(hedDetails);
                    }
                }
                string _mobilNo = "";
                string _cusName = "";
                string _Newmg = "";
                string _recTp = "";
                decimal _recAmt = 0;
                _mobilNo = receiptHeaderTBS.Sir_mob_no;
                _cusName = receiptHeaderTBS.Sir_debtor_name;
                _recAmt = receiptHeaderTBS.Sir_tot_settle_amt;


                if (!string.IsNullOrEmpty(_mobilNo))
                {

                    if (_mobilNo.Length >= 9)
                    {
                        if (receiptHeaderTBS.Sir_receipt_type == "ADVAN")
                        {
                            _recTp = "Advance payment";
                        }
                        else
                        {
                            _recTp = "Third party payment";
                        }

                        if (!string.IsNullOrEmpty(_recTp))
                        {
                            if (_recAmt > 0)
                            {
                                string _pcName = "";
                                DataTable dt = _generalDAL.CheckProfitCenter(receiptHeaderTBS.Sir_com_cd, receiptHeaderTBS.Sir_profit_center_cd);
                                if (dt.Rows.Count > 0)
                                {
                                    _pcName = Convert.ToString(dt.Rows[0]["mpc_desc"]);
                                }

                                _Newmg = "Thank u for the payment of LKR " + _recAmt + " received to the " + _pcName + " for " + _recTp + ",R/N " + _documentNo;// "Thank u for the purchased items on HP A/C: " + _AccountNo + " @ " + _pcName + " S/R. HP value Rs.: " + _HPAccount.Hpa_hp_val + " and " + _smsInsu + " Rs.: " + _insuAmt + "-" + _generalDAL.GetHPCustContactPhoneNo();
                                OutSMS _out = new OutSMS();
                                _out.Msg = _Newmg;
                                _out.Msgstatus = 0;
                                _out.Msgtype = "S";
                                _out.Receivedtime = DateTime.Now;
                                _out.Receiver = "CUSTOMER";
                                //_out.Receiverphno = _info.Mmi_mobi_no;

                                if (_mobilNo.Length == 10)
                                {
                                    _out.Receiverphno = "+94" + _mobilNo.Substring(1, 9);
                                    _out.Senderphno = "+94" + _mobilNo.Substring(1, 9);
                                }
                                if (_mobilNo.Length == 9)
                                {
                                    _out.Receiverphno = "+94" + _mobilNo;
                                    _out.Senderphno = "+94" + _mobilNo;
                                }

                                _out.Refdocno = _documentNo;
                                _out.Sender = receiptHeaderTBS.Sir_create_by;
                                _out.Createtime = DateTime.Now;
                                _generalDAL.SaveSMSOut(_out);
                            }
                        }
                    }
                }
                _inventoryDAL.TransactionCommit();
                _salesDAL.TransactionCommit();
                _generalDAL.TransactionCommit();

                toursdal.TransactionCommit();



                _effects = 1;
            }
            catch (Exception err)
            {
                _effects = -1;
                docno = "ERROR : " + err.Message.ToString();
                _inventoryDAL.TransactionRollback();
                _salesDAL.TransactionRollback();
                _generalDAL.TransactionRollback();
                toursdal.TransactionRollback();

            }
            _inventoryDAL.ConnectionClose();
            _salesDAL.ConnectionClose();
            _generalDAL.ConnectionClose();
            toursdal.ConnectionClose();
            return _effects;
        }
        public List<ST_ENQ_CHARGES> tempEnquiryCharges(string ENQID)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.saleGetChargItemDetails(ENQID);
        }
        public List<mst_fleet_driver> getAlowcatedFleetAndDriverDetails(string driver, string fleet, DateTime frmDt, DateTime toDt, string fletordri)
        {
            try
            {
                ToursDAL _ToursDAL = new ToursDAL();
                return _ToursDAL.getAlowcatedFleetAndDriverDetails(driver, fleet, frmDt, toDt, fletordri);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GEN_CUS_ENQ_DRIVER> getAlowcatedFleetAndDriverDetailsInEnquiry(string driver, DateTime frmDt, DateTime toDt)
        {
            try
            {
                ToursDAL _ToursDAL = new ToursDAL();
                return _ToursDAL.getAlowcatedFleetAndDriverDetailsInEnquiry(driver, frmDt, toDt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<GEN_CUS_ENQ_DRIVER> getEnquiryDriverDetails(string enqId)
        {
            try
            {
                ToursDAL _ToursDAL = new ToursDAL();
                return _ToursDAL.getEnquiryDriverDetails(enqId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<GEN_CUS_ENQ_FLEET> getEnquiryFleetDetails(string enqId)
        {
            try
            {
                ToursDAL _ToursDAL = new ToursDAL();
                return _ToursDAL.getEnquiryFleetDetails(enqId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<GEN_CUS_ENQ_FLEET> getAlowcatedFleetDetailsInEnquiry(string fleet, DateTime frmDt, DateTime toDt)
        {
            try
            {
                ToursDAL _ToursDAL = new ToursDAL();
                return _ToursDAL.getAlowcatedFleetDetailsInEnquiry(fleet, frmDt, toDt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable getEnquiryHeaderData(string enqNo, string pc)
        {
            try
            {
                ToursDAL _ToursDAL = new ToursDAL();
                return _ToursDAL.getEnquiryHeaderData(enqNo, pc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable getEnquiryInvoiceItems(string enqNo)
        {
            try
            {
                ToursDAL _ToursDAL = new ToursDAL();
                return _ToursDAL.getEnquiryInvoiceItems(enqNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DEPO_AMT_DATA getLiabilityDetails(string liability)
        {
            try
            {
                ToursDAL _ToursDAL = new ToursDAL();
                return _ToursDAL.getLiabilityDetails(liability);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DEPO_AMT_DATA getLiabilityDatabyChgCd(string chgCd)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            return _ToursDAL.getLiabilityDatabyChgCd(chgCd);
        }

        //subodana 2016-12-19
        public DataTable GetAgreementParameters(Int32 agrrno)
        {
            try
            {
                ToursDAL _ToursDAL = new ToursDAL();
                return _ToursDAL.getAgreementParameter(agrrno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //subodana 2017-01-06
        public Int32 SaveBusChargeCode(List<Cus_chg_cds> chg, string userid, out string _error)
        {
            _error = string.Empty;
            Int32 result = 0;
            ToursDAL _ToursDAL = new ToursDAL();
            _ToursDAL.ConnectionOpen();
            _ToursDAL.BeginTransaction();
            try
            {
                result = _ToursDAL.Delete_BusChargecd(chg.FirstOrDefault().bcd_cus_cd.ToString(), userid);
                result = _ToursDAL.Save_BusChargecd(chg, userid);
                _ToursDAL.TransactionCommit();
            }
            catch (Exception ex)
            {
                _error = ex.Message.ToString();
                _ToursDAL.TransactionRollback();
                _ToursDAL.ConnectionClose();
            }
            return result;
        }
        public List<Cus_chg_cds> GETBUSCHARGECODES(string cuscd)
        {
            try
            {
                ToursDAL _ToursDAL = new ToursDAL();
                return _ToursDAL.GETBUSCHARGECODES(cuscd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //SUBODANA 2017-01-11
        public List<DriverAllocationHome> GET_TOURS_DRIALLOC(string company, string userDefPro, string pgeNum, string pgeSize, string searchFld, string searchVal, String Status)
        {
            _ToursDAL = new ToursDAL();
            return _ToursDAL.GET_TOURS_DRIALLOC(company, userDefPro, pgeNum, pgeSize, searchFld, searchVal, Status);
        }
        //SUBODANA 2017-01-13
        public List<FleetAlert> FleetAlertdata(DateTime DATE, string TYPE)
        {
            try
            {
                ToursDAL _ToursDAL = new ToursDAL();
                return _ToursDAL.FleetAlertdata(DATE, TYPE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //subodana 2017-01-19
        public DataTable GETINVCHARGES(string INVNO)
        {
            try
            {
                ToursDAL _ToursDAL = new ToursDAL();
                return _ToursDAL.GETINVCHARGES(INVNO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //subodana 2017-01-20
        public DataTable GETFLEETGES(string fleet, DateTime fdate, DateTime tdate)
        {
            try
            {
                ToursDAL _ToursDAL = new ToursDAL();
                return _ToursDAL.GETFLEETGES(fleet, fdate, tdate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //subodana 2017-02-14
        public DataTable CHECK_DRIVER_ALLOC(string fleet, DateTime fdate, DateTime tdate)
        {
            try
            {
                ToursDAL _ToursDAL = new ToursDAL();
                return _ToursDAL.CHECK_DRIVER_ALLOC(fleet, fdate, tdate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //subodana 2017-01-23
        public int SaveCreditNote(InvoiceHeader _invoiceHeader, List<InvoiceItem> _invoiceItem, MasterAutoNumber _invoiceAuto, out  string _invoiceNo, MasterBusinessEntity _businessCompany, out string _errorlist, List<MST_ST_PAX_DET> paxDetList = null, InvoiceHeader _oldInvoiceHeader = null)
        {
            string _invNo = string.Empty;
            string _recNo = string.Empty;
            string _DONo = string.Empty;
            string _buybackinv = string.Empty;
            Int32 _effect = 0;
            string _location = string.Empty;
            string _error = string.Empty;
            string _db = string.Empty;
            MasterAutoNumber _auto = null;
            bool _VoucherPromotion = false;
            Boolean _isNew = false;
            string enqMsg = string.Empty;
            bool _isHoldInvoiceProcess = false;
            try
            {
                _db = DataBase._ems;
                _salesDAL = new SalesDAL();
                _salesDAL.ConnectionOpen();
                _salesDAL.BeginTransaction();
                _db = DataBase._ems;
                _inventoryDAL = new InventoryDAL();
                _inventoryDAL.ConnectionOpen();
                _inventoryDAL.BeginTransaction();
                _db = DataBase._fms;
                _fmsInventoryDal = new FMS_InventoryDAL();
                _fmsInventoryDal.ConnectionOpen();
                _fmsInventoryDal.BeginTransaction();
                _db = DataBase._reportdb;
                _inventoryRepDAL = new ReptCommonDAL();
                _inventoryRepDAL.ConnectionOpen();
                _inventoryRepDAL.BeginTransaction();
                _db = DataBase._ems;
                _ToursDAL = new ToursDAL();
                _ToursDAL.ConnectionOpen();
                _ToursDAL.BeginTransaction();

                if (_invoiceHeader != null)
                {
                    _invoiceAuto.Aut_year = null;
                    MasterAutoNumber InvoiceAuto = _inventoryDAL.GetAutoNumber(_invoiceAuto.Aut_moduleid, _invoiceAuto.Aut_direction, _invoiceAuto.Aut_start_char, _invoiceAuto.Aut_cate_tp, _invoiceAuto.Aut_cate_cd, _invoiceAuto.Aut_modify_dt, _invoiceAuto.Aut_year);
                    _invNo = _invoiceAuto.Aut_start_char + InvoiceAuto.Aut_number.ToString("00000", CultureInfo.InvariantCulture);
                    _invoiceAuto.Aut_year = null;
                    _invoiceAuto.Aut_modify_dt = null;
                    _salesDAL.UpdateInvoiceAutoNumber(_invoiceAuto);
                    _invoiceHeader.Sah_inv_no = _invNo;
                    _invoiceHeader.Sah_ref_doc = _invoiceHeader.Sah_anal_4;
                }

                _db = string.Empty;
                _location = _invoiceHeader.Sah_com;
                string _recieptSeq = null;
                string _invoiceSeq = null;
                InventoryHeader _invHdr = null;
                DataTable _dataTable = null;
                decimal anal8 = _invoiceHeader.Sah_anal_8;
                InvoiceHeader invHed = _ToursDAL.getInvoiceHederData(_invoiceHeader.Sah_ref_doc, _invoiceHeader.Sah_com, _invoiceHeader.Sah_pc);
                string statusnew = "";

                if (invHed.Sah_anal_7 <= _invoiceHeader.Sah_anal_8)
                {
                    statusnew = "R";
                }
                else
                {
                    statusnew = "A";
                }
                _invoiceHeader.Sah_stus = "A";
                _invoiceHeader.Sah_anal_8 = 0;
                string invNo = (_invNo != "") ? _invNo : "";

                //CommonSaveInvoiceWithDeliveryOrderWithTransaction(_invoiceHeader, _invoiceItem,null, null,null, null,null,null, _invoiceAuto, null, null, false, out  string _invoiceNo, out _receiptNo, out _deliveryOrder, null,null, null, false, false, out  _error, false, out  _invoiceSeq, out  _recieptSeq, out _invHdr,  out _dataTable,string invNo);
                CommonSaveInvoiceWithDeliveryOrderWithTransaction(_invoiceHeader, _invoiceItem, null, null, null, null, null, null, _invoiceAuto, null, null, false, out _invNo, out _recNo, out _DONo, _inventoryDAL, _salesDAL, _inventoryRepDAL, false, _isHoldInvoiceProcess, out _error, false, out _invoiceSeq, out _recieptSeq, out _invHdr, out _dataTable, invNo);


                Int32 result = 0;

                result = _ToursDAL.UpdateEnquiryStage(13, _invoiceHeader.Sah_cre_by, _invoiceHeader.Sah_ref_doc, _invoiceHeader.Sah_com, _invoiceHeader.Sah_pc);

                result = _ToursDAL.updateOldTourInvoiceStatus(_oldInvoiceHeader);
                //2015-06-26
                if (_invoiceHeader != null)
                {
                    foreach (InvoiceItem item in _invoiceItem)
                    {
                        result = _ToursDAL.UPDATE_LOG_HDR_INVOICE(Convert.ToInt32(item.Sad_promo_cd), 1, _invoiceHeader.Sah_cre_by);
                    }
                }


                string _customerCode = (_invoiceHeader != null) ? _invoiceHeader.Sah_cus_cd : "";
                GroupBussinessEntity _businessEntityGrup = new GroupBussinessEntity();
                if (string.IsNullOrEmpty(_error))
                {
                    #region Customer Creation

                    #endregion Customer Creation

                    #region update auto no


                    #endregion update auto no

                    #region Update Manual Doc
                    if (_invoiceHeader != null)
                        if (_invoiceHeader.Sah_manual) _fmsInventoryDal.UpdateManualDocNo(_location, "MDOC_INV", Convert.ToInt32(_invoiceHeader.Sah_ref_doc), _invNo);

                    #endregion Update Manual Doc

                    #region update invoice discount / Promotion Voucher page as F
                    if (_invoiceHeader != null)
                    {
                        foreach (InvoiceItem _itm in _invoiceItem)
                        {
                            if (_itm.Sad_dis_type == "P")
                            {
                                _salesDAL.UpdateDiscountUsedTimes(_itm.Sad_dis_seq, 1);
                            }

                            if (_itm.Sad_res_no == "PROMO_VOU" && _itm.Sad_res_line_no > 0)
                            {
                                //Add by Chamal 6-Jul-2014
                                _salesDAL.Update_GV_Pages(1, _invoiceHeader.Sah_com, _invoiceHeader.Sah_pc, _invoiceHeader.Sah_dt.Date, "", "F", _itm.Sad_res_line_no, "P_GV", _invoiceHeader.Sah_cre_by, _invNo);
                            }
                        }


                    #endregion update invoice discount / Promotion Voucher page as F


                        //update anal8
                        _oldInvoiceHeader.Sah_anal_8 = anal8;
                        _oldInvoiceHeader.Sah_stus = statusnew;
                        result = _ToursDAL.updateOldTourInvoiceanal8(_oldInvoiceHeader);


                        if (paxDetList != null)
                        {
                            Int32 ln = 1;
                            foreach (MST_ST_PAX_DET pax in paxDetList)
                            {
                                pax.SPD_SEQ = Convert.ToInt32(_invoiceSeq);
                                pax.SPD_LINE = ln;
                                pax.SPD_INV_NO = _invNo;
                                pax.SPD_ENQ_ID = _invoiceHeader.Sah_ref_doc;
                                Int32 ef = _ToursDAL.saveInvoicePax(pax);
                                ln++;
                            }
                        }
                    }
                    _error = enqMsg + _error;
                    _error = _error.Trim();
                    _effect = 1;
                }
                else
                    _effect = -1;

                try
                {
                    if (string.IsNullOrEmpty(_error))
                    {
                        _db = DataBase._ems;
                        _salesDAL.TransactionCommit();
                        _db = DataBase._ems;
                        _inventoryDAL.TransactionCommit();
                        _db = DataBase._fms;
                        _fmsInventoryDal.TransactionCommit();
                        _db = DataBase._reportdb;
                        _inventoryRepDAL.TransactionCommit();
                        _db = DataBase._ems;
                        _ToursDAL.TransactionCommit();

                        if (_invNo != "")
                            _inventoryDAL.UpdateInvoiceDOStatus(_invNo);

                        //cus code update
                        if (_auto != null)
                        {
                            _inventoryDAL.UpdateAutoNumber(_auto);
                        }
                    }
                    else
                    {
                        _salesDAL.TransactionRollback();
                        _inventoryDAL.TransactionRollback();
                        _fmsInventoryDal.TransactionRollback();
                        _inventoryRepDAL.TransactionRollback();
                        _ToursDAL.TransactionRollback();
                    }
                }
                catch (Exception ex)
                {
                    _invoiceNo = string.Empty;
                    _errorlist = "Database" + _db + " is not responding. Please contact IT Operation.\n" + ex.Message;
                    _effect = -1;
                    return _effect;
                }
            }
            catch (Exception ex)
            {
                _error = ex.Message.ToString();
                _invoiceNo = string.Empty;
                _errorlist = _error;
                _effect = -1;

                _salesDAL.TransactionRollback();
                _inventoryDAL.TransactionRollback();
                _fmsInventoryDal.TransactionRollback();
                _inventoryRepDAL.TransactionRollback();
                _ToursDAL.TransactionRollback();
            }

            _invoiceNo = _invNo;
            _errorlist = _error;
            return _effect;
        }
        public int Isinvoiced(string com, string enqid)
        {
            ToursDAL _ToursDAL = new ToursDAL();
            DataTable dt1 = _ToursDAL.IsInvoiced1(com, enqid);
            DataTable dt2 = _ToursDAL.IsInvoiced2(enqid);
            int count = dt1.Rows.Count;
            count = count + dt2.Rows.Count;
            return count;
        }

        //ISURU
        public DataTable GET_PRINT_DATA(string id, string company)
        {
            _ToursDAL = new ToursDAL();
            _ToursDAL.ConnectionOpen();
            return _ToursDAL.GET_PRINT_DATA(id, company);

        }

        //ISURU 2017/02/22
        public DataTable GET_TRIPREQUEST_DATA(DateTime fromdate, DateTime todate, string company, string profcen)
        {
            _ToursDAL = new ToursDAL();
            _ToursDAL.ConnectionOpen();
            return _ToursDAL.GET_TRIPREQUEST_DATA(fromdate, todate, company, profcen);

        }

        //ISURU 2017/02/24
        public DataTable GET_LOGSHEET_DATA(DateTime fromdate, DateTime todate, string company, string profcen)
        {
            _ToursDAL = new ToursDAL();
            _ToursDAL.ConnectionOpen();
            return _ToursDAL.GET_LOGSHEET_DATA(fromdate, todate, company, profcen);

        }

        //ISURU 2017/02/27
        public DataTable HOME_CONFIG_DATA(string user, string company, string profcen, string type)
        {
            _ToursDAL = new ToursDAL();
            _ToursDAL.ConnectionOpen();
            return _ToursDAL.HOME_CONFIG_DATA(user, company, profcen, type);

        }

        // ISURU 2017/02/27

        public DataTable LEASED_CAR_DATA(DateTime fromdate, DateTime todate, string com, string fleet)
        {
            _ToursDAL = new ToursDAL();
            _ToursDAL.ConnectionOpen();
            return _ToursDAL.LEASED_CAR_DATA(fromdate, todate, com, fleet);
        }

        // ISURU 2017/02/28
        public List<FleetAllocDaily> FleetAllocDailydata(string com, DateTime fdate, DateTime todate, string prc)
        {
            try
            {
                ToursDAL _ToursDAL = new ToursDAL();
                return _ToursDAL.FleetAllocDailydata(com,  fdate, todate, prc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}