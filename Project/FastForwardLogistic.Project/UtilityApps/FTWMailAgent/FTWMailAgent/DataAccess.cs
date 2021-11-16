using FTWMailAgent.Model;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTWMailAgent
{
    class DataAccess : DBConnection
    {
        public string Query;
        internal List<MST_MODULE_CONF> getRunningMethod()
        {
            try
            {
                DataTable dt = new DataTable();
                List<MST_MODULE_CONF> conf = new List<MST_MODULE_CONF>();
                OracleDataReader OrdMovCNo;
                Query = "SELECT * FROM MST_MODULE_CONF";
                OpenEMS();
                OracleCommand _oCom = new OracleCommand(Query, oConnEMS) { CommandType = CommandType.Text, BindByName = true };
                OrdMovCNo = _oCom.ExecuteReader();
                if (OrdMovCNo.HasRows == true)
                {
                    dt.Load(OrdMovCNo);
                    if (dt.Rows.Count > 0)
                    {
                        conf = DataTableExtensions.ToGenericList<MST_MODULE_CONF>(dt, MST_MODULE_CONF.Converter);
                    }
                }
                OrdMovCNo.Close();
                ConnectionCloseEMS();
                return conf;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal MST_MODULE_CONF getRunningMethodCof(string method)
        {
            try
            {
                Query = String.Empty;
                DataTable dt = new DataTable();
                MST_MODULE_CONF conf = new MST_MODULE_CONF();
                OracleDataReader OrdMovCNo;
                Query = "SELECT * FROM MST_MODULE_CONF WHERE MMC_MOD_CD='" + method + "'";
                OpenEMS();
                OracleCommand _oCom = new OracleCommand(Query, oConnEMS) { CommandType = CommandType.Text, BindByName = true };
                OrdMovCNo = _oCom.ExecuteReader();
                if (OrdMovCNo.HasRows == true)
                {
                    dt.Load(OrdMovCNo);
                    if (dt.Rows.Count > 0)
                    {
                        conf = DataTableExtensions.ToGenericList<MST_MODULE_CONF>(dt, MST_MODULE_CONF.Converter)[0];
                    }
                }
                OrdMovCNo.Close();
                ConnectionCloseEMS();
                return conf;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal bool updateConfTable(string module, DateTime date)
        {
            try
            {
                Query = "UPDATE MST_MODULE_CONF SET MMC_LAST_RUN_DT=to_date('" + date.ToString() + "','dd/Mon/yyyy hh:mi:ss AM')  WHERE MMC_MOD_CD= '" + module + "' ";
                OpenEMS();
                OracleCommand _oCom = new OracleCommand(Query, oConnEMS) { CommandType = CommandType.Text, BindByName = true };
                _oCom.ExecuteNonQuery();
                ConnectionCloseEMS();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal string getComDesc(string com)
        {
            try
            {
                OpenEMS();
                OracleParameter[] param = new OracleParameter[2];

                (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
                param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResultsItm = QueryDataTable("tbl", "SP_GET_COMPDESC", CommandType.StoredProcedure, false, param);

                ConnectionCloseEMS();
                return _dtResultsItm.Rows[0]["MC_DESC"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal List<MST_ALERT_CRITERIA> getUserMailCriteria(string modcd)
        {
            try
            {
                DataTable dt = new DataTable();
                List<MST_ALERT_CRITERIA> USERS = new List<MST_ALERT_CRITERIA>();
                OracleDataReader OrdMovCNo;
                Query = "SELECT alc_com,alc_module,alc_criteria_type,alc_code,alc_brand,alc_ca1,alc_ca2,alc_ca3,alc_ca4,alc_ca5,alc_late_noof_dt FROM mst_alert_criteria where alc_module=:modcd group by alc_module,alc_criteria_type,alc_code,alc_com,alc_brand,alc_ca1,alc_ca2,alc_ca3,alc_ca4,alc_ca5,alc_late_noof_dt";
                OpenEMS();
                OracleCommand _oCom = new OracleCommand(Query, oConnEMS) { CommandType = CommandType.Text, BindByName = true };
                _oCom.Parameters.Add(":modcd", OracleDbType.NVarchar2).Value = modcd;
                OrdMovCNo = _oCom.ExecuteReader();
                if (OrdMovCNo.HasRows == true)
                {
                    dt.Load(OrdMovCNo);
                    if (dt.Rows.Count > 0)
                    {
                        USERS = DataTableExtensions.ToGenericList<MST_ALERT_CRITERIA>(dt, MST_ALERT_CRITERIA.ConverterPty);
                    }
                }
                OrdMovCNo.Close();
                ConnectionCloseEMS();
                return USERS;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal List<MST_ALERT_CRITERIA> getCriterialUSer(string modeule)
        {
            try
            {
                DataTable dt = new DataTable();
                List<MST_ALERT_CRITERIA> USERS = new List<MST_ALERT_CRITERIA>();
                OracleDataReader OrdMovCNo;
                Query = "SELECT * FROM MST_ALERT_CRITERIA WHERE ALC_MODULE=:modeule";
                OpenEMS();
                OracleCommand _oCom = new OracleCommand(Query, oConnEMS) { CommandType = CommandType.Text, BindByName = true };
                _oCom.Parameters.Add(":modeule", OracleDbType.NVarchar2).Value = modeule;
                OrdMovCNo = _oCom.ExecuteReader();
                if (OrdMovCNo.HasRows == true)
                {
                    dt.Load(OrdMovCNo);
                    if (dt.Rows.Count > 0)
                    {
                        USERS = DataTableExtensions.ToGenericList<MST_ALERT_CRITERIA>(dt, MST_ALERT_CRITERIA.Converter);
                    }
                }
                OrdMovCNo.Close();
                ConnectionCloseEMS();
                return USERS;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal DataTable getUnsettledPettyCaseRequsts(string crittyp, string crival, int lteDate)
        {
          
                try
                {
                    OpenEMS();
                    OracleParameter[] param = new OracleParameter[3];

                    (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = crival;
                    (param[1] = new OracleParameter("p_dt", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lteDate;
                     param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                     DataTable _dtResultsItm = QueryDataTable("tbl", "SP_GET_PENDPTYSETLLE", CommandType.StoredProcedure, false, param);
              
                    ConnectionCloseEMS();
                    return _dtResultsItm;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
        
        }

        internal DataTable getUnsettledPInvoice(string crittyp, string crival, int lteDate)
        {
            try
            {
                OpenEMS();
                OracleParameter[] param = new OracleParameter[3];

                (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = crival;
                (param[1] = new OracleParameter("p_pendt", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lteDate;
                param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResultsItm = QueryDataTable("tbl", "SP_GET_UNSETINVOICE", CommandType.StoredProcedure, false, param);

                ConnectionCloseEMS();
                return _dtResultsItm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        internal DataTable getExcecIds( string crival, int lteDate)
        {
            try
            {
                OpenEMS();
                OracleParameter[] param = new OracleParameter[3];

                (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = crival;
                (param[1] = new OracleParameter("p_dt", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lteDate;
                param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResultsItm = QueryDataTable("tbl", "SP_GET_PTYREQEXEC", CommandType.StoredProcedure, false, param);

                ConnectionCloseEMS();
                return _dtResultsItm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal DataTable getUnsettledPettyCaseRequstsUser(string crittyp, string crival, int lteDate, string exce)
        {
            try
            {
                OpenEMS();
                OracleParameter[] param = new OracleParameter[4];

                (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = crival;
                (param[1] = new OracleParameter("p_dt", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lteDate;
                (param[2] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = exce;
                param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResultsItm = QueryDataTable("tbl", "SP_GET_PENDPTYSETLLEUSER", CommandType.StoredProcedure, false, param);

                ConnectionCloseEMS();
                return _dtResultsItm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal DataTable getExcecInvoiceIds( string company, int minsnddt)
        {
            try
            {
                OpenEMS();
                OracleParameter[] param = new OracleParameter[3];

                (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[1] = new OracleParameter("p_pendt", OracleDbType.Int32, null, ParameterDirection.Input)).Value = minsnddt;
                param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResultsItm = QueryDataTable("tbl", "SP_GET_UNSETINVOICEUSER", CommandType.StoredProcedure, false, param);

                ConnectionCloseEMS();
                return _dtResultsItm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal DataTable getUnsettledPInvoiceUser( string crival, int lteDate,string exce)
        {
            try
            {
                OpenEMS();
                OracleParameter[] param = new OracleParameter[4];

                (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = crival;
                (param[1] = new OracleParameter("p_pendt", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lteDate;
                (param[2] = new OracleParameter("p_excec", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = exce;
                param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResultsItm = QueryDataTable("tbl", "SP_GET_UNSETINVOICEUSERDET", CommandType.StoredProcedure, false, param);

                ConnectionCloseEMS();
                return _dtResultsItm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }   
    }
}
