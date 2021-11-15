using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.Sql;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;


namespace EMS_Upload_Console
{
    class Conn
    {
        public OracleConnection oConnEMS;
        public OracleTransaction oTrEMS;

        public OracleDataAdapter oAdapter;

        public OracleConnection oConnSCM;
        public OracleTransaction oTrSCM;

        public OracleConnection oConnHMC;
        public OracleTransaction oTrHMC;

        public OracleConnection oConnREP;
        public OracleTransaction oTrREP;

        public MySqlConnection oConnMySql;
        public MySqlTransaction oTrMySql;

        public SqlConnection oConnSql;
        public SqlTransaction  oTrSql;

        public bool _isTr = false;
        string ConnectionString = "";

        #region Established Connections
        public Conn()
        {
            ConnectionString = GetEMSConnectionString();
            oConnEMS = new OracleConnection(ConnectionString);

            ConnectionString = GetSCMConnectionString();
             oConnSCM = new OracleConnection(ConnectionString);

             ConnectionString = GetHMCConnectionString();
             oConnHMC = new OracleConnection(ConnectionString);

             ConnectionString = GetRepDBConnectionString();
             oConnREP = new OracleConnection(ConnectionString);

             ConnectionString = GetMySqlConnectionString();
             oConnMySql = new MySqlConnection(ConnectionString);
             
            oAdapter = null;
            oTrEMS = null;
        }
        #endregion

        #region Get Connection String
        public string GetEMSConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["ConnEMS"].ConnectionString;
        }

        public string GetHMCConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["ConnHMC"].ConnectionString;
        }


        public string GetMySqlConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["ConnBDL"].ConnectionString;
        }

        public string GetSqlConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["ConnWellaSer"].ConnectionString;
        }

        public string GetSCMConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["ConnSCM"].ConnectionString;
        }
        public string GetRepDBConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["ConnReportsdb"].ConnectionString;
        }
        public string GetRepDBHMCConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["ConnReportsdbHMC"].ConnectionString;
        }
        #endregion

        #region Open Connection

        #region SCM2
        public void OpenEMS(Int16 _db)
        {
            Boolean _isOpen = false;
            if (oConnEMS.State != ConnectionState.Open)
            {
                if (_db == 1)
                {
                    oConnEMS = new OracleConnection(GetEMSConnectionString().ToString());
                }
                else
                { oConnEMS = new OracleConnection(GetEMSConnectionString().ToString());
                }
                oConnEMS.Open();
            }

            if (oConnEMS.State == ConnectionState.Open)
                _isOpen = true;
            else _isOpen = false;


            _isTr = false;
           // return _isOpen;
        }

        public void EmsBegin(Int16 _db)
        {
            if (oConnEMS.State != ConnectionState.Open)
            {
                if (_db == 1)
                {
                    oConnEMS = new OracleConnection(GetEMSConnectionString().ToString());
                }
                else
                {
                    oConnEMS = new OracleConnection(GetEMSConnectionString().ToString());
                }

                oConnEMS.Open();
                oTrEMS = oConnEMS.BeginTransaction();
               _isTr = true;
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
        #endregion

        #region SCM
        public void OpenSCM()
        {
            if (oConnSCM.State != ConnectionState.Open)
            {
                oConnSCM = new OracleConnection(GetSCMConnectionString().ToString());
                oConnSCM.Open();
            }
        }

        public void ScmBegin()
        {
            if (oConnSCM.State != ConnectionState.Open)
            {
                oConnSCM = new OracleConnection(GetSCMConnectionString().ToString());
                oConnSCM.Open();
                oTrSCM = oConnSCM.BeginTransaction();
            }
        }

        public void ScmCommit()
        {
            oTrSCM.Commit();
            oConnSCM.Close();
        }

        public void ScmRollback()
        {
            oTrSCM.Rollback();
            oConnSCM.Close();
        }
        #endregion

        #region HMC
        public void OpenHMC(Int16 _db)
        {
            if (oConnHMC.State != ConnectionState.Open)
            {
                if (_db==1)
                { oConnHMC = new OracleConnection(GetHMCConnectionString().ToString()); }
                else { oConnHMC = new OracleConnection(GetHMCConnectionString().ToString()); }

                oConnHMC.Open();
            }
        }

        public void HMCBegin(Int16 _db)
        {
            if (oConnHMC.State != ConnectionState.Open)
            {
                if (_db == 1)
                { oConnHMC = new OracleConnection(GetHMCConnectionString().ToString()); }
                else { oConnHMC = new OracleConnection(GetEMSConnectionString().ToString()); }
                oConnHMC.Open();
                oTrHMC = oConnHMC.BeginTransaction();
            }
        }

        public void HMCCommit()
        {
            oTrHMC.Commit();
            oConnHMC.Close();
        }

        public void HMCRollback()
        {
            oTrHMC.Rollback();
            oConnHMC.Close();
        }
        #endregion

        #region MySql
        public void OpenMySql()
        {
            if (oConnMySql.State != ConnectionState.Open)
            {
                oConnMySql = new MySqlConnection(GetMySqlConnectionString().ToString());
               oConnMySql.Open();
            }
        }

        public void  MySqlBegin()
        {
            if (oConnMySql.State != ConnectionState.Open)
            {
                oConnMySql = new MySqlConnection(GetMySqlConnectionString().ToString());
                oConnMySql.Open();
                oTrMySql = oConnMySql.BeginTransaction();
            }
        }

        public void MySqlCommit()
        {
            oTrMySql.Commit();
            oConnMySql.Close();
        }

        public void MySqlRollback()
        {
             oTrMySql.Rollback();
            oConnMySql.Close();
        }
        #endregion

        #region SQL - Wellawatte
        public void OpenSql()
        {
            if (oConnSql== null)
            {
                oConnSql = new SqlConnection(GetSqlConnectionString().ToString());
                oConnSql.Open();
            }
            else
            {
                if (oConnSql.State != ConnectionState.Open)
                {
                    oConnSql = new SqlConnection(GetSqlConnectionString().ToString());
                    oConnSql.Open();
                }
            }
        }

        public void SqlBegin()
        {
            if (oConnSql == null)
            {
                oConnSql = new SqlConnection(GetSqlConnectionString().ToString());
                oConnSql.Open();
                oTrSql = oConnSql.BeginTransaction();
            }
            else
            {
                if (oConnSql.State != ConnectionState.Open)
                {
                    oConnSql = new SqlConnection(GetSqlConnectionString().ToString());
                    oConnSql.Open();
                    oTrSql = oConnSql.BeginTransaction();
                }
            }
        }

        public void SqlCommit()
        {
            oTrSql.Commit();
            oConnSql.Close();
        }

        public void SqlRollback()
        {
            oTrSql.Rollback();
            oConnSql.Close();
        }
        #endregion


        #region ReportDB
        public void OpenREP(Int16 _db)
        {
            if (oConnREP.State != ConnectionState.Open)
            {
                if (_db==1)
                { oConnREP = new OracleConnection(GetRepDBHMCConnectionString().ToString()); }
                else { oConnREP = new OracleConnection(GetRepDBConnectionString().ToString()); }
                oConnREP.Open();
            }
        }

        public void REPBegin(Int16 _db)
        {
            if (oConnREP.State != ConnectionState.Open)
            {
                 
                if (_db==1)
                { oConnREP = new OracleConnection(GetRepDBHMCConnectionString().ToString()); }
                else { oConnREP = new OracleConnection(GetRepDBConnectionString().ToString()); }
                oConnREP.Open();
                oTrREP = oConnREP.BeginTransaction();
            }
        }

        public void REPCommit()
        {
            oTrREP.Commit();
            oConnREP.Close();
        }

        public void REPRollback()
        {
            oTrREP.Rollback();
            oConnREP.Close();
        }
        #endregion

        #endregion

        #region Close Connection
        //EMS
        public void ConnectionCloseEMS()
        {
            if (oConnEMS.State == ConnectionState.Open)
            {
                oConnEMS.FlushCache();
                oConnEMS.Close();
                //oConnEMS.Dispose();
                //OracleConnection.ClearAllPools();
            }
        }
        //SCM
        public void ConnectionCloseSCM()
        {
            if (oConnSCM.State == ConnectionState.Open)
            {
                oConnSCM.Close();
                //oConnSCM.Dispose();
                //OracleConnection.ClearAllPools();
            }
        }
        public void ConnectionCloseSQL()
        {
            if (oConnSql.State == ConnectionState.Open)
            {
                oConnSql.Close();
                //oConnSCM.Dispose();
                //OracleConnection.ClearAllPools();
            }
        }

        #endregion

        #region Update Command

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

        #endregion
    }
}
