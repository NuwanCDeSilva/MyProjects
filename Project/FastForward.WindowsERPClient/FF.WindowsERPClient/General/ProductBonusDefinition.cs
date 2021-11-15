using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.General
{
    public partial class ProductBonusDefinition : Base
    {
        #region properties

        private List<IncentiveSch> _incSchList = null;
        private List<IncentiveSchPC> _incSchPCList = null;
        private List<IncentiveSchStkTp> _incSchStkTpList = null;
        private List<IncentiveSchDet> _incSchDetList = null;
        private List<IncentiveSchPersn> _incSchPersonList = null;
        private List<IncentiveSchSerial> _incSchSerialList = null;
        private List<IncentiveSchInc> _incSchIncList = null;
        private List<IncentiveSchIncDet> _incSchIncDet = null;
        private List<IncentiveSchPB> _incSchPBList = null;
        private List<IncentiveSchMode> _incSchModeList = null;

        private List<IncentiveSchStkTp> _incSchStkTpList_main = null;
        private List<IncentiveSchDet> _incSchDetList_main = null;
        private List<IncentiveSchPersn> _incSchPersonList_main = null;
        private List<IncentiveSchSerial> _incSchSerialList_main = null;
        private List<IncentiveSchInc> _incSchIncList_main = null;
        private List<IncentiveSchIncDet> _incSchIncDet_main = null;
        private List<IncentiveSchPB> _incSchPBList_main = null;
        private List<IncentiveSchMode> _incSchModeList_main = null;

        private Boolean _isNew = false;
        private Int32 _itemLine = 1;
        private Int32 _pcLine = 1;
        private string _refNo = "";

        #endregion

        public ProductBonusDefinition()
        {
            InitializeComponent();

            _incSchList = new List<IncentiveSch>();
            _incSchDetList = new List<IncentiveSchDet>();
            _incSchPCList = new List<IncentiveSchPC>();
            _incSchStkTpList = new List<IncentiveSchStkTp>();
            _incSchPersonList = new List<IncentiveSchPersn>();
            _incSchSerialList = new List<IncentiveSchSerial>();
            _incSchIncList = new List<IncentiveSchInc>();
            _incSchIncDet = new List<IncentiveSchIncDet>();
            _incSchModeList = new List<IncentiveSchMode>();
            _incSchPBList = new List<IncentiveSchPB>();

            _incSchDetList_main = new List<IncentiveSchDet>();
            _incSchStkTpList_main = new List<IncentiveSchStkTp>();
            _incSchPersonList_main = new List<IncentiveSchPersn>();
            _incSchSerialList_main = new List<IncentiveSchSerial>();
            _incSchIncList_main = new List<IncentiveSchInc>();
            _incSchIncDet_main = new List<IncentiveSchIncDet>();
            _incSchPBList_main = new List<IncentiveSchPB>();
            _incSchModeList_main = new List<IncentiveSchMode>();

            txtComp.Text = BaseCls.GlbUserComCode;
        }

        public void BindPartyType()
        {

            Dictionary<string, string> PartyTypes = new Dictionary<string, string>();
            //    PartyTypes.Add("GPC", "GPC");
            PartyTypes.Add("CHNL", "Channel");
            PartyTypes.Add("SCHNL", "Sub Channel");
            PartyTypes.Add("AREA", "Area");
            PartyTypes.Add("REGION", "Region");
            PartyTypes.Add("ZONE", "Zone");
            PartyTypes.Add("PC", "PC");

            DropDownListPartyTypes.DataSource = new BindingSource(PartyTypes, null);
            DropDownListPartyTypes.DisplayMember = "Value";
            DropDownListPartyTypes.ValueMember = "Key";


            cmbIncType.Items.Clear();
            cmbIncType.DataSource = null;
            cmbIncType.DataSource = CHNLSVC.General.GetAllType("INC");
            cmbIncType.DisplayMember = "Mtp_desc";
            cmbIncType.ValueMember = "Mtp_cd";
            cmbIncType.SelectedIndex = -1;

            comboBoxPayModes.DataSource = CHNLSVC.Sales.GetAllPaymentType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, null);
            comboBoxPayModes.DisplayMember = "SAPT_DESC";
            comboBoxPayModes.ValueMember = "SAPT_CD";
            comboBoxPayModes.SelectedIndex = -1;


        }

        private void EliteCommissionDefinition_Load(object sender, EventArgs e)
        {
            BindPartyType();
            BindSalesTypes();
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
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
                case CommonUIDefiniton.SearchUserControlType.PritHierarchy:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.IncSaleTp:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.OPE:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
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
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
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
                case CommonUIDefiniton.SearchUserControlType.Model:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EliteCircular:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EmployeeCate:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemStatus:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.IncentiveCirc:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AvailableSerialWithOth:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + txtItem.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceBookByCompany:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevelByBook:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtPB.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.IncentiveCircular:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void btnAddPartys_Click(object sender, EventArgs e)
        {
            //_incSchPCList = new List<IncentiveSchPC>();
            //if (grvParty.Rows.Count > 0)
            //{
            //    MessageBox.Show("Already selected", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            if (string.IsNullOrEmpty(txtHierchCode.Text))
            {
                MessageBox.Show("Select the heirachy code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Base _basePage = new Base();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();


            IncentiveSchPC _invSchPC = new IncentiveSchPC();

            DataTable dt = new DataTable();
            DataTable _result = new DataTable();
            if (DropDownListPartyTypes.SelectedValue.ToString() == "COM")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                if (txtHierchCode.Text != "")
                {
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                }
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "CHNL")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                if (txtHierchCode.Text != "")
                {
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                }
                _invSchPC.Incl_chnl = txtHierchCode.Text;
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "SCHNL")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                if (txtHierchCode.Text != "")
                {
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                }
                _invSchPC.Incl_schnl = txtHierchCode.Text;
            }

            else if (DropDownListPartyTypes.SelectedValue.ToString() == "AREA")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                if (txtHierchCode.Text != "")
                {
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                }
                _invSchPC.Incl_area = txtHierchCode.Text;
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "REGION")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                if (txtHierchCode.Text != "")
                {
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                }
                _invSchPC.Incl_reg = txtHierchCode.Text;
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "ZONE")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                if (txtHierchCode.Text != "")
                {
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                }
                _invSchPC.Incl_zone = txtHierchCode.Text;
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "PC")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                if (txtHierchCode.Text != "")
                {
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                }
                _invSchPC.Incl_pc = txtHierchCode.Text;
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "GPC")
            {
                // _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GPC);
                //  _result = _basePage.CHNLSVC.CommonSearch.Get_GPC(_CommonSearch.SearchParams, null, null);
                _result = CHNLSVC.General.Get_GET_GPC("", "");
                if (txtHierchCode.Text != "")
                {
                    _result = _basePage.CHNLSVC.General.Get_GET_GPC(txtHierchCode.Text, "");
                }
            }
            if (_result == null || _result.Rows.Count <= 0)
            {
                MessageBox.Show("Invalid search term, no result found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            _invSchPC.Incl_circ = txtCircularNo.Text;
            _invSchPC.Incl_ref = _refNo;
            _invSchPC.Incl_line = 1;
            _incSchPCList.Add(_invSchPC);

            //_pcLine = _pcLine + 1;

            grvParty.AutoGenerateColumns = false;
            grvParty.DataSource = new List<IncentiveSchPC>();
            grvParty.DataSource = _incSchPCList;


        }

        private void btnHierachySearch_Click(object sender, EventArgs e)
        {
            Base _basePage = new Base();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            DataTable dt = new DataTable();
            DataTable _result = new DataTable();
            if (DropDownListPartyTypes.SelectedValue.ToString() == "COM")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "CHNL")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "SCHNL")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "AREA")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "REGION")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "ZONE")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "PC")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "GPC")
            {
                // _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GPC);
                //  _result = _basePage.CHNLSVC.CommonSearch.Get_GPC(_CommonSearch.SearchParams, null, null);
                _result = CHNLSVC.General.Get_GET_GPC("", "");
            }

            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtHierchCode;
            _CommonSearch.ShowDialog();
            txtHierchCode.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCircularNo.Text))
            {
                MessageBox.Show("Circular Number can not be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //if (string.IsNullOrEmpty(cmbScheme.Text))
            //{
                if (_incSchDetList_main.Count <= 0)
                {
                    MessageBox.Show("Definition not found to save", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            //}

            this.Cursor = Cursors.WaitCursor;
            Process();
            this.Cursor = Cursors.Default;
        }

        private void Process()
        {
            try
            {
                IncentiveSch _tmpinvSch = new IncentiveSch();
                _tmpinvSch.Inc_all_itms = chkIncAll.Checked;
                _tmpinvSch.Inc_ref = _refNo;
                _tmpinvSch.Inc_circ = txtCircularNo.Text;
                _tmpinvSch.Inc_dt = Convert.ToDateTime(dtDate.Value).Date;
                _tmpinvSch.Inc_from = Convert.ToDateTime(dtFrom.Value).Date;
                _tmpinvSch.Inc_to = Convert.ToDateTime(dtTo.Value).Date;
                _tmpinvSch.Inc_allow_disc = chkAlwDisc.Checked;
                _tmpinvSch.Inc_allow_fwsales = chkAlwFSale.Checked;
                _tmpinvSch.Inc_fwd_period = Convert.ToInt32(txtPeriod.Text);
                _tmpinvSch.Inc_stus = "N";
                _tmpinvSch.Inc_procesed = "P";
                _tmpinvSch.Inc_xno_of_times_upd = 0;
                //_tmpinvSch.Inc_xref =
                //_tmpinvSch.Inc_xmod_by =
                //_tmpinvSch.Inc_xmod_dt =
                _tmpinvSch.Inc_cre_by = BaseCls.GlbUserID;
                _tmpinvSch.Inc_cre_when = Convert.ToDateTime(DateTime.Now).Date;
                _tmpinvSch.Inc_mod_by = BaseCls.GlbUserID;
                _tmpinvSch.Inc_mod_when = Convert.ToDateTime(DateTime.Now).Date;
                _tmpinvSch.Inc_session_id = BaseCls.GlbUserSessionID;

                _incSchList.Add(_tmpinvSch);

                //if (!string.IsNullOrEmpty(cmbScheme.Text))
                //{
                //    foreach (IncentiveSch _one in _incSchList)
                //    {
                //        List<IncentiveSch> _two = new List<IncentiveSch>();
                //        _two.Add(_one);
                //        CHNLSVC.General.SaveIncentiveSchPC(null, null, _two, null, null, null, null, null, null, null);
                //    }
                //    goto AAA;
                //}

                foreach (IncentiveSchInc _one in _incSchIncList_main)
                {
                    List<IncentiveSchInc> _two = new List<IncentiveSchInc>();
                    _two.Add(_one);
                    CHNLSVC.General.SaveIncentiveSchPC(_two, null, null, null, null, null, null, null, null, null);
                }
                foreach (IncentiveSchIncDet _one in _incSchIncDet_main)
                {
                    List<IncentiveSchIncDet> _two = new List<IncentiveSchIncDet>();
                    _two.Add(_one);
                    CHNLSVC.General.SaveIncentiveSchPC(null, _two, null, null, null, null, null, null, null, null);
                }
                foreach (IncentiveSch _one in _incSchList)
                {
                    List<IncentiveSch> _two = new List<IncentiveSch>();
                    _two.Add(_one);
                    CHNLSVC.General.SaveIncentiveSchPC(null, null, _two, null, null, null, null, null, null, null);
                }
                foreach (IncentiveSchPC _one in _incSchPCList)
                {
                    List<IncentiveSchPC> _two = new List<IncentiveSchPC>();
                    _two.Add(_one);
                    CHNLSVC.General.SaveIncentiveSchPC(null, null, null, _two, null, null, null, null, null, null);
                }
                foreach (IncentiveSchDet _one in _incSchDetList_main)
                {
                    List<IncentiveSchDet> _two = new List<IncentiveSchDet>();
                    _two.Add(_one);
                    CHNLSVC.General.SaveIncentiveSchPC(null, null, null, null, _two, null, null, null, null, null);
                }
                foreach (IncentiveSchStkTp _one in _incSchStkTpList_main)
                {
                    List<IncentiveSchStkTp> _two = new List<IncentiveSchStkTp>();
                    _two.Add(_one);
                    CHNLSVC.General.SaveIncentiveSchPC(null, null, null, null, null, _two, null, null, null, null);
                }
                foreach (IncentiveSchPersn _one in _incSchPersonList_main)
                {
                    List<IncentiveSchPersn> _two = new List<IncentiveSchPersn>();
                    _two.Add(_one);
                    CHNLSVC.General.SaveIncentiveSchPC(null, null, null, null, null, null, _two, null, null, null);
                }
                foreach (IncentiveSchSerial _one in _incSchSerialList_main)
                {
                    List<IncentiveSchSerial> _two = new List<IncentiveSchSerial>();
                    _two.Add(_one);
                    CHNLSVC.General.SaveIncentiveSchPC(null, null, null, null, null, null, null, _two, null, null);
                }
                foreach (IncentiveSchPB _one in _incSchPBList_main)
                {
                    List<IncentiveSchPB> _two = new List<IncentiveSchPB>();
                    _two.Add(_one);
                    CHNLSVC.General.SaveIncentiveSchPC(null, null, null, null, null, null, null, null, _two, null);
                }
                foreach (IncentiveSchMode _one in _incSchModeList_main)
                {
                    List<IncentiveSchMode> _two = new List<IncentiveSchMode>();
                    _two.Add(_one);
                    CHNLSVC.General.SaveIncentiveSchPC(null, null, null, null, null, null, null, null, null, _two);
                }

            AAA:
                int result = 1;
                //int result = CHNLSVC.General.SaveIncentiveSchPC(_incSchIncList_main, _incSchIncDet_main, _incSchList, _incSchPCList, _incSchDetList_main, _incSchStkTpList_main, _incSchPersonList_main, _incSchSerialList_main, _incSchPBList_main, _incSchModeList_main);

                if (result > 0)
                {
                    MessageBox.Show("Successfully saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Save Unsuccessful", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                btnClear_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtMgrRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtMgrValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtAddtionalAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void btnBrand_Click_1(object sender, EventArgs e)
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

        private void btnMainCat_Click(object sender, EventArgs e)
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

        private void btnCat_Click(object sender, EventArgs e)
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

        private void btnItem_Click(object sender, EventArgs e)
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

        private void btnPromation_Click(object sender, EventArgs e)
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

        private void btnCircular_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.IncentiveCircular);
            DataTable _result = CHNLSVC.CommonSearch.GetSearchPBonusCircular(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCircularNo;
            _CommonSearch.ShowDialog();
            txtCircularNo.Focus();

            bindSchemes();
            LoadIncentiveDet();
        }

        public void LoadIncentiveDet()
        {
            List<IncentiveSch> _t_incSch = new List<IncentiveSch>();
            _t_incSch = CHNLSVC.General.GetIncentiveSchemes(txtCircularNo.Text);
            if (_t_incSch != null)
            {
                foreach (IncentiveSch _tmp in _t_incSch)
                {
                    dtFrom.Value = Convert.ToDateTime(_tmp.Inc_from);
                    dtTo.Value = Convert.ToDateTime(_tmp.Inc_to);
                    txtPeriod.Text = _tmp.Inc_fwd_period.ToString();
                    chkAlwFSale.Checked = _tmp.Inc_allow_fwsales;
                    chkAlwDisc.Checked = _tmp.Inc_allow_disc;
                }
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //variables
            _incSchList = new List<IncentiveSch>();
            _incSchPCList = new List<IncentiveSchPC>();
            _incSchStkTpList = new List<IncentiveSchStkTp>();
            _incSchDetList = new List<IncentiveSchDet>();
            _incSchPersonList = new List<IncentiveSchPersn>();
            _incSchSerialList = new List<IncentiveSchSerial>();
            _incSchIncList = new List<IncentiveSchInc>();
            _incSchIncDet = new List<IncentiveSchIncDet>();
            _incSchModeList = new List<IncentiveSchMode>();
            _incSchPBList = new List<IncentiveSchPB>();

            _incSchStkTpList_main = new List<IncentiveSchStkTp>();
            _incSchDetList_main = new List<IncentiveSchDet>();
            _incSchPersonList_main = new List<IncentiveSchPersn>();
            _incSchSerialList_main = new List<IncentiveSchSerial>();
            _incSchIncList_main = new List<IncentiveSchInc>();
            _incSchIncDet_main = new List<IncentiveSchIncDet>();
            _incSchPBList_main = new List<IncentiveSchPB>();
            _incSchModeList_main = new List<IncentiveSchMode>();

            //controls
            this.Cursor = Cursors.WaitCursor;
            while (this.Controls.Count > 0)
            {
                Controls[0].Dispose();
            }
            InitializeComponent();
            BindPartyType();
            txtSalesTp.Enabled = true;

            _isNew = false;
            txtCircularNo.Enabled = true;

            pnlInc.Enabled = true;
            pnlItem.Enabled = true;
            pnlCreateNew.Enabled = true;

            this.Cursor = Cursors.Default;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _incSchList = new List<IncentiveSch>();
            _incSchPCList = new List<IncentiveSchPC>();
            _incSchStkTpList = new List<IncentiveSchStkTp>();
            _incSchDetList = new List<IncentiveSchDet>();
            _incSchPersonList = new List<IncentiveSchPersn>();
            _incSchSerialList = new List<IncentiveSchSerial>();
            _incSchIncList = new List<IncentiveSchInc>();
            _incSchIncDet = new List<IncentiveSchIncDet>();
            _incSchModeList = new List<IncentiveSchMode>();
            _incSchPBList = new List<IncentiveSchPB>();

            _incSchStkTpList_main = new List<IncentiveSchStkTp>();
            _incSchDetList_main = new List<IncentiveSchDet>();
            _incSchPersonList_main = new List<IncentiveSchPersn>();
            _incSchSerialList_main = new List<IncentiveSchSerial>();
            _incSchIncList_main = new List<IncentiveSchInc>();
            _incSchIncDet_main = new List<IncentiveSchIncDet>();
            _incSchPBList_main = new List<IncentiveSchPB>();
            _incSchModeList_main = new List<IncentiveSchMode>();

            _isNew = true;
            txtCircularNo.Enabled = false;
            _itemLine = 1;

            MasterAutoNumber masterAuto = new MasterAutoNumber();
            masterAuto.Aut_cate_cd = null;
            masterAuto.Aut_cate_tp = null;
            masterAuto.Aut_direction = null;
            masterAuto.Aut_modify_dt = null;
            masterAuto.Aut_moduleid = "INC";
            masterAuto.Aut_start_char = "INC";
            masterAuto.Aut_year = null;

            _refNo = CHNLSVC.Inventory.GetNextIncentiveNo(BaseCls.GlbUserDefLoca, masterAuto);

            pnlInc.Enabled = true;
            pnlItem.Enabled = true;
            pnlSaleTp.Enabled = true;
            pnlPerson.Enabled = true;
            pnlStkTp.Enabled = true;
            pnlPC.Enabled = true;
            pnlPaymode.Enabled = true;
            pnlPB.Enabled = true;
            pnlCreateNew.Enabled = true;


            grvItems.AutoGenerateColumns = false;
            grvItems.DataSource = new List<IncentiveSchDet>();
            grvItems.DataSource = _incSchDetList;
        }

        protected override bool ProcessKeyPreview(ref Message m)
        {
            if (m.Msg == 0x0100 && (int)m.WParam == 13)
            {
                this.ProcessTabKey(true);
            }
            return base.ProcessKeyPreview(ref m);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BindSalesTypes()
        {
            //cmbSalesType.DataSource = CHNLSVC.General.GetIncentiveSaleTypes();
            //cmbSalesType.DisplayMember = "incst_cd";
            //cmbSalesType.ValueMember = "incst_cd";
        }

        private void btnAddSt_Click(object sender, EventArgs e)
        {
            string code = (txtSalesTp.Text != null) ? txtSalesTp.Text.ToString() : "";
            DataTable _dt = CHNLSVC.General.GetIncentiveSTypeByCode(code);
            if (_dt.Rows.Count > 0)
            {
                grvSalesType.DataSource = _dt;
                txtSalesTp.Enabled = false;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void btn_Srch_Itm_Stus_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemStatus);
            DataTable _result = CHNLSVC.CommonSearch.GetCompanyItemStatusData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtItemStatus;
            _CommonSearch.ShowDialog();
            txtItemStatus.Focus();
        }

        private void btnAddStkTp_Click(object sender, EventArgs e)
        {
            if (chkAllStkTp.Checked == false)
            {
                if (string.IsNullOrEmpty(txtItemStatus.Text))
                {
                    MessageBox.Show("Select the item status", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataTable warr_dt = CHNLSVC.Sales.GetItemStatus(txtItemStatus.Text);
                if (warr_dt == null || warr_dt.Rows.Count <= 0)
                {
                    MessageBox.Show("Select valid item status", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (chkAllStkTp.Checked == true)
            {
                _incSchStkTpList = new List<IncentiveSchStkTp>();
                List<MasterItemStatus> pclist = new List<MasterItemStatus>();
                pclist = CHNLSVC.General.GetAllStockTypes(BaseCls.GlbUserComCode);
                foreach (MasterItemStatus pc in pclist)
                {
                    int rowIndex = -1;
                    foreach (DataGridViewRow row in grvStkTp.Rows)
                    {
                        if (row.Cells[1].Value.ToString().Equals(pc.Mis_cd))
                        {
                            rowIndex = row.Index;
                            return;
                        }
                    }

                    IncentiveSchStkTp _invSchStkTp = new IncentiveSchStkTp();

                    _invSchStkTp.Incs_circ = txtCircularNo.Text;
                    _invSchStkTp.Incs_stk_tp = pc.Mis_cd;
                    _incSchStkTpList.Add(_invSchStkTp);
                }
            }
            else
            {
                int rowIndex = -1;
                foreach (DataGridViewRow row in grvStkTp.Rows)
                {
                    if (row.Cells[1].Value.ToString().Equals(txtItemStatus.Text))
                    {
                        rowIndex = row.Index;
                        return;
                    }
                }

                IncentiveSchStkTp _invSchStkTp = new IncentiveSchStkTp();
                _invSchStkTp.Incs_circ = txtCircularNo.Text;
                _invSchStkTp.Incs_stk_tp = txtItemStatus.Text;
                _incSchStkTpList.Add(_invSchStkTp);

            }
            txtItemStatus.Text = "";

            grvStkTp.AutoGenerateColumns = false;
            grvStkTp.DataSource = new List<IncentiveSchStkTp>();
            grvStkTp.DataSource = _incSchStkTpList;
        }

        private void btnItemSearch_Click(object sender, EventArgs e)
        {
            DataTable _result = null;
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            if (opt_itm.Checked == true)
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (opt_model.Checked == true)
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                _result = CHNLSVC.CommonSearch.GetAllModels(_CommonSearch.SearchParams, null, null);
            }
            else if (opt_brand.Checked == true)
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (opt_mcat.Checked == true)
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (opt_cat.Checked == true)
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
            }

            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtItem;
            _CommonSearch.ShowDialog();
            txtItem.Select();
            LoadItemDetails();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {


            if (txtValFrom.Text == "0")
            {
                MessageBox.Show("Please select the qty/value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            IncentiveSchDet _invSchDet = new IncentiveSchDet();
            _invSchDet.Incd_is_alt_itm = chk_IsAlterItem.Checked;
            _invSchDet.Incd_alt = chk_AlterItem.Checked;
            _invSchDet.Incd_is_comb_itm = chkIsCombine.Checked;
            _invSchDet.Incd_circ = txtCircularNo.Text;

            _invSchDet.Incd_sale_tp = txtSalesTp.Text.ToString();
            _invSchDet.Incd_allow_ser = chk_Allow_Ser.Checked;
            _invSchDet.Incd_is_val_range = chkRange.Checked;
            _invSchDet.Incd_all_stk_tp = chkAllStkTp.Checked;
            if (opt_mcat.Checked == true) _invSchDet.Incd_main_cat = txtItem.Text;
            if (opt_cat.Checked == true) _invSchDet.Incd_cat = txtItem.Text;
            if (opt_brand.Checked == true) _invSchDet.Incd_brand = txtItem.Text;
            if (opt_model.Checked == true) _invSchDet.Incd_model = txtItem.Text;
            if (opt_itm.Checked == true) _invSchDet.Incd_itm = txtItem.Text;

            _invSchDet.Incd_val_from = Convert.ToInt32(txtValFrom.Text);
            _invSchDet.Incd_val_to = Convert.ToInt32(txtValTo.Text);
            _invSchDet.Incd_ref = _refNo;
            //_invSchDet.Incd_comen_dt =
            //_invSchDet.Incd_comen_line =
            //_invSchDet.Incd_is_oth_qty =
            //_invSchDet.Incd_actual_qty =
            //_invSchDet.Incd_oth_qty = 

            _incSchDetList.Add(_invSchDet);

            grvItemsDet.AutoGenerateColumns = false;
            grvItemsDet.DataSource = new List<IncentiveSchDet>();
            grvItemsDet.DataSource = _incSchDetList;

            clear_add_item();

        }

        private void btn_Srch_Party_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PritHierarchy);
            DataTable _result = CHNLSVC.CommonSearch.GetIncSchPersonSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtParty;
            _CommonSearch.ShowDialog();
            txtParty.Focus();
        }

        private void btnAddParty_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtParty.Text))
            {
                MessageBox.Show("Select the applicable party", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int rowIndex = -1;
            foreach (DataGridViewRow row in grv_Party.Rows)
            {
                if (row.Cells[1].Value.ToString().Equals(txtParty.Text))
                {
                    rowIndex = row.Index;
                    return;
                }
            }

            string _catDesc = "";
            IncentiveSchPersn _invSchPerson = new IncentiveSchPersn();
            _invSchPerson.Incp_person = txtParty.Text;
            _invSchPerson.Incp_circ = txtCircularNo.Text;
            int X = CHNLSVC.Financial.GetIncSchPsnCatDesc(txtParty.Text, out _catDesc);
            _invSchPerson.Incp_desc = _catDesc;

            _incSchPersonList.Add(_invSchPerson);

            grv_Party.AutoGenerateColumns = false;
            grv_Party.DataSource = new List<IncentiveSchPersn>();
            grv_Party.DataSource = _incSchPersonList;
            txtParty.Text = "";

        }

        private void grv_Party_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _incSchPersonList.RemoveAt(e.RowIndex);
                    BindingSource _source = new BindingSource();
                    _source.DataSource = _incSchPersonList;
                    grv_Party.DataSource = _source;
                }
            }
        }

        private void grvStkTp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _incSchStkTpList.RemoveAt(e.RowIndex);
                    BindingSource _source = new BindingSource();
                    _source.DataSource = _incSchStkTpList;
                    grvStkTp.DataSource = _source;
                }
            }
        }

        private void grvParty_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _incSchPCList.RemoveAt(e.RowIndex);
                    BindingSource _source = new BindingSource();
                    _source.DataSource = _incSchPCList;
                    grvParty.DataSource = _source;
                }
            }
        }

        private void btn_Srch_Itm_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txt_Item;
            _CommonSearch.ShowDialog();
            txt_Item.Select();
            LoadItemDetails();
        }

        private void LoadItemDetails()
        {
            if (opt_itm.Checked == true)
                if (txtItem.Text != "")
                {
                    FF.BusinessObjects.MasterItem _item = (FF.BusinessObjects.MasterItem)CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.ToUpper());
                    if (_item != null)
                    {
                        txtItemDesn.Text = _item.Mi_shortdesc;
                        txtItemDesc.Text = _item.Mi_shortdesc;
                    }
                    else
                    {
                        MessageBox.Show("Invalid Item Code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtItem.Text = "";
                        txt_Item.Text = "";
                        txtItem.Focus();
                        txtItemDesn.Text = "";
                        txtItemDesc.Text = "";
                        return;
                    }
                }
        }

        private void btn_Srch_Serial_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtItem.Text))
            {
                MessageBox.Show("Select the item code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithOth);
            DataTable _result = CHNLSVC.CommonSearch.GetAvailableSerialWithOthSerialSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtSer;
            _CommonSearch.ShowDialog();
            txtSer.Select();
        }

        private void btn_Add_Serial_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSer.Text))
            {
                MessageBox.Show("Enter serial #", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            IncentiveSchSerial _invSchSer = new IncentiveSchSerial();
            _invSchSer.Incr_ser = txtSer.Text;

            _incSchSerialList.Add(_invSchSer);

            grvSerial.AutoGenerateColumns = false;
            grvSerial.DataSource = new List<IncentiveSchSerial>();
            grvSerial.DataSource = _incSchSerialList;

            txtSer.Text = "";
        }

        private void chk_Allow_Ser_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Allow_Ser.Checked == true)
            {
                pnlSerial.Visible = true;
            }
            else
            {
                pnlSerial.Visible = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (grvItemsDet.Rows.Count == 0)
            {
                MessageBox.Show("Item Details not selected", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (grvSalesType.Rows.Count == 0)
            {
                MessageBox.Show("Sales Type not selected", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (grvParty.Rows.Count == 0)
            {
                MessageBox.Show("Business heirachy not selected", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (grvStkTp.Rows.Count == 0)
            {
                MessageBox.Show("Stock type not selected", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (grv_Party.Rows.Count == 0)
            {
                MessageBox.Show("Applicable party not selected", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (grvInc.Rows.Count == 0)
            {
                MessageBox.Show("Incentive details not entered", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (IncentiveSchDet _det in _incSchDetList)
            {

                IncentiveSchDet _tmpinvSchDet = new IncentiveSchDet();
                _tmpinvSchDet.Incd_actual_qty = _det.Incd_actual_qty;

                _tmpinvSchDet.Incd_is_alt_itm = _det.Incd_is_alt_itm;
                _tmpinvSchDet.Incd_alt = _det.Incd_alt;
                _tmpinvSchDet.Incd_is_comb_itm = _det.Incd_is_comb_itm;
                _tmpinvSchDet.Incd_itm = _det.Incd_itm;
                _tmpinvSchDet.Incd_line = _itemLine;
                _tmpinvSchDet.Incd_circ = _det.Incd_circ;

                _tmpinvSchDet.Incd_sale_tp = _det.Incd_sale_tp;
                _tmpinvSchDet.Incd_allow_ser = _det.Incd_allow_ser;
                _tmpinvSchDet.Incd_is_val_range = _det.Incd_is_val_range;
                _tmpinvSchDet.Incd_all_stk_tp = _det.Incd_all_stk_tp;
                _tmpinvSchDet.Incd_main_cat = _det.Incd_main_cat;
                _tmpinvSchDet.Incd_cat = _det.Incd_cat;
                _tmpinvSchDet.Incd_brand = _det.Incd_brand;
                _tmpinvSchDet.Incd_model = _det.Incd_model;
                _tmpinvSchDet.Incd_val_from = _det.Incd_val_from;
                _tmpinvSchDet.Incd_val_to = _det.Incd_val_to;
                _tmpinvSchDet.Incd_comen_dt = _det.Incd_comen_dt;
                _tmpinvSchDet.Incd_comen_line = _det.Incd_comen_line;
                _tmpinvSchDet.Incd_is_oth_qty = _det.Incd_is_oth_qty;
                _tmpinvSchDet.Incd_oth_qty = _det.Incd_oth_qty;
                _tmpinvSchDet.Incd_ref = _det.Incd_ref;

                _incSchDetList_main.Add(_tmpinvSchDet);

                Int32 _line = 1;
                foreach (IncentiveSchStkTp _detStk in _incSchStkTpList)
                {
                    IncentiveSchStkTp _tmpinvSchStk = new IncentiveSchStkTp();
                    _tmpinvSchStk.Incs_circ = _detStk.Incs_circ;
                    _tmpinvSchStk.Incs_dt_line = _itemLine;
                    _tmpinvSchStk.Incs_line = _line;
                    _tmpinvSchStk.Incs_ref = _refNo;
                    _tmpinvSchStk.Incs_stk_tp = _detStk.Incs_stk_tp;

                    _incSchStkTpList_main.Add(_tmpinvSchStk);
                    _line = _line + 1;
                }
                _line = 1;

                foreach (IncentiveSchPersn _detPsn in _incSchPersonList)
                {
                    IncentiveSchPersn _tmpinvSchPerson = new IncentiveSchPersn();
                    _tmpinvSchPerson.Incp_circ = _detPsn.Incp_circ;
                    _tmpinvSchPerson.Incp_dt_line = _itemLine;
                    _tmpinvSchPerson.Incp_line = _line;
                    _tmpinvSchPerson.Incp_person = _detPsn.Incp_person;
                    _tmpinvSchPerson.Incp_ref = _refNo;

                    _incSchPersonList_main.Add(_tmpinvSchPerson);
                    _line = _line + 1;
                }
                _line = 1;

                foreach (IncentiveSchSerial _detSerial in _incSchSerialList)
                {
                    IncentiveSchSerial _tmpinvSchSerial = new IncentiveSchSerial();
                    _tmpinvSchSerial.Incr_circ = _detSerial.Incr_circ;
                    _tmpinvSchSerial.Incr_dt_line = _itemLine;
                    _tmpinvSchSerial.Incr_line = _line;
                    _tmpinvSchSerial.Incr_ref = _refNo;
                    _tmpinvSchSerial.Incr_ser = _detSerial.Incr_ser;

                    _incSchSerialList_main.Add(_tmpinvSchSerial);
                    _line = _line + 1;
                }
                _line = 1;

                foreach (IncentiveSchInc _detSchInc in _incSchIncList)
                {
                    IncentiveSchInc _tmpinvSchInc = new IncentiveSchInc();
                    _tmpinvSchInc.Inci_dt_line = _itemLine;
                    _tmpinvSchInc.Inci_ref = _refNo;
                    _tmpinvSchInc.Inci_circ = txtCircularNo.Text;
                    _tmpinvSchInc.Inci_line = _line;
                    _tmpinvSchInc.Inci_inc_tp = _detSchInc.Inci_inc_tp;
                    _tmpinvSchInc.Inci_is_alt_inc = _detSchInc.Inci_is_alt_inc;
                    _tmpinvSchInc.Inci_alt = _detSchInc.Inci_alt;

                    _incSchIncList_main.Add(_tmpinvSchInc);
                    _line = _line + 1;
                }
                _line = 1;

                foreach (IncentiveSchPB _detPB in _incSchPBList)
                {
                    IncentiveSchPB _tmpinvSchPB = new IncentiveSchPB();
                    _tmpinvSchPB.Inpbl_circ = _detPB.Inpbl_circ;
                    _tmpinvSchPB.Inpbl_dt_line = _itemLine;
                    _tmpinvSchPB.Inpbl_line = _line;
                    _tmpinvSchPB.Inpbl_pb = _detPB.Inpbl_pb;
                    _tmpinvSchPB.Inpbl_pb_lvl = _detPB.Inpbl_pb_lvl;

                    _tmpinvSchPB.Inpbl_ref = _refNo;

                    _incSchPBList_main.Add(_tmpinvSchPB);
                    _line = _line + 1;
                }
                _line = 1;

                foreach (IncentiveSchMode _detMode in _incSchModeList)
                {
                    IncentiveSchMode _tmpinvSchMode = new IncentiveSchMode();
                    _tmpinvSchMode.Inpym_circ = _detMode.Inpym_circ;
                    _tmpinvSchMode.Inpym_dt_line = _itemLine;
                    _tmpinvSchMode.Inpym_line = _line;
                    _tmpinvSchMode.Inpym_mode = _detMode.Inpym_mode;
                    _tmpinvSchMode.Inpym_is_promo = _detMode.Inpym_is_promo;

                    _tmpinvSchMode.Inpym_ref = _refNo;

                    _incSchModeList_main.Add(_tmpinvSchMode);
                    _line = _line + 1;
                }
                _line = 1;

                foreach (IncentiveSchIncDet _detSchIncDet in _incSchIncDet)
                {
                    IncentiveSchIncDet _tmpinvSchIncDet = new IncentiveSchIncDet();
                    _tmpinvSchIncDet.Incid_dt_line = _itemLine;
                    _tmpinvSchIncDet.Incid_ref = _refNo;
                    _tmpinvSchIncDet.Incid_circ = txtCircularNo.Text;
                    _tmpinvSchIncDet.Incid_alt = _detSchIncDet.Incid_alt;
                    _tmpinvSchIncDet.Incid_amount = _detSchIncDet.Incid_amount;
                    _tmpinvSchIncDet.Incid_desc = _detSchIncDet.Incid_desc;
                    _tmpinvSchIncDet.Incid_inc_line = _detSchIncDet.Incid_inc_line;
                    _tmpinvSchIncDet.Incid_is_alt_itm = _detSchIncDet.Incid_is_alt_itm;
                    _tmpinvSchIncDet.Incid_itm = _detSchIncDet.Incid_itm;
                    _tmpinvSchIncDet.Incid_line = _detSchIncDet.Incid_line;
                    _tmpinvSchIncDet.Incid_qty = _detSchIncDet.Incid_qty;


                    _incSchIncDet_main.Add(_tmpinvSchIncDet);
                    _line = _line + 1;
                }


                _itemLine = _itemLine + 1;
            }
            grvItems.AutoGenerateColumns = false;
            grvItems.DataSource = new List<IncentiveSchDet>();
            grvItems.DataSource = _incSchDetList_main;

            MasterAutoNumber masterAuto = new MasterAutoNumber();
            masterAuto.Aut_cate_cd = null;
            masterAuto.Aut_cate_tp = null;
            masterAuto.Aut_direction = null;
            masterAuto.Aut_modify_dt = null;
            masterAuto.Aut_moduleid = "INC";
            masterAuto.Aut_start_char = "INC";
            masterAuto.Aut_year = null;

            Int16 X = CHNLSVC.Inventory.UpdateIncentiveNo(masterAuto);

            //_refNo = CHNLSVC.Inventory.GetNextIncentiveNo(BaseCls.GlbUserDefLoca, masterAuto);
            _pcLine = 1;
            clear_add_item_det();
        }

        private void clear_add_item()
        {
            grvSerial.DataSource = null;
            chk_IsAlterItem.Checked = false;
            chk_AlterItem.Checked = false;
            chkIsCombine.Checked = false;
            chkIsVal.Checked = false;
            chkRange.Checked = false;
            txtActVal.Text = "0";
            txtValFrom.Text = "0";
            txtValTo.Text = "0";
            txtMapPoints.Text = "0";
            txtActVal.Text = "0";
            chk_Allow_Ser.Checked = false;
            //opt_mcat.Checked = true;
            txtItem.Text = "";
            txtItemDesc.Text = "";

        }

        private void clear_add_inc()
        {
            chk_IsAlterInce.Checked = false;
            chk_AlterInce.Checked = false;
            chk_IsAlterInceDet.Checked = false;
            optItem.Checked = true;
            chk_AlterInceDet.Checked = false;
            txt_Item.Text = "";
            txtItemDesc.Text = "";
            txtQty.Text = "0";
            txtValue.Text = "0";
            txtNaration.Text = "0";
        }

        private void clear_add_item_det()
        {

           // _incSchStkTpList = new List<IncentiveSchStkTp>();
            _incSchDetList = new List<IncentiveSchDet>();
           // _incSchPersonList = new List<IncentiveSchPersn>();
            _incSchSerialList = new List<IncentiveSchSerial>();
            _incSchIncList = new List<IncentiveSchInc>();
            _incSchIncDet = new List<IncentiveSchIncDet>();

            grvItemsDet.DataSource = null;
            grvInc.DataSource = null;
            grvSalesType.DataSource = null;
           // grvStkTp.DataSource = null;
          //  grv_Party.DataSource = null;
            grvParty.DataSource = null;

        }

        private void txtCircularNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

            }
        }

        private void bindSchemes()
        {
            // cmbScheme.Items.Clear();
            cmbScheme.DataSource = null;
            cmbScheme.DataSource = CHNLSVC.General.GetIncentiveSchemes(txtCircularNo.Text);
            cmbScheme.DisplayMember = "INC_REF";
            cmbScheme.ValueMember = "INC_REF";
            cmbScheme.SelectedIndex = -1;
        }

        private void btn_Add_Inc_Click(object sender, EventArgs e)
        {
            if (txtQty.Text == "0" && txtValue.Text == "0")
            {
                MessageBox.Show("Select valid Qty/Value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Int32 _DLine = 1;
            foreach (IncentiveSchInc _detInc in _incSchIncList)
            {
                IncentiveSchIncDet _invSchIncDet = new IncentiveSchIncDet();
                _invSchIncDet.Incid_ref = _refNo;
                _invSchIncDet.Incid_circ = txtCircularNo.Text;
                _invSchIncDet.Incid_line = _DLine;
                _invSchIncDet.Incid_inc_line = _detInc.Inci_line;
                _invSchIncDet.Incid_alt = chk_AlterInceDet.Checked;
                _invSchIncDet.Incid_is_alt_itm = chk_IsAlterInceDet.Checked;
                _invSchIncDet.Incid_itm = txt_Item.Text;
                _invSchIncDet.Incid_qty = Convert.ToInt32(txtQty.Text);
                _invSchIncDet.Incid_amount = Convert.ToDecimal(txtValue.Text);
                //_invSchIncDet.Incid_desc =
                //_invSchIncDet.Incid_dt_line = 

                _incSchIncDet.Add(_invSchIncDet);

                _DLine = _DLine + 1;
            }

            grvInc.AutoGenerateColumns = false;
            grvInc.DataSource = new List<IncentiveSchIncDet>();
            grvInc.DataSource = _incSchIncDet;

            clear_add_inc();
            if (MessageBox.Show("Do you wish to add Alternate details for this Incentive Type?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                pnlCreateNew.Enabled = true;
            }
            pnlIncItem.Enabled = false;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbIncType.Text))
            {
                MessageBox.Show("Select the incentive type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Int32 _HLine = _incSchIncList.Count + 1;

            IncentiveSchInc _invSchInc = new IncentiveSchInc();
            _invSchInc.Inci_ref = _refNo;
            _invSchInc.Inci_circ = txtCircularNo.Text;
            _invSchInc.Inci_is_alt_inc = chk_IsAlterInce.Checked;
            _invSchInc.Inci_alt = Convert.ToInt32(chk_AlterInce.Checked);
            _invSchInc.Inci_inc_tp = cmbIncType.SelectedValue.ToString();
            _invSchInc.Inci_line = _HLine;
            //_invSchInc.Inci_dt_line=

            _incSchIncList.Add(_invSchInc);

            pnlCreateNew.Enabled = false;
            pnlIncItem.Enabled = true;

        }

        private void opt_mcat_CheckedChanged(object sender, EventArgs e)
        {
            txtItem.Text = "";
        }

        private void opt_cat_CheckedChanged(object sender, EventArgs e)
        {
            txtItem.Text = "";
        }

        private void opt_brand_CheckedChanged(object sender, EventArgs e)
        {
            txtItem.Text = "";
        }

        private void opt_model_CheckedChanged(object sender, EventArgs e)
        {
            txtItem.Text = "";
        }

        private void opt_itm_CheckedChanged(object sender, EventArgs e)
        {
            txtItem.Text = "";
        }

        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F5)
            {
                if (opt_mcat.Checked == true)
                {
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_ItemSearchData("CAT_Main", "%", null, null, null, null, "%");
                    grvMainCat.DataSource = _result;

                    pnlMCat.Visible = true;
                    pnlMCat.Left = 331;
                    pnlMCat.Top = 5;
                }
                if (opt_cat.Checked == true)
                {
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_ItemSearchData("CAT_Sub1", "%", "%", null, null, null, "%");
                    grvCat.DataSource = _result;

                    pnlcat.Visible = true;
                    pnlcat.Left = 252;
                    pnlcat.Top = 5;
                }
                if (opt_brand.Checked == true)
                {
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_ItemSearchData("Brand", "%", "%", "%", null, null, "%");
                    grvBrand.DataSource = _result;

                    pnlBrand.Visible = true;
                    pnlBrand.Left = 198;
                    pnlBrand.Top = 5;
                }
                if (opt_model.Checked == true)
                {
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_ItemSearchData("Model", "%", "%", "%", "%", null, "%");
                    grvModel.DataSource = _result;

                    pnlModel.Visible = true;
                    pnlModel.Left = 122;
                    pnlModel.Top = 5;
                }
                if (opt_itm.Checked == true)
                {
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_ItemSearchData("Item", "%", "%", "%", "%", "%", "%");
                    grvItm.DataSource = _result;

                    pnlItm.Visible = true;
                    pnlItm.Left = 32;
                    pnlItm.Top = 5;
                }

            }
        }

        private void chkAll_MainCat_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAll_MainCat.Checked == true)
                {
                    foreach (DataGridViewRow row in grvMainCat.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = true;
                    }
                    grvMainCat.EndEdit();

                }
                else
                {
                    foreach (DataGridViewRow row in grvMainCat.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = false;
                    }
                    grvMainCat.EndEdit();

                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnClose_MCat_Click(object sender, EventArgs e)
        {
            pnlMCat.Visible = false;
        }

        private void btn_Srch_MCat_Click(object sender, EventArgs e)
        {
            string _mcat = "";
            string _mcatdesc = "";

            _mcat = (txtmcat.Text != null) ? (txtmcat.Text.ToUpper() + "%") : "%";
            _mcatdesc = (txtmcatdesc.Text != null) ? (txtmcatdesc.Text.ToUpper() + "%") : "%";

            DataTable _result = CHNLSVC.CommonSearch.GetCat_ItemSearchData("CAT_Main", _mcat, null, null, null, null, _mcatdesc);
            grvMainCat.DataSource = _result;
        }

        private List<string> get_selected_Main_Cat()
        {
            grvMainCat.EndEdit();
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvMainCat.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells[1].Value.ToString());
                }
            }
            return list;
        }

        private void btnAdd_MCat_Click(object sender, EventArgs e)
        {
            List<string> list_mcat = get_selected_Main_Cat();
            pnlMCat.Visible = false;

            for (int i = 0; i < list_mcat.Count; i++)
            {

                IncentiveSchDet _invSchDet = new IncentiveSchDet();
                _invSchDet.Incd_is_alt_itm = chk_IsAlterItem.Checked;
                _invSchDet.Incd_alt = chk_AlterItem.Checked;
                _invSchDet.Incd_is_comb_itm = chkIsCombine.Checked;
                _invSchDet.Incd_circ = txtCircularNo.Text;

                _invSchDet.Incd_sale_tp = txtSalesTp.Text.ToString();
                _invSchDet.Incd_allow_ser = chk_Allow_Ser.Checked;
                _invSchDet.Incd_is_val_range = chkRange.Checked;
                _invSchDet.Incd_all_stk_tp = chkAllStkTp.Checked;

                if (opt_mcat.Checked == true) _invSchDet.Incd_main_cat = list_mcat[i];
                if (opt_cat.Checked == true) _invSchDet.Incd_cat = list_mcat[i];
                if (opt_brand.Checked == true) _invSchDet.Incd_brand = list_mcat[i];
                if (opt_model.Checked == true) _invSchDet.Incd_model = list_mcat[i];
                if (opt_itm.Checked == true) _invSchDet.Incd_itm = list_mcat[i];

                _invSchDet.Incd_val_from = Convert.ToInt32(txtValFrom.Text);
                _invSchDet.Incd_val_to = Convert.ToInt32(txtValTo.Text);
                _invSchDet.Incd_ref = _refNo;
                //_invSchDet.Incd_comen_dt =
                //_invSchDet.Incd_comen_line =
                //_invSchDet.Incd_is_oth_qty =
                //_invSchDet.Incd_actual_qty =
                //_invSchDet.Incd_oth_qty = 

                _incSchDetList.Add(_invSchDet);
            }
            grvItemsDet.AutoGenerateColumns = false;
            grvItemsDet.DataSource = new List<IncentiveSchDet>();
            grvItemsDet.DataSource = _incSchDetList;

            clear_add_item();
        }

        private void chkAll_Cat_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAll_Cat.Checked == true)
                {
                    foreach (DataGridViewRow row in grvCat.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = true;
                    }
                    grvCat.EndEdit();

                }
                else
                {
                    foreach (DataGridViewRow row in grvCat.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = false;
                    }
                    grvCat.EndEdit();

                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btn_Srch_Cat_Click(object sender, EventArgs e)
        {
            string _cat = "";
            string _catdesc = "";
            string _mcat = "";

            _cat = (txtcat.Text != null) ? (txtcat.Text.ToUpper() + "%") : "%";
            _catdesc = (txtcatdesc.Text != null) ? (txtcatdesc.Text.ToUpper() + "%") : "%";
            _mcat = (txtmcat_cat.Text != null) ? (txtmcat_cat.Text.ToUpper() + "%") : "%";

            DataTable _result = CHNLSVC.CommonSearch.GetCat_ItemSearchData("CAT_Sub1", _mcat, _cat, null, null, null, _catdesc);
            grvCat.DataSource = _result;
        }

        private List<string> get_selected_Cat()
        {
            grvCat.EndEdit();
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvCat.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells[1].Value.ToString());
                }
            }
            return list;
        }

        private void btnClose_Cat_Click(object sender, EventArgs e)
        {
            pnlcat.Visible = false;
        }

        private void btnAdd_Cat_Click(object sender, EventArgs e)
        {
            List<string> list_cat = get_selected_Cat();
            pnlcat.Visible = false;

            for (int i = 0; i < list_cat.Count; i++)
            {

                IncentiveSchDet _invSchDet = new IncentiveSchDet();
                _invSchDet.Incd_is_alt_itm = chk_IsAlterItem.Checked;
                _invSchDet.Incd_alt = chk_AlterItem.Checked;
                _invSchDet.Incd_is_comb_itm = chkIsCombine.Checked;
                _invSchDet.Incd_circ = txtCircularNo.Text;

                _invSchDet.Incd_sale_tp = txtSalesTp.Text.ToString();
                _invSchDet.Incd_allow_ser = chk_Allow_Ser.Checked;
                _invSchDet.Incd_is_val_range = chkRange.Checked;
                _invSchDet.Incd_all_stk_tp = chkAllStkTp.Checked;

                if (opt_mcat.Checked == true) _invSchDet.Incd_main_cat = list_cat[i];
                if (opt_cat.Checked == true) _invSchDet.Incd_cat = list_cat[i];
                if (opt_brand.Checked == true) _invSchDet.Incd_brand = list_cat[i];
                if (opt_model.Checked == true) _invSchDet.Incd_model = list_cat[i];
                if (opt_itm.Checked == true) _invSchDet.Incd_itm = list_cat[i];

                _invSchDet.Incd_val_from = Convert.ToInt32(txtValFrom.Text);
                _invSchDet.Incd_val_to = Convert.ToInt32(txtValTo.Text);
                _invSchDet.Incd_ref = _refNo;
                //_invSchDet.Incd_comen_dt =
                //_invSchDet.Incd_comen_line =
                //_invSchDet.Incd_is_oth_qty =
                //_invSchDet.Incd_actual_qty =
                //_invSchDet.Incd_oth_qty = 

                _incSchDetList.Add(_invSchDet);
            }
            grvItemsDet.AutoGenerateColumns = false;
            grvItemsDet.DataSource = new List<IncentiveSchDet>();
            grvItemsDet.DataSource = _incSchDetList;

            clear_add_item();
        }

        private void btnAdd_Brand_Click(object sender, EventArgs e)
        {
            List<string> list_Brand = get_selected_Brand();
            pnlBrand.Visible = false;

            for (int i = 0; i < list_Brand.Count; i++)
            {

                IncentiveSchDet _invSchDet = new IncentiveSchDet();
                _invSchDet.Incd_is_alt_itm = chk_IsAlterItem.Checked;
                _invSchDet.Incd_alt = chk_AlterItem.Checked;
                _invSchDet.Incd_is_comb_itm = chkIsCombine.Checked;
                _invSchDet.Incd_circ = txtCircularNo.Text;

                _invSchDet.Incd_sale_tp = txtSalesTp.Text.ToString();
                _invSchDet.Incd_allow_ser = chk_Allow_Ser.Checked;
                _invSchDet.Incd_is_val_range = chkRange.Checked;
                _invSchDet.Incd_all_stk_tp = chkAllStkTp.Checked;

                if (opt_mcat.Checked == true) _invSchDet.Incd_main_cat = list_Brand[i];
                if (opt_cat.Checked == true) _invSchDet.Incd_cat = list_Brand[i];
                if (opt_brand.Checked == true) _invSchDet.Incd_brand = list_Brand[i];
                if (opt_model.Checked == true) _invSchDet.Incd_model = list_Brand[i];
                if (opt_itm.Checked == true) _invSchDet.Incd_itm = list_Brand[i];

                _invSchDet.Incd_val_from = Convert.ToInt32(txtValFrom.Text);
                _invSchDet.Incd_val_to = Convert.ToInt32(txtValTo.Text);
                _invSchDet.Incd_ref = _refNo;
                //_invSchDet.Incd_comen_dt =
                //_invSchDet.Incd_comen_line =
                //_invSchDet.Incd_is_oth_qty =
                //_invSchDet.Incd_actual_qty =
                //_invSchDet.Incd_oth_qty = 

                _incSchDetList.Add(_invSchDet);
            }
            grvItemsDet.AutoGenerateColumns = false;
            grvItemsDet.DataSource = new List<IncentiveSchDet>();
            grvItemsDet.DataSource = _incSchDetList;

            clear_add_item();
        }

        private List<string> get_selected_Brand()
        {
            grvBrand.EndEdit();
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvBrand.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells[1].Value.ToString());
                }
            }
            return list;
        }

        private void btnClose_Brand_Click(object sender, EventArgs e)
        {
            pnlBrand.Visible = false;
        }

        private void btn_Srch_Brand_Click(object sender, EventArgs e)
        {
            string _brand = "";
            string _branddesc = "";
            string _mcat = "";
            string _cat = "";

            _brand = (txtBrnd.Text != null) ? (txtBrnd.Text.ToUpper() + "%") : "%";
            _branddesc = (txtBrndDesc.Text != null) ? (txtBrndDesc.Text.ToUpper() + "%") : "%";
            _mcat = (txtMCat_Brnd.Text != null) ? (txtMCat_Brnd.Text.ToUpper() + "%") : "%";
            _cat = (txtCat_Brnd.Text != null) ? (txtCat_Brnd.Text.ToUpper() + "%") : "%";

            DataTable _result = CHNLSVC.CommonSearch.GetCat_ItemSearchData("Brand", _mcat, _cat, _brand, null, null, _branddesc);
            grvBrand.DataSource = _result;
        }

        private void chkAll_Brand_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAll_Brand.Checked == true)
                {
                    foreach (DataGridViewRow row in grvBrand.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = true;
                    }
                    grvBrand.EndEdit();

                }
                else
                {
                    foreach (DataGridViewRow row in grvBrand.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = false;
                    }
                    grvBrand.EndEdit();

                }
            }
            catch (Exception ex)
            {

            }
        }

        private List<string> get_selected_Model()
        {
            grvModel.EndEdit();
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvModel.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells[1].Value.ToString());
                }
            }
            return list;
        }

        private void btnClose_Model_Click(object sender, EventArgs e)
        {
            pnlModel.Visible = false;
        }

        private void btn_Srch_Model_Click(object sender, EventArgs e)
        {
            string _model = "";
            string _brand = "";
            string _modeldesc = "";
            string _mcat = "";
            string _cat = "";

            _model = (txtModel.Text != null) ? (txtModel.Text.ToUpper() + "%") : "%";
            _modeldesc = (txtModelDesc.Text != null) ? (txtModelDesc.Text.ToUpper() + "%") : "%";

            _mcat = (txtMCat_Model.Text != null) ? (txtMCat_Model.Text.ToUpper() + "%") : "%";
            _cat = (txtCat_Model.Text != null) ? (txtCat_Model.Text.ToUpper() + "%") : "%";
            _brand = (txtBrand_Model.Text != null) ? (txtBrand_Model.Text.ToUpper() + "%") : "%";

            DataTable _result = CHNLSVC.CommonSearch.GetCat_ItemSearchData("Model", _mcat, _cat, _brand, _model, null, _modeldesc);
            grvModel.DataSource = _result;
        }

        private void btnAdd_Model_Click(object sender, EventArgs e)
        {
            List<string> list_Model = get_selected_Model();
            pnlModel.Visible = false;

            for (int i = 0; i < list_Model.Count; i++)
            {

                IncentiveSchDet _invSchDet = new IncentiveSchDet();
                _invSchDet.Incd_is_alt_itm = chk_IsAlterItem.Checked;
                _invSchDet.Incd_alt = chk_AlterItem.Checked;
                _invSchDet.Incd_is_comb_itm = chkIsCombine.Checked;
                _invSchDet.Incd_circ = txtCircularNo.Text;

                _invSchDet.Incd_sale_tp = txtSalesTp.Text.ToString();
                _invSchDet.Incd_allow_ser = chk_Allow_Ser.Checked;
                _invSchDet.Incd_is_val_range = chkRange.Checked;
                _invSchDet.Incd_all_stk_tp = chkAllStkTp.Checked;

                if (opt_mcat.Checked == true) _invSchDet.Incd_main_cat = list_Model[i];
                if (opt_cat.Checked == true) _invSchDet.Incd_cat = list_Model[i];
                if (opt_brand.Checked == true) _invSchDet.Incd_brand = list_Model[i];
                if (opt_model.Checked == true) _invSchDet.Incd_model = list_Model[i];
                if (opt_itm.Checked == true) _invSchDet.Incd_itm = list_Model[i];

                _invSchDet.Incd_val_from = Convert.ToInt32(txtValFrom.Text);
                _invSchDet.Incd_val_to = Convert.ToInt32(txtValTo.Text);
                _invSchDet.Incd_ref = _refNo;
                //_invSchDet.Incd_comen_dt =
                //_invSchDet.Incd_comen_line =
                //_invSchDet.Incd_is_oth_qty =
                //_invSchDet.Incd_actual_qty =
                //_invSchDet.Incd_oth_qty = 

                _incSchDetList.Add(_invSchDet);
            }
            grvItemsDet.AutoGenerateColumns = false;
            grvItemsDet.DataSource = new List<IncentiveSchDet>();
            grvItemsDet.DataSource = _incSchDetList;

            clear_add_item();
        }

        private void chkAll_Model_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAll_Model.Checked == true)
                {
                    foreach (DataGridViewRow row in grvModel.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = true;
                    }
                    grvModel.EndEdit();

                }
                else
                {
                    foreach (DataGridViewRow row in grvModel.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = false;
                    }
                    grvModel.EndEdit();

                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnClose_Itm_Click(object sender, EventArgs e)
        {
            pnlItm.Visible = false;
        }

        private List<string> get_selected_Items()
        {
            grvItm.EndEdit();
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvItm.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells[1].Value.ToString());
                }
            }
            return list;
        }

        private void btn_Srch_Item_Click(object sender, EventArgs e)
        {
            string _item = "";
            string _model = "";
            string _brand = "";
            string _itemdesc = "";
            string _mcat = "";
            string _cat = "";

            _item = (txtItm.Text != null) ? (txtItm.Text.ToUpper() + "%") : "%";
            _itemdesc = (txtItmDesc.Text != null) ? (txtItmDesc.Text.ToUpper() + "%") : "%";

            _mcat = (txtMCat_Itm.Text != null) ? (txtMCat_Itm.Text.ToUpper() + "%") : "%";
            _cat = (txtCat_Itm.Text != null) ? (txtCat_Itm.Text.ToUpper() + "%") : "%";
            _brand = (txtBrand_Itm.Text != null) ? (txtBrand_Itm.Text.ToUpper() + "%") : "%";
            _model = (txtModel_Itm.Text != null) ? (txtModel_Itm.Text.ToUpper() + "%") : "%";


            DataTable _result = CHNLSVC.CommonSearch.GetCat_ItemSearchData("Item", _mcat, _cat, _brand, _model, _item, _itemdesc);
            grvItm.DataSource = _result;
        }

        private void btnAdd_Itm_Click(object sender, EventArgs e)
        {
            if (txtValFrom.Text == "0")
            {
                MessageBox.Show("Please select the qty/value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<string> list_Item = get_selected_Items();
            pnlItm.Visible = false;

            for (int i = 0; i < list_Item.Count; i++)
            {

                IncentiveSchDet _invSchDet = new IncentiveSchDet();
                _invSchDet.Incd_is_alt_itm = chk_IsAlterItem.Checked;
                _invSchDet.Incd_alt = chk_AlterItem.Checked;
                _invSchDet.Incd_is_comb_itm = chkIsCombine.Checked;
                _invSchDet.Incd_circ = txtCircularNo.Text;

                _invSchDet.Incd_sale_tp = txtSalesTp.Text.ToString();
                _invSchDet.Incd_allow_ser = chk_Allow_Ser.Checked;
                _invSchDet.Incd_is_val_range = chkRange.Checked;
                _invSchDet.Incd_all_stk_tp = chkAllStkTp.Checked;

                if (opt_mcat.Checked == true) _invSchDet.Incd_main_cat = list_Item[i];
                if (opt_cat.Checked == true) _invSchDet.Incd_cat = list_Item[i];
                if (opt_brand.Checked == true) _invSchDet.Incd_brand = list_Item[i];
                if (opt_model.Checked == true) _invSchDet.Incd_model = list_Item[i];
                if (opt_itm.Checked == true) _invSchDet.Incd_itm = list_Item[i];

                _invSchDet.Incd_val_from = Convert.ToInt32(txtValFrom.Text);
                _invSchDet.Incd_val_to = Convert.ToInt32(txtValTo.Text);
                _invSchDet.Incd_ref = _refNo;
                //_invSchDet.Incd_comen_dt =
                //_invSchDet.Incd_comen_line =
                //_invSchDet.Incd_is_oth_qty =
                //_invSchDet.Incd_actual_qty =
                //_invSchDet.Incd_oth_qty = 

                _incSchDetList.Add(_invSchDet);
            }
            grvItemsDet.AutoGenerateColumns = false;
            grvItemsDet.DataSource = new List<IncentiveSchDet>();
            grvItemsDet.DataSource = _incSchDetList;

            clear_add_item();
        }

        private void chkAll_Itm_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAll_Itm.Checked == true)
                {
                    foreach (DataGridViewRow row in grvItm.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = true;
                    }
                    grvItm.EndEdit();

                }
                else
                {
                    foreach (DataGridViewRow row in grvItm.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = false;
                    }
                    grvItm.EndEdit();

                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

            BaseCls.GlbReportHeading = "INCENTIVE DEFINITIONS";

            BaseCls.GlbReportDocType = txtCircularNo.Text;
            if (!string.IsNullOrEmpty(cmbScheme.Text))
            {
                BaseCls.GlbReportDocSubType = cmbScheme.SelectedValue.ToString();
            }
            Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
            BaseCls.GlbReportName = "Incentive_Def.rpt";
            _view.Show();
            _view = null;
        }

        private void btn_Srch_Sales_Tp_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.IncSaleTp);
            DataTable _result = CHNLSVC.CommonSearch.GetIncSchSaleTypeSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtSalesTp;
            _CommonSearch.ShowDialog();
            txtSalesTp.Focus();
        }

        private void grvItemsDet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _incSchDetList.RemoveAt(e.RowIndex);
                    BindingSource _source = new BindingSource();
                    _source.DataSource = _incSchDetList;
                    grvItemsDet.DataSource = _source;
                }
            }
        }

        private void cmbScheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            _incSchDetList = new List<IncentiveSchDet>();
            if (cmbScheme.SelectedValue != null)
            {
                _incSchDetList = CHNLSVC.General.GetIncSchDet(cmbScheme.SelectedValue.ToString());

                grvItems.AutoGenerateColumns = false;
                grvItems.DataSource = new List<IncentiveSchDet>();
                grvItems.DataSource = _incSchDetList;
            }
            pnlInc.Enabled = false;
            pnlItem.Enabled = false;
        }

        private void DropDownListPartyTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            _incSchPCList = new List<IncentiveSchPC>();
            //_pcLine = 1;

            grvParty.AutoGenerateColumns = false;
            grvParty.DataSource = new List<IncentiveSchPC>();
            grvParty.DataSource = _incSchPCList;

            if (DropDownListPartyTypes.SelectedValue == "PC")
            {
                btn_src_pc.Enabled = true;
            }
            else
            {
                btn_src_pc.Enabled = false;
            }
        }

        private void chlAllPB_CheckedChanged(object sender, EventArgs e)
        {
            if (chlAllPB.Checked == true)
            {
                btnAddPB.Enabled = false;
            }
            else
            {
                btnAddPB.Enabled = true;
            }
        }

        private void chkAllMode_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllMode.Checked == true)
            {
                btnAddMode.Enabled = false;
            }
            else
            {
                btnAddMode.Enabled = true;
            }
        }

        private void btn_Srch_PB_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
            DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtPB;
            _CommonSearch.ShowDialog();
            txtPB.Focus();
        }

        private void btn_Srch_Level_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
            DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtLevel;
            _CommonSearch.ShowDialog();
            txtLevel.Focus();
        }

        private void btnAddPB_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPB.Text))
            {
                MessageBox.Show("Select the price book", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtLevel.Text))
            {
                MessageBox.Show("Select the price level", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int rowIndex = -1;
            foreach (DataGridViewRow row in grvPBLevel.Rows)
            {
                if (row.Cells[1].Value.ToString().Equals(txtPB.Text))
                {
                    if (row.Cells[2].Value.ToString().Equals(txtLevel.Text))
                    {
                        rowIndex = row.Index;
                        return;
                    }
                }
            }

            IncentiveSchPB _invSchPB = new IncentiveSchPB();
            _invSchPB.Inpbl_circ = txtCircularNo.Text;
            _invSchPB.Inpbl_pb = txtPB.Text;
            _invSchPB.Inpbl_pb_lvl = txtLevel.Text;

            _incSchPBList.Add(_invSchPB);

            grvPBLevel.AutoGenerateColumns = false;
            grvPBLevel.DataSource = new List<IncentiveSchPB>();
            grvPBLevel.DataSource = _incSchPBList;
            txtPB.Text = "";
            txtLevel.Text = "";
        }

        private void btnAddMode_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxPayModes.Text))
            {
                MessageBox.Show("Select the pay mode", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            int rowIndex = -1;
            foreach (DataGridViewRow row in grvPaymode.Rows)
            {
                if (row.Cells[1].Value.ToString().Equals(comboBoxPayModes.SelectedValue))
                {
                    string _x = Convert.ToInt32(chkCCPromo.Checked).ToString();
                    if (row.Cells[2].Value.ToString().Equals(_x))
                    {
                        rowIndex = row.Index;
                        return;
                    }

                }
            }

            IncentiveSchMode _invSchMode = new IncentiveSchMode();
            _invSchMode.Inpym_circ = txtCircularNo.Text;
            _invSchMode.Inpym_mode = comboBoxPayModes.SelectedValue.ToString();
            _invSchMode.Inpym_is_promo = Convert.ToInt32(chkCCPromo.Checked);

            _incSchModeList.Add(_invSchMode);

            grvPaymode.AutoGenerateColumns = false;
            grvPaymode.DataSource = new List<IncentiveSchPB>();
            grvPaymode.DataSource = _incSchModeList;
            comboBoxPayModes.Text = "";
        }

        private void grvPaymode_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _incSchModeList.RemoveAt(e.RowIndex);
                    BindingSource _source = new BindingSource();
                    _source.DataSource = _incSchModeList;
                    grvPaymode.DataSource = _source;
                }
            }
        }

        private void grvPBLevel_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _incSchPBList.RemoveAt(e.RowIndex);
                    BindingSource _source = new BindingSource();
                    _source.DataSource = _incSchPBList;
                    grvPBLevel.DataSource = _source;
                }
            }
        }

        private void chkIsVal_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsVal.Checked == true)
            {
                lblActual.Text = "Val Actual";
                lblMapPoint.Text = "Val Map/Points";
                lblQtyFrom.Text = "Val From";
                lblQtyTo.Text = "Val To";
            }
            else
            {
                lblActual.Text = "Qty Actual";
                lblMapPoint.Text = "Qty Map/Points";
                lblQtyFrom.Text = "Qty From";
                lblQtyTo.Text = "Qty To";
            }
        }

        private void chkRange_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRange.Checked == true)
            {
                txtValTo.Enabled = true;
            }
            else
            {
                txtValTo.Enabled = false;
                txtValTo.Text = txtValFrom.Text;
            }
        }

        private void chk_IsMap_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_IsMap.Checked == true)
            {
                txtMapPoints.Enabled = true;
                txtActVal.Enabled = true;
            }
            else
            {
                txtMapPoints.Enabled = false;
                txtActVal.Enabled = false;
            }
        }

        private void txtValFrom_TextChanged(object sender, EventArgs e)
        {
            txtValTo.Text = txtValFrom.Text;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCircularNo.Text))
            {
                MessageBox.Show("Select circular number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Are you Sure that you want to confirm " + txtCircularNo.Text, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Int32 X = CHNLSVC.Financial.confirm_inc_sch(txtCircularNo.Text, BaseCls.GlbUserID);
                MessageBox.Show("Successfully confirmed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void optVal_CheckedChanged(object sender, EventArgs e)
        {
            txtValue.Enabled = true;
            txtQty.Enabled = false;
        }

        private void optItem_CheckedChanged(object sender, EventArgs e)
        {
            txtValue.Enabled = false;
            txtQty.Enabled = true;
        }

        private void opt_itm_CheckedChanged_1(object sender, EventArgs e)
        {
            txtItem.Enabled = true;
        }

        private void txtItemStatus_Leave(object sender, EventArgs e)
        {

        }

        private void btn_loc_close_Click(object sender, EventArgs e)
        {
            pnlLoc.Visible = false;
        }

        private void txtChanel_DoubleClick(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtChanel;
            _CommonSearch.ShowDialog();
            txtChanel.Select();
        }

        private void txtSChanel_DoubleClick(object sender, EventArgs e)
        {
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

        private void txtArea_DoubleClick(object sender, EventArgs e)
        {
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

        private void txtRegion_DoubleClick(object sender, EventArgs e)
        {
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

        private void txtZone_DoubleClick(object sender, EventArgs e)
        {
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

        private void txtPC_DoubleClick(object sender, EventArgs e)
        {
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

        private void txtChanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtChanel_DoubleClick(null, null);
            }
        }

        private void txtSChanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtSChanel_DoubleClick(null, null);
            }
        }

        private void txtArea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtArea_DoubleClick(null, null);
            }
        }

        private void txtRegion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtRegion_DoubleClick(null, null);
            }
        }

        private void txtZone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtZone_DoubleClick(null, null);
            }
        }

        private void txtPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtPC_DoubleClick(null, null);
            }
        }

        private void btn_add_loc_Click(object sender, EventArgs e)
        {
            _incSchPCList = new List<IncentiveSchPC>();

            foreach (ListViewItem Item in lstPC.Items)
            {
                string pc = Item.Text;

                if (Item.Checked == true)
                {
                    IncentiveSchPC _invSchPC = new IncentiveSchPC();
                    _invSchPC.Incl_pc = pc;
                    _invSchPC.Incl_circ = txtCircularNo.Text;
                    _invSchPC.Incl_ref = _refNo;
                    _invSchPC.Incl_line = 1;
                    _incSchPCList.Add(_invSchPC);

                    //_pcLine = _pcLine + 1;

                }
            }
            grvParty.AutoGenerateColumns = false;
            grvParty.DataSource = new List<IncentiveSchPC>();
            grvParty.DataSource = _incSchPCList;

            pnlLoc.Visible = false;
        }

        private void btn_src_pc_Click(object sender, EventArgs e)
        {
            pnlLoc.Visible = true;
            pnlLoc.Left = 1;
            pnlLoc.Top = 5;
        }

        private void btnLocAdd_Click(object sender, EventArgs e)
        {
            lstPC.Clear();
            DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(txtComp.Text, txtChanel.Text, txtSChanel.Text, txtArea.Text, txtRegion.Text, txtZone.Text, txtPC.Text);
            foreach (DataRow drow in dt.Rows)
            {
                lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
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

        private void btnClr_Click(object sender, EventArgs e)
        {
            lstPC.Clear();
        }

        private void grvInc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _incSchDetList.RemoveAt(e.RowIndex);
                    BindingSource _source = new BindingSource();
                    _source.DataSource = _incSchDetList;
                    grvInc.DataSource = _source;
                }
            }
        }

        private void txtCircularNo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCircularNo.Text))
            {
                bindSchemes();
                LoadIncentiveDet();

            }
        }

        private void grvItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _incSchDetList_main.RemoveAt(e.RowIndex);
                    BindingSource _source = new BindingSource();
                    _source.DataSource = _incSchDetList_main;
                    grvItems.DataSource = _source;
                }
            }
        }
    }

}
