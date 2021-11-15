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
using Oracle.DataAccess;
using Oracle.DataAccess.Client;

namespace FF.DataAccessLayer
{
   public class POSSCMUploadDAL
    {
        #region Variables
        OracleConnection oCnn;
        OracleCommand oCmd;
        OracleTransaction oTr;
        OracleDataAdapter oAdp;

        DataSet Ods;
        DataTable Odt1;
        DataTableReader Odtr1;

        SqlConnection sCnn;
        SqlCommand sCmd;
        SqlTransaction sTr;
        SqlDataAdapter sAdp;

        string oradb1 = System.Configuration.ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
        string sqldb1 = System.Configuration.ConfigurationManager.ConnectionStrings["ConnPOS"].ConnectionString;

        string _sql = string.Empty;
        #endregion

        #region db connections
        void orBegin()
        {
            oCnn = new OracleConnection(oradb1);
            oCnn.Open();
            oTr = oCnn.BeginTransaction();
        }

        void sqlBegin()
        {
            sCnn = new SqlConnection(sqldb1);
            sCnn.Open();
            sTr = sCnn.BeginTransaction();
        }

        void orCommit()
        {
            oTr.Commit();
            oCnn.Close();
            oCnn.Dispose();
        }

        void sqlCommit()
        {
            sTr.Commit();
            sCnn.Close();
            sCnn.Dispose();
        }

        void orRollback()
        {
            oTr.Rollback();
            oCnn.Close();
            oCnn.Dispose();
        }

        void sqlRollback()
        {
            sTr.Rollback();
            sCnn.Close();
            sCnn.Dispose();
        }
        #endregion

      public Int32 Account_Upload_Process(string _loc,string _acc,DateTime _dt,out string _err)
        {
            //try
            //{
                Int32 _effect = 0;
                orBegin();
                sqlBegin();

                string sql = "";
                string _com = "ABL";
                string _pc = _loc;
                string _error = "";
                Int32 _funcEffect = 0;

                OracleCommand _oCom = new OracleCommand();
                SqlCommand _sCom_1 = new SqlCommand();

                //string sql_1 = "SELECT * FROM GN_R_Location_SCM2 where loc_code=@loc";
                //_sCom_1 = new SqlCommand(sql_1, sCnn);
                //_sCom_1.CommandType = CommandType.Text;
                //_sCom_1.CommandTimeout = 500;
                //_sCom_1.Transaction = sTr;
                //_sCom_1.Parameters.Add("@loc", SqlDbType.NChar).Value = _loc;
                //sAdp = new SqlDataAdapter(_sCom_1);
                //Ods = new DataSet();
                //sAdp.Fill(Ods, "GN_R_LOCATION_SCM2");


                sql = "SELECT * FROM HPT_ACC WHERE hpa_com=:com and hpa_acc_no=:acc";
                _oCom = new OracleCommand(sql, oCnn);
                _oCom.CommandType = CommandType.Text;
                _oCom.Transaction = oTr;
                _oCom.Parameters.Add(":com", OracleDbType.NVarchar2).Value = _com;
                _oCom.Parameters.Add(":acc", OracleDbType.NVarchar2).Value = _acc;
                oAdp = new OracleDataAdapter(_oCom);
                Ods = new DataSet();
                oAdp.Fill(Ods, "GN_R_LOCATION_SCM2");

                if (Ods.Tables["GN_R_LOCATION_SCM2"].Rows.Count <= 0)
                {
                    sql = "DELETE FROM HPT_ACC WHERE hpa_com=:com and hpa_acc_no=:acc";
                    _oCom = new OracleCommand(sql, oCnn) { CommandType = CommandType.Text, BindByName = true };
                    _oCom.Transaction = oTr;
                    _oCom.Parameters.Add(":com", OracleDbType.NVarchar2).Value = _com;
                    _oCom.Parameters.Add(":acc", OracleDbType.NVarchar2).Value = _acc;
                    _oCom.ExecuteNonQuery();

                    sql = "SELECT * FROM SAT_RECEIPT WHERE sar_com_cd=:com and sar_acc_no =:acc";
                    _oCom = new OracleCommand(sql, oCnn);
                    _oCom.CommandType = CommandType.Text;
                    _oCom.Transaction = oTr;
                    _oCom.Parameters.Add(":com", OracleDbType.NVarchar2).Value = _com;
                    _oCom.Parameters.Add(":acc", OracleDbType.NVarchar2).Value = _acc;
                    oAdp = new OracleDataAdapter(_oCom);
                    Ods = new DataSet();
                    oAdp.Fill(Ods, "SAT_RECEIPT");

                    if (Ods.Tables["SAT_RECEIPT"].Rows.Count > 0)
                    {
                        foreach (DataRow dr in Ods.Tables["SAT_RECEIPT"].Rows)
                        {
                            sql = "DELETE FROM SAT_RECEIPTITM WHERE SARD_SEQ_NO = :seqNo";
                            _oCom = new OracleCommand(sql, oCnn) { CommandType = CommandType.Text, BindByName = true };
                            _oCom.Transaction = oTr;
                            _oCom.Parameters.Add(":seqNo", OracleDbType.Int32).Value = Convert.ToInt32(dr["SAR_SEQ_NO"]);
                            _oCom.ExecuteNonQuery();

                            sql = "DELETE FROM SAT_RECEIPT WHERE SAR_SEQ_NO = :seqNo";
                            _oCom = new OracleCommand(sql, oCnn) { CommandType = CommandType.Text, BindByName = true };
                            _oCom.Transaction = oTr;
                            _oCom.Parameters.Add(":seqNo", OracleDbType.Int32).Value = Convert.ToInt32(dr["SAR_SEQ_NO"]);
                            _oCom.ExecuteNonQuery();
                        }
                    }

                    sql = "SELECT hpa_Number FROM HP_T_Accounts WHERE hpa_Number =@acc";
                    _sCom_1 = new SqlCommand(sql, sCnn);
                    _sCom_1.CommandType = CommandType.Text;
                    _sCom_1.CommandTimeout = 500;
                    _sCom_1.Transaction = sTr;
                    _sCom_1.Parameters.Add("@acc", SqlDbType.NChar).Value = _acc;
                    sAdp = new SqlDataAdapter(_sCom_1);
                    Ods = new DataSet();
                    sAdp.Fill(Ods, "HP_T_Accounts");

                    if (Ods.Tables["HP_T_Accounts"].Rows.Count > 0)
                    {
                        foreach (DataRow dr in Ods.Tables["HP_T_Accounts"].Rows)
                        {
                           _funcEffect = Create_Account(_loc, _acc, _dt.Date, out _error);
                           if (_funcEffect == -1)
                           {
                               orRollback();
                               sqlRollback();
                               _err = _error;
                               return _funcEffect;
                           }

                           _funcEffect = Create_Shedule(_loc, _acc, _dt.Date, out _error);
                           if (_funcEffect == -1)
                           {
                               orRollback();
                               sqlRollback();
                               _err = _error;
                               return _funcEffect;
                           }

                           _funcEffect = Create_Customer(_loc, _acc, _dt.Date, out _error);
                           if (_funcEffect == -1)
                           {
                               orRollback();
                               sqlRollback();
                               _err = _error;
                               return _funcEffect;
                           }
                            
                           //_funcEffect = Create_Receipt(_loc, _acc, _dt.Date, out _error);
                           //if (_funcEffect == -1)
                           //{
                           //    orRollback();
                           //    sqlRollback();
                           //    _err = _error;
                           //    return _funcEffect;
                           //}

                           _funcEffect = Create_Adj(_loc, _acc, _dt.Date, out _error);
                           if (_funcEffect == -1)
                           {
                               orRollback();
                               sqlRollback();
                               _err = _error;
                               return _funcEffect;
                           }
                           _funcEffect = Create_Txn(_loc, _acc, _dt.Date, out _error);
                           if (_funcEffect == -1)
                           {
                               orRollback();
                               sqlRollback();
                               _err = _error;
                               return _funcEffect;
                           }
                           _funcEffect = Create_Acc_Log(_loc, _acc, _dt.Date, out _error);
                           if (_funcEffect == -1)
                           {
                               orRollback();
                               sqlRollback();
                               _err = _error;
                               return _funcEffect;
                           }
                        }
                    }

                }
                else
                {
                    //MessageBox.Show("Accsess denide.", "Message");
                }

                if (_funcEffect == 1)
                {
                    orCommit();
                    sqlCommit();
                    _effect = 1;
                    _err = "Upload Success!";
                    return _effect;
                }
                else
                {
                    orRollback();
                    sqlRollback();
                    _err = "Upload Fail" + _error;
                    return -1;
                }
            //}
            //catch (Exception e)
            //{
            //    orRollback();
            //    sqlRollback();
            //    _err = e.Message.ToString() + _error;
            //    return -1;
            //    //MessageBox.Show(e.Message, "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    //Application.Exit();
            //}
          
        }

      public String Get_Current_Acc_Loc(string _accNo, DateTime _dt, string _loc)
      {
          SqlCommand _sCom_1 = new SqlCommand();
          OracleCommand _oCom = new OracleCommand();
          string sql = "";
          string cur_loc = "";

          sql = "SELECT * FROM HP_T_Accounts_Log WHERE HP_T_Accounts_Log.log_Accno =@acc " +
                " AND HP_T_Accounts_Log.log_Date <=@recDt AND HP_T_Accounts_Log.log_Transfer_Date >=@recDt";

          _sCom_1 = new SqlCommand(sql, sCnn);
          _sCom_1.CommandType = CommandType.Text;
          _sCom_1.Transaction = sTr;
          _sCom_1.Parameters.Add("@acc", SqlDbType.NChar).Value = _accNo;
          _sCom_1.Parameters.Add("@recDt", SqlDbType.DateTime).Value = _dt.Date;
          sAdp = new SqlDataAdapter(_sCom_1);
          Ods = new DataSet();
          sAdp.Fill(Ods, "HP_T_Accounts_Log");

          if (Ods.Tables["HP_T_Accounts_Log"].Rows.Count > 0)
          {
              foreach (DataRow dr in Ods.Tables["HP_T_Accounts_Log"].Rows)
              {
                  cur_loc = dr["log_Location"].ToString();
              }
          }
          else
          {
              cur_loc = _loc;
          }

          return cur_loc;
      }


      public Int32 Create_Acc_Log(string _loc, string _acc, DateTime _dt, out string _err)
      {
          try
          {
              SqlCommand _sCom_1 = new SqlCommand();
              OracleCommand _oCom = new OracleCommand();
              string sql = "";
              DateTime _recDt = Convert.ToDateTime(_dt).Date;
              string strLoc = "";
              string strSubCate = "";
              decimal TMP_SEQ = 0;
              decimal _serChg = 0;
              string _hpStatus = "";
              _err = "";
              Int32 _effect = 0;
              DateTime _resheDt = Convert.ToDateTime("31/Dec/9999").Date;


              sql = "DELETE FROM HPT_ACC_LOG WHERE HAL_PC =:pc AND HAL_ACC_NO =:acc";
              _oCom = new OracleCommand(sql, oCnn) { CommandType = CommandType.Text, BindByName = true };
              _oCom.Transaction = oTr;
              _oCom.Parameters.Add(":pc", OracleDbType.NVarchar2).Value = _loc;
              _oCom.Parameters.Add(":acc", OracleDbType.NVarchar2).Value = _acc;
              _oCom.ExecuteNonQuery();

              sql = "SELECT * " +
              "   FROM HP_T_Accounts INNER JOIN " +
              "   SA_T_Sales_Header ON HP_T_Accounts.hpa_Number = SA_T_Sales_Header.sa_AccountNo INNER JOIN " +
              "   HP_T_Accounts_Log ON HP_T_Accounts_Log.log_AccNo = HP_T_Accounts.hpa_Number AND SA_T_Sales_Header.sa_Txn_Date < HP_T_Accounts_Log.log_Transfer_Date " +
              "   WHERE (HP_T_Accounts_Log.log_Location in (SELECT loc_Code FROM GN_R_Location WHERE (loc_Acc_No =@loc))) " +
              "   AND (SA_T_Sales_Header.sa_Txn_Date <=@recDt) " +
              "   AND SA_T_Sales_Header.sa_Txn_Date < HP_T_Accounts_Log.log_Transfer_Date " +
              "   AND (HP_T_Accounts_Log.log_Date <=@recDt) " +
              "   AND (HP_T_Accounts.hpa_Rescheduled = 0) " +
              "   AND (SA_T_Sales_Header.sa_Status <> 'C') " +
              "   AND (SA_T_Sales_Header.sa_Type = 'HS') " +
              "   AND (hpa_Number =@acc)";

              _sCom_1 = new SqlCommand(sql, sCnn);
              _sCom_1.CommandType = CommandType.Text;
              _sCom_1.Transaction = sTr;
              _sCom_1.Parameters.Add("@loc", SqlDbType.NChar).Value = _loc;
              _sCom_1.Parameters.Add("@recDt", SqlDbType.DateTime).Value = _recDt.Date;
              _sCom_1.Parameters.Add("@acc", SqlDbType.NChar).Value = _acc;
              sAdp = new SqlDataAdapter(_sCom_1);
              Ods = new DataSet();
              sAdp.Fill(Ods, "Acc_Det");

              if (Ods.Tables["Acc_Det"].Rows.Count > 0)
              {
                  foreach (DataRow dr in Ods.Tables["Acc_Det"].Rows)
                  {
                      strLoc = Get_Current_Acc_Loc(dr["hpa_Number"].ToString(), Convert.ToDateTime(dr["sa_Txn_Date"]).Date, dr["hpa_Location"].ToString());

                      sql = "SELECT * FROM GN_R_LOCATION WHERE LOC_CODE =@strLoc";

                      _sCom_1 = new SqlCommand(sql, sCnn);
                      _sCom_1.CommandType = CommandType.Text;
                      _sCom_1.Transaction = sTr;
                      _sCom_1.Parameters.Add("@strLoc", SqlDbType.NChar).Value = strLoc;
                      sAdp = new SqlDataAdapter(_sCom_1);
                      Ods = new DataSet();
                      sAdp.Fill(Ods, "GN_R_LOCATION");

                      if (Ods.Tables["GN_R_LOCATION"].Rows.Count > 0)
                      {
                          foreach (DataRow dr1 in Ods.Tables["GN_R_LOCATION"].Rows)
                          {
                              strSubCate = dr1["loc_Sub_Category"].ToString();
                          }
                      }


                      sql = "INSERT INTO HPT_ACC_LOG (HAL_SEQ_NO,HAL_ACC_NO,HAL_COM,HAL_PC,HAL_SEQ,HAL_SA_SUB_TP,HAL_LOG_DT,HAL_REV_STUS,HPA_ACC_CRE_DT,HAL_GRUP_CD,HAL_INVC_NO,HAL_SCH_TP,HAL_SCH_CD,HAL_TERM,HAL_INTR_RT,HAL_DP_COMM,HAL_INST_COMM,HAL_CASH_VAL,HAL_TOT_VAT,HAL_NET_VAL,HAL_DP_VAL,HAL_AF_VAL,HAL_TOT_INTR,HAL_SER_CHG,HAL_HP_VAL,HAL_TC_VAL, " +
                            " HAL_INIT_INS,HAL_INIT_VAT,HAL_INIT_STM,HAL_INIT_SER_CHG,HAL_INST_INS,HAL_INST_VAT,HAL_INST_STM,HAL_INST_SER_CHG,HAL_BUY_VAL,HAL_OTH_CHG,HAL_STUS,HAL_CLS_DT,HAL_RV_DT,HAL_RLS_DT,HAL_ECD_STUS,HAL_ECD_TP,HAL_MGR_CD,HAL_IS_RSCH,HAL_RSCH_DT,HAL_BANK,HAL_FLAG,HAL_VAL_01,HAL_VAL_02,HAL_VAL_03,HAL_VAL_04,HAL_VAL_05,HAL_CRE_BY,HAL_CRE_DT) " +
                            " VALUES (:seq,:acc,:subCate,:loc,:accSeq,:subTp,:txnDt,:IsRev,:accCreDt,:groupSales,:invNo,:schemeTp,:scheme,:hpTerm,:hpIntRt,:hpVal0,:hpVal1,:hpCashPrice,:hpTotVat,:hpNetVal,:hpDownPay,:amtFinance,:totInt,:hpaSerChg,:hireVal,:totCash " +
                            ",:initInsu,:initVat,:initStamp,:initService,:instInsu,:instVAT,:instStamp,:instService,:buyBackChg,:otherChg,:hpStatus,:closeDt,:revertDt,:revReleaseDt,0,'',:responsOfficer,:reShedule,:reSheduleDt,'001','001',0,0,0,0,0 ,'UPLOAD',:curDt)";


                      TMP_SEQ = GetSerialID();
                      _oCom = new OracleCommand(sql, oCnn) { CommandType = CommandType.Text, BindByName = true };
                      _oCom.Transaction = oTr;
                      _oCom.Parameters.Add(":seq", OracleDbType.Int32).Value = TMP_SEQ;
                      _oCom.Parameters.Add(":acc", OracleDbType.NVarchar2).Value = dr["hpa_Number"].ToString();
                      _oCom.Parameters.Add(":subCate", OracleDbType.NVarchar2).Value = strSubCate;
                      _oCom.Parameters.Add(":loc", OracleDbType.NVarchar2).Value = strLoc;
                      _oCom.Parameters.Add(":accSeq", OracleDbType.Int32).Value = Convert.ToInt32(dr["hpa_SeqenceNo"]);
                      _oCom.Parameters.Add(":subTp", OracleDbType.NVarchar2).Value = dr["sa_SubType"].ToString();
                      _oCom.Parameters.Add(":txnDt", OracleDbType.Date).Value = Convert.ToDateTime(dr["sa_Txn_Date"]);
                      _oCom.Parameters.Add(":IsRev", OracleDbType.Int32).Value = Convert.ToInt32(dr["sa_IsReversal"]);
                      _oCom.Parameters.Add(":accCreDt", OracleDbType.Date).Value = Convert.ToDateTime(dr["hpa_CreatedDate"]).Date;
                      _oCom.Parameters.Add(":groupSales", OracleDbType.NVarchar2).Value = dr["hpa_GroupSale_Code"].ToString();
                      _oCom.Parameters.Add(":invNo", OracleDbType.NVarchar2).Value = dr["sa_InvoiceNo"].ToString();
                      _oCom.Parameters.Add(":schemeTp", OracleDbType.NVarchar2).Value = dr["hpa_SchemeType"].ToString();
                      _oCom.Parameters.Add(":scheme", OracleDbType.NVarchar2).Value = dr["hpa_Scheme"].ToString();
                      _oCom.Parameters.Add(":hpTerm", OracleDbType.Int32).Value = Convert.ToInt32(dr["hpa_Term"]);
                      _oCom.Parameters.Add(":hpIntRt", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_IntRate"]);
                      _oCom.Parameters.Add(":hpVal0", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Value_0"]);
                      _oCom.Parameters.Add(":hpVal1", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Value_1"]);
                      _oCom.Parameters.Add(":hpCashPrice", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Cash_Price"]);
                      _oCom.Parameters.Add(":hpTotVat", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Tot_VAT"]);
                      _oCom.Parameters.Add(":hpNetVal", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Net_Value"]);
                      _oCom.Parameters.Add(":hpDownPay", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Down_Payment"]);
                      _oCom.Parameters.Add(":amtFinance", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_AmountFinance"]);
                      _oCom.Parameters.Add(":totInt", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Tot_Int"]);
                      _serChg = Convert.ToDecimal(dr["hpa_Init_ServiceChg"]) + Convert.ToDecimal(dr["hpa_Inst_ServiceChg"]);
                      _oCom.Parameters.Add(":hpaSerChg", OracleDbType.Decimal).Value = _serChg;
                      _oCom.Parameters.Add(":hireVal", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Hire_Value"]);
                      _oCom.Parameters.Add(":totCash", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Total_Cash"]);
                      _oCom.Parameters.Add(":initInsu", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Init_Insu"]);
                      _oCom.Parameters.Add(":initVat", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Init_VAT"]);
                      _oCom.Parameters.Add(":initStamp", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Init_StampDuty"]);
                      _oCom.Parameters.Add(":initService", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Init_ServiceChg"]);
                      _oCom.Parameters.Add(":instInsu", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Inst_Insu"]);
                      _oCom.Parameters.Add(":instVAT", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Inst_VAT"]);
                      _oCom.Parameters.Add(":instStamp", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Inst_StampDuty"]);
                      _oCom.Parameters.Add(":instService", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Inst_ServiceChg"]);
                      _oCom.Parameters.Add(":buyBackChg", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_BuyBack_Charge"]);
                      _oCom.Parameters.Add(":otherChg", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Other_Charges"]);

                      if (dr["hpa_Status"].ToString() == "RV")
                      {
                          _hpStatus = "R";
                      }
                      else if (dr["hpa_Status"].ToString() == "O")
                      {
                          _hpStatus = "C";
                      }
                      else
                      {
                          _hpStatus = "A";
                      }
                      _oCom.Parameters.Add(":hpStatus", OracleDbType.NVarchar2).Value = _hpStatus;
                      _oCom.Parameters.Add(":closeDt", OracleDbType.Date).Value = Convert.ToDateTime(dr["hpa_ClosedDate"]);
                      _oCom.Parameters.Add(":revertDt", OracleDbType.Date).Value = Convert.ToDateTime(dr["hpa_RevertedDate"]);
                      _oCom.Parameters.Add(":revReleaseDt", OracleDbType.Date).Value = Convert.ToDateTime(dr["hpa_RevertReleasedDate"]);
                      _oCom.Parameters.Add(":responsOfficer", OracleDbType.NVarchar2).Value = dr["hpa_Responsible_Officer"].ToString();
                      _oCom.Parameters.Add(":reShedule", OracleDbType.Int32).Value = Convert.ToInt32(dr["hpa_Rescheduled"]);

                      if (Convert.ToInt32(dr["hpa_Rescheduled"]) == 1)
                      {
                          _resheDt = Convert.ToDateTime(dr["hpa_Rescheduled_Date"]).Date;
                      }
                      else
                      {
                          _resheDt = Convert.ToDateTime("31/Dec/9999").Date;
                      }
                      _oCom.Parameters.Add(":reSheduleDt", OracleDbType.Date).Value = Convert.ToDateTime(_resheDt).Date;
                      _oCom.Parameters.Add(":curDt", OracleDbType.Date).Value = DateTime.Now;
                      _oCom.ExecuteNonQuery();

                  }
              }

              sql = "SELECT * FROM  HP_T_Accounts INNER JOIN " +
                    " SA_T_Sales_Header ON HP_T_Accounts.hpa_Number = SA_T_Sales_Header.sa_AccountNo INNER JOIN " +
                    " HP_T_Accounts_Log on HP_T_Accounts_Log.log_AccNo=HP_T_Accounts.hpa_Number AND HP_T_Accounts_Log.log_Transfer_Date >= SA_T_Sales_Header.sa_Txn_Date AND HP_T_Accounts_Log.log_Date <= SA_T_Sales_Header.sa_Txn_Date " +
                    "WHERE (HP_T_Accounts_Log.log_Location in (SELECT loc_Code FROM GN_R_Location WHERE (loc_Acc_No =@loc))) " +
                    " AND (SA_T_Sales_Header.sa_Txn_Date <=@recDt) " +
                    " AND (SA_T_Sales_Header.sa_Txn_Date < HP_T_Accounts_Log.log_Transfer_Date) " +
                    " AND (HP_T_Accounts_Log.log_Date <=@recDt) " +
                    " AND (SA_T_Sales_Header.sa_SubType = 'RSCH' OR SA_T_Sales_Header.sa_SubType = 'EXO') " +
                    " AND (hpa_Number=@acc)";

              _sCom_1 = new SqlCommand(sql, sCnn);
              _sCom_1.CommandType = CommandType.Text;
              _sCom_1.Transaction = sTr;
              _sCom_1.Parameters.Add("@loc", SqlDbType.NChar).Value = _loc;
              _sCom_1.Parameters.Add("@recDt", SqlDbType.DateTime).Value = _recDt.Date;
              _sCom_1.Parameters.Add("@acc", SqlDbType.NChar).Value = _acc;
              sAdp = new SqlDataAdapter(_sCom_1);
              Ods = new DataSet();
              sAdp.Fill(Ods, "Acc_Det1");

              if (Ods.Tables["Acc_Det1"].Rows.Count > 0)
              {
                  foreach (DataRow dr in Ods.Tables["Acc_Det1"].Rows)
                  {
                      //If an account has rescheduled one time
                      if (Convert.ToDateTime(dr["sa_Txn_Date"]) == Convert.ToDateTime(dr["hpa_Rescheduled_Date"]))
                      {
                          strLoc = Get_Current_Acc_Loc(dr["hpa_Number"].ToString(), Convert.ToDateTime(dr["sa_Txn_Date"]).Date, dr["hpa_Location"].ToString());

                          sql = "SELECT * FROM GN_R_LOCATION WHERE LOC_CODE =@strLoc";

                          _sCom_1 = new SqlCommand(sql, sCnn);
                          _sCom_1.CommandType = CommandType.Text;
                          _sCom_1.Transaction = sTr;
                          _sCom_1.Parameters.Add("@strLoc", SqlDbType.NChar).Value = strLoc;
                          sAdp = new SqlDataAdapter(_sCom_1);
                          Ods = new DataSet();
                          sAdp.Fill(Ods, "GN_R_LOCATION");

                          if (Ods.Tables["GN_R_LOCATION"].Rows.Count > 0)
                          {
                              foreach (DataRow dr1 in Ods.Tables["GN_R_LOCATION"].Rows)
                              {
                                  strSubCate = dr1["loc_Sub_Category"].ToString();
                              }
                          }

                          sql = "INSERT INTO HPT_ACC_LOG (HAL_SEQ_NO,HAL_ACC_NO,HAL_COM,HAL_PC,HAL_SEQ,HAL_SA_SUB_TP,HAL_LOG_DT,HAL_REV_STUS,HPA_ACC_CRE_DT,HAL_GRUP_CD,HAL_INVC_NO,HAL_SCH_TP,HAL_SCH_CD,HAL_TERM,HAL_INTR_RT,HAL_DP_COMM,HAL_INST_COMM,HAL_CASH_VAL,HAL_TOT_VAT,HAL_NET_VAL,HAL_DP_VAL,HAL_AF_VAL,HAL_TOT_INTR,HAL_SER_CHG,HAL_HP_VAL,HAL_TC_VAL, " +
                            " HAL_INIT_INS,HAL_INIT_VAT,HAL_INIT_STM,HAL_INIT_SER_CHG,HAL_INST_INS,HAL_INST_VAT,HAL_INST_STM,HAL_INST_SER_CHG,HAL_BUY_VAL,HAL_OTH_CHG,HAL_STUS,HAL_CLS_DT,HAL_RV_DT,HAL_RLS_DT,HAL_ECD_STUS,HAL_ECD_TP,HAL_MGR_CD,HAL_IS_RSCH,HAL_RSCH_DT,HAL_BANK,HAL_FLAG,HAL_VAL_01,HAL_VAL_02,HAL_VAL_03,HAL_VAL_04,HAL_VAL_05,HAL_CRE_BY,HAL_CRE_DT) " +
                            " VALUES (:seq,:acc,:subCate,:loc,:accSeq,:subTp,:txnDt,:IsRev,:accCreDt,:groupSales,:invNo,:schemeTp,:scheme,:hpTerm,:hpIntRt,:hpVal0,:hpVal1,:hpCashPrice,:hpTotVat,:hpNetVal,:hpDownPay,:amtFinance,:totInt,:hpaSerChg,:hireVal,:totCash " +
                            ",:initInsu,:initVat,:initStamp,:initService,:instInsu,:instVAT,:instStamp,:instService,:buyBackChg,:otherChg,:hpStatus,:closeDt,:revertDt,:revReleaseDt,0,'',:responsOfficer,:reShedule,:reSheduleDt,'001','001',0,0,0,0,0 ,'UPLOAD',:curDt)";


                          TMP_SEQ = GetSerialID();
                          _oCom = new OracleCommand(sql, oCnn) { CommandType = CommandType.Text, BindByName = true };
                          _oCom.Transaction = oTr;
                          _oCom.Parameters.Add(":seq", OracleDbType.Int32).Value = TMP_SEQ;
                          _oCom.Parameters.Add(":acc", OracleDbType.NVarchar2).Value = dr["hpa_Number"].ToString();
                          _oCom.Parameters.Add(":subCate", OracleDbType.NVarchar2).Value = strSubCate;
                          _oCom.Parameters.Add(":loc", OracleDbType.NVarchar2).Value = strLoc;
                          _oCom.Parameters.Add(":accSeq", OracleDbType.Int32).Value = Convert.ToInt32(dr["hpa_SeqenceNo"]);
                          _oCom.Parameters.Add(":subTp", OracleDbType.NVarchar2).Value = dr["sa_SubType"].ToString();
                          _oCom.Parameters.Add(":txnDt", OracleDbType.Date).Value = Convert.ToDateTime(dr["sa_Txn_Date"]);
                          _oCom.Parameters.Add(":IsRev", OracleDbType.Int32).Value = Convert.ToInt32(dr["sa_IsReversal"]);
                          _oCom.Parameters.Add(":accCreDt", OracleDbType.Date).Value = Convert.ToDateTime(dr["hpa_CreatedDate"]).Date;
                          _oCom.Parameters.Add(":groupSales", OracleDbType.NVarchar2).Value = dr["hpa_GroupSale_Code"].ToString();
                          _oCom.Parameters.Add(":invNo", OracleDbType.NVarchar2).Value = dr["sa_InvoiceNo"].ToString();
                          _oCom.Parameters.Add(":schemeTp", OracleDbType.NVarchar2).Value = dr["hpa_SchemeType"].ToString();
                          _oCom.Parameters.Add(":scheme", OracleDbType.NVarchar2).Value = dr["hpa_Scheme"].ToString();
                          _oCom.Parameters.Add(":hpTerm", OracleDbType.Int32).Value = Convert.ToInt32(dr["hpa_Term"]);
                          _oCom.Parameters.Add(":hpIntRt", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_IntRate"]);
                          _oCom.Parameters.Add(":hpVal0", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Value_0"]);
                          _oCom.Parameters.Add(":hpVal1", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Value_1"]);
                          _oCom.Parameters.Add(":hpCashPrice", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Cash_Price"]);
                          _oCom.Parameters.Add(":hpTotVat", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Tot_VAT"]);
                          _oCom.Parameters.Add(":hpNetVal", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Net_Value"]);
                          _oCom.Parameters.Add(":hpDownPay", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Down_Payment"]);
                          _oCom.Parameters.Add(":amtFinance", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_AmountFinance"]);
                          _oCom.Parameters.Add(":totInt", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Tot_Int"]);
                          _serChg = Convert.ToDecimal(dr["hpa_Init_ServiceChg"]) + Convert.ToDecimal(dr["hpa_Inst_ServiceChg"]);
                          _oCom.Parameters.Add(":hpaSerChg", OracleDbType.Decimal).Value = _serChg;
                          _oCom.Parameters.Add(":hireVal", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Hire_Value"]);
                          _oCom.Parameters.Add(":totCash", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Total_Cash"]);
                          _oCom.Parameters.Add(":initInsu", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Init_Insu"]);
                          _oCom.Parameters.Add(":initVat", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Init_VAT"]);
                          _oCom.Parameters.Add(":initStamp", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Init_StampDuty"]);
                          _oCom.Parameters.Add(":initService", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Init_ServiceChg"]);
                          _oCom.Parameters.Add(":instInsu", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Inst_Insu"]);
                          _oCom.Parameters.Add(":instVAT", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Inst_VAT"]);
                          _oCom.Parameters.Add(":instStamp", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Inst_StampDuty"]);
                          _oCom.Parameters.Add(":instService", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Inst_ServiceChg"]);
                          _oCom.Parameters.Add(":buyBackChg", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_BuyBack_Charge"]);
                          _oCom.Parameters.Add(":otherChg", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Other_Charges"]);

                          if (dr["hpa_Status"].ToString() == "RV")
                          {
                              _hpStatus = "R";
                          }
                          else if (dr["hpa_Status"].ToString() == "O")
                          {
                              _hpStatus = "C";
                          }
                          else
                          {
                              _hpStatus = "A";
                          }
                          _oCom.Parameters.Add(":hpStatus", OracleDbType.NVarchar2).Value = _hpStatus;
                          _oCom.Parameters.Add(":closeDt", OracleDbType.Date).Value = Convert.ToDateTime(dr["hpa_ClosedDate"]);
                          _oCom.Parameters.Add(":revertDt", OracleDbType.Date).Value = Convert.ToDateTime(dr["hpa_RevertedDate"]);
                          _oCom.Parameters.Add(":revReleaseDt", OracleDbType.Date).Value = Convert.ToDateTime(dr["hpa_RevertReleasedDate"]);
                          _oCom.Parameters.Add(":responsOfficer", OracleDbType.NVarchar2).Value = dr["hpa_Responsible_Officer"].ToString();
                          _oCom.Parameters.Add(":reShedule", OracleDbType.Int32).Value = Convert.ToInt32(dr["hpa_Rescheduled"]);

                          if (Convert.ToInt32(dr["hpa_Rescheduled"]) == 1)
                          {
                              _resheDt = Convert.ToDateTime(dr["hpa_Rescheduled_Date"]).Date;
                          }
                          else
                          {
                              _resheDt = Convert.ToDateTime("31/Dec/9999").Date;
                          }
                          _oCom.Parameters.Add(":reSheduleDt", OracleDbType.Date).Value = Convert.ToDateTime(_resheDt).Date;
                          _oCom.Parameters.Add(":curDt", OracleDbType.Date).Value = DateTime.Now;
                          _oCom.ExecuteNonQuery();

                      }
                      //if reschedule an account more than one time
                      else if (Convert.ToDateTime(dr["sa_Txn_Date"]) < Convert.ToDateTime(dr["hpa_Rescheduled_Date"]))
                      {
                          sql = "SELECT min(hpa_Reschedule_Date) AS min_date From HP_T_Rescheduled_Accounts WHERE (hpa_Number =@acc) AND (hpa_Reschedule_Date >@txnDt)";

                          _sCom_1 = new SqlCommand(sql, sCnn);
                          _sCom_1.CommandType = CommandType.Text;
                          _sCom_1.Transaction = sTr;
                          _sCom_1.Parameters.Add("@acc", SqlDbType.NChar).Value = _acc;
                          _sCom_1.Parameters.Add("@txnDt", SqlDbType.DateTime).Value = Convert.ToDateTime(dr["sa_Txn_Date"]);
                          sAdp = new SqlDataAdapter(_sCom_1);
                          Ods = new DataSet();
                          sAdp.Fill(Ods, "HP_T_Rescheduled_Accounts");

                          if (Ods.Tables["HP_T_Rescheduled_Accounts"].Rows.Count > 0)
                          {
                              foreach (DataRow dr1 in Ods.Tables["HP_T_Rescheduled_Accounts"].Rows)
                              {
                                  sql = "SELECT * FROM  HP_T_Rescheduled_Accounts INNER JOIN SA_T_Sales_Header ON HP_T_Rescheduled_Accounts.hpa_Number = SA_T_Sales_Header.sa_AccountNo WHERE (HP_T_Rescheduled_Accounts.hpa_Number =@acc) AND (HP_T_Rescheduled_Accounts.hpa_Reschedule_Date =@minDt) AND (SA_T_Sales_Header.sa_Txn_Date =@txnDt) AND (SA_T_Sales_Header.sa_SubType = 'RSCH' or SA_T_Sales_Header.sa_SubType = 'EXO')";

                                  _sCom_1 = new SqlCommand(sql, sCnn);
                                  _sCom_1.CommandType = CommandType.Text;
                                  _sCom_1.Transaction = sTr;
                                  _sCom_1.Parameters.Add("@acc", SqlDbType.NChar).Value = _acc;
                                  _sCom_1.Parameters.Add("@minDt", SqlDbType.DateTime).Value = Convert.ToDateTime(dr1["min_date"]).Date;
                                  _sCom_1.Parameters.Add("@txnDt", SqlDbType.DateTime).Value = Convert.ToDateTime(dr["sa_Txn_Date"]);
                                  sAdp = new SqlDataAdapter(_sCom_1);
                                  Ods = new DataSet();
                                  sAdp.Fill(Ods, "HP_T_Rescheduled_AccountsDet");

                                  if (Ods.Tables["HP_T_Rescheduled_AccountsDet"].Rows.Count > 0)
                                  {
                                      foreach (DataRow dr2 in Ods.Tables["HP_T_Rescheduled_AccountsDet"].Rows)
                                      {
                                          strLoc = Get_Current_Acc_Loc(dr2["hpa_Number"].ToString(), Convert.ToDateTime(dr2["sa_Txn_Date"]).Date, dr2["hpa_Location"].ToString());

                                          sql = "SELECT * FROM GN_R_LOCATION WHERE LOC_CODE =@strLoc";

                                          _sCom_1 = new SqlCommand(sql, sCnn);
                                          _sCom_1.CommandType = CommandType.Text;
                                          _sCom_1.Transaction = sTr;
                                          _sCom_1.Parameters.Add("@strLoc", SqlDbType.NChar).Value = strLoc;
                                          sAdp = new SqlDataAdapter(_sCom_1);
                                          Ods = new DataSet();
                                          sAdp.Fill(Ods, "GN_R_LOCATION");

                                          if (Ods.Tables["GN_R_LOCATION"].Rows.Count > 0)
                                          {
                                              foreach (DataRow dr3 in Ods.Tables["GN_R_LOCATION"].Rows)
                                              {
                                                  strSubCate = dr1["loc_Sub_Category"].ToString();
                                              }
                                          }

                                          sql = "INSERT INTO HPT_ACC_LOG (HAL_SEQ_NO,HAL_ACC_NO,HAL_COM,HAL_PC,HAL_SEQ,HAL_SA_SUB_TP,HAL_LOG_DT,HAL_REV_STUS,HPA_ACC_CRE_DT,HAL_GRUP_CD,HAL_INVC_NO,HAL_SCH_TP,HAL_SCH_CD,HAL_TERM,HAL_INTR_RT,HAL_DP_COMM,HAL_INST_COMM,HAL_CASH_VAL,HAL_TOT_VAT,HAL_NET_VAL,HAL_DP_VAL,HAL_AF_VAL,HAL_TOT_INTR,HAL_SER_CHG,HAL_HP_VAL,HAL_TC_VAL, " +
                                                " HAL_INIT_INS,HAL_INIT_VAT,HAL_INIT_STM,HAL_INIT_SER_CHG,HAL_INST_INS,HAL_INST_VAT,HAL_INST_STM,HAL_INST_SER_CHG,HAL_BUY_VAL,HAL_OTH_CHG,HAL_STUS,HAL_CLS_DT,HAL_RV_DT,HAL_RLS_DT,HAL_ECD_STUS,HAL_ECD_TP,HAL_MGR_CD,HAL_IS_RSCH,HAL_RSCH_DT,HAL_BANK,HAL_FLAG,HAL_VAL_01,HAL_VAL_02,HAL_VAL_03,HAL_VAL_04,HAL_VAL_05,HAL_CRE_BY,HAL_CRE_DT) " +
                                                " VALUES (:seq,:acc,:subCate,:loc,:accSeq,:subTp,:txnDt,:IsRev,:accCreDt,:groupSales,:invNo,:schemeTp,:scheme,:hpTerm,:hpIntRt,:hpVal0,:hpVal1,:hpCashPrice,:hpTotVat,:hpNetVal,:hpDownPay,:amtFinance,:totInt,:hpaSerChg,:hireVal,:totCash " +
                                                ",:initInsu,:initVat,:initStamp,:initService,:instInsu,:instVAT,:instStamp,:instService,:buyBackChg,:otherChg,:hpStatus,:closeDt,:revertDt,:revReleaseDt,0,'',:responsOfficer,:reShedule,:reSheduleDt,'001','001',0,0,0,0,0 ,'UPLOAD',:curDt)";

                                          TMP_SEQ = GetSerialID();
                                          _oCom = new OracleCommand(sql, oCnn) { CommandType = CommandType.Text, BindByName = true };
                                          _oCom.Transaction = oTr;
                                          _oCom.Parameters.Add(":seq", OracleDbType.Int32).Value = TMP_SEQ;
                                          _oCom.Parameters.Add(":acc", OracleDbType.NVarchar2).Value = dr2["hpa_Number"].ToString();
                                          _oCom.Parameters.Add(":subCate", OracleDbType.NVarchar2).Value = strSubCate;
                                          _oCom.Parameters.Add(":loc", OracleDbType.NVarchar2).Value = strLoc;
                                          _oCom.Parameters.Add(":accSeq", OracleDbType.Int32).Value = Convert.ToInt32(dr2["hpa_SeqenceNo"]);
                                          _oCom.Parameters.Add(":subTp", OracleDbType.NVarchar2).Value = dr2["sa_SubType"].ToString();
                                          _oCom.Parameters.Add(":txnDt", OracleDbType.Date).Value = Convert.ToDateTime(dr2["sa_Txn_Date"]);
                                          _oCom.Parameters.Add(":IsRev", OracleDbType.Int32).Value = Convert.ToInt32(dr2["sa_IsReversal"]);
                                          _oCom.Parameters.Add(":accCreDt", OracleDbType.Date).Value = Convert.ToDateTime(dr2["hpa_CreatedDate"]).Date;
                                          _oCom.Parameters.Add(":groupSales", OracleDbType.NVarchar2).Value = dr2["hpa_GroupSale_Code"].ToString();
                                          _oCom.Parameters.Add(":invNo", OracleDbType.NVarchar2).Value = dr2["sa_InvoiceNo"].ToString();
                                          _oCom.Parameters.Add(":schemeTp", OracleDbType.NVarchar2).Value = dr2["hpa_SchemeType"].ToString();
                                          _oCom.Parameters.Add(":scheme", OracleDbType.NVarchar2).Value = dr2["hpa_Scheme"].ToString();
                                          _oCom.Parameters.Add(":hpTerm", OracleDbType.Int32).Value = Convert.ToInt32(dr2["hpa_Term"]);
                                          _oCom.Parameters.Add(":hpIntRt", OracleDbType.Decimal).Value = Convert.ToDecimal(dr2["hpa_IntRate"]);
                                          _oCom.Parameters.Add(":hpVal0", OracleDbType.Decimal).Value = Convert.ToDecimal(dr2["hpa_DP_Comm"]);
                                          _oCom.Parameters.Add(":hpVal1", OracleDbType.Decimal).Value = Convert.ToDecimal(dr2["hpa_DP_Comm"]);
                                          _oCom.Parameters.Add(":hpCashPrice", OracleDbType.Decimal).Value = Convert.ToDecimal(dr2["hpa_Cash_Price"]);
                                          _oCom.Parameters.Add(":hpTotVat", OracleDbType.Decimal).Value = Convert.ToDecimal(dr2["hpa_Tot_VAT"]);

                                          Decimal _netVal = Convert.ToDecimal(dr2["hpa_Cash_Price"]) - Convert.ToDecimal(dr2["hpa_Tot_VAT"]);
                                          _oCom.Parameters.Add(":hpNetVal", OracleDbType.Decimal).Value = _netVal;

                                          _oCom.Parameters.Add(":hpDownPay", OracleDbType.Decimal).Value = Convert.ToDecimal(dr2["hpa_Down_Payment"]);
                                          _oCom.Parameters.Add(":amtFinance", OracleDbType.Decimal).Value = Convert.ToDecimal(dr2["hpa_AmountFinance"]);
                                          _oCom.Parameters.Add(":totInt", OracleDbType.Decimal).Value = Convert.ToDecimal(dr2["hpa_Tot_Int"]);
                                          _serChg = Convert.ToDecimal(dr2["hpa_Init_ServiceChg"]) + Convert.ToDecimal(dr2["hpa_Inst_ServiceChg"]);
                                          _oCom.Parameters.Add(":hpaSerChg", OracleDbType.Decimal).Value = _serChg;
                                          _oCom.Parameters.Add(":hireVal", OracleDbType.Decimal).Value = Convert.ToDecimal(dr2["hpa_Hire_Value"]);
                                          _oCom.Parameters.Add(":totCash", OracleDbType.Decimal).Value = Convert.ToDecimal(dr2["hpa_Total_Cash"]);
                                          _oCom.Parameters.Add(":initInsu", OracleDbType.Decimal).Value = Convert.ToDecimal(dr2["hpa_Init_Insu"]);
                                          _oCom.Parameters.Add(":initVat", OracleDbType.Decimal).Value = Convert.ToDecimal(dr2["hpa_Init_VAT"]);
                                          _oCom.Parameters.Add(":initStamp", OracleDbType.Decimal).Value = Convert.ToDecimal(dr2["hpa_Init_StampDuty"]);
                                          _oCom.Parameters.Add(":initService", OracleDbType.Decimal).Value = Convert.ToDecimal(dr2["hpa_Init_ServiceChg"]);
                                          _oCom.Parameters.Add(":instInsu", OracleDbType.Decimal).Value = Convert.ToDecimal(dr2["hpa_Inst_Insu"]);
                                          _oCom.Parameters.Add(":instVAT", OracleDbType.Decimal).Value = Convert.ToDecimal(dr2["hpa_Inst_VAT"]);
                                          _oCom.Parameters.Add(":instStamp", OracleDbType.Decimal).Value = Convert.ToDecimal(dr2["hpa_Inst_StampDuty"]);
                                          _oCom.Parameters.Add(":instService", OracleDbType.Decimal).Value = Convert.ToDecimal(dr2["hpa_Inst_ServiceChg"]);
                                          _oCom.Parameters.Add(":buyBackChg", OracleDbType.Decimal).Value = Convert.ToDecimal(dr2["hpa_BuyBack_Charge"]);
                                          _oCom.Parameters.Add(":otherChg", OracleDbType.Decimal).Value = Convert.ToDecimal(dr2["hpa_Other_Charges"]);

                                          if (dr2["hpa_Status"].ToString() == "RV")
                                          {
                                              _hpStatus = "R";
                                          }
                                          else if (dr2["hpa_Status"].ToString() == "O")
                                          {
                                              _hpStatus = "C";
                                          }
                                          else
                                          {
                                              _hpStatus = "A";
                                          }
                                          _oCom.Parameters.Add(":hpStatus", OracleDbType.NVarchar2).Value = _hpStatus;
                                          _oCom.Parameters.Add(":closeDt", OracleDbType.Date).Value = Convert.ToDateTime(dr["hpa_ClosedDate"]);
                                          _oCom.Parameters.Add(":revertDt", OracleDbType.Date).Value = Convert.ToDateTime(dr["hpa_RevertedDate"]);
                                          _oCom.Parameters.Add(":revReleaseDt", OracleDbType.Date).Value = Convert.ToDateTime(dr["hpa_RevertReleasedDate"]);
                                          _oCom.Parameters.Add(":responsOfficer", OracleDbType.NVarchar2).Value = dr2["hpa_Responsible_Officer"].ToString();
                                          _oCom.Parameters.Add(":reShedule", OracleDbType.Int32).Value = Convert.ToInt32(dr["hpa_Rescheduled"]);

                                          if (Convert.ToInt32(dr["hpa_Rescheduled"]) == 1)
                                          {
                                              _resheDt = Convert.ToDateTime(dr2["hpa_Rescheduled_Date"]).Date;
                                          }
                                          else
                                          {
                                              _resheDt = Convert.ToDateTime("31/Dec/9999").Date;
                                          }
                                          _oCom.Parameters.Add(":reSheduleDt", OracleDbType.Date).Value = Convert.ToDateTime(_resheDt).Date;
                                          _oCom.Parameters.Add(":curDt", OracleDbType.Date).Value = DateTime.Now;
                                          _oCom.ExecuteNonQuery();
                                      }
                                  }

                              }
                          }
                      }

                  }
              }

              //'Reschedule in and Exchange In
              sql = "SELECT * FROM  SA_T_Sales_Header INNER JOIN  " +
                    " HP_T_Rescheduled_Accounts ON SA_T_Sales_Header.sa_AccountNo = HP_T_Rescheduled_Accounts.hpa_Number AND SA_T_Sales_Header.sa_Txn_Date = HP_T_Rescheduled_Accounts.hpa_Reschedule_Date INNER JOIN " +
                    " HP_T_Accounts_Log on HP_T_Accounts_Log.log_AccNo=SA_T_Sales_Header.sa_AccountNo " +
                    " WHERE (HP_T_Accounts_Log.log_Location in (SELECT loc_Code FROM GN_R_Location WHERE (loc_Acc_No =@loc))) " +
                    " AND (SA_T_Sales_Header.sa_Txn_Date <=@recDt) " +
                    " AND SA_T_Sales_Header.sa_Txn_Date < HP_T_Accounts_Log.log_Transfer_Date " +
                    " AND (HP_T_Accounts_Log.log_Date <=@recDt) " +
                    " AND (SA_T_Sales_Header.sa_SubType = 'RSCHR' OR SA_T_Sales_Header.sa_SubType = 'EXI') " +
                    " AND (hpa_Number =@acc)";

              _sCom_1 = new SqlCommand(sql, sCnn);
              _sCom_1.CommandType = CommandType.Text;
              _sCom_1.Transaction = sTr;
              _sCom_1.Parameters.Add("@loc", SqlDbType.NChar).Value = _loc;
              _sCom_1.Parameters.Add("@recDt", SqlDbType.DateTime).Value = _recDt.Date;
              _sCom_1.Parameters.Add("@acc", SqlDbType.NChar).Value = _acc;
              sAdp = new SqlDataAdapter(_sCom_1);
              Ods = new DataSet();
              sAdp.Fill(Ods, "Acc_Det2");

              if (Ods.Tables["Acc_Det2"].Rows.Count > 0)
              {
                  foreach (DataRow dr in Ods.Tables["Acc_Det2"].Rows)
                  {
                      strLoc = Get_Current_Acc_Loc(dr["hpa_Number"].ToString(), Convert.ToDateTime(dr["sa_Txn_Date"]).Date, dr["hpa_Location"].ToString());

                      sql = "SELECT * FROM GN_R_LOCATION WHERE LOC_CODE =@strLoc";

                      _sCom_1 = new SqlCommand(sql, sCnn);
                      _sCom_1.CommandType = CommandType.Text;
                      _sCom_1.Transaction = sTr;
                      _sCom_1.Parameters.Add("@strLoc", SqlDbType.NChar).Value = strLoc;
                      sAdp = new SqlDataAdapter(_sCom_1);
                      Ods = new DataSet();
                      sAdp.Fill(Ods, "GN_R_LOCATION");

                      if (Ods.Tables["GN_R_LOCATION"].Rows.Count > 0)
                      {
                          foreach (DataRow dr1 in Ods.Tables["GN_R_LOCATION"].Rows)
                          {
                              strSubCate = dr1["loc_Sub_Category"].ToString();
                          }
                      }

                      sql = "SELECT * FROM HP_T_Accounts WHERE HPA_NUMBER =@acc";

                      _sCom_1 = new SqlCommand(sql, sCnn);
                      _sCom_1.CommandType = CommandType.Text;
                      _sCom_1.Transaction = sTr;
                      _sCom_1.Parameters.Add("@acc", SqlDbType.NChar).Value = dr["hpa_Number"].ToString();
                      sAdp = new SqlDataAdapter(_sCom_1);
                      Ods = new DataSet();
                      sAdp.Fill(Ods, "HP_T_Accounts");

                      if (Ods.Tables["HP_T_Accounts"].Rows.Count > 0)
                      {
                          foreach (DataRow dr2 in Ods.Tables["HP_T_Accounts"].Rows)
                          {

                              sql = "INSERT INTO HPT_ACC_LOG (HAL_SEQ_NO,HAL_ACC_NO,HAL_COM,HAL_PC,HAL_SEQ,HAL_SA_SUB_TP,HAL_LOG_DT,HAL_REV_STUS,HPA_ACC_CRE_DT,HAL_GRUP_CD,HAL_INVC_NO,HAL_SCH_TP,HAL_SCH_CD,HAL_TERM,HAL_INTR_RT,HAL_DP_COMM,HAL_INST_COMM,HAL_CASH_VAL,HAL_TOT_VAT,HAL_NET_VAL,HAL_DP_VAL,HAL_AF_VAL,HAL_TOT_INTR,HAL_SER_CHG,HAL_HP_VAL,HAL_TC_VAL, " +
                                                " HAL_INIT_INS,HAL_INIT_VAT,HAL_INIT_STM,HAL_INIT_SER_CHG,HAL_INST_INS,HAL_INST_VAT,HAL_INST_STM,HAL_INST_SER_CHG,HAL_BUY_VAL,HAL_OTH_CHG,HAL_STUS,HAL_CLS_DT,HAL_RV_DT,HAL_RLS_DT,HAL_ECD_STUS,HAL_ECD_TP,HAL_MGR_CD,HAL_IS_RSCH,HAL_RSCH_DT,HAL_BANK,HAL_FLAG,HAL_VAL_01,HAL_VAL_02,HAL_VAL_03,HAL_VAL_04,HAL_VAL_05,HAL_CRE_BY,HAL_CRE_DT) " +
                                                " VALUES (:seq,:acc,:subCate,:loc,:accSeq,:subTp,:txnDt,:IsRev,:accCreDt,:groupSales,:invNo,:schemeTp,:scheme,:hpTerm,:hpIntRt,:hpVal0,:hpVal1,:hpCashPrice,:hpTotVat,:hpNetVal,:hpDownPay,:amtFinance,:totInt,:hpaSerChg,:hireVal,:totCash " +
                                                ",:initInsu,:initVat,:initStamp,:initService,:instInsu,:instVAT,:instStamp,:instService,:buyBackChg,:otherChg,:hpStatus,:closeDt,:revertDt,:revReleaseDt,0,'',:responsOfficer,:reShedule,:reSheduleDt,'001','001',0,0,0,0,0 ,'UPLOAD',:curDt)";

                              TMP_SEQ = GetSerialID();
                              _oCom = new OracleCommand(sql, oCnn) { CommandType = CommandType.Text, BindByName = true };
                              _oCom.Transaction = oTr;
                              _oCom.Parameters.Add(":seq", OracleDbType.Int32).Value = TMP_SEQ;
                              _oCom.Parameters.Add(":acc", OracleDbType.NVarchar2).Value = dr["hpa_Number"].ToString();
                              _oCom.Parameters.Add(":subCate", OracleDbType.NVarchar2).Value = strSubCate;
                              _oCom.Parameters.Add(":loc", OracleDbType.NVarchar2).Value = strLoc;
                              _oCom.Parameters.Add(":accSeq", OracleDbType.Int32).Value = Convert.ToInt32(dr["hpa_SeqenceNo"]);
                              _oCom.Parameters.Add(":subTp", OracleDbType.NVarchar2).Value = dr["sa_SubType"].ToString();
                              _oCom.Parameters.Add(":txnDt", OracleDbType.Date).Value = Convert.ToDateTime(dr["sa_Txn_Date"]);
                              _oCom.Parameters.Add(":IsRev", OracleDbType.Int32).Value = Convert.ToInt32(dr["sa_IsReversal"]);
                              _oCom.Parameters.Add(":accCreDt", OracleDbType.Date).Value = Convert.ToDateTime(dr["hpa_CreatedDate"]).Date;
                              _oCom.Parameters.Add(":groupSales", OracleDbType.NVarchar2).Value = dr["hpa_GroupSale_Code"].ToString();
                              _oCom.Parameters.Add(":invNo", OracleDbType.NVarchar2).Value = dr["sa_InvoiceNo"].ToString();
                              _oCom.Parameters.Add(":schemeTp", OracleDbType.NVarchar2).Value = dr["hpa_SchemeType"].ToString();
                              _oCom.Parameters.Add(":scheme", OracleDbType.NVarchar2).Value = dr["hpa_Scheme"].ToString();
                              _oCom.Parameters.Add(":hpTerm", OracleDbType.Int32).Value = Convert.ToInt32(dr["hpa_Term"]);
                              _oCom.Parameters.Add(":hpIntRt", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_IntRate"]);
                              _oCom.Parameters.Add(":hpVal0", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_DP_Comm"]);
                              _oCom.Parameters.Add(":hpVal1", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_DP_Comm"]);
                              _oCom.Parameters.Add(":hpCashPrice", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Cash_Price"]);
                              _oCom.Parameters.Add(":hpTotVat", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Tot_VAT"]);

                              Decimal _netVal = Convert.ToDecimal(dr["hpa_Cash_Price"]) - Convert.ToDecimal(dr["hpa_Tot_VAT"]);
                              _oCom.Parameters.Add(":hpNetVal", OracleDbType.Decimal).Value = _netVal;

                              _oCom.Parameters.Add(":hpDownPay", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Down_Payment"]);
                              _oCom.Parameters.Add(":amtFinance", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_AmountFinance"]);
                              _oCom.Parameters.Add(":totInt", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Tot_Int"]);
                              _serChg = Convert.ToDecimal(dr["hpa_Init_ServiceChg"]) + Convert.ToDecimal(dr["hpa_Inst_ServiceChg"]);
                              _oCom.Parameters.Add(":hpaSerChg", OracleDbType.Decimal).Value = _serChg;
                              _oCom.Parameters.Add(":hireVal", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Hire_Value"]);
                              _oCom.Parameters.Add(":totCash", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Total_Cash"]);
                              _oCom.Parameters.Add(":initInsu", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Init_Insu"]);
                              _oCom.Parameters.Add(":initVat", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Init_VAT"]);
                              _oCom.Parameters.Add(":initStamp", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Init_StampDuty"]);
                              _oCom.Parameters.Add(":initService", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Init_ServiceChg"]);
                              _oCom.Parameters.Add(":instInsu", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Inst_Insu"]);
                              _oCom.Parameters.Add(":instVAT", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Inst_VAT"]);
                              _oCom.Parameters.Add(":instStamp", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Inst_StampDuty"]);
                              _oCom.Parameters.Add(":instService", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Inst_ServiceChg"]);
                              _oCom.Parameters.Add(":buyBackChg", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_BuyBack_Charge"]);
                              _oCom.Parameters.Add(":otherChg", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Other_Charges"]);

                              if (dr["hpa_Status"].ToString() == "RV")
                              {
                                  _hpStatus = "R";
                              }
                              else if (dr["hpa_Status"].ToString() == "O")
                              {
                                  _hpStatus = "C";
                              }
                              else
                              {
                                  _hpStatus = "A";
                              }
                              _oCom.Parameters.Add(":hpStatus", OracleDbType.NVarchar2).Value = _hpStatus;
                              _oCom.Parameters.Add(":closeDt", OracleDbType.Date).Value = Convert.ToDateTime(dr2["hpa_ClosedDate"]);
                              _oCom.Parameters.Add(":revertDt", OracleDbType.Date).Value = Convert.ToDateTime(dr2["hpa_RevertedDate"]);
                              _oCom.Parameters.Add(":revReleaseDt", OracleDbType.Date).Value = Convert.ToDateTime(dr2["hpa_RevertReleasedDate"]);
                              _oCom.Parameters.Add(":responsOfficer", OracleDbType.NVarchar2).Value = dr["hpa_Responsible_Officer"].ToString();
                              _oCom.Parameters.Add(":reShedule", OracleDbType.Int32).Value = Convert.ToInt32(dr2["hpa_Rescheduled"]);

                              if (Convert.ToInt32(dr2["hpa_Rescheduled"]) == 1)
                              {
                                  _resheDt = Convert.ToDateTime(dr2["hpa_Rescheduled_Date"]).Date;
                              }
                              else
                              {
                                  _resheDt = Convert.ToDateTime("31/Dec/9999").Date;
                              }
                              _oCom.Parameters.Add(":reSheduleDt", OracleDbType.Date).Value = Convert.ToDateTime(_resheDt).Date;
                              _oCom.Parameters.Add(":curDt", OracleDbType.Date).Value = DateTime.Now;
                              _oCom.ExecuteNonQuery();
                          }
                      }


                  }
              }

              //original sales after reschedule
              sql = "SELECT SA_T_Sales_Header.*, HP_T_Rescheduled_Accounts.* FROM  SA_T_Sales_Header INNER JOIN HP_T_Rescheduled_Accounts ON SA_T_Sales_Header.sa_AccountNo = HP_T_Rescheduled_Accounts.hpa_Number " +
                   "AND SA_T_Sales_Header.sa_InvoiceNo = HP_T_Rescheduled_Accounts.hpa_InvoiceNo " +
                   "INNER JOIN HP_T_Accounts_Log on HP_T_Accounts_Log.log_AccNo=SA_T_Sales_Header.sa_AccountNo " +
                   "WHERE (HP_T_Accounts_Log.log_Location in (SELECT loc_Code FROM GN_R_Location WHERE (loc_Acc_No =@loc))) " +
                   "AND (SA_T_Sales_Header.sa_Txn_Date <=@recDt) " +
                   "AND SA_T_Sales_Header.sa_Txn_Date < HP_T_Accounts_Log.log_Transfer_Date " +
                   "AND (HP_T_Accounts_Log.log_Date <=@recDt) " +
                   "AND (SA_T_Sales_Header.sa_SubType = 'SA') " +
                   "AND (hpa_Number =@acc)";

              _sCom_1 = new SqlCommand(sql, sCnn);
              _sCom_1.CommandType = CommandType.Text;
              _sCom_1.Transaction = sTr;
              _sCom_1.Parameters.Add("@loc", SqlDbType.NChar).Value = _loc;
              _sCom_1.Parameters.Add("@recDt", SqlDbType.DateTime).Value = _recDt.Date;
              _sCom_1.Parameters.Add("@acc", SqlDbType.NChar).Value = _acc;
              sAdp = new SqlDataAdapter(_sCom_1);
              Ods = new DataSet();
              sAdp.Fill(Ods, "Acc_Det3");

              if (Ods.Tables["Acc_Det3"].Rows.Count > 0)
              {
                  foreach (DataRow dr in Ods.Tables["Acc_Det3"].Rows)
                  {
                      strLoc = Get_Current_Acc_Loc(dr["hpa_Number"].ToString(), Convert.ToDateTime(dr["sa_Txn_Date"]).Date, dr["hpa_Location"].ToString());

                      sql = "SELECT * FROM GN_R_LOCATION WHERE LOC_CODE =@strLoc";

                      _sCom_1 = new SqlCommand(sql, sCnn);
                      _sCom_1.CommandType = CommandType.Text;
                      _sCom_1.Transaction = sTr;
                      _sCom_1.Parameters.Add("@strLoc", SqlDbType.NChar).Value = strLoc;
                      sAdp = new SqlDataAdapter(_sCom_1);
                      Ods = new DataSet();
                      sAdp.Fill(Ods, "GN_R_LOCATION");

                      if (Ods.Tables["GN_R_LOCATION"].Rows.Count > 0)
                      {
                          foreach (DataRow dr1 in Ods.Tables["GN_R_LOCATION"].Rows)
                          {
                              strSubCate = dr1["loc_Sub_Category"].ToString();
                          }
                      }

                      sql = "SELECT * FROM HP_T_Accounts WHERE HPA_NUMBER =@acc";

                      _sCom_1 = new SqlCommand(sql, sCnn);
                      _sCom_1.CommandType = CommandType.Text;
                      _sCom_1.Transaction = sTr;
                      _sCom_1.Parameters.Add("@acc", SqlDbType.NChar).Value = dr["hpa_Number"].ToString();
                      sAdp = new SqlDataAdapter(_sCom_1);
                      Ods = new DataSet();
                      sAdp.Fill(Ods, "HP_T_Accounts");

                      if (Ods.Tables["HP_T_Accounts"].Rows.Count > 0)
                      {
                          foreach (DataRow dr2 in Ods.Tables["HP_T_Accounts"].Rows)
                          {
                              sql = "INSERT INTO HPT_ACC_LOG (HAL_SEQ_NO,HAL_ACC_NO,HAL_COM,HAL_PC,HAL_SEQ,HAL_SA_SUB_TP,HAL_LOG_DT,HAL_REV_STUS,HPA_ACC_CRE_DT,HAL_GRUP_CD,HAL_INVC_NO,HAL_SCH_TP,HAL_SCH_CD,HAL_TERM,HAL_INTR_RT,HAL_DP_COMM,HAL_INST_COMM,HAL_CASH_VAL,HAL_TOT_VAT,HAL_NET_VAL,HAL_DP_VAL,HAL_AF_VAL,HAL_TOT_INTR,HAL_SER_CHG,HAL_HP_VAL,HAL_TC_VAL, " +
                                                " HAL_INIT_INS,HAL_INIT_VAT,HAL_INIT_STM,HAL_INIT_SER_CHG,HAL_INST_INS,HAL_INST_VAT,HAL_INST_STM,HAL_INST_SER_CHG,HAL_BUY_VAL,HAL_OTH_CHG,HAL_STUS,HAL_CLS_DT,HAL_RV_DT,HAL_RLS_DT,HAL_ECD_STUS,HAL_ECD_TP,HAL_MGR_CD,HAL_IS_RSCH,HAL_RSCH_DT,HAL_BANK,HAL_FLAG,HAL_VAL_01,HAL_VAL_02,HAL_VAL_03,HAL_VAL_04,HAL_VAL_05,HAL_CRE_BY,HAL_CRE_DT) " +
                                                " VALUES (:seq,:acc,:subCate,:loc,:accSeq,:subTp,:txnDt,:IsRev,:accCreDt,:groupSales,:invNo,:schemeTp,:scheme,:hpTerm,:hpIntRt,:hpVal0,:hpVal1,:hpCashPrice,:hpTotVat,:hpNetVal,:hpDownPay,:amtFinance,:totInt,:hpaSerChg,:hireVal,:totCash " +
                                                ",:initInsu,:initVat,:initStamp,:initService,:instInsu,:instVAT,:instStamp,:instService,:buyBackChg,:otherChg,:hpStatus,:closeDt,:revertDt,:revReleaseDt,0,'',:responsOfficer,:reShedule,:reSheduleDt,'001','001',0,0,0,0,0 ,'UPLOAD',:curDt)";

                              TMP_SEQ = GetSerialID();
                              _oCom = new OracleCommand(sql, oCnn) { CommandType = CommandType.Text, BindByName = true };
                              _oCom.Transaction = oTr;
                              _oCom.Parameters.Add(":seq", OracleDbType.Int32).Value = TMP_SEQ;
                              _oCom.Parameters.Add(":acc", OracleDbType.NVarchar2).Value = dr["hpa_Number"].ToString();
                              _oCom.Parameters.Add(":subCate", OracleDbType.NVarchar2).Value = strSubCate;
                              _oCom.Parameters.Add(":loc", OracleDbType.NVarchar2).Value = strLoc;
                              _oCom.Parameters.Add(":accSeq", OracleDbType.Int32).Value = Convert.ToInt32(dr["hpa_SeqenceNo"]);
                              _oCom.Parameters.Add(":subTp", OracleDbType.NVarchar2).Value = dr["sa_SubType"].ToString();
                              _oCom.Parameters.Add(":txnDt", OracleDbType.Date).Value = Convert.ToDateTime(dr["sa_Txn_Date"]);
                              _oCom.Parameters.Add(":IsRev", OracleDbType.Int32).Value = Convert.ToInt32(dr["sa_IsReversal"]);
                              _oCom.Parameters.Add(":accCreDt", OracleDbType.Date).Value = Convert.ToDateTime(dr["hpa_CreatedDate"]).Date;
                              _oCom.Parameters.Add(":groupSales", OracleDbType.NVarchar2).Value = dr["hpa_GroupSale_Code"].ToString();
                              _oCom.Parameters.Add(":invNo", OracleDbType.NVarchar2).Value = dr["sa_InvoiceNo"].ToString();
                              _oCom.Parameters.Add(":schemeTp", OracleDbType.NVarchar2).Value = dr["hpa_SchemeType"].ToString();
                              _oCom.Parameters.Add(":scheme", OracleDbType.NVarchar2).Value = dr["hpa_Scheme"].ToString();
                              _oCom.Parameters.Add(":hpTerm", OracleDbType.Int32).Value = Convert.ToInt32(dr["hpa_Term"]);
                              _oCom.Parameters.Add(":hpIntRt", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_IntRate"]);
                              _oCom.Parameters.Add(":hpVal0", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_DP_Comm"]);
                              _oCom.Parameters.Add(":hpVal1", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_DP_Comm"]);
                              _oCom.Parameters.Add(":hpCashPrice", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Cash_Price"]);
                              _oCom.Parameters.Add(":hpTotVat", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Tot_VAT"]);

                              Decimal _netVal = Convert.ToDecimal(dr["hpa_Cash_Price"]) - Convert.ToDecimal(dr["hpa_Tot_VAT"]);
                              _oCom.Parameters.Add(":hpNetVal", OracleDbType.Decimal).Value = _netVal;

                              _oCom.Parameters.Add(":hpDownPay", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Down_Payment"]);
                              _oCom.Parameters.Add(":amtFinance", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_AmountFinance"]);
                              _oCom.Parameters.Add(":totInt", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Tot_Int"]);
                              _serChg = Convert.ToDecimal(dr["hpa_Init_ServiceChg"]) + Convert.ToDecimal(dr["hpa_Inst_ServiceChg"]);
                              _oCom.Parameters.Add(":hpaSerChg", OracleDbType.Decimal).Value = _serChg;
                              _oCom.Parameters.Add(":hireVal", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Hire_Value"]);
                              _oCom.Parameters.Add(":totCash", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Total_Cash"]);
                              _oCom.Parameters.Add(":initInsu", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Init_Insu"]);
                              _oCom.Parameters.Add(":initVat", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Init_VAT"]);
                              _oCom.Parameters.Add(":initStamp", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Init_StampDuty"]);
                              _oCom.Parameters.Add(":initService", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Init_ServiceChg"]);
                              _oCom.Parameters.Add(":instInsu", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Inst_Insu"]);
                              _oCom.Parameters.Add(":instVAT", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Inst_VAT"]);
                              _oCom.Parameters.Add(":instStamp", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Inst_StampDuty"]);
                              _oCom.Parameters.Add(":instService", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Inst_ServiceChg"]);
                              _oCom.Parameters.Add(":buyBackChg", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_BuyBack_Charge"]);
                              _oCom.Parameters.Add(":otherChg", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Other_Charges"]);

                              if (dr["hpa_Status"].ToString() == "RV")
                              {
                                  _hpStatus = "R";
                              }
                              else if (dr["hpa_Status"].ToString() == "O")
                              {
                                  _hpStatus = "C";
                              }
                              else
                              {
                                  _hpStatus = "A";
                              }
                              _oCom.Parameters.Add(":hpStatus", OracleDbType.NVarchar2).Value = _hpStatus;
                              _oCom.Parameters.Add(":closeDt", OracleDbType.Date).Value = Convert.ToDateTime(dr2["hpa_ClosedDate"]);
                              _oCom.Parameters.Add(":revertDt", OracleDbType.Date).Value = Convert.ToDateTime(dr2["hpa_RevertedDate"]);
                              _oCom.Parameters.Add(":revReleaseDt", OracleDbType.Date).Value = Convert.ToDateTime(dr2["hpa_RevertReleasedDate"]);
                              _oCom.Parameters.Add(":responsOfficer", OracleDbType.NVarchar2).Value = dr["hpa_Responsible_Officer"].ToString();
                              _oCom.Parameters.Add(":reShedule", OracleDbType.Int32).Value = Convert.ToInt32(dr2["hpa_Rescheduled"]);

                              if (Convert.ToInt32(dr2["hpa_Rescheduled"]) == 1)
                              {
                                  _resheDt = Convert.ToDateTime(dr2["hpa_Rescheduled_Date"]).Date;
                              }
                              else
                              {
                                  _resheDt = Convert.ToDateTime("31/Dec/9999").Date;
                              }
                              _oCom.Parameters.Add(":reSheduleDt", OracleDbType.Date).Value = Convert.ToDateTime(_resheDt).Date;
                              _oCom.Parameters.Add(":curDt", OracleDbType.Date).Value = DateTime.Now;
                              _oCom.ExecuteNonQuery();
                          }
                      }

                  }
              }

              //conversion/reversals after reschedule
              sql = "SELECT * FROM  SA_T_Sales_Header INNER JOIN HP_T_Accounts ON SA_T_Sales_Header.sa_AccountNo = HP_T_Accounts.hpa_Number " +
                   "INNER JOIN HP_T_Accounts_Log on HP_T_Accounts_Log.log_AccNo=SA_T_Sales_Header.sa_AccountNo " +
                   "WHERE (HP_T_Accounts_Log.log_Location in (SELECT loc_Code FROM GN_R_Location WHERE (loc_Acc_No =@loc))) " +
                   "AND (SA_T_Sales_Header.sa_Txn_Date <=@recDt) " +
                   "AND SA_T_Sales_Header.sa_Txn_Date < HP_T_Accounts_Log.log_Transfer_Date " +
                   "AND (HP_T_Accounts_Log.log_Date <=@recDt) " +
                   "AND (SA_T_Sales_Header.sa_SubType = 'CC' OR SA_T_Sales_Header.sa_SubType = 'REV') AND (SA_T_Sales_Header.sa_Type = 'HS') " +
                   "AND (HP_T_Accounts.hpa_Rescheduled = 1) AND (SA_T_Sales_Header.sa_Status<>'C') " +
                   "AND (SA_T_Sales_Header.sa_Type='HS') " +
                   "AND (hpa_Number =@acc)";

              _sCom_1 = new SqlCommand(sql, sCnn);
              _sCom_1.CommandType = CommandType.Text;
              _sCom_1.Transaction = sTr;
              _sCom_1.Parameters.Add("@loc", SqlDbType.NChar).Value = _loc;
              _sCom_1.Parameters.Add("@recDt", SqlDbType.DateTime).Value = _recDt.Date;
              _sCom_1.Parameters.Add("@acc", SqlDbType.NChar).Value = _acc;
              sAdp = new SqlDataAdapter(_sCom_1);
              Ods = new DataSet();
              sAdp.Fill(Ods, "Acc_Det4");

              if (Ods.Tables["Acc_Det4"].Rows.Count > 0)
              {
                  foreach (DataRow dr in Ods.Tables["Acc_Det4"].Rows)
                  {
                      strLoc = Get_Current_Acc_Loc(dr["hpa_Number"].ToString(), Convert.ToDateTime(dr["sa_Txn_Date"]).Date, dr["hpa_Location"].ToString());

                      sql = "SELECT * FROM GN_R_LOCATION WHERE LOC_CODE =@strLoc";

                      _sCom_1 = new SqlCommand(sql, sCnn);
                      _sCom_1.CommandType = CommandType.Text;
                      _sCom_1.Transaction = sTr;
                      _sCom_1.Parameters.Add("@strLoc", SqlDbType.NChar).Value = strLoc;
                      sAdp = new SqlDataAdapter(_sCom_1);
                      Ods = new DataSet();
                      sAdp.Fill(Ods, "GN_R_LOCATION");

                      if (Ods.Tables["GN_R_LOCATION"].Rows.Count > 0)
                      {
                          foreach (DataRow dr1 in Ods.Tables["GN_R_LOCATION"].Rows)
                          {
                              strSubCate = dr1["loc_Sub_Category"].ToString();
                          }
                      }

                      sql = "INSERT INTO HPT_ACC_LOG (HAL_SEQ_NO,HAL_ACC_NO,HAL_COM,HAL_PC,HAL_SEQ,HAL_SA_SUB_TP,HAL_LOG_DT,HAL_REV_STUS,HPA_ACC_CRE_DT,HAL_GRUP_CD,HAL_INVC_NO,HAL_SCH_TP,HAL_SCH_CD,HAL_TERM,HAL_INTR_RT,HAL_DP_COMM,HAL_INST_COMM,HAL_CASH_VAL,HAL_TOT_VAT,HAL_NET_VAL,HAL_DP_VAL,HAL_AF_VAL,HAL_TOT_INTR,HAL_SER_CHG,HAL_HP_VAL,HAL_TC_VAL, " +
                                                " HAL_INIT_INS,HAL_INIT_VAT,HAL_INIT_STM,HAL_INIT_SER_CHG,HAL_INST_INS,HAL_INST_VAT,HAL_INST_STM,HAL_INST_SER_CHG,HAL_BUY_VAL,HAL_OTH_CHG,HAL_STUS,HAL_CLS_DT,HAL_RV_DT,HAL_RLS_DT,HAL_ECD_STUS,HAL_ECD_TP,HAL_MGR_CD,HAL_IS_RSCH,HAL_RSCH_DT,HAL_BANK,HAL_FLAG,HAL_VAL_01,HAL_VAL_02,HAL_VAL_03,HAL_VAL_04,HAL_VAL_05,HAL_CRE_BY,HAL_CRE_DT) " +
                                                " VALUES (:seq,:acc,:subCate,:loc,:accSeq,:subTp,:txnDt,:IsRev,:accCreDt,:groupSales,:invNo,:schemeTp,:scheme,:hpTerm,:hpIntRt,:hpVal0,:hpVal1,:hpCashPrice,:hpTotVat,:hpNetVal,:hpDownPay,:amtFinance,:totInt,:hpaSerChg,:hireVal,:totCash " +
                                                ",:initInsu,:initVat,:initStamp,:initService,:instInsu,:instVAT,:instStamp,:instService,:buyBackChg,:otherChg,:hpStatus,:closeDt,:revertDt,:revReleaseDt,0,'',:responsOfficer,:reShedule,:reSheduleDt,'001','001',0,0,0,0,0 ,'UPLOAD',:curDt)";

                      TMP_SEQ = GetSerialID();
                      _oCom = new OracleCommand(sql, oCnn) { CommandType = CommandType.Text, BindByName = true };
                      _oCom.Transaction = oTr;
                      _oCom.Parameters.Add(":seq", OracleDbType.Int32).Value = TMP_SEQ;
                      _oCom.Parameters.Add(":acc", OracleDbType.NVarchar2).Value = dr["hpa_Number"].ToString();
                      _oCom.Parameters.Add(":subCate", OracleDbType.NVarchar2).Value = strSubCate;
                      _oCom.Parameters.Add(":loc", OracleDbType.NVarchar2).Value = strLoc;
                      _oCom.Parameters.Add(":accSeq", OracleDbType.Int32).Value = Convert.ToInt32(dr["hpa_SeqenceNo"]);
                      _oCom.Parameters.Add(":subTp", OracleDbType.NVarchar2).Value = dr["sa_SubType"].ToString();
                      _oCom.Parameters.Add(":txnDt", OracleDbType.Date).Value = Convert.ToDateTime(dr["sa_Txn_Date"]);
                      _oCom.Parameters.Add(":IsRev", OracleDbType.Int32).Value = Convert.ToInt32(dr["sa_IsReversal"]);
                      _oCom.Parameters.Add(":accCreDt", OracleDbType.Date).Value = Convert.ToDateTime(dr["hpa_CreatedDate"]).Date;
                      _oCom.Parameters.Add(":groupSales", OracleDbType.NVarchar2).Value = dr["hpa_GroupSale_Code"].ToString();
                      _oCom.Parameters.Add(":invNo", OracleDbType.NVarchar2).Value = dr["sa_InvoiceNo"].ToString();
                      _oCom.Parameters.Add(":schemeTp", OracleDbType.NVarchar2).Value = dr["hpa_SchemeType"].ToString();
                      _oCom.Parameters.Add(":scheme", OracleDbType.NVarchar2).Value = dr["hpa_Scheme"].ToString();
                      _oCom.Parameters.Add(":hpTerm", OracleDbType.Int32).Value = Convert.ToInt32(dr["hpa_Term"]);
                      _oCom.Parameters.Add(":hpIntRt", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_IntRate"]);
                      _oCom.Parameters.Add(":hpVal0", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_DP_Comm"]);
                      _oCom.Parameters.Add(":hpVal1", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_DP_Comm"]);
                      _oCom.Parameters.Add(":hpCashPrice", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Cash_Price"]);
                      _oCom.Parameters.Add(":hpTotVat", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Tot_VAT"]);

                      Decimal _netVal = Convert.ToDecimal(dr["hpa_Cash_Price"]) - Convert.ToDecimal(dr["hpa_Tot_VAT"]);
                      _oCom.Parameters.Add(":hpNetVal", OracleDbType.Decimal).Value = _netVal;

                      _oCom.Parameters.Add(":hpDownPay", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Down_Payment"]);
                      _oCom.Parameters.Add(":amtFinance", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_AmountFinance"]);
                      _oCom.Parameters.Add(":totInt", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Tot_Int"]);
                      _serChg = Convert.ToDecimal(dr["hpa_Init_ServiceChg"]) + Convert.ToDecimal(dr["hpa_Inst_ServiceChg"]);
                      _oCom.Parameters.Add(":hpaSerChg", OracleDbType.Decimal).Value = _serChg;
                      _oCom.Parameters.Add(":hireVal", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Hire_Value"]);
                      _oCom.Parameters.Add(":totCash", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Total_Cash"]);
                      _oCom.Parameters.Add(":initInsu", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Init_Insu"]);
                      _oCom.Parameters.Add(":initVat", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Init_VAT"]);
                      _oCom.Parameters.Add(":initStamp", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Init_StampDuty"]);
                      _oCom.Parameters.Add(":initService", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Init_ServiceChg"]);
                      _oCom.Parameters.Add(":instInsu", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Inst_Insu"]);
                      _oCom.Parameters.Add(":instVAT", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Inst_VAT"]);
                      _oCom.Parameters.Add(":instStamp", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Inst_StampDuty"]);
                      _oCom.Parameters.Add(":instService", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Inst_ServiceChg"]);
                      _oCom.Parameters.Add(":buyBackChg", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_BuyBack_Charge"]);
                      _oCom.Parameters.Add(":otherChg", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Other_Charges"]);

                      if (dr["hpa_Status"].ToString() == "RV")
                      {
                          _hpStatus = "R";
                      }
                      else if (dr["hpa_Status"].ToString() == "O")
                      {
                          _hpStatus = "C";
                      }
                      else
                      {
                          _hpStatus = "A";
                      }
                      _oCom.Parameters.Add(":hpStatus", OracleDbType.NVarchar2).Value = _hpStatus;
                      _oCom.Parameters.Add(":closeDt", OracleDbType.Date).Value = Convert.ToDateTime(dr["hpa_ClosedDate"]);
                      _oCom.Parameters.Add(":revertDt", OracleDbType.Date).Value = Convert.ToDateTime(dr["hpa_RevertedDate"]);
                      _oCom.Parameters.Add(":revReleaseDt", OracleDbType.Date).Value = Convert.ToDateTime(dr["hpa_RevertReleasedDate"]);
                      _oCom.Parameters.Add(":responsOfficer", OracleDbType.NVarchar2).Value = dr["hpa_Responsible_Officer"].ToString();
                      _oCom.Parameters.Add(":reShedule", OracleDbType.Int32).Value = Convert.ToInt32(dr["hpa_Rescheduled"]);

                      if (Convert.ToInt32(dr["hpa_Rescheduled"]) == 1)
                      {
                          _resheDt = Convert.ToDateTime(dr["hpa_Rescheduled_Date"]).Date;
                      }
                      else
                      {
                          _resheDt = Convert.ToDateTime("31/Dec/9999").Date;
                      }
                      _oCom.Parameters.Add(":reSheduleDt", OracleDbType.Date).Value = Convert.ToDateTime(_resheDt).Date;
                      _oCom.Parameters.Add(":curDt", OracleDbType.Date).Value = DateTime.Now;
                      _oCom.ExecuteNonQuery();
                  }
              }
              _effect = 1;
              return _effect;
          }
          catch (Exception e)
          {
              //orRollback();
              //sqlRollback();
              _err = e.Message.ToString() + "log";
              return -1;
              //MessageBox.Show(e.Message, "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
              //Application.Exit();
          }

      }

      public Int32 Create_Txn(string _loc, string _acc, DateTime _dt, out string _err)
      {
          try
          {
              SqlCommand _sCom_1 = new SqlCommand();
              OracleCommand _oCom = new OracleCommand();
              string sql = "";
              string hp_Rct = "";
              _err = "";
              Int32 _effect = 0;
              DateTime _recDt = Convert.ToDateTime(_dt).Date;

              sql = "DELETE FROM HPT_TXN WHERE HPT_ACC_NO = :acc";
              _oCom = new OracleCommand(sql, oCnn) { CommandType = CommandType.Text, BindByName = true };
              _oCom.Transaction = oTr;
              _oCom.Parameters.Add(":acc", OracleDbType.NVarchar2).Value = _acc;
              _oCom.ExecuteNonQuery();

              sql = "SELECT * FROM HP_T_Transactions INNER JOIN " +
                    " GN_R_Location ON HP_T_Transactions.hpt_Location = GN_R_Location.loc_Code " +
                    " WHERE (HP_T_Transactions.hpt_Account =@acc) AND (hpt_Date <=@recDt)";

              _sCom_1 = new SqlCommand(sql, sCnn);
              _sCom_1.CommandType = CommandType.Text;
              _sCom_1.Transaction = sTr;
              _sCom_1.Parameters.Add("@acc", SqlDbType.NChar).Value = _acc;
              _sCom_1.Parameters.Add("@recDt", SqlDbType.DateTime).Value = _recDt.Date;
              sAdp = new SqlDataAdapter(_sCom_1);
              Ods = new DataSet();
              sAdp.Fill(Ods, "HP_TXN");

              if (Ods.Tables["HP_TXN"].Rows.Count > 0)
              {
                  foreach (DataRow dr in Ods.Tables["HP_TXN"].Rows)
                  {
                      if (dr["hpt_Trans_Type"].ToString() == "ADJ")
                      {
                          hp_Rct = "HPADJ";
                      }
                      else if (dr["hpt_Trans_Type"].ToString() == "AP")
                      {
                          hp_Rct = "HPAR";
                      }
                      else if (dr["hpt_Trans_Type"].ToString() == "AR")
                      {
                          hp_Rct = "HPAR";
                      }
                      else if (dr["hpt_Trans_Type"].ToString() == "C")
                      {
                          hp_Rct = "CC";
                      }
                      else if (dr["hpt_Trans_Type"].ToString() == "DO")
                      {
                          hp_Rct = "HPDP";
                      }
                      else if (dr["hpt_Trans_Type"].ToString() == "DP")
                      {
                          hp_Rct = "HPDP";
                      }
                      else if (dr["hpt_Trans_Type"].ToString() == "HPCOL")
                      {
                          hp_Rct = "HPRM";
                      }
                      else if (dr["hpt_Trans_Type"].ToString() == "HPRREV")
                      {
                          hp_Rct = "HPREV";
                      }
                      else if (dr["hpt_Trans_Type"].ToString() == "OBEX")
                      {
                          hp_Rct = "EXBAL";
                      }
                      else if (dr["hpt_Trans_Type"].ToString() == "RV")
                      {
                          hp_Rct = "RVRLS";
                      }
                      else
                      {
                          hp_Rct = dr["hpt_Trans_Type"].ToString().ToUpper();
                      }

                      sql = "INSERT INTO HPT_TXN (HPT_SEQ,HPT_REF_NO,HPT_COM,HPT_PC,HPT_ACC_NO,HPT_TXN_DT,HPT_TXN_TP,HPT_TXN_REF,HPT_DESC,HPT_MNL_REF,HPT_DBT,HPT_CRDT,HPT_BAL,HPT_ARS,HPT_CRE_BY,HPT_CRE_DT) " +
                            " VALUES (:seq,:ref,:subCate,:loc,:acc,:dt,:rct,:transRef,:hptDesc,:manualRef,:drAmt,:crAmt,0,0,'UPLOAD',:curDt)";

                      decimal TMP_SEQ = GetSerialID();
                      _oCom = new OracleCommand(sql, oCnn) { CommandType = CommandType.Text, BindByName = true };
                      _oCom.Transaction = oTr;
                      _oCom.Parameters.Add(":seq", OracleDbType.Int32).Value = TMP_SEQ;
                      _oCom.Parameters.Add(":ref", OracleDbType.NVarchar2).Value = dr["hpt_Ref"].ToString();
                      _oCom.Parameters.Add(":subCate", OracleDbType.NVarchar2).Value = dr["loc_Sub_Category"].ToString();
                      _oCom.Parameters.Add(":loc", OracleDbType.NVarchar2).Value = dr["hpt_Location"].ToString();
                      _oCom.Parameters.Add(":acc", OracleDbType.NVarchar2).Value = _acc;
                      _oCom.Parameters.Add(":dt", OracleDbType.Date).Value = Convert.ToDateTime(dr["hpt_Date"]).Date;
                      _oCom.Parameters.Add(":rct", OracleDbType.NVarchar2).Value = hp_Rct;
                      _oCom.Parameters.Add(":transRef", OracleDbType.NVarchar2).Value = dr["hpt_Trans_Ref"].ToString();
                      _oCom.Parameters.Add(":hptDesc", OracleDbType.NVarchar2).Value = dr["hpt_Desc"].ToString().ToUpper();
                      _oCom.Parameters.Add(":manualRef", OracleDbType.NVarchar2).Value = dr["hpt_ManualRef"].ToString();
                      _oCom.Parameters.Add(":drAmt", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpt_Amount_Dr"]);
                      _oCom.Parameters.Add(":crAmt", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpt_Amount_Cr"]);
                      _oCom.Parameters.Add(":curDt", OracleDbType.Date).Value = DateTime.Now;
                      _oCom.ExecuteNonQuery();
                  }
              }
              _effect = 1;
              return _effect;
          }
          catch (Exception e)
          {
              //orRollback();
              //sqlRollback();
              _err = e.Message.ToString() + "Txn";
              return -1;
              //MessageBox.Show(e.Message, "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
              //Application.Exit();
          }

      }

      public Int32 Create_Adj(string _loc, string _acc, DateTime _dt, out string _err)
      {
          try
          {
              SqlCommand _sCom_1 = new SqlCommand();
              OracleCommand _oCom = new OracleCommand();
              string sql = "";
              _err = "";
              Int32 _effect = 0;
              DateTime _recDt = Convert.ToDateTime(_dt).Date;

              sql = "SELECT * FROM HP_T_Adjustment INNER JOIN " +
                    " GN_R_Location ON HP_T_Adjustment.cdn_Location = GN_R_Location.loc_Code " +
                    " WHERE cdn_Account =@acc AND cdn_Date <=@recDt";

              _sCom_1 = new SqlCommand(sql, sCnn);
              _sCom_1.CommandType = CommandType.Text;
              _sCom_1.Transaction = sTr;
              _sCom_1.Parameters.Add("@acc", SqlDbType.NChar).Value = _acc;
              _sCom_1.Parameters.Add("@recDt", SqlDbType.DateTime).Value = _recDt.Date;
              sAdp = new SqlDataAdapter(_sCom_1);
              Ods = new DataSet();
              sAdp.Fill(Ods, "AdjDet");

              if (Ods.Tables["AdjDet"].Rows.Count > 0)
              {
                  foreach (DataRow dr in Ods.Tables["AdjDet"].Rows)
                  {
                      sql = "INSERT INTO HPT_ADJ (HAD_SEQ,HAD_REF,HAD_COM,HAD_PC,HAD_ACC_NO,HAD_DT,HAD_TP,HAD_ADJ_TP,HAD_MNL_REF,HAD_DBT_VAL,HAD_CRDT_VAL,HAD_RMK,HAD_CRE_BY,HAD_CRE_DT) " +
                            " VALUES (:seq,:refCd,:subCate,:loc,:acc,:adjDt,:crTp,:adjTp,:manRef,:amtDr,:amtCr,:rmk,'UPLOAD',:curDt)";

                      decimal TMP_SEQ = GetSerialID();
                      _oCom = new OracleCommand(sql, oCnn) { CommandType = CommandType.Text, BindByName = true };
                      _oCom.Transaction = oTr;
                      _oCom.Parameters.Add(":seq", OracleDbType.Int32).Value = TMP_SEQ;
                      _oCom.Parameters.Add(":refCd", OracleDbType.NVarchar2).Value = dr["cdn_Code"].ToString();
                      _oCom.Parameters.Add(":subCate", OracleDbType.NVarchar2).Value = dr["loc_Sub_Category"].ToString();
                      _oCom.Parameters.Add(":loc", OracleDbType.NVarchar2).Value = dr["cdn_Location"].ToString();
                      _oCom.Parameters.Add(":acc", OracleDbType.NVarchar2).Value = _acc;
                      _oCom.Parameters.Add(":adjDt", OracleDbType.Date).Value = Convert.ToDateTime(dr["cdn_Date"]).Date;
                      _oCom.Parameters.Add(":crTp", OracleDbType.NVarchar2).Value = dr["cdn_CrDr"].ToString();
                      _oCom.Parameters.Add(":adjTp", OracleDbType.NVarchar2).Value = dr["cdn_Adj_Type"].ToString().ToUpper();
                      _oCom.Parameters.Add(":manRef", OracleDbType.NVarchar2).Value = dr["cdn_MaualRef"].ToString().ToUpper();
                      _oCom.Parameters.Add(":amtDr", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["cdn_Amount_Dr"]);
                      _oCom.Parameters.Add(":amtCr", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["cdn_Amount_Cr"]);
                      _oCom.Parameters.Add(":rmk", OracleDbType.NVarchar2).Value = dr["cdn_Remarks"].ToString().ToUpper();
                      _oCom.Parameters.Add(":curDt", OracleDbType.Date).Value = DateTime.Now;
                      _oCom.ExecuteNonQuery();
                  }
              }

              _effect = 1;
              return _effect;
          }
          catch (Exception e)
          {
              //orRollback();
              //sqlRollback();
              _err = e.Message.ToString() + "Adj";
              return -1;
              //MessageBox.Show(e.Message, "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
              //Application.Exit();
          }

      }

      public Int32 Create_Receipt(string _loc, string _acc, DateTime _dt, out string _err)
      {
          try
          {
              SqlCommand _sCom_1 = new SqlCommand();
              OracleCommand _oCom = new OracleCommand();
              string sql = "";
              DateTime _recDt = Convert.ToDateTime(_dt).Date;
              string _hp_Rct_tp = "";
              decimal _calAmt = 0;
              decimal _hp_Bal = 0;
              Int32 j = 0;
              Int32 _effect = 0;
              _err = "";

              sql = "SELECT HP_T_Receipt.*,GN_R_Location.loc_Sub_Category " +
                    " FROM HP_T_Receipt INNER JOIN GN_R_Location ON HP_T_Receipt.hpr_Location = GN_R_Location.loc_Code " +
                    " WHERE hpr_Account_No =@acc AND hpr_Receipt_Date <=@recDt AND hpr_Cancelled = 0";

              _sCom_1 = new SqlCommand(sql, sCnn);
              _sCom_1.CommandType = CommandType.Text;
              _sCom_1.CommandTimeout = 500;
              _sCom_1.Transaction = sTr;
              _sCom_1.Parameters.Add("@acc", SqlDbType.NChar).Value = _acc;
              _sCom_1.Parameters.Add("@recDt", SqlDbType.DateTime).Value = Convert.ToDateTime(_recDt).Date;
              sAdp = new SqlDataAdapter(_sCom_1);
              Ods = new DataSet();
              sAdp.Fill(Ods, "RecDet");

             if (Ods.Tables["RecDet"].Rows.Count > 0)
              {
                  foreach (DataRow dr in Ods.Tables["RecDet"].Rows)
                  {

                      sql = "SELECT * FROM SAT_RECEIPT WHERE sar_com_cd=:com and sar_receipt_no=:recNo";
                      _oCom = new OracleCommand(sql, oCnn);
                      _oCom.CommandType = CommandType.Text;
                      _oCom.Transaction = oTr;
                      _oCom.Parameters.Add(":com", OracleDbType.NVarchar2).Value = "ABL";
                      _oCom.Parameters.Add(":recNo", OracleDbType.NVarchar2).Value = dr["hpr_Ref"].ToString(); 
                      
                      oAdp = new OracleDataAdapter(_oCom);
                      Ods = new DataSet();
                      oAdp.Fill(Ods, "SAT_RECEIPT");

                      if (Ods.Tables["SAT_RECEIPT"].Rows.Count > 0)
                      {

                        if (dr["hpr_Receipt_Type"].ToString() == "D")
                          {
                              _hp_Rct_tp = "HPDPM";
                          }
                          else if (dr["hpr_Receipt_Type"].ToString() == "A")
                          {
                              _hp_Rct_tp = "HPARM";
                          }
                          else
                          {
                              _hp_Rct_tp = "HPRM";
                          }


                          sql = "INSERT INTO SAT_RECEIPT (SAR_SEQ_NO,SAR_COM_CD,SAR_PROFIT_CENTER_CD,SAR_RECEIPT_TYPE,SAR_RECEIPT_NO,SAR_PREFIX,SAR_MANUAL_REF_NO,SAR_RECEIPT_DATE,SAR_DIRECT,SAR_ACC_NO ,SAR_IS_OTH_SHOP,SAR_OTH_SR,SAR_TOT_SETTLE_AMT,SAR_COMM_AMT,SAR_IS_MGR_ISS,SAR_ESD_RATE,SAR_WHT_RATE,SAR_EPF_RATE,SAR_ACT,SAR_CREATE_BY,SAR_CREATE_WHEN,SAR_ANAL_5,SAR_ANAL_6,SAR_ANAL_7) " +
                                " VALUES (:seq,:subCate,:loc,:recTp,:refNo,:preFix,:recNo,:recDt,1,:accNo,:isOthShop,:srCd,:amt,:commAmt,:isMngIssue,:esdRt,:WHTRt,:EPFRt,1,'UPLOAD',:curDt,:commRt,:calAmt,:val4)";

                          decimal TMP_SEQ = GetSerialID();
                          _oCom = new OracleCommand(sql, oCnn) { CommandType = CommandType.Text, BindByName = true };
                          _oCom.Transaction = oTr;
                          _oCom.Parameters.Add(":seq", OracleDbType.Int32).Value = TMP_SEQ;
                          _oCom.Parameters.Add(":subCate", OracleDbType.NVarchar2).Value = dr["loc_Sub_Category"].ToString();
                          _oCom.Parameters.Add(":loc", OracleDbType.NVarchar2).Value = dr["hpr_Location"].ToString();
                          _oCom.Parameters.Add(":recTp", OracleDbType.NVarchar2).Value = _hp_Rct_tp;
                          _oCom.Parameters.Add(":refNo", OracleDbType.NVarchar2).Value = dr["hpr_Ref"].ToString();
                          _oCom.Parameters.Add(":preFix", OracleDbType.NVarchar2).Value = dr["hpr_Prefix"].ToString();
                          _oCom.Parameters.Add(":recNo", OracleDbType.NVarchar2).Value = dr["hpr_Receipt_No"].ToString();
                          _oCom.Parameters.Add(":recDt", OracleDbType.Date).Value = Convert.ToDateTime(dr["hpr_Receipt_Date"]).Date;
                          _oCom.Parameters.Add(":accNo", OracleDbType.NVarchar2).Value = dr["hpr_Account_No"].ToString();
                          _oCom.Parameters.Add(":isOthShop", OracleDbType.Int32).Value = Convert.ToInt32(dr["hpr_OtherShop"]);
                          _oCom.Parameters.Add(":srCd", OracleDbType.NVarchar2).Value = dr["hpr_ShowroomCode"].ToString();
                          _oCom.Parameters.Add(":amt", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpr_Amount"]);
                          _oCom.Parameters.Add(":commAmt", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpr_CommAmount"]);
                          _oCom.Parameters.Add(":isMngIssue", OracleDbType.Int32).Value = Convert.ToInt32(dr["hpr_Manager_Issue"]);
                          _oCom.Parameters.Add(":esdRt", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpr_ESD_Rate"]);
                          _oCom.Parameters.Add(":WHTRt", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpr_WHT_Rate"]);
                          _oCom.Parameters.Add(":EPFRt", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpr_EPF_Rate"]);
                          _oCom.Parameters.Add(":curDt", OracleDbType.Date).Value = DateTime.Now;
                          _oCom.Parameters.Add(":commRt", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpr_CommRate"]);

                          if (Convert.ToDecimal(dr["hpr_Value_4"]) != 0)
                          {
                              _calAmt = Convert.ToDecimal(dr["hpr_Value_4"]) * 100;

                              if (Convert.ToDecimal(dr["hpr_Amount"]) == 0)
                              {
                                  _calAmt = _calAmt / 1;
                              }
                              else
                              {
                                  _calAmt = _calAmt / Convert.ToDecimal(dr["hpr_Amount"]);
                              }
                          }
                          else
                          {
                              _calAmt = 0;
                          }

                          _oCom.Parameters.Add(":calAmt", OracleDbType.Decimal).Value = _calAmt;
                          _oCom.Parameters.Add(":val4", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpr_Value_4"]);


                          _oCom.ExecuteNonQuery();

                          _hp_Bal = Convert.ToDecimal(dr["hpr_Amount"]);

                          if (dr["hpr_Receipt_Type"].ToString() == "D")
                          {
                              sql = "SELECT pay_Ref_ID, pay_Location, pay_Trans_Type, pay_Trans_Ref, pay_Date, pay_Mode, pay_Amount, pay_Remarks, pay_SessionID, pay_CreatedDate,pay_ModifiedDate, pay_TimeStamp, pay_Deposit_Bank, pay_Return_Chq_Amount FROM SA_T_Payments WHERE " +
                                    " (pay_Trans_Ref IN (SELECT sa_InvoiceNo From SA_T_Sales_Header WHERE " +
                                    " (sa_Type = 'HS') AND (sa_AccountNo =@acc) AND " +
                                    " (sa_Txn_Date =@recDt) AND (sa_IsReversal = 0) AND " +
                                    " (sa_Status <> 'C')))";

                              _sCom_1 = new SqlCommand(sql, sCnn);
                              _sCom_1.CommandType = CommandType.Text;
                              _sCom_1.CommandTimeout = 500;
                              _sCom_1.Transaction = sTr;
                              _sCom_1.Parameters.Add("@acc", SqlDbType.NChar).Value = _acc;
                              _sCom_1.Parameters.Add("@recDt", SqlDbType.DateTime).Value = Convert.ToDateTime(dr["hpr_Receipt_Date"]).Date;
                              sAdp = new SqlDataAdapter(_sCom_1);
                              Ods = new DataSet();
                              sAdp.Fill(Ods, "SA_T_Payments");
                          }
                          else
                          {
                              sql = "SELECT pay_Ref_ID, pay_Location, pay_Trans_Type, pay_Trans_Ref, pay_Date, pay_Mode, pay_Amount, pay_Remarks, pay_SessionID, pay_CreatedDate,pay_ModifiedDate, pay_TimeStamp, pay_Deposit_Bank, pay_Return_Chq_Amount FROM SA_T_Payments WHERE (pay_Trans_Ref =@ref) ORDER BY pay_Ref_ID DESC";

                              _sCom_1 = new SqlCommand(sql, sCnn);
                              _sCom_1.CommandType = CommandType.Text;
                              _sCom_1.CommandTimeout = 500;
                              _sCom_1.Transaction = sTr;
                              _sCom_1.Parameters.Add("@ref", SqlDbType.NChar).Value = dr["hpr_Ref"].ToString();
                              sAdp = new SqlDataAdapter(_sCom_1);
                              Ods = new DataSet();
                              sAdp.Fill(Ods, "SA_T_Payments");
                          }

                          if (Ods.Tables["SA_T_Payments"].Rows.Count > 0)
                          {
                              j = 0;

                              foreach (DataRow dr1 in Ods.Tables["SA_T_Payments"].Rows)
                              {
                                  j = j + 1;

                                  if (_hp_Bal > 0)
                                  {
                                      if (dr1["pay_Mode"].ToString() == "CHQ")
                                      {
                                          sql = "SELECT * FROM SA_T_Cheques WHERE (chq_Ref_ID =@payrefId)";

                                          _sCom_1 = new SqlCommand(sql, sCnn);
                                          _sCom_1.CommandType = CommandType.Text;
                                          _sCom_1.CommandTimeout = 500;
                                          _sCom_1.Transaction = sTr;
                                          _sCom_1.Parameters.Add("@payrefId", SqlDbType.NChar).Value = dr1["pay_Ref_ID"].ToString();
                                          sAdp = new SqlDataAdapter(_sCom_1);
                                          Ods = new DataSet();
                                          sAdp.Fill(Ods, "SA_T_Cheques");

                                          if (Ods.Tables["SA_T_Cheques"].Rows.Count > 0)
                                          {
                                              foreach (DataRow dr2 in Ods.Tables["SA_T_Cheques"].Rows)
                                              {
                                                  sql = "INSERT INTO SAT_RECEIPTITM (SARD_SEQ_NO,SARD_LINE_NO,SARD_RECEIPT_NO,SARD_PAY_TP,SARD_SETTLE_AMT,SARD_REF_NO,SARD_CHQ_BANK_CD,SARD_CHQ_BRANCH,SARD_CHQ_DT) " +
                                                        " VALUES (:seq,:lineNo,:ref,'CHEQUE',:payAmt,:cheque,:chqBank,:chqBranch,:chqDt)";

                                                  _oCom = new OracleCommand(sql, oCnn) { CommandType = CommandType.Text, BindByName = true };
                                                  _oCom.Transaction = oTr;
                                                  _oCom.Parameters.Add(":seq", OracleDbType.Int32).Value = TMP_SEQ;
                                                  _oCom.Parameters.Add(":lineNo", OracleDbType.Int32).Value = j;
                                                  _oCom.Parameters.Add(":ref", OracleDbType.NVarchar2).Value = dr["hpr_Ref"].ToString();
                                                  _oCom.Parameters.Add(":payAmt", OracleDbType.Decimal).Value = Convert.ToDecimal(dr1["pay_Amount"]);
                                                  _oCom.Parameters.Add(":cheque", OracleDbType.NVarchar2).Value = dr2["chq_Cheque_No"].ToString();
                                                  _oCom.Parameters.Add(":chqBank", OracleDbType.NVarchar2).Value = dr2["chq_Bank"].ToString();

                                                  string _branch = "";
                                                  if (!string.IsNullOrEmpty(dr2["chq_Branch"].ToString()))
                                                  {
                                                      _branch = dr2["chq_Branch"].ToString().Replace("'", "`").ToUpper();
                                                      //_branch = _branch.Substring(0, 20);
                                                  }
                                                  else
                                                  {
                                                      _branch = "";
                                                  }

                                                  _oCom.Parameters.Add(":chqBranch", OracleDbType.NVarchar2).Value = _branch;
                                                  _oCom.Parameters.Add(":chqDt", OracleDbType.Date).Value = Convert.ToDateTime(dr2["chq_Date"]).Date;
                                                  _oCom.ExecuteNonQuery();

                                                  _hp_Bal = _hp_Bal - Convert.ToDecimal(dr1["pay_Amount"]);
                                              }
                                          }
                                      }
                                      else if (dr1["pay_Mode"].ToString() == "CC")
                                      {
                                          sql = "SELECT * FROM  SA_T_Credit_Card WHERE (cc_Ref_ID =@payrefId)";

                                          _sCom_1 = new SqlCommand(sql, sCnn);
                                          _sCom_1.CommandType = CommandType.Text;
                                          _sCom_1.CommandTimeout = 500;
                                          _sCom_1.Transaction = sTr;
                                          _sCom_1.Parameters.Add("@payrefId", SqlDbType.NChar).Value = dr1["pay_Ref_ID"].ToString();
                                          sAdp = new SqlDataAdapter(_sCom_1);
                                          Ods = new DataSet();
                                          sAdp.Fill(Ods, "SA_T_Credit_Card");

                                          if (Ods.Tables["SA_T_Credit_Card"].Rows.Count > 0)
                                          {
                                              foreach (DataRow dr3 in Ods.Tables["SA_T_Credit_Card"].Rows)
                                              {
                                                  sql = "INSERT INTO SAT_RECEIPTITM (SARD_SEQ_NO,SARD_LINE_NO,SARD_RECEIPT_NO,SARD_PAY_TP,SARD_SETTLE_AMT,SARD_REF_NO,SARD_CREDIT_CARD_BANK,SARD_CC_TP,SARD_CC_BATCH,SARD_CC_IS_PROMO,SARD_CC_PERIOD) " +
                                                        " VALUES (:seq,:lineNo,:ref,'CRCD',:payAmt,:cardNo,:ccBank,:ccType,:batchNo,:isPromo,:ccPeriod)";

                                                  _oCom = new OracleCommand(sql, oCnn) { CommandType = CommandType.Text, BindByName = true };
                                                  _oCom.Transaction = oTr;
                                                  _oCom.Parameters.Add(":seq", OracleDbType.Int32).Value = TMP_SEQ;
                                                  _oCom.Parameters.Add(":lineNo", OracleDbType.Int32).Value = j;
                                                  _oCom.Parameters.Add(":ref", OracleDbType.NVarchar2).Value = dr["hpr_Ref"].ToString();
                                                  _oCom.Parameters.Add(":payAmt", OracleDbType.Decimal).Value = Convert.ToDecimal(dr1["pay_Amount"]);
                                                  _oCom.Parameters.Add(":cardNo", OracleDbType.NVarchar2).Value = dr3["cc_Card_No"].ToString();
                                                  _oCom.Parameters.Add(":ccBank", OracleDbType.NVarchar2).Value = dr3["cc_Bank"].ToString();
                                                  _oCom.Parameters.Add(":ccType", OracleDbType.NVarchar2).Value = dr3["cc_Type"].ToString();
                                                  _oCom.Parameters.Add(":batchNo", OracleDbType.NVarchar2).Value = dr3["cc_Batch_No"].ToString();
                                                  _oCom.Parameters.Add(":isPromo", OracleDbType.Int32).Value = Convert.ToInt32(dr3["cc_IsPromotion"]);
                                                  _oCom.Parameters.Add(":ccPeriod", OracleDbType.Int32).Value = Convert.ToInt32(dr3["cc_PayPeriod"]);

                                                  _oCom.ExecuteNonQuery();

                                                  _hp_Bal = _hp_Bal - Convert.ToDecimal(dr1["pay_Amount"]);
                                              }
                                          }

                                      }
                                      else if (dr1["pay_Mode"].ToString() == "GV" || dr1["pay_Mode"].ToString() == "GVO")
                                      {
                                          sql = "SELECT * FROM  SA_T_Gift_Voucher WHERE (gv_Ref_ID =@payrefId)";

                                          _sCom_1 = new SqlCommand(sql, sCnn);
                                          _sCom_1.CommandType = CommandType.Text;
                                          _sCom_1.CommandTimeout = 500;
                                          _sCom_1.Transaction = sTr;
                                          _sCom_1.Parameters.Add("@payrefId", SqlDbType.NChar).Value = dr1["pay_Ref_ID"].ToString();
                                          sAdp = new SqlDataAdapter(_sCom_1);
                                          Ods = new DataSet();
                                          sAdp.Fill(Ods, "SA_T_Gift_Voucher");

                                          if (Ods.Tables["SA_T_Gift_Voucher"].Rows.Count > 0)
                                          {
                                              foreach (DataRow dr4 in Ods.Tables["SA_T_Gift_Voucher"].Rows)
                                              {
                                                  sql = "INSERT INTO SAT_RECEIPTITM (SARD_SEQ_NO,SARD_LINE_NO,SARD_RECEIPT_NO,SARD_PAY_TP,SARD_SETTLE_AMT,SARD_REF_NO,SARD_GV_ISSUE_LOC) " +
                                                        " VALUES (:seq,:lineNo,:ref,'GVO',:payAmt,:VouNo,:IssueLoc)";

                                                  _oCom = new OracleCommand(sql, oCnn) { CommandType = CommandType.Text, BindByName = true };
                                                  _oCom.Transaction = oTr;
                                                  _oCom.Parameters.Add(":seq", OracleDbType.Int32).Value = TMP_SEQ;
                                                  _oCom.Parameters.Add(":lineNo", OracleDbType.Int32).Value = j;
                                                  _oCom.Parameters.Add(":ref", OracleDbType.NVarchar2).Value = dr["hpr_Ref"].ToString();
                                                  _oCom.Parameters.Add(":payAmt", OracleDbType.Decimal).Value = Convert.ToDecimal(dr1["pay_Amount"]);
                                                  _oCom.Parameters.Add(":VouNo", OracleDbType.NVarchar2).Value = dr4["gv_Voucher_No"].ToString();
                                                  _oCom.Parameters.Add(":IssueLoc", OracleDbType.NVarchar2).Value = dr4["gv_Issue_Loc"].ToString();

                                                  _oCom.ExecuteNonQuery();

                                                  _hp_Bal = _hp_Bal - Convert.ToDecimal(dr1["pay_Amount"]);
                                              }
                                          }
                                      }
                                      else
                                      {
                                          sql = "INSERT INTO SAT_RECEIPTITM (SARD_SEQ_NO,SARD_LINE_NO,SARD_RECEIPT_NO,SARD_PAY_TP,SARD_SETTLE_AMT) " +
                                                   "VALUES(:seq,:lineNo,:ref,'CASH',:payAmt)";


                                          _oCom = new OracleCommand(sql, oCnn) { CommandType = CommandType.Text, BindByName = true };
                                          _oCom.Transaction = oTr;
                                          _oCom.Parameters.Add(":seq", OracleDbType.Int32).Value = TMP_SEQ;
                                          _oCom.Parameters.Add(":lineNo", OracleDbType.Int32).Value = j;
                                          _oCom.Parameters.Add(":ref", OracleDbType.NVarchar2).Value = dr["hpr_Ref"].ToString();
                                          _oCom.Parameters.Add(":payAmt", OracleDbType.Decimal).Value = _hp_Bal;

                                          _oCom.ExecuteNonQuery();

                                          _hp_Bal = 0;
                                      }

                                  }
                              }
                          }
                          else
                          {
                              sql = "INSERT INTO SAT_RECEIPTITM (SARD_SEQ_NO,SARD_LINE_NO,SARD_RECEIPT_NO,SARD_PAY_TP,SARD_SETTLE_AMT) " +
                                    " VALUES (:seq,:lineNo,:ref,'CASH',:payAmt)";


                              _oCom = new OracleCommand(sql, oCnn) { CommandType = CommandType.Text, BindByName = true };
                              _oCom.Transaction = oTr;
                              _oCom.Parameters.Add(":seq", OracleDbType.Int32).Value = TMP_SEQ;
                              _oCom.Parameters.Add(":lineNo", OracleDbType.Int32).Value = j + 1;
                              _oCom.Parameters.Add(":ref", OracleDbType.NVarchar2).Value = dr["hpr_Ref"].ToString();
                              _oCom.Parameters.Add(":payAmt", OracleDbType.Decimal).Value = _hp_Bal;

                              _oCom.ExecuteNonQuery();

                              _hp_Bal = 0;
                          }

                      }
                  }
              }

              sql = "SELECT * FROM  HP_T_Receipt_Reversal INNER JOIN " +
                    " HP_T_Receipt_Reversal_Details ON HP_T_Receipt_Reversal.rrv_Ref = HP_T_Receipt_Reversal_Details.rrvd_Ref INNER JOIN " +
                    " GN_R_Location ON HP_T_Receipt_Reversal.rrv_Location = GN_R_Location.loc_Code " +
                    " WHERE rrv_Account_No =@acc AND rrv_RevDate <=@dt";

              _sCom_1 = new SqlCommand(sql, sCnn);
              _sCom_1.CommandType = CommandType.Text;
              _sCom_1.CommandTimeout = 500;
              _sCom_1.Transaction = sTr;
              _sCom_1.Parameters.Add("@acc", SqlDbType.NChar).Value = _acc;
              _sCom_1.Parameters.Add("@dt", SqlDbType.DateTime).Value = _recDt.Date;
              sAdp = new SqlDataAdapter(_sCom_1);
              Ods = new DataSet();
              sAdp.Fill(Ods, "Rec_Rev_Dt");

              if (Ods.Tables["Rec_Rev_Dt"].Rows.Count > 0)
              {
                  Int32 I = 0;
                  foreach (DataRow dr in Ods.Tables["Rec_Rev_Dt"].Rows)
                  {
                      I = I + 1;
                      if (dr["rrvd_Receipt_Type"].ToString() == "D")
                      {
                          _hp_Rct_tp = "HPDRV";
                      }
                      else
                      {
                          _hp_Rct_tp = "HPREV";
                      }

                      decimal TMP_SEQ_1 = GetSerialID();
                      string Hp_rct = dr["rrv_Ref"].ToString() + I;

                      sql = "INSERT INTO SAT_RECEIPT (SAR_SEQ_NO,SAR_COM_CD,SAR_PROFIT_CENTER_CD,SAR_RECEIPT_TYPE,SAR_RECEIPT_NO,SAR_PREFIX,SAR_MANUAL_REF_NO,SAR_RECEIPT_DATE,SAR_DIRECT,SAR_ACC_NO,SAR_IS_OTH_SHOP,SAR_OTH_SR,SAR_TOT_SETTLE_AMT,SAR_COMM_AMT,SAR_ANAL_5,SAR_CREATE_BY,SAR_CREATE_WHEN,SAR_ACT) " +
                            " VALUES (:seq,:subCate,:loc,:recTp,:refNo,:preFix,:recNo,:recDt,0,:accNo,0,'',:amt,:commAmt,:calAmt,'UPLOAD',:curDt,1)";


                      _oCom = new OracleCommand(sql, oCnn) { CommandType = CommandType.Text, BindByName = true };
                      _oCom.Transaction = oTr;
                      _oCom.Parameters.Add(":seq", OracleDbType.Int32).Value = TMP_SEQ_1;
                      _oCom.Parameters.Add(":subCate", OracleDbType.NVarchar2).Value = dr["loc_Sub_Category"].ToString();
                      _oCom.Parameters.Add(":loc", OracleDbType.NVarchar2).Value = dr["rrv_Location"].ToString();
                      _oCom.Parameters.Add(":recTp", OracleDbType.NVarchar2).Value = _hp_Rct_tp;
                      _oCom.Parameters.Add(":refNo", OracleDbType.NVarchar2).Value = Hp_rct;
                      _oCom.Parameters.Add(":preFix", OracleDbType.NVarchar2).Value = dr["rrvd_Prefix"].ToString();
                      _oCom.Parameters.Add(":recNo", OracleDbType.NVarchar2).Value = dr["rrvd_Receipt_No"].ToString();
                      _oCom.Parameters.Add(":recDt", OracleDbType.Date).Value = Convert.ToDateTime(dr["rrvd_Reverse_Date"]).Date;
                      _oCom.Parameters.Add(":accNo", OracleDbType.NVarchar2).Value = dr["rrv_Account_No"].ToString();
                      _oCom.Parameters.Add(":amt", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["rrvd_Amount"]);
                      _oCom.Parameters.Add(":commAmt", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["rrvd_Commission"]);

                      if (Convert.ToDecimal(dr["rrvd_Commission"]) != 0)
                      {
                          _calAmt = Convert.ToDecimal(dr["rrvd_Commission"]) * 100;

                          if (Convert.ToDecimal(dr["rrvd_Amount"]) == 0)
                          {
                              _calAmt = _calAmt / 1;
                          }
                          else
                          {
                              _calAmt = _calAmt / Convert.ToDecimal(dr["rrvd_Amount"]);
                          }
                      }
                      else
                      {
                          _calAmt = 0;
                      }

                      _oCom.Parameters.Add(":calAmt", OracleDbType.Decimal).Value = _calAmt;
                      _oCom.Parameters.Add(":curDt", OracleDbType.Date).Value = DateTime.Now;

                      _oCom.ExecuteNonQuery();


                      sql = "INSERT INTO SAT_RECEIPTITM (SARD_SEQ_NO,SARD_LINE_NO,SARD_RECEIPT_NO ,SARD_PAY_TP,SARD_SETTLE_AMT) " +
                            " VALUES (:seq,1,:refNo,'CASH',:amt)";


                      _oCom = new OracleCommand(sql, oCnn) { CommandType = CommandType.Text, BindByName = true };
                      _oCom.Transaction = oTr;
                      _oCom.Parameters.Add(":seq", OracleDbType.Int32).Value = TMP_SEQ_1;
                      _oCom.Parameters.Add(":refNo", OracleDbType.NVarchar2).Value = Hp_rct;
                      _oCom.Parameters.Add(":amt", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["rrvd_Amount"]);

                      _oCom.ExecuteNonQuery();
                  }
              }
              _effect = 1;
              return _effect;
          }
          catch (Exception e)
          {
              //orRollback();
              //sqlRollback();
              if (e.Message.Contains("Timeout"))
              {
                  _err = "Unable to connect POS system" + "Receipt";
              }
              else
              {
                  _err = e.Message.ToString() + "Receipt";
              }
              return -1;
              //MessageBox.Show(e.Message, "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
              //Application.Exit();
          }
      }

      public Int32 Create_Customer(string _loc, string _acc, DateTime _dt, out string _err)
      {
          try
          {
              SqlCommand _sCom_1 = new SqlCommand();
              OracleCommand _oCom = new OracleCommand();
              string sql = "";
              Int32 hp_adr = 0;
              string _add1 = "";
              string _add2 = "";
              string _add3 = "";
              string hp_Cus = "";
              Int32 _effect = 0;
              _err = "";

              sql = "DELETE FROM HPT_CUST WHERE HTC_ACC_NO =:acc";
              _oCom = new OracleCommand(sql, oCnn) { CommandType = CommandType.Text, BindByName = true };
              _oCom.Transaction = oTr;
              _oCom.Parameters.Add(":acc", OracleDbType.NVarchar2).Value = _acc;
              _oCom.ExecuteNonQuery();

              sql = "SELECT HP_T_Accounts.hpa_Number, HP_R_Customer_Address.ad_AppNo, HP_R_Customer_Address.ad_Contact, HP_R_Customer_Address.ad_Type, HP_R_Customer_Address.ad_Address_1 , HP_R_Customer_Address.ad_Address_2, HP_R_Customer_Address.ad_Address_3 " +
                    " FROM HP_T_Accounts INNER JOIN HP_R_Customer_Address ON HP_T_Accounts.hpa_Application_No = HP_R_Customer_Address.ad_AppNo " +
                    " WHERE (HP_T_Accounts.hpa_Number =@acc)";

              _sCom_1 = new SqlCommand(sql, sCnn);
              _sCom_1.CommandType = CommandType.Text;
              _sCom_1.Transaction = sTr;
              _sCom_1.Parameters.Add("@acc", SqlDbType.NChar).Value = _acc;
              sAdp = new SqlDataAdapter(_sCom_1);
              Ods = new DataSet();
              sAdp.Fill(Ods, "CusDet");

              if (Ods.Tables["CusDet"].Rows.Count > 0)
              {
                  foreach (DataRow dr in Ods.Tables["CusDet"].Rows)
                  {
                      if (dr["ad_Type"].ToString() == "HOME")
                      {
                          hp_adr = 1;
                      }
                      else if (dr["ad_Type"].ToString() == "PRES")
                      {
                          hp_adr = 2;
                      }
                      else if (dr["ad_Type"].ToString() == "PROD")
                      {
                          hp_adr = 3;
                      }
                      else if (dr["ad_Type"].ToString() == "WPLC")
                      {
                          hp_adr = 4;
                      }

                      sql = "INSERT INTO HPT_CUST (HTC_SEQ,HTC_ACC_NO,HTC_CUST_CD,HTC_CUST_TP,HTC_ADR_TP,HTC_ADR_01,HTC_ADR_02,HTC_ADR_03,HTC_CRE_BY,HTC_CRE_DT) " +
                            " VALUES (:seq,:acc,:conCd,'C',:hpAdr,:add1,:add2,:add3,'UPLOAD',:curDt)";

                      decimal TMP_SEQ = GetSerialID();
                      _oCom = new OracleCommand(sql, oCnn) { CommandType = CommandType.Text, BindByName = true };
                      _oCom.Transaction = oTr;
                      _oCom.Parameters.Add(":seq", OracleDbType.Int32).Value = TMP_SEQ;
                      _oCom.Parameters.Add(":acc", OracleDbType.NVarchar2).Value = dr["hpa_Number"].ToString();
                      _oCom.Parameters.Add(":conCd", OracleDbType.NVarchar2).Value = dr["ad_Contact"].ToString();
                      _oCom.Parameters.Add(":hpAdr", OracleDbType.Int32).Value = hp_adr;

                      _add1 = dr["ad_Address_1"].ToString().Replace("'", "`").ToUpper();
                      _add2 = dr["ad_Address_2"].ToString().Replace("'", "`").ToUpper();
                      _add3 = dr["ad_Address_3"].ToString().Replace("'", "`").ToUpper();

                      _oCom.Parameters.Add(":add1", OracleDbType.NVarchar2).Value = _add1;
                      _oCom.Parameters.Add(":add2", OracleDbType.NVarchar2).Value = _add2;
                      _oCom.Parameters.Add(":add3", OracleDbType.NVarchar2).Value = _add3;
                      _oCom.Parameters.Add(":curDt", OracleDbType.Date).Value = DateTime.Now;
                      _oCom.ExecuteNonQuery();
                  }
              }


              sql = "SELECT HP_T_Customers.cus_Account, HP_T_Customers.cus_Code, HP_T_Customers.cus_Type, GN_R_Contact.cnt_Address_1, GN_R_Contact.cnt_Address_2, GN_R_Contact.cnt_Address_3 " +
                    " FROM HP_T_Customers INNER JOIN GN_R_Contact ON HP_T_Customers.cus_Code = GN_R_Contact.cnt_Code " +
                    " WHERE (HP_T_Customers.cus_Account =@acc)";

              _sCom_1 = new SqlCommand(sql, sCnn);
              _sCom_1.CommandType = CommandType.Text;
              _sCom_1.Transaction = sTr;
              _sCom_1.Parameters.Add("@acc", SqlDbType.NChar).Value = _acc;
              sAdp = new SqlDataAdapter(_sCom_1);
              Ods = new DataSet();
              sAdp.Fill(Ods, "CusDet1");

              if (Ods.Tables["CusDet1"].Rows.Count > 0)
              {
                  foreach (DataRow dr in Ods.Tables["CusDet1"].Rows)
                  {
                      sql = "SELECT * FROM HPT_CUST WHERE HTC_ACC_NO =:acc AND HTC_CUST_TP =:cusTp AND HTC_CUST_CD =:cusCD";
                      _oCom = new OracleCommand(sql, oCnn);
                      _oCom.CommandType = CommandType.Text;
                      _oCom.Transaction = oTr;
                      _oCom.Parameters.Add(":acc", OracleDbType.NVarchar2).Value = dr["cus_Account"].ToString();
                      _oCom.Parameters.Add(":cusTp", OracleDbType.NVarchar2).Value = dr["cus_Type"].ToString();
                      _oCom.Parameters.Add(":cusCD", OracleDbType.NVarchar2).Value = dr["cus_Code"].ToString();
                      oAdp = new OracleDataAdapter(_oCom);
                      Ods = new DataSet();
                      oAdp.Fill(Ods, "HPT_CUST");

                      if (Ods.Tables["HPT_CUST"].Rows.Count <= 0)
                      {
                          sql = "INSERT INTO HPT_CUST (HTC_SEQ,HTC_ACC_NO,HTC_CUST_CD,HTC_CUST_TP,HTC_ADR_TP,HTC_ADR_01,HTC_ADR_02,HTC_ADR_03,HTC_CRE_BY,HTC_CRE_DT) " +
                                   " VALUES (:seq,:acc,:conCd,:cusTp,1,:add1,:add2,:add3,'UPLOAD',:curDt)";

                          decimal TMP_SEQ = GetSerialID();
                          _oCom = new OracleCommand(sql, oCnn) { CommandType = CommandType.Text, BindByName = true };
                          _oCom.Transaction = oTr;
                          _oCom.Parameters.Add(":seq", OracleDbType.Int32).Value = TMP_SEQ;
                          _oCom.Parameters.Add(":acc", OracleDbType.NVarchar2).Value = dr["cus_Account"].ToString();
                          _oCom.Parameters.Add(":conCd", OracleDbType.NVarchar2).Value = dr["cus_Code"].ToString();
                          _oCom.Parameters.Add(":cusTp", OracleDbType.NVarchar2).Value = dr["cus_Type"].ToString();

                          _add1 = dr["cnt_Address_1"].ToString().Replace("'", "`").ToUpper();
                          _add2 = dr["cnt_Address_2"].ToString().Replace("'", "`").ToUpper();
                          _add3 = dr["cnt_Address_3"].ToString().Replace("'", "`").ToUpper();

                          _oCom.Parameters.Add(":add1", OracleDbType.NVarchar2).Value = _add1;
                          _oCom.Parameters.Add(":add2", OracleDbType.NVarchar2).Value = _add2;
                          _oCom.Parameters.Add(":add3", OracleDbType.NVarchar2).Value = _add3;
                          _oCom.Parameters.Add(":curDt", OracleDbType.Date).Value = DateTime.Now;
                          _oCom.ExecuteNonQuery();
                      }
                  }
              }

              sql = "SELECT GN_R_Contact.*, GN_R_Location.loc_Sub_Category " +
                    " FROM HP_T_Customers INNER JOIN GN_R_Contact ON HP_T_Customers.cus_Code = GN_R_Contact.cnt_Code INNER JOIN GN_R_Location ON GN_R_Contact.cnt_Location = GN_R_Location.loc_Code " +
                    " WHERE (HP_T_Customers.cus_Account =@acc)";

              _sCom_1 = new SqlCommand(sql, sCnn);
              _sCom_1.CommandType = CommandType.Text;
              _sCom_1.CommandTimeout = 500;
              _sCom_1.Transaction = sTr;
              _sCom_1.Parameters.Add("@acc", SqlDbType.NChar).Value = _acc;
              sAdp = new SqlDataAdapter(_sCom_1);
              Ods = new DataSet();
              sAdp.Fill(Ods, "CusDet2");

              if (Ods.Tables["CusDet2"].Rows.Count > 0)
              {
                  foreach (DataRow dr in Ods.Tables["CusDet2"].Rows)
                  {
                      hp_Cus = dr["cnt_Title"].ToString() + " " + dr["cnt_First_Name"].ToString() + " " + dr["cnt_Middle_Name"].ToString() + " " + dr["cnt_Last_Name"].ToString();
                      hp_Cus = hp_Cus.ToUpper();

                      sql = "SELECT * FROM MST_BUSENTITY WHERE MBE_CD = :cd AND MBE_TP = 'C' AND MBE_SUB_TP = 'H'";
                      _oCom = new OracleCommand(sql, oCnn);
                      _oCom.CommandType = CommandType.Text;
                      _oCom.Transaction = oTr;
                      _oCom.Parameters.Add(":cd", OracleDbType.NVarchar2).Value = dr["cnt_Code"].ToString();
                      oAdp = new OracleDataAdapter(_oCom);
                      Ods = new DataSet();
                      oAdp.Fill(Ods, "MST_BUSENTITY");

                      if (Ods.Tables["MST_BUSENTITY"].Rows.Count <= 0)
                      {
                          sql = "INSERT INTO MST_BUSENTITY (MBE_COM,MBE_CD,MBE_TP,MBE_SUB_TP,MBE_NAME,MBE_ADD1,MBE_ADD2,MBE_TEL,MBE_FAX,MBE_MOB,MBE_NIC,MBE_EMAIL,MBE_CONTACT,MBE_ACT,MBE_IS_TAX,MBE_TAX_NO,MBE_CRE_BY,MBE_CRE_DT) " +
                                   "VALUES (:locSubTp,:cd,'C','H',:cusName,:add1,:add2,:telHome,:fax,:mob,:Nic,:eMail,:TelRes,1,:taxReg,:taxNo,'UPLOAD',:curDt)";

                          _oCom = new OracleCommand(sql, oCnn) { CommandType = CommandType.Text, BindByName = true };
                          _oCom.Transaction = oTr;
                          _oCom.Parameters.Add(":locSubTp", OracleDbType.NVarchar2).Value = dr["loc_Sub_Category"].ToString();
                          _oCom.Parameters.Add(":cd", OracleDbType.NVarchar2).Value = dr["cnt_Code"].ToString();
                          _oCom.Parameters.Add(":cusName", OracleDbType.NVarchar2).Value = hp_Cus;

                          _add1 = dr["cnt_Address_1"].ToString().Replace("'", "`").ToUpper();
                          _add2 = dr["cnt_Address_2"].ToString().Replace("'", "`").ToUpper();
                          _add3 = dr["cnt_Address_3"].ToString().Replace("'", "`").ToUpper();

                          _oCom.Parameters.Add(":add1", OracleDbType.NVarchar2).Value = _add1 + " " + _add2;
                          _oCom.Parameters.Add(":add2", OracleDbType.NVarchar2).Value = _add3;

                          string _telHome = "";
                          if (!string.IsNullOrEmpty(Convert.ToString(dr["cnt_Tel_Home"])))
                          {
                              _telHome = dr["cnt_Tel_Home"].ToString().Substring(0, 10);
                          }
                          else
                          {
                              _telHome = "";
                          }
                          _oCom.Parameters.Add(":telHome", OracleDbType.NVarchar2).Value = _telHome;
                          _oCom.Parameters.Add(":fax", OracleDbType.NVarchar2).Value = dr["cnt_Fax"].ToString();
                          _oCom.Parameters.Add(":mob", OracleDbType.NVarchar2).Value = dr["cnt_Mobile"].ToString();

                          string _Nic = "";

                          if (!string.IsNullOrEmpty(Convert.ToString(dr["cnt_NIC"])))
                          {
                              _Nic = dr["cnt_NIC"].ToString().Substring(0, 10);
                          }
                          else
                          {
                              _Nic = "";
                          }

                          _oCom.Parameters.Add(":Nic", OracleDbType.NVarchar2).Value = _Nic;
                          _oCom.Parameters.Add(":eMail", OracleDbType.NVarchar2).Value = dr["cnt_Email"].ToString();
                          _oCom.Parameters.Add(":TelRes", OracleDbType.NVarchar2).Value = dr["cnt_Tel_Resident"].ToString();
                          _oCom.Parameters.Add(":taxReg", OracleDbType.Int32).Value = dr["cnt_Tax_Registered"];
                          _oCom.Parameters.Add(":taxNo", OracleDbType.NVarchar2).Value = dr["cnt_Tax_Number"].ToString();
                          _oCom.Parameters.Add(":curDt", OracleDbType.Date).Value = DateTime.Now;

                          _oCom.ExecuteNonQuery();
                      }

                  }
              }
              _effect = 1;
              return _effect;
          }
          catch (Exception e)
          {
              //orRollback();
              //sqlRollback();
              _err = e.Message.ToString() + "Customer";
              return -1;
              //MessageBox.Show(e.Message, "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
              //Application.Exit();
          }

      }

      public Int32 Create_Shedule(string _loc, string _acc, DateTime _dt, out string _err)
      {
          try
          {
              SqlCommand _sCom_1 = new SqlCommand();
              OracleCommand _oCom = new OracleCommand();
              string sql = "";
              decimal _sheTot = 0;
              _err = "";
              Int32 _effect = 0;

              sql = "DELETE FROM HPT_SHED WHERE HTS_ACC_NO =:acc";
              _oCom = new OracleCommand(sql, oCnn) { CommandType = CommandType.Text, BindByName = true };
              _oCom.Transaction = oTr;
              _oCom.Parameters.Add(":acc", OracleDbType.NVarchar2).Value = _acc;
              _oCom.ExecuteNonQuery();

              sql = "SELECT sch_Account, sch_RentalNo, sch_RentalAmount, sch_Interest, sch_DueDate, sch_VAT, sch_ServiceCharge, sch_Insu, sch_StampDuty FROM HP_T_Schedule WHERE sch_Account=@acc";
              _sCom_1 = new SqlCommand(sql, sCnn);
              _sCom_1.CommandType = CommandType.Text;
              _sCom_1.CommandTimeout = 500;
              _sCom_1.Transaction = sTr;
              _sCom_1.Parameters.Add("@acc", SqlDbType.NChar).Value = _acc;
              sAdp = new SqlDataAdapter(_sCom_1);
              Ods = new DataSet();
              sAdp.Fill(Ods, "HP_T_Schedule");

              if (Ods.Tables["HP_T_Schedule"].Rows.Count > 0)
              {
                  foreach (DataRow dr in Ods.Tables["HP_T_Schedule"].Rows)
                  {

                      sql = "INSERT INTO HPT_SHED (HTS_SEQ,HTS_ACC_NO,HTS_RNT_NO,HTS_DUE_DT,HTS_RNT_VAL,HTS_INTR,HTS_VAT,HTS_SER,HTS_INS,HTS_SDT,HTS_TOT_VAL,HTS_CRE_BY,HTS_CRE_DT) " +
                               "VALUES (:seq,:acc,:rntNo,:dueDt,:rntAmt,:interest,:schVat,:serChg,:schInsu,:schStamp,:schTot,'UPLOAD',:curDt)";

                      decimal TMP_SEQ = GetSerialID();
                      _oCom = new OracleCommand(sql, oCnn) { CommandType = CommandType.Text, BindByName = true };
                      _oCom.Transaction = oTr;
                      _oCom.Parameters.Add(":seq", OracleDbType.Int32).Value = TMP_SEQ;
                      _oCom.Parameters.Add(":acc", OracleDbType.NVarchar2).Value = dr["sch_Account"].ToString();
                      _oCom.Parameters.Add(":rntNo", OracleDbType.Int32).Value = dr["sch_RentalNo"];
                      _oCom.Parameters.Add(":dueDt", OracleDbType.Date).Value = Convert.ToDateTime(dr["sch_DueDate"]).Date;
                      _oCom.Parameters.Add(":rntAmt", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["sch_RentalAmount"]);
                      _oCom.Parameters.Add(":interest", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["sch_Interest"]);
                      _oCom.Parameters.Add(":schVat", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["sch_VAT"]);
                      _oCom.Parameters.Add(":serChg", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["sch_ServiceCharge"]);
                      _oCom.Parameters.Add(":schInsu", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["sch_Insu"]);
                      _oCom.Parameters.Add(":schStamp", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["sch_StampDuty"]);

                      _sheTot = Convert.ToDecimal(dr["sch_RentalAmount"]) + Convert.ToDecimal(dr["sch_Insu"]);

                      _oCom.Parameters.Add(":schTot", OracleDbType.Decimal).Value = _sheTot;
                      _oCom.Parameters.Add(":curDt", OracleDbType.Date).Value = DateTime.Now;
                      _oCom.ExecuteNonQuery();
                  }

              }
              _effect = 1;
              return _effect;
          }
          catch (Exception e)
          {
              //orRollback();
              //sqlRollback();
              _err = e.Message.ToString() + "shedule";
              return -1;
              //MessageBox.Show(e.Message, "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
              //Application.Exit();
          }
      }

      public Int32 GetSerialID()
      {
          OracleCommand _oCom = new OracleCommand("sp_getserialid", oCnn);
          _oCom.CommandType = CommandType.StoredProcedure;
          _oCom.Transaction = oTr;
          _oCom.Parameters.Add("o_serialid", OracleDbType.Int32).Direction = ParameterDirection.Output;
          _oCom.ExecuteNonQuery();
          Int32 effects = Convert.ToInt32(_oCom.Parameters["o_serialid"].Value.ToString());

          return effects;

      }


     public Int32 Create_Account(string _loc, string _acc, DateTime _dt,out string _err)
      {
          try
          {
              SqlCommand _sCom_1 = new SqlCommand();
              OracleCommand _oCom = new OracleCommand();
              string sql = "";
              string strBank = "";
              string strFlag = "";
              string accStus = "";
              Int32 _effect =0;
              _err = "";
              DateTime resheDt = Convert.ToDateTime("31/Dec/9999").Date;

              sql = "SELECT HP_T_Accounts.*,GN_R_Location.*,HP_T_Account_Banks.*,HP_T_Account_Flags.* FROM HP_T_Accounts INNER JOIN GN_R_Location ON HP_T_Accounts.hpa_Location = GN_R_Location.loc_Code LEFT OUTER JOIN HP_T_Account_Banks ON HP_T_Accounts.hpa_Number = HP_T_Account_Banks.bnk_Account LEFT OUTER JOIN HP_T_Account_Flags ON HP_T_Accounts.hpa_Number = HP_T_Account_Flags.afl_Account WHERE hpa_Number =@acc";
              _sCom_1 = new SqlCommand(sql, sCnn);
              _sCom_1.CommandType = CommandType.Text;
              _sCom_1.Transaction = sTr;
              _sCom_1.Parameters.Add("@acc", SqlDbType.NChar).Value = _acc;
              sAdp = new SqlDataAdapter(_sCom_1);
              Ods = new DataSet();
              sAdp.Fill(Ods, "AccountDet");

              if (Ods.Tables["AccountDet"].Rows.Count > 0)
              {
                  foreach (DataRow dr in Ods.Tables["AccountDet"].Rows)
                  {
                      if (string.IsNullOrEmpty(dr["bnk_BankCode"].ToString()))
                      {
                          strBank = "001";
                      }
                      else
                      {
                          strBank = dr["bnk_BankCode"].ToString();
                      }

                      if (string.IsNullOrEmpty(dr["afl_FlagCode"].ToString()))
                      {
                          strFlag = "001";
                      }
                      else
                      {
                          strFlag = dr["afl_FlagCode"].ToString();
                      }

                      sql = "INSERT INTO HPT_ACC (HPA_SEQ_NO,HPA_ACC_NO,HPA_COM,HPA_PC,HPA_SEQ,HPA_ACC_CRE_DT,HPA_GRUP_CD,HPA_INVC_NO,HPA_SCH_TP,HPA_SCH_CD,HPA_TERM,HPA_INTR_RT,HPA_DP_COMM,HPA_INST_COMM,HPA_CASH_VAL,HPA_TOT_VAT,HPA_NET_VAL,HPA_DP_VAL,HPA_AF_VAL,HPA_TOT_INTR,HPA_SER_CHG,HPA_HP_VAL,HPA_TC_VAL,HPA_INIT_INS,HPA_INIT_VAT,HPA_INIT_STM,HPA_INIT_SER_CHG,HPA_INST_INS,HPA_INST_VAT,HPA_INST_STM,HPA_INST_SER_CHG,HPA_BUY_VAL,HPA_OTH_CHG,HPA_STUS,HPA_CLS_DT,HPA_RV_DT,HPA_RLS_DT,HPA_ECD_STUS,HPA_ECD_TP,HPA_MGR_CD,HPA_IS_RSCH,HPA_RSCH_DT,HPA_PRT_ACK,HPA_VAL_01,HPA_VAL_02,HPA_VAL_03,HPA_VAL_04,HPA_VAL_05,HPA_CRE_BY,HPA_CRE_DT,HPA_BANK,HPA_FLAG) VALUES " +
                           " (:seq,:acc,:locCate,:loc,:hpSeq,:accDt,:gsCode,:invNo,:schTp,:sch,:Hpterm,:intRt,:hpVal_0,:hpVal_1,:cashPrice,:totVat,:NetValue,:DownPayment,:AmountFinance,:totInt,:serChg,:HireVal,:TotCash,:InitInsu,:InitVat,:InitStamp,:InitServiceChg,:InstInsu,:InstVat,:InstStamp,:InstService,:BuyBackChg,:OtherChg,:accStus,:closeDt,:rvtDt,:rvtreleaseDt,0,'',:responsOfficer,:isReShe,:reSheDt,0,0,0,0,0,0,'UPLOAD',:curDt,:strBank,:strFlag)";

                      decimal TMP_SEQ = GetSerialID();
                      _oCom = new OracleCommand(sql, oCnn) { CommandType = CommandType.Text, BindByName = true };
                      _oCom.Transaction = oTr;
                      _oCom.Parameters.Add(":seq", OracleDbType.Int32).Value = TMP_SEQ;
                      _oCom.Parameters.Add(":acc", OracleDbType.NVarchar2).Value = dr["hpa_Number"].ToString();
                      _oCom.Parameters.Add(":locCate", OracleDbType.NVarchar2).Value = dr["loc_Sub_Category"].ToString();
                      _oCom.Parameters.Add(":loc", OracleDbType.NVarchar2).Value = dr["hpa_Location"].ToString();
                      _oCom.Parameters.Add(":hpSeq", OracleDbType.NVarchar2).Value = dr["hpa_SeqenceNo"];
                      _oCom.Parameters.Add(":accDt", OracleDbType.Date).Value = Convert.ToDateTime(dr["hpa_CreatedDate"]).Date;
                      _oCom.Parameters.Add(":gsCode", OracleDbType.NVarchar2).Value = dr["hpa_GroupSale_Code"].ToString();
                      _oCom.Parameters.Add(":invNo", OracleDbType.NVarchar2).Value = dr["hpa_InvoiceNo"].ToString();
                      _oCom.Parameters.Add(":schTp", OracleDbType.NVarchar2).Value = dr["hpa_SchemeType"].ToString();
                      _oCom.Parameters.Add(":sch", OracleDbType.NVarchar2).Value = dr["hpa_Scheme"].ToString();
                      _oCom.Parameters.Add(":Hpterm", OracleDbType.Int32).Value = dr["hpa_Term"];
                      _oCom.Parameters.Add(":intRt", OracleDbType.Decimal).Value = dr["hpa_IntRate"];
                      _oCom.Parameters.Add(":hpVal_0", OracleDbType.Decimal).Value = dr["hpa_Value_0"];
                      _oCom.Parameters.Add(":hpVal_1", OracleDbType.Decimal).Value = dr["hpa_Value_1"];
                      _oCom.Parameters.Add(":cashPrice", OracleDbType.Decimal).Value = dr["hpa_Cash_Price"];
                      _oCom.Parameters.Add(":totVat", OracleDbType.Decimal).Value = dr["hpa_Tot_VAT"];
                      _oCom.Parameters.Add(":NetValue", OracleDbType.Decimal).Value = dr["hpa_Net_Value"];
                      _oCom.Parameters.Add(":DownPayment", OracleDbType.Decimal).Value = dr["hpa_Down_Payment"];
                      _oCom.Parameters.Add(":AmountFinance", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_AmountFinance"]);
                      _oCom.Parameters.Add(":totInt", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Tot_Int"]);
                      _oCom.Parameters.Add(":serChg", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Init_ServiceChg"]) + Convert.ToDecimal(dr["hpa_Inst_ServiceChg"]);
                      _oCom.Parameters.Add(":HireVal", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Hire_Value"]);
                      _oCom.Parameters.Add(":TotCash", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Total_Cash"]);
                      _oCom.Parameters.Add(":InitInsu", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Init_Insu"]);
                      _oCom.Parameters.Add(":InitVat", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Init_VAT"]);
                      _oCom.Parameters.Add(":InitStamp", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Init_StampDuty"]);
                      _oCom.Parameters.Add(":InitServiceChg", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Init_ServiceChg"]);
                      _oCom.Parameters.Add(":InstInsu", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Inst_Insu"]);
                      _oCom.Parameters.Add(":InstVat", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Inst_VAT"]);
                      _oCom.Parameters.Add(":InstStamp", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Inst_StampDuty"]);
                      _oCom.Parameters.Add(":InstService", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Inst_ServiceChg"]);
                      _oCom.Parameters.Add(":BuyBackChg", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_BuyBack_Charge"]);
                      _oCom.Parameters.Add(":OtherChg", OracleDbType.Decimal).Value = Convert.ToDecimal(dr["hpa_Other_Charges"]);

                      if (dr["hpa_SchemeType"].ToString() == "RV")
                      {
                          accStus = "R";
                      }
                      else if (dr["hpa_SchemeType"].ToString() == "O")
                      {
                          accStus = "C";
                      }
                      else
                      {
                          accStus = "A";
                      }

                      _oCom.Parameters.Add(":accStus", OracleDbType.NVarchar2).Value = accStus;
                      _oCom.Parameters.Add(":closeDt", OracleDbType.Date).Value = Convert.ToDateTime(dr["hpa_ClosedDate"]).Date;
                      _oCom.Parameters.Add(":rvtDt", OracleDbType.Date).Value = Convert.ToDateTime(dr["hpa_RevertedDate"]).Date;
                      _oCom.Parameters.Add(":rvtreleaseDt", OracleDbType.Date).Value = Convert.ToDateTime(dr["hpa_RevertReleasedDate"]).Date;
                      _oCom.Parameters.Add(":responsOfficer", OracleDbType.NVarchar2).Value = dr["hpa_Responsible_Officer"].ToString();
                      _oCom.Parameters.Add(":isReShe", OracleDbType.Int32).Value = Convert.ToInt32(dr["hpa_Rescheduled"]);

                      if (Convert.ToInt32(dr["hpa_Rescheduled"]) == 1)
                      {
                          resheDt = Convert.ToDateTime(dr["hpa_Rescheduled_Date"]).Date;
                      }
                      else
                      {
                          resheDt = Convert.ToDateTime("31/Dec/9999").Date;
                      }

                      _oCom.Parameters.Add(":reSheDt", OracleDbType.Date).Value = Convert.ToDateTime(resheDt).Date;
                      _oCom.Parameters.Add(":curDt", OracleDbType.Date).Value = DateTime.Now;
                      _oCom.Parameters.Add(":strBank", OracleDbType.NVarchar2).Value = strBank;
                      _oCom.Parameters.Add(":strFlag", OracleDbType.NVarchar2).Value = strFlag;

                      _oCom.ExecuteNonQuery();


                  }

              }
              _effect = 1;
              return _effect;
          }

          catch (Exception e)
          {
              //orRollback();
              //sqlRollback();
              _err = e.Message.ToString() + "account";
              return -1;
              //MessageBox.Show(e.Message, "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
              //Application.Exit();
          }
      }

    }
}
