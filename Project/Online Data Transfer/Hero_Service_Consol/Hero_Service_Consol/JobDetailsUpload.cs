using EMS_Upload_Console;
using FF.BusinessObjects;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UtilityClasses;

namespace Hero_Service_Consol
{
    internal class JobDetailsUpload : Conn
    {
        private OracleDataAdapter _oDa;

        public void GetJobDetails(Int16  _db)
        {
            Console.WriteLine("");
            try
            {
                List<Service_job_Det> oJobDetails = new List<Service_job_Det>();
                List<Service_JOB_HDR> oJobHeaders = new List<Service_JOB_HDR>();

                #region Get Job + Header Details from HCM DB

                _oDa = new OracleDataAdapter();
                DataSet _emsData = new DataSet();
                EmsBegin(_db);
                HMCBegin(_db);

                String _sql = @"SELECT   DISTINCT A.*, C.JBS_DESC
                            FROM   SCV_JOB_DET A, SAT_ITM B, SCV_JOB_STAGE C , SAT_HDR D
                            WHERE       A.JBD_JOBNO = B.SAD_JOB_NO
                                 AND A.JBD_JOBLINE = B.SAD_JOB_LINE
                                 AND A.JBD_STAGE = C.JBS_STAGE
                                 AND D.sah_seq_no = B.sad_seq_no
                                 AND D.sah_anal_10 = 0";

                OracleCommand _oCom = new OracleCommand(_sql, oConnHMC);
                _oCom.Transaction = oTrHMC;
                _oDa.SelectCommand = _oCom;
                _oDa.Fill(_emsData, "SCV_JOB_DET");

                if (_emsData.Tables[0].Rows.Count > 0)
                {
                    oJobDetails = DataTableExtensionsTolist.ToGenericList<Service_job_Det>(_emsData.Tables[0], Service_job_Det.Converter);
                }

                Console.WriteLine("Job Detail Records Count :- " + oJobDetails.Count.ToString());

                List<String> _JonNumList = oJobDetails.Distinct().Select(x => x.Jbd_jobno).ToList();

                foreach (String _JobNumber in _JonNumList)
                {
                    _oDa = new OracleDataAdapter();
                    DataSet dsJobHeaders = new DataSet();
                    String _sql2 = @"SELECT * FROM scv_job_hdr WHERE sjb_jobno = :P_JobNumer";
                    _oCom = new OracleCommand(_sql2, oConnHMC);
                    _oCom.Parameters.Add(":p_loc", OracleDbType.NVarchar2).Value = _JobNumber;
                    _oCom.Transaction = oTrHMC;
                    _oDa.SelectCommand = _oCom;
                    _oDa.Fill(dsJobHeaders, "SCV_JOB_HDR");

                    Service_JOB_HDR oHeader = new Service_JOB_HDR();

                    if (dsJobHeaders.Tables[0].Rows.Count > 0)
                    {
                        oHeader = DataTableExtensionsTolist.ToGenericList<Service_JOB_HDR>(dsJobHeaders.Tables[0], Service_JOB_HDR.Converter)[0];
                        oJobHeaders.Add(oHeader);
                    }
                }

                Console.WriteLine("Job Header Records Count :- " + oJobHeaders.Count.ToString());

                #endregion Get Job + Header Details from HCM DB

                #region Save Job Header + Details

                foreach (Service_JOB_HDR oHeader in oJobHeaders)
                {
                    OracleParameter[] param = new OracleParameter[1];
                    Int32 effects = 0;

                    param[0] = new OracleParameter("o_serialid", OracleDbType.Int32, null, ParameterDirection.Output);
                    effects = (Int32)UpdateRecords(oConnEMS, "sp_getjobserialid", CommandType.StoredProcedure, param);

                    oHeader.SJB_SEQ_NO = effects;
                    List<Service_job_Det> oSelectedItem = oJobDetails.FindAll(x => x.Jbd_jobno == oHeader.SJB_JOBNO);
                    oSelectedItem.ForEach(x => x.Jbd_seq_no = effects);

                    OracleParameter[] param1 = new OracleParameter[66];

                    #region Set Para

                    (param1[0] = new OracleParameter("p_SJB_SEQ_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oHeader.SJB_SEQ_NO;
                    (param1[1] = new OracleParameter("p_SJB_JOBNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "HMC-" + oHeader.SJB_JOBNO;
                    (param1[2] = new OracleParameter("p_SJB_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = oHeader.SJB_DT;
                    (param1[3] = new OracleParameter("p_SJB_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_COM;
                    (param1[4] = new OracleParameter("p_SJB_JOBCAT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_JOBCAT;
                    (param1[5] = new OracleParameter("p_SJB_JOBTP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_JOBTP;
                    (param1[6] = new OracleParameter("p_SJB_JOBSTP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_JOBSTP;
                    (param1[7] = new OracleParameter("p_SJB_MANUALREF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_MANUALREF;
                    (param1[8] = new OracleParameter("p_SJB_OTHERREF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_OTHERREF;
                    (param1[9] = new OracleParameter("p_SJB_REQNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_REQNO;
                    (param1[10] = new OracleParameter("p_SJB_JOBSTAGE", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = oHeader.SJB_JOBSTAGE;
                    (param1[11] = new OracleParameter("p_SJB_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_RMK;
                    (param1[12] = new OracleParameter("p_SJB_PRORITY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_PRORITY;
                    (param1[13] = new OracleParameter("p_SJB_ST_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = oHeader.SJB_ST_DT;
                    (param1[14] = new OracleParameter("p_SJB_ED_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = oHeader.SJB_ED_DT;
                    (param1[15] = new OracleParameter("p_SJB_NOOFPRINT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oHeader.SJB_NOOFPRINT;
                    (param1[16] = new OracleParameter("p_SJB_LASTPRINTBY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_LASTPRINTBY;
                    (param1[17] = new OracleParameter("p_SJB_ORDERNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_ORDERNO;
                    (param1[18] = new OracleParameter("p_SJB_CUSTEXPTDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = oHeader.SJB_CUSTEXPTDT;
                    (param1[19] = new OracleParameter("p_SJB_SUBSTAGE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_SUBSTAGE;
                    (param1[20] = new OracleParameter("p_SJB_CUST_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_CUST_CD;
                    (param1[21] = new OracleParameter("p_SJB_CUST_TIT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_CUST_TIT;
                    (param1[22] = new OracleParameter("p_SJB_CUST_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_CUST_NAME;
                    (param1[23] = new OracleParameter("p_SJB_NIC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_NIC;
                    (param1[24] = new OracleParameter("p_SJB_DL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_DL;
                    (param1[25] = new OracleParameter("p_SJB_PP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_PP;
                    (param1[26] = new OracleParameter("p_SJB_MOBINO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_MOBINO;
                    (param1[27] = new OracleParameter("p_SJB_ADD1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_ADD1;
                    (param1[28] = new OracleParameter("p_SJB_ADD2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_ADD2;
                    (param1[29] = new OracleParameter("p_SJB_ADD3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_ADD3;
                    (param1[30] = new OracleParameter("p_SJB_TOWN", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_TOWN;
                    (param1[31] = new OracleParameter("p_SJB_PHNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_PHNO;
                    (param1[32] = new OracleParameter("p_SJB_FAXNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_FAXNO;
                    (param1[33] = new OracleParameter("p_SJB_EMAIL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_EMAIL;
                    (param1[34] = new OracleParameter("p_SJB_CNT_PERSON", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_CNT_PERSON;
                    (param1[35] = new OracleParameter("p_SJB_CNT_ADD1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_CNT_ADD1;
                    (param1[36] = new OracleParameter("p_SJB_CNT_ADD2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_CNT_ADD2;
                    (param1[37] = new OracleParameter("p_SJB_CNT_PHNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_CNT_PHNO;
                    (param1[38] = new OracleParameter("p_SJB_JOB_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_JOB_RMK;
                    (param1[39] = new OracleParameter("p_SJB_TECH_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_TECH_RMK;
                    (param1[40] = new OracleParameter("p_SJB_B_CUST_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_B_CUST_CD;
                    (param1[41] = new OracleParameter("p_SJB_B_CUST_TIT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_B_CUST_TIT;
                    (param1[42] = new OracleParameter("p_SJB_B_CUST_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_B_CUST_NAME;
                    (param1[43] = new OracleParameter("p_SJB_B_NIC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_B_NIC;
                    (param1[44] = new OracleParameter("p_SJB_B_DL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_B_DL;
                    (param1[45] = new OracleParameter("p_SJB_B_PP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_B_PP;
                    (param1[46] = new OracleParameter("p_SJB_B_MOBINO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_B_MOBINO;
                    (param1[47] = new OracleParameter("p_SJB_B_ADD1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_B_ADD1;
                    (param1[48] = new OracleParameter("p_SJB_B_ADD2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_B_ADD2;
                    (param1[49] = new OracleParameter("p_SJB_B_ADD3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_B_ADD3;
                    (param1[50] = new OracleParameter("p_SJB_B_TOWN", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_B_TOWN;
                    (param1[51] = new OracleParameter("p_SJB_B_PHNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_B_PHNO;
                    (param1[52] = new OracleParameter("p_SJB_B_FAX", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_B_FAX;
                    (param1[53] = new OracleParameter("p_SJB_B_EMAIL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_B_EMAIL;
                    (param1[54] = new OracleParameter("p_SJB_INFM_PERSON", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_INFM_PERSON;
                    (param1[55] = new OracleParameter("p_SJB_INFM_ADD1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_INFM_ADD1;
                    (param1[56] = new OracleParameter("p_SJB_INFM_ADD2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_INFM_ADD2;
                    (param1[57] = new OracleParameter("p_SJB_INFM_PHNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_INFM_PHNO;
                    (param1[58] = new OracleParameter("p_SJB_STUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_STUS;
                    (param1[59] = new OracleParameter("p_SJB_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_CRE_BY;
                    (param1[60] = new OracleParameter("p_SJB_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = oHeader.SJB_CRE_DT;
                    (param1[61] = new OracleParameter("p_SJB_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_MOD_BY;
                    (param1[62] = new OracleParameter("p_SJB_MOD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = oHeader.SJB_MOD_DT;
                    (param1[63] = new OracleParameter("SJB_CHG_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oHeader.SJB_CHG_CD;
                    (param1[64] = new OracleParameter("SJB_CHG", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = oHeader.SJB_CHG;
                    param1[65] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

                    #endregion Set Para

                    Int32 effects1 = (Int16)UpdateRecords(oConnEMS, "sp_upd_cust_job_hdr", CommandType.StoredProcedure, param1);

                    Console.WriteLine("Saved job number :-" + oHeader.SJB_JOBNO);

                    foreach (Service_job_Det oItem in oSelectedItem)
                    {
                        OracleParameter[] param2 = new OracleParameter[101];

                        #region Set Para

                        (param2[0] = new OracleParameter("p_jbd_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Jbd_seq_no;
                        (param2[1] = new OracleParameter("p_jbd_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "HMC-" + oItem.Jbd_jobno;
                        (param2[2] = new OracleParameter("p_jbd_jobline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Jbd_jobline;
                        (param2[3] = new OracleParameter("p_jbd_sjobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_sjobno;
                        (param2[4] = new OracleParameter("p_jbd_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_loc;
                        (param2[5] = new OracleParameter("p_jbd_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_pc;
                        (param2[6] = new OracleParameter("p_jbd_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_itm_cd;
                        (param2[7] = new OracleParameter("p_jbd_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_itm_stus;
                        (param2[8] = new OracleParameter("p_jbd_itm_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_itm_desc;
                        (param2[9] = new OracleParameter("p_jbd_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_brand;
                        (param2[10] = new OracleParameter("p_jbd_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_model;
                        (param2[11] = new OracleParameter("p_jbd_itm_cost", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Jbd_itm_cost;
                        (param2[12] = new OracleParameter("p_jbd_ser1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_ser1;
                        (param2[13] = new OracleParameter("p_jbd_ser2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_ser2;
                        (param2[14] = new OracleParameter("p_jbd_warr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_warr;
                        (param2[15] = new OracleParameter("p_jbd_regno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_regno;
                        (param2[16] = new OracleParameter("p_jbd_milage", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Jbd_milage;
                        (param2[17] = new OracleParameter("p_jbd_warr_stus", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Jbd_warr_stus;
                        (param2[18] = new OracleParameter("p_jbd_onloan", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Jbd_onloan;
                        (param2[19] = new OracleParameter("p_jbd_chg_warr_stdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = oItem.Jbd_chg_warr_stdt;
                        (param2[20] = new OracleParameter("p_jbd_chg_warr_rmk", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_chg_warr_rmk;
                        (param2[21] = new OracleParameter("p_jbd_isinsurance", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Jbd_isinsurance;
                        (param2[22] = new OracleParameter("p_jbd_cate1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_cate1;
                        (param2[23] = new OracleParameter("p_jbd_ser_term", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Jbd_ser_term;
                        (param2[24] = new OracleParameter("p_jbd_lastwarr_stdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = oItem.Jbd_lastwarr_stdt;
                        (param2[25] = new OracleParameter("p_jbd_issued", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Jbd_issued;
                        (param2[26] = new OracleParameter("p_jbd_mainitmcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_mainitmcd;
                        (param2[27] = new OracleParameter("p_jbd_mainitmser", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_mainitmser;
                        (param2[28] = new OracleParameter("p_jbd_mainitmwarr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_mainitmwarr;
                        (param2[29] = new OracleParameter("p_jbd_itmmfc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_itmmfc;
                        (param2[30] = new OracleParameter("p_jbd_mainitmmfc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_mainitmmfc;
                        (param2[31] = new OracleParameter("p_jbd_availabilty", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Jbd_availabilty;
                        (param2[32] = new OracleParameter("p_jbd_usejob", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_usejob;
                        (param2[33] = new OracleParameter("p_jbd_msnno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_msnno;
                        (param2[34] = new OracleParameter("p_jbd_itmtp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_itmtp;
                        (param2[35] = new OracleParameter("p_jbd_serlocchr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_serlocchr;
                        (param2[36] = new OracleParameter("p_jbd_custnotes", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_custnotes;
                        (param2[37] = new OracleParameter("p_jbd_mainreqno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_mainreqno;
                        (param2[38] = new OracleParameter("p_jbd_mainreqloc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_mainreqloc;
                        (param2[39] = new OracleParameter("p_jbd_mainjobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_mainjobno;
                        (param2[40] = new OracleParameter("p_jbd_reqitmtp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_reqitmtp;
                        (param2[41] = new OracleParameter("p_jbd_reqno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_reqno;
                        (param2[42] = new OracleParameter("p_jbd_reqline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Jbd_reqline;
                        (param2[43] = new OracleParameter("p_jbd_isstockupdate", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Jbd_isstockupdate;
                        (param2[44] = new OracleParameter("p_jbd_isgatepass", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Jbd_needgatepass;
                        (param2[45] = new OracleParameter("p_jbd_iswrn", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Jbd_iswrn;
                        (param2[46] = new OracleParameter("p_jbd_warrperiod", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Jbd_warrperiod;
                        (param2[47] = new OracleParameter("p_jbd_warrrmk", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_warrrmk;
                        (param2[48] = new OracleParameter("p_jbd_warrstartdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = oItem.Jbd_warrstartdt;
                        (param2[49] = new OracleParameter("p_jbd_warrreplace", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Jbd_warrreplace;
                        (param2[50] = new OracleParameter("p_jbd_date_pur", OracleDbType.Date, null, ParameterDirection.Input)).Value = oItem.Jbd_date_pur;
                        (param2[51] = new OracleParameter("p_jbd_invc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_invc_no;
                        (param2[52] = new OracleParameter("p_jbd_waraamd_seq", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_waraamd_seq;
                        (param2[53] = new OracleParameter("p_jbd_waraamd_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_waraamd_by;
                        (param2[54] = new OracleParameter("p_jbd_waraamd_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = oItem.Jbd_waraamd_dt;
                        (param2[55] = new OracleParameter("p_jbd_invc_showroom", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_invc_showroom;
                        (param2[56] = new OracleParameter("p_jbd_aodissueloc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_aodissueloc;
                        (param2[57] = new OracleParameter("p_jbd_aodissuedt", OracleDbType.Date, null, ParameterDirection.Input)).Value = oItem.Jbd_aodissuedt;
                        (param2[58] = new OracleParameter("p_jbd_aodissueno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_aodissueno;
                        (param2[59] = new OracleParameter("p_jbd_aodrecno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_aodrecno;
                        (param2[60] = new OracleParameter("p_jbd_techst_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = oItem.Jbd_techst_dt;
                        (param2[61] = new OracleParameter("p_jbd_techfin_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = oItem.Jbd_techfin_dt;
                        (param2[62] = new OracleParameter("p_jbd_msn_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_msn_no;
                        (param2[63] = new OracleParameter("p_jbd_isexternalitm", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Jbd_isexternalitm;
                        (param2[64] = new OracleParameter("p_jbd_conf_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = oItem.Jbd_conf_dt;
                        (param2[65] = new OracleParameter("p_jbd_conf_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_conf_cd;
                        (param2[66] = new OracleParameter("p_jbd_conf_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_conf_desc;
                        (param2[67] = new OracleParameter("p_jbd_conf_rmk", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_conf_rmk;
                        (param2[68] = new OracleParameter("p_jbd_tranf_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_tranf_by;
                        (param2[69] = new OracleParameter("p_jbd_tranf_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = oItem.Jbd_tranf_dt;
                        (param2[70] = new OracleParameter("p_jbd_do_invoice", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Jbd_do_invoice;
                        (param2[71] = new OracleParameter("p_jbd_insu_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_insu_com;
                        (param2[72] = new OracleParameter("p_jbd_agreeno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_agreeno;
                        (param2[73] = new OracleParameter("p_jbd_issrn", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Jbd_issrn;
                        (param2[74] = new OracleParameter("p_jbd_isagreement", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_isagreement;
                        (param2[75] = new OracleParameter("p_jbd_cust_agreeno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_cust_agreeno;
                        (param2[76] = new OracleParameter("p_jbd_quo_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_quo_no;
                        (param2[77] = new OracleParameter("p_jbd_stage", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = oItem.Jbd_stage;
                        (param2[78] = new OracleParameter("p_jbd_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_com;
                        (param2[79] = new OracleParameter("p_jbd_ser_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_ser_id;
                        (param2[80] = new OracleParameter("p_jbd_techst_dt_man", OracleDbType.Date, null, ParameterDirection.Input)).Value = oItem.Jbd_techst_dt_man;
                        (param2[81] = new OracleParameter("p_jbd_techfin_dt_man", OracleDbType.Date, null, ParameterDirection.Input)).Value = oItem.Jbd_techfin_dt_man;
                        (param2[82] = new OracleParameter("p_jbd_reqwcn", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Jbd_reqwcn;
                        (param2[83] = new OracleParameter("p_jbd_reqwcndt", OracleDbType.Date, null, ParameterDirection.Input)).Value = oItem.Jbd_reqwcnsysdt;
                        (param2[84] = new OracleParameter("p_jbd_reqwcnsysdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = oItem.Jbd_reqwcnsysdt;
                        (param2[85] = new OracleParameter("p_jbd_sentwcn", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Jbd_sentwcn;
                        (param2[86] = new OracleParameter("p_jbd_recwcn", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Jbd_recwcn;
                        (param2[87] = new OracleParameter("p_jbd_takewcn", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Jbd_takewcn;
                        (param2[88] = new OracleParameter("p_jbd_takewcndt", OracleDbType.Date, null, ParameterDirection.Input)).Value = oItem.Jbd_takewcndt;
                        (param2[89] = new OracleParameter("p_jbd_takewcnsysdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = oItem.Jbd_takewcnsysdt;
                        (param2[90] = new OracleParameter("p_jbd_supp_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_supp_cd;
                        (param2[91] = new OracleParameter("p_jbd_part_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_part_cd;
                        (param2[92] = new OracleParameter("p_jbd_oem_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_oem_no;
                        (param2[93] = new OracleParameter("p_jbd_case_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_case_id;
                        (param2[94] = new OracleParameter("p_jbd_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Jbd_act;
                        (param2[95] = new OracleParameter("p_jbd_oldjobline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.Jbd_oldjobline;
                        (param2[96] = new OracleParameter("p_jbd_tech_rmk", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_tech_rmk;
                        (param2[97] = new OracleParameter("p_jbd_tech_custrmk", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_tech_custrmk;
                        (param2[98] = new OracleParameter("p_jbd_tech_cls_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.Jbd_tech_cls_tp;
                        (param2[99] = new OracleParameter("P_JBD_TECH_CLS_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.JBD_TECH_CLS_RMK;
                        #endregion Set Para
                         

                        param2[100] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

                        Int32 effects3 = (Int16)UpdateRecords(oConnEMS, "sp_upd_cust_job_det", CommandType.StoredProcedure, param2);
                        Console.WriteLine("     Saved job Item Line :-" + oItem.Jbd_jobline.ToString());
                    }

                    #region Update HMC SAT HEADER

                    String _sql3 = @"UPDATE SAT_HDR SET SAH_ANAL_10 = 1
                                 WHERE SAH_REF_DOC = :P_JOBNUM";

                    OracleCommand _oCom3 = new OracleCommand(_sql3, oConnHMC);
                    _oCom3.Parameters.Add(":P_JOBNUM", OracleDbType.NVarchar2).Value = oHeader.SJB_JOBNO;
                    _oCom3.Transaction = oTrHMC;
                    int result = _oCom3.ExecuteNonQuery();
                    if (result >0 )
                    {
                        Console.WriteLine("HMC updated job number :- " + oHeader.SJB_JOBNO);
                    }
                    else
                    {
                        Console.WriteLine("********** HMC updated Failed.");
                    }

                    #endregion Update HMC SAT HEADER
                }

                EmsCommit();
                HMCCommit();

                #endregion Save Job Header + Details

                Console.WriteLine("Jobs migrate from HMC db to live SCM2 db completed. (" + DateTime.Now.ToString("hh.mm.ss tt") + ")");
                Console.WriteLine("");

            }
            catch (Exception ex)
            {
                EmsRollback();
                HMCRollback();

                PuchaseOrderGeneration oPogeneration = new PuchaseOrderGeneration();
                oPogeneration.Send_SMTPMail("chamald@abansgroup.com", "chamald@abansgroup.com",  "chamald@abansgroup.com",  "chamald@abansgroup.com", "chamald@abansgroup.com", "HMC/BDL Agent Issue", "Job Details- HMC" + ex.Message);
                Console.WriteLine("");
                Console.WriteLine("***********************ERROR :-" + ex.Message);
            }
        }
    }
}