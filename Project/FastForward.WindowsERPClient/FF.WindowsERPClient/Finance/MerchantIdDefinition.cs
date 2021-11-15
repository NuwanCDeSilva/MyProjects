using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using System.Configuration;
using System.Data.OleDb;

namespace FF.WindowsERPClient.Finance
{
    public partial class MerchantIdDefinition : Base
    {
        string _option;
        int _pun_tp = 0;
        int _main_puntp = 0;
        bool isUpdate = false;
        Deposit_Bank_Pc_wise obj_mids;
        List<Deposit_Bank_Pc_wise> _lstMasterPcwiseMid;
        List<PriceBookLevelRef> PBList;
        List<CashCommissionDetailRef> ItemBrandCat_List;
        public MerchantIdDefinition()
        {
            InitializeComponent();
            PBList = new List<PriceBookLevelRef>();
            ItemBrandCat_List = new List<CashCommissionDetailRef>();

          //  pnlCreateNew.Location = new Point(402, 144);
            pnlInactivate.Location = new Point(16, 331);
            pnlsearchmid.Location = new Point(421, 369);
            BindCategoryTypes();
            BindPriceType();
            cmbPriceType.Text = "";
            cmbPriceType.SelectedValue=-1;
            load_bank();
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
                case CommonUIDefiniton.SearchUserControlType.PriceBookByCompany:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevelByBook:
                    {

                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtPriceBook.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator);
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
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append("" + seperator + "" + seperator + BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "Loc" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item_Serials:
                    {
                        paramsText.Append(txtItemCD.Text + seperator + "" + seperator + BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "Loc" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Circular:
                    {
                        paramsText.Append("" + seperator + "Circular" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Promotion:
                    {
                        paramsText.Append("" + seperator + "Promotion" + seperator);
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
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GPC:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.OPE:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
               
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            string com = txtComp.Text;
            string chanel = txtChanel.Text;
            string subChanel = txtSChanel.Text;
            string area = txtArea.Text;
            string region = txtRegion.Text;
            string zone = txtZone.Text;
            string pc = txtPC.Text;

            lstPC.Clear();
            string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
            if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "REPS"))
            {
                DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(com, chanel, subChanel, area, region, zone, pc);
                foreach (DataRow drow in dt.Rows)
                {
                    lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                }
            }
            else
            {
                DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep(BaseCls.GlbUserID, com, chanel, subChanel, area, region, zone, pc);
                foreach (DataRow drow in dt.Rows)
                {
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

        private void txtComp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtComp;
                _CommonSearch.ShowDialog();
                txtComp.Select();
            }
        }

        private void txtChanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtChanel;
            _CommonSearch.ShowDialog();
            txtChanel.Select();
            txtSChanel.Focus();

        }

        private void txtChanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtChanel_MouseDoubleClick(null, null);
            }
        }

        private void txtSChanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtSChanel;
            _CommonSearch.ShowDialog();
            txtSChanel.Select();
        }

        private void txtSChanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtSChanel_MouseDoubleClick(null, null);
            }
        }

        private void txtArea_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtArea;
            _CommonSearch.ShowDialog();
            txtArea.Select();
        }

        private void txtArea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtArea_MouseDoubleClick(null, null);

            }

        }

        private void txtRegion_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtRegion;
            _CommonSearch.ShowDialog();
            txtRegion.Select();
        }

        private void txtRegion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtRegion_MouseDoubleClick(null, null);
            }
        }

        private void txtZone_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtZone;
            _CommonSearch.ShowDialog();
            txtZone.Select();
        }

        private void txtZone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtZone_MouseDoubleClick(null, null);
            }
        }

        private void txtPC_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtPC;
            _CommonSearch.ShowDialog();
            txtPC.Select();
        }

        private void txtPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtPC_MouseDoubleClick(null, null);
            }
        }

        private void MerchantIdDefinition_Load(object sender, EventArgs e)
        {
            txtComp.Text = BaseCls.GlbUserComCode;
            dtpFromDate.MinDate = DateTime.Now;
            dtpTodate.MinDate = DateTime.Now;

            loadMidDetails();

        }

        private void loadMidDetails()
        {
            if (rdoOffline.Checked)
            {
                 _main_puntp = 0;
            }
            else
            {
                 _main_puntp = 1;
            }

            DataTable dt = CHNLSVC.Sales.getMerchantIdDets(_main_puntp);

            if (dt.Rows.Count > 1)
            {
                IEnumerable<DataRow> results = (from MyRows in dt.AsEnumerable()
                                                where
                                                 MyRows.Field<string>("mbi_id") == cmbbanknew.SelectedValue.ToString()

                                                select MyRows);
                if (results.Count() != 0)
                {
                    dt = results.CopyToDataTable();
                }
                else
                { 
                
                }
              
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                
                cmbMainMidNo.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    //cmbMainMidNo.Items.Add(dt.Rows[i][0] + " || " + dt.Rows[i][1]);
                    cmbMainMidNo.Items.Add(dt.Rows[i][0]);
                }
                cmbMainMidNo.SelectedIndex = -1;
            }
            else
            {
                cmbMainMidNo.DataSource = null;
            }
        }


        private void LoadBanks()
        {
            DataTable dt = CHNLSVC.Sales.LoadBankNewDets();
            if (dt != null && dt.Rows.Count > 0)
            {
                
                cmbBankName.DataSource = dt;
                cmbBankName.DisplayMember = "MBI_DESC";
                cmbBankName.ValueMember = "MBI_ID";
                cmbBankName.Text = "---Select a Bank---";

            }
            else
            {
                cmbBankName.DataSource = null;
            }
        }

        private void clearfields()
        {
            txtperiod.Text = "";
            cmbMainMidNo.SelectedIndex = -1;
            dtpFromDate.Value = dtpFromDate.MinDate;
            dtpTodate.Value = dtpTodate.MinDate;
        }

        private void AddValueToGrid()
        {
            try
            {
                //dgvMerchantDetails.Rows.Clear();
                if (rdoOffline.Checked)
                {
                    _option = "off line";
                }
                else if (rdoOnline.Checked)
                {
                     _option = "on line";
                }
                string _mids = cmbMainMidNo.Text;
                string[] arr = _mids.Split(new string[] { "||" }, StringSplitOptions.None);
                string _acc = arr[0].Trim();

                foreach (DataGridViewRow dgvr in dgvMerchantDetails.Rows)
                {
                    if (Convert.ToDateTime(dtpFromDate.Value.Date) == Convert.ToDateTime(dgvr.Cells["clmFromDate"].Value) && Convert.ToInt32(txtperiod.Text.Trim()) == Convert.ToInt32(dgvr.Cells["clmperiod"].Value) && _option == Convert.ToString(dgvr.Cells["clmOption"].Value))
                    {

                        MessageBox.Show("These values are already available in the grid.", "MID", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                string selection = "";
                if (cmbSelectCat.SelectedValue.ToString() == "10")
                    selection = "CATE1";
                else if (cmbSelectCat.SelectedValue.ToString() == "9")
                    selection = "CATE2";
                else if (cmbSelectCat.SelectedValue.ToString() == "8")
                    selection = "CATE3";
                else if (cmbSelectCat.SelectedValue.ToString() == "7")
                    selection = "BRAND";
                else if (cmbSelectCat.SelectedValue.ToString() == "6")
                    selection = "BRAND_CATE1";
                else if (cmbSelectCat.SelectedValue.ToString() == "5")
                    selection = "BRAND_CATE2";
                else if (cmbSelectCat.SelectedValue.ToString() == "4")
                    selection = "BRAND_CATE3";
                else if (cmbSelectCat.SelectedValue.ToString() == "3")
                    selection = "ITEM";
                else if (cmbSelectCat.SelectedValue.ToString() == "2")
                    selection = "PROMOTION";
                else if (cmbSelectCat.SelectedValue.ToString() == "1")
                    selection = "SERIAL";
                else if (cmbSelectCat.SelectedValue.ToString() == "11")
                    selection = "ITEM_PROMO";
                else if (cmbSelectCat.SelectedValue.ToString() == "12")
                    selection = "CIRCULARS";

                if (dataGridViewPriceBook.Rows.Count > 0)
                {
                    foreach (DataGridViewRow r in dataGridViewPriceBook.Rows)
                    {
                        if (grvSalesTypes.Rows.Count > 0)
                        {
                            foreach (DataGridViewRow r1 in grvSalesTypes.Rows)
                            {

                                dgvMerchantDetails.Rows.Add();

                                dgvMerchantDetails["clmFromDate", dgvMerchantDetails.Rows.Count - 1].Value = Convert.ToDateTime(dtpFromDate.Value.Date);
                                dgvMerchantDetails["clmTodate", dgvMerchantDetails.Rows.Count - 1].Value = Convert.ToDateTime(dtpTodate.Value.Date);
                                dgvMerchantDetails["clmOption", dgvMerchantDetails.Rows.Count - 1].Value = _option;
                                dgvMerchantDetails["clmperiod", dgvMerchantDetails.Rows.Count - 1].Value = Convert.ToInt32(txtperiod.Text.Trim());
                                dgvMerchantDetails["clmMid", dgvMerchantDetails.Rows.Count - 1].Value = _acc;
                                dgvMerchantDetails["clmbnkchg", dgvMerchantDetails.Rows.Count - 1].Value = Convert.ToDecimal(txtbankchg.Text.Trim()); //Sanjeewa 2017-02-24
                                dgvMerchantDetails["GP_Margin", dgvMerchantDetails.Rows.Count - 1].Value = Convert.ToDecimal(txtgp.Text.Trim()); //tharanga 2018-10-08
                                dgvMerchantDetails["clmpb", dgvMerchantDetails.Rows.Count - 1].Value = r.Cells["Sapl_pb"].Value.ToString();
                                dgvMerchantDetails["clmpblvl", dgvMerchantDetails.Rows.Count - 1].Value = r.Cells["Sapl_pb_lvl_cd"].Value.ToString();
                                if (selection == "BRAND")
                                    dgvMerchantDetails["clmbrand", dgvMerchantDetails.Rows.Count - 1].Value = r1.Cells["Sccd_itm"].Value.ToString();
                                if (selection == "CATE1")
                                    dgvMerchantDetails["clmmaincat", dgvMerchantDetails.Rows.Count - 1].Value = r1.Cells["Sccd_itm"].Value.ToString();
                                if (selection == "ITEM")
                                    dgvMerchantDetails["clmitem", dgvMerchantDetails.Rows.Count - 1].Value = r1.Cells["Sccd_itm"].Value.ToString();
                                if (selection == "PROMOTION")
                                {
                                    dgvMerchantDetails["clmpromo", dgvMerchantDetails.Rows.Count - 1].Value = r1.Cells["Sccd_itm"].Value.ToString();
                                   
                                }
                                if (selection == "BRAND_CATE1")
                                {
                                    dgvMerchantDetails["clmmaincat", dgvMerchantDetails.Rows.Count - 1].Value = r1.Cells["Sccd_itm"].Value.ToString();
                                    dgvMerchantDetails["clmbrand", dgvMerchantDetails.Rows.Count - 1].Value = r1.Cells["Column12"].Value.ToString();

                                }
                                if (selection == "ITEM_PROMO")
                                {
                                    dgvMerchantDetails["clmitem", dgvMerchantDetails.Rows.Count - 1].Value = r1.Cells["Sccd_itm"].Value.ToString();
                                    dgvMerchantDetails["clmpromo", dgvMerchantDetails.Rows.Count - 1].Value = r1.Cells["Column12"].Value.ToString();

                                }
                                if (selection == "CIRCULARS")
                                {
                                    dgvMerchantDetails["clmitem", dgvMerchantDetails.Rows.Count - 1].Value = r1.Cells["Sccd_itm"].Value.ToString();
                                }
                                if (r.Cells["Price_type"].Value == null)
                                {

                                    dgvMerchantDetails["Price_Tp", dgvMerchantDetails.Rows.Count - 1].Value = "-1";
                                }
                                else
                                {
                                    dgvMerchantDetails["Price_Ty", dgvMerchantDetails.Rows.Count - 1].Value = r.Cells["Price_type"].Value.ToString();
                                    dgvMerchantDetails["Price_Tp", dgvMerchantDetails.Rows.Count - 1].Value = r.Cells["pricetp"].Value.ToString();
                                }
                            }
                        }
                        else
                        {
                            dgvMerchantDetails.Rows.Add();
                            dgvMerchantDetails["clmFromDate", dgvMerchantDetails.Rows.Count - 1].Value = Convert.ToDateTime(dtpFromDate.Value.Date);
                            dgvMerchantDetails["clmTodate", dgvMerchantDetails.Rows.Count - 1].Value = Convert.ToDateTime(dtpTodate.Value.Date);
                            dgvMerchantDetails["clmOption", dgvMerchantDetails.Rows.Count - 1].Value = _option;
                            dgvMerchantDetails["clmperiod", dgvMerchantDetails.Rows.Count - 1].Value = Convert.ToInt32(txtperiod.Text.Trim());
                            dgvMerchantDetails["clmMid", dgvMerchantDetails.Rows.Count - 1].Value = _acc;
                            dgvMerchantDetails["clmbnkchg", dgvMerchantDetails.Rows.Count - 1].Value = Convert.ToDecimal(txtbankchg.Text.Trim()); //Sanjeewa 2017-02-24
                            dgvMerchantDetails["GP_Margin", dgvMerchantDetails.Rows.Count - 1].Value = Convert.ToDecimal(txtgp.Text.Trim()); //tharanga 2018-10-08
                            dgvMerchantDetails["clmpb", dgvMerchantDetails.Rows.Count - 1].Value = r.Cells["Sapl_pb"].Value.ToString();
                            dgvMerchantDetails["clmpblvl", dgvMerchantDetails.Rows.Count - 1].Value = r.Cells["Sapl_pb_lvl_cd"].Value.ToString();
                            if (r.Cells["Price_type"].Value == null)
                            {
                                dgvMerchantDetails["Price_Tp", dgvMerchantDetails.Rows.Count - 1].Value = "-1";
                            }
                            else
                            {
                                dgvMerchantDetails["Price_Ty", dgvMerchantDetails.Rows.Count - 1].Value = r.Cells["Price_type"].Value.ToString();
                                dgvMerchantDetails["Price_Tp", dgvMerchantDetails.Rows.Count - 1].Value = r.Cells["pricetp"].Value.ToString();
                            }
                        }
                    }
                }
                else
                {
                    if (grvSalesTypes.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow r1 in grvSalesTypes.Rows)
                        {

                            dgvMerchantDetails.Rows.Add();

                            dgvMerchantDetails["clmFromDate", dgvMerchantDetails.Rows.Count - 1].Value = Convert.ToDateTime(dtpFromDate.Value.Date);
                            dgvMerchantDetails["clmTodate", dgvMerchantDetails.Rows.Count - 1].Value = Convert.ToDateTime(dtpTodate.Value.Date);
                            dgvMerchantDetails["clmOption", dgvMerchantDetails.Rows.Count - 1].Value = _option;
                            dgvMerchantDetails["clmperiod", dgvMerchantDetails.Rows.Count - 1].Value = Convert.ToInt32(txtperiod.Text.Trim());
                            dgvMerchantDetails["clmMid", dgvMerchantDetails.Rows.Count - 1].Value = _acc;
                            dgvMerchantDetails["clmbnkchg", dgvMerchantDetails.Rows.Count - 1].Value = Convert.ToDecimal(txtbankchg.Text.Trim()); //Sanjeewa 2017-02-24
                            dgvMerchantDetails["GP_Margin", dgvMerchantDetails.Rows.Count - 1].Value = Convert.ToDecimal(txtgp.Text.Trim()); //tharanga 2018-10-08


                            dgvMerchantDetails["Price_Tp", dgvMerchantDetails.Rows.Count - 1].Value = "-1";
                            if (selection == "BRAND")
                                dgvMerchantDetails["clmbrand", dgvMerchantDetails.Rows.Count - 1].Value = r1.Cells["Sccd_itm"].Value.ToString();
                            if (selection == "CATE1")
                                dgvMerchantDetails["clmmaincat", dgvMerchantDetails.Rows.Count - 1].Value = r1.Cells["Sccd_itm"].Value.ToString();
                            if (selection == "ITEM")
                                dgvMerchantDetails["clmitem", dgvMerchantDetails.Rows.Count - 1].Value = r1.Cells["Sccd_itm"].Value.ToString();
                            if (selection == "PROMOTION")
                            {
                                dgvMerchantDetails["clmpromo", dgvMerchantDetails.Rows.Count - 1].Value = r1.Cells["Sccd_itm"].Value.ToString();

                            }
                            if (selection == "BRAND_CATE1")
                            {
                                dgvMerchantDetails["clmmaincat", dgvMerchantDetails.Rows.Count - 1].Value = r1.Cells["Sccd_itm"].Value.ToString();
                                dgvMerchantDetails["clmbrand", dgvMerchantDetails.Rows.Count - 1].Value = r1.Cells["Column12"].Value.ToString();

                            }
                            if (selection == "ITEM_PROMO")
                            {
                                dgvMerchantDetails["clmitem", dgvMerchantDetails.Rows.Count - 1].Value = r1.Cells["Sccd_itm"].Value.ToString();
                                dgvMerchantDetails["clmpromo", dgvMerchantDetails.Rows.Count - 1].Value = r1.Cells["Column12"].Value.ToString();

                            }
                            if (selection == "CIRCULARS")
                            {
                                dgvMerchantDetails["clmitem", dgvMerchantDetails.Rows.Count - 1].Value = r1.Cells["Sccd_itm"].Value.ToString();
                            }


                        }
                    }
                    else
                    {
                        dgvMerchantDetails.Rows.Add();

                        dgvMerchantDetails["clmFromDate", dgvMerchantDetails.Rows.Count - 1].Value = Convert.ToDateTime(dtpFromDate.Value.Date);
                        dgvMerchantDetails["clmTodate", dgvMerchantDetails.Rows.Count - 1].Value = Convert.ToDateTime(dtpTodate.Value.Date);
                        dgvMerchantDetails["clmOption", dgvMerchantDetails.Rows.Count - 1].Value = _option;
                        dgvMerchantDetails["clmperiod", dgvMerchantDetails.Rows.Count - 1].Value = Convert.ToInt32(txtperiod.Text.Trim());
                        dgvMerchantDetails["clmMid", dgvMerchantDetails.Rows.Count - 1].Value = _acc;
                        dgvMerchantDetails["clmbnkchg", dgvMerchantDetails.Rows.Count - 1].Value = Convert.ToDecimal(txtbankchg.Text.Trim()); //Sanjeewa 2017-02-24
                        dgvMerchantDetails["GP_Margin", dgvMerchantDetails.Rows.Count - 1].Value = Convert.ToDecimal(txtgp.Text.Trim()); //tharanga 2018-10-08


                        dgvMerchantDetails["Price_Tp", dgvMerchantDetails.Rows.Count - 1].Value = "-1";
                    }
                }
                
            }
            catch (Exception ex)
            {    
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CHNLSVC.CloseChannel();
            }
            finally
            {

            }
        }

        private void btnAddtogrid_Click(object sender, EventArgs e)
        {
            if (dtpFromDate.Value.Date > dtpTodate.Value.Date)
            {
                MessageBox.Show("From Date must be less than To Date", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtperiod.Text.Trim() == "")
            {
                MessageBox.Show("Please enter period", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtperiod.Focus();
                return;
            }
            if (txtbankchg.Text.Trim() == "") //Sanjeewa 2017-02-24
            {
                MessageBox.Show("Please bank charge", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtbankchg.Focus();
                return;
            }
            if (txtgp.Text.Trim() == "") 
            {
                MessageBox.Show("Please GP range", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtgp.Focus();
                return;
            }
            if (cmbMainMidNo.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Merchant Id Number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtperiod.Focus();
                return;
            }

            AddValueToGrid();
            clearfields();

        }

        private void txtperiod_Leave(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtperiod.Text.Trim())) return;

            if (!txtperiod.Text.All(c => Char.IsNumber(c)))
            {
                MessageBox.Show("Please enter only positive numbers", "MID", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtperiod.Text = "";
                txtperiod.Focus();
                return;
            }
            int num = Convert.ToInt32(txtperiod.Text.Trim());
            if (num < 0)
            {
                MessageBox.Show("Please enter positive numbers", "MID", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtperiod.Text = "";
                txtperiod.Focus();
                return;
            }
           


            cmbMainMidNo.Focus();
            
        }

        private Deposit_Bank_Pc_wise filltoMids()
        {
            obj_mids = new Deposit_Bank_Pc_wise();
            obj_mids.BankName = cmbBankName.SelectedValue.ToString();
            obj_mids.Mid_no = txtMechantID.Text.Trim();
            obj_mids.SunAccNo = txtSunAcc.Text.Trim();
            obj_mids.Create_by = BaseCls.GlbUserID;
            if (rdoOf.Checked)
                {
                     obj_mids.Pun_tp = 0;
                }
                else
                {
                      obj_mids.Pun_tp = 1;
                }
            return obj_mids;
        }


        private void btnClearNew_Click(object sender, EventArgs e)
        {
            #region validation

            if (cmbBankName.Text == "---Select a Bank---")
            {
                MessageBox.Show("Please select Bank", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtMechantID.Text.Trim() == "")
            {
                MessageBox.Show("Please enter merchant id number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //if (txtSunAcc.Text.Trim() == "")
            //{
            //    MessageBox.Show("Please enter sun account number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            
            #endregion

            try
            {
                if (rdoOf.Checked)
                {
                    _pun_tp = 0;
                }
                else
                {
                    _pun_tp = 1;
                }
               
                if (MessageBox.Show("Are you sure ?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

                if (isUpdate == false)
                {
                    bool chek = CHNLSVC.Sales.Check_Available_Mids(cmbBankName.SelectedValue.ToString(), txtMechantID.Text.Trim(), _pun_tp);
                    if (chek == true)
                    {
                        MessageBox.Show("Thease Records are already inserted!!!..Try Again..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMechantID.Text = "";
                        return;
                    }
                    string _error = "";
                    int result = CHNLSVC.Sales.Insert_to_master_mid(filltoMids(), out _error);
                    if (result == -1)
                    {
                        MessageBox.Show("Error occurred while processing\n" + _error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Records inserted Successfully", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        chkActive.Text = "Active";
                        btnClearNew.Text = "Save";
                        isUpdate = false;
                        txtMechantID.Text = "";
                        txtSunAcc.Text = "";
                        txtMechantID.ReadOnly = false;
                        cmbBankName.DataSource = null;
                        loadMidDetails();
                        pnlCreateNew.Visible = false;

                    }


                }
                else
                {
                    string _error = "";
                    int result = CHNLSVC.Sales.Update_to_master_mid(filltoMids(), out _error);
                    if (result == -1)
                    {
                        MessageBox.Show("Error occurred while processing\n" + _error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Records Updated Successfully", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        chkActive.Text = "Active";
                        btnClearNew.Text = "Save";
                        isUpdate = false;
                        txtMechantID.Text = "";
                        txtMechantID.ReadOnly = false;
                        cmbBankName.DataSource = null;
                        pnlCreateNew.Visible = false;

                    }
                }
            }
            catch (Exception ex)
            {

                //throw ex;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CHNLSVC.CloseChannel();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            pnlCreateNew.Visible = true;
            pnlInactivate.Location = new Point(16, 331);
            pnlsearchmid.Location = new Point(421, 369);
            pnlCreateNew.Size = new Size(287, 151);
            
            LoadBanks();
        }

        private void cmbBankName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = (char)Keys.None;
        }

        private void btnClose_new_Click(object sender, EventArgs e)
        {
            chkActive.Text = "Active";
            btnClearNew.Text = "Save";
            isUpdate = false;
            txtMechantID.Text = "";
            txtSunAcc.Enabled = true;
            txtMechantID.ReadOnly = false;
            cmbBankName.DataSource = null;
            pnlCreateNew.Visible = false;
        }
        private void rdoOn_CheckedChanged(object sender, EventArgs e)
        {
            cmbBankName.Text = "---Select a Bank---";
           
            chkActive.Text = "Active";
            btnClearNew.Text = "Save";
            isUpdate = false;
            txtMechantID.Text = "";
            txtMechantID.ReadOnly = false;

        }

        private void rdoOf_CheckedChanged(object sender, EventArgs e)
        {
            cmbBankName.Text = "---Select a Bank---";
           
            chkActive.Text = "Active";
            btnClearNew.Text = "Save";
            isUpdate = false;
            txtMechantID.Text = "";
            txtMechantID.ReadOnly = false;
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            if (cmbBankName.Text == "---Select a Bank---")
            {
                MessageBox.Show("Please select bank", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (rdoOf.Checked)
            {
               _pun_tp = 0;
            }   
            else
            {
                _pun_tp = 1;
            }
            DataTable dt = CHNLSVC.Sales.Load_mids(cmbBankName.SelectedValue.ToString(), _pun_tp);
            if (dt != null && dt.Rows.Count > 0)
            {

                pnlsearchmid.Visible = true;
                dgvSearchMid.AutoGenerateColumns = false;
                dgvSearchMid.DataSource = dt;

            }
            else
            {
                MessageBox.Show("No record found..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvSearchMid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           if (e.RowIndex != -1)
            {

                txtMechantID.Text = dgvSearchMid["clmMerchant", e.RowIndex].Value.ToString();
                txtSunAcc.Enabled = false;
                DataTable dt_stus = CHNLSVC.Sales.Get_stus_for_mid(cmbBankName.SelectedValue.ToString(), txtMechantID.Text.Trim().ToString(), _pun_tp);
                int status = Convert.ToInt32(dt_stus.Rows[0]["mstm_stus"]);
                if (status == 1)
                {
                    isUpdate = true;
                    btnClearNew.Text = "Edit";
                    chkActive.Text = "InActive";
                    txtMechantID.ReadOnly = true;

                    pnlsearchmid.Visible = false;
                }
                else
                {
                    isUpdate = true;
                    btnClearNew.Text = "Edit";
                    chkActive.Text = "Active";
                    txtMechantID.ReadOnly = true;

                    pnlsearchmid.Visible = false;
                }
             

            }
        }

        private void dgvSearchMid_KeyDown(object sender, KeyEventArgs e)
        {
          if (e.KeyCode == Keys.Enter)
            {
                int row = dgvSearchMid.CurrentCell.RowIndex;

                txtMechantID.Text = dgvSearchMid["clmMerchant", row].Value.ToString();
                txtSunAcc.Enabled = false;
                DataTable dt_stus = CHNLSVC.Sales.Get_stus_for_mid(cmbBankName.SelectedValue.ToString(), txtMechantID.Text.Trim().ToString(), _pun_tp);
                int status = Convert.ToInt32(dt_stus.Rows[0]["mstm_stus"]);
                if (status == 1)
                {
                    isUpdate = true;
                    btnClearNew.Text = "Edit";
                    chkActive.Text = "InActive";
                    txtMechantID.ReadOnly = true;
                    pnlsearchmid.Visible = false;
                }
                else
                {
                    isUpdate = true;
                    btnClearNew.Text = "Edit";
                    chkActive.Text = "Active";
                    txtMechantID.ReadOnly = true;

                    pnlsearchmid.Visible = false;
                }

            }
        }

        private void btnsearchClose_Click(object sender, EventArgs e)
        {
            dgvSearchMid.DataSource = null;
            pnlsearchmid.Visible = false;

        }

        private List<Deposit_Bank_Pc_wise> fillToMerchantDets()
        {
            _lstMasterPcwiseMid = new List<Deposit_Bank_Pc_wise>();
            List<string> _pcList = new List<string>();

            foreach (ListViewItem itm in lstPC.Items)
            {
                if (itm.Checked)
                {
                    _pcList.Add(itm.Text);
                }
            }

            //save pc
            foreach (string _pc in _pcList)
            {
                //save Mid Details
                foreach (DataGridViewRow dgvr in dgvMerchantDetails.Rows)
                {

                    obj_mids = new Deposit_Bank_Pc_wise();
                    obj_mids.Company = BaseCls.GlbUserComCode;
                    obj_mids.Profit_center = _pc;
                    obj_mids.From_date = Convert.ToDateTime(dgvr.Cells["clmFromDate"].Value);
                    obj_mids.To_date = Convert.ToDateTime(dgvr.Cells["clmTodate"].Value);
                    string d = dgvr.Cells["clmOption"].Value.ToString();
                    if (d == "off line")
                    {
                        _main_puntp = 0;
                    }
                    else
                    {
                        _main_puntp = 1;
                    }

                    obj_mids.Pun_tp = _main_puntp;
                    obj_mids.Period = Convert.ToInt32(dgvr.Cells["clmperiod"].Value);
                    obj_mids.Mid_no = dgvr.Cells["clmMid"].Value.ToString();
                    obj_mids.Create_by = BaseCls.GlbUserID;
                    obj_mids.Bankcharge = Convert.ToDecimal(dgvr.Cells["clmbnkchg"].Value);
                    if (dgvr.Cells["price_tp"].Value !=null)
                    {
                        obj_mids.MPM_PRICE_TYPE = Convert.ToInt32(dgvr.Cells["price_tp"].Value.ToString());    
                    }
                    
                    obj_mids.MPM_GP_MARGIN = Convert.ToDecimal(dgvr.Cells["GP_Margin"].Value);
                    //kapila 9/5/2017 OLD COMMENT
                    //if (dgvr.Cells["clmpb"].Value != null)
                    //    obj_mids.Mpm_pb = dgvr.Cells["clmpb"].Value.ToString();

                    //if (dgvr.Cells["clmpblvl"].Value != null)
                    //obj_mids.Mpm_pblvl = dgvr.Cells["clmpblvl"].Value.ToString();

                    //if (dgvr.Cells["clmbrand"].Value != null)
                    //obj_mids.Mpm_brand = dgvr.Cells["clmbrand"].Value.ToString();

                    //if (dgvr.Cells["clmmaincat"].Value != null)
                    //obj_mids.Mpm_maincat = dgvr.Cells["clmmaincat"].Value.ToString();

                    //if (dgvr.Cells["clmcat"].Value != null)
                    //obj_mids.Mpm_cat = dgvr.Cells["clmcat"].Value.ToString();

                    //if (dgvr.Cells["clmitem"].Value != null)
                    //obj_mids.Mpm_item = dgvr.Cells["clmitem"].Value.ToString();

                    //if (dgvr.Cells["clmpromo"].Value != null)
                    //obj_mids.Mpm_promo = dgvr.Cells["clmpromo"].Value.ToString();


                    //
                    if (dgvr.Cells["clmpb"].Value != null)
                    {
                    obj_mids.Mpm_pb = dgvr.Cells["clmpb"].Value.ToString();
                    }
                    else
                    {
                        obj_mids.Mpm_pb = "N/A";
                    }

                 
               


                    if (dgvr.Cells["clmpblvl"].Value != null)
                    {
                    obj_mids.Mpm_pblvl = dgvr.Cells["clmpblvl"].Value.ToString();
                    }
                    else
                    {
                        obj_mids.Mpm_pblvl = "N/A";
                    }



                    if (dgvr.Cells["clmbrand"].Value != null)
                    {
                    obj_mids.Mpm_brand = dgvr.Cells["clmbrand"].Value.ToString();
                    }
                    else
                    {
                        obj_mids.Mpm_brand = "N/A";
                    }



                    if (dgvr.Cells["clmmaincat"].Value != null)
                    {
                    obj_mids.Mpm_maincat = dgvr.Cells["clmmaincat"].Value.ToString();
                    }
                    else
                    {
                        obj_mids.Mpm_maincat = "N/A";
                    }

                    if (dgvr.Cells["clmcat"].Value != null)
                    {
                    obj_mids.Mpm_cat = dgvr.Cells["clmcat"].Value.ToString();
                    }
                    else
                    {
                        obj_mids.Mpm_cat = "N/A";
                    }

                    if (dgvr.Cells["clmitem"].Value != null)
                    {
                    obj_mids.Mpm_item = dgvr.Cells["clmitem"].Value.ToString();
                    }
                    else
                    {
                        obj_mids.Mpm_item = "N/A";
                    }

                    if (dgvr.Cells["clmpromo"].Value != null)
                    {
                    obj_mids.Mpm_promo = dgvr.Cells["clmpromo"].Value.ToString();
                    }
                    else
                    {
                        obj_mids.Mpm_promo = "N/A";
                    }

                    _lstMasterPcwiseMid.Add(obj_mids);


                }

            }

            if (_pcList.Count <= 0)
            {
                MessageBox.Show("Please select applicable profit center(s)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (_lstMasterPcwiseMid.Count <= 0)
            {
                MessageBox.Show("Please add merchant number details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }



            return _lstMasterPcwiseMid;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dgvMerchantDetails.Rows.Count > 0)
            {
                _lstMasterPcwiseMid = new List<Deposit_Bank_Pc_wise>();

                #region validation

                if (lstPC.Items.Count <= 0)
                {
                    MessageBox.Show("Please select applicable profit center(s)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion

                _lstMasterPcwiseMid = fillToMerchantDets();
                if (_lstMasterPcwiseMid.Count > 0)
                {
                    //if (MessageBox.Show("Are you sure ?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

                    if (MessageBox.Show("Are you sure want to Save ?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                    string _error = "";
                    int result = CHNLSVC.Sales.SaveToMasterPcWiseMid(_lstMasterPcwiseMid, out _error);
                    if (result == -1)
                    {
                        MessageBox.Show("Error occurred while processing\n" + _error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Records inserted Successfully", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        btnClear_Click(null, null);
                        
                    }
                }
            }
            else
            {
                MessageBox.Show("Please add merchant number details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void rdoOnline_CheckedChanged(object sender, EventArgs e)
        {

            loadMidDetails();
        }

        private void rdoOffline_CheckedChanged(object sender, EventArgs e)
        {
            loadMidDetails();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtArea.Text = "";
            txtChanel.Text = "";
            txtComp.Text = "";
            txtCompAddr.Text = "";
            txtCompDesc.Text = "";
            txtPC.Text = "";
            txtPCDesn.Text = "";
            txtRegion.Text = "";
            txtSChanel.Text = "";
            txtZone.Text = "";
            txtbankchg.Text = "";
            lstPC.Items.Clear();
            txtComp.Text = BaseCls.GlbUserComCode;

            txtperiod.Text = "";
            cmbMainMidNo.SelectedIndex = -1;
            List<Deposit_Bank_Pc_wise> clr = new List<Deposit_Bank_Pc_wise>();
           
           
            dgvMerchantDetails.AutoGenerateColumns = false;
            dgvMerchantDetails.DataSource = clr;
            dgvMerchantDetails.DataSource = null;
            dtpFromDate.Value = dtpFromDate.MinDate;
            dtpTodate.Value = dtpTodate.MinDate;

            dgvMerchantDetails.Rows.Clear();
            PBList = new List<PriceBookLevelRef>();
            ItemBrandCat_List = new List<CashCommissionDetailRef>();

            dataGridViewPriceBook.AutoGenerateColumns = false;
            dataGridViewPriceBook.DataSource = PBList;
            grvSalesTypes.AutoGenerateColumns = false;
            grvSalesTypes.DataSource = ItemBrandCat_List;
            txtFileName_rd.Text = string.Empty;
            txtPriceBook.Text = string.Empty;
            txtLevel.Text = string.Empty;

            txtBrand.Text = "";
            txtItemCD.Text = "";
            txtCate1.Text = "";
            txtCate2.Text = "";
            txtCate3.Text = "";
            txtCircular.Text = "";
            txtFileName_rd.Text = "";
            txtPromotion.Text = "";
            txtCate1.Enabled = false;
            txtBrand.Enabled = false;
            txtItemCD.Enabled = false;
            txtPromotion.Enabled = false;
            txtCircular.Enabled = false;
            cmbPriceType.Text = "";
            cmbPriceType.SelectedValue = -1;
            txtgp.Text = string.Empty;
        }

        private void btnInactive_Click(object sender, EventArgs e)
        {
            pnlInactivate.Visible = true;
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int _tp_in;

            if (rdoInOfline.Checked)
            {
               _tp_in = 0;
            }   
            else
            {
               _tp_in = 1;
            }


            #region validation

            if (txtprof.Text == "")
            {
                MessageBox.Show("Please select profit center", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbTerm.SelectedIndex == -1)
            {
                MessageBox.Show("Please select term", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbMidIn.SelectedIndex == -1)
            {
                MessageBox.Show("Please select merchant id number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion

                if (MessageBox.Show("Are you sure want to update ?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                string _error = "";
            int result = CHNLSVC.Sales.Update_to_mst_pc_mid(BaseCls.GlbUserComCode, txtprof.Text.Trim(), _tp_in, Convert.ToInt32(cmbTerm.SelectedValue), cmbMidIn.Text, BaseCls.GlbUserID, out _error);
                if (result == -1)
                {
                    MessageBox.Show("Error occurred while processing\n" + _error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    MessageBox.Show("Records updated Successfully", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //btnClear_Click(null, null);
                    clearInfilds();
                    pnlInactivate.Visible = false;
                }
            
    
          
        }

        private void clearInfilds()
        {
            txtprof.Text = "";
            cmbTerm.DataSource = null;
            cmbMidIn.DataSource = null;
           

        }

        private void btnInactClear_Click(object sender, EventArgs e)
        {
            clearInfilds();
        }

        private void ImgBtnPC_Click(object sender, EventArgs e)
        {
            try
            {
                txtprof.Text = "";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtprof;
                _CommonSearch.txtSearchbyword.Text = txtprof.Text;
                _CommonSearch.ShowDialog();
                txtprof.Focus();

                // get_PCDet();
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
            
            pnlInactivate.Visible = false;
        }

        private void txtProfitCenter_DoubleClick(object sender, EventArgs e)
        {
            //try
            //{
            //    txtProfitCenter.Text = "";
            //    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            //    _CommonSearch.ReturnIndex = 0;
            //    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
            //    DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
            //    _CommonSearch.dvResult.DataSource = _result;
            //    _CommonSearch.BindUCtrlDDLData(_result);
            //    _CommonSearch.obj_TragetTextBox = txtProfitCenter;
            //    _CommonSearch.txtSearchbyword.Text = txtProfitCenter.Text;
            //    _CommonSearch.ShowDialog();
            //    txtProfitCenter.Focus();

            //    // get_PCDet();
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


        private void loadTerms()
        {
            int _pun_tp_in;

            if (rdoInOfline.Checked)
            {
               _pun_tp_in = 0;
            }   
            else
            {
               _pun_tp_in = 1;
            }

            cmbTerm.SelectedIndexChanged -= new EventHandler(cmbTerm_SelectedIndexChanged);
            DataTable dt = CHNLSVC.Sales.get_Terms(BaseCls.GlbUserComCode, _pun_tp_in, txtprof.Text.Trim());
            if (dt != null && dt.Rows.Count > 0)
            {

                cmbTerm.DataSource = dt;
                cmbTerm.DisplayMember = "MPM_TERM";
                cmbTerm.ValueMember = "MPM_TERM";
                cmbTerm.SelectedIndex = -1;
                cmbTerm.SelectedIndexChanged += new EventHandler(cmbTerm_SelectedIndexChanged);

            }
            else
            {
                cmbTerm.DataSource = null;
            }

        }
        private void loadmidsForIn()
        {
            int _pun_tp_in_mid;

            if (rdoInOfline.Checked)
            {
                _pun_tp_in_mid = 0;
            }
            else
            {
                _pun_tp_in_mid = 1;
            }

            DataTable dt = CHNLSVC.Sales.get_mids_forIn(BaseCls.GlbUserComCode, _pun_tp_in_mid, txtprof.Text.Trim(), Convert.ToInt32(cmbTerm.SelectedValue));
            if (dt != null && dt.Rows.Count > 0)
            {

                cmbMidIn.DataSource = dt;
                cmbMidIn.DisplayMember = "mpm_mid_no";
                cmbMidIn.ValueMember = "mpm_mid_no";
                cmbMidIn.SelectedIndex = -1;


            }
            else
            {
                cmbMidIn.DataSource = null;
            }

        }


        private void txtProfitCenter_Leave(object sender, EventArgs e)
        {
            

            //if (!string.IsNullOrEmpty(txtProfitCenter.Text))
            //{
            //    Boolean _IsValid = CHNLSVC.Sales.IsvalidPC(BaseCls.GlbUserComCode, txtProfitCenter.Text);
            //    if (_IsValid == false)
            //    {
            //        MessageBox.Show("Invalid Profit Center.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //        txtProfitCenter.Focus();
            //        return;
            //    }

            //    loadTerms();
            //    cmbTerm.Focus();


            //}




        }

        private void txtProfitCenter_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //    loadTerms();
            //    cmbTerm.Focus();

            //if (e.KeyCode == Keys.F2)
            //{
            //    txtProfitCenter_DoubleClick(null, null);
            //}
        }

        private void cmbTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTerm.SelectedIndex != -1)
           {
             loadmidsForIn();
             cmbMidIn.Focus();
           }
        }

        private void rdoInOnline_CheckedChanged(object sender, EventArgs e)
        {
            loadTerms();
        }

        private void rdoInOfline_CheckedChanged(object sender, EventArgs e)
        {
            loadTerms();
        }

        private void cmbMidIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnUpdate.Focus();
        }

        private void txtprof_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                txtprof.Text = "";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtprof;
                _CommonSearch.txtSearchbyword.Text = txtprof.Text;
                _CommonSearch.ShowDialog();
                txtprof.Focus();

                // get_PCDet();
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

        private void txtprof_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtprof.Text))
            {
                Boolean _IsValid = CHNLSVC.Sales.IsvalidPC(BaseCls.GlbUserComCode, txtprof.Text);
                if (_IsValid == false)
                {
                    MessageBox.Show("Invalid Profit Center.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtprof.Text = "";
                    txtprof.Focus();
                    return;
                }

                loadTerms();
                cmbTerm.Focus();


            }

        }

        private void txtprof_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loadTerms();
                rdoInOfline.Focus();
            }

            else if (e.KeyCode == Keys.F2)
            {
                txtprof_DoubleClick(null, null);
            }

        }

        private void btnAddPB_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewPriceBook.AutoGenerateColumns = false;
                List<PriceBookLevelRef> pbLIST = new List<PriceBookLevelRef>();
                if (txtLevel.Text != "" && txtPriceBook.Text != "")
                {
                    List<PriceBookLevelRef> tem = (from _res in PBList
                                                   where _res.Sapl_pb == txtPriceBook.Text && _res.Sapl_pb_lvl_cd == txtLevel.Text
                                                   select _res).ToList<PriceBookLevelRef>();
                    if (tem != null && tem.Count > 0)
                    {
                        MessageBox.Show("Selected price book, level already in the list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else if (txtPriceBook.Text != "")
                {
                    List<PriceBookLevelRef> tem = (from _res in PBList
                                                   where _res.Sapl_pb == txtPriceBook.Text
                                                   select _res).ToList<PriceBookLevelRef>();
                    if (tem != null && tem.Count > 0)
                    {
                        MessageBox.Show("Selected price book already in the list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                pbLIST = (CHNLSVC.Sales.GetPriceLevelList(BaseCls.GlbUserComCode, txtPriceBook.Text.Trim().ToUpper(), txtLevel.Text.Trim().ToUpper()));

                pbLIST.RemoveAll(x => x.Sapl_act == false);
                foreach (PriceBookLevelRef item in pbLIST)
                {
                    item.price_type = Convert.ToInt16(cmbPriceType.SelectedValue);
                    item.price_type_desc = cmbPriceType.Text.ToString();
                }
                var distinctList = pbLIST.GroupBy(x => x.Sapl_pb_lvl_cd)
                             .Select(g => g.First())
                             .ToList();
                if (distinctList == null || distinctList.Count <= 0)
                {
                    MessageBox.Show("Invalid Price book or Level", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
               
                PBList.AddRange(distinctList);





                BindingSource source = new BindingSource();
                source.DataSource = PBList;
                dataGridViewPriceBook.DataSource = source;
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

        private void btnSearchPB_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPriceBook;
                _CommonSearch.txtSearchbyword.Text = txtPriceBook.Text;
                _CommonSearch.ShowDialog();
                txtPriceBook.Focus();
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

        private void btnSearchPriceLvl_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtLevel;
                _CommonSearch.txtSearchbyword.Text = txtLevel.Text;
                _CommonSearch.ShowDialog();
                txtLevel.Focus();
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

        private void txtPriceBook_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearchPB_Click(null, null);
        }

        private void txtLevel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearchPriceLvl_Click(null, null);
        }

        private void btnAddCat_Click(object sender, EventArgs e)
        {
            try
            {

                if (cmbSelectCat.SelectedItem == null || cmbSelectCat.SelectedItem.ToString() == "")
                {
                    MessageBox.Show("Please select Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cmbSelectCat.SelectedItem.ToString() == "BRAND_CATE1" || cmbSelectCat.SelectedItem.ToString() == "BRAND_CATE2")
                {
                    if (txtBrand.Text == string.Empty)
                    {
                        MessageBox.Show("Specify brand also!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                string selection = "";
                if (cmbSelectCat.SelectedValue.ToString() == "10")
                {
                    selection = "CATE1";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "9")
                {
                    selection = "CATE2";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "8")
                {
                    selection = "CATE3";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "7")
                {
                    selection = "BRAND";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "6")
                {
                    selection = "BRAND_CATE1";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "5")
                {
                    selection = "BRAND_CATE2";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "4")
                {
                    selection = "BRAND_CATE3";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "3")
                {
                    selection = "ITEM";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "2")
                {
                    selection = "PROMOTION";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "1")
                {
                    selection = "SERIAL";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "11")
                {
                    selection = "ITEM_PROMO";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "12")
                {
                    selection = "CIRCULARS";
                }
               
                //ItemBrandCat_List = new List<CashCommissionDetailRef>();
                DataTable dt = new DataTable();
                DataTable dtnew = new DataTable();
                List<CashCommissionDetailRef> addList = new List<CashCommissionDetailRef>();
                if (string.IsNullOrEmpty(txtCircular.Text))
                {
                    dt = CHNLSVC.Sales.GetBrandsCatsItems(selection, txtBrand.Text.Trim(), txtCate1.Text.Trim(), txtCate2.Text.Trim(), txtCate3.Text, txtItemCD.Text.Trim(), null, "", txtPromotion.Text.Trim());
                    if (dt.Rows.Count > 0)
                    {
                        grvSalesTypes.Columns[1].HeaderText = cmbSelectCat.Text;
                    }
                    //List<CashCommissionDetailRef> addList = new List<CashCommissionDetailRef>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        string code = dr["code"].ToString();
                        string brand = txtBrand.Text;
                        CashCommissionDetailRef obj = new CashCommissionDetailRef(); //for display purpose
                        if (cmbSelectCat.SelectedItem.ToString() == "BRAND_CATE1" || cmbSelectCat.SelectedItem.ToString() == "BRAND_CATE2" || cmbSelectCat.SelectedItem.ToString() == "BRAND_CATE3")
                        {
                            obj.Sccd_brd = brand;
                        }
                        else
                        {
                            obj.Sccd_brd = "N/A";
                        }

                        obj.Sccd_itm = code;
                        try
                        {
                            obj.Sccd_ser = dr["descript"].ToString();
                        }
                        catch (Exception)
                        {
                            obj.Sccd_ser = "";
                        }

                        var _duplicate = from _dup in ItemBrandCat_List
                                         where _dup.Sccd_itm == obj.Sccd_itm && _dup.Sccd_brd == obj.Sccd_brd
                                         select _dup;

                        if (_duplicate.Count() == 0)
                        {
                            addList.Add(obj);
                        }
                        if (_duplicate.Count() > 0)
                        {
                            MessageBox.Show("Duplicate record", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                    }
                    if (addList == null || addList.Count <= 0)
                    {
                        MessageBox.Show("Invalid search term, no data found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    grvSalesTypes.AutoGenerateColumns = false;
                    ItemBrandCat_List.AddRange(addList);
                    BindingSource source = new BindingSource();
                    source.DataSource = ItemBrandCat_List;
                    grvSalesTypes.AutoGenerateColumns = false;
                    grvSalesTypes.DataSource = source;
                    if (dt.Rows.Count > 0)
                    {
                        grvSalesTypes.Columns[1].HeaderText = cmbSelectCat.Text;
                    }

                    foreach (DataGridViewRow grv in grvSalesTypes.Rows)
                    {
                        DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)grv.Cells[0];
                        cell.Value = "True";
                    }

                    txtItemCD.Text = "";
                    txtCate1.Text = "";
                    txtCate2.Text = "";
                    txtCate3.Text = "";

                    txtPromotion.Text = "";

                }
                else
                {
                    dtnew = CHNLSVC.Sales.GetPromoCodesByCirnew(txtCircular.Text, null, null, null);
                    if (dtnew.Rows.Count > 0)
                    {
                       
                        grvSalesTypes.Columns[1].HeaderText = cmbSelectCat.Text;

                        foreach (DataRow drow in dtnew.Rows)
                        {
                            CashCommissionDetailRef obj = new CashCommissionDetailRef(); //for display purpose
                            //obj.Sccd_itm = drow["sapd_promo_cd"].ToString(); ;
                            obj.Sccd_itm = drow["sapd_itm_cd"].ToString();
                            try
                            {
                                obj.Sccd_ser = drow["SAPD_CIRCULAR_NO"].ToString();
                            }
                            catch (Exception)
                            {
                                obj.Sccd_ser = "";
                            }
                          
                            //addList.Add(drow["sapd_promo_cd"].ToString());
                            addList.Add(obj);
                        }
                        grvSalesTypes.AutoGenerateColumns = false;
                        ItemBrandCat_List.AddRange(addList);
                        BindingSource source = new BindingSource();
                        source.DataSource = ItemBrandCat_List;
                        grvSalesTypes.AutoGenerateColumns = false;
                        grvSalesTypes.DataSource = source;
                        txtCircular.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Please check enter circluar #.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCircular.Text = "";
                        txtCircular.Focus();
                        return;
                    }
                }
                //dt = CHNLSVC.Sales.GetBrandsCatsItems(selection, txtBrand.Text.Trim(), txtCate1.Text.Trim(), txtCate2.Text.Trim(), txtCate3.Text, txtItemCD.Text.Trim(),null, "", txtPromotion.Text.Trim());



                //var _lst = dt.AsEnumerable().Select(X => X.Field<string>("esep_cat_cd")).Distinct().ToList();


                #region commnt by tharanga 2018/04/03
                //if (dt.Rows.Count > 0)
                //{
                //    grvSalesTypes.Columns[1].HeaderText = cmbSelectCat.Text;
                //}
                ////List<CashCommissionDetailRef> addList = new List<CashCommissionDetailRef>();
                //foreach (DataRow dr in dt.Rows)
                //{
                //    string code = dr["code"].ToString();
                //    string brand = txtBrand.Text;
                //    CashCommissionDetailRef obj = new CashCommissionDetailRef(); //for display purpose
                //    if (cmbSelectCat.SelectedItem.ToString() == "BRAND_CATE1" || cmbSelectCat.SelectedItem.ToString() == "BRAND_CATE2" || cmbSelectCat.SelectedItem.ToString() == "BRAND_CATE3")
                //    {
                //        obj.Sccd_brd = brand;
                //    }
                //    else
                //    {
                //        obj.Sccd_brd = "N/A";
                //    }

                //    obj.Sccd_itm = code;
                //    try
                //    {
                //        obj.Sccd_ser = dr["descript"].ToString();
                //    }
                //    catch (Exception)
                //    {
                //        obj.Sccd_ser = "";
                //    }

                //    var _duplicate = from _dup in ItemBrandCat_List
                //                     where _dup.Sccd_itm == obj.Sccd_itm && _dup.Sccd_brd == obj.Sccd_brd
                //                     select _dup;

                //    if (_duplicate.Count() == 0)
                //    {
                //        addList.Add(obj);
                //    }
                //    if (_duplicate.Count() > 0)
                //    {
                //        MessageBox.Show("Duplicate record", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        return;
                //    }

                //}
                //if (addList == null || addList.Count <= 0)
                //{
                //    MessageBox.Show("Invalid search term, no data found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
                //grvSalesTypes.AutoGenerateColumns = false;
                //ItemBrandCat_List.AddRange(addList);
                //BindingSource source = new BindingSource();
                //source.DataSource = ItemBrandCat_List;
                //grvSalesTypes.AutoGenerateColumns = false;
                //grvSalesTypes.DataSource = source;
                //if (dt.Rows.Count > 0)
                //{
                //    grvSalesTypes.Columns[1].HeaderText = cmbSelectCat.Text;
                //}

                //foreach (DataGridViewRow grv in grvSalesTypes.Rows)
                //{
                //    DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)grv.Cells[0];
                //    cell.Value = "True";
                //}

                //txtItemCD.Text = "";
                //txtCate1.Text = "";
                //txtCate2.Text = "";
                //txtCate3.Text = "";
          
                //txtPromotion.Text = "";
#endregion commnt by tharanga
                //foreach (DataGridViewRow gr in grvSalesTypes.Rows)
                //{
                //    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)grvSalesTypes.Rows[gr.Index].Cells[0];
                //    chk.Value = "true";
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

        private void btnItemAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gr in grvSalesTypes.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)grvSalesTypes.Rows[gr.Index].Cells[0];
                chk.Value = "True";
            }
        }

        private void btnItemNone_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gr in grvSalesTypes.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)grvSalesTypes.Rows[gr.Index].Cells[0];
                chk.Value = "False";
            }
        }

        private void btnItemClear_Click(object sender, EventArgs e)
        {
            grvSalesTypes.DataSource = null;
            ItemBrandCat_List = new List<CashCommissionDetailRef>();
        }

        private void btnBrand_Click(object sender, EventArgs e)
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
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnMainCat_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCate1;
                _CommonSearch.txtSearchbyword.Text = txtCate1.Text;
                _CommonSearch.ShowDialog();
                txtCate1.Focus();
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

        private void btnCat_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCate2;
                _CommonSearch.txtSearchbyword.Text = txtCate2.Text;
                _CommonSearch.ShowDialog();
                txtCate2.Focus();
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

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCate3;
                _CommonSearch.ShowDialog();
                txtCate3.Focus();
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

        private void btnItem_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItemCD;
                _CommonSearch.ShowDialog();
                txtItemCD.Select();
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

        private void btnPromation_Click(object sender, EventArgs e)
        {

            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotion);
                DataTable _result = CHNLSVC.CommonSearch.GetPromotionSearch(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPromotion;
                _CommonSearch.txtSearchbyword.Text = txtPromotion.Text;
                _CommonSearch.ShowDialog();
                txtPromotion.Focus();
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

        private void dataGridViewPriceBook_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        PBList.RemoveAt(e.RowIndex);
                        BindingSource source = new BindingSource();
                        source.DataSource = PBList;
                        dataGridViewPriceBook.DataSource = source;
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

        private void cmbSelectCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBrand.Text = "";
            txtItemCD.Text = "";
            txtCate1.Text = "";
            txtCate2.Text = "";
            txtCate3.Text = "";
            txtCircular.Text = "";
            txtFileName_rd.Text = "";
            txtCate1.Enabled = false;
            txtBrand.Enabled = false;
            txtItemCD.Enabled = false;
            txtPromotion.Enabled = false;
            txtCircular.Enabled = false;

            if (cmbSelectCat.SelectedValue.ToString() == "10")
                txtCate1.Enabled = true;
            
            else if (cmbSelectCat.SelectedValue.ToString() == "7")
                txtBrand.Enabled = true;
            else if (cmbSelectCat.SelectedValue.ToString() == "6")
            {
                txtBrand.Enabled = true;
                txtCate1.Enabled = true;
            }
           
            else if (cmbSelectCat.SelectedValue.ToString() == "3")
                txtItemCD.Enabled = true;
            else if (cmbSelectCat.SelectedValue.ToString() == "2")
                txtPromotion.Enabled = true;

            else if (cmbSelectCat.SelectedValue.ToString() == "11")
            {
                txtItemCD.Enabled = true;
                txtPromotion.Enabled = true;
            }
            else if (cmbSelectCat.SelectedValue.ToString() == "12")
                txtCircular.Enabled = true;

            txtPromotion.Text = "";
            grvSalesTypes.DataSource = null;
            ItemBrandCat_List = new List<CashCommissionDetailRef>();
        }

        private void BindCategoryTypes()
        {

            Dictionary<int, string> categoryType = new Dictionary<int, string>();
            //categoryType.Add(1, "Serial");
            categoryType.Add(2, "Promotion");
            categoryType.Add(3, "Item");
            //categoryType.Add(4, "Brand & Sub cat");
            //categoryType.Add(5, "Brand & Cat");
            categoryType.Add(6, "Brand & main cat");
            categoryType.Add(7, "Brand");
            //categoryType.Add(8, "Sub cat");
            //categoryType.Add(9, "Cat");
            categoryType.Add(10, "Main cat");
            categoryType.Add(11, "Item & Promotion");
            categoryType.Add(12, "CIRCULARS");


            cmbSelectCat.DataSource = new BindingSource(categoryType, null);
            cmbSelectCat.DisplayMember = "Value";
            cmbSelectCat.ValueMember = "Key";

        }
        private void load_bank()
        {
            DataTable dt = CHNLSVC.Sales.LoadBankNewDets();
            if (dt != null && dt.Rows.Count > 0)
            {

                cmbbanknew.DataSource = dt;
                cmbbanknew.DisplayMember = "MBI_DESC";
                cmbbanknew.ValueMember = "MBI_ID";
                cmbbanknew.Text = "---Select a Bank---";

            }
            else
            {
                cmbbanknew.DataSource = null;
            }
        }

        private void cmbMainMidNo_Click(object sender, EventArgs e)
        {
          
        }

        private void cmbbanknew_Leave(object sender, EventArgs e)
        {
            loadMidDetails();
        }

        private void btnSearchCirclar_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
              
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Circular);
                

                DataTable _resultTemp = CHNLSVC.CommonSearch.GetPromotionSearch(_CommonSearch.SearchParams, null, null);

                DataTable _result = _resultTemp.Clone();
               
                    _result.Merge(_resultTemp);
                

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.dvResult.Columns["STATUS"].Visible = false;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.obj_TragetTextBox = txtCircular;
                _CommonSearch.ShowDialog();
                txtCircular.Select();
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

        private void txtCircular_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCircular_DoubleClick(object sender, EventArgs e)
        {

            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
              
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Circular);
                

                DataTable _resultTemp = CHNLSVC.CommonSearch.GetPromotionSearch(_CommonSearch.SearchParams, null, null);

                DataTable _result = _resultTemp.Clone();
               
                    _result.Merge(_resultTemp);
                
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.dvResult.Columns["STATUS"].Visible = false;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCircular;
                _CommonSearch.ShowDialog();
                txtCircular.Select();
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

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSearchFile_srd_Click(object sender, EventArgs e)
        {
            txtFileName_rd.Text = string.Empty;
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "txt files (*.xls)|*.xls,*.xlsx|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.ShowDialog();
            string[] _obj = openFileDialog1.FileName.Split('\\');
            txtFileName_rd.Text = openFileDialog1.FileName;
        }

        private void btnUploadFile_rd_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder _errorLst = new StringBuilder();
                string _msg = string.Empty;
                ReminderLetter _ltr = new ReminderLetter();
                if (string.IsNullOrEmpty(txtFileName_rd.Text))
                {
                    MessageBox.Show("Please select upload file path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFileName_rd.Clear();
                    txtFileName_rd.Focus();
                    return;
                }

                System.IO.FileInfo fileObj = new System.IO.FileInfo(txtFileName_rd.Text);
                if (fileObj.Exists == false)
                {
                    MessageBox.Show("Selected file does not exist at the following path.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //btnGvBrowse.Focus();
                    return;
                }

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
                else
                {
                    MessageBox.Show("Invalid upload file Format", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFileName_rd.Text = "";
                    return;
                }
                string _excelConnectionString = ConfigurationManager.ConnectionStrings[Extension == ".XLS" ? "ConStringExcel03" : "ConStringExcel07"].ConnectionString;


                _excelConnectionString = String.Format(conStr, txtFileName_rd.Text, "YES");
                OleDbConnection connExcel = new OleDbConnection(_excelConnectionString);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable _dt = new DataTable();
                cmdExcel.Connection = connExcel;
                try
                {
                    connExcel.Open();
                }
                catch (Exception)
                {

                    MessageBox.Show("First Colse the Excle file. ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                connExcel.Close();

                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(_dt);
                connExcel.Close();
                //StringBuilder _errorLst = new StringBuilder();
                List<CashCommissionDetailRef> addList = new List<CashCommissionDetailRef>();
                ItemBrandCat_List = new List<CashCommissionDetailRef>();
                if (_dt == null || _dt.Rows.Count <= 0) { MessageBox.Show("The excel file is empty. Please check the file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }


                if (_dt == null || _dt.Rows.Count <= 0) { MessageBox.Show("The excel file is empty. Please check the file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

                if (_dt.Rows.Count > 0)
                {

                    foreach (DataRow _dr in _dt.Rows)
                    {
                        CashCommissionDetailRef obj = new CashCommissionDetailRef(); //for display purpose
                        addList = new List<CashCommissionDetailRef>();
                        if (cmbSelectCat.Text == "Brand")
                        {
                            #region Brand
                            if (string.IsNullOrEmpty(_dr["BRAND"].ToString().Trim())) continue;
                            MasterItemBrand _brd = CHNLSVC.Sales.GetItemBrand(_dr["BRAND"].ToString().Trim()); //BRAND


                            if (_brd.Mb_cd == null)
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid brand - " + _dr["BRAND"].ToString().Trim());
                                else _errorLst.Append(" and invalid brand - " + _dr["BRAND"].ToString().Trim());
                                continue;
                            }
                            obj.Sccd_itm = _brd.Mb_cd;
                            obj.Sccd_ser = _brd.Mb_desc;
                            var _dup = ItemBrandCat_List.Where(r => r.Sccd_itm == _brd.Mb_cd).ToList();
                            if (_dup != null && _dup.Count > 1)
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("brand " + _dr["BRAND"].ToString().Trim() + " duplicate");
                                else _errorLst.Append(" and brand " + _dr["BRAND"].ToString().Trim() + " duplicate");
                                continue;
                            }
                            addList.Add(obj);
                            ItemBrandCat_List.AddRange(addList);

                            ////var _dup = ItemBrandCat_List.Where(x => x. == _dr[1].ToString()).ToList();
                            //if (_dup != null && _dup.Count > 1)
                            //{
                            //    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("brand " + _dr[1].ToString() + " duplicate");
                            //    else _errorLst.Append(" and brand " + _dr[1].ToString() + " duplicate");
                            //    continue;
                            //}


                            //ItemBrandCat_List.Items.Add(_dr[1].ToString().Trim());

                            #endregion

                        }
                        else if (cmbSelectCat.Text == "Main cat")
                        {
                            #region  Main cat
                            if (string.IsNullOrEmpty(_dr["MAIN CAT"].ToString().Trim())) continue;

                            DataTable _categoryDet = CHNLSVC.General.GetMainCategoryDetail(_dr["MAIN CAT"].ToString().Trim());

                            if (_categoryDet.Rows.Count <= 0)
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid main category - " + _dr["MAIN CAT"].ToString().Trim());
                                else _errorLst.Append(" and invalid main category  - " + _dr["MAIN CAT"].ToString().Trim());
                                continue;
                            }
                            obj.Sccd_itm = _categoryDet.Rows[0]["RIC1_CD"].ToString();// _dr[0].ToString();
                            obj.Sccd_ser = _categoryDet.Rows[0]["RIC1_DESC"].ToString();// _dr[0].ToString();
                            var _dup = ItemBrandCat_List.Where(r => r.Sccd_itm == obj.Sccd_itm).ToList();
                            if (_dup != null && _dup.Count > 1)
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("main category " + _dr["MAIN CAT"].ToString().Trim() + " duplicate");
                                else _errorLst.Append(" and main category " + _dr["MAIN CAT"].ToString().Trim() + " duplicate");
                                continue;
                            }
                            addList.Add(obj);
                            ItemBrandCat_List.AddRange(addList);
                            #endregion
                        }
                        else if (cmbSelectCat.Text == "Brand & main cat")
                        {
                            DataTable dt = CHNLSVC.Sales.GetBrandsCatsItems("BRAND_CATE1", _dr["BRAND"].ToString().Trim(), _dr["MAIN CAT"].ToString().Trim(), txtCate2.Text.Trim(), txtCate3.Text, txtItemCD.Text.Trim(), null, "", txtPromotion.Text.Trim());
                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    addList = new List<CashCommissionDetailRef>();
                                    obj = new CashCommissionDetailRef(); //for display purpose
                                    string code = dr["code"].ToString();
                                    obj.Sccd_itm = code;
                                    try
                                    {
                                        obj.Sccd_ser = dr["descript"].ToString();
                                    }
                                    catch (Exception)
                                    {
                                        obj.Sccd_ser = "";
                                    }
                                    var _dup = ItemBrandCat_List.Where(r => r.Sccd_itm == code).ToList();
                                    if (_dup != null && _dup.Count > 1)
                                    {
                                        if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("main category " + _dr["BRAND"].ToString() + " duplicate");
                                        else _errorLst.Append(" and main category " + _dr["BRAND"].ToString() + " duplicate");
                                        continue;
                                    }

                                    addList.Add(obj);
                                    ItemBrandCat_List.AddRange(addList);
                                }
                                // grvSalesTypes.Columns[1].HeaderText = cmbSelectCat.Text;
                            }

                            else
                            {

                            }
                        }
                        else if (cmbSelectCat.Text == "Item")
                        {
                            //DataTable dtCompanyItems = channelService.General.GetCompanyItemsByCompany(BaseCls.GlbUserComCode);
                            if (string.IsNullOrEmpty(_dr["ITEM"].ToString().Trim())) continue;

                            MasterItem _item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _dr["ITEM"].ToString().Trim());
                            if (_item == null)
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid item - " + _dr[0].ToString());
                                else _errorLst.Append(" and invalid item - " + _dr["ITEM"].ToString().Trim());
                                continue;
                            }

                            obj.Sccd_itm = _item.Mi_cd;
                            obj.Sccd_ser = _item.Mi_shortdesc;
                            var _dup = ItemBrandCat_List.Where(r => r.Sccd_itm == obj.Sccd_itm).ToList();
                            if (_dup != null && _dup.Count > 1)
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("Item " + _dr["ITEM"].ToString().Trim() + " duplicate");
                                else _errorLst.Append(" and Item " + _dr["ITEM"].ToString().Trim() + " duplicate");
                                continue;
                            }
                            addList.Add(obj);
                            ItemBrandCat_List.AddRange(addList);
                            //var _dup = _itemLst.Where(x => x == _dr[0].ToString()).ToList();
                            //if (_dup != null && _dup.Count > 0)
                            //{
                            //    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("item " + _dr[0].ToString() + " duplicate");
                            //    else _errorLst.Append(" and item " + _dr[0].ToString() + " duplicate");
                            //    continue;
                            //}



                        }
                        else if (cmbSelectCat.Text == "Promotion")
                        {

                            string promo = _dr["PROMOTION"].ToString().Trim();

                            if (string.IsNullOrEmpty(promo))
                            {
                                MessageBox.Show("invalid Promotion Code ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            DataTable dt = CHNLSVC.Sales.GetBrandsCatsItems("PROMOTION", null, null, null, null, null, null, "", _dr["PROMOTION"].ToString().Trim());
                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    addList = new List<CashCommissionDetailRef>();
                                    obj = new CashCommissionDetailRef(); //for display purpose
                                    string code = dr["code"].ToString();
                                    obj.Sccd_itm = code;
                                    try
                                    {
                                        obj.Sccd_ser = dr["descript"].ToString();
                                    }
                                    catch (Exception)
                                    {
                                        obj.Sccd_ser = "";
                                    }
                                    var _dup = ItemBrandCat_List.Where(r => r.Sccd_itm == code).ToList();
                                    if (_dup != null && _dup.Count > 1)
                                    {
                                        if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("Promotion " + _dr["PROMOTION"].ToString().Trim() + " duplicate");
                                        else _errorLst.Append(" and Promotion " + _dr["PROMOTION"].ToString().Trim() + " duplicate");
                                        continue;
                                    }

                                    addList.Add(obj);
                                    ItemBrandCat_List.AddRange(addList);
                                }
                            }
                        }
                        else if (cmbSelectCat.Text == "Item & Promotion")
                        {
                            DataTable dt = new DataTable();
                            dt = CHNLSVC.Sales.GetBrandsCatsItems("ITEM_PROMO", null, null, null, null, _dr["ITEM"].ToString().Trim(), null, "", _dr["PROMOTION"].ToString().Trim());
                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    addList = new List<CashCommissionDetailRef>();
                                    obj = new CashCommissionDetailRef(); //for display purpose
                                    string code = dr["code"].ToString();
                                    obj.Sccd_itm = code;
                                    try
                                    {
                                        obj.Sccd_ser = dr["descript"].ToString();
                                    }
                                    catch (Exception)
                                    {
                                        obj.Sccd_ser = "";
                                    }
                                    var _dup = ItemBrandCat_List.Where(r => r.Sccd_itm == code).ToList();
                                    if (_dup != null && _dup.Count > 1)
                                    {
                                        if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("Promotion " + _dr["PROMOTION"].ToString().Trim() + " duplicate");
                                        else _errorLst.Append(" and Promotion " + _dr["PROMOTION"].ToString().Trim() + " duplicate");
                                        continue;
                                    }

                                    addList.Add(obj);
                                    ItemBrandCat_List.AddRange(addList);
                                }
                            }
                        }
                        else if (cmbSelectCat.Text == "CIRCULARS")
                        {
                            DataTable dtnew = CHNLSVC.Sales.GetPromoCodesByCirnew(_dr["CIRCULARS"].ToString().Trim(), null, null, null);
                            if (dtnew.Rows.Count > 0)
                            {

                                grvSalesTypes.Columns[1].HeaderText = cmbSelectCat.Text;

                                foreach (DataRow drow in dtnew.Rows)
                                {
                                    addList = new List<CashCommissionDetailRef>();
                                    obj = new CashCommissionDetailRef(); //for display purpose
                                    //obj.Sccd_itm = drow["sapd_promo_cd"].ToString(); ;
                                    obj.Sccd_itm = drow["sapd_itm_cd"].ToString();
                                    try
                                    {
                                        obj.Sccd_ser = drow["SAPD_CIRCULAR_NO"].ToString();
                                    }
                                    catch (Exception)
                                    {
                                        obj.Sccd_ser = "";
                                    }

                                    //addList.Add(drow["sapd_promo_cd"].ToString());
                                    addList.Add(obj);
                                    ItemBrandCat_List.AddRange(addList);
                                }
                            }
                        }
                    }
                       
                    if (ItemBrandCat_List.Count <= 0)
                    {
                        MessageBox.Show("Records not found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;

                    }
                    if (!string.IsNullOrEmpty(_errorLst.ToString()))
                    {
                        if (MessageBox.Show("Following discrepancies found when checking the file.\n" + _errorLst.ToString() + ".\n Do you need to continue anyway?", "Discrepancies", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            //ItemBrandCat_List.Clear();
                        }
                        else
                        {
                            grvSalesTypes.AutoGenerateColumns = false;
                            //ItemBrandCat_List.AddRange(addList);
                            BindingSource source = new BindingSource();
                            source.DataSource = ItemBrandCat_List;
                            grvSalesTypes.AutoGenerateColumns = false;
                            grvSalesTypes.DataSource = source;
                            grvSalesTypes.Columns[1].HeaderText = cmbSelectCat.Text;
                        }
                    }
                    else
                    {
                        grvSalesTypes.AutoGenerateColumns = false;
                        //ItemBrandCat_List.AddRange(addList);
                        BindingSource source = new BindingSource();
                        source.DataSource = ItemBrandCat_List;
                        grvSalesTypes.AutoGenerateColumns = false;
                        grvSalesTypes.DataSource = source;
                        grvSalesTypes.Columns[1].HeaderText = cmbSelectCat.Text;
                        
                    }

                }
            }
            catch (Exception)
            {
                
                throw;
            }
          
        }

        private void btn_serch_deteils_mid_Click(object sender, EventArgs e)
        {
            List<string> _pcList = new List<string>();
            List<Deposit_Bank_Pc_wise> _Deposit_Bank_Pc_wise = new List<Deposit_Bank_Pc_wise>();
            foreach (ListViewItem itm in lstPC.Items)
            {
                if (itm.Checked)
                {
                    _pcList.Add(itm.Text);
                }
            }
            if (_pcList.Count != 0)
            {
                foreach (var item in _pcList)
                {
                    List<Deposit_Bank_Pc_wise> _list = new List<Deposit_Bank_Pc_wise>();
                    _list = CHNLSVC.Financial.get_Deposit_Bank_Pc_wise_det(BaseCls.GlbUserComCode, item, cmbMainMidNo.Text.ToString(), dtpFromDate.Value.Date, dtpTodate.Value.Date);
                    _Deposit_Bank_Pc_wise.AddRange(_list);
                }
            }
            else
            {
                List<Deposit_Bank_Pc_wise> _list = new List<Deposit_Bank_Pc_wise>();
                _list = CHNLSVC.Financial.get_Deposit_Bank_Pc_wise_det(BaseCls.GlbUserComCode, null, cmbMainMidNo.Text.ToString(), dtpFromDate.Value.Date, dtpTodate.Value.Date);
                _Deposit_Bank_Pc_wise.AddRange(_list);
            }
          
            dgvMerchantDetails.AutoGenerateColumns = false;
            dgvMerchantDetails.DataSource = _Deposit_Bank_Pc_wise;
        }

        private void bupdate_Click(object sender, EventArgs e)
        {
            List<Deposit_Bank_Pc_wise> _lstMasterPcwiseMidupdate = new List<Deposit_Bank_Pc_wise>();
            foreach (DataGridViewRow dgvr in dgvMerchantDetails.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells["Select"] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    Deposit_Bank_Pc_wise obj = new Deposit_Bank_Pc_wise();
                    obj.Company = BaseCls.GlbUserComCode;
                    obj.Profit_center = dgvr.Cells["Prof_center"].Value.ToString();
                    obj.From_date = Convert.ToDateTime(dgvr.Cells["clmFromDate"].Value.ToString());
                    obj.To_date = Convert.ToDateTime(dgvr.Cells["clmTodate"].Value.ToString());
                    if (dgvr.Cells["clmOption"].Value.ToString() == "Online")
                    { obj.Pun_tp = 1; }
                    else
                    { obj.Pun_tp = 0; }

                    obj.Period = Convert.ToInt16(dgvr.Cells["clmperiod"].Value.ToString());
                    obj.Mid_no = dgvr.Cells["clmMid"].Value.ToString();
                    obj.Mid_no = dgvr.Cells["clmMid"].Value.ToString();
                    obj.Create_by = BaseCls.GlbUserID;
                   
                    obj._from_date_update = dtpFromDate.Value.Date;
                    obj._to_date_update = dtpTodate.Value.Date;
                    _lstMasterPcwiseMidupdate.Add(obj);
                }

            }

            if (_lstMasterPcwiseMidupdate.Count == 0)
            {
                MessageBox.Show("Records not selected", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Are you sure want to Update ?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            string _error = "";
            int result = CHNLSVC.Sales.updateToMasterPcWiseMid(_lstMasterPcwiseMidupdate, out _error);
            if (result == -1)
            {
                MessageBox.Show("Error occurred while processing\n" + _error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                MessageBox.Show("Records Update Successfully", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnClear_Click(null, null);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Deposit_Bank_Pc_wise> _lstMasterPcwiseMidupdate = new List<Deposit_Bank_Pc_wise>();
            foreach (DataGridViewRow dgvr in dgvMerchantDetails.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells["Select"] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    Deposit_Bank_Pc_wise obj = new Deposit_Bank_Pc_wise();
                    obj.Company = BaseCls.GlbUserComCode;
                    obj.Profit_center = dgvr.Cells["Prof_center"].Value.ToString();
                    obj.From_date = Convert.ToDateTime(dgvr.Cells["clmFromDate"].Value.ToString());
                    obj.To_date = Convert.ToDateTime(dgvr.Cells["clmTodate"].Value.ToString());
                    if (dgvr.Cells["clmOption"].Value.ToString() == "Online")
                    { obj.Pun_tp = 1; }
                    else
                    { obj.Pun_tp = 0; }

                    obj.Period = Convert.ToInt16(dgvr.Cells["clmperiod"].Value.ToString());
                    obj.Mid_no = dgvr.Cells["clmMid"].Value.ToString();
                    obj.Mid_no = dgvr.Cells["clmMid"].Value.ToString();
                    obj.Create_by = BaseCls.GlbUserID;

                    obj._from_date_update = dtpFromDate.Value.Date;
                    obj._to_date_update = dtpTodate.Value.Date;
                    _lstMasterPcwiseMidupdate.Add(obj);
                }

            }

            if (_lstMasterPcwiseMidupdate.Count == 0)
            {
                MessageBox.Show("Records not selected", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Are you sure want to Inactive ?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            string _error = "";
            //int result = CHNLSVC.Sales.cancel_MasterPcWiseMid(_lstMasterPcwiseMidupdate, out _error);
            int result = 0;
            if (result == -1)
            {
                MessageBox.Show("Error occurred while processing\n" + _error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                MessageBox.Show("Records Update Successfully", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnClear_Click(null, null);
            }
        }

        protected void BindPriceType()
        {
            cmbPriceType.Items.Clear();
            List<PriceTypeRef> _list = CHNLSVC.Sales.GetAllPriceType(string.Empty);
            _list.Add(new PriceTypeRef { Sarpt_cd = "", Sarpt_indi = -1 });
            cmbPriceType.DataSource = _list;
            cmbPriceType.DisplayMember = "Sarpt_cd";
            cmbPriceType.ValueMember = "Sarpt_indi";
        }

        private void txtbankchg_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbankchg.Text.Trim())) return;

            if (!txtbankchg.Text.All(c => Char.IsNumber(c)))
            {
                MessageBox.Show("Please enter only positive numbers", "MID", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtbankchg.Text = "";
                txtbankchg.Focus();
                return;
            }
        }

        private void txtgp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtgp.Text.Trim())) return;

            if (!txtbankchg.Text.All(c => Char.IsNumber(c)))
            {
                MessageBox.Show("Please enter only positive numbers", "MID", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtgp.Text = "";
                txtgp.Focus();
                return;
            }
        }

        private void dgvMerchantDetails_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {

        }

        private void dgvMerchantDetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            int row = dgvMerchantDetails.CurrentCell.RowIndex;
            dgvMerchantDetails.Rows.RemoveAt(row);
  
        }
    }
}
