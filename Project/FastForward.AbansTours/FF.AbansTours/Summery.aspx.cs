using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;

namespace FF.AbansTours
{
    public partial class Summery : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Convert.ToString(Session["UserID"])) &&
                      !String.IsNullOrEmpty(Convert.ToString(Session["UserCompanyCode"])) &&
                      !String.IsNullOrEmpty(Convert.ToString(Session["UserDefProf"])) &&
                      !String.IsNullOrEmpty(Convert.ToString(Session["UserDefLoca"])))
                {
                    if (!IsPostBack)
                    {

                        loadData();
                    }
                }
                else
                {
                    string gotoURL = "login.aspx";
                    Response.Write("<script>window.open('" + gotoURL + "','_parent');</script>");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void loadData()
        {
            string Status = "0,1,2,3,4,5,6,7,8";
            String Pc = "";
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 15001))
            {
                Pc = Session["UserDefProf"].ToString();
            }
            else
            {
                Pc = "";
            }

            List<GEN_CUST_ENQ> oItems = CHNLSVC.Tours.GET_ENQRY_BY_PC_STATUS(Session["UserCompanyCode"].ToString(), Pc, Status);
            if (oItems.Count > 0)
            {
                dgvHistry.DataSource = oItems;
                dgvHistry.DataBind();
               // modifyGrid();
            }
            else
            {
               // divColors.Visible = false;
            }
        }
    }
}