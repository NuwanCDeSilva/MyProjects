using FastForward.SCMWeb.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.UserControls
{
    public partial class SerSearch : System.Web.UI.UserControl
    {
        Base _basePage;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Unnamed2_Click(object sender, EventArgs e)
        {
           // UserPopoup1.Show();
        //    _basePage = new Base();

           // string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
          /*  DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
            dvResult.DataSource = _result;
            dvResult.DataBind();*/
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            UserPopoup1.Show();
        }
    }
}