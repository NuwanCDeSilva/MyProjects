using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTWMailAgent
{

    class DBConnection
    {
        public OracleConnection oConnEMS;

        public OracleDataAdapter oAdapter;
        public OracleTransaction oTrEMS;


        string ConnectionString = "";

        public DBConnection()
        {
            ConnectionString = GetEMSConnectionString();
            oConnEMS = new OracleConnection(ConnectionString);


            oAdapter = null;
        }


        public string GetEMSConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
        }


        public void OpenEMS()
        {
            if (oConnEMS.State != ConnectionState.Open)
            {
                oConnEMS = new OracleConnection(GetEMSConnectionString().ToString());
                oConnEMS.Open();
            }
        }

        public void EmsBegin()
        {
            if (oConnEMS.State != ConnectionState.Open)
            {
                oConnEMS = new OracleConnection(GetEMSConnectionString().ToString());
                oConnEMS.Open();
                oTrEMS = oConnEMS.BeginTransaction();
            }
        }

        public void EmsCommit()
        {
            oTrEMS.Commit();
            oConnEMS.Close();
        }

        public void EmsRollback()
        {
            oTrEMS.Rollback();
            oConnEMS.Close();
        }

        public void ConnectionCloseEMS()
        {
            if (oConnEMS.State == ConnectionState.Open)
            {
                oConnEMS.FlushCache();
                oConnEMS.Close();
            }
        }

        public Int32 UpdateRecords(OracleConnection oConnection, String _storedProcedure, CommandType _commTypes, params OracleParameter[] _parameters)
        {
            OracleCommand command = new OracleCommand();
            Int32 effects = 0;

            MakeCommand(command, oConnection, _commTypes, _storedProcedure, true, _parameters);

            effects = command.ExecuteNonQuery();

            if (_parameters[_parameters.Length - 1].Value == DBNull.Value)
                effects = 0;
            else
                effects = Convert.ToInt32(_parameters[_parameters.Length - 1].Value.ToString());

            return effects;
        }


        private void MakeCommand(OracleCommand _command, OracleConnection _connection, CommandType _commandType, string _commandText, Boolean _isTransactionBase, params  OracleParameter[] _parameters)
        {
            _command.Connection = _connection;
            _command.Transaction = oTrEMS;
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

        public DataTable QueryDataTable(OracleCommand _command, OracleConnection _connection, String _tableName, String _storedProcedure, CommandType _commTypes, Boolean _isTransactionBase, params OracleParameter[] _parameters)
        {
            OracleCommand command = new OracleCommand();
            DataTable table = null;
            MakeCommand(command, _connection, _commTypes, _storedProcedure, _isTransactionBase, _parameters);
            OracleDataAdapter adapter = GetAdapter();
            table = new DataTable(_tableName);
            adapter.SelectCommand = command;
            adapter.Fill(table);
            return table;

        }

        private OracleDataAdapter GetAdapter()
        {
            if (oAdapter == null)
            {
                oAdapter = new OracleDataAdapter();

            }
            return oAdapter;
        }

        public Int32 UpdateRecordsSqlSvr(SqlConnection oConnection, String _storedProcedure, CommandType _commTypes, params SqlParameter[] _parameters)
        {
            SqlCommand command = new SqlCommand();
            Int32 effects = 0;

            MakeCommandSqlSvr(command, oConnection, _commTypes, _storedProcedure, true, _parameters);

            effects = command.ExecuteNonQuery();

            //if (_parameters[_parameters.Length - 1].Value == DBNull.Value)
            //    effects = 0;
            //else
            //    effects = Convert.ToInt32(_parameters[_parameters.Length - 1].Value.ToString());

            return effects;
        }

        private void MakeCommandSqlSvr(SqlCommand _command, SqlConnection _connection, CommandType _commandType, string _commandText, Boolean _isTransactionBase, params  SqlParameter[] _parameters)
        {
            _command.Connection = _connection;
            _command.CommandText = _commandText;
            _command.CommandType = _commandType;
            if (_parameters != null)
            {
                foreach (SqlParameter param in _parameters)
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

        public DataTable QueryDataTable(String _tableName, String _storedProcedure, CommandType _commTypes, Boolean _isTransactionBase, params OracleParameter[] _parameters)
        {
            OracleCommand command = new OracleCommand();
            DataTable table = null;
            MakeCommand(command, oConnEMS, oTrEMS, _commTypes, _storedProcedure, _isTransactionBase, _parameters);
            OracleDataAdapter adapter = GetAdapter();
            table = new DataTable(_tableName);
            adapter.SelectCommand = command;
            adapter.Fill(table);
            return table;
        }
        private void MakeCommand(OracleCommand _command, OracleConnection _connection, OracleTransaction _transaction, CommandType _commandType, string _commandText, Boolean _isTransactionBase, params  OracleParameter[] _parameters)
        {

            _command.Connection = _connection;
            _command.Transaction = oTrEMS;
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
        /// <summary>
        /// Update/Insert by executing command object
        /// </summary>
        /// <param name="_storedProcedure">Used to indicate stored procedure name</param>
        /// <param name="_commTypes">Used to</param>
        /// <param name="_parameters"></param>
        /// <returns></returns>
        public Int32 UpdateRecords(String _storedProcedure, CommandType _commTypes, params OracleParameter[] _parameters)
        {
            OracleCommand command = new OracleCommand();
            Int32 effects = 0;

            MakeCommand(command, oConnEMS, oTrEMS, _commTypes, _storedProcedure, true, _parameters);
            effects = command.ExecuteNonQuery();

            if (_parameters[_parameters.Length - 1].Value == DBNull.Value)
                effects = 0;
            else
                effects = Convert.ToInt32(_parameters[_parameters.Length - 1].Value.ToString());

            return effects;
        }
    }

}
