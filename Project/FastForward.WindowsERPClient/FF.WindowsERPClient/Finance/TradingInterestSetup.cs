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
using FF.WindowsERPClient.UtilityClasses;



namespace FF.WindowsERPClient.Finance
{
    public partial class TradingInterestSetup : Base
    {
        //List<TradingInterest> _lstInterest;
        //TradingInterest objInterest;
        public TradingInterestSetup()
        {
            InitializeComponent();
            LoadSchemeCategory();
        }
        List<TradingBase> _lstTradingbase; 
        List<TradingInterest> _lstTradingInterest;
        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void DrpDwnLstSchemeCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSchemeType();
        }

        #region MainCategory
        private void btnsearchMainCat_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtMaincat;
                _CommonSearch.txtSearchbyword.Text = txtMaincat.Text;
                _CommonSearch.ShowDialog();
                txtMaincat.Focus();
                btnSearchcat2.Enabled = true;
                txtCategory2.Text = "";
                txtCategory3.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtMaincat_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    txtMaincat_MouseDoubleClick(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtMaincat_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtMaincat.Text))
                {
                    DataTable _categoryDet = CHNLSVC.General.GetMainCategoryDetail(txtMaincat.Text);
                    if (_categoryDet.Rows.Count == 0)
                    {
                        MessageBox.Show("Invalid Main Category", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtMaincat.Text = "";
                        txtMaincat.Focus();
                        return;
                    }
                }
                if (string.IsNullOrEmpty(txtMaincat.Text))
                {
                    btnSearchcat2.Enabled = false;
                    btnSearchcat3.Enabled = false;
                }
                else
                {
                    btnSearchcat2.Enabled = true;
                    btnSearchcat3.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtMaincat_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                btnsearchMainCat_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region SubCategory2
        private void btnSearchcat2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaincat.Text))
                {
                    MessageBox.Show("Select Main Category", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCategory2;
                _CommonSearch.txtSearchbyword.Text = txtCategory2.Text;
                _CommonSearch.ShowDialog();
                txtCategory2.Focus();
                btnSearchcat3.Enabled = true;
                txtCategory3.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtCategory2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    txtCategory2_MouseDoubleClick(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtCategory2_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCategory2.Text))
                {
                    DataTable _cat2 = CHNLSVC.Sales.GetItemSubCate2(txtMaincat.Text, txtCategory2.Text);
                    if (_cat2.Rows.Count == 0)
                    {
                        MessageBox.Show("Invalid category 2", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtCategory2.Text = "";
                        txtCategory2.Focus();
                        return;
                    }
                }

                if (string.IsNullOrEmpty(txtCategory2.Text))
                    btnSearchcat3.Enabled = false;
                else
                    btnSearchcat3.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtCategory2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                btnSearchcat2_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region SubCategory3
        private void btnSearchcat3_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaincat.Text))
                {
                    MessageBox.Show("Select Main Category", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (string.IsNullOrEmpty(txtCategory2.Text))
                {
                    MessageBox.Show("Select Category2", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCategory3;
                _CommonSearch.txtSearchbyword.Text = txtCategory3.Text;
                _CommonSearch.ShowDialog();
                txtCategory2.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
        }

        private void txtCategory3_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    txtCategory3_MouseDoubleClick(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
        }

        private void txtCategory3_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCategory3.Text))
                {
                    DataTable _cat3 = CHNLSVC.Sales.GetItemSubCate3(txtMaincat.Text, txtCategory2.Text, txtCategory3.Text);
                    if (_cat3.Rows.Count == 0)
                    {
                        MessageBox.Show("Invalid Item category 3", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtCategory3.Text = "";
                        txtCategory3.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
        }

        private void txtCategory3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                btnSearchcat3_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
        }
        #endregion

        #region ItemCode
        private void btnSearch_Itemcode_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterItem);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtItemcode;
            _CommonSearch.ShowDialog();
            txtItemcode.Select();
        }

        private void txtItemcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearch_Itemcode_Click(null, null);
            }

        }

        private void txtItemcode_Leave(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtItemcode.Text))
            {
                MasterItem _itemdetail = new MasterItem();
                _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItemcode.Text);
                if (_itemdetail == null)
                {
                    MessageBox.Show("Invalid Item Code", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtItemcode.Text = "";
                    txtItemcode.Focus();
                    return;
        }
            }
        }

        private void txtItemcode_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_Itemcode_Click(null, null);
        }
        #endregion

        #region Methods
        private void LoadSchemeCategory()
        {
            DataTable dt = CHNLSVC.Sales.GetSAllchemeCategoryies("%");
            DrpDwnLstSchemeCategory.DataSource = dt;
            DrpDwnLstSchemeCategory.DisplayMember = "HSC_DESC";
            DrpDwnLstSchemeCategory.ValueMember = "HSC_CD";
        }

        private void LoadSchemeType()
        {
            List<HpSchemeType> _schemeList = CHNLSVC.Sales.GetSchemeTypeByCategory(DrpDwnLstSchemeCategory.SelectedValue.ToString());
            foreach (HpSchemeType sc in _schemeList)
            {
                string space = "";
                if ((10 - sc.Hst_cd.Length) > 0)
                {
                    for (int i = 0; i <= (10 - sc.Hst_cd.Length); i++)
                    {
                        space = space + " ";
                    }
                }
                sc.Hst_desc = sc.Hst_cd + space + "--" + sc.Hst_desc;
            }

            DrpDwnLstSchemeType.DataSource = _schemeList;
            DrpDwnLstSchemeType.DisplayMember = "Hst_desc";
            DrpDwnLstSchemeType.ValueMember = "Hst_cd";
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator + "" + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {
                        paramsText.Append(txtMaincat.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        paramsText.Append(txtMaincat.Text + seperator + txtCategory2.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append("" + seperator + "" + seperator + BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "Loc" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Brand:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Brand.ToString() + seperator + "" + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemStatus:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                //case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location:
                //    {
                //        paramsText.Append(com + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location.ToString() + seperator);
                //        break;
                //    }

                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion

        private void btnAddSchemes_Click(object sender, EventArgs e)
        {
            List<TradingBase> _lstTradingbase = new List<TradingBase>();
            try
            {
                #region Validation
                //if ((!string.IsNullOrEmpty(txtSchmeCode.Text)) && (!string.IsNullOrEmpty(txtSchemeTerm.Text)))
                //{
                //    DataTable dt = CHNLSVC.Financial.ChekSchemeValidCode(txtSchmeCode.Text, txtSchemeTerm.Text);

                //    if (dt.Rows.Count <= 0)
                //    {
                //       // MessageBox.Show("Please Select Valid SchemeCode/Term", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //       // return;
                //    }

                //}

                //if (!string.IsNullOrEmpty(txtSchmeCode.Text))
                //{
                //    DataTable dt = CHNLSVC.Financial.ChekSchemeValidCode(txtSchmeCode.Text, "");

                //    if (dt.Rows.Count <= 0)
                //    {
                //       // MessageBox.Show("Please Select Valid SchemeCode", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //      //  return;
                //    }

                //}

                //if (!string.IsNullOrEmpty(txtSchemeTerm.Text))
                //{
                //    DataTable dt = CHNLSVC.Financial.ChekSchemeValidCode("", txtSchemeTerm.Text);

                //    if (dt.Rows.Count <= 0)
                //    {
                //        MessageBox.Show("Please Select Valid TermCode", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        return;
                //    }

                //}

                if ((string.IsNullOrEmpty(txtSchemeTerm.Text)))
                {
                   // MessageBox.Show("Please Enter The Term", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //return;
                }

                decimal term = 0;
                decimal.TryParse(txtSchemeTerm.Text, out term);

                if (term <= 0 || term > 100)
                {
                  //  MessageBox.Show("Please Enter The Valid Term", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                  //  return;
                }

                #endregion

                TradingBase objTradingbase = new TradingBase();

                objTradingbase.sch_cat = DrpDwnLstSchemeCategory.SelectedValue.ToString();

                if (DrpDwnLstSchemeType.SelectedIndex != -1)
               {
                objTradingbase.sch_type = DrpDwnLstSchemeType.SelectedValue.ToString();
               }
             
                //objTradingbase.sch_code = txtSchmeCode.Text;
                //objTradingbase.sch_term = txtSchemeTerm.Text;
               // _lstTradingbase.Add(objTradingbase);

                //grdDefinition.AutoGenerateColumns = false;
                //grdDefinition.DataSource = _lstTradingbase;

                if (DrpDwnLstSchemeCategory.SelectedIndex == -1)
                {
                    return;
                }
                if (chkAll.Checked == true)
                {
                    DrpDwnLstSchemeType.SelectedIndex = -1;
                }
             
                if (chkAll.Checked == true)
                {
                    List<HpSchemeType> _schemeList = CHNLSVC.Sales.GetSchemeTypeByCategory(DrpDwnLstSchemeCategory.SelectedValue.ToString());
                    grdDefinition.DataSource = _schemeList;
                    DrpDwnLstSchemeType.DisplayMember = "Hst_desc";
                    DrpDwnLstSchemeType.ValueMember = "Hst_cd";

                    DataTable dt = new DataTable();
                    int termval = 0; 
                    int.TryParse(txtSchemeTerm.Text,out termval);

                    if (termval > 0)
                    {
                        foreach (HpSchemeType schTp in _schemeList)
                        {
                            DataTable dt1 = CHNLSVC.Sales.GetSchemes_term("TERM", termval, schTp.Hst_cd);
                            dt.Merge(dt1);
                        }
                    }
                    else
                    {
                        foreach (HpSchemeType schTp in _schemeList)
                        {
                            DataTable dt1 = CHNLSVC.Sales.GetSchemes("TYPE", schTp.Hst_cd);
                            dt.Merge(dt1);
                        }
                    }

                  



                    grdDefinition.DataSource = null;
                    grdDefinition.AutoGenerateColumns = false;
                    grdDefinition.DataSource = dt;
                }
                else
                {
                    if (DrpDwnLstSchemeType.SelectedIndex == -1)
                    {
                         MessageBox.Show("Please Select Scheme Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    //DataTable datasource = CHNLSVC.Sales.GetSchemes("TYPE", DrpDwnLstSchemeType.SelectedValue.ToString());

                    //grdDefinition.AutoGenerateColumns = false;
                    //grdDefinition.DataSource = datasource;


                    //List<HpSchemeType> _schemeList = CHNLSVC.Sales.GetSchemeTypeByCategory(DrpDwnLstSchemeCategory.SelectedValue.ToString());
                    //grdDefinition.DataSource = _schemeList;
                    //DrpDwnLstSchemeType.DisplayMember = "Hst_desc";
                    //DrpDwnLstSchemeType.ValueMember = "Hst_cd";

                    DataTable dt = new DataTable();
                    int termval = 0;
                    int.TryParse(txtSchemeTerm.Text, out termval);

                    if (termval > 0)
                    {
                       
                        DataTable dt1 = CHNLSVC.Sales.GetSchemes_term("TERM", termval, DrpDwnLstSchemeType.SelectedValue.ToString());
                        dt = dt1;
                    }
                    else
                    {
                        DataTable dt1 = CHNLSVC.Sales.GetSchemes("TYPE", DrpDwnLstSchemeType.SelectedValue.ToString());
                        dt = dt1;
                    }





                    grdDefinition.DataSource = null;
                grdDefinition.AutoGenerateColumns = false;
                    grdDefinition.DataSource = dt;


                }

                CommonUIValidation.TextBoxvalidation(new TextBox[] { txtSchemeTerm }, CommonUIValidation.validationType.Clear);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAddFilterations_Click(object sender, EventArgs e)
        {
            _lstTradingInterest = new List<TradingInterest>();
            _lstTradingbase = new List<TradingBase>();
            try
            {
                #region Validation

                if ((string.IsNullOrEmpty(txtRate.Text)))
                {
                    MessageBox.Show("Please Enter The Rate", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal rate = 0;
                decimal.TryParse(txtRate.Text, out rate);

                if (rate <= 0 || rate > 100)
                {
                    MessageBox.Show("Please Enter The Valid Rate", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                if (Convert.ToDateTime(dtpfrom.Text) > Convert.ToDateTime(dtpto.Text))
                {
                    MessageBox.Show("From date must be greater than the to date");
                    return;
                }


                #endregion

               //TradingBase objTradingbase = new TradingBase();

               //objTradingbase.sch_cat = DrpDwnLstSchemeCategory.SelectedValue.ToString();

               //if (DrpDwnLstSchemeType.SelectedIndex != -1)
               //{
               //    objTradingbase.sch_type = DrpDwnLstSchemeType.SelectedValue.ToString();
               //} 
           
               

              
                if (grdDefinition.RowCount > 0)
                {
                    foreach (DataGridViewRow dgvr in grdDefinition.Rows)
                    {
                        TradingBase objTradingbase = new TradingBase();
                        DataGridViewCheckBoxCell chk = dgvr.Cells[3] as DataGridViewCheckBoxCell;
                        if (Convert.ToBoolean(chk.Value) == true)
                        {
                            objTradingbase.isselect = dgvr.Cells["chkSC"].Value.ToString();
                            objTradingbase.sch_cat = DrpDwnLstSchemeCategory.SelectedValue.ToString();
                            objTradingbase.sch_code = dgvr.Cells["colCode"].Value.ToString();
                            objTradingbase.sch_term = dgvr.Cells["colTerm"].Value.ToString();


                            _lstTradingbase.Add(objTradingbase);
                        }

                       
                    }

                    if (_lstTradingbase.Count <= 0)
                    {
                        MessageBox.Show("Please select the scheme details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                }


                    foreach (var itm in _lstTradingbase)
                    {
                        TradingInterest objTradingInterest = new TradingInterest();
                        objTradingInterest.mti_sch_cat = itm.sch_cat;
                        objTradingInterest.mti_sch_trm = Convert.ToInt32(itm.sch_term);
                        objTradingInterest.mti_sch_cd = itm.sch_code;

                objTradingInterest.mti_frm = Convert.ToDateTime(dtpfrom.Text);
                objTradingInterest.mti_to = Convert.ToDateTime(dtpto.Text);
                objTradingInterest.mti_itm = txtItemcode.Text.Trim() == null || txtItemcode.Text.Trim() == "" ? "" : txtItemcode.Text.Trim();
                objTradingInterest.mti_cat1 = txtMaincat.Text.Trim() == null || txtMaincat.Text.Trim() == "" ? "" : txtMaincat.Text.Trim();
                objTradingInterest.mti_cat2 = txtCategory2.Text.Trim() == null || txtCategory2.Text.Trim() == "" ? "" : txtCategory2.Text.Trim();
                objTradingInterest.mti_cat3 = txtCategory3.Text.Trim() == null || txtCategory3.Text.Trim() == "" ? "" : txtCategory3.Text.Trim();
                objTradingInterest.mti_rt = rate;
                        objTradingInterest.mti_com = BaseCls.GlbUserComCode;
                        objTradingInterest.mti_cre_by = BaseCls.GlbUserID;
                        objTradingInterest.mti_act = 1;
                _lstTradingInterest.Add(objTradingInterest);

                    }

                dgvTradingdteails.AutoGenerateColumns = false;
                dgvTradingdteails.DataSource = _lstTradingInterest;
                
                    CommonUIValidation.TextBoxvalidation(new TextBox[] { txtItemcode, txtMaincat, txtCategory2, txtCategory3, txtRate }, CommonUIValidation.validationType.Clear);


                    

            }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTradingdteails.Rows.Count > 0)
                {
                    if (MessageBox.Show("Are you sure to save?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

                    string _error = "";
                    int result = CHNLSVC.Financial.SaveTradingInterestDetails(_lstTradingInterest, out _error);

                    if (result == -1)
                    {

                        MessageBox.Show("Error occurred while processing\n" + _error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Record Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


                        if (_lstTradingInterest.Count > 0)
                        {
                            _lstTradingInterest.Clear();
                            dgvTradingdteails.DataSource = null;
                            dgvTradingdteails.Rows.Clear();

                            grdDefinition.DataSource = null;
                            grdDefinition.Rows.Clear();
                            _lstTradingbase = new List<TradingBase>();
                            chkAll.Checked = false;
                            chkAllgrid.Checked = false;
                        }

                       
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnClear_Click_1(object sender, EventArgs e)
        {
            try
            {
                CommonUIValidation.TextBoxvalidation(new TextBox[] { txtItemcode, txtMaincat, txtCategory2, txtCategory3, txtSchemeTerm }, CommonUIValidation.validationType.Clear);
                dgvTradingdteails.DataSource = null;
                dgvTradingdteails.Rows.Clear();
                _lstTradingInterest = new List<TradingInterest>();
                grdDefinition.DataSource = null;
                grdDefinition.Rows.Clear();
                _lstTradingbase = new List<TradingBase>();
                chkAll.Checked = false;
                chkAllgrid.Checked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure to View?", "View", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

                _lstTradingInterest = new List<TradingInterest>();
                dgvTradingdteails.DataSource = null;

                DateTime frmdate = dtpfrom.Value.Date;
                DateTime todate = dtpto.Value.Date;

                DataTable dt = CHNLSVC.Financial.GetTradingdetails(frmdate, todate);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TradingInterest objTradingInterest = new TradingInterest();
                    objTradingInterest.mti_com = dt.Rows[i]["mti_com"].ToString();
                    objTradingInterest.mti_sch_cd = dt.Rows[i]["mti_sch_cd"].ToString();
                    objTradingInterest.mti_sch_cat = dt.Rows[i]["mti_sch_cat"].ToString();
                    objTradingInterest.mti_itm = dt.Rows[i]["mti_itm"].ToString();
                    objTradingInterest.mti_cat1 = dt.Rows[i]["mti_cat1"].ToString();
                    objTradingInterest.mti_cat2 = dt.Rows[i]["mti_cat2"].ToString();
                    objTradingInterest.mti_cat3 = dt.Rows[i]["mti_cat3"].ToString();
                    objTradingInterest.mti_rt = Convert.ToDecimal(dt.Rows[i]["mti_rt"].ToString());
                    objTradingInterest.mti_frm = Convert.ToDateTime(dt.Rows[i]["mti_frm"].ToString());
                    objTradingInterest.mti_to = Convert.ToDateTime(dt.Rows[i]["mti_to"].ToString());
                    objTradingInterest.mti_sch_trm = Convert.ToInt32(dt.Rows[i]["MTI_SCH_TRM"].ToString());

                    _lstTradingInterest.Add(objTradingInterest);
                }

                if (dt.Rows.Count > 0)
                {
                    //CommonUIValidation.TextBoxvalidation(new TextBox[] { txtLocation, txtItemCat1, txtItemCat2, txtItemCat3, txtBrand, txtItemstatus }, CommonUIValidation.validationType.Clear);                
                    dgvTradingdteails.AutoGenerateColumns = false;
                    dgvTradingdteails.DataSource = _lstTradingInterest;
                }
                else
                {
                    MessageBox.Show("No records found", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information); CHNLSVC.CloseChannel();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            DrpDwnLstSchemeType.SelectedIndex = -1;
        }

        private void DrpDwnLstSchemeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if( DrpDwnLstSchemeType.SelectedIndex == -1)
            {
                chkAll.Checked = false;
            }
            
        }

        private void btnAll_Schemes_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grdDefinition.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[3];
                    chk.Value = true;
                }
                grdDefinition.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnNon_Schemes_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grdDefinition.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[3];
                    chk.Value = false;
                }
                grdDefinition.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void chkAllgrid_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllgrid.Checked == true)
            {
                this.btnAll_Schemes_Click(null, null);
            }
            else
            {
                this.btnNon_Schemes_Click(null, null);
            }
        }

        private void btnGridclear_Click(object sender, EventArgs e)
        {
            if(grdDefinition.RowCount > 0)
            {
                grdDefinition.Rows.Clear();
            }
        }


    }
}
