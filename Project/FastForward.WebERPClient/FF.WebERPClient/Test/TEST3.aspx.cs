using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using System.Text;

namespace FF.WebERPClient.Test
{
    public partial class TEST3 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["UserID"] = "ADMIN";
                Session["UserCompanyCode"] = "ABL";
                Session["SessionID"] = "666";
                Session["UserDefLoca"] = "MSR16";
                Session["UserDefProf"] = "39";
                DisplayUserApprovePermission();
            }
        }

        private void DisplayUserApprovePermission()
        {
            StringBuilder sb = new StringBuilder();

            //--------------------------------------------------------------------------------------------------------------------------------- 

            /// <summary>
            /// This is a commom method used in Approve cycle,to get Approve cycle definiton for particular function.
            /// </summary>
            /// <param name="_sourceCompanyCode">Used to get source location hierachy</param>
            /// <param name="_sourceLocCode">Used to get source location hierachy</param>
            /// <param name="_reqSubType">RequestSubType</param>
            /// <param name="_transactionDate">TransactionDate</param>
            /// <param name="_destinationCompanyCode">Used to get destination location hierachy</param>
            /// <param name="_destinationLocCode">Used to get destination location hierachy</param>
            /// <returns></returns>
            string dt = "6/20/2012"; //INVCCR //INVMRN          
            //Without destination type.
            //RequestApproveCycleDefinition _result = CHNLSVC.Security.GetApproveCycleDefinitionData("ABL", "R2AM", "INVMRN", Convert.ToDateTime(dt), null);

            //With destination type.
            RequestApproveCycleDefinition _result = CHNLSVC.Security.GetApproveCycleDefinitionDataforProfitCenter("ABL", "39", "ARQT010", DateTime.Now.Date, "", "", CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());


            //--------------------------------------------------------------------------------------------------------------------------------- 

            /// <summary>
            /// This is a commom method used in Approve Cycle,to get user request approval permission for paticular function.Permission can have 
            /// either location wise or user wise.
            /// </summary>
            /// <param name="_userId">Logged UserId</param>
            /// <param name="_reqSubType">RequestSubType</param>
            /// <param name="_companyCode">Used to get type hierachy</param>
            /// <param name="_locationCode">Used to get type hierachy</param>
            /// <returns></returns>

            //Get logged user request approval permission. 
            UserRequestApprovePermission _obj = CHNLSVC.Security.GetUserRequestApprovalPermissionDataForProfitCenter("ADMIN", "ARQT010", "ABL", "39", CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

            if (_result != null)
            {

                displayUserPerm.InnerHtml = string.Empty;

                sb.AppendLine("SourceType - " + _result.SourceType + "<br>");
                sb.AppendLine("SourceTypeCode - " + _result.SourceTypeCode + "<br>");
                sb.AppendLine("TransactionDate - " + _result.TransactionDate.ToString() + "<br>");
                sb.AppendLine("DestinationType - " + _result.DestinationType + "<br>");
                sb.AppendLine("DestinationTypeCode - " + _result.DestinationTypeCode + "<br>");
                sb.AppendLine("RequestMainType - " + _result.UserRequestApprovePermission.RequestMainType + "<br>");
                sb.AppendLine("RequestSubType - " + _result.UserRequestApprovePermission.RequestSubType + "<br>");
                sb.AppendLine("Description - " + _result.UserRequestApprovePermission.Description + "<br>");
                sb.AppendLine("IsApprovalNeeded - " + _result.IsApprovalNeeded + "<br>");
            }

            sb.AppendLine("==========================================================================================" + "<br>");

            if (_obj != null)
            {
                sb.AppendLine("UserId - " + _obj.UserId + "<br>");
                sb.AppendLine("Type - " + _obj.Type + "<br>");
                sb.AppendLine("TypeCode -" + _obj.TypeCode + "<br>");
                sb.AppendLine("UserPermissionCode - " + _obj.UserPermissionCode + "<br>");
                sb.AppendLine("UserPermissionLevel - " + _obj.UserPermissionLevel.ToString() + "<br><br>");
                sb.AppendLine("RequestMainType - " + _obj.RequestMainType + "<br>");
                sb.AppendLine("RequestSubType - " + _obj.RequestSubType + "<br>");
                sb.AppendLine("Description - " + _obj.Description + "<br>");
                sb.AppendLine("RequestLevel - " + _obj.RequestLevel.ToString() + "<br>");
                sb.AppendLine("RequestApproveLevel -" + _obj.RequestApproveLevel.ToString() + "<br><br>");
                sb.AppendLine("IsApprovalUser -" + _obj.IsApprovalUser.ToString() + "<br>");
                sb.AppendLine("IsFinalApprovalUser -" + _obj.IsFinalApprovalUser.ToString() + "<br>");
                sb.AppendLine("IsRequestGenerateUser - " + _obj.IsRequestGenerateUser.ToString() + "<br>");

            }
            displayUserPerm.InnerHtml = sb.ToString();
        }

    }
}
