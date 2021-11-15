using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FF.BusinessObjects;

namespace FF.AbansTours.UserControls
{
    public partial class uc_HPReminder : System.Web.UI.UserControl
    {
        #region properties

        public string Acc_no {
            get { return (string)ViewState["acc_no"]; }
            set { ViewState["acc_no"] = value; }
        }


        public AjaxControlToolkit.ModalPopupExtender UCModalPopupExtender
        {
            get { return this.ModalPopupExtender1; }
        }

        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGrid();
            }
        }

        public void LoadGrid()
        {
            BasePage _basepage = new BasePage();
            //List<HPReminder> rmdList = _basepage.CHNLSVC.Sales.GetReminders(Acc_no, "A", _basepage.GlbUserComCode, _basepage.GlbUserDefProf, "Date", DateTime.Now.Date);
            //if (rmdList != null)
            //{
            //    GridViewReminder.DataSource = rmdList;
            //    GridViewReminder.DataBind();
            //    ModalPopupExtender1.Show();
            //}
            //else
            //{
            //    DataTable dt = new DataTable();
            //    GridViewReminder.DataSource = dt;
            //    GridViewReminder.DataBind();
            //    ModalPopupExtender1.Hide();
            //}


        }

        //public bool HasReminders() { 
        // BasePage _basepage = new BasePage();
        //    List<HPReminder> rmdList =_basepage.CHNLSVC.Sales.GetReminders(Acc_no,"A", _basepage.GlbUserComCode,_basepage.GlbUserDefProf,"Date",DateTime.Now.Date);
        //    if (rmdList != null)
        //}

        protected void ButtonAccept_Click(object sender, EventArgs e)
        {
            BasePage _basepage = new BasePage();
            foreach (GridViewRow gr in GridViewReminder.Rows)
            {
                CheckBox chk = (CheckBox)gr.FindControl("chekPc");
                if (chk.Checked)
                {
                    HPReminder _rmd = new HPReminder();
                    _rmd.Hra_seq = Convert.ToInt32(GridViewReminder.DataKeys[gr.RowIndex][0].ToString());
                    _rmd.Hra_stus = "C";
                    _rmd.Hra_stus_dt = DateTime.Now;
                    _rmd.Hra_mod_by = _basepage.GlbUserName;

                    //update process
                   // int result = _basepage.CHNLSVC.Sales.UpdateHPReminder(_rmd);
                    //if (result > 0)
                    {
                        string Msg = "<script>alert('Record Updated Sucessfully!!');</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                    }
                }
            }
        }

    }
}