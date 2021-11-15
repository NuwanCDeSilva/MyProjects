using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class FA_Inventory_Req_Hdr
    {
        #region Private Members
        private string _dips_town;
        private string _disp_aodno;
        private DateTime _disp_approved_date;
        private string _disp_authorized_by1;
        private string _disp_authorized_by2;
        private string _disp_authorized_by3;
        private string _disp_collector_id;
        private string _disp_collector_name;
        private string _disp_comments;
        private string _disp_company;
        private string _disp_created_by;
        private DateTime _disp_created_date;
        private string _disp_cust_code;
        private DateTime _disp_date;
        private string _disp_del_by;
        private string _disp_doc_ref_no;
        private string _disp_last_modify_by;
        private DateTime _disp_last_modify_when;
        private string _disp_location;
        private Int32 _disp_pda_status;
        private string _disp_prefer_location;
        private string _disp_production_job;
        private string _disp_production_stage;
        private string _disp_ref_no;
        private string _disp_reqt_seq;
        private DateTime _disp_request_date;
        private string _disp_request_type;
        private string _disp_req_by;
        private string _disp_sbu;
        private Boolean _disp_skd_req;
        private Boolean _disp_skip_app;
        private string _disp_status;
        private string _disp_sub_type;
        private string _disp_temp_loca;
        private string _disp_type;
        private string _main_mrn_no;
        private string _pro_mrn_sub_type;

        List<FA_Inventory_Req_Itm> _inventoryRequestItemList = null;

        #endregion

        public List<FA_Inventory_Req_Itm> InventoryRequestItemList
        {
            get { return _inventoryRequestItemList; }
            set { _inventoryRequestItemList = value; }
        }
        public string Dips_town
        {
            get { return _dips_town; }
            set { _dips_town = value; }
        }
        public string Disp_aodno
        {
            get { return _disp_aodno; }
            set { _disp_aodno = value; }
        }
        public DateTime Disp_approved_date
        {
            get { return _disp_approved_date; }
            set { _disp_approved_date = value; }
        }
        public string Disp_authorized_by1
        {
            get { return _disp_authorized_by1; }
            set { _disp_authorized_by1 = value; }
        }
        public string Disp_authorized_by2
        {
            get { return _disp_authorized_by2; }
            set { _disp_authorized_by2 = value; }
        }
        public string Disp_authorized_by3
        {
            get { return _disp_authorized_by3; }
            set { _disp_authorized_by3 = value; }
        }
        public string Disp_collector_id
        {
            get { return _disp_collector_id; }
            set { _disp_collector_id = value; }
        }
        public string Disp_collector_name
        {
            get { return _disp_collector_name; }
            set { _disp_collector_name = value; }
        }
        public string Disp_comments
        {
            get { return _disp_comments; }
            set { _disp_comments = value; }
        }
        public string Disp_company
        {
            get { return _disp_company; }
            set { _disp_company = value; }
        }
        public string Disp_created_by
        {
            get { return _disp_created_by; }
            set { _disp_created_by = value; }
        }
        public DateTime Disp_created_date
        {
            get { return _disp_created_date; }
            set { _disp_created_date = value; }
        }
        public string Disp_cust_code
        {
            get { return _disp_cust_code; }
            set { _disp_cust_code = value; }
        }
        public DateTime Disp_date
        {
            get { return _disp_date; }
            set { _disp_date = value; }
        }
        public string Disp_del_by
        {
            get { return _disp_del_by; }
            set { _disp_del_by = value; }
        }
        public string Disp_doc_ref_no
        {
            get { return _disp_doc_ref_no; }
            set { _disp_doc_ref_no = value; }
        }
        public string Disp_last_modify_by
        {
            get { return _disp_last_modify_by; }
            set { _disp_last_modify_by = value; }
        }
        public DateTime Disp_last_modify_when
        {
            get { return _disp_last_modify_when; }
            set { _disp_last_modify_when = value; }
        }
        public string Disp_location
        {
            get { return _disp_location; }
            set { _disp_location = value; }
        }
        public Int32 Disp_pda_status
        {
            get { return _disp_pda_status; }
            set { _disp_pda_status = value; }
        }
        public string Disp_prefer_location
        {
            get { return _disp_prefer_location; }
            set { _disp_prefer_location = value; }
        }
        public string Disp_production_job
        {
            get { return _disp_production_job; }
            set { _disp_production_job = value; }
        }
        public string Disp_production_stage
        {
            get { return _disp_production_stage; }
            set { _disp_production_stage = value; }
        }
        public string Disp_ref_no
        {
            get { return _disp_ref_no; }
            set { _disp_ref_no = value; }
        }
        public string Disp_reqt_seq
        {
            get { return _disp_reqt_seq; }
            set { _disp_reqt_seq = value; }
        }
        public DateTime Disp_request_date
        {
            get { return _disp_request_date; }
            set { _disp_request_date = value; }
        }
        public string Disp_request_type
        {
            get { return _disp_request_type; }
            set { _disp_request_type = value; }
        }
        public string Disp_req_by
        {
            get { return _disp_req_by; }
            set { _disp_req_by = value; }
        }
        public string Disp_sbu
        {
            get { return _disp_sbu; }
            set { _disp_sbu = value; }
        }
        public Boolean Disp_skd_req
        {
            get { return _disp_skd_req; }
            set { _disp_skd_req = value; }
        }
        public Boolean Disp_skip_app
        {
            get { return _disp_skip_app; }
            set { _disp_skip_app = value; }
        }
        public string Disp_status
        {
            get { return _disp_status; }
            set { _disp_status = value; }
        }
        public string Disp_sub_type
        {
            get { return _disp_sub_type; }
            set { _disp_sub_type = value; }
        }
        public string Disp_temp_loca
        {
            get { return _disp_temp_loca; }
            set { _disp_temp_loca = value; }
        }
        public string Disp_type
        {
            get { return _disp_type; }
            set { _disp_type = value; }
        }
        public string Main_mrn_no
        {
            get { return _main_mrn_no; }
            set { _main_mrn_no = value; }
        }
        public string Pro_mrn_sub_type
        {
            get { return _pro_mrn_sub_type; }
            set { _pro_mrn_sub_type = value; }
        }




        public static FA_Inventory_Req_Hdr Converter(DataRow row)
        {
            return new FA_Inventory_Req_Hdr
            {

                Dips_town = row["DIPS_TOWN"] == DBNull.Value ? string.Empty : row["DIPS_TOWN"].ToString(),
                Disp_aodno = row["DISP_AODNO"] == DBNull.Value ? string.Empty : row["DISP_AODNO"].ToString(),
                Disp_approved_date = row["DISP_APPROVED_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["DISP_APPROVED_DATE"]),
                Disp_authorized_by1 = row["DISP_AUTHORIZED_BY1"] == DBNull.Value ? string.Empty : row["DISP_AUTHORIZED_BY1"].ToString(),
                Disp_authorized_by2 = row["DISP_AUTHORIZED_BY2"] == DBNull.Value ? string.Empty : row["DISP_AUTHORIZED_BY2"].ToString(),
                Disp_authorized_by3 = row["DISP_AUTHORIZED_BY3"] == DBNull.Value ? string.Empty : row["DISP_AUTHORIZED_BY3"].ToString(),
                Disp_collector_id = row["DISP_COLLECTOR_ID"] == DBNull.Value ? string.Empty : row["DISP_COLLECTOR_ID"].ToString(),
                Disp_collector_name = row["DISP_COLLECTOR_NAME"] == DBNull.Value ? string.Empty : row["DISP_COLLECTOR_NAME"].ToString(),
                Disp_comments = row["DISP_COMMENTS"] == DBNull.Value ? string.Empty : row["DISP_COMMENTS"].ToString(),
                Disp_company = row["DISP_COMPANY"] == DBNull.Value ? string.Empty : row["DISP_COMPANY"].ToString(),
                Disp_created_by = row["DISP_CREATED_BY"] == DBNull.Value ? string.Empty : row["DISP_CREATED_BY"].ToString(),
                Disp_created_date = row["DISP_CREATED_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["DISP_CREATED_DATE"]),
                Disp_cust_code = row["DISP_CUST_CODE"] == DBNull.Value ? string.Empty : row["DISP_CUST_CODE"].ToString(),
                Disp_date = row["DISP_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["DISP_DATE"]),
                Disp_del_by = row["DISP_DEL_BY"] == DBNull.Value ? string.Empty : row["DISP_DEL_BY"].ToString(),
                Disp_doc_ref_no = row["DISP_DOC_REF_NO"] == DBNull.Value ? string.Empty : row["DISP_DOC_REF_NO"].ToString(),
                Disp_last_modify_by = row["DISP_LAST_MODIFY_BY"] == DBNull.Value ? string.Empty : row["DISP_LAST_MODIFY_BY"].ToString(),
                Disp_last_modify_when = row["DISP_LAST_MODIFY_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["DISP_LAST_MODIFY_WHEN"]),
                Disp_location = row["DISP_LOCATION"] == DBNull.Value ? string.Empty : row["DISP_LOCATION"].ToString(),
                Disp_pda_status = row["DISP_PDA_STATUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["DISP_PDA_STATUS"]),
                Disp_prefer_location = row["DISP_PREFER_LOCATION"] == DBNull.Value ? string.Empty : row["DISP_PREFER_LOCATION"].ToString(),
                Disp_production_job = row["DISP_PRODUCTION_JOB"] == DBNull.Value ? string.Empty : row["DISP_PRODUCTION_JOB"].ToString(),
                Disp_production_stage = row["DISP_PRODUCTION_STAGE"] == DBNull.Value ? string.Empty : row["DISP_PRODUCTION_STAGE"].ToString(),
                Disp_ref_no = row["DISP_REF_NO"] == DBNull.Value ? string.Empty : row["DISP_REF_NO"].ToString(),
                Disp_reqt_seq = row["DISP_REQT_SEQ"] == DBNull.Value ? string.Empty : row["DISP_REQT_SEQ"].ToString(),
                Disp_request_date = row["DISP_REQUEST_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["DISP_REQUEST_DATE"]),
                Disp_request_type = row["DISP_REQUEST_TYPE"] == DBNull.Value ? string.Empty : row["DISP_REQUEST_TYPE"].ToString(),
                Disp_req_by = row["DISP_REQ_BY"] == DBNull.Value ? string.Empty : row["DISP_REQ_BY"].ToString(),
                Disp_sbu = row["DISP_SBU"] == DBNull.Value ? string.Empty : row["DISP_SBU"].ToString(),
                Disp_skd_req = row["DISP_SKD_REQ"] == DBNull.Value ? false : Convert.ToBoolean(row["DISP_SKD_REQ"]),
                Disp_skip_app = row["DISP_SKIP_APP"] == DBNull.Value ? false : Convert.ToBoolean(row["DISP_SKIP_APP"]),
                Disp_status = row["DISP_STATUS"] == DBNull.Value ? string.Empty : row["DISP_STATUS"].ToString(),
                Disp_sub_type = row["DISP_SUB_TYPE"] == DBNull.Value ? string.Empty : row["DISP_SUB_TYPE"].ToString(),
                Disp_temp_loca = row["DISP_TEMP_LOCA"] == DBNull.Value ? string.Empty : row["DISP_TEMP_LOCA"].ToString(),
                Disp_type = row["DISP_TYPE"] == DBNull.Value ? string.Empty : row["DISP_TYPE"].ToString(),
                Main_mrn_no = row["MAIN_MRN_NO"] == DBNull.Value ? string.Empty : row["MAIN_MRN_NO"].ToString(),
                Pro_mrn_sub_type = row["PRO_MRN_SUB_TYPE"] == DBNull.Value ? string.Empty : row["PRO_MRN_SUB_TYPE"].ToString()



            };
        }

    }
}
