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
   public class FIXADAL
    {
                //  Task            : Data Access Layer for the Web Base Supply Chain Management System 
        //  Developed By    : Prabhath Wijetunge 
        //  Competed On     : 03/02/2012 

        ///<summary> Database reference variable which allocate for each and every processes </summary>
        ///<remarks> The DataBase connection is set when the new object create. </remarks>
        ///<c>oConnection</c> Used to store Oracle DataBase Connection
        ///<seealso cref="Oracle.DataAccess.Client.OracleConnection"/>
        public OracleConnection oConnection_FIXA;
        ///<c>oAdapter</c> Used to store Oracle DataBase Adapter Connectivity
        ///<seealso cref="Oracle.DataAccess.Client.OracleDataAdapter"/>
        public OracleDataAdapter oAdapter;
        ///<c>oRead</c> Used to control transaction between DataBase and the Entity
        ///<seealso cref="Oracle.DataAccess.Client.OracleTransaction"/>
        public OracleTransaction oTransaction;
        ///<c>ConnectionString</c> Used to Get the connection string and store it for further use
        ///<seealso cref="String"/>
        string ConnectionString = "";

        public bool _isTr = false;

        public void BeginTransaction()
        {
            if (oTransaction == null)
            {
                oTransaction = oConnection_FIXA.BeginTransaction();
                _isTr = true;
            }
        }


        // Tharindu 2018-05-16
        public string GetConnectionFixedAssets()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["ConnFix"].ConnectionString;
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
        public OracleTransaction GetTransaction()
        {
            if (oTransaction == null)
            {
                oTransaction = oConnection_FIXA.BeginTransaction();
            }
            return oTransaction;
        }
        ///<Summary>Used to convert .Net Types to Oracle DataBase Data Types </Summary>
        ///<param name="_type">Used to indicate data type.</param>
        ///<seealso cref="Oracle.DataAccess.Client.OracleDbType"/>
        ///<seealso cref="System.Type"/>
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
        ///<Summary>Set Oracle Parameters for the Command Objects</Summary>
        ///<param name="_commands">Used to transfer Oracle Command Object.</param>
        ///<seealso cref="Oracle.DataAccess.Client.OracleCommand"/>
        ///<param name="_table">Used to transfer Table.</param>
        ///<seealso cref="System.Data.DataTable"/>
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
        ///<param name="_isTransactionBase">Used to indicate which use the transaction base process.</param>
        ///<seealso cref="System.Boolean"/>
        private void MakeCommand(OracleCommand _command, OracleConnection _connection, OracleTransaction _transaction, CommandType _commandType, string _commandText, Boolean _isTransactionBase, params  OracleParameter[] _parameters)
            {

            _command.Connection = _connection;
            _command.CommandText = _commandText;

            //if (_isTransactionBase)
            //{
            //    _command.Transaction = _transaction;
            //}

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
        private void MakeCommand(OracleCommand _command, OracleConnection _connection, OracleTransaction _transaction, CommandType _commandType, string _commandText, Boolean _isTransactionBase)
        {
            _command.Connection = _connection;
            _command.CommandText = _commandText;
            if (_isTr) _command.Transaction = oTransaction;
            _command.CommandType = _commandType;
            return;
        }

        ///<summary> Class Constructor </summary>
        ///<c>DA</c> is the Construtor for the Data Access Layer Class



        public FIXADAL()
        {
            try
            {
                ConnectionString = GetConnectionFixedAssets();
            }
            catch (Exception ex)
            {
                //TODO: To be handle by exception class
            }

            finally
            {
                oConnection_FIXA = new OracleConnection(ConnectionString);
                oAdapter = null;
                oTransaction = null;
            }


        }



        //public BaseDAL(string dbname)
        //{
        //    try
        //    {
        //        ConnectionString = GetConnectionFixedAssets();
        //    }
        //    catch (Exception ex)
        //    {
        //        //TODO: To be handle by exception class
        //    }

        //    finally
        //    {
        //        oConnection = new OracleConnection(ConnectionString);
        //        oAdapter = null;
        //        oTransaction = null;
        //    }


        //}

        /// <summary>
        /// Used to Commit the Transaction
        /// </summary>
        public void TransactionCommit()
        {
            if (oTransaction != null)
            {
                oTransaction.Commit();
                oTransaction.Dispose();
                oTransaction = null;
            }
        }
        /// <summary>
        /// Used to Roll Back the transaction
        /// </summary>
        public void TransactionRollback()
        {
            if (oTransaction != null)
            {
                oTransaction.Rollback();
                oTransaction.Dispose();
                oTransaction = null;
            }
        }
        /// <summary>
        /// Used to establish the connectivity of the DataBase
        /// </summary>
        /// <returns> Return whether connected or not connnected (True/False) </returns>
        public Boolean ConnectionOpen()
        {
            Boolean _isOpen = false;
            if (oConnection_FIXA.State != ConnectionState.Open)
            {
                oConnection_FIXA.Open();
            }

            if (oConnection_FIXA.State == ConnectionState.Open)
                _isOpen = true;
            else _isOpen = false;


            _isTr = false;
            return _isOpen;
        }

     

        //Add by Tharindu 16-05-2018
        public Boolean ConnectionOpenFix()
        {
            Boolean _isOpen = false;

           
                oConnection_FIXA = new OracleConnection(GetConnectionFixedAssets());

                if (oConnection_FIXA.State != ConnectionState.Open) oConnection_FIXA.Open();

                if (oConnection_FIXA.State == ConnectionState.Open) _isOpen = true;
                else _isOpen = false;

                _isTr = false;
            

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
                oConnection_FIXA = new OracleConnection(_connectionstring);
                if (oConnection_FIXA.State != ConnectionState.Open) oConnection_FIXA.Open();
                if (oConnection_FIXA.State == ConnectionState.Open) _isOpen = true;
                else _isOpen = false;
                _isTr = false;
            }
            return _isOpen;
        }

        /// <summary>
        /// Used to disconnect the connectivity of the DataBase
        /// </summary>
        /// <returns>Return whether disconnected or not disconnected (True/False)</returns>
        public Boolean ConnectionClose()
        {
            Boolean _isClose = false;
            if (oConnection_FIXA.State == ConnectionState.Open)
            {
                oConnection_FIXA.Close();
                oConnection_FIXA.Dispose();
                //OracleConnection.ClearAllPools();
            }

            if (oConnection_FIXA.State == ConnectionState.Closed)
                _isClose = true;
            else
                _isClose = false;

            return _isClose;

        }

        ///<summary>Add/Update/Delete records from the data table and return the status of the process
        ///</summary>
        ///<param name="_table">Used to transfer Table.</param>
        ///<seealso cref="System.Data.DataTable"/>
        ///<param name="_storedProcedure">Used to transfer Stored Procedure Name.</param>
        ///<seealso cref="System.String"/>
        ///<param name="_isSave">Used to indicate the saving process.</param>
        ///<seealso cref="System.Boolean"/>
        ///<param name="_isUpdate">Used to indicate the updating process.</param>
        ///<seealso cref="System.Boolean"/>
        ///<param name="_isDelete">Used to indicate the delieting process.</param>
        ///<seealso cref="System.Boolean"/>
        ///<returns>Return Integer which mark as 1 and 0 for indicate updated/Deleted/Added or not</returns>
        ///<example ><code> OpenConnection();
        /// int32 effected = UpdateRecords(_table, _storedProcedure, true,false, false);
        /// if (effected==0)
        /// {
        ///     TransactionRollback();
        /// }
        /// else 
        /// {
        ///     TransactionCommit();
        /// }
        /// </code><remarks>No need to open transaction</remarks>
        ///</example>
        ///

        //public Int32 UpdateRecords(DataTable _table, String _storedProcedure, Boolean _isSave, Boolean _isUpdate, Boolean _isDelete)
        //{
        //    OracleCommand command = new OracleCommand();
        //    command = SetParameters(command, _table);
        //    command.CommandText = _storedProcedure;
        //    command.CommandType = CommandType.StoredProcedure;

        //    oAdapter = GetAdapter();

        //    if (_isSave)
        //    {
        //        oAdapter.InsertCommand = command;

        //    }
        //    else if (_isUpdate)
        //    {
        //        oAdapter.UpdateCommand = command;
        //    }
        //    else if (_isDelete)
        //    {
        //        oAdapter.DeleteCommand = command;
        //    }

        //    command.Connection = oConnection;
        //    oTransaction = GetTransaction();
        //    command.Transaction = oTransaction;

        //    Int32 _rowsEffect = oAdapter.Update(_table);
        //    command.Dispose();

        //    return _rowsEffect;

        //}

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
        ///<param name="_isTransactionBase">Used to indicate which use the transaction base process.</param>
        ///<seealso cref="System.Boolean"/>
        ///<returns>Used to return as DataTable</returns>
        ///<example ><code> ConnectionOpen();
        /// DataTable _table = QueryDataTable(_tableName, _storedProcedure, _commTypes, _parameters, false);
        /// ConnectionClose();
        /// </code><remarks>No Transaction occure</remarks>
        ///</example>
        public DataTable QueryDataTable(String _tableName, String _storedProcedure, CommandType _commTypes, Boolean _isTransactionBase, params OracleParameter[] _parameters)
        {
            OracleCommand command = new OracleCommand();
            DataTable table = null;  
            MakeCommand(command, oConnection_FIXA, oTransaction, _commTypes, _storedProcedure, _isTransactionBase, _parameters);
            OracleDataAdapter adapter = GetAdapter();
            table = new DataTable(_tableName);
            adapter.SelectCommand = command;
            adapter.Fill(table);
            return table;
        }
        public DataTable QueryDataTable(String _tableName, String _storedProcedure, CommandType _commTypes, Boolean _isTransactionBase)
        {
            OracleCommand command = new OracleCommand();
            DataTable table = null;
            MakeCommand(command, oConnection_FIXA, oTransaction, _commTypes, _storedProcedure, _isTransactionBase);
            OracleDataAdapter adapter = GetAdapter();
            table = new DataTable(_tableName);
            adapter.SelectCommand = command;
            adapter.Fill(table);
            return table;
        }

        public string GetPasswordHash()
        {
            return ConfigurationManager.ConnectionStrings["PasswordHash"].ConnectionString.ToString();
        }

        #region Mail Sender Information
        public string GetMailAddress()
        {
            return ConfigurationManager.ConnectionStrings["MailAdd"].ConnectionString.ToString();
        }

        public string GetMailDispalyName()
        {
            return ConfigurationManager.ConnectionStrings["MailDisp"].ConnectionString.ToString();
        }

        public string GetMailHost()
        {
            return ConfigurationManager.ConnectionStrings["MailHost"].ConnectionString.ToString();
        }

        public string GetMailFooterMsg()
        {
            return ConfigurationManager.ConnectionStrings["MailFooter"].ConnectionString.ToString();
        }
        public string GetMailFooterMsgColor()
        {
            return ConfigurationManager.ConnectionStrings["MailFooterColor"].ConnectionString.ToString();
        }
        
        public string GetHPCustContactPhoneNo()
        {
            return ConfigurationManager.ConnectionStrings["HPContPhoneNo"].ConnectionString.ToString();
        }
        #endregion

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
        ///<param name="_isTransactionBase">Used to indicate which use the transaction base process.</param>
        ///<seealso cref="System.Boolean"/> 
        ///<returns>Used to return as DataSet</returns>
        public DataSet QueryDataSet(String _tableName, String _storedProcedure, CommandType _commTypes, Boolean _isTransactionBase, params OracleParameter[] _parameters)
        {
            OracleCommand command = new OracleCommand();
            MakeCommand(command, oConnection_FIXA, oTransaction, _commTypes, _storedProcedure, _isTransactionBase, _parameters);
            OracleDataAdapter adapter = GetAdapter();
            DataSet dataset = new DataSet();
            adapter.SelectCommand = command;
            adapter.Fill(dataset, _tableName);
            return dataset;

        }
        public DataSet QueryDataSet(String _tableName, String _storedProcedure, CommandType _commTypes, Boolean _isTransactionBase)
        {
            OracleCommand command = new OracleCommand();
            MakeCommand(command, oConnection_FIXA, oTransaction, _commTypes, _storedProcedure, _isTransactionBase);
            OracleDataAdapter adapter = GetAdapter();
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
        public Int32 UpdateRecords(String _storedProcedure, CommandType _commTypes, params OracleParameter[] _parameters)
        {
            OracleCommand command = new OracleCommand();
            Int32 effects = 0;

            MakeCommand(command, oConnection_FIXA, oTransaction, _commTypes, _storedProcedure, true, _parameters);
            effects = command.ExecuteNonQuery();

            if (_parameters[_parameters.Length - 1].Value == DBNull.Value)
                effects = 0;
            else
                effects = Convert.ToInt32(_parameters[_parameters.Length - 1].Value.ToString());

            return effects;
        }
        public Int32 UpdateRecords(String _storedProcedure, CommandType _commTypes)
        {
            OracleCommand command = new OracleCommand();
            Int32 effects = 0;
            MakeCommand(command, oConnection_FIXA, oTransaction, _commTypes, _storedProcedure, true);
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
        public Int32 QueryFunction(String _storedProcedure, CommandType _commTypes, params OracleParameter[] _parameters)
        {
            OracleCommand command = new OracleCommand();
            Int32 effected = 0;
            MakeCommand(command, oConnection_FIXA, oTransaction, _commTypes, _storedProcedure, false, _parameters);
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
        public Int32 ReturnSP_SingleValue(String _storedProcedure, CommandType _commTypes, OracleParameter _outPara, params OracleParameter[] _parameters)
        {
            OracleCommand command = new OracleCommand();
            Int32 effected = 0;
            MakeCommand(command, oConnection_FIXA, oTransaction, _commTypes, _storedProcedure, false, _parameters);
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

        public Decimal ReturnSP_Single_DecimalValue(String _storedProcedure, CommandType _commTypes, OracleParameter _outPara, params OracleParameter[] _parameters)
        {
            OracleCommand command = new OracleCommand();
            Decimal effected = 0;

            MakeCommand(command, oConnection_FIXA, oTransaction, _commTypes, _storedProcedure, false, _parameters);
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

        #region Active Directory Information

        ///<summary>
        /// This section using for getting active directory(AD) information
        /// adConnectionString
        /// adDomainName
        /// adDomainUserName
        /// adDomainUserPW
        ///

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


        //Shani 26-10-2012
        public List<OracleParameter> ReturnSP_Many_DecimalValues(String _storedProcedure, CommandType _commTypes, List<OracleParameter> _outPara, params OracleParameter[] _parameters)
        {
            OracleCommand command = new OracleCommand();
            // Decimal effected = 0;

            MakeCommand(command, oConnection_FIXA, oTransaction, _commTypes, _storedProcedure, false, _parameters);
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
        /// <param name="_isTransactionBase"> support from oracle transaction </param>
        /// <param name="_startFrom"> starting record for paging </param>
        /// <param name="_endFrom"> ending record for paging </param>
        /// <param name="_parameters"> oracle parameters </param>
        /// <returns></returns>
        public DataTable QueryDataTable(String _tableName, String _storedProcedure, CommandType _commTypes, Boolean _isTransactionBase, int _startFrom, int _endFrom, params OracleParameter[] _parameters)
        {
            OracleCommand command = new OracleCommand();
            DataTable table = null;
            MakeCommand(command, oConnection_FIXA, oTransaction, _commTypes, _storedProcedure, _isTransactionBase, _parameters);
            OracleDataAdapter adapter = GetAdapter();
            table = new DataTable(_tableName);
            adapter.SelectCommand = command;
            adapter.Fill(_startFrom, _endFrom, table);
            return table;

        }

        /// <summary>
        /// Written by Prabhath on 07/08/2013 to overcome the enlisted problem
        /// </summary>
        /// <param name="_tx"></param>
        public void EnlistTransaction(Transaction _tx)
        {
            oConnection_FIXA.EnlistTransaction(_tx);
        }

    }
}
