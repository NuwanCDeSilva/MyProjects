using EMS_Upload_Console;
using FF.BusinessObjects;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UtilityClasses;
using System.Net.Mail;

namespace Hero_Service_Consol
{
    class PuchaseOrderGeneration : Conn
    {
    
        private OracleDataAdapter oDA;
        private OracleDataAdapter oDA1;
        private OracleCommand _oCom;
        String sqlQry = string.Empty;
        string _sql;
        #region PO Generation
        public void PO_Generation(string _fromCom, string _toCom, Int16 _db,string _team)
        {
              try
            {
                Console.WriteLine("");

                HMCBegin(_db);
                ScmBegin();
                REPBegin(_db);

                List<PurchaseOrder> oPOHeader = new List<PurchaseOrder>();
                List<PurchaseOrderDetail> oPODetails = new List<PurchaseOrderDetail>();
                int I  = 0;

                oDA = new OracleDataAdapter();

                oDA1 = new OracleDataAdapter();
             
                DataSet dsInvHeaders = new DataSet();
                DataSet dsInvDet = new DataSet();
                DataSet dsMoveHdr= new DataSet();
                DataSet dsMoveDet= new DataSet();
                DataSet dsMoveSer= new DataSet();


                string _pb = string.Empty;
                string _pbLvl = string.Empty;
                string _PC = string.Empty;
                string _cust = string.Empty;
                string _invType = string.Empty;
            

                ///  List<InterCompanySalesParameter> _priceParam = GetInterCompanyParameter("CC", "ABL", string.Empty, "BDL", string.Empty);
                List<InterCompanySalesParameter> _priceParam = GetInterCompanyParameter(_team, _fromCom, string.Empty, _toCom, string.Empty);
                if (_priceParam != null && _priceParam.Count > 0)
                {
                    _PC = _priceParam[0].Sritc_frm_prof;
                    _cust = _priceParam[0].Sritc_customer;
                    _pb = _priceParam[0].Sritc_pb;
                    _pbLvl = _priceParam[0].Sritc_pb_lvl;
                    _invType = _priceParam[0].SRITC_INV_TYPE;
                }





          //    String _sql2 = @"Select * FROM whf_invoice_header where invoice_no= 'WPCR003841'";
                String _sql2 = @"Select * FROM whf_invoice_header WHERE sun_order_upload=0 AND  profit_center_code='" + _PC + "' AND COMPANY_CODE='" + _fromCom + "' and  CUSTOMER_CODE='" + _cust + "'";
                String _com = string.Empty;
                String docno = string.Empty;
                      _com = _toCom; 
                _oCom = new OracleCommand(_sql2, oConnSCM);
                //_oCom.Parameters.Add(":P_invoice_no", OracleDbType.NVarchar2).Value = "WPCR003841";
                _oCom.Transaction = oTrSCM;
                oDA.SelectCommand = _oCom;
                oDA.Fill(dsInvHeaders, "whf_invoice_header");
                Console.WriteLine("Invoice Count :- " + dsInvHeaders.Tables["whf_invoice_header"].Rows.Count.ToString());
                for (I = 0; I <= dsInvHeaders.Tables["whf_invoice_header"].Rows.Count - 1; I++)
                {

                    // _com = get_Supplier_Com(dsInvHeaders.Tables["whf_invoice_header"].Rows[I]["customer_code"].ToString(), dsInvHeaders.Tables["whf_invoice_header"].Rows[I]["company_code"].ToString()); 
                    _com = _toCom; //get_Com(dsInvHeaders.Tables["whf_invoice_header"].Rows[I]["del_code"].ToString());
                    PurchaseOrder _PurchaseOrder = new PurchaseOrder();
                    _PurchaseOrder.Poh_seq_no = Generate_new_seq_num("ADMIN", "PO", 1, _com);
                    _PurchaseOrder.Poh_tp = "L";
                    _PurchaseOrder.Poh_sub_tp = "N";
                    _PurchaseOrder.Poh_doc_no =Convert.ToString(_PurchaseOrder.Poh_seq_no);

                    _PurchaseOrder.Poh_com = _com;
                    _PurchaseOrder.Poh_ope = "INV";
                    _PurchaseOrder.Poh_profit_cd = string.Empty;
                    _PurchaseOrder.Poh_dt = Convert.ToDateTime(dsInvHeaders.Tables["whf_invoice_header"].Rows[I]["invoice_date"].ToString()).Date;
                    _PurchaseOrder.Poh_ref = dsInvHeaders.Tables["whf_invoice_header"].Rows[I]["invoice_no"].ToString();
                    _PurchaseOrder.Poh_job_no = string.Empty;
                    _PurchaseOrder.Poh_pay_term = string.Empty;
                    _PurchaseOrder.Poh_supp = get_Supplier(dsInvHeaders.Tables["whf_invoice_header"].Rows[I]["customer_code"].ToString(), _com);
                    _PurchaseOrder.Poh_cur_cd = "LKR";
                    _PurchaseOrder.Poh_ex_rt = 1;
                    _PurchaseOrder.Poh_trans_term = "";
                    _PurchaseOrder.Poh_port_of_orig = "";
                    _PurchaseOrder.Poh_cre_period = string.Empty;
                    _PurchaseOrder.Poh_frm_yer = Convert.ToDateTime(Convert.ToDateTime(dsInvHeaders.Tables["whf_invoice_header"].Rows[I]["invoice_date"].ToString()).Date).Year;
                    _PurchaseOrder.Poh_frm_mon = Convert.ToDateTime(Convert.ToDateTime(dsInvHeaders.Tables["whf_invoice_header"].Rows[I]["invoice_date"].ToString()).Date).Month;
                    _PurchaseOrder.Poh_to_yer = Convert.ToDateTime(Convert.ToDateTime(dsInvHeaders.Tables["whf_invoice_header"].Rows[I]["invoice_date"].ToString()).Date).Year;
                    _PurchaseOrder.Poh_to_mon = Convert.ToDateTime(Convert.ToDateTime(dsInvHeaders.Tables["whf_invoice_header"].Rows[I]["invoice_date"].ToString()).Date).Month;
                    _PurchaseOrder.Poh_preferd_eta = Convert.ToDateTime(Convert.ToDateTime(dsInvHeaders.Tables["whf_invoice_header"].Rows[I]["invoice_date"].ToString()).Date).Date;
                    _PurchaseOrder.Poh_contain_kit = false;
                    _PurchaseOrder.Poh_sent_to_vendor = false;
                    _PurchaseOrder.Poh_sent_by = "";
                    _PurchaseOrder.Poh_sent_via = "";
                    _PurchaseOrder.Poh_sent_add = "";
                    _PurchaseOrder.Poh_stus = "A";
                    _PurchaseOrder.Poh_remarks = dsInvHeaders.Tables["whf_invoice_header"].Rows[I]["REMARKS"].ToString();
                    _PurchaseOrder.Poh_sub_tot = Convert.ToDecimal(dsInvHeaders.Tables["whf_invoice_header"].Rows[I]["invoice_amount"].ToString());
                    _PurchaseOrder.Poh_tax_tot = Convert.ToDecimal(dsInvHeaders.Tables["whf_invoice_header"].Rows[I]["invoice_tax_amount"].ToString());
                    _PurchaseOrder.Poh_dis_rt = 0;
                    _PurchaseOrder.Poh_dis_amt = Convert.ToDecimal(dsInvHeaders.Tables["whf_invoice_header"].Rows[I]["tot_discount"].ToString());
                    _PurchaseOrder.Poh_oth_tot = 0;
                    _PurchaseOrder.Poh_tot = Convert.ToDecimal(dsInvHeaders.Tables["whf_invoice_header"].Rows[I]["total_invoice_amount"].ToString());
                    _PurchaseOrder.Poh_reprint = false;
                    _PurchaseOrder.Poh_tax_chg = false;
                    _PurchaseOrder.poh_is_conspo = 0;
                    _PurchaseOrder.Poh_cre_by = "SCMAgent";

                    SaveNewPO(_PurchaseOrder);

                    MasterAutoNumber masterAuto = new MasterAutoNumber();
                    masterAuto.Aut_cate_cd = _PurchaseOrder.Poh_com;
                    masterAuto.Aut_cate_tp = "COM";
                    masterAuto.Aut_direction = null;
                    masterAuto.Aut_modify_dt = null;
                    masterAuto.Aut_moduleid = "PUR";
                    masterAuto.Aut_number = 5;//what is Aut_number
                    masterAuto.Aut_start_char = "PUR";
                    masterAuto.Aut_year = null;


                    Int32 _autoNo = GetAutoNumber(masterAuto.Aut_moduleid, masterAuto.Aut_direction, masterAuto.Aut_start_char, masterAuto.Aut_cate_tp, masterAuto.Aut_cate_cd, masterAuto.Aut_modify_dt, masterAuto.Aut_year).Aut_number;
                    string _documentNo = masterAuto.Aut_cate_cd + "-" + masterAuto.Aut_start_char + string.Format("{0:000000}", _autoNo);
                    UpdateAutoNumber(masterAuto);
                    UpdatePODocNo(_PurchaseOrder.Poh_seq_no, _documentNo);
                    docno = _documentNo;

                    int j = 0;
                    int _lineNo = 0;
                    _sql2 = @" select * from whf_invoice_details where invoice_line=:p_invoice_line";
                    //    _com = string.Empty;
                    //  docno = string.Empty;
                    _oCom = new OracleCommand(_sql2, oConnSCM);
                    _oCom.Parameters.Add(":p_invoice_line", OracleDbType.NVarchar2).Value = dsInvHeaders.Tables["whf_invoice_header"].Rows[I]["invoice_line"].ToString();
                    _oCom.Transaction = oTrSCM;
                    oDA.SelectCommand = _oCom;
                    oDA.Fill(dsInvDet, "whf_invoice_details");
                    for (j = 0; j <= dsInvDet.Tables["whf_invoice_details"].Rows.Count - 1; j++)
                    {
                        _lineNo = _lineNo + 1;
                        PurchaseOrderDetail _tmpPoDetails = new PurchaseOrderDetail();
                        _tmpPoDetails.Pod_dis_amt = Convert.ToDecimal(dsInvDet.Tables["whf_invoice_details"].Rows[j]["dis_amount"].ToString());
                        _tmpPoDetails.Pod_dis_rt = Convert.ToDecimal(dsInvDet.Tables["whf_invoice_details"].Rows[j]["dis_rate"].ToString());
                        _tmpPoDetails.Pod_grn_bal = Convert.ToDecimal(dsInvDet.Tables["whf_invoice_details"].Rows[j]["qty"].ToString());
                        _tmpPoDetails.Pod_item_desc = dsInvDet.Tables["whf_invoice_details"].Rows[j]["description"].ToString();
                        _tmpPoDetails.Pod_itm_cd = dsInvDet.Tables["whf_invoice_details"].Rows[j]["item_code"].ToString();
                        _tmpPoDetails.Pod_itm_stus = "GDLP";//get_Status(dsInvDet.Tables["whf_invoice_details"].Rows[j]["status"].ToString());
                        _tmpPoDetails.Pod_itm_tp = dsInvDet.Tables["whf_invoice_details"].Rows[j]["item_type"].ToString(); ;
                        _tmpPoDetails.Pod_kit_itm_cd = "";
                        _tmpPoDetails.Pod_kit_line_no = 0;
                        _tmpPoDetails.Pod_lc_bal = 0;
                        _tmpPoDetails.Pod_line_amt = Convert.ToDecimal(dsInvDet.Tables["whf_invoice_details"].Rows[j]["tot_amount"].ToString());
                        _tmpPoDetails.Pod_line_no = _lineNo;
                        _tmpPoDetails.Pod_line_tax = Convert.ToDecimal(dsInvDet.Tables["whf_invoice_details"].Rows[j]["item_tax_amount"].ToString());
                        _tmpPoDetails.Pod_line_val = Convert.ToDecimal(dsInvDet.Tables["whf_invoice_details"].Rows[j]["amount"].ToString());
                        _tmpPoDetails.Pod_nbt = 0;
                        _tmpPoDetails.Pod_nbt_before = 0;
                        _tmpPoDetails.Pod_pi_bal = 0;
                        _tmpPoDetails.Pod_qty = Convert.ToDecimal(dsInvDet.Tables["whf_invoice_details"].Rows[j]["qty"].ToString());
                        _tmpPoDetails.Pod_ref_no = string.Empty;
                        _tmpPoDetails.Pod_seq_no = _PurchaseOrder.Poh_seq_no;
                        _tmpPoDetails.Pod_si_bal = 0;
                        _tmpPoDetails.Pod_tot_tax_before = 0;
                        _tmpPoDetails.Pod_unit_price = Convert.ToDecimal(dsInvDet.Tables["whf_invoice_details"].Rows[j]["unit_rate"].ToString());
                        _tmpPoDetails.Pod_uom = "";
                        _tmpPoDetails.Pod_vat = 0;
                        _tmpPoDetails.Pod_vat_before = 0;
                   if    ( get_Supplier_TaxClaim(_PurchaseOrder.Poh_supp )==true)// Tax Claimable
                   {     decimal _taxClimRate= 0;
                         decimal _TaxAmt= 0;
                         decimal _itemTaxRate= 0;
                       _taxClimRate=get_Supplier_TaxClaimItem(_com,_tmpPoDetails.Pod_itm_cd);
                       if(_taxClimRate >0)
                       {  _itemTaxRate=get_Supplier_ItemTax(_tmpPoDetails.Pod_itm_cd);
                           _TaxAmt = (( _tmpPoDetails.Pod_line_tax) * (_taxClimRate / _itemTaxRate));
                       }
                       // 03-06-2015 corrected actual value issue
                       _tmpPoDetails.Pod_act_unit_price = (((_tmpPoDetails.Pod_line_amt - _tmpPoDetails.Pod_line_tax) + _TaxAmt) / _tmpPoDetails.Pod_qty);
      

                   }
                   else
                    {
                        _tmpPoDetails.Pod_act_unit_price = (_tmpPoDetails.Pod_line_amt / _tmpPoDetails.Pod_qty);
                    }
                        SaveNewPOItem(_tmpPoDetails);

                        PurchaseOrderDelivery _tmpPoDel = new PurchaseOrderDelivery();
                        _tmpPoDel.Podi_seq_no = _PurchaseOrder.Poh_seq_no;
                        _tmpPoDel.Podi_bal_qty = Convert.ToDecimal(dsInvDet.Tables["whf_invoice_details"].Rows[j]["qty"].ToString());
                        _tmpPoDel.Podi_del_line_no = _lineNo;
                        _tmpPoDel.Podi_itm_cd = dsInvDet.Tables["whf_invoice_details"].Rows[j]["item_code"].ToString();
                        _tmpPoDel.Podi_itm_stus  = "GDLP";// get_Status(dsInvDet.Tables["whf_invoice_details"].Rows[j]["status"].ToString());
                        _tmpPoDel.Podi_line_no = _lineNo;
                        _tmpPoDel.Podi_loca = "BDLK";//dsInvHeaders.Tables["whf_invoice_header"].Rows[I]["del_code"].ToString();
                        _tmpPoDel.Podi_qty = Convert.ToDecimal(dsInvDet.Tables["whf_invoice_details"].Rows[j]["qty"].ToString());
                        _tmpPoDel.Podi_remarks = string.Empty;
                        _tmpPoDel.Podi_exp_dt = DateTime.Today;
                        SaveNewPODel(_tmpPoDel);

                    }
                    dsInvDet.Tables["whf_invoice_details"].Rows.Clear();


                    #region Update Invoice header

           
                    String _sql3 = @"UPDATE whf_invoice_header SET purchase_order_no = :P_purchase_order_no ,sun_order_upload=1   WHERE invoice_no = :P_invoice_no";
                    OracleCommand _oCom3 = new OracleCommand(_sql3, oConnSCM);
                    _oCom3.Parameters.Add(":P_purchase_order_no", OracleDbType.NVarchar2).Value = _documentNo;
                    _oCom3.Parameters.Add(":P_invoice_no", OracleDbType.NVarchar2).Value = dsInvHeaders.Tables["whf_invoice_header"].Rows[I]["invoice_no"].ToString();
                    _oCom3.Transaction = oTrSCM;
                    int result = _oCom3.ExecuteNonQuery();
                    // So update 
                
                 //   if( string.IsNullOrEmpty ( _PurchaseOrder.Poh_supp))
                   // {   _oCom3.Parameters.Add(":supp", OracleDbType.NVarchar2).Value = "N/A";  }
                  //  else
                  //  {
                  //  _oCom3.Parameters.Add(":supp", OracleDbType.NVarchar2).Value = _PurchaseOrder.Poh_supp;  
                  //   }


                    _sql3 = @"UPDATE whf_so_header SET  purchase_order_no = :P_purchase_order_no , is_pdi_req_send=1 WHERE so_no=:so_no";

                    OracleCommand _oCom33 = new OracleCommand(_sql3, oConnSCM);
                    _oCom33.Parameters.Add(":P_purchase_order_no", OracleDbType.NVarchar2).Value = _documentNo;
                    _oCom33.Parameters.Add(":so_no", OracleDbType.NVarchar2).Value = dsInvHeaders.Tables["whf_invoice_header"].Rows[I]["other_doc_no"].ToString();
       
                    _oCom33.Transaction = oTrSCM;
                     result = _oCom33.ExecuteNonQuery();



                }
                       #endregion Update Invoice header

                      #region DO



                Console.WriteLine("Invoice upload Successfull "  );
                   

                       int k = 0;
                       Int32 user_seq_num = 0;
               //   _sql2 = @"select inv_movement_header.*,purchase_order_no from whf_invoice_header,inv_movement_header  where whf_invoice_header.invoice_no=inv_movement_header.other_doc_no  AND inv_movement_header.other_doc_no='WPCR003841'";
                       _sql2 = @"select inv_movement_header.*,purchase_order_no from whf_invoice_header,inv_movement_header WHERE whf_invoice_header.invoice_no=inv_movement_header.other_doc_no   AND inv_movement_header.sun_upload=0 AND profit_center_code='" + _PC + "'  AND whf_invoice_header.COMPANY_CODE='" + _fromCom + "' AND doc_date >='27-FEB-2015' and   whf_invoice_header.CUSTOMER_CODE='" + _cust + "'";
               
                       _oCom = new OracleCommand(_sql2, oConnSCM);
 
                       _oCom.Transaction = oTrSCM;
                       oDA.SelectCommand = _oCom;
                       oDA.Fill(dsMoveHdr, "inv_movement_header");
                       Console.WriteLine("Do Count :- " + dsMoveHdr.Tables["inv_movement_header"].Rows.Count.ToString());
                       for (k = 0; k <= dsMoveHdr.Tables["inv_movement_header"].Rows.Count - 1; k++)
                       {
                           if (dsMoveHdr.Tables["inv_movement_header"].Rows.Count >0 )

                           {



                           string _documentNo = dsMoveHdr.Tables["inv_movement_header"].Rows[k]["purchase_order_no"].ToString();



                           string _sql3 = @"update pur_hdr set poh_remarks=:poh_remarks where poh_doc_no=:poh_doc_no";
                           OracleCommand _oCom33 = new OracleCommand(_sql3, oConnHMC);
                           _oCom33.Parameters.Add(":poh_remarks", OracleDbType.NVarchar2).Value = dsMoveHdr.Tables["inv_movement_header"].Rows[k]["doc_no"].ToString();
                           _oCom33.Parameters.Add(":poh_doc_no", OracleDbType.NVarchar2).Value = _documentNo;
                           _oCom33.Transaction = oTrHMC;
                            int    result = _oCom33.ExecuteNonQuery();

                             _sql3 = @"DELETE FROM temp_pick_hdr WHERE tuh_doc_no=:tuh_doc_no";
                           OracleCommand _oCom34 = new OracleCommand(_sql3, oConnREP);
                           _oCom34.Parameters.Add(":tuh_doc_no", OracleDbType.NVarchar2).Value = _documentNo;
                           _oCom34.Transaction = oTrREP;
                            result = _oCom34.ExecuteNonQuery();

                           _sql3 = @"DELETE FROM temp_pick_ser WHERE tus_doc_no=:tuh_doc_no";
                           OracleCommand _oCom35 = new OracleCommand(_sql3, oConnREP);
                           _oCom35.Parameters.Add(":tuh_doc_no", OracleDbType.NVarchar2).Value = _documentNo;
                           _oCom35.Transaction = oTrREP;
                           result = _oCom35.ExecuteNonQuery();

                           _sql3 = @"UPDATE inv_movement_header SET warrenty_print_status='YES' , pos_uploaded=1,sun_upload=1,cost_based_doc =:po WHERE DOC_NO=:DOC_NO";
                           OracleCommand _oCom36 = new OracleCommand(_sql3, oConnSCM);
                           _oCom36.Parameters.Add(":po", OracleDbType.NVarchar2).Value = _documentNo;
                           _oCom36.Parameters.Add(":DOC_NO", OracleDbType.NVarchar2).Value = dsMoveHdr.Tables["inv_movement_header"].Rows[k]["doc_no"].ToString();
                     
                          _oCom36.Transaction = oTrSCM;
                           result = _oCom36.ExecuteNonQuery();

                             _sql3 = @"UPDATE inv_movement_item_serials SET update_ex_cost = 0 
                                 WHERE doc_no = :P_dpc_no";

                           OracleCommand _oCom4 = new OracleCommand(_sql3, oConnSCM);
                           _oCom4.Parameters.Add(":P_dpc_no", OracleDbType.NVarchar2).Value = dsMoveHdr.Tables["inv_movement_header"].Rows[k]["doc_no"].ToString();
                           _oCom4.Transaction = oTrSCM;
                           result = _oCom4.ExecuteNonQuery();


                           OracleDataReader Ordsub = default(OracleDataReader);
                           OracleDataReader Ordsub1 = default(OracleDataReader);
                          _sql = " select * from inv_movement_cost where doc_no=:p_doc_no ORDER BY ITEM_LINE_NO";
                           OracleCommand _oCom5 = new OracleCommand(_sql, oConnSCM) { CommandType = CommandType.Text, BindByName = true };
                           _oCom5.Transaction = oTrSCM;
                           _oCom5.Parameters.Add(":p_doc_no", OracleDbType.NVarchar2).Value = dsMoveHdr.Tables["inv_movement_header"].Rows[k]["doc_no"].ToString();
                           Ordsub = _oCom5.ExecuteReader();
                           if (Ordsub.HasRows == true)
                           {
                               int r = 0;
                               while (Ordsub.Read())
                               {
                                   for (r = 0; r <= Convert.ToInt32(Ordsub["QTY"].ToString()) - 1; r++)
                                   {
                                       _sql = "select rownum as nowno  from inv_movement_item_serials  WHERE  DOC_NO  =:p_doc_no AND ITEM_CODE=:p_itemCode AND ITEM_STATUS=:p_status AND update_ex_cost=0  AND unit_price=:p_unit_cost";
                                       OracleCommand _oCom6 = new OracleCommand(_sql, oConnSCM) { CommandType = CommandType.Text, BindByName = true };
                                       _oCom6.Transaction = oTrSCM;
                                       _oCom6.Parameters.Add(":p_doc_no", OracleDbType.NVarchar2).Value = dsMoveHdr.Tables["inv_movement_header"].Rows[k]["doc_no"].ToString();
                                       _oCom6.Parameters.Add(":p_itemCode", OracleDbType.NVarchar2).Value = Ordsub["ITEM_CODE"].ToString();
                                       _oCom6.Parameters.Add(":p_status", OracleDbType.NVarchar2).Value = Ordsub["status"].ToString();
                                       _oCom6.Parameters.Add(":p_unit_cost", OracleDbType.NVarchar2).Value = Ordsub["unit_cost"].ToString();
                                       Ordsub1 = _oCom6.ExecuteReader();
                                       if (Ordsub1.Read())
                                       {

                                           _sql3 = @"UPDATE inv_movement_item_serials SET  update_ex_cost= :p_update_ex_cost  WHERE  DOC_NO= :p_DOC_NO AND ITEM_CODE= :p_ITEM_CODE AND ITEM_STATUS= :p_ITEM_STATUS AND update_ex_cost=0 and rownum = :p_rownum AND unit_price= :p_unit_price";
                                           OracleCommand _oCom7 = new OracleCommand(_sql3, oConnSCM);
                                           _oCom7.Parameters.Add(":p_update_ex_cost", OracleDbType.NVarchar2).Value = Ordsub["ITEM_LINE_NO"].ToString();
                                           _oCom7.Parameters.Add(":p_DOC_NO", OracleDbType.NVarchar2).Value = Ordsub["DOC_NO"].ToString();
                                           _oCom7.Parameters.Add(":p_ITEM_CODE", OracleDbType.NVarchar2).Value = Ordsub["ITEM_CODE"].ToString();
                                           _oCom7.Parameters.Add(":p_ITEM_STATUS", OracleDbType.NVarchar2).Value = Ordsub["STATUS"].ToString();
                                           _oCom7.Parameters.Add(":p_rownum", OracleDbType.NVarchar2).Value = Ordsub1["nowno"].ToString();
                                           _oCom7.Parameters.Add(":p_unit_price", OracleDbType.NVarchar2).Value = Ordsub["unit_cost"].ToString();
                                           _oCom7.Transaction = oTrSCM;
                                           result = _oCom7.ExecuteNonQuery();
                                       }
                                       Ordsub1.Close();
                                   }
                               }
                               }
                                 Ordsub.Close();

                                   // functionReturnValue = Ordsub["QTY"].ToString();
                         
                             
                    
                    



                           user_seq_num = Generate_new_seq_num("ADMIN", "GRN", 1, _com);
                           ReptPickHeader RPH = new ReptPickHeader();
                           RPH.Tuh_doc_tp = "GRN";
                           RPH.Tuh_cre_dt = DateTime.Today;
                           RPH.Tuh_ischek_itmstus = true;
                           RPH.Tuh_ischek_reqqty = true;
                           RPH.Tuh_ischek_simitm = true;
                           RPH.Tuh_session_id = string.Empty;
                           RPH.Tuh_usr_com = _com;
                           RPH.Tuh_usr_id = "ADMIN";
                           RPH.Tuh_usrseq_no = user_seq_num;
                           RPH.Tuh_direct = true;
                           RPH.Tuh_doc_no = _documentNo;

                           int affected_rows = SaveSeq_to_TempPickHDR(RPH);


                      
                           int  l = 0;
                           int m= 0;






                           _sql2 = @"select * from inv_movement_cost where  doc_no=:p_doc_no";
                    
                       _oCom = new OracleCommand(_sql2, oConnSCM);
                       _oCom.Parameters.Add(":p_doc_no", OracleDbType.NVarchar2).Value = dsMoveHdr.Tables["inv_movement_header"].Rows[k]["doc_no"].ToString();
                       _oCom.Transaction = oTrSCM;
                       oDA.SelectCommand = _oCom;
                       oDA.Fill(dsMoveDet, "inv_movement_cost");
                       for (l = 0; l <= dsMoveDet.Tables["inv_movement_cost"].Rows.Count - 1; l++)
                       {
                           ReptPickSerials _pick = new ReptPickSerials();

                           #region Serial
                           _sql2 = @"select * from inv_movement_item_serials where doc_no=:p_doc_no and item_code=:P_item_code and item_status=:P_item_status AND update_ex_cost=:p_line ";
                           _oCom = new OracleCommand(_sql2, oConnSCM);
                           _oCom.Parameters.Add(":p_doc_no", OracleDbType.NVarchar2).Value = dsMoveDet.Tables["inv_movement_cost"].Rows[l]["doc_no"].ToString();
                           _oCom.Parameters.Add(":P_item_code", OracleDbType.NVarchar2).Value = dsMoveDet.Tables["inv_movement_cost"].Rows[l]["item_code"].ToString();
                           _oCom.Parameters.Add(":P_item_status", OracleDbType.NVarchar2).Value = dsMoveDet.Tables["inv_movement_cost"].Rows[l]["status"].ToString();
                           _oCom.Parameters.Add(":p_line", OracleDbType.NVarchar2).Value = dsMoveDet.Tables["inv_movement_cost"].Rows[l]["ITEM_LINE_NO"].ToString();
                           _oCom.Transaction = oTrSCM;
                           oDA.SelectCommand = _oCom;
                           oDA.Fill(dsMoveSer, "inv_movement_item_serials");
                           for (m = 0; m <= dsMoveSer.Tables["inv_movement_item_serials"].Rows.Count -1; m++)
                           {

                               _pick.Tus_base_doc_no = string.Empty;
                               _pick.Tus_base_itm_line = 1;
                               _pick.Tus_batch_line = 1;
                               _pick.Tus_bin = GetDefaultBinCode(_com, dsMoveHdr.Tables["inv_movement_header"].Rows[k]["other_location"].ToString());
                               _pick.Tus_com = _com;
                               _pick.Tus_cre_by = "ADMIN";
                               _pick.Tus_cre_dt = System.DateTime.Now;
                               //_pick.Tus_cross_batchline = Convert.ToInt16(_dr["ITS_BATCH_LINE"]);
                               //_pick.Tus_cross_itemline = Convert.ToInt16(_dr["ITS_ITM_LINE"]);
                               //_pick.Tus_cross_seqno = Convert.ToInt32(_dr["ITS_SEQ_NO"]);
                               //_pick.Tus_cross_serline = Convert.ToInt16(_dr["ITS_SER_LINE"]);
                               _pick.Tus_doc_dt = System.DateTime.Now.Date;
                               _pick.Tus_doc_no = _documentNo;

                               _pick.Tus_exist_grncom = _com;
                                  string  supp =   get_Supplier(_documentNo);
                               _pick.Tus_exist_grnno = dsMoveDet.Tables["inv_movement_cost"].Rows[l]["doc_no"].ToString();
                               _pick.Tus_exist_grndt = Convert.ToDateTime(dsMoveHdr.Tables["inv_movement_header"].Rows[k]["DOC_DATE"].ToString()).Date;
                               _pick.Tus_exist_supp = supp;
                               _pick.Tus_itm_stus = "GDLP";// get_Status(dsMoveDet.Tables["inv_movement_cost"].Rows[l]["STATUS"].ToString());
                               _pick.Tus_unit_price = Convert.ToDecimal(dsMoveDet.Tables["inv_movement_cost"].Rows[l]["unit_amount"].ToString());

                               MasterItem _itmlist = new MasterItem();
                               _itmlist = GetItem(_com, dsMoveSer.Tables["inv_movement_item_serials"].Rows[m]["item_code"].ToString());
                               if (_itmlist != null)
                               {
                                   _pick.Tus_itm_brand = _itmlist.Mi_brand;
                                   _pick.Tus_itm_desc = _itmlist.Mi_longdesc;
                                   _pick.Tus_itm_model = _itmlist.Mi_model;
                                   if (_itmlist.Mi_is_ser1 == -1)
                                   {    _pick.Tus_ser_id = 0;
                                      
                                   }
                                   else
                                   {
                                    _pick.Tus_ser_id =  GetSerialID();
                                   }
                               }

                               _pick.Tus_itm_cd = dsMoveSer.Tables["inv_movement_item_serials"].Rows[m]["item_code"].ToString();

                               _pick.Tus_itm_line = Convert.ToInt32(dsMoveDet.Tables["inv_movement_cost"].Rows[l]["item_line_no"].ToString());

                               _pick.Tus_loc = dsMoveHdr.Tables["inv_movement_header"].Rows[k]["other_location"].ToString();
                               _pick.Tus_new_remarks = String.Empty;
                               _pick.Tus_new_status = String.Empty;

                               _pick.Tus_orig_grncom = _com;
                              // _pick.Tus_orig_grndt = Convert.ToDateTime(dsMoveHdr.Tables["inv_movement_header"].Rows[k]["DOC_DATE"].ToString()).Date;
                               _pick.Tus_orig_grndt = Convert.ToDateTime(dsMoveSer.Tables["inv_movement_item_serials"].Rows[m]["grn_date"].ToString()).Date;
                               _pick.Tus_orig_grnno = dsMoveSer.Tables["inv_movement_item_serials"].Rows[m]["com_ref_no"].ToString();
                               _pick.Tus_orig_supp = supp;

                               _pick.Tus_out_date = DateTime.Now.Date;
                               _pick.Tus_qty = 1;
                               _pick.Tus_seq_no = 0;
                               _pick.Tus_ser_1 = dsMoveSer.Tables["inv_movement_item_serials"].Rows[m]["serial_no"] == DBNull.Value ? string.Empty : dsMoveSer.Tables["inv_movement_item_serials"].Rows[m]["serial_no"].ToString();
                               _pick.Tus_ser_2 = dsMoveSer.Tables["inv_movement_item_serials"].Rows[m]["chassis_no"] == DBNull.Value ? string.Empty : dsMoveSer.Tables["inv_movement_item_serials"].Rows[m]["chassis_no"].ToString();
                               _pick.Tus_ser_3 = string.Empty;
                               _pick.Tus_ser_4 = string.Empty;

                        

                               _pick.Tus_ser_line = 0;

                               _pick.Tus_serial_id = String.Empty;

                               _pick.Tus_session_id = String.Empty;
                               _pick.Tus_unit_cost = get_Cost(_documentNo, _pick.Tus_itm_cd, _pick.Tus_itm_stus);
                            //   _pick.Tus_unit_cost = Convert.ToDecimal(dsMoveDet.Tables["inv_movement_cost"].Rows[l]["unit_cost"].ToString());

                               _pick.Tus_usrseq_no    = user_seq_num;;
                               _pick.Tus_warr_no = dsMoveSer.Tables["inv_movement_item_serials"].Rows[m]["warrenty_no"] == DBNull.Value ? string.Empty : dsMoveSer.Tables["inv_movement_item_serials"].Rows[m]["warrenty_no"].ToString();
                               _pick.Tus_warr_period = Convert.ToInt16(dsMoveSer.Tables["inv_movement_item_serials"].Rows[m]["wara_period"]);

                               _pick.Tus_job_no = dsMoveDet.Tables["inv_movement_cost"].Rows[l]["entry_no"].ToString();
                               _pick.Tus_job_line = 1; //Add by Chamal/Suneth 21-Jan-2015 

                               SavePickedItemSerials(_pick);



                           }
   
                           dsMoveSer.Tables["inv_movement_item_serials"].Rows.Clear();
                           #endregion
                       }
                       dsMoveDet.Tables["inv_movement_cost"].Rows.Clear();



                       }
      }

                 


                   
                     #endregion  DO                  
                  
                       Console.WriteLine("Do Successfull ");
           
                   
                ScmCommit();
                HMCCommit();
                REPCommit();
            }
              catch (Exception ex)
              {
                  HMCRollback();
                  ScmRollback();
                  REPRollback();

                  SendSMS("0773973588", "0773973588", "HMC/BDL Agent Issue " + ex.Message);
                  Send_SMTPMail("chamald@abansgroup.com","chamald@abansgroup.com","chamald@abansgroup.com","chamald@abansgroup.com","chamald@abansgroup.com", "HMC/BDL Agent Issue","PO Generation" + ex.Message);
                  Console.WriteLine("");
                  Console.WriteLine("***********************ERROR :-" + ex.Message);
              }
        }
        #endregion
        #region Other Functions

        public int GetSerialID()
        {
        //    OracleParameter[] param = new OracleParameter[1];
        //    Int32 effects = 0;
        //    OracleParameter outParameter = new OracleParameter("o_serialid", OracleDbType.Int32, null, ParameterDirection.Output);
        ////    param[0] = new OracleParameter("o_serialid", OracleDbType.Int32, null, ParameterDirection.Output);
        //       //    effects = (Int16)UpdateRecords(oConnHMC, "sp_getserialid", CommandType.StoredProcedure, param);
        //    effects =  ReturnSP_SingleValue("sp_getserialid", CommandType.StoredProcedure, outParameter, param);
        //    return effects;


            //ConnectionOpen();

            OracleParameter outParameter = new OracleParameter("o_serialid", OracleDbType.Int32, null, ParameterDirection.Output);

            int seq = ReturnSP_SingleValue("sp_getserialid", CommandType.StoredProcedure, outParameter, null);
      
            return seq;


        }

        public String GetDefaultBinCode(String _com, String _loc)
        {
            String _defBin = "";
            OracleParameter[] param = new OracleParameter[3];

            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbldefbin", "sp_getdefbin", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                foreach (DataRow tr in _dtResults.Rows)
                {
                    _defBin = (string)tr["ibl_bin_cd"];
                }
            }

            return _defBin;
        }

        public MasterItem GetItem(string _company, string _item)
        {
            MasterItem _itemList = null;
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _itemTable = QueryDataTable("tblItem", "get_allitemdetails", CommandType.StoredProcedure, false, param);
            if (_itemTable.Rows.Count > 0)
            {
                _itemList = DataTableExtensionsTolist.ToGenericList<MasterItem>(_itemTable, MasterItem.ConvertTotal)[0];
            }
            return _itemList;
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

 

            return _masterAutoNumber;
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

            effected = (Int32)UpdateRecords(oConnHMC, "sp_updateautonumber", CommandType.StoredProcedure, param);
            return (Int16)effected;
        }

        public Int16 UpdatePODocNo(Int32 _seqno, string _docno)
        {
            OracleParameter[] param = new OracleParameter[3];
            Int16 effects = 0;
            (param[0] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _seqno;
            (param[1] = new OracleParameter("p_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _docno;
            param[2] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int16)UpdateRecords(oConnHMC, "sp_update_pono", CommandType.StoredProcedure, param);
            return effects;
        }


        public int Generate_new_seq_num(string usrID, string doc_type, int direction_, string company_)
        {
            //ConnectionOpen();
            OracleParameter[] param = new OracleParameter[4];

            (param[0] = new OracleParameter("usrid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = usrID;
            (param[1] = new OracleParameter("doc_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = doc_type;
            (param[2] = new OracleParameter("direction", OracleDbType.Int32, null, ParameterDirection.Input)).Value = direction_;
            (param[3] = new OracleParameter("company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company_;
            //  param[4] = new OracleParameter("seq_number", OracleDbType.Int32, null, ParameterDirection.Output);
            OracleParameter outParameter = new OracleParameter("seq_number", OracleDbType.Int32, null, ParameterDirection.Output);
            //param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            // ReturnSP_SingleValue(String _storedProcedure, CommandType _commTypes,OracleParameter _outPara, params OracleParameter[] _parameters)
           int seq = ReturnSP_SingleValue("SP_SEQ_NUM_GENERATOR", CommandType.StoredProcedure, outParameter, param);
           // int seq = (Int16)UpdateRecords(oConnHMC, "SP_SEQ_NUM_GENERATOR", CommandType.StoredProcedure, param);
            //ConnectionClose();
            return seq;
        }
        public Int32 ReturnSP_SingleValue(String _storedProcedure, CommandType _commTypes, OracleParameter _outPara, params OracleParameter[] _parameters)
        {
            OracleCommand command = new OracleCommand();
            Int32 effected = 0;
            MakeCommand(command, oConnHMC, oTrEMS, _commTypes, _storedProcedure, false, _parameters);
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
        //public Int32 ReturnSP_SingleValue(String _storedProcedure, CommandType _commTypes, OracleParameter _outPara, params OracleParameter[] _parameters)
        //{
        //    OracleCommand command = new OracleCommand();
        //    Int32 effected = 0;
        //    UpdateRecords(command, oConnection, oTransaction, _commTypes, _storedProcedure, false, _parameters);
         
        //    OracleParameter _return = _outPara;
        //    command.Parameters.Add(_return);
        //    command.ExecuteNonQuery();

        //    if (_return.Value == DBNull.Value)
        //        effected = 0;
        //    else if (_return.Value == null)
        //        effected = 0;
        //    else
        //        effected = Convert.ToInt32(_return.Value.ToString());

        //    return (Int32)effected;
        //}

        public string get_Supplier(string Str_CustCode, string Str_CustCom)
        {
            string functionReturnValue = string.Empty ;
            OracleDataReader Ordsub = default(OracleDataReader);

            functionReturnValue = string.Empty;
            _sql = " select supplier_code from scm_supplier_company where cus_code=:p_custcd and sup_company_code=:p_com" ;
                     OracleCommand _oCom = new OracleCommand(_sql, oConnSCM) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Transaction = oTrSCM;
            _oCom.Parameters.Add(":p_custcd", OracleDbType.NVarchar2).Value = Str_CustCode;
            _oCom.Parameters.Add(":p_com", OracleDbType.NVarchar2).Value = Str_CustCom;
            Ordsub = _oCom.ExecuteReader();
            if (Ordsub.HasRows == true)
            {
                while (Ordsub.Read())
                {
                    functionReturnValue = Ordsub["supplier_code"].ToString();
                }
            }
            Ordsub.Close();
            return functionReturnValue;
        }
        public string get_Supplier_Com(string Str_CustCode, string Str_CustCom)
        {
            string functionReturnValue = string.Empty;
            OracleDataReader Ordsub = default(OracleDataReader);

            functionReturnValue = string.Empty;
            _sql = " select cus_company_code from scm_supplier_company where cus_code=:p_custcd and sup_company_code=:p_com";
            OracleCommand _oCom = new OracleCommand(_sql, oConnSCM) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Transaction = oTrSCM;
            _oCom.Parameters.Add(":p_custcd", OracleDbType.NVarchar2).Value = Str_CustCode;
            _oCom.Parameters.Add(":p_com", OracleDbType.NVarchar2).Value = Str_CustCom;
            Ordsub = _oCom.ExecuteReader();
            if (Ordsub.HasRows == true)
            {
                while (Ordsub.Read())
                {
                    functionReturnValue = Ordsub["cus_company_code"].ToString();
                }
            }
            Ordsub.Close();
            return functionReturnValue;
        }

        public Boolean get_Supplier_TaxClaim(string Str_Supode)
        {
            Boolean functionReturnValue = false;
            OracleDataReader Ordsub = default(OracleDataReader);

             
            _sql = " select  supplier_code from scm_supplier_master where supplier_code=:p_supplier_code and tax_category='VAT_C'";
            OracleCommand _oCom = new OracleCommand(_sql, oConnSCM) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Transaction = oTrSCM;
            _oCom.Parameters.Add(":p_supplier_code", OracleDbType.NVarchar2).Value = Str_Supode;
                      Ordsub = _oCom.ExecuteReader();
            if (Ordsub.HasRows == true)
            {
                functionReturnValue = true;
            }
            Ordsub.Close();
            return functionReturnValue;
        }
        public decimal  get_Supplier_TaxClaimItem(string Str_Com,string str_itm)
        {
            decimal functionReturnValue = 0;
            OracleDataReader Ordsub = default(OracleDataReader);


            _sql = " select vat_claim from  scm_tax_codes_claimbale where tax_com=:p_tax_com and item_code=:p_item_code and tax_status=1 and tax_code='VAT_C'";
            OracleCommand _oCom = new OracleCommand(_sql, oConnSCM) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Transaction = oTrSCM;
            _oCom.Parameters.Add(":p_tax_com", OracleDbType.NVarchar2).Value = Str_Com;
            _oCom.Parameters.Add(":p_item_code", OracleDbType.NVarchar2).Value = str_itm;
            Ordsub = _oCom.ExecuteReader();
            if (Ordsub.HasRows == true)
            { while (Ordsub.Read())
                {
                    functionReturnValue =Convert.ToDecimal( Ordsub["vat_claim"].ToString());
                }
            }
            Ordsub.Close();
            return functionReturnValue;
        }

        public decimal get_Supplier_ItemTax(  string str_itm)
        {
            decimal functionReturnValue = 0;
            OracleDataReader Ordsub = default(OracleDataReader);


            _sql = " SELECT TAX_RATE_CODE,TAX_RATE,ITEM_CODE FROM SCM_ITEM_MASTER WHERE  ITEM_CODE = :p_item_code AND STATUS = 'E'";
            OracleCommand _oCom = new OracleCommand(_sql, oConnSCM) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Transaction = oTrSCM;
            _oCom.Parameters.Add(":p_tax_com", OracleDbType.NVarchar2).Value = str_itm;
           
            Ordsub = _oCom.ExecuteReader();
            if (Ordsub.HasRows == true)
            {
                while (Ordsub.Read())
                {
                    functionReturnValue = Convert.ToDecimal(Ordsub["TAX_RATE"].ToString());
                }
            }
            Ordsub.Close();
            return functionReturnValue;
        }
        public string get_Com(string Str_Loc)
        {
            string functionReturnValue = string.Empty;
            OracleDataReader Ordsub = default(OracleDataReader);

            functionReturnValue = string.Empty;
            _sql = " select com_code from scm_location_master where loc_code =:p_loc_code and status='E'";
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

           public string get_Supplier (string Str_po)
        {
            string functionReturnValue = string.Empty;
            OracleDataReader Ordsub = default(OracleDataReader);

            functionReturnValue = string.Empty;
            _sql = " select poh_supp from pur_hdr where poh_doc_no=:p_poh_doc_no";
            OracleCommand _oCom = new OracleCommand(_sql, oConnHMC) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Transaction = oTrHMC;
            _oCom.Parameters.Add(":p_poh_doc_no", OracleDbType.NVarchar2).Value = Str_po;
           
            Ordsub = _oCom.ExecuteReader();
            if (Ordsub.HasRows == true)
            {
                while (Ordsub.Read())
                {
                    functionReturnValue = Ordsub["poh_supp"].ToString();
                }
            }
            Ordsub.Close();
            return functionReturnValue;
        }



           public Decimal get_Cost(string Str_po, string Str_item, string Str_sts)
        {
            Decimal functionReturnValue =0;
            OracleDataReader Ordsub = default(OracleDataReader);

            functionReturnValue =0;
            _sql = " select pod_act_unit_price from pur_det,pur_hdr where pur_hdr.poh_seq_no= pod_seq_no and  poh_doc_no=:p_poh_doc_no and pod_itm_cd=:p_pod_itm_cd and pod_itm_stus=:p_pod_itm_stus ";
            OracleCommand _oCom = new OracleCommand(_sql, oConnHMC) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Transaction = oTrHMC;
            _oCom.Parameters.Add(":p_poh_doc_no", OracleDbType.NVarchar2).Value = Str_po;
            _oCom.Parameters.Add(":p_pod_itm_cd", OracleDbType.NVarchar2).Value = Str_item;
            _oCom.Parameters.Add(":p_pod_itm_stus", OracleDbType.NVarchar2).Value = Str_sts;
           
            Ordsub = _oCom.ExecuteReader();
            if (Ordsub.HasRows == true)
            {
                while (Ordsub.Read())
                {
                    functionReturnValue =Convert.ToDecimal ( Ordsub["pod_act_unit_price"].ToString());
                }
            }
            Ordsub.Close();
            return functionReturnValue;
        }
          
        #endregion

        public Int16 SaveNewPO(PurchaseOrder _NewPO)
        {
            Int16 rows_affected = 0;
            OracleParameter[] param = new OracleParameter[40];
            (param[0] = new OracleParameter("p_poh_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _NewPO.Poh_seq_no;
            (param[1] = new OracleParameter("p_poh_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _NewPO.Poh_tp;
            (param[2] = new OracleParameter("p_poh_sub_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _NewPO.Poh_sub_tp;
            (param[3] = new OracleParameter("p_poh_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _NewPO.Poh_doc_no;
            (param[4] = new OracleParameter("p_poh_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _NewPO.Poh_com;
            (param[5] = new OracleParameter("p_poh_ope", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _NewPO.Poh_ope;
            (param[6] = new OracleParameter("p_poh_profit_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _NewPO.Poh_profit_cd;
            (param[7] = new OracleParameter("p_poh_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _NewPO.Poh_dt;
            (param[8] = new OracleParameter("p_poh_ref", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _NewPO.Poh_ref;
            (param[9] = new OracleParameter("p_poh_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _NewPO.Poh_job_no;
            (param[10] = new OracleParameter("p_poh_pay_term", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _NewPO.Poh_pay_term;
            (param[11] = new OracleParameter("p_poh_supp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _NewPO.Poh_supp;
            (param[12] = new OracleParameter("p_poh_cur_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _NewPO.Poh_cur_cd;
            (param[13] = new OracleParameter("p_poh_ex_rt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _NewPO.Poh_ex_rt;
            (param[14] = new OracleParameter("p_poh_trans_term", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _NewPO.Poh_trans_term;
            (param[15] = new OracleParameter("p_poh_port_of_orig", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _NewPO.Poh_port_of_orig;
            (param[16] = new OracleParameter("p_poh_cre_period", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _NewPO.Poh_cre_period;
            (param[17] = new OracleParameter("p_poh_frm_yer", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _NewPO.Poh_frm_yer;
            (param[18] = new OracleParameter("p_poh_frm_mon", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _NewPO.Poh_frm_mon;
            (param[19] = new OracleParameter("p_poh_to_yer", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _NewPO.Poh_to_yer;
            (param[20] = new OracleParameter("p_poh_to_mon", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _NewPO.Poh_to_mon;
            (param[21] = new OracleParameter("p_poh_preferd_eta", OracleDbType.Date, null, ParameterDirection.Input)).Value = _NewPO.Poh_preferd_eta;
            (param[22] = new OracleParameter("p_poh_contain_kit", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _NewPO.Poh_contain_kit;
            (param[23] = new OracleParameter("p_poh_sent_to_vendor", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _NewPO.Poh_sent_to_vendor;
            (param[24] = new OracleParameter("p_poh_sent_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _NewPO.Poh_sent_by;
            (param[25] = new OracleParameter("p_poh_sent_via", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _NewPO.Poh_sent_via;
            (param[26] = new OracleParameter("p_poh_sent_add", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _NewPO.Poh_sent_add;
            (param[27] = new OracleParameter("p_poh_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _NewPO.Poh_stus;
            (param[28] = new OracleParameter("p_poh_remarks", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _NewPO.Poh_remarks;
            (param[29] = new OracleParameter("p_poh_sub_tot", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _NewPO.Poh_sub_tot;
            (param[30] = new OracleParameter("p_poh_tax_tot", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _NewPO.Poh_tax_tot;
            (param[31] = new OracleParameter("p_poh_dis_rt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _NewPO.Poh_dis_rt;
            (param[32] = new OracleParameter("p_poh_dis_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _NewPO.Poh_dis_amt;
            (param[33] = new OracleParameter("p_poh_oth_tot", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _NewPO.Poh_oth_tot;
            (param[34] = new OracleParameter("p_poh_tot", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _NewPO.Poh_tot;
            (param[35] = new OracleParameter("p_poh_reprint", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _NewPO.Poh_reprint;
            (param[36] = new OracleParameter("p_poh_tax_chg", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _NewPO.Poh_tax_chg;
            (param[37] = new OracleParameter("p_poh_is_conspo", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _NewPO.poh_is_conspo;
            (param[38] = new OracleParameter("p_poh_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _NewPO.Poh_cre_by;
            param[39] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            rows_affected = (Int16)UpdateRecords(oConnHMC, "sp_savepoheader", CommandType.StoredProcedure, param);
            return rows_affected;
        }
        public Int16 SaveNewPOItem(PurchaseOrderDetail _NewPOItem)
        {
            Int16 rows_affected = 0;
            OracleParameter[] param = new OracleParameter[30];
            (param[0] = new OracleParameter("p_pod_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _NewPOItem.Pod_seq_no;
            (param[1] = new OracleParameter("p_pod_line_no", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _NewPOItem.Pod_line_no;
            (param[2] = new OracleParameter("p_pod_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _NewPOItem.Pod_itm_cd;
            (param[3] = new OracleParameter("p_pod_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _NewPOItem.Pod_itm_stus;
            (param[4] = new OracleParameter("p_pod_itm_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _NewPOItem.Pod_itm_tp;
            (param[5] = new OracleParameter("p_pod_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _NewPOItem.Pod_qty;
            (param[6] = new OracleParameter("p_pod_uom", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _NewPOItem.Pod_uom;
            (param[7] = new OracleParameter("p_pod_unit_price", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _NewPOItem.Pod_unit_price;
            (param[8] = new OracleParameter("p_pod_line_val", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _NewPOItem.Pod_line_val;
            (param[9] = new OracleParameter("p_pod_dis_rt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _NewPOItem.Pod_dis_rt;
            (param[10] = new OracleParameter("p_pod_dis_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _NewPOItem.Pod_dis_amt;
            (param[11] = new OracleParameter("p_pod_line_tax", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _NewPOItem.Pod_line_tax;
            (param[12] = new OracleParameter("p_pod_line_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _NewPOItem.Pod_line_amt;
            (param[13] = new OracleParameter("p_pod_pi_bal", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _NewPOItem.Pod_pi_bal;
            (param[14] = new OracleParameter("p_pod_lc_bal", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _NewPOItem.Pod_lc_bal;
            (param[15] = new OracleParameter("p_pod_si_bal", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _NewPOItem.Pod_si_bal;
            (param[16] = new OracleParameter("p_pod_grn_bal", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _NewPOItem.Pod_grn_bal;
            (param[17] = new OracleParameter("p_pod_ref_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _NewPOItem.Pod_ref_no;
            (param[18] = new OracleParameter("p_pod_act_unit_price", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _NewPOItem.Pod_act_unit_price;
            (param[19] = new OracleParameter("p_pod_item_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _NewPOItem.Pod_item_desc;
            (param[20] = new OracleParameter("p_pod_kit_line_no", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _NewPOItem.Pod_kit_line_no;
            (param[21] = new OracleParameter("p_pod_kit_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _NewPOItem.Pod_kit_itm_cd;
            (param[22] = new OracleParameter("p_pod_vat", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _NewPOItem.Pod_vat;
            (param[23] = new OracleParameter("p_pod_nbt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _NewPOItem.Pod_nbt;
            (param[24] = new OracleParameter("p_pod_vat_before", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _NewPOItem.Pod_vat_before;
            (param[25] = new OracleParameter("p_pod_nbt_before", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _NewPOItem.Pod_nbt_before;
            (param[26] = new OracleParameter("p_pod_tot_tax_before", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _NewPOItem.Pod_tot_tax_before;
            (param[27] = new OracleParameter("P_POD_QT_SUP", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _NewPOItem.Pod_qt_supply;
            (param[28] = new OracleParameter("P_POD_QT_PRICE", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _NewPOItem.Pod_qty_price;
            param[29] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            rows_affected = (Int16)UpdateRecords(oConnHMC, "sp_savepoitems", CommandType.StoredProcedure, param);
            return rows_affected;
        }

        public Int16 SaveNewPODel(PurchaseOrderDelivery _NewPODel)
        {
            Int16 rows_affected = 0;
            OracleParameter[] param = new OracleParameter[11];
            (param[0] = new OracleParameter("p_podi_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _NewPODel.Podi_seq_no;
            (param[1] = new OracleParameter("p_podi_line_no", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _NewPODel.Podi_line_no;
            (param[2] = new OracleParameter("p_podi_del_line_no", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _NewPODel.Podi_del_line_no;
            (param[3] = new OracleParameter("p_podi_loca", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _NewPODel.Podi_loca;
            (param[4] = new OracleParameter("p_podi_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _NewPODel.Podi_itm_cd;
            (param[5] = new OracleParameter("p_podi_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _NewPODel.Podi_itm_stus;
            (param[6] = new OracleParameter("p_podi_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _NewPODel.Podi_qty;
            (param[7] = new OracleParameter("p_podi_bal_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _NewPODel.Podi_bal_qty;
            (param[8] = new OracleParameter("p_podi_remarks", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _NewPODel.Podi_remarks;
            (param[9] = new OracleParameter("p_podi_ex_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = _NewPODel.Podi_exp_dt;
            param[10] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            rows_affected = (Int16)UpdateRecords(oConnHMC, "sp_savepodel", CommandType.StoredProcedure, param);
            return rows_affected;
        }
        public Int32 SaveSeq_to_TempPickHDR(ReptPickHeader Rph)
        {
            Int16 rows_affected = 0;
            //try
            //{
            //Create parameters and assign values.
            OracleParameter[] param = new OracleParameter[22];
            (param[0] = new OracleParameter("p_usrseq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Rph.Tuh_usrseq_no;
            (param[1] = new OracleParameter("p_usr_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Rph.Tuh_usr_id;
            (param[2] = new OracleParameter("p_usr_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Rph.Tuh_usr_com;
            (param[3] = new OracleParameter("p_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Rph.Tuh_session_id;
            (param[4] = new OracleParameter("p_cre_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = Rph.Tuh_cre_dt;
            (param[5] = new OracleParameter("p_doc_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Rph.Tuh_doc_tp;

            //direction should be 1 or 2. Not true or false
            (param[6] = new OracleParameter("p_direction", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Rph.Tuh_direct;
            (param[7] = new OracleParameter("p_ischeck_itemstatus", OracleDbType.Int32, null, ParameterDirection.Input)).Value = 1;
            (param[8] = new OracleParameter("p_ischeck_similaritem", OracleDbType.Int32, null, ParameterDirection.Input)).Value = 1;
            (param[9] = new OracleParameter("p_ischeck_reqqty", OracleDbType.Int32, null, ParameterDirection.Input)).Value = 1;
            (param[10] = new OracleParameter("p_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Rph.Tuh_doc_no;

            (param[11] = new OracleParameter("p_rec_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Rph.Tuh_rec_com;
            (param[12] = new OracleParameter("p_rec_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Rph.Tuh_rec_loc;
            (param[13] = new OracleParameter("p_isdirect", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Rph.Tuh_isdirect;
            (param[14] = new OracleParameter("p_pro_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Rph.Tuh_pro_user;
            (param[15] = new OracleParameter("p_usr_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Rph.Tuh_usr_loc;
            (param[16] = new OracleParameter("p_dir_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Rph.Tuh_dir_type;
            (param[17] = new OracleParameter("p_tuh_wh_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
            (param[18] = new OracleParameter("p_tuh_wh_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
            (param[19] = new OracleParameter("p_tuh_load_bay", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
            (param[20] = new OracleParameter("p_tuh_is_take_res", OracleDbType.Int16, null, ParameterDirection.Input)).Value = Rph.Tuh_is_take_res == true ? 1 : 0;
            param[21] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);


            rows_affected = (Int16)UpdateRecords(oConnREP, "sp_pickhdr", CommandType.StoredProcedure, param);

            //}
            //catch (Exception e)
            //{
            //    rows_affected = -1;
            //    throw new Exception(e.Message);
            //}
            //finally
            //{
            //    ConnectionClose();
            //}

            return rows_affected;
        }
        public Int16 SavePickedItemSerials(ReptPickSerials _scanserNew)
        {
            Int16 rows_affected = 0;
            OracleParameter[] param = new OracleParameter[48];
            (param[0] = new OracleParameter("p_usrseq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_usrseq_no;
            (param[1] = new OracleParameter("p_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_doc_no;
            (param[2] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_seq_no;
            (param[3] = new OracleParameter("p_itm_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_line;
            (param[4] = new OracleParameter("p_batch_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_batch_line;
            (param[5] = new OracleParameter("p_ser_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_line;
            (param[6] = new OracleParameter("p_doc_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_doc_dt;
            (param[7] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_com;
            (param[8] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_loc;
            (param[9] = new OracleParameter("p_bin", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_bin;
            (param[10] = new OracleParameter("p_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_cd;
            (param[11] = new OracleParameter("p_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_stus;
            (param[12] = new OracleParameter("p_unit_cost", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanserNew.Tus_unit_cost;
            (param[13] = new OracleParameter("p_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanserNew.Tus_qty;
            (param[14] = new OracleParameter("p_ser_id", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_id;
            (param[15] = new OracleParameter("p_ser_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_1;
            (param[16] = new OracleParameter("p_ser_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_2;
            (param[17] = new OracleParameter("p_ser_3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_3;
            (param[18] = new OracleParameter("p_ser_4", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_4;
            (param[19] = new OracleParameter("p_warr_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_warr_no;
            (param[20] = new OracleParameter("p_warr_period", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_warr_period;
            (param[21] = new OracleParameter("p_orig_grncom ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_orig_grncom;
            (param[22] = new OracleParameter("p_orig_grnno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_orig_grnno;
            (param[23] = new OracleParameter("p_orig_grndt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_orig_grndt;
            (param[24] = new OracleParameter("p_orig_supp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_orig_supp;
            (param[25] = new OracleParameter("p_exist_grncom", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_exist_grncom;
            (param[26] = new OracleParameter("p_exist_grnno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_exist_grnno;
            (param[27] = new OracleParameter("p_exist_grndt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_exist_grndt;
            (param[28] = new OracleParameter("p_exist_supp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_exist_supp;
            (param[29] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_cre_by;
            (param[30] = new OracleParameter("p_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_session_id;
            (param[31] = new OracleParameter("p_unit_price", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanserNew.Tus_unit_price;
            (param[32] = new OracleParameter("p_new_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_new_status;
            (param[33] = new OracleParameter("p_base_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_base_doc_no;
            (param[34] = new OracleParameter("p_base_itm_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_base_itm_line;
            (param[35] = new OracleParameter("p_itm_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_desc;
            (param[36] = new OracleParameter("p_itm_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_model;
            (param[37] = new OracleParameter("p_itm_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_brand;
            (param[38] = new OracleParameter("p_ser_remarks", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_new_remarks;
            (param[39] = new OracleParameter("p_resqty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanserNew.Tus_resqty;
            (param[40] = new OracleParameter("p_ageloc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ageloc;
            (param[41] = new OracleParameter("p_ageloc_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ageloc_dt.Date;
            (param[42] = new OracleParameter("p_isownmrn", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_isownmrn;

            (param[43] = new OracleParameter("p_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_job_no;//Chamal 20-Jan-2015
            (param[44] = new OracleParameter("p_job_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_job_line;//Chamal 20-Jan-2015
            (param[45] = new OracleParameter("p_res_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_res_no;//Chamal 20-Jan-2015
            (param[46] = new OracleParameter("p_res_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_res_line;//Chamal 20-Jan-2015

            param[47] = new OracleParameter("o_effect", OracleDbType.Int16, null, ParameterDirection.Output);


            rows_affected = (Int16)UpdateRecords(oConnREP, "sp_picksernew", CommandType.StoredProcedure, param);
            return rows_affected;
        }
        public void SendSMS(string _SENDER, string _SENDERPHNO, string _MSG)
        {    OracleDataReader Ordsub = default(OracleDataReader);
           int  _ref = 0;
           OpenSCM();
             _sql = " select MSG from SCM_MSG_OUT where MSG =:MSG and SENDERPHNO=:SENDERPHNO";
            OracleCommand _oCom1 = new OracleCommand(_sql, oConnSCM) { CommandType = CommandType.Text, BindByName = true };
            _oCom1.Transaction = oTrSCM;
            _oCom1.Parameters.Add(":MSG", OracleDbType.NVarchar2).Value = _MSG;
            _oCom1.Parameters.Add(":SENDERPHNO", OracleDbType.NVarchar2).Value = _SENDERPHNO;
            Ordsub = _oCom1.ExecuteReader();
            if (Ordsub.HasRows == false)
            { 
            _sql = "INSERT INTO SCM_MSG_OUT(SENDER,SENDERPHNO,RECEIVER,RECEIVERPHNO,MSG,MSGID,MSGSTATUS,REFDOCNO,CREATETIME) VALUES " +
            "(:SENDER,:SENDERPHNO,'SCM2','+9477777777',:MSG,'ERR',0,'ERR',CURRENT_DATE)";
            OracleCommand _oCom = new OracleCommand(_sql, oConnSCM) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Parameters.Add(":SENDER", OracleDbType.NVarchar2).Value = _SENDER;
            _oCom.Parameters.Add(":SENDERPHNO", OracleDbType.NVarchar2).Value = _SENDERPHNO;
            _oCom.Parameters.Add(":MSG", OracleDbType.NVarchar2).Value = _MSG;
            _ref = _oCom.ExecuteNonQuery();
        }

        }
        public void Send_SMTPMail(string _recipientEmailAddress, string _superior_mail1,string _superior_mail2,string _superior_mail3,string _superior_mail4, string _subject, string _message)
        {
            OracleDataReader Ordsub = default(OracleDataReader);
                   OpenSCM();
                   _sql = " select M_DES from SCM_MAIL_LOG where M_DES =:M_DES and M_TYPE=:M_TYPE";
            OracleCommand _oCom1 = new OracleCommand(_sql, oConnSCM) { CommandType = CommandType.Text, BindByName = true };
            _oCom1.Transaction = oTrSCM;
            _oCom1.Parameters.Add(":M_DES", OracleDbType.NVarchar2).Value = _message;
            _oCom1.Parameters.Add(":M_TYPE", OracleDbType.NVarchar2).Value = "BIGDEAL";
            Ordsub = _oCom1.ExecuteReader();
            if (Ordsub.HasRows == false)
            {

                _sql = "INSERT INTO SCM_MAIL_LOG(M_DATE,M_EMAL,M_DES,M_CRE_DATE,M_TYPE) VALUES " +
               "(:M_DATE,:M_EMAL,:M_DES,CURRENT_DATE,:M_TYPE)";
                OracleCommand _oCom = new OracleCommand(_sql, oConnSCM) { CommandType = CommandType.Text, BindByName = true };
                _oCom.Parameters.Add(":M_DATE", OracleDbType.Date ).Value = DateTime.Today;
                _oCom.Parameters.Add(":M_EMAL", OracleDbType.NVarchar2).Value = _recipientEmailAddress;
                _oCom.Parameters.Add(":M_DES", OracleDbType.NVarchar2).Value = _message;
                _oCom.Parameters.Add(":M_TYPE", OracleDbType.NVarchar2).Value = "BIGDEAL";
                 _oCom.ExecuteNonQuery();



                _sql = "INSERT INTO SCM_MAIL_LOG(M_DATE,M_EMAL,M_DES,M_CRE_DATE,M_TYPE) VALUES " +
               "(:M_DATE,:M_EMAL,:M_DES,CURRENT_DATE,:M_TYPE)";
                _oCom = new OracleCommand(_sql, oConnSCM) { CommandType = CommandType.Text, BindByName = true };
                _oCom.Parameters.Add(":M_DATE", OracleDbType.Date).Value = DateTime.Today;
                _oCom.Parameters.Add(":M_EMAL", OracleDbType.NVarchar2).Value = _superior_mail1;
                _oCom.Parameters.Add(":M_DES", OracleDbType.NVarchar2).Value = _message;
                _oCom.Parameters.Add(":M_TYPE", OracleDbType.NVarchar2).Value = "BIGDEAL";
               _oCom.ExecuteNonQuery();


                _sql = "INSERT INTO SCM_MAIL_LOG(M_DATE,M_EMAL,M_DES,M_CRE_DATE,M_TYPE) VALUES " +
               "(:M_DATE,:M_EMAL,:M_DES,CURRENT_DATE,:M_TYPE)";
                _oCom = new OracleCommand(_sql, oConnSCM) { CommandType = CommandType.Text, BindByName = true };
                _oCom.Parameters.Add(":M_DATE", OracleDbType.Date).Value = DateTime.Today;
                _oCom.Parameters.Add(":M_EMAL", OracleDbType.NVarchar2).Value = _superior_mail2;
                _oCom.Parameters.Add(":M_DES", OracleDbType.NVarchar2).Value = _message;
                _oCom.Parameters.Add(":M_TYPE", OracleDbType.NVarchar2).Value = "BIGDEAL";
                _oCom.ExecuteNonQuery();



                _sql = "INSERT INTO SCM_MAIL_LOG(M_DATE,M_EMAL,M_DES,M_CRE_DATE,M_TYPE) VALUES " +
               "(:M_DATE,:M_EMAL,:M_DES,CURRENT_DATE,:M_TYPE)";
                _oCom = new OracleCommand(_sql, oConnSCM) { CommandType = CommandType.Text, BindByName = true };
                _oCom.Parameters.Add(":M_DATE", OracleDbType.Date).Value = DateTime.Today;
                _oCom.Parameters.Add(":M_EMAL", OracleDbType.NVarchar2).Value = _superior_mail3;
                _oCom.Parameters.Add(":M_DES", OracleDbType.NVarchar2).Value = _message;
                _oCom.Parameters.Add(":M_TYPE", OracleDbType.NVarchar2).Value = "BIGDEAL";
               _oCom.ExecuteNonQuery();



                _sql = "INSERT INTO SCM_MAIL_LOG(M_DATE,M_EMAL,M_DES,M_CRE_DATE,M_TYPE) VALUES " +
               "(:M_DATE,:M_EMAL,:M_DES,CURRENT_DATE,:M_TYPE)";
                  _oCom = new OracleCommand(_sql, oConnSCM) { CommandType = CommandType.Text, BindByName = true };
                  _oCom.Parameters.Add(":M_DATE", OracleDbType.Date).Value = DateTime.Today;
                _oCom.Parameters.Add(":M_EMAL", OracleDbType.NVarchar2).Value = _superior_mail4;
                _oCom.Parameters.Add(":M_DES", OracleDbType.NVarchar2).Value = _message;
                _oCom.Parameters.Add(":M_TYPE", OracleDbType.NVarchar2).Value = "BIGDEAL";
                _oCom.ExecuteNonQuery();






                SmtpClient smtpClient = new SmtpClient();
                MailMessage message = new MailMessage();

                MailAddress _senderEmailAddress = new MailAddress("SCM@abansgroup.com", "SCM");

                smtpClient.Host = System.Configuration.ConfigurationManager.ConnectionStrings["MailHost"].ConnectionString;
                smtpClient.Port = 25;
                message.From = _senderEmailAddress;

                //string _email = _generalDAL.GetMailFooterMsg();

                message.To.Add(_recipientEmailAddress);
                message.Subject = _subject;
                message.CC.Add(new MailAddress(_superior_mail1));
                message.CC.Add(new MailAddress(_superior_mail2));
                message.CC.Add(new MailAddress(_superior_mail3));
              //  message.CC.Add(new MailAddress(_superior_mail4));
                //message.Bcc.Add(new MailAddress(""));
                message.IsBodyHtml = false;
                message.Body = _message;
                message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;

                // Send SMTP mail
                smtpClient.Send(message);
            }
        }
    }
}
