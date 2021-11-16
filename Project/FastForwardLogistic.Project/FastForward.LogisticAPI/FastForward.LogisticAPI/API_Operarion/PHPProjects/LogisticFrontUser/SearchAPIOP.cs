using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FastForward.LogisticAPI.Controllers;
using FF.BusinessObjects;
using FF.BusinessObjects.Genaral;
using FF.BusinessObjects.Search;
using System.Data;

namespace FastForward.LogisticAPI.API_Operarion.PHPProjects.LogisticFrontUser
{
    public class SearchAPIOP:BaseController
    {
        public List<MainServices> GetMainServicesCodes()
        {
            List<MainServices> _serviceses = CHNLSVC.General.GetMainServicesCodes();
            return _serviceses;
        }

        public List<MST_COUNTRY> GetCountryList()
        {
            List<MST_COUNTRY> _countries = CHNLSVC.General.getCustomerCountry();
            return _countries;
        }

        public List<CustomerSearchObject> GetCustomer(string telephone, string email)
        {
            List<CustomerSearchObject> _customer = CHNLSVC.CommonSearch.getCustomer(telephone, email);
            return _customer;
        }

        public List<trn_jb_hdr> GetJobStatus(string jobNo)
        {
            List<trn_jb_hdr> _jobStatus = CHNLSVC.General.GetJobHdr(jobNo);
            return _jobStatus;
        }
        public List<trn_jb_hdr> GetCustomerJobs(string cus_code)
        {
            List<trn_jb_hdr> _jobStatus = CHNLSVC.General.GetCustomerJobs(cus_code);
            return _jobStatus;
        }

        public List<cus_invoices> getCustomerInvoices(string cus_code)
        {
            List<cus_invoices> _invoices = CHNLSVC.General.getCustomerInvoices(cus_code);
            return _invoices;
        }

        public DataTable Inv_Details(string InvNo, string company, out string error)
        {
            DataTable _invoices = CHNLSVC.Sales.Inv_Details(InvNo,company,out error);
            return _invoices;
        }
    }
}