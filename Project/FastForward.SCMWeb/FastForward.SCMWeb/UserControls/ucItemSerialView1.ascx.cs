using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FastForward.SCMWeb.Services;
using FF.BusinessObjects;

namespace FastForward.SCMWeb.UserControls
{
    public partial class ucItemSerialView1 : System.Web.UI.UserControl
    {
        Base _basePage;
        private bool isVisible;
        DataTable _dtBinStock
        {
            get { if (Session["_dtBinStock"] != null) { return (DataTable)Session["_dtBinStock"]; } else { return new DataTable(); } }
            set { Session["_dtBinStock"] = value; }
        }
        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }
        private string iTEM_CODE;

        public string ITEM_CODE
        {
            get { return Session["ITEM_CODE"] as string; }
            set { Session["ITEM_CODE"] = value; }
        }

        private string iTEM_STATUS;

        public string ITEM_STATUS
        {
            get { return iTEM_STATUS; }
            set { iTEM_STATUS = value; }
        }

        private string cOMPANY;

        public string COMPANY
        {
            get { return cOMPANY; }
            set { cOMPANY = value; }
        }

        private string lOC;

        public string LOC
        {
            get { return lOC; }
            set { lOC = value; }
        }

        private string cHANNEL;

        public string CHANNEL
        {
            get { return cHANNEL; }
            set { cHANNEL = value; }
        }

        private string sUB_CHANNEL;

        public string SUB_CHANNEL
        {
            get { return sUB_CHANNEL; }
            set { sUB_CHANNEL = value; }
        }

        private string aREA;

        public string AREA
        {
            get { return aREA; }
            set { aREA = value; }
        }

        private string zONE;

        public string ZONE
        {
            get { return zONE; }
            set { zONE = value; }
        }

        private string rEAGION;

        public string REAGION
        {
            get { return rEAGION; }
            set { rEAGION = value; }
        }

        private string tYPE;

        public string TYPE
        {
            get { return tYPE; }
            set { tYPE = value; }
        }

        private bool showCost;

        public bool ShowCost
        {
            get { return showCost; }
            set { showCost = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _basePage = new Base();

                IsVisible = false;
                ITEM_CODE = string.Empty;
                ITEM_STATUS = string.Empty;
                COMPANY = string.Empty;
                CHANNEL = string.Empty;
                SUB_CHANNEL = string.Empty;
                AREA = string.Empty;
                ZONE = string.Empty;
                REAGION = string.Empty;
                LOC = string.Empty;
                TYPE = string.Empty;
                ShowCost = false;
                Session["ShowUcDocument"] = "N";
            }
            // Display();
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            _basePage = new Base();
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Item_Documents:
                    {
                        paramsText.Append(ITEM_CODE + seperator + ITEM_STATUS + seperator + COMPANY + seperator + CHANNEL + seperator + SUB_CHANNEL + seperator + AREA + seperator + REAGION + seperator + ZONE + seperator + LOC + seperator + TYPE + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item_Serials:
                    {
                        paramsText.Append(ITEM_CODE + seperator + ITEM_STATUS + seperator + COMPANY + seperator + CHANNEL + seperator + SUB_CHANNEL + seperator + AREA + seperator + REAGION + seperator + ZONE + seperator + LOC + seperator + TYPE + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemCate:
                    {
                        paramsText.Append(ITEM_CODE + seperator + ITEM_STATUS + seperator + COMPANY + seperator + CHANNEL + seperator + SUB_CHANNEL + seperator + AREA + seperator + REAGION + seperator + ZONE + seperator + LOC + seperator + TYPE.ToUpper() + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        public void ClearDisplay()
        {
            dgvDocument.DataSource = new int[] { };
            dgvSerial.DataSource = new int[] { };
            dgvDocument.DataBind();
            dgvSerial.DataBind();
        }
        public void Display()
        {
            string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Serials);
            DataTable _result2 = _basePage.CHNLSVC.CommonSearch.GetItemSerialSearchData(para);
            dgvSerial.Columns[10].Visible = true;
            dgvSerial.DataSource = new int[] { };
            dgvSerial.DataSource = _result2;
            Session["DATASERIAL"] = _result2;
            dgvSerial.DataBind();




            //change row color
            foreach (GridViewRow gvr in dgvSerial.Rows)
            {
                int available = Convert.ToInt32(gvr.Cells[10].Text);
                if (available == -1)
                {
                    gvr.BackColor = Color.Red;
                    gvr.Cells[8].Text = "Yes";
                    //gvr.Cells["_avail"].Style.ForeColor = Color.Red;
                }
                else
                {
                    gvr.Cells[8].Text = "No";
                    //gvr.Cells["_avail"].Style.ForeColor = Color.Green;
                }
            }

            dgvSerial.Columns[10].Visible = false;

            para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Documents);
            // DataTable _result = _basePage.CHNLSVC.CommonSearch.GetItemDocSearchData(para);
            DataTable _result = _basePage.CHNLSVC.CommonSearch.GetInvTrackerDocData(para);
            dgvDocument.DataSource = new int[] { };
            dgvDocument.DataSource = _result;
            Session["DATADOC"] = _result;
            dgvDocument.DataBind();

            /*Bin data bind*/
            _result = new DataTable();
            para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemCate);
            _result = _basePage.CHNLSVC.CommonSearch.GetItemBinSearchData(para);
            dgvBin.DataSource = new int[] { };
            if (_result.Rows.Count > 0)
            {
                dgvBin.DataSource = _result;
                _dtBinStock = _result;
            }
            dgvBin.DataBind();

            Base obj = new Base();
            MasterItem mst_item = obj.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), ITEM_CODE);
            if (mst_item != null)
            {
                if (mst_item.Mi_is_ser1 == -1 || mst_item.Mi_is_ser1 == 0)
                {
                    hdfTabIndex.Value = "#Document";
                    hdfTabShow.Value = "#Document";
                    dgvSerial.DataSource = new int[] { };
                    dgvSerial.DataBind();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "CustomTabShow();", true);
                    //btnTemp_Click(null,null);
                }
            }
            dgvDocument.Columns[3].Visible = showCost;
            dgvSerial.Columns[9].Visible = showCost;

            //foreach (GridViewRow rw in dgvDocument.Rows)
            //{
            //    Label lblUnitCost = (Label)rw.FindControl("lblUnitCost");
            //    lblUnitCost.Visible = showCost;
            //}
            //foreach (GridViewRow rw in dgvDocument.Rows)
            //{
            //    Label lblUnitCost = (Label)rw.FindControl("lblUnitCost");
            //    lblUnitCost.Visible = showCost;
            //}
            //Label lblHdUnitCost = (Label)dgvDocument.HeaderRow.FindControl("lblHdUnitCost");
            //lblHdUnitCost.Visible = showCost;

            //Label lblHddUnitCost = (Label)dgvDocument.HeaderRow.FindControl("lblHdUnitCost");
            //lblHddUnitCost.Visible = showCost;

        }
        bool serials = false;
        bool doc = false;


        protected void dgvSerial_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                foreach (GridViewRow gvr in dgvSerial.Rows)
                {
                    ITEM_CODE = (gvr.FindControl("lblItem") as Label).Text;
                    Session["ItemCode"] = ITEM_CODE;
                    break;
                }
                dgvSerial.Columns[10].Visible = true;
                dgvSerial.PageIndex = e.NewPageIndex;
                dgvSerial.DataSource = new int[] { };
                dgvSerial.DataSource = (DataTable)Session["DATASERIAL"];
                dgvSerial.DataBind();

                Session["ShowUcDocument"] = "Y";
                //if (ShowCost)
                //{
                //    dgvSerial.Columns[9].Visible = true;
                //}
                //else
                //{
                //    dgvSerial.Columns[9].Visible = false;
                //}


                //change row color
                foreach (GridViewRow gvr in dgvSerial.Rows)
                {
                    int available = Convert.ToInt32(gvr.Cells[10].Text);
                    if (available == -1)
                    {
                        gvr.BackColor = Color.Red;
                        gvr.Cells[8].Text = "Yes";
                        //gvr.Cells["_avail"].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        gvr.Cells[8].Text = "No";
                        //gvr.Cells["_avail"].Style.ForeColor = Color.Green;
                    }
                    dgvSerial.Columns[10].Visible = false;
                }

                MasterItem mst_item = new Base().CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), Session["ItemCode"] != null ? (string)Session["ItemCode"] : ITEM_CODE);
                if (mst_item.Mi_is_ser1 == -1 || mst_item.Mi_is_ser1 == 0)
                {
                    dgvSerial.DataSource = new int[] { };
                    dgvSerial.DataBind();
                    hdfTabIndex.Value = "#Document";
                    // serialTab.Visible = false;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "CustomTabShow();", true);
                    //btnTemp_Click(null,null);
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void dgvDocument_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvDocument.PageIndex = e.NewPageIndex;
                dgvDocument.DataSource = new int[] { };
                dgvDocument.DataSource = (DataTable)Session["DATADOC"];
                dgvDocument.DataBind();
                Session["ShowUcDocument"] = "Y";
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void btnTemp_Click(object sender, EventArgs e)
        {
            hdfTabIndex.Value = "#Document";
        }

        protected void dgvBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvBin.PageIndex = e.NewPageIndex;
                dgvBin.DataSource = new int[] { };
                dgvBin.DataSource = (DataTable)_dtBinStock;
                dgvBin.DataBind();
                Session["ShowUcDocument"] = "Y";
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }


    }
}