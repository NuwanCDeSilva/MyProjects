using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using FF.Interfaces;


namespace FF.WindowsERPClient.General
{
    public partial class VehicleReleaseToHPAccHolder : Base
    {
        //sp_search_InvoiceNoForVehicle =update *********


        //sp_get_RequiredDocuments =UPDATE
        //sp_search_InvoiceNoForVehicle UPDATE **        
        //pkg_search.sp_search_salesdoc  NEW

        
        private List<ReptPickSerials> approveList;

        public List<ReptPickSerials> ApproveList
        {
            get { return approveList; }
            set { approveList = value; }
        }
        
        public VehicleReleaseToHPAccHolder()
        {
            try
            {
                InitializeComponent();
                TextBoxChDate.Visible = false;
                TextBoxChDate2.Visible = false;

                grvSerchResults.Columns["svrt_veh_reg_no"].Visible = false;
                grvSerchResults.Columns["sah_acc_no"].Visible = false;
                //grvInvoiceDet.DataSource = new List<VehicalRegistration>();
                //grvInvoiceDet.DataBind();
                ApproveList = new List<ReptPickSerials>();
                DataTable dt = CHNLSVC.General.Get_RequiredDocuments("VHREG");
                if (dt != null)
                {
                    grv_docRequired.DataSource = dt;
                }
                else
                {
                    return;
                }
                //-------------------------------------------------------
                foreach (DataGridViewRow row in grv_docRequired.Rows)
                {
                    if (row.Cells["hsp_is_required"].Value.ToString() == "1")
                    {
                        row.Cells["col_isReq"].Value = "*";
                        row.Cells["col_isReq"].Style.ForeColor = Color.Red;
                        //row.Cells["col_isReq"].Style = Color.Green;
                    }
                    else
                    {
                        row.Cells["col_isReq"].Value = "";

                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                VehicleReleaseToHPAccHolder formnew = new VehicleReleaseToHPAccHolder();
                formnew.MdiParent = this.MdiParent;
                formnew.Location = this.Location;
                formnew.Show();
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
            
        }

        private void txtInvoiceNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==(char)Keys.Enter)
            {
                this.btnGetDet_Click(null, null);
                //try
                //{
                //    #region
                //    List<VehicalRegistration> listofInvoice = new List<VehicalRegistration>();
                //    // listofInvoice= CHNLSVC.General.GetVehiclesByInvoiceNo(GlbUserComCode, GlbUserDefProf, txtInvoiceNum.Text.Trim());
                //    List<FF.BusinessObjects.VehicalRegistration> vehRegList = CHNLSVC.General.GetVehiclesByInvoiceNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtInvoiceNum.Text.Trim());
                //    grvInvoiceDet.DataSource = null;
                //    grvInvoiceDet.AutoGenerateColumns = false;
                //    grvInvoiceDet.DataSource = vehRegList;
                   
                //    if (vehRegList==null)
                //    {
                //        lblCustCD.Text = "";
                //        lblCustName.Text = "";
                //        lblInsuranceNo.Text = "";
                //        lblInvDt.Text = "";
                //        return;
                //    }
                //    if (vehRegList.Count > 0)
                //    {
                //        foreach (FF.BusinessObjects.VehicalRegistration veh in vehRegList)
                //        {                            
                //            List<ReptPickSerials> rpsList = CHNLSVC.General.getserialByInvoiceNum(veh.P_svrt_inv_no, BaseCls.GlbUserComCode);
                //            foreach (ReptPickSerials rp in rpsList)
                //            {
                //                rp.Tus_base_doc_no = veh.P_srvt_ref_no;
                //            }
                //            ApproveList.AddRange(rpsList);
                //        }
                //        if (ApproveList.Count<1)
                //        {
                //            MessageBox.Show("No tranaction Items found in this invoice!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);//sat_veh_reg_txn has no data.
                //        }

                //        string custCD = vehRegList[0].P_svrt_cust_cd;
                //        string custName = vehRegList[0].P_svrt_full_name;
                //        string insuranceNo = vehRegList[0].P_srvt_insu_ref;
                //        lblCustCD.Text = custCD;
                //        lblCustName.Text = custName;
                //        lblInsuranceNo.Text = insuranceNo;
                //        lblInvDt.Text = vehRegList[0].P_svrt_dt.Date.ToString();

                //        //if (vehRegList != null )
                //        //{
                //        //    if (vehRegList.Count > 0)
                //        //    {
                //        //        string custCD = vehRegList[0].P_svrt_cust_cd;
                //        //        string custName = vehRegList[0].P_svrt_full_name;
                //        //        string insuranceNo = vehRegList[0].P_srvt_insu_ref;
                //        //        lblCustCD.Text = custCD;
                //        //        lblCustName.Text = custName;
                //        //        lblInsuranceNo.Text = insuranceNo;
                //        //        lblInvDt.Text = vehRegList[0].P_svrt_dt.Date.ToString();
                //        //    }
                            
                //        //}
                //    }
                //    #endregion
                //}
                //catch (Exception er)
                //{                   
                //   // string Msg = "<script>alert('System Error Occur. : " + er.Message + "');window.location = 'General_Modules/VehicleReleaseToHPAccHolder.aspx-(btnInvOk_Click)';</script>";
                //  //  ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                //    MessageBox.Show("Cannot load invoice data!","",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                //    return;
                //}
            }
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
             
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
              
                case CommonUIDefiniton.SearchUserControlType.InvSalesInvoice:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "INV" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceDet:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }
        private void ImgBtnInvSearch_Click(object sender, EventArgs e)
        {
            //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            //_CommonSearch.ReturnIndex = 0;
            //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvSalesInvoice);
            ////DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(_CommonSearch.SearchParams, null, null);//GetInvoiceSearchData
            //DataTable _result = CHNLSVC.CommonSearch.GetInvoiceSearchData(_CommonSearch.SearchParams, null, null);
            //_CommonSearch.dvResult.DataSource = _result;
            //_CommonSearch.BindUCtrlDDLData(_result);
            //_CommonSearch.obj_TragetTextBox = txtInvoiceNum;
            //_CommonSearch.ShowDialog();
            //txtInvoiceNum.Select();
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceDet);
                //DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(_CommonSearch.SearchParams, null, null);//GetInvoiceSearchData
                DataTable _result = CHNLSVC.CommonSearch.Get_invoiceDet(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtInvoiceNum;
                _CommonSearch.ShowDialog();
                txtInvoiceNum.Select();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
               
        private void txtInvoiceNum_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.ImgBtnInvSearch_Click(sender, e);
        }
              
     
        private void btnSave_Click_1(object sender, EventArgs e)
        {
             //DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
             //       if (Convert.ToBoolean(chk.Value) == true)
            try
            {
                Int32 count = 0;
                grvInvoiceDet.EndEdit();
                foreach (DataGridViewRow row in grvInvoiceDet.Rows)
                {
                    DataGridViewCheckBoxCell chk = row.Cells[0] as DataGridViewCheckBoxCell;
                    //bool isChecked = Convert.ToBoolean(chk.Value);
                    //if (chekbox.Checked == false)
                    if (Convert.ToBoolean(chk.Value) == true)
                    {
                        count = count + 1;
                    }
                }
                if (count < 1)
                {
                    MessageBox.Show("Select Items to approve!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {

                }

                #region
                ApproveList = new List<ReptPickSerials>();
                foreach (DataGridViewRow row in grvInvoiceDet.Rows)
                {
                    // LinkButton refNo = (LinkButton)row.FindControl("linkBtnRefNum");
                    string engineNo = row.Cells["P_svrt_engine"].Value.ToString();//row.Cells[4].Text;
                    string chasseNo = row.Cells["P_svrt_chassis"].Value.ToString();//row.Cells[5].Text;
                    string docNo = row.Cells["P_srvt_ref_no"].Value.ToString();//refNo.Text.Trim();

                    //CheckBox chekbox = (CheckBox)row.FindControl("chekApprove");
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    bool isChecked = Convert.ToBoolean(chk.Value);
                    //if (chekbox.Checked == false)
                    if (isChecked == false)
                    {
                        // List<ReptPickSerials> rpsList = CHNLSVC.General.getserialByInvoiceNum(docNo, GlbUserComCode);
                        // ApproveList.AddRange(rpsList);
                        ApproveList.RemoveAll(x => x.Tus_ser_1 == engineNo && x.Tus_ser_2 == chasseNo);
                        //ApproveList.RemoveAll(x => x.Tus_base_doc_no == refNo && x.Tus_ser_1=);
                    }
                    else
                    {
                        ReptPickSerials _obj = new ReptPickSerials();
                        _obj.Tus_itm_cd = row.Cells["P_srvt_itm_cd"].Value.ToString();
                        _obj.Tus_base_doc_no = row.Cells["P_srvt_ref_no"].Value.ToString();
                        _obj.Tus_ser_1 = row.Cells["P_svrt_engine"].Value.ToString();
                        _obj.Tus_ser_2 = row.Cells["P_svrt_chassis"].Value.ToString();
                        _obj.Tus_ser_id = 0;
                        _obj.Tus_usrseq_no = 0;
                        ApproveList.Add(_obj);
                    }
                }
                //added
                if (ApproveList.Count < 1)
                {
                    // MessageBox.Show("No transaction items found to approve!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    MessageBox.Show("Items are released already.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //save approvelist.
                if (MessageBox.Show("Are you sure to Approve?", "Confirm Approve", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }

                Int32 effect = CHNLSVC.General.Update_ListVehicleApproveStatus(BaseCls.GlbUserComCode, ApproveList, CHNLSVC.Security.GetServerDateTime().Date, BaseCls.GlbUserID, txtInvoiceNum.Text.Trim());
                if (effect > 0)
                {
                    MessageBox.Show("Items approved successfully!", "Approve", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.btnClear_Click(null, null);
                }
                if (effect == 0)
                {
                    MessageBox.Show("No items approved!", "Approve", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                #endregion

            }
            catch (Exception er)
            {
                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, er.Message);
                //string Msg = "<script>alert('System Error Occur. : " + er.Message + "');window.location = 'General_Modules/VehicleReleaseToHPAccHolder.aspx-(btn_APPROVE_Click)';</script>";
                //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                MessageBox.Show(er.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel(); 
                return;
            }
            finally {
                CHNLSVC.CloseAllChannels();
            }
        }

       

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSearchOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdoAccNo.Checked == false && rdoInvDate.Checked == false && rdoRegiNo.Checked == false)
                {
                    MessageBox.Show("Please select the search type first!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                #region
                DataTable dt = new DataTable();
                //if (ddlSerachBy.SelectedValue == "Account No.")
                if (rdoAccNo.Checked == true)
                {
                    if (txtInvSerch.Text.Trim() == "")
                    {
                        //lblSearchMsg.Text = "Please enter Account # to search!";
                        // MessageBox.Show("Please enter Account # to search!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //return;
                        txtInvSerch.Text = "%";
                    }
                    //else
                    //{
                    //    dt = CHNLSVC.General.SearchInvoiceNo_forVehicle(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtInvSerch.Text.Trim(), null, null);

                    //}
                    dt = CHNLSVC.General.SearchInvoiceNo_forVehicle(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtInvSerch.Text.Trim(), null, null, null);
                }
                //if (ddlSerachBy.SelectedValue == "Registration No.")
                if (rdoRegiNo.Checked == true)
                {
                    if (txtInvSerch.Text.Trim() == "")
                    {
                        //lblSearchMsg.Text = "Please enter Registration # to search!";
                        // MessageBox.Show("Please enter Registration # to search!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        // return;
                        txtInvSerch.Text = "%";
                    }
                    //else
                    //{
                    //    //lblSearchMsg.Text = "";
                    //    //search on Reg no
                    //    dt = CHNLSVC.General.SearchInvoiceNo_forVehicle(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, null, txtInvSerch.Text.Trim(), null);
                    //}
                    dt = CHNLSVC.General.SearchInvoiceNo_forVehicle(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, null, txtInvSerch.Text.Trim(), null, null);
                }
                // if (ddlSerachBy.SelectedValue == "Invoice Date")
                if (rdoInvDate.Checked == true)
                {
                    //if (txtInvSerch.Text.Trim() == "")
                    //{
                    //    txtInvSerch.Text = TextBoxChDate.Value.ToShortDateString();
                    //   // MessageBox.Show("Please enter a Date to search!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //   // return;
                    //}
                    //else
                    //{
                    //lblSearchMsg.Text = "";
                    //search on Invoice Date
                    txtInvSerch.Text = TextBoxChDate.Value.ToShortDateString();
                    try
                    {
                        string invDate = Convert.ToDateTime(txtInvSerch.Text.Trim()).ToShortDateString();
                    }
                    catch (Exception ex)
                    {
                        // lblSearchMsg.Text = "Invalid Date Format!";
                        MessageBox.Show("Invalid Date Format!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    //string invoiceDate = Convert.ToDateTime(txtInvSerch.Text.Trim()).ToShortDateString();
                    //  string invoiceDate = Convert.ToDateTime(txtInvSerch.Text.Trim()).ToShortDateString();
                    string invoiceFromDate = TextBoxChDate.Value.Date.ToShortDateString(); ;
                    string invoiceToDate = TextBoxChDate2.Value.Date.ToShortDateString(); ;
                    dt = CHNLSVC.General.SearchInvoiceNo_forVehicle(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, null, null, invoiceFromDate, invoiceToDate);

                    //}
                }
                grvSerchResults.DataSource = null;
                grvSerchResults.AutoGenerateColumns = false;
                grvSerchResults.DataSource = dt;

                if (dt == null)
                {
                    MessageBox.Show("No records found for searched criteria!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No records found for searched criteria!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                #endregion
            }
            catch (Exception er)
            {
                // string Msg = "<script>alert('System Error Occur. : " + er.Message + "');window.location = 'General_Modules/VehicleReleaseToHPAccHolder.aspx-(btnSearchOk_Click)';</script>";
                //  ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                MessageBox.Show(er.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel(); 
                return;
            }
            finally {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            panel_advSearch.Visible = false;
            panel_Desc.Enabled = true;
        }

        private void btnAdvnSearch_Click(object sender, EventArgs e)
        {
            rdoRegiNo.Checked = false;
            rdoInvDate.Checked = false;
            rdoAccNo.Checked = false;
            txtInvoiceNum.Text = "";            
            TextBoxChDate.Value = DateTime.Now.Date;
            txtInvSerch.Text = "";

           // grvSerchResults.DataSource = null;
           // grvSerchResults.AutoGenerateColumns = false;

            panel_Desc.Enabled = false;
            panel_advSearch.Location = new Point(120, 60);//4,16
            panel_advSearch.Visible = true;

            DataTable dt = new DataTable();
            grvSerchResults.DataSource = null;
            grvSerchResults.AutoGenerateColumns = false;
            grvSerchResults.DataSource = dt;
            grvSerchResults.Columns["svrt_veh_reg_no"].Visible = false;
            grvSerchResults.Columns["sah_acc_no"].Visible = false;
             
        }

        private void TextBoxChDate_ValueChanged(object sender, EventArgs e)
        {
          //  txtInvSerch.Text = Convert.ToDateTime(TextBoxChDate.Value).Date.ToShortDateString();
        }

        private void grvSerchResults_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Int32 rowIndex = e.RowIndex;
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                //GridViewRow row = grvInvItems.SelectedRow;
                DataGridViewRow row = grvSerchResults.Rows[rowIndex];
                string Invoice_no = row.Cells["sah_inv_no"].Value.ToString();
                txtInvoiceNum.Text = Invoice_no;
              //  panel_advSearch.Visible = false;
                panel_Desc.Enabled = true;           
                txtInvoiceNum.Focus();
                this.btnGetDet_Click(null, null);
            }
        }

        private void label2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Int32 x_position = 0;
                Int32 y_position = 0;

                x_position = this.panel_advSearch.Location.X + e.X;
                y_position = this.panel_advSearch.Location.Y + e.Y;

                this.panel_advSearch.Location = new Point(x_position, y_position);
                //this.panel_move.Location = new Point(Cursor.Position.X + e.X, Cursor.Position.Y + e.Y);
            }
        }

        private void rdoAccNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoAccNo.Checked==true)
            {
                DataTable dt = new DataTable();
                grvSerchResults.DataSource = null;
                grvSerchResults.AutoGenerateColumns = false;
                grvSerchResults.DataSource = dt;   

                TextBoxChDate.Visible = false;
                TextBoxChDate2.Visible = false;
                txtInvSerch.Enabled = true;
                txtInvSerch.Text = "%";
                grvSerchResults.Columns["svrt_veh_reg_no"].Visible = false;
                //sah_acc_no
                grvSerchResults.Columns["sah_acc_no"].Visible = true;
            }
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoInvDate.Checked == true)
            {
                DataTable dt = new DataTable();
                grvSerchResults.DataSource = null;
                grvSerchResults.AutoGenerateColumns = false;
                grvSerchResults.DataSource = dt;  

                TextBoxChDate.Visible = true;
                TextBoxChDate2.Visible = true;
                txtInvSerch.Enabled = false;
                txtInvSerch.Text = "";
                grvSerchResults.Columns["svrt_veh_reg_no"].Visible = false;
                grvSerchResults.Columns["sah_acc_no"].Visible = false;
            }
        }

        private void rdoRegiNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoRegiNo.Checked == true)
            {
                DataTable dt = new DataTable();
                grvSerchResults.DataSource = null;
                grvSerchResults.AutoGenerateColumns = false;
                grvSerchResults.DataSource = dt;  

                TextBoxChDate.Visible = false;
                TextBoxChDate2.Visible = false;
                txtInvSerch.Enabled = true;
                txtInvSerch.Text = "%";
                //svrt_veh_reg_no
                grvSerchResults.Columns["svrt_veh_reg_no"].Visible = true;
                grvSerchResults.Columns["sah_acc_no"].Visible = false;
            }
        }

        private void btnGetDet_Click(object sender, EventArgs e)
        {
            try
            {
                #region
                List<VehicalRegistration> listofInvoice = new List<VehicalRegistration>();
                // listofInvoice= CHNLSVC.General.GetVehiclesByInvoiceNo(GlbUserComCode, GlbUserDefProf, txtInvoiceNum.Text.Trim());
                List<FF.BusinessObjects.VehicalRegistration> vehRegList = CHNLSVC.General.GetVehiclesByInvoiceNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtInvoiceNum.Text.Trim());


                if (vehRegList == null)
                {
                    lblCustCD.Text = "";
                    lblCustName.Text = "";
                    lblInsuranceNo.Text = "";
                    lblInvDt.Text = "";
                    MessageBox.Show("There is no registration reciept for selected invoice!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop); //invoice should be in 'sat_veh_reg_txn' table
                    return;
                }
                if (vehRegList.Count > 0)
                {
                    //-------------------------------------------------------------------------------------
                    Int32 not_ApprovedCont = 0;
                    foreach (FF.BusinessObjects.VehicalRegistration reg in vehRegList)
                    {
                        if (reg.P_srvt_isapp == false)
                        {
                            not_ApprovedCont = not_ApprovedCont + 1;
                        }
                    }
                    if (not_ApprovedCont == 0)
                    {
                        //all items are approved
                        MessageBox.Show("Approved already!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);//sat_veh_reg_txn has no data.
                        return;
                    }
                    //-------------------------------------------------------------------------------------

                    Boolean _chk = false;
                    foreach (FF.BusinessObjects.VehicalRegistration veh in vehRegList)
                    {
                         _chk = CHNLSVC.Financial.IsChkVehRelease(veh.P_svrt_inv_no);
                        //List<ReptPickSerials> rpsList = CHNLSVC.General.getserialByInvoiceNum(veh.P_svrt_inv_no, BaseCls.GlbUserComCode);
                        //foreach (ReptPickSerials rp in rpsList)
                        //{
                        //    rp.Tus_base_doc_no = veh.P_srvt_ref_no;
                        //}
                        //ApproveList.AddRange(rpsList);
                    }
                    if (_chk==false)
                    {
                        //MessageBox.Show("No tranaction Items found in this invoice!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);//sat_veh_reg_txn has no data.
                        MessageBox.Show("Already approved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);//sat_veh_reg_txn has no data.
                        return;
                    }
                    grvInvoiceDet.DataSource = null;
                    grvInvoiceDet.AutoGenerateColumns = false;
                    grvInvoiceDet.DataSource = vehRegList;

                    string custCD = vehRegList[0].P_svrt_cust_cd;
                    string custName = vehRegList[0].P_svrt_full_name;
                    string insuranceNo = vehRegList[0].P_srvt_insu_ref;
                    lblCustCD.Text = custCD;
                    lblCustName.Text = custName;
                    lblInsuranceNo.Text = insuranceNo;
                    lblInvDt.Text = vehRegList[0].P_svrt_dt.Date.ToString();

                }
                #endregion
            }
            catch (Exception er)
            {
                MessageBox.Show("Cannot load invoice data!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CHNLSVC.CloseChannel(); 
                return;
            }
            finally {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtInvSerch_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtInvSerch_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Enter)
            {
                this.btnSearchOk_Click(null, null);
            }
        }

        private void txtInvoiceNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.ImgBtnInvSearch_Click(null, null);
            }
        }
    }
}
