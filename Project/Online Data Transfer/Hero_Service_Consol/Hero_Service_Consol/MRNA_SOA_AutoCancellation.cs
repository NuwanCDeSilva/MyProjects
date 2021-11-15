using FF.BusinessObjects;
using Hero_Service_Consol.DTOs;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Hero_Service_Consol
{
    class MRNA_SOA_AutoCancellation
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

        const string _AgentName = "MRNA_SOA_CANCELLATION";

        //Connection properties
        private string _connectionString = null;
        OracleConnection _connection = null;
        OracleTransaction _transaction = null;

        //Constuctor
        public MRNA_SOA_AutoCancellation(string _conString)
        {
            _connectionString= _conString;
        }

# region DB Operations
        //Prepare command object
        private void PrepareCommand(OracleCommand _command, OracleConnection _connection, CommandType _commandType, string _commandText, params  OracleParameter[] _parameters)
        {
            _command.Connection = _connection;
            _command.Transaction = _transaction;
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
            OracleCommand _command = new OracleCommand();
            int effect = 0;
            try
            {
                PrepareCommand(_command, _connection, _commandType, _commandString, _parameter);
                _updatedCount = _command.ExecuteNonQuery();

                if (_parameter[_parameter.Length - 1].Value == DBNull.Value)
                    effect = 0;
                else
                    effect = Convert.ToInt32(_parameter[_parameter.Length - 1].Value.ToString());
            }
            catch (Exception)
            {

                throw;
            }

            return effect;
        }

#endregion

        private bool IsScheduleValidate(DateTime _lastRunDateTime, string _timeUnit, int _mmcRunTime)
        {
            bool _isvalidate = false;
            int _timeDiff = 0;

            try
            {
                if (_timeUnit.ToUpper() == JobRunningTimeUnits.SECOND.ToString())
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

        private List<MstAlertCriteria> GetJobScheduleCriterias(string _comCode, string _jobName)
        {
            DataTable _criterias = new DataTable();
            List<MstAlertCriteria> _list = new List<MstAlertCriteria>();
            try
            {
                OracleParameter[] _parameter = new OracleParameter[3];

                (_parameter[0] = new OracleParameter("p_ComCode", OracleDbType.NVarchar2, ParameterDirection.Input)).Value = _comCode;
                (_parameter[1] = new OracleParameter("p_module", OracleDbType.NVarchar2, ParameterDirection.Input)).Value = _jobName;
                _parameter[2] = new OracleParameter("o_result", OracleDbType.RefCursor, ParameterDirection.Output);

                _criterias = GetData("Schedule_Criterias", CommandType.StoredProcedure, "SP_GET_ALERTCRITERIA", _parameter);
                if (_criterias.Rows.Count > 0)
                {
                    _list = DataTableExtensions.ToGenericList<MstAlertCriteria>(_criterias, MstAlertCriteria.Converter);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _list;
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

        private List<string> GetAgentRunnigCompany()
        {
            List<string> _companyList = new List<string>();
            try
            {
                _companyList = ConfigurationManager.AppSettings.AllKeys
                                        .Where(x => x.ToString()=="Company")
                                        .Select(x => ConfigurationManager.AppSettings[x]).First().ToString().Split('|').ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return _companyList;
        }

        private List<InventoryRequest> GetMrnaAndSoaDetails(string _company, string _criteria, string _value, int _period)
        {
            DataTable _result = new DataTable();
            List<InventoryRequest> _list = new List<InventoryRequest>();
            try
            {
                OracleParameter[] _parameter = new OracleParameter[5];

                (_parameter[0] = new OracleParameter("p_ComCode", OracleDbType.NVarchar2, ParameterDirection.Input)).Value = _company;
                (_parameter[1] = new OracleParameter("p_CriteriaType", OracleDbType.NVarchar2, ParameterDirection.Input)).Value = _criteria;
                (_parameter[2] = new OracleParameter("p_value", OracleDbType.NVarchar2, ParameterDirection.Input)).Value = _value;
                (_parameter[3] = new OracleParameter("p_Period", OracleDbType.Int32, ParameterDirection.Input)).Value = _period;
                _parameter[4] = new OracleParameter("o_result", OracleDbType.RefCursor, ParameterDirection.Output);

                _result = GetData("Mrna_And_Soa", CommandType.StoredProcedure, "SP_GET_INTREQ_DETAILS", _parameter);
                if (_result.Rows.Count > 0)
                {
                    _list = DataTableExtensions.ToGenericList<InventoryRequest>(_result, InventoryRequest.Converter);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _list;
        }

        private List<InventoryRequestItem> GetIntRequestItems(string _intRequesNo)
        {
            List<InventoryRequestItem> _list = new List<InventoryRequestItem>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_itr_req_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _intRequesNo;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = GetData("int_req_itm",  CommandType.StoredProcedure,"sp_get_int_req_itm_by_reqno", param);
            if (_dtResults.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<InventoryRequestItem>(_dtResults, InventoryRequestItem.ConverterNew);
                _list = _list.Where(x => x.Itri_bqty > 0).ToList();
            }
            return _list;
        }

        private Int32 Update_ReqHeaderStatus(string STATUS, string USER, string COM, string MRN)
        {
            OracleParameter[] param = new OracleParameter[5];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_STATUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = STATUS;
            (param[1] = new OracleParameter("P_USER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = USER;
            (param[2] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = COM;
            (param[3] = new OracleParameter("P_MRN", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = MRN;
            param[4] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateData("SP_UPDATE_INT_HDR_STUS", CommandType.StoredProcedure, param);
            return effects;
        }

        private int GetAllocatedSerialCount_byDoc(string _com, string _docno, Int32 _line)
        {
            int _count = 0;
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_COMPANY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("P_DOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _docno;
            (param[2] = new OracleParameter("P_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _line;
            param[3] = new OracleParameter("O_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dt = GetData("Allocated_Seeials", CommandType.StoredProcedure, "CHECKTEMPTICSERNW", param);
            if(_dt.Rows.Count >0)
            {
                _count = Convert.ToInt32(_dt.Rows[0][0].ToString());
            }
            return _count;
        }        

        private Int16 UpdateAutoNumber(MasterAutoNumber _masterAutoNumber)
        {
            int effected = 0;
            OracleParameter[] param = new OracleParameter[8];

            (param[0] = new OracleParameter("p_module", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _masterAutoNumber.Aut_moduleid;
            (param[1] = new OracleParameter("p_direction", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _masterAutoNumber.Aut_direction;
            (param[2] = new OracleParameter("p_startchar", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _masterAutoNumber.Aut_start_char;
            (param[3] = new OracleParameter("p_cattype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _masterAutoNumber.Aut_cate_tp;
            (param[4] = new OracleParameter("p_catcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _masterAutoNumber.Aut_cate_cd;
            (param[5] = new OracleParameter("p_modifydate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _masterAutoNumber.Aut_modify_dt;
            (param[6] = new OracleParameter("p_year", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _masterAutoNumber.Aut_year;
            param[7] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effected = UpdateData("sp_updateautonumber", CommandType.StoredProcedure, param);
            return (Int16)effected;
        }

        private int SaveInventoryRequest(InventoryRequest _inventoryRequest)
        {
            int seqno = 0;
            OracleParameter[] param = new OracleParameter[39];
            (param[0] = new OracleParameter("p_itr_req_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_req_no;
            (param[1] = new OracleParameter("p_itr_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_com;
            (param[2] = new OracleParameter("p_itr_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_tp;
            (param[3] = new OracleParameter("p_itr_sub_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_sub_tp;
            (param[4] = new OracleParameter("p_itr_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_loc;
            (param[5] = new OracleParameter("p_itr_ref", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_ref;
            (param[6] = new OracleParameter("p_itr_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_dt;
            (param[7] = new OracleParameter("p_itr_exp_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_exp_dt;
            (param[8] = new OracleParameter("p_itr_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_stus;
            (param[9] = new OracleParameter("p_itr_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_job_no;
            (param[10] = new OracleParameter("p_itr_bus_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_bus_code;
            (param[11] = new OracleParameter("p_itr_note", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_note;
            (param[12] = new OracleParameter("p_itr_issue_from", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_issue_from;
            (param[13] = new OracleParameter("p_itr_rec_to", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_rec_to;
            (param[14] = new OracleParameter("p_itr_direct", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_direct;
            (param[15] = new OracleParameter("p_itr_country_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_country_cd;
            (param[16] = new OracleParameter("p_itr_town_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_town_cd;
            (param[17] = new OracleParameter("p_itr_cur_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_cur_code;
            (param[18] = new OracleParameter("p_itr_exg_rate", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_exg_rate;
            (param[19] = new OracleParameter("p_itr_collector_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_collector_id;
            (param[20] = new OracleParameter("p_itr_collector_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_collector_name;
            (param[21] = new OracleParameter("p_itr_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_act;
            (param[22] = new OracleParameter("p_itr_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_cre_by;
            (param[23] = new OracleParameter("p_itr_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_session_id;
            (param[24] = new OracleParameter("p_itr_issue_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_issue_com; 
            (param[25] = new OracleParameter("p_itr_gran_opt1", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_gran_opt1; 
            (param[26] = new OracleParameter("p_itr_gran_opt2", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_gran_opt2; 
            (param[27] = new OracleParameter("p_itr_gran_opt3", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_gran_opt3; 
            (param[28] = new OracleParameter("p_itr_gran_opt4", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_gran_opt4; 
            (param[29] = new OracleParameter("p_itr_gran_nstus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_gran_nstus; 
            (param[30] = new OracleParameter("p_itr_gran_app_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_gran_app_by; 
            (param[31] = new OracleParameter("p_itr_gran_narrt", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_gran_narrt; 
            (param[32] = new OracleParameter("p_itr_gran_app_note", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_gran_app_note; 
            (param[33] = new OracleParameter("p_itr_gran_app_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_gran_app_stus; 
            (param[34] = new OracleParameter("p_itr_anal1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_anal1; 
            (param[35] = new OracleParameter("p_itr_anal2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_anal2;
            (param[36] = new OracleParameter("p_itr_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_seq_no; 
            (param[37] = new OracleParameter("P_itr_job_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_job_line; 
            param[38] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            seqno = UpdateData("sp_updateinventoryrequest", CommandType.StoredProcedure, param);
            _inventoryRequest.Itr_seq_no = seqno;
            return seqno;
        }        

        private MasterAutoNumber GetAutoNumber(string _module, Int32? _direction, string _startChar, string _catType, string _catCode, DateTime? _modifyDate, Int32? _year)
        {
            OracleParameter[] param = new OracleParameter[8];
            MasterAutoNumber _masterAutoNumber = new MasterAutoNumber();

            (param[0] = new OracleParameter("p_module", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _module;
            (param[1] = new OracleParameter("p_direction", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _direction;
            (param[2] = new OracleParameter("p_startchar", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _startChar;
            (param[3] = new OracleParameter("p_cattype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _catType;
            (param[4] = new OracleParameter("p_catcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _catCode;
            (param[5] = new OracleParameter("p_modifydate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _modifyDate;
            (param[6] = new OracleParameter("p_year", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _year;
            param[7] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = GetData("tblAuto", CommandType.StoredProcedure,"sp_autonumber",  param);
            
            if (_dtResults.Rows.Count > 0)
            {
                _masterAutoNumber = DataTableExtensions.ToGenericList<MasterAutoNumber>(_dtResults, MasterAutoNumber.ConvertTotal)[0];
            }
            else if (_dtResults.Rows.Count == 0)
            {
                _masterAutoNumber.Aut_cate_cd = _catCode;
                _masterAutoNumber.Aut_cate_tp = _catType;
                _masterAutoNumber.Aut_direction = _direction;
                _masterAutoNumber.Aut_modify_dt = _modifyDate;
                _masterAutoNumber.Aut_moduleid = _module;
                _masterAutoNumber.Aut_number = 1;
                _masterAutoNumber.Aut_start_char = _startChar;
                _masterAutoNumber.Aut_year = _year;
            }
            return _masterAutoNumber;
        }

        private MasterAutoNumber GetNumberSequence(string _location, string _documentType, out string _type)
        {
            MasterAutoNumber masterAuto = new MasterAutoNumber();
            string _tmpDocument = null;
            switch (_documentType)
            {
                case "MRNA":
                    _tmpDocument = "MRNC";
                    break;
                case "CONSA":
                    _tmpDocument = "CONSC";
                    break;
                case "SOA":
                    _tmpDocument = "SOC";
                    break;
                default:
                    _tmpDocument = "MRNC";
                    break;
            }
            _type = _tmpDocument;
            masterAuto.Aut_cate_tp = "LOC";
            masterAuto.Aut_cate_cd = _location;
            masterAuto.Aut_direction = null;
            masterAuto.Aut_modify_dt = null;
            masterAuto.Aut_moduleid = _tmpDocument;
            masterAuto.Aut_number = 0;
            masterAuto.Aut_start_char = _tmpDocument;
            masterAuto.Aut_year = null;

            return masterAuto;
        }

        private Int32 UpdateSerQtyForDispatch(string doc, string item, string status, int qty, string line)
        {
            int _eff = 0;
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_doc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = doc;
            (param[1] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item;
            (param[2] = new OracleParameter("p_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = status;
            (param[3] = new OracleParameter("p_qty", OracleDbType.Int32, null, ParameterDirection.Input)).Value = qty;
            (param[4] = new OracleParameter("p_line", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = line;
            param[5] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            _eff = (Int32)UpdateData("Update_ser_qty_dispatch", CommandType.StoredProcedure, param);
            return _eff;
        }

        private Int32 UpdateCancelRequestDetails(string _requestno, string _item, decimal _qty, Int32 _lineno, string _refDocNo = null)
        {
            OracleParameter[] param = new OracleParameter[6];
            Int16 effects = 0;

            (param[0] = new OracleParameter("p_reqNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _requestno;
            (param[1] = new OracleParameter("p_RefNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _refDocNo;
            (param[2] = new OracleParameter("p_Item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[3] = new OracleParameter("p_Qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _qty;
            (param[4] = new OracleParameter("p_LineNo", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _lineno;
            param[5] = new OracleParameter("o_Effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateData("SP_UPDATE_CNLSOAANDMRNA", CommandType.StoredProcedure, param);
            return effects;
        }

        private MasterItem GetItemDetails(string _company, string _item)
        {
            MasterItem _itemList = null;
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _itemTable = GetData("tblItem", CommandType.StoredProcedure, "get_allitemdetails", param);
            if (_itemTable.Rows.Count > 0)
            {
                _itemList = DataTableExtensions.ToGenericList<MasterItem>(_itemTable, MasterItem.ConvertTotal)[0];
            }
            return _itemList;
        }

        private int SaveInventoryRequestItem(InventoryRequestItem _inventoryRequestItem)
        {
            int rows_affected = 0;
            OracleParameter[] param = new OracleParameter[31];
            (param[0] = new OracleParameter("p_itri_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_seq_no;
            (param[1] = new OracleParameter("p_itri_line_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_line_no;
            if (_inventoryRequestItem.MasterItem != null)
            {
                (param[2] = new OracleParameter("p_itri_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequestItem.MasterItem.Mi_cd;
            }
            else
            {
                (param[2] = new OracleParameter("p_itri_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_mitm_cd;
            }
            (param[3] = new OracleParameter("p_itri_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_itm_stus;
            (param[4] = new OracleParameter("p_itri_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_qty;
            (param[5] = new OracleParameter("p_itri_unit_price", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_unit_price;
            (param[6] = new OracleParameter("p_itri_app_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_app_qty;
            (param[7] = new OracleParameter("p_itri_res_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_res_no;
            (param[8] = new OracleParameter("p_itri_note", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_note;
            (param[9] = new OracleParameter("p_itri_mitm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_mitm_cd;
            (param[10] = new OracleParameter("p_itri_mitm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_mitm_stus;
            (param[11] = new OracleParameter("p_itri_mqty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_mqty;
            (param[12] = new OracleParameter("p_itri_bqty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_bqty;
            (param[13] = new OracleParameter("p_itri_itm_cond", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequestItem.ITRI_ITM_COND;
            (param[14] = new OracleParameter("P_itri_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_job_no;//Add by Suneth 20-11-2014
            (param[15] = new OracleParameter("P_itri_job_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_job_line;//Add by Suneth 20-11-2014
            (param[16] = new OracleParameter("P_itri_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_com;
            (param[17] = new OracleParameter("P_itri_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_loc;
            (param[18] = new OracleParameter("P_itri_po_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_po_qty;
            (param[19] = new OracleParameter("P_itri_iss_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_issue_qty;
            (param[20] = new OracleParameter("P_itri_res_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_res_qty;
            (param[21] = new OracleParameter("P_itri_res_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_res_line;
            (param[22] = new OracleParameter("P_itri_cncl_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_cncl_qty;
            (param[23] = new OracleParameter("P_itri_shop_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_shop_qty;
            (param[24] = new OracleParameter("P_itri_fd_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_fd_qty;
            (param[25] = new OracleParameter("P_itri_git_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_git_qty;
            (param[26] = new OracleParameter("P_itri_buffer", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_buffer;
            (param[27] = new OracleParameter("P_itri_advan_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_advan_qty;
            (param[28] = new OracleParameter("P_itri_base_req_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_base_req_no;//Add by Rukshan 19-12-2015
            (param[29] = new OracleParameter("P_itri_base_req_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_base_req_line;//Add by Rukshan 19-12-2015
            param[30] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            rows_affected = UpdateData("sp_updateinventoryrequestitem", CommandType.StoredProcedure, param);
            return rows_affected;
        }

        public DataTable GetReserverItemQty(string COM, string LOC, string ITM, string STATUS, string USER, decimal QTY)
        {
            //SUBODANA
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = COM;
            (param[1] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = LOC;
            (param[2] = new OracleParameter("P_ITM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = ITM;
            (param[3] = new OracleParameter("P_STUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = STATUS;
            (param[4] = new OracleParameter("P_USR", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = USER;
            (param[5] = new OracleParameter("P_QTY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = QTY;
            param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = GetData("tbl", CommandType.StoredProcedure,  "SP_CHECKRESQTY", param);
            return _dtResults;
        }

        public Int32 ROLLBACKLocationRes(string _company, string _location, string _item, string _stus, string _user, decimal _qty)
        {
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _location;
            (param[2] = new OracleParameter("p_itm", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[3] = new OracleParameter("p_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _stus;
            (param[4] = new OracleParameter("p_usr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            (param[5] = new OracleParameter("p_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _qty;
            param[6] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return UpdateData("pkg_inventory.SP_ROLLBACKLOCRES", CommandType.StoredProcedure, param);
        }

        private List<InventoryRequest> GET_INT_REQ_DATA_NEW(InventoryRequest _obj)
        {
            List<InventoryRequest> _list = new List<InventoryRequest>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_itr_req_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.Itr_req_no;
            (param[1] = new OracleParameter("p_itr_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.Itr_com;
            (param[2] = new OracleParameter("p_itr_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.Itr_tp;
            (param[3] = new OracleParameter("p_itr_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.Itr_stus;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = GetData("int_req", CommandType.StoredProcedure, "sp_get_int_req_data", param);
            if (_dtResults.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<InventoryRequest>(_dtResults, InventoryRequest.ConverterNew);
            }
            return _list;
        }

        public List<INR_RES> GET_INR_RES_DATA(INR_RES _obj)
        {
            List<INR_RES> _list = new List<INR_RES>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_irs_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.IRS_COM;
            (param[1] = new OracleParameter("p_irs_res_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.IRS_RES_NO;
            (param[2] = new OracleParameter("p_irs_anal_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.IRS_ANAL_2;
            (param[3] = new OracleParameter("p_irs_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.IRS_STUS;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = GetData("int_req", CommandType.StoredProcedure, "sp_get_inr_res_data", param);
            if (_dtResults.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<INR_RES>(_dtResults, INR_RES.ConverterNew);
            }
            return _list;
        }

        private List<INR_RES_DET> GET_INR_RES_DET_DATA(INR_RES_DET _resDet)
        {
            List<INR_RES_DET> _list = new List<INR_RES_DET>();
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_ird_res_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _resDet.IRD_RES_NO;
            (param[1] = new OracleParameter("p_ird_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _resDet.IRD_ITM_CD;
            (param[2] = new OracleParameter("p_ird_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _resDet.IRD_ITM_STUS;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = GetData("int_req_itm", CommandType.StoredProcedure, "sp_get_inr_res_det_data", param);
            if (_dtResults.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<INR_RES_DET>(_dtResults, INR_RES_DET.ConverterNew);
            }
            return _list;
        }

        private List<INR_RES_LOG> GET_INR_RES_LOG_DATA_NEW(INR_RES_LOG _resLog)
        {
            List<INR_RES_LOG> _list = new List<INR_RES_LOG>();
            OracleParameter[] param = new OracleParameter[9];
            (param[0] = new OracleParameter("p_irl_res_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _resLog.IRL_RES_NO;
            (param[1] = new OracleParameter("p_irl_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _resLog.IRL_ITM_CD;
            (param[2] = new OracleParameter("p_irl_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _resLog.IRL_ITM_STUS;
            (param[3] = new OracleParameter("p_irl_curt_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _resLog.IRL_CURT_COM;
            (param[4] = new OracleParameter("p_irl_curt_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _resLog.IRL_CURT_LOC;
            (param[5] = new OracleParameter("p_irl_curt_doc_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _resLog.IRL_CURT_DOC_TP;
            (param[6] = new OracleParameter("p_irl_curt_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _resLog.IRL_CURT_DOC_NO;
            (param[7] = new OracleParameter("p_irl_act", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _resLog.IRL_ACT;
            param[8] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _tblBalance = GetData("inr_res", CommandType.StoredProcedure, "sp_get_inr_res_log_data_new", param);
            if (_tblBalance.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<INR_RES_LOG>(_tblBalance, INR_RES_LOG.ConvertAllData);
            }
            return _list;
        }

        private Int32 InrResLogDataUpdateIssue(INR_RES_LOG _obj)
        {
            int _eff = 0;
            OracleParameter[] param = new OracleParameter[16];
            (param[0] = new OracleParameter("p_irl_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _obj.IRL_SEQ;
            (param[1] = new OracleParameter("p_irl_res_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.IRL_RES_NO;
            (param[2] = new OracleParameter("p_irl_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.IRL_ITM_CD;
            (param[3] = new OracleParameter("p_irl_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.IRL_ITM_STUS;
            (param[4] = new OracleParameter("p_irl_res_iqty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _obj.IRL_RES_IQTY;
            (param[5] = new OracleParameter("p_irl_curt_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.IRL_CURT_COM;
            (param[6] = new OracleParameter("p_irl_curt_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.IRL_CURT_LOC;
            (param[7] = new OracleParameter("p_irl_curt_doc_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.IRL_CURT_DOC_TP;
            (param[8] = new OracleParameter("p_irl_curt_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.IRL_CURT_DOC_NO;
            (param[9] = new OracleParameter("p_irl_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _obj.IRL_ACT;
            (param[10] = new OracleParameter("p_irl_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.IRL_MOD_BY_NEW;
            (param[11] = new OracleParameter("p_irl_mod_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _obj.IRL_MOD_DT;
            (param[12] = new OracleParameter("p_irl_mod_session", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.IRL_MOD_SESSION;
            (param[13] = new OracleParameter("p_IRL_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _obj.IRL_LINE;
            (param[14] = new OracleParameter("p_irl_res_wp", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _obj.IRL_RES_WP;
            param[15] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            _eff = (Int32)UpdateData("sp_upd_inr_res_log_issue_new", CommandType.StoredProcedure, param);
            return _eff;
        }

        private Int32 InrResLogDataSave(INR_RES_LOG _obj)
        {
            int _eff = 0;
            OracleParameter[] param = new OracleParameter[33];
            (param[0] = new OracleParameter("p_irl_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _obj.IRL_SEQ;
            (param[1] = new OracleParameter("p_irl_res_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.IRL_RES_NO;
            (param[2] = new OracleParameter("p_irl_res_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _obj.IRL_RES_LINE;
            (param[3] = new OracleParameter("p_irl_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _obj.IRL_LINE;
            (param[4] = new OracleParameter("p_irl_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.IRL_ITM_CD;
            (param[5] = new OracleParameter("p_irl_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.IRL_ITM_STUS;
            (param[6] = new OracleParameter("p_irl_res_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _obj.IRL_RES_QTY;
            (param[7] = new OracleParameter("p_irl_res_bqty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _obj.IRL_RES_BQTY;
            (param[8] = new OracleParameter("p_irl_res_iqty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _obj.IRL_RES_IQTY;
            (param[9] = new OracleParameter("p_irl_orig_doc_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.IRL_ORIG_DOC_TP;
            (param[10] = new OracleParameter("p_irl_orig_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.IRL_ORIG_DOC_NO;
            (param[11] = new OracleParameter("p_irl_orig_doc_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _obj.IRL_ORIG_DOC_DT;
            (param[12] = new OracleParameter("p_irl_orig_itm_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _obj.IRL_ORIG_ITM_LINE;
            (param[13] = new OracleParameter("p_irl_orig_batch_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _obj.IRL_ORIG_BATCH_LINE;
            (param[14] = new OracleParameter("p_irl_orig_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.IRL_ORIG_COM;
            (param[15] = new OracleParameter("p_irl_orig_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.IRL_ORIG_LOC;
            (param[16] = new OracleParameter("p_irl_curt_doc_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.IRL_CURT_DOC_TP;
            (param[17] = new OracleParameter("p_irl_curt_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.IRL_CURT_DOC_NO;
            (param[18] = new OracleParameter("p_irl_curt_doc_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _obj.IRL_CURT_DOC_DT;
            (param[19] = new OracleParameter("p_irl_curt_itm_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _obj.IRL_CURT_ITM_LINE;
            (param[20] = new OracleParameter("p_irl_curt_batch_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _obj.IRL_CURT_BATCH_LINE;
            (param[21] = new OracleParameter("p_irl_curt_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.IRL_CURT_COM;
            (param[22] = new OracleParameter("p_irl_curt_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.IRL_CURT_LOC;
            (param[23] = new OracleParameter("p_irl_base_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _obj.IRL_BASE_LINE;
            (param[24] = new OracleParameter("p_irl_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _obj.IRL_ACT;
            (param[25] = new OracleParameter("p_irl_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.IRL_CRE_BY;
            (param[26] = new OracleParameter("p_irl_cre_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _obj.IRL_CRE_DT;
            (param[27] = new OracleParameter("p_irl_cre_session", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.IRL_CRE_SESSION;
            (param[28] = new OracleParameter("p_irl_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.IRL_MOD_BY_NEW;
            (param[29] = new OracleParameter("p_irl_mod_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _obj.IRL_MOD_DT;
            (param[30] = new OracleParameter("p_irl_mod_session", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.IRL_MOD_SESSION;
            (param[31] = new OracleParameter("p_irl_res_wp", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _obj.IRL_RES_WP;
            param[32] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            _eff = (Int32)UpdateData("sp_save_inr_inr_res_log", CommandType.StoredProcedure, param);
            return _eff;
        }

        private Int32 ReservationLogDataUpdateCancel(INR_RES_LOG _obj, Int32 _updateTp, out string _err)
        {
            _err = string.Empty;
            Int32 _res = 0;
            List<INR_RES_LOG> _resLogAvaData = new List<INR_RES_LOG>();
            INR_RES_LOG _resLogSave = new INR_RES_LOG();
            INR_RES _resHdr = new INR_RES();
            INR_RES_DET _resDet = new INR_RES_DET();
            bool _resDetDataAva = false;
            bool _resLogDataAva = false;
            try
            {
                #region collect data 1
                _resHdr = GET_INR_RES_DATA(new INR_RES() { IRS_RES_NO = _obj.IRL_RES_NO, IRS_COM = _obj.IRL_CURT_COM }).FirstOrDefault();
                _resDet = GET_INR_RES_DET_DATA(new INR_RES_DET()
                    {
                        IRD_RES_NO = _obj.IRL_RES_NO,
                        IRD_ITM_CD = _obj.IRL_ITM_CD,
                        IRD_ITM_STUS = _obj.IRL_ITM_STUS
                    }).FirstOrDefault();

                if (_resDet != null)
                {
                    _resDetDataAva = true;
                }
                if (!_resDetDataAva)
                {
                    _res = -1;
                    _err = "Reservation detail data not found !";
                    return _res;
                }
                #endregion
                if (_updateTp == 0)//Insert
                {
                    #region collect data 2
                    INR_RES_LOG _resObj = new INR_RES_LOG();
                    _resObj.IRL_RES_NO = _obj.IRL_RES_NO;
                    _resObj.IRL_ITM_CD = _obj.IRL_ITM_CD;
                    _resObj.IRL_ITM_STUS = _obj.IRL_ITM_STUS;
                    _resObj.IRL_CURT_COM = _obj.IRL_CURT_COM;
                    _resObj.IRL_CURT_LOC = _obj.IRL_CURT_LOC;
                    _resObj.IRL_CURT_DOC_NO = _obj.IRL_ORIG_DOC_NO;
                    _resObj.IRL_CURT_DOC_TP = _obj.IRL_ORIG_DOC_TP;
                    _resObj.IRL_ACT = 1;
                    if (_obj.IRL_CURT_DOC_TP == "AOD-IN")
                    {
                        _resObj.IRL_CURT_COM = "GIT";
                        _resObj.IRL_CURT_LOC = "GIT";
                    }
                    _resLogAvaData = GET_INR_RES_LOG_DATA_NEW(_resObj);

                    if (_resLogAvaData != null)
                    {
                        if (_resLogAvaData.Count > 0)
                        {
                            _resLogDataAva = true;
                        }
                    }
                    if (!_resLogDataAva)
                    {
                        _res = -1;
                        _err = "Reservation log data not found !";
                        return _res;
                    }
                    #endregion
                    if (_resLogAvaData.Count > 0)
                    {
                        #region update temp bal column
                        INR_RES_LOG _tmpLogRes = new INR_RES_LOG();
                        foreach (var _tmpResData in _resLogAvaData)
                        {
                            _tmpLogRes = INR_RES_LOG.CreateNewObject(_tmpResData);
                            _tmpResData.TMP_IRL_RES_BQTY = _tmpResData.IRL_RES_BQTY;
                        }
                        #endregion
                        _resLogAvaData = _resLogAvaData.OrderBy(c => c.IRL_CURT_DOC_DT).ToList();
                        //Update process
                        while (_obj.IRL_RES_QTY > 0)
                        {
                            decimal _reAvaCount = 0;
                            if (_resLogAvaData.Count > 0)
                            {
                                _reAvaCount = _resLogAvaData.Sum(c => c.IRL_RES_BQTY);
                            }
                            if (_reAvaCount <= 0)
                            {
                                break;
                            }

                            foreach (var _resAva in _resLogAvaData)
                            {
                                if (_obj.IRL_RES_QTY > 0)
                                {
                                    if (_resAva.IRL_RES_BQTY > 0)
                                    {
                                        //if (_resAva.TMP_IRL_RES_BQTY >= _obj.IRL_RES_QTY)
                                        if (_obj.IRL_RES_QTY <= _resAva.IRL_RES_BQTY)
                                        {
                                            #region if available balance equal or grater
                                            #region update
                                            _resLogSave = new INR_RES_LOG();
                                            _resLogSave.IRL_RES_NO = _resAva.IRL_RES_NO;
                                            _resLogSave.IRL_CURT_COM = _resAva.IRL_CURT_COM;
                                            _resLogSave.IRL_CURT_LOC = _resAva.IRL_CURT_LOC;
                                            _resLogSave.IRL_CURT_DOC_TP = _resAva.IRL_CURT_DOC_TP;
                                            _resLogSave.IRL_CURT_DOC_NO = _resAva.IRL_CURT_DOC_NO;
                                            _resLogSave.IRL_ITM_CD = _resAva.IRL_ITM_CD;
                                            _resLogSave.IRL_ITM_STUS = _resAva.IRL_ITM_STUS;
                                            _resLogSave.IRL_MOD_BY_NEW = _obj.IRL_MOD_BY_NEW;
                                            _resLogSave.IRL_MOD_DT = _obj.IRL_MOD_DT;
                                            _resLogSave.IRL_MOD_SESSION = _obj.IRL_MOD_SESSION;
                                            _resLogSave.IRL_LINE = _resAva.IRL_LINE;


                                            _resLogSave.IRL_RES_IQTY = _obj.IRL_RES_QTY;
                                            _resAva.IRL_RES_BQTY = _resAva.IRL_RES_BQTY - _obj.IRL_RES_QTY;
                                            //_resAva.TMP_IRL_RES_BQTY = _resAva.TMP_IRL_RES_BQTY - _obj.IRL_RES_QTY;
                                            _resLogSave.IRL_ACT = _resAva.IRL_RES_BQTY <= 0 ? 0 : 1;
                                            _res = InrResLogDataUpdateIssue(_resLogSave);
                                            #endregion
                                            #region save
                                            _resLogSave.IRL_ORIG_COM = _resAva.IRL_CURT_COM;
                                            _resLogSave.IRL_ORIG_LOC = _resAva.IRL_CURT_LOC;

                                            _resLogSave.IRL_SEQ = _resHdr.IRS_SEQ;
                                            _resLogSave.IRL_LINE = _resDet.IRD_LINE;
                                            _resLogSave.IRL_RES_LINE = _resDet.IRD_LINE;
                                            _resLogSave.IRL_RES_QTY = _obj.IRL_RES_QTY;
                                            _resLogSave.IRL_RES_BQTY = _obj.IRL_RES_QTY;
                                            _resLogSave.IRL_RES_IQTY = 0;
                                            _resLogSave.IRL_CRE_BY = _obj.IRL_CRE_BY;
                                            _resLogSave.IRL_CRE_DT = _obj.IRL_CRE_DT;
                                            _resLogSave.IRL_CRE_SESSION = _obj.IRL_CRE_SESSION;
                                            _resLogSave.IRL_ACT = _resLogSave.IRL_RES_BQTY <= 0 ? 0 : 1;

                                            _resLogSave.IRL_ORIG_DOC_NO = _resAva.IRL_CURT_DOC_NO;
                                            _resLogSave.IRL_ORIG_DOC_TP = _resAva.IRL_CURT_DOC_TP;
                                            _resLogSave.IRL_ORIG_DOC_DT = _resAva.IRL_CURT_DOC_DT;

                                            _resLogSave.IRL_CURT_DOC_NO = _obj.IRL_CURT_DOC_NO;
                                            _resLogSave.IRL_CURT_DOC_TP = _obj.IRL_CURT_DOC_TP;
                                            _resLogSave.IRL_CURT_DOC_DT = _obj.IRL_CURT_DOC_DT;
                                            _resLogSave.IRL_RES_WP = _obj.IRL_RES_WP;
                                            if (_resLogSave.IRL_CURT_DOC_TP == "AOD-OUT")
                                            {
                                                _resLogSave.IRL_CURT_COM = "GIT";
                                                _resLogSave.IRL_CURT_LOC = "GIT";
                                                _resLogSave.IRL_RES_WP = 1;
                                            }
                                            if (_resLogSave.IRL_CURT_DOC_TP == "AOD-IN")
                                            {
                                                _resLogSave.IRL_CURT_COM = _obj.TMP_AOD_IN_COM;
                                                _resLogSave.IRL_CURT_LOC = _obj.TMP_AOD_IN_LOC;
                                                _resLogSave.IRL_RES_WP = 0;
                                            }
                                            if (_resLogSave.IRL_CURT_DOC_TP == "DO")
                                            {
                                                _resLogSave.IRL_RES_BQTY = 0;
                                                _resLogSave.IRL_RES_IQTY = _resLogSave.IRL_RES_QTY;
                                                _resLogSave.IRL_ACT = 0;
                                            }
                                            if (_resLogSave.IRL_CURT_DOC_TP != "INV")
                                            {
                                                _resLogSave.IRL_RES_WP = 1;
                                            }
                                            _resLogSave.IRL_BASE_LINE = _resAva.IRL_LINE;
                                            _res = InrResLogDataSave(_resLogSave);
                                            #endregion
                                            _obj.IRL_RES_QTY = _obj.IRL_RES_QTY - _obj.IRL_RES_QTY;
                                            #endregion
                                        }
                                        else
                                        {
                                            #region if available balance  less
                                            _resLogSave = new INR_RES_LOG();
                                            _resLogSave.IRL_RES_NO = _resAva.IRL_RES_NO;
                                            _resLogSave.IRL_CURT_COM = _resAva.IRL_CURT_COM;
                                            _resLogSave.IRL_CURT_LOC = _resAva.IRL_CURT_LOC;
                                            _resLogSave.IRL_CURT_DOC_TP = _resAva.IRL_CURT_DOC_TP;
                                            _resLogSave.IRL_CURT_DOC_NO = _resAva.IRL_CURT_DOC_NO;
                                            _resLogSave.IRL_ITM_CD = _resAva.IRL_ITM_CD;
                                            _resLogSave.IRL_ITM_STUS = _resAva.IRL_ITM_STUS;
                                            _resLogSave.IRL_RES_IQTY = _resAva.IRL_RES_BQTY;
                                            _resAva.IRL_RES_BQTY = 0;
                                            // _resAva.TMP_IRL_RES_BQTY = 0;
                                            _resLogSave.IRL_ACT = 0;
                                            _resLogSave.IRL_MOD_BY_NEW = _obj.IRL_MOD_BY_NEW;
                                            _resLogSave.IRL_MOD_DT = _obj.IRL_MOD_DT;
                                            _resLogSave.IRL_MOD_SESSION = _obj.IRL_MOD_SESSION;
                                            _resLogSave.IRL_LINE = _resAva.IRL_LINE;
                                            _res = InrResLogDataUpdateIssue(_resLogSave);

                                            _resLogSave.IRL_ORIG_COM = _resAva.IRL_CURT_COM;
                                            _resLogSave.IRL_ORIG_LOC = _resAva.IRL_CURT_LOC;
                                            _resLogSave.IRL_SEQ = _resHdr.IRS_SEQ;
                                            _resLogSave.IRL_LINE = _resDet.IRD_LINE;
                                            _resLogSave.IRL_RES_LINE = _resDet.IRD_LINE;
                                            _resLogSave.IRL_RES_QTY = _resLogSave.IRL_RES_IQTY;
                                            _resLogSave.IRL_RES_BQTY = _resLogSave.IRL_RES_IQTY;
                                            _resLogSave.IRL_RES_IQTY = 0;
                                            _resLogSave.IRL_CRE_BY = _obj.IRL_CRE_BY;
                                            _resLogSave.IRL_CRE_DT = _obj.IRL_CRE_DT;
                                            _resLogSave.IRL_CRE_SESSION = _obj.IRL_CRE_SESSION;
                                            _resLogSave.IRL_ACT = 1;

                                            _resLogSave.IRL_ORIG_DOC_NO = _resAva.IRL_CURT_DOC_NO;
                                            _resLogSave.IRL_ORIG_DOC_TP = _resAva.IRL_CURT_DOC_TP;
                                            _resLogSave.IRL_ORIG_DOC_DT = _resAva.IRL_CURT_DOC_DT;

                                            _resLogSave.IRL_CURT_DOC_NO = _obj.IRL_CURT_DOC_NO;
                                            _resLogSave.IRL_CURT_DOC_TP = _obj.IRL_CURT_DOC_TP;
                                            _resLogSave.IRL_CURT_DOC_DT = _obj.IRL_CURT_DOC_DT;
                                            _resLogSave.IRL_RES_WP = _obj.IRL_RES_WP;
                                            if (_resLogSave.IRL_CURT_DOC_TP == "AOD-OUT")
                                            {
                                                _resLogSave.IRL_CURT_COM = "GIT";
                                                _resLogSave.IRL_CURT_LOC = "GIT";
                                                _resLogSave.IRL_RES_WP = 1;
                                            }
                                            if (_resLogSave.IRL_CURT_DOC_TP == "AOD-IN")
                                            {
                                                _resLogSave.IRL_CURT_COM = _obj.TMP_AOD_IN_COM;
                                                _resLogSave.IRL_CURT_LOC = _obj.TMP_AOD_IN_LOC;
                                                _resLogSave.IRL_RES_WP = 0;
                                            }
                                            if (_resLogSave.IRL_CURT_DOC_TP == "INV")
                                            {
                                                _resLogSave.IRL_CURT_COM = _obj.TMP_AOD_IN_COM;
                                                _resLogSave.IRL_CURT_LOC = _obj.TMP_AOD_IN_LOC;
                                                _resLogSave.IRL_RES_WP = 0;
                                            }
                                            if (_resLogSave.IRL_CURT_DOC_TP == "DO")
                                            {
                                                _resLogSave.IRL_RES_BQTY = 0;
                                                _resLogSave.IRL_RES_IQTY = _resLogSave.IRL_RES_QTY;
                                                _resLogSave.IRL_ACT = 0;
                                            }
                                            if (_resLogSave.IRL_CURT_DOC_TP != "INV")
                                            {
                                                _resLogSave.IRL_RES_WP = 1;
                                            }
                                            _resLogSave.IRL_BASE_LINE = _resAva.IRL_LINE;
                                            _res = InrResLogDataSave(_resLogSave);
                                            _obj.IRL_RES_QTY = _obj.IRL_RES_QTY - _resLogSave.IRL_RES_QTY;
                                            if (_obj.IRL_RES_QTY == 0)
                                            {
                                                continue;
                                            }
                                            #endregion
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _err = ex.Message.ToString();
                _res = -1;
            }
            return _res;
        }

        public void StartCancellationProcess()
        {
            try
            {
                using (_connection = new OracleConnection(_connectionString))
                {
                    _connection.Open(); // open connection

                    MstModuleConf _configs = InitializeJobs(_AgentName); // get agent schedule details
                    if (_configs != null)
                    {
                        if (IsScheduleValidate(_configs.Mmc_Last_Run_Dt, _configs.Mmc_Run_Time_Unit, _configs.Mmc_Run_Time))
                        {
                            List<string> _compayList = new List<string>();
                            _compayList = GetAgentRunnigCompany();//get agent running company details from app.config file
                            if (_compayList.Count > 0)
                            {
                                foreach (string _company in _compayList)
                                {
                                    List<MstAlertCriteria> _criteriaList = new List<MstAlertCriteria>();
                                    _criteriaList = GetJobScheduleCriterias(_company, _AgentName); // get agent running criterea. agent can be run location base or company
                                    if (_criteriaList.Count > 0)
                                    {
                                        foreach (MstAlertCriteria _criteria in _criteriaList)
                                        {
                                            List<InventoryRequest> _request = new List<InventoryRequest>();
                                            _request = GetMrnaAndSoaDetails(_company, _criteria.Alc_Criteria_Type, _criteria.Alc_Code, _criteria.Alc_Late_Noof_Dt);
                                            if (_request.Count > 0)
                                            {
                                                CancelSoaAndMrna(_request);
                                            }
                                        }
                                    }
                                    else// if criterea not found agent will run company base, date setting will take from mst_module_conf
                                    {
                                        List<InventoryRequest> _request = new List<InventoryRequest>();
                                        _request = GetMrnaAndSoaDetails(_company, "COM", _company, _configs.Mmc_Minimum_Send_Dt);
                                        if (_request.Count > 0)
                                        {
                                            CancelSoaAndMrna(_request);
                                        }
                                    }
                                }

                                //update mst_module_conf
                                UpdateLastRunDate(_AgentName);
                            }
                            else
                            {
                                throw new Exception("Job cannot be executed. Configuration code 'Company' has not defined." + Environment.NewLine + "Please check the App.Config file");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Job cannot be executed. Job schedule details not found." + Environment.NewLine + "Please check the table - mst_module_conf | mmc_mod_cd --> " + _AgentName);
                    }
                }
            }
            catch (Exception ex)
            {
                string _errorMsg = "MRNA & SOA auto Cancellation process terminated due to an error ! " + Environment.NewLine + ex.Message + Environment.NewLine + ex.Source;
                Console.WriteLine(_errorMsg);
            }
        }
        private void CancelSoaAndMrna(List<InventoryRequest> _intRequests)
        {
            if (_intRequests != null)
            {
                int _canceledDocumentCount = 0; 
                Console.WriteLine("No of MRNA & SOA found = " + _intRequests.Count.ToString());
                //using (_connection = new OracleConnection(_connectionString))
                //{
                    //_connection.Open();                    

                    foreach (InventoryRequest oHeader in _intRequests)
                    {
                        _transaction = _connection.BeginTransaction();
                        List<InventoryRequestItem> _intRequestItems = new List<InventoryRequestItem>();
                        Int32 effect = 0;

                        try
                        {
                            string _basedoc = oHeader.Itr_req_no;
                            string _baseDocRefNo = oHeader.Itr_ref;

                            _intRequestItems = GetIntRequestItems(oHeader.Itr_req_no);
                            if (_intRequestItems.Count > 0)
                            {
                                int _requestTotal = Convert.ToInt32(_intRequestItems.Sum(x => x.Itri_qty));
                                int _balanceTotal = Convert.ToInt32(_intRequestItems.Sum(x => x.Itri_bqty));

                                string _tmpStatus = "C";
                                if (_requestTotal == _balanceTotal) { _tmpStatus = "C"; }
                                else if (_requestTotal > _balanceTotal) { _tmpStatus = "F"; }
                                oHeader.Itr_cre_by = "MRNA-SOA AUTOCNL";
                                oHeader.InventoryRequestItemList = _intRequestItems;

                                //Cancel MRNA & SOA
                                effect = Update_ReqHeaderStatus(_tmpStatus, oHeader.Itr_cre_by, oHeader.Itr_com, oHeader.Itr_req_no);
                                if (effect > 0)
                                {
                                    //update related previus REQ NO
                                    effect = Update_ReqHeaderStatus("A", oHeader.Itr_cre_by, oHeader.Itr_com, oHeader.Itr_ref);
                                    if (effect > 0)
                                    {
                                        string _documentType = string.Empty;
                                        MasterAutoNumber _mastAutoNo = GetNumberSequence(oHeader.Itr_loc, oHeader.Itr_tp, out _documentType);
                                        Int32 _autoNo = GetAutoNumber(_mastAutoNo.Aut_moduleid, _mastAutoNo.Aut_direction, _mastAutoNo.Aut_start_char, _mastAutoNo.Aut_cate_tp, _mastAutoNo.Aut_cate_cd, _mastAutoNo.Aut_modify_dt, _mastAutoNo.Aut_year).Aut_number;
                                        string _documentNo = oHeader.Itr_loc + "-" + _mastAutoNo.Aut_start_char + "-" + Convert.ToString(DateTime.Now.Date.Year).Remove(0, 2) + "-" + _autoNo.ToString("00000", CultureInfo.InvariantCulture);
                                        effect = UpdateAutoNumber(_mastAutoNo);

                                        Int32 SeqNumber = 0;
                                        oHeader.Itr_req_no = _documentNo;
                                        oHeader.Itr_ref = _basedoc;
                                        oHeader.Itr_tp = _documentType;
                                        oHeader.Itr_stus = "C";
                                        SeqNumber = SaveInventoryRequest(oHeader); // Add new reord to int_req with newly updated document no

                                        foreach (InventoryRequestItem _inventoryRequestItem in oHeader.InventoryRequestItemList)
                                        {
                                            //check for alocated serials
                                            if (GetAllocatedSerialCount_byDoc(oHeader.Itr_com, _basedoc, _inventoryRequestItem.Itri_line_no) < 1) 
                                            {
                                                if (_inventoryRequestItem.Itri_bqty > 0)
                                                {
                                                    //update temppickitem
                                                    effect = UpdateSerQtyForDispatch(_basedoc, _inventoryRequestItem.Itri_itm_cd, _inventoryRequestItem.Itri_itm_stus, Convert.ToInt32(_inventoryRequestItem.Itri_bqty), _inventoryRequestItem.Itri_line_no.ToString());

                                                    effect = UpdateCancelRequestDetails(_basedoc, _inventoryRequestItem.Itri_itm_cd, _inventoryRequestItem.Itri_bqty, _inventoryRequestItem.Itri_line_no);
                                                    if (effect > 0)
                                                    {
                                                        effect = UpdateCancelRequestDetails(_basedoc, _inventoryRequestItem.Itri_itm_cd, _inventoryRequestItem.Itri_bqty, _inventoryRequestItem.Itri_base_req_line, _baseDocRefNo);
                                                        if (effect > 0)
                                                        {
                                                            MasterItem _items = new MasterItem();
                                                            _items = GetItemDetails(oHeader.Itr_com, _inventoryRequestItem.Itri_itm_cd);
                                                            if (_items != null)
                                                            {
                                                                _inventoryRequestItem.MasterItem = _items;
                                                                _inventoryRequestItem.Itri_seq_no = SeqNumber;
                                                                effect = SaveInventoryRequestItem(_inventoryRequestItem);
                                                                if (effect > 0)
                                                                {
                                                                    DataTable _reserverItems = new DataTable();
                                                                    _reserverItems = GetReserverItemQty(oHeader.Itr_com, _inventoryRequestItem.Itri_loc, _inventoryRequestItem.Itri_itm_cd, _inventoryRequestItem.Itri_itm_stus, oHeader.Itr_cre_by, _inventoryRequestItem.Itri_bqty);
                                                                    if (_reserverItems.Rows.Count > 0)
                                                                    {
                                                                        if (_inventoryRequestItem.Itri_res_no == null || _inventoryRequestItem.Itri_res_no == "N/A" || _inventoryRequestItem.Itri_res_no == "")
                                                                        {
                                                                            foreach (DataRow _row in _reserverItems.Rows)
                                                                            {
                                                                                decimal resqty = Convert.ToDecimal(_row["INL_RES_QTY"].ToString());
                                                                                decimal freeqty = Convert.ToDecimal(_row["inl_free_qty"].ToString());
                                                                                if (resqty >= Convert.ToDecimal(_inventoryRequestItem.Itri_bqty))
                                                                                {
                                                                                    //Update reserverd items
                                                                                    effect = ROLLBACKLocationRes(oHeader.Itr_com, _inventoryRequestItem.Itri_loc, _inventoryRequestItem.Itri_itm_cd, _inventoryRequestItem.Itri_itm_stus, oHeader.Itr_cre_by, _inventoryRequestItem.Itri_bqty);
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                    if (!string.IsNullOrEmpty(_inventoryRequestItem.Itri_res_no) && _inventoryRequestItem.Itri_res_no != "N/A")
                                                                    {
                                                                        InventoryRequest _intRefReqData = new InventoryRequest();
                                                                        INR_RES_LOG _baseDocCancel = new INR_RES_LOG();
                                                                        int _res = 0;
                                                                        string _errro = null;

                                                                        _baseDocCancel.IRL_CURT_COM = oHeader.Itr_com;
                                                                        _baseDocCancel.IRL_CURT_LOC = oHeader.Itr_issue_from;
                                                                        _baseDocCancel.IRL_ORIG_DOC_TP = oHeader.Tmp_res_base_doc_tp;
                                                                        _baseDocCancel.IRL_ORIG_DOC_NO = oHeader.Tmp_res_base_doc_no;
                                                                        _baseDocCancel.IRL_ORIG_DOC_DT = DateTime.Now;
                                                                        _baseDocCancel.IRL_CURT_DOC_TP = oHeader.Tmp_res_base_doc_tp + "_CANCL";
                                                                        _baseDocCancel.IRL_CURT_DOC_NO = oHeader.Tmp_res_base_doc_no;
                                                                        _baseDocCancel.IRL_CURT_DOC_DT = oHeader.Itr_dt;
                                                                        _baseDocCancel.IRL_RES_NO = _inventoryRequestItem.Itri_res_no;
                                                                        _baseDocCancel.IRL_ITM_CD = _inventoryRequestItem.Itri_itm_cd;
                                                                        _baseDocCancel.IRL_ITM_STUS = _inventoryRequestItem.Itri_itm_stus;
                                                                        _baseDocCancel.IRL_MOD_BY_NEW = oHeader.Itr_cre_by;
                                                                        _baseDocCancel.IRL_MOD_SESSION = oHeader.Itr_session_id;
                                                                        _baseDocCancel.IRL_MOD_DT = DateTime.Now;
                                                                        _baseDocCancel.IRL_CRE_BY = oHeader.Itr_cre_by;
                                                                        _baseDocCancel.IRL_CRE_DT = DateTime.Now;
                                                                        _baseDocCancel.IRL_CRE_SESSION = oHeader.Itr_session_id;
                                                                        _baseDocCancel.IRL_RES_QTY = _inventoryRequestItem.Itri_bqty;
                                                                        _res = ReservationLogDataUpdateCancel(_baseDocCancel, 0, out _errro);
                                                                        if (_res > 0)
                                                                        {
                                                                            _intRefReqData = GET_INT_REQ_DATA_NEW(new InventoryRequest() { Itr_req_no = oHeader.Itr_ref }).FirstOrDefault();
                                                                            INR_RES_LOG _refDocSave = new INR_RES_LOG();
                                                                            _refDocSave.IRL_CURT_COM = oHeader.Itr_com;
                                                                            _refDocSave.IRL_CURT_LOC = oHeader.Itr_issue_from;
                                                                            _refDocSave.IRL_ORIG_DOC_TP = oHeader.Tmp_res_base_doc_tp + "_CANCL";
                                                                            _refDocSave.IRL_ORIG_DOC_NO = oHeader.Tmp_res_base_doc_no;
                                                                            _refDocSave.IRL_ORIG_DOC_DT = DateTime.Now;
                                                                            _refDocSave.IRL_CURT_DOC_TP = _intRefReqData.Itr_tp;
                                                                            _refDocSave.IRL_CURT_DOC_NO = _intRefReqData.Itr_req_no;
                                                                            _refDocSave.IRL_CURT_DOC_DT = _intRefReqData.Itr_dt;
                                                                            _refDocSave.IRL_RES_NO = _inventoryRequestItem.Itri_res_no;
                                                                            _refDocSave.IRL_ITM_CD = _inventoryRequestItem.Itri_itm_cd;
                                                                            _refDocSave.IRL_ITM_STUS = _inventoryRequestItem.Itri_itm_stus;
                                                                            _refDocSave.IRL_MOD_BY_NEW = oHeader.Itr_cre_by;
                                                                            _refDocSave.IRL_MOD_SESSION = oHeader.Itr_session_id;
                                                                            _refDocSave.IRL_MOD_DT = DateTime.Now;
                                                                            _refDocSave.IRL_CRE_BY = oHeader.Itr_cre_by;
                                                                            _refDocSave.IRL_CRE_DT = DateTime.Now;
                                                                            _refDocSave.IRL_CRE_SESSION = oHeader.Itr_session_id;
                                                                            _refDocSave.IRL_RES_QTY = _inventoryRequestItem.Itri_bqty;
                                                                            _res = ReservationLogDataUpdateCancel(_refDocSave, 0, out _errro);
                                                                        }
                                                                        else { _transaction.Rollback(); goto Next_Document; }
                                                                    }
                                                                }
                                                                else { _transaction.Rollback(); goto Next_Document; }
                                                            }
                                                            else { _transaction.Rollback(); goto Next_Document; }
                                                        }
                                                        else { _transaction.Rollback(); goto Next_Document; }
                                                    }
                                                    else { _transaction.Rollback(); goto Next_Document; }
                                                }
                                                else { _transaction.Rollback(); goto Next_Document; }
                                            }
                                            else { _transaction.Rollback(); goto Next_Document; }
                                        } 
                                    }
                                    else { _transaction.Rollback(); goto Next_Document; }
                                }
                                else { _transaction.Rollback(); goto Next_Document; }

                                _canceledDocumentCount += 1;
                            }
                            else { _transaction.Rollback(); goto Next_Document; }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Document number : " + oHeader.Itr_req_no + " couldn't cancel."  + Environment.NewLine  + ex.Message);
                            _transaction.Rollback();
                            throw;
                        }

                        _transaction.Commit();

                    Next_Document: ;                        
                    }                    
                //}
                Console.WriteLine("No of document canceled : " + _canceledDocumentCount.ToString());
            }
        }

    }

     public static class DataTableExtensions
    {
        public static List<T> ToGenericList<T>(this DataTable datatable, Func<DataRow, T> converter)
        {
            return (from row in datatable.AsEnumerable()
                    select converter(row)).ToList();
        }
    }

}
