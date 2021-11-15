using EMS_Upload_Console;
using FF.BusinessObjects;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UtilityClasses;
using System.Linq;
using MySql.Data.MySqlClient;
 
using System.IO;
using System.Globalization;
namespace Hero_Service_Consol
{
    class InterTransferRequests : Conn
    {

        private OracleDataAdapter oDA;
        private OracleCommand _oCom;
        private MySqlCommand _oComMysql;
        private MySqlDataAdapter oDAMysql;
        String sqlQry = string.Empty;

        public void GetBigDealInvoice(string _fromCom, string _toCom, Int16 _db, string Team)
        { // Get From mySQl
            try
            {
                Console.WriteLine("");

                MySqlBegin();
                EmsBegin(1);
                List<InvoiceHeader> oHeader = new List<InvoiceHeader>();
                List<InvoiceItem> oDetails = new List<InvoiceItem>();
                List<InvoiceItemTax> oTax = new List<InvoiceItemTax>();
                List<RecieptHeader> oRec= new List<RecieptHeader>();
                List<RecieptItem> oRecDet = new List<RecieptItem>();
                #region Get Header
                oDA = new OracleDataAdapter();
                oDAMysql = new MySqlDataAdapter();
                DataSet dsTemp = new DataSet();

                string _pb = string.Empty;
                string _pbLvl = string.Empty;
                string _pc = string.Empty;
                string _cust = string.Empty;
                string _docType = string.Empty;
                string _customer = string.Empty;
                string _invType = string.Empty;

                List<InterCompanySalesParameter> _priceParam = GetInterCompanyParameter(Team, _toCom, string.Empty, _fromCom, string.Empty);

                if (_priceParam != null && _priceParam.Count > 0)
                {
                    _pc = _priceParam[0].Sritc_frm_prof;
                    _cust = _priceParam[0].Sritc_customer;
                    _pb = _priceParam[0].Sritc_pb;
                    _pbLvl = _priceParam[0].Sritc_pb_lvl;
                    _customer = _priceParam[0].Sritc_customer;
                    _invType = _priceParam[0].SRITC_INV_TYPE;
                }


                sqlQry = @"SELECT * FROM scm_sat_hdr_support WHERE  web_update = 1 and sah_stus<>'C' AND sah_inv_no IS NULL";
           
                _oComMysql = new MySqlCommand(sqlQry, oConnMySql);
                _oComMysql.Transaction = oTrMySql;
                oDAMysql.SelectCommand = _oComMysql;
                oDAMysql.Fill(dsTemp, "SAT_HDR");

                if (dsTemp.Tables[0].Rows.Count > 0)
                {
                    oHeader = DataTableExtensionsTolist.ToGenericList<InvoiceHeader>(dsTemp.Tables[0], InvoiceHeader.ConvertTotal);
              
                }

                #endregion

                Console.WriteLine("Request Records Count :- " + oHeader.Count.ToString());

              
               
                 
                    #region Get Items

                    foreach (InvoiceHeader _inv in oHeader)
                    {
                        if(CheckInvoice(_inv.Sah_ref_doc,_inv.Sah_com )==false)// Check invoice avaliable in SCM2
                        {

                        string _invoicePrefix = GetInvoicePrefix(_inv.Sah_com, _inv.Sah_pc, _inv.Sah_inv_tp);
                         MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
                         _invoiceAuto.Aut_cate_cd = _inv.Sah_pc;
                        _invoiceAuto.Aut_cate_tp = "PRO";
                        _invoiceAuto.Aut_direction = 1;
                        _invoiceAuto.Aut_modify_dt = null;
                        _invoiceAuto.Aut_moduleid = _inv.Sah_inv_tp;
                        _invoiceAuto.Aut_number = 0;
                        _invoiceAuto.Aut_start_char =  _invoicePrefix;
                        _invoiceAuto.Aut_year = Convert.ToDateTime(_inv.Sah_dt).Year;
                     

                        _invoiceAuto.Aut_year = null;
                        MasterAutoNumber InvoiceAuto = GetAutoNumber(_invoiceAuto.Aut_moduleid, _invoiceAuto.Aut_direction, _invoiceAuto.Aut_start_char, _invoiceAuto.Aut_cate_tp, _invoiceAuto.Aut_cate_cd, _invoiceAuto.Aut_modify_dt, _invoiceAuto.Aut_year);
                         string     _invNo = _invoiceAuto.Aut_start_char + InvoiceAuto.Aut_number.ToString("00000", CultureInfo.InvariantCulture);
                        _invoiceAuto.Aut_year = null;
                        _invoiceAuto.Aut_modify_dt = null;
                         UpdateInvoiceAutoNumber(_invoiceAuto);
                         _inv.Sah_inv_no = _invNo;
                        Int32 _invSeq = 0;
                        _invSeq= GetSerialID();
                         _inv.Sah_seq_no= _invSeq;
                       
                        SaveSalesHeader(_inv);
                        oDAMysql = new MySqlDataAdapter();
                        DataSet dsItemDetails = new DataSet();
                        sqlQry = @"select * from scm_sat_itm_support where  sah_seq_id = @P_sah_seq_id";
                        _oComMysql = new MySqlCommand(sqlQry, oConnMySql);
                        _oComMysql.Parameters.Add("@P_sah_seq_id", MySqlDbType.Int32).Value = _inv.sah_seq_id;
                        _oComMysql.Transaction = oTrMySql;
                        oDAMysql.SelectCommand = _oComMysql;
                        oDAMysql.Fill(dsItemDetails, "SAT_ITM");

                        List<InvoiceItem> oItem = new List<InvoiceItem>();

                        if (dsItemDetails.Tables[0].Rows.Count > 0)
                        {
                            oItem = DataTableExtensionsTolist.ToGenericList<InvoiceItem>(dsItemDetails.Tables[0], InvoiceItem.ConvertTotal);
                            oDetails.AddRange(oItem);
                        }

                        foreach (InvoiceItem _ser in oDetails)
                        {
                            _ser.Sad_seq_no = _inv.Sah_seq_no;
                            _ser.Sad_inv_no = _inv.Sah_inv_no;
                            SaveSalesItem(_ser);


                            List<MasterItemTax> _itmTax = new List<MasterItemTax>();
                            _itmTax = GetItemTax(_inv.Sah_com, _ser.Sad_itm_cd, _ser.Sad_itm_stus, string.Empty, string.Empty);

                            foreach (MasterItemTax _one in _itmTax)
                            {
                                decimal _disAmt = 0;
                                InvoiceItemTax _tax = new InvoiceItemTax();
                                _tax.Satx_inv_no = _ser.Sad_inv_no;
                                _tax.Satx_itm_cd = _ser.Sad_itm_cd;
                                _tax.Satx_itm_line = _ser.Sad_itm_line;
                                _disAmt = _ser.Sad_unit_amt * _ser.Sad_disc_rt / 100;
                                if (_ser.Sad_itm_tax_amt == 0)
                                {
                                    _tax.Satx_itm_tax_amt = 0;
                                }
                                else
                                {
                                    _tax.Satx_itm_tax_amt = Math.Round(((_ser.Sad_unit_rt - _disAmt) * _one.Mict_tax_rate / 100) * _ser.Sad_qty, 0);//_tax.Satx_itm_tax_amt = (_itm.Sad_unit_rt * _one.Mict_tax_rate / 100) * _itm.Sad_qty;
                                }
                                _tax.Satx_itm_tax_rt = _one.Mict_tax_rate;
                                _tax.Satx_itm_tax_tp = _one.Mict_tax_cd;
                                _tax.Satx_job_line = 0;
                                _tax.Satx_job_no = "";

                                _tax.Satx_seq_no = _inv.Sah_seq_no;
                                _tax.Satx_inv_no = _inv.Sah_inv_no;
                                SaveSalesItemTax(_tax);
                                
                            }

                           
                        }



                        //List<InvoiceItemTax> oItemTax = new List<InvoiceItemTax>();
                        //oDAMysql = new MySqlDataAdapter();
                        //DataSet dsItemDetailsTax = new DataSet();
                        //sqlQry = @"SELECT * FROM     SCM_sat_itm_tax WHERE satx_seq_no= :P_sad_seq_no";
                        //_oComMysql = new MySqlCommand(sqlQry, oConnMySql);
                        //_oComMysql.Parameters.Add(":P_sad_seq_no", OracleDbType.Int32).Value = _inv.sah_seq_id;
                        //_oComMysql.Transaction = oTrMySql;
                        //oDAMysql.SelectCommand = _oComMysql;
                        //oDAMysql.Fill(dsItemDetailsTax, "SAT_ITM_TAX");

                        //if (dsItemDetailsTax.Tables[0].Rows.Count > 0)
                        //{
                        //    oItemTax = DataTableExtensionsTolist.ToGenericList<InvoiceItemTax>(dsItemDetailsTax.Tables[0], InvoiceItemTax.ConvertTotal);
                        //    oTax.AddRange(oItemTax);
                        //}
                        //foreach (InvoiceItemTax _ser in oTax)
                        //{
                        //    _ser.Satx_seq_no = _inv.Sah_seq_no;
                        //    _ser.Satx_inv_no = _inv.Sah_inv_no;
                        //    SaveSalesItemTax(_ser);
                        //}

                        List<RecieptHeader> oReceipt = new List<RecieptHeader>();
                        oDAMysql = new MySqlDataAdapter();
                        DataSet dsReceipt = new DataSet();
                        sqlQry = @"select * from scm_sat_receipt_support WHERE web_order_id= @P_web_order_id";
                        _oComMysql = new MySqlCommand(sqlQry, oConnMySql);
                        _oComMysql.Parameters.Add("@P_web_order_id", MySqlDbType.Int32).Value = _inv.web_order_id;
                        _oComMysql.Transaction = oTrMySql;
                        oDAMysql.SelectCommand = _oComMysql;
                        oDAMysql.Fill(dsReceipt, "RecieptHeader");

                        if (dsReceipt.Tables[0].Rows.Count > 0)
                        {
                            oReceipt = DataTableExtensionsTolist.ToGenericList<RecieptHeader>(dsReceipt.Tables[0], RecieptHeader.ConvertTotal);
                            oRec.AddRange(oReceipt);
                        }

                        foreach (RecieptHeader _recHdr in oRec)
                        {
                            MasterAutoNumber _receiptAuto = new MasterAutoNumber();


                            _receiptAuto.Aut_cate_cd =  _inv.Sah_pc;
                            _receiptAuto.Aut_cate_tp = "PRO";
                            _receiptAuto.Aut_direction = 1;
                            _receiptAuto.Aut_modify_dt = null;
                            _receiptAuto.Aut_moduleid = "RECEIPT";
                            _receiptAuto.Aut_number = 0;
                            _receiptAuto.Aut_start_char = "DIR";
                            _receiptAuto.Aut_year = Convert.ToDateTime(_inv.Sah_dt).Year;


                            MasterAutoNumber _number = GetAutoNumber(_receiptAuto.Aut_moduleid, _receiptAuto.Aut_direction, _receiptAuto.Aut_start_char, _receiptAuto.Aut_cate_tp, _receiptAuto.Aut_cate_cd, _receiptAuto.Aut_modify_dt, _receiptAuto.Aut_year);
                            // _receiptAuto.Aut_cate_cd + "-" + _receiptAuto.Aut_start_char + "-" + _number.Aut_number.ToString("000000", CultureInfo.InvariantCulture);
                           string    _receiptNo = _receiptAuto.Aut_cate_cd + _receiptAuto.Aut_start_char + string.Format("{0:0000}", _number.Aut_number);
                            UpdateAutoNumber(_receiptAuto);

                            _recHdr.Sar_receipt_no = _receiptNo;
                            _recHdr.Sar_seq_no= GetSerialID();
                            SaveReceiptHeader(_recHdr);

                            List<RecieptItem> oReceiptDet = new List<RecieptItem>();
                            oDAMysql = new MySqlDataAdapter();
                            DataSet dsReceiptDet = new DataSet();
                            sqlQry = @"select * from scm_sat_receiptitm_support WHERE sar_seq_id= @P_sar_seq_id";
                            _oComMysql = new MySqlCommand(sqlQry, oConnMySql);
                            _oComMysql.Parameters.Add("@P_sar_seq_id", MySqlDbType.Int32).Value = _recHdr.sar_seq_id;
                            _oComMysql.Transaction = oTrMySql;
                            oDAMysql.SelectCommand = _oComMysql;
                            oDAMysql.Fill(dsReceiptDet, "RecieptItem");

                            if (dsReceiptDet.Tables[0].Rows.Count > 0)
                            {
                                oReceiptDet = DataTableExtensionsTolist.ToGenericList<RecieptItem>(dsReceiptDet.Tables[0], RecieptItem.ConvertTotal);
                                oRecDet.AddRange(oReceiptDet);
                            }

                            foreach (RecieptItem _ser in oRecDet)
                            {
                                _ser.Sard_ref_no = _receiptNo;
                                _ser.Sard_seq_no = _recHdr.Sar_seq_no;
                                SaveReceiptItem(_ser);
                            }
                        }


                        sqlQry = @"UPDATE scm_sat_hdr_support SET web_update = 1,sah_inv_no=@P_sah_inv_no WHERE sah_ref_doc = @P_sah_ref_doc  ";
                        _oComMysql = new MySqlCommand(sqlQry, oConnMySql);
                        _oComMysql.Parameters.Add("@P_sah_inv_no", MySqlDbType.String).Value = _inv.Sah_inv_no;
                        _oComMysql.Parameters.Add("@P_sah_ref_doc", MySqlDbType.String).Value = _inv.Sah_ref_doc;
                        _oComMysql.Transaction = oTrMySql;
                        int resultFinal = _oComMysql.ExecuteNonQuery();


                    }

                    Console.WriteLine("Invoice items Count :- " + oDetails.Count.ToString());

                    #endregion


          // ScmCommit();
                EmsCommit();
                MySqlCommit();

                PuchaseOrderGeneration oPogeneration = new PuchaseOrderGeneration();

                oPogeneration.Send_SMTPMail("priyamal@abansgroup.com", "maheshikad@abansgroup.com", "chamald@abansgroup.com", "chamald@abansgroup.com", "chamald@abansgroup.com", " Big Deal Agent", "Big No :"+ _inv.Sah_ref_doc +"uploaded to SCM2");

                    }
            }
            catch (Exception ex)
            {
                MySqlRollback();
                EmsRollback ();
              //  ScmRollback();
           
                PuchaseOrderGeneration oPogeneration = new PuchaseOrderGeneration();
                oPogeneration.SendSMS("0773973588", "0773973588", "HMC/BDL Agent Issue" + ex.Message);
                oPogeneration.Send_SMTPMail("chamald@abansgroup.com", "chamald@abansgroup.com", "chamald@abansgroup.com", "chamald@abansgroup.com", "chamald@abansgroup.com", "HMC/BDL Agent Issue", "Inter Transfer" + ex.Message);
                Console.WriteLine("");
                Console.WriteLine("***********************ERROR :-" + ex.Message);
            }
        }
        public MasterAutoNumber GetAutoNumber(string _module, Int32? _direction, string _startChar, string _catType, string _catCode, DateTime? _modifyDate, Int32? _year)
        {
            OracleParameter[] param = new OracleParameter[8];
            MasterAutoNumber _masterAutoNumber = new MasterAutoNumber();

            (param[0] = new OracleParameter("p_module", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _module;
            (param[1] = new OracleParameter("p_direction", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _direction;
            (param[2] = new OracleParameter("p_startchar", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _startChar;
            (param[3] = new OracleParameter("p_cattype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _catType;
            (param[4] = new OracleParameter("p_catcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _catCode;
            (param[5] = new OracleParameter("p_modifydate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _modifyDate;
            (param[6] = new OracleParameter("p_year", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _year;
            param[7] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblAuto", "sp_autonumber", CommandType.StoredProcedure, false, param);
            //   p_module IN NVARCHAR2,p_direction IN NUMBER,p_startchar IN NVARCHAR2,p_cattype in NVARCHAR2,p_catcode in NVARCHAR2,p_modifydate in DATE,p_year in NUMBER ,c_data out SYS_REFCURSOR
            if (_dtResults.Rows.Count > 0)
            {
                _masterAutoNumber = DataTableExtensionsTolist.ToGenericList<MasterAutoNumber>(_dtResults, MasterAutoNumber.ConvertTotal)[0];
            }
            else if (_dtResults.Rows.Count == 0)
            {
                //_masterAutoNumber.Aut_number = 999;//since the auto number not generated, it is hard corded.

                _masterAutoNumber.Aut_cate_cd = _catCode;
                _masterAutoNumber.Aut_cate_tp = _catType;
                _masterAutoNumber.Aut_direction = _direction;
                _masterAutoNumber.Aut_modify_dt = _modifyDate;
                _masterAutoNumber.Aut_moduleid = _module;
                _masterAutoNumber.Aut_number = 1;
                _masterAutoNumber.Aut_start_char = _startChar;
                _masterAutoNumber.Aut_year = _year;
            }

            //if (_masterAutoNumber == null)
            //{
            //    _masterAutoNumber.Aut_cate_cd = _catCode;
            //    _masterAutoNumber.Aut_cate_tp = _catType;
            //    _masterAutoNumber.Aut_direction = _direction;
            //    _masterAutoNumber.Aut_modify_dt = _modifyDate;
            //    _masterAutoNumber.Aut_moduleid = _module;
            //    _masterAutoNumber.Aut_number = 1;
            //    _masterAutoNumber.Aut_start_char = _startChar;
            //    _masterAutoNumber.Aut_year = _year;
            //}

            return _masterAutoNumber;
        }
        public Int32 UpdateInvoiceAutoNumber(MasterAutoNumber _auto)
        {
            //SP_UPDATEAUTOINVOICE(p_module in NVARCHAR2,p_direct in NUMBER,p_startchar in NVARCHAR2,p_cattp in NVARCHAR2,p_catcd in NVARCHAR2,p_year in NUMBER,o_effect out NUMBER)

            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_module", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _auto.Aut_moduleid;
            (param[1] = new OracleParameter("p_direct", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _auto.Aut_direction;
            (param[2] = new OracleParameter("p_startchar", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _auto.Aut_start_char;
            (param[3] = new OracleParameter("p_cattp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _auto.Aut_cate_tp;
            (param[4] = new OracleParameter("p_catcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _auto.Aut_cate_cd;
            (param[5] = new OracleParameter("p_year", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _auto.Aut_year;
            param[6] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            int _effect = 0;
            _effect = (Int16)UpdateRecords(oConnEMS, "SP_UPDATEAUTOINVOICE", CommandType.StoredProcedure, param);
           

            return _effect;

        }
        public Int16 UpdateAutoNumber(MasterAutoNumber _masterAutoNumber)
        {
            int effected = 0;
            OracleParameter[] param = new OracleParameter[8];

            (param[0] = new OracleParameter("p_module", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _masterAutoNumber.Aut_moduleid;
            (param[1] = new OracleParameter("p_direction", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _masterAutoNumber.Aut_direction;
            (param[2] = new OracleParameter("p_startchar", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _masterAutoNumber.Aut_start_char;
            (param[3] = new OracleParameter("p_cattype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _masterAutoNumber.Aut_cate_tp;
            (param[4] = new OracleParameter("p_catcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _masterAutoNumber.Aut_cate_cd;
            (param[5] = new OracleParameter("p_modifydate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _masterAutoNumber.Aut_modify_dt;
            (param[6] = new OracleParameter("p_year", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _masterAutoNumber.Aut_year;
            param[7] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effected = (Int16)UpdateRecords(oConnEMS, "sp_updateautonumber", CommandType.StoredProcedure, param);
 
            return (Int16)effected;
        }
        public Int32 SaveSalesHeader(InvoiceHeader _invoiceHeader)
        {
            OracleParameter[] param = new OracleParameter[71];
            Int32 effects = 0;

            (param[0] = new OracleParameter("p_anal_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_anal_1;
            (param[1] = new OracleParameter("p_anal_10", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_anal_10;
            (param[2] = new OracleParameter("p_anal_11", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_anal_11;
            (param[3] = new OracleParameter("p_anal_12", OracleDbType.Date, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_anal_12;
            (param[4] = new OracleParameter("p_anal_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_anal_2;
            (param[5] = new OracleParameter("p_anal_3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_anal_3;
            (param[6] = new OracleParameter("p_anal_4", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_anal_4;
            (param[7] = new OracleParameter("p_anal_5", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_anal_5;
            (param[8] = new OracleParameter("p_anal_6", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_anal_6;
            (param[9] = new OracleParameter("p_anal_7", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_anal_7;
            (param[10] = new OracleParameter("p_anal_8", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_anal_8;
            (param[11] = new OracleParameter("p_anal_9", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_anal_9;
            (param[12] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_com;
            (param[13] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_cre_by;
            (param[14] = new OracleParameter("p_cre_when", OracleDbType.Date, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_cre_when;
            (param[15] = new OracleParameter("p_currency", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_currency;
            (param[16] = new OracleParameter("p_cus_add1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_cus_add1;
            (param[17] = new OracleParameter("p_cus_add2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_cus_add2;
            (param[18] = new OracleParameter("p_cus_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_cus_cd;
            (param[19] = new OracleParameter("p_cus_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_cus_name;
            (param[20] = new OracleParameter("p_d_cust_add1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_d_cust_add1;
            (param[21] = new OracleParameter("p_d_cust_add2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_d_cust_add2;
            (param[22] = new OracleParameter("p_d_cust_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_d_cust_cd;
            (param[23] = new OracleParameter("p_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_dt;
            (param[24] = new OracleParameter("p_epf_rt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_epf_rt;
            (param[25] = new OracleParameter("p_esd_rt", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_esd_rt;
            (param[26] = new OracleParameter("p_ex_rt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_ex_rt;
            (param[27] = new OracleParameter("p_inv_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_inv_no;
            (param[28] = new OracleParameter("p_inv_sub_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_inv_sub_tp;
            (param[29] = new OracleParameter("p_inv_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_inv_tp;
            (param[30] = new OracleParameter("p_is_acc_upload", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_is_acc_upload;
            (param[31] = new OracleParameter("p_man_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_man_cd;
            (param[32] = new OracleParameter("p_man_ref", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_man_ref;
            (param[33] = new OracleParameter("p_manual", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_manual;
            (param[34] = new OracleParameter("p_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_mod_by;
            (param[35] = new OracleParameter("p_mod_when", OracleDbType.Date, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_mod_when;
            (param[36] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_pc;
            (param[37] = new OracleParameter("p_pdi_req", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_pdi_req;
            (param[38] = new OracleParameter("p_ref_doc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_ref_doc;
            (param[39] = new OracleParameter("p_remarks", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_remarks;
            (param[40] = new OracleParameter("p_sales_chn_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_sales_chn_cd;
            (param[41] = new OracleParameter("p_sales_chn_man", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_sales_chn_man;
            (param[42] = new OracleParameter("p_sales_ex_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_sales_ex_cd;
            (param[43] = new OracleParameter("p_sales_region_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_sales_region_cd;
            (param[44] = new OracleParameter("p_sales_region_man", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_sales_region_man;
            (param[45] = new OracleParameter("p_sales_sbu_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_sales_sbu_cd;
            (param[46] = new OracleParameter("p_sales_sbu_man", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_sales_sbu_man;
            (param[47] = new OracleParameter("p_sales_str_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_sales_str_cd;
            (param[48] = new OracleParameter("p_sales_zone_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_sales_zone_cd;
            (param[49] = new OracleParameter("p_sales_zone_man", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_sales_zone_man;
            (param[50] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_seq_no;
            (param[51] = new OracleParameter("p_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_session_id;
            (param[52] = new OracleParameter("p_structure_seq", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_structure_seq;
            (param[53] = new OracleParameter("p_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_stus;
            (param[54] = new OracleParameter("p_town_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_town_cd;
            (param[55] = new OracleParameter("p_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_tp;
            (param[56] = new OracleParameter("p_wht_rt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_wht_rt;
            (param[57] = new OracleParameter("p_direct", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_direct ? 1 : 0;
            (param[58] = new OracleParameter("p_tax_inv", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_tax_inv;

            (param[59] = new OracleParameter("p_grup_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_grup_cd;
            (param[60] = new OracleParameter("p_acc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_acc_no;
            (param[61] = new OracleParameter("p_tax_exempted", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_tax_exempted;
            (param[62] = new OracleParameter("p_is_svat", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_is_svat;
            (param[63] = new OracleParameter("p_fin_chrg", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_fin_chrg;
            (param[64] = new OracleParameter("p_del_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_del_loc;
            (param[65] = new OracleParameter("p_grn_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_grn_com;
            (param[66] = new OracleParameter("p_grn_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_grn_loc;
            (param[67] = new OracleParameter("p_is_grn", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_is_grn;
            (param[68] = new OracleParameter("p_d_cust_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_d_cust_name;//Edit by Chamal 27/05/2013
            (param[69] = new OracleParameter("p_is_dayend", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_is_dayend;//Edit by Prabhath on  20/06/2013
            param[70] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords(oConnEMS, "sp_save_sathdr", CommandType.StoredProcedure, param);
          
            return effects;


        }

        public Int32 SaveSalesItem(InvoiceItem _invoiceItem)
        {
            OracleParameter[] param = new OracleParameter[45];
            Int32 effects = 0;

            (param[0] = new OracleParameter("p_alt_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_alt_itm_cd;
            (param[1] = new OracleParameter("p_alt_itm_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_alt_itm_desc;
            (param[2] = new OracleParameter("p_comm_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_comm_amt;
            (param[3] = new OracleParameter("p_disc_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_disc_amt;
            (param[4] = new OracleParameter("p_disc_rt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_disc_rt;
            (param[5] = new OracleParameter("p_do_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_do_qty;
            (param[6] = new OracleParameter("p_fws_ignore_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_fws_ignore_qty;
            (param[7] = new OracleParameter("p_inv_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_inv_no;
            (param[8] = new OracleParameter("p_is_promo", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_is_promo;
            (param[9] = new OracleParameter("p_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_itm_cd;
            (param[10] = new OracleParameter("p_itm_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_itm_line;
            (param[11] = new OracleParameter("p_itm_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_itm_seq;
            (param[12] = new OracleParameter("p_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_itm_stus;
            (param[13] = new OracleParameter("p_itm_tax_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_itm_tax_amt;
            (param[14] = new OracleParameter("p_itm_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_itm_tp;
            (param[15] = new OracleParameter("p_job_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_job_line;
            (param[16] = new OracleParameter("p_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_job_no;
            (param[17] = new OracleParameter("p_merge_itm", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_merge_itm;
            (param[18] = new OracleParameter("p_outlet_dept", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_outlet_dept;
            (param[19] = new OracleParameter("p_pb_lvl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_pb_lvl;
            (param[20] = new OracleParameter("p_pb_price", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_pb_price;
            (param[21] = new OracleParameter("p_pbook", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_pbook;
            (param[22] = new OracleParameter("p_print_stus", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_print_stus;
            (param[23] = new OracleParameter("p_promo_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_promo_cd;
            (param[24] = new OracleParameter("p_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_qty;
            (param[25] = new OracleParameter("p_res_line_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_res_line_no;
            (param[26] = new OracleParameter("p_res_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_res_no;
            (param[27] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_seq;
            (param[28] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_seq_no;
            (param[29] = new OracleParameter("p_srn_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_srn_qty;
            (param[30] = new OracleParameter("p_tot_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_tot_amt;
            (param[31] = new OracleParameter("p_unit_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = (_invoiceItem.Sad_unit_rt * _invoiceItem.Sad_qty);
            (param[32] = new OracleParameter("p_unit_rt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_unit_rt;
            (param[33] = new OracleParameter("p_uom", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_uom;
            (param[34] = new OracleParameter("p_warr_based", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_warr_based;
            (param[35] = new OracleParameter("p_warr_period", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_warr_period;
            (param[36] = new OracleParameter("p_warr_remarks", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_warr_remarks;
            (param[37] = new OracleParameter("p_sad_isapp", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_isapp;
            (param[38] = new OracleParameter("p_sad_iscovernote", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_iscovernote;
            (param[39] = new OracleParameter("p_dis_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_dis_seq;//Added by Prabhath on 20/sp_save_sathdr06/2013
            (param[40] = new OracleParameter("p_dis_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_dis_line;//Added by Prabhath on 20/06/2013
            (param[41] = new OracleParameter("p_dis_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_dis_type;//Added by Prabhath on 20/06/2013
            (param[42] = new OracleParameter("P_SAD_CONF_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_conf_no;//Added by Tharaka 2015-10-16
            (param[43] = new OracleParameter("P_SAD_CONF_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_conf_line;//Added by Tharaka 2015-10-16
            param[44] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);


            effects = (Int16)UpdateRecords(oConnEMS, "sp_save_satitm", CommandType.StoredProcedure, param);
            return effects;

        }

        public Int32 SaveSalesItemTax(InvoiceItemTax _invoiceItemTax)
        {
            OracleParameter[] param = new OracleParameter[10];
            Int32 effects = 0;
            (param[0] = new OracleParameter("p_inv_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItemTax.Satx_inv_no;
            (param[1] = new OracleParameter("p_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItemTax.Satx_itm_cd;
            (param[2] = new OracleParameter("p_itm_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _invoiceItemTax.Satx_itm_line;
            (param[3] = new OracleParameter("p_itm_tax_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceItemTax.Satx_itm_tax_amt;
            (param[4] = new OracleParameter("p_itm_tax_rt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceItemTax.Satx_itm_tax_rt;
            (param[5] = new OracleParameter("p_itm_tax_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItemTax.Satx_itm_tax_tp;
            (param[6] = new OracleParameter("p_job_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _invoiceItemTax.Satx_job_line;
            (param[7] = new OracleParameter("p_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItemTax.Satx_job_no;
            (param[8] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _invoiceItemTax.Satx_seq_no;
            param[9] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int16)UpdateRecords(oConnEMS, "sp_save_satitmtax", CommandType.StoredProcedure, param);
            return effects;

        }
        public Int32 SaveReceiptHeader(RecieptHeader _recieptHeader)
        {
            OracleParameter[] param = new OracleParameter[54];
            Int32 effects = 0;

            (param[0] = new OracleParameter("p_acc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_acc_no;
            (param[1] = new OracleParameter("p_act", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_act;
            (param[2] = new OracleParameter("p_anal_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_anal_1;
            (param[3] = new OracleParameter("p_anal_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_anal_2;
            (param[4] = new OracleParameter("p_anal_3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_anal_3;
            (param[5] = new OracleParameter("p_anal_4", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_anal_4;
            (param[6] = new OracleParameter("p_anal_5", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_anal_5;
            (param[7] = new OracleParameter("p_anal_6", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_anal_6;
            (param[8] = new OracleParameter("p_anal_7", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_anal_7;
            (param[9] = new OracleParameter("p_anal_8", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_anal_8;
            (param[10] = new OracleParameter("p_anal_9", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_anal_9;
            (param[11] = new OracleParameter("p_com_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_com_cd;
            (param[12] = new OracleParameter("p_comm_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_comm_amt;
            (param[13] = new OracleParameter("p_create_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_create_by;
            (param[14] = new OracleParameter("p_create_when", OracleDbType.Date, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_create_when;
            (param[15] = new OracleParameter("p_currency_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_currency_cd;
            (param[16] = new OracleParameter("p_debtor_add_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_debtor_add_1;
            (param[17] = new OracleParameter("p_debtor_add_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_debtor_add_2;
            (param[18] = new OracleParameter("p_debtor_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_debtor_cd;
            (param[19] = new OracleParameter("p_debtor_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_debtor_name;
            (param[20] = new OracleParameter("p_direct", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_direct;
            (param[21] = new OracleParameter("p_direct_deposit_bank_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_direct_deposit_bank_cd;
            (param[22] = new OracleParameter("p_direct_deposit_branch", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_direct_deposit_branch;
            (param[23] = new OracleParameter("p_epf_rate", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_epf_rate;
            (param[24] = new OracleParameter("p_esd_rate", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_esd_rate;
            (param[25] = new OracleParameter("p_is_mgr_iss", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_is_mgr_iss;
            (param[26] = new OracleParameter("p_is_oth_shop", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_is_oth_shop;
            (param[27] = new OracleParameter("p_is_used", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_is_used;
            (param[28] = new OracleParameter("p_manual_ref_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_manual_ref_no;
            (param[29] = new OracleParameter("p_mob_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_mob_no;
            (param[30] = new OracleParameter("p_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_mod_by;
            (param[31] = new OracleParameter("p_mod_when", OracleDbType.Date, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_mod_when;
            (param[32] = new OracleParameter("p_nic_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_nic_no;
            (param[33] = new OracleParameter("p_oth_sr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_oth_sr;
            (param[34] = new OracleParameter("p_prefix", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_prefix;
            (param[35] = new OracleParameter("p_profit_center_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_profit_center_cd;
            (param[36] = new OracleParameter("p_receipt_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_receipt_date;
            (param[37] = new OracleParameter("p_receipt_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_receipt_no;
            (param[38] = new OracleParameter("p_receipt_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_receipt_type;
            (param[39] = new OracleParameter("p_ref_doc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_ref_doc;
            (param[40] = new OracleParameter("p_remarks", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_remarks;
            (param[41] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_seq_no;
            (param[42] = new OracleParameter("p_ser_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_ser_job_no;
            (param[43] = new OracleParameter("p_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_session_id;
            (param[44] = new OracleParameter("p_tel_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_tel_no;
            (param[45] = new OracleParameter("p_tot_settle_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_tot_settle_amt;
            (param[46] = new OracleParameter("p_uploaded_to_finance", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_uploaded_to_finance;
            (param[47] = new OracleParameter("p_used_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_used_amt;
            (param[48] = new OracleParameter("p_wht_rate", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_wht_rate;
            (param[49] = new OracleParameter("p_is_dayend", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_is_dayend;//Added by Prabhath on 20/06/2013
            (param[50] = new OracleParameter("p_sar_valid_to", OracleDbType.Date, null, ParameterDirection.Input)).Value = _recieptHeader.SAR_VALID_TO;
            (param[51] = new OracleParameter("p_sar_inv_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_inv_type;
            (param[52] = new OracleParameter("p_sar_scheme", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_scheme;
            param[53] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);


            effects = (Int16)UpdateRecords(oConnEMS, "sp_save_satreceipt", CommandType.StoredProcedure, param);
            return effects;

        }
        public Int32 SaveReceiptItem(RecieptItem _recieptItem)
        {
            OracleParameter[] param = new OracleParameter[25];
            Int32 effects = 0;

            (param[0] = new OracleParameter("p_anal_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_anal_1;
            (param[1] = new OracleParameter("p_anal_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_anal_2;
            (param[2] = new OracleParameter("p_anal_3", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptItem.Sard_anal_3;
            (param[3] = new OracleParameter("p_anal_4", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptItem.Sard_anal_4;
            (param[4] = new OracleParameter("p_anal_5", OracleDbType.Date, null, ParameterDirection.Input)).Value = _recieptItem.Sard_anal_5;
            (param[5] = new OracleParameter("p_cc_expiry_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _recieptItem.Sard_cc_expiry_dt;
            (param[6] = new OracleParameter("p_cc_is_promo", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _recieptItem.Sard_cc_is_promo;
            (param[7] = new OracleParameter("p_cc_period", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _recieptItem.Sard_cc_period;
            (param[8] = new OracleParameter("p_cc_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_cc_tp;
            (param[9] = new OracleParameter("p_chq_bank_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_chq_bank_cd;
            (param[10] = new OracleParameter("p_chq_branch", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_chq_branch;
            (param[11] = new OracleParameter("p_credit_card_bank", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_credit_card_bank;
            (param[12] = new OracleParameter("p_deposit_bank_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_deposit_bank_cd;
            (param[13] = new OracleParameter("p_deposit_branch", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_deposit_branch;
            (param[14] = new OracleParameter("p_gv_issue_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _recieptItem.Sard_gv_issue_dt;
            (param[15] = new OracleParameter("p_gv_issue_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_gv_issue_loc;
            (param[16] = new OracleParameter("p_inv_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_inv_no;
            (param[17] = new OracleParameter("p_line_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _recieptItem.Sard_line_no;
            (param[18] = new OracleParameter("p_pay_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_pay_tp;
            (param[19] = new OracleParameter("p_receipt_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_receipt_no;
            (param[20] = new OracleParameter("p_ref_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_ref_no;
            (param[21] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _recieptItem.Sard_seq_no;
            (param[22] = new OracleParameter("p_settle_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptItem.Sard_settle_amt;
            (param[23] = new OracleParameter("p_sim_ser", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_sim_ser;
            param[24] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int16)UpdateRecords(oConnEMS, "sp_save_satreceiptitm", CommandType.StoredProcedure, param);
          
            return effects;
        }

        public void InterTrnsfReq(string _fromCom, string _toCom, Int16 _db,string Team)
        {
            try
            {
                Console.WriteLine("");

                HMCBegin(_db);

                List<INT_REQ> oHeader = new List<INT_REQ>();
                List<INT_REQ_ITM> oDetails = new List<INT_REQ_ITM>();

                #region Get Header
                oDA = new OracleDataAdapter();
                DataSet dsTemp = new DataSet();

                string _pb = string.Empty;
                string _pbLvl = string.Empty;
                string _pc = string.Empty;
                string _cust = string.Empty;
                string _docType  = string.Empty;
                string _customer = string.Empty;
                string _invType = string.Empty;

                List<InterCompanySalesParameter> _priceParam = GetInterCompanyParameter(Team, _toCom, string.Empty, _fromCom, string.Empty);

                if (_priceParam != null && _priceParam.Count > 0)
                {
                    _pc = _priceParam[0].Sritc_frm_prof;
                    _cust = _priceParam[0].Sritc_customer;
                    _pb = _priceParam[0].Sritc_pb;
                    _pbLvl = _priceParam[0].Sritc_pb_lvl;
                    _customer = _priceParam[0].Sritc_customer;
                    _invType = _priceParam[0].SRITC_INV_TYPE;
                }


                sqlQry = @"SELECT * FROM INT_REQ WHERE ITR_COM ='" + _fromCom + "' AND ITR_TP ='INTR' AND ITR_SUB_TP ='DISP' AND ITR_STUS ='A' AND ITR_DT >='08/MAR/2015'";

                _oCom = new OracleCommand(sqlQry, oConnHMC);
                _oCom.Transaction = oTrHMC;
                oDA.SelectCommand = _oCom;
                oDA.Fill(dsTemp, "INT_REQ");

                if (dsTemp.Tables[0].Rows.Count > 0)
                {
                    oHeader = DataTableExtensionsTolist.ToGenericList<INT_REQ>(dsTemp.Tables[0], INT_REQ.Converter);
                }

                #endregion

                Console.WriteLine("Request Records Count :- " + oHeader.Count.ToString());

                List<Int32> _JonNumList = oHeader.Distinct().Select(x => x.Itr_seq_no).ToList();

                #region Get Items

                foreach (Int32 seqNumber in _JonNumList)
                {
                    oDA = new OracleDataAdapter();
                    DataSet dsItemDetails = new DataSet();
                    sqlQry = @"SELECT A.* , B.MI_LONGDESC FROM INT_REQ_ITM A , MST_ITM B WHERE A.itri_itm_cd = B.MI_CD AND ITRI_SEQ_NO = :P_SeqNumber";
                    _oCom = new OracleCommand(sqlQry, oConnHMC);
                    _oCom.Parameters.Add(":P_SeqNumber", OracleDbType.Int32).Value = seqNumber;
                    _oCom.Transaction = oTrHMC;
                    oDA.SelectCommand = _oCom;
                    oDA.Fill(dsItemDetails, "INT_REQ_ITM");

                    List<INT_REQ_ITM> oItem = new List<INT_REQ_ITM>();

                    if (dsItemDetails.Tables[0].Rows.Count > 0)
                    {
                        oItem = DataTableExtensionsTolist.ToGenericList<INT_REQ_ITM>(dsItemDetails.Tables[0], INT_REQ_ITM.Converter);
                        oDetails.AddRange(oItem);
                    }
                }

                Console.WriteLine("Request items Count :- " + oDetails.Count.ToString());

                #endregion

                ScmBegin();

                #region Save WHF_ORDER_REQUEST_HEADER and WHF_ORDER_REQUEST_DETAILS

                int resultHeader = 0;

                foreach (INT_REQ item in oHeader)
                {
                    Boolean _priceAvl = false;
                    Boolean _TaxAvl = false;




                    List<INT_REQ_ITM> oSelectedItms = new List<INT_REQ_ITM>();
                    oSelectedItms = oDetails.FindAll(x => x.Itri_seq_no == item.Itr_seq_no);
                    if (oSelectedItms.Count > 0)
                    {

                        foreach (INT_REQ_ITM IntReqItm in oSelectedItms)
                        {
                            decimal _Price = 0;
                            decimal _taxRate = 0;
                            string _itemstatus = string.Empty;
                            _itemstatus = get_pb_Status(_pbLvl);
                           // _itemstatus = get_Status(IntReqItm.Itri_itm_stus);
                            _priceAvl = get_Price(_fromCom, _pb, _pbLvl, IntReqItm.Itri_itm_cd, IntReqItm.Itri_qty, DateTime.Today, out _Price);
                            _TaxAvl = get_ItemTax(_fromCom, IntReqItm.Itri_itm_cd, _itemstatus, out _taxRate);
                            if (_priceAvl == false || _TaxAvl == false)
                            {
                                break;
                            }

                        }
                    }

                    _cust = get_customer(_fromCom, item.Itr_loc);
                if (!string.IsNullOrEmpty(_cust))
                {
                    if (_priceAvl == true && _TaxAvl == true)
                    {
                        _pc = _priceParam[0].Sritc_frm_prof;

                        resultHeader += Save_So_Header_Inter(item, oDetails, _pb, _pbLvl, _pc, _invType, _toCom, _cust);
                    }
                    else
                    {
                        resultHeader += Save_So_Request_InterTranfer(item, oDetails, _toCom, _pb, _pbLvl, _pc, _invType, _cust);
                    }
                }
                }
             
                if (resultHeader > 0)
                {
                    Console.WriteLine("Inter transfer request upload successful. No of requests :-" + resultHeader.ToString());
                }
                
                #endregion

                ScmCommit();
                HMCCommit();
            }
            catch (Exception ex)
            {
                HMCRollback();
                ScmRollback();

                PuchaseOrderGeneration oPogeneration = new PuchaseOrderGeneration();
                oPogeneration.SendSMS("0773973588", "0773973588", "HMC/BDL Agent Issue" + ex.Message);
                oPogeneration.Send_SMTPMail("chamald@abansgroup.com", "chamald@abansgroup.com", "chamald@abansgroup.com", "chamald@abansgroup.com", "chamald@abansgroup.com", "HMC/BDL Agent Issue", "Inter Transfer" + ex.Message);
                Console.WriteLine("");
                Console.WriteLine("***********************ERROR :-" + ex.Message);
            }
        }
        public int Save_So_Header_Inter(INT_REQ item, List<INT_REQ_ITM> oDetails, string _pb, string _pblevel,string _pc,string _docType,string _com,string _customer )
        {
            whf_so_header oNewHeader = new whf_so_header();
            oNewHeader.SO_LINE = item.Itr_seq_no;
            oNewHeader.SO_NO = item.Itr_req_no;
            oNewHeader.COMPANY_CODE =_com ;
            oNewHeader.SO_DATE = item.Itr_dt;
            oNewHeader.PROFIT_CENTER_CODE = _pc;
            oNewHeader.SO_TYPE = _docType;
            oNewHeader.SALE_EX_CODE = "N/A";
            oNewHeader.CUSTOMER_CODE =_customer;
            //   oNewHeader.Wor_exp_date = "01-JAN-9999";
            if (!string.IsNullOrEmpty(item.Itr_note))
            { oNewHeader.REMARKS = item.Itr_note; }
            else
            {
                oNewHeader.REMARKS = "N/A";
            }
            oNewHeader.TOTAL_SO_AMOUNT = 0;
            oNewHeader.INVOICE_STATUS = "PENDING";
            oNewHeader.CREATE_BY = item.Itr_cre_by;
            oNewHeader.CREATE_WHEN = item.Itr_cre_dt;
            oNewHeader.LAST_MODIFY_BY = item.Itr_mod_by;
            oNewHeader.LAST_MODIFY_WHEN = item.Itr_mod_dt;
            oNewHeader.SO_REF_NO = "HMC";//item.Itr_ref;
            oNewHeader.CURRENCY_CODE = "LKR";
            oNewHeader.EXCHANGE_RATE = 0;
            oNewHeader.SO_AMOUNT = 0;
            oNewHeader.SO_TAX_AMOUNT = 0;
            oNewHeader.SO_COMPUTER_IP = "N/A";
            oNewHeader.D_ADD1 = "N/A";
            oNewHeader.D_ADD2 = "N/A";
            oNewHeader.TOT_DISCOUNT = 0;
            oNewHeader.DIS_RATE = 0;
            oNewHeader.DIS_AMOUNT = 0;
            oNewHeader.TRANSPORT = 0;
            oNewHeader.OTHER = 0;
            oNewHeader.DOC_TAX_AMOUNT = 0;

            sqlQry = @"INSERT INTO whf_so_header
                            (
                            SO_LINE,
                            SO_NO            ,   
                            COMPANY_CODE        ,   
                            SO_DATE           ,   
                            PROFIT_CENTER_CODE  ,   
                            SO_TYPE       ,   
                            SALE_EX_CODE      ,   
                            CUSTOMER_CODE       ,   
                            REMARKS        ,   
                            TOTAL_SO_AMOUNT        ,   
                            INVOICE_STATUS         ,   
                            CREATE_BY       ,   
                            CREATE_WHEN       ,   
                            LAST_MODIFY_BY     ,   
                            LAST_MODIFY_WHEN   ,   
                            SO_REF_NO ,   
                            CURRENCY_CODE    ,
                            EXCHANGE_RATE ,
                            SO_AMOUNT,
                            SO_TAX_AMOUNT    ,
                            SO_COMPUTER_IP   ,
                            D_ADD1,
                            D_ADD2,
                            TOT_DISCOUNT,
                            DIS_RATE,
                            DIS_AMOUNT ,
 TRANSPORT,
OTHER,
DOC_TAX_AMOUNT
                            )
                            VALUES
                            (
                            :P_SO_LINE,
                            :P_SO_NO            ,   
                            :P_COMPANY_CODE        ,   
                            :P_SO_DATE           ,   
                            :P_PROFIT_CENTER_CODE  ,   
                            :P_SO_TYPE       ,   
                            :P_SALE_EX_CODE      ,   
                            :P_CUSTOMER_CODE       ,   
                            :P_REMARKS        ,   
                            :P_TOTAL_SO_AMOUNT        ,   
                            :P_INVOICE_STATUS         ,   
                            :P_CREATE_BY       ,   
                            :P_CREATE_WHEN       ,   
                            :P_LAST_MODIFY_BY     ,   
                            :P_LAST_MODIFY_WHEN   ,   
                            :P_SO_REF_NO ,   
                            :P_CURRENCY_CODE   ,
                            :P_EXCHANGE_RATE  ,
                            :P_SO_AMOUNT,
                            :P_SO_TAX_AMOUNT,
                            :P_SO_COMPUTER_IP,
                                :P_D_ADD1,
                                :P_D_ADD2,
                                :P_TOT_DISCOUNT,
                                :P_DIS_RATE,
                                :P_DIS_AMOUNT,
:P_TRANSPORT,
:P_OTHER,
:P_DOC_TAX_AMOUNT
                            )";

            _oCom = new OracleCommand(sqlQry, oConnSCM);
            _oCom.Parameters.Add(":P_SO_LINE", OracleDbType.Int32).Value = oNewHeader.SO_LINE;
            _oCom.Parameters.Add(":P_SO_NO", OracleDbType.NVarchar2).Value = oNewHeader.SO_NO;
            _oCom.Parameters.Add(":P_COMPANY_CODE", OracleDbType.NVarchar2).Value = oNewHeader.COMPANY_CODE;
            _oCom.Parameters.Add(":P_SO_DATE", OracleDbType.Date).Value = oNewHeader.SO_DATE;
            _oCom.Parameters.Add(":P_PROFIT_CENTER_CODE", OracleDbType.NVarchar2).Value = oNewHeader.PROFIT_CENTER_CODE;
            _oCom.Parameters.Add(":P_SO_TYPE", OracleDbType.NVarchar2).Value = oNewHeader.SO_TYPE;
            _oCom.Parameters.Add(":P_SALE_EX_CODE", OracleDbType.NVarchar2).Value = oNewHeader.SALE_EX_CODE;
            _oCom.Parameters.Add(":P_CUSTOMER_CODE", OracleDbType.NVarchar2).Value = oNewHeader.CUSTOMER_CODE;

            _oCom.Parameters.Add(":P_REMARKS", OracleDbType.NVarchar2).Value = oNewHeader.REMARKS;
            _oCom.Parameters.Add(":P_TOTAL_SO_AMOUNT", OracleDbType.Decimal).Value = oNewHeader.TOTAL_SO_AMOUNT;
            _oCom.Parameters.Add(":P_INVOICE_STATUS", OracleDbType.NVarchar2).Value = oNewHeader.INVOICE_STATUS;
            _oCom.Parameters.Add(":P_CREATE_BY", OracleDbType.NVarchar2).Value = oNewHeader.CREATE_BY;
            _oCom.Parameters.Add(":P_CREATE_WHEN", OracleDbType.Date).Value = oNewHeader.CREATE_WHEN;
            _oCom.Parameters.Add(":P_LAST_MODIFY_BY", OracleDbType.NVarchar2).Value = oNewHeader.LAST_MODIFY_BY;
            _oCom.Parameters.Add(":P_LAST_MODIFY_WHEN", OracleDbType.Date).Value = oNewHeader.LAST_MODIFY_WHEN;
            _oCom.Parameters.Add(":P_SO_REF_NO", OracleDbType.NVarchar2).Value = oNewHeader.SO_REF_NO;
            _oCom.Parameters.Add(":P_CURRENCY_CODE", OracleDbType.NVarchar2).Value = oNewHeader.CURRENCY_CODE;
            _oCom.Parameters.Add(":P_EXCHANGE_RATE", OracleDbType.Decimal).Value = oNewHeader.EXCHANGE_RATE;
            _oCom.Parameters.Add(":P_SO_AMOUNT", OracleDbType.Decimal).Value = oNewHeader.SO_AMOUNT;
            _oCom.Parameters.Add(":P_SO_TAX_AMOUNT", OracleDbType.Decimal).Value = oNewHeader.SO_TAX_AMOUNT;
            _oCom.Parameters.Add(":P_SO_COMPUTER_IP", OracleDbType.NVarchar2).Value = oNewHeader.SO_COMPUTER_IP;
            _oCom.Parameters.Add(":P_D_ADD1", OracleDbType.NVarchar2).Value = oNewHeader.D_ADD1;
            _oCom.Parameters.Add(":P_D_ADD2", OracleDbType.NVarchar2).Value = oNewHeader.D_ADD2;
            _oCom.Parameters.Add(":P_TOT_DISCOUNT", OracleDbType.Int32).Value = oNewHeader.TOT_DISCOUNT;
            _oCom.Parameters.Add(":P_DIS_RATE", OracleDbType.Int32).Value = oNewHeader.DIS_RATE;
            _oCom.Parameters.Add(":P_DIS_AMOUNT", OracleDbType.Int32).Value = oNewHeader.DIS_AMOUNT;
            _oCom.Parameters.Add(":P_TRANSPORT", OracleDbType.Int32).Value = oNewHeader.TRANSPORT;
            _oCom.Parameters.Add(":P_OTHER", OracleDbType.Int32).Value = oNewHeader.OTHER;
            _oCom.Parameters.Add(":P_DOC_TAX_AMOUNT", OracleDbType.Int32).Value = oNewHeader.DOC_TAX_AMOUNT;
            _oCom.Transaction = oTrSCM;
            int result = _oCom.ExecuteNonQuery();
            //   resultHeader += result;

            Console.WriteLine("New Sales Order inserted. So # :-" + oNewHeader.SO_NO);
            decimal _soTot = 0;
            decimal _soTotTax = 0;
            if (result > 0)
            {
                int ItemResult = 0;
         
                List<INT_REQ_ITM> oSelectedItms = new List<INT_REQ_ITM>();
                oSelectedItms = oDetails.FindAll(x => x.Itri_seq_no == item.Itr_seq_no);
                if (oSelectedItms.Count > 0)
                {

                    foreach (INT_REQ_ITM IntReqItm in oSelectedItms)
                    {
                        whf_so_details oDetailItem = new whf_so_details();
                        decimal _Price = 0;
                        decimal _taxRate = 0;
                        string _itemstatus = string.Empty;
                        _itemstatus = get_pb_Status(_pblevel);
                     //   _itemstatus = get_Status(IntReqItm.Itri_itm_stus);
                        get_Price(oNewHeader.COMPANY_CODE, _pb, _pblevel, IntReqItm.Itri_itm_cd, IntReqItm.Itri_qty, DateTime.Today,out _Price);
                        get_ItemTax(oNewHeader.COMPANY_CODE, IntReqItm.Itri_itm_cd, _itemstatus,out _taxRate);

                        oDetailItem.SO_NO = oNewHeader.SO_NO;
                        oDetailItem.SO_LINE = oNewHeader.SO_LINE;
                        oDetailItem.ITEM_LINE_NO = IntReqItm.Itri_line_no;
                        oDetailItem.ITEM_CODE = IntReqItm.Itri_itm_cd;
                        oDetailItem.DESCRIPTION = IntReqItm.MI_LONGDESC;
                        oDetailItem.QTY = IntReqItm.Itri_qty;
                        oDetailItem.UNIT_RATE = _Price;
                        oDetailItem.AMOUNT = _Price * IntReqItm.Itri_qty;
                        oDetailItem.SO_BALANCE = IntReqItm.Itri_qty;
                        if (_taxRate > 0) { oDetailItem.ITEM_TAX_AMOUNT = oDetailItem.AMOUNT * _taxRate / 100; }
                        else { oDetailItem.ITEM_TAX_AMOUNT = 0; }
                        oDetailItem.TOT_AMOUNT = oDetailItem.AMOUNT + oDetailItem.ITEM_TAX_AMOUNT;
                        oDetailItem.PRICE_BOOK = _pb;
                        oDetailItem.PRICE_LEVEL = _pblevel;
                        oDetailItem.STATUS = _itemstatus;
                        _soTot += oDetailItem.AMOUNT;
                        _soTotTax += oDetailItem.ITEM_TAX_AMOUNT;
                        oDetailItem.MODEL = "N/A";
                        oDetailItem.DIS_AMOUNT = 0;
                        oDetailItem.DIS_RATE = 0;
                        oDetailItem.UOM = "NOS";
                        sqlQry = @"INSERT INTO whf_so_details (SO_NO,
                                       SO_LINE,
                                       ITEM_LINE_NO,
                                       ITEM_CODE,
                                        DESCRIPTION,
                                       QTY,
                                       UNIT_RATE,
                                       AMOUNT,
                                       SO_BALANCE,
                                      ITEM_TAX_AMOUNT,
                                      TOT_AMOUNT,
                                      PRICE_BOOK,
                                      PRICE_LEVEL,
                                      STATUS,
                                        MODEL,
                                        DIS_AMOUNT,
                                        DIS_RATE,
                                        UOM
                                        )
                                      VALUES   (:P_SO_NO,
                                       :P_SO_LINE,
                                       :P_ITEM_LINE_NO,
                                       :P_ITEM_CODE,
                                       :P_DESCRIPTION,
                                       :P_QTY,
                                       :P_UNIT_RATE,
                                       :P_AMOUNT,
                                       :P_SO_BALANCE,
                                      :P_ITEM_TAX_AMOUNT,
                                      :P_TOT_AMOUNT,
                                      :P_PRICE_BOOK,
                                      :P_PRICE_LEVEL,
                                      :P_STATUS,
                                      :P_MODEL,
                                      :P_DIS_AMOUNT,
                                      :P_DIS_RATE,
                                      :P_UOM)";

                        _oCom = new OracleCommand(sqlQry, oConnSCM);
                        _soTot += oDetailItem.TOT_AMOUNT;
                        _oCom.Parameters.Add(":P_SO_NO", OracleDbType.NVarchar2).Value = oDetailItem.SO_NO;
                        _oCom.Parameters.Add(":P_SO_LINE", OracleDbType.Int32).Value = oDetailItem.SO_LINE;
                        _oCom.Parameters.Add(":P_ITEM_LINE_NO", OracleDbType.Int32).Value = oDetailItem.ITEM_LINE_NO;
                        _oCom.Parameters.Add(":P_ITEM_CODE", OracleDbType.NVarchar2).Value = oDetailItem.ITEM_CODE;
                        _oCom.Parameters.Add(":P_DESCRIPTION", OracleDbType.NVarchar2).Value = oDetailItem.DESCRIPTION;
                        _oCom.Parameters.Add(":P_QTY", OracleDbType.Decimal).Value = oDetailItem.QTY;

                        _oCom.Parameters.Add(":P_UNIT_RATE", OracleDbType.Decimal).Value = oDetailItem.UNIT_RATE;
                        _oCom.Parameters.Add(":P_AMOUNT", OracleDbType.Decimal).Value = oDetailItem.AMOUNT;
                        _oCom.Parameters.Add(":P_SO_BALANCE", OracleDbType.Decimal).Value = oDetailItem.SO_BALANCE;
                        _oCom.Parameters.Add(":P_ITEM_TAX_AMOUNT", OracleDbType.Decimal).Value = oDetailItem.ITEM_TAX_AMOUNT;
                        _oCom.Parameters.Add(":P_TOT_AMOUNT", OracleDbType.Decimal).Value = oDetailItem.TOT_AMOUNT;
                        _oCom.Parameters.Add(":P_PRICE_BOOK", OracleDbType.NVarchar2).Value = oDetailItem.PRICE_BOOK;
                        _oCom.Parameters.Add(":P_PRICE_LEVEL", OracleDbType.NVarchar2).Value = oDetailItem.PRICE_LEVEL;
                        _oCom.Parameters.Add(":P_STATUS", OracleDbType.NVarchar2).Value = oDetailItem.STATUS;
                        _oCom.Parameters.Add(":P_MODEL", OracleDbType.NVarchar2).Value = oDetailItem.MODEL;
                        _oCom.Parameters.Add(":P_DIS_AMOUNT", OracleDbType.Decimal).Value = oDetailItem.DIS_AMOUNT;
                        _oCom.Parameters.Add(":P_DIS_RATE", OracleDbType.Decimal).Value = oDetailItem.DIS_RATE;
                        _oCom.Parameters.Add(":P_UOM", OracleDbType.NVarchar2).Value = oDetailItem.UOM;
                        _oCom.Transaction = oTrSCM;
                        ItemResult += _oCom.ExecuteNonQuery();


                        sqlQry = @"INSERT INTO WHF_SO_ITEM_TAX (SO_NO,
                                                            SO_ITEM_CODE,
                                                            SO_ITEM_TAX_TYPE,
                                                            SO_ITEM_TAX_RATE,
                                                            SO_ITEM_TAX_AMOUNT)
                                                    VALUES   (:P_SO_NO,
                                                     :P_SO_ITEM_CODE,
                                                     :SO_ITEM_TAX_TYPE,
                                                     :SO_ITEM_TAX_RATE,
                                                     :SO_ITEM_TAX_AMOUNT)";
                        _oCom = new OracleCommand(sqlQry, oConnSCM);
                        _oCom.Parameters.Add(":P_SO_NO", OracleDbType.NVarchar2).Value = oDetailItem.SO_NO;
                        _oCom.Parameters.Add(":P_SO_ITEM_CODE", OracleDbType.NVarchar2).Value = oDetailItem.ITEM_CODE;
                        _oCom.Parameters.Add(":SO_ITEM_TAX_TYPE", OracleDbType.NVarchar2).Value = "VAT";
                        _oCom.Parameters.Add(":SO_ITEM_TAX_RATE", OracleDbType.Decimal).Value = _taxRate;
                        _oCom.Parameters.Add(":SO_ITEM_TAX_AMOUNT", OracleDbType.Decimal).Value = oDetailItem.ITEM_TAX_AMOUNT;
                        _oCom.Transaction = oTrSCM;
                        ItemResult += _oCom.ExecuteNonQuery();

                    }

                    Console.WriteLine("New request items inserted. Item count :-" + ItemResult.ToString());
                }
            }
            if (result > 0)
            {
                sqlQry = @"UPDATE INT_REQ SET ITR_STUS = 'F' WHERE itr_seq_no = :P_SEQ  ";
                _oCom = new OracleCommand(sqlQry, oConnHMC);
                _oCom.Parameters.Add(":P_SEQ", OracleDbType.Int32).Value = item.Itr_seq_no;
                _oCom.Transaction = oTrHMC;
                int resultFinal = _oCom.ExecuteNonQuery();

                sqlQry = @"UPDATE whf_so_header SET TOTAL_SO_AMOUNT = :P_TOT ,SO_AMOUNT=:P_SO_AMOUNT , SO_TAX_AMOUNT=:P_SO_TAX_AMOUNT WHERE SO_LINE = :P_SEQ  ";
                _oCom = new OracleCommand(sqlQry, oConnSCM);
                _oCom.Parameters.Add(":P_TOT", OracleDbType.Decimal).Value = _soTot + _soTotTax;
                _oCom.Parameters.Add(":P_SO_AMOUNT", OracleDbType.Decimal).Value = _soTot;
                _oCom.Parameters.Add(":P_SO_TAX_AMOUNT", OracleDbType.Decimal).Value = _soTotTax;
                _oCom.Parameters.Add(":P_SEQ", OracleDbType.Int32).Value = item.Itr_seq_no;
                _oCom.Transaction = oTrSCM;
                resultFinal = _oCom.ExecuteNonQuery();

            } return result;
        }
        public int Save_So_Request_InterTranfer(INT_REQ item, List<INT_REQ_ITM> oDetails, string _com, string _pb, string _pblevel, string _pc, string _docType,   string _customer)
        {
            int resultHeader = 0;
    Whf_order_request_header oNewHeader = new Whf_order_request_header();
                    oNewHeader.Wor_ref = item.Itr_req_no;
                    oNewHeader.Wor_company = _com;
                    oNewHeader.Wor_date = item.Itr_dt;
                    oNewHeader.Wor_profit_center = _pc;
                    oNewHeader.Wor_req_type = _docType;
                    oNewHeader.Wor_executive = item.Itr_cre_by;
                    oNewHeader.Wor_custcode = _customer;
                    oNewHeader.Wor_exp_date = item.Itr_exp_dt;
                    if (!string.IsNullOrEmpty(item.Itr_note))
                    { oNewHeader.Wor_remarks = item.Itr_note; }
                    else
                    {
                        oNewHeader.Wor_remarks = "N/A";
                    }
                    oNewHeader.Wor_tot_amt = 0;
                    oNewHeader.Wor_active = "E";
                    oNewHeader.Wor_createby = item.Itr_cre_by;
                    oNewHeader.Wor_createwhen = item.Itr_cre_dt;
                    oNewHeader.Wor_lastmodifyby = item.Itr_mod_by;
                    oNewHeader.Wor_lastmodifywhen = item.Itr_mod_dt;
                    oNewHeader.Wor_ref_no =  "HMC";//item.Itr_ref;
                    oNewHeader.Wor_cus_app = "P";
                    oNewHeader.Wor_location = item.Itr_issue_from;

                    sqlQry = @"INSERT INTO WHF_ORDER_REQUEST_HEADER
                            (
                            wor_ref            ,   
                            wor_company        ,   
                            wor_date           ,   
                            wor_profit_center  ,   
                            wor_req_type       ,   
                            wor_executive      ,   
                            wor_custcode       ,   
                            wor_exp_date       ,   
                            wor_remarks        ,   
                            wor_tot_amt        ,   
                            wor_active         ,   
                            wor_createby       ,   
                            wor_createwhen     ,   
                            wor_lastmodifyby   ,   
                            wor_lastmodifywhen ,   
                            wor_ref_no         ,   
                            wor_cus_app        ,
                            wor_location
                            )
                            VALUES
                            (
                            :P_wor_ref            , 
                            :P_wor_company        , 
                            :P_wor_date           , 
                            :P_wor_profit_center  , 
                            :P_wor_req_type       , 
                            :P_wor_executive      , 
                            :P_wor_custcode       , 
                            :P_wor_exp_date       , 
                            :P_wor_remarks        , 
                            :P_wor_tot_amt        , 
                            :P_wor_active         , 
                            :P_wor_createby       , 
                            :P_wor_createwhen     , 
                            :P_wor_lastmodifyby   , 
                            :P_wor_lastmodifywhen , 
                            :P_wor_ref_no         , 
                            :P_wor_cus_app        ,
                            :P_wor_location
                            )";

                    _oCom = new OracleCommand(sqlQry, oConnSCM);
                    _oCom.Parameters.Add(":P_wor_ref", OracleDbType.NVarchar2).Value = oNewHeader.Wor_ref;
                    _oCom.Parameters.Add(":P_wor_company", OracleDbType.NVarchar2).Value = oNewHeader.Wor_company;
                    _oCom.Parameters.Add(":P_wor_date", OracleDbType.Date).Value = oNewHeader.Wor_date;
                    _oCom.Parameters.Add(":P_wor_profit_center", OracleDbType.NVarchar2).Value = oNewHeader.Wor_profit_center;
                    _oCom.Parameters.Add(":P_wor_req_type", OracleDbType.NVarchar2).Value = oNewHeader.Wor_req_type;
                    _oCom.Parameters.Add(":P_wor_executive", OracleDbType.NVarchar2).Value = oNewHeader.Wor_executive;
                    _oCom.Parameters.Add(":P_wor_custcode", OracleDbType.NVarchar2).Value = oNewHeader.Wor_custcode;
                    _oCom.Parameters.Add(":P_wor_exp_date", OracleDbType.Date).Value = oNewHeader.Wor_exp_date;
                    _oCom.Parameters.Add(":P_wor_remarks", OracleDbType.NVarchar2).Value = oNewHeader.Wor_remarks;
                    _oCom.Parameters.Add(":P_wor_tot_amt", OracleDbType.Decimal).Value = oNewHeader.Wor_tot_amt;
                    _oCom.Parameters.Add(":P_wor_active", OracleDbType.NVarchar2).Value = oNewHeader.Wor_active;
                    _oCom.Parameters.Add(":P_wor_createby", OracleDbType.NVarchar2).Value = oNewHeader.Wor_createby;
                    _oCom.Parameters.Add(":P_wor_createwhen", OracleDbType.Date).Value = oNewHeader.Wor_createwhen;
                    _oCom.Parameters.Add(":P_wor_lastmodifyby", OracleDbType.NVarchar2).Value = oNewHeader.Wor_lastmodifyby;
                    _oCom.Parameters.Add(":P_wor_lastmodifywhen", OracleDbType.Date).Value = oNewHeader.Wor_lastmodifywhen;
                    _oCom.Parameters.Add(":P_wor_ref_no", OracleDbType.NVarchar2).Value = oNewHeader.Wor_ref_no;
                    _oCom.Parameters.Add(":P_wor_cus_app", OracleDbType.NVarchar2).Value = oNewHeader.Wor_cus_app;
                    _oCom.Parameters.Add(":P_wor_location", OracleDbType.NVarchar2).Value = oNewHeader.Wor_location;
                    _oCom.Transaction = oTrSCM;
                    int result = _oCom.ExecuteNonQuery();
                    resultHeader += result;

                    Console.WriteLine("New Request inserted. Req # :-" + oNewHeader.Wor_ref);

                    if (result > 0)
                    {
                        int ItemResult = 0;
                        List<INT_REQ_ITM> oSelectedItms = new List<INT_REQ_ITM>();
                        oSelectedItms = oDetails.FindAll(x => x.Itri_seq_no == item.Itr_seq_no);
                        if (oSelectedItms.Count > 0)
                        {

                            foreach (INT_REQ_ITM IntReqItm in oSelectedItms)
                            {
                                Whf_order_request_details oDetailItem = new Whf_order_request_details();
                                oDetailItem.Word_ref = oNewHeader.Wor_ref;
                                oDetailItem.Word_line = IntReqItm.Itri_line_no;
                                oDetailItem.Word_item_code = IntReqItm.Itri_itm_cd;
                                oDetailItem.Word_item_des = IntReqItm.MI_LONGDESC;
                                oDetailItem.Word_qty = IntReqItm.Itri_qty;
                                oDetailItem.Word_unit_price = 0;
                                oDetailItem.Word_value = 0;
                                oDetailItem.Word_bal_qty = IntReqItm.Itri_qty;

                                sqlQry = @"INSERT INTO WHF_ORDER_REQUEST_DETAILS (WORD_REF,
                                       WORD_LINE,
                                       WORD_ITEM_CODE,
                                       WORD_ITEM_DES,
                                       WORD_QTY,
                                       WORD_UNIT_PRICE,
                                       WORD_VALUE,
                                       WORD_BAL_QTY)
                                      VALUES   (:P_WORD_REF,
                                                :P_WORD_LINE,
                                                :P_WORD_ITEM_CODE,
                                                :P_WORD_ITEM_DES,
                                                :P_WORD_QTY,
                                                :P_WORD_UNIT_PRICE,
                                                :P_WORD_VALUE,
                                                :P_WORD_BAL_QTY)";

                                _oCom = new OracleCommand(sqlQry, oConnSCM);
                                _oCom.Parameters.Add(":P_WORD_REF", OracleDbType.NVarchar2).Value = oDetailItem.Word_ref;
                                _oCom.Parameters.Add(":P_WORD_LINE", OracleDbType.Int32).Value = oDetailItem.Word_line;
                                _oCom.Parameters.Add(":P_WORD_ITEM_CODE", OracleDbType.NVarchar2).Value = oDetailItem.Word_item_code;
                                _oCom.Parameters.Add(":P_WORD_ITEM_DES", OracleDbType.NVarchar2).Value = oDetailItem.Word_item_des;
                                _oCom.Parameters.Add(":P_WORD_QTY", OracleDbType.Decimal).Value = oDetailItem.Word_qty;
                                _oCom.Parameters.Add(":P_WORD_UNIT_PRICE", OracleDbType.Decimal).Value = oDetailItem.Word_unit_price;
                                _oCom.Parameters.Add(":P_WORD_VALUE", OracleDbType.Decimal).Value = oDetailItem.Word_value;
                                _oCom.Parameters.Add(":P_WORD_BAL_QTY", OracleDbType.Decimal).Value = oDetailItem.Word_bal_qty;
                                _oCom.Transaction = oTrSCM;
                                ItemResult += _oCom.ExecuteNonQuery();
                            }

                            Console.WriteLine("New request items inserted. Item count :-" + ItemResult.ToString());
                        }
                    }
                    if (result > 0)
                    {
                        sqlQry = @"UPDATE INT_REQ SET ITR_STUS = 'F' WHERE ITR_SEQ_NO = :P_SEQ AND ITR_REQ_NO = :P_REQ";

                        _oCom = new OracleCommand(sqlQry, oConnHMC);
                        _oCom.Parameters.Add(":P_SEQ", OracleDbType.Int32).Value = item.Itr_seq_no;
                        _oCom.Parameters.Add(":P_REQ", OracleDbType.NVarchar2).Value = item.Itr_req_no;
                        _oCom.Transaction = oTrHMC;
                        int resultFinal = _oCom.ExecuteNonQuery();
                    }
                    return resultHeader;
                }


        public void So_Generation(string _fromCom, string _toCom, Int16 _db, string Team)
        {
            try
            {
                Console.WriteLine("");

                HMCBegin(_db);

                List<InvoiceHeader> oHeader = new List<InvoiceHeader>();
                List<InvoiceItem> oDetails = new List<InvoiceItem>();
                
                #region Get Header
                oDA = new OracleDataAdapter();
                DataSet dsTemp = new DataSet();
                string _pb = string.Empty;
                string _pbLvl = string.Empty;
                string _pc = string.Empty;
                string _cust = string.Empty;
                string _invType= string.Empty;
              ///  List<InterCompanySalesParameter> _priceParam = GetInterCompanyParameter("CC", "ABL", string.Empty, "BDL", string.Empty);
                List<InterCompanySalesParameter> _priceParam = GetInterCompanyParameter(Team, _toCom, string.Empty, _fromCom, string.Empty);
                if (_priceParam != null && _priceParam.Count > 0)
                {
                    _pc = _priceParam[0].Sritc_frm_prof;
                    _cust = _priceParam[0].Sritc_customer;
                    _pb = _priceParam[0].Sritc_pb;
                    _pbLvl = _priceParam[0].Sritc_pb_lvl;
                    _invType= _priceParam[0].SRITC_INV_TYPE;
                }




               sqlQry = @"select * from sat_hdr where sah_com=:P_sah_com AND sah_direct=1 AND (SAH_SCM_UPLOAD=0 or SAH_SCM_UPLOAD is null or SAH_SCM_UPLOAD=2 ) AND sah_stus='A'";

              // sqlQry = @"select * from sat_hdr where sah_inv_no='001-CSS-01410'";
                _oCom = new OracleCommand(sqlQry, oConnHMC);
                _oCom.Parameters.Add(":P_sah_com", OracleDbType.Varchar2).Value = _fromCom;
                _oCom.Transaction = oTrHMC;
                oDA.SelectCommand = _oCom;
                oDA.Fill(dsTemp, "sat_hdr");

                if (dsTemp.Tables[0].Rows.Count > 0)
                {
                    oHeader = DataTableExtensionsTolist.ToGenericList<InvoiceHeader>(dsTemp.Tables[0], InvoiceHeader.ConvertAll);
                }

                #endregion

                Console.WriteLine("Invoice Records Count :- " + oHeader.Count.ToString());

                List<Int32> _JonNumList = oHeader.Distinct().Select(x => x.Sah_seq_no).ToList();

                #region Get Items

                foreach (Int32 seqNumber in _JonNumList)
                {
                    oDA = new OracleDataAdapter();
                    DataSet dsItemDetails = new DataSet();
                    sqlQry = @"SELECT A.* , B.MI_LONGDESC FROM sat_itm A , MST_ITM B WHERE A.sad_itm_cd = B.MI_CD AND sad_seq_no = :P_SeqNumber AND  B.MI_ITM_TP <>'V' AND A.sad_itm_cd IN(select mi_cd from mst_itm where  mi_purcom_cd IN('ABL','AAL','AOA','LRP'))";
                    _oCom = new OracleCommand(sqlQry, oConnHMC);
                    _oCom.Parameters.Add(":P_SeqNumber", OracleDbType.Int32).Value = seqNumber;
                    _oCom.Transaction = oTrHMC;
                    oDA.SelectCommand = _oCom;
                    oDA.Fill(dsItemDetails, "sat_itm");

                    List<InvoiceItem> oItem = new List<InvoiceItem>();

                    if (dsItemDetails.Tables[0].Rows.Count > 0)
                    {
                        oItem = DataTableExtensionsTolist.ToGenericList<InvoiceItem>(dsItemDetails.Tables[0], InvoiceItem.ConvertInvoiceItem);
                        oDetails.AddRange(oItem);
                    }
                }

                Console.WriteLine("Request items Count :- " + oDetails.Count.ToString());

                #endregion

                ScmBegin();

                #region Save WHF_ORDER_REQUEST_HEADER and WHF_ORDER_REQUEST_DETAILS

                int resultHeader = 0;

                foreach (InvoiceHeader item in oHeader)
                {
                    Boolean _priceAvl = false;
                    Boolean _TaxAvl = false;
           

                

                List<InvoiceItem> oSelectedItms = new List<InvoiceItem>();
                oSelectedItms = oDetails.FindAll(x => x.Sad_seq_no == item.Sah_seq_no);
                if (oSelectedItms.Count > 0)
                {

                    foreach (InvoiceItem IntReqItm in oSelectedItms)
                    {
                        decimal _Price = 0;
                        decimal _taxRate = 0;
                        string _itemstatus = string.Empty;
                        _itemstatus = get_pb_Status(_pbLvl);
                        //_itemstatus = get_Status(_itemstatus);
                        _priceAvl = get_Price(_toCom, _pb, _pbLvl, IntReqItm.Sad_itm_cd, IntReqItm.Sad_qty, DateTime.Today, out _Price);
                        //_priceAvl = get_Price(_toCom, _pb, _pbLvl, IntReqItm.Sad_itm_cd, IntReqItm.Sad_qty, Convert.ToDateTime("31-Oct-2016").Date, out _Price);

                        if (_fromCom == "BDL" && _priceAvl == false)
                        {
                           
                            int resultFinal = _oCom.ExecuteNonQuery();
                           if(item.SAH_SCM_UPLOAD != 2)
                           {
                                sqlQry = @"UPDATE sat_hdr SET SAH_SCM_UPLOAD = 2 WHERE sah_seq_no = :P_SEQ  ";
                                _oCom = new OracleCommand(sqlQry, oConnHMC);
                                _oCom.Parameters.Add(":P_SEQ", OracleDbType.Int32).Value = item.Sah_seq_no;
                                _oCom.Transaction = oTrHMC;
                                _oCom.ExecuteNonQuery();
                               string _items = string.Empty;
                               foreach (InvoiceItem IntReqItm1 in oSelectedItms)
                               {
                                   Boolean _priceAvlFlg = false;
                                   _priceAvlFlg = get_Price(_toCom, _pb, _pbLvl, IntReqItm1.Sad_itm_cd, IntReqItm1.Sad_qty, DateTime.Today, out _Price);
                                   if (_priceAvlFlg == false)
                                   {
                                       if (string.IsNullOrEmpty(_items))
                                       {
                                           _items =  IntReqItm1.Sad_itm_cd;
                                       }
                                       else
                                       {
                                           _items = _items + "," + IntReqItm1.Sad_itm_cd;
                                       }
                                   }
                               }
                               

                               PuchaseOrderGeneration oPogeneration1 = new PuchaseOrderGeneration();
                               if (oDetails.Count > 0)
                               {
                                   oPogeneration1.Send_SMTPMail("Inoshi@abansgroup.com, soba@abansgroup.com,chamald@abansgroup.com ,darshana@abansgroup.com ,thilinis@abansgroup.com ,stores@bigdeals.lk ", "aravindak@abansgroup.com", "bhagya@abansgroup.com", "subash@abansgroup.com", "chamald@abansgroup.com", "Big Deal Transfer Price ", "  Dear Inoshi , \n \n Transfer price from Abans Limited (ABL) to Big  Deal (BDL) not available  for below mentioned items.\n Big deal Order No : " + item.Sah_remarks + " \n Item Code : " + _items + " \n \n \n Regards,  \n  IT Projects  \n  Sirius Technologies Service (Pvt) Ltd  ");
                               }
                           }
                            break;
                        }

                        _TaxAvl = get_ItemTax(_toCom, IntReqItm.Sad_itm_cd, _itemstatus, out _taxRate);
                      if (_priceAvl == false || _TaxAvl == false)
                      {
                          break ;
                      }
                     
                    }
                }
                if (_priceAvl == true && _TaxAvl == true)
                {
                    if (oDetails.Count > 0)
                    {
                        resultHeader += Save_So_Header(item, oDetails, _pb, _pbLvl, _toCom, _pc, _invType, _cust);
                    }
                }
                else
                {
                 //  PuchaseOrderGeneration oPogeneration1 = new PuchaseOrderGeneration();
                 //  if (_priceAvl == false)
                 // //  { oPogeneration1.SendSMS("0773973588", "0773973588", "Big Deal  Console - Price not available/ Item " + item.Sah_inv_no); }
                  //  if (_TaxAvl == false)
                   // { oPogeneration1.SendSMS("0773973588", "0773973588", "Big Deal  Console - tax not setup" + item.Sah_inv_no); }
                 //  resultHeader += Save_So_RequestHeader(item, oDetails, _toCom, _pc, _invType, _cust);
                }
                            
                }
                if (resultHeader > 0)
                {
                    Console.WriteLine("Inter transfer request upload successful. No of requests :-" + resultHeader.ToString());
                }

                #endregion

               
                ScmCommit();
                HMCCommit();
            }
            catch (Exception ex)
            {
                HMCRollback();
                ScmRollback();

                PuchaseOrderGeneration oPogeneration = new PuchaseOrderGeneration();
                oPogeneration.SendSMS("0773973588", "0773973588", "HMC/BDL Agent Issue" + ex.Message);
                oPogeneration.Send_SMTPMail("chamald@abansgroup.com","chamald@abansgroup.com","chamald@abansgroup.com","chamald@abansgroup.com","chamald@abansgroup.com", "HMC/BDL Agent Issue","So Generation "+ ex.Message);
                Console.WriteLine("");
                Console.WriteLine("***********************ERROR :-" + ex.Message);
            }
        }
        public int Save_So_RequestHeader(InvoiceHeader item, List<InvoiceItem> oDetails, string _toCom, string _pc, string _invType, string _customer)
        {
            Whf_order_request_header oNewHeader = new Whf_order_request_header();
            oNewHeader.Wor_ref = item.Sah_inv_no;
            oNewHeader.Wor_company = _toCom;
            oNewHeader.Wor_date = item.Sah_dt;
            oNewHeader.Wor_profit_center = _pc;
            oNewHeader.Wor_req_type = _invType;
            oNewHeader.Wor_executive = item.Sah_cre_by;
            oNewHeader.Wor_custcode = _customer;
            //   oNewHeader.Wor_exp_date = "01-JAN-9999";
            if (!string.IsNullOrEmpty(item.Sah_remarks))
            { oNewHeader.Wor_remarks = item.Sah_remarks; }
            else
            {
                oNewHeader.Wor_remarks = "N/A";
            }
            oNewHeader.Wor_tot_amt = 0;
            oNewHeader.Wor_active = "E";
            oNewHeader.Wor_createby = item.Sah_cre_by;
            oNewHeader.Wor_createwhen = item.Sah_cre_when;
            oNewHeader.Wor_lastmodifyby = item.Sah_mod_by;
            oNewHeader.Wor_lastmodifywhen = item.Sah_mod_when;
            oNewHeader.Wor_ref_no = "BDL";//item.Itr_ref;
            oNewHeader.Wor_cus_app = "P";

            sqlQry = @"INSERT INTO WHF_ORDER_REQUEST_HEADER
                            (
                            wor_ref            ,   
                            wor_company        ,   
                            wor_date           ,   
                            wor_profit_center  ,   
                            wor_req_type       ,   
                            wor_executive      ,   
                            wor_custcode       ,   
                            wor_exp_date       ,   
                            wor_remarks        ,   
                            wor_tot_amt        ,   
                            wor_active         ,   
                            wor_createby       ,   
                            wor_createwhen     ,   
                            wor_lastmodifyby   ,   
                            wor_lastmodifywhen ,   
                            wor_ref_no         ,   
                            wor_cus_app        
                            )
                            VALUES
                            (
                            :P_wor_ref            , 
                            :P_wor_company        , 
                            :P_wor_date           , 
                            :P_wor_profit_center  , 
                            :P_wor_req_type       , 
                            :P_wor_executive      , 
                            :P_wor_custcode       , 
                            :P_wor_exp_date       , 
                            :P_wor_remarks        , 
                            :P_wor_tot_amt        , 
                            :P_wor_active         , 
                            :P_wor_createby       , 
                            :P_wor_createwhen     , 
                            :P_wor_lastmodifyby   , 
                            :P_wor_lastmodifywhen , 
                            :P_wor_ref_no         , 
                            :P_wor_cus_app        
                            )";

            _oCom = new OracleCommand(sqlQry, oConnSCM);
            _oCom.Parameters.Add(":P_wor_ref", OracleDbType.NVarchar2).Value = oNewHeader.Wor_ref;
            _oCom.Parameters.Add(":P_wor_company", OracleDbType.NVarchar2).Value = oNewHeader.Wor_company;
            _oCom.Parameters.Add(":P_wor_date", OracleDbType.Date).Value = oNewHeader.Wor_date;
            _oCom.Parameters.Add(":P_wor_profit_center", OracleDbType.NVarchar2).Value = oNewHeader.Wor_profit_center;
            _oCom.Parameters.Add(":P_wor_req_type", OracleDbType.NVarchar2).Value = oNewHeader.Wor_req_type;
            _oCom.Parameters.Add(":P_wor_executive", OracleDbType.NVarchar2).Value = oNewHeader.Wor_executive;
            _oCom.Parameters.Add(":P_wor_custcode", OracleDbType.NVarchar2).Value = oNewHeader.Wor_custcode;
            _oCom.Parameters.Add(":P_wor_exp_date", OracleDbType.Date).Value = oNewHeader.Wor_exp_date;
            _oCom.Parameters.Add(":P_wor_remarks", OracleDbType.NVarchar2).Value = oNewHeader.Wor_remarks;
            _oCom.Parameters.Add(":P_wor_tot_amt", OracleDbType.Decimal).Value = oNewHeader.Wor_tot_amt;
            _oCom.Parameters.Add(":P_wor_active", OracleDbType.NVarchar2).Value = oNewHeader.Wor_active;
            _oCom.Parameters.Add(":P_wor_createby", OracleDbType.NVarchar2).Value = oNewHeader.Wor_createby;
            _oCom.Parameters.Add(":P_wor_createwhen", OracleDbType.Date).Value = oNewHeader.Wor_createwhen;
            _oCom.Parameters.Add(":P_wor_lastmodifyby", OracleDbType.NVarchar2).Value = oNewHeader.Wor_lastmodifyby;
            _oCom.Parameters.Add(":P_wor_lastmodifywhen", OracleDbType.Date).Value = oNewHeader.Wor_lastmodifywhen;
            _oCom.Parameters.Add(":P_wor_ref_no", OracleDbType.NVarchar2).Value = oNewHeader.Wor_ref_no;
            _oCom.Parameters.Add(":P_wor_cus_app", OracleDbType.NVarchar2).Value = oNewHeader.Wor_cus_app;

            _oCom.Transaction = oTrSCM;
            int result = _oCom.ExecuteNonQuery();
          //   resultHeader += result;

            Console.WriteLine("New Request inserted. Req # :-" + oNewHeader.Wor_ref);

            if (result > 0)
            {
                int ItemResult = 0;
                List<InvoiceItem> oSelectedItms = new List<InvoiceItem>();
                oSelectedItms = oDetails.FindAll(x => x.Sad_seq_no == item.Sah_seq_no);
                if (oSelectedItms.Count > 0)
                {

                    foreach (InvoiceItem IntReqItm in oSelectedItms)
                    {
                        Whf_order_request_details oDetailItem = new Whf_order_request_details();
                        oDetailItem.Word_ref = oNewHeader.Wor_ref;
                        oDetailItem.Word_line = IntReqItm.Sad_itm_line;
                        oDetailItem.Word_item_code = IntReqItm.Sad_itm_cd;
                        oDetailItem.Word_item_des = IntReqItm.Mi_longdesc;
                        oDetailItem.Word_qty = IntReqItm.Sad_qty;
                        oDetailItem.Word_unit_price = 0;
                        oDetailItem.Word_value = 0;
                        oDetailItem.Word_bal_qty = IntReqItm.Sad_qty;

                        sqlQry = @"INSERT INTO WHF_ORDER_REQUEST_DETAILS (WORD_REF,
                                       WORD_LINE,
                                       WORD_ITEM_CODE,
                                       WORD_ITEM_DES,
                                       WORD_QTY,
                                       WORD_UNIT_PRICE,
                                       WORD_VALUE,
                                       WORD_BAL_QTY)
                                      VALUES   (:P_WORD_REF,
                                                :P_WORD_LINE,
                                                :P_WORD_ITEM_CODE,
                                                :P_WORD_ITEM_DES,
                                                :P_WORD_QTY,
                                                :P_WORD_UNIT_PRICE,
                                                :P_WORD_VALUE,
                                                :P_WORD_BAL_QTY)";

                        _oCom = new OracleCommand(sqlQry, oConnSCM);
                        _oCom.Parameters.Add(":P_WORD_REF", OracleDbType.NVarchar2).Value = oDetailItem.Word_ref;
                        _oCom.Parameters.Add(":P_WORD_LINE", OracleDbType.Int32).Value = oDetailItem.Word_line;
                        _oCom.Parameters.Add(":P_WORD_ITEM_CODE", OracleDbType.NVarchar2).Value = oDetailItem.Word_item_code;
                        _oCom.Parameters.Add(":P_WORD_ITEM_DES", OracleDbType.NVarchar2).Value = oDetailItem.Word_item_des;
                        _oCom.Parameters.Add(":P_WORD_QTY", OracleDbType.Decimal).Value = oDetailItem.Word_qty;
                        _oCom.Parameters.Add(":P_WORD_UNIT_PRICE", OracleDbType.Decimal).Value = oDetailItem.Word_unit_price;
                        _oCom.Parameters.Add(":P_WORD_VALUE", OracleDbType.Decimal).Value = oDetailItem.Word_value;
                        _oCom.Parameters.Add(":P_WORD_BAL_QTY", OracleDbType.Decimal).Value = oDetailItem.Word_bal_qty;
                        _oCom.Transaction = oTrSCM;
                        ItemResult += _oCom.ExecuteNonQuery();
                    }

                    Console.WriteLine("New request items inserted. Item count :-" + ItemResult.ToString());
                }
            }
            if (result > 0)
            {
                   sqlQry = @"UPDATE sat_hdr SET SAH_SCM_UPLOAD = 1 WHERE sah_seq_no = :P_SEQ  ";

               _oCom = new OracleCommand(sqlQry, oConnHMC);
                _oCom.Parameters.Add(":P_SEQ", OracleDbType.Int32).Value = item.Sah_seq_no;
 
                _oCom.Transaction = oTrHMC;
               int resultFinal = _oCom.ExecuteNonQuery();
            } return result;
        }

        public int Save_So_Header(InvoiceHeader item, List<InvoiceItem> oDetails, string _pb, string _pblevel, string _toCom, string _pc, string _invType, string _customer)
        {
            whf_so_header oNewHeader = new whf_so_header();
            oNewHeader.SO_LINE = item.Sah_seq_no;
            oNewHeader.SO_NO = item.Sah_inv_no;
            oNewHeader.COMPANY_CODE = _toCom;
            oNewHeader.SO_DATE = item.Sah_dt;
            oNewHeader.PROFIT_CENTER_CODE = _pc;
            oNewHeader.SO_TYPE = _invType;
            oNewHeader.SALE_EX_CODE = "N/A";
            oNewHeader.CUSTOMER_CODE = _customer;
            //   oNewHeader.Wor_exp_date = "01-JAN-9999";
            if (!string.IsNullOrEmpty(item.Sah_remarks))
            { oNewHeader.REMARKS = item.Sah_remarks; }
            else
            {
                oNewHeader.REMARKS = "N/A";
            }
            oNewHeader.TOTAL_SO_AMOUNT = 0;
            oNewHeader.INVOICE_STATUS = "PENDING";
            oNewHeader.CREATE_BY = item.Sah_cre_by;
            oNewHeader.CREATE_WHEN = item.Sah_cre_when;
            oNewHeader.LAST_MODIFY_BY = item.Sah_mod_by;
            oNewHeader.LAST_MODIFY_WHEN = item.Sah_mod_when;
            oNewHeader.SO_REF_NO = "HMC";//item.Itr_ref;
            oNewHeader.CURRENCY_CODE = "LKR";
            oNewHeader.EXCHANGE_RATE = 0;
            oNewHeader.SO_AMOUNT = 0;
            oNewHeader.SO_TAX_AMOUNT = 0;
            oNewHeader.SO_COMPUTER_IP = "N/A";
            oNewHeader.D_ADD1 = "N/A";
            oNewHeader.D_ADD2 = "N/A";
          oNewHeader.TOT_DISCOUNT=0;
  oNewHeader.DIS_RATE=0;
       oNewHeader.DIS_AMOUNT=0;
               oNewHeader.TRANSPORT=0;
  oNewHeader.OTHER=0;
  oNewHeader.DOC_TAX_AMOUNT=0;

            sqlQry = @"INSERT INTO whf_so_header
                            (
                            SO_LINE,
                            SO_NO            ,   
                            COMPANY_CODE        ,   
                            SO_DATE           ,   
                            PROFIT_CENTER_CODE  ,   
                            SO_TYPE       ,   
                            SALE_EX_CODE      ,   
                            CUSTOMER_CODE       ,   
                            REMARKS        ,   
                            TOTAL_SO_AMOUNT        ,   
                            INVOICE_STATUS         ,   
                            CREATE_BY       ,   
                            CREATE_WHEN       ,   
                            LAST_MODIFY_BY     ,   
                            LAST_MODIFY_WHEN   ,   
                            SO_REF_NO ,   
                            CURRENCY_CODE    ,
                            EXCHANGE_RATE ,
                            SO_AMOUNT,
                            SO_TAX_AMOUNT    ,
                            SO_COMPUTER_IP   ,
                            D_ADD1,
                            D_ADD2,
                            TOT_DISCOUNT,
                            DIS_RATE,
                            DIS_AMOUNT ,
 TRANSPORT,
OTHER,
DOC_TAX_AMOUNT
                            )
                            VALUES
                            (
                            :P_SO_LINE,
                            :P_SO_NO            ,   
                            :P_COMPANY_CODE        ,   
                            :P_SO_DATE           ,   
                            :P_PROFIT_CENTER_CODE  ,   
                            :P_SO_TYPE       ,   
                            :P_SALE_EX_CODE      ,   
                            :P_CUSTOMER_CODE       ,   
                            :P_REMARKS        ,   
                            :P_TOTAL_SO_AMOUNT        ,   
                            :P_INVOICE_STATUS         ,   
                            :P_CREATE_BY       ,   
                            :P_CREATE_WHEN       ,   
                            :P_LAST_MODIFY_BY     ,   
                            CURRENT_DATE  ,   
                            :P_SO_REF_NO ,   
                            :P_CURRENCY_CODE   ,
                            :P_EXCHANGE_RATE  ,
                            :P_SO_AMOUNT,
                            :P_SO_TAX_AMOUNT,
                            :P_SO_COMPUTER_IP,
                                :P_D_ADD1,
                                :P_D_ADD2,
                                :P_TOT_DISCOUNT,
                                :P_DIS_RATE,
                                :P_DIS_AMOUNT,
:P_TRANSPORT,
:P_OTHER,
:P_DOC_TAX_AMOUNT
                            )";

            _oCom = new OracleCommand(sqlQry, oConnSCM);
            _oCom.Parameters.Add(":P_SO_LINE", OracleDbType.Int32).Value = oNewHeader.SO_LINE;
            _oCom.Parameters.Add(":P_SO_NO", OracleDbType.NVarchar2).Value = oNewHeader.SO_NO;
            _oCom.Parameters.Add(":P_COMPANY_CODE", OracleDbType.NVarchar2).Value = oNewHeader.COMPANY_CODE;
            _oCom.Parameters.Add(":P_SO_DATE", OracleDbType.Date).Value = oNewHeader.SO_DATE;
            _oCom.Parameters.Add(":P_PROFIT_CENTER_CODE", OracleDbType.NVarchar2).Value = oNewHeader.PROFIT_CENTER_CODE;
            _oCom.Parameters.Add(":P_SO_TYPE", OracleDbType.NVarchar2).Value = oNewHeader.SO_TYPE;
            _oCom.Parameters.Add(":P_SALE_EX_CODE", OracleDbType.NVarchar2).Value = oNewHeader.SALE_EX_CODE;
            _oCom.Parameters.Add(":P_CUSTOMER_CODE", OracleDbType.NVarchar2).Value = oNewHeader.CUSTOMER_CODE;

            _oCom.Parameters.Add(":P_REMARKS", OracleDbType.NVarchar2).Value = oNewHeader.REMARKS;
            _oCom.Parameters.Add(":P_TOTAL_SO_AMOUNT", OracleDbType.Decimal).Value = oNewHeader.TOTAL_SO_AMOUNT;
            _oCom.Parameters.Add(":P_INVOICE_STATUS", OracleDbType.NVarchar2).Value = oNewHeader.INVOICE_STATUS;
            _oCom.Parameters.Add(":P_CREATE_BY", OracleDbType.NVarchar2).Value = oNewHeader.CREATE_BY;
            _oCom.Parameters.Add(":P_CREATE_WHEN", OracleDbType.Date).Value = oNewHeader.CREATE_WHEN;
            _oCom.Parameters.Add(":P_LAST_MODIFY_BY", OracleDbType.NVarchar2).Value = oNewHeader.LAST_MODIFY_BY;
        //    _oCom.Parameters.Add(":P_LAST_MODIFY_WHEN", OracleDbType.Date).Value = oNewHeader.LAST_MODIFY_WHEN;
            _oCom.Parameters.Add(":P_SO_REF_NO", OracleDbType.NVarchar2).Value = oNewHeader.SO_REF_NO;
            _oCom.Parameters.Add(":P_CURRENCY_CODE", OracleDbType.NVarchar2).Value = oNewHeader.CURRENCY_CODE;
            _oCom.Parameters.Add(":P_EXCHANGE_RATE", OracleDbType.Decimal).Value = oNewHeader.EXCHANGE_RATE;
            _oCom.Parameters.Add(":P_SO_AMOUNT", OracleDbType.Decimal).Value = oNewHeader.SO_AMOUNT;
            _oCom.Parameters.Add(":P_SO_TAX_AMOUNT", OracleDbType.Decimal).Value = oNewHeader.SO_TAX_AMOUNT;
            _oCom.Parameters.Add(":P_SO_COMPUTER_IP", OracleDbType.NVarchar2).Value = oNewHeader.SO_COMPUTER_IP;
            _oCom.Parameters.Add(":P_D_ADD1", OracleDbType.NVarchar2).Value = oNewHeader.D_ADD1;
            _oCom.Parameters.Add(":P_D_ADD2", OracleDbType.NVarchar2).Value = oNewHeader.D_ADD2;
            _oCom.Parameters.Add(":P_TOT_DISCOUNT", OracleDbType.Int32).Value = oNewHeader.TOT_DISCOUNT;
            _oCom.Parameters.Add(":P_DIS_RATE", OracleDbType.Int32).Value = oNewHeader.DIS_RATE;
            _oCom.Parameters.Add(":P_DIS_AMOUNT", OracleDbType.Int32).Value = oNewHeader.DIS_AMOUNT;
            _oCom.Parameters.Add(":P_TRANSPORT", OracleDbType.Int32).Value = oNewHeader.TRANSPORT;
            _oCom.Parameters.Add(":P_OTHER", OracleDbType.Int32).Value = oNewHeader.OTHER;
            _oCom.Parameters.Add(":P_DOC_TAX_AMOUNT", OracleDbType.Int32).Value = oNewHeader.DOC_TAX_AMOUNT;
            _oCom.Transaction = oTrSCM;
            int result = _oCom.ExecuteNonQuery();
            //   resultHeader += result;

            Console.WriteLine("New Sales Order inserted. So # :-" + oNewHeader.SO_NO);
            decimal _soTot = 0;
            decimal _soTotTax = 0;
            if (result > 0)
            {
                int ItemResult = 0;
                List<InvoiceItem> oSelectedItms = new List<InvoiceItem>();
                oSelectedItms = oDetails.FindAll(x => x.Sad_seq_no == item.Sah_seq_no);
                if (oSelectedItms.Count > 0)
                {
                    
                    foreach (InvoiceItem IntReqItm in oSelectedItms)
                    {
                        whf_so_details oDetailItem = new whf_so_details();
                        decimal _Price = 0;
                        decimal _taxRate = 0;
                        string _itemstatus =string.Empty;
                        _itemstatus = get_pb_Status(_pblevel);
                       // _itemstatus=  get_Status(IntReqItm.Sad_itm_stus);
                        get_Price(oNewHeader.COMPANY_CODE, _pb, _pblevel, IntReqItm.Sad_itm_cd, IntReqItm.Sad_qty, DateTime.Today,out  _Price);
                        get_ItemTax(oNewHeader.COMPANY_CODE, IntReqItm.Sad_itm_cd, _itemstatus,out  _taxRate);

                        oDetailItem.SO_NO = oNewHeader.SO_NO;
                        oDetailItem.SO_LINE = oNewHeader.SO_LINE;
                        oDetailItem.ITEM_LINE_NO = IntReqItm.Sad_itm_line;
                        oDetailItem.ITEM_CODE = IntReqItm.Sad_itm_cd;
                        oDetailItem.DESCRIPTION = IntReqItm.Mi_longdesc;
                        oDetailItem.QTY = IntReqItm.Sad_qty;
                        oDetailItem.UNIT_RATE = _Price;
                        oDetailItem.AMOUNT = _Price * IntReqItm.Sad_qty;
                        oDetailItem.SO_BALANCE = IntReqItm.Sad_qty;
                        if (_taxRate > 0) { oDetailItem.ITEM_TAX_AMOUNT = oDetailItem.AMOUNT * _taxRate / 100; }
                        else { oDetailItem.ITEM_TAX_AMOUNT = 0; }
                        oDetailItem.TOT_AMOUNT = oDetailItem.AMOUNT + oDetailItem.ITEM_TAX_AMOUNT;
                        oDetailItem.PRICE_BOOK = _pb;
                        oDetailItem.PRICE_LEVEL = _pblevel;
                        oDetailItem.STATUS =_itemstatus;
                        _soTot += oDetailItem.AMOUNT;
                        _soTotTax += oDetailItem.ITEM_TAX_AMOUNT;
                        oDetailItem.MODEL = "N/A";
                        oDetailItem.DIS_AMOUNT = 0;
                        oDetailItem.DIS_RATE = 0;
                        oDetailItem.UOM = "NOS";
                        if (!string.IsNullOrEmpty(IntReqItm.Sad_res_no))
                        {
                            oDetailItem.RES_NO = IntReqItm.Sad_res_no;
                        }
                        else
                        {
                            oDetailItem.RES_NO = "N/A";
                        }
                        oDetailItem.RES_ITEM_CODE = "N/A";
                        oDetailItem.RES_LINE_NO = IntReqItm.Sad_res_line_no;
                         oDetailItem.RES_QTY=0;
                        oDetailItem.RES_BAL_QTY=0;
                        oDetailItem.RES_REQ_NO = string.Empty;
                      if  (     get_Reservation(oNewHeader.COMPANY_CODE, IntReqItm.Sad_itm_cd, _itemstatus, IntReqItm.Sad_res_no)==true);
                        { 
                        oDetailItem.RES_ITEM_CODE=_resItem;
                       
                            if( IntReqItm.Sad_qty<= _Doc_Bal)
                            {  oDetailItem.RES_BAL_QTY = oDetailItem.QTY ;
                            }
                            else
                            {oDetailItem.RES_BAL_QTY=_Doc_Bal;
                            }
                            oDetailItem.RES_REQ_NO = _resReq;
                      }
                        
                     if  ( get_ReservationBalance(IntReqItm.Sad_itm_cd,_itemstatus,IntReqItm.Sad_res_no,IntReqItm.Sad_res_line_no)==true)
                     {
                              if( IntReqItm.Sad_qty< _Doc_Bal)
                             { 
                               oDetailItem.RES_QTY=  IntReqItm.Sad_qty;
                             }
                           else
                             {
                                oDetailItem.RES_QTY=    _Doc_Bal;
                             }

                              sqlQry = @"UPDATE SCM_RESERVATION_DETAIL SET QTY_DOC_BAL = QTY_DOC_BAL - :QTY_DOC_BAL ,LAST_MODIFY_BY = :LAST_MODIFY_BY, LAST_MODIFY_WHEN = CURRENT_DATE  WHERE RESNO =:RESNO AND RES_LINENO = :RES_LINENO  AND RES_ITEMCODE=:RES_ITEMCODE AND STATUS = :STATUS ";

                              _oCom = new OracleCommand(sqlQry, oConnSCM);
                              _oCom.Parameters.Add(":QTY_DOC_BAL", OracleDbType.Decimal).Value = oDetailItem.RES_QTY;
                              _oCom.Parameters.Add(":LAST_MODIFY_BY", OracleDbType.Varchar2).Value = "BDL_AGENT";
                              _oCom.Parameters.Add(":RESNO", OracleDbType.Varchar2).Value = oDetailItem.RES_NO;
                              _oCom.Parameters.Add(":RES_LINENO", OracleDbType.Int32).Value = oDetailItem.RES_LINE_NO;
                              _oCom.Parameters.Add(":RES_ITEMCODE", OracleDbType.Varchar2).Value = IntReqItm.Sad_itm_cd;
                              _oCom.Parameters.Add(":STATUS", OracleDbType.Varchar2).Value = _itemstatus;
                              _oCom.Transaction = oTrSCM;
                              int resultFinal = _oCom.ExecuteNonQuery();
           
                     }

               
                     

           _Doc_Bal = 0;
          _resReq = string.Empty;
          _resItem= string.Empty;
          if (string.IsNullOrEmpty(oDetailItem.RES_ITEM_CODE))
          {oDetailItem.RES_ITEM_CODE="N/A";
          }
  
                        sqlQry = @"INSERT INTO whf_so_details (SO_NO,
                                       SO_LINE,
                                       ITEM_LINE_NO,
                                       ITEM_CODE,
                                        DESCRIPTION,
                                       QTY,
                                       UNIT_RATE,
                                       AMOUNT,
                                       SO_BALANCE,
                                      ITEM_TAX_AMOUNT,
                                      TOT_AMOUNT,
                                      PRICE_BOOK,
                                      PRICE_LEVEL,
                                      STATUS,
                                        MODEL,
                                        DIS_AMOUNT,
                                        DIS_RATE,
                                        UOM,
                                        RES_NO,
                                        RES_LINE_NO,
                                        RES_ITEM_CODE,
                                        RES_QTY,
                                        RES_BAL_QTY,
                                        RES_REQ_NO
                                        )
                                      VALUES   (:P_SO_NO,
                                       :P_SO_LINE,
                                       :P_ITEM_LINE_NO,
                                       :P_ITEM_CODE,
                                       :P_DESCRIPTION,
                                       :P_QTY,
                                       :P_UNIT_RATE,
                                       :P_AMOUNT,
                                       :P_SO_BALANCE,
                                      :P_ITEM_TAX_AMOUNT,
                                      :P_TOT_AMOUNT,
                                      :P_PRICE_BOOK,
                                      :P_PRICE_LEVEL,
                                      :P_STATUS,
                                      :P_MODEL,
                                      :P_DIS_AMOUNT,
                                      :P_DIS_RATE,
                                      :P_UOM,
                                        :P_RES_NO,
                                        :P_RES_LINE_NO,
                                      :P_RES_ITEM_CODE,
                                      :P_RES_QTY,
                                      :P_RES_BAL_QTY,
:P_RES_REQ_NO)";
                      
                        _oCom = new OracleCommand(sqlQry, oConnSCM);
                        
                        _oCom.Parameters.Add(":P_SO_NO", OracleDbType.NVarchar2).Value = oDetailItem.SO_NO;
                        _oCom.Parameters.Add(":P_SO_LINE", OracleDbType.Int32).Value = oDetailItem.SO_LINE;
                        _oCom.Parameters.Add(":P_ITEM_LINE_NO", OracleDbType.Int32).Value = oDetailItem.ITEM_LINE_NO;
                        _oCom.Parameters.Add(":P_ITEM_CODE", OracleDbType.NVarchar2).Value = oDetailItem.ITEM_CODE;
                        _oCom.Parameters.Add(":P_DESCRIPTION", OracleDbType.NVarchar2).Value = oDetailItem.DESCRIPTION;
                        _oCom.Parameters.Add(":P_QTY", OracleDbType.Decimal).Value = oDetailItem.QTY;
                   
                        _oCom.Parameters.Add(":P_UNIT_RATE", OracleDbType.Decimal).Value = oDetailItem.UNIT_RATE;
                        _oCom.Parameters.Add(":P_AMOUNT", OracleDbType.Decimal).Value = oDetailItem.AMOUNT;
                        _oCom.Parameters.Add(":P_SO_BALANCE", OracleDbType.Decimal).Value = oDetailItem.SO_BALANCE;
                        _oCom.Parameters.Add(":P_ITEM_TAX_AMOUNT", OracleDbType.Decimal).Value = oDetailItem.ITEM_TAX_AMOUNT;
                        _oCom.Parameters.Add(":P_TOT_AMOUNT", OracleDbType.Decimal).Value = oDetailItem.TOT_AMOUNT;
                        _oCom.Parameters.Add(":P_PRICE_BOOK", OracleDbType.NVarchar2).Value = oDetailItem.PRICE_BOOK;
                        _oCom.Parameters.Add(":P_PRICE_LEVEL", OracleDbType.NVarchar2).Value = oDetailItem.PRICE_LEVEL;
                        _oCom.Parameters.Add(":P_STATUS", OracleDbType.NVarchar2).Value = oDetailItem.STATUS;
                        _oCom.Parameters.Add(":P_MODEL", OracleDbType.NVarchar2).Value = oDetailItem.MODEL;
                         _oCom.Parameters.Add(":P_DIS_AMOUNT", OracleDbType.Decimal).Value = oDetailItem.DIS_AMOUNT;
                         _oCom.Parameters.Add(":P_DIS_RATE", OracleDbType.Decimal).Value = oDetailItem.DIS_RATE;
                         _oCom.Parameters.Add(":P_UOM", OracleDbType.NVarchar2).Value = oDetailItem.UOM;
                         _oCom.Parameters.Add(":P_RES_NO", OracleDbType.NVarchar2).Value = oDetailItem.RES_NO;
                         _oCom.Parameters.Add(":P_RES_LINE_NO", OracleDbType.Int32).Value = oDetailItem.RES_LINE_NO;
                         _oCom.Parameters.Add(":P_RES_ITEM_CODE", OracleDbType.NVarchar2).Value = oDetailItem.RES_ITEM_CODE;
                         _oCom.Parameters.Add(":P_RES_QTY", OracleDbType.Decimal).Value = oDetailItem.RES_QTY;
                         _oCom.Parameters.Add(":P_RES_BAL_QTY", OracleDbType.Decimal).Value = oDetailItem.RES_BAL_QTY;
                         _oCom.Parameters.Add(":P_RES_REQ_NO", OracleDbType.NVarchar2).Value = oDetailItem.RES_NO;
                        _oCom.Transaction = oTrSCM;
                        ItemResult += _oCom.ExecuteNonQuery();


                        sqlQry = @"INSERT INTO WHF_SO_ITEM_TAX (SO_NO,
                                                            SO_ITEM_CODE,
                                                            SO_ITEM_TAX_TYPE,
                                                            SO_ITEM_TAX_RATE,
                                                            SO_ITEM_TAX_AMOUNT)
                                                    VALUES   (:P_SO_NO,
                                                     :P_SO_ITEM_CODE,
                                                     :SO_ITEM_TAX_TYPE,
                                                     :SO_ITEM_TAX_RATE,
                                                     :SO_ITEM_TAX_AMOUNT)";
                        _oCom = new OracleCommand(sqlQry, oConnSCM);
                        _oCom.Parameters.Add(":P_SO_NO", OracleDbType.NVarchar2).Value = oDetailItem.SO_NO;
                        _oCom.Parameters.Add(":P_SO_ITEM_CODE", OracleDbType.NVarchar2).Value = oDetailItem.ITEM_CODE;
                        _oCom.Parameters.Add(":SO_ITEM_TAX_TYPE", OracleDbType.NVarchar2).Value = "VAT";
                        _oCom.Parameters.Add(":SO_ITEM_TAX_RATE", OracleDbType.Decimal).Value = _taxRate;
                        _oCom.Parameters.Add(":SO_ITEM_TAX_AMOUNT", OracleDbType.Decimal).Value = oDetailItem.ITEM_TAX_AMOUNT;
                        _oCom.Transaction = oTrSCM;
                        ItemResult += _oCom.ExecuteNonQuery();
                    
                    }

                    Console.WriteLine("New request items inserted. Item count :-" + ItemResult.ToString());
                }
            }
            if (result > 0)
            {
                sqlQry = @"UPDATE sat_hdr SET SAH_SCM_UPLOAD = 1 WHERE sah_seq_no = :P_SEQ  ";
                _oCom = new OracleCommand(sqlQry, oConnHMC);
                _oCom.Parameters.Add(":P_SEQ", OracleDbType.Int32).Value = item.Sah_seq_no;
                _oCom.Transaction = oTrHMC;
               int resultFinal = _oCom.ExecuteNonQuery();

                sqlQry = @"UPDATE whf_so_header SET TOTAL_SO_AMOUNT = :P_TOT ,SO_AMOUNT=:P_SO_AMOUNT , SO_TAX_AMOUNT=:P_SO_TAX_AMOUNT WHERE SO_LINE = :P_SEQ  ";
                _oCom = new OracleCommand(sqlQry, oConnSCM);
                _oCom.Parameters.Add(":P_TOT", OracleDbType.Decimal).Value = _soTot + _soTotTax;
                _oCom.Parameters.Add(":P_SO_AMOUNT", OracleDbType.Decimal).Value = _soTot;
                _oCom.Parameters.Add(":P_SO_TAX_AMOUNT", OracleDbType.Decimal).Value = _soTotTax;
                _oCom.Parameters.Add(":P_SEQ", OracleDbType.Int32).Value = item.Sah_seq_no;
                _oCom.Transaction = oTrSCM;
                  resultFinal = _oCom.ExecuteNonQuery();

            } return result;
        }
        public string get_Com(string Str_Loc)
        {
            string functionReturnValue = string.Empty;
            OracleDataReader Ordsub = default(OracleDataReader);

            functionReturnValue = string.Empty;
            string _sql = " select com_code from scm_location_master where loc_code =:p_loc_code and status='E'";
            OracleCommand _oCom = new OracleCommand(_sql, oConnSCM) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Transaction = oTrSCM;
            _oCom.Parameters.Add(":p_loc_code", OracleDbType.NVarchar2).Value = Str_Loc;

            Ordsub = _oCom.ExecuteReader();
            if (Ordsub.HasRows == true)
            {
                while (Ordsub.Read())
                {
                    functionReturnValue = Ordsub["com_code"].ToString();
                }
            }
            Ordsub.Close();
            return functionReturnValue;
        }

        public string get_pb_Status(string Str_pl)
        {
            string functionReturnValue = string.Empty;
            OracleDataReader Ordsub = default(OracleDataReader);

            functionReturnValue = string.Empty;
            string _sql = " SELECT item_status FROM scm_pricebook_levels WHERE pbook_level_code=:p_pl AND is_default=1 and status='E'";
            OracleCommand _oCom = new OracleCommand(_sql, oConnSCM) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Transaction = oTrSCM;
            _oCom.Parameters.Add(":p_pl", OracleDbType.NVarchar2).Value = Str_pl;

            Ordsub = _oCom.ExecuteReader();
            if (Ordsub.HasRows == true)
            {
                while (Ordsub.Read())
                {
                    functionReturnValue = Ordsub["item_status"].ToString();
                }
            }
            Ordsub.Close();
            return functionReturnValue;
        }
        public string get_Status(string Str_status)
        {
            string functionReturnValue = string.Empty;
            OracleDataReader Ordsub = default(OracleDataReader);

            functionReturnValue = string.Empty;
            string _sql = " select mis_old_cd from mst_itm_stus where  mis_cd =:p_status";
            OracleCommand _oCom = new OracleCommand(_sql, oConnHMC) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Transaction = oTrHMC;
            _oCom.Parameters.Add(":p_status", OracleDbType.NVarchar2).Value = Str_status;

            Ordsub = _oCom.ExecuteReader();
            if (Ordsub.HasRows == true)
            {
                while (Ordsub.Read())
                {
                    functionReturnValue = Ordsub["mis_old_cd"].ToString();
                }
            }
            Ordsub.Close();
            return functionReturnValue;
        }

        public Boolean CheckInvoice(string Str_refNo,string _com)
        {
            Boolean functionReturnValue = false;
            OracleDataReader Ordsub = default(OracleDataReader);

      
            string _sql = " select sah_ref_doc from sat_hdr where sah_ref_doc=:p_ref and sah_com=:p_com";
            OracleCommand _oCom = new OracleCommand(_sql, oConnEMS) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Transaction = oTrEMS;
            _oCom.Parameters.Add(":p_ref", OracleDbType.NVarchar2).Value = Str_refNo;
            _oCom.Parameters.Add(":p_com", OracleDbType.NVarchar2).Value = _com;
            Ordsub = _oCom.ExecuteReader();
            if (Ordsub.HasRows == true)
            {

                functionReturnValue = true;
                 
            }
            Ordsub.Close();
            return functionReturnValue;
        }
        public string get_customer(string Str_com, string Str_loc)
        {
            string functionReturnValue = string.Empty;
            OracleDataReader Ordsub = default(OracleDataReader);

            functionReturnValue = string.Empty;
            string _sql = " select sun_ref from scm_cost_center_master where company_code=:p_com and def_loc=:p_loc";
            OracleCommand _oCom = new OracleCommand(_sql, oConnSCM) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Transaction = oTrSCM;
            _oCom.Parameters.Add(":p_com", OracleDbType.NVarchar2).Value = Str_com;
            _oCom.Parameters.Add(":p_loc", OracleDbType.NVarchar2).Value = Str_loc;
            Ordsub = _oCom.ExecuteReader();
            if (Ordsub.HasRows == true)
            {
                while (Ordsub.Read())
                {
                    functionReturnValue = Ordsub["sun_ref"].ToString();
                }
            }
            Ordsub.Close();
            return functionReturnValue;
        }


//        public Boolean get_Price(string Str_com, string Str_pb, string Str_pblvl, string Str_item, decimal Str_Qty, DateTime Str_Date, out decimal OutPrice)
//        {
//            Boolean functionReturnValue = false;
//            OracleDataReader Ordsub = default(OracleDataReader);
//            OutPrice = 0;

//            string _sql = @"selecT DISTINCT SAPD_PB_TP_CD,SAPD_PBK_LVL_CD,SAPD_ITM_CD,SAPD_ITM_PRICE,sapd_from_date,sapd_pb_seq from sar_pb_det where sapd_pb_tp_cd =:p_pbook AND SAPD_PBK_LVL_CD =:p_book_level_code AND SAPD_ITM_CD =:p_item_code AND SAPD_FROM_DATE <=:p_tdate AND SAPD_TO_DATE >=:p_tdate
//                            AND SAPD_QTY_FROM <=:p_qty AND SAPD_QTY_TO >=:p_qty AND sapd_price_stus ='A' AND sapd_is_cancel =0 ORDER BY sapd_from_date DESC,sapd_pb_seq DESC";


//            //_oCom.Parameters.Add(":P_sah_com", OracleDbType.Varchar2).Value = _fromCom;
//            //_oCom.Transaction = oTrHMC;
//            //oDA.SelectCommand = _oCom;

//            OracleCommand _oCom = new OracleCommand(_sql, oConnHMC) { CommandType = CommandType.Text, BindByName = true };
//            _oCom.Transaction = oTrHMC;
//            _oCom.Parameters.Add(":p_item_code", OracleDbType.NVarchar2).Value = Str_item;
//            _oCom.Parameters.Add(":p_book_level_code", OracleDbType.NVarchar2).Value = Str_pblvl;
//            _oCom.Parameters.Add(":p_tdate", OracleDbType.Date).Value = Str_Date;
//            _oCom.Parameters.Add(":p_qty", OracleDbType.Decimal).Value = Str_Qty;
//            _oCom.Parameters.Add(":p_pbook", OracleDbType.NVarchar2).Value = Str_pb;
//            //  _oCom.Parameters.Add(":p_com_code", OracleDbType.NVarchar2).Value = Str_com;
//            Ordsub = _oCom.ExecuteReader();
//            if (Ordsub.HasRows == true)
//            {
//                functionReturnValue = true;
//                if (Ordsub.Read())
//                {
//                    OutPrice = Convert.ToDecimal(Ordsub["SAPD_ITM_PRICE"].ToString());
//                }
//            }
//            Ordsub.Close();
//            return functionReturnValue;
//        }

        public Boolean get_Price(string Str_com, string Str_pb, string Str_pblvl, string Str_item, decimal Str_Qty, DateTime Str_Date, out decimal OutPrice)
        {
            Boolean functionReturnValue = false;
            OracleDataReader Ordsub = default(OracleDataReader);
            OutPrice = 0;

            string _sql = @"select  distinct 'a',scm_pricebook_header.pbook_type_code, scm_pricebook_header.pbook_level_code, scm_pricebook_header.valid_from,
                            scm_pricebook_header.valid_to, scm_pricebook_details.item_code, scm_pricebook_details.qty_from,
                            scm_pricebook_details.qty_to, scm_pricebook_details.item_price,scm_pricebook_details.pb_seq,scm_pricebook_details.seq_no,scm_pricebook_details.price_status from scm_pricebook_header inner join 
                            scm_pricebook_details on scm_pricebook_header.pbook_type_code = scm_pricebook_details.pbook_type_code and 
                            scm_pricebook_header.pbook_level_code = scm_pricebook_details.pbook_level_code and 
                            scm_pricebook_header.pb_seq = scm_pricebook_details.pb_seq inner join
                            scm_pricebook_levels on scm_pricebook_header.pbook_level_code = scm_pricebook_levels.pbook_level_code and
                            scm_pricebook_header.pbook_type_code = scm_pricebook_levels.price_book 
                            where (scm_pricebook_details.item_code = :p_item_code) 
                            and (scm_pricebook_header.pbook_level_code = :p_book_level_code) and 
                            (scm_pricebook_header.valid_from <= :p_tdate) and
                            (scm_pricebook_header.valid_to >= :p_tdate) and 
                            (scm_pricebook_details.qty_from <= :p_qty) 
                            and (scm_pricebook_details.qty_to >= :p_qty) and 
                            (scm_pricebook_header.pbook_type_code = :p_pbook) 
                            and (scm_pricebook_header.status = 'E') and (scm_pricebook_header.is_combine = 0)
                            and (scm_pricebook_details.is_cancel = 0) and (scm_pricebook_header.customer_code = 'N/A') and (scm_pricebook_details.price_status in ('A','S')) 
                            and (scm_pricebook_levels.com_code = :p_com_code)
                            order by scm_pricebook_header.valid_from desc,scm_pricebook_details.pb_seq desc";
            OracleCommand _oCom = new OracleCommand(_sql, oConnSCM) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Transaction = oTrSCM;
            _oCom.Parameters.Add(":p_item_code", OracleDbType.NVarchar2).Value = Str_item;
            _oCom.Parameters.Add(":p_book_level_code", OracleDbType.NVarchar2).Value = Str_pblvl;
            _oCom.Parameters.Add(":p_tdate", OracleDbType.Date).Value = Str_Date;
            _oCom.Parameters.Add(":p_qty", OracleDbType.Decimal).Value = Str_Qty;
            _oCom.Parameters.Add(":p_pbook", OracleDbType.NVarchar2).Value = Str_pb;
            _oCom.Parameters.Add(":p_com_code", OracleDbType.NVarchar2).Value = Str_com;
            Ordsub = _oCom.ExecuteReader();
            if (Ordsub.HasRows == true)
            {
                functionReturnValue = true;
                if (Ordsub.Read())
                {
                    OutPrice = Convert.ToDecimal(Ordsub["item_price"].ToString());
                }
            }
            Ordsub.Close();
            return functionReturnValue;
        }

        public Boolean get_ItemTax(string Str_Com, string Str_item, string Str_Status,out decimal strTaxRate)
        {
            Boolean functionReturnValue =false;
            OracleDataReader Ordsub = default(OracleDataReader);
            strTaxRate = 0;

            string _sql = "select tax_rate from scm_item_company_tax_details where com_code =:p_com_code and item_code=:p_item_code and item_status= :p_item_status ";
            OracleCommand _oCom = new OracleCommand(_sql, oConnSCM) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Transaction = oTrSCM;
            _oCom.Parameters.Add(":p_com_code", OracleDbType.NVarchar2).Value = Str_Com;
            _oCom.Parameters.Add(":p_item_code", OracleDbType.NVarchar2).Value = Str_item;
            _oCom.Parameters.Add(":p_item_status", OracleDbType.NVarchar2).Value = Str_Status;
            Ordsub = _oCom.ExecuteReader();
            if (Ordsub.HasRows == true)
            {
                functionReturnValue = true;
                if (Ordsub.Read())
                {
                    strTaxRate = Convert.ToDecimal(Ordsub["tax_rate"].ToString());
                }
            }
            Ordsub.Close();
            return functionReturnValue;
        }

        Decimal _Doc_Bal = 0;
        string _resReq = string.Empty;
        string _resItem= string.Empty;
      
        public Boolean get_Reservation(string Str_Com, string Str_item, string Str_Status, string Str_Reservation)
        {
            Boolean functionReturnValue = false;
            OracleDataReader Ordsub = default(OracleDataReader);
        


            string _sql = @"SELECT 'A',SCM_RESERVATION.RESERVATION_NO, SCM_RESERVATION.REQUEST_NO, SCM_RESERVATION.RESERVATION_DATE,
SCM_RESERVATION_DETAIL.RES_LINENO,SCM_RESERVATION_DETAIL.STATUS,SCM_RESERVATION_DETAIL.QTY_DOC_BAL, SCM_RESERVATION_DETAIL.RES_ITEMCODE 
FROM SCM_RESERVATION INNER JOIN 
 SCM_RESERVATION_DETAIL ON SCM_RESERVATION.RESERVATION_NO = SCM_RESERVATION_DETAIL.RESNO 
WHERE (SCM_RESERVATION_DETAIL.RES_ITEMCODE = :RES_ITEMCODE ) AND (SCM_RESERVATION_DETAIL.STATUS =:STATUS) AND (SCM_RESERVATION.CUS_CODE  ='NA' OR SCM_RESERVATION.CUS_CODE  ='N/A' ) AND 
 (SCM_RESERVATION.STATUS IN ('OPEN')) AND (SCM_RESERVATION.COMPANY_CODE = :COMPANY_CODE) AND (SCM_RESERVATION.RESERVATION_NO=:RESERVATION_NO) ";


            OracleCommand _oCom = new OracleCommand(_sql, oConnSCM) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Transaction = oTrSCM;
            _oCom.Parameters.Add(":RES_ITEMCODE", OracleDbType.NVarchar2).Value = Str_item;
            _oCom.Parameters.Add(":STATUS", OracleDbType.NVarchar2).Value = Str_Status;
            _oCom.Parameters.Add(":COMPANY_CODE", OracleDbType.NVarchar2).Value = Str_Com;
            _oCom.Parameters.Add(":RESERVATION_NO", OracleDbType.NVarchar2).Value = Str_Reservation;
            Ordsub = _oCom.ExecuteReader();
            if (Ordsub.HasRows == true)
            {
                functionReturnValue = true;
                if (Ordsub.Read())
                {
                    _Doc_Bal = Convert.ToDecimal(Ordsub["QTY_DOC_BAL"].ToString());
                    _resReq = Convert.ToString(Ordsub["REQUEST_NO"].ToString());
                    _resItem = Convert.ToString(Ordsub["RES_ITEMCODE"].ToString());
  
                }
            }
            Ordsub.Close();
            return functionReturnValue;
        }

        public Boolean get_ReservationBalance( string Str_item, string Str_Status,  string Str_Reservation, Int32 _resLine)
        {
            Boolean functionReturnValue = false;
            OracleDataReader Ordsub = default(OracleDataReader);



            string _sql = "SELECT * FROM SCM_RESERVATION_DETAIL WHERE RESNO = :RESNO AND RES_LINENO = :RES_LINENO  AND RES_ITEMCODE=:RES_ITEMCODE AND STATUS = :STATUS  AND QTY_DOC_BAL > 0";


            OracleCommand _oCom = new OracleCommand(_sql, oConnSCM) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Transaction = oTrSCM;
            _oCom.Parameters.Add(":RESNO", OracleDbType.NVarchar2).Value = Str_Reservation;
            _oCom.Parameters.Add(":RES_LINENO", OracleDbType.Int32).Value = _resLine;
            _oCom.Parameters.Add(":RES_ITEMCODE", OracleDbType.NVarchar2).Value = Str_item;
            _oCom.Parameters.Add(":STATUS", OracleDbType.NVarchar2).Value = Str_Status;
            Ordsub = _oCom.ExecuteReader();
            if (Ordsub.HasRows == true)
            {
                functionReturnValue = true;
                if (Ordsub.Read())
                {
                    _Doc_Bal = Convert.ToDecimal(Ordsub["QTY_DOC_BAL"].ToString());
                 

                }
            }
            Ordsub.Close();
            return functionReturnValue;
        }
        public List<InterCompanySalesParameter> GetInterCompanyParameter(string _admintm, string _fromCompany, string _fromProfit, string _toCompany, string _toProfit)
        {
            //sp_getintercompanyparameter (p_admintm in NVARCHAR2, p_fromcom in VARCHAR2,p_fromprofit in NVARCHAR2,p_tocom in NVARCHAR2,p_toprofit in NVARCHAR2, c_data OUT sys_refcursor)
            List<InterCompanySalesParameter> _list = new List<InterCompanySalesParameter>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_admintm", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _admintm;
            (param[1] = new OracleParameter("p_fromcom", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _fromCompany;
            (param[2] = new OracleParameter("p_fromprofit", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _fromProfit;
            (param[3] = new OracleParameter("p_tocom", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _toCompany;
            (param[4] = new OracleParameter("p_toprofit", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _toProfit;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _tblData = QueryDataTable("tblData", "sp_getintercompanyparameter", CommandType.StoredProcedure, false, param);
            if (_tblData.Rows.Count > 0)
            {
                _list = DataTableExtensionsTolist.ToGenericList<InterCompanySalesParameter>(_tblData, InterCompanySalesParameter.Converter);
                
            }

            return _list;
        }

        public string GetInvoicePrefix(string _company, string _profitcenter, string _invoicetype)
        {
            // sp_getinvoiceprefix (p_com in NVARCHAR2,p_profit in NVARCHAR2 ,p_invoicetype in NVARCHAR2,c_data out sys_refcursor)
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_profit", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _profitcenter;
            (param[2] = new OracleParameter("p_invoicetype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoicetype;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            string _prefix = string.Empty;

            DataTable _dtResults = QueryDataTable("tbldata", "sp_getinvoiceprefix", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                foreach (DataRow _r in _dtResults.Rows)
                {
                    _prefix = _r[0].ToString();
                    break;
                }

            }

            return _prefix;
        }

        public List<MasterItemTax> GetItemTax(string _company, string _item, string _status, string _taxCode, string _taxRateCode)
        {
            //sp_getitemtax   (p_com in NVARCHAR2,p_item in NVARCHAR2,p_status in NVARCHAR2 , p_taxcode in NVARCHAR2,p_taxratecode in NVARCHAR2, c_data  OUT sys_refcursor)
            List<MasterItemTax> _list = new List<MasterItemTax>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[2] = new OracleParameter("p_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _status;
            (param[3] = new OracleParameter("p_taxcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _taxCode;
            (param[4] = new OracleParameter("p_taxratecode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _taxRateCode;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _tblData = QueryDataTable("tblData", "sp_getitemtax", CommandType.StoredProcedure, false, param);
            if (_tblData.Rows.Count > 0)
            {
                _list = DataTableExtensionsTolist.ToGenericList<MasterItemTax>(_tblData, MasterItemTax.ConvertTotal);
            }

            return _list;


        }

        public Int32 GetSerialID()
        {
            OracleParameter[] param = new OracleParameter[1];
            Int32 effects = 0;

            param[0] = new OracleParameter("o_serialid", OracleDbType.Int32, null, ParameterDirection.Output);
           
            effects = (Int16)UpdateRecords(oConnEMS, "sp_getserialid", CommandType.StoredProcedure, param);
            return effects;
        }


        public DataTable QueryDataTable(String _tableName, String _storedProcedure, CommandType _commTypes, Boolean _isTransactionBase, params OracleParameter[] _parameters)
        {
            OracleCommand command = new OracleCommand();
            DataTable table = null;
            MakeCommand(command, oConnHMC, oTrHMC, _commTypes, _storedProcedure, _isTransactionBase, _parameters);
            OracleDataAdapter adapter = GetAdapter();
            table = new DataTable(_tableName);
            adapter.SelectCommand = command;
            adapter.Fill(table);
            return table;
        }
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
                _command.Transaction = oTrEMS;
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
        private OracleDataAdapter GetAdapter()
        {
            if (oAdapter == null)
            {
                oAdapter = new OracleDataAdapter();

            }
            return oAdapter;
        }
    }
}
