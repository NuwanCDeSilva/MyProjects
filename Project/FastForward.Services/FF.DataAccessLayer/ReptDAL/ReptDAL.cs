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
    public class ReptDAL
    {

        //  Task            : Data Access Layer for the Web Base Supply Chain Management System 
        //  Developed By    : Prabhath Wijetunge 
        //  Competed On     : 03/02/2012 

        ///<summary> Database reference variable which allocate for each and every processes </summary>
        ///<remarks> The DataBase connection is set when the new object create. </remarks>
        ///<c>oConnection</c> Used to store Oracle DataBase Connection
        ///<seealso cref="Oracle.DataAccess.Client.OracleConnection"/>
        private OracleConnection oConnection;
        ///<c>oAdapter</c> Used to store Oracle DataBase Adapter Connectivity
        ///<seealso cref="Oracle.DataAccess.Client.OracleDataAdapter"/>
        private OracleDataAdapter oAdapter;
        ///<c>oRead</c> Used to control transaction between DataBase and the Entity
        ///<seealso cref="Oracle.DataAccess.Client.OracleTransaction"/>
        private OracleTransaction oTransaction;
        ///<c>ConnectionString</c> Used to Get the connection string and store it for further use
        ///<seealso cref="String"/>
        string ConnectionString = "";

        public bool _isTr = false;

        public void BeginTransaction()
        {
            if (oTransaction == null)
            {
                oTransaction = oConnection.BeginTransaction();
                _isTr = true;
            }
        }

        public string GetConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["ConnReportsdb"].ConnectionString;
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
                oTransaction = oConnection.BeginTransaction();
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

            //modify by Prabhath, on 8 3 2012 for use transaction scope

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

        ///<summary> Class Constructor </summary>
        ///<c>DA</c> is the Construtor for the Data Access Layer Class
        public ReptDAL()
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
                oConnection = new OracleConnection(ConnectionString);
                oAdapter = null;
                oTransaction = null;
            }


        }
        /// <summary>
        /// Used to Commit the Transaction
        /// </summary>
        public void TransactionCommit()
        {
            //try
            //{
            if (oTransaction != null)
            {
                oTransaction.Commit();
                oTransaction.Dispose();
                oTransaction = null;
            }
            //}
            //catch (Exception ex)
            //{
            //    //TODO: To be handle by exception class
            //}

        }
        /// <summary>
        /// Used to Roll Back the transaction
        /// </summary>
        public void TransactionRollback()
        {
            if (oTransaction != null)
            {
                //try
                //{
                oTransaction.Rollback();
                oTransaction.Dispose();
                oTransaction = null;
                //}
                //catch (OracleException ex)
                //{
                //    throw new ApplicationException("Can not rollback the transaction", ex);
                //    //TODO : Error class to be implement
                //}

            }
        }
        /// <summary>
        /// Used to establish the connectivity of the DataBase
        /// </summary>
        /// <returns> Return whether connected or not connnected (True/False) </returns>
        public Boolean ConnectionOpen()
        {
            Boolean _isOpen = false;
            if (oConnection.State != ConnectionState.Open)
            {
                oConnection.Open();
            }

            if (oConnection.State == ConnectionState.Open)
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
            if (oConnection.State == ConnectionState.Open)
            {
                oConnection.Close();
                oConnection.Dispose();
                //OracleConnection.ClearAllPools();
            }

            if (oConnection.State == ConnectionState.Closed)
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

            command.Connection = oConnection;
            oTransaction = GetTransaction();
            command.Transaction = oTransaction;

            Int32 _rowsEffect = oAdapter.Update(_table);
            command.Dispose();

            return _rowsEffect;


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
            //try
            //{

            MakeCommand(command, oConnection, oTransaction, _commTypes, _storedProcedure, _isTransactionBase, _parameters);
            OracleDataAdapter adapter = GetAdapter();
            table = new DataTable(_tableName);
            adapter.SelectCommand = command;
            adapter.Fill(table);
            //}
            //catch (Exception ex)
            //{
            //    //TODO: To be handle by exception class
            //    throw new Exception(ex.Message);
            //}
            //finally
            //{

            //}
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
        ///<seealso cref="Oracle.DataAccess.Client.OracleParameter"/>
        ///<param name="_isTransactionBase">Used to indicate which use the transaction base process.</param>
        ///<seealso cref="System.Boolean"/> 
        ///<returns>Used to return as DataSet</returns>
        public DataSet QueryDataSet(String _tableName, String _storedProcedure, CommandType _commTypes, Boolean _isTransactionBase, params OracleParameter[] _parameters)
        {
            OracleCommand command = new OracleCommand();
            MakeCommand(command, oConnection, oTransaction, _commTypes, _storedProcedure, _isTransactionBase, _parameters);
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

            //try
            //{
            //Transaction Object assign - modify by Prabhath on 06/03/2012
            MakeCommand(command, oConnection, oTransaction, _commTypes, _storedProcedure, true, _parameters);
            // OracleParameter _return = new OracleParameter("return", OracleDbType.Int32, ParameterDirection.ReturnValue);
            // command.Parameters.Add(_return);
            effects = command.ExecuteNonQuery();

            if (_parameters[_parameters.Length - 1].Value == DBNull.Value)
                effects = 0;
            else
                effects = Convert.ToInt32(_parameters[_parameters.Length - 1].Value.ToString());
            //}
            //catch (Exception ex)
            //{
            //    //TODO: To be handle by exception class
            //    throw new Exception(ex.Message);
            //}
            //finally
            //{

            //}
            return effects;
        }

        /// <summary>
        /// Written By Prabhath on 06/03/20112
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

            //try
            //{
            MakeCommand(command, oConnection, oTransaction, _commTypes, _storedProcedure, false, _parameters);
            OracleParameter _return = new OracleParameter("return", OracleDbType.Int32, ParameterDirection.ReturnValue);
            command.Parameters.Add(_return);
            command.ExecuteNonQuery();

            if (_return.Value == DBNull.Value)
                effected = 0;
            else
                effected = Convert.ToInt32(_return.Value.ToString());

            //}
            //catch (Exception ex)
            //{
            //    //TODO: To be handle by exception class
            //    throw new Exception(ex.Message);
            //}
            //finally
            //{

            //}
            return (Int32)effected;
        }

        //Written by P.Wijetunge on 09/04/2012
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

            //try
            //{
            MakeCommand(command, oConnection, oTransaction, _commTypes, _storedProcedure, false, _parameters);
            OracleParameter _return = _outPara;
            command.Parameters.Add(_return);
            command.ExecuteNonQuery();

            if (_return.Value == DBNull.Value)
                effected = 0;
            else if (_return.Value == null)
            {
                effected = 1;
            }
            else
                effected = Convert.ToInt32(_return.Value.ToString());

            //}
            //catch (Exception ex)
            //{
            //    //TODO: To be handle by exception class
            //    throw new Exception(ex.Message);
            //}
            //finally
            //{

            //}
            return (Int32)effected;
        }

        /// <summary>
        /// Written by Prabhath on 07/08/2013 to overcome the enlisted problem
        /// </summary>
        /// <param name="_tx"></param>
        public void EnlistTransaction(Transaction _tx)
        {
            oConnection.EnlistTransaction(_tx);
        }

        public DataTable GetCustAcknowledgeDetails(string _User)
        {
            // Sanjeewa 06-03-2014         
            OracleParameter[] param = new OracleParameter[2];
            param[0] = new OracleParameter("n_cursor", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("IN_USER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _User;
            
            DataTable _dtResults;
            _dtResults = QueryDataTable("tbl", "sp_get_cust_acknowledgement", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }               

        #region Brand Reports

        public DataTable GetBrandRep01Details(DateTime _fromDate, DateTime _toDate, string _Cust, string _Exec, string _DocTp, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _Com, string _Pc, string _User, string _repTp)
        {
            // Sanjeewa 03-03-2014         
            OracleParameter[] param = new OracleParameter[16];
            param[0] = new OracleParameter("n_cursor", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fromDate.Date;
            (param[2] = new OracleParameter("tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _toDate.Date;
            (param[3] = new OracleParameter("IN_CUST", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Cust;
            (param[4] = new OracleParameter("IN_EXEC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Exec;
            (param[5] = new OracleParameter("IN_DOCTYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _DocTp;
            (param[6] = new OracleParameter("IN_ITEMCODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ItemCode;
            (param[7] = new OracleParameter("IN_BRAND", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Brand;
            (param[8] = new OracleParameter("IN_MODEL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Model;
            (param[9] = new OracleParameter("IN_ITEMCAT1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Cat1;
            (param[10] = new OracleParameter("IN_ITEMCAT2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Cat2;
            (param[11] = new OracleParameter("IN_ITEMCAT3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Cat3;
            (param[12] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Com;
            (param[13] = new OracleParameter("in_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Pc;
            (param[14] = new OracleParameter("IN_USER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _User;
            (param[15] = new OracleParameter("in_reptp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _repTp;

            DataTable _dtResults;
            _dtResults = QueryDataTable("tbl", "pkg_brand_rep.proc_rep_01", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }


        #endregion

        public DataTable get_BulkJobPrint(string _com)
        {//Sanjeewa
            OracleParameter[] param = new OracleParameter[1];

            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "sp_get_bulk_job_print", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        public int get_UpdateBulkJobPrint(string _Job)
        {//Sanjeewa
            OracleParameter[] param = new OracleParameter[2];

            (param[0] = new OracleParameter("in_job", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Job;
            param[1] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            Int32 result = UpdateRecords("sp_update_bulk_job_print", CommandType.StoredProcedure, param);

            return result;
        }

    }

}
