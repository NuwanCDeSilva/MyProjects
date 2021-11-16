using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using FastForward.LogisticAPI.API_Operarion.PHPProjects.LogisticFrontUser;
using FastForward.LogisticAPI.Objects.PHPProjects.LogisticFrontUser;
using FF.BusinessObjects.Genaral;
using FF.BusinessObjects.Search;
using FF.BusinessObjects;
using Newtonsoft.Json;

namespace FastForward.LogisticAPI.Controllers.PHPProjects.LogisticFrontUser
{
    [RoutePrefix("request")]
    public class RequestController : ApiController
    {
        [HttpPost]
        [Route("save-request")]
        public string saveRequest(RequestObject requestObject)
        {
            RequestAPIOP requestApiop = new RequestAPIOP();
            JsonObject json = new JsonObject();
            trn_req_hdr _request = new trn_req_hdr();
            CustomerSearchObject _customer = new CustomerSearchObject();
            MasterAutoNumber _customerCode = new MasterAutoNumber();
            MasterAutoNumber _jobNumber = new MasterAutoNumber();
            mst_busentity_grup _busentityGrup = new mst_busentity_grup();
            trn_req_serdet _reqSerdet = new trn_req_serdet();
            trn_req_cus_det _reqCusDet = new trn_req_cus_det();

            _jobNumber.Aut_moduleid = "JOB-REQ";
            _jobNumber.Aut_direction = 1;
            _jobNumber.Aut_start_char = requestObject.main_services + "/" + requestObject.sub_services;
            _jobNumber.Aut_cate_tp = "COM";
            _jobNumber.Aut_cate_cd = "LGT";
            _jobNumber.Aut_year = DateTime.Now.Year;

            _request.Rq_com_cd = "LGT";
            _request.Rq_dt = DateTime.Now;
            _request.Rq_pouch_no = String.Empty;
            _request.Rq_sbu_cd = String.Empty;
            _request.Rq_mos_cd = String.Empty;
            _request.Rq_tos_cd = String.Empty;
            _request.Rq_rmk = requestObject.enquiry;
            _request.Rq_stus = "A";
            _request.Rq_stage = 1;
            _request.Rq_cre_by = "WEB";
            _request.Rq_cre_dt = DateTime.Now;
            _request.Rq_mod_by = "WEB";
            _request.Rq_mod_de = DateTime.Now;

            _reqSerdet.rs_line_no = 1;
            _reqSerdet.rs_ser_tp = requestObject.main_services;
            _reqSerdet.rs_rmk = requestObject.enquiry;
            _reqSerdet.rs_cre_by = "WEB";
            _reqSerdet.rs_cre_dt = DateTime.Now;
            _reqSerdet.rs_mod_by = "WEB";
            _reqSerdet.rs_mod_dt = DateTime.Now;
            _reqSerdet.rs_mser_tp = requestObject.sub_services;

            _reqCusDet.Rc_cus_tp = "C";
            _reqCusDet.Rc_cre_by = "WEB";
            _reqCusDet.Rc_cre_dt = DateTime.Now;
            _reqCusDet.Rc_mod_by = "WEB";
            _reqCusDet.Rc_mod_dt = DateTime.Now;

            _customerCode.Aut_moduleid = "CUS";
            _customerCode.Aut_direction = 1;
            _customerCode.Aut_start_char = "CONT";
            _customerCode.Aut_cate_tp = "COM";
            _customerCode.Aut_cate_cd = "LGT";

            _customer.Mbe_cd = requestObject.cus_code;
            _customer.Mbe_com = "LGT";
            _customer.Mbe_tp = "C";
            _customer.Mbe_name = requestObject.name;
            _customer.Mbe_add1 = requestObject.address_1;
            _customer.Mbe_add2 = requestObject.address_2;
            _customer.Mbe_country_cd = requestObject.country;
            _customer.Mbe_mob = requestObject.telephone;
            _customer.Mbe_email = requestObject.email;
            _customer.Mbe_wr_com_name = requestObject.company_name;
            _customer.Mbe_cre_by = "WEB";
            _customer.Mbe_cre_dt = DateTime.Now;

            _busentityGrup.Mbg_name = requestObject.name;
            _busentityGrup.Mbg_add1 = requestObject.address_1;
            _busentityGrup.Mbg_add2 = requestObject.address_2;
            _busentityGrup.Mbg_country_cd = requestObject.country;
            _busentityGrup.Mbg_mob = requestObject.telephone;
            _busentityGrup.Mbg_email = requestObject.email;
            _busentityGrup.Mbg_cre_by = "WEB";
            _busentityGrup.Mbg_cre_dt = DateTime.Now;
            _busentityGrup.Mbg_mod_by = "WEB";
            _busentityGrup.Mbg_mod_dt = DateTime.Now;


            cus_code_api response = requestApiop.saveRequest(_request, _customer, _customerCode, _jobNumber, _busentityGrup, _reqSerdet, _reqCusDet);

            json.Data = response.cus_code;
            json.Success = (int)response.count > 0 ? true : false;
            json.Message = "";
            json.ErrorNumber = (int)response.count;
            return JsonConvert.SerializeObject(json);
        }
    }
}