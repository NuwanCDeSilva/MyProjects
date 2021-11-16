using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using System.Collections.Generic;
using System.Data;
using FF.BusinessObjects.Genaral;
using FF.BusinessObjects.Sales;
using FF.BusinessObjects;
using FF.BusinessObjects.Security;
using FF.BusinessObjects.Search;


namespace FF.DataAccessLayer.BaseDAL
{
    public class SalesDAL : BaseDAL
    {
        /// <summary>
        /// Isuru 2017/05/29
        /// </summary>
        /// <param name="cust"></param>
        /// <param name="userid"></param>
        /// <param name="loc"></param>
        /// <param name="company"></param>
        /// <returns></returns>
        public String SaveCustomerDetails(cus_details cust, string _IS_TAX, string _ACT, string _IS_SVAT, string _TAX_EX, string _IS_SUSPEND, string _AGRE_SEND_SMS, string _AGRE_SEND_EMAIL, string userid, string loc, string company)
        {
            OracleParameter[] param = new OracleParameter[39];
            Int32 effects = 0;
            (param[0] = new OracleParameter("p_cus_typ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_TP;
            (param[1] = new OracleParameter("p_cus_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_CD;
            (param[2] = new OracleParameter("p_br_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_BR_NO;
            (param[3] = new OracleParameter("p_dr_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_DL_NO;
            (param[4] = new OracleParameter("p_tin_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_OTH_ID_NO;
            (param[5] = new OracleParameter("p_cus_nme", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_NAME;
            (param[6] = new OracleParameter("p_nic_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_NIC;
            (param[7] = new OracleParameter("p_pp_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_PP_NO;
            (param[8] = new OracleParameter("p_tax_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToBoolean(_IS_TAX);
            (param[9] = new OracleParameter("p_tax_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_TAX_NO;
            (param[10] = new OracleParameter("p_svat_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToBoolean(_IS_SVAT);
            (param[11] = new OracleParameter("p_svat_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_SVAT_NO;
            (param[12] = new OracleParameter("p_currncy", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_CUR_CD;
            (param[13] = new OracleParameter("p_tax_exc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _TAX_EX;
            (param[14] = new OracleParameter("p_country", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_COUNTRY_CD;
            (param[15] = new OracleParameter("p_district", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_DISTRIC_CD;
            (param[16] = new OracleParameter("p_province", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_PROVINCE_CD;
            (param[17] = new OracleParameter("p_mob_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_MOB;
            (param[18] = new OracleParameter("p_web", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_WEB;
            (param[19] = new OracleParameter("p_phn", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_TEL;
            (param[20] = new OracleParameter("p_twn", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_TOWN_CD;
            (param[21] = new OracleParameter("p_addrs", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_ADD1;
            (param[22] = new OracleParameter("p_fax", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_FAX;
            (param[23] = new OracleParameter("p_email", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_EMAIL;
            (param[24] = new OracleParameter("p_cntct_persn", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_CONTACT_PERSON;
            (param[25] = new OracleParameter("p_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToBoolean(_ACT);
            (param[26] = new OracleParameter("p_is_susp", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToBoolean(_IS_SUSPEND);
            (param[27] = new OracleParameter("p_alw_sms", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToBoolean(_AGRE_SEND_SMS);
            (param[28] = new OracleParameter("p_alw_email", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToBoolean(_AGRE_SEND_EMAIL);
            (param[29] = new OracleParameter("p_intr_exe", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_INTRO_EX;
            (param[30] = new OracleParameter("p_ent_mod", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_ENT_MODULE;
            (param[31] = new OracleParameter("p_segmen", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_REMARK;
            (param[32] = new OracleParameter("p_crte_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userid;
            (param[33] = new OracleParameter("p_crte_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = DateTime.Now;
            (param[34] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[35] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_CUST_LOC;
            (param[36] = new OracleParameter("p_addrs2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_ADD2;
            (param[37] = new OracleParameter("p_crdlimit", OracleDbType.Int32, null, ParameterDirection.Input)).Value = cust.MBE_CREDIT_DAYS;
            param[38] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_save_customer_new", CommandType.StoredProcedure, param);
            return cust.MBE_CD;
            //return effects;
        }

        public Int32 UpdateCustomerDetails(cus_details cust, string _IS_TAX, string _ACT, string _IS_SVAT, string _TAX_EX, string _IS_SUSPEND, string _AGRE_SEND_SMS, string _AGRE_SEND_EMAIL, string userid, string loc, string company)
        {
            OracleParameter[] param = new OracleParameter[39];
            Int32 effects = 0;
            (param[0] = new OracleParameter("p_cus_typ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_TP;
            (param[1] = new OracleParameter("p_cus_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_CD;
            (param[2] = new OracleParameter("p_br_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_BR_NO;
            (param[3] = new OracleParameter("p_dr_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_DL_NO;
            (param[4] = new OracleParameter("p_tin_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_OTH_ID_NO;
            (param[5] = new OracleParameter("p_cus_nme", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_NAME;
            (param[6] = new OracleParameter("p_nic_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_NIC;
            (param[7] = new OracleParameter("p_pp_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_PP_NO;
            (param[8] = new OracleParameter("p_tax_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToBoolean(_IS_TAX);
            (param[9] = new OracleParameter("p_tax_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_TAX_NO;
            (param[10] = new OracleParameter("p_svat_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToBoolean(_IS_SVAT);
            (param[11] = new OracleParameter("p_svat_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_SVAT_NO;
            (param[12] = new OracleParameter("p_currncy", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_CUR_CD;
            (param[13] = new OracleParameter("p_tax_exc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _TAX_EX;
            (param[14] = new OracleParameter("p_country", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_COUNTRY_CD;
            (param[15] = new OracleParameter("p_district", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_DISTRIC_CD;
            (param[16] = new OracleParameter("p_province", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_PROVINCE_CD;
            (param[17] = new OracleParameter("p_mob_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_MOB;
            (param[18] = new OracleParameter("p_web", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_WEB;
            (param[19] = new OracleParameter("p_phn", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_TEL;
            (param[20] = new OracleParameter("p_twn", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_TOWN_CD;
            (param[21] = new OracleParameter("p_addrs", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_ADD1;
            (param[22] = new OracleParameter("p_fax", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_FAX;
            (param[23] = new OracleParameter("p_email", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_EMAIL;
            (param[24] = new OracleParameter("p_cntct_persn", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_CONTACT_PERSON;
            (param[25] = new OracleParameter("p_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToBoolean(_ACT);
            (param[26] = new OracleParameter("p_is_susp", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToBoolean(_IS_SUSPEND);
            (param[27] = new OracleParameter("p_alw_sms", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToBoolean(_AGRE_SEND_SMS);
            (param[28] = new OracleParameter("p_alw_email", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToBoolean(_AGRE_SEND_EMAIL);
            (param[29] = new OracleParameter("p_intr_exe", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_INTRO_EX;
            (param[30] = new OracleParameter("p_ent_mod", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_ENT_MODULE;
            (param[31] = new OracleParameter("p_segmen", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_REMARK;
            (param[32] = new OracleParameter("p_crte_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userid;
            (param[33] = new OracleParameter("p_crte_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = DateTime.Now;
            (param[34] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[35] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_CUST_LOC;
            (param[36] = new OracleParameter("p_addrs2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_ADD2;
            (param[37] = new OracleParameter("p_crdlimit", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cust.MBE_CREDIT_DAYS;
            param[38] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_update_customer", CommandType.StoredProcedure, param);
            return effects;
        }

        public Int32 SaveEmployeeData(MST_EMP mst_employee_tbs)
        {
            OracleParameter[] param = new OracleParameter[32];
            Int32 effects = 0;
            (param[0] = new OracleParameter("ESEP_TITLE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_TITLE;
            (param[1] = new OracleParameter("ESEP_FIRST_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_FIRST_NAME;
            (param[2] = new OracleParameter("ESEP_LAST_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_LAST_NAME;
            (param[3] = new OracleParameter("ESEP_EPF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_EPF;
            (param[4] = new OracleParameter("ESEP_NIC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_NIC;
            (param[5] = new OracleParameter("ESEP_DOB", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_DOB;
            (param[6] = new OracleParameter("ESEP_DOJ", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_DOJ;
            (param[7] = new OracleParameter("ESEP_LIVING_ADD_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_LIVING_ADD_1;
            (param[8] = new OracleParameter("ESEP_LIVING_ADD_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_LIVING_ADD_2;
            (param[9] = new OracleParameter("ESEP_LIVING_ADD_3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_LIVING_ADD_3;
            (param[10] = new OracleParameter("ESEP_TEL_HOME_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_TEL_HOME_NO;
            (param[11] = new OracleParameter("ESEP_MOBI_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_MOBI_NO;
            (param[12] = new OracleParameter("ESEP_CON_PER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_CONTRACTOR;
            (param[13] = new OracleParameter("ESEP_CON_PER_MOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
            (param[14] = new OracleParameter("ESEP_ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_ACT;
            (param[15] = new OracleParameter("ESEP_CAT_SUBCD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_CAT_SUBCD;
            (param[16] = new OracleParameter("ESEP_CAT_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_CAT_CD;
            (param[17] = new OracleParameter("ESEP_TOU_LIC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
            (param[18] = new OracleParameter("ESEP_LIC_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_DL_NO;
            (param[19] = new OracleParameter("ESEP_LIC_CAT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_DL_CLASS;
            (param[20] = new OracleParameter("ESEP_LIC_EXDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_DL_EXPDT;
            (param[21] = new OracleParameter("ESEP_LIC_ISSDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_DL_ISSDT;
            (param[22] = new OracleParameter("ESEP_COM_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_COM_CD;
            (param[23] = new OracleParameter("ESEP_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_CD;
            (param[24] = new OracleParameter("ESEP_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_CRE_BY;
            (param[25] = new OracleParameter("ESEP_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_CRE_DT;
            (param[26] = new OracleParameter("ESEP_EMAIL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_EMAIL;
            (param[27] = new OracleParameter("ESEP_ANAL1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
            (param[28] = new OracleParameter("ANAL2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
            (param[29] = new OracleParameter("ANAL3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
            (param[30] = new OracleParameter("ESEP_COST", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = null;
            param[31] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_saveemployeedetails", CommandType.StoredProcedure, param);
            return effects;
        }

        public Int32 UpdateEmployeeData(MST_EMP mst_employee_tbs)
        {
            OracleParameter[] param = new OracleParameter[32];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_ESEP_TITLE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_TITLE;
            (param[1] = new OracleParameter("P_ESEP_FIRST_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_FIRST_NAME;
            (param[2] = new OracleParameter("P_ESEP_LAST_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_LAST_NAME;
            (param[3] = new OracleParameter("P_ESEP_EPF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_EPF;
            (param[4] = new OracleParameter("P_ESEP_NIC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_NIC;
            (param[5] = new OracleParameter("P_ESEP_DOB", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_DOB;
            (param[6] = new OracleParameter("P_ESEP_DOJ", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_DOJ;
            (param[7] = new OracleParameter("P_ESEP_LIVING_ADD_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_LIVING_ADD_1;
            (param[8] = new OracleParameter("P_ESEP_LIVING_ADD_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_LIVING_ADD_2;
            (param[9] = new OracleParameter("P_ESEP_LIVING_ADD_3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_LIVING_ADD_3;
            (param[10] = new OracleParameter("P_ESEP_TEL_HOME_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_TEL_HOME_NO;
            (param[11] = new OracleParameter("P_ESEP_MOBI_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_MOBI_NO;
            (param[12] = new OracleParameter("P_ESEP_CON_PER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_CONTRACTOR;
            (param[13] = new OracleParameter("P_ESEP_CON_PER_MOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
            (param[14] = new OracleParameter("P_ESEP_ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_ACT;
            (param[15] = new OracleParameter("P_ESEP_CAT_SUBCD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_CAT_SUBCD;
            (param[16] = new OracleParameter("P_ESEP_CAT_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_CAT_CD;
            (param[17] = new OracleParameter("P_ESEP_TOU_LIC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
            (param[18] = new OracleParameter("P_ESEP_LIC_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_DL_NO;
            (param[19] = new OracleParameter("P_ESEP_LIC_CAT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_DL_CLASS;
            (param[20] = new OracleParameter("P_ESEP_LIC_EXDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_DL_EXPDT;
            (param[21] = new OracleParameter("P_ESEP_LIC_ISSDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_DL_ISSDT;
            (param[22] = new OracleParameter("P_ESEP_COM_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_COM_CD;
            (param[23] = new OracleParameter("P_ESEP_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_CD;
            (param[24] = new OracleParameter("P_ESEP_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_CRE_BY;
            (param[25] = new OracleParameter("P_ESEP_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_CRE_DT;
            (param[26] = new OracleParameter("P_ESEP_EMAIL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.ESEP_EMAIL;
            (param[27] = new OracleParameter("P_ESEP_ANAL1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
            (param[28] = new OracleParameter("ANAL2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
            (param[29] = new OracleParameter("ANAL3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
            (param[30] = new OracleParameter("P_ESEP_COST", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = null;
            param[31] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_updateemployeedetails", CommandType.StoredProcedure, param);
            return effects;
        }

        public Int32 SaveVessalData(MST_VESSEL mst_vsl_tbs)
        {
            OracleParameter[] param = new OracleParameter[13];
            Int32 effects = 0;
            (param[0] = new OracleParameter("VM_VESSAL_CD ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.VM_VESSAL_CD;
            (param[1] = new OracleParameter("VM_VESSAL_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.VM_VESSAL_NAME;
            (param[2] = new OracleParameter("VM_COUNTRY_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.VM_COUNTRY_CD;
            (param[3] = new OracleParameter("VM_MOS_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.VM_MOS_CD;
            (param[4] = new OracleParameter("VM_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.VM_RMK;
            (param[5] = new OracleParameter("VM_ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = mst_vsl_tbs.VM_ACT;
            (param[6] = new OracleParameter("VM_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.VM_CRE_BY;
            (param[7] = new OracleParameter("VM_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_vsl_tbs.VM_CRE_DT;
            (param[8] = new OracleParameter("VM_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.VM_MOD_BY;
            (param[9] = new OracleParameter("VM_MOD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_vsl_tbs.VM_MOD_DT;
            (param[10] = new OracleParameter("VM_CRE_SESSION_ID", OracleDbType.Int32, null, ParameterDirection.Input)).Value = mst_vsl_tbs.VM_CRE_SESSION_ID;
            (param[11] = new OracleParameter("VM_MOD_SESSION_ID", OracleDbType.Int32, null, ParameterDirection.Input)).Value = mst_vsl_tbs.VM_MOD_SESSION_ID;
            param[12] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_savevesseldetails", CommandType.StoredProcedure, param);
            return effects;
        }

        public Int32 UpdateVessalData(MST_VESSEL mst_vsl_tbs)
        {
            OracleParameter[] param = new OracleParameter[13];
            Int32 effects = 0;
            (param[0] = new OracleParameter("VESSAL_CD ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.VM_VESSAL_CD;
            (param[1] = new OracleParameter("VESSAL_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.VM_VESSAL_NAME;
            (param[2] = new OracleParameter("COUNTRY_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.VM_COUNTRY_CD;
            (param[3] = new OracleParameter("MOS_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.VM_MOS_CD;
            (param[4] = new OracleParameter("RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.VM_RMK;
            (param[5] = new OracleParameter("ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = mst_vsl_tbs.VM_ACT;
            (param[6] = new OracleParameter("CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.VM_CRE_BY;
            (param[7] = new OracleParameter("CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_vsl_tbs.VM_CRE_DT;
            (param[8] = new OracleParameter("MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.VM_MOD_BY;
            (param[9] = new OracleParameter("MOD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_vsl_tbs.VM_MOD_DT;
            (param[10] = new OracleParameter("CRE_SESSION_ID", OracleDbType.Int32, null, ParameterDirection.Input)).Value = mst_vsl_tbs.VM_CRE_SESSION_ID;
            (param[11] = new OracleParameter("MOD_SESSION_ID", OracleDbType.Int32, null, ParameterDirection.Input)).Value = mst_vsl_tbs.VM_MOD_SESSION_ID;
            param[12] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_updatevesseldetails", CommandType.StoredProcedure, param);
            return effects;
        }

        public Int32 SaveCostEleData(MST_VESSEL mst_vsl_tbs)
        {
            OracleParameter[] param = new OracleParameter[12];
            Int32 effects = 0;
            (param[0] = new OracleParameter("MCE_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = mst_vsl_tbs.MCE_SEQ;
            (param[1] = new OracleParameter("MCE_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.MCE_CD;
            (param[2] = new OracleParameter("MCE_DESC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.MCE_DESC;
            (param[3] = new OracleParameter("MCE_ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = mst_vsl_tbs.MCE_ACT;
            (param[4] = new OracleParameter("MCE_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.MCE_CRE_BY;
            (param[5] = new OracleParameter("MCE_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_vsl_tbs.MCE_CRE_DT;
            (param[6] = new OracleParameter("MCE_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.MCE_MOD_BY;
            (param[7] = new OracleParameter("MCE_MOD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_vsl_tbs.MCE_MOD_DT;
            (param[8] = new OracleParameter("MCE_IGNORE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = mst_vsl_tbs.MCE_IGNORE;
            (param[9] = new OracleParameter("MCE_ACC_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.MCE_ACC_CD;
            (param[10] = new OracleParameter("MCE_COST_REV_STS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.MCE_COST_REV_STS;
            param[11] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_savecosteledetails", CommandType.StoredProcedure, param);
            return effects;
        }

        public Int32 UpdateCostEleData(MST_VESSEL mst_vsl_tbs)
        {
            OracleParameter[] param = new OracleParameter[12];
            Int32 effects = 0;
            (param[0] = new OracleParameter("STRMCE_SEQ ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.MCE_SEQ;
            (param[1] = new OracleParameter("STRMCE_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.MCE_CD;
            (param[2] = new OracleParameter("STRMCE_DESC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.MCE_DESC;
            (param[3] = new OracleParameter("STRMCE_ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = mst_vsl_tbs.MCE_ACT;
            (param[4] = new OracleParameter("STRMCE_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.MCE_CRE_BY;
            (param[5] = new OracleParameter("STRMCE_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_vsl_tbs.MCE_CRE_DT;
            (param[6] = new OracleParameter("STRMCE_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.MCE_MOD_BY;
            (param[7] = new OracleParameter("STRMCE_MOD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_vsl_tbs.MCE_MOD_DT;
            (param[8] = new OracleParameter("STRMCE_IGNORE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = mst_vsl_tbs.MCE_IGNORE;
            (param[9] = new OracleParameter("STRMCE_ACC_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.MCE_ACC_CD;
            (param[10] = new OracleParameter("STRMCE_COST_REV_STS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.MCE_COST_REV_STS; 
            param[11] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_updatecosteledetails", CommandType.StoredProcedure, param);
            return effects;
        }
        public Int32 SavePortData(MST_VESSEL mst_vsl_tbs)
        {
            OracleParameter[] param = new OracleParameter[11];
            Int32 effects = 0;
            (param[0] = new OracleParameter("PA_PRT_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.PA_PRT_CD;
            (param[1] = new OracleParameter("PA_PRT_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.PA_PRT_NAME;
            (param[2] = new OracleParameter("PA_CUNTRY_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.PA_CUNTRY_CD;
            (param[3] = new OracleParameter("PA_MOS_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.PA_MOS_CD;
            (param[4] = new OracleParameter("PA_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.PA_RMK;
            (param[5] = new OracleParameter("PA_ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = mst_vsl_tbs.PA_ACT;
            (param[6] = new OracleParameter("PA_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.PA_CRE_BY;
            (param[7] = new OracleParameter("PA_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_vsl_tbs.PA_CRE_DT;
            (param[8] = new OracleParameter("PA_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.PA_MOD_BY;
            (param[9] = new OracleParameter("PA_MOD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_vsl_tbs.PA_MOD_DT;
            param[10] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_saveportdetails", CommandType.StoredProcedure, param);
            return effects;
        }

        public Int32 UpdatePortData(MST_VESSEL mst_vsl_tbs)
        {
            OracleParameter[] param = new OracleParameter[11];
            Int32 effects = 0;
            (param[0] = new OracleParameter("PRT_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.PA_PRT_CD;
            (param[1] = new OracleParameter("PRT_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.PA_PRT_NAME;
            (param[2] = new OracleParameter("CUNTRY_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.PA_CUNTRY_CD;
            (param[3] = new OracleParameter("MOS_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.PA_MOS_CD;
            (param[4] = new OracleParameter("RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.PA_RMK;
            (param[5] = new OracleParameter("ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = mst_vsl_tbs.PA_ACT;
            (param[6] = new OracleParameter("CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.PA_CRE_BY;
            (param[7] = new OracleParameter("CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_vsl_tbs.PA_CRE_DT;
            (param[8] = new OracleParameter("MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_vsl_tbs.PA_MOD_BY;
            (param[9] = new OracleParameter("MOD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_vsl_tbs.PA_MOD_DT;
            param[10] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_updateportdetails", CommandType.StoredProcedure, param);
            return effects;
        }
        /// <summary>
        /// Isuru 2017/05/29
        /// </summary>
        /// <param name="_com"></param>
        /// <param name="_nic"></param>
        /// <param name="_mob"></param>
        /// <param name="_br"></param>
        /// <param name="_pp"></param>
        /// <param name="_dl"></param>
        /// <param name="_type"></param>
        /// <returns></returns>
        //public List<cus_details> CustomerSearchAll(string _com, string _nic, string _mob, string _br, string _pp, string _dl, int _type)
        //{

        //    List<cus_details> _customer = null;

        //    OracleParameter[] param = new OracleParameter[8];
        //    (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
        //    (param[1] = new OracleParameter("p_nic", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _nic;
        //    (param[2] = new OracleParameter("p_dl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _dl;
        //    (param[3] = new OracleParameter("p_br", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _br;
        //    (param[4] = new OracleParameter("p_mob", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _mob;
        //    (param[5] = new OracleParameter("p_pp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pp;
        //    (param[6] = new OracleParameter("p_type", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _type;

        //    param[7] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
        //    DataTable tblDet = QueryDataTable("tblpromoDis", "sp_get_cut_mul_rcd", CommandType.StoredProcedure, false, param);


        //    if (tblDet.Rows.Count > 0)
        //    {
        //        _customer = DataTableExtensions.ToGenericList<cus_details>(tblDet, cus_details.Converter);
        //    }

        //    return _customer;

        //}




        public List<MST_REQ_TYPE> getReqyestTypes(string module)
        {
            try
            {
                List<MST_REQ_TYPE> list = new List<MST_REQ_TYPE>();

                OracleParameter[] param = new OracleParameter[2];

                (param[0] = new OracleParameter("p_module", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = module;
                param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable tblDet = QueryDataTable("tblpromoDis", "SP_GET_REQTYPES", CommandType.StoredProcedure, false, param);


                if (tblDet.Rows.Count > 0)
                {
                    list = DataTableExtensions.ToGenericList<MST_REQ_TYPE>(tblDet, MST_REQ_TYPE.Converter);
                }

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MST_PROFIT_CENTER getProfitCenterDetails(string pccd, string com, string userid)
        {
            MST_PROFIT_CENTER pc = new MST_PROFIT_CENTER();

            OracleParameter[] param = new OracleParameter[4];

            (param[0] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pccd;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[2] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userid;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable tblDet = QueryDataTable("tblpromoDis", "SP_GET_PROFITCENTER", CommandType.StoredProcedure, false, param);


            if (tblDet.Rows.Count > 0)
            {
                pc = DataTableExtensions.ToGenericList<MST_PROFIT_CENTER>(tblDet, MST_PROFIT_CENTER.Converter)[0];
            }

            return pc;
        }
        public MST_EMP getEmployeeDetails(string epf, string com)
        {
            MST_EMP emp = new MST_EMP();

            OracleParameter[] param = new OracleParameter[3];

            (param[0] = new OracleParameter("p_epf", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = epf;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable tblDet = QueryDataTable("tblpromoDis", "SP_GET_EMPDETAILS", CommandType.StoredProcedure, false, param);


            if (tblDet.Rows.Count > 0)
            {
                emp = DataTableExtensions.ToGenericList<MST_EMP>(tblDet, MST_EMP.Converter)[0];
            }

            return emp;
        }
        public MST_EMP getReqEmployeeDetails(string epf, string com)
        {
            MST_EMP emp = new MST_EMP();

            OracleParameter[] param = new OracleParameter[3];

            (param[0] = new OracleParameter("p_epf", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = epf;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable tblDet = QueryDataTable("tblpromoDis", "SP_GET_REQEMPDETAILS", CommandType.StoredProcedure, false, param);


            if (tblDet.Rows.Count > 0)
            {
                emp = DataTableExtensions.ToGenericList<MST_EMP>(tblDet, MST_EMP.Converter)[0];
            }

            return emp;
        }

        public MST_BUSENTITY getConsigneeDetailsByAccCode(string cuscd, string company, string type)
        {
            MST_BUSENTITY res = new MST_BUSENTITY();

            OracleParameter[] param = new OracleParameter[4];

            (param[0] = new OracleParameter("p_cuscd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cuscd;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[2] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable tblDet = QueryDataTable("tblpromoDis", "SP_GET_CUSDETBYACCCD", CommandType.StoredProcedure, false, param);


            if (tblDet.Rows.Count > 0)
            {
                res = DataTableExtensions.ToGenericList<MST_BUSENTITY>(tblDet, MST_BUSENTITY.Converter)[0];
            }

            return res;
        }

        public TRN_PETTYCASH_REQ_HDR getReqyestDetials(string type, string reqno, string company, string userDefPro)
        {
            try
            {
                TRN_PETTYCASH_REQ_HDR res = new TRN_PETTYCASH_REQ_HDR();

                OracleParameter[] param = new OracleParameter[5];

                (param[0] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
                (param[1] = new OracleParameter("p_req", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = reqno;
                (param[2] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[3] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userDefPro;
                param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable tblDet = QueryDataTable("tblpromoDis", "SP_GET_PTYREQDETAILS", CommandType.StoredProcedure, false, param);


                if (tblDet.Rows.Count > 0)
                {
                    res = DataTableExtensions.ToGenericList<TRN_PETTYCASH_REQ_HDR>(tblDet, TRN_PETTYCASH_REQ_HDR.Converter)[0];
                }

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TRN_PETTYCASH_REQ_DTL> getReqyestItemDetials(int seq)
        {
            try
            {
                List<TRN_PETTYCASH_REQ_DTL> itm = new List<TRN_PETTYCASH_REQ_DTL>();
                OracleParameter[] param = new OracleParameter[2];

                (param[0] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = seq;
                param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable tblDet = QueryDataTable("tblpromoDis", "SP_GET_PTYREQITEMDETAILS", CommandType.StoredProcedure, false, param);


                if (tblDet.Rows.Count > 0)
                {
                    itm = DataTableExtensions.ToGenericList<TRN_PETTYCASH_REQ_DTL>(tblDet, TRN_PETTYCASH_REQ_DTL.Converter);
                }

                return itm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public trn_jb_hdr GetJobDetails(string jobno, string company)
        {
            try
            {
                trn_jb_hdr itmList = new trn_jb_hdr();
                OracleParameter[] param = new OracleParameter[3];
                (param[0] = new OracleParameter("p_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobno;
                (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tbl", "SP_GET_JOBDETAILS", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<trn_jb_hdr>(_dtResults, trn_jb_hdr.Converter)[0];
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MST_COST_ELEMENT GetCostElementDetails(string eleCode)
        {
            try
            {
                MST_COST_ELEMENT itmList = new MST_COST_ELEMENT();
                OracleParameter[] param = new OracleParameter[2];
                (param[0] = new OracleParameter("p_elecd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = eleCode;
                param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tbl", "SP_GET_COSTELEDETAILS", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<MST_COST_ELEMENT>(_dtResults, MST_COST_ELEMENT.Converter)[0];
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FTW_MES_TP GetUOMDetails(string uomcd)
        {
            try
            {
                FTW_MES_TP itmList = new FTW_MES_TP();
                OracleParameter[] param = new OracleParameter[2];
                (param[0] = new OracleParameter("p_uomcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = uomcd;
                param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tbl", "SP_GET_UOMDETAILS", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<FTW_MES_TP>(_dtResults, FTW_MES_TP.Converter)[0];
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MST_COM getCompanyDetails(string company)
        {
            try
            {
                MST_COM itmList = new MST_COM();
                OracleParameter[] param = new OracleParameter[2];
                (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tbl", "SP_GET_COMPANYDETAILS", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<MST_COM>(_dtResults, MST_COM.Converter)[0];
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MST_CUR GetCurrencyDetails(string curcd)
        {
            try
            {
                MST_CUR itmList = new MST_CUR();
                OracleParameter[] param = new OracleParameter[2];
                (param[0] = new OracleParameter("p_curcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = curcd;
                param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tbl", "SP_GET_CURRENCYDETAILS", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<MST_CUR>(_dtResults, MST_CUR.Converter)[0];
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FTW_VEHICLE_NO getTelVehLcDet(string code)
        {
            try
            {
                FTW_VEHICLE_NO itmList = new FTW_VEHICLE_NO();
                OracleParameter[] param = new OracleParameter[2];
                (param[0] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
                param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tbl", "SP_GET_TELVEHLCDETAILS", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<FTW_VEHICLE_NO>(_dtResults, FTW_VEHICLE_NO.Converter)[0];
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MasterProfitCenter GetProfitCenter(string _company, string _profitCenter)
        {
            MasterProfitCenter _masterProfitCenter = null;
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_pcenter", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _profitCenter;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblProfitCenter = QueryDataTable("tblPc", "sp_getprofitcenterdetail", CommandType.StoredProcedure, false, param);

            if (_tblProfitCenter.Rows.Count > 0)
            {
                _masterProfitCenter = DataTableExtensions.ToGenericList<MasterProfitCenter>(_tblProfitCenter, MasterProfitCenter.ConvertTotal)[0];
            }

            return _masterProfitCenter;
        }
        public MasterCompany GetUserCompanySet(string _company, string _profitCenter)
        {
            MasterCompany _userList = null;
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_pcenter", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _profitCenter;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblProfitCenter = QueryDataTable("tblPc", "sp_getcompanydetail", CommandType.StoredProcedure, false, param);

            if (_tblProfitCenter.Rows.Count > 0)
            {
                _userList = DataTableExtensions.ToGenericList<MasterCompany>(_tblProfitCenter, MasterCompany.ConverterTotal)[0];
            }

            return _userList;
        }

        public MasterExchangeRate GetExchangeRate(string _com, string _fromCur, DateTime _date, string _toCur, string _pc)
        {
            MasterExchangeRate _list = null;
            try
            {
                OracleParameter[] param = new OracleParameter[6];
                (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
                (param[1] = new OracleParameter("p_curcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _fromCur;
                (param[2] = new OracleParameter("p_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = _date.Date;
                (param[3] = new OracleParameter("p_tocur", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _toCur;
                (param[4] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
                param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

                DataTable _tblData = QueryDataTable("tbData", "sp_get_excrt", CommandType.StoredProcedure, false, param);

                if (_tblData.Rows.Count > 0)
                {
                    _list = DataTableExtensions.ToGenericList<MasterExchangeRate>(_tblData, MasterExchangeRate.Converter)[0];
                }
                return _list;
            }
            catch (Exception) { return null; }
        }

        public int saveRequestHdr(TRN_PETTYCASH_REQ_HDR hdr)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[20];
                Int32 effects = 0;

                (param[0] = new OracleParameter("p_req_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.TPRH_REQ_NO;
                (param[1] = new OracleParameter("p_manual_ref", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.TPRH_MANUAL_REF;
                (param[2] = new OracleParameter("p_req_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = hdr.TPRH_REQ_DT;
                (param[3] = new OracleParameter("p_remarks", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.TPRH_REMARKS;
                (param[4] = new OracleParameter("p_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.TPRH_STUS;
                (param[5] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.TPRH_CRE_BY;
                (param[6] = new OracleParameter("p_cre_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = hdr.TPRH_CRE_DT;
                (param[7] = new OracleParameter("p_tot_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = hdr.TPRH_TOT_AMT;
                (param[8] = new OracleParameter("p_req_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.TPRH_REQ_BY;
                (param[9] = new OracleParameter("p_settle", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.TPRH_SETTLE;
                (param[10] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.TPRH_TYPE;
                (param[11] = new OracleParameter("p_pay_to", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.TPRH_PAY_TO;
                (param[12] = new OracleParameter("p_pay_to_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.TPRH_PAY_TO_NAME;
                (param[13] = new OracleParameter("p_pay_to_add1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.TPRH_PAY_TO_ADD1;
                (param[14] = new OracleParameter("p_pay_to_add2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.TPRH_PAY_TO_ADD2;
                (param[15] = new OracleParameter("p_pc_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.TPRH_PC_CD;
                (param[16] = new OracleParameter("p_payment_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = hdr.TPRH_PAYMENT_DT;
                (param[17] = new OracleParameter("p_com_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.TPRH_COM_CD;
                (param[18] = new OracleParameter("p_cre_session_id", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToInt32(hdr.TPRH_CRE_SESSION_ID);
                param[19] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int32)UpdateRecords("sp_save_pttycshreqhdr", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int saveRequestDtl(TRN_PETTYCASH_REQ_DTL dtl)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[22];
                Int32 effects = 0;

                (param[0] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = dtl.TPRD_SEQ;
                (param[1] = new OracleParameter("p_req_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = dtl.TPRD_REQ_NO;
                (param[2] = new OracleParameter("p_line_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = dtl.TPRD_LINE_NO;
                (param[3] = new OracleParameter("p_element_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = dtl.TPRD_ELEMENT_CD;
                (param[4] = new OracleParameter("p_element_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = dtl.TPRD_ELEMENT_AMT;
                (param[5] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = dtl.TPRD_CRE_BY;
                (param[6] = new OracleParameter("p_cre_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = dtl.TPRD_CRE_DT;
                (param[7] = new OracleParameter("p_element_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = dtl.TPRD_ELEMENT_DESC;
                (param[8] = new OracleParameter("p_currency_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = dtl.TPRD_CURRENCY_CODE;
                (param[9] = new OracleParameter("p_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = dtl.TPRD_JOB_NO;
                (param[10] = new OracleParameter("p_balance_set", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = dtl.TPRD_BALANCE_SET;
                (param[11] = new OracleParameter("p_comments", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = dtl.TPRD_COMMENTS;
                (param[12] = new OracleParameter("p_uom", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = dtl.TPRD_UOM;
                (param[13] = new OracleParameter("p_no_units", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = dtl.TPRD_NO_UNITS;
                (param[14] = new OracleParameter("p_unit_price", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = dtl.TPRD_UNIT_PRICE;
                (param[15] = new OracleParameter("p_ex_rate", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = dtl.TPRD_EX_RATE;
                (param[16] = new OracleParameter("p_upload_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = dtl.TPRD_UPLOAD_DATE;
                (param[17] = new OracleParameter("p_vehtel", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = dtl.TPRD_VEC_TELE;
                (param[18] = new OracleParameter("p_invno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = dtl.TPRD_INV_NO;
                (param[19] = new OracleParameter("p_invdt", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = dtl.TPRD_INV_DT;
                (param[20] = new OracleParameter("p_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = dtl.TPRD_ACT;
                param[21] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int32)UpdateRecords("sp_save_pttycshreqdtl", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int updateApproveHdrStus(TRN_PETTYCASH_REQ_HDR request, Int32 appl)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[18];
                Int32 effects = 0;

                (param[0] = new OracleParameter("P_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = request.TPRH_SEQ;
                (param[1] = new OracleParameter("P_req_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = request.TPRH_REQ_NO;
                (param[2] = new OracleParameter("P_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = request.TPRH_STUS;
                (param[3] = new OracleParameter("P_app_1", OracleDbType.Int32, null, ParameterDirection.Input)).Value = request.TPRH_APP_1;
                (param[4] = new OracleParameter("P_app_1_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = request.TPRH_APP_1_BY;
                (param[5] = new OracleParameter("P_app_1_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = request.TPRH_APP_1_DT;
                (param[6] = new OracleParameter("P_app_2", OracleDbType.Int32, null, ParameterDirection.Input)).Value = request.TPRH_APP_2;
                (param[7] = new OracleParameter("P_app_2_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = request.TPRH_APP_2_BY;
                (param[8] = new OracleParameter("P_app_2_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = request.TPRH_APP_2_DT;
                (param[9] = new OracleParameter("P_app_3", OracleDbType.Int32, null, ParameterDirection.Input)).Value = request.TPRH_APP_3;
                (param[10] = new OracleParameter("P_app_3_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = request.TPRH_APP_3_BY;
                (param[11] = new OracleParameter("P_app_3_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = request.TPRH_APP_3_DT;
                (param[12] = new OracleParameter("P_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = request.TPRH_MOD_BY;
                (param[13] = new OracleParameter("P_mod_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = request.TPRH_MOD_DT;
                (param[14] = new OracleParameter("p_applvl", OracleDbType.Int32, null, ParameterDirection.Input)).Value = appl;
                (param[15] = new OracleParameter("p_mod_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = request.TPRH_MOD_SESSION_ID;
                (param[16] = new OracleParameter("p_paydate", OracleDbType.Date, null, ParameterDirection.Input)).Value = request.TPRH_PAYMENT_DT;
                param[17] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int32)UpdateRecords("SP_UPDATE_APPROVEREQUEST", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //dilshan
        public int updateReprintDocStus(string request, string userId)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[3];
                Int32 effects = 0;

                (param[0] = new OracleParameter("P_request", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = request;
                (param[1] = new OracleParameter("P_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userId;
                param[2] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int32)UpdateRecords("sp_update_reprintrequest", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int updateReprintDocStus_New(string request, string company)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[3];
                Int32 effects = 0;

                (param[0] = new OracleParameter("P_docNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = request;
                (param[1] = new OracleParameter("P_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                param[2] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int32)UpdateRecords("sp_update_doreprint", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public int SendSMS(OutSMS _smsout)
        //{
        //    try
        //    {
        //        int effect = 0;
        //        //OpenEMS();
        //        OracleParameter[] param = new OracleParameter[17];

        //        (param[0] = new OracleParameter("p_createtime", OracleDbType.Date, null, ParameterDirection.Input)).Value = _smsout.Createtime;
        //        (param[1] = new OracleParameter("p_deletedtime", OracleDbType.Date, null, ParameterDirection.Input)).Value = _smsout.Deletedtime;
        //        (param[2] = new OracleParameter("p_deliveredtime", OracleDbType.Date, null, ParameterDirection.Input)).Value = _smsout.Deliveredtime;
        //        (param[3] = new OracleParameter("p_downloadtime", OracleDbType.Date, null, ParameterDirection.Input)).Value = _smsout.Downloadtime;
        //        (param[4] = new OracleParameter("p_msg", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _smsout.Msg;
        //        (param[5] = new OracleParameter("p_msgid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _smsout.Msgid;
        //        (param[6] = new OracleParameter("p_msgstatus", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _smsout.Msgstatus;
        //        (param[7] = new OracleParameter("p_msgtype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _smsout.Msgtype;
        //        (param[8] = new OracleParameter("p_receivedtime", OracleDbType.Date, null, ParameterDirection.Input)).Value = _smsout.Receivedtime;
        //        (param[9] = new OracleParameter("p_receiver", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _smsout.Receiver;
        //        (param[10] = new OracleParameter("p_receiverphno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _smsout.Receiverphno;
        //        (param[11] = new OracleParameter("p_refdocno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _smsout.Refdocno;
        //        (param[12] = new OracleParameter("p_sender", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _smsout.Sender;
        //        (param[13] = new OracleParameter("p_senderphno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _smsout.Senderphno;
        //        (param[14] = new OracleParameter("p_seqno", OracleDbType.Int64, null, ParameterDirection.Input)).Value = _smsout.Seqno;
        //        (param[15] = new OracleParameter("p_comcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _smsout.comcode;
        //        param[16] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
        //        effect = (Int32)UpdateRecords("sp_savesmsout", CommandType.StoredProcedure, param);
        //        //ConnectionCloseEMS();
        //        return effect;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public List<TRN_PETTYCASH_REQ_HDR> getPendingData(string company, string pc, DateTime fromdt, DateTime tdt, int applvl)
        {
            try
            {
                List<TRN_PETTYCASH_REQ_HDR> itmList = new List<TRN_PETTYCASH_REQ_HDR>();
                OracleParameter[] param = new OracleParameter[6];
                (param[0] = new OracleParameter("p_applvl", OracleDbType.Int32, null, ParameterDirection.Input)).Value = applvl;
                (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[2] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
                (param[3] = new OracleParameter("p_frmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = fromdt.Date;
                (param[4] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = tdt.Date;
                param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tbl", "SP_GET_PENDINGREQ", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<TRN_PETTYCASH_REQ_HDR>(_dtResults, TRN_PETTYCASH_REQ_HDR.ConverterSub);
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TRN_PETTYCASH_REQ_HDR loadRequestDetailsbySeq(int seq, string company, string userDefPro)
        {
            try
            {
                TRN_PETTYCASH_REQ_HDR res = new TRN_PETTYCASH_REQ_HDR();
                OracleParameter[] param = new OracleParameter[4];
                (param[0] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = seq;
                (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[2] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userDefPro;
                param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable tblDet = QueryDataTable("tblpromoDis", "SP_GET_PTYREQDETAILSBYSEQ", CommandType.StoredProcedure, false, param);


                if (tblDet.Rows.Count > 0)
                {
                    res = DataTableExtensions.ToGenericList<TRN_PETTYCASH_REQ_HDR>(tblDet, TRN_PETTYCASH_REQ_HDR.Converter)[0];
                }

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getPetyCshRptData(int reqSeq, string type)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[3];
                (param[0] = new OracleParameter("p_reqseq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = reqSeq;
                (param[1] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
                param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable tblDet = QueryDataTable("tblpromoDis", "SP_GET_PETTYCASHRPT", CommandType.StoredProcedure, false, param);
                return tblDet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getCompanyDetailsBycd(string company)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable tblDet = QueryDataTable("tblpromoDis", "SP_GET_COMDESC", CommandType.StoredProcedure, false, param);
            return tblDet;
        }

        public int rejectPtyCshRequest(int sqNo, string userId, DateTime dt, string sessionid)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[5];
                Int32 effects = 0;

                (param[0] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = sqNo;
                (param[1] = new OracleParameter("p_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userId;
                (param[2] = new OracleParameter("p_mod_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = dt;
                (param[3] = new OracleParameter("p_mod_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = sessionid;
                param[4] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int32)UpdateRecords("SP_UPDATE_REJECTSTUS", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //SUBODANA 2017-07-12
        public int saveInvoiceHDr(trn_inv_hdr hdr, out Int32 seqno)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[43];
                Int32 effects = 0;
                seqno = 0;
                (param[0] = new OracleParameter("p_tih_inv_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.Tih_inv_no;
                (param[1] = new OracleParameter("p_tih_com_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.Tih_com_cd;
                (param[2] = new OracleParameter("p_tih_pc_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.Tih_pc_cd;
                (param[3] = new OracleParameter("p_tih_man_ref_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.Tih_man_ref_no;
                (param[4] = new OracleParameter("p_tih_inv_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = hdr.Tih_inv_dt;
                (param[5] = new OracleParameter("p_tih_cus_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.Tih_cus_cd;
                (param[6] = new OracleParameter("p_tih_inv_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = hdr.Tih_inv_amt;
                (param[7] = new OracleParameter("p_tih_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.Tih_job_no;
                (param[8] = new OracleParameter("p_tih_rmk", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.Tih_rmk;
                (param[9] = new OracleParameter("p_tih_inv_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.Tih_inv_tp;
                (param[10] = new OracleParameter("p_tih_inv_sub_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.Tih_inv_sub_tp;
                (param[11] = new OracleParameter("p_tih_ex_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.Tih_ex_cd;
                (param[12] = new OracleParameter("p_tih_bal_settle_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = hdr.Tih_bal_settle_amt;
                (param[13] = new OracleParameter("p_tih_settle_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = hdr.Tih_settle_amt;
                (param[14] = new OracleParameter("p_tih_bl_m_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.Tih_bl_m_no;
                (param[15] = new OracleParameter("p_tih_bl_h_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.Tih_bl_h_no;
                (param[16] = new OracleParameter("p_tih_bl_d_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.Tih_bl_d_no;
                (param[17] = new OracleParameter("p_tih_inv_party_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.Tih_inv_party_cd;
                (param[18] = new OracleParameter("p_tih_acc_cus_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.Tih_acc_cus_cd;
                (param[19] = new OracleParameter("p_tih_acc_cus_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.Tih_acc_cus_name;
                (param[20] = new OracleParameter("p_tih_acc_cus_add1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.Tih_acc_cus_add1;
                (param[21] = new OracleParameter("p_tih_acc_cus_add2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.Tih_acc_cus_add2;
                (param[22] = new OracleParameter("p_tih_sun_upload", OracleDbType.Int32, null, ParameterDirection.Input)).Value = hdr.Tih_sun_upload;
                (param[23] = new OracleParameter("p_tih_is_print", OracleDbType.Int32, null, ParameterDirection.Input)).Value = hdr.Tih_is_print;
                (param[24] = new OracleParameter("p_tih_is_cancel", OracleDbType.Int32, null, ParameterDirection.Input)).Value = hdr.Tih_is_cancel;
                (param[25] = new OracleParameter("p_tih_cancel_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.Tih_cancel_by;
                (param[26] = new OracleParameter("p_tih_cancel_when", OracleDbType.Date, null, ParameterDirection.Input)).Value = hdr.Tih_cancel_when;
                (param[27] = new OracleParameter("p_tih_inv_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.Tih_inv_status;
                (param[28] = new OracleParameter("p_tih_doc_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.Tih_doc_type;
                (param[29] = new OracleParameter("p_tih_inv_curr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.Tih_inv_curr;
                (param[30] = new OracleParameter("p_tih_other_ref_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.Tih_other_ref_no;
                (param[31] = new OracleParameter("p_tih_is_svat", OracleDbType.Int32, null, ParameterDirection.Input)).Value = hdr.Tih_is_svat;
                (param[32] = new OracleParameter("p_tih_pono", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.Tih_pono;
                (param[33] = new OracleParameter("p_tih_due_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = hdr.Tih_due_date;
                (param[34] = new OracleParameter("p_tih_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.Tih_cre_by;
                (param[35] = new OracleParameter("p_tih_cre_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = hdr.Tih_cre_dt;
                (param[36] = new OracleParameter("p_tih_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.Tih_mod_by;
                (param[37] = new OracleParameter("p_tih_mod_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = hdr.Tih_mod_dt;
                (param[38] = new OracleParameter("p_tih_tot_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = hdr.Tih_tot_amt;
                (param[39] = new OracleParameter("p_tih_exec_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.Tih_exec_name;
                (param[40] = new OracleParameter("p_tih_inv_cred_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.Tih_inv_cred_no;
                param[41] = new OracleParameter("O_SEQ", OracleDbType.Int32, null, ParameterDirection.Output);
                param[42] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int32)UpdateRecords("sp_save_inv_hdr", CommandType.StoredProcedure, param);
                if (effects > 0) seqno = Convert.ToInt32(param[41].Value.ToString());
                return effects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //SUBODANA 2017/07/12
        public int SaveInvDetails(trn_inv_det details)
        {
            OracleParameter[] param = new OracleParameter[27];
            (param[0] = new OracleParameter("p_tid_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = details.Tid_seq_no;
            (param[1] = new OracleParameter("p_tid_line_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = details.Tid_line_no;
            (param[2] = new OracleParameter("p_tid_inv_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = details.Tid_inv_no;
            (param[3] = new OracleParameter("p_tid_cha_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = details.Tid_cha_code;
            (param[4] = new OracleParameter("p_tid_cha_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = details.Tid_cha_desc;
            (param[5] = new OracleParameter("p_tid_cha_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = details.Tid_cha_amt;
            (param[6] = new OracleParameter("p_tid_is_rev", OracleDbType.Int32, null, ParameterDirection.Input)).Value = details.Tid_is_rev;
            (param[7] = new OracleParameter("p_tid_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = details.Tid_qty;
            (param[8] = new OracleParameter("p_tid_units", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = details.Tid_units;
            (param[9] = new OracleParameter("p_tid_unit_amnt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = details.Tid_unit_amnt;
            (param[10] = new OracleParameter("p_tid_curr_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = details.Tid_curr_cd;
            (param[11] = new OracleParameter("p_tid_ex_rate", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = details.Tid_ex_rate;
            (param[12] = new OracleParameter("p_tid_cha_rate", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = details.Tid_cha_rate;
            (param[13] = new OracleParameter("p_tid_merge_chacode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = details.Tid_merge_chacode;
            (param[14] = new OracleParameter("p_tid_rmk", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = details.Tid_rmk;
            (param[15] = new OracleParameter("p_tid_merge_val", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = details.Tid_merge_val;
            (param[16] = new OracleParameter("p_tid_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = details.Tid_job_no;
            (param[17] = new OracleParameter("p_tid_bl_m_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = details.Tid_bl_m_no;
            (param[18] = new OracleParameter("p_tid_bl_h_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = details.Tid_bl_h_no;
            (param[19] = new OracleParameter("p_tid_bl_d_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = details.Tid_bl_d_no;
            (param[20] = new OracleParameter("p_tid_invr_merge_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = details.Tid_invr_merge_line;
            (param[21] = new OracleParameter("p_tid_ser_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = details.Tid_ser_cd;
            (param[22] = new OracleParameter("p_tid_inv_method", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = details.Tid_inv_method;
            (param[23] = new OracleParameter("p_tid_docline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = details.Tid_docline;
            (param[24] = new OracleParameter("p_tid_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = details.Tid_doc_no;
            (param[25] = new OracleParameter("p_tid_com_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = details.Tid_com_cd;
            param[26] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            Int32 result = UpdateRecords("sp_save_inv_det", CommandType.StoredProcedure, param);
            return result;
        }

        public List<TRN_PETTYCASH_REQ_HDR> loadPendingSetReq(string company, string pc, string type, DateTime fmdt, DateTime tdt, string jobno)
        {
            try
            {
                List<TRN_PETTYCASH_REQ_HDR> itmList = new List<TRN_PETTYCASH_REQ_HDR>();
                OracleParameter[] param = new OracleParameter[7];
                (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
                (param[2] = new OracleParameter("p_frmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = fmdt.Date;
                (param[3] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = tdt.Date;
                (param[4] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
                (param[5] = new OracleParameter("p_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobno;
                param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tbl", "SP_GET_PENDINGSETLEREQ", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<TRN_PETTYCASH_REQ_HDR>(_dtResults, TRN_PETTYCASH_REQ_HDR.ConverterSubWithJobNo);
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TRN_PETTYCASH_SETTLE_HDR loadSettlementHdr(string company, string pc, string reqNo)
        {
            try
            {
                TRN_PETTYCASH_SETTLE_HDR res = new TRN_PETTYCASH_SETTLE_HDR();
                OracleParameter[] param = new OracleParameter[4];
                //(param[0] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = reqSeq;
                (param[0] = new OracleParameter("p_reqno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = reqNo;
                (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[2] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
                param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable tblDet = QueryDataTable("tblpromoDis", "SP_GET_SETLMNTREQDETAILSBYSEQ", CommandType.StoredProcedure, false, param);


                if (tblDet.Rows.Count > 0)
                {
                    res = DataTableExtensions.ToGenericList<TRN_PETTYCASH_SETTLE_HDR>(tblDet, TRN_PETTYCASH_SETTLE_HDR.Converter)[0];
                }
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TRN_PETTYCASH_SETTLE_DTL> loadSettlementDet(string company, string pc, string reqNo, Int32 reqSeq)
        {
            try
            {
                List<TRN_PETTYCASH_SETTLE_DTL> res = new List<TRN_PETTYCASH_SETTLE_DTL>();
                OracleParameter[] param = new OracleParameter[5];
                (param[0] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = reqSeq;
                (param[1] = new OracleParameter("p_reqno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = reqNo;
                (param[2] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[3] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
                param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable tblDet = QueryDataTable("tblpromoDis", "SP_GET_SETLMNTREQDETBYSEQ", CommandType.StoredProcedure, false, param);


                if (tblDet.Rows.Count > 0)
                {
                    res = DataTableExtensions.ToGenericList<TRN_PETTYCASH_SETTLE_DTL>(tblDet, TRN_PETTYCASH_SETTLE_DTL.Converter);
                }
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int saveSetleRequestHdr(TRN_PETTYCASH_SETTLE_HDR hdr)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[14];
                Int32 effects = 0;

                (param[0] = new OracleParameter("p_settle_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.TPSH_SETTLE_NO;
                (param[1] = new OracleParameter("p_man_ref", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.TPSH_MAN_REF;
                (param[2] = new OracleParameter("p_settle_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = hdr.TPSH_SETTLE_DT;
                (param[3] = new OracleParameter("p_remarks", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.TPSH_REMARKS;
                (param[4] = new OracleParameter("p_settle_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = hdr.TPSH_SETTLE_AMT;
                (param[5] = new OracleParameter("p_pc_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.TPSH_PC_CD;
                (param[6] = new OracleParameter("p_pay_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = hdr.TPSH_PAY_DT;
                (param[7] = new OracleParameter("p_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.TPSH_STUS;
                (param[8] = new OracleParameter("p_reject", OracleDbType.Int32, null, ParameterDirection.Input)).Value = hdr.TPSH_REJECT;
                (param[9] = new OracleParameter("p_com_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.TPSH_COM_CD;
                (param[10] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.TPSH_CRE_BY;
                (param[11] = new OracleParameter("p_cre_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = hdr.TPSH_CRE_DT;
                (param[12] = new OracleParameter("p_sesid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = hdr.TPSH_CRE_SES_ID;
                param[13] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int32)UpdateRecords("SP_SAVE_SETTLEMENTHDR", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int saveRequestDtl(TRN_PETTYCASH_SETTLE_DTL RQ)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[17];
                Int32 effects = 0;

                (param[0] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = RQ.TPSD_SEQ_NO;
                (param[1] = new OracleParameter("p_settle_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = RQ.TPSD_SETTLE_NO;
                (param[2] = new OracleParameter("p_line_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = RQ.TPSD_LINE_NO;
                (param[3] = new OracleParameter("p_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = RQ.TPSD_JOB_NO;
                (param[4] = new OracleParameter("p_element_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = RQ.TPSD_ELEMENT_CD;
                (param[5] = new OracleParameter("p_element_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = RQ.TPSD_ELEMENT_DESC;
                (param[6] = new OracleParameter("p_req_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = RQ.TPSD_REQ_AMT;
                (param[7] = new OracleParameter("p_settle_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = RQ.TPSD_SETTLE_AMT;
                (param[8] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = RQ.TPSD_CRE_BY;
                (param[9] = new OracleParameter("p_cre_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = RQ.TPSD_CRE_DT;
                (param[10] = new OracleParameter("p_att_receipt", OracleDbType.Int32, null, ParameterDirection.Input)).Value = RQ.TPSD_ATT_RECEIPT;
                (param[11] = new OracleParameter("p_req_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = RQ.TPSD_REQ_NO;
                (param[12] = new OracleParameter("p_remarks", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = RQ.TPSD_REMARKS;
                (param[13] = new OracleParameter("p_vec_tele", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = RQ.TPSD_VEC_TELE;
                (param[14] = new OracleParameter("p_settle_lineno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = RQ.TPSD_SETLE_LINO_NO;
                (param[15] = new OracleParameter("p_receipt_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = RQ.TPSD_RECEIPT_NO;
                param[16] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int32)UpdateRecords("SP_SAVE_SETTLEMENTDTL", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int updateSettlementRequest(string reqno, string userid, DateTime credt, string sessionid)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[6];
                Int32 effects = 0;

                (param[0] = new OracleParameter("p_req_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = reqno;
                (param[1] = new OracleParameter("p_settle", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "S";
                (param[2] = new OracleParameter("p_mod_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = sessionid;
                (param[3] = new OracleParameter("p_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userid;
                (param[4] = new OracleParameter("p_mod_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = credt;
                param[5] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int32)UpdateRecords("SP_UPDATE_PTYREQSETSTUS", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int updateSetlementApproveStus(TRN_PETTYCASH_SETTLE_HDR request, int appl)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[18];
                Int32 effects = 0;

                (param[0] = new OracleParameter("P_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = request.TPSH_SEQ_NO;
                (param[1] = new OracleParameter("P_req_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = request.TPSH_SETTLE_NO;
                (param[2] = new OracleParameter("P_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = request.TPSH_STUS;
                (param[3] = new OracleParameter("P_app_1", OracleDbType.Int32, null, ParameterDirection.Input)).Value = request.TPSH_APP1;
                (param[4] = new OracleParameter("P_app_1_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = request.TPSH_APP1_BY;
                (param[5] = new OracleParameter("P_app_1_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = request.TPSH_APP1_DT;
                (param[6] = new OracleParameter("P_app_2", OracleDbType.Int32, null, ParameterDirection.Input)).Value = request.TPSH_APP2;
                (param[7] = new OracleParameter("P_app_2_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = request.TPSH_APP2_BY;
                (param[8] = new OracleParameter("P_app_2_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = request.TPSH_APP2_DT;
                (param[9] = new OracleParameter("P_app_3", OracleDbType.Int32, null, ParameterDirection.Input)).Value = request.TPSH_APP3;
                (param[10] = new OracleParameter("P_app_3_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = request.TPSH_APP3_BY;
                (param[11] = new OracleParameter("P_app_3_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = request.TPSH_APP3_DT;
                (param[12] = new OracleParameter("P_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = request.TPSH_MOD_BY;
                (param[13] = new OracleParameter("P_mod_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = request.TPSH_MOD_DT;
                (param[14] = new OracleParameter("p_applvl", OracleDbType.Int32, null, ParameterDirection.Input)).Value = appl;
                (param[15] = new OracleParameter("p_mod_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = request.TPSH_MOD_SES_ID;
                (param[16] = new OracleParameter("p_paydate", OracleDbType.Date, null, ParameterDirection.Input)).Value = request.TPSH_PAY_DT;
                param[17] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int32)UpdateRecords("SP_UPDATE_APPROVESETTLEMENT", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int rejectSettlementRequest(string reqno, string userId, DateTime date, string sessionid)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[5];
                Int32 effects = 0;

                (param[0] = new OracleParameter("p_setno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = reqno;
                (param[1] = new OracleParameter("p_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userId;
                (param[2] = new OracleParameter("p_mod_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = date;
                (param[3] = new OracleParameter("p_mod_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = sessionid;
                param[4] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int32)UpdateRecords("SP_UPDATE_SETLMENTREJECTSTUS", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int updateSettlementRequestStatus(string reqNo)
        {
            OracleParameter[] param = new OracleParameter[3];
            Int32 effects = 0;

            (param[0] = new OracleParameter("p_reqno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = reqNo;
            (param[1] = new OracleParameter("p_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "P";
            param[2] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDATE_SETLMENTREQSTUS", CommandType.StoredProcedure, param);
            return effects;
        }
        //subodana 2017-07-14
        public int SaveInvoiceTaxItem(trn_inv_itmtax _itms)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[13];
                Int32 effects = 0;
                (param[0] = new OracleParameter("p_tiit_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _itms.Tiit_seq_no;
                (param[1] = new OracleParameter("p_tiit_inv_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itms.Tiit_inv_no;
                (param[2] = new OracleParameter("p_tiid_com_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itms.Tiid_com_cd;
                (param[3] = new OracleParameter("p_tiit_tax_element", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itms.Tiit_tax_element;
                (param[4] = new OracleParameter("p_tiid_tax_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itms.Tiid_tax_type;
                (param[5] = new OracleParameter("p_tiid_tax_rate", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _itms.Tiid_tax_rate;
                (param[6] = new OracleParameter("p_tiid_tax_unc_rate", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _itms.Tiid_tax_unc_rate;
                (param[7] = new OracleParameter("p_tiid_tax_clb_rate", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _itms.Tiid_tax_clb_rate;
                (param[8] = new OracleParameter("p_tiid_tax_unc_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _itms.Tiid_tax_unc_amt;
                (param[9] = new OracleParameter("p_tiid_tax_clb_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _itms.Tiid_tax_clb_amt;
                (param[10] = new OracleParameter("p_tiid_tax_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itms.Tiid_tax_job_no;
                (param[11] = new OracleParameter("p_tiid_tax_ser_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itms.Tiid_tax_ser_cd;
                param[12] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int32)UpdateRecords("sp_save_inv_tax", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<PaymentType> GetPossiblePaymentTypes_new(string _com, string _party, string _cd, string txn_tp, DateTime today, Int32 _isBOCN)
        {
            List<PaymentType> _userList = null;

            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_party", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _party;
            (param[2] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cd;
            (param[3] = new OracleParameter("p_txn_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = txn_tp;
            (param[4] = new OracleParameter("p_current_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = today;
            (param[5] = new OracleParameter("p_is_BOCN", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _isBOCN;
            param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblUserProf", "sp_get_posible_pay_tps_new", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _userList = DataTableExtensions.ToGenericList<PaymentType>(_dtResults, PaymentType.Converter);
            }

            return _userList;
        }
        public string getBankCode(string bankId)
        {
            string code = null;
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_bank_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = bankId;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblBnkCd", "SP_GET_BANKCDFRMID", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                code = _dtResults.Rows[0]["MBI_CD"].ToString();
            }
            return code;
        }
        public DataTable get_bank_mid_code(string branch_code, string pc, int mode, int period, DateTime _trdate, string _com)
        {

            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_bank", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = branch_code;
            (param[1] = new OracleParameter("p_profit_cen", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
            (param[2] = new OracleParameter("p_mode", OracleDbType.Int32, null, ParameterDirection.Input)).Value = mode;
            (param[3] = new OracleParameter("p_period", OracleDbType.Int32, null, ParameterDirection.Input)).Value = period;
            (param[4] = new OracleParameter("p_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = _trdate;
            (param[5] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable tblDet = QueryDataTable("tbl_det", "sp_get_bank_mid", CommandType.StoredProcedure, false, param);
            return tblDet;
        }
        public MasterOutsideParty GetOutSidePartyDetailsById(string _code)
        {
            MasterOutsideParty _party = new MasterOutsideParty();

            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _code;

            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblParty = QueryDataTable("tblParty", "sp_get_bany_by_id", CommandType.StoredProcedure, false, param);
            if (_tblParty.Rows.Count > 0)
            {
                _party = DataTableExtensions.ToGenericList<MasterOutsideParty>(_tblParty, MasterOutsideParty.ConvertTotal)[0];
            }

            return _party;

        }
        public DataTable GetBankCC(string _bank)
        {

            OracleParameter[] param1 = new OracleParameter[2];
            (param1[0] = new OracleParameter("p_bank_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _bank;
            param1[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblInvoiceDet = QueryDataTable("tblRecItm", "sp_get_bank_cctp", CommandType.StoredProcedure, false, param1);
            return _tblInvoiceDet;
        }
        public string getAdvanceRefAmount(string cuscd, string company, string receiptno)
        {
            string amnt = null;
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_customer", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cuscd;
            (param[1] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[2] = new OracleParameter("p_recetype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "ADVAN";
            (param[3] = new OracleParameter("p_debcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "CASH";
            (param[4] = new OracleParameter("p_receiptno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = receiptno;
            param[5] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblBnkCd", "SP_GET_ADVANCEREFAMT", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                amnt = _dtResults.Rows[0]["SAR_USED_AMT"].ToString();
            }
            return amnt;
        }
        public string getCreditRefAmount(string cuscd, string company, string refNo, string profcen)
        {
            string amnt = null;
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_customer", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cuscd;
            (param[1] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[2] = new OracleParameter("p_profcen", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = profcen;
            (param[3] = new OracleParameter("p_refno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = refNo;
            (param[4] = new OracleParameter("p_waveoffval", OracleDbType.Int32, null, ParameterDirection.Input)).Value = vaweOffVal(company);
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblBnkCd", "SP_GET_CREDITNTEREFAMT", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                amnt = _dtResults.Rows[0]["CREDIT_AMT"].ToString();
            }
            return amnt;
        }
        public int vaweOffVal(string company)
        {

            int vaweOff = 0;
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblProf", "SP_GET_WAVEOFFVAL", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0 && _dtResults.Rows[0]["MC_WAVEOFF_VAL"].ToString() != "")
            {
                vaweOff = Convert.ToInt32(_dtResults.Rows[0]["MC_WAVEOFF_VAL"].ToString());
            }
            return vaweOff;
        }
        public List<GiftVoucherPages> GetGiftVoucherPages(string _com, int _page)
        {
            OracleParameter[] param = new OracleParameter[3];
            List<GiftVoucherPages> _gift = null;
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_page", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _page;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.
            DataTable _dtResults = QueryDataTable("tblInvoiceSearch", "sp_get_gv", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                _gift = DataTableExtensions.ToGenericList<GiftVoucherPages>(_dtResults, GiftVoucherPages.Converter);
            }
            return _gift;
        }
        public DataTable GetGVAlwCom(string _comCode, string _itm, Int32 _act)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _comCode;
            (param[1] = new OracleParameter("p_itm", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itm;
            (param[2] = new OracleParameter("p_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _act;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblPendingConsRequests", "sp_getredalwcom", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        public LoyaltyMemeber getLoyaltyDetails(string customer, string loyalNu)
        {

            OracleParameter[] param = new OracleParameter[3];
            LoyaltyMemeber loyCus = new LoyaltyMemeber();
            (param[0] = new OracleParameter("p_customer", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = customer;
            (param[1] = new OracleParameter("p_loyaltnum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = loyalNu;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_LOYALTYDETAILS", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                loyCus = DataTableExtensions.ToGenericList<LoyaltyMemeber>(_dtResults, LoyaltyMemeber.Converter)[0];
            }
            return loyCus;

        }
        public LoyaltyPointRedeemDefinition GetLoyaltyRedeemDefinition(string prtTp, string prt, DateTime date, string loyalty)
        {

            LoyaltyPointRedeemDefinition type = null;
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_loyalty", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = loyalty;
            (param[1] = new OracleParameter("p_partytype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = prtTp;
            (param[2] = new OracleParameter("p_partycode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = prt;
            (param[3] = new OracleParameter("p_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = date;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tbl = QueryDataTable("tbl", "sp_get_loyalty_redeem_def", CommandType.StoredProcedure, false, param);

            if (_tbl.Rows.Count > 0)
            {
                type = DataTableExtensions.ToGenericList<LoyaltyPointRedeemDefinition>(_tbl, LoyaltyPointRedeemDefinition.Converter)[0];
            }
            return type;
        }
        public BlackListCustomers GetBlackListCustomerDetails(string _cusCode, Int32 _active)
        {
            BlackListCustomers _blackListCustomers = new BlackListCustomers();

            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_cus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cusCode;
            (param[1] = new OracleParameter("p_active", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _active;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblBlackListCustomers = QueryDataTable("tblBlackListCustomers", "sp_checkcusblacklist", CommandType.StoredProcedure, false, param);
            if (_tblBlackListCustomers.Rows.Count > 0)
            {
                _blackListCustomers = DataTableExtensions.ToGenericList<BlackListCustomers>(_tblBlackListCustomers, BlackListCustomers.Converter)[0];
            }

            return _blackListCustomers;


        }
        public DataTable get_Dep_Bank_Name(string _com, string _pc, string _paytp, string _acc)
        {
            OracleParameter[] param = new OracleParameter[5];

            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            (param[2] = new OracleParameter("p_pay_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _paytp;
            (param[3] = new OracleParameter("p_acc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _acc;

            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "sp_get_dep_bnk_name", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        public GiftVoucherPages getGiftVoucherPage(string voucherNo, string voucherBook, string company)
        {
            OracleParameter[] param = new OracleParameter[4];
            GiftVoucherPages vouPge = new GiftVoucherPages();
            (param[0] = new OracleParameter("p_voucheNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = voucherNo;
            (param[1] = new OracleParameter("p_voucheBook", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = voucherBook;
            (param[2] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_GIFTVOUCHERBOOK", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                vouPge = DataTableExtensions.ToGenericList<GiftVoucherPages>(_dtResults, GiftVoucherPages.Converter)[0];
            }
            return vouPge;
        }
        public MasterItem GetItem(string _company, string _item)
        {
            MasterItem _itemList = null;
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _itemTable = QueryDataTable("tblItem", "get_allitemdetails", CommandType.StoredProcedure, false, param);
            if (_itemTable.Rows.Count > 0)
            {
                _itemList = DataTableExtensions.ToGenericList<MasterItem>(_itemTable, MasterItem.ConvertTotal)[0];
            }
            return _itemList;
        }
        public GiftVoucherPages GetGiftVoucherPage(string _com, string _pc, string _item, int _book, int _page, string _prefix)
        {

            GiftVoucherPages _gift = null;
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            (param[2] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[3] = new OracleParameter("p_book", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _book;
            (param[4] = new OracleParameter("p_page", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _page;
            (param[5] = new OracleParameter("p_prefix", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _prefix;
            param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _table = QueryDataTable("_tblBalance", "SP_GETAPPROVEDPAGEDETAIL", CommandType.StoredProcedure, false, param);
            if (_table.Rows.Count > 0)
            {
                _gift = DataTableExtensions.ToGenericList<GiftVoucherPages>(_table, GiftVoucherPages.Converter)[0];
            }
            return _gift;

        }
        public DataTable GetReceipt(string _doc)
        {

            OracleParameter[] param = new OracleParameter[2];

            (param[0] = new OracleParameter("p_doc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _doc;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);


            DataTable _tblData = QueryDataTable("tbl", "sp_getreceipt", CommandType.StoredProcedure, false, param);

            return _tblData;
        }
        public MasterOutsideParty GetOutSidePartyDetails(string _code, string _type)
        {
            MasterOutsideParty _party = new MasterOutsideParty();

            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _code;
            (param[1] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _type;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblParty = QueryDataTable("tblParty", "sp_getbuscomdetails", CommandType.StoredProcedure, false, param);
            if (_tblParty.Rows.Count > 0)
            {
                _party = DataTableExtensions.ToGenericList<MasterOutsideParty>(_tblParty, MasterOutsideParty.ConvertTotal)[0];
            }

            return _party;

        }
        public DataTable Get_buscom_branch_det(string bus_cd)
        {

            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("bus_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = bus_cd;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblParty = new DataTable();
            _tblParty = QueryDataTable("tbldet", "sp_get_buscom_branch_det", CommandType.StoredProcedure, false, param);

            return _tblParty;
        }
        public DataTable PettyCash_SettlementDetls(int reqSeq, string comCode)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[3];
                (param[0] = new OracleParameter("p_reqseq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = reqSeq;
                (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comCode;
                param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable tblDet = QueryDataTable("tblSettlementDtl", "SP_GET_PETTY_CASH_SETT", CommandType.StoredProcedure, false, param);
                return tblDet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TRN_PETTYCASH_SETTLE_HDR load_PtyC_Setl_RequestDetails(int seq, string company, string userDefPro)
        {
            try
            {
                TRN_PETTYCASH_SETTLE_HDR res = new TRN_PETTYCASH_SETTLE_HDR();
                OracleParameter[] param = new OracleParameter[4];
                (param[0] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = seq;
                (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[2] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userDefPro;
                param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable tblDet = QueryDataTable("tblprycshSetl", "SP_GET_PTYCS_STLE_REQDETAILS", CommandType.StoredProcedure, false, param);
                if (tblDet.Rows.Count > 0)
                {
                    res = DataTableExtensions.ToGenericList<TRN_PETTYCASH_SETTLE_HDR>(tblDet, TRN_PETTYCASH_SETTLE_HDR.Converter)[0];
                }
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int updateRequestHdr(TRN_PETTYCASH_REQ_HDR request)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[8];
                Int32 effects = 0;
                (param[0] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = request.TPRH_SEQ;
                (param[1] = new OracleParameter("p_manref", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = request.TPRH_MANUAL_REF;
                (param[2] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = request.TPRH_PC_CD;
                (param[3] = new OracleParameter("p_rmk", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = request.TPRH_REMARKS;
                (param[4] = new OracleParameter("p_modby", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = request.TPRH_MOD_BY;
                (param[5] = new OracleParameter("p_moddt", OracleDbType.Date, null, ParameterDirection.Input)).Value = request.TPRH_MOD_DT;
                (param[6] = new OracleParameter("p_modsess", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = request.TPRH_MOD_SESSION_ID;
                param[7] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int32)UpdateRecords("SP_UPDATE_PTUREQUEST", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int updateActivePtyDet(TRN_PETTYCASH_REQ_HDR request)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[4];
                Int32 effects = 0;
                (param[0] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = request.TPRH_SEQ;
                (param[1] = new OracleParameter("p_modby", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = request.TPRH_MOD_BY;
                (param[2] = new OracleParameter("p_moddt", OracleDbType.Date, null, ParameterDirection.Input)).Value = request.TPRH_MOD_DT;
                param[3] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int32)UpdateRecords("SP_UPDATE_PTUREQUESTDTL", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string requestTypeDesc(Int32 seq)
        {
            string typedesc = "";
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = seq;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblParty = new DataTable();
            _tblParty = QueryDataTable("tbldet", "SP_GET_REQTYPEDESC", CommandType.StoredProcedure, false, param);
            if (_tblParty.Rows.Count > 0)
            {
                if (_tblParty.Rows[0]["DESCRIPTION"] != DBNull.Value)
                {
                    typedesc = _tblParty.Rows[0]["DESCRIPTION"].ToString();
                }
            }
            return typedesc;
        }
        public DataTable Inv_Details(string InvNo, string company, string type)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[4];
                (param[0] = new OracleParameter("p_InvNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = InvNo;
                (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[2] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
                param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable tblDet = QueryDataTable("tblinvDtl", "SP_GET_INVOICE_DTL", CommandType.StoredProcedure, false, param);

                return tblDet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Int32 setInvoicePrintedStatus(string invoice, string company)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_invoice", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = invoice;
            (param[1] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            param[2] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            Int32 result = UpdateRecords("SP_UPDATE_ISPRINTED", CommandType.StoredProcedure, param);
            return result;
        }
        public MasterReceiptDivision GetDefRecDivision(string _com, string _pc)
        {
            MasterReceiptDivision _RecDivision = new MasterReceiptDivision();

            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblReceiptDivision = QueryDataTable("tblrecdiv", "sp_getdefdiv", CommandType.StoredProcedure, false, param);
            if (_tblReceiptDivision.Rows.Count > 0)
            {
                _RecDivision = DataTableExtensions.ToGenericList<MasterReceiptDivision>(_tblReceiptDivision, MasterReceiptDivision.Converter)[0];
            }

            return _RecDivision;
        }
        public List<MasterItemTax> GetItemTax(string _company, string _item, string _status, string _taxCode, string _taxRateCode)
        {
            //sp_getitemtax   (p_com in NVARCHAR2,p_item in NVARCHAR2,p_status in NVARCHAR2 , p_taxcode in NVARCHAR2,p_taxratecode in NVARCHAR2, c_data  OUT sys_refcursor)
            List<MasterItemTax> _list = new List<MasterItemTax>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[2] = new OracleParameter("p_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _status;
            (param[3] = new OracleParameter("p_taxcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _taxCode;
            (param[4] = new OracleParameter("p_taxratecode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _taxRateCode;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _tblData = QueryDataTable("tblData", "sp_getitemtax", CommandType.StoredProcedure, false, param);
            if (_tblData.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<MasterItemTax>(_tblData, MasterItemTax.ConvertTotal);
            }

            return _list;


        }
        public List<MasterItemTax> GetItemTaxEffDt(string _company, string _item, string _status, string _taxCode, string _taxRateCode, DateTime _effDate)
        {
            List<MasterItemTax> _list = new List<MasterItemTax>();
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[2] = new OracleParameter("p_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _status;
            (param[3] = new OracleParameter("p_taxcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _taxCode;
            (param[4] = new OracleParameter("p_taxratecode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _taxRateCode;
            (param[5] = new OracleParameter("p_effDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _effDate;
            param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _tblData = QueryDataTable("tblData", "sp_getitemtaxEffDt", CommandType.StoredProcedure, false, param);
            if (_tblData.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<MasterItemTax>(_tblData, MasterItemTax.ConvertTotal);
            }

            return _list;

        }
        public List<LogMasterItemTax> GetItemTaxLog(string _company, string _item, string _status, string _taxCode, string _taxRateCode, DateTime _effDate)
        {
            List<LogMasterItemTax> _list = new List<LogMasterItemTax>();
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[2] = new OracleParameter("p_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _status;
            (param[3] = new OracleParameter("p_taxcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _taxCode;
            (param[4] = new OracleParameter("p_taxratecode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _taxRateCode;
            (param[5] = new OracleParameter("p_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = _effDate;
            param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _tblData = QueryDataTable("tblData", "sp_getitemtaxlog", CommandType.StoredProcedure, false, param);
            if (_tblData.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<LogMasterItemTax>(_tblData, LogMasterItemTax.ConvertTotal);
            }

            return _list;
        }
        //subodana 2017-07-20
        public List<trn_inv_hdr> GetInvHdr(string doc, string com)
        {
            List<trn_inv_hdr> itmList = new List<trn_inv_hdr>();
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_COMPANY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("P_INVNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = doc;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_INV_HDR", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                itmList = DataTableExtensions.ToGenericList<trn_inv_hdr>(_dtResults, trn_inv_hdr.Converter);
            }
            return itmList;
        }
        //subodana 2017-07-20
        public List<trn_inv_det> Get_Inv_det(string seq)
        {
            List<trn_inv_det> itmList = new List<trn_inv_det>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_SEQ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = seq;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_INV_ITEMS", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                itmList = DataTableExtensions.ToGenericList<trn_inv_det>(_dtResults, trn_inv_det.Converter);
            }
            return itmList;
        }
        public List<trn_inv_det> Get_Inv_detApp(string seq)
        {
            List<trn_inv_det> itmList = new List<trn_inv_det>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_SEQ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = seq;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_INV_ITEMSAPP", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                itmList = DataTableExtensions.ToGenericList<trn_inv_det>(_dtResults, trn_inv_det.Converter);
            }
            return itmList;
        }
        public Int32 SaveReceiptHeader(RecieptHeader _recieptHeader)
        {
            OracleParameter[] param = new OracleParameter[61];
            Int32 effects = 0;

            (param[0] = new OracleParameter("p_acc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_acc_no;
            (param[1] = new OracleParameter("p_act", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_act;
            (param[2] = new OracleParameter("p_anal_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_anal_1;
            (param[3] = new OracleParameter("p_anal_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_anal_2;
            (param[4] = new OracleParameter("p_anal_3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_anal_3;
            (param[5] = new OracleParameter("p_anal_4", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_anal_4;
            (param[6] = new OracleParameter("p_anal_5", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_anal_5;
            (param[7] = new OracleParameter("p_anal_6", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_anal_6;
            (param[8] = new OracleParameter("p_anal_7", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_anal_7;
            (param[9] = new OracleParameter("p_anal_8", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_anal_8;
            (param[10] = new OracleParameter("p_anal_9", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_anal_9;
            (param[11] = new OracleParameter("p_com_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_com_cd;
            (param[12] = new OracleParameter("p_comm_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_comm_amt;
            (param[13] = new OracleParameter("p_create_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_create_by;
            (param[14] = new OracleParameter("p_create_when", OracleDbType.Date, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_create_when;
            (param[15] = new OracleParameter("p_currency_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_currency_cd;
            (param[16] = new OracleParameter("p_debtor_add_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_debtor_add_1;
            (param[17] = new OracleParameter("p_debtor_add_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_debtor_add_2;
            (param[18] = new OracleParameter("p_debtor_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_debtor_cd;
            (param[19] = new OracleParameter("p_debtor_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_debtor_name;
            (param[20] = new OracleParameter("p_direct", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_direct;
            (param[21] = new OracleParameter("p_direct_deposit_bank_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_direct_deposit_bank_cd;
            (param[22] = new OracleParameter("p_direct_deposit_branch", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_direct_deposit_branch;
            (param[23] = new OracleParameter("p_epf_rate", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_epf_rate;
            (param[24] = new OracleParameter("p_esd_rate", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_esd_rate;
            (param[25] = new OracleParameter("p_is_mgr_iss", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_is_mgr_iss;
            (param[26] = new OracleParameter("p_is_oth_shop", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_is_oth_shop;
            (param[27] = new OracleParameter("p_is_used", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_is_used;
            (param[28] = new OracleParameter("p_manual_ref_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_manual_ref_no;
            (param[29] = new OracleParameter("p_mob_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_mob_no;
            (param[30] = new OracleParameter("p_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_mod_by;
            (param[31] = new OracleParameter("p_mod_when", OracleDbType.Date, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_mod_when;
            (param[32] = new OracleParameter("p_nic_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_nic_no;
            (param[33] = new OracleParameter("p_oth_sr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_oth_sr;
            (param[34] = new OracleParameter("p_prefix", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_prefix;
            (param[35] = new OracleParameter("p_profit_center_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_profit_center_cd;
            //(param[36] = new OracleParameter("p_receipt_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_receipt_date;
            (param[36] = new OracleParameter("p_receipt_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_receipt_date.Date;//DateTime.Now; // Added by Chathura
            (param[37] = new OracleParameter("p_receipt_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_receipt_no;
            (param[38] = new OracleParameter("p_receipt_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_receipt_type;
            (param[39] = new OracleParameter("p_ref_doc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_ref_doc;
            (param[40] = new OracleParameter("p_remarks", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_remarks;
            (param[41] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_seq_no;
            (param[42] = new OracleParameter("p_ser_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_ser_job_no;
            (param[43] = new OracleParameter("p_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_session_id;
            (param[44] = new OracleParameter("p_tel_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_tel_no;
            (param[45] = new OracleParameter("p_tot_settle_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_tot_settle_amt;
            (param[46] = new OracleParameter("p_uploaded_to_finance", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_uploaded_to_finance;
            (param[47] = new OracleParameter("p_used_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_used_amt;
            (param[48] = new OracleParameter("p_wht_rate", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_wht_rate;
            (param[49] = new OracleParameter("p_is_dayend", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_is_dayend;//Added by Prabhath on 20/06/2013
            (param[50] = new OracleParameter("p_sar_valid_to", OracleDbType.Date, null, ParameterDirection.Input)).Value = _recieptHeader.SAR_VALID_TO;
            (param[51] = new OracleParameter("p_sar_inv_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_inv_type;
            (param[52] = new OracleParameter("p_sar_scheme", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_scheme;
            (param[53] = new OracleParameter("p_sar_mgr_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.SAR_MGR_CD;
            (param[54] = new OracleParameter("p_sar_colect_mgr_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.SAR_COLECT_MGR_CD;
            (param[55] = new OracleParameter("p_SAR_BK_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.SAR_BK_NO;
            (param[56] = new OracleParameter("p_sar_free_reg", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _recieptHeader.SAR_FREE_REG;
            (param[57] = new OracleParameter("p_subrec_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_subrec_tp;
            (param[58] = new OracleParameter("p_itmpr_validto", OracleDbType.Date, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_itmpr_validto;
            (param[59] = new OracleParameter("p_refund_validto", OracleDbType.Date, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_refund_validto;

            param[60] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int16)UpdateRecords("sp_save_satreceipt", CommandType.StoredProcedure, param);
            return effects;

        }
        public Int32 SaveReceiptItem(RecieptItem _recieptItem)
        {
            OracleParameter[] param = new OracleParameter[25];
            Int32 effects = 0;

            (param[0] = new OracleParameter("p_anal_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_anal_1;
            (param[1] = new OracleParameter("p_anal_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_anal_2;
            (param[2] = new OracleParameter("p_anal_3", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptItem.Sard_anal_3;
            (param[3] = new OracleParameter("p_anal_4", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptItem.Sard_anal_4;
            (param[4] = new OracleParameter("p_anal_5", OracleDbType.Date, null, ParameterDirection.Input)).Value = _recieptItem.Sard_anal_5;
            (param[5] = new OracleParameter("p_cc_expiry_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _recieptItem.Sard_cc_expiry_dt;
            (param[6] = new OracleParameter("p_cc_is_promo", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _recieptItem.Sard_cc_is_promo;
            (param[7] = new OracleParameter("p_cc_period", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _recieptItem.Sard_cc_period;
            (param[8] = new OracleParameter("p_cc_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_cc_tp;
            (param[9] = new OracleParameter("p_chq_bank_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_chq_bank_cd;
            (param[10] = new OracleParameter("p_chq_branch", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_chq_branch;
            (param[11] = new OracleParameter("p_credit_card_bank", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_credit_card_bank;
            (param[12] = new OracleParameter("p_deposit_bank_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_deposit_bank_cd;
            (param[13] = new OracleParameter("p_deposit_branch", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_deposit_branch;
            (param[14] = new OracleParameter("p_gv_issue_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _recieptItem.Sard_gv_issue_dt;
            (param[15] = new OracleParameter("p_gv_issue_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_gv_issue_loc;
            (param[16] = new OracleParameter("p_inv_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_inv_no;
            (param[17] = new OracleParameter("p_line_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _recieptItem.Sard_line_no;
            (param[18] = new OracleParameter("p_pay_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_pay_tp;
            (param[19] = new OracleParameter("p_receipt_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_receipt_no;
            (param[20] = new OracleParameter("p_ref_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_ref_no;
            (param[21] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _recieptItem.Sard_seq_no;
            (param[22] = new OracleParameter("p_settle_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptItem.Sard_settle_amt;
            (param[23] = new OracleParameter("p_sim_ser", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_sim_ser;
            param[24] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int16)UpdateRecords("sp_save_satreceiptitm", CommandType.StoredProcedure, param);
            return effects;
        }
        //Udaya 26.07.2017 Get Data for Manifest Letter
        public DataTable GetManifestLetter_Dtl(DateTime frmDate, DateTime toDate, string docNo, string comCode)
        {
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_fromdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmDate;
            (param[1] = new OracleParameter("p_todate", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDate;
            (param[2] = new OracleParameter("p_docNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docNo;
            (param[3] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comCode;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblManiFest", "SP_GET_MANIFEST_LTER_DTL", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        //Udaya 26.07.2017 Get Data for Cargo Manifest
        public DataTable Get_CargoManifest_Dtl(DateTime frmDate, DateTime toDate, string docNo, string comCode)
        {
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_fromdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmDate;
            (param[1] = new OracleParameter("p_todate", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDate;
            (param[2] = new OracleParameter("p_docNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docNo;
            (param[3] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comCode;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblCargoManiFest", "SP_GET_CARGO_MANIFEST_DTL", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        //Udaya 27.07.2017 Get Data for Delivary Order
        public DataTable Get_Container_Dtl(string docNo)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_docNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docNo;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblCntainer", "SP_GET_CONTAINER_DTL", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        //dilshan 06/03/2018
        public DataTable Get_Container_Dtlcount(string docNo)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_docNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docNo;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblCntainer", "SP_GET_CONTAINER_DTLCOUNT", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        //Udaya 27.07.2017 Get Data for Delivary Order
        public DataTable Get_DeliveryOrder_Dtl(DateTime frmDate, DateTime toDate, string docNo, string comCode)
        {
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_fromdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmDate;
            (param[1] = new OracleParameter("p_todate", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDate;
            (param[2] = new OracleParameter("p_docNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docNo;
            (param[3] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comCode;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblDeliveryOrder", "SP_GET_DELIVERY_ORDER_DTL", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        public bool IsValidReceiptType(string _company, string _type)
        {
            OracleParameter[] param = new OracleParameter[3];
            bool _isValid = false;
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _type;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblvalidsupplier = QueryDataTable("tblvalidrectype", "sp_checkvalidrectype", CommandType.StoredProcedure, false, param);
            if (_tblvalidsupplier.Rows.Count <= 0)
                _isValid = false;
            else _isValid = true;
            return _isValid;
        }
        public int UpdateInvoiceBalance(string invno, decimal bal)
        {
            OracleParameter[] param = new OracleParameter[3];
            //(param[0] = new OracleParameter("P_SEQ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = invno;
            //(param[1] = new OracleParameter("P_SETTLE_AMT", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal;
            (param[0] = new OracleParameter("P_INV_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = invno;
            (param[1] = new OracleParameter("P_SETTLE_AMT", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal;
            param[2] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            Int32 result = UpdateRecords("SP_UPDATE_INV_BAL", CommandType.StoredProcedure, param);
            return result;
        }
        public RecieptHeader GetReceiptHeader(string _com, string _pc, string _doc)
        {
            RecieptHeader _ReceiptList = null;

            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            (param[2] = new OracleParameter("p_doc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _doc;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblReceiptHeader", "SP_GETRECEIPTHEADERDT", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _ReceiptList = DataTableExtensions.ToGenericList<RecieptHeader>(_dtResults, RecieptHeader.ConvertTotal)[0];
            }
            return _ReceiptList;
        }
        public List<RecieptItem> GetReceiptDetails(string receipt_no)
        {
            List<RecieptItem> _RecItemList = null;
            RecieptItem _RecItems = null;

            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_doc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = receipt_no;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);


            //Query Data base.        
            DataTable _dtResults = QueryDataTable("tblRecItems", "sp_getreceiptdetails", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _RecItemList = new List<RecieptItem>();

                for (int i = 0; i < _dtResults.Rows.Count; i++)
                {
                    _RecItems = new RecieptItem();

                    _RecItems.Sard_seq_no = (_dtResults.Rows[i]["Sard_seq_no"] == DBNull.Value) ? 0 : Convert.ToInt32(_dtResults.Rows[i]["Sard_seq_no"]);
                    _RecItems.Sard_line_no = (_dtResults.Rows[i]["Sard_line_no"] == DBNull.Value) ? 0 : Convert.ToInt16(_dtResults.Rows[i]["Sard_line_no"]);
                    _RecItems.Sard_receipt_no = (_dtResults.Rows[i]["Sard_receipt_no"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_receipt_no"].ToString();
                    _RecItems.Sard_inv_no = (_dtResults.Rows[i]["Sard_inv_no"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_inv_no"].ToString();
                    _RecItems.Sard_pay_tp = (_dtResults.Rows[i]["Sard_pay_tp"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_pay_tp"].ToString();
                    _RecItems.Sard_ref_no = (_dtResults.Rows[i]["Sard_ref_no"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_ref_no"].ToString();
                    _RecItems.Sard_chq_bank_cd = (_dtResults.Rows[i]["Sard_chq_bank_cd"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_chq_bank_cd"].ToString();
                    _RecItems.Sard_chq_branch = (_dtResults.Rows[i]["Sard_chq_branch"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_chq_branch"].ToString();
                    _RecItems.Sard_deposit_bank_cd = (_dtResults.Rows[i]["Sard_deposit_bank_cd"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_deposit_bank_cd"].ToString();
                    _RecItems.Sard_deposit_branch = (_dtResults.Rows[i]["Sard_deposit_branch"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_deposit_branch"].ToString();
                    _RecItems.Sard_credit_card_bank = (_dtResults.Rows[i]["Sard_credit_card_bank"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_credit_card_bank"].ToString();
                    _RecItems.Sard_cc_tp = (_dtResults.Rows[i]["Sard_cc_tp"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_cc_tp"].ToString();
                    _RecItems.Sard_cc_expiry_dt = (_dtResults.Rows[i]["Sard_cc_expiry_dt"] == DBNull.Value) ? DateTime.Today : Convert.ToDateTime(_dtResults.Rows[i]["Sard_cc_expiry_dt"]);
                    _RecItems.Sard_cc_is_promo = (_dtResults.Rows[i]["Sard_cc_is_promo"] == DBNull.Value) ? false : Convert.ToBoolean(_dtResults.Rows[i]["Sard_cc_is_promo"]);
                    _RecItems.Sard_cc_period = (_dtResults.Rows[i]["Sard_cc_period"] == DBNull.Value) ? 0 : Convert.ToInt16(_dtResults.Rows[i]["Sard_cc_period"]);
                    _RecItems.Sard_gv_issue_loc = (_dtResults.Rows[i]["Sard_gv_issue_loc"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_gv_issue_loc"].ToString();
                    _RecItems.Sard_gv_issue_dt = (_dtResults.Rows[i]["Sard_gv_issue_dt"] == DBNull.Value) ? DateTime.Today : Convert.ToDateTime(_dtResults.Rows[i]["Sard_gv_issue_dt"]);
                    _RecItems.Sard_settle_amt = (_dtResults.Rows[i]["Sard_settle_amt"] == DBNull.Value) ? 0 : Convert.ToDecimal(_dtResults.Rows[i]["Sard_settle_amt"]);
                    _RecItems.Sard_sim_ser = (_dtResults.Rows[i]["Sard_sim_ser"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_sim_ser"].ToString();
                    _RecItems.Sard_anal_1 = (_dtResults.Rows[i]["Sard_anal_1"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_anal_1"].ToString();
                    _RecItems.Sard_anal_2 = (_dtResults.Rows[i]["Sard_anal_2"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_anal_2"].ToString();
                    _RecItems.Sard_anal_3 = (_dtResults.Rows[i]["Sard_anal_3"] == DBNull.Value) ? 0 : Convert.ToDecimal(_dtResults.Rows[i]["Sard_anal_3"]);
                    _RecItems.Sard_anal_4 = (_dtResults.Rows[i]["Sard_anal_4"] == DBNull.Value) ? 0 : Convert.ToDecimal(_dtResults.Rows[i]["Sard_anal_4"]);
                    _RecItems.Sard_anal_5 = (_dtResults.Rows[i]["Sard_anal_5"] == DBNull.Value) ? DateTime.Today : Convert.ToDateTime(_dtResults.Rows[i]["Sard_anal_5"]);
                    _RecItemList.Add(_RecItems);
                }
            }
            return _RecItemList;
        }
        public List<RecieptItem> GetReceiptDetailsWithinvno(string inv_no)
        {
            List<RecieptItem> _RecItemList = null;
            RecieptItem _RecItems = null;

            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_doc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = inv_no;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);


            //Query Data base.        
            DataTable _dtResults = QueryDataTable("tblRecItems", "sp_get_pay_det", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _RecItemList = new List<RecieptItem>();

                for (int i = 0; i < _dtResults.Rows.Count; i++)
                {
                    _RecItems = new RecieptItem();

                    _RecItems.Sard_seq_no = (_dtResults.Rows[i]["Sard_seq_no"] == DBNull.Value) ? 0 : Convert.ToInt32(_dtResults.Rows[i]["Sard_seq_no"]);
                    _RecItems.Sard_line_no = (_dtResults.Rows[i]["Sard_line_no"] == DBNull.Value) ? 0 : Convert.ToInt16(_dtResults.Rows[i]["Sard_line_no"]);
                    _RecItems.Sard_receipt_no = (_dtResults.Rows[i]["Sard_receipt_no"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_receipt_no"].ToString();
                    _RecItems.Sard_inv_no = (_dtResults.Rows[i]["Sard_inv_no"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_inv_no"].ToString();
                    _RecItems.Sard_pay_tp = (_dtResults.Rows[i]["Sard_pay_tp"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_pay_tp"].ToString();
                    _RecItems.Sard_ref_no = (_dtResults.Rows[i]["Sard_ref_no"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_ref_no"].ToString();
                    _RecItems.Sard_chq_bank_cd = (_dtResults.Rows[i]["Sard_chq_bank_cd"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_chq_bank_cd"].ToString();
                    _RecItems.Sard_chq_branch = (_dtResults.Rows[i]["Sard_chq_branch"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_chq_branch"].ToString();
                    _RecItems.Sard_deposit_bank_cd = (_dtResults.Rows[i]["Sard_deposit_bank_cd"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_deposit_bank_cd"].ToString();
                    _RecItems.Sard_deposit_branch = (_dtResults.Rows[i]["Sard_deposit_branch"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_deposit_branch"].ToString();
                    _RecItems.Sard_credit_card_bank = (_dtResults.Rows[i]["Sard_credit_card_bank"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_credit_card_bank"].ToString();
                    _RecItems.Sard_cc_tp = (_dtResults.Rows[i]["Sard_cc_tp"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_cc_tp"].ToString();
                    _RecItems.Sard_cc_expiry_dt = (_dtResults.Rows[i]["Sard_cc_expiry_dt"] == DBNull.Value) ? DateTime.Today : Convert.ToDateTime(_dtResults.Rows[i]["Sard_cc_expiry_dt"]);
                    _RecItems.Sard_cc_is_promo = (_dtResults.Rows[i]["Sard_cc_is_promo"] == DBNull.Value) ? false : Convert.ToBoolean(_dtResults.Rows[i]["Sard_cc_is_promo"]);
                    _RecItems.Sard_cc_period = (_dtResults.Rows[i]["Sard_cc_period"] == DBNull.Value) ? 0 : Convert.ToInt16(_dtResults.Rows[i]["Sard_cc_period"]);
                    _RecItems.Sard_gv_issue_loc = (_dtResults.Rows[i]["Sard_gv_issue_loc"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_gv_issue_loc"].ToString();
                    _RecItems.Sard_gv_issue_dt = (_dtResults.Rows[i]["Sard_gv_issue_dt"] == DBNull.Value) ? DateTime.Today : Convert.ToDateTime(_dtResults.Rows[i]["Sard_gv_issue_dt"]);
                    _RecItems.Sard_settle_amt = (_dtResults.Rows[i]["Sard_settle_amt"] == DBNull.Value) ? 0 : Convert.ToDecimal(_dtResults.Rows[i]["Sard_settle_amt"]);
                    _RecItems.Sard_sim_ser = (_dtResults.Rows[i]["Sard_sim_ser"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_sim_ser"].ToString();
                    _RecItems.Sard_anal_1 = (_dtResults.Rows[i]["Sard_anal_1"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_anal_1"].ToString();
                    _RecItems.Sard_anal_2 = (_dtResults.Rows[i]["Sard_anal_2"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_anal_2"].ToString();
                    _RecItems.Sard_anal_3 = (_dtResults.Rows[i]["Sard_anal_3"] == DBNull.Value) ? 0 : Convert.ToDecimal(_dtResults.Rows[i]["Sard_anal_3"]);
                    _RecItems.Sard_anal_4 = (_dtResults.Rows[i]["Sard_anal_4"] == DBNull.Value) ? 0 : Convert.ToDecimal(_dtResults.Rows[i]["Sard_anal_4"]);
                    _RecItems.Sard_anal_5 = (_dtResults.Rows[i]["Sard_anal_5"] == DBNull.Value) ? DateTime.Today : Convert.ToDateTime(_dtResults.Rows[i]["Sard_anal_5"]);
                    _RecItemList.Add(_RecItems);
                }
            }
            return _RecItemList;
        }
        //Udaya 28.07.2017 Get Data for Draft
        public DataTable Get_Draft_Dtl(DateTime frmDate, DateTime toDate, string docNo, string comCode)
        {
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_fromdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmDate;
            (param[1] = new OracleParameter("p_todate", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDate;
            (param[2] = new OracleParameter("p_docNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docNo;
            (param[3] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comCode;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblDraft", "SP_GET_DRAFT_DTL", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        //Udaya 28.07.2017 Get Data for House rpt (Air Wise Bill Report)
        public DataTable Get_House_Dtl(DateTime frmDate, DateTime toDate, string docNo, string comCode)
        {
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_fromdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmDate;
            (param[1] = new OracleParameter("p_todate", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDate;
            (param[2] = new OracleParameter("p_docNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docNo;
            (param[3] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comCode;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblHouse", "SP_GET_HOUSE_DTL", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        //Udaya 31.07.2017 Get Data for Sales rpt
        public DataTable Get_Sales_Dtl(DateTime frmDate, DateTime toDate, string docNo, string comCode)
        {
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_fromdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmDate;
            (param[1] = new OracleParameter("p_todate", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDate;
            (param[2] = new OracleParameter("p_docNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docNo;
            (param[3] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comCode;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblSales", "SP_GET_SALES_DTL", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        public DataTable GetContainerType()
        {// Nadeeka
            OracleParameter[] param = new OracleParameter[1];

            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblRcc", "sp_getContainer", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        //Udaya 01.08.2017 Get Data for Debtors Outstanding rpt
        public DataTable Get_Debtors_Outstanding(DateTime frmDate, DateTime toDate, string cusCode, string comCode, string proCntCode, string userId)
        {
            OracleParameter[] param = new OracleParameter[7];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmDate;
            (param[2] = new OracleParameter("tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDate;
            (param[3] = new OracleParameter("in_cust", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cusCode;
            (param[4] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comCode;
            (param[5] = new OracleParameter("in_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = proCntCode;
            (param[6] = new OracleParameter("in_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userId;
            DataTable _dtResults = QueryDataTable("tblDebtorsOutstanding", "SP_GET_DEBTOR_OUTSTANDING", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        //DILSHAN 
        public DataTable Get_Debtors_Out(DateTime frmDate, DateTime toDate, string cusCode, string comCode, string proCntCode, string userId)
        {
            OracleParameter[] param = new OracleParameter[7];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmDate;
            (param[2] = new OracleParameter("tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDate;
            (param[3] = new OracleParameter("in_cust", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cusCode;
            (param[4] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comCode;
            (param[5] = new OracleParameter("in_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = proCntCode;
            (param[6] = new OracleParameter("in_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userId;
            DataTable _dtResults = QueryDataTable("tblDebtorsOutstanding", "SP_GET_DEBTOR_OUTSTANDING_RPT", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        public List<trn_inv_hdr> GetInvHdr_Dtls(string JobNo, string modOfShpmnt, string typOfShpmnt, string cusCode, string hbl, string comCode)
        {
            List<trn_inv_hdr> itmList = new List<trn_inv_hdr>();
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_jobNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = JobNo;
            (param[1] = new OracleParameter("p_modOfShpmnt", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = modOfShpmnt;
            (param[2] = new OracleParameter("p_typOfShpmnt", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = typOfShpmnt;
            (param[3] = new OracleParameter("p_cusCode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cusCode;
            (param[4] = new OracleParameter("p_hbl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hbl;
            (param[5] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comCode;
            param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblInvEnquiry", "SP_GET_INV_HDR_SER", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                itmList = DataTableExtensions.ToGenericList<trn_inv_hdr>(_dtResults, trn_inv_hdr.Converter);
            }
            return itmList;
        }
        public DataTable GetSunPC(string type, string Com)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            return QueryDataTable("boireq", "SP_GetSunPC", CommandType.StoredProcedure, false, param);
        }
        //Udaya 05.08.2017 Get Data for Payment Voucher Details Enquiry
        public List<TRN_PETTYCASH_REQ_DTL> GetVou_Dtls(string ReqNo, string SeqNo)
        {
            List<TRN_PETTYCASH_REQ_DTL> VouList = new List<TRN_PETTYCASH_REQ_DTL>();
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_ReqNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = ReqNo;
            (param[1] = new OracleParameter("p_SeqNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = SeqNo;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblPayVouEnquiry", "SP_GET_VOU_DTL", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                VouList = DataTableExtensions.ToGenericList<TRN_PETTYCASH_REQ_DTL>(_dtResults, TRN_PETTYCASH_REQ_DTL.Converter);
            }
            return VouList;
        }
        //Udaya 05.08.2017 Get Data for Payment Voucher Header Enquiry
        public List<TRN_PETTYCASH_REQ_HDR> GetVou_Hdr(DateTime frmDate, DateTime toDate, string reqNo, string jobNo, string manRefNo, string proCnt)
        {
            List<TRN_PETTYCASH_REQ_HDR> VouHdrList = new List<TRN_PETTYCASH_REQ_HDR>();
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_frmDate", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmDate;
            (param[1] = new OracleParameter("p_toDate", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDate;
            (param[2] = new OracleParameter("p_reqNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = reqNo;
            (param[3] = new OracleParameter("p_jobNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobNo;
            (param[4] = new OracleParameter("p_manRefNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = manRefNo;
            (param[5] = new OracleParameter("p_proCnt", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = proCnt;
            param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblPayVouHdrEnquiry", "SP_GET_PAYVOUC_HDR_SER", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                VouHdrList = DataTableExtensions.ToGenericList<TRN_PETTYCASH_REQ_HDR>(_dtResults, TRN_PETTYCASH_REQ_HDR.ConverterEnquiry);
            }
            return VouHdrList;
        }
        public List<SUN_JURNAL> GetSunJurnalnew(String Com)
        {
            List<SUN_JURNAL> oResult = null;
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dtTemp = QueryDataTable("IMP_OP_ITM", "SP_GETGRNALDATANEW", CommandType.StoredProcedure, false, param);

            if (dtTemp.Rows.Count > 0)
            {
                oResult = DataTableExtensions.ToGenericList<SUN_JURNAL>(dtTemp, SUN_JURNAL.Converter);
            }
            return oResult;
        }
        public List<SUNINVHDR> GetSunInvdatanew(String Com, string pc, DateTime sdate, DateTime edate)
        {
            List<SUNINVHDR> oResult = null;
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("P_COMPANY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[1] = new OracleParameter("P_PROCEN", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
            (param[2] = new OracleParameter("P_ST_DATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = sdate;
            (param[3] = new OracleParameter("P_END_DATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = edate;
            param[4] = new OracleParameter("O_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dtTemp = QueryDataTable("IMP_OP_ITM", "SP_GET_SALESHDRSUNUPLOAD", CommandType.StoredProcedure, false, param);

            if (dtTemp.Rows.Count > 0)
            {
                oResult = DataTableExtensions.ToGenericList<SUNINVHDR>(dtTemp, SUNINVHDR.Converter);
            }
            return oResult;
        }
        public List<SUNINVHDR> GetSunInvdatanewRev(String Com, string pc, DateTime sdate, DateTime edate)
        {
            List<SUNINVHDR> oResult = null;
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("P_COMPANY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[1] = new OracleParameter("P_PROCEN", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
            (param[2] = new OracleParameter("P_ST_DATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = sdate;
            (param[3] = new OracleParameter("P_END_DATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = edate;
            param[4] = new OracleParameter("O_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dtTemp = QueryDataTable("IMP_OP_ITM", "SP_GET_SALESHDRSUNUPLOADREV", CommandType.StoredProcedure, false, param);

            if (dtTemp.Rows.Count > 0)
            {
                oResult = DataTableExtensions.ToGenericList<SUNINVHDR>(dtTemp, SUNINVHDR.Converter);
            }
            return oResult;
        }
        //subodana
        public string GetAccountCodeByTp(string _company, string _type)
        {
            OracleParameter[] param = new OracleParameter[3];
            string _isValid = "";
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("P_TYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _type;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _tblvalidsupplier = QueryDataTable("tblvaliddivision", "SP_SUNTYPEACC", CommandType.StoredProcedure, false, param);
            if (_tblvalidsupplier != null)
            {
                if (_tblvalidsupplier.Rows.Count > 0)
                {
                    _isValid = _tblvalidsupplier.Rows[0][0].ToString();
                }
            }

            return _isValid;
        }
        public int UpdateInvoiceSatatus(string invno, string com, string Status, string User)
        {
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("P_INV_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = invno;
            (param[1] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[2] = new OracleParameter("P_STATUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Status;
            (param[3] = new OracleParameter("P_USER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = User;
            param[4] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            Int32 result = UpdateRecords("SP_UPDATE_INV_STATUS", CommandType.StoredProcedure, param);
            return result;
        }

        public Int32 saveRequestCostDetails(TRN_PETTYCASH_SETTLE_DTL RQ, TRN_PETTYCASH_SETTLE_HDR hdr)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[15];
                Int32 effects = 0;

                (param[0] = new OracleParameter("p_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = RQ.TPSD_JOB_NO;
                (param[1] = new OracleParameter("p_line_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = RQ.TPSD_LINE_NO;
                (param[2] = new OracleParameter("p_req_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = RQ.TPSD_SETTLE_NO;
                (param[3] = new OracleParameter("p_ref_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.TPSH_MAN_REF;
                (param[4] = new OracleParameter("p_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = hdr.TPSH_SETTLE_DT.Date;
                (param[5] = new OracleParameter("p_element_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "";
                (param[6] = new OracleParameter("p_element_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = RQ.TPSD_ELEMENT_CD;
                (param[7] = new OracleParameter("p_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = RQ.TPSD_ELEMENT_DESC;
                (param[8] = new OracleParameter("p_cost_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = RQ.TPSD_SETTLE_AMT;
                (param[9] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.TPSH_COM_CD;
                (param[10] = new OracleParameter("p_rmk", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = RQ.TPSD_REMARKS;
                (param[11] = new OracleParameter("p_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.TPSH_CRE_SES_ID;
                (param[12] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.TPSH_CRE_BY;
                (param[13] = new OracleParameter("p_cre_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = hdr.TPSH_CRE_DT;
                param[14] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int32)UpdateRecords("SP_SAVE_PTTYCSCOSTDTL", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int deleteSettlementCost(string settleno)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_settleno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = settleno;
            param[1] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            Int32 result = UpdateRecords("SP_UPDATE_SELLECOST", CommandType.StoredProcedure, param);
            return result;
        }
        public List<TRN_JOB_COST> GetJobCostData(String job)
        {
            List<TRN_JOB_COST> oResult = null;
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dtTemp = QueryDataTable("IMP_OP_ITM", "SP_GETJOBCOSTDATA", CommandType.StoredProcedure, false, param);

            if (dtTemp.Rows.Count > 0)
            {
                oResult = DataTableExtensions.ToGenericList<TRN_JOB_COST>(dtTemp, TRN_JOB_COST.ConverterJobCost);
            }
            return oResult;
        }
        //dilshan on 25/10/2018
        public List<TRN_JOB_COST> GetJobCostData_New(String job, String com, String pc)
        {
            List<TRN_JOB_COST> oResult = null;
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (param[1] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[2] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dtTemp = QueryDataTable("IMP_OP_ITM", "SP_GETJOBCOSTDATA_NEW", CommandType.StoredProcedure, false, param);

            if (dtTemp.Rows.Count > 0)
            {
                oResult = DataTableExtensions.ToGenericList<TRN_JOB_COST>(dtTemp, TRN_JOB_COST.ConverterJobPreCost);
            }
            return oResult;
        }
        public Int32 GetJobCostSavedData(String job, String com, String pc)
        {
            Int32 oResult = 0;
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (param[1] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[2] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dtTemp = QueryDataTable("IMP_OP_ITM", "sp_getjobsavedcostdata", CommandType.StoredProcedure, false, param);

            if (dtTemp.Rows.Count > 0)
            {
                oResult = dtTemp.Rows.Count;
            }
            return oResult;
        }
        public Int32 saveJobCostData(TRN_JOB_COST _cost)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[16];
                Int32 effects = 0;

                (param[0] = new OracleParameter("p_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cost.TJC_JOB_NO;
                (param[1] = new OracleParameter("p_line_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _cost.TJC_LINE_NO;
                (param[2] = new OracleParameter("p_req_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cost.TJC_REQ_NO;
                (param[3] = new OracleParameter("p_ref_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cost.TJC_REF_NO;
                (param[4] = new OracleParameter("p_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _cost.TJC_DT;
                (param[5] = new OracleParameter("p_element_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cost.TJC_ELEMENT_TYPE;
                (param[6] = new OracleParameter("p_element_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cost.TJC_ELEMENT_CODE;
                (param[7] = new OracleParameter("p_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cost.TJC_DESC;
                (param[8] = new OracleParameter("p_cost_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _cost.TJC_COST_AMT;
                (param[9] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cost.TJC_COM;
                (param[10] = new OracleParameter("p_rmk", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cost.TJC_RMK;
                (param[11] = new OracleParameter("p_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cost.TJC_SESSION_ID;
                (param[12] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cost.TJC_CRE_BY;
                (param[13] = new OracleParameter("p_cre_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _cost.TJC_CRE_DT;
                (param[14] = new OracleParameter("p_rev_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _cost.TJC_REV_AMT;
                param[15] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int32)UpdateRecords("SP_SAVE_JOB_COST", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //dilshan on 26/10/2018
        public Int32 savePreJobCostData(TRN_JOB_COST _cost, string cost_rev, string com, string user)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[18];
                Int32 effects = 0;

                (param[0] = new OracleParameter("p_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cost.TJC_JOB_NO;
                (param[1] = new OracleParameter("p_line_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _cost.TJC_LINE_NO;
                (param[2] = new OracleParameter("p_req_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cost.TJC_REQ_NO;
                (param[3] = new OracleParameter("p_ref_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cost.TJC_REF_NO;
                (param[4] = new OracleParameter("p_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _cost.TJC_DT;
                (param[5] = new OracleParameter("p_element_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cost.TJC_ELEMENT_TYPE;
                (param[6] = new OracleParameter("p_element_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cost.TJC_ELEMENT_CODE;
                (param[7] = new OracleParameter("p_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cost.TJC_DESC;
                (param[8] = new OracleParameter("p_cost_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _cost.TJC_COST_AMT;
                (param[9] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;// _cost.TJC_COM;
                (param[10] = new OracleParameter("p_rmk", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cost.TJC_RMK;
                (param[11] = new OracleParameter("p_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cost.TJC_SESSION_ID;
                (param[12] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = user;// _cost.TJC_CRE_BY;
                (param[13] = new OracleParameter("p_cre_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _cost.TJC_CRE_DT;
                (param[14] = new OracleParameter("p_rev_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _cost.TJC_REV_AMT;
                (param[15] = new OracleParameter("p_cos_rev", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cost_rev;
                (param[16] = new OracleParameter("p_margin", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cost.TJC_MARGIN;
                param[17] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int32)UpdateRecords("SP_SAVE_JOB_COST_NEW", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //dilshan on 26/10/2018
        public Int32 AppPreJobCostData(TRN_JOB_COST _cost, int x, string user, DateTime date)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[17];
                Int32 effects = 0;

                (param[0] = new OracleParameter("p_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cost.TJC_JOB_NO;
                (param[1] = new OracleParameter("p_line_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _cost.TJC_LINE_NO;
                (param[2] = new OracleParameter("p_req_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cost.TJC_REQ_NO;
                (param[3] = new OracleParameter("p_ref_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cost.TJC_REF_NO;
                (param[4] = new OracleParameter("p_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _cost.TJC_DT;
                (param[5] = new OracleParameter("p_element_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cost.TJC_ELEMENT_TYPE;
                (param[6] = new OracleParameter("p_element_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cost.TJC_ELEMENT_CODE;
                (param[7] = new OracleParameter("p_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cost.TJC_DESC;
                (param[8] = new OracleParameter("p_cost_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _cost.TJC_COST_AMT;
                (param[9] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cost.TJC_COM;
                (param[10] = new OracleParameter("p_rmk", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cost.TJC_RMK;
                (param[11] = new OracleParameter("p_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cost.TJC_SESSION_ID;
                (param[12] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = user;
                (param[13] = new OracleParameter("p_cre_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = date;
                (param[14] = new OracleParameter("p_rev_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _cost.TJC_REV_AMT;
                (param[15] = new OracleParameter("p_app_level", OracleDbType.Int32, null, ParameterDirection.Input)).Value = x;                
                param[16] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int32)UpdateRecords("SP_APP_JOB_COST_NEW", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int UpdateJobStatus(string jobno, string Status, string User)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_JOB_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobno;
            (param[1] = new OracleParameter("P_STATUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Status;
            (param[2] = new OracleParameter("P_USER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = User;
            param[3] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            Int32 result = UpdateRecords("SP_UPDATE_JOB_STATUS", CommandType.StoredProcedure, param);
            return result;
        }
        //dilshan on 05/09/2018
        public int AutoUpdateJobStatus(DateTime Date, string Status, string User)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_DATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = Date;
            (param[1] = new OracleParameter("P_STATUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Status;
            (param[2] = new OracleParameter("P_USER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = User;
            param[3] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            Int32 result = UpdateRecords("sp_autoupdate_job_status", CommandType.StoredProcedure, param);
            return result;
        }
        //dilshan
        public int UpdateReopenJobStatus(string jobno, string Status, string User, string Remark)
        {
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("P_JOB_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobno;
            (param[1] = new OracleParameter("P_STATUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Status;
            (param[2] = new OracleParameter("P_USER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = User;
            (param[3] = new OracleParameter("P_REMARK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Remark;
            param[4] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            Int32 result = UpdateRecords("SP_UPDATE_REOPENJOB_STATUS", CommandType.StoredProcedure, param);
            return result;
        }
        //subodana 2017-08-16
        public List<SUNRECIEPTHDR> GetSunRecieptdatanew(String Com, string pc, DateTime sdate, DateTime edate, string type)
        {
            List<SUNRECIEPTHDR> oResult = null;
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("P_COMPANY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[1] = new OracleParameter("P_PROCEN", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
            (param[2] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
            (param[3] = new OracleParameter("P_ST_DATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = sdate;
            (param[4] = new OracleParameter("P_END_DATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = edate;
            param[5] = new OracleParameter("O_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dtTemp = QueryDataTable("IMP_OP_ITM", "SP_GET_RECIPTHDRSUNUPLOAD", CommandType.StoredProcedure, false, param);

            if (dtTemp.Rows.Count > 0)
            {
                oResult = DataTableExtensions.ToGenericList<SUNRECIEPTHDR>(dtTemp, SUNRECIEPTHDR.Converter);
            }
            return oResult;
        }
        //subodana 2017-08-16
        public List<PetyCashUpload> GetSunPetyCash(String Com, DateTime sdate, DateTime edate)
        {
            List<PetyCashUpload> oResult = null;
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_COMPANY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[1] = new OracleParameter("P_FDATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = sdate;
            (param[2] = new OracleParameter("P_TDATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = edate;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dtTemp = QueryDataTable("IMP_OP_ITM", "SP_PETYASHUPLOAD", CommandType.StoredProcedure, false, param);

            if (dtTemp.Rows.Count > 0)
            {
                oResult = DataTableExtensions.ToGenericList<PetyCashUpload>(dtTemp, PetyCashUpload.Converter);
            }
            return oResult;
        }
        //subodana 2018-01-25
        public List<PetyCashUpload> GetSunPetyCashPaymentReq(String Com, DateTime sdate, DateTime edate)
        {
            List<PetyCashUpload> oResult = null;
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_COMPANY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[1] = new OracleParameter("P_FDATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = sdate;
            (param[2] = new OracleParameter("P_TDATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = edate;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dtTemp = QueryDataTable("IMP_OP_ITM", "SP_PETYPAYREQ", CommandType.StoredProcedure, false, param);

            if (dtTemp.Rows.Count > 0)
            {
                oResult = DataTableExtensions.ToGenericList<PetyCashUpload>(dtTemp, PetyCashUpload.Converter);
            }
            return oResult;
        }
        //subodana 2017-08-16
        public List<PetyCashUpload> GetSunPetyCashReq(String Type, DateTime sdate, DateTime edate,string com)
        {
            List<PetyCashUpload> oResult = null;
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("P_TYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Type;
            (param[1] = new OracleParameter("P_FDATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = sdate;
            (param[2] = new OracleParameter("P_TDATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = edate;
            (param[3] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dtTemp = QueryDataTable("IMP_OP_ITM", "SP_PETYREQASHUPLOAD", CommandType.StoredProcedure, false, param);

            if (dtTemp.Rows.Count > 0)
            {
                oResult = DataTableExtensions.ToGenericList<PetyCashUpload>(dtTemp, PetyCashUpload.Converter);
            }
            return oResult;
        }
        public int CheckJobUse(string Job)
        {
            int count = 0;

            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Job;
            param[1] = new OracleParameter("O_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dtTemp = QueryDataTable("count", "SP_CHECK_JOB_USEBL", CommandType.StoredProcedure, false, param);
            if (dtTemp != null)
            {
                if (dtTemp.Rows.Count > 0)
                {
                    count = Convert.ToInt16(dtTemp.Rows[0][0].ToString());
                    if (count <= 0)
                    {
                        param = new OracleParameter[2];
                        (param[0] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Job;
                        param[1] = new OracleParameter("O_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
                        dtTemp = QueryDataTable("count", "SP_CHECK_JOB_USEPETTY", CommandType.StoredProcedure, false, param);
                        count = Convert.ToInt16(dtTemp.Rows[0][0].ToString());
                    }
                }
                else
                {
                    param = new OracleParameter[2];
                    (param[0] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Job;
                    param[1] = new OracleParameter("O_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    dtTemp = QueryDataTable("count", "SP_CHECK_JOB_USEPETTY", CommandType.StoredProcedure, false, param);
                    count = Convert.ToInt16(dtTemp.Rows[0][0].ToString());
                }
            }
            else
            {
                param = new OracleParameter[2];
                (param[0] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Job;
                param[1] = new OracleParameter("O_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
                dtTemp = QueryDataTable("count", "SP_CHECK_JOB_USEPETTY", CommandType.StoredProcedure, false, param);
                count = Convert.ToInt16(dtTemp.Rows[0][0].ToString());
            }

            return count;
        }

        public int updateRequestDtl(TRN_PETTYCASH_SETTLE_DTL RQ)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[6];
                Int32 effects = 0;

                (param[0] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = RQ.TPSD_SEQ_NO;
                (param[1] = new OracleParameter("p_setno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = RQ.TPSD_SETTLE_NO;
                (param[2] = new OracleParameter("p_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = RQ.TPSD_LINE_NO;
                (param[3] = new OracleParameter("p_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = RQ.TPSD_JOB_NO;
                (param[4] = new OracleParameter("p_setlineno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = RQ.TPSD_SETLE_LINO_NO;
                param[5] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int32)UpdateRecords("sp_update_ptysetdetails", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int updateRequestCostDetails(TRN_PETTYCASH_SETTLE_DTL RQ, TRN_PETTYCASH_SETTLE_HDR hdr)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[6];
                Int32 effects = 0;

                (param[0] = new OracleParameter("p_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = RQ.TPSD_JOB_NO;
                (param[1] = new OracleParameter("p_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = RQ.TPSD_LINE_NO;
                (param[2] = new OracleParameter("p_reqno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = RQ.TPSD_SETTLE_NO;
                (param[3] = new OracleParameter("p_modby", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.TPSH_CRE_BY;
                (param[4] = new OracleParameter("p_moddt", OracleDbType.Date, null, ParameterDirection.Input)).Value = DateTime.Now;
                param[5] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int32)UpdateRecords("sp_update_pttycostdtl", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable PettyCash_SettlementDetls_Summ(int reqSeq, string comCode)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[3];
                (param[0] = new OracleParameter("p_reqseq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = reqSeq;
                (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comCode;
                param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable tblDet = QueryDataTable("tblSettlementDtl_summ", "SP_GET_PETTY_CASH_SETT_SUMM", CommandType.StoredProcedure, false, param);
                return tblDet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TRN_PETTYCASH_SETTLE_HDR load_PtyC_Setl_RequestDtl_Validate(int seq, string company, string userDefPro)
        {
            try
            {
                TRN_PETTYCASH_SETTLE_HDR res = new TRN_PETTYCASH_SETTLE_HDR();
                OracleParameter[] param = new OracleParameter[4];
                (param[0] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = seq;
                (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[2] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userDefPro;
                param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable tblDet = QueryDataTable("tblprycshSetl_val", "SP_GET_PTYCS_STLE_REQDTL_VAL", CommandType.StoredProcedure, false, param);
                if (tblDet.Rows.Count > 0)
                {
                    res = DataTableExtensions.ToGenericList<TRN_PETTYCASH_SETTLE_HDR>(tblDet, TRN_PETTYCASH_SETTLE_HDR.Converter)[0];
                }
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TRN_PETTYCASH_REQ_HDR loadRequestDetailsbySeq_val(int seq, string company, string userDefPro)
        {
            try
            {
                TRN_PETTYCASH_REQ_HDR res = new TRN_PETTYCASH_REQ_HDR();
                OracleParameter[] param = new OracleParameter[4];
                (param[0] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = seq;
                (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[2] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userDefPro;
                param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable tblDet = QueryDataTable("tblpromoDis_val", "SP_GET_PTYREQDETAILSBYSEQ_val", CommandType.StoredProcedure, false, param);


                if (tblDet.Rows.Count > 0)
                {
                    res = DataTableExtensions.ToGenericList<TRN_PETTYCASH_REQ_HDR>(tblDet, TRN_PETTYCASH_REQ_HDR.Converter)[0];
                }

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // Added by Chathura on 15-sep-2017
        public List<SystemUserChannel> GetUserChannels(string UserID, string Comp)
        {
            List<SystemUserChannel> _userList = null;

            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = UserID;
            (param[1] = new OracleParameter("p_comcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Comp;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblUserProf", "SP_GETUSERCHANEL", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _userList = DataTableExtensions.ToGenericList<SystemUserChannel>(_dtResults, SystemUserChannel.Converter);
            }
            return _userList;
        }

        //Added by Chathura on 15-sep-2017
        public List<SystemUserProf> GetUserProfCenters(string UserID, string Comp, string User_def_chnl)
        {
            List<SystemUserProf> _userList = null;

            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = UserID;
            (param[1] = new OracleParameter("p_comcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Comp;
            (param[2] = new OracleParameter("p_chnlcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = User_def_chnl;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblUserProf", "SP_GETUSERPROFBYCHNL", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _userList = DataTableExtensions.ToGenericList<SystemUserProf>(_dtResults, SystemUserProf.Converter);
            }
            return _userList;
        }

        // Added by Chathura on 21-sep-2017
        public Int32 CancelJobReciept(string receiptNo)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[2];
                Int32 effects = 0;

                (param[0] = new OracleParameter("p_receiptNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = receiptNo;
                param[1] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int32)UpdateRecords("SP_CANCEL_RECEIPT", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TRN_PETTYCASH_SETTLE_DTL> LoadAllRefundableJobData(string jobno)
        {
            List<TRN_PETTYCASH_SETTLE_DTL> _list = new List<TRN_PETTYCASH_SETTLE_DTL>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobno;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _tblData = QueryDataTable("tblData", "SP_LOAD_ALL_REFUNDABLEJOB", CommandType.StoredProcedure, false, param);
            if (_tblData.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<TRN_PETTYCASH_SETTLE_DTL>(_tblData, TRN_PETTYCASH_SETTLE_DTL.Converter);
            }

            return _list;
        }

        public Int32 saveRequestCostDetailsRefund(TRN_PETTYCASH_SETTLE_DTL RQ, TRN_PETTYCASH_SETTLE_HDR hdr)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[16];
                Int32 effects = 0;

                (param[0] = new OracleParameter("p_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = RQ.TPSD_JOB_NO;
                (param[1] = new OracleParameter("p_line_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = RQ.TPSD_LINE_NO;
                (param[2] = new OracleParameter("p_req_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = RQ.TPSD_SETTLE_NO;
                (param[3] = new OracleParameter("p_ref_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.TPSH_MAN_REF;
                (param[4] = new OracleParameter("p_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = hdr.TPSH_SETTLE_DT.Date;
                (param[5] = new OracleParameter("p_element_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "";
                (param[6] = new OracleParameter("p_element_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = RQ.TPSD_ELEMENT_CD;
                (param[7] = new OracleParameter("p_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = RQ.TPSD_ELEMENT_DESC;
                (param[8] = new OracleParameter("p_cost_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = RQ.TPSD_SETTLE_AMT;
                (param[9] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.TPSH_COM_CD;
                (param[10] = new OracleParameter("p_rmk", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = RQ.TPSD_REMARKS;
                (param[11] = new OracleParameter("p_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.TPSH_CRE_SES_ID;
                (param[12] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hdr.TPSH_CRE_BY;
                (param[13] = new OracleParameter("p_cre_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = hdr.TPSH_CRE_DT;
                (param[14] = new OracleParameter("p_settle_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = RQ.TPSD_SETTLE_NO;
                param[15] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int32)UpdateRecords("SP_SAVE_PTTYCSCOSTDTL", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int updateSettlementHdr(TRN_PETTYCASH_SETTLE_HDR request)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[8];
                Int32 effects = 0;

                (param[0] = new OracleParameter("P_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = request.TPSH_SEQ_NO;
                (param[1] = new OracleParameter("P_req_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = request.TPSH_SETTLE_NO;
                (param[2] = new OracleParameter("P_paydt", OracleDbType.Date, null, ParameterDirection.Input)).Value = request.TPSH_PAY_DT;
                (param[3] = new OracleParameter("P_rmk", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = request.TPSH_REMARKS;
                (param[4] = new OracleParameter("P_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = request.TPSH_MOD_BY;
                (param[5] = new OracleParameter("P_mod_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = request.TPSH_MOD_DT;
                (param[6] = new OracleParameter("p_mod_session_id", OracleDbType.Int32, null, ParameterDirection.Input)).Value = request.TPSH_MOD_SES_ID;
                param[7] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int32)UpdateRecords("SP_UPDATE_PTYSETTLEMENTHDR", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CancelRefund(string jobno)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[2];
                Int32 effects = 0;

                (param[0] = new OracleParameter("p_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobno;
                param[1] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int32)UpdateRecords("SP_CANCEL_REFUND", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TRN_PETTYCASH_SETTLE_DTL> CheckJobAlreadyHasRefunds(string jobno)
        {
            List<TRN_PETTYCASH_SETTLE_DTL> _list = new List<TRN_PETTYCASH_SETTLE_DTL>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobno;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _tblData = QueryDataTable("tblData", "SP_CHECK_JOB_HASREFUND", CommandType.StoredProcedure, false, param);
            if (_tblData.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<TRN_PETTYCASH_SETTLE_DTL>(_tblData, TRN_PETTYCASH_SETTLE_DTL.Converter);
            }

            return _list;
        }

        public List<TRN_PETTYCASH_SETTLE_DTL> CheckJobFullyRefunded(string jobno)
        {
            List<TRN_PETTYCASH_SETTLE_DTL> _list = new List<TRN_PETTYCASH_SETTLE_DTL>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobno;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _tblData = QueryDataTable("tblData", "SP_CHECK_JOB_FULLYREFUNDED", CommandType.StoredProcedure, false, param);
            if (_tblData.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<TRN_PETTYCASH_SETTLE_DTL>(_tblData, TRN_PETTYCASH_SETTLE_DTL.Converter);
            }

            return _list;
        }

        public List<TRN_PETTYCASH_SETTLE_DTL> CheckSettlementRejected(string jobno)
        {
            List<TRN_PETTYCASH_SETTLE_DTL> _list = new List<TRN_PETTYCASH_SETTLE_DTL>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobno;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _tblData = QueryDataTable("tblData", "SP_CHECK_SETTLEMENT_REJECT", CommandType.StoredProcedure, false, param);
            if (_tblData.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<TRN_PETTYCASH_SETTLE_DTL>(_tblData, TRN_PETTYCASH_SETTLE_DTL.Converter);
            }

            return _list;
        }

        public List<TRN_PETTYCASH_SETTLE_DTL> CheckSettlementApproved(string jobno)
        {
            List<TRN_PETTYCASH_SETTLE_DTL> _list = new List<TRN_PETTYCASH_SETTLE_DTL>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobno;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _tblData = QueryDataTable("tblData", "SP_CHECK_SETTLEMENT_APPROVE", CommandType.StoredProcedure, false, param);
            if (_tblData.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<TRN_PETTYCASH_SETTLE_DTL>(_tblData, TRN_PETTYCASH_SETTLE_DTL.Converter);
            }

            return _list;
        }

        public trn_inv_hdr validateInvoiceNUmber(string company, string cus, string othpc, string pc)
        {
            trn_inv_hdr det = new trn_inv_hdr();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[1] = new OracleParameter("p_cus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cus;
            (param[2] = new OracleParameter("p_othpc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = othpc;
            (param[3] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _tblData = QueryDataTable("tblData", "SP_CHECK_INVOICENUMBER", CommandType.StoredProcedure, false, param);
            if (_tblData.Rows.Count > 0)
            {
                det = DataTableExtensions.ToGenericList<trn_inv_hdr>(_tblData, trn_inv_hdr.Converter)[0];

            }

            return det;
        }

        public List<JOB_NUM_SEARCH> JobOrPouchDetails(string code, string type, string company, string userId)
        {
            List<JOB_NUM_SEARCH> _list = new List<JOB_NUM_SEARCH>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
            (param[1] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
            (param[2] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[3] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userId;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _tblData = QueryDataTable("tblData", "SP_GET_JOB_POUCH_DETAILS", CommandType.StoredProcedure, false, param);
            if (_tblData.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<JOB_NUM_SEARCH>(_tblData, JOB_NUM_SEARCH.Converter);
            }

            return _list;
        }
        public List<JOB_NUM_SEARCH> JobOrPouchCostDetails(string code, string type, string company, string userId)
        {
            List<JOB_NUM_SEARCH> _list = new List<JOB_NUM_SEARCH>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
            (param[1] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
            (param[2] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[3] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userId;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _tblData = QueryDataTable("tblData", "SP_GET_JOB_POUCH_COSTDETAILS", CommandType.StoredProcedure, false, param);
            if (_tblData.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<JOB_NUM_SEARCH>(_tblData, JOB_NUM_SEARCH.ConverterCost);
            }

            return _list;
        }

        public List<TRN_JOB_COST> GetJobActualCostData(String job)
        {
            List<TRN_JOB_COST> oResult = null;
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dtTemp = QueryDataTable("IMP_OP_ITM", "SP_GETJOBACTUAL_COSTDATA", CommandType.StoredProcedure, false, param);

            if (dtTemp.Rows.Count > 0)
            {
                oResult = DataTableExtensions.ToGenericList<TRN_JOB_COST>(dtTemp, TRN_JOB_COST.ConverterJobCost);
            }
            return oResult;
        }
        public List<TRN_JOB_COST> GetJobActualCostData_New(String job, String com, String pc)
        {
            List<TRN_JOB_COST> oResult = null;
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (param[1] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[2] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dtTemp = QueryDataTable("IMP_OP_ITM", "SP_GETJOBACTUAL_COSTDATA_NEW", CommandType.StoredProcedure, false, param);

            if (dtTemp.Rows.Count > 0)
            {
                oResult = DataTableExtensions.ToGenericList<TRN_JOB_COST>(dtTemp, TRN_JOB_COST.ConverterJobPreCost);
            }
            return oResult;
        }


        public int cancelReceiptDetails(RecieptItem itm, string receiptNo)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[3];
                Int32 effects = 0;

                (param[0] = new OracleParameter("p_invno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = itm.Sard_inv_no;
                (param[1] = new OracleParameter("p_setamt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = itm.Sard_settle_amt;
                param[2] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

                effects = (Int32)UpdateRecords("SP_CANCEL_INVOICERECPT", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int cancelRefundDetails(string receiptNo)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[2];
                Int32 effects = 0;

                (param[0] = new OracleParameter("p_recno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = receiptNo;
                param[1] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

                effects = (Int32)UpdateRecords("SP_CANCEL_SETTLEMENTDET", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RecieptItem> GetReceiptDetailsNonAlocated(string receiptNo)
        {
            List<RecieptItem> _RecItemList = null;
            RecieptItem _RecItems = null;

            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_doc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = receiptNo;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);


            //Query Data base.        
            DataTable _dtResults = QueryDataTable("tblRecItems", "sp_getreceiptdetailsunalow", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _RecItemList = new List<RecieptItem>();

                for (int i = 0; i < _dtResults.Rows.Count; i++)
                {
                    _RecItems = new RecieptItem();

                    _RecItems.Sard_seq_no = (_dtResults.Rows[i]["Sard_seq_no"] == DBNull.Value) ? 0 : Convert.ToInt32(_dtResults.Rows[i]["Sard_seq_no"]);
                    _RecItems.Sard_line_no = (_dtResults.Rows[i]["Sard_line_no"] == DBNull.Value) ? 0 : Convert.ToInt16(_dtResults.Rows[i]["Sard_line_no"]);
                    _RecItems.Sard_receipt_no = (_dtResults.Rows[i]["Sard_receipt_no"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_receipt_no"].ToString();
                    _RecItems.Sard_inv_no = (_dtResults.Rows[i]["Sard_inv_no"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_inv_no"].ToString();
                    _RecItems.Sard_pay_tp = (_dtResults.Rows[i]["Sard_pay_tp"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_pay_tp"].ToString();
                    _RecItems.Sard_ref_no = (_dtResults.Rows[i]["Sard_ref_no"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_ref_no"].ToString();
                    _RecItems.Sard_chq_bank_cd = (_dtResults.Rows[i]["Sard_chq_bank_cd"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_chq_bank_cd"].ToString();
                    _RecItems.Sard_chq_branch = (_dtResults.Rows[i]["Sard_chq_branch"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_chq_branch"].ToString();
                    _RecItems.Sard_deposit_bank_cd = (_dtResults.Rows[i]["Sard_deposit_bank_cd"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_deposit_bank_cd"].ToString();
                    _RecItems.Sard_deposit_branch = (_dtResults.Rows[i]["Sard_deposit_branch"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_deposit_branch"].ToString();
                    _RecItems.Sard_credit_card_bank = (_dtResults.Rows[i]["Sard_credit_card_bank"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_credit_card_bank"].ToString();
                    _RecItems.Sard_cc_tp = (_dtResults.Rows[i]["Sard_cc_tp"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_cc_tp"].ToString();
                    _RecItems.Sard_cc_expiry_dt = (_dtResults.Rows[i]["Sard_cc_expiry_dt"] == DBNull.Value) ? DateTime.Today : Convert.ToDateTime(_dtResults.Rows[i]["Sard_cc_expiry_dt"]);
                    _RecItems.Sard_cc_is_promo = (_dtResults.Rows[i]["Sard_cc_is_promo"] == DBNull.Value) ? false : Convert.ToBoolean(_dtResults.Rows[i]["Sard_cc_is_promo"]);
                    _RecItems.Sard_cc_period = (_dtResults.Rows[i]["Sard_cc_period"] == DBNull.Value) ? 0 : Convert.ToInt16(_dtResults.Rows[i]["Sard_cc_period"]);
                    _RecItems.Sard_gv_issue_loc = (_dtResults.Rows[i]["Sard_gv_issue_loc"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_gv_issue_loc"].ToString();
                    _RecItems.Sard_gv_issue_dt = (_dtResults.Rows[i]["Sard_gv_issue_dt"] == DBNull.Value) ? DateTime.Today : Convert.ToDateTime(_dtResults.Rows[i]["Sard_gv_issue_dt"]);
                    _RecItems.Sard_settle_amt = (_dtResults.Rows[i]["Sard_settle_amt"] == DBNull.Value) ? 0 : Convert.ToDecimal(_dtResults.Rows[i]["Sard_settle_amt"]);
                    _RecItems.Sard_sim_ser = (_dtResults.Rows[i]["Sard_sim_ser"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_sim_ser"].ToString();
                    _RecItems.Sard_anal_1 = (_dtResults.Rows[i]["Sard_anal_1"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_anal_1"].ToString();
                    _RecItems.Sard_anal_2 = (_dtResults.Rows[i]["Sard_anal_2"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Sard_anal_2"].ToString();
                    _RecItems.Sard_anal_3 = (_dtResults.Rows[i]["Sard_anal_3"] == DBNull.Value) ? 0 : Convert.ToDecimal(_dtResults.Rows[i]["Sard_anal_3"]);
                    _RecItems.Sard_anal_4 = (_dtResults.Rows[i]["Sard_anal_4"] == DBNull.Value) ? 0 : Convert.ToDecimal(_dtResults.Rows[i]["Sard_anal_4"]);
                    _RecItems.Sard_anal_5 = (_dtResults.Rows[i]["Sard_anal_5"] == DBNull.Value) ? DateTime.Today : Convert.ToDateTime(_dtResults.Rows[i]["Sard_anal_5"]);
                    _RecItemList.Add(_RecItems);
                }
            }
            return _RecItemList;
        }

        public int updateReceiptItemOriginal(int seq, decimal setleamt)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[3];
                Int32 effects = 0;

                (param[0] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = seq;
                (param[1] = new OracleParameter("p_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = setleamt;
                param[2] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

                effects = (Int32)UpdateRecords("sp_updateunalowreceipt", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<mst_item_tax> GetElementWiseTaxDetails(String element, string channel, string company)
        {
            List<mst_item_tax> oResult = null;
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_element", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = element;
            (param[1] = new OracleParameter("p_channel", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = channel;
            (param[2] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dtTemp = QueryDataTable("tabl", "SP_GET_ELEMENT_TAX", CommandType.StoredProcedure, false, param);

            if (dtTemp.Rows.Count > 0)
            {
                oResult = DataTableExtensions.ToGenericList<mst_item_tax>(dtTemp, mst_item_tax.Converter);
            }
            return oResult;
        }

        public List<mst_item_tax> GetAllTaxDetails(string channel, string company)
        {
            List<mst_item_tax> oResult = null;
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_channel", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = channel;
            (param[1] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dtTemp = QueryDataTable("tabl", "SP_GET_ELEMENT_TAXALL", CommandType.StoredProcedure, false, param);

            if (dtTemp.Rows.Count > 0)
            {
                oResult = DataTableExtensions.ToGenericList<mst_item_tax>(dtTemp, mst_item_tax.Converter);
            }
            return oResult;
        }

        public List<MainServices> GetJobServiceCode(String jobno, string cusid, string company, string pc)
        {
            List<MainServices> oResult = null;
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobno;
            (param[1] = new OracleParameter("p_cusid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cusid;
            (param[2] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[3] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dtTemp = QueryDataTable("tabl", "SP_GET_JOBSERVICE_CD", CommandType.StoredProcedure, false, param);

            if (dtTemp.Rows.Count > 0)
            {
                oResult = DataTableExtensions.ToGenericList<MainServices>(dtTemp, MainServices.Converter);
            }
            return oResult;
        }

        public List<cus_details> GetCustomerTaxEx(string invparty)
        {
            List<cus_details> oResult = null;
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_invparty", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = invparty;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dtTemp = QueryDataTable("tabl", "SP_GET_CUSTOMERTAXEX", CommandType.StoredProcedure, false, param);

            if (dtTemp.Rows.Count > 0)
            {
                oResult = DataTableExtensions.ToGenericList<cus_details>(dtTemp, cus_details.ConverterCusTax);
            }
            return oResult;
        }

        public List<InvoiceCom> GetSunUpRestrictStatus(string company, string userDefPro, DateTime invdate)
        {
            List<InvoiceCom> oResult = null;
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userDefPro;
            (param[2] = new OracleParameter("p_invdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = invdate.Date;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dtTemp = QueryDataTable("tabl", "SP_GET_HAS_SUNUPDATE", CommandType.StoredProcedure, false, param);

            if (dtTemp.Rows.Count > 0)
            {
                oResult = DataTableExtensions.ToGenericList<InvoiceCom>(dtTemp, InvoiceCom.ConverterToHasDate);
            }
            return oResult;
        }

        public List<InvoiceCom> GetNumOfBackdates(string company, string userDefPro)
        {
            List<InvoiceCom> oResult = null;
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userDefPro;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dtTemp = QueryDataTable("tabl", "SP_GET_BACKDATES", CommandType.StoredProcedure, false, param);

            if (dtTemp.Rows.Count > 0)
            {
                oResult = DataTableExtensions.ToGenericList<InvoiceCom>(dtTemp, InvoiceCom.ConverterToBackDates);
            }
            return oResult;
        }

        public List<InvoiceCom> GetEtaEtdInvoiceDate(string hblnum, string pc)
        {
            List<InvoiceCom> oResult = null;
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_hblnum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hblnum;
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dtTemp = QueryDataTable("tabl", "SP_GET_ETAETDINVOICE", CommandType.StoredProcedure, false, param);

            if (dtTemp.Rows.Count > 0)
            {
                oResult = DataTableExtensions.ToGenericList<InvoiceCom>(dtTemp, InvoiceCom.ConverterToInvDate);
            }
            return oResult;
        }

        //Tharindu 2017-12-26
        public DataTable Get_Cash_Outstanding(DateTime frmDate,DateTime toDate,string comCode, string proCntCode)
        {
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("fromdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmDate;
            (param[1] = new OracleParameter("todate", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDate;
            (param[2] = new OracleParameter("com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comCode;
            (param[3] = new OracleParameter("pcenter", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = proCntCode;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblCashout", "sp_get_cash_adv_out", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        //Dilshan 2018-07-25
        public DataTable Get_Job_Status_Detail(DateTime frmDate, DateTime toDate, string comCode, string proCntCode, string jobstatus)
        {
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("fromdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmDate;
            (param[1] = new OracleParameter("todate", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDate;
            (param[2] = new OracleParameter("com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comCode;
            (param[3] = new OracleParameter("pcenter", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = proCntCode;
            (param[4] = new OracleParameter("jobsts", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobstatus;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblCashout", "sp_get_job_status_details", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        //Dilshan 2018-10-02
        public DataTable Get_IRD_Detail(DateTime frmDate, DateTime toDate, string comCode, string proCntCode, string jobstatus)
        {
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("fromdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmDate;
            (param[1] = new OracleParameter("todate", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDate;
            (param[2] = new OracleParameter("com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comCode;
            (param[3] = new OracleParameter("pcenter", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = proCntCode;
            (param[4] = new OracleParameter("jobsts", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobstatus;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblCashout", "sp_get_ird_details", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        //Tharindu 2017-12-29
        public DataTable Get_Collection_Summary(DateTime frmDate, DateTime toDate, string comCode, string proCntCode, string type, string paytype, string usertype)
        {
            OracleParameter[] param = new OracleParameter[8];
            (param[0] = new OracleParameter("p_fromdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmDate;
            (param[1] = new OracleParameter("p_todate", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDate;
            (param[2] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comCode;
            (param[3] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = proCntCode;
            (param[4] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
            (param[5] = new OracleParameter("p_paytype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = paytype;
            (param[6] = new OracleParameter("p_usertype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = usertype;
            param[7] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblCollection", "sp_get_collection_summary", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        //Tharindu 2018-04-01
        public DataTable Get_jb_header_new(string comCode, string jobnum, string hbl)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comCode;
            (param[1] = new OracleParameter("p_jobnumn", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobnum;
            (param[2] = new OracleParameter("p_hbl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hbl;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblCollection", "sp_get_jb_header_new", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        //Tharindu 2018-04-01
        public DataTable Get_jb_header(string comCode, string jobnum)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comCode;
            (param[1] = new OracleParameter("p_jobnumn", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobnum;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblCollection", "sp_get_jb_header", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        //Tharindu 2018-04-01
        public DataTable GetJobCostingData(string job)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dtTemp = QueryDataTable("IMP_OP_ITM", "SP_GETJOBCOSTDATA", CommandType.StoredProcedure, false, param);
            return dtTemp;
        }

        //dilshan on 2018/11/06
        public DataTable GetJobCostingData_new(string job,string status)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            (param[1] = new OracleParameter("P_STATUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = status;
            param[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dtTemp = QueryDataTable("IMP_OP_ITM", "SP_GETPREJOBCOSTDATA", CommandType.StoredProcedure, false, param);
            return dtTemp;
        }
        //Tharindu 2018-04-01
        public DataTable GetJobActualCostingData(string job)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dtTemp = QueryDataTable("IMP_OP_ITM", "SP_GETJOBACTUAL_COSTDATA", CommandType.StoredProcedure, false, param);
            return dtTemp;
        }

        //Tharindu 2018-01-05
        public DataTable GetInvoiceAuditTrail(DateTime frmDate, DateTime toDate, string comCode, string pc, string cusid)
        {
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_fromdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmDate;
            (param[1] = new OracleParameter("p_todate", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDate;
            (param[2] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comCode;
            (param[3] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
            (param[4] = new OracleParameter("p_cusid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cusid;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dtTemp = QueryDataTable("IMP_OP_ITM", "sp_get_inv_audit_rpt", CommandType.StoredProcedure, false, param);
            return dtTemp;
        }
        //subodana
        public string GetEleAccount(string code, string costtype)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_ELECD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
            (param[1] = new OracleParameter("P_CSTTP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = costtype;
            param[2] = new OracleParameter("O_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults;
            _dtResults = QueryDataTable("tbl", "SP_GETELEACC", CommandType.StoredProcedure, false, param);
            string acc = "";
            if (_dtResults != null && _dtResults.Rows.Count > 0)
            {
                acc = _dtResults.Rows[0][0].ToString();
            }
            return acc;
        }

        //Tharindu 2018-01-10
        public DataTable GetRptReceiptDetails(string comCode, string pc, string receiptno)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comCode;
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
            (param[2] = new OracleParameter("p_repcitno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = receiptno;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dtTemp = QueryDataTable("tblreceipt", "sp_get_receipt_details", CommandType.StoredProcedure, false, param);
            return dtTemp;
        }


        //Tharindu 2018-01-12
        public DataTable GetrptContainerDetails(string comCode, string BLNo)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comCode;
            (param[1] = new OracleParameter("p_blno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = BLNo;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dtTemp = QueryDataTable("tblreceipt", "sp_get_containerdt", CommandType.StoredProcedure, false, param);
            return dtTemp;
        }

        //Tharindu 2018-01-12
        public DataTable GetfrightChargePayble(string comCode, string HouseblNo, string InvNo)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comCode;
            (param[1] = new OracleParameter("p_houseblno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = HouseblNo;
            (param[2] = new OracleParameter("p_invno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = InvNo;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dtTemp = QueryDataTable("tblreceipt", "sp_get_fright_chg_payble", CommandType.StoredProcedure, false, param);
            return dtTemp;
        }

        //subodana 2018-01-12
        public List<PayReqUploads> GetPayCashReq(String Type, DateTime sdate, DateTime edate)
        {
            List<PayReqUploads> oResult = null;
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_TYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Type;
            (param[1] = new OracleParameter("P_FDATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = sdate;
            (param[2] = new OracleParameter("P_TDATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = edate;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dtTemp = QueryDataTable("IMP_OP_ITM", "SP_PAYREQASHUPLOAD", CommandType.StoredProcedure, false, param);

            if (dtTemp.Rows.Count > 0)
            {
                oResult = DataTableExtensions.ToGenericList<PayReqUploads>(dtTemp, PayReqUploads.Converter);
            }
            return oResult;
        }
        ////SUBODANA 2018-01-23
        public int SaveSunT4(SunLC _obsunlcdata)
        {
            OracleParameter[] param = new OracleParameter[11];
            (param[0] = new OracleParameter("p_sun_db", OracleDbType.Char, null, ParameterDirection.Input)).Value = _obsunlcdata.sun_db;
            (param[1] = new OracleParameter("p_category", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obsunlcdata.category;
            (param[2] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obsunlcdata.code;
            (param[3] = new OracleParameter("p_lookup", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obsunlcdata.lookup;
            (param[4] = new OracleParameter("p_updated", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obsunlcdata.updated;
            (param[5] = new OracleParameter("p_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obsunlcdata.name;
            (param[6] = new OracleParameter("p_prohb_post", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obsunlcdata.prohb_post;
            (param[7] = new OracleParameter("p_budget_check", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obsunlcdata.budget_check;
            (param[8] = new OracleParameter("p_budget_stop", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obsunlcdata.budget_stop;
            (param[9] = new OracleParameter("p_data_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obsunlcdata.data_1;
            param[10] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            Int32 result = UpdateRecords("sp_save_sunLC", CommandType.StoredProcedure, param);
            return result;
        }
        // 2018-01-23 SUBODANA
        public DataTable CheckSUNLC(string COM, string CAT, string CODE)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = COM;
            (param[1] = new OracleParameter("p_cat", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = CAT;
            (param[2] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = CODE;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SUNLCCOST", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        public Int32 UPDATE_RECIEPT_HDRENGLOG(string recNo, Int32 value, string com)
        {
            Int32 effects = 0;
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_REC_NO", OracleDbType.Varchar2, null, ParameterDirection.Input)).Value = recNo;
            (param[1] = new OracleParameter("P_ANAL", OracleDbType.Int32, null, ParameterDirection.Input)).Value = value;
            (param[2] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            param[3] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("UPDATE_REC_SUNST", CommandType.StoredProcedure, param);
            return effects;
        }
        public Int32 UPDATE_INV_HDRENGLOG(string invNo, Int32 value, string com)
        {
            Int32 effects = 0;
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_SIH_INV_NO", OracleDbType.Varchar2, null, ParameterDirection.Input)).Value = invNo;
            (param[1] = new OracleParameter("P_SIH_ANAL_10", OracleDbType.Int32, null, ParameterDirection.Input)).Value = value;
            (param[2] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            param[3] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("UPDATE_INV_SUNUPLOAD", CommandType.StoredProcedure, param);
            return effects;
        }
        public Int32 UPDATE_PETTYREQ(string doc, Int32 value, string com)
        {
            Int32 effects = 0;
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_DOC", OracleDbType.Varchar2, null, ParameterDirection.Input)).Value = doc;
            (param[1] = new OracleParameter("P_STATUS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = value;
            (param[2] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            param[3] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("UPDATE_INV_PETTYREQ", CommandType.StoredProcedure, param);
            return effects;
        }
        public Int32 UPDATE_PETTYSETTL(string doc, Int32 value, string com)
        {
            Int32 effects = 0;
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_DOC", OracleDbType.Varchar2, null, ParameterDirection.Input)).Value = doc;
            (param[1] = new OracleParameter("P_STATUS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = value;
            (param[2] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            param[3] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("UPDATE_PETTY_SETTL", CommandType.StoredProcedure, param);
            return effects;
        }
        //subodana
        public bool CheckNBTVAT(string com, string pc, string code)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("P_PC", OracleDbType.Char, null, ParameterDirection.Input)).Value = pc;
            (param[2] = new OracleParameter("P_CODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
            param[3] = new OracleParameter("O_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults;
            _dtResults = QueryDataTable("tbl", "SP_ISNBTVAT", CommandType.StoredProcedure, false, param);
            bool type = false;
            if (_dtResults != null && _dtResults.Rows.Count > 0)
            {
                type = true;
            }
            return type;
        }
        //dilshan on 08-03-2018
        public int SaveSunAccountsAll(SunAccountall _sundata)
        {
            OracleParameter[] param = new OracleParameter[49];
            (param[0] = new OracleParameter("P_ACCNT_CODE", OracleDbType.Char, null, ParameterDirection.Input)).Value = _sundata.accnt_code;
            (param[1] = new OracleParameter("P_PERIOD", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _sundata.period;
            (param[2] = new OracleParameter("P_TRANS_DATE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _sundata.trans_date;
            (param[3] = new OracleParameter("P_JRNAL_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _sundata.jrnal_no;
            (param[4] = new OracleParameter("P_JRNAL_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _sundata.jrnal_line;
            (param[5] = new OracleParameter("P_AMOUNT", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = Math.Round(_sundata.amount, 2);
            (param[6] = new OracleParameter("P_D_C", OracleDbType.Char, null, ParameterDirection.Input)).Value = _sundata.d_c;
            (param[7] = new OracleParameter("P_ALLOCATION", OracleDbType.Char, null, ParameterDirection.Input)).Value = _sundata.allocation;
            (param[8] = new OracleParameter("P_JRNAL_TYPE", OracleDbType.Char, null, ParameterDirection.Input)).Value = _sundata.jrnal_type;
            (param[9] = new OracleParameter("P_JRNAL_SRCE", OracleDbType.Char, null, ParameterDirection.Input)).Value = _sundata.jrnal_srce;
            (param[10] = new OracleParameter("P_TREFERENCE", OracleDbType.Char, null, ParameterDirection.Input)).Value = _sundata.treference;
            (param[11] = new OracleParameter("P_DESCRIPTN", OracleDbType.Char, null, ParameterDirection.Input)).Value = _sundata.descriptn;
            (param[12] = new OracleParameter("P_ENTRY_DATE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _sundata.entry_date;
            (param[13] = new OracleParameter("P_ENTRY_PRD", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _sundata.entry_prd;
            (param[14] = new OracleParameter("P_DUE_DATE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _sundata.due_date;
            (param[15] = new OracleParameter("P_ALLOC_REF", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _sundata.alloc_ref;
            (param[16] = new OracleParameter("P_ALLOC_DATE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _sundata.alloc_date;
            (param[17] = new OracleParameter("P_ALLOC_PERIOD", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _sundata.alloc_period;
            (param[18] = new OracleParameter("P_ASSET_IND", OracleDbType.Char, null, ParameterDirection.Input)).Value = _sundata.asset_ind;
            (param[19] = new OracleParameter("P_ASSET_CODE", OracleDbType.Char, null, ParameterDirection.Input)).Value = _sundata.asset_code;
            (param[20] = new OracleParameter("P_ASSET_SUB", OracleDbType.Char, null, ParameterDirection.Input)).Value = _sundata.asset_sub;
            (param[21] = new OracleParameter("P_CONV_CODE", OracleDbType.Char, null, ParameterDirection.Input)).Value = _sundata.conv_code;
            (param[22] = new OracleParameter("P_CONV_RATE", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _sundata.conv_rate;
            (param[23] = new OracleParameter("P_OTHER_AMT", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _sundata.other_amt;
            (param[24] = new OracleParameter("P_OTHER_DP", OracleDbType.Char, null, ParameterDirection.Input)).Value = _sundata.other_dp;
            (param[25] = new OracleParameter("P_CLEARDOWN", OracleDbType.Char, null, ParameterDirection.Input)).Value = _sundata.cleardown;
            (param[26] = new OracleParameter("P_REVERSAL", OracleDbType.Char, null, ParameterDirection.Input)).Value = _sundata.reversal;
            (param[27] = new OracleParameter("P_LOSS_GAIN", OracleDbType.Char, null, ParameterDirection.Input)).Value = _sundata.loss_gain;
            (param[28] = new OracleParameter("P_ROUGH_FLAG", OracleDbType.Char, null, ParameterDirection.Input)).Value = _sundata.rough_flag;
            (param[29] = new OracleParameter("P_IN_USE_FLAG", OracleDbType.Char, null, ParameterDirection.Input)).Value = _sundata.in_use_flag;
            (param[30] = new OracleParameter("P_ANAL_T0", OracleDbType.Char, null, ParameterDirection.Input)).Value = _sundata.anal_t0;
            (param[31] = new OracleParameter("P_ANAL_T1", OracleDbType.Char, null, ParameterDirection.Input)).Value = _sundata.anal_t1;
            (param[32] = new OracleParameter("P_ANAL_T2", OracleDbType.Char, null, ParameterDirection.Input)).Value = _sundata.anal_t2;
            (param[33] = new OracleParameter("P_ANAL_T3", OracleDbType.Char, null, ParameterDirection.Input)).Value = _sundata.anal_t3;
            (param[34] = new OracleParameter("P_ANAL_T4", OracleDbType.Char, null, ParameterDirection.Input)).Value = _sundata.anal_t4;
            (param[35] = new OracleParameter("P_ANAL_T5", OracleDbType.Char, null, ParameterDirection.Input)).Value = _sundata.anal_t5;
            (param[36] = new OracleParameter("P_ANAL_T6", OracleDbType.Char, null, ParameterDirection.Input)).Value = _sundata.anal_t6;
            (param[37] = new OracleParameter("P_ANAL_T7", OracleDbType.Char, null, ParameterDirection.Input)).Value = _sundata.anal_t7;
            (param[38] = new OracleParameter("P_ANAL_T8", OracleDbType.Char, null, ParameterDirection.Input)).Value = _sundata.anal_t8;
            (param[39] = new OracleParameter("P_ANAL_T9", OracleDbType.Char, null, ParameterDirection.Input)).Value = _sundata.anal_t9;
            (param[40] = new OracleParameter("P_POSTING_DATE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _sundata.posting_date;
            (param[41] = new OracleParameter("P_ALLOC_IN_PROGRESS", OracleDbType.Char, null, ParameterDirection.Input)).Value = _sundata.alloc_in_progress;
            (param[42] = new OracleParameter("P_HOLD_REF", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _sundata.hold_ref;
            (param[43] = new OracleParameter("P_HOLD_OP_ID", OracleDbType.Char, null, ParameterDirection.Input)).Value = _sundata.hold_op_id;
            (param[44] = new OracleParameter("P_LAST_CHANGE_USER_ID", OracleDbType.Char, null, ParameterDirection.Input)).Value = _sundata.last_change_user_id;
            (param[45] = new OracleParameter("P_LAST_CHANGE_DATE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _sundata.last_change_date;
            (param[46] = new OracleParameter("P_ORIGINATOR_ID", OracleDbType.Char, null, ParameterDirection.Input)).Value = _sundata.originator_id;
            (param[47] = new OracleParameter("P_COM", OracleDbType.Char, null, ParameterDirection.Input)).Value = _sundata.Com;
            param[48] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            Int32 result = UpdateRecords("sp_save_suninvdirupload", CommandType.StoredProcedure, param);
            return result;
        }
        public Int32 UPDATE_JNALNUMBER( string com)
        {
            Int32 effects = 0;
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            param[1] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("UPDATE_JNALNUMBER", CommandType.StoredProcedure, param);
            return effects;
        }
        //Dilshan on 19-04-2018 Get Data for Sales with gp product wise
        public DataTable Get_Sales_GPProduct(DateTime frmDate, DateTime toDate, string docNo, string comCode)
        {
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_fromdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmDate;
            (param[1] = new OracleParameter("p_todate", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDate;
            (param[2] = new OracleParameter("p_docNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docNo;
            (param[3] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comCode;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblSales", "sp_get_sales_gpproduct", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        public DataTable Get_Sales_GPSales(DateTime frmDate, DateTime toDate, string docNo, string comCode)
        {
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_fromdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmDate;
            (param[1] = new OracleParameter("p_todate", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDate;
            (param[2] = new OracleParameter("p_docNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docNo;
            (param[3] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comCode;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblSales", "sp_get_sales_gpsales", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        public DataTable GetBusinessEntityAllValues(string category, string type_)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_cate", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = category;
            (param[1] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type_;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbData", "sp_get_businessEnt_ValDet", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        public List<CUSTOMER_SALES> getCustomerDetails(DateTime SalesFrom, DateTime SalesTo, decimal CheckAmount, string company,
           string pclst, string MainCat, string Brand, string txtModel, string txtItem, string filterby, string cat2,
           string cat3, Int32 visit, string age, string salary, string customer, string invtype, string schemetype,
           string schemecode, string CTown, string PTown, string BankCode, string Withserial, string Paymenttype,
           string Channel, string Subchnl, string Area, string Region, string Zone, string pc, string user, string dist, string prov)
        {
            try
            {
                List<CUSTOMER_SALES> finres = new List<CUSTOMER_SALES>();
                OracleParameter[] param = new OracleParameter[32];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("p_fromdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = Convert.ToDateTime(SalesFrom.ToString("dd/MMM/yyyy")).Date;
                (param[2] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = Convert.ToDateTime(SalesTo.ToString("dd/MMM/yyyy")).Date;
                (param[3] = new OracleParameter("p_chkamt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = CheckAmount;
                (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[5] = new OracleParameter("p_chnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Channel;
                (param[6] = new OracleParameter("p_subchnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Subchnl;
                (param[7] = new OracleParameter("p_area", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Area;
                (param[8] = new OracleParameter("p_region", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Subchnl;
                (param[9] = new OracleParameter("p_zone", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Subchnl;
                (param[10] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
                (param[11] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = user;
                (param[12] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = txtItem;
                (param[13] = new OracleParameter("p_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = txtModel;
                (param[14] = new OracleParameter("p_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Brand;
                (param[15] = new OracleParameter("p_cat1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = MainCat;
                (param[16] = new OracleParameter("p_cat2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cat2;
                (param[17] = new OracleParameter("p_cat3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cat3;
                (param[18] = new OracleParameter("p_reptype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = filterby;
                (param[19] = new OracleParameter("p_customer", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = customer;
                (param[20] = new OracleParameter("p_numberofvisit", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = visit;
                (param[21] = new OracleParameter("p_age", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = age;
                (param[22] = new OracleParameter("p_salary", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = salary;
                (param[23] = new OracleParameter("p_custown", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = CTown;
                (param[24] = new OracleParameter("p_purctown", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PTown;
                (param[25] = new OracleParameter("p_saletype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = invtype;
                (param[26] = new OracleParameter("p_schematype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = schemetype;
                (param[27] = new OracleParameter("p_schemacd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = schemecode;
                (param[28] = new OracleParameter("p_paytype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Paymenttype;
                (param[29] = new OracleParameter("p_crdcdbnk", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = BankCode;
                (param[30] = new OracleParameter("p_dist", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = dist;
                (param[31] = new OracleParameter("p_prov", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = prov;


                //(param[9] = new OracleParameter("p_itemcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = txtItem;
                //(param[10] = new OracleParameter("p_filterby", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = filterby;

                //(param[13] = new OracleParameter("p_visit", OracleDbType.Int32, null, ParameterDirection.Input)).Value = visit;
                //(param[14] = new OracleParameter("p_age", OracleDbType.Int32, null, ParameterDirection.Input)).Value = age;
                //(param[16] = new OracleParameter("p_customer", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = customer;
                //(param[17] = new OracleParameter("p_invtype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = invtype;
                //(param[18] = new OracleParameter("P_schemetype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = schemetype;
                //(param[19] = new OracleParameter("P_schemecode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = schemecode;
                //(param[20] = new OracleParameter("p_ctown", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = CTown;
                //(param[21] = new OracleParameter("p_ptown", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PTown;
                //(param[22] = new OracleParameter("p_bankcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = BankCode;
                //(param[23] = new OracleParameter("p_withserial", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Withserial;
                //(param[24] = new OracleParameter("p_paytype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Paymenttype;

                DataTable _dtResults = QueryDataTable("tblcrg", "sp_customer_analysis", CommandType.StoredProcedure, false, param);
                //DataTable _dtResults = QueryDataTable("tblcrg", "pkg_dashboard.SP_GET_CUSTOMERSALES_NEW", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    //if (filterby == "item")
                    //{
                    finres = DataTableExtensions.ToGenericList<CUSTOMER_SALES>(_dtResults, CUSTOMER_SALES.CustomerDetails);
                    //}
                    //else
                    //{
                    //    finres = DataTableExtensions.ToGenericList<CUSTOMER_SALES>(_dtResults, CUSTOMER_SALES.ConverterSub);
                    //}
                }

                return finres;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CUSTOMER_SALES> getCustomerDetails_new(string town, string selectedcompany, string mode, string procenter, string district, string province, string dist, string prov, DateTime SalesFrom, DateTime SalesTo, decimal CheckAmount, string CheckAge, string CheckSalary)
        {
            try
            {
                List<CUSTOMER_SALES> finres = new List<CUSTOMER_SALES>();
                OracleParameter[] param = new OracleParameter[14];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("p_fromdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = Convert.ToDateTime(SalesFrom.ToString("dd/MMM/yyyy")).Date;
                (param[2] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = Convert.ToDateTime(SalesTo.ToString("dd/MMM/yyyy")).Date;
                (param[3] = new OracleParameter("p_chkamt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = CheckAmount;
                (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = selectedcompany;
                (param[5] = new OracleParameter("p_mode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mode;
                (param[6] = new OracleParameter("p_procenter", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = procenter;
                (param[7] = new OracleParameter("p_district", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = district;
                (param[8] = new OracleParameter("p_province", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = province;
                (param[9] = new OracleParameter("p_dist", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = dist;
                (param[10] = new OracleParameter("p_prov", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = prov;
                (param[11] = new OracleParameter("p_age", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = CheckAge;
                (param[12] = new OracleParameter("p_salary", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = CheckSalary;
                (param[13] = new OracleParameter("p_town", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = town;

                DataTable _dtResults = QueryDataTable("tblcrg", "sp_customer_analysisnew", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    finres = DataTableExtensions.ToGenericList<CUSTOMER_SALES>(_dtResults, CUSTOMER_SALES.CustomerDetails);
                }

                return finres;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //subodana 2017-07-20
        public List<trn_inv_hdr> GetInvHdrAct(string doc, string com)
        {
            List<trn_inv_hdr> itmList = new List<trn_inv_hdr>();
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_COMPANY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("P_INVNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = doc;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_INV_HDR_ACTIVE", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                itmList = DataTableExtensions.ToGenericList<trn_inv_hdr>(_dtResults, trn_inv_hdr.Converter);
            }
            return itmList;
        }

        public int saveSettlementRequestAllocate(string Settno, string userid, DateTime credt, string sessionid)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[5];
                Int32 effects = 0;

                (param[0] = new OracleParameter("p_settle_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Settno;
                (param[1] = new OracleParameter("p_mod_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = sessionid;
                (param[2] = new OracleParameter("p_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userid;
                (param[3] = new OracleParameter("p_mod_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = credt;
                param[4] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int32)UpdateRecords("sp_save_settlementdtl_allocate", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Get_Job_Invoice_Detail(DateTime frmDate, DateTime toDate, string comCode, string proCntCode, string jobstatus)
        {
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("fromdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmDate;
            (param[1] = new OracleParameter("todate", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            (param[2] = new OracleParameter("com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comCode;
            (param[3] = new OracleParameter("pcenter", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = proCntCode;
            (param[4] = new OracleParameter("jobsts", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobstatus;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("InvoiceViseReport", "sp_get_job_invoice_details", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        public DataTable GetSunFwdDetails(string category, string type_)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_cate", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = category;
            (param[1] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type_;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbData", "sp_get_sunfwddetails", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        public DataTable Get_Cost_Of_Sales(DateTime frmDate, DateTime toDate, string comCode, string proCntCode, string userId)
        {
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmDate;
            (param[1] = new OracleParameter("tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDate;
            (param[2] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comCode;
            (param[3] = new OracleParameter("in_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = proCntCode;
            (param[4] = new OracleParameter("in_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userId;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblDebtorsOutstanding", "SP_GET_COST_OF_SALES", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        public DataTable Get_Cost_Of_Sales_Req(DateTime frmDate, DateTime toDate, string comCode, string proCntCode, string userId)
        {
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmDate;
            (param[1] = new OracleParameter("tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDate;
            (param[2] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comCode;
            (param[3] = new OracleParameter("in_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = proCntCode;
            (param[4] = new OracleParameter("in_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userId;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblDebtorsOutstanding", "SP_GET_COST_OF_SALES_REQ", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        public DataTable Get_Cost_Of_Sales_Hdr(DateTime frmDate, DateTime toDate, string comCode, string proCntCode, string userId)
        {
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmDate;
            (param[1] = new OracleParameter("tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDate;
            (param[2] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comCode;
            (param[3] = new OracleParameter("in_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = proCntCode;
            (param[4] = new OracleParameter("in_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userId;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblDebtorsOutstanding", "SP_GET_COST_OF_SALES_HDR", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        public DataTable Get_GP_Closed_Job(DateTime frmDate, DateTime toDate, string comCode, string proCntCode, string userId)
        {
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmDate;
            (param[1] = new OracleParameter("tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDate;
            (param[2] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comCode;
            (param[3] = new OracleParameter("in_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = proCntCode;
            (param[4] = new OracleParameter("in_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userId;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblDebtorsOutstanding", "SP_GET_GP_CLOSED_JOBS", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        public DataTable Get_GP_Closed_Job_Cost(DateTime frmDate, DateTime toDate, string comCode, string proCntCode, string userId)
        {
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmDate;
            (param[1] = new OracleParameter("tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDate;
            (param[2] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comCode;
            (param[3] = new OracleParameter("in_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = proCntCode;
            (param[4] = new OracleParameter("in_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userId;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblDebtorsOutstanding", "SP_GET_GP_CLOSED_JOBS_COST", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        public DataTable Get_Pending_Adv(DateTime frmDate, DateTime toDate, string comCode, string proCntCode, string userId)
        {
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmDate;
            (param[1] = new OracleParameter("tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDate;
            (param[2] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comCode;
            (param[3] = new OracleParameter("in_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = proCntCode;
            (param[4] = new OracleParameter("in_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userId;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblDebtorsOutstanding", "SP_GET_PENDING_ADV", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        //DILSHAN 
        public DataTable Get_Debtors_Out_Summ(DateTime frmDate, DateTime toDate, string cusCode, string comCode, string proCntCode, string userId)
        {
            OracleParameter[] param = new OracleParameter[7];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmDate;
            (param[2] = new OracleParameter("tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDate;
            (param[3] = new OracleParameter("in_cust", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cusCode;
            (param[4] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comCode;
            (param[5] = new OracleParameter("in_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = proCntCode;
            (param[6] = new OracleParameter("in_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userId;
            DataTable _dtResults = QueryDataTable("tblDebtorsOutstanding", "SP_GET_DEBTOR_OUTSTANDING_SUMM", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        //dilshan 
        public DataTable GetJobStatusForSun(string _company, string _type, string _no)
        {
            OracleParameter[] param = new OracleParameter[4];
            //string _isValid = "";
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("P_TYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _type;
            (param[2] = new OracleParameter("P_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _no;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _tblSunDetails = QueryDataTable("tblvaliddivision", "SP_GETJOBSTATUS", CommandType.StoredProcedure, false, param);
            //if (_tblvalidsupplier != null)
            //{
            //    if (_tblvalidsupplier.Rows.Count > 0)
            //    {
            //        _isValid = _tblvalidsupplier.Rows[0][0].ToString();
            //    }
            //}

            return _tblSunDetails;
        }
        //dilshan 
        public DataTable GetFwdAccForSun(string _company, string _type, string _pc, string _doctype)
        {
            OracleParameter[] param = new OracleParameter[5];
            //string _isValid = "";
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("P_TYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _type;
            (param[2] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            (param[3] = new OracleParameter("P_DOCTYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _doctype;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _tblSunDetails = QueryDataTable("tblvaliddivision", "SP_GETFWDSUNACC", CommandType.StoredProcedure, false, param);
            //if (_tblvalidsupplier != null)
            //{
            //    if (_tblvalidsupplier.Rows.Count > 0)
            //    {
            //        _isValid = _tblvalidsupplier.Rows[0][0].ToString();
            //    }
            //}

            return _tblSunDetails;
        }
    }
}
