using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects.InventoryNew;
using FF.WindowsERPClient.UtilityClasses;
using System.Data.OleDb;
using System.Configuration;
using System.Text.RegularExpressions;

namespace FF.WindowsERPClient.Inventory
{
    public partial class ProductReferenceDefinition : Base
    {

        #region Global Declration
        string com = BaseCls.GlbUserComCode;
        Production_ref objlst = new Production_ref();
        List<Production_ref> _lstProductionitm = new List<Production_ref>();
        #endregion

        #region Constructors
        public ProductReferenceDefinition()
        {
            InitializeComponent();
        }
        #endregion

        #region Events

        #region ItemCode1
        private void txtCat1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    btn_Srch_Cat1_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtCat1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                btn_Srch_Cat1_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtCat1_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCat1.Text))
                {
                    DataTable _categoryDet = CHNLSVC.General.GetMainCategoryDetail(txtCat1.Text);
                    if (_categoryDet.Rows.Count == 0)
                    {
                        MessageBox.Show("Invalid Item category 1", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtCat1.Text = "";
                        txtCat1.Focus();
                        return;
                    }
                }
                if (string.IsNullOrEmpty(txtCat1.Text))
                {
                    btn_Srch_Cat2.Enabled = false;
                    btn_Srch_Cat3.Enabled = false;
                }
                else
                {
                    btn_Srch_Cat2.Enabled = true;
                    btn_Srch_Cat3.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_Srch_Cat1_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCat1;
                _CommonSearch.txtSearchbyword.Text = txtCat1.Text;
                _CommonSearch.ShowDialog();
                txtCat1.Focus();
                btn_Srch_Cat2.Enabled = true;
                txtCat2.Text = "";
                txtCat3.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region ItemCode2
        private void txtCat2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    btn_Srch_Cat2_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtCat2_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCat2.Text))
                {
                    DataTable _cat2 = CHNLSVC.Sales.GetItemSubCate2(txtCat1.Text, txtCat2.Text);
                    if (_cat2.Rows.Count == 0)
                    {
                        MessageBox.Show("Invalid Item category 2", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtCat2.Text = "";
                        txtCat2.Focus();
                        return;
                    }
                }

                if (string.IsNullOrEmpty(txtCat2.Text))
                    btn_Srch_Cat3.Enabled = false;
                else
                    btn_Srch_Cat3.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtCat2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                btn_Srch_Cat2_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_Srch_Cat2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCat1.Text))
                {
                    MessageBox.Show("Select Item category 1", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCat2;
                _CommonSearch.txtSearchbyword.Text = txtCat2.Text;
                _CommonSearch.ShowDialog();
                txtCat2.Focus();
                btn_Srch_Cat3.Enabled = true;
                txtCat3.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region ItemCode3
        private void txtCat3_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    btn_Srch_Cat3_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
        }

        private void txtCat3_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCat3.Text))
                {
                    DataTable _cat3 = CHNLSVC.Sales.GetItemSubCate3(txtCat1.Text, txtCat2.Text, txtCat3.Text);
                    if (_cat3.Rows.Count == 0)
                    {
                        MessageBox.Show("Invalid Item category 3", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtCat3.Text = "";
                        txtCat3.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
        }

        private void txtCat3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                btn_Srch_Cat3_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
        }

        private void btn_Srch_Cat3_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCat1.Text))
                {
                    MessageBox.Show("Select Item category 1", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (string.IsNullOrEmpty(txtCat2.Text))
                {
                    MessageBox.Show("Select Item category 2", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCat3;
                _CommonSearch.txtSearchbyword.Text = txtCat3.Text;
                _CommonSearch.ShowDialog();
                txtCat2.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
        }
        #endregion

        #region Code
        private void txtCode_Leave(object sender, EventArgs e)
        {
            try
            {
                txtDescription.Enabled = true;
                if (!string.IsNullOrEmpty(txtCode.Text))
                {
                    //  DataTable _codeDes = CHNLSVC.Inventory.Get_Description(txtCode.Text.Trim());
                    DataTable _codeDes = CHNLSVC.Inventory.Get_Product_refernce(txtCode.Text.Trim(), "", "", "", com, 1);
                    if (_codeDes.Rows.Count > 0)
                    {
                        if (_codeDes.Rows[0]["RCT_DESC"] != DBNull.Value)
                        {
                            txtDescription.Text = "";
                            txtDescription.Text = _codeDes.Rows[0]["RCT_DESC"].ToString();
                            txtCharge.Focus();
                            txtDescription.Enabled = false;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_Srch_Code_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Ref_code);
                DataTable _result = CHNLSVC.CommonSearch.Get_RefernceTypes(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCode;
                _CommonSearch.txtSearchbyword.Text = txtCode.Text;
                _CommonSearch.ShowDialog();
                txtCode.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    btn_Srch_Code_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtCode_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                btn_Srch_Code_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion
        private void txtCharge_KeyDown(object sender, KeyEventArgs e)
        {

        }

        #endregion

        #region SubMethods
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
                        paramsText.Append(txtCat1.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        paramsText.Append(txtCat1.Text + seperator + txtCat2.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
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
                case CommonUIDefiniton.SearchUserControlType.Ref_code:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }
        private void btnSearchExclpath_Click(object sender, EventArgs e)
        {

            try
            {
                txtExeclUpload.Text = string.Empty;
                openFileDialog1.InitialDirectory = @"C:\";
                openFileDialog1.Filter = "txt files (*.xls)|*.xls,*.xlsx|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.FileName = string.Empty;
                openFileDialog1.ShowDialog();
                string[] _obj = openFileDialog1.FileName.Split('\\');
                txtExeclUpload.Text = openFileDialog1.FileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
        }
        #endregion

        #region DDL Methods
        private void btnAddtogrid_Click(object sender, EventArgs e)
        {
            try
            {

                #region Validation
                if (txtCode.Text == "" || txtCode.Text == string.Empty)
                {
                    MessageBox.Show("Code Cannot be empty", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (txtDescription.Text == "" || txtDescription.Text == string.Empty)
                {
                    MessageBox.Show("Description Cannot be empty", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (txtCharge.Text == "" || txtCharge.Text == string.Empty)
                {
                    MessageBox.Show("Charge Cannot be empty", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (txtCat1.Text == "" || txtCat1.Text == string.Empty)
                {
                    MessageBox.Show("Category 1 Cannot be empty", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Match match = Regex.Match(txtCharge.Text, "^\\d+(\\.\\d+)?$");
                if (!match.Success)
                {
                    MessageBox.Show("Please Input valid numbers", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if(txtCode.TextLength > 5)
                {
                    MessageBox.Show("Code length cannot be greater than five", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                #endregion

                if (dgvProductRef.DataSource != null)
                {
                    dgvProductRef.DataSource = null;

                }

                objlst = new Production_ref();
                objlst.code = txtCode.Text.Trim() == null || txtCode.Text.Trim() == "" ? "N/A" : txtCode.Text.Trim();
                objlst.description = txtDescription.Text.Trim() == null || txtDescription.Text.Trim() == "" ? "0" : txtDescription.Text.Trim();
                objlst.charge = txtCharge.Text.Trim() == null || txtCharge.Text.Trim() == "" ? "N/A" : txtCharge.Text.Trim();
                objlst.itemCat1 = txtCat1.Text.Trim() == null || txtCat1.Text.Trim() == "" ? "N/A" : txtCat1.Text.Trim();
                objlst.itemCat2 = txtCat2.Text.Trim() == null || txtCat2.Text.Trim() == "" ? "N/A" : txtCat2.Text.Trim();
                objlst.itemCat3 = txtCat3.Text.Trim() == null || txtCat3.Text.Trim() == "" ? "N/A" : txtCat3.Text.Trim();

                int status = 0;
                if (chkIsActive.Checked)
                {
                    status = 1;
                }

                int intsetup = 0;
                if (chkIntSetup.Checked)
                {
                    intsetup = 1;
                }

                objlst.isactive = status;
                objlst.intialsetup = intsetup;

                objlst.company = com;
                objlst.createuser = BaseCls.GlbUserID;
                _lstProductionitm.Add(objlst);

                dgvProductRef.AutoGenerateColumns = false;
                dgvProductRef.DataSource = _lstProductionitm;

                CommonUIValidation.TextBoxvalidation(new TextBox[] { txtCode, txtDescription, txtCharge, txtCat1, txtCat2, txtCat3 }, CommonUIValidation.validationType.Clear);
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
                #region Validation
                if (txtCode.Text == "" && txtCat1.Text == "" && txtCat2.Text == "" && txtCat3.Text == "")
                {
                    MessageBox.Show("Please fill a field/s to search", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion

                if (MessageBox.Show("Are you sure to View?", "View", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

                DataTable dt = CHNLSVC.Inventory.Get_Product_refernce(txtCode.Text, txtCat1.Text, txtCat2.Text, txtCat3.Text, com, 2);

                _lstProductionitm = new List<Production_ref>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objlst = new Production_ref();
                    objlst.code = dt.Rows[i]["Code"].ToString();
                    objlst.description = dt.Rows[i]["Description"].ToString();
                    objlst.charge = dt.Rows[i]["Charge"].ToString();
                    objlst.itemCat1 = dt.Rows[i]["Category1"].ToString();
                    objlst.itemCat2 = dt.Rows[i]["Category2"].ToString();
                    objlst.itemCat3 = dt.Rows[i]["Category3"].ToString();
                    objlst.isactive = Convert.ToInt32(dt.Rows[i]["Active"].ToString());
                    objlst.intialsetup = Convert.ToInt32(dt.Rows[i]["IntialSetup"].ToString());

                    _lstProductionitm.Add(objlst);
                }

                if (dt.Rows.Count > 0)
                {
                    CommonUIValidation.TextBoxvalidation(new TextBox[] { txtCode, txtDescription, txtCharge, txtCat1, txtCat2, txtCat3 }, CommonUIValidation.validationType.Clear);
                    dgvProductRef.AutoGenerateColumns = false;
                    dgvProductRef.DataSource = _lstProductionitm;

                    //_lstProductionitm = new List<Production_ref>();
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            List<Production_ref> _Production_ref = new List<Production_ref>();
            try
            {
                if (txtDescription.Enabled == false)
                {
                    txtDescription.Enabled = true;
                }

                CommonUIValidation.TextBoxvalidation(new TextBox[] { txtCode, txtDescription, txtCharge, txtCat1, txtCat2, txtCat3, txtExeclUpload }, CommonUIValidation.validationType.Clear);

                if (dgvProductRef.Rows.Count > 0)
                {

                    if (dgvProductRef.DataSource != null)
                    {
                        _lstProductionitm = new List<Production_ref>();
                        dgvProductRef.DataSource = _lstProductionitm;
                    }
                    else
                    {
                        dgvProductRef.Rows.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvProductRef.Rows.Count > 0)
                {
                    if (MessageBox.Show("Are you sure to save?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

                    _lstProductionitm = new List<Production_ref>();

                    foreach (DataGridViewRow dgvr in dgvProductRef.Rows)
                    {
                        objlst = new Production_ref();
                        objlst.code = dgvr.Cells["colCode"].Value.ToString();
                        objlst.description = dgvr.Cells["colDescription"].Value.ToString();
                        objlst.charge = dgvr.Cells["colCharge"].Value.ToString();
                        objlst.itemCat1 = dgvr.Cells["colCat1"].Value.ToString();
                        objlst.itemCat2 = dgvr.Cells["colCat2"].Value.ToString();
                        objlst.itemCat3 = dgvr.Cells["colCat3"].Value.ToString();
                            objlst.isactive = Convert.ToInt32(dgvr.Cells["ColActive"].Value.ToString());
                            objlst.intialsetup = Convert.ToInt32(dgvr.Cells["colIntsetup"].Value.ToString());

                        objlst.company = com;
                        objlst.createuser = BaseCls.GlbUserID;
                        _lstProductionitm.Add(objlst);

                    }

                    string _error = "";
                    int result = CHNLSVC.Inventory.SaveProductRefernce(_lstProductionitm, out _error);

                    if (result == -1)
                    {
                        MessageBox.Show("Error occurred while processing\n" + _error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Record Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CommonUIValidation.TextBoxvalidation(new TextBox[] { txtCode, txtDescription, txtCharge, txtCat1, txtCat2, txtCat3, txtExeclUpload }, CommonUIValidation.validationType.Clear);

                        if (_lstProductionitm.Count > 0)
                        {
                            _lstProductionitm.Clear();
                            dgvProductRef.DataSource = null;
                            dgvProductRef.Rows.Clear();
                        }

                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Please add records to grid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
        }


        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                # region validation

                if (string.IsNullOrEmpty(txtExeclUpload.Text))
                {
                    MessageBox.Show("Please select upload file path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtExeclUpload.Clear();
                    txtExeclUpload.Focus();
                    return;
                }

                System.IO.FileInfo fileObj = new System.IO.FileInfo(txtExeclUpload.Text);

                if (fileObj.Exists == false)
                {
                    MessageBox.Show("Selected file does not exist at the following path.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }
                #endregion

                #region open excel
                string Extension = fileObj.Extension;

                string conStr = "";

                if (Extension.ToUpper() == ".XLS")
                {

                    conStr = ConfigurationManager.ConnectionStrings["ConStringExcel03"]
                             .ConnectionString;
                }
                else if (Extension.ToUpper() == ".XLSX")
                {
                    conStr = ConfigurationManager.ConnectionStrings["ConStringExcel07"]
                              .ConnectionString;

                }


                conStr = String.Format(conStr, txtExeclUpload.Text, 0);
                OleDbConnection connExcel = new OleDbConnection(conStr);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable _dt = new DataTable();
                cmdExcel.Connection = connExcel;

                //Get the name of First Sheet
                connExcel.Open();
                DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                connExcel.Close();

                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(_dt);
                connExcel.Close();

                #endregion

                string code = "";
                string desc = "";
                decimal charge = 0;
                string cat1 = "";
                string cat2 = "";
                string cat3 = "";
                int isactive = 0;
                int intialsetup = 0;

                Int32 _currentRow = 0;

                StringBuilder _errorLst = new StringBuilder();
                if (_dt == null || _dt.Rows.Count <= 0) { MessageBox.Show("The excel file is empty. Please check the file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

                if (_dt.Rows.Count > 0)
                {
                    if (MessageBox.Show("Are you sure to Upload ?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No) return;

                    if (dgvProductRef.DataSource != null)
                    {
                        dgvProductRef.DataSource = null;

                    }

                    foreach (DataRow _dr in _dt.Rows)
                    {
                        _currentRow = _currentRow + 1;

                        code = _dr[0].ToString().Trim();
                        desc = _dr[1].ToString().Trim();
                        charge = _dr[2] == DBNull.Value ? 0 : Convert.ToDecimal(_dr[2]);
                        cat1 = _dr[3].ToString().Trim();
                        cat2 = _dr[4].ToString().Trim();
                        cat3 = _dr[5].ToString().Trim();
                        isactive = _dr[6] == DBNull.Value ? 0 : Convert.ToInt32(_dr[6]);
                        intialsetup = _dr[7] == DBNull.Value ? 0 : Convert.ToInt32(_dr[7]);

                        #region item validation

                        if (string.IsNullOrEmpty(code))
                        {
                            MessageBox.Show("Please enter code.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }

                        if (string.IsNullOrEmpty(desc.ToString()))
                        {
                            MessageBox.Show("Please enter desc.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }

                        if (!string.IsNullOrEmpty(_dr[3].ToString()))
                        {
                            DataTable _categoryDet = CHNLSVC.General.GetMainCategoryDetail(_dr[3].ToString());
                            if (_categoryDet.Rows.Count == 0)
                            {
                                MessageBox.Show("Invalid Item category1 Line No'" + _currentRow + "", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }

                        }

                        if (!string.IsNullOrEmpty(_dr[4].ToString()))
                        {
                            DataTable _cat2 = CHNLSVC.Sales.GetItemSubCate2(_dr[3].ToString(), _dr[4].ToString());
                            if (_cat2.Rows.Count == 0)
                            {
                                MessageBox.Show("Invalid Item category2 Line No'" + _currentRow + "", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }

                        if (!string.IsNullOrEmpty(_dr[5].ToString()))
                        {
                            DataTable _cat3 = CHNLSVC.Sales.GetItemSubCate3(_dr[3].ToString(), _dr[4].ToString(), _dr[5].ToString());
                            if (_cat3.Rows.Count == 0)
                            {
                                MessageBox.Show("Invalid Item category3 Line No'" + _currentRow + "", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }

                        #endregion


                        dgvProductRef.Rows.Add();

                        dgvProductRef["colCode", dgvProductRef.Rows.Count - 1].Value = code.Trim();
                        dgvProductRef["colDescription", dgvProductRef.Rows.Count - 1].Value = desc.Trim();
                        dgvProductRef["colCharge", dgvProductRef.Rows.Count - 1].Value = charge;
                        dgvProductRef["colCat1", dgvProductRef.Rows.Count - 1].Value = cat1.Trim();
                        dgvProductRef["colCat2", dgvProductRef.Rows.Count - 1].Value = cat2.Trim();
                        dgvProductRef["colCat3", dgvProductRef.Rows.Count - 1].Value = cat3.Trim();

                        dgvProductRef["ColActive", dgvProductRef.Rows.Count - 1].Value = Convert.ToInt32(isactive);
                        dgvProductRef["colIntsetup", dgvProductRef.Rows.Count - 1].Value = Convert.ToInt32(intialsetup);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
        }


        #endregion

        #region Other
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        #endregion

        private void txtCharge_Leave(object sender, EventArgs e)
        {

        }

        private void dgvProductRef_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // DataGridViewCheckBoxCell checkbox = dgvProductRef.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewCheckBoxCell;
            // checkbox = dgvProductRef.Rows[e.RowIndex].Cells[e.ColumnIndex];
            //DataGridViewTextBoxCell cellRate = YourDGV.Rows[e.RowIndex].Cells[rateColumnIndex];
            // Convert.ToInt32(dgvProductRef.CellValueChanged["ColActive"].Value.ToString());
            //_lstProductionitm.
        }

        private void dgvProductRef_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgvProductRef.Rows.Count > 0)
            //{
            //    string _itemCode = dgvProductRef.Rows[e.RowIndex].Cells["colCode"].Value == null ? string.Empty : dgvProductRef.Rows[e.RowIndex].Cells["colCode"].Value.ToString();

            //    int tmpFlag = 0;
            //    if (_lstProductionitm != null && _lstProductionitm.Count > 0)
            //    {
            //        if (dgvProductRef.Columns[e.ColumnIndex].Name == "ColActive")
            //        {
            //            object abc = dgvProductRef.Rows[e.RowIndex].Cells["ColActive"].Value;
                       
            //            if (abc)
            //            {
            //                tmpFlag = 1;
            //            }
            //            else
            //            { 
            //                tmpFlag = 0 ;
            //            }
            //            _lstProductionitm.Where(x => x.code == _itemCode).ToList().ForEach(y => y.isactive = tmpFlag);
            //        }
            //        else if (dgvProductRef.Columns[e.ColumnIndex].Name == "colIntsetup")
            //        {
            //            object cde = dgvProductRef.Rows[e.RowIndex].Cells["colIntsetup"].Value;

            //            if ((bool)dgvProductRef.Rows[e.RowIndex].Cells["colIntsetup"].Value)
            //            {
            //                tmpFlag = 1;
            //            }
            //            else
            //            {
            //                tmpFlag = 0;
            //            }
            //            _lstProductionitm.Where(x => x.code == _itemCode).ToList().ForEach(y => y.intialsetup = tmpFlag);
            //        }
            //    }
            //}
        }

        private void dgvProductRef_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (this.dgvProductRef.CurrentCell is DataGridViewCheckBoxCell)
             {
              this.dgvProductRef.EndEdit();
            }
        }



    }
}
