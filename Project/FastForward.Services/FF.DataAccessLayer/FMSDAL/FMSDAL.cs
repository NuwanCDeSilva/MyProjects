using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;
using System.Collections;
using System.Configuration;
using FF.BusinessObjects;
using System.Transactions;

namespace FF.DataAccessLayer
{
    public class FMSDAL
    {
        public OracleConnection oConnection_FMS;
        private OracleDataAdapter oAdapter;
        private OracleTransaction oTransaction;
        string ConnectionString = "";

        public bool _isTr = false;


        public void BeginTransaction()
        {
            if (oTransaction == null)
            {
                oTransaction = oConnection_FMS.BeginTransaction();
                _isTr = true;
            }
        }


        public string GetConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["ConnFMS"].ConnectionString;
        }

        ///<Summary> Used to return Data Adapter </Summary>
        private OracleDataAdapter GetAdapter()
        {
            if (oAdapter == null)
            {
                oAdapter = new OracleDataAdapter();

            }
            return oAdapter;
        }
        ///<Summary>Used to return Data Transaction </Summary>
        private OracleTransaction GetTransaction()
        {
            if (oTransaction == null)
            {
                oTransaction = oConnection_FMS.BeginTransaction();

            }
            return oTransaction;
        }

        private OracleDbType GetDataType(Type _type)
        {
            OracleDbType _oType;
            switch (_type.ToString())
            {
                case "System.Decimal":
                    _oType = OracleDbType.Decimal;
                    break;
                case "System.Int32":
                    _oType = OracleDbType.Int32;
                    break;
                case "System.DateTime":
                    _oType = OracleDbType.Date;
                    break;
                case "System.Boolean":
                    _oType = OracleDbType.Char;
                    break;
                case "System.TypeCode.Double":
                    _oType = OracleDbType.Double;
                    break;
                case "System.Byte[]":
                    _oType = OracleDbType.Blob;
                    break;
                case "System.String":
                    _oType = OracleDbType.NVarchar2;
                    break;
                default:
                    _oType = OracleDbType.NVarchar2;
                    break;
            }
            return _oType;
        }

        private OracleCommand SetParameters(OracleCommand _commands, DataTable _table)
        {

            foreach (DataColumn col in _table.Columns)
            {
                OracleDbType oracleType = GetDataType(col.DataType);
                _commands.Parameters.Add(new OracleParameter("@" + col.ColumnName, oracleType));
                _commands.Parameters["@" + col.ColumnName].SourceColumn = col.ColumnName;

            }

            return _commands;
        }
        private void MakeCommand(OracleCommand _command, OracleConnection _connection, CommandType _commandType, string _commandText, Boolean _isTransactionBase, params  OracleParameter[] _parameters)
        {

            _command.Connection = _connection;
            _command.CommandText = _commandText;
            if (_isTr)
            {
                _command.Transaction = oTransaction;
            }
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

        //private void MakeCommand(OracleCommand _command, OracleConnection _connection, OracleTransaction _transaction, CommandType _commandType, string _commandText, Boolean _isTransactionBase, params  OracleParameter[] _parameters)
        //{

        //    _command.Connection = _connection;
        //    _command.CommandText = _commandText;



        //    _command.CommandType = _commandType;
        //    if (_parameters != null)
        //    {
        //        foreach (OracleParameter param in _parameters)
        //        {
        //            if ((param.Direction == ParameterDirection.InputOutput) && (param.Value == null))
        //            {
        //                param.Value = DBNull.Value;
        //            }

        //            _command.Parameters.Add(param);
        //        }
        //    }

        //    return;
        //}

        public FMSDAL()
        {
            try
            {
                ConnectionString = GetConnectionString();
            }
            catch (Exception ex)
            {
                //TODO: To be handle by exception class
            }

            finally
            {
                oConnection_FMS = new OracleConnection(ConnectionString);
                oAdapter = null;
                oTransaction = null;
            }


        }

        public void TransactionCommit()
        {
            if (oTransaction != null)
            {
                oTransaction.Commit();
                oTransaction.Dispose();
                oTransaction = null;
            }
        }

        public void TransactionRollback()
        {
            if (oTransaction != null)
            {
                oTransaction.Rollback();
                oTransaction.Dispose();
                oTransaction = null;
            }
        }

        public Boolean ConnectionOpen()
        {
            Boolean _isOpen = false;
            if (oConnection_FMS.State != ConnectionState.Open)
            {
                oConnection_FMS.Open();
            }

            if (oConnection_FMS.State == ConnectionState.Open)
                _isOpen = true;
            else _isOpen = false;

            _isTr = false;
            return _isOpen;
        }

        public Boolean ConnectionClose()
        {
            Boolean _isClose = false;

            if (oConnection_FMS.State == ConnectionState.Open)
            {
                oConnection_FMS.Close();
                oConnection_FMS.Dispose();
                //OracleConnection.ClearAllPools();
            }

            if (oConnection_FMS.State == ConnectionState.Closed)
                _isClose = true;
            else
                _isClose = false;

            return _isClose;
        }


        public Int32 UpdateRecords(DataTable _table, String _storedProcedure, Boolean _isSave, Boolean _isUpdate, Boolean _isDelete)
        {
            OracleCommand command = new OracleCommand();
            command = SetParameters(command, _table);
            command.CommandText = _storedProcedure;
            command.CommandType = CommandType.StoredProcedure;

            oAdapter = GetAdapter();

            if (_isSave)
            {
                oAdapter.InsertCommand = command;

            }
            else if (_isUpdate)
            {
                oAdapter.UpdateCommand = command;
            }
            else if (_isDelete)
            {
                oAdapter.DeleteCommand = command;
            }

            command.Connection = oConnection_FMS;
            oTransaction = GetTransaction();
            command.Transaction = oTransaction;

            Int32 _rowsEffect = oAdapter.Update(_table);
            command.Dispose();

            return _rowsEffect;


        }

        public DataTable QueryDataTable(String _tableName, String _storedProcedure, CommandType _commTypes, Boolean _isTransactionBase, params OracleParameter[] _parameters)
        {
            OracleCommand command = new OracleCommand();
            DataTable table = null;
            MakeCommand(command, oConnection_FMS, _commTypes, _storedProcedure, true, _parameters);
            OracleDataAdapter adapter = GetAdapter();
            table = new DataTable(_tableName);
            adapter.SelectCommand = command;
            adapter.Fill(table);
            return table;
        }


        public DataSet QueryDataSet(String _tableName, String _storedProcedure, CommandType _commTypes, Boolean _isTransactionBase, params OracleParameter[] _parameters)
        {
            OracleCommand command = new OracleCommand();
            MakeCommand(command, oConnection_FMS, _commTypes, _storedProcedure, true, _parameters);
            OracleDataAdapter adapter = GetAdapter();
            DataSet dataset = new DataSet();
            adapter.SelectCommand = command;
            adapter.Fill(dataset, _tableName);
            return dataset;

        }

        public Int32 UpdateRecords(String _storedProcedure, CommandType _commTypes, params OracleParameter[] _parameters)
        {
            //OracleCommand command = new OracleCommand();
            //Int32 effects = 0;

            //MakeCommand(command, oConnection_FMS, oTransaction, _commTypes, _storedProcedure, true, _parameters);

            //effects = command.ExecuteNonQuery();

            //if (_parameters[_parameters.Length - 1].Value == DBNull.Value)
            //    effects = 0;
            //else
            //    effects = Convert.ToInt32(_parameters[_parameters.Length - 1].Value.ToString());
            //return effects;

            OracleCommand command = new OracleCommand();
            Int32 effects = 0;

            MakeCommand(command, oConnection_FMS, _commTypes, _storedProcedure, true, _parameters);

            effects = command.ExecuteNonQuery();

            if (_parameters[_parameters.Length - 1].Value == DBNull.Value)
                effects = 0;
            else
                effects = Convert.ToInt32(_parameters[_parameters.Length - 1].Value.ToString());
            return effects;

        }


        public Int32 QueryFunction(String _storedProcedure, CommandType _commTypes, params OracleParameter[] _parameters)
        {
            OracleCommand command = new OracleCommand();
            Int32 effected = 0;

            try
            {
                //MakeCommand(command, oConnection_FMS, oTransaction, _commTypes, _storedProcedure, false, _parameters);
                MakeCommand(command, oConnection_FMS, _commTypes, _storedProcedure, true, _parameters);
                OracleParameter _return = new OracleParameter("return", OracleDbType.Int32, ParameterDirection.ReturnValue);
                command.Parameters.Add(_return);
                command.ExecuteNonQuery();

                if (_return.Value == DBNull.Value)
                    effected = 0;
                else
                    effected = Convert.ToInt32(_return.Value.ToString());

            }
            catch (Exception ex)
            {
                //TODO: To be handle by exception class
                throw new Exception(ex.Message);
            }
            finally
            {

            }
            return (Int32)effected;
        }


        public Int32 ReturnSP_SingleValue(String _storedProcedure, CommandType _commTypes, OracleParameter _outPara, params OracleParameter[] _parameters)
        {
            OracleCommand command = new OracleCommand();
            Int32 effected = 0;

            try
            {
                MakeCommand(command, oConnection_FMS, _commTypes, _storedProcedure, true, _parameters);
                OracleParameter _return = _outPara;
                command.Parameters.Add(_return);
                command.ExecuteNonQuery();

                if (_return.Value == DBNull.Value)
                    effected = 0;
                else if (_return.Value == null)
                    effected = 0;
                else
                    effected = Convert.ToInt32(_return.Value.ToString());

            }
            catch (Exception ex)
            {
                //TODO: To be handle by exception class
                throw new Exception(ex.Message);
            }
            finally
            {

            }
            return (Int32)effected;
        }



        #region Active Directory Information



        public string GetADConnectionString()
        {
            //return System.Configuration.ConfigurationManager.ConnectionStrings["LdapConn"].ConnectionString;
            //string s = System.Configuration.ConfigurationManager.ConnectionStrings["LdapConn"].ConnectionString;
            //string b = System.Configuration.ConfigurationManager.AppSettings["domainName"].ToString();
            return ConfigurationManager.ConnectionStrings["LdapConn"].ConnectionString; ;
        }

        public string GetADDomainName()
        {
            //return System.Configuration.ConfigurationManager.AppSettings["domainName"].ToString();
            return ConfigurationManager.ConnectionStrings["domainName"].ConnectionString;
        }

        public string GetADAuthenticateUser()
        {
            //return System.Configuration.ConfigurationManager.AppSettings["domainAuthenticateUser"].ToString();
            return ConfigurationManager.ConnectionStrings["domainAuthenticateUser"].ConnectionString;
        }

        public string GetADAuthenticateUserPw()
        {
            //return System.Configuration.ConfigurationManager.AppSettings["domainAuthenticateUserpw"].ToString();
            return ConfigurationManager.ConnectionStrings["domainAuthenticateUserpw"].ConnectionString;
        }

        #endregion


        public Int16 update_manual_serial(string _refNo, string _userCode, Int32 _Seq_No, string _defBin, string _Comp, string _Loc)
        {
            string _sql = string.Empty;
            string _sql1 = string.Empty;
            Int16 rows_affected = 0;
            Int16 rows_aff = 0;

            oConnection_FMS.Open();

            OracleCommand cmdUpdate = oConnection_FMS.CreateCommand();
            cmdUpdate.CommandText = "delete from temp_pick_ser where tus_doc_no=:doc_No";
            cmdUpdate.Parameters.Add("doc_No", _refNo);
            cmdUpdate.ExecuteNonQuery();

            //OracleParameter[] param1 = new OracleParameter[1];
            //(param1[0] = new OracleParameter("p_docno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _refNo;
            //rows_aff = (Int16)this.UpdateRecords("sp_del_pick_ser", CommandType.StoredProcedure, param1);

            _sql = "select * from temp_man_doc_det where MDD_REF=:refno and MDD_USER=:mdduser";
            OracleCommand cmdGetItem = new OracleCommand(_sql, oConnection_FMS);
            cmdGetItem.Parameters.Add("refno", _refNo);
            cmdGetItem.Parameters.Add("mdduser", _userCode);

            OracleDataReader drItem = cmdGetItem.ExecuteReader();
            Int32 _ItemLine = 1;

            while (drItem.Read())
            {
                Int32 _count = 0;
                Int32 _nextNo = Convert.ToInt32(drItem["MDD_FIRST"]);
                Int32 _CNT = Convert.ToInt32(drItem["MDD_CNT"]);
                Int32 _Line = Convert.ToInt32(drItem["MDD_LINE"]);

                while (_count < _CNT)
                {
                    OracleParameter[] param = new OracleParameter[39];
                    (param[0] = new OracleParameter("p_usrseq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _Seq_No;
                    (param[1] = new OracleParameter("p_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Convert.ToString(drItem["MDD_REF"]);
                    (param[2] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = 0;
                    (param[3] = new OracleParameter("p_itm_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _ItemLine;
                    (param[4] = new OracleParameter("p_batch_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _Line;   //2/5/2013
                    (param[5] = new OracleParameter("p_ser_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = 0;
                    (param[6] = new OracleParameter("p_doc_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = Convert.ToDateTime(DateTime.Now.Date).Date;
                    (param[7] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Comp;
                    (param[8] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Loc;
                    (param[9] = new OracleParameter("p_bin", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _defBin;
                    (param[10] = new OracleParameter("p_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Convert.ToString(drItem["MDD_ITM_CD"]);
                    (param[11] = new OracleParameter("p_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "GOD";
                    (param[12] = new OracleParameter("p_unit_cost", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = 0;
                    (param[13] = new OracleParameter("p_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = 1;
                    (param[14] = new OracleParameter("p_ser_id", OracleDbType.Int32, null, ParameterDirection.Input)).Value = GetSerialID();
                    (param[15] = new OracleParameter("p_ser_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _nextNo;
                    (param[16] = new OracleParameter("p_ser_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Convert.ToString(drItem["MDD_BK_NO"]);    //2/5/2013
                    (param[17] = new OracleParameter("p_ser_3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "";
                    (param[18] = new OracleParameter("p_ser_4", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Convert.ToString(drItem["mdd_prefix"]);
                    (param[19] = new OracleParameter("p_warr_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "";
                    (param[20] = new OracleParameter("p_warr_period", OracleDbType.Int32, null, ParameterDirection.Input)).Value = 0;
                    (param[21] = new OracleParameter("p_orig_grncom ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Comp;
                    (param[22] = new OracleParameter("p_orig_grnno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Convert.ToString(drItem["MDD_REF"]);
                    (param[23] = new OracleParameter("p_orig_grndt", OracleDbType.Date, null, ParameterDirection.Input)).Value = Convert.ToDateTime(DateTime.Now.Date).Date;
                    (param[24] = new OracleParameter("p_orig_supp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "";
                    (param[25] = new OracleParameter("p_exist_grncom", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "";
                    (param[26] = new OracleParameter("p_exist_grnno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "";
                    (param[27] = new OracleParameter("p_exist_grndt", OracleDbType.Date, null, ParameterDirection.Input)).Value = Convert.ToDateTime(DateTime.Now.Date).Date;
                    (param[28] = new OracleParameter("p_exist_supp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "";
                    (param[29] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "";
                    (param[30] = new OracleParameter("p_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "";
                    (param[31] = new OracleParameter("p_unit_price", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = 0;
                    (param[32] = new OracleParameter("p_new_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "";
                    (param[33] = new OracleParameter("p_base_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "";// Convert.ToString(drItem["MDD_REF"]);
                    (param[34] = new OracleParameter("p_base_itm_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = 0;// _ItemLine;  // Convert.ToInt32(drItem["MDD_LINE"]);
                    (param[35] = new OracleParameter("p_itm_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "";
                    (param[36] = new OracleParameter("p_itm_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "";
                    (param[37] = new OracleParameter("p_itm_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "";

                    param[38] = new OracleParameter("o_effect", OracleDbType.Int16, null, ParameterDirection.Output);

                    rows_affected = (Int16)this.UpdateRecords("sp_pickser", CommandType.StoredProcedure, param);
                    _nextNo = _nextNo + 1;
                    _count = _count + 1;
                }
                _ItemLine = _ItemLine + 1;
            }
            drItem.Close();
            return rows_affected;
        }

        public Int32 GetSerialID()
        {
            OracleParameter[] param = new OracleParameter[1];
            Int16 effects = 0;
            param[0] = new OracleParameter("o_serialid", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_getserialid", CommandType.StoredProcedure, param);
            return effects;
        }

        /// <summary>
        /// Written by Prabhath on 07/08/2013 to overcome the enlisted problem
        /// </summary>
        /// <param name="_tx"></param>
        public void EnlistTransaction(Transaction _tx)
        {
            oConnection_FMS.EnlistTransaction(_tx);
        }


    }

}
