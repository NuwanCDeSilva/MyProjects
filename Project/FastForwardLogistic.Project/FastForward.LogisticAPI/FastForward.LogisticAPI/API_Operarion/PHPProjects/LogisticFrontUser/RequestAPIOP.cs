using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FastForward.LogisticAPI.Controllers;
using FastForward.LogisticAPI.Objects.PHPProjects.LogisticFrontUser;
using FF.BusinessObjects.Genaral;
using FF.BusinessObjects.Search;
using FF.BusinessObjects;

namespace FastForward.LogisticAPI.API_Operarion.PHPProjects.LogisticFrontUser
{
    public class RequestAPIOP:BaseController
    {
        public cus_code_api saveRequest(trn_req_hdr request, CustomerSearchObject customerObject, MasterAutoNumber cusomerNumber, MasterAutoNumber jobNumber, mst_busentity_grup busentityGrup, trn_req_serdet reqSerdet, trn_req_cus_det reqCusDet)
        {
            cus_code_api response = CHNLSVC.General.SaveJobRequest(request, customerObject, cusomerNumber, jobNumber, busentityGrup, reqSerdet, reqCusDet);
            return response;
        }
    }
}