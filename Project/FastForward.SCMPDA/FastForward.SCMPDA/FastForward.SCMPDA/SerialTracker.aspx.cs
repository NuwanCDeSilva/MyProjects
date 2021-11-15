using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMPDA
{
    public partial class SerialTracker : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnback_Click(object sender, EventArgs e)
        {

            try
            {
                string _selectedjob = string.Empty;
                string docdirection = (string)Session["DOCDIRECTION"];
                string passvalue = "";
                if (Request.QueryString["docno"] != null)
                {
                    passvalue = Request.QueryString["docno"].ToString();
                }
                if (!string.IsNullOrEmpty(passvalue))
                {
                    if (docdirection == "1")
                    {
                        Response.Redirect("CreateJobNumber.aspx?JobNo=" + passvalue, false);
                    }
                    else
                    {
                        if (Session["UserCompanyName"].ToString() == "AAL")
                        {
                            Response.Redirect("CreateOutJobNew.aspx?JobNo=" + passvalue, false);

                        }
                        else
                        {
                            Response.Redirect("CreateOutJob.aspx?JobNo=" + passvalue, false);

                        }
                    }
                }
                else
                {
                    Response.Redirect("ScanItem.aspx", false);
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        protected void lbtnalert_Click(object sender, EventArgs e)
        {
            try
            {
                divalert.Visible = false;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = ex.Message.ToString();
            }
        }
        protected void btnCheck_Click(object sender, EventArgs e)
        {
            try
            {
                string error = "";
                if (txtserialnumber1.Text != "" || txtserialnumber2.Text != "")
                {
                    divalert.Visible = false;
                    string company = Session["UserCompanyCode"].ToString();
                    DataTable ser1Data = CHNLSVC.Inventory.getSerialDetailsStatus(txtserialnumber1.Text.ToString(), txtserialnumber2.Text.ToString(), company, out error);
                    if (ser1Data == null || ser1Data.Rows.Count == 0)
                    {
                        serdetailsdiv.Visible = false;
                        detailsdiv.Visible = true;
                        invalSer.Text = "No any details found for Serial #1 or 2.";
                    }
                    else
                    {
                        txtserialnumber1.Text = "";
                        txtserialnumber2.Text = "";
                        serdetailsdiv.Visible = true;
                        detailsdiv.Visible = false;
                        lblItemCd.Text = ser1Data.Rows[0]["INS_ITM_CD"].ToString();
                        lblLocation.Text = ser1Data.Rows[0]["INS_LOC"].ToString();
                        lblDesc.Text = ser1Data.Rows[0]["ML_LOC_DESC"].ToString();
                        lblSer1.Text = (ser1Data.Rows[0]["INS_SER_1"] != DBNull.Value) ? ser1Data.Rows[0]["INS_SER_1"].ToString() : "";
                        lblSer2.Text = (ser1Data.Rows[0]["INS_SER_2"]!=DBNull.Value) ? ser1Data.Rows[0]["INS_SER_2"].ToString() : "-";
                        lblScnStus.Text = (ser1Data.Rows[0]["INS_AVAILABLE"].ToString() == "1") ? "Available" : "Already Scanned";
                    }
                }
                else
                {
                    serdetailsdiv.Visible = false;
                    detailsdiv.Visible = false;
                    divalert.Visible = true;
                    lblalert.Text = "Please scan serial #1 or 2.";
                    txtserialnumber1.Focus();
                    txtserialnumber2.Text = "";
                    return;

                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = ex.Message.ToString();
            }
        }
    }

}
