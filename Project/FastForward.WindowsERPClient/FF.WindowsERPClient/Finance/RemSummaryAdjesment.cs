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
using System.IO;

namespace FF.WindowsERPClient.Finance
{
    public partial class RemSummaryAdjesment :Base
    {
        #region properties

        private bool canUpdate;
        public bool CanUpdate
        {
            get { return canUpdate; }
            set { canUpdate = value; }
        }
      
        private bool isExcess;
        public bool IsExcess
        {
            get { return isExcess; }
            set { isExcess = value; }
        }      
        #endregion

        public RemSummaryAdjesment()
        {
            InitializeComponent();

            CanUpdate = false;
            IsExcess = false;
            BindSections(DropDownListSection);
            TextBoxRemitanceDate.Value = CHNLSVC.Security.GetServerDateTime();
            //CHNLSVC.Security.GetServerDateTime().Date

            txtComp.Text = BaseCls.GlbUserComCode;
            txtPC.Text = BaseCls.GlbUserDefProf;

             MonthYearPicker.CustomFormat = "MMM yyyy";

             cmbYear.Items.Add("2012");
             cmbYear.Items.Add("2013");
             cmbYear.Items.Add("2014");
             cmbYear.Items.Add("2015");
             cmbYear.Items.Add("2016");
             cmbYear.Items.Add("2017");
             cmbYear.Items.Add("2018");
             cmbMonth.SelectedIndex = -1;

             int _Year = DateTime.Now.Year;
             cmbYear.SelectedIndex = _Year % 2013 + 1;

             cmbMonth.Items.Add("January");
             cmbMonth.Items.Add("February");
             cmbMonth.Items.Add("March");
             cmbMonth.Items.Add("April");
             cmbMonth.Items.Add("May");
             cmbMonth.Items.Add("June");
             cmbMonth.Items.Add("July");
             cmbMonth.Items.Add("August");
             cmbMonth.Items.Add("September");
             cmbMonth.Items.Add("October");
             cmbMonth.Items.Add("November");
             cmbMonth.Items.Add("December");
             cmbMonth.SelectedIndex = DateTime.Now.Month - 1;

        }

       

        private void TextBoxRemitanceDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                DivViewAmo.Visible = false;
                IsExcess = false;
                CanUpdate = false;
                TextBoxOriginalComment.Enabled = false;
                TextBoxOriginalAmount.Enabled = false;
                BindGridView();
                BindSections(DropDownListSection);
                LoadRemitTypes(DropDownListSection.SelectedValue.ToString());
                TextBoxOriginalAmount.Text = "";
                TextBoxFinalAmount.Text = "";
                TextBoxOriginalComment.Text = "";
                TextBoxFinalComment.Text = "";
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

        private void BindGridView()
        {
            try
            {
                List<RemitanceSummaryDetail> _remsumdetTem = CHNLSVC.Financial.GetRemitanceSumDetailAdjusment(Convert.ToDateTime(TextBoxRemitanceDate.Value.Date), BaseCls.GlbUserDefProf);
                if (_remsumdetTem != null)
                {
                    List<RemitanceSummaryDetail> _remsumdet = new List<RemitanceSummaryDetail>();
                    //loop for remove section 5 ,records
                    foreach (RemitanceSummaryDetail _rem in _remsumdetTem)
                    {
                        if (_rem.Rem_sec != "05")
                        {
                            _rem.Rem_lg_desc = _rem.RemSumDet.Rsd_desc;
                            _remsumdet.Add(_rem);
                        }
                    }
                    try
                    {
                        var ascendingQuery = from data in _remsumdet
                                             orderby data.Rem_sec, data.Rem_cd ascending
                                             select data;

                        gvRemLimit.DataSource = null;
                        gvRemLimit.AutoGenerateColumns = false;
                        gvRemLimit.DataSource = ascendingQuery.ToList();
                    }
                    catch (Exception ex)
                    {
                        gvRemLimit.DataSource = null;
                        gvRemLimit.AutoGenerateColumns = false;
                        gvRemLimit.DataSource = _remsumdet;
                    }
                }
                else
                {
                    BindEmptyData();
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
        private void BindEmptyData()
        {
            try
            {
                gvRemLimit.DataSource = null;
                gvRemLimit.AutoGenerateColumns = false;
                gvRemLimit.DataSource = CHNLSVC.Financial.GetRemSumLimitations("", "", "", "");
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
          //  gvRemLimit.DataBind();
        }

        private void BindSections(ComboBox DropDownListSection)
        {
            try
            {
                // DropDownListSection.Items.Clear();
                //DropDownListSection.Items.Add(new ListItem("--Select Section--", "-1"));
                List<RemSection> bindlist = new List<RemSection>();
                RemSection select = new RemSection();
                select.Rss_desc = "--Select Section--";
                select.Rss_cd = "-1";
                bindlist.Add(select);

                List<RemSection> getlist = CHNLSVC.Financial.GetSection();
                if (getlist != null)
                {
                    bindlist.AddRange(getlist);
                }

                bindlist.RemoveAll(x => x.Rss_cd == "05");

                DropDownListSection.DataSource = bindlist;//CHNLSVC.Financial.GetSection();
                DropDownListSection.DisplayMember = "rss_desc";
                DropDownListSection.ValueMember = "rss_cd";
                DropDownListSection.SelectedValue = "-1";
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

            //DropDownListSection.DataTextField = "rss_desc";
            //DropDownListSection.DataValueField = "rss_cd";
            //DropDownListSection.DataBind();            
            //DropDownListSection.Items.Remove(new ListItem("Remittance Details", "05"));
        }
        private void LoadRemitTypes(string _sec)
        {
            try
            {
                //DropDownListRemitType.Items.Clear();
                //DropDownListRemitType.Items.Add(new ListItem("--Select Type--", "-1"));
                //DropDownListRemitType.DataSource = CHNLSVC.Financial.get_rem_type_by_sec(_sec, 0);
                //DropDownListRemitType.DataTextField = "rsd_desc";
                //DropDownListRemitType.DataValueField = "rsd_cd";
                //DropDownListRemitType.DataBind();

                //DropDownListRemitType.Items.Clear();
                List<RemitanceSumHeading> bindlist_ = new List<RemitanceSumHeading>();
                RemitanceSumHeading select = new RemitanceSumHeading();
                select.Rsd_desc = "--Select Type--";
                select.Rsd_cd = "-1";
                bindlist_.Add(select);

                List<RemitanceSumHeading> getlist_ = new List<RemitanceSumHeading>();
                getlist_ = CHNLSVC.Financial.get_rem_type_by_sec(_sec, 0);
                if (getlist_ != null)
                {
                    bindlist_.AddRange(getlist_);
                }

                DropDownListRemitType.DataSource = bindlist_;
                DropDownListRemitType.DisplayMember = "rsd_desc";
                DropDownListRemitType.ValueMember = "rsd_cd";
                DropDownListRemitType.SelectedValue = "-1";
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

        private void DropDownListSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadRemitTypes(DropDownListSection.SelectedValue.ToString());
                DropDownListSection.Select();
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
                RemSummaryAdjesment formnew = new RemSummaryAdjesment();
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

        protected void CalBonusNet(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxFinalAmount.Text) || string.IsNullOrEmpty(txtFinalAdjusment.Text) || string.IsNullOrEmpty(txtFinalDeduction.Text))
            { }
            else
            {
                txtFinalNet.Text = (Convert.ToDecimal(TextBoxFinalAmount.Text) + Convert.ToDecimal(txtFinalAdjusment.Text) - Convert.ToDecimal(txtFinalDeduction.Text)).ToString();
            }
        }

        private void gvRemLimit_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void load_details(string _sec,string _code,string _refno)
        {
            try
            {
                if (_sec == "02" && _code == "013")
                {
                    label7.Text = "Final Gross Amount";
                }
                else
                {
                    label7.Text = "Final Amount";
                }
                TextBoxOriginalAmount.Text = "0";
                TextBoxFinalAmount.Text = "0";
                //insert excess process
                //section cd Summary and rem type Fine Charges 
                //Remitance date has to equal DateTime.Now
                lblRef.Text = "";
                if (_sec == "03" && _code == "027")
                {
                    //today
                    //if (Convert.ToDateTime(TextBoxRemitanceDate.Value.ToString()).Date == DateTime.Now.Date)
                    if (Convert.ToDateTime(TextBoxRemitanceDate.Value.Date).Date == CHNLSVC.Security.GetServerDateTime().Date)
                    {
                        DivViewAmo.Visible = true;
                        decimal value = 0;
                        //CHNLSVC.Financial.GetPrvDayExcess(DateTime.Now,BaseCls.GlbUserDefProf, out value);CHNLSVC.Security.GetServerDateTime().Date
                        CHNLSVC.Financial.GetPrvDayExcess(CHNLSVC.Security.GetServerDateTime().Date, BaseCls.GlbUserDefProf,BaseCls.GlbUserComCode, out value);
                        //LabelExcess.Text = value.ToString();
                        LabelExcess.Text = string.Format("{0:n2}", value);

                        // TextBoxOriginalAmount.Text = "";
                        TextBoxFinalAmount.Text = value.ToString();
                        IsExcess = true;
                        CanUpdate = true;
                    }
                    //back date
                    else if (CHNLSVC.General.IsAllowBackDate(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf, TextBoxRemitanceDate.Value.Date.ToString()))
                    {
                        DivViewAmo.Visible = true;
                        decimal value = 0;
                        CHNLSVC.Financial.GetPrvDayExcess(Convert.ToDateTime(TextBoxRemitanceDate.Value.Date), BaseCls.GlbUserDefProf,BaseCls.GlbUserComCode, out value);
                        LabelExcess.Text = value.ToString();

                        //  TextBoxOriginalAmount.Text = "";
                        TextBoxFinalAmount.Text = value.ToString();
                        IsExcess = true;
                        CanUpdate = true;
                    }



                }
                //update process
                else
                {
                    if (!(_sec == "03" && _code == "027"))
                    {


                        RemitanceSummaryDetail _remDet = CHNLSVC.Financial.GetRemitanceAdjesmentDetails(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(TextBoxRemitanceDate.Value.Date), _sec, _code,null);
                        if (_remDet != null)
                        {
                            CanUpdate = true;
                            TextBoxOriginalAmount.Text = _remDet.Rem_val.ToString();
                            TextBoxFinalAmount.Text = (_remDet.Rem_val_final).ToString();
                            TextBoxFinalComment.Text = _remDet.Rem_rmk_fin;
                            TextBoxOriginalComment.Text = _remDet.Rem_rmk;                      
                        }
                        else
                        {
                            CanUpdate = false;
                            // TextBoxOriginalAmount.Text = "";
                            // TextBoxFinalAmount.Text = "";
                            TextBoxOriginalComment.Text = "";
                            TextBoxFinalComment.Text = "";
                        }

                        if ((_sec == "02" && _code == "013"))
                        {
                            RemitanceSummaryDetail _tremDet = CHNLSVC.Financial.GetRemitanceAdjesmentDetails(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(TextBoxRemitanceDate.Value.Date), _sec, _code, _refno);
                            if (_tremDet != null)
                            {
                                lblAdjusment.Text = _tremDet.Rem_add.ToString();
                                lblDeduction.Text = _tremDet.Rem_ded.ToString();

                                //TextBoxFinalAmount.Text = (_remDet.Rem_val_final - _remDet.Rem_add_fin + _remDet.Rem_ded_fin).ToString();
                                

                                lblNet.Text = _tremDet.Rem_net.ToString();
                                txtFinalNet.Text = _tremDet.Rem_net_fin.ToString();

                                txtFinalAdjusment.Text = _tremDet.Rem_add_fin.ToString();
                                txtFinalDeduction.Text = _tremDet.Rem_ded_fin.ToString();
                                lblRef.Text = _tremDet.Rem_ref_no.ToString();

                                TextBoxOriginalAmount.Text = _tremDet.Rem_val.ToString();
                                TextBoxFinalComment.Text = _tremDet.Rem_rmk_fin;
                                TextBoxOriginalComment.Text = _tremDet.Rem_rmk;
                                TextBoxFinalAmount.Text = (_tremDet.Rem_val_final).ToString();   //kapila 26/12/2013

                                CanUpdate = true;
                            }
                        }
                    }
                    DivViewAmo.Visible = false;
                    TextBoxOriginalComment.Enabled = false;
                    TextBoxOriginalAmount.Enabled = false;
                    IsExcess = false;
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
            // string type_des = DropDownListRemitType.Text;
            //string code = _code;
            //MessageBox.Show("Type Desc :" + type_des + "\nTp Code :" + code);
        }

        private void DropDownListRemitType_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_details(DropDownListSection.SelectedValue.ToString(), DropDownListRemitType.SelectedValue.ToString(),"");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "REMSM"))
                //{

                //}
                //else
                //{
                //    MessageBox.Show("Permission Not Granted!\n( Advice: Reqired permission code :REMSM )", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
                #region validation

                decimal tem;

                if (DropDownListRemitType.SelectedValue.ToString() == "-1")
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select remitance type");
                    MessageBox.Show("Please select Remitance Type!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (DropDownListSection.SelectedValue.ToString() == "-1")
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select remitance section");
                    MessageBox.Show("Please select Remitance Section!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (TextBoxFinalAmount.Text == "")
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter final amount");
                    TextBoxFinalAmount.Focus();
                    MessageBox.Show("Please enter Final Amount!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!decimal.TryParse(TextBoxFinalAmount.Text, out tem))
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter final amount in correctly");
                    TextBoxFinalAmount.Focus();
                    MessageBox.Show("Please enter valid Final Amount!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (CHNLSVC.Financial.IsPeriodClosed(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "FIN_REM", Convert.ToDateTime(TextBoxRemitanceDate.Value.Date)))
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Period Close");
                    MessageBox.Show("Period Close!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (DropDownListSection.SelectedValue.ToString() == "02" && DropDownListRemitType.SelectedValue.ToString() == "013" && string.IsNullOrEmpty(lblRef.Text))
                {
                    MessageBox.Show("Select collection bonus!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("Are you sure to save?", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                #endregion

                if (IsExcess)
                {
                    if (Convert.ToDecimal(TextBoxFinalAmount.Text) > Convert.ToDecimal(LabelExcess.Text))
                    {
                        //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Fine charge can not exceed Pre. Day Excess amount");
                        MessageBox.Show("Fine charge can not exceed Pre.Day Excess amount!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    //insert new record
                    RemitanceSummaryDetail _remDet = new RemitanceSummaryDetail();
                    _remDet.Rem_dt = Convert.ToDateTime(TextBoxRemitanceDate.Value.Date).Date;
                    decimal week = 0;
                    CHNLSVC.General.GetWeek(Convert.ToDateTime(TextBoxRemitanceDate.Value.Date).Date, out week, BaseCls.GlbUserComCode);
                    _remDet.Rem_week = week.ToString();
                    _remDet.Rem_com = BaseCls.GlbUserComCode;
                    _remDet.Rem_pc = BaseCls.GlbUserDefProf;
                    _remDet.Rem_cre_by = BaseCls.GlbUserID;
                    _remDet.Rem_cre_dt = CHNLSVC.Security.GetServerDateTime().Date;//DateTime.Now;
                    _remDet.Rem_rmk = TextBoxFinalComment.Text;
                    _remDet.Rem_rmk_fin = TextBoxFinalComment.Text;
                    _remDet.Rem_val = Convert.ToDecimal(TextBoxFinalAmount.Text);
                    _remDet.Rem_val_final = Convert.ToDecimal(TextBoxFinalAmount.Text);
                    //TODO: Need section and type codes
                    _remDet.Rem_sec = DropDownListSection.SelectedValue.ToString();
                    _remDet.Rem_cd = DropDownListRemitType.SelectedValue.ToString();
                    _remDet.Rem_sh_desc = DropDownListRemitType.Text; //DropDownListRemitType.SelectedText.ToString();//DropDownListRemitType.SelectedItem.Text;
                    _remDet.Rem_lg_desc = DropDownListRemitType.Text;//DropDownListRemitType.SelectedText.ToString();//DropDownListRemitType.SelectedItem.Text;
                    _remDet.REM_REM_MONTH = Convert.ToDateTime("31/Dec/9999");    //kapila 14/3/2013

                    //kapila 28/5/2013
                    _remDet.Rem_is_dayend = true;
                    _remDet.Rem_is_rem_sum = true;
                    _remDet.Rem_is_sos = true;
                    _remDet.Rem_is_sun = true;

                    //8/8/2013
                    _remDet.REM_CHQNO = "";
                    _remDet.REM_CHQ_BANK_CD = "";
                    _remDet.REM_CHQ_BRANCH = "";
                    _remDet.REM_CHQ_DT = Convert.ToDateTime("31/Dec/2099");
                    _remDet.REM_DEPOSIT_BANK_CD = "";
                    _remDet.REM_DEPOSIT_BRANCH = "";


                    //add sachith
                    if (!string.IsNullOrEmpty(txtFinalAdjusment.Text)) {
                        try
                        {
                            _remDet.Rem_add_fin = Convert.ToDecimal(txtFinalAdjusment.Text);
                        }
                        catch (Exception) { }
                    }
                    if (!string.IsNullOrEmpty(txtFinalDeduction.Text))
                    {
                        try
                        {
                            _remDet.Rem_add_fin = Convert.ToDecimal(txtFinalDeduction.Text);
                        }
                        catch (Exception) { }
                    }

                    CanUpdate = false;
                    IsExcess = false;

                    int result = CHNLSVC.Financial.SaveRemSummaryDetails(_remDet);
                    if (result > 0)
                    {
                        //string Msg = "<script>alert('Record Insert Successfully!!');window.location = 'RemSummaryAdjesment.aspx';</script>";
                        //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                        MessageBox.Show("Record Inserted Successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // string Msg = "<script>alert('Error occurded while processing!!')</script>";
                        // ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                        MessageBox.Show("Error occurded while processing!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (CanUpdate)
                {
                    decimal val_fin;
                    RemitanceSummaryDetail _remDet = new RemitanceSummaryDetail();
                    _remDet.Rem_com = BaseCls.GlbUserComCode;
                    _remDet.Rem_pc = BaseCls.GlbUserDefProf;
                    _remDet.Rem_dt = Convert.ToDateTime(TextBoxRemitanceDate.Value.Date).Date;
                    _remDet.Rem_sec = DropDownListSection.SelectedValue.ToString();
                    _remDet.Rem_cd = DropDownListRemitType.SelectedValue.ToString();
                    val_fin = Convert.ToDecimal(TextBoxFinalAmount.Text);
                    _remDet.Rem_rmk_fin = TextBoxFinalComment.Text;
                    _remDet.Rem_val = Convert.ToDecimal(TextBoxOriginalAmount.Text);
                    _remDet.Rem_rmk = TextBoxOriginalComment.Text;



                    if (!string.IsNullOrEmpty(txtFinalAdjusment.Text))
                    {
                        try
                        {
                            _remDet.Rem_add_fin = Convert.ToDecimal(txtFinalAdjusment.Text);
                        }
                        catch (Exception) {
                         _remDet.Rem_add_fin=0;
                        }
                    }
                    if (!string.IsNullOrEmpty(txtFinalDeduction.Text))
                    {
                        try
                        {
                            _remDet.Rem_ded_fin = Convert.ToDecimal(txtFinalDeduction.Text);
                        }
                        catch (Exception) { 
                         _remDet.Rem_ded_fin=0;
                        }
                    }

                    _remDet.Rem_net_fin =val_fin + _remDet.Rem_add_fin - _remDet.Rem_ded_fin;
//                    _remDet.Rem_val_final=_remDet.Rem_net_fin;
                    _remDet.Rem_val_final = val_fin;        //kapila 26/12/2013

                    int result = CHNLSVC.Financial.UpdateRemitanceAdjusment(_remDet);
                    if (result > 0)
                    {
                        //string Msg = "<script>alert('Record Updated Successfully!!');window.location = 'RemSummaryAdjesment.aspx';</script>";
                        //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                        MessageBox.Show("Record Updated Successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        //string Msg = "<script>alert('Error occurded while processing!!')</script>";
                        //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                        MessageBox.Show("Error occurded while updating!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    //set bool value
                    CanUpdate = false;
                }
                else
                {
                    //insert new record
                    RemitanceSummaryDetail _remDet = new RemitanceSummaryDetail();
                    _remDet.Rem_dt = Convert.ToDateTime(TextBoxRemitanceDate.Value.Date).Date;
                    decimal week = 0;
                    CHNLSVC.General.GetWeek(Convert.ToDateTime(TextBoxRemitanceDate.Value.Date).Date, out week, BaseCls.GlbUserComCode);
                    _remDet.Rem_week = week.ToString();
                    _remDet.Rem_cre_by = BaseCls.GlbUserID;
                    _remDet.Rem_cre_dt = CHNLSVC.Security.GetServerDateTime().Date;//DateTime.Now;
                    _remDet.Rem_rmk_fin = TextBoxFinalComment.Text;
                    _remDet.Rem_val_final = Convert.ToDecimal(TextBoxFinalAmount.Text);
                    _remDet.Rem_val = Convert.ToDecimal(TextBoxOriginalAmount.Text);

                    _remDet.Rem_sec = DropDownListSection.SelectedValue.ToString();
                    _remDet.Rem_cd = DropDownListRemitType.SelectedValue.ToString();

                    _remDet.Rem_com = BaseCls.GlbUserComCode;
                    _remDet.Rem_pc = BaseCls.GlbUserDefProf;
                    _remDet.Rem_sh_desc = DropDownListRemitType.Text;//DropDownListRemitType.SelectedItem.Text;
                    _remDet.Rem_lg_desc = DropDownListRemitType.Text;//DropDownListRemitType.SelectedItem.Text;
                    _remDet.REM_REM_MONTH = Convert.ToDateTime("31/Dec/9999");     //kapila 14/3/2013
                    CanUpdate = false;
                    IsExcess = false;

                    //kapila 28/5/2013
                    _remDet.Rem_is_dayend = true;
                    _remDet.Rem_is_rem_sum = true;
                    _remDet.Rem_is_sos = true;
                    _remDet.Rem_is_sun = true;
                    //8/8/2013
                    _remDet.REM_CHQNO = "";
                    _remDet.REM_CHQ_BANK_CD = "";
                    _remDet.REM_CHQ_BRANCH = "";
                    _remDet.REM_CHQ_DT = Convert.ToDateTime("31/Dec/2099");
                    _remDet.REM_DEPOSIT_BANK_CD = "";
                    _remDet.REM_DEPOSIT_BRANCH = "";

                    int result = CHNLSVC.Financial.SaveRemSummaryDetails(_remDet);
                    if (result > 0)
                    {
                        // string Msg = "<script>alert('Record Insert Successfully!!');window.location = 'RemSummaryAdjesment.aspx';</script>";
                        // ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                        MessageBox.Show("Record Inserted Successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        //string Msg = "<script>alert('Error occurded while processing!!')</script>";
                        //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                        MessageBox.Show("Error occured while processing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                btnClear_Click(null, null);
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

        private void btnFinalize_Click(object sender, EventArgs e)
        {
            StringBuilder _errorLst = new StringBuilder();
            try
            {
                //if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "REMSM"))
                //{

                //}
                //else
                //{
                //    MessageBox.Show("Permission Not Granted!\n( Advice: Reqired permission code :REMSM )", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
                txtErrList.Text = "";
                Boolean _isSel = false;

                if (string.IsNullOrEmpty(lblFrmdtWk.Text))
                {
                    MessageBox.Show("Select the Week!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (lstPC.Items.Count == 0)
                {
                    if (string.IsNullOrEmpty(txtPC.Text))
                    {
                        MessageBox.Show("Select the location(s)", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (CHNLSVC.Financial.IsPeriodClosed(BaseCls.GlbUserComCode, txtPC.Text, "FIN_REM", Convert.ToDateTime(lblFrmdtWk.Text).Date))
                    {
                        MessageBox.Show("Already Finalized the period!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    _isSel = true;
                }
                else
                {
                    foreach (ListViewItem Item in lstPC.Items)
                    {
                        string pc = Item.Text;

                        if (Item.Checked == true)
                        {
                            _isSel = true;
                            if (CHNLSVC.Financial.IsPeriodClosed(BaseCls.GlbUserComCode, pc, "FIN_REM", Convert.ToDateTime(lblFrmdtWk.Text).Date))
                                if (string.IsNullOrEmpty(Convert.ToString(_errorLst)))
                                    _errorLst.Append("Already Finalized the period - " + pc);
                                else
                                    _errorLst.Append(_errorLst);
                        }
                    }
                }

                if(_isSel ==false)
                {
                    MessageBox.Show("Select the location(s)", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if(!string.IsNullOrEmpty(_errorLst.ToString()))
                {
                    MessageBox.Show("Process halted. please check the error list", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtErrList.Text = _errorLst.ToString();
                    return;
                }

                Int32 month = MonthYearPicker.Value.Month;
                Int32 year = MonthYearPicker.Value.Year;

                // MessageBox.Show("Month= " + MonthYearPicker.Value.Month.ToString() + "\nYear= " + MonthYearPicker.Value.Year.ToString());
                if (string.IsNullOrEmpty(lblTodtWk.Text))
                {
                    MessageBox.Show("Week definition not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (MessageBox.Show("Are you sure to Finalize?", "Confirm Finalize", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                update_PC_List_RPTDB(); 

                RemitanceStatus _remStaus = new RemitanceStatus();
                _remStaus.Gpac_com = BaseCls.GlbUserComCode;
              //  _remStaus.Gpac_pc = BaseCls.GlbUserDefProf;

                DateTime _date = new DateTime(year, month, 1);

                _remStaus.Gpac_stus = true;
                _remStaus.Gpac_cre_by = BaseCls.GlbUserID;
                _remStaus.Gpac_cre_dt = CHNLSVC.Security.GetServerDateTime().Date;//DateTime.Now;
                //_date = _date.AddDays(DateTime.DaysInMonth(_date.Year, _date.Month) - 1);
                _remStaus.Gpac_op_dt = Convert.ToDateTime(lblTodtWk.Text);
                _remStaus.Gpac_tp = "FIN_REM";

                string _errMsg = "";
                int X = CHNLSVC.Financial.FinalizeDayEnd(_remStaus, Convert.ToDateTime(lblFrmdtWk.Text).Date, Convert.ToDateTime(lblTodtWk.Text).Date, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, BaseCls.GlbUserID, out _errMsg);

               // int result = CHNLSVC.Financial.SaveRemitanceStatus(_remStaus);

                if (string.IsNullOrEmpty(_errMsg))
                {
                    MessageBox.Show("Successfully Finalized!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error occurded while processing!\n" + _errMsg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void update_PC_List_RPTDB()
        {
            string _tmpPC = "";
            BaseCls.GlbReportProfit = "";

            Boolean _isPCFound = false;
            Int32 del = CHNLSVC.Sales.Delete_TEMP_PC_LOC_RPTDB(BaseCls.GlbUserID, txtComp.Text, null, null);

            foreach (ListViewItem Item in lstPC.Items)
            {
                string pc = Item.Text;

                if (Item.Checked == true)
                {
                    Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC_RPTDB(BaseCls.GlbUserID, txtComp.Text, pc, null);

                    _isPCFound = true;
                    if (string.IsNullOrEmpty(BaseCls.GlbReportProfit))
                    {
                        BaseCls.GlbReportProfit = pc;
                    }
                    else
                    {
                        //BaseCls.GlbReportProfit = BaseCls.GlbReportProfit + "," + pc;
                        BaseCls.GlbReportProfit = "All Locations Based on User Rights";
                    }
                }
            }

            if (_isPCFound == false)
            {
                BaseCls.GlbReportProfit = txtPC.Text;
                Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC_RPTDB(BaseCls.GlbUserID, txtComp.Text, txtPC.Text, null);
            }
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbMonth.SelectedIndex = -1;
            ddlWeek.SelectedIndex = -1;
            lblFrmdtWk.Text = "";
            lblTodtWk.Text = "";
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbYear.Text != "")
            {
                int month = cmbMonth.SelectedIndex + 1;
                if (month > 0)
                {
                    DateTime dtFrom = new DateTime(Convert.ToInt32(cmbYear.Text), month, 1);
            
                }
            }

            ddlWeek.SelectedIndex = -1;
            lblFrmdtWk.Text = "";
            lblTodtWk.Text = "";
        }

        private void ddlWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime _from;
                DateTime _to;
                if (cmbYear.Text == "")
                {
                    MessageBox.Show("Select Year !", "Scan Docs", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                if (!string.IsNullOrEmpty(ddlWeek.Text))
                {
                    DataTable _weekDef = CHNLSVC.General.GetWeekDefinition(Convert.ToInt32(cmbMonth.SelectedIndex + 1), Convert.ToInt32(cmbYear.Text), Convert.ToInt32(ddlWeek.SelectedIndex + 1), out _from, out _to,BaseCls.GlbUserComCode,"");
                    if (_weekDef == null)
                    {
                        MessageBox.Show("Week Definition not set!", "Scan Docs", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (_from != Convert.ToDateTime("31/Dec/9999"))
                    {
                        lblFrmdtWk.Text = _from.Date.ToString("dd/MMM/yyyy");
                        lblTodtWk.Text = _to.Date.ToString("dd/MMM/yyyy");
                    }
                    else
                    {
                        lblFrmdtWk.Text = string.Empty;
                        lblTodtWk.Text = string.Empty;
                    }
                }
                this.Cursor = Cursors.Default;
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

        private void TextBoxFinalAmount_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxFinalAmount.Text))
            CalBonusNet(null, null);
        }

        private void txtFinalAdjusment_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFinalAdjusment.Text))
            CalBonusNet(null, null);
        }

        private void txtFinalDeduction_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFinalDeduction.Text))
            CalBonusNet(null, null);
        }

        private void gvRemLimit_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            load_details(gvRemLimit.Rows[e.RowIndex].Cells[0].Value.ToString(), gvRemLimit.Rows[e.RowIndex].Cells[1].Value.ToString(), gvRemLimit.Rows[e.RowIndex].Cells[8].Value.ToString());
            DropDownListSection.SelectedValue = gvRemLimit.Rows[e.RowIndex].Cells[0].Value.ToString();
            DropDownListRemitType.SelectedValue = gvRemLimit.Rows[e.RowIndex].Cells[1].Value.ToString();

        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            string chanel = txtChanel.Text;
            string subChanel = txtSChanel.Text;
            string area = txtArea.Text;
            string region = txtRegion.Text;
            string zone = txtZone.Text;
            string pc = txtPC.Text;

            lstPC.Clear();

            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10044))
            {
                DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(BaseCls.GlbUserComCode, chanel, subChanel, area, region, zone, pc);
                foreach (DataRow drow in dt.Rows)
                {
                    if (!lstPC.Items.Equals(drow["PROFIT_CENTER"].ToString()))
                        lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                }
            }
            else
            {
                DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep(BaseCls.GlbUserID, BaseCls.GlbUserComCode, chanel, subChanel, area, region, zone, pc);
                foreach (DataRow drow in dt.Rows)
                {
                    if (!lstPC.Items.Equals(drow["PROFIT_CENTER"].ToString()))
                        lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                }
            }
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstPC.Items)
            {
                Item.Checked = true;
            }
        }

        private void btnNone_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstPC.Items)
            {
                Item.Checked = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lstPC.Clear();
        }




    }

}
