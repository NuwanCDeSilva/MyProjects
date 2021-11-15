using System;
using System.Collections.Generic;
using System.Data;
using FF.BusinessObjects;
using Oracle.DataAccess.Client;
using FF.BusinessObjects.CustService;
using FF.BusinessObjects.Services;
using FF.BusinessObjects.TempObject;

namespace FF.DataAccessLayer
{
    public class CustServiceDAL : BaseDAL
    {
        public DataTable Get_oldpart_byjob(string _com, string _job)
        {
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (_para[1] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _job;
            _para[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "sp_get_oldpart_byjob", CommandType.StoredProcedure, false, _para);
            return _dtResults;
        }
        public DataTable InsuranceForServiceReport(string _com, string _pc, DateTime _from, DateTime _to)
        {   //kapila 3/7/2017
            DataTable _dtResults = null;
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("in_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            (param[2] = new OracleParameter("fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _from;
            (param[3] = new OracleParameter("tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _to;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _dtResults = QueryDataTable("tbl", "sp_insurance_ser_loc", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        //kapila
        public Int32 SaveRccDetail(RCC_Det _rccdet)
        {
            OracleParameter[] param = new OracleParameter[12];

            (param[0] = new OracleParameter("p_inrd_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _rccdet.Inrd_no;
            (param[1] = new OracleParameter("p_inrd_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _rccdet.Inrd_line;
            (param[2] = new OracleParameter("p_inrd_inv_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _rccdet.Inrd_inv_no;
            (param[3] = new OracleParameter("p_inrd_acc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _rccdet.Inrd_acc_no;
            (param[4] = new OracleParameter("p_inrd_inv_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _rccdet.Inrd_inv_dt;
            (param[5] = new OracleParameter("p_inrd_oth_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _rccdet.Inrd_oth_doc_no;
            (param[6] = new OracleParameter("p_inrd_oth_doc_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _rccdet.Inrd_oth_doc_dt;
            (param[7] = new OracleParameter("p_inrd_itm", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _rccdet.Inrd_itm;
            (param[8] = new OracleParameter("p_inrd_qty", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _rccdet.Inrd_qty;
            (param[9] = new OracleParameter("p_inrd_ser", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _rccdet.Inrd_ser;
            (param[10] = new OracleParameter("p_inrd_warr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _rccdet.Inrd_warr;

            param[11] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            Int32 effects;
            effects = (Int16)UpdateRecords("sp_save_rcc_det", CommandType.StoredProcedure, param);
            return effects;
        }
        public DataTable get_receipt_byjobno(string _com, string _jobno)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("P_job", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobno;

            param[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "sp_getreceiptbyjobno", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        public DataTable Get_Job_Item_grade_Val(string _com, DateTime _date,string _item)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("P_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = _date;
            (param[2] = new OracleParameter("P_itm", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "sp_get_item_gv_grade", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }
        public DataTable get_gatepass_byjob(string _job,DateTime _date)
        {   // kapila
            OracleParameter[] param = new OracleParameter[3];

            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("p_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _job;
            (param[2] = new OracleParameter("p_jobno", OracleDbType.Date, null, ParameterDirection.Input)).Value = _date;

            DataTable _dtResults = QueryDataTable("tblRcc", "sp_get_gatepass_byjob", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public Int32 cancel_gate_pass(string _jobno, string _rccno)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobno;
            (param[1] = new OracleParameter("p_rccno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _rccno;
            
            param[2] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            Int32 effects;
            effects = (Int16)UpdateRecords("sp_cancel_gate_pass", CommandType.StoredProcedure, param);
            return effects;
        }

        public DataTable sp_get_pcbyloc_details(string _com, string _loc)
        {   // kapila
            OracleParameter[] param = new OracleParameter[3];

            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[2] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;

            DataTable _dtResults = QueryDataTable("tblRcc", "sp_get_pcbyloc", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        //kapila
        public DataTable get_SCV_CLS_ALW_LOC(string _com, string _loc, string _type,Int32 _isDef,string _alwloc)
        {
            DataTable _dtResults = null;
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            (param[2] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _type;
            (param[3] = new OracleParameter("p_is_def", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _isDef;
            (param[4] = new OracleParameter("p_alwloc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alwloc;
            param[5] = new OracleParameter("n_Cursor", OracleDbType.RefCursor, null, ParameterDirection.Output);

            _dtResults = QueryDataTable("tbl", "sp_get_CLS_ALW_LOC", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        //kapila
        public List<InvoiceItem> GetSCMInvDetails(string _invoice, string _itm, string _ser)
        {
            List<InvoiceItem> _list = null;
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_inv", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoice;
            (param[1] = new OracleParameter("p_itm", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itm;
            (param[2] = new OracleParameter("p_ser", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ser;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblData = QueryDataTable("tbData", "sp_getscminvdetail", CommandType.StoredProcedure, false, param);
            if (_tblData.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<InvoiceItem>(_tblData, InvoiceItem.ConvertInvoiceItemForReversal);
            }
            return _list;

        }
        //kapila 26/2/2016
        public DataTable GetAcceptPendingJobs(string _com, string _pc,string _ser,string _job,string _tech,string _chnl,Int32 _istech,decimal _stage)
        {
            DataTable _dtResults = null;
            OracleParameter[] param = new OracleParameter[9];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            (param[2] = new OracleParameter("p_ser", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ser;
            (param[3] = new OracleParameter("p_job", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _job;
            (param[4] = new OracleParameter("p_tech", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _tech;
            (param[5] = new OracleParameter("p_chnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _chnl;
            (param[6] = new OracleParameter("p_istech", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _istech;
            (param[7] = new OracleParameter("p_stage", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _stage;
            param[8] = new OracleParameter("n_Cursor", OracleDbType.RefCursor, null, ParameterDirection.Output);

            _dtResults = QueryDataTable("tbl", "sp_get_accept_pend_jobs", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        //kapila 24/2/2016
        public DataTable GetPendingAcceptanceStatus(string _com, string _loc,string _cat)
        {
            DataTable _dtResults = null;
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            (param[2] = new OracleParameter("p_cat", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat;
            param[3] = new OracleParameter("n_Cursor", OracleDbType.RefCursor, null, ParameterDirection.Output);

            _dtResults = QueryDataTable("tbl", "sp_get_pend_accept_stus", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        //kapila
        public DataTable getTransportMethod()
        {
            OracleParameter[] param = new OracleParameter[1];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_TRANS_METHOD", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        //kapila
        public Int32 SaveJobImagePath(string _job, Int32 _jobline,string _ser, Int32 _imgline, string _imgpath, string _imgname)
        {
            OracleParameter[] param = new OracleParameter[7];
            Int32 effects = 0;
            (param[0] = new OracleParameter("p_job", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _job;
            (param[1] = new OracleParameter("p_jobline", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobline;
            (param[2] = new OracleParameter("P_imgline", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _imgline;
            (param[3] = new OracleParameter("P_ser", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ser;
            (param[4] = new OracleParameter("P_imgpath", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _imgpath;
            (param[5] = new OracleParameter("P_imgname", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _imgname;
            param[6] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_SAVE_JOB_IMAGE_PATH", CommandType.StoredProcedure, param);
            return effects;
        }
        //kapila
        public DataTable getNotAllocatedJobs(string _com, string _loc, DateTime _fdate, DateTime _tdate)
        {

            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            (param[2] = new OracleParameter("p_fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fdate;
            (param[3] = new OracleParameter("p_tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _tdate;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _result = QueryDataTable("tbl", "sp_get_not_alloc_jobs", CommandType.StoredProcedure, false, param);

            return _result;
        }
        //kapila
        public DataTable getAllocatedPendingJobs(string _com, string _empCd, Int32 _istech, string _chnl, DateTime _fdate, DateTime _tdate)
        {

            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_empcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _empCd;
            (param[2] = new OracleParameter("p_istech", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _istech;
            (param[3] = new OracleParameter("p_chnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _chnl;
            (param[4] = new OracleParameter("p_fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fdate;
            (param[5] = new OracleParameter("p_tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _tdate;
            param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _result = QueryDataTable("tbl", "sp_get_alloc_pend_jobs", CommandType.StoredProcedure, false, param);

            return _result;
        }

        //kapila
        public Int32 UPD_TMP_ISSUE_RET(string _com,string _loc,string _item,string _ser,string _doctp)
        {
            OracleParameter[] param = new OracleParameter[6];
            Int32 effects = 0;
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            (param[2] = new OracleParameter("P_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[3] = new OracleParameter("P_ser", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ser;
            (param[4] = new OracleParameter("P_doctp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _doctp;
            param[5] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPD_TMP_ISSUE_RET", CommandType.StoredProcedure, param);
            return effects;
        }
        //kapila
        public Boolean IsWarReplaceFound(string _jobno)
        {
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("p_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobno;
            _para[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            Boolean _Found = QueryDataTable("tbl", "sp_chk_war_replace", CommandType.StoredProcedure, false, _para).Rows.Count > 0 ? true : false;

            return _Found;
        }

        public Boolean IsWarReplaceFound_Exchnge(string _jobno)
        {
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("p_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobno;
            _para[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            Boolean _Found = QueryDataTable("tbl", "sp_chk_war_replace_EXCHG", CommandType.StoredProcedure, false, _para).Rows.Count > 0 ? true : false;

            return _Found;
        }
        public Int32 upd_brnd_man_alloc(string _com, string _man)
        {
            OracleParameter[] param = new OracleParameter[3];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("P_man", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _man;

            param[2] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_upd_brnd_man_alloc", CommandType.StoredProcedure, param);
            return effects;
        }

        public DataTable getCreditNote4PendingSRN(string _com, DateTime _from, DateTime _to, string _delLoc, string _custCd, string _invno, Int32 _isDelAny)
        {
            OracleParameter[] param = new OracleParameter[8];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_from", OracleDbType.Date, null, ParameterDirection.Input)).Value = _from;
            (param[2] = new OracleParameter("p_to", OracleDbType.Date, null, ParameterDirection.Input)).Value = _to;
            (param[3] = new OracleParameter("p_delloc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _delLoc;
            (param[4] = new OracleParameter("p_cust", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _custCd;
            (param[5] = new OracleParameter("p_inv", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invno;
            (param[6] = new OracleParameter("p_isanyloc", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _isDelAny;
            param[7] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _itemTable = new DataTable();
            _itemTable = QueryDataTable("tblserial", "sp_get_pending_CreditNote", CommandType.StoredProcedure, false, param);
            return _itemTable;
        }
        public DataTable GetAgrClaimType(string _code)
        {   //kapila
            DataTable _dtResults = null;
            OracleParameter[] param = new OracleParameter[2];

            (param[0] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _code;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            _dtResults = QueryDataTable("tbl", "sp_get_agr_claimtp", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        public DataTable getSCVAGRITM_bySer(string _ser)
        {   //kapila
            DataTable _dtResults = null;
            OracleParameter[] param = new OracleParameter[2];

            (param[0] = new OracleParameter("p_ser", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ser;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            _dtResults = QueryDataTable("tbl", "sp_get_scvagritmbyser", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        public DataTable GetAgrType(string _code)
        {   //kapila
            DataTable _dtResults = null;
            OracleParameter[] param = new OracleParameter[2];

            (param[0] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _code;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            _dtResults = QueryDataTable("tbl", "sp_get_agr_type", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable getJobTechnician(string _jobno, string _com)
        {   //Sanjeewa
            DataTable _dtResults = null;

            OracleParameter[] param = new OracleParameter[3];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobno;
            (param[2] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;

            _dtResults = QueryDataTable("tbl", "sp_get_tech_alloc_names", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        //public Int32 SaveAgreementHeader(SCV_AGR_HDR _agrHdr)
        //{
        //    OracleParameter[] param = new OracleParameter[38];
        //    Int32 effects = 0;

        //    (param[0] = new OracleParameter("p_SAG_SEQ_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _agrHdr.Sag_seq_no;
        //    (param[1] = new OracleParameter("p_SAG_AGR_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrHdr.Sag_agr_no;
        //    (param[2] = new OracleParameter("p_SAG_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrHdr.Sag_com;
        //    (param[3] = new OracleParameter("p_SAG_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrHdr.Sag_tp;
        //    (param[4] = new OracleParameter("p_SAG_CLM_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrHdr.Sag_clm_tp;
        //    (param[5] = new OracleParameter("p_SAG_CHNL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrHdr.Sag_chnl;
        //    (param[6] = new OracleParameter("p_SAG_SCHNL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrHdr.Sag_schnl;
        //    (param[7] = new OracleParameter("p_SAG_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrHdr.Sag_pc;
        //    (param[8] = new OracleParameter("p_SAG_MULTI_PC", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _agrHdr.Sag_multi_pc;
        //    (param[9] = new OracleParameter("p_SAG_COMMDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _agrHdr.Sag_commdt;
        //    (param[10] = new OracleParameter("p_SAG_EXDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _agrHdr.Sag_exdt;
        //    (param[11] = new OracleParameter("p_SAG_REWLDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _agrHdr.Sag_rewldt;
        //    (param[12] = new OracleParameter("p_SAG_CUSTCD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrHdr.Sag_custcd;
        //    (param[13] = new OracleParameter("p_SAG_CUST_TOWN", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrHdr.Sag_cust_town;
        //    (param[14] = new OracleParameter("p_SAG_TOWN_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrHdr.Sag_town_rmk;
        //    (param[15] = new OracleParameter("p_SAG_CONT_PERSON", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrHdr.Sag_cont_person;
        //    (param[16] = new OracleParameter("p_SAG_CONT_ADD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrHdr.Sag_cont_add;
        //    (param[17] = new OracleParameter("p_SAG_CONT_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrHdr.Sag_cont_no;
        //    (param[18] = new OracleParameter("p_SAG_MANUALREF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrHdr.Sag_manualref;
        //    (param[19] = new OracleParameter("p_SAG_OTHERREF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrHdr.Sag_otherref;
        //    (param[20] = new OracleParameter("p_SAG_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrHdr.Sag_rmk;
        //    (param[21] = new OracleParameter("p_SAG_GL_DEBTOR_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrHdr.Sag_gl_debtor_cd;
        //    (param[22] = new OracleParameter("p_SAG_TOT_AMT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _agrHdr.Sag_tot_amt;
        //    (param[23] = new OracleParameter("p_SAG_SET_AMT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _agrHdr.Sag_set_amt;
        //    (param[24] = new OracleParameter("p_SAG_PERIOD_BASIS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrHdr.Sag_period_basis;
        //    (param[25] = new OracleParameter("p_SAG_SER_ATTEMPT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _agrHdr.Sag_ser_attempt;
        //    (param[26] = new OracleParameter("p_SAG_TOP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrHdr.Sag_top;
        //    (param[27] = new OracleParameter("p_SAG_TAC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrHdr.Sag_tac;
        //    (param[28] = new OracleParameter("p_SAG_WORK_INC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrHdr.Sag_work_inc;
        //    (param[29] = new OracleParameter("p_SAG_SVC_FREQ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrHdr.Sag_svc_freq;
        //    (param[30] = new OracleParameter("p_SAG_PERIOD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrHdr.Sag_period;
        //    (param[31] = new OracleParameter("p_SAG_STUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrHdr.Sag_stus;
        //    (param[32] = new OracleParameter("p_SAG_STUS_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrHdr.Sag_stus_rmk;
        //    (param[33] = new OracleParameter("p_SAG_AUTHOBY1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrHdr.Sag_authoby1;
        //    (param[34] = new OracleParameter("p_SAG_AUTHOBY2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrHdr.Sag_authoby2;
        //    (param[35] = new OracleParameter("p_SAG_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrHdr.Sag_cre_by;
        //    (param[36] = new OracleParameter("p_SAG_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrHdr.Sag_mod_by;


        //    param[37] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

        //    effects = (Int16)UpdateRecords("sp_save_scv_agr_hdr", CommandType.StoredProcedure, param);
        //    return effects;
        //}

        //public Int32 SaveAgreementItem(SCV_AGR_ITM _agrItm)
        //{
        //    OracleParameter[] param = new OracleParameter[19];
        //    Int32 effects = 0;

        //    (param[0] = new OracleParameter("p_SAI_SEQ_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _agrItm.Sai_seq_no;
        //    (param[1] = new OracleParameter("p_SAI_AGR_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrItm.Sai_agr_no;
        //    (param[2] = new OracleParameter("p_SAI_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _agrItm.Sai_line;
        //    (param[3] = new OracleParameter("p_SHI_ITM_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrItm.Shi_itm_cd;
        //    (param[4] = new OracleParameter("p_SHI_ITM_DESC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrItm.Shi_itm_desc;
        //    (param[5] = new OracleParameter("p_SHI_BRAND", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrItm.Shi_brand;
        //    (param[6] = new OracleParameter("p_SHI_MODEL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrItm.Shi_model;
        //    (param[7] = new OracleParameter("p_SHI_SER1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrItm.Shi_ser1;
        //    (param[8] = new OracleParameter("p_SHI_SER2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrItm.Shi_ser2;
        //    (param[9] = new OracleParameter("p_SHI_SER_ID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrItm.Shi_ser_id;
        //    (param[10] = new OracleParameter("p_SHI_WARRNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrItm.Shi_warrno;
        //    (param[11] = new OracleParameter("p_SHI_WARR_STUS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _agrItm.Shi_warr_stus;
        //    (param[12] = new OracleParameter("p_SHI_REGNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrItm.Shi_regno;
        //    (param[13] = new OracleParameter("p_SHI_CATE1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrItm.Shi_cate1;
        //    (param[14] = new OracleParameter("p_SHI_CATE2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrItm.Shi_cate2;
        //    (param[15] = new OracleParameter("p_SHI_CATE3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrItm.Shi_cate3;
        //    (param[16] = new OracleParameter("p_SHI_QTY", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _agrItm.Shi_qty;
        //    (param[17] = new OracleParameter("p_SHI_SESSIONS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _agrItm.Shi_sessions;

        //    param[18] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

        //    effects = (Int16)UpdateRecords("sp_save_scv_agr_itm", CommandType.StoredProcedure, param);
        //    return effects;
        //}

        //public Int32 SaveAgreementSess(SCV_AGR_SES _agrSes)
        //{
        //    OracleParameter[] param = new OracleParameter[8];
        //    Int32 effects = 0;

        //    (param[0] = new OracleParameter("p_SAGA_SEQ_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _agrSes.Saga_seq_no;
        //    (param[1] = new OracleParameter("p_SAGA_AGR_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrSes.Saga_agr_no;
        //    (param[2] = new OracleParameter("p_SAGA_ITM_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _agrSes.Saga_itm_line;
        //    (param[3] = new OracleParameter("p_SAGA_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _agrSes.Saga_line;
        //    (param[4] = new OracleParameter("p_SAGA_FROM_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _agrSes.Saga_from_dt;
        //    (param[5] = new OracleParameter("p_SAGA_TO_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _agrSes.Saga_to_dt;
        //    (param[6] = new OracleParameter("p_SAGA_STUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrSes.Saga_stus;


        //    param[7] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

        //    effects = (Int16)UpdateRecords("sp_save_scv_agr_ses", CommandType.StoredProcedure, param);
        //    return effects;
        //}

        //public Int32 SaveAgreementCoverItem(SCV_AGR_COVER_ITM _agrCov)
        //{
        //    OracleParameter[] param = new OracleParameter[6];
        //    Int32 effects = 0;

        //    (param[0] = new OracleParameter("p_SAIC_SEQ_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _agrCov.Saic_seq_no;
        //    (param[1] = new OracleParameter("p_SAIC_AGR_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrCov.Saic_agr_no;
        //    (param[2] = new OracleParameter("p_SAIC_ITM_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _agrCov.Saic_itm_line;
        //    (param[3] = new OracleParameter("p_SAIC_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _agrCov.Saic_line;
        //    (param[4] = new OracleParameter("p_SAIC_ITM_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrCov.Saic_itm_cd;

        //    param[5] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

        //    effects = (Int16)UpdateRecords("sp_save_scv_agr_covitm", CommandType.StoredProcedure, param);
        //    return effects;
        //}

        public Int32 updateAgreement(string _agrno, string _stus, string _user)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_agrno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrno;
            (param[1] = new OracleParameter("p_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _stus;
            (param[2] = new OracleParameter("p_modby", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            param[3] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            Int32 effects;
            effects = (Int16)UpdateRecords("sp_upd_scv_agr", CommandType.StoredProcedure, param);
            return effects;
        }

        //kapila
        public int DeleteSatisVal(Int32 _seq, Int32 _line)
        {
            int row_aff = 0;
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _seq;
            (param[1] = new OracleParameter("p_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _line;
            param[2] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            row_aff = UpdateRecords("sp_del_satis_val", CommandType.StoredProcedure, param);
            return row_aff;
        }

        public DataTable JobSummaryDetails(DateTime _from, DateTime _to, string _com, string _loc, string in_Itemcode, string in_Brand, string in_Model, string in_Itemcat1,
            string in_Itemcat2, string in_Itemcat3, string _technician, string _jobcat, string _itemtp, Int16 _jobstatus, Int16 _warrstatus, string _jobno, string _user)
        {   //Sanjeewa 2015-04-21
            DataTable _dtResults = null;
            OracleParameter[] param = new OracleParameter[18];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _from;
            (param[2] = new OracleParameter("tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _to;
            (param[3] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[4] = new OracleParameter("in_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            (param[5] = new OracleParameter("in_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcode;
            (param[6] = new OracleParameter("in_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Brand;
            (param[7] = new OracleParameter("in_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Model;
            (param[8] = new OracleParameter("in_cat1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcat1;
            (param[9] = new OracleParameter("in_cat2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcat2;
            (param[10] = new OracleParameter("in_cat3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcat3;
            (param[11] = new OracleParameter("in_technician", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _technician;
            (param[12] = new OracleParameter("in_job_cat", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobcat;
            (param[13] = new OracleParameter("in_itm_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemtp;
            (param[14] = new OracleParameter("in_job_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobstatus;
            (param[15] = new OracleParameter("in_warr_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warrstatus;
            (param[16] = new OracleParameter("in_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobno;
            (param[17] = new OracleParameter("in_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;

            _dtResults = QueryDataTable("tbl", "sp_get_scv_job_details", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable DFExchangeDetails(DateTime _from, DateTime _to, string _com, string _pc, int _isExport, string _reqtp)
        {   //Sanjeewa 2015-06-27
            DataTable _dtResults = null;
            OracleParameter[] param = new OracleParameter[7];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _from;
            (param[2] = new OracleParameter("tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _to;
            (param[3] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[4] = new OracleParameter("in_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            (param[5] = new OracleParameter("in_export", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _isExport;
            (param[6] = new OracleParameter("in_retp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqtp;

            _dtResults = QueryDataTable("tbl", "sp_get_df_exchg_detail", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable AgreementDetailsReport(DateTime _from, DateTime _to, string _com, string _loc, string _user)
        {   //Sanjeewa 2017-04-17
            DataTable _dtResults = null;
            OracleParameter[] param = new OracleParameter[6];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _from;
            (param[2] = new OracleParameter("tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _to;
            (param[3] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[4] = new OracleParameter("in_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            (param[5] = new OracleParameter("in_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;

            _dtResults = QueryDataTable("tbl", "sp_get_agree_stus_rep", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable JobProcessTrackingDetails(DateTime _from, DateTime _to, string _com, string _loc, string in_Itemcode, string in_Brand, string in_Model, string in_Itemcat1,
            string in_Itemcat2, string in_Itemcat3, string _technician, string _jobcat, string _itemtp, decimal _jobstatus, string _warrstatus, string _jobno, string _user, string _option, int _export, string _origin, int _chkw_cr_time, DateTime _create_from, DateTime _create_to)
        {   //Sanjeewa 2015-04-22
            DataTable _dtResults = null;
            OracleParameter[] param = new OracleParameter[24];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _from;
            (param[2] = new OracleParameter("tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _to;
            (param[3] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[4] = new OracleParameter("in_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            (param[5] = new OracleParameter("in_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcode;
            (param[6] = new OracleParameter("in_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Brand;
            (param[7] = new OracleParameter("in_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Model;
            (param[8] = new OracleParameter("in_cat1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcat1;
            (param[9] = new OracleParameter("in_cat2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcat2;
            (param[10] = new OracleParameter("in_cat3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcat3;
            (param[11] = new OracleParameter("in_technician", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _technician;
            (param[12] = new OracleParameter("in_job_cat", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobcat;
            (param[13] = new OracleParameter("in_itm_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemtp;
            (param[14] = new OracleParameter("in_job_status", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _jobstatus;
            (param[15] = new OracleParameter("in_warr_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warrstatus;
            (param[16] = new OracleParameter("in_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobno;
            (param[17] = new OracleParameter("in_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            (param[18] = new OracleParameter("in_option", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _option;
            (param[19] = new OracleParameter("in_export", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _export;
            (param[20] = new OracleParameter("in_option", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _origin;
            (param[21] = new OracleParameter("in_chkw_cr_time", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _chkw_cr_time;
            (param[22] = new OracleParameter("f_cr_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = _create_from;
            (param[23] = new OracleParameter("t_cr_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = _create_to;         


            _dtResults = QueryDataTable("tbl", "sp_get_job_process_tracking", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable JobDetails(DateTime _from, DateTime _to, string _com, string _loc, string in_Itemcode, string in_Brand, string in_Model, string in_Itemcat1,
          string in_Itemcat2, string in_Itemcat3, string _technician, string _jobcat, string _itemtp, decimal _jobstatus, string _warrstatus, string _jobno, string _user)
        {   //Sanjeewa 2015-09-09
            DataTable _dtResults = null;
            OracleParameter[] param = new OracleParameter[18];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _from;
            (param[2] = new OracleParameter("tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _to;
            (param[3] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[4] = new OracleParameter("in_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            (param[5] = new OracleParameter("in_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcode;
            (param[6] = new OracleParameter("in_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Brand;
            (param[7] = new OracleParameter("in_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Model;
            (param[8] = new OracleParameter("in_cat1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcat1;
            (param[9] = new OracleParameter("in_cat2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcat2;
            (param[10] = new OracleParameter("in_cat3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcat3;
            (param[11] = new OracleParameter("in_technician", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _technician;
            (param[12] = new OracleParameter("in_job_cat", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobcat;
            (param[13] = new OracleParameter("in_itm_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemtp;
            (param[14] = new OracleParameter("in_job_status", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _jobstatus;
            (param[15] = new OracleParameter("in_warr_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warrstatus;
            (param[16] = new OracleParameter("in_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobno;
            (param[17] = new OracleParameter("in_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;

            _dtResults = QueryDataTable("tbl", "sp_get_job_detail", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable JobTimeDetails(DateTime _from, DateTime _to, string _com, string _loc, string in_Itemcode, string in_Brand, string in_Model, string in_Itemcat1,
            string in_Itemcat2, string in_Itemcat3, string _user, string _defect, string _serial)
        {   //Sanjeewa 2016-01-25
            DataTable _dtResults = null;
            OracleParameter[] param = new OracleParameter[14];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _from;
            (param[2] = new OracleParameter("tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _to;
            (param[3] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[4] = new OracleParameter("in_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            (param[5] = new OracleParameter("in_itemcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcode;
            (param[6] = new OracleParameter("in_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Brand;
            (param[7] = new OracleParameter("in_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Model;
            (param[8] = new OracleParameter("in_itemcat1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcat1;
            (param[9] = new OracleParameter("in_itemcat2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcat2;
            (param[10] = new OracleParameter("in_itemcat3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcat3;
            (param[11] = new OracleParameter("in_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            (param[12] = new OracleParameter("in_defect", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _defect;
            (param[13] = new OracleParameter("in_serial", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial;

            _dtResults = QueryDataTable("tbl", "sp_get_job_time2", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable SmartInsuClaimDetails(DateTime _from, DateTime _to, string _com, string _loc, string in_Itemcode, string in_Brand, string in_Model, string in_Itemcat1, string in_Itemcat2, string in_Itemcat3)
        {   //Sanjeewa 2015-08-26
            DataTable _dtResults = null;
            OracleParameter[] param = new OracleParameter[11];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _from;
            (param[2] = new OracleParameter("tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _to;
            (param[3] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[4] = new OracleParameter("in_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            (param[5] = new OracleParameter("in_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcode;
            (param[6] = new OracleParameter("in_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Brand;
            (param[7] = new OracleParameter("in_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Model;
            (param[8] = new OracleParameter("in_cat1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcat1;
            (param[9] = new OracleParameter("in_cat2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcat2;
            (param[10] = new OracleParameter("in_cat3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcat3;

            _dtResults = QueryDataTable("tbl", "sp_get_smrt_insu_claim_dtl", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable BERLetterDetails(string _jobno, string _type)
        {   //Sanjeewa 2015-05-16
            DataTable _dtResults = null;
            OracleParameter[] param = new OracleParameter[3];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("in_Jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobno;
            (param[2] = new OracleParameter("in_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _type;

            _dtResults = QueryDataTable("tbl", "sp_get_ber_letter_print", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable JobProcesses()
        {
            DataTable _dtResults = null;
            OracleParameter[] param = new OracleParameter[1];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _dtResults = QueryDataTable("tbl", "sp_get_job_processes", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable PrintTechComments(string _com, string _pc, DateTime _from, DateTime _to, string _user, string in_Brand, string in_Model, string in_Itemcode, string in_Itemcat1, string in_Itemcat2, string in_Itemcat3, string _job, string _coment, string _jobcat)
        {
            DataTable _dtResults = null;
            OracleParameter[] param = new OracleParameter[15];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_from", OracleDbType.Date, null, ParameterDirection.Input)).Value = _from;
            (param[2] = new OracleParameter("p_to", OracleDbType.Date, null, ParameterDirection.Input)).Value = _to;
            (param[3] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            (param[4] = new OracleParameter("in_Brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Brand;
            (param[5] = new OracleParameter("in_Model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Model;
            (param[6] = new OracleParameter("in_Itemcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcode;
            (param[7] = new OracleParameter("in_Itemcat1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcat1;
            (param[8] = new OracleParameter("in_Itemcat2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcat2;
            (param[9] = new OracleParameter("in_Itemcat3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcat3;
            (param[10] = new OracleParameter("p_job", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _job;
            (param[11] = new OracleParameter("p_coment", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _coment;
            (param[12] = new OracleParameter("p_jobcat", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobcat;
            (param[13] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            param[14] = new OracleParameter("n_Cursor", OracleDbType.RefCursor, null, ParameterDirection.Output);

            _dtResults = QueryDataTable("tbl", "pkg_reports.sp_print_tech_comment", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable PrintRepeatedJobs(string _com, string _pc, DateTime _from, DateTime _to, string _user, string in_Brand, string in_Model, string in_Itemcode, string in_Itemcat1, string in_Itemcat2, string in_Itemcat3, string _ser, string _jobcat, Int32 _jobstus)
        {
            DataTable _dtResults = null;
            OracleParameter[] param = new OracleParameter[15];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_from", OracleDbType.Date, null, ParameterDirection.Input)).Value = _from;
            (param[2] = new OracleParameter("p_to", OracleDbType.Date, null, ParameterDirection.Input)).Value = _to;
            (param[3] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            (param[4] = new OracleParameter("in_Brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Brand;
            (param[5] = new OracleParameter("in_Model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Model;
            (param[6] = new OracleParameter("in_Itemcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcode;
            (param[7] = new OracleParameter("in_Itemcat1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcat1;
            (param[8] = new OracleParameter("in_Itemcat2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcat2;
            (param[9] = new OracleParameter("in_Itemcat3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcat3;
            (param[10] = new OracleParameter("in_ser", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ser;
            (param[11] = new OracleParameter("p_jobcat", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobcat;
            (param[12] = new OracleParameter("p_jobstus", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobstus;

            (param[13] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            param[14] = new OracleParameter("n_Cursor", OracleDbType.RefCursor, null, ParameterDirection.Output);

            _dtResults = QueryDataTable("tbl", "pkg_reports.sp_print_repeat_jobs", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable DefectAnalysisDetails(DateTime _from, DateTime _to, string _com, string _loc, string in_Itemcode, string in_Brand, string in_Model, string in_Itemcat1, string in_Itemcat2, string in_Itemcat3, string in_defect, string in_Warrstatus, string _user)
        { //Sanjeewa 2015-04-27
            DataTable _dtResults = null;
            OracleParameter[] param = new OracleParameter[14];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("in_from", OracleDbType.Date, null, ParameterDirection.Input)).Value = _from;
            (param[2] = new OracleParameter("in_to", OracleDbType.Date, null, ParameterDirection.Input)).Value = _to;
            (param[3] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[4] = new OracleParameter("in_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            (param[5] = new OracleParameter("in_Itemcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcode;
            (param[6] = new OracleParameter("in_Brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Brand;
            (param[7] = new OracleParameter("in_Model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Model;
            (param[8] = new OracleParameter("in_Itemcat1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcat1;
            (param[9] = new OracleParameter("in_Itemcat2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcat2;
            (param[10] = new OracleParameter("in_Itemcat3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcat3;
            (param[11] = new OracleParameter("in_defect", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_defect;
            (param[12] = new OracleParameter("in_warrstatus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Warrstatus;
            (param[13] = new OracleParameter("in_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;

            _dtResults = QueryDataTable("tbl", "sp_get_defect_analysis", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        //kapila
        public Int32 Save_Supplier_Claim_Itm(Service_supp_claim_itm supClaim)
        {
            OracleParameter[] param = new OracleParameter[18];
            Int32 effects = 0;

            (param[0] = new OracleParameter("p_SSC_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = supClaim.SSC_COM;
            (param[1] = new OracleParameter("p_SSC_SUPP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = supClaim.SSC_SUPP;
            (param[2] = new OracleParameter("p_SSC_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = supClaim.SSC_TP;
            (param[3] = new OracleParameter("p_SSC_BRND", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = supClaim.SSC_BRND;
            (param[4] = new OracleParameter("p_SSC_CAT1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = supClaim.SSC_CAT1;
            (param[5] = new OracleParameter("p_SSC_CAT2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = supClaim.SSC_CAT2;
            (param[6] = new OracleParameter("p_SSC_ITM_ALW", OracleDbType.Int32, null, ParameterDirection.Input)).Value = supClaim.SSC_ITM_ALW;
            (param[7] = new OracleParameter("p_SSC_CASH_ALW", OracleDbType.Int32, null, ParameterDirection.Input)).Value = supClaim.SSC_CASH_ALW;
            (param[8] = new OracleParameter("p_SSC_EXITM_ALW", OracleDbType.Int32, null, ParameterDirection.Input)).Value = supClaim.SSC_EXITM_ALW;
            (param[9] = new OracleParameter("p_SSC_UNWARR_ALW", OracleDbType.Int32, null, ParameterDirection.Input)).Value = supClaim.SSC_UNWARR_ALW;
            (param[10] = new OracleParameter("p_SSC_OVWARR_ALW", OracleDbType.Int32, null, ParameterDirection.Input)).Value = supClaim.SSC_OVWARR_ALW;
            (param[11] = new OracleParameter("p_SSC_CLAIM_SUPP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = supClaim.SSC_CLAIM_SUPP;
            (param[12] = new OracleParameter("p_SSC_ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToBoolean(supClaim.SSC_ACT);
            (param[13] = new OracleParameter("p_SSC_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = supClaim.SSC_CRE_BY;
            (param[14] = new OracleParameter("p_SSC_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = supClaim.SSC_MOD_BY;
            (param[15] = new OracleParameter("p_SSC_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = supClaim.SSC_SEQ;
            (param[16] = new OracleParameter("p_SSC_CREDIT_PRD", OracleDbType.Int32, null, ParameterDirection.Input)).Value = supClaim.SSC_CREDIT_PRD;
            param[17] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_save_sup_claim", CommandType.StoredProcedure, param);
            return effects;
        }
        // Nadeeka
        public Int32 Save_Supplier_Claim_Amt(SCV_SUPP_CLAIM_REC supClaimAmt)
        {
            OracleParameter[] param = new OracleParameter[8];
            Int32 effects = 0;

            (param[0] = new OracleParameter("p_SCC_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = supClaimAmt.Scc_seq;
            (param[1] = new OracleParameter("p_SCC_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = supClaimAmt.Scc_line;
            (param[2] = new OracleParameter("p_SCC_CODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = supClaimAmt.Scc_code;
            (param[3] = new OracleParameter("p_SCC_AMT", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = supClaimAmt.Scc_amt;
            (param[4] = new OracleParameter("p_SCC_FROMDATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = supClaimAmt.Scc_fromdate;
            (param[5] = new OracleParameter("p_SCC_TODATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = supClaimAmt.Scc_todate;
            (param[6] = new OracleParameter("p_SCC_ACTIVE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToInt32(supClaimAmt.Scc_active);
            param[7] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_save_sup_amt", CommandType.StoredProcedure, param);
            return effects;
        }


        public List<SCV_SUPP_CLAIM_REC> GetSuppWaraPayment(Int64 _seq)
        {
            List<SCV_SUPP_CLAIM_REC> _supWara = null;
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_seq", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _seq;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);


            DataTable _tblData = QueryDataTable("tbl", "sp_getsupplierPayment", CommandType.StoredProcedure, false, param);

            if (_tblData.Rows.Count > 0)
            {
                _supWara = DataTableExtensions.ToGenericList<SCV_SUPP_CLAIM_REC>(_tblData, SCV_SUPP_CLAIM_REC.Converter);
            }
            return _supWara;
        }

        //kapila
        public Int32 sp_upd_tmp_isu_return(Int32 Seq, Int32 lineNum)
        {
            OracleParameter[] param = new OracleParameter[3];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Seq;
            (param[1] = new OracleParameter("P_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lineNum;

            param[2] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_upd_tmp_isu_return", CommandType.StoredProcedure, param);
            return effects;
        }

        //kapila
        public DataTable GetTempIssueItemsByJobline(string Com, string job, Int32 jobline, string _type)
        {
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[1] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (param[2] = new OracleParameter("P_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = jobline;
            (param[3] = new OracleParameter("P_TYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _type;
            param[4] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "sp_get_tmp_isu_itms", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        //kapila
        public DataTable GetPartRemoveByJobline(string Com, string job, Int32 jobline)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[1] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (param[2] = new OracleParameter("P_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = jobline;
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "sp_get_part_by_jobline", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }


        //kapila
        public DataTable GetJobMRNByJobline(string Com, string job, Int32 jobline)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[1] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (param[2] = new OracleParameter("P_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = jobline;
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "sp_get_mrn_by_jobline", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        //kapila
        public DataTable GetJobTaskByJobline(string Com, string job, Int32 jobline)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[1] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (param[2] = new OracleParameter("P_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = jobline;
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "sp_get_jobstagelog", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        //kapila
        public DataTable GetReqByJobline(string Com, string job, Int32 jobline)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[1] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (param[2] = new OracleParameter("P_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = jobline;
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "sp_get_reqbyjob", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        //kapila
        public DataTable GetRecByJobline(string Com, string job, Int32 jobline)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[1] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (param[2] = new OracleParameter("P_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = jobline;
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "sp_get_rec_by_job", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable GetJobTaskByJob(string Com, string job, Int32 jobline)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[1] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (param[2] = new OracleParameter("P_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = jobline;
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "sp_get_jobtask_by_job", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable GetEstiByJobline(string Com, string job, string _type)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[1] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (param[2] = new OracleParameter("P_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _type;
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "sp_get_est_by_job", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        //kapila 9/7/2015
        public int Update_War_Master(string _warNo, string _ser2)
        {
            int rows_aff = 0;
            OracleParameter[] param = new OracleParameter[3];

            (param[0] = new OracleParameter("p_warno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warNo;
            (param[1] = new OracleParameter("p_ser2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ser2;

            param[2] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            ConnectionOpen();
            rows_aff = UpdateRecords("sp_upd_war_mst", CommandType.StoredProcedure, param);
            return rows_aff;
        }

        //kapila        
        public DataTable GetReceiptByJobNo(string _job, Int32 _line)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_job", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _job;
            (param[1] = new OracleParameter("p_jobItmLine", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _line;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblData = QueryDataTable("tbl", "sp_get_rec_by_jobno", CommandType.StoredProcedure, false, param);
            return _tblData;

        }

        //kapila        
        public DataTable GetJobServiceCharge(string _job, Int32 _line,string _cust)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_job", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _job;
            (param[1] = new OracleParameter("p_jobItmLine", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _line;
            (param[2] = new OracleParameter("p_cust", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cust;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblData = QueryDataTable("tbl", "sp_get_svc_chrg_new", CommandType.StoredProcedure, false, param);
            return _tblData;

        }
        //kapila        
        public DataTable GetCustSatisReplyVal(string _com, string _chnl, Int32 _issms, string _job)
        {
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_chnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _chnl;
            (param[2] = new OracleParameter("p_issms", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _issms;
            (param[3] = new OracleParameter("p_job", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _job;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblData = QueryDataTable("tbl", "sp_get_cust_satis_val", CommandType.StoredProcedure, false, param);
            return _tblData;

        }

        //kapila        
        public DataTable GetCustSatisByChnl(string _com, string _chnl, Int32 _issms)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_chnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _chnl;
            (param[2] = new OracleParameter("p_issms", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _issms;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblData = QueryDataTable("tbl", "sp_get_cust_satisby_chnl", CommandType.StoredProcedure, false, param);
            return _tblData;

        }

        //kapila
        public Int32 UpdateCustomerQSatis(Int32 ssv_seq, Int32 ssv_line, string ssv_desc, string ssv_grade, Int32 ssv_act, string ssv_cre_by)
        {
            OracleParameter[] param = new OracleParameter[7];
            Int32 effects = 0;
            (param[0] = new OracleParameter("p_ssv_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = ssv_seq;
            (param[1] = new OracleParameter("p_ssv_line", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = ssv_line;
            (param[2] = new OracleParameter("p_ssv_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = ssv_desc;
            (param[3] = new OracleParameter("p_ssv_grade", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = ssv_grade;
            (param[4] = new OracleParameter("p_ssv_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = ssv_act;
            (param[5] = new OracleParameter("p_ssv_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = ssv_cre_by;
            param[6] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int16)UpdateRecords("sp_upd_cust_satis_lvl", CommandType.StoredProcedure, param);
            return effects;
        }

        //kapila
        public Int32 UpdateCustomerQuest(Int32 ssq_seq, string ssq_com, string ssq_schnl, string ssq_quest, Int32 ssq_act, string ssq_cre_by, Int32 ssq_issms)
        {
            OracleParameter[] param = new OracleParameter[8];
            Int32 effects = 0;
            (param[0] = new OracleParameter("p_ssq_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = ssq_seq;
            (param[1] = new OracleParameter("p_ssq_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = ssq_com;
            (param[2] = new OracleParameter("p_ssq_schnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = ssq_schnl;
            (param[3] = new OracleParameter("p_ssq_quest", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = ssq_quest;
            (param[4] = new OracleParameter("p_ssq_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = ssq_act;
            (param[5] = new OracleParameter("p_ssq_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = ssq_cre_by;
            (param[6] = new OracleParameter("p_ssq_issms", OracleDbType.Int32, null, ParameterDirection.Input)).Value = ssq_issms;

            param[7] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int16)UpdateRecords("sp_upd_cust_quest", CommandType.StoredProcedure, param);
            return effects;
        }

        //kapila
        public DataTable GetCustQuestData(Int32 _seq)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _seq;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblData = QueryDataTable("tbl", "sp_get_cust_quest", CommandType.StoredProcedure, false, param);
            return _tblData;

        }

        //kapila        
        public DataTable GetCustSatisfData(Int32 _seq, Int32 _line)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _seq;
            (param[1] = new OracleParameter("p_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _line;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblData = QueryDataTable("tbl", "sp_get_cust_satisfc", CommandType.StoredProcedure, false, param);
            return _tblData;

        }

        //kapila
        public DataTable ReorderItemsPrint(string _com, string _user, string in_Brand, string in_Model, string in_Itemcode, string in_Itemcat1, string in_Itemcat2, string in_Itemcat3, Int32 _withStores)
        {
            OracleParameter[] param = new OracleParameter[10];

            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            (param[2] = new OracleParameter("in_Brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Brand;
            (param[3] = new OracleParameter("in_Model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Model;
            (param[4] = new OracleParameter("in_Itemcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcode;
            (param[5] = new OracleParameter("in_Itemcat1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcat1;
            (param[6] = new OracleParameter("in_Itemcat2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcat2;
            (param[7] = new OracleParameter("in_Itemcat3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcat3;
            (param[8] = new OracleParameter("p_with_stores", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _withStores;
            param[9] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults;
            _dtResults = QueryDataTable("tbl", "pkg_reports.sp_proc_reorder_report", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        public Int32 Update_SCV_Req_Hdr(string _reqno)
        {
            Int32 effects = 0;

            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_reqno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqno;

            param[1] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_upd_scv_req_stus", CommandType.StoredProcedure, param);

            return effects;
        }

        //kapila
        public Boolean IsJobOpenReq(string _reqno)
        {
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("p_reqno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqno;
            _para[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            Boolean _Found = QueryDataTable("tbl", "sp_chk_isjobopen_req", CommandType.StoredProcedure, false, _para).Rows.Count > 0 ? true : false;
            return _Found;
        }

        //kapila
        public List<Service_Req_Def> GetSCVreqDefects(string jobNo, Int32 lineNo)
        {
            List<Service_Req_Def> result = new List<Service_Req_Def>();
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("P_REQNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobNo;
            (_para[1] = new OracleParameter("P_LINENO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lineNo;
            _para[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobsDefects", "SP_GET_REQ_DEF_BYREQ", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Req_Def>(_dtResults, Service_Req_Def.Converter_req);
            }
            return result;
        }

        //kapila
        public Service_Req_Hdr GetSCVReqHeader(string reqNo, string com)
        {
            Service_Req_Hdr result = new Service_Req_Hdr();
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("P_req", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = reqNo;
            (_para[1] = new OracleParameter("P_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            _para[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobsDefects", "sp_get_jobreq_hdr", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Req_Hdr>(_dtResults, Service_Req_Hdr.Converter)[0];


            }
            return result;
        }

        //kapila
        public List<Service_Req_Det> GetSCVReqDetails(string reqNo, string com)
        {
            List<Service_Req_Det> result = new List<Service_Req_Det>();
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (_para[1] = new OracleParameter("P_reqNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = reqNo;
            _para[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobsDefects", "sp_get_reqdet_byreq", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Req_Det>(_dtResults, Service_Req_Det.Converter);
            }
            return result;
        }

        public DataTable GetSCVReqData(string reqNo, string com)
        {

            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (_para[1] = new OracleParameter("P_reqNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = reqNo;
            _para[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobsDefects", "sp_get_reqdet_byreq", CommandType.StoredProcedure, false, _para);


            return _dtResults;
        }

        public DataTable sp_get_job_hdrby_jobno(string _jobNo)
        {   // kapila
            OracleParameter[] param = new OracleParameter[2];

            (param[0] = new OracleParameter("p_jobNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobNo;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblRcc", "sp_get_job_hdrby_jobno", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable sp_get_job_category(string _jobcat)
        {   // Sanjeewa
            OracleParameter[] param = new OracleParameter[2];

            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("in_job_cat", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobcat;

            DataTable _dtResults = QueryDataTable("tblRcc", "sp_get_job_category", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable getServicejobDet(string _jobNo, Int32 _line)
        {   // kapila
            OracleParameter[] param = new OracleParameter[3];

            (param[0] = new OracleParameter("p_jobNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobNo;
            (param[1] = new OracleParameter("p_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _line;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblRcc", "sp_get_jobdetby_jobno", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable getServicejobDetBySer(string _ser)
        {   // kapila
            OracleParameter[] param = new OracleParameter[2];

            (param[0] = new OracleParameter("p_ser", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ser;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblRcc", "sp_get_jobdetby_ser", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable getServicejobDef(string _jobNo, Int32 _line)
        {   // kapila
            OracleParameter[] param = new OracleParameter[3];

            (param[0] = new OracleParameter("p_jobNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobNo;
            (param[1] = new OracleParameter("p_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _line;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblRcc", "sp_get_jobdefby_jobno", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable getServiceTempIssuItems(string _jobNo, Int32 _line, Int32 _visitline)
        {   // kapila
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_jobNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobNo;
            (param[1] = new OracleParameter("p_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _line;
            (param[2] = new OracleParameter("p_visit_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _visitline;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblRcc", "sp_get_temp_issu_itms", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable sp_get_job_header(string _jobNo)
        {   // Sanjeewa
            OracleParameter[] param = new OracleParameter[2];

            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("p_jobNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobNo;

            DataTable _dtResults = QueryDataTable("tblRcc", "sp_get_job_header", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable get_agree_header(string _agreeNo)
        {   // Sanjeewa
            OracleParameter[] param = new OracleParameter[2];

            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("p_agreeNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agreeNo;

            DataTable _dtResults = QueryDataTable("tblRcc", "sp_get_agree_hdr", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable get_agree_item(string _agreeNo)
        {   // Sanjeewa
            OracleParameter[] param = new OracleParameter[2];

            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("p_agreeNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agreeNo;

            DataTable _dtResults = QueryDataTable("tblRcc", "sp_get_agree_item", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable get_agree_session(string _agreeNo)
        {   // Sanjeewa
            OracleParameter[] param = new OracleParameter[2];

            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("p_agreeNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agreeNo;

            DataTable _dtResults = QueryDataTable("tblRcc", "sp_get_agree_session", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable get_agree_charge(string _agreeNo)
        {   // Sanjeewa
            OracleParameter[] param = new OracleParameter[2];

            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("p_agreeNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agreeNo;

            DataTable _dtResults = QueryDataTable("tblRcc", "sp_get_agree_charges", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable get_profitcenter(string _com, string _pc)
        {   // Sanjeewa
            OracleParameter[] param = new OracleParameter[3];

            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[2] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;

            DataTable _dtResults = QueryDataTable("tblRcc", "sp_get_profitcenter", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable sp_get_Report_info_chnl(string _report, string _channel)
        {   // Sanjeewa
            OracleParameter[] param = new OracleParameter[3];

            (param[0] = new OracleParameter("p_rep", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _report;
            (param[1] = new OracleParameter("p_jobNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _channel;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblRcc", "sp_get_report_infor_chnl", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable sp_get_job_details(string _jobNo, string _type)
        {   // Sanjeewa
            OracleParameter[] param = new OracleParameter[3];

            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("p_jobNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobNo;
            (param[2] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _type;

            DataTable _dtResults = QueryDataTable("tblRcc", "sp_get_job_detls", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable sp_get_job_detailsSub(string _jobNo)
        {   // Sanjeewa
            OracleParameter[] param = new OracleParameter[2];

            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("p_jobNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobNo;

            DataTable _dtResults = QueryDataTable("tblRcc", "sp_get_job_dtl_sub", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable sp_get_job_defects(string _jobNo)
        {   // Sanjeewa
            OracleParameter[] param = new OracleParameter[2];

            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("p_jobNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobNo;

            DataTable _dtResults = QueryDataTable("tblRcc", "sp_get_job_def", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable sp_get_com_details(string _comcode)
        {   // Sanjeewa
            OracleParameter[] param = new OracleParameter[2];

            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("p_comcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _comcode;

            DataTable _dtResults = QueryDataTable("tblRcc", "sp_get_com_definition", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable sp_get_loc_details(string _loccode)
        {   // Sanjeewa
            OracleParameter[] param = new OracleParameter[2];

            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("p_loccode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loccode;

            DataTable _dtResults = QueryDataTable("tblRcc", "sp_get_loc_definition", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        public DataTable sp_get_locbypc_details(string _com, string _pc)
        {   // Sanjeewa
            OracleParameter[] param = new OracleParameter[3];

            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[2] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;

            DataTable _dtResults = QueryDataTable("tblRcc", "sp_get_locbypc", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable sp_get_gatepass_details(string _gpNo)
        {   // Sanjeewa
            OracleParameter[] param = new OracleParameter[2];

            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("p_gatepass", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _gpNo;

            DataTable _dtResults = QueryDataTable("tblRcc", "sp_get_gatepass", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable sp_get_gpOldpart_details(string _jobNo, Int32 _jobline)
        {   // Sanjeewa
            OracleParameter[] param = new OracleParameter[3];

            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("p_job", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobNo;
            (param[2] = new OracleParameter("p_jobline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobline;

            DataTable _dtResults = QueryDataTable("tblRcc", "sp_get_job_oldpart", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable sp_get_Estimate_details(string _EstNo)
        {   // Sanjeewa
            OracleParameter[] param = new OracleParameter[2];

            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("in_est_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _EstNo;

            DataTable _dtResults = QueryDataTable("tblRcc", "sp_get_estimate_header", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable sp_get_EstimateItem_details(string _EstNo)
        {   // Sanjeewa
            OracleParameter[] param = new OracleParameter[2];

            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("in_est_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _EstNo;

            DataTable _dtResults = QueryDataTable("tblRcc", "sp_get_estimate_items", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable sp_get_Estimate_det(string _com, string _EstNo)
        {   // kapila
            OracleParameter[] param = new OracleParameter[3];

            (param[0] = new OracleParameter("p_est", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _EstNo;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);


            DataTable _dtResults = QueryDataTable("tblRcc", "SP_GET_SEVICE_EST_HEADER", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }


        public DataTable sp_get_Estimatejobs(string _com, string _pc, DateTime _from, DateTime _to, string _estTp, string _cust, string _tech)
        {   // kapila
            OracleParameter[] param = new OracleParameter[8];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            (param[2] = new OracleParameter("p_from", OracleDbType.Date, null, ParameterDirection.Input)).Value = _from;
            (param[3] = new OracleParameter("p_to", OracleDbType.Date, null, ParameterDirection.Input)).Value = _to;
            (param[4] = new OracleParameter("p_est_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _estTp;
            (param[5] = new OracleParameter("p_cust", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cust;
            (param[6] = new OracleParameter("p_tech", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _tech;
            param[7] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);


            DataTable _dtResults = QueryDataTable("tblRcc", "sp_get_est_det", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }


        public DataTable sp_get_EstimateItem_det(string _EstNo)
        {   // kapila
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_est", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _EstNo;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);


            DataTable _dtResults = QueryDataTable("tblRcc", "sp_get_scv_est_itm", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        //kapila
        public Int32 Save_Allocated_Employee(List<MasterServiceEmployee> lst)
        {
            Int32 effects = 0;
            foreach (MasterServiceEmployee _itemDetails in lst)
            {
                OracleParameter[] param = new OracleParameter[12];

                (param[0] = new OracleParameter("p_MSI_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemDetails.Msi_com;
                (param[1] = new OracleParameter("p_MSI_PTY_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemDetails.Msi_pty_tp;
                (param[2] = new OracleParameter("p_MSI_PTY_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemDetails.Msi_pty_cd;
                (param[3] = new OracleParameter("p_MSI_EMP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemDetails.Msi_emp;
                (param[4] = new OracleParameter("p_MSI_PRNT_CATE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemDetails.Msi_prnt_cate;
                (param[5] = new OracleParameter("p_MSI_PRNT_EMP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemDetails.Msi_prnt_emp;
                (param[6] = new OracleParameter("p_MSI_ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _itemDetails.Msi_act;
                (param[7] = new OracleParameter("p_MSI_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemDetails.Msi_cre_by;
                (param[8] = new OracleParameter("p_MSI_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _itemDetails.Msi_cre_dt;
                (param[9] = new OracleParameter("p_MSI_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemDetails.Msi_mod_by;
                (param[10] = new OracleParameter("p_MSI_MOD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _itemDetails.Msi_mod_dt;
                param[11] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

                effects = (Int16)UpdateRecords("sp_save_scv_emp", CommandType.StoredProcedure, param);
            }

            return effects;
        }
        //kapila
        public DataTable getItemComponentDet()
        {
            OracleParameter[] _para = new OracleParameter[1];
            _para[0] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "sp_get_itm_comp", CommandType.StoredProcedure, false, _para);
            return _dtResults;
        }

        //kapila 11/2/2015
        public Int32 UpdateItemComponent(string _cat1, string _cat2, string _cat3, string _item, Int32 _qty, Int32 _war, Int32 _act, Int32 _isser, string _user)
        {
            OracleParameter[] param = new OracleParameter[10];
            Int32 effects = 0;
            (param[0] = new OracleParameter("p_mcc_cat1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat1;
            (param[1] = new OracleParameter("p_mcc_cat2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat2;
            (param[2] = new OracleParameter("p_mcc_cat3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat3;
            (param[3] = new OracleParameter("p_mcc_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[4] = new OracleParameter("p_mcc_qty", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _qty;
            (param[5] = new OracleParameter("p_mcc_supp_warr", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _war;
            (param[6] = new OracleParameter("p_mcc_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _act;
            (param[7] = new OracleParameter("p_mcc_isser", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _isser;
            (param[8] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;

            param[9] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int16)UpdateRecords("sp_upd_itm_cat_comp", CommandType.StoredProcedure, param);
            return effects;
        }

        //kapila
        public DataTable getJobStageByJobNo(string JOb, string Com, Int32 _isSCM2)
        {
            Service_Enquiry_Job_Hdr result = new Service_Enquiry_Job_Hdr();
            OracleParameter[] _para = new OracleParameter[4];
            (_para[0] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = JOb;
            (_para[1] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (_para[2] = new OracleParameter("p_is_scm2", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _isSCM2;
            _para[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "sp_get_jobstage_new", CommandType.StoredProcedure, false, _para);
            return _dtResults;
        }

        //kapila
        public Int32 SaveRequestDefDetail(Service_Req_Def _serviceReqDef)
        {
            OracleParameter[] param = new OracleParameter[13];

            (param[0] = new OracleParameter("p_SRDF_SEQ_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serviceReqDef.Srdf_seq_no;
            (param[1] = new OracleParameter("p_SRDF_REQ_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDef.Srdf_req_no;
            (param[2] = new OracleParameter("p_SRDF_REQ_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serviceReqDef.Srdf_req_line;
            (param[3] = new OracleParameter("p_SRDF_STAGE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDef.Srdf_stage;
            (param[4] = new OracleParameter("p_SRDF_DEF_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serviceReqDef.Srdf_def_line;
            (param[5] = new OracleParameter("p_SRDF_DEF_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDef.Srdf_def_tp;
            (param[6] = new OracleParameter("p_SRDF_DEF_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDef.Srdf_def_rmk;
            (param[7] = new OracleParameter("p_SRDF_ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serviceReqDef.Srdf_act;
            (param[8] = new OracleParameter("p_SRDF_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDef.Srdf_cre_by;
            (param[9] = new OracleParameter("p_SRDF_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _serviceReqDef.Srdf_cre_dt;
            (param[10] = new OracleParameter("p_SRDF_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDef.Srdf_mod_by;
            (param[11] = new OracleParameter("p_SRDF_MOD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _serviceReqDef.Srdf_mod_dt;

            param[12] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            Int32 effects;
            effects = (Int16)UpdateRecords("sp_save_req_def", CommandType.StoredProcedure, param);
            return effects;
        }

        //kapila
        public Int32 Save_Allocated_Priority(List<scv_prit_task> lst)
        {
            Int32 effects = 0;
            foreach (scv_prit_task _itemDetails in lst)
            {
                OracleParameter[] param = new OracleParameter[10];

                (param[0] = new OracleParameter("p_SPIT_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemDetails.Spit_com;
                (param[1] = new OracleParameter("p_SPIT_PTY_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemDetails.Spit_pty_tp;
                (param[2] = new OracleParameter("p_SPIT_PTY_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemDetails.Spit_pty_cd;
                (param[3] = new OracleParameter("p_SPIT_PRIT_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemDetails.Spit_prit_cd;
                (param[4] = new OracleParameter("p_SPIT_STAGE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _itemDetails.Spit_stage;
                (param[5] = new OracleParameter("p_SPIT_EXPT_DUR", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _itemDetails.Spit_expt_dur;
                (param[6] = new OracleParameter("p_SPIT_EXPT_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemDetails.Spit_expt_tp;
                (param[7] = new OracleParameter("p_SPIT_ALRT_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _itemDetails.Spit_alrt_seq;
                (param[8] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemDetails.Spit_cre_by;
                param[9] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

                effects = (Int16)UpdateRecords("sp_save_prio_task", CommandType.StoredProcedure, param);
            }

            return effects;
        }

        //kapila
        public DataTable getPriorityDataByCode(string _code)
        {
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("P_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _code;
            _para[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblScvReqItems", "sp_get_prio", CommandType.StoredProcedure, false, _para);
            return _dtResults;
        }

        //kapila
        public Int32 updatePriorityData(string _code, string _desc, Int32 _act, Int32 _def, string _color, string _user)
        {
            OracleParameter[] param = new OracleParameter[7];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _code;
            (param[1] = new OracleParameter("P_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _desc;
            (param[2] = new OracleParameter("P_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _act;
            (param[3] = new OracleParameter("P_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _def;
            (param[4] = new OracleParameter("P_color", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _color;
            (param[5] = new OracleParameter("P_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            param[6] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_upd_prio_data", CommandType.StoredProcedure, param);
            return effects;
        }

        //kapila
        public DataTable getPriorityData()
        {
            OracleParameter[] _para = new OracleParameter[1];
            _para[0] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblScvReqItems", "sp_get_priority_data", CommandType.StoredProcedure, false, _para);
            return _dtResults;
        }

        //kapila
        public DataTable getChannelPara(string _com, string _code)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _code;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblData = QueryDataTable("tbData", "sp_chk_chnl_para", CommandType.StoredProcedure, false, param);
            return _tblData;
        }

        //kapila
        public List<Service_Confirm_detail> GetServiceConfirmDet(string _jobNo, Int32 _line)
        {
            List<Service_Confirm_detail> result = new List<Service_Confirm_detail>();
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_job", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobNo;
            (param[1] = new OracleParameter("P_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _line;
            param[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "sp_get_conf_det", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Confirm_detail>(_dtResults, Service_Confirm_detail.Converter);
            }
            return result;
        }
        // Nadeeka
        public List<Service_Confirm_detail> GetServiceConfirmDetJob(string _jobNo, string _conNo)
        {
            List<Service_Confirm_detail> result = new List<Service_Confirm_detail>();
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_job", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobNo;
            (param[1] = new OracleParameter("P_confNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _conNo;
            param[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "sp_get_conf_det_job", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Confirm_detail>(_dtResults, Service_Confirm_detail.Converter);
            }
            return result;
        }
        //kapila
        public Boolean checkFiedJob(string _jobno)
        {
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("p_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobno;

            _para[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            Boolean _isAllow = QueryDataTable("tbl", "sp_chk_fld_job", CommandType.StoredProcedure, false, _para).Rows.Count > 0 ? true : false;
            return _isAllow;
        }
        //Nadeeka
        public DataTable geTechAllocationPending(string _com, string _userid)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userid;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _itemTable = new DataTable();
            _itemTable = QueryDataTable("tblserial", "sp_getTechAllocPendingJobs", CommandType.StoredProcedure, false, param);
            return _itemTable;
        }

        //kapila
        public DataTable getCustSatisData(string _com, string _schnl, string _code)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_schnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _schnl;
            (param[2] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _code;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _itemTable = new DataTable();
            _itemTable = QueryDataTable("tblserial", "sp_get_cust_satis", CommandType.StoredProcedure, false, param);
            return _itemTable;
        }

        //kapila
        public DataTable InvoiceByJobLine(string _jobno, Int32 _line, string _item, string _stus)
        {
            OracleParameter[] _para = new OracleParameter[5];
            (_para[0] = new OracleParameter("p_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobno;
            (_para[1] = new OracleParameter("p_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _line;
            (_para[2] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (_para[3] = new OracleParameter("p_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _stus;
            _para[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _itemTable = new DataTable();
            _itemTable = QueryDataTable("tblserial", "sp_chk_jb_line_inv_new", CommandType.StoredProcedure, false, _para);
            return _itemTable;

        }

        public DataTable InvoiceByJobConfirmLine(string _confno, Int32 _line)
        {
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("p_confno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confno;
            (_para[1] = new OracleParameter("p_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _line;

            _para[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _itemTable = new DataTable();
            _itemTable = QueryDataTable("tblserial", "sp_getInv_by_conf_line", CommandType.StoredProcedure, false, _para);
            return _itemTable;

        }

        //kapila
        public Boolean IsJobLineInvoiced(string _jobno, Int32 _line)
        {
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("p_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobno;
            (_para[1] = new OracleParameter("p_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _line;
            _para[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            Boolean _isAllow = QueryDataTable("tbl", "sp_chk_jb_line_inv", CommandType.StoredProcedure, false, _para).Rows.Count > 0 ? true : false;
            return _isAllow;
        }

        //kapila
        public Boolean IsCustAllwGatePassWOutInv(string _com, string _code)
        {
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (_para[1] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _code;
            _para[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            Boolean _isAllow = QueryDataTable("tbl", "sp_chk_cust_allw_gtpass", CommandType.StoredProcedure, false, _para).Rows.Count > 0 ? true : false;
            return _isAllow;
        }

        //kapila
        public DataTable getSerialIDByDocument(string _doc, string _item, string _ser)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_document", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _doc;
            (param[1] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[2] = new OracleParameter("p_ser", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ser;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _itemTable = new DataTable();
            _itemTable = QueryDataTable("tblserial", "sp_get_serid_by_docno", CommandType.StoredProcedure, false, param);
            return _itemTable;
        }

        //kapila
        //public Int32 UPDATE_OLD_PART_BY_GATEPASS(string jobnumber, Int32 jobLine)
        //{
        //    OracleParameter[] param = new OracleParameter[3];
        //    Int32 effects = 0;
        //    (param[0] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobnumber;
        //    (param[1] = new OracleParameter("P_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = jobLine;
        //    param[2] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
        //    effects = (Int32)UpdateRecords("sp_upd_old_part_by_gatepass", CommandType.StoredProcedure, param);
        //    return effects;
        //}

        //kapila
        //public Int32 UPDATE_SCV_JOB_DET_BY_GATEPASS(string jobnumber, Int32 jobLine)
        //{
        //    OracleParameter[] param = new OracleParameter[3];
        //    Int32 effects = 0;
        //    (param[0] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobnumber;
        //    (param[1] = new OracleParameter("P_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = jobLine;
        //    param[2] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
        //    effects = (Int32)UpdateRecords("sp_updjob_det_by_gatepass", CommandType.StoredProcedure, param);
        //    return effects;
        //}

        //kapila
        //public Int32 SaveGatePassHdr(string _com, string _loc, string _doc, DateTime _date, string _jobno, Int32 _jobline, string _rmk, string _stus, string _creby, string _satis, string _satisRem)
        //{
        //    OracleParameter[] param = new OracleParameter[12];
        //    (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
        //    (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
        //    (param[2] = new OracleParameter("p_doc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _doc;
        //    (param[3] = new OracleParameter("p_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = _date;
        //    (param[4] = new OracleParameter("p_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobno;
        //    (param[5] = new OracleParameter("p_jobline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobline;
        //    (param[6] = new OracleParameter("p_rmk", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _rmk;
        //    (param[7] = new OracleParameter("p_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _stus;
        //    (param[8] = new OracleParameter("p_creby", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _creby;
        //    (param[9] = new OracleParameter("p_satis", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _satis;
        //    (param[10] = new OracleParameter("p_satis_rem", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _satisRem;

        //    param[11] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

        //    Int32 effects;
        //    effects = (Int16)UpdateRecords("sp_save_gate_pass_hdr", CommandType.StoredProcedure, param);
        //    return effects;
        //}

        //kapila
        public Int32 SaveRequestDetail(Service_Req_Det _serviceReqDet)
        {
            OracleParameter[] param = new OracleParameter[82];

            (param[0] = new OracleParameter("p_JRD_SEQ_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_seq_no;
            (param[1] = new OracleParameter("p_JRD_REQNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_reqno;
            (param[2] = new OracleParameter("p_JRD_REQLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_reqline;
            (param[3] = new OracleParameter("p_JRD_SJOBNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_sjobno;
            (param[4] = new OracleParameter("p_JRD_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_loc;
            (param[5] = new OracleParameter("p_JRD_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_pc;
            (param[6] = new OracleParameter("p_JRD_ITM_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_itm_cd;
            (param[7] = new OracleParameter("p_JRD_ITM_STUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_itm_stus;
            (param[8] = new OracleParameter("p_JRD_ITM_DESC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_itm_desc;
            (param[9] = new OracleParameter("p_JRD_BRAND", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_brand;
            (param[10] = new OracleParameter("p_JRD_MODEL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_model;
            (param[11] = new OracleParameter("p_JRD_ITM_COST", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_itm_cost;
            (param[12] = new OracleParameter("p_JRD_SER1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_ser1;
            (param[13] = new OracleParameter("p_JRD_SER2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_ser2;
            (param[14] = new OracleParameter("p_JRD_WARR", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_warr;
            (param[15] = new OracleParameter("p_JRD_REGNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_regno;
            (param[16] = new OracleParameter("p_JRD_MILAGE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_milage;
            (param[17] = new OracleParameter("p_JRD_WARR_STUS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_warr_stus;
            (param[18] = new OracleParameter("p_JRD_ONLOAN", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_onloan;
            (param[19] = new OracleParameter("p_JRD_CHG_WARR_STDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_chg_warr_stdt;
            (param[20] = new OracleParameter("p_JRD_CHG_WARR_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_chg_warr_rmk;
            (param[21] = new OracleParameter("p_JRD_SENTWCN", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_sentwcn;
            (param[22] = new OracleParameter("p_JRD_ISINSURANCE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_isinsurance;
            (param[23] = new OracleParameter("p_JRD_SER_TERM", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_ser_term;
            (param[24] = new OracleParameter("p_JRD_LASTWARR_STDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_lastwarr_stdt;
            (param[25] = new OracleParameter("p_JRD_ISSUED", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_issued;
            (param[26] = new OracleParameter("p_JRD_MAINITMCD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_mainitmcd;
            (param[27] = new OracleParameter("p_JRD_MAINITMSER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_mainitmser;
            (param[28] = new OracleParameter("p_JRD_MAINITMWARR", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_mainitmwarr;
            (param[29] = new OracleParameter("p_JRD_ITMMFC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_itmmfc;
            (param[30] = new OracleParameter("p_JRD_MAINITMMFC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_mainitmmfc;
            (param[31] = new OracleParameter("p_JRD_AVAILABILTY", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_availabilty;
            (param[32] = new OracleParameter("p_JRD_USEJOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_usejob;
            (param[33] = new OracleParameter("p_JRD_MSNNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_msnno;
            (param[34] = new OracleParameter("p_JRD_ITMTP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_itmtp;
            (param[35] = new OracleParameter("p_JRD_SERLOCCHR", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_serlocchr;
            (param[36] = new OracleParameter("p_JRD_CUSTNOTES", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_custnotes;
            (param[37] = new OracleParameter("p_JRD_MAINREQNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_mainreqno;
            (param[38] = new OracleParameter("p_JRD_MAINREQLOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_mainreqloc;
            (param[39] = new OracleParameter("p_JRD_MAINJOBNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_mainjobno;
            (param[40] = new OracleParameter("p_JRD_ISSTOCKUPDATE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_isstockupdate;
            (param[41] = new OracleParameter("p_JRD_NEEDGATEPASS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_needgatepass;
            (param[42] = new OracleParameter("p_JRD_ISWRN", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_iswrn;
            (param[43] = new OracleParameter("p_JRD_WARRPERIOD", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_warrperiod;
            (param[44] = new OracleParameter("p_JRD_WARRRMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_warrrmk;
            (param[45] = new OracleParameter("p_JRD_WARRSTARTDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_warrstartdt;
            (param[46] = new OracleParameter("p_JRD_WARRREPLACE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_warrreplace;
            (param[47] = new OracleParameter("p_JRD_DATE_PUR", OracleDbType.Date, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_date_pur;
            (param[48] = new OracleParameter("p_JRD_INVC_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_invc_no;
            (param[49] = new OracleParameter("p_JRD_WARAAMD_SEQ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_waraamd_seq;
            (param[50] = new OracleParameter("p_JRD_WARAAMD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_waraamd_by;
            (param[51] = new OracleParameter("p_JRD_WARAAMD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_waraamd_dt;
            (param[52] = new OracleParameter("p_JRD_INVC_SHOWROOM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_invc_showroom;
            (param[53] = new OracleParameter("p_JRD_AODISSUELOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_aodissueloc;
            (param[54] = new OracleParameter("p_JRD_AODISSUEDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_aodissuedt;
            (param[55] = new OracleParameter("p_JRD_AODISSUENO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_aodissueno;
            (param[56] = new OracleParameter("p_JRD_AODRECNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_aodrecno;
            (param[57] = new OracleParameter("p_JRD_TECHST_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_techst_dt;
            (param[58] = new OracleParameter("p_JRD_TECHFIN_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_techfin_dt;
            (param[59] = new OracleParameter("p_JRD_MSN_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_msn_no;
            (param[60] = new OracleParameter("p_JRD_ISEXTERNALITM", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_isexternalitm;
            (param[61] = new OracleParameter("p_JRD_CONF_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_conf_dt;
            (param[62] = new OracleParameter("p_JRD_CONF_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_conf_cd;
            (param[63] = new OracleParameter("p_JRD_CONF_DESC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_conf_desc;
            (param[64] = new OracleParameter("p_JRD_CONF_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_conf_rmk;
            (param[65] = new OracleParameter("p_JRD_TRANF_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_tranf_by;
            (param[66] = new OracleParameter("p_JRD_TRANF_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_tranf_dt;
            (param[67] = new OracleParameter("p_JRD_DO_INVOICE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_do_invoice;
            (param[68] = new OracleParameter("p_JRD_INSU_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_insu_com;
            (param[69] = new OracleParameter("p_JRD_AGREENO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_agreeno;
            (param[70] = new OracleParameter("p_JRD_ISSRN", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_issrn;
            (param[71] = new OracleParameter("p_JRD_ISAGREEMENT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_isagreement;
            (param[72] = new OracleParameter("p_JRD_CUST_AGREENO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_cust_agreeno;
            (param[73] = new OracleParameter("p_JRD_QUO_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_quo_no;
            (param[74] = new OracleParameter("p_JRD_STAGE", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_stage;
            (param[75] = new OracleParameter("p_JRD_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_com;
            (param[76] = new OracleParameter("p_JRD_SER_ID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_ser_id;
            (param[77] = new OracleParameter("p_JRD_USED", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_used;
            (param[78] = new OracleParameter("p_JRD_JOBNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_jobno;
            (param[79] = new OracleParameter("p_JRD_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_jobline;
            (param[80] = new OracleParameter("p_JRD_SUPP_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqDet.Jrd_supp_cd;
            param[81] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            Int32 effects;
            effects = (Int16)UpdateRecords("sp_save_req_det_new", CommandType.StoredProcedure, param);
            return effects;
        }

        //kapila
        public Int32 SaveRequestHeader(Service_Req_Hdr _serviceReqHdr)
        {
            OracleParameter[] param = new OracleParameter[69];

            (param[0] = new OracleParameter("p_SRB_SEQ_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_seq_no;
            (param[1] = new OracleParameter("p_SRB_REQNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_reqno;
            (param[2] = new OracleParameter("p_SRB_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_dt;
            (param[3] = new OracleParameter("p_SRB_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_com;
            (param[4] = new OracleParameter("p_SRB_JOBCAT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_jobcat;
            (param[5] = new OracleParameter("p_SRB_JOBTP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_jobtp;
            (param[6] = new OracleParameter("p_SRB_JOBSTP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_jobstp;
            (param[7] = new OracleParameter("p_SRB_MANUALREF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_manualref;
            (param[8] = new OracleParameter("p_SRB_OTHERREF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_otherref;
            (param[9] = new OracleParameter("p_SRB_REFNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_refno;
            (param[10] = new OracleParameter("p_SRB_JOBSTAGE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_jobstage;
            (param[11] = new OracleParameter("p_SRB_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_rmk;
            (param[12] = new OracleParameter("p_SRB_PRORITY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_prority;
            (param[13] = new OracleParameter("p_SRB_ST_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_st_dt;
            (param[14] = new OracleParameter("p_SRB_ED_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_ed_dt;
            (param[15] = new OracleParameter("p_SRB_NOOFPRINT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_noofprint;
            (param[16] = new OracleParameter("p_SRB_LASTPRINTBY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_lastprintby;
            (param[17] = new OracleParameter("p_SRB_ORDERNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_orderno;
            (param[18] = new OracleParameter("p_SRB_CUSTEXPTDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_custexptdt;
            (param[19] = new OracleParameter("p_SRB_SUBSTAGE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_substage;
            (param[20] = new OracleParameter("p_SRB_CUST_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_cust_cd;
            (param[21] = new OracleParameter("p_SRB_CUST_TIT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_cust_tit;
            (param[22] = new OracleParameter("p_SRB_CUST_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_cust_name;
            (param[23] = new OracleParameter("p_SRB_NIC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_nic;
            (param[24] = new OracleParameter("p_SRB_DL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_dl;
            (param[25] = new OracleParameter("p_SRB_PP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_pp;
            (param[26] = new OracleParameter("p_SRB_MOBINO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_mobino;
            (param[27] = new OracleParameter("p_SRB_ADD1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_add1;
            (param[28] = new OracleParameter("p_SRB_ADD2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_add2;
            (param[29] = new OracleParameter("p_SRB_ADD3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_add3;
            (param[30] = new OracleParameter("p_SRB_TOWN", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_town;
            (param[31] = new OracleParameter("p_SRB_PHNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_phno;
            (param[32] = new OracleParameter("p_SRB_FAXNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_faxno;
            (param[33] = new OracleParameter("p_SRB_EMAIL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_email;
            (param[34] = new OracleParameter("p_SRB_CNT_PERSON", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_cnt_person;
            (param[35] = new OracleParameter("p_SRB_CNT_ADD1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_cnt_add1;
            (param[36] = new OracleParameter("p_SRB_CNT_ADD2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_cnt_add2;
            (param[37] = new OracleParameter("p_SRB_CNT_PHNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_cnt_phno;
            (param[38] = new OracleParameter("p_SRB_JOB_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_job_rmk;
            (param[39] = new OracleParameter("p_SRB_TECH_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_tech_rmk;
            (param[40] = new OracleParameter("p_SRB_B_CUST_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_b_cust_cd;
            (param[41] = new OracleParameter("p_SRB_B_CUST_TIT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_b_cust_tit;
            (param[42] = new OracleParameter("p_SRB_B_CUST_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_b_cust_name;
            (param[43] = new OracleParameter("p_SRB_B_NIC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_b_nic;
            (param[44] = new OracleParameter("p_SRB_B_DL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_b_dl;
            (param[45] = new OracleParameter("p_SRB_B_PP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_b_pp;
            (param[46] = new OracleParameter("p_SRB_B_MOBINO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_b_mobino;
            (param[47] = new OracleParameter("p_SRB_B_ADD1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_b_add1;
            (param[48] = new OracleParameter("p_SRB_B_ADD2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_b_add2;
            (param[49] = new OracleParameter("p_SRB_B_ADD3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_b_add3;
            (param[50] = new OracleParameter("p_SRB_B_TOWN", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_b_town;
            (param[51] = new OracleParameter("p_SRB_B_PHNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_b_phno;
            (param[52] = new OracleParameter("p_SRB_B_FAX", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_b_fax;
            (param[53] = new OracleParameter("p_SRB_B_EMAIL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_b_email;
            (param[54] = new OracleParameter("p_SRB_INFM_PERSON", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_infm_person;
            (param[55] = new OracleParameter("p_SRB_INFM_ADD1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_infm_add1;
            (param[56] = new OracleParameter("p_SRB_INFM_ADD2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_infm_add2;
            (param[57] = new OracleParameter("p_SRB_INFM_PHNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_infm_phno;
            (param[58] = new OracleParameter("p_SRB_STUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_stus;
            (param[59] = new OracleParameter("p_SRB_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_cre_by;
            (param[60] = new OracleParameter("p_SRB_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_cre_dt;
            (param[61] = new OracleParameter("p_SRB_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_mod_by;
            (param[62] = new OracleParameter("p_SRB_MOD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_mod_dt;

            (param[63] = new OracleParameter("p_SRB_B_CUST_TAX_EXEMPTED", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _serviceReqHdr.SRB_B_CUST_TAX_EXEMPTED;
            (param[64] = new OracleParameter("p_SRB_B_CUST_IS_SVAT", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _serviceReqHdr.SRB_B_CUST_IS_SVAT;
            (param[65] = new OracleParameter("p_SRB_ADDITIONAL_COST", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _serviceReqHdr.SRB_ADDITIONAL_COST;
            (param[66] = new OracleParameter("p_SRB_B_CUST_IS_TAX", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _serviceReqHdr.SRB_B_CUST_IS_TAX;
            (param[67] = new OracleParameter("P_SRB_CNT_EMAIL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.SRB_CNT_EMAIL;
            param[68] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            Int32 effects;
            effects = (Int16)UpdateRecords("sp_save_req_hdr", CommandType.StoredProcedure, param);
            return effects;
        }

        //kapila
        public Boolean IsMonthAdvance(string _com, DateTime _date, string _module, string _cate_tp, string _cate_cd)
        {
            OracleParameter[] _para = new OracleParameter[7];
            (_para[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (_para[1] = new OracleParameter("p_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = _date;
            (_para[2] = new OracleParameter("p_module", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _module;
            (_para[3] = new OracleParameter("p_dir", OracleDbType.Int32, null, ParameterDirection.Input)).Value = 0;
            (_para[4] = new OracleParameter("p_cate_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cate_tp;
            (_para[5] = new OracleParameter("p_cate_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cate_cd;
            _para[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            Boolean IsFound = QueryDataTable("tbl", "sp_chk_mnth_adv", CommandType.StoredProcedure, false, _para).Rows.Count > 0 ? true : false;

            return IsFound;
        }

        //kapila
        public Int32 GetJobSerialID()
        {
            OracleParameter[] param = new OracleParameter[1];
            Int32 effects = 0;

            param[0] = new OracleParameter("o_serialid", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_getjobserialid", CommandType.StoredProcedure, param);
            return effects;
        }

        //kapila
        public Int32 SaveCustomerSatis(SCV_JOB_SATIS _custSatis)
        {
            Int32 _effect = 0;
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_SSJ_SEQ_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _custSatis.Ssj_seq_no;
            (param[1] = new OracleParameter("p_SSJ_JOBNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _custSatis.Ssj_jobno;
            (param[2] = new OracleParameter("p_SSJ_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _custSatis.Ssj_jobline;
            (param[3] = new OracleParameter("p_SSJ_QUEST_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _custSatis.Ssj_quest_seq;
            (param[4] = new OracleParameter("p_SSJ_QUEST_VAL", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _custSatis.Ssj_quest_val;
            (param[5] = new OracleParameter("p_SSJ_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _custSatis.Ssj_cre_by;

            param[6] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            _effect = (Int32)UpdateRecords("sp_upd_cust_satis", CommandType.StoredProcedure, param);

            return _effect;
        }

        //kapila, Edit by Chamal 01-Dec-2014
        public Int32 UpdateJobDefects(Service_Job_Defects _jobDef)
        {
            OracleParameter[] param = new OracleParameter[13];
            (param[0] = new OracleParameter("p_srd_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDef.SRD_SEQ_NO;
            (param[1] = new OracleParameter("p_srd_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDef.SRD_JOB_NO;
            (param[2] = new OracleParameter("p_srd_job_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDef.SRD_JOB_LINE;
            (param[3] = new OracleParameter("p_srd_stage", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDef.SRD_STAGE;
            (param[4] = new OracleParameter("p_srd_def_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDef.SRD_DEF_LINE;
            (param[5] = new OracleParameter("p_srd_def_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDef.SRD_DEF_TP;
            (param[6] = new OracleParameter("p_srd_def_rmk", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDef.SRD_DEF_RMK;
            (param[7] = new OracleParameter("p_srd_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDef.SRD_ACT;
            (param[8] = new OracleParameter("p_srd_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDef.SRD_CRE_BY;
            (param[9] = new OracleParameter("p_srd_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDef.SRD_MOD_BY;
            (param[10] = new OracleParameter("p_srd_is_main_def", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDef.SRD_IS_MAIN_DEF;
            (param[11] = new OracleParameter("p_srd_deff_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDef.SDT_DESC;

            param[12] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            Int32 effects;
            effects = (Int16)UpdateRecords("sp_upd_cust_job_def", CommandType.StoredProcedure, param);
            return effects;
        }

        //kapila
        public Int32 UpdateJobHeader(Service_JOB_HDR _jobHdr)
        {
            OracleParameter[] param = new OracleParameter[72];

            (param[0] = new OracleParameter("p_SJB_SEQ_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobHdr.SJB_SEQ_NO;
            (param[1] = new OracleParameter("p_SJB_JOBNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_JOBNO;
            (param[2] = new OracleParameter("p_SJB_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobHdr.SJB_DT;
            (param[3] = new OracleParameter("p_SJB_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_COM;
            (param[4] = new OracleParameter("p_SJB_JOBCAT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_JOBCAT;
            (param[5] = new OracleParameter("p_SJB_JOBTP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_JOBTP;
            (param[6] = new OracleParameter("p_SJB_JOBSTP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_JOBSTP;
            (param[7] = new OracleParameter("p_SJB_MANUALREF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_MANUALREF;
            (param[8] = new OracleParameter("p_SJB_OTHERREF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_OTHERREF;
            (param[9] = new OracleParameter("p_SJB_REQNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_REQNO;
            (param[10] = new OracleParameter("p_SJB_JOBSTAGE", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _jobHdr.SJB_JOBSTAGE;
            (param[11] = new OracleParameter("p_SJB_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_RMK;
            (param[12] = new OracleParameter("p_SJB_PRORITY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_PRORITY;
            (param[13] = new OracleParameter("p_SJB_ST_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobHdr.SJB_ST_DT;
            (param[14] = new OracleParameter("p_SJB_ED_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobHdr.SJB_ED_DT;
            (param[15] = new OracleParameter("p_SJB_NOOFPRINT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobHdr.SJB_NOOFPRINT;
            (param[16] = new OracleParameter("p_SJB_LASTPRINTBY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_LASTPRINTBY;
            (param[17] = new OracleParameter("p_SJB_ORDERNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_ORDERNO;
            (param[18] = new OracleParameter("p_SJB_CUSTEXPTDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobHdr.SJB_CUSTEXPTDT;
            (param[19] = new OracleParameter("p_SJB_SUBSTAGE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_SUBSTAGE;
            (param[20] = new OracleParameter("p_SJB_CUST_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_CUST_CD;
            (param[21] = new OracleParameter("p_SJB_CUST_TIT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_CUST_TIT;
            (param[22] = new OracleParameter("p_SJB_CUST_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_CUST_NAME;
            (param[23] = new OracleParameter("p_SJB_NIC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_NIC;
            (param[24] = new OracleParameter("p_SJB_DL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_DL;
            (param[25] = new OracleParameter("p_SJB_PP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_PP;
            (param[26] = new OracleParameter("p_SJB_MOBINO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_MOBINO;
            (param[27] = new OracleParameter("p_SJB_ADD1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_ADD1;
            (param[28] = new OracleParameter("p_SJB_ADD2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_ADD2;
            (param[29] = new OracleParameter("p_SJB_ADD3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_ADD3;
            (param[30] = new OracleParameter("p_SJB_TOWN", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_TOWN;
            (param[31] = new OracleParameter("p_SJB_PHNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_PHNO;
            (param[32] = new OracleParameter("p_SJB_FAXNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_FAXNO;
            (param[33] = new OracleParameter("p_SJB_EMAIL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_EMAIL;
            (param[34] = new OracleParameter("p_SJB_CNT_PERSON", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_CNT_PERSON;
            (param[35] = new OracleParameter("p_SJB_CNT_ADD1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_CNT_ADD1;
            (param[36] = new OracleParameter("p_SJB_CNT_ADD2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_CNT_ADD2;
            (param[37] = new OracleParameter("p_SJB_CNT_PHNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_CNT_PHNO;
            (param[38] = new OracleParameter("p_SJB_JOB_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_JOB_RMK;
            (param[39] = new OracleParameter("p_SJB_TECH_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_TECH_RMK;
            (param[40] = new OracleParameter("p_SJB_B_CUST_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_B_CUST_CD;
            (param[41] = new OracleParameter("p_SJB_B_CUST_TIT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_B_CUST_TIT;
            (param[42] = new OracleParameter("p_SJB_B_CUST_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_B_CUST_NAME;
            (param[43] = new OracleParameter("p_SJB_B_NIC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_B_NIC;
            (param[44] = new OracleParameter("p_SJB_B_DL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_B_DL;
            (param[45] = new OracleParameter("p_SJB_B_PP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_B_PP;
            (param[46] = new OracleParameter("p_SJB_B_MOBINO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_B_MOBINO;
            (param[47] = new OracleParameter("p_SJB_B_ADD1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_B_ADD1;
            (param[48] = new OracleParameter("p_SJB_B_ADD2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_B_ADD2;
            (param[49] = new OracleParameter("p_SJB_B_ADD3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_B_ADD3;
            (param[50] = new OracleParameter("p_SJB_B_TOWN", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_B_TOWN;
            (param[51] = new OracleParameter("p_SJB_B_PHNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_B_PHNO;
            (param[52] = new OracleParameter("p_SJB_B_FAX", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_B_FAX;
            (param[53] = new OracleParameter("p_SJB_B_EMAIL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_B_EMAIL;
            (param[54] = new OracleParameter("p_SJB_INFM_PERSON", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_INFM_PERSON;
            (param[55] = new OracleParameter("p_SJB_INFM_ADD1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_INFM_ADD1;
            (param[56] = new OracleParameter("p_SJB_INFM_ADD2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_INFM_ADD2;
            (param[57] = new OracleParameter("p_SJB_INFM_PHNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_INFM_PHNO;
            (param[58] = new OracleParameter("p_SJB_STUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_STUS;
            (param[59] = new OracleParameter("p_SJB_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_CRE_BY;
            (param[60] = new OracleParameter("p_SJB_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobHdr.SJB_CRE_DT;
            (param[61] = new OracleParameter("p_SJB_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_MOD_BY;
            (param[62] = new OracleParameter("p_SJB_MOD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobHdr.SJB_MOD_DT;
            (param[63] = new OracleParameter("SJB_CHG_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_CHG_CD;
            (param[64] = new OracleParameter("SJB_CHG", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _jobHdr.SJB_CHG;
            (param[65] = new OracleParameter("p_sjb_pod_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_POD_NO;
            (param[66] = new OracleParameter("p_sjb_rec_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.SJB_REC_LOC;

            //updated by akila 2017/06/16 save service location details
            (param[67] = new OracleParameter("p_sjb_scv_add1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.sjb_scv_add1;
            (param[68] = new OracleParameter("p_sjb_scv_add2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.sjb_scv_add2;
            (param[69] = new OracleParameter("p_sjb_scv_add3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.sjb_scv_add3;
            (param[70] = new OracleParameter("p_sjb_scv_add4", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobHdr.sjb_scv_add4;
            param[71] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            Int32 effects;
            effects = (Int16)UpdateRecords("sp_upd_cust_job_hdr", CommandType.StoredProcedure, param);
            return effects;
        }

        //kapila
        //Edit by Chamal 01-Dec-2014
        public Int32 UpdateJobDetail(Service_job_Det _jobDet)
        {
            OracleParameter[] param = new OracleParameter[107];

            (param[0] = new OracleParameter("p_jbd_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_seq_no;
            (param[1] = new OracleParameter("p_jbd_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_jobno;
            (param[2] = new OracleParameter("p_jbd_jobline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_jobline;
            (param[3] = new OracleParameter("p_jbd_sjobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_sjobno;
            (param[4] = new OracleParameter("p_jbd_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_loc;
            (param[5] = new OracleParameter("p_jbd_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_pc;
            (param[6] = new OracleParameter("p_jbd_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_itm_cd;
            (param[7] = new OracleParameter("p_jbd_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_itm_stus;
            (param[8] = new OracleParameter("p_jbd_itm_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_itm_desc;
            (param[9] = new OracleParameter("p_jbd_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_brand;
            (param[10] = new OracleParameter("p_jbd_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_model;
            (param[11] = new OracleParameter("p_jbd_itm_cost", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_itm_cost;
            (param[12] = new OracleParameter("p_jbd_ser1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_ser1;
            (param[13] = new OracleParameter("p_jbd_ser2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_ser2;
            (param[14] = new OracleParameter("p_jbd_warr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_warr;
            (param[15] = new OracleParameter("p_jbd_regno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_regno;
            (param[16] = new OracleParameter("p_jbd_milage", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _jobDet.Jbd_milage;
            (param[17] = new OracleParameter("p_jbd_warr_stus", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_warr_stus;
            (param[18] = new OracleParameter("p_jbd_onloan", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_onloan;
            (param[19] = new OracleParameter("p_jbd_chg_warr_stdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobDet.Jbd_chg_warr_stdt;
            (param[20] = new OracleParameter("p_jbd_chg_warr_rmk", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_chg_warr_rmk;
            (param[21] = new OracleParameter("p_jbd_isinsurance", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_isinsurance;
            (param[22] = new OracleParameter("p_jbd_cate1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_cate1;
            (param[23] = new OracleParameter("p_jbd_ser_term", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_ser_term;
            (param[24] = new OracleParameter("p_jbd_lastwarr_stdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobDet.Jbd_lastwarr_stdt;
            (param[25] = new OracleParameter("p_jbd_issued", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_issued;
            (param[26] = new OracleParameter("p_jbd_mainitmcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_mainitmcd;
            (param[27] = new OracleParameter("p_jbd_mainitmser", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_mainitmser;
            (param[28] = new OracleParameter("p_jbd_mainitmwarr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_mainitmwarr;
            (param[29] = new OracleParameter("p_jbd_itmmfc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_itmmfc;
            (param[30] = new OracleParameter("p_jbd_mainitmmfc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_mainitmmfc;
            (param[31] = new OracleParameter("p_jbd_availabilty", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_availabilty;
            (param[32] = new OracleParameter("p_jbd_usejob", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_usejob;
            (param[33] = new OracleParameter("p_jbd_msnno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_msnno;
            (param[34] = new OracleParameter("p_jbd_itmtp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_itmtp;
            (param[35] = new OracleParameter("p_jbd_serlocchr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_serlocchr;
            (param[36] = new OracleParameter("p_jbd_custnotes", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_custnotes;
            (param[37] = new OracleParameter("p_jbd_mainreqno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_mainreqno;
            (param[38] = new OracleParameter("p_jbd_mainreqloc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_mainreqloc;
            (param[39] = new OracleParameter("p_jbd_mainjobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_mainjobno;
            (param[40] = new OracleParameter("p_jbd_reqitmtp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_reqitmtp;
            (param[41] = new OracleParameter("p_jbd_reqno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_reqno;
            (param[42] = new OracleParameter("p_jbd_reqline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_reqline;
            (param[43] = new OracleParameter("p_jbd_isstockupdate", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_isstockupdate;
            (param[44] = new OracleParameter("p_jbd_isgatepass", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_isgatepass;
            (param[45] = new OracleParameter("p_jbd_iswrn", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_iswrn;
            (param[46] = new OracleParameter("p_jbd_warrperiod", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_warrperiod;
            (param[47] = new OracleParameter("p_jbd_warrrmk", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_warrrmk;
            (param[48] = new OracleParameter("p_jbd_warrstartdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobDet.Jbd_warrstartdt;
            (param[49] = new OracleParameter("p_jbd_warrreplace", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_warrreplace;
            (param[50] = new OracleParameter("p_jbd_date_pur", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobDet.Jbd_date_pur;
            (param[51] = new OracleParameter("p_jbd_invc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_invc_no;
            (param[52] = new OracleParameter("p_jbd_waraamd_seq", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_waraamd_seq;
            (param[53] = new OracleParameter("p_jbd_waraamd_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_waraamd_by;
            (param[54] = new OracleParameter("p_jbd_waraamd_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobDet.Jbd_waraamd_dt;
            (param[55] = new OracleParameter("p_jbd_invc_showroom", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_invc_showroom;
            (param[56] = new OracleParameter("p_jbd_aodissueloc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_aodissueloc;
            (param[57] = new OracleParameter("p_jbd_aodissuedt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobDet.Jbd_aodissuedt;
            (param[58] = new OracleParameter("p_jbd_aodissueno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_aodissueno;
            (param[59] = new OracleParameter("p_jbd_aodrecno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_aodrecno;
            (param[60] = new OracleParameter("p_jbd_techst_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobDet.Jbd_techst_dt;
            (param[61] = new OracleParameter("p_jbd_techfin_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobDet.Jbd_techfin_dt;
            (param[62] = new OracleParameter("p_jbd_msn_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_msn_no;
            (param[63] = new OracleParameter("p_jbd_isexternalitm", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_isexternalitm;
            (param[64] = new OracleParameter("p_jbd_conf_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobDet.Jbd_conf_dt;
            (param[65] = new OracleParameter("p_jbd_conf_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_conf_cd;
            (param[66] = new OracleParameter("p_jbd_conf_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_conf_desc;
            (param[67] = new OracleParameter("p_jbd_conf_rmk", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_conf_rmk;
            (param[68] = new OracleParameter("p_jbd_tranf_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_tranf_by;
            (param[69] = new OracleParameter("p_jbd_tranf_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobDet.Jbd_tranf_dt;
            (param[70] = new OracleParameter("p_jbd_do_invoice", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_do_invoice;
            (param[71] = new OracleParameter("p_jbd_insu_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_insu_com;
            (param[72] = new OracleParameter("p_jbd_agreeno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_agreeno;
            (param[73] = new OracleParameter("p_jbd_issrn", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_issrn;
            (param[74] = new OracleParameter("p_jbd_isagreement", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_isagreement;
            (param[75] = new OracleParameter("p_jbd_cust_agreeno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_cust_agreeno;
            (param[76] = new OracleParameter("p_jbd_quo_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_quo_no;
            (param[77] = new OracleParameter("p_jbd_stage", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _jobDet.Jbd_stage;
            (param[78] = new OracleParameter("p_jbd_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_com;
            (param[79] = new OracleParameter("p_jbd_ser_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_ser_id;
            (param[80] = new OracleParameter("p_jbd_techst_dt_man", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobDet.Jbd_techst_dt_man;
            (param[81] = new OracleParameter("p_jbd_techfin_dt_man", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobDet.Jbd_techfin_dt_man;
            (param[82] = new OracleParameter("p_jbd_reqwcn", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_reqwcn;
            (param[83] = new OracleParameter("p_jbd_reqwcndt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobDet.Jbd_reqwcnsysdt;
            (param[84] = new OracleParameter("p_jbd_reqwcnsysdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobDet.Jbd_reqwcnsysdt;
            (param[85] = new OracleParameter("p_jbd_sentwcn", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_sentwcn;
            (param[86] = new OracleParameter("p_jbd_recwcn", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_recwcn;
            (param[87] = new OracleParameter("p_jbd_takewcn", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_takewcn;
            (param[88] = new OracleParameter("p_jbd_takewcndt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobDet.Jbd_takewcndt;
            (param[89] = new OracleParameter("p_jbd_takewcnsysdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobDet.Jbd_takewcnsysdt;
            (param[90] = new OracleParameter("p_jbd_supp_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_supp_cd;
            (param[91] = new OracleParameter("p_jbd_part_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_part_cd;
            (param[92] = new OracleParameter("p_jbd_oem_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_oem_no;
            (param[93] = new OracleParameter("p_jbd_case_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_case_id;
            (param[94] = new OracleParameter("p_jbd_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_act;
            (param[95] = new OracleParameter("p_jbd_oldjobline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_oldjobline;
            (param[96] = new OracleParameter("p_jbd_tech_rmk", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_tech_rmk;
            (param[97] = new OracleParameter("p_jbd_tech_custrmk", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_tech_custrmk;
            (param[98] = new OracleParameter("p_jbd_tech_cls_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_tech_cls_tp;
            (param[99] = new OracleParameter("P_jbd_tech_cls_rmk", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_tech_cls_rmk;
            (param[100] = new OracleParameter("P_JBD_IS_FGAP", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_is_fgap;
            (param[101] = new OracleParameter("P_JBD_REP_PERC", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _jobDet.Jbd_rep_perc;
            (param[102] = new OracleParameter("P_JBD_SW_STUS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.jbd_sw_stus;
            (param[103] = new OracleParameter("P_JBD_PB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.jbd_pb;
            (param[104] = new OracleParameter("P_JBD_PBLVL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.jbd_pblvl;
            (param[105] = new OracleParameter("P_JBD_DEL_SALE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobDet.jbd_del_sale_dt;
            param[106] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            Int32 effects;
            effects = (Int16)UpdateRecords("sp_upd_cust_job_det", CommandType.StoredProcedure, param);
            return effects;
        }

        public int Sample01(string _com, string _pc)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            return UpdateRecords("sample", CommandType.StoredProcedure, param);
        }

        //kapila, edit by chamal 27-11-2014
        public DataTable getDefectTypes(string _com, string _chnl, string _cat, string _code)
        {
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_chnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _chnl;
            (param[2] = new OracleParameter("p_cat", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat;
            (param[3] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _code;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _itemTable = new DataTable();
            _itemTable = QueryDataTable("tblserial", "sp_get_def_types", CommandType.StoredProcedure, false, param);
            return _itemTable;
        }

        public DataTable getAgrDetailsGenReq(string _agrno, Int32 _line)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_agrno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrno;
            (param[1] = new OracleParameter("p_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _line;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _itemTable = new DataTable();
            _itemTable = QueryDataTable("tblserial", "sp_get_reqagr_det", CommandType.StoredProcedure, false, param);
            return _itemTable;
        }
        public DataTable getAgreementItems(string _agrno)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_agrno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrno;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _itemTable = new DataTable();
            _itemTable = QueryDataTable("tblserial", "sp_get_reqagr_items", CommandType.StoredProcedure, false, param);
            return _itemTable;
        }

        public DataTable getAgrNo4ReqGen(string _com, string _pc, DateTime _from, DateTime _to, string _agrNo)
        {
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            (param[2] = new OracleParameter("p_from", OracleDbType.Date, null, ParameterDirection.Input)).Value = _from;
            (param[3] = new OracleParameter("p_to", OracleDbType.Date, null, ParameterDirection.Input)).Value = _to;
            (param[4] = new OracleParameter("p_agrno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrNo;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _itemTable = new DataTable();
            _itemTable = QueryDataTable("tblserial", "sp_get_reqagr", CommandType.StoredProcedure, false, param);
            return _itemTable;
        }

        public DataTable getAgrNoInv(string _com, string _pc, DateTime _from, DateTime _to)
        {// Nadeeka 27/07/2015
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            (param[2] = new OracleParameter("p_from", OracleDbType.Date, null, ParameterDirection.Input)).Value = _from;
            (param[3] = new OracleParameter("p_to", OracleDbType.Date, null, ParameterDirection.Input)).Value = _to;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _itemTable = new DataTable();
            _itemTable = QueryDataTable("tblserial", "sp_get_reqagrforInv", CommandType.StoredProcedure, false, param);
            return _itemTable;
        }

        public List<scv_agr_payshed> getAgrPay(string _Agree)
        {// Nadeeka 27/07/2015
            List<scv_agr_payshed> result = new List<scv_agr_payshed>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_agr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Agree;

            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _itemTable = new DataTable();
            _itemTable = QueryDataTable("tblserial", "sp_get_agreementPaySch", CommandType.StoredProcedure, false, param);
            if (_itemTable.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<scv_agr_payshed>(_itemTable, scv_agr_payshed.Converter);
            }
            return result;


        }

        public List<scv_agr_cha> getAgrItem(string _Agree)
        {// Nadeeka 27/07/2015
            List<scv_agr_cha> result = new List<scv_agr_cha>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_agr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Agree;

            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _itemTable = new DataTable();
            _itemTable = QueryDataTable("tblserial", "sp_get_agreementItem", CommandType.StoredProcedure, false, param);



            if (_itemTable.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<scv_agr_cha>(_itemTable, scv_agr_cha.Converter);
            }
            return result;

        }

        public DataTable getBrandMgrAlloc(string _com, string _man, string _cat=null, string _brand=null)
        {
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_man", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _man;
            (param[2] = new OracleParameter("p_cat1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat;
            (param[3] = new OracleParameter("p_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _brand;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _itemTable = new DataTable();
            _itemTable = QueryDataTable("tblserial", "sp_get_brnd_alloc", CommandType.StoredProcedure, false, param);
            return _itemTable;
        }

        public DataTable getSalesTypeByInvNo(string _invno)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invno;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _itemTable = new DataTable();
            _itemTable = QueryDataTable("tblserial", "sp_get_inv_type", CommandType.StoredProcedure, false, param);
            return _itemTable;
        }

        //Tharaka 2014-09-30
        public DataTable GetTechAllocJobs(string com, DateTime From, DateTime To, string jobno, string Stage, Int32 isCusexpectDate, string customer, string PC,string town)
        {
            OracleParameter[] _para = new OracleParameter[10];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (_para[1] = new OracleParameter("P_From", OracleDbType.Date, null, ParameterDirection.Input)).Value = From;
            (_para[2] = new OracleParameter("P_TO", OracleDbType.Date, null, ParameterDirection.Input)).Value = To;
            (_para[3] = new OracleParameter("p_jonno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobno;
            (_para[4] = new OracleParameter("P_Stage", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Stage;
            (_para[5] = new OracleParameter("p_iscusexpectdate", OracleDbType.Int32, null, ParameterDirection.Input)).Value = isCusexpectDate;
            (_para[6] = new OracleParameter("P_Customer", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = customer;
            (_para[7] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PC;
            (_para[8] = new OracleParameter("P_TOWN", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = town;
            _para[9] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //DataTable _dtResults = QueryDataTable("tblJobs", "SP_GET_SCVJOB_TECHALLOCATON", CommandType.StoredProcedure, false, _para);
            DataTable _dtResults = QueryDataTable("tblJobs", "SP_GET_SCVJOB_TECHALLOCATON_t", CommandType.StoredProcedure, false, _para);

            return _dtResults;
        }

        //Tharaka 2014-09-30
        public List<Service_Job_Defects> GetJobDefects(string jobNo, Int32 lineNo, string Type)
        {
            List<Service_Job_Defects> result = new List<Service_Job_Defects>();
            OracleParameter[] _para = new OracleParameter[4];
            (_para[0] = new OracleParameter("P_JOBNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobNo;
            (_para[1] = new OracleParameter("P_LINENO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lineNo;
            (_para[2] = new OracleParameter("P_TYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Type;
            _para[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobsDefects", "SP_GET_JOB_DEFECTS", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Job_Defects>(_dtResults, Service_Job_Defects.ConverterAll);
            }
            return result;
        }

        //Tharaka 09-09-2014
        public DataTable GetEmployeByDefect(string com, string defect)
        {
            DataTable _dtResults = null;
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("P_DEFECT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = defect;
            param[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _dtResults = QueryDataTable("tbl", "SP_GET_EMP_BYDEFECT", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public Int32 Is_EstimateAvailable(string _jobno)
        {
            try
            {
                int _effect = 0;
                OracleParameter[] param = new OracleParameter[3];

                (param[0] = new OracleParameter("in_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobno; 
                OracleParameter orP = new OracleParameter("c_result", OracleDbType.Int32, null, ParameterDirection.Output);
                _effect = ReturnSP_SingleValue("sp_is_estimate_available", CommandType.StoredProcedure, orP, param);
                return _effect;
            }
            catch (Exception) { return -1; }
        }

        //Tharaka 2014-10-02
        public DataTable GetEmployeBySkillDesignation(string com, string Skill, string Designation, string EPF, string PC)
        {
            DataTable _dtResults = null;
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("P_DESIGNATION", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Designation;
            (param[2] = new OracleParameter("P_SKILL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Skill;
            (param[3] = new OracleParameter("P_EPF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = EPF;
            (param[4] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PC;
            param[5] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _dtResults = QueryDataTable("tbl", "SP_GET_EMP_BYDESIG_SKILL", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        //THARAKA 2014-10-03
        public Int32 Save_TechnicianAllocatoinHEader(Service_Tech_Aloc_Hdr oHeader)
        {
            OracleParameter[] param = new OracleParameter[23];
            Int32 effects = 0; ;
            (param[0] = new OracleParameter("P_sth_alocno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.STH_ALOCNO;
            (param[1] = new OracleParameter("P_sth_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.STH_COM;
            (param[2] = new OracleParameter("P_sth_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.STH_LOC;
            (param[3] = new OracleParameter("P_sth_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.STH_TP;
            (param[4] = new OracleParameter("P_sth_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.STH_JOBNO;
            (param[5] = new OracleParameter("P_sth_jobline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oHeader.STH_JOBLINE;
            (param[6] = new OracleParameter("P_sth_emp_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.STH_EMP_CD;
            (param[7] = new OracleParameter("P_sth_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.STH_STUS;
            (param[8] = new OracleParameter("P_sth_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.STH_CRE_BY;
            (param[9] = new OracleParameter("P_sth_cre_when", OracleDbType.Date, null, ParameterDirection.Input)).Value = oHeader.STH_CRE_WHEN;
            (param[10] = new OracleParameter("P_sth_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.STH_MOD_BY;
            (param[11] = new OracleParameter("P_sth_mod_when", OracleDbType.Date, null, ParameterDirection.Input)).Value = oHeader.STH_MOD_WHEN;
            (param[12] = new OracleParameter("P_sth_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.STH_SESSION_ID;
            (param[13] = new OracleParameter("P_sth_town", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.STH_TOWN;
            (param[14] = new OracleParameter("P_sth_from_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = oHeader.STH_FROM_DT;
            (param[15] = new OracleParameter("P_sth_to_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = oHeader.STH_TO_DT;
            (param[16] = new OracleParameter("P_sth_reqno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.STH_REQNO;
            (param[17] = new OracleParameter("P_sth_reqline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oHeader.STH_REQLINE;
            (param[18] = new OracleParameter("P_STH_TERMINAL", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oHeader.STH_TERMINAL;
            (param[19] = new OracleParameter("P_STH_CURR_STUS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oHeader.STH_CURR_STUS;
            (param[20] = new OracleParameter("P_STH_IS_MAIN_TECH", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oHeader.STH_IS_MAIN_TECH;
            (param[21] = new OracleParameter("P_STH_TECH_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.ESEP_FIRST_NAME;
            
            param[22] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int32)UpdateRecords("SP_SAVE_ALLOCATION_HDR", CommandType.StoredProcedure, param);
            return effects;
        }

        //THARAKA 2014-10-03
        public Int32 Save_ServiceJobStageLog(Service_Job_StageLog oHeader)
        {
            OracleParameter[] param = new OracleParameter[13];
            Int32 effects = 0; ;
            (param[0] = new OracleParameter("P_sjl_reqno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJL_REQNO;
            (param[1] = new OracleParameter("P_sjl_reqline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oHeader.SJL_REQLINE;
            (param[2] = new OracleParameter("P_sjl_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJL_JOBNO;
            (param[3] = new OracleParameter("P_sjl_jobline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oHeader.SJL_JOBLINE;
            (param[4] = new OracleParameter("P_sjl_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJL_COM;
            (param[5] = new OracleParameter("P_sjl_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJL_LOC;
            (param[6] = new OracleParameter("P_sjl_otherdocno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJL_OTHERDOCNO;
            (param[7] = new OracleParameter("P_sjl_jobstage", OracleDbType.Decimal , null, ParameterDirection.Input)).Value = oHeader.SJL_JOBSTAGE;
            (param[8] = new OracleParameter("P_sjl_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJL_CRE_BY;
            (param[9] = new OracleParameter("P_sjl_cre_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = oHeader.SJL_CRE_DT;
            (param[10] = new OracleParameter("P_sjl_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJL_SESSION_ID;
            (param[11] = new OracleParameter("P_sjl_infsup ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oHeader.SJL_INFSUP;
            param[12] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int32)UpdateRecords("SP_SAVE_JOBSTAGELOG", CommandType.StoredProcedure, param);
            return effects;
        }
        //Sanjeewa 2018-09-05
        public Int32 Save_CancelledwarrantyActive(string _item, string _serial, string _warranty)
        {
            OracleParameter[] param = new OracleParameter[4];
            Int32 effects = 0; ;
            (param[0] = new OracleParameter("in_itm", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[1] = new OracleParameter("in_serial", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial;
            (param[2] = new OracleParameter("in_warr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty;
            
            param[3] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int32)UpdateRecords("sp_save_cancel_warr_active", CommandType.StoredProcedure, param);
            return effects;
        }

        public Int32 UpdateAgreementSession(string _agreeNo, int _agreeline, int _agreeSession, string _ReqNo)
        {
            OracleParameter[] param = new OracleParameter[5];
            Int32 effects = 0;
            (param[0] = new OracleParameter("in_agree", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agreeNo;
            (param[1] = new OracleParameter("in_agreeline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _agreeline;
            (param[2] = new OracleParameter("in_agreesession", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _agreeSession;
            (param[3] = new OracleParameter("in_reqno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ReqNo;            
            param[4] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int32)UpdateRecords("sp_update_agree_session", CommandType.StoredProcedure, param);
            return effects;
        }

        //THARAKA 2014-10-03
        public Int32 Update_JobDetailStage(string JObNo, Int32 JobLine, Decimal Stage, String _user = "")
        {
            OracleParameter[] param = new OracleParameter[5];
            Int32 effects = 0; ;
            (param[0] = new OracleParameter("P_STAGE", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = Stage;
            (param[1] = new OracleParameter("P_JOBNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = JObNo;
            (param[2] = new OracleParameter("P_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = JobLine;
            (param[3] = new OracleParameter("P_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            param[4] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int32)UpdateRecords("SP_UPDATE_JOB_DET_STAGE", CommandType.StoredProcedure, param);
            return effects;
        }

        public Int32 Update_RccStage(string RCCNo, String _user = "")
        {
            OracleParameter[] param = new OracleParameter[3];
            Int32 effects = 0; ;
            (param[0] = new OracleParameter("P_RCC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = RCCNo;            
            (param[1] = new OracleParameter("P_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            param[2] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int32)UpdateRecords("SP_UPDATE_RCC_STAGE", CommandType.StoredProcedure, param);
            return effects;
        }

        public Int32 UpdateAgrSession(string _agrno, DateTime _from, DateTime _to, string _reqno)
        {
            OracleParameter[] param = new OracleParameter[5];
            Int32 effects = 0; ;
            (param[0] = new OracleParameter("p_agrno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrno;
            (param[1] = new OracleParameter("P_FROM", OracleDbType.Date, null, ParameterDirection.Input)).Value = _from;
            (param[2] = new OracleParameter("P_TO", OracleDbType.Date, null, ParameterDirection.Input)).Value = _to;
            (param[3] = new OracleParameter("p_reqno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqno;

            param[4] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int32)UpdateRecords("sp_update_agr_sess", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2014-10-03
        public DataTable GetAllocatedHistory(DateTime From, DateTime To, string com, string lOC, string Town, string Emp, out List<Service_TechAllocation> oItems)
        {
            oItems = new List<Service_TechAllocation>();
            DataTable _dtResults = null;
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("P_FROM", OracleDbType.Date, null, ParameterDirection.Input)).Value = From;
            (param[1] = new OracleParameter("P_TO", OracleDbType.Date, null, ParameterDirection.Input)).Value = To;
            (param[2] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[3] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lOC;
            (param[4] = new OracleParameter("P_Town", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Town;
            (param[5] = new OracleParameter("P_EMP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Emp;
            param[6] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _dtResults = QueryDataTable("tbl", "SP_GET_TECHALLO_HISTY", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                oItems = DataTableExtensions.ToGenericList<Service_TechAllocation>(_dtResults, Service_TechAllocation.Converter);
            }
            return _dtResults;
        }

        //Tharaka 2014-10-04
        public List<Service_Tech_Aloc_Hdr> GetJobAllocations(string jobNo, Int32 lineNo, string com)
        {
            List<Service_Tech_Aloc_Hdr> result = new List<Service_Tech_Aloc_Hdr>();
            OracleParameter[] _para = new OracleParameter[4];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (_para[1] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobNo;
            (_para[2] = new OracleParameter("P_LINE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lineNo;
            _para[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobsDefects", "SP_GET_TECH_ALLOC", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Tech_Aloc_Hdr>(_dtResults, Service_Tech_Aloc_Hdr.Converter1);
            }
            return result;
        }

        //Tharaka 2014-10-04
        public Int32 Update_TechAllocationStatus(string JObNo, Int32 JobLine, string Status, string com)
        {
            OracleParameter[] param = new OracleParameter[5];
            Int32 effects = 0; ;
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("p_job", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = JObNo;
            (param[2] = new OracleParameter("p_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = JobLine;
            (param[3] = new OracleParameter("p_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Status;
            param[4] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int32)UpdateRecords("sp_change_techallo_status", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2014-10-04
        public Int32 GetLocationCapacity(string com, string lOC, string Type)
        {
            OracleParameter[] param = new OracleParameter[4];
            Int32 Capasity = 0; ;
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lOC;
            (param[2] = new OracleParameter("P_TYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Type;
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobsDefects", "SP_GET_LOC_CAPACITY", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                Capasity = Convert.ToInt32(_dtResults.Rows[0]["slf_capa"].ToString());
            }
            return Capasity;
        }

        //Tharaka 2014-10-07
        public Int32 GetLocationCurrectSlotCount(string com, string lOC, string Terminal, DateTime From, DateTime To)
        {
            OracleParameter[] param = new OracleParameter[6];
            Int32 Capasity = 0; ;
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lOC;
            (param[2] = new OracleParameter("P_FROM", OracleDbType.Date, null, ParameterDirection.Input)).Value = From;
            (param[3] = new OracleParameter("P_TO", OracleDbType.Date, null, ParameterDirection.Input)).Value = To;
            (param[4] = new OracleParameter("P_TERMINAL", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToInt32(Terminal);
            param[5] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobsDefects", "SP_GET_LOC_CRNT_TMNL_COUNT", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                Capasity = Convert.ToInt32(_dtResults.Rows[0]["TOTAL"].ToString());
            }
            return Capasity;
        }

        //shalika 07/10/2014
        public DataTable LoadTypes()
        {
            OracleParameter[] param = new OracleParameter[1];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_TYPES", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        //shalika 07/10/2014
        public Int32 UpdateTaskType(string code, string desc, string type, int act, string userid)
        {
            OracleParameter[] param = new OracleParameter[6];

            (param[0] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
            (param[1] = new OracleParameter("p_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = desc;
            (param[2] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
            (param[3] = new OracleParameter("P_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToInt32(act);
            (param[4] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userid;
            param[5] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            Int32 effects;
            effects = (Int16)UpdateRecords("sp_insert_into_scv_cat", CommandType.StoredProcedure, param);
            return effects;
        }

        //shalika 07/10/2014
        public DataTable LoadCat()
        {
            OracleParameter[] param = new OracleParameter[1];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SER_CAT", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public SCV_AGR_HDR GetServiceAgrHeader(string _agrNo)
        {
            SCV_AGR_HDR result = new SCV_AGR_HDR();
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("p_agrno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrNo;
            _para[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobsDefects", "sp_get_scv_agr_hdr", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<SCV_AGR_HDR>(_dtResults, SCV_AGR_HDR.Converter)[0];

            }
            return result;
        }

        public List<SCV_AGR_ITM> GetServiceAgrItems(string _agrNo)
        {
            List<SCV_AGR_ITM> result = new List<SCV_AGR_ITM>();
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("p_agrno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrNo;
            _para[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobsDefects", "sp_get_scv_agr_itm", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<SCV_AGR_ITM>(_dtResults, SCV_AGR_ITM.Converter);

            }
            return result;
        }

        public List<SCV_AGR_SES> GetServiceAgrSessions(string _agrNo)
        {
            List<SCV_AGR_SES> result = new List<SCV_AGR_SES>();
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("p_agrno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrNo;
            _para[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobsDefects", "sp_get_scv_agr_sess", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<SCV_AGR_SES>(_dtResults, SCV_AGR_SES.Converter);

            }
            return result;
        }

        public List<scv_agr_cha> GetServiceAgrCharges(string _agrNo)
        {
            List<scv_agr_cha> result = new List<scv_agr_cha>();
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("p_agrno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrNo;
            _para[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobsDefects", "sp_get_scv_agr_cha", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<scv_agr_cha>(_dtResults, scv_agr_cha.Converter);

            }
            return result;
        }

        public List<scv_agr_payshed> GetServiceAgrPayShed(string _agrNo)
        {
            List<scv_agr_payshed> result = new List<scv_agr_payshed>();
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("p_agrno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _agrNo;
            _para[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobsDefects", "sp_get_scv_agr_payshed", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<scv_agr_payshed>(_dtResults, scv_agr_payshed.Converter);

            }
            return result;
        }

        //Tharaka 2014-10-09
        public Service_JOB_HDR GetServiceJobHeader(string jobNo, string com)
        {
            Service_JOB_HDR result = new Service_JOB_HDR();
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("P_JOBNUM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobNo;
            (_para[1] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            _para[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobsDefects", "SP_GET_JOB_HDR", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_JOB_HDR>(_dtResults, Service_JOB_HDR.Converter)[0];

                result.SJB_JOBSTAGE_TEXT = _dtResults.Rows[0]["jbs_desc"].ToString();
            }
            return result;
        }

        //Tharaka 2014-10-09
        public List<Service_job_Det> GetJobDetails(string jobNo, Int32 lineNo, string com)
        {
            List<Service_job_Det> result = new List<Service_job_Det>();
            OracleParameter[] _para = new OracleParameter[4];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (_para[1] = new OracleParameter("P_JOBNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobNo;
            (_para[2] = new OracleParameter("P_LINENO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lineNo;
            _para[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobsDefects", "SP_GET_JOB_DET", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_job_Det>(_dtResults, Service_job_Det.Converter);
            }
            return result;
        }

        // Nadeeka22-07-2015
        public DataTable GetUserNameByEmpCode(string _EmpCode)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_userEPF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _EmpCode;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblUser", "SP_GET_USER_BY_EMP_CODE", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        // Nadeeka 07-03-2015
        public Service_job_Det GetJobDetailsdtl(string jobNo, Int32 lineNo, string com)
        {
            Service_job_Det result = new Service_job_Det();
            OracleParameter[] _para = new OracleParameter[4];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (_para[1] = new OracleParameter("P_JOBNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobNo;
            (_para[2] = new OracleParameter("P_LINENO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lineNo;
            _para[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobsDefects", "SP_GET_JOB_DET", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_job_Det>(_dtResults, Service_job_Det.Converter)[0];


            }

            return result;
        }

        //Tharaka 2014-10-09
        public DataTable getServiceJobDefects(string jobNo, Int32 lineNo)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_job", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobNo;
            (param[1] = new OracleParameter("p_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lineNo;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "sp_get_scv_job_defts", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        //shalika 10/10/2014
        public Int32 Save_Allocated_Category(List<ServiceTaskLocations> lst_all_items)
        {
            Int32 effects = 0;
            foreach (ServiceTaskLocations _itemDetails in lst_all_items)
            {
                OracleParameter[] param = new OracleParameter[8];

                (param[0] = new OracleParameter("p_SCS_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemDetails.Com;
                (param[1] = new OracleParameter("p_SCS_PTY_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemDetails.Scs_pty_tp;
                (param[2] = new OracleParameter("p_SCS_PTY_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemDetails.Scs_pty_code;
                (param[3] = new OracleParameter("p_SCS_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemDetails.Scs_tp;
                (param[4] = new OracleParameter("p_SCS_DEF", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _itemDetails.Is_default;
                (param[5] = new OracleParameter("p_SCS_ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _itemDetails.Is_active;
                (param[6] = new OracleParameter("p_SCS_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemDetails.Create_by;
                param[7] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

                effects = (Int16)UpdateRecords("SP_INSERT_ALLOCATE_CAT", CommandType.StoredProcedure, param);
            }

            return effects;
        }

        //shalika 07/10/2014
        public DataTable bind_task_loc(string loc)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = loc;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_LOC_TASK", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        //shalika 10/10/2014
        public Int32 SaveUpdateUtilization(int active, string com, string loc, decimal capacity, string type, string usr)
        {
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_isactive", OracleDbType.Int32, null, ParameterDirection.Input)).Value = active;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[2] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = loc;
            (param[3] = new OracleParameter("p_capacity", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToInt32(capacity);
            (param[4] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
            (param[5] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = usr;
            param[6] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            Int32 effects;
            effects = (Int16)UpdateRecords("sp_insert_scv_loc_fac", CommandType.StoredProcedure, param);
            return effects;
        }

        // Nadeeka 29-12-2014
        public List<Service_job_Det> getSupplierClaimRequest(string _com, string _loc, string _supp, string _job, string _jobPart, string _jobserial, string _jobBrand, string _jobItem, DateTime _FromDate, DateTime _ToDate, string _cat, Int32 _sts)
        {
            List<Service_job_Det> _list = null;
            OracleParameter[] param = new OracleParameter[13];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            (param[2] = new OracleParameter("p_supp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _supp;
            (param[3] = new OracleParameter("p_job", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _job;
            (param[4] = new OracleParameter("p_part", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobPart;
            (param[5] = new OracleParameter("p_serial", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobserial;
            (param[6] = new OracleParameter("p_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobBrand;
            (param[7] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobItem;
            (param[8] = new OracleParameter("p_from", OracleDbType.Date, null, ParameterDirection.Input)).Value = _FromDate;
            (param[9] = new OracleParameter("p_todate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _ToDate;
            (param[10] = new OracleParameter("p_cat", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat;
            (param[11] = new OracleParameter("p_sts", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _sts;
            param[12] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblData = QueryDataTable("tbData", "sp_getSupClaimRequest", CommandType.StoredProcedure, false, param);

            if (_tblData.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<Service_job_Det>(_tblData, Service_job_Det.ConverterClaimItems);
            }
            return _list;
        }


        public List<Service_job_Det> getSupplierClaimRequestMRN(string _com, string _job,Int32 _jobline )
        { // Nadeeka 29-01-2016
            List<Service_job_Det> _list = null;
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_job", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _job;
            (param[2] = new OracleParameter("p_jobline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobline; 
             param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblData = QueryDataTable("tbData", "sp_getSupClaimRequestmrn", CommandType.StoredProcedure, false, param);

            if (_tblData.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<Service_job_Det>(_tblData, Service_job_Det.ConverterDet);
            }
            return _list;
        }



        public DataTable GetServiceSupplierWarranty(string _com, string _loc, string _supp, string _cat1, string _cat2, string _cat3, string _model, string _brand, string _item, DateTime _fdate, DateTime _tdate,string _doctp)
        {
            // Nadeeka 06-05-2015
            OracleParameter[] param = new OracleParameter[13];

            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[2] = new OracleParameter("in_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            (param[3] = new OracleParameter("in_supp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _supp;
            (param[4] = new OracleParameter("in_from", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fdate;
            (param[5] = new OracleParameter("in_todate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _tdate;
            (param[6] = new OracleParameter("in_cat1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat1;
            (param[7] = new OracleParameter("in_cat2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat2;
            (param[8] = new OracleParameter("in_cat3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat3;
            (param[9] = new OracleParameter("in_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _model;
            (param[10] = new OracleParameter("in_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _brand;
            (param[11] = new OracleParameter("in_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[12] = new OracleParameter("in_doctp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _doctp;            
            DataTable _tblData = QueryDataTable("tbl", "sp_Supplier_Warranty", CommandType.StoredProcedure, false, param);
            return _tblData;
        }

        public DataTable GetServiceSupplierWarrantyJob(string _com, string _job, Int32 _line)
        {
            // Nadeeka 06-05-2015
            OracleParameter[] param = new OracleParameter[4];


            (param[0] = new OracleParameter("p_job", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _job;
            (param[2] = new OracleParameter("p_line", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _line;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _tblData = QueryDataTable("tbl", "sp_get_sup_warr_job", CommandType.StoredProcedure, false, param);
            return _tblData;
        }



        // Nadeeka 29-12-2014
        public List<Service_JOB_HDR> getTransferJob(string _com, string _loc, DateTime _FromDate, DateTime _ToDate, string _job)
        {
            List<Service_JOB_HDR> _list = null;
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            (param[2] = new OracleParameter("p_from", OracleDbType.Date, null, ParameterDirection.Input)).Value = _FromDate;
            (param[3] = new OracleParameter("p_todate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _ToDate;
            (param[4] = new OracleParameter("p_job", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _job;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblData = QueryDataTable("tbData", "sp_getTranferJob", CommandType.StoredProcedure, false, param);

            if (_tblData.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<Service_JOB_HDR>(_tblData, Service_JOB_HDR.Converter);
            }
            return _list;
        }
        public Int32 Insert_ServiceShedule(Service_job_Det sevDet, int isUpdate, DateTime jobDate)
        {
            //INR_SERMST_SEVSHED
            // NADEEKA
            Int32 effects = 0;
            OracleParameter[] param = new OracleParameter[15];
            (param[0] = new OracleParameter("p_regNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = sevDet.Jbd_regno;
            (param[1] = new OracleParameter("p_EngineNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = sevDet.Jbd_ser1;
            (param[2] = new OracleParameter("p_ChasseNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = sevDet.Jbd_ser2;
            (param[3] = new OracleParameter("p_serID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = sevDet.Jbd_ser_id;
            (param[4] = new OracleParameter("p_itemCD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = sevDet.Jbd_itm_cd;
            (param[5] = new OracleParameter("p_itmStatus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = sevDet.Jbd_itm_stus;
            (param[6] = new OracleParameter("p_isUpdate", OracleDbType.Int32, null, ParameterDirection.Input)).Value = isUpdate;
            (param[7] = new OracleParameter("p_jobNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = sevDet.Jbd_jobno;
            (param[8] = new OracleParameter("p_jobDt", OracleDbType.Date, null, ParameterDirection.Input)).Value = jobDate;
            (param[9] = new OracleParameter("p_actReading", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = sevDet.Jbd_milage;
            (param[10] = new OracleParameter("p_sevTerm", OracleDbType.Int32, null, ParameterDirection.Input)).Value = sevDet.Jbd_ser_term;
            (param[11] = new OracleParameter("p_warr_pd_uom", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "KM";
            (param[12] = new OracleParameter("p_warr_pdalt_uom", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "DD";
            (param[13] = new OracleParameter("p_warr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = sevDet.Jbd_warr;
            param[14] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int32)UpdateRecords("sp_update_Or_insert_ServiceDet", CommandType.StoredProcedure, param);
            return effects;
        }

        //Nadeeka
        public List<Service_WCN_Hdr> getWCNHeader(string _com, string _loc, DateTime _fdate, DateTime _todate, string _sts, string _job, string _order, string _docno)
        {
            List<Service_WCN_Hdr> _list = null;
            OracleParameter[] param = new OracleParameter[9];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            (param[2] = new OracleParameter("p_fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fdate;
            (param[3] = new OracleParameter("p_tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _todate;
            (param[4] = new OracleParameter("p_sts", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _sts;
            (param[5] = new OracleParameter("p_job", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _job;
            (param[6] = new OracleParameter("p_order", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _order;
            (param[7] = new OracleParameter("p_doc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _docno;
            param[8] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "sp_getsupclaimheader", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<Service_WCN_Hdr>(_dtResults, Service_WCN_Hdr.Converter);
            }
            return _list;
        }

        // Nadeeka
        public List<Service_WCN_Hdr> getWCNHeaderCheck(string _com, string _loc, DateTime _fdate, DateTime _todate, string _sts, string _job,string _doc)
        {
            List<Service_WCN_Hdr> _list = null;
            OracleParameter[] param = new OracleParameter[8];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            (param[2] = new OracleParameter("p_fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fdate;
            (param[3] = new OracleParameter("p_tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _todate;
            (param[4] = new OracleParameter("p_sts", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _sts;
            (param[5] = new OracleParameter("p_job", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _job;
            (param[6] = new OracleParameter("p_doc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _doc;
             param[7] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "sp_getSupClaimHeaderCheck", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<Service_WCN_Hdr>(_dtResults, Service_WCN_Hdr.Converter);
            }
            return _list;
        }


        //Nadeeka
        public List<Service_WCN_Hdr> getWCNHeader_basedRef(string _com, string _ref)
        {
            List<Service_WCN_Hdr> _list = null;
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_ref", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ref;
 
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "sp_getSupClaimHdr", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<Service_WCN_Hdr>(_dtResults, Service_WCN_Hdr.Converter);
            }
            return _list;
        }



        //Nadeeka
        public DataTable getWCNHeaderBrand(string _com, string _loc, DateTime _fdate, DateTime _todate, string _sts)
        {

            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            (param[2] = new OracleParameter("p_fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fdate;
            (param[3] = new OracleParameter("p_tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _todate;
            (param[4] = new OracleParameter("p_sts", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _sts;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "sp_getSupClaimHeaderBrand", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        //Nadeeka
        public DataTable getWCNTypes(string _type)
        {

            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_TYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _type;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "sp_getSupClaimTypes", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        // Nadeeka 29-12-2014
        public List<Service_job_Det> sp_getSupClaimDetails(string _claimNo)
        {
            List<Service_job_Det> _list = null;
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_ref", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _claimNo;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblData = QueryDataTable("tbData", "sp_getSupClaimDetails", CommandType.StoredProcedure, false, param);

            if (_tblData.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<Service_job_Det>(_tblData, Service_job_Det.ConverterClaimDetails);
            }
            return _list;
        }

        //Tharaka 2014-10-13
        public DataTable getServiceJobEmployees(string jobNo, Int32 lineNo)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_JOBNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobNo;
            (param[1] = new OracleParameter("P_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lineNo;
            param[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_ALL_EMP", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable getServiceJobUser(string _UserID)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _UserID;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblUser", "SP_GET_USER_BY_USERID", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        //Tharaka 2014-10-16
        public DataTable getEstimateTypes(string TYPE)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_TYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = TYPE;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "GET_ESTIMATE_TYPE", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        //17/10/2014 shalika
        public DataTable GetUtilitiDetails(string com)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_Com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_UTILITY_DET", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        //17/10/2014 shalika
        public DataTable CheckDefault(string com, string party_type, string loc)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_Com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("p_party_ty", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = party_type;
            (param[2] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = loc;
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_CheckDefault", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        //Tharaka 2014-10-23
        public Int32 Save_estimateHeader(Service_Estimate_Header oHeader)
        {
            OracleParameter[] param = new OracleParameter[28];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_ESH_SEQ_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oHeader.ESH_SEQ_NO;
            (param[1] = new OracleParameter("P_ESH_ESTNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.ESH_ESTNO;
            (param[2] = new OracleParameter("P_ESH_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = oHeader.ESH_DT;
            (param[3] = new OracleParameter("P_ESH_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.ESH_COM;
            (param[4] = new OracleParameter("P_ESH_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.ESH_LOC;
            (param[5] = new OracleParameter("P_EST_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.EST_PC;
            (param[6] = new OracleParameter("P_ESH_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.ESH_TP;
            (param[7] = new OracleParameter("P_ESH_JOB_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.ESH_JOB_NO;
            (param[8] = new OracleParameter("P_EST_STUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.EST_STUS;
            (param[9] = new OracleParameter("P_EST_APP", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oHeader.EST_APP;
            (param[10] = new OracleParameter("P_EST_APP_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.EST_APP_BY;
            (param[11] = new OracleParameter("P_EST_APP_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = oHeader.EST_APP_DT;
            (param[12] = new OracleParameter("P_EST_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.EST_RMK;
            (param[13] = new OracleParameter("P_EST_MAN_REF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.EST_MAN_REF;
            (param[14] = new OracleParameter("P_EST_DURATION", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oHeader.EST_DURATION;
            (param[15] = new OracleParameter("P_EST_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.EST_CRE_BY;
            (param[16] = new OracleParameter("P_EST_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = oHeader.EST_CRE_DT;
            (param[17] = new OracleParameter("P_EST_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.EST_MOD_BY;
            (param[18] = new OracleParameter("P_EST_MOD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = oHeader.EST_MOD_DT;
            (param[19] = new OracleParameter("P_EST_PRINT_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.EST_PRINT_RMK;
            (param[20] = new OracleParameter("P_EST_CUST_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.EST_CUST_CD;
            (param[21] = new OracleParameter("P_EST_CUST_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.EST_CUST_TP;
            (param[22] = new OracleParameter("P_EST_IS_TAX", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = oHeader.EST_IS_TAX;
            (param[23] = new OracleParameter("P_EST_TAX_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.EST_TAX_NO;
            (param[24] = new OracleParameter("P_EST_TAX_EX", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = oHeader.EST_TAX_EX;
            (param[25] = new OracleParameter("P_EST_IS_SVAT", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = oHeader.EST_IS_SVAT;
            (param[26] = new OracleParameter("P_EST_SVAT_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.EST_SVAT_NO;
            param[27] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_SAVE_SERVICE_ESTMT_HDR", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2014-10-23
        public Int32 Save_estimateItem(Service_Estimate_Item oItem)
        {
            OracleParameter[] param = new OracleParameter[29];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_ESI_SEQ_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.ESI_SEQ_NO;
            (param[1] = new OracleParameter("P_ESI_ESTNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.ESI_ESTNO;
            (param[2] = new OracleParameter("P_ESI_REQNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.ESI_REQNO;
            (param[3] = new OracleParameter("P_ESI_REQLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.ESI_REQLINE;
            (param[4] = new OracleParameter("P_ESI_JOBNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.ESI_JOBNO;
            (param[5] = new OracleParameter("P_ESI_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.ESI_JOBLINE;
            (param[6] = new OracleParameter("P_ESI_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.ESI_LINE;
            (param[7] = new OracleParameter("P_ESI_ITM_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.ESI_ITM_CD;
            (param[8] = new OracleParameter("P_ESI_ITM_STUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.ESI_ITM_STUS;
            (param[9] = new OracleParameter("P_ESI_ITM_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.ESI_ITM_TP;
            (param[10] = new OracleParameter("P_ESI_UOM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.ESI_UOM;
            (param[11] = new OracleParameter("P_ESI_QTY", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.ESI_QTY;
            (param[12] = new OracleParameter("P_ESI_PB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.ESI_PB;
            (param[13] = new OracleParameter("P_ESI_PLVL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.ESI_PLVL;
            (param[14] = new OracleParameter("P_ESI_PBPRICE", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = oItem.ESI_PBPRICE;
            (param[15] = new OracleParameter("P_ESI_UNIT_RT", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = oItem.ESI_UNIT_RT;
            (param[16] = new OracleParameter("P_ESI_DISC_RT", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = oItem.ESI_DISC_RT;
            (param[17] = new OracleParameter("P_ESI_DISC_AMT", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = oItem.ESI_DISC_AMT;
            (param[18] = new OracleParameter("P_ESI_TAX_AMT", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = oItem.ESI_TAX_AMT;
            (param[19] = new OracleParameter("P_ESI_NET", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = oItem.ESI_NET;
            (param[20] = new OracleParameter("P_ESI_SEQ", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = oItem.ESI_SEQ;
            (param[21] = new OracleParameter("P_ESI_ITM_SEQ", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = oItem.ESI_ITM_SEQ;
            (param[22] = new OracleParameter("P_ESI_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.ESI_RMK;
            (param[23] = new OracleParameter("P_ESI_ISSUE_QTY", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = oItem.ESI_ISSUE_QTY;
            (param[24] = new OracleParameter("P_ESI_UNIT_COST", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = oItem.ESI_UNIT_COST;
            (param[25] = new OracleParameter("P_ESI_PRINT", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = oItem.ESI_PRINT;
            (param[26] = new OracleParameter("P_ESI_ACTIVE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.ESI_ACTIVE;
            (param[27] = new OracleParameter("p_ESI_ITM_DESC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.ESI_ITM_Description;
            param[28] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_SAVE_SERVICE_ESTMT_ITEM", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2014-10-23
        public Int32 Save_estimateTAX(Service_Estimate_Tax oItem)
        {
            OracleParameter[] param = new OracleParameter[10];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_esv_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.ESV_SEQ_NO;
            (param[1] = new OracleParameter("P_esv_estno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.ESV_ESTNO;
            (param[2] = new OracleParameter("P_esv_itm_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.ESV_ITM_LINE;
            (param[3] = new OracleParameter("P_esv_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.ESV_LINE;
            (param[4] = new OracleParameter("P_esv_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.ESV_ITM_CD;
            (param[5] = new OracleParameter("P_esv_tax_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.ESV_TAX_TP;
            (param[6] = new OracleParameter("P_esv_tax_rt", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.ESV_TAX_RT;
            (param[7] = new OracleParameter("P_esv_tax_amt", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.ESV_TAX_AMT;
            (param[8] = new OracleParameter("P_esv_active", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.ESV_ACTIVE;
            param[9] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_SAVE_SERVICE_TAX", CommandType.StoredProcedure, param);
            return effects;
        }

        //kapila
        public Int32 SAVE_SCV_JOBCUS_FEED(string _sjf_jobno, Int32 _sjf_jobline, DateTime _sjf_date, string _sjf_cuscd, string _sjf_feedback, string _sjf_cre_by, Int32 _sjf_jb_type, Int32 sjf_jb_stage, Int32 sjf_infm_all, Int32 sjf_infm_tech, Int32 sjf_is_sms)
        {
            OracleParameter[] param = new OracleParameter[12];
            Int32 effects = 0;
            (param[0] = new OracleParameter("p_sjf_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _sjf_jobno;
            (param[1] = new OracleParameter("p_sjf_jobline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _sjf_jobline;
            (param[2] = new OracleParameter("p_sjf_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = _sjf_date;
            (param[3] = new OracleParameter("p_sjf_cuscd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _sjf_cuscd;
            (param[4] = new OracleParameter("p_sjf_feedback", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _sjf_feedback;
            (param[5] = new OracleParameter("p_sjf_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _sjf_cre_by;
            (param[6] = new OracleParameter("p_job_type", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _sjf_jb_type;
            (param[7] = new OracleParameter("p_job_stage", OracleDbType.Int32, null, ParameterDirection.Input)).Value = sjf_jb_stage;

            (param[8] = new OracleParameter("p_infm_all", OracleDbType.Int32, null, ParameterDirection.Input)).Value = sjf_infm_all;
            (param[9] = new OracleParameter("p_infm_tech", OracleDbType.Int32, null, ParameterDirection.Input)).Value = sjf_infm_tech;
            (param[10] = new OracleParameter("p_infm_is_sms", OracleDbType.Int32, null, ParameterDirection.Input)).Value = sjf_is_sms;

            param[11] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_save_cust_coment", CommandType.StoredProcedure, param);
            return effects;
        }
        
        //Tharaka 2014-10-24
        public Service_Estimate_Header GetServiceEstimateHeader(string EstimateNo, string com)
        {
            Service_Estimate_Header result = new Service_Estimate_Header();
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("P_ESTNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = EstimateNo;
            (_para[1] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            _para[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobsDefects", "SP_GET_SEVICE_EST_HEADER", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Estimate_Header>(_dtResults, Service_Estimate_Header.Converter)[0];
            }
            return result;
        }

        //Tharaka 2014-10-24
        public List<Service_Estimate_Item> GetServiceEstimateItems(string EstimateNo)
        {
            List<Service_Estimate_Item> result = new List<Service_Estimate_Item>();
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("P_ESTNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = EstimateNo;
            _para[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobsDefects", "SP_GET_SERVICE_ESTMT_ITEMS", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Estimate_Item>(_dtResults, Service_Estimate_Item.Converter);
            }
            return result;
        }

        //Tharaka 2014-10-23
        public Int32 Update_Estimate_itemStatus(Int32 status, string EstimateNo)
        {
            OracleParameter[] param = new OracleParameter[3];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_STAUTS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = status;
            (param[1] = new OracleParameter("P_ESTNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = EstimateNo;
            param[2] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDATE_SCV_EST_ITEM_STATUS", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2014-10-23
        public Int32 MoveToLogTables(Int32 SeqNo)
        {
            OracleParameter[] param = new OracleParameter[2];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_ESI_SEQ_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = SeqNo;
            param[1] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_SAVE_SERVICE_ESTMT_ITEM_LOG", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2014-10-23
        public Int32 Update_Estimate_HEaderStatus(string status, string EstimateNo, string user)
        {
            OracleParameter[] param = new OracleParameter[4];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_STUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = status;
            (param[1] = new OracleParameter("P_ESTNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = EstimateNo;
            (param[2] = new OracleParameter("P_USER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = user;
            param[3] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDATE_SCV_EST_HDR_STATUS", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2014-10-27
        public Int32 Update_Estimate_HEaderStatus(Int32 status, Int32 seq, string itemCode)
        {
            OracleParameter[] param = new OracleParameter[4];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = seq;
            (param[1] = new OracleParameter("P_STATUS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = status;
            (param[2] = new OracleParameter("P_ESV_ITM_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = itemCode;
            param[3] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDATE_SERVICE_EST_TAX_STUS", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2014-10-27
        public Int32 Update_Job_dates(string jobNo, Int32 lineNo, DateTime techStart, DateTime techEnd, DateTime techStartMAn, DateTime techEndMan)
        {
            OracleParameter[] param = new OracleParameter[7];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_TECHST_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = techStart;
            (param[1] = new OracleParameter("P_TECHFIN_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = techEnd;
            (param[2] = new OracleParameter("P_TECHST_DT_MAN", OracleDbType.Date, null, ParameterDirection.Input)).Value = techStartMAn;
            (param[3] = new OracleParameter("P_TECHFIN_DT_MAN", OracleDbType.Date, null, ParameterDirection.Input)).Value = techEndMan;
            (param[4] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobNo;
            (param[5] = new OracleParameter("P_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lineNo;
            param[6] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDATE_SCV_JOB_DET_DATES", CommandType.StoredProcedure, param);
            return effects;
        }

        //Nadeeka 19-03-2015
        public Int32 Update_Job_trans(string jobNo, Int32 lineNo, string _loc, string _sjob, decimal _stage, string _pc)
        {
            OracleParameter[] param = new OracleParameter[7];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            (param[1] = new OracleParameter("P_SJOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _sjob;
            (param[2] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            (param[3] = new OracleParameter("P_STAGE", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _stage;
            (param[4] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobNo;
            (param[5] = new OracleParameter("P_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lineNo;
            param[6] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDATE_SCV_JOB_DET_TRANS", CommandType.StoredProcedure, param);
            return effects;
        }
        //Nadeeka 19-03-2015
        public Int32 Update_Job_trans_mrn(string jobNo, Int32 lineNo, string sts)
        {
            OracleParameter[] param = new OracleParameter[4];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_STS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = sts;
            (param[1] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobNo;
            (param[2] = new OracleParameter("P_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lineNo;
            param[3] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDATE_TRANSFER_REQUEST", CommandType.StoredProcedure, param);
            return effects;
        }
        //Tharaka 2014-10-30
        public DataTable GetServiceWIPMRNLocation(string Com, string Loc)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[1] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Loc;
            param[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_WIP_MRN_LOC", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        //Sanjeewa 2017-02-25
        public DataTable checkAppMRNforJob(string _job)
        {
            OracleParameter[] param = new OracleParameter[2];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("in_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _job;

            DataTable _dtResults = QueryDataTable("tbl", "sp_check_app_mrn_at_jobcls", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        //kapila
        public DataTable GetCustJobFeedback(string _jobno)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobno;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "sp_get_jobcus_feedbk", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        //Tharaka 2014-10-31
        public DataTable GetMRNItemsByJobline(string Com, string job, Int32 jobline)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[1] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (param[2] = new OracleParameter("P_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = jobline;
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_SCV_MRN_GET_ITM_BY_JOB_LINE", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        //Tharaka 2014-11-04
        public Int32 Update_ReqHeaderStatus(string STATUS, string USER, string COM, string MRN)
        {
            OracleParameter[] param = new OracleParameter[5];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_STATUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = STATUS;
            (param[1] = new OracleParameter("P_USER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = USER;
            (param[2] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = COM;
            (param[3] = new OracleParameter("P_MRN", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = MRN;
            param[4] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDATE_INT_HDR_STUS", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2014-10-31
        public List<Service_stockReturn> Get_ServiceWIP_StockReturnItems(string Com, string job, Int32 jobline, string Item, string LOC)
        {
            List<Service_stockReturn> result = new List<Service_stockReturn>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[1] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = LOC;
            (param[2] = new OracleParameter("P_JOb", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (param[3] = new OracleParameter("P_Item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item;
            (param[4] = new OracleParameter("P_JobLine", OracleDbType.Int32, null, ParameterDirection.Input)).Value = jobline;
            param[5] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_STOCKRETURN_ITEMS", CommandType.StoredProcedure, false, param);
            List<Service_stockReturn> result2 = new List<Service_stockReturn>();
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_stockReturn>(_dtResults, Service_stockReturn.Converter2);

                if (result.FindAll(x => x.INB_ITM_STUS != "STDBY").Count > 0)
                {
                    result2 = result.FindAll(x => x.INB_ITM_STUS != "STDBY");
                }
            }
            return result2;
        }

        //Tharka 2014-11-08
        public Int32 Save_OldParts(Service_OldPartRemove Item)
        {
            OracleParameter[] param = new OracleParameter[36];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_SOP_SEQNO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.SOP_SEQNO;
            (param[1] = new OracleParameter("P_SOP_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = Item.SOP_DT;
            (param[2] = new OracleParameter("P_SOP_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.SOP_COM;
            (param[3] = new OracleParameter("P_SOP_JOBNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.SOP_JOBNO;
            (param[4] = new OracleParameter("P_SOP_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.SOP_JOBLINE;
            (param[5] = new OracleParameter("P_SOP_OLDITMCD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.SOP_OLDITMCD;
            (param[6] = new OracleParameter("P_SOP_OLDITMSTUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.SOP_OLDITMSTUS;
            (param[7] = new OracleParameter("P_SOP_OLDITMSER1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.SOP_OLDITMSER1;
            (param[8] = new OracleParameter("P_SOP_OLDITMWARR", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.SOP_OLDITMWARR;
            (param[9] = new OracleParameter("P_SOP_OLDSERID", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.SOP_OLDSERID;
            (param[10] = new OracleParameter("P_SOP_OLDITMQTY", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.SOP_OLDITMQTY;
            (param[11] = new OracleParameter("P_SOP_DOC_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.SOP_DOC_NO;
            (param[12] = new OracleParameter("P_SOP_IS_SETTLED", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.SOP_IS_SETTLED;
            (param[13] = new OracleParameter("P_SOP_BASE_DOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.SOP_BASE_DOC;
            (param[14] = new OracleParameter("P_SOP_DOCLINENO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.SOP_DOCLINENO;
            (param[15] = new OracleParameter("P_SOP_REQUESTNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.SOP_REQUESTNO;
            (param[16] = new OracleParameter("P_SOP_REQWCN", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.SOP_REQWCN;
            (param[17] = new OracleParameter("P_SOP_SENTWCN", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.SOP_SENTWCN;
            (param[18] = new OracleParameter("P_SOP_RECWNC", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.SOP_RECWNC;
            (param[19] = new OracleParameter("P_SOP_TAKEWCN", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.SOP_TAKEWCN;
            (param[20] = new OracleParameter("P_SOP_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.SOP_CRE_BY;
            (param[21] = new OracleParameter("P_SOP_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = Item.SOP_CRE_DT;
            (param[22] = new OracleParameter("P_SOP_REQWCN_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = Item.SOP_REQWCN_DT;
            (param[23] = new OracleParameter("P_SOP_REQWCN_SYSDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = Item.SOP_REQWCN_SYSDT;
            (param[24] = new OracleParameter("P_SOP_TAKEWCN_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = Item.SOP_TAKEWCN_DT;
            (param[25] = new OracleParameter("P_SOP_TAKEWCN_SYSDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = Item.SOP_TAKEWCN_SYSDT;
            (param[26] = new OracleParameter("P_SOP_PART_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.SOP_PART_CD;
            (param[27] = new OracleParameter("P_SOP_OEM_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.SOP_OEM_NO;
            (param[28] = new OracleParameter("P_SOP_CASE_ID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.SOP_CASE_ID;
            (param[29] = new OracleParameter("P_SOP_REFIX", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.SOP_REFIX;
            (param[30] = new OracleParameter("P_SOP_REFIX_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = Item.SOP_REFIX_DT;
            (param[31] = new OracleParameter("P_SOP_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.SOP_RMK;
            (param[32] = new OracleParameter("P_SOP_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.SOP_TP;
            (param[33] = new OracleParameter("P_SOP_ISSUPPWARR", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.SOP_ISSUPPWARR;
            (param[34] = new OracleParameter("P_SOP_REQWCN_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.SOP_REQWCN_BY;
            param[35] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_SAVE_SCV_OLDPART", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharka 2014-11-08
        public List<Service_OldPartRemove> Get_SCV_Oldparts(string jobNumber, Int32 lineNumber, string itemCode, string serial)
        {
            List<Service_OldPartRemove> result = new List<Service_OldPartRemove>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobNumber;
            (param[1] = new OracleParameter("P_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lineNumber;
            (param[2] = new OracleParameter("P_ITMECODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = itemCode;
            (param[3] = new OracleParameter("P_SERIAL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = serial;
            param[4] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_OLDPART_BY_JOBLINE", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_OldPartRemove>(_dtResults, Service_OldPartRemove.Converter);
            }
            return result;
        }

        //08/11/2014 shalika
        public DataTable GetCatogeryDetails(string code)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_Com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GetCatogeryDetails", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        //10/11/2014 shalika
        public DataTable GetSupplierDetails(string code, string com)
        {
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("P_CODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
            (_para[1] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            _para[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblsupdetails", "sp_get_sup_details", CommandType.StoredProcedure, false, _para);
            return _dtResults;
        }

        //Tharka 2014-11-08
        public Int32 Update_SCV_Oldpart_Refix(Service_OldPartRemove Item)
        {
            OracleParameter[] param = new OracleParameter[4];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_SOP_SEQNO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.SOP_SEQNO;
            (param[1] = new OracleParameter("P_SOP_REFIX", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.SOP_REFIX;
            (param[2] = new OracleParameter("P_QTY", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.SOP_OLDITMQTY;
            param[3] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDATE_SCV_OLDPART_REFIX", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharka 2014-11-11
        public Int32 GetEstimateSeq()
        {
            OracleParameter[] param = new OracleParameter[1];
            Int32 effects = 0;

            param[0] = new OracleParameter("O_SEQ", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_GETESTIMATESEQ", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharka 2014-11-11
        public Int32 GetTempIssueSeq()
        {
            OracleParameter[] param = new OracleParameter[1];
            Int32 effects = 0;

            param[0] = new OracleParameter("O_SEQ", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_gettempissueseq", CommandType.StoredProcedure, param);
            return effects;
        }

        //Nadeeka 2014-12-31
        public Int32 GetSupClaimSeq()
        {
            OracleParameter[] param = new OracleParameter[1];
            Int32 effects = 0;

            param[0] = new OracleParameter("O_SEQ", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_GETSUPCLAIMSEQ", CommandType.StoredProcedure, param);
            return effects;
        }


        //Nadeeka 2015-03-17
        public Int32 GetJobTransSeq()
        {
            OracleParameter[] param = new OracleParameter[1];
            Int32 effects = 0;

            param[0] = new OracleParameter("O_SEQ", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_getJobTransseq", CommandType.StoredProcedure, param);
            return effects;
        }

        //Nadeeka 2015-03-17
        public Int32 GetJobsmsSeq()
        {
            OracleParameter[] param = new OracleParameter[1];
            Int32 effects = 0;

            param[0] = new OracleParameter("O_SEQ", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_getJobsmsSeq", CommandType.StoredProcedure, param);
            return effects;
        }

        //Nadeeka 2015-01-02
        public Int32 Save_scv_JobTransfer(SCV_TRANS_LOG Item)
        {
            OracleParameter[] param = new OracleParameter[10];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_STL_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.Stl_seq;
            (param[1] = new OracleParameter("STL_JOBSEQ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.Stl_jobseq;
            (param[2] = new OracleParameter("P_STL_JOBNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.Stl_jobno;
            (param[3] = new OracleParameter("P_STL_JOBLINE", OracleDbType.Int16, null, ParameterDirection.Input)).Value = Item.Stl_jobline;
            (param[4] = new OracleParameter("P_STL_SJOBNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.Stl_sjobno;
            (param[5] = new OracleParameter("P_STL_CUR_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.Stl_cur_loc;
            (param[6] = new OracleParameter("P_STL_FROM_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.Stl_from_loc;
            (param[7] = new OracleParameter("P_STL_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.Stl_cre_by;
            (param[8] = new OracleParameter("P_STL_TRNS_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = Item.Stl_trns_dt;
            param[9] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_save_scv_trans_log", CommandType.StoredProcedure, param);
            return effects;
        }
        //Nadeeka 2015-01-02
        public Int32 Save_scv_SupplierClaim(Service_WCN_Hdr Item)
        {
            OracleParameter[] param = new OracleParameter[25];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_SWC_SEQ_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.Swc_seq_no;
            (param[1] = new OracleParameter("P_SWC_DOC_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.Swc_doc_no;
            (param[2] = new OracleParameter("P_SWC_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = Item.Swc_dt;
            (param[3] = new OracleParameter("P_SWC_TP", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.Swc_tp;
            (param[4] = new OracleParameter("P_SWC_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.Swc_com;
            (param[5] = new OracleParameter("P_SWC_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.Swc_loc;
            (param[6] = new OracleParameter("P_SWC_SUPP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.Swc_supp;
            (param[7] = new OracleParameter("P_SWC_CLM_SUPP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.Swc_clm_supp;
            (param[8] = new OracleParameter("P_SWC_SUPP_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.Swc_supp_tp;
            (param[9] = new OracleParameter("P_SWC_OTHDOCNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.Swc_othdocno;
            (param[10] = new OracleParameter("P_SWC_RMKS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.Swc_rmks;
            (param[11] = new OracleParameter("P_SWC_AIR_BILL_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.Swc_air_bill_no;
            (param[12] = new OracleParameter("P_SWC_BILL_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = Item.Swc_bill_dt;
            (param[13] = new OracleParameter("P_SWC_ISPICK", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.Swc_ispick;
            (param[14] = new OracleParameter("P_SWC_STUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.Swc_stus;
            (param[15] = new OracleParameter("P_SWC_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.Swc_cre_by;
            (param[16] = new OracleParameter("P_SWC_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = Item.Swc_cre_dt;
            (param[17] = new OracleParameter("P_SWC_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.Swc_mod_by;
            (param[18] = new OracleParameter("P_SWC_MOD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = Item.Swc_mod_dt;
            (param[19] = new OracleParameter("P_SWC_ISEMAIL", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.Swc_isemail;
            (param[20] = new OracleParameter("P_SWC_ORDER_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.Swc_order_no;
            (param[21] = new OracleParameter("P_SWC_REC_TYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.SWC_REC_TYPE;
            (param[22] = new OracleParameter("P_SWC_HOLD_REASON", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.SWC_HOLD_REASON;
            (param[23] = new OracleParameter("P_SWC_NEED_CHK", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.Swc_need_chk;
             param[24] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_SAVE_SCV_WCN_HDR", CommandType.StoredProcedure, param);
            return effects;
        }

        public Int32 Save_scv_SupplierClaimDet(Service_WCN_Detail Item)
        {
            OracleParameter[] param = new OracleParameter[24];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_SWD_SEQ_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.SWD_SEQ_NO;
            (param[1] = new OracleParameter("P_SWD_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.SWD_LINE;
            (param[2] = new OracleParameter("P_SWD_DOC_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.SWD_DOC_NO;
            (param[3] = new OracleParameter("P_SWD_ITMCD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.SWD_ITMCD;
            (param[4] = new OracleParameter("P_SWD_SUPPITMCD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.SWD_SUPPITMCD;
            (param[5] = new OracleParameter("P_SWD_SER1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.SWD_SER1;
            (param[6] = new OracleParameter("P_SWD_SERID", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.SWD_SERID;
            (param[7] = new OracleParameter("P_SWD_WARRNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.SWD_WARRNO;
            (param[8] = new OracleParameter("P_SWD_SUPPWARRNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.SWD_SUPPWARRNO;
            (param[9] = new OracleParameter("P_SWD_OEMSERNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.SWD_OEMSERNO;
            (param[10] = new OracleParameter("P_SWD_CASEID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.SWD_CASEID;
            (param[11] = new OracleParameter("P_SWD_SETTLED", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.SWD_SETTLED;
            (param[12] = new OracleParameter("P_SWD_OTHDOCNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.SWD_OTHDOCNO;
            (param[13] = new OracleParameter("P_SWD_ISWCRN", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.SWD_ISWCRN;
            (param[14] = new OracleParameter("P_SWD_ISJOBCLOSE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.SWD_ISJOBCLOSE;
            (param[15] = new OracleParameter("P_SWD_JOBNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.SWD_JOBNO;
            (param[16] = new OracleParameter("P_SWD_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.SWD_JOBLINE;
            (param[17] = new OracleParameter("P_SWD_OLDPARTSEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.SWD_OLDPARTSEQ;
            (param[18] = new OracleParameter("P_SWD_QTY", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.SWD_QTY;
            (param[19] = new OracleParameter("P_SWD_ITM_STUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.SWD_ITM_STUS;
            (param[20] = new OracleParameter("p_swd_jobno_prv", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.SWD_JOBNO_PRV;
            (param[21] = new OracleParameter("p_swd_jobline_prv", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.SWD_JOBLINE_PRV;
            (param[22] = new OracleParameter("p_swd_need_chk", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.Swd_need_chk;
            param[23] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_save_scv_wcn_det", CommandType.StoredProcedure, param);
            return effects;
        }
        //Nadeeka 14-02-2015
        public int UpdateSupplierClaimWarrantySts(string _doc, string _sts)
        {
            int rows_affected = 0;
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_doc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _doc;
            (param[1] = new OracleParameter("p_STS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _sts;
            param[2] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            rows_affected = UpdateRecords("SP_UPDATE_SUPP_CLAIM", CommandType.StoredProcedure, param);
            return rows_affected;
        }


        // Nadeeka 29-01-2016
        public int UpdateSupplierClaimWarrantyCheck(string _doc, Int32  _line , string _rem , string _tech)
        {
            int rows_affected = 0;
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_doc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _doc;
            (param[1] = new OracleParameter("p_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _line;
            (param[2] = new OracleParameter("p_rem", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _rem;
            (param[3] = new OracleParameter("p_tech", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _tech;
            param[4] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            rows_affected = UpdateRecords("SP_UPDATE_SUPP_CLAIM_CHK", CommandType.StoredProcedure, param);
            return rows_affected;
        }

        // Nadeeka 26-01-2016
        public int CancelSupplierClaimWarrantyReq(string _job, Int32 _line ,Int32 _seq,string _user )
        {
            int rows_affected = 0;
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _job;
            (param[1] = new OracleParameter("p_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _line;
            (param[2] = new OracleParameter("p_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _seq;
            (param[3] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            param[4] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            rows_affected = UpdateRecords("SP_CANCEL_SUPP_CLAIM_REQ", CommandType.StoredProcedure, param);
            return rows_affected;
        }
        public int UpdateSupplierClaimWarrantyHdr(string _doc, string _supp, string _clm_supp, string _othdocno, string _rmks, string _air_bill_no, DateTime _bill_dt, string _order_no, string _hold_reason, DateTime _eta_dt)
 
        {
            int rows_affected = 0;
            OracleParameter[] param = new OracleParameter[11];
            (param[0] = new OracleParameter("p_doc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _doc;
            (param[1] = new OracleParameter("p_supp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _supp;
            (param[2] = new OracleParameter("p_clm_supp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _clm_supp;
            (param[3] = new OracleParameter("p_othdocno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _othdocno;
            (param[4] = new OracleParameter("p_rmks", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _rmks;
            (param[5] = new OracleParameter("p_air_bill_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _air_bill_no;
            (param[6] = new OracleParameter("p_bill_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _bill_dt;
            (param[7] = new OracleParameter("p_order_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _order_no;
            (param[8] = new OracleParameter("p_hold_reason", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hold_reason;
            (param[9] = new OracleParameter("p_eta", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _eta_dt;
            param[10] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            rows_affected = UpdateRecords("SP_UPDATE_SUPP_CLAIM_HDR", CommandType.StoredProcedure, param);
            return rows_affected;
        }

        //Nadeeka 29-01-2016
        public int UpdateSupplierClaimForMRN(string _job, Int32 _jobLine ,string _user ,string _rem)
        {
            int rows_affected = 0;
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _job;
            (param[1] = new OracleParameter("p_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobLine;
            (param[2] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            (param[3] = new OracleParameter("p_rem", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _rem;
            param[4] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            rows_affected = UpdateRecords("SP_UPDATE_SUP_CLAIM_REQ_MRN", CommandType.StoredProcedure, param);
            return rows_affected;
        }


        //Nadeeka 14-02-2015
        public int UpdateSupplierClaimrec(string _claimNo, Int32 _Line)
        {
            int rows_affected = 0;
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _claimNo;
            (param[1] = new OracleParameter("p_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _Line;
            param[2] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            rows_affected = UpdateRecords("sp_update_wcn_det", CommandType.StoredProcedure, param);
            return rows_affected;
        }



        //Nadeeka 14-02-2015

        //Nadeeka 14-02-2015
        public int UpdateSupplierClaimWarrantySent_job(string _job, Int32 _jobLine, Int32 _STS)
        {
            int rows_affected = 0;
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _job;
            (param[1] = new OracleParameter("p_JOBline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobLine;
            (param[2] = new OracleParameter("P_STS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _STS;
            param[3] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            rows_affected = UpdateRecords("SP_UPDATE_SUPP_CLAIM_SENT_JOB", CommandType.StoredProcedure, param);
            return rows_affected;
        }
        //Nadeeka 14-02-2015
        public int UpdateSupplierClaimWarrantySent_job_Reject(string _job, Int32 _jobLine, string _user, Int32 _sts, string _reason)
        {
            int rows_affected = 0;
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _job;
            (param[1] = new OracleParameter("p_JOBline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobLine;
            (param[2] = new OracleParameter("P_USER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            (param[3] = new OracleParameter("P_STS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _sts;
            (param[4] = new OracleParameter("p_reason", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reason;
            param[5] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            rows_affected = UpdateRecords("SP_UPDATE_SUPP_CLAIM_SENT_REJ", CommandType.StoredProcedure, param);
            return rows_affected;
        }

        public int UpdateSupplierClaimWarrantySent_job_cancel(string _job, Int32 _jobLine, string _user)
        {
            int rows_affected = 0;
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _job;
            (param[1] = new OracleParameter("p_JOBline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobLine;
            (param[2] = new OracleParameter("P_USER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;

            param[3] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            rows_affected = UpdateRecords("SP_UPDATE_SUPP_CLAIM_CANCEL", CommandType.StoredProcedure, param);
            return rows_affected;
        }

        //Nadeeka 14-02-2015
        public int UpdateSupplierClaimWarrantySent_job_oldpt(string _job, Int32 _jobLine, string _item, string _serial, Int32 _STS)
        {
            int rows_affected = 0;
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _job;
            (param[1] = new OracleParameter("p_JOBline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobLine;
            (param[2] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[3] = new OracleParameter("p_serial", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial;
            (param[4] = new OracleParameter("P_STS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _STS;
            param[5] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            rows_affected = UpdateRecords("SP_UPDATE_SUPP_CLAIM_SENT_OP", CommandType.StoredProcedure, param);
            return rows_affected;
        }
        //Nadeeka 14-02-2015
        public int UpdateSupplierClaimWarrantySent_job_oldpt_Reject(string _job, Int32 _jobLine, string _item, string _serial, string _user, Int32 _sts, string _reason, Int32 _seq)
        {
            int rows_affected = 0;
            OracleParameter[] param = new OracleParameter[9];
            (param[0] = new OracleParameter("p_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _job;
            (param[1] = new OracleParameter("p_JOBline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobLine;
            (param[2] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[3] = new OracleParameter("p_serial", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial;
            (param[4] = new OracleParameter("P_USER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            (param[5] = new OracleParameter("P_STS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _sts;
            (param[6] = new OracleParameter("p_reason", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reason;
            (param[7] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _seq;
            param[8] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            rows_affected = UpdateRecords("SP_UPDATE_SUPP_CLAIM_SENT_OPR", CommandType.StoredProcedure, param);
            return rows_affected;
        }

        //Nadeeka 14-02-2015
        public int UpdateSupplierClaimWarrantySent_job_oldpt_Cancel(string _job, Int32 _jobLine, string _item, string _serial, string _user)
        {
            int rows_affected = 0;
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _job;
            (param[1] = new OracleParameter("p_JOBline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobLine;
            (param[2] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[3] = new OracleParameter("p_serial", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial;
            (param[4] = new OracleParameter("P_USER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            param[5] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            rows_affected = UpdateRecords("SP_UPDATE_SUPP_OLD_CANCEL", CommandType.StoredProcedure, param);
            return rows_affected;
        }
        //Nadeeka 14-02-2015
        public int UpdateSupplierClaimWarrantyRec_job(string _job, Int32 _jobLine)
        {
            int rows_affected = 0;
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _job;
            (param[1] = new OracleParameter("p_JOBline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobLine;
            param[2] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            rows_affected = UpdateRecords("SP_UPDATE_SUPP_CLAIM_REC_JOB", CommandType.StoredProcedure, param);
            return rows_affected;
        }

        public int UpdateSupplierClaimWarrantyRec_job_oldpt(string _job, Int32 _jobLine, string _item, string _serial, string _nserial)
        {
            int rows_affected = 0;
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _job;
            (param[1] = new OracleParameter("p_JOBline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobLine;
            (param[2] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[3] = new OracleParameter("p_serial", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial;
            (param[4] = new OracleParameter("p_nserial", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _nserial;
            param[5] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            rows_affected = UpdateRecords("SP_UPDATE_SUPP_CLAIM_REC_OP", CommandType.StoredProcedure, param);
            return rows_affected;
        }

        //Nadeeka 29-06-2015
        public int UpdateSupplierClaimWarrantyClaimInv(string _com, string _invNo, decimal _amt)
        {
            int rows_affected = 0;
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_inv_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invNo;
            (param[2] = new OracleParameter("p_CLAIM_AMT", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _amt;
            param[3] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            rows_affected = UpdateRecords("SP_UPDATE_SUPP_CLAIM_INVOICE", CommandType.StoredProcedure, param);
            return rows_affected;
        }
        //Nadeeka 08-06-2015 
        public int UpdateSupplierClaimWarrantyRec_job_old_part(string _job, Int32 _jobLine)
        {
            int rows_affected = 0;
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _job;
            (param[1] = new OracleParameter("p_JOBline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobLine;
            param[2] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            rows_affected = UpdateRecords("SP_UPDATE_SUPP_CLAIM_OLD_PART", CommandType.StoredProcedure, param);
            return rows_affected;
        }
        //Tharka 2014-11-11
        public Int32 Save_scv_tempIssue(Service_TempIssue Item)
        {
            OracleParameter[] param = new OracleParameter[28];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_STI_SEQNO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.STI_SEQNO;
            (param[1] = new OracleParameter("P_STI_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.STI_LINE;
            (param[2] = new OracleParameter("P_STI_DOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.STI_DOC;
            (param[3] = new OracleParameter("P_STI_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = Item.STI_DT;
            (param[4] = new OracleParameter("P_STI_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.STI_COM;
            (param[5] = new OracleParameter("P_STI_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.STI_LOC;
            (param[6] = new OracleParameter("P_STI_DOC_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.STI_DOC_TP;
            (param[7] = new OracleParameter("P_STI_JOBNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.STI_JOBNO;
            (param[8] = new OracleParameter("P_STI_JOBLINENO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.STI_JOBLINENO;
            (param[9] = new OracleParameter("P_STI_ISSUEITMCD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.STI_ISSUEITMCD;
            (param[10] = new OracleParameter("P_STI_ISSUEITMSTUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.STI_ISSUEITMSTUS;
            (param[11] = new OracleParameter("P_STI_ISSUESERIALNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.STI_ISSUESERIALNO;
            (param[12] = new OracleParameter("P_STI_ISSUESERID", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.STI_ISSUESERID;
            (param[13] = new OracleParameter("P_STI_ISSUEITMQTY", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.STI_ISSUEITMQTY;
            (param[14] = new OracleParameter("P_STI_BALQTY", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.STI_BALQTY;
            (param[15] = new OracleParameter("P_STI_CROSS_SEQNO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.STI_CROSS_SEQNO;
            (param[16] = new OracleParameter("P_STI_CROSS_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.STI_CROSS_LINE;
            (param[17] = new OracleParameter("P_STI_ISRECEIVE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.STI_ISRECEIVE;
            (param[18] = new OracleParameter("P_STI_TECHCODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.STI_TECHCODE;
            (param[19] = new OracleParameter("P_STI_REFDOCNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.STI_REFDOCNO;
            (param[20] = new OracleParameter("P_STI_REFDOCLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.STI_REFDOCLINE;
            (param[21] = new OracleParameter("P_STI_STUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.STI_STUS;
            (param[22] = new OracleParameter("P_STI_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.STI_RMK;
            (param[23] = new OracleParameter("P_STI_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.STI_CRE_BY;
            (param[24] = new OracleParameter("P_STI_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = Item.STI_CRE_DT;
            (param[25] = new OracleParameter("P_STI_SEQNO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.STI_VISIT_SEQ;
            (param[26] = new OracleParameter("p_STI_SUB_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.STI_SUB_LOC;
            param[27] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_SAVE_SCV_TEMP_ISSUE_NEW", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2014-11-12
        public List<Service_TempIssue> Get_ServiceWIP_TempIssued_Items(string Com, string job, Int32 jobline, string Item, string LOC, string Type)
        {
            List<Service_TempIssue> result = new List<Service_TempIssue>();
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[1] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = LOC;
            (param[2] = new OracleParameter("P_ITEMCODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item;
            (param[3] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (param[4] = new OracleParameter("P_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = jobline;
            (param[5] = new OracleParameter("P_TYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Type;
            param[6] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_TEMPISSUE_ITEMS", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_TempIssue>(_dtResults, Service_TempIssue.Converter);

                for (int i = 0; i < _dtResults.Rows.Count; i++)
                {
                    result[i].Desc = _dtResults.Rows[i]["DESC"].ToString();
                }
            }
            return result;
        }

        //Tharaka 2014-11-12
        public List<Service_TempIssue> Get_ServiceWIP_TempIssued_Items_BY_SEQ(Int32 SEQ)
        {
            List<Service_TempIssue> result = new List<Service_TempIssue>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_SEQ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = SEQ;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_SCV_TMP_ISSUE_GET_BY_SEQ", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_TempIssue>(_dtResults, Service_TempIssue.Converter);
            }
            return result;
        }

        //THARAKA 2014-11-12
        public Int32 UPDATE_TMP_ISSUE_RETURN(decimal Qty, string doc, Int32 DocLine, Int32 Seq, Int32 lineNum, Int32 CrossSeq, Int32 CrossLine, Int32 ISRECEIVE)
        {
            OracleParameter[] param = new OracleParameter[9];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_QTY", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = Qty;
            (param[1] = new OracleParameter("P_DOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = doc;
            (param[2] = new OracleParameter("P_DOCLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = DocLine;
            (param[3] = new OracleParameter("P_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Seq;
            (param[4] = new OracleParameter("P_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lineNum;
            (param[5] = new OracleParameter("P_CROSSLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = CrossLine;
            (param[6] = new OracleParameter("P_CROSSSEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = CrossSeq;
            (param[7] = new OracleParameter("P_ISRECEIVE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = ISRECEIVE;
            param[8] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDATE_TMP_ISSUE_RETURN", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2014-11-12
        public List<Service_TempIssue> GET_TEMPISSUE_RETURNED_ITMS(string Com, string job, Int32 jobline, string Item, string LOC)
        {
            List<Service_TempIssue> result = new List<Service_TempIssue>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[1] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = LOC;
            (param[2] = new OracleParameter("P_ITEMCODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item;
            (param[3] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (param[4] = new OracleParameter("P_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = jobline;
            param[5] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_TEMPISSUE_RETURNED_ITMS", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_TempIssue>(_dtResults, Service_TempIssue.Converter);
            }
            return result;
        }

        //Tharaka 2014-11-13
        public DataTable GET_SUP_WRNT_CLM_REQ(string Com, string job, Int32 jobline)
        {
            OracleParameter[] _para = new OracleParameter[4];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (_para[1] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (_para[2] = new OracleParameter("P_JOBLINE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobline;
            _para[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblsupdetails", "SP_GET_SUP_WRNT_CLM_REQ", CommandType.StoredProcedure, false, _para);
            return _dtResults;
        }

        //Tharaka 2014-11-14
        public List<Service_supp_claim_itm> GET_TEMPISSUE_RETURNED_ITMS(string Com)
        {
            List<Service_supp_claim_itm> result = new List<Service_supp_claim_itm>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_SUPP_CLAIM_ITEM", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_supp_claim_itm>(_dtResults, Service_supp_claim_itm.Converter);
            }
            return result;
        }

        //Nadeeka 27-jan-2015
        public List<Service_supp_claim_itm> getSupClaimItems(string _com, string _sup, string _claimsup)
        {
            List<Service_supp_claim_itm> result = new List<Service_supp_claim_itm>();
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_sup", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _sup;
            (param[2] = new OracleParameter("p_claimsup", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _claimsup;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "sp_get_scv_supp_claim_supp", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_supp_claim_itm>(_dtResults, Service_supp_claim_itm.Converter);
            }
            return result;
        }
        //Nadeeka 27-jan-2015
        public List<SCV_SUPP_CLAIM_REC> getSupClaimAmt(Int32 _seq)
        {
            List<SCV_SUPP_CLAIM_REC> result = new List<SCV_SUPP_CLAIM_REC>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _seq;

            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "sp_get_scv_supp_claim_amt", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<SCV_SUPP_CLAIM_REC>(_dtResults, SCV_SUPP_CLAIM_REC.Converter);
            }
            return result;
        }

        //Tharaka 2014-11-14
        public Int32 UPDATE_SCV_SUPP_WAR_REQ(Service_SupplierWarrantyClaim item)
        {
            OracleParameter[] param = new OracleParameter[12];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_FROMTABLE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.FROMTABLE;
            (param[1] = new OracleParameter("P_DATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = item.Date;
            (param[2] = new OracleParameter("P_PARTCODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.PartID;
            (param[3] = new OracleParameter("P_OEM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.OEM;
            (param[4] = new OracleParameter("P_CASEID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.CaseID;
            (param[5] = new OracleParameter("P_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.SEQ;
            (param[6] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.JOB;
            (param[7] = new OracleParameter("P_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.JOBLINE;
            (param[8] = new OracleParameter("P_QTY", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.QTY;
            (param[9] = new OracleParameter("P_STAGE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.JobStage;
            (param[10] = new OracleParameter("P_USER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.User;
            param[11] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDATE_SCV_SUPP_WAR_REQ", CommandType.StoredProcedure, param);
            return effects;
        }

        public List<InventorySerialMaster> GetWarrantyDetails(string _invoice, string _acc, string _item, string _serial, string _warranty)
        {
            List<InventorySerialMaster> _list = null;
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_invoice", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoice;
            (param[1] = new OracleParameter("p_account", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _acc;
            (param[2] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[3] = new OracleParameter("p_serial", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial;
            (param[4] = new OracleParameter("p_warranty", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty;

            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _tblData = QueryDataTable("tblItemSimilar", "sp_getwarrantydetails", CommandType.StoredProcedure, false, param);
            if (_tblData.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<InventorySerialMaster>(_tblData, InventorySerialMaster.ConvertTotal);
            }
            return _list;
        }

        //Tharaka 2014-11-17
        public DataTable GET_SUP_WRNT_CLM_Requested(string Com, string job, Int32 jobline)
        {
            OracleParameter[] _para = new OracleParameter[4];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (_para[1] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (_para[2] = new OracleParameter("P_JOBLINE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobline;
            _para[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblsupdetails", "SP_GET_SCV_SUPP_WRNTY_REQED", CommandType.StoredProcedure, false, _para);
            return _dtResults;
        }


        // Nadeeka 25-01-2016
        public DataTable GET_SUP_WRNT_CLM_Requested_Serial(string Com, string job, Int32 jobline)
        {
            OracleParameter[] _para = new OracleParameter[4];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (_para[1] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (_para[2] = new OracleParameter("P_JOBLINE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobline;
            _para[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblsupdetails", "SP_GET_SCV_SUPP_WRN_SER", CommandType.StoredProcedure, false, _para);
            return _dtResults;
        }

        //Nadeeka 16-07-2015
        public DataTable get_Customer_Feedback(string Com, string job, Int32 jobline)
        {
            OracleParameter[] _para = new OracleParameter[4];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (_para[1] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (_para[2] = new OracleParameter("P_JOBLINE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobline;
            _para[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblsupdetails", "sp_get_customer_feedback", CommandType.StoredProcedure, false, _para);
            return _dtResults;
        }


        //Tharaka 2014-11-18
        public List<Service_WCN_Detail> GetSupWarntyClaimReveiedItems(string job, Int32 jobline)
        {
            List<Service_WCN_Detail> result = new List<Service_WCN_Detail>();
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (_para[1] = new OracleParameter("P_JOBLINE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobline;
            _para[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblsupdetails", "SP_GET_SCV_SUPP_WRNTY_RECEIVES", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_WCN_Detail>(_dtResults, Service_WCN_Detail.Converter);
            }
            return result;
        }
        // Nadeeka 
        public List<Service_WCN_Detail> GetSupWarntyClaimAvb(string job, Int32 jobline)
        {
            List<Service_WCN_Detail> result = new List<Service_WCN_Detail>();
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (_para[1] = new OracleParameter("P_JOBLINE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobline;
            _para[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblsupdetails", "sp_get_scv_supp_claim_avb", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_WCN_Detail>(_dtResults, Service_WCN_Detail.Converter);
            }
            return result;
        }

        //Darshana 2014-11-19
        public List<Service_job_Det> GetPcJobDetails(string com, string pc, string JobNo)
        {
            List<Service_job_Det> result = new List<Service_job_Det>();
            OracleParameter[] _para = new OracleParameter[4];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (_para[1] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
            (_para[2] = new OracleParameter("P_JOBNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = JobNo;
            _para[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobsDefects", "sp_getpcjobdet", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_job_Det>(_dtResults, Service_job_Det.ConverterDet);
            }
            return result;
        }

        public List<InventorySerialMaster> GetWarrantyMasterSCM2(string _item, string _ser1, string _ser2, string _regno, string _warr, string _invoice, int _serid)
        {
            List<InventorySerialMaster> _list = null;
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_serial1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ser1;
            (param[1] = new OracleParameter("p_serial2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ser2;
            (param[2] = new OracleParameter("p_regno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _regno;
            (param[3] = new OracleParameter("p_warranty", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warr;
            (param[4] = new OracleParameter("p_invoice", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoice;
            (param[5] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _tblData = QueryDataTable("tblItemSimilar", "sp_getwarrantmaster", CommandType.StoredProcedure, false, param);
            if (_tblData.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<InventorySerialMaster>(_tblData, InventorySerialMaster.ConvertTotal);
            }
            return _list;
        }

        public List<InventorySerialMaster> GetWarrantyMaster(string _item, string _ser1, string _ser2, string _regno, string _warr, string _invoice, int _serid)
        {
            List<InventorySerialMaster> _list = null;
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_serial1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ser1;
            (param[1] = new OracleParameter("p_serial2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ser2;
            (param[2] = new OracleParameter("p_regno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _regno;
            (param[3] = new OracleParameter("p_warranty", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warr;
            (param[4] = new OracleParameter("p_invoice", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoice;
            (param[5] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _tblData = QueryDataTable("tblItemSimilar", "sp_getwarrantmasterscm", CommandType.StoredProcedure, false, param);
            if (_tblData.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<InventorySerialMaster>(_tblData, InventorySerialMaster.ConvertTotal);
            }
            else
            {
                //DataTable _tblData1 = QueryDataTable("tblItemSimilar", "sp_getwarrantmaster", CommandType.StoredProcedure, false, param);
                //if (_tblData1.Rows.Count > 0)
                //{
                //    _list = DataTableExtensions.ToGenericList<InventorySerialMaster>(_tblData1, InventorySerialMaster.ConvertTotal);
                //}
            }
            return _list;
        }

        public List<InventorySerialMaster> GetWarrantyMaster_OW(string _item, string _ser1, string _ser2, string _regno, string _warr, string _invoice, int _serid)
        { //Nadeeka
            List<InventorySerialMaster> _list = null;
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_serial1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ser1;
            (param[1] = new OracleParameter("p_serial2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ser2;
            (param[2] = new OracleParameter("p_regno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _regno;
            (param[3] = new OracleParameter("p_warranty", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warr;
            (param[4] = new OracleParameter("p_invoice", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoice;
            (param[5] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _tblData = QueryDataTable("tblItemSimilar", "sp_getwarrantmasterscm", CommandType.StoredProcedure, false, param);
            if (_tblData.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<InventorySerialMaster>(_tblData, InventorySerialMaster.ConvertTotal);
            }
            else
            {
                //DataTable _tblData1 = QueryDataTable("tblItemSimilar", "sp_getwarrantmaster", CommandType.StoredProcedure, false, param);
                //if (_tblData1.Rows.Count > 0)
                //{
                //    _list = DataTableExtensions.ToGenericList<InventorySerialMaster>(_tblData1, InventorySerialMaster.ConvertTotal);
                //}
            }
            return _list;
        }


        //Chamal 03-Dec-2014
        public List<InventorySubSerialMaster> GetWarrantyMasterSub(string _warrNo, int _serid)
        {
            List<InventorySubSerialMaster> _list = null;
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_warranty", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warrNo;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _tblData = QueryDataTable("tblItemSimilar", "sp_getwarrantmastersubscm", CommandType.StoredProcedure, false, param);
            if (_tblData.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<InventorySubSerialMaster>(_tblData, InventorySubSerialMaster.ConvertTotal);
            }
            else
            {
                //SCM2 sps
            }
            return _list;
        }

        //Tharaka 2014-11-19
        public Int32 Update_SCV_WCN_DET_Settled(Int32 iSSettled, Int32 seqnum, Int32 Linenum, string job, Int32 jobLine)
        {
            OracleParameter[] param = new OracleParameter[6];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_SWD_SETTLED", OracleDbType.Int32, null, ParameterDirection.Input)).Value = iSSettled;
            (param[1] = new OracleParameter("P_SWD_SEQ_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = seqnum;
            (param[2] = new OracleParameter("P_SWD_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Linenum;
            (param[3] = new OracleParameter("P_SWD_JOBNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (param[4] = new OracleParameter("P_SWD_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = jobLine;
            param[5] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDATE_SCV_WCN_DET_SETTLED", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2014-11-19
        public Int32 SAVE_SUPP_WRNTY_RECEIVE_ITM(string Com, string job, Int32 jobLine, string serial)
        {
            OracleParameter[] param = new OracleParameter[5];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[1] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (param[2] = new OracleParameter("P_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = jobLine;
            (param[3] = new OracleParameter("P_SERIAL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = serial;
            param[4] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_SAVE_SUPP_WRNTY_RECEIVE_ITM", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2014-11-20
        public List<Service_Tech_Aloc_Hdr> GetJobAllocations_ByNewItem(string jobNo, Int32 lineNo, string com)
        {
            List<Service_Tech_Aloc_Hdr> result = new List<Service_Tech_Aloc_Hdr>();
            OracleParameter[] _para = new OracleParameter[4];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (_para[1] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobNo;
            (_para[2] = new OracleParameter("P_LINE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lineNo;
            _para[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobsDefects", "SP_GET_TECH_ALLOC_BYE_NEW_ITEM", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Tech_Aloc_Hdr>(_dtResults, Service_Tech_Aloc_Hdr.Converter);
            }
            return result;
        }

        //Tharaka 2014-10-31
        public List<Service_stockReturn> Get_ServiceWIP_ConsumableItems(string Com, string LOC, string Item)
        {
            List<Service_stockReturn> result = new List<Service_stockReturn>();
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[1] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = LOC;
            (param[2] = new OracleParameter("P_ITME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item;
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_CONSUMABLEITEMS", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_stockReturn>(_dtResults, Service_stockReturn.Converter);
            }
            return result;
        }

        //Tharka 2014-11-22
        public Int32 UPDATE_SCV_TEMPISSUE_QTY(Service_TempIssue Item)
        {
            OracleParameter[] param = new OracleParameter[9];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_ISSUEITMQTY", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.STI_ISSUEITMQTY;
            (param[1] = new OracleParameter("P_BALQTY", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.STI_BALQTY;
            (param[2] = new OracleParameter("P_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.STI_CRE_BY;
            (param[3] = new OracleParameter("P_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = Item.STI_CRE_DT;
            (param[4] = new OracleParameter("P_SEQNO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.STI_SEQNO;
            (param[5] = new OracleParameter("P_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Item.STI_LINE;
            (param[6] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.STI_COM;
            (param[7] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item.STI_LOC;
            param[8] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDATE_SCV_TEMPISSUE_QTY", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2014-11-24
        public DataTable Get_service_location(string Com, string loc)
        {
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (_para[1] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = loc;
            _para[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblsupdetails", "SP_GET_SCV_LOC", CommandType.StoredProcedure, false, _para);
            return _dtResults;
        }

        public DataTable Get_last_outwarddoc(string _item, string _serial)
        {
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("in_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (_para[1] = new OracleParameter("in_serial", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial;
            _para[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "sp_get_last_outward_doc", CommandType.StoredProcedure, false, _para);
            return _dtResults;
        }

        //Tharaka 2014-11-24
        public Int32 Update_Olppart_ReturnWarehouse(Int32 RTNWH, Int32 SEQ)
        {
            OracleParameter[] param = new OracleParameter[3];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_RTNWH", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = RTNWH;
            (param[1] = new OracleParameter("P_SEQ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = SEQ;
            param[2] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDATE_SCV_OLDPART_RTN_WH", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2014-11-24
        public Service_OldPartRemove GET_SCV_OLDPART_BY_SEQ(Int32 SEQ)
        {
            Service_OldPartRemove result = new Service_OldPartRemove();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = SEQ;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_OLDPART_BY_SEQ", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_OldPartRemove>(_dtResults, Service_OldPartRemove.Converter)[0];
            }
            return result;
        }

        //Tharaka 2014-11-25
        public Int32 Update_ScvJobDetRemark(string TechRemark, string cusRemark, string job, Int32 jobLine, string Com, string location, string closeType, String clsRemark)
        {
            OracleParameter[] param = new OracleParameter[9];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_TECH", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = TechRemark;
            (param[1] = new OracleParameter("P_CUST", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cusRemark;
            (param[2] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (param[3] = new OracleParameter("P_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = jobLine;
            (param[4] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = location;
            (param[5] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[6] = new OracleParameter("P_CLOSETYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = closeType;
            (param[7] = new OracleParameter("P_CLSREMAK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = clsRemark;
            param[8] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDATE_SCV_JOB_DET_REMARK", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2014-11-26
        public List<Service_Defect_Types> GetDefectTypes(string Com, string CHN)
        {
            List<Service_Defect_Types> result = new List<Service_Defect_Types>();
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[1] = new OracleParameter("P_CHN", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = CHN;
            param[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_DEFECT_TYPES", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Defect_Types>(_dtResults, Service_Defect_Types.Converter);
            }
            return result;
        }

        //Tharaka 2014-11-26
        public Int32 SAVE_SCV_JOB_DEFECTS(Service_Job_Defects item)
        {
            OracleParameter[] param = new OracleParameter[13];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_SRD_SEQ_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.SRD_SEQ_NO;
            (param[1] = new OracleParameter("P_SRD_JOB_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.SRD_JOB_NO;
            (param[2] = new OracleParameter("P_SRD_JOB_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.SRD_JOB_LINE;
            (param[3] = new OracleParameter("P_SRD_STAGE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.SRD_STAGE;
            (param[4] = new OracleParameter("P_SRD_DEF_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.SRD_DEF_LINE;
            (param[5] = new OracleParameter("P_SRD_DEF_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.SRD_DEF_TP;
            (param[6] = new OracleParameter("P_SRD_DEF_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.SRD_DEF_RMK;
            (param[7] = new OracleParameter("P_SRD_ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.SRD_ACT;
            (param[8] = new OracleParameter("P_SRD_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.SRD_CRE_BY;
            (param[9] = new OracleParameter("P_SRD_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = item.SRD_CRE_DT;
            (param[10] = new OracleParameter("P_SRD_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.SRD_MOD_BY;
            (param[11] = new OracleParameter("P_SRD_MOD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = item.SRD_MOD_DT;
            param[12] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_SAVE_SCV_JOB_DEFECTS", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2014-11-26
        public Int32 UPDATE_SCV_JOB_DEF_STATUS(Service_Job_Defects item, Int32 states)
        {
            OracleParameter[] param = new OracleParameter[9];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_SRD_SEQ_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.SRD_SEQ_NO;
            (param[1] = new OracleParameter("P_SRD_JOB_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.SRD_JOB_NO;
            (param[2] = new OracleParameter("P_SRD_JOB_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.SRD_JOB_LINE;
            (param[3] = new OracleParameter("P_SRD_STAGE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.SRD_STAGE;
            (param[4] = new OracleParameter("P_SRD_DEF_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.SRD_DEF_LINE;
            (param[5] = new OracleParameter("P_SRD_ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = states;
            (param[6] = new OracleParameter("P_SRD_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.SRD_MOD_BY;
            (param[7] = new OracleParameter("P_SRD_MOD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = item.SRD_MOD_DT;
            param[8] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDATE_SCV_JOB_DEF_STATUS", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2014-11-26
        public List<Service_Tech_Comment> GetTechCommtByChnnl(string Com, string CHN)
        {
            List<Service_Tech_Comment> result = new List<Service_Tech_Comment>();
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[1] = new OracleParameter("P_CHNL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = CHN;
            param[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_TECH_COMMT_BY_CHNNL", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Tech_Comment>(_dtResults, Service_Tech_Comment.Converter);
            }
            return result;
        }

        //Tharaka 2014-11-26
        public List<Service_Job_Tech_Comments> GET_SCV_TECH_CMNT(Int32 seq, string job, Int32 jobLine)
        {
            List<Service_Job_Tech_Comments> result = new List<Service_Job_Tech_Comments>();
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = seq;
            (param[1] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (param[2] = new OracleParameter("P_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = jobLine;
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_TECH_CMNT", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Job_Tech_Comments>(_dtResults, Service_Job_Tech_Comments.Converter);
            }
            return result;
        }

        //Tharaka 2014-11-26
        public Int32 SAVE_SCV_JOB_CMNT(Service_Job_Tech_Comments item)
        {
            OracleParameter[] param = new OracleParameter[12];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_JTC_SEQ_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.JTC_SEQ_NO;
            (param[1] = new OracleParameter("P_JTC_JOB_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.JTC_JOB_NO;
            (param[2] = new OracleParameter("P_JTC_JOB_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.JTC_JOB_LINE;
            (param[3] = new OracleParameter("P_JTC_CMT_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.JTC_CMT_LINE;
            (param[4] = new OracleParameter("P_JTC_CMT_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.JTC_CMT_TP;
            (param[5] = new OracleParameter("P_JTC_CMT_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.JTC_CMT_RMK;
            (param[6] = new OracleParameter("P_JTC_ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.JTC_ACT;
            (param[7] = new OracleParameter("P_JTC_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.JTC_CRE_BY;
            (param[8] = new OracleParameter("P_JTC_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = item.JTC_CRE_DT;
            (param[9] = new OracleParameter("P_JTC_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.JTC_MOD_BY;
            (param[10] = new OracleParameter("P_JTC_MOD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = item.JTC_MOD_DT;
            param[11] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_SAVE_SCV_JOB_CMNT", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2014-11-26
        public Int32 UPDATE_SCV_TECH_CMNT_STATUS(Service_Job_Tech_Comments item, Int32 status)
        {
            OracleParameter[] param = new OracleParameter[12];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_JTC_SEQ_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.JTC_SEQ_NO;
            (param[1] = new OracleParameter("P_JTC_JOB_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.JTC_JOB_NO;
            (param[2] = new OracleParameter("P_JTC_JOB_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.JTC_JOB_LINE;
            (param[3] = new OracleParameter("P_JTC_CMT_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.JTC_CMT_LINE;
            (param[4] = new OracleParameter("P_JTC_CMT_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.JTC_CMT_TP;
            (param[5] = new OracleParameter("P_JTC_CMT_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.JTC_CMT_RMK;
            (param[6] = new OracleParameter("P_JTC_ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = status;
            (param[7] = new OracleParameter("P_JTC_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.JTC_CRE_BY;
            (param[8] = new OracleParameter("P_JTC_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = item.JTC_CRE_DT;
            (param[9] = new OracleParameter("P_JTC_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.JTC_MOD_BY;
            (param[10] = new OracleParameter("P_JTC_MOD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = item.JTC_MOD_DT;
            param[11] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDATE_SCV_TECH_CMNT_STATUS", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2014-11-27
        public List<Service_Close_Type> GetServiceCloseType(string Com, string CHN, string _jobType = null)
        {
            List<Service_Close_Type> result = new List<Service_Close_Type>();
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[1] = new OracleParameter("P_CHNL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = CHN;
            (param[2] = new OracleParameter("P_JOBTYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobType;
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_CLS_TYPE_BY_CHNNL", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Close_Type>(_dtResults, Service_Close_Type.Converter);
            }
            return result;
        }

        //Tharaka 2014-11-27
        public List<Service_Job_Det_Sub> GetServiceJobDetailSubItems(string job, Int32 jobLine)
        {
            List<Service_Job_Det_Sub> result = new List<Service_Job_Det_Sub>();
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (param[1] = new OracleParameter("P_JOBLINE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobLine;
            param[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_JOB_DET_SUB", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Job_Det_Sub>(_dtResults, Service_Job_Det_Sub.Converter);
            }
            return result;

        }

        public DataTable GetServiceJobDetailSubItemsData(string job, Int32 jobLine)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (param[1] = new OracleParameter("P_JOBLINE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobLine;
            param[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_JOB_DET_SUB_NEW", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        //Tharaka 2014-11-26
        public Int32 UPDATE_SCV_JOB_DET_SUB_RPLC(Service_Job_Det_Sub item)
        {
            OracleParameter[] param = new OracleParameter[7];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_ITEM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.JBDS_ITM_CD;
            (param[1] = new OracleParameter("P_SERIALID", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.JBDS_SER1;
            (param[2] = new OracleParameter("P_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.JBDS_SEQ_NO;
            (param[3] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.JBDS_JOBNO;
            (param[4] = new OracleParameter("P_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.JBDS_JOBLINE;
            (param[5] = new OracleParameter("P_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.JBDS_LINE;
            param[6] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDATE_SCV_JOB_DET_SUB_RPLC", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2014-11-28
        public List<Service_VisitComments> GET_SCV_JOB_VISIT_COMNT(string job, Int32 jobLine)
        {
            List<Service_VisitComments> result = new List<Service_VisitComments>();
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (param[1] = new OracleParameter("P_LINE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobLine;
            param[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_JOB_VISIT_COMNT", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_VisitComments>(_dtResults, Service_VisitComments.Converter);
            }
            return result;
        }

        //Tharaka 2014-11-28
        public Int32 SaveServiceVisitiComment(Service_VisitComments item)
        {
            OracleParameter[] param = new OracleParameter[13];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_JTV_SEQ_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.JTV_SEQ_NO;
            (param[1] = new OracleParameter("P_JTV_JOB_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.JTV_JOB_NO;
            (param[2] = new OracleParameter("P_JTV_JOB_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.JTV_JOB_LINE;
            (param[3] = new OracleParameter("P_JTV_VISIT_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.JTV_VISIT_LINE;
            (param[4] = new OracleParameter("P_JTV_VISIT_FROM", OracleDbType.Date, null, ParameterDirection.Input)).Value = item.JTV_VISIT_FROM;
            (param[5] = new OracleParameter("P_JTV_VISIT_TO", OracleDbType.Date, null, ParameterDirection.Input)).Value = item.JTV_VISIT_TO;
            (param[6] = new OracleParameter("P_JTV_VISIT_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.JTV_VISIT_RMK;
            (param[7] = new OracleParameter("P_JTV_ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.JTV_ACT;
            (param[8] = new OracleParameter("P_JTV_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.JTV_CRE_BY;
            (param[9] = new OracleParameter("P_JTV_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = item.JTV_CRE_DT;
            (param[10] = new OracleParameter("P_JTV_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.JTV_MOD_BY;
            (param[11] = new OracleParameter("P_JTV_MOD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = item.JTV_MOD_DT;
            param[12] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_SAVE_SCV_VISIT_CMNT", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2014-11-28
        public Int32 UpdateServiceJobVisitStatus(Service_VisitComments item, Int32 status)
        {
            OracleParameter[] param = new OracleParameter[7];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_JTV_SEQ_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.JTV_SEQ_NO;
            (param[1] = new OracleParameter("P_JTV_JOB_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.JTV_JOB_NO;
            (param[2] = new OracleParameter("P_JTV_JOB_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.JTV_JOB_LINE;
            (param[3] = new OracleParameter("P_JTV_ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = status;
            (param[4] = new OracleParameter("P_JTV_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.JTV_MOD_BY;
            (param[5] = new OracleParameter("P_JTV_MOD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = item.JTV_MOD_DT;
            param[6] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDATE_SCV_JOB_VISITCMNT_ST", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2014-11-29
        public Int32 SAVE_SCV_JOB_VISIT_TECH(Service_job_visit_Technician item)
        {
            OracleParameter[] param = new OracleParameter[11];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_JVT_SEQ_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.JVT_SEQ_NO;
            (param[1] = new OracleParameter("P_JVT_JOB_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.JVT_JOB_NO;
            (param[2] = new OracleParameter("P_JVT_JOB_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.JVT_JOB_LINE;
            (param[3] = new OracleParameter("P_JVT_VISIT_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.JVT_VISIT_LINE;
            (param[4] = new OracleParameter("P_JVT_EMP_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.JVT_EMP_CD;
            (param[5] = new OracleParameter("P_JVT_ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.JVT_ACT;
            (param[6] = new OracleParameter("P_JVT_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.JVT_CRE_BY;
            (param[7] = new OracleParameter("P_JVT_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = item.JVT_CRE_DT;
            (param[8] = new OracleParameter("P_JVT_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.JVT_MOD_BY;
            (param[9] = new OracleParameter("P_JVT_MOD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = item.JVT_MOD_DT;
            param[10] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_SAVE_SCV_JOB_VISIT_TECH", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2014-11-29
        public List<Service_job_visit_Technician> GET_SCV_JOB_VISIT_TECH(string job, Int32 jobLine, Int32 Seq)
        {
            List<Service_job_visit_Technician> result = new List<Service_job_visit_Technician>();
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Seq;
            (param[1] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (param[2] = new OracleParameter("P_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = jobLine;
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_JOB_VISIT_TECH", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_job_visit_Technician>(_dtResults, Service_job_visit_Technician.Converter);
            }
            return result;
        }

        //Darshana 2014-12-02
        public List<InventoryBatchRefN> GetInvBalByJob(string _company, string _location, string _jobNo, Int32 _lineNo)
        {
            //sp_getbatchbalance(p_com in NVARCHAR2,p_loc in NVARCHAR2,p_item in NVARCHAR2,p_status in NVARCHAR2,c_data out sys_refcursor)
            List<InventoryBatchRefN> _result = new List<InventoryBatchRefN>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _location;
            (param[2] = new OracleParameter("p_job", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobNo;
            (param[3] = new OracleParameter("p_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _lineNo;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResult = QueryDataTable("tblGetBatchBalance", "sp_getbaljobitm", CommandType.StoredProcedure, false, param);

            if (_dtResult.Rows.Count > 0)
            {
                _result = DataTableExtensions.ToGenericList<InventoryBatchRefN>(_dtResult, InventoryBatchRefN.Converter);
            }
            return _result;
        }

        //Darshana 2014-12-03
        public List<InventorySerialRefN> GetBalSerForJob(string _com, string _loc, Int32 _seqNo, Int32 _itmLine, Int32 _itmBatLine)
        {
            //sp_getbatchbalance(p_com in NVARCHAR2,p_loc in NVARCHAR2,p_item in NVARCHAR2,p_status in NVARCHAR2,c_data out sys_refcursor)
            List<InventorySerialRefN> _result = new List<InventorySerialRefN>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            (param[2] = new OracleParameter("p_seqNo", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _seqNo;
            (param[3] = new OracleParameter("p_itmLine", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _itmLine;
            (param[4] = new OracleParameter("p_batLine", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _itmBatLine;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResult = QueryDataTable("tblGetBatchBalance", "sp_getbalserialforjob", CommandType.StoredProcedure, false, param);

            if (_dtResult.Rows.Count > 0)
            {
                _result = DataTableExtensions.ToGenericList<InventorySerialRefN>(_dtResult, InventorySerialRefN.Converter);
            }
            return _result;
        }

        //Tharaka 2014-12-01
        public List<Service_confirm_Header> GetServiceConfirmHeader(String Com, String Channal, String Location, String ProfitCenter, String JobNumber, String RequestNO, String CustomerCode, String ConfrimNum, DateTime From, DateTime To)
        {
            List<Service_confirm_Header> result = new List<Service_confirm_Header>();
            OracleParameter[] param = new OracleParameter[10];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[1] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Location;
            (param[2] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = ProfitCenter;
            (param[3] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = JobNumber;
            (param[4] = new OracleParameter("P_REQ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = RequestNO;
            (param[5] = new OracleParameter("P_CUST", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = CustomerCode;
            (param[6] = new OracleParameter("P_CONNUM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = ConfrimNum;
            (param[7] = new OracleParameter("P_FROM", OracleDbType.Date, null, ParameterDirection.Input)).Value = From;
            (param[8] = new OracleParameter("P_TO", OracleDbType.Date, null, ParameterDirection.Input)).Value = To;
            param[9] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_CONF_HDR", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_confirm_Header>(_dtResults, Service_confirm_Header.Converter);
            }
            return result;
        }

        //Tharaka 2014-12-01
        public List<Service_Confirm_detail> GetServiceConfirmDetials(Int32 Seq, String ConfirmNum)
        {
            List<Service_Confirm_detail> result = new List<Service_Confirm_detail>();
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_SEQ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Seq;
            (param[1] = new OracleParameter("P_CONNUM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = ConfirmNum;
            param[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_CONF_DET", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Confirm_detail>(_dtResults, Service_Confirm_detail.Converter);
            }
            return result;
        }

        //Tharaka 2014-12-03
        public List<Service_JOB_HDR> GetServiceJobHeaderAll(string jobNo, string com)
        {
            //Job category get as SJB_MOD_BY
            List<Service_JOB_HDR> result = new List<Service_JOB_HDR>();
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("P_JOBNUM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobNo;
            (_para[1] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            _para[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobsDefects", "SP_GET_ALL_JOB_HDR", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_JOB_HDR>(_dtResults, Service_JOB_HDR.Converter);
                for (int i = 0; i < _dtResults.Rows.Count; i++)
                {
                    result[i].SJB_JOBSTAGE_TEXT = _dtResults.Rows[i]["jbs_desc"].ToString();
                }
            }
            return result;
        }

        //Chamal 2014-12-03
        public Int32 SAVE_SCV_JOB_DET_SUB(Service_Job_Det_Sub item)
        {
            OracleParameter[] param = new OracleParameter[27];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_SEQ_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.JBDS_SEQ_NO;
            (param[1] = new OracleParameter("P_JOBNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.JBDS_JOBNO;
            (param[2] = new OracleParameter("P_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.JBDS_JOBLINE;
            (param[3] = new OracleParameter("P_SJOBNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.JBDS_SJOBNO;
            (param[4] = new OracleParameter("P_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.JBDS_LINE;
            (param[5] = new OracleParameter("P_ITM_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.JBDS_ITM_CD;
            (param[6] = new OracleParameter("P_ITM_STUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.JBDS_ITM_STUS;
            (param[7] = new OracleParameter("P_ITM_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.JBDS_ITM_TP;
            (param[8] = new OracleParameter("P_ITM_DESC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.JBDS_ITM_DESC;
            (param[9] = new OracleParameter("P_BRAND", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.JBDS_BRAND;
            (param[10] = new OracleParameter("P_MODEL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.JBDS_MODEL;
            (param[11] = new OracleParameter("P_ITM_COST", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = item.JBDS_ITM_COST;
            (param[12] = new OracleParameter("P_SER1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.JBDS_SER1;
            (param[13] = new OracleParameter("P_SER2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.JBDS_SER2;
            (param[14] = new OracleParameter("P_WARR", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.JBDS_WARR;
            (param[15] = new OracleParameter("P_WARR_PERIOD", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.JBDS_WARR_PERIOD;
            (param[16] = new OracleParameter("P_WARR_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.JBDS_WARR_RMK;
            (param[17] = new OracleParameter("P_ISSUB", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.JBDS_ISSUB;
            (param[18] = new OracleParameter("P_AVAILABILTY", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.JBDS_AVAILABILTY;
            (param[19] = new OracleParameter("P_RTN_WH", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.JBDS_RTN_WH;
            (param[20] = new OracleParameter("P_NEED_REPLACE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.JBDS_NEED_REPLACE;
            (param[21] = new OracleParameter("P_REPL_ITMCD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.JBDS_REPL_ITMCD;
            (param[22] = new OracleParameter("P_REPL_SERID", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.JBDS_REPL_SERID;
            (param[23] = new OracleParameter("P_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.JBDS_CRE_BY;
            (param[24] = new OracleParameter("P_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = item.JBDS_CRE_DT;
            (param[25] = new OracleParameter("P_QTY", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = item.JBDS_QTY;
            param[26] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDATE_SCV_JOB_DET_SUB", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2014-12-11
        public List<Service_Appove> GetJobsServiceApprove(string com, DateTime From, DateTime To, string jobno, string Stage, string customer, string PC, Int32 option, String Loc)
        {
            List<Service_Appove> result = new List<Service_Appove>();
            OracleParameter[] _para = new OracleParameter[10];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (_para[1] = new OracleParameter("P_From", OracleDbType.Date, null, ParameterDirection.Input)).Value = From;
            (_para[2] = new OracleParameter("P_TO", OracleDbType.Date, null, ParameterDirection.Input)).Value = To;
            (_para[3] = new OracleParameter("p_jonno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobno;
            (_para[4] = new OracleParameter("P_Stage", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Stage;
            (_para[5] = new OracleParameter("P_Customer", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = customer;
            (_para[6] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PC;
            (_para[7] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Loc;
            (_para[8] = new OracleParameter("P_OPTION", OracleDbType.Int32, null, ParameterDirection.Input)).Value = option;
            _para[9] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_GET_SCV_APPROVE", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Appove>(_dtResults, Service_Appove.Converter);
            }

            return result;
        }

        //Tharaka 2014-12-11
        public Int32 SAVE_LOG_COMMON_FILE(Log_Common_File item)
        {
            OracleParameter[] param = new OracleParameter[23];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_LCF_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.Lcf_seq;
            (param[1] = new OracleParameter("P_LCF_LOG_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.Lcf_log_by;
            (param[2] = new OracleParameter("P_LCF_LOG_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = item.Lcf_log_dt;
            (param[3] = new OracleParameter("P_LCF_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.Lcf_com;
            (param[4] = new OracleParameter("P_LCF_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.Lcf_pc;
            (param[5] = new OracleParameter("P_LCF_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.Lcf_loc;
            (param[6] = new OracleParameter("P_LCF_FUNC_ID", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.Lcf_func_id;
            (param[7] = new OracleParameter("P_LCF_FUNC_DOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.Lcf_func_doc;
            (param[8] = new OracleParameter("P_LCF_FUNC_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item.Lcf_func_line;
            (param[9] = new OracleParameter("P_LCF_REF_DOC1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.Lcf_ref_doc1;
            (param[10] = new OracleParameter("P_LCF_REF_DOC2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.Lcf_ref_doc2;
            (param[11] = new OracleParameter("P_LCF_REF_DOC3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.Lcf_ref_doc3;
            (param[12] = new OracleParameter("P_LCF_COL1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.Lcf_col1;
            (param[13] = new OracleParameter("P_LCF_COL1_VAL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.Lcf_col1_val;
            (param[14] = new OracleParameter("P_LCF_COL2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.Lcf_col2;
            (param[15] = new OracleParameter("P_LCF_COL2_VAL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.Lcf_col2_val;
            (param[16] = new OracleParameter("P_LCF_COL3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.Lcf_col3;
            (param[17] = new OracleParameter("P_LCF_COL3_VAL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.Lcf_col3_val;
            (param[18] = new OracleParameter("P_LCF_COL4", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.Lcf_col4;
            (param[19] = new OracleParameter("P_LCF_COL4_VAL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.Lcf_col4_val;
            (param[20] = new OracleParameter("P_LCF_COL5", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.Lcf_col5;
            (param[21] = new OracleParameter("P_LCF_COL5_VAL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item.Lcf_col5_val;
            param[22] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_SAVE_LOG_COMMON_FILE", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2014-12-11
        public Int32 UPDATE_SCV_JOB_DET_WAR_STUS(Int32 Status, Int32 seq, string jobnumber, Int32 jobLine)
        {
            OracleParameter[] param = new OracleParameter[5];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_STATUS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Status;
            (param[1] = new OracleParameter("P_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = seq;
            (param[2] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobnumber;
            (param[3] = new OracleParameter("P_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = jobLine;
            param[4] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDATE_SCV_JOB_DET_WAR_STUS", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2014-12-12
        public Int32 UPDATE_SCV_CONF_HDR_ISINVD(Int32 Status, Int32 seq, string com, String jobNum)
        {
            OracleParameter[] param = new OracleParameter[5];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_STATUS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Status;
            (param[1] = new OracleParameter("P_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = seq;
            (param[2] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[3] = new OracleParameter("P_JOBNUM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobNum;
            param[4] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDATE_SCV_CONF_HDR_ISINVD", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2014-12-12
        public Int32 UPDATE_SCV_CONF_HDR_STUS(string Status, Int32 seq, string com, string ConfirmNum)
        {
            OracleParameter[] param = new OracleParameter[5];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_STATUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Status;
            (param[1] = new OracleParameter("P_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = seq;
            (param[2] = new OracleParameter("P_CONNUM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = ConfirmNum;
            (param[3] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            param[4] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDATE_SCV_CONF_HDR_STUS", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2014-12-12
        public Int32 GET_LOG_FUNC_ID(string MAIN, string SUB)
        {
            Int32 funcId = -1;
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("P_MAIN", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = MAIN;
            (_para[1] = new OracleParameter("P_SUB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = SUB;
            _para[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobsDefects", "SP_GET_LOG_FUNC_ID", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                funcId = Convert.ToInt32(_dtResults.Rows[0][0].ToString());
            }
            return funcId;
        }

        //Tharaka 2014-12-12
        public Int32 UPDATE_SCV_JOB_DET_FOC(Int32 Status, Int32 seq, string jobnumber, Int32 jobLine)
        {
            OracleParameter[] param = new OracleParameter[5];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_STATUS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Status;
            (param[1] = new OracleParameter("P_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = seq;
            (param[2] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobnumber;
            (param[3] = new OracleParameter("P_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = jobLine;
            param[4] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDATE_SCV_JOB_DET_FOC", CommandType.StoredProcedure, param);
            return effects;
        }

        //Chamal 2014-Dec-15
        public Service_Req_Hdr GetServiceReqHeader(string _com, string _reqNo)
        {
            Service_Req_Hdr _result = new Service_Req_Hdr();
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("p_reqno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqNo;
            (_para[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            _para[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblScvReq", "sp_get_jobreq_hdr", CommandType.StoredProcedure, false, _para);
            if (_dtResults != null)
            {
                if (_dtResults.Rows.Count > 0)
                {
                    _result = DataTableExtensions.ToGenericList<Service_Req_Hdr>(_dtResults, Service_Req_Hdr.Converter)[0];
                    //result.SJB_JOBSTAGE_TEXT = _dtResults.Rows[0]["jbs_desc"].ToString();
                }
            }
            return _result;
        }

        //Chamal 2014-Dec-15
        public List<Service_Req_Det> GetServiceReqDetails(string _com, string _loc, string _reqNo, int _lineNo)
        {
            List<Service_Req_Det> result = new List<Service_Req_Det>();
            OracleParameter[] _para = new OracleParameter[5];
            (_para[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (_para[1] = new OracleParameter("p_reqno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqNo;
            (_para[2] = new OracleParameter("p_lineno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _lineNo;
            _para[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (_para[4] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            DataTable _dtResults = QueryDataTable("tblScvReqItems", "sp_get_jobreq_det", CommandType.StoredProcedure, false, _para);
            if (_dtResults != null)
            {
                if (_dtResults.Rows.Count > 0)
                {
                    result = DataTableExtensions.ToGenericList<Service_Req_Det>(_dtResults, Service_Req_Det.Converter);
                }
            }
            return result;
        }

        //Tharaka 2014-12-11
        public Log_Common_File GET_LOG_COMMN(string COM, string PC, string LOC, Int32 FUNC, string JOB, Int32 JOBLINE)
        {
            Log_Common_File result = new Log_Common_File();
            OracleParameter[] _para = new OracleParameter[7];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = COM;
            (_para[1] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PC;
            (_para[2] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = LOC;
            (_para[3] = new OracleParameter("P_FUNC", OracleDbType.Int32, null, ParameterDirection.Input)).Value = FUNC;
            (_para[4] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = JOB;
            (_para[5] = new OracleParameter("P_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = JOBLINE;
            _para[6] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_GET_LOG_COMMN", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Log_Common_File>(_dtResults, Log_Common_File.Converter)[0];
            }
            return result;
        }

        //Tharaka 2014-12-15
        public List<Service_Estimate_Header> GetEstimateApprove(string com, DateTime From, DateTime To, string jobno, string Stage, string customer, string PC, Int32 option, String Loc)
        {
            List<Service_Estimate_Header> result = new List<Service_Estimate_Header>();
            OracleParameter[] _para = new OracleParameter[10];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (_para[1] = new OracleParameter("P_From", OracleDbType.Date, null, ParameterDirection.Input)).Value = From;
            (_para[2] = new OracleParameter("P_TO", OracleDbType.Date, null, ParameterDirection.Input)).Value = To;
            (_para[3] = new OracleParameter("p_jonno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobno;
            (_para[4] = new OracleParameter("P_Stage", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Stage;
            (_para[5] = new OracleParameter("P_Customer", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = customer;
            (_para[6] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PC;
            (_para[7] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Loc;
            (_para[8] = new OracleParameter("P_OPTION", OracleDbType.Int32, null, ParameterDirection.Input)).Value = option;
            _para[9] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_GET_ESTMT_FOR_SCV_APP", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Estimate_Header>(_dtResults, Service_Estimate_Header.Converter);
            }

            return result;
        }

        //Tharaka 2014-12-16
        public List<Service_Appove_MRN> GET_MRN_FOR_JOB(string Job, string com, string Loc, string Req)
        {
            List<Service_Appove_MRN> result = new List<Service_Appove_MRN>();
            OracleParameter[] _para = new OracleParameter[5];
            (_para[0] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Job;
            (_para[1] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (_para[2] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Loc;
            (_para[3] = new OracleParameter("P_REQ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Req;
            _para[4] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_GET_MRN_FOR_JOB", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Appove_MRN>(_dtResults, Service_Appove_MRN.Converter);
            }

            return result;
        }

        public DataTable GetIntHdrByOtherDocNumber(String OtherDoc, string com)
        {
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("P_ODEC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = OtherDoc;
            (_para[1] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            _para[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblScvReqItems", "SP_GET_INT_REQ_BY_OTH_DOC", CommandType.StoredProcedure, false, _para);
            return _dtResults;
        }

        //Tharaka 2014-12-16
        public Int32 UPDATE_INT_HDR_STATUS(Int32 Seqnum, String com, String otherDoc, String Status)
        {
            OracleParameter[] param = new OracleParameter[5];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Seqnum;
            (param[1] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[2] = new OracleParameter("P_OTHERDOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = otherDoc;
            (param[3] = new OracleParameter("P_STATUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Status;
            param[4] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDATE_INT_HDR_STATUS", CommandType.StoredProcedure, param);
            return effects;
        }

        public Int32 updateMPCBWarranty(string _serial)
        {
            OracleParameter[] param = new OracleParameter[2];
            Int32 effects = 0;
            (param[0] = new OracleParameter("in_serial", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial;
            param[1] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_update_mpcb_ser", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2014-12-17
        public List<Scv_wrrt_App> GET_SCV_CUST_WRT_CLM(string Com, string Loc, string Status, string Job, DateTime From, DateTime To)
        {
            List<Scv_wrrt_App> result = new List<Scv_wrrt_App>();
            OracleParameter[] _para = new OracleParameter[7];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (_para[1] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Loc;
            (_para[2] = new OracleParameter("P_STATUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Status;
            (_para[3] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Job;
            (_para[4] = new OracleParameter("P_FROM", OracleDbType.Date, null, ParameterDirection.Input)).Value = From;
            (_para[5] = new OracleParameter("P_TO", OracleDbType.Date, null, ParameterDirection.Input)).Value = To;
            _para[6] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_GET_SCV_CUST_WRT_CLM", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Scv_wrrt_App>(_dtResults, Scv_wrrt_App.Converter);
            }

            return result;
        }

        //Tharaka 2014-12-16
        public Int32 UPDATE_INT_SER_APP_STUS(String Status, String Com, String Loc, String Job, Int32 JobLine, String User)
        {
            OracleParameter[] param = new OracleParameter[7];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_STUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Status;
            (param[1] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[2] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Loc;
            (param[3] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Job;
            (param[4] = new OracleParameter("P_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = JobLine;
            (param[5] = new OracleParameter("P_USER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = User;
            param[6] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDATE_INT_SER_APP_STUS", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2014-12-17
        public List<ComboBoxObject> GET_INV_TYPES(string Com, string Loc)
        {
            List<ComboBoxObject> result = new List<ComboBoxObject>();
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (_para[1] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Loc;
            _para[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_GET_INV_TYPES", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                for (int i = 0; i < _dtResults.Rows.Count; i++)
                {
                    ComboBoxObject oItem = new ComboBoxObject();
                    oItem.Text = _dtResults.Rows[i][0].ToString();
                    oItem.Value = _dtResults.Rows[i][0].ToString();
                    result.Add(oItem);
                }
            }
            return result;
        }

        /// <summary>
        ///  damith module services reminder
        ///  16/DEC/2014
        /// </summary>
        ///
        public List<Service_Reminder> GetServJobReminder(ServiceRreminder _servReminder)
        {
            List<Service_Reminder> returnServerRLst = new List<Service_Reminder>();
            OracleParameter[] _para = new OracleParameter[7];
            (_para[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _servReminder.comCode;
            (_para[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _servReminder.locCode;
            (_para[2] = new OracleParameter("p_msgType", OracleDbType.Int32, -1, ParameterDirection.Input)).Value = _servReminder.msgType;
            (_para[3] = new OracleParameter("p_frmDte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _servReminder.frmDate.Date;
            (_para[4] = new OracleParameter("p_toDte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _servReminder.toDate.Date;
            (_para[5] = new OracleParameter("p_jobNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _servReminder.jobNO;
            _para[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblSerReminder", "get_customer_reminder", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                returnServerRLst = DataTableExtensions.ToGenericList<Service_Reminder>(_dtResults, Service_Reminder.Converter);
            }
            return returnServerRLst;
        }

        /// <summary>
        /// Damith 17-dec-2014
        /// Gets the reminder template.
        /// </summary>
        /// <returns></returns>
        public List<Service_Reminder_Template> GetReminderTemplate()
        {
            List<Service_Reminder_Template> returnLst = new List<Service_Reminder_Template>();
            OracleParameter[] _para = new OracleParameter[1];
            _para[0] = new OracleParameter("C_OUT", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResult = QueryDataTable("tblRTemplate", "sp_get_sms_template", CommandType.StoredProcedure, false, _para);
            if (_dtResult.Rows.Count > 0)
            {
                returnLst = DataTableExtensions.ToGenericList<Service_Reminder_Template>(_dtResult, Service_Reminder_Template.Converter);
            }
            return returnLst;
        }

        //Darshana 23-12-2014
        public Int32 SAVE_PO_APPROVE(Service_Purchase_Approval _poApp)
        {
            OracleParameter[] param = new OracleParameter[15];
            Int32 effects = 0;
            (param[0] = new OracleParameter("p_posa_po_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _poApp.Posa_po_seq;
            (param[1] = new OracleParameter("p_posa_po_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _poApp.Posa_po_no;
            (param[2] = new OracleParameter("p_posa_po_itm", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _poApp.Posa_po_itm;
            (param[3] = new OracleParameter("p_posa_po_itm_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _poApp.Posa_po_itm_line;
            (param[4] = new OracleParameter("p_posa_po_del_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _poApp.Posa_po_del_line;
            (param[5] = new OracleParameter("p_posa_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _poApp.Posa_job_no;
            (param[6] = new OracleParameter("p_posa_job_itm", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _poApp.Posa_job_itm;
            (param[7] = new OracleParameter("p_posa_job_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _poApp.Posa_job_line;
            (param[8] = new OracleParameter("p_posa_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _poApp.Posa_qty;
            (param[9] = new OracleParameter("p_posa_unit_cost", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _poApp.Posa_unit_cost;
            (param[10] = new OracleParameter("p_posa_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _poApp.Posa_cre_by;
            (param[11] = new OracleParameter("p_posa_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _poApp.Posa_mod_by;
            (param[12] = new OracleParameter("p_POSA_ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _poApp.Posa_act;
            (param[13] = new OracleParameter("p_POSA_APP_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _poApp.Posa_app_dt;
            param[14] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_savescvapp", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2014-12-22
        public Service_JOB_HDR GET_SCV_JOB_HDR(string Job, string com)
        {
            Service_JOB_HDR result = new Service_JOB_HDR();
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Job;
            (_para[1] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            _para[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_GET_SCV_JOB_HDR", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_JOB_HDR>(_dtResults, Service_JOB_HDR.Converter)[0];

                if (_dtResults.Rows.Count > 0)
                {
                    result.SJB_JOBSTAGE_TEXT = _dtResults.Rows[0]["SJB_STUS_TEXT"].ToString();
                }
            }

            return result;
        }

        //Tharaka 2014-12-23
        public List<Service_Enquiry_Job_Det> GET_JOB_DET_ENQRY(String Ser1, String Ser2, String RegNum, String Com, String Loc, DateTime Fromdt, DateTime Todt)
        {
            List<Service_Enquiry_Job_Det> result = new List<Service_Enquiry_Job_Det>();
            OracleParameter[] _para = new OracleParameter[8];
            (_para[0] = new OracleParameter("P_SERIAL1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Ser1;
            (_para[1] = new OracleParameter("P_SERIAL2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Ser2;
            (_para[2] = new OracleParameter("P_REQIS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = RegNum;
            (_para[3] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (_para[4] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Loc;
            (_para[5] = new OracleParameter("P_FROM", OracleDbType.Date, null, ParameterDirection.Input)).Value = Fromdt;
            (_para[6] = new OracleParameter("P_TO", OracleDbType.Date, null, ParameterDirection.Input)).Value = Todt;
            _para[7] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_GET_JOB_DET_ENQRY", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Enquiry_Job_Det>(_dtResults, Service_Enquiry_Job_Det.Converter);
            }
            return result;
        }

        //Tharaka 2014-12-23
        public Service_Enquiry_Job_Hdr GET_SCV_JOBHDR_ENQRY(string JOb, string Com)
        {
            Service_Enquiry_Job_Hdr result = new Service_Enquiry_Job_Hdr();
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = JOb;
            (_para[1] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            _para[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_GET_SCV_JOBHDR_ENQRY", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Enquiry_Job_Hdr>(_dtResults, Service_Enquiry_Job_Hdr.Converter)[0];
            }

            return result;
        }

        //Tharaka 2014-12-24
        public List<Service_Enquiry_TechAllo_Hdr> GET_SCV_TECHALLO_ENQRY(string jobNo, Int32 lineNo, string com)
        {
            List<Service_Enquiry_TechAllo_Hdr> result = new List<Service_Enquiry_TechAllo_Hdr>();
            OracleParameter[] _para = new OracleParameter[4];
            (_para[0] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobNo;
            (_para[1] = new OracleParameter("P_JOBLINE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lineNo;
            (_para[2] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            _para[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobsDefects", "SP_GET_SCV_TECHALLO_ENQRY", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Enquiry_TechAllo_Hdr>(_dtResults, Service_Enquiry_TechAllo_Hdr.Converter);
            }
            return result;
        }

        //Chamal 29-12-2014
        public decimal GetScvJobStageRate(string _com, string _schnl, string _loc, string _scvCate, decimal _qty, DateTime _date, string _jobStage, string _item, string _fromTown, string _toTown, out string _msg)
        {
            bool _found = false;
            decimal _chg = 0;
            int _chgSeq = 0;
            Int16 _chgsub = 0;
            DataTable _d0 = new DataTable();
            OracleParameter[] param = new OracleParameter[9];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "LOC";
            (param[2] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            (param[3] = new OracleParameter("p_taskloc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scvCate;
            (param[4] = new OracleParameter("p_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _qty;
            (param[5] = new OracleParameter("p_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = _date.Date;
            (param[6] = new OracleParameter("p_js", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobStage;
            (param[7] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            param[8] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _d0 = QueryDataTable("tblZPC", "sp_GetScvChg", CommandType.StoredProcedure, false, param);

            if (_d0 != null)
            {
                if (_d0.Rows.Count > 0) _found = true;
            }

            if (_found == false)
            {
                _d0 = new DataTable();
                param = new OracleParameter[9];
                (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
                (param[1] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "SCHNL";
                (param[2] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _schnl;
                (param[3] = new OracleParameter("p_taskloc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scvCate;
                (param[4] = new OracleParameter("p_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _qty;
                (param[5] = new OracleParameter("p_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = _date.Date;
                (param[6] = new OracleParameter("p_js", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobStage;
                (param[7] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
                param[8] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                _d0 = QueryDataTable("tblZPC", "sp_GetScvChg", CommandType.StoredProcedure, false, param);

                if (_d0 != null)
                {
                    if (_d0.Rows.Count > 0) _found = true;
                }
            }

            if (_found == false)
            {
                _msg = "Invalid charge item!";
                return -1;
            }

            if (_d0.Rows.Count > 0)
            {
                for (int i = 0; i < _d0.Rows.Count; i++)
                {
                    _chgSeq = Convert.ToInt32(_d0.Rows[i]["SCG_SEQ"].ToString());
                    _chgsub = Convert.ToInt16(_d0.Rows[i]["SCG_READ_SUB"].ToString());
                    _chg = Convert.ToDecimal(_d0.Rows[i]["SCG_RATE"].ToString());
                    break;
                }
            }

            if (_chgsub == 1)
            {
                _found = false;
                _d0 = new DataTable();
                param = new OracleParameter[4];
                (param[0] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _chgSeq;
                (param[1] = new OracleParameter("p_from", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _fromTown;
                (param[2] = new OracleParameter("p_to", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _toTown;
                param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                _d0 = QueryDataTable("tblZPC", "sp_GetScvChgArea", CommandType.StoredProcedure, false, param);

                if (_d0 != null)
                {
                    if (_d0.Rows.Count > 0) _found = true;
                }

                if (_found == false)
                {
                    _msg = "Area has not define any charges!";
                    return -1;
                }

                if (_d0.Rows.Count > 0)
                {
                    for (int i = 0; i < _d0.Rows.Count; i++)
                    {
                        _chg = Convert.ToDecimal(_d0.Rows[i]["SCA_SCG_RATE"].ToString());
                        break;
                    }
                }
            }

            _msg = string.Empty;
            return _chg;
        }

        //Tharaka 2014-12-29
        public List<Service_Enquiry_Tech_Cmnt> GET_TECH_CMNT_ENQRY(string jobNo, Int32 lineNo)
        {
            List<Service_Enquiry_Tech_Cmnt> result = new List<Service_Enquiry_Tech_Cmnt>();
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("P_JOBNUM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobNo;
            (_para[1] = new OracleParameter("P_JOBLINE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lineNo;
            _para[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobsDefects", "SP_GET_TECH_CMNT_ENQRY", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Enquiry_Tech_Cmnt>(_dtResults, Service_Enquiry_Tech_Cmnt.Converter);
            }
            return result;
        }

        //Tharaka 2014-12-29
        public List<Service_Enquiry_StandByItems> GET_STNDBY_ITMS_ENQRY(String jobNo, Int32 lineNo, String com, String loc)
        {
            List<Service_Enquiry_StandByItems> result = new List<Service_Enquiry_StandByItems>();
            OracleParameter[] _para = new OracleParameter[5];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (_para[1] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = loc;
            (_para[2] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobNo;
            (_para[3] = new OracleParameter("P_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lineNo;
            _para[4] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_STNDBY_ITMS_ENQRY", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Enquiry_StandByItems>(_dtResults, Service_Enquiry_StandByItems.Converter);
            }
            return result;
        }

        //Tharaka 2014-12-30
        public List<Service_job_Det> GET_SCV_JOB_DET_BY_SERIAL(String Serial, String item, String com)
        {
            List<Service_job_Det> result = new List<Service_job_Det>();
            OracleParameter[] _para = new OracleParameter[4];
            (_para[0] = new OracleParameter("P_SERIAL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Serial;
            (_para[1] = new OracleParameter("P_ITM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item;
            (_para[2] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            _para[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_JOB_DET_BY_SERIAL", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_job_Det>(_dtResults, Service_job_Det.Converter);
            }
            return result;
        }

        //Tharaka 2014-12-30
        public List<Service_Enquiry_InventryItems> GET_INVITMS_BYJOBLINE_ENQRY(String jobNo, Int32 lineNo, String Com)
        {
            List<Service_Enquiry_InventryItems> result = new List<Service_Enquiry_InventryItems>();
            OracleParameter[] _para = new OracleParameter[4];
            (_para[0] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobNo;
            (_para[1] = new OracleParameter("P_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lineNo;
            (_para[2] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            _para[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_INVITMS_BYJOBLINE_ENQRY", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Enquiry_InventryItems>(_dtResults, Service_Enquiry_InventryItems.Converter);
            }
            return result;
        }

        //Tharaka 2014-12-30
        public List<Tuple<string, string, string>> GET_SCV_CONFIM_INVO_RMRK(String jobNo, Int32 lineNo, String COm)
        {
            List<Tuple<string, string, string>> stringList = new List<Tuple<string, string, string>>();

            String result = string.Empty;
            OracleParameter[] _para = new OracleParameter[4];
            (_para[0] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobNo;
            (_para[1] = new OracleParameter("P_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lineNo;
            (_para[2] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = COm;
            _para[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

          //  DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_CONFIM_INVO_RMRK", CommandType.StoredProcedure, false, _para);
            DataTable _dtResults = QueryDataTable("tbl", "sP_JOB_REMARKS", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = _dtResults.Rows[0][0].ToString();
                stringList.Add(new Tuple<string, string, string>(_dtResults.Rows[0]["REMARKS"].ToString(), _dtResults.Rows[0]["SOURCE"].ToString(), _dtResults.Rows[0]["CREATEUSER"].ToString()));
            }
            return stringList;
        }

        //Tharaka 2015-01-02
        public List<Service_Enquiry_Estimate_Hdr> GET_SCV_EST_BY_JOB(String Com, String Loc, String Pc, String Job)
        {
            List<Service_Enquiry_Estimate_Hdr> result = new List<Service_Enquiry_Estimate_Hdr>();
            OracleParameter[] _para = new OracleParameter[5];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (_para[1] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Loc;
            (_para[2] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Pc;
            (_para[3] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Job;
            _para[4] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_EST_BY_JOB", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Enquiry_Estimate_Hdr>(_dtResults, Service_Enquiry_Estimate_Hdr.Converter);
            }
            return result;
        }

        //Tharaka 2015-01-02
        public List<Service_Enquiry_Estimate_Items> GET_SCV_EST_ITM_ENQRY(Int32 Seq)
        {
            List<Service_Enquiry_Estimate_Items> result = new List<Service_Enquiry_Estimate_Items>();
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("P_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Seq;
            _para[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_EST_ITM_ENQRY", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Enquiry_Estimate_Items>(_dtResults, Service_Enquiry_Estimate_Items.Converter);
            }
            return result;
        }

        //Tharaka 2015-01-02
        public List<Service_Enquiry_Estimate_TAX> GET_SCV_EST_TAX_ENQRY(Int32 Seq, Int32 ItemLine)
        {
            List<Service_Enquiry_Estimate_TAX> result = new List<Service_Enquiry_Estimate_TAX>();
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("P_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Seq;
            (_para[1] = new OracleParameter("P_ITMLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = ItemLine;
            _para[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_EST_TAX_ENQRY", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Enquiry_Estimate_TAX>(_dtResults, Service_Enquiry_Estimate_TAX.Converter);
            }
            return result;
        }

        //Tharaka 2015-01-02
        public List<Service_Enquiry_Invoice_Items> GET_SCV_INVO_DET_ENQRY(String Job, String Com)
        {
            List<Service_Enquiry_Invoice_Items> result = new List<Service_Enquiry_Invoice_Items>();
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Job;
            (_para[1] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            _para[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_INVO_DET_ENQRY", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Enquiry_Invoice_Items>(_dtResults, Service_Enquiry_Invoice_Items.Converter);
            }
            return result;
        }

        //Tharaka 2015-01-02
        public List<Service_Enquiry_Invoice_TAX> GET_SCV_INVO_ITM_TAX_ENQRY(String InvoiceNum, Int32 lineNum)
        {
            List<Service_Enquiry_Invoice_TAX> result = new List<Service_Enquiry_Invoice_TAX>();
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("P_INVOICE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = InvoiceNum;
            (_para[1] = new OracleParameter("P_LINE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lineNum;
            _para[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_INVO_ITM_TAX_ENQRY", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Enquiry_Invoice_TAX>(_dtResults, Service_Enquiry_Invoice_TAX.Converter);
            }
            return result;
        }

        //Darshana 2015-01-02
        public List<Service_Purchase_Approval> GetServicePOApp(string jobNo, Int32 lineNo)
        {
            List<Service_Purchase_Approval> result = new List<Service_Purchase_Approval>();
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("p_job", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobNo;
            (_para[1] = new OracleParameter("p_jobItmLine", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lineNo;
            _para[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobsDefects", "sp_getappserpo", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Purchase_Approval>(_dtResults, Service_Purchase_Approval.Converter);
            }
            return result;
        }

        //Tharaka 2015-01-03
        public List<Service_Enquiry_PartTrasferd> GET_SCV_PART_TRSFER_ENQRY(String jobNo, Int32 lineNo, String Com)
        {
            List<Service_Enquiry_PartTrasferd> result = new List<Service_Enquiry_PartTrasferd>();
            OracleParameter[] _para = new OracleParameter[4];
            (_para[0] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobNo;
            (_para[1] = new OracleParameter("P_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lineNo;
            (_para[2] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            _para[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_PART_TRSFER_ENQRY", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Enquiry_PartTrasferd>(_dtResults, Service_Enquiry_PartTrasferd.ConverterNew);
            }
            return result;
        }

        //Tharaka 2015-01-03
        public List<Service_Enquiry_SupplierWrntyClaim> SCV_PART_TRSFER_ENQRY(String jobNo, Int32 lineNo, String Com)
        {
            List<Service_Enquiry_SupplierWrntyClaim> result = new List<Service_Enquiry_SupplierWrntyClaim>();
            OracleParameter[] _para = new OracleParameter[4];
            (_para[0] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobNo;
            (_para[1] = new OracleParameter("P_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lineNo;
            (_para[2] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            _para[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_PART_TRSFER_ENQRY", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Enquiry_SupplierWrntyClaim>(_dtResults, Service_Enquiry_SupplierWrntyClaim.Converter);
            }
            return result;
        }

        //Tharaka 2015-01-03
        public List<Service_Enquiry_SupplierWrntyDetails> GET_SCV_SUPP_WRNTREQHDR_ENQ(String jobNo, Int32 lineNo, Int32 Seq, String Type)
        {
            List<Service_Enquiry_SupplierWrntyDetails> result = new List<Service_Enquiry_SupplierWrntyDetails>();
            OracleParameter[] _para = new OracleParameter[5];
            (_para[0] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobNo;
            (_para[1] = new OracleParameter("P_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lineNo;
            (_para[2] = new OracleParameter("P_OLDSEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Seq;
            (_para[3] = new OracleParameter("P_TYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Type;
            _para[4] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_SUPP_WRNTREQHDR_ENQ", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Enquiry_SupplierWrntyDetails>(_dtResults, Service_Enquiry_SupplierWrntyDetails.Converter);
            }
            return result;
        }

        //Darshana 2015-01-06
        public Int32 save_Conf_Hdr(Service_confirm_Header _confHdr)
        {
            OracleParameter[] param = new OracleParameter[26];
            Int32 effects = 0;
            (param[0] = new OracleParameter("p_jch_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _confHdr.Jch_seq;
            (param[1] = new OracleParameter("p_jch_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confHdr.Jch_no;
            (param[2] = new OracleParameter("p_jch_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confHdr.Jch_com;
            (param[3] = new OracleParameter("p_jch_chnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confHdr.Jch_chnl;
            (param[4] = new OracleParameter("p_jch_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confHdr.Jch_loc;
            (param[5] = new OracleParameter("p_jch_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confHdr.Jch_pc;
            (param[6] = new OracleParameter("p_jch_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _confHdr.Jch_dt;
            (param[7] = new OracleParameter("p_jch_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confHdr.Jch_jobno;
            (param[8] = new OracleParameter("p_jch_invtp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confHdr.Jch_invtp;
            (param[9] = new OracleParameter("p_jch_reqno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confHdr.Jch_reqno;
            (param[10] = new OracleParameter("p_jch_manualref", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confHdr.Jch_manualref;
            (param[11] = new OracleParameter("p_jch_rmk", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confHdr.Jch_rmk;
            (param[12] = new OracleParameter("p_jch_isdoneinvoiced", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _confHdr.Jch_isdoneinvoiced;
            (param[13] = new OracleParameter("p_jch_cust_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confHdr.Jch_cust_cd;
            (param[14] = new OracleParameter("p_jch_cust_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confHdr.Jch_cust_name;
            (param[15] = new OracleParameter("p_jch_add1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confHdr.Jch_add1;
            (param[16] = new OracleParameter("p_jch_add2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confHdr.Jch_add2;
            (param[17] = new OracleParameter("p_jch_curr_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confHdr.Jch_curr_cd;
            (param[18] = new OracleParameter("p_jch_ex_rt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _confHdr.Jch_ex_rt;
            (param[19] = new OracleParameter("p_jch_jobclosetp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confHdr.Jch_jobclosetp;
            (param[20] = new OracleParameter("p_jch_jobclosedesc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confHdr.Jch_jobclosedesc;
            (param[21] = new OracleParameter("p_jch_jobclosermk", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confHdr.Jch_jobclosermk;
            (param[22] = new OracleParameter("p_jch_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confHdr.Jch_stus;
            (param[23] = new OracleParameter("p_jch_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confHdr.Jch_cre_by;
            (param[24] = new OracleParameter("p_jch_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confHdr.Jch_mod_by;

            param[25] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_save_confhdr", CommandType.StoredProcedure, param);
            return effects;
        }

        //Darshana 07-01-2015
        public Int32 save_Conf_Det(Service_Confirm_detail _confDet)
        {
            OracleParameter[] param = new OracleParameter[51];
            Int32 effects = 0;
            (param[0] = new OracleParameter("p_jcd_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _confDet.Jcd_seq;
            (param[1] = new OracleParameter("p_jcd_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confDet.Jcd_no;
            (param[2] = new OracleParameter("p_jcd_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _confDet.Jcd_line;
            (param[3] = new OracleParameter("p_jcd_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confDet.Jcd_jobno;
            (param[4] = new OracleParameter("p_jcd_joblineno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _confDet.Jcd_joblineno;
            (param[5] = new OracleParameter("p_jcd_itmcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confDet.Jcd_itmcd;
            (param[6] = new OracleParameter("p_jcd_itmstus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confDet.Jcd_itmstus;
            (param[7] = new OracleParameter("p_jcd_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _confDet.Jcd_qty;
            (param[8] = new OracleParameter("p_jcd_balqty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _confDet.Jcd_balqty;
            (param[9] = new OracleParameter("p_jcd_pb", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confDet.Jcd_pb;
            (param[10] = new OracleParameter("p_jcd_pblvl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confDet.Jcd_pblvl;
            (param[11] = new OracleParameter("p_jcd_unitprice", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _confDet.Jcd_unitprice;
            (param[12] = new OracleParameter("p_jcd_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _confDet.Jcd_amt;
            (param[13] = new OracleParameter("p_jcd_tax_rt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _confDet.Jcd_tax_rt;
            (param[14] = new OracleParameter("p_jcd_tax", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _confDet.Jcd_tax;
            (param[15] = new OracleParameter("p_jcd_dis_rt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _confDet.Jcd_dis_rt;
            (param[16] = new OracleParameter("p_jcd_dis", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _confDet.Jcd_dis;
            (param[17] = new OracleParameter("p_jcd_net_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _confDet.Jcd_net_amt;
            (param[18] = new OracleParameter("p_jcd_itmtp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confDet.Jcd_itmtp;
            (param[19] = new OracleParameter("p_jcd_foc", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _confDet.Jcd_foc;
            (param[20] = new OracleParameter("p_jcd_costelement", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confDet.Jcd_costelement;
            (param[21] = new OracleParameter("p_jcd_docno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confDet.Jcd_docno;
            (param[22] = new OracleParameter("p_jcd_rmk", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confDet.Jcd_rmk;
            (param[23] = new OracleParameter("p_jcd_costsheetlineno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _confDet.Jcd_costsheetlineno;
            (param[24] = new OracleParameter("p_jcd_jobitmcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confDet.Jcd_jobitmcd;
            (param[25] = new OracleParameter("p_jcd_jobitmser", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confDet.Jcd_jobitmser;
            (param[26] = new OracleParameter("p_jcd_jobwarrno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confDet.Jcd_jobwarrno;
            (param[27] = new OracleParameter("p_jcd_pbprice", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _confDet.Jcd_pbprice;
            (param[28] = new OracleParameter("p_jcd_pbseqno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _confDet.Jcd_pbseqno;
            (param[29] = new OracleParameter("p_jcd_pbitmseqno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _confDet.Jcd_pbitmseqno;
            (param[30] = new OracleParameter("p_jcd_itmdesc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confDet.Jcd_itmdesc;
            (param[31] = new OracleParameter("p_jcd_itmmodel", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confDet.Jcd_itmmodel;
            (param[32] = new OracleParameter("p_jcd_itmbrand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confDet.Jcd_itmbrand;
            (param[33] = new OracleParameter("p_jcd_itmuom", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confDet.Jcd_itmuom;
            (param[34] = new OracleParameter("p_jcd_mov_doc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confDet.Jcd_mov_doc;
            (param[35] = new OracleParameter("p_jcd_itmline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _confDet.Jcd_itmline;
            (param[36] = new OracleParameter("p_jcd_batchline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _confDet.Jcd_batchline;
            (param[37] = new OracleParameter("p_jcd_serline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _confDet.Jcd_serline;
            (param[38] = new OracleParameter("p_jcd_ser_id", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _confDet.Jcd_ser_id;
            (param[39] = new OracleParameter("p_jcd_gatepass_raised", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _confDet.Jcd_gatepass_raised;
            (param[40] = new OracleParameter("p_jcd_invtype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confDet.Jcd_invtype;
            (param[41] = new OracleParameter("p_jcd_iswarr", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _confDet.Jcd_iswarr;
            (param[42] = new OracleParameter("p_jcd_movedoctp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confDet.Jcd_movedoctp;
            (param[43] = new OracleParameter("p_jcd_cuscd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confDet.Jcd_cuscd;
            (param[44] = new OracleParameter("p_jcd_cusname", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confDet.Jcd_cusname;
            (param[45] = new OracleParameter("p_jcd_cusadd1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confDet.Jcd_cusadd1;
            (param[46] = new OracleParameter("p_jcd_cusadd2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confDet.Jcd_cusadd2;
            (param[47] = new OracleParameter("p_jcd_isadditm", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _confDet.Jcd_isadditm;
            (param[48] = new OracleParameter("P_jcd_mainitmcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confDet.Jcd_mainitmcd;
            (param[49] = new OracleParameter("p_jcd_mainitmdesc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confDet.Jcd_mainitmdesc;

            param[50] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_saveconfdet", CommandType.StoredProcedure, param);
            return effects;
        }

        //Darshana 07-01-2015
        public Int32 GetConfSeq()
        {
            OracleParameter[] param = new OracleParameter[1];
            Int32 effects = 0;

            param[0] = new OracleParameter("O_SEQ", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_getscvconfseq", CommandType.StoredProcedure, param);
            return effects;
        }

        // Nadeeka 24-06-2015
        public Int32 GetSuppWaraSeq()
        {
            OracleParameter[] param = new OracleParameter[1];
            Int32 effects = 0;

            param[0] = new OracleParameter("O_SEQ", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_getsupwaraDefSeq", CommandType.StoredProcedure, param);
            return effects;
        }

        //Darshana 07-01-2015
        public Int32 save_JobCostSheet(Service_Cost_sheet _jobCost)
        {
            OracleParameter[] param = new OracleParameter[42];
            Int32 effects = 0;

            (param[0] = new OracleParameter("p_cs_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobCost.CS_JOBNO;
            (param[1] = new OracleParameter("p_cs_joblineno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobCost.CS_JOBLINENO;
            (param[2] = new OracleParameter("p_cs_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobCost.CS_LINE;
            (param[3] = new OracleParameter("p_cs_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobCost.CS_COM;
            (param[4] = new OracleParameter("p_cs_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobCost.CS_LOC;
            (param[5] = new OracleParameter("p_cs_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobCost.CS_PC;
            (param[6] = new OracleParameter("p_cs_jobitmcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobCost.CS_JOBITMCD;
            (param[7] = new OracleParameter("p_cs_jobitmser", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobCost.CS_JOBITMSER;
            (param[8] = new OracleParameter("p_cs_jobitmwarr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobCost.CS_JOBITMWARR;
            (param[9] = new OracleParameter("p_cs_itmcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobCost.CS_ITMCD;
            (param[10] = new OracleParameter("p_cs_itmstus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobCost.CS_ITMSTUS;
            (param[11] = new OracleParameter("p_cs_itmser", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobCost.CS_ITMSER;
            (param[12] = new OracleParameter("p_cs_uom", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobCost.CS_UOM;
            (param[13] = new OracleParameter("p_cs_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _jobCost.CS_QTY;
            (param[14] = new OracleParameter("p_cs_direct", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobCost.CS_DIRECT;
            (param[15] = new OracleParameter("p_cs_unitcost", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _jobCost.CS_UNITCOST;
            (param[16] = new OracleParameter("p_cs_totunitcost", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _jobCost.CS_TOTUNITCOST;
            (param[17] = new OracleParameter("p_cs_doctp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobCost.CS_DOCTP;
            (param[18] = new OracleParameter("p_cs_docno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobCost.CS_DOCNO;
            (param[19] = new OracleParameter("p_cs_serid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobCost.CS_SERID;
            (param[20] = new OracleParameter("p_cs_docdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobCost.CS_DOCDT;
            (param[21] = new OracleParameter("p_cs_outdocno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobCost.CS_OUTDOCNO;
            (param[22] = new OracleParameter("p_cs_outdocline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobCost.CS_OUTDOCLINE;
            (param[23] = new OracleParameter("p_cs_movelineno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobCost.CS_MOVELINENO;
            (param[24] = new OracleParameter("p_cs_itmtp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobCost.CS_ITMTP;
            (param[25] = new OracleParameter("p_cs_itmdesc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobCost.CS_ITMDESC;
            (param[26] = new OracleParameter("p_cs_cuscd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobCost.CS_CUSCD;
            (param[27] = new OracleParameter("p_cs_cusname", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobCost.CS_CUSNAME;
            (param[28] = new OracleParameter("p_cs_docqty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _jobCost.CS_DOCQTY;
            (param[29] = new OracleParameter("p_cs_balqty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _jobCost.CS_BALQTY;
            (param[30] = new OracleParameter("p_cs_isfoc", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobCost.CS_ISFOC;
            (param[31] = new OracleParameter("p_cs_jobconfno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobCost.CS_JOBCONFNO;
            (param[32] = new OracleParameter("p_cs_isinv", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobCost.CS_ISINV;
            (param[33] = new OracleParameter("p_cs_invno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobCost.CS_INVNO;
            (param[34] = new OracleParameter("p_cs_isrevitm", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobCost.CS_ISREVITM;
            (param[35] = new OracleParameter("p_cs_jobclose", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobCost.CS_JOBCLOSE;
            (param[36] = new OracleParameter("p_cs_consumablecd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobCost.CS_CONSUMABLECD;
            (param[37] = new OracleParameter("p_cs_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobCost.CS_ACT;
            (param[38] = new OracleParameter("p_cs_oldline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobCost.CS_OLDLINE;
            (param[39] = new OracleParameter("p_cs_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobCost.CS_CRE_BY;
            (param[40] = new OracleParameter("p_cs_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobCost.CS_MOD_BY;

            param[41] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_savejobcostsheet", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2015-01-10
        public List<Service_Enquiry_ConfDetails> GET_SCV_CONFDET_ENQRY(String jobNo, Int32 lineNo, String Com)
        {
            List<Service_Enquiry_ConfDetails> result = new List<Service_Enquiry_ConfDetails>();
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobNo;
            (_para[1] = new OracleParameter("P_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lineNo;
            _para[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_CONFDET_ENQRY", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Enquiry_ConfDetails>(_dtResults, Service_Enquiry_ConfDetails.Converter);
            }
            return result;
        }

        //Tharaka 2015-01-10
        public List<Service_Enquiry_CostSheet> GET_SCV_COST_SHEET_ENQRY(String jobNo, Int32 lineNo, String Com, String Loc)
        {
            List<Service_Enquiry_CostSheet> result = new List<Service_Enquiry_CostSheet>();
            OracleParameter[] _para = new OracleParameter[5];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (_para[1] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Loc;
            (_para[2] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobNo;
            (_para[3] = new OracleParameter("P_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lineNo;
            _para[4] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_COST_SHEET_ENQRY", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Enquiry_CostSheet>(_dtResults, Service_Enquiry_CostSheet.Converter);
            }
            return result;
        }

        //Tharaka 2015-01-12
        public List<Service_Enquiry_WarrtyReplacement> GET_SCV_WARTYREPLMENT_ENQRY(String jobNo, Int32 lineNo, String Com)
        {
            List<Service_Enquiry_WarrtyReplacement> result = new List<Service_Enquiry_WarrtyReplacement>();
            OracleParameter[] _para = new OracleParameter[4];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (_para[1] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobNo;
            (_para[2] = new OracleParameter("P_JOBLINE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lineNo;
            _para[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_WARTYREPLMENT_ENQRY", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Enquiry_WarrtyReplacement>(_dtResults, Service_Enquiry_WarrtyReplacement.Converter);
            }
            return result;
        }

        //Tharaka 2015-01-12
        public List<Service_Enquiry_CustCollectionData> GET_CUST_COLLDATE_ENQRY(String jobNo, Int32 lineNo, String Com)
        {
            List<Service_Enquiry_CustCollectionData> result = new List<Service_Enquiry_CustCollectionData>();
            OracleParameter[] _para = new OracleParameter[4];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (_para[1] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobNo;
            (_para[2] = new OracleParameter("P_JOBLINE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lineNo;
            _para[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_CUST_COLLDATE_ENQRY", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Enquiry_CustCollectionData>(_dtResults, Service_Enquiry_CustCollectionData.Converter);
            }
            return result;
        }

        //Tharaka 2015-01-12
        public List<_Service_Enquiry_StageLog> GET_STAGELOG_ENQRY(String jobNo, Int32 lineNo)
        {
            List<_Service_Enquiry_StageLog> result = new List<_Service_Enquiry_StageLog>();
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobNo;
            (_para[1] = new OracleParameter("P_JOBLINE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lineNo;
            _para[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_STAGELOG_ENQRY", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<_Service_Enquiry_StageLog>(_dtResults, _Service_Enquiry_StageLog.Converter);
            }
            return result;
        }

        //Tharaka 2015-01-13
        public List<Service_Enquiry_Inssuarance> GET_INSSURANCE_ENQRY(String Com, String Ser, String Item, String Invoice)
        {
            List<Service_Enquiry_Inssuarance> result = new List<Service_Enquiry_Inssuarance>();
            OracleParameter[] _para = new OracleParameter[5];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (_para[1] = new OracleParameter("P_SER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Ser;
            (_para[2] = new OracleParameter("P_ITM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item;
            (_para[3] = new OracleParameter("P_INVOIC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Invoice;
            _para[4] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_INSSURANCE_ENQRY", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Enquiry_Inssuarance>(_dtResults, Service_Enquiry_Inssuarance.Converter);
            }
            return result;
        }

        //Tharaka 2015-01-13
        public Decimal GET_SCV_CONFIM_INVO_AMOUNT(String jobNo, Int32 lineNo, String Com)
        {
            Decimal Amount = 0;
            List<Service_Enquiry_CustCollectionData> result = new List<Service_Enquiry_CustCollectionData>();
            OracleParameter[] _para = new OracleParameter[4];
            (_para[0] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobNo;
            (_para[1] = new OracleParameter("P_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lineNo;
            (_para[2] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            _para[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_CONFIM_INVO_AMOUNT", CommandType.StoredProcedure, false, _para);
            Amount = Convert.ToDecimal(_dtResults.Rows[0][0].ToString());
            return Amount;
        }

        //Tharaka 2015-01-14
        public List<Service_job_Det> SCV_JOB_GET_SER_OSR_REG(String Com, String Loc, String Serial, String Serial2, String RegNum)
        {
            List<Service_job_Det> result = new List<Service_job_Det>();
            OracleParameter[] _para = new OracleParameter[6];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (_para[1] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Loc;
            (_para[2] = new OracleParameter("P_SER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Serial;
            (_para[3] = new OracleParameter("P_SER2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Serial2;
            (_para[4] = new OracleParameter("P_REG", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = RegNum;
            _para[5] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_JOB_GET_SER_OSR_REG", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_job_Det>(_dtResults, Service_job_Det.Converter);
            }
            return result;
        }

        //Tharaka 2015-01-16
        public Decimal GET_SCV_ITM_COST(String Com, String Job, Int32 Line, String Item, String Status, Decimal Qty)
        {
            Decimal Values = 0;
            OracleParameter[] _para = new OracleParameter[7];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (_para[1] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Job;
            (_para[2] = new OracleParameter("P_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Line;
            (_para[3] = new OracleParameter("P_ITEM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item;
            (_para[4] = new OracleParameter("P_STATUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Status;
            (_para[5] = new OracleParameter("P_QTY", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = Qty;
            _para[6] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_ITM_COST", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                Values = Convert.ToDecimal(_dtResults.Rows[0][0].ToString());
            }

            return Values;
        }
        public List<Service_Cost_sheet> GET_SCV_ITM_COST_SHEET(String Com, String Job, Int32 Line)
        {// Nadeeka
            List<Service_Cost_sheet> result = new List<Service_Cost_sheet>();
            OracleParameter[] _para = new OracleParameter[4];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (_para[1] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Job;
            (_para[2] = new OracleParameter("P_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Line;
            _para[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_ITM_COST_LINE", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Cost_sheet>(_dtResults, Service_Cost_sheet.Converter);
            }

            return result;
        }


        //Tharaka 2015-01-21
        public List<Service_stockReturn> Get_ServiceWIP_ViewStockItems(string Com, string job, Int32 jobline, string Item, string LOC)
        {
            List<Service_stockReturn> result = new List<Service_stockReturn>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[1] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = LOC;
            (param[2] = new OracleParameter("P_JOb", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (param[3] = new OracleParameter("P_Item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item;
            (param[4] = new OracleParameter("P_JobLine", OracleDbType.Int32, null, ParameterDirection.Input)).Value = jobline;
            param[5] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_VIEW_STOCKITMS", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_stockReturn>(_dtResults, Service_stockReturn.Converter);
            }
            return result;
        }

        //Tharaka 2015-01-22
        public List<Service_TempIssue> GET_TEMPISSUE_By_LOC(string Com, string job, Int32 jobline, string Item, string LOC, string Type)
        {
            List<Service_TempIssue> result = new List<Service_TempIssue>();
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[1] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = LOC;
            (param[2] = new OracleParameter("P_ITEMCODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item;
            (param[3] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (param[4] = new OracleParameter("P_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = jobline;
            (param[5] = new OracleParameter("P_TYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Type;
            param[6] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_TEMPISSUE_By_LOC", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_TempIssue>(_dtResults, Service_TempIssue.Converter);

                for (int i = 0; i < _dtResults.Rows.Count; i++)
                {
                    result[i].Desc = _dtResults.Rows[i]["DESC"].ToString();
                }
            }
            return result;
        }

        //kapila
        public DataTable GET_TEMPISSUE_By_SER(string Com, string loc,  string Item, string ser, string Type)
        {
            List<Service_TempIssue> result = new List<Service_TempIssue>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[1] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = loc;
            (param[2] = new OracleParameter("P_ITEMCODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item;
            (param[3] = new OracleParameter("P_ser", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = ser;
            (param[4] = new OracleParameter("P_TYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Type;
            param[5] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_TEMPISSUE_By_SER", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        //Chamal 22-Jan-2015
        public Int32 UpdateRequestItemViaJob(Service_job_Det _jobDet)
        {
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_com;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_loc;
            (param[2] = new OracleParameter("p_reqno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_reqno;
            (param[3] = new OracleParameter("p_reqlineno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_reqline;
            (param[4] = new OracleParameter("p_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_jobno;
            (param[5] = new OracleParameter("p_joblineno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_jobline;
            param[6] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return UpdateRecords("sp_update_reqdet_via_job", CommandType.StoredProcedure, param);
        }

        //Chamal 22-Jan-2015
        public Int32 sp_update_reqhdr_via_job(Service_JOB_HDR _jobhdr)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobhdr.SJB_COM;
            (param[1] = new OracleParameter("p_reqno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobhdr.SJB_REQNO;
            (param[2] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobhdr.SJB_CRE_BY;
            param[3] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return UpdateRecords("sp_update_reqhdr_via_job", CommandType.StoredProcedure, param);
        }

        //Tharaka 2015-01-22
        public String GET_SCV_JOB_CATE(string Com, string job)
        {
            String resu = string.Empty;
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (param[1] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            param[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_JOB_CATE", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                resu = _dtResults.Rows[0][0].ToString();
            }
            return resu;
        }

        //Tharaka 2015-01-23
        public List<Service_Gate_Pass_HDR> SCV_CHEK_GP_FOR_JOBLINE(String jobNo, Int32 lineNo, String Com)
        {
            List<Service_Gate_Pass_HDR> result = new List<Service_Gate_Pass_HDR>();
            OracleParameter[] _para = new OracleParameter[4];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (_para[1] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobNo;
            (_para[2] = new OracleParameter("P_JOBLINE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lineNo;
            _para[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_SCV_CHEK_GP_FOR_JOBLINE", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Gate_Pass_HDR>(_dtResults, Service_Gate_Pass_HDR.Converter);
            }
            return result;
        }

        //Tharaka 2015-01-23
        public Service_Category GET_SCV_CATE_BY_JOB(String jobNo, String Com)
        {
            Service_Category result = new Service_Category();
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (_para[1] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobNo;
            _para[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_CATE_BY_JOB", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Category>(_dtResults, Service_Category.Converter)[0];
            }
            return result;
        }

        //THARAKA 2015-01-26
        public Int32 Save_TechnicianAllocatoinHEaderLog(Service_Tech_Aloc_Hdr oHeader, String user)
        {
            OracleParameter[] param = new OracleParameter[23];
            Int32 effects = 0; ;
            (param[0] = new OracleParameter("P_sth_alocno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.STH_ALOCNO;
            (param[1] = new OracleParameter("P_sth_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.STH_COM;
            (param[2] = new OracleParameter("P_sth_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.STH_LOC;
            (param[3] = new OracleParameter("P_sth_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.STH_TP;
            (param[4] = new OracleParameter("P_sth_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.STH_JOBNO;
            (param[5] = new OracleParameter("P_sth_jobline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oHeader.STH_JOBLINE;
            (param[6] = new OracleParameter("P_sth_emp_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.STH_EMP_CD;
            (param[7] = new OracleParameter("P_sth_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.STH_STUS;
            (param[8] = new OracleParameter("P_sth_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.STH_CRE_BY;
            (param[9] = new OracleParameter("P_sth_cre_when", OracleDbType.Date, null, ParameterDirection.Input)).Value = oHeader.STH_CRE_WHEN;
            (param[10] = new OracleParameter("P_sth_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.STH_MOD_BY;
            (param[11] = new OracleParameter("P_sth_mod_when", OracleDbType.Date, null, ParameterDirection.Input)).Value = oHeader.STH_MOD_WHEN;
            (param[12] = new OracleParameter("P_sth_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.STH_SESSION_ID;
            (param[13] = new OracleParameter("P_sth_town", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.STH_TOWN;
            (param[14] = new OracleParameter("P_sth_from_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = oHeader.STH_FROM_DT;
            (param[15] = new OracleParameter("P_sth_to_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = oHeader.STH_TO_DT;
            (param[16] = new OracleParameter("P_sth_reqno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.STH_REQNO;
            (param[17] = new OracleParameter("P_sth_reqline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oHeader.STH_REQLINE;
            (param[18] = new OracleParameter("P_STH_TERMINAL", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oHeader.STH_TERMINAL;
            (param[19] = new OracleParameter("P_STH_CURR_STUS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oHeader.STH_CURR_STUS;
            (param[20] = new OracleParameter("P_STH_EDIT_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = user;
            (param[21] = new OracleParameter("P_STH_EDIT_WHEN", OracleDbType.Date, null, ParameterDirection.Input)).Value = DateTime.Now;
            param[22] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int32)UpdateRecords("SP_SAVE_ALL_HDR_LOG", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2014-09-30
        public DataTable GetJObsFOrWIP(string com, DateTime From, DateTime To, string jobno, string Stage, Int32 isCusexpectDate, string customer, string PC, String userID)
        {
            OracleParameter[] _para = new OracleParameter[10];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (_para[1] = new OracleParameter("P_From", OracleDbType.Date, null, ParameterDirection.Input)).Value = From;
            (_para[2] = new OracleParameter("P_TO", OracleDbType.Date, null, ParameterDirection.Input)).Value = To;
            (_para[3] = new OracleParameter("p_jonno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobno;
            (_para[4] = new OracleParameter("P_Stage", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Stage;
            (_para[5] = new OracleParameter("p_iscusexpectdate", OracleDbType.Int32, null, ParameterDirection.Input)).Value = isCusexpectDate;
            (_para[6] = new OracleParameter("P_Customer", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = customer;
            (_para[7] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PC;
            (_para[8] = new OracleParameter("P_USER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userID;
            _para[9] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblJobs", "SP_GET_SCV_WIP_JOB", CommandType.StoredProcedure, false, _para);
            return _dtResults;
        }

        //Tharaka 2015-01-27
        public List<MST_BUSPRIT_LVL> GetCustomerPriorityLevel(String custCode, String Com)
        {
            List<MST_BUSPRIT_LVL> result = new List<MST_BUSPRIT_LVL>();
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (_para[1] = new OracleParameter("P_CUSTCODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = custCode;
            _para[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_CUST_PRIT_LVL", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<MST_BUSPRIT_LVL>(_dtResults, MST_BUSPRIT_LVL.Converter);
            }
            return result;
        }

        //Tharaka 2015-01-27
        public List<MST_BUSPRIT_TASK> GetCustomerPriorityTask(MST_BUSPRIT_TASK OInput)
        {
            List<MST_BUSPRIT_TASK> result = new List<MST_BUSPRIT_TASK>();
            OracleParameter[] _para = new OracleParameter[6];
            (_para[0] = new OracleParameter("P_CUSTCODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = OInput.Mbt_cd;
            (_para[1] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = OInput.Mbt_com;
            (_para[2] = new OracleParameter("P_PARTTYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = OInput.Mbt_pty_tp;
            (_para[3] = new OracleParameter("P_PARTYCODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = OInput.Mbt_pty_cd;
            (_para[4] = new OracleParameter("P_PRIORITYCD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = OInput.Mbt_prit_cd;
            _para[5] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_CUST_PRTY_TASK", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<MST_BUSPRIT_TASK>(_dtResults, MST_BUSPRIT_TASK.Converter);
            }
            return result;
        }

        //Tharaka 2015-01-27
        public List<SCV_ALRT_LVL> GetAlertLevel(Int32 Seq)
        {
            List<SCV_ALRT_LVL> result = new List<SCV_ALRT_LVL>();
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("P_SEQ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Seq;
            _para[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_ALERT_LVL", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<SCV_ALRT_LVL>(_dtResults, SCV_ALRT_LVL.Converter);
            }
            return result;
        }

        //Tharaka 2015-01-27
        public List<SCV_ALRT_EMP> GetAlertEmployees(Int32 Seq)
        {
            List<SCV_ALRT_EMP> result = new List<SCV_ALRT_EMP>();
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("P_SEQ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Seq;
            _para[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_ALERT_EMPS", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<SCV_ALRT_EMP>(_dtResults, SCV_ALRT_EMP.Converter);
            }
            return result;
        }

        //Tharaka 2015-01-27
        public List<scv_prit_task> GetPriorityTask(scv_prit_task OInput)
        {
            List<scv_prit_task> result = new List<scv_prit_task>();
            OracleParameter[] _para = new OracleParameter[5];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = OInput.Spit_com;
            (_para[1] = new OracleParameter("P_PARTTYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = OInput.Spit_pty_tp;
            (_para[2] = new OracleParameter("P_PARTYCODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = OInput.Spit_pty_cd;
            (_para[3] = new OracleParameter("P_PRIORITY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = OInput.Spit_prit_cd;
            _para[4] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_PRIT_TASK", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<scv_prit_task>(_dtResults, scv_prit_task.Converter);
            }
            return result;
        }

        //THARAKA 2015-01-28
        public Int32 UPDATE_INRSEMST_BY_WARR(String IRSM_COM, String IRSM_WARR_NO, String IRSM_CUST_CD, String IRSM_CUST_PREFIX, String IRSM_CUST_NAME, String IRSM_CUST_ADDR, String IRSM_CUST_DEL_ADDR, String IRSM_CUST_TOWN, String IRSM_CUST_TEL, String IRSM_CUST_MOBILE, String IRSM_SER_ID, String IRSM_ORIG_SUPP)
        {
            OracleParameter[] param = new OracleParameter[13];
            Int32 effects = 0; ;
            (param[0] = new OracleParameter("P_IRSM_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = IRSM_COM;
            (param[1] = new OracleParameter("P_IRSM_WARR_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = IRSM_WARR_NO;
            (param[2] = new OracleParameter("P_IRSM_CUST_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = IRSM_CUST_CD;
            (param[3] = new OracleParameter("P_IRSM_CUST_PREFIX", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = IRSM_CUST_PREFIX;
            (param[4] = new OracleParameter("P_IRSM_CUST_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = IRSM_CUST_NAME;
            (param[5] = new OracleParameter("P_IRSM_CUST_ADDR", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = IRSM_CUST_ADDR;
            (param[6] = new OracleParameter("P_IRSM_CUST_DEL_ADDR", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = IRSM_CUST_DEL_ADDR;
            (param[7] = new OracleParameter("P_IRSM_CUST_TOWN", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = IRSM_CUST_TOWN;
            (param[8] = new OracleParameter("P_IRSM_CUST_TEL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = IRSM_CUST_TEL;
            (param[9] = new OracleParameter("P_IRSM_CUST_MOBILE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = IRSM_CUST_MOBILE;
            (param[10] = new OracleParameter("P_IRSM_SER_ID", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToInt32(IRSM_SER_ID);
            (param[11] = new OracleParameter("P_IRSM_ORIG_SUPP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = IRSM_ORIG_SUPP;
            param[12] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDATE_INRSEMST_BY_WARR", CommandType.StoredProcedure, param);
            return effects;
        }

        //THARAKA 2015-01-28
        public Int32 UPDATE_TEMPWARUPLOAD_BYWARR(String customercode, String customerprefix, String customername, String customeraddressservice, String customerarea, String customerphoneno, String customercell, String warrantyno, String SupplierCode)
        {
            OracleParameter[] param = new OracleParameter[10];
            Int32 effects = 0; ;
            (param[0] = new OracleParameter("P_CUSTOMERCODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = customercode;
            (param[1] = new OracleParameter("P_CUSTOMERPREFIX", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = customerprefix;
            (param[2] = new OracleParameter("P_CUSTOMERNAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = customername;
            (param[3] = new OracleParameter("P_CUSTOMERADDRESSSERVICE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = customeraddressservice;
            (param[4] = new OracleParameter("P_CUSTOMERAREA", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = customerarea;
            (param[5] = new OracleParameter("P_CUSTOMERPHONENO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = customerphoneno;
            (param[6] = new OracleParameter("P_CUSTOMERCELL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = customercell;
            (param[7] = new OracleParameter("P_WARRANTYNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = warrantyno;
            (param[8] = new OracleParameter("P_SUPP_CODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = SupplierCode;
            param[9] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDATE_TEMPWARUPLOAD_BYWARR", CommandType.StoredProcedure, param);
            return effects;
        }

        //Darshana 28-01-2015
        public List<Service_Job_Charges> GetServiceJobCharges(String jobNo, Int32 _jobLine)
        {
            List<Service_Job_Charges> result = new List<Service_Job_Charges>();
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("p_job", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobNo;
            (_para[1] = new OracleParameter("p_jobItmLine", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobLine;
            _para[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "sp_getjobchg", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Job_Charges>(_dtResults, Service_Job_Charges.Converter);
            }
            return result;
        }

        //Tharaka 2015-01-29
        public List<Service_StandBy> GetStandByItems(string Com, string job, Int32 jobline, string Item, string LOC)
        {
            List<Service_StandBy> result = new List<Service_StandBy>();
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[1] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = LOC;
            param[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_STANDBY_ITMS", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_StandBy>(_dtResults, Service_StandBy.Converter);
            }
            return result;
        }

        //Tharaka 2015-01-30
        public List<Service_stockReturn> GetServiceJobStockItems(string Com, string job, Int32 jobline, string Item, string LOC)
        {
            List<Service_stockReturn> result = new List<Service_stockReturn>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[1] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = LOC;
            (param[2] = new OracleParameter("P_JOb", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (param[3] = new OracleParameter("P_Item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item;
            (param[4] = new OracleParameter("P_JobLine", OracleDbType.Int32, null, ParameterDirection.Input)).Value = jobline;
            param[5] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_STOCKRETURN_ITEMS", CommandType.StoredProcedure, false, param);
            List<Service_stockReturn> result2 = new List<Service_stockReturn>();
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_stockReturn>(_dtResults, Service_stockReturn.Converter2);

                if (result.FindAll(x => x.INB_ITM_STUS == "STDBY").Count > 0)
                {
                    result2 = result.FindAll(x => x.INB_ITM_STUS == "STDBY");
                }
            }
            return result2;
        }

        //Tharaka 2015-02-06
        public String GET_OTH_WRR_REMARK(String Item, String Ser)
        {
            String result = string.Empty;
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_ITM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item;
            (param[1] = new OracleParameter("P_SER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Ser;
            param[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_OTH_WRR_REMARK", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = _dtResults.Rows[0][0].ToString();
            }
            return result;
        }

        //Tharaka 2015-02-06
        public List<Service_Request_Defects> getRequestDefects(string Req, Int32 line)
        {
            List<Service_Request_Defects> result = new List<Service_Request_Defects>();
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_REQNUM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Req;
            (param[1] = new OracleParameter("P_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = line;
            param[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_REQ_DEF", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Request_Defects>(_dtResults, Service_Request_Defects.Converter);
            }
            return result;
        }

        //Tharaka 2015-02-06
        public List<Service_Job_Defects> GetRequestJobDefectsJobEnty(string Req, Int32 lineNo)
        {
            List<Service_Job_Defects> result = new List<Service_Job_Defects>();
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("P_REQNUM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Req;
            (_para[1] = new OracleParameter("P_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lineNo;
            _para[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobsDefects", "SP_GET_REQ_DEF_JOBENTY", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Job_Defects>(_dtResults, Service_Job_Defects.ConverterAllReuest);
            }
            return result;
        }

        //Tharaka 2015-02-07
        public DataTable GET_INT_HDR_ITMS_JOBENTY(String DocNum, String InvoiceNum)
        {
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("P_REQNUM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = DocNum;
            (_para[1] = new OracleParameter("P_LINE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = InvoiceNum;
            _para[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobsDefects", "SP_GET_INT_HDR_ITMS_JOBENTY", CommandType.StoredProcedure, false, _para);

            return _dtResults;
        }

        //Tharaka 2015-02-11
        public List<MST_ITM_CAT_COMP> getMasterItemCategoryComponant(string Cate1)
        {
            List<MST_ITM_CAT_COMP> result = new List<MST_ITM_CAT_COMP>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_CAT1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Cate1;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_MST_ITM_CAT_COMP", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<MST_ITM_CAT_COMP>(_dtResults, MST_ITM_CAT_COMP.Converter);
            }
            return result;
        }
        //Darshana 2015-02-12
        public List<MasterServiceEmployee> GetMasterSerEmp(string _com, string _Tp, string _cd, string _cate, string _emp, Int16 _act)
        {
            List<MasterServiceEmployee> _result = new List<MasterServiceEmployee>();
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_Tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Tp;
            (param[2] = new OracleParameter("p_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cd;
            (param[3] = new OracleParameter("p_cate", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cate;
            (param[4] = new OracleParameter("p_emp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _emp;
            (param[5] = new OracleParameter("p_act", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _act;
            param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "sp_getseremp", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                _result = DataTableExtensions.ToGenericList<MasterServiceEmployee>(_dtResults, MasterServiceEmployee.Converter);
            }
            return _result;
        }
        //DArshana 12-02-2015
        public Service_Tech_Aloc_Hdr GetAllocTechJob(String _com, String _pc, string _tp, string _jobNo, Int32 _jobLine, string _empCd, string _stus, Int32 _curStatus)
        {
            Service_Tech_Aloc_Hdr result = new Service_Tech_Aloc_Hdr();
            OracleParameter[] _para = new OracleParameter[9];
            (_para[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (_para[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            (_para[2] = new OracleParameter("p_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _tp;
            (_para[3] = new OracleParameter("p_job", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobNo;
            (_para[4] = new OracleParameter("p_jobLine", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobLine;
            (_para[5] = new OracleParameter("p_empCd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _empCd;
            (_para[6] = new OracleParameter("p_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _stus;
            (_para[7] = new OracleParameter("p_curStus", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _curStatus;
            _para[8] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "sp_getsuptecalloc", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Tech_Aloc_Hdr>(_dtResults, Service_Tech_Aloc_Hdr.Converter)[0];
            }
            return result;
        }
        // Nadeeka 18-05-2015
        public Service_Tech_Aloc_Hdr GetAllocationDet(String _com, string _jobNo)
        {
            Service_Tech_Aloc_Hdr result = new Service_Tech_Aloc_Hdr();
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (_para[1] = new OracleParameter("p_job", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobNo;
            _para[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "sp_getTechAlloc", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Tech_Aloc_Hdr>(_dtResults, Service_Tech_Aloc_Hdr.Converter)[0];
            }
            return result;
        }


        // Nadeeka 18-05-2015
        public Service_Tech_Aloc_Hdr GetAllocationDet_loc(String _com, string _jobNo, string _loc)
        {
            Service_Tech_Aloc_Hdr result = new Service_Tech_Aloc_Hdr();
            OracleParameter[] _para = new OracleParameter[4];
            (_para[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (_para[1] = new OracleParameter("p_job", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobNo;
            (_para[2] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            _para[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "sp_getTechAlloc_loc", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Tech_Aloc_Hdr>(_dtResults, Service_Tech_Aloc_Hdr.Converter)[0];
            }
            return result;
        }

        //Tharaka 2015-02-12
        public int INSERT_INR_SERMSTSUB(InventoryWarrantySubDetail oItem)
        {
            OracleParameter[] param = new OracleParameter[13];
            Int32 effects = 0; ;
            (param[0] = new OracleParameter("P_SER_ID", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Irsms_ser_id;
            (param[1] = new OracleParameter("P_SER_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Irsms_ser_line;
            (param[2] = new OracleParameter("P_WARR_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Irsms_warr_no;
            (param[3] = new OracleParameter("P_ITM_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Irsms_itm_cd;
            (param[4] = new OracleParameter("P_ITM_STUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Irsms_itm_stus;
            (param[5] = new OracleParameter("P_SUB_SER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Irsms_sub_ser;
            (param[6] = new OracleParameter("P_MFC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Irsms_mfc;
            (param[7] = new OracleParameter("P_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Irsms_tp;
            (param[8] = new OracleParameter("P_WARR_PERIOD", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Irsms_warr_period;
            (param[9] = new OracleParameter("P_WARR_REM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Irsms_warr_rem;
            (param[10] = new OracleParameter("P_ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Irsms_act;
            (param[11] = new OracleParameter("P_IRSMS_QTY", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = oItem.Irsms_qty;
            param[12] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int32)UpdateRecords("SP_INSERT_INR_SERMSTSUB", CommandType.StoredProcedure, param);
            return effects;
        }
        //darshana 2015-02-12
        public List<MST_ITM_CAT_COMP> getMasterItmCatComponant(string Cate1, string Cate2, string Cate3)
        {
            List<MST_ITM_CAT_COMP> result = new List<MST_ITM_CAT_COMP>();
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_cat1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Cate1;
            (param[1] = new OracleParameter("p_cat2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Cate2;
            (param[2] = new OracleParameter("p_cat3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Cate3;
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "sp_get_itmcatcomp", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<MST_ITM_CAT_COMP>(_dtResults, MST_ITM_CAT_COMP.Converter);
            }
            return result;
        }

        //Darshana 2015-02-12
        public int Update_INR_SERMSTSUB(Int32 _serId, string _itmCd, Int32 _act)
        {
            OracleParameter[] param = new OracleParameter[4];
            Int32 effects = 0; ;
            (param[0] = new OracleParameter("P_SER_ID", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serId;
            (param[1] = new OracleParameter("P_ITM_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itmCd;
            (param[2] = new OracleParameter("P_ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _act;
            param[3] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int32)UpdateRecords("sp_update_sermstsup_act", CommandType.StoredProcedure, param);
            return effects;
        }
        //Darshana 2015-02-12
        public int SAVE_INR_SERMSTSUB(InventoryWarrantySubDetail oItem)
        {
            OracleParameter[] param = new OracleParameter[13];
            Int32 effects = 0; ;
            (param[0] = new OracleParameter("P_SER_ID", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Irsms_ser_id;
            (param[1] = new OracleParameter("P_SER_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Irsms_ser_line;
            (param[2] = new OracleParameter("P_WARR_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Irsms_warr_no;
            (param[3] = new OracleParameter("P_ITM_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Irsms_itm_cd;
            (param[4] = new OracleParameter("P_ITM_STUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Irsms_itm_stus;
            (param[5] = new OracleParameter("P_SUB_SER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Irsms_sub_ser;
            (param[6] = new OracleParameter("P_MFC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Irsms_mfc;
            (param[7] = new OracleParameter("P_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Irsms_tp;
            (param[8] = new OracleParameter("P_WARR_PERIOD", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Irsms_warr_period;
            (param[9] = new OracleParameter("P_WARR_REM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Irsms_warr_rem;
            (param[10] = new OracleParameter("P_ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Irsms_act;
            (param[11] = new OracleParameter("P_IRSMS_QTY", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = oItem.Irsms_qty;
            param[12] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int32)UpdateRecords("sp_save_inr_sermstsub", CommandType.StoredProcedure, param);
            return effects;
        }

        //Chamal 2015-01-13
        public int Save_Job_Charges(Service_Job_Charges _jobChg)
        {
            OracleParameter[] param = new OracleParameter[14];
            Int32 effects = 0; ;
            (param[0] = new OracleParameter("P_SEQ_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobChg.Sjc_seq_no;
            (param[1] = new OracleParameter("P_JOBNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobChg.Sjc_jobno;
            (param[2] = new OracleParameter("P_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobChg.Sjc_jobline;
            (param[3] = new OracleParameter("P_CHGLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobChg.Sjc_chgline;
            (param[4] = new OracleParameter("P_ITM_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobChg.Sjc_itm_cd;
            (param[5] = new OracleParameter("P_ITM_STUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobChg.Sjc_itm_stus;
            (param[6] = new OracleParameter("P_QTY", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _jobChg.Sjc_qty;
            (param[7] = new OracleParameter("P_UNIT_RT", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _jobChg.Sjc_unit_rt;
            (param[8] = new OracleParameter("P_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobChg.Sjc_rmk;
            (param[9] = new OracleParameter("P_ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobChg.Sjc_act;
            (param[10] = new OracleParameter("P_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobChg.Sjc_cre_by;
            (param[11] = new OracleParameter("P_STAGE", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _jobChg.Sjc_stage;
            (param[12] = new OracleParameter("P_USED", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobChg.Sjc_used;
            param[13] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_SAVE_SCV_JOB_CHG", CommandType.StoredProcedure, param);
            return effects;
        }

        //Darshana 2015-02-16
        public List<InventoryWarrantySubDetail> getSerMstSubBySerID(Int32 _serID)
        {
            List<InventoryWarrantySubDetail> result = new List<InventoryWarrantySubDetail>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_serID", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serID;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "sp_getsermstsub_byserid", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<InventoryWarrantySubDetail>(_dtResults, InventoryWarrantySubDetail.ConvertTotal);
            }
            return result;
        }

        //Tharaka 2015-02-17
        public List<Service_Message> GetMessage()
        {
            List<Service_Message> result = new List<Service_Message>();
            OracleParameter[] param = new OracleParameter[1];
            param[0] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GEL_MSG", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Message>(_dtResults, Service_Message.Converter);
            }
            return result;
        }

        //Tharaka 2015-02-17
        public List<Service_Message_Template> GetMessageTemplates()
        {
            List<Service_Message_Template> result = new List<Service_Message_Template>();
            OracleParameter[] param = new OracleParameter[1];
            param[0] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_MSG_TEMPLATE", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Message_Template>(_dtResults, Service_Message_Template.Converter);
            }
            return result;
        }

        //Tharaka 2015-02-17
        public int UPDATE_SCV_MSG_STUS(Int32 SeqNumber, Int32 Status, Int32 isMail, Int32 isSms, String User)
        {
            OracleParameter[] param = new OracleParameter[6];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = SeqNumber;
            (param[1] = new OracleParameter("P_STATUS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Status;
            (param[2] = new OracleParameter("P_SM_SMS_DONE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = isSms;
            (param[3] = new OracleParameter("P_SM_EMAIL_DONE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = isMail;
            (param[4] = new OracleParameter("P_SM_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = User;
            param[5] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDATE_SCV_MSG_STUS", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2015-02-17
        public int SaveServiceMsg(Service_Message oItem)
        {
            OracleParameter[] param = new OracleParameter[20];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_SM_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Sm_seq;
            (param[1] = new OracleParameter("P_SM_JOBNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Sm_jobno;
            (param[2] = new OracleParameter("P_SM_JOBOLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Sm_joboline;
            (param[3] = new OracleParameter("P_SM_JOBSTAGE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Sm_jobstage;
            (param[4] = new OracleParameter("P_SM_MSG_TMLT_ID", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Sm_msg_tmlt_id;
            (param[5] = new OracleParameter("P_SM_STATUS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Sm_status;
            (param[6] = new OracleParameter("P_SM_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Sm_com;
            (param[7] = new OracleParameter("P_SM_REF_NUM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Sm_ref_num;
            (param[8] = new OracleParameter("P_SM_SMS_TEXT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Sm_sms_text;
            (param[9] = new OracleParameter("P_SM_SMS_GAP", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Sm_sms_gap;
            (param[10] = new OracleParameter("P_SM_SMS_DONE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Sm_sms_done;
            (param[11] = new OracleParameter("P_SM_MAIL_TEXT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Sm_mail_text;
            (param[12] = new OracleParameter("P_SM_MAIL_GAP", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Sm_mail_gap;
            (param[13] = new OracleParameter("P_SM_EMAIL_DONE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Sm_email_done;
            (param[14] = new OracleParameter("P_SM_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Sm_cre_by;
            (param[15] = new OracleParameter("P_SM_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = oItem.Sm_cre_dt;
            (param[16] = new OracleParameter("P_SM_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Sm_mod_by;
            (param[17] = new OracleParameter("P_SM_MOD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = oItem.Sm_mod_dt;
            (param[18] = new OracleParameter("P_SM_EMAIL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Sm_email;
            param[19] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_SAVE_SCV_MSG", CommandType.StoredProcedure, param);
            return effects;
        }
        //Darshana 2015-02-18
        public int SaveBRServiceApproval(BRServiceApproval _app)
        {
            OracleParameter[] param = new OracleParameter[89];
            Int32 effects = 0;
            (param[0] = new OracleParameter("p_inra_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Inra_no;
            (param[1] = new OracleParameter("p_insa_com_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_com_cd;
            (param[2] = new OracleParameter("p_insa_loc_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_loc_cd;
            (param[3] = new OracleParameter("p_insa_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _app.Insa_dt;
            (param[4] = new OracleParameter("p_insa_is_manual", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _app.Insa_is_manual;
            (param[5] = new OracleParameter("p_insa_manual_ref", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_manual_ref;
            (param[6] = new OracleParameter("p_insa_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_tp;
            (param[7] = new OracleParameter("p_insa_sub_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_sub_tp;
            (param[8] = new OracleParameter("p_insa_agent", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_agent;
            (param[9] = new OracleParameter("p_insa_col_method", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_col_method;
            (param[10] = new OracleParameter("p_insa_inv_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_inv_no;
            (param[11] = new OracleParameter("p_insa_acc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_acc_no;
            (param[12] = new OracleParameter("p_insa_inv_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _app.Insa_inv_dt;
            (param[13] = new OracleParameter("p_insa_oth_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_oth_doc_no;
            (param[14] = new OracleParameter("p_insa_oth_doc_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _app.Insa_oth_doc_dt;
            (param[15] = new OracleParameter("p_insa_cust_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_cust_cd;
            (param[16] = new OracleParameter("p_insa_cust_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_cust_name;
            (param[17] = new OracleParameter("p_insa_addr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_addr;
            (param[18] = new OracleParameter("p_insa_tel", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_tel;
            (param[19] = new OracleParameter("p_insa_itm", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_itm;
            (param[20] = new OracleParameter("p_insa_ser", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_ser;
            (param[21] = new OracleParameter("p_insa_warr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_warr;
            (param[22] = new OracleParameter("p_insa_def_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_def_cd;
            (param[23] = new OracleParameter("p_insa_def", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_def;
            (param[24] = new OracleParameter("p_insa_condition", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_condition;
            (param[25] = new OracleParameter("p_insa_accessories", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_accessories;
            (param[26] = new OracleParameter("p_insa_easy_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_easy_loc;
            (param[27] = new OracleParameter("p_insa_insp_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_insp_by;
            (param[28] = new OracleParameter("p_insa_rem1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_rem1;
            (param[29] = new OracleParameter("p_insa_def_rem", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_def_rem;
            (param[30] = new OracleParameter("p_insa_is_jb_open", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _app.Insa_is_jb_open;
            (param[31] = new OracleParameter("p_insa_jb_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_jb_no;
            (param[32] = new OracleParameter("p_insa_open_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_open_by;
            (param[33] = new OracleParameter("p_insa_jb_rem", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_jb_rem;
            (param[34] = new OracleParameter("p_insa_is_repaired", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _app.Insa_is_repaired;
            (param[35] = new OracleParameter("p_insa_repair_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_repair_stus;
            (param[36] = new OracleParameter("p_insa_rem2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_rem2;
            (param[37] = new OracleParameter("p_insa_is_returned", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _app.Insa_is_returned;
            (param[38] = new OracleParameter("p_insa_return_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _app.Insa_return_dt;
            (param[39] = new OracleParameter("p_insa_ret_condition", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_ret_condition;
            (param[40] = new OracleParameter("p_insa_hollogram_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_hollogram_no;
            (param[41] = new OracleParameter("p_insa_is_replace", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _app.Insa_is_replace;
            (param[42] = new OracleParameter("p_insa_is_resell", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _app.Insa_is_resell;
            (param[43] = new OracleParameter("p_insa_is_ret", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _app.Insa_is_ret;
            (param[44] = new OracleParameter("p_insa_is_dispose", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _app.Insa_is_dispose;
            (param[45] = new OracleParameter("p_insa_acknoledge_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _app.Insa_acknoledge_dt;
            (param[46] = new OracleParameter("p_insa_is_complete", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _app.Insa_is_complete;
            (param[47] = new OracleParameter("p_insa_complete_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _app.Insa_complete_dt;
            (param[48] = new OracleParameter("p_insa_complete_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_complete_by;
            (param[49] = new OracleParameter("p_insa_rem3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_rem3;
            (param[50] = new OracleParameter("p_insa_closure_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_closure_tp;
            (param[51] = new OracleParameter("p_insa_stage", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _app.Insa_stage;
            (param[52] = new OracleParameter("p_insa_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_stus;
            (param[53] = new OracleParameter("p_insa_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_cre_by;
            (param[54] = new OracleParameter("p_insa_cre_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _app.Insa_cre_dt;
            (param[55] = new OracleParameter("p_insa_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_mod_by;
            (param[56] = new OracleParameter("p_insa_mod_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _app.Insa_mod_dt;
            (param[57] = new OracleParameter("p_insa_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_session_id;
            (param[58] = new OracleParameter("p_insa_anal1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_anal1;
            (param[59] = new OracleParameter("p_insa_anal2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_anal2;
            (param[60] = new OracleParameter("p_insa_anal3", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _app.Insa_anal3;
            (param[61] = new OracleParameter("p_insa_anal4", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _app.Insa_anal4;
            (param[62] = new OracleParameter("p_insa_anal5", OracleDbType.Date, null, ParameterDirection.Input)).Value = _app.Insa_anal5;
            (param[63] = new OracleParameter("p_insa_anal6", OracleDbType.Date, null, ParameterDirection.Input)).Value = _app.Insa_anal6;
            (param[64] = new OracleParameter("p_insa_anal7", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_anal7;
            (param[65] = new OracleParameter("p_insa_in_stus", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _app.Insa_in_stus;
            (param[66] = new OracleParameter("p_insa_out_stus", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _app.Insa_out_stus;
            (param[67] = new OracleParameter("p_insa_is_external", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _app.Insa_is_external;
            (param[68] = new OracleParameter("p_insa_serial_id", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _app.Insa_serial_id;
            (param[69] = new OracleParameter("p_insa_is_req", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _app.Insa_is_req;
            (param[70] = new OracleParameter("p_insa_app_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _app.Insa_app_dt;
            (param[71] = new OracleParameter("p_insa_app_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_app_by;
            (param[72] = new OracleParameter("p_insa_rej_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _app.Insa_rej_dt;
            (param[73] = new OracleParameter("p_insa_rej_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_rej_by;
            (param[74] = new OracleParameter("p_insa_disp_rem1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_disp_rem1;
            (param[75] = new OracleParameter("p_insa_disp_rem2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_disp_rem2;
            (param[76] = new OracleParameter("p_insa_disp_rem3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_disp_rem3;
            (param[77] = new OracleParameter("p_insa_is_misplace", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _app.Insa_is_misplace;
            (param[78] = new OracleParameter("p_insa_disp_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _app.Insa_disp_no;
            (param[79] = new OracleParameter("p_insa_accept_pending", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _app.Insa_accept_pending;
            (param[80] = new OracleParameter("p_insa_job_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _app.Insa_job_dt;
            (param[81] = new OracleParameter("p_insa_repair_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _app.Insa_repair_dt;
            (param[82] = new OracleParameter("p_insa_disprem1_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _app.Insa_disprem1_dt;
            (param[83] = new OracleParameter("p_insa_disprem2_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _app.Insa_disprem2_dt;
            (param[84] = new OracleParameter("p_insa_disprem3_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _app.Insa_disprem3_dt;
            (param[85] = new OracleParameter("p_insa_rem1_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _app.Insa_rem1_dt;
            (param[86] = new OracleParameter("p_insa_war_period", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _app.Insa_war_period;
            (param[87] = new OracleParameter("p_insa_is_col4_upd", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _app.Insa_is_col4_upd;
            param[88] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_saveintserapp", CommandType.StoredProcedure, param);
            return effects;
        }
        //darshana 19-02-2015
        public List<Service_job_Det> getPrejobDetails(string _com, string _ser, string _itm)
        {
            List<Service_job_Det> result = new List<Service_job_Det>();
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_ser", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ser;
            (param[2] = new OracleParameter("p_itm", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itm;
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "sp_getprejobs", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_job_Det>(_dtResults, Service_job_Det.ConverterPureAll);
            }
            return result;
        }

        public string getJobCreateBy(string _jobno)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_job", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobno;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dt = new DataTable();
            dt = QueryDataTable("tbl", "sp_get_job_creby", CommandType.StoredProcedure, false, param);
            string des = "";
            foreach (DataRow tr in dt.Rows)
            {
                des = (string)tr["CRE_BY"];
            }
            return des;
        }

        //Tharaka 2015-02-17
        public List<Service_job_Det> GET_ALL_PENDING_JOBDET(decimal Stage)
        {
            List<Service_job_Det> result = new List<Service_job_Det>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_STAGE", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = Stage;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_ALL_PENDING_JOBDET", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_job_Det>(_dtResults, Service_job_Det.Converter);
            }
            return result;
        }
        //Tharaka 2015-02-17
        public List<Service_Alert_Log> GET_ALL_PENDING_JOBDET(String job, string com, Int32 jobLine)
        {
            List<Service_Alert_Log> result = new List<Service_Alert_Log>();
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (param[2] = new OracleParameter("P_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = jobLine;
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_ALRT_LOG", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Alert_Log>(_dtResults, Service_Alert_Log.Converter);
            }
            return result;
        }

        //Tharaka 2015-02-17
        public List<scv_prit_task> GET_SCV_PRNT_TASK(String Type, String Code, String Priority)
        {
            List<scv_prit_task> result = new List<scv_prit_task>();
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_TYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Type;
            (param[1] = new OracleParameter("P_CODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Code;
            (param[2] = new OracleParameter("P_PRIORITY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Priority;
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_PRNT_TASK", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<scv_prit_task>(_dtResults, scv_prit_task.ConverterPure);
            }
            return result;
        }

        //Tharaka 2015-02-18
        public List<MST_ITM_CAT_COMP> getMasterItemCategoryComByItem(string Item)
        {
            List<MST_ITM_CAT_COMP> result = new List<MST_ITM_CAT_COMP>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_ITM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_ITM_CAT_COMP_ITM", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<MST_ITM_CAT_COMP>(_dtResults, MST_ITM_CAT_COMP.Converter);
            }
            return result;
        }

        //Tharaka 2015-02-19
        public int SAVE_SCV_PEDING_JOBS(Service_Pending_Jobs oItem)
        {
            OracleParameter[] param = new OracleParameter[48];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.COM;
            (param[1] = new OracleParameter("P_SCHNL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.SCHNL;
            (param[2] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.LOC;
            (param[3] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.JOB;
            (param[4] = new OracleParameter("P_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.JOBLINE;
            (param[5] = new OracleParameter("P_JOB_DATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = oItem.JOB_DATE;
            (param[6] = new OracleParameter("P_JOB_ACTUAL_DATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = oItem.JOB_ACTUAL_DATE;
            (param[7] = new OracleParameter("P_ITEMCODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.ITEMCODE;
            (param[8] = new OracleParameter("P_SERIAL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.SERIAL;
            (param[9] = new OracleParameter("P_ITM_DESC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.ITM_DESC;
            (param[10] = new OracleParameter("P_MODEL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.MODEL;
            (param[11] = new OracleParameter("P_BRAND", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.BRAND;
            (param[12] = new OracleParameter("P_CAT1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.CAT1;
            (param[13] = new OracleParameter("P_CAT2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.CAT2;
            (param[14] = new OracleParameter("P_CAT3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.CAT3;
            (param[15] = new OracleParameter("P_JOB_CATE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.JOB_CATE;
            (param[16] = new OracleParameter("P_JOB_DIRC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.JOB_DIRC;
            (param[17] = new OracleParameter("P_JOB_SUBTYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.JOB_SUBTYPE;
            (param[18] = new OracleParameter("P_JOB_STAGE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.JOB_STAGE;
            (param[19] = new OracleParameter("P_JOB_STAGE_TEXT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.JOB_STAGE_TEXT;
            (param[20] = new OracleParameter("P_CUST_CODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.CUST_CODE;
            (param[21] = new OracleParameter("P_CUST_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.CUST_NAME;
            (param[22] = new OracleParameter("P_TECHNICAN_COMMENT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.TECHNICAN_COMMENT;
            (param[23] = new OracleParameter("P_PENDING_DATES", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.PENDING_DATES;
            (param[24] = new OracleParameter("P_WARR_STATUS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.WARR_STATUS;
            (param[25] = new OracleParameter("P_EMP_LVL1_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.EMP_LVL1_CD;
            (param[26] = new OracleParameter("P_EMP_LVL2_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.EMP_LVL2_CD;
            (param[27] = new OracleParameter("P_EMP_LVL3_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.EMP_LVL3_CD;
            (param[28] = new OracleParameter("P_EMP_LVL4_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.EMP_LVL4_CD;
            (param[29] = new OracleParameter("P_EMP_LVL5_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.EMP_LVL5_CD;
            (param[30] = new OracleParameter("P_EMP_LVL6_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.EMP_LVL6_CD;
            (param[31] = new OracleParameter("P_EMP_LVL1_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.EMP_LVL1_NAME;
            (param[32] = new OracleParameter("P_EMP_LVL2_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.EMP_LVL2_NAME;
            (param[33] = new OracleParameter("P_EMP_LVL3_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.EMP_LVL3_NAME;
            (param[34] = new OracleParameter("P_EMP_LVL4_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.EMP_LVL4_NAME;
            (param[35] = new OracleParameter("P_EMP_LVL5_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.EMP_LVL5_NAME;
            (param[36] = new OracleParameter("P_EMP_LVL6_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.EMP_LVL6_NAME;
            (param[37] = new OracleParameter("P_JOB_DIRECT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.JOB_DIRECT;
            (param[38] = new OracleParameter("P_ANAL_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.ANAL_1;
            (param[39] = new OracleParameter("P_ANAL_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.ANAL_2;
            (param[40] = new OracleParameter("P_ANAL_3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.ANAL_3;
            (param[41] = new OracleParameter("P_ANAL_4", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.ANAL_4;
            (param[42] = new OracleParameter("P_ANAL_5", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.ANAL_5;
            (param[43] = new OracleParameter("P_ANAL_6", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.ANAL_6;
            (param[44] = new OracleParameter("P_ANAL_7", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.ANAL_7;
            (param[45] = new OracleParameter("P_PC_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.PC_CD;
            (param[46] = new OracleParameter("P_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.CRE_BY;

            param[47] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_SAVE_SCV_PEDING_JOBS", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2015-02-19
        public int DELETE_SCV_PEDING_JOBS()
        {
            OracleParameter[] param = new OracleParameter[1];
            Int32 effects = 0;
            param[0] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_DELETE_SCV_PEDING_JOBS", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2015-02-19
        public MasterServiceEmployee GetScvEmployee(String com, String PartyType, String PartyCode, String emp)
        {
            MasterServiceEmployee result = new MasterServiceEmployee();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("P_PTY_TYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PartyType;
            (param[2] = new OracleParameter("P_PTY_CODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PartyCode;
            (param[3] = new OracleParameter("P_EMP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = emp;
            param[4] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SC_EMP_BY_PARTY", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<MasterServiceEmployee>(_dtResults, MasterServiceEmployee.Converter)[0];
            }
            return result;
        }

        //Tharaka 2015-02-17
        public List<Service_Pending_Jobs> GetPendingJobSCMREPP(String level, String empCOde, String com, String chnl, String loc)
        {
            List<Service_Pending_Jobs> result = new List<Service_Pending_Jobs>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("P_LEVEL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = level;
            (param[1] = new OracleParameter("P_EMP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = empCOde;
            (param[2] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[3] = new OracleParameter("P_CHNL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = chnl;
            (param[4] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = loc;
            param[5] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_PENDING_JOB_SCMREP", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                if (level == "EMP_LVL1_CD")
                {
                    result = DataTableExtensions.ToGenericList<Service_Pending_Jobs>(_dtResults, Service_Pending_Jobs.ConverterL1);
                }
                if (level == "EMP_LVL2_CD")
                {
                    result = DataTableExtensions.ToGenericList<Service_Pending_Jobs>(_dtResults, Service_Pending_Jobs.ConverterL2);
                }
                if (level == "EMP_LVL3_CD")
                {
                    result = DataTableExtensions.ToGenericList<Service_Pending_Jobs>(_dtResults, Service_Pending_Jobs.ConverterL3);
                }
            }
            return result;
        }

        //Tharaka 2015-02-20
        public List<String> GetPendingJObDistinctEMPs(String level, String com, String chnl, String loc)
        {
            List<String> result = new List<String>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("P_LEVEL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = level;
            (param[1] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[2] = new OracleParameter("P_CHNL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = chnl;
            (param[3] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = loc;
            param[4] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_PENDING_EMPS", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                for (int i = 0; i < _dtResults.Rows.Count; i++)
                {
                    result.Add(_dtResults.Rows[i][0].ToString());
                }
            }
            return result;
        }

        //Chamal  2015-02-20
        public int Save_Inr_SerMst_Bulk(string _warrNo)
        {
            OracleParameter[] param = new OracleParameter[2];
            Int32 effects = 0;
            (param[0] = new OracleParameter("p_warr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warrNo;
            param[1] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_save_inr_sermst_bulk", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2015-02-20
        public SystemUser GetSystemuserbyEMP(String EMP)
        {
            SystemUser result = new SystemUser();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_EMP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = EMP;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SEC_USER_BY_EMP", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<SystemUser>(_dtResults, SystemUser.converter)[0];
            }
            return result;
        }

        //Tharaka 2015-02-20
        public MsgInformation GET_MST_MSG_INFOBASE_BY_EMP(String EMP, String com, String loc, String DocType)
        {
            MsgInformation result = new MsgInformation();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("P_EMP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = EMP;
            (param[1] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[2] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = loc;
            (param[3] = new OracleParameter("P_DOCTYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = DocType;
            param[4] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_MST_MSG_INFOBASE_BY_EMP", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<MsgInformation>(_dtResults, MsgInformation.Converter)[0];
            }
            return result;
        }

        //Tharaka 2015-02-20
        public MST_ScvEmpCate GET_SCV_EMPCATE(String com, String Chanl, String CATE)
        {
            MST_ScvEmpCate result = new MST_ScvEmpCate();
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("P_CHANL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Chanl;
            (param[2] = new OracleParameter("P_CATE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = CATE;
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_EMPCATE", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<MST_ScvEmpCate>(_dtResults, MST_ScvEmpCate.Converter)[0];
            }
            return result;
        }

        //Tharaka 2015-02-23
        public int UPDATE_MSGINFOBASE_MODDT(MsgInformation oItem)
        {
            OracleParameter[] param = new OracleParameter[5];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_MODDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = oItem.Mmi_mod_dt;
            (param[1] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Mmi_com;
            (param[2] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Mmi_loc;
            (param[3] = new OracleParameter("P_USERID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Mmi_receiver;
            param[4] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDATE_MSGINFOBASE_MODDT", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2015-02-23
        public List<COM_CHNL_LOC> GET_DISTINCT_COM_CHN_LOC()
        {
            List<COM_CHNL_LOC> result = new List<COM_CHNL_LOC>();
            OracleParameter[] param = new OracleParameter[1];
            param[0] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_DISTINCT_COM_CHN_LOC", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<COM_CHNL_LOC>(_dtResults, COM_CHNL_LOC.Converter);
            }
            return result;
        }


        //Nadeeka 29-06-2015
        public DataTable get_supp_claim_amt(string _com, string _claimno)
        {

            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("P_CLAIMNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _claimno;
            param[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_SUPP_CLAIM_INV_AMT", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }


        //Nadeeka 07-11-2015
        public DataTable get_ServiceAgreement(string _com, string _pc, DateTime _fromdate, DateTime _todate)
        {

            OracleParameter[] param = new OracleParameter[5];

            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[2] = new OracleParameter("in_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            (param[3] = new OracleParameter("in_from", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fromdate;
            (param[4] = new OracleParameter("in_todate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _todate;

            DataTable _dtResults = QueryDataTable("tbl", "sp_service_Agreement", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }


        public DataTable get_ServiceIncentive(string _com, string _pc, DateTime _fromdate, DateTime _todate, string _reqcat)
        {
            //Sanjeewa 2015-11-09
            OracleParameter[] param = new OracleParameter[6];

            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[2] = new OracleParameter("in_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            (param[3] = new OracleParameter("in_from", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fromdate;
            (param[4] = new OracleParameter("in_todate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _todate;
            (param[5] = new OracleParameter("in_reqcat", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqcat;

            DataTable _dtResults = QueryDataTable("tbl", "sp_service_Incentive", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        public DataTable get_HPSystemParaDetail(string _type, string _value, string _code, DateTime _date)
        {
            //Sanjeewa 2015-11-19
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _type;
            (param[1] = new OracleParameter("p_value", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _value;
            (param[2] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _code;
            (param[3] = new OracleParameter("p_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = _date;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "sp_getsyspara", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }
        public DataTable get_MsgInformation(string _company, string _location, string _documenttype)
        {
            //Sanjeewa 2015-11-19
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _location;
            (param[2] = new OracleParameter("p_doctype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _documenttype;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "sp_getmsginformation", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }


        public DataTable get_RepeatedJobDetail(string _item, string _serial, DateTime _date)
        {
            //Sanjeewa 2015-11-18
            OracleParameter[] param = new OracleParameter[4];

            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("in_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[2] = new OracleParameter("in_serial", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial;
            (param[3] = new OracleParameter("in_date", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _date;

            DataTable _dtResults = QueryDataTable("tbl", "sp_get_repeat_job", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        public DataTable get_JobHeader(string _job)
        {
            //Sanjeewa 2016-05-13
            OracleParameter[] param = new OracleParameter[2];

            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("in_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _job;

            DataTable _dtResults = QueryDataTable("tbl", "sp_job_header", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        public DataTable get_RepeatedJobTechnicianDetail(string _job)
        {
            //Sanjeewa 2015-11-18
            OracleParameter[] param = new OracleParameter[2];

            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("in_job", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _job;

            DataTable _dtResults = QueryDataTable("tbl", "sp_get_job_technician", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }


        //Darshana 2015-02-25
        public decimal GetScvJobStageCost(string _com, string _schnl, string _loc, string _scvCate, decimal _qty, DateTime _date, string _jobStage, string _item)
        {
            bool _found = false;
            decimal _chg = 0;
            DataTable _d0 = new DataTable();
            OracleParameter[] param = new OracleParameter[9];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "LOC";
            (param[2] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            (param[3] = new OracleParameter("p_taskloc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scvCate;
            (param[4] = new OracleParameter("p_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _qty;
            (param[5] = new OracleParameter("p_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = _date.Date;
            (param[6] = new OracleParameter("p_js", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobStage;
            (param[7] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            param[8] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _d0 = QueryDataTable("tblZPC", "sp_GetScvChg", CommandType.StoredProcedure, false, param);

            if (_d0 != null)
            {
                if (_d0.Rows.Count > 0) _found = true;
            }

            if (_found == false)
            {
                _d0 = new DataTable();
                param = new OracleParameter[9];
                (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
                (param[1] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "SCHNL";
                (param[2] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _schnl;
                (param[3] = new OracleParameter("p_taskloc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scvCate;
                (param[4] = new OracleParameter("p_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _qty;
                (param[5] = new OracleParameter("p_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = _date.Date;
                (param[6] = new OracleParameter("p_js", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobStage;
                (param[7] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
                param[8] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                _d0 = QueryDataTable("tblZPC", "sp_GetScvChg", CommandType.StoredProcedure, false, param);

                if (_d0 != null)
                {
                    if (_d0.Rows.Count > 0) _found = true;
                }
            }

            if (_found == false)
            {
                return -1;
            }

            if (_d0.Rows.Count > 0)
            {
                for (int i = 0; i < _d0.Rows.Count; i++)
                {
                    _chg = Convert.ToDecimal(_d0.Rows[i]["scg_cost"].ToString());
                    break;
                }
            }

            return _chg;
        }

        //Darshana on 27-02-2015
        public DataTable GetInvDetBySerId(string _invoice, Int32 _serID, string _itm)
        {
            //sp_getinvoicewithserial(p_invoice in NVARCHAR2,c_data out sys_refcursor)
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_inv", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoice;
            (param[1] = new OracleParameter("p_serID", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serID;
            (param[2] = new OracleParameter("p_itm", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itm;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblData = QueryDataTable("tbData", "sp_getinvdetbyserid", CommandType.StoredProcedure, false, param);
            return _tblData;

        }

        //Tharaka 2015-02-19
        public MasterServiceEmployee GetScvEmployeeByCode(String com, String emp)
        {
            MasterServiceEmployee result = new MasterServiceEmployee();
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("P_EMP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = emp;
            param[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SC_EMP_BY_CODE", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<MasterServiceEmployee>(_dtResults, MasterServiceEmployee.Converter)[0];
            }
            return result;
        }

        //Tharaka 2015-03-16
        public Service_Message_Template GetMessageTemplates_byID(String com, String Chnal, Int32 Id)
        {
            Service_Message_Template result = new Service_Message_Template();
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("P_CHNL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Chnal;
            (param[2] = new OracleParameter("P_ID", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Id;
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "sp_get_msg_template_byid", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Message_Template>(_dtResults, Service_Message_Template.Converter)[0];
            }
            return result;
        }

        //Darshana 2015-03-18
        public Service_confirm_Header GetConfDetByJob(string _com, string _jobNo, string _confNo)
        {
            Service_confirm_Header result = new Service_confirm_Header();
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_jobNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobNo;
            (param[2] = new OracleParameter("p_confNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confNo;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "sp_getconfbyjob", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_confirm_Header>(_dtResults, Service_confirm_Header.Converter)[0];
            }
            return result;
        }

        public DataTable GetJobConfByJob(string _com, string _jobNo, string _confNo)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_jobNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobNo;
            (param[2] = new OracleParameter("p_confNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _confNo;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "sp_getconfbyjob", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        //Nadeeka 2015-03-19
        public List<Service_confirm_Header> GetConfDetByJobNo(string _com, string _jobNo, Int32 _jobLine)
        {
            List<Service_confirm_Header> result = new List<Service_confirm_Header>();
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_jobNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobNo;
            (param[2] = new OracleParameter("p_jobLine", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobLine;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblnew", "sp_getconfbyjobNo", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_confirm_Header>(_dtResults, Service_confirm_Header.Converter);
            }
            return result;
        }

        public DataTable Get_service_originalLoc(string _job)
        {
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("p_jobNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _job;
            _para[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblsupdetails", "sp_getOriginalLocation", CommandType.StoredProcedure, false, _para);
            return _dtResults;
        }

        //Tharaka 2015-03-20
        public MasterServiceEmployee GetScvEmployeeBYCATE(String com, String PartyType, String PartyCode, String empCATE)
        {
            MasterServiceEmployee result = new MasterServiceEmployee();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("P_PTY_TYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PartyType;
            (param[2] = new OracleParameter("P_PTY_CODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PartyCode;
            (param[3] = new OracleParameter("P_EMP_CATE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = empCATE;
            param[4] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SC_EMP_BY_CATE", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<MasterServiceEmployee>(_dtResults, MasterServiceEmployee.Converter)[0];
            }
            return result;
        }

        //Tharaka 2015-03-21
        public List<MasterServiceEmployee> GET_SC_EMP_BY_LOC_PC(String com, String loc, String pc, String Chnl, String emp)
        {
            List<MasterServiceEmployee> result = new List<MasterServiceEmployee>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = loc;
            (param[2] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
            (param[3] = new OracleParameter("P_CHNL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Chnl;
            (param[4] = new OracleParameter("P_EMP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = emp;
            param[5] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SC_EMP_BY_LOC_PC", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<MasterServiceEmployee>(_dtResults, MasterServiceEmployee.Converter);
            }
            return result;

        }

        //Tharaka 2015-05-15
        public SCV_TRANS_LOG GetTrasferDetailsEnquiry(String job, Int32 Line, String loc)
        {
            SCV_TRANS_LOG result = new SCV_TRANS_LOG();
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (param[1] = new OracleParameter("P_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Line;
            (param[2] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = loc;
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_TRSJOBDET_ENQUIRY", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<SCV_TRANS_LOG>(_dtResults, SCV_TRANS_LOG.Converter)[0];
            }
            return result;
        }
        //Darshana 2015-05-19
        public Int32 Update_Conf_Cancel(string Status, Int32 seq, string com, string ConfirmNum, string _user)
        {
            OracleParameter[] param = new OracleParameter[6];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_STATUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Status;
            (param[1] = new OracleParameter("P_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = seq;
            (param[2] = new OracleParameter("P_CONNUM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = ConfirmNum;
            (param[3] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[4] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            param[5] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_conf_can_stus", CommandType.StoredProcedure, param);
            return effects;

        }

        //Tharaka 2015-05-29
        public List<Service_stockReturn> GetStandyPendingADOItems(string Com, string job, Int32 jobline, string Item, string LOC)
        {
            List<Service_stockReturn> result = new List<Service_stockReturn>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[1] = new OracleParameter("P_WLOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = LOC;
            (param[2] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (param[3] = new OracleParameter("P_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = jobline;
            param[4] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_PENDING_ADOS", CommandType.StoredProcedure, false, param);
            List<Service_stockReturn> result2 = new List<Service_stockReturn>();
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_stockReturn>(_dtResults, Service_stockReturn.Converter2);

                if (result.FindAll(x => x.INB_ITM_STUS == "STDBY").Count > 0)
                {
                    result2 = result.FindAll(x => x.INB_ITM_STUS == "STDBY");
                }
            }
            return result2;
        }

        //Tharaka 2015-06-01
        public DataTable GET_CON_HDRS_JOB_COM(string Com, string job)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[1] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            param[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_CON_HDRS_JOB_COM", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        //Darshana 2015-06-15
        public Int32 Update_Warranty_mpcb(string _com, string _jobItm, string _jobSer, string _jobWarr, Int32 _jobSerID, string _newItm, string _newSer, Int32 _newSerID)
        {
            OracleParameter[] param = new OracleParameter[9];
            Int32 effects = 0;
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_jobitm", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobItm;
            (param[2] = new OracleParameter("p_jobser", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobSer;
            (param[3] = new OracleParameter("p_jobwarr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobWarr;
            (param[4] = new OracleParameter("p_jobserID", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobSerID;
            (param[5] = new OracleParameter("p_newitm", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _newItm;
            (param[6] = new OracleParameter("p_newser", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _newSer;
            (param[7] = new OracleParameter("p_newserID", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _newSerID;
            param[8] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_update_warr_mpcb", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2015-06-17
        public Int32 UpdateJobSubItemRetrun(Int32 RTNWH, Int32 SEQ, String Serial)
        {
            OracleParameter[] param = new OracleParameter[4];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_RTNWH", OracleDbType.Int32, null, ParameterDirection.Input)).Value = RTNWH;
            (param[1] = new OracleParameter("P_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = SEQ;
            (param[2] = new OracleParameter("P_SER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Serial;
            param[3] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDATE_SCV_SUB_RTN_WH", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2015-06-17
        public List<Service_Job_Det_Sub> GetServiceJobDetailSubItemsBySerial(Int32 Seq, String serial)
        {
            List<Service_Job_Det_Sub> result = new List<Service_Job_Det_Sub>();
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Seq;
            (param[1] = new OracleParameter("P_SER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = serial;
            param[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_JOB_DET_SUB_SER", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Job_Det_Sub>(_dtResults, Service_Job_Det_Sub.Converter);
            }
            return result;
        }
        //Darshana 2015-06-19
        public DataTable GetInvDetBySerial(string _invoice, string _serial, string _itm)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_inv", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoice;
            (param[1] = new OracleParameter("p_ser", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial;
            (param[2] = new OracleParameter("p_itm", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itm;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblData = QueryDataTable("tbData", "sp_getinvdetbyserial", CommandType.StoredProcedure, false, param);
            return _tblData;

        }
        //Darshana 2015-06-19
        public DataTable GetInvDetfrmScm(string _invoice, string _itm)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_inv", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoice;
            (param[1] = new OracleParameter("p_itm", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itm;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblData = QueryDataTable("tbData", "sp_getinvdetfrmscm", CommandType.StoredProcedure, false, param);
            return _tblData;

        }
        //Darshana 2015-06-19
        public DataTable GetNorPbDet(string _com, string _schnl)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_schnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _schnl;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblData = QueryDataTable("tbData", "sp_getnorpbdet", CommandType.StoredProcedure, false, param);
            return _tblData;

        }

        //Tharaka 2015-06-18
        public Int32 DELETE_JOB_DET_SUB(Int32 Seq, String job, Int32 JobLine)
        {
            OracleParameter[] param = new OracleParameter[4];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_SEQ_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Seq;
            (param[1] = new OracleParameter("P_JOBNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (param[2] = new OracleParameter("P_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = JobLine;
            param[3] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_DELETE_JOB_DET_SUB", CommandType.StoredProcedure, param);
            return effects;
        }

        public Int32 DeleteCustFeedback(String job, Int32 Line)
        {
            OracleParameter[] param = new OracleParameter[3];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_JOBNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (param[1] = new OracleParameter("P_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Line;
            param[2] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_del_cust_feedbk", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2015-06-20
        public DataTable GET_TEMPWARR_BY_SERAL(string Serial)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_SERIAL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Serial;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblData = QueryDataTable("tbData", "SP_GET_TEMPWARR_BY_SERAL", CommandType.StoredProcedure, false, param);
            return _tblData;
        }

        //Tharaka 2015-06-20
        public DataTable GET_SERIALDETFROMSCM(string inovice, String ItemCOde)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_INVOICE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = inovice;
            (param[1] = new OracleParameter("P_ITMECODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = ItemCOde;
            param[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblData = QueryDataTable("tbData", "SP_GET_SERIALDETFROMSCM", CommandType.StoredProcedure, false, param);
            return _tblData;
        }
        //Darshana 2015-06-29
        public DataTable GetInvDetWithDofrmScm(string _invoice, string _itm, string _ser)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_inv", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoice;
            (param[1] = new OracleParameter("p_itm", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itm;
            (param[2] = new OracleParameter("p_ser", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ser;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblData = QueryDataTable("tbData", "sp_geninvdetwithdofrmscm", CommandType.StoredProcedure, false, param);
            return _tblData;

        }

        //Tharaka 2015-07-06
        public Int32 UPDATE_SCV_CONF_HDR_ISINVD_type(Int32 Status, Int32 seq, string com, String jobNum, String InvoiceType)
        {
            OracleParameter[] param = new OracleParameter[6];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_STATUS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Status;
            (param[1] = new OracleParameter("P_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = seq;
            (param[2] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[3] = new OracleParameter("P_JOBNUM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobNum;
            (param[4] = new OracleParameter("P_INVTYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = InvoiceType;
            param[5] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDT_SCV_CNF_HDR_ISIND_TYPE", CommandType.StoredProcedure, param);
            return effects;
        }
        //Darshana 09-Jul-2015
        public int Save_Warr_Replacement(Warr_Replacement_Det _warrRepDet)
        {
            OracleParameter[] param = new OracleParameter[16];
            (param[0] = new OracleParameter("p_swr_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warrRepDet.Swr_tp;
            (param[1] = new OracleParameter("p_swr_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _warrRepDet.Swr_line;
            (param[2] = new OracleParameter("p_swr_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _warrRepDet.Swr_dt;
            (param[3] = new OracleParameter("p_swr_ref", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warrRepDet.Swr_ref;
            (param[4] = new OracleParameter("p_swr_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warrRepDet.Swr_jobno;
            (param[5] = new OracleParameter("p_swr_job_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _warrRepDet.Swr_job_line;
            (param[6] = new OracleParameter("p_swr_o_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warrRepDet.Swr_o_itm_cd;
            (param[7] = new OracleParameter("p_swr_o_itm_ser", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warrRepDet.Swr_o_itm_ser;
            (param[8] = new OracleParameter("p_swr_o_warr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warrRepDet.Swr_o_warr;
            (param[9] = new OracleParameter("p_swr_n_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warrRepDet.Swr_n_itm_cd;
            (param[10] = new OracleParameter("p_swr_n_itm_ser", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warrRepDet.Swr_n_itm_ser;
            (param[11] = new OracleParameter("p_swr_n_warr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warrRepDet.Swr_n_warr;
            (param[12] = new OracleParameter("p_swr_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _warrRepDet.Swr_act;
            (param[13] = new OracleParameter("p_swr_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warrRepDet.Swr_cre_by;
            (param[14] = new OracleParameter("p_swr_rep_val", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _warrRepDet.Swr_rep_val;
            param[15] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return (Int32)UpdateRecords("sp_save_warr_replace", CommandType.StoredProcedure, param);
        }
        //DArshana 09-Jul-2015
        public int Update_warr_rep_stus(Warr_Replacement_Det _warrRepStatus)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_swr_ref", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warrRepStatus.Swr_ref;
            (param[1] = new OracleParameter("p_swr_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _warrRepStatus.Swr_act;
            (param[2] = new OracleParameter("p_canby", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warrRepStatus.Swr_cnl_by;
            param[3] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return (Int32)UpdateRecords("sp_update_warr_rep", CommandType.StoredProcedure, param);
        }

        //Tharka 2015-07-13
        public DataTable GET_WRR_RPLC_DETAILS(string Com, string job)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[1] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _tblData = QueryDataTable("tbData", "SP_GET_WRR_RPLC_DETAILS", CommandType.StoredProcedure, false, param);
            return _tblData;
        }
        public DataTable check_Invoiced_JobClosed(string _invoice)
        {
            OracleParameter[] param = new OracleParameter[2];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("in_invoice", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoice;
            DataTable _tblData = QueryDataTable("tbData", "sp_check_invoiced_job_close", CommandType.StoredProcedure, false, param);
            return _tblData;
        }

        //Tharka 2015-07-13
        public DataTable GET_WRR_INVTRYDET_BY_SUBDOC(string doc)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_DOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = doc;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _tblData = QueryDataTable("tbData", "SP_GET_WRR_INVTRYDET_BY_SUBDOC", CommandType.StoredProcedure, false, param);
            return _tblData;
        }

        //Tharka 2015-07-13
        public DataTable GET_WARR_RPLCE_SERIALS(string job)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_DOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _tblData = QueryDataTable("tbData", "SP_GET_WARR_RPLCE_SERIALS", CommandType.StoredProcedure, false, param);
            return _tblData;
        }

        //Tharka 2015-07-13
        public DataTable GET_WRR_SER_BY_DOC(string doc)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_DOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = doc;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _tblData = QueryDataTable("tbData", "SP_GET_WRR_SER_BY_DOC", CommandType.StoredProcedure, false, param);
            return _tblData;
        }

        //Darshana 13-07-2015
        public int Update_Warr_rep_ReqCan(string _jobNo, Int32 _jobLine, string _user)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_jobNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobNo;
            (param[1] = new OracleParameter("p_jobLineNo", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobLine;
            (param[2] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            param[3] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return (Int32)UpdateRecords("sp_update_warrrepreq_can", CommandType.StoredProcedure, param);
        }
        //DArshana 13-07-2015
        public List<RequestApprovalHeader> GetWarrRepReqByJobNumber(string _jobNo, Int32 _jobLineNo)
        {
            List<RequestApprovalHeader> result = new List<RequestApprovalHeader>();
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_jobNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobNo;
            (param[1] = new OracleParameter("p_jobLine", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobLineNo;
            param[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "sp_get_wrplreq", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<RequestApprovalHeader>(_dtResults, RequestApprovalHeader.Converter);
            }
            return result;
        }

        //Tharaka 2015-07-16
        public List<InventoryHeader> GET_ADO_BY_INVOICE(String invoiceNUm, String COm, String Loc)
        {
            List<InventoryHeader> result = new List<InventoryHeader>();
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_INVOICE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = invoiceNUm;
            (param[1] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = COm;
            (param[2] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Loc;
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_ADO_BY_INVOICE", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<InventoryHeader>(_dtResults, InventoryHeader.ConvertTotal);
            }
            return result;
        }

        //Tharaka 2015-07-16
        public List<InventoryHeader> GET_OTH_LOC_AODIN_BY_OTHDOC(String OtherDoc, String COm, String Loc)
        {
            List<InventoryHeader> result = new List<InventoryHeader>();
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = COm;
            (param[1] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Loc;
            (param[2] = new OracleParameter("P_ODOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = OtherDoc;
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_OTH_LOC_AODIN_BY_OTHDOC", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<InventoryHeader>(_dtResults, InventoryHeader.ConvertTotal);
            }
            return result;
        }
        //Darshana 16-07-2015
        public Warr_Replacement_Det GetWarrantyReplacementHistory(string _nItm, string _nSer, string _tp, string _nWara)
        {
            Warr_Replacement_Det result = new Warr_Replacement_Det();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_newjobItm", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _nItm;
            (param[1] = new OracleParameter("p_newjobSer", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _nSer;
            (param[2] = new OracleParameter("p_repTp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _tp;
            (param[3] = new OracleParameter("p_newWara", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _nWara;
            param[4] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "sp_get_repdet_fornewserial", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Warr_Replacement_Det>(_dtResults, Warr_Replacement_Det.Converter)[0];
            }
            return result;
        }
        //Darshana 17-07-2015
        public Refund_credit_definition GetRefundCreditDefinition(string _com, string _ptyTp, string _ptyCd, string _itm, string _cat1, string _cat2, string _cat3, string _brnd, Int32 _pd, DateTime _processDt, DateTime _invDt)
        {
            Refund_credit_definition result = new Refund_credit_definition();
            OracleParameter[] param = new OracleParameter[12];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_ptyTp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ptyTp;
            (param[2] = new OracleParameter("p_ptyCd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ptyCd;
            (param[3] = new OracleParameter("p_itm", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itm;
            (param[4] = new OracleParameter("p_cat1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat1;
            (param[5] = new OracleParameter("p_cat2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat2;
            (param[6] = new OracleParameter("p_cat3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat3;
            (param[7] = new OracleParameter("p_brnd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _brnd;
            (param[8] = new OracleParameter("p_pd", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _pd;
            (param[9] = new OracleParameter("p_prossDt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _processDt;
            (param[10] = new OracleParameter("p_invDt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _invDt;
            param[11] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "sp_getref_defn", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Refund_credit_definition>(_dtResults, Refund_credit_definition.Converter)[0];
            }
            return result;

        }


        //Tharka 2015-07-21
        public DataTable GET_WARR_DET_BY_NEW_SER(string Serial)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_SER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Serial;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _tblData = QueryDataTable("tbData", "SP_GET_WARR_DET_BY_NEW_SER", CommandType.StoredProcedure, false, param);
            return _tblData;
        }

        //Tharaka 2015-07-21
        public List<Service_Enquiry_Job_Det> GET_ENQUIRY_DET_BY_JOB_LINE(String job, Int32 Line)
        {
            List<Service_Enquiry_Job_Det> result = new List<Service_Enquiry_Job_Det>();
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (_para[1] = new OracleParameter("P_LINE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Line;
            _para[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_GET_ENQUIRY_DET_BY_JOB_LINE", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Enquiry_Job_Det>(_dtResults, Service_Enquiry_Job_Det.Converter);
            }
            return result;
        }

        //Tharka 2015-07-21
        public DataTable GET_WARR_DET_BY_OLD_SER(string Serial)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_SER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Serial;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _tblData = QueryDataTable("tbData", "SP_GET_WARR_DET_BY_OLD_SER", CommandType.StoredProcedure, false, param);
            return _tblData;
        }

        //Tharaka 2015-07-26
        public MasterServiceEmployee GetScvEmployeeBYCATE2(String com, String PartyType, String PartyCode, String empCATE)
        {
            MasterServiceEmployee result = new MasterServiceEmployee();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("P_PTY_TYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PartyType;
            (param[2] = new OracleParameter("P_PTY_CODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PartyCode;
            (param[3] = new OracleParameter("P_EMP_CATE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = empCATE;
            param[4] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SC_EMP_BY_CATE2", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<MasterServiceEmployee>(_dtResults, MasterServiceEmployee.Converter)[0];
            }
            return result;
        }

        //Tharaka 2015-07-29
        public DataTable GET_SERIALDETFROMSCM2(string inovice, String ItemCOde)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_INVOICE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = inovice;
            (param[1] = new OracleParameter("P_ITMECODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = ItemCOde;
            param[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblData = QueryDataTable("tbData", "SP_GET_SERIALDETFROMSCM2", CommandType.StoredProcedure, false, param);
            return _tblData;

        }

        //Tharaka 2015-08-12
        public List<Service_Pending_Jobs> GetPendingJobSCMREPPByLoc(String level, String empCOde, String com, String chnl, String loc)
        {
            List<Service_Pending_Jobs> result = new List<Service_Pending_Jobs>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("P_LEVEL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = level;
            (param[1] = new OracleParameter("P_EMP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = empCOde;
            (param[2] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[3] = new OracleParameter("P_CHNL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = chnl;
            (param[4] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = loc;
            param[5] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_PENDING_JOB_BY_LOC", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Pending_Jobs>(_dtResults, Service_Pending_Jobs.ConverterL1);
            }
            return result;
        }

        //Tharaka 2015-08-13
        public List<Service_Pending_Jobs> GET_PENDING_REQUEST()
        {
            List<Service_Pending_Jobs> result = new List<Service_Pending_Jobs>();
            OracleParameter[] param = new OracleParameter[1];
            param[0] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_PENDING_REQUEST", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Pending_Jobs>(_dtResults, Service_Pending_Jobs.Converter);
            }
            return result;
        }

        //Tharaka 2015-08-13
        public DataTable GET_INT_HDR_FOR_ENQRY_BYDOC(String doc)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_DOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = doc;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblData = QueryDataTable("tbData", "SP_GET_INT_HDR_FOR_ENQRY_BYDOC", CommandType.StoredProcedure, false, param);
            return _tblData;
        }

        //Tharaka 2015-08-19
        public Int32 UpdateInspectionSerial(Service_job_Det _jobDet, out Int32 Newline)
        {
            Newline = 0;
            Int32 effects = 0;
            OracleParameter[] param = new OracleParameter[111];
            (param[0] = new OracleParameter("P_JBD_SEQ_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_seq_no;
            (param[1] = new OracleParameter("P_JBD_JOBNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_jobno;
            (param[2] = new OracleParameter("P_JBD_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_jobline;
            (param[3] = new OracleParameter("P_JBD_SJOBNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_sjobno;
            (param[4] = new OracleParameter("P_JBD_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_loc;
            (param[5] = new OracleParameter("P_JBD_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_pc;
            (param[6] = new OracleParameter("P_JBD_ITM_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_itm_cd;
            (param[7] = new OracleParameter("P_JBD_ITM_STUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_itm_stus;
            (param[8] = new OracleParameter("P_JBD_ITM_DESC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_itm_desc;
            (param[9] = new OracleParameter("P_JBD_BRAND", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_brand;
            (param[10] = new OracleParameter("P_JBD_MODEL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_model;
            (param[11] = new OracleParameter("P_JBD_ITM_COST", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_itm_cost;
            (param[12] = new OracleParameter("P_JBD_SER1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_ser1;
            (param[13] = new OracleParameter("P_JBD_SER2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_ser2;
            (param[14] = new OracleParameter("P_JBD_WARR", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_warr;
            (param[15] = new OracleParameter("P_JBD_REGNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_regno;
            (param[16] = new OracleParameter("P_JBD_MILAGE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_milage;
            (param[17] = new OracleParameter("P_JBD_WARR_STUS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_warr_stus;
            (param[18] = new OracleParameter("P_JBD_ONLOAN", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_onloan;
            (param[19] = new OracleParameter("P_JBD_CHG_WARR_STDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobDet.Jbd_chg_warr_stdt;
            (param[20] = new OracleParameter("P_JBD_CHG_WARR_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_chg_warr_rmk;
            (param[21] = new OracleParameter("P_JBD_ISINSURANCE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_isinsurance;
            (param[22] = new OracleParameter("P_JBD_CATE1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_cate1;
            (param[23] = new OracleParameter("P_JBD_SER_TERM", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_ser_term;
            (param[24] = new OracleParameter("P_JBD_LASTWARR_STDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobDet.Jbd_lastwarr_stdt;
            (param[25] = new OracleParameter("P_JBD_ISSUED", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_issued;
            (param[26] = new OracleParameter("P_JBD_MAINITMCD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_mainitmcd;
            (param[27] = new OracleParameter("P_JBD_MAINITMSER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_mainitmser;
            (param[28] = new OracleParameter("P_JBD_MAINITMWARR", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_mainitmwarr;
            (param[29] = new OracleParameter("P_JBD_ITMMFC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_itmmfc;
            (param[30] = new OracleParameter("P_JBD_MAINITMMFC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_mainitmmfc;
            (param[31] = new OracleParameter("P_JBD_AVAILABILTY", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_availabilty;
            (param[32] = new OracleParameter("P_JBD_USEJOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_usejob;
            (param[33] = new OracleParameter("P_JBD_MSNNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_msnno;
            (param[34] = new OracleParameter("P_JBD_ITMTP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_itmtp;
            (param[35] = new OracleParameter("P_JBD_SERLOCCHR", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_serlocchr;
            (param[36] = new OracleParameter("P_JBD_CUSTNOTES", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_custnotes;
            (param[37] = new OracleParameter("P_JBD_MAINREQNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_mainreqno;
            (param[38] = new OracleParameter("P_JBD_MAINREQLOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_mainreqloc;
            (param[39] = new OracleParameter("P_JBD_MAINJOBNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_mainjobno;
            (param[40] = new OracleParameter("P_JBD_REQITMTP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_reqitmtp;
            (param[41] = new OracleParameter("P_JBD_REQNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_reqno;
            (param[42] = new OracleParameter("P_JBD_REQLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_reqline;
            (param[43] = new OracleParameter("P_JBD_ISSTOCKUPDATE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_isstockupdate;
            (param[44] = new OracleParameter("P_JBD_ISGATEPASS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_isgatepass;
            (param[45] = new OracleParameter("P_JBD_ISWRN", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_iswrn;
            (param[46] = new OracleParameter("P_JBD_WARRPERIOD", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_warrperiod;
            (param[47] = new OracleParameter("P_JBD_WARRRMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_warrrmk;
            (param[48] = new OracleParameter("P_JBD_WARRSTARTDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobDet.Jbd_warrstartdt;
            (param[49] = new OracleParameter("P_JBD_WARRREPLACE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_warrreplace;
            (param[50] = new OracleParameter("P_JBD_DATE_PUR", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobDet.Jbd_date_pur;
            (param[51] = new OracleParameter("P_JBD_INVC_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_invc_no;
            (param[52] = new OracleParameter("P_JBD_WARAAMD_SEQ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_waraamd_seq;
            (param[53] = new OracleParameter("P_JBD_WARAAMD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_waraamd_by;
            (param[54] = new OracleParameter("P_JBD_WARAAMD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobDet.Jbd_waraamd_dt;
            (param[55] = new OracleParameter("P_JBD_INVC_SHOWROOM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_invc_showroom;
            (param[56] = new OracleParameter("P_JBD_AODISSUELOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_aodissueloc;
            (param[57] = new OracleParameter("P_JBD_AODISSUEDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobDet.Jbd_aodissuedt;
            (param[58] = new OracleParameter("P_JBD_AODISSUENO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_aodissueno;
            (param[59] = new OracleParameter("P_JBD_AODRECNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_aodrecno;
            (param[60] = new OracleParameter("P_JBD_TECHST_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobDet.Jbd_techst_dt;
            (param[61] = new OracleParameter("P_JBD_TECHFIN_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobDet.Jbd_techfin_dt;
            (param[62] = new OracleParameter("P_JBD_MSN_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_msn_no;
            (param[63] = new OracleParameter("P_JBD_ISEXTERNALITM", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_isexternalitm;
            (param[64] = new OracleParameter("P_JBD_CONF_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobDet.Jbd_conf_dt;
            (param[65] = new OracleParameter("P_JBD_CONF_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_conf_cd;
            (param[66] = new OracleParameter("P_JBD_CONF_DESC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_conf_desc;
            (param[67] = new OracleParameter("P_JBD_CONF_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_conf_rmk;
            (param[68] = new OracleParameter("P_JBD_TRANF_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_tranf_by;
            (param[69] = new OracleParameter("P_JBD_TRANF_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobDet.Jbd_tranf_dt;
            (param[70] = new OracleParameter("P_JBD_DO_INVOICE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_do_invoice;
            (param[71] = new OracleParameter("P_JBD_INSU_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_insu_com;
            (param[72] = new OracleParameter("P_JBD_AGREENO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_agreeno;
            (param[73] = new OracleParameter("P_JBD_ISSRN", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_issrn;
            (param[74] = new OracleParameter("P_JBD_ISAGREEMENT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_isagreement;
            (param[75] = new OracleParameter("P_JBD_CUST_AGREENO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_cust_agreeno;
            (param[76] = new OracleParameter("P_JBD_QUO_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_quo_no;
            (param[77] = new OracleParameter("P_JBD_STAGE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_stage;
            (param[78] = new OracleParameter("P_JBD_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_com;
            (param[79] = new OracleParameter("P_JBD_SER_ID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_ser_id;
            (param[80] = new OracleParameter("P_JBD_TECHST_DT_MAN", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobDet.Jbd_techst_dt_man;
            (param[81] = new OracleParameter("P_JBD_TECHFIN_DT_MAN", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobDet.Jbd_techfin_dt_man;
            (param[82] = new OracleParameter("P_JBD_REQWCN", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_reqwcn;
            (param[83] = new OracleParameter("P_JBD_REQWCNDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobDet.Jbd_reqwcndt;
            (param[84] = new OracleParameter("P_JBD_REQWCNSYSDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobDet.Jbd_reqwcnsysdt;
            (param[85] = new OracleParameter("P_JBD_SENTWCN", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_sentwcn;
            (param[86] = new OracleParameter("P_JBD_RECWCN", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_recwcn;
            (param[87] = new OracleParameter("P_JBD_TAKEWCN", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_takewcn;
            (param[88] = new OracleParameter("P_JBD_TAKEWCNDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobDet.Jbd_takewcndt;
            (param[89] = new OracleParameter("P_JBD_TAKEWCNSYSDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobDet.Jbd_takewcnsysdt;
            (param[90] = new OracleParameter("P_JBD_SUPP_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_supp_cd;
            (param[91] = new OracleParameter("P_JBD_PART_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_part_cd;
            (param[92] = new OracleParameter("P_JBD_OEM_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_oem_no;
            (param[93] = new OracleParameter("P_JBD_CASE_ID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_case_id;
            (param[94] = new OracleParameter("P_JBD_ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_act;
            (param[95] = new OracleParameter("P_JBD_OLDJOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_oldjobline;
            (param[96] = new OracleParameter("P_JBD_TECH_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_tech_rmk;
            (param[97] = new OracleParameter("P_JBD_TECH_CUSTRMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_tech_custrmk;
            (param[98] = new OracleParameter("P_JBD_TECH_CLS_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_tech_cls_tp;
            (param[99] = new OracleParameter("P_JBD_ISFOCAPP", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_isfocapp;
            (param[100] = new OracleParameter("P_JBD_SWARR_STUS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_swarr_stus;
            (param[101] = new OracleParameter("P_JBD_SWARRPERIOD", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_swarrperiod;
            (param[102] = new OracleParameter("P_JBD_SWARRRMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_swarrrmk;
            (param[103] = new OracleParameter("P_JBD_SWARRSTARTDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobDet.Jbd_swarrstartdt;
            (param[104] = new OracleParameter("P_JBD_TECH_CLS_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.Jbd_tech_cls_rmk;
            (param[105] = new OracleParameter("P_JBD_SER_REPEAT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobDet.Jbd_ser_repeat;
            (param[106] = new OracleParameter("P_JBD_REJECT_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobDet.JBD_REJECT_BY;
            (param[107] = new OracleParameter("P_JBD_REJECT_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _jobDet.JBD_REJECT_DT;
            (param[108] = new OracleParameter("P_JBD_REQWCN_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "";
            param[109] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            param[109] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            param[110] = new OracleParameter("O_NEWLINE", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDATE_INSPECT_SERIAL", CommandType.StoredProcedure, param);

            if (effects > 0)
            {
                Newline = Convert.ToInt32(param[110].Value.ToString());
            }

            return effects;
        }

        //Tharaka 2015-08-20
        public DataTable GET_ALLALOCATEDJOBS_BY_USER(String Com, String Loc, String User)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[1] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Loc;
            (param[2] = new OracleParameter("P_USER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = User;
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblData = QueryDataTable("tbData", "SP_GET_ALLALOCATEDJOBS_BY_USER", CommandType.StoredProcedure, false, param);
            return _tblData;
        }

        //Tharaka 2015-08-22
        public Int32 SAVE_SCVMAILS_RPTDB(SCV_MSGPORTAL_MAILS oItems)
        {
            Int32 effects = 0;
            OracleParameter[] param = new OracleParameter[8];
            (param[0] = new OracleParameter("P_JOBNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItems.JOBNO;
            (param[1] = new OracleParameter("P_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItems.JOBLINE;
            (param[2] = new OracleParameter("P_SUBJECT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItems.SUBJECT;
            (param[3] = new OracleParameter("P_MAILBODY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItems.MAILBODY;
            (param[4] = new OracleParameter("P_EMAILADDRESS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItems.EMAILADDRESS;
            (param[5] = new OracleParameter("P_STATUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItems.STATUS;
            (param[6] = new OracleParameter("P_refseq", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItems.REFSEQ;
            param[7] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_SAVE_SCVMAILS_RPTDB", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2015-08-22
        public List<SCV_MSGPORTAL_MAILS> GET_SCV_MAILS()
        {
            List<SCV_MSGPORTAL_MAILS> result = new List<SCV_MSGPORTAL_MAILS>();
            OracleParameter[] param = new OracleParameter[1];
            param[0] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblData = QueryDataTable("tbData", "SP_GET_SCV_MAILS", CommandType.StoredProcedure, false, param);
            if (_tblData.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<SCV_MSGPORTAL_MAILS>(_tblData, SCV_MSGPORTAL_MAILS.Converter);
            }
            return result;
        }

        //Tharaka 2015-08-22
        public Int32 UPDATE_MAILSTATUS(Int32 MailStatus, Int32 RecStatus, Int32 Seq)
        {
            Int32 effects = 0;
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_MSILSTATUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = MailStatus;
            (param[1] = new OracleParameter("P_RECSTATUS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = RecStatus;
            (param[2] = new OracleParameter("P_SEQ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Seq;
            param[3] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDATE_MAILSTATUS", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2015-08-22
        public Int32 DELECT_SCV_MAIL()
        {
            Int32 effects = 0;
            OracleParameter[] param = new OracleParameter[1];
            param[0] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_DELECT_SCV_MAIL", CommandType.StoredProcedure, param);
            return effects;
        }

        // Tharaka 2015-08-27
        public List<Service_Confirm_detail> GET_CONF_DET_BY_JOB(string _jobNo)
        {
            List<Service_Confirm_detail> result = new List<Service_Confirm_detail>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_job", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobNo;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_CONF_DET_BY_JOB", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Confirm_detail>(_dtResults, Service_Confirm_detail.Converter);
            }
            return result;
        }

        //Tharaka 2015-08-27
        public DataTable GET_SMS_MAIL(String SMS, String mail)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_SMS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = SMS;
            (param[1] = new OracleParameter("P_MAIL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mail;
            param[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblData = QueryDataTable("tbData", "SP_GET_SMS_MAIL", CommandType.StoredProcedure, false, param);
            return _tblData;
        }

        //Tharaka 2015-09-08
        public DataTable get_defect_det(String defect)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_defect_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = defect;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblData = QueryDataTable("tbData", "sp_get_defect_det", CommandType.StoredProcedure, false, param);
            return _tblData;
        }

        //Sanejewa 2016-01-08
        public DataTable get_isSmartWarr(String Jobno, Int16 lineno)
        {
            OracleParameter[] param = new OracleParameter[3];
            param[0] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("in_job", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Jobno;
            (param[2] = new OracleParameter("in_line", OracleDbType.Int16, null, ParameterDirection.Input)).Value = lineno;

            DataTable _tblData = QueryDataTable("tbData", "sp_get_is_smartwarr_pb", CommandType.StoredProcedure, false, param);
            return _tblData;
        }

        //Sanejewa 2016-01-08
        public DataTable get_isSmartWarr_Job(Decimal sw_perc, String invno, String serial, String item)
        {
            OracleParameter[] param = new OracleParameter[5];
            param[0] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("in_sw_perc", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = sw_perc;
            (param[2] = new OracleParameter("in_invoice", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = invno;
            (param[3] = new OracleParameter("in_serial", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = serial;
            (param[4] = new OracleParameter("in_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item;

            DataTable _tblData = QueryDataTable("tbData", "sp_get_is_smartwarr", CommandType.StoredProcedure, false, param);
            return _tblData;
        }

        //Sanejewa 2016-04-07
        public DataTable check_IsJobClosed(String _jobno, Int16 _line)
        {
            OracleParameter[] param = new OracleParameter[3];
            param[0] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("in_job", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobno;
            (param[2] = new OracleParameter("in_jobline", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _line;

            DataTable _tblData = QueryDataTable("tbData", "sp_check_is_jobclosed", CommandType.StoredProcedure, false, param);
            return _tblData;
        }

        //Tharaka 2015-11-24
        public List<InventoryHeader> GET_ADO_BY_OTH_DOC(String invoiceNUm, String COm, String Loc)
        {
            List<InventoryHeader> result = new List<InventoryHeader>();
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_INVOICE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = invoiceNUm;
            (param[1] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = COm;
            (param[2] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Loc;
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_ADO_BY_OTH_DOC", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<InventoryHeader>(_dtResults, InventoryHeader.ConvertTotal);
            }
            return result;
        }

        //Tharaka 2016-01-28
        public List<MST_SCV_ADHOC_MAIL> GET_SCV_ADHOC_MAIL()
        {
            List<MST_SCV_ADHOC_MAIL> result = new List<MST_SCV_ADHOC_MAIL>();
            OracleParameter[] param = new OracleParameter[1];
            param[0] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_ADHOC_MAIL", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<MST_SCV_ADHOC_MAIL>(_dtResults, MST_SCV_ADHOC_MAIL.Converter);
            }
            return result;
        }

        //Tharaka 2016-01-28
        public List<Service_Pending_Jobs> GET_ALL_PENDINGJOS_FOR_CHL(string com, string chl)
        {
            List<Service_Pending_Jobs> result = new List<Service_Pending_Jobs>();
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("P_CHNL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = chl;
            param[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_ALL_PENDINGJOS_FOR_CHL", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Service_Pending_Jobs>(_dtResults, Service_Pending_Jobs.ConverterBrandName);
            }
            return result;
        }
        //SUBODANA 2016-04-12
        public List<TBS_IMG_UPLOAD> GetImageDetails(string jobid)
        {
            List<TBS_IMG_UPLOAD> result = new List<TBS_IMG_UPLOAD>();
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("P_ENQID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobid;
            _para[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_GET_ALLIMAGES", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<TBS_IMG_UPLOAD>(_dtResults, TBS_IMG_UPLOAD.Converter);
            }
            return result;
        }
        //subodana 2016-05-13
        public DataTable GetCustomValDeclar(string _doc, string _com)
        {


            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_docno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _doc;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _result = QueryDataTable("tbl", "GetCusValDecl", CommandType.StoredProcedure, false, param);
            return _result;
        }

        //subodana 2016-05-16
        public DataTable GetCustomElements( string _com, string docno, string type)
        {


            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_docno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docno;
            (param[2] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _result = QueryDataTable("tbl", "GetCustomElements", CommandType.StoredProcedure, false, param);
            return _result;
        }

        //subodana 2016-05-17
        public DataTable GetCustomWorkingSheet( string docno, string type)
        {


            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_docno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docno;
            (param[1] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _result = QueryDataTable("tbl", "GETWORKINGSHEET", CommandType.StoredProcedure, false, param);
            return _result;
        }

        //subodana 2016-05-25
        public DataTable GetRouteDetails(string com)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _result = QueryDataTable("tbl", "SP_GET_ROUTEDET", CommandType.StoredProcedure, false, param);
            return _result;
        }

        //subodana 2016-05-27
        public DataTable StockVerification(string com, string docno)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("p_docno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docno;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _result = QueryDataTable("Stock@scmrep", "SP_GET_STOCK_VERIFY", CommandType.StoredProcedure, false, param);
            return _result;
        }
        //subodana 2016-05-27
        public DataTable GetSysSerials(string com, string loc, string itemcode)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = loc;
            (param[2] = new OracleParameter("p_itemcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = itemcode;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _result = QueryDataTable("StockSeri", "SP_GET_SYS_SERI", CommandType.StoredProcedure, false, param);
            return _result;
        }

        //subodana 2016-05-28
        public DataTable GetSystemQTY(string com, string loc, string itemcode)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = loc;
            (param[2] = new OracleParameter("p_itemcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = itemcode;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _result = QueryDataTable("StockSeriQTY", "SP_GET_SYSTEMQTY", CommandType.StoredProcedure, false, param);
            return _result;
        }
        //subodana 2016-05-28
        public DataTable GetScanQTY(string com, string doc)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("p_doc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = doc;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _result = QueryDataTable("StockSeriQTY", "SP_GET_SCAN_QTY", CommandType.StoredProcedure, false, param);
            return _result;
        }

        //subodana 2016-06-17
        public DataTable GetGoodsDeclarationDetails(string entryno)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_entryno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = entryno;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _result = QueryDataTable("tbl", "SP_GET_GOODSDECLARATION", CommandType.StoredProcedure, false, param);
            return _result;
        }
        //subodana 2016-06-18
        public DataTable GetCusdecHDRData(string entryno, string com)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_docno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = entryno;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _result = QueryDataTable("tbl", "GetCusDecHDRDAta", CommandType.StoredProcedure, false, param);
            return _result;
        }
        //subodana 2016-06-24
        public DataTable GetCusdecElementnew(string entryno)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_docno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = entryno;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _result = QueryDataTable("tbl", "GetCusElenew", CommandType.StoredProcedure, false, param);
            return _result;
        }

        //subodana 2016-06-24
        public DataTable GetCusdecCommonNew(string country)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_CNTY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = country;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _result = QueryDataTable("tbl", "SP_GET_cusdec_common", CommandType.StoredProcedure, false, param);
            return _result;
        }

        //subodana 2016-07-12
        public List<Cusdec_Goods_decl> GetGoodsDeclarationDetailsList(string entryno)
        {
            List<Cusdec_Goods_decl> result = new List<Cusdec_Goods_decl>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_entryno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = entryno;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _result = QueryDataTable("tbl", "SP_GET_GOODSDECLARATION", CommandType.StoredProcedure, false, param);
            if (_result.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Cusdec_Goods_decl>(_result, Cusdec_Goods_decl.Converter);
            }
            return result;
        }

        //subodana 2016-07-21
        public List<SunAccountBusEntity> GetSunAccountDetails(string accno, string com)
        {
            List<SunAccountBusEntity> result = new List<SunAccountBusEntity>();
            DataTable _result = new DataTable();
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_accno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = accno;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            if (com=="AEC")
            {
                _result = QueryDataTable("tbl", "SP_GET_SUNACCDETAILSELEC", CommandType.StoredProcedure, false, param);

            }else
            {
             _result = QueryDataTable("tbl", "SP_GET_SUNACCDETAILS", CommandType.StoredProcedure, false, param);
            }
            
            if (_result.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<SunAccountBusEntity>(_result, SunAccountBusEntity.Converter);
            }
            return result;
        }
        //subodana 2016-08-15
        public DataTable GetCusdecAssessmentData(string EntryNo)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_entryno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = EntryNo;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _result = QueryDataTable("tbl", "SP_GET_ASTDET", CommandType.StoredProcedure, false, param);
            return _result;
        }

        //subodana 2016-08-15
        public DataTable GetCusdecAssessmentAccountData(string EntryNo, string com)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_entryno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = EntryNo;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _result = QueryDataTable("tbl", "SP_GET_ASTACCDET", CommandType.StoredProcedure, false, param);
            return _result;
        }

        //subodana 2016-07-21
        public List<SunAccountBusEntity> GetSunAccountDetailsforSO(string com, string cus)
        {
            List<SunAccountBusEntity> result = new List<SunAccountBusEntity>();
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("p_cus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cus;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _result = QueryDataTable("tbl", "sp_CHECKACCOUNT", CommandType.StoredProcedure, false, param);
            if (_result.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<SunAccountBusEntity>(_result, SunAccountBusEntity.Converterso);
            }
            return result;
        }
        public DataTable GetMaxSRnum(string code)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_CODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
            param[1] = new OracleParameter("O_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _result = QueryDataTable("tbl", "GetSRMaxNum", CommandType.StoredProcedure, false, param);
            return _result;
        }

        //Akila 2017/04/01
        public DataTable GetServiceJobHistoryBySerial(string _serail, string _item, int _isWarrantyReplaced)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_Serial", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serail;
            (param[1] = new OracleParameter("p_Item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[2] = new OracleParameter("p_IsWarrantyReplaced", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _isWarrantyReplaced;
            param[3] = new OracleParameter("o_Result", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _result = QueryDataTable("Service_Job_History", "SP_GET_SCVJOBHISTORY_BYSERIAL", CommandType.StoredProcedure, false, param);
            return _result;
        }
       //Tharanga 2017/05/06
        public Int32 SaveserviceAreas(ServiceAreaS _ServiceAreaS)
        {
            OracleParameter[] param = new OracleParameter[7];

            (param[0] = new OracleParameter("p_ssa_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ServiceAreaS.SSA_COM;
            (param[1] = new OracleParameter("p_ssa_ser_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ServiceAreaS.SSA_SER_LOC;
            (param[2] = new OracleParameter("p_ssa_town_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ServiceAreaS.SSA_TOWN_CD;
            (param[3] = new OracleParameter("p_ssa_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _ServiceAreaS.SSA_ACT;
            (param[4] = new OracleParameter("p_ssa_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ServiceAreaS.SSA_CRE_BY;
            (param[5] = new OracleParameter("p_ssa_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ServiceAreaS.SSA_MOD_BY;
           
            param[6] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            Int32 effects;
            effects = (Int16)UpdateRecords("SP_SAVE_Service_Areas", CommandType.StoredProcedure, param);
            return effects;
        }

        //Akila 2017/05/06 Save service Allocated Supervicers
        public Int32 SaveServiceAlocatedSupervicer(ServiceTechAlocSupervice _supervicer)
        {
            OracleParameter[] param = new OracleParameter[9];
            Int32 effects = 0;
            (param[0] = new OracleParameter("p_stas_com_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _supervicer.Stas_Com_Cd;
            (param[1] = new OracleParameter("p_stas_loc_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _supervicer.Stas_Loc_Cd;
            (param[2] = new OracleParameter("p_stas_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _supervicer.Stas_Job_No;
            (param[3] = new OracleParameter("p_stas_emp_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _supervicer.Stas_Emp_Cd;
            (param[4] = new OracleParameter("p_stas_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _supervicer.Stas_Status;
            (param[5] = new OracleParameter("p_stas_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _supervicer.Stas_Cre_By;
            (param[6] = new OracleParameter("p_stas_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _supervicer.Stas_Mod_By;
            (param[7] = new OracleParameter("p_stas_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _supervicer.Stas_Session_Id;
            param[8] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_SAVE_STASUPVICE", CommandType.StoredProcedure, param);
            return effects;
        }

        //Akila 2017/05/08 update registration number in tempwarraupload
        public Int32 UpdateTmpWarraUploadRegNo(string _warrantyNo, string _serialNo, string _regNo, string _userId)
        {
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_WarrantyNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warrantyNo;
            (param[1] = new OracleParameter("p_SerialNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serialNo;
            (param[2] = new OracleParameter("p_RegNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _regNo;
            (param[3] = new OracleParameter("p_UserId", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userId;

            param[4] = new OracleParameter("o_Effect", OracleDbType.Int32, null, ParameterDirection.Output);

            Int32 effects = 0;
            effects = (Int32)UpdateRecords("SP_UPDATE_TMPWARAREGNO", CommandType.StoredProcedure, param);
            return effects;
        }
        //Add by lakshan  23 May 2017 copy from GetJObsFOrWIP
       
        public List<TmpServiceWorkingProcess> GetJObsFOrWIPWeb(string com, DateTime From, DateTime To, string jobno, string Stage, Int32 isCusexpectDate, string customer, string PC, String userID)
        {
            List<TmpServiceWorkingProcess> _list = new List<TmpServiceWorkingProcess>();
            OracleParameter[] _para = new OracleParameter[10];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (_para[1] = new OracleParameter("P_From", OracleDbType.Date, null, ParameterDirection.Input)).Value = From;
            (_para[2] = new OracleParameter("P_TO", OracleDbType.Date, null, ParameterDirection.Input)).Value = To;
            (_para[3] = new OracleParameter("p_jonno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobno;
            (_para[4] = new OracleParameter("P_Stage", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Stage;
            (_para[5] = new OracleParameter("p_iscusexpectdate", OracleDbType.Int32, null, ParameterDirection.Input)).Value = isCusexpectDate;
            (_para[6] = new OracleParameter("P_Customer", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = customer;
            (_para[7] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PC;
            (_para[8] = new OracleParameter("P_USER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userID;
            _para[9] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblJobs", "SP_GET_SCV_WIP_JOB", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<TmpServiceWorkingProcess>(_dtResults, TmpServiceWorkingProcess.Converter);
            }
            return _list;
        }
      //Tharanga 2017/06/06
        public List<_Service_Enquiry_StageLog_stage> GET_STAGELOG_ENQRY_Stage(String jobNo, Int32 lineNo)
        {
            List<_Service_Enquiry_StageLog_stage> result = new List<_Service_Enquiry_StageLog_stage>();
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobNo;
            (_para[1] = new OracleParameter("P_JOBLINE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lineNo;
            _para[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_STAGELOG_ENQRY_STAGE", CommandType.StoredProcedure, false, _para);

            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<_Service_Enquiry_StageLog_stage>(_dtResults, _Service_Enquiry_StageLog_stage.Converter);
            }
            return result;
        }
   //Tharanga 2017/06/06
        public DataTable GetJobStage_des(string _P_com, string _P_chnl, decimal _P_stage)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _P_com;
            (param[1] = new OracleParameter("P_chnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _P_chnl;
            (param[2] = new OracleParameter("P_stage", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _P_stage;
         
            param[3] = new OracleParameter("o_Result", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _result = QueryDataTable("Service_Job_Des", "SP_GET_JOB_STAGE_DES", CommandType.StoredProcedure, false, param);
            return _result;
        }

        //By akila 2017/06/17 get po details
        public DataTable GetPoItemDetails(string _jobNo, Int32 _lineNo, string _itemCode, string _poNo)
        {
            DataTable _poItemDetails = new DataTable();

            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_JobNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobNo;
            (param[1] = new OracleParameter("p_LineNo", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _lineNo;
            (param[2] = new OracleParameter("p_Item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemCode;
            (param[3] = new OracleParameter("p_PoNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _poNo;

            param[4] = new OracleParameter("o_Result", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _poItemDetails = QueryDataTable("Po_Item_Details", "SP_GET_POITMDETAILS", CommandType.StoredProcedure, false, param);
            return _poItemDetails;
        }

        //By akila 2017/06/20
        public DataTable GetJobDetailsWithStage(string _company, string _location, string _jobNo)
        {
            DataTable _jobDetails = new DataTable();

            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_ComCode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_LocCode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _location;
            (param[2] = new OracleParameter("p_JobNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobNo;

            param[3] = new OracleParameter("o_Result", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _jobDetails = QueryDataTable("Job_Item_Details", "SP_GET_JOBDETAILS_WITHSTAGE", CommandType.StoredProcedure, false, param);
            return _jobDetails;
        }

        //Tharanga 2017/06/26
        public DataTable GetInvoice_Summary(string _company, string _location, DateTime _fromdate,DateTime _todate,string _pc)
        {
            DataTable _Invoice_Summary = new DataTable();

            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_ComCode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_LocCode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _location;
            (param[2] = new OracleParameter("p_frmDate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fromdate;
            (param[3] = new OracleParameter("p_toDate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _todate;
            (param[4] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            param[5] = new OracleParameter("o_Result", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _Invoice_Summary = QueryDataTable("Job_Item_Details", "sp_Invoice_Summary", CommandType.StoredProcedure, false, param);
            return _Invoice_Summary;
        }

        //Akila 2017/07/03
        public DataTable GetConfirmedServicePoDetails(string _jobNo, string _poNo)
        {
            DataTable _tmpDataTable = new DataTable();

            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_jobNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobNo;
            (param[1] = new OracleParameter("p_PoNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _poNo;
            param[2] = new OracleParameter("o_Result", OracleDbType.RefCursor, null, ParameterDirection.Output);

            _tmpDataTable = QueryDataTable("ConfirmedServicePo", "SP_GET_CONFIR_SRCEPO_DET", CommandType.StoredProcedure, false, param);

            return _tmpDataTable;
        }
        /// <summary>
        /// get job stage from JOB,SERIAL,NIC OR MOB by Dulanga
        /// </summary>
        /// <param name="p_code"></param>
        /// <param name="p_type"></param>
        /// <returns></returns>
        public DataTable GetMobJobStage(string p_code, string p_type)
        {   
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_code;
            (param[1] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_type;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblRcc", "SP_MOB_SERVICE_STAGE", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }
        public DataTable sp_get_scv_job_amount(string p_jobno,int p_jobline)//Tharanga 2017/07/26
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_jobno;
            (param[1] = new OracleParameter("p_jobline", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_jobline;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblRcc", "sp_get_scv_job_amount", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        //Akila 2017/07/14
        public DataTable GetAppovelPendingJobs(string _comCode, string _location, DateTime _fromDate, DateTime _toDate)
        {
            DataTable _tmpDataTable = new DataTable();

            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_ComCode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _comCode;
            (param[1] = new OracleParameter("p_locCode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _location;
            (param[2] = new OracleParameter("p_fromDate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fromDate.Date;
            (param[3] = new OracleParameter("p_toDate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _toDate.Date;
            param[4] = new OracleParameter("p_result", OracleDbType.RefCursor, null, ParameterDirection.Output);

            _tmpDataTable = QueryDataTable("PendingJobs", "SP_GET_APPROVEPENDINGJOBS", CommandType.StoredProcedure, false, param);

            return _tmpDataTable;
        }

        //Akila 2017/07/15
        public Int32 UpdateJobHeaderStage(string _jobNo, int _stage, string _userId, string _sessionId)
        {
            OracleParameter[] param = new OracleParameter[5];
            Int32 effects = 0;
            (param[0] = new OracleParameter("p_JobNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobNo;
            (param[1] = new OracleParameter("p_JobStage", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _stage;
            (param[2] = new OracleParameter("p_ModBy", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userId;
            (param[3] = new OracleParameter("p_SessionId", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _sessionId;
            param[4] = new OracleParameter("o_Effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDATE_JOBHDR_STAGE", CommandType.StoredProcedure, param);
            return effects;
        }



        public DataTable JobDetails_model_brand(DateTime _from, DateTime _to, string _com, string _loc, string in_Itemcode, string in_Brand, string in_Model, string in_Itemcat1,
            string in_Itemcat2, string in_Itemcat3, string _technician, string _jobcat, string _itemtp, decimal _jobstatus, string _warrstatus, string _jobno, string _user)
        {   //Sanjeewa 2015-09-09
            DataTable _dtResults = null;
            OracleParameter[] param = new OracleParameter[18];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _from;
            (param[2] = new OracleParameter("tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _to;
            (param[3] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[4] = new OracleParameter("in_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            (param[5] = new OracleParameter("in_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcode;
            (param[6] = new OracleParameter("in_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Brand;
            (param[7] = new OracleParameter("in_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Model;
            (param[8] = new OracleParameter("in_cat1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcat1;
            (param[9] = new OracleParameter("in_cat2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcat2;
            (param[10] = new OracleParameter("in_cat3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = in_Itemcat3;
            (param[11] = new OracleParameter("in_technician", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _technician;
            (param[12] = new OracleParameter("in_job_cat", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobcat;
            (param[13] = new OracleParameter("in_itm_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemtp;
            (param[14] = new OracleParameter("in_job_status", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _jobstatus;
            (param[15] = new OracleParameter("in_warr_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warrstatus;
            (param[16] = new OracleParameter("in_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobno;
            (param[17] = new OracleParameter("in_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;

            _dtResults = QueryDataTable("tbl", "sp_get_job_detail_model_brand", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        
        /// <summary>
        /// get service location town by Dist Dulanga
        /// </summary>
        /// <param name="p_code"></param>
        /// <returns></returns>
        public DataTable GetServiceTownByDist(string p_code)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_code;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblRcc", "SP_SERVICE_TOWN_BY_DIST", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        /// <summary>
        /// get Job by Key Dulanga
        /// </summary>
        /// <param name="p_key"></param>
        /// <returns></returns>

        public DataTable GetJobByKey(string p_key)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_key", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_key;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblRcc", "SP_MOB_JOB_BY_KEY", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }


        public DataTable GetJobByKeyNew(string p_key,string p_com,string p_pc,string p_loc)
        {
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_key", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_key;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_com;
            (param[2] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_loc;
            (param[3] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_pc;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblRcc", "SP_MOB_JOB_BY_KEY_NEW", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        public DataTable GetJobBySerial(string p_serial,string p_com,string p_pc,string p_loc)
        {
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_serial", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_serial;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_com;
            (param[2] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_pc;
            (param[3] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_loc;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblRcc", "SP_MOB_JOB_BY_SERIAL", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }





        /// <summary>
        /// tec Alocation job by Dulanga
        /// </summary>
        /// <param name="p_com"></param>
        /// <param name="p_loc"></param>
        /// <param name="p_pc"></param>
        /// <param name="p_sdate"></param>
        /// <param name="p_edate"></param>
        /// <param name="p_sstage"></param>
        /// <param name="p_estage"></param>
        /// <param name="p_user"></param>
        /// <returns></returns>
        public DataTable GetTecAllocateJob(string p_com, string p_loc, string p_pc, DateTime p_sdate, DateTime p_edate,
            string p_sstage, string p_estage, string p_user)
        {
            OracleParameter[] param = new OracleParameter[9];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_com;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_loc;
            (param[2] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_pc;
            (param[3] = new OracleParameter("p_sdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = p_sdate;
            (param[4] = new OracleParameter("p_edate", OracleDbType.Date, null, ParameterDirection.Input)).Value = p_edate;
            (param[5] = new OracleParameter("p_sstage", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_sstage;
            (param[6] = new OracleParameter("p_estage", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_estage;
            (param[7] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_user;
            param[8] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblRcc", "SP_MOB_TEC_ALLOCATE_JOBS", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }


        /// <summary>
        /// get comment type by dulanga
        /// </summary>
        /// <param name="com"></param>
        /// <param name="chnl"></param>
        /// <returns></returns>
        public DataTable getCommentType(string com,string chnl){

            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("P_CHNL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = chnl;
             param[2] = new OracleParameter("C_DATA ", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblRcc", "SP_GET_MOB_COM_TYPE", CommandType.StoredProcedure, false, param);

            return _dtResults;

        }

        /// <summary>
        /// get serial detail by dulanga
        /// </summary>
        /// <param name="serial"></param>
        /// <returns></returns>
        public DataTable getMobSerialDetail(string serial)
        {

            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_serial", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = serial;
             param[1] = new OracleParameter("C_DATA ", OracleDbType.RefCursor, null, ParameterDirection.Output);
             DataTable _dtResults = QueryDataTable("tblRcc", "sp_mob_get_serial_details", CommandType.StoredProcedure, false, param);

            return _dtResults;

        }



        /// <summary>
        /// get Mob MRN locations By Dulanga
        /// </summary>
        /// <param name="com"></param>
        /// <param name="loc"></param>
        /// <returns></returns>
        public DataTable getMobMRNLocations(string com, string loc,string type)
        {

            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = loc;
            (param[2] = new OracleParameter("P_TYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
            param[3] = new OracleParameter("C_DATA ", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblRcc", "SP_GET_SCV_MOB_MRN_LOC ", CommandType.StoredProcedure, false, param);

            return _dtResults;

        }

        /// <summary>
        /// get Email setup 
        /// </summary>
        /// <param name="com"></param>
        /// <param name="pc"></param>
        /// <param name="module"></param>
        /// <returns></returns>
        public DataTable getMobEmailSetup(string com, string pc, string module)
        {

            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
            (param[2] = new OracleParameter("p_module", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = module;
            param[3] = new OracleParameter("C_DATA ", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblRcc", "SP_MOB_ALERTCRITERIA", CommandType.StoredProcedure, false, param);

            return _dtResults;

        }




        /// <summary>
        /// get defect type Dulanga
        /// </summary>
        /// <param name="com"></param>
        /// <param name="chnl"></param>
        /// <param name="cate"></param>
        /// <returns></returns>
        public DataTable getMobDefectType(string com, string chnl,string cate)
        {

            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("p_chnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = chnl;
            (param[2] = new OracleParameter("p_cat", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cate;
             param[3] = new OracleParameter("C_DATA ", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblRcc", "sp_mob_get_def_types", CommandType.StoredProcedure, false, param);

            return _dtResults;

        }
        //ADD BY THARANGA 2017/09/14
        public DataTable get_msg_info_MAIL(string p_com, string p_pc, string p_doc_tp)
        {  

            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_com;
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_pc;
            (param[2] = new OracleParameter("p_doc_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_doc_tp;
            param[3] = new OracleParameter("C_DATA ", OracleDbType.RefCursor, null, ParameterDirection.Output);
           

            DataTable _dtResults = QueryDataTable("tblRcc", "sp_get_email", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        //akila 2017/11/15
        public DataTable GetWarrantyReplaceSerialDetails(string _serialNo, string _itemCode)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_serial", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serialNo;
            (param[1] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemCode;
            param[2] = new OracleParameter("o_Result ", OracleDbType.RefCursor, null, ParameterDirection.Output);


            DataTable _dtResults = QueryDataTable("Wara_Rep_Ser_Det", "SP_GET_WARA_REP_SER_DET", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

       // Tharindu 2017-12-14

        public DataTable GetRccLetterDetails(string _rccNo)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_rcc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _rccNo;
            param[1] = new OracleParameter("c_data ", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("dt", "sp_get_rcc_letter", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable GetErrJobList()
        {
            OracleParameter[] param = new OracleParameter[1];            
            param[0] = new OracleParameter("c_data ", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("dt", "sp_get_gatepass_err_corr", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public Int32 UpdatesatitmDOqty_GP(string _Jobno)
        {
            Int32 _effect = 0;

            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("in_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Jobno;            
            param[1] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            _effect = (Int32)UpdateRecords("sp_update_gp_sat_itm_do", CommandType.StoredProcedure, param);
            return _effect;
        }

        public bool check_cust_qty_inv_balance(string p_job_no, string p_com, string p_loc, string p_pc)
        {
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_job_no;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_com;
            (param[2] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_loc;
            (param[3] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_pc;
            param[4] = new OracleParameter("O_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults;
            _dtResults = QueryDataTable("tbl", "sp_check_sat_qty_inv_balance", CommandType.StoredProcedure, false, param);
            bool ser = false;
            decimal sad_qty = 0;
            decimal inb_qty = 0;

            if (_dtResults != null && _dtResults.Rows.Count > 0)
            {
                sad_qty = Convert.ToDecimal(_dtResults.Rows[0][0].ToString());
                inb_qty = Convert.ToDecimal(_dtResults.Rows[0][1].ToString());
                if (sad_qty == inb_qty)
                {
                    ser = true;
                }
            }
            return ser;
        }

        //Akila 2018/02/21
        public DataTable GetPendingClaimDocs(string _docNo)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _docNo;
            param[1] = new OracleParameter("o_pendingDocs ", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("PendingDocs", "SP_GET_PENDING_CLAIM_DOC", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
    
        //subodana 2018-03-05
        public DataTable GetMailBrands(string _docNo)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _docNo;
            param[1] = new OracleParameter("o_pendingDocs ", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("PendingDocs", "SP_GET_EMAILS_BRANDS", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        //subodana 2018-03-05
        public DataTable GetBrandsMail(string _Brand, string _cat, string com)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Brand;
            (param[1] = new OracleParameter("p_cat", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat;
            (param[2] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            param[3] = new OracleParameter("o_pendingDocs ", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("PendingDocs", "SP_GET_BRANDS_EMAILS", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
      //  subodana 2018-03-05 eDITED bY DULAJ ADD COMPANY 2018/Oct/31 
        public DataTable GetGenuineEMails(string _code,string com)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _code;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            param[2] = new OracleParameter("o_pendingDocs ", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("PendingDocs", "SP_GET_PROPTBLTY_EMAILS", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
      
         //Tharindu 2018-03-30
        public DataTable GetJobHeader(string jobNo, string com)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("p_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobNo;
            param[2] = new OracleParameter("c_data ", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("dt", "sp_get_job_header_only", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        //Tharindu 2018-03-30
        public DataTable GetJobDetail(string jobNo, string com)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("p_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobNo;
            param[2] = new OracleParameter("c_data ", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("dt", "sp_get_job_detail_only", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        public DataTable get_REF_OLD_PART_CAT(string _cate_1, string _cate_2, string _cate_3)
        {
            DataTable _dtResults = null;
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_cate_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cate_1;
            (param[1] = new OracleParameter("P_cate_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cate_2;
            (param[2] = new OracleParameter("P_cate_3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cate_3;

            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            _dtResults = QueryDataTable("tbl", "GET_REF_OLD_PART_CAT", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        //add by tharanga 2017/07/11
        public Int32 SAVE_SCV_JOBCUS_FEED_NEW(string _sjf_jobno, Int32 _sjf_jobline, DateTime _sjf_date, string _sjf_cuscd, string _sjf_feedback, string _sjf_cre_by, Int32 _sjf_jb_type, Int32 sjf_jb_stage, Int32 sjf_infm_all, Int32 sjf_infm_tech, Int32 sjf_is_sms, string _sjf_feedback_tp, Int32 _sjf_seqline)
        {
            OracleParameter[] param = new OracleParameter[14];
            Int32 effects = 0;
            (param[0] = new OracleParameter("p_sjf_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _sjf_jobno;
            (param[1] = new OracleParameter("p_sjf_jobline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _sjf_jobline;
            (param[2] = new OracleParameter("p_sjf_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = _sjf_date;
            (param[3] = new OracleParameter("p_sjf_cuscd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _sjf_cuscd;
            (param[4] = new OracleParameter("p_sjf_feedback", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _sjf_feedback;
            (param[5] = new OracleParameter("p_sjf_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _sjf_cre_by;
            (param[6] = new OracleParameter("p_job_type", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _sjf_jb_type;
            (param[7] = new OracleParameter("p_job_stage", OracleDbType.Int32, null, ParameterDirection.Input)).Value = sjf_jb_stage;

            (param[8] = new OracleParameter("p_infm_all", OracleDbType.Int32, null, ParameterDirection.Input)).Value = sjf_infm_all;
            (param[9] = new OracleParameter("p_infm_tech", OracleDbType.Int32, null, ParameterDirection.Input)).Value = sjf_infm_tech;
            (param[10] = new OracleParameter("p_infm_is_sms", OracleDbType.Int32, null, ParameterDirection.Input)).Value = sjf_is_sms;
          
            (param[11] = new OracleParameter("p_sjf_feedback_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _sjf_feedback_tp;
            (param[12] = new OracleParameter("p_sjf_seqline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _sjf_seqline;
            param[13] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_save_cust_comentNEW", CommandType.StoredProcedure, param);
            return effects;
        }

        public List<scv_jobcus_feed> GetCustJobFeedback_list(string _jobno,Int32 _seq, Int32 _jobline)
        {
           // List<scv_jobcus_feed> _list = null;
            List<scv_jobcus_feed> _list = new List<scv_jobcus_feed>();
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_SEQ", OracleDbType.Int32 , null, ParameterDirection.Input)).Value = _seq;
            (param[1] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobno;
            (param[2] = new OracleParameter("P_JOBLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobline;
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SCV_FEED_BACK", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<scv_jobcus_feed>(_dtResults, scv_jobcus_feed.Converter);
            }

            return _list;
        }
        public Int32 save_busentity_newcom(string _com, string _cust_cd)
        {
            Int32 _effect = 0;

            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_cust_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cust_cd;
            param[2] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            _effect = (Int32)UpdateRecords("sp_save_busentity_newcom", CommandType.StoredProcedure, param);
            return _effect;
        }
        public DataTable GET_JOB_REQ_DET(string _ComCode, string _loc, string _ProfitCenter, string _req, DateTime _frmdate, DateTime _todate, string _town)
        {
            DataTable _dtResults = null;
            OracleParameter[] param = new OracleParameter[8];
            (param[0] = new OracleParameter("p_ComCode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ComCode;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            (param[2] = new OracleParameter("p_ProfitCenter", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ProfitCenter;
            (param[3] = new OracleParameter("p_req", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _req;
            (param[4] = new OracleParameter("p_frmdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _frmdate;
            (param[5] = new OracleParameter("p_todate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _todate;
            (param[6] = new OracleParameter("p_town", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _town;
            param[7] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            _dtResults = QueryDataTable("tbl", "SP_GET_JOB_REQ_DET", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        public DataTable GET_ALLEMP_BY_TEAM_CD(string _COM, string _PC, string _TEAM_CD)
        {
            DataTable _dtResults = null;
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _COM;
            (param[1] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _PC;
            (param[2] = new OracleParameter("P_TEAM_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _TEAM_CD;

            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _dtResults = QueryDataTable("tbl", "SP_GET_ALLEMP_BY_TEAM_CD", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        //tharanga 04/may/2017
        public List<ServiceAreaS> GetServiceCenterDetailslist(string _com_CD, string _svc_CD, string _Town_CD, string _searchText)
        {
            DataTable _dtResults = null;
            List<ServiceAreaS> _ServiceAreaSlist = null;
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_com_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com_CD;
            (param[1] = new OracleParameter("p_svc_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _svc_CD;
            (param[2] = new OracleParameter("p_town_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Town_CD;
            (param[3] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _searchText;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            _dtResults = QueryDataTable("tblusers", " sp_search_service_center", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                _ServiceAreaSlist = DataTableExtensions.ToGenericList<ServiceAreaS>(_dtResults, ServiceAreaS.Converter);
            }
            return _ServiceAreaSlist;


        }


        //Wimal @ 13/07/2018
        public DataTable get_balStockItem(string com, string InvNo)
        {

            DataTable _dtResults = null;
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("P_INV_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = InvNo;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            _dtResults = QueryDataTable("tbl", "SP_GETBAL_STKITM", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        public Int32 update_scv_agr_hdr(SCV_AGR_HDR oHeader)
        {
            OracleParameter[] param = new OracleParameter[11];
            Int32 effects = 0; ;
            (param[0] = new OracleParameter("p_SAG_AGR_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.Sag_agr_no;
            (param[1] = new OracleParameter("p_SAG_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.Sag_com;
            (param[2] = new OracleParameter("p_SAG_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.Sag_pc;
            (param[3] = new OracleParameter("p_SAG_CUSTCD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.Sag_custcd;
            (param[4] = new OracleParameter("p_SAG_CUST_TOWN", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.Sag_cust_town;
            (param[5] = new OracleParameter("p_SAG_TOWN_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.Sag_cust_town;
            (param[6] = new OracleParameter("p_SAG_CONT_PERSON", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.Sag_cont_person;
            (param[7] = new OracleParameter("p_SAG_CONT_ADD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.Sag_cont_no;
            (param[8] = new OracleParameter("p_SAG_CONT_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.Sag_cont_add;
            (param[9] = new OracleParameter("p_sag_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.Sag_mod_by;
            param[10] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int32)UpdateRecords("sp_update_scv_agr_hdr", CommandType.StoredProcedure, param);
            return effects;
        }
        public Int32 update_svc_hdr_cst_exdate(string _jobno, Int32 _jobline, DateTime _date,string _user)
        {
            OracleParameter[] param = new OracleParameter[5];
            Int32 effects = 0; ;
            (param[0] = new OracleParameter("p_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _jobno;
            (param[1] = new OracleParameter("p_jobline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _jobline;
            (param[2] = new OracleParameter("p_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = _date;
            (param[3] = new OracleParameter("p_modby", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
         
            param[4] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int32)UpdateRecords("sp_update_svc_hdr_cst_exdate", CommandType.StoredProcedure, param);
            return effects;
        }

        public Int32 updateRequestHeader(Service_Req_Hdr _serviceReqHdr)
        {
            OracleParameter[] param = new OracleParameter[66];

            (param[0] = new OracleParameter("p_SRB_SEQ_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_seq_no;
            (param[1] = new OracleParameter("p_SRB_REQNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_reqno;
            (param[2] = new OracleParameter("p_SRB_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_dt;
            (param[3] = new OracleParameter("p_SRB_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_com;
            (param[4] = new OracleParameter("p_SRB_JOBCAT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_jobcat;
            (param[5] = new OracleParameter("p_SRB_JOBTP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_jobtp;
            (param[6] = new OracleParameter("p_SRB_JOBSTP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_jobstp;
            (param[7] = new OracleParameter("p_SRB_MANUALREF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_manualref;
            (param[8] = new OracleParameter("p_SRB_OTHERREF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_otherref;
            (param[9] = new OracleParameter("p_SRB_REFNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_refno;
            (param[10] = new OracleParameter("p_SRB_JOBSTAGE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_jobstage;
            (param[11] = new OracleParameter("p_SRB_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_rmk;
            (param[12] = new OracleParameter("p_SRB_PRORITY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_prority;
            (param[13] = new OracleParameter("p_SRB_ST_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_st_dt;
            (param[14] = new OracleParameter("p_SRB_ED_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_ed_dt;
            (param[15] = new OracleParameter("p_SRB_NOOFPRINT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_noofprint;
            (param[16] = new OracleParameter("p_SRB_LASTPRINTBY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_lastprintby;
            (param[17] = new OracleParameter("p_SRB_ORDERNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_orderno;
            (param[18] = new OracleParameter("p_SRB_CUSTEXPTDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_custexptdt;
            (param[19] = new OracleParameter("p_SRB_SUBSTAGE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_substage;
            (param[20] = new OracleParameter("p_SRB_CUST_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_cust_cd;
            (param[21] = new OracleParameter("p_SRB_CUST_TIT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_cust_tit;
            (param[22] = new OracleParameter("p_SRB_CUST_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_cust_name;
            (param[23] = new OracleParameter("p_SRB_NIC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_nic;
            (param[24] = new OracleParameter("p_SRB_DL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_dl;
            (param[25] = new OracleParameter("p_SRB_PP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_pp;
            (param[26] = new OracleParameter("p_SRB_MOBINO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_mobino;
            (param[27] = new OracleParameter("p_SRB_ADD1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_add1;
            (param[28] = new OracleParameter("p_SRB_ADD2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_add2;
            (param[29] = new OracleParameter("p_SRB_ADD3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_add3;
            (param[30] = new OracleParameter("p_SRB_TOWN", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_town;
            (param[31] = new OracleParameter("p_SRB_PHNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_phno;
            (param[32] = new OracleParameter("p_SRB_FAXNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_faxno;
            (param[33] = new OracleParameter("p_SRB_EMAIL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_email;
            (param[34] = new OracleParameter("p_SRB_CNT_PERSON", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_cnt_person;
            (param[35] = new OracleParameter("p_SRB_CNT_ADD1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_cnt_add1;
            (param[36] = new OracleParameter("p_SRB_CNT_ADD2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_cnt_add2;
            (param[37] = new OracleParameter("p_SRB_CNT_PHNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_cnt_phno;
            (param[38] = new OracleParameter("p_SRB_JOB_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_job_rmk;
            (param[39] = new OracleParameter("p_SRB_TECH_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_tech_rmk;
            (param[40] = new OracleParameter("p_SRB_B_CUST_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_b_cust_cd;
            (param[41] = new OracleParameter("p_SRB_B_CUST_TIT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_b_cust_tit;
            (param[42] = new OracleParameter("p_SRB_B_CUST_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_b_cust_name;
            (param[43] = new OracleParameter("p_SRB_B_NIC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_b_nic;
            (param[44] = new OracleParameter("p_SRB_B_DL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_b_dl;
            (param[45] = new OracleParameter("p_SRB_B_PP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_b_pp;
            (param[46] = new OracleParameter("p_SRB_B_MOBINO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_b_mobino;
            (param[47] = new OracleParameter("p_SRB_B_ADD1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_b_add1;
            (param[48] = new OracleParameter("p_SRB_B_ADD2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_b_add2;
            (param[49] = new OracleParameter("p_SRB_B_ADD3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_b_add3;
            (param[50] = new OracleParameter("p_SRB_B_TOWN", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_b_town;
            (param[51] = new OracleParameter("p_SRB_B_PHNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_b_phno;
            (param[52] = new OracleParameter("p_SRB_B_FAX", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_b_fax;
            (param[53] = new OracleParameter("p_SRB_B_EMAIL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_b_email;
            (param[54] = new OracleParameter("p_SRB_INFM_PERSON", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_infm_person;
            (param[55] = new OracleParameter("p_SRB_INFM_ADD1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_infm_add1;
            (param[56] = new OracleParameter("p_SRB_INFM_ADD2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_infm_add2;
            (param[57] = new OracleParameter("p_SRB_INFM_PHNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_infm_phno;
            (param[58] = new OracleParameter("p_SRB_STUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_stus;
            (param[59] = new OracleParameter("p_SRB_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_cre_by;
            (param[60] = new OracleParameter("p_SRB_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_cre_dt;
            (param[61] = new OracleParameter("p_SRB_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_mod_by;
            (param[62] = new OracleParameter("p_SRB_MOD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _serviceReqHdr.Srb_mod_dt;
            (param[63] = new OracleParameter("P_SRB_CNT_EMAIL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.SRB_CNT_EMAIL;
            (param[64] = new OracleParameter("p_SRB_ADDITIONAL_COST", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serviceReqHdr.SRB_ADDITIONAL_COST;

            param[65] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            Int32 effects;
            effects = (Int16)UpdateRecords("SP_UPDATE_REQ_HDR", CommandType.StoredProcedure, param);
            return effects;
        }

        public int updateJobReqDetByRCC(string _rccno, string _aodno)
        {
            int rows_aff = 0;
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_rcc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _rccno;
            (param[1] = new OracleParameter("p_aod", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _aodno;
            param[2] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            rows_aff = UpdateRecords("sp_upd_reqdet_by_rcc", CommandType.StoredProcedure, param);
            return rows_aff;
        }

        public int updateJobReqByRCC(string _rccno, string _aodno)
        {
            int rows_aff = 0;
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_rcc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _rccno;
            (param[1] = new OracleParameter("p_aod", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _aodno;
            param[2] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            rows_aff = UpdateRecords("sp_upd_reqhdr_by_rcc", CommandType.StoredProcedure, param);
            return rows_aff;
        }

        public DataTable GetCustomWorkingSheetHS(string docno, string type)
        {


            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_docno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docno;
            (param[1] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _result = QueryDataTable("tbl", "sp_GetCusValDecl", CommandType.StoredProcedure, false, param);
            return _result;
        }
    }
}



