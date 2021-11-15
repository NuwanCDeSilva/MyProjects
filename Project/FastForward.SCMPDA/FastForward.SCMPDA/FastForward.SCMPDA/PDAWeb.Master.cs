using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FastForward.SCMPDA.Services;
using FF.BusinessObjects;

namespace FastForward.SCMPDA
{
    public partial class PDAWeb : System.Web.UI.MasterPage
    {
        Base _base = new Base();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if ((!string.IsNullOrEmpty(Convert.ToString(Session["UserID"]))) && (!string.IsNullOrEmpty(Convert.ToString(Session["UserCompanyCode"]))) && (!string.IsNullOrEmpty(Convert.ToString(Session["UserCompanyName"]))) && (!string.IsNullOrEmpty(Convert.ToString(Session["UserIP"]))) && (!string.IsNullOrEmpty(Convert.ToString(Session["UserComputer"]))))
                {
                    string user = (string)Session["UserID"];
                    lbluser.Text = user;
                    lbllocation.Text = Session["UserDefLoca"].ToString();

                    string pageName = this.BodyContent.Page.GetType().FullName;
                    if ((pageName=="ASP.createjobnumber_aspx") || (pageName=="ASP.checkscannedstock_aspx"))
                    {
                        lbljob.Text = " : " + (string)Session["DOCNO"];
                    }

                    string userloadingpoint = (string)Session["LOADING_POINT_NAME"];

                    if (!string.IsNullOrEmpty(userloadingpoint))
                    {
                        lbllp.Visible = true;
                        //lbllpdash.Visible = true;
                        lbllp.Text = userloadingpoint;
                    }
                    else
                    {
                        lbllp.Visible = false;
                        //lbllpdash.Visible = false;
                        lbllp.Text = string.Empty;
                    }
                }
                else
                {
                    Response.Redirect("LoginPDA.aspx", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lbtnhome_Click(object sender, EventArgs e)
        {
            try
            {
                Session["CHECKBUTTON"] = null;
                Response.Redirect("Default.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lbtnlogout_Click(object sender, EventArgs e)
        {
            try
            {
                if (BaseCls.GlbIsExit == false)
                {
                    _base.CHNLSVC.CloseChannel();
                    _base.CHNLSVC.Security.ExitLoginSession(Session["UserID"].ToString(), Session["UserCompanyName"].ToString(), (string)Session["GlbUserSessionID"]);
                    BaseCls.GlbIsExit = true;
                }
                Session.RemoveAll();
                Session.Clear();
                Session["UserDefLoca"] = string.Empty;

                Session["LOCCHANGED"] = null;
                Session["DOCDIRECTION"] = null;
                Session["DOCTYPE"] = null;
                Session["SEQNO"] = null;
                Session["SERIALIZED"] = null;
                Session["UOM"] = null;
                Session["DOCNO"] = null;
                Session["SER2"] = null;
                Session["SER3"] = null;
                Session["ISITEMACTIVE"] = null;
                Session["ITEM"] = null;
                Session["ITEMSTATUS"] = null;
                Session["LASTSCANSERIAL"] = null;
                Session["CHECKBUTTON"] = null;
                Session["STOCKBALANCE"] = null;
                Session["QTYOFBIN"] = null;
                Session["STOCKBALANCE"] = null;
                Session["EXPDATE"] = null;
                Session["UserID"] = null;
                Session["WAREHOUSE_COMPDA"] = null;
                Session["WAREHOUSE_LOCPDA"] = null;
                Session["LOADING_POINT"] = null;
                Session["LOADING_POINT_NAME"] = null;
                Session["BAY_CHANGED"] = null;
                Session["SELECTED_JOB"] = null;

                Response.Redirect("LoginPDA.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }
    }
}