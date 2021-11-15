using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using FF.Interfaces;

namespace FF.WindowsERPClient.HP
{
    public partial class AccountCreationRestriction : Base
    {
        //sp_save_hpr_sys_para   =update
        //sp_getAllValidAccRest  =NEW
        DataTable select_PC_List = new DataTable();
        DataTable select_PC_List2 = new DataTable();
        private string company2;
        public string Company2
        {
            get { return company2; }
            set { company2 = value; }
        }

        private string company;
        public string Company
        {
            get { return company; }
            set { company = value; }
        }
        private Int32 restrSEQ;
        public Int32 RestrSEQ
        {
            get { return restrSEQ; }
            set { restrSEQ = value; }
        }

        private List<AccountRestriction> accRestrList;
        public List<AccountRestriction> AccRestrList
        {
            get { return accRestrList; }
            set { accRestrList = value; }
        }

        private List<Hpr_SysParameter> hpr_ParaList;
        public List<Hpr_SysParameter> Hpr_ParaList
        {
            get { return hpr_ParaList; }
            set { hpr_ParaList = value; }
        }

        private List<Hpr_SysParameter> show_Hpr_ParaList;
        public List<Hpr_SysParameter> Show_Hpr_ParaList
        {
            get { return show_Hpr_ParaList; }
            set { show_Hpr_ParaList = value; }
        }

        private List<string> clonePcList;
        public List<string> ClonePcList
        {
            get { return clonePcList; }
            set { clonePcList = value; }
        }
      
        public AccountCreationRestriction()
        {
            InitializeComponent();
            divNoOfMonths.Visible = false;
            ucProfitCenterSearch1.Company = BaseCls.GlbUserComCode;
            ucProfitCenterSearch1.ProfitCenter = BaseCls.GlbUserDefProf;
            ucProfitCenterSearch2.Company = BaseCls.GlbUserComCode;
            ucProfitCenterSearch2.ProfitCenter = BaseCls.GlbUserDefProf;
            AccRestrList = new List<AccountRestriction>();
            RestrSEQ = 0;
            DataTable dt = new DataTable();
            grvResctrict.DataSource = dt;
           
          //  clearScreen(); //TODO:
           
            //********
            Hpr_ParaList = new List<Hpr_SysParameter>();
            Show_Hpr_ParaList = new List<Hpr_SysParameter>();
            ClonePcList = new List<string>();
          //  grvClonePc.DataSource = ClonePcList;  //TODO:
      
            DataTable dt2 = new DataTable();
          //  grvParameters.DataSource = dt2;  //TODO:
          //  grvParameters.DataBind();  //TODO:

            toolStrip1.Show();
            toolStrip2.Hide();
            showPopUpPanel(null);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            AccountCreationRestriction formnew = new AccountCreationRestriction();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {   
            //if (txtApprNoOfAcc.Text == "" || txtSalesVal.Text == "" || txtSalesVal.Text == "")
            //{
            //    MessageBox.Show("Please enter reqired details!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            if(rdoAnual.Checked==true)
            {
                if (Convert.ToDateTime(txtFromDate.Value).Date > Convert.ToDateTime(txtToDate.Value).Date)
                {
                    MessageBox.Show("From date should be less than to date!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
          
            try
            {
                if (txtApprNoOfAcc.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter number of accounts", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtSalesVal.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter allowing sale amount", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

               Int32 AC=  Convert.ToInt32(txtApprNoOfAcc.Text.Trim());
                Decimal SAL= Convert.ToDecimal(txtSalesVal.Text.Trim());
                if (AC <0 ||SAL<0)
                {
                    MessageBox.Show("Please enter valid details!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Please enter valid details", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(rdoMonthly.Checked==true)
            {               
                try
                {
                    if (txtNoOfMonths.Text.Trim()=="")
                    {
                        MessageBox.Show("Please enter No of restrict months", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    Convert.ToDecimal(txtNoOfMonths.Text.Trim());
                    Decimal MON = Convert.ToDecimal(txtNoOfMonths.Text.Trim());
                    if (MON < 0 )
                    {
                        MessageBox.Show("Please enter valid details!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Please enter valid details!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            try
            {
                Convert.ToInt64(txtApprNoOfAcc.Text.Trim());
            }
            catch (Exception ex)
            {
                txtApprNoOfAcc.Focus();
               // this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Exceeds max length.");
                MessageBox.Show("Larger number for number of accounts!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                Convert.ToDecimal(txtSalesVal.Text.Trim());
            }
            catch (Exception ex)
            {
                txtSalesVal.Focus();
               // this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Exceeds max length.");
                MessageBox.Show("Sales value exceeds max length.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //--------------------------------------------------------------
            if (grvResctrict.Rows.Count > 0 && grvProfCents.Rows.Count > 0)
            {
                List<string> PC_list = GetSelectedPCList();
                foreach (DataGridViewRow dgvr in grvResctrict.Rows)
                {                   
                    if (PC_list.Count >0)
                    {
                       foreach (string pc in PC_list)
                       {
                           if (pc == dgvr.Cells["Hrs_pc"].Value.ToString() && txtFromDate.Value.Date == Convert.ToDateTime(dgvr.Cells["Hrs_from_dt"].Value.ToString()).Date)
                           {
                               MessageBox.Show("Cannot enter duplicate rows.","",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                               return;
                           }
                       }
                    }
                }
            }
            //--------------------------------------------------------------
            if (grvProfCents.Rows.Count > 0)
            {
                if (rdoAnual.Checked)
                {
                    AddToRestrictionList();
                }
                else if (rdoMonthly.Checked)
                {
                    Int32 NoOfMonths = 0;
                    try
                    {
                        NoOfMonths = Convert.ToInt32(txtNoOfMonths.Text.Trim());
                    }
                    catch (Exception ex)
                    {
                       // this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please valid # of months!");
                        MessageBox.Show("Please enter valid # of months!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                        return;                        
                    }
                    DateTime orgFromDt = Convert.ToDateTime(txtFromDate.Text.Trim());
                    for (int i = 0; i < NoOfMonths; i++)
                    {
                        AddToRestrictionList();
                        txtFromDate.Text = (Convert.ToDateTime(txtFromDate.Text.Trim()).AddMonths(1)).ToString();
                    }
                    txtFromDate.Text = orgFromDt.ToShortDateString();
                }
                grvResctrict.DataSource = null;
                grvResctrict.AutoGenerateColumns = false;
                grvResctrict.DataSource = AccRestrList;
                if(grvResctrict.Rows.Count>0)
                {
                    foreach (DataGridViewRow dgvr in grvResctrict.Rows)
                    {
                        if (dgvr.Cells["Hrs_tp"].Value.ToString() == "2")
                        {
                            dgvr.Cells["show_Hrs_tp"].Value= "Anually";
                        }
                        else if (dgvr.Cells["Hrs_tp"].Value.ToString() == "1")
                        {
                            dgvr.Cells["show_Hrs_tp"].Value = "Monthly";
                        }
                    }
                }
                
                //grvProfCents.DataSource = null;
                this.btnClearPc_Click(null, null);
             //   clearAddScreen(); TODO:

            }
            else
            {
                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please select Profit centers!");
                MessageBox.Show("Please select Profit centers!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
        private List<string> GetSelectedPCList()
        {
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvProfCents.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells[1].Value.ToString());
                }

            }
            return list;
        }
        private void AddToRestrictionList()
        {
            List<string> PC_list = GetSelectedPCList();
            if (PC_list.Count < 1)
            {
                MessageBox.Show("Please add and select profit centers.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            foreach (string prof in PC_list)
            {             
            //}
            //foreach (DataGridView gvr in grvProfCents.Rows)
            //{               
                if (AccRestrList == null)
                {
                    AccRestrList = new List<AccountRestriction>();
                }
               // CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chekPc");
                //if (chkSelect.Checked)
                //{
                string profCenter = prof;//gvr.Cells[0].Text.Trim();

                    AccountRestriction AccRestr = new AccountRestriction();
                    AccRestr.Hrs_cre_by = BaseCls.GlbUserID; ;
                    AccRestr.Hrs_cre_dt = CHNLSVC.Security.GetServerDateTime();// DateTime.Now.Date;

                    AccRestr.Hrs_no_ac = Convert.ToInt64(txtApprNoOfAcc.Text.Trim());
                    AccRestr.Hrs_pc = profCenter;
                    AccRestr.Hrs_seq = RestrSEQ++; //this is generated automatically by DB Sequence when inserting
                    AccRestr.Hrs_tot_val = Convert.ToDecimal(txtSalesVal.Text.Trim());
                    try
                    {
                        AccRestr.Hrs_from_dt = Convert.ToDateTime(txtFromDate.Text.Trim());
                        if (rdoMonthly.Checked == true)
                        {
                            AccRestr.Hrs_to_dt = Convert.ToDateTime(txtToDate.Text.Trim());
                        } 
                        //if (txtToDate.Text.Trim() != "")
                        //{
                        //    AccRestr.Hrs_to_dt = Convert.ToDateTime(txtToDate.Text.Trim());
                        //}
                    }
                    catch (Exception ex)
                    {
                       // this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please enter valid Date!");
                        MessageBox.Show("Please enter valid Date!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (rdoAnual.Checked)
                    {
                        AccRestr.Hrs_tp = 2;

                        if (txtToDate.Text.Trim() == "")
                        {
                            //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please enter 'To Date'.");
                            MessageBox.Show("Please enter 'To Date'", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        AccRestr.Hrs_to_dt = Convert.ToDateTime(txtToDate.Text.Trim());
                    }
                    else
                    {
                        AccRestr.Hrs_to_dt = AccRestr.Hrs_from_dt.AddMonths(1);
                        AccRestr.Hrs_to_dt = AccRestr.Hrs_to_dt.AddDays(-1);
                        AccRestr.Hrs_tp = 1;

                    }
                    AccRestrList.Add(AccRestr);
                //}
            }
        }

        private void btnAddPc_Click(object sender, EventArgs e)
        {
            string oldCom = Company;
            Company = ucProfitCenterSearch1.Company;
            if (oldCom != Company)
            {
                this.btnClearPc_Click(null, null);
            }
            if (ucProfitCenterSearch1.Company == "")
            {
                return;
            }
            string com = ucProfitCenterSearch1.Company;
            string chanel = ucProfitCenterSearch1.Channel;
            string subChanel = ucProfitCenterSearch1.SubChannel;
            string area = ucProfitCenterSearch1.Area;
            string region = ucProfitCenterSearch1.Regien;
            string zone = ucProfitCenterSearch1.Zone;
            string pc = ucProfitCenterSearch1.ProfitCenter;

            DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy(com.ToUpper(), chanel.ToUpper(), subChanel.ToUpper(), area.ToUpper(), region.ToUpper(), zone.ToUpper(), pc.ToUpper());
           // DataTable dt_BIND = CHNLSVC.Sales.GetPC_from_Hierachy(com.ToUpper(), chanel.ToUpper(), subChanel.ToUpper(), area.ToUpper(), region.ToUpper(), zone.ToUpper(), pc.ToUpper());
            List<string> addedList = GetSelectedPCList();
            //PROFIT_CENTER
            DataTable dt_BIND2 = new DataTable();
            foreach(DataRow dr in dt.Rows)
            {
                var _duplicate = from _dup in addedList
                                 where _dup == dr["PROFIT_CENTER"].ToString()
                                 select _dup;
                if (_duplicate.Count() != 0)
                {
                    MessageBox.Show("Can't add existing profit centers!","",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }  
            }
              

            select_PC_List.Merge(dt);
            grvProfCents.DataSource = null;
            grvProfCents.AutoGenerateColumns = false;
            grvProfCents.DataSource = select_PC_List;
            this.btnAllPc_Click(sender, e);
                       
        }

        private void btnAllPc_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvProfCents.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
                grvProfCents.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnNonPc_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvProfCents.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = false;
                }
                grvProfCents.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnClearPc_Click(object sender, EventArgs e)
        {
            // DataTable emptyDt = new DataTable();
            select_PC_List = new DataTable();
            grvProfCents.DataSource = null;
            grvProfCents.AutoGenerateColumns = false;
            grvProfCents.DataSource = select_PC_List;
            //grvProfCents.DataSource = emptyDt;
        }

        private void rdoMonthly_CheckedChanged(object sender, EventArgs e)
        {
            //clearAddScreen(); TODO:
            divToDate.Visible = false;
            divNoOfMonths.Visible = true;
        }

        private void rdoAnual_CheckedChanged(object sender, EventArgs e)
        {
            //clearAddScreen(); TODO:
            divToDate.Visible = true;
            divNoOfMonths.Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "ACRES") == false)
            {
                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Permission Denied!");
                MessageBox.Show("Permission Denied!\n( Advice: Reqired permission code :ACRES)", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);              
                return;
            }
            if (MessageBox.Show("Are you sure to Save?", "Confirm Save", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            if (AccRestrList.Count == 0)
            {
                MessageBox.Show("Please add restrictions to save!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Int32 effect = 0;
            try
            {
                effect = CHNLSVC.Sales.SaveAccRestriction(AccRestrList);
            }
            catch(Exception ex){
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel(); 
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            if (effect > 0)
            {
               // clearScreen(); TODO:

               // this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully Saved!");
               // string Msg = "<script>alert('Successfully Saved!' );</script>";
                //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                MessageBox.Show("Successfully Saved!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.btnClear_Click(null, null);
            }
            else
            {
               // clearScreen(); TODO:

               // this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Could not save!");
                MessageBox.Show("Could not save!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.btnClear_Click(null, null);
                return;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AccRestrList.Clear();
            grvResctrict.DataSource = null;
            grvResctrict.AutoGenerateColumns = false;
            grvResctrict.DataSource = AccRestrList;
           
        }

        private void btnAddPc2_Click(object sender, EventArgs e)
        {
            string oldCom = Company2;
            Company2 = ucProfitCenterSearch2.Company;
            if (oldCom != Company2)
            {
                this.btnClearPc2_Click(null, null);
            }
            if (ucProfitCenterSearch2.Company == "")
            {
                MessageBox.Show("Enter company code");
                return;
            }
            string com = ucProfitCenterSearch2.Company;
            string chanel = ucProfitCenterSearch2.Channel;
            string subChanel = ucProfitCenterSearch2.SubChannel;
            string area = ucProfitCenterSearch2.Area;
            string region = ucProfitCenterSearch2.Regien;
            string zone = ucProfitCenterSearch2.Zone;
            string pc = ucProfitCenterSearch2.ProfitCenter;

            DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy(com.ToUpper(), chanel.ToUpper(), subChanel.ToUpper(), area.ToUpper(), region.ToUpper(), zone.ToUpper(), pc.ToUpper());
            List<string> addedList = PARA_GetSelectedPCList();
            //PROFIT_CENTER
            DataTable dt_BIND2 = new DataTable();
            foreach (DataRow dr in dt.Rows)
            {
                var _duplicate = from _dup in addedList
                                 where _dup == dr["PROFIT_CENTER"].ToString()
                                 select _dup;
                if (_duplicate.Count() != 0)
                {
                    MessageBox.Show("Can't add existing profit centers!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            
            select_PC_List2.Merge(dt);
            grvProfCents2.DataSource = null;
            grvProfCents2.AutoGenerateColumns = false;
            grvProfCents2.DataSource = select_PC_List2;
            this.btnAllPc2_Click(sender, e);
        }

        private void btnAllPc2_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvProfCents2.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
                grvProfCents2.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnNonPc2_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvProfCents2.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = false;
                }
                grvProfCents2.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnClearPc2_Click(object sender, EventArgs e)
        {
            // DataTable emptyDt = new DataTable();
            select_PC_List2 = new DataTable();
            grvProfCents2.DataSource = null;
            grvProfCents2.AutoGenerateColumns = false;
            grvProfCents2.DataSource = select_PC_List2;
            //grvProfCents.DataSource = emptyDt;
        }
             
        private void grvResctrict_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Int32 rowIndex = e.RowIndex;//lblSEQno
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                string SEQ_ = grvResctrict.Rows[rowIndex].Cells["Hrs_seq"].Value.ToString();
                Double SEQ = Convert.ToDouble(SEQ_);
                AccRestrList.RemoveAll(x => x.Hrs_seq == SEQ);

                grvResctrict.DataSource = null;
                grvResctrict.AutoGenerateColumns = false;
                grvResctrict.DataSource = AccRestrList;
            }  
        }
   //----------------------------------------------------------------------------------------------------------------------------------
        private List<string> PARA_GetSelectedPCList()
        {
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvProfCents2.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells[1].Value.ToString());
                }

            }
            return list;
        }
        private void btnAddPara_Click(object sender, EventArgs e)
        {
            if (txtFromDt_pty.Text.Trim() == "" || txtToDt_pty.Text.Trim() == "" || txtValue_pty.Text.Trim() == "" || txtParaCode.Text=="")
            {
                MessageBox.Show("Please fill all the details to enter!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Convert.ToDateTime(txtFromDt_pty.Value).Date > Convert.ToDateTime(txtToDt_pty.Value).Date)
            {
                MessageBox.Show("From date should be less than to date!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "HPPRM") == false)
            {
              //  this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Permission Denied!");
                MessageBox.Show("Permission Denied!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
             
            //if (grvProfCents2.Rows.Count > 0) //TODO
            if (PARA_GetSelectedPCList().Count>0)
            {
                if (Show_Hpr_ParaList.Count < 1)
                {
                    Show_Hpr_ParaList = CHNLSVC.Sales.GetAll_hpr_Para(txtParaCode.Text.Trim().ToUpper(), "PC", string.Empty);
                    //grvParameters.DataSource = null;
                    //grvParameters.AutoGenerateColumns = false;
                    //grvParameters.DataSource = Show_Hpr_ParaList;    
                    bindParaGrid(Show_Hpr_ParaList);        
                }
                AddTo_ParaList(); //Add new Parameter to PCs
                 txtValue_pty.Text= "" ;
                 txtParaCode.Text = "";
                 txtFromDt_pty.Value = DateTime.Now.Date;
                 txtToDt_pty.Value = DateTime.Now.Date;
                this.btnClearPc2_Click(null, null);
            }
            else
            {              
                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please add Profit Centers!");
                MessageBox.Show("Please add Profit Centers!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }            
        }

        private void AddTo_ParaList()
        {            
            string codeDescript = string.Empty;
            DataTable dt = CHNLSVC.Sales.Get_get_hpr_para_types(txtParaCode.Text.Trim().ToUpper());
            if (dt == null)
            {
              //  this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Invalid Code!");
                MessageBox.Show("Invalid Code!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtParaCode.Focus();
                return;
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    codeDescript = dt.Rows[0]["pt_desc"].ToString();
                   // lblCodeDesc.Text = codeDescript;
                }
                else
                {
                   // this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Invalid Code!");
                    MessageBox.Show("Invalid Code!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtParaCode.Focus();
                    return;
                }
            }
            if (Hpr_ParaList == null)
            {
                Hpr_ParaList = new List<Hpr_SysParameter>();
            }
            if (Show_Hpr_ParaList == null)
            {
                Show_Hpr_ParaList = new List<Hpr_SysParameter>();
            }
            //Int32 Sseq = -100;
             List<string> SELECT_PC_LIST= PARA_GetSelectedPCList();
            foreach (string pc_ in SELECT_PC_LIST)
            {
                var _duplicate = from _dup in Hpr_ParaList
                                 where _dup.Hsy_cd == txtParaCode.Text.Trim().ToUpper() && _dup.Hsy_pty_cd == pc_
                                 select _dup;
                if (_duplicate.Count() > 0)
                {
                    //string Msg = "<script>alert('already added!');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);        
                    MessageBox.Show("Cannot enter duplicate records!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            foreach (string pc_ in SELECT_PC_LIST)
            {                      
                string profCenter = pc_;

                Hpr_SysParameter hpr_para = new Hpr_SysParameter();
                hpr_para.Hsy_seq = -1;
                hpr_para.Hsy_cd = txtParaCode.Text.Trim().ToUpper();
                hpr_para.Hsy_cre_by =BaseCls.GlbUserID;
                hpr_para.Hsy_cre_dt = DateTime.Now.Date;
                hpr_para.Hsy_desc = codeDescript;
                hpr_para.Hsy_pty_cd = profCenter;
                hpr_para.Hsy_pty_tp = "PC";
                hpr_para.Hsy_val = Convert.ToDecimal(txtValue_pty.Text.Trim());
                try
                {
                    hpr_para.Hsy_from_dt = Convert.ToDateTime(txtFromDt_pty.Text.Trim());
                    hpr_para.Hsy_to_dt = Convert.ToDateTime(txtToDt_pty.Text.Trim());
                }
                catch (Exception ex)
                {
                    // this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please enter valid dates!");
                    return;
                }
                //-------------------------------------------
                var _duplicate = from _dup in Hpr_ParaList
                                    where _dup.Hsy_cd == hpr_para.Hsy_cd && _dup.Hsy_pty_cd == profCenter
                                    select _dup;
                if (_duplicate.Count() > 0)
                {
                    //string Msg = "<script>alert('already added!');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);        
                    //MessageBox.Show("Already added to the list!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);                   
                }
                else
                {
                    Hpr_ParaList.Add(hpr_para);
                    Show_Hpr_ParaList.Add(hpr_para);
                }
                //-------------------------------------------  
              //  bindParaGrid(Show_Hpr_ParaList);   
                bindParaGrid(Hpr_ParaList);                                   
            }
        }
        private void bindParaGrid(List<Hpr_SysParameter> bindList)
        {
            grvParameters.DataSource = null;
            grvParameters.AutoGenerateColumns = false;
            BindingSource _source = new BindingSource();
            _source.DataSource = bindList;//Show_Hpr_ParaList;
            grvParameters.DataSource = _source;
        }
        private void grvParameters_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Int32 rowIndex = e.RowIndex;
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {               
               // Label SEQ = (Label)grvParameters.Rows[rowIndex].FindControl("lblParaSeq");
                string SEQ_ = grvParameters.Rows[rowIndex].Cells["Hsy_seq"].Value.ToString();

                string para_CD = grvParameters.Rows[rowIndex].Cells["Hsy_cd"].Value.ToString();//grvParameters.Rows[rowIndex].Cells[2].Text;
                string pc = grvParameters.Rows[rowIndex].Cells["Hsy_pty_cd"].Value.ToString();//grvParameters.Rows[rowIndex].Cells[4].Text;
                if (SEQ_ == "-1")
                {
                    Show_Hpr_ParaList.RemoveAll(x => x.Hsy_cd == para_CD && x.Hsy_pty_cd == pc);
                    Hpr_ParaList.RemoveAll(x => x.Hsy_cd == para_CD && x.Hsy_pty_cd == pc);
                }
                else
                {
                    //string Msg = "<script>alert('Cannot delete existing records in the database!');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                    MessageBox.Show("Cannot delete existing records in the database!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                //grvParameters.DataSource = Show_Hpr_ParaList;

               // bindParaGrid(Show_Hpr_ParaList); Hpr_ParaList
                bindParaGrid(Hpr_ParaList); 
            }
        }

        private void txtParaCode_Leave(object sender, EventArgs e)
        {            
        //    Hpr_ParaList = new List<Hpr_SysParameter>();
        //    Show_Hpr_ParaList = new List<Hpr_SysParameter>();

        //    Show_Hpr_ParaList = CHNLSVC.Sales.GetAll_hpr_Para(txtParaCode.Text.Trim().ToUpper(), "PC", string.Empty);
           
        //   // grvParameters.DataSource = Show_Hpr_ParaList;
        //   // grvParameters.DataBind();
        //    bindParaGrid(Show_Hpr_ParaList);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage2)
            {
                Show_Hpr_ParaList = new List<Hpr_SysParameter>();
                Hpr_ParaList = new List<Hpr_SysParameter>();
               // Show_Hpr_ParaList = CHNLSVC.Sales.GetAll_hpr_Para(txtParaCode.Text.Trim().ToUpper(), "PC", string.Empty);
                bindParaGrid(Hpr_ParaList);

                toolStrip1.Hide();
                toolStrip2.Show();
            }
            else if (tabControl1.SelectedTab == tabPage1)
            {
                toolStrip1.Show();
                toolStrip2.Hide();
            }
         
        }

        #region Searchin
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {

                case CommonUIDefiniton.SearchUserControlType.HpParaTp:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion
        private void ImgCodeSearch_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpParaTp);
            DataTable _result = CHNLSVC.CommonSearch.Get_hp_parameterTypes(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtParaCode;
            _CommonSearch.ShowDialog();
            txtParaCode.Focus();                      
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AccountCreationRestriction formnew = new AccountCreationRestriction();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();
        }

        private void btnSavePara_Click(object sender, EventArgs e)
        {
            if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "HPPRM") == false)
            {
                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Permission Denied!");
                MessageBox.Show("Permission Denied!\n( Advice: Reqired permission code :HPPRM)", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               // MessageBox.Show("Permission Denied!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //---------------------------------------------------------
            if (Hpr_ParaList.Count == 0)
            {
               // this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Add new definitions to the list!");
                MessageBox.Show("Add new definitions to the list!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Are you sure to Save?", "Confirm Save", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            Int32 effect = 0;
            try
            {
                effect = CHNLSVC.Sales.Save_hpr_sys_para(Hpr_ParaList);
            }
            catch (Exception EX)
            {
                //string Msg = "<script>alert('System error!');</script>";
                //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                MessageBox.Show("System error!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (effect > 0)
            {
                //clearScreen_para(); //TODO:
                //string Msg = "<script>alert('Successfully saved!');</script>";
                //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully saved!");
                MessageBox.Show("Successfully saved!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.btnClear_Click(null, null);
            }
            else
            {
                //string Msg = "<script>alert('Failed to save. Try again!');</script>";
                //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Failed to save!");
                MessageBox.Show("Failed to save!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ImgVeiwParamCd_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpParaTp);
            DataTable _result = CHNLSVC.CommonSearch.Get_hp_parameterTypes(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtParaCodePopUp;
            _CommonSearch.ShowDialog();
            txtParaCodePopUp.Focus();  
        }

        private void btnVeiwPopUp_Click(object sender, EventArgs e)
        {
            string code = txtParaCodePopUp.Text.Trim().ToUpper();

            if (code == "" || code == string.Empty)
            {
                code = " ";
            }
            List<Hpr_SysParameter> veiw_list = new List<Hpr_SysParameter>();
            if (checkAllCodesPopUp.Checked == true)
            {
                veiw_list = CHNLSVC.Sales.GetAll_hpr_Para(string.Empty, "PC", txtParaPc_PopUp.Text.Trim().ToUpper());
            }
            else
            {
                veiw_list = CHNLSVC.Sales.GetAll_hpr_Para(code, "PC", txtParaPc_PopUp.Text.Trim().ToUpper());
            }
            grvVeiwParaPopUp.DataSource = null;
            grvVeiwParaPopUp.AutoGenerateColumns = false;
            grvVeiwParaPopUp.DataSource = veiw_list;
            
        }
        private void showPopUpPanel(Panel panel)
        {
            panel_viewPara.Visible = false;
            panel_ClonePara.Visible = false;
            panel_viewAcRestr.Visible = false;
            //-----------------------------------
            if (panel==null)
            {
                return;
            }
            panel.Visible = true;
            panel.Location = new Point(95, 1);
            
        }

        private void btnViewPara_Click(object sender, EventArgs e)
        {
            txtParaCodePopUp.Text = "";
            txtParaPc_PopUp.Text = "";
            checkAllCodesPopUp.Text = "";

            List<Hpr_SysParameter> veiw_list = new List<Hpr_SysParameter>();
            grvVeiwParaPopUp.DataSource = null;
            grvVeiwParaPopUp.AutoGenerateColumns = false;
            grvVeiwParaPopUp.DataSource = veiw_list;
          
            panel_viewPara.Size = new Size(740, 272);
            
            showPopUpPanel(panel_viewPara);
            
        }

        private void btnNewComClose_Click(object sender, EventArgs e)
        {
            panel_viewPara.Visible = false;
        }

        private void ImgBtnCloneCode_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpParaTp);
            DataTable _result = CHNLSVC.CommonSearch.Get_hp_parameterTypes(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCloneParaCode;
            _CommonSearch.ShowDialog();
            txtCloneParaCode.Focus(); 
        }

        private void ImgBtnCloneAdd_Click(object sender, EventArgs e)
        {
            List<string> tem = (from _res in ClonePcList
                                where _res == txtCloneAddPc.Text
                                select _res).ToList<string>();
            if (tem != null && tem.Count > 0)
            {
                MessageBox.Show("Profit Center " + txtCloneAddPc.Text + " Already in the list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //---------------------------------
            ClonePcList.Add(txtCloneAddPc.Text.Trim().ToUpper());
                        
            var pcs = (from _res in ClonePcList
                       select new { PC = _res });

            BindingSource source = new BindingSource();
            source.DataSource = pcs.ToList();
            grvClonePc.AutoGenerateColumns = false;
            grvClonePc.DataSource = source;
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel_ClonePara.Visible = false;
        }

        private void btnProcessClone_Click(object sender, EventArgs e)
        {            
            if (ClonePcList.Count > 0)
            {
                if (MessageBox.Show("Are you sure to clone?", "Confirm clone", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
                Int32 eff = 0;

                eff = CHNLSVC.Sales.Clone_hpr_para_types(txtCloneParaCode.Text.Trim().ToUpper(), txtClonePC.Text.Trim().ToUpper(), ClonePcList, BaseCls.GlbUserID, CHNLSVC.Security.GetServerDateTime().Date);
                if (eff > 0)
                {
                    //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cloning Compleated Successfully!");
                    //string Msg = "<script>alert('Cloning Compleated Successfully!');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

                    MessageBox.Show("Cloning Compleated Successfully!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Sorry. Failed to compleate. Please try again!");
                    MessageBox.Show("Sorry. Failed to compleate. Please try again!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                //string Msg = "<script>alert('Please add profit centers to the cloning list.');</script>";
                //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                MessageBox.Show("Please add profit centers to the cloning list", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void btnClearClonelist_Click(object sender, EventArgs e)
        {
            btnClonePara_Click(null, null);
        }

        private void btnClonePara_Click(object sender, EventArgs e)
        {
            ClonePcList = new List<string>();
            txtCloneParaCode.Text = "";
            txtClonePC.Text = "";
            txtCloneAddPc.Text = "";

            //grvClonePc.DataSource = null;
            //grvClonePc.AutoGenerateColumns = false;
            //grvClonePc.DataSource = ClonePcList;
            var pcs = (from _res in ClonePcList
                       select new { PC = _res });

            BindingSource source = new BindingSource();
            source.DataSource = pcs.ToList();
            grvClonePc.AutoGenerateColumns = false;
            grvClonePc.DataSource = source;

            panel_ClonePara.Size = new Size(490, 222);

            showPopUpPanel(panel_ClonePara);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TextBox txtBox = new TextBox();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
            DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtClonePC; //txtBox;
            _CommonSearch.ShowDialog();
            txtClonePC.Focus(); 
        }

        private void button8_Click(object sender, EventArgs e)
        {
            TextBox txtBox = new TextBox();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
            DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCloneAddPc; //txtBox;
            _CommonSearch.ShowDialog();
            txtCloneAddPc.Focus(); 
        }

        private void btnPCviewPara_Click(object sender, EventArgs e)
        {
            //
            TextBox txtBox = new TextBox();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
            DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtParaPc_PopUp; //txtBox;
            _CommonSearch.ShowDialog();
            txtParaPc_PopUp.Focus(); 
        }

        private void btnViewAccRestPc_Click(object sender, EventArgs e)
        {
            TextBox txtBox = new TextBox();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
            DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtVeiwPcAcRestr; //txtBox;
            _CommonSearch.ShowDialog();
            txtVeiwPcAcRestr.Focus(); 
        }
        private void btnCloseAccRstView_Click(object sender, EventArgs e)
        {
            panel_viewAcRestr.Visible = false;
        }
        private void btnVeiwAcRestr_Click(object sender, EventArgs e)
        {
            if (rdoVeiwMonthly.Checked == false && rdoVeiwAnnually.Checked==false)
            {
                MessageBox.Show("Please select Anually or Monthly option", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);   
                return;
            }
            if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID,BaseCls.GlbUserComCode, string.Empty, "ACRES") == false)
            {
                //DataTable dt = CHNLSVC.General.Get_All_User_paramTypes("ACRES");
                //string desc = string.Empty;
                //if (dt != null)
                //{
                //    if (dt.Rows.Count > 0)
                //    {
                //        try
                //        {
                //            desc = "(No Permission for '" + dt.Rows[0]["seup_usr_permdesc"].ToString() + "')";
                //        }
                //        catch (Exception ex)
                //        {
                //            desc = "(No Permission for 'ACRES')";
                //        }

                //    }
                //}
                MessageBox.Show("Permission Denied!\n( Advice: Reqired permission code :ACRES)", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);   
                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Permission Denied! " + desc);
                return;
            }
            if (rdoVeiwMonthly.Checked)
            {
                List<HpAccRestriction> list = CHNLSVC.Sales.GetAll_SavedAccountRestrictons(txtVeiwPcAcRestr.Text.Trim().ToUpper(), 1);
                grvVeiwAcRestr.DataSource = null;
                grvVeiwAcRestr.AutoGenerateColumns = false;
                grvVeiwAcRestr.DataSource = list;
               
            }
            if (rdoVeiwAnnually.Checked)
            {
                List<HpAccRestriction> list = CHNLSVC.Sales.GetAll_SavedAccountRestrictons(txtVeiwPcAcRestr.Text.Trim().ToUpper(), 2);
                grvVeiwAcRestr.DataSource = null;
                grvVeiwAcRestr.AutoGenerateColumns = false;
                grvVeiwAcRestr.DataSource = list;                
            }
        }

        private void btnViewRestr_Click(object sender, EventArgs e)
        {
            txtVeiwPcAcRestr.Text = "";

            List<HpAccRestriction> veiw_list = new List<HpAccRestriction>();
            grvVeiwAcRestr.DataSource = null;
            grvVeiwAcRestr.AutoGenerateColumns = false;
            grvVeiwAcRestr.DataSource = veiw_list;

            panel_viewAcRestr.Size = new Size(810, 268);

            showPopUpPanel(panel_viewAcRestr);
        }

        private void txtParaCode_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.ImgCodeSearch_Click(null,null);
        }

        private void txtParaCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.ImgCodeSearch_Click(null, null);
            }
        }

        private void txtParaCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtFromDt_pty.Focus();
            }   
        }

        private void txtFromDt_pty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtToDt_pty.Focus();
            }
        }

        private void txtToDt_pty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtValue_pty.Focus();
            }  
        }

        private void txtValue_pty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.btnAddPara_Click(null, null);
            } 
        }

        private void txtFromDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {                
                txtToDate.Focus();
            } 
        }

        private void txtToDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtApprNoOfAcc.Focus();
            } 
        }

        private void txtApprNoOfAcc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtSalesVal.Focus();
            } 
        }

        private void txtSalesVal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtNoOfMonths.Focus();
            }
        }

        private void txtNoOfMonths_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.ButtonAdd_Click(null, null);
            }
        }

        private void label32_MouseMove(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Left)
            //{
            //    Int32 x_position = 0;
            //    Int32 y_position = 0;

            //    //x_position = (this.panel_move.Location.X + e.X) > this.Width ? this.panel_move.Location.X : this.panel_move.Location.X + e.X;
            //    x_position = this.panel_viewAcRestr.Location.X + e.X;
            //    y_position = this.panel_viewAcRestr.Location.Y + e.Y;

            //    this.panel_viewAcRestr.Location = new Point(x_position, y_position);
            //    //this.panel_move.Location = new Point(Cursor.Position.X + e.X, Cursor.Position.Y + e.Y);
            //}
        }

        
    }
}
