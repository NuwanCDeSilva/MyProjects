using FF.BusinessObjects.InventoryNew;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Enquiries.Inventory
{
    public partial class AODTracker : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    grvaoddetails.DataSource = new int[] { };
                    grvaoddetails.DataBind();
                    grditemdet.DataSource = new int[] { };
                    grditemdet.DataBind();
                    grdserialdet.DataSource = new int[] { };
                    grdserialdet.DataBind();

                    txtfdate.Text = DateTime.Now.Date.AddDays(-2).ToString("dd/MMM/yyyy");
                    txttdate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                    txtfromloc.Text = Session["UserDefLoca"].ToString();

                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }


        }

        private void DispMsg(string msgText, string msgType = "")
        {
            msgText = msgText.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");

            if (msgType == "N")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + msgText + "');", true);
            }
            else if (msgType == "W" || msgType == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msgText + "');", true);
            }
            else if (msgType == "S")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + msgText + "');", true);
            }
            else if (msgType == "E")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + msgText + "');", true);
            }
            else if (msgType == "A")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + msgText + "');", true);
            }
        }
        private string SetComSeInParameters(CommonUIDefiniton.SearchUserControlType _type)
        {

            System.Text.StringBuilder paramsText = new System.Text.StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.TransMethod:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.TransParty:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.TransRef:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Trackdoc:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }

                default:
                    break;
            }
            return paramsText.ToString();
        }
        public void BindCmbSearchbykey(DataTable _dataSource)
        {
            this.cmbSearchbykey.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                this.cmbSearchbykey.Items.Add(col.ColumnName);
            }
            this.cmbSearchbykey.SelectedIndex = 0;
        }
        private void FilterData()
        {
            try
            {
                if (lblvalue.Text == "fLocSearch")
                {
                    string SearchParams = SetComSeInParameters(CommonUIDefiniton.SearchUserControlType.Location);
                    DataTable result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, cmbSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    dgvResult.DataSource = result;
                    dgvResult.DataBind();
                    lblvalue.Text = "fLocSearch";
                    ViewState["SEARCH"] = result;
                    PopupSearch.Show();
                    Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                if (lblvalue.Text == "toLocSearch")
                {
                    string SearchParams = SetComSeInParameters(CommonUIDefiniton.SearchUserControlType.Location);
                    DataTable result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, cmbSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    dgvResult.DataSource = result;
                    dgvResult.DataBind();
                    lblvalue.Text = "toLocSearch";
                    ViewState["SEARCH"] = result;
                    PopupSearch.Show();
                    Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                if (lblvalue.Text == "TransMethod")
                {
                    string SearchParams = SetComSeInParameters(CommonUIDefiniton.SearchUserControlType.TransMethod);
                    DataTable result = CHNLSVC.CommonSearch.GetTransportMethods(SearchParams, cmbSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    dgvResult.DataSource = result;
                    dgvResult.DataBind();
                    lblvalue.Text = "TransMethod";
                    ViewState["SEARCH"] = result;
                    PopupSearch.Show();
                    Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                if (lblvalue.Text == "TransParty")
                {
                    string SearchParams = SetComSeInParameters(CommonUIDefiniton.SearchUserControlType.TransParty);
                    DataTable result = CHNLSVC.CommonSearch.GetTransportParty(SearchParams, cmbSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    dgvResult.DataSource = result;
                    dgvResult.DataBind();
                    lblvalue.Text = "TransParty";
                    ViewState["SEARCH"] = result;
                    PopupSearch.Show();
                    Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                if (lblvalue.Text == "TransRef")
                {
                    string SearchParams = SetComSeInParameters(CommonUIDefiniton.SearchUserControlType.TransRef);
                    DataTable result = CHNLSVC.CommonSearch.GetTransportReference(SearchParams, cmbSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    dgvResult.DataSource = result;
                    dgvResult.DataBind();
                    lblvalue.Text = "TransRef";
                    ViewState["SEARCH"] = result;
                    PopupSearch.Show();
                    Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                if (lblvalue.Text == "Trackdoc")
                {
                    string SearchParams = SetComSeInParameters(CommonUIDefiniton.SearchUserControlType.Trackdoc);
                    DataTable result = CHNLSVC.CommonSearch.GetAODTrackerDOC(SearchParams, cmbSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    dgvResult.DataSource = result;
                    dgvResult.DataBind();
                    lblvalue.Text = "Trackdoc";
                    ViewState["SEARCH"] = result;
                    PopupSearch.Show();
                    Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                


            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }
        protected void dgvResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {

                dgvResult.PageIndex = e.NewPageIndex;
                dgvResult.DataSource = null;
                dgvResult.DataSource = (DataTable)ViewState["SEARCH"];
                dgvResult.DataBind();
                dgvResult.PageIndex = 0;
                PopupSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void dgvResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lblvalue.Text == "fLocSearch")
                {
                    txtfromloc.Text = dgvResult.SelectedRow.Cells[1].Text;
                }
                if (lblvalue.Text == "toLocSearch")
                {
                    txttoloc.Text = dgvResult.SelectedRow.Cells[1].Text;
                }
                if (lblvalue.Text == "TransMethod")
                {
                    txttransportmethod.Text = dgvResult.SelectedRow.Cells[1].Text;
                }
                if (lblvalue.Text == "TransParty")
                {
                    txttrparty.Text = dgvResult.SelectedRow.Cells[2].Text;
                }
                if (lblvalue.Text == "TransRef")
                {
                    txtrefno.Text = dgvResult.SelectedRow.Cells[1].Text;
                    txttransportmethod.Text = dgvResult.SelectedRow.Cells[2].Text;
                    txttrparty.Text = dgvResult.SelectedRow.Cells[3].Text;

                }
                if (lblvalue.Text == "Trackdoc")
                {
                    txtdocno.Text = dgvResult.SelectedRow.Cells[1].Text;
                }
                


            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }
        protected void txtfromloc_TextChanged(object sender, EventArgs e)
        {

        }

        protected void chkinword_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void lbtnfromloc_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetComSeInParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, null, null);
                dgvResult.DataSource = result;
                dgvResult.DataBind();
                lblvalue.Text = "fLocSearch";
                BindCmbSearchbykey(result);
                ViewState["SEARCH"] = result;
                PopupSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void chkoutward_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkoutward.Checked)
                {
                    lbcomplete.Visible = true;
                    lbgit.Visible = true;
                    chkcomplete.Visible = true;
                    chkgz7.Visible = true;
                }
                else
                {
                    lbcomplete.Visible = false;
                    lbgit.Visible = false;
                    chkcomplete.Visible = false;
                    chkgz7.Visible = false;
                    chkcomplete.Checked = false;
                    chkgz7.Checked = false;
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void txttoloc_TextChanged(object sender, EventArgs e)
        {

        }

        protected void lbtntoloc_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetComSeInParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, null, null);
                dgvResult.DataSource = result;
                dgvResult.DataBind();
                lblvalue.Text = "toLocSearch";
                BindCmbSearchbykey(result);
                ViewState["SEARCH"] = result;
                PopupSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void txtfdate_TextChanged(object sender, EventArgs e)
        {

        }

        protected void chkcomplete_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chkgz7_CheckedChanged(object sender, EventArgs e)
        {
          
        }

        protected void txtdocno_TextChanged(object sender, EventArgs e)
        {
            lbtnmainsearch2_Click(null, null);
        }

        protected void lbtndocsearch_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetComSeInParameters(CommonUIDefiniton.SearchUserControlType.Trackdoc);
                DataTable result = CHNLSVC.CommonSearch.GetAODTrackerDOC(SearchParams, null, null);
                dgvResult.DataSource = result;
                dgvResult.DataBind();
                lblvalue.Text = "Trackdoc";
                BindCmbSearchbykey(result);
                ViewState["SEARCH"] = result;
                PopupSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void lbtnmainsearch2_Click(object sender, EventArgs e)
        {
            try
            {
                Clearbasic();
                string docno = txtdocno.Text.ToString();
                if (docno=="")
                {
                    DispMsg("Please Enter Document No","N");
                    return;
                }
                List<AODTrackerHDRdata> hdrdata = CHNLSVC.Inventory.Get_AOD_TrackerdataByDoc(Session["UserCompanyCode"].ToString(),docno);
                grvaoddetails.DataSource = hdrdata;
                grvaoddetails.DataBind();
            }catch(Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void lbtntrmethod_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetComSeInParameters(CommonUIDefiniton.SearchUserControlType.TransMethod);
                DataTable result = CHNLSVC.CommonSearch.GetTransportMethods(SearchParams, null, null);
                dgvResult.DataSource = result;
                dgvResult.DataBind();
                lblvalue.Text = "TransMethod";
                BindCmbSearchbykey(result);
                ViewState["SEARCH"] = result;
                PopupSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void txttrparty_TextChanged(object sender, EventArgs e)
        {

        }

        protected void lbtntrparty_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetComSeInParameters(CommonUIDefiniton.SearchUserControlType.TransParty);
                DataTable result = CHNLSVC.CommonSearch.GetTransportParty(SearchParams, null, null);
                dgvResult.DataSource = result;
                dgvResult.DataBind();
                lblvalue.Text = "TransParty";
                BindCmbSearchbykey(result);
                ViewState["SEARCH"] = result;
                PopupSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void txttransportmethod_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnClose_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnsearchpopup_Click(object sender, EventArgs e)
        {
            try
            {
                FilterData();
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }


        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                FilterData();
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void txtrefno_TextChanged(object sender, EventArgs e)
        {

        }

        protected void lbtnrefno_Click(object sender, EventArgs e)
        {

            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetComSeInParameters(CommonUIDefiniton.SearchUserControlType.TransRef);
                DataTable result = CHNLSVC.CommonSearch.GetTransportReference(SearchParams, null, null);
                dgvResult.DataSource = result;
                dgvResult.DataBind();
                lblvalue.Text = "TransRef";
                BindCmbSearchbykey(result);
                ViewState["SEARCH"] = result;
                PopupSearch.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void lbtnsearch3_Click(object sender, EventArgs e)
        {
            try
            {
                Clearbasic();
                string trmethod = txttransportmethod.Text.ToString();
                string trparty = txttrparty.Text.ToString();
                string trrefno = txtrefno.Text.ToString();
                string com = Session["UserCompanyCode"].ToString();

                if (trmethod == "" && trparty == "" && trrefno == "")
                {
                    DispMsg("Please Select Transport Details!", "N");
                    return;
                }

                List<AODTrackerHDRdata> hdrdata = CHNLSVC.Inventory.Get_AOD_TrackerdataByTRNS(com,trmethod,trparty,trrefno);
                grvaoddetails.DataSource = hdrdata;
                grvaoddetails.DataBind();

            }catch(Exception ex)
            {
                DispMsg(ex.Message,"E");
            }
        }

        protected void lbtnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                Clearbasic();
                string com = Session["UserCompanyCode"].ToString();
                if (Regex.IsMatch(txtfdate.Text, "^(0?[1-9]|[12][0-9]|3[01])/(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)/(19|20)\\d\\d$") == false)
                {
                    DispMsg("Please Enter Valid From Date!", "N");
                    txtfdate.Text = "";
                    txtfdate.Focus();
                    return;
                }

                if (Regex.IsMatch(txttdate.Text, "^(0?[1-9]|[12][0-9]|3[01])/(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)/(19|20)\\d\\d$") == false)
                {
                    DispMsg("Please Enter Valid To Date!", "N");
                    txttdate.Text = "";
                    txttdate.Focus();
                    return;
                }

                DateTime fromdate = Convert.ToDateTime(txtfdate.Text.ToString());
                DateTime todate = Convert.ToDateTime(txttdate.Text.ToString());
                string inloc = txtfromloc.Text.ToString();
                string outloc = txttoloc.Text.ToString();
                string inval = null;
                string outval = null; ;
                if (chkinword.Checked)
                {
                    inval = "1";
                }
                if (chkoutward.Checked)
                {
                    outval = "0";
                }
                List<AODTrackerHDRdata> hdrdata = CHNLSVC.Inventory.Get_AOD_Trackerdata(com, outloc, inloc, fromdate, todate, inval, outval);
                if (chkcomplete.Checked)
                {
                    hdrdata = hdrdata.Where(a => a.ith_stus == "F").ToList();
                }
                if (chkgz7.Checked)
                {
                    hdrdata = hdrdata.Where(a => a.ith_stus != "F").ToList();
                    hdrdata = hdrdata.Where(a => a.ith_stus != "C").ToList();

                }
                hdrdata = hdrdata.OrderByDescending(a => a.DocDate).ToList();
                grvaoddetails.DataSource = hdrdata;
                grvaoddetails.DataBind();

            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }
        protected void lbtnedititem_Click(object sender, EventArgs e)
        {
            try
            {
                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;
                Label lbDocNogr = (Label)row.FindControl("lbDocNo");
                Label lbDocDategr = (Label)row.FindControl("lbDocDate");
                Label lbusergr = (Label)row.FindControl("lbuser");
                Label lbCreDategr = (Label)row.FindControl("lbCreDate");

                string com = Session["UserCompanyCode"].ToString();
                DataTable trdata = CHNLSVC.Inventory.GETTRANSDETAILS(com, lbDocNogr.Text.ToString());

                lbdocnotxt.Visible = true;
                lbdocno.Text = lbDocNogr.Text.ToString();
                lbdocno.Visible = true;

                lbdocdatetxt.Visible = true;
                lbdocdate.Text = lbDocDategr.Text.ToString();
                lbdocdate.Visible = true;

                lbusertxt.Visible = true;
                lbuser.Text = lbusergr.Text.ToString();
                lbuser.Visible = true;

                lbcredttxt.Visible = true;
                lbcredt.Text = lbCreDategr.Text.ToString();
                lbcredt.Visible = true;

                if (trdata.Rows.Count>0)
                {
                    string trmethod = trdata.Rows[0]["itrn_trns_method"].ToString();
                    string trtype = trdata.Rows[0]["itrn_trns_pty_tp"].ToString();
                    string refno = trdata.Rows[0]["itrn_ref_no"].ToString();
                    string party = trdata.Rows[0]["itrn_trns_pty_cd"].ToString();

                    lbtrmethodtext.Visible = true;
                    lbtrmethod.Visible = true;
                    lbtrmethod.Text = trmethod;

                    lbpartytxt.Visible = true;
                    lbparty.Visible = true;
                    lbparty.Text = party;

                    lbrefnotxt.Visible = true;
                    lbrefno.Text = refno;
                    lbrefno.Visible = true;
                    
                }
                else
                {
                    lbtrmethodtext.Visible = false;
                    lbtrmethod.Visible = false;
                    lbpartytxt.Visible = false;
                    lbparty.Visible = false;
                    lbrefnotxt.Visible = false;
                    lbrefno.Visible = false;

                }
                LoadItemDetails(lbDocNogr.Text.ToString());
                lbitmcounttxt.Visible = true;
                lbitmcount.Text = Session["totqty"].ToString();
                lbitmcount.Visible = true;
                LoadSerialDetails(com, lbDocNogr.Text.ToString());


            }catch(Exception ex)
            {
                DispMsg(ex.Message,"E");
            }
        }

        private void LoadItemDetails(string docno)
        {
            try
            {
                Int32 totqty = 0;
                DataTable itms = CHNLSVC.Inventory.GETAODITEMS(docno);
                if (itms.Rows.Count > 0)
                {
                    grditemdet.DataSource = itms;
                    grditemdet.DataBind();
                    for (int k = 0; k < itms.Rows.Count; k++)
                    {
                        totqty = totqty+ Convert.ToInt32(itms.Rows[k]["iti_qty"].ToString());
                    }
                    Session["totqty"] = totqty;

                }
            }
            catch(Exception EX)
            {
                DispMsg(EX.Message, "E");
            }
        }
        private void LoadSerialDetails(string com, string docno)
        {
            try
            {
                DataTable serials = CHNLSVC.Inventory.GETAODSERIAL(com, docno);
                if (serials.Rows.Count > 0)
                {
                    grdserialdet.DataSource = serials;
                    grdserialdet.DataBind();
                }
            }
            catch (Exception EX)
            {
                DispMsg(EX.Message, "E");
            }
        }

        private void Clearbasic()
        {
            grvaoddetails.DataSource = new int[] { };
            grvaoddetails.DataBind();
            grditemdet.DataSource = new int[] { };
            grditemdet.DataBind();
            grdserialdet.DataSource = new int[] { };
            grdserialdet.DataBind();
            lbtrmethodtext.Visible = false;
            lbtrmethod.Visible = false;
            lbpartytxt.Visible = false;
            lbparty.Visible = false;
            lbrefnotxt.Visible = false;
            lbrefno.Visible = false;

            lbdocnotxt.Visible = false;
            lbdocno.Visible = false;

            lbdocdatetxt.Visible = false;
            lbdocdate.Visible = false;

            lbusertxt.Visible = false;
            lbuser.Visible = false;

            lbcredttxt.Visible = false;
            lbcredt.Visible = false;
            lbitmcounttxt.Visible = false;
            lbitmcount.Visible = false;
        }
        private void ClearAll()
        {
            grvaoddetails.DataSource = new int[] { };
            grvaoddetails.DataBind();
            grditemdet.DataSource = new int[] { };
            grditemdet.DataBind();
            grdserialdet.DataSource = new int[] { };
            grdserialdet.DataBind();
            //txt field
            txttoloc.Text = "";
            txtdocno.Text = "";
            txttransportmethod.Text = "";
            txttrparty.Text = "";
            txtrefno.Text = "";

            //lable
            lbtrmethodtext.Visible = false;
            lbtrmethod.Visible = false;
            lbpartytxt.Visible = false;
            lbparty.Visible = false;
            lbrefnotxt.Visible = false;
            lbrefno.Visible = false;

            lbdocnotxt.Visible = false;
            lbdocno.Visible = false;

            lbdocdatetxt.Visible = false;
            lbdocdate.Visible = false;

            lbusertxt.Visible = false;
            lbuser.Visible = false;

            lbcredttxt.Visible = false;
            lbcredt.Visible = false;

            lbitmcounttxt.Visible = false;
            lbitmcount.Visible = false;

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
    }
}