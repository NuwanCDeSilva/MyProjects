using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using FF.BusinessObjects;
using System.Globalization;

namespace FastForward.SCMPDA
{
    public class ApproveRequestUC : BasePage
    {
      //  
        public RequestApprovalHeader getRequestDetail(string requestNumber)
        {
            //List<RequestApprovalHeader> reqNumList = new List<RequestApprovalHeader>();

            //DataTable dt = new DataTable();
            //dt = CHNLSVC.General.GetApprovedRequestDetails(com, pc, fucCD, appType, isInApprovedStatus, userApprovalLevel);

            //foreach(DataRow row in dt.Rows)
            //{
                            
            //}
            //return reqNumList;

            RequestApprovalHeader reqHdr = new RequestApprovalHeader();
            return reqHdr;
        }
        public List<string> getApprovedReqNumbersList(string com, string pc, string fucCD, string appType, Int32 isInApprovedStatus, Int32 userApprovalLevel)
        {
            List<string> reqNumList = new List<string>();

            DataTable dt = new DataTable();
            dt = CHNLSVC.General.GetApprovedRequestDetails(com, pc, fucCD, appType, isInApprovedStatus, userApprovalLevel);
            if(dt.Rows.Count>0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string reqno = row["GRAH_REF"].ToString();
                    reqNumList.Add(reqno);
                }
            }
           
            return reqNumList;
        }
        public DataTable getRequestDetails(string com, string pc, string fucCD, string appType, Int32 isInApprovedStatus, Int32 userApprovalLevel)
        {
            DataTable dt = new DataTable();
            dt = CHNLSVC.General.GetApprovedRequestDetails(com, pc, fucCD, appType, isInApprovedStatus, userApprovalLevel);
           
            return dt;
        }
    }
}