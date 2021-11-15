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

namespace FF.WindowsERPClient.Inventory
{
    //Tharindu 2017-11-22
    public partial class WareHouseItemSetup : Base
    {
        #region Global Declration

        string com = BaseCls.GlbUserComCode;
        WarehseItemSetup objlst = new WarehseItemSetup();
        List<WarehseItemSetup> _lstwarehouseitem = new List<WarehseItemSetup>();
        #endregion

        #region Constructors
        public WareHouseItemSetup()
        {
            InitializeComponent();
            cmbDefType.SelectedIndex = 1;
        }
        #endregion

        #region Events

        #region Location
        private void btn_Srch_Loc_Click(object sender, EventArgs e)
        {
            try
            {
                txtLocation_MouseDoubleClick(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void txtLocation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    txtLocation_MouseDoubleClick(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void txtLocation_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (com == "")
                {
                    MessageBox.Show("Company Not Connected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Get_LOC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtLocation;
                
                _CommonSearch.ShowDialog();
                txtLocation.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

           

        }
        #endregion

        #region ItemCode1
        private void txtItemCat1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    txtItemCat1_MouseDoubleClick(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void txtItemCat1_Leave(object sender, EventArgs e)
        {


            try
            {

                if (!string.IsNullOrEmpty(txtItemCat1.Text))

                {

                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    DataTable _categoryDet = CHNLSVC.General.GetMainCategoryDetail(txtItemCat1.Text);
                    _CommonSearch.dvResult.DataSource = _categoryDet;
                    if (_categoryDet.Rows.Count == 0)

                    {
                        MessageBox.Show("Invalid Main category", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtItemCat1.Text = "";
                        LbLMainCat.Text = "";
                        txtItemCat1.Focus();
                        return;
                    }
                    else
                    {
                        DataTable _cat1 = CHNLSVC.General.GetMainCategoryDetail(txtItemCat1.Text);
                        LbLMainCat.Text = _cat1.Rows[0]["ric1_desc"].ToString();

                    }
                    
                }

                else
                    {
                        LbLMainCat.Text = "";

                    }




                if (string.IsNullOrEmpty(txtItemCat1.Text))
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
        private void txtItemCat1_MouseDoubleClick(object sender, MouseEventArgs e)
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
                _CommonSearch.obj_TragetTextBox = txtItemCat1;
                _CommonSearch.txtSearchbyword.Text = txtItemCat1.Text;

                _CommonSearch.ShowDialog();
                txtItemCat1.Focus();
                btn_Srch_Cat2.Enabled = true;
                txtItemCat2.Text = "";
                txtItemCat3.Text = "";

                

            }



            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


         

           
        }
        #endregion

        #region ItemCode2
        private void txtItemCat2_KeyDown(object sender, KeyEventArgs e)
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

        private void txtItemCat2_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtItemCat2.Text))
                {
                    DataTable _cat2 = CHNLSVC.Sales.GetItemSubCate2(txtItemCat1.Text, txtItemCat2.Text);
                    if (_cat2.Rows.Count == 0)
                    {
                        MessageBox.Show("Invalid Sub Category", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtItemCat2.Text = "";
                        LblSubCat.Text = "";
                        txtItemCat2.Focus();
                        return;
                    }
                    else
                    {
                        //DataTable _cat2 = CHNLSVC.Sales.GetItemSubCate2(txtItemCat1.Text, txtItemCat2.Text);
                        LblSubCat.Text = _cat2.Rows[0]["ric2_desc"].ToString();
                    }
                }
                else
                {
                    LblSubCat.Text = "";
                }

                if (string.IsNullOrEmpty(txtItemCat2.Text))
                    btn_Srch_Cat3.Enabled = false;
                else
                    btn_Srch_Cat3.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


            
        }

        private void txtItemCat2_MouseDoubleClick(object sender, MouseEventArgs e)
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
                if (string.IsNullOrEmpty(txtItemCat1.Text))
                {
                    MessageBox.Show("Select Main Catergory", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItemCat2;
                _CommonSearch.txtSearchbyword.Text = txtItemCat2.Text;
                _CommonSearch.ShowDialog();
                txtItemCat2.Focus();
                btn_Srch_Cat3.Enabled = true;
                txtItemCat3.Text = "";

              

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region ItemCode3
        private void txtItemCat3_KeyDown(object sender, KeyEventArgs e)
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

        private void txtItemCat3_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtItemCat3.Text))
                {
                    DataTable _cat3 = CHNLSVC.Sales.GetItemSubCate3(txtItemCat1.Text, txtItemCat2.Text, txtItemCat3.Text);
                    if (_cat3.Rows.Count == 0)
                    {
                        MessageBox.Show("Invalid Item Range", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtItemCat3.Text = "";
                        LblRange.Text = "";
                        txtItemCat3.Focus();
                        return;
                    }
                    else
                    {
                        //DataTable _cat3 = CHNLSVC.Sales.GetItemSubCate3(txtItemCat1.Text, txtItemCat2.Text, txtItemCat3.Text);
                        LblRange.Text = _cat3.Rows[0]["ric2_desc"].ToString();
                    }
                   
                }
                else
                {
                    LblRange.Text = "";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }

            

        }

        private void txtItemCat3_MouseDoubleClick(object sender, MouseEventArgs e)
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
                if (string.IsNullOrEmpty(txtItemCat1.Text))
                {
                    MessageBox.Show("Select Main Category", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (string.IsNullOrEmpty(txtItemCat2.Text))
                {
                    MessageBox.Show("Select Sub category", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItemCat3;
                _CommonSearch.txtSearchbyword.Text = txtItemCat3.Text;
                _CommonSearch.ShowDialog();
                txtItemCat3.Focus();

               

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
        }
        #endregion

        #region Brand
        private void txtBrand_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    btn_Srch_Brand_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }

        }

        private void txtBrand_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                btn_Srch_Brand_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }

        }

        private void btn_Srch_Brand_Click(object sender, EventArgs e)
        {
            try
            {

               
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtBrand;

                _CommonSearch.txtSearchbyword.Text = txtBrand.Text;
                _CommonSearch.ShowDialog();
                txtBrand.Focus();


               


            }

         


            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }


          
        }
        #endregion

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
                        paramsText.Append(txtItemCat1.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        paramsText.Append(txtItemCat1.Text + seperator + txtItemCat2.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub2.ToString() + seperator + "" + seperator);
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
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location:
                    {
                        paramsText.Append(com + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location.ToString() + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion

        private void cmbDefType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbDefType.SelectedIndex == 0)
                {
                    txtItemstatus.Enabled = false;
                    btn_scrh_itemstatus.Enabled = false;
                }
                else
                {
                    txtItemstatus.Enabled = true;
                    btn_scrh_itemstatus.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                #region Validation
                if (txtLocation.Text == "" || txtLocation.Text == string.Empty)
                {
                    MessageBox.Show("Location Cannot be empty", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (txtItemCat1.Text == "" || txtItemCat1.Text == string.Empty)
                {
                    MessageBox.Show("Item category 1 Cannot be empty", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                #endregion

                if (dgvItemSetup.DataSource != null)
                {
                    dgvItemSetup.DataSource = null;
                }

                objlst = new WarehseItemSetup();
                objlst.definitionType = cmbDefType.SelectedItem.ToString();
                objlst.location = txtLocation.Text.Trim() == null || txtLocation.Text.Trim() == "" ? "N/A" : txtLocation.Text.Trim();
                objlst.itemCat1 = txtItemCat1.Text.Trim() == null || txtItemCat1.Text.Trim() == "" ? "N/A" : txtItemCat1.Text.Trim();
                objlst.itemCat2 = txtItemCat2.Text.Trim() == null || txtItemCat2.Text.Trim() == "" ? "N/A" : txtItemCat2.Text.Trim();
                objlst.itemCat3 = txtItemCat3.Text.Trim() == null || txtItemCat3.Text.Trim() == "" ? "N/A" : txtItemCat3.Text.Trim();
                objlst.brand = txtBrand.Text.Trim() == null || txtBrand.Text.Trim() == "" ? "N/A" : txtBrand.Text.Trim();
                objlst.itemStatus = txtItemstatus.Text.Trim() == null || txtItemstatus.Text.Trim() == "" ? "N/A" : txtItemstatus.Text.Trim();
                objlst.tostatus = tostatustxt.Text.Trim() == null || tostatustxt.Text.Trim() == "" ? "N/A" : tostatustxt.Text.Trim();
               
               int status = 0;
                if (chbActive.Checked)
                {
                    status = 1;
                }
                //else
                //{
                //    status = 0;
                //}

                objlst.activeStatus = status;
                //   int val = objlst.activeStatus == "True" ? 1 : 0;
                // objlst.activeStatus = val.ToString();

                objlst.company = com;
                objlst.createuser = BaseCls.GlbUserID;
                _lstwarehouseitem.Add(objlst);

                dgvItemSetup.AutoGenerateColumns = false;
                dgvItemSetup.DataSource = _lstwarehouseitem;
                dgvItemSetup.Refresh();

                CommonUIValidation.TextBoxvalidation(new TextBox[] { txtLocation, txtItemCat1, txtItemCat2, txtItemCat3, txtBrand, txtItemstatus, tostatustxt }, CommonUIValidation.validationType.Clear);
              
                    LblLocationDes.Text = "";
                    LbLMainCat.Text = "";
                    LblSubCat.Text = "";
                    LblRange.Text = "";
                    BrandLBL.Text = "";
                    LBLStatus.Text = "";
                    tostatusLbl.Text = "";


            
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
        }

        private void dgvItemSetup_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_scrh_itemstatus_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemStatus);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanyItemStatusData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItemstatus;
                _CommonSearch.ShowDialog();
                txtItemstatus.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
        }

        private void txtLocation_Leave(object sender, EventArgs e)
        {
            try
            {
                
                if (string.IsNullOrEmpty(txtLocation.Text))
                {
                    MessageBox.Show("Please select location.", "Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LblLocationDes.Text = "";
                    return;
                }

                DataTable _tbl = CHNLSVC.Inventory.Get_location_by_code(com.Trim(), txtLocation.Text.Trim());
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    MessageBox.Show("Please select the valid location.", "Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtLocation.Clear();
                    return;
                }
                else
                {
                    //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    //LblLocationDes.Text = _CommonSearch.GetLoc_HIRC_SearchDesc(41, txtLocation.Text.ToUpper());


                    LblLocationDes.Text = _tbl.Rows[0]["ml_loc_desc"].ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }

            
        }

        private void txtBrand_Leave(object sender, EventArgs e)
        {
            try
            {
                 if (!string.IsNullOrEmpty(txtBrand.Text))
                {
                    DataTable _branddet = CHNLSVC.General.GetBrand(txtBrand.Text);
                    
                    if (_branddet.Rows.Count == 0)
                    {
                        MessageBox.Show("Invalid Brand Name", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtBrand.Text = "";
                        BrandLBL.Text = "";
                        txtBrand.Focus();
                        return;
                    }
                    else
                    {
                        BrandLBL.Text = _branddet.Rows[0]["MB_DESC"].ToString();

                    }
                }
                 else
                 {

                     BrandLBL.Text = "";
                 }
            }
             catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }

    

   
        }

        private void txtItemstatus_Leave(object sender, EventArgs e)
        {
            try
            {
                 if (!string.IsNullOrEmpty(txtItemstatus.Text))
                {
                    DataTable _itemstatus = CHNLSVC.General.GetItemStatus(txtItemstatus.Text);
                    
                    if (_itemstatus.Rows.Count == 0)
                    {
                        MessageBox.Show("Invalid Item Status", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtItemstatus.Text = "";
                        LBLStatus.Text = "";
                        txtItemstatus.Focus();
                        return;
                    }
              
                 else
                 {
                     LBLStatus.Text = _itemstatus.Rows[0]["MIS_DESC"].ToString();

                 }
                }
                 else
                 {
                     LBLStatus.Text = "";
                 }

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
                if (txtLocation.Text == "" && txtItemCat1.Text == "" && txtBrand.Text == "" && cmbDefType.SelectedIndex < -1)
                {
                    MessageBox.Show("Please fill a field/s to search", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion

                if (MessageBox.Show("Are you sure to View?", "View", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

                _lstwarehouseitem = new List<WarehseItemSetup>();
                dgvItemSetup.DataSource = null;

                DataTable dt = CHNLSVC.Inventory.Get_WarehouseItem(com, txtLocation.Text.Trim(), txtItemCat1.Text.Trim(), txtBrand.Text.Trim(), cmbDefType.SelectedItem.ToString());
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objlst = new WarehseItemSetup();
                    objlst.definitionType = dt.Rows[i]["DefinitionType"].ToString();
                    objlst.location = dt.Rows[i]["Location"].ToString();
                    objlst.itemCat1 = dt.Rows[i]["Category1"].ToString();
                    objlst.itemCat2 = dt.Rows[i]["Category2"].ToString();
                    objlst.itemCat3 = dt.Rows[i]["Category3"].ToString();
                    objlst.brand = dt.Rows[i]["Brand"].ToString();
                   
                    objlst.itemStatus = dt.Rows[i]["Itemstatus"].ToString();
                    objlst.tostatus = dt.Rows[i]["Tostatus"].ToString();
                    objlst.activeStatus = dt.Rows[i]["Active"].ToString() == null || dt.Rows[i]["Active"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[i]["Active"].ToString());


                 
                  

                    objlst.company = com;
                    objlst.createuser = BaseCls.GlbUserID;
                    
                    
                    
                    _lstwarehouseitem.Add(objlst);
                }

                if (dt.Rows.Count > 0)
                {
                   CommonUIValidation.TextBoxvalidation(new TextBox[] { txtLocation, txtItemCat1, txtItemCat2, txtItemCat3, txtBrand, txtItemstatus, tostatustxt }, CommonUIValidation.validationType.Clear);                
                    dgvItemSetup.AutoGenerateColumns = false;
                    dgvItemSetup.DataSource = _lstwarehouseitem;
                
                }
                else
                {
                    MessageBox.Show("No records found", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information); CHNLSVC.CloseChannel();
                }
               
                LblLocationDes.Text = "";
                LbLMainCat.Text = "";
                LblSubCat.Text = "";
                LblRange.Text = "";
                BrandLBL.Text = "";
                LBLStatus.Text = "";
                tostatusLbl.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

            try
            {
                LblLocationDes.Text = "";
                LbLMainCat.Text = "";
                LblSubCat.Text = "";
                LblRange.Text = "";
                BrandLBL.Text = "";
                LBLStatus.Text = "";


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            try
            {
                CommonUIValidation.TextBoxvalidation(new TextBox[] { txtLocation, txtItemCat1, txtItemCat2, txtItemCat3, txtBrand, txtItemstatus }, CommonUIValidation.validationType.Clear);
                dgvItemSetup.DataSource = null;
                dgvItemSetup.Rows.Clear();
                _lstwarehouseitem = new List<WarehseItemSetup>();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<WarehseItemSetup> _lstwarehouseitemnew = new List<WarehseItemSetup>();
            try                    
            {
                if (dgvItemSetup.Rows.Count > 0)
                {
                    if (MessageBox.Show("Are you sure to save?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;


                    dgvItemSetup.EndEdit();
                    foreach (DataGridViewRow dgvr in dgvItemSetup.Rows)
                    {
                       DataGridViewCheckBoxCell chk = dgvr.Cells[8] as DataGridViewCheckBoxCell;
                       if (Convert.ToBoolean(chk.Value) == true)
                        {


                            _lstwarehouseitem.Where(r => r.definitionType == Convert.ToString(dgvr.Cells["dgclmDeftype"].Value) && r.location == Convert.ToString(dgvr.Cells["dgcolLoc"].Value) && r.itemCat1 == Convert.ToString(dgvr.Cells["dgcolCat1"].Value) && r.itemCat2 == Convert.ToString(dgvr.Cells["dgcolCat2"].Value) && r.itemCat3 == Convert.ToString(dgvr.Cells["dgcolCat3"].Value) && r.brand == Convert.ToString(dgvr.Cells["dgcolBrand"].Value) && r.itemStatus == Convert.ToString(dgvr.Cells["dgcolItemstatus"].Value) && r.tostatus == Convert.ToString(dgvr.Cells["dgtostatus"].Value)).ToList()
                                .ForEach(i =>
                                {
                                    i.activeStatus = 1;
                                });
                        }
                        else
                        {

                            _lstwarehouseitem.Where(r => r.definitionType == Convert.ToString(dgvr.Cells["dgclmDeftype"].Value) && r.location == Convert.ToString(dgvr.Cells["dgcolLoc"].Value) && r.itemCat1 == Convert.ToString(dgvr.Cells["dgcolCat1"].Value) && r.itemCat2 == Convert.ToString(dgvr.Cells["dgcolCat2"].Value) && r.itemCat3 == Convert.ToString(dgvr.Cells["dgcolCat3"].Value) && r.brand == Convert.ToString(dgvr.Cells["dgcolBrand"].Value) && r.itemStatus == Convert.ToString(dgvr.Cells["dgcolItemstatus"].Value) && r.tostatus == Convert.ToString(dgvr.Cells["dgtostatus"].Value)).ToList()
                                .ForEach(i =>
                                {
                                    i.activeStatus = 0;
                                });
                        }
                      
                     



                    }




                    string _error = "";
                    int result = CHNLSVC.Inventory.SaveWarehouseItem(_lstwarehouseitem, out _error);

                    if (result == -1)
                    {

                        MessageBox.Show("Error occurred while processing\n" + _error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Record Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CommonUIValidation.TextBoxvalidation(new TextBox[] { txtLocation, txtItemCat1, txtItemCat2, txtItemCat3, txtBrand }, CommonUIValidation.validationType.Clear);

                        if (_lstwarehouseitem.Count > 0)
                        {
                            _lstwarehouseitem.Clear();
                            dgvItemSetup.DataSource = null;
                            dgvItemSetup.Rows.Clear();
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemStatus);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanyItemStatusData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = tostatustxt;
                _CommonSearch.ShowDialog();
                tostatustxt.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
        }

        private void tostatustxt_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(tostatustxt.Text))
                    
                {
                    
                    DataTable _itemstatus = CHNLSVC.General.GetItemStatus(tostatustxt.Text);

                    if (_itemstatus.Rows.Count == 0)
                    {
                        MessageBox.Show("Invalid Item Status", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        tostatustxt.Text = "";
                        tostatusLbl.Text = "";
                        tostatustxt.Focus();
                        return;
                    }

                    else
                    {
                        tostatusLbl.Text = _itemstatus.Rows[0]["MIS_DESC"].ToString();

                    }
                }
                else
                {
                    tostatusLbl.Text = "";

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
        }

        private void LblSubCat_Click(object sender, EventArgs e)
        {

        }

        private void dgvItemSetup_CellClick(object sender, DataGridViewCellEventArgs e)
        {

          
        
        }

        private void WareHouseItemSetup_Load(object sender, EventArgs e)
        {

        }

        private void chbActive_CheckedChanged(object sender, EventArgs e)
        {

        }

        

        

       

    }
}
