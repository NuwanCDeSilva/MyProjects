using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using FF.BusinessObjects;
using System.Globalization;
using System.Text.RegularExpressions;

namespace FastForward.SCMWeb
{
    public class CustomerCreationUC : BasePage
    {


        public string SaveCustomer(MasterBusinessEntity _businessEntity, CustomerAccountRef _account,List<MasterBusinessEntityInfo> bisInfo)
        {
            string custCD;

            _businessEntity.Mbe_com = GlbUserComCode;
            _businessEntity.Mbe_cre_by = GlbUserName;
            _businessEntity.Mbe_cre_pc = GlbUserDefProf;

           // Int32 effect = CHNLSVC.Sales.SaveBusinessEntityDetail(_businessEntity, _account, bisInfo, out custCD);
            custCD = "kk";
            return custCD;

        }
        public Int32 UpdateCustomer(MasterBusinessEntity _businessEntity, Decimal newCredLimit, List<MasterBusinessEntityInfo> bisInfo, List<MasterBusinessEntitySalesType> _salesTypes)
        {
           // _businessEntity.Mbe_com = GlbUserComCode;
           // _businessEntity.Mbe_cre_by = GlbUserName;
           // _businessEntity.Mbe_cre_pc = GlbUserDefProf;

            Int32 effect = CHNLSVC.Sales.UpdateBusinessEntityProfile(_businessEntity, GlbUserName, DateTime.Today.Date, newCredLimit, bisInfo, _salesTypes);
            return effect;

        }
        public DataTable GetBuizEntityTypes(string category)
        {
            return CHNLSVC.Sales.GetBusinessEntityTypes(category);
        }
        public DataTable GetBuizEntityTypesValues(string category, string type_)
        {
            return CHNLSVC.Sales.GetBusinessEntityAllValues(category, type_);
        }
        public DataTable Get_DetBy_town(string town)
        {
            return CHNLSVC.General.Get_DetBy_town(town);
        }
        #region Validations

        public bool IsValidEmail(string email)
        {
            //THIS IS CASE SENSITIVE IF U DONT WONT CASE SENSITIVE PLEASE USE FOLLOWING CODE
            // email = email.ToLower();
            string pattern = @"^[-a-zA-Z0-9][-.a-zA-Z0-9]*@[-.a-zA-Z0-9]+(\.[-.a-zA-Z0-9]+)*\.(com|edu|info|gov|int|mil|net|org|biz|name|museum|coop|aero|pro|tv|[a-zA-Z]{2})$";
            // ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
            Regex check = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
            //boolean variable to return to calling method
            bool valid = false;
            //make sure an email address was provided
            if (string.IsNullOrEmpty(email))
            {
                valid = false;
            }
            else
            {
                //use IsMatch to validate the address
                valid = check.IsMatch(email);
            }
            //return the value to the calling method
            return valid;
        }


        public bool IsValidTelephone(string telephoneNumber)
        {
            bool valid = false;
            telephoneNumber = telephoneNumber.Replace("-", "").Replace("/", "").Trim();
            if (telephoneNumber.Trim().Length == 10)
            {
                try
                {
                    Int64.Parse(telephoneNumber);
                    valid = true;
                }
                catch (Exception)
                {
                    valid = false;
                }
            }
            else
            {
                valid = false;
            }
            return valid;
        }
        public  bool IsValidNIC(string nic)
        {
            //THIS IS CASE SENSITIVE IF U DONT WONT CASE SENSITIVE PLEASE USE FOLLOWING CODE
            // email = email.ToLower();
            string pattern = @"\d{9}[V|v|x|X]";
            Regex check = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
            //boolean variable to return to calling method
            bool valid = false;
            //make sure an email address was provided
            if (string.IsNullOrEmpty(nic))
            {
                valid = false;
            }
            else
            {
                //use IsMatch to validate the address
                valid = check.IsMatch(nic);
            }
            //return the value to the calling method
            return valid;
        }

        #endregion Validations



        #region LoadCustProfile 

        public MasterBusinessEntity GetbyCustCD(string custCD)
        {
            return CHNLSVC.Sales.GetCustomerProfile(custCD, null, null, null,null);
        }
        public MasterBusinessEntity GetbyNIC(string nic)
        {
            return CHNLSVC.Sales.GetCustomerProfile(null, nic, null, null,null);
        }
        public MasterBusinessEntity GetbyDL(string dl)
        {
            return CHNLSVC.Sales.GetCustomerProfile(null, null, dl, null,null);
        }
        public MasterBusinessEntity GetbyPPno(string ppno)
        {
            return CHNLSVC.Sales.GetCustomerProfile(null, null, null, ppno,null);
        }
        public MasterBusinessEntity GetbyBrNo(string brNo)
        {
            return CHNLSVC.Sales.GetCustomerProfile(null, null, null, null, brNo);
        }
        #endregion LoadCustProfile
    }
}