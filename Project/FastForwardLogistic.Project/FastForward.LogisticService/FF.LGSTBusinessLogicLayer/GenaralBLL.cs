using FF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using FF.DataAccessLayer;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using FF.BusinessObjects;
using FF.BusinessObjects.Genaral;
using FF.BusinessObjects.Search;
using FF.DataAccessLayer.BaseDAL;
using FF.BusinessObjects.Sales;

namespace FF.BusinessLogicLayer
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class GenaralBLL : IGenaral
    {
        GenaralDAL _GenaralDAL;
        SalesDAL _SalesDAL;

        //Isuru 2017/05/27
        public List<MST_COUNTRY> getCustomerCountry()
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            return _GenaralDAL.getCustomerCountry();
        }

        public List<MST_TITLE> GetTitleList()
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            return _GenaralDAL.GetTitleList();
        }
        //Isuru 2017/05/27
        public List<cus_details> getCustomerPassPNo(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            return _GenaralDAL.getCustomerPassPNo(pgeNum, pgeSize, searchFld, searchVal, company);

        }

        //Isuru 2017/05/27
        public List<cus_details> getCustomerDLNo(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            return _GenaralDAL.getCustomerDLNo(pgeNum, pgeSize, searchFld, searchVal, company);

        }

        //Isuru 2017/05/27
        public List<cus_details> getCustomerBRNo(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            return _GenaralDAL.getCustomerBRNo(pgeNum, pgeSize, searchFld, searchVal, company);

        }

        //Isuru 2017/05/27
        public List<cus_details> getCustomerTelNo(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            return _GenaralDAL.getCustomerTelNo(pgeNum, pgeSize, searchFld, searchVal, company);

        }

        //Isuru 2017/05/31
        public List<cus_details> getcustomernicno(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            return _GenaralDAL.getcustomernicno(pgeNum, pgeSize, searchFld, searchVal, company);

        }


        //Isuru 2017/05/31
        public List<mst_bus_entity_tp> getcustomertype(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            return _GenaralDAL.getcustomertype(pgeNum, pgeSize, searchFld, searchVal, company);

        }
        //Subodana
        public List<MainServices> GetMainServicesCodes()
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            return _GenaralDAL.GetMainServicesCodes();
        }
        //Subodana
        public List<PendingServiceRequest> GetPendingJobRequse(string Com)
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            return _GenaralDAL.GetPendingJobRequse(Com);
        }
        //Subodana
        public List<cus_details> GetCustormerdata(string Com, string cuscode)
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            return _GenaralDAL.GetCustormerdata(Com, cuscode);
        }
        //Dilshan 2017/09/11
        public List<cus_details> GetCountryTown(string Com, string concode)
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            return _GenaralDAL.GetCountryTown(Com, concode);
        }

        //Dilshan 04/09/2017
        public List<MST_EMP> GetEmployeedata(string Com, string cuscode)
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            return _GenaralDAL.GetEmployeedata(Com, cuscode);
        }
        //Dilshan 06/09/2017
        public List<MST_VESSEL> GetVessaldata(string Com, string vslcode)
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            return _GenaralDAL.GetVessaldata(Com, vslcode);
        }
        //Dilshan 06/09/2017
        public List<MST_VESSEL> GetCostdata(string Com, string eleCode)
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            return _GenaralDAL.GetCostdata(Com, eleCode);
        }
        public List<MST_VESSEL> GetPortdata(string Com, string prtCode)
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            return _GenaralDAL.GetPortdata(Com, prtCode);
        }
        //dilshan
        public List<Mst_empcate> Get_mst_empcate()
        {
            _GenaralDAL = new GenaralDAL();
            return _GenaralDAL.Get_mst_empcate();
        }
        public Int32 Check_Employeeepf(string epf)
        {
            _GenaralDAL = new GenaralDAL();
            _GenaralDAL.ConnectionOpen();
            int retu = _GenaralDAL.Check_Employeeepf(epf);
            _GenaralDAL.TransactionCommit();
            _GenaralDAL.ConnectionClose();
            return retu;
        }

        public Int32 Check_vessal(string code)
        {
            _GenaralDAL = new GenaralDAL();
            _GenaralDAL.ConnectionOpen();
            int retu = _GenaralDAL.Check_vessal(code);
            _GenaralDAL.TransactionCommit();
            _GenaralDAL.ConnectionClose();
            return retu;
        }
        public Int32 Check_costele(string code)
        {
            _GenaralDAL = new GenaralDAL();
            _GenaralDAL.ConnectionOpen();
            int retu = _GenaralDAL.Check_costele(code);
            _GenaralDAL.TransactionCommit();
            _GenaralDAL.ConnectionClose();
            return retu;
        }
        public Int32 Check_port(string code)
        {
            _GenaralDAL = new GenaralDAL();
            _GenaralDAL.ConnectionOpen();
            int retu = _GenaralDAL.Check_port(code);
            _GenaralDAL.TransactionCommit();
            _GenaralDAL.ConnectionClose();
            return retu;
        }

        //Sanjaya 2017-06-27
        public cus_code_api SaveJobRequest(trn_req_hdr request, CustomerSearchObject customerObject, MasterAutoNumber customerNumber, MasterAutoNumber jobNumber, mst_busentity_grup busentityGrup, trn_req_serdet reqSerdet, trn_req_cus_det reqCusDet)
        {
            int count = 0;
            string jobSeq = "0";
            cus_code_api _cus_code_api = new cus_code_api();
            try
            {
                _GenaralDAL = new GenaralDAL();



                string customerCode = String.Empty;
                string jobCode = String.Empty;
                Int32 effect = 0;

                _GenaralDAL.ConnectionOpen();
                _GenaralDAL.BeginTransaction();


                //Create Auto number for job request

                Int32 _autoJobNumber =
                    _GenaralDAL.GetAutoNumber(jobNumber.Aut_moduleid, jobNumber.Aut_direction,
                        jobNumber.Aut_start_char, jobNumber.Aut_cate_tp, jobNumber.Aut_cate_cd,
                        jobNumber.Aut_modify_dt, jobNumber.Aut_year).Aut_number;

                jobCode = jobNumber.Aut_cate_cd + "/" + jobNumber.Aut_start_char + "/" + Convert.ToString(jobNumber.Aut_year).Remove(0, 2) + "/" + _autoJobNumber.ToString("0000", CultureInfo.InvariantCulture);
                request.Rq_no = jobCode;
                //Update new number
                count = count + _GenaralDAL.UpdateAutoNumber(jobNumber);

                //Save job request
                jobSeq = _GenaralDAL.SaveJobRequest(request);
                count = jobSeq == "0" ? 0 : count + 1;

                if (customerObject.Mbe_cd == null)
                {
                    //Create customer code
                    Int32 _autoNumber =
                        _GenaralDAL.GetAutoNumber(customerNumber.Aut_moduleid, customerNumber.Aut_direction,
                            customerNumber.Aut_start_char, customerNumber.Aut_cate_tp, customerNumber.Aut_cate_cd,
                            customerNumber.Aut_modify_dt, customerNumber.Aut_year).Aut_number;

                    customerCode = customerNumber.Aut_cate_cd + "-" + customerNumber.Aut_start_char + "-" +
                                   _autoNumber.ToString("0000", CultureInfo.InvariantCulture);
                    customerObject.Mbe_cd = customerCode;

                    //Update new number
                    count = count + _GenaralDAL.UpdateAutoNumber(customerNumber);

                    //Save customer to busentity
                    count = count + _GenaralDAL.SaveNewCustomer(customerObject);

                    //Save customer to busentity_group
                    busentityGrup.Mbg_cd = customerCode;
                    count = count + _GenaralDAL.SaveNewCustomerGroup(busentityGrup);
                }
                else
                {
                    customerCode = customerObject.Mbe_cd;
                }

                //Save to ser_det table
                reqSerdet.rs_seq_no = jobSeq;
                reqSerdet.rs_cus_cd = customerCode;
                count = count + _GenaralDAL.SaveRequestDetails(reqSerdet);

                //Save to trn_req_cus_det table
                reqCusDet.Rc_seq_no = jobSeq;
                reqCusDet.Rc_cus_cd = customerCode;
                count = count + _GenaralDAL.SaveRequesCustomertDetails(reqCusDet);

                _GenaralDAL.TransactionCommit();
                _GenaralDAL.ConnectionClose();

                _cus_code_api.count = count;
                _cus_code_api.cus_code = customerCode;

            }
            catch (Exception ex)
            {
                _GenaralDAL.TransactionRollback();
                _GenaralDAL.ConnectionClose();
                _cus_code_api.count = 0;
            }


            return _cus_code_api;
        }
        //subodana 2017/06/20
        //public Int32 SaveJobDetails(trn_jb_hdr _hdr, List<trn_job_serdet> _jobdet, List<trn_jb_cus_det> _cusdet, MasterAutoNumber _masterAutoNumber, out string err)
        //{
        //    err = "";
        //    Int32 seqno = 0;
        //    int effect = 0;
        //    string documentNo = "";
        //    try
        //    {
        //        _GenaralDAL = new GenaralDAL();
        //        _GenaralDAL.ConnectionOpen();
        //        _GenaralDAL.BeginTransaction();

        //        //if (_hdr.Jb_jb_no == null || _hdr.Jb_jb_no == "")
        //        //{
        //        //    Int32 _autoNo = _GenaralDAL.GetAutoNumber(_masterAutoNumber.Aut_moduleid, _masterAutoNumber.Aut_direction, _masterAutoNumber.Aut_start_char, _masterAutoNumber.Aut_cate_tp, _masterAutoNumber.Aut_cate_cd, _masterAutoNumber.Aut_modify_dt, _masterAutoNumber.Aut_year).Aut_number;
        //        //    documentNo = _masterAutoNumber.Aut_cate_cd + "-" + _masterAutoNumber.Aut_start_char + "-" + Convert.ToString(_masterAutoNumber.Aut_year).Remove(0, 2) + "-" + _autoNo.ToString("0000", CultureInfo.InvariantCulture);
        //        //    effect = _GenaralDAL.UpdateAutoNumber(_masterAutoNumber);
        //        //    _hdr.Jb_jb_no = documentNo;
        //        //    _hdr.Jb_pouch_no = _autoNo.ToString("00000", CultureInfo.InvariantCulture);
        //        //    err = documentNo + " Pouch No :" + _hdr.Jb_pouch_no;
        //        //}

        //        //// Commented above  and Added below by Chathura on 04-oct-2017 requested by Vajira (COMPANY/CHANNEL/PROFITCENTR/YEAR/ AUTO NUMBER)
        //        if (_hdr.Jb_jb_no == null || _hdr.Jb_jb_no == "")
        //        {
        //            Int32 _autoNo = _GenaralDAL.GetAutoNumber(_masterAutoNumber.Aut_moduleid, _masterAutoNumber.Aut_direction, _masterAutoNumber.Aut_start_char, _masterAutoNumber.Aut_cate_tp, _masterAutoNumber.Aut_cate_cd, _masterAutoNumber.Aut_modify_dt, _masterAutoNumber.Aut_year).Aut_number;
        //            documentNo = _masterAutoNumber.Aut_cate_cd + "-" + _masterAutoNumber.Aut_start_char + "-" + Convert.ToString(_masterAutoNumber.Aut_year).Remove(0, 2) + "-" + _autoNo.ToString("0000", CultureInfo.InvariantCulture);
        //            effect = _GenaralDAL.UpdateAutoNumber(_masterAutoNumber);
        //            _hdr.Jb_jb_no = documentNo;
        //            _hdr.Jb_pouch_no = _autoNo.ToString("00000", CultureInfo.InvariantCulture);
        //            err = documentNo + " Pouch No :" + _hdr.Jb_pouch_no;
        //        }

        //        effect = _GenaralDAL.SaveJobserHDR(_hdr, out seqno);
        //        err = _hdr.Jb_jb_no + " Pouch No :" + _hdr.Jb_pouch_no;
        //        effect = _GenaralDAL.DeleteSerJobDetails(seqno.ToString());
        //        List<trn_jb_cus_det> _fin_cus= new List<trn_jb_cus_det>();

        //        if (_jobdet != null)
        //        {
        //            foreach (var _detlist in _jobdet)
        //            {
        //                _detlist.JS_SEQ_NO = seqno.ToString();
        //                effect = _GenaralDAL.SaveJobServiceDetails(_detlist);
        //                var _f_cus = _cusdet.Where(a => a.Jc_cus_cd == _detlist.JS_CUS_CD).First();
        //                var _cus_count = 0;
        //                if (_fin_cus.Count>0)
        //                {
        //                    _cus_count = _fin_cus.Where(a => a.Jc_cus_cd == _detlist.JS_CUS_CD).Count();
        //                }
        //                if (_cus_count ==0)
        //               {
        //                   _fin_cus.Add(_f_cus);
        //               }
        //            }
        //        }
        //        if (_fin_cus != null)
        //        {
        //            foreach (var _cuslist in _fin_cus)
        //            {
        //                _cuslist.Jc_seq_no = seqno.ToString();
        //                effect = _GenaralDAL.SaveJobServiceCustormer(_cuslist);
        //            }
        //        }
        //        _GenaralDAL.TransactionCommit();

        //    }
        //    catch (Exception ex)
        //    {
        //        err = ex.Message;
        //        _GenaralDAL.TransactionRollback();
        //        _GenaralDAL.ConnectionClose();
        //        effect = -1;
        //    }

        //    return effect;
        //}

        // Updated by Chathura
        public Int32 SaveJobDetails(trn_jb_hdr _hdr, List<trn_job_serdet> _jobdet, List<trn_jb_cus_det> _cusdet, MasterAutoNumber _masterAutoNumber, out string err)
        {
            err = "";
            Int32 seqno = 0;
            int effect = 0;
            string documentNo = "";
            try
            {
                _GenaralDAL = new GenaralDAL();
                _GenaralDAL.ConnectionOpen();
                _GenaralDAL.BeginTransaction();

                //if (_hdr.Jb_jb_no == null || _hdr.Jb_jb_no == "")
                //{
                //    Int32 _autoNo = _GenaralDAL.GetAutoNumber(_masterAutoNumber.Aut_moduleid, _masterAutoNumber.Aut_direction, _masterAutoNumber.Aut_start_char, _masterAutoNumber.Aut_cate_tp, _masterAutoNumber.Aut_cate_cd, _masterAutoNumber.Aut_modify_dt, _masterAutoNumber.Aut_year).Aut_number;
                //    documentNo = _masterAutoNumber.Aut_cate_cd + "-" + _masterAutoNumber.Aut_start_char + "-" + Convert.ToString(_masterAutoNumber.Aut_year).Remove(0, 2) + "-" + _autoNo.ToString("0000", CultureInfo.InvariantCulture);
                //    effect = _GenaralDAL.UpdateAutoNumber(_masterAutoNumber);
                //    _hdr.Jb_jb_no = documentNo;
                //    _hdr.Jb_pouch_no = _autoNo.ToString("00000", CultureInfo.InvariantCulture);
                //    err = documentNo + " Pouch No :" + _hdr.Jb_pouch_no;
                //}

                //// Commented above  and Added below by Chathura on 04-oct-2017 requested by Vajira (COMPANY/CHANNEL/PROFITCENTR/YEAR/ AUTO NUMBER)
                if (_hdr.Jb_jb_no == null || _hdr.Jb_jb_no == "")
                {
                    Int32 _autoNo = _GenaralDAL.GetAutoNumber(_masterAutoNumber.Aut_moduleid, _masterAutoNumber.Aut_direction, _masterAutoNumber.Aut_start_char, _masterAutoNumber.Aut_cate_tp, _masterAutoNumber.Aut_cate_cd, _masterAutoNumber.Aut_modify_dt, _masterAutoNumber.Aut_year).Aut_number;
                    //documentNo = _masterAutoNumber.Aut_cate_cd + "/" + _masterAutoNumber.Aut_start_char + "-" + Convert.ToString(_masterAutoNumber.Aut_year).Remove(0, 2) + "-" + _autoNo.ToString("0000", CultureInfo.InvariantCulture);
                    documentNo = _hdr.Jb_com_cd + "/" + _hdr.chnl + "/" + _hdr.pc + "" + "0" + "" + _autoNo.ToString("00000", CultureInfo.InvariantCulture);
                    effect = _GenaralDAL.UpdateAutoNumber(_masterAutoNumber);
                    _hdr.Jb_jb_no = documentNo;
                    _hdr.Jb_pouch_no = _autoNo.ToString("00000", CultureInfo.InvariantCulture);
                    err = documentNo + " Pouch No :" + _hdr.Jb_pouch_no;
                }

                effect = _GenaralDAL.SaveJobserHDR(_hdr, out seqno);
                err = _hdr.Jb_jb_no + " Pouch No :" + _hdr.Jb_pouch_no;
                var effect2 = _GenaralDAL.DeleteSerJobDetails(seqno.ToString());
                List<trn_jb_cus_det> _fin_cus = new List<trn_jb_cus_det>();

                if (_jobdet != null)
                {
                    foreach (var _detlist in _jobdet)
                    {
                        _detlist.JS_SEQ_NO = seqno.ToString();
                        effect = _GenaralDAL.SaveJobServiceDetails(_detlist);
                        var _f_cus = _cusdet.Where(a => a.Jc_cus_cd == _detlist.JS_CUS_CD).First();
                        var _cus_count = 0;
                        if (_fin_cus.Count > 0)
                        {
                            _cus_count = _fin_cus.Where(a => a.Jc_cus_cd == _detlist.JS_CUS_CD).Count();
                        }
                        if (_cus_count == 0)
                        {
                            _fin_cus.Add(_f_cus);
                        }
                    }
                }
                if (_fin_cus != null)
                {
                    foreach (var _cuslist in _fin_cus)
                    {
                        _cuslist.Jc_seq_no = seqno.ToString();
                        effect = _GenaralDAL.SaveJobServiceCustormer(_cuslist);
                    }
                }
                _GenaralDAL.TransactionCommit();

            }
            catch (Exception ex)
            {
                err = ex.Message;
                _GenaralDAL.TransactionRollback();
                _GenaralDAL.ConnectionClose();
                effect = -1;
            }

            return effect;
        }
        //SUBODANA 2017/06/21
        //public int SaveJobDetailswithPendingRequest(List<PendingServiceRequest> _penging, trn_jb_hdr _hdr, MasterAutoNumber _masterAutoNumber, out string err)
        //{
        //    int effect = 0;
        //    err = "";
        //    try
        //    {
        //        _GenaralDAL = new GenaralDAL();
        //        _GenaralDAL.ConnectionOpen();
        //        _GenaralDAL.BeginTransaction();
        //        string documentNo = "";
        //        Int32 seqno = 0;
        //        List<trn_job_serdet> _allser = new List<trn_job_serdet>();
        //        List<trn_jb_cus_det> _allcusde = new List<trn_jb_cus_det>();
        //        //autonumber
        //        Int32 _autoNo = _GenaralDAL.GetAutoNumber(_masterAutoNumber.Aut_moduleid, _masterAutoNumber.Aut_direction, _masterAutoNumber.Aut_start_char, _masterAutoNumber.Aut_cate_tp, _masterAutoNumber.Aut_cate_cd, _masterAutoNumber.Aut_modify_dt, _masterAutoNumber.Aut_year).Aut_number;
        //        documentNo = _masterAutoNumber.Aut_cate_cd + "-" + _masterAutoNumber.Aut_start_char + "-" + Convert.ToString(_masterAutoNumber.Aut_year).Remove(0, 2) + "-" + _autoNo.ToString("0000", CultureInfo.InvariantCulture);
        //        effect = _GenaralDAL.UpdateAutoNumber(_masterAutoNumber);

        //        _hdr.Jb_jb_no = documentNo;
        //        _hdr.Jb_pouch_no = _autoNo.ToString("00000", CultureInfo.InvariantCulture);
        //        err = documentNo + " Pouch No :" + _hdr.Jb_pouch_no;
        //        effect = _GenaralDAL.SaveJobserHDR(_hdr, out seqno);
        //        int i = 1;
        //        foreach (var pn_list in _penging)
        //        {

        //            List<PendingServiceRequest> pen_det = _GenaralDAL.GetPendingJobRequstData(_hdr.Jb_com_cd, pn_list.rq_no);
        //            List<trn_req_serdet> req_ser_req = _GenaralDAL.GetPendingJobserdata(pen_det.First().rq_seq_no);
        //            //update status
        //            effect = _GenaralDAL.UpdateReqStatus(pn_list.rq_no, documentNo);
        //            foreach (var itm in req_ser_req)
        //            {
        //                trn_job_serdet ob = new trn_job_serdet();
        //                ob.JS_CRE_BY = _hdr.Jb_cre_by;
        //                ob.JS_CRE_DT = DateTime.Now;
        //                ob.JS_CUS_CD = itm.rs_cus_cd;
        //                ob.JS_LINE_NO = i;
        //                ob.JS_MOD_BY = _hdr.Jb_mod_by;
        //                ob.JS_MOD_DT = DateTime.Now;
        //                ob.JS_PC = _hdr.pc;
        //                ob.JS_RMK = _hdr.Jb_rmk;
        //                ob.JS_SEQ_NO = seqno.ToString();
        //                ob.JS_SER_TP = itm.rs_ser_tp;
        //                _allser.Add(ob);
        //                i++;

        //            }
        //            List<trn_req_cus_det> req_cus_req = _GenaralDAL.GetPendingJobcusdata(pen_det.First().rq_seq_no);
        //            foreach (var itm2 in req_cus_req)
        //            {
        //                trn_jb_cus_det ob = new trn_jb_cus_det();
        //                ob.Jc_cre_by = _hdr.Jb_cre_by;
        //                ob.Jc_cre_dt = DateTime.Now;
        //                ob.Jc_cus_cd = itm2.Rc_cus_cd;
        //                ob.Jc_cus_tp = itm2.Rc_cus_tp;
        //                ob.Jc_exe_cd = itm2.Rc_exe_cd;
        //                ob.Jc_mod_by = _hdr.Jb_mod_by;
        //                ob.Jc_mod_dt = DateTime.Now;
        //                ob.Jc_seq_no = seqno.ToString();
        //                _allcusde.Add(ob);
        //            }
        //        }

        //        if (_allser != null)
        //        {
        //            foreach (var _detlist in _allser)
        //            {
        //                _detlist.JS_SEQ_NO = seqno.ToString();
        //                effect = _GenaralDAL.SaveJobServiceDetails(_detlist);
        //            }
        //        }
        //        if (_allcusde != null)
        //        {
        //            foreach (var _cuslist in _allcusde)
        //            {
        //                _cuslist.Jc_seq_no = seqno.ToString();
        //                effect = _GenaralDAL.SaveJobServiceCustormer(_cuslist);
        //            }

        //        }

        //        _GenaralDAL.TransactionCommit();

        //    }
        //    catch (Exception ex)
        //    {
        //        err = ex.Message;
        //        _GenaralDAL.TransactionRollback();
        //        _GenaralDAL.ConnectionClose();
        //        effect = -1;
        //    }

        //    return effect;
        //}

        // Updated by Chathura
        public int SaveJobDetailswithPendingRequest(List<PendingServiceRequest> _penging, trn_jb_hdr _hdr, MasterAutoNumber _masterAutoNumber, out string err)
        {
            int effect = 0;
            err = "";
            try
            {
                _GenaralDAL = new GenaralDAL();
                _GenaralDAL.ConnectionOpen();
                _GenaralDAL.BeginTransaction();
                string documentNo = "";
                Int32 seqno = 0;
                List<trn_job_serdet> _allser = new List<trn_job_serdet>();
                List<trn_jb_cus_det> _allcusde = new List<trn_jb_cus_det>();
                //autonumber
                Int32 _autoNo = _GenaralDAL.GetAutoNumber(_masterAutoNumber.Aut_moduleid, _masterAutoNumber.Aut_direction, _masterAutoNumber.Aut_start_char, _masterAutoNumber.Aut_cate_tp, _masterAutoNumber.Aut_cate_cd, _masterAutoNumber.Aut_modify_dt, _masterAutoNumber.Aut_year).Aut_number;
                //documentNo = _masterAutoNumber.Aut_cate_cd + "-" + _masterAutoNumber.Aut_start_char + "-" + Convert.ToString(_masterAutoNumber.Aut_year).Remove(0, 2) + "-" + _autoNo.ToString("0000", CultureInfo.InvariantCulture);
                documentNo = _hdr.Jb_com_cd + "/" + _hdr.chnl + "/" + _hdr.pc + "" + "0" + "" + _autoNo.ToString("00000", CultureInfo.InvariantCulture);
                effect = _GenaralDAL.UpdateAutoNumber(_masterAutoNumber);

                _hdr.Jb_jb_no = documentNo;
                _hdr.Jb_pouch_no = _autoNo.ToString("00000", CultureInfo.InvariantCulture);
                err = documentNo + " Pouch No :" + _hdr.Jb_pouch_no;
                effect = _GenaralDAL.SaveJobserHDR(_hdr, out seqno);
                int i = 1;
                foreach (var pn_list in _penging)
                {

                    List<PendingServiceRequest> pen_det = _GenaralDAL.GetPendingJobRequstData(_hdr.Jb_com_cd, pn_list.rq_no);
                    List<trn_req_serdet> req_ser_req = _GenaralDAL.GetPendingJobserdata(pen_det.First().rq_seq_no);
                    //update status
                    effect = _GenaralDAL.UpdateReqStatus(pn_list.rq_no, documentNo);
                    foreach (var itm in req_ser_req)
                    {
                        trn_job_serdet ob = new trn_job_serdet();
                        ob.JS_CRE_BY = _hdr.Jb_cre_by;
                        ob.JS_CRE_DT = DateTime.Now;
                        ob.JS_CUS_CD = itm.rs_cus_cd;
                        ob.JS_LINE_NO = i;
                        ob.JS_MOD_BY = _hdr.Jb_mod_by;
                        ob.JS_MOD_DT = DateTime.Now;
                        ob.JS_PC = _hdr.pc;
                        ob.JS_RMK = _hdr.Jb_rmk;
                        ob.JS_SEQ_NO = seqno.ToString();
                        ob.JS_SER_TP = itm.rs_ser_tp;
                        _allser.Add(ob);
                        i++;

                    }
                    List<trn_req_cus_det> req_cus_req = _GenaralDAL.GetPendingJobcusdata(pen_det.First().rq_seq_no);
                    foreach (var itm2 in req_cus_req)
                    {
                        trn_jb_cus_det ob = new trn_jb_cus_det();
                        ob.Jc_cre_by = _hdr.Jb_cre_by;
                        ob.Jc_cre_dt = DateTime.Now;
                        ob.Jc_cus_cd = itm2.Rc_cus_cd;
                        ob.Jc_cus_tp = itm2.Rc_cus_tp;
                        ob.Jc_exe_cd = itm2.Rc_exe_cd;
                        ob.Jc_mod_by = _hdr.Jb_mod_by;
                        ob.Jc_mod_dt = DateTime.Now;
                        ob.Jc_seq_no = seqno.ToString();
                        _allcusde.Add(ob);
                    }
                }

                if (_allser != null)
                {
                    foreach (var _detlist in _allser)
                    {
                        _detlist.JS_SEQ_NO = seqno.ToString();
                        effect = _GenaralDAL.SaveJobServiceDetails(_detlist);
                    }
                }
                if (_allcusde != null)
                {
                    foreach (var _cuslist in _allcusde)
                    {
                        _cuslist.Jc_seq_no = seqno.ToString();
                        effect = _GenaralDAL.SaveJobServiceCustormer(_cuslist);
                    }

                }

                _GenaralDAL.TransactionCommit();

            }
            catch (Exception ex)
            {
                err = ex.Message;
                _GenaralDAL.TransactionRollback();
                _GenaralDAL.ConnectionClose();
                effect = -1;
            }

            return effect;
        }
        //Subodana
        public List<trn_jb_hdr> GetJobHdr(string doc)
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            return _GenaralDAL.GetJobHdr(doc);
        }
        public List<trn_jb_hdr> GetJobHdrbypouch(string pouch)
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            return _GenaralDAL.GetJobHdrbypouch(pouch);
        }
        //Subodana
        public List<trn_req_cus_det> req_cus_data(string doc)
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            return _GenaralDAL.GetPendingJobcusdata(doc);
        }
        //Subodana
        public List<PendingServiceRequest> req_all_data(string com, string doc)
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            return _GenaralDAL.GetPendingJobRequstData(com, doc);
        }
        //Subodana
        public List<trn_job_serdet> GetJobServicesdetails(string seq)
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            return _GenaralDAL.GetJobServicesdetails(seq);
        }
        //Subodana
        public List<trn_jb_cus_det> GetJobCustomerDetails(string seq)
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            return _GenaralDAL.GetJobCustomerDetails(seq);
        }

        public int SaveBLDraftHdr(trn_bl_header _hdr, List<trn_bl_det> _bl_det, List<trn_bl_cont_det> _con_det, MasterAutoNumber _masterAutoNumber, out string err)
        {
            err = "";
            Int32 seqno = 0;
            int effect = 0;
            string documentNo = _hdr.Bl_d_doc_no;
            err = documentNo;
            try
            {
                _GenaralDAL = new GenaralDAL();
                _GenaralDAL.ConnectionOpen();
                _GenaralDAL.BeginTransaction();
                //autonumber
                if (_hdr.Bl_d_doc_no == "" || _hdr.Bl_d_doc_no == null)
                {
                    Int32 _autoNo = _GenaralDAL.GetAutoNumber(_masterAutoNumber.Aut_moduleid, _masterAutoNumber.Aut_direction, _masterAutoNumber.Aut_start_char, _masterAutoNumber.Aut_cate_tp, _masterAutoNumber.Aut_cate_cd, _masterAutoNumber.Aut_modify_dt, _masterAutoNumber.Aut_year).Aut_number;
                    documentNo = _masterAutoNumber.Aut_cate_cd + "-" + _masterAutoNumber.Aut_start_char + "-" + Convert.ToString(_masterAutoNumber.Aut_year).Remove(0, 2) + "-" + _autoNo.ToString("0000", CultureInfo.InvariantCulture);
                    effect = _GenaralDAL.UpdateAutoNumber(_masterAutoNumber);
                    err = documentNo;
                    _hdr.Bl_doc_no = documentNo;
                    _hdr.Bl_d_doc_no = documentNo;
                }
                _hdr.Bl_doc_no = _hdr.Bl_d_doc_no;
                //get custormerdata
                if (_hdr.Bl_cus_cd != "" && _hdr.Bl_cus_cd != null)
                {
                    List<GET_CUS_BASIC_DATA> _cusdata = _GenaralDAL.GetCustormerBasicData(_hdr.Bl_cus_cd, _hdr.Bl_com_cd, "C");

                }
                if (_hdr.Bl_shipper_cd != "" && _hdr.Bl_shipper_cd != null)
                {
                    List<GET_CUS_BASIC_DATA> _cusdata = _GenaralDAL.GetCustormerBasicData(_hdr.Bl_shipper_cd, _hdr.Bl_com_cd, "C");
                    if (_cusdata != null)
                    {
                        if (_cusdata.Count > 0)
                        {
                            _hdr.Bl_shipper_name = _cusdata.First().mbe_name.ToString();
                            _hdr.Bl_shipper_add1 = _cusdata.First().mbe_add1.ToString();
                            _hdr.Bl_shipper_add2 = _cusdata.First().mbe_add2.ToString();
                        }
                    }

                }
                if (_hdr.Bl_consignee_cd != "" && _hdr.Bl_consignee_cd != null)
                {
                    List<GET_CUS_BASIC_DATA> _cusdata = _GenaralDAL.GetCustormerBasicData(_hdr.Bl_consignee_cd, _hdr.Bl_com_cd, "C");
                    if (_cusdata != null)
                    {
                        if (_cusdata.Count > 0)
                        {
                            _hdr.Bl_consignee_name = _cusdata.First().mbe_name.ToString();
                            _hdr.Bl_consignee_add1 = _cusdata.First().mbe_add1.ToString();
                            _hdr.Bl_consignee_add2 = _cusdata.First().mbe_add2.ToString();
                        }
                    }

                }
                if (_hdr.Bl_ntfy_party_cd != "" && _hdr.Bl_ntfy_party_cd != null)
                {
                    List<GET_CUS_BASIC_DATA> _cusdata = _GenaralDAL.GetCustormerBasicData(_hdr.Bl_ntfy_party_cd, _hdr.Bl_com_cd, "C");
                    if (_cusdata != null)
                    {
                        if (_cusdata.Count > 0)
                        {
                            _hdr.Bl_ntfy_party_name = _cusdata.First().mbe_name.ToString();
                            _hdr.Bl_ntfy_party_add1 = _cusdata.First().mbe_add1.ToString();
                            _hdr.Bl_ntfy_party_add2 = _cusdata.First().mbe_add2.ToString();
                        }
                    }

                }
                if (_hdr.Bl_agent_cd != "" && _hdr.Bl_agent_cd != null)
                {
                    List<GET_CUS_BASIC_DATA> _cusdata = _GenaralDAL.GetCustormerBasicData(_hdr.Bl_agent_cd, _hdr.Bl_com_cd, "C");
                    if (_cusdata != null)
                    {
                        if (_cusdata.Count > 0)
                        {
                            _hdr.Bl_del_agent_name = _cusdata.First().mbe_name.ToString();
                            _hdr.Bl_del_agent_add1 = _cusdata.First().mbe_add1.ToString();
                            _hdr.Bl_del_agent_add2 = _cusdata.First().mbe_add2.ToString();
                            _hdr.Bl_del_agent_cd = _hdr.Bl_agent_cd;
                        }
                    }

                }

                effect = _GenaralDAL.SaveBLHDR(_hdr, out seqno);
                effect = _GenaralDAL.DeleteBLContdata(seqno.ToString());
                if (_bl_det != null)
                {
                    foreach (var _bllist in _bl_det)
                    {
                        _bllist.bld_seq_no = seqno.ToString();
                        effect = _GenaralDAL.SaveDrafBLDetails(_bllist);
                    }
                }
                if (_con_det != null)
                {
                    foreach (var _con_list in _con_det)
                    {
                        _con_list.Blct_seq_no = seqno.ToString();
                        _con_list.Blct_bl_doc = _hdr.Bl_doc_no;
                        effect = _GenaralDAL.SaveDrafBLContainerDetails(_con_list);
                    }
                }
                _GenaralDAL.TransactionCommit();
            }

            catch (Exception ex)
            {
                err = ex.Message;
                _GenaralDAL.TransactionRollback();
                _GenaralDAL.ConnectionClose();
                effect = -1;
            }
            return effect;
        }
        public int SaveBLHousetHdr(trn_bl_header _hdr, List<trn_bl_det> _bl_det, List<trn_bl_cont_det> _con_det, MasterAutoNumber _masterAutoNumber, out string err)
        {
            err = "";
            Int32 seqno = 0;
            int effect = 0;
            string documentNo = _hdr.Bl_h_doc_no;
            err = documentNo;
            try
            {
                _GenaralDAL = new GenaralDAL();
                _GenaralDAL.ConnectionOpen();
                _GenaralDAL.BeginTransaction();
                //autonumber
                if (_hdr.Bl_h_doc_no == "" || _hdr.Bl_h_doc_no == null)
                {
                    Int32 _autoNo = _GenaralDAL.GetAutoNumber(_masterAutoNumber.Aut_moduleid, _masterAutoNumber.Aut_direction, _masterAutoNumber.Aut_start_char, _masterAutoNumber.Aut_cate_tp, _masterAutoNumber.Aut_cate_cd, _masterAutoNumber.Aut_modify_dt, _masterAutoNumber.Aut_year).Aut_number;
                    documentNo = _masterAutoNumber.Aut_cate_cd + "-" + _masterAutoNumber.Aut_start_char + "-" + Convert.ToString(_masterAutoNumber.Aut_year).Remove(0, 2) + "-" + _autoNo.ToString("0000", CultureInfo.InvariantCulture);
                    effect = _GenaralDAL.UpdateAutoNumber(_masterAutoNumber);
                    err = documentNo;
                    _hdr.Bl_doc_no = documentNo;
                    _hdr.Bl_h_doc_no = documentNo;
                }
                _hdr.Bl_doc_no = _hdr.Bl_h_doc_no;
                //get custormerdata
                if (_hdr.Bl_cus_cd != "" && _hdr.Bl_cus_cd != null)
                {
                    List<GET_CUS_BASIC_DATA> _cusdata = _GenaralDAL.GetCustormerBasicData(_hdr.Bl_cus_cd, _hdr.Bl_com_cd, "C");

                }
                if (_hdr.Bl_shipper_cd != "" && _hdr.Bl_shipper_cd != null)
                {
                    List<GET_CUS_BASIC_DATA> _cusdata = _GenaralDAL.GetCustormerBasicData(_hdr.Bl_shipper_cd, _hdr.Bl_com_cd, "C");
                    if (_cusdata != null)
                    {
                        if (_cusdata.Count > 0)
                        {
                            _hdr.Bl_shipper_name = _cusdata.First().mbe_name.ToString();
                            _hdr.Bl_shipper_add1 = _cusdata.First().mbe_add1.ToString();
                            _hdr.Bl_shipper_add2 = _cusdata.First().mbe_add2.ToString();
                        }
                    }

                }
                if (_hdr.Bl_consignee_cd != "" && _hdr.Bl_consignee_cd != null)
                {
                    List<GET_CUS_BASIC_DATA> _cusdata = _GenaralDAL.GetCustormerBasicData(_hdr.Bl_consignee_cd, _hdr.Bl_com_cd, "C");
                    if (_cusdata != null)
                    {
                        if (_cusdata.Count > 0)
                        {
                            _hdr.Bl_consignee_name = _cusdata.First().mbe_name.ToString();
                            _hdr.Bl_consignee_add1 = _cusdata.First().mbe_add1.ToString();
                            _hdr.Bl_consignee_add2 = _cusdata.First().mbe_add2.ToString();
                        }
                    }

                }
                if (_hdr.Bl_ntfy_party_cd != "" && _hdr.Bl_ntfy_party_cd != null)
                {
                    List<GET_CUS_BASIC_DATA> _cusdata = _GenaralDAL.GetCustormerBasicData(_hdr.Bl_ntfy_party_cd, _hdr.Bl_com_cd, "C");
                    if (_cusdata != null)
                    {
                        if (_cusdata.Count > 0)
                        {
                            _hdr.Bl_ntfy_party_name = _cusdata.First().mbe_name.ToString();
                            _hdr.Bl_ntfy_party_add1 = _cusdata.First().mbe_add1.ToString();
                            _hdr.Bl_ntfy_party_add2 = _cusdata.First().mbe_add2.ToString();
                        }
                    }

                }
                if (_hdr.Bl_agent_cd != "" && _hdr.Bl_agent_cd != null)
                {
                    List<GET_CUS_BASIC_DATA> _cusdata = _GenaralDAL.GetCustormerBasicData(_hdr.Bl_agent_cd, _hdr.Bl_com_cd, "C");
                    if (_cusdata != null)
                    {
                        if (_cusdata.Count > 0)
                        {
                            _hdr.Bl_del_agent_name = _cusdata.First().mbe_name.ToString();
                            _hdr.Bl_del_agent_add1 = _cusdata.First().mbe_add1.ToString();
                            _hdr.Bl_del_agent_add2 = _cusdata.First().mbe_add2.ToString();
                            _hdr.Bl_del_agent_cd = _hdr.Bl_agent_cd;
                        }
                    }

                }

                effect = _GenaralDAL.SaveBLHDR(_hdr, out seqno);
                effect = _GenaralDAL.DeleteBLContdata(seqno.ToString());
                if (_bl_det != null)
                {
                    foreach (var _bllist in _bl_det)
                    {
                        _bllist.bld_seq_no = seqno.ToString();
                        effect = _GenaralDAL.SaveDrafBLDetails(_bllist);
                    }
                }
                if (_con_det != null)
                {
                    foreach (var _con_list in _con_det)
                    {
                        _con_list.Blct_seq_no = seqno.ToString();
                        _con_list.Blct_bl_doc = _hdr.Bl_doc_no;
                        effect = _GenaralDAL.SaveDrafBLContainerDetails(_con_list);
                    }
                }
                _GenaralDAL.TransactionCommit();
            }

            catch (Exception ex)
            {
                err = ex.Message;
                _GenaralDAL.TransactionRollback();
                _GenaralDAL.ConnectionClose();
                effect = -1;
            }
            return effect;
        }
        public int SaveBLMastertHdr(trn_bl_header _hdr, List<trn_bl_det> _bl_det, List<trn_bl_cont_det> _con_det, List<HouseBLAll> _houseblall, MasterAutoNumber _masterAutoNumber, out string err)
        {
            err = "";
            Int32 seqno = 0;
            int effect = 0;
            string documentNo = _hdr.Bl_m_doc_no;
            err = documentNo;
            try
            {
                _GenaralDAL = new GenaralDAL();
                _GenaralDAL.ConnectionOpen();
                _GenaralDAL.BeginTransaction();
                //autonumber
                if (_hdr.Bl_m_doc_no == "" || _hdr.Bl_m_doc_no == null)
                {
                    Int32 _autoNo = _GenaralDAL.GetAutoNumber(_masterAutoNumber.Aut_moduleid, _masterAutoNumber.Aut_direction, _masterAutoNumber.Aut_start_char, _masterAutoNumber.Aut_cate_tp, _masterAutoNumber.Aut_cate_cd, _masterAutoNumber.Aut_modify_dt, _masterAutoNumber.Aut_year).Aut_number;
                    documentNo = _masterAutoNumber.Aut_cate_cd + "-" + _masterAutoNumber.Aut_start_char + "-" + Convert.ToString(_masterAutoNumber.Aut_year).Remove(0, 2) + "-" + _autoNo.ToString("0000", CultureInfo.InvariantCulture);
                    effect = _GenaralDAL.UpdateAutoNumber(_masterAutoNumber);
                    err = documentNo;
                    _hdr.Bl_doc_no = documentNo;
                    _hdr.Bl_m_doc_no = documentNo;
                }
                _hdr.Bl_doc_no = _hdr.Bl_m_doc_no;
                //get custormerdata
                if (_hdr.Bl_cus_cd != "" && _hdr.Bl_cus_cd != null)
                {
                    List<GET_CUS_BASIC_DATA> _cusdata = _GenaralDAL.GetCustormerBasicData(_hdr.Bl_cus_cd, _hdr.Bl_com_cd, "C");

                }
                if (_hdr.Bl_shipper_cd != "" && _hdr.Bl_shipper_cd != null)
                {
                    List<GET_CUS_BASIC_DATA> _cusdata = _GenaralDAL.GetCustormerBasicData(_hdr.Bl_shipper_cd, _hdr.Bl_com_cd, "C");
                    if (_cusdata != null)
                    {
                        if (_cusdata.Count > 0)
                        {
                            _hdr.Bl_shipper_name = _cusdata.First().mbe_name.ToString();
                            _hdr.Bl_shipper_add1 = _cusdata.First().mbe_add1.ToString();
                            _hdr.Bl_shipper_add2 = _cusdata.First().mbe_add2.ToString();
                        }
                    }

                }
                if (_hdr.Bl_consignee_cd != "" && _hdr.Bl_consignee_cd != null)
                {
                    List<GET_CUS_BASIC_DATA> _cusdata = _GenaralDAL.GetCustormerBasicData(_hdr.Bl_consignee_cd, _hdr.Bl_com_cd, "C");
                    if (_cusdata != null)
                    {
                        if (_cusdata.Count > 0)
                        {
                            _hdr.Bl_consignee_name = _cusdata.First().mbe_name.ToString();
                            _hdr.Bl_consignee_add1 = _cusdata.First().mbe_add1.ToString();
                            _hdr.Bl_consignee_add2 = _cusdata.First().mbe_add2.ToString();
                        }
                    }

                }
                if (_hdr.Bl_ntfy_party_cd != "" && _hdr.Bl_ntfy_party_cd != null)
                {
                    List<GET_CUS_BASIC_DATA> _cusdata = _GenaralDAL.GetCustormerBasicData(_hdr.Bl_ntfy_party_cd, _hdr.Bl_com_cd, "C");
                    if (_cusdata != null)
                    {
                        if (_cusdata.Count > 0)
                        {
                            _hdr.Bl_ntfy_party_name = _cusdata.First().mbe_name.ToString();
                            _hdr.Bl_ntfy_party_add1 = _cusdata.First().mbe_add1.ToString();
                            _hdr.Bl_ntfy_party_add2 = _cusdata.First().mbe_add2.ToString();
                        }
                    }

                }
                if (_hdr.Bl_agent_cd != "" && _hdr.Bl_agent_cd != null)
                {
                    List<GET_CUS_BASIC_DATA> _cusdata = _GenaralDAL.GetCustormerBasicData(_hdr.Bl_agent_cd, _hdr.Bl_com_cd, "C");
                    if (_cusdata != null)
                    {
                        if (_cusdata.Count > 0)
                        {
                            _hdr.Bl_del_agent_name = _cusdata.First().mbe_name.ToString();
                            _hdr.Bl_del_agent_add1 = _cusdata.First().mbe_add1.ToString();
                            _hdr.Bl_del_agent_add2 = _cusdata.First().mbe_add2.ToString();
                            _hdr.Bl_del_agent_cd = _hdr.Bl_agent_cd;
                        }
                    }

                }

                effect = _GenaralDAL.SaveBLHDR(_hdr, out seqno);
                effect = _GenaralDAL.DeleteBLContdata(seqno.ToString());
                if (_bl_det != null)
                {
                    foreach (var _bllist in _bl_det)
                    {
                        _bllist.bld_seq_no = seqno.ToString();
                        effect = _GenaralDAL.SaveDrafBLDetails(_bllist);
                    }
                }
                if (_con_det != null)
                {
                    foreach (var _con_list in _con_det)
                    {
                        _con_list.Blct_seq_no = seqno.ToString();
                        _con_list.Blct_bl_doc = _hdr.Bl_doc_no;
                        effect = _GenaralDAL.SaveDrafBLContainerDetails(_con_list);
                    }
                }
                if (_houseblall != null)
                {
                    foreach (var list in _houseblall)
                    {
                        // update reference
                        effect = _GenaralDAL.UpdateHouseBLMaster(list.blno, _hdr.Bl_doc_no);
                    }
                }

                _GenaralDAL.TransactionCommit();
            }

            catch (Exception ex)
            {
                err = ex.Message;
                _GenaralDAL.TransactionRollback();
                _GenaralDAL.ConnectionClose();
                effect = -1;
            }
            return effect;
        }
        //subodana 2017/06/28
        public List<trn_bl_header> GetBLHdr(string doc, string com)
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            return _GenaralDAL.GetBLHdr(doc, com);
        }
        //subodana 2017/06/28
        public List<trn_bl_det> GetBLitemdetails(string seq)
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            return _GenaralDAL.GetBLitemdetails(seq);
        }
        //subodana 2017/06/28
        public List<trn_bl_cont_det> GetBLContainer(string seq)
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            return _GenaralDAL.GetBLContainer(seq);
        }
        //subodana 2017/06/29
        public List<BL_DOC_NO> GetBLDocNo(string type, string searchtype, string docno)
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            return _GenaralDAL.GetBLDocNo(type, searchtype, docno);
        }
        //subodana 2017/06/29
        public List<GET_CUS_BASIC_DATA> GetCustormerBasicData(string code, string com, string type)
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            return _GenaralDAL.GetCustormerBasicData(code, com, type);
        }
        //subodana 2017/07/04
        public List<EntityType> GetJobEntity()
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            return _GenaralDAL.GetJobEntity();
        }
        public List<MST_CUR> GetAllCurrency(string _currencyCode)
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            return _GenaralDAL.GetAllCurrency(_currencyCode);
        }
        public TRN_MOD_MAX_APPLVL getMaxAppLvlPermission(string modcd, string com)
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            return _GenaralDAL.getMaxAppLvlPermission(modcd, com);
        }
        public List<MST_COUNTRY> getCountryDetails(string countryCd)
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            return _GenaralDAL.getCountryDetails(countryCd);
        }
        public DataTable Get_DetBy_town(string town)
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            _GenaralDAL.ConnectionOpen();
            DataTable dt = _GenaralDAL.Get_DetBy_town(town);
            return dt;
        }

        //Sanjaya De Silva 2017-7-13
        public List<trn_jb_hdr> GetCustomerJobs(string cus_code)
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            return _GenaralDAL.GetCustomerJobs(cus_code);
        }
        public DataTable getSubChannelDet(string _company, string _code)
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            return _GenaralDAL.getSubChannelDet(_company, _code);
        }

        public List<cus_invoices> getCustomerInvoices(string _cus_code)
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            return _GenaralDAL.getCustomerInvoices(_cus_code);
        }
        public List<PortAgentDet> getPortDetails(DateTime fromdate, DateTime todate, string company, out string error)
        {
            error = string.Empty;
            List<PortAgentDet> det = new List<PortAgentDet>();
            try
            {

                GenaralDAL _GenaralDAL = new GenaralDAL();
                det = _GenaralDAL.getPortDetails(fromdate, todate, company);
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }
            return det;
        }
        public DataTable getShipmentContainers(DateTime fromdate, DateTime todate, string company, string port, string agent)
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            return _GenaralDAL.getShipmentContainers(fromdate, todate, company, port, agent);
        }
        public List<BarChartDataPort> getPortTotal(DateTime frmdt, DateTime toDt, string company)
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            List<BarChartDataPort> det = new List<BarChartDataPort>();
            det = _GenaralDAL.getPortTotal(frmdt, toDt, company);
            return det;
        }
        public List<BarChartDataAgent> getAgentTotal(DateTime frmdt, DateTime toDt, string company)
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            List<BarChartDataAgent> det = new List<BarChartDataAgent>();
            det = _GenaralDAL.getAgentTotal(frmdt, toDt, company);
            return det;
        }

        // Added by Chathura on 18-sep-2017
        public List<HBLSelectedData> GetHBLNumbersForMaster(string MBLno)
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            List<HBLSelectedData> det = new List<HBLSelectedData>();
            det = _GenaralDAL.GetHBLNumbersForMaster(MBLno);
            return det;
        }
        // Added by Chathura on 3-oct-2017
        public List<BLData> LoadBLDetails(string BLno)
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            List<BLData> det = new List<BLData>();
            det = _GenaralDAL.LoadBLDetails(BLno);
            return det;
        }
        public List<CustomerBasicData> GetCustomerBasicDetails(string cuscode)
        {
            GenaralDAL _GenaralDAL = new GenaralDAL();
            List<CustomerBasicData> det = new List<CustomerBasicData>();
            det = _GenaralDAL.GetCustomerBasicDetails(cuscode);
            return det;
        }

        public Int32 SaveReportErrorLog(string _erropt, string _errform, string _error, string _user)
        {
            Int32 result = 0;
            try
            {
                _GenaralDAL = new GenaralDAL();
                _GenaralDAL.ConnectionOpen();
                _GenaralDAL.BeginTransaction();

                result = _GenaralDAL.SaveReportErrorLog(_erropt, _errform, _error, _user);

                _GenaralDAL.TransactionCommit();
                _GenaralDAL.ConnectionClose();
            }
            catch (Exception)
            {
                result = -1;
                _GenaralDAL.TransactionRollback();
            }

            return result;
        }
        public bool SendSms(string mobileNo, string message, string customerName, string sender, string company)
        {

            _GenaralDAL = new GenaralDAL();
            _GenaralDAL.ConnectionOpen();
            Int32 _saved = 0;
            string _errMsg = "";
            //string _message = string.Format("Reminder!{0}Please collect your item from {1} showroom within {2} {3} from this day, {4} will not hold responsibility for any loss, damage or disposal of the item.", Environment.NewLine, _locDesc, _period.ToString(), _periodControl, _comDesc);
            try
            {
                if (string.IsNullOrEmpty(mobileNo))
                {
                    _errMsg = "Receiver mobile number not found !";
                }
                else if (string.IsNullOrEmpty(message))
                {
                    _errMsg = "SMS body is empty !";
                }
                else if (mobileNo.Length >= 9)
                {
                    OutSMS _out = new OutSMS();
                    _out.Msg = message;
                    _out.Msgstatus = 0;
                    _out.Msgtype = "S";
                    _out.Receivedtime = DateTime.Now;
                    _out.Receiver = customerName; //customer name or code
                    //_out._deletedtime = DateTime.Now;
                    //_out._deliveredtime = DateTime.Now;
                    //_out._downloadtime = DateTime.Now;
                    //_out._msgid = "";
                    //_out.Seqno = 1;
                    //_out._receivedtime = DateTime.Now;

                    if (mobileNo.Length == 10)
                    {
                        _out.Receiverphno = "+94" + mobileNo.Substring(1, 9);
                        _out.Senderphno = "+94" + mobileNo.Substring(1, 9); //mobile number
                    }
                    if (mobileNo.Length == 9)
                    {
                        _out.Receiverphno = "+94" + mobileNo;
                        _out.Senderphno = "+94" + mobileNo;
                    }

                    _out.Refdocno = "";
                    _out.Sender = sender; //send by 
                    _out.Createtime = DateTime.Now;
                    _out.comcode = company;
                    _out.Msgid = "";

                    _saved = _GenaralDAL.SaveSMSOut(_out);
                }
                else
                {
                    _errMsg = "Invalid mobile number !";
                }
                if (_saved > 0)
                {
                    _GenaralDAL.TransactionCommit();
                    _GenaralDAL.ConnectionClose();
                    return true;
                }
                else
                {
                    _GenaralDAL.TransactionCommit();
                    _GenaralDAL.TransactionRollback();
                    return false;
                }
            }
            catch (Exception ex)
            {
                _GenaralDAL.TransactionCommit();
                _GenaralDAL.TransactionRollback();
                return false;
            }
        }

        public List<USER_ROLE> getUserRoleID(string company, string roleid)
        {
            GenaralDAL _generalSearchDAL = new GenaralDAL();
            return _generalSearchDAL.getUserRoleID(company, roleid);
        }

        public List<UsersRoleData> getUserDetailsByRID(string company, string roleid)
        {
            GenaralDAL _generalSearchDAL = new GenaralDAL();
            return _generalSearchDAL.getUserDetailsByRID(company, roleid);
        }

        public List<ROLE_MENU_BRID> getMenuDetailsByRID(string userid, string company, string roleid)
        {
            GenaralDAL _generalSearchDAL = new GenaralDAL();
            return _generalSearchDAL.getMenuDetailsByRID(userid, company, roleid);
        }


        public List<OptionId_Details> getOptions(string company, string roleid)
        {
            GenaralDAL _generalSearchDAL = new GenaralDAL();
            return _generalSearchDAL.getOptions(company, roleid);
        }


        public int Save_Option_IDS(List<string> OptionStatList, string company, string roleid, List<string> OptionIdList, out string err)
        {
            int val = 0;
            err = "";
            err = string.Empty;
            GenaralDAL _generalDAL = new GenaralDAL();
            try
            {

                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();

                for (int i = 0; i < OptionIdList.Count; i++)
                {
                    val = _generalDAL.Save_Option_IDS(OptionStatList[i].ToString(), company, roleid, OptionIdList[i].ToString());
                }
                _generalDAL.TransactionCommit();
                _generalDAL.ConnectionClose();

            }
            catch (Exception ex)
            {
                _generalDAL.TransactionRollback();
                err = ex.Message;
            }
            return val;
        }

        public List<USER_COMPANY_LIST> getUserListCom(string userid)
        {
            GenaralDAL _generalSearchDAL = new GenaralDAL();
            return _generalSearchDAL.getUserListCom(userid);
        }

        public List<MST_SHIPMENTS> getShipmentList(string company)
        {
            GenaralDAL _generalSearchDAL = new GenaralDAL();
            return _generalSearchDAL.getShipmentList(company);
        }

        public int Update_Modeshipment_Details(string company,List<string> ModeList,string ShipDesc, List<string> StatList,string choice, out string err)
        {
            int val = 0;
            err = "";
            err = string.Empty;
            GenaralDAL _generalDAL = new GenaralDAL();
            try
            {

                _generalDAL.ConnectionOpen();
                _generalDAL.BeginTransaction();

                for (int i = 0; i < ModeList.Count; i++)
                {
                    val = _generalDAL.Update_Modeshipment_Details(company, ModeList[i].ToString(), ShipDesc, StatList[i].ToString(), choice);
                }
                _generalDAL.TransactionCommit();
                _generalDAL.ConnectionClose();

            }
            catch (Exception ex)
            {
                _generalDAL.TransactionRollback();
                err = ex.Message;
            }
            return val;
        }

    }

}
