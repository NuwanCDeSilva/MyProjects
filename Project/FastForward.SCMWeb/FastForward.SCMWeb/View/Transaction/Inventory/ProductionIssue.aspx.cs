using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using FF.BusinessObjects.InventoryNew;
using FF.BusinessObjects.Sales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Inventory
{
    public partial class ProductionIssue : Base
    {
        protected List<ReptPickSerials> serial_list { get { return (List<ReptPickSerials>)Session["serial_list"]; } set { Session["serial_list"] = value; } }
        protected List<ReptPickSerials> minusserial_list { get { return (List<ReptPickSerials>)Session["minusserial_list"]; } set { Session["minusserial_list"] = value; } }
        protected List<ProductionFinGood> _ProductionFinGood { get { return (List<ProductionFinGood>)Session["_ProductionFinGood"]; } set { Session["_ProductionFinGood"] = value; } }
        protected List<SatProjectDetails> _SatProjectDetails { get { return (List<SatProjectDetails>)Session["_SatProjectDetails"]; } set { Session["_SatProjectDetails"] = value; } }
      
        protected List<InventoryRequest> _InventoryRequest { get { return (List<InventoryRequest>)Session["_InventoryRequest"]; } set { Session["_InventoryRequest"] = value; } }
        protected List<InventoryRequestItem> _InventoryRequestItem { get { return (List<InventoryRequestItem>)Session["_InventoryRequestItem"]; } set { Session["_InventoryRequestItem"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null || Session["UserCompanyCode"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                PageClear();
                
            }
        }
        private void PageClear()
        {
            DateTime orddate = DateTime.Now;
            serial_list = new List<ReptPickSerials>();
            minusserial_list = new List<ReptPickSerials>();
            _InventoryRequest = new List<InventoryRequest>();
            _InventoryRequestItem = new List<InventoryRequestItem>();
            _SatProjectDetails = new List<SatProjectDetails>();
            _ProductionFinGood = new List<ProductionFinGood>();
            txtfrom.Text = orddate.ToString("dd/MMM/yyyy");
            txtto.Text = orddate.ToString("dd/MMM/yyyy");
            txtpDate.Text = orddate.ToString("dd/MMM/yyyy");
            txtfloc.Text = Session["UserDefLoca"].ToString();
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
            DataTable result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, "Code", txtfloc.Text);
            if (result.Rows.Count > 0)
            {
                txtfloc.ToolTip = result.Rows[0][1].ToString();
            }
           
            txttoloc.Text = string.Empty;
            txtcom.Text = Session["UserCompanyCode"].ToString();
            txtreq.Text = string.Empty;
            txtjob.Text = string.Empty;
            txtmitem.Text = string.Empty;
            txtvehi.Text = string.Empty;
            txtremark.Text = string.Empty;
            txtpqty.Text = "0";
            txtIqty.Text = "0";
            txtprefix.Text = string.Empty;
            txtser1.Text = string.Empty;
            txtser2.Text = string.Empty;
            grdItem.DataSource = new int[] { };
            grdItem.DataBind();
            grdpendinreq.DataSource = new int[] { };
            grdpendinreq.DataBind();
            grdserial.DataSource = new int[] { };
            grdserial.DataBind();
            
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            InventoryRequest _obj = new InventoryRequest();
            _obj.Itr_com = Session["UserCompanyCode"].ToString();
            _obj.Itr_sub_tp = "PRO";
            _obj.Itr_dt = Convert.ToDateTime(txtfrom.Text);
            _obj.Itr_exp_dt = Convert.ToDateTime(txtto.Text);
            _obj.Itr_loc = txtfloc.Text.ToString();
            _InventoryRequest = CHNLSVC.Inventory.GET_INT_REQPRISSUE(_obj);
            if (_InventoryRequest != null)
            {
                grdpendinreq.DataSource = _InventoryRequest;
                grdpendinreq.DataBind();
            }
        }
        protected void lbtnselect_Click(object sender, EventArgs e)
        {
          
            if (grdpendinreq.Rows.Count == 0) return;
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                string doc = (row.FindControl("col_itr_job_no") as Label).Text;
                string mrn = (row.FindControl("col_itr_req_no") as Label).Text;

                txtreq.Text = mrn;
                txtjob.Text = doc;

                
                 _ProductionFinGood = CHNLSVC.Sales.GETFINGOD(doc);
                 _SatProjectDetails = CHNLSVC.Sales.GETBOQDETAILS(doc);
                 if (_ProductionFinGood != null)
                 {
                     if (_ProductionFinGood.Count > 0)
                     {
                         txtmitem.Text = _ProductionFinGood[0].SPF_ITM;
                         txtpqty.Text = _ProductionFinGood[0].SPF_BQTY.ToString();
                         txtIqty.Text = _ProductionFinGood[0].SPF_BQTY.ToString();

                         if (_SatProjectDetails != null)
                         {
                             foreach (var _list in _SatProjectDetails)
                             {
                                 if (_list.SPD_ITM == "ELC001028")
                                 {
                                     string a = "a";
                                 }
                               _list.SPD_EST_QTY= _list.SPD_EST_QTY*_ProductionFinGood[0].SPF_BQTY/_ProductionFinGood[0].SPF_QTY;
                             }
                         }
                        
                         grdItem.DataSource = _SatProjectDetails;
                         grdItem.DataBind();
                     }
                     else
                     {
                         grdItem.DataSource = _SatProjectDetails;
                         grdItem.DataBind();
                     }
                 }


            }
        }
        protected void llbtnclearserial_Click(object sender, EventArgs e)
        {
            serial_list = new List<ReptPickSerials>();
            grdserial.DataSource = serial_list;
            grdserial.DataBind();
        }
        protected void lbtnadserial_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtmitem.Text))
            {
                DisplayMessage("Please select the job number", 1);
                txtmitem.Focus();
                return;
            }

            //added by Wimal @ 10/08/2018 to add decimal items 
            MasterItem _mstItem = new MasterItem();
            _mstItem = CHNLSVC.General.GetItemMaster(txtmitem.Text);
            if (_mstItem.Mi_is_ser1 != 1)
            {
                int j = Convert.ToInt32(txtser2.Text);
                ////Check Finishgood balance already inline
                //List<InventoryBatchRefN> dt = CHNLSVC.Inventory.getItemBalanceQtyWithJobNo(Session["UserCompanyCode"].ToString(), (txtfloc.Text), txtmitem.Text);
                //if (dt.Count > 0 )
                //{
                //    DisplayMessage("Production line contains balance of " + txtmitem.Text, 1);
                //    txtmitem.Focus();
                //    return;                
                //}

                if (string.IsNullOrEmpty(txttoloc.Text))
                {
                    DisplayMessage("Please enter the To location ", 1);
                    txtmitem.Focus();
                    return;
                }
                if (j <= 0)
                {
                    DisplayMessage("Cannot add zero or minus qty", 1);
                    txtser1.Focus();
                    return;
                }
                if (serial_list.Count > 0)
                {
                    DisplayMessage("Already serial has been added", 1);
                    txtser1.Focus();
                    return;
                }

                txtprefix.Text = "N/A";
                txtser1.Text = "0";

                List<mst_itm_fg_cost> _mstcostlist = new List<mst_itm_fg_cost>();
                _mstcostlist = CHNLSVC.General.GetFinishGood(txtmitem.Text);

                decimal _unitCost = 0;
                if (_mstcostlist != null)
                {
                    _unitCost = _mstcostlist[0].Ifc_cost_amount;
                }               

                #region Fill Pick Serial Object
                ReptPickSerials _inputReptPickSerials = new ReptPickSerials();
                //// _inputReptPickSerials.Tus_usrseq_no = _userSeqNo;
                _inputReptPickSerials.Tus_doc_no = txtjob.Text;
                _inputReptPickSerials.Tus_base_doc_no = txtjob.Text;
                _inputReptPickSerials.Tus_base_itm_line = 1;
                _inputReptPickSerials.Tus_seq_no = 0;
                _inputReptPickSerials.Tus_itm_line = 1;
                // _inputReptPickSerials.Tus_batch_line = 0;
                _inputReptPickSerials.Tus_ser_line = 1;
                _inputReptPickSerials.Tus_doc_dt = DateTime.Now.Date;
                _inputReptPickSerials.Tus_orig_grndt = DateTime.Now.Date;
                _inputReptPickSerials.Tus_com = Session["UserCompanyCode"].ToString();
                _inputReptPickSerials.Tus_loc = txttoloc.Text;
                _inputReptPickSerials.Tus_bin = Session["GlbDefaultBin"].ToString();
                _inputReptPickSerials.Tus_itm_cd = txtmitem.Text;
                _inputReptPickSerials.Tus_itm_stus = "GOD";
                _inputReptPickSerials.Tus_itm_stus_Desc = "GOOD";
                _inputReptPickSerials.Tus_unit_cost = _unitCost;
                _inputReptPickSerials.Tus_unit_price = _unitCost;
                // if (Session["_itemSerializedStatus"].ToString() == "0")
                // {
                //   _inputReptPickSerials.Tus_qty = Convert.ToDecimal(txtqty.Text);
                // }
                // else
                // {
                _inputReptPickSerials.Tus_qty = (j);
                // }


                // if (_serID > 0)
                // { _inputReptPickSerials.Tus_ser_id = _serID; }
                // else
                // { 
                _inputReptPickSerials.Tus_ser_id = 0;
                _inputReptPickSerials.Tus_ser_1 = "N/A";
                // _inputReptPickSerials.Tus_ser_2 = _serialNo2;
                // _inputReptPickSerials.Tus_ser_3 = _serialNo3;
                // if (string.IsNullOrEmpty(_warrantyno))
                //  string _warrantyno = String.Format("{0:dd}", DateTime.Now.Date) + String.Format("{0:MM}", DateTime.Now.Date) + String.Format("{0:yy}", DateTime.Now.Date) + "-" + Session["UserDefLoca"].ToString() + "-" + _userwarrid + "-" + _inputReptPickSerials.Tus_ser_id.ToString();
                // _inputReptPickSerials.Tus_warr_no = _warrantyno;
                // _inputReptPickSerials.Tus_itm_desc = txtItemDes.Text;
                // _inputReptPickSerials.Tus_itm_model = txtModel.Text;
                // _inputReptPickSerials.Tus_itm_brand = txtBrand.Text;
                // _inputReptPickSerials.Tus_itm_line = ItemLineNo;
                // _inputReptPickSerials.Tus_cre_by = Session["UserID"].ToString();
                // _inputReptPickSerials.Tus_cre_dt = DateTime.Now.Date;
                // _inputReptPickSerials.Tus_session_id = Session["SessionID"].ToString();
                // _inputReptPickSerials.Tus_itm_desc = txtItemDes.Text;
                // _inputReptPickSerials.Tus_itm_model = txtModel.Text;
                // _inputReptPickSerials.Tus_itm_brand = txtBrand.Text;

                // string _baseItem = Session["baseitem"].ToString();
                // if (string.IsNullOrEmpty(_baseItem))
                // {
                //     _baseItem = PopupItemCode;
                // }
                // _inputReptPickSerials.Tus_new_itm_cd = _baseItem;

                // //_inputReptPickSerials.Tus_base_doc_no = Session["baseitem"].ToString();
                // _inputReptPickSerials.Tus_job_line = ItemLineNo;
                // _inputReptPickSerials.Tus_job_no = txtPORefNo.Text;

                serial_list.Add(_inputReptPickSerials);
                #endregion

            }
            else
            {
                if (string.IsNullOrEmpty(txtprefix.Text))
                {
                    DisplayMessage("Please enter the prefix", 1);
                    txtmitem.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtser1.Text))
                {
                    DisplayMessage("Please enter the From serial #", 1);
                    txtmitem.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtser2.Text))
                {
                    DisplayMessage("Please enter the To serial #", 1);
                    txtmitem.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txttoloc.Text))
                {
                    DisplayMessage("Please enter the To location ", 1);
                    txtmitem.Focus();
                    return;
                }
                int i = 0;
                i = Convert.ToInt32(txtser1.Text);

                int j = Convert.ToInt32(txtser2.Text);
                int linrno = 1;

                if (i <= 0)
                {
                    DisplayMessage("Cannot add zero or minus qty", 1);
                    txtser1.Focus();
                    return;
                }
                if (j <= 0)
                {
                    DisplayMessage("Cannot add zero or minus qty", 1);
                    txtser1.Focus();
                    return;
                }
                if (serial_list.Count > 0)
                {
                    DisplayMessage("Already serial has been added", 1);
                    txtser1.Focus();
                    return;
                }
                if (j < i)
                {
                    DisplayMessage("From quantity cannot exceed to quantity !", 1);
                    txtser2.Focus();
                    return;
                }

                List<mst_itm_fg_cost> _mstcostlist = new List<mst_itm_fg_cost>();
                _mstcostlist = CHNLSVC.General.GetFinishGood(txtmitem.Text);

                decimal _unitCost = 0;
                if (_mstcostlist != null)
                {
                    _unitCost = _mstcostlist[0].Ifc_cost_amount;
                }
            //}          

            ////added by Wimal @ 10/08/2018 to add decimal items 
            //if (_mstItem.Mi_is_ser1!=1)
            //{
              
            //}
            //else
            //{
            while (i <= j)
            {
                string serial = "";
                ReptPickSerials _inputReptPickSerials = new ReptPickSerials();

                int serlenth = i.ToString().Length;
                if (serlenth==5)
                {
                     serial = txtprefix.Text + i.ToString();
                }
                else
                {
                    int deflenth = 5 - serlenth;
                    for (int k = 0; k < deflenth;k++ )
                    {
                        serial = serial + "0";
                    }
                    serial =txtprefix.Text+ serial  + i.ToString();
                }

                //if()
                //{

                //}

               
                #region Fill Pick Serial Object
                //// _inputReptPickSerials.Tus_usrseq_no = _userSeqNo;
                _inputReptPickSerials.Tus_doc_no = txtjob.Text;
                _inputReptPickSerials.Tus_base_doc_no = txtjob.Text;
                _inputReptPickSerials.Tus_base_itm_line = linrno;
                _inputReptPickSerials.Tus_seq_no = 0;
                _inputReptPickSerials.Tus_itm_line = linrno;
                // _inputReptPickSerials.Tus_batch_line = 0;
                 _inputReptPickSerials.Tus_ser_line = i;
                 _inputReptPickSerials.Tus_doc_dt = DateTime.Now.Date;
                 _inputReptPickSerials.Tus_orig_grndt = DateTime.Now.Date;
                 _inputReptPickSerials.Tus_com = Session["UserCompanyCode"].ToString();
                 _inputReptPickSerials.Tus_loc = txttoloc.Text;
                 _inputReptPickSerials.Tus_bin = Session["GlbDefaultBin"].ToString();
                 _inputReptPickSerials.Tus_itm_cd = txtmitem.Text;
                 _inputReptPickSerials.Tus_itm_stus = "GOD";
                 _inputReptPickSerials.Tus_itm_stus_Desc = "GOOD";
                 _inputReptPickSerials.Tus_unit_cost = _unitCost;
                 _inputReptPickSerials.Tus_unit_price = _unitCost;
                // if (Session["_itemSerializedStatus"].ToString() == "0")
                // {
                  //   _inputReptPickSerials.Tus_qty = Convert.ToDecimal(txtqty.Text);
                // }
                // else
                // {
                     _inputReptPickSerials.Tus_qty = 1;
                // }


                // if (_serID > 0)
                // { _inputReptPickSerials.Tus_ser_id = _serID; }
                // else
                // { 
                _inputReptPickSerials.Tus_ser_id = CHNLSVC.Inventory.GetSerialID();
                _inputReptPickSerials.Tus_ser_1 = serial;
                // _inputReptPickSerials.Tus_ser_2 = _serialNo2;
                // _inputReptPickSerials.Tus_ser_3 = _serialNo3;
                // if (string.IsNullOrEmpty(_warrantyno))
                  //  string _warrantyno = String.Format("{0:dd}", DateTime.Now.Date) + String.Format("{0:MM}", DateTime.Now.Date) + String.Format("{0:yy}", DateTime.Now.Date) + "-" + Session["UserDefLoca"].ToString() + "-" + _userwarrid + "-" + _inputReptPickSerials.Tus_ser_id.ToString();
                // _inputReptPickSerials.Tus_warr_no = _warrantyno;
                // _inputReptPickSerials.Tus_itm_desc = txtItemDes.Text;
                // _inputReptPickSerials.Tus_itm_model = txtModel.Text;
                // _inputReptPickSerials.Tus_itm_brand = txtBrand.Text;
                // _inputReptPickSerials.Tus_itm_line = ItemLineNo;
                // _inputReptPickSerials.Tus_cre_by = Session["UserID"].ToString();
                // _inputReptPickSerials.Tus_cre_dt = DateTime.Now.Date;
                // _inputReptPickSerials.Tus_session_id = Session["SessionID"].ToString();
                // _inputReptPickSerials.Tus_itm_desc = txtItemDes.Text;
                // _inputReptPickSerials.Tus_itm_model = txtModel.Text;
                // _inputReptPickSerials.Tus_itm_brand = txtBrand.Text;

                // string _baseItem = Session["baseitem"].ToString();
                // if (string.IsNullOrEmpty(_baseItem))
                // {
                //     _baseItem = PopupItemCode;
                // }
                // _inputReptPickSerials.Tus_new_itm_cd = _baseItem;

                // //_inputReptPickSerials.Tus_base_doc_no = Session["baseitem"].ToString();
                // _inputReptPickSerials.Tus_job_line = ItemLineNo;
                // _inputReptPickSerials.Tus_job_no = txtPORefNo.Text;

                 serial_list.Add(_inputReptPickSerials);
                 linrno++;
                 i++;
                #endregion

            }
        }
            if (serial_list.Count>0)
            {
                string _err = CHNLSVC.Inventory.CheackSerialIsAvailableInCompany(serial_list);
                if (!string.IsNullOrEmpty(_err))
                {
                    serial_list = new List<ReptPickSerials>();
                    DisplayMessage(_err, 1); 
                }
            }
            grdserial.DataSource = serial_list;
            grdserial.DataBind();
           
        }
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            
            if (txtSavelconformmessageValue.Value == "No")
            {
                return;
            }
          
            if (serial_list == null)
            {
                DisplayMessage("Please add the serials", 1);
                txtmitem.Focus();
                return;
            }
            if (serial_list.Count == 0)
            {
                DisplayMessage("Please add the serials", 1);
                txtmitem.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtfrom.Text))
            {
                DisplayMessage("Please enter the From location", 1);
                txtmitem.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txttoloc.Text))
            {
                DisplayMessage("Please enter the To location", 1);
                txtmitem.Focus();
                return;
            }
            save();
        }
        protected void lbtnaddqty_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtmitem.Text))
            {
                DisplayMessage("Please select the pending MRN number ! ", 1);
                txtmitem.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtpqty.Text))
            {
                DisplayMessage("Please enter the issue qty", 1);
                txtmitem.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtIqty.Text))
            {
                DisplayMessage("Please enter the issue qty", 1);
                txtmitem.Focus();
                return;
            }
            decimal fqty = Convert.ToDecimal(txtpqty.Text);
            decimal tqty = Convert.ToDecimal(txtIqty.Text);


            if (fqty <= 0)
            {
                DisplayMessage("Cannot add zero or minus qty", 1);
                txtpqty.Focus();
                return;
            }
            if (tqty <= 0)
            {
                DisplayMessage("Cannot add zero or minus qty", 1);
                txtIqty.Focus();
                return;
            }
            if (tqty > fqty)
            {
                DisplayMessage("Cannot add more than the production qty !", 1);
                txtIqty.Focus();
                return;
            }
            if (_SatProjectDetails != null)
            {
                if (_SatProjectDetails.Count > 0) 
                {
                    foreach (SatProjectDetails _pro in _SatProjectDetails)
                    {
                        if (_pro.SPD_ITM == "ELC001028")
                        {
                            string a = "a";
                        }
                        decimal unitval = 0;
                        DataTable unit = CHNLSVC.Inventory.GetComponetUnit(txtmitem.Text.ToString().Trim(), _pro.SPD_ITM);
                        if (unit != null)
                        {
                            if (unit.Rows.Count >0)
                            {
                                unitval = Convert.ToDecimal(unit.Rows[0][0].ToString());
                            }
                            else
                            {
                                DisplayMessage("Please Setup Kit Component:" + _pro.SPD_ITM + " And Finish good:" + txtmitem.Text.ToString().Trim(), 1);
                                return;
                            }
                        }
                      //  decimal _qty = _pro.SPD_EST_QTY / Convert.ToDecimal(txtIqty.Text);
                      //  _pro.SPD_EST_QTY = Convert.ToDecimal(txtpqty.Text) / _qty;
                        _pro.SPD_EST_QTY = Convert.ToDecimal(txtIqty.Text) * unitval;
                    }
                    grdItem.DataSource = _SatProjectDetails;
                    grdItem.DataBind();
                }
                else
                {
                    DisplayMessage("PLease select the job #", 1);

                    return;
                }
               
            }
            else
            {
                DisplayMessage("PLease select the job #", 1);
                
                return;
            }
        }
        private void save()
        {
            
            InventoryHeader inHeader = new InventoryHeader();
            #region Fill InventoryHeader
            DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
            foreach (DataRow r in dt_location.Rows)
            {
                // Get the value of the wanted column and cast it to string
                inHeader.Ith_sbu = (string)r["ML_OPE_CD"];
                if (System.DBNull.Value != r["ML_CATE_2"])
                {
                    inHeader.Ith_channel = (string)r["ML_CATE_2"];
                }
                else
                {
                    inHeader.Ith_channel = string.Empty;
                }
            }
            inHeader.Ith_acc_no = "STOCK_ADJ";
            inHeader.Ith_anal_1 = "";

            inHeader.Ith_anal_3 = "";
            inHeader.Ith_anal_4 = "";
            inHeader.Ith_anal_5 = "";
            inHeader.Ith_anal_6 = 0;
            inHeader.Ith_anal_7 = 0;
            inHeader.Ith_anal_8 = DateTime.MinValue;
            inHeader.Ith_anal_9 = DateTime.MinValue;
          
            inHeader.Ith_anal_10 = true;
            inHeader.Ith_anal_2 = txtjob.Text;
          


            inHeader.Ith_anal_11 = false;
            inHeader.Ith_anal_12 = false;
            inHeader.Ith_bus_entity = "";
            inHeader.Ith_cate_tp = "PRO";
            inHeader.Ith_com = Session["UserCompanyCode"].ToString();
            inHeader.Ith_com_docno = "";
            inHeader.Ith_cre_by = Session["UserID"].ToString();
            inHeader.Ith_cre_when = DateTime.Now;
            inHeader.Ith_del_add1 = "";// txtDAdd1.Text.Trim();
            inHeader.Ith_del_add2 = "";// txtDAdd2.Text.Trim();
            inHeader.Ith_del_code = "";
            inHeader.Ith_del_party = "";
            inHeader.Ith_del_town = "";

            inHeader.Ith_doc_date = Convert.ToDateTime(txtpDate.Text).Date;
            inHeader.Ith_doc_no = string.Empty;
            inHeader.Ith_doc_tp = "ADJ";
            inHeader.Ith_doc_year = Convert.ToDateTime(txtpDate.Text).Year;
           
            inHeader.Ith_entry_no = txtjob.Text;
            
           // inHeader.Ith_entry_tp = txtSubType.Text.ToString().Trim();
            inHeader.Ith_git_close = true;
            inHeader.Ith_git_close_date = DateTime.MinValue;
            inHeader.Ith_git_close_doc = string.Empty;
            inHeader.Ith_isprinted = false;
            inHeader.Ith_is_manual = false;
            inHeader.Ith_job_no = string.Empty;
            inHeader.Ith_loading_point = string.Empty;
            inHeader.Ith_loading_user = string.Empty;
            inHeader.Ith_loc = Session["UserDefLoca"].ToString();
            inHeader.Ith_manual_ref = txtreq.Text.Trim();
            inHeader.Ith_mod_by = Session["UserID"].ToString();
            inHeader.Ith_mod_when = DateTime.Now;
            inHeader.Ith_noofcopies = 0;
            inHeader.Ith_oth_loc = string.Empty;
            inHeader.Ith_oth_docno = txtjob.Text.Trim();
           // inHeader.Ith_remarks = txtRemarks.Text;
            //inHeader.Ith_seq_no = 6; removed by Chamal 12-05-2013
            inHeader.Ith_session_id = Session["SessionID"].ToString();
            inHeader.Ith_stus = "A";
            inHeader.Ith_sub_tp = "PRO";
            inHeader.Ith_vehi_no = string.Empty;
            inHeader.Ith_anal_3 = "";//ddlDeliver.SelectedItem.Text;
            #endregion
            string _prodno = "";
           
            if (_SatProjectDetails != null)
            {
                minusserial_list = new List<ReptPickSerials>();
                if (_SatProjectDetails.Count > 0)
                {
                    int i = 1;
                    foreach (SatProjectDetails _det in _SatProjectDetails)
                    {
                        if (_det.SPD_ITM == "ELC001028")
                        {
                            string a = "";
                        }
                        ReptPickSerials _inputReptPickSerials = new ReptPickSerials();
                        #region Fill Pick Serial Object
                        //// _inputReptPickSerials.Tus_usrseq_no = _userSeqNo;
                        _inputReptPickSerials.Tus_doc_no = txtjob.Text;
                        _inputReptPickSerials.Tus_base_doc_no = txtjob.Text;
                        _inputReptPickSerials.Tus_seq_no = 0;
                        _inputReptPickSerials.Tus_itm_line = i;
                        _inputReptPickSerials.Tus_base_itm_line = i;
                        // _inputReptPickSerials.Tus_batch_line = 0;
                        _inputReptPickSerials.Tus_ser_line = i;
                        _inputReptPickSerials.Tus_doc_dt = DateTime.Now.Date;
                        _inputReptPickSerials.Tus_orig_grndt = DateTime.Now.Date;
                        _inputReptPickSerials.Tus_com = Session["UserCompanyCode"].ToString();
                        _inputReptPickSerials.Tus_loc = txttoloc.Text;
                        _inputReptPickSerials.Tus_bin = CHNLSVC.Inventory.Get_defaultBinCDWeb(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                        _inputReptPickSerials.Tus_itm_cd = _det.SPD_ITM;
                        _inputReptPickSerials.Tus_itm_stus = "GOD";
                       
                        // _inputReptPickSerials.Tus_unit_cost = _unitCost;
                        // _inputReptPickSerials.Tus_unit_price = _unitPrice;
                        // if (Session["_itemSerializedStatus"].ToString() == "0")
                        // {
                        //   _inputReptPickSerials.Tus_qty = Convert.ToDecimal(txtqty.Text);
                        // }
                        // else
                        // {
                        _inputReptPickSerials.Tus_qty = _det.SPD_EST_QTY;
                        // }


                        // if (_serID > 0)
                        // { _inputReptPickSerials.Tus_ser_id = _serID; }
                        // else
                        // { 
                        _inputReptPickSerials.Tus_ser_id = 0;
                        _inputReptPickSerials.Tus_ser_1 = "N/A";
                        // _inputReptPickSerials.Tus_ser_2 = _serialNo2;
                        // _inputReptPickSerials.Tus_ser_3 = _serialNo3;
                        // if (string.IsNullOrEmpty(_warrantyno))
                        //  string _warrantyno = String.Format("{0:dd}", DateTime.Now.Date) + String.Format("{0:MM}", DateTime.Now.Date) + String.Format("{0:yy}", DateTime.Now.Date) + "-" + Session["UserDefLoca"].ToString() + "-" + _userwarrid + "-" + _inputReptPickSerials.Tus_ser_id.ToString();
                        // _inputReptPickSerials.Tus_warr_no = _warrantyno;
                        // _inputReptPickSerials.Tus_itm_desc = txtItemDes.Text;
                        // _inputReptPickSerials.Tus_itm_model = txtModel.Text;
                        // _inputReptPickSerials.Tus_itm_brand = txtBrand.Text;
                        // _inputReptPickSerials.Tus_itm_line = ItemLineNo;
                        // _inputReptPickSerials.Tus_cre_by = Session["UserID"].ToString();
                        // _inputReptPickSerials.Tus_cre_dt = DateTime.Now.Date;
                        // _inputReptPickSerials.Tus_session_id = Session["SessionID"].ToString();
                        // _inputReptPickSerials.Tus_itm_desc = txtItemDes.Text;
                        // _inputReptPickSerials.Tus_itm_model = txtModel.Text;
                        // _inputReptPickSerials.Tus_itm_brand = txtBrand.Text;

                        // string _baseItem = Session["baseitem"].ToString();
                        // if (string.IsNullOrEmpty(_baseItem))
                        // {
                        //     _baseItem = PopupItemCode;
                        // }
                        // _inputReptPickSerials.Tus_new_itm_cd = _baseItem;

                        _inputReptPickSerials.Tus_base_doc_no = txtjob.Text;
                        _inputReptPickSerials.Tus_job_no = txtjob.Text;
                        // _inputReptPickSerials.Tus_job_line = ItemLineNo;
                        // _inputReptPickSerials.Tus_job_no = txtPORefNo.Text;
                        _prodno = _det.SPD_NO;
                        
                        minusserial_list.Add(_inputReptPickSerials);
                        i++;
                        #endregion
                        i++;
                    }
                }
            }
            string _doc=string.Empty;
            int result = 0;
            if (minusserial_list == null)
            {
                DisplayMessage("Please select the job number", 1);
                txtmitem.Focus();
                return;
            }
            if (minusserial_list.Count == 0)
            {
                DisplayMessage("Please select the job number", 1);
                txtmitem.Focus();
                return;
            }
            inHeader.ProductionBaseDoc = true;
            inHeader.ProductionBaseDocNo = txtjob.Text;
            inHeader.Ith_service_job_base = true;
            inHeader.Ith_service_job_no = txtjob.Text;
            inHeader.Ith_gen_frm = "SCMWEBPROISSU";
            #region add different status out by lakshan 15Jan2018
            TmpValidation _err = new TmpValidation();
            InventoryBatchRefN _locBal = new InventoryBatchRefN();
            List<InventoryBatchRefN> _locBalList = new List<InventoryBatchRefN>();
            List<TmpValidation> _errList = new List<TmpValidation>();
            List<ReptPickSerials> _issueSerialList = new List<ReptPickSerials>();
            ReptPickSerials _tmpSer = new ReptPickSerials();
            var _ItemList = minusserial_list.GroupBy(x => new { x.Tus_com, x.Tus_loc,x.Tus_itm_cd }
                ).Select(group => new { Peo = group.Key, theQty = group.Sum(o => o.Tus_qty) });
            foreach (var item in _ItemList)
            {
                if (item.Peo.Tus_itm_cd == "ELC001028")
                {
                    int vv = 0;
                }
                bool _objResSet = false;
                _locBalList = CHNLSVC.Inventory.GET_INR_BATCH_BY_JOB_NO_PRO_ISSU(new InventoryBatchRefN()
                {
                    Inb_com = item.Peo.Tus_com,
                    Inb_loc = inHeader.Ith_loc,
                    Inb_itm_cd = item.Peo.Tus_itm_cd,
                    Inb_itm_stus = "GOD",
                    Inb_job_no = txtjob.Text
                });
                decimal _sumInbQty = _locBalList.Sum(c=> c.Inb_qty);
                if (_sumInbQty < item.theQty)
                {
                    _locBalList = CHNLSVC.Inventory.GET_INR_BATCH_BY_JOB_NO_PRO_ISSU(new InventoryBatchRefN()
                    {
                        Inb_com = item.Peo.Tus_com,
                        Inb_loc = inHeader.Ith_loc,
                        Inb_itm_cd = item.Peo.Tus_itm_cd,
                        Inb_job_no = txtjob.Text
                    });
                    decimal _locTotalQty = _locBalList.Sum(c => c.Inb_qty);
                    if (_locTotalQty < item.theQty)
                    {
                        #region err
                        _err = new TmpValidation();
                        _err.Inl_com = item.Peo.Tus_com;
                        _err.Inl_loc = inHeader.Ith_loc;
                        _err.Inl_itm_cd = item.Peo.Tus_itm_cd;
                        _err.Inl_itm_stus = "GOD";
                        _err.Pick_qty = item.theQty;
                        _err.Inl_free_qty = _locTotalQty;
                        _err.Sad_do_qty = _locTotalQty;
                        _errList.Add(_err);
                        #endregion
                    }
                    else
                    {
                        _objResSet = true;
                    }
                }
                if (!_objResSet)
                {
                    var _serDtList = minusserial_list.Where(c =>
                        c.Tus_com == item.Peo.Tus_com  && c.Tus_loc == item.Peo.Tus_loc &&
                        c.Tus_itm_cd == item.Peo.Tus_itm_cd //&& c.Tus_itm_stus == item.Peo.Tus_itm_stus
                        ).ToList();
                    _issueSerialList.AddRange(_serDtList);
                }
                else
                {
                    var _serDtList = minusserial_list.Where(c =>
                        c.Tus_com == item.Peo.Tus_com && c.Tus_loc == item.Peo.Tus_loc &&   
                        c.Tus_itm_cd == item.Peo.Tus_itm_cd //&& c.Tus_itm_stus == item.Peo.Tus_itm_stus
                        ).ToList();
                    _locBalList = CHNLSVC.Inventory.GET_INR_BATCH_BY_JOB_NO_PRO_ISSU(new InventoryBatchRefN()
                    {
                        Inb_com = item.Peo.Tus_com,
                        Inb_loc = inHeader.Ith_loc,
                        Inb_itm_cd = item.Peo.Tus_itm_cd,
                        Inb_job_no = txtjob.Text
                    }).OrderByDescending(c=>c.Inb_qty).ToList();
                    ReptPickSerials _tser = new ReptPickSerials();
                    foreach (var _ser in _serDtList)
                    {
                        foreach (var _vLocBal in _locBalList)
                        {
                            if (_ser.Tmp_used_qty < _ser.Tus_qty)
                            {
                                if (_vLocBal.Tmp_inb_used_qty < _vLocBal.Inb_qty)
                                {
                                    if (_vLocBal.Inb_qty == (_ser.Tus_qty - _ser.Tmp_used_qty))
                                    {
                                        decimal qty = (_ser.Tus_qty - _ser.Tmp_used_qty);
                                        _ser.Tmp_used_qty += (_ser.Tus_qty - _ser.Tmp_used_qty);
                                        _vLocBal.Tmp_inb_used_qty = _vLocBal.Inb_qty;
                                        _tser = ReptPickSerials.CreateNewObject(_ser);
                                        _tser.Tus_itm_stus = _vLocBal.Inb_itm_stus;
                                        _tser.Tus_qty = qty;
                                        _issueSerialList.Add(_tser);
                                    }
                                    else if (_vLocBal.Inb_qty >( _ser.Tus_qty - _ser.Tmp_used_qty))
                                    {
                                        decimal qty = (_ser.Tus_qty - _ser.Tmp_used_qty);
                                        _ser.Tmp_used_qty += (_ser.Tus_qty - _ser.Tmp_used_qty);
                                        _vLocBal.Tmp_inb_used_qty = (_ser.Tus_qty - _ser.Tmp_used_qty);
                                        _tser = ReptPickSerials.CreateNewObject(_ser);
                                        _tser.Tus_itm_stus = _vLocBal.Inb_itm_stus;
                                        _tser.Tus_qty = qty;
                                        _issueSerialList.Add(_tser);
                                    }
                                    else if (_vLocBal.Inb_qty < (_ser.Tus_qty - _ser.Tmp_used_qty))
	                                {
                                         _ser.Tmp_used_qty +=_vLocBal.Inb_qty;
                                        _vLocBal.Tmp_inb_used_qty =  _vLocBal.Inb_qty;
                                        _tser = ReptPickSerials.CreateNewObject(_ser);
                                        _tser.Tus_itm_stus = _vLocBal.Inb_itm_stus;
                                        _tser.Tus_qty = _vLocBal.Inb_qty;
                                        _issueSerialList.Add(_tser);
	                                }
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //_tmpSer = 
                        //10/8
                    }
                }
            }
            if (_errList.Count > 0)
            {
                dgvItmBalErr.DataSource = _errList;
                dgvItmBalErr.DataBind();
                popBalancErr.Show();
                return;
            }
            foreach (ReptPickSerials sr in minusserial_list)
            {
                decimal qty = _issueSerialList.Where(x => x.Tus_itm_cd == sr.Tus_itm_cd && x.Tus_base_itm_line == sr.Tus_base_itm_line).Sum(c => c.Tus_qty);
                if (qty != sr.Tus_qty)
                {
                    #region err
                    _err = new TmpValidation();
                    _err.Inl_com = sr.Tus_com;
                    _err.Inl_loc = inHeader.Ith_loc;
                    _err.Inl_itm_cd = sr.Tus_itm_cd;
                    _err.Inl_itm_stus = "GOD";
                    _err.Pick_qty =sr.Tus_qty ;
                    _err.Inl_free_qty = 0;
                    _err.Sad_do_qty = qty;
                    _errList.Add(_err);
                    #endregion
                }
            }
            if (_errList.Count > 0)
            {
                dgvItmBalErr.DataSource = _errList;
                dgvItmBalErr.DataBind();
                popBalancErr.Show();
                return;
            }
            decimal _newSerObjQty = _issueSerialList.Sum(c=> c.Tus_qty);
            decimal _oldSerObjQty = minusserial_list.Sum(c => c.Tus_qty);
            if (_newSerObjQty != _oldSerObjQty)
            {
                DispMsg("Serial count mismatch !"); return;
            }
            
            #endregion
            result = CHNLSVC.Inventory.ProductionIssue(inHeader, _issueSerialList, serial_list, txtfrom.Text, txttoloc.Text, out  _doc, _prodno);
            if (result != -99 && result >= 0)
            {
                string Msg = "Successfully saved ! Document No :  " + _doc;
                DisplayMessage(Msg, 3);
                PageClear();
            }
            else
            {
                if (_doc.Contains("CHK_INLFREEQTY"))
                {
                    DisplayMessage("Free Qty is not available in location :" + Session["UserDefLoca"].ToString(), 1);
                }
                else if (_doc.Contains("NO_STOCK_BALANCE"))
                {
                    DisplayMessage("Stock balance is not available in location :" + Session["UserDefLoca"].ToString(), 1);
                }
                else
                {
                    DisplayMessage(_doc, 1);
                }
            }
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.InvoiceWithDate:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "C" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceType:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocNo:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "PRO" + seperator);
                        break;
                    }
              
                default:
                    break;
            }

            return paramsText.ToString();
        }
        private void DisplayMessage(String Msg, Int32 option, Exception ex = null)
        {
            Msg = Msg.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ").Replace("\r", "");
            if (option == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
            }
            else if (option == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + Msg + "');", true);
            }
            else if (option == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + Msg + "');", true);
            }
            else if (option == 4)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + Msg + "');", true);
            }
        }

        protected void txtfloc_TextChanged(object sender, EventArgs e)
        {
            txtfloc.Text = txtfloc.Text.ToUpper().Trim();
            DataTable result = new DataTable();
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
            result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, "Code", txtfloc.Text);
            if (result.Rows.Count == 0)
            {
                txtfloc.Text = string.Empty;
               
                DisplayMessage("Please enter valid location", 1);
                return;
            }
            txtfloc.ToolTip = result.Rows[0][1].ToString();
        }
        protected void txttoloc_TextChanged(object sender, EventArgs e)
        {
            txttoloc.Text = txttoloc.Text.ToUpper().Trim();
            DataTable result = new DataTable();
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
            result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, "Code", txttoloc.Text);
            if (result.Rows.Count == 0)
            {
                txttoloc.Text = string.Empty;
                DisplayMessage("Please enter valid location", 1);
                return;
            }
            txttoloc.ToolTip = result.Rows[0][1].ToString();
        }
        #region Modalpopup
        protected void btnClose_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
        }

        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            string ID = grdResult.SelectedRow.Cells[1].Text;
            if (lblvalue.Text == "location")
            {
                txtfloc.Text = grdResult.SelectedRow.Cells[1].Text;
               
                return;
            }
            if (lblvalue.Text == "location2")
            {
                txttoloc.Text = grdResult.SelectedRow.Cells[1].Text;

                return;
            }
            lblvalue.Text = "";
        }
        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResult.PageIndex = e.NewPageIndex;
            grdResult.DataSource = null;
            grdResult.DataSource = (DataTable)ViewState["SEARCH"];
            grdResult.DataBind();
            UserPopoup.Show();
            Session["SIPopup"] = "SIPopup";
            txtSearchbyword.Focus();
        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {

            try
            {
                FilterData();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {

            try
            {
                FilterData();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }

        private void FilterData()
        {
            if ((lblvalue.Text == "location")||(lblvalue.Text == "location2"))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, "%" + txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }


        }

        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            this.ddlSearchbykey.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykey.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykey.SelectedIndex = 0;
        }

        #endregion
        protected void lbtnloc_Click(object sender, EventArgs e)
        {
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
            DataTable result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
            grdResult.DataSource = result;
            grdResult.DataBind();
            lblvalue.Text = "location";
            BindUCtrlDDLData(result);
            txtSearchbyword.Focus();
            UserPopoup.Show();

        }
        protected void lbtnloc2_Click(object sender, EventArgs e)
        {
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
            DataTable result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
            grdResult.DataSource = result;
            grdResult.DataBind();
            lblvalue.Text = "location2";
            BindUCtrlDDLData(result);
            txtSearchbyword.Focus();
            UserPopoup.Show();

        }
        protected void lbtnclear_Click(object sender, EventArgs e)
        {
            if (txtClearlconformmessageValue.Value == "Yes")
            {
                try
                {
                    try
                    {
                        Response.Redirect(Request.RawUrl, false);
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
                    }
                }
                catch (Exception ex)
                {
                    //divalert.Visible = true;
                    DisplayMessage(ex.Message, 4);
                }
            }
        }

        protected void txtprefix_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string item = txtmitem.Text.ToString();
                if (item=="")
                {
                    DisplayMessage("Please Select Main Item", 1);
                    txtprefix.Text = "";
                    return;
                }
                else
                {
                    DataTable dt = CHNLSVC.General.GetItemPrefix(item);
                    MasterItem _mstItem = new MasterItem();
                    _mstItem = CHNLSVC.General.GetItemMaster(item);
                    if (_mstItem.Mi_is_ser1 == 1) 
                    {                  
                        if (dt !=null)
                        {
                            if (dt.Rows.Count>0)
                            {
                                string prefix = dt.Rows[0][0].ToString();
                                txtprefix.Text = txtprefix.Text + prefix;
                            }
                      
                        }
                    }   
                    else { txtprefix.Text = "N/A"; }
                  
                }

            }catch(Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }
        protected void txtser1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtser1.Text.ToString() != "")
                {
                    Int32 count = Convert.ToInt32(txtIqty.Text.ToString());
                    Int32 lengthser1 = txtser1.Text.ToString().Length;

                    Int64 _ser1 = Convert.ToInt64(txtser1.Text.ToString());
                    Int64 _ser2 = _ser1 + count-1;
                    Int32 lengthser2 = _ser2.ToString().Length;

                    if (lengthser1 == lengthser2)
                    {
                        txtser2.Text = _ser2.ToString() ;
                    }
                    else if (lengthser1 > lengthser2)
                    {
                        int i = lengthser1 - lengthser2;
                        string ser2text = _ser2.ToString();
                        for (int k = 1; k <= i; k++ )
                        {
                            ser2text ="0"+ ser2text;
                        }
                        txtser2.Text = ser2text;
                    }
                    else if (lengthser1 < lengthser2)
                    {
                        int i = lengthser2 - lengthser1;
                        string ser2text = _ser2.ToString();
                        for (int k = 1; k >= i; k++)
                        {
                            ser2text = ser2text.Remove(0);
                        }
                        txtser2.Text = ser2text;
                    }
                }
            }catch(Exception ex)
            {
                DisplayMessage(ex.Message, 4);
                return;
            }
        }

        protected void lbtnpda_Click(object sender, EventArgs e)
        {
            PopulateLoadingBays();
            MPPDA.Show();
        }

        protected void btnsend_Click(object sender, EventArgs e)
        {

            try
            {

                Session["loadingbay"] = ddlloadingbay.SelectedValue;
                if(txtjob.Text=="")
                {
                    DisplayMessage("Please Select Job No",2);
                    return;
                }
                 SaveData();
                MPPDA.Hide();


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);
                return;
            }
        }

        protected void btncClose_Click(object sender, EventArgs e)
        {
            try
            {
                MPPDA.Hide();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);
                return;
            }
        }
        private void PopulateLoadingBays()
        {
            try
            {
                DataTable dtbays = CHNLSVC.Inventory.LoadLoadingBays(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "LB");

                if (dtbays.Rows.Count > 0)
                {
                    ddlloadingbay.DataSource = dtbays;
                    ddlloadingbay.DataTextField = "mws_res_name";
                    ddlloadingbay.DataValueField = "mws_res_cd";
                    ddlloadingbay.DataBind();
                }

                ddlloadingbay.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);
                CHNLSVC.CloseChannel();
            }

        }
        private void SaveData()
        {
            Int32 val = 0;
            string warehousecom = (string)Session["WAREHOUSE_COM"];
            string warehouseloc = (string)Session["WAREHOUSE_LOC"];

            if (ddlloadingbay.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the loading bay !!!')", true);
                ddlloadingbay.Focus();
                MPPDA.Show();
                return;
            }
            SatProjectDetails _obj = new SatProjectDetails();
            SatProjectHeader _HDR = CHNLSVC.Inventory.GET_SAT_PRO_HDR_DATA(Session["UserCompanyCode"].ToString(), txtjob.Text.ToString());
            _obj.SPD_NO = _HDR.SPH_NO;
            _obj.SPD_ACTVE = 1;

            List<SatProjectDetails> oRequesItems = CHNLSVC.Inventory.GET_SAT_PRO_DET_DATA(_obj);


            #region Add by Lakshan to chk doc already send or not 01 Oct 2016
            bool _docAva = false;
            ReptPickHeader _tmpPickHdr = CHNLSVC.Inventory.GET_TEMP_PICK_HDR_DATA(new ReptPickHeader()
            {
                Tuh_usr_com = Session["UserCompanyCode"].ToString(),
                Tuh_doc_no = txtjob.Text
            }).FirstOrDefault();
            if (_tmpPickHdr != null)
            {
                List<ReptPickSerials> _repSerList = CHNLSVC.Inventory.GET_TEMP_PICK_SER_DATA(new ReptPickSerials() { Tus_usrseq_no = _tmpPickHdr.Tuh_usrseq_no });
                if (_repSerList != null)
                {
                    if (_repSerList.Count > 0)
                    {
                        _docAva = true;
                    }
                }
                List<ReptPickItems> _repItmList = CHNLSVC.Inventory.GET_TEMP_PICK_ITM_DATA(new ReptPickItems() { Tui_usrseq_no = _tmpPickHdr.Tuh_usrseq_no });
                if (_repItmList != null)
                {
                    if (_repItmList.Count > 0)
                    {
                        _docAva = true;
                    }
                }

            }
            if (_docAva)
            {
                //string _msg = "Document has already sent to PDA or has alread processed by User : " + _tmpPickHdr.Tuh_usr_id + " in loading bay :" + _tmpPickHdr.Tuh_load_bay ;
                string _msg = "Document has already been sent to PDA or has already been processed by User : " + _tmpPickHdr.Tuh_usr_id + " in loading bay " + _tmpPickHdr.Tuh_load_bay;
                DisplayMessage(_msg, 2);
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Document has already sent to PDA or has alread processed !!!')", true);
                return;
            }
            #endregion

            Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(_HDR.SPH_ANAL2, Session["UserCompanyCode"].ToString(), txtjob.Text, Convert.ToInt32(0));
            if (user_seq_num == -1)
            {
                user_seq_num = GenerateNewUserSeqNo(_HDR.SPH_ANAL2, _HDR.SPH_NO);
                ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                _inputReptPickHeader.Tuh_usrseq_no = Convert.ToInt32(user_seq_num);
                _inputReptPickHeader.Tuh_usr_id = Session["UserID"].ToString();
                _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                _inputReptPickHeader.Tuh_session_id = Session["SessionID"].ToString();
                _inputReptPickHeader.Tuh_doc_tp = _HDR.SPH_ANAL2;
                int dir = Convert.ToInt32(0);
                _inputReptPickHeader.Tuh_direct = Convert.ToBoolean(dir);
                _inputReptPickHeader.Tuh_ischek_itmstus = false;
                _inputReptPickHeader.Tuh_ischek_simitm = false;
                _inputReptPickHeader.Tuh_ischek_reqqty = false;
                _inputReptPickHeader.Tuh_doc_no = _HDR.SPH_NO;
                _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                _inputReptPickHeader.Tuh_wh_com = warehousecom;
                _inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                _inputReptPickHeader.Tuh_load_bay = ddlloadingbay.SelectedValue;
                _inputReptPickHeader.Tuh_is_take_res = true;
                _inputReptPickHeader.Tuh_rec_loc = _HDR.SPH_PRO_LOC;
                val = CHNLSVC.Inventory.SavePickedHeader(_inputReptPickHeader);

                if (val == -1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                    CHNLSVC.CloseChannel();
                    return;
                }
            }
            else
            {
                ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                _inputReptPickHeader.Tuh_doc_no = _HDR.SPH_NO;
                _inputReptPickHeader.Tuh_doc_tp = _HDR.SPH_ANAL2;
                int direc = Convert.ToInt32(0);
                _inputReptPickHeader.Tuh_direct = Convert.ToBoolean(direc);
                _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                _inputReptPickHeader.Tuh_wh_com = warehousecom;
                _inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                _inputReptPickHeader.Tuh_load_bay = ddlloadingbay.SelectedValue;
                _inputReptPickHeader.Tuh_usrseq_no = Convert.ToInt32(user_seq_num);
                _inputReptPickHeader.Tuh_is_take_res = false;
                val = CHNLSVC.Inventory.UpdatePickHeader(_inputReptPickHeader);

                if (Convert.ToInt32(val) == -1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                    CHNLSVC.CloseChannel();
                    return;
                }
            }


            DataTable dtchkitm = CHNLSVC.Inventory.CheckItemsScannedStatus(user_seq_num);

            if (dtchkitm.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Document has already sent to PDA or has alread processed !!!')", true);
                return;
            }
            else
            {
                List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                foreach (SatProjectDetails _row in oRequesItems)
                {
                    ReptPickItems _reptitm = new ReptPickItems();
                    _reptitm.Tui_usrseq_no = Convert.ToInt32(user_seq_num);
                    _reptitm.Tui_req_itm_qty = _row.SPD_EST_QTY;
                    _reptitm.Tui_req_itm_cd = _row.SPD_ITM;
                    _reptitm.Tui_req_itm_stus = "GOD";
                    _reptitm.Tui_pic_itm_cd = _row.SPD_LINE.ToString();
                    _saveonly.Add(_reptitm);
                    val = CHNLSVC.Inventory.SavePickedItems(_saveonly);
                }
            }

            if (val == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Successfully sent !!!')", true);
                MPPDA.Hide();
            }

        }
        private Int32 GenerateNewUserSeqNo(string DocumentType, string _scanDocument)
        {
            Int32 generated_seq = 0;
            generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), DocumentType, 1, Session["UserCompanyCode"].ToString());//direction always =1 for this method                    //assign user_seqno
            ReptPickHeader RPH = new ReptPickHeader();
            RPH.Tuh_doc_tp = DocumentType;
            RPH.Tuh_cre_dt = DateTime.Now;// DateTime.Today;//might change //Calendar-SelectedDate;
            RPH.Tuh_ischek_itmstus = true;//might change 
            RPH.Tuh_ischek_reqqty = true;//might change
            RPH.Tuh_ischek_simitm = true;//might change
            RPH.Tuh_session_id = Session["SessionID"].ToString();
            RPH.Tuh_usr_com = Session["UserCompanyCode"].ToString();//might change 
            RPH.Tuh_usr_id = Session["UserID"].ToString();
            RPH.Tuh_usrseq_no = generated_seq;
            RPH.Tuh_direct = true; //direction always (-) for change status
            RPH.Tuh_doc_no = _scanDocument;
            return generated_seq;
        }

        protected void txtIqty_TextChanged(object sender, EventArgs e)
        {
            lbtnaddqty_Click(null, null);
        }
        private void DispMsg(string msgText, string msgType = "")
        {
            msgText = msgText.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");

            if (msgType == "N")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + msgText + "');", true);
            }
            else if (msgType == "W" || msgType == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msgText + "');", true);
            }
            else if (msgType == "S")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + msgText + "');", true);
            }
            else if (msgType == "E")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + msgText + "');", true);
            }
        }

        protected void lbtnBalancErr_Click(object sender, EventArgs e)
        {
            popBalancErr.Hide();
        }
    }
}