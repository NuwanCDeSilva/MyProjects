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
using FF.BusinessObjects.Asycuda;

namespace FF.DataAccessLayer
{
    public class AsyBaseDAL : BaseDAL
    {

        //  Task            : Data Access Layer for the Web Base Asycuda system
        //  Developed By    : Nuwan De Silva
        //  Competed On     : 18/12/2015 

        public ASY_DB_SOURCE DataBaseSource = null;
        ///<summary> Database reference variable which allocate for each and every processes </summary>
        ///<remarks> The DataBase connection is set when the new object create. </remarks>
        ///<c>oASyConnection</c> Used to store Oracle DataBase Connection
        ///<seealso cref="Oracle.DataAccess.Client.OracleConnection"/>
        public OracleConnection oASyConnection;
        ///<c>oAsyAdapter</c> Used to store Oracle DataBase Adapter Connectivity
        ///<seealso cref="Oracle.DataAccess.Client.OracleDataAdapter"/>
        public OracleDataAdapter oAsyAdapter;
        ///<c>oRead</c> Used to control transaction between DataBase and the Entity
        ///<seealso cref="Oracle.DataAccess.Client.OracleTransaction"/>
        public OracleTransaction oAsyTransaction;
        ///<c>ConnectionString</c> Used to Get the connection string and store it for further use
        ///<seealso cref="String"/>
        string ConnectionString = "";

        public bool _isAsyTr = false;

        public void BeginCommonTransaction()
        {
            if (DataBaseSource != null)
            {
                switch (DataBaseSource.Add_db_tp) { 
                    case  "ORACLE" :
                        if (oAsyTransaction == null)
                        {
                            oAsyTransaction = oASyConnection.BeginTransaction();
                            _isAsyTr = true;
                        }
                        break;
                    default:
                        break;
                }
                     

            }
            
        }

        public string GetCommonConnectionString()
        {
            if (DataBaseSource != null)
            {
                return DataBaseSource.Add_db_str;
            }
            else {
                return null;
            }
           // return System.Configuration.ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
        }

        public string GetConnectionStringDR()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["ConnDR"].ConnectionString;
        }

        //<add name="DR_Enable" connectionString="0"/>
        public string GetDRConnectionStatus()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DR_Enable"].ConnectionString;
        }

        ///<Summary> Used to return Data Adapter </Summary>
        private OracleDataAdapter GetCommonAdapter()
        {
            if (oAsyAdapter == null)
            {
                oAsyAdapter = new OracleDataAdapter();

            }
            return oAsyAdapter;
        }
        ///<Summary>Used to return Data Transaction </Summary>
        public OracleTransaction GetCommonTransaction()
        {
            if (oAsyTransaction == null)
            {
                oAsyTransaction = oASyConnection.BeginTransaction();
            }
            return oAsyTransaction;
        }
        ///<Summary>Used to convert .Net Types to Oracle DataBase Data Types </Summary>
        ///<param name="_type">Used to indicate data type.</param>
        ///<seealso cref="Oracle.DataAccess.Client.OracleDbType"/>
        ///<seealso cref="System.Type"/>
        private OracleDbType GetCommonDataType(Type _type)
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
        ///<Summary>Set Oracle Parameters for the Command Objects</Summary>
        ///<param name="_commands">Used to transfer Oracle Command Object.</param>
        ///<seealso cref="Oracle.DataAccess.Client.OracleCommand"/>
        ///<param name="_table">Used to transfer Table.</param>
        ///<seealso cref="System.Data.DataTable"/>
        private OracleCommand SetCommonParameters(OracleCommand _commands, DataTable _table)
        {

            foreach (DataColumn col in _table.Columns)
            {
                OracleDbType oracleType = GetCommonDataType(col.DataType);
                _commands.Parameters.Add(new OracleParameter("@" + col.ColumnName, oracleType));
                _commands.Parameters["@" + col.ColumnName].SourceColumn = col.ColumnName;

            }

            return _commands;
        }
        ///<Summary>Re-Arrange Oracle Command</Summary>
        ///<param name="_command">Used to indicate Oracle Command object.</param>
        ///<seealso cref="Oracle.DataAccess.Client.OracleCommand"/>
        ///<param name="_connection">Used to indicate Oracle Connection object.</param>
        ///<seealso cref="Oracle.DataAccess.Client.OracleConnection"/>
        ///<param name="_transaction">Used to indicate Oracle Transaction object.</param>
        ///<seealso cref="Oracle.DataAccess.Client.OracleTransaction"/>
        ///<param name="_commandType">Used to indicate the Command Type.</param>
        ///<seealso cref="System.Data.CommandType"/>
        ///<param name="_commandText">Used to indicate the Command Text.</param>
        ///<seealso cref="System.String"/>
        ///<param name="_parameters">Used to indicate the Parameters.</param>
        ///<seealso cref="Oracle.DataAccess.Client.OracleParameter"/>
        ///<param name="_isAsyTransactionBase">Used to indicate which use the transaction base process.</param>
        ///<seealso cref="System.Boolean"/>
        private void MakeCommonCommand(OracleCommand _command, OracleConnection _connection, OracleTransaction _transaction, CommandType _commandType, string _commandText, Boolean _isAsyTransactionBase, params  OracleParameter[] _parameters)
        {
            if (DataBaseSource != null) {
                switch (DataBaseSource.Add_db_tp) { 
                    case "ORACLE":
                        _command.Connection = _connection;
                        _command.CommandText = _commandText;

                        if (_isAsyTr)
                        {
                            _command.Transaction = oAsyTransaction;
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
                    default:
                        break;
                }
            }
            

        }
        private void MakeCommonCommand(OracleCommand _command, OracleConnection _connection, OracleTransaction _transaction, CommandType _commandType, string _commandText, Boolean _isAsyTransactionBase)
        {
            _command.Connection = _connection;
            _command.CommandText = _commandText;
            if (_isAsyTr) _command.Transaction = oAsyTransaction;
            _command.CommandType = _commandType;
            return;
        }

        ///<summary> Class Constructor </summary>
        ///<c>DA</c> is the Construtor for the Data Access Layer Class
        public AsyBaseDAL(ASY_DB_SOURCE DbSrc)
        {
            try
            {
                if (DbSrc != null) { 
                    DataBaseSource = DbSrc;
                    ConnectionString = GetCommonConnectionString();
                }
            }
            catch (Exception ex)
            {
                //TODO: To be handle by exception class
            }

            finally
            {
                if (DataBaseSource != null) {
                    switch (DataBaseSource.Add_db_tp) { 
                        case "ORACLE":
                            oASyConnection = new OracleConnection(ConnectionString);
                            oAsyAdapter = null;
                            oAsyTransaction = null;
                            break;
                        default :
                            break;
                    }
                }
                
            }


        }
        /// <summary>
        /// Used to Commit the Transaction
        /// </summary>
        public void CommonTransactionCommit()
        {
            if (DataBaseSource != null) 
            {
                switch (DataBaseSource.Add_db_tp) { 
                    case "ORACLE":
                        if (oAsyTransaction != null)
                        {
                            oAsyTransaction.Commit();
                            oAsyTransaction.Dispose();
                            oAsyTransaction = null;
                        }
                        break;
                    default:
                        break;
                }
            }
            
        }
        /// <summary>
        /// Used to Roll Back the transaction
        /// </summary>
        public void CommonTransactionRollback()
        {
            if (DataBaseSource != null)
            {
                switch (DataBaseSource.Add_db_tp)
                {
                    case "ORACLE":
                        if (oAsyTransaction != null)
                        {
                            oAsyTransaction.Rollback();
                            oAsyTransaction.Dispose();
                            oAsyTransaction = null;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// Used to establish the connectivity of the DataBase
        /// </summary>
        /// <returns> Return whether connected or not connnected (True/False) </returns>
        public Boolean CommonConnectionOpen()
        {
            Boolean _isOpen = false;
            if (DataBaseSource != null) {
                switch (DataBaseSource.Add_db_tp)
                {
                    case "ORACLE":
                       
                        if (oASyConnection.State != ConnectionState.Open)
                        {
                            oASyConnection.Open();
                        }

                        if (oASyConnection.State == ConnectionState.Open)
                            _isOpen = true;
                        else _isOpen = false;


                        _isAsyTr = false;
                        break;
                    default:
                        break;
                }
            }
            return _isOpen;
            
        }

        //Add by Chamal 16-Nov-2013
        public Boolean ConnectionOpen_DR()
        {
            Boolean _isOpen = false;

            if (GetDRConnectionStatus() == "1")
            {
                oASyConnection = new OracleConnection(GetConnectionStringDR());

                if (oASyConnection.State != ConnectionState.Open) oASyConnection.Open();

                if (oASyConnection.State == ConnectionState.Open) _isOpen = true;
                else _isOpen = false;

                _isAsyTr = false;
            }

            return _isOpen;
        }

        /// <summary>
        /// Get Connected for the unknown database in a different company
        /// Unethical rooting for the profession, but as per the IT Dept requirement.
        /// Written by Prabhath on 19/12/2013
        /// </summary>
        /// <param name="_connectionstring"> Connection string for the different company which not exposed reside in a database </param>
        /// <returns> Whether connection is open/close </returns>
        public Boolean ConnectionOpenUnknown(string _connectionstring)
        {
            Boolean _isOpen = false;
            if (!string.IsNullOrEmpty(_connectionstring))
            {
                oASyConnection = new OracleConnection(_connectionstring);
                if (oASyConnection.State != ConnectionState.Open) oASyConnection.Open();
                if (oASyConnection.State == ConnectionState.Open) _isOpen = true;
                else _isOpen = false;
                _isAsyTr = false;
            }
            return _isOpen;
        }

        /// <summary>
        /// Used to disconnect the connectivity of the DataBase
        /// </summary>
        /// <returns>Return whether disconnected or not disconnected (True/False)</returns>
        public Boolean CommonConnectionClose()
        {
            Boolean _isClose = false;
            if (DataBaseSource != null)
            {
                switch (DataBaseSource.Add_db_tp)
                {
                    case "ORACLE":
                        if (oASyConnection.State == ConnectionState.Open)
                        {
                            oASyConnection.Close();
                            oASyConnection.Dispose();
                            //OracleConnection.ClearAllPools();
                        }

                        if (oASyConnection.State == ConnectionState.Closed)
                            _isClose = true;
                        else
                            _isClose = false;
                        break;
                    default:
                        break;
                }
            }
            return _isClose;

        }

       
        ///<summary>Get Data From DataBase as a Table Set
        ///</summary>
        ///<param name="_tableName">Used to transfer Table Name.</param>
        ///<seealso cref="System.String"/>
        ///<param name="_storedProcedure">Used to transfer Stored Procedure Name.</param>
        ///<seealso cref="System.String"/>
        ///<param name="_commTypes">Used to indicate the Command Type.</param>
        ///<seealso cref="System.Data.CommandType"/>
        ///<param name="_parameters">Used to indicate the List of Parameters.</param>
        ///<seealso cref="Oracle.DataAccess.Client.OracleParameter"/>
        ///<param name="_isAsyTransactionBase">Used to indicate which use the transaction base process.</param>
        ///<seealso cref="System.Boolean"/>
        ///<returns>Used to return as DataTable</returns>
        ///<example ><code> CommonConnectionOpen();
        /// DataTable _table = QueryDataTable(_tableName, _storedProcedure, _commTypes, _parameters, false);
        /// CommonConnectionClose();
        /// </code><remarks>No Transaction occure</remarks>
        ///</example>
        public DataTable CommonQueryDataTable(String _tableName, String _storedProcedure, CommandType _commTypes, Boolean _isAsyTransactionBase, params OracleParameter[] _parameters)
        {
            OracleCommand command = new OracleCommand();
            DataTable table = null;  
            MakeCommonCommand(command, oASyConnection, oAsyTransaction, _commTypes, _storedProcedure, _isAsyTransactionBase, _parameters);
            OracleDataAdapter adapter = GetCommonAdapter();
            table = new DataTable(_tableName);
            adapter.SelectCommand = command;
            adapter.Fill(table);
            return table;
        }
        public DataTable CommonQueryDataTable(String _tableName, String _storedProcedure, CommandType _commTypes, Boolean _isAsyTransactionBase)
        {
            OracleCommand command = new OracleCommand();
            DataTable table = null;
            MakeCommonCommand(command, oASyConnection, oAsyTransaction, _commTypes, _storedProcedure, _isAsyTransactionBase);
            OracleDataAdapter adapter = GetCommonAdapter();
            table = new DataTable(_tableName);
            adapter.SelectCommand = command;
            adapter.Fill(table);
            return table;
        }

        public string CommonGetPasswordHash()
        {
            return ConfigurationManager.ConnectionStrings["PasswordHash"].ConnectionString.ToString();
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
        ///<seealso cref="Oracle.DataAccess.Client.OracleParameter"/>
        ///<param name="_isAsyTransactionBase">Used to indicate which use the transaction base process.</param>
        ///<seealso cref="System.Boolean"/> 
        ///<returns>Used to return as DataSet</returns>
        public DataSet CommonQueryDataSet(String _tableName, String _storedProcedure, CommandType _commTypes, Boolean _isAsyTransactionBase, params OracleParameter[] _parameters)
        {
            OracleCommand command = new OracleCommand();
            MakeCommonCommand(command, oASyConnection, oAsyTransaction, _commTypes, _storedProcedure, _isAsyTransactionBase, _parameters);
            OracleDataAdapter adapter = GetCommonAdapter();
            DataSet dataset = new DataSet();
            adapter.SelectCommand = command;
            adapter.Fill(dataset, _tableName);
            return dataset;

        }
        public DataSet CommonQueryDataSet(String _tableName, String _storedProcedure, CommandType _commTypes, Boolean _isAsyTransactionBase)
        {
            OracleCommand command = new OracleCommand();
            MakeCommonCommand(command, oASyConnection, oAsyTransaction, _commTypes, _storedProcedure, _isAsyTransactionBase);
            OracleDataAdapter adapter = GetCommonAdapter();
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
        public Int32 CommonUpdateRecords(String _storedProcedure, CommandType _commTypes, params OracleParameter[] _parameters)
        {
            OracleCommand command = new OracleCommand();
            Int32 effects = 0;

            MakeCommonCommand(command, oASyConnection, oAsyTransaction, _commTypes, _storedProcedure, true, _parameters);
            effects = command.ExecuteNonQuery();

            if (_parameters[_parameters.Length - 1].Value == DBNull.Value)
                effects = 0;
            else
                effects = Convert.ToInt32(_parameters[_parameters.Length - 1].Value.ToString());

            return effects;
        }
        public Int32 CommonUpdateRecords(String _storedProcedure, CommandType _commTypes)
        {
            OracleCommand command = new OracleCommand();
            Int32 effects = 0;
            MakeCommonCommand(command, oASyConnection, oAsyTransaction, _commTypes, _storedProcedure, true);
            effects = command.ExecuteNonQuery();
            return effects;
        }
        /// <summary>
        /// For Execute a oracle function which return True/False or Numeric Values
        /// </summary>
        /// <param name="_storedProcedure"> Function Name </param>
        /// <param name="_commTypes"> Command type should be Stored Procedures </param>
        /// <param name="_parameters"> Collection of parameters which assign to function </param>
        /// <returns></returns>
        public Int32 CommonQueryFunction(String _storedProcedure, CommandType _commTypes, params OracleParameter[] _parameters)
        {
            OracleCommand command = new OracleCommand();
            Int32 effected = 0;
            MakeCommonCommand(command, oASyConnection, oAsyTransaction, _commTypes, _storedProcedure, false, _parameters);
            OracleParameter _return = new OracleParameter("return", OracleDbType.Int32, ParameterDirection.ReturnValue);
            command.Parameters.Add(_return);
            command.ExecuteNonQuery();

            if (_return.Value == DBNull.Value)
                effected = 0;
            else
                effected = Convert.ToInt32(_return.Value.ToString());

            return (Int32)effected;
        }


        /// <summary>
        /// Return Single Value from Store Procedure
        /// </summary>
        /// <param name="_storedProcedure"> Stored Procedure name </param>
        /// <param name="_commTypes"> Type of the command </param>
        /// <param name="_outPara"> Single OUT Parameter </param>
        /// <param name="_parameters">Many IN parameters</param>
        /// <returns></returns>
        public Int32 CommonReturnSP_SingleValue(String _storedProcedure, CommandType _commTypes, OracleParameter _outPara, params OracleParameter[] _parameters)
        {
            OracleCommand command = new OracleCommand();
            Int32 effected = 0;
            MakeCommonCommand(command, oASyConnection, oAsyTransaction, _commTypes, _storedProcedure, false, _parameters);
            OracleParameter _return = _outPara;
            command.Parameters.Add(_return);
            command.ExecuteNonQuery();

            if (_return.Value == DBNull.Value)
                effected = 0;
            else if (_return.Value == null)
                effected = 0;
            else
                effected = Convert.ToInt32(_return.Value.ToString());

            return (Int32)effected;
        }

        public Decimal CommonReturnSP_Single_DecimalValue(String _storedProcedure, CommandType _commTypes, OracleParameter _outPara, params OracleParameter[] _parameters)
        {
            OracleCommand command = new OracleCommand();
            Decimal effected = 0;

            MakeCommonCommand(command, oASyConnection, oAsyTransaction, _commTypes, _storedProcedure, false, _parameters);
            OracleParameter _return = _outPara;
            command.Parameters.Add(_return);
            command.ExecuteNonQuery();

            if (_return.Value == DBNull.Value)
                effected = 0;
            else if (_return.Value == null)
                effected = 0;
            else
                effected = Convert.ToDecimal(_return.Value.ToString());

            return (Decimal)effected;
        }

        //Shani 26-10-2012
        public List<OracleParameter> CommonReturnSP_Many_DecimalValues(String _storedProcedure, CommandType _commTypes, List<OracleParameter> _outPara, params OracleParameter[] _parameters)
        {
            OracleCommand command = new OracleCommand();
            // Decimal effected = 0;

            MakeCommonCommand(command, oASyConnection, oAsyTransaction, _commTypes, _storedProcedure, false, _parameters);
            //OracleParameter _return = _outPara;
            foreach (OracleParameter _return in _outPara)
            {
                command.Parameters.Add(_return);
            }

            command.ExecuteNonQuery();
            foreach (OracleParameter _return in _outPara)
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


        /// <summary>
        /// Added By Prabhath on 28/03/2013 for support paging to the front-end
        /// </summary>
        /// <param name="_tableName"> Table Name which going to use  </param>
        /// <param name="_storedProcedure"> stored procedure name </param>
        /// <param name="_commTypes"> execution type </param>
        /// <param name="_isAsyTransactionBase"> support from oracle transaction </param>
        /// <param name="_startFrom"> starting record for paging </param>
        /// <param name="_endFrom"> ending record for paging </param>
        /// <param name="_parameters"> oracle parameters </param>
        /// <returns></returns>
        public DataTable CommonQueryDataTable(String _tableName, String _storedProcedure, CommandType _commTypes, Boolean _isAsyTransactionBase, int _startFrom, int _endFrom, params OracleParameter[] _parameters)
        {
            OracleCommand command = new OracleCommand();
            DataTable table = null;
            MakeCommonCommand(command, oASyConnection, oAsyTransaction, _commTypes, _storedProcedure, _isAsyTransactionBase, _parameters);
            OracleDataAdapter adapter = GetCommonAdapter();
            table = new DataTable(_tableName);
            adapter.SelectCommand = command;
            adapter.Fill(_startFrom, _endFrom, table);
            return table;

        }

        /// <summary>
        /// Written by Prabhath on 07/08/2013 to overcome the enlisted problem
        /// </summary>
        /// <param name="_tx"></param>
        public void CommonEnlistTransaction(Transaction _tx)
        {
            oASyConnection.EnlistTransaction(_tx);
        }

    }

}
