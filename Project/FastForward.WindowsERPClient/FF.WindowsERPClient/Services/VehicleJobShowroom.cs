using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using FF.BusinessObjects;

namespace FF.WindowsERPClient.Services
{
    public partial class VehicleJobShowroom : Base
    {
        #region global variables
        private Service_Chanal_parameter _scvParam = null;
         private List<Service_job_Det> _scvItemList = null;
         private InventorySerialMaster VehicleObect = null;
        List<Service_Job_Defects> Defect_List;
        private MasterItem _itemdetail = null;
        private MasterBusinessEntity _masterBusinessCompany = null;
        private Service_JOB_HDR _scvjobHdr = null;
 
        private List<Service_Job_Defects> _scvDefList = null;
        private List<Service_Tech_Aloc_Hdr> _scvEmpList = null;
        private List<Service_Job_Det_Sub> _scvItemSubList = null;
        private List<Service_Job_Det_Sub> _tempItemSubList = null;
        private List<Service_TempIssue> _scvStdbyList = null;
        private int _jobRecall = 0;
        int Defect_LineNo;
        int IsService;
        int IsUpdateShedule;
        int ServiceTerm;
        int IsFreeService;
        string _warrSearchtp = string.Empty;
        string _warrSearchorder = string.Empty;
        string _itemType = string.Empty;
        Int32 _warStus = 0;
        string _itemBrand  = string.Empty;
        string _warranty = string.Empty;
        Int32 _warrPrd = 0;
        string _itemStatus = string.Empty;
        DateTime _warrStdate;
        string _jobStage = "2";
        string _email = string.Empty;
        private Boolean _isGroup = false;
        Decimal _chkJobStage = 0;
        int _jobSeq = 0;
        #endregion

        public VehicleJobShowroom()
        {
            InitializeComponent();
          //  VehicleObect = new InventorySerialMaster();
            Defect_List = new List<Service_Job_Defects>();
            _scvItemList = new List<Service_job_Det>();
            Defect_LineNo = 0;
            IsService = 0;
            IsUpdateShedule = 0;
            ServiceTerm = 0;
            Clear(false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.Yes)
                this.Close();
        }

        #region main button click

       
       

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        private void Clear(bool isExternal)
        {
            //text box
            txtCustAddress2.Text = "";
            _jobRecall = 0;
            _chkJobStage = 0;
            txtChasseNo.Text = "";
            txtCustAddress.Text = "";
            txtCustCD.Text = "";
            txtCustMobile.Text = "";
            txtCustName.Text = "";
            txtCustTelephone.Text = "";
            txtCustTown.Text = "";
            txtDefectRmk.Text = "";
            txtDescription.Text = "";
            txtEnginNo.Text = "";
            txtJobRmk.Text = "";
            txtMilage.Text = "";
            txtModel.Text = "";
            txtPopUpType.Text = "";
            txtRegNo.Text = "";
            txtNIC.Text = "";
            //label
            lblCustDO_no.Text = "";
            lblCustInvoiceDate.Text = "";
            lblCustInvoiceNo.Text = "";
            txtItmCd.Text = "";
            lblLastServNoOfAttempt.Text = "";
            lblLastSevJobDt.Text = "";
            lblLastSevJobNo.Text = "";
            lblLastSevMilage.Text = "";
            lblLastSevPayOrFree.Text = "";
            lblServiceCount.Text = "";
            lblSlash.Text = "";
            lblWarrCoopenNo.Text = "";
            lblWarrEndDate.Text = "";
            lblWarrIsFree.Text = "";
            lblWarrNo.Text = "";
            lblWarrPeriod.Text = "";
            lblWarrRemainingDays.Text = "";
            lblWarrServiceDays.Text = "";
            lblWarrServiceMilage.Text = "";
            lblWarrStatus.Text = "";
            groupBox1.Enabled = true;
            txtContPersn.Text = "";
            txtContNo.Text = "";
            txtContLoc.Text = "";
            txtInfoPersn.Text = "";
            txtInfoNo.Text = "";
            txtInfoLoc.Text = "";
            btnSave.Enabled = true;
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            dateTimePickerDate.Value = _date;
            dtExpectOn.Value = _date;
            _jobSeq = 0;
            dataGridViewDefectList.DataSource = null;
            _email = string.Empty;
    
            txtDef.Text = "";
            lblDefDesc.Text = "";


            txtCustCD.Enabled = true;
            txtCustName.Enabled = true;
            txtCustAddress.Enabled = true;
            txtCustAddress2.Enabled = true;
            // txtAddress2.Enabled = true;
            txtCustMobile.Enabled = true;
            txtNIC.Enabled = true;

            btnSearch_NIC.Enabled = true;
            btnSearch_CustCode.Enabled = true;
            btnSearch_Mobile.Enabled = true;


            lblBuyerCustCode.Text = "";
            lblBuyerCustName.Text = "";
            lblBuyerCustAdd1.Text = "";
            //    lblBuyerCustAdd2.Text = _reqHdr.Srb_add2;
            lblBuyerCustMobi.Text = "";
            lblPhone.Text = "";
            lblNIC.Text = "";
            lblTown.Text = "";

         //   cmbServiceTypes.DataSource = null;

            //clear variables
            VehicleObect = new InventorySerialMaster();
            Defect_List = new List<Service_Job_Defects>();
            Defect_LineNo = 0;
            IsService = 0;
            IsUpdateShedule = 0;
            ServiceTerm = 0;
            btnSave.Enabled = true;
            _warranty = string.Empty;
              _warrPrd = 0;
              _itemStatus = string.Empty;
              LoadComboData();

              cmbServiceTypes.Text = "--Select--";

            if (!isExternal)
            {
                ChkExternal.Checked = false;
                btnOK.Enabled = true;
                btnVehicleSearch.Enabled = true;

                txtEnginNo.ReadOnly = true;
                txtChasseNo.ReadOnly = true;
                txtDescription.ReadOnly = true;
                txtModel.ReadOnly = true;
                //txtCustCD.ReadOnly = true;
                //txtCustName.ReadOnly = true;
                //txtCustAddress.ReadOnly = true;
               //  txtCustTown.ReadOnly = true;
               // txtCustTelephone.ReadOnly = true;
               // txtCustMobile.ReadOnly = true;
                txtItmCd.Text = "";
            }
        }

        private void ClearCustomer(bool _isCustomer)
        {
            if (_isCustomer) txtCustCD.Clear();
            txtCustName.Clear();
            txtCustAddress.Clear();
              txtCustAddress2.Clear();
         
            //txtAddress2.Clear();
            txtCustMobile.Clear();
            txtNIC.Clear();
            txtCustTown.Clear();
            txtCustName.Enabled = true;
            txtCustAddress.Enabled = true;
            txtCustAddress2.Enabled = true;
           // txtAddress2.Enabled = true;
            txtCustMobile.Enabled = true;
            txtNIC.Enabled = true;
            txtCustTown.Enabled = true;
            //chkTaxPayable.Checked = false;
            //txtLoyalty.Clear();
        }
        private void SetCustomerAndDeliveryDetails(bool _isRecall, InvoiceHeader _hdr)
        {
            txtCustCD.Text = _masterBusinessCompany.Mbe_cd;
            txtCustName.Text = _masterBusinessCompany.Mbe_name;
            txtCustAddress.Text = _masterBusinessCompany.Mbe_add1;
            txtCustAddress2.Text = _masterBusinessCompany.Mbe_add2;
            txtCustMobile.Text = _masterBusinessCompany.Mbe_mob;
            txtNIC.Text = _masterBusinessCompany.Mbe_nic;
            txtCustTelephone.Text = _masterBusinessCompany.Mbe_tel;
            txtCustTown.Text = _masterBusinessCompany.Mbe_town_cd;
           // cmbTitle.Text = _masterBusinessCompany.MBE_TIT;
            //ucPayModes1.Customer_Code = txtCustomer.Text.Trim();
            //ucPayModes1.Mobile = txtMobile.Text.Trim();
            _email = _masterBusinessCompany.Mbe_email;
            if (_isRecall == false)
            {
                //txtDelAddress1.Text = _masterBusinessCompany.Mbe_add1;
                //txtDelAddress2.Text = _masterBusinessCompany.Mbe_add2;
                //txtDelCustomer.Text = _masterBusinessCompany.Mbe_cd;
                //txtDelName.Text = _masterBusinessCompany.Mbe_name;
            }
            else
            {
                txtCustName.Text = _hdr.Sah_cus_name;
                txtCustAddress.Text = _hdr.Sah_cus_add1;
                txtCustAddress2.Text = _hdr.Sah_cus_add2;

                //txtDelAddress1.Text = _hdr.Sah_d_cust_add1;
                //txtDelAddress2.Text = _hdr.Sah_d_cust_add2;
                //txtDelCustomer.Text = _hdr.Sah_d_cust_cd;
                //txtDelName.Text = _hdr.Sah_d_cust_name;
            }



            //if (string.IsNullOrEmpty(txtNIC.Text)) { cmbTitle.SelectedIndex = 0; return; }
            //if (IsValidNIC(txtNIC.Text) == false) { cmbTitle.SelectedIndex = 0; return; }
            //GetNICGender();
            //if (string.IsNullOrEmpty(txtCusName.Text)) txtCusName.Text = cmbTitle.Text.Trim();
            //else
            //{
            //    string _title = ExtractTitleByCustomerName(txtCusName.Text.Trim());
            //    bool _exist = cmbTitle.Items.Contains(_title);
            //    if (_exist)
            //        cmbTitle.Text = _title;
            //}
        }
        protected void LoadCustomerDetailsByCustomer()
        {
            _masterBusinessCompany = new MasterBusinessEntity();
            _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(txtCustCD.Text, null, null, null, null, BaseCls.GlbUserComCode);
            if (_masterBusinessCompany.Mbe_cd != null)
            {
                if (_masterBusinessCompany.Mbe_act == false)
                {
                    this.Cursor = Cursors.Default;
                    { MessageBox.Show("This customer already inactive. Please contact accounts dept.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                     ClearCustomer(true);
                    txtCustCD.Focus();
                    return;
                }

                if (_masterBusinessCompany.Mbe_cd == "CASH")
                {
                    txtCustCD.Text = _masterBusinessCompany.Mbe_cd;
                     SetCustomerAndDeliveryDetails(false, null);
                     ClearCustomer(false);
                }
                else
                {
                    SetCustomerAndDeliveryDetails(false, null);
                }
            }
            else
            {
                this.Cursor = Cursors.Default;
                { MessageBox.Show("Please select the valid customer", "Customer Detail", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                //ClearCustomer(true);
                txtCustCD.Focus();
                return;

            }
        }
        private void ChkExternal_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ChkExternal.Checked)
                {
                    Clear(true);
                    btnOK.Enabled = false;
                    btnVehicleSearch.Enabled = false;
                   // DataTable dt = CHNLSVC.Sales.GetExternalVehicalJob(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                    btnItemSearch.Enabled = true;
                    txtItmCd.Enabled = true;
                    txtEnginNo.ReadOnly = false;
                    //if (dt.Rows.Count > 0)
                    //    txtItmCd.Text = dt.Rows[0]["MI_CD"].ToString();
                    txtEnginNo.ReadOnly = false;
                    txtChasseNo.ReadOnly = false;
                    txtDescription.ReadOnly = false;
                    txtModel.ReadOnly = false;
                    txtCustCD.ReadOnly = false;
                    txtCustName.ReadOnly = false;
                    txtCustAddress.ReadOnly = false;
                    txtCustAddress2.ReadOnly = false;
                    txtCustTown.ReadOnly = false;
                    txtCustTelephone.ReadOnly = false;
                    txtCustMobile.ReadOnly = false;
                    btnItemSearch.Enabled = true;
                }
                else
                {
                    Clear(true);
                    btnOK.Enabled = true;
                    btnVehicleSearch.Enabled = true;
                    txtItmCd.Enabled = false;
                    txtEnginNo.ReadOnly = true;
                    txtChasseNo.ReadOnly = true;
                    txtDescription.ReadOnly = true;
                    txtModel.ReadOnly = true;
                    txtCustCD.ReadOnly = false;
                    txtCustName.ReadOnly = false;
                    txtCustAddress.ReadOnly = false;
                    txtCustAddress2.ReadOnly = false;
                    //txtCustTown.ReadOnly = true;
                    //txtCustTelephone.ReadOnly = true;
                    //txtCustMobile.ReadOnly = true;
                    txtItmCd.Text = "";
                    btnItemSearch.Enabled = false;
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
        private void LoadJob(string _com, string _jobNo, string _jobStage)
        { 
            int _returnStatus = 0;
            string _returnMsg = string.Empty;

            _scvjobHdr = new Service_JOB_HDR();
            _scvItemList = new List<Service_job_Det>();
            _scvDefList = new List<Service_Job_Defects>();
            _scvEmpList = new List<Service_Tech_Aloc_Hdr>();
            _scvItemSubList = new List<Service_Job_Det_Sub>();
            _tempItemSubList = new List<Service_Job_Det_Sub>();
            _scvStdbyList = new List<Service_TempIssue>();

            _returnStatus = CHNLSVC.CustService.GetScvJob(_com, _jobNo, out _scvjobHdr, out  _scvItemList, out  _scvItemSubList, out  _scvDefList, out  _scvEmpList, out  _scvStdbyList, out  _returnMsg);
            if (_returnStatus != 1)
            {
                SystemInformationMessage(_returnMsg, "Service Job");
                txtReqNo.Clear();
                txtReqNo.Focus();
                return;
            }
          //  btnSave.Enabled = false;
            groupBox1.Enabled = false;
             _jobRecall = 1;
            if (_jobStage == "1.1")
            {
                if (_scvjobHdr.SJB_JOBSTAGE != Convert.ToDecimal(1.1))
                {
                    SystemInformationMessage("The job is not inspection stage!", "Inspection Job");
                    txtReqNo.Clear();
                    txtReqNo.Focus();
                    return;
                }
            }

            btnPrint.Visible = true;

          //  _jobRecallSeq = _scvjobHdr.SJB_SEQ_NO;
            cmbServiceTypes.Text = _scvjobHdr.SJB_JOBCAT;
            txtNIC.Text = _scvjobHdr.SJB_B_NIC;
            txtCustMobile.Text = _scvjobHdr.SJB_B_MOBINO;
            txtCustCD.Text = _scvjobHdr.SJB_B_CUST_CD;
            txtCustName.Text = _scvjobHdr.SJB_B_CUST_NAME;
          //  cmbTitle.Text = _scvjobHdr.SJB_B_CUST_TIT;
            txtCustAddress.Text = _scvjobHdr.SJB_B_ADD1;
            txtCustAddress2.Text = _scvjobHdr.SJB_B_ADD2;
            txtCustTown.Text = _scvjobHdr.SJB_B_ADD3;
            txtCustTown.Tag = _scvjobHdr.SJB_B_TOWN;
            txtCustTelephone.Text = _scvjobHdr.SJB_B_PHNO;
            txtContPersn.Text = _scvjobHdr.SJB_CNT_PERSON;
            txtContNo.Text = _scvjobHdr.SJB_CNT_PHNO;
            txtContLoc.Text = _scvjobHdr.SJB_CNT_ADD1;

            txtInfoPersn.Text = _scvjobHdr.SJB_INFM_PERSON;
            txtInfoNo.Text = _scvjobHdr.SJB_INFM_PHNO;
            txtInfoLoc.Text = _scvjobHdr.SJB_INFM_ADD1;

            _chkJobStage = _scvjobHdr.SJB_JOBSTAGE;

            _jobSeq = _scvjobHdr.SJB_SEQ_NO;

            if ((Convert.ToDateTime(_scvjobHdr.SJB_CUSTEXPTDT).Date.ToString() != "01/Jan/0001 12:00:00 AM"))
            {
                dtExpectOn.Value = _scvjobHdr.SJB_CUSTEXPTDT.Date;
            }
            else
            {
                dtExpectOn.Value = DateTime.Today.Date;
            }

          //  txtReqRmk.Text = _scvjobHdr.SJB_RMK;
            txtJobRmk.Text = _scvjobHdr.SJB_JOB_RMK;
          //  txtTechIns.Text = _scvjobHdr.SJB_TECH_RMK;

          //  txtManRef.Text = _scvjobHdr.SJB_MANUALREF;
         //   txtOrdRef.Text = _scvjobHdr.SJB_ORDERNO;

            if (_scvjobHdr.SJB_JOBTP == "E")
            {
                ChkExternal.Checked = true;
                ChkExternal.Enabled = false;

                //pnlItem.Visible = true;
                //pnlSer.Enabled = false;
                optJob.Enabled = false;
               // optReq.Enabled = false;
            }

            //if (!string.IsNullOrEmpty(_scvjobHdr.SJB_CHG_CD))
            //{
            //    txtVisitChgCode.Text = _scvjobHdr.SJB_CHG_CD;
            //    lblCharge.Text = lblCharge.Text = String.Format("{0:#,###,###.00}", _scvjobHdr.SJB_CHG.ToString());
            //}

            //grvJobItms.AutoGenerateColumns = false;
            //grvJobItms.DataSource = new List<Service_job_Det>();
            //grvJobItms.DataSource = _scvItemList;

            txtRegNo.Text=_scvItemList[0].Jbd_regno;
           // txtRegNo.Text =_scvItemList[0].Jbd_jobno;
            txtEnginNo.Text =_scvItemList[0].Jbd_ser1;
            txtItmCd.Text =_scvItemList[0].Jbd_itm_cd;
            txtDescription.Text =_scvItemList[0].Jbd_itm_desc;
            txtModel.Text =_scvItemList[0].Jbd_model;
            txtChasseNo.Text = _scvItemList[0].Jbd_ser2;

            txtMilage.Text = Convert.ToString(_scvItemList[0].Jbd_milage);

            dataGridViewDefectList.AutoGenerateColumns = false;
            dataGridViewDefectList.DataSource = new List<Service_Job_Defects>();
            dataGridViewDefectList.DataSource = _scvDefList;
            Defect_List = _scvDefList;

            Defect_LineNo = _scvDefList.Count ;
            //grvTech.AutoGenerateColumns = false;
            //grvTech.DataSource = new List<Service_Tech_Aloc_Hdr>();
            //grvTech.DataSource = _scvEmpList;

            //grvAddiItems.AutoGenerateColumns = false;
            //grvAddiItems.DataSource = new List<Service_Job_Det_Sub>();
            //grvAddiItems.DataSource = _scvItemSubList;

            //grvAddiItems.AutoGenerateColumns = false;
            //grvAddiItems.DataSource = new List<Service_TempIssue>();
            //grvAddiItems.DataSource = _scvStdbyList;



        }

        private void LoadComboData()
        {
            DataTable dt = new DataTable();

            DataTable datasource = CHNLSVC.Sales.GetServiceTypes(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
            DataRow drTemp = datasource.NewRow();

            dt.Columns.Add("SC_TP", typeof(string));
            dt.Columns.Add("SC_DESC", typeof(string));

            drTemp["SC_TP"] = "--Select--";
            drTemp["SC_DESC"] = "--Select--";

            datasource.Rows.Add(drTemp);

            dt.Merge(datasource);
 
            if (dt != null && dt.Rows.Count > 0)
            {
                ComboBoxDraw(dt, cmbServiceTypes, "SC_TP", "SC_DESC");
            } 

            //-----------------------------------------------
            //DataTable defcTypes = CHNLSVC.Sales.Get_DefectTypes(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
            //if (defcTypes != null && defcTypes.Rows.Count > 0)
            //{
            //    string defaultDefect = string.Empty;
            //    ComboBoxDraw(defcTypes, cmbDefectTypes, "SDT_CD", "SDT_DESC");
            //    foreach (DataRow dr in defcTypes.Rows)
            //    {
            //        if (Convert.ToInt16(dr["SDT_DEF"]) == 1)
            //        {
            //            defaultDefect = dr["SDT_CD"].ToString();
            //        }
            //    }
            //    if (!(defaultDefect == string.Empty))
            //    {
            //        cmbDefectTypes.SelectedValue = defaultDefect;
            //    }

            //}
        }

        private void ComboBoxDraw(DataTable table, ComboBox combo, string code, string desc)
        {
            combo.DataSource = table;
            combo.DisplayMember = desc;
            combo.ValueMember = code;

            // Enable the owner draw on the ComboBox.
            combo.DrawMode = DrawMode.OwnerDrawVariable;
            // Handle the DrawItem event to draw the items.
            combo.DrawItem += delegate(object cmb, DrawItemEventArgs args)
            {

                // Draw the default background
                args.DrawBackground();


                // The ComboBox is bound to a DataTable,
                // so the items are DataRowView objects.
                DataRowView drv = (DataRowView)combo.Items[args.Index];

                // Retrieve the value of each column.
                string id = drv[code].ToString();
                string name = drv[desc].ToString();

                // Get the bounds for the first column
                Rectangle r1 = args.Bounds;
                r1.Width /= 2;

                // Draw the text on the first column
                using (SolidBrush sb = new SolidBrush(args.ForeColor))
                {
                    args.Graphics.DrawString(id, args.Font, sb, r1);
                }

                // Draw a line to isolate the columns 
                using (Pen p = new Pen(Color.Black))
                {
                    args.Graphics.DrawLine(p, r1.Right - 5, 0, r1.Right - 5, r1.Bottom);
                }

                // Get the bounds for the second column
                Rectangle r2 = args.Bounds;
                r2.X = args.Bounds.Width / 2;
                r2.Width /= 2;

                // Draw the text on the second column
                using (SolidBrush sb = new SolidBrush(args.ForeColor))
                {
                    args.Graphics.DrawString(name, args.Font, sb, r2);
                }
            };
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //InventorySerialMaster veh = new InventorySerialMaster();
            //if (txtRegNo.Text.Trim() == "")
            //{
            //    MessageBox.Show("Please enter vehicle registration number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            //veh = CHNLSVC.Sales.GetVehicleDetails(txtRegNo.Text.Trim(), null, null);
            //if (veh.Irsm_ser_1 == null || veh.Irsm_ser_2 == null)
            //{
            //    MessageBox.Show("Invalid vehicle registration number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            //VehicleObect = veh;
            //Load_VehicleDetails(veh);
        }

        private void btnVehicleSearch_Click(object sender, EventArgs e)
        {
            //pnlMain.Enabled = false;
            //pnlPopupVeh.Visible = true;
            //toolStrip1.Enabled = false;
            //txtPopUpType.Focus();
            try
            {
                Cursor = Cursors.WaitCursor;
                _warrSearchtp = _scvParam.SP_DB_SERIAL;
                _warrSearchorder = "WARR";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                DataTable _result = new DataTable();
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceSerial);
                _result = CHNLSVC.CommonSearch.SearchWarranty(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtRegNo;
                _CommonSearch.ShowDialog();
                txtRegNo.Select();
                Cursor = Cursors.Default;

                InventorySerialMaster veh = new InventorySerialMaster();
                if (txtRegNo.Text.Trim() == "")
                {
                    return;
                }
                veh = CHNLSVC.Sales.GetVehicleDetails(null, txtRegNo.Text.Trim(), null);
                //if (veh.Irsm_ser_1 == null || veh.Irsm_ser_2 == null)
                //{
                //    MessageBox.Show("Invalid vehicle registration number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
                VehicleObect = veh;
                Load_VehicleDetails(false);
               // txtRegNo.Text = veh.Irsm_reg_no;
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
        #region Common Message
        private void SystemErrorMessage(Exception ex)
        { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        private void SystemWarnningMessage(string _msg, string _title)
        { this.Cursor = Cursors.Default; MessageBox.Show(_msg, _title, MessageBoxButtons.OK, MessageBoxIcon.Warning); }

            private void SystemInformationMessage(string _msg, string _title)
        { this.Cursor = Cursors.Default; MessageBox.Show(_msg, _title, MessageBoxButtons.OK, MessageBoxIcon.Information); }
        
        #endregion


        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.VehicalJobRegistrationNo:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceSerial:
                    {
                        paramsText.Append(_warrSearchtp + seperator + _warrSearchorder + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DefectTypes:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + _scvParam.SP_SERCHNL + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Town:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceJobSearch:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + _jobStage + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.NIC:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Mobile:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        private void btnPopSearchOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPopUpType.Text.Trim() != string.Empty)
                {
                    if (cmbPopUpSearchType.SelectedItem.ToString() == "Engine #")
                    {
                        InventorySerialMaster veh = CHNLSVC.Sales.GetVehicleDetails(null, txtPopUpType.Text.Trim(), null);
                        Load_VehicleDetails(false);
                    }
                    else if (cmbPopUpSearchType.SelectedItem.ToString() == "Chasse #")
                    {
                        InventorySerialMaster veh = CHNLSVC.Sales.GetVehicleDetails(null, null, txtPopUpType.Text.Trim());
                        Load_VehicleDetails(false);
                    }
                    pnlPopupVeh.Visible = false;
                    pnlMain.Enabled = true;
                    toolStrip1.Enabled = true;

                    txtPopUpType.Text = "";
                    cmbPopUpSearchType.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Please enter search term", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
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

        private void cmbServiceTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMilage.Focus();
        }

        private void grvDefectList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string type = grvDefectList.Rows[e.RowIndex].Cells[1].Value.ToString();
                        string remark = grvDefectList.Rows[e.RowIndex].Cells[2].Value.ToString();

                        Defect_List.RemoveAll(x => x.SRD_DEF_TP == type && x.SRD_DEF_RMK == remark);
                        BindingSource source = new BindingSource();
                        source.DataSource = Defect_List;
                        grvDefectList.DataSource = source;

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

        #region link click

        private void lnkAdd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }

        private void lnkView_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlMain.Enabled = false;
            pnlPopUpDefectList.Visible = true;
            toolStrip1.Enabled = false;
            grvDefectList.Focus();
        }

        private void lnkLastService_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
          
        }

        #endregion

        #region popup close

        private void btnPopupLastClose_Click(object sender, EventArgs e)
        {
            pnlPopupLastDefect.Visible = false;
            pnlMain.Enabled = true;
            toolStrip1.Enabled = true;

            //clear
            lblLastSevPayOrFree.Text = "";
            lblLastSevJobDt.Text = "";
            lblLastSevJobNo.Text = "";
            lblLastSevMilage.Text = "";
            lblLastServNoOfAttempt.Text = "";
        }

        private void btnPopupDefectClose_Click(object sender, EventArgs e)
        {
            pnlMain.Enabled = true;
            toolStrip1.Enabled = true;
            pnlPopUpDefectList.Visible = false;
        }

        private void btnPopupVehClose_Click(object sender, EventArgs e)
        {
            cmbPopUpSearchType.SelectedIndex = 0;
            txtPopUpType.Text = "";
            pnlPopupVeh.Visible = false;
            pnlMain.Enabled = true;
            toolStrip1.Enabled = true;
        }
        
        #endregion

        #region data load

        private void Load_VehicleDetails(Boolean isRecall)
        {

            DataTable DelCustomer = new DataTable();
            
            int _returnStatus = 0;
            string _returnMsg = string.Empty;
            List<InventorySerialMaster> _warrMst = new List<InventorySerialMaster>();
            List<InventorySubSerialMaster> _warrMstSub = new List<InventorySubSerialMaster>();
            Dictionary<List<InventorySerialMaster>, List<InventorySubSerialMaster>> _warrMstDic = null;
           
                InventorySerialMaster veh = new InventorySerialMaster();
                if (txtRegNo.Text.Trim() == "" || ChkExternal.Checked==true)
                {
                    return;
                }
              //  veh = CHNLSVC.Sales.GetVehicleDetails(txtRegNo.Text.Trim(), null, null);

                _warrMstDic = CHNLSVC.CustService.GetWarrantyMaster(null, "", txtRegNo.Text.Trim(), null, "", "", 0, out _returnStatus, out _returnMsg);

                if (_warrMstDic == null)
                {
                    SystemInformationMessage("There is no warranty details available.", "No warranty");
                    txtRegNo.Clear();
                    txtRegNo.Focus();
                    return;
                }

                foreach (KeyValuePair<List<InventorySerialMaster>, List<InventorySubSerialMaster>> pair in _warrMstDic)
                {
                    _warrMst = pair.Key;
                    _warrMstSub = pair.Value;
                   
                }
             
                VehicleObect = veh;
                veh=_warrMst[0];

            txtEnginNo.Text = veh.Irsm_ser_1;
            txtChasseNo.Text = veh.Irsm_ser_2;
            txtItmCd.Text = veh.Irsm_itm_cd;
            txtModel.Text = veh.Irsm_itm_model;
            txtDescription.Text = veh.Irsm_itm_desc;
          
            //-------------------------------------------
            lblBuyerCustCode.Text = veh.Irsm_cust_cd;
            lblBuyerCustName.Text = veh.Irsm_cust_name;
            lblBuyerCustAdd1.Text = veh.Irsm_cust_addr;
            //    lblBuyerCustAdd2.Text = _reqHdr.Srb_add2;
            lblBuyerCustMobi.Text = veh.Irsm_cust_mobile;
            lblPhone.Text = veh.Irsm_cust_tel;

            lblTown.Text = veh.Irsm_cust_town;
          

            if (isRecall == false)
            {

                DelCustomer = CHNLSVC.Sales.GetDeliverCustomer(veh.Irsm_invoice_no);
                foreach (DataRow row in DelCustomer.Rows)
                {
                    txtCustName.Text = row["MBE_NAME"].ToString();
                    txtCustCD.Text = row["mbe_cd"].ToString();
                    txtCustTelephone.Text = row["MBE_TEL"].ToString();
                    txtCustMobile.Text = row["MBE_MOB"].ToString();
                    if (row["mbe_town_cd"].ToString() != "")
                    {
                        txtCustTown.Text = row["mbe_town_cd"].ToString();
                    }
                    else
                    {
                        txtCustTown.Text = string.Empty;
                    }

                    txtCustAddress.Text = row["ITH_DEL_ADD1"].ToString() ;
                    txtCustAddress2.Text = row["ITH_DEL_ADD2"].ToString();
                }
                if (DelCustomer.Rows.Count == 0)
                {
                    txtCustName.Text = veh.Irsm_cust_name;
                    txtCustCD.Text = veh.Irsm_cust_cd;
                    txtCustTelephone.Text = veh.Irsm_cust_tel;
                    txtCustMobile.Text = veh.Irsm_cust_mobile;
                    if (veh.Irsm_cust_town != "")
                    {
                        txtCustTown.Text = veh.Irsm_cust_town;
                    }
                    else
                    {
                        txtCustTown.Text = string.Empty;
                    }

                    txtCustAddress.Text = veh.Irsm_cust_addr;
                    txtCustAddress2.Text = "N/A";
                }
            }
            
            lblCustInvoiceNo.Text = veh.Irsm_invoice_no;
            lblCustInvoiceDate.Text = veh.Irsm_invoice_dt.Date.ToShortDateString();
            lblCustDO_no.Text = veh.Irsm_doc_no;
            lblCustInvoiceNo.Text = veh.Irsm_invoice_no;
            _warranty = veh.Irsm_warr_no;
            _warrPrd = veh.Irsm_warr_period;
            _itemStatus = veh.Irsm_itm_stus;
            _warrStdate = veh.Irsm_warr_start_dt;
            lblWarrNo.Text = veh.Irsm_warr_no;





            _masterBusinessCompany = new MasterBusinessEntity();
            if (isRecall == false)
            {
                _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(txtCustCD.Text, null, null, null, null, BaseCls.GlbUserComCode);
                if (_masterBusinessCompany.Mbe_cd != null)
                {
                    txtNIC.Text = _masterBusinessCompany.Mbe_nic;
                    lblNIC.Text = _masterBusinessCompany.Mbe_nic;
                }
            }

            MasterItem _item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItmCd.Text.ToUpper());
            if (_item != null)
            {
                
                lblItemCat.Text = _item.Mi_cate_1;
                _itemType = _item.Mi_itm_tp;
              
                _itemBrand = _item.Mi_brand;
                 IsFreeService=  _item.Mi_need_freesev;
      
            }



            //-------------------------------------------
            //lblWarrEndDate.Text=veh.wa
            TimeSpan _noDays ;
            lblWarrPeriod.Text = veh.Irsm_warr_period.ToString();
            //lblWarrEndDate.Text = veh.Irsm_warr_start_dt.Date.AddDays(veh.Irsm_warr_period).ToString();
            lblWarrEndDate.Text = veh.Irsm_warr_start_dt.Date.AddMonths(veh.Irsm_warr_period).ToShortDateString();
            if ((Convert.ToDateTime(lblWarrEndDate.Text).Date > DateTime.Today.Date))
            {
                _noDays =  Convert.ToDateTime(lblWarrEndDate.Text).Date - DateTime.Today.Date;
                lblWarrRemainingDays.Text = Convert.ToString(_noDays.TotalDays)  ;
                lblWarrStatus.Text = "AVAILABLE";
                Color myColor = Color.Green;
                lblWarrStatus.ForeColor = myColor;
            }
            else if ((Convert.ToDateTime(lblWarrEndDate.Text).Date.ToString("yyyy/mmm") =="0001/00"))
            {
                lblWarrRemainingDays.Text = "0";
                lblWarrStatus.Text = "INVALID";
                Color myColor = Color.Red;
                lblWarrStatus.ForeColor = myColor;
            }
            else
            {
                lblWarrRemainingDays.Text = "0";
                lblWarrStatus.Text = "Expired";
                Color myColor = Color.Red;
                lblWarrStatus.ForeColor = myColor;
            }
            lblWarrCoopenNo.Text = veh.Irsm_ser_4;



             _warStus =( lblWarrStatus.Text == "AVAILABLE") ? 1 : 0;
            //-------------------------------------------


        }

        private void VehicleJobShowroom_Load(object sender, EventArgs e)
        {
            try
            {
                _scvParam = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());
                if (_scvParam == null)
                {
                    SystemWarnningMessage("Service parameter(s) not setup!", "Default Parameter(s)");
                    this.Close();
                }
            
                ChkExternal_CheckedChanged(null, null);
                cmbPopUpSearchType.SelectedIndex = 0;
                DateTime _date = CHNLSVC.Security.GetServerDateTime();
                dateTimePickerDate.Value = _date.Date;
                LoadComboData();
                cmbServiceTypes.Text = "--Select--";
                //if backdate need 
                bool _allowCurrentTrans = false;
                IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, dateTimePickerDate, lblBackDate, string.Empty, out _allowCurrentTrans);
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

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (pnlPopUpDefectList.Visible)
                {
                    pnlPopUpDefectList.Visible = false;
                    pnlMain.Enabled = true;
                    toolStrip1.Enabled = true;
                }
                else if (pnlPopupLastDefect.Visible)
                {
                    pnlPopupLastDefect.Visible = false;
                    pnlMain.Enabled = true;
                    toolStrip1.Enabled = true;

                    lblLastServNoOfAttempt.Text = "";
                    lblLastSevJobDt.Text = "";
                    lblLastSevJobNo.Text = "";
                    lblLastSevMilage.Text = "";
                    lblLastSevPayOrFree.Text = "";
                }
                else if (pnlPopupVeh.Visible)
                {
                    pnlPopupVeh.Visible = false;
                    pnlMain.Enabled = true;
                    toolStrip1.Enabled = true;

                    cmbPopUpSearchType.SelectedIndex = 0;
                    txtPopUpType.Text = "";
                }
                else
                {
                    if (MessageBox.Show("Are you want to quit?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.Close();
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

        protected override bool ProcessKeyPreview(ref Message m)
        {
            if (m.Msg == 0x0100 && (int)m.WParam == 13)
            {
                this.ProcessTabKey(true);
            }
            return base.ProcessKeyPreview(ref m);
        }

        private void txtRegNo_Leave(object sender, EventArgs e)
        {
            try
            {
                Load_VehicleDetails(false);
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

        private void txtMilage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtCustMobile_Leave(object sender, EventArgs e)
        {
            if (txtCustMobile.Text == "N/A")
            {
                txtCustMobile.Text = "";
            }

            if (!String.IsNullOrEmpty(txtCustMobile.Text))
            {
                Boolean _isValid = IsValidMobileOrLandNo(txtCustMobile.Text.Trim());

                if (_isValid == false)
                {
                    MessageBox.Show("Invalid mobile number.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCustMobile.Text = "";
                    txtCustMobile.Focus();
                    return;
                }
                else
                {
                    if (_jobRecall == 0)
                    {
                        LoadCustomerDetailsByMobile();
                    }
                }
            }
        }

        private void txtCustTelephone_Leave(object sender, EventArgs e)
        {
            if (txtCustTelephone.Text == "N/A")
            {
                txtCustTelephone.Text = "";
            }
            if (!string.IsNullOrEmpty(txtCustTelephone.Text))
            {
                if (!IsValidMobileOrLandNo(txtCustTelephone.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please Enter the valid contact", "Customer Mobile", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCustTelephone.Text = ""; return;
                }
            }
        }

        private void dataGridViewDefectList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string type = dataGridViewDefectList.Rows[e.RowIndex].Cells[1].Value.ToString();
                        string remark = dataGridViewDefectList.Rows[e.RowIndex].Cells[2].Value.ToString();

                        Defect_List.RemoveAll(x => x.SRD_DEF_TP == type && x.SRD_DEF_RMK == remark);
                        BindingSource source = new BindingSource();
                        source.DataSource = Defect_List;
                        dataGridViewDefectList.DataSource = source;

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

        private void txtRegNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                cmbServiceTypes.Focus();
            }
            if(e.KeyCode==Keys.F2)
                btnVehicleSearch_Click(null, null);
        }

        private void txtRegNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnVehicleSearch_Click(null, null);
        }

        private void txtRegNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_srch_def_type_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRegNo.Text == "")
                {
                    MessageBox.Show("Please select registration #", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                DataTable _result = new DataTable();
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DefectTypes);
                _result = CHNLSVC.CommonSearch.GetDefectTypes(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDef;
                _CommonSearch.ShowDialog();
                txtDef.Focus();
                txtDef.SelectAll();
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                SystemErrorMessage(ex);
            }
        }
        private bool load_def_desc()
        {
            bool _ok = false;
            lblDefDesc.Text = "";
            if (!string.IsNullOrEmpty(txtDef.Text))
            {
                DataTable _dt = CHNLSVC.CustService.getDefectTypes(BaseCls.GlbUserComCode, _scvParam.SP_SERCHNL, lblItemCat.Text, txtDef.Text);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    { lblDefDesc.Text = _dt.Rows[0]["SD_DESC"].ToString(); _ok = true; }
                }
            }
            return _ok;
        }

        protected void LoadCustomerDetailsByNIC()
        {
            try
            {
                if (string.IsNullOrEmpty(txtNIC.Text))
                {
                    if (txtCustName.Enabled == false)
                    {
                        txtCustCD.Text = "CASH";
                        EnableDisableCustomer();
                    }
                    return;
                }

                _masterBusinessCompany = new MasterBusinessEntity();
                this.Cursor = Cursors.WaitCursor;
                if (!string.IsNullOrEmpty(txtNIC.Text))
                {
                    if (!IsValidNIC(txtNIC.Text))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Please select the valid NIC", "Customer NIC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtNIC.Text = ""; return;
                    }
                    List<MasterBusinessEntity> _custList = CHNLSVC.Sales.GetActiveCustomerDetailList(BaseCls.GlbUserComCode, string.Empty, txtNIC.Text, string.Empty, "C");
                    if (_custList != null && _custList.Count > 0)
                    {
                        if (_custList.Count > 1)
                        {
                            //Tempory removed by Chamal 26-04-2014
                            //if (string.IsNullOrEmpty(txtCustomer.Text) || txtCustomer.Text.Trim() == "CASH") MessageBox.Show("There are " + _custList.Count + " number of active customers are available for the selected NIC.\nPlease contact Accounts Dept.", "Customers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); txtNIC.Clear(); txtNIC.Focus(); return;
                        }
                    }


                    //_masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, string.Empty, txtNIC.Text, string.Empty, "C");
                    _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(null, txtNIC.Text, null, null, null, BaseCls.GlbUserComCode);
                }
                if (!string.IsNullOrEmpty(_masterBusinessCompany.Mbe_cd))
                {
                    if (_masterBusinessCompany.Mbe_act == true)
                    {
                        SetCustomerAndDeliveryDetails(false, null);
                        //ViewCustomerAccountDetail(txtCustomer.Text);
//GetNICGender();
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("This customer already inactive. Please contact accounts dept.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearCustomer(true);
                        txtCustCD.Focus();
                        return;
                    }
                }
                else
                {

                    GroupBussinessEntity _grupProf = CHNLSVC.Sales.GetCustomerProfileByGrup(null, txtNIC.Text.Trim().ToUpper(), null, null, null, null);
                    if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                    {
                       //GetNICGender();
                        SetCustomerAndDeliveryDetailsGroup(_grupProf);
                        _isGroup = true;
                    }

                }

                EnableDisableCustomer();
                txtCustMobile.Focus();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            { ClearCustomer(true); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private void SetCustomerAndDeliveryDetailsGroup(GroupBussinessEntity _cust)
        {
            txtCustCD.Text = _cust.Mbg_cd;
            txtCustName.Text = _cust.Mbg_name;
            txtCustAddress.Text = _cust.Mbg_add1;
            txtCustAddress2.Text = _cust.Mbg_add2;
           // txtAddress2.Text = _cust.Mbg_add2;
            txtCustMobile.Text = _cust.Mbg_mob;
            txtNIC.Text = _cust.Mbg_nic;
           // cmbTitle.Text = _cust.Mbg_tit;
        }
        private void EnableDisableCustomer()
        {
            if (txtCustCD.Text == "CASH")
            {
                txtCustCD.Enabled = true;
                txtCustName.Enabled = true;
                txtCustAddress.Enabled = true;
                txtCustAddress2.Enabled = true;
               // txtAddress2.Enabled = true;
                txtCustMobile.Enabled = true;
                txtNIC.Enabled = true;

                btnSearch_NIC.Enabled = true;
                btnSearch_CustCode.Enabled = true;
                btnSearch_Mobile.Enabled = true;
            }
            else
            {
                //txtCustomer.Enabled = false;
                txtCustName.Enabled = false;
                txtCustAddress.Enabled = false;
                txtCustAddress2.Enabled = false;
              //  txtAddress2.Enabled = false;
            //    txtCustMobile.Enabled = false;
             //   txtNIC.Enabled = false;

                //btnSearch_NIC.Enabled = false;
                //btnSearch_Customer.Enabled = false;
                //btnSearch_Mobile.Enabled = false;
            }
        }
        protected void LoadCustomerDetailsByMobile()
        {
            try
            {
                if (string.IsNullOrEmpty(txtCustMobile.Text))
                {
                    if (txtCustName.Enabled == false)
                    {
                        txtCustCD.Text = "CASH";
                        EnableDisableCustomer();
                    }
                    return;
                }
                _masterBusinessCompany = new MasterBusinessEntity();
                this.Cursor = Cursors.WaitCursor;
                if (!string.IsNullOrEmpty(txtCustMobile.Text))
                {
                    if (!IsValidMobileOrLandNo(txtCustMobile.Text))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Please select the valid mobile", "Customer Mobile", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCustMobile.Text = ""; return;
                    }
                    List<MasterBusinessEntity> _custList = CHNLSVC.Sales.GetActiveCustomerDetailList(BaseCls.GlbUserComCode, string.Empty, string.Empty, txtCustMobile.Text, "C");
                    if (_custList != null && _custList.Count > 0)
                    {
                        if (_custList.Count > 1)
                        {
                            //Tempory removed by Chamal 26-04-2014
                            //if (string.IsNullOrEmpty(txtCustomer.Text) || txtCustomer.Text.Trim() == "CASH") MessageBox.Show("There are " + _custList.Count + " number of customers are available for the selected mobile.", "Customers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); txtMobile.Clear(); txtMobile.Focus(); return;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtCustCD.Text) && txtCustCD.Text.Trim() != "CASH")
                        _masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustCD.Text.Trim(), string.Empty, txtCustMobile.Text, "C");
                    else
                        _masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, string.Empty, string.Empty, txtCustMobile.Text, "C");


                }
                //if (!string.IsNullOrEmpty(_masterBusinessCompany.Mbe_cd) && txtCustomer.Text != "CASH")
                if (!string.IsNullOrEmpty(_masterBusinessCompany.Mbe_cd))
                {
                    if (_masterBusinessCompany.Mbe_act == true)
                    {
                        SetCustomerAndDeliveryDetails(false, null);
                        //ViewCustomerAccountDetail(txtCustomer.Text);
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("This customer already inactive. Please contact accounts dept.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearCustomer(true);
                        txtCustCD.Focus();
                        return;
                    }
                }
                else
                {
                    GroupBussinessEntity _grupProf = CHNLSVC.Sales.GetCustomerProfileByGrup(null, null, null, null, null, txtCustMobile.Text.Trim().ToUpper());
                    if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                    {
                        SetCustomerAndDeliveryDetailsGroup(_grupProf);
                        _isGroup = true;
                    }
                    else
                    {
                        _isGroup = false;
                    }
                }
                EnableDisableCustomer();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            { ClearCustomer(true); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private void txtDef_Leave(object sender, EventArgs e)
        {



            try
            {


                if (!string.IsNullOrEmpty(txtDef.Text))
                {
                    if (load_def_desc() == false)
                    {
                        SystemInformationMessage("Invalid defect type!", "Defect Type");
                        txtDef.Clear();
                        lblDefDesc.Text = "";
                        txtDef.Focus();
                    }
                }

                //clear labels
                lblWarrIsFree.Text = string.Empty;
                lblServiceCount.Text = string.Empty;
                lblWarrServiceMilage.Text = string.Empty;
                lblWarrServiceDays.Text = string.Empty;
                lblServiceCount.Text = string.Empty;
                ServiceTerm = 0;
                IsService = 0;
                IsUpdateShedule = 0;

                if (txtChasseNo.Text == "" && txtEnginNo.Text.Trim() == "")
                {
                    return;
                }
                DataTable datasource = CHNLSVC.CustService.getDefectTypes(BaseCls.GlbUserComCode, _scvParam.SP_SERCHNL, lblItemCat.Text, txtDef.Text);
        
                foreach (DataRow dr in datasource.Rows)
                {

                    if (dr["sd_tp"].ToString() == txtDef.Text  && dr["sd_kind"] != DBNull.Value &&  dr["sd_kind"].ToString() == "S")
                    {
                        IsService = 1;
                        //load the current service.
                        DataTable serviceDet = CHNLSVC.Sales.GetNextServiceDet(txtRegNo.Text.Trim(), txtEnginNo.Text.Trim(), txtChasseNo.Text.Trim());
                        if (serviceDet != null && serviceDet.Rows.Count > 0)
                        {
                            string from_distance = serviceDet.Rows[0]["IRSV_WARR_PD_FROM"].ToString();//irsv_warr_pd_from,irsv_warr_pd_to,     
                            string to_distance = serviceDet.Rows[0]["IRSV_WARR_PD_TO"].ToString();
                            string mesure_distance = serviceDet.Rows[0]["IN_DISTANCE"].ToString();//in_distance
                            lblWarrServiceMilage.Text = from_distance + " - " + to_distance + " " + mesure_distance;


                            string from_period = serviceDet.Rows[0]["IRSV_WARR_PDALT_FROM"].ToString();//irsv_warr_pdalt_from
                            string to_period = serviceDet.Rows[0]["IRSV_WARR_PDALT_TO"].ToString();//irsv_warr_pdalt_to
                            string mesure_period = serviceDet.Rows[0]["IN_INPERIOD"].ToString();//in_inperiod
                            lblWarrServiceDays.Text = from_period + " - " + to_period + " " + mesure_period;


                            string term = serviceDet.Rows[0]["IRSV_SEV_TERM"].ToString();//irsv_sev_term
                            lblServiceCount.Text = "Service : " + term;

                            if (Convert.ToInt16(serviceDet.Rows[0]["IRSV_IS_FREE"]) == 1)// irsv_is_free
                            {
                                lblWarrIsFree.Text = "FREE SERVICE";
                                Color myColor = Color.Green;
                                lblWarrIsFree.ForeColor = myColor;
                                IsUpdateShedule = 1; //Update the schedule table
                            }

                            //divNextService.Style.Add("background-color", " #FFFFCC");// background-color: #FFFFCC

                            ServiceTerm = Convert.ToInt32(term);
                            lblSlash.Visible = true;
                        }
                        else
                        {
                            lblWarrIsFree.Text = "CHARGE SERVICE";
                            Color myColor = Color.Red;
                            lblWarrIsFree.ForeColor = myColor;
                            DataTable lastserviceDet = CHNLSVC.Sales.GetLastServiceDet(txtRegNo.Text.Trim(), txtEnginNo.Text.Trim(), txtChasseNo.Text.Trim());
                            if (lastserviceDet.Rows.Count > 0)
                            {
                                lblServiceCount.Text = "Service : " + (Convert.ToInt32(lastserviceDet.Rows[0]["IRSV_SEV_TERM"]) + 1); //payed sercice

                                IsUpdateShedule = 0; //insert to schedule table
                                lblSlash.Visible = false;
                            }

                        }
                        //back color
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

        private void txtDef_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDef_KeyDown(object sender, KeyEventArgs e)
        {


            if (e.KeyCode == Keys.F2)
            {
                btn_srch_def_type_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtDefectRmk.Focus();
            }
        }

        private void btnItemSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnItemSearch_Click_1(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItmCd;
                _CommonSearch.ShowDialog();
                txtItmCd.Select();
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

        private void txtItmCd_Leave(object sender, EventArgs e)
        {
            try
            {



                if (string.IsNullOrEmpty(txtItmCd.Text)) return;

              //  this.Cursor = Cursors.WaitCursor;
                if (!string.IsNullOrEmpty(txtItmCd.Text)) _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItmCd.Text);
                if (_itemdetail != null && !string.IsNullOrEmpty(_itemdetail.Mi_cd))
                {
                    txtDescription.Text = _itemdetail.Mi_shortdesc;
                    txtModel.Text = _itemdetail.Mi_model;
                    lblItemCat.Text = _itemdetail.Mi_cate_1;
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the valid item code", "Service Parameters", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); txtItmCd.Clear();
                    txtItmCd.Focus();

                    return;
                }
                Cursor.Current = Cursors.Default;

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

        private void txtItmCd_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_srch_town_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town);
                DataTable _result = CHNLSVC.CommonSearch.GetTown(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCustTown;
                _CommonSearch.ShowDialog();
                txtCustTown.Select();
            }
            catch (Exception ex) { txtCustTown.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
        }

        private void txtCustTown_Leave(object sender, EventArgs e)
        {
            if (txtCustTown.Text == "N/A")
            {
                txtCustTown.Text = "";
            }

            if (!string.IsNullOrEmpty(txtCustTown.Text))
            {
                DataTable dt = new DataTable();

                dt = CHNLSVC.General.Get_DetBy_town(txtCustTown.Text.Trim());
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                       
                        txtCustTown.Tag = dt.Rows[0]["TOWN_ID"].ToString();
              

                    }
                    else
                    {
                        MessageBox.Show("Invalid town.", "Town", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCustTown.Text = "";
                        txtCustTown.Focus();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid town.", "Town", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCustTown.Text = "";
                    txtCustTown.Focus();
                    return;
                }
            }
        }

        private void txtDef_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_def_type_Click(null, null);
        }

        private void btn_add_def_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtDef.Text.Trim() == "")
                {
                    MessageBox.Show("Please Enter Defect!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDef.Focus();
                    return;
                }


                Service_Job_Defects defect = new Service_Job_Defects();

                List<Service_Job_Defects> _duplicate = (from _dup in Defect_List
                                                        where _dup.SRD_DEF_TP == txtDef.Text
                                                        select _dup).ToList<Service_Job_Defects>();
                if (_duplicate != null && _duplicate.Count > 0)
                {
                    MessageBox.Show("Duplicated Defect", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    Defect_LineNo++;
                    defect.SRD_DEF_LINE = Defect_LineNo;
                    defect.SRD_DEF_RMK = txtDefectRmk.Text.Trim();
                    defect.SRD_DEF_TP = txtDef.Text;
                    defect.SRD_JOB_LINE = 1;
                    Defect_List.Add(defect);
                    dataGridViewDefectList.AutoGenerateColumns = false;

                    BindingSource source = new BindingSource();
                    source.DataSource = Defect_List;
                    dataGridViewDefectList.DataSource = source;


                         DataTable datasource = CHNLSVC.CustService.getDefectTypes(BaseCls.GlbUserComCode, _scvParam.SP_SERCHNL, lblItemCat.Text, txtDef.Text);

                         foreach (DataRow dr in datasource.Rows)
                         {

                             if (dr["sd_tp"].ToString() == txtDef.Text && dr["sd_kind"] != DBNull.Value && dr["sd_kind"].ToString() == "S")
                             {
                                 IsService = 1;
                             }
                         }






                    txtDefectRmk.Text = "";
                    txtDef.Text = "";
                    lblDefDesc.Text = "";


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

        private void btnMore1_Click(object sender, EventArgs e)
        {
           
        }

        private void txtCustTelephone_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtContNo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtContNo.Text))
            {
                if (!IsValidMobileOrLandNo(txtContNo.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the valid contact", "Customer Mobile", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtContNo.Text = ""; return;
                }
            }
        }

        private void txtInfoNo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtInfoNo.Text))
            {
                if (!IsValidMobileOrLandNo(txtInfoNo.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the valid contact", "Customer Mobile", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtInfoNo.Text = ""; return;
                }
            }
        }

        private void txtCustMobile_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           
            try
            {

                if (_chkJobStage == 8)
                {
                    MessageBox.Show("Job  already invoiced, unable to ammend!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (_chkJobStage == 12)
                {
                    MessageBox.Show("Job  already Cancelled!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                btnSave.Enabled = false;
                DateTime _date = CHNLSVC.Security.GetServerDateTime();
                decimal val;
                    bool _allowCurrentTrans = false;
                    if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, dateTimePickerDate, lblBackDate, dateTimePickerDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
                    {

                        if (_allowCurrentTrans == true)
                        {
                            if (dateTimePickerDate.Value.Date != DateTime.Now.Date)
                            {
                                dateTimePickerDate.Enabled = true;
                                MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                dateTimePickerDate.Focus();
                                return;
                            }
                        }
                        else
                        {
                            dateTimePickerDate.Enabled = true;
                            MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dateTimePickerDate.Focus();
                            return;
                        }
                    }
                if (txtEnginNo.Text.Trim() == string.Empty && txtChasseNo.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Please Enter vehicle", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnSave.Enabled = true;
                    return;
                }
                if (Defect_List == null || Defect_List.Count < 1)
                {
                    MessageBox.Show("Please Add Defects!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnSave.Enabled = true;
                    return;
                }
                if (cmbServiceTypes.Text.Trim() == string.Empty || cmbServiceTypes.Text == "--Select--")
                {  MessageBox.Show("Please Enter service type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnSave.Enabled = true;
                    return;
                }
                

                if (ChkExternal.Checked == true)  
                {
                   if (txtItmCd.Text.Trim() == "")
                    {
                        MessageBox.Show("Please Enter Item Code!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnSave.Enabled = true;
                        return;}
                }
            
                if (txtMilage.Text.Trim() == "")
                {
                    MessageBox.Show("Please Enter Mileage!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnSave.Enabled = true;
                    return;
                }
                if (txtCustTown.Text.Trim() == "")
                {
                    MessageBox.Show("Please Enter Town!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnSave.Enabled = true;
                    return;
                }
                if (txtCustMobile.Text.Trim() == "")
                {
                    MessageBox.Show("Please Enter Mobile #!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnSave.Enabled = true;
                    return;
                }
                //TODO: need clarify
                //if (ChkExternal.Checked && lblItmCD.Text == "")
                //{
                //    MessageBox.Show("Please enter No Item Code!","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                //    return;
                //}
                if (!decimal.TryParse(txtMilage.Text, out val))
                {
                    MessageBox.Show("Mileage has to be a number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnSave.Enabled = true;
                    return;
                }
                try
                {

                    //------------------Job Header Filling--------------------------------
                    DataTable _loc = CHNLSVC.Inventory.Get_location_by_code(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);

                    Service_JOB_HDR sevHdr = new Service_JOB_HDR();
                    sevHdr.SJB_COM = BaseCls.GlbUserComCode;
                    sevHdr.SJB_DT = _date.Date;
                    sevHdr.SJB_CRE_BY = BaseCls.GlbUserID;
                    sevHdr.SJB_CRE_DT = DateTime.Now.Date;
                    //sevHdr.Sjb_pc = BaseCls.GlbUserDefProf;
                    sevHdr.SJB_JOBCAT = BaseCls.GlbUserDefLoca;
                    sevHdr.SJB_CUSTEXPTDT = dtExpectOn.Value.Date;
                    //sevHdr.SJB_ST_DT = _date;
                    //sevHdr.SJB_ED_DT = _date;
                    //if (_loc.Rows.Count > 0)
                    //{
                    //    sevHdr. = _loc.Rows[0]["ML_SEV_CHNL"].ToString();
                    //}
                    //sevHdr.Sjb_job_cat = "";
                    sevHdr.SJB_STUS = "P";
                    sevHdr.SJB_JOBSTAGE = 2;
                    //sevHdr.Sjb_chnl=
                    //sevHdr.Sjb_jobno = "12"; 
                    sevHdr.SJB_CUST_CD = txtCustCD.Text.Trim() == string.Empty ? "NOT FOUND" : txtCustCD.Text.Trim();
                    sevHdr.SJB_CUST_NAME = txtCustName.Text.Trim() == string.Empty ? "NOT FOUND" : txtCustName.Text.Trim();
                    sevHdr.SJB_CUST_TIT = " ";
                    sevHdr.SJB_JOB_RMK = txtJobRmk.Text.Trim();

                    sevHdr.SJB_RECALL = _jobRecall;

                    if(_jobRecall!=0)
                    {
                        sevHdr.SJB_JOBNO = txtReqNo.Text;
                        sevHdr.SJB_SEQ_NO = _jobSeq;
                    }
                    
                  
                    sevHdr.SJB_JOBSTP = "SERDESK";
                    sevHdr.SJB_JOBCAT = cmbServiceTypes.SelectedValue.ToString();
                 
                    //sevHdr.SJB_CNT_PERSON = txtContPersn.Text;
                    //sevHdr.SJB_CNT_ADD1 = txtContLoc.Text;
                    //sevHdr.SJB_INFM_PERSON = txtInfoPersn.Text;
                    //sevHdr.SJB_INFM_ADD1 = txtInfoLoc.Text;
                    //sevHdr.SJB_INFM_PHNO = txtInfoNo.Text;
                    //sevHdr.SJB_CNT_PHNO = txtContNo.Text;
                    //sevHdr.SJB_MOBINO = txtCustMobile.Text;
                    //sevHdr.SJB_ADD1 = txtCustAddress.Text;
                    //sevHdr.SJB_B_NIC = txtNIC.Text;

                    sevHdr.SJB_CUST_CD =  lblBuyerCustCode.Text ;
                  //  sevHdr.SJB_CUST_TIT = cmbTitle.Text;
                    sevHdr.SJB_CUST_NAME = lblBuyerCustName.Text;
                    sevHdr.SJB_NIC = lblNIC.Text;
                    //_jobHeader.SJB_DL = 
                    //_jobHeader.SJB_PP = 
                    sevHdr.SJB_MOBINO = lblBuyerCustMobi.Text;
                    sevHdr.SJB_ADD1 = lblBuyerCustAdd1.Text;
                  //  sevHdr.SJB_ADD2 = txtAddress2.Text;
                    sevHdr.SJB_ADD3 = lblTown.Text;
                    sevHdr.SJB_TOWN = lblTown.Text;
                    sevHdr.SJB_PHNO = lblPhone.Text;

                    



                    if (ChkExternal.Checked == false)
                    {
                        sevHdr.SJB_CUST_CD = sevHdr.SJB_CUST_CD;
                        sevHdr.SJB_CUST_TIT = sevHdr.SJB_CUST_TIT;
                        sevHdr.SJB_CUST_NAME = sevHdr.SJB_CUST_NAME;
                        sevHdr.SJB_NIC = sevHdr.SJB_NIC;
                        //_jobHeader.SJB_DL = 
                        //_jobHeader.SJB_PP = 
                        sevHdr.SJB_MOBINO = sevHdr.SJB_MOBINO;
                        sevHdr.SJB_ADD1 = sevHdr.SJB_ADD1;
                        sevHdr.SJB_ADD2 = sevHdr.SJB_ADD2;
                        sevHdr.SJB_ADD3 = sevHdr.SJB_TOWN;
                        sevHdr.SJB_TOWN = sevHdr.SJB_TOWN;

                        sevHdr.SJB_PHNO = sevHdr.SJB_PHNO;
                        //_jobHeader.SJB_FAXNO = 
                        //_jobHeader.SJB_EMAIL = 
                    }
                    else
                    {
                        sevHdr.SJB_CUST_CD = txtCustCD.Text;
                      //  sevHdr.SJB_CUST_TIT = cmbTitle.Text;
                        sevHdr.SJB_CUST_NAME = txtCustName.Text;
                        sevHdr.SJB_NIC = txtNIC.Text;
                        //_jobHeader.SJB_DL = 
                        //_jobHeader.SJB_PP = 
                        sevHdr.SJB_MOBINO = txtCustMobile.Text;
                        sevHdr.SJB_ADD1 = txtCustAddress.Text;
                        sevHdr.SJB_ADD2 = txtCustAddress2.Text;
                        sevHdr.SJB_ADD3 = txtCustTown.Text;
                        sevHdr.SJB_TOWN = txtCustTown.Text;
                        sevHdr.SJB_PHNO = txtCustTelephone.Text;
                        //_jobHeader.SJB_PHNO = 
                        //_jobHeader.SJB_FAXNO = 
                        //_jobHeader.SJB_EMAIL = 
                    }

                    sevHdr.SJB_CNT_PERSON = txtContPersn.Text;
                    sevHdr.SJB_CNT_ADD1 = txtContLoc.Text;
                    //_jobHeader.SJB_CNT_ADD2 = 
                    sevHdr.SJB_CNT_PHNO = txtContNo.Text;
                    sevHdr.SJB_JOB_RMK = txtJobRmk.Text;
                  //  sevHdr.SJB_TECH_RMK = txtTechIns.Text;

                    sevHdr.SJB_B_CUST_CD = txtCustCD.Text;
                   // sevHdr.SJB_B_CUST_TIT = cmbTitle.Text;
                    sevHdr.SJB_B_CUST_NAME = txtCustName.Text;
                    sevHdr.SJB_B_NIC = txtNIC.Text;
                    sevHdr.SJB_B_MOBINO = txtCustMobile.Text;
                    sevHdr.SJB_B_ADD1 = txtCustAddress.Text;
                    sevHdr.SJB_B_ADD2 = txtCustAddress2.Text;
                    sevHdr.SJB_B_ADD3 = txtCustTown.Text.ToString();
                    sevHdr.SJB_B_PHNO =   txtCustTelephone.Text;
                    sevHdr.SJB_B_EMAIL = _email;

                    if (txtCustTown.Tag != null) sevHdr.SJB_B_TOWN = txtCustTown.Tag.ToString();
                    //_jobHeader.SJB_B_PHNO = 
                    //_jobHeader.SJB_B_FAX = 
                    //_jobHeader.SJB_B_EMAIL = 

                    if (string.IsNullOrEmpty(sevHdr.SJB_CUST_CD) && string.IsNullOrEmpty(_scvjobHdr.SJB_CUST_NAME))
                    {
                        sevHdr.SJB_CUST_CD = _scvjobHdr.SJB_B_CUST_CD;
                        sevHdr.SJB_CUST_TIT = _scvjobHdr.SJB_B_CUST_TIT;
                        sevHdr.SJB_CUST_NAME = _scvjobHdr.SJB_B_CUST_NAME;
                        sevHdr.SJB_NIC = _scvjobHdr.SJB_B_NIC;
                        //_jobHeader.SJB_DL = 
                        //_jobHeader.SJB_PP = 
                        sevHdr.SJB_MOBINO = _scvjobHdr.SJB_B_MOBINO;
                        sevHdr.SJB_ADD1 = _scvjobHdr.SJB_B_ADD1;
                        sevHdr.SJB_ADD2 = _scvjobHdr.SJB_B_ADD2;
                        sevHdr.SJB_ADD3 = _scvjobHdr.SJB_B_ADD3;
                        sevHdr.SJB_TOWN = _scvjobHdr.SJB_B_TOWN;

                        sevHdr.SJB_PHNO = _scvjobHdr.SJB_PHNO;
                        //_jobHeader.SJB_FAXNO = 
                        //_jobHeader.SJB_EMAIL = 
                    }

                    sevHdr.SJB_INFM_PERSON = txtInfoPersn.Text;
                    sevHdr.SJB_INFM_ADD1 = txtInfoLoc.Text;
                    //_jobHeader.SJB_INFM_ADD2 = 
                    sevHdr.SJB_INFM_PHNO = txtInfoNo.Text;

                    if (ChkExternal.Checked)
                    {
                        sevHdr.SJB_JOBTP = "E";
                    }
                    else
                    {
                        sevHdr.SJB_JOBTP = "I";
                    }
                    //-------------------Job Herder Job No---------------------------------

                    MasterAutoNumber _jobAuto = new MasterAutoNumber();
                   
                    _jobAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                    _jobAuto.Aut_cate_tp = "LOC";
                    _jobAuto.Aut_moduleid = "SVJOB";
                    _jobAuto.Aut_direction = 0;
                    _jobAuto.Aut_year = sevHdr.SJB_DT.Year;




                    //-------------------Job Detail Filling--------------------------------

                    Service_job_Det sevDet = new Service_job_Det();
                    //sevDet.Jbd_jobno = "12";//_inventoryDAL.GetSerialID();
                    sevDet.Jbd_jobline = 1; //only one item, so one jobline
                   
                    
                        sevDet.Jbd_itm_cd = txtItmCd.Text;
                    sevDet.Jbd_com=   BaseCls.GlbUserComCode;
                    sevDet.Jbd_itm_desc = txtDescription.Text;
                    sevDet.Jbd_model = txtModel.Text;
                    sevDet.Jbd_regno = txtRegNo.Text.Trim();
                    sevDet.Jbd_ser1 = txtEnginNo.Text;
                    sevDet.Jbd_ser2 = txtChasseNo.Text;
                    sevDet.Jbd_loc= BaseCls.GlbUserDefLoca;
                    sevDet.Jbd_pc  = BaseCls.GlbUserDefProf;
                    //if not an external job
                    if (!ChkExternal.Checked)
                    {

                        if (lblWarrStatus.Text == "AVAILABLE")
                        {
                            sevDet.Jbd_warr_stus = 1;
                        }
                        else
                        {
                            sevDet.Jbd_warr_stus = 0;
                        }


                        sevDet.Jbd_warrperiod = _warrPrd;
                        sevDet.Jbd_itm_stus = _itemStatus;
                        //sevDet.Jbd_warrreplace
                        sevDet.Jbd_warrstartdt = VehicleObect.Irsm_warr_start_dt;
                        sevDet.Jbd_warr = _warranty;
                        sevDet.Jbd_warrstartdt = _warrStdate;
 

                        //sevDet.Jbd_waraamd_seq
                        //sevDet.Jbd_waraamd_dt
                        //sevDet.Jbd_waraamd_by
                        //sevDet.Jbd_usejob
                        //sevDet.Jbd_sevlocachr 

                        DataTable serviceDet = CHNLSVC.Sales.GetLastServiceDet(txtRegNo.Text.Trim(), txtEnginNo.Text.Trim(), txtChasseNo.Text.Trim());
                        if (ServiceTerm != 0)
                        {
                            sevDet.Jbd_ser_term = Convert.ToInt32(ServiceTerm); //free service
                        }
                        else
                        {
                            if (serviceDet.Rows.Count > 0)
                            {

                                sevDet.Jbd_ser_term = Convert.ToInt32(serviceDet.Rows[0]["IRSV_SEV_TERM"]) + 1; //payed sercice
                            }
                            else
                                sevDet.Jbd_ser_term = 1;
                        }

                        //sevDet.Jbd_sentwcn
                        //sevDet.Jbd_reqno
                        //sevDet.Jbd_reqline
                        //sevDet.Jbd_reqitmtp
                        //sevDet.Jbd_req_tp
                        //sevDet.Jbd_onloan
                        //sevDet.Jbd_needgatepass
                        //sevDet.Jbd_msnno
                        sevDet.Jbd_milage = Convert.ToDecimal(txtMilage.Text.Trim());
                        //sevDet.Jbd_mainreqno
                        //sevDet.Jbd_mainreqloc
                        //sevDet.Jbd_mainjobno
                        //sevDet.Jbd_mainitmwarr
                        //sevDet.Jbd_lastwarrstdt =//:TODO
                        //sevDet.Jbd_isinsurance :TODO
                        sevDet.Jbd_invc_no = VehicleObect.Irsm_invoice_no;

                        if (IsFreeService == 1 && IsService == 1)
                        {
                            sevDet.Jbd_is_service = 1;
                            sevDet.Jbd_is_updatesch = 1;
                        }
                        else
                        {
                            sevDet.Jbd_is_service = 0;
                            sevDet.Jbd_is_updatesch = 0;
                        }

                    }
                    _scvItemList.Add(sevDet);


                    //----------------Job StageLog Filling-------------------------------------------------------------------------
                    ServiceJobStageLog stgLog = new ServiceJobStageLog();
                    stgLog.Sjl_cre_by = BaseCls.GlbUserID;
                    stgLog.Sjl_cre_dt = dateTimePickerDate.Value.Date;
                    // stgLog.Sjl_jobno= sevHdr.Sjb_jobno;
                    stgLog.Sjl_jobstage = 2;
                    stgLog.Sjl_loc = BaseCls.GlbUserDefLoca;
                    //stgLog.Sjl_othdocno //blank
                    //stgLog.Sjl_reqno  //blank
                    //stgLog.Sjl_seqno //this is auto generated by a trigger
                    //-------------------Call save ----------------------------------------------------------------------------------
                   
             

                    if (ChkExternal.Checked)
                        VehicleObect = new InventorySerialMaster();
                    string jobNo;
                    string receiptNo;
                  //  Int32 eff = CHNLSVC.Sales.SaveVehicleJob(sevHdr, sevDet, Defect_List, stgLog, VehicleObect.Irsm_ser_id, IsService, IsUpdateShedule, _jobAuto, ChkExternal.Checked, out jobNo);
                    string _msg = "";
                    int eff = CHNLSVC.CustService.Save_Job(sevHdr, _scvItemList, Defect_List, null, null, null, null,null, null, BaseCls.GlbDefSubChannel, _itemType, _itemBrand, _warStus, _jobAuto, out _msg, out jobNo, out receiptNo);

                    Clear(false);
                    txtReqNo.Text = "";
                    if (eff > 0)
                    {
                        SystemInformationMessage(_msg, "Job Entry");
                      //  MessageBox.Show("Saved successfully!\nJob No " + jobNo, "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();
                        BaseCls.GlbReportCompCode = BaseCls.GlbUserComCode;
                        BaseCls.GlbReportName = "ServiceJobCardAut.rpt";
                        BaseCls.GlbReportDoc = jobNo;
                        _view.Show();
                        _view = null;

                      
                    }
                    else
                    {
                        MessageBox.Show("Not saved!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnSave.Enabled = true;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    btnSave.Enabled = true;
                    MessageBox.Show("Error occurred while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CHNLSVC.CloseChannel(); 
                    return;
                }
                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                btnSave.Enabled=true;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure want to clear?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Clear(false);
                    txtReqNo.Text = "";
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

        private void txtNIC_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNIC_Leave(object sender, EventArgs e)
        {
          
            if (!string.IsNullOrEmpty(txtNIC.Text))
            {
                Boolean _isValid = IsValidNIC(txtNIC.Text.Trim());
                if (_isValid == false)
                {
                    MessageBox.Show("Invalid NIC.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNIC.Text = "";
                    txtNIC.Focus();
                    return;
                }
                else
                {
                    if (_jobRecall == 0)
                    {
                        LoadCustomerDetailsByNIC();
                    }
                }
            }
        }

        private void txtModel_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCustTown_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtContPersn_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCustTown_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_town_Click(null, null);
        }

        private void txtCustTown_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F2)
            {
                btn_srch_town_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtCustTelephone.Focus();
            }
        }

        private void txtItmCd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {cmbServiceTypes.Focus ();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnItemSearch_Click_1(null, null);
            }
        }

        private void txtItmCd_DoubleClick(object sender, EventArgs e)
        {
            btnItemSearch_Click_1(null, null);
        }

        private void txtNIC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCustMobile.Focus();
            }
        }

        private void txtCustMobile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNIC.Focus();
            }
        }

        private void txtContPersn_KeyDown(object sender, KeyEventArgs e)
        { 
            if (e.KeyCode == Keys.Enter)
            {
                txtContNo.Focus();
            }
        }

        private void txtContNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtContLoc.Focus();
            }
        }

        private void txtContLoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtInfoPersn.Focus();
            }
        }

        private void txtInfoPersn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtInfoNo.Focus();
            }

        }

        private void txtInfoNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtInfoLoc.Focus();
            }

        }

        private void txtInfoLoc_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                btnSave.Focus();
            }

        }

        private void txtCustTelephone_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtContNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtContLoc_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtInfoPersn_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtInfoNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtInfoLoc_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEnginNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btn_srch_req_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            _jobStage = "2";
            CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearch);
            _CommonSearch.dtpTo.Value = DateTime.Now.Date;
            _CommonSearch.dtpFrom.Value = _CommonSearch.dtpTo.Value.Date.AddMonths(-1);
            DataTable _result = CHNLSVC.CommonSearch.GetServiceJobs(_CommonSearch.SearchParams, null, null, _CommonSearch.dtpFrom.Value.Date, _CommonSearch.dtpTo.Value.Date);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtReqNo;
            this.Cursor = Cursors.Default;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.ShowDialog();
            txtReqNo.Focus();
        }

        private void txtReqNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_srch_req_Click(null, null);
            }
        }

        private void txtReqNo_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtReqNo.Text)) return;
             
            LoadJob(BaseCls.GlbUserComCode, txtReqNo.Text, "0");
            Load_VehicleDetails(true);
        
        }

        private void txtReqNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtReqNo_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_req_Click(null, null);
        }

        private void btnNewCust_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                General.CustomerCreation _CusCre = new General.CustomerCreation();
                _CusCre._isFromOther = true;
                _CusCre.obj_TragetTextBox = txtCustCD;
                this.Cursor = Cursors.Default;
                _CusCre.ShowDialog();
                txtCustCD.Select();
            }
            catch (Exception ex)
            { txtCustCD.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
        }

        private void txtCustMobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToChar(e.KeyChar.ToString()) != '\b')
            {
                int isNumber = 0;
                e.Handled = !int.TryParse(e.KeyChar.ToString(), out isNumber);
            }
        }

        private void txtCustTelephone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToChar( e.KeyChar.ToString()) != '\b')
            {
                int isNumber = 0;
                e.Handled = !int.TryParse(e.KeyChar.ToString(), out isNumber);
            }
        }

        private void txtCustCD_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCustName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtContPersn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToChar(e.KeyChar.ToString()) != '\b')
            {
                e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
            }
        }

        private void txtInfoPersn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToChar(e.KeyChar.ToString()) != '\b')
            {
                e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
            }
        }

        private void txtCustName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txtMilage_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {
            if (label10.Text == "More >>")
            {
                pnlWarr.Visible = true;
                label10.Text = "<< More";
            }
            else
            {
                pnlWarr.Visible = false;
                label10.Text = "More >>";
            }
        }

        private void cmbServiceTypes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMilage.Focus();
            }
        }

        private void txtMilage_KeyDown(object sender, KeyEventArgs e)
        {
            
                if (e.KeyCode == Keys.Enter)
            {
                txtDef.Focus();
            }
        }

        private void btnSearch_CustCode_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearch _commonSearch = new CommonSearch.CommonSearch();
                _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_commonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                _commonSearch.dvResult.DataSource = _result;
                _commonSearch.BindUCtrlDDLData(_result);
                _commonSearch.obj_TragetTextBox = txtCustCD;
                _commonSearch.IsSearchEnter = true; //Add by Chamal 10/Aug/2013
                this.Cursor = Cursors.Default;
                _commonSearch.ShowDialog();
                txtCustCD.Select();
            }
            catch (Exception ex) { txtCustCD.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
        }

        private void txtCustCD_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCustCD.Text))
            {
                if (_jobRecall == 0)
                {
                    _email = string.Empty;
                    LoadCustomerDetailsByCustomer();
                }
            }
        }

        private void txtCustCD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearch_CustCode_Click(null, null);
            }
        }

        private void txtCustCD_DoubleClick(object sender, EventArgs e)
        {
            btnSearch_CustCode_Click(null, null);
        }

        private void lblWarrRemainingDays_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_NIC_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearch _commonSearch = new CommonSearch.CommonSearch();
                _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.NIC);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_commonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_NIC.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_NIC));
                _commonSearch.dvResult.DataSource = _result;
                _commonSearch.BindUCtrlDDLData(_result);
                _commonSearch.obj_TragetTextBox = txtNIC;
                _commonSearch.IsSearchEnter = true;
                this.Cursor = Cursors.Default;
                _commonSearch.ShowDialog();
                txtNIC.Select();
            }
            catch (Exception ex)
            { txtNIC.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
        }

        private void btnSearch_Mobile_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearch _commonSearch = new CommonSearch.CommonSearch();
                _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Mobile);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_commonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_MOB.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_MOB));
                _commonSearch.dvResult.DataSource = _result;
                _commonSearch.BindUCtrlDDLData(_result);
                _commonSearch.obj_TragetTextBox = txtCustMobile;
                _commonSearch.IsSearchEnter = true;
                this.Cursor = Cursors.Default;
                _commonSearch.ShowDialog();
                txtCustMobile.Select();
                if (_commonSearch.GlbSelectData == null) return;
                string[] args = _commonSearch.GlbSelectData.Split('|');
                string _cuscode = args[4];
                if (string.IsNullOrEmpty(txtCustCD.Text) || txtCustCD.Text.Trim() == "CASH") txtCustCD.Text = _cuscode;
                else if (txtCustCD.Text.Trim() != _cuscode && txtCustCD.Text.Trim() != "CASH")
                {
                    DialogResult _res = MessageBox.Show("Currently selected customer code " + txtCustCD.Text + " is differ which selected (" + _cuscode + ") from here. Do you need to change current customer code from selected customer", "Invoice", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (_res == System.Windows.Forms.DialogResult.Yes)
                    {
                        txtCustCD.Text = _cuscode;
                        txtCustCD.Focus();
                    }
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
        }

        private void lblBuyerCustAdd2_Click(object sender, EventArgs e)
        {

        }

        private void pnlWarr_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblBuyerCustMobi_Click(object sender, EventArgs e)
        {

        }

        private void lblNIC_Click(object sender, EventArgs e)
        {

        }

        private void lblCustDO_no_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblPhone_Click(object sender, EventArgs e)
        {

        }

        private void txtCustAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (optJob.Checked == true)
            {
                if (txtReqNo.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Select job no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtReqNo.Focus();
                    return;
                }
                if (_chkJobStage == 8)
                {
                    MessageBox.Show("Job  already invoiced, unable to cancel!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (_chkJobStage == 12)
                {
                    MessageBox.Show("Job  already Cancelled!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                 
                    if (_scvItemList != null && _scvItemList.Count > 0)
                    {
                        if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10803))
                        {
                            MessageBox.Show("You don't have the permission for job cancel function.\nPermission Code :- 10803", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }
                        else
                        { 
                            //Cancel code
                            Int32 _resultCNCL = -1;
                            String _msg = string.Empty;
                            _resultCNCL = CHNLSVC.CustService.ServiceApprove(txtReqNo.Text, 1, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf, BaseCls.GlbUserID, BaseCls.GlbUserSessionID, "Cancel", txtJobRmk.Text, (Int32)CommonEnum.Job_Cancel, out _msg, "", "");
                            if (_resultCNCL == -1)
                            {
                                SystemWarnningMessage(_msg, "Job Cancellation");
                                return;
                            }
                            else
                            {
                                MessageBox.Show("Sucessfully Cancelled", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                
                            }
                            Clear(false);
                            txtReqNo.Text = "";
                        }
                    }
                
            }
        }

        private void btnHist_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEnginNo.Text))
            {
                if (txtEnginNo.Text.ToUpper().ToString() != "N/A")
                {
                    pnlMain.Enabled = false;
                    lblDefectHistyHeader.Text = ":: Job History :: [ Item :- " + txtItmCd.Text + " Serial :-" + txtEnginNo.Text + " ]";
                    ucDefectHistory1.Serial = txtEnginNo.Text;
                    ucDefectHistory1.Item = txtItmCd.Text;
                    ucDefectHistory1.loadData();
                    pnlHistory.Width = 778;
                    pnlHistory.Height = 504;
                    pnlHistory.Show();
                    dateTimePickerDate.Focus();
                }
            }
        }

        private void btnSerDet_Click(object sender, EventArgs e)
        {
            try
            {
                lblLastServNoOfAttempt.Text = string.Empty;
                lblLastSevJobNo.Text = string.Empty;
                lblLastSevJobDt.Text = string.Empty;
                lblLastSevMilage.Text = string.Empty;

                if (txtEnginNo.Text.Trim() == string.Empty && txtChasseNo.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Please select vehicle before view last service", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //load the last servic.
                DataTable serviceDet = CHNLSVC.Sales.GetLastServiceDet(txtRegNo.Text.Trim(), txtEnginNo.Text.Trim(), txtChasseNo.Text.Trim());
                if (serviceDet.Rows.Count == 0)
                {
                    MessageBox.Show("No free services history for selected registration #", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (serviceDet != null && serviceDet.Rows.Count > 0)
                {
                    string term = serviceDet.Rows[0]["IRSV_SEV_TERM"].ToString();//irsv_sev_term
                    lblLastServNoOfAttempt.Text = term;

                    string JobNo = serviceDet.Rows[0]["IRSV_JOB_NO"].ToString();//irsv_job_no, 
                    lblLastSevJobNo.Text = JobNo;

                    string JobDate = Convert.ToDateTime(serviceDet.Rows[0]["IRSV_JOB_DT"]).Date.ToShortDateString();//irsv_job_dt,
                    lblLastSevJobDt.Text = JobDate;

                    string milage = serviceDet.Rows[0]["IRSV_ACT_READ"].ToString();//irsv_act_read
                    lblLastSevMilage.Text = milage + " Km";

                    if (Convert.ToInt16(serviceDet.Rows[0]["IRSV_IS_FREE"]) == 1)// irsv_is_free
                    {
                        lblLastSevPayOrFree.Text = "FREE SERVICE";
                        Color myColor = Color.Green;
                        lblLastSevPayOrFree.ForeColor = myColor;
                    }


                    else
                    {
                        lblLastSevPayOrFree.Text = "CHARGE SERVICE";
                        Color myColor = Color.Red;
                        lblLastSevPayOrFree.ForeColor = myColor;

                    }
                    grvServiceHistory.AutoGenerateColumns = false;
                    grvServiceHistory.DataSource = serviceDet;
                    //divNextService.Style.Add("background-color", " #FFCCCC");// background-color: #FFFFCC
                }

                pnlPopupLastDefect.Visible = true;
                pnlMain.Enabled = false;
                toolStrip1.Enabled = false;
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

        private void btnJobPrint_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtReqNo.Text))
            {
                Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();
                BaseCls.GlbReportCompCode = BaseCls.GlbUserComCode;
                BaseCls.GlbReportName = "ServiceJobCardAut.rpt";
                BaseCls.GlbReportDoc = txtReqNo.Text;
                _view.Show();
                _view = null;
            }
        }

        private void btnCloseHistory_Click(object sender, EventArgs e)
        {

            pnlHistory.Hide();
            pnlMain.Enabled = true;
        }

        private void txtCustCD_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void txtCustName_MouseClick(object sender, MouseEventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();

            toolTip1.ShowAlways = true;

            toolTip1.SetToolTip(txtCustName, txtCustName.Text ); 
        }

        private void txtCustAddress_MouseClick(object sender, MouseEventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(txtCustAddress, null);
            toolTip1.ShowAlways = true;

            toolTip1.SetToolTip(txtCustAddress, txtCustAddress.Text);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtReqNo.Text))
            {
                Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();
                BaseCls.GlbReportCompCode = BaseCls.GlbUserComCode;
                BaseCls.GlbReportName = "ServiceJobCardAut.rpt";
                BaseCls.GlbReportDoc = txtReqNo.Text;
                _view.Show();
                _view = null;
            }
        }

        private void txtCustAddress2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCustTown_MouseClick(object sender, MouseEventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();

            toolTip1.ShowAlways = true;

            toolTip1.SetToolTip(txtCustTown, txtCustTown.Text);
        }

        private void txtCustAddress2_MouseClick(object sender, MouseEventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();

            toolTip1.ShowAlways = true;

            toolTip1.SetToolTip(txtCustAddress2, txtCustAddress2.Text);
        }

       

         

        }

   
}
