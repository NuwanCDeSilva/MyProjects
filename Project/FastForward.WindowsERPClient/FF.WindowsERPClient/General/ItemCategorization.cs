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
using System.Data.OleDb;
using System.Configuration;
using System.Text.RegularExpressions;

namespace FF.WindowsERPClient.General
{
    public partial class ItemCategorization : Base
    {
      
        List<REF_ITM_CATE1> _lstcate1 = new   List<REF_ITM_CATE1>();
        List<MasterItemSubCate> _lstcate2 = new List<MasterItemSubCate>();
        List<REF_ITM_CATE3> _lstcate3 = new List<REF_ITM_CATE3>();
        List<REF_ITM_CATE4> _lstcate4 = new List<REF_ITM_CATE4>();
        List<REF_ITM_CATE5> _lstcate5 = new List<REF_ITM_CATE5>();
        List<MasterItemBrand> _lstbrand = new List<MasterItemBrand>();
        List<MasterItemModel> _lstmodel = new List<MasterItemModel>();
        List<MasterUOM> _lstuom = new List<MasterUOM>();
        List<MasterColor> _lstclr = new List<MasterColor>();


        List<REF_ITM_CATE1> _lstcate1del = new List<REF_ITM_CATE1>();
        List<MasterItemSubCate> _lstcate2del = new List<MasterItemSubCate>();
        List<REF_ITM_CATE3> _lstcate3del = new List<REF_ITM_CATE3>();
        List<REF_ITM_CATE4> _lstcate4del = new List<REF_ITM_CATE4>();
        List<REF_ITM_CATE5> _lstcate5del = new List<REF_ITM_CATE5>();
        List<MasterItemBrand> _lstbranddel = new List<MasterItemBrand>();
        List<MasterItemModel> _lstmodeldel = new List<MasterItemModel>();
        List<MasterUOM> _lstuomdel = new List<MasterUOM>();
        List<MasterColor> _lstclrdel = new List<MasterColor>();

        Boolean _IsCat = false;

        string _cat1 = string.Empty;
        string _cat2 = string.Empty;
        string _cat3 = string.Empty;
        string _cat4 = string.Empty;


        Int32 _cat1tag = 0;
        Int32 _cat2tag = 0;
        Int32 _cat3tag = 0;
        Int32 _cat4tag = 0;
        Int32 _cat5tag = 0;
        Int32 _brandtag = 0;
        Int32 _uomtag = 0;
        Int32 _colortag = 0;
        Int32 _modeltag = 0;
        public ItemCategorization()
        {
            InitializeComponent();
          _lstcate1=  CHNLSVC.General.GetItemCate1();
          gvCate1.DataSource = null;
          gvCate1.AutoGenerateColumns = false;
          gvCate1.DataSource = new List<REF_ITM_CATE1>();
          gvCate1.DataSource = _lstcate1;

        }

        private void btnAddCate1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtCate1.Text))
            {
                MessageBox.Show("Enter Item Category", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtCate1Des.Text))
            {
                MessageBox.Show("Enter Item Category Description", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_lstcate1 != null)
            {
                REF_ITM_CATE1 result = _lstcate1.Find(x => x.Ric1_cd == txtCate1.Text);
                if (result != null)
                {
                    _cat1 = txtCate1.Text;
                    MessageBox.Show("This category already exist", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }
            else
            {
                 _lstcate1 = new List<REF_ITM_CATE1>();
            }

            REF_ITM_CATE1 _cate1= new REF_ITM_CATE1() ;
            _cate1.Ric1_cd=txtCate1.Text.Trim() ;
            _cate1.Ric1_desc = txtCate1Des.Text;
            _cate1.Ric1_act = true;
            _lstcate1.Add(_cate1);

            gvCate1.DataSource = null;
            gvCate1.AutoGenerateColumns = false;
            gvCate1.DataSource = new List<REF_ITM_CATE1>();
            gvCate1.DataSource = _lstcate1;
            _cat1 = txtCate1.Text;

            txtCate1.Text = "";
            txtCate1Des.Text = "";
            MessageBox.Show("Successfully Added", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _cat1tag = 1;
        }

        private void btnAddCate2_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtCate2.Text))
            {
                MessageBox.Show("Enter Item Category", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtCate2Des.Text))
            {
                MessageBox.Show("Enter Item Category Description", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(_cat1))
            {
                MessageBox.Show("Select Main Item Category  ", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_lstcate2 != null)
            { 

                MasterItemSubCate result = _lstcate2.Find(x => x.Ric2_cd == txtCate2.Text && x.Ric2_cd1 == _cat1);
                if (result != null)
                {
                    _cat2 = txtCate2.Text;
                    MessageBox.Show("This category already exist", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            
            else
            {
                _lstcate2 = new List<MasterItemSubCate>();
            }
            
            MasterItemSubCate _cate2 = new MasterItemSubCate();
            _cate2.Ric2_cd = txtCate2.Text.Trim();
            _cate2.Ric2_desc = txtCate2Des.Text;
            _cate2.Ric2_cd1 = _cat1;
            _cate2.Ric2_act = true;
            _lstcate2.Add(_cate2);

            gvCate2.DataSource = null;
            gvCate2.AutoGenerateColumns = false;
            gvCate2.DataSource = new List<MasterItemSubCate>();
            gvCate2.DataSource = _lstcate2;
            _cat2 = txtCate2.Text;
            txtCate2.Text = "";
            txtCate2Des.Text = "";
            MessageBox.Show("Successfully Added", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _cat2tag = 1;

        }

        private void btnAddCate3_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtCate3.Text))
            {
                MessageBox.Show("Enter Item Category", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtCate3Des.Text))
            {
                MessageBox.Show("Enter Item Category Description", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(_cat1))
            {
                MessageBox.Show("Select Main Item Category \\ Sub Item Category ", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(_cat2))
            {
                MessageBox.Show("Select Main Item Category \\ Sub Item Category  ", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_lstcate3 != null)
            {

                REF_ITM_CATE3 result = _lstcate3.Find(x => x.Ric3_cd == txtCate3.Text && x.Ric3_cd1 == _cat1 && x.Ric3_cd2 == _cat2);
                if (result != null)
                {
                    _cat3 = txtCate3.Text;
                    MessageBox.Show("This category already exist", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            else
            {
                _lstcate3 = new List<REF_ITM_CATE3>();
            }

            REF_ITM_CATE3 _cate3 = new REF_ITM_CATE3();
            _cate3.Ric3_cd = txtCate3.Text.Trim();
            _cate3.Ric2_desc = txtCate3Des.Text;
            _cate3.Ric3_cd1 = _cat1;
            _cate3.Ric3_cd2 = _cat2;
            _cate3.Ric2_act = true;
            _lstcate3.Add(_cate3);

            gvCate3.DataSource = null;
            gvCate3.AutoGenerateColumns = false;
            gvCate3.DataSource = new List<REF_ITM_CATE3>();
            gvCate3.DataSource = _lstcate3;
            _cat3 = txtCate3.Text;

            txtCate3.Text = "";
            txtCate3Des.Text = "";
            MessageBox.Show("Successfully Added", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _cat3tag = 1;
        }

        private void btnAddCate4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCate4.Text))
            {
                MessageBox.Show("Enter Item Category", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtCate4Des.Text))
            {
                MessageBox.Show("Enter Item Category Description", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(_cat1))
            {
                MessageBox.Show("Select Main Item Category \\Sub Item Category \\Item Range 1  ", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(_cat2))
            {
                MessageBox.Show("Select Main Item Category \\Sub Item Category \\Item Range 1   ", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(_cat3))
            {
                MessageBox.Show("Select Main Item Category \\Sub Item Category \\Item Range 1   ", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_lstcate4 != null)
            {

                REF_ITM_CATE4 result = _lstcate4.Find(x => x.Ric4_cd == txtCate4.Text && x.Ric4_cd1 == _cat1 && x.Ric4_cd2 == _cat2 && x.Ric4_cd3 == _cat3);
                if (result != null)
                {
                    _cat4 = txtCate4.Text;
                    MessageBox.Show("This category already exist", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                _lstcate4 = new List<REF_ITM_CATE4>();
            }

            REF_ITM_CATE4 _cate4 = new REF_ITM_CATE4();
            _cate4.Ric4_cd = txtCate4.Text.Trim();
            _cate4.Ric4_desc = txtCate4Des.Text;
            _cate4.Ric4_cd1 = _cat1;
            _cate4.Ric4_cd2 = _cat2;
            _cate4.Ric4_cd3 = _cat3;
            _cate4.Ric4_act = true;
            _lstcate4.Add(_cate4);

            gvCate4.DataSource = null;
            gvCate4.AutoGenerateColumns = false;
            gvCate4.DataSource = new List<REF_ITM_CATE4>();
            gvCate4.DataSource = _lstcate4;
            _cat4 = txtCate4.Text;
            txtCate4.Text = "";
            txtCate4Des.Text = "";
            MessageBox.Show("Successfully Added", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _cat4tag = 1;
        }

        private void btnAddCate5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCate5.Text))
            {
                MessageBox.Show("Enter Item Category", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtCate5Des.Text))
            {
                MessageBox.Show("Enter Item Category Description", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(_cat1))
            {
                MessageBox.Show("Main Item Category \\Sub Item Category \\Item Range 1 \\Item Range 2  ", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(_cat2))
            {
                MessageBox.Show("Main Item Category \\Sub Item Category \\Item Range 1 \\Item Range 2   ", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(_cat3))
            {
                MessageBox.Show("Main Item Category \\Sub Item Category \\Item Range 1 \\Item Range 2  ", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(_cat4))
            {
                MessageBox.Show("Main Item Category \\Sub Item Category \\Item Range 1 \\Item Range 2  ", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_lstcate5 != null)
            {
                REF_ITM_CATE5 result = _lstcate5.Find(x => x.Ric5_cd == txtCate5.Text && x.Ric5_cd1 == _cat1 && x.Ric5_cd2 == _cat2 && x.Ric5_cd3 == _cat3 && x.Ric5_cd4 == _cat4);
                if (result != null)
                {
                    MessageBox.Show("This category already exist", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                _lstcate5 = new List<REF_ITM_CATE5>();
            }
            REF_ITM_CATE5 _cate5 = new REF_ITM_CATE5();
            _cate5.Ric5_cd = txtCate5.Text.Trim();
            _cate5.Ric5_desc = txtCate5Des.Text;
            _cate5.Ric5_cd1 = _cat1;
            _cate5.Ric5_cd2 = _cat2;
            _cate5.Ric5_cd3 = _cat3;
            _cate5.Ric5_cd4 = _cat4;
            _cate5.Ric5_act = true;
            _lstcate5.Add(_cate5);

            gvCate5.DataSource = null;
            gvCate5.AutoGenerateColumns = false;
            gvCate5.DataSource = new List<REF_ITM_CATE5>();
            gvCate5.DataSource = _lstcate5;
           // _cat5 = txtCate5.Text;
            txtCate5.Text = "";
            txtCate5Des.Text = "";
            MessageBox.Show("Successfully Added", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _cat5tag = 1;
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
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ModelMaster:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {

                        paramsText.Append(txtCate1.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        paramsText.Append(txtCate1.Text + seperator + txtCate2.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.ItemBrand:
                    {
                         
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Model:
                    {
                        paramsText.Append("");
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.masterCat1:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.masterCat1.ToString() + seperator + "" + seperator + "CAT_Main" + seperator);

 
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterCat2:
                    {

                         if (_IsCat == true)
                        {
                            paramsText.Append(txtMainCat.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.masterCat2.ToString() + seperator + "" + seperator + "CAT_Sub1" + seperator);
                        }

                        else
                         {
                        paramsText.Append(txtCate1.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.masterCat2.ToString() + seperator + "" + seperator + "CAT_Sub1" + seperator);

                        }
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterCat3:
                    {
                         if (_IsCat == true)
                        {
                            paramsText.Append(txtMainCat.Text + seperator + txtCat1.Text + seperator + CommonUIDefiniton.SearchUserControlType.masterCat3.ToString() + seperator + "" + seperator + "CAT_Sub2" + seperator);
                        }
                        else
                         {
                        paramsText.Append(txtCate1.Text + seperator + txtCate2.Text + seperator + CommonUIDefiniton.SearchUserControlType.masterCat3.ToString() + seperator + "" + seperator + "CAT_Sub2" + seperator);
 
                        }
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterCat4:
                    {
                        if (_IsCat == true)
                        {
                            paramsText.Append(txtMainCat.Text + seperator + txtCat1.Text + seperator + txtCat2.Text + seperator + CommonUIDefiniton.SearchUserControlType.masterCat4.ToString() + seperator + "CAT_Sub3" + seperator);
                        }
                        else
                        {
                        paramsText.Append(txtCate1.Text + seperator + txtCate2.Text + seperator + txtCate3.Text + seperator + CommonUIDefiniton.SearchUserControlType.masterCat4.ToString() + seperator + "CAT_Sub3" + seperator);
                        }
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterCat5:
                    {
                        if (_IsCat == true)
                        {
                            paramsText.Append(txtMainCat.Text + seperator + txtCat1.Text + seperator + txtCat2.Text + seperator + txtCat3.Text + seperator + "CAT_Sub4" + seperator);
                        }
                        else
                        {                        
                        paramsText.Append(txtCate1.Text + seperator + txtCate2.Text + seperator + txtCate3.Text + seperator + txtCate4.Text + seperator + "CAT_Sub4" + seperator);
                       }
                        break;
                    }
                    case CommonUIDefiniton.SearchUserControlType.masterColor:
                    {
                        paramsText.Append("");
                        break;
                    }

                      case CommonUIDefiniton.SearchUserControlType.masterUOM:
                    {
                        paramsText.Append("");
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }
        private void btnCat1_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _IsCat = false;
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat1);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCate1;
            _CommonSearch.txtSearchbyword.Text = txtCate1.Text;
            _CommonSearch.ShowDialog();
            txtCate1.Focus();
        }

        private void btnCat2_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _IsCat = false;
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat2);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCate2;
            _CommonSearch.txtSearchbyword.Text = txtCate2.Text;
            _CommonSearch.ShowDialog();
            txtCate2.Focus();
        }

        private void btnCat3_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _IsCat = false;
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat3);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCate3;
            _CommonSearch.txtSearchbyword.Text = txtCate3.Text;
            _CommonSearch.ShowDialog();
            txtCate3.Focus();
        }

        private void btnSave1_Click(object sender, EventArgs e)
        {
            if (_lstcate1 == null)
            {
                MessageBox.Show("Select Main items to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }

            if (_lstcate1.Count == 0)
            {
                MessageBox.Show("Select Main items to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!string.IsNullOrEmpty(txtCate1.Text))
            {
                MessageBox.Show("Please Add Item Main Category", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!string.IsNullOrEmpty(txtCate1Des.Text))
            {
                MessageBox.Show("Please Add Item Main Category Description", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_cat1tag == 0)
            {
                MessageBox.Show("No any modificatoin or new record to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
           
            }
            


            string _err;
            int row_aff = CHNLSVC.General.SaveItemCate1(_lstcate1, _lstcate1del, out _err);
            if (row_aff == 1)
            {
                MessageBox.Show("Successfully Saved", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _cat1tag = 0;
            }
            else
            {
                MessageBox.Show("Terminate", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnSave2_Click(object sender, EventArgs e)
        {
            if (_lstcate2 == null)
            {
                MessageBox.Show("Select sub items to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }

            
            if (_lstcate2.Count == 0)
            {
                MessageBox.Show("Select sub items to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!string.IsNullOrEmpty(txtCate2.Text))
            {
                MessageBox.Show("Please add Sub category", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!string.IsNullOrEmpty(txtCate2Des.Text))
            {
                MessageBox.Show("Please add Sub category Description", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_cat2tag == 0)
            {
                MessageBox.Show("No any modificatoin or new record to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            string _err;
            int row_aff = CHNLSVC.General.SaveItemCate2(_lstcate2, _lstcate2del, out _err);
            if (row_aff == 1)
            {
                MessageBox.Show("Successfully Saved", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _cat2tag = 0;
            }
            else
            {
                MessageBox.Show("Terminate", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSave3_Click(object sender, EventArgs e)
        {
            if (_lstcate3 == null)
            {
                MessageBox.Show("Select Item Range 1 to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            
            if (_lstcate3.Count == 0)
            {
                MessageBox.Show("Select Item Range 1 to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!string.IsNullOrEmpty(txtCate3.Text))
            {
                MessageBox.Show("Please add item range 1", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!string.IsNullOrEmpty(txtCate3Des.Text))
            {
                MessageBox.Show("ls add item range 1 Description", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_cat3tag == 0)
            {
                MessageBox.Show("No any modificatoin or new record to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            string _err;
            int row_aff = CHNLSVC.General.SaveItemCate3(_lstcate3, _lstcate3del, out _err);
            if (row_aff == 1)
            {
                MessageBox.Show("Successfully Saved", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _cat3tag = 0;
            }
            else
            {
                MessageBox.Show("Terminate", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSave4_Click(object sender, EventArgs e)
        {
            if (_lstcate4 == null)
            {
                MessageBox.Show("Select Item Range 2 to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            if (!string.IsNullOrEmpty(txtCate4.Text))
            {
                MessageBox.Show("Please add item range 2", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!string.IsNullOrEmpty(txtCate4Des.Text))
            {
                MessageBox.Show("Please add item range 2Description", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_cat4tag == 0)
            {
                MessageBox.Show("No any modificatoin or new record to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            
            if (_lstcate4.Count == 0)
            {
                MessageBox.Show("Select Item Range 2 to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string _err;
            int row_aff = CHNLSVC.General.SaveItemCate4(_lstcate4, _lstcate4del, out _err);
            if (row_aff == 1)
            {
                MessageBox.Show("Successfully Saved", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _cat4tag = 0;
            }
            else
            {
                MessageBox.Show("Terminate", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSave5_Click(object sender, EventArgs e)
        {
            if (_lstcate5 == null)
            {
                MessageBox.Show("Select Item Range 3 to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            
            
            if (_lstcate5.Count == 0)
            {
                MessageBox.Show("Select Item Range 3 to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!string.IsNullOrEmpty(txtCate5.Text))
            {
                MessageBox.Show("Please add item rage 3", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!string.IsNullOrEmpty(txtCate5Des.Text))
            {
                MessageBox.Show("Please add item rage 3 Description", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_cat5tag == 0)
            {
                MessageBox.Show("No any modificatoin or new record to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            string _err;
            int row_aff = CHNLSVC.General.SaveItemCate5(_lstcate5, _lstcate5del, out _err);
            if (row_aff == 1)
            {
                MessageBox.Show("Successfully Saved", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _cat5tag = 0;
            }
            else
            {
                MessageBox.Show("Terminate", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvCate1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {  
        }

        private void gvCate2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void gvCate3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void gvCate4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnClear1_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("Are you sure want to clear?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                //gvCate1.DataSource = new List<REF_ITM_CATE1>();
            txtCate1.Text = "";
            txtCate1Des.Text = "";
            _cat1 = "";}
        }

        private void btnClear2_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("Are you sure want to clear?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                //gvCate2.DataSource = new List<MasterItemSubCate>();
            txtCate2.Text = "";
            txtCate2Des.Text = "";
            _cat1 = "";
            _cat2 = "";}
        }

        private void btnClear3_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("Are you sure want to clear?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                //gvCate3.DataSource = new List<REF_ITM_CATE3>();
            txtCate3.Text = "";
            txtCate3Des.Text = "";
            _cat1 = "";
            _cat2 = "";
            _cat3 = "";}
        }

        private void btnClear4_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("Are you sure want to clear?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                //gvCate4.DataSource = new List<REF_ITM_CATE4>();
            txtCate4.Text = "";
            txtCate4Des.Text = "";
            _cat1 = "";
            _cat2 = "";
            _cat3 = "";
            _cat4 = "";}
        }

        private void btnClear5_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("Are you sure want to clear?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                //gvCate5.DataSource = new List<REF_ITM_CATE5>();
            txtCate5.Text = "";
            txtCate5Des.Text = "";
            _cat1 = "";
            _cat2 = "";
            _cat3 = "";
            _cat4 = "";}
            
        }

        private void gvCate1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex ==3)
                {
                    _cat1tag = 1;
                }


                if (e.ColumnIndex == 0)
                {
                     
                        if (MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            string type = gvCate1.Rows[e.RowIndex].Cells[1].Value.ToString();

                            _lstcate1del.AddRange(_lstcate1.Where(x => x.Ric1_cd == type));

                            _lstcate1.RemoveAll(x => x.Ric1_cd == type);
                            BindingSource source = new BindingSource();
                            source.DataSource = _lstcate1;
                            gvCate1.DataSource = source;
                            _cat1tag = 1;

                        }
                    

                }
                else
                {
                    if (Convert.ToBoolean(gvCate1.Rows[e.RowIndex].Cells[3].Value.ToString()) == true)
                    {
                        _cat1 = gvCate1.Rows[e.RowIndex].Cells[1].Value.ToString();

                        _lstcate2 = CHNLSVC.General.GetItemCate2(_cat1);
                        gvCate2.DataSource = null;
                        gvCate2.AutoGenerateColumns = false;
                        gvCate2.DataSource = new List<MasterItemSubCate>();
                        gvCate2.DataSource = _lstcate2;
                    }
                }



            }
          
        }

        private void gvCate2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 3)
                {
                    _cat2tag = 1;
                }

                if (e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string type = gvCate2.Rows[e.RowIndex].Cells[1].Value.ToString();

                        _lstcate2del.AddRange(_lstcate2.Where(x => x.Ric2_cd == type));
                        _lstcate2.RemoveAll(x => x.Ric2_cd == type);
                        BindingSource source = new BindingSource();
                        source.DataSource = _lstcate2;
                        gvCate2.DataSource = source;
                        _cat2tag = 1;

                    }

                }
                else
                {


                    _cat2 = gvCate2.Rows[e.RowIndex].Cells[1].Value.ToString();

                    _lstcate3 = CHNLSVC.General.GetItemCate3(_cat1, _cat2);
                    gvCate3.DataSource = null;
                    gvCate3.AutoGenerateColumns = false;
                    gvCate3.DataSource = new List<REF_ITM_CATE3>();
                    gvCate3.DataSource = _lstcate3;
                }
            }
        }

        private void gvCate3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 3)
                {
                    _cat3tag = 1;
                }

                if (e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string type = gvCate3.Rows[e.RowIndex].Cells[1].Value.ToString();

                        _lstcate3del.AddRange(_lstcate3.Where(x => x.Ric3_cd == type));
                        _lstcate3.RemoveAll(x => x.Ric3_cd == type);
                        BindingSource source = new BindingSource();
                        source.DataSource = _lstcate3;
                        gvCate3.DataSource = source;
                        _cat3tag = 1;

                    }
                }

                else
                {

                    _cat3 = gvCate3.Rows[e.RowIndex].Cells[1].Value.ToString();
                    _lstcate4 = CHNLSVC.General.GetItemCate4(_cat1, _cat2, _cat3);
                    gvCate4.DataSource = null;
                    gvCate4.AutoGenerateColumns = false;
                    gvCate4.DataSource = new List<REF_ITM_CATE4>();
                    gvCate4.DataSource = _lstcate4;
                }
            }
        }

        private void gvCate4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)


            {

                if (e.ColumnIndex == 3)
                {
                    _cat4tag = 1;
                }
                if ( e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string type = gvCate4.Rows[e.RowIndex].Cells[1].Value.ToString();
                        _lstcate4del.AddRange(_lstcate4.Where(x => x.Ric4_cd == type));

                        _lstcate4.RemoveAll(x => x.Ric4_cd == type);
                        BindingSource source = new BindingSource();
                        source.DataSource = _lstcate4;
                        gvCate4.DataSource = source;
                        _cat4tag = 1;

                    }
                }

                else
                {
                    _cat4 = gvCate4.Rows[e.RowIndex].Cells[1].Value.ToString();

                    _lstcate5 = CHNLSVC.General.GetItemCate5(_cat1, _cat2, _cat3, _cat4);
                    gvCate5.DataSource = null;
                    gvCate5.AutoGenerateColumns = false;
                    gvCate5.DataSource = new List<REF_ITM_CATE4>();
                    gvCate5.DataSource = _lstcate5;
                }
            }
        }

        private void btnSearchModel_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ModelMaster);
             
            DataTable _result = CHNLSVC.CommonSearch.GetModelMaster(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtModel; //txtBox;
            _CommonSearch.ShowDialog();
            txtModel.Focus();
        }

        private void btnSearchBrand_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemBrand);
            DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtBrand;
            _CommonSearch.txtSearchbyword.Text = txtBrand.Text;
            _CommonSearch.ShowDialog();
            txtBrand.Focus();
        }

        private void txtBrand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBrandName.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                 btnSearchBrand_Click(null, null); 
            }

        }

        private void txtBrandName_KeyDown(object sender, KeyEventArgs e)
        {if (e.KeyCode == Keys.Enter)
            {
                btnAddBrand.Focus();
        }
        }

        private void txtBrand_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCate1Des.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnCat1_Click(null, null);
            }
        }

        private void txtCate1Des_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddCate1.Focus();
            }
            
        }

        private void txtCate2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCate2Des.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnCat2_Click(null, null);
            }
        }

        private void txtCate2Des_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddCate2.Focus();
            }
        }

        private void txtCate3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCate3Des.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnCat3_Click(null, null);
            }
        }

        private void txtCate3Des_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddCate3.Focus();
            }
        }

        private void txtCate4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCate4Des.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnCat4_Click(null, null);
            }
        }

        private void txtCate4Des_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddCate4.Focus();
            }
        }

        private void txtCate5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCate5Des.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnCat5_Click(null, null);
            }
        }

        private void txtCate5Des_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddCate5.Focus();
            }

        }

        private void txtCate1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCate1Des_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtModelDes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMainCat.Focus();
            }
        }

        private void txtModel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtModelDes.Focus();
            }

            else if (e.KeyCode == Keys.F2 )
            {
                btnSearchModel_Click(null, null);
            }
           
        }

        private void txtUOM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtUOMDes.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnSearchUOM_Click(null, null);
            }
        }

        private void txtUOMDes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddModel.Focus();
            }
            
        }

        private void txtColor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtColorDes.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnSrhClr_Click(null, null);
            }
        }

        private void txtColorDes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddColor.Focus();
            }
 
        }

        private void txtModel_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtModelDes_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBrandName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAddBrand_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBrand.Text))
            {
                MessageBox.Show("Enter brand", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtBrandName.Text))
            {
                MessageBox.Show("Enter Description", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            if (_lstbrand != null)
            {
                MasterItemBrand result = _lstbrand.Find(x => x.Mb_cd == txtBrand.Text);
                if (result != null)
                {
                    MessageBox.Show("This brand  already exist", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                _lstbrand = new List<MasterItemBrand>();
            }

            MasterItemBrand _brand = new MasterItemBrand();
            _brand.Mb_cd = txtBrand.Text;
            _brand.Mb_desc = txtBrandName.Text;
            _brand.Mb_act = true;
            _lstbrand.Add(_brand);

            gvBrand.DataSource = null;
            gvBrand.AutoGenerateColumns = false;
            gvBrand.DataSource = new List<MasterItemBrand>();
            gvBrand.DataSource = _lstbrand;

            txtBrand.Text = "";
            txtBrandName.Text = "";
            MessageBox.Show("Successfully Added", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
              _brandtag = 1;
          

        }

        private void btnAddModel_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtModel.Text ))
            {
                MessageBox.Show("Enter model", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtModelDes.Text))
            {
                MessageBox.Show("Enter Description", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtMainCat.Text))
            {
                MessageBox.Show("Enter Main category", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtCat1.Text))
            {
                MessageBox.Show("Enter Sub category 1", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            if (string.IsNullOrEmpty(txtCat2.Text))
            {
                MessageBox.Show("Enter Sub category 2", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtCat3.Text))
            {
                MessageBox.Show("Enter Sub category 3", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtCat4.Text))
            {
                MessageBox.Show("Enter Sub category 4", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_lstmodel != null)
            {
                MasterItemModel result = _lstmodel.Find(x => x.Mm_cd == txtModel.Text);
                if (result != null)
                {
                    MessageBox.Show("This model  already exist", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            


            }
            else
            {
                _lstmodel = new List<MasterItemModel>();
            }
           
                MasterItemModel _model = new MasterItemModel();
                _model.Mm_cd = txtModel.Text;
                _model.Mm_desc = txtModelDes.Text;
                _model.Mm_act = true;
                _model.Mm_cat1 = txtMainCat.Text;
                _model.Mm_cat2 = txtCat1.Text;
                _model.Mm_cat3 = txtCat2.Text;
                _model.Mm_cat4 = txtCat3.Text;
                _model.Mm_cat5 = txtCat4.Text;
                _model.Mm_cre_by = BaseCls.GlbUserID;
                _model.Mm_dis_dt = DateTime.Now.Date;
                _model.Mm_mod_by = BaseCls.GlbUserID;
                _model.Mm_cre_dt = DateTime.Now.Date;
                _lstmodel.Add(_model);
             

            gvModel.DataSource = null;
            gvModel.AutoGenerateColumns = false;
            gvModel.DataSource = new List<MasterItemModel>();
            gvModel.DataSource = _lstmodel;

          txtModel.Text="";
          txtModelDes.Text = "";

          txtMainCat.Text = "";
          txtCat1.Text = "";
          txtCat2.Text = "";
          txtCat3.Text = "";
          txtCat4.Text = "";
          MessageBox.Show("Successfully Added", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
          _modeltag = 1;
   
         
        }

        private void btnAddUOM_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUOM.Text))
            {
                MessageBox.Show("Enter UOM", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtUOMDes.Text))
            {
                MessageBox.Show("Enter Description", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_lstuom != null)
            {
                MasterUOM result = _lstuom.Find(x => x.Msu_cd == txtUOM.Text);
                if (result != null)
                {
                    MessageBox.Show("This UOM already exist", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                _lstuom = new List<MasterUOM>();
            }

            MasterUOM _uom = new MasterUOM();
            _uom.Msu_cd = txtUOM.Text;
            _uom.Msu_desc = txtUOMDes.Text;
            _uom.Msu_act = true;
            _lstuom.Add(_uom);

            gvUOM.DataSource = null;
            gvUOM.AutoGenerateColumns = false;
            gvUOM.DataSource = new List<MasterUOM>();
            gvUOM.DataSource = _lstuom;

            txtUOMDes.Text = "";
            txtUOM.Text = "";
            MessageBox.Show("Successfully Added", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _uomtag = 1;
    
        }

        private void btnAddColor_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtColor.Text))
            {
                MessageBox.Show("Enter color", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtColorDes.Text))
            {
                MessageBox.Show("Enter Description", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_lstclr != null)
            {
                MasterColor result = _lstclr.Find(x => x.Clr_cd == txtColor.Text);
                if (result != null)
                {
                    MessageBox.Show("This color  already exist", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                _lstclr = new List<MasterColor>();
            }

            MasterColor _clor = new MasterColor();
            _clor.Clr_cd = txtColor.Text;
            _clor.Clr_desc = txtColorDes.Text;
            _clor.Clr_stus =true;
            _lstclr.Add(_clor);

            gvColor.DataSource = null;
            gvColor.AutoGenerateColumns = false;
            gvColor.DataSource = new List<MasterColor>();
            gvColor.DataSource = _lstclr;

            txtColorDes.Text = "";
            txtColor.Text = "";
            MessageBox.Show("Successfully Added", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
              _colortag = 1;
        }

        private void btnSaveModel_Click(object sender, EventArgs e)
        {
            if (_lstmodel ==null)
            {
                MessageBox.Show("Enter model to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_lstmodel.Count == 0)
            {
                MessageBox.Show("Enter model to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!string.IsNullOrEmpty(txtModel.Text))
            {
                MessageBox.Show("Please add model", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!string.IsNullOrEmpty(txtModelDes.Text))
            {
                MessageBox.Show("Please add Description", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!string.IsNullOrEmpty(txtMainCat.Text))
            {
                MessageBox.Show("Please add Main category", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!string.IsNullOrEmpty(txtCat1.Text))
            {
                MessageBox.Show("Please add Sub category 1", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            if (!string.IsNullOrEmpty(txtCat2.Text))
            {
                MessageBox.Show("Please add Sub category 2", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!string.IsNullOrEmpty(txtCat3.Text))
            {
                MessageBox.Show("Please add Sub category 3", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!string.IsNullOrEmpty(txtCat4.Text))
            {
                MessageBox.Show("Please add Sub category 4", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_modeltag == 0)
            {
                MessageBox.Show("No any modificatoin or new record to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            
            string _msg=string.Empty ;
            
            //As per the darshana's comment new paraeters are set as null in windows version by Chamal on 03-Aug-2016
            int row_aff = CHNLSVC.General.SaveItemModel(_lstmodel, _lstmodeldel,null,null,null,null, out _msg);
           if (row_aff == 1)
           {
               MessageBox.Show("Successfully Saved", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
               _modeltag = 0;
           }
           else
           {
               MessageBox.Show("Terminate", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
           }
        }

        private void btnSaveBrand_Click(object sender, EventArgs e)
        {
            if (_lstbrand.Count == null)
            {
                MessageBox.Show("Enter brand to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_lstbrand.Count == 0)
            {
                MessageBox.Show("Enter brand to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!string.IsNullOrEmpty(txtBrand.Text))
            {
                MessageBox.Show("Please add brand", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!string.IsNullOrEmpty(txtBrandName.Text))
            {
                MessageBox.Show("Please add Description", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_brandtag == 0)
            {
                MessageBox.Show("No any modificatoin or new record to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            string _msg = string.Empty;
            int row_aff = CHNLSVC.General.SaveItemBrand(_lstbrand, _lstbranddel, out _msg);
            if (row_aff == 1)
            {
                MessageBox.Show("Successfully Saved", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _brandtag = 0;
            }
            else
            {
                MessageBox.Show("Terminate", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSaveUOM_Click(object sender, EventArgs e)
        {
            if (_lstuom.Count == null)
            {
                MessageBox.Show("Enter UOM to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_lstuom.Count == 0)
            {
                MessageBox.Show("Enter UOM to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!string.IsNullOrEmpty(txtUOM.Text))
            {
                MessageBox.Show("Please add UOM", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!string.IsNullOrEmpty(txtUOMDes.Text))
            {
                MessageBox.Show("Please add Description", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_uomtag == 0)
            {
                MessageBox.Show("No any modificatoin or new record to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            string _msg = string.Empty;
            int row_aff = CHNLSVC.General.SaveItemUOM(_lstuom,_lstuomdel, out _msg);
            if (row_aff == 1)
            {
                MessageBox.Show("Successfully Saved", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _uomtag = 0;
            }
            else
            {
                MessageBox.Show("UOM cannot be deleted", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSaveColor_Click(object sender, EventArgs e)
        {
            if (_lstclr.Count == null)
            {
                MessageBox.Show("Enter color to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_lstclr.Count == 0)
            {
                MessageBox.Show("Enter color to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!string.IsNullOrEmpty(txtColor.Text))
            {
                MessageBox.Show("Please add  color", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!string.IsNullOrEmpty(txtColorDes.Text))
            {
                MessageBox.Show("Please add  Description", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_colortag == 0)
            {
                MessageBox.Show("No any modificatoin or new record to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            string _msg = string.Empty;
            int row_aff = CHNLSVC.General.SaveItemColor(_lstclr, _lstclrdel, out _msg);
            if (row_aff == 1)
            {
                MessageBox.Show("Successfully Saved", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _colortag = 0;
            }
            else
            {
                MessageBox.Show("Terminate", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClearColor_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("Are you sure want to clear?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
            //    _lstclr = new List<MasterColor>();
            //gvColor.DataSource = null;
            //gvColor.AutoGenerateColumns = false;
            //gvColor.DataSource = new List<MasterColor>();
            //gvColor.DataSource = _lstclr;
            txtColorDes.Text="";
            txtColor.Text="";
            }
        }

        private void btnClearModel_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("Are you sure want to clear?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
            //    _lstmodel = new List<MasterItemModel>();
            //gvModel.DataSource = null;
            //gvModel.AutoGenerateColumns = false;
            //gvModel.DataSource = new List<MasterItemModel>();
            //gvModel.DataSource = _lstmodel;
            txtModel.Text="";
            txtModelDes.Text="";
            txtMainCat.Text = "";
            txtCat1.Text = "";
            txtCat2.Text = "";
            txtCat3.Text = "";
            txtCat4.Text = "";
            txtExeclUpload.Text = "";
            }
        }

        private void btnClearBrand_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("Are you sure want to clear?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
            //    _lstbrand = new List<MasterItemBrand>();
            //gvBrand.DataSource = null;
            //gvBrand.AutoGenerateColumns = false;
            //gvBrand.DataSource = new List<MasterItemBrand>();
            //gvBrand.DataSource = _lstbrand;
            txtBrand.Text="";
            txtBrandName.Text="";}
        }

        private void btnClearUOM_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("Are you sure want to clear?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
            //    _lstuom = new List<MasterUOM>();
            //gvUOM.DataSource = null;
            //gvUOM.AutoGenerateColumns = false;
            //gvUOM.DataSource = new List<MasterUOM>();
            //gvUOM.DataSource = _lstuom;
            txtUOMDes.Text="";
            txtUOM.Text="";}
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void tabItem_Click(object sender, EventArgs e)
        {
             
        }

        private void tabItem_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (tabItem.SelectedTab == tabItem.TabPages[1])
            {  _lstmodel = CHNLSVC.General.GetItemModel();
                gvModel.DataSource = null;
                gvModel.AutoGenerateColumns = false;
                gvModel.DataSource = new List<MasterItemModel>();
                gvModel.DataSource = _lstmodel;
            }




            if (tabItem.SelectedTab == tabItem.TabPages[2])
            {
                _lstbrand = CHNLSVC.General.GetItemBrand();
                gvBrand.DataSource = null;
                gvBrand.AutoGenerateColumns = false;
                gvBrand.DataSource = new List<MasterItemBrand>();
                gvBrand.DataSource = _lstbrand;
            }

            if (tabItem.SelectedTab == tabItem.TabPages[3])
            {
                _lstuom = CHNLSVC.General.GetItemUOM();
                gvUOM.DataSource = null;
                gvUOM.AutoGenerateColumns = false;
                gvUOM.DataSource = new List<MasterUOM>();
                gvUOM.DataSource = _lstuom;
            }


            if (tabItem.SelectedTab == tabItem.TabPages[4])
            {
                _lstclr = CHNLSVC.General.GetItemColor();
                gvColor.DataSource = null;
                gvColor.AutoGenerateColumns = false;
                gvColor.DataSource = new List<MasterColor>();
                gvColor.DataSource = _lstclr;
            }
        }

        private void btnSearchUOM_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtUOM;
            _CommonSearch.txtSearchbyword.Text = txtUOM.Text;
            _CommonSearch.ShowDialog();
            txtUOM.Focus();
        }



        private void btnCat4_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _IsCat = false;
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat4);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCate4;
            _CommonSearch.txtSearchbyword.Text = txtCate4.Text;
            _CommonSearch.ShowDialog();
            txtCate4.Focus();
        }

        private void btnCat5_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _IsCat = false;
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat5);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCate5;
            _CommonSearch.txtSearchbyword.Text = txtCate5.Text;
            _CommonSearch.ShowDialog();
            txtCate5.Focus();
        }

        private void txtCate2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_Srch_mainCat_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _IsCat = true;
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat1);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
           
            _CommonSearch.obj_TragetTextBox = txtMainCat;
            _CommonSearch.txtSearchbyword.Text = txtMainCat.Text;
            _CommonSearch.ShowDialog();
            txtMainCat.Focus();
        }

        private void btn_Srch_cat1_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _IsCat = true;
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat2);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
           
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCat1;
            _CommonSearch.txtSearchbyword.Text = txtCat1.Text;
            _CommonSearch.ShowDialog();
            txtCat1.Focus();
        }

        private void btn_Srch_cat2_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _IsCat = true;
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat3);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCat2;
            _CommonSearch.txtSearchbyword.Text = txtCat2.Text;
            _CommonSearch.ShowDialog();
            txtCat2.Focus();
        }

        private void btn_Srch_cat3_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _IsCat = true;
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat4);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCat3;
            _CommonSearch.txtSearchbyword.Text = txtCat3.Text;
            _CommonSearch.ShowDialog();
            txtCat3.Focus();
        }

        private void btn_Srch_cat4_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _IsCat = true;
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat5);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCat4;
            _CommonSearch.txtSearchbyword.Text = txtCat4.Text;
            _CommonSearch.ShowDialog();
            txtCat4.Focus();
        }

        private void txtMainCat_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMainCat.Text))
            {
                return;
            }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat1);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(_CommonSearch.SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtMainCat.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                MessageBox.Show("Invalid Category", "Invalid Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMainCat.Clear();
                txtMainCat.Focus();
                return;
            }
            
        }

        private void txtCat1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCat1.Text))
            {
                return;
            }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat2);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(_CommonSearch.SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtCat1.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                MessageBox.Show("Invalid Category", "Invalid Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCat1.Clear();
                txtCat1.Focus();
                return;
            }
        }

        private void txtCat2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCat2.Text))
            {
                return;
            }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat3);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(_CommonSearch.SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtCat2.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                MessageBox.Show("Invalid Category", "Invalid Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCat2.Clear();
                txtCat2.Focus();
                return;
            }

        }

        private void txtCat3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCat3.Text))
            {
                return;
            }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat4);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(_CommonSearch.SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtCat3.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                MessageBox.Show("Invalid Category", "Invalid Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCat3.Clear();
                txtCat3.Focus();
                return;
            }
        }

        private void txtCat4_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCat4.Text))
            {
                return;
            }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat5);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(_CommonSearch.SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtCat4.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                MessageBox.Show("Invalid Category", "Invalid Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCat4.Clear();
                txtCat4.Focus();
                return;
            }
        }

        private void txtMainCat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCat1.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btn_Srch_mainCat_Click(null, null);
            }
        }

        private void txtCat1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCat2.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btn_Srch_cat1_Click(null, null);
            }
        }

        private void txtCat2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCat3.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btn_Srch_cat2_Click(null, null);
            }
        }

        private void txtCat3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCat4.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btn_Srch_cat3_Click(null, null);
            }
        }

        private void txtCat4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddModel.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btn_Srch_cat4_Click(null, null);
            }
        }

       

        private void gvModel_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtColor_DoubleClick(object sender, EventArgs e)
        {
            btnSrhClr_Click(null, null);
        }

        private void txtUOM_DoubleClick(object sender, EventArgs e)
        {
            btnSearchUOM_Click(null, null);
        }

        private void txtBrand_DoubleClick(object sender, EventArgs e)
        {
            btnSearchBrand_Click(null, null);
        }

        private void txtModel_DoubleClick(object sender, EventArgs e)
        {
            btnSearchModel_Click(null, null);
        }

        private void txtMainCat_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_mainCat_Click(null, null);
        }

        private void txtCat1_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_cat1_Click(null, null);
        }

        private void txtCat2_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_cat2_Click(null, null);
        }

        private void txtCat3_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_cat3_Click(null, null);
        }

        private void txtCat4_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_cat4_Click(null, null);
        }

        private void txtCate1_DoubleClick(object sender, EventArgs e)
        {
            btnCat1_Click(null,null);
        }

        private void txtCate2_DoubleClick(object sender, EventArgs e)
        {
            btnCat2_Click(null, null);
        }

        private void txtCate3_DoubleClick(object sender, EventArgs e)
        {
            btnCat3_Click(null, null);
        }

        private void txtCate4_DoubleClick(object sender, EventArgs e)
        {
            btnCat4_Click(null, null);

        }

        private void txtCate5_DoubleClick(object sender, EventArgs e)
        {
            btnCat5_Click(null, null);
        }

        private void txtMainCat_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUOM_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCat1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCat2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCat3_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCat4_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtColor_TextChanged(object sender, EventArgs e)
        {

        }

        private void gvColor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gvCate1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void gvCate2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //  //  if (chkDel.Checked)
            //    {
            //        if (e.RowIndex != -1)
            //        {
            //            if (MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //            {
            //                string type = gvCate2.Rows[e.RowIndex].Cells[1].Value.ToString();


            //                _lstcate2.RemoveAll(x => x.Ric2_cd == type);
            //                BindingSource source = new BindingSource();
            //                source.DataSource = _lstcate2;
            //                gvCate2.DataSource = source;

            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            //}
            //finally
            //{
            //    CHNLSVC.CloseAllChannels();
            //}
        }

        private void gvCate3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //   // if (chkDel.Checked)
            //    {
            //        if (e.RowIndex != -1)
            //        {
            //            if (MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //            {
            //                string type = gvCate3.Rows[e.RowIndex].Cells[1].Value.ToString();


            //                _lstcate3.RemoveAll(x => x.Ric3_cd == type);
            //                BindingSource source = new BindingSource();
            //                source.DataSource = _lstcate3;
            //                gvCate3.DataSource = source;

            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            //}
            //finally
            //{
            //    CHNLSVC.CloseAllChannels();
            //}
        }

        private void gvCate4_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //   // if (chkDel.Checked)
            //    {
            //        if (e.RowIndex != -1)
            //        {
            //            if (MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //            {
            //                string type = gvCate4.Rows[e.RowIndex].Cells[1].Value.ToString();


            //                _lstcate4.RemoveAll(x => x.Ric4_cd == type);
            //                BindingSource source = new BindingSource();
            //                source.DataSource = _lstcate4;
            //                gvCate4.DataSource = source;

            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            //}
            //finally
            //{
            //    CHNLSVC.CloseAllChannels();
            //}
        }

        private void gvCate5_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{ // if (chkDel.Checked)
            //    {
            //        if (e.RowIndex != -1)
            //        {
            //            if (MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //            {
            //                string type = gvCate5.Rows[e.RowIndex].Cells[1].Value.ToString();


            //                _lstcate5.RemoveAll(x => x.Ric5_cd == type);
            //                BindingSource source = new BindingSource();
            //                source.DataSource = _lstcate5;
            //                gvCate5.DataSource = source;

            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            //}
            //finally
            //{
            //    CHNLSVC.CloseAllChannels();
            //}
        }

        private void gvModel_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gvModel_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //  //  if (chkDelModel.Checked)
            //    {
            //        if (e.RowIndex != -1)
            //        {
            //            if (MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //            {
            //                string type = gvModel.Rows[e.RowIndex].Cells[1].Value.ToString();


            //                _lstmodel.RemoveAll(x => x.Mm_cd == type);
            //                BindingSource source = new BindingSource();
            //                source.DataSource = _lstmodel;
            //                gvModel.DataSource = source;

            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            //}
            //finally
            //{
            //    CHNLSVC.CloseAllChannels();
            //}
        }

        private void gvBrand_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gvBrand_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //   // if (chkDelBrand.Checked)
            //    {
            //        if (e.RowIndex != -1)
            //        {
            //            if (MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //            {
            //                string type = gvBrand.Rows[e.RowIndex].Cells[1].Value.ToString();


            //                _lstbrand.RemoveAll(x => x.Mb_cd == type);
            //                BindingSource source = new BindingSource();
            //                source.DataSource = _lstbrand;
            //                gvBrand.DataSource = source;

            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            //}
            //finally
            //{
            //    CHNLSVC.CloseAllChannels();
            //}
        }

        private void gvUOM_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    if (chkDeluom.Checked)
            //    {
            //        if (e.RowIndex != -1)
            //        {
            //            if (MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //            {
            //                string type = gvUOM.Rows[e.RowIndex].Cells[1].Value.ToString();


            //                _lstuom.RemoveAll(x => x.Msu_cd == type);
            //                BindingSource source = new BindingSource();
            //                source.DataSource = _lstuom;
            //                gvUOM.DataSource = source;

            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            //}
            //finally
            //{
            //    CHNLSVC.CloseAllChannels();
            //}
        }

        private void gvColor_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    if (chkDelClr.Checked)
            //    {
            //        if (e.RowIndex != -1)
            //        {
            //            if (MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //            {
            //                string type = gvColor.Rows[e.RowIndex].Cells[1].Value.ToString();


            //                _lstclr.RemoveAll(x => x.Clr_cd == type);
            //                BindingSource source = new BindingSource();
            //                source.DataSource = _lstclr;
            //                gvColor.DataSource = source;

            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            //}
            //finally
            //{
            //    CHNLSVC.CloseAllChannels();
            //}
        }

        private void gvUOM_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSrhClr_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterColor);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchColorMaster(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtColor;
            _CommonSearch.txtSearchbyword.Text = txtColor.Text;
            _CommonSearch.ShowDialog();
            txtColor.Focus();
        }

        private void gvCate5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 3)
                {
                    _cat5tag = 1;
                }
               
                if (MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string type = gvCate5.Rows[e.RowIndex].Cells[1].Value.ToString();

                    _lstcate5del.AddRange(_lstcate5.Where(x => x.Ric5_cd == type));
                    _lstcate5.RemoveAll(x => x.Ric5_cd == type);
                    BindingSource source = new BindingSource();
                    source.DataSource = _lstcate5;
                    gvCate5.DataSource = source;
                    _cat5tag = 1;

                }
            }
        }

        private void gvModel_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {

                if (e.ColumnIndex == 3)
                {
                    _modeltag = 1;
                }

                if (e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string type = gvModel.Rows[e.RowIndex].Cells[1].Value.ToString();


                        _lstmodeldel.AddRange(_lstmodel.Where(x => x.Mm_cd == type));
                        _lstmodel.RemoveAll(x => x.Mm_cd == type);
                        BindingSource source = new BindingSource();
                        source.DataSource = _lstmodel;
                        gvModel.DataSource = source;
                        _modeltag = 1;

                    }
                }
            }
        }

        private void gvBrand_CellClick(object sender, DataGridViewCellEventArgs e)
        {
          
                if (e.RowIndex != -1)
                {

                    if (e.ColumnIndex == 3)
                    {
                        _brandtag = 1;
                    }

                    if (e.ColumnIndex == 0)
                    {
                    if (MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string type = gvBrand.Rows[e.RowIndex].Cells[1].Value.ToString();

                        _lstbranddel.AddRange(_lstbrand.Where(x => x.Mb_cd == type));
                        _lstbrand.RemoveAll(x => x.Mb_cd == type);
                        BindingSource source = new BindingSource();
                        source.DataSource = _lstbrand;
                        gvBrand.DataSource = source;
                        _brandtag = 1;

                    }
                }
            }
        }

        private void gvUOM_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 3)
                {
                    _uomtag = 1;
                }

                if (e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string type = gvUOM.Rows[e.RowIndex].Cells[1].Value.ToString();

                        _lstuomdel.AddRange(_lstuom.Where(x => x.Msu_cd == type));
                        _lstuom.RemoveAll(x => x.Msu_cd == type);
                        BindingSource source = new BindingSource();
                        source.DataSource = _lstuom;
                        gvUOM.DataSource = source;
                        _uomtag = 1;

                    }
                }
            }
        }

        private void gvColor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 3)
                {
                    _colortag = 1;
                }
                if (e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string type = gvColor.Rows[e.RowIndex].Cells[1].Value.ToString();

                        _lstclrdel.AddRange(_lstclr.Where(x => x.Clr_cd == type));
                        _lstclr.RemoveAll(x => x.Clr_cd == type);
                        BindingSource source = new BindingSource();
                        source.DataSource = _lstclr;
                        gvColor.DataSource = source;
                        _colortag = 1;

                    }
                }
            }
        }

        private void txtCate1_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCate1.Text))
            {
                DataTable _categoryDet = CHNLSVC.General.GetMainCategoryDetail(txtCate1.Text);
                if (_categoryDet != null && _categoryDet.Rows.Count > 0)
                {
                    txtCate1Des.Text =_categoryDet.Rows[0]["RIC1_DESC"].ToString();
                   
                      
                }
            }
        }

        private void txtCate2_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCate2.Text))
            {
                MasterItemSubCate subCate = CHNLSVC.Sales.GetItemSubCate(txtCate2.Text);
                if (subCate != null)
                {
                    txtCate2Des.Text = subCate.Ric2_desc;
                }
            }

        }

        private void txtCate3_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCate3.Text))
            {
                REF_ITM_CATE3 subCate = CHNLSVC.General.GetItemCategory3(txtCate3.Text);
                if (subCate != null)
                
                {
                    txtCate3Des.Text = subCate.Ric2_desc;
                }
            }
        }

        private void txtCate4_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCate4.Text))
            {
                REF_ITM_CATE4 subCate = CHNLSVC.General.GetItemCategory4(txtCate4.Text);
                if (subCate != null)
                {
                    txtCate4Des.Text = subCate.Ric4_desc;
                }
            }
        }

        private void txtCate5_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCate5.Text))
            {
                REF_ITM_CATE5 subCate = CHNLSVC.General.GetItemCategory5(txtCate5.Text);
                if (subCate != null)
                {
                    txtCate5Des.Text = subCate.Ric5_desc;
                }
            }
        }

        private void txtCate5_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCate3_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtModel_Leave(object sender, EventArgs e)
        {
            //List<MasterItemModel> _lstmodeltem = new List<MasterItemModel>();
            //List<MasterItemModel> _lstmodel = new List<MasterItemModel>();
            //_lstmodeltem = CHNLSVC.General.GetItemModel();
            //if (_lstmodeltem != null)
            //{
            //    _lstmodel = _lstmodeltem.Where(x => x.Mm_cd == txtModel.Text).ToList();
            //    txtMainCat.Text = _lstmodel[0].Mm_cat1;
            //    txtCat1.Text = _lstmodel[0].Mm_cat2;
            //    txtCat2.Text = _lstmodel[0].Mm_cat3;
            //    txtCat3.Text = _lstmodel[0].Mm_cat4;
            //    txtCat4.Text = _lstmodel[0].Mm_cat5;
            //}
        }

        //Tharindu 2018-01-23
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

                string modelcode = "";
                string desc = "";
                string maincat = "";
                string subcat1 = "";
                string subcat2 = "";
                string subcat3 = "";
                string subcat4 = "";
              
                Int32 _currentRow = 0;

                StringBuilder _errorLst = new StringBuilder();
                if (_dt == null || _dt.Rows.Count <= 0) { MessageBox.Show("The excel file is empty. Please check the file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

                if (_dt.Rows.Count > 0)
                {
                    if (MessageBox.Show("Are you sure to Upload ?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No) return;

                    // Empty the list.
                    _lstmodel = new List<MasterItemModel>();

                    foreach (DataRow _dr in _dt.Rows)
                    {
                        _currentRow = _currentRow + 1;

                        modelcode = _dr[0].ToString().Trim();
                        desc = _dr[1].ToString().Trim();
                        maincat = _dr[2].ToString().Trim();
                        subcat1 = _dr[3].ToString().Trim();
                        subcat2 = _dr[4].ToString().Trim();
                        subcat3 = _dr[5].ToString().Trim();
                        subcat4 = _dr[6].ToString().Trim();
                       
                        #region item validation

                        if (string.IsNullOrEmpty(modelcode))
                        {
                            MessageBox.Show("Please Enter Model Code.", "Item Category", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }

                        if (string.IsNullOrEmpty(desc.ToString()))
                        {
                            MessageBox.Show("Please Enter Description.", "Item Category", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }

                        if (!string.IsNullOrEmpty(maincat))
                        {
                            DataTable _categoryDet = CHNLSVC.General.GetMainCategoryDetail(maincat);
                            if (_categoryDet.Rows.Count == 0)
                            {
                                MessageBox.Show("Invalid Main Category Line No'" + _currentRow + "", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }

                        }

                        if (!string.IsNullOrEmpty(subcat1))
                        {
                            DataTable _cat2 = CHNLSVC.Sales.GetItemSubCate2(maincat, subcat1);
                            if (_cat2.Rows.Count == 0)
                            {
                                MessageBox.Show("Invalid Sub Category1 Line No'" + _currentRow + "", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }

                        if (!string.IsNullOrEmpty(subcat2))
                        {
                            DataTable _cat3 = CHNLSVC.Sales.GetItemSubCate3(maincat, subcat1,subcat2);
                            if (_cat3.Rows.Count == 0)
                            {
                                MessageBox.Show("Invalid Sub Category2 Line No'" + _currentRow + "", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }

                        if (!string.IsNullOrEmpty(subcat3))
                        {
                            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                            _CommonSearch.ReturnIndex = 0;
                            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat4);
                            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(_CommonSearch.SearchParams, null, null);

                            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == subcat3.Trim()).ToList();
                            if (_validate == null || _validate.Count <= 0)
                            {
                                MessageBox.Show("Invalid Sub Category3 Line No'" + _currentRow + "", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }

                        if (!string.IsNullOrEmpty(subcat4))
                        {
                            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                            _CommonSearch.ReturnIndex = 0;
                            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat5);
                            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(_CommonSearch.SearchParams, null, null);

                            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == subcat4.Trim()).ToList();
                            if (_validate == null || _validate.Count <= 0)
                            {
                                MessageBox.Show("Invalid Sub Category4 Line No'" + _currentRow + "", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }

                        #endregion

                        #region Add Correct Items to List
                        MasterItemModel _model = new MasterItemModel();
                        _model.Mm_cd = modelcode;
                        _model.Mm_desc = desc;
                        _model.Mm_act = true;
                        _model.Mm_cat1 = maincat;
                        _model.Mm_cat2 = subcat1;
                        _model.Mm_cat3 = subcat2;
                        _model.Mm_cat4 = subcat3;
                        _model.Mm_cat5 = subcat4;
                        _model.Mm_cre_by = BaseCls.GlbUserID;
                        _model.Mm_cre_dt = DateTime.Now.Date;
                        _lstmodel.Add(_model);                  
                        #endregion                      

                    }
                    if (MessageBox.Show("Upload is done. Are you sure to Save ?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No) return;

                    #region Save The Excel Data
                    string _msg = string.Empty;

                    int row_aff = CHNLSVC.General.SaveItemModel(_lstmodel, _lstmodeldel, null, null, null, null, out _msg);
                    if (row_aff == 1)
                    {
                        MessageBox.Show("Successfully Saved", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _modeltag = 0;
                        txtExeclUpload.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Terminate", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    } 
                    #endregion
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
        }

        private void label22_Click(object sender, EventArgs e)
        {

        }        
    }
}
