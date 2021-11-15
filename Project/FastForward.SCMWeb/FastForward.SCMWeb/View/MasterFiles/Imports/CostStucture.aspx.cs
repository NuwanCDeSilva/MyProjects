using FastForward.SCMWeb.Services;
using FF.BusinessObjects.Financial;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.MasterFiles.Imports
{
    public partial class CostStucture : Base
    {
        ImportCostCatergoryMaster _ImportCostCatergoryMaster = new ImportCostCatergoryMaster();
        ImportCostSegmentMaster _ImportCostSegmentMaster = new ImportCostSegmentMaster();
        ImportCostType _ImportCostType = new ImportCostType();
        ImpoertCostElement _ImpoertCostElement = new ImpoertCostElement();
        DataTable _result;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClearPage();
                BuildTree();
                LoadCostCatGrid();
            }
            else
            {
                
            }
        }

        #region Privat Method
        private void ClearPage()
        {
            ClearErrorMsg();
            ClearTextBox();
        }
        protected void lbtnWarningCostCategoryClose_Click(object sender, EventArgs e)
        {
            ClearErrorMsg();

        }

        private void ClearTextBox()
        {
            txtCostCategoryCode.Text = string.Empty;
           
            txtCostCategoryCode.ReadOnly = false;
            txtCaterDes.Text = string.Empty;
            txtCSegmentCode.Text = string.Empty;
           
            txtCSegmentCode.ReadOnly = false;
            txtSegDescription.Text = string.Empty;
            txtCTCategoryC.Text = string.Empty;
            txtCTSegC.Text = string.Empty;
           // txtCTDes.Text = string.Empty;
            chkCatergoryActive.Checked = true;
            
            Session["seg"] = "";
            Session["SE2CODE"] = "";
        }
        private void ClearErrorMsg()
        {
            WarningCostCategory.Visible = false;
            SuccessCostCategory.Visible = false;
            WarnningCostSegment.Visible = false;
            SuccessCostSegment.Visible = false;
            WarnningCostType.Visible = false;
            SuccessCostType.Visible = false;
        }
        private void ErrorMsgCostCater(string _Msg)
        {
            WarningCostCategory.Visible = true;
            lblWarningCostCategory.Text = _Msg;
        }
        private void SuccessMsgCostCater(string _Msg)
        {
            SuccessCostCategory.Visible = true;
            lblSuccessCostCategory.Text = _Msg;
        }
        private void ErrorMsgCosSeg(string _Msg)
        {
            WarnningCostSegment.Visible = true;
            lblWarnningCostSegment.Text = _Msg;
        }
        private void SuccessMsgCostSeg(string _Msg)
        {
            SuccessCostSegment.Visible = true;
            lblSuccessCostSegment.Text = _Msg;
        }
        private void ErrorMsgCosType(string _Msg)
        {
            WarnningCostType.Visible = true;
            lblWarnningCostType.Text = _Msg;
        }
        private void SuccessMsgCostType(string _Msg)
        {
            SuccessCostType.Visible = true;
            lblSuccessCostType.Text = _Msg;
        }
        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            this.ddlSearchbykey.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykey.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykey.SelectedIndex = 0;
        }

        private void LoadCostCatGrid()
        {
            _result = CHNLSVC.Financial.GetCostCatergoryMaster(null,null);
            grdCostcat.DataSource = _result;
            grdCostcat.DataBind();
        }
        private void BuildTree()
        {
            try
            {
                DataTable dt_tree = CHNLSVC.Financial.GetCostCatergoryMaster(null,null);
                //TreeNode TN = new TreeNode();
                //TN.Value = "m";
                //TN.Text = "Modules";
                //treeView1.Nodes.Add(TN);
                //ADD_CHILD(ref TN, TN.Value.ToString());
                //treeView1.CollapseAll();

                treeView1.Nodes.Clear();

                foreach (DataRow dr in dt_tree.Rows)
                {

                    TreeNode tnParent = new TreeNode();

                    tnParent.Text = dr["CODE"].ToString();

                    tnParent.Value = dr["CODE"].ToString();

                    tnParent.PopulateOnDemand = true;

                    tnParent.ToolTip = "Click to get Child";

                    tnParent.SelectAction = TreeNodeSelectAction.SelectExpand;
                    tnParent.ShowCheckBox = false;
                    //tnParent.Expand();

                    tnParent.Selected = true;

                    treeView1.Nodes.Add(tnParent);
                    FillChild(tnParent, tnParent.Value);
                   
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }
        public void FillChild(TreeNode parent, string ParentId)
        {
           
            parent.ChildNodes.Clear();
            Session["txtCTCategoryC"] = ParentId;
            DataTable dt_tree = CHNLSVC.Financial.GetCostType_CODE(ParentId, "");
            foreach (DataRow dr in dt_tree.Rows)
            {

                TreeNode child = new TreeNode();

                child.Text = dr["mcat_cd"].ToString().Trim();

                child.Value = dr["mcat_cd"].ToString().Trim();

                if (child.ChildNodes.Count == 0)
                {

                    child.PopulateOnDemand = true;

                }

                child.ToolTip = "Click to get Child";

                child.SelectAction = TreeNodeSelectAction.SelectExpand;

                child.CollapseAll();

                parent.ChildNodes.Add(child);
                SUBFillChild(child, child.Value);
            }

        }

        public void SUBFillChild(TreeNode parent, string ParentId)
        {

            parent.ChildNodes.Clear();

           
            Session["SECODE"] = ParentId;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.costtype);
            DataTable dt_tree = CHNLSVC.Financial.GetCostELEMaster_CODE(SearchParams);

            foreach (DataRow dr in dt_tree.Rows)
            {

                TreeNode child = new TreeNode();

                child.Text = dr["mcae_cd"].ToString().Trim();

                child.Value = dr["mcae_cd"].ToString().Trim();

                if (child.ChildNodes.Count == 0)
                {

                    child.PopulateOnDemand = true;

                }

                child.ToolTip = "Click to get Child";

                child.SelectAction = TreeNodeSelectAction.SelectExpand;

                child.CollapseAll();

                parent.ChildNodes.Add(child);

            }

        }
        #endregion

        #region Cost Catergory
        protected void btnAddNewCostCategory_Click(object sender, EventArgs e)
        {

            ClearErrorMsg();
            if (txtCostCategoryCode.Text == string.Empty)
            {
                ErrorMsgCostCater("Please enter Cost Catergory Code !");
                return;
            }
            if (txtCaterDes.Text == string.Empty)
            {
                ErrorMsgCostCater("Please enter Description!");
                return;
            }
            Int32 row_aff = 0;
            _ImportCostCatergoryMaster.Mca_cd = txtCostCategoryCode.Text.Trim();
            _ImportCostCatergoryMaster.Mca_desc = txtCaterDes.Text;
            _ImportCostCatergoryMaster.Mca_act = (chkCatergoryActive.Checked == true) ? true : false;
            _ImportCostCatergoryMaster.Mca_cre_by = Session["UserID"].ToString();
            _ImportCostCatergoryMaster.Mca_cre_dt = System.DateTime.Now;
            _ImportCostCatergoryMaster.Mca_session_id = Session["SessionID"].ToString();
            _ImportCostCatergoryMaster.Mca_mod_by = Session["UserID"].ToString();
            _ImportCostCatergoryMaster.Mca_mod_dt =System.DateTime.Now;
            if (txtSavelconformmessageValue.Value == "Yes")
            {
                row_aff = CHNLSVC.Financial.SaveCostCatergoryMaster(_ImportCostCatergoryMaster);
                if (row_aff > 0)
                {
                    SuccessMsgCostCater("Successfully Saved.. !");
                    ClearTextBox();
                    BuildTree();
                    LoadCostCatGrid();

                }
               
            }

        }

        protected void lbtnCaterCode_Click(object sender, EventArgs e)
        {
            ClearErrorMsg();
            _result = CHNLSVC.Financial.GetCostCatergoryMaster(null,null);
            grdResult.DataSource = _result;
            grdResult.DataBind();
            lblvalue.Text = "1";
            BindUCtrlDDLData(_result);
           // ddlSearchbykey.Items.FindByText("DESCRIPTION").Enabled = false;
            UserPopoup.Show();
        }
        //protected void lbtnWarningCostCategoryClose_Click(object sender, EventArgs e)
        //{
        //    ClearErrorMsg();
        //    ClearErrorMsg();
        //}

        protected void lblCostCategoryClear_Click(object sender, EventArgs e)
        {
            ClearTextBox();
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");
            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.costtype:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["txtCTCategoryC"] + seperator + Session["SE2CODE"].ToString());
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion
       

        #region ModalPopup
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            if (lblvalue.Text =="1")
            {
                _result = CHNLSVC.Financial.GetCostCatergoryMaster(ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());             
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "1";
                BindUCtrlDDLData(_result);
               // ddlSearchbykey.Items.FindByText("DESCRIPTION").Enabled = false;
                UserPopoup.Show();
            }
            else if (lblvalue.Text == "2")
            {
                _result = CHNLSVC.Financial.GetCostSegementMaster(ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;              
                grdResult.DataBind();
                lblvalue.Text = "2";
               // ddlSearchbykey.Items.FindByText("DESCRIPTION").Enabled = false;
                BindUCtrlDDLData(_result);
                UserPopoup.Show();
            }
            else if (lblvalue.Text == "3")
            {
                _result = CHNLSVC.Financial.GetCostCatergoryMaster(ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "3";
                BindUCtrlDDLData(_result);
               // ddlSearchbykey.Items.FindByText("DESCRIPTION").Enabled = false;
                UserPopoup.Show();
            }
            else if (lblvalue.Text == "4")
            {
                _result = CHNLSVC.Financial.GetCostSegementMaster(ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "4";
                //BindUCtrlDDLData(_result);
                UserPopoup.Show();
            }
            else if (lblvalue.Text == "5")
            {
                _result = CHNLSVC.Financial.GetCostSegementMaster(ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "5";
                Session["seg"] = "5";
                BindUCtrlDDLData(_result);
                UserPopoup.Show();
            }
        }
        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            string Des = grdResult.SelectedRow.Cells[1].Text;
            if (lblvalue.Text == "1")
            {
                lblvalue.Text = "";
                _result = CHNLSVC.Financial.GetCostCMasterBYID(Des);
                if ((_result != null) && (_result.Rows.Count > 0))
                {
                    txtCostCategoryCode.ReadOnly = true;
                    txtCostCategoryCode.Text = _result.Rows[0][0].ToString();
                    txtCaterDes.Text = _result.Rows[0][1].ToString();
                    bool active = Convert.ToBoolean(_result.Rows[0][2]);
                    if (active == true)
                    {
                        chkCatergoryActive.Checked =true;
                    }
                    else 
                    {
                        chkCatergoryActive.Checked = false;
                    }
                }
            }
            if (lblvalue.Text == "2")
            {
                lblvalue.Text = "";
                _result = CHNLSVC.Financial.GetCostSegmentMaster_CODE(Des);
                if ((_result != null) && (_result.Rows.Count > 0))
                {
                    txtCSegmentCode.ReadOnly = true;
                    txtCSegmentCode.Text = _result.Rows[0][0].ToString();
                    txtSegDescription.Text = _result.Rows[0][1].ToString();
                    bool active = Convert.ToBoolean(_result.Rows[0][2]);
                    if (active == true)
                    {
                        chkSegActive.Checked = true;
                    }
                    else
                    {
                        chkSegActive.Checked = false;
                    }
                }
            }
            if (lblvalue.Text == "3")
            {
                lblvalue.Text = "";
                txtCTCategoryC.Text = Des;
            }
            if (lblvalue.Text == "4")
            {
                lblvalue.Text = "";
                txtCTSegC.Text = Des;
                _result = CHNLSVC.Financial.GetCostSegmentMaster_CODE(Des);
                if (_result.Rows.Count > 0)
                {
                    txtCTSegC.Text = _result.Rows[0][0].ToString();
                    Session["SegDescription"]= _result.Rows[0][1].ToString();
                }
            }
            if (lblvalue.Text == "5")
            {
                lblvalue.Text = "";
                txtcostEleSeg.Text = Des;
                _result = CHNLSVC.Financial.GetCostSegmentMaster_CODE(Des);
                if (_result.Rows.Count > 0)
                {
                    txtcostEleSeg.Text = _result.Rows[0][0].ToString();
                    Session["SegDescription"] = _result.Rows[0][1].ToString();
                }
            }
            UserPopoup.Hide();
        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "1")
            {
                _result = CHNLSVC.Financial.GetCostCatergoryMaster(ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "1";
                BindUCtrlDDLData(_result);
               // ddlSearchbykey.Items.FindByText("DESCRIPTION").Enabled = false;
                UserPopoup.Show();
            }
            else if (lblvalue.Text == "2")
            {
                _result = CHNLSVC.Financial.GetCostSegementMaster(ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "2";
               // ddlSearchbykey.Items.FindByText("DESCRIPTION").Enabled = false;
                BindUCtrlDDLData(_result);
                UserPopoup.Show();
            }
            else if (lblvalue.Text == "3")
            {
                _result = CHNLSVC.Financial.GetCostCatergoryMaster(ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "3";
                BindUCtrlDDLData(_result);
               // ddlSearchbykey.Items.FindByText("DESCRIPTION").Enabled = false;
                UserPopoup.Show();
            }
            else if (lblvalue.Text == "4")
            {
                _result = CHNLSVC.Financial.GetCostSegementMaster(ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "4";
                BindUCtrlDDLData(_result);
                UserPopoup.Show();
            }
            else if (lblvalue.Text == "5")
            {
                _result = CHNLSVC.Financial.GetCostSegementMaster(ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "5";
                BindUCtrlDDLData(_result);
                UserPopoup.Show();
            }
        }
        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResult.PageIndex = e.NewPageIndex;
            if (lblvalue.Text == "1")
            {
                _result = CHNLSVC.Financial.GetCostCatergoryMaster(null,null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "1";
                BindUCtrlDDLData(_result);
              //  ddlSearchbykey.Items.FindByText("DESCRIPTION").Enabled = false;
                UserPopoup.Show();
            }
            if (lblvalue.Text == "2")
            {
                _result = CHNLSVC.Financial.GetCostSegementMaster("","");
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "2";
                BindUCtrlDDLData(_result);       
                UserPopoup.Show();
            }
            else if (lblvalue.Text == "3")
            {
                _result = CHNLSVC.Financial.GetCostCatergoryMaster(ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "3";
                BindUCtrlDDLData(_result);
              //  ddlSearchbykey.Items.FindByText("DESCRIPTION").Enabled = false;
                UserPopoup.Show();
            }
            else if (lblvalue.Text == "4")
            {
                _result = CHNLSVC.Financial.GetCostSegementMaster(ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "4";
                BindUCtrlDDLData(_result);
                UserPopoup.Show();
            }

            else if (lblvalue.Text == "5")
            {
                _result = CHNLSVC.Financial.GetCostSegementMaster(ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "5";
                BindUCtrlDDLData(_result);
                UserPopoup.Show();
            }
            UserPopoup.Show();
        }
#endregion

        #region Cost Segment
        protected void lbtnCostSegmentAdd_Click(object sender, EventArgs e)
        {
            ClearErrorMsg();
            if (txtCSegmentCode.Text == string.Empty)
            {
                ErrorMsgCosSeg("Please enter Cost Segment Code !");
                return;
            }
            if (txtSegDescription.Text == string.Empty)
            {
                ErrorMsgCosSeg("Please enter Description!");
                return;
            }
            Int32 row_aff = 0;
            _ImportCostSegmentMaster.Msse_cd = txtCSegmentCode.Text.Trim();
            _ImportCostSegmentMaster.Msse_desc = txtSegDescription.Text;
            _ImportCostSegmentMaster.Msse_act = (chkSegActive.Checked == true) ? true : false;
            _ImportCostSegmentMaster.Msse_cre_by = Session["UserID"].ToString();
            _ImportCostSegmentMaster.Msse_cre_dt = System.DateTime.Now;
            _ImportCostSegmentMaster.Msse_session_id = Session["SessionID"].ToString();
            _ImportCostSegmentMaster.Msse_mod_by = Session["UserID"].ToString();
            _ImportCostSegmentMaster.Msse_mod_dt = System.DateTime.Now;
            if (txtSavelconformmessageValue.Value == "Yes")
            {
                row_aff = CHNLSVC.Financial.SaveCostSegmentMaster(_ImportCostSegmentMaster);
                if (row_aff > 0)
                {
                    SuccessMsgCostSeg("Successfully Saved. !");
                    ClearTextBox();
                    
                }
               
            }
            UserPopoup.Hide();
        }

        protected void lbtnCsegCode_Click(object sender, EventArgs e)
        {
            ClearErrorMsg();
            _result = CHNLSVC.Financial.GetCostSegementMaster("","");
            grdResult.DataSource = _result;
            grdResult.DataBind();
            lblvalue.Text = "2";
            BindUCtrlDDLData(_result);
           // ddlSearchbykey.Items.FindByText("DESCRIPTION").Enabled = false;
            UserPopoup.Show();
        }

        #endregion

        protected void lbtnCTCategoryC_Click(object sender, EventArgs e)
        {
            ClearErrorMsg();
            _result = CHNLSVC.Financial.GetCostCatergoryMaster(null,null);
            grdResult.DataSource = _result;
            grdResult.DataBind();
            lblvalue.Text = "3";
            BindUCtrlDDLData(_result);
           // ddlSearchbykey.Items.FindByText("DESCRIPTION").Enabled = false;
            UserPopoup.Show();
        }

        protected void lbtnCTSegC_Click(object sender, EventArgs e)
        {
            ClearErrorMsg();
            _result = CHNLSVC.Financial.GetCostSegementMaster("", "");
            grdResult.DataSource = _result;
            grdResult.DataBind();
            lblvalue.Text = "4";
            BindUCtrlDDLData(_result);
            //ddlSearchbykey.Items.FindByText("DESCRIPTION").Enabled = false;
            UserPopoup.Show();
        }

        protected void lbtnCostTypeAdd_Click(object sender, EventArgs e)
        {
            ClearErrorMsg();

            if (ViewState["CostSeg"] == null)
            {
                ErrorMsgCosType("Please Select  Cost Category Code!");
                return;
            }
            //if (txtCTSegC.Text == string.Empty)
            //{
            //    ErrorMsgCosType("Please enter Cost Segment Code !");
            //    return;
            //}
            DataTable CosttypeTbl_ = new DataTable();
            CosttypeTbl_.Columns.Add(new System.Data.DataColumn("COST", typeof(String)));
            CosttypeTbl_.Columns.Add(new System.Data.DataColumn("mcat_act", typeof(Boolean)));
            CosttypeTbl_.TableName = "tblCostSegment";
            DataRow dr;
            foreach (GridViewRow row in grdCosetSeg.Rows)
            {
                Label mcat_cd = (Label)row.FindControl("mcat_cd");
                CheckBox mcat_act = (CheckBox)row.FindControl("mcat_act");
                dr = CosttypeTbl_.NewRow();
                dr[0] = mcat_cd.Text;
                if (mcat_act.Checked == true)
                {
                    dr[1] = true;
                }
                if (mcat_act.Checked == false)
                {
                    dr[1] = false;
                }
                CosttypeTbl_.Rows.Add(dr);
            }
            DataTable CostELETbl_ = new DataTable();
            CostELETbl_.Columns.Add(new System.Data.DataColumn("mcae_ele_cat", typeof(String)));
            CostELETbl_.Columns.Add(new System.Data.DataColumn("mcae_ele_tp", typeof(String)));
            CostELETbl_.Columns.Add(new System.Data.DataColumn("mcae_cd", typeof(String)));
            CostELETbl_.Columns.Add(new System.Data.DataColumn("mcae_is_edit", typeof(Boolean)));
            CostELETbl_.Columns.Add(new System.Data.DataColumn("mcae_act", typeof(Boolean)));
            CostELETbl_.TableName = "tblCostELE";
            DataRow ddr;
            foreach (GridViewRow row in gridELE.Rows)
            {
                Label mcae_ele_cat = (Label)row.FindControl("mcae_ele_cat");
                Label mcae_ele_tp = (Label)row.FindControl("mcae_ele_tp");
                Label mcae_cd = (Label)row.FindControl("mcae_cd");
                CheckBox mcae_is_edit = (CheckBox)row.FindControl("mcae_is_edit");
                CheckBox mcae_act = (CheckBox)row.FindControl("mcae_act");
                ddr = CostELETbl_.NewRow();
                ddr[0] = mcae_ele_cat.Text;
                ddr[1] = mcae_ele_tp.Text;
                ddr[2] = mcae_cd.Text;
                if (mcae_is_edit.Checked == true){ddr[3] = true;}
                if (mcae_is_edit.Checked == false){ddr[3] = false;}
                if (mcae_act.Checked == true) { ddr[4] = true; }
                if (mcae_act.Checked == false) { ddr[4] = false; }

                CostELETbl_.Rows.Add(ddr);
            }

           // ViewState["CostSeg"] = CosttypeTbl_;
           // DataTable CosttypeTbl = ViewState["CostSeg"] as DataTable;
           // DataTable CostELETbl_ = ViewState["CostELE"] as DataTable;

            Int32 row_aff = 0;
            _ImportCostType.Mcat_cat_cd = txtCTCategoryC.Text.Trim();
            _ImportCostType.Mcat_cre_by = Session["UserID"].ToString();
            _ImportCostType.Mcat_cre_dt = System.DateTime.Now;
            _ImportCostType.Mcat_session_id = Session["SessionID"].ToString();
            _ImportCostType.Mcat_mod_by = Session["UserID"].ToString();
            _ImportCostType.Mcat_mod_dt = System.DateTime.Now;
            _ImportCostType.Mcat_session_id = Session["SessionID"].ToString();


            _ImpoertCostElement.Mcae_com = Session["UserCompanyCode"].ToString();
            _ImpoertCostElement.Mcae_cre_by = Session["UserID"].ToString();
            _ImpoertCostElement.Mcae_cre_dt = System.DateTime.Now;
            _ImpoertCostElement.Mcae_mod_by = Session["UserID"].ToString();
            _ImpoertCostElement.Mcae_mod_dt = System.DateTime.Now;
            _ImpoertCostElement.Mcae_session_id = Session["SessionID"].ToString();

            if (txtSavelconformmessageValue.Value == "Yes")
            {
                row_aff = CHNLSVC.Financial.SaveCostTypeMaster(_ImportCostType, CosttypeTbl_, _ImpoertCostElement, CostELETbl_);
                if (row_aff > 0)
                {
                    SuccessMsgCostType("Successfully Saved. !");

                    //ClearTextBox();
                }
                //else
                //{
                //    ErrorMsgCosType("UnSuccessfully Saved... ");
                //}
            }
            UserPopoup.Hide();
        }

        protected void grdCostcat_SelectedIndexChanged(object sender, EventArgs e)
        {
           

        }
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            ClearErrorMsg();
            gridELE.DataSource = new int[] { };
            gridELE.DataBind();
            txtCTCategoryC.Text = "";
            txtCTSegC.Text = "";
            txtcostEleSeg.Text = "";
            foreach (GridViewRow row in grdCostcat.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("COST_CS") as CheckBox);
                    if (chkRow.Checked)
                    {
                        txtCTCategoryC.Text = (row.FindControl("CODE") as Label).Text;

                        DataTable tbl = CHNLSVC.Financial.GetCostType_CODE(txtCTCategoryC.Text, "");
                        grdCosetSeg.DataSource = tbl;
                        grdCosetSeg.DataBind();
                        ViewState["CostSeg"] = tbl;
                       
                    }
                }
            }
           


        }

        protected void lbtnAddSeg_Click(object sender, EventArgs e)
        {
            ClearErrorMsg();
            if (txtCTCategoryC.Text == string.Empty)
            {
                ErrorMsgCosType("Please select Cost Catergory");
                return;
            }
            if (txtCTSegC.Text == string.Empty)
            {
                ErrorMsgCosType("Please select Cost Segment");
                return;
            }
            DataTable Segtbl = ViewState["CostSeg"] as DataTable;
            int value ;
            if((Segtbl!=null) && (Segtbl.Rows.Count > 0))
            {
                string CODE = Segtbl.Rows[0]["mcat_cd"].ToString();
                foreach (DataRow row in Segtbl.Rows)
                {
                    if (row["mcat_cd"].ToString() == txtCTSegC.Text)
                    {
                        ErrorMsgCosType("This cost segment is already added ");
                        return;
                    }
                }
                if (chkCTActive.Checked == true)
                {
                    value = 1;
                }
                else
                {
                    value = 0;
                }
                Segtbl.Rows.Add(txtCTSegC.Text, Session["SegDescription"].ToString(), value);
                grdCosetSeg.DataSource = Segtbl;
                grdCosetSeg.DataBind();
                ViewState["CostSeg"] = Segtbl;

            }
            else
            {
                if (chkCTActive.Checked == true)
                {
                    value = 1;
                }
                else
                {
                    value = 0;
                }
                Segtbl.Rows.Add(txtCTSegC.Text, Session["SegDescription"].ToString(), value);
                grdCosetSeg.DataSource = Segtbl;
                grdCosetSeg.DataBind();
                ViewState["CostSeg"] = Segtbl;
            }
        }

        protected void COST_Seg__CheckedChanged(object sender, EventArgs e)
        {
            ClearErrorMsg();
            var activeCheckBox = sender as CheckBox;
            if (activeCheckBox != null)
            {
                var isChecked = activeCheckBox.Checked;
                var tempCheckBox = new CheckBox();
                if (isChecked == false)
                {
                    activeCheckBox.Checked = false;
                    Session["txtCTCategoryC"] = "";
                    Session["SE2CODE"] = "";
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.costtype);
                    DataTable tbl = CHNLSVC.Financial.GetCostELEMaster_CODE(SearchParams);
                    gridELE.DataSource = tbl;
                    gridELE.DataBind();
                    ViewState["CostELE"] = tbl;
                    return;
                }
                foreach (GridViewRow gvRow in grdCosetSeg.Rows)
                {
                    tempCheckBox = gvRow.FindControl("COST_Seg_") as CheckBox;
                    if (tempCheckBox != null)
                    {
                        tempCheckBox.Checked = !isChecked;
                    }
                }
                if (isChecked)
                {
                    activeCheckBox.Checked = true;
                }
            }
            foreach (GridViewRow row in grdCosetSeg.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("COST_Seg_") as CheckBox);
                    if (chkRow.Checked)
                    {
                        Session["txtCTCategoryC"] = txtCTCategoryC.Text;
                        Session["SE2CODE"] = (row.FindControl("mcat_cd") as Label).Text;
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.costtype);
                        DataTable tbl = CHNLSVC.Financial.GetCostELEMaster_CODE(SearchParams);
                        gridELE.DataSource = tbl;
                        gridELE.DataBind();
                        ViewState["CostELE"] = tbl;
                    }
                }
            }



        }

        protected void lbtneleseg_Click(object sender, EventArgs e)
        {
            ClearErrorMsg();
            _result = CHNLSVC.Financial.GetCostSegementMaster("", "");
            grdResult.DataSource = _result;
            grdResult.DataBind();
            lblvalue.Text = "5";
            BindUCtrlDDLData(_result);
       
            //ddlSearchbykey.Items.FindByText("DESCRIPTION").Enabled = false;
            UserPopoup.Show();
        }

        protected void lbtnAddELE_Click(object sender, EventArgs e)
        {
            ClearErrorMsg();
            int value, editvalue;

            if (txtCTCategoryC.Text == string.Empty)
            {
                ErrorMsgCosType("Please select Cost Catergory");
                return;
            }
            if (txtcostEleSeg.Text == string.Empty)
            {
                ErrorMsgCosType("Please select Cost Segment");
                return;
            }
            if (Session["SE2CODE"].ToString() == "")
            {
                ErrorMsgCosType("Please select Cost Segment");
                return;
            }
            if (chkEleActive.Checked == true) { value = 1; } else { value = 0; }
            if (chkEleEdit.Checked == true) { editvalue = 1; } else { editvalue = 0; }

            DataTable CELEtbl = ViewState["CostELE"] as DataTable;
            
            if ((CELEtbl != null) && (CELEtbl.Rows.Count > 0))
            {
                string CODE = CELEtbl.Rows[0]["mcae_cd"].ToString();
                foreach (DataRow row in CELEtbl.Rows)
                {
                    if (row["mcae_cd"].ToString() == txtcostEleSeg.Text)
                    {
                        ErrorMsgCosType("This cost segment is already added ");
                        return;
                    }
                }

                CELEtbl.Rows.Add(txtCTCategoryC.Text, Session["SE2CODE"].ToString(), txtcostEleSeg.Text, editvalue, value);
                gridELE.DataSource = CELEtbl;
                gridELE.DataBind();
                ViewState["CostELE"] = CELEtbl;
            }
            else
            {
                CELEtbl.Rows.Add(txtCTCategoryC.Text, Session["SE2CODE"].ToString(), txtcostEleSeg.Text, editvalue, value);
                gridELE.DataSource = CELEtbl;
                gridELE.DataBind();
                ViewState["CostELE"] = CELEtbl;
            }
        }

        protected void lbtnCostTypeClear_Click(object sender, EventArgs e)
        {
            ClearErrorMsg();
            if (txtClearlconformmessageValue.Value == "Yes")
            {
                try
                {
                    Response.Redirect(Request.RawUrl, false);
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
                }
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
            }
            txtClearlconformmessageValue.Value = "";
        }
      

        

    }
}