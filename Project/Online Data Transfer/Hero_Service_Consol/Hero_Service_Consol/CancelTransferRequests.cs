using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;
using EMS_Upload_Console;
using Hero_Service_Consol.DTOs;
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using CrystalDecisions.Shared;

namespace Hero_Service_Consol
{
    class CancelTransferRequests
    {
        //Job running time units
        public enum JobRunningTimeUnits
        {
            SECOND,
            MINUTE,
            HOUR,
            DAY,
            WEEK,
            MONTH
        };

        public enum JobName
        {
            CANCELTRANSFERREQUEST,
            CNLTRANSFERREQBYPERIOD
        };

        private string _connectionString = null;

        OracleConnection _connection = null;
        OracleTransaction _transaction = null;

        //Initialize the connection wen create the object
        public CancelTransferRequests(string _conString)
        {
            _connectionString= _conString;
        }
        
        //Prepare command object
        private void PrepareCommand(OracleCommand _command, OracleConnection _connection, CommandType _commandType, string _commandText, params  OracleParameter[] _parameters)
        {
            _command.Connection = _connection;
            _command.CommandText = _commandText;
            _command.CommandType = _commandType;

            if (_parameters != null)
            {
                foreach (OracleParameter param in _parameters)
                {
                    if ((param.Direction == ParameterDirection.InputOutput) && (param.Value == null))
                    {
                        param.Value = DBNull.Value;
                    }
                    _command.Parameters.Add(param);
                }
            }
            return;
        }

        //Data Adapter operations
        private DataTable GetData(string _tableName, CommandType _commandType, string _commandString, params OracleParameter[] _parameters)
        {
            DataTable _returnTable = new DataTable(_tableName);
            try
            {
                //using (OracleConnection _connection = new OracleConnection(_connectionString))
                //{
                //    _connection.Open();
                    OracleCommand _command = new OracleCommand();
                
                    PrepareCommand(_command, _connection, _commandType, _commandString, _parameters);
                    OracleDataAdapter _dataAdapter = new OracleDataAdapter(_command);
                    _dataAdapter.SelectCommand = _command;
                    _dataAdapter.Fill(_returnTable);
                //}
            }
            catch (Exception)
            {
                throw;
            }

            return _returnTable;
        }

        //Update operation
        private Int32 UpdateData(string _commandString, CommandType _commandType, params OracleParameter[] _parameter)
        {
            Int32 _updatedCount = 0;

            //using(OracleConnection _connection = new OracleConnection(_connectionString))
            //{
            //    _connection.Open();

                OracleCommand _command = new OracleCommand();
                //OracleTransaction _transaction = _connection.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    _command.Transaction = _transaction;
                    PrepareCommand(_command, _connection, _commandType, _commandString, _parameter);
                    _updatedCount = _command.ExecuteNonQuery();
                   // _transaction.Commit();
                }
                catch (Exception)
                {
                    //_transaction.Rollback();
                    throw;
                }
           // }

            return _updatedCount;
        }

        ////Insert Operation
        //private Int32 SaveData()
        //{ 
        //}

        private DataTable GetTransferRequests()
        {
            DataTable _requests = new DataTable();
            try
            {
                OracleParameter[] _parameter = new OracleParameter[1];
                _parameter[0] = new OracleParameter("o_tranRequests", OracleDbType.RefCursor, ParameterDirection.Output);

                _requests = GetData("Transfer_Requests", CommandType.StoredProcedure, "sp_get_tran_requests", _parameter);
            }
            catch (Exception)
            {
                throw;
            }
            return _requests;
        }
        private DataTable GetTransferRequests(string _comCode, int _period)
        {
            DataTable _requests = new DataTable();
            try
            {
                OracleParameter[] _parameter = new OracleParameter[3];
                (_parameter[0] = new OracleParameter("p_comcode", OracleDbType.NVarchar2, ParameterDirection.Input)).Value = _comCode;
                (_parameter[1] = new OracleParameter("p_period", OracleDbType.Int32, ParameterDirection.Input)).Value = _period;
                _parameter[2] = new OracleParameter("o_tranRequests", OracleDbType.RefCursor, ParameterDirection.Output);

                _requests = GetData("Transfer_Requests", CommandType.StoredProcedure, "sp_get_TranRequestsByPeriod", _parameter);
            }
            catch (Exception)
            {
                throw;
            }
            return _requests;
        }

        private DataTable GetSerilas(string comCode, string locCode, Int32 serialId, string itemCode)
        {
            DataTable _serails = new DataTable();

            try
            {
                OracleParameter[] _parameter = new OracleParameter[5];
                (_parameter[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, ParameterDirection.Input)).Value = comCode;
                (_parameter[1] = new OracleParameter("p_item", OracleDbType.NVarchar2, ParameterDirection.Input)).Value = itemCode;
                (_parameter[2] = new OracleParameter("p_serial", OracleDbType.NVarchar2, ParameterDirection.Input)).Value = serialId;
                (_parameter[3] = new OracleParameter("p_loc", OracleDbType.NVarchar2, ParameterDirection.Input)).Value = locCode;

                _parameter[4] = new OracleParameter("c_data", OracleDbType.RefCursor, ParameterDirection.Output);

                _serails = GetData("tbl_serails", CommandType.StoredProcedure, "sp_checkserial", _parameter);
            }
            catch (Exception)
            {
                throw;
            }
            return _serails;
        }

        private Int32 UpdateRequestStatus(string comCode, string locCode, string requestNo)
        {
            Int32 _rowEffected = 0;
            try
            {
                OracleParameter[] _parameter = new OracleParameter[7];
                (_parameter[0] = new OracleParameter("P_STS", OracleDbType.NVarchar2, ParameterDirection.Input)).Value = null;
                (_parameter[1] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, ParameterDirection.Input)).Value = null;
                (_parameter[2] = new OracleParameter("P_LINE", OracleDbType.Int32, ParameterDirection.Input)).Value = 0;
                (_parameter[3] = new OracleParameter("P_REQNO", OracleDbType.NVarchar2, ParameterDirection.Input)).Value = requestNo;
                (_parameter[4] = new OracleParameter("P_COMCODE", OracleDbType.NVarchar2, ParameterDirection.Input)).Value = comCode;
                (_parameter[5] = new OracleParameter("P_LOCCODE", OracleDbType.NVarchar2, ParameterDirection.Input)).Value = locCode;

                _parameter[6] = new OracleParameter("O_EFFECT", OracleDbType.Int32, ParameterDirection.Output);
               _rowEffected =  UpdateData("SP_UPDATE_TRANSFER_REQUEST_NEW", CommandType.StoredProcedure, _parameter);

               if (_rowEffected != 0)
               {
                   _rowEffected = 1;
               }

            }
            catch (Exception)
            {
                _rowEffected = 0;
                throw;
            }
            return _rowEffected;
        }

        private DataTable GetJobSchedules(string jobName)
        {
            DataTable _schedules = new DataTable();

            try
            {
                OracleParameter[] _parameter = new OracleParameter[2];

                (_parameter[0] = new OracleParameter("p_jobName", OracleDbType.NVarchar2, ParameterDirection.Input)).Value = jobName;
                _parameter[1] = new OracleParameter("o_schedules", OracleDbType.RefCursor, ParameterDirection.Output);

                _schedules = GetData("tbl_JobSchedules", CommandType.StoredProcedure, "sp_get_JobAgentSchedules", _parameter);
            }
            catch (Exception)
            {                
                throw;
            }
            return _schedules;
        }

        private MstModuleConf InitializeJobs(string jobName)
        {
            MstModuleConf _config = null;

            DataTable _schedules = new DataTable();
            _schedules = GetJobSchedules(jobName);
            if (_schedules.Rows.Count > 0)
            {
                _config = new MstModuleConf(_schedules.Rows[0]);
            }
            return _config;
        }

        private bool IsScheduleValidate(DateTime _lastRunDateTime, string _timeUnit, int _mmcRunTime)
        {
            bool _isvalidate = false;
            int _timeDiff = 0;

            try
            {
               if(_timeUnit.ToUpper() == JobRunningTimeUnits.SECOND.ToString ())
               {
                   _timeDiff = DateTime.Now.Subtract(_lastRunDateTime).Seconds;
               }
               else if (_timeUnit.ToUpper() == JobRunningTimeUnits.MINUTE.ToString())
               {
                   _timeDiff = DateTime.Now.Subtract(_lastRunDateTime).Minutes;
               }
               else if (_timeUnit.ToUpper() == JobRunningTimeUnits.HOUR.ToString())
               {
                   _timeDiff = DateTime.Now.Subtract(_lastRunDateTime).Hours;
               }
               else if (_timeUnit.ToUpper() == JobRunningTimeUnits.DAY.ToString())
               {
                   _timeDiff = DateTime.Now.Subtract(_lastRunDateTime).Days;
               }

               if (_timeDiff >= _mmcRunTime)
               {
                   _isvalidate = true;
               }
            }
            catch (Exception)
            {                
                throw;
            }
            return _isvalidate;
        }

        private void UpdateLastRunDate(string jobName) 
        {
            try
            {
                OracleParameter[] _parameter = new OracleParameter[2];

                (_parameter[0] = new OracleParameter("p_jobName", OracleDbType.NVarchar2, ParameterDirection.Input)).Value = jobName;
                _parameter[1] = new OracleParameter("o_effect", OracleDbType.Int32, ParameterDirection.Output);

                UpdateData("sp_Update_MstModuleConf", CommandType.StoredProcedure, _parameter);
            }
            catch (Exception)
            {                
                //throw;
            }
        }

        private void SaveCnlTransferOrders(DataRow _transferOrders)
        {
            try
            {
                TmpCnlTransOrderReq _caneledOrders = new TmpCnlTransOrderReq(_transferOrders);

                OracleParameter[] _paramter = new OracleParameter[8];
                (_paramter[0] = new OracleParameter("p_comcode", OracleDbType.NVarchar2, ParameterDirection.InputOutput)).Value = _caneledOrders.ComCode;
                (_paramter[1] = new OracleParameter("p_loccode", OracleDbType.NVarchar2, ParameterDirection.InputOutput)).Value = _caneledOrders.LocCode;
                (_paramter[2] = new OracleParameter("p_reqno", OracleDbType.NVarchar2, ParameterDirection.InputOutput)).Value = _caneledOrders.ReqCode;
                (_paramter[3] = new OracleParameter("p_reqtype", OracleDbType.NVarchar2, ParameterDirection.InputOutput)).Value = _caneledOrders.ReqType;
                (_paramter[4] = new OracleParameter("p_reqstatus", OracleDbType.NVarchar2, ParameterDirection.InputOutput)).Value = _caneledOrders.ReqStatus;
                (_paramter[5] = new OracleParameter("p_reqdate", OracleDbType.Date, ParameterDirection.InputOutput)).Value = _caneledOrders.ReqDate;
                (_paramter[6] = new OracleParameter("p_issuefrm", OracleDbType.NVarchar2, ParameterDirection.InputOutput)).Value = _caneledOrders.IssueFrm;

                _paramter[7] = new OracleParameter("o_effect", OracleDbType.Int32, ParameterDirection.Output);

                UpdateData("sp_SaveCnlTranferReq", CommandType.StoredProcedure, _paramter);

            }
            catch (Exception)
            {                
                throw;
            }
        }

        private void DeleteTmpCnlTransferOrders()
        {
              OracleParameter[] _paramter = new OracleParameter[1];
              try
              {
                  _paramter[0] = new OracleParameter("o_effect", OracleDbType.Int32, ParameterDirection.Output);
                  UpdateData("sp_delete_tmpcnltransorderreq", CommandType.StoredProcedure, _paramter);
              }
              catch (Exception)
              {
                  
                  throw;
              }
        }

        private List<string> GetCnlPeriodByComapny()
        {
            List<string> _periodsWithCompany = new List<string>();

            try
            {
                _periodsWithCompany = ConfigurationManager.AppSettings.AllKeys
                                        .Where(key => key.StartsWith("cnlPeriod"))
                                        .Select(key => ConfigurationManager.AppSettings[key]).ToList();
            }
            catch (Exception)
            {                
                throw;
            }
            return _periodsWithCompany;
        }

        public void CancelRequests()
        {
            int _noOfRequestsFound = 0;
            int _noOfRequestUpdated = 0;

            try
            {
                Console.WriteLine();

                using (_connection = new OracleConnection(_connectionString))
                {
                    _connection.Open();
                    _transaction = _connection.BeginTransaction(IsolationLevel.ReadCommitted);

                    try
                    {
                        MstModuleConf _configs = InitializeJobs(JobName.CANCELTRANSFERREQUEST.ToString());
                        if (_configs != null)
                        {
                            if (IsScheduleValidate(_configs.Mmc_Last_Run_Dt, _configs.Mmc_Run_Time_Unit, _configs.Mmc_Run_Time))
                            {
                                // Load pending and approved GRAN from int_req
                                DataTable _transferRequests = new DataTable();
                                _transferRequests = GetTransferRequests(); 
                                
                                if (_transferRequests.Rows.Count > 0)
                                {                                    
                                    //Delete temp data 
                                    DeleteTmpCnlTransferOrders();
                                    _noOfRequestsFound = _transferRequests.Rows.Count;

                                    foreach (DataRow request in _transferRequests.Rows)
                                    {                                     
                                        string _reqestNo = request["itr_req_no"] == DBNull.Value ? string.Empty : request["itr_req_no"].ToString();
                                        string _comCode = request["itr_com"] == DBNull.Value ?string.Empty : request["itr_com"].ToString();
                                        string _locCode = request["itr_loc"] == DBNull.Value ? string.Empty : request["itr_loc"].ToString();
                                        
                                        //string _reqestNo = request["itr_req_no"].ToString();
                                        //Int32 _serialId = Convert.ToInt32(request["itrs_ser_id"].ToString());
                                        //string _itemCode = request["itrs_itm_cd"].ToString();

                                        //Load request details
                                        DataTable _requestDetails = new DataTable();
                                        _requestDetails = GetTransferRequestDetails(_comCode, _locCode, _reqestNo);
                                        if (_requestDetails.Rows.Count > 0)
                                        {
                                            //add by akila 20174/05/25
                                            var _tmpCount = _requestDetails.AsEnumerable().Count(x => x.Field<string>("itr_req_no") == _reqestNo && x.Field<Int16>("ins_available") != 0);
                                            if (_tmpCount == 0)
                                            {
                                                int _effected = UpdateRequestStatus(_comCode, _locCode, _reqestNo);
                                                if (_effected > 0)
                                                {
                                                    SaveCnlTransferOrders(request);
                                                    Console.WriteLine(string.Format("Request# {0} updated ==> Company - {1} | Location - {2} ", _reqestNo, _comCode, _locCode));
                                                    _noOfRequestUpdated++;
                                                }
                                            }
                                        }                                        
                                    }

                                    //Generate Report
                                    GenerateCnlTransOrderRpt(JobName.CANCELTRANSFERREQUEST);

                                    //Send Email
                                    JobAgentEmail _agentEmail = new JobAgentEmail(GetEmailAddress(JobName.CANCELTRANSFERREQUEST));

                                    string _errMessage = null;
                                    int result = _agentEmail.SendEMail(_agentEmail.RecipientAddress, _agentEmail.BccRecipient, _agentEmail.CcRecipient, _agentEmail.EmailBody, ref _errMessage);
                                    if (!string.IsNullOrEmpty(_errMessage))
                                    {
                                        throw new Exception(_errMessage);
                                    }
                                    else if (result == 1)
                                    {
                                        Console.WriteLine("Email have been send successfully");
                                    }

                                    //update mst_module_conf
                                    UpdateLastRunDate(JobName.CANCELTRANSFERREQUEST.ToString());

                                    _transaction.Commit();
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Job cannot be executed! Job schedule details not found." + Environment.NewLine + "Please check the table - mst_module_conf | mmc_mod_cd --> " + JobName.CNLTRANSFERREQBYPERIOD.ToString());
                        }
                    }
                    catch (Exception)
                    {
                        _transaction.Rollback();
                        throw;
                    }
                }

                Console.WriteLine();
                Console.WriteLine("Number of request found = " + _noOfRequestsFound);
                Console.WriteLine("Number of request updated = " + _noOfRequestUpdated);
            }
            catch (Exception ex)
            {
                string _errorMsg = "Transfer order auto cancel process terminated due to an error !!!!" + Environment.NewLine + ex.Message + Environment.NewLine + ex.Source ;
                Console.WriteLine(_errorMsg);

                //Send Error Email
                JobAgentEmail _agentEmail = new JobAgentEmail();
                _agentEmail.SendErrorEmail(_errorMsg);
            }

            Console.WriteLine();
        }

        public void CancelRequestsByPeriod()
        {
            int _noOfRequestsFound = 0;
            int _noOfRequestUpdated = 0;

            try
            {
                Console.WriteLine();

                List<string> _periodsByCom = new List<string>();
                _periodsByCom = GetCnlPeriodByComapny();
                if (_periodsByCom.Count() > 0)
                {
                    using (_connection = new OracleConnection(_connectionString))
                    {
                        _connection.Open();
                        _transaction = _connection.BeginTransaction(IsolationLevel.ReadCommitted);

                        try
                        {  
                             MstModuleConf _configs = InitializeJobs(JobName.CNLTRANSFERREQBYPERIOD.ToString());
                             if (_configs != null)
                             {
                                 if (IsScheduleValidate(_configs.Mmc_Last_Run_Dt, _configs.Mmc_Run_Time_Unit, _configs.Mmc_Run_Time))
                                 {
                                     //Delete temp data
                                     DeleteTmpCnlTransferOrders();

                                     foreach (string item in _periodsByCom)
                                     { 
                                         List<string> _splitedStr = item.Split('|').ToList();
                                         if (_splitedStr.Count > 0)
                                         {
                                             string _comCode = _splitedStr[0];
                                             int _period = Convert.ToInt32(_splitedStr[1]);

                                             Console.WriteLine("Job start for company : " + _comCode);
                                             DataTable _transferRequests = new DataTable();
                                             _transferRequests = GetTransferRequests(_comCode, _period);

                                             if (_transferRequests.Rows.Count > 0)
                                              {
                                                 _noOfRequestsFound = _transferRequests.Rows.Count;
                                                 foreach (DataRow request in _transferRequests.Rows)
                                                 {
                                                     string _locCode = request["itr_loc"].ToString();
                                                     string _reqestNo = request["itr_req_no"].ToString();

                                                     int _effected = UpdateRequestStatus(_comCode, _locCode, _reqestNo);
                                                     if (_effected > 0)
                                                     {
                                                         SaveCnlTransferOrders(request);
                                                         Console.WriteLine(string.Format("Request# {0} updated ==> Company - {1} | Location - {2} ", _reqestNo, _comCode, _locCode));
                                                         _noOfRequestUpdated++;
                                                     }
                                                 }
                                             }
                                         }
                                     }

                                     //Generate Report
                                     GenerateCnlTransOrderRpt(JobName.CNLTRANSFERREQBYPERIOD);

                                     //Send Email
                                     JobAgentEmail _agentEmail = new JobAgentEmail(GetEmailAddress(JobName.CNLTRANSFERREQBYPERIOD));

                                     string _errMessage = null;
                                     int result = _agentEmail.SendEMail(_agentEmail.RecipientAddress, _agentEmail.BccRecipient, _agentEmail.CcRecipient, _agentEmail.EmailBody, ref _errMessage);
                                     if (!string.IsNullOrEmpty(_errMessage))
                                     {
                                         throw new Exception(_errMessage);
                                     }
                                     else if (result == 1)
                                     {
                                         Console.WriteLine("Email have been send successfully");
                                     }

                                     //update mst_module_conf
                                     UpdateLastRunDate(JobName.CNLTRANSFERREQBYPERIOD.ToString());

                                     _transaction.Commit();
                                 }  
                             }
                             else
                             {                                 
                                 throw new Exception("Job cannot be executed! Job schedule details not found." + Environment.NewLine + "Please check the table - mst_module_conf | mmc_mod_cd --> " + JobName.CNLTRANSFERREQBYPERIOD.ToString());
                             }
                        }
                        catch (Exception)
                        {
                            _transaction.Rollback();
                            throw;
                        }
                    }

                    Console.WriteLine();
                    Console.WriteLine("Number of request found = " + _noOfRequestsFound);
                    Console.WriteLine("Number of request updated = " + _noOfRequestUpdated);
                }
                else
                {
                    throw new Exception("Program cannot be executed. Missing app seting(cnlPeriod) in app.config file!");
                }
            }
            catch (Exception ex)
            {
                string _errorMsg = "Transfer order auto cancel process terminated due to an error !!!!" + Environment.NewLine + ex.Message + Environment.NewLine + ex.Source;
                Console.WriteLine(_errorMsg);
                
                //Send Error Email
                JobAgentEmail _agentEmail = new JobAgentEmail();
                _agentEmail.SendErrorEmail(_errorMsg);
            }

            Console.WriteLine();
        }

        private DataTable GetEmailAddress(JobName _name)
        {
            DataTable _emailAddresses = new DataTable();

            try
            {
                OracleParameter[] _parameter = new OracleParameter[2];
                (_parameter[0] = new OracleParameter("p_module", OracleDbType.NVarchar2, ParameterDirection.Input)).Value = _name.ToString();

                _parameter[1] = new OracleParameter("o_records", OracleDbType.RefCursor, ParameterDirection.Output);
                _emailAddresses = GetData("tbl_EmailAddress", CommandType.StoredProcedure, "SP_GetEmailAddess", _parameter);
            }
            catch (Exception)
            {                
                throw;
            }
            return _emailAddresses;
        }

        private DataTable GetTmpTranRequest()
        {
            DataTable _tmpRequest = new DataTable();

            try
            {
                OracleParameter[] _parameter = new OracleParameter[1];

                _parameter[0] = new OracleParameter("o_result", OracleDbType.RefCursor, ParameterDirection.Output);

                _tmpRequest = GetData("tbl_CnlTransOrders", CommandType.StoredProcedure, "sp_get_tmpcnltransorderreq", _parameter);
            }
            catch (Exception)
            {                
                throw;
            }
            return _tmpRequest;
        }

        private void GenerateCnlTransOrderRpt( JobName _jobName)
        {
            DataTable _cnlTranOrders = new DataTable();

            try
            {
                _cnlTranOrders = GetTmpTranRequest();

                ReportDocument rptDoc = new ReportDocument();
                string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "rptTransferOrderCancel.rpt");
                
                rptDoc.Load(filePath);
                rptDoc.SetDataSource(_cnlTranOrders);

                if (_jobName == JobName.CANCELTRANSFERREQUEST)
                {
                    rptDoc.SummaryInfo.ReportTitle = "Cancelled Inter Transfer Request [GRAN]";
                }
                else if (_jobName == JobName.CNLTRANSFERREQBYPERIOD)
                {
                    rptDoc.SummaryInfo.ReportTitle = "Cancelled Inter Transfer Request [INTR]";
                }

                if (!Directory.Exists(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Reports")))
                { 
                    Directory.CreateDirectory(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Reports"));
                }
                rptDoc.ExportToDisk(ExportFormatType.PortableDocFormat, Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Reports\\CanceledTransferRequest.pdf"));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while genarating report. " + ex.Message);
            }
        }

        private DataTable GetTransferRequestDetails( string _company, string _location, string _requestNo)
        {
            DataTable _requests = new DataTable();
            try
            {
                OracleParameter[] _parameter = new OracleParameter[4];
                (_parameter[0] = new OracleParameter("p_comCode", OracleDbType.NVarchar2, ParameterDirection.Input)).Value = _company;
                (_parameter[1] = new OracleParameter("p_location", OracleDbType.NVarchar2, ParameterDirection.Input)).Value = _location;
                (_parameter[2] = new OracleParameter("p_requestNo", OracleDbType.NVarchar2, ParameterDirection.Input)).Value = _requestNo;
                _parameter[3] = new OracleParameter("o_tranReqDetails", OracleDbType.RefCursor, ParameterDirection.Output);

                _requests = GetData("Transfer_Request_Details", CommandType.StoredProcedure, "sp_get_tran_request_details", _parameter);
            }
            catch (Exception)
            {
                throw;
            }
            return _requests;
        }
    }
}
