using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Data.SqlClient;  
using FF.BusinessObjects.Sales;
using FF.BusinessObjects;
using System.Transactions;
using System.Globalization;

namespace FF.DataAccessLayer
{
    public class POSCommonDAL : PosDAL
    {

        public DataTable CheckIsAodReceived(string _document)
        {
            ConnectionOpen();
            DataTable _dt = null; 

            string _sql = "SELECT aod_Number AS AODIN_NO FROM IN_T_AOD WHERE (aod_Direction = 'I') AND (aod_AOD_In_Ref =@p_aodout) AND (aod_Status <> 'C')";
            SqlCommand _sCom = new SqlCommand(_sql, sConnection) { CommandType = CommandType.Text };
            _sCom.Parameters.Add("@p_aodout", SqlDbType.VarChar).Value = _document;
            SqlDataAdapter adp = new SqlDataAdapter(_sCom);
            DataSet ds = new DataSet();
            adp.Fill(ds, "IN_T_AOD");

            _dt = ds.Tables["IN_T_AOD"];

            _sql = "SELECT aod_Number AS AODIN_NO FROM IN_T_AOD_TEMP WHERE (aod_Number =@p_aodout) AND (aod_Status = 'F')";
            _sCom = new SqlCommand(_sql, sConnection) { CommandType = CommandType.Text };
            _sCom.Parameters.Add("@p_aodout", SqlDbType.VarChar).Value = _document;
            adp = new SqlDataAdapter(_sCom);
            ds = new DataSet();
            adp.Fill(ds, "IN_T_AOD");

            _dt.Merge(ds.Tables["IN_T_AOD"]);

            adp.Dispose();
            ConnectionClose();
            return _dt;
        }



    }
}
