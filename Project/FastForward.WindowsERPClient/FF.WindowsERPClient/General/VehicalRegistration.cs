using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using FF.BusinessObjects;
using System.Text.RegularExpressions;

namespace FF.WindowsERPClient.General
{
    //sp_get_Reg_recievedDocForSend = NEW
    //sp_update_vehSendPay =new
    //sp_get_Reg_IssuedDocs= NEW
    //pkg_search.sp_search_vehical_regTxn =NEW
    //sp_get_RegDocs_ForSend =NEW
    //sp_update_vehDocREC=NEW
    //sp_update_vehDocREC_final=NEW
    //sp_update_vehSendPay=NEW
    //sp_update_vehCollectChq=NEW

    //get_VEH_DOCS_ON_STATUS =new (PENDING FUNCTIONS-FOR RADIO BUTTONS)

    public partial class VehicalRegistration : Base
    {

        string RecieptNo;
        private Int32 _selTabPage = 0;
        List<VehicleRegCompany> _listVehRegComp = new List<VehicleRegCompany>();
        private string _engNo = "";
        private string _invNo = "";
        private string _chasisNo = "";
        private string _cust = "";
        private string _locCode = "";
        DataTable _submitDoc = new DataTable();

        //private void button2_Click_1(object sender, EventArgs e)
        //{
        //    foreach (DataGridViewRow dr in grvRecDoc.Rows)
        //    {
        //        //dr.Cells["gdh_rec_doc"].Value.ToString()

        //        if (dr.Cells["gdh_rec_doc"].Value.ToString() == "0")
        //        {
        //            dr.DefaultCellStyle.BackColor = Color.Beige;
        //        }            
        //        //-----------------

        //        if (dr.Cells["gdh_rec_doc"].Value.ToString() == "1")
        //        {
        //            dr.DefaultCellStyle.BackColor = Color.LightCoral;
        //        }               
        //        //------------------------------
        //        if (dr.Cells["gdh_send_reg"].Value.ToString() == "1")                
        //        {
        //            dr.DefaultCellStyle.BackColor = Color.MistyRose;
        //        }
        //        //--------------------------------------
        //        if (dr.Cells["gdh_cr_rec"].Value.ToString() == "1")                  
        //        {
        //            dr.DefaultCellStyle.BackColor = Color.MintCream;
        //        }
        //        //-------------------------
        //        if (dr.Cells["gdh_send_pay"].Value.ToString() == "1")                 
        //        {
        //            dr.DefaultCellStyle.BackColor = Color.Olive;
        //        }

        //    }
        //}
        public VehicalRegistration()
        {
            try
            {
                InitializeComponent();//tabPage0
                if (tabControl1.SelectedTab.Name == "tabPage0")
                {
                    btnSave.Visible = false;
                    btnPrint.Visible = false;
                }

                RecieptNo = "";

                DataTable dt_recDoc = CHNLSVC.Sales.GetRecievedDocFor_VehReg(BaseCls.GlbUserID);
                //DataTable dt_recDoc = CHNLSVC.Sales.Get_IssuedDocFor_VehReg(BaseCls.GlbUserID, txtRecNoDocReceive.Text.Trim(), txtInvIss.Text.Trim(), txtEngineIssue.Text.Trim(), txtChassisIssue.Text.Trim());

                dt_recDoc.Columns.Add("ho_rec", typeof(string));
                dt_recDoc.Columns.Add("ho_allRec", typeof(string));
                dt_recDoc.Columns.Add("send_RMV", typeof(string));
                dt_recDoc.Columns.Add("reg_Status", typeof(string));
                dt_recDoc.Columns.Add("send_Pay", typeof(string));

                grvRecDoc.DataSource = null;
                grvRecDoc.AutoGenerateColumns = false;
                grvRecDoc.DataSource = dt_recDoc;

                if (dt_recDoc.Rows.Count > 0)
                {

                    foreach (DataRow dr in dt_recDoc.Rows)
                    {
                        //-------------------------------------------------------1
                        //if (dr["gdh_isse_doc"].ToString() == "1" && dr["gdh_rec_doc"].ToString() == "0")
                        if (dr["gdh_rec_doc"].ToString() == "0")
                        {
                            dr["ho_rec"] = "Pending";

                        }
                        else
                        {
                            dr["ho_rec"] = "All Sent";
                        }
                        //---------------------------------------------------------2
                        //if (dr["gdh_rec_doc"].ToString() == "1" && dr["gdh_send_reg"].ToString() == "0")
                        if (dr["gdh_rec_doc"].ToString() == "1")
                        {
                            dr["ho_allRec"] = "H/O Received";
                        }
                        else
                        {
                            dr["ho_allRec"] = "Pending";
                        }
                        //---------------------------------------------------------3
                        //if (dr["gdh_send_reg"].ToString() == "1" && dr["gdh_cr_rec"].ToString() == "0")
                        if (dr["gdh_send_reg"].ToString() == "1")
                        {
                            dr["send_RMV"] = "Sent to RMV";
                        }
                        else
                        {
                            dr["send_RMV"] = "Not Sent";
                        }
                        //---------------------------------------------------------4
                        //if (dr["gdh_cr_rec"].ToString() == "1" && dr["gdh_send_pay"].ToString() == "0")
                        if (dr["gdh_cr_rec"].ToString() == "1")
                        {
                            dr["reg_Status"] = "Registered";
                        }
                        else
                        {
                            dr["reg_Status"] = "Not Registered";
                        }
                        //---------------------------------------------------------4
                        //if (dr["gdh_send_pay"].ToString() == "1" && dr["gdh_coll_cheq"].ToString() == "0")
                        if (dr["gdh_send_pay"].ToString() == "1")
                        {
                            dr["send_Pay"] = "Sent For Pay";
                        }
                        else
                        {
                            dr["send_Pay"] = "Not Sent";
                        }

                    }
                }

                collorRows();
                //grvRecDoc.DataSource = null;
                //grvRecDoc.AutoGenerateColumns = false;
                //grvRecDoc.DataSource = dt_recDoc;


                //if (grvRecDoc.Rows.Count>0)
                //{
                //    foreach(DataGridViewRow dgvr in grvRecDoc.Rows)
                //    {
                //        if (dgvr.Cells["gdh_isse_doc"].Value.ToString() == "1" && dgvr.Cells["gdh_rec_doc"].Value.ToString() == "0")
                //        {
                //            //ho_receive
                //            dgvr.Cells["ho_receive"].Value = "All Received";
                //        }
                //        else
                //        {
                //            dgvr.Cells["ho_receive"].Value = "Pending";
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Quit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (tabControl1.SelectedIndex == 0)
            //{
            //    DisplayPrintButton();
            //    RecieptNo = "";
            //    txtADCompany.Focus();
            //}
            //else if (tabControl1.SelectedIndex == 1)
            //{
            //    HidePrintButton();
            //    RecieptNo = "";
            //    txtSRCompany.Focus();
            //}
            //else if (tabControl1.SelectedIndex == 2)
            //{
            //    HidePrintButton();
            //    RecieptNo = "";
            //    txtARCompany.Focus();
            //}
            //else if (tabControl1.SelectedIndex == 3)
            //{
            //    HidePrintButton();
            //    RecieptNo = "";
            //    txtSCCompany.Focus();
            //}
            //else {
            //    HidePrintButton();
            //    RecieptNo = "";
            //    txtJCCompany.Focus();
            //}
            if (tabControl1.SelectedTab.Name == "tabPage0")
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10137))
                {
                    MessageBox.Show("Sorry, You have no permission!\n( Advice: Reqired permission code :10137)");
                    tabControl1.SelectedIndex = _selTabPage;
                    return;
                }
                btnSave.Visible = false;
                btnPrint.Visible = false;
                _selTabPage = 0;
            }
            else
            {
                btnSave.Visible = true;
                btnPrint.Visible = true;
            }
            if (tabControl1.SelectedTab.Name == "tabPage0")
            {
                HidePrintButton();
                RecieptNo = "";

            }
            if (tabControl1.SelectedTab.Name == "tabPage1")
            {
                DisplayPrintButton();
                RecieptNo = "";
                _selTabPage = 1;
                txtADCompany.Focus();

            }
            else if (tabControl1.SelectedTab.Name == "tabPage2")
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10137))
                {
                    MessageBox.Show("Sorry, You have no permission!\n( Advice: Reqired permission code :10137)");
                    tabControl1.SelectedIndex = _selTabPage;
                    return;
                }
                // HidePrintButton();
                btnPrint.Visible = true;
                RecieptNo = "";
                _selTabPage = 2;
                txtSRCompany.Focus();
            }
            else if (tabControl1.SelectedTab.Name == "tabPage3")
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10137))
                {
                    MessageBox.Show("Sorry, You have no permission!\n( Advice: Reqired permission code :10137)");
                    tabControl1.SelectedIndex = _selTabPage;
                    return;
                }
                HidePrintButton();
                RecieptNo = "";
                _selTabPage = 3;
                txtARCompany.Focus();
            }
            else if (tabControl1.SelectedTab.Name == "tabPage4")
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10137))
                {
                    MessageBox.Show("Sorry, You have no permission!\n( Advice: Reqired permission code :10137)");
                    tabControl1.SelectedIndex = _selTabPage;
                    return;
                }
                HidePrintButton();
                RecieptNo = "";
                _selTabPage = 4;
                txtSCCompany.Focus();
            }
            else if (tabControl1.SelectedTab.Name == "tabPage5")
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10137))
                {
                    MessageBox.Show("Sorry, You have no permission!\n( Advice: Reqired permission code :10137)");
                    tabControl1.SelectedIndex = _selTabPage;
                    return;
                }
                HidePrintButton();
                RecieptNo = "";
                _selTabPage = 5;
                txtJCCompany.Focus();
            }
            else if (tabControl1.SelectedTab.Name == "tabPage6")
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10137))
                {
                    MessageBox.Show("Sorry, You have no permission!\n( Advice: Reqired permission code :10137)");
                    tabControl1.SelectedIndex = _selTabPage;
                    return;
                }
                _selTabPage = 6;
            }

        }

        private void VehicalRegistration_Load(object sender, EventArgs e)
        {
            try
            {
                //Application details
                txtADCompany.Text = BaseCls.GlbUserComCode;
                txtADProfitCenter.Text = BaseCls.GlbUserDefProf;
                //send rmv
                txtSRCompany.Text = BaseCls.GlbUserComCode;
                txtSRProfitCenter.Text = BaseCls.GlbUserDefProf;
                //assign registration
                txtARCompany.Text = BaseCls.GlbUserComCode;
                txtARProfitCenter.Text = BaseCls.GlbUserDefProf;
                //send customer
                txtSCCompany.Text = BaseCls.GlbUserComCode;
                txtSCProfitCenter.Text = BaseCls.GlbUserDefProf;
                //job close
                txtJCCompany.Text = BaseCls.GlbUserComCode;
                txtJCProfitCenter.Text = BaseCls.GlbUserDefProf;

                BindDistrict(cmbDistrict);
                cmbDistrict_SelectionChangeCommitted(null, null);
                txtADCompany.Select();

                collorRows();
                tabControl1.SelectedIndex = 1;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        private void cmbDistrict_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (cmbDistrict.SelectedValue == null) return;
                if (string.IsNullOrEmpty(cmbDistrict.SelectedValue.ToString())) return;
                DistrictProvince _type = CHNLSVC.Sales.GetDistrict(cmbDistrict.SelectedValue.ToString())[0];
                if (_type.Mds_district == null) { MessageBox.Show("Province information not available.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                txtProvince.Text = _type.Mds_province;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        #region main button events

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            /* Commented by shani on  10-06-2013 */
            //if (tabControl1.SelectedIndex == 0)
            //{
            //    ClearApplicationDetails();
            //}
            //else if (tabControl1.SelectedIndex == 1)
            //{
            //    ClearSendRMV();
            //}
            //else if (tabControl1.SelectedIndex == 2)
            //{
            //    ClearAssignRegistration();
            //}
            //else if (tabControl1.SelectedIndex == 3)
            //{
            //    ClearSendCustomer();
            //}
            //else
            //{
            //    ClearJobClose();
            //}
            //RecieptNo = "";
            try
            {
                if (tabControl1.SelectedTab.Name == "tabPage0") //new tab
                {
                    VehicalRegistration formnew = new VehicalRegistration();
                    formnew.MdiParent = this.MdiParent;
                    formnew.Location = this.Location;
                    formnew.Show();
                    this.Close();
                }
                if (tabControl1.SelectedTab.Name == "tabPage1")
                {
                    ClearApplicationDetails();
                }
                else if (tabControl1.SelectedTab.Name == "tabPage2")
                {
                    ClearSendRMV();
                }
                else if (tabControl1.SelectedTab.Name == "tabPage3")
                {
                    ClearAssignRegistration();
                }
                else if (tabControl1.SelectedTab.Name == "tabPage4")
                {
                    ClearSendCustomer();
                }
                else if (tabControl1.SelectedTab.Name == "tabPage5")
                {
                    ClearSubmit();
                }

                else //tabControl1.SelectedTab.Name == "tabPage1"
                {
                    ClearJobClose();
                }
                RecieptNo = "";
                _listVehRegComp = new List<VehicleRegCompany>();
                lblFinComp.Text = "";
                txtFinComp.Text = "";
                txtFinPOD.Text = "";
                txtFinRem.Text = "";
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string _error = "";
            try
            {
                Boolean _isSaveReg = false;
                DateTime _date = CHNLSVC.Security.GetServerDateTime();
                if (_date.Date != DateTime.Now.Date)
                {
                    MessageBox.Show("Your system date is not equal to server date! \nPlease contact system administrator....", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (MessageBox.Show("Do you want to save?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // if (tabControl1.SelectedIndex == 0) /* COMMENTED BY SHANI */
                    if (tabControl1.SelectedTab.Name == "tabPage1")
                    {
                        List<FF.BusinessObjects.VehicalRegistration> list = CHNLSVC.General.GetVehicalRegistrations(RecieptNo);
                        if (list != null)
                        {
                            int capacity;
                            int hp;
                            int wheelBase;
                            int weight;
                            int manf;
                            int result = 0;
                            
                            if (list[0].P_srvt_rmv_stus == 0)
                            {
                                if (!Int32.TryParse(txtCapacity.Text, out capacity))
                                {
                                    MessageBox.Show("Enter capacity in number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                if (!Int32.TryParse(txtHorsePower.Text, out hp))
                                {
                                    MessageBox.Show("Enter horse power in number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                if (!Int32.TryParse(txtWheelBase.Text, out wheelBase))
                                {
                                    MessageBox.Show("Enter wheel base in number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                if (!Int32.TryParse(txtWeight.Text, out weight))
                                {
                                    MessageBox.Show("Enter weight in number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                if (!Int32.TryParse(txtManfYear.Text, out manf))
                                {
                                    MessageBox.Show("Enter manf. year in number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                if (cmbIDType.SelectedItem == null)
                                {
                                    MessageBox.Show("Please select ID Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                if (cmbCustomerTitle.SelectedItem == null)
                                {
                                    MessageBox.Show("Please select customer tite", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                if (cmbDistrict.SelectedValue == null)
                                {
                                    MessageBox.Show("Please select district", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                if (!IsValidNIC(txtADID.Text) && cmbIDType.SelectedItem.ToString() == "NIC")
                                {
                                    MessageBox.Show("NIC format invalid.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }

                                list[0].P_svrt_id_tp = cmbIDType.SelectedItem.ToString();
                                list[0].P_svrt_id_ref = txtADID.Text;
                                list[0].P_svrt_cust_title = cmbCustomerTitle.SelectedItem.ToString();
                                list[0].P_svrt_last_name = txtLastName.Text.ToUpper();
                                list[0].P_svrt_full_name = txtFullAnme.Text.ToUpper();
                                list[0].P_svrt_initial = txtInitials.Text.ToUpper();
                                list[0].P_svrt_add01 = txtCustomerAddressLine1.Text.ToUpper();
                                list[0].P_svrt_add02 = txtCustomerAddressLine2.Text.ToUpper();
                                list[0].P_svrt_city = txtCity.Text.ToUpper();
                                list[0].P_svrt_district = cmbDistrict.SelectedValue.ToString().ToUpper();
                                list[0].P_svrt_cust_cd = txtADCusCode.Text;
                                list[0].P_svrt_province = txtProvince.Text.ToUpper();
                                list[0].P_svrt_contact = txtContactNo.Text;
                                list[0].P_svrt_model = txtModel.Text.ToUpper();
                                list[0].P_svrt_brd = txtBrand.Text.ToUpper();
                                //list[0].P_svrt_chassis = TextBoxChassie.Text;
                                //list[0].P_svrt_engine = TextBoxEngine.Text;
                                list[0].P_svrt_color = txtColour.Text.ToUpper();
                                list[0].P_svrt_fuel = txtFuel.Text.ToUpper();
                                list[0].P_svrt_capacity = Convert.ToDecimal(txtCapacity.Text);
                                list[0].P_svrt_unit = txtUnit.Text.ToUpper();
                                list[0].P_svrt_horse_power = Convert.ToDecimal(txtHorsePower.Text);
                                list[0].P_svrt_wheel_base = Convert.ToDecimal(txtWheelBase.Text);
                                list[0].P_svrt_tire_front = txtFrontTire.Text.ToUpper();
                                list[0].P_svrt_tire_rear = txtRearTire.Text.ToUpper();
                                list[0].P_svrt_weight = Convert.ToDecimal(txtWeight.Text);
                                list[0].P_svrt_man_year = Convert.ToDecimal(txtManfYear.Text);
                                list[0].P_svrt_import = txtImportLicense.Text;
                                list[0].P_svrt_importer = txtImporter.Text.ToUpper();
                                list[0].P_svrt_authority = txtAuthority.Text.ToUpper();
                                list[0].P_svrt_country = txtCountry.Text.ToUpper();
                                list[0].P_srvt_cust_dt = dateTimePickerCustomDate.Value.Date;
                                list[0].P_svrt_clear_dt = dateTimePickerClearenceDate.Value.Date;
                                list[0].P_svrt_declear_dt = dateTimePickerDeclearDate.Value.Date;
                                list[0].P_svrt_importer = txtImporter.Text.ToUpper();
                                list[0].P_svrt_importer_add01 = txtImportAddress1.Text.ToUpper();
                                list[0].P_svrt_importer_add02 = txtImportAddress2.Text.ToUpper();

                                result = CHNLSVC.General.SaveVehicalRegistration(list[0],out _error);
                            }
                            else
                            {
                                MessageBox.Show("Can not save after send to RMV!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            if (result > 0)
                            {
                                MessageBox.Show("Records updated successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show(_error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            //clear controls
                            ClearApplicationDetails();
                        }
                        else
                        {
                            MessageBox.Show("Please select valid receipt", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    //else if (tabControl1.SelectedIndex == 1)  /* COMMENTED BY SHANI */
                    else if (tabControl1.SelectedTab.Name == "tabPage2")
                    {
                        List<FF.BusinessObjects.VehicalRegistration> list = CHNLSVC.General.GetVehicalRegistrations(RecieptNo);
                        if (list != null)
                        {
                            //status updates
                            list[0].P_srvt_rmv_stus = 1;
                            list[0].P_srvt_rmv_by = BaseCls.GlbUserID;
                            list[0].P_srvt_rmv_dt = dateTimePickerSendDate.Value.Date;
                            list[0].SRVT_RMV_TIME = DateTime.Now;
                            int result = CHNLSVC.General.SaveVehicalRegistration(list[0],out _error);
                            //ADDED 2013/06/13
                            //UPDATE TABLE gen_doc_pro_det

                            CreditSaleDocsHeader _header = new CreditSaleDocsHeader();
                            _header.Gdh_chassis = list[0].P_svrt_chassis;
                            _header.Gdh_engine = list[0].P_svrt_engine;
                            _header.Gdh_rec = list[0].P_srvt_ref_no;
                            _header.Gdh_inv = list[0].P_svrt_inv_no;
                            _header.Gdh_reg_rmks = txtRmvRemark.Text;

                            int sendRes = CHNLSVC.Sales.Update_Veh_RMVSend(_header.Gdh_inv, _header.Gdh_rec, _header.Gdh_engine, _header.Gdh_chassis, dateTimePickerSendDate.Value.Date, _header.Gdh_reg_rmks);
                            //END


                            ClearSendRMV();
                            if (result > 0)
                            {
                                MessageBox.Show("Record updated successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show(_error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please select valid reciept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    // else if (tabControl1.SelectedIndex == 2)  /* COMMENTED BY SHANI */
                    else if (tabControl1.SelectedTab.Name == "tabPage3")
                    {
                        List<FF.BusinessObjects.VehicalRegistration> list = CHNLSVC.General.GetVehicalRegistrations(RecieptNo);
                        if (list != null)
                        {
                            //validation
                            if (chkRec.Checked == false)
                            {
                                CreateRegistrationNumber();
                                if (lblRegistrationNo.Text.Length < 8)
                                {
                                    MessageBox.Show("Invalid registration number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }

                                string pattern = @"^[a-z]{4,5}[-][0-9]{4}";
                                System.Text.RegularExpressions.Match match = Regex.Match(lblRegistrationNo.Text.Trim(), pattern, RegexOptions.IgnoreCase);
                                if (!match.Success)
                                {
                                    MessageBox.Show("Invalid registration number format\nFormat Should be XXXX(X)-1234", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                            //check whether reg no is available



                            if (chkRec.Checked == true)   //kapila 4/11/2014
                            {
                                list[0].SRVT_PLT_REC_BY = BaseCls.GlbUserID;
                                list[0].SRVT_PLT_REC_DT = dtNumPltRec.Value.Date;
                                list[0].SRVT_PLT_REC_TIME = DateTime.Now;
                                list[0].SRVT_PLT_REC_REM = txtNumPltRecRem.Text;
                            }

                            //kapila 15/3/2016
                            if (chkResend.Checked == true)
                            {
                                list[0].SRVT_IS_RESEND = 1;
                                list[0].SRVT_RESEND_DT = Convert.ToDateTime(dtResend.Value.Date);
                                list[0].SRVT_RESEND_REM = txtResendRem.Text;
                            }
                            if (chkRerec.Checked == true)
                            {
                                list[0].SRVT_IS_REREC = 1;
                                list[0].SRVT_REREC_DT = Convert.ToDateTime(dtRerec.Value.Date);
                            }

                            //status updates
                            if (!string.IsNullOrEmpty(lblRegistrationNo.Text))
                            {
                                Boolean _chk = CHNLSVC.General.IsVehRegNoExist(BaseCls.GlbUserComCode, RecieptNo, lblRegistrationNo.Text);
                                if (_chk == true)
                                {
                                    MessageBox.Show("This registration number is already available", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                _isSaveReg = true;
                                list[0].P_svrt_veh_reg_no = lblRegistrationNo.Text;
                                list[0].P_svrt_reg_by = BaseCls.GlbUserID;
                                list[0].P_svrt_reg_dt = Convert.ToDateTime(dateTimePickerARRegDate.Value).Date;
                                list[0].SVRT_REG_TIME = DateTime.Now;
                            }

                            if (txtFileName.Text != "")
                            {
                                string[] ext = (txtFileName.Text.Split('\\')[txtFileName.Text.Split('\\').Length - 1]).Split('.');
                                list[0].P_svrt_image = DateTime.Now.ToString().Replace('/', ' ').Replace(':', ' ') + "." + ext[ext.Length - 1];
                                System.IO.File.Copy(txtFileName.Text, "\\\\192.168.1.20\\Public\\Image Upload\\" + DateTime.Now.ToString().Replace('/', ' ').Replace(':', ' ') + "." + ext[ext.Length - 1]);
                            }

                            //TODO:IMAGE UPLOAD

                            //ADDED BY SACHITH 2012/11/08
                            //UPDATE INR_SERMST
                            CHNLSVC.General.UpdateVehReg(list[0]);
                            //END

                            CHNLSVC.General.SaveVehicalRegistration(list[0], out _error);
                            //update insurance
                            if (list[0].P_srvt_insu_ref != null && list[0].P_srvt_insu_ref != "")
                            {
                                CHNLSVC.General.UpdateInsuranceFromReg(lblRegistrationNo.Text, BaseCls.GlbUserID, dateTimePickerARRegDate.Value, list[0].P_srvt_insu_ref);
                            }

                            //ADDED 2013/06/13
                            //UPDATE table gen_doc_pro_hdr

                            CreditSaleDocsHeader _header = new CreditSaleDocsHeader();
                            _header.Gdh_chassis = list[0].P_svrt_chassis;
                            _header.Gdh_engine = list[0].P_svrt_engine;
                            _header.Gdh_rec = list[0].P_srvt_ref_no;
                            _header.Gdh_inv = list[0].P_svrt_inv_no;
                            _header.Gdh_cr_rmks = txtAssignRegRemk.Text;
                            _header.Gdh_cr_no = lblRegistrationNo.Text;

                            int crRes = CHNLSVC.Sales.Update_CR_Recieve(_header.Gdh_inv, _header.Gdh_rec, _header.Gdh_engine, _header.Gdh_chassis, dateTimePickerARRegDate.Value.Date, _header.Gdh_cr_rmks, _header.Gdh_cr_no);

                            //kapila 19/5/2016 send sms to customer
                            List<FF.BusinessObjects.VehicalRegistration> _listV = null;
                            string _mobNumber = "";
                            OutSMS smsout = new OutSMS();
                            if (_isSaveReg == true)
                            {
                                _listV = CHNLSVC.General.GetVehicalRegistrations(RecieptNo);
                                if (_listV != null)
                                {

                                    MasterBusinessEntity _masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(null, _listV[0].P_svrt_cust_cd, null, string.Empty, "C");
                                    if (_masterBusinessCompany.Mbe_cd != null)
                                    {

                                        _mobNumber = _masterBusinessCompany.Mbe_mob;
                                        bool isValid = ValidateMobileNo(_mobNumber);
                                        if (isValid == true)
                                        {

                                            MasterItem _itmMas = CHNLSVC.Inventory.GetItem(null, _listV[0].P_srvt_itm_cd);

                                            string _msg = "Dear Hero Family Member.Thank you for purchasing Hero " + _itmMas.Mi_model + " ,your bike has now been registered.Vehicle No " + lblRegistrationNo.Text + " Assuring you unique service at all the times.Thank you";

                                            if (_mobNumber.Length == 10)
                                            {
                                                smsout.Receiverphno = "+94" + _mobNumber.Substring(1, 9);
                                                smsout.Senderphno = "+94" + _mobNumber.Substring(1, 9);
                                            }
                                            if (_mobNumber.Length == 9)
                                            {
                                                smsout.Receiverphno = "+94" + _mobNumber;
                                                smsout.Senderphno = "+94" + _mobNumber;
                                            }

                                            smsout.Msg = _msg;
                                            smsout.Receiver = BaseCls.GlbUserDefProf;
                                            smsout.Sender = BaseCls.GlbUserID;
                                            smsout.Seqno = 0;
                                            smsout.Msgstatus = 0;
                                            smsout.Msgtype = "GEN_E";
                                            smsout.Createtime = DateTime.Now;

                                            Int32 errroCode = CHNLSVC.General.SaveSMSOut(smsout);
                                        }

                                    }
                                }
                            }

                            //END


                            ClearAssignRegistration();
                            MessageBox.Show("Record updated successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Please select valid reciept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    //else if (tabControl1.SelectedIndex == 3)  /* COMMENTED BY SHANI */
                    else if (tabControl1.SelectedTab.Name == "tabPage4")
                    {
                        List<FF.BusinessObjects.VehicalRegistration> list = CHNLSVC.General.GetVehicalRegistrations(RecieptNo);
                        if (list != null)
                        {
                            //if (string.IsNullOrEmpty(txtNumberPodNo.Text))
                            //{
                            //    MessageBox.Show("Please enter Number Plate Couriered POD Number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //    return;
                            //}

                            list[0].P_srvt_cust_by = BaseCls.GlbUserID;
                            list[0].P_srvt_cust_dt = _date;
                            list[0].Srvt_cr_pod_ref = txtCRNumber.Text;
                            list[0].SRVT_CR_COUR_DT = dateTimePickerCRCourriedDate.Value;   //kapila 17/3/2016
                            list[0].Srvt_no_plt_dt = dateTimePickerNumberCouriedDate.Value;
                            list[0].Srvt_no_plt_pod_ref = txtNumberPodNo.Text;
                            list[0].SRVT_NO_PLT_TIME = DateTime.Now;
                            list[0].SRVT_CUST_TIME = DateTime.Now;
                            if (txtNumberPodNo.Text != "")
                                list[0].P_srvt_cust_stus = 1;


                            //job close if sales type != HP
                            if (list[0].P_svrt_sales_tp == "CS")
                            {
                                list[0].P_srvt_cls_dt = _date;
                                list[0].P_srvt_cls_by = BaseCls.GlbUserID;
                                list[0].P_srvt_cls_stus = 1;
                                list[0].SRVT_CLS_TIME = DateTime.Now;
                            }

                            //kapila 15/3/2016
                            if (optPC.Checked == true)
                                list[0].srvt_cour_to = "P";
                            else
                                list[0].srvt_cour_to = "F";

                            if (chkRetHO.Checked == true)
                            {
                                list[0].SRVT_IS_RET_HO = 1;
                                list[0].SRVT_RET_HO_DT = Convert.ToDateTime(dtRetDt.Value.Date);
                                list[0].SRVT_RET_HO_REM = txtRetRem.Text;
                            }
                            //kapila 27/5/2016
                            if (chkTax.Checked)
                            {
                                list[0].SRVT_IS_TX_RP = 1;
                                list[0].SRVT_IS_TX_DT = Convert.ToDateTime(dtpTaxDt.Value.Date);
                                list[0].SRVT_IS_TX_REM = txtRemTax.Text;
                            }
                            if (chkRecRP.Checked)
                            {
                                list[0].SRVT_IS_REC_RP = 1;
                                list[0].SRVT_IS_REC_DT = Convert.ToDateTime(dtpRP.Value.Date);
                                list[0].SRVT_IS_REC_REM = txtRemRP.Text;
                            }

                            int result = CHNLSVC.General.SaveVehicalRegistration(list[0], out _error);
                            if (result > 0)
                            {
                                string _mobNumber = "";
                                OutSMS smsout = new OutSMS();
                                MasterBusinessEntity _masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(null, list[0].P_svrt_cust_cd, null, string.Empty, "C");
                                if (_masterBusinessCompany.Mbe_cd != null)
                                {
                                    _mobNumber = _masterBusinessCompany.Mbe_mob;
                                    bool isValid = ValidateMobileNo(_mobNumber);
                                    if (isValid == true)
                                    {

                                        MasterItem _itmMas = CHNLSVC.Inventory.GetItem(null, list[0].P_srvt_itm_cd);

                                        string _msg = "Dear Hero Family Member.Thank you for purchasing Hero " + _itmMas.Mi_model + ".We have received your number plate.It will delivered to nearest Hero showroom/dealer soon.They will call you for collection.Assuring you unique service at all the times.Thank you";

                                        if (_mobNumber.Length == 10)
                                        {
                                            smsout.Receiverphno = "+94" + _mobNumber.Substring(1, 9);
                                            smsout.Senderphno = "+94" + _mobNumber.Substring(1, 9);
                                        }
                                        if (_mobNumber.Length == 9)
                                        {
                                            smsout.Receiverphno = "+94" + _mobNumber;
                                            smsout.Senderphno = "+94" + _mobNumber;
                                        }

                                        smsout.Msg = _msg;
                                        smsout.Receiver = BaseCls.GlbUserDefProf;
                                        smsout.Sender = BaseCls.GlbUserID;
                                        smsout.Seqno = 0;
                                        smsout.Msgstatus = 0;
                                        smsout.Msgtype = "GEN_E";
                                        smsout.Createtime = DateTime.Now;

                                        Int32 errroCode = CHNLSVC.General.SaveSMSOut(smsout);
                                    }

                                }

                                MessageBox.Show("Record updated sucessfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show(_error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            ClearSendCustomer();
                        }
                        //else  /* COMMENTED BY SHANI */
                        else if (tabControl1.SelectedTab.Name == "tabPage5")
                        {
                            MessageBox.Show("Please select valid reciept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }


                    else if (tabControl1.SelectedTab.Name == "tabPage6")
                    { // Docuemtn Submit Nadeeka 05-11-2015

                        if (_submitDoc.Rows.Count > 0)
                        {


                            int sendRes = CHNLSVC.Sales.Update_Veh_submit_Date(_submitDoc);
                            //END


                            ClearSubmit();
                            if (sendRes > 0)
                            {
                                MessageBox.Show("Record updated successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Nothing saved", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please select valid reciept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    else
                    {
                        List<FF.BusinessObjects.VehicalRegistration> list = CHNLSVC.General.GetVehicalRegistrations(RecieptNo);
                        if (list != null)
                        {
                            //status updates
                            list[0].P_srvt_cls_dt = dateTimePickerJobCloseDate.Value;
                            list[0].P_srvt_cls_by = BaseCls.GlbUserID;
                            list[0].P_srvt_cls_stus = 1;
                            int result = CHNLSVC.General.SaveVehicalRegistration(list[0], out _error);
                            if (result > 0)
                            {
                                //kapila 7/6/2016
                                int _efc = CHNLSVC.Sales.SaveVehRegCompany(_listVehRegComp);
                                MessageBox.Show("Record updated sucessfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _listVehRegComp = new List<VehicleRegCompany>();
                                grvFinComp.DataSource = new List<VehicleRegCompany>();
                                grvFinComp.DataSource = _listVehRegComp;
                                lblFinComp.Text = "";
                                txtFinComp.Text = "";
                                txtFinPOD.Text = "";
                                txtFinRem.Text = "";

                            }
                            else
                            {
                                MessageBox.Show(_error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }

                        else
                        {
                            MessageBox.Show("Please select valid reciept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private bool ValidateMobileNo(string num)
        {
            int intNum = 0;
            //check only contain degits
            if (!int.TryParse(num, out intNum))
                return false;
            //check for length
            else
            {
                if (num.Length < 10)
                {
                    return false;
                }
                //check for first three chars
                else
                {
                    string firstChar = num.Substring(0, 3);
                    if (firstChar != "071" && firstChar != "077" && firstChar != "078" && firstChar != "072" && firstChar != "075" && firstChar != "076" && firstChar != "074")
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string _error = "";
            try
            {
                if (tabControl1.SelectedTab.Name == "tabPage1")
                {
                    List<FF.BusinessObjects.VehicalRegistration> list = CHNLSVC.General.GetVehicalRegistrations(RecieptNo);
                    if (list != null)
                    {
                        list[0].P_svrt_prnt_stus = 1;
                        list[0].P_svrt_prnt_by = BaseCls.GlbUserID;
                        list[0].P_svrt_prnt_dt = DateTime.Now;
                        CHNLSVC.General.SaveVehicalRegistration(list[0], out _error);

                        //TODO: NEED TO ADD PRINT
                        BaseCls.GlbReportDoc = RecieptNo;       //kapila 17/5/2016
                        Reports.Reconciliation.clsRecon obj = new Reports.Reconciliation.clsRecon();
                        obj.VehRegAppReport1();

                        //MessageBox.Show("Print under construction", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Please select receipt number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                if (tabControl1.SelectedTab.Name == "tabPage2")
                {
                    //Reports.Sales.ReportViewer _view1 = new Reports.Sales.ReportViewer();
                    //BaseCls.GlbReportName = string.Empty;
                    //_view1.GlbReportName = string.Empty;
                    //_view1.GlbReportName = "VehicleRegistrationSlip.rpt";
                    //BaseCls.GlbReportName = "VehicleRegistrationSlip.rpt";
                    //_view1.GlbReportDoc = RecieptNo;
                    //BaseCls.GlbReportDoc = RecieptNo;
                    //_view1.Show();
                    //_view1 = null;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        #endregion

        #region main button hide/display

        private void DisplayPrintButton()
        {
            btnPrint.Visible = true;
            toolStripSeparator1.Visible = true;
        }

        private void HidePrintButton()
        {
            btnPrint.Visible = false;
            toolStripSeparator1.Visible = false;
        }

        #endregion

        #region clear methods

        private void ClearApplicationDetails()
        {
            RecieptNo = "";

            txtADCompany.Text = "";
            txtADProfitCenter.Text = "";
            txtADAccount.Text = "";
            txtADInvoice.Text = "";
            txtADReciept.Text = "";
            txtADVehNo.Text = "";
            txtADEngine.Text = "";
            txtADChassis.Text = "";

            cmbCustomerTitle.SelectedIndex = 0;
            cmbIDType.SelectedIndex = 0;
            cmbDistrict.SelectedIndex = 0;
            txtADRegAmt.Text = "";
            txtADClaimAmt.Text = "";
            txtADInvNo.Text = "";
            txtADSalesType.Text = "";
            dateTimePickerRecieptDate.Value = DateTime.Now;
            txtADSaleAmt.Text = "";

            //cus details
            txtADID.Text = "";
            txtLastName.Text = "";
            txtFullAnme.Text = "";
            txtInitials.Text = "";
            txtCustomerAddressLine1.Text = "";
            txtCustomerAddressLine2.Text = "";
            txtCity.Text = "";
            txtProvince.Text = "";
            txtContactNo.Text = "";

            //vehical details
            txtADCusCode.Text = "";
            txtModel.Text = "";
            txtBrand.Text = "";
            txtChassie.Text = "";
            txtEngine.Text = "";
            txtColour.Text = "";
            txtFuel.Text = "";
            txtCapacity.Text = "";
            txtUnit.Text = "";
            txtHorsePower.Text = "";
            txtWheelBase.Text = "";
            txtFrontTire.Text = "";
            txtRearTire.Text = "";
            txtWeight.Text = "";
            txtManfYear.Text = "";
            txtImporter.Text = "";
            txtImportLicense.Text = "";
            txtAuthority.Text = "";
            txtCountry.Text = "";
            dateTimePickerCustomDate.Value = DateTime.Now;
            dateTimePickerClearenceDate.Value = DateTime.Now;
            dateTimePickerDeclearDate.Value = DateTime.Now;

            //importer details
            txtImporter.Text = "";
            txtImportAddress1.Text = "";
            txtImportAddress2.Text = "";

            //cus details
            btnSearchCustomer.Enabled = true;
            cmbIDType.Enabled = true;
            cmbCustomerTitle.Enabled = true;
            txtADID.ReadOnly = true;
            txtLastName.ReadOnly = false;
            txtFullAnme.ReadOnly = false;
            txtInitials.ReadOnly = false;
            txtCustomerAddressLine1.ReadOnly = false;
            txtCustomerAddressLine2.ReadOnly = false;
            txtCity.ReadOnly = false;
            cmbDistrict.Enabled = true;
            txtProvince.ReadOnly = false;
            txtContactNo.ReadOnly = false;

            //vehical details
            txtADCusCode.ReadOnly = false;
            txtModel.ReadOnly = false;
            txtBrand.ReadOnly = false;
            txtColour.ReadOnly = false;
            txtFuel.ReadOnly = false;
            txtCapacity.ReadOnly = false;
            txtUnit.ReadOnly = false;
            txtHorsePower.ReadOnly = false;
            txtWheelBase.ReadOnly = false;
            txtFrontTire.ReadOnly = false;
            txtRearTire.ReadOnly = false;
            txtWeight.ReadOnly = false;
            txtManfYear.ReadOnly = false;
            txtImporter.ReadOnly = false;
            txtImportLicense.ReadOnly = false;
            txtAuthority.ReadOnly = false;
            txtCountry.ReadOnly = false;
            dateTimePickerCustomDate.Enabled = true;
            dateTimePickerClearenceDate.Enabled = true;
            dateTimePickerDeclearDate.Enabled = true;

            //importer details
            txtImporter.ReadOnly = false;
            txtImportAddress1.ReadOnly = false;
            txtImportAddress2.ReadOnly = false;

            cmbDistrict_SelectionChangeCommitted(null, null);
            txtADCompany.Text = BaseCls.GlbUserComCode;
            txtADProfitCenter.Text = BaseCls.GlbUserDefProf;
            dataGridViewADSearchResult.DataSource = null;

        }

        private void ClearSendRMV()
        {
            RecieptNo = "";

            txtSRCompany.Text = "";
            txtSRProfitCenter.Text = "";
            txtSRRecieptNo.Text = "";
            txtSRInvoiceNo.Text = "";
            txtSREngineNo.Text = "";
            txtSRChassiseNo.Text = "";
            txtSRAccNo.Text = "";
            txtRmvRemark.Text = "";
            dateTimePickerSendDate.Value = DateTime.Now;
            txtSRCompany.Text = BaseCls.GlbUserComCode;
            txtSRProfitCenter.Text = BaseCls.GlbUserDefProf;
            dataGridViewSRSearchResult.DataSource = null;
        }

        private void ClearSubmit()
        {
            RecieptNo = "";

            txtRec.Text = "";
            txtcom.Text = "";
            txtAcc.Text = "";
            txtEngineNo.Text = "";
            txtLoc.Text = "";
            txtCustomer.Text = "";
            txtChasis.Text = "";
            txtInv.Text = "";
            txtPc.Text = "";
            dtpApply.Value = DateTime.Now;
            txtcom.Text = BaseCls.GlbUserComCode;
            txtPc.Text = BaseCls.GlbUserDefProf;
            dgvSubmit.DataSource = null;
        }

        private void ClearAssignRegistration()
        {
            RecieptNo = "";

            txtARCompany.Text = "";
            txtARProfitCenter.Text = "";
            txtARRecieptNo.Text = "";
            txtARInvoiceNo.Text = "";
            txtAREngineNo.Text = "";
            txtARChassieNo.Text = "";
            txtARAccountNo.Text = "";

            txtARRegNo1.Text = "";

            lblRegistrationNo.Text = "";
            txtARRegNo1.Text = "";
            txtARRegNo2.Text = "";
            txtARRegNo3.Text = "";
            txtARRegNo4.Text = "";
            txtARRegNo5.Text = "";
            txtARRegNo6.Text = "";
            txtARRegNo7.Text = "";
            txtARRegNo8.Text = "";
            txtARRegNo9.Text = "";
            txtFileName.Text = "";
            txtAssignRegRemk.Text = "";

            dateTimePickerARRegDate.Value = DateTime.Now;
            txtARCompany.Text = BaseCls.GlbUserComCode;
            txtARProfitCenter.Text = BaseCls.GlbUserDefProf;
            dataGridViewARSearchResult.DataSource = null;
            dtNumPltRec.Value = DateTime.Now;

        }

        private void ClearSendCustomer()
        {
            RecieptNo = "";

            txtSCCompany.Text = "";
            txtSCProfitCenter.Text = "";
            txtSCRecieptNo.Text = "";
            txtSCInvoiceNo.Text = "";
            txtSCEngineNo.Text = "";
            txtSCChassieNo.Text = "";
            txtSCAccountNo.Text = "";

            dateTimePickerCRCourriedDate.Value = DateTime.Now;
            dateTimePickerNumberCouriedDate.Value = DateTime.Now;
            txtCRNumber.Text = "";
            txtNumberPodNo.Text = "";
            txtSCCompany.Text = BaseCls.GlbUserComCode;
            txtSCProfitCenter.Text = BaseCls.GlbUserDefProf;
            dataGridViewSCSearchResult.DataSource = null;
        }

        private void ClearJobClose()
        {
            RecieptNo = "";

            txtJCCompany.Text = "";
            txtJCProfitCenter.Text = "";
            txtJCReciept.Text = "";
            txtJCInvoice.Text = "";
            txtJCEngineNo.Text = "";
            txtJCChassieNo.Text = "";
            txtJCAccount.Text = "";
            txtJCVehicalNo.Text = "";

            dateTimePickerJobCloseDate.Value = DateTime.Now;
            txtJCCompany.Text = BaseCls.GlbUserComCode;
            txtJCProfitCenter.Text = BaseCls.GlbUserDefProf;
            dataGridViewJCSearchResult.DataSource = null;
        }

        #endregion

        #region search buttons

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewADSearchResult.AutoGenerateColumns = false;
                DataTable dt = CHNLSVC.General.GetVehicalSearch(txtADCompany.Text, txtADProfitCenter.Text, "Registration", txtADVehNo.Text, txtADChassis.Text, txtADEngine.Text, txtADInvoice.Text, txtADReciept.Text, txtADAccount.Text, DateTime.Today, DateTime.Today, null);
                dataGridViewADSearchResult.DataSource = dt;
                //cmbIDType.Focus();
                dataGridViewADSearchResult.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSRSearch_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewSRSearchResult.AutoGenerateColumns = false;
                DataTable dt = CHNLSVC.General.GetVehicalSearch(txtSRCompany.Text, txtSRProfitCenter.Text, "SendRMV", "", txtSRChassiseNo.Text, txtSREngineNo.Text, txtSRInvoiceNo.Text, txtSRRecieptNo.Text, txtSRAccNo.Text, DateTime.Today, DateTime.Today, null);
                dataGridViewSRSearchResult.DataSource = dt;
                dateTimePickerSendDate.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnARSearch_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewARSearchResult.AutoGenerateColumns = false;
                DataTable dt = CHNLSVC.General.GetVehicalSearch(txtARCompany.Text, txtARProfitCenter.Text, "AssignReg", txtARVehNo.Text, txtARChassieNo.Text, txtAREngineNo.Text, txtARInvoiceNo.Text, txtARRecieptNo.Text, txtARAccountNo.Text, DateTime.Today, DateTime.Today, null);
                dataGridViewARSearchResult.DataSource = dt;
                txtARRegNo1.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        private void btnSCSearch_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewSCSearchResult.AutoGenerateColumns = false;
                DataTable dt = CHNLSVC.General.GetVehicalSearch(txtSCCompany.Text, txtSCProfitCenter.Text, "SendCus", txtSCVehicalNo.Text, txtSCChassieNo.Text, txtSCEngineNo.Text, txtSCInvoiceNo.Text, txtSCRecieptNo.Text, txtSCAccountNo.Text, DateTime.Today, DateTime.Today, null);
                dataGridViewSCSearchResult.DataSource = dt;
                dateTimePickerCRCourriedDate.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnJCSesarch_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewJCSearchResult.AutoGenerateColumns = false;
                DataTable dt = CHNLSVC.General.GetVehicalSearch(txtJCCompany.Text, txtJCProfitCenter.Text, "AssignReg", txtJCVehicalNo.Text, txtJCChassieNo.Text, txtJCEngineNo.Text, txtJCInvoice.Text, txtJCReciept.Text, txtJCAccount.Text, DateTime.Today, DateTime.Today, null);
                dataGridViewJCSearchResult.DataSource = dt;
                dateTimePickerJobCloseDate.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        #endregion

        #region common search

        #region application details

        private void btnADCompany_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanySearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtADCompany;
                _CommonSearch.txtSearchbyword.Text = txtADCompany.Text;
                _CommonSearch.ShowDialog();
                txtADCompany.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnADAccount_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
                DataTable _result = CHNLSVC.CommonSearch.GetHpAccountSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtADAccount;
                _CommonSearch.txtSearchbyword.Text = txtADAccount.Text;
                _CommonSearch.ShowDialog();
                txtADAccount.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnADProfitCenter_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtADProfitCenter;
                _CommonSearch.txtSearchbyword.Text = txtADProfitCenter.Text;
                _CommonSearch.ShowDialog();
                txtADProfitCenter.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSearchCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_CommonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtADCusCode;
                _CommonSearch.txtSearchbyword.Text = txtADCusCode.Text;
                _CommonSearch.ShowDialog();
                txtADCusCode.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        #endregion

        #region send to rmv

        private void btnSRCompanySearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanySearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSRCompany;
                _CommonSearch.txtSearchbyword.Text = txtSRCompany.Text;
                _CommonSearch.ShowDialog();
                txtSRCompany.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSRAccount_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
                DataTable _result = CHNLSVC.CommonSearch.GetHpAccountSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSRAccNo;
                _CommonSearch.txtSearchbyword.Text = txtSRAccNo.Text;
                _CommonSearch.ShowDialog();
                txtSRAccNo.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSRProfitCenter_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSRProfitCenter;
                _CommonSearch.txtSearchbyword.Text = txtSRProfitCenter.Text;
                _CommonSearch.ShowDialog();
                txtSRProfitCenter.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        #endregion

        #region assign registration number


        private void btnARCompany_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanySearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtARCompany;
                _CommonSearch.txtSearchbyword.Text = txtARCompany.Text;
                _CommonSearch.ShowDialog();
                txtARCompany.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnARProfitCenter_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtARProfitCenter;
                _CommonSearch.txtSearchbyword.Text = txtARProfitCenter.Text;
                _CommonSearch.ShowDialog();
                txtARProfitCenter.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnARAccount_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
                DataTable _result = CHNLSVC.CommonSearch.GetHpAccountSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtARAccountNo;
                _CommonSearch.txtSearchbyword.Text = txtARAccountNo.Text;
                _CommonSearch.ShowDialog();
                txtARAccountNo.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        #endregion

        #region send customer

        private void btnSCCompany_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanySearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtADCompany;
                _CommonSearch.txtSearchbyword.Text = txtADCompany.Text;
                _CommonSearch.ShowDialog();
                txtSCCompany.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSCProfitCenter_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSCProfitCenter;
                _CommonSearch.txtSearchbyword.Text = txtSCProfitCenter.Text;
                _CommonSearch.ShowDialog();
                txtSCProfitCenter.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSCAccount_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
                DataTable _result = CHNLSVC.CommonSearch.GetHpAccountSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSCAccountNo;
                _CommonSearch.txtSearchbyword.Text = txtSCAccountNo.Text;
                _CommonSearch.ShowDialog();
                txtSCAccountNo.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        #endregion

        #region job close

        private void btnJCCompany_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanySearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtADCompany;
                _CommonSearch.txtSearchbyword.Text = txtADCompany.Text;
                _CommonSearch.ShowDialog();
                txtJCCompany.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnJCProfitCenter_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtJCProfitCenter;
                _CommonSearch.txtSearchbyword.Text = txtJCProfitCenter.Text;
                _CommonSearch.ShowDialog();
                txtJCProfitCenter.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnJCAccount_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
                DataTable _result = CHNLSVC.CommonSearch.GetHpAccountSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtJCAccount;
                _CommonSearch.txtSearchbyword.Text = txtJCAccount.Text;
                _CommonSearch.ShowDialog();
                txtJCAccount.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        #endregion

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append("LEASE" + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        if (tabControl1.SelectedIndex == 0)
                        {
                            paramsText.Append(BaseCls.GlbUserID + seperator + txtADCompany.Text + seperator);
                            break;
                        }
                        else if (tabControl1.SelectedIndex == 1)
                        {
                            paramsText.Append(BaseCls.GlbUserID + seperator + txtSRCompany.Text + seperator);
                            break;
                        }
                        else if (tabControl1.SelectedIndex == 2)
                        {
                            paramsText.Append(BaseCls.GlbUserID + seperator + txtARCompany.Text + seperator);
                            break;
                        }
                        else if (tabControl1.SelectedIndex == 3)
                        {
                            paramsText.Append(BaseCls.GlbUserID + seperator + txtSCCompany.Text + seperator);
                            break;
                        }
                        else
                        {
                            paramsText.Append(BaseCls.GlbUserID + seperator + txtJCCompany.Text + seperator);
                            break;
                        }
                    }
                case CommonUIDefiniton.SearchUserControlType.HpAccount:
                    {
                        if (tabControl1.SelectedIndex == 0)
                        {
                            paramsText.Append(txtADCompany.Text + seperator + txtADProfitCenter.Text + seperator + "A" + seperator);
                            break;
                        }
                        else if (tabControl1.SelectedIndex == 1)
                        {
                            paramsText.Append(txtSRCompany.Text + seperator + txtSRProfitCenter.Text + seperator + "A" + seperator);
                            break;
                        }
                        else if (tabControl1.SelectedIndex == 2)
                        {
                            paramsText.Append(txtARCompany.Text + seperator + txtARProfitCenter.Text + seperator + "A" + seperator);
                            break;
                        }
                        else if (tabControl1.SelectedIndex == 3)
                        {
                            paramsText.Append(txtSCCompany.Text + seperator + txtSCProfitCenter.Text + seperator + "A" + seperator);
                            break;
                        }
                        else
                        {
                            paramsText.Append(txtJCCompany.Text + seperator + txtJCProfitCenter.Text + seperator + "A" + seperator);
                            break;
                        }
                    }
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        if (tabControl1.SelectedIndex == 0)
                        {
                            paramsText.Append(txtADCompany.Text + seperator);
                            break;
                        }
                        else if (tabControl1.SelectedIndex == 1)
                        {
                            paramsText.Append(txtSRCompany.Text + seperator);
                            break;
                        }
                        else if (tabControl1.SelectedIndex == 2)
                        {
                            paramsText.Append(txtARCompany.Text + seperator);
                            break;

                        }
                        else if (tabControl1.SelectedIndex == 3)
                        {
                            paramsText.Append(txtSCCompany.Text + seperator);
                            break;
                        }
                        else
                        {
                            paramsText.Append(txtJCCompany.Text + seperator);
                            break;
                        }
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(txtADCompany.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.VehicleInsuranceRef:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.VehRegTxn:
                    {
                        paramsText.Append(seperator);
                        break;
                    }


            }
            return paramsText.ToString();
        }

        #endregion

        #region search grid cell content click

        private void dataGridViewADSearchResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    RecieptNo = dataGridViewADSearchResult.Rows[e.RowIndex].Cells[1].Value.ToString();
                    LoadApplicationDetails();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void dataGridViewSRSearchResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                RecieptNo = dataGridViewSRSearchResult.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtSRLoc.Text = dataGridViewSRSearchResult.Rows[e.RowIndex].Cells[7].Value.ToString();
            }
        }

        private void dataGridViewARSearchResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                RecieptNo = dataGridViewARSearchResult.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtARLoc.Text = dataGridViewARSearchResult.Rows[e.RowIndex].Cells[7].Value.ToString();
                string _regiNo = dataGridViewARSearchResult.Rows[e.RowIndex].Cells[2].Value.ToString();
                if (!string.IsNullOrEmpty(_regiNo))      //kapila 1/7/2016
                {
                    txtARRegNo3.Text = "";
                    txtARRegNo1.Text = _regiNo.Substring(0, 1);
                    txtARRegNo2.Text = _regiNo.Substring(1, 1);
                    if (_regiNo.Length == 9)
                    {
                        txtARRegNo4.Text = _regiNo.Substring(2, 1);
                        txtARRegNo5.Text = _regiNo.Substring(3, 1);
                        txtARRegNo6.Text = _regiNo.Substring(5, 1);
                        txtARRegNo7.Text = _regiNo.Substring(6, 1);
                        txtARRegNo8.Text = _regiNo.Substring(7, 1);
                        txtARRegNo9.Text = _regiNo.Substring(8, 1);
                    }
                    else
                    {
                        txtARRegNo3.Text = _regiNo.Substring(2, 1);
                        txtARRegNo4.Text = _regiNo.Substring(3, 1);
                        txtARRegNo5.Text = _regiNo.Substring(4, 1);
                        txtARRegNo6.Text = _regiNo.Substring(6, 1);
                        txtARRegNo7.Text = _regiNo.Substring(7, 1);
                        txtARRegNo8.Text = _regiNo.Substring(8, 1);
                        txtARRegNo9.Text = _regiNo.Substring(9, 1);
                    }
                }

            }
        }

        private void dataGridViewSCSearchResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                RecieptNo = dataGridViewSCSearchResult.Rows[e.RowIndex].Cells[1].Value.ToString();
                List<FF.BusinessObjects.VehicalRegistration> list = CHNLSVC.General.GetVehicalRegistrations(RecieptNo);
                if (!string.IsNullOrEmpty(list[0].Srvt_cr_pod_ref))
                {
                    txtCRNumber.Text = list[0].Srvt_cr_pod_ref;
                    dateTimePickerCRCourriedDate.Value = Convert.ToDateTime(list[0].SRVT_CR_COUR_DT);
                }
                if (!string.IsNullOrEmpty(list[0].Srvt_no_plt_pod_ref))
                {
                    dateTimePickerNumberCouriedDate.Value = Convert.ToDateTime(list[0].Srvt_no_plt_dt);
                    txtNumberPodNo.Text = list[0].Srvt_no_plt_pod_ref;
                }
            }
        }

        private void dataGridViewJCSearchResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                RecieptNo = dataGridViewJCSearchResult.Rows[e.RowIndex].Cells[1].Value.ToString();
                _engNo = dataGridViewJCSearchResult.Rows[e.RowIndex].Cells[5].Value.ToString();
                _chasisNo = dataGridViewJCSearchResult.Rows[e.RowIndex].Cells[4].Value.ToString();
                _invNo = dataGridViewJCSearchResult.Rows[e.RowIndex].Cells[3].Value.ToString();
                _cust = dataGridViewJCSearchResult.Rows[e.RowIndex].Cells[6].Value.ToString();
                _locCode = dataGridViewJCSearchResult.Rows[e.RowIndex].Cells[7].Value.ToString();
                txtFinComp.Text = dataGridViewJCSearchResult.Rows[e.RowIndex].Cells[9].Value.ToString();
                DataTable _dtRegCom = CHNLSVC.Financial.getVehRegComDetails(BaseCls.GlbUserComCode, RecieptNo);
                if (_dtRegCom.Rows.Count > 0)
                {
                    txtFinRem.Text = _dtRegCom.Rows[0]["Svfc_rem"].ToString();
                    txtFinPOD.Text = _dtRegCom.Rows[0]["Svfc_pod"].ToString();
                    dateTimePickerJobCloseDate.Value = Convert.ToDateTime(_dtRegCom.Rows[0]["Svfc_dt"]);
                }
                txtFinComp_Leave(null, null);

            }
        }

        #endregion

        #region data load methods

        private void LoadApplicationDetails()
        {
            List<FF.BusinessObjects.VehicalRegistration> list = CHNLSVC.General.GetVehicalRegistrations(RecieptNo);
            if (list != null)
            {
                //reciept details
                cmbIDType.SelectedItem = list[0].P_svrt_id_tp;
                txtADRegAmt.Text = list[0].P_svrt_reg_val.ToString();
                txtADClaimAmt.Text = list[0].P_svrt_claim_val.ToString();
                List<InvoiceItem> Inolist = CHNLSVC.Sales.GetInvoiceDetailByInvoice(list[0].P_svrt_inv_no);
                if (Inolist != null)
                    txtADSaleAmt.Text = Inolist[0].Sad_unit_amt.ToString();
                txtADInvNo.Text = list[0].P_svrt_inv_no;
                txtADSaleAmt.Text = list[0].P_svrt_sales_tp;
                dateTimePickerRecieptDate.Value = list[0].P_svrt_dt;

                //cus details
                cmbCustomerTitle.SelectedItem = list[0].P_svrt_cust_title;
                txtADSalesType.Text = list[0].P_svrt_sales_tp;
                txtADID.Text = list[0].P_svrt_id_ref;
                txtLastName.Text = list[0].P_svrt_last_name;
                txtFullAnme.Text = list[0].P_svrt_full_name;
                txtInitials.Text = list[0].P_svrt_initial;
                txtCustomerAddressLine1.Text = list[0].P_svrt_add01;
                txtCustomerAddressLine2.Text = list[0].P_svrt_add02;
                txtCity.Text = list[0].P_svrt_city;
                cmbDistrict.SelectedValue = list[0].P_svrt_district;
                txtProvince.Text = list[0].P_svrt_province;
                txtContactNo.Text = list[0].P_svrt_contact;

                //vehical details
                txtADCusCode.Text = list[0].P_svrt_cust_cd;
                txtModel.Text = list[0].P_svrt_model;
                txtBrand.Text = list[0].P_svrt_brd;
                txtChassie.Text = list[0].P_svrt_chassis;
                txtEngine.Text = list[0].P_svrt_engine;
                txtColour.Text = list[0].P_svrt_color;
                txtFuel.Text = list[0].P_svrt_fuel;
                txtCapacity.Text = list[0].P_svrt_capacity.ToString();
                txtUnit.Text = list[0].P_svrt_unit;
                txtHorsePower.Text = list[0].P_svrt_horse_power.ToString();
                txtWheelBase.Text = list[0].P_svrt_wheel_base.ToString();
                txtFrontTire.Text = list[0].P_svrt_tire_front;
                txtRearTire.Text = list[0].P_svrt_tire_rear;
                txtWeight.Text = list[0].P_svrt_weight.ToString();
                txtManfYear.Text = list[0].P_svrt_man_year.ToString();
                txtImporter.Text = list[0].P_svrt_importer;
                txtImportLicense.Text = list[0].P_svrt_import;
                txtAuthority.Text = list[0].P_svrt_authority;
                txtCountry.Text = list[0].P_svrt_country;
                if (list[0].P_srvt_cust_dt > dateTimePickerCustomDate.MaxDate || list[0].P_srvt_cust_dt < dateTimePickerCustomDate.MinDate)
                    dateTimePickerCustomDate.Value = DateTime.Now;
                else
                    dateTimePickerCustomDate.Value = list[0].P_srvt_cust_dt;
                dateTimePickerClearenceDate.Value = list[0].P_svrt_clear_dt;
                dateTimePickerDeclearDate.Value = list[0].P_svrt_declear_dt;

                //importer details
                txtImporter.Text = list[0].P_svrt_importer;
                txtImportAddress1.Text = list[0].P_svrt_importer_add01;
                txtImportAddress2.Text = list[0].P_svrt_importer_add02;

                chkSendRMV.Checked = Convert.ToBoolean(list[0].P_srvt_rmv_stus);
                chkJobClose.Checked = Convert.ToBoolean(list[0].P_srvt_cls_stus);
                chkSendCustomer.Checked = Convert.ToBoolean(list[0].P_srvt_cust_stus);
                if (list[0].P_svrt_veh_reg_no != "" && list[0].P_svrt_veh_reg_no != null)
                    chkAssignRegNumber.Checked = true;
                else
                    chkAssignRegNumber.Checked = false;

                if (list[0].P_srvt_rmv_stus == 1)
                {
                    btnSearchCustomer.Enabled = false;
                    //cus details
                    cmbIDType.Enabled = false;
                    cmbCustomerTitle.Enabled = false;
                    txtADID.ReadOnly = true;
                    txtLastName.ReadOnly = true;
                    txtFullAnme.ReadOnly = true;
                    txtInitials.ReadOnly = true;
                    txtCustomerAddressLine1.ReadOnly = true;
                    txtCustomerAddressLine2.ReadOnly = true;
                    txtCity.ReadOnly = true;
                    cmbDistrict.Enabled = false;
                    txtProvince.ReadOnly = true;
                    txtContactNo.ReadOnly = true;

                    //vehical details
                    txtADCusCode.ReadOnly = true;
                    txtModel.ReadOnly = true;
                    txtBrand.ReadOnly = true;
                    txtColour.ReadOnly = true;
                    txtFuel.ReadOnly = true;
                    txtCapacity.ReadOnly = true;
                    txtUnit.ReadOnly = true;
                    txtHorsePower.ReadOnly = true;
                    txtWheelBase.ReadOnly = true;
                    txtFrontTire.ReadOnly = true;
                    txtRearTire.ReadOnly = true;
                    txtWeight.ReadOnly = true;
                    txtManfYear.ReadOnly = true;
                    txtImporter.ReadOnly = true;
                    txtImportLicense.ReadOnly = true;
                    txtAuthority.ReadOnly = true;
                    txtCountry.Enabled = true;
                    dateTimePickerCustomDate.Enabled = false;
                    dateTimePickerClearenceDate.Enabled = false;
                    dateTimePickerDeclearDate.Enabled = false;

                    //importer details
                    txtImporter.ReadOnly = true;
                    txtImportAddress1.ReadOnly = true;
                    txtImportAddress2.ReadOnly = true;
                }
                else
                {
                    btnSearchCustomer.Enabled = true;
                    //cus details
                    cmbIDType.Enabled = true;
                    cmbCustomerTitle.Enabled = true;
                    txtADID.ReadOnly = false;
                    txtLastName.ReadOnly = false;
                    txtFullAnme.ReadOnly = false;
                    txtInitials.ReadOnly = false;
                    txtCustomerAddressLine1.ReadOnly = false;
                    txtCustomerAddressLine2.ReadOnly = false;
                    txtCity.ReadOnly = false;
                    cmbDistrict.Enabled = true;
                    txtProvince.ReadOnly = false;
                    txtContactNo.ReadOnly = false;

                    //vehical details
                    txtADCusCode.ReadOnly = false;
                    txtModel.ReadOnly = false;
                    txtBrand.ReadOnly = false;
                    txtColour.ReadOnly = false;
                    txtFuel.ReadOnly = false;
                    txtCapacity.ReadOnly = false;
                    txtUnit.ReadOnly = false;
                    txtHorsePower.ReadOnly = false;
                    txtWheelBase.ReadOnly = false;
                    txtFrontTire.ReadOnly = false;
                    txtRearTire.ReadOnly = false;
                    txtWeight.ReadOnly = false;
                    txtManfYear.ReadOnly = false;
                    txtImporter.ReadOnly = false;
                    txtImportLicense.ReadOnly = false;
                    txtAuthority.ReadOnly = false;
                    txtCountry.ReadOnly = false;
                    dateTimePickerCustomDate.Enabled = true;
                    dateTimePickerClearenceDate.Enabled = true;
                    dateTimePickerDeclearDate.Enabled = true;

                    //importer details
                    txtImporter.ReadOnly = false;
                    txtImportAddress1.ReadOnly = false;
                    txtImportAddress2.ReadOnly = false;
                }
            }
        }

        private void BindDistrict(ComboBox cmbDistrict)
        {
            List<DistrictProvince> _district = CHNLSVC.Sales.GetDistrict("");
            var source = new BindingSource();
            source.DataSource = _district.OrderBy(items => items.Mds_district);
            cmbDistrict.DataSource = source;
            cmbDistrict.DisplayMember = "Mds_district";
            cmbDistrict.ValueMember = "Mds_district";
        }

        #endregion

        #region registration no creation

        private void txtARRegNo1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtARRegNo2.Focus();
                }
                else
                {
                    CreateRegistrationNumber();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtARRegNo2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtARRegNo3.Focus();
                }
                else
                {
                    CreateRegistrationNumber();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtARRegNo3_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtARRegNo4.Focus();
                }
                else
                {
                    CreateRegistrationNumber();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtARRegNo4_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtARRegNo5.Focus();
                }
                else
                {
                    CreateRegistrationNumber();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtARRegNo5_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtARRegNo6.Focus();
                }
                else
                {
                    CreateRegistrationNumber();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtARRegNo6_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtARRegNo7.Focus();
                }
                else
                {
                    CreateRegistrationNumber();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtARRegNo7_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtARRegNo8.Focus();
                }
                else
                {
                    CreateRegistrationNumber();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtARRegNo8_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtARRegNo9.Focus();
                }
                else
                {
                    CreateRegistrationNumber();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtARRegNo9_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dateTimePickerARRegDate.Focus();
                }
                else
                {
                    CreateRegistrationNumber();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtARRegNo9_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtARRegNo9.Text != "")
                {
                    CreateRegistrationNumber();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtARRegNo8_Leave(object sender, EventArgs e)
        {
            if (txtARRegNo8.Text != "")
            {
                CreateRegistrationNumber();
            }
        }

        private void txtARRegNo7_Leave(object sender, EventArgs e)
        {
            if (txtARRegNo7.Text != "")
            {
                CreateRegistrationNumber();
            }
        }

        private void txtARRegNo6_Leave(object sender, EventArgs e)
        {
            if (txtARRegNo6.Text != "")
            {
                CreateRegistrationNumber();
            }
        }

        private void txtARRegNo5_Leave(object sender, EventArgs e)
        {
            if (txtARRegNo5.Text != "")
            {
                CreateRegistrationNumber();
            }
        }

        private void txtARRegNo4_Leave(object sender, EventArgs e)
        {
            if (txtARRegNo4.Text != "")
            {
                CreateRegistrationNumber();
            }
        }

        private void txtARRegNo3_Leave(object sender, EventArgs e)
        {
            if (txtARRegNo3.Text != "")
            {
                CreateRegistrationNumber();
            }
        }

        private void txtARRegNo2_Leave(object sender, EventArgs e)
        {
            if (txtARRegNo2.Text != "")
            {
                CreateRegistrationNumber();
            }

        }

        private void txtARRegNo1_Leave(object sender, EventArgs e)
        {
            if (txtARRegNo1.Text != "")
            {
                CreateRegistrationNumber();
            }
        }

        private void CreateRegistrationNumber()
        {
            lblRegistrationNo.Text = txtARRegNo1.Text + txtARRegNo2.Text + txtARRegNo3.Text + txtARRegNo4.Text + txtARRegNo5.Text + "-" + txtARRegNo6.Text + txtARRegNo7.Text + txtARRegNo8.Text + txtARRegNo9.Text;
        }

        #endregion

        #region application detail search textbox enter

        private void txtADCompany_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtADProfitCenter.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnADCompany_Click(null, null);
            }
        }

        private void txtADProfitCenter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtADAccount.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnADProfitCenter_Click(null, null);
            }
        }

        private void txtADAccount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtADInvoice.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnADAccount_Click(null, null);
            }
        }

        private void txtADInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtADReciept.Focus();
            }
        }

        private void txtADReciept_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtADVehNo.Focus();
            }
        }

        private void txtADVehNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtADEngine.Focus();
            }
        }

        private void txtADEngine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtADChassis.Focus();
            }
        }

        private void txtADChassis_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnsearch.Focus();
            }
        }

        #endregion

        #region application detail enter key press


        private void cmbIDType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtADID.Focus();
            }
        }

        private void txtADID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtADCusCode.Focus();
            }
        }

        private void txtADCusCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbCustomerTitle.Focus();
            }
        }

        private void cmbCustomerTitle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtLastName.Focus();
            }
        }

        private void txtLastName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtFullAnme.Focus();
            }
        }

        private void txtFullAnme_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtInitials.Focus();
            }
        }

        private void txtInitials_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCustomerAddressLine1.Focus();
            }
        }

        private void txtCustomerAddressLine1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCustomerAddressLine2.Focus();
            }
        }

        private void txtCustomerAddressLine2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCity.Focus();
            }
        }

        private void txtCity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbDistrict.Focus();
            }
        }

        private void cmbDistrict_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtProvince.Focus();
            }
        }

        private void txtProvince_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtContactNo.Focus();
            }
        }

        private void txtContactNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtModel.Focus();
            }
        }

        private void txtModel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBrand.Focus();
            }
        }

        private void txtBrand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtColour.Focus();
            }
        }

        private void txtColour_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtFuel.Focus();
            }
        }

        private void txtFuel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCapacity.Focus();
            }
        }

        private void txtCapacity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtUnit.Focus();
            }
        }

        private void txtUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtHorsePower.Focus();
            }
        }

        private void txtHorsePower_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtWheelBase.Focus();
            }
        }

        private void txtWheelBase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtFrontTire.Focus();
            }
        }

        private void txtFrontTire_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtRearTire.Focus();
            }
        }

        private void txtRearTire_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtWeight.Focus();
            }
        }

        private void txtWeight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtManfYear.Focus();
            }
        }

        private void txtManfYear_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtImportLicense.Focus();
            }
        }

        private void txtImportLicense_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAuthority.Focus();
            }
        }

        private void txtAuthority_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCountry.Focus();
            }
        }

        private void txtCountry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dateTimePickerCustomDate.Focus();
            }
        }

        private void dateTimePickerCustomDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dateTimePickerClearenceDate.Focus();
            }
        }

        private void dateTimePickerClearenceDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dateTimePickerDeclearDate.Focus();
            }
        }

        private void dateTimePickerDeclearDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtImporter.Focus();
            }
        }

        private void txtImporter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtImportAddress1.Focus();
            }
        }

        private void txtImportAddress1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtImportAddress2.Focus();
            }
        }

        private void txtImportAddress2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                toolStrip1.Focus();
                btnSave.Select();
            }
        }

        #endregion

        #region send rmv enter key press

        private void txtSRCompany_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSRProfitCenter.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnSRCompanySearch_Click(null, null);
            }
        }

        private void txtSRProfitCenter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSRAccNo.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnSRProfitCenter_Click(null, null);
            }
        }

        private void txtSRAccNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSRInvoiceNo.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnSRAccount_Click(null, null);
            }
        }

        private void txtSRInvoiceNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSRRecieptNo.Focus();
            }
        }

        private void txtSRRecieptNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSREngineNo.Focus();
            }
        }

        private void txtSREngineNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSRChassiseNo.Focus();
            }
        }

        private void txtSRChassiseNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSRSearch.Focus();
            }
        }

        private void dateTimePickerSendDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                toolStrip1.Focus();
                btnSave.Select();
            }
        }

        #endregion

        #region assign reg number enter key press

        private void txtARCompany_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtARProfitCenter.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnARCompany_Click(null, null);
            }
        }

        private void txtARProfitCenter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtARAccountNo.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnARProfitCenter_Click(null, null);
            }
        }

        private void txtARAccountNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtARInvoiceNo.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnARAccount_Click(null, null);
            }
        }

        private void txtARInvoiceNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtARRecieptNo.Focus();
            }
        }

        private void txtARRecieptNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAREngineNo.Focus();
            }
        }

        private void txtAREngineNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtARChassieNo.Focus();
            }
        }

        private void txtARChassieNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnARSearch.Focus();
            }

        }

        private void dateTimePickerARRegDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                toolStrip1.Focus();
                btnSave.Select();
            }
        }

        #endregion

        #region send customer enter key press

        private void txtSCCompany_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSCProfitCenter.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnSCCompany_Click(null, null);
            }
        }

        private void txtSCProfitCenter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSCAccountNo.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnSCProfitCenter_Click(null, null);
            }
        }

        private void txtSCAccountNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSCInvoiceNo.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnSCAccount_Click(null, null);
            }
        }

        private void txtSCInvoiceNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSCRecieptNo.Focus();
            }
        }

        private void txtSCRecieptNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSCVehicalNo.Focus();
            }
        }

        private void txtSCVehicalNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSCEngineNo.Focus();
            }
        }

        private void txtSCEngineNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSCChassieNo.Focus();
            }
        }

        private void txtSCChassieNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSCSearch.Focus();
            }
        }

        private void dateTimePickerCRCourriedDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCRNumber.Focus();
            }
        }

        private void txtCRNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dateTimePickerNumberCouriedDate.Focus();
            }
        }

        private void dateTimePickerNumberCouriedDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNumberPodNo.Focus();
            }
        }

        private void txtNumberPodNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                toolStrip1.Focus();
                btnSave.Select();
            }
        }

        #endregion

        #region job close enter key press

        private void txtJCCompany_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtJCProfitCenter.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnJCCompany_Click(null, null);
            }
        }

        private void txtJCProfitCenter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtJCAccount.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnJCProfitCenter_Click(null, null);
            }
        }

        private void txtJCAccount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtJCInvoice.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnJCAccount_Click(null, null);
            }
        }

        private void txtJCInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtJCReciept.Focus();
            }
        }

        private void txtJCReciept_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtJCVehicalNo.Focus();
            }
        }

        private void txtJCVehicalNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtJCEngineNo.Focus();
            }
        }

        private void txtJCEngineNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtJCChassieNo.Focus();
            }

        }

        private void txtJCChassieNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnJCSesarch.Focus();
            }

        }

        private void dateTimePickerJobCloseDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                toolStrip1.Focus();
                btnSave.Select();
            }
        }

        #endregion

        private void btnFileUplaod_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                txtFileName.Text = openFileDialog1.FileName;
            }
            else
                txtFileName.Text = "";
        }

        private void btnReciept_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.VehRegTxn);
                DataTable _result = CHNLSVC.CommonSearch.GetVehical_regTxn(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtADReciept;
                _CommonSearch.txtSearchbyword.Text = txtADReciept.Text;
                _CommonSearch.ShowDialog();
                txtADReciept.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSRReciept_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.VehRegTxn);
                DataTable _result = CHNLSVC.CommonSearch.GetVehical_regTxn(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSRRecieptNo;
                _CommonSearch.txtSearchbyword.Text = txtSRRecieptNo.Text;
                _CommonSearch.ShowDialog();
                txtSRRecieptNo.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnARReciept_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.VehRegTxn);
                DataTable _result = CHNLSVC.CommonSearch.GetVehical_regTxn(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtARRecieptNo;
                _CommonSearch.txtSearchbyword.Text = txtARRecieptNo.Text;
                _CommonSearch.ShowDialog();
                txtARRecieptNo.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnJCReciept_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.VehRegTxn);
                DataTable _result = CHNLSVC.CommonSearch.GetVehical_regTxn(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtJCReciept;
                _CommonSearch.txtSearchbyword.Text = txtJCReciept.Text;
                _CommonSearch.ShowDialog();
                txtJCReciept.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSCReciept_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.VehRegTxn);
                DataTable _result = CHNLSVC.CommonSearch.GetVehical_regTxn(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSCRecieptNo;
                _CommonSearch.txtSearchbyword.Text = txtSCRecieptNo.Text;
                _CommonSearch.ShowDialog();
                txtSCRecieptNo.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void grvRecDoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0 && e.RowIndex != -1)
                {
                    //  MessageBox.Show("HI");
                    //if (dr["gdh_rec_doc"].ToString() == "1")
                    //{
                    //    dr["ho_allRec"] = "H/O Received";
                    //}
                    //finalRemark
                    string HO_RECEIVED = grvRecDoc.Rows[e.RowIndex].Cells["gdh_rec_doc"].Value.ToString();
                    string REGISTERED = grvRecDoc.Rows[e.RowIndex].Cells["gdh_cr_rec"].Value.ToString();
                    string SENT_PAY = grvRecDoc.Rows[e.RowIndex].Cells["gdh_send_pay"].Value.ToString();

                    string receiptNo = grvRecDoc.Rows[e.RowIndex].Cells["gdh_rec"].Value.ToString();
                    string chassis = grvRecDoc.Rows[e.RowIndex].Cells["gdh_chassis"].Value.ToString();
                    string engine = grvRecDoc.Rows[e.RowIndex].Cells["gdh_engine"].Value.ToString();
                    Int32 Seq = Convert.ToInt32(grvRecDoc.Rows[e.RowIndex].Cells["gdh_seq"].Value.ToString());
                    string vehNo = grvRecDoc.Rows[e.RowIndex].Cells["gdh_cr_no"].Value.ToString();
                    string finalRmk = grvRecDoc.Rows[e.RowIndex].Cells["finalRemark"].Value.ToString();

                    txtSeq.Text = Seq.ToString();
                    txtIsCr.Text = grvRecDoc.Rows[e.RowIndex].Cells["gdh_cr_rec"].Value.ToString();
                    txtIsPay.Text = grvRecDoc.Rows[e.RowIndex].Cells["gdh_send_pay"].Value.ToString();

                    DataTable dt = CHNLSVC.Sales.Get_DocDet_For_VehReg(receiptNo);
                    if (dt != null)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr["gdd_rec_dt"].ToString() == "")
                            {
                                dr["gdd_rec_dt"] = CHNLSVC.Security.GetServerDateTime();
                            }
                        }

                    }
                    grvDocDetailS.DataSource = null;
                    grvDocDetailS.AutoGenerateColumns = false;
                    grvDocDetailS.DataSource = dt;

                    // if (grvRecDoc.Rows[e.RowIndex].Cells["gdh_cr_rec"].Value.ToString() == "1")
                    if (true)
                    {
                        txtPayEngine.Text = chassis;
                        txtPayChassis.Text = engine;
                        txtPayRemk.Text = "";
                        txtVehNo.Text = vehNo;
                    }
                    //if (grvRecDoc.Rows[e.RowIndex].Cells["gdh_send_pay"].Value.ToString() == "1")
                    if (true)
                    {
                        txtChqEngine.Text = engine;
                        txtChqChassis.Text = chassis;
                        txtChqRemk.Text = finalRmk;
                        txtVehNoChq.Text = vehNo;

                    }

                    if (HO_RECEIVED == "1")
                    {
                        btnSend.Enabled = false;
                    }
                    else
                    {
                        btnSend.Enabled = true;
                    }
                    //--------------
                    //if (REGISTERED == "1")
                    //{
                    //    btnPaySend.Enabled = tr;

                    //    btnSend.Enabled = false;
                    //}
                    //else
                    //{
                    //    btnPaySend.Enabled = true;
                    //}
                    //--------------
                    if (SENT_PAY == "1")
                    {
                        btnPaySend.Enabled = false;
                    }
                    else
                    {
                        btnPaySend.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void grvRecVehDet_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                //  MessageBox.Show("HI");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void grvRecVehDet_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "VEHDOC") == false)
                {
                    //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Permission Denied!");
                    MessageBox.Show("Permission Denied!\n( Advice: Reqired permission code :VEHDOC)", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // MessageBox.Show("Permission Denied!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                grvDocDetailS.EndEdit();
                Dictionary<int, string> updateLines = new Dictionary<int, string>();

                Int32 SEQ = -1;

                foreach (DataGridViewRow row in grvDocDetailS.Rows)
                {
                    string engineNo = row.Cells["DOC_gdh_engine"].Value.ToString();
                    string chasseNo = row.Cells["DOC_gdh_chassis"].Value.ToString();
                    string docNo = row.Cells["DOC_gdd_doc"].Value.ToString();

                    SEQ = Convert.ToInt32(row.Cells["DOC_gdd_seq"].Value.ToString());
                    Int32 line = Convert.ToInt32(row.Cells["DOC_gdd_line"].Value.ToString());
                    string line_remark = row.Cells["DOC_gdd_rec_rmks"].Value.ToString();

                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    bool isChecked = Convert.ToBoolean(chk.Value == DBNull.Value ? 0 : chk.Value);


                    List<int> lines = new List<int>();
                    if (isChecked == true)
                    {
                        updateLines.Add(line, line_remark);
                        //ApproveList.RemoveAll(x => x.Tus_ser_1 == engineNo && x.Tus_ser_2 == chasseNo);                  
                    }
                }

                if (updateLines.Count < 1)
                {
                    MessageBox.Show("Select documents first!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (MessageBox.Show("Do you want to save?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                try
                {
                    //comented by kapila 21/3/2016
                    //  CHNLSVC.Sales.Update_Veh_Doc_receive(SEQ, updateLines, CHNLSVC.Security.GetServerDateTime(), "Received", BaseCls.GlbUserID);
                    MessageBox.Show("Updated Successfully!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.btnClear_Click(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occured!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        private void grvDocDetailS_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 5 && e.RowIndex != -1)
            {
            }
            else
            {
                // this.Cursor = Cursors.Default;
            }
        }

        private void btnPaySend_Click(object sender, EventArgs e)
        {
            try
            {
                if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "VEHDOC") == false)
                {
                    //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Permission Denied!");
                    MessageBox.Show("Permission Denied!\n( Advice: Reqired permission code :VEHDOC)", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // MessageBox.Show("Permission Denied!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtPayEngine.Text == "")
                {
                    // MessageBox.Show("Not registered yet!", "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtIsCr.Text != "1")
                {
                    MessageBox.Show("Not registered yet!", "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnClear_Click(null, null);
                    return;
                }


                if (MessageBox.Show("Do you want to update?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                Int32 eff = CHNLSVC.Sales.Update_veh_DocPay(Convert.ToInt32(txtSeq.Text.Trim()), CHNLSVC.Security.GetServerDateTime(), txtPayRemk.Text, BaseCls.GlbUserID);
                if (eff > 0)
                {
                    MessageBox.Show("Successfully updated!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnClear_Click(null, null);

                }
                else
                {
                    MessageBox.Show("No records updated!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //sp_update_vehSendPay
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnChqColl_Click(object sender, EventArgs e)
        {
            try
            {
                if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "VEHDOC") == false)
                {
                    //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Permission Denied!");
                    MessageBox.Show("Permission Denied!\n( Advice: Reqired permission code :VEHDOC)", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // MessageBox.Show("Permission Denied!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtChqEngine.Text == "")
                {
                    return;
                }


                if (txtIsPay.Text != "1")
                {
                    MessageBox.Show("Not sent for payments yet!", "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnClear_Click(null, null);
                    return;
                }

                if (txtChqNo.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter cheque number!", "Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Do you want to update?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                Int32 eff = CHNLSVC.Sales.Update_veh_Collect_Cheque(Convert.ToInt32(txtSeq.Text.Trim()), CHNLSVC.Security.GetServerDateTime(), txtChqRemk.Text, BaseCls.GlbUserID, txtChqNo.Text.Trim(), 1);
                if (eff > 0)
                {
                    MessageBox.Show("Successfully updated!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnClear_Click(null, null);
                }
                else
                {
                    MessageBox.Show("No records updated!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //sp_update_vehCollectChq
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSearchRecDoc_Click(object sender, EventArgs e)
        {
            //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            //_CommonSearch.ReturnIndex = 0;
            //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.VehicleInsuranceRef);
            //DataTable _result = CHNLSVC.CommonSearch.GetVehicalRegistrationRef(_CommonSearch.SearchParams, null, null);
            //_CommonSearch.dvResult.DataSource = _result;
            //_CommonSearch.BindUCtrlDDLData(_result);
            //_CommonSearch.obj_TragetTextBox = txtRecNoDocReceive;
            //_CommonSearch.txtSearchbyword.Text = txtRecNoDocReceive.Text;
            //_CommonSearch.ShowDialog();
            //txtRecNoDocReceive.Focus();
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.VehRegTxn);
                DataTable _result = CHNLSVC.CommonSearch.GetVehical_regTxn(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtRecNoDocReceive;
                _CommonSearch.txtSearchbyword.Text = txtRecNoDocReceive.Text;
                _CommonSearch.ShowDialog();
                txtRecNoDocReceive.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtRecNoDocReceive_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.btnSearchAllIssue_Click(null, null);

                //DataTable dt_recDoc = CHNLSVC.Sales.Get_IssuedDocFor_VehReg(BaseCls.GlbUserID, txtRecNoDocReceive.Text.Trim(), txtInvIss.Text.Trim());

                //dt_recDoc.Columns.Add("ho_rec", typeof(string));
                //dt_recDoc.Columns.Add("ho_allRec", typeof(string));
                //dt_recDoc.Columns.Add("send_RMV", typeof(string));
                //dt_recDoc.Columns.Add("reg_Status", typeof(string));
                //dt_recDoc.Columns.Add("send_Pay", typeof(string));


                //if (dt_recDoc.Rows.Count > 0)
                //{
                //    foreach (DataRow dr in dt_recDoc.Rows)
                //    {
                //        //-------------------------------------------------------1
                //        //if (dr["gdh_isse_doc"].ToString() == "1" && dr["gdh_rec_doc"].ToString() == "0")
                //        if (dr["gdh_rec_doc"].ToString() == "0")
                //        {
                //            dr["ho_rec"] = "Pending";
                //        }
                //        else
                //        {
                //            dr["ho_rec"] = "All Sent";
                //        }
                //        //---------------------------------------------------------2
                //        //if (dr["gdh_rec_doc"].ToString() == "1" && dr["gdh_send_reg"].ToString() == "0")
                //        if (dr["gdh_rec_doc"].ToString() == "1")
                //        {
                //            dr["ho_allRec"] = "H/O Received";
                //        }
                //        else
                //        {
                //            dr["ho_allRec"] = "Pending";
                //        }
                //        //---------------------------------------------------------3
                //        //if (dr["gdh_send_reg"].ToString() == "1" && dr["gdh_cr_rec"].ToString() == "0")
                //        if (dr["gdh_send_reg"].ToString() == "1")
                //        {
                //            dr["send_RMV"] = "Sent to RMV";
                //        }
                //        else
                //        {
                //            dr["send_RMV"] = "Not Sent";
                //        }
                //        //---------------------------------------------------------4
                //        //if (dr["gdh_cr_rec"].ToString() == "1" && dr["gdh_send_pay"].ToString() == "0")
                //        if (dr["gdh_cr_rec"].ToString() == "1")
                //        {
                //            dr["reg_Status"] = "Registered";
                //        }
                //        else
                //        {
                //            dr["reg_Status"] = "Not Registered";
                //        }
                //        //---------------------------------------------------------4
                //        //if (dr["gdh_send_pay"].ToString() == "1" && dr["gdh_coll_cheq"].ToString() == "0")
                //        if (dr["gdh_send_pay"].ToString() == "1")
                //        {
                //            dr["send_Pay"] = "Sent For Pay";
                //        }
                //        else
                //        {
                //            dr["send_Pay"] = "Not Sent";
                //        }

                //    }
                //}

                //grvRecDoc.DataSource = null;
                //grvRecDoc.AutoGenerateColumns = false;
                //grvRecDoc.DataSource = dt_recDoc;
            }
        }

        private void btnInvIssSearch_Click(object sender, EventArgs e)
        {
            try
            {
                // this.btnSearchAllIssue_Click(null, null);
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.VehRegTxn);
                DataTable _result = CHNLSVC.CommonSearch.GetVehical_regTxn(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtInvIss;
                _CommonSearch.txtSearchbyword.Text = txtInvIss.Text;
                _CommonSearch.ShowDialog();
                txtInvIss.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSearchAllIssue_Click(object sender, EventArgs e)
        {
            try
            {
                rdoAll.Checked = true;

                grvDocDetailS.DataSource = null;
                grvDocDetailS.AutoGenerateColumns = false;

                DataTable dt_recDoc = CHNLSVC.Sales.Get_IssuedDocFor_VehReg(BaseCls.GlbUserID, txtRecNoDocReceive.Text.Trim(), txtInvIss.Text.Trim(), txtEngineIssue.Text.Trim(), txtChassisIssue.Text.Trim());

                dt_recDoc.Columns.Add("ho_rec", typeof(string));
                dt_recDoc.Columns.Add("ho_allRec", typeof(string));
                dt_recDoc.Columns.Add("send_RMV", typeof(string));
                dt_recDoc.Columns.Add("reg_Status", typeof(string));
                dt_recDoc.Columns.Add("send_Pay", typeof(string));


                if (dt_recDoc.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt_recDoc.Rows)
                    {
                        //-------------------------------------------------------1
                        //if (dr["gdh_isse_doc"].ToString() == "1" && dr["gdh_rec_doc"].ToString() == "0")
                        if (dr["gdh_rec_doc"].ToString() == "0")
                        {
                            dr["ho_rec"] = "Pending";
                        }
                        else
                        {
                            dr["ho_rec"] = "All Sent";
                        }
                        //---------------------------------------------------------2
                        //if (dr["gdh_rec_doc"].ToString() == "1" && dr["gdh_send_reg"].ToString() == "0")
                        if (dr["gdh_rec_doc"].ToString() == "1")
                        {
                            dr["ho_allRec"] = "H/O Received";
                        }
                        else
                        {
                            dr["ho_allRec"] = "Pending";
                        }
                        //---------------------------------------------------------3
                        //if (dr["gdh_send_reg"].ToString() == "1" && dr["gdh_cr_rec"].ToString() == "0")
                        if (dr["gdh_send_reg"].ToString() == "1")
                        {
                            dr["send_RMV"] = "Sent to RMV";
                        }
                        else
                        {
                            dr["send_RMV"] = "Not Sent";
                        }
                        //---------------------------------------------------------4
                        //if (dr["gdh_cr_rec"].ToString() == "1" && dr["gdh_send_pay"].ToString() == "0")
                        if (dr["gdh_cr_rec"].ToString() == "1")
                        {
                            dr["reg_Status"] = "Registered";
                        }
                        else
                        {
                            dr["reg_Status"] = "Not Registered";
                        }
                        //---------------------------------------------------------4
                        //if (dr["gdh_send_pay"].ToString() == "1" && dr["gdh_coll_cheq"].ToString() == "0")
                        if (dr["gdh_send_pay"].ToString() == "1")
                        {
                            dr["send_Pay"] = "Sent For Pay";
                        }
                        else
                        {
                            dr["send_Pay"] = "Not Sent";
                        }

                    }
                }

                grvRecDoc.DataSource = null;
                grvRecDoc.AutoGenerateColumns = false;
                grvRecDoc.DataSource = dt_recDoc;

                if (dt_recDoc.Rows.Count < 1)
                {
                    DataTable dt = CHNLSVC.Security.Get_gen_doc_pro_hdr(BaseCls.GlbUserID, txtRecNoDocReceive.Text.Trim(), txtInvIss.Text.Trim(), txtEngineIssue.Text.Trim(), txtChassisIssue.Text.Trim());
                    if (dt == null)
                    {
                        MessageBox.Show("Not found!", "Documents", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Not found!", "Documents", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (dt.Rows[0]["gdh_coll_cheq"].ToString() == "1")
                    {
                        MessageBox.Show("Cheque collected already!", "Documents", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (dt.Rows[0]["gdh_isse_doc"].ToString() == "0")
                    {
                        MessageBox.Show("All documents are not received yet!", "Documents", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    collorRows();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void collorRows()
        {
            foreach (DataGridViewRow dr in grvRecDoc.Rows)
            {
                //dr.Cells["gdh_rec_doc"].Value.ToString()

                if (dr.Cells["gdh_rec_doc"].Value.ToString() == "0")
                {
                    dr.DefaultCellStyle.BackColor = Color.Beige;
                }
                //-----------------

                if (dr.Cells["gdh_rec_doc"].Value.ToString() == "1")
                {
                    dr.DefaultCellStyle.BackColor = Color.LightCoral;
                }
                //------------------------------
                if (dr.Cells["gdh_send_reg"].Value.ToString() == "1")
                {
                    dr.DefaultCellStyle.BackColor = Color.MistyRose;
                }
                //--------------------------------------
                if (dr.Cells["gdh_cr_rec"].Value.ToString() == "1")
                {
                    dr.DefaultCellStyle.BackColor = Color.MintCream;
                }
                //-------------------------
                if (dr.Cells["gdh_send_pay"].Value.ToString() == "1")
                {
                    dr.DefaultCellStyle.BackColor = Color.Olive;
                }


            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 3;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.VehRegTxn);
                DataTable _result = CHNLSVC.CommonSearch.GetVehical_regTxn(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtEngineIssue;
                _CommonSearch.txtSearchbyword.Text = txtEngineIssue.Text;
                _CommonSearch.ShowDialog();
                txtEngineIssue.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 4;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.VehRegTxn);
                DataTable _result = CHNLSVC.CommonSearch.GetVehical_regTxn(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtChassisIssue;
                _CommonSearch.txtSearchbyword.Text = txtChassisIssue.Text;
                _CommonSearch.ShowDialog();
                txtChassisIssue.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void txtRecNoDocReceive_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtInvIss.Focus();
            }
        }

        private void txtInvIss_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtEngineIssue.Focus();
            }
        }

        private void txtEngineIssue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //txtEngineIssue
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtChassisIssue.Focus();
            }
        }

        private void check_allDocs_CheckedChanged(object sender, EventArgs e)
        {
            if (check_allDocs.Checked == true)
            {
                try
                {
                    foreach (DataGridViewRow row in grvDocDetailS.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = true;
                    }
                    grvDocDetailS.EndEdit();
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                try
                {
                    foreach (DataGridViewRow row in grvDocDetailS.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = false;
                    }
                    grvDocDetailS.EndEdit();
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void btnUpdateChq_Click(object sender, EventArgs e)
        {
            try
            {
                if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "VEHDOC") == false)
                {
                    //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Permission Denied!");
                    MessageBox.Show("Permission Denied!\n( Advice: Reqired permission code :VEHDOC)", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // MessageBox.Show("Permission Denied!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtChqEngine.Text == "")
                {
                    return;
                }


                //if (txtIsPay.Text != "1")
                //{
                //    MessageBox.Show("Not sent for payments yet!", "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    btnClear_Click(null, null);
                //    return;
                //}

                if (MessageBox.Show("Do you want to update?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                Int32 eff = CHNLSVC.Sales.Update_veh_Collect_Cheque(Convert.ToInt32(txtSeq.Text.Trim()), CHNLSVC.Security.GetServerDateTime(), txtChqRemk.Text, BaseCls.GlbUserID, txtChqNo.Text.Trim(), 0);
                if (eff > 0)
                {
                    MessageBox.Show("Successfully updated!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnClear_Click(null, null);
                }
                else
                {
                    MessageBox.Show("No records updated!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void chkRec_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRec.Checked == true)
            {
                dtNumPltRec.Enabled = true;
                txtNumPltRecRem.Enabled = true;
            }
            else
            {
                dtNumPltRec.Enabled = false;
                txtNumPltRecRem.Enabled = false;
            }
        }

        private void txtARRegNo1_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtARRegNo2.Focus();
            CreateRegistrationNumber();

        }

        private void txtARRegNo2_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtARRegNo3.Focus();
            CreateRegistrationNumber();
        }

        private void txtARRegNo3_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtARRegNo4.Focus();
            CreateRegistrationNumber();
        }

        private void txtARRegNo4_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtARRegNo5.Focus();
            CreateRegistrationNumber();
        }

        private void txtARRegNo5_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtARRegNo6.Focus();
            CreateRegistrationNumber();
        }

        private void txtARRegNo6_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtARRegNo7.Focus();
            CreateRegistrationNumber();
        }

        private void txtARRegNo7_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtARRegNo8.Focus();
            CreateRegistrationNumber();
        }

        private void txtARRegNo8_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtARRegNo9.Focus();
            CreateRegistrationNumber();
        }

        private void btnSrh_Click(object sender, EventArgs e)
        {
            try
            {
                _submitDoc = new DataTable();
                dgvSubmit.AutoGenerateColumns = false;
                _submitDoc = CHNLSVC.General.GetVehicalSearch(txtSRCompany.Text, txtSRProfitCenter.Text, "DocSubmit", "", txtSRChassiseNo.Text, txtSREngineNo.Text, txtSRInvoiceNo.Text, txtSRRecieptNo.Text, txtSRAccNo.Text, dtpFrom.Value, dtpTo.Value, txtCustomer.Text);
                dgvSubmit.DataSource = _submitDoc;

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnCom_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanySearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtcom;
                _CommonSearch.txtSearchbyword.Text = txtcom.Text;
                _CommonSearch.ShowDialog();
                txtcom.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnAcc_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
                DataTable _result = CHNLSVC.CommonSearch.GetHpAccountSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAcc;
                _CommonSearch.txtSearchbyword.Text = txtAcc.Text;
                _CommonSearch.ShowDialog();
                txtAcc.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnPc_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPc;
                _CommonSearch.txtSearchbyword.Text = txtPc.Text;
                _CommonSearch.ShowDialog();
                txtPc.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnRecNo_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.VehRegTxn);
                DataTable _result = CHNLSVC.CommonSearch.GetVehical_regTxn(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtRec;
                _CommonSearch.txtSearchbyword.Text = txtRec.Text;
                _CommonSearch.ShowDialog();
                txtRec.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnCust_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_CommonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCustomer;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtCustomer.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnAppy_Click(object sender, EventArgs e)
        {
            foreach (DataRow dr in _submitDoc.Rows)
            {
                if (dr["SVRT_ENGINE"].ToString() == txtEngineNo.Text)
                {
                    dr["SRVT_SUBMIT_DATE"] = dtpApply.Value;
                }

            }


            dgvSubmit.DataSource = null;
            dgvSubmit.AutoGenerateColumns = false;
            dgvSubmit.DataSource = _submitDoc;
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            foreach (DataRow dr in _submitDoc.Rows)
            {

                dr["SRVT_SUBMIT_DATE"] = dtpApply.Value;

            }


            dgvSubmit.DataSource = null;
            dgvSubmit.AutoGenerateColumns = false;
            dgvSubmit.DataSource = _submitDoc;
        }

        private void dtpApply_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dgvSubmit_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {

                txtEngineNo.Text = dgvSubmit.Rows[e.RowIndex].Cells[5].Value.ToString();
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chkRetHO_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRetHO.Checked == true)
            {
                dtRetDt.Enabled = true;
                txtRetRem.Enabled = true;
            }
            else
            {
                dtRetDt.Enabled = false;
                txtRetRem.Enabled = false;
            }
        }

        private void chkResend_CheckedChanged(object sender, EventArgs e)
        {
            if (chkResend.Checked == true)
            {
                dtResend.Enabled = true;
                txtResendRem.Enabled = true;
            }
            else
            {
                dtResend.Enabled = false;
                txtResendRem.Enabled = false;
            }
        }

        private void chkRerec_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRerec.Checked == true)
            {
                dtRerec.Enabled = true;

            }
            else
            {
                dtRerec.Enabled = false;
            }
        }

        private void chkTax_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTax.Checked == true)
            {
                dtpTaxDt.Enabled = true;
                txtRemTax.Enabled = true;
            }
            else
            {
                dtpTaxDt.Enabled = false;
                txtRemTax.Enabled = false;
            }
        }

        private void chkRecRP_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRecRP.Checked == true)
            {
                dtpRP.Enabled = true;
                txtRemRP.Enabled = true;
            }
            else
            {
                dtpRP.Enabled = false;
                txtRemRP.Enabled = false;
            }
        }

        private void btnAddFinComp_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(RecieptNo))
            {
                MessageBox.Show("Please select the receipt number", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(lblFinComp.Text))
            {
                MessageBox.Show("Please select the finance company", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //if (string.IsNullOrEmpty(txtFinPOD.Text))
            //{
            //    MessageBox.Show("Please enter the POD Number", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            VehicleRegCompany obTemp = new VehicleRegCompany();
            obTemp = _listVehRegComp.Find(x => x.Svfc_rec_no == RecieptNo);
            if (obTemp != null)
                if (!string.IsNullOrEmpty(obTemp.Svfc_rec_no))
                {
                    MessageBox.Show("Already added this receipt #", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            VehicleRegCompany _objRegCOm = new VehicleRegCompany();
            _objRegCOm.Svfc_chasis = _chasisNo;
            _objRegCOm.Svfc_com = BaseCls.GlbUserComCode;
            _objRegCOm.Svfc_cre_by = BaseCls.GlbUserID;
            _objRegCOm.Svfc_cus_name = _cust;
            _objRegCOm.Svfc_dt = Convert.ToDateTime(dateTimePickerJobCloseDate.Value);
            _objRegCOm.Svfc_engine = _engNo;
            _objRegCOm.Svfc_fin_comp = txtFinComp.Text;
            _objRegCOm.Svfc_inv_no = _invNo;
            _objRegCOm.Svfc_pc = _locCode;
            _objRegCOm.Svfc_pod = txtFinPOD.Text;
            _objRegCOm.Svfc_rec_no = RecieptNo;
            _objRegCOm.Svfc_rem = txtFinRem.Text;
            _listVehRegComp.Add(_objRegCOm);

            grvFinComp.AutoGenerateColumns = false;
            grvFinComp.DataSource = new List<VehicleRegCompany>();
            grvFinComp.DataSource = _listVehRegComp;

        }

        private void btn_srch_fin_comp_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 2;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
            DataTable _result = CHNLSVC.CommonSearch.GetBusinessCompany(_CommonSearch.SearchParams, null, null);
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtFinComp;
            _CommonSearch.ShowDialog();
            txtFinComp.Select();

            txtFinComp_Leave(null, null);
        }



        private void txtFinComp_Leave(object sender, EventArgs e)
        {
            lblFinComp.Text = "";
            if (!string.IsNullOrEmpty(txtFinComp.Text))
            {
                MasterOutsideParty _OutPartyDetails = new MasterOutsideParty();
                _OutPartyDetails = CHNLSVC.Sales.GetOutSidePartyDetails(txtFinComp.Text.Trim(), "LEASE");

                if (_OutPartyDetails.Mbi_cd == null)
                {
                    MessageBox.Show("Invalid leasing company.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFinComp.Text = "";
                    txtFinComp.Focus();
                    return;
                }
                else
                    lblFinComp.Text = _OutPartyDetails.Mbi_desc;
            }
        }

        private void grvFinComp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _listVehRegComp.RemoveAt(e.RowIndex);
                    BindingSource _bnding = new BindingSource();
                    _bnding.DataSource = _listVehRegComp;
                    grvFinComp.DataSource = _bnding;
                }
            }
        }


        //private void dataGridViewADSearchResult_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        if (dataGridViewADSearchResult.Rows.Count > 0 && dataGridViewADSearchResult.SelectedRows.Count > 0)
        //        {
        //            if (dataGridViewADSearchResult.SelectedRows[0].Index != -1)
        //            {
        //                RecieptNo = dataGridViewADSearchResult.Rows[dataGridViewADSearchResult.SelectedRows[0].Index].Cells[1].Value.ToString();
        //                LoadApplicationDetails();
        //                cmbIDType.Focus();
        //            }
        //        }
        //    }
        //}



    }
}
