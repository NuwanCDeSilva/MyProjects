using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using System.Data;
using System.Transactions;
using System.Text;
using System.Drawing;

namespace FF.WebERPClient.General_Modules
{
    public partial class VehicleReleaseToHPAccHolder : BasePage
    {

        public List<ReptPickSerials> ApproveList
        {
            get { return (List<ReptPickSerials>)ViewState["ApproveList"]; }
            set { ViewState["ApproveList"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                try
                {

                    grvInvoiceDet.DataSource = new List<VehicalRegistration>();
                    grvInvoiceDet.DataBind();

                    ApproveList = new List<ReptPickSerials>();
                    DataTable dt = CHNLSVC.General.Get_RequiredDocuments("VHREG");
                    if (dt != null)
                    {
                        grv_docRequired.DataSource = dt;
                        grv_docRequired.DataBind();

                    }


                }
                catch (Exception er)
                {
                    //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, er.Message);
                    string Msg = "<script>alert('System Error Occur. : " + er.Message + "');window.location ='General_Modules/VehicleReleaseToHPAccHolder.aspx-(Page_Load)';</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

                    return;
                }



            }
        }

        protected void btnInvOk_Click(object sender, EventArgs e)
        {

            try
            {
                #region
                List<VehicalRegistration> listofInvoice = new List<VehicalRegistration>();
                // listofInvoice= CHNLSVC.General.GetVehiclesByInvoiceNo(GlbUserComCode, GlbUserDefProf, txtInvoiceNum.Text.Trim());
                List<FF.BusinessObjects.VehicalRegistration> vehRegList = CHNLSVC.General.GetVehiclesByInvoiceNo(GlbUserComCode, GlbUserDefProf, txtInvoiceNum.Text.Trim());
                grvInvoiceDet.DataSource = vehRegList;
                grvInvoiceDet.DataBind();
                if (vehRegList != null && vehRegList.Count > 0)
                {
                    foreach (FF.BusinessObjects.VehicalRegistration veh in vehRegList)
                    {
                        //List<ReptPickSerials> rpsList = CHNLSVC.General.getserialByInvoiceNum(veh.P_srvt_ref_no, GlbUserComCode);
                        List<ReptPickSerials> rpsList = CHNLSVC.General.getserialByInvoiceNum(veh.P_svrt_inv_no, GlbUserComCode);
                        foreach (ReptPickSerials rp in rpsList)
                        {
                            rp.Tus_base_doc_no = veh.P_srvt_ref_no;
                            
                        }
                        ApproveList.AddRange(rpsList);
                    }

                    if (vehRegList != null && vehRegList.Count > 0)
                    {
                        string custCD = vehRegList[0].P_svrt_cust_cd;
                        string custName = vehRegList[0].P_svrt_full_name;
                        string insuranceNo = vehRegList[0].P_srvt_insu_ref;
                        lblCustCD.Text = custCD;
                        lblCustName.Text = custName;
                        lblInsuranceNo.Text = insuranceNo;
                        lblInvDt.Text = vehRegList[0].P_svrt_dt.Date.ToString();
                    }
                }
                #endregion
            }
            catch (Exception er)
            {
                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, er.Message);
                string Msg = "<script>alert('System Error Occur. : " + er.Message + "');window.location = 'General_Modules/VehicleReleaseToHPAccHolder.aspx-(btnInvOk_Click)';</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                return;
            }

        }

        protected void linkBtnRefNum_Click(object sender, EventArgs e)
        {
        }

        protected void grvInvoiceDet_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //int row_id = e.RowIndex;
            //string docNo = Convert.ToString(grvInvoiceDet.DataKeys[row_id]["P_srvt_ref_no"]);
            ////getserialByInvoiceNum
            //List<ReptPickSerials> rpsList = CHNLSVC.General.getserialByInvoiceNum(docNo, GlbUserComCode);

            //ApproveList.AddRange(rpsList);

            //grvVehicleDet.DataSource = rpsList;
            //grvVehicleDet.DataBind();


            //// ReptPickSerials rps = new ReptPickSerials();
            // //rps.Tus_ser_1;
            // //rps.Tus_ser_2;
            // //rps.
            // //rps.Tus_doc_no
            // // rps.Tus_usrseq_no;

        }

        protected void btn_Clear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/General_Modules/VehicleReleaseToHPAccHolder.aspx");
        }

        protected void btn_CANCEL_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }

        protected void btn_APPROVE_Click(object sender, EventArgs e)
        {
            try
            {
                #region
                foreach (GridViewRow row in grvInvoiceDet.Rows)
                {
                    //string firstColValue = row.cells[0].Text;

                    LinkButton refNo = (LinkButton)row.FindControl("linkBtnRefNum");
                    string engineNo = row.Cells[4].Text;
                    string chasseNo = row.Cells[5].Text;

                    string docNo = refNo.Text.Trim();
                    CheckBox chekbox = (CheckBox)row.FindControl("chekApprove");
                    if (chekbox.Checked == false)
                    {
                        // List<ReptPickSerials> rpsList = CHNLSVC.General.getserialByInvoiceNum(docNo, GlbUserComCode);
                        // ApproveList.AddRange(rpsList);
                        ApproveList.RemoveAll(x => x.Tus_ser_1 == engineNo && x.Tus_ser_2 == chasseNo);
                        //ApproveList.RemoveAll(x => x.Tus_base_doc_no == refNo && x.Tus_ser_1=);
                    }
                    else
                    { 
                    
                    }
                }
                //save approvelist.


                Int32 effect = CHNLSVC.General.Update_ListVehicleApproveStatus(GlbUserComCode, ApproveList, DateTime.Now.Date, GlbUserName,txtInvoiceNum.Text.Trim());
                if (effect > 0)
                {
                    string Msg_ = "<script>alert(' Items approved successfully!' );</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg_, false);
                }
                if (effect == 0)
                {
                    string Msg_ = "<script>alert('No items approved!' );</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg_, false);
                }
                #endregion

            }
            catch (Exception er)
            {
                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, er.Message);
                string Msg = "<script>alert('System Error Occur. : " + er.Message + "');window.location = 'General_Modules/VehicleReleaseToHPAccHolder.aspx-(btn_APPROVE_Click)';</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                return;
            }

        }

        protected void chekApprove_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void grvVehicleDet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ////CheckBox chekbox =(e.Row.Cells[4]);
            //CheckBox chekbox = (CheckBox)e.Row.FindControl("chekApprove");
            ////Label chekbox = (Label)e.Row.FindControl("");
            //foreach(ReptPickSerials rps in ApproveList)
            //{
            //    if (rps.Tus_isapp == 1)
            //    {
            //        chekbox.Checked = true;
            //    }
            //    else
            //    {
            //        chekbox.Checked = false;
            //    }
            //}

        }

        protected void grvVehicleDet_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int row_id = e.RowIndex;
            Int32 seq = (Int32)grvVehicleDet.DataKeys[row_id][0];
            string engineNo = (string)grvVehicleDet.DataKeys[row_id][1];
            string chasseNo = (string)grvVehicleDet.DataKeys[row_id][2];
            ReptPickSerials rps = (ReptPickSerials)grvVehicleDet.SelectedValue;

            //CheckBox chekbox = (CheckBox)grvVehicleDet.SelectedRow.FindControl("chekApprove");
            //if (chekbox.Checked)
            //{
            //    chekbox.Checked = false;
            //    foreach(ReptPickSerials ser in ApproveList)
            //    {
            //        if (ser.Tus_usrseq_no == seq && ser.Tus_ser_1==engineNo&& ser.Tus_ser_2==chasseNo)
            //        {
            //            ser.Tus_isapp = 0;
            //        }
            //    }

            //}
            //else
            //{
            //    chekbox.Checked = true;
            //    foreach (ReptPickSerials ser in ApproveList)
            //    {
            //        if (ser.Tus_usrseq_no == seq && ser.Tus_ser_1 == engineNo && ser.Tus_ser_2 == chasseNo)
            //        {
            //            ser.Tus_isapp = 0;
            //        }
            //    }

            //}
            //Defect_List.RemoveAll(x => x.Srd_job_defc_tp == _defectType && x.Srd_job_defc_rmk == _defectRemarks);

        }

        protected void grvSerchResults_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                GridViewRow row = grvSerchResults.SelectedRow;
                string InvoiceNo = row.Cells[1].Text.Trim();
                txtInvoiceNum.Text = InvoiceNo;
                btnInvOk_Click(this, null);

            }
            catch (Exception er)
            {
                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, er.Message);
                string Msg = "<script>alert('System Error Occur. : " + er.Message + "');window.location = 'General_Modules/VehicleReleaseToHPAccHolder.aspx-(grvSerchResults_SelectedIndexChanged)';</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                return;
            }


        }

        protected void btnSearchOk_Click(object sender, EventArgs e)
        {
            try
            {

                #region
                DataTable dt = new DataTable();
                if (ddlSerachBy.SelectedValue == "Account No.")
                {
                    if (txtInvSerch.Text.Trim() == "")
                    {
                        lblSearchMsg.Text = "Please enter Account # to search!";
                        return;
                    }
                    else
                    {
                        lblSearchMsg.Text = "";
                        //search on Acc no

                        dt = CHNLSVC.General.SearchInvoiceNo_forVehicle(GlbUserComCode, GlbUserDefProf, txtInvSerch.Text.Trim(), null, null);

                    }
                }
                if (ddlSerachBy.SelectedValue == "Registration No.")
                {
                    if (txtInvSerch.Text.Trim() == "")
                    {
                        lblSearchMsg.Text = "Please enter Registration # to search!";
                        return;
                    }
                    else
                    {
                        lblSearchMsg.Text = "";
                        //search on Reg no
                        dt = CHNLSVC.General.SearchInvoiceNo_forVehicle(GlbUserComCode, GlbUserDefProf, null, txtInvSerch.Text.Trim(), null);
                    }
                }
                if (ddlSerachBy.SelectedValue == "Invoice Date")
                {
                    if (txtInvSerch.Text.Trim() == "")
                    {
                        lblSearchMsg.Text = "Please enter a Date to search!";
                        return;
                    }
                    else
                    {
                        lblSearchMsg.Text = "";
                        //search on Invoice Date
                        try
                        {
                            string invDate = Convert.ToDateTime(txtInvSerch.Text.Trim()).ToShortDateString();
                        }
                        catch (Exception ex)
                        {

                            lblSearchMsg.Text = "Invalid Date Format!";

                            return;
                        }

                        string invoiceDate = Convert.ToDateTime(txtInvSerch.Text.Trim()).ToShortDateString();
                        dt = CHNLSVC.General.SearchInvoiceNo_forVehicle(GlbUserComCode, GlbUserDefProf, null, null, invoiceDate);

                    }

                }
                
                grvSerchResults.DataSource = dt;
                grvSerchResults.DataBind();
                ModalPopupExtSearch.Show();
                #endregion
            }
            catch (Exception er)
            {
                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, er.Message);
                string Msg = "<script>alert('System Error Occur. : " + er.Message + "');window.location = 'General_Modules/VehicleReleaseToHPAccHolder.aspx-(btnSearchOk_Click)';</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                return;
            }

        }

        protected void imgbtnSearch_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ModalPopupExtSearch.Show();
            }
            catch (Exception er)
            {
                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, er.Message);
                string Msg = "<script>alert('System Error Occur. : " + er.Message + "');window.location = 'General_Modules/VehicleReleaseToHPAccHolder.aspx-(imgbtnSearch_Click)';</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                return;
            }


        }

        protected void grv_docRequired_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           

            try
            {
                #region
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label isMand = (Label)e.Row.FindControl("lbl_IsMand");
                    isMand.Visible = false;
                    Label star = (Label)e.Row.FindControl("lbl_IsMand");

                    if (isMand.Text == "1")
                    {
                        star.Visible = true;
                    }
                    else
                    {
                        star.Visible = false;
                    }

                }
                #endregion
            }
            catch (Exception er)
            {
                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, er.Message);
                string Msg = "<script>alert('System Error Occur. : " + er.Message + "');window.location = 'General_Modules/VehicleReleaseToHPAccHolder.aspx-(grv_docRequired_RowDataBound)';</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                return;
            }


           

        }

        protected void txtInvoiceNum_TextChanged(object sender, EventArgs e)
        {
            
            
        }
    }
}