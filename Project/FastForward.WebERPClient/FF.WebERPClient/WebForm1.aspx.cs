using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FF.WebERPClient
{
    public partial class WebForm1 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           // uc_CommonSearch1.BindUCtrlGridData(CHNLSVC.General.GetCommonSearchData(string.Empty, string.Empty));
           //// uc_CommonSearch1.ReturnResultControl = txtCompany.ClientID;
           // uc_CommonSearch1.UCModalPopupExtender.Show();

            CommonUIOperations s = new CommonUIOperations();
            string html = CommonUIOperations.ConvertDataTableToHtml(CHNLSVC.CommonSearch.GetCommonSearchData(string.Empty, string.Empty));
            divResult.InnerHtml = html;


        }
    }
}