using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.Enquiries.Inventory
{
    public partial class InventoryTracker : Base
    {
        private string hdnUserLevel = "0";
        private string selectLocation = "";
        private List<int> RoleId = new List<int>();
        private string errMsg = string.Empty;
        private Boolean _isStrucBaseTax = false;

        public InventoryTracker()
        {
            try
            {
                InitializeComponent();
                //---------------------------------------------
                //GlbDefChannel
                //txtChannel.Text = BaseCls.GlbDefChannel;
                //if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10039))
                //{
                //    panel_chanel.Visible = true;
                //}
                //else
                //{
                //    panel_chanel.Visible = false;
                //}
                //---------------------------------------------
                panel_serialView.Visible = false;
                panel_main_tab2.Visible = true;// Active 15/Sep/2013 Chamal
                gvStatus.AutoGenerateColumns = false;
                gvStatus.DataSource = new DataTable();
                pnlPackingStock.Visible = false;

                //kapila 9/3/2017
                MasterCompany _masterComp = null;
                _masterComp = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);

                if (_masterComp.MC_TAX_CALC_MTD == "1") _isStrucBaseTax = true;

                lblPackItem.Text = "";

                string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
                if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "INVAC"))
                //if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10084))
                {
                    //TODO:
                    //PanelCompanyDetails.Enabled = true;

                    ImageButtonCompany.Enabled = true;
                    //CollapsiblePanelExtender1.Enabled = true;
                    // ucLoactionSearch1.Enabled = true;
                    ucLoactionSearch1.Visible = true;
                    panel_advanceSearch.Visible = true;

                    checkAdvnSearch.Visible = true;
                    checkAllLoc.Enabled = true;
                    //hdnUserLevel.Value = "1";
                    hdnUserLevel = "1";

                    TextBoxCompany.Enabled = true;
                    //TextBoxCompany.Enabled = false;
                }
                else
                {
                    CheckBoxShowCostVal.Visible = false;
                    GridViewItemDetails.Columns["Description"].Width = GridViewItemDetails.Columns["Description"].Width + 100;
                    //Description

                    CheckBoxShowCostVal.Checked = false;
                    CheckBoxShowCostVal_CheckedChanged(null, null); //TODO
                    //PanelCompanyDetails.Enabled = false;
                    TextBoxCompany.Enabled = false;
                    ImageButtonCompany.Enabled = false;
                    checkAllLoc.Enabled = false;
                    //hdnUserLevel.Value = "0";
                    hdnUserLevel = "0";
                    //CollapsiblePanelExtender1.Enabled = false;
                    //ucLoactionSearch1.Enabled = false;
                    ucLoactionSearch1.Visible = false;
                    panel_advanceSearch.Visible = false;
                    checkAdvnSearch.Visible = false;
                    //btnAdvnSearch.Enabled = false;
                    TextBoxLoc.Enabled = false;
                    //Image2.Visible = false;
                    //--------------------------------------------------------**
                    TextBoxCompany.Enabled = false;
                    TextBoxLoc.Enabled = false;
                }
                if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "INVWC"))
                {
                    CheckBoxShowCostVal.Visible = true;
                    CheckBoxShowCostVal.Checked = true;
                    CheckBoxShowCostVal_CheckedChanged(null, null);
                }
                else
                {
                    CheckBoxShowCostVal.Visible = false;
                    CheckBoxShowCostVal.Checked = false;
                    CheckBoxShowCostVal_CheckedChanged(null, null);
                }
                if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "INVTA"))
                {
                    txtTaxCom.Enabled = true;
                    btnTaxCom.Enabled = true;
                }
                else
                {
                    txtTaxCom.Enabled = false;
                    btnTaxCom.Enabled = false;
                }

                if (checkAllLoc.Enabled == false)
                {
                    if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10038))
                    {
                        checkAllLoc.Enabled = true;
                        TextBoxLoc.Enabled = true;
                        TextBoxLoc.Text = BaseCls.GlbUserDefLoca;
                        txtChannel.Text = "";
                        txtChnlLoc.Text = "";

                        txtChannel.Enabled = false;
                        txtChnlLoc.Enabled = false;

                        btnChnlSearch.Enabled = false;
                        btnSearchChnlLoc.Enabled = false;
                    }
                }
                //--------------------------------------------------------------------------------------
                TextBoxCompany.Text = BaseCls.GlbUserComCode;
                TextBoxLoc.Text = BaseCls.GlbUserDefLoca;
                ucLoactionSearch1.Company = BaseCls.GlbUserComCode;
                //   ucLoactionSearch1.CompanyDes = BaseCls.GlbUserComDesc;

                Dictionary<string, string> status_list = CHNLSVC.Inventory.Get_all_ItemSatus();
                foreach (string Key in status_list.Keys)
                {
                    ComboboxItem stat = new ComboboxItem();
                    stat.Text = status_list[Key];
                    stat.Value = Key;
                    DDLStatus.Items.Add(stat);
                }
                ComboboxItem stat_All = new ComboboxItem();
                stat_All.Text = "Any";
                stat_All.Value = "%";
                DDLStatus.Items.Add(stat_All);
                DDLStatus.SelectedItem = stat_All;

                List<MasterLocation> loc_list = CHNLSVC.Inventory.getAllLoc_WithSubLoc(BaseCls.GlbUserComCode, TextBoxLoc.Text.ToUpper());
                if (loc_list == null)
                {
                    MasterLocation loc_ = CHNLSVC.General.GetLocationByLocCode(BaseCls.GlbUserComCode, TextBoxLoc.Text.ToUpper());
                    loc_list = new List<MasterLocation>();
                    loc_list.Add(loc_);
                }
                else if (loc_list.Count < 1)
                {
                    MasterLocation loc_ = CHNLSVC.General.GetLocationByLocCode(BaseCls.GlbUserComCode, TextBoxLoc.Text.ToUpper());
                    loc_list.Add(loc_);
                }

                GridAllLocations.DataSource = null;
                GridAllLocations.AutoGenerateColumns = false;
                GridAllLocations.DataSource = loc_list;

                DataTable _role = CHNLSVC.Security.GetCompanyUserRole(BaseCls.GlbUserID, BaseCls.GlbUserComCode);

                if (_role == null || _role.Rows.Count <= 0)
                {
                    MessageBox.Show("User role not found, Can not Proceed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                foreach (DataRow dr in _role.Rows)
                {
                    RoleId.Add(Convert.ToInt32(dr["serm_role_id"]));
                }

                DataTable _userChannel = CHNLSVC.Inventory.GetAllChannelForInventoryTracker(BaseCls.GlbUserComCode, RoleId);
                if (_userChannel.Rows.Count > 0)
                {
                    panel_chanel.Visible = true;
                }
                else
                {
                    panel_chanel.Visible = false;
                }

                foreach (DataGridViewRow row in GridAllLocations.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
                GridAllLocations.EndEdit();
                txtTaxCom.Text = BaseCls.GlbUserComCode;

                this.btnAllLoc_Click(null, null);
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

        public class ComboboxItem
        {
            public string Text { get; set; }

            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        protected void CheckBoxShowCostVal_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxShowCostVal.Checked)
            {
                GridViewItemDetails.Columns["Cost_Value"].Visible = true;
                ucItemSerialView1.ShowCost = true;
                // DivTotal.Visible = true;
            }
            else
            {
                //GridViewItemDetails.Columns[7].Visible = false;
                GridViewItemDetails.Columns["Cost_Value"].Visible = false;
                ucItemSerialView1.ShowCost = false;
                // DivTotal.Visible = false;
            }
        }

        private void InventoryTracker_Load(object sender, EventArgs e)
        {
            chkAsatdate.Checked = true; // add by tharanga 2017/08/15
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Model:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Brand:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Brand.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(TextBoxCompany.Text.ToUpper() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.WHAREHOUSE:
                    {
                        paramsText.Append(TextBoxCompany.Text.ToUpper() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        paramsText.Append(TextBoxCompany.Text.ToUpper() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {
                        paramsText.Append(TextBoxMain.Text.ToUpper() + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        paramsText.Append(TextBoxMain.Text.ToUpper() + seperator + TextBoxSub.Text.ToUpper() + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub3:
                    {
                        paramsText.Append(TextBoxMain.Text.ToUpper() + seperator + TextBoxSub.Text.ToUpper() + seperator + TextBoxRange.Text.ToUpper() + seperator + "" + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub4:
                    {
                        paramsText.Append(TextBoxMain.Text.ToUpper() + seperator + TextBoxSub.Text.ToUpper() + seperator + TextBoxRange.Text.ToUpper() + seperator + TextBoxCat4.Text.ToUpper() + seperator + "" + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel:
                    {
                        paramsText.Append(TextBoxCompany.Text.Trim().ToUpper() + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location:
                    {
                        paramsText.Append(TextBoxCompany.Text.ToUpper() + seperator + txtChannel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Inventory_Tracker: // updated by akila 2017/12/05 - add color and size for search criteria
                    {
                        string status = null;
                        if (DDLStatus.SelectedItem == null)
                            status = "";
                        else
                        {
                            //---------------------
                            ComboboxItem combo_reqtp = (ComboboxItem)DDLStatus.SelectedItem;
                            status = combo_reqtp.Value.ToString();
                            //status = DDLStatus.SelectedItem.Value;
                            //---------------------
                        }
                        if (BaseCls.GlbUserComCode == "AST")
                        {
                            string _item = "";
                            //kapila 18/11/2013
                            if (TextBoxCode.Text.Length == 16)
                                _item = TextBoxCode.Text.Substring(1, 7);
                            else if (TextBoxCode.Text.Length == 15)
                                _item = TextBoxCode.Text.Substring(0, 7);
                            else if (TextBoxCode.Text.Length == 8)
                                _item = TextBoxCode.Text.Substring(1, 7);
                            else if (TextBoxCode.Text.Length == 20)
                                _item = TextBoxCode.Text.Substring(0, 12);
                            else
                                _item = TextBoxCode.Text;

                            TextBoxCode.Text = _item;
                        }
                        if (chkChannel.Checked)
                        {
                            if (txtChnlLoc.Text == "")
                            {
                                paramsText.Append(TextBoxCode.Text.ToUpper() + seperator + TextBoxModel.Text.ToUpper() + seperator + status + seperator + ucLoactionSearch1.Company.ToUpper() + seperator + TextBoxMain.Text.ToUpper() + seperator + TextBoxSub.Text.ToUpper() + seperator + txtChannel.Text.ToUpper() + seperator + ucLoactionSearch1.SubChannel.ToUpper() + seperator + ucLoactionSearch1.Area.ToUpper() + seperator + ucLoactionSearch1.Regien.ToUpper() + seperator + ucLoactionSearch1.Zone.ToUpper() + seperator + "No_Loc" + seperator + "" + seperator + TextBoxRange.Text.ToUpper() + seperator + TextBoxBrand.Text.ToUpper() + seperator + CheckBoxShowCostVal.Checked +
                                    seperator + TextBoxCat4.Text + seperator + TextBoxCat5.Text + seperator + txtSearchColor.Text.Trim() + seperator + txtSearchSize.Text.Trim() + seperator);
                                break;
                            }
                            else
                            {
                                paramsText.Append(TextBoxCode.Text.ToUpper() + seperator + TextBoxModel.Text.ToUpper() + seperator + status + seperator + ucLoactionSearch1.Company.ToUpper() + seperator + TextBoxMain.Text.ToUpper() + seperator + TextBoxSub.Text.ToUpper() + seperator + ucLoactionSearch1.Channel.ToUpper() + seperator + ucLoactionSearch1.SubChannel.ToUpper() + seperator + ucLoactionSearch1.Area.ToUpper() + seperator + ucLoactionSearch1.Regien.ToUpper() + seperator + ucLoactionSearch1.Zone.ToUpper() + seperator + "Loc" + seperator + txtChnlLoc.Text.ToUpper() + seperator + TextBoxRange.Text.ToUpper() + seperator + TextBoxBrand.Text.ToUpper() + seperator + CheckBoxShowCostVal.Checked +
                                    seperator + TextBoxCat4.Text + seperator + TextBoxCat5.Text + seperator + txtSearchColor.Text.Trim() + seperator + txtSearchSize.Text.Trim() + seperator);
                                break;
                            }
                        }
                        if (selectLocation == "")
                        {
                            if (TextBoxLoc.Text == "")
                            {
                                paramsText.Append(TextBoxCode.Text.ToUpper() + seperator + TextBoxModel.Text.ToUpper() + seperator + status + seperator + ucLoactionSearch1.Company.ToUpper() + seperator + TextBoxMain.Text.ToUpper() + seperator + TextBoxSub.Text.ToUpper() + seperator + ucLoactionSearch1.Channel.ToUpper() + seperator + ucLoactionSearch1.SubChannel.ToUpper() + seperator + ucLoactionSearch1.Area.ToUpper() + seperator + ucLoactionSearch1.Regien.ToUpper() + seperator + ucLoactionSearch1.Zone.ToUpper() + seperator + "No_Loc" + seperator + ucLoactionSearch1.ProfitCenter.ToUpper() + seperator + TextBoxRange.Text.ToUpper() + seperator + TextBoxBrand.Text.ToUpper() + seperator + CheckBoxShowCostVal.Checked +
                                    seperator + TextBoxCat4.Text + seperator + TextBoxCat5.Text + seperator + txtSearchColor.Text.Trim() + seperator + txtSearchSize.Text.Trim() + seperator);
                                break;
                            }
                            else
                            {
                                paramsText.Append(TextBoxCode.Text.ToUpper() + seperator + TextBoxModel.Text.ToUpper() + seperator + status + seperator + ucLoactionSearch1.Company.ToUpper() + seperator + TextBoxMain.Text.ToUpper() + seperator + TextBoxSub.Text.ToUpper() + seperator + ucLoactionSearch1.Channel.ToUpper() + seperator + ucLoactionSearch1.SubChannel.ToUpper() + seperator + ucLoactionSearch1.Area.ToUpper() + seperator + ucLoactionSearch1.Regien.ToUpper() + seperator + ucLoactionSearch1.Zone.ToUpper() + seperator + "Loc" + seperator + TextBoxLoc.Text.ToUpper() + seperator + TextBoxRange.Text.ToUpper() + seperator + TextBoxBrand.Text.ToUpper() + seperator + CheckBoxShowCostVal.Checked +
                                    seperator + TextBoxCat4.Text + seperator + TextBoxCat5.Text + seperator + txtSearchColor.Text.Trim() + seperator + txtSearchSize.Text.Trim() + seperator);
                                break;
                            }
                        }
                        if (selectLocation == "%")
                        {
                            //All locations in the Company

                            //paramsText.Append(TextBoxCode.Text.ToUpper() + seperator + TextBoxModel.Text.ToUpper() + seperator + status + seperator + ucLoactionSearch1.Company.ToUpper() + seperator + TextBoxMain.Text.ToUpper() + seperator + TextBoxSub.Text.ToUpper() + seperator + ucLoactionSearch1.Channel.ToUpper() + seperator + ucLoactionSearch1.SubChannel.ToUpper() + seperator + ucLoactionSearch1.Area.ToUpper() + seperator + ucLoactionSearch1.Regien.ToUpper() + seperator + ucLoactionSearch1.Zone.ToUpper() + seperator + "Loc" + seperator + ucLoactionSearch1.ProfitCenter.ToUpper() + seperator + TextBoxRange.Text.ToUpper() + seperator);
                            paramsText.Append(TextBoxCode.Text.ToUpper() + seperator + TextBoxModel.Text.ToUpper() + seperator + status + seperator + TextBoxCompany.Text.Trim().ToUpper() + seperator + TextBoxMain.Text.ToUpper() + seperator + TextBoxSub.Text.ToUpper() + seperator + ucLoactionSearch1.Channel.ToUpper() + seperator + ucLoactionSearch1.SubChannel.ToUpper() + seperator + ucLoactionSearch1.Area.ToUpper() + seperator + ucLoactionSearch1.Regien.ToUpper() + seperator + ucLoactionSearch1.Zone.ToUpper() + seperator + "No_Loc" + seperator + selectLocation.ToUpper() + seperator + TextBoxRange.Text.ToUpper() + seperator + TextBoxBrand.Text.ToUpper() + seperator + CheckBoxShowCostVal.Checked +
                                seperator + TextBoxCat4.Text + seperator + TextBoxCat5.Text + seperator + txtSearchColor.Text.Trim() + seperator + txtSearchSize.Text.Trim() + seperator);
                            break;
                        }
                        //------------------------------------------------------------------------------------------------
                        if (!TextBoxCompany.Enabled && ucLoactionSearch1.ProfitCenter != string.Empty)
                        {
                            //selectLocation
                            //paramsText.Append(TextBoxCode.Text.ToUpper() + seperator + TextBoxModel.Text.ToUpper() + seperator + status + seperator + ucLoactionSearch1.Company.ToUpper() + seperator + TextBoxMain.Text.ToUpper() + seperator + TextBoxSub.Text.ToUpper() + seperator + ucLoactionSearch1.Channel.ToUpper() + seperator + ucLoactionSearch1.SubChannel.ToUpper() + seperator + ucLoactionSearch1.Area.ToUpper() + seperator + ucLoactionSearch1.Regien.ToUpper() + seperator + ucLoactionSearch1.Zone.ToUpper() + seperator + "Loc" + seperator + ucLoactionSearch1.ProfitCenter.ToUpper() + seperator + TextBoxRange.Text.ToUpper() + seperator);
                            paramsText.Append(TextBoxCode.Text.ToUpper() + seperator + TextBoxModel.Text.ToUpper() + seperator + status + seperator + ucLoactionSearch1.Company.ToUpper() + seperator + TextBoxMain.Text.ToUpper() + seperator + TextBoxSub.Text.ToUpper() + seperator + ucLoactionSearch1.Channel.ToUpper() + seperator + ucLoactionSearch1.SubChannel.ToUpper() + seperator + ucLoactionSearch1.Area.ToUpper() + seperator + ucLoactionSearch1.Regien.ToUpper() + seperator + ucLoactionSearch1.Zone.ToUpper() + seperator + "Loc" + seperator + selectLocation.ToUpper() + seperator + TextBoxRange.Text.ToUpper() + seperator + TextBoxBrand.Text.ToUpper() + seperator + CheckBoxShowCostVal.Checked +
                                seperator + TextBoxCat4.Text + seperator + TextBoxCat5.Text + seperator + txtSearchColor.Text.Trim() + seperator + txtSearchSize.Text.Trim() + seperator);
                        }
                        //else if (!TextBoxLoc.Enabled)
                        //{
                        //    paramsText.Append(TextBoxCode.Text.ToUpper() + seperator + TextBoxModel.Text.ToUpper() + seperator + status + seperator + ucLoactionSearch1.Company.ToUpper() + seperator + TextBoxMain.Text.ToUpper() + seperator + TextBoxSub.Text.ToUpper() + seperator + ucLoactionSearch1.Channel.ToUpper() + seperator + ucLoactionSearch1.SubChannel.ToUpper() + seperator + ucLoactionSearch1.Area.ToUpper() + seperator + ucLoactionSearch1.Regien.ToUpper() + seperator + ucLoactionSearch1.Zone.ToUpper() + seperator + "No_Loc" + seperator + "" + seperator + TextBoxRange.Text.ToUpper() + seperator);

                        //}
                        else
                        {
                            // paramsText.Append(TextBoxCode.Text + seperator + TextBoxModel.Text.ToUpper() + seperator + status + seperator + TextBoxCompany.Text.ToUpper() + seperator + TextBoxMain.Text.ToUpper() + seperator + TextBoxSub.Text.ToUpper() + seperator + ucLoactionSearch1.Channel.ToUpper() + seperator + ucLoactionSearch1.SubChannel.ToUpper() + seperator + ucLoactionSearch1.Area.ToUpper() + seperator + ucLoactionSearch1.Regien.ToUpper() + seperator + ucLoactionSearch1.Zone.ToUpper() + seperator + "Loc" + seperator + TextBoxLoc.Text.ToUpper() + seperator + TextBoxRange.Text.ToUpper() + seperator);
                            paramsText.Append(TextBoxCode.Text.ToUpper() + seperator + TextBoxModel.Text.ToUpper() + seperator + status + seperator + TextBoxCompany.Text.ToUpper() + seperator + TextBoxMain.Text.ToUpper() + seperator + TextBoxSub.Text.ToUpper() + seperator + ucLoactionSearch1.Channel.ToUpper() + seperator + ucLoactionSearch1.SubChannel.ToUpper() + seperator + ucLoactionSearch1.Area.ToUpper() + seperator + ucLoactionSearch1.Regien.ToUpper() + seperator + ucLoactionSearch1.Zone.ToUpper() + seperator + "Loc" + seperator + selectLocation.ToUpper() + seperator + TextBoxRange.Text.ToUpper() + seperator + TextBoxBrand.Text.ToUpper() + seperator + CheckBoxShowCostVal.Checked +
                                seperator + TextBoxCat4.Text + seperator + TextBoxCat5.Text + seperator + txtSearchColor.Text.Trim() + seperator + txtSearchSize.Text.Trim() + seperator);
                        }
                        break;
                    }
                //load empty grid
                case CommonUIDefiniton.SearchUserControlType.Item_Serials:
                    {
                        paramsText.Append("-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "Loc" + seperator + "-999" + seperator + "-999" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvTrcChnl:
                    {
                        string roleid = "";
                        foreach (int role in RoleId)
                        {
                            roleid = roleid + "," + role;
                        }

                        paramsText.Append(BaseCls.GlbUserComCode + seperator + roleid + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + txtArea.Text + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + txtArea.Text + seperator + txtRegion.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Add_Inv_Tracker: // added by darshana to get packing item details
                    {
                        
                        //paramsText.Append(TextBoxCode.Text.ToUpper() + seperator + TextBoxModel.Text.ToUpper() + seperator + status + seperator + ucLoactionSearch1.Company.ToUpper() + seperator + TextBoxMain.Text.ToUpper() + seperator + TextBoxSub.Text.ToUpper() + seperator + ucLoactionSearch1.Channel.ToUpper() + seperator + ucLoactionSearch1.SubChannel.ToUpper() + seperator + ucLoactionSearch1.Area.ToUpper() + seperator + ucLoactionSearch1.Regien.ToUpper() + seperator + ucLoactionSearch1.Zone.ToUpper() + seperator + "No_Loc" + seperator + ucLoactionSearch1.ProfitCenter.ToUpper() + seperator + TextBoxRange.Text.ToUpper() + seperator + TextBoxBrand.Text.ToUpper() + seperator);
                        paramsText.Append(lblPackItem.Text.ToUpper() + seperator + null + seperator + null + seperator + BaseCls.GlbUserComCode + seperator + null + seperator + null + seperator + null + seperator + null + seperator + null + seperator + null + seperator + null + seperator + "No_Loc" + seperator + null + seperator + null + seperator + null + seperator);
                        break;

                    }
                case CommonUIDefiniton.SearchUserControlType.masterColor:
                    {
                        paramsText.Append("");
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        private void imgItemSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchType = "ITEMS";
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = TextBoxCode;
                _CommonSearch.ShowDialog();
                TextBoxCode.Focus();
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

        private void ImageButtonMain_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = TextBoxMain;
                _CommonSearch.ShowDialog();
                TextBoxMain.Focus();
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

        private void ImageButtonSub_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = TextBoxSub;
                _CommonSearch.ShowDialog();
                TextBoxSub.Focus();
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

        private void ImageButtonRange_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = TextBoxRange;
                _CommonSearch.ShowDialog();
                TextBoxRange.Focus();
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

        private void ImageButtonCompany_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanySearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = TextBoxCompany;
                _CommonSearch.ShowDialog();
                TextBoxCompany.Focus();
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

        private void ImageButtonLocation_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = TextBoxLoc;
                _CommonSearch.ShowDialog();
                TextBoxLoc.Focus();
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

        private void ImageButtonStatus_Click(object sender, EventArgs e)
        {
        }

        private void ImageButtonModle_Click(object sender, EventArgs e)
        {
        }

        private int GetFreeQty(DataTable dataSource)
        {
            int total = 0;
            try
            {
                for (int i = 0; i < dataSource.Rows.Count; i++)
                {
                    total = total + Convert.ToInt32(dataSource.Rows[i]["FREE_QTY"]);
                }
            }
            catch (Exception EX)
            {
            }

            return total;
        }

        private int GetResQty(DataTable dataSource)
        {
            int total = 0;
            try
            {
                for (int i = 0; i < dataSource.Rows.Count; i++)
                {
                    total = total + Convert.ToInt32(dataSource.Rows[i]["RES_QTY"]);
                }
            }
            catch (Exception EX)
            {
            }

            return total;
        }
        private int GetQty(DataTable dataSource)
        {
            int total = 0;
            try
            {
                for (int i = 0; i < dataSource.Rows.Count; i++)
                {
                    total = total + Convert.ToInt32(dataSource.Rows[i]["QTY"]);
                }
            }
            catch (Exception EX)
            {
            }

            return total;
        }

        private void GridViewDataBind(DataTable dataSource)
        {
            GridViewItemDetails.DataSource = null;
            GridViewItemDetails.AutoGenerateColumns = false;

            CheckBoxShowCostVal_CheckedChanged(null, null);
            GridViewItemDetails.DataSource = dataSource;
            //GridViewItemDetails.DataBind();
            TextBoxTqty.Text = GetFreeQty(dataSource).ToString();
            txtResQty.Text = GetResQty(dataSource).ToString();
            txtInHand.Text = GetQty(dataSource).ToString();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage8"])
            {
                ViewItemCondtion();
                return;
            }
            try
            {
                if (GridAllLocations.Rows.Count <= 0 && TextBoxLoc.Text == "" && txtChannel.Text == "" && panel_advanceSearch.Visible == false && checkAllLoc.Checked == false)
                {
                    MessageBox.Show("Please enter search criteria for search", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                panel_serialView.Visible = false;

                GridAllLocations.EndEdit();
                lblNote.Text = "";
                TextBoxTqty.Text = "";
                txtResQty.Text = "";
                txtInHand.Text = "";
                Boolean allow_SCM = true; //CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "SCMI");
                Boolean allow_SCM2 = true; //CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "SCM2I");

                if (TextBoxLoc.Text.Trim() == BaseCls.GlbUserDefLoca)
                {
                    // allow_SCM = true;
                    allow_SCM2 = true;
                }
                if (allow_SCM == true && allow_SCM2 == true)
                {
                    //lblNote.Text = "Enquired from both SCM and SCM2 tables.";
                    lblNote.Text = "Enquired from Allowed locations and/or Channels.";
                }
                else if (allow_SCM == true && allow_SCM2 == false)
                {
                    // lblNote.Text = "Enquired from SCM tables only."; //
                    lblNote.Text = "Enquired from allowed locations and warehouses only";
                }
                else if (allow_SCM == false && allow_SCM2 == true)
                {
                    //lblNote.Text = "Enquired from SCM2 tables only";//Enquired from allowed locations only
                    lblNote.Text = "Enquired from allowed locations only.";
                }
                else
                {
                    //lblNote.Text = "Permission not granted for either SCM or SCM2.";
                    lblNote.Text = "Permission not granted.";
                }
                GridViewItemDetails.DataSource = null;
                GridViewItemDetails.AutoGenerateColumns = false;
                GridViewItemDetails.DataSource = new DataTable();
                GridViewItemDetails.Refresh();

                DataTable tbl_all = new DataTable();
                DataTable tbl = new DataTable();
                GridViewDataBind(tbl_all);
                selectLocation = (selectLocation != "%") ? TextBoxLoc.Text : "%";

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
                //if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "INV2"))
                if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "INVAC"))
                {
                    allow_SCM = true; // add  by akila 2017/07/27
                    if (checkAdvnSearch.Checked == true)
                    {
                        if (MessageBox.Show("Do you want to do Advance Searching?", "Advance Search", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            selectLocation = "";
                            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Inventory_Tracker);
                            tbl = CHNLSVC.CommonSearch.GetInventoryTrackerSearchData_new2(_CommonSearch.SearchParams, allow_SCM, allow_SCM2, out errMsg);
                            if (tbl != null)
                            {
                                if (tbl.Rows.Count > 0)
                                {
                                    tbl_all.Merge(tbl);
                                }
                            }
                            GridViewDataBind(tbl_all);
                            if (txtInHand.Text == "0")
                            {
                                // MessageBox.Show("No stock available.");
                                MessageBox.Show("No stock available.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            return;
                        }
                        else
                        {
                            checkAdvnSearch.Checked = false;
                            return;
                        }
                    }
                }
                else { allow_SCM = false; } // add by akila 2017/07/27

                if (selectLocation == "%")
                {
                    if (TextBoxCode.Text.Trim() == "")
                    {
                        if (MessageBox.Show("Do you want to search All Items in All locations?\n(It might take a long time.)", "Advance Search", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            return;
                        }
                    }
                    //else
                    //{
                    //selectLocation = "%";
                    try
                    {
                        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Inventory_Tracker);
                        tbl = CHNLSVC.CommonSearch.GetInventoryTrackerSearchData_new2(_CommonSearch.SearchParams, allow_SCM, allow_SCM2,out errMsg);
                        if (tbl != null)
                        {
                            if (tbl.Rows.Count > 0)
                            {
                                tbl_all.Merge(tbl);
                            }
                        }
                        GridViewDataBind(tbl_all);
                        if (txtInHand.Text == "0")
                        {
                            //MessageBox.Show("No stock available.");
                            MessageBox.Show("No stock available.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        ALLOW_WHEARHOUSE_QTY();
                        return;
                    }
                    catch (Exception EX)
                    {
                        MessageBox.Show("Error occuered! \nAdvice: This might cause because of huge data amount.\nPlease enter a single Item code and try.)");
                    }

                    // }
                }
                //ADDED 2013/08/27 CHANNEL SEARCH
                if (chkChannel.Checked && txtChannel.Text != "")
                {
                    if (TextBoxCode.Text.Trim() == "")
                    {
                        if (MessageBox.Show("Do you want to search All Items in Channel?\n(It might take a long time.)", "Advance Search", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            return;
                        }
                    }

                    DataTable _dt = CHNLSVC.Inventory.GetInventoryTrackerChannel(BaseCls.GlbUserComCode, txtChannel.Text, RoleId);

                    if (_dt == null || _dt.Rows.Count <= 0)
                    {
                        MessageBox.Show("Entered Channel is not allow to user or Invalid Channel Code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Inventory_Tracker);
                    tbl = CHNLSVC.CommonSearch.GetInventoryTrackerSearchData_new2(_CommonSearch.SearchParams, allow_SCM, allow_SCM2, out errMsg);
                    if (tbl != null)
                    {
                        if (tbl.Rows.Count > 0)
                        {
                            tbl_all.Merge(tbl);
                        }
                    }
                    GridViewDataBind(tbl_all);
                    if (txtInHand.Text == "0")
                    {
                        //MessageBox.Show("No stock available.");
                        MessageBox.Show("No stock available.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    if (Convert.ToInt32(_dt.Rows[0]["SSRT_WO_QTY"]) == 1)
                    {
                        foreach (DataGridViewRow row in GridViewItemDetails.Rows)
                        {
                            row.Cells["Avail_Stock"].Value = DBNull.Value;
                            row.Cells["Avail_Stock"].Style.ForeColor = Color.Green;

                            lblGrandTot.Visible = false;
                            TextBoxTqty.Visible = false;
                        }
                    }

                    // ALLOW_WHEARHOUSE_QTY();

                    return;
                }

                //else
                //{
                //selectLocation = "%";
                try
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Inventory_Tracker);
                    tbl = CHNLSVC.CommonSearch.GetInventoryTrackerSearchData_new2(_CommonSearch.SearchParams, allow_SCM, allow_SCM2, out errMsg);
                    if (tbl != null)
                    {
                        if (tbl.Rows.Count > 0)
                        {
                            tbl_all.Merge(tbl);
                        }
                    }
                    GridViewDataBind(tbl_all);
                    if (txtInHand.Text == "0")
                    {
                        //MessageBox.Show("No stock available.");
                        MessageBox.Show("No stock available.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    ALLOW_WHEARHOUSE_QTY();
                    return;
                }
                catch (Exception EX)
                {
                    MessageBox.Show("Error occuered! \nAdvice: This might cause because of huge data amount.\nPlease enter a single Item code and try.)");
                }

                //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Inventory_Tracker);
                ////DataTable dataSource = CHNLSVC.CommonSearch.GetInventoryTrackerSearchData(_CommonSearch.SearchParams);
                //DataTable dataSource = CHNLSVC.CommonSearch.GetInventoryTrackerSearchData_new2(_CommonSearch.SearchParams, allow_SCM, allow_SCM2);

                //------------------------------------------------**-----------------------------------------------------------------------

                GridViewItemDetails.Refresh();
                if (GridAllLocations.DataSource == null)
                {
                    this.TextBoxLoc_Leave(sender, e);
                }
                else if (GridAllLocations.Rows.Count == 1)
                {
                    this.TextBoxLoc_Leave(sender, e);
                }
                if (GridAllLocations.Rows.Count > 0)
                {
                    foreach (DataGridViewRow dgvr in GridAllLocations.Rows)
                    {
                        DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                        if (Convert.ToBoolean(chk.Value) == true)
                        {
                            if (dgvr.Cells["Loc_code"].Value == null)
                            {
                                this.TextBoxLoc_Leave(sender, e);
                                if (dgvr.Cells["Loc_code"].Value == null)
                                {
                                    MessageBox.Show("Enter Location");
                                    return;
                                }
                            }
                            string loc_ = dgvr.Cells["Loc_code"].Value.ToString();
                            selectLocation = loc_;

                            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Inventory_Tracker);
                            tbl = CHNLSVC.CommonSearch.GetInventoryTrackerSearchData_new2(_CommonSearch.SearchParams, allow_SCM, allow_SCM2, out errMsg);
                            if (tbl != null)
                            {
                                if (tbl.Rows.Count > 0)
                                {
                                    tbl_all.Merge(tbl);
                                }
                            }
                        }
                    }
                    GridViewDataBind(tbl_all);
                    if (txtInHand.Text == "0")
                    {
                        // MessageBox.Show("No stock available.");
                        MessageBox.Show("No stock available.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                ALLOW_WHEARHOUSE_QTY();
                //#region
                ////---13-03-2013----------------------**----ALLOW WHEARHOUSE QTY-------**-----------------------------------------------------------------
                //Boolean allow_WAREHOUSE_QTY = CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "SCMIWOQ");

                //if (allow_WAREHOUSE_QTY == false)
                //{
                //    #region LOOP THE GRID
                //    DataTable wh_houses = null;
                //    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.WHAREHOUSE);
                //    wh_houses = CHNLSVC.Inventory.GetLocationByType(_CommonSearch.SearchParams, null, null);

                //    foreach (DataGridViewRow row in GridViewItemDetails.Rows)
                //    {
                //        var _duplicate = from _dup in wh_houses.AsEnumerable()
                //                         where _dup.Field<string>("CODE") == row.Cells["Location"].Value.ToString()
                //                         select _dup;

                //        if (_duplicate.Count() > 0)
                //        {
                //            row.Cells["Avail_Stock"].Value = DBNull.Value;
                //            row.Cells["Avail_Stock"].Style.ForeColor = Color.Green;
                //        }
                //    }
                //    #endregion
                //}
                ////-------------------------**-------------------------------**-----------------------------------------------------------------
                //#endregion
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

        private void ALLOW_WHEARHOUSE_QTY()
        {
            try
            {
                Boolean allow_WAREHOUSE_QTY = CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "INVAC");

                if (allow_WAREHOUSE_QTY == false)
                {
                    #region LOOP THE GRID

                    DataTable wh_houses = null;
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.WHAREHOUSE);
                    wh_houses = CHNLSVC.Inventory.GetLocationByType(_CommonSearch.SearchParams, null, null);

                    foreach (DataGridViewRow row in GridViewItemDetails.Rows)
                    {
                        var _duplicate = from _dup in wh_houses.AsEnumerable()
                                         where _dup.Field<string>("CODE") == row.Cells["Location"].Value.ToString()
                                         select _dup;

                        if (_duplicate.Count() > 0)
                        {
                            row.Cells["Avail_Stock"].Value = DBNull.Value;
                            row.Cells["Avail_Stock"].Style.ForeColor = Color.Green;

                            lblGrandTot.Visible = false;
                            TextBoxTqty.Visible = false;
                        }
                    }

                    #endregion LOOP THE GRID
                }
                else
                {
                    lblGrandTot.Visible = true;
                    TextBoxTqty.Visible = true;
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

        private void GridViewItemDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if (e.ColumnIndex == 0 && e.RowIndex != -1)
                if (e.ColumnIndex == 14 && e.RowIndex != -1)
                {
                    GridViewItemDetails.Rows[e.RowIndex].Selected = true;
                    DataGridViewRow gvr = GridViewItemDetails.SelectedRows[0];

                    string companyCode = gvr.Cells["Company"].Value.ToString();
                    string itemCode = gvr.Cells["Item_Code"].Value.ToString();
                    string location = gvr.Cells["Location"].Value.ToString();
                    string itemStatus = gvr.Cells["Status"].Value.ToString();


                    MasterItem _item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, itemCode);// Nadeeka 06-08-2015

                     if (_item.Mi_is_ser1 == -1)
                     {
                         MessageBox.Show(" This item is a decimal allowed item,Serials are not available for such items", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         return;
                     }


                    Dictionary<string, string> status_list = CHNLSVC.Inventory.Get_all_ItemSatus();

                    foreach (KeyValuePair<string, string> pair in status_list)
                    {
                        //Console.WriteLine("{0}, {1}", pair.Key, pair.Value);
                        if (pair.Value == itemStatus)
                        {
                            itemStatus = pair.Key;
                        }
                    }
                    // panel_serialView.Visible = true;
                    //***************************************************
                    string status = string.Empty;
                    if (DDLStatus.SelectedItem == null)
                    { status = ""; }
                    else
                    {
                        //    status = DDLStatus.SelectedItem.Value;
                        ComboboxItem combo_reqtp = (ComboboxItem)DDLStatus.SelectedItem;
                        status = combo_reqtp.Value.ToString();
                    }

                    ucItemSerialView1.ITEM_CODE = itemCode;//GridViewItemDetails.Rows[GridViewItemDetails.SelectedIndex].Cells[2].Text;
                    ucItemSerialView1.ITEM_STATUS = itemStatus;//status;
                    ucItemSerialView1.COMPANY = companyCode; //GridViewItemDetails.Rows[GridViewItemDetails.SelectedIndex].Cells[0].Text;
                    ucItemSerialView1.CHANNEL = "";
                    ucItemSerialView1.SUB_CHANNEL = "";
                    ucItemSerialView1.AREA = "";
                    ucItemSerialView1.REAGION = "";
                    ucItemSerialView1.ZONE = "";
                    ucItemSerialView1.TYPE = "Loc";
                    ucItemSerialView1.LOC = location;//GridViewItemDetails.Rows[GridViewItemDetails.SelectedIndex].Cells[1].Text;

                    ucItemSerialView1.IsVisible = true;

                    panel_serialView.Visible = true;
                    ucItemSerialView1.Display();

                    //***************************************************
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

        private void btnPopUpClose_Click(object sender, EventArgs e)
        {
            panel_serialView.Visible = false;
            //TODO: CLEAR PANEL GRIDS.
        }

        private void Clear()
        {
            txtItemCD_PDv.Clear();
            lblCate1Age.Text = "";
            txtItmType.Clear();
            txtItemDesc_PDv.Text = "";
            txtItemBrand_PDv.Text = "";
            txtItemMod_PDv.Text = "";
            txtExColor_PDv.Text = "";
            txtInColor_PDv.Text = "";
            txtItemCatM_PDv.Text = "";
            //txtItemTaxRt_PDv.Text= mst_item.
            chkHasHpIns_PDv.Checked = false;
            txtUOM.Text = "";
            chkAct.Checked = false;
            txtCat1.Text = "";
            txtCat2.Text = "";
            txtCat3.Text = "";
            lblCat1.Text = "";
            lblCat2.Text = "";
            lblCat3.Text = "";
            txtItemTaxRt_PDv.Text = "";
            txtPackCD.Text = "";
            lblPackItem.Text = "";

            grvWarranty.DataSource = null;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            InventoryTracker formnew = new InventoryTracker();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();
            //DataTable DT = new DataTable();
            //GridAllLocations.AutoGenerateColumns = false;
            //GridAllLocations.DataSource = DT;

            //GridViewDataBind(DT);
            //panel_serialView.Visible = false;

            //TextBoxCode.Text="";
            //TextBoxMain.Text="";
            //TextBoxSub.Text="";
            //TextBoxRange.Text="";
            //TextBoxModel.Text="";
            //TextBoxCompany.Text= BaseCls.GlbUserComCode;
            //TextBoxLoc.Text = BaseCls.GlbUserDefLoca;

            //lblNote.Text = "";
        }

        private void TextBoxLoc_Leave(object sender, EventArgs e)
        {
            try
            {
                if (TextBoxLoc.Text.Trim() == "")
                {
                    GridAllLocations.DataSource = null;
                    GridAllLocations.AutoGenerateColumns = false;
                    GridAllLocations.DataSource = new DataTable();
                    return;
                }
                try
                {
                    List<MasterLocation> loc_list = CHNLSVC.Inventory.getAllLoc_WithSubLoc(BaseCls.GlbUserComCode, TextBoxLoc.Text.ToUpper());

                    if (loc_list == null)
                    {
                        //MasterLocation loc_ = CHNLSVC.General.GetLocationByLocCode(BaseCls.GlbUserComCode, TextBoxLoc.Text.ToUpper());
                        //loc_list = new List<MasterLocation>();
                        //loc_list.Add(loc_);
                        MessageBox.Show("Invalid Location Code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (loc_list.Count < 1)
                    {
                        MasterLocation loc_ = CHNLSVC.General.GetLocationByLocCode(BaseCls.GlbUserComCode, TextBoxLoc.Text.ToUpper());
                        loc_list.Add(loc_);
                    }

                    GridAllLocations.DataSource = null;
                    GridAllLocations.AutoGenerateColumns = false;
                    GridAllLocations.DataSource = loc_list;

                    foreach (DataGridViewRow row in GridAllLocations.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        //GridAllLocations.ReadOnly = true;
                        chk.Value = true;
                    }
                }
                catch (Exception ex)
                {
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

        private void ucLoactionSearch1_Leave(object sender, EventArgs e)
        {
            try
            {
                if (ucLoactionSearch1.ProfitCenter != "")
                {
                    List<MasterLocation> loc_list = CHNLSVC.Inventory.getAllLoc_WithSubLoc(ucLoactionSearch1.Company, ucLoactionSearch1.ProfitCenter);
                    if (loc_list == null)
                    {
                        MasterLocation loc_ = CHNLSVC.General.GetLocationByLocCode(BaseCls.GlbUserComCode, TextBoxLoc.Text.ToUpper());
                        loc_list = new List<MasterLocation>();
                        loc_list.Add(loc_);
                    }
                    else if (loc_list.Count < 1)
                    {
                        MasterLocation loc_ = CHNLSVC.General.GetLocationByLocCode(BaseCls.GlbUserComCode, TextBoxLoc.Text.ToUpper());
                        loc_list.Add(loc_);
                    }
                    GridAllLocations.DataSource = null;
                    GridAllLocations.AutoGenerateColumns = false;
                    GridAllLocations.DataSource = loc_list;
                    foreach (DataGridViewRow row in GridAllLocations.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        //GridAllLocations.ReadOnly = true;
                        chk.Value = true;
                    }
                    TextBoxLoc.Text = ucLoactionSearch1.ProfitCenter;
                    TextBoxCompany.Text = ucLoactionSearch1.Company;
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

        private void TextBoxCode_Leave(object sender, EventArgs e)
        {
            //if (TextBoxCode.Text.Trim()=="")
            //{
            //    return;
            //}
            //try
            //{
            //    MasterItem itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, TextBoxCode.Text.ToUpper());
            //    TextBoxModel.Text = itm.Mi_model;
            //}
            //catch (Exception EX) {  }
        }

        private void ucLoactionSearch1_ItemAdded(object sender, EventArgs e)
        {
            try
            {
                this.ucLoactionSearch1_Leave(sender, e);
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

        private void TextBoxLoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {
                    this.TextBoxLoc_Leave(sender, e);
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
        }

        private void btnModleSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                DataTable _result = CHNLSVC.CommonSearch.GetAllModels(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = TextBoxModel; //txtBox;
                _CommonSearch.ShowDialog();
                TextBoxModel.Focus();
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

        private void btnLocSearch_Click(object sender, EventArgs e)
        {
            try
            {
                TextBox txtBox = new TextBox();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;

                DataTable _result = new DataTable();
                //----------------------------------------------------------------------------
                Boolean allow_WHAREHOUSE = CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "INVAC");
                DataTable _result2 = null;
                if (allow_WHAREHOUSE == true)
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.WHAREHOUSE);
                    //DataTable _result = CHNLSVC.Inventory.GetLocationByType(_CommonSearch.SearchParams, null, null);
                    _result2 = CHNLSVC.Inventory.GetLocationByType(_CommonSearch.SearchParams, null, null);
                    _result.Merge(_result2);
                }
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                _result.Merge(CHNLSVC.CommonSearch.GetUserLocationSearchData(_CommonSearch.SearchParams, null, null));
                //----------------------------------------------------------------------------
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = TextBoxLoc; //txtBox;
                _CommonSearch.ShowDialog();
                TextBoxLoc.Focus();

                //------------------------------------
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

        private void btnAllLoc_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in GridAllLocations.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
                GridAllLocations.EndEdit();
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

        private void btnNoneLoc_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in GridAllLocations.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = false;
                }
                GridAllLocations.EndEdit();
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

        private void btnClearLoc_Click(object sender, EventArgs e)
        {
            GridAllLocations.AutoGenerateColumns = false;
            GridAllLocations.DataSource = null;
        }

        private void btnViewNotAv_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                grvNotAvailableItms.DataSource = null;
                grvNotAvailableItms.AutoGenerateColumns = false;
                DataTable dt = CHNLSVC.Inventory.Get_Reserved_Serials(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                grvNotAvailableItms.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
            }
        }

        private void btnItemCdSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchType = "ITEMS";
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txt_ItemCode;
                _CommonSearch.ShowDialog();
                TextBoxCode.Focus();
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

        private void TextBoxCode_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.imgItemSearch_Click(sender, e);
        }

        private void TextBoxModel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.btnModleSearch_Click(sender, e);
        }

        private void TextBoxMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.ImageButtonMain_Click(sender, e);
        }

        private void TextBoxSub_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.ImageButtonSub_Click(sender, e);
        }

        private void TextBoxRange_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.ImageButtonRange_Click(sender, e);
        }

        private void checkAllLoc_CheckedChanged(object sender, EventArgs e)
        {
            if (checkAllLoc.Checked)
            {
                TextBoxLoc.Text = "";
                selectLocation = "%";
            }
            else
            {
                TextBoxLoc.Text = BaseCls.GlbUserDefLoca;
                selectLocation = TextBoxLoc.Text;
                try
                {
                    this.TextBoxLoc_Leave(sender, e);
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
        }

        private void TextBoxCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                //TextBoxModel.Focus();
                this.btnView_Click(null, null);
            }
        }

        private void TextBoxModel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                TextBoxRange.Focus();
            }
        }

        private void TextBoxRange_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                TextBoxMain.Focus();
            }
        }

        private void TextBoxMain_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                TextBoxSub.Focus();
            }
        }

        private void TextBoxCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.imgItemSearch_Click(sender, e);
            }
        }

        private void TextBoxModel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.btnModleSearch_Click(sender, e);
            }
        }

        private void TextBoxMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.ImageButtonMain_Click(sender, e);
            }
        }

        private void TextBoxSub_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.ImageButtonSub_Click(sender, e);
            }
        }

        private void TextBoxRange_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.ImageButtonRange_Click(sender, e);
            }
        }

        private void InventoryTracker_Shown(object sender, EventArgs e)
        {
            this.btnAllLoc_Click(sender, e);
        }

        private void TextBoxLoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.btnLocSearch_Click(sender, e);
            }
        }

        private void TextBoxCompany_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.ImageButtonCompany_Click(sender, e);
            }
        }

        private void TextBoxLoc_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.btnLocSearch_Click(sender, e);
        }

        private void TextBoxCompany_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.ImageButtonCompany_Click(sender, e);
        }

        private void CheckBoxShowCostVal_CheckedChanged_1(object sender, EventArgs e)
        {
            if (CheckBoxShowCostVal.Checked)
            {
                GridViewItemDetails.Columns["Cost_Value"].Visible = true;
                ucItemSerialView1.ShowCost = true;
                // DivTotal.Visible = true;
            }
            else
            {
                //GridViewItemDetails.Columns[7].Visible = false;
                GridViewItemDetails.Columns["Cost_Value"].Visible = false;
                ucItemSerialView1.ShowCost = false;
                // DivTotal.Visible = false;
            }
        }

        private void label12_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Int32 x_position = 0;
                Int32 y_position = 0;

                //x_position = (this.panel_move.Location.X + e.X) > this.Width ? this.panel_move.Location.X : this.panel_move.Location.X + e.X;
                x_position = this.panel_serialView.Location.X + e.X;
                y_position = this.panel_serialView.Location.Y + e.Y;

                this.panel_serialView.Location = new Point(x_position, y_position);
                //this.panel_move.Location = new Point(Cursor.Position.X + e.X, Cursor.Position.Y + e.Y);
            }
        }

        private void TextBoxLoc_TextChanged(object sender, EventArgs e)
        {
            this.TextBoxLoc_Leave(sender, e);
        }

        private void btnChnlSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextBoxCompany.Text.Trim() == "")
                {
                    MessageBox.Show("Enter Company Code");
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvTrcChnl);
                DataTable _result = CHNLSVC.CommonSearch.GetInventoryTrackeChannel(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtChannel;
                _CommonSearch.ShowDialog();

                txtChannel.Focus();
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

        private void btnChnlView_Click(object sender, EventArgs e)
        {
            try
            {
                GridAllLocations.EndEdit();
                lblNote.Text = "";
                TextBoxTqty.Text = "";
                txtResQty.Text = "";
                txtInHand.Text = "";
                Boolean allow_SCM = CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "SCMI");
                Boolean allow_SCM2 = CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "SCM2I");

                if (allow_SCM == false)
                {
                    allow_SCM = CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10037);
                }
                if (allow_SCM2 == false)
                {
                    allow_SCM2 = CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10038);
                }

                if (TextBoxLoc.Text.Trim() == BaseCls.GlbUserDefLoca)
                {
                    // allow_SCM = true;
                    allow_SCM2 = true;
                }
                if (allow_SCM == true && allow_SCM2 == true)
                {
                    //lblNote.Text = "Enquired from both SCM and SCM2 tables.";
                    lblNote.Text = "Enquired from allowed locations and warehouses.";
                }
                else if (allow_SCM == true && allow_SCM2 == false)
                {
                    // lblNote.Text = "Enquired from SCM tables only."; //
                    lblNote.Text = "Enquired from allowed locations and warehouses only";
                }
                else if (allow_SCM == false && allow_SCM2 == true)
                {
                    //lblNote.Text = "Enquired from SCM2 tables only";//Enquired from allowed locations only
                    lblNote.Text = "Enquired from allowed locations only.";
                }
                else
                {
                    //lblNote.Text = "Permission not granted for either SCM or SCM2.";
                    lblNote.Text = "Permission not granted.";
                }
                GridViewItemDetails.DataSource = null;
                GridViewItemDetails.AutoGenerateColumns = false;
                GridViewItemDetails.DataSource = new DataTable();
                GridViewItemDetails.Refresh();

                DataTable tbl_all = new DataTable();
                DataTable tbl = new DataTable();
                GridViewDataBind(tbl_all);

                string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;

                //selectLocation = "";

                StringBuilder paramsText = new StringBuilder();
                string seperator = "|";
                CommonUIDefiniton.SearchUserControlType _type = new CommonUIDefiniton.SearchUserControlType();
                paramsText.Append(((int)_type).ToString() + ":");

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                // _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Inventory_Tracker);

                //-----------------------------------------------------------------
                string status = null;
                if (DDLStatus.SelectedItem == null)
                    status = "";
                else
                {
                    //---------------------
                    ComboboxItem combo_reqtp = (ComboboxItem)DDLStatus.SelectedItem;
                    status = combo_reqtp.Value.ToString();
                    //---------------------
                }

                _CommonSearch.SearchParams = paramsText.Append(TextBoxCode.Text.ToUpper() + seperator + TextBoxModel.Text.ToUpper() + seperator + status + seperator + TextBoxCompany.Text.Trim().ToUpper() + seperator + TextBoxMain.Text.ToUpper() + seperator + TextBoxSub.Text.ToUpper() + seperator + txtChannel.Text.Trim().ToUpper() + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "Loc" + seperator + "" + seperator + TextBoxRange.Text.ToUpper() + seperator + CheckBoxShowCostVal.Checked).ToString();
                tbl = CHNLSVC.CommonSearch.GetInventoryTrackerSearchData_new2(_CommonSearch.SearchParams, allow_SCM, allow_SCM2, out errMsg);
                //-----------------------------------------------------------------
                if (tbl != null)
                {
                    if (tbl.Rows.Count > 0)
                    {
                        tbl_all.Merge(tbl);
                    }
                }
                GridViewDataBind(tbl_all);
                if (txtInHand.Text == "0")
                {
                    // MessageBox.Show("No stock available.");
                    MessageBox.Show("No stock available.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return;
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

        private void btnBrandSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = TextBoxBrand;
                //_CommonSearch.txtSearchbyword.Text = txtBrand.Text;
                _CommonSearch.ShowDialog();
                TextBoxBrand.Focus();
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

        private void txtChnlLoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {
                    this.TextBoxLoc_Leave(sender, e);
                }
                catch (Exception ex) { }
            }
        }

        private void btnSearchChnlLoc_Click(object sender, EventArgs e)
        {
            //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location);
            //DataTable _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            //_CommonSearch.dvResult.DataSource = _result;
            //_CommonSearch.BindUCtrlDDLData(_result);
            //------------------------------------------------------------------------------
            try
            {
                if (TextBoxCompany.Text.Trim() == "")
                {
                    MessageBox.Show("Enter Company Code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtChannel.Text.Trim() == "")
                {
                    MessageBox.Show("Enter Channel Code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtChnlLoc;
                _CommonSearch.ShowDialog();

                txtChnlLoc.Focus();
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

        private void btnLocationSearch_Click(object sender, EventArgs e)
        {
        }

        private void btnItemCd_PD_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchType = "ITEMS";
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItemCd_PD;
                _CommonSearch.ShowDialog();
                txtItemCd_PD.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

            //CHNLSVC.Sales.GetItemCode(BaseCls.GlbUserComCode,
        }

        private void btnGetItemDet_Click(object sender, EventArgs e)
        {
            try
            {
                //sp_getItemStatusWiseWarrDet = new

                //TODO: GET ITEM DET
                // DataTable dt= CHNLSVC.Sales.GetItemCode(BaseCls.GlbUserComCode, txtItemCd_PD.Text.Trim());

                if (string.IsNullOrEmpty(txtItemCd_PD.Text))
                {
                    Clear();
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                MasterItem mst_item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItemCd_PD.Text.Trim());
                if (mst_item == null)
                {
                    MessageBox.Show("Invalid Item Code", "Item Code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Clear();
                    this.Cursor = Cursors.Default;
                    return;
                }
                else
                {
                    txtItemCD_PDv.Text = mst_item.Mi_cd;
                    txtItemDesc_PDv.Text = mst_item.Mi_shortdesc;
                    txtItemBrand_PDv.Text = mst_item.Mi_brand;
                    txtItemMod_PDv.Text = mst_item.Mi_model;
                    txtExColor_PDv.Text = mst_item.Mi_color_ext;
                    txtInColor_PDv.Text = mst_item.Mi_color_int;
                    txtItemCatM_PDv.Text = mst_item.Mi_cate_1;
                    //txtItemTaxRt_PDv.Text= mst_item.
                    chkHasHpIns_PDv.Checked = mst_item.Mi_need_insu;
                    txtUOM.Text = mst_item.Mi_itm_uom;
                    chkAct.Checked = mst_item.Mi_act;
                    txtCat1.Text = mst_item.Mi_cate_1;
                    txtCat2.Text = mst_item.Mi_cate_2;
                    txtCat3.Text = mst_item.Mi_cate_3;
                    txtPackCD.Text = mst_item.Mi_packing_cd;

                    //add sachith
                    if (mst_item.Mi_is_ser1 == 1)
                        chkSerial.Checked = true;
                    else
                        chkSerial.Checked = false;
                    chkWarranty.Checked = mst_item.Mi_warr;
                    chkInsurance.Checked = mst_item.Mi_insu_allow;
                    txtItmType.Text = mst_item.Mi_itm_tp;

                    DataTable _cat1 = CHNLSVC.General.GetMainCategoryDetail(txtCat1.Text);
                    lblCat1.Text = _cat1.Rows[0]["ric1_desc"].ToString();
                    if (string.IsNullOrEmpty(_cat1.Rows[0]["ric1_age"].ToString()))
                    {
                        lblCate1Age.Text = "Product aging policy is not setup";
                    }
                    else
                    {
                        lblCate1Age.Text = "Product aging policy activate after " + _cat1.Rows[0]["ric1_age"].ToString() + " days";
                    }

                    DataTable _cat2 = CHNLSVC.Sales.GetItemSubCate2(txtCat1.Text, txtCat2.Text);
                    lblCat2.Text = _cat2.Rows[0]["ric2_desc"].ToString();

                    DataTable _cat3 = CHNLSVC.Sales.GetItemSubCate3(txtCat1.Text, txtCat2.Text, txtCat3.Text);
                    if(_cat3.Rows.Count>0)
                    lblCat3.Text = _cat3.Rows[0]["ric2_desc"].ToString();

                    Decimal TAX_RT = 0;  

                    //--------------------------------------------

                    //GetItemStatusWiseWarrantyPeriods
                    DataTable warr_dt = CHNLSVC.Sales.GetItemStatusWiseWarrantyPeriods(mst_item.Mi_cd, string.Empty);

                    grvWarranty.DataSource = null;
                    grvWarranty.AutoGenerateColumns = false;
                    grvWarranty.DataSource = warr_dt;

                    //kapila 9/3/2017
                    DataTable _warrtax =null;
                    if (_isStrucBaseTax)
                    {
                        //kapila 21/4/2017
                        _warrtax = CHNLSVC.Inventory.GetTaxStrucData(BaseCls.GlbUserComCode, mst_item.Mi_cd);

                        List<MasterItemTax> _itmTax = new List<MasterItemTax>();
                        MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, mst_item.Mi_cd);
                        _itmTax = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, mst_item.Mi_cd, "GOD", string.Empty, string.Empty, _mstItem.Mi_anal1);
                        if (_itmTax.Count>0)
                        {
                            foreach (MasterItemTax _one in _itmTax)
                            {
                                TAX_RT = _one.Mict_tax_rate;
                            }
                        }
                        else
                        {
                            _itmTax = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, mst_item.Mi_cd, "GDLP", string.Empty, string.Empty, _mstItem.Mi_anal1);
                            if (_itmTax.Count > 0)
                            {
                                foreach (MasterItemTax _one in _itmTax)
                                {
                                    TAX_RT = _one.Mict_tax_rate;
                                }
                            }
                        }
                    }
                    else
                    {
                        _warrtax = CHNLSVC.Inventory.GetItemTaxData(BaseCls.GlbUserComCode, mst_item.Mi_cd);

                        CHNLSVC.Sales.GET_Item_vat_Rate(BaseCls.GlbUserComCode, mst_item.Mi_cd, "VAT");
                        
                    }

                    txtItemTaxRt_PDv.Text = TAX_RT.ToString();

                    grvTax.DataSource = null;
                    grvTax.AutoGenerateColumns = false;
                    grvTax.DataSource = _warrtax;

                    gvComponent.AutoGenerateColumns = false;
                    DataTable _component = CHNLSVC.Inventory.GetItemComponentTable(txtItemCd_PD.Text.Trim());

                    gvComponent.DataSource = _component;

                    grvService.AutoGenerateColumns = false;
                    DataTable _service = CHNLSVC.Inventory.GetItemServiceSchedule(txtItemCd_PD.Text.Trim());

                    grvService.DataSource = _service;

                    grnSpWara.AutoGenerateColumns = false;
                    DataTable _SpWara = CHNLSVC.Sales.GetAllPCWara(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtItemCd_PD.Text.Trim());
                    grnSpWara.DataSource = _SpWara;

                    dgvSimilar.AutoGenerateColumns = false;
                    List<MasterItemSimilar> _similar = CHNLSVC.Inventory.GetSimilarItems("I", txtItemCd_PD.Text.Trim(), BaseCls.GlbUserComCode, DateTime.Now.Date, null, null, null, BaseCls.GlbUserDefProf);
                    if (_similar==null) // add by tharanga 2017/08/15
                    {
                        _similar = CHNLSVC.Inventory.GetSimilarItems("S", txtItemCd_PD.Text.Trim(), BaseCls.GlbUserComCode, DateTime.Now.Date, null, null, null,BaseCls.GlbUserDefProf);
                    }
                    dgvSimilar.DataSource = _similar;
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------
        private string second_SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Brand:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Brand.ToString() + seperator + "" + seperator);
                        //paramsText.Append("" + seperator + "" + seperator + txtBrand_PD.Text.Trim().ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        paramsText.Append(TextBoxCompany.Text + seperator);
                        break;
                    }

                default:
                    break;
            }
            return paramsText.ToString();
        }

        private void btnLoc_PD_Click(object sender, EventArgs e)
        {
        }

        private void btnModle_PD_Click(object sender, EventArgs e)
        {
            //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            //_CommonSearch.SearchParams = second_SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
            //DataTable _result = CHNLSVC.CommonSearch.GetAllModels(_CommonSearch.SearchParams, null, null);

            //_CommonSearch.dvResult.DataSource = _result;
            //_CommonSearch.BindUCtrlDDLData(_result);
            //_CommonSearch.obj_TragetTextBox = txtModle_PD; //txtBox;
            //_CommonSearch.ShowDialog();
            //txtModle_PD.Focus();
        }

        private void btnBrand_PD_Click(object sender, EventArgs e)
        {
            try
            {
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

        private void btnMainCat_PD_Click(object sender, EventArgs e)
        {
        }

        private void txtBrand_PD_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtItemCd_PD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnItemCd_PD_Click(null, null);
            }
            if (e.KeyCode == Keys.Enter)
            {
                btnGetItemDet_Click(null, null);
            }
        }

        private void txtItemCd_PD_DoubleClick(object sender, EventArgs e)
        {
            btnItemCd_PD_Click(null, null);
        }

        private void chkChannel_CheckedChanged(object sender, EventArgs e)
        {
            if (chkChannel.Checked)
            {
                TextBoxLoc.Enabled = false;
                TextBoxLoc.Text = "";

                txtChannel.Enabled = true;
                txtChnlLoc.Enabled = true;

                btnChnlSearch.Enabled = true;
                btnSearchChnlLoc.Enabled = true;
            }
            else
            {
                TextBoxLoc.Enabled = true;
                TextBoxLoc.Text = BaseCls.GlbUserDefLoca;
                txtChannel.Text = "";
                txtChnlLoc.Text = "";

                txtChannel.Enabled = false;
                txtChnlLoc.Enabled = false;

                btnChnlSearch.Enabled = false;
                btnSearchChnlLoc.Enabled = false;
            }
        }

        private void btnTaxCom_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanySearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtTaxCom;
                _CommonSearch.ShowDialog();
                txtTaxCom.Focus();
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

        private void txtTaxCom_Leave(object sender, EventArgs e)
        {
            if (txtItemCd_PD.Text == "")
            {
                return;
            }

            DataTable _warrtax = CHNLSVC.Inventory.GetItemTaxData(txtTaxCom.Text, txtItemCd_PD.Text.ToUpper());

            grvTax.DataSource = null;
            grvTax.AutoGenerateColumns = false;
            grvTax.DataSource = _warrtax;
        }

        private void txtTaxCom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                grvTax.Focus();
        }

        private void txtChnlLoc_Leave(object sender, EventArgs e)
        {
            if (TextBoxCompany.Text.Trim() == "")
            {
                MessageBox.Show("Enter Company Code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtChnlLoc.Text = "";
                return;
            }
            if (txtChannel.Text.Trim() == "")
            {
                MessageBox.Show("Enter Channel Code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtChnlLoc.Text = "";
                return;
            }
        }

        #region Rooting for Load Item Detail

        private bool LoadItemDetail(string _item, TextBox _txt)
        {
            bool _isValid = false;
            try
            {
                lblItemDescription.Text = "Description : ";
                lblItemModel.Text = "Model : ";
                lblItemBrand.Text = "Brand : ";
                //lblItemSubStatus.Text = "Serial Status : ";
                //lblVatRate.Text = "Imported VAT Rt. : ";
                MasterItem _itemdetail = new MasterItem();

                if (!string.IsNullOrEmpty(_item)) _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                if (_itemdetail == null || string.IsNullOrEmpty(_itemdetail.Mi_cd))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please check the item code", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _txt.Clear();
                    _txt.Focus();
                    _isValid = false;
                    return _isValid;
                }
                if (_itemdetail != null)
                    if (!string.IsNullOrEmpty(_itemdetail.Mi_cd))
                    {
                        _isValid = true;
                        string _description = _itemdetail.Mi_longdesc;
                        string _model = _itemdetail.Mi_model;
                        string _brand = _itemdetail.Mi_brand;
                        string _serialstatus = _itemdetail.Mi_is_ser1 == 1 ? "Available" : "None";

                        lblItemDescription.Text = "Description : " + _description;
                        lblItemModel.Text = "Model : " + _model;
                        lblItemBrand.Text = "Brand : " + _brand;
                        //lblItemSubStatus.Text = "Serial Status : " + _serialstatus;

                        //Decimal VAT_RATE = CHNLSVC.Sales.GET_Item_vat_Rate(BaseCls.GlbUserComCode, _itemdetail.Mi_cd, "VAT");
                        //lblVatRate.Text = "Imported VAT Rt. : " + VAT_RATE.ToString() + "%";
                    }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return _isValid;
            }
            finally
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
            return _isValid;
        }

        #endregion Rooting for Load Item Detail

        private void txt_ItemCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_ItemCode.Text))
                {
                    LoadItemDetail(txt_ItemCode.Text.Trim(), txt_ItemCode);
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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                btnViewNotAv_Click(null, null);
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                txtComp.Text = BaseCls.GlbUserComCode;
                GetCompanyDet();
                FillCategories();
                dgvICDetails.AutoGenerateColumns = false;
                chkICAdvSch_CheckedChanged(null, null);
                txtIClocationDef.Text = BaseCls.GlbUserDefLoca;
                chkAllconditions.Checked = true;
            }
        }

        private void grvNotAvailableItms_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (grvNotAvailableItms.ColumnCount > 0)
                {
                    Int32 _rowIndex = e.RowIndex;
                    Int32 _colIndex = e.ColumnIndex;

                    if (_rowIndex != -1)
                    {
                        #region Get Scan Infor

                        string _docNo = grvNotAvailableItms.Rows[_rowIndex].Cells["col_InwardDoc"].Value.ToString();
                        string _docType = string.Empty;
                        string _scanDoc = string.Empty;
                        int _serID = Convert.ToInt32(grvNotAvailableItms.Rows[_rowIndex].Cells["col_SerialID"].Value);
                        int _scanSeq = 0;

                        DataTable _infor = CHNLSVC.Inventory.GetScanDocInfor(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _docNo, _serID);
                        if (_infor != null && _infor.Rows.Count > 0)
                        {
                            _docType = _infor.Rows[0].Field<string>("TUH_DOC_TP");
                            _scanDoc = _infor.Rows[0].Field<string>("TUH_DOC_NO");
                            lblpickdocno.Text = _scanDoc + " | " + _docType;
                            lblpickuser.Text = _infor.Rows[0].Field<string>("TUS_CRE_BY");
                            lblpickdatetime.Text = _infor.Rows[0].Field<DateTime>("TUS_CRE_DT").ToString();
                            _scanSeq = Convert.ToInt32(_infor.Rows[0].Field<Int64>("TUH_USRSEQ_NO"));
                        }
                        else
                        {
                            _scanSeq = -1;
                            lblpickdocno.Text = string.Empty;
                            lblpickuser.Text = string.Empty;
                            lblpickdatetime.Text = string.Empty;
                        }

                        #endregion Get Scan Infor

                        #region Deleting Row

                        bool _isDelete = true;
                        if (_scanSeq == -1)
                        {
                            _isDelete = true;
                        }

                        if (_docType == "COM_OUT")
                        {
                            _isDelete = true;
                        }

                        if (_isDelete == true)
                        {
                            if (CHNLSVC.Inventory.Check_Valid_Document(BaseCls.GlbUserComCode, _scanDoc, "ADV_REC"))
                            { _isDelete = false; }
                            else
                            { _isDelete = true; }
                        }

                        if (_isDelete == true)
                        {
                            if (CHNLSVC.Inventory.Check_Valid_Document(BaseCls.GlbUserComCode, _scanDoc, "QUO"))
                            { _isDelete = false; }
                            else
                            { _isDelete = true; }
                        }

                        if (_isDelete == true)
                        {
                            //Add by Chamal 26-05-2014 (Vehicle Reg. Receipt)
                            if (CHNLSVC.Inventory.Check_Valid_Document(BaseCls.GlbUserComCode, _scanDoc, "RECEIPT"))
                            { _isDelete = false; }
                            else
                            { _isDelete = true; }
                        }

                        if (_colIndex == 0)
                        {
                            if (_isDelete == false)
                            {
                                MessageBox.Show("Can't removed this serial!", "Removing...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            if (MessageBox.Show("Do you want to remove?", "Removing...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            { return; }
                            else
                            {
                                string _err = string.Empty;
                                int _ref = CHNLSVC.Inventory.RemoveReservedSerials(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _scanSeq, _docNo, _serID, BaseCls.GlbUserID, out _err);
                                if (_ref == -1)
                                {
                                    MessageBox.Show("Error Occurred while processing...\n" + _err, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    CHNLSVC.CloseChannel();
                                }
                                else if (_ref == 1)
                                {
                                    btnViewNotAv_Click(null, null);
                                    MessageBox.Show("Done!", "Removing...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            return;
                        }

                        #endregion Deleting Row
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel();
            }
            finally
            {
                this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels();
            }
        }

        private void GridViewItemDetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (GridViewItemDetails.ColumnCount > 0)
                {
                    Int32 _rowIndex = e.RowIndex;
                    Int32 _colIndex = e.ColumnIndex;

                    if (_rowIndex != -1)
                    {
                        #region Copy text

                        string _copyText = GridViewItemDetails.Rows[_rowIndex].Cells[_colIndex].Value.ToString();
                        Clipboard.SetText(_copyText.ToString());
                        MessageBox.Show(_copyText, "Copy to Clipboard");

                        #endregion Copy text
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel();
            }
            finally
            {
                this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels();
            }
        }

        //private void btnAdvnSearch_Click(object sender, EventArgs e)
        //{
        //    if (btnAdvnSearch.BackgroundImage == FF.WindowsERPClient.Properties.Resources.downloadarrowicon)
        //    {
        //        btnAdvnSearch.BackgroundImage = FF.WindowsERPClient.Properties.Resources.ExitSCM2;

        //        ucLoactionSearch1.Visible = true;
        //        TextBoxCompany.Enabled = false;
        //        TextBoxLoc.Enabled = false;
        //    }
        //    else
        //    {
        //        btnAdvnSearch.BackgroundImage = FF.WindowsERPClient.Properties.Resources.downloadarrowicon;

        //        ucLoactionSearch1.Visible = false;
        //        TextBoxCompany.Enabled = true;
        //        TextBoxLoc.Enabled = true;
        //    }
        //}

        #region Item Condition setup

        private void btnICMainCatSch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtICMainCategori;
                _CommonSearch.ShowDialog();
                txtICMainCategori.Focus();
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

        private void btnICSubCatSch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtICSubCate;
                _CommonSearch.ShowDialog();
                txtICSubCate.Focus();
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

        private void btnICBrandSch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtICBrand;
                _CommonSearch.ShowDialog();
                txtICBrand.Focus();
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

        private void btnICItemCodeSch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchType = "ITEMS";
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtICItemCode;
                _CommonSearch.ShowDialog();
                txtICItemCode.Focus();
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

        private void btnICModelSch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                DataTable _result = CHNLSVC.CommonSearch.GetAllModels(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtICModel;
                _CommonSearch.ShowDialog();
                txtICModel.Focus();
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

        private void txtICMainCategori_DoubleClick(object sender, EventArgs e)
        {
            btnICMainCatSch_Click(null, null);
        }

        private void txtICSubCate_DoubleClick(object sender, EventArgs e)
        {
            btnICSubCatSch_Click(null, null);
        }

        private void txtICBrand_DoubleClick(object sender, EventArgs e)
        {
            btnICBrandSch_Click(null, null);
        }

        private void txtICItemCode_DoubleClick(object sender, EventArgs e)
        {
            btnICItemCodeSch_Click(null, null);
        }

        private void txtICModel_DoubleClick(object sender, EventArgs e)
        {
            btnICModelSch_Click(null, null);
        }

        private void txtICMainCategori_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnICMainCatSch_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnView_Click(null, null);
            }
        }

        private void txtICSubCate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnICSubCatSch_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnView_Click(null, null);
            }
        }

        private void txtICBrand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnICBrandSch_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnView_Click(null, null);
            }
        }

        private void txtICItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnICItemCodeSch_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnView_Click(null, null);
            }
        }

        private void txtICModel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnICModelSch_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnView_Click(null, null);
            }
        }

        private void txtComp_DoubleClick(object sender, EventArgs e)
        {
        }

        private void txtChanel_DoubleClick(object sender, EventArgs e)
        {
        }

        private void txtSChanel_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (txtComp.Text == "")
                {
                    MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (string.IsNullOrEmpty(txtChanel.Text))
                {
                    MessageBox.Show("Select the Chanel", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel);
                DataTable _result = CHNLSVC.CommonSearch.Get_LOC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSChanel;
                _CommonSearch.ShowDialog();
                txtSChanel.Select();

                DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "SCHNL", txtSChanel.Text);
                foreach (DataRow row2 in LocDes.Rows)
                {
                    txtSChnlDesc.Text = row2["descp"].ToString();
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Inventory Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtArea_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (txtComp.Text == "")
                {
                    MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area);
                DataTable _result = CHNLSVC.CommonSearch.Get_LOC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtArea;
                _CommonSearch.ShowDialog();
                txtArea.Select();

                DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "AREA", txtArea.Text);
                foreach (DataRow row2 in LocDes.Rows)
                {
                    txtAreaDesc.Text = row2["descp"].ToString();
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Inventory Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtRegion_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (txtComp.Text == "")
                {
                    MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region);
                DataTable _result = CHNLSVC.CommonSearch.Get_LOC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtRegion;
                _CommonSearch.ShowDialog();
                txtRegion.Select();

                DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "REGION", txtRegion.Text);
                foreach (DataRow row2 in LocDes.Rows)
                {
                    txtRegionName.Text = row2["descp"].ToString();
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Inventory Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtZone_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (txtComp.Text == "")
                {
                    MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone);
                DataTable _result = CHNLSVC.CommonSearch.Get_LOC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtZone;
                _CommonSearch.ShowDialog();
                txtZone.Select();

                DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "ZONE", txtZone.Text);
                foreach (DataRow row2 in LocDes.Rows)
                {
                    txtZoneDesc.Text = row2["descp"].ToString();
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Inventory Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtPC_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (txtComp.Text == "")
                {
                    MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Get_LOC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPC;
                _CommonSearch.ShowDialog();
                txtPC.Select();
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Inventory Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtComp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_LOC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtComp;
                _CommonSearch.ShowDialog();
                txtComp.Select();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnView_Click(null, null);
            }
        }

        private void txtChanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtChanel_DoubleClick_1(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnView_Click(null, null);
            }
        }

        private void txtSChanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtSChanel_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnView_Click(null, null);
            }
        }

        private void txtArea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtArea_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnView_Click(null, null);
            }
        }

        private void txtRegion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtRegion_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnView_Click(null, null);
            }
        }

        private void txtZone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtZone_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnView_Click(null, null);
            }
        }

        private void txtPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtPC_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnView_Click(null, null);
            }
        }

        private void dgvICDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    lblICLocatiob.Text = "[" + dgvICDetails.Rows[e.RowIndex].Cells["ICLocation"].Value.ToString() + "] " + dgvICDetails.Rows[e.RowIndex].Cells["LocationDesc"].Value.ToString();
                    lblRemark.Text = dgvICDetails.Rows[e.RowIndex].Cells["ICRemark"].Value.ToString();
                    lblICItem.Text = dgvICDetails.Rows[e.RowIndex].Cells["ItemShortDesc"].Value.ToString();
                    lblICConditiondec.Text = dgvICDetails.Rows[e.RowIndex].Cells["ConditionDesc"].Value.ToString();
                    lblICBrand.Text = dgvICDetails.Rows[e.RowIndex].Cells["ICBrand"].Value.ToString();
                }

                if (e.ColumnIndex == 13 && e.RowIndex != -1)
                {
                    dgvICDetails.Rows[e.RowIndex].Selected = true;
                    DataGridViewRow gvrPack = dgvICDetails.SelectedRows[0];
                    DataTable tbl = new DataTable();
                    DataTable tbl_all = new DataTable();

                    string packItem = gvrPack.Cells["col_pacItm"].Value.ToString();
                    if (string.IsNullOrEmpty(packItem) || packItem == "N/A" || packItem == "NA")
                    {
                        MessageBox.Show("Packing item code not setup.", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        lblPackItem.Text = packItem;
                    }

                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Add_Inv_Tracker);
                    tbl = CHNLSVC.CommonSearch.GetInventoryTrackerSearchData_new2(_CommonSearch.SearchParams, true, true, out errMsg);
                    if (tbl != null)
                    {
                        if (tbl.Rows.Count > 0)
                        {
                            tbl_all.Merge(tbl);
                        }
                    }

                    gvStatus.AutoGenerateColumns = false;
                    gvStatus.DataSource = new DataTable();
                    gvStatus.DataSource = tbl_all;

                    if (tbl_all.Rows.Count > 0)
                    {
                        pnlPackingStock.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("No stocks available.", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
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

        private void chkAllconditions_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllconditions.Checked)
            {
                ddlCItemStatus.Enabled = false;
            }
            else
            {
                ddlCItemStatus.Enabled = true;
            }
        }

        private void dgvICDetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (dgvICDetails.ColumnCount > 0)
                {
                    Int32 _rowIndex = e.RowIndex;
                    Int32 _colIndex = e.ColumnIndex;

                    if (_rowIndex != -1)
                    {
                        #region Copy text

                        string _copyText = dgvICDetails.Rows[_rowIndex].Cells[_colIndex].Value.ToString();
                        Clipboard.SetText(_copyText.ToString());
                        MessageBox.Show(_copyText, "Copy to Clipboard");

                        #endregion Copy text
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel();
            }
            finally
            {
                this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels();
            }
        }

        private void btnICLoc_Click(object sender, EventArgs e)
        {
            try
            {
                TextBox txtBox = new TextBox();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;

                DataTable _result = new DataTable();
                Boolean allow_WHAREHOUSE = CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "INVAC");
                DataTable _result2 = null;
                if (allow_WHAREHOUSE == true)
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.WHAREHOUSE);
                    _result2 = CHNLSVC.Inventory.GetLocationByType(_CommonSearch.SearchParams, null, null);
                    _result.Merge(_result2);
                }

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                _result.Merge(CHNLSVC.CommonSearch.GetUserLocationSearchData(_CommonSearch.SearchParams, null, null));
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtIClocationDef;
                _CommonSearch.ShowDialog();
                txtIClocationDef.Focus();
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

        private void txtIClocationDef_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnICLoc_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnView_Click(null, null);
            }
        }

        private void txtIClocationDef_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnICLoc_Click(null, null);
        }

        private void ViewItemCondtion()
        {
            Cursor = Cursors.WaitCursor;
            List<string> LocationList = new List<string>();
            if (chkICAdvSch.Checked)
            {
                string com = txtComp.Text;
                string chanel = txtChanel.Text;
                string subChanel = txtSChanel.Text;
                string area = txtArea.Text;
                string region = txtRegion.Text;
                string zone = txtZone.Text;
                string pc = txtPC.Text;

                LocationList.Clear();
                string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;

                DataTable dt = CHNLSVC.Sales.GetLoc_from_Hierachy_Rep_all(com, chanel, subChanel, area, region, zone, pc);

                foreach (DataRow drow in dt.Rows)
                {
                    LocationList.Add(drow["PROFIT_CENTER"].ToString());
                }

            }
            else
            {
                LocationList.Add(txtIClocationDef.Text);
            }

            string ItemCondition = string.Empty;
            if (!chkAllconditions.Checked)
            {
                ItemCondition = ddlCItemStatus.SelectedValue.ToString();
            }

            DataTable dtDetails = new DataTable();
            dtDetails = CHNLSVC.General.Get_ItemCondition_Inquiary(BaseCls.GlbUserID, txtComp.Text, txtICMainCategori.Text, txtICSubCate.Text, txtICBrand.Text, txtICItemCode.Text, txtICModel.Text, ItemCondition, LocationList);

            if (dtDetails.Rows.Count > 0)
            {
                dgvICDetails.DataSource = dtDetails;
            }
            else
            {
                dgvICDetails.DataSource = null;
                MessageBox.Show("No stock Available", "Item Condition - Enquiries", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Cursor = Cursors.Default;
        }

        protected void GetCompanyDet()
        {
            MasterCompany _masterComp = null;
            _masterComp = CHNLSVC.General.GetCompByCode(txtComp.Text);
            if (_masterComp != null)
            {
                txtCompDesc.Text = _masterComp.Mc_desc;
            }
            else
            {
                txtCompDesc.Text = "";
            }
        }

        private void FillCategories()
        {
            List<ItemConditionCategori> oonditionCategoris = new List<ItemConditionCategori>();
            oonditionCategoris = CHNLSVC.General.Get_ItemConditionCategories(BaseCls.GlbUserComCode);
            ddlCItemStatus.DataSource = oonditionCategoris;
            ddlCItemStatus.DisplayMember = "rcc_desc";
            ddlCItemStatus.ValueMember = "rcc_cate";
        }

        #endregion Item Condition setup

        private void chkICAdvSch_CheckedChanged(object sender, EventArgs e)
        {
            txtIClocationDef.Clear();

            if (chkICAdvSch.Checked)
            {
                txtChanel.Enabled = true;
                txtSChanel.Enabled = true;
                txtArea.Enabled = true;
                txtRegion.Enabled = true;
                txtZone.Enabled = true;
                txtPC.Enabled = true;
            }
            else
            {
                txtChanel.Enabled = false;
                txtSChanel.Enabled = false;
                txtArea.Enabled = false;
                txtRegion.Enabled = false;
                txtZone.Enabled = false;
                txtPC.Enabled = false;

                txtChanel.Clear();
                txtChanel.Clear();
                txtSChanel.Clear();
                txtArea.Clear();
                txtRegion.Clear();
                txtZone.Clear();
                txtPC.Clear();
            }
        }

        private void txtChanel_DoubleClick_1(object sender, EventArgs e)
        {
            try
            {
                if (txtComp.Text == "")
                {
                    MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
                DataTable _result = CHNLSVC.CommonSearch.Get_LOC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtChanel;
                _CommonSearch.ShowDialog();
                txtChanel.Select();

                DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "CHNL", txtChanel.Text);
                foreach (DataRow row2 in LocDes.Rows)
                {
                    txtChnlDesc.Text = row2["descp"].ToString();
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Inventory Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnCloseStatus_Click(object sender, EventArgs e)
        {
            gvStatus.AutoGenerateColumns = false;
            gvStatus.DataSource = new DataTable();
            pnlPackingStock.Visible = false;
        }

        private void tabPage8_Click(object sender, EventArgs e)
        {

        }

        private void GridViewItemDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ucItemSerialView1_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBoxCat4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.imgBtnCat4_Click(sender, e);
            }
        }

        private void TextBoxCat4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                TextBoxCat5.Focus();
            }
        }

        private void TextBoxCat4_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.imgBtnCat4_Click(sender, e);
        }

        private void TextBoxCat5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.imgBtnCat5_Click(sender, e);
            }
        }

        private void TextBoxCat5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                txtSearchColor.Focus();
                //TextBoxMain.Focus();
            }
        }

        private void TextBoxCat5_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.imgBtnCat5_Click(sender, e);
        }

        private void imgBtnCat4_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub3);
                DataTable _result = CHNLSVC.General.GetItemSubCat4(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = TextBoxCat4;
                _CommonSearch.ShowDialog();
                TextBoxCat4.Focus();
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

        private void imgBtnCat5_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub4);
                DataTable _result = CHNLSVC.General.GetItemSubCat5(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = TextBoxCat5;
                _CommonSearch.ShowDialog();
                TextBoxCat5.Focus();
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
        //Add by tharanga 2017/08/15
        private void chkAsatdate_CheckedChanged(object sender, EventArgs e)
        {

            List<MasterItemSimilar> _similar = CHNLSVC.Inventory.GetSimilarItems("I", txtItemCd_PD.Text.Trim(), BaseCls.GlbUserComCode, DateTime.Now.Date, null, null, null, BaseCls.GlbUserDefProf);
            if (_similar == null)
            {
                _similar = CHNLSVC.Inventory.GetSimilarItems("S", txtItemCd_PD.Text.Trim(), BaseCls.GlbUserComCode, DateTime.Now.Date, null, null, null, BaseCls.GlbUserDefProf);
            }
            if (_similar != null)
            {
                DateTime today = Convert.ToDateTime(txttodate.Text.ToString());
                if (chkAsatdate.Checked == false)
                {
                    dgvSimilar.DataSource = _similar;
                }
                else
                {
                    _similar = _similar.Where(x => x.Misi_to >= today).ToList();
                    dgvSimilar.DataSource = _similar;
                }
            }
          
        }

        private void btnSearchColor_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterColor);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchColorMaster(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtSearchColor;
            _CommonSearch.ShowDialog();
            txtSearchColor.Focus();
        }

        private void txtSearchColor_DoubleClick(object sender, EventArgs e)
        {
            btnSearchColor_Click(null, null);
        }

        private void txtSearchColor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearchColor_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtSearchSize.Focus();
            }
        }

        private void txtSearchSize_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                
            }
            else if (e.KeyCode == Keys.Enter)
            {
                TextBoxMain.Focus();
            }
        }
    }
}