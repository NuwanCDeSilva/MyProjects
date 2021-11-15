using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes; 
using System.Data;
using System.Collections;
using System.Configuration;
using FF.BusinessObjects;

namespace FF.DataAccessLayer
{
    public class PosDAL
    {

        //  Task            : POS Database Connectivity
        //  Developed By    : Chamal De Silva
        //  Competed On     : 07/Aug/2012 

        ///<summary> Database reference variable which allocate for each and every processes </summary>
        ///<remarks> The DataBase connection is set when the new object create. </remarks>
        ///<c>oConnection</c> Used to store  Sql DataBase Connection
        ///<seealso cref=" Sql.DataAccess.Client. SqlConnection"/>
        public SqlConnection sConnection;
        ///<c>oAdapter</c> Used to store  Sql DataBase Adapter Connectivity
        ///<seealso cref=" Sql.DataAccess.Client. SqlDataAdapter"/>
        private SqlDataAdapter sAdapter;
        ///<c>oRead</c> Used to control transaction between DataBase and the Entity
        ///<seealso cref=" Sql.DataAccess.Client. SqlTransaction"/>
        private SqlTransaction sTransaction;
        ///<c>ConnectionString</c> Used to Get the connection string and store it for further use
        ///<seealso cref="String"/>
        string ConnectionString = "";

        public bool _isTr = false;
        
        public void BeginTransaction()
        {
            if (sTransaction == null)
            {
                sTransaction = sConnection.BeginTransaction();
                _isTr = true;
            }
        }

        public string GetConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["ConnPOS"].ConnectionString;
        }

        ///<Summary> Used to return Data Adapter </Summary>
        private SqlDataAdapter GetAdapter()
        {
            if (sAdapter == null)
            {
                sAdapter = new SqlDataAdapter();

            }
            return sAdapter;
        }
        ///<Summary>Used to return Data Transaction </Summary>
        public SqlTransaction GetTransaction()
        {
            if (sTransaction == null)
            {
                sTransaction = sConnection.BeginTransaction();
            }
            return sTransaction;
        }

        private SqlDbType GetDataType(Type _type)
        {
            System.Data.SqlDbType returnValue;
            switch (_type.ToString())
            {
                case "System.Decimal":
                    returnValue = SqlDbType.Decimal;
                    break;
                case "System.Int32":
                    returnValue = SqlDbType.Int;
                    break;
                case "System.DateTime":
                    returnValue = SqlDbType.DateTime;
                    break;
                case "System.Boolean":
                    returnValue = SqlDbType.Bit;
                    break;
                case "System.TypeCode.Double":
                    returnValue = SqlDbType.Money;
                    break;
                case "System.Byte[]":
                    returnValue = SqlDbType.Image;
                    break;
                case "System.String":
                default:
                    returnValue = SqlDbType.VarChar;
                    break;
            }
            return returnValue;
        }

        private SqlCommand SetParameters(SqlCommand _commands, DataTable _table)
        {

            foreach (DataColumn col in _table.Columns)
            {
                SqlDbType oracleType = GetDataType(col.DataType);
                _commands.Parameters.Add(new  SqlParameter("@" + col.ColumnName, oracleType));
                _commands.Parameters["@" + col.ColumnName].SourceColumn = col.ColumnName;

            }

            return _commands;
        }

        private void MakeCommand( SqlCommand _command,  SqlConnection _connection,  SqlTransaction _transaction, CommandType _commandType, string _commandText, Boolean _isTransactionBase, params   SqlParameter[] _parameters)
        {
            _command.Connection = _connection;
            _command.CommandText = _commandText;
            if (_isTr)_command.Transaction = sTransaction;
            _command.CommandType = _commandType;
            if (_parameters != null)
            {
                foreach ( SqlParameter param in _parameters)
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

        ///<summary> Class Constructor </summary>
        ///<c>DA</c> is the Construtor for the Data Access Layer Class
        public PosDAL()
        {
  
                ConnectionString = GetConnectionString();
                sConnection = new SqlConnection(ConnectionString);
                sAdapter = null;
                sTransaction = null;
        }
        /// <summary>
        /// Used to Commit the Transaction
        /// </summary>
        public void TransactionCommit()
        {
            if (sTransaction != null)
            {
                sTransaction.Commit();
                sTransaction.Dispose();
                sTransaction = null;
            }
        }
        /// <summary>
        /// Used to Roll Back the transaction
        /// </summary>
        public void TransactionRollback()
        {
            if (sTransaction != null)
            {
                sTransaction.Rollback();
                sTransaction.Dispose();
                sTransaction = null;
            }
        }
        /// <summary>
        /// Used to establish the connectivity of the DataBase
        /// </summary>
        /// <returns> Return whether connected or not connnected (True/False) </returns>
        public Boolean ConnectionOpen()
        {
            Boolean _isOpen = false;
            if (sConnection.State != ConnectionState.Open)
            {
                sConnection.Open();
            }

            if (sConnection.State == ConnectionState.Open)
                _isOpen = true;
            else _isOpen = false;


            _isTr = false;
            return _isOpen;
        }
        /// <summary>
        /// Used to disconnect the connectivity of the DataBase
        /// </summary>
        /// <returns>Return whether disconnected or not disconnnected (True/False)</returns>
        public Boolean ConnectionClose()
        {
            Boolean _isClose = false;
            if (sConnection.State == ConnectionState.Open)
            {
                sConnection.Close();
                sConnection.Dispose();
                SqlConnection.ClearAllPools();
            }

            if (sConnection.State == ConnectionState.Closed)
                _isClose = true;
            else
                _isClose = false;

            return _isClose;

        }
      
        public DataTable QueryDataTable(String _tableName, String _storedProcedure, CommandType _commTypes, Boolean _isTransactionBase, params  SqlParameter[] _parameters)
        {
            SqlCommand command = new SqlCommand();
            DataTable table = null;
            MakeCommand(command, sConnection, sTransaction, _commTypes, _storedProcedure, _isTransactionBase, _parameters);
            SqlDataAdapter adapter = GetAdapter();
            table = new DataTable(_tableName);
            adapter.SelectCommand = command;
            adapter.Fill(table);
            return table;
        }

        ///<summary>Get Data From DataBase as a Data Set
        ///</summary>
        ///<param name="_tableName">Used to transfer Table Name.</param>
        ///<seealso cref="System.String"/>
        ///<param name="_storedProcedure">Used to transfer Stored Procedure Name.</param>
        ///<seealso cref="System.String"/>
        ///<param name="_commTypes">Used to indicate the Command Type.</param>
        ///<seealso cref="System.Data.CommandType"/>
        ///<param name="_parameters">Used to indicate the List of Parameters.</param>
        ///<seealso cref=" Sql.DataAccess.Client. SqlParameter"/>
        ///<param name="_isTransactionBase">Used to indicate which use the transaction base process.</param>
        ///<seealso cref="System.Boolean"/> 
        ///<returns>Used to return as DataSet</returns>
        public DataSet QueryDataSet(String _tableName, String _storedProcedure, CommandType _commTypes, Boolean _isTransactionBase, params  SqlParameter[] _parameters)
        {
            SqlCommand command = new SqlCommand();
            MakeCommand(command, sConnection, sTransaction, _commTypes, _storedProcedure, _isTransactionBase, _parameters);
            SqlDataAdapter adapter = GetAdapter();
            DataSet dataset = new DataSet();
            adapter.SelectCommand = command;
            adapter.Fill(dataset, _tableName);
            return dataset;

        }

        /// <summary>
        /// Update/Insert by executing command object
        /// </summary>
        /// <param name="_storedProcedure">Used to indicate stored procedure name</param>
        /// <param name="_commTypes">Used to</param>
        /// <param name="_parameters"></param>
        /// <returns></returns>
        public Int32 UpdateRecords(String _storedProcedure, CommandType _commTypes, params  SqlParameter[] _parameters)
        {
            SqlCommand command = new SqlCommand();
            Int32 effects = 0;

            MakeCommand(command, sConnection, sTransaction, _commTypes, _storedProcedure, true, _parameters);
            effects = command.ExecuteNonQuery();

            if (_parameters[_parameters.Length - 1].Value == DBNull.Value)
                effects = 0;
            else
                effects = Convert.ToInt32(_parameters[_parameters.Length - 1].Value.ToString());

            return effects;
        }

       


        //Shani 26-10-2012
        public List< SqlParameter> ReturnSP_Many_DecimalValues(String _storedProcedure, CommandType _commTypes, List< SqlParameter> _outPara, params  SqlParameter[] _parameters)
        {
             SqlCommand command = new  SqlCommand();
            // Decimal effected = 0;

            MakeCommand(command, sConnection, sTransaction, _commTypes, _storedProcedure, false, _parameters);
            // SqlParameter _return = _outPara;
            foreach ( SqlParameter _return in _outPara)
            {
                command.Parameters.Add(_return);
            }

            command.ExecuteNonQuery();
            foreach ( SqlParameter _return in _outPara)
            {
                if (_return.Value == DBNull.Value)
                    _return.Value = 0;
                else if (_return.Value == null)
                    _return.Value = 0;
                else
                    _return.Value = Convert.ToDecimal(_return.Value.ToString());

            }

            //return (Decimal)effected;
            return _outPara;
        }


        public DataTable QueryDataTable(String _tableName, String _storedProcedure, CommandType _commTypes, Boolean _isTransactionBase, int _startFrom, int _endFrom, params  SqlParameter[] _parameters)
        {
             SqlCommand command = new  SqlCommand();
            DataTable table = null;
            MakeCommand(command, sConnection, sTransaction, _commTypes, _storedProcedure, _isTransactionBase, _parameters);
             SqlDataAdapter adapter = GetAdapter();
            table = new DataTable(_tableName);
            adapter.SelectCommand = command;
            adapter.Fill(_startFrom, _endFrom, table);
            return table;
        }
    }

}
