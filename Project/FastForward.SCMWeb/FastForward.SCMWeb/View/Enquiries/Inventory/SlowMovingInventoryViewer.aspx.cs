using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Enquiries.Inventory
{
    public partial class SlowMovingInventoryViewer : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    gvalldetails.DataSource = new int[] { };
                    gvalldetails.DataBind();

                    gvorderbydetails.DataSource = new int[] { };
                    gvorderbydetails.DataBind();

                    //DataTable dtdates = CHNLSVC.Inventory.LoadDistinctDates(Session["UserCompanyCode"].ToString());
                    //ddlyear.DataSource = dtdates;
                    //ddlyear.DataTextField = "mac_avg_dt";
                    //ddlyear.DataValueField = "mac_avg_dt";
                    //ddlyear.DataBind();

                    DateTime fromdt = DateTime.Now.AddMonths(-1);
                    txtfrom.Text = fromdt.ToString("dd/MMM/yyyy");

                    DateTime todate = DateTime.Now;
                    txtto.Text = todate.ToString("dd/MMM/yyyy");

                    chkbrand.Checked = true;
                    chkclasific.Checked = true;
                    chkcat.Checked = true;
                    chkqty.Checked = true;
                    chknumitm.Checked = true;
                    chkitemall.Checked = true;
                    chkmodelall.Checked = true;

                    RBordby.SelectedValue = "3";
                    RBordby.Enabled = false;
                    /*
                    lb90.Visible = false;
                    lb120.Visible = false;
                    lb170.Visible = false;
                    lb270.Visible = false;
                    lb365.Visible = false;
                    lbavg.Visible = false;
                     * */
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        private void Clear()
        {
            try
            {
                gvalldetails.DataSource = new int[] { };
                gvalldetails.DataBind();

                gvorderbydetails.DataSource = new int[] { };
                gvorderbydetails.DataBind();

                DateTime fromdt = DateTime.Now.AddMonths(-1);
                txtfrom.Text = fromdt.ToString("dd/MMM/yyyy");

                DateTime todate = DateTime.Now;
                txtto.Text = todate.ToString("dd/MMM/yyyy");

                txtbrand.Text = string.Empty;
                chkbrand.Checked = true;
                txtclassific.Text = string.Empty;
                chkclasific.Checked = true;
                txtcat.Text = string.Empty;
                chkcat.Checked = true;
                txtqtylimit.Text = string.Empty;
                txtnoofitems.Text = string.Empty;
                chknumitm.Checked = true;
              //  txtpercen.Text = string.Empty;
                chkqty.Checked = true;

                /*
                lb90.Visible = false;
                lb120.Visible = false;
                lb170.Visible = false;
                lb270.Visible = false;
                lb365.Visible = false;
                lbavg.Visible = false;
                 * */
                txtitem.Text = "";
                txtmodel.Text = "";
                /*
                lb90val.Visible = false;
                lb120val.Visible = false;
                lb170val.Visible = false;
                lb270val.Visible = false;
                lb365val.Visible = false;
                lbavgval.Visible = false;
                 * */
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            if (txtconfirmclear.Value == "Yes")
            {
                Clear();  
            }
        }

        protected void lbtnsearchrec_Click(object sender, EventArgs e)
        {
            try
            {
                string brand = txtbrand.Text.Trim().ToUpper();
                string mainclassification = txtclassific.Text.Trim().ToUpper();
                string cat = txtcat.Text.Trim().ToUpper();
                Int32 qtylimit = 0;
                Int32 numofitems = 0;
                Decimal invpercentage = 0;

                if ((!string.IsNullOrEmpty(txtqtylimit.Text.Trim())) && (!string.IsNullOrEmpty(txtnoofitems.Text.Trim())))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Cant search by qty limit & no of items at the same time !!!')", true);
                    txtqtylimit.Text = string.Empty;
                    txtnoofitems.Text = string.Empty;
                    gvalldetails.DataSource = new int[] { };
                    gvalldetails.DataBind();
                    chkqty.Checked = false;
                    chknumitm.Checked = false;
                    return;
                }

                if (!string.IsNullOrEmpty(txtqtylimit.Text.Trim()))
                {
                    qtylimit = Convert.ToInt32(txtqtylimit.Text.Trim());
                }

                if (!string.IsNullOrEmpty(txtnoofitems.Text.Trim()))
                {
                    numofitems = Convert.ToInt32(txtnoofitems.Text.Trim());
                }

                //if (!string.IsNullOrEmpty(txtpercen.Text.Trim()))
                //{
                //    invpercentage = Convert.ToDecimal(txtpercen.Text.Trim());
                //}
                
                gvalldetails.DataSource = null;
                gvalldetails.DataBind();

                if (string.IsNullOrEmpty(txtbrand.Text.Trim()))
                {
                    chkbrand.Checked = true;
                }

                if (string.IsNullOrEmpty(txtclassific.Text.Trim()))
                {
                    chkclasific.Checked = true;
                }

                if (string.IsNullOrEmpty(txtcat.Text.Trim()))
                {
                    chkcat.Checked = true;
                }

                if (string.IsNullOrEmpty(txtqtylimit.Text.Trim()))
                {
                    chkqty.Checked = true;
                }

                if (string.IsNullOrEmpty(txtnoofitems.Text.Trim()))
                {
                    chknumitm.Checked = true;
                }

                DataTable dtslowmovitems = CHNLSVC.Inventory.GetSlowMovingInventoryDetails(Session["UserCompanyCode"].ToString(), Convert.ToDateTime(txtfrom.Text.Trim()), Convert.ToDateTime(txtto.Text.Trim()), Convert.ToDateTime(DateTime.Now.Date),txtitem.Text.ToString(),txtmodel.Text.ToString(),txtbrand.Text.ToString(),txtcat.Text.ToString());

                if (!string.IsNullOrEmpty(txtbrand.Text.Trim()))
                {
                    string soundExCodebrand = SoundEx(brand);

                    EnumerableRowCollection<DataRow> query1 = from contact in dtslowmovitems.AsEnumerable()
                                                             where SoundEx(contact.Field<string>("mi_brand")) == soundExCodebrand
                                                             select contact;

                    DataView view1 = query1.AsDataView();
                    dtslowmovitems = view1.ToTable();
                }

                if (!string.IsNullOrEmpty(txtclassific.Text.Trim()) && !chkclasific.Checked)
                {
                    DataView view2 = new DataView(dtslowmovitems);

                    view2.RowFilter = "CLASIFICATION = '" + mainclassification + "'";
                    dtslowmovitems = view2.ToTable();
                }

                if (!string.IsNullOrEmpty(txtcat.Text.Trim()))
                {
                    string soundExCodecat = SoundEx(cat);

                    EnumerableRowCollection<DataRow> query3 = from contact in dtslowmovitems.AsEnumerable()
                                                              where SoundEx(contact.Field<string>("mi_cate_1")) == soundExCodecat
                                                              select contact;

                    DataView view3 = query3.AsDataView();
                    dtslowmovitems = view3.ToTable();
                }

                if (!string.IsNullOrEmpty(txtqtylimit.Text.Trim()))
                {
                    DataView view4 = new DataView(dtslowmovitems);

                    view4.RowFilter = "sold_qty <= " + qtylimit;
                    dtslowmovitems = view4.ToTable();
                }

                //if (!string.IsNullOrEmpty(txtpercen.Text.Trim()))
                //{
                //    DataView view5 = new DataView(dtslowmovitems);

                //    view5.RowFilter = "percentage_inv_val = '" + invpercentage + "'";
                //    dtslowmovitems = view5.ToTable();
                //}

                DataView view6 = new DataView(dtslowmovitems);
                view6.Sort = "sold_qty asc";
                dtslowmovitems = view6.ToTable();

                if (!string.IsNullOrEmpty(txtnoofitems.Text.Trim()))
                {
                    Int32 dtcount = dtslowmovitems.Rows.Count;

                    if (numofitems > dtcount)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('There are no " + numofitems + " items to display !!!')", true);
                        txtnoofitems.Text = string.Empty;
                        return;
                    }
                    else
                    {
                        dtslowmovitems = SelectTopDataRow(dtslowmovitems, numofitems);
                    }
                }

                gvalldetails.DataSource = dtslowmovitems;
                gvalldetails.DataBind();

                gvorderbydetails.DataSource = new int[] { };
                gvorderbydetails.DataBind();

                RBordby.Enabled = false;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        public DataTable SelectTopDataRow(DataTable dt, int count)
        {
            DataTable dtn = dt.Clone();
            for (int i = 0; i < count; i++)
            {
                dtn.ImportRow(dt.Rows[i]);
            }

            return dtn;
        }
        static private string SoundEx(string word)
        {
            // The length of the returned code.
            int length = 4;

            // Value to return.
            string value = "";

            // The size of the word to process.
            int size = word.Length;

            // The word must be at least two characters in length.
            if (size > 1)
            {
                // Convert the word to uppercase characters.
                word = word.ToUpper(System.Globalization.CultureInfo.InvariantCulture);

                // Convert the word to a character array.
                char[] chars = word.ToCharArray();

                // Buffer to hold the character codes.
                StringBuilder buffer = new StringBuilder();
                buffer.Length = 0;

                // The current and previous character codes.
                int prevCode = 0;
                int currCode = 0;

                // Add the first character to the buffer.
                buffer.Append(chars[0]);

                // Loop through all the characters and convert them to the proper character code.
                for (int i = 1; i < size; i++)
                {
                    switch (chars[i])
                    {
                        case 'A':
                        case 'E':
                        case 'I':
                        case 'O':
                        case 'U':
                        case 'H':
                        case 'W':
                        case 'Y':
                            currCode = 0;
                            break;
                        case 'B':
                        case 'F':
                        case 'P':
                        case 'V':
                            currCode = 1;
                            break;
                        case 'C':
                        case 'G':
                        case 'J':
                        case 'K':
                        case 'Q':
                        case 'S':
                        case 'X':
                        case 'Z':
                            currCode = 2;
                            break;
                        case 'D':
                        case 'T':
                            currCode = 3;
                            break;
                        case 'L':
                            currCode = 4;
                            break;
                        case 'M':
                        case 'N':
                            currCode = 5;
                            break;
                        case 'R':
                            currCode = 6;
                            break;
                    }

                    // Check if the current code is the same as the previous code.
                    if (currCode != prevCode)
                    {
                        // Check to see if the current code is 0 (a vowel); do not process vowels.
                        if (currCode != 0)
                            buffer.Append(currCode);
                    }
                    // Set the previous character code.
                    prevCode = currCode;

                    // If the buffer size meets the length limit, exit the loop.
                    if (buffer.Length == length)
                        break;
                }
                // Pad the buffer, if required.
                size = buffer.Length;
                if (size < length)
                    buffer.Append('0', (length - size));

                // Set the value to return.
                value = buffer.ToString();
            }
            // Return the value.
            return value;
        }
        protected void gvalldetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable dtageslots = CHNLSVC.Inventory.LoadAgeSlots(Session["UserCompanyCode"].ToString());

            foreach (DataRow DDRitem in dtageslots.Rows)
            {
                Session["AGE_SLOT1"] = DDRitem["rags_slot_l1"].ToString();
                Session["AGE_SLOT2"] = DDRitem["rags_slot_l2"].ToString();
                Session["AGE_SLOT3"] = DDRitem["rags_slot_l3"].ToString();
                Session["AGE_SLOT4"] = DDRitem["rags_slot_l4"].ToString();
                Session["AGE_SLOT5"] = DDRitem["rags_slot_g1"].ToString();
            }

            string classi1 = (string)Session["AGE_SLOT1"];
            string classi2 = (string)Session["AGE_SLOT2"];
            string classi3 = (string)Session["AGE_SLOT3"];
            string classi4 = (string)Session["AGE_SLOT4"];
            string classi5 = (string)Session["AGE_SLOT5"];

            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (e.Row.Cells[9].Text.Trim() == "CL1")
                {
                    e.Row.Cells[9].Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;< " + classi1;
                }
                if (e.Row.Cells[10].Text.Trim() == "CL2")
                {
                    e.Row.Cells[10].Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;< " + classi2;
                }
                if (e.Row.Cells[11].Text.Trim() == "CL3")
                {
                    e.Row.Cells[11].Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;< " + classi3;
                }
                if (e.Row.Cells[12].Text.Trim() == "CL4")
                {
                    e.Row.Cells[12].Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;< " + classi4;
                }
                if (e.Row.Cells[13].Text.Trim() == "CL5")
                {
                    e.Row.Cells[13].Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;> " + classi5;
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvalldetails, "Select$" + e.Row.RowIndex);
                e.Row.Cells[0].Attributes["style"] = "cursor:pointer";
            }
        }

        protected void txtqtylimit_TextChanged(object sender, EventArgs e)
        {
            chkqty.Checked = false;
            lbtnsearchrec_Click(null, null);
        }

        protected void txtnoofitems_TextChanged(object sender, EventArgs e)
        {
            chknumitm.Checked = false;
            lbtnsearchrec_Click(null, null);
        }

        protected void txtbrand_TextChanged(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtbrand.Text))
            {
                return;
            }

            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtbrand.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                DisplayMessage("Invalid brand", 2);
                txtbrand.Text = string.Empty;
                txtbrand.Focus();
                return;
            }
        }
        private void DisplayMessage(String _err, Int32 option)
        {
            string Msg = _err.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");
            if (option == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + Msg + "');", true);
            }
            else if (option == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
            }
            else if (option == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + Msg + "');", true);
            }
            else if (option == 4)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + Msg + "');", true);
            }
        }
        protected void txtclassific_TextChanged(object sender, EventArgs e)
        {
            chkclasific.Checked = false;
            lbtnsearchrec_Click(null, null);
        }

        protected void txtcat_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtcat.Text))
            {
                return;
            }

            string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, "Code", "%" + txtcat.Text.Trim());

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtcat.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                DisplayMessage("Invalid category", 2);
                txtcat.Text = string.Empty;
                txtcat.Focus();
                return;
            }
        }

        protected void chkbrand_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbrand.Checked == true)
            {
                txtbrand.Text = string.Empty;
            }
        }

        protected void chkclasific_CheckedChanged(object sender, EventArgs e)
        {
            if (chkclasific.Checked == true)
            {
                txtclassific.Text = string.Empty;
            }
        }

        protected void chkcat_CheckedChanged(object sender, EventArgs e)
        {
            if (chkcat.Checked == true)
            {
                txtcat.Text = string.Empty;
            }
        }

        protected void chkqty_CheckedChanged(object sender, EventArgs e)
        {
            if (chkqty.Checked == true)
            {
                txtqtylimit.Text = string.Empty;
            }
        }

        protected void chknumitm_CheckedChanged(object sender, EventArgs e)
        {
            if (chknumitm.Checked == true)
            {
                txtnoofitems.Text = string.Empty;
            }
        }

        protected void gvalldetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                RBordby.Enabled = true;

                if (string.IsNullOrEmpty(RBordby.SelectedValue))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select order by option !!!')", true);
                    return;
                }

                string itemcode = string.Empty;

                if (gvalldetails.Rows.Count == 0)
                {
                    return;
                }
                else
                {
                    itemcode = (gvalldetails.SelectedRow.FindControl("lblitm") as Label).Text;
                }

                if (string.IsNullOrEmpty(itemcode))
                {
                    return;
                }

                string isorderbyloc = string.Empty;

                if (RBordby.SelectedValue == "2")
                {
                    isorderbyloc = Session["UserDefLoca"].ToString();
                }
                else
                {
                    isorderbyloc = string.Empty;
                }

                DataTable dtinv = CHNLSVC.Inventory.LoadInventoryDataByExpiryDate(Session["UserCompanyCode"].ToString(), itemcode, isorderbyloc);
                if (RBordby.SelectedValue == "3")
                {
                    DataView dv = dtinv.DefaultView;
                    dv.Sort = "inb_doc_dt";
                    dtinv = dv.ToTable();
                }
                int i = 0;
                dtinv.Columns.Add("EXP_DATE", typeof(string));
                foreach (var dt in dtinv.Rows)
                {
                    if (dtinv.Rows[i]["INB_EXP_DT"].ToString().Contains("0001"))
                    {
                        dtinv.Rows[i]["EXP_DATE"] = "";
                    }
                    else
                    {
                        dtinv.Rows[i]["EXP_DATE"] = dtinv.Rows[i]["INB_EXP_DT"].ToString();
                    }
                   
                    i++;
                }

                gvorderbydetails.DataSource = null;
                gvorderbydetails.DataBind();

                gvorderbydetails.DataSource = dtinv;
                gvorderbydetails.DataBind();

                dtinv.Columns["inb_loc"].ColumnName = "Location";
                dtinv.Columns["inb_doc_no"].ColumnName = "Document No";
                dtinv.Columns["inb_batch_no"].ColumnName = "Batch No";
                dtinv.Columns["inb_doc_dt"].ColumnName = "Document Date";
                Convert.ToDateTime(dtinv.Columns["inb_doc_dt"]).ToShortDateString();
                dtinv.Columns["exp_date"].ColumnName = "Expire Date";
                Convert.ToDateTime(dtinv.Columns["exp_date"]).ToShortDateString();
                dtinv.Columns["inb_qty"].ColumnName = "Qty";

                ViewState["dtinv"] = dtinv;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void RBordby_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvalldetails_SelectedIndexChanged(null, null);
        }

        protected void txtpercen_TextChanged(object sender, EventArgs e)
        {
            lbtnsearchrec_Click(null, null);
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
           string Select_company = (string)Session["SelectCompany"];
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Channel:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item2:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
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
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
               
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator + "" + seperator);
                        break;
                    }
               

                //load empty grid
                case CommonUIDefiniton.SearchUserControlType.Item_Serials:
                    {
                        paramsText.Append("-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "Loc" + seperator + "-999" + seperator + "-999" + seperator);
                        break;
                    }
               
                
                case CommonUIDefiniton.SearchUserControlType.DocNo:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + 1 + seperator + "A,F" + seperator + "GRN" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EntryNo:
                    {
                        paramsText.Append("TO,AIR,LR" + seperator + "A,F" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocWiseBalancePOBL:
                    {
                        paramsText.Append(1 + seperator + "A,F" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocWiseBalanceDocGRN:
                    {
                        paramsText.Append("1" + seperator + "A,F" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocWiseBalanceEntry:
                    {
                        paramsText.Append("" + seperator + "" + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }
        protected void lBtnBrand_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Brand";
                Session["Brand"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["Brand"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                Label2.Text = "Brand";
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }
        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            this.cmbSearchbykey.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.cmbSearchbykey.Items.Add(col.ColumnName);
            }

            this.cmbSearchbykey.SelectedIndex = 0;
        }

        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            if (Label2.Text == "Item")
            {
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                dgvResultItem.DataSource = _result;
                dgvResultItem.DataBind();
                ItemPopup.Show();
                Session["Item"] = _result;
                return;
            }
            if (Label2.Text == "Item2")
            {
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item2);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                dgvResultItem.DataSource = _result;
                dgvResultItem.DataBind();
                ItemPopup.Show();
                Session["Item2"] = _result;
                return;
            }
            if (Label2.Text == "Item3")
            {
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item2);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                dgvResultItem.DataSource = _result;
                dgvResultItem.DataBind();
                ItemPopup.Show();
                Session["Item3"] = _result;
                return;
            }
            if (Label2.Text == "Channel2")
            {
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Channel);
                DataTable _result = CHNLSVC.CommonSearch.GetChanalDetailsNew(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                dgvResultItem.DataSource = _result;
                dgvResultItem.DataBind();
                ItemPopup.Show();
                Session["Channel2"] = _result;
                return;
            }
            if (Label2.Text == "locationall")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                dgvResultItem.DataSource = result;
                dgvResultItem.DataBind();
                ItemPopup.Show();
                Session["locationall"] = result;
                return;
            }
            if (Label2.Text == "Model")
            {
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                DataTable _result = CHNLSVC.CommonSearch.GetAllModels(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                dgvResultItem.DataSource = _result;
                dgvResultItem.DataBind();
                ItemPopup.Show();
                Session["Model"] = _result;
                return;
            }
            if (Label2.Text == "Brand")
            {
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                dgvResultItem.DataSource = _result;
                dgvResultItem.DataBind();
                ItemPopup.Show();
                Session["Brand"] = _result;
                return;
            }
            if (Label2.Text == "CAT_Main")
            {
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                dgvResultItem.DataSource = _result;
                dgvResultItem.DataBind();
                ItemPopup.Show();
                Session["CAT_Main"] = _result;
                return;
            }
            if (Label2.Text == "CAT_Sub1")
            {
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                string valuetest = txtSearchbyword.Text;
                valuetest = (valuetest == "") ? valuetest : "%" + valuetest;
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, cmbSearchbykey.SelectedValue, valuetest);
                dgvResultItem.DataSource = _result;
                dgvResultItem.DataBind();
                ItemPopup.Show();
                Session["CAT_Sub1"] = _result;
                return;
            }
            if (Label2.Text == "CAT_Sub2")
            {
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                string valuetest = txtSearchbyword.Text;
                valuetest = (valuetest == "") ? valuetest : "%" + valuetest;
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, cmbSearchbykey.SelectedValue, valuetest);
                dgvResultItem.DataSource = _result;
                dgvResultItem.DataBind();
                ItemPopup.Show();
                Session["CAT_Sub2"] = _result;
                return;
            }
            if (Label2.Text == "CAT_Sub3")
            {
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub3);
                DataTable _result = CHNLSVC.General.GetItemSubCat4(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                dgvResultItem.DataSource = _result;
                dgvResultItem.DataBind();
                ItemPopup.Show();
                Session["CAT_Sub3"] = _result;
                return;
            }
            if (Label2.Text == "CAT_Sub4")
            {
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub4);
                DataTable _result = CHNLSVC.General.GetItemSubCat5(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                dgvResultItem.DataSource = _result;
                dgvResultItem.DataBind();
                ItemPopup.Show();
                Session["CAT_Sub4"] = _result;
                return;
            }
            if (Label2.Text == "Company")
            {
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanySearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                dgvResultItem.DataSource = _result;
                dgvResultItem.DataBind();
                ItemPopup.Show();
                Session["Company"] = _result;
                return;
            }
            if (Label2.Text == "InvTrcChnl")
            {
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvTrcChnl);
                DataTable _result = CHNLSVC.CommonSearch.GetInventoryTrackeChannel(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                dgvResultItem.DataSource = _result;
                dgvResultItem.DataBind();
                ItemPopup.Show();
                Session["InvTrcChnl"] = _result;
                return;
            }
            if (Label2.Text == "Loc_HIRC_Location")
            {
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                dgvResultItem.DataSource = _result;
                dgvResultItem.DataBind();
                ItemPopup.Show();
                Session["Loc_HIRC_Location"] = _result;
                return;
            }
            if (Label2.Text == "UserLocation")
            {
                string para = "";
                //_CommonSearch.ReturnIndex = 0;

                DataTable _result = new DataTable();
                //----------------------------------------------------------------------------
                Boolean allow_WHAREHOUSE = CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), string.Empty, "INVAC");
                DataTable _result2 = null;
                DataTable _dtGetLoc = new DataTable();
                if (allow_WHAREHOUSE == true)
                {
                    para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.WHAREHOUSE);
                    _result2 = CHNLSVC.Inventory.GetLocationByType(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    _dtGetLoc.Merge(_result2);
                }

                para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                _dtGetLoc.Merge(CHNLSVC.CommonSearch.GetUserLocationSearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text)); dgvResultItem.DataSource = _result;

                DataTable _newDt = new DataTable();
                _newDt.Columns.Add("Code");
                _newDt.Columns.Add("Description");
                foreach (DataRow dr in _dtGetLoc.Rows)
                {
                    DataRow _newDataRow = _newDt.NewRow();
                    _newDataRow["Code"] = dr["Code"].ToString();
                    _newDataRow["Description"] = dr["Description"].ToString();
                    String author = dr["Code"].ToString();
                    bool contains = _newDt.AsEnumerable().Any(row => author == row.Field<String>("Code"));
                    if (!contains)
                    {
                        _newDt.Rows.Add(_newDataRow);
                    }
                }

                _result = _newDt;
                dgvResultItem.DataSource = _result;
                dgvResultItem.DataBind();
                ItemPopup.Show();
                Session["UserLocation"] = _result;
                return;

            }
            if (Label2.Text == "Channel")
            {
                string para = "";
                //_CommonSearch.ReturnIndex = 0;

                DataTable _result = new DataTable();
                //----------------------------------------------------------------------------
                Boolean allow_WHAREHOUSE = CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), string.Empty, "INVAC");
                DataTable _result2 = null;
                if (allow_WHAREHOUSE == true)
                {
                    para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.WHAREHOUSE);
                    _result2 = CHNLSVC.Inventory.GetLocationByType(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    _result.Merge(_result2);
                }

                para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                _result.Merge(CHNLSVC.CommonSearch.GetUserLocationSearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text)); dgvResultItem.DataSource = _result;
                dgvResultItem.DataSource = _result;
                dgvResultItem.DataBind();
                ItemPopup.Show();
                Session["Channel"] = _result;
                return;
            }


            if (Label2.Text == "ProdcutDetailsItem")
            {
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                dgvResultItem.DataSource = _result;
                dgvResultItem.DataBind();
                ItemPopup.Show();
                Session["ProdcutDetailsItem"] = _result;
                return;

            }

            else if (Label2.Text == "ProdcutDetailsCompanyCode")
            {
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanySearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                dgvResultItem.DataSource = _result;
                dgvResultItem.DataBind();
                ItemPopup.Show();
                Session["ProdcutDetailsCompanyCode"] = _result;
                return;
            }

            else if (Label2.Text == "BalancesPOorBLNo")
            {
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.POorBLNo);
                DataTable _result = CHNLSVC.General.Get_PoBlNumber(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                dgvResultItem.DataSource = _result;
                dgvResultItem.DataBind();
                ItemPopup.Show();
                Session["BalancesPOorBLNo"] = _result;
                return;
            }
        }
        protected void dgvResultItem_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvResultItem.PageIndex = e.NewPageIndex;
                DataTable _result = null;
                if (lblSearchType.Text == "Item")
                {
                    _result = (DataTable)Session["Item"];
                }
                if (lblSearchType.Text == "Item2")
                {
                    _result = (DataTable)Session["Item2"];
                }
                if (lblSearchType.Text == "Item3")
                {
                    _result = (DataTable)Session["Item3"];
                }
                if (lblSearchType.Text == "Channel2")
                {
                    _result = (DataTable)Session["Channel2"];
                }
                if (lblSearchType.Text == "locationall")
                {
                    _result = (DataTable)Session["locationall"];
                }
                //added by kelum : set prdcut details item code : 2016-May-24
                if (lblSearchType.Text == "ProdcutDetailsItem")
                {
                    _result = (DataTable)Session["ProdcutDetailsItem"];
                }

                //added by kelum : set balances PO or BL number : 2016-june-02

                if (lblSearchType.Text == "BalancesPOorBLNo")
                {
                    _result = (DataTable)Session["BalancesPOorBLNo"];
                }
                //
                //added by kelum : set balances Doc GRN number : 2016-june-02

                if (lblSearchType.Text == "BalancesDocGrnNo")
                {
                    _result = (DataTable)Session["BalancesDocGrnNo"];
                }

                //added by kelum : set balances entry number : 2016-june-02

                if (lblSearchType.Text == "BalancesEntryNo")
                {
                    _result = (DataTable)Session["BalancesEntryNo"];
                }

                if (lblSearchType.Text == "Model")
                {
                    _result = (DataTable)Session["Model"];
                }
                if (lblSearchType.Text == "Model2")
                {
                    _result = (DataTable)Session["Model2"];
                }
                if (lblSearchType.Text == "Brand")
                {
                    _result = (DataTable)Session["Brand"];
                }
                if (lblSearchType.Text == "Brand2")
                {
                    _result = (DataTable)Session["Brand2"];
                }
                if (lblSearchType.Text == "CAT_Main")
                {
                    _result = (DataTable)Session["CAT_Main"];
                }
                if (lblSearchType.Text == "CAT_Main2")
                {
                    _result = (DataTable)Session["CAT_Main2"];
                }
                if (lblSearchType.Text == "CAT_Sub1")
                {
                    _result = (DataTable)Session["CAT_Sub1"];
                }
                if (lblSearchType.Text == "CAT_Sub12")
                {
                    _result = (DataTable)Session["CAT_Sub12"];
                }
                if (lblSearchType.Text == "CAT_Sub2")
                {
                    _result = (DataTable)Session["CAT_Sub2"];
                }
                if (lblSearchType.Text == "CAT_Sub22")
                {
                    _result = (DataTable)Session["CAT_Sub22"];
                }
                if (lblSearchType.Text == "Tobond_bl")
                {
                    _result = (DataTable)Session["Tobond_bl"];
                }
                if (lblSearchType.Text == "tobond")
                {
                    _result = (DataTable)Session["tobond"];
                }
                if (lblSearchType.Text == "CAT_Sub3")
                {
                    _result = (DataTable)Session["CAT_Sub3"];
                }
                if (lblSearchType.Text == "CAT_Sub4")
                {
                    _result = (DataTable)Session["CAT_Sub4"];
                }
                if (lblSearchType.Text == "Company")
                {
                    _result = (DataTable)Session["Company"];
                }
                if (lblSearchType.Text == "UserLocation")
                {
                    _result = (DataTable)Session["UserLocation"];
                }
                if (lblSearchType.Text == "Loc_HIRC_Location")
                {
                    _result = (DataTable)Session["Loc_HIRC_Location"];
                }
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;

                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = string.Empty;
                txtSearchbyword.Focus();
                ItemPopup.Show();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }
        protected void dgvResultItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Session["SELECTEDITEM"] = null;
                Session["SELECTEDMODEL"] = null;
                Session["SELECTEDBRAND"] = null;
                Session["SELECTEDCAT_MAIN"] = null;
                Session["SELECTEDCAT_SUB1"] = null;
                Session["SELECTEDCAT_SUB2"] = null;
                Session["SELECTEDCOMPANY"] = null;
                Session["SELECTEDUSERLOCATION"] = null;
                Session["SELECTEDCHANNELLOCATION"] = null;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript",
                    "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                string code = dgvResultItem.SelectedRow.Cells[1].Text;

               
              
                if (lblSearchType.Text == "Brand")
                {
                    Session["SELECTEDBRAND"] = code;
                    txtbrand.Text = code;
                    //btnLoad_Click(null, null);
                }
                if (lblSearchType.Text == "CAT_Main")
                {
                    Session["SELECTEDBRAND"] = code;
                    txtcat.Text = code;
                    //btnLoad_Click(null, null);
                }
                if (lblSearchType.Text == "Item")
                {
                    Session["SELECTEDITEMCODE"] = code;
                    txtitem.Text = code;
                    //btnLoad_Click(null, null);
                }
                if (lblSearchType.Text == "Model")
                {
                    Session["SELECTEDMODEL"] = code;
                    txtmodel.Text = code;
                    //btnLoad_Click(null, null);
                }
              
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }
        protected void ImageSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable _result = new DataTable();
                Session["Item"] = null;
                Session["Item2"] = null;
                Session["Model"] = null;
                Session["Model2"] = null;
                Session["Brand"] = null;
                Session["Brand2"] = null;
                Session["CAT_Main"] = null;
                Session["CAT_Main2"] = null;
                Session["CAT_Sub1"] = null;
                Session["CAT_Sub12"] = null;
                Session["CAT_Sub2"] = null;
                Session["CAT_Sub22"] = null;
                Session["Tobond_bl"] = null;
                Session["tobond"] = null;
                Session["CAT_Sub3"] = null;
                Session["CAT_Sub4"] = null;
                Session["Company"] = null;
                Session["InvTrcChnl"] = null;
                Session["Loc_HIRC_Location"] = null;
                Session["UserLocation"] = null;
                Session["Channel"] = null;

                // Added by Kelum
                Session["ProdcutDetailsItem"] = null;
                Session["ProdcutDetailsCompanyCode"] = null;
                //

                 if (lblSearchType.Text == "Brand")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                    _result = CHNLSVC.CommonSearch.GetItemBrands(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Brand"] = _result;
                }

                 if (lblSearchType.Text == "Item")
                 {
                     string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                     _result = CHNLSVC.CommonSearch.GetItemSearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                     Session["Item"] = _result;
                 }
                 if (lblSearchType.Text == "Model")
                 {
                     string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                     _result = CHNLSVC.CommonSearch.GetAllModels(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                     Session["Model"] = _result;
                 }
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                }
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                dgvResultItem.DataBind();
                ItemPopup.Show();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void lbtncategory_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "CAT_Main";
                Session["CAT_Main"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["CAT_Main"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                Label2.Text = "CAT_Main";
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void lbtnageview_Click(object sender, EventArgs e)
        {
            try
            {
               GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
               Label lblitm = row.FindControl("lblitm") as Label;

               Label lb90val = row.FindControl("lb90val") as Label;
               Label lb120val = row.FindControl("lb120val") as Label;
               Label lb170val = row.FindControl("lb170val") as Label;
               Label lb270val = row.FindControl("lb270val") as Label;
               Label lb365val = row.FindControl("lb365val") as Label;
               Label lbavgval = row.FindControl("lbavgval") as Label;
               
               DateTime fromdate = Convert.ToDateTime(txtfrom.Text.ToString());
               DateTime todate = Convert.ToDateTime(txtto.Text.ToString());
               string company = Session["UserCompanyCode"].ToString();
               DataTable dt = CHNLSVC.Inventory.GET_AGE_DETAILS(company, lblitm.Text.ToString(),fromdate,todate,0,90,"");
               DataTable dt2 = CHNLSVC.Inventory.GET_AGE_DETAILS(company, lblitm.Text.ToString(), fromdate, todate, 90, 120,"");
               DataTable dt3 = CHNLSVC.Inventory.GET_AGE_DETAILS(company, lblitm.Text.ToString(), fromdate, todate, 120, 170,"");
               DataTable dt4 = CHNLSVC.Inventory.GET_AGE_DETAILS(company, lblitm.Text.ToString(), fromdate, todate, 170, 270,"");
               DataTable dt5 = CHNLSVC.Inventory.GET_AGE_DETAILS(company, lblitm.Text.ToString(), fromdate, todate, 270, 2000,"");
              // DataTable avg = CHNLSVC.Inventory.GET_AGE_DETAILS(company, lblitm.Text.ToString(), fromdate, todate, 270, 365, "AVG");

               decimal average = Convert.ToInt16(dt.Rows[0][0].ToString() == "" ? "0" : dt.Rows[0][0].ToString()) + Convert.ToInt16(dt2.Rows[0][0].ToString() == "" ? "0" : dt2.Rows[0][0].ToString()) + Convert.ToInt16(dt3.Rows[0][0].ToString() == "" ? "0" : dt3.Rows[0][0].ToString()) + Convert.ToInt16(dt4.Rows[0][0].ToString() == "" ? "0" : dt4.Rows[0][0].ToString()) + Convert.ToInt16(dt5.Rows[0][0].ToString() == "" ? "0" : dt5.Rows[0][0].ToString());
               average =Math.Round( average / 5);

               /*
              lb90.Visible = true;
              lb120.Visible = true;
              lb170.Visible = true;
              lb270.Visible = true;
              lb365.Visible = true;
              lbavg.Visible = true;
                                 * */
               lb90val.Text = dt.Rows[0][0].ToString();
               lb120val.Text = dt2.Rows[0][0].ToString();
               lb170val.Text = dt3.Rows[0][0].ToString();
               lb270val.Text = dt4.Rows[0][0].ToString();
               lb365val.Text = dt5.Rows[0][0].ToString();
               lbavgval.Text = average.ToString();//avg.Rows[0][0].ToString();
            }
            catch(Exception ex)
            {
                DisplayMessage(ex.Message,2);
            }
        }

        protected void txtitem_TextChanged(object sender, EventArgs e)
        {

        }

        protected void lbtnitemsearch_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Item";
                Session["Item"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["Item"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                Label2.Text = "Item";
                ItemPopup.Show();
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 2);
            }
        }

        protected void chkitemall_CheckedChanged(object sender, EventArgs e)
        {
            if (chkitemall.Checked)
            {
                txtitem.Text = "";
            }
        }

        protected void txtmodel_TextChanged(object sender, EventArgs e)
        {

        }

        protected void chkmodelall_CheckedChanged(object sender, EventArgs e)
        {
            if (chkmodelall.Checked)
            {
                txtmodel.Text = "";
            }
        }

        protected void chkmodelall_CheckedChanged1(object sender, EventArgs e)
        {

        }

        protected void lbtnmodel_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Model";
                Session["Model"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                DataTable _result = CHNLSVC.CommonSearch.GetAllModels(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["Model"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                Label2.Text = "Model";
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 2);
            }
        }

        protected void lbtnExcelUpload_Click(object sender, EventArgs e)
        {
            if (hfexceldownload.Value == "Yes")
            {
                DataTable excelData = new DataTable();
                excelData = ViewState["dtinv"] as DataTable;
                excelData.Columns.Remove("INB_EXP_DT");

                //Convert.ToDateTime(excelData.Columns["Document Date"]).ToShortDateString();
                //Convert.ToDateTime(excelData.Columns["Expire Date"]).ToShortDateString();

                string _err = "";
                string userId = Session["UserID"].ToString();
                string company = Session["UserCompanyCode"].ToString();
                string path = CHNLSVC.MsgPortal.GetSlowMovingInventry(company, userId, Session["UserCompanyName"].ToString(), excelData, out _err);
                _copytoLocal(path);
                string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".xlsx','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
            }
            else return;
        }

        private void _copytoLocal(string _filePath)
        {
            string filenamenew = Session["UserID"].ToString();
            string name = _filePath;
            FileInfo file = new FileInfo(name);
            if (file.Exists)
            {
                string targetFileName = Server.MapPath("~\\Temp\\") + filenamenew + ".xlsx";
                System.IO.File.Copy(@"" + _filePath, targetFileName, true);
            }
            else
            {
                return;
            }
        }
    }
}