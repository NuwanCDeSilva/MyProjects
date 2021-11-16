using FastForward.LogisticAPI.API_Operarion.PHPProjects.LogisticFrontUser;
using FastForward.LogisticAPI.Objects.PHPProjects.LogisticFrontUser;
using System;
using System.Collections.Generic;
using System.Web.Http;
using FF.BusinessObjects;
using FF.BusinessObjects.Genaral;
using FF.BusinessObjects.Search;
using Newtonsoft.Json;
using System.Data;
using System.Linq;

namespace FastForward.LogisticAPI.Controllers.PHPProjects.LogisticFrontUser
{
    [RoutePrefix("Search")]
    public class SearchController : ApiController
    {
        JsonObject json = new JsonObject();
        SearchAPIOP search = new SearchAPIOP();

        [HttpGet]
        [Route("GetMainServices")]
        public string GetMainServicesCodes()
        {
            try
            {
                List<MainServices> mainServiceses = search.GetMainServicesCodes();
                if (mainServiceses == null || mainServiceses.Count == 0)
                {
                    json.Data = null;
                    json.Success = false;
                    json.Message = "";
                    json.ErrorNumber = -1;
                    return JsonConvert.SerializeObject(json);
                }
                else
                {
                    List<MainServices> _serviceses = new List<MainServices>();
                    foreach (MainServices service in mainServiceses)
                    {
                        service.fms_ser_cd = service.fms_ser_cd.ToString();
                        service.fms_ser_desc = service.fms_ser_desc.ToString();
                        _serviceses.Add(service);
                    }
                    json.Data = _serviceses;
                    json.Success = true;
                    json.Message = null;
                    json.ErrorNumber = 0;
                    return JsonConvert.SerializeObject(json);
                }
            }
            catch (Exception exception)
            {

                json.Data = null;
                json.Success = false;
                json.Message = exception.Message;
                json.ErrorNumber = -1;
                return JsonConvert.SerializeObject(json);
            }
        }

        [HttpGet]
        [Route("GetCountries")]
        public string GetCountryList()
        {
            try
            {
                List<MST_COUNTRY> _countries = search.GetCountryList();
                if (_countries == null || _countries.Count == 0)
                {
                    json.Data = null;
                    json.Success = false;
                    json.Message = "";
                    json.ErrorNumber = -1;
                    return JsonConvert.SerializeObject(json);
                }
                else
                {
                    List<MST_COUNTRY> _country = new List<MST_COUNTRY>();
                    foreach (MST_COUNTRY countryList in _countries)
                    {
                        countryList.MCU_CD = countryList.MCU_CD.ToString();
                        countryList.MCU_DESC = countryList.MCU_DESC.ToString();
                        countryList.MCU_REGION_CD = countryList.MCU_REGION_CD.ToString();
                        _country.Add(countryList);
                    }
                    json.Data = _country;
                    json.Success = true;
                    json.Message = null;
                    json.ErrorNumber = 0;
                    return JsonConvert.SerializeObject(json);
                }
            }
            catch (Exception exception)
            {

                json.Data = null;
                json.Success = false;
                json.Message = exception.Message;
                json.ErrorNumber = -1;
                return JsonConvert.SerializeObject(json);
            }
        }

        [HttpPost]
        [Route("GetCustomerSearch")]
        public string GetCustomer(CustomerGetObject customerGetObject)
        {
            try
            {
                List<CustomerSearchObject> _customerSearch = search.GetCustomer(customerGetObject.telephone, customerGetObject.email);
                if (_customerSearch == null || _customerSearch.Count == 0)
                {
                    json.Data = null;
                    json.Success = false;
                    json.Message = "";
                    json.ErrorNumber = -1;
                    return JsonConvert.SerializeObject(json);
                }
                else
                {
                    List<CustomerSearchObject> _customer = new List<CustomerSearchObject>();
                    foreach (CustomerSearchObject _customerData in _customerSearch)
                    {
                        _customerData.Mbe_cd = _customerData.Mbe_cd.ToString();
                        _customerData.Mbe_name = _customerData.Mbe_name.ToString();
                        _customerData.Mbe_add1 = _customerData.Mbe_add1.ToString();
                        _customerData.Mbe_add2 = _customerData.Mbe_add2.ToString();
                        _customerData.Mbe_country_cd = _customerData.Mbe_country_cd.ToString();
                        _customerData.Mbe_mob = _customerData.Mbe_mob.ToString();
                        _customerData.Mbe_email = _customerData.Mbe_email.ToString();
                        _customer.Add(_customerData);
                    }
                    json.Data = _customer;
                    json.Success = true;
                    json.Message = null;
                    json.ErrorNumber = 0;
                    return JsonConvert.SerializeObject(json);
                }
            }
            catch (Exception exception)
            {

                json.Data = null;
                json.Success = false;
                json.Message = exception.Message;
                json.ErrorNumber = -1;
                return JsonConvert.SerializeObject(json);
            }
        }

        [HttpGet]
        [Route("GetJobStatus")]
        public string GetJobStatus(string cus_code)
        {
            try
            {
                List<trn_jb_hdr> _jobNo = search.GetCustomerJobs(cus_code);
                if (_jobNo == null || _jobNo.Count == 0)
                {
                    json.Data = null;
                    json.Success = false;
                    json.Message = "";
                    json.ErrorNumber = -1;
                    return JsonConvert.SerializeObject(json);
                }
                else
                {
                    List<trn_jb_hdr> _customerJobStatus = new List<trn_jb_hdr>();
                    foreach (trn_jb_hdr _jobStatus in _jobNo)
                    {
                        _jobStatus.Jb_stus = _jobStatus.Jb_stus.ToString();
                        _jobStatus.Jb_cre_dt = _jobStatus.Jb_cre_dt.Date;
                        _jobStatus.Jb_mod_de = _jobStatus.Jb_mod_de.Date;
                        _customerJobStatus.Add(_jobStatus);
                    }
                    json.Data = _customerJobStatus;
                    json.Success = true;
                    json.Message = null;
                    json.ErrorNumber = 0;
                    return JsonConvert.SerializeObject(json);
                }
            }
            catch (Exception exception)
            {

                json.Data = null;
                json.Success = false;
                json.Message = exception.Message;
                json.ErrorNumber = -1;
                return JsonConvert.SerializeObject(json);
            }
        }

        [HttpGet]
        [Route("GetCustomerInvoices")]
        public string getCustomerInvoices(string cus_code)
        {
            try
            {
                List<cus_invoices> _invoices = search.getCustomerInvoices(cus_code);
                if (_invoices == null || _invoices.Count == 0)
                {
                    json.Data = null;
                    json.Success = false;
                    json.Message = "";
                    json.ErrorNumber = -1;
                    return JsonConvert.SerializeObject(json);
                }
                else
                {
                    List<cus_invoices> _customerInvoices = new List<cus_invoices>();
                    foreach (cus_invoices _invoice in _invoices)
                    {
                        _invoice.TIH_INV_NO = _invoice.TIH_INV_NO.ToString();
                        _invoice.TIH_MAN_REF_NO = _invoice.TIH_MAN_REF_NO.ToString();
                        _invoice.TIH_TOT_AMT = _invoice.TIH_TOT_AMT;
                        _invoice.TIH_INV_DT = _invoice.TIH_INV_DT.Date;
                        _customerInvoices.Add(_invoice);
                    }
                    json.Data = _customerInvoices;
                    json.Success = true;
                    json.Message = null;
                    json.ErrorNumber = 0;
                    return JsonConvert.SerializeObject(json);
                }
            }
            catch (Exception exception)
            {

                json.Data = null;
                json.Success = false;
                json.Message = exception.Message;
                json.ErrorNumber = -1;
                return JsonConvert.SerializeObject(json);
            }
        }

        [HttpGet]
        [Route("GetCustomerInvoiceDetails")]
        public string getCustomerInvoiceDetails(string InvNo, string company)
        {
             try
             {
            string error = null;
            DataTable _invoices = search.Inv_Details(InvNo, company, out error);
            if (_invoices == null)
            {
                json.Data = null;
                json.Success = false;
                json.Message = "";
                json.ErrorNumber = -1;
                return JsonConvert.SerializeObject(json);
            }
            else
            {
                 var _customerInvoices = _invoices.AsEnumerable().Select(result => new
                {
                    TIH_ACC_CUS_NAME = result.Field<string>("TIH_ACC_CUS_NAME"),
                    TIH_ACC_CUS_ADD1 = result.Field<string>("TIH_ACC_CUS_ADD1"),
                    TIH_ACC_CUS_ADD2 = result.Field<string>("TIH_ACC_CUS_ADD2"),
                    TIH_INV_NO = result.Field<string>("TIH_INV_NO"),
                    VATNO = result.Field<string>("VATNO"),
                    TIH_OTHER_REF_NO = result.Field<string>("TIH_OTHER_REF_NO"),
                    TIH_INV_DT = result.Field<DateTime>("TIH_INV_DT"),
                    TIH_JOB_NO = result.Field<string>("TIH_JOB_NO"),
                    DESTIN = result.Field<string>("DESTIN"),
                    FLIGHT = result.Field<string>("FLIGHT"),
                    BLNO = result.Field<string>("BLNO"),
                    ETD_CMB = result.Field<string>("ETD_CMB"),
                    CHEE = result.Field<string>("CHEE"),
                    TID_CHA_DESC = result.Field<string>("TID_CHA_DESC"),
                    TID_CHA_AMT = result.Field<Decimal>("TID_CHA_AMT"),
                    TIH_RMK = result.Field<string>("TIH_RMK"),
                    TIH_INV_CURR = result.Field<string>("TIH_INV_CURR"),
                    TID_CURR_CD = result.Field<string>("TID_CURR_CD"),
                    TIID_TAX_RATE = result.Field<string>("TIID_TAX_RATE"),
                    TIID_TAX_UNC_AMT = result.Field<string>("TIID_TAX_UNC_AMT")
                }).ToList();

            
                json.Data = _customerInvoices;
                json.Success = true;
                json.Message = null;
                json.ErrorNumber = 0;
                return JsonConvert.SerializeObject(json);
            }
        }
        catch (Exception exception)
        {

            json.Data = null;
            json.Success = false;
            json.Message = exception.Message;
            json.ErrorNumber = -1;
            return JsonConvert.SerializeObject(json);
        }
        }
    }
}